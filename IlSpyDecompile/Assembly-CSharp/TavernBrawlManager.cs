using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using PegasusClient;
using PegasusFSG;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class TavernBrawlManager : IService
{
	public delegate void CallbackEnsureServerDataReady();

	public delegate void TavernBrawlSessionLimitRaisedCallback(int oldLimit, int newLimit);

	public const int REGULAR_TAVERN_BRAWL_MINIMUM_CLASS_LEVEL = 20;

	public const int FSG_BRAWL_MINIMUM_CLASS_LEVEL = 1;

	public const int SPECIAL_TAVERN_BRAWL_SEASON_NUMBER_START = 100000;

	private const float DEFAULT_REFRESH_SPEC_SLUSH_SECONDS_MIN = 0f;

	private const float DEFAULT_REFRESH_SPEC_SLUSH_SECONDS_MAX = 120f;

	private const int REWARD_REFRESH_PADDING_SECONDS = 2;

	private const int REWARD_REFRESH_RANDOM_DELAY_MIN = 0;

	private const int REWARD_REFRESH_RANDOM_DELAY_MAX = 30;

	private static TavernBrawlManager s_instance;

	private TavernBrawlMission[] m_missions = new TavernBrawlMission[3];

	private bool[] m_downloadableDbfAssetsPendingLoad = new bool[3];

	private TavernBrawlPlayerRecord[] m_playerRecords = new TavernBrawlPlayerRecord[3];

	private DateTime?[] m_scheduledRefreshTimes = new DateTime?[3];

	private DateTime?[] m_nextSeasonStartDates = new DateTime?[3];

	private int?[] m_latestSeenSeasonThisSession = new int?[3];

	private int?[] m_latestSeenChalkboardThisSession = new int?[3];

	private BrawlType m_currentBrawlType = BrawlType.BRAWL_TYPE_TAVERN_BRAWL;

	private List<CallbackEnsureServerDataReady> m_serverDataReadyCallbacks;

	private bool m_hasGottenClientOptionsAtLeastOnce;

	private bool m_isFirstTimeSeeingThisFeature;

	private bool m_isFirstTimeSeeingCurrentSeason;

	public BrawlType CurrentBrawlType
	{
		get
		{
			return m_currentBrawlType;
		}
		set
		{
			if (value >= BrawlType.BRAWL_TYPE_TAVERN_BRAWL && value < BrawlType.BRAWL_TYPE_COUNT)
			{
				m_currentBrawlType = value;
			}
		}
	}

	public bool IsCurrentBrawlTypeActive => IsTavernBrawlActive(m_currentBrawlType);

	public bool CanChallengeToCurrentTavernBrawl => CanChallengeToTavernBrawl(m_currentBrawlType);

	public bool HasUnlockedAnyTavernBrawl
	{
		get
		{
			for (int i = 1; i < 3; i++)
			{
				BrawlType brawlType = (BrawlType)i;
				if (HasUnlockedTavernBrawl(brawlType))
				{
					return true;
				}
			}
			return false;
		}
	}

	public bool HasUnlockedCurrentTavernBrawl => HasUnlockedTavernBrawl(m_currentBrawlType);

	public bool IsFirstTimeSeeingThisFeature
	{
		get
		{
			if (m_isFirstTimeSeeingThisFeature)
			{
				return IsCurrentBrawlTypeActive;
			}
			return false;
		}
	}

	public bool IsFirstTimeSeeingCurrentSeason
	{
		get
		{
			if (IsCurrentBrawlTypeActive)
			{
				return m_isFirstTimeSeeingCurrentSeason;
			}
			return false;
		}
	}

	public int LatestSeenTavernBrawlSeason
	{
		get
		{
			if (m_latestSeenSeasonThisSession[(int)m_currentBrawlType].HasValue)
			{
				return m_latestSeenSeasonThisSession[(int)m_currentBrawlType].Value;
			}
			Option option = Option.LATEST_SEEN_TAVERNBRAWL_SEASON;
			if (m_currentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
			{
				option = Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON;
			}
			return Options.Get().GetInt(option);
		}
		set
		{
			m_latestSeenSeasonThisSession[(int)m_currentBrawlType] = value;
			if (value <= 100000)
			{
				Option option = Option.LATEST_SEEN_TAVERNBRAWL_SEASON;
				if (m_currentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
				{
					option = Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON;
				}
				Options.Get().SetInt(option, value);
			}
		}
	}

	public int LatestSeenTavernBrawlChalkboard
	{
		get
		{
			if (m_latestSeenChalkboardThisSession[(int)m_currentBrawlType].HasValue)
			{
				return m_latestSeenChalkboardThisSession[(int)m_currentBrawlType].Value;
			}
			Option option = Option.LATEST_SEEN_TAVERNBRAWL_SEASON_CHALKBOARD;
			if (m_currentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
			{
				option = Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON_CHALKBOARD;
			}
			return Options.Get().GetInt(option);
		}
		set
		{
			m_latestSeenChalkboardThisSession[(int)m_currentBrawlType] = value;
			if (value <= 100000)
			{
				Option option = Option.LATEST_SEEN_TAVERNBRAWL_SEASON_CHALKBOARD;
				if (m_currentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
				{
					option = Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON_CHALKBOARD;
				}
				Options.Get().SetInt(option, value);
			}
		}
	}

	public bool HasSeenTavernBrawlScreen => LatestSeenTavernBrawlChalkboard > 0;

	public long CurrentTavernBrawlSeasonEndInSeconds => TavernBrawlSeasonEndInSeconds(m_currentBrawlType);

	public long NextTavernBrawlSeasonStartInSeconds => TavernBrawlSeasonStartInSeconds(m_currentBrawlType);

	public float CurrentScheduledSecondsToRefresh => ScheduledSecondsToRefresh(m_currentBrawlType);

	public bool IsDeckLocked => CurrentDeck()?.Locked ?? false;

	public bool IsCurrentSeasonSessionBased => IsSeasonSessionBased(m_currentBrawlType);

	public TavernBrawlMode CurrentSeasonBrawlMode => GetBrawlModeForBrawlType(m_currentBrawlType);

	public long CurrentTavernBrawlSeasonNewSessionsClosedInSeconds => TavernBrawlSeasonNewSessionsClosedInSeconds(CurrentBrawlType);

	public bool IsCurrentTavernBrawlSeasonClosedToPlayer
	{
		get
		{
			if (CurrentTavernBrawlSeasonNewSessionsClosedInSeconds < 0)
			{
				if (MyRecord == null)
				{
					return false;
				}
				if (MyRecord.HasNumTicketsOwned && MyRecord.NumTicketsOwned > 0)
				{
					return false;
				}
				if (PlayerStatus != TavernBrawlStatus.TB_STATUS_ACTIVE)
				{
					return PlayerStatus != TavernBrawlStatus.TB_STATUS_IN_REWARDS;
				}
				return false;
			}
			return false;
		}
	}

	public bool IsPlayerAtSessionMaxForCurrentTavernBrawl
	{
		get
		{
			bool isCurrentSeasonSessionBased = IsCurrentSeasonSessionBased;
			bool flag = NumSessionsAvailableForPurchase == 0;
			bool flag2 = NumSessionsAllowedThisSeason == 0;
			bool flag3 = PlayerStatus == TavernBrawlStatus.TB_STATUS_TICKET_REQUIRED;
			bool flag4 = NumTicketsOwned == 0;
			return isCurrentSeasonSessionBased && flag && !flag2 && flag3 && flag4;
		}
	}

	public TavernBrawlStatus PlayerStatus
	{
		get
		{
			if (MyRecord != null && MyRecord.HasSessionStatus)
			{
				return MyRecord.SessionStatus;
			}
			return TavernBrawlStatus.TB_STATUS_INVALID;
		}
	}

	public int NumTicketsOwned
	{
		get
		{
			if (MyRecord != null && MyRecord.HasNumTicketsOwned)
			{
				return MyRecord.NumTicketsOwned;
			}
			return 0;
		}
	}

	public int NumSessionsAllowedThisSeason
	{
		get
		{
			if (CurrentMission() != null)
			{
				return CurrentMission().maxSessions;
			}
			return -1;
		}
	}

	public int NumSessionsAvailableForPurchase
	{
		get
		{
			if (MyRecord != null && MyRecord.HasNumSessionsPurchasable)
			{
				return MyRecord.NumSessionsPurchasable;
			}
			return 0;
		}
	}

	public TavernBrawlPlayerSession CurrentSession
	{
		get
		{
			if (MyRecord != null && MyRecord.HasSession)
			{
				return MyRecord.Session;
			}
			return null;
		}
	}

	public int WinStreak
	{
		get
		{
			if (MyRecord != null && MyRecord.HasWinStreak)
			{
				return MyRecord.WinStreak;
			}
			return 0;
		}
	}

	public int GamesWon
	{
		get
		{
			if (CurrentMission().IsSessionBased)
			{
				if (CurrentSession != null)
				{
					return CurrentSession.Wins;
				}
				return 0;
			}
			if (MyRecord != null)
			{
				return MyRecord.GamesWon;
			}
			return 0;
		}
	}

	public int GamesLost
	{
		get
		{
			if (CurrentMission().IsSessionBased)
			{
				if (CurrentSession != null)
				{
					return CurrentSession.Losses;
				}
				return 0;
			}
			return GamesPlayed - GamesWon;
		}
	}

	public int GamesPlayed
	{
		get
		{
			if (MyRecord != null && MyRecord.HasGamesPlayed)
			{
				return MyRecord.GamesPlayed;
			}
			return 0;
		}
	}

	public int RewardProgress
	{
		get
		{
			if (MyRecord != null)
			{
				return MyRecord.RewardProgress;
			}
			return 0;
		}
	}

	public string EndingTimeText
	{
		get
		{
			long num = ((CurrentMission() == null) ? (-1) : CurrentTavernBrawlSeasonEndInSeconds);
			if (num < 0)
			{
				return null;
			}
			TimeUtils.ElapsedStringSet stringSet = new TimeUtils.ElapsedStringSet
			{
				m_seconds = "GLUE_TAVERN_BRAWL_LABEL_ENDING_SECONDS",
				m_minutes = "GLUE_TAVERN_BRAWL_LABEL_ENDING_MINUTES",
				m_hours = "GLUE_TAVERN_BRAWL_LABEL_ENDING_HOURS",
				m_yesterday = null,
				m_days = "GLUE_TAVERN_BRAWL_LABEL_ENDING_DAYS",
				m_weeks = "GLUE_TAVERN_BRAWL_LABEL_ENDING_WEEKS",
				m_monthAgo = "GLUE_TAVERN_BRAWL_LABEL_ENDING_OVER_1_MONTH"
			};
			return TimeUtils.GetElapsedTimeString((int)num, stringSet, roundUp: true);
		}
	}

	public List<TavernBrawlMission> Missions => m_missions.Where((TavernBrawlMission m) => m != null).ToList();

	public List<RewardData> CurrentSessionRewards
	{
		get
		{
			if (CurrentSession != null && CurrentSession.Chest != null)
			{
				return Network.ConvertRewardChest(CurrentSession.Chest).Rewards;
			}
			return new List<RewardData>();
		}
	}

	public DeckType DeckTypeForCurrentBrawlType
	{
		get
		{
			BrawlType currentBrawlType = m_currentBrawlType;
			if (currentBrawlType != BrawlType.BRAWL_TYPE_TAVERN_BRAWL && currentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
			{
				return DeckType.FSG_BRAWL_DECK;
			}
			return DeckType.TAVERN_BRAWL_DECK;
		}
	}

	public bool IsCurrentBrawlInfoReady
	{
		get
		{
			NetCache.NetCacheClientOptions netObject = NetCache.Get().GetNetObject<NetCache.NetCacheClientOptions>();
			NetCache.NetCacheHeroLevels netObject2 = NetCache.Get().GetNetObject<NetCache.NetCacheHeroLevels>();
			if (netObject == null)
			{
				return false;
			}
			if (CurrentMission() == null)
			{
				return false;
			}
			if (netObject2 == null)
			{
				return false;
			}
			return true;
		}
	}

	public bool IsCurrentBrawlAllDataReady => IsAllDataReady(m_currentBrawlType);

	private TavernBrawlPlayerRecord MyRecord => m_playerRecords[(int)m_currentBrawlType];

	private bool CurrentBrawlDeckContentsLoaded
	{
		get
		{
			TavernBrawlMission tavernBrawlMission = CurrentMission();
			if (tavernBrawlMission == null)
			{
				return true;
			}
			BrawlType currentBrawlType = m_currentBrawlType;
			int seasonId = tavernBrawlMission.seasonId;
			foreach (CollectionDeck value in CollectionManager.Get().GetDecks().Values)
			{
				if (TranslateDeckTypeToBrawlType(value.Type) == currentBrawlType && value.SeasonId == seasonId && !value.NetworkContentsLoaded())
				{
					return false;
				}
			}
			return true;
		}
	}

	public bool IsCheated { get; private set; }

	public event Action OnTavernBrawlUpdated;

	public event TavernBrawlSessionLimitRaisedCallback OnSessionLimitRaised;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		Network network = serviceLocator.Get<Network>();
		NetCache netCache = NetCache.Get();
		network.RegisterNetHandler(TavernBrawlRequestSessionBeginResponse.PacketID.ID, OnBeginSession);
		network.RegisterNetHandler(TavernBrawlRequestSessionRetireResponse.PacketID.ID, OnRetireSession);
		network.RegisterNetHandler(TavernBrawlSessionAckRewardsResponse.PacketID.ID, OnAckRewards);
		network.RegisterNetHandler(TavernBrawlPlayerRecordResponse.PacketID.ID, OnTavernBrawlRecord);
		network.RegisterNetHandler(TavernBrawlInfo.PacketID.ID, OnTavernBrawlInfo);
		network.RegisterNetHandler(CheckInToFSGResponse.PacketID.ID, OnCheckInToFSGResponse);
		netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheHeroLevels), NetCache_OnClientOptions);
		serviceLocator.Get<GameMgr>().RegisterFindGameEvent(OnFindGameEvent);
		RegisterOptionsListeners(register: true);
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[3]
		{
			typeof(Network),
			typeof(GameMgr),
			typeof(NetCache)
		};
	}

	public void Shutdown()
	{
	}

	public static TavernBrawlManager Get()
	{
		return HearthstoneServices.Get<TavernBrawlManager>();
	}

	public TavernBrawlMission CurrentMission()
	{
		return GetMission(m_currentBrawlType);
	}

	public TavernBrawlMission GetMission(BrawlType brawlType)
	{
		if (brawlType < BrawlType.BRAWL_TYPE_TAVERN_BRAWL || brawlType >= BrawlType.BRAWL_TYPE_COUNT)
		{
			return null;
		}
		return m_missions[(int)brawlType];
	}

	public bool SelectHeroBeforeMission()
	{
		return SelectHeroBeforeMission(m_currentBrawlType);
	}

	public bool SelectHeroBeforeMission(BrawlType brawlType)
	{
		if (GetMission(brawlType) != null && GetMission(brawlType).canSelectHeroForDeck)
		{
			return !GetMission(brawlType).canCreateDeck;
		}
		return false;
	}

	public static bool IsInTavernBrawlFriendlyChallenge()
	{
		if (SceneMgr.Get().IsInTavernBrawlMode() || SceneMgr.Get().GetMode() == SceneMgr.Mode.FRIENDLY)
		{
			return FriendChallengeMgr.Get().IsChallengeTavernBrawl();
		}
		return false;
	}

	public TavernBrawlPlayerRecord GetRecord(BrawlType brawlType)
	{
		if (brawlType < BrawlType.BRAWL_TYPE_TAVERN_BRAWL || brawlType >= BrawlType.BRAWL_TYPE_COUNT)
		{
			return null;
		}
		return m_playerRecords[(int)brawlType];
	}

	public string GetStartingTimeText(bool singleLine = false)
	{
		long nextTavernBrawlSeasonStartInSeconds = NextTavernBrawlSeasonStartInSeconds;
		if (nextTavernBrawlSeasonStartInSeconds < 0)
		{
			return GameStrings.Get("GLUE_TAVERN_BRAWL_RETURNS_UNKNOWN" + (singleLine ? "_SINGLE_LINE" : ""));
		}
		TimeUtils.ElapsedStringSet stringSet = new TimeUtils.ElapsedStringSet
		{
			m_seconds = "GLUE_TAVERN_BRAWL_RETURNS_LESS_THAN_1_HOUR",
			m_minutes = "GLUE_TAVERN_BRAWL_RETURNS_LESS_THAN_1_HOUR",
			m_hours = "GLUE_TAVERN_BRAWL_RETURNS_HOURS",
			m_yesterday = null,
			m_days = "GLUE_TAVERN_BRAWL_RETURNS_DAYS",
			m_weeks = "GLUE_TAVERN_BRAWL_RETURNS_WEEKS",
			m_monthAgo = "GLUE_TAVERN_BRAWL_RETURNS_OVER_1_MONTH"
		};
		string text = TimeUtils.GetElapsedTimeString(nextTavernBrawlSeasonStartInSeconds, stringSet);
		if (singleLine)
		{
			text = text.Replace("\n", " ").Replace("\r", "");
		}
		return text;
	}

	public DeckRuleset GetCurrentDeckRuleset()
	{
		return GetDeckRuleset(m_currentBrawlType);
	}

	public DeckRuleset GetDeckRuleset(BrawlType brawlType, int brawlLibraryItemId = 0)
	{
		TavernBrawlMission mission = GetMission(brawlType);
		if (mission == null)
		{
			return null;
		}
		if (brawlLibraryItemId == 0)
		{
			brawlLibraryItemId = mission.SelectedBrawlLibraryItemId;
		}
		DeckRuleset deckRuleset = mission.GetDeckRuleset(brawlLibraryItemId);
		if (deckRuleset != null)
		{
			return deckRuleset;
		}
		return DeckRuleset.GetRuleset(mission.formatType);
	}

	public void StartGame(long deckId = 0L)
	{
		if (CurrentMission() == null)
		{
			Error.AddDevFatal("TB: m_currentMission is null");
			return;
		}
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.TAVERN_BRAWL_QUEUE);
		GameType gameType = CurrentMission().GameType;
		GameMgr.Get().FindGame(gameType, FormatType.FT_WILD, CurrentMission().missionId, 0, deckId);
	}

	public void StartGameWithHero(int heroCardDbId)
	{
		TavernBrawlMission tavernBrawlMission = CurrentMission();
		if (tavernBrawlMission == null)
		{
			Error.AddDevFatal("TB: m_currentMission is null");
			return;
		}
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.TAVERN_BRAWL_QUEUE);
		GameMgr.Get().FindGameWithHero(tavernBrawlMission.GameType, FormatType.FT_WILD, tavernBrawlMission.missionId, tavernBrawlMission.SelectedBrawlLibraryItemId, heroCardDbId, 0L);
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		if (!GameMgr.Get().IsNextTavernBrawl() || GameMgr.Get().IsNextSpectator())
		{
			return false;
		}
		switch (eventData.m_state)
		{
		case FindGameState.CLIENT_CANCELED:
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.BNET_ERROR:
		case FindGameState.SERVER_GAME_CANCELED:
			if (PresenceMgr.Get().CurrentStatus == Global.PresenceStatus.TAVERN_BRAWL_QUEUE)
			{
				PresenceMgr.Get().SetPrevStatus();
			}
			break;
		case FindGameState.SERVER_GAME_CONNECTING:
			if (GameMgr.Get().IsNextTavernBrawl() && GameMgr.Get().IsNextReconnect() && IsCurrentSeasonSessionBased)
			{
				SessionRecord sessionRecord = new SessionRecord();
				sessionRecord.Wins = (uint)GamesWon;
				sessionRecord.Losses = (uint)GamesLost;
				sessionRecord.RunFinished = false;
				sessionRecord.SessionRecordType = ((CurrentSeasonBrawlMode != 0) ? SessionRecordType.HEROIC_BRAWL : SessionRecordType.TAVERN_BRAWL);
				BnetPresenceMgr.Get().SetGameFieldBlob(22u, sessionRecord);
			}
			break;
		}
		return false;
	}

	private void ShowSessionLimitWarning()
	{
		int numSessionsAllowedThisSeason = Get().NumSessionsAllowedThisSeason;
		int numSessionsAvailableForPurchase = Get().NumSessionsAvailableForPurchase;
		if (numSessionsAllowedThisSeason == 0)
		{
			return;
		}
		string text;
		if (numSessionsAvailableForPurchase == 0)
		{
			text = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_LIMIT_ALERT_DESCRIPTION_FINAL");
		}
		else
		{
			if (numSessionsAllowedThisSeason - numSessionsAvailableForPurchase <= 1)
			{
				return;
			}
			text = GameStrings.Format("GLUE_HEROIC_BRAWL_SESSION_LIMIT_ALERT_DESCRIPTION_NORMAL", numSessionsAvailableForPurchase, numSessionsAvailableForPurchase);
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
		popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_LIMIT_ALERT_TITLE");
		popupInfo.m_text = text;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	public bool HasCreatedDeck()
	{
		return CurrentDeck() != null;
	}

	public CollectionDeck CurrentDeck()
	{
		return GetDeck(m_currentBrawlType);
	}

	public CollectionDeck GetDeck(BrawlType brawlType, int brawlLibraryItemId = 0)
	{
		TavernBrawlMission mission = GetMission(brawlType);
		if (mission == null)
		{
			return null;
		}
		if (brawlLibraryItemId == 0)
		{
			brawlLibraryItemId = mission.SelectedBrawlLibraryItemId;
		}
		foreach (CollectionDeck value in CollectionManager.Get().GetDecks().Values)
		{
			if (TranslateDeckTypeToBrawlType(value.Type) == brawlType && mission.seasonId == value.SeasonId && brawlLibraryItemId == value.BrawlLibraryItemId)
			{
				return value;
			}
		}
		return null;
	}

	public bool HasValidDeckForCurrent()
	{
		return HasValidDeck(m_currentBrawlType);
	}

	public bool HasValidDeck(BrawlType brawlType, int brawlLibraryItemId = 0)
	{
		TavernBrawlMission mission = GetMission(brawlType);
		if (mission == null || !mission.CanCreateDeck(brawlLibraryItemId))
		{
			return false;
		}
		CollectionDeck deck = GetDeck(brawlType, brawlLibraryItemId);
		if (deck == null)
		{
			return false;
		}
		if (!deck.NetworkContentsLoaded())
		{
			CollectionManager.Get().RequestDeckContents(deck.ID);
			return false;
		}
		DeckRuleset deckRuleset = GetDeckRuleset(brawlType, brawlLibraryItemId);
		if (deckRuleset != null && !deckRuleset.IsDeckValid(deck))
		{
			return false;
		}
		return true;
	}

	public static bool IsBrawlDeckType(DeckType deckType)
	{
		if ((uint)(deckType - 6) <= 1u)
		{
			return true;
		}
		return false;
	}

	private static BrawlType TranslateDeckTypeToBrawlType(DeckType deckType)
	{
		return deckType switch
		{
			DeckType.TAVERN_BRAWL_DECK => BrawlType.BRAWL_TYPE_TAVERN_BRAWL, 
			DeckType.FSG_BRAWL_DECK => BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING, 
			_ => BrawlType.BRAWL_TYPE_UNKNOWN, 
		};
	}

	public bool IsTavernBrawlActiveByDeckType(DeckType deckType)
	{
		BrawlType brawlType = TranslateDeckTypeToBrawlType(deckType);
		if (brawlType == BrawlType.BRAWL_TYPE_UNKNOWN)
		{
			return false;
		}
		return IsTavernBrawlActive(brawlType);
	}

	public bool IsSeasonActive(DeckType deckType, int seasonId, int brawlLibraryItemId)
	{
		BrawlType brawlType = TranslateDeckTypeToBrawlType(deckType);
		if (brawlType == BrawlType.BRAWL_TYPE_UNKNOWN)
		{
			return false;
		}
		if (!IsSeasonActive(brawlType, seasonId))
		{
			return false;
		}
		if (brawlLibraryItemId != 0)
		{
			TavernBrawlMission mission = GetMission(brawlType);
			if (mission == null || !mission.BrawlList.Any((GameContentScenario scen) => scen.LibraryItemId == brawlLibraryItemId))
			{
				return false;
			}
		}
		return true;
	}

	public bool IsSeasonActive(BrawlType brawlType, int seasonId)
	{
		if (!IsTavernBrawlActive(brawlType))
		{
			return false;
		}
		TavernBrawlMission tavernBrawlMission = m_missions[(int)brawlType];
		if (tavernBrawlMission == null || tavernBrawlMission.seasonId != seasonId)
		{
			return false;
		}
		if (tavernBrawlMission.BrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING && !FiresideGatheringManager.Get().IsCheckedIn)
		{
			return false;
		}
		return true;
	}

	public bool IsScenarioActiveInAnyBrawl(int scenarioId)
	{
		for (int i = 1; i < m_missions.Length; i++)
		{
			TavernBrawlMission tavernBrawlMission = m_missions[i];
			if (tavernBrawlMission != null && IsTavernBrawlActive(tavernBrawlMission.BrawlType) && tavernBrawlMission.missionId == scenarioId && (tavernBrawlMission.BrawlType != BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING || FiresideGatheringManager.Get().IsCheckedIn))
			{
				return true;
			}
		}
		return false;
	}

	public void EnsureAllDataReady(CallbackEnsureServerDataReady callback = null)
	{
		EnsureAllDataReady(m_currentBrawlType, callback);
	}

	public void EnsureAllDataReady(BrawlType brawlType, CallbackEnsureServerDataReady callback = null)
	{
		TavernBrawlMission mission = GetMission(brawlType);
		if (mission == null)
		{
			return;
		}
		if (IsAllDataReady(brawlType))
		{
			if (callback != null)
			{
				callback();
			}
			return;
		}
		if (callback != null)
		{
			if (m_serverDataReadyCallbacks == null)
			{
				m_serverDataReadyCallbacks = new List<CallbackEnsureServerDataReady>();
			}
			m_serverDataReadyCallbacks.Add(callback);
		}
		_ = mission.tavernBrawlSpec;
		List<AssetRecordInfo> list = new List<AssetRecordInfo>();
		foreach (GameContentScenario brawl in mission.BrawlList)
		{
			AssetRecordInfo assetRecordInfo = new AssetRecordInfo();
			assetRecordInfo.Asset = new AssetKey();
			assetRecordInfo.Asset.Type = AssetType.ASSET_TYPE_SCENARIO;
			assetRecordInfo.Asset.AssetId = brawl.ScenarioId;
			assetRecordInfo.RecordByteSize = brawl.ScenarioRecordByteSize;
			assetRecordInfo.RecordHash = brawl.ScenarioRecordHash;
			list.Add(assetRecordInfo);
			if (brawl.AdditionalAssets != null && brawl.AdditionalAssets.Count > 0)
			{
				list.AddRange(brawl.AdditionalAssets);
			}
		}
		if (DownloadableDbfCache.Get().IsAssetRequestInProgress(mission.missionId, AssetType.ASSET_TYPE_SCENARIO))
		{
			DownloadableDbfCache.Get().LoadCachedAssets(canRequestFromServer: false, delegate(AssetKey requestedKey, ErrorCode code, byte[] assetBytes)
			{
				OnDownloadableDbfAssetsLoaded(requestedKey, code, assetBytes, brawlType);
			}, list.ToArray());
		}
		else if (HearthstoneApplication.IsInternal())
		{
			Processor.ScheduleCallback(Mathf.Max(0f, UnityEngine.Random.Range(-3f, 3f)), realTime: false, delegate
			{
				TavernBrawlManager tavernBrawlManager = Get();
				if (tavernBrawlManager.IsAllDataReady(brawlType))
				{
					if (callback != null)
					{
						if (tavernBrawlManager.m_serverDataReadyCallbacks != null)
						{
							tavernBrawlManager.m_serverDataReadyCallbacks.Remove(callback);
						}
						callback();
					}
				}
				else
				{
					GameContentSeasonSpec gameContentSeason = mission.tavernBrawlSpec.GameContentSeason;
					List<AssetRecordInfo> list2 = new List<AssetRecordInfo>();
					foreach (GameContentScenario scenario in gameContentSeason.Scenarios)
					{
						AssetRecordInfo assetRecordInfo2 = new AssetRecordInfo();
						assetRecordInfo2.Asset = new AssetKey();
						assetRecordInfo2.Asset.Type = AssetType.ASSET_TYPE_SCENARIO;
						assetRecordInfo2.Asset.AssetId = scenario.ScenarioId;
						assetRecordInfo2.RecordByteSize = scenario.ScenarioRecordByteSize;
						assetRecordInfo2.RecordHash = scenario.ScenarioRecordHash;
						list2.Add(assetRecordInfo2);
						if (scenario.AdditionalAssets != null && scenario.AdditionalAssets.Count > 0)
						{
							list2.AddRange(scenario.AdditionalAssets);
						}
					}
					DownloadableDbfCache.Get().LoadCachedAssets(canRequestFromServer: true, delegate(AssetKey requestedKey, ErrorCode code, byte[] assetBytes)
					{
						OnDownloadableDbfAssetsLoaded(requestedKey, code, assetBytes, brawlType);
					}, list2.ToArray());
				}
			});
		}
		else
		{
			DownloadableDbfCache.Get().LoadCachedAssets(canRequestFromServer: true, delegate(AssetKey requestedKey, ErrorCode code, byte[] assetBytes)
			{
				OnDownloadableDbfAssetsLoaded(requestedKey, code, assetBytes, brawlType);
			}, list.ToArray());
		}
	}

	private bool IsAllDataReady(BrawlType brawlType)
	{
		if (brawlType < BrawlType.BRAWL_TYPE_TAVERN_BRAWL || brawlType >= BrawlType.BRAWL_TYPE_COUNT)
		{
			return true;
		}
		TavernBrawlMission mission = GetMission(brawlType);
		if (mission == null)
		{
			return true;
		}
		if (m_downloadableDbfAssetsPendingLoad[(int)brawlType])
		{
			return false;
		}
		if (mission.BrawlList.Any((GameContentScenario brawl) => GameDbf.Scenario.GetRecord(brawl.ScenarioId) == null))
		{
			return false;
		}
		return true;
	}

	public void RefreshServerData(BrawlType brawlType = BrawlType.BRAWL_TYPE_UNKNOWN)
	{
		brawlType = ((brawlType == BrawlType.BRAWL_TYPE_UNKNOWN) ? m_currentBrawlType : brawlType);
		Network.Get().RequestTavernBrawlInfo(brawlType);
	}

	public bool HasUnlockedTavernBrawl(BrawlType brawlType)
	{
		NetCache.NetCacheHeroLevels netObject = NetCache.Get().GetNetObject<NetCache.NetCacheHeroLevels>();
		return brawlType switch
		{
			BrawlType.BRAWL_TYPE_TAVERN_BRAWL => netObject?.Levels.Any((NetCache.HeroLevel l) => l.CurrentLevel.Level >= 20) ?? false, 
			BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING => netObject?.Levels.Any((NetCache.HeroLevel l) => l.CurrentLevel.Level >= 1) ?? false, 
			_ => true, 
		};
	}

	public bool CanChallengeToTavernBrawl(BrawlType brawlType)
	{
		if (!IsTavernBrawlActive(brawlType))
		{
			return false;
		}
		TavernBrawlMission mission = GetMission(brawlType);
		if (GameUtils.IsAIMission(mission.missionId))
		{
			return false;
		}
		if (mission.friendlyChallengeDisabled)
		{
			return false;
		}
		return true;
	}

	public bool IsEligibleForFreeTicket()
	{
		if (CurrentSession == null || CurrentMission() == null)
		{
			return false;
		}
		uint sessionCount = CurrentSession.SessionCount;
		uint freeSessions = CurrentMission().FreeSessions;
		if (freeSessions != 0)
		{
			return sessionCount < freeSessions;
		}
		return false;
	}

	public bool IsTavernBrawlActive(BrawlType brawlType)
	{
		TavernBrawlMission tavernBrawlMission = m_missions[(int)brawlType];
		if (brawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING && !FiresideGatheringManager.Get().IsCheckedIn)
		{
			return false;
		}
		if (tavernBrawlMission != null)
		{
			return TavernBrawlSeasonEndInSeconds(brawlType) > 0;
		}
		return false;
	}

	public void RefreshPlayerRecord()
	{
		Network.Get().RequestTavernBrawlPlayerRecord(m_currentBrawlType);
	}

	public long TavernBrawlSeasonStartInSeconds(BrawlType brawlType)
	{
		DateTime? dateTime = m_nextSeasonStartDates[(int)brawlType];
		if (!dateTime.HasValue || !dateTime.HasValue)
		{
			return -1L;
		}
		return (long)(dateTime.Value - DateTime.Now).TotalSeconds;
	}

	public float ScheduledSecondsToRefresh(BrawlType brawlType)
	{
		DateTime? dateTime = m_scheduledRefreshTimes[(int)brawlType];
		if (!dateTime.HasValue || !dateTime.HasValue)
		{
			return -1f;
		}
		return (float)(dateTime.Value - DateTime.Now).TotalSeconds;
	}

	public long TavernBrawlSeasonNewSessionsClosedInSeconds(BrawlType brawlType)
	{
		TavernBrawlMission mission = GetMission(brawlType);
		if (mission == null || !mission.closedToNewSessionsDateLocal.HasValue)
		{
			return 2147483647L;
		}
		return (long)(mission.closedToNewSessionsDateLocal.Value - DateTime.Now).TotalSeconds;
	}

	public bool IsSeasonSessionBased(BrawlType brawlType)
	{
		return GetMission(brawlType)?.IsSessionBased ?? false;
	}

	public TavernBrawlMode GetBrawlModeForBrawlType(BrawlType brawlType)
	{
		return GetMission(brawlType)?.brawlMode ?? TavernBrawlMode.TB_MODE_NORMAL;
	}

	public void RequestSessionBegin()
	{
		Network.Get().RequestTavernBrawlSessionBegin();
	}

	private void RegisterOptionsListeners(bool register)
	{
		if (register)
		{
			NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheClientOptions), NetCache_OnClientOptions);
			Options.Get().RegisterChangedListener(Option.LATEST_SEEN_TAVERNBRAWL_SEASON, OnOptionChangedCallback);
			Options.Get().RegisterChangedListener(Option.LATEST_SEEN_TAVERNBRAWL_SEASON_CHALKBOARD, OnOptionChangedCallback);
			Options.Get().RegisterChangedListener(Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON, OnOptionChangedCallback);
			Options.Get().RegisterChangedListener(Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON_CHALKBOARD, OnOptionChangedCallback);
		}
		else
		{
			NetCache.Get().RemoveUpdatedListener(typeof(NetCache.NetCacheClientOptions), NetCache_OnClientOptions);
			Options.Get().UnregisterChangedListener(Option.LATEST_SEEN_TAVERNBRAWL_SEASON, OnOptionChangedCallback);
			Options.Get().UnregisterChangedListener(Option.LATEST_SEEN_TAVERNBRAWL_SEASON_CHALKBOARD, OnOptionChangedCallback);
			Options.Get().UnregisterChangedListener(Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON, OnOptionChangedCallback);
			Options.Get().UnregisterChangedListener(Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON_CHALKBOARD, OnOptionChangedCallback);
		}
	}

	private void NetCache_OnClientOptions()
	{
		RegisterOptionsListeners(register: false);
		bool seasonHasChanged = CheckLatestSeenSeason(canSetOption: true);
		CheckLatestSessionLimit(seasonHasChanged);
		RegisterOptionsListeners(register: true);
	}

	private void NetCache_OnTavernBrawlRecord()
	{
		if (this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}

	private void OnOptionChangedCallback(Option option, object prevValue, bool existed, object userData)
	{
		RegisterOptionsListeners(register: false);
		bool seasonHasChanged = CheckLatestSeenSeason(canSetOption: false);
		CheckLatestSessionLimit(seasonHasChanged);
		RegisterOptionsListeners(register: true);
	}

	private bool CheckLatestSeenSeason(bool canSetOption)
	{
		bool result = false;
		if (!IsCurrentBrawlInfoReady)
		{
			return result;
		}
		bool num = !m_hasGottenClientOptionsAtLeastOnce;
		m_hasGottenClientOptionsAtLeastOnce = true;
		bool isFirstTimeSeeingThisFeature = IsFirstTimeSeeingThisFeature;
		bool flag = CurrentMission() != null && LatestSeenTavernBrawlSeason < CurrentMission().seasonId;
		m_isFirstTimeSeeingThisFeature = false;
		m_isFirstTimeSeeingCurrentSeason = false;
		TavernBrawlMission tavernBrawlMission = CurrentMission();
		if (tavernBrawlMission != null)
		{
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			bool flag2 = netObject != null && netObject.Games.TavernBrawl && HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
			int latestSeenTavernBrawlSeason = LatestSeenTavernBrawlSeason;
			if (latestSeenTavernBrawlSeason == 0 && flag2 && tavernBrawlMission.BrawlType != BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
			{
				m_isFirstTimeSeeingThisFeature = true;
				NotificationManager.Get().ForceRemoveSoundFromPlayedList("VO_INNKEEPER_TAVERNBRAWL_PUSH_32.prefab:4f57cd2af5fe5194fbc46c91171ab135");
				NotificationManager.Get().ForceRemoveSoundFromPlayedList("VO_INNKEEPER_TAVERNBRAWL_WELCOME1_27.prefab:094070b7fecad8548b0b8fdb02bde052");
			}
			if (latestSeenTavernBrawlSeason < tavernBrawlMission.seasonId && flag2)
			{
				m_isFirstTimeSeeingCurrentSeason = true;
				NotificationManager.Get().ForceRemoveSoundFromPlayedList("VO_INNKEEPER_TAVERNBRAWL_DESC2_30.prefab:498657df8d08bc1468bfd1ad9f74ccac");
				Hub.s_hasAlreadyShownTBAnimation = false;
				if (canSetOption)
				{
					LatestSeenTavernBrawlSeason = tavernBrawlMission.seasonId;
				}
				result = true;
			}
		}
		if ((num || isFirstTimeSeeingThisFeature != IsFirstTimeSeeingThisFeature || flag != IsFirstTimeSeeingCurrentSeason) && this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
		return result;
	}

	private void CheckLatestSessionLimit(bool seasonHasChanged)
	{
		if (!IsCurrentBrawlInfoReady)
		{
			return;
		}
		TavernBrawlMission tavernBrawlMission = CurrentMission();
		if (tavernBrawlMission == null)
		{
			return;
		}
		if (seasonHasChanged)
		{
			Options.Get().SetInt(Option.LATEST_SEEN_TAVERNBRAWL_SESSION_LIMIT, tavernBrawlMission.maxSessions);
			return;
		}
		int @int = Options.Get().GetInt(Option.LATEST_SEEN_TAVERNBRAWL_SESSION_LIMIT);
		if (@int == tavernBrawlMission.maxSessions)
		{
			return;
		}
		if (@int == 0)
		{
			Options.Get().SetInt(Option.LATEST_SEEN_TAVERNBRAWL_SESSION_LIMIT, tavernBrawlMission.maxSessions);
			return;
		}
		if (tavernBrawlMission.maxSessions > @int && this.OnSessionLimitRaised != null)
		{
			this.OnSessionLimitRaised(@int, tavernBrawlMission.maxSessions);
		}
		Options.Get().SetInt(Option.LATEST_SEEN_TAVERNBRAWL_SESSION_LIMIT, tavernBrawlMission.maxSessions);
	}

	private void ScheduleTimedCallbacksForBrawl(TavernBrawlInfo serverInfo)
	{
		if (serverInfo.HasNextStartSecondsFromNow)
		{
			m_nextSeasonStartDates[(int)serverInfo.BrawlType] = DateTime.Now + new TimeSpan(0, 0, (int)serverInfo.NextStartSecondsFromNow);
		}
		else
		{
			m_nextSeasonStartDates[(int)serverInfo.BrawlType] = null;
		}
		Processor.CancelScheduledCallback(ScheduledEndOfCurrentTBCallback, serverInfo.BrawlType);
		long num = TavernBrawlSeasonEndInSeconds(serverInfo.BrawlType);
		if (IsTavernBrawlActive(serverInfo.BrawlType) && num > 0)
		{
			Log.EventTiming.Print("Scheduling end of current {0} {1} secs from now.", serverInfo.BrawlType, num);
			Processor.ScheduleCallback(num, realTime: true, ScheduledEndOfCurrentTBCallback, serverInfo.BrawlType);
		}
		Processor.CancelScheduledCallback(ScheduledRefreshTBSpecCallback, serverInfo.BrawlType);
		long num2 = TavernBrawlSeasonStartInSeconds(serverInfo.BrawlType);
		if (num2 >= 0)
		{
			m_scheduledRefreshTimes[(int)serverInfo.BrawlType] = DateTime.Now + new TimeSpan(0, 0, 0, (int)num2, 0);
			Log.EventTiming.Print("Scheduling {0} refresh for {1} secs from now.", serverInfo.BrawlType, num2);
			Processor.ScheduleCallback(num2, realTime: true, ScheduledRefreshTBSpecCallback, serverInfo.BrawlType);
		}
		long num3 = TavernBrawlSeasonNewSessionsClosedInSeconds(serverInfo.BrawlType);
		if (IsSeasonSessionBased(serverInfo.BrawlType) && num3 > 0)
		{
			Log.EventTiming.Print("Scheduling {0} Closed Update for {1} secs from now.", serverInfo.BrawlType, num3);
			Processor.ScheduleCallback(num3, realTime: true, ScheduleTBClosedUpdateCallback, serverInfo.BrawlType);
		}
	}

	private void OnCheckInToFSGResponse()
	{
		CheckInToFSGResponse checkInToFSGResponse = Network.Get().GetCheckInToFSGResponse();
		if (checkInToFSGResponse.ErrorCode == ErrorCode.ERROR_OK)
		{
			if (checkInToFSGResponse.HasPlayerRecord)
			{
				m_playerRecords[2] = checkInToFSGResponse.PlayerRecord;
			}
			if (m_currentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING && this.OnTavernBrawlUpdated != null)
			{
				this.OnTavernBrawlUpdated();
			}
		}
	}

	private void OnFiresideGatheringLeave()
	{
		if (m_currentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING && this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}

	private void ScheduledEndOfCurrentTBCallback(object userData)
	{
		Log.EventTiming.Print("ScheduledEndOfCurrentTBCallback: ending current TB now.");
		bool flag = TavernBrawlDisplay.Get() != null && TavernBrawlDisplay.Get().IsInRewards();
		BrawlType brawlType = (BrawlType)userData;
		TavernBrawlMission tavernBrawlMission = m_missions[(int)m_currentBrawlType];
		TavernBrawlPlayerRecord tavernBrawlPlayerRecord = m_playerRecords[(int)m_currentBrawlType];
		if (tavernBrawlMission != null && tavernBrawlMission.IsSessionBased && (tavernBrawlPlayerRecord.SessionStatus == TavernBrawlStatus.TB_STATUS_ACTIVE || tavernBrawlPlayerRecord.SessionStatus == TavernBrawlStatus.TB_STATUS_IN_REWARDS) && (brawlType != m_currentBrawlType || !flag))
		{
			int num = 2;
			num = ((tavernBrawlMission.SeasonEndSecondsSpreadCount <= 0) ? (num + UnityEngine.Random.Range(0, 30)) : (num + tavernBrawlMission.SeasonEndSecondsSpreadCount));
			Processor.ScheduleCallback(num, realTime: true, ScheduledEndOfCurrentTBCallback_AfterSpreadWhenRewardsExpected, brawlType);
		}
		if (brawlType == m_currentBrawlType)
		{
			m_missions[(int)brawlType] = null;
			if (GameMgr.Get().IsFindingGame())
			{
				GameMgr.Get().CancelFindGame();
			}
			if (this.OnTavernBrawlUpdated != null)
			{
				this.OnTavernBrawlUpdated();
			}
		}
	}

	private void ScheduledEndOfCurrentTBCallback_AfterSpreadWhenRewardsExpected(object userData)
	{
		BrawlType brawlType = (BrawlType)userData;
		Network.Get().RequestTavernBrawlPlayerRecord(brawlType);
	}

	private void ScheduledRefreshTBSpecCallback(object userData)
	{
		BrawlType brawlType = (BrawlType)userData;
		Log.EventTiming.Print("ScheduledRefreshTBSpecCallback: refreshing now.");
		RefreshServerData(brawlType);
	}

	private void ScheduleTBClosedUpdateCallback(object userData)
	{
		BrawlType num = (BrawlType)userData;
		Log.EventTiming.Print("ScheduledUpdateTBCallback: updating now.");
		if (num == m_currentBrawlType && this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}

	private void OnDownloadableDbfAssetsLoaded(AssetKey requestedKey, ErrorCode code, byte[] assetBytes, BrawlType brawlType)
	{
		if (requestedKey == null || requestedKey.Type != AssetType.ASSET_TYPE_SCENARIO)
		{
			Log.TavernBrawl.Print("OnDownloadableDbfAssetsLoaded bad AssetType assetId={0} assetType={1} {2}", requestedKey?.AssetId ?? 0, (int)(requestedKey?.Type ?? ((AssetType)0)), (requestedKey == null) ? "(null)" : requestedKey.Type.ToString());
			return;
		}
		if (assetBytes == null || assetBytes.Length == 0)
		{
			Log.TavernBrawl.PrintError("OnDownloadableDbfAssetsLoaded failed to load Asset: assetId={0} assetType={1} {2} error={3}", requestedKey?.AssetId ?? 0, (int)(requestedKey?.Type ?? ((AssetType)0)), (requestedKey == null) ? "(null)" : requestedKey.Type.ToString(), code);
			return;
		}
		TavernBrawlMission tavernBrawlMission = m_missions[(int)brawlType];
		if (tavernBrawlMission == null)
		{
			return;
		}
		ScenarioDbRecord scenarioDbRecord = ProtobufUtil.ParseFrom<ScenarioDbRecord>(assetBytes, 0, assetBytes.Length);
		if (tavernBrawlMission.BrawlList.Count != 0 && tavernBrawlMission.BrawlList.First().ScenarioId == scenarioDbRecord.Id)
		{
			m_downloadableDbfAssetsPendingLoad[(int)brawlType] = false;
			if (m_currentBrawlType == brawlType)
			{
				Processor.RunCoroutine(OnDownloadableDbfAssetsLoaded_EnsureCurrentBrawlDeckContentsLoaded());
			}
		}
	}

	private IEnumerator OnDownloadableDbfAssetsLoaded_EnsureCurrentBrawlDeckContentsLoaded()
	{
		foreach (CollectionDeck value in CollectionManager.Get().GetDecks().Values)
		{
			if (TranslateDeckTypeToBrawlType(value.Type) == m_currentBrawlType && !value.NetworkContentsLoaded())
			{
				CollectionManager.Get().RequestDeckContents(value.ID);
			}
		}
		if (CurrentMission() != null && !CurrentBrawlDeckContentsLoaded)
		{
			float timeAtStart = Time.realtimeSinceStartup;
			bool done = false;
			while (!done)
			{
				yield return null;
				if (Time.realtimeSinceStartup - timeAtStart > 30f)
				{
					done = true;
				}
				else if (!IsCurrentBrawlAllDataReady)
				{
					done = true;
				}
				else if (CurrentMission() == null)
				{
					done = true;
				}
				else if (CurrentBrawlDeckContentsLoaded)
				{
					done = true;
				}
			}
		}
		if (!IsCurrentBrawlAllDataReady)
		{
			yield break;
		}
		if (this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
		if (m_serverDataReadyCallbacks != null)
		{
			CallbackEnsureServerDataReady[] array = m_serverDataReadyCallbacks.ToArray();
			m_serverDataReadyCallbacks.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				array[i]();
			}
		}
	}

	private long TavernBrawlSeasonEndInSeconds(BrawlType brawlType)
	{
		TavernBrawlMission tavernBrawlMission = m_missions[(int)brawlType];
		if (tavernBrawlMission == null)
		{
			return -1L;
		}
		if (!tavernBrawlMission.endDateLocal.HasValue)
		{
			return 2147483647L;
		}
		return (long)(tavernBrawlMission.endDateLocal.Value - DateTime.Now).TotalSeconds;
	}

	private void OnTavernBrawlRecord_Internal(TavernBrawlPlayerRecord record)
	{
		if (record != null)
		{
			m_playerRecords[(int)record.BrawlType] = record;
			if (m_currentBrawlType == record.BrawlType && this.OnTavernBrawlUpdated != null)
			{
				this.OnTavernBrawlUpdated();
			}
		}
	}

	private void OnTavernBrawlInfo_Internal(TavernBrawlInfo serverInfo)
	{
		if (serverInfo == null)
		{
			return;
		}
		int brawlType = (int)serverInfo.BrawlType;
		if (brawlType < 0 || brawlType >= m_missions.Length)
		{
			Log.TavernBrawl.PrintError("OnTavernBrawlInfo_Internal: received invalid index for BrawlType={0} arrayLength={1}", brawlType, m_missions.Length);
			return;
		}
		if (!serverInfo.HasCurrentTavernBrawl)
		{
			m_missions[brawlType] = null;
		}
		else
		{
			if (m_missions[brawlType] == null)
			{
				m_missions[brawlType] = new TavernBrawlMission();
			}
			m_missions[brawlType].SetSeasonSpec(serverInfo.CurrentTavernBrawl, serverInfo.BrawlType);
			m_downloadableDbfAssetsPendingLoad[brawlType] = true;
			if (this.OnTavernBrawlUpdated != null)
			{
				EnsureAllDataReady(serverInfo.BrawlType);
			}
		}
		bool seasonHasChanged = CheckLatestSeenSeason(canSetOption: true);
		CheckLatestSessionLimit(seasonHasChanged);
		ScheduleTimedCallbacksForBrawl(serverInfo);
		if (serverInfo.HasMyRecord)
		{
			OnTavernBrawlRecord_Internal(serverInfo.MyRecord);
		}
		if (m_currentBrawlType == serverInfo.BrawlType && this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}

	private void OnBeginSession()
	{
		Log.TavernBrawl.Print($"TavernBrawlManager.OnBeginSession");
		TavernBrawlRequestSessionBeginResponse tavernBrawlSessionBegin = Network.Get().GetTavernBrawlSessionBegin();
		if (tavernBrawlSessionBegin.HasErrorCode && tavernBrawlSessionBegin.ErrorCode != 0)
		{
			string text = tavernBrawlSessionBegin.ErrorCode.ToString();
			Debug.LogWarning(string.Concat("TavernBrawlManager.OnBeginSession: Got Error ", tavernBrawlSessionBegin.ErrorCode, " : ", text));
			if (SceneMgr.Get().IsSceneLoaded() && ((!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.TAVERN_BRAWL) && !SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FIRESIDE_GATHERING)) || Get().PlayerStatus != TavernBrawlStatus.TB_STATUS_ACTIVE))
			{
				if (TavernBrawlStore.Get() != null)
				{
					TavernBrawlStore.Get().Hide();
				}
				if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
				{
					SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
				}
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				if (CurrentMission().brawlMode == TavernBrawlMode.TB_MODE_HEROIC)
				{
					popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_ERROR_TITLE");
					popupInfo.m_text = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_ERROR");
				}
				else
				{
					popupInfo.m_headerText = GameStrings.Get("GLUE_BRAWLISEUM_SESSION_ERROR_TITLE");
					popupInfo.m_text = GameStrings.Get("GLUE_BRAWLISEUM_SESSION_ERROR");
				}
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
				DialogManager.Get().ShowPopup(popupInfo);
			}
		}
		else
		{
			SessionRecord sessionRecord = new SessionRecord();
			sessionRecord.Wins = 0u;
			sessionRecord.Losses = 0u;
			sessionRecord.RunFinished = false;
			sessionRecord.SessionRecordType = SessionRecordType.TAVERN_BRAWL;
			BnetPresenceMgr.Get().SetGameFieldBlob(22u, sessionRecord);
			if (tavernBrawlSessionBegin.HasPlayerRecord)
			{
				OnTavernBrawlRecord_Internal(tavernBrawlSessionBegin.PlayerRecord);
			}
			ShowSessionLimitWarning();
			if (this.OnTavernBrawlUpdated != null)
			{
				this.OnTavernBrawlUpdated();
			}
		}
	}

	private void OnRetireSession()
	{
		Log.TavernBrawl.Print($"TavernBrawlManager.OnRetireSession");
		CollectionManager.Get()?.DoneEditing();
		TavernBrawlRequestSessionRetireResponse tavernBrawlSessionRetired = Network.Get().GetTavernBrawlSessionRetired();
		if (tavernBrawlSessionRetired.ErrorCode != 0)
		{
			string text = tavernBrawlSessionRetired.ErrorCode.ToString();
			Debug.LogWarning(string.Concat("TavernBrawlManager.OnRetireSession: Got Error ", tavernBrawlSessionRetired.ErrorCode, " : ", text));
			return;
		}
		if (tavernBrawlSessionRetired.HasPlayerRecord)
		{
			OnTavernBrawlRecord_Internal(tavernBrawlSessionRetired.PlayerRecord);
		}
		MyRecord.SessionStatus = TavernBrawlStatus.TB_STATUS_IN_REWARDS;
		CurrentSession.Chest = tavernBrawlSessionRetired.Chest;
		if (this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}

	private void OnAckRewards()
	{
		Log.TavernBrawl.Print($"TavernBrawlManager.OnAckRewards");
		SessionRecord sessionRecord = new SessionRecord();
		sessionRecord.Wins = (uint)GamesWon;
		sessionRecord.Losses = (uint)GamesLost;
		sessionRecord.RunFinished = true;
		sessionRecord.SessionRecordType = ((CurrentSeasonBrawlMode != 0) ? SessionRecordType.HEROIC_BRAWL : SessionRecordType.TAVERN_BRAWL);
		BnetPresenceMgr.Get().SetGameFieldBlob(22u, sessionRecord);
		Network.Get().RequestTavernBrawlPlayerRecord(m_currentBrawlType);
		if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
		}
	}

	private void OnTavernBrawlRecord()
	{
		TavernBrawlPlayerRecord tavernBrawlRecord = Network.Get().GetTavernBrawlRecord();
		OnTavernBrawlRecord_Internal(tavernBrawlRecord);
	}

	private void OnTavernBrawlInfo()
	{
		TavernBrawlInfo tavernBrawlInfo = Network.Get().GetTavernBrawlInfo();
		OnTavernBrawlInfo_Internal(tavernBrawlInfo);
	}

	public void Cheat_SetScenario(int scenarioId, BrawlType brawlType = BrawlType.BRAWL_TYPE_TAVERN_BRAWL)
	{
		if (!HearthstoneApplication.IsPublic())
		{
			IsCheated = true;
			int num = (int)brawlType;
			if (m_missions[num] == null)
			{
				m_missions[num] = new TavernBrawlMission();
			}
			m_missions[num].SetSeasonSpec(new TavernBrawlSeasonSpec(), brawlType);
			m_missions[num].tavernBrawlSpec.GameContentSeason.Scenarios.Add(new GameContentScenario());
			m_missions[num].tavernBrawlSpec.GameContentSeason.Scenarios[0].ScenarioId = scenarioId;
			m_downloadableDbfAssetsPendingLoad[(int)brawlType] = true;
			if (this.OnTavernBrawlUpdated != null)
			{
				this.OnTavernBrawlUpdated();
			}
			AssetRecordInfo assetRecordInfo = new AssetRecordInfo();
			assetRecordInfo.Asset = new AssetKey();
			assetRecordInfo.Asset.Type = AssetType.ASSET_TYPE_SCENARIO;
			assetRecordInfo.Asset.AssetId = scenarioId;
			assetRecordInfo.RecordByteSize = 0u;
			assetRecordInfo.RecordHash = null;
			DownloadableDbfCache.Get().LoadCachedAssets(true, delegate(AssetKey requestedKey, ErrorCode code, byte[] assetBytes)
			{
				OnDownloadableDbfAssetsLoaded(requestedKey, code, assetBytes, brawlType);
			}, assetRecordInfo);
		}
	}

	public void Cheat_ResetToServerData()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		IsCheated = false;
		OnTavernBrawlInfo();
		if (CurrentMission() != null)
		{
			AssetRecordInfo assetRecordInfo = new AssetRecordInfo();
			assetRecordInfo.Asset = new AssetKey();
			assetRecordInfo.Asset.Type = AssetType.ASSET_TYPE_SCENARIO;
			assetRecordInfo.Asset.AssetId = CurrentMission().missionId;
			assetRecordInfo.RecordByteSize = 0u;
			assetRecordInfo.RecordHash = null;
			DownloadableDbfCache.Get().LoadCachedAssets(true, delegate(AssetKey requestedKey, ErrorCode code, byte[] assetBytes)
			{
				OnDownloadableDbfAssetsLoaded(requestedKey, code, assetBytes, BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
			}, assetRecordInfo);
		}
	}

	public void Cheat_ResetSeenStuff(int newValue)
	{
		if (!HearthstoneApplication.IsPublic())
		{
			RegisterOptionsListeners(register: false);
			LatestSeenTavernBrawlChalkboard = newValue;
			LatestSeenTavernBrawlSeason = newValue;
			Options.Get().SetInt(Option.TIMES_SEEN_TAVERNBRAWL_CRAZY_RULES_QUOTE, 0);
			bool seasonHasChanged = CheckLatestSeenSeason(canSetOption: false);
			CheckLatestSessionLimit(seasonHasChanged);
			RegisterOptionsListeners(register: true);
		}
	}

	public void Cheat_SetWins(int numWins)
	{
		if (!HearthstoneApplication.IsPublic())
		{
			CurrentSession.Wins = numWins;
			if (this.OnTavernBrawlUpdated != null)
			{
				this.OnTavernBrawlUpdated();
			}
		}
	}

	public void Cheat_SetLosses(int numLosses)
	{
		if (!HearthstoneApplication.IsPublic())
		{
			CurrentSession.Losses = numLosses;
			if (this.OnTavernBrawlUpdated != null)
			{
				this.OnTavernBrawlUpdated();
			}
		}
	}

	public void Cheat_SetActiveSession(int status)
	{
		MyRecord.SessionStatus = (TavernBrawlStatus)status;
		TavernBrawlPlayerSession session = new TavernBrawlPlayerSession();
		MyRecord.Session = session;
	}

	public void Cheat_DoHeroicRewards(int wins, TavernBrawlMode mode)
	{
		MyRecord.SessionStatus = TavernBrawlStatus.TB_STATUS_IN_REWARDS;
		CurrentSession.Chest = RewardUtils.GenerateTavernBrawlRewardChest_CHEAT(wins, mode);
		CurrentSession.Wins = wins;
		if (this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}
}
