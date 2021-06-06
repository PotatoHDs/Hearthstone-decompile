using System;
using System.Collections.Generic;
using System.Linq;
using PegasusShared;

// Token: 0x02000737 RID: 1847
public class TavernBrawlMission
{
	// Token: 0x17000617 RID: 1559
	// (get) Token: 0x06006811 RID: 26641 RVA: 0x0021F64B File Offset: 0x0021D84B
	// (set) Token: 0x06006812 RID: 26642 RVA: 0x0021F653 File Offset: 0x0021D853
	public TavernBrawlSeasonSpec tavernBrawlSpec { get; private set; }

	// Token: 0x17000618 RID: 1560
	// (get) Token: 0x06006813 RID: 26643 RVA: 0x0021F65C File Offset: 0x0021D85C
	public int seasonId
	{
		get
		{
			return this.tavernBrawlSpec.GameContentSeason.SeasonId;
		}
	}

	// Token: 0x17000619 RID: 1561
	// (get) Token: 0x06006814 RID: 26644 RVA: 0x0021F670 File Offset: 0x0021D870
	public GameContentScenario SelectedBrawl
	{
		get
		{
			if (this.tavernBrawlSpec != null && this.m_selectedBrawlIndex >= 0 && this.m_selectedBrawlIndex < this.tavernBrawlSpec.GameContentSeason.Scenarios.Count)
			{
				return this.tavernBrawlSpec.GameContentSeason.Scenarios[this.m_selectedBrawlIndex];
			}
			if (this.tavernBrawlSpec != null && this.tavernBrawlSpec.GameContentSeason.Scenarios.Count == 1)
			{
				return this.tavernBrawlSpec.GameContentSeason.Scenarios[0];
			}
			return null;
		}
	}

	// Token: 0x1700061A RID: 1562
	// (get) Token: 0x06006815 RID: 26645 RVA: 0x0021F6FF File Offset: 0x0021D8FF
	public IList<GameContentScenario> BrawlList
	{
		get
		{
			if (this.tavernBrawlSpec != null)
			{
				return this.tavernBrawlSpec.GameContentSeason.Scenarios;
			}
			return new List<GameContentScenario>();
		}
	}

	// Token: 0x1700061B RID: 1563
	// (get) Token: 0x06006816 RID: 26646 RVA: 0x0021F720 File Offset: 0x0021D920
	public int missionId
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl != null)
			{
				return selectedBrawl.ScenarioId;
			}
			return 0;
		}
	}

	// Token: 0x1700061C RID: 1564
	// (get) Token: 0x06006817 RID: 26647 RVA: 0x0021F740 File Offset: 0x0021D940
	public int SelectedBrawlLibraryItemId
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl != null)
			{
				return selectedBrawl.LibraryItemId;
			}
			return 0;
		}
	}

	// Token: 0x1700061D RID: 1565
	// (get) Token: 0x06006818 RID: 26648 RVA: 0x0021F760 File Offset: 0x0021D960
	public TavernBrawlMode brawlMode
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl != null)
			{
				return selectedBrawl.BrawlMode;
			}
			return TavernBrawlMode.TB_MODE_NORMAL;
		}
	}

	// Token: 0x1700061E RID: 1566
	// (get) Token: 0x06006819 RID: 26649 RVA: 0x0021F780 File Offset: 0x0021D980
	public FormatType formatType
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl != null)
			{
				return selectedBrawl.FormatType;
			}
			return FormatType.FT_UNKNOWN;
		}
	}

	// Token: 0x1700061F RID: 1567
	// (get) Token: 0x0600681A RID: 26650 RVA: 0x0021F7A0 File Offset: 0x0021D9A0
	public DateTime? endDateLocal
	{
		get
		{
			if (!this.tavernBrawlSpec.GameContentSeason.HasEndSecondsFromNow)
			{
				return null;
			}
			return new DateTime?(DateTime.Now + new TimeSpan(0, 0, (int)this.tavernBrawlSpec.GameContentSeason.EndSecondsFromNow));
		}
	}

	// Token: 0x17000620 RID: 1568
	// (get) Token: 0x0600681B RID: 26651 RVA: 0x0021F7F0 File Offset: 0x0021D9F0
	public DateTime? closedToNewSessionsDateLocal
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl == null || !selectedBrawl.HasClosedToNewSessionsSecondsFromNow)
			{
				return null;
			}
			return new DateTime?(DateTime.Now + new TimeSpan(0, 0, (int)selectedBrawl.ClosedToNewSessionsSecondsFromNow));
		}
	}

	// Token: 0x17000621 RID: 1569
	// (get) Token: 0x0600681C RID: 26652 RVA: 0x0021F836 File Offset: 0x0021DA36
	public bool canCreateDeck
	{
		get
		{
			return this.CanCreateDeck(this.SelectedBrawlLibraryItemId);
		}
	}

	// Token: 0x0600681D RID: 26653 RVA: 0x0021F844 File Offset: 0x0021DA44
	public bool CanCreateDeck(int brawlLibraryItemId)
	{
		if (brawlLibraryItemId == 0)
		{
			brawlLibraryItemId = this.SelectedBrawlLibraryItemId;
		}
		int scenarioId = this.GetScenarioId(brawlLibraryItemId);
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(scenarioId);
		if (record != null)
		{
			RuleType ruleType = (RuleType)record.RuleType;
			if (ruleType == RuleType.RULE_CHOOSE_DECK)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x17000622 RID: 1570
	// (get) Token: 0x0600681E RID: 26654 RVA: 0x0021F884 File Offset: 0x0021DA84
	public bool canEditDeck
	{
		get
		{
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(this.missionId);
			if (record != null)
			{
				RuleType ruleType = (RuleType)record.RuleType;
				if (ruleType == RuleType.RULE_CHOOSE_DECK)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x17000623 RID: 1571
	// (get) Token: 0x0600681F RID: 26655 RVA: 0x0021F8B4 File Offset: 0x0021DAB4
	public bool canSelectHeroForDeck
	{
		get
		{
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(this.missionId);
			if (record != null)
			{
				RuleType ruleType = (RuleType)record.RuleType;
				if (ruleType - RuleType.RULE_CHOOSE_HERO <= 1)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x06006820 RID: 26656 RVA: 0x0021F8E8 File Offset: 0x0021DAE8
	public DeckRuleset GetDeckRuleset(int brawlLibraryItemId)
	{
		DeckRuleset deckRuleset = null;
		if (!this.m_cachedSelectedDeckRuleset.TryGetValue(brawlLibraryItemId, out deckRuleset))
		{
			int scenarioId = this.GetScenarioId(brawlLibraryItemId);
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(scenarioId);
			if (record != null)
			{
				deckRuleset = DeckRuleset.GetDeckRuleset(record.DeckRulesetId);
				this.m_cachedSelectedDeckRuleset[brawlLibraryItemId] = deckRuleset;
			}
		}
		return deckRuleset;
	}

	// Token: 0x17000624 RID: 1572
	// (get) Token: 0x06006821 RID: 26657 RVA: 0x0021F938 File Offset: 0x0021DB38
	public int ticketType
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl != null)
			{
				return selectedBrawl.TicketType;
			}
			return 0;
		}
	}

	// Token: 0x17000625 RID: 1573
	// (get) Token: 0x06006822 RID: 26658 RVA: 0x0021F958 File Offset: 0x0021DB58
	public RewardType rewardType
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl != null)
			{
				return selectedBrawl.RewardType;
			}
			return RewardType.REWARD_UNKNOWN;
		}
	}

	// Token: 0x17000626 RID: 1574
	// (get) Token: 0x06006823 RID: 26659 RVA: 0x0021F978 File Offset: 0x0021DB78
	public RewardTrigger rewardTrigger
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl != null)
			{
				return selectedBrawl.RewardTrigger;
			}
			return RewardTrigger.REWARD_TRIGGER_UNKNOWN;
		}
	}

	// Token: 0x17000627 RID: 1575
	// (get) Token: 0x06006824 RID: 26660 RVA: 0x0021F998 File Offset: 0x0021DB98
	public long RewardData1
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl != null)
			{
				return selectedBrawl.RewardData1;
			}
			return 0L;
		}
	}

	// Token: 0x17000628 RID: 1576
	// (get) Token: 0x06006825 RID: 26661 RVA: 0x0021F9B8 File Offset: 0x0021DBB8
	public long RewardData2
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl != null)
			{
				return selectedBrawl.RewardData2;
			}
			return 0L;
		}
	}

	// Token: 0x17000629 RID: 1577
	// (get) Token: 0x06006826 RID: 26662 RVA: 0x0021F9D8 File Offset: 0x0021DBD8
	public int RewardTriggerQuota
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl != null)
			{
				return selectedBrawl.RewardTriggerQuota;
			}
			return 0;
		}
	}

	// Token: 0x1700062A RID: 1578
	// (get) Token: 0x06006827 RID: 26663 RVA: 0x0021F9F8 File Offset: 0x0021DBF8
	public int maxWins
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl != null)
			{
				return selectedBrawl.MaxWins;
			}
			return 0;
		}
	}

	// Token: 0x1700062B RID: 1579
	// (get) Token: 0x06006828 RID: 26664 RVA: 0x0021FA18 File Offset: 0x0021DC18
	public int maxLosses
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl != null)
			{
				return selectedBrawl.MaxLosses;
			}
			return 0;
		}
	}

	// Token: 0x1700062C RID: 1580
	// (get) Token: 0x06006829 RID: 26665 RVA: 0x0021FA38 File Offset: 0x0021DC38
	public int maxSessions
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl != null)
			{
				return selectedBrawl.MaxSessions;
			}
			return 0;
		}
	}

	// Token: 0x1700062D RID: 1581
	// (get) Token: 0x0600682A RID: 26666 RVA: 0x0021FA57 File Offset: 0x0021DC57
	public int SeasonEndSecondsSpreadCount
	{
		get
		{
			return this.tavernBrawlSpec.GameContentSeason.SeasonEndSecondSpreadCount;
		}
	}

	// Token: 0x1700062E RID: 1582
	// (get) Token: 0x0600682B RID: 26667 RVA: 0x0021FA6C File Offset: 0x0021DC6C
	public bool friendlyChallengeDisabled
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			return selectedBrawl != null && selectedBrawl.FriendlyChallengeDisabled;
		}
	}

	// Token: 0x1700062F RID: 1583
	// (get) Token: 0x0600682C RID: 26668 RVA: 0x0021FA8C File Offset: 0x0021DC8C
	public uint FreeSessions
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl == null || !selectedBrawl.HasFreeSessions)
			{
				return 0U;
			}
			return selectedBrawl.FreeSessions;
		}
	}

	// Token: 0x17000630 RID: 1584
	// (get) Token: 0x0600682D RID: 26669 RVA: 0x0021FAB3 File Offset: 0x0021DCB3
	public bool IsPrerelease
	{
		get
		{
			return this.BrawlList.Any((GameContentScenario s) => s.HasIsPrerelease && s.IsPrerelease);
		}
	}

	// Token: 0x17000631 RID: 1585
	// (get) Token: 0x0600682E RID: 26670 RVA: 0x0021FADF File Offset: 0x0021DCDF
	public bool IsSessionBased
	{
		get
		{
			return this.maxWins > 0 || this.maxLosses > 0;
		}
	}

	// Token: 0x17000632 RID: 1586
	// (get) Token: 0x0600682F RID: 26671 RVA: 0x0021FAF5 File Offset: 0x0021DCF5
	// (set) Token: 0x06006830 RID: 26672 RVA: 0x0021FAFD File Offset: 0x0021DCFD
	public BrawlType BrawlType { get; private set; }

	// Token: 0x17000633 RID: 1587
	// (get) Token: 0x06006831 RID: 26673 RVA: 0x0021FB08 File Offset: 0x0021DD08
	public int FirstTimeSeenCharacterDialogID
	{
		get
		{
			GameContentScenario selectedBrawl = this.SelectedBrawl;
			if (selectedBrawl != null)
			{
				return selectedBrawl.FirstTimeSeenDialogId;
			}
			return 0;
		}
	}

	// Token: 0x17000634 RID: 1588
	// (get) Token: 0x06006832 RID: 26674 RVA: 0x0021FB28 File Offset: 0x0021DD28
	public bool IsDungeonRun
	{
		get
		{
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(this.missionId);
			if (record != null)
			{
				AdventureModeDbId modeId = (AdventureModeDbId)record.ModeId;
				return modeId == AdventureModeDbId.DUNGEON_CRAWL || modeId == AdventureModeDbId.DUNGEON_CRAWL_HEROIC;
			}
			return false;
		}
	}

	// Token: 0x17000635 RID: 1589
	// (get) Token: 0x06006833 RID: 26675 RVA: 0x0021FB5C File Offset: 0x0021DD5C
	public CharacterDialogSequence FirstTimeSeenCharacterDialogSequence
	{
		get
		{
			if (this.FirstTimeSeenCharacterDialogID < 1)
			{
				return null;
			}
			if (this.m_firstTimeSeenCharacterDialogSequence == null)
			{
				this.m_firstTimeSeenCharacterDialogSequence = new CharacterDialogSequence(this.FirstTimeSeenCharacterDialogID, CharacterDialogEventType.UNSPECIFIED);
			}
			return this.m_firstTimeSeenCharacterDialogSequence;
		}
	}

	// Token: 0x06006834 RID: 26676 RVA: 0x0021FB8C File Offset: 0x0021DD8C
	public void SetSeasonSpec(TavernBrawlSeasonSpec spec, BrawlType brawlType)
	{
		if (spec == null)
		{
			throw new ArgumentNullException("TavernBrawlMissions must have a spec provided");
		}
		this.tavernBrawlSpec = spec;
		this.BrawlType = brawlType;
		this.m_selectedBrawlIndex = -1;
		this.m_firstTimeSeenCharacterDialogSequence = null;
		this.m_cachedSelectedDeckRuleset.Clear();
		spec.GameContentSeason.Scenarios.Sort(delegate(GameContentScenario a, GameContentScenario b)
		{
			if (a.IsRequired != b.IsRequired)
			{
				if (!a.IsRequired)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (a.IsFallback == b.IsFallback)
				{
					return b.ScenarioId - a.ScenarioId;
				}
				if (!a.IsFallback)
				{
					return 1;
				}
				return -1;
			}
		});
	}

	// Token: 0x17000636 RID: 1590
	// (get) Token: 0x06006835 RID: 26677 RVA: 0x0021FBFD File Offset: 0x0021DDFD
	public GameType GameType
	{
		get
		{
			return TavernBrawlMission.GetGameType(this.BrawlType, this.missionId, false);
		}
	}

	// Token: 0x06006836 RID: 26678 RVA: 0x0021FC14 File Offset: 0x0021DE14
	private static GameType GetGameType(BrawlType brawlType, int scenarioId, bool isFriendlyChallenge = false)
	{
		if (brawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
		{
			if (isFriendlyChallenge)
			{
				return GameType.GT_FSG_BRAWL_VS_FRIEND;
			}
			GameType result = GameType.GT_FSG_BRAWL;
			if (GameUtils.IsAIMission(scenarioId))
			{
				result = GameType.GT_FSG_BRAWL_1P_VS_AI;
			}
			else if (GameUtils.IsCoopMission(scenarioId))
			{
				result = GameType.GT_FSG_BRAWL_2P_COOP;
			}
			return result;
		}
		else
		{
			if (!isFriendlyChallenge)
			{
				return GameType.GT_TAVERNBRAWL;
			}
			return GameType.GT_VS_FRIEND;
		}
	}

	// Token: 0x17000637 RID: 1591
	// (get) Token: 0x06006837 RID: 26679 RVA: 0x0021FC50 File Offset: 0x0021DE50
	public string StoreInstructionPrefab
	{
		get
		{
			if (!UniversalInputManager.UsePhoneUI)
			{
				if (!this.tavernBrawlSpec.HasDeprecatedStoreInstructionPrefab)
				{
					return string.Empty;
				}
				return this.tavernBrawlSpec.DeprecatedStoreInstructionPrefab;
			}
			else
			{
				if (!this.tavernBrawlSpec.HasDeprecatedStoreInstructionPrefabPhone)
				{
					return string.Empty;
				}
				return this.tavernBrawlSpec.DeprecatedStoreInstructionPrefabPhone;
			}
		}
	}

	// Token: 0x17000638 RID: 1592
	// (get) Token: 0x06006838 RID: 26680 RVA: 0x0021FCA6 File Offset: 0x0021DEA6
	public int SelectedBrawlIndex
	{
		get
		{
			return this.m_selectedBrawlIndex;
		}
	}

	// Token: 0x06006839 RID: 26681 RVA: 0x0021FCB0 File Offset: 0x0021DEB0
	public void SetSelectedBrawlLibraryItemId(int brawlLibraryItemId)
	{
		this.m_selectedBrawlIndex = -1;
		IList<GameContentScenario> brawlList = this.BrawlList;
		for (int i = 0; i < brawlList.Count; i++)
		{
			if (brawlList[i].LibraryItemId == brawlLibraryItemId)
			{
				this.m_selectedBrawlIndex = i;
				return;
			}
		}
	}

	// Token: 0x0600683A RID: 26682 RVA: 0x0021FCF4 File Offset: 0x0021DEF4
	public int GetScenarioId(int brawlLibraryItemId)
	{
		if (brawlLibraryItemId == 0)
		{
			brawlLibraryItemId = this.SelectedBrawlLibraryItemId;
		}
		GameContentScenario gameContentScenario = this.BrawlList.FirstOrDefault((GameContentScenario s) => s.LibraryItemId == brawlLibraryItemId);
		if (gameContentScenario != null)
		{
			return gameContentScenario.ScenarioId;
		}
		return 0;
	}

	// Token: 0x040055B9 RID: 21945
	private CharacterDialogSequence m_firstTimeSeenCharacterDialogSequence;

	// Token: 0x040055BA RID: 21946
	private int m_selectedBrawlIndex = -1;

	// Token: 0x040055BB RID: 21947
	private Map<int, DeckRuleset> m_cachedSelectedDeckRuleset = new Map<int, DeckRuleset>();
}
