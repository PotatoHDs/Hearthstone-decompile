using System;
using System.Collections.Generic;
using System.Linq;
using bgs;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using bnet.protocol;
using bnet.protocol.account.v1;
using Hearthstone;
using PegasusShared;
using SpectatorProto;

public class PartyManager : IService
{
	public enum PartyInviteEvent
	{
		I_CREATED_PARTY,
		I_SENT_INVITE,
		I_RESCINDED_INVITE,
		FRIEND_RECEIVED_INVITE,
		FRIEND_ACCEPTED_INVITE,
		FRIEND_DECLINED_INVITE,
		INVITE_EXPIRED,
		I_ACCEPTED_INVITE,
		I_DECLINED_INVITE,
		FRIEND_RESCINDED_INVITE,
		FRIEND_LEFT,
		LEADER_DISSOLVED_PARTY
	}

	public class PartyData
	{
		public PartyType m_type;

		public ulong m_inviteId;

		public PartyId m_partyId;

		public ScenarioDbId m_scenarioId;

		public FormatType m_format;

		public int m_season;
	}

	private class ChangedListener : EventListener<ChangedCallback>
	{
		public void Fire(PartyInviteEvent challengeEvent, BnetGameAccountId playerGameAccountId, PartyData challengeData)
		{
			m_callback(challengeEvent, playerGameAccountId, challengeData, m_userData);
		}
	}

	public delegate void ChangedCallback(PartyInviteEvent challengeEvent, BnetGameAccountId playerGameAccountId, PartyData challengeData, object userData);

	private PartyData m_partyData = new PartyData();

	private DialogBase m_inviteDialog;

	private PartyId m_pendingParty;

	private const string ATTRIBUTE_KEY_QUEUE = "queue";

	private const string ATTRIBUTE_VALUE_IN_QUEUE = "in_queue";

	private const string ATTRIBUTE_VALUE_CANCEL_QUEUE = "cancel_queue";

	private const string ATTRIBUTE_KEY_CANCELED_BY = "canceled_by";

	private const string ATTRIBUTE_KEY_STATUS = "ready";

	private const string ATTRIBUTE_VALUE_READY = "ready";

	private const string ATTRIBUTE_VALUE_NOT_READY = "not_ready";

	private const string ATTRIBUTE_VALUE_DECLINED = "declined";

	private const string ATTRIBUTE_VALUE_LEADER = "leader";

	private const string ATTRIBUTE_KEY_SPECTATOR_INFO = "spectator_info";

	public static int BATTLEGROUNDS_PARTY_LIMIT = 8;

	public static int BATTLEGROUNDS_MAX_RANKED_PARTY_SIZE_FALLBACK = 4;

	private List<ChangedListener> m_changedListeners = new List<ChangedListener>();

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		m_partyData = new PartyData();
		BnetParty.OnJoined += BnetParty_OnJoined;
		BnetParty.OnReceivedInvite += BnetParty_OnReceivedInvite;
		BnetParty.OnPartyAttributeChanged += BnetParty_OnPartyAttributeChanged;
		BnetParty.OnMemberAttributeChanged += BnetParty_OnMemberAttributeChanged;
		BnetParty.OnMemberEvent += BnetParty_OnMemberEvent;
		BnetParty.OnSentInvite += BnetParty_OnSentInvite;
		BnetParty.OnReceivedInviteRequest += BnetParty_OnReceivedInviteRequest;
		BnetPresenceMgr.Get().AddPlayersChangedListener(OnPresenceUpdated);
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
		LoginManager.Get().OnInitialClientStateReceived += OnLoginComplete;
		HearthstoneApplication.Get().WillReset += WillReset;
		yield break;
	}

	public void Shutdown()
	{
		BnetParty.OnJoined -= BnetParty_OnJoined;
		BnetParty.OnReceivedInvite -= BnetParty_OnReceivedInvite;
		BnetParty.OnPartyAttributeChanged -= BnetParty_OnPartyAttributeChanged;
		BnetParty.OnMemberAttributeChanged -= BnetParty_OnMemberAttributeChanged;
		BnetParty.OnMemberEvent -= BnetParty_OnMemberEvent;
		BnetParty.OnSentInvite -= BnetParty_OnSentInvite;
		BnetParty.OnReceivedInviteRequest -= BnetParty_OnReceivedInviteRequest;
		BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPresenceUpdated);
		FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
		LoginManager.Get().OnInitialClientStateReceived -= OnLoginComplete;
		HearthstoneApplication.Get().WillReset -= WillReset;
	}

	public Type[] GetDependencies()
	{
		return new Type[2]
		{
			typeof(LoginManager),
			typeof(Network)
		};
	}

	public static PartyManager Get()
	{
		return HearthstoneServices.Get<PartyManager>();
	}

	private void WillReset()
	{
		ClearPartyData();
	}

	public static bool IsPartyTypeEnabledInGuardian(PartyType partyType)
	{
		NetCache.NetCacheFeatures.CacheGames games = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games;
		return partyType switch
		{
			PartyType.BATTLEGROUNDS_PARTY => games.BattlegroundsFriendlyChallenge, 
			PartyType.FRIENDLY_CHALLENGE => games.Friendly, 
			_ => true, 
		};
	}

	public bool IsInParty()
	{
		return m_partyData.m_partyId != null;
	}

	public bool IsInBattlegroundsParty()
	{
		if (IsInParty())
		{
			return m_partyData.m_type == PartyType.BATTLEGROUNDS_PARTY;
		}
		return false;
	}

	public bool IsPlayerInCurrentPartyOrPending(BnetGameAccountId playerGameAccountId)
	{
		if (IsPlayerInCurrentParty(playerGameAccountId))
		{
			return true;
		}
		if (IsPlayerPendingInCurrentParty(playerGameAccountId))
		{
			return true;
		}
		return false;
	}

	public bool IsPlayerInCurrentParty(BnetGameAccountId playerGameAccountId)
	{
		if (BnetParty.IsMember(m_partyData.m_partyId, playerGameAccountId))
		{
			return true;
		}
		return false;
	}

	public bool IsPlayerPendingInCurrentParty(BnetGameAccountId playerGameAccountId)
	{
		PartyInvite[] pendingInvites = GetPendingInvites();
		for (int i = 0; i < pendingInvites.Length; i++)
		{
			if (pendingInvites[i].InviteeId == playerGameAccountId)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsPlayerInAnyParty(BnetGameAccountId playerGameAccountId)
	{
		BnetPlayer player = BnetUtils.GetPlayer(playerGameAccountId);
		if (player == null)
		{
			return false;
		}
		BnetGameAccount hearthstoneGameAccount = player.GetHearthstoneGameAccount();
		if (hearthstoneGameAccount == null || hearthstoneGameAccount.GetGameFields() == null)
		{
			return false;
		}
		return hearthstoneGameAccount.GetPartyId() != PartyId.Empty;
	}

	public bool IsPartyLeader()
	{
		return BnetParty.IsLeader(m_partyData.m_partyId);
	}

	public bool CanInvite(BnetGameAccountId playerGameAccountId)
	{
		if (IsInParty() && !IsPartyLeader())
		{
			return false;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (!myPlayer.IsOnline() || myPlayer.IsAppearingOffline())
		{
			return false;
		}
		if (IsPlayerInAnyParty(playerGameAccountId))
		{
			return false;
		}
		if (IsPlayerPendingInCurrentParty(playerGameAccountId))
		{
			return false;
		}
		if (IsInParty() && GetCurrentPartySize() >= GetMaxPartySizeByPartyType(m_partyData.m_type))
		{
			return false;
		}
		BnetPlayer player = BnetUtils.GetPlayer(playerGameAccountId);
		if (player == null || !FriendChallengeMgr.Get().IsOpponentAvailable(player))
		{
			return false;
		}
		return true;
	}

	public bool CanKick(BnetGameAccountId playerGameAccountId)
	{
		if (IsInParty() && !IsPartyLeader())
		{
			return false;
		}
		if (!IsPlayerInCurrentPartyOrPending(playerGameAccountId))
		{
			return false;
		}
		return true;
	}

	public bool CanSpectatePartyMember(BnetGameAccountId gameAccountId)
	{
		JoinInfo spectatorJoinInfoForPlayer = GetSpectatorJoinInfoForPlayer(gameAccountId);
		if (spectatorJoinInfoForPlayer == null)
		{
			return false;
		}
		return SpectatorManager.Get().CanSpectate(gameAccountId, spectatorJoinInfoForPlayer);
	}

	public bool SpectatePartyMember(BnetGameAccountId gameAccountId)
	{
		JoinInfo spectatorJoinInfoForPlayer = GetSpectatorJoinInfoForPlayer(gameAccountId);
		if (spectatorJoinInfoForPlayer == null)
		{
			return false;
		}
		if (!CanSpectatePartyMember(gameAccountId))
		{
			return false;
		}
		SpectatorManager.Get().SpectatePlayer(gameAccountId, spectatorJoinInfoForPlayer);
		return true;
	}

	public void SendInvite(PartyType partyType, BnetGameAccountId playerGameAccountId)
	{
		if (CanInvite(playerGameAccountId) && !IsPlayerInCurrentPartyOrPending(playerGameAccountId))
		{
			if (!IsInParty() && ShouldSupportPartyType(partyType))
			{
				CreateParty(partyType, playerGameAccountId);
			}
			else if (partyType == PartyType.BATTLEGROUNDS_PARTY)
			{
				InvitePlayerToBattlegroundsParty(playerGameAccountId);
			}
			else
			{
				SendInvite_Internal(playerGameAccountId);
			}
		}
	}

	public void KickPlayerFromParty(BnetGameAccountId playerGameAccountId)
	{
		if (!IsInParty())
		{
			return;
		}
		if (BnetParty.IsMember(m_partyData.m_partyId, playerGameAccountId))
		{
			BnetParty.KickMember(m_partyData.m_partyId, playerGameAccountId);
			return;
		}
		ulong? pendingInviteIdFromGameAccount = GetPendingInviteIdFromGameAccount(playerGameAccountId);
		if (pendingInviteIdFromGameAccount.HasValue)
		{
			BnetNearbyPlayerMgr.Get().FindNearbyStranger(playerGameAccountId)?.GetHearthstoneGameAccount().SetGameField(1u, false);
			BnetParty.RevokeSentInvite(m_partyData.m_partyId, pendingInviteIdFromGameAccount.Value);
			FireChangedEvent(PartyInviteEvent.I_RESCINDED_INVITE, playerGameAccountId);
		}
		else
		{
			Log.Party.PrintError("Unable to kick player {0} from party. Player not found in party.", playerGameAccountId.ToString());
		}
	}

	public PartyMember[] GetMembers()
	{
		if (m_partyData.m_partyId == null)
		{
			return new PartyMember[0];
		}
		return BnetParty.GetMembers(m_partyData.m_partyId);
	}

	public PartyInvite[] GetPendingInvites()
	{
		if (m_partyData.m_partyId == null)
		{
			return new PartyInvite[0];
		}
		return BnetParty.GetSentInvites(m_partyData.m_partyId);
	}

	public void FindGame()
	{
		BnetParty.SetPartyAttributeString(m_partyData.m_partyId, "queue", "in_queue");
		if (IsBaconParty())
		{
			Network.Get().EnterBattlegroundsWithParty(GetMembers(), 3459);
		}
		WaitForGame();
	}

	public BnetGameAccountId GetLeader()
	{
		if (!IsInParty())
		{
			return null;
		}
		PartyMember leader = BnetParty.GetLeader(m_partyData.m_partyId);
		if (leader != null)
		{
			return leader.GameAccountId;
		}
		Log.Party.PrintError("PartyManager.GetLeader() - Unable to get party leader!");
		return null;
	}

	public bool IsBaconParty()
	{
		if (m_partyData.m_partyId != null)
		{
			return m_partyData.m_scenarioId == ScenarioDbId.TB_BACONSHOP_8P;
		}
		return false;
	}

	public void LeaveParty()
	{
		if (IsInParty())
		{
			if (IsPartyLeader())
			{
				BnetParty.DissolveParty(m_partyData.m_partyId);
			}
			else
			{
				BnetParty.Leave(m_partyData.m_partyId);
			}
			ClearPartyData();
		}
	}

	public void CancelQueue()
	{
		BnetGameAccountId hearthstoneGameAccountId = BnetPresenceMgr.Get().GetMyPlayer().GetHearthstoneGameAccountId();
		byte[] value = ProtobufUtil.ToByteArray(new BnetId
		{
			Hi = hearthstoneGameAccountId.GetHi(),
			Lo = hearthstoneGameAccountId.GetLo()
		});
		BnetParty.SetPartyAttributeBlob(m_partyData.m_partyId, "canceled_by", value);
		BnetParty.SetPartyAttributeString(m_partyData.m_partyId, "queue", "cancel_queue");
	}

	public int GetCurrentPartySize()
	{
		return GetCurrentAndPendingPartyMembers().Count();
	}

	public int GetReadyPartyMemberCount()
	{
		int num = 0;
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		PartyMember[] members = BnetParty.GetMembers(m_partyData.m_partyId);
		for (int i = 0; i < members.Length; i++)
		{
			BnetPlayer player = BnetUtils.GetPlayer(members[i].GameAccountId);
			if (player != null && (myPlayer == player || FriendChallengeMgr.Get().IsOpponentAvailable(player)))
			{
				num++;
			}
		}
		return num;
	}

	public List<BnetGameAccountId> GetCurrentAndPendingPartyMembers()
	{
		List<BnetGameAccountId> list = new List<BnetGameAccountId>();
		PartyMember[] members = BnetParty.GetMembers(m_partyData.m_partyId);
		foreach (PartyMember partyMember in members)
		{
			list.Add(partyMember.GameAccountId);
		}
		PartyInvite[] pendingInvites = GetPendingInvites();
		for (int i = 0; i < pendingInvites.Length; i++)
		{
			BnetGameAccountId inviteeId = pendingInvites[i].InviteeId;
			if (!list.Contains(inviteeId))
			{
				list.Add(inviteeId);
			}
		}
		return list;
	}

	public int GetMaxPartySizeByPartyType(PartyType type)
	{
		if (type == PartyType.BATTLEGROUNDS_PARTY)
		{
			return BATTLEGROUNDS_PARTY_LIMIT;
		}
		Log.Party.PrintError("GetMaxPartySizeByPartyType() - Unsupported party type {0}.", type.ToString());
		return 2;
	}

	public void UpdateSpectatorJoinInfo(JoinInfo joinInfo)
	{
		if (IsInParty())
		{
			BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
			byte[] value = ((joinInfo == null) ? null : ProtobufUtil.ToByteArray(joinInfo));
			BnetParty.SetMemberAttributeBlob(m_partyData.m_partyId, myGameAccountId.GetGameAccountHandle(), "spectator_info", value);
		}
	}

	public JoinInfo GetSpectatorJoinInfoForPlayer(BnetGameAccountId gameAccountId)
	{
		if (!IsInParty())
		{
			return null;
		}
		byte[] memberAttributeBlob = BnetParty.GetMemberAttributeBlob(m_partyData.m_partyId, gameAccountId.GetGameAccountHandle(), "spectator_info");
		if (memberAttributeBlob != null && memberAttributeBlob.Length != 0)
		{
			return ProtobufUtil.ParseFrom<JoinInfo>(memberAttributeBlob);
		}
		return null;
	}

	public string GetPartyMemberName(BnetGameAccountId playerGameAccountId)
	{
		BnetPlayer player = BnetUtils.GetPlayer(playerGameAccountId);
		if (player != null)
		{
			return player.GetBestName();
		}
		PartyMember[] members = GetMembers();
		foreach (PartyMember partyMember in members)
		{
			if (!(partyMember.GameAccountId == playerGameAccountId))
			{
				continue;
			}
			if (!string.IsNullOrEmpty(partyMember.BattleTag))
			{
				BnetBattleTag bnetBattleTag = BnetBattleTag.CreateFromString(partyMember.BattleTag);
				if (!(bnetBattleTag == null))
				{
					return bnetBattleTag.GetName();
				}
				return partyMember.BattleTag;
			}
			Log.Party.PrintError("GetPartyMemberName() - No name for party member {0}.", playerGameAccountId.ToString());
		}
		PartyInvite[] pendingInvites = GetPendingInvites();
		foreach (PartyInvite partyInvite in pendingInvites)
		{
			if (!(partyInvite.InviteeId == playerGameAccountId))
			{
				continue;
			}
			if (!string.IsNullOrEmpty(partyInvite.InviteeName))
			{
				BnetBattleTag bnetBattleTag2 = BnetBattleTag.CreateFromString(partyInvite.InviteeName);
				if (!(bnetBattleTag2 == null))
				{
					return bnetBattleTag2.GetName();
				}
				return partyInvite.InviteeName;
			}
			Log.Party.PrintError("GetPartyMemberName() - No name for pending invitee {0}.", playerGameAccountId.ToString());
		}
		return GameStrings.Get("GLUE_PARTY_MEMBER_NO_NAME");
	}

	public bool HasPendingPartyInviteOrDialog()
	{
		if (!(m_pendingParty != null))
		{
			return m_inviteDialog != null;
		}
		return true;
	}

	public int GetBattlegroundsMaxRankedPartySize()
	{
		return NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>()?.BattlegroundsMaxRankedPartySize ?? BATTLEGROUNDS_MAX_RANKED_PARTY_SIZE_FALLBACK;
	}

	private void InvitePlayerToBattlegroundsParty(BnetGameAccountId playerGameAccountId)
	{
		int currentPartySize = GetCurrentPartySize();
		if (currentPartySize < BATTLEGROUNDS_PARTY_LIMIT)
		{
			if (currentPartySize == GetBattlegroundsMaxRankedPartySize())
			{
				ShowBattlegroundsPrivatePartyDialog(playerGameAccountId);
			}
			else
			{
				SendInvite_Internal(playerGameAccountId);
			}
		}
	}

	private void ShowBattlegroundsPrivatePartyDialog(BnetGameAccountId playerGameAccountId)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_BACON_PRIVATE_PARTY_TITLE");
		popupInfo.m_text = GameStrings.Format("GLUE_BACON_PRIVATE_PARTY_WARNING", GetBattlegroundsMaxRankedPartySize());
		popupInfo.m_iconSet = AlertPopup.PopupInfo.IconSet.Default;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_confirmText = GameStrings.Get("GLUE_COLLECTION_DECK_COMPLETE_POPUP_CONFIRM");
		popupInfo.m_cancelText = GameStrings.Get("GLUE_COLLECTION_DECK_COMPLETE_POPUP_CANCEL");
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			if (response == AlertPopup.Response.CONFIRM)
			{
				SendInvite_Internal(playerGameAccountId);
			}
		};
		AlertPopup.PopupInfo info = popupInfo;
		DialogManager.Get().ShowPopup(info);
	}

	private void ClearPartyData()
	{
		m_partyData = new PartyData();
		UpdateMyAvailability();
	}

	private bool ShouldSupportPartyType(PartyType partyType)
	{
		return partyType == PartyType.BATTLEGROUNDS_PARTY;
	}

	private void WaitForGame()
	{
		GameMgr.Get().WaitForFriendChallengeToStart(m_partyData.m_format, BrawlType.BRAWL_TYPE_UNKNOWN, (int)m_partyData.m_scenarioId, 0, IsBaconParty());
	}

	private ScenarioDbId GetScenario(PartyType type)
	{
		if (type == PartyType.BATTLEGROUNDS_PARTY)
		{
			return ScenarioDbId.TB_BACONSHOP_8P;
		}
		Log.Party.PrintError("PartyManager.GetScenario() received an unsupported party type: {0}", type);
		return ScenarioDbId.INVALID;
	}

	private FormatType GetFormat(PartyType type)
	{
		return FormatType.FT_UNKNOWN;
	}

	private int GetSeason(PartyType type)
	{
		return 0;
	}

	private SceneMgr.Mode GetMode(PartyType type)
	{
		if (type == PartyType.BATTLEGROUNDS_PARTY)
		{
			return SceneMgr.Mode.BACON;
		}
		Log.Party.PrintError("PartyManager.GetMode() received an unsupported party type: {0}", type);
		return SceneMgr.Mode.HUB;
	}

	private void CreateParty(PartyType type, BnetGameAccountId playerToInvite)
	{
		if (IsInParty())
		{
			return;
		}
		m_partyData.m_type = type;
		m_partyData.m_scenarioId = GetScenario(type);
		m_partyData.m_format = GetFormat(type);
		m_partyData.m_season = GetSeason(type);
		bnet.protocol.Attribute attributeV = ProtocolHelper.CreateAttribute("WTCG.Game.ScenarioId", (long)m_partyData.m_scenarioId);
		bnet.protocol.Attribute attributeV2 = ProtocolHelper.CreateAttribute("WTCG.Format.Type", (long)m_partyData.m_format);
		bnet.protocol.Attribute attributeV3 = ProtocolHelper.CreateAttribute("WTCG.Season.Id", m_partyData.m_season);
		BnetParty.CreateParty(type, PrivacyLevel.OPEN_INVITATION, delegate(PartyType pType, PartyId newlyCreatedPartyId)
		{
			m_partyData.m_partyId = newlyCreatedPartyId;
			UpdateMyAvailability();
			BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
			SetPersonalPartyMemberAttributes();
			FireChangedEvent(PartyInviteEvent.I_CREATED_PARTY, myGameAccountId);
			if (playerToInvite != null)
			{
				SendInvite(type, playerToInvite);
			}
		}, ProtocolHelper.V1AttributeToV2Attribute(attributeV), ProtocolHelper.V1AttributeToV2Attribute(attributeV2), ProtocolHelper.V1AttributeToV2Attribute(attributeV3));
	}

	private void SetPersonalPartyMemberAttributes()
	{
		GameAccountHandle gameAccountHandle = BnetPresenceMgr.Get().GetMyPlayer().GetBestGameAccountId()
			.GetGameAccountHandle();
		BnetParty.SetMemberAttributeString(m_partyData.m_partyId, gameAccountHandle, "ready", "ready");
	}

	private void SendInvite_Internal(BnetGameAccountId bnetGameAccountId)
	{
		BnetParty.SendInvite(m_partyData.m_partyId, bnetGameAccountId, isReservation: true);
	}

	private void UpdateMyAvailability()
	{
		if (Network.ShouldBeConnectedToAurora() && Network.IsLoggedIn())
		{
			PartyId partyId = m_partyData.m_partyId;
			BnetPresenceMgr.Get().SetGameField(26u, (partyId != null) ? partyId.ToEntityId() : PartyId.Empty.ToEntityId());
			BnetNearbyPlayerMgr.Get().SetPartyId(partyId ?? PartyId.Empty);
		}
	}

	private void ShowInviteDialog(BnetGameAccountId leaderGameAccountId, PartyType partyType)
	{
		BnetPlayer player = BnetUtils.GetPlayer(leaderGameAccountId);
		if (player == null)
		{
			Log.Party.PrintError("PartyManager.ShowInviteDialog() - Received invite from player {0} with no presence!", leaderGameAccountId);
		}
		DialogManager.Get().ShowFriendlyChallenge(FormatType.FT_UNKNOWN, player, challengeIsTavernBrawl: false, partyType, OnInviteReceivedDialogResponse, OnInviteReceivedDialogProcessed);
	}

	private void ShowSimpleAlertDialog(string header, string body, bool showAlertIcon = false)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get(header);
		popupInfo.m_text = GameStrings.Get(body);
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_showAlertIcon = showAlertIcon;
		popupInfo.m_okText = GameStrings.Get("GLOBAL_OKAY");
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private bool OnInviteReceivedDialogProcessed(DialogBase dialog, object userData)
	{
		m_inviteDialog = dialog;
		return true;
	}

	private void OnInviteReceivedDialogResponse(bool accept)
	{
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		if (accept)
		{
			if (BnetPresenceMgr.Get().GetMyPlayer().IsAppearingOffline())
			{
				DeclinePartyInvite(m_partyData.m_inviteId);
				ShowSimpleAlertDialog("GLUE_BACON_INVITE_WHILE_APPEARING_OFFLINE_HEADER", "GLUE_BACON_INVITE_WHILE_APPEARING_OFFLINE", showAlertIcon: true);
			}
			else if (m_pendingParty != null && !IsInParty())
			{
				m_partyData.m_partyId = m_pendingParty;
				BnetParty.AcceptReceivedInvite(m_partyData.m_inviteId);
				UpdateMyAvailability();
				FireChangedEvent(PartyInviteEvent.I_ACCEPTED_INVITE, myGameAccountId);
				TransitionModeIfNeeded();
			}
			else if (IsInParty())
			{
				ShowSimpleAlertDialog("GLUE_BACON_EXPIRED_INVITE_HEADER", "GLUE_BACON_PARTY_INVITE_WHILE_IN_PARTY");
			}
			else
			{
				ShowSimpleAlertDialog("GLUE_BACON_EXPIRED_INVITE_HEADER", "GLUE_BACON_EXPIRD_INVITE_BODY");
			}
		}
		else
		{
			DeclinePartyInvite(m_partyData.m_inviteId);
		}
		m_inviteDialog = null;
		m_pendingParty = null;
		FriendChallengeMgr.Get().UpdateMyAvailability();
	}

	private void DeclinePartyInvite(ulong inviteId)
	{
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		BnetParty.DeclineReceivedInvite(inviteId);
		FireChangedEvent(PartyInviteEvent.I_DECLINED_INVITE, myGameAccountId);
		m_pendingParty = null;
	}

	private void TransitionModeIfNeeded()
	{
		SceneMgr.Mode mode = GetMode(m_partyData.m_type);
		SceneMgr.Mode mode2 = SceneMgr.Get().GetMode();
		if (mode != mode2)
		{
			SceneMgr.Get().SetNextMode(mode);
		}
	}

	private void OnPresenceUpdated(BnetPlayerChangelist changelist, object userData)
	{
		foreach (BnetPlayerChange change in changelist.GetChanges())
		{
			BnetPlayer player = change.GetPlayer();
			BnetGameAccountId bestGameAccountId = player.GetBestGameAccountId();
			if (IsPlayerInCurrentPartyOrPending(bestGameAccountId) && !player.IsOnline())
			{
				KickPlayerFromParty(bestGameAccountId);
			}
		}
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		ClearPartyData();
	}

	private void OnLoginComplete()
	{
		UpdateMyAvailability();
	}

	private ulong? GetPendingInviteIdFromGameAccount(BnetGameAccountId gameAccountId)
	{
		PartyInvite[] pendingInvites = GetPendingInvites();
		foreach (PartyInvite partyInvite in pendingInvites)
		{
			if (partyInvite.InviteeId == gameAccountId)
			{
				return partyInvite.InviteId;
			}
		}
		return null;
	}

	private void BnetParty_OnJoined(OnlineEventType evt, PartyInfo party, LeaveReason? reason)
	{
		if (!ShouldSupportPartyType(party.Type) || party.Id != m_partyData.m_partyId)
		{
			return;
		}
		if (evt == OnlineEventType.ADDED)
		{
			m_partyData.m_partyId = party.Id;
			UpdateMyAvailability();
			SetPersonalPartyMemberAttributes();
			long? partyAttributeLong = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Game.ScenarioId");
			if (partyAttributeLong.HasValue)
			{
				m_partyData.m_scenarioId = (ScenarioDbId)partyAttributeLong.Value;
			}
			long? partyAttributeLong2 = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Format.Type");
			if (partyAttributeLong2.HasValue)
			{
				m_partyData.m_format = (FormatType)partyAttributeLong2.Value;
			}
			long? partyAttributeLong3 = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Season.Id");
			if (partyAttributeLong3.HasValue)
			{
				m_partyData.m_season = (int)partyAttributeLong3.Value;
			}
		}
		if (evt == OnlineEventType.REMOVED)
		{
			ClearPartyData();
			UpdateMyAvailability();
			switch (reason)
			{
			case LeaveReason.DISSOLVED_BY_MEMBER:
			case LeaveReason.DISSOLVED_BY_SERVICE:
				ShowSimpleAlertDialog("GLUE_BACON_PARTY_DISBANDED_HEADER", "GLUE_BACON_PARTY_DISBANDED_BODY");
				break;
			case LeaveReason.MEMBER_KICKED:
				ShowSimpleAlertDialog("GLUE_BACON_PARTY_KICKED_HEADER", "GLUE_BACON_PARTY_KICKED_BODY");
				break;
			}
			FireChangedEvent(PartyInviteEvent.LEADER_DISSOLVED_PARTY, null);
		}
	}

	private void BnetParty_OnReceivedInvite(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, InviteRemoveReason? reason)
	{
		if (!ShouldSupportPartyType(party.Type))
		{
			return;
		}
		switch (evt)
		{
		case OnlineEventType.ADDED:
			if (!IsPartyTypeEnabledInGuardian(party.Type))
			{
				DeclinePartyInvite(inviteId);
				return;
			}
			if (!FriendChallengeMgr.Get().AmIAvailable())
			{
				DeclinePartyInvite(inviteId);
				return;
			}
			m_partyData.m_inviteId = inviteId;
			m_partyData.m_type = party.Type;
			m_pendingParty = party.Id;
			ShowInviteDialog(inviter, party.Type);
			break;
		case OnlineEventType.REMOVED:
			m_pendingParty = null;
			if (m_inviteDialog != null)
			{
				m_inviteDialog.AddHiddenOrDestroyedListener(delegate
				{
					m_inviteDialog = null;
					FriendChallengeMgr.Get().UpdateMyAvailability();
				});
			}
			break;
		}
		FriendChallengeMgr.Get().UpdateMyAvailability();
	}

	private void BnetParty_OnMemberEvent(OnlineEventType evt, PartyInfo party, BnetGameAccountId memberId, bool isRolesUpdate, LeaveReason? reason)
	{
		if (!ShouldSupportPartyType(party.Type) || party.Id != m_partyData.m_partyId)
		{
			return;
		}
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		Log.Party.PrintDebug("PartyManager.BnetParty_OnMemberEvent() received event {0} for member {1}", evt.ToString(), memberId.ToString());
		if (evt == OnlineEventType.REMOVED && BnetParty.IsInParty(party.Id) && memberId != myGameAccountId)
		{
			FireChangedEvent(PartyInviteEvent.FRIEND_LEFT, memberId);
			BnetGameAccountId leader = GetLeader();
			if (leader == null || leader == memberId)
			{
				LeaveParty();
				FireChangedEvent(PartyInviteEvent.LEADER_DISSOLVED_PARTY, memberId);
			}
		}
		else if (evt == OnlineEventType.ADDED && BnetParty.IsInParty(party.Id) && memberId != myGameAccountId)
		{
			if (IsPartyLeader() && !BnetPresenceMgr.Get().GetGameAccount(memberId).IsOnline())
			{
				KickPlayerFromParty(memberId);
			}
			else
			{
				FireChangedEvent(PartyInviteEvent.FRIEND_ACCEPTED_INVITE, memberId);
			}
		}
	}

	private void BnetParty_OnPartyAttributeChanged(PartyInfo party, string attributeKey, Variant attributeValue)
	{
		if (!ShouldSupportPartyType(party.Type) || m_partyData.m_partyId != party.Id)
		{
			return;
		}
		if (!(attributeKey == "queue"))
		{
			if (attributeKey == "canceled_by" && attributeValue.HasBlobValue)
			{
				BnetId bnetId = ProtobufUtil.ParseFrom<BnetId>(attributeValue.BlobValue);
				BnetGameAccountId bnetGameAccountId = new BnetGameAccountId();
				bnetGameAccountId.SetHi(bnetId.Hi);
				bnetGameAccountId.SetLo(bnetId.Lo);
				BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
				if (!(bnetGameAccountId == myGameAccountId))
				{
					string partyMemberName = GetPartyMemberName(bnetGameAccountId);
					AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
					popupInfo.m_headerText = GameStrings.Get("GLUE_BACON_PRIVATE_PARTY_TITLE");
					popupInfo.m_text = GameStrings.Format("GLUE_BACON_QUEUE_CANCELED", "5ecaf0ff", partyMemberName);
					popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
					popupInfo.m_showAlertIcon = false;
					popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
					popupInfo.m_okText = GameStrings.Get("GLOBAL_OKAY");
					DialogManager.Get().ShowPopup(popupInfo);
				}
			}
		}
		else
		{
			if (attributeValue.HasStringValue && attributeValue.StringValue.Equals("in_queue"))
			{
				Shop.Get().Close(forceClose: true);
				WaitForGame();
			}
			if (attributeValue.HasStringValue && attributeValue.StringValue.Equals("cancel_queue"))
			{
				GameMgr.Get().CancelFindGame();
			}
		}
	}

	private void BnetParty_OnMemberAttributeChanged(PartyInfo party, BnetGameAccountId partyMember, string attributeKey, Variant attributeValue)
	{
		if (ShouldSupportPartyType(party.Type) && !(m_partyData.m_partyId != party.Id))
		{
			Log.Party.PrintDebug("PartyManager.BnetParty_OnMemberAttributeChanged() - Key={0}, Value={1}", attributeKey, attributeValue);
		}
	}

	private void BnetParty_OnSentInvite(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, bool senderIsMyself, InviteRemoveReason? reason)
	{
		if (!ShouldSupportPartyType(party.Type) || m_partyData.m_partyId != party.Id)
		{
			return;
		}
		if (evt == OnlineEventType.ADDED)
		{
			if (inviter != BnetPresenceMgr.Get().GetMyGameAccountId())
			{
				FireChangedEvent(PartyInviteEvent.I_SENT_INVITE, invitee);
			}
			else
			{
				FireChangedEvent(PartyInviteEvent.FRIEND_RECEIVED_INVITE, invitee);
			}
		}
		if (evt != OnlineEventType.REMOVED)
		{
			return;
		}
		InviteRemoveReason? inviteRemoveReason = reason;
		if (inviteRemoveReason.HasValue)
		{
			switch (inviteRemoveReason.GetValueOrDefault())
			{
			case InviteRemoveReason.DECLINED:
				FireChangedEvent(PartyInviteEvent.FRIEND_DECLINED_INVITE, invitee);
				break;
			case InviteRemoveReason.REVOKED:
			case InviteRemoveReason.EXPIRED:
			case InviteRemoveReason.CANCELED:
				FireChangedEvent(PartyInviteEvent.INVITE_EXPIRED, invitee);
				break;
			case InviteRemoveReason.IGNORED:
				break;
			}
		}
	}

	private void BnetParty_OnReceivedInviteRequest(OnlineEventType evt, PartyInfo party, InviteRequest request, InviteRequestRemovedReason? reason)
	{
		if (ShouldSupportPartyType(party.Type))
		{
			_ = m_partyData.m_partyId != party.Id;
		}
	}

	private void FireChangedEvent(PartyInviteEvent challengeEvent, BnetGameAccountId playerGameAccountId)
	{
		ChangedListener[] array = m_changedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(challengeEvent, playerGameAccountId, m_partyData);
		}
	}

	public bool AddChangedListener(ChangedCallback callback)
	{
		return AddChangedListener(callback, null);
	}

	public bool AddChangedListener(ChangedCallback callback, object userData)
	{
		ChangedListener changedListener = new ChangedListener();
		changedListener.SetCallback(callback);
		changedListener.SetUserData(userData);
		if (m_changedListeners.Contains(changedListener))
		{
			return false;
		}
		m_changedListeners.Add(changedListener);
		return true;
	}

	public bool RemoveChangedListener(ChangedCallback callback)
	{
		return RemoveChangedListener(callback, null);
	}

	public bool RemoveChangedListener(ChangedCallback callback, object userData)
	{
		ChangedListener changedListener = new ChangedListener();
		changedListener.SetCallback(callback);
		changedListener.SetUserData(userData);
		return m_changedListeners.Remove(changedListener);
	}
}
