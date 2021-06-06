using System.Collections.Generic;
using PegasusShared;

namespace Hearthstone.DungeonCrawl
{
	public class DungeonCrawlDataTavernBrawl : IDungeonCrawlData
	{
		public struct DataSet
		{
			public AdventureDbId m_selectedAdventure;

			public AdventureModeDbId m_selectedMode;

			public ScenarioDbId m_mission;

			public ScenarioDbId m_missionOverride;

			public bool m_selectableHeroPowersExist;

			public bool m_selectableDecksExist;

			public bool m_selectableHeroPowersAndDecksExist;

			public bool m_selectableLoadoutTreasuresExist;

			public int m_bossesInRun;

			public bool m_heroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure;

			public List<int> m_guestHeroes;

			public bool m_requiresDeck;

			public GameSaveKeyId m_gameSaveClientKey;
		}

		private DataSet m_data;

		public TAG_CLASS SelectedHeroClass { get; set; }

		public long SelectedDeckId { get; set; }

		public long SelectedHeroPowerDbId { get; set; }

		public long SelectedLoadoutTreasureDbId { get; set; }

		public bool AnomalyModeActivated { get; set; }

		public bool IsDevMode { get; set; }

		public DungeonRunVisualStyle VisualStyle { get; set; }

		public AssetLoadingHelper AssetLoadingHelper { get; set; }

		public GameType GameType => GameType.GT_TAVERNBRAWL;

		public DungeonCrawlDataTavernBrawl(DataSet data)
		{
			m_data = data;
			SelectedHeroClass = TAG_CLASS.INVALID;
			SelectedDeckId = 0L;
			SelectedHeroPowerDbId = 0L;
			SelectedLoadoutTreasureDbId = 0L;
			AnomalyModeActivated = false;
			IsDevMode = false;
			VisualStyle = DungeonRunVisualStyle.TAVERN_BRAWL;
		}

		public AdventureDbId GetSelectedAdventure()
		{
			return m_data.m_selectedAdventure;
		}

		public AdventureModeDbId GetSelectedMode()
		{
			return m_data.m_selectedMode;
		}

		public void SetMission(ScenarioDbId mission, bool showDetails = true)
		{
			m_data.m_mission = mission;
		}

		public ScenarioDbId GetMission()
		{
			return m_data.m_mission;
		}

		public bool SelectableHeroPowersExist()
		{
			return m_data.m_selectableHeroPowersExist;
		}

		public bool SelectableDecksExist()
		{
			return m_data.m_selectableDecksExist;
		}

		public bool SelectableHeroPowersAndDecksExist()
		{
			return m_data.m_selectableHeroPowersAndDecksExist;
		}

		public bool SelectableLoadoutTreasuresExist()
		{
			return m_data.m_selectableLoadoutTreasuresExist;
		}

		public void SetMissionOverride(ScenarioDbId missionOverride)
		{
			m_data.m_missionOverride = missionOverride;
		}

		public ScenarioDbId GetMissionOverride()
		{
			return m_data.m_missionOverride;
		}

		public ScenarioDbId GetMissionToPlay()
		{
			if (m_data.m_missionOverride == ScenarioDbId.INVALID)
			{
				return m_data.m_mission;
			}
			return m_data.m_missionOverride;
		}

		public int GetAdventureBossesInRun(WingDbfRecord wingRecord)
		{
			return m_data.m_bossesInRun;
		}

		public bool HeroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure()
		{
			return m_data.m_heroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure;
		}

		public List<int> GetGuestHeroesForCurrentAdventure()
		{
			return m_data.m_guestHeroes;
		}

		public bool GuestHeroesExistForCurrentAdventure()
		{
			if (m_data.m_guestHeroes != null)
			{
				return m_data.m_guestHeroes.Count > 0;
			}
			return false;
		}

		public bool DoesSelectedMissionRequireDeck()
		{
			return m_data.m_requiresDeck;
		}

		public List<AdventureHeroPowerDbfRecord> GetHeroPowersForClass(TAG_CLASS classId)
		{
			return AdventureUtils.GetHeroPowersForAdventureAndClass(GetSelectedAdventure(), classId);
		}

		public List<AdventureDeckDbfRecord> GetDecksForClass(TAG_CLASS classId)
		{
			return AdventureUtils.GetDecksForAdventureAndClass(GetSelectedAdventure(), classId);
		}

		public List<AdventureLoadoutTreasuresDbfRecord> GetLoadoutTreasuresForClass(TAG_CLASS classId)
		{
			return AdventureUtils.GetLoadoutTreasuresForAdventureAndClass(GetSelectedAdventure(), classId);
		}

		public bool AdventureHeroPowerIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureHeroPowerDbfRecord heroPowerRecord, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureHeroPowerIsUnlocked(gameSaveServerKey, heroPowerRecord, out unlockProgress, out hasUnlockCriteria);
		}

		public bool AdventureDeckIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureDeckDbfRecord deckRecord, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureDeckIsUnlocked(gameSaveServerKey, deckRecord, out unlockProgress, out hasUnlockCriteria);
		}

		public bool AdventureTreasureIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureLoadoutTreasuresDbfRecord treasureLoadoutRecord, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureTreasureIsUnlocked(gameSaveServerKey, treasureLoadoutRecord, out unlockProgress, out hasUnlockCriteria);
		}

		public bool AdventureTreasureIsUpgraded(GameSaveKeyId gameSaveServerKey, AdventureLoadoutTreasuresDbfRecord treasureLoadoutRecord, out long upgradeProgress)
		{
			return AdventureUtils.AdventureTreasureIsUpgraded(gameSaveServerKey, treasureLoadoutRecord, out upgradeProgress);
		}

		public void SetSelectedAdventureMode(AdventureDbId adventureId, AdventureModeDbId modeId)
		{
		}

		public bool AdventureRewardIsUnlocked(GameSaveKeyId gameSaveServerKey, GameSaveKeySubkeyId unlockGameSaveSubkey, int unlockValue, int unlockAchievement, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureRewardIsUnlocked(gameSaveServerKey, unlockGameSaveSubkey, unlockValue, unlockAchievement, out unlockProgress, out hasUnlockCriteria);
		}

		public AdventureDef GetAdventureDef()
		{
			TavernBrawlDisplay tavernBrawlDisplay = TavernBrawlDisplay.Get();
			if (tavernBrawlDisplay == null)
			{
				return null;
			}
			return tavernBrawlDisplay.GetAdventureDef(GetSelectedAdventure());
		}

		public GameSaveKeyId GetGameSaveClientKey()
		{
			return m_data.m_gameSaveClientKey;
		}

		public AdventureWingDef GetWingDef(WingDbId wingId)
		{
			TavernBrawlDisplay tavernBrawlDisplay = TavernBrawlDisplay.Get();
			if (tavernBrawlDisplay == null)
			{
				return null;
			}
			return tavernBrawlDisplay.GetAdventureWingDef(wingId);
		}

		public AdventureDataDbfRecord GetSelectedAdventureDataRecord()
		{
			return GameUtils.GetAdventureDataRecord((int)GetSelectedAdventure(), (int)GetSelectedMode());
		}

		public bool HasValidLoadoutForSelectedAdventure()
		{
			return true;
		}
	}
}
