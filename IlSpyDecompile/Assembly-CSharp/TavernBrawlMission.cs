using System;
using System.Collections.Generic;
using System.Linq;
using PegasusShared;

public class TavernBrawlMission
{
	private CharacterDialogSequence m_firstTimeSeenCharacterDialogSequence;

	private int m_selectedBrawlIndex = -1;

	private Map<int, DeckRuleset> m_cachedSelectedDeckRuleset = new Map<int, DeckRuleset>();

	public TavernBrawlSeasonSpec tavernBrawlSpec { get; private set; }

	public int seasonId => tavernBrawlSpec.GameContentSeason.SeasonId;

	public GameContentScenario SelectedBrawl
	{
		get
		{
			if (tavernBrawlSpec == null || m_selectedBrawlIndex < 0 || m_selectedBrawlIndex >= tavernBrawlSpec.GameContentSeason.Scenarios.Count)
			{
				if (tavernBrawlSpec != null && tavernBrawlSpec.GameContentSeason.Scenarios.Count == 1)
				{
					return tavernBrawlSpec.GameContentSeason.Scenarios[0];
				}
				return null;
			}
			return tavernBrawlSpec.GameContentSeason.Scenarios[m_selectedBrawlIndex];
		}
	}

	public IList<GameContentScenario> BrawlList
	{
		get
		{
			if (tavernBrawlSpec != null)
			{
				return tavernBrawlSpec.GameContentSeason.Scenarios;
			}
			return new List<GameContentScenario>();
		}
	}

	public int missionId => SelectedBrawl?.ScenarioId ?? 0;

	public int SelectedBrawlLibraryItemId => SelectedBrawl?.LibraryItemId ?? 0;

	public TavernBrawlMode brawlMode => SelectedBrawl?.BrawlMode ?? TavernBrawlMode.TB_MODE_NORMAL;

	public FormatType formatType => SelectedBrawl?.FormatType ?? FormatType.FT_UNKNOWN;

	public DateTime? endDateLocal
	{
		get
		{
			if (!tavernBrawlSpec.GameContentSeason.HasEndSecondsFromNow)
			{
				return null;
			}
			return DateTime.Now + new TimeSpan(0, 0, (int)tavernBrawlSpec.GameContentSeason.EndSecondsFromNow);
		}
	}

	public DateTime? closedToNewSessionsDateLocal
	{
		get
		{
			GameContentScenario selectedBrawl = SelectedBrawl;
			if (selectedBrawl == null || !selectedBrawl.HasClosedToNewSessionsSecondsFromNow)
			{
				return null;
			}
			return DateTime.Now + new TimeSpan(0, 0, (int)selectedBrawl.ClosedToNewSessionsSecondsFromNow);
		}
	}

	public bool canCreateDeck => CanCreateDeck(SelectedBrawlLibraryItemId);

	public bool canEditDeck
	{
		get
		{
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
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

	public bool canSelectHeroForDeck
	{
		get
		{
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
			if (record != null)
			{
				RuleType ruleType = (RuleType)record.RuleType;
				if ((uint)(ruleType - 1) <= 1u)
				{
					return true;
				}
			}
			return false;
		}
	}

	public int ticketType => SelectedBrawl?.TicketType ?? 0;

	public RewardType rewardType => SelectedBrawl?.RewardType ?? RewardType.REWARD_UNKNOWN;

	public RewardTrigger rewardTrigger => SelectedBrawl?.RewardTrigger ?? RewardTrigger.REWARD_TRIGGER_UNKNOWN;

	public long RewardData1 => SelectedBrawl?.RewardData1 ?? 0;

	public long RewardData2 => SelectedBrawl?.RewardData2 ?? 0;

	public int RewardTriggerQuota => SelectedBrawl?.RewardTriggerQuota ?? 0;

	public int maxWins => SelectedBrawl?.MaxWins ?? 0;

	public int maxLosses => SelectedBrawl?.MaxLosses ?? 0;

	public int maxSessions => SelectedBrawl?.MaxSessions ?? 0;

	public int SeasonEndSecondsSpreadCount => tavernBrawlSpec.GameContentSeason.SeasonEndSecondSpreadCount;

	public bool friendlyChallengeDisabled => SelectedBrawl?.FriendlyChallengeDisabled ?? false;

	public uint FreeSessions
	{
		get
		{
			GameContentScenario selectedBrawl = SelectedBrawl;
			if (selectedBrawl == null || !selectedBrawl.HasFreeSessions)
			{
				return 0u;
			}
			return selectedBrawl.FreeSessions;
		}
	}

	public bool IsPrerelease => BrawlList.Any((GameContentScenario s) => s.HasIsPrerelease && s.IsPrerelease);

	public bool IsSessionBased
	{
		get
		{
			if (maxWins <= 0)
			{
				return maxLosses > 0;
			}
			return true;
		}
	}

	public BrawlType BrawlType { get; private set; }

	public int FirstTimeSeenCharacterDialogID => SelectedBrawl?.FirstTimeSeenDialogId ?? 0;

	public bool IsDungeonRun
	{
		get
		{
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
			if (record != null)
			{
				AdventureModeDbId modeId = (AdventureModeDbId)record.ModeId;
				if (modeId != AdventureModeDbId.DUNGEON_CRAWL)
				{
					return modeId == AdventureModeDbId.DUNGEON_CRAWL_HEROIC;
				}
				return true;
			}
			return false;
		}
	}

	public CharacterDialogSequence FirstTimeSeenCharacterDialogSequence
	{
		get
		{
			if (FirstTimeSeenCharacterDialogID < 1)
			{
				return null;
			}
			if (m_firstTimeSeenCharacterDialogSequence == null)
			{
				m_firstTimeSeenCharacterDialogSequence = new CharacterDialogSequence(FirstTimeSeenCharacterDialogID);
			}
			return m_firstTimeSeenCharacterDialogSequence;
		}
	}

	public GameType GameType => GetGameType(BrawlType, missionId);

	public string StoreInstructionPrefab
	{
		get
		{
			if (!UniversalInputManager.UsePhoneUI)
			{
				if (!tavernBrawlSpec.HasDeprecatedStoreInstructionPrefab)
				{
					return string.Empty;
				}
				return tavernBrawlSpec.DeprecatedStoreInstructionPrefab;
			}
			if (!tavernBrawlSpec.HasDeprecatedStoreInstructionPrefabPhone)
			{
				return string.Empty;
			}
			return tavernBrawlSpec.DeprecatedStoreInstructionPrefabPhone;
		}
	}

	public int SelectedBrawlIndex => m_selectedBrawlIndex;

	public bool CanCreateDeck(int brawlLibraryItemId)
	{
		if (brawlLibraryItemId == 0)
		{
			brawlLibraryItemId = SelectedBrawlLibraryItemId;
		}
		int scenarioId = GetScenarioId(brawlLibraryItemId);
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

	public DeckRuleset GetDeckRuleset(int brawlLibraryItemId)
	{
		DeckRuleset value = null;
		if (!m_cachedSelectedDeckRuleset.TryGetValue(brawlLibraryItemId, out value))
		{
			int scenarioId = GetScenarioId(brawlLibraryItemId);
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(scenarioId);
			if (record != null)
			{
				value = DeckRuleset.GetDeckRuleset(record.DeckRulesetId);
				m_cachedSelectedDeckRuleset[brawlLibraryItemId] = value;
			}
		}
		return value;
	}

	public void SetSeasonSpec(TavernBrawlSeasonSpec spec, BrawlType brawlType)
	{
		if (spec == null)
		{
			throw new ArgumentNullException("TavernBrawlMissions must have a spec provided");
		}
		tavernBrawlSpec = spec;
		BrawlType = brawlType;
		m_selectedBrawlIndex = -1;
		m_firstTimeSeenCharacterDialogSequence = null;
		m_cachedSelectedDeckRuleset.Clear();
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
			return (a.IsFallback != b.IsFallback) ? ((!a.IsFallback) ? 1 : (-1)) : (b.ScenarioId - a.ScenarioId);
		});
	}

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
		if (!isFriendlyChallenge)
		{
			return GameType.GT_TAVERNBRAWL;
		}
		return GameType.GT_VS_FRIEND;
	}

	public void SetSelectedBrawlLibraryItemId(int brawlLibraryItemId)
	{
		m_selectedBrawlIndex = -1;
		IList<GameContentScenario> brawlList = BrawlList;
		for (int i = 0; i < brawlList.Count; i++)
		{
			if (brawlList[i].LibraryItemId == brawlLibraryItemId)
			{
				m_selectedBrawlIndex = i;
				break;
			}
		}
	}

	public int GetScenarioId(int brawlLibraryItemId)
	{
		if (brawlLibraryItemId == 0)
		{
			brawlLibraryItemId = SelectedBrawlLibraryItemId;
		}
		return BrawlList.FirstOrDefault((GameContentScenario s) => s.LibraryItemId == brawlLibraryItemId)?.ScenarioId ?? 0;
	}
}
