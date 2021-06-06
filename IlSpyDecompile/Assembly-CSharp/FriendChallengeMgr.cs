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
using PegasusShared;
using PegasusUtil;
using SpectatorProto;
using UnityEngine;

public class FriendChallengeMgr
{
	public enum ChallengeMethod
	{
		INVALID,
		FROM_FRIEND_LIST,
		FROM_FIRESIDE_GATHERING_OPPONENT_PICKER
	}

	public enum DeclineReason
	{
		None,
		UserDeclined,
		NoValidDeck,
		StandardNoValidDeck,
		TavernBrawlNoValidDeck,
		TavernBrawlNotUnlocked,
		UserIsBusy,
		NotSeenWild,
		BattlegroundsNoEarlyAccess,
		ClassicNoValidDeck
	}

	public delegate void ChangedCallback(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData, object userData);

	private class ChangedListener : EventListener<ChangedCallback>
	{
		public void Fire(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData)
		{
			m_callback(challengeEvent, player, challengeData, m_userData);
		}
	}

	public const int DEFAULT_SCENARIO_ID = 2;

	private const bool FRIENDLY_CHALLENGE_PARTY_INVITES_USE_RESERVATIONS = true;

	private static FriendChallengeMgr s_instance;

	private bool m_netCacheReady;

	private bool m_myPlayerReady;

	private FriendlyChallengeData m_data = new FriendlyChallengeData();

	private bool m_hasPreSelectedDeckOrHero;

	private long m_preSelectedDeckId;

	private long m_preSelectedHeroId;

	private ChallengeMethod m_challengeMethod;

	private List<ChangedListener> m_changedListeners = new List<ChangedListener>();

	private DialogBase m_challengeDialog;

	private bool m_hasSeenDeclinedReason;

	private bool m_canBeInvitedToGame;

	private bool m_updateMyAvailabilityCallbackScheduledThisFrame;

	private const string ATTRIBUTE_STATE_PLAYER1 = "s1";

	private const string ATTRIBUTE_STATE_PLAYER2 = "s2";

	private const string ATTRIBUTE_DECK_PLAYER1 = "d1";

	private const string ATTRIBUTE_DECK_PLAYER2 = "d2";

	private const string ATTRIBUTE_HERO_PLAYER1 = "hero1";

	private const string ATTRIBUTE_HERO_PLAYER2 = "hero2";

	private const string ATTRIBUTE_FSG_SHARED_SECRET_KEY_PLAYER1 = "fsg1";

	private const string ATTRIBUTE_FSG_SHARED_SECRET_KEY_PLAYER2 = "fsg2";

	private const string ATTRIBUTE_LEFT = "left";

	private const string ATTRIBUTE_ERROR = "error";

	private const string ATTRIBUTE_DECK_SHARE_STATE_PLAYER1 = "p1DeckShareState";

	private const string ATTRIBUTE_DECK_SHARE_STATE_PLAYER2 = "p2DeckShareState";

	private const string ATTRIBUTE_DECK_SHARE_DECKS_PLAYER1 = "p1DeckShareDecks";

	private const string ATTRIBUTE_DECK_SHARE_DECKS_PLAYER2 = "p2DeckShareDecks";

	private const string ATTRIBUTE_DECK_SHARE_ENABLED = "isDeckShareEnabled";

	private const string ATTRIBUTE_QUEST_INFO = "quests";

	private const string ATTRIBUTE_VALUE_STATE_WAIT = "wait";

	private const string ATTRIBUTE_VALUE_STATE_WAIT_FOR_DECK_OR_HERO_SELECT = "deck";

	private const string ATTRIBUTE_VALUE_STATE_READY = "ready";

	private const string ATTRIBUTE_VALUE_STATE_GAME = "game";

	private const string ATTRIBUTE_VALUE_STATE_GOTO = "goto";

	private const string ATTRIBUTE_VALUE_DECK_SHARE_STATE_NONE = "none";

	private const string ATTRIBUTE_VALUE_DECK_SHARE_STATE_REQUESTED = "requested";

	private const string ATTRIBUTE_VALUE_DECK_SHARE_STATE_SHARING = "sharing";

	private const string ATTRIBUTE_VALUE_DECK_SHARE_STATE_SHARING_UNUSED = "sharingUnused";

	private const string ATTRIBUTE_VALUE_DECK_SHARE_STATE_DECLINED = "declined";

	private const string ATTRIBUTE_VALUE_DECK_SHARE_STATE_ERROR = "error";

	private const string ATTRIBUTE_VALUE_DECK_SHARE_ENABLED = "deckShareEnabled";

	private const string ATTRIBUTE_VALUE_DECK_SHARE_DISABLED = "deckShareDisabled";

	public bool IsChallengeFriendlyDuel
	{
		get
		{
			if (!IsChallengeStandardDuel() && !IsChallengeWildDuel())
			{
				return IsChallengeClassicDuel();
			}
			return true;
		}
	}

	public static FriendChallengeMgr Get()
	{
		if (s_instance == null)
		{
			s_instance = new FriendChallengeMgr();
			HearthstoneApplication.Get().WillReset += s_instance.WillReset;
			AchieveManager.Get().RegisterAchievesUpdatedListener(s_instance.AchieveManager_OnAchievesUpdated);
			BnetParty.OnJoined += s_instance.BnetParty_OnJoined;
			BnetParty.OnReceivedInvite += s_instance.BnetParty_OnReceivedInvite;
			BnetParty.OnPartyAttributeChanged += s_instance.BnetParty_OnPartyAttributeChanged;
			BnetParty.OnMemberEvent += s_instance.BnetParty_OnMemberEvent;
			BnetParty.OnSentInvite += s_instance.BnetParty_OnSentInvite;
		}
		return s_instance;
	}

	public void OnLoggedIn()
	{
		NetCache.Get().RegisterFriendChallenge(OnNetCacheReady);
		SceneMgr.Get().RegisterSceneUnloadedEvent(OnSceneUnloaded);
		SceneMgr.Get().RegisterSceneLoadedEvent(OnSceneLoaded);
		BnetPresenceMgr.Get().AddPlayersChangedListener(OnPlayersChanged);
		BnetFriendMgr.Get().AddChangeListener(OnFriendsChanged);
		BnetNearbyPlayerMgr.Get().AddChangeListener(OnNearbyPlayersChanged);
		BnetEventMgr.Get().AddChangeListener(OnBnetEventOccurred);
		GameMgr.Get().RegisterFindGameEvent(OnFindGameEvent);
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
		LoginManager.Get().OnInitialClientStateReceived += OnReconnectLoginComplete;
		AddChangedListener(OnChallengeChanged);
		BnetPresenceMgr.Get().SetGameField(19u, BattleNet.GetVersion());
		BnetPresenceMgr.Get().SetGameField(20u, BattleNet.GetEnvironment());
	}

	private void BnetParty_OnJoined(OnlineEventType evt, PartyInfo party, LeaveReason? reason)
	{
		if (party.Type != PartyType.FRIENDLY_CHALLENGE)
		{
			return;
		}
		switch (evt)
		{
		case OnlineEventType.ADDED:
		{
			if (DidSendChallenge() && !BnetParty.IsLeader(party.Id))
			{
				BnetParty.DissolveParty(party.Id.ToBnetEntityId());
				break;
			}
			if (m_data.m_partyId != null && m_data.m_partyId != party.Id.ToBnetEntityId())
			{
				BnetParty.DissolveParty(party.Id.ToBnetEntityId());
				break;
			}
			m_data.m_partyId = party.Id.ToBnetEntityId();
			long? partyAttributeLong = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Game.ScenarioId");
			if (partyAttributeLong.HasValue)
			{
				m_data.m_scenarioId = (int)partyAttributeLong.Value;
			}
			long? partyAttributeLong2 = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Format.Type");
			if (partyAttributeLong2.HasValue)
			{
				m_data.m_challengeFormatType = (FormatType)partyAttributeLong2.Value;
			}
			else
			{
				m_data.m_challengeFormatType = FormatType.FT_UNKNOWN;
			}
			m_data.m_challengeBrawlType = BrawlType.BRAWL_TYPE_UNKNOWN;
			long? partyAttributeLong3 = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Brawl.Type");
			if (partyAttributeLong3.HasValue && partyAttributeLong3.Value >= 1 && partyAttributeLong3.Value < 3)
			{
				m_data.m_challengeBrawlType = (BrawlType)partyAttributeLong3.Value;
			}
			m_data.m_seasonId = 0;
			long? partyAttributeLong4 = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Season.Id");
			if (partyAttributeLong4.HasValue)
			{
				m_data.m_seasonId = (int)partyAttributeLong4.Value;
			}
			m_data.m_brawlLibraryItemId = 0;
			long? partyAttributeLong5 = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Brawl.LibraryItemId");
			if (partyAttributeLong5.HasValue)
			{
				m_data.m_brawlLibraryItemId = (int)partyAttributeLong5.Value;
			}
			string attributeKey = (DidSendChallenge() ? "s1" : "s2");
			BnetParty.SetPartyAttributeString(party.Id, attributeKey, "wait");
			UpdateMyFsgSharedSecret(party.Id, FiresideGatheringManager.Get().CurrentFsgSharedSecretKey);
			m_data.m_challengerDeckShareState = "none";
			m_data.m_challengeeDeckShareState = "none";
			m_data.m_sharedDecks = null;
			if (DidSendChallenge())
			{
				BnetParty.SendInvite(party.Id, m_data.m_challengee.GetHearthstoneGameAccountId(), isReservation: true);
			}
			else
			{
				Attribute[] allPartyAttributesVariant = BnetParty.GetAllPartyAttributesVariant(party.Id);
				foreach (Attribute attribute in allPartyAttributesVariant)
				{
					BnetParty_OnPartyAttributeChanged(party, attribute.Name, attribute.Value);
				}
			}
			if (m_data.m_challengerDeckId != 0L)
			{
				SelectDeck(m_data.m_challengerDeckId);
			}
			if (m_data.m_challengerHeroId != 0L)
			{
				SelectHero(m_data.m_challengerHeroId);
			}
			break;
		}
		case OnlineEventType.REMOVED:
			if (!BnetParty.GetJoinedParties().Any((PartyInfo i) => i.Type == PartyType.FRIENDLY_CHALLENGE))
			{
				m_data.m_scenarioId = 2;
			}
			if (party.Id.ToBnetEntityId() == m_data.m_partyId)
			{
				string data = (reason.HasValue ? ((int)reason.Value).ToString() : "NO_SUPPLIED_REASON");
				PushPartyEvent(party.Id.ToBnetEntityId(), "left", data);
			}
			break;
		}
	}

	private void BnetParty_OnReceivedInvite(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, InviteRemoveReason? reason)
	{
		if (party.Type == PartyType.FRIENDLY_CHALLENGE && evt == OnlineEventType.ADDED)
		{
			if (!PartyManager.IsPartyTypeEnabledInGuardian(party.Type))
			{
				BnetParty.DeclineReceivedInvite(inviteId);
			}
			else if (BnetParty.IsInParty(m_data.m_partyId) || DidSendChallenge())
			{
				BnetParty.DeclineReceivedInvite(inviteId);
			}
			else
			{
				BnetParty.AcceptReceivedInvite(inviteId);
			}
		}
	}

	private void BnetParty_OnPartyAttributeChanged(PartyInfo party, string attributeKey, Variant value)
	{
		if (party.Type != PartyType.FRIENDLY_CHALLENGE || m_data.m_partyId != party.Id.ToBnetEntityId())
		{
			return;
		}
		switch (attributeKey)
		{
		case "WTCG.Friendly.DeclineReason":
			BnetParty_OnPartyAttributeChanged_DeclineReason(party, attributeKey, value);
			break;
		case "error":
			BnetParty_OnPartyAttributeChanged_Error(party, attributeKey, value);
			break;
		case "d1":
			m_data.m_challengerDeckId = (value.HasIntValue ? value.IntValue : 0);
			m_data.m_challengerDeckOrHeroSelected = m_data.m_challengerDeckId > 0;
			break;
		case "d2":
			m_data.m_challengeeDeckId = (value.HasIntValue ? value.IntValue : 0);
			m_data.m_challengeeDeckOrHeroSelected = m_data.m_challengeeDeckId > 0;
			break;
		case "hero1":
			m_data.m_challengerHeroId = (value.HasIntValue ? value.IntValue : 0);
			m_data.m_challengerDeckOrHeroSelected = m_data.m_challengerHeroId > 0;
			break;
		case "hero2":
			m_data.m_challengeeHeroId = (value.HasIntValue ? value.IntValue : 0);
			m_data.m_challengeeDeckOrHeroSelected = m_data.m_challengeeHeroId > 0;
			break;
		case "fsg1":
			m_data.m_challengerFsgSharedSecret = (value.HasBlobValue ? value.BlobValue : null);
			break;
		case "fsg2":
			m_data.m_challengeeFsgSharedSecret = (value.HasBlobValue ? value.BlobValue : null);
			break;
		}
		BnetGameAccountId bnetGameAccountId = null;
		if (DidSendChallenge())
		{
			if (m_data.m_challengee != null)
			{
				bnetGameAccountId = m_data.m_challengee.GetHearthstoneGameAccountId();
			}
		}
		else if (m_data.m_challenger != null)
		{
			bnetGameAccountId = m_data.m_challenger.GetHearthstoneGameAccountId();
		}
		if (bnetGameAccountId == null)
		{
			BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
			bgs.PartyMember[] members = BnetParty.GetMembers(party.Id);
			foreach (bgs.PartyMember partyMember in members)
			{
				if (partyMember.GameAccountId != myGameAccountId)
				{
					bnetGameAccountId = partyMember.GameAccountId;
					break;
				}
			}
		}
		string data = (value.HasStringValue ? value.StringValue : string.Empty);
		PushPartyEvent(party.Id.ToBnetEntityId(), attributeKey, data, bnetGameAccountId);
	}

	private void BnetParty_OnPartyAttributeChanged_DeclineReason(PartyInfo party, string attributeKey, Variant value)
	{
		if (party.Type != PartyType.FRIENDLY_CHALLENGE || !DidSendChallenge() || !value.HasIntValue)
		{
			return;
		}
		DeclineReason declineReason = (DeclineReason)value.IntValue;
		string text = null;
		switch (declineReason)
		{
		case DeclineReason.UserIsBusy:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_USER_IS_BUSY";
			break;
		case DeclineReason.NoValidDeck:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_NO_DECK";
			break;
		case DeclineReason.StandardNoValidDeck:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_NO_STANDARD_DECK";
			break;
		case DeclineReason.TavernBrawlNoValidDeck:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_NO_TAVERN_BRAWL_DECK";
			break;
		case DeclineReason.TavernBrawlNotUnlocked:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_TAVERN_BRAWL_LOCKED";
			break;
		case DeclineReason.NotSeenWild:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_NOT_SEEN_WILD";
			break;
		case DeclineReason.BattlegroundsNoEarlyAccess:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_NO_BATTLEGROUNDS_EARLY_ACCESS";
			break;
		case DeclineReason.ClassicNoValidDeck:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_NO_CLASSIC_DECK";
			break;
		}
		if (text != null)
		{
			if (m_challengeDialog != null)
			{
				m_challengeDialog.Hide();
				m_challengeDialog = null;
			}
			m_hasSeenDeclinedReason = true;
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
				m_text = GameStrings.Get(text),
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
		}
	}

	private void BnetParty_OnPartyAttributeChanged_Error(PartyInfo party, string attributeKey, Variant value)
	{
		if (party.Type == PartyType.FRIENDLY_CHALLENGE)
		{
			if (DidReceiveChallenge() && value.HasIntValue)
			{
				Log.Party.Print(Log.LogLevel.Error, "BnetParty_OnPartyAttributeChanged_Error - code={0}", value.IntValue);
				BnetErrorInfo info = new BnetErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnCreated, (BattleNetErrors)value.IntValue);
				GameMgr.Get().OnBnetError(info, null);
			}
			if (BnetParty.IsLeader(party.Id) && !value.IsNone())
			{
				BnetParty.ClearPartyAttribute(party.Id, attributeKey);
			}
		}
	}

	private void BnetParty_OnMemberEvent(OnlineEventType evt, PartyInfo party, BnetGameAccountId memberId, bool isRolesUpdate, LeaveReason? reason)
	{
		if (party.Type == PartyType.FRIENDLY_CHALLENGE && evt == OnlineEventType.REMOVED && BnetParty.IsInParty(party.Id))
		{
			BnetParty.DissolveParty(party.Id);
		}
	}

	private void BnetParty_OnSentInvite(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, bool senderIsMyself, InviteRemoveReason? reason)
	{
		if (party.Type == PartyType.FRIENDLY_CHALLENGE && evt == OnlineEventType.REMOVED && reason == InviteRemoveReason.DECLINED)
		{
			DeclineFriendChallenge_Internal(party.Id.ToBnetEntityId());
			if (party.Id.ToBnetEntityId() == m_data.m_partyId)
			{
				BnetPlayer challengee = m_data.m_challengee;
				FriendlyChallengeData challengeData = CleanUpChallengeData();
				FireChangedEvent(FriendChallengeEvent.OPPONENT_DECLINED_CHALLENGE, challengee, challengeData);
			}
		}
	}

	private void AchieveManager_OnAchievesUpdated(List<Achievement> updatedAchieves, List<Achievement> completedAchievements, object userData)
	{
		if (completedAchievements.Any((Achievement a) => a.IsFriendlyChallengeQuest))
		{
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
			{
				m_data.m_updatePartyQuestInfoOnGameplaySceneUnload = true;
			}
			else
			{
				UpdatePartyQuestInfo();
			}
		}
	}

	private void UpdatePartyQuestInfo()
	{
		if (!DidSendChallenge() || !BnetParty.IsInParty(m_data.m_partyId))
		{
			return;
		}
		byte[] value = null;
		IEnumerable<Achievement> source = from q in AchieveManager.Get().GetActiveQuests()
			where q.IsFriendlyChallengeQuest
			select q;
		if (source.Any())
		{
			PartyQuestInfo partyQuestInfo = new PartyQuestInfo();
			partyQuestInfo.QuestIds.AddRange(source.Select((Achievement q) => q.ID));
			value = ProtobufUtil.ToByteArray(partyQuestInfo);
		}
		BnetParty.SetPartyAttributeBlob(m_data.m_partyId, "quests", value);
	}

	public void OnStoreOpened()
	{
		UpdateMyAvailability();
	}

	public void OnStoreClosed()
	{
		UpdateMyAvailability();
	}

	public BnetPlayer GetChallengee()
	{
		return m_data.m_challengee;
	}

	public BnetPlayer GetChallenger()
	{
		return m_data.m_challenger;
	}

	public bool DidReceiveChallenge()
	{
		return m_data.DidReceiveChallenge;
	}

	public bool DidSendChallenge()
	{
		return m_data.DidSendChallenge;
	}

	public bool HasChallenge()
	{
		if (!DidSendChallenge())
		{
			return DidReceiveChallenge();
		}
		return true;
	}

	public bool DidChallengeeAccept()
	{
		return m_data.m_challengeeAccepted;
	}

	public bool AmIInGameState()
	{
		if (DidSendChallenge())
		{
			return m_data.m_challengerInGameState;
		}
		return m_data.m_challengeeInGameState;
	}

	public BnetPlayer GetOpponent(BnetPlayer player)
	{
		if (player == m_data.m_challenger)
		{
			return m_data.m_challengee;
		}
		if (player == m_data.m_challengee)
		{
			return m_data.m_challenger;
		}
		return null;
	}

	public BnetPlayer GetMyOpponent()
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		return GetOpponent(myPlayer);
	}

	public bool CanChallenge(BnetPlayer player)
	{
		if (player == null)
		{
			return false;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (player == myPlayer)
		{
			return false;
		}
		if (!AmIAvailable())
		{
			return false;
		}
		if (!IsOpponentAvailable(player))
		{
			return false;
		}
		if (PartyManager.Get().IsPlayerInAnyParty(player.GetBestGameAccountId()))
		{
			return false;
		}
		return true;
	}

	public bool AmIAvailable()
	{
		if (!m_netCacheReady)
		{
			return false;
		}
		if (!m_myPlayerReady)
		{
			return false;
		}
		if (SpectatorManager.Get().IsSpectatingOrWatching)
		{
			return false;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		BnetGameAccount hearthstoneGameAccount = myPlayer.GetHearthstoneGameAccount();
		if (hearthstoneGameAccount == null)
		{
			return false;
		}
		if (!myPlayer.IsOnline() || myPlayer.IsAppearingOffline())
		{
			return false;
		}
		if (!Network.IsLoggedIn())
		{
			return false;
		}
		if (PopupDisplayManager.Get().IsShowing)
		{
			return false;
		}
		if (PartyManager.Get().IsInParty())
		{
			return false;
		}
		return hearthstoneGameAccount.CanBeInvitedToGame();
	}

	public bool IsOpponentAvailable(BnetPlayer player)
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		BnetGameAccount hearthstoneGameAccount = player.GetHearthstoneGameAccount();
		if (hearthstoneGameAccount == null)
		{
			return false;
		}
		if (!hearthstoneGameAccount.IsOnline())
		{
			return false;
		}
		if (!hearthstoneGameAccount.CanBeInvitedToGame())
		{
			return false;
		}
		if (HearthstoneApplication.IsPublic())
		{
			BnetGameAccount hearthstoneGameAccount2 = myPlayer.GetHearthstoneGameAccount();
			if (string.Compare(hearthstoneGameAccount.GetClientVersion(), hearthstoneGameAccount2.GetClientVersion()) != 0)
			{
				return false;
			}
			if (string.Compare(hearthstoneGameAccount.GetClientEnv(), hearthstoneGameAccount2.GetClientEnv()) != 0)
			{
				return false;
			}
		}
		return true;
	}

	public bool DidISelectDeckOrHero()
	{
		if (DidSendChallenge())
		{
			return m_data.m_challengerDeckOrHeroSelected;
		}
		if (DidReceiveChallenge())
		{
			return m_data.m_challengeeDeckOrHeroSelected;
		}
		return true;
	}

	public bool DidOpponentSelectDeckOrHero()
	{
		if (DidSendChallenge())
		{
			return m_data.m_challengeeDeckOrHeroSelected;
		}
		if (DidReceiveChallenge())
		{
			return m_data.m_challengerDeckOrHeroSelected;
		}
		return true;
	}

	public static void ShowChallengerNeedsToCreateTavernBrawlDeckAlert()
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
			m_text = GameStrings.Format("GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGER_NO_TAVERN_BRAWL_DECK"),
			m_showAlertIcon = true,
			m_responseDisplay = AlertPopup.ResponseDisplay.OK
		};
		DialogManager.Get().ShowPopup(info);
	}

	public void SendChallenge(BnetPlayer player, FormatType formatType, bool enableDeckShare)
	{
		if (CanChallenge(player))
		{
			SendChallenge_Internal(player, formatType, BrawlType.BRAWL_TYPE_UNKNOWN, enableDeckShare, 0, 0, isBaconGame: false);
		}
	}

	public void SendTavernBrawlChallenge(BnetPlayer player, BrawlType brawlType, int seasonId, int brawlLibraryItemId)
	{
		if (CanChallenge(player))
		{
			TavernBrawlManager.Get().EnsureAllDataReady(brawlType, delegate
			{
				TavernBrawl_SendChallenge_OnEnsureServerDataReady(player, brawlType, seasonId, brawlLibraryItemId);
			});
		}
	}

	private void TavernBrawl_SendChallenge_OnEnsureServerDataReady(BnetPlayer player, BrawlType brawlType, int seasonId, int brawlLibraryItemId)
	{
		TavernBrawlManager tavernBrawlManager = TavernBrawlManager.Get();
		if (CanChallenge(player) && tavernBrawlManager.IsTavernBrawlActive(brawlType) && !HasChallenge())
		{
			if (!tavernBrawlManager.CanChallengeToTavernBrawl(brawlType))
			{
				AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
				{
					m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
					m_text = GameStrings.Format("GLOBAL_FRIENDLIST_CHALLENGE_TOOLTIP_TAVERN_BRAWL_NOT_CHALLENGEABLE"),
					m_showAlertIcon = true,
					m_responseDisplay = AlertPopup.ResponseDisplay.OK
				};
				DialogManager.Get().ShowPopup(info);
			}
			else if (tavernBrawlManager.GetMission(brawlType).canCreateDeck && !tavernBrawlManager.HasValidDeck(brawlType))
			{
				ShowChallengerNeedsToCreateTavernBrawlDeckAlert();
			}
			else
			{
				SendChallenge_Internal(player, FormatType.FT_UNKNOWN, brawlType, enableDeckShare: false, seasonId, brawlLibraryItemId, isBaconGame: false);
			}
		}
	}

	private void SendChallenge_Internal(BnetPlayer player, FormatType formatType, BrawlType brawlType, bool enableDeckShare, int seasonId, int brawlLibraryItemId, bool isBaconGame)
	{
		if (m_data.m_partyId != null)
		{
			BnetParty.DissolveParty(m_data.m_partyId);
		}
		CleanUpChallengeData();
		if (m_hasPreSelectedDeckOrHero)
		{
			m_data.m_challengerDeckId = m_preSelectedDeckId;
			m_data.m_challengerHeroId = m_preSelectedHeroId;
			m_data.m_challengerDeckOrHeroSelected = m_hasPreSelectedDeckOrHero;
		}
		m_data.m_challenger = BnetPresenceMgr.Get().GetMyPlayer();
		m_data.m_challengerId = m_data.m_challenger.GetHearthstoneGameAccount().GetId();
		m_data.m_challengee = player;
		m_hasSeenDeclinedReason = false;
		m_data.m_scenarioId = 2;
		m_data.m_seasonId = seasonId;
		m_data.m_brawlLibraryItemId = brawlLibraryItemId;
		m_data.m_challengeBrawlType = brawlType;
		m_data.m_challengeFormatType = formatType;
		if (isBaconGame)
		{
			m_data.m_scenarioId = 3459;
		}
		else if (IsChallengeTavernBrawl())
		{
			TavernBrawlManager.Get().CurrentBrawlType = m_data.m_challengeBrawlType;
			TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(brawlType);
			mission.SetSelectedBrawlLibraryItemId(brawlLibraryItemId);
			m_data.m_scenarioId = mission.missionId;
			m_data.m_challengeFormatType = mission.formatType;
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING);
		}
		Attribute attributeV = null;
		IEnumerable<Achievement> source = from q in AchieveManager.Get().GetActiveQuests()
			where q.IsFriendlyChallengeQuest
			select q;
		if (source.Any())
		{
			PartyQuestInfo partyQuestInfo = new PartyQuestInfo();
			partyQuestInfo.QuestIds.AddRange(source.Select((Achievement q) => q.ID));
			byte[] val = ProtobufUtil.ToByteArray(partyQuestInfo);
			attributeV = ProtocolHelper.CreateAttribute("quests", val);
		}
		Attribute attributeV2 = ProtocolHelper.CreateAttribute("WTCG.Game.ScenarioId", m_data.m_scenarioId);
		Attribute attributeV3 = ProtocolHelper.CreateAttribute("WTCG.Format.Type", (long)m_data.m_challengeFormatType);
		Attribute attributeV4 = (IsChallengeTavernBrawl() ? ProtocolHelper.CreateAttribute("WTCG.Brawl.Type", (long)m_data.m_challengeBrawlType) : null);
		Attribute attributeV5 = ProtocolHelper.CreateAttribute("WTCG.Season.Id", m_data.m_seasonId);
		Attribute attributeV6 = (IsChallengeTavernBrawl() ? ProtocolHelper.CreateAttribute("WTCG.Brawl.LibraryItemId", m_data.m_brawlLibraryItemId) : null);
		Attribute attributeV7 = null;
		if (FiresideGatheringManager.Get().IsCheckedIn && FiresideGatheringManager.Get().CurrentFsgSharedSecretKey != null)
		{
			byte[] val2 = SHA256.Create().ComputeHash(FiresideGatheringManager.Get().CurrentFsgSharedSecretKey, 0, FiresideGatheringManager.Get().CurrentFsgSharedSecretKey.Length);
			attributeV7 = ProtocolHelper.CreateAttribute("fsg1", val2);
		}
		Attribute attributeV8 = ((m_data.m_challengerDeckId != 0L) ? ProtocolHelper.CreateAttribute("d1", m_data.m_challengerDeckId) : null);
		Attribute attributeV9 = ((m_data.m_challengerHeroId != 0L) ? ProtocolHelper.CreateAttribute("hero1", m_data.m_challengerHeroId) : null);
		Attribute attributeV10 = (m_data.m_challengerDeckOrHeroSelected ? ProtocolHelper.CreateAttribute("s1", "ready") : null);
		string val3 = (enableDeckShare ? "deckShareEnabled" : "deckShareDisabled");
		Attribute attributeV11 = ProtocolHelper.CreateAttribute("isDeckShareEnabled", val3);
		Attribute attributeV12 = ProtocolHelper.CreateAttribute("p1DeckShareState", "none");
		Attribute attributeV13 = ProtocolHelper.CreateAttribute("p2DeckShareState", "none");
		BnetParty.CreateParty(PartyType.FRIENDLY_CHALLENGE, PrivacyLevel.OPEN_INVITATION, null, ProtocolHelper.V1AttributeToV2Attribute(attributeV2), ProtocolHelper.V1AttributeToV2Attribute(attributeV3), ProtocolHelper.V1AttributeToV2Attribute(attributeV), ProtocolHelper.V1AttributeToV2Attribute(attributeV4), ProtocolHelper.V1AttributeToV2Attribute(attributeV5), ProtocolHelper.V1AttributeToV2Attribute(attributeV6), ProtocolHelper.V1AttributeToV2Attribute(attributeV8), ProtocolHelper.V1AttributeToV2Attribute(attributeV9), ProtocolHelper.V1AttributeToV2Attribute(attributeV10), ProtocolHelper.V1AttributeToV2Attribute(attributeV7), ProtocolHelper.V1AttributeToV2Attribute(attributeV11), ProtocolHelper.V1AttributeToV2Attribute(attributeV12), ProtocolHelper.V1AttributeToV2Attribute(attributeV13));
		UpdateMyAvailability();
		FireChangedEvent(FriendChallengeEvent.I_SENT_CHALLENGE, player);
	}

	public void CancelChallenge()
	{
		if (HasChallenge())
		{
			if (DidSendChallenge())
			{
				RescindChallenge();
			}
			else if (DidReceiveChallenge())
			{
				DeclineChallenge();
			}
		}
	}

	public void AcceptChallenge()
	{
		if (DidReceiveChallenge())
		{
			m_data.m_challengeeAccepted = true;
			string attributeKey = (DidSendChallenge() ? "s1" : "s2");
			BnetParty.SetPartyAttributeString(m_data.m_partyId, attributeKey, "deck");
			FireChangedEvent(FriendChallengeEvent.I_ACCEPTED_CHALLENGE, m_data.m_challenger);
		}
	}

	public void DeclineChallenge()
	{
		if (DidReceiveChallenge())
		{
			RevertTavernBrawlPresenceStatus();
			DeclineFriendChallenge_Internal(m_data.m_partyId);
			BnetPlayer challenger = m_data.m_challenger;
			FriendlyChallengeData challengeData = CleanUpChallengeData();
			FireChangedEvent(FriendChallengeEvent.I_DECLINED_CHALLENGE, challenger, challengeData);
		}
	}

	private void DeclineFriendChallenge_Internal(BnetEntityId partyId)
	{
		if (BnetParty.IsInParty(partyId))
		{
			BnetParty.DissolveParty(partyId);
		}
	}

	public void QueueCanceled()
	{
		BnetPlayer player;
		if (DidReceiveChallenge())
		{
			player = m_data.m_challenger;
		}
		else
		{
			if (!DidSendChallenge())
			{
				return;
			}
			player = m_data.m_challengee;
		}
		FriendlyChallengeData challengeData = CleanUpChallengeData();
		FireChangedEvent(FriendChallengeEvent.QUEUE_CANCELED, player, challengeData);
	}

	private void PushPartyEvent(BnetEntityId partyId, string type, string data, BnetGameAccountId otherPlayerGameAccountId = null)
	{
		if (otherPlayerGameAccountId == null)
		{
			otherPlayerGameAccountId = (DidSendChallenge() ? m_data.m_challenger : m_data.m_challengee)?.GetHearthstoneGameAccountId();
		}
		PartyEvent partyEvent = default(PartyEvent);
		partyEvent.partyId.hi = partyId.GetHi();
		partyEvent.partyId.lo = partyId.GetLo();
		partyEvent.eventName = type;
		partyEvent.eventData = data;
		if (otherPlayerGameAccountId != null)
		{
			partyEvent.otherMemberId.hi = otherPlayerGameAccountId.GetHi();
			partyEvent.otherMemberId.lo = otherPlayerGameAccountId.GetLo();
		}
		OnPartyUpdate(new PartyEvent[1] { partyEvent });
	}

	public void RescindChallenge()
	{
		if (DidSendChallenge())
		{
			RevertTavernBrawlPresenceStatus();
			if (BnetParty.IsInParty(m_data.m_partyId))
			{
				BnetParty.DissolveParty(m_data.m_partyId);
			}
			BnetPlayer challengee = m_data.m_challengee;
			FriendlyChallengeData challengeData = CleanUpChallengeData();
			FireChangedEvent(FriendChallengeEvent.I_RESCINDED_CHALLENGE, challengee, challengeData);
		}
	}

	public bool IsChallengeStandardDuel()
	{
		if (!HasChallenge())
		{
			return false;
		}
		if (!IsChallengeTavernBrawl())
		{
			return m_data.m_challengeFormatType == FormatType.FT_STANDARD;
		}
		return false;
	}

	public bool IsChallengeWildDuel()
	{
		if (!HasChallenge())
		{
			return false;
		}
		if (!IsChallengeTavernBrawl())
		{
			return m_data.m_challengeFormatType == FormatType.FT_WILD;
		}
		return false;
	}

	public bool IsChallengeClassicDuel()
	{
		if (!HasChallenge())
		{
			return false;
		}
		if (!IsChallengeTavernBrawl())
		{
			return m_data.m_challengeFormatType == FormatType.FT_CLASSIC;
		}
		return false;
	}

	public bool IsChallengeTavernBrawl()
	{
		if (!HasChallenge())
		{
			return false;
		}
		return m_data.m_challengeBrawlType != BrawlType.BRAWL_TYPE_UNKNOWN;
	}

	public bool IsChallengeFiresideBrawl()
	{
		if (IsChallengeTavernBrawl())
		{
			return m_data.m_challengeBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING;
		}
		return false;
	}

	public bool IsChallengeBacon()
	{
		if (!HasChallenge())
		{
			return false;
		}
		return m_data.m_scenarioId == 3459;
	}

	public BrawlType GetChallengeBrawlType()
	{
		if (!HasChallenge())
		{
			return BrawlType.BRAWL_TYPE_UNKNOWN;
		}
		return m_data.m_challengeBrawlType;
	}

	public bool IsDeckShareEnabled()
	{
		if (!HasChallenge())
		{
			return false;
		}
		return BnetParty.GetPartyAttributeString(m_data.m_partyId, "isDeckShareEnabled") == "deckShareEnabled";
	}

	public void RequestDeckShare()
	{
		if (DidSendChallenge())
		{
			if (BnetParty.GetPartyAttributeString(m_data.m_partyId, "p1DeckShareState") == "sharingUnused")
			{
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "p1DeckShareState", "sharing");
			}
			else if (BnetParty.GetPartyAttributeString(m_data.m_partyId, "p1DeckShareState") == "none")
			{
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "p1DeckShareState", "requested");
			}
		}
		else if (DidReceiveChallenge())
		{
			if (BnetParty.GetPartyAttributeString(m_data.m_partyId, "p2DeckShareState") == "sharingUnused")
			{
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "p2DeckShareState", "sharing");
			}
			else if (BnetParty.GetPartyAttributeString(m_data.m_partyId, "p2DeckShareState") == "none")
			{
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "p2DeckShareState", "requested");
			}
		}
	}

	public void EndDeckShare()
	{
		if (DidSendChallenge())
		{
			if (BnetParty.GetPartyAttributeString(m_data.m_partyId, "p1DeckShareState") == "sharing")
			{
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "p1DeckShareState", "sharingUnused");
			}
		}
		else if (DidReceiveChallenge() && BnetParty.GetPartyAttributeString(m_data.m_partyId, "p2DeckShareState") == "sharing")
		{
			BnetParty.SetPartyAttributeString(m_data.m_partyId, "p2DeckShareState", "sharingUnused");
		}
	}

	private void ShareDecks_InternalParty()
	{
		List<CollectionDeck> decks = CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK);
		byte[] array = SerializeSharedDecks(decks);
		if (array == null)
		{
			Log.Party.PrintError("{0}.ShareDecks_InternalParty(): Unable to Serialize decks!.", this);
			if (DidSendChallenge())
			{
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "p2DeckShareState", "error");
			}
			else if (DidReceiveChallenge())
			{
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "p1DeckShareState", "error");
			}
		}
		else if (DidSendChallenge())
		{
			BnetParty.SetPartyAttributeBlob(m_data.m_partyId, "p1DeckShareDecks", array);
		}
		else if (DidReceiveChallenge())
		{
			BnetParty.SetPartyAttributeBlob(m_data.m_partyId, "p2DeckShareDecks", array);
		}
	}

	public List<CollectionDeck> GetSharedDecks()
	{
		if (m_data.m_sharedDecks != null)
		{
			return new List<CollectionDeck>(m_data.m_sharedDecks);
		}
		byte[] array = null;
		if (DidSendChallenge() && (m_data.m_challengerDeckShareState == "sharing" || m_data.m_challengerDeckShareState == "sharingUnused"))
		{
			array = BnetParty.GetPartyAttributeBlob(m_data.m_partyId, "p2DeckShareDecks");
		}
		else if (DidReceiveChallenge() && (m_data.m_challengeeDeckShareState == "sharing" || m_data.m_challengeeDeckShareState == "sharingUnused"))
		{
			array = BnetParty.GetPartyAttributeBlob(m_data.m_partyId, "p1DeckShareDecks");
		}
		if (array == null)
		{
			return null;
		}
		return DeserializeSharedDecks(array);
	}

	private byte[] SerializeSharedDecks(List<CollectionDeck> collectionDecks)
	{
		if (collectionDecks == null || collectionDecks.Count <= 0)
		{
			return null;
		}
		DeckList deckList = new DeckList();
		FormatType formatType = Options.GetFormatType();
		foreach (CollectionDeck collectionDeck in collectionDecks)
		{
			if (collectionDeck.IsValidForRuleset && collectionDeck.IsValidForFormat(formatType))
			{
				ulong num = 0uL;
				if (collectionDeck.NeedsName)
				{
					num |= 0x200;
				}
				if (formatType == FormatType.FT_STANDARD)
				{
					num |= 0x80;
				}
				if (collectionDeck.Locked)
				{
					num |= 0x400;
				}
				DeckInfo deckInfo = new DeckInfo
				{
					Id = collectionDeck.ID,
					Name = collectionDeck.Name,
					Hero = GameUtils.TranslateCardIdToDbId(collectionDeck.HeroCardID),
					HeroPremium = (int)collectionDeck.HeroPremium,
					DeckType = collectionDeck.Type,
					CardBack = collectionDeck.CardBackID,
					CardBackOverride = collectionDeck.CardBackOverridden,
					HeroOverride = collectionDeck.HeroOverridden,
					SeasonId = collectionDeck.SeasonId,
					BrawlLibraryItemId = collectionDeck.BrawlLibraryItemId,
					SortOrder = collectionDeck.SortOrder,
					FormatType = collectionDeck.FormatType,
					SourceType = collectionDeck.SourceType,
					Validity = num
				};
				if (collectionDeck.HasUIHeroOverride())
				{
					deckInfo.UiHeroOverride = GameUtils.TranslateCardIdToDbId(collectionDeck.UIHeroOverrideCardID);
					deckInfo.UiHeroOverridePremium = (int)collectionDeck.UIHeroOverridePremium;
				}
				deckList.Decks.Add(deckInfo);
			}
		}
		return ProtobufUtil.ToByteArray(deckList);
	}

	private List<CollectionDeck> DeserializeSharedDecks(byte[] blob)
	{
		if (blob == null)
		{
			return null;
		}
		try
		{
			DeckList deckList = ProtobufUtil.ParseFrom<DeckList>(blob);
			m_data.m_sharedDecks = new List<CollectionDeck>();
			foreach (DeckInfo deck in deckList.Decks)
			{
				CollectionDeck collectionDeck = new CollectionDeck
				{
					ID = deck.Id,
					Name = deck.Name,
					HeroCardID = GameUtils.TranslateDbIdToCardId(deck.Hero),
					HeroPremium = (TAG_PREMIUM)deck.HeroPremium,
					Type = deck.DeckType,
					CardBackID = deck.CardBack,
					CardBackOverridden = deck.CardBackOverride,
					HeroOverridden = deck.HeroOverride,
					SeasonId = deck.SeasonId,
					BrawlLibraryItemId = deck.BrawlLibraryItemId,
					NeedsName = Network.DeckNeedsName(deck.Validity),
					SortOrder = (deck.HasSortOrder ? deck.SortOrder : deck.Id),
					FormatType = deck.FormatType,
					SourceType = (deck.HasSourceType ? deck.SourceType : DeckSourceType.DECK_SOURCE_TYPE_UNKNOWN),
					Locked = Network.AreDeckFlagsLocked(deck.Validity),
					IsShared = true
				};
				if (deck.HasUiHeroOverride)
				{
					collectionDeck.UIHeroOverrideCardID = GameUtils.TranslateDbIdToCardId(deck.UiHeroOverride);
					collectionDeck.UIHeroOverridePremium = (TAG_PREMIUM)deck.UiHeroOverridePremium;
				}
				m_data.m_sharedDecks.Add(collectionDeck);
			}
		}
		catch
		{
			Log.Party.PrintError("{0}.ShareDecks_InternalParty(): Unable to Deserialize decks!.", this);
			m_data.m_sharedDecks = null;
		}
		return m_data.m_sharedDecks;
	}

	public bool HasOpponentSharedDecks()
	{
		return GetSharedDecks() != null;
	}

	public bool ShouldUseSharedDecks()
	{
		if (!HasOpponentSharedDecks())
		{
			return false;
		}
		if (DidSendChallenge() && m_data.m_challengerDeckShareState != "sharing")
		{
			return false;
		}
		if (DidReceiveChallenge() && m_data.m_challengeeDeckShareState != "sharing")
		{
			return false;
		}
		return true;
	}

	private void OnFriendChallengeDeckShareRequestDialogWaitingResponse(AlertPopup.Response response, object userData)
	{
		if (response != AlertPopup.Response.CANCEL)
		{
			return;
		}
		if (DidSendChallenge())
		{
			if (BnetParty.GetPartyAttributeString(m_data.m_partyId, "p1DeckShareState") == "requested")
			{
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "p1DeckShareState", "none");
			}
		}
		else if (DidReceiveChallenge() && BnetParty.GetPartyAttributeString(m_data.m_partyId, "p2DeckShareState") == "requested")
		{
			BnetParty.SetPartyAttributeString(m_data.m_partyId, "p2DeckShareState", "none");
		}
	}

	private void OnFriendChallengeDeckShareRequestDialogResponse(AlertPopup.Response response, object userData)
	{
		string value = ((response == AlertPopup.Response.CANCEL) ? "declined" : "sharing");
		if (DidSendChallenge())
		{
			if (BnetParty.GetPartyAttributeString(m_data.m_partyId, "p2DeckShareState") == "requested")
			{
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "p2DeckShareState", value);
			}
			if (BnetParty.GetPartyAttributeString(m_data.m_partyId, "p1DeckShareState") == "requested")
			{
				FriendlyChallengeHelper.Get().ShowDeckShareRequestWaitingDialog(OnFriendChallengeDeckShareRequestDialogWaitingResponse);
			}
		}
		else if (DidReceiveChallenge())
		{
			if (BnetParty.GetPartyAttributeString(m_data.m_partyId, "p1DeckShareState") == "requested")
			{
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "p1DeckShareState", value);
			}
			if (BnetParty.GetPartyAttributeString(m_data.m_partyId, "p2DeckShareState") == "requested")
			{
				FriendlyChallengeHelper.Get().ShowDeckShareRequestWaitingDialog(OnFriendChallengeDeckShareRequestDialogWaitingResponse);
			}
		}
	}

	private DeckShareState GetDeckShareStateEnumFromAttribute(string deckShareStateAttribute)
	{
		DeckShareState result = DeckShareState.NO_DECK_SHARE;
		if (deckShareStateAttribute == "sharingUnused")
		{
			result = DeckShareState.DECK_SHARED_UNUSED;
		}
		else if (deckShareStateAttribute == "sharing")
		{
			result = DeckShareState.USING_SHARED_DECK;
		}
		return result;
	}

	public void SkipDeckSelection()
	{
		SelectDeck(1L);
	}

	public void SelectDeck(long deckId)
	{
		if (DidSendChallenge())
		{
			m_data.m_challengerDeckOrHeroSelected = true;
		}
		else
		{
			if (!DidReceiveChallenge())
			{
				return;
			}
			m_data.m_challengeeDeckOrHeroSelected = true;
		}
		SelectMyDeck_InternalParty(deckId);
		FireChangedEvent(FriendChallengeEvent.SELECTED_DECK_OR_HERO, BnetPresenceMgr.Get().GetMyPlayer());
	}

	public void SelectDeckBeforeSendingChallenge(long deckId)
	{
		m_hasPreSelectedDeckOrHero = true;
		m_preSelectedDeckId = deckId;
	}

	public void ClearSelectedDeckAndHeroBeforeSendingChallenge()
	{
		m_hasPreSelectedDeckOrHero = false;
		m_preSelectedDeckId = 0L;
		m_preSelectedHeroId = 0L;
	}

	public void SelectHero(long heroCardDbId)
	{
		if (DidSendChallenge())
		{
			m_data.m_challengerDeckOrHeroSelected = true;
		}
		else
		{
			if (!DidReceiveChallenge())
			{
				return;
			}
			m_data.m_challengeeDeckOrHeroSelected = true;
		}
		SelectMyHero_InternalParty(heroCardDbId);
		FireChangedEvent(FriendChallengeEvent.SELECTED_DECK_OR_HERO, BnetPresenceMgr.Get().GetMyPlayer());
	}

	public void SelectHeroBeforeSendingChallenge(long heroCardDbId)
	{
		m_hasPreSelectedDeckOrHero = true;
		m_preSelectedHeroId = heroCardDbId;
	}

	public void DeselectDeckOrHero()
	{
		if (m_hasPreSelectedDeckOrHero)
		{
			m_hasPreSelectedDeckOrHero = false;
			m_preSelectedDeckId = 0L;
			m_preSelectedHeroId = 0L;
		}
		if (DidSendChallenge() && m_data.m_challengerDeckOrHeroSelected)
		{
			m_data.m_challengerDeckOrHeroSelected = false;
			m_data.m_challengerDeckId = 0L;
			m_data.m_challengerHeroId = 0L;
			m_data.m_challengerInGameState = false;
		}
		else
		{
			if (!DidReceiveChallenge() || !m_data.m_challengeeDeckOrHeroSelected)
			{
				return;
			}
			m_data.m_challengeeDeckOrHeroSelected = false;
			m_data.m_challengeeDeckId = 0L;
			m_data.m_challengeeHeroId = 0L;
			m_data.m_challengeeInGameState = false;
		}
		SelectMyDeck_InternalParty(0L);
		SelectMyHero_InternalParty(0L);
		FireChangedEvent(FriendChallengeEvent.DESELECTED_DECK_OR_HERO, BnetPresenceMgr.Get().GetMyPlayer());
	}

	public void SetChallengeMethod(ChallengeMethod challengeMethod)
	{
		m_challengeMethod = challengeMethod;
	}

	private bool ShouldTransitionToFriendlySceneAccordingToChallengeMethod()
	{
		return m_challengeMethod != ChallengeMethod.FROM_FIRESIDE_GATHERING_OPPONENT_PICKER;
	}

	private void SelectMyDeck_InternalParty(long deckId)
	{
		string val = ((deckId == 0L) ? "deck" : "ready");
		Attribute[] attributeV;
		if (DidSendChallenge())
		{
			m_data.m_challengerDeckId = deckId;
			attributeV = new Attribute[2]
			{
				ProtocolHelper.CreateAttribute("s1", val),
				ProtocolHelper.CreateAttribute("d1", deckId)
			};
		}
		else
		{
			m_data.m_challengeeDeckId = deckId;
			attributeV = new Attribute[2]
			{
				ProtocolHelper.CreateAttribute("s2", val),
				ProtocolHelper.CreateAttribute("d2", deckId)
			};
		}
		BnetParty.SetPartyAttributes(m_data.m_partyId, ProtocolHelper.V1AttributeToV2Attribute(attributeV));
	}

	private void SelectMyHero_InternalParty(long heroCardDbId)
	{
		string val = ((heroCardDbId == 0L) ? "deck" : "ready");
		Attribute[] attributeV;
		if (DidSendChallenge())
		{
			m_data.m_challengerHeroId = heroCardDbId;
			attributeV = new Attribute[2]
			{
				ProtocolHelper.CreateAttribute("s1", val),
				ProtocolHelper.CreateAttribute("hero1", heroCardDbId)
			};
		}
		else
		{
			m_data.m_challengeeHeroId = heroCardDbId;
			attributeV = new Attribute[2]
			{
				ProtocolHelper.CreateAttribute("s2", val),
				ProtocolHelper.CreateAttribute("hero2", heroCardDbId)
			};
		}
		BnetParty.SetPartyAttributes(m_data.m_partyId, ProtocolHelper.V1AttributeToV2Attribute(attributeV));
	}

	private void SetMyFsgSharedSecretKey_InternalParty(PartyId partyId, byte[] fsgSharedSecretKey)
	{
		Attribute[] attributeV;
		if (DidSendChallenge())
		{
			m_data.m_challengerFsgSharedSecret = fsgSharedSecretKey;
			attributeV = new Attribute[1] { ProtocolHelper.CreateAttribute("fsg1", fsgSharedSecretKey) };
		}
		else
		{
			m_data.m_challengeeFsgSharedSecret = fsgSharedSecretKey;
			attributeV = new Attribute[1] { ProtocolHelper.CreateAttribute("fsg2", fsgSharedSecretKey) };
		}
		BnetParty.SetPartyAttributes(partyId, ProtocolHelper.V1AttributeToV2Attribute(attributeV));
	}

	public int GetScenarioId()
	{
		return m_data.m_scenarioId;
	}

	public FormatType GetFormatType()
	{
		return m_data.m_challengeFormatType;
	}

	public PartyQuestInfo GetPartyQuestInfo()
	{
		PartyQuestInfo result = null;
		byte[] partyAttributeBlob = BnetParty.GetPartyAttributeBlob(m_data.m_partyId, "quests");
		if (partyAttributeBlob != null && partyAttributeBlob.Length != 0)
		{
			result = ProtobufUtil.ParseFrom<PartyQuestInfo>(partyAttributeBlob);
		}
		return result;
	}

	public bool PlayersInSameFiresideGathering()
	{
		if (m_data.m_challengerFsgSharedSecret != null && m_data.m_challengeeFsgSharedSecret != null)
		{
			return GeneralUtils.AreArraysEqual(m_data.m_challengerFsgSharedSecret, m_data.m_challengeeFsgSharedSecret);
		}
		return false;
	}

	public void UpdateMyFsgSharedSecret(byte[] currentFsgSharedSecretKey)
	{
		UpdateMyFsgSharedSecret(m_data.m_partyId, currentFsgSharedSecretKey);
	}

	public void UpdateMyFsgSharedSecret(PartyId partyId, byte[] currentFsgSharedSecretKey)
	{
		if (!(partyId == null))
		{
			if (!FiresideGatheringManager.Get().IsCheckedIn || currentFsgSharedSecretKey == null)
			{
				SetMyFsgSharedSecretKey_InternalParty(partyId, null);
				return;
			}
			byte[] fsgSharedSecretKey = SHA256.Create().ComputeHash(currentFsgSharedSecretKey, 0, currentFsgSharedSecretKey.Length);
			SetMyFsgSharedSecretKey_InternalParty(partyId, fsgSharedSecretKey);
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

	private bool RemoveChangedListener(ChangedCallback callback, object userData)
	{
		ChangedListener changedListener = new ChangedListener();
		changedListener.SetCallback(callback);
		changedListener.SetUserData(userData);
		return m_changedListeners.Remove(changedListener);
	}

	public static bool RemoveChangedListenerFromInstance(ChangedCallback callback, object userData = null)
	{
		if (s_instance == null)
		{
			return false;
		}
		return s_instance.RemoveChangedListener(callback, userData);
	}

	private void OnPartyUpdate(PartyEvent[] updates)
	{
		for (int i = 0; i < updates.Length; i++)
		{
			PartyEvent partyEvent = updates[i];
			BnetEntityId partyId = BnetEntityId.CreateFromEntityId(partyEvent.partyId);
			BnetGameAccountId otherMemberId = BnetGameAccountId.CreateFromEntityId(partyEvent.otherMemberId);
			if (partyEvent.eventName == "s1")
			{
				if (partyEvent.eventData == "wait")
				{
					OnPartyUpdate_CreatedParty(partyId, otherMemberId);
				}
				else if (partyEvent.eventData == "deck")
				{
					if (DidReceiveChallenge() && m_data.m_challengerDeckOrHeroSelected)
					{
						m_data.m_challengerDeckOrHeroSelected = false;
						m_data.m_challengerInGameState = false;
						FireChangedEvent(FriendChallengeEvent.DESELECTED_DECK_OR_HERO, m_data.m_challenger);
					}
				}
				else if (partyEvent.eventData == "ready")
				{
					if (DidReceiveChallenge())
					{
						m_data.m_challengerDeckOrHeroSelected = true;
						FireChangedEvent(FriendChallengeEvent.SELECTED_DECK_OR_HERO, m_data.m_challenger);
						SetIAmInGameState();
					}
				}
				else if (partyEvent.eventData == "game")
				{
					if (DidReceiveChallenge())
					{
						m_data.m_challengerInGameState = true;
						SetIAmInGameState();
						StartFriendlyChallengeGameIfReady();
						FriendlyChallengeHelper.Get().WaitForFriendChallengeToStart();
						m_data.m_findGameErrorOccurred = false;
					}
				}
				else if (partyEvent.eventData == "goto")
				{
					m_data.m_challengerDeckOrHeroSelected = false;
					m_data.m_challengerInGameState = false;
				}
			}
			else if (partyEvent.eventName == "s2")
			{
				if (partyEvent.eventData == "wait")
				{
					OnPartyUpdate_JoinedParty(partyId, otherMemberId);
				}
				else if (partyEvent.eventData == "deck")
				{
					if (DidSendChallenge())
					{
						if (m_data.m_challengeeAccepted)
						{
							m_data.m_challengeeDeckOrHeroSelected = false;
							m_data.m_challengeeInGameState = false;
							FireChangedEvent(FriendChallengeEvent.DESELECTED_DECK_OR_HERO, m_data.m_challengee);
						}
						else
						{
							m_data.m_challengeeAccepted = true;
							FireChangedEvent(FriendChallengeEvent.OPPONENT_ACCEPTED_CHALLENGE, m_data.m_challengee);
						}
					}
				}
				else if (partyEvent.eventData == "ready")
				{
					if (DidSendChallenge())
					{
						m_data.m_challengeeDeckOrHeroSelected = true;
						FireChangedEvent(FriendChallengeEvent.SELECTED_DECK_OR_HERO, m_data.m_challengee);
						SetIAmInGameState();
					}
				}
				else if (partyEvent.eventData == "game")
				{
					if (DidSendChallenge())
					{
						m_data.m_challengeeInGameState = true;
						SetIAmInGameState();
						if (StartFriendlyChallengeGameIfReady())
						{
							FriendlyChallengeHelper.Get().WaitForFriendChallengeToStart();
						}
					}
				}
				else if (partyEvent.eventData == "goto")
				{
					m_data.m_challengeeDeckOrHeroSelected = false;
					m_data.m_challengeeInGameState = false;
				}
			}
			else if (partyEvent.eventName == "left")
			{
				if (DidSendChallenge())
				{
					BnetPlayer challengee = m_data.m_challengee;
					bool challengeeAccepted = m_data.m_challengeeAccepted;
					RevertTavernBrawlPresenceStatus();
					FriendlyChallengeData challengeData = CleanUpChallengeData();
					if (challengeeAccepted)
					{
						FireChangedEvent(FriendChallengeEvent.OPPONENT_CANCELED_CHALLENGE, challengee, challengeData);
					}
					else
					{
						FireChangedEvent(FriendChallengeEvent.OPPONENT_DECLINED_CHALLENGE, challengee, challengeData);
					}
				}
				else if (DidReceiveChallenge())
				{
					BnetPlayer challenger = m_data.m_challenger;
					bool challengeeAccepted2 = m_data.m_challengeeAccepted;
					RevertTavernBrawlPresenceStatus();
					FriendlyChallengeData challengeData2 = CleanUpChallengeData();
					if (challenger != null)
					{
						if (challengeeAccepted2)
						{
							FireChangedEvent(FriendChallengeEvent.OPPONENT_CANCELED_CHALLENGE, challenger, challengeData2);
						}
						else
						{
							FireChangedEvent(FriendChallengeEvent.OPPONENT_RESCINDED_CHALLENGE, challenger, challengeData2);
						}
					}
				}
				else
				{
					CleanUpChallengeData();
				}
			}
			else if (partyEvent.eventName == "p1DeckShareState")
			{
				if (m_data.m_challenger == null)
				{
					continue;
				}
				string challengerDeckShareState = m_data.m_challengerDeckShareState;
				m_data.m_challengerDeckShareState = partyEvent.eventData;
				if (challengerDeckShareState == "none" && m_data.m_challengerDeckShareState == "requested")
				{
					if (DidReceiveChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.OPPONENT_REQUESTED_DECK_SHARE, m_data.m_challenger);
					}
					else if (DidSendChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.I_REQUESTED_DECK_SHARE, m_data.m_challenger);
					}
				}
				else if (challengerDeckShareState == "requested" && m_data.m_challengerDeckShareState == "none")
				{
					if (DidSendChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.I_CANCELED_DECK_SHARE_REQUEST, m_data.m_challenger);
					}
					if (DidReceiveChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.OPPONENT_CANCELED_DECK_SHARE_REQUEST, m_data.m_challenger);
					}
				}
				else if (challengerDeckShareState == "requested" && m_data.m_challengerDeckShareState == "declined")
				{
					if (DidReceiveChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.I_DECLINED_DECK_SHARE_REQUEST, m_data.m_challenger);
					}
					else if (DidSendChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.OPPONENT_DECLINED_DECK_SHARE_REQUEST, m_data.m_challenger);
					}
				}
				else if (challengerDeckShareState == "requested" && m_data.m_challengerDeckShareState == "sharing")
				{
					if (DidReceiveChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.I_ACCEPTED_DECK_SHARE_REQUEST, m_data.m_challenger);
					}
					else if (DidSendChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.OPPONENT_ACCEPTED_DECK_SHARE_REQUEST, m_data.m_challenger);
					}
				}
				else if (challengerDeckShareState == "sharing" && m_data.m_challengerDeckShareState == "sharingUnused")
				{
					if (DidSendChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.I_ENDED_DECK_SHARE, m_data.m_challenger);
					}
				}
				else if (challengerDeckShareState == "sharingUnused" && m_data.m_challengerDeckShareState == "sharing")
				{
					if (DidSendChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.I_RECEIVED_SHARED_DECKS, m_data.m_challenger);
					}
				}
				else if (m_data.m_challengerDeckShareState == "error" && DidSendChallenge())
				{
					FireChangedEvent(FriendChallengeEvent.DECK_SHARE_ERROR_OCCURED, m_data.m_challenger);
				}
			}
			else if (partyEvent.eventName == "p2DeckShareState")
			{
				if (m_data.m_challengee == null)
				{
					continue;
				}
				string challengeeDeckShareState = m_data.m_challengeeDeckShareState;
				m_data.m_challengeeDeckShareState = partyEvent.eventData;
				if (challengeeDeckShareState == "none" && m_data.m_challengeeDeckShareState == "requested")
				{
					if (DidReceiveChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.I_REQUESTED_DECK_SHARE, m_data.m_challengee);
					}
					else if (DidSendChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.OPPONENT_REQUESTED_DECK_SHARE, m_data.m_challengee);
					}
				}
				else if (challengeeDeckShareState == "requested" && m_data.m_challengeeDeckShareState == "none")
				{
					if (DidSendChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.OPPONENT_CANCELED_DECK_SHARE_REQUEST, m_data.m_challengee);
					}
					else if (DidReceiveChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.I_CANCELED_DECK_SHARE_REQUEST, m_data.m_challengee);
					}
				}
				else if (challengeeDeckShareState == "requested" && m_data.m_challengeeDeckShareState == "declined")
				{
					if (DidReceiveChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.OPPONENT_DECLINED_DECK_SHARE_REQUEST, m_data.m_challengee);
					}
					else if (DidSendChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.I_DECLINED_DECK_SHARE_REQUEST, m_data.m_challengee);
					}
				}
				else if (challengeeDeckShareState == "requested" && m_data.m_challengeeDeckShareState == "sharing")
				{
					if (DidReceiveChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.OPPONENT_ACCEPTED_DECK_SHARE_REQUEST, m_data.m_challengee);
					}
					else if (DidSendChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.I_ACCEPTED_DECK_SHARE_REQUEST, m_data.m_challengee);
					}
				}
				else if (challengeeDeckShareState == "sharing" && m_data.m_challengeeDeckShareState == "sharingUnused")
				{
					if (DidReceiveChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.I_ENDED_DECK_SHARE, m_data.m_challengee);
					}
				}
				else if (challengeeDeckShareState == "sharingUnused" && m_data.m_challengeeDeckShareState == "sharing")
				{
					if (DidReceiveChallenge())
					{
						FireChangedEvent(FriendChallengeEvent.I_RECEIVED_SHARED_DECKS, m_data.m_challengee);
					}
				}
				else if (m_data.m_challengeeDeckShareState == "error" && DidReceiveChallenge())
				{
					FireChangedEvent(FriendChallengeEvent.DECK_SHARE_ERROR_OCCURED, m_data.m_challengee);
				}
			}
			else if (partyEvent.eventName == "p1DeckShareDecks")
			{
				if (DidReceiveChallenge() && m_data.m_challengeeDeckShareState == "sharing")
				{
					if (HasOpponentSharedDecks())
					{
						FireChangedEvent(FriendChallengeEvent.I_RECEIVED_SHARED_DECKS, m_data.m_challengee);
					}
					else
					{
						BnetParty.SetPartyAttributeString(m_data.m_partyId, "p2DeckShareState", "error");
					}
				}
			}
			else if (partyEvent.eventName == "p2DeckShareDecks" && DidSendChallenge() && m_data.m_challengerDeckShareState == "sharing")
			{
				if (HasOpponentSharedDecks())
				{
					FireChangedEvent(FriendChallengeEvent.I_RECEIVED_SHARED_DECKS, m_data.m_challenger);
				}
				else
				{
					BnetParty.SetPartyAttributeString(m_data.m_partyId, "p1DeckShareState", "error");
				}
			}
		}
	}

	private void OnPartyUpdate_CreatedParty(BnetEntityId partyId, BnetGameAccountId otherMemberId)
	{
		UpdateChallengeSentDialog();
	}

	private void OnPartyUpdate_JoinedParty(BnetEntityId partyId, BnetGameAccountId otherMemberId)
	{
		if (!DidSendChallenge())
		{
			if (!CanReceiveChallengeFrom(otherMemberId, partyId))
			{
				DeclineFriendChallenge_Internal(partyId);
			}
			else if (!AmIAvailable())
			{
				DeclineFriendChallenge_Internal(partyId);
			}
			else
			{
				HandleJoinedParty(partyId, otherMemberId);
			}
		}
	}

	private static bool CanReceiveChallengeFrom(BnetGameAccountId challengerPlayer, BnetEntityId challengerPartyId)
	{
		if (BnetFriendMgr.Get().IsFriend(challengerPlayer))
		{
			return true;
		}
		if (BnetNearbyPlayerMgr.Get().IsNearbyStranger(challengerPlayer))
		{
			return true;
		}
		FiresideGatheringManager firesideGatheringManager = FiresideGatheringManager.Get();
		if (firesideGatheringManager.IsCheckedIn)
		{
			if (firesideGatheringManager.IsPlayerInMyFSG(BnetUtils.GetPlayer(challengerPlayer)))
			{
				return true;
			}
			if (firesideGatheringManager.CurrentFsgSharedSecretKey != null)
			{
				byte[] arr = SHA256.Create().ComputeHash(firesideGatheringManager.CurrentFsgSharedSecretKey, 0, firesideGatheringManager.CurrentFsgSharedSecretKey.Length);
				if (GeneralUtils.AreArraysEqual(BnetParty.GetPartyAttributeBlob(challengerPartyId, "fsg1"), arr))
				{
					return true;
				}
			}
		}
		return false;
	}

	private bool StartFriendlyChallengeGameIfReady()
	{
		if (!DidSendChallenge())
		{
			return false;
		}
		if (!BnetParty.IsInParty(m_data.m_partyId))
		{
			return false;
		}
		bool flag = m_data.m_challengerDeckId != 0L && m_data.m_challengeeDeckId != 0;
		bool flag2 = m_data.m_challengerHeroId != 0L && m_data.m_challengeeHeroId != 0;
		if (!flag && !flag2)
		{
			return false;
		}
		if (!m_data.m_challengerInGameState || !m_data.m_challengeeInGameState)
		{
			return false;
		}
		m_data.m_findGameErrorOccurred = false;
		Attribute attributeV = ProtocolHelper.CreateAttribute("s1", "goto");
		Attribute attributeV2 = ProtocolHelper.CreateAttribute("s2", "goto");
		BnetParty.SetPartyAttributes(m_data.m_partyId, ProtocolHelper.V1AttributeToV2Attribute(attributeV), ProtocolHelper.V1AttributeToV2Attribute(attributeV2));
		FormatType formatType = GetFormatType();
		if (IsChallengeBacon())
		{
			Network.Get().EnterBattlegroundsWithFriend(m_data.m_challengee.GetHearthstoneGameAccountId(), m_data.m_scenarioId);
		}
		else if (flag)
		{
			string partyAttributeString = BnetParty.GetPartyAttributeString(m_data.m_partyId, "p1DeckShareState");
			DeckShareState deckShareStateEnumFromAttribute = GetDeckShareStateEnumFromAttribute(partyAttributeString);
			string partyAttributeString2 = BnetParty.GetPartyAttributeString(m_data.m_partyId, "p2DeckShareState");
			DeckShareState deckShareStateEnumFromAttribute2 = GetDeckShareStateEnumFromAttribute(partyAttributeString2);
			GameMgr.Get().EnterFriendlyChallengeGameWithDecks(formatType, m_data.m_challengeBrawlType, m_data.m_scenarioId, m_data.m_seasonId, m_data.m_brawlLibraryItemId, deckShareStateEnumFromAttribute, m_data.m_challengerDeckId, deckShareStateEnumFromAttribute2, m_data.m_challengeeDeckId, m_data.m_challengee.GetHearthstoneGameAccountId());
		}
		else
		{
			GameMgr.Get().EnterFriendlyChallengeGameWithHeroes(formatType, m_data.m_challengeBrawlType, m_data.m_scenarioId, m_data.m_seasonId, m_data.m_brawlLibraryItemId, m_data.m_challengerHeroId, m_data.m_challengeeHeroId, m_data.m_challengee.GetHearthstoneGameAccountId());
		}
		if (m_challengeDialog != null)
		{
			m_challengeDialog.Hide();
			m_challengeDialog = null;
		}
		return true;
	}

	private void SetIAmInGameState()
	{
		if (BnetParty.IsInParty(m_data.m_partyId) && m_data.m_challengerDeckOrHeroSelected && m_data.m_challengeeDeckOrHeroSelected && !AmIInGameState())
		{
			if (DidSendChallenge())
			{
				m_data.m_challengerInGameState = true;
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "s1", "game");
			}
			else
			{
				m_data.m_challengeeInGameState = true;
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "s2", "game");
			}
		}
	}

	private void OnNetCacheReady()
	{
		NetCache.Get().UnregisterNetCacheHandler(OnNetCacheReady);
		m_netCacheReady = true;
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.FATAL_ERROR)
		{
			UpdateMyAvailability();
		}
	}

	private void OnSceneUnloaded(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		if (prevMode != SceneMgr.Mode.GAMEPLAY)
		{
			UpdateMyAvailability();
		}
		if (m_data.m_updatePartyQuestInfoOnGameplaySceneUnload && prevMode == SceneMgr.Mode.GAMEPLAY)
		{
			m_data.m_updatePartyQuestInfoOnGameplaySceneUnload = false;
			UpdatePartyQuestInfo();
		}
	}

	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY && mode != SceneMgr.Mode.GAMEPLAY && mode != SceneMgr.Mode.FATAL_ERROR)
		{
			m_netCacheReady = false;
			if (mode == SceneMgr.Mode.FRIENDLY || (mode == SceneMgr.Mode.TAVERN_BRAWL && Get().IsChallengeTavernBrawl()))
			{
				UpdateMyAvailability();
			}
			else
			{
				CancelChallenge();
			}
			NetCache.Get().RegisterFriendChallenge(OnNetCacheReady);
		}
	}

	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		BnetPlayerChange bnetPlayerChange = changelist.FindChange(myPlayer);
		if (bnetPlayerChange != null)
		{
			bool flag = AmIAvailable();
			BnetGameAccount hearthstoneGameAccount = myPlayer.GetHearthstoneGameAccount();
			if (hearthstoneGameAccount != null && !m_myPlayerReady && hearthstoneGameAccount.HasGameField(20u) && hearthstoneGameAccount.HasGameField(19u))
			{
				m_myPlayerReady = true;
				if (!UpdateMyAvailability())
				{
					flag = false;
				}
			}
			if (!flag && m_data.m_challengerPending)
			{
				DeclineFriendChallenge_Internal(m_data.m_partyId);
				CleanUpChallengeData();
			}
		}
		if (!m_data.m_challengerPending)
		{
			return;
		}
		bnetPlayerChange = changelist.FindChange(m_data.m_challengerId);
		if (bnetPlayerChange != null)
		{
			BnetPlayer player = bnetPlayerChange.GetPlayer();
			if (player.IsDisplayable())
			{
				m_data.m_challenger = player;
				m_data.m_challengerPending = false;
				FireChangedEvent(FriendChallengeEvent.I_RECEIVED_CHALLENGE, m_data.m_challenger);
			}
		}
	}

	private void OnFriendsChanged(BnetFriendChangelist changelist, object userData)
	{
		if (!HasChallenge())
		{
			return;
		}
		List<BnetPlayer> removedFriends = changelist.GetRemovedFriends();
		if (removedFriends == null)
		{
			return;
		}
		BnetPlayer opponent = GetOpponent(BnetPresenceMgr.Get().GetMyPlayer());
		if (opponent == null)
		{
			return;
		}
		foreach (BnetPlayer item in removedFriends)
		{
			if (item != opponent)
			{
				continue;
			}
			PartyInfo[] joinedParties = BnetParty.GetJoinedParties();
			BnetGameAccountId hearthstoneGameAccountId = opponent.GetHearthstoneGameAccountId();
			PartyInfo[] array = joinedParties;
			foreach (PartyInfo partyInfo in array)
			{
				if (BnetParty.IsMember(partyInfo.Id, hearthstoneGameAccountId))
				{
					BnetParty.Leave(partyInfo.Id);
				}
			}
			RevertTavernBrawlPresenceStatus();
			FriendlyChallengeData challengeData = CleanUpChallengeData();
			FireChangedEvent(FriendChallengeEvent.OPPONENT_REMOVED_FROM_FRIENDS, opponent, challengeData);
			break;
		}
	}

	private void OnNearbyPlayersChanged(BnetNearbyPlayerChangelist changelist, object userData)
	{
		if (!HasChallenge())
		{
			return;
		}
		List<BnetPlayer> removedPlayers = changelist.GetRemovedPlayers();
		if (removedPlayers == null)
		{
			return;
		}
		BnetPlayer opponent = GetOpponent(BnetPresenceMgr.Get().GetMyPlayer());
		if (opponent == null)
		{
			return;
		}
		foreach (BnetPlayer item in removedPlayers)
		{
			if (item == opponent)
			{
				FriendlyChallengeData challengeData = CleanUpChallengeData();
				FireChangedEvent(FriendChallengeEvent.OPPONENT_CANCELED_CHALLENGE, opponent, challengeData);
				break;
			}
		}
	}

	private void OnBnetEventOccurred(BattleNet.BnetEvent bnetEvent, object userData)
	{
		if (bnetEvent == BattleNet.BnetEvent.Disconnected)
		{
			OnDisconnect();
		}
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		OnDisconnect();
	}

	private void OnDisconnect()
	{
		if (m_challengeDialog != null)
		{
			m_challengeDialog.Hide();
			m_challengeDialog = null;
		}
		CleanUpChallengeData();
	}

	private void OnReconnectLoginComplete()
	{
		UpdateMyAvailability();
	}

	private void OnChallengeChanged(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData, object userData)
	{
		switch (challengeEvent)
		{
		case FriendChallengeEvent.I_SENT_CHALLENGE:
			ShowISentChallengeDialog(player);
			break;
		case FriendChallengeEvent.OPPONENT_ACCEPTED_CHALLENGE:
			StartChallengeProcess();
			break;
		case FriendChallengeEvent.OPPONENT_DECLINED_CHALLENGE:
			ShowOpponentDeclinedChallengeDialog(player, challengeData);
			break;
		case FriendChallengeEvent.I_RECEIVED_CHALLENGE:
			if (CanPromptReceivedChallenge())
			{
				if (IsChallengeTavernBrawl())
				{
					PresenceMgr.Get().SetStatus(Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING);
				}
				ShowIReceivedChallengeDialog(player);
			}
			break;
		case FriendChallengeEvent.I_ACCEPTED_CHALLENGE:
			StartChallengeProcess();
			break;
		case FriendChallengeEvent.I_RESCINDED_CHALLENGE:
		case FriendChallengeEvent.I_DECLINED_CHALLENGE:
			OnChallengeCanceled();
			break;
		case FriendChallengeEvent.OPPONENT_RESCINDED_CHALLENGE:
			OnChallengeCanceled();
			ShowOpponentCanceledChallengeDialog(player, challengeData);
			break;
		case FriendChallengeEvent.OPPONENT_CANCELED_CHALLENGE:
			FriendlyChallengeHelper.Get().HideAllDeckShareDialogs();
			OnChallengeCanceled();
			ShowOpponentCanceledChallengeDialog(player, challengeData);
			break;
		case FriendChallengeEvent.OPPONENT_REMOVED_FROM_FRIENDS:
			FriendlyChallengeHelper.Get().HideAllDeckShareDialogs();
			ShowOpponentRemovedFromFriendsDialog(player, challengeData);
			break;
		case FriendChallengeEvent.QUEUE_CANCELED:
			OnChallengeCanceled();
			ShowQueueCanceledDialog(player, challengeData);
			break;
		case FriendChallengeEvent.I_REQUESTED_DECK_SHARE:
			if (!FriendlyChallengeHelper.Get().IsShowingDeckShareRequestDialog())
			{
				FriendlyChallengeHelper.Get().ShowDeckShareRequestWaitingDialog(OnFriendChallengeDeckShareRequestDialogWaitingResponse);
			}
			break;
		case FriendChallengeEvent.I_ACCEPTED_DECK_SHARE_REQUEST:
			FriendlyChallengeHelper.Get().HideDeckShareRequestDialog();
			ShareDecks_InternalParty();
			break;
		case FriendChallengeEvent.I_DECLINED_DECK_SHARE_REQUEST:
			FriendlyChallengeHelper.Get().HideDeckShareRequestDialog();
			break;
		case FriendChallengeEvent.I_CANCELED_DECK_SHARE_REQUEST:
			FriendlyChallengeHelper.Get().HideDeckShareRequestWaitingDialog();
			break;
		case FriendChallengeEvent.DECK_SHARE_ERROR_OCCURED:
			if (DidSendChallenge())
			{
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "p1DeckShareState", "none");
			}
			else if (DidReceiveChallenge())
			{
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "p2DeckShareState", "none");
			}
			FriendlyChallengeHelper.Get().HideAllDeckShareDialogs();
			FriendlyChallengeHelper.Get().ShowDeckShareErrorDialog();
			break;
		case FriendChallengeEvent.OPPONENT_REQUESTED_DECK_SHARE:
			FriendlyChallengeHelper.Get().HideAllDeckShareDialogs();
			FriendlyChallengeHelper.Get().HideFriendChallengeWaitingForOpponentDialog();
			FriendlyChallengeHelper.Get().ShowDeckShareRequestDialog(OnFriendChallengeDeckShareRequestDialogResponse);
			break;
		case FriendChallengeEvent.OPPONENT_ACCEPTED_DECK_SHARE_REQUEST:
			FriendlyChallengeHelper.Get().HideDeckShareRequestWaitingDialog();
			break;
		case FriendChallengeEvent.OPPONENT_DECLINED_DECK_SHARE_REQUEST:
			if (DidSendChallenge())
			{
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "p1DeckShareState", "none");
			}
			else if (DidReceiveChallenge())
			{
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "p2DeckShareState", "none");
			}
			FriendlyChallengeHelper.Get().ShowDeckShareRequestDeclinedDialog();
			FriendlyChallengeHelper.Get().HideDeckShareRequestWaitingDialog();
			break;
		case FriendChallengeEvent.OPPONENT_CANCELED_DECK_SHARE_REQUEST:
			FriendlyChallengeHelper.Get().ShowDeckShareRequestCanceledDialog();
			FriendlyChallengeHelper.Get().HideDeckShareRequestDialog();
			break;
		case FriendChallengeEvent.SELECTED_DECK_OR_HERO:
		case FriendChallengeEvent.DESELECTED_DECK_OR_HERO:
		case FriendChallengeEvent.I_ENDED_DECK_SHARE:
		case FriendChallengeEvent.I_RECEIVED_SHARED_DECKS:
			break;
		}
	}

	private void OnChallengeCanceled()
	{
		if (SceneMgr.Get() != null && SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING && FiresideGatheringManager.Get() != null)
		{
			FiresideGatheringManager.Get().CurrentFiresideGatheringMode = FiresideGatheringManager.FiresideGatheringMode.NONE;
		}
		GameMgr.Get().CancelFindGame();
		GameMgr.Get().HideTransitionPopup();
	}

	private bool CanPromptReceivedChallenge()
	{
		bool flag = !UserAttentionManager.CanShowAttentionGrabber("FriendlyChallengeMgr.CanPromptReceivedChallenge");
		if (!flag)
		{
			if (GameMgr.Get().IsFindingGame())
			{
				flag = true;
			}
			else if (RankMgr.Get().IsLegendRankInAnyFormat)
			{
				flag = SceneMgr.Get().IsModeRequested(SceneMgr.Mode.TOURNAMENT);
			}
		}
		if (flag)
		{
			BnetParty.SetPartyAttributeLong(m_data.m_partyId, "WTCG.Friendly.DeclineReason", 6L);
			DeclineChallenge();
			return false;
		}
		if (IsChallengeTavernBrawl())
		{
			if (!TavernBrawlManager.Get().HasUnlockedTavernBrawl(m_data.m_challengeBrawlType) && !PlayersInSameFiresideGathering())
			{
				DeclineReason declineReason = DeclineReason.TavernBrawlNotUnlocked;
				BnetParty.SetPartyAttributeLong(m_data.m_partyId, "WTCG.Friendly.DeclineReason", (long)declineReason);
				DeclineChallenge();
				return false;
			}
			TavernBrawlManager.Get().EnsureAllDataReady(m_data.m_challengeBrawlType, TavernBrawl_ReceivedChallenge_OnEnsureServerDataReady);
			return false;
		}
		if (!CollectionManager.Get().AreAllDeckContentsReady())
		{
			CollectionManager.Get().RequestDeckContentsForDecksWithoutContentsLoaded(CanPromptReceivedChallenge_OnDeckContentsLoaded);
			return false;
		}
		if (IsChallengeStandardDuel() && !CollectionManager.Get().AccountHasValidDeck(FormatType.FT_STANDARD))
		{
			DeclineReason declineReason2 = DeclineReason.StandardNoValidDeck;
			BnetParty.SetPartyAttributeLong(m_data.m_partyId, "WTCG.Friendly.DeclineReason", (long)declineReason2);
			DeclineChallenge();
			return false;
		}
		if (IsChallengeWildDuel())
		{
			if (!CollectionManager.Get().ShouldAccountSeeStandardWild())
			{
				DeclineReason declineReason3 = DeclineReason.NotSeenWild;
				BnetParty.SetPartyAttributeLong(m_data.m_partyId, "WTCG.Friendly.DeclineReason", (long)declineReason3);
				DeclineChallenge();
				return false;
			}
			if (!CollectionManager.Get().AccountHasAnyValidDeck())
			{
				DeclineReason declineReason4 = DeclineReason.NoValidDeck;
				BnetParty.SetPartyAttributeLong(m_data.m_partyId, "WTCG.Friendly.DeclineReason", (long)declineReason4);
				DeclineChallenge();
				return false;
			}
		}
		else
		{
			if (IsChallengeClassicDuel() && !CollectionManager.Get().AccountHasValidDeck(FormatType.FT_CLASSIC))
			{
				DeclineReason declineReason5 = DeclineReason.ClassicNoValidDeck;
				BnetParty.SetPartyAttributeLong(m_data.m_partyId, "WTCG.Friendly.DeclineReason", (long)declineReason5);
				DeclineChallenge();
				return false;
			}
			if (IsChallengeBacon() && SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", activeIfDoesNotExist: false) && !AccountLicenseMgr.Get().OwnsAccountLicense(NetCache.Get().GetBattlegroundsEarlyAccessLicenseId()))
			{
				DeclineReason declineReason6 = DeclineReason.BattlegroundsNoEarlyAccess;
				BnetParty.SetPartyAttributeLong(m_data.m_partyId, "WTCG.Friendly.DeclineReason", (long)declineReason6);
				DeclineChallenge();
				return false;
			}
		}
		return true;
	}

	private void CanPromptReceivedChallenge_OnDeckContentsLoaded()
	{
		if (DidReceiveChallenge() && CanPromptReceivedChallenge())
		{
			ShowIReceivedChallengeDialog(m_data.m_challenger);
		}
	}

	private void TavernBrawl_ReceivedChallenge_OnEnsureServerDataReady()
	{
		TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(m_data.m_challengeBrawlType);
		DeclineReason? declineReason = null;
		if (mission == null)
		{
			declineReason = DeclineReason.None;
		}
		if (mission != null && mission.CanCreateDeck(m_data.m_brawlLibraryItemId) && !TavernBrawlManager.Get().HasValidDeck(m_data.m_challengeBrawlType))
		{
			declineReason = DeclineReason.TavernBrawlNoValidDeck;
		}
		if (declineReason.HasValue)
		{
			BnetParty.SetPartyAttributeLong(m_data.m_partyId, "WTCG.Friendly.DeclineReason", (long)declineReason.Value);
			DeclineChallenge();
			return;
		}
		if (IsChallengeTavernBrawl())
		{
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING);
		}
		ShowIReceivedChallengeDialog(m_data.m_challenger);
	}

	private bool RevertTavernBrawlPresenceStatus()
	{
		if (IsChallengeTavernBrawl() && PresenceMgr.Get().CurrentStatus == Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING)
		{
			PresenceMgr.Get().SetPrevStatus();
			return true;
		}
		return false;
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		UpdateMyAvailability();
		switch (eventData.m_state)
		{
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.BNET_ERROR:
		{
			m_data.m_findGameErrorOccurred = true;
			if (DidSendChallenge())
			{
				BnetParty.SetPartyAttributeLong(m_data.m_partyId, "error", GameMgr.Get().GetLastEnterGameError());
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "s1", "deck");
			}
			else if (DidReceiveChallenge())
			{
				BnetParty.SetPartyAttributeString(m_data.m_partyId, "s2", "deck");
			}
			SceneMgr.Mode mode = SceneMgr.Get().GetMode();
			bool flag = mode != SceneMgr.Mode.FRIENDLY && mode != SceneMgr.Mode.TAVERN_BRAWL && mode != SceneMgr.Mode.FIRESIDE_GATHERING;
			if (DidSendChallenge() && IsChallengeFiresideBrawl())
			{
				flag = true;
			}
			if (flag)
			{
				QueueCanceled();
			}
			break;
		}
		case FindGameState.BNET_QUEUE_ENTERED:
		case FindGameState.SERVER_GAME_CONNECTING:
			if (HasChallenge())
			{
				DeselectDeckOrHero();
			}
			break;
		}
		return false;
	}

	private void WillReset()
	{
		CleanUpChallengeData(updateAvailability: false);
		if (m_challengeDialog != null)
		{
			m_challengeDialog.Hide();
			m_challengeDialog = null;
		}
		FriendlyChallengeHelper.Get().HideAllDeckShareDialogs();
	}

	private void ShowISentChallengeDialog(BnetPlayer challengee)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER");
		popupInfo.m_text = GameStrings.Format("GLOBAL_FRIEND_CHALLENGE_OPPONENT_WAITING_RESPONSE", FriendUtils.GetUniqueName(challengee));
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.NONE;
		popupInfo.m_responseCallback = OnChallengeSentDialogResponse;
		popupInfo.m_layerToUse = GameLayer.UI;
		DialogManager.Get().ShowPopup(popupInfo, OnChallengeSentDialogProcessed);
	}

	private void ShowOpponentDeclinedChallengeDialog(BnetPlayer challengee, FriendlyChallengeData challengeData)
	{
		if (m_challengeDialog != null)
		{
			m_challengeDialog.Hide();
			m_challengeDialog = null;
		}
		if (!m_hasSeenDeclinedReason)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER");
			popupInfo.m_text = GameStrings.Format("GLOBAL_FRIEND_CHALLENGE_OPPONENT_DECLINED", FriendUtils.GetUniqueName(challengee));
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			popupInfo.m_showAlertIcon = false;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_responseCallback = OnOpponentDeclinedChallengeDialogDismissed;
			AlertPopup.PopupInfo info = popupInfo;
			DialogManager.Get().ShowPopup(info);
		}
	}

	private void OnOpponentDeclinedChallengeDialogDismissed(AlertPopup.Response response, object userData)
	{
		ChatMgr.Get().UpdateFriendItemsWhenAvailable();
	}

	private void ShowIReceivedChallengeDialog(BnetPlayer challenger)
	{
		if (m_challengeDialog != null)
		{
			m_challengeDialog.Hide();
			m_challengeDialog = null;
		}
		DialogManager.Get().ShowFriendlyChallenge(m_data.m_challengeFormatType, challenger, IsChallengeTavernBrawl(), PartyType.FRIENDLY_CHALLENGE, OnChallengeReceivedDialogResponse, OnChallengeReceivedDialogProcessed);
	}

	private void ShowOpponentCanceledChallengeDialog(BnetPlayer otherPlayer, FriendlyChallengeData challengeData)
	{
		if (m_challengeDialog != null)
		{
			m_challengeDialog.Hide();
			m_challengeDialog = null;
		}
		if ((GameMgr.Get() == null || !SuppressChallengeCanceledDialogByMissionId(GameMgr.Get().GetMissionId())) && (SceneMgr.Get() == null || !SceneMgr.Get().IsInGame() || GameState.Get() == null || GameState.Get().IsGameOverNowOrPending()) && ((challengeData.m_challengeBrawlType != BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING && SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING) || (challengeData.m_challengeeAccepted && !challengeData.IsPendingGotoGame && !challengeData.m_findGameErrorOccurred)))
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER");
			popupInfo.m_text = GameStrings.Format("GLOBAL_FRIEND_CHALLENGE_OPPONENT_CANCELED", FriendUtils.GetUniqueName(otherPlayer));
			popupInfo.m_showAlertIcon = false;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_responseCallback = OnOpponentCanceledChallengeDialogClosed;
			DialogManager.Get().ShowPopup(popupInfo);
		}
	}

	public void OnOpponentCanceledChallengeDialogClosed(AlertPopup.Response response, object userData)
	{
		if (SceneMgr.Get().IsTransitionNowOrPending() && SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.FRIENDLY)
		{
			SceneMgr.Get().ReturnToPreviousMode();
		}
	}

	private void ShowOpponentRemovedFromFriendsDialog(BnetPlayer otherPlayer, FriendlyChallengeData challengeData)
	{
		if (m_challengeDialog != null)
		{
			m_challengeDialog.Hide();
			m_challengeDialog = null;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER");
		popupInfo.m_text = GameStrings.Format("GLOBAL_FRIEND_CHALLENGE_OPPONENT_FRIEND_REMOVED", FriendUtils.GetUniqueName(otherPlayer));
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void ShowQueueCanceledDialog(BnetPlayer otherPlayer, FriendlyChallengeData challengeData)
	{
		if (m_challengeDialog != null)
		{
			m_challengeDialog.Hide();
			m_challengeDialog = null;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER");
		popupInfo.m_text = GameStrings.Format("GLOBAL_FRIEND_CHALLENGE_QUEUE_CANCELED");
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private bool OnChallengeSentDialogProcessed(DialogBase dialog, object userData)
	{
		if (!DidSendChallenge())
		{
			return false;
		}
		if (m_data.m_challengeeAccepted)
		{
			return false;
		}
		m_challengeDialog = dialog;
		UpdateChallengeSentDialog();
		return true;
	}

	private void UpdateChallengeSentDialog()
	{
		if (!(m_data.m_partyId == null) && !(m_challengeDialog == null))
		{
			AlertPopup alertPopup = (AlertPopup)m_challengeDialog;
			AlertPopup.PopupInfo info = alertPopup.GetInfo();
			if (info != null)
			{
				info.m_responseDisplay = AlertPopup.ResponseDisplay.CANCEL;
				alertPopup.UpdateInfo(info);
			}
		}
	}

	private void OnChallengeSentDialogResponse(AlertPopup.Response response, object userData)
	{
		m_challengeDialog = null;
		RescindChallenge();
	}

	private bool OnChallengeReceivedDialogProcessed(DialogBase dialog, object userData)
	{
		if (!DidReceiveChallenge())
		{
			return false;
		}
		m_challengeDialog = dialog;
		PartyQuestInfo partyQuestInfo = GetPartyQuestInfo();
		if (partyQuestInfo != null)
		{
			((FriendlyChallengeDialog)dialog).SetQuestInfo(partyQuestInfo);
		}
		return true;
	}

	private void OnChallengeReceivedDialogResponse(bool accept)
	{
		m_challengeDialog = null;
		if (accept)
		{
			AcceptChallenge();
		}
		else
		{
			DeclineChallenge();
		}
	}

	private void HandleJoinedParty(BnetEntityId partyId, BnetGameAccountId otherMemberId)
	{
		m_data.m_partyId = partyId;
		m_data.m_challengerId = otherMemberId;
		m_data.m_challenger = BnetUtils.GetPlayer(m_data.m_challengerId);
		m_data.m_challengee = BnetPresenceMgr.Get().GetMyPlayer();
		m_hasSeenDeclinedReason = false;
		if (m_data.m_challenger == null || !m_data.m_challenger.IsDisplayable())
		{
			m_data.m_challengerPending = true;
			UpdateMyAvailability();
		}
		else
		{
			UpdateMyAvailability();
			FireChangedEvent(FriendChallengeEvent.I_RECEIVED_CHALLENGE, m_data.m_challenger);
		}
	}

	public bool UpdateMyAvailability()
	{
		if (!Network.ShouldBeConnectedToAurora() || !Network.IsLoggedIn())
		{
			return false;
		}
		bool flag = !HasAvailabilityBlocker();
		Log.Presence.PrintDebug("UpdateMyAvailability: Available=" + flag);
		m_canBeInvitedToGame = flag;
		if (!m_updateMyAvailabilityCallbackScheduledThisFrame)
		{
			Processor.ScheduleCallback(0f, realTime: false, UpdateMyAvailabilityScheduledCallback);
		}
		m_updateMyAvailabilityCallbackScheduledThisFrame = true;
		return flag;
	}

	private void UpdateMyAvailabilityScheduledCallback(object userData)
	{
		if (m_updateMyAvailabilityCallbackScheduledThisFrame)
		{
			m_updateMyAvailabilityCallbackScheduledThisFrame = false;
			Log.Presence.PrintDebug("UpdateMyAvailabilityScheduledCallback: Available=" + m_canBeInvitedToGame);
			BnetPresenceMgr.Get().SetGameField(1u, m_canBeInvitedToGame);
			BnetNearbyPlayerMgr.Get().SetAvailability(m_canBeInvitedToGame);
		}
	}

	private bool HasAvailabilityBlocker()
	{
		if (GetAvailabilityBlockerReason() != 0)
		{
			return true;
		}
		return false;
	}

	private AvailabilityBlockerReasons GetAvailabilityBlockerReason()
	{
		AvailabilityBlockerReasons availabilityBlockerReasons = AvailabilityBlockerReasons.NONE;
		if (!m_netCacheReady)
		{
			availabilityBlockerReasons = AvailabilityBlockerReasons.NETCACHE_NOT_READY;
		}
		if (!m_myPlayerReady)
		{
			availabilityBlockerReasons = AvailabilityBlockerReasons.MY_PLAYER_NOT_READY;
		}
		if (HasChallenge())
		{
			availabilityBlockerReasons = AvailabilityBlockerReasons.HAS_EXISTING_CHALLENGE;
		}
		if (PartyManager.Get().HasPendingPartyInviteOrDialog())
		{
			availabilityBlockerReasons = AvailabilityBlockerReasons.HAS_PENDING_PARTY_INVITE;
		}
		if (availabilityBlockerReasons == AvailabilityBlockerReasons.NONE)
		{
			availabilityBlockerReasons = UserAttentionManager.GetAvailabilityBlockerReason(isFriendlyChallenge: true);
		}
		if (availabilityBlockerReasons != 0)
		{
			Log.Presence.PrintDebug("GetAvailabilityBlockerReason: " + availabilityBlockerReasons);
		}
		return availabilityBlockerReasons;
	}

	private void FireChangedEvent(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData = null)
	{
		if (challengeData == null)
		{
			challengeData = m_data;
		}
		ChangedListener[] array = m_changedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(challengeEvent, player, challengeData);
		}
	}

	private FriendlyChallengeData CleanUpChallengeData(bool updateAvailability = true)
	{
		FriendlyChallengeData data = m_data;
		m_data = new FriendlyChallengeData();
		if (updateAvailability)
		{
			UpdateMyAvailability();
		}
		return data;
	}

	private void StartChallengeProcess()
	{
		bool flag = (!DidSendChallenge() && m_data.m_challengeeDeckOrHeroSelected) || (DidSendChallenge() && m_data.m_challengerDeckOrHeroSelected);
		if (m_challengeDialog != null && !flag)
		{
			m_challengeDialog.Hide();
			m_challengeDialog = null;
		}
		GameMgr.Get().SetPendingAutoConcede(pendingAutoConcede: true);
		if (CollectionManager.Get().IsInEditMode())
		{
			CollectionManager.Get().GetEditedDeck()?.SendChanges();
		}
		if (IsChallengeTavernBrawl())
		{
			TavernBrawlManager.Get().CurrentBrawlType = m_data.m_challengeBrawlType;
			TavernBrawlManager.Get().CurrentMission()?.SetSelectedBrawlLibraryItemId(m_data.m_brawlLibraryItemId);
		}
		if (IsChallengeBacon())
		{
			SkipDeckSelection();
			return;
		}
		if (IsChallengeTavernBrawl() && !TavernBrawlManager.Get().SelectHeroBeforeMission(m_data.m_challengeBrawlType))
		{
			if (TavernBrawlManager.Get().GetMission(m_data.m_challengeBrawlType).canCreateDeck)
			{
				if (TavernBrawlManager.Get().HasValidDeck(m_data.m_challengeBrawlType))
				{
					SelectDeck(TavernBrawlManager.Get().GetDeck(m_data.m_challengeBrawlType).ID);
				}
				else
				{
					Debug.LogError("Attempting to start a Tavern Brawl challenge without a valid deck!  How did this happen?");
				}
			}
			else
			{
				SkipDeckSelection();
			}
			return;
		}
		if (!IsChallengeTavernBrawl())
		{
			Options.SetFormatType(m_data.m_challengeFormatType);
		}
		bool flag2 = DidSendChallenge() && m_data.m_challengerDeckOrHeroSelected;
		if (!(!ShouldTransitionToFriendlySceneAccordingToChallengeMethod() && flag2))
		{
			if (m_challengeDialog != null)
			{
				m_challengeDialog.Hide();
				m_challengeDialog = null;
			}
			Navigation.Clear();
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.FRIENDLY);
		}
	}

	private bool SuppressChallengeCanceledDialogByMissionId(int missionId)
	{
		if (missionId == 3459)
		{
			return true;
		}
		return false;
	}
}
