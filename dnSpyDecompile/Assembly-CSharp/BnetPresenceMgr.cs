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

// Token: 0x02000772 RID: 1906
public class BnetPresenceMgr
{
	// Token: 0x14000073 RID: 115
	// (add) Token: 0x06006B91 RID: 27537 RVA: 0x0022D604 File Offset: 0x0022B804
	// (remove) Token: 0x06006B92 RID: 27538 RVA: 0x0022D63C File Offset: 0x0022B83C
	public event Action<PresenceUpdate[]> OnGameAccountPresenceChange;

	// Token: 0x06006B93 RID: 27539 RVA: 0x0022D674 File Offset: 0x0022B874
	public static BnetPresenceMgr Get()
	{
		if (BnetPresenceMgr.s_instance == null)
		{
			BnetPresenceMgr.s_instance = new BnetPresenceMgr();
			HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
			if (hearthstoneApplication != null)
			{
				hearthstoneApplication.WillReset += delegate()
				{
					BnetPresenceMgr bnetPresenceMgr = BnetPresenceMgr.s_instance;
					BnetPresenceMgr.s_instance = new BnetPresenceMgr();
					BnetPresenceMgr.s_instance.m_playersChangedListeners = bnetPresenceMgr.m_playersChangedListeners;
					BnetPresenceMgr.s_instance.OnGameAccountPresenceChange = bnetPresenceMgr.OnGameAccountPresenceChange;
				};
			}
			else
			{
				global::Log.BattleNet.PrintWarning("BnetPresenceMgr.Get(): HearthstoneApplication.Get() returned null. Unable to subscribe to HearthstoneApplication.WillReset.", Array.Empty<object>());
			}
		}
		return BnetPresenceMgr.s_instance;
	}

	// Token: 0x06006B94 RID: 27540 RVA: 0x0022D6E4 File Offset: 0x0022B8E4
	public void Initialize()
	{
		Network.Get().SetPresenceHandler(new Network.PresenceHandler(this.OnPresenceUpdate));
		BnetEventMgr.Get().AddChangeListener(new BnetEventMgr.ChangeCallback(this.OnBnetEventOccurred));
		EntityId myGameAccountId = BattleNet.GetMyGameAccountId();
		this.m_myGameAccountId = BnetGameAccountId.CreateFromEntityId(myGameAccountId);
		EntityId myAccoundId = BattleNet.GetMyAccoundId();
		this.m_myBattleNetAccountId = BnetAccountId.CreateFromEntityId(myAccoundId);
	}

	// Token: 0x06006B95 RID: 27541 RVA: 0x0022D742 File Offset: 0x0022B942
	public void Shutdown()
	{
		Network.Get().SetPresenceHandler(null);
	}

	// Token: 0x06006B96 RID: 27542 RVA: 0x0022D74F File Offset: 0x0022B94F
	public BnetGameAccountId GetMyGameAccountId()
	{
		return this.m_myGameAccountId;
	}

	// Token: 0x06006B97 RID: 27543 RVA: 0x0022D757 File Offset: 0x0022B957
	public BnetId GetMyGameAccountIdAsBnetId()
	{
		return BnetUtils.CreatePegasusBnetId(this.m_myGameAccountId);
	}

	// Token: 0x06006B98 RID: 27544 RVA: 0x0022D764 File Offset: 0x0022B964
	public BnetId GetMyBattleNetAccountIdAsBnetId()
	{
		return BnetUtils.CreatePegasusBnetId(this.m_myBattleNetAccountId);
	}

	// Token: 0x06006B99 RID: 27545 RVA: 0x0022D771 File Offset: 0x0022B971
	public BnetPlayer GetMyPlayer()
	{
		return this.m_myPlayer;
	}

	// Token: 0x06006B9A RID: 27546 RVA: 0x0022D77C File Offset: 0x0022B97C
	public BnetAccount GetAccount(BnetAccountId id)
	{
		if (id == null)
		{
			return null;
		}
		BnetAccount result = null;
		this.m_accounts.TryGetValue(id, out result);
		return result;
	}

	// Token: 0x06006B9B RID: 27547 RVA: 0x0022D7A8 File Offset: 0x0022B9A8
	public BnetGameAccount GetGameAccount(BnetGameAccountId id)
	{
		if (id == null)
		{
			return null;
		}
		BnetGameAccount result = null;
		this.m_gameAccounts.TryGetValue(id, out result);
		return result;
	}

	// Token: 0x06006B9C RID: 27548 RVA: 0x0022D7D4 File Offset: 0x0022B9D4
	public BnetPlayer GetPlayer(BnetAccountId id)
	{
		if (id == null)
		{
			return null;
		}
		BnetPlayer result = null;
		this.m_players.TryGetValue(id, out result);
		return result;
	}

	// Token: 0x06006B9D RID: 27549 RVA: 0x0022D800 File Offset: 0x0022BA00
	public BnetPlayer GetPlayer(BnetGameAccountId id)
	{
		BnetGameAccount gameAccount = this.GetGameAccount(id);
		if (gameAccount == null)
		{
			return null;
		}
		return this.GetPlayer(gameAccount.GetOwnerId());
	}

	// Token: 0x06006B9E RID: 27550 RVA: 0x0022D82C File Offset: 0x0022BA2C
	public BnetPlayer RegisterPlayer(BnetPlayerSource source, BnetAccountId accountId, BnetGameAccountId gameAccountId = null, BnetProgramId programId = null)
	{
		BnetPlayer bnetPlayer = this.GetPlayer(accountId);
		if (bnetPlayer != null)
		{
			return bnetPlayer;
		}
		bnetPlayer = new BnetPlayer(source);
		bnetPlayer.SetAccountId(accountId);
		this.m_players[accountId] = bnetPlayer;
		BnetAccount bnetAccount = new BnetAccount();
		this.m_accounts.Add(accountId, bnetAccount);
		bnetAccount.SetId(accountId);
		bnetPlayer.SetAccount(bnetAccount);
		if (gameAccountId != null)
		{
			BnetGameAccount bnetGameAccount;
			if (!this.m_gameAccounts.TryGetValue(gameAccountId, out bnetGameAccount))
			{
				bnetGameAccount = new BnetGameAccount();
				bnetGameAccount.SetId(gameAccountId);
				bnetGameAccount.SetOwnerId(accountId);
				this.m_gameAccounts.Add(gameAccountId, bnetGameAccount);
				if (programId != null)
				{
					bnetGameAccount.SetProgramId(programId);
				}
			}
			bnetPlayer.AddGameAccount(bnetGameAccount);
		}
		BnetPlayerChange bnetPlayerChange = new BnetPlayerChange();
		bnetPlayerChange.SetNewPlayer(bnetPlayer);
		BnetPlayerChangelist bnetPlayerChangelist = new BnetPlayerChangelist();
		bnetPlayerChangelist.AddChange(bnetPlayerChange);
		this.FirePlayersChangedEvent(bnetPlayerChangelist);
		return bnetPlayer;
	}

	// Token: 0x06006B9F RID: 27551 RVA: 0x0022D900 File Offset: 0x0022BB00
	public void RegisterBnetPlayer(BnetPlayer player)
	{
		if (player == null || player.GetAccount() == null || player.GetAccountId() == null)
		{
			return;
		}
		bool flag = false;
		BnetAccountId accountId = player.GetAccountId();
		BnetPlayer bnetPlayer;
		if (this.m_players.TryGetValue(accountId, out bnetPlayer))
		{
			if (bnetPlayer != player)
			{
				flag = true;
				global::Log.All.PrintWarning("Already registered BnetPlayer accountId={0} newSrc={1} - will overwrite.", new object[]
				{
					accountId.GetLo(),
					player.Source
				});
			}
		}
		else
		{
			flag = true;
		}
		this.m_players[accountId] = player;
		BnetAccount bnetAccount;
		if (this.m_accounts.TryGetValue(accountId, out bnetAccount))
		{
			if (bnetAccount != player.GetAccount())
			{
				flag = true;
				global::Log.All.PrintWarning("Already registered BnetAccount accountId={0} newSrc={1} - will overwrite.", new object[]
				{
					accountId.GetLo(),
					player.Source
				});
			}
		}
		else
		{
			flag = true;
		}
		this.m_accounts[accountId] = player.GetAccount();
		foreach (KeyValuePair<BnetGameAccountId, BnetGameAccount> keyValuePair in player.GetGameAccounts())
		{
			BnetGameAccountId key = keyValuePair.Key;
			BnetGameAccount value = keyValuePair.Value;
			BnetGameAccount bnetGameAccount;
			if (this.m_gameAccounts.TryGetValue(key, out bnetGameAccount))
			{
				if (bnetGameAccount != value)
				{
					flag = true;
					global::Log.All.PrintWarning("Already registered BnetAccount accountId={0} newSrc={1} - will overwrite.", new object[]
					{
						accountId.GetLo(),
						player.Source
					});
				}
			}
			else
			{
				flag = true;
			}
			this.m_gameAccounts[key] = value;
		}
		if (flag)
		{
			BnetPlayerChange bnetPlayerChange = new BnetPlayerChange();
			bnetPlayerChange.SetNewPlayer(player);
			BnetPlayerChangelist bnetPlayerChangelist = new BnetPlayerChangelist();
			bnetPlayerChangelist.AddChange(bnetPlayerChange);
			this.FirePlayersChangedEvent(bnetPlayerChangelist);
		}
	}

	// Token: 0x06006BA0 RID: 27552 RVA: 0x0022DAD0 File Offset: 0x0022BCD0
	public bool IsSubscribedToPlayer(BnetGameAccountId id)
	{
		return BattleNet.IsSubscribedToEntity(new EntityId(BnetEntityId.CreateForProtocol(id)));
	}

	// Token: 0x06006BA1 RID: 27553 RVA: 0x0022DAE4 File Offset: 0x0022BCE4
	public void CheckSubscriptionsAndClearTransientStatus(BnetAccountId accountId)
	{
		BnetPlayer bnetPlayer;
		if (!this.m_players.TryGetValue(accountId, out bnetPlayer))
		{
			return;
		}
		foreach (KeyValuePair<BnetGameAccountId, BnetGameAccount> keyValuePair in bnetPlayer.GetGameAccounts())
		{
			this.CheckSubscriptionsAndClearTransientStatus_Internal(keyValuePair.Value);
		}
	}

	// Token: 0x06006BA2 RID: 27554 RVA: 0x0022DB50 File Offset: 0x0022BD50
	public void CheckSubscriptionsAndClearTransientStatus(BnetGameAccountId gameAccountId)
	{
		BnetGameAccount gameAccount;
		if (!this.m_gameAccounts.TryGetValue(gameAccountId, out gameAccount))
		{
			return;
		}
		this.CheckSubscriptionsAndClearTransientStatus_Internal(gameAccount);
	}

	// Token: 0x06006BA3 RID: 27555 RVA: 0x0022DB78 File Offset: 0x0022BD78
	private void CheckSubscriptionsAndClearTransientStatus_Internal(BnetGameAccount gameAccount)
	{
		BnetGameAccountId id = gameAccount.GetId();
		if (this.IsSubscribedToPlayer(id))
		{
			return;
		}
		foreach (uint fieldId in GamePresenceField.TransientStatusFields)
		{
			gameAccount.SetGameField(fieldId, null);
		}
		gameAccount.SetOnline(BnetNearbyPlayerMgr.Get().IsNearbyPlayer(gameAccount.GetId()));
		gameAccount.SetBusy(false);
		gameAccount.SetAway(false);
		gameAccount.SetAwayTimeMicrosec(0L);
		gameAccount.SetRichPresence(null);
	}

	// Token: 0x06006BA4 RID: 27556 RVA: 0x0022DBEC File Offset: 0x0022BDEC
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
				groupId = 1U,
				fieldId = 4U,
				uniqueId = 0UL
			}
		}.ToArray();
		BattleNet.RequestPresenceFields(false, entityId, fieldList);
		global::Log.Presence.Print("Requesting BattleTag for player {0}!", new object[]
		{
			id
		});
	}

	// Token: 0x06006BA5 RID: 27557 RVA: 0x0022DC80 File Offset: 0x0022BE80
	public bool SetGameField(uint fieldId, bool val)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			global::Error.AddDevFatal("Caller should check for Battle.net connection before calling SetGameField {0}={1}", new object[]
			{
				fieldId,
				val
			});
			return false;
		}
		BnetGameAccount bnetGameAccount;
		if (!this.ShouldUpdateGameField(fieldId, val, out bnetGameAccount))
		{
			return false;
		}
		if (fieldId == 2U)
		{
			bnetGameAccount.SetBusy(val);
			int num = val ? 1 : 0;
			BattleNet.SetPresenceInt(fieldId, (long)num);
		}
		else
		{
			BattleNet.SetPresenceBool(fieldId, val);
		}
		BnetPlayerChangelist changelist = this.ChangeGameField(bnetGameAccount, fieldId, val);
		if (fieldId != 2U)
		{
			if (fieldId == 10U)
			{
				if (val)
				{
					bnetGameAccount.SetBusy(false);
				}
			}
		}
		else if (val)
		{
			bnetGameAccount.SetAway(false);
		}
		this.FirePlayersChangedEvent(changelist);
		return true;
	}

	// Token: 0x06006BA6 RID: 27558 RVA: 0x0022DD28 File Offset: 0x0022BF28
	public bool SetAccountField(uint fieldId, bool val)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			global::Error.AddDevFatal("Caller should check for Battle.net connection before calling SetGameField {0}={1}", new object[]
			{
				fieldId,
				val
			});
			return false;
		}
		BattleNet.SetAccountLevelPresenceBool(fieldId, val);
		PresenceUpdate presenceUpdate = new PresenceUpdate
		{
			entityId = BattleNet.GetMyAccoundId(),
			programId = BnetProgramId.BNET.GetValue(),
			groupId = 1U,
			fieldId = fieldId,
			boolVal = val
		};
		this.OnPresenceUpdate(new PresenceUpdate[]
		{
			presenceUpdate
		});
		return true;
	}

	// Token: 0x06006BA7 RID: 27559 RVA: 0x0022DDBC File Offset: 0x0022BFBC
	public bool SetGameField(uint fieldId, int val)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			global::Error.AddDevFatal("Caller should check for Battle.net connection before calling SetGameField {0}={1}", new object[]
			{
				fieldId,
				val
			});
			return false;
		}
		BnetGameAccount hsGameAccount;
		if (!this.ShouldUpdateGameField(fieldId, val, out hsGameAccount))
		{
			return false;
		}
		BattleNet.SetPresenceInt(fieldId, (long)val);
		BnetPlayerChangelist changelist = this.ChangeGameField(hsGameAccount, fieldId, val);
		this.FirePlayersChangedEvent(changelist);
		return true;
	}

	// Token: 0x06006BA8 RID: 27560 RVA: 0x0022DE28 File Offset: 0x0022C028
	public bool SetGameField(uint fieldId, string val)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			global::Error.AddDevFatal("Caller should check for Battle.net connection before calling SetGameField {0}={1}", new object[]
			{
				fieldId,
				val
			});
			return false;
		}
		BnetGameAccount hsGameAccount;
		if (!this.ShouldUpdateGameField(fieldId, val, out hsGameAccount))
		{
			return false;
		}
		BattleNet.SetPresenceString(fieldId, val);
		BnetPlayerChangelist changelist = this.ChangeGameField(hsGameAccount, fieldId, val);
		this.FirePlayersChangedEvent(changelist);
		return true;
	}

	// Token: 0x06006BA9 RID: 27561 RVA: 0x0022DE84 File Offset: 0x0022C084
	public bool SetGameField(uint fieldId, byte[] val)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			global::Error.AddDevFatal("Caller should check for Battle.net connection before calling SetGameField {0}=[{1}]", new object[]
			{
				fieldId,
				(val == null) ? "" : val.Length.ToString()
			});
			return false;
		}
		BnetGameAccount hsGameAccount;
		if (!this.ShouldUpdateGameFieldBlob(fieldId, val, out hsGameAccount))
		{
			return false;
		}
		BattleNet.SetPresenceBlob(fieldId, val);
		BnetPlayerChangelist changelist = this.ChangeGameField(hsGameAccount, fieldId, val);
		this.FirePlayersChangedEvent(changelist);
		return true;
	}

	// Token: 0x06006BAA RID: 27562 RVA: 0x0022DEF4 File Offset: 0x0022C0F4
	public bool SetGameFieldBlob(uint fieldId, IProtoBuf protoMessage)
	{
		if (fieldId == 21U || fieldId == 23U)
		{
			this.SetPresenceSpectatorJoinInfo(protoMessage as JoinInfo);
			return true;
		}
		byte[] val = (protoMessage == null) ? null : ProtobufUtil.ToByteArray(protoMessage);
		return this.SetGameField(fieldId, val);
	}

	// Token: 0x06006BAB RID: 27563 RVA: 0x0022DF30 File Offset: 0x0022C130
	public bool SetGameField(uint fieldId, EntityId val)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			global::Error.AddDevFatal("Caller should check for Battle.net connection before calling SetGameField {0}=[{1}]", new object[]
			{
				fieldId,
				val.ToString()
			});
			return false;
		}
		BnetGameAccount hsGameAccount;
		if (!this.ShouldUpdateGameField(fieldId, val, out hsGameAccount))
		{
			return false;
		}
		BattleNet.SetPresenceEntityId(fieldId, val);
		BnetPlayerChangelist changelist = this.ChangeGameField(hsGameAccount, fieldId, val);
		this.FirePlayersChangedEvent(changelist);
		return true;
	}

	// Token: 0x06006BAC RID: 27564 RVA: 0x0022DFA0 File Offset: 0x0022C1A0
	public void SetPresenceSpectatorJoinInfo(JoinInfo joinInfo)
	{
		byte[] array = (joinInfo == null) ? null : ProtobufUtil.ToByteArray(joinInfo);
		this.SetGameField(21U, array);
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
		this.SetGameField(23U, val);
	}

	// Token: 0x06006BAD RID: 27565 RVA: 0x0022E038 File Offset: 0x0022C238
	public bool SetRichPresence(Enum[] richPresence)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			string message = "Caller should check for Battle.net connection before calling SetRichPresence {0}";
			object[] array = new object[1];
			int num = 0;
			object obj;
			if (richPresence != null)
			{
				obj = string.Join(", ", (from x in richPresence
				select x.ToString()).ToArray<string>());
			}
			else
			{
				obj = "";
			}
			array[num] = obj;
			global::Error.AddDevFatal(message, array);
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
		RichPresenceUpdate[] array2 = new RichPresenceUpdate[richPresence.Length];
		for (int i = 0; i < richPresence.Length; i++)
		{
			Enum @enum = richPresence[i];
			Type type = @enum.GetType();
			global::FourCC fourCC = RichPresence.s_streamIds[type];
			array2[i] = new RichPresenceUpdate
			{
				presenceFieldIndex = (ulong)((i == 0) ? 0 : (458752 + i)),
				programId = BnetProgramId.HEARTHSTONE.GetValue(),
				streamId = fourCC.GetValue(),
				index = Convert.ToUInt32(@enum)
			};
		}
		BattleNet.SetRichPresence(array2);
		return true;
	}

	// Token: 0x06006BAE RID: 27566 RVA: 0x0022E132 File Offset: 0x0022C332
	public void SetDeckValidity(DeckValidity deckValidity)
	{
		this.SetGameFieldBlob(24U, deckValidity);
	}

	// Token: 0x06006BAF RID: 27567 RVA: 0x0022E13E File Offset: 0x0022C33E
	public bool AddPlayersChangedListener(BnetPresenceMgr.PlayersChangedCallback callback)
	{
		return this.AddPlayersChangedListener(callback, null);
	}

	// Token: 0x06006BB0 RID: 27568 RVA: 0x0022E148 File Offset: 0x0022C348
	public bool AddPlayersChangedListener(BnetPresenceMgr.PlayersChangedCallback callback, object userData)
	{
		BnetPresenceMgr.PlayersChangedListener playersChangedListener = new BnetPresenceMgr.PlayersChangedListener();
		playersChangedListener.SetCallback(callback);
		playersChangedListener.SetUserData(userData);
		if (this.m_playersChangedListeners.Contains(playersChangedListener))
		{
			return false;
		}
		this.m_playersChangedListeners.Add(playersChangedListener);
		return true;
	}

	// Token: 0x06006BB1 RID: 27569 RVA: 0x0022E186 File Offset: 0x0022C386
	public bool RemovePlayersChangedListener(BnetPresenceMgr.PlayersChangedCallback callback)
	{
		return this.RemovePlayersChangedListener(callback, null);
	}

	// Token: 0x06006BB2 RID: 27570 RVA: 0x0022E190 File Offset: 0x0022C390
	private bool RemovePlayersChangedListener(BnetPresenceMgr.PlayersChangedCallback callback, object userData)
	{
		BnetPresenceMgr.PlayersChangedListener playersChangedListener = new BnetPresenceMgr.PlayersChangedListener();
		playersChangedListener.SetCallback(callback);
		playersChangedListener.SetUserData(userData);
		return this.m_playersChangedListeners.Remove(playersChangedListener);
	}

	// Token: 0x06006BB3 RID: 27571 RVA: 0x0022E1BD File Offset: 0x0022C3BD
	public static bool RemovePlayersChangedListenerFromInstance(BnetPresenceMgr.PlayersChangedCallback callback, object userData = null)
	{
		return BnetPresenceMgr.s_instance != null && BnetPresenceMgr.s_instance.RemovePlayersChangedListener(callback, userData);
	}

	// Token: 0x06006BB4 RID: 27572 RVA: 0x0022E1D4 File Offset: 0x0022C3D4
	private void OnPresenceUpdate(PresenceUpdate[] updates)
	{
		BnetPlayerChangelist changelist = new BnetPlayerChangelist();
		foreach (PresenceUpdate presenceUpdate in from u in updates
		where u.programId == BnetProgramId.BNET && u.groupId == 2U && u.fieldId == 7U
		select u)
		{
			BnetGameAccountId bnetGameAccountId = BnetGameAccountId.CreateFromEntityId(presenceUpdate.entityId);
			BnetAccountId bnetAccountId = BnetAccountId.CreateFromEntityId(presenceUpdate.entityIdVal);
			if (!bnetAccountId.IsEmpty())
			{
				if (this.GetAccount(bnetAccountId) == null)
				{
					PresenceUpdate update = default(PresenceUpdate);
					BnetPlayerChangelist changelist2 = new BnetPlayerChangelist();
					this.CreateAccount(bnetAccountId, update, changelist2);
				}
				if (!bnetGameAccountId.IsEmpty() && this.GetGameAccount(bnetGameAccountId) == null)
				{
					this.CreateGameAccount(bnetGameAccountId, presenceUpdate, changelist);
				}
			}
		}
		List<PresenceUpdate> list = null;
		foreach (PresenceUpdate presenceUpdate2 in updates)
		{
			if (presenceUpdate2.programId == BnetProgramId.BNET)
			{
				if (presenceUpdate2.groupId == 1U)
				{
					this.OnAccountUpdate(presenceUpdate2, changelist);
				}
				else if (presenceUpdate2.groupId == 2U)
				{
					this.OnGameAccountUpdate(presenceUpdate2, changelist);
				}
			}
			else if (presenceUpdate2.programId == BnetProgramId.HEARTHSTONE)
			{
				this.OnGameUpdate(presenceUpdate2, changelist);
			}
			if ((presenceUpdate2.programId == BnetProgramId.HEARTHSTONE || (presenceUpdate2.programId == BnetProgramId.BNET && presenceUpdate2.groupId == 2U)) && this.OnGameAccountPresenceChange != null)
			{
				if (list == null)
				{
					list = new List<PresenceUpdate>();
				}
				list.Add(presenceUpdate2);
			}
		}
		BnetPresenceMgr.LogPresenceUpdates(updates);
		if (list != null)
		{
			this.OnGameAccountPresenceChange(list.ToArray());
		}
		this.FirePlayersChangedEvent(changelist);
	}

	// Token: 0x06006BB5 RID: 27573 RVA: 0x0022E39C File Offset: 0x0022C59C
	private static void LogPresenceUpdates(PresenceUpdate[] updates)
	{
		global::Log.LogLevel level = global::Log.LogLevel.Debug;
		bool flag = true;
		StringBuilder stringBuilder = null;
		foreach (PresenceUpdate update in updates)
		{
			BnetPresenceMgr.LogPresenceUpdate(ref stringBuilder, level, flag, update);
		}
		if (stringBuilder != null)
		{
			global::Log.Presence.Print(level, flag, stringBuilder.ToString());
		}
	}

	// Token: 0x06006BB6 RID: 27574 RVA: 0x0022E3F0 File Offset: 0x0022C5F0
	private static void LogPresenceUpdate(ref StringBuilder buffer, global::Log.LogLevel level, bool verbosity, PresenceUpdate update)
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		if (!global::Log.Presence.CanPrint(level, new bool?(verbosity)))
		{
			return;
		}
		BnetAccountId id = BnetAccountId.CreateFromEntityId(update.entityId);
		BnetGameAccountId gameAccountId = BnetGameAccountId.CreateFromEntityId(update.entityId);
		object obj = update.entityId == BattleNet.GetMyAccoundId() || update.entityId == BattleNet.GetMyGameAccountId();
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(gameAccountId);
		if (player == null)
		{
			player = BnetPresenceMgr.Get().GetPlayer(id);
		}
		object obj2 = obj;
		bool flag = obj2 == null && BnetFriendMgr.Get().IsFriend(player);
		bool flag2 = obj2 == null && GameState.Get() != null && (GameMgr.Get() == null || !GameMgr.Get().IsSpectator()) && GameState.Get().GetOpposingSidePlayer() != null && GameState.Get().GetOpposingSidePlayer().GetGameAccountId() == gameAccountId;
		string text = (obj2 != null) ? "myself" : (flag2 ? "opponent" : (flag ? "friend" : string.Empty));
		if (obj2 == null && !flag2 && !flag)
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
		string text2 = (player == null || player.GetBattleTag() == null) ? "" : player.GetBattleTag().ToString();
		if (string.IsNullOrEmpty(text2) && update.programId == BnetProgramId.BNET && ((update.groupId == 1U && update.fieldId == 4U) || (update.groupId == 2U && update.fieldId == 5U)))
		{
			text2 = update.stringVal;
		}
		if (string.IsNullOrEmpty(text2) && string.IsNullOrEmpty(text))
		{
			text = "someone";
		}
		else
		{
			text = string.Format("{0}{1}", text2, (string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text)) ? text : string.Format("({0})", text));
		}
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
		string fieldValue = BnetPresenceMgr.GetFieldValue(update);
		if (buffer == null)
		{
			buffer = new StringBuilder();
		}
		else
		{
			buffer.Append("\n");
		}
		buffer.AppendFormat("Update entity={0} who={1} {2}.{3}.{4}={5}", new object[]
		{
			string.Format("{{hi:{0} lo:{1}}}", update.entityId.hi, update.entityId.lo),
			text,
			bnetProgramId,
			text3,
			text4,
			fieldValue
		});
	}

	// Token: 0x06006BB7 RID: 27575 RVA: 0x0022E70B File Offset: 0x0022C90B
	private static string GetFieldValue(PresenceUpdate update)
	{
		if (update.programId == BnetProgramId.HEARTHSTONE)
		{
			return GamePresenceField.GetFieldValue(update);
		}
		return BnetPresenceField.GetFieldValue(update);
	}

	// Token: 0x06006BB8 RID: 27576 RVA: 0x0022E72C File Offset: 0x0022C92C
	private void OnBnetEventOccurred(BattleNet.BnetEvent bnetEvent, object userData)
	{
		if (bnetEvent == BattleNet.BnetEvent.Disconnected)
		{
			this.m_accounts.Clear();
			this.m_gameAccounts.Clear();
			this.m_players.Clear();
		}
	}

	// Token: 0x06006BB9 RID: 27577 RVA: 0x0022E754 File Offset: 0x0022C954
	private void OnAccountUpdate(PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetAccountId bnetAccountId = BnetAccountId.CreateFromEntityId(update.entityId);
		BnetAccount account = null;
		if (!this.m_accounts.TryGetValue(bnetAccountId, out account))
		{
			this.CreateAccount(bnetAccountId, update, changelist);
			return;
		}
		this.UpdateAccount(account, update, changelist);
	}

	// Token: 0x06006BBA RID: 27578 RVA: 0x0022E794 File Offset: 0x0022C994
	private void CreateAccount(BnetAccountId id, PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetAccount bnetAccount = new BnetAccount();
		this.m_accounts.Add(id, bnetAccount);
		bnetAccount.SetId(id);
		BnetPlayer bnetPlayer = null;
		if (!this.m_players.TryGetValue(id, out bnetPlayer))
		{
			bnetPlayer = new BnetPlayer(BnetPlayerSource.PRESENCE_UPDATE);
			this.m_players.Add(id, bnetPlayer);
			BnetPlayerChange bnetPlayerChange = new BnetPlayerChange();
			bnetPlayerChange.SetNewPlayer(bnetPlayer);
			changelist.AddChange(bnetPlayerChange);
		}
		bnetPlayer.SetAccount(bnetAccount);
		this.UpdateAccount(bnetAccount, update, changelist);
	}

	// Token: 0x06006BBB RID: 27579 RVA: 0x0022E808 File Offset: 0x0022CA08
	private void UpdateAccount(BnetAccount account, PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetPlayer player = this.m_players[account.GetId()];
		if (update.fieldId == 7U)
		{
			bool boolVal = update.boolVal;
			if (boolVal == account.IsAway())
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			account.SetAway(boolVal);
			if (boolVal)
			{
				account.SetBusy(false);
				return;
			}
		}
		else if (update.fieldId == 8U)
		{
			long intVal = update.intVal;
			if (intVal == account.GetAwayTimeMicrosec())
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			account.SetAwayTimeMicrosec(intVal);
			return;
		}
		else if (update.fieldId == 11U)
		{
			bool boolVal2 = update.boolVal;
			if (boolVal2 == account.IsBusy())
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			account.SetBusy(boolVal2);
			if (boolVal2)
			{
				account.SetAway(false);
				return;
			}
		}
		else if (update.fieldId == 4U)
		{
			BnetBattleTag bnetBattleTag = BnetBattleTag.CreateFromString(update.stringVal);
			if (bnetBattleTag == null)
			{
				global::Log.All.Print("Failed to parse BattleTag={0} for account={1}", new object[]
				{
					update.stringVal,
					update.entityId.lo
				});
			}
			if (bnetBattleTag == account.GetBattleTag())
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			account.SetBattleTag(bnetBattleTag);
			return;
		}
		else if (update.fieldId == 1U)
		{
			string stringVal = update.stringVal;
			if (stringVal == null)
			{
				global::Error.AddDevFatal("BnetPresenceMgr.UpdateAccount() - Failed to convert full name to native string for {0}.", new object[]
				{
					account
				});
				return;
			}
			if (stringVal == account.GetFullName())
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			account.SetFullName(stringVal);
			return;
		}
		else if (update.fieldId == 6U)
		{
			long intVal2 = update.intVal;
			if (intVal2 == account.GetLastOnlineMicrosec())
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			account.SetLastOnlineMicrosec(intVal2);
			return;
		}
		else if (update.fieldId != 3U && update.fieldId == 12U)
		{
			bool boolVal3 = update.boolVal;
			if (boolVal3 == account.IsAppearingOffline())
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			account.SetAppearingOffline(boolVal3);
		}
	}

	// Token: 0x06006BBC RID: 27580 RVA: 0x0022E9E0 File Offset: 0x0022CBE0
	private void OnGameAccountUpdate(PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetGameAccountId bnetGameAccountId = BnetGameAccountId.CreateFromEntityId(update.entityId);
		BnetGameAccount gameAccount = null;
		if (!this.m_gameAccounts.TryGetValue(bnetGameAccountId, out gameAccount))
		{
			this.CreateGameAccount(bnetGameAccountId, update, changelist);
			return;
		}
		this.UpdateGameAccount(gameAccount, update, changelist);
	}

	// Token: 0x06006BBD RID: 27581 RVA: 0x0022EA20 File Offset: 0x0022CC20
	private void CreateGameAccount(BnetGameAccountId id, PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetGameAccount bnetGameAccount = new BnetGameAccount();
		this.m_gameAccounts.Add(id, bnetGameAccount);
		bnetGameAccount.SetId(id);
		this.UpdateGameAccount(bnetGameAccount, update, changelist);
	}

	// Token: 0x06006BBE RID: 27582 RVA: 0x0022EA50 File Offset: 0x0022CC50
	private void UpdateGameAccount(BnetGameAccount gameAccount, PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetPlayer player = null;
		BnetAccountId ownerId = gameAccount.GetOwnerId();
		if (ownerId != null)
		{
			this.m_players.TryGetValue(ownerId, out player);
		}
		if (update.fieldId == 2U)
		{
			int num = gameAccount.IsBusy() ? 1 : 0;
			int num2 = (int)update.intVal;
			if (num2 == num)
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			bool flag = num2 == 1;
			gameAccount.SetBusy(flag);
			if (flag)
			{
				gameAccount.SetAway(false);
			}
			this.HandleGameAccountChange(player, update);
			return;
		}
		else if (update.fieldId == 10U)
		{
			bool boolVal = update.boolVal;
			if (boolVal == gameAccount.IsAway())
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			gameAccount.SetAway(boolVal);
			if (boolVal)
			{
				gameAccount.SetBusy(false);
			}
			this.HandleGameAccountChange(player, update);
			return;
		}
		else if (update.fieldId == 11U)
		{
			long intVal = update.intVal;
			if (intVal == gameAccount.GetAwayTimeMicrosec())
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			gameAccount.SetAwayTimeMicrosec(intVal);
			this.HandleGameAccountChange(player, update);
			return;
		}
		else if (update.fieldId == 5U)
		{
			BnetBattleTag bnetBattleTag = BnetBattleTag.CreateFromString(update.stringVal);
			if (bnetBattleTag == null)
			{
				global::Log.All.Print("Failed to parse BattleTag={0} for gameAccount={1}", new object[]
				{
					update.stringVal,
					update.entityId.lo
				});
			}
			if (bnetBattleTag == gameAccount.GetBattleTag())
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			gameAccount.SetBattleTag(bnetBattleTag);
			this.HandleGameAccountChange(player, update);
			return;
		}
		else if (update.fieldId == 1U)
		{
			bool boolVal2 = update.boolVal;
			if (boolVal2 == gameAccount.IsOnline())
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			gameAccount.SetOnline(boolVal2);
			this.HandleGameAccountChange(player, update);
			return;
		}
		else if (update.fieldId == 3U)
		{
			BnetProgramId bnetProgramId = new BnetProgramId(update.stringVal);
			if (bnetProgramId == gameAccount.GetProgramId())
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			gameAccount.SetProgramId(bnetProgramId);
			this.HandleGameAccountChange(player, update);
			return;
		}
		else if (update.fieldId == 4U)
		{
			long intVal2 = update.intVal;
			if (intVal2 == gameAccount.GetLastOnlineMicrosec())
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			gameAccount.SetLastOnlineMicrosec(intVal2);
			this.HandleGameAccountChange(player, update);
			return;
		}
		else if (update.fieldId == 7U)
		{
			BnetAccountId bnetAccountId = BnetAccountId.CreateFromEntityId(update.entityIdVal);
			if (bnetAccountId == gameAccount.GetOwnerId())
			{
				return;
			}
			this.UpdateGameAccountOwner(bnetAccountId, gameAccount, changelist);
			return;
		}
		else
		{
			if (update.fieldId != 9U)
			{
				if (update.fieldId == 1000U)
				{
					string text = update.stringVal;
					if (text == null)
					{
						text = "";
					}
					if (text == gameAccount.GetRichPresence())
					{
						return;
					}
					this.AddChangedPlayer(player, changelist);
					gameAccount.SetRichPresence(text);
					this.HandleGameAccountChange(player, update);
				}
				return;
			}
			if (!update.valCleared)
			{
				return;
			}
			if (gameAccount.GetRichPresence() == null)
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			gameAccount.SetRichPresence(null);
			this.HandleGameAccountChange(player, update);
			return;
		}
	}

	// Token: 0x06006BBF RID: 27583 RVA: 0x0022ED14 File Offset: 0x0022CF14
	private void UpdateGameAccountOwner(BnetAccountId ownerId, BnetGameAccount gameAccount, BnetPlayerChangelist changelist)
	{
		BnetPlayer bnetPlayer = null;
		BnetAccountId ownerId2 = gameAccount.GetOwnerId();
		if (ownerId2 != null && this.m_players.TryGetValue(ownerId2, out bnetPlayer))
		{
			bnetPlayer.RemoveGameAccount(gameAccount.GetId());
			this.AddChangedPlayer(bnetPlayer, changelist);
		}
		BnetPlayer bnetPlayer2 = null;
		if (this.m_players.TryGetValue(ownerId, out bnetPlayer2))
		{
			this.AddChangedPlayer(bnetPlayer2, changelist);
		}
		else
		{
			bnetPlayer2 = new BnetPlayer(BnetPlayerSource.PRESENCE_UPDATE);
			this.m_players.Add(ownerId, bnetPlayer2);
			BnetPlayerChange bnetPlayerChange = new BnetPlayerChange();
			bnetPlayerChange.SetNewPlayer(bnetPlayer2);
			changelist.AddChange(bnetPlayerChange);
		}
		gameAccount.SetOwnerId(ownerId);
		bnetPlayer2.AddGameAccount(gameAccount);
		this.CacheMyself(gameAccount, bnetPlayer2);
	}

	// Token: 0x06006BC0 RID: 27584 RVA: 0x0022EDB2 File Offset: 0x0022CFB2
	private void HandleGameAccountChange(BnetPlayer player, PresenceUpdate update)
	{
		if (player == null)
		{
			return;
		}
		player.OnGameAccountChanged(update.fieldId);
	}

	// Token: 0x06006BC1 RID: 27585 RVA: 0x0022EDC4 File Offset: 0x0022CFC4
	private void OnGameUpdate(PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetGameAccountId bnetGameAccountId = BnetGameAccountId.CreateFromEntityId(update.entityId);
		BnetGameAccount gameAccount = null;
		if (!this.m_gameAccounts.TryGetValue(bnetGameAccountId, out gameAccount))
		{
			this.CreateGameInfo(bnetGameAccountId, update, changelist);
			return;
		}
		this.UpdateGameInfo(gameAccount, update, changelist);
	}

	// Token: 0x06006BC2 RID: 27586 RVA: 0x0022EE04 File Offset: 0x0022D004
	private void CreateGameInfo(BnetGameAccountId id, PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetGameAccount bnetGameAccount = new BnetGameAccount();
		this.m_gameAccounts.Add(id, bnetGameAccount);
		bnetGameAccount.SetId(id);
		this.UpdateGameInfo(bnetGameAccount, update, changelist);
	}

	// Token: 0x06006BC3 RID: 27587 RVA: 0x0022EE34 File Offset: 0x0022D034
	private void UpdateGameInfo(BnetGameAccount gameAccount, PresenceUpdate update, BnetPlayerChangelist changelist)
	{
		BnetPlayer player = null;
		BnetAccountId ownerId = gameAccount.GetOwnerId();
		if (ownerId != null)
		{
			this.m_players.TryGetValue(ownerId, out player);
		}
		if (update.valCleared)
		{
			if (gameAccount.HasGameField(update.fieldId))
			{
				this.AddChangedPlayer(player, changelist);
				gameAccount.RemoveGameField(update.fieldId);
				this.HandleGameAccountChange(player, update);
			}
			return;
		}
		switch (update.fieldId)
		{
		case 1U:
			if (update.boolVal == gameAccount.GetGameFieldBool(update.fieldId))
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			gameAccount.SetGameField(update.fieldId, update.boolVal);
			this.HandleGameAccountChange(player, update);
			return;
		case 2U:
		case 4U:
		case 19U:
		case 20U:
			if (update.stringVal == gameAccount.GetGameFieldString(update.fieldId))
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			gameAccount.SetGameField(update.fieldId, update.stringVal);
			this.HandleGameAccountChange(player, update);
			return;
		case 5U:
		case 6U:
		case 7U:
		case 8U:
		case 9U:
		case 10U:
		case 11U:
		case 12U:
		case 13U:
		case 14U:
		case 15U:
		case 16U:
			if ((int)update.intVal == gameAccount.GetGameFieldInt(update.fieldId))
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			gameAccount.SetGameField(update.fieldId, (int)update.intVal);
			this.HandleGameAccountChange(player, update);
			return;
		case 17U:
		case 18U:
		case 21U:
		case 22U:
		case 23U:
		case 24U:
		case 25U:
			if (GeneralUtils.AreBytesEqual(update.blobVal, gameAccount.GetGameFieldBytes(update.fieldId)))
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			gameAccount.SetGameField(update.fieldId, update.blobVal);
			this.HandleGameAccountChange(player, update);
			return;
		case 26U:
			if (update.entityIdVal == gameAccount.GetGameFieldEntityId(update.fieldId))
			{
				return;
			}
			this.AddChangedPlayer(player, changelist);
			gameAccount.SetGameField(update.fieldId, update.entityIdVal);
			this.HandleGameAccountChange(player, update);
			return;
		}
		global::Log.Presence.PrintWarning("Unknown HS game account fieldId={0} - not saved into presence cache.", new object[]
		{
			update.fieldId
		});
	}

	// Token: 0x06006BC4 RID: 27588 RVA: 0x0022F06D File Offset: 0x0022D26D
	private void CacheMyself(BnetGameAccount gameAccount, BnetPlayer player)
	{
		if (player == this.m_myPlayer)
		{
			return;
		}
		if (gameAccount.GetId() != this.m_myGameAccountId)
		{
			return;
		}
		this.m_myPlayer = player;
	}

	// Token: 0x06006BC5 RID: 27589 RVA: 0x0022F094 File Offset: 0x0022D294
	private void AddChangedPlayer(BnetPlayer player, BnetPlayerChangelist changelist)
	{
		if (player == null)
		{
			return;
		}
		if (changelist.HasChange(player))
		{
			return;
		}
		BnetPlayerChange bnetPlayerChange = new BnetPlayerChange();
		bnetPlayerChange.SetOldPlayer(player.Clone());
		bnetPlayerChange.SetNewPlayer(player);
		changelist.AddChange(bnetPlayerChange);
	}

	// Token: 0x06006BC6 RID: 27590 RVA: 0x0022F0D0 File Offset: 0x0022D2D0
	private void FirePlayersChangedEvent(BnetPlayerChangelist changelist)
	{
		if (changelist == null)
		{
			return;
		}
		if (changelist.GetChanges().Count == 0)
		{
			return;
		}
		BnetPresenceMgr.PlayersChangedListener[] array = this.m_playersChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(changelist);
		}
	}

	// Token: 0x06006BC7 RID: 27591 RVA: 0x0022F114 File Offset: 0x0022D314
	private bool ShouldUpdateGameField(uint fieldId, object val, out BnetGameAccount hsGameAccount)
	{
		hsGameAccount = null;
		if (this.m_myPlayer == null)
		{
			return true;
		}
		hsGameAccount = this.m_myPlayer.GetHearthstoneGameAccount();
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

	// Token: 0x06006BC8 RID: 27592 RVA: 0x0022F174 File Offset: 0x0022D374
	private bool ShouldUpdateGameFieldBlob(uint fieldId, byte[] val, out BnetGameAccount hsGameAccount)
	{
		hsGameAccount = null;
		if (this.m_myPlayer == null)
		{
			return true;
		}
		hsGameAccount = this.m_myPlayer.GetHearthstoneGameAccount();
		if (hsGameAccount == null)
		{
			return true;
		}
		if (hsGameAccount.HasGameField(fieldId))
		{
			byte[] gameFieldBytes = hsGameAccount.GetGameFieldBytes(fieldId);
			if (GeneralUtils.AreArraysEqual<byte>(val, gameFieldBytes))
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

	// Token: 0x06006BC9 RID: 27593 RVA: 0x0022F1CC File Offset: 0x0022D3CC
	private BnetPlayerChangelist ChangeGameField(BnetGameAccount hsGameAccount, uint fieldId, object val)
	{
		if (hsGameAccount == null)
		{
			return null;
		}
		BnetPlayerChange bnetPlayerChange = new BnetPlayerChange();
		bnetPlayerChange.SetOldPlayer(this.m_myPlayer.Clone());
		bnetPlayerChange.SetNewPlayer(this.m_myPlayer);
		hsGameAccount.SetGameField(fieldId, val);
		BnetPlayerChangelist bnetPlayerChangelist = new BnetPlayerChangelist();
		bnetPlayerChangelist.AddChange(bnetPlayerChange);
		return bnetPlayerChangelist;
	}

	// Token: 0x06006BCA RID: 27594 RVA: 0x0022F21B File Offset: 0x0022D41B
	public void Cheat_SetMyPlayer(BnetPlayer player)
	{
		this.m_myPlayer = player;
	}

	// Token: 0x06006BCB RID: 27595 RVA: 0x0022F224 File Offset: 0x0022D424
	public bool Cheat_AddAccount(BnetAccount account)
	{
		BnetAccountId id = account.GetId();
		if (this.m_accounts.ContainsKey(id))
		{
			global::Error.AddDevFatal("BnetPresenceMgr.AddAccount() - {0} is already being tracked.", new object[]
			{
				account
			});
			return false;
		}
		this.m_accounts.Add(id, account);
		return true;
	}

	// Token: 0x06006BCC RID: 27596 RVA: 0x0022F26C File Offset: 0x0022D46C
	public bool Cheat_AddGameAccount(BnetGameAccount gameAccount)
	{
		BnetGameAccountId id = gameAccount.GetId();
		if (this.m_gameAccounts.ContainsKey(id))
		{
			global::Error.AddDevFatal("BnetPresenceMgr.AddGameAccount() - {0} is already being tracked.", new object[]
			{
				gameAccount
			});
			return false;
		}
		this.m_gameAccounts.Add(id, gameAccount);
		return true;
	}

	// Token: 0x06006BCD RID: 27597 RVA: 0x0022F2B4 File Offset: 0x0022D4B4
	public bool Cheat_AddPlayer(BnetPlayer player)
	{
		BnetAccountId accountId = player.GetAccountId();
		if (this.m_players.ContainsKey(accountId))
		{
			global::Error.AddDevFatal("BnetPresenceMgr.AddPlayer() - {0} is already being tracked.", new object[]
			{
				player
			});
			return false;
		}
		this.m_players.Add(accountId, player);
		return true;
	}

	// Token: 0x04005732 RID: 22322
	private static BnetPresenceMgr s_instance;

	// Token: 0x04005733 RID: 22323
	private global::Map<BnetAccountId, BnetAccount> m_accounts = new global::Map<BnetAccountId, BnetAccount>();

	// Token: 0x04005734 RID: 22324
	private global::Map<BnetGameAccountId, BnetGameAccount> m_gameAccounts = new global::Map<BnetGameAccountId, BnetGameAccount>();

	// Token: 0x04005735 RID: 22325
	private global::Map<BnetAccountId, BnetPlayer> m_players = new global::Map<BnetAccountId, BnetPlayer>();

	// Token: 0x04005736 RID: 22326
	private BnetAccountId m_myBattleNetAccountId;

	// Token: 0x04005737 RID: 22327
	private BnetGameAccountId m_myGameAccountId;

	// Token: 0x04005738 RID: 22328
	private BnetPlayer m_myPlayer;

	// Token: 0x04005739 RID: 22329
	private List<BnetPresenceMgr.PlayersChangedListener> m_playersChangedListeners = new List<BnetPresenceMgr.PlayersChangedListener>();

	// Token: 0x02002342 RID: 9026
	// (Invoke) Token: 0x06012A5A RID: 76378
	public delegate void PlayersChangedCallback(BnetPlayerChangelist changelist, object userData);

	// Token: 0x02002343 RID: 9027
	private class PlayersChangedListener : global::EventListener<BnetPresenceMgr.PlayersChangedCallback>
	{
		// Token: 0x06012A5D RID: 76381 RVA: 0x00511E32 File Offset: 0x00510032
		public void Fire(BnetPlayerChangelist changelist)
		{
			this.m_callback(changelist, this.m_userData);
		}
	}
}
