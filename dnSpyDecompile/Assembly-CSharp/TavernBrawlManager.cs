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

// Token: 0x02000738 RID: 1848
public class TavernBrawlManager : IService
{
	// Token: 0x0600683C RID: 26684 RVA: 0x0021FD5F File Offset: 0x0021DF5F
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		Network network = serviceLocator.Get<Network>();
		NetCache netCache = NetCache.Get();
		network.RegisterNetHandler(TavernBrawlRequestSessionBeginResponse.PacketID.ID, new Network.NetHandler(this.OnBeginSession), null);
		network.RegisterNetHandler(TavernBrawlRequestSessionRetireResponse.PacketID.ID, new Network.NetHandler(this.OnRetireSession), null);
		network.RegisterNetHandler(TavernBrawlSessionAckRewardsResponse.PacketID.ID, new Network.NetHandler(this.OnAckRewards), null);
		network.RegisterNetHandler(TavernBrawlPlayerRecordResponse.PacketID.ID, new Network.NetHandler(this.OnTavernBrawlRecord), null);
		network.RegisterNetHandler(TavernBrawlInfo.PacketID.ID, new Network.NetHandler(this.OnTavernBrawlInfo), null);
		network.RegisterNetHandler(CheckInToFSGResponse.PacketID.ID, new Network.NetHandler(this.OnCheckInToFSGResponse), null);
		netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheHeroLevels), new Action(this.NetCache_OnClientOptions));
		serviceLocator.Get<GameMgr>().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		this.RegisterOptionsListeners(true);
		yield break;
	}

	// Token: 0x0600683D RID: 26685 RVA: 0x0021FD75 File Offset: 0x0021DF75
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network),
			typeof(GameMgr),
			typeof(NetCache)
		};
	}

	// Token: 0x0600683E RID: 26686 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x0600683F RID: 26687 RVA: 0x0021FDA4 File Offset: 0x0021DFA4
	public static TavernBrawlManager Get()
	{
		return HearthstoneServices.Get<TavernBrawlManager>();
	}

	// Token: 0x14000071 RID: 113
	// (add) Token: 0x06006840 RID: 26688 RVA: 0x0021FDAC File Offset: 0x0021DFAC
	// (remove) Token: 0x06006841 RID: 26689 RVA: 0x0021FDE4 File Offset: 0x0021DFE4
	public event Action OnTavernBrawlUpdated;

	// Token: 0x14000072 RID: 114
	// (add) Token: 0x06006842 RID: 26690 RVA: 0x0021FE1C File Offset: 0x0021E01C
	// (remove) Token: 0x06006843 RID: 26691 RVA: 0x0021FE54 File Offset: 0x0021E054
	public event TavernBrawlManager.TavernBrawlSessionLimitRaisedCallback OnSessionLimitRaised;

	// Token: 0x17000639 RID: 1593
	// (get) Token: 0x06006844 RID: 26692 RVA: 0x0021FE89 File Offset: 0x0021E089
	// (set) Token: 0x06006845 RID: 26693 RVA: 0x0021FE91 File Offset: 0x0021E091
	public BrawlType CurrentBrawlType
	{
		get
		{
			return this.m_currentBrawlType;
		}
		set
		{
			if (value >= BrawlType.BRAWL_TYPE_TAVERN_BRAWL && value < BrawlType.BRAWL_TYPE_COUNT)
			{
				this.m_currentBrawlType = value;
			}
		}
	}

	// Token: 0x1700063A RID: 1594
	// (get) Token: 0x06006846 RID: 26694 RVA: 0x0021FEA2 File Offset: 0x0021E0A2
	public bool IsCurrentBrawlTypeActive
	{
		get
		{
			return this.IsTavernBrawlActive(this.m_currentBrawlType);
		}
	}

	// Token: 0x06006847 RID: 26695 RVA: 0x0021FEB0 File Offset: 0x0021E0B0
	public TavernBrawlMission CurrentMission()
	{
		return this.GetMission(this.m_currentBrawlType);
	}

	// Token: 0x06006848 RID: 26696 RVA: 0x0021FEBE File Offset: 0x0021E0BE
	public TavernBrawlMission GetMission(BrawlType brawlType)
	{
		if (brawlType < BrawlType.BRAWL_TYPE_TAVERN_BRAWL || brawlType >= BrawlType.BRAWL_TYPE_COUNT)
		{
			return null;
		}
		return this.m_missions[(int)brawlType];
	}

	// Token: 0x06006849 RID: 26697 RVA: 0x0021FED2 File Offset: 0x0021E0D2
	public bool SelectHeroBeforeMission()
	{
		return this.SelectHeroBeforeMission(this.m_currentBrawlType);
	}

	// Token: 0x0600684A RID: 26698 RVA: 0x0021FEE0 File Offset: 0x0021E0E0
	public bool SelectHeroBeforeMission(BrawlType brawlType)
	{
		return this.GetMission(brawlType) != null && this.GetMission(brawlType).canSelectHeroForDeck && !this.GetMission(brawlType).canCreateDeck;
	}

	// Token: 0x0600684B RID: 26699 RVA: 0x0021FF0A File Offset: 0x0021E10A
	public static bool IsInTavernBrawlFriendlyChallenge()
	{
		return (SceneMgr.Get().IsInTavernBrawlMode() || SceneMgr.Get().GetMode() == SceneMgr.Mode.FRIENDLY) && FriendChallengeMgr.Get().IsChallengeTavernBrawl();
	}

	// Token: 0x1700063B RID: 1595
	// (get) Token: 0x0600684C RID: 26700 RVA: 0x0021FF31 File Offset: 0x0021E131
	public bool CanChallengeToCurrentTavernBrawl
	{
		get
		{
			return this.CanChallengeToTavernBrawl(this.m_currentBrawlType);
		}
	}

	// Token: 0x1700063C RID: 1596
	// (get) Token: 0x0600684D RID: 26701 RVA: 0x0021FF40 File Offset: 0x0021E140
	public bool HasUnlockedAnyTavernBrawl
	{
		get
		{
			for (int i = 1; i < 3; i++)
			{
				BrawlType brawlType = (BrawlType)i;
				if (this.HasUnlockedTavernBrawl(brawlType))
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x1700063D RID: 1597
	// (get) Token: 0x0600684E RID: 26702 RVA: 0x0021FF67 File Offset: 0x0021E167
	public bool HasUnlockedCurrentTavernBrawl
	{
		get
		{
			return this.HasUnlockedTavernBrawl(this.m_currentBrawlType);
		}
	}

	// Token: 0x1700063E RID: 1598
	// (get) Token: 0x0600684F RID: 26703 RVA: 0x0021FF75 File Offset: 0x0021E175
	public bool IsFirstTimeSeeingThisFeature
	{
		get
		{
			return this.m_isFirstTimeSeeingThisFeature && this.IsCurrentBrawlTypeActive;
		}
	}

	// Token: 0x1700063F RID: 1599
	// (get) Token: 0x06006850 RID: 26704 RVA: 0x0021FF87 File Offset: 0x0021E187
	public bool IsFirstTimeSeeingCurrentSeason
	{
		get
		{
			return this.IsCurrentBrawlTypeActive && this.m_isFirstTimeSeeingCurrentSeason;
		}
	}

	// Token: 0x17000640 RID: 1600
	// (get) Token: 0x06006851 RID: 26705 RVA: 0x0021FF9C File Offset: 0x0021E19C
	// (set) Token: 0x06006852 RID: 26706 RVA: 0x0021FFF8 File Offset: 0x0021E1F8
	public int LatestSeenTavernBrawlSeason
	{
		get
		{
			if (this.m_latestSeenSeasonThisSession[(int)this.m_currentBrawlType] != null)
			{
				return this.m_latestSeenSeasonThisSession[(int)this.m_currentBrawlType].Value;
			}
			Option option = Option.LATEST_SEEN_TAVERNBRAWL_SEASON;
			if (this.m_currentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
			{
				option = Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON;
			}
			return Options.Get().GetInt(option);
		}
		set
		{
			this.m_latestSeenSeasonThisSession[(int)this.m_currentBrawlType] = new int?(value);
			if (value > 100000)
			{
				return;
			}
			Option option = Option.LATEST_SEEN_TAVERNBRAWL_SEASON;
			if (this.m_currentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
			{
				option = Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON;
			}
			Options.Get().SetInt(option, value);
		}
	}

	// Token: 0x17000641 RID: 1601
	// (get) Token: 0x06006853 RID: 26707 RVA: 0x00220048 File Offset: 0x0021E248
	// (set) Token: 0x06006854 RID: 26708 RVA: 0x002200A4 File Offset: 0x0021E2A4
	public int LatestSeenTavernBrawlChalkboard
	{
		get
		{
			if (this.m_latestSeenChalkboardThisSession[(int)this.m_currentBrawlType] != null)
			{
				return this.m_latestSeenChalkboardThisSession[(int)this.m_currentBrawlType].Value;
			}
			Option option = Option.LATEST_SEEN_TAVERNBRAWL_SEASON_CHALKBOARD;
			if (this.m_currentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
			{
				option = Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON_CHALKBOARD;
			}
			return Options.Get().GetInt(option);
		}
		set
		{
			this.m_latestSeenChalkboardThisSession[(int)this.m_currentBrawlType] = new int?(value);
			if (value > 100000)
			{
				return;
			}
			Option option = Option.LATEST_SEEN_TAVERNBRAWL_SEASON_CHALKBOARD;
			if (this.m_currentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
			{
				option = Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON_CHALKBOARD;
			}
			Options.Get().SetInt(option, value);
		}
	}

	// Token: 0x17000642 RID: 1602
	// (get) Token: 0x06006855 RID: 26709 RVA: 0x002200F2 File Offset: 0x0021E2F2
	public bool HasSeenTavernBrawlScreen
	{
		get
		{
			return this.LatestSeenTavernBrawlChalkboard > 0;
		}
	}

	// Token: 0x17000643 RID: 1603
	// (get) Token: 0x06006856 RID: 26710 RVA: 0x002200FD File Offset: 0x0021E2FD
	public long CurrentTavernBrawlSeasonEndInSeconds
	{
		get
		{
			return this.TavernBrawlSeasonEndInSeconds(this.m_currentBrawlType);
		}
	}

	// Token: 0x17000644 RID: 1604
	// (get) Token: 0x06006857 RID: 26711 RVA: 0x0022010B File Offset: 0x0021E30B
	public long NextTavernBrawlSeasonStartInSeconds
	{
		get
		{
			return this.TavernBrawlSeasonStartInSeconds(this.m_currentBrawlType);
		}
	}

	// Token: 0x17000645 RID: 1605
	// (get) Token: 0x06006858 RID: 26712 RVA: 0x00220119 File Offset: 0x0021E319
	public float CurrentScheduledSecondsToRefresh
	{
		get
		{
			return this.ScheduledSecondsToRefresh(this.m_currentBrawlType);
		}
	}

	// Token: 0x17000646 RID: 1606
	// (get) Token: 0x06006859 RID: 26713 RVA: 0x00220128 File Offset: 0x0021E328
	public bool IsDeckLocked
	{
		get
		{
			CollectionDeck collectionDeck = this.CurrentDeck();
			return collectionDeck != null && collectionDeck.Locked;
		}
	}

	// Token: 0x17000647 RID: 1607
	// (get) Token: 0x0600685A RID: 26714 RVA: 0x00220147 File Offset: 0x0021E347
	public bool IsCurrentSeasonSessionBased
	{
		get
		{
			return this.IsSeasonSessionBased(this.m_currentBrawlType);
		}
	}

	// Token: 0x17000648 RID: 1608
	// (get) Token: 0x0600685B RID: 26715 RVA: 0x00220155 File Offset: 0x0021E355
	public TavernBrawlMode CurrentSeasonBrawlMode
	{
		get
		{
			return this.GetBrawlModeForBrawlType(this.m_currentBrawlType);
		}
	}

	// Token: 0x17000649 RID: 1609
	// (get) Token: 0x0600685C RID: 26716 RVA: 0x00220163 File Offset: 0x0021E363
	public long CurrentTavernBrawlSeasonNewSessionsClosedInSeconds
	{
		get
		{
			return this.TavernBrawlSeasonNewSessionsClosedInSeconds(this.CurrentBrawlType);
		}
	}

	// Token: 0x0600685D RID: 26717 RVA: 0x00220171 File Offset: 0x0021E371
	public TavernBrawlPlayerRecord GetRecord(BrawlType brawlType)
	{
		if (brawlType < BrawlType.BRAWL_TYPE_TAVERN_BRAWL || brawlType >= BrawlType.BRAWL_TYPE_COUNT)
		{
			return null;
		}
		return this.m_playerRecords[(int)brawlType];
	}

	// Token: 0x1700064A RID: 1610
	// (get) Token: 0x0600685E RID: 26718 RVA: 0x00220188 File Offset: 0x0021E388
	public bool IsCurrentTavernBrawlSeasonClosedToPlayer
	{
		get
		{
			return this.CurrentTavernBrawlSeasonNewSessionsClosedInSeconds < 0L && this.MyRecord != null && (!this.MyRecord.HasNumTicketsOwned || this.MyRecord.NumTicketsOwned <= 0) && this.PlayerStatus != TavernBrawlStatus.TB_STATUS_ACTIVE && this.PlayerStatus != TavernBrawlStatus.TB_STATUS_IN_REWARDS;
		}
	}

	// Token: 0x1700064B RID: 1611
	// (get) Token: 0x0600685F RID: 26719 RVA: 0x002201E0 File Offset: 0x0021E3E0
	public bool IsPlayerAtSessionMaxForCurrentTavernBrawl
	{
		get
		{
			bool isCurrentSeasonSessionBased = this.IsCurrentSeasonSessionBased;
			bool flag = this.NumSessionsAvailableForPurchase == 0;
			bool flag2 = this.NumSessionsAllowedThisSeason == 0;
			bool flag3 = this.PlayerStatus == TavernBrawlStatus.TB_STATUS_TICKET_REQUIRED;
			bool flag4 = this.NumTicketsOwned == 0;
			return isCurrentSeasonSessionBased && flag && !flag2 && flag3 && flag4;
		}
	}

	// Token: 0x1700064C RID: 1612
	// (get) Token: 0x06006860 RID: 26720 RVA: 0x0022022A File Offset: 0x0021E42A
	public TavernBrawlStatus PlayerStatus
	{
		get
		{
			if (this.MyRecord != null && this.MyRecord.HasSessionStatus)
			{
				return this.MyRecord.SessionStatus;
			}
			return TavernBrawlStatus.TB_STATUS_INVALID;
		}
	}

	// Token: 0x1700064D RID: 1613
	// (get) Token: 0x06006861 RID: 26721 RVA: 0x0022024E File Offset: 0x0021E44E
	public int NumTicketsOwned
	{
		get
		{
			if (this.MyRecord != null && this.MyRecord.HasNumTicketsOwned)
			{
				return this.MyRecord.NumTicketsOwned;
			}
			return 0;
		}
	}

	// Token: 0x1700064E RID: 1614
	// (get) Token: 0x06006862 RID: 26722 RVA: 0x00220272 File Offset: 0x0021E472
	public int NumSessionsAllowedThisSeason
	{
		get
		{
			if (this.CurrentMission() != null)
			{
				return this.CurrentMission().maxSessions;
			}
			return -1;
		}
	}

	// Token: 0x1700064F RID: 1615
	// (get) Token: 0x06006863 RID: 26723 RVA: 0x00220289 File Offset: 0x0021E489
	public int NumSessionsAvailableForPurchase
	{
		get
		{
			if (this.MyRecord != null && this.MyRecord.HasNumSessionsPurchasable)
			{
				return this.MyRecord.NumSessionsPurchasable;
			}
			return 0;
		}
	}

	// Token: 0x17000650 RID: 1616
	// (get) Token: 0x06006864 RID: 26724 RVA: 0x002202AD File Offset: 0x0021E4AD
	public TavernBrawlPlayerSession CurrentSession
	{
		get
		{
			if (this.MyRecord != null && this.MyRecord.HasSession)
			{
				return this.MyRecord.Session;
			}
			return null;
		}
	}

	// Token: 0x17000651 RID: 1617
	// (get) Token: 0x06006865 RID: 26725 RVA: 0x002202D1 File Offset: 0x0021E4D1
	public int WinStreak
	{
		get
		{
			if (this.MyRecord != null && this.MyRecord.HasWinStreak)
			{
				return this.MyRecord.WinStreak;
			}
			return 0;
		}
	}

	// Token: 0x17000652 RID: 1618
	// (get) Token: 0x06006866 RID: 26726 RVA: 0x002202F5 File Offset: 0x0021E4F5
	public int GamesWon
	{
		get
		{
			if (this.CurrentMission().IsSessionBased)
			{
				if (this.CurrentSession != null)
				{
					return this.CurrentSession.Wins;
				}
				return 0;
			}
			else
			{
				if (this.MyRecord != null)
				{
					return this.MyRecord.GamesWon;
				}
				return 0;
			}
		}
	}

	// Token: 0x17000653 RID: 1619
	// (get) Token: 0x06006867 RID: 26727 RVA: 0x0022032F File Offset: 0x0021E52F
	public int GamesLost
	{
		get
		{
			if (!this.CurrentMission().IsSessionBased)
			{
				return this.GamesPlayed - this.GamesWon;
			}
			if (this.CurrentSession != null)
			{
				return this.CurrentSession.Losses;
			}
			return 0;
		}
	}

	// Token: 0x17000654 RID: 1620
	// (get) Token: 0x06006868 RID: 26728 RVA: 0x00220361 File Offset: 0x0021E561
	public int GamesPlayed
	{
		get
		{
			if (this.MyRecord != null && this.MyRecord.HasGamesPlayed)
			{
				return this.MyRecord.GamesPlayed;
			}
			return 0;
		}
	}

	// Token: 0x17000655 RID: 1621
	// (get) Token: 0x06006869 RID: 26729 RVA: 0x00220385 File Offset: 0x0021E585
	public int RewardProgress
	{
		get
		{
			if (this.MyRecord != null)
			{
				return this.MyRecord.RewardProgress;
			}
			return 0;
		}
	}

	// Token: 0x17000656 RID: 1622
	// (get) Token: 0x0600686A RID: 26730 RVA: 0x0022039C File Offset: 0x0021E59C
	public string EndingTimeText
	{
		get
		{
			long num = (this.CurrentMission() == null) ? -1L : this.CurrentTavernBrawlSeasonEndInSeconds;
			if (num < 0L)
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
			return TimeUtils.GetElapsedTimeString((int)num, stringSet, true);
		}
	}

	// Token: 0x17000657 RID: 1623
	// (get) Token: 0x0600686B RID: 26731 RVA: 0x0022041B File Offset: 0x0021E61B
	public List<TavernBrawlMission> Missions
	{
		get
		{
			return (from m in this.m_missions
			where m != null
			select m).ToList<TavernBrawlMission>();
		}
	}

	// Token: 0x0600686C RID: 26732 RVA: 0x0022044C File Offset: 0x0021E64C
	public string GetStartingTimeText(bool singleLine = false)
	{
		long nextTavernBrawlSeasonStartInSeconds = this.NextTavernBrawlSeasonStartInSeconds;
		if (nextTavernBrawlSeasonStartInSeconds < 0L)
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
		string text = TimeUtils.GetElapsedTimeString(nextTavernBrawlSeasonStartInSeconds, stringSet, false);
		if (singleLine)
		{
			text = text.Replace("\n", " ").Replace("\r", "");
		}
		return text;
	}

	// Token: 0x0600686D RID: 26733 RVA: 0x00220500 File Offset: 0x0021E700
	public global::DeckRuleset GetCurrentDeckRuleset()
	{
		return this.GetDeckRuleset(this.m_currentBrawlType, 0);
	}

	// Token: 0x0600686E RID: 26734 RVA: 0x00220510 File Offset: 0x0021E710
	public global::DeckRuleset GetDeckRuleset(BrawlType brawlType, int brawlLibraryItemId = 0)
	{
		TavernBrawlMission mission = this.GetMission(brawlType);
		if (mission == null)
		{
			return null;
		}
		if (brawlLibraryItemId == 0)
		{
			brawlLibraryItemId = mission.SelectedBrawlLibraryItemId;
		}
		global::DeckRuleset deckRuleset = mission.GetDeckRuleset(brawlLibraryItemId);
		if (deckRuleset != null)
		{
			return deckRuleset;
		}
		return global::DeckRuleset.GetRuleset(mission.formatType);
	}

	// Token: 0x17000658 RID: 1624
	// (get) Token: 0x0600686F RID: 26735 RVA: 0x0022054D File Offset: 0x0021E74D
	public List<RewardData> CurrentSessionRewards
	{
		get
		{
			if (this.CurrentSession != null && this.CurrentSession.Chest != null)
			{
				return Network.ConvertRewardChest(this.CurrentSession.Chest).Rewards;
			}
			return new List<RewardData>();
		}
	}

	// Token: 0x06006870 RID: 26736 RVA: 0x00220580 File Offset: 0x0021E780
	public void StartGame(long deckId = 0L)
	{
		if (this.CurrentMission() == null)
		{
			Error.AddDevFatal("TB: m_currentMission is null", Array.Empty<object>());
			return;
		}
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.TAVERN_BRAWL_QUEUE
		});
		GameType gameType = this.CurrentMission().GameType;
		GameMgr.Get().FindGame(gameType, FormatType.FT_WILD, this.CurrentMission().missionId, 0, deckId, null, null, false, null, GameType.GT_UNKNOWN);
	}

	// Token: 0x06006871 RID: 26737 RVA: 0x002205F4 File Offset: 0x0021E7F4
	public void StartGameWithHero(int heroCardDbId)
	{
		TavernBrawlMission tavernBrawlMission = this.CurrentMission();
		if (tavernBrawlMission == null)
		{
			Error.AddDevFatal("TB: m_currentMission is null", Array.Empty<object>());
			return;
		}
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.TAVERN_BRAWL_QUEUE
		});
		GameMgr.Get().FindGameWithHero(tavernBrawlMission.GameType, FormatType.FT_WILD, tavernBrawlMission.missionId, tavernBrawlMission.SelectedBrawlLibraryItemId, heroCardDbId, 0L);
	}

	// Token: 0x06006872 RID: 26738 RVA: 0x00220658 File Offset: 0x0021E858
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
			if (GameMgr.Get().IsNextTavernBrawl() && GameMgr.Get().IsNextReconnect() && this.IsCurrentSeasonSessionBased)
			{
				SessionRecord sessionRecord = new SessionRecord();
				sessionRecord.Wins = (uint)this.GamesWon;
				sessionRecord.Losses = (uint)this.GamesLost;
				sessionRecord.RunFinished = false;
				sessionRecord.SessionRecordType = ((this.CurrentSeasonBrawlMode == TavernBrawlMode.TB_MODE_NORMAL) ? SessionRecordType.TAVERN_BRAWL : SessionRecordType.HEROIC_BRAWL);
				BnetPresenceMgr.Get().SetGameFieldBlob(22U, sessionRecord);
			}
			break;
		}
		return false;
	}

	// Token: 0x06006873 RID: 26739 RVA: 0x0022073C File Offset: 0x0021E93C
	private void ShowSessionLimitWarning()
	{
		int numSessionsAllowedThisSeason = TavernBrawlManager.Get().NumSessionsAllowedThisSeason;
		int numSessionsAvailableForPurchase = TavernBrawlManager.Get().NumSessionsAvailableForPurchase;
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
			text = GameStrings.Format("GLUE_HEROIC_BRAWL_SESSION_LIMIT_ALERT_DESCRIPTION_NORMAL", new object[]
			{
				numSessionsAvailableForPurchase,
				numSessionsAvailableForPurchase
			});
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
		popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_LIMIT_ALERT_TITLE");
		popupInfo.m_text = text;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06006874 RID: 26740 RVA: 0x002207DC File Offset: 0x0021E9DC
	public bool HasCreatedDeck()
	{
		return this.CurrentDeck() != null;
	}

	// Token: 0x06006875 RID: 26741 RVA: 0x002207E7 File Offset: 0x0021E9E7
	public CollectionDeck CurrentDeck()
	{
		return this.GetDeck(this.m_currentBrawlType, 0);
	}

	// Token: 0x06006876 RID: 26742 RVA: 0x002207F8 File Offset: 0x0021E9F8
	public CollectionDeck GetDeck(BrawlType brawlType, int brawlLibraryItemId = 0)
	{
		TavernBrawlMission mission = this.GetMission(brawlType);
		if (mission == null)
		{
			return null;
		}
		if (brawlLibraryItemId == 0)
		{
			brawlLibraryItemId = mission.SelectedBrawlLibraryItemId;
		}
		foreach (CollectionDeck collectionDeck in CollectionManager.Get().GetDecks().Values)
		{
			if (TavernBrawlManager.TranslateDeckTypeToBrawlType(collectionDeck.Type) == brawlType && mission.seasonId == collectionDeck.SeasonId && brawlLibraryItemId == collectionDeck.BrawlLibraryItemId)
			{
				return collectionDeck;
			}
		}
		return null;
	}

	// Token: 0x06006877 RID: 26743 RVA: 0x00220894 File Offset: 0x0021EA94
	public bool HasValidDeckForCurrent()
	{
		return this.HasValidDeck(this.m_currentBrawlType, 0);
	}

	// Token: 0x06006878 RID: 26744 RVA: 0x002208A4 File Offset: 0x0021EAA4
	public bool HasValidDeck(BrawlType brawlType, int brawlLibraryItemId = 0)
	{
		TavernBrawlMission mission = this.GetMission(brawlType);
		if (mission == null || !mission.CanCreateDeck(brawlLibraryItemId))
		{
			return false;
		}
		CollectionDeck deck = this.GetDeck(brawlType, brawlLibraryItemId);
		if (deck == null)
		{
			return false;
		}
		if (!deck.NetworkContentsLoaded())
		{
			CollectionManager.Get().RequestDeckContents(deck.ID);
			return false;
		}
		global::DeckRuleset deckRuleset = this.GetDeckRuleset(brawlType, brawlLibraryItemId);
		return deckRuleset == null || deckRuleset.IsDeckValid(deck);
	}

	// Token: 0x06006879 RID: 26745 RVA: 0x00220907 File Offset: 0x0021EB07
	public static bool IsBrawlDeckType(DeckType deckType)
	{
		return deckType - DeckType.TAVERN_BRAWL_DECK <= 1;
	}

	// Token: 0x17000659 RID: 1625
	// (get) Token: 0x0600687A RID: 26746 RVA: 0x00220914 File Offset: 0x0021EB14
	public DeckType DeckTypeForCurrentBrawlType
	{
		get
		{
			BrawlType currentBrawlType = this.m_currentBrawlType;
			if (currentBrawlType != BrawlType.BRAWL_TYPE_TAVERN_BRAWL && currentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
			{
				return DeckType.FSG_BRAWL_DECK;
			}
			return DeckType.TAVERN_BRAWL_DECK;
		}
	}

	// Token: 0x0600687B RID: 26747 RVA: 0x00220933 File Offset: 0x0021EB33
	private static BrawlType TranslateDeckTypeToBrawlType(DeckType deckType)
	{
		if (deckType == DeckType.TAVERN_BRAWL_DECK)
		{
			return BrawlType.BRAWL_TYPE_TAVERN_BRAWL;
		}
		if (deckType != DeckType.FSG_BRAWL_DECK)
		{
			return BrawlType.BRAWL_TYPE_UNKNOWN;
		}
		return BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING;
	}

	// Token: 0x0600687C RID: 26748 RVA: 0x00220944 File Offset: 0x0021EB44
	public bool IsTavernBrawlActiveByDeckType(DeckType deckType)
	{
		BrawlType brawlType = TavernBrawlManager.TranslateDeckTypeToBrawlType(deckType);
		return brawlType != BrawlType.BRAWL_TYPE_UNKNOWN && this.IsTavernBrawlActive(brawlType);
	}

	// Token: 0x0600687D RID: 26749 RVA: 0x00220964 File Offset: 0x0021EB64
	public bool IsSeasonActive(DeckType deckType, int seasonId, int brawlLibraryItemId)
	{
		BrawlType brawlType = TavernBrawlManager.TranslateDeckTypeToBrawlType(deckType);
		if (brawlType == BrawlType.BRAWL_TYPE_UNKNOWN)
		{
			return false;
		}
		if (!this.IsSeasonActive(brawlType, seasonId))
		{
			return false;
		}
		if (brawlLibraryItemId != 0)
		{
			TavernBrawlMission mission = this.GetMission(brawlType);
			if (mission == null || !mission.BrawlList.Any((GameContentScenario scen) => scen.LibraryItemId == brawlLibraryItemId))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600687E RID: 26750 RVA: 0x002209C8 File Offset: 0x0021EBC8
	public bool IsSeasonActive(BrawlType brawlType, int seasonId)
	{
		if (!this.IsTavernBrawlActive(brawlType))
		{
			return false;
		}
		TavernBrawlMission tavernBrawlMission = this.m_missions[(int)brawlType];
		return tavernBrawlMission != null && tavernBrawlMission.seasonId == seasonId && (tavernBrawlMission.BrawlType != BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING || FiresideGatheringManager.Get().IsCheckedIn);
	}

	// Token: 0x0600687F RID: 26751 RVA: 0x00220A10 File Offset: 0x0021EC10
	public bool IsScenarioActiveInAnyBrawl(int scenarioId)
	{
		for (int i = 1; i < this.m_missions.Length; i++)
		{
			TavernBrawlMission tavernBrawlMission = this.m_missions[i];
			if (tavernBrawlMission != null && this.IsTavernBrawlActive(tavernBrawlMission.BrawlType) && tavernBrawlMission.missionId == scenarioId && (tavernBrawlMission.BrawlType != BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING || FiresideGatheringManager.Get().IsCheckedIn))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006880 RID: 26752 RVA: 0x00220A6B File Offset: 0x0021EC6B
	public void EnsureAllDataReady(TavernBrawlManager.CallbackEnsureServerDataReady callback = null)
	{
		this.EnsureAllDataReady(this.m_currentBrawlType, callback);
	}

	// Token: 0x06006881 RID: 26753 RVA: 0x00220A7C File Offset: 0x0021EC7C
	public void EnsureAllDataReady(BrawlType brawlType, TavernBrawlManager.CallbackEnsureServerDataReady callback = null)
	{
		TavernBrawlMission mission = this.GetMission(brawlType);
		if (mission == null)
		{
			return;
		}
		if (this.IsAllDataReady(brawlType))
		{
			if (callback != null)
			{
				callback();
			}
			return;
		}
		if (callback != null)
		{
			if (this.m_serverDataReadyCallbacks == null)
			{
				this.m_serverDataReadyCallbacks = new List<TavernBrawlManager.CallbackEnsureServerDataReady>();
			}
			this.m_serverDataReadyCallbacks.Add(callback);
		}
		TavernBrawlSeasonSpec tavernBrawlSpec = mission.tavernBrawlSpec;
		List<AssetRecordInfo> list = new List<AssetRecordInfo>();
		foreach (GameContentScenario gameContentScenario in mission.BrawlList)
		{
			list.Add(new AssetRecordInfo
			{
				Asset = new AssetKey(),
				Asset = 
				{
					Type = AssetType.ASSET_TYPE_SCENARIO,
					AssetId = gameContentScenario.ScenarioId
				},
				RecordByteSize = gameContentScenario.ScenarioRecordByteSize,
				RecordHash = gameContentScenario.ScenarioRecordHash
			});
			if (gameContentScenario.AdditionalAssets != null && gameContentScenario.AdditionalAssets.Count > 0)
			{
				list.AddRange(gameContentScenario.AdditionalAssets);
			}
		}
		if (DownloadableDbfCache.Get().IsAssetRequestInProgress(mission.missionId, AssetType.ASSET_TYPE_SCENARIO))
		{
			DownloadableDbfCache.Get().LoadCachedAssets(false, delegate(AssetKey requestedKey, ErrorCode code, byte[] assetBytes)
			{
				this.OnDownloadableDbfAssetsLoaded(requestedKey, code, assetBytes, brawlType);
			}, list.ToArray());
			return;
		}
		if (HearthstoneApplication.IsInternal())
		{
			DownloadableDbfCache.LoadCachedAssetCallback <>9__3;
			Processor.ScheduleCallback(Mathf.Max(0f, UnityEngine.Random.Range(-3f, 3f)), false, delegate(object userData)
			{
				TavernBrawlManager tavernBrawlManager = TavernBrawlManager.Get();
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
					return;
				}
				GameContentSeasonSpec gameContentSeason = mission.tavernBrawlSpec.GameContentSeason;
				List<AssetRecordInfo> list2 = new List<AssetRecordInfo>();
				foreach (GameContentScenario gameContentScenario2 in gameContentSeason.Scenarios)
				{
					list2.Add(new AssetRecordInfo
					{
						Asset = new AssetKey(),
						Asset = 
						{
							Type = AssetType.ASSET_TYPE_SCENARIO,
							AssetId = gameContentScenario2.ScenarioId
						},
						RecordByteSize = gameContentScenario2.ScenarioRecordByteSize,
						RecordHash = gameContentScenario2.ScenarioRecordHash
					});
					if (gameContentScenario2.AdditionalAssets != null && gameContentScenario2.AdditionalAssets.Count > 0)
					{
						list2.AddRange(gameContentScenario2.AdditionalAssets);
					}
				}
				DownloadableDbfCache downloadableDbfCache = DownloadableDbfCache.Get();
				bool canRequestFromServer = true;
				DownloadableDbfCache.LoadCachedAssetCallback cb;
				if ((cb = <>9__3) == null)
				{
					cb = (<>9__3 = delegate(AssetKey requestedKey, ErrorCode code, byte[] assetBytes)
					{
						this.OnDownloadableDbfAssetsLoaded(requestedKey, code, assetBytes, brawlType);
					});
				}
				downloadableDbfCache.LoadCachedAssets(canRequestFromServer, cb, list2.ToArray());
			}, null);
			return;
		}
		DownloadableDbfCache.Get().LoadCachedAssets(true, delegate(AssetKey requestedKey, ErrorCode code, byte[] assetBytes)
		{
			this.OnDownloadableDbfAssetsLoaded(requestedKey, code, assetBytes, brawlType);
		}, list.ToArray());
	}

	// Token: 0x1700065A RID: 1626
	// (get) Token: 0x06006882 RID: 26754 RVA: 0x00220C60 File Offset: 0x0021EE60
	public bool IsCurrentBrawlInfoReady
	{
		get
		{
			bool netObject = NetCache.Get().GetNetObject<NetCache.NetCacheClientOptions>() != null;
			NetCache.NetCacheHeroLevels netObject2 = NetCache.Get().GetNetObject<NetCache.NetCacheHeroLevels>();
			return netObject && this.CurrentMission() != null && netObject2 != null;
		}
	}

	// Token: 0x1700065B RID: 1627
	// (get) Token: 0x06006883 RID: 26755 RVA: 0x00220C96 File Offset: 0x0021EE96
	public bool IsCurrentBrawlAllDataReady
	{
		get
		{
			return this.IsAllDataReady(this.m_currentBrawlType);
		}
	}

	// Token: 0x06006884 RID: 26756 RVA: 0x00220CA4 File Offset: 0x0021EEA4
	private bool IsAllDataReady(BrawlType brawlType)
	{
		if (brawlType < BrawlType.BRAWL_TYPE_TAVERN_BRAWL || brawlType >= BrawlType.BRAWL_TYPE_COUNT)
		{
			return true;
		}
		TavernBrawlMission mission = this.GetMission(brawlType);
		if (mission == null)
		{
			return true;
		}
		if (this.m_downloadableDbfAssetsPendingLoad[(int)brawlType])
		{
			return false;
		}
		return !mission.BrawlList.Any((GameContentScenario brawl) => GameDbf.Scenario.GetRecord(brawl.ScenarioId) == null);
	}

	// Token: 0x06006885 RID: 26757 RVA: 0x00220D03 File Offset: 0x0021EF03
	public void RefreshServerData(BrawlType brawlType = BrawlType.BRAWL_TYPE_UNKNOWN)
	{
		brawlType = ((brawlType == BrawlType.BRAWL_TYPE_UNKNOWN) ? this.m_currentBrawlType : brawlType);
		Network.Get().RequestTavernBrawlInfo(brawlType);
	}

	// Token: 0x06006886 RID: 26758 RVA: 0x00220D20 File Offset: 0x0021EF20
	public bool HasUnlockedTavernBrawl(BrawlType brawlType)
	{
		NetCache.NetCacheHeroLevels netObject = NetCache.Get().GetNetObject<NetCache.NetCacheHeroLevels>();
		if (brawlType == BrawlType.BRAWL_TYPE_TAVERN_BRAWL)
		{
			if (netObject == null)
			{
				return false;
			}
			return netObject.Levels.Any((NetCache.HeroLevel l) => l.CurrentLevel.Level >= 20);
		}
		else
		{
			if (brawlType != BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
			{
				return true;
			}
			if (netObject == null)
			{
				return false;
			}
			return netObject.Levels.Any((NetCache.HeroLevel l) => l.CurrentLevel.Level >= 1);
		}
	}

	// Token: 0x06006887 RID: 26759 RVA: 0x00220DA4 File Offset: 0x0021EFA4
	public bool CanChallengeToTavernBrawl(BrawlType brawlType)
	{
		if (!this.IsTavernBrawlActive(brawlType))
		{
			return false;
		}
		TavernBrawlMission mission = this.GetMission(brawlType);
		return !GameUtils.IsAIMission(mission.missionId) && !mission.friendlyChallengeDisabled;
	}

	// Token: 0x06006888 RID: 26760 RVA: 0x00220DE0 File Offset: 0x0021EFE0
	public bool IsEligibleForFreeTicket()
	{
		if (this.CurrentSession == null || this.CurrentMission() == null)
		{
			return false;
		}
		uint sessionCount = this.CurrentSession.SessionCount;
		uint freeSessions = this.CurrentMission().FreeSessions;
		return freeSessions > 0U && sessionCount < freeSessions;
	}

	// Token: 0x06006889 RID: 26761 RVA: 0x00220E24 File Offset: 0x0021F024
	public bool IsTavernBrawlActive(BrawlType brawlType)
	{
		TavernBrawlMission tavernBrawlMission = this.m_missions[(int)brawlType];
		return (brawlType != BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING || FiresideGatheringManager.Get().IsCheckedIn) && tavernBrawlMission != null && this.TavernBrawlSeasonEndInSeconds(brawlType) > 0L;
	}

	// Token: 0x0600688A RID: 26762 RVA: 0x00220E5C File Offset: 0x0021F05C
	public void RefreshPlayerRecord()
	{
		Network.Get().RequestTavernBrawlPlayerRecord(this.m_currentBrawlType);
	}

	// Token: 0x0600688B RID: 26763 RVA: 0x00220E70 File Offset: 0x0021F070
	public long TavernBrawlSeasonStartInSeconds(BrawlType brawlType)
	{
		DateTime? dateTime = this.m_nextSeasonStartDates[(int)brawlType];
		if (dateTime == null || dateTime == null)
		{
			return -1L;
		}
		return (long)(dateTime.Value - DateTime.Now).TotalSeconds;
	}

	// Token: 0x0600688C RID: 26764 RVA: 0x00220EBC File Offset: 0x0021F0BC
	public float ScheduledSecondsToRefresh(BrawlType brawlType)
	{
		DateTime? dateTime = this.m_scheduledRefreshTimes[(int)brawlType];
		if (dateTime == null || dateTime == null)
		{
			return -1f;
		}
		return (float)(dateTime.Value - DateTime.Now).TotalSeconds;
	}

	// Token: 0x0600688D RID: 26765 RVA: 0x00220F08 File Offset: 0x0021F108
	public long TavernBrawlSeasonNewSessionsClosedInSeconds(BrawlType brawlType)
	{
		TavernBrawlMission mission = this.GetMission(brawlType);
		if (mission == null || mission.closedToNewSessionsDateLocal == null)
		{
			return 2147483647L;
		}
		return (long)(mission.closedToNewSessionsDateLocal.Value - DateTime.Now).TotalSeconds;
	}

	// Token: 0x0600688E RID: 26766 RVA: 0x00220F58 File Offset: 0x0021F158
	public bool IsSeasonSessionBased(BrawlType brawlType)
	{
		TavernBrawlMission mission = this.GetMission(brawlType);
		return mission != null && mission.IsSessionBased;
	}

	// Token: 0x0600688F RID: 26767 RVA: 0x00220F78 File Offset: 0x0021F178
	public TavernBrawlMode GetBrawlModeForBrawlType(BrawlType brawlType)
	{
		TavernBrawlMission mission = this.GetMission(brawlType);
		if (mission != null)
		{
			return mission.brawlMode;
		}
		return TavernBrawlMode.TB_MODE_NORMAL;
	}

	// Token: 0x06006890 RID: 26768 RVA: 0x00220F98 File Offset: 0x0021F198
	public void RequestSessionBegin()
	{
		Network.Get().RequestTavernBrawlSessionBegin();
	}

	// Token: 0x1700065C RID: 1628
	// (get) Token: 0x06006891 RID: 26769 RVA: 0x00220FA4 File Offset: 0x0021F1A4
	private TavernBrawlPlayerRecord MyRecord
	{
		get
		{
			return this.m_playerRecords[(int)this.m_currentBrawlType];
		}
	}

	// Token: 0x06006892 RID: 26770 RVA: 0x00220FB4 File Offset: 0x0021F1B4
	private void RegisterOptionsListeners(bool register)
	{
		if (register)
		{
			NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheClientOptions), new Action(this.NetCache_OnClientOptions));
			Options.Get().RegisterChangedListener(Option.LATEST_SEEN_TAVERNBRAWL_SEASON, new Options.ChangedCallback(this.OnOptionChangedCallback));
			Options.Get().RegisterChangedListener(Option.LATEST_SEEN_TAVERNBRAWL_SEASON_CHALKBOARD, new Options.ChangedCallback(this.OnOptionChangedCallback));
			Options.Get().RegisterChangedListener(Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON, new Options.ChangedCallback(this.OnOptionChangedCallback));
			Options.Get().RegisterChangedListener(Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON_CHALKBOARD, new Options.ChangedCallback(this.OnOptionChangedCallback));
			return;
		}
		NetCache.Get().RemoveUpdatedListener(typeof(NetCache.NetCacheClientOptions), new Action(this.NetCache_OnClientOptions));
		Options.Get().UnregisterChangedListener(Option.LATEST_SEEN_TAVERNBRAWL_SEASON, new Options.ChangedCallback(this.OnOptionChangedCallback));
		Options.Get().UnregisterChangedListener(Option.LATEST_SEEN_TAVERNBRAWL_SEASON_CHALKBOARD, new Options.ChangedCallback(this.OnOptionChangedCallback));
		Options.Get().UnregisterChangedListener(Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON, new Options.ChangedCallback(this.OnOptionChangedCallback));
		Options.Get().UnregisterChangedListener(Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON_CHALKBOARD, new Options.ChangedCallback(this.OnOptionChangedCallback));
	}

	// Token: 0x06006893 RID: 26771 RVA: 0x002210E8 File Offset: 0x0021F2E8
	private void NetCache_OnClientOptions()
	{
		this.RegisterOptionsListeners(false);
		bool seasonHasChanged = this.CheckLatestSeenSeason(true);
		this.CheckLatestSessionLimit(seasonHasChanged);
		this.RegisterOptionsListeners(true);
	}

	// Token: 0x06006894 RID: 26772 RVA: 0x00221112 File Offset: 0x0021F312
	private void NetCache_OnTavernBrawlRecord()
	{
		if (this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}

	// Token: 0x06006895 RID: 26773 RVA: 0x00221128 File Offset: 0x0021F328
	private void OnOptionChangedCallback(Option option, object prevValue, bool existed, object userData)
	{
		this.RegisterOptionsListeners(false);
		bool seasonHasChanged = this.CheckLatestSeenSeason(false);
		this.CheckLatestSessionLimit(seasonHasChanged);
		this.RegisterOptionsListeners(true);
	}

	// Token: 0x06006896 RID: 26774 RVA: 0x00221154 File Offset: 0x0021F354
	private bool CheckLatestSeenSeason(bool canSetOption)
	{
		bool result = false;
		if (!this.IsCurrentBrawlInfoReady)
		{
			return result;
		}
		bool flag = !this.m_hasGottenClientOptionsAtLeastOnce;
		this.m_hasGottenClientOptionsAtLeastOnce = true;
		bool isFirstTimeSeeingThisFeature = this.IsFirstTimeSeeingThisFeature;
		bool flag2 = this.CurrentMission() != null && this.LatestSeenTavernBrawlSeason < this.CurrentMission().seasonId;
		this.m_isFirstTimeSeeingThisFeature = false;
		this.m_isFirstTimeSeeingCurrentSeason = false;
		TavernBrawlMission tavernBrawlMission = this.CurrentMission();
		if (tavernBrawlMission != null)
		{
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			bool flag3 = netObject != null && netObject.Games.TavernBrawl && this.HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
			int latestSeenTavernBrawlSeason = this.LatestSeenTavernBrawlSeason;
			if (latestSeenTavernBrawlSeason == 0 && flag3 && tavernBrawlMission.BrawlType != BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
			{
				this.m_isFirstTimeSeeingThisFeature = true;
				NotificationManager.Get().ForceRemoveSoundFromPlayedList("VO_INNKEEPER_TAVERNBRAWL_PUSH_32.prefab:4f57cd2af5fe5194fbc46c91171ab135");
				NotificationManager.Get().ForceRemoveSoundFromPlayedList("VO_INNKEEPER_TAVERNBRAWL_WELCOME1_27.prefab:094070b7fecad8548b0b8fdb02bde052");
			}
			if (latestSeenTavernBrawlSeason < tavernBrawlMission.seasonId && flag3)
			{
				this.m_isFirstTimeSeeingCurrentSeason = true;
				NotificationManager.Get().ForceRemoveSoundFromPlayedList("VO_INNKEEPER_TAVERNBRAWL_DESC2_30.prefab:498657df8d08bc1468bfd1ad9f74ccac");
				Hub.s_hasAlreadyShownTBAnimation = false;
				if (canSetOption)
				{
					this.LatestSeenTavernBrawlSeason = tavernBrawlMission.seasonId;
				}
				result = true;
			}
		}
		if ((flag || isFirstTimeSeeingThisFeature != this.IsFirstTimeSeeingThisFeature || flag2 != this.IsFirstTimeSeeingCurrentSeason) && this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
		return result;
	}

	// Token: 0x06006897 RID: 26775 RVA: 0x00221288 File Offset: 0x0021F488
	private void CheckLatestSessionLimit(bool seasonHasChanged)
	{
		if (!this.IsCurrentBrawlInfoReady)
		{
			return;
		}
		TavernBrawlMission tavernBrawlMission = this.CurrentMission();
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

	// Token: 0x06006898 RID: 26776 RVA: 0x00221330 File Offset: 0x0021F530
	private void ScheduleTimedCallbacksForBrawl(TavernBrawlInfo serverInfo)
	{
		if (serverInfo.HasNextStartSecondsFromNow)
		{
			this.m_nextSeasonStartDates[(int)serverInfo.BrawlType] = new DateTime?(DateTime.Now + new TimeSpan(0, 0, (int)serverInfo.NextStartSecondsFromNow));
		}
		else
		{
			this.m_nextSeasonStartDates[(int)serverInfo.BrawlType] = null;
		}
		Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.ScheduledEndOfCurrentTBCallback), serverInfo.BrawlType);
		long num = this.TavernBrawlSeasonEndInSeconds(serverInfo.BrawlType);
		if (this.IsTavernBrawlActive(serverInfo.BrawlType) && num > 0L)
		{
			Log.EventTiming.Print("Scheduling end of current {0} {1} secs from now.", new object[]
			{
				serverInfo.BrawlType,
				num
			});
			Processor.ScheduleCallback((float)num, true, new Processor.ScheduledCallback(this.ScheduledEndOfCurrentTBCallback), serverInfo.BrawlType);
		}
		Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.ScheduledRefreshTBSpecCallback), serverInfo.BrawlType);
		long num2 = this.TavernBrawlSeasonStartInSeconds(serverInfo.BrawlType);
		if (num2 >= 0L)
		{
			this.m_scheduledRefreshTimes[(int)serverInfo.BrawlType] = new DateTime?(DateTime.Now + new TimeSpan(0, 0, 0, (int)num2, 0));
			Log.EventTiming.Print("Scheduling {0} refresh for {1} secs from now.", new object[]
			{
				serverInfo.BrawlType,
				num2
			});
			Processor.ScheduleCallback((float)num2, true, new Processor.ScheduledCallback(this.ScheduledRefreshTBSpecCallback), serverInfo.BrawlType);
		}
		long num3 = this.TavernBrawlSeasonNewSessionsClosedInSeconds(serverInfo.BrawlType);
		if (this.IsSeasonSessionBased(serverInfo.BrawlType) && num3 > 0L)
		{
			Log.EventTiming.Print("Scheduling {0} Closed Update for {1} secs from now.", new object[]
			{
				serverInfo.BrawlType,
				num3
			});
			Processor.ScheduleCallback((float)num3, true, new Processor.ScheduledCallback(this.ScheduleTBClosedUpdateCallback), serverInfo.BrawlType);
		}
	}

	// Token: 0x06006899 RID: 26777 RVA: 0x00221528 File Offset: 0x0021F728
	private void OnCheckInToFSGResponse()
	{
		CheckInToFSGResponse checkInToFSGResponse = Network.Get().GetCheckInToFSGResponse();
		if (checkInToFSGResponse.ErrorCode != ErrorCode.ERROR_OK)
		{
			return;
		}
		if (checkInToFSGResponse.HasPlayerRecord)
		{
			this.m_playerRecords[2] = checkInToFSGResponse.PlayerRecord;
		}
		if (this.m_currentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING && this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}

	// Token: 0x0600689A RID: 26778 RVA: 0x0022157B File Offset: 0x0021F77B
	private void OnFiresideGatheringLeave()
	{
		if (this.m_currentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING && this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}

	// Token: 0x0600689B RID: 26779 RVA: 0x0022159C File Offset: 0x0021F79C
	private void ScheduledEndOfCurrentTBCallback(object userData)
	{
		Log.EventTiming.Print("ScheduledEndOfCurrentTBCallback: ending current TB now.", Array.Empty<object>());
		bool flag = TavernBrawlDisplay.Get() != null && TavernBrawlDisplay.Get().IsInRewards();
		BrawlType brawlType = (BrawlType)userData;
		TavernBrawlMission tavernBrawlMission = this.m_missions[(int)this.m_currentBrawlType];
		TavernBrawlPlayerRecord tavernBrawlPlayerRecord = this.m_playerRecords[(int)this.m_currentBrawlType];
		if (tavernBrawlMission != null && tavernBrawlMission.IsSessionBased && (tavernBrawlPlayerRecord.SessionStatus == TavernBrawlStatus.TB_STATUS_ACTIVE || tavernBrawlPlayerRecord.SessionStatus == TavernBrawlStatus.TB_STATUS_IN_REWARDS) && (brawlType != this.m_currentBrawlType || !flag))
		{
			int num = 2;
			if (tavernBrawlMission.SeasonEndSecondsSpreadCount > 0)
			{
				num += tavernBrawlMission.SeasonEndSecondsSpreadCount;
			}
			else
			{
				num += UnityEngine.Random.Range(0, 30);
			}
			Processor.ScheduleCallback((float)num, true, new Processor.ScheduledCallback(this.ScheduledEndOfCurrentTBCallback_AfterSpreadWhenRewardsExpected), brawlType);
		}
		if (brawlType == this.m_currentBrawlType)
		{
			this.m_missions[(int)brawlType] = null;
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

	// Token: 0x0600689C RID: 26780 RVA: 0x002216A4 File Offset: 0x0021F8A4
	private void ScheduledEndOfCurrentTBCallback_AfterSpreadWhenRewardsExpected(object userData)
	{
		BrawlType brawlType = (BrawlType)userData;
		Network.Get().RequestTavernBrawlPlayerRecord(brawlType);
	}

	// Token: 0x0600689D RID: 26781 RVA: 0x002216C4 File Offset: 0x0021F8C4
	private void ScheduledRefreshTBSpecCallback(object userData)
	{
		BrawlType brawlType = (BrawlType)userData;
		Log.EventTiming.Print("ScheduledRefreshTBSpecCallback: refreshing now.", Array.Empty<object>());
		this.RefreshServerData(brawlType);
	}

	// Token: 0x0600689E RID: 26782 RVA: 0x002216F3 File Offset: 0x0021F8F3
	private void ScheduleTBClosedUpdateCallback(object userData)
	{
		BrawlType brawlType = (BrawlType)userData;
		Log.EventTiming.Print("ScheduledUpdateTBCallback: updating now.", Array.Empty<object>());
		if (brawlType == this.m_currentBrawlType && this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}

	// Token: 0x0600689F RID: 26783 RVA: 0x0022172C File Offset: 0x0021F92C
	private void OnDownloadableDbfAssetsLoaded(AssetKey requestedKey, ErrorCode code, byte[] assetBytes, BrawlType brawlType)
	{
		if (requestedKey == null || requestedKey.Type != AssetType.ASSET_TYPE_SCENARIO)
		{
			Log.TavernBrawl.Print("OnDownloadableDbfAssetsLoaded bad AssetType assetId={0} assetType={1} {2}", new object[]
			{
				(requestedKey == null) ? 0 : requestedKey.AssetId,
				(int)((requestedKey == null) ? ((AssetType)0) : requestedKey.Type),
				(requestedKey == null) ? "(null)" : requestedKey.Type.ToString()
			});
			return;
		}
		if (assetBytes == null || assetBytes.Length == 0)
		{
			Log.TavernBrawl.PrintError("OnDownloadableDbfAssetsLoaded failed to load Asset: assetId={0} assetType={1} {2} error={3}", new object[]
			{
				(requestedKey == null) ? 0 : requestedKey.AssetId,
				(int)((requestedKey == null) ? ((AssetType)0) : requestedKey.Type),
				(requestedKey == null) ? "(null)" : requestedKey.Type.ToString(),
				code
			});
			return;
		}
		TavernBrawlMission tavernBrawlMission = this.m_missions[(int)brawlType];
		if (tavernBrawlMission == null)
		{
			return;
		}
		ScenarioDbRecord scenarioDbRecord = ProtobufUtil.ParseFrom<ScenarioDbRecord>(assetBytes, 0, assetBytes.Length);
		if (tavernBrawlMission.BrawlList.Count == 0 || tavernBrawlMission.BrawlList.First<GameContentScenario>().ScenarioId != scenarioDbRecord.Id)
		{
			return;
		}
		this.m_downloadableDbfAssetsPendingLoad[(int)brawlType] = false;
		if (this.m_currentBrawlType == brawlType)
		{
			Processor.RunCoroutine(this.OnDownloadableDbfAssetsLoaded_EnsureCurrentBrawlDeckContentsLoaded(), null);
		}
	}

	// Token: 0x060068A0 RID: 26784 RVA: 0x00221873 File Offset: 0x0021FA73
	private IEnumerator OnDownloadableDbfAssetsLoaded_EnsureCurrentBrawlDeckContentsLoaded()
	{
		foreach (CollectionDeck collectionDeck in CollectionManager.Get().GetDecks().Values)
		{
			if (TavernBrawlManager.TranslateDeckTypeToBrawlType(collectionDeck.Type) == this.m_currentBrawlType && !collectionDeck.NetworkContentsLoaded())
			{
				CollectionManager.Get().RequestDeckContents(collectionDeck.ID);
			}
		}
		if (this.CurrentMission() != null && !this.CurrentBrawlDeckContentsLoaded)
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
				else if (!this.IsCurrentBrawlAllDataReady)
				{
					done = true;
				}
				else if (this.CurrentMission() == null)
				{
					done = true;
				}
				else if (this.CurrentBrawlDeckContentsLoaded)
				{
					done = true;
				}
			}
		}
		if (!this.IsCurrentBrawlAllDataReady)
		{
			yield break;
		}
		if (this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
		if (this.m_serverDataReadyCallbacks != null)
		{
			TavernBrawlManager.CallbackEnsureServerDataReady[] array = this.m_serverDataReadyCallbacks.ToArray();
			this.m_serverDataReadyCallbacks.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				array[i]();
			}
		}
		yield break;
	}

	// Token: 0x1700065D RID: 1629
	// (get) Token: 0x060068A1 RID: 26785 RVA: 0x00221884 File Offset: 0x0021FA84
	private bool CurrentBrawlDeckContentsLoaded
	{
		get
		{
			TavernBrawlMission tavernBrawlMission = this.CurrentMission();
			if (tavernBrawlMission == null)
			{
				return true;
			}
			BrawlType currentBrawlType = this.m_currentBrawlType;
			int seasonId = tavernBrawlMission.seasonId;
			foreach (CollectionDeck collectionDeck in CollectionManager.Get().GetDecks().Values)
			{
				if (TavernBrawlManager.TranslateDeckTypeToBrawlType(collectionDeck.Type) == currentBrawlType && collectionDeck.SeasonId == seasonId && !collectionDeck.NetworkContentsLoaded())
				{
					return false;
				}
			}
			return true;
		}
	}

	// Token: 0x060068A2 RID: 26786 RVA: 0x00221920 File Offset: 0x0021FB20
	private long TavernBrawlSeasonEndInSeconds(BrawlType brawlType)
	{
		TavernBrawlMission tavernBrawlMission = this.m_missions[(int)brawlType];
		if (tavernBrawlMission == null)
		{
			return -1L;
		}
		if (tavernBrawlMission.endDateLocal == null)
		{
			return 2147483647L;
		}
		return (long)(tavernBrawlMission.endDateLocal.Value - DateTime.Now).TotalSeconds;
	}

	// Token: 0x060068A3 RID: 26787 RVA: 0x00221974 File Offset: 0x0021FB74
	private void OnTavernBrawlRecord_Internal(TavernBrawlPlayerRecord record)
	{
		if (record == null)
		{
			return;
		}
		this.m_playerRecords[(int)record.BrawlType] = record;
		if (this.m_currentBrawlType == record.BrawlType && this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}

	// Token: 0x060068A4 RID: 26788 RVA: 0x002219AC File Offset: 0x0021FBAC
	private void OnTavernBrawlInfo_Internal(TavernBrawlInfo serverInfo)
	{
		if (serverInfo == null)
		{
			return;
		}
		int brawlType = (int)serverInfo.BrawlType;
		if (brawlType < 0 || brawlType >= this.m_missions.Length)
		{
			Log.TavernBrawl.PrintError("OnTavernBrawlInfo_Internal: received invalid index for BrawlType={0} arrayLength={1}", new object[]
			{
				brawlType,
				this.m_missions.Length
			});
			return;
		}
		if (!serverInfo.HasCurrentTavernBrawl)
		{
			this.m_missions[brawlType] = null;
		}
		else
		{
			if (this.m_missions[brawlType] == null)
			{
				this.m_missions[brawlType] = new TavernBrawlMission();
			}
			this.m_missions[brawlType].SetSeasonSpec(serverInfo.CurrentTavernBrawl, serverInfo.BrawlType);
			this.m_downloadableDbfAssetsPendingLoad[brawlType] = true;
			if (this.OnTavernBrawlUpdated != null)
			{
				this.EnsureAllDataReady(serverInfo.BrawlType, null);
			}
		}
		bool seasonHasChanged = this.CheckLatestSeenSeason(true);
		this.CheckLatestSessionLimit(seasonHasChanged);
		this.ScheduleTimedCallbacksForBrawl(serverInfo);
		if (serverInfo.HasMyRecord)
		{
			this.OnTavernBrawlRecord_Internal(serverInfo.MyRecord);
		}
		if (this.m_currentBrawlType == serverInfo.BrawlType && this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}

	// Token: 0x060068A5 RID: 26789 RVA: 0x00221AB0 File Offset: 0x0021FCB0
	private void OnBeginSession()
	{
		Log.TavernBrawl.Print(string.Format("TavernBrawlManager.OnBeginSession", Array.Empty<object>()), Array.Empty<object>());
		TavernBrawlRequestSessionBeginResponse tavernBrawlSessionBegin = Network.Get().GetTavernBrawlSessionBegin();
		if (!tavernBrawlSessionBegin.HasErrorCode || tavernBrawlSessionBegin.ErrorCode == ErrorCode.ERROR_OK)
		{
			SessionRecord sessionRecord = new SessionRecord();
			sessionRecord.Wins = 0U;
			sessionRecord.Losses = 0U;
			sessionRecord.RunFinished = false;
			sessionRecord.SessionRecordType = SessionRecordType.TAVERN_BRAWL;
			BnetPresenceMgr.Get().SetGameFieldBlob(22U, sessionRecord);
			if (tavernBrawlSessionBegin.HasPlayerRecord)
			{
				this.OnTavernBrawlRecord_Internal(tavernBrawlSessionBegin.PlayerRecord);
			}
			this.ShowSessionLimitWarning();
			if (this.OnTavernBrawlUpdated != null)
			{
				this.OnTavernBrawlUpdated();
			}
			return;
		}
		string text = tavernBrawlSessionBegin.ErrorCode.ToString();
		Debug.LogWarning(string.Concat(new object[]
		{
			"TavernBrawlManager.OnBeginSession: Got Error ",
			tavernBrawlSessionBegin.ErrorCode,
			" : ",
			text
		}));
		if (!SceneMgr.Get().IsSceneLoaded())
		{
			return;
		}
		if ((SceneMgr.Get().IsModeRequested(SceneMgr.Mode.TAVERN_BRAWL) || SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FIRESIDE_GATHERING)) && TavernBrawlManager.Get().PlayerStatus == TavernBrawlStatus.TB_STATUS_ACTIVE)
		{
			return;
		}
		if (TavernBrawlStore.Get() != null)
		{
			TavernBrawlStore.Get().Hide();
		}
		if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		if (this.CurrentMission().brawlMode == TavernBrawlMode.TB_MODE_HEROIC)
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

	// Token: 0x060068A6 RID: 26790 RVA: 0x00221C74 File Offset: 0x0021FE74
	private void OnRetireSession()
	{
		Log.TavernBrawl.Print(string.Format("TavernBrawlManager.OnRetireSession", Array.Empty<object>()), Array.Empty<object>());
		CollectionManager collectionManager = CollectionManager.Get();
		if (collectionManager != null)
		{
			collectionManager.DoneEditing();
		}
		TavernBrawlRequestSessionRetireResponse tavernBrawlSessionRetired = Network.Get().GetTavernBrawlSessionRetired();
		if (tavernBrawlSessionRetired.ErrorCode != ErrorCode.ERROR_OK)
		{
			string text = tavernBrawlSessionRetired.ErrorCode.ToString();
			Debug.LogWarning(string.Concat(new object[]
			{
				"TavernBrawlManager.OnRetireSession: Got Error ",
				tavernBrawlSessionRetired.ErrorCode,
				" : ",
				text
			}));
			return;
		}
		if (tavernBrawlSessionRetired.HasPlayerRecord)
		{
			this.OnTavernBrawlRecord_Internal(tavernBrawlSessionRetired.PlayerRecord);
		}
		this.MyRecord.SessionStatus = TavernBrawlStatus.TB_STATUS_IN_REWARDS;
		this.CurrentSession.Chest = tavernBrawlSessionRetired.Chest;
		if (this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}

	// Token: 0x060068A7 RID: 26791 RVA: 0x00221D50 File Offset: 0x0021FF50
	private void OnAckRewards()
	{
		Log.TavernBrawl.Print(string.Format("TavernBrawlManager.OnAckRewards", Array.Empty<object>()), Array.Empty<object>());
		SessionRecord sessionRecord = new SessionRecord();
		sessionRecord.Wins = (uint)this.GamesWon;
		sessionRecord.Losses = (uint)this.GamesLost;
		sessionRecord.RunFinished = true;
		sessionRecord.SessionRecordType = ((this.CurrentSeasonBrawlMode == TavernBrawlMode.TB_MODE_NORMAL) ? SessionRecordType.TAVERN_BRAWL : SessionRecordType.HEROIC_BRAWL);
		BnetPresenceMgr.Get().SetGameFieldBlob(22U, sessionRecord);
		Network.Get().RequestTavernBrawlPlayerRecord(this.m_currentBrawlType);
		if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		}
	}

	// Token: 0x060068A8 RID: 26792 RVA: 0x00221DEC File Offset: 0x0021FFEC
	private void OnTavernBrawlRecord()
	{
		TavernBrawlPlayerRecord tavernBrawlRecord = Network.Get().GetTavernBrawlRecord();
		this.OnTavernBrawlRecord_Internal(tavernBrawlRecord);
	}

	// Token: 0x060068A9 RID: 26793 RVA: 0x00221E0C File Offset: 0x0022000C
	private void OnTavernBrawlInfo()
	{
		TavernBrawlInfo tavernBrawlInfo = Network.Get().GetTavernBrawlInfo();
		this.OnTavernBrawlInfo_Internal(tavernBrawlInfo);
	}

	// Token: 0x1700065E RID: 1630
	// (get) Token: 0x060068AA RID: 26794 RVA: 0x00221E2B File Offset: 0x0022002B
	// (set) Token: 0x060068AB RID: 26795 RVA: 0x00221E33 File Offset: 0x00220033
	public bool IsCheated { get; private set; }

	// Token: 0x060068AC RID: 26796 RVA: 0x00221E3C File Offset: 0x0022003C
	public void Cheat_SetScenario(int scenarioId, BrawlType brawlType = BrawlType.BRAWL_TYPE_TAVERN_BRAWL)
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		this.IsCheated = true;
		int brawlType2 = (int)brawlType;
		if (this.m_missions[brawlType2] == null)
		{
			this.m_missions[brawlType2] = new TavernBrawlMission();
		}
		this.m_missions[brawlType2].SetSeasonSpec(new TavernBrawlSeasonSpec(), brawlType);
		this.m_missions[brawlType2].tavernBrawlSpec.GameContentSeason.Scenarios.Add(new GameContentScenario());
		this.m_missions[brawlType2].tavernBrawlSpec.GameContentSeason.Scenarios[0].ScenarioId = scenarioId;
		this.m_downloadableDbfAssetsPendingLoad[(int)brawlType] = true;
		if (this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
		AssetRecordInfo assetRecordInfo = new AssetRecordInfo();
		assetRecordInfo.Asset = new AssetKey();
		assetRecordInfo.Asset.Type = AssetType.ASSET_TYPE_SCENARIO;
		assetRecordInfo.Asset.AssetId = scenarioId;
		assetRecordInfo.RecordByteSize = 0U;
		assetRecordInfo.RecordHash = null;
		DownloadableDbfCache.Get().LoadCachedAssets(true, delegate(AssetKey requestedKey, ErrorCode code, byte[] assetBytes)
		{
			this.OnDownloadableDbfAssetsLoaded(requestedKey, code, assetBytes, brawlType);
		}, new AssetRecordInfo[]
		{
			assetRecordInfo
		});
	}

	// Token: 0x060068AD RID: 26797 RVA: 0x00221F60 File Offset: 0x00220160
	public void Cheat_ResetToServerData()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		this.IsCheated = false;
		this.OnTavernBrawlInfo();
		if (this.CurrentMission() != null)
		{
			AssetRecordInfo assetRecordInfo = new AssetRecordInfo();
			assetRecordInfo.Asset = new AssetKey();
			assetRecordInfo.Asset.Type = AssetType.ASSET_TYPE_SCENARIO;
			assetRecordInfo.Asset.AssetId = this.CurrentMission().missionId;
			assetRecordInfo.RecordByteSize = 0U;
			assetRecordInfo.RecordHash = null;
			DownloadableDbfCache.Get().LoadCachedAssets(true, delegate(AssetKey requestedKey, ErrorCode code, byte[] assetBytes)
			{
				this.OnDownloadableDbfAssetsLoaded(requestedKey, code, assetBytes, BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
			}, new AssetRecordInfo[]
			{
				assetRecordInfo
			});
		}
	}

	// Token: 0x060068AE RID: 26798 RVA: 0x00221FF0 File Offset: 0x002201F0
	public void Cheat_ResetSeenStuff(int newValue)
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		this.RegisterOptionsListeners(false);
		this.LatestSeenTavernBrawlChalkboard = newValue;
		this.LatestSeenTavernBrawlSeason = newValue;
		Options.Get().SetInt(Option.TIMES_SEEN_TAVERNBRAWL_CRAZY_RULES_QUOTE, 0);
		bool seasonHasChanged = this.CheckLatestSeenSeason(false);
		this.CheckLatestSessionLimit(seasonHasChanged);
		this.RegisterOptionsListeners(true);
	}

	// Token: 0x060068AF RID: 26799 RVA: 0x00222040 File Offset: 0x00220240
	public void Cheat_SetWins(int numWins)
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		this.CurrentSession.Wins = numWins;
		if (this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}

	// Token: 0x060068B0 RID: 26800 RVA: 0x00222069 File Offset: 0x00220269
	public void Cheat_SetLosses(int numLosses)
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		this.CurrentSession.Losses = numLosses;
		if (this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}

	// Token: 0x060068B1 RID: 26801 RVA: 0x00222094 File Offset: 0x00220294
	public void Cheat_SetActiveSession(int status)
	{
		this.MyRecord.SessionStatus = (TavernBrawlStatus)status;
		TavernBrawlPlayerSession session = new TavernBrawlPlayerSession();
		this.MyRecord.Session = session;
	}

	// Token: 0x060068B2 RID: 26802 RVA: 0x002220BF File Offset: 0x002202BF
	public void Cheat_DoHeroicRewards(int wins, TavernBrawlMode mode)
	{
		this.MyRecord.SessionStatus = TavernBrawlStatus.TB_STATUS_IN_REWARDS;
		this.CurrentSession.Chest = RewardUtils.GenerateTavernBrawlRewardChest_CHEAT(wins, mode);
		this.CurrentSession.Wins = wins;
		if (this.OnTavernBrawlUpdated != null)
		{
			this.OnTavernBrawlUpdated();
		}
	}

	// Token: 0x040055BC RID: 21948
	public const int REGULAR_TAVERN_BRAWL_MINIMUM_CLASS_LEVEL = 20;

	// Token: 0x040055BD RID: 21949
	public const int FSG_BRAWL_MINIMUM_CLASS_LEVEL = 1;

	// Token: 0x040055BE RID: 21950
	public const int SPECIAL_TAVERN_BRAWL_SEASON_NUMBER_START = 100000;

	// Token: 0x040055BF RID: 21951
	private const float DEFAULT_REFRESH_SPEC_SLUSH_SECONDS_MIN = 0f;

	// Token: 0x040055C0 RID: 21952
	private const float DEFAULT_REFRESH_SPEC_SLUSH_SECONDS_MAX = 120f;

	// Token: 0x040055C1 RID: 21953
	private const int REWARD_REFRESH_PADDING_SECONDS = 2;

	// Token: 0x040055C2 RID: 21954
	private const int REWARD_REFRESH_RANDOM_DELAY_MIN = 0;

	// Token: 0x040055C3 RID: 21955
	private const int REWARD_REFRESH_RANDOM_DELAY_MAX = 30;

	// Token: 0x040055C4 RID: 21956
	private static TavernBrawlManager s_instance;

	// Token: 0x040055C5 RID: 21957
	private TavernBrawlMission[] m_missions = new TavernBrawlMission[3];

	// Token: 0x040055C6 RID: 21958
	private bool[] m_downloadableDbfAssetsPendingLoad = new bool[3];

	// Token: 0x040055C7 RID: 21959
	private TavernBrawlPlayerRecord[] m_playerRecords = new TavernBrawlPlayerRecord[3];

	// Token: 0x040055C8 RID: 21960
	private DateTime?[] m_scheduledRefreshTimes = new DateTime?[3];

	// Token: 0x040055C9 RID: 21961
	private DateTime?[] m_nextSeasonStartDates = new DateTime?[3];

	// Token: 0x040055CA RID: 21962
	private int?[] m_latestSeenSeasonThisSession = new int?[3];

	// Token: 0x040055CB RID: 21963
	private int?[] m_latestSeenChalkboardThisSession = new int?[3];

	// Token: 0x040055CC RID: 21964
	private BrawlType m_currentBrawlType = BrawlType.BRAWL_TYPE_TAVERN_BRAWL;

	// Token: 0x040055CD RID: 21965
	private List<TavernBrawlManager.CallbackEnsureServerDataReady> m_serverDataReadyCallbacks;

	// Token: 0x040055CE RID: 21966
	private bool m_hasGottenClientOptionsAtLeastOnce;

	// Token: 0x040055CF RID: 21967
	private bool m_isFirstTimeSeeingThisFeature;

	// Token: 0x040055D0 RID: 21968
	private bool m_isFirstTimeSeeingCurrentSeason;

	// Token: 0x02002306 RID: 8966
	// (Invoke) Token: 0x06012980 RID: 76160
	public delegate void CallbackEnsureServerDataReady();

	// Token: 0x02002307 RID: 8967
	// (Invoke) Token: 0x06012984 RID: 76164
	public delegate void TavernBrawlSessionLimitRaisedCallback(int oldLimit, int newLimit);
}
