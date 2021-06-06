using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Assets;
using bgs;
using bgs.types;
using Blizzard.T5.Core;
using bnet.protocol;
using Hearthstone;
using Hearthstone.Core;
using PegasusGame;
using PegasusShared;
using SpectatorProto;
using UnityEngine;

public class SpectatorManager
{
	public delegate void InviteReceivedHandler(OnlineEventType evt, BnetPlayer inviter);

	public delegate void InviteSentHandler(OnlineEventType evt, BnetPlayer invitee);

	public delegate void SpectatorToMyGameHandler(OnlineEventType evt, BnetPlayer spectator);

	public delegate void SpectatorModeChangedHandler(OnlineEventType evt, BnetPlayer spectatee);

	private struct ReceivedInvite
	{
		public float m_timestamp;

		public JoinInfo m_joinInfo;

		public ReceivedInvite(JoinInfo joinInfo)
		{
			m_timestamp = Time.realtimeSinceStartup;
			m_joinInfo = joinInfo;
		}
	}

	private class IntendedSpectateeParty
	{
		public BnetGameAccountId SpectateeId;

		public PartyId PartyId;

		public IntendedSpectateeParty(BnetGameAccountId spectateeId, PartyId partyId)
		{
			SpectateeId = spectateeId;
			PartyId = partyId;
		}
	}

	private class PendingSpectatePlayer
	{
		public BnetGameAccountId SpectateeId;

		public JoinInfo JoinInfo;

		public PendingSpectatePlayer(BnetGameAccountId spectateeId, JoinInfo joinInfo)
		{
			SpectateeId = spectateeId;
			JoinInfo = joinInfo;
		}
	}

	public const int MAX_SPECTATORS_PER_SIDE = 10;

	private const float RECEIVED_INVITE_TIMEOUT_SECONDS = 300f;

	private const float SENT_INVITE_TIMEOUT_SECONDS = 30f;

	private const bool SPECTATOR_PARTY_INVITES_USE_RESERVATIONS = false;

	private const float REQUEST_INVITE_TIMEOUT_SECONDS = 5f;

	private const string ALERTPOPUPID_WAITINGFORNEXTGAME = "SPECTATOR_WAITING_FOR_NEXT_GAME";

	private const float ENDGAMESCREEN_AUTO_CLOSE_SECONDS = 5f;

	private static readonly PlatformDependentValue<float> WAITING_FOR_NEXT_GAME_AUTO_LEAVE_SECONDS = new PlatformDependentValue<float>(PlatformCategory.OS)
	{
		iOS = 300f,
		Android = 300f,
		PC = -1f,
		Mac = -1f
	};

	private static readonly PlatformDependentValue<bool> DISABLE_MENU_BUTTON_WHILE_WAITING = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		iOS = true,
		Android = true,
		PC = false,
		Mac = false
	};

	private static SpectatorManager s_instance = null;

	private bool m_initialized;

	private BnetGameAccountId m_spectateeFriendlySide;

	private BnetGameAccountId m_spectateeOpposingSide;

	private PartyId m_spectatorPartyIdMain;

	private PartyId m_spectatorPartyIdOpposingSide;

	private Map<PartyId, BnetGameAccountId> m_knownPartyCreatorIds = new Map<PartyId, BnetGameAccountId>();

	private IntendedSpectateeParty m_requestedInvite;

	private AlertPopup m_waitingForNextGameDialog;

	private HashSet<PartyId> m_leavePartyIdsRequested;

	private PendingSpectatePlayer m_pendingSpectatePlayerAfterLeave;

	private HashSet<BnetGameAccountId> m_userInitiatedOutgoingInvites;

	private HashSet<BnetGameAccountId> m_kickedPlayers;

	private Map<BnetGameAccountId, uint> m_kickedFromSpectatingList;

	private int? m_expectedDisconnectReason;

	private bool m_isExpectingArriveInGameplayAsSpectator;

	private bool m_isShowingRemovedAsSpectatorPopup;

	private HashSet<BnetGameAccountId> m_gameServerKnownSpectators = new HashSet<BnetGameAccountId>();

	private Map<BnetGameAccountId, ReceivedInvite> m_receivedSpectateMeInvites = new Map<BnetGameAccountId, ReceivedInvite>();

	private Map<BnetGameAccountId, float> m_sentSpectateMeInvites = new Map<BnetGameAccountId, float>();

	public bool IsSpectatingOrWatching
	{
		get
		{
			if (GameMgr.Get() != null && GameMgr.Get().IsSpectator())
			{
				return true;
			}
			if (IsInSpectatorMode())
			{
				return true;
			}
			return false;
		}
	}

	private static bool IsGameOver
	{
		get
		{
			if (GameState.Get() == null)
			{
				return false;
			}
			if (GameState.Get().IsGameOverNowOrPending())
			{
				return true;
			}
			return false;
		}
	}

	public event InviteReceivedHandler OnInviteReceived;

	public event InviteSentHandler OnInviteSent;

	public event Action OnSpectateRejected;

	public event SpectatorToMyGameHandler OnSpectatorToMyGame;

	public event SpectatorModeChangedHandler OnSpectatorModeChanged;

	public static SpectatorManager Get()
	{
		if (s_instance == null && SceneMgr.Get() != null)
		{
			CreateInstance();
		}
		return s_instance;
	}

	public static bool InstanceExists()
	{
		return s_instance != null;
	}

	public static JoinInfo GetSpectatorJoinInfo(BnetGameAccount gameAccount)
	{
		if (gameAccount == null)
		{
			return null;
		}
		byte[] gameFieldBytes = gameAccount.GetGameFieldBytes(21u);
		if (gameFieldBytes != null && gameFieldBytes.Length != 0)
		{
			return ProtobufUtil.ParseFrom<JoinInfo>(gameFieldBytes);
		}
		gameFieldBytes = gameAccount.GetGameFieldBytes(23u);
		if (gameFieldBytes != null && gameFieldBytes.Length != 0)
		{
			try
			{
				SecretJoinInfo secretJoinInfo = ProtobufUtil.ParseFrom<SecretJoinInfo>(gameFieldBytes);
				if (secretJoinInfo != null)
				{
					byte[] array = null;
					SecretSource source = secretJoinInfo.Source;
					if (source == SecretSource.SECRET_SOURCE_FIRESIDE_GATHERING && secretJoinInfo.HasSpecificSourceIdentity && secretJoinInfo.SpecificSourceIdentity == FiresideGatheringManager.Get().CurrentFsgId)
					{
						array = FiresideGatheringManager.Get().CurrentFsgSharedSecretKey;
					}
					if (array != null)
					{
						byte[] secretKey = SHA256.Create().ComputeHash(array, 0, array.Length);
						gameFieldBytes = Crypto.Rijndael.Decrypt(secretJoinInfo.EncryptedMessage, secretKey);
						return ProtobufUtil.ParseFrom<JoinInfo>(gameFieldBytes);
					}
				}
			}
			catch (Exception ex)
			{
				Log.All.PrintError("{0} parsing/decrypting secret JoinInfo, isInFsg={1}: {2}", ex.GetType().Name, FiresideGatheringManager.Get().IsCheckedIn, ex.ToString());
			}
		}
		JoinInfo spectatorJoinInfoForPlayer = PartyManager.Get().GetSpectatorJoinInfoForPlayer(gameAccount.GetId());
		if (spectatorJoinInfoForPlayer != null)
		{
			return spectatorJoinInfoForPlayer;
		}
		return null;
	}

	public static int GetSpectatorGameHandleFromPlayer(BnetPlayer player)
	{
		return GetSpectatorJoinInfo(player.GetHearthstoneGameAccount())?.GameHandle ?? (-1);
	}

	public static bool IsSpectatorSlotAvailable(JoinInfo info)
	{
		if (info == null)
		{
			return false;
		}
		if (!info.HasPartyId)
		{
			if (!info.HasServerIpAddress || !info.HasSecretKey)
			{
				return false;
			}
			if (string.IsNullOrEmpty(info.SecretKey))
			{
				return false;
			}
		}
		if (info.HasIsJoinable && !info.IsJoinable)
		{
			return false;
		}
		if (info.HasMaxNumSpectators && info.HasCurrentNumSpectators && info.CurrentNumSpectators >= info.MaxNumSpectators)
		{
			return false;
		}
		return true;
	}

	public static bool IsSpectatorSlotAvailable(BnetGameAccount gameAccount)
	{
		return IsSpectatorSlotAvailable(GetSpectatorJoinInfo(gameAccount));
	}

	public void InitializeConnectedToBnet()
	{
		if (!m_initialized)
		{
			m_initialized = true;
			PartyInfo[] joinedParties = BnetParty.GetJoinedParties();
			foreach (PartyInfo party in joinedParties)
			{
				BnetParty_OnJoined(OnlineEventType.ADDED, party, null);
			}
			PartyInvite[] receivedInvites = BnetParty.GetReceivedInvites();
			foreach (PartyInvite partyInvite in receivedInvites)
			{
				BnetParty_OnReceivedInvite(OnlineEventType.ADDED, new PartyInfo(partyInvite.PartyId, partyInvite.PartyType), partyInvite.InviteId, null, null, null);
			}
		}
	}

	private bool IsInSpectableContextWithPlayer(BnetGameAccountId gameAccountId)
	{
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(gameAccountId);
		return IsInSpectableContextWithPlayer(player);
	}

	private bool IsInSpectableContextWithPlayer(BnetPlayer player)
	{
		if (player == null)
		{
			return false;
		}
		if (BnetFriendMgr.Get().IsFriend(player))
		{
			return true;
		}
		if (FiresideGatheringManager.Get().IsPlayerInMyFSG(player))
		{
			return true;
		}
		if (PartyManager.Get().IsPlayerInCurrentPartyOrPending(player.GetBestGameAccountId()))
		{
			return true;
		}
		return false;
	}

	public bool CanSpectate(BnetPlayer player)
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (player == myPlayer)
		{
			return false;
		}
		if (!IsInSpectableContextWithPlayer(player))
		{
			return false;
		}
		BnetGameAccount hearthstoneGameAccount = player.GetHearthstoneGameAccount();
		BnetGameAccount hearthstoneGameAccount2 = myPlayer.GetHearthstoneGameAccount();
		if (hearthstoneGameAccount == null || hearthstoneGameAccount2 == null || !hearthstoneGameAccount.IsOnline() || !hearthstoneGameAccount2.IsOnline())
		{
			return false;
		}
		if (HearthstoneApplication.IsPublic() && (string.Compare(hearthstoneGameAccount.GetClientVersion(), hearthstoneGameAccount2.GetClientVersion()) != 0 || string.Compare(hearthstoneGameAccount.GetClientEnv(), hearthstoneGameAccount2.GetClientEnv()) != 0))
		{
			return false;
		}
		BnetGameAccountId hearthstoneGameAccountId = player.GetHearthstoneGameAccountId();
		JoinInfo spectatorJoinInfo = GetSpectatorJoinInfo(hearthstoneGameAccount);
		return CanSpectate(hearthstoneGameAccountId, spectatorJoinInfo);
	}

	public bool CanSpectate(BnetGameAccountId gameAccountId, JoinInfo joinInfo)
	{
		if (IsSpectatingPlayer(gameAccountId))
		{
			return false;
		}
		if (m_spectateeOpposingSide != null)
		{
			return false;
		}
		if (HasPreviouslyKickedMeFromGame(gameAccountId, joinInfo?.GameHandle ?? (-1)) && !HasInvitedMeToSpectate(gameAccountId))
		{
			return false;
		}
		if (GameMgr.Get().IsFindingGame())
		{
			return false;
		}
		if (GameMgr.Get().IsNextSpectator())
		{
			return false;
		}
		if (FriendChallengeMgr.Get().HasChallenge())
		{
			return false;
		}
		if (!IsSpectatorSlotAvailable(joinInfo) && !HasInvitedMeToSpectate(gameAccountId))
		{
			return false;
		}
		if (GameMgr.Get().IsSpectator())
		{
			if (!IsPlayerInGame(gameAccountId))
			{
				return false;
			}
		}
		else if (SceneMgr.Get().IsInGame())
		{
			return false;
		}
		if (!GameUtils.AreAllTutorialsComplete())
		{
			return false;
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN)
		{
			return false;
		}
		if (BnetPresenceMgr.Get().GetMyPlayer().IsAppearingOffline())
		{
			return false;
		}
		if (PartyManager.Get().IsInParty() && !PartyManager.Get().IsPlayerInCurrentPartyOrPending(gameAccountId))
		{
			return false;
		}
		return true;
	}

	public bool IsInSpectatorMode()
	{
		if (m_spectateeFriendlySide == null)
		{
			return false;
		}
		if (m_spectatorPartyIdMain == null)
		{
			return false;
		}
		if (!IsStillInParty(m_spectatorPartyIdMain))
		{
			return false;
		}
		if (!m_initialized)
		{
			return false;
		}
		if (GetPartyCreator(m_spectatorPartyIdMain) == null)
		{
			return false;
		}
		if (ShouldBePartyLeader(m_spectatorPartyIdMain))
		{
			return false;
		}
		return true;
	}

	public bool ShouldBeSpectatingInGame()
	{
		if (m_spectatorPartyIdMain == null)
		{
			return false;
		}
		if (BnetParty.GetPartyAttributeBlob(m_spectatorPartyIdMain, "WTCG.Party.ServerInfo") == null)
		{
			return false;
		}
		return true;
	}

	public bool IsSpectatingPlayer(BnetGameAccountId gameAccountId)
	{
		if (m_spectateeFriendlySide != null && m_spectateeFriendlySide == gameAccountId)
		{
			return true;
		}
		if (m_spectateeOpposingSide != null && m_spectateeOpposingSide == gameAccountId)
		{
			return true;
		}
		return false;
	}

	public bool IsSpectatingMe(BnetGameAccountId gameAccountId)
	{
		if (IsInSpectatorMode())
		{
			return false;
		}
		if (m_gameServerKnownSpectators.Contains(gameAccountId))
		{
			return true;
		}
		if (gameAccountId != BnetPresenceMgr.Get().GetMyGameAccountId() && BnetParty.IsMember(m_spectatorPartyIdMain, gameAccountId))
		{
			return true;
		}
		return false;
	}

	public int GetCountSpectatingMe()
	{
		if (m_spectatorPartyIdMain != null && !ShouldBePartyLeader(m_spectatorPartyIdMain))
		{
			return 0;
		}
		int count = m_gameServerKnownSpectators.Count;
		return Mathf.Max(BnetParty.CountMembers(m_spectatorPartyIdMain) - 1, count);
	}

	public bool IsBeingSpectated()
	{
		return GetCountSpectatingMe() > 0;
	}

	public BnetGameAccountId[] GetSpectatorPartyMembers(bool friendlySide = true, bool includeSelf = false)
	{
		List<BnetGameAccountId> list = new List<BnetGameAccountId>(m_gameServerKnownSpectators);
		bgs.PartyMember[] members = BnetParty.GetMembers(friendlySide ? m_spectatorPartyIdMain : m_spectatorPartyIdOpposingSide);
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		bgs.PartyMember[] array = members;
		foreach (bgs.PartyMember partyMember in array)
		{
			if ((includeSelf || partyMember.GameAccountId != myGameAccountId) && !list.Contains(partyMember.GameAccountId))
			{
				list.Add(partyMember.GameAccountId);
			}
		}
		return list.ToArray();
	}

	public bool IsInSpectatableGame()
	{
		if (!SceneMgr.Get().IsInGame())
		{
			return false;
		}
		if (GameMgr.Get().IsSpectator())
		{
			return false;
		}
		if (IsGameOver)
		{
			return false;
		}
		return true;
	}

	public bool IsInSpectatableScene()
	{
		return IsInSpectatableScene(alsoCheckRequestedScene: false);
	}

	private bool IsInSpectatableScene(bool alsoCheckRequestedScene)
	{
		if (SceneMgr.Get().IsInGame())
		{
			return true;
		}
		if (IsSpectatableScene(SceneMgr.Get().GetMode()))
		{
			return true;
		}
		if (alsoCheckRequestedScene && IsSpectatableScene(SceneMgr.Get().GetNextMode()))
		{
			return true;
		}
		return false;
	}

	private static bool IsSpectatableScene(SceneMgr.Mode sceneMode)
	{
		if (sceneMode == SceneMgr.Mode.GAMEPLAY)
		{
			return true;
		}
		return false;
	}

	public bool CanAddSpectators()
	{
		if (GameMgr.Get() != null && GameMgr.Get().IsSpectator())
		{
			return false;
		}
		if (m_spectateeFriendlySide != null || m_spectateeOpposingSide != null)
		{
			return false;
		}
		int countSpectatingMe = GetCountSpectatingMe();
		if (!IsInSpectatableGame())
		{
			if (m_spectatorPartyIdMain == null)
			{
				return false;
			}
			if (countSpectatingMe <= 0)
			{
				return false;
			}
		}
		if (countSpectatingMe >= 10)
		{
			return false;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer != null && myPlayer.IsAppearingOffline())
		{
			return false;
		}
		return true;
	}

	public bool CanInviteToSpectateMyGame(BnetGameAccountId gameAccountId)
	{
		if (!CanAddSpectators())
		{
			return false;
		}
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		if (gameAccountId == myGameAccountId)
		{
			return false;
		}
		if (!IsInSpectableContextWithPlayer(gameAccountId))
		{
			return false;
		}
		if (IsSpectatingMe(gameAccountId))
		{
			return false;
		}
		if (IsInvitedToSpectateMyGame(gameAccountId))
		{
			return false;
		}
		if (PartyManager.Get().IsPlayerInAnyParty(gameAccountId))
		{
			return false;
		}
		BnetGameAccount gameAccount = BnetPresenceMgr.Get().GetGameAccount(gameAccountId);
		if (gameAccount == null || !gameAccount.IsOnline())
		{
			return false;
		}
		if (!gameAccount.CanBeInvitedToGame() && !IsPlayerSpectatingMyGamesOpposingSide(gameAccountId))
		{
			return false;
		}
		BnetGameAccount hearthstoneGameAccount = BnetPresenceMgr.Get().GetMyPlayer().GetHearthstoneGameAccount();
		if (string.Compare(gameAccount.GetClientVersion(), hearthstoneGameAccount.GetClientVersion()) != 0)
		{
			return false;
		}
		if (HearthstoneApplication.IsPublic() && string.Compare(gameAccount.GetClientEnv(), hearthstoneGameAccount.GetClientEnv()) != 0)
		{
			return false;
		}
		if (!SceneMgr.Get().IsInGame())
		{
			return false;
		}
		return true;
	}

	public bool IsPlayerSpectatingMyGamesOpposingSide(BnetGameAccountId gameAccountId)
	{
		BnetGameAccount gameAccount = BnetPresenceMgr.Get().GetGameAccount(gameAccountId);
		if (gameAccount == null)
		{
			return false;
		}
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		bool result = false;
		if (IsInSpectableContextWithPlayer(gameAccountId))
		{
			JoinInfo spectatorJoinInfo = GetSpectatorJoinInfo(gameAccount);
			Map<int, Player>.ValueCollection valueCollection = ((GameState.Get() == null) ? null : GameState.Get().GetPlayerMap().Values);
			if (spectatorJoinInfo != null && spectatorJoinInfo.SpectatedPlayers.Count > 0 && valueCollection != null && valueCollection.Count > 0)
			{
				for (int i = 0; i < spectatorJoinInfo.SpectatedPlayers.Count; i++)
				{
					BnetGameAccountId spectatedPlayerId = BnetGameAccountId.CreateFromNet(spectatorJoinInfo.SpectatedPlayers[i]);
					if (spectatedPlayerId != myGameAccountId && valueCollection.Any((Player p) => p.GetGameAccountId() == spectatedPlayerId))
					{
						result = true;
						break;
					}
				}
			}
		}
		return result;
	}

	public bool IsInvitedToSpectateMyGame(BnetGameAccountId gameAccountId)
	{
		if (BnetParty.GetSentInvites(m_spectatorPartyIdMain).FirstOrDefault((PartyInvite i) => i.InviteeId == gameAccountId) != null)
		{
			return true;
		}
		return false;
	}

	public bool CanKickSpectator(BnetGameAccountId gameAccountId)
	{
		if (!IsSpectatingMe(gameAccountId))
		{
			return false;
		}
		return true;
	}

	public bool HasInvitedMeToSpectate(BnetGameAccountId gameAccountId)
	{
		if (BnetParty.GetReceivedInviteFrom(gameAccountId, PartyType.SPECTATOR_PARTY) != null)
		{
			return true;
		}
		return false;
	}

	public bool HasAnyReceivedInvites()
	{
		return (from i in BnetParty.GetReceivedInvites()
			where i.PartyType == PartyType.SPECTATOR_PARTY
			select i).ToArray().Length != 0;
	}

	public bool MyGameHasSpectators()
	{
		if (!SceneMgr.Get().IsInGame())
		{
			return false;
		}
		return m_gameServerKnownSpectators.Count > 0;
	}

	public BnetGameAccountId GetSpectateeFriendlySide()
	{
		return m_spectateeFriendlySide;
	}

	public BnetGameAccountId GetSpectateeOpposingSide()
	{
		return m_spectateeOpposingSide;
	}

	public bool IsSpectatingOpposingSide()
	{
		return m_spectateeOpposingSide != null;
	}

	public bool HasPreviouslyKickedMeFromGame(BnetGameAccountId playerId, int currentGameHandle)
	{
		if (m_kickedFromSpectatingList == null)
		{
			return false;
		}
		uint value = 0u;
		if (m_kickedFromSpectatingList.TryGetValue(playerId, out value))
		{
			if (value == currentGameHandle)
			{
				return true;
			}
			m_kickedFromSpectatingList.Remove(playerId);
			if (m_kickedFromSpectatingList.Count == 0)
			{
				m_kickedFromSpectatingList = null;
			}
		}
		return false;
	}

	public void SpectatePlayer(BnetPlayer player)
	{
		if (CanSpectate(player))
		{
			BnetGameAccountId hearthstoneGameAccountId = player.GetHearthstoneGameAccountId();
			JoinInfo spectatorJoinInfo = GetSpectatorJoinInfo(player.GetHearthstoneGameAccount());
			SpectatePlayer(hearthstoneGameAccountId, spectatorJoinInfo);
		}
	}

	public void SpectatePlayer(BnetGameAccountId gameAccountId, JoinInfo joinInfo)
	{
		if (m_pendingSpectatePlayerAfterLeave != null)
		{
			return;
		}
		PartyInvite receivedInviteFrom = BnetParty.GetReceivedInviteFrom(gameAccountId, PartyType.SPECTATOR_PARTY);
		if (receivedInviteFrom != null)
		{
			if (m_spectateeFriendlySide == null || (m_spectateeOpposingSide == null && IsPlayerInGame(gameAccountId)))
			{
				CloseWaitingForNextGameDialog();
				if (m_spectateeFriendlySide != null && gameAccountId != m_spectateeFriendlySide)
				{
					m_spectateeOpposingSide = gameAccountId;
				}
				BnetParty.AcceptReceivedInvite(receivedInviteFrom.InviteId);
				HideShownUI();
			}
			else
			{
				LogInfoParty("SpectatePlayer: trying to accept an invite even though there is no room for another spectatee: player={0} spectatee1={1} spectatee2={2} isPlayerInGame={3} inviteId={4}", string.Concat(gameAccountId, " (", BnetUtils.GetPlayerBestName(gameAccountId), ")"), m_spectateeFriendlySide, m_spectateeOpposingSide, IsPlayerInGame(gameAccountId), receivedInviteFrom.InviteId);
				BnetParty.DeclineReceivedInvite(receivedInviteFrom.InviteId);
			}
			return;
		}
		if (joinInfo == null)
		{
			Error.AddWarningLoc("Bad Spectator Key", "Spectator key is blank!");
			return;
		}
		if (!joinInfo.HasPartyId && string.IsNullOrEmpty(joinInfo.SecretKey))
		{
			Error.AddWarningLoc("No Party/Bad Spectator Key", "No party information and Spectator key is blank!");
			return;
		}
		if (joinInfo.HasPartyId && m_requestedInvite != null)
		{
			LogInfoParty("SpectatePlayer: already requesting invite from {0}:party={1}, cannot request another from {2}:party={3}", m_requestedInvite.SpectateeId, m_requestedInvite.PartyId, gameAccountId, BnetUtils.CreatePartyId(joinInfo.PartyId));
			return;
		}
		HideShownUI();
		if (!(m_spectateeFriendlySide != null) || !(m_spectateeOpposingSide == null) || GameMgr.Get() == null || !GameMgr.Get().IsSpectator())
		{
			if (m_spectatorPartyIdMain != null)
			{
				if (IsInSpectatorMode())
				{
					EndSpectatorMode(wasKnownSpectating: true);
				}
				else
				{
					LeaveParty(m_spectatorPartyIdMain, ShouldBePartyLeader(m_spectatorPartyIdMain));
				}
				m_pendingSpectatePlayerAfterLeave = new PendingSpectatePlayer(gameAccountId, joinInfo);
				return;
			}
			if (m_spectatorPartyIdOpposingSide != null)
			{
				m_pendingSpectatePlayerAfterLeave = new PendingSpectatePlayer(gameAccountId, joinInfo);
				LeaveParty(m_spectatorPartyIdOpposingSide, dissolve: false);
				return;
			}
		}
		SpectatePlayer_Internal(gameAccountId, joinInfo);
	}

	private void HideShownUI()
	{
		ShownUIMgr shownUIMgr = ShownUIMgr.Get();
		if (shownUIMgr == null)
		{
			return;
		}
		switch (shownUIMgr.GetShownUI())
		{
		case ShownUIMgr.UI_WINDOW.GENERAL_STORE:
			if (GeneralStore.Get() != null)
			{
				GeneralStore.Get().Close(closeWithAnimation: false);
			}
			break;
		case ShownUIMgr.UI_WINDOW.QUEST_LOG:
			if (QuestLog.Get() != null)
			{
				QuestLog.Get().Hide();
			}
			break;
		case ShownUIMgr.UI_WINDOW.ARENA_STORE:
			if (ArenaStore.Get() != null)
			{
				ArenaStore.Get().Hide();
			}
			break;
		case ShownUIMgr.UI_WINDOW.TAVERN_BRAWL_STORE:
			if (TavernBrawlStore.Get() != null)
			{
				TavernBrawlStore.Get().Hide();
			}
			break;
		}
	}

	private void FireSpectatorModeChanged(OnlineEventType evt, BnetPlayer spectatee)
	{
		if (FriendChallengeMgr.Get() != null)
		{
			FriendChallengeMgr.Get().UpdateMyAvailability();
		}
		if (this.OnSpectatorModeChanged != null)
		{
			this.OnSpectatorModeChanged(evt, spectatee);
		}
		switch (evt)
		{
		case OnlineEventType.ADDED:
			Screen.sleepTimeout = -1;
			break;
		case OnlineEventType.REMOVED:
			if (SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
			{
				Screen.sleepTimeout = -2;
			}
			break;
		}
	}

	private void SpectatePlayer_Internal(BnetGameAccountId gameAccountId, JoinInfo joinInfo)
	{
		if (!m_initialized)
		{
			LogInfoParty("ERROR: SpectatePlayer_Internal called before initialized; spectatee={0}", gameAccountId);
		}
		m_pendingSpectatePlayerAfterLeave = null;
		if (WelcomeQuests.Get() != null)
		{
			WelcomeQuests.Hide();
		}
		PartyInvite receivedInviteFrom = BnetParty.GetReceivedInviteFrom(gameAccountId, PartyType.SPECTATOR_PARTY);
		if (m_spectateeFriendlySide == null)
		{
			LogInfoPower("================== Begin Spectating 1st player ==================");
			m_spectateeFriendlySide = gameAccountId;
			if (receivedInviteFrom != null)
			{
				CloseWaitingForNextGameDialog();
				BnetParty.AcceptReceivedInvite(receivedInviteFrom.InviteId);
			}
			else if (joinInfo.HasPartyId)
			{
				PartyId partyId = BnetUtils.CreatePartyId(joinInfo.PartyId);
				m_requestedInvite = new IntendedSpectateeParty(gameAccountId, partyId);
				BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
				BnetParty.RequestInvite(partyId, gameAccountId, myGameAccountId, PartyType.SPECTATOR_PARTY);
				Processor.ScheduleCallback(5f, realTime: true, SpectatePlayer_RequestInvite_FriendlySide_Timeout);
			}
			else
			{
				CloseWaitingForNextGameDialog();
				m_isExpectingArriveInGameplayAsSpectator = true;
				GameMgr.Get().SpectateGame(joinInfo);
			}
		}
		else if (m_spectateeOpposingSide == null)
		{
			if (!IsPlayerInGame(gameAccountId))
			{
				Error.AddWarning(GameStrings.Get("GLOBAL_ERROR_GENERIC_HEADER"), GameStrings.Get("GLOBAL_SPECTATOR_ERROR_CANNOT_SPECTATE_2_GAMES"));
				return;
			}
			if (m_spectateeFriendlySide == gameAccountId)
			{
				LogInfoParty("SpectatePlayer: already spectating player {0}", gameAccountId);
				if (receivedInviteFrom != null)
				{
					BnetParty.AcceptReceivedInvite(receivedInviteFrom.InviteId);
				}
				return;
			}
			LogInfoPower("================== Begin Spectating 2nd player ==================");
			m_spectateeOpposingSide = gameAccountId;
			if (receivedInviteFrom != null)
			{
				BnetParty.AcceptReceivedInvite(receivedInviteFrom.InviteId);
			}
			else if (joinInfo.HasPartyId)
			{
				PartyId partyId2 = BnetUtils.CreatePartyId(joinInfo.PartyId);
				m_requestedInvite = new IntendedSpectateeParty(gameAccountId, partyId2);
				BnetGameAccountId myGameAccountId2 = BnetPresenceMgr.Get().GetMyGameAccountId();
				BnetParty.RequestInvite(partyId2, gameAccountId, myGameAccountId2, PartyType.SPECTATOR_PARTY);
				Processor.ScheduleCallback(5f, realTime: true, SpectatePlayer_RequestInvite_OpposingSide_Timeout);
			}
			else
			{
				SpectateSecondPlayer_Network(joinInfo);
			}
		}
		else if (m_spectateeFriendlySide == gameAccountId || m_spectateeOpposingSide == gameAccountId)
		{
			LogInfoParty("SpectatePlayer: already spectating player {0}", gameAccountId);
		}
		else
		{
			Error.AddDevFatal("Cannot spectate more than 2 players.");
		}
	}

	private void SpectatePlayer_RequestInvite_FriendlySide_Timeout(object userData)
	{
		if (m_requestedInvite != null)
		{
			m_spectateeFriendlySide = null;
			EndSpectatorMode(wasKnownSpectating: true);
			string header = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_HEADER");
			string body = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_TEXT");
			DisplayErrorDialog(header, body);
			if (this.OnSpectateRejected != null)
			{
				this.OnSpectateRejected();
			}
		}
	}

	private void SpectatePlayer_RequestInvite_OpposingSide_Timeout(object userData)
	{
		if (m_requestedInvite != null)
		{
			m_requestedInvite = null;
			m_spectateeOpposingSide = null;
			string header = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_HEADER");
			string body = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_TEXT");
			DisplayErrorDialog(header, body);
			if (this.OnSpectateRejected != null)
			{
				this.OnSpectateRejected();
			}
		}
	}

	private static JoinInfo CreateJoinInfo(PartyServerInfo serverInfo)
	{
		JoinInfo joinInfo = new JoinInfo();
		joinInfo.ServerIpAddress = serverInfo.ServerIpAddress;
		joinInfo.ServerPort = serverInfo.ServerPort;
		joinInfo.GameHandle = serverInfo.GameHandle;
		joinInfo.SecretKey = serverInfo.SecretKey;
		if (serverInfo.HasGameType)
		{
			joinInfo.GameType = serverInfo.GameType;
		}
		if (serverInfo.HasFormatType)
		{
			joinInfo.FormatType = serverInfo.FormatType;
		}
		if (serverInfo.HasMissionId)
		{
			joinInfo.MissionId = serverInfo.MissionId;
		}
		return joinInfo;
	}

	private static bool IsSameGameAndServer(PartyServerInfo a, PartyServerInfo b)
	{
		if (a == null)
		{
			return b == null;
		}
		if (b == null)
		{
			return false;
		}
		if (a.ServerIpAddress == b.ServerIpAddress)
		{
			return a.GameHandle == b.GameHandle;
		}
		return false;
	}

	private static bool IsSameGameAndServer(PartyServerInfo a, GameServerInfo b)
	{
		if (a == null)
		{
			return b == null;
		}
		if (b == null)
		{
			return false;
		}
		if (a.ServerIpAddress == b.Address)
		{
			return a.GameHandle == b.GameHandle;
		}
		return false;
	}

	private void SpectateSecondPlayer_Network(JoinInfo joinInfo)
	{
		GameServerInfo gameServerInfo = new GameServerInfo();
		gameServerInfo.Address = joinInfo.ServerIpAddress;
		gameServerInfo.Port = joinInfo.ServerPort;
		gameServerInfo.GameHandle = (uint)joinInfo.GameHandle;
		gameServerInfo.SpectatorPassword = joinInfo.SecretKey;
		gameServerInfo.SpectatorMode = true;
		Network.Get().SpectateSecondPlayer(gameServerInfo);
	}

	private void JoinPartyGame(PartyId partyId)
	{
		if (!(partyId == null))
		{
			PartyInfo joinedParty = BnetParty.GetJoinedParty(partyId);
			if (joinedParty != null)
			{
				BnetParty_OnPartyAttributeChanged_ServerInfo(joinedParty, "WTCG.Party.ServerInfo", BnetParty.GetPartyAttributeVariant(partyId, "WTCG.Party.ServerInfo"));
			}
		}
	}

	public void LeaveSpectatorMode()
	{
		if (GameMgr.Get().IsSpectator())
		{
			if (Network.Get().IsConnectedToGameServer())
			{
				Network.Get().DisconnectFromGameServer();
			}
			else
			{
				LeaveGameScene();
			}
		}
		if (m_spectatorPartyIdOpposingSide != null)
		{
			LeaveParty(m_spectatorPartyIdOpposingSide, dissolve: false);
		}
		if (m_spectatorPartyIdMain != null)
		{
			LeaveParty(m_spectatorPartyIdMain, dissolve: false);
		}
	}

	public void InviteToSpectateMe(BnetPlayer player)
	{
		if (player == null)
		{
			return;
		}
		BnetGameAccountId hearthstoneGameAccountId = player.GetHearthstoneGameAccountId();
		if (m_kickedPlayers != null && m_kickedPlayers.Contains(hearthstoneGameAccountId))
		{
			m_kickedPlayers.Remove(hearthstoneGameAccountId);
		}
		if (CanInviteToSpectateMyGame(hearthstoneGameAccountId))
		{
			if (m_userInitiatedOutgoingInvites == null)
			{
				m_userInitiatedOutgoingInvites = new HashSet<BnetGameAccountId>();
			}
			m_userInitiatedOutgoingInvites.Add(hearthstoneGameAccountId);
			if (m_spectatorPartyIdMain == null)
			{
				byte[] creatorBlob = ProtobufUtil.ToByteArray(BnetUtils.CreatePegasusBnetId(BnetPresenceMgr.Get().GetMyGameAccountId()));
				BnetParty.CreateParty(PartyType.SPECTATOR_PARTY, PrivacyLevel.OPEN_INVITATION, creatorBlob, null);
			}
			else
			{
				BnetParty.SendInvite(m_spectatorPartyIdMain, hearthstoneGameAccountId, isReservation: false);
			}
		}
	}

	public void KickSpectator(BnetPlayer player, bool regenerateSpectatorPassword)
	{
		KickSpectator_Internal(player, regenerateSpectatorPassword, addToKickList: true);
	}

	private void KickSpectator_Internal(BnetPlayer player, bool regenerateSpectatorPassword, bool addToKickList)
	{
		if (player == null)
		{
			return;
		}
		BnetGameAccountId hearthstoneGameAccountId = player.GetHearthstoneGameAccountId();
		if (!CanKickSpectator(hearthstoneGameAccountId))
		{
			return;
		}
		if (addToKickList)
		{
			if (m_kickedPlayers == null)
			{
				m_kickedPlayers = new HashSet<BnetGameAccountId>();
			}
			m_kickedPlayers.Add(hearthstoneGameAccountId);
		}
		if (Network.Get().IsConnectedToGameServer())
		{
			Network.Get().SendRemoveSpectators(regenerateSpectatorPassword, hearthstoneGameAccountId);
		}
		if (m_spectatorPartyIdMain != null && ShouldBePartyLeader(m_spectatorPartyIdMain) && BnetParty.IsMember(m_spectatorPartyIdMain, hearthstoneGameAccountId))
		{
			BnetParty.KickMember(m_spectatorPartyIdMain, hearthstoneGameAccountId);
		}
	}

	public void UpdateMySpectatorInfo()
	{
		UpdateSpectatorPresence();
		UpdateSpectatorPartyServerInfo();
	}

	private JoinInfo GetMyGameJoinInfo()
	{
		JoinInfo result = null;
		JoinInfo joinInfo = new JoinInfo();
		if (IsInSpectatorMode())
		{
			if (m_spectateeFriendlySide != null)
			{
				BnetId item = BnetUtils.CreatePegasusBnetId(m_spectateeFriendlySide);
				joinInfo.SpectatedPlayers.Add(item);
			}
			if (m_spectateeOpposingSide != null)
			{
				BnetId item2 = BnetUtils.CreatePegasusBnetId(m_spectateeOpposingSide);
				joinInfo.SpectatedPlayers.Add(item2);
			}
			if (joinInfo.SpectatedPlayers.Count > 0)
			{
				result = joinInfo;
			}
		}
		else if (SceneMgr.Get().IsInGame())
		{
			int countSpectatingMe = GetCountSpectatingMe();
			if (CanAddSpectators())
			{
				GameServerInfo lastGameServerJoined = Network.Get().GetLastGameServerJoined();
				if (m_spectatorPartyIdMain == null && lastGameServerJoined != null && SceneMgr.Get().IsInGame() && !IsGameOver)
				{
					joinInfo.ServerIpAddress = lastGameServerJoined.Address;
					joinInfo.ServerPort = lastGameServerJoined.Port;
					joinInfo.GameHandle = (int)lastGameServerJoined.GameHandle;
					joinInfo.SecretKey = lastGameServerJoined.SpectatorPassword ?? "";
				}
				if (m_spectatorPartyIdMain != null)
				{
					BnetId bnetId2 = (joinInfo.PartyId = BnetUtils.CreatePegasusBnetId(m_spectatorPartyIdMain));
					joinInfo.GameHandle = (int)lastGameServerJoined.GameHandle;
				}
			}
			joinInfo.CurrentNumSpectators = countSpectatingMe;
			joinInfo.MaxNumSpectators = 10;
			joinInfo.IsJoinable = joinInfo.CurrentNumSpectators < joinInfo.MaxNumSpectators;
			joinInfo.GameType = GameMgr.Get().GetGameType();
			joinInfo.FormatType = GameMgr.Get().GetFormatType();
			joinInfo.MissionId = GameMgr.Get().GetMissionId();
			result = joinInfo;
		}
		return result;
	}

	private static PartyServerInfo GetPartyServerInfo(PartyId partyId)
	{
		byte[] partyAttributeBlob = BnetParty.GetPartyAttributeBlob(partyId, "WTCG.Party.ServerInfo");
		if (partyAttributeBlob != null)
		{
			return ProtobufUtil.ParseFrom<PartyServerInfo>(partyAttributeBlob);
		}
		return null;
	}

	public bool HandleDisconnectFromGameplay()
	{
		bool hasValue = m_expectedDisconnectReason.HasValue;
		EndCurrentSpectatedGame(isLeavingGameplay: false);
		if (hasValue)
		{
			if (GameMgr.Get().IsTransitionPopupShown())
			{
				GameMgr.Get().GetTransitionPopup().Cancel();
				return hasValue;
			}
			LeaveGameScene();
		}
		return hasValue;
	}

	public void OnRealTimeGameOver()
	{
		UpdateMySpectatorInfo();
	}

	private void EndCurrentSpectatedGame(bool isLeavingGameplay)
	{
		if (isLeavingGameplay && IsInSpectatorMode())
		{
			SoundManager.Get().LoadAndPlay("SpectatorMode_Exit.prefab:f1d7dab96facdc64fb6648ff1dd22073");
		}
		m_expectedDisconnectReason = null;
		m_isExpectingArriveInGameplayAsSpectator = false;
		ClearAllGameServerKnownSpectators();
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null && !hearthstoneApplication.IsResetting())
		{
			UpdateSpectatorPresence();
		}
		if (GameMgr.Get() != null && GameMgr.Get().GetTransitionPopup() != null)
		{
			GameMgr.Get().GetTransitionPopup().OnHidden -= EnterSpectatorMode_OnTransitionPopupHide;
		}
	}

	private void EndSpectatorMode(bool wasKnownSpectating = false)
	{
		bool isExpectingArriveInGameplayAsSpectator = m_isExpectingArriveInGameplayAsSpectator;
		bool num = wasKnownSpectating || m_spectateeFriendlySide != null || m_spectateeOpposingSide != null;
		LeaveSpectatorMode();
		EndCurrentSpectatedGame(isLeavingGameplay: false);
		m_spectateeFriendlySide = null;
		m_spectateeOpposingSide = null;
		m_requestedInvite = null;
		CloseWaitingForNextGameDialog();
		m_pendingSpectatePlayerAfterLeave = null;
		m_isExpectingArriveInGameplayAsSpectator = false;
		if (num)
		{
			LogInfoPower("================== End Spectator Mode ==================");
			BnetPlayer player = BnetUtils.GetPlayer(m_spectateeFriendlySide);
			FireSpectatorModeChanged(OnlineEventType.REMOVED, player);
		}
		SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
		if ((postGameSceneMode == SceneMgr.Mode.HUB || postGameSceneMode == SceneMgr.Mode.INVALID) && !PartyManager.Get().IsInParty() && !HearthstoneApplication.Get().IsResetting())
		{
			if (isExpectingArriveInGameplayAsSpectator)
			{
				ReturnToHub(allowReloadHub: true);
			}
			else
			{
				ReturnToHub();
			}
		}
	}

	public void ReturnToHub(bool allowReloadHub = false)
	{
		SceneMgr.Mode mode = SceneMgr.Mode.HUB;
		bool flag = SceneMgr.Get().GetMode() == mode;
		if (!GameUtils.AreAllTutorialsComplete() && Network.ShouldBeConnectedToAurora())
		{
			Network.Get().ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_LOST_GAME_CONNECTION");
		}
		else if (!SceneMgr.Get().IsModeRequested(mode))
		{
			SceneMgr.Get().SetNextMode(mode);
		}
		else if (flag && allowReloadHub)
		{
			SceneMgr.Get().ReloadMode();
		}
		if (flag && !allowReloadHub)
		{
			CheckShowWaitingForNextGameDialog();
		}
	}

	private void ClearAllCacheForReset()
	{
		EndSpectatorMode();
		m_initialized = false;
		m_spectatorPartyIdMain = null;
		m_spectatorPartyIdOpposingSide = null;
		m_requestedInvite = null;
		m_waitingForNextGameDialog = null;
		m_pendingSpectatePlayerAfterLeave = null;
		m_userInitiatedOutgoingInvites = null;
		m_kickedPlayers = null;
		m_kickedFromSpectatingList = null;
		m_expectedDisconnectReason = null;
		m_isExpectingArriveInGameplayAsSpectator = false;
		m_isShowingRemovedAsSpectatorPopup = false;
		m_gameServerKnownSpectators.Clear();
	}

	private void WillReset()
	{
		ClearAllCacheForReset();
		Processor.CancelScheduledCallback(SpectatorManager_UpdatePresenceNextFrame);
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		switch (eventData.m_state)
		{
		case FindGameState.CLIENT_CANCELED:
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.BNET_ERROR:
			if (IsInSpectatorMode())
			{
				EndSpectatorMode(wasKnownSpectating: true);
			}
			break;
		case FindGameState.SERVER_GAME_CANCELED:
			if (IsInSpectatorMode())
			{
				string header = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_HEADER");
				string body = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_TEXT");
				DisplayErrorDialog(header, body);
				EndSpectatorMode(wasKnownSpectating: true);
				if (this.OnSpectateRejected != null)
				{
					this.OnSpectateRejected();
				}
			}
			break;
		}
		return false;
	}

	private void GameState_InitializedEvent(GameState instance, object userData)
	{
		if (m_spectatorPartyIdOpposingSide != null)
		{
			GameState.Get().RegisterCreateGameListener(GameState_CreateGameEvent, null);
		}
	}

	private void GameState_CreateGameEvent(GameState.CreateGamePhase createGamePhase, object userData)
	{
		if (createGamePhase >= GameState.CreateGamePhase.CREATED)
		{
			GameState.Get().UnregisterCreateGameListener(GameState_CreateGameEvent);
			if (m_spectatorPartyIdOpposingSide != null)
			{
				AutoSpectateOpposingSide();
			}
		}
	}

	private void AutoSpectateOpposingSide()
	{
		if (GameState.Get() == null)
		{
			return;
		}
		if (GameState.Get().GetCreateGamePhase() < GameState.CreateGamePhase.CREATED)
		{
			GameState.Get().RegisterCreateGameListener(GameState_CreateGameEvent, null);
		}
		else
		{
			if (SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
			{
				return;
			}
			if (GameMgr.Get().GetTransitionPopup() != null && GameMgr.Get().GetTransitionPopup().IsShown())
			{
				GameMgr.Get().GetTransitionPopup().OnHidden += EnterSpectatorMode_OnTransitionPopupHide;
			}
			else
			{
				if (!(m_spectatorPartyIdOpposingSide != null) || !(m_spectateeOpposingSide != null) || !IsStillInParty(m_spectatorPartyIdOpposingSide))
				{
					return;
				}
				if (IsPlayerInGame(m_spectateeOpposingSide))
				{
					PartyServerInfo partyServerInfo = GetPartyServerInfo(m_spectatorPartyIdOpposingSide);
					JoinInfo joinInfo = ((partyServerInfo == null) ? null : CreateJoinInfo(partyServerInfo));
					if (joinInfo != null)
					{
						SpectateSecondPlayer_Network(joinInfo);
					}
				}
				else
				{
					LogInfoPower("================== End Spectating 2nd player ==================");
					LeaveParty(m_spectatorPartyIdOpposingSide, dissolve: false);
				}
			}
		}
	}

	private void OnSceneUnloaded(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			m_gameServerKnownSpectators.Clear();
		}
		if (mode == SceneMgr.Mode.GAMEPLAY && prevMode != SceneMgr.Mode.GAMEPLAY)
		{
			if (m_spectateeFriendlySide != null)
			{
				BnetBar.Get().HideFriendList();
			}
			if (GameMgr.Get().IsSpectator())
			{
				if (GameMgr.Get().GetTransitionPopup() != null)
				{
					GameMgr.Get().GetTransitionPopup().OnHidden += EnterSpectatorMode_OnTransitionPopupHide;
				}
				BnetPlayer player = BnetUtils.GetPlayer(m_spectateeOpposingSide ?? m_spectateeFriendlySide);
				FireSpectatorModeChanged(OnlineEventType.ADDED, player);
			}
			else
			{
				m_kickedPlayers = null;
			}
			CloseWaitingForNextGameDialog();
			DeclineAllReceivedInvitations();
			UpdateMySpectatorInfo();
		}
		else
		{
			if (prevMode != SceneMgr.Mode.GAMEPLAY || mode == SceneMgr.Mode.GAMEPLAY)
			{
				return;
			}
			if (IsInSpectatorMode())
			{
				LogInfoPower("================== End Spectator Game ==================");
				TimeScaleMgr.Get().SetGameTimeScale(1f);
			}
			EndCurrentSpectatedGame(isLeavingGameplay: true);
			UpdateMySpectatorInfo();
			if (!IsInSpectatorMode())
			{
				return;
			}
			PartyServerInfo partyServerInfo = GetPartyServerInfo(m_spectatorPartyIdMain);
			if (partyServerInfo == null)
			{
				ShowWaitingForNextGameDialog();
				return;
			}
			GameServerInfo lastGameServerJoined = Network.Get().GetLastGameServerJoined();
			if (!IsSameGameAndServer(partyServerInfo, lastGameServerJoined))
			{
				LogInfoPower("================== OnSceneUnloaded: auto-spectating game after leaving game ==================");
				BnetParty_OnPartyAttributeChanged_ServerInfo(new PartyInfo(m_spectatorPartyIdMain, PartyType.SPECTATOR_PARTY), "WTCG.Party.ServerInfo", BnetParty.GetPartyAttributeVariant(m_spectatorPartyIdMain, "WTCG.Party.ServerInfo"));
			}
			else
			{
				ShowWaitingForNextGameDialog();
			}
		}
	}

	public void CheckShowWaitingForNextGameDialog()
	{
		bool flag = true;
		if (!IsInSpectatorMode())
		{
			flag = false;
		}
		else if (SceneMgr.Get().GetNextMode() != 0)
		{
			flag = false;
		}
		else if (IsInSpectatableScene(alsoCheckRequestedScene: true))
		{
			flag = false;
		}
		if (flag)
		{
			ShowWaitingForNextGameDialog();
		}
		else
		{
			CloseWaitingForNextGameDialog();
		}
	}

	public void ShowWaitingForNextGameDialog()
	{
		if (Network.IsLoggedIn())
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_id = "SPECTATOR_WAITING_FOR_NEXT_GAME";
			popupInfo.m_layerToUse = GameLayer.UI;
			popupInfo.m_headerText = GameStrings.Get("GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_HEADER");
			popupInfo.m_text = GetWaitingForNextGameDialogText();
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CANCEL;
			popupInfo.m_cancelText = GameStrings.Get("GLOBAL_LEAVE_SPECTATOR_MODE");
			popupInfo.m_responseCallback = OnSceneUnloaded_AwaitingNextGame_LeaveSpectatorMode;
			popupInfo.m_keyboardEscIsCancel = false;
			DialogManager.Get().ShowUniquePopup(popupInfo, OnSceneUnloaded_AwaitingNextGame_DialogProcessCallback);
			Processor.CancelScheduledCallback(WaitingForNextGame_AutoLeaveSpectatorMode);
			if ((float)WAITING_FOR_NEXT_GAME_AUTO_LEAVE_SECONDS >= 0f)
			{
				Processor.ScheduleCallback(WAITING_FOR_NEXT_GAME_AUTO_LEAVE_SECONDS, realTime: true, WaitingForNextGame_AutoLeaveSpectatorMode);
			}
		}
	}

	private void CloseWaitingForNextGameDialog()
	{
		if ((bool)DISABLE_MENU_BUTTON_WHILE_WAITING)
		{
			BnetBar.Get().m_menuButton.SetEnabled(enabled: true);
		}
		if (DialogManager.Get() != null)
		{
			DialogManager.Get().RemoveUniquePopupRequestFromQueue("SPECTATOR_WAITING_FOR_NEXT_GAME");
		}
		if (m_waitingForNextGameDialog != null)
		{
			m_waitingForNextGameDialog.Hide();
			m_waitingForNextGameDialog = null;
		}
		Processor.CancelScheduledCallback(WaitingForNextGame_AutoLeaveSpectatorMode);
	}

	private void UpdateWaitingForNextGameDialog()
	{
		if (!(m_waitingForNextGameDialog == null))
		{
			string waitingForNextGameDialogText = GetWaitingForNextGameDialogText();
			m_waitingForNextGameDialog.BodyText = waitingForNextGameDialogText;
		}
	}

	private string GetWaitingForNextGameDialogText()
	{
		BnetPlayer player = BnetUtils.GetPlayer(m_spectateeFriendlySide);
		string playerBestName = BnetUtils.GetPlayerBestName(m_spectateeFriendlySide);
		string text = null;
		string text2 = null;
		if (player != null && player.IsOnline())
		{
			text2 = PresenceMgr.Get().GetStatusText(player) ?? "";
			if (!string.IsNullOrEmpty(text2))
			{
				text2 = text2.Trim();
				text = "GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_TEXT";
			}
			else
			{
				text = "GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_TEXT_ONLINE";
			}
			Enum[] statusEnums = PresenceMgr.Get().GetStatusEnums(player);
			if (statusEnums.Length != 0 && (Global.PresenceStatus)(object)statusEnums[0] == Global.PresenceStatus.ADVENTURE_SCENARIO_SELECT)
			{
				if (statusEnums.Length > 1 && (PresenceAdventureMode)(object)statusEnums[1] < PresenceAdventureMode.RETURNING_PLAYER_CHALLENGE)
				{
					text = "GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_TEXT_ENTERING";
				}
			}
			else if (statusEnums.Length != 0 && (Global.PresenceStatus)(object)statusEnums[0] == Global.PresenceStatus.ADVENTURE_SCENARIO_PLAYING_GAME)
			{
				if (statusEnums.Length > 1 && GameUtils.IsHeroicAdventureMission((int)(ScenarioDbId)(object)statusEnums[1]))
				{
					text = "GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_TEXT_BATTLING";
				}
				else if (statusEnums.Length > 1 && GameUtils.IsClassChallengeMission((int)(ScenarioDbId)(object)statusEnums[1]))
				{
					text = "GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_TEXT_PLAYING";
				}
			}
		}
		else
		{
			text = "GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_TEXT_OFFLINE";
			text2 = GameStrings.Get("GLOBAL_OFFLINE");
		}
		return GameStrings.Format(text, playerBestName, text2);
	}

	private bool OnSceneUnloaded_AwaitingNextGame_DialogProcessCallback(DialogBase dialog, object userData)
	{
		if (SceneMgr.Get().IsInGame() || (GameMgr.Get() != null && GameMgr.Get().IsFindingGame()))
		{
			return false;
		}
		m_waitingForNextGameDialog = (AlertPopup)dialog;
		UpdateWaitingForNextGameDialog();
		if ((bool)DISABLE_MENU_BUTTON_WHILE_WAITING)
		{
			BnetBar.Get().m_menuButton.SetEnabled(enabled: false);
		}
		return true;
	}

	private static void WaitingForNextGame_AutoLeaveSpectatorMode(object userData)
	{
		if (Get().IsInSpectatorMode() && !SceneMgr.Get().IsInGame())
		{
			Get().LeaveSpectatorMode();
			string header = GameStrings.Get("GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_HEADER");
			string body = GameStrings.Format("GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_TIMEOUT");
			DisplayErrorDialog(header, body);
		}
	}

	private void OnSceneUnloaded_AwaitingNextGame_LeaveSpectatorMode(AlertPopup.Response response, object userData)
	{
		LeaveSpectatorMode();
	}

	private void EnterSpectatorMode_OnTransitionPopupHide(TransitionPopup popup)
	{
		popup.OnHidden -= EnterSpectatorMode_OnTransitionPopupHide;
		if (SoundManager.Get() != null)
		{
			SoundManager.Get().LoadAndPlay("SpectatorMode_Enter.prefab:e0c11cb0f554e6c4cb9f24994bf13e1c");
		}
		if (m_spectateeOpposingSide != null)
		{
			AutoSpectateOpposingSide();
		}
	}

	private void OnSpectatorOpenJoinOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		bool @bool = Options.Get().GetBool(Option.SPECTATOR_OPEN_JOIN);
		if ((!existed || (bool)prevValue != @bool) && HearthstoneServices.IsAvailable<SceneMgr>() && SceneMgr.Get().IsInGame() && (GameMgr.Get() == null || !GameMgr.Get().IsSpectator()))
		{
			JoinInfo presenceSpectatorJoinInfo = ((!@bool) ? null : GetMyGameJoinInfo());
			if (Network.ShouldBeConnectedToAurora() && Network.IsLoggedIn())
			{
				BnetPresenceMgr.Get().SetPresenceSpectatorJoinInfo(presenceSpectatorJoinInfo);
			}
		}
	}

	private void Network_OnSpectatorInviteReceived(Invite protoInvite)
	{
		BnetGameAccountId inviterId = BnetGameAccountId.CreateFromNet(protoInvite.InviterGameAccountId);
		AddReceivedInvitation(inviterId, protoInvite.JoinInfo);
	}

	private void Network_OnSpectatorInviteReceived_ResponseCallback(AlertPopup.Response response, object userData)
	{
		BnetGameAccountId bnetGameAccountId = (BnetGameAccountId)userData;
		if (response == AlertPopup.Response.CANCEL)
		{
			RemoveReceivedInvitation(bnetGameAccountId);
			return;
		}
		BnetPlayer player = BnetUtils.GetPlayer(bnetGameAccountId);
		if (player != null)
		{
			SpectatePlayer(player);
		}
	}

	private void Network_OnSpectatorNotifyEvent()
	{
		SpectatorNotify spectatorNotify = Network.Get().GetSpectatorNotify();
		if (spectatorNotify == null)
		{
			TelemetryManager.Client().SendLiveIssue("Network_OnSpectatorNotifyEvent Exception", "'notify' is null.");
			return;
		}
		if (spectatorNotify.HasSpectatorPasswordUpdate && !string.IsNullOrEmpty(spectatorNotify.SpectatorPasswordUpdate))
		{
			GameServerInfo lastGameServerJoined = Network.Get().GetLastGameServerJoined();
			if (lastGameServerJoined == null)
			{
				TelemetryManager.Client().SendLiveIssue("Network_OnSpectatorNotifyEvent Exception", "'serverInfo' is null.");
			}
			else if (!spectatorNotify.SpectatorPasswordUpdate.Equals(lastGameServerJoined.SpectatorPassword))
			{
				lastGameServerJoined.SpectatorPassword = spectatorNotify.SpectatorPasswordUpdate;
				UpdateMySpectatorInfo();
				RevokeAllSentInvitations();
			}
		}
		if (spectatorNotify.HasSpectatorRemoved)
		{
			m_expectedDisconnectReason = spectatorNotify.SpectatorRemoved.ReasonCode;
			GameMgr gameMgr = GameMgr.Get();
			if (gameMgr == null)
			{
				TelemetryManager.Client().SendLiveIssue("Network_OnSpectatorNotifyEvent Exception", "GameMgr is null.");
			}
			bool flag = gameMgr?.IsTransitionPopupShown() ?? false;
			if (spectatorNotify.SpectatorRemoved.ReasonCode == 0)
			{
				if (spectatorNotify.SpectatorRemoved.HasRemovedBy)
				{
					GameServerInfo lastGameServerJoined2 = Network.Get().GetLastGameServerJoined();
					if (lastGameServerJoined2 != null)
					{
						if (m_kickedFromSpectatingList == null)
						{
							m_kickedFromSpectatingList = new Map<BnetGameAccountId, uint>();
						}
						BnetGameAccountId key = BnetGameAccountId.CreateFromNet(spectatorNotify.SpectatorRemoved.RemovedBy);
						m_kickedFromSpectatingList[key] = lastGameServerJoined2.GameHandle;
					}
				}
				if (!m_isShowingRemovedAsSpectatorPopup)
				{
					AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
					popupInfo.m_headerText = GameStrings.Get("GLOBAL_SPECTATOR_REMOVED_PROMPT_HEADER");
					popupInfo.m_text = GameStrings.Get("GLOBAL_SPECTATOR_REMOVED_PROMPT_TEXT");
					popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
					if (flag)
					{
						popupInfo.m_responseCallback = Network_OnSpectatorNotifyEvent_Removed_GoToNextMode;
					}
					else
					{
						popupInfo.m_responseCallback = delegate
						{
							SpectatorManager spectatorManager = Get();
							if (spectatorManager != null)
							{
								spectatorManager.m_isShowingRemovedAsSpectatorPopup = false;
							}
							else
							{
								TelemetryManager.Client().SendLiveIssue("Network_OnSpectatorNotifyEvent Exception", "SpectatorManager is null in response callback.");
							}
						};
					}
					m_isShowingRemovedAsSpectatorPopup = true;
					DialogManager dialogManager = DialogManager.Get();
					if (dialogManager != null)
					{
						dialogManager.ShowPopup(popupInfo);
					}
					else
					{
						TelemetryManager.Client().SendLiveIssue("Network_OnSpectatorNotifyEvent Exception", "DialogManager is null.");
					}
				}
			}
			else if (flag)
			{
				Network_OnSpectatorNotifyEvent_Removed_GoToNextMode(AlertPopup.Response.OK, null);
			}
			SoundManager soundManager = SoundManager.Get();
			if (soundManager != null)
			{
				soundManager.LoadAndPlay("SpectatorMode_Exit.prefab:f1d7dab96facdc64fb6648ff1dd22073");
			}
			else
			{
				TelemetryManager.Client().SendLiveIssue("Network_OnSpectatorNotifyEvent Exception", "SoundManager is null.");
			}
			EndSpectatorMode(wasKnownSpectating: true);
			m_expectedDisconnectReason = spectatorNotify.SpectatorRemoved.ReasonCode;
		}
		if (spectatorNotify == null || spectatorNotify.SpectatorChange.Count == 0 || (GameMgr.Get() != null && GameMgr.Get().IsSpectator()))
		{
			return;
		}
		foreach (SpectatorChange item in spectatorNotify.SpectatorChange)
		{
			BnetGameAccountId gameAccountId = BnetGameAccountId.CreateFromNet(item.GameAccountId);
			if (item.IsRemoved)
			{
				RemoveKnownSpectator(gameAccountId);
				continue;
			}
			AddKnownSpectator(gameAccountId);
			ReinviteKnownSpectatorsNotInParty();
		}
	}

	private void Network_OnSpectatorNotifyEvent_Removed_GoToNextMode(AlertPopup.Response response, object userData)
	{
		m_isShowingRemovedAsSpectatorPopup = false;
	}

	private void ReceivedInvitation_ExpireTimeout(object userData)
	{
		PruneOldInvites();
		if (m_receivedSpectateMeInvites.Count > 0)
		{
			float num = m_receivedSpectateMeInvites.Min((KeyValuePair<BnetGameAccountId, ReceivedInvite> kv) => kv.Value.m_timestamp);
			Processor.ScheduleCallback(Mathf.Max(0f, num + 300f - Time.realtimeSinceStartup), realTime: true, ReceivedInvitation_ExpireTimeout);
		}
	}

	private void Presence_OnGameAccountPresenceChange(PresenceUpdate[] updates)
	{
		for (int j = 0; j < updates.Length; j++)
		{
			PresenceUpdate presenceUpdate = updates[j];
			BnetGameAccountId entityId = BnetGameAccountId.CreateFromEntityId(presenceUpdate.entityId);
			bool flag = presenceUpdate.fieldId == 1 && presenceUpdate.programId == BnetProgramId.BNET;
			bool flag2 = presenceUpdate.programId == BnetProgramId.HEARTHSTONE && presenceUpdate.fieldId == 17;
			if (m_waitingForNextGameDialog != null && m_spectateeFriendlySide != null && (flag || flag2) && entityId == m_spectateeFriendlySide)
			{
				UpdateWaitingForNextGameDialog();
			}
			if (!flag || !presenceUpdate.boolVal)
			{
				continue;
			}
			PartyId[] joinedPartyIds = BnetParty.GetJoinedPartyIds();
			foreach (PartyId partyId in joinedPartyIds)
			{
				if (BnetParty.IsLeader(partyId) && !BnetParty.IsMember(partyId, entityId))
				{
					BnetGameAccountId partyCreator = GetPartyCreator(partyId);
					if (partyCreator != null && partyCreator == entityId && !BnetParty.GetSentInvites(partyId).Any((PartyInvite i) => i.InviteeId == entityId))
					{
						BnetParty.SendInvite(partyId, entityId, isReservation: false);
					}
				}
			}
		}
	}

	private void BnetFriendMgr_OnFriendsChanged(BnetFriendChangelist changelist, object userData)
	{
		if (changelist != null)
		{
			List<BnetPlayer> removedFriends = changelist.GetRemovedFriends();
			CheckSpectatorsOnChangedContext(removedFriends);
		}
	}

	private void FiresideGatheringManager_OnPatronListUpdated(List<BnetPlayer> addedList, List<BnetPlayer> removedList)
	{
		CheckSpectatorsOnChangedContext(removedList);
	}

	private void CheckSpectatorsOnChangedContext(List<BnetPlayer> players)
	{
		if (!IsBeingSpectated() || players == null)
		{
			return;
		}
		foreach (BnetPlayer player in players)
		{
			BnetGameAccountId hearthstoneGameAccountId = player.GetHearthstoneGameAccountId();
			if (IsSpectatingMe(hearthstoneGameAccountId) && !IsInSpectableContextWithPlayer(hearthstoneGameAccountId))
			{
				KickSpectator_Internal(player, regenerateSpectatorPassword: true, addToKickList: false);
			}
		}
	}

	private void EndGameScreen_OnTwoScoopsShown(bool shown, EndGameTwoScoop twoScoops)
	{
		if (IsSpectatingOrWatching)
		{
			if (shown)
			{
				Processor.ScheduleCallback(5f, realTime: false, EndGameScreen_OnTwoScoopsShown_AutoClose);
			}
			else
			{
				Processor.CancelScheduledCallback(EndGameScreen_OnTwoScoopsShown_AutoClose);
			}
		}
	}

	private void EndGameScreen_OnTwoScoopsShown_AutoClose(object userData)
	{
		if (EndGameScreen.Get() == null)
		{
			return;
		}
		if ((float)WAITING_FOR_NEXT_GAME_AUTO_LEAVE_SECONDS >= 0f)
		{
			int num = 0;
			while (EndGameScreen.Get().ContinueEvents())
			{
				num++;
				if (num > 100)
				{
					break;
				}
			}
		}
		else
		{
			EndGameScreen.Get().ContinueEvents();
		}
	}

	private void EndGameScreen_OnBackOutOfGameplay()
	{
		if (PartyManager.Get().IsInParty())
		{
			LeaveSpectatorMode();
		}
	}

	private void BnetParty_OnError(PartyError error)
	{
		if (!error.IsOperationCallback)
		{
			return;
		}
		switch (error.FeatureEvent)
		{
		case BnetFeatureEvent.Party_Leave_Callback:
		case BnetFeatureEvent.Party_Dissolve_Callback:
			if (m_leavePartyIdsRequested != null)
			{
				m_leavePartyIdsRequested.Remove(error.PartyId);
			}
			if (m_pendingSpectatePlayerAfterLeave != null && error.ErrorCode != BattleNetErrors.ERROR_OK)
			{
				string playerBestName = BnetUtils.GetPlayerBestName(m_pendingSpectatePlayerAfterLeave.SpectateeId);
				string header2 = GameStrings.Get("GLOBAL_ERROR_GENERIC_HEADER");
				string body2 = GameStrings.Format("GLOBAL_SPECTATOR_ERROR_LEAVE_FOR_SPECTATE_PLAYER_TEXT", playerBestName);
				DisplayErrorDialog(header2, body2);
				m_pendingSpectatePlayerAfterLeave = null;
			}
			break;
		case BnetFeatureEvent.Party_Create_Callback:
			if (error.ErrorCode != BattleNetErrors.ERROR_OK)
			{
				m_userInitiatedOutgoingInvites = null;
				string header = GameStrings.Get("GLOBAL_ERROR_GENERIC_HEADER");
				string body = GameStrings.Format("GLOBAL_SPECTATOR_ERROR_CREATE_PARTY_TEXT");
				DisplayErrorDialog(header, body);
			}
			break;
		}
	}

	private static void DisplayErrorDialog(string header, string body)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = header;
		popupInfo.m_text = body;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void BnetParty_OnJoined(OnlineEventType evt, PartyInfo party, LeaveReason? reason)
	{
		if (!m_initialized || party.Type != PartyType.SPECTATOR_PARTY)
		{
			return;
		}
		if (evt == OnlineEventType.REMOVED)
		{
			bool flag = false;
			if (m_leavePartyIdsRequested != null)
			{
				flag = m_leavePartyIdsRequested.Remove(party.Id);
			}
			LogInfoParty("SpectatorParty_OnLeft: left party={0} current={1} reason={2} wasRequested={3}", party, m_spectatorPartyIdMain, reason.HasValue ? reason.Value.ToString() : "null", flag);
			bool flag2 = false;
			if (party.Id == m_spectatorPartyIdOpposingSide)
			{
				m_spectatorPartyIdOpposingSide = null;
				flag2 = true;
			}
			else if (m_spectateeFriendlySide != null)
			{
				if (party.Id == m_spectatorPartyIdMain)
				{
					m_spectatorPartyIdMain = null;
					flag2 = true;
				}
			}
			else if (m_spectateeFriendlySide == null && m_spectateeOpposingSide == null)
			{
				if (party.Id != m_spectatorPartyIdMain)
				{
					CreatePartyIfNecessary();
					return;
				}
				m_userInitiatedOutgoingInvites = null;
				m_spectatorPartyIdMain = null;
				UpdateSpectatorPresence();
				if (reason.HasValue && reason.Value != 0 && reason.Value != LeaveReason.DISSOLVED_BY_MEMBER)
				{
					Processor.ScheduleCallback(1f, realTime: true, delegate
					{
						CreatePartyIfNecessary();
					});
				}
			}
			if (m_pendingSpectatePlayerAfterLeave != null && m_spectatorPartyIdMain == null && m_spectatorPartyIdOpposingSide == null)
			{
				SpectatePlayer_Internal(m_pendingSpectatePlayerAfterLeave.SpectateeId, m_pendingSpectatePlayerAfterLeave.JoinInfo);
			}
			else if (flag2 && m_spectatorPartyIdMain == null)
			{
				if (flag)
				{
					EndSpectatorMode(wasKnownSpectating: true);
				}
				else
				{
					bool flag3 = reason.HasValue && reason.Value == LeaveReason.MEMBER_KICKED;
					bool flag4 = m_expectedDisconnectReason.HasValue && m_expectedDisconnectReason.Value == 0;
					EndSpectatorMode(wasKnownSpectating: true);
					if (flag3 && !flag4)
					{
						if (flag3)
						{
							BnetGameAccountId bnetGameAccountId = GetPartyCreator(party.Id);
							if (bnetGameAccountId == null)
							{
								bnetGameAccountId = BnetParty.GetLeader(party.Id)?.GameAccountId;
							}
							if (bnetGameAccountId != null)
							{
								GameServerInfo lastGameServerJoined = Network.Get().GetLastGameServerJoined();
								if (lastGameServerJoined != null)
								{
									if (m_kickedFromSpectatingList == null)
									{
										m_kickedFromSpectatingList = new Map<BnetGameAccountId, uint>();
									}
									m_kickedFromSpectatingList[bnetGameAccountId] = lastGameServerJoined.GameHandle;
								}
							}
						}
						if (!m_isShowingRemovedAsSpectatorPopup)
						{
							bool num = GameMgr.Get().IsTransitionPopupShown();
							AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo
							{
								m_headerText = GameStrings.Get("GLOBAL_SPECTATOR_REMOVED_PROMPT_HEADER"),
								m_text = (BnetPresenceMgr.Get().GetMyPlayer().IsAppearingOffline() ? GameStrings.Get("GLOBAL_SPECTATOR_REMOVED_PROMPT_APPEAR_OFFLINE_TEXT") : GameStrings.Get("GLOBAL_SPECTATOR_REMOVED_PROMPT_TEXT")),
								m_responseDisplay = AlertPopup.ResponseDisplay.OK
							};
							if (num)
							{
								popupInfo.m_responseCallback = Network_OnSpectatorNotifyEvent_Removed_GoToNextMode;
							}
							else
							{
								popupInfo.m_responseCallback = delegate
								{
									Get().m_isShowingRemovedAsSpectatorPopup = false;
								};
							}
							m_isShowingRemovedAsSpectatorPopup = true;
							DialogManager.Get().ShowPopup(popupInfo);
						}
					}
				}
			}
			Processor.ScheduleCallback(0.5f, realTime: false, BnetParty_OnLostPartyReference_RemoveKnownCreator, party.Id);
		}
		if (evt != 0)
		{
			return;
		}
		BnetGameAccountId partyCreator = GetPartyCreator(party.Id);
		if (partyCreator == null)
		{
			LogInfoParty("SpectatorParty_OnJoined: joined party={0} without creator.", party.Id);
			LeaveParty(party.Id, BnetParty.IsLeader(party.Id));
			return;
		}
		if (m_requestedInvite != null && m_requestedInvite.PartyId == party.Id)
		{
			m_requestedInvite = null;
			Processor.CancelScheduledCallback(SpectatePlayer_RequestInvite_FriendlySide_Timeout);
			Processor.CancelScheduledCallback(SpectatePlayer_RequestInvite_OpposingSide_Timeout);
		}
		bool flag5 = ShouldBePartyLeader(party.Id);
		bool flag6 = m_spectatorPartyIdMain == null;
		bool flag7 = flag6;
		if (m_spectatorPartyIdMain != null && m_spectatorPartyIdMain != party.Id && (flag5 || partyCreator != m_spectateeOpposingSide))
		{
			flag7 = true;
			LogInfoParty("SpectatorParty_OnJoined: joined party={0} when different current={1} (will be clobbered) joinedParties={2}", party.Id, m_spectatorPartyIdMain, string.Join(", ", (from i in BnetParty.GetJoinedParties()
				select i.ToString()).ToArray()));
		}
		if (flag5)
		{
			m_spectatorPartyIdMain = party.Id;
			if (flag7)
			{
				UpdateSpectatorPresence();
			}
			UpdateSpectatorPartyServerInfo();
			ReinviteKnownSpectatorsNotInParty();
			if (m_userInitiatedOutgoingInvites != null)
			{
				foreach (BnetGameAccountId userInitiatedOutgoingInvite in m_userInitiatedOutgoingInvites)
				{
					BnetParty.SendInvite(m_spectatorPartyIdMain, userInitiatedOutgoingInvite, isReservation: false);
				}
			}
			if (!flag6 || this.OnSpectatorToMyGame == null)
			{
				return;
			}
			bgs.PartyMember[] members = BnetParty.GetMembers(m_spectatorPartyIdMain);
			foreach (bgs.PartyMember partyMember in members)
			{
				if (!(partyMember.GameAccountId == BnetPresenceMgr.Get().GetMyGameAccountId()))
				{
					Processor.RunCoroutine(WaitForPresenceThenToast(partyMember.GameAccountId, SocialToastMgr.TOAST_TYPE.SPECTATOR_ADDED));
					BnetPlayer player = BnetUtils.GetPlayer(partyMember.GameAccountId);
					this.OnSpectatorToMyGame(OnlineEventType.ADDED, player);
				}
			}
			return;
		}
		bool flag8 = true;
		if (m_spectateeFriendlySide == null)
		{
			m_spectateeFriendlySide = partyCreator;
			m_spectatorPartyIdMain = party.Id;
			flag8 = false;
		}
		else if (partyCreator == m_spectateeFriendlySide)
		{
			m_spectatorPartyIdMain = party.Id;
		}
		else if (partyCreator == m_spectateeOpposingSide)
		{
			m_spectatorPartyIdOpposingSide = party.Id;
		}
		if (BnetParty.GetPartyAttributeBlob(party.Id, "WTCG.Party.ServerInfo") != null)
		{
			LogInfoParty("SpectatorParty_OnJoined: joined party={0} as spectator, begin spectating game.", party.Id);
			if (!flag8)
			{
				if (partyCreator == m_spectateeOpposingSide)
				{
					LogInfoPower("================== Begin Spectating 2nd player ==================");
				}
				else
				{
					LogInfoPower("================== Begin Spectating 1st player ==================");
				}
			}
			JoinPartyGame(party.Id);
			return;
		}
		if (PartyManager.Get().IsInParty())
		{
			string header = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_HEADER");
			string body = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_TEXT");
			DisplayErrorDialog(header, body);
			EndSpectatorMode(wasKnownSpectating: true);
			if (this.OnSpectateRejected != null)
			{
				this.OnSpectateRejected();
			}
		}
		else if (!SceneMgr.Get().IsInGame())
		{
			ShowWaitingForNextGameDialog();
		}
		BnetPlayer player2 = BnetUtils.GetPlayer(partyCreator);
		FireSpectatorModeChanged(OnlineEventType.ADDED, player2);
	}

	private void BnetParty_OnLostPartyReference_RemoveKnownCreator(object userData)
	{
		PartyId partyId = userData as PartyId;
		if (partyId != null && !BnetParty.IsInParty(partyId) && !BnetParty.GetReceivedInvites().Any((PartyInvite i) => i.PartyId == partyId))
		{
			Get().m_knownPartyCreatorIds.Remove(partyId);
		}
	}

	private void BnetParty_OnReceivedInvite(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviterId, BnetGameAccountId inviteeId, InviteRemoveReason? reason)
	{
		if (!m_initialized || party.Type != PartyType.SPECTATOR_PARTY)
		{
			return;
		}
		PartyInvite receivedInvite = BnetParty.GetReceivedInvite(inviteId);
		bool flag = receivedInvite != null && (receivedInvite.IsRejoin || (receivedInvite.InviterId == receivedInvite.InviteeId && receivedInvite.InviteeId == BnetPresenceMgr.Get().GetMyGameAccountId()));
		BnetGameAccountId bnetGameAccountId = ((receivedInvite == null) ? null : GetPartyCreator(receivedInvite.PartyId));
		BnetPlayer inviter = ((receivedInvite == null) ? null : BnetUtils.GetPlayer(receivedInvite.InviterId));
		bool flag2 = false;
		bool flag3 = false;
		string text = string.Empty;
		switch (evt)
		{
		case OnlineEventType.ADDED:
			if (receivedInvite == null)
			{
				return;
			}
			if (flag || ShouldBePartyLeader(receivedInvite.PartyId))
			{
				if (ShouldBePartyLeader(receivedInvite.PartyId))
				{
					flag2 = true;
					text = "should_be_leader";
					break;
				}
				if (m_spectatorPartyIdMain != null)
				{
					if (m_spectatorPartyIdMain == receivedInvite.PartyId)
					{
						flag2 = true;
						text = "spectating_this_party";
					}
					else
					{
						flag3 = true;
						text = "spectating_other_party";
					}
					break;
				}
				flag3 = true;
				text = "not_spectating";
				if (bnetGameAccountId != null && m_spectateeFriendlySide == null)
				{
					m_spectateeFriendlySide = bnetGameAccountId;
					flag2 = true;
					flag3 = false;
					text = "rejoin_spectating";
				}
			}
			else if (receivedInvite.InviterId == m_spectateeFriendlySide || receivedInvite.InviterId == m_spectateeOpposingSide || (m_requestedInvite != null && m_requestedInvite.PartyId == receivedInvite.PartyId))
			{
				flag2 = true;
				text = "spectating_this_player";
				if (m_requestedInvite != null)
				{
					m_requestedInvite = null;
					Processor.CancelScheduledCallback(SpectatePlayer_RequestInvite_FriendlySide_Timeout);
					Processor.CancelScheduledCallback(SpectatePlayer_RequestInvite_OpposingSide_Timeout);
				}
			}
			else if (!UserAttentionManager.CanShowAttentionGrabber("SpectatorManager.BnetParty_OnReceivedInvite:" + evt))
			{
				flag3 = true;
				text = "user_attention_blocked";
			}
			else
			{
				if (m_kickedFromSpectatingList != null)
				{
					m_kickedFromSpectatingList.Remove(receivedInvite.InviterId);
				}
				if (SocialToastMgr.Get() != null)
				{
					string inviterBestName = BnetUtils.GetInviterBestName(receivedInvite);
					SocialToastMgr.Get().AddToast(UserAttentionBlocker.NONE, inviterBestName, SocialToastMgr.TOAST_TYPE.SPECTATOR_INVITE_RECEIVED);
				}
			}
			break;
		case OnlineEventType.REMOVED:
			if (!reason.HasValue || reason.Value == InviteRemoveReason.ACCEPTED)
			{
				Processor.ScheduleCallback(0.5f, realTime: false, BnetParty_OnLostPartyReference_RemoveKnownCreator, party.Id);
			}
			break;
		}
		LogInfoParty("Spectator_OnReceivedInvite {0} rejoin={1} partyId={2} creatorId={3} accept={4} decline={5} acceptDeclineReason={6} removeReason={7}", evt, flag, party.Id, bnetGameAccountId, flag2, flag3, text, reason);
		if (flag2)
		{
			BnetParty.AcceptReceivedInvite(inviteId);
		}
		else if (flag3)
		{
			BnetParty.DeclineReceivedInvite(inviteId);
		}
		else if (this.OnInviteReceived != null)
		{
			this.OnInviteReceived(evt, inviter);
		}
	}

	private void BnetParty_OnSentInvite(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviterId, BnetGameAccountId inviteeId, bool senderIsMyself, InviteRemoveReason? reason)
	{
		if (party.Type != PartyType.SPECTATOR_PARTY || !senderIsMyself)
		{
			return;
		}
		PartyInvite sentInvite = BnetParty.GetSentInvite(party.Id, inviteId);
		BnetPlayer invitee = ((sentInvite == null) ? null : BnetUtils.GetPlayer(sentInvite.InviteeId));
		if (evt == OnlineEventType.ADDED)
		{
			bool flag = false;
			if (m_userInitiatedOutgoingInvites != null && sentInvite != null)
			{
				flag = m_userInitiatedOutgoingInvites.Remove(sentInvite.InviteeId);
			}
			if (flag && sentInvite != null && ShouldBePartyLeader(party.Id) && !m_gameServerKnownSpectators.Contains(sentInvite.InviteeId) && SocialToastMgr.Get() != null)
			{
				string playerBestName = BnetUtils.GetPlayerBestName(sentInvite.InviteeId);
				SocialToastMgr.Get().AddToast(UserAttentionBlocker.NONE, playerBestName, SocialToastMgr.TOAST_TYPE.SPECTATOR_INVITE_SENT);
			}
		}
		if (sentInvite != null && !m_gameServerKnownSpectators.Contains(sentInvite.InviteeId) && this.OnInviteSent != null)
		{
			this.OnInviteSent(evt, invitee);
		}
	}

	private void BnetParty_OnReceivedInviteRequest(OnlineEventType evt, PartyInfo party, InviteRequest request, InviteRequestRemovedReason? reason)
	{
		if (party.Type == PartyType.SPECTATOR_PARTY && evt == OnlineEventType.ADDED)
		{
			bool flag = false;
			if (party.Id != m_spectatorPartyIdMain)
			{
				flag = true;
			}
			if (request.RequesterId != null && request.RequesterId == request.TargetId && !Options.Get().GetBool(Option.SPECTATOR_OPEN_JOIN))
			{
				flag = true;
			}
			if (!IsInSpectableContextWithPlayer(request.RequesterId))
			{
				flag = true;
			}
			if (!IsInSpectableContextWithPlayer(request.TargetId))
			{
				flag = true;
			}
			if (m_kickedPlayers != null && (m_kickedPlayers.Contains(request.RequesterId) || m_kickedPlayers.Contains(request.TargetId)))
			{
				flag = true;
			}
			if (flag)
			{
				BnetParty.IgnoreInviteRequest(party.Id, request.TargetId);
			}
			else
			{
				BnetParty.AcceptInviteRequest(party.Id, request.TargetId, isReservation: false);
			}
		}
	}

	private void BnetParty_OnMemberEvent(OnlineEventType evt, PartyInfo party, BnetGameAccountId memberId, bool isRolesUpdate, LeaveReason? reason)
	{
		if (party.Id == null || (party.Id != m_spectatorPartyIdMain && party.Id != m_spectatorPartyIdOpposingSide))
		{
			return;
		}
		if (evt == OnlineEventType.ADDED && BnetParty.IsLeader(party.Id))
		{
			BnetGameAccountId partyCreator = GetPartyCreator(party.Id);
			if (partyCreator != null && partyCreator == memberId)
			{
				BnetParty.SetLeader(party.Id, memberId);
			}
		}
		if (m_initialized && evt != OnlineEventType.UPDATED && memberId != BnetPresenceMgr.Get().GetMyGameAccountId() && ShouldBePartyLeader(party.Id) && (!SceneMgr.Get().IsInGame() || !Network.Get().IsConnectedToGameServer() || !m_gameServerKnownSpectators.Contains(memberId)))
		{
			SocialToastMgr.TOAST_TYPE toastType = ((evt == OnlineEventType.ADDED) ? SocialToastMgr.TOAST_TYPE.SPECTATOR_ADDED : SocialToastMgr.TOAST_TYPE.SPECTATOR_REMOVED);
			Processor.RunCoroutine(WaitForPresenceThenToast(memberId, toastType));
			if (this.OnSpectatorToMyGame != null)
			{
				BnetPlayer player = BnetUtils.GetPlayer(memberId);
				this.OnSpectatorToMyGame(evt, player);
			}
		}
	}

	private void BnetParty_OnChatMessage(PartyInfo party, BnetGameAccountId speakerId, string chatMessage)
	{
	}

	private void BnetParty_OnPartyAttributeChanged_ServerInfo(PartyInfo party, string attributeKey, Variant value)
	{
		if (party.Type != PartyType.SPECTATOR_PARTY || value == null)
		{
			return;
		}
		byte[] array = (value.HasBlobValue ? value.BlobValue : null);
		if (array == null)
		{
			return;
		}
		PartyServerInfo partyServerInfo = ProtobufUtil.ParseFrom<PartyServerInfo>(array);
		if (partyServerInfo == null)
		{
			return;
		}
		if (!partyServerInfo.HasSecretKey || string.IsNullOrEmpty(partyServerInfo.SecretKey))
		{
			LogInfoParty("BnetParty_OnPartyAttributeChanged_ServerInfo: no secret key in serverInfo.");
			return;
		}
		GameServerInfo lastGameServerJoined = Network.Get().GetLastGameServerJoined();
		bool flag = Network.Get().IsConnectedToGameServer() && IsSameGameAndServer(partyServerInfo, lastGameServerJoined);
		if (!flag && SceneMgr.Get().IsInGame())
		{
			LogInfoParty("BnetParty_OnPartyAttributeChanged_ServerInfo: cannot join game while in gameplay new={0} curr={1}.", partyServerInfo.GameHandle, lastGameServerJoined.GameHandle);
			return;
		}
		JoinInfo joinInfo = CreateJoinInfo(partyServerInfo);
		if (party.Id == m_spectatorPartyIdOpposingSide)
		{
			if (GameMgr.Get().GetTransitionPopup() == null && GameMgr.Get().IsSpectator())
			{
				SpectateSecondPlayer_Network(joinInfo);
			}
		}
		else if (!flag && party.Id == m_spectatorPartyIdMain)
		{
			LogInfoPower("================== Start Spectator Game ==================");
			m_isExpectingArriveInGameplayAsSpectator = true;
			GameMgr.Get().SpectateGame(joinInfo);
			CloseWaitingForNextGameDialog();
		}
	}

	private void LogInfoParty(string format, params object[] args)
	{
		Log.Party.Print(format, args);
	}

	private void LogInfoPower(string format, params object[] args)
	{
		Log.Party.Print(format, args);
		Log.Power.Print(format, args);
	}

	private bool IsPlayerInGame(BnetGameAccountId gameAccountId)
	{
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return false;
		}
		foreach (KeyValuePair<int, Player> item in gameState.GetPlayerMap())
		{
			BnetPlayer bnetPlayer = item.Value.GetBnetPlayer();
			if (bnetPlayer != null && bnetPlayer.GetHearthstoneGameAccountId() == gameAccountId)
			{
				return true;
			}
		}
		return false;
	}

	private bool IsStillInParty(PartyId partyId)
	{
		if (!BnetParty.IsInParty(partyId))
		{
			return false;
		}
		if (m_leavePartyIdsRequested != null && m_leavePartyIdsRequested.Contains(partyId))
		{
			return false;
		}
		return true;
	}

	private void BnetPresenceMgr_OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		BnetPlayerChange myOwnChange = changelist.FindChange(myGameAccountId);
		if (myOwnChange != null)
		{
			bool flag = myOwnChange.GetNewPlayer().IsAppearingOffline();
			bool flag2 = myOwnChange.GetOldPlayer().IsAppearingOffline();
			if (flag && !flag2 && MyGameHasSpectators())
			{
				BnetGameAccountId[] array = GetSpectatorPartyMembers().ToArray();
				foreach (BnetGameAccountId id in array)
				{
					KickSpectator_Internal(BnetPresenceMgr.Get().GetPlayer(id), regenerateSpectatorPassword: true, addToKickList: false);
				}
			}
			else if (flag2 && !flag)
			{
				UpdateMySpectatorInfo();
			}
		}
		if (!IsBeingSpectated())
		{
			return;
		}
		foreach (BnetPlayerChange item in from c in changelist.GetChanges()
			where c != myOwnChange && c.GetOldPlayer() != null && c.GetOldPlayer().IsOnline() && !c.GetNewPlayer().IsOnline()
			select c)
		{
			KickSpectator_Internal(BnetPresenceMgr.Get().GetPlayer(item.GetPlayer().GetAccountId()), regenerateSpectatorPassword: true, addToKickList: false);
		}
	}

	private void PruneOldInvites()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		List<BnetGameAccountId> list = new List<BnetGameAccountId>();
		foreach (KeyValuePair<BnetGameAccountId, ReceivedInvite> receivedSpectateMeInvite in m_receivedSpectateMeInvites)
		{
			float timestamp = receivedSpectateMeInvite.Value.m_timestamp;
			if (realtimeSinceStartup - timestamp > 300f)
			{
				list.Add(receivedSpectateMeInvite.Key);
			}
		}
		foreach (BnetGameAccountId item in list)
		{
			RemoveReceivedInvitation(item);
		}
		list.Clear();
		foreach (KeyValuePair<BnetGameAccountId, float> sentSpectateMeInvite in m_sentSpectateMeInvites)
		{
			float value = sentSpectateMeInvite.Value;
			if (realtimeSinceStartup - value > 30f)
			{
				list.Add(sentSpectateMeInvite.Key);
			}
		}
		foreach (BnetGameAccountId item2 in list)
		{
			RemoveSentInvitation(item2);
		}
	}

	private void AddReceivedInvitation(BnetGameAccountId inviterId, JoinInfo joinInfo)
	{
		bool num = !m_receivedSpectateMeInvites.ContainsKey(inviterId);
		ReceivedInvite value = new ReceivedInvite(joinInfo);
		m_receivedSpectateMeInvites[inviterId] = value;
		if (num)
		{
			BnetPlayer player = BnetUtils.GetPlayer(inviterId);
			if (SocialToastMgr.Get() != null)
			{
				SocialToastMgr.Get().AddToast(UserAttentionBlocker.NONE, BnetUtils.GetPlayerBestName(inviterId), SocialToastMgr.TOAST_TYPE.SPECTATOR_INVITE_RECEIVED);
			}
			if (this.OnInviteReceived != null)
			{
				this.OnInviteReceived(OnlineEventType.ADDED, player);
			}
		}
		float num2 = m_receivedSpectateMeInvites.Min((KeyValuePair<BnetGameAccountId, ReceivedInvite> kv) => kv.Value.m_timestamp);
		float secondsToWait = Mathf.Max(0f, num2 + 300f - Time.realtimeSinceStartup);
		Processor.CancelScheduledCallback(ReceivedInvitation_ExpireTimeout);
		Processor.ScheduleCallback(secondsToWait, realTime: true, ReceivedInvitation_ExpireTimeout);
	}

	private void RemoveReceivedInvitation(BnetGameAccountId inviterId)
	{
		if (!(inviterId == null) && m_receivedSpectateMeInvites.Remove(inviterId))
		{
			BnetPlayer player = BnetUtils.GetPlayer(inviterId);
			if (this.OnInviteReceived != null)
			{
				this.OnInviteReceived(OnlineEventType.REMOVED, player);
			}
		}
	}

	private void ClearAllReceivedInvitations()
	{
		BnetGameAccountId[] array = m_receivedSpectateMeInvites.Keys.ToArray();
		m_receivedSpectateMeInvites.Clear();
		if (this.OnInviteReceived != null)
		{
			BnetGameAccountId[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				BnetPlayer player = BnetUtils.GetPlayer(array2[i]);
				this.OnInviteReceived(OnlineEventType.REMOVED, player);
			}
		}
	}

	private void AddSentInvitation(BnetGameAccountId inviteeId)
	{
		if (inviteeId == null)
		{
			return;
		}
		bool num = !m_sentSpectateMeInvites.ContainsKey(inviteeId);
		m_sentSpectateMeInvites[inviteeId] = Time.realtimeSinceStartup;
		if (num)
		{
			BnetPlayer player = BnetUtils.GetPlayer(inviteeId);
			if (this.OnInviteSent != null)
			{
				this.OnInviteSent(OnlineEventType.ADDED, player);
			}
		}
	}

	private void RemoveSentInvitation(BnetGameAccountId inviteeId)
	{
		if (!(inviteeId == null) && m_sentSpectateMeInvites.Remove(inviteeId))
		{
			BnetPlayer player = BnetUtils.GetPlayer(inviteeId);
			if (this.OnInviteSent != null)
			{
				this.OnInviteSent(OnlineEventType.REMOVED, player);
			}
		}
	}

	private void DeclineAllReceivedInvitations()
	{
		PartyInvite[] receivedInvites = BnetParty.GetReceivedInvites();
		foreach (PartyInvite partyInvite in receivedInvites)
		{
			if (partyInvite.PartyType == PartyType.SPECTATOR_PARTY)
			{
				BnetParty.DeclineReceivedInvite(partyInvite.InviteId);
			}
		}
	}

	private void RevokeAllSentInvitations()
	{
		ClearAllSentInvitations();
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		PartyId[] array = new PartyId[2] { m_spectatorPartyIdMain, m_spectatorPartyIdOpposingSide };
		foreach (PartyId partyId in array)
		{
			if (partyId == null)
			{
				continue;
			}
			PartyInvite[] sentInvites = BnetParty.GetSentInvites(partyId);
			foreach (PartyInvite partyInvite in sentInvites)
			{
				if (!(partyInvite.InviterId != myGameAccountId))
				{
					BnetParty.RevokeSentInvite(partyId, partyInvite.InviteId);
				}
			}
		}
	}

	private void ClearAllSentInvitations()
	{
		BnetGameAccountId[] array = m_sentSpectateMeInvites.Keys.ToArray();
		m_sentSpectateMeInvites.Clear();
		if (this.OnInviteSent != null)
		{
			BnetGameAccountId[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				BnetPlayer player = BnetUtils.GetPlayer(array2[i]);
				this.OnInviteSent(OnlineEventType.REMOVED, player);
			}
		}
	}

	private void AddKnownSpectator(BnetGameAccountId gameAccountId)
	{
		if (gameAccountId == null)
		{
			return;
		}
		bool num = m_gameServerKnownSpectators.Add(gameAccountId);
		CreatePartyIfNecessary();
		RemoveSentInvitation(gameAccountId);
		RemoveReceivedInvitation(gameAccountId);
		if (!num)
		{
			return;
		}
		if (SceneMgr.Get().IsInGame() && Network.Get().IsConnectedToGameServer())
		{
			bool num2 = BnetParty.IsMember(m_spectatorPartyIdMain, gameAccountId);
			BnetPlayer player = BnetUtils.GetPlayer(gameAccountId);
			if (!num2)
			{
				Processor.RunCoroutine(WaitForPresenceThenToast(gameAccountId, SocialToastMgr.TOAST_TYPE.SPECTATOR_ADDED));
			}
			if (this.OnSpectatorToMyGame != null)
			{
				this.OnSpectatorToMyGame(OnlineEventType.ADDED, player);
			}
		}
		UpdateSpectatorPresence();
	}

	private void RemoveKnownSpectator(BnetGameAccountId gameAccountId)
	{
		if (gameAccountId == null || !m_gameServerKnownSpectators.Remove(gameAccountId))
		{
			return;
		}
		if (SceneMgr.Get().IsInGame() && Network.Get().IsConnectedToGameServer())
		{
			bool num = BnetParty.IsMember(m_spectatorPartyIdMain, gameAccountId);
			BnetPlayer player = BnetUtils.GetPlayer(gameAccountId);
			if (!num)
			{
				Processor.RunCoroutine(WaitForPresenceThenToast(gameAccountId, SocialToastMgr.TOAST_TYPE.SPECTATOR_REMOVED));
			}
			if (this.OnSpectatorToMyGame != null)
			{
				this.OnSpectatorToMyGame(OnlineEventType.REMOVED, player);
			}
		}
		UpdateSpectatorPresence();
	}

	private void ClearAllGameServerKnownSpectators()
	{
		BnetGameAccountId[] array = m_gameServerKnownSpectators.ToArray();
		m_gameServerKnownSpectators.Clear();
		if (this.OnSpectatorToMyGame != null && SceneMgr.Get().IsInGame() && Network.Get().IsConnectedToGameServer())
		{
			BnetGameAccountId[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				BnetPlayer player = BnetUtils.GetPlayer(array2[i]);
				this.OnSpectatorToMyGame(OnlineEventType.REMOVED, player);
			}
		}
		if (array.Length != 0)
		{
			UpdateSpectatorPresence();
		}
	}

	private void UpdateSpectatorPresence()
	{
		if (HearthstoneApplication.Get() != null)
		{
			Processor.CancelScheduledCallback(SpectatorManager_UpdatePresenceNextFrame);
			Processor.ScheduleCallback(0f, realTime: true, SpectatorManager_UpdatePresenceNextFrame);
		}
		else
		{
			SpectatorManager_UpdatePresenceNextFrame(null);
		}
	}

	private void SpectatorManager_UpdatePresenceNextFrame(object userData)
	{
		JoinInfo joinInfo = null;
		bool flag = Options.Get().GetBool(Option.SPECTATOR_OPEN_JOIN) || IsInSpectatorMode();
		joinInfo = GetMyGameJoinInfo();
		if (Network.ShouldBeConnectedToAurora() && Network.IsLoggedIn())
		{
			BnetPresenceMgr.Get().SetPresenceSpectatorJoinInfo(flag ? joinInfo : null);
		}
		PartyManager.Get().UpdateSpectatorJoinInfo(joinInfo);
	}

	private void UpdateSpectatorPartyServerInfo()
	{
		if (m_spectatorPartyIdMain == null)
		{
			return;
		}
		if (!ShouldBePartyLeader(m_spectatorPartyIdMain))
		{
			if (BnetParty.IsLeader(m_spectatorPartyIdMain))
			{
				BnetParty.ClearPartyAttribute(m_spectatorPartyIdMain, "WTCG.Party.ServerInfo");
			}
			return;
		}
		byte[] partyAttributeBlob = BnetParty.GetPartyAttributeBlob(m_spectatorPartyIdMain, "WTCG.Party.ServerInfo");
		GameServerInfo lastGameServerJoined = Network.Get().GetLastGameServerJoined();
		if (IsGameOver || !SceneMgr.Get().IsInGame() || !Network.Get().IsConnectedToGameServer() || lastGameServerJoined == null || string.IsNullOrEmpty(lastGameServerJoined.Address))
		{
			if (partyAttributeBlob != null)
			{
				BnetParty.ClearPartyAttribute(m_spectatorPartyIdMain, "WTCG.Party.ServerInfo");
			}
			return;
		}
		byte[] array = ProtobufUtil.ToByteArray(new PartyServerInfo
		{
			ServerIpAddress = lastGameServerJoined.Address,
			ServerPort = lastGameServerJoined.Port,
			GameHandle = (int)lastGameServerJoined.GameHandle,
			SecretKey = (lastGameServerJoined.SpectatorPassword ?? ""),
			GameType = GameMgr.Get().GetGameType(),
			FormatType = GameMgr.Get().GetFormatType(),
			MissionId = GameMgr.Get().GetMissionId()
		});
		if (!GeneralUtils.AreArraysEqual(array, partyAttributeBlob))
		{
			BnetParty.SetPartyAttributeBlob(m_spectatorPartyIdMain, "WTCG.Party.ServerInfo", array);
		}
	}

	private bool ShouldBePartyLeader(PartyId partyId)
	{
		if (GameMgr.Get().IsSpectator())
		{
			return false;
		}
		if (m_spectateeFriendlySide != null || m_spectateeOpposingSide != null)
		{
			return false;
		}
		BnetGameAccountId partyCreator = GetPartyCreator(partyId);
		if (partyCreator == null)
		{
			return false;
		}
		if (partyCreator != BnetPresenceMgr.Get().GetMyGameAccountId())
		{
			return false;
		}
		return true;
	}

	private BnetGameAccountId GetPartyCreator(PartyId partyId)
	{
		if (partyId == null)
		{
			return null;
		}
		BnetGameAccountId value = null;
		if (m_knownPartyCreatorIds.TryGetValue(partyId, out value) && value != null)
		{
			return value;
		}
		byte[] partyAttributeBlob = BnetParty.GetPartyAttributeBlob(partyId, "WTCG.Party.Creator");
		if (partyAttributeBlob == null)
		{
			return null;
		}
		value = BnetGameAccountId.CreateFromNet(ProtobufUtil.ParseFrom<BnetId>(partyAttributeBlob));
		if (value.IsValid())
		{
			m_knownPartyCreatorIds[partyId] = value;
		}
		return value;
	}

	private bool CreatePartyIfNecessary()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return false;
		}
		if (m_spectatorPartyIdMain != null)
		{
			if (GetPartyCreator(m_spectatorPartyIdMain) != null && !ShouldBePartyLeader(m_spectatorPartyIdMain))
			{
				return false;
			}
			PartyInfo[] joinedParties = BnetParty.GetJoinedParties();
			PartyInfo partyInfo = joinedParties.FirstOrDefault((PartyInfo i) => i.Id == m_spectatorPartyIdMain && i.Type == PartyType.SPECTATOR_PARTY);
			if (partyInfo == null)
			{
				LogInfoParty("CreatePartyIfNecessary stored PartyId={0} is not in joined party list: {1}", m_spectatorPartyIdMain, string.Join(", ", joinedParties.Select((PartyInfo i) => i.ToString()).ToArray()));
				m_spectatorPartyIdMain = null;
				UpdateSpectatorPresence();
			}
			partyInfo = joinedParties.FirstOrDefault((PartyInfo i) => i.Type == PartyType.SPECTATOR_PARTY);
			if (partyInfo != null && m_spectatorPartyIdMain != partyInfo.Id)
			{
				LogInfoParty("CreatePartyIfNecessary repairing mismatching PartyIds current={0} new={1}", m_spectatorPartyIdMain, partyInfo.Id);
				m_spectatorPartyIdMain = partyInfo.Id;
				UpdateSpectatorPresence();
			}
			if (m_spectatorPartyIdMain != null)
			{
				return false;
			}
		}
		if (GetCountSpectatingMe() <= 0)
		{
			return false;
		}
		byte[] creatorBlob = ProtobufUtil.ToByteArray(BnetUtils.CreatePegasusBnetId(BnetPresenceMgr.Get().GetMyGameAccountId()));
		BnetParty.CreateParty(PartyType.SPECTATOR_PARTY, PrivacyLevel.OPEN_INVITATION, creatorBlob, null);
		return true;
	}

	private void ReinviteKnownSpectatorsNotInParty()
	{
		if (m_spectatorPartyIdMain == null || !ShouldBePartyLeader(m_spectatorPartyIdMain))
		{
			return;
		}
		bgs.PartyMember[] members = BnetParty.GetMembers(m_spectatorPartyIdMain);
		foreach (BnetGameAccountId knownSpectator in m_gameServerKnownSpectators)
		{
			if (members.FirstOrDefault((bgs.PartyMember m) => m.GameAccountId == knownSpectator) == null)
			{
				BnetParty.SendInvite(m_spectatorPartyIdMain, knownSpectator, isReservation: false);
			}
		}
	}

	private void LeaveParty(PartyId partyId, bool dissolve)
	{
		if (!(partyId == null))
		{
			if (m_leavePartyIdsRequested == null)
			{
				m_leavePartyIdsRequested = new HashSet<PartyId>();
			}
			m_leavePartyIdsRequested.Add(partyId);
			if (dissolve)
			{
				BnetParty.DissolveParty(partyId);
			}
			else
			{
				BnetParty.Leave(partyId);
			}
		}
	}

	public void LeaveGameScene()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.TriggerPress();
			EndGameScreen.Get().m_hitbox.TriggerRelease();
		}
		else if (!HearthstoneApplication.Get().IsResetting())
		{
			SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
			SceneMgr.Get().SetNextMode(postGameSceneMode);
		}
	}

	private IEnumerator WaitForPresenceThenToast(BnetGameAccountId gameAccountId, SocialToastMgr.TOAST_TYPE toastType)
	{
		float timeStarted = Time.time;
		float num = Time.time - timeStarted;
		while (num < 30f && !BnetUtils.HasPlayerBestNamePresence(gameAccountId))
		{
			yield return null;
			num = Time.time - timeStarted;
		}
		if (SocialToastMgr.Get() != null)
		{
			string playerBestName = BnetUtils.GetPlayerBestName(gameAccountId);
			SocialToastMgr.Get().AddToast(UserAttentionBlocker.NONE, playerBestName, toastType);
		}
	}

	private SpectatorManager()
	{
	}

	private static SpectatorManager CreateInstance()
	{
		s_instance = new SpectatorManager();
		HearthstoneApplication.Get().WillReset += s_instance.WillReset;
		GameMgr.Get().RegisterFindGameEvent(s_instance.OnFindGameEvent);
		SceneMgr.Get().RegisterSceneUnloadedEvent(s_instance.OnSceneUnloaded);
		GameState.RegisterGameStateInitializedListener(s_instance.GameState_InitializedEvent);
		Options.Get().RegisterChangedListener(Option.SPECTATOR_OPEN_JOIN, s_instance.OnSpectatorOpenJoinOptionChanged);
		BnetPresenceMgr.Get().OnGameAccountPresenceChange += s_instance.Presence_OnGameAccountPresenceChange;
		BnetFriendMgr.Get().AddChangeListener(s_instance.BnetFriendMgr_OnFriendsChanged);
		FiresideGatheringManager.OnPatronListUpdated += s_instance.FiresideGatheringManager_OnPatronListUpdated;
		EndGameScreen.OnTwoScoopsShown = (EndGameScreen.OnTwoScoopsShownHandler)Delegate.Combine(EndGameScreen.OnTwoScoopsShown, new EndGameScreen.OnTwoScoopsShownHandler(s_instance.EndGameScreen_OnTwoScoopsShown));
		EndGameScreen.OnBackOutOfGameplay = (Action)Delegate.Combine(EndGameScreen.OnBackOutOfGameplay, new Action(s_instance.EndGameScreen_OnBackOutOfGameplay));
		BnetPresenceMgr.Get().AddPlayersChangedListener(s_instance.BnetPresenceMgr_OnPlayersChanged);
		Network.Get().RegisterNetHandler(SpectatorNotify.PacketID.ID, s_instance.Network_OnSpectatorNotifyEvent);
		BnetParty.OnError += s_instance.BnetParty_OnError;
		BnetParty.OnJoined += s_instance.BnetParty_OnJoined;
		BnetParty.OnReceivedInvite += s_instance.BnetParty_OnReceivedInvite;
		BnetParty.OnSentInvite += s_instance.BnetParty_OnSentInvite;
		BnetParty.OnReceivedInviteRequest += s_instance.BnetParty_OnReceivedInviteRequest;
		BnetParty.OnMemberEvent += s_instance.BnetParty_OnMemberEvent;
		BnetParty.OnChatMessage += s_instance.BnetParty_OnChatMessage;
		BnetParty.RegisterAttributeChangedHandler("WTCG.Party.ServerInfo", s_instance.BnetParty_OnPartyAttributeChanged_ServerInfo);
		return s_instance;
	}
}
