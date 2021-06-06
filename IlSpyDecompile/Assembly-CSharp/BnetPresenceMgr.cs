using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using bgs;
using bgs.types;
using Blizzard.T5.Core;
using Hearthstone;
using PegasusFSG;
using PegasusShared;
using SpectatorProto;

public class BnetPresenceMgr
{
	public delegate void PlayersChangedCallback(BnetPlayerChangelist changelist, object userData);

	private class PlayersChangedListener : EventListener<PlayersChangedCallback>
	{
		public void Fire(BnetPlayerChangelist changelist)
		{
			m_callback(changelist, m_userData);
		}
	}

	private static BnetPresenceMgr s_instance;

	private Map<BnetAccountId, BnetAccount> m_accounts = new Map<BnetAccountId, BnetAccount>();

	private Map<BnetGameAccountId, BnetGameAccount> m_gameAccounts = new Map<BnetGameAccountId, BnetGameAccount>();

	private Map<BnetAccountId, BnetPlayer> m_players = new Map<BnetAccountId, BnetPlayer>();

	private BnetAccountId m_myBattleNetAccountId;

	private BnetGameAccountId m_myGameAccountId;

	private BnetPlayer m_myPlayer;

	private List<PlayersChangedListener> m_playersChangedListeners = new List<PlayersChangedListener>();

	public event Action<PresenceUpdate[]> OnGameAccountPresenceChange;

	public static BnetPresenceMgr Get()
	{
		if (s_instance == null)
		{
			s_instance = new BnetPresenceMgr();
			HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
			if (hearthstoneApplication != null)
			{
				hearthstoneApplication.WillReset += delegate
				{
					BnetPresenceMgr bnetPresenceMgr = s_instance;
					s_instance = new BnetPresenceMgr();
					s_instance.m_playersChangedListeners = bnetPresenceMgr.m_playersChangedListeners;
					s_instance.OnGameAccountPresenceChange = bnetPresenceMgr.OnGameAccountPresenceChange;
				};
			}
			else
			{
				Log.BattleNet.PrintWarning("BnetPresenceMgr.Get(): HearthstoneApplication.Get() returned null. Unable to subscribe to HearthstoneApplication.WillReset.");
			}
		}
		return s_instance;
	}

	public void Initialize()
	{
		Network.Get().SetPresenceHandler(OnPresenceUpdate);
		BnetEventMgr.Get().AddChangeListener(OnBnetEventOccurred);
		EntityId myGameAccountId = BattleNet.GetMyGameAccountId();
		m_myGameAccountId = BnetGameAccountId.CreateFromEntityId(myGameAccountId);
		EntityId myAccoundId = BattleNet.GetMyAccoundId();
		m_myBattleNetAccountId = BnetAccountId.CreateFromEntityId(myAccoundId);
	}

	public void Shutdown()
	{
		Network.Get().SetPresenceHandler(null);
	}

	public BnetGameAccountId GetMyGameAccountId()
	{
		return m_myGameAccountId;
	}

	public BnetId GetMyGameAccountIdAsBnetId()
	{
		return BnetUtils.CreatePegasusBnetId(m_myGameAccountId);
	}

	public BnetId GetMyBattleNetAccountIdAsBnetId()
	{
		return BnetUtils.CreatePegasusBnetId(m_myBattleNetAccountId);
	}

	public BnetPlayer GetMyPlayer()
	{
		return m_myPlayer;
	}

	public BnetAccount GetAccount(BnetAccountId id)
	{
		if (id == null)
		{
			return null;
		}
		BnetAccount value = null;
		m_accounts.TryGetValue(id, out value);
		return value;
	}

	public BnetGameAccount GetGameAccount(BnetGameAccountId id)
	{
		if (id == null)
		{
			return null;
		}
		BnetGameAccount value = null;
		m_gameAccounts.TryGetValue(id, out value);
		return value;
	}

	public BnetPlayer GetPlayer(BnetAccountId id)
	{
		if (id == null)
		{
			return null;
		}
		BnetPlayer value = null;
		m_players.TryGetValue(id, out value);
		return value;
	}

	public BnetPlayer GetPlayer(BnetGameAccountId id)
	{
		BnetGameAccount gameAccount = GetGameAccount(id);
		if (gameAccount == null)
		{
			return null;
		}
		return GetPlayer(gameAccount.GetOwnerId());
	}

	public BnetPlayer RegisterPlayer(BnetPlayerSource source, BnetAccountId accountId, BnetGameAccountId gameAccountId = null, BnetProgramId programId = null)
	{
		BnetPlayer player = GetPlayer(accountId);
		if (player != null)
		{
			return player;
		}
		player = new BnetPlayer(source);
		player.SetAccountId(accountId);
		m_players[accountId] = player;
		BnetAccount bnetAccount = new BnetAccount();
		m_accounts.Add(accountId, bnetAccount);
		bnetAccount.SetId(accountId);
		player.SetAccount(bnetAccount);
		if (gameAccountId != null)
		{
			if (!m_gameAccounts.TryGetValue(gameAccountId, out var value))
			{
				value = new BnetGameAccount();
				value.SetId(gameAccountId);
				value.SetOwnerId(accountId);
				m_gameAccounts.Add(gameAccountId, value);
				if (programId != null)
				{
					value.SetProgramId(programId);
				}
			}
			player.AddGameAccount(value);
		}
		BnetPlayerChange bnetPlayerChange = new BnetPlayerChange();
		bnetPlayerChange.SetNewPlayer(player);
		BnetPlayerChangelist bnetPlayerChangelist = new BnetPlayerChangelist();
		bnetPlayerChangelist.AddChange(bnetPlayerChange);
		FirePlayersChangedEvent(bnetPlayerChangelist);
		return player;
	}

	public void RegisterBnetPlayer(BnetPlayer player)
	{
		if (player == null || player.GetAccount() == null || player.GetAccountId() == null)
		{
			return;
		}
		bool flag = false;
		BnetAccountId accountId = player.GetAccountId();
		if (m_players.TryGetValue(accountId, out var value))
		{
			if (value != player)
			{
				flag = true;
				Log.All.PrintWarning("Already registered BnetPlayer accountId={0} newSrc={1} - will overwrite.", accountId.GetLo(), player.Source);
			}
		}
		else
		{
			flag = true;
		}
		m_players[accountId] = player;
		if (m_accounts.TryGetValue(accountId, out var value2))
		{
			if ((object)value2 != player.GetAccount())
			{
				flag = true;
				Log.All.PrintWarning("Already registered BnetAccount accountId={0} newSrc={1} - will overwrite.", accountId.GetLo(), player.Source);
			}
		}
		else
		{
			flag = true;
		}
		m_accounts[accountId] = player.GetAccount();
		foreach (KeyValuePair<BnetGameAccountId, BnetGameAccount> gameAccount in player.GetGameAccounts())
		{
			BnetGameAccountId key = gameAccount.Key;
			BnetGameAccount value3 = gameAccount.Value;
			if (m_gameAccounts.TryGetValue(key, out var value4))
			{
				if ((object)value4 != value3)
				{
					flag = true;
					Log.All.PrintWarning("Already registered BnetAccount accountId={0} newSrc={1} - will overwrite.", accountId.GetLo(), player.Source);
				}
			}
			else
			{
				flag = true;
			}
			m_gameAccounts[key] = value3;
		}
		if (flag)
		{
			BnetPlayerChange bnetPlayerChange = new BnetPlayerChange();
			bnetPlayerChange.SetNewPlayer(player);
			BnetPlayerChangelist bnetPlayerChangelist = new BnetPlayerChangelist();
			bnetPlayerChangelist.AddChange(bnetPlayerChange);
			FirePlayersChangedEvent(bnetPlayerChangelist);
		}
	}

	public bool IsSubscribedToPlayer(BnetGameAccountId id)
	{
		return BattleNet.IsSubscribedToEntity(new EntityId(BnetEntityId.CreateForProtocol(id)));
	}

	public void CheckSubscriptionsAndClearTransientStatus(BnetAccountId accountId)
	{
		if (!m_players.TryGetValue(accountId, out var value))
		{
			return;
		}
		foreach (KeyValuePair<BnetGameAccountId, BnetGameAccount> gameAccount in value.GetGameAccounts())
		{
			CheckSubscriptionsAndClearTransientStatus_Internal(gameAccount.Value);
		}
	}

	public void CheckSubscriptionsAndClearTransientStatus(BnetGameAccountId gameAccountId)
	{
		if (m_gameAccounts.TryGetValue(gameAccountId, out var value))
		{
			CheckSubscriptionsAndClearTransientStatus_Internal(value);
		}
	}

	private void CheckSubscriptionsAndClearTransientStatus_Internal(BnetGameAccount gameAccount)
	{
		BnetGameAccountId id = gameAccount.GetId();
		if (!IsSubscribedToPlayer(id))
		{
			uint[] transientStatusFields = GamePresenceField.TransientStatusFields;
			foreach (uint fieldId in transientStatusFields)
			{
				gameAccount.SetGameField(fieldId, null);
			}
			gameAccount.SetOnline(BnetNearbyPlayerMgr.Get().IsNearbyPlayer(gameAccount.GetId()));
			gameAccount.SetBusy(busy: false);
			gameAccount.SetAway(away: false);
			gameAccount.SetAwayTimeMicrosec(0L);
			gameAccount.SetRichPresence(null);
		}
	}

	public static void RequestPlayerBattleTag(BnetAccountId id)
	{
		EntityId entityId = default(EntityId);
		entityId.hi = id.GetHi();
		entityId.lo = id.GetLo();
		PresenceFieldKey[] fieldList = new List<PresenceFieldKey>
		{
			new PresenceFieldKey
			{
				programId = BnetProgramId.BNET.GetValue(),
				groupId = 1u,
				fieldId = 4u,
				uniqueId = 0uL
			}
		}.ToArray();
		BattleNet.RequestPresenceFields(isGameAccountEntityId: false, entityId, fieldList);
		Log.Presence.Print("Requesting BattleTag for player {0}!", id);
	}

	public bool SetGameField(uint fieldId, bool val)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			Error.AddDevFatal("Caller should check for Battle.net connection before calling SetGameField {0}={1}", fieldId, val);
			return false;
		}
		if (!ShouldUpdateGameField(fieldId, val, out var hsGameAccount))
		{
			return false;
		}
		if (fieldId == 2)
		{
			hsGameAccount.SetBusy(val);
			int num = (val ? 1 : 0);
			BattleNet.SetPresenceInt(fieldId, num);
		}
		else
		{
			BattleNet.SetPresenceBool(fieldId, val);
		}
		BnetPlayerChangelist changelist = ChangeGameField(hsGameAccount, fieldId, val);
		switch (fieldId)
		{
		case 2u:
			if (val)
			{
				hsGameAccount.SetAway(away: false);
			}
			break;
		case 10u:
			if (val)
			{
				hsGameAccount.SetBusy(busy: false);
			}
			break;
		}
		FirePlayersChangedEvent(changelist);
		return true;
	}

	public bool SetAccountField(uint fieldId, bool val)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			Error.AddDevFatal("Caller should check for Battle.net connection before calling SetGameField {0}={1}", fieldId, val);
			return false;
		}
		BattleNet.SetAccountLevelPresenceBool(fieldId, val);
		PresenceUpdate presenceUpdate = default(PresenceUpdate);
		presenceUpdate.entityId = BattleNet.GetMyAccoundId();
		presenceUpdate.programId = BnetProgramId.BNET.GetValue();
		presenceUpdate.groupId = 1u;
		presenceUpdate.fieldId = fieldId;
		presenceUpdate.boolVal = val;
		PresenceUpdate presenceUpdate2 = presenceUpdate;
		OnPresenceUpdate(new PresenceUpdate[1] { presenceUpdate2 });
		return true;
	}

	public bool SetGameField(uint fieldId, int val)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			Error.AddDevFatal("Caller should check for Battle.net connection before calling SetGameField {0}={1}", fieldId, val);
			return false;
		}
		if (!ShouldUpdateGameField(fieldId, val, out var hsGameAccount))
		{
			return false;
		}
		BattleNet.SetPresenceInt(fieldId, val);
		BnetPlayerChangelist changelist = ChangeGameField(hsGameAccount, fieldId, val);
		FirePlayersChangedEvent(changelist);
		return true;
	}

	public bool SetGameField(uint fieldId, string val)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			Error.AddDevFatal("Caller should check for Battle.net connection before calling SetGameField {0}={1}", fieldId, val);
			return false;
		}
		if (!ShouldUpdateGameField(fieldId, val, out var hsGameAccount))
		{
			return false;
		}
		BattleNet.SetPresenceString(fieldId, val);
		BnetPlayerChangelist changelist = ChangeGameField(hsGameAccount, fieldId, val);
		FirePlayersChangedEvent(changelist);
		return true;
	}

	public bool SetGameField(uint fieldId, byte[] val)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			Error.AddDevFatal("Caller should check for Battle.net connection before calling SetGameField {0}=[{1}]", fieldId, (val == null) ? "" : val.Length.ToString());
			return false;
		}
		if (!ShouldUpdateGameFieldBlob(fieldId, val, out var hsGameAccount))
		{
			return false;
		}
		BattleNet.SetPresenceBlob(fieldId, val);
		BnetPlayerChangelist changelist = ChangeGameField(hsGameAccount, fieldId, val);
		FirePlayersChangedEvent(changelist);
		return true;
	}

	public bool SetGameFieldBlob(uint fieldId, IProtoBuf protoMessage)
	{
		if (fieldId == 21 || fieldId == 23)
		{
			SetPresenceSpectatorJoinInfo(protoMessage as JoinInfo);
			return true;
		}
		byte[] val = ((protoMessage == null) ? null : ProtobufUtil.ToByteArray(protoMessage));
		return SetGameField(fieldId, val);
	}

	public bool SetGameField(uint fieldId, EntityId val)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			Error.AddDevFatal("Caller should check for Battle.net connection before calling SetGameField {0}=[{1}]", fieldId, val.ToString());
			return false;
		}
		if (!ShouldUpdateGameField(fieldId, val, out var hsGameAccount))
		{
			return false;
		}
		BattleNet.SetPresenceEntityId(fieldId, val);
		BnetPlayerChangelist changelist = ChangeGameField(hsGameAccount, fieldId, val);
		FirePlayersChangedEvent(changelist);
		return true;
	}

	public void SetPresenceSpectatorJoinInfo(JoinInfo joinInfo)
	{
		byte[] array = ((joinInfo == null) ? null : ProtobufUtil.ToByteArray(joinInfo));
		SetGameField(21u, array);
		byte[] val = null;
		if (joinInfo != null && FiresideGatheringManager.Get().IsCheckedIn && FiresideGatheringManager.Get().CurrentFsgSharedSecretKey != null)
		{
			byte[] currentFsgSharedSecretKey = FiresideGatheringManager.Get().CurrentFsgSharedSecretKey;
			byte[] secretKey = SHA256.Create().ComputeHash(currentFsgSharedSecretKey, 0, currentFsgSharedSecretKey.Length);
			val = ProtobufUtil.ToByteArray(new SecretJoinInfo
			{
				Source = SecretSource.SECRET_SOURCE_FIRESIDE_GATHERING,
				SpecificSourceIdentity = FiresideGatheringManager.Get().CurrentFsgId,
				EncryptedMessage = Crypto.Rijndael.Encrypt(array, secretKey)
			});
		}
		SetGameField(23u, val);
	}

	public bool SetRichPresence(Enum[] richPresence)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			Error.AddDevFatal("Caller should check for Battle.net connection before calling SetRichPresence {0}", (richPresence == null) ? "" : string.Join(", ", richPresence.Select((Enum x) => x.ToString()).ToArray()));
			return false;
		}
		if (richPresence == null)
		{
			return false;
		}
		if (richPresence.Length == 0)
		{
			return false;
		}
		RichPresenceUpdate[] array = new RichPresenceUpdate[richPresence.Length];
		for (int i = 0; i < richPresence.Length; i++)
		{
			Enum @enum = richPresence[i];
			Type type = @enum.GetType();
			FourCC fourCC = RichPresence.s_streamIds[type];
			RichPresenceUpdate richPresenceUpdate = default(RichPresenceUpdate);
			richPresenceUpdate.presenceFieldIndex = ((i != 0) ? ((uint)(458752 + i)) : 0u);
			richPresenceUpdate.programId = BnetProgramId.HEARTHSTONE.GetValue();
			richPresenceUpdate.streamId = fourCC.GetValue();
			richPresenceUpdate.index = Convert.ToUInt32(@enum);
			array[i] = richPresenceUpdate;
		}
		BattleNet.SetRichPresence(array);
		return true;
	}

	public void SetDeckValidity(DeckValidity deckValidity)
	{
		SetGameFieldBlob(24u, deckValidity);
	}

	public bool AddPlayersChangedListener(PlayersChangedCallback callback)
	{
		return AddPlayersChangedListener(callback, null);
	}

	public bool AddPlayersChangedListener(PlayersChangedCallback callback, object userData)
	{
		PlayersChangedListener playersChangedListener = new PlayersChangedListener();
		playersChangedListener.SetCallback(callback);
		playersChangedListener.SetUserData(userData);
		if (m_playersChangedListeners.Contains(playersChangedListener))
		{
			return false;
		}
		m_playersChangedListeners.Add(playersChangedListener);
		return true;
	}

	public bool RemovePlayersChangedListener(PlayersChangedCallback callback)
	{
		return RemovePlayersChangedListener(callback, null);
	}

	private bool RemovePlayersChangedListener(PlayersChangedCallback callback, object userData)
	{
		PlayersChangedListener playersChangedListener = new PlayersChangedListener();
		playersChangedListener.SetCallback(callback);
		playersChangedListener.SetUserData(userData);
		return m_playersChangedListeners.Remove(playersChangedListener);
	}

	public static bool RemovePlayersChangedListenerFromInstance(PlayersChangedCallback callback, object userData = null)
	{
		if (s_instance == null)
		{
			return false;
		}
		return s_instance.RemovePlayersChangedListener(callback, userData);
	}

	private void OnPresenceUpdate(PresenceUpdate[] updates)
	{
		BnetPlayerChangelist changelist = new BnetPlayerChangelist();
		foreach (PresenceUpdate item in updates.Where((PresenceUpdate u) => u.programId == BnetProgramId.BNET && u.groupId == 2 && u.fieldId == 7))
		{
			BnetGameAccountId bnetGameAccountId = BnetGameAccountId.CreateFromEntityId(item.entityId);
			BnetAccountId bnetAccountId = BnetAccountId.CreateFromEntityId(item.entityIdVal);
			if (!bnetAccountId.IsEmpty())
			{
				if (GetAccount(bnetAccountId) == null)
				{
					PresenceUpdate update = default(PresenceUpdate);
					BnetPlayerChangelist changelist2 = new BnetPlayerChangelist();
					CreateAccount(bnetAccountId, update, changelist2);
				}
				if (!bnetGameAccountId.IsEmpty() && GetGameAccount(bnetGameAccountId) == null)
				{
					CreateGameAccount(bnetGameAccountId, item, changelist);
				}
			}
		}
		List<PresenceUpdate> list = null;
		for (int i = 0; i < updates.Length; i++)
		{
			PresenceUpdate presenceUpdate = updates[i];
			if (presenceUpdate.programId == BnetProgramId.BNET)
			{
				if (presenceUpdate.groupId == 1)
				{
					OnAccountUpdate(presenceUpdate, changelist);
				}
				else if (presenceUpdate.groupId == 2)
				{
					OnGameAccountUpdate(presenceUpdate, changelist);
				}
			}
			else if (presenceUpdate.programId == BnetProgramId.HEARTHSTONE)
			{
				OnGameUpdate(presenceUpdate, changelist);
			}
			if ((presenceUpdate.programId == BnetProgramId.HEARTHSTONE || (presenceUpdate.programId == BnetProgramId.BNET && presenceUpdate.groupId == 2)) && this.OnGameAccountPresenceChange != null)
			{
				if (list == null)
				{
					list = new List<PresenceUpdate>();
				}
				list.Add(presenceUpdate);
			}
		}
		LogPresenceUpdates(updates);
		if (list != null)
		{
			this.OnGameAccountPresenceChange(list.ToArray());
		}
		FirePlayersChangedEvent(changelist);
	}

	private static void LogPresenceUpdates(PresenceUpdate[] updates)
	{
		Log.LogLevel level = Log.LogLevel.Debug;
		bool flag = true;
		StringBuilder buffer = null;
		foreach (PresenceUpdate update in updates)
		{
			LogPresenceUpdate(ref buffer, level, flag, update);
		}
		if (buffer != null)
		{
			Log.Presence.Print(level, flag, buffer.ToString());
		}
	}

	private static void LogPresenceUpdate(ref StringBuilder buffer, Log.LogLevel level, bool verbosity, PresenceUpdate update)
	{
		if (HearthstoneApplication.IsPublic() || !Log.Presence.CanPrint(level, verbosity))
		{
			return;
		}
		BnetAccountId id = BnetAccountId.CreateFromEntityId(update.entityId);
		BnetGameAccountId gameAccountId = BnetGameAccountId.CreateFromEntityId(update.entityId);
		bool num = update.entityId == BattleNet.GetMyAccoundId() || update.entityId == BattleNet.GetMyGameAccountId();
		BnetPlayer player = Get().GetPlayer(gameAccountId);
		if (player == null)
		{
			player = Get().GetPlayer(id);
		}
		bool flag = !num && BnetFriendMgr.Get().IsFriend(player);
		bool flag2 = !num && GameState.Get() != null && (GameMgr.Get() == null || !GameMgr.Get().IsSpectator()) && GameState.Get().GetOpposingSidePlayer() != null && GameState.Get().GetOpposingSidePlayer().GetGameAccountId() == gameAccountId;
		string text = (num ? "myself" : (flag2 ? "opponent" : (flag ? "friend" : string.Empty)));
		if (!num && !flag2 && !flag)
		{
			if (FiresideGatheringManager.Get() != null && FiresideGatheringManager.Get().IsPlayerInMyFSG(player))
			{
				text = "fsgpatron";
			}
			else if (BnetNearbyPlayerMgr.Get().IsNearbyPlayer(player))
			{
				text = "nearbyplayer";
			}
			else if (BnetParty.GetJoinedParties().Any((PartyInfo p) => p.Type == PartyType.SPECTATOR_PARTY && BnetParty.IsMember(p.Id, gameAccountId)))
			{
				text = "fellowspecator";
			}
		}
		string text2 = ((player == null || player.GetBattleTag() == null) ? "" : player.GetBattleTag().ToString());
		if (string.IsNullOrEmpty(text2) && update.programId == BnetProgramId.BNET && ((update.groupId == 1 && update.fieldId == 4) || (update.groupId == 2 && update.fieldId == 5)))
		{
			text2 = update.stringVal;
		}
		text = ((!string.IsNullOrEmpty(text2) || !string.IsNullOrEmpty(text)) ? $"{text2}{((string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text)) ? text : $"({text})")}" : "someone");
		BnetProgramId bnetProgramId = new BnetProgramId(update.programId);
		string text3;
		string text4;
		if (bnetProgramId == BnetProgramId.BNET)
		{
			text3 = BnetPresenceField.GetGroupName(update.groupId);
			text4 = BnetPresenceField.GetFieldName(update.groupId, update.fieldId);
		}
		else if (bnetProgramId == BnetProgramId.HEARTHSTONE)
		{
			text3 = "GameAccount";
			text4 = GamePresenceField.GetFieldName(update.fieldId);
		}
		else
		{
			text3 = update.groupId.ToString();
			text4 = update.fieldId.ToString();
		}
		string fieldValue = GetFieldValue(update);
		if (buffer == null)
		{
			buffer = new StringBuilder();
		}
		else
		{
			buffer.Append("\n");
		}
		buffer.AppendFormat("Update entity={0} who={1} {2}.{3}.{4}={5}", $"{{hi:{update.entityId.hi} lo:{update.entityId.lo}}}", text, bnetProgramId, text3, text4, fieldValue);
	}

	private static string GetFieldValue(PresenceUpdate update)
	{
		if (update.programId == BnetProgramId.HEARTHSTONE)
		{
			return GamePresenceField.GetFieldValue(update);
		}
		return BnetPresenceField.GetFieldValue(update);
	}

	private void OnBnetEventOccurred(BattleNet.BnetEvent bnetEvent, object userData)
	{
		if (bnetEvent == BattleNet.BnetEvent.Disconnected)
		{
			m_accounts.Clear();
			m_gameAccounts.Clear();
			m_players.Clear();
		}
	}

	private void OnAccountUpdate(PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetAccountId bnetAccountId = BnetAccountId.CreateFromEntityId(update.entityId);
		BnetAccount value = null;
		if (!m_accounts.TryGetValue(bnetAccountId, out value))
		{
			CreateAccount(bnetAccountId, update, changelist);
		}
		else
		{
			UpdateAccount(value, update, changelist);
		}
	}

	private void CreateAccount(BnetAccountId id, PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetAccount bnetAccount = new BnetAccount();
		m_accounts.Add(id, bnetAccount);
		bnetAccount.SetId(id);
		BnetPlayer value = null;
		if (!m_players.TryGetValue(id, out value))
		{
			value = new BnetPlayer(BnetPlayerSource.PRESENCE_UPDATE);
			m_players.Add(id, value);
			BnetPlayerChange bnetPlayerChange = new BnetPlayerChange();
			bnetPlayerChange.SetNewPlayer(value);
			changelist.AddChange(bnetPlayerChange);
		}
		value.SetAccount(bnetAccount);
		UpdateAccount(bnetAccount, update, changelist);
	}

	private void UpdateAccount(BnetAccount account, PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetPlayer player = m_players[account.GetId()];
		if (update.fieldId == 7)
		{
			bool boolVal = update.boolVal;
			if (boolVal != account.IsAway())
			{
				AddChangedPlayer(player, changelist);
				account.SetAway(boolVal);
				if (boolVal)
				{
					account.SetBusy(busy: false);
				}
			}
		}
		else if (update.fieldId == 8)
		{
			long intVal = update.intVal;
			if (intVal != account.GetAwayTimeMicrosec())
			{
				AddChangedPlayer(player, changelist);
				account.SetAwayTimeMicrosec(intVal);
			}
		}
		else if (update.fieldId == 11)
		{
			bool boolVal2 = update.boolVal;
			if (boolVal2 != account.IsBusy())
			{
				AddChangedPlayer(player, changelist);
				account.SetBusy(boolVal2);
				if (boolVal2)
				{
					account.SetAway(away: false);
				}
			}
		}
		else if (update.fieldId == 4)
		{
			BnetBattleTag bnetBattleTag = BnetBattleTag.CreateFromString(update.stringVal);
			if (bnetBattleTag == null)
			{
				Log.All.Print("Failed to parse BattleTag={0} for account={1}", update.stringVal, update.entityId.lo);
			}
			if (!(bnetBattleTag == account.GetBattleTag()))
			{
				AddChangedPlayer(player, changelist);
				account.SetBattleTag(bnetBattleTag);
			}
		}
		else if (update.fieldId == 1)
		{
			string stringVal = update.stringVal;
			if (stringVal == null)
			{
				Error.AddDevFatal("BnetPresenceMgr.UpdateAccount() - Failed to convert full name to native string for {0}.", account);
			}
			else if (!(stringVal == account.GetFullName()))
			{
				AddChangedPlayer(player, changelist);
				account.SetFullName(stringVal);
			}
		}
		else if (update.fieldId == 6)
		{
			long intVal2 = update.intVal;
			if (intVal2 != account.GetLastOnlineMicrosec())
			{
				AddChangedPlayer(player, changelist);
				account.SetLastOnlineMicrosec(intVal2);
			}
		}
		else if (update.fieldId != 3 && update.fieldId == 12)
		{
			bool boolVal3 = update.boolVal;
			if (boolVal3 != account.IsAppearingOffline())
			{
				AddChangedPlayer(player, changelist);
				account.SetAppearingOffline(boolVal3);
			}
		}
	}

	private void OnGameAccountUpdate(PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetGameAccountId bnetGameAccountId = BnetGameAccountId.CreateFromEntityId(update.entityId);
		BnetGameAccount value = null;
		if (!m_gameAccounts.TryGetValue(bnetGameAccountId, out value))
		{
			CreateGameAccount(bnetGameAccountId, update, changelist);
		}
		else
		{
			UpdateGameAccount(value, update, changelist);
		}
	}

	private void CreateGameAccount(BnetGameAccountId id, PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetGameAccount bnetGameAccount = new BnetGameAccount();
		m_gameAccounts.Add(id, bnetGameAccount);
		bnetGameAccount.SetId(id);
		UpdateGameAccount(bnetGameAccount, update, changelist);
	}

	private void UpdateGameAccount(BnetGameAccount gameAccount, PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetPlayer value = null;
		BnetAccountId ownerId = gameAccount.GetOwnerId();
		if (ownerId != null)
		{
			m_players.TryGetValue(ownerId, out value);
		}
		if (update.fieldId == 2)
		{
			int num = (gameAccount.IsBusy() ? 1 : 0);
			int num2 = (int)update.intVal;
			if (num2 != num)
			{
				AddChangedPlayer(value, changelist);
				bool flag = num2 == 1;
				gameAccount.SetBusy(flag);
				if (flag)
				{
					gameAccount.SetAway(away: false);
				}
				HandleGameAccountChange(value, update);
			}
		}
		else if (update.fieldId == 10)
		{
			bool boolVal = update.boolVal;
			if (boolVal != gameAccount.IsAway())
			{
				AddChangedPlayer(value, changelist);
				gameAccount.SetAway(boolVal);
				if (boolVal)
				{
					gameAccount.SetBusy(busy: false);
				}
				HandleGameAccountChange(value, update);
			}
		}
		else if (update.fieldId == 11)
		{
			long intVal = update.intVal;
			if (intVal != gameAccount.GetAwayTimeMicrosec())
			{
				AddChangedPlayer(value, changelist);
				gameAccount.SetAwayTimeMicrosec(intVal);
				HandleGameAccountChange(value, update);
			}
		}
		else if (update.fieldId == 5)
		{
			BnetBattleTag bnetBattleTag = BnetBattleTag.CreateFromString(update.stringVal);
			if (bnetBattleTag == null)
			{
				Log.All.Print("Failed to parse BattleTag={0} for gameAccount={1}", update.stringVal, update.entityId.lo);
			}
			if (!(bnetBattleTag == gameAccount.GetBattleTag()))
			{
				AddChangedPlayer(value, changelist);
				gameAccount.SetBattleTag(bnetBattleTag);
				HandleGameAccountChange(value, update);
			}
		}
		else if (update.fieldId == 1)
		{
			bool boolVal2 = update.boolVal;
			if (boolVal2 != gameAccount.IsOnline())
			{
				AddChangedPlayer(value, changelist);
				gameAccount.SetOnline(boolVal2);
				HandleGameAccountChange(value, update);
			}
		}
		else if (update.fieldId == 3)
		{
			BnetProgramId bnetProgramId = new BnetProgramId(update.stringVal);
			if (!(bnetProgramId == gameAccount.GetProgramId()))
			{
				AddChangedPlayer(value, changelist);
				gameAccount.SetProgramId(bnetProgramId);
				HandleGameAccountChange(value, update);
			}
		}
		else if (update.fieldId == 4)
		{
			long intVal2 = update.intVal;
			if (intVal2 != gameAccount.GetLastOnlineMicrosec())
			{
				AddChangedPlayer(value, changelist);
				gameAccount.SetLastOnlineMicrosec(intVal2);
				HandleGameAccountChange(value, update);
			}
		}
		else if (update.fieldId == 7)
		{
			BnetAccountId bnetAccountId = BnetAccountId.CreateFromEntityId(update.entityIdVal);
			if (!(bnetAccountId == gameAccount.GetOwnerId()))
			{
				UpdateGameAccountOwner(bnetAccountId, gameAccount, changelist);
			}
		}
		else if (update.fieldId == 9)
		{
			if (update.valCleared && gameAccount.GetRichPresence() != null)
			{
				AddChangedPlayer(value, changelist);
				gameAccount.SetRichPresence(null);
				HandleGameAccountChange(value, update);
			}
		}
		else if (update.fieldId == 1000)
		{
			string text = update.stringVal;
			if (text == null)
			{
				text = "";
			}
			if (!(text == gameAccount.GetRichPresence()))
			{
				AddChangedPlayer(value, changelist);
				gameAccount.SetRichPresence(text);
				HandleGameAccountChange(value, update);
			}
		}
	}

	private void UpdateGameAccountOwner(BnetAccountId ownerId, BnetGameAccount gameAccount, BnetPlayerChangelist changelist)
	{
		BnetPlayer value = null;
		BnetAccountId ownerId2 = gameAccount.GetOwnerId();
		if (ownerId2 != null && m_players.TryGetValue(ownerId2, out value))
		{
			value.RemoveGameAccount(gameAccount.GetId());
			AddChangedPlayer(value, changelist);
		}
		BnetPlayer value2 = null;
		if (m_players.TryGetValue(ownerId, out value2))
		{
			AddChangedPlayer(value2, changelist);
		}
		else
		{
			value2 = new BnetPlayer(BnetPlayerSource.PRESENCE_UPDATE);
			m_players.Add(ownerId, value2);
			BnetPlayerChange bnetPlayerChange = new BnetPlayerChange();
			bnetPlayerChange.SetNewPlayer(value2);
			changelist.AddChange(bnetPlayerChange);
		}
		gameAccount.SetOwnerId(ownerId);
		value2.AddGameAccount(gameAccount);
		CacheMyself(gameAccount, value2);
	}

	private void HandleGameAccountChange(BnetPlayer player, PresenceUpdate update)
	{
		player?.OnGameAccountChanged(update.fieldId);
	}

	private void OnGameUpdate(PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetGameAccountId bnetGameAccountId = BnetGameAccountId.CreateFromEntityId(update.entityId);
		BnetGameAccount value = null;
		if (!m_gameAccounts.TryGetValue(bnetGameAccountId, out value))
		{
			CreateGameInfo(bnetGameAccountId, update, changelist);
		}
		else
		{
			UpdateGameInfo(value, update, changelist);
		}
	}

	private void CreateGameInfo(BnetGameAccountId id, PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetGameAccount bnetGameAccount = new BnetGameAccount();
		m_gameAccounts.Add(id, bnetGameAccount);
		bnetGameAccount.SetId(id);
		UpdateGameInfo(bnetGameAccount, update, changelist);
	}

	private void UpdateGameInfo(BnetGameAccount gameAccount, PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetPlayer value = null;
		BnetAccountId ownerId = gameAccount.GetOwnerId();
		if (ownerId != null)
		{
			m_players.TryGetValue(ownerId, out value);
		}
		if (update.valCleared)
		{
			if (gameAccount.HasGameField(update.fieldId))
			{
				AddChangedPlayer(value, changelist);
				gameAccount.RemoveGameField(update.fieldId);
				HandleGameAccountChange(value, update);
			}
			return;
		}
		switch (update.fieldId)
		{
		case 1u:
			if (update.boolVal != gameAccount.GetGameFieldBool(update.fieldId))
			{
				AddChangedPlayer(value, changelist);
				gameAccount.SetGameField(update.fieldId, update.boolVal);
				HandleGameAccountChange(value, update);
			}
			break;
		case 5u:
		case 6u:
		case 7u:
		case 8u:
		case 9u:
		case 10u:
		case 11u:
		case 12u:
		case 13u:
		case 14u:
		case 15u:
		case 16u:
			if ((int)update.intVal != gameAccount.GetGameFieldInt(update.fieldId))
			{
				AddChangedPlayer(value, changelist);
				gameAccount.SetGameField(update.fieldId, (int)update.intVal);
				HandleGameAccountChange(value, update);
			}
			break;
		case 2u:
		case 4u:
		case 19u:
		case 20u:
			if (!(update.stringVal == gameAccount.GetGameFieldString(update.fieldId)))
			{
				AddChangedPlayer(value, changelist);
				gameAccount.SetGameField(update.fieldId, update.stringVal);
				HandleGameAccountChange(value, update);
			}
			break;
		case 17u:
		case 18u:
		case 21u:
		case 22u:
		case 23u:
		case 24u:
		case 25u:
			if (!GeneralUtils.AreBytesEqual(update.blobVal, gameAccount.GetGameFieldBytes(update.fieldId)))
			{
				AddChangedPlayer(value, changelist);
				gameAccount.SetGameField(update.fieldId, update.blobVal);
				HandleGameAccountChange(value, update);
			}
			break;
		case 26u:
			if (!(update.entityIdVal == gameAccount.GetGameFieldEntityId(update.fieldId)))
			{
				AddChangedPlayer(value, changelist);
				gameAccount.SetGameField(update.fieldId, update.entityIdVal);
				HandleGameAccountChange(value, update);
			}
			break;
		default:
			Log.Presence.PrintWarning("Unknown HS game account fieldId={0} - not saved into presence cache.", update.fieldId);
			break;
		}
	}

	private void CacheMyself(BnetGameAccount gameAccount, BnetPlayer player)
	{
		if (player != m_myPlayer && !(gameAccount.GetId() != m_myGameAccountId))
		{
			m_myPlayer = player;
		}
	}

	private void AddChangedPlayer(BnetPlayer player, BnetPlayerChangelist changelist)
	{
		if (player != null && !changelist.HasChange(player))
		{
			BnetPlayerChange bnetPlayerChange = new BnetPlayerChange();
			bnetPlayerChange.SetOldPlayer(player.Clone());
			bnetPlayerChange.SetNewPlayer(player);
			changelist.AddChange(bnetPlayerChange);
		}
	}

	private void FirePlayersChangedEvent(BnetPlayerChangelist changelist)
	{
		if (changelist != null && changelist.GetChanges().Count != 0)
		{
			PlayersChangedListener[] array = m_playersChangedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire(changelist);
			}
		}
	}

	private bool ShouldUpdateGameField(uint fieldId, object val, out BnetGameAccount hsGameAccount)
	{
		hsGameAccount = null;
		if (m_myPlayer == null)
		{
			return true;
		}
		hsGameAccount = m_myPlayer.GetHearthstoneGameAccount();
		if (hsGameAccount == null)
		{
			return true;
		}
		if (hsGameAccount.HasGameField(fieldId))
		{
			object gameField = hsGameAccount.GetGameField(fieldId);
			if (val == null)
			{
				if (gameField == null)
				{
					return false;
				}
			}
			else if (val.Equals(gameField))
			{
				return false;
			}
		}
		else if (val == null)
		{
			return false;
		}
		return true;
	}

	private bool ShouldUpdateGameFieldBlob(uint fieldId, byte[] val, out BnetGameAccount hsGameAccount)
	{
		hsGameAccount = null;
		if (m_myPlayer == null)
		{
			return true;
		}
		hsGameAccount = m_myPlayer.GetHearthstoneGameAccount();
		if (hsGameAccount == null)
		{
			return true;
		}
		if (hsGameAccount.HasGameField(fieldId))
		{
			byte[] gameFieldBytes = hsGameAccount.GetGameFieldBytes(fieldId);
			if (GeneralUtils.AreArraysEqual(val, gameFieldBytes))
			{
				return false;
			}
		}
		else if (val == null)
		{
			return false;
		}
		return true;
	}

	private BnetPlayerChangelist ChangeGameField(BnetGameAccount hsGameAccount, uint fieldId, object val)
	{
		if (hsGameAccount == null)
		{
			return null;
		}
		BnetPlayerChange bnetPlayerChange = new BnetPlayerChange();
		bnetPlayerChange.SetOldPlayer(m_myPlayer.Clone());
		bnetPlayerChange.SetNewPlayer(m_myPlayer);
		hsGameAccount.SetGameField(fieldId, val);
		BnetPlayerChangelist bnetPlayerChangelist = new BnetPlayerChangelist();
		bnetPlayerChangelist.AddChange(bnetPlayerChange);
		return bnetPlayerChangelist;
	}

	public void Cheat_SetMyPlayer(BnetPlayer player)
	{
		m_myPlayer = player;
	}

	public bool Cheat_AddAccount(BnetAccount account)
	{
		BnetAccountId id = account.GetId();
		if (m_accounts.ContainsKey(id))
		{
			Error.AddDevFatal("BnetPresenceMgr.AddAccount() - {0} is already being tracked.", account);
			return false;
		}
		m_accounts.Add(id, account);
		return true;
	}

	public bool Cheat_AddGameAccount(BnetGameAccount gameAccount)
	{
		BnetGameAccountId id = gameAccount.GetId();
		if (m_gameAccounts.ContainsKey(id))
		{
			Error.AddDevFatal("BnetPresenceMgr.AddGameAccount() - {0} is already being tracked.", gameAccount);
			return false;
		}
		m_gameAccounts.Add(id, gameAccount);
		return true;
	}

	public bool Cheat_AddPlayer(BnetPlayer player)
	{
		BnetAccountId accountId = player.GetAccountId();
		if (m_players.ContainsKey(accountId))
		{
			Error.AddDevFatal("BnetPresenceMgr.AddPlayer() - {0} is already being tracked.", player);
			return false;
		}
		m_players.Add(accountId, player);
		return true;
	}
}
