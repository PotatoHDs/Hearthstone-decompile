using System.Collections.Generic;
using PegasusShared;

namespace Hearthstone.DungeonCrawl
{
	public class DungeonCrawlDataAdventureAdapter : IDungeonCrawlData
	{
		public TAG_CLASS SelectedHeroClass
		{
			get
			{
				AdventureConfig adventureConfig = AdventureConfig.Get();
				if (adventureConfig == null)
				{
					return TAG_CLASS.INVALID;
				}
				return adventureConfig.SelectedHeroClass;
			}
			set
			{
				AdventureConfig adventureConfig = AdventureConfig.Get();
				if (!(adventureConfig == null))
				{
					adventureConfig.SelectedHeroClass = value;
				}
			}
		}

		public long SelectedDeckId
		{
			get
			{
				AdventureConfig adventureConfig = AdventureConfig.Get();
				if (adventureConfig == null)
				{
					return 0L;
				}
				return adventureConfig.SelectedDeckId;
			}
			set
			{
				AdventureConfig adventureConfig = AdventureConfig.Get();
				if (!(adventureConfig == null))
				{
					adventureConfig.SelectedDeckId = value;
				}
			}
		}

		public long SelectedHeroPowerDbId
		{
			get
			{
				AdventureConfig adventureConfig = AdventureConfig.Get();
				if (adventureConfig == null)
				{
					return 0L;
				}
				return adventureConfig.SelectedHeroPowerDbId;
			}
			set
			{
				AdventureConfig adventureConfig = AdventureConfig.Get();
				if (!(adventureConfig == null))
				{
					adventureConfig.SelectedHeroPowerDbId = value;
				}
			}
		}

		public long SelectedLoadoutTreasureDbId
		{
			get
			{
				AdventureConfig adventureConfig = AdventureConfig.Get();
				if (adventureConfig == null)
				{
					return 0L;
				}
				return adventureConfig.SelectedLoadoutTreasureDbId;
			}
			set
			{
				AdventureConfig adventureConfig = AdventureConfig.Get();
				if (!(adventureConfig == null))
				{
					adventureConfig.SelectedLoadoutTreasureDbId = value;
				}
			}
		}

		public bool AnomalyModeActivated
		{
			get
			{
				AdventureConfig adventureConfig = AdventureConfig.Get();
				if (adventureConfig == null)
				{
					return false;
				}
				return adventureConfig.AnomalyModeActivated;
			}
			set
			{
				AdventureConfig adventureConfig = AdventureConfig.Get();
				if (!(adventureConfig == null))
				{
					adventureConfig.AnomalyModeActivated = value;
				}
			}
		}

		public bool IsDevMode
		{
			get
			{
				AdventureScene adventureScene = AdventureScene.Get();
				if (adventureScene == null)
				{
					return false;
				}
				return adventureScene.IsDevMode;
			}
			set
			{
				AdventureScene adventureScene = AdventureScene.Get();
				if (!(adventureScene == null))
				{
					adventureScene.IsDevMode = value;
				}
			}
		}

		public DungeonRunVisualStyle VisualStyle => (DungeonRunVisualStyle)GetSelectedAdventure();

		public AssetLoadingHelper AssetLoadingHelper { get; set; }

		public GameType GameType => GameType.GT_VS_AI;

		public AdventureDbId GetSelectedAdventure()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return AdventureDbId.INVALID;
			}
			return adventureConfig.GetSelectedAdventure();
		}

		public AdventureModeDbId GetSelectedMode()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return AdventureModeDbId.INVALID;
			}
			return adventureConfig.GetSelectedMode();
		}

		public void SetMission(ScenarioDbId mission, bool showDetails = true)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (!(adventureConfig == null))
			{
				adventureConfig.SetMission(mission, showDetails);
			}
		}

		public ScenarioDbId GetMission()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return ScenarioDbId.INVALID;
			}
			return adventureConfig.GetMission();
		}

		public bool SelectableHeroPowersExist()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return false;
			}
			return AdventureUtils.SelectableHeroPowersExistForAdventure(adventureConfig.GetSelectedAdventure());
		}

		public bool SelectableDecksExist()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return false;
			}
			return AdventureUtils.SelectableDecksExistForAdventure(adventureConfig.GetSelectedAdventure());
		}

		public bool SelectableHeroPowersAndDecksExist()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return false;
			}
			return AdventureUtils.SelectableHeroPowersAndDecksExistForAdventure(adventureConfig.GetSelectedAdventure());
		}

		public bool SelectableLoadoutTreasuresExist()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return false;
			}
			return AdventureUtils.SelectableLoadoutTreasuresExistForAdventure(adventureConfig.GetSelectedAdventure());
		}

		public void SetMissionOverride(ScenarioDbId missionOverride)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (!(adventureConfig == null))
			{
				adventureConfig.SetMissionOverride(missionOverride);
			}
		}

		public ScenarioDbId GetMissionOverride()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return ScenarioDbId.INVALID;
			}
			return adventureConfig.GetMissionOverride();
		}

		public ScenarioDbId GetMissionToPlay()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return ScenarioDbId.INVALID;
			}
			return adventureConfig.GetMissionToPlay();
		}

		public int GetAdventureBossesInRun(WingDbfRecord wingRecord)
		{
			return AdventureConfig.GetAdventureBossesInRun(wingRecord);
		}

		public bool HeroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return false;
			}
			return adventureConfig.IsHeroSelectedBeforeDungeonCrawlScreenForSelectedAdventure();
		}

		public List<int> GetGuestHeroesForCurrentAdventure()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return new List<int>();
			}
			return adventureConfig.GetGuestHeroesForCurrentAdventure();
		}

		public bool GuestHeroesExistForCurrentAdventure()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return false;
			}
			return adventureConfig.GuestHeroesExistForCurrentAdventure();
		}

		public bool DoesSelectedMissionRequireDeck()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return false;
			}
			return adventureConfig.DoesSelectedMissionRequireDeck();
		}

		public List<AdventureHeroPowerDbfRecord> GetHeroPowersForClass(TAG_CLASS classId)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return new List<AdventureHeroPowerDbfRecord>();
			}
			return AdventureUtils.GetHeroPowersForAdventureAndClass(adventureConfig.GetSelectedAdventure(), classId);
		}

		public List<AdventureDeckDbfRecord> GetDecksForClass(TAG_CLASS classId)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return new List<AdventureDeckDbfRecord>();
			}
			return AdventureUtils.GetDecksForAdventureAndClass(adventureConfig.GetSelectedAdventure(), classId);
		}

		public List<AdventureLoadoutTreasuresDbfRecord> GetLoadoutTreasuresForClass(TAG_CLASS classId)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return new List<AdventureLoadoutTreasuresDbfRecord>();
			}
			return AdventureUtils.GetLoadoutTreasuresForAdventureAndClass(adventureConfig.GetSelectedAdventure(), classId);
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
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (!(adventureConfig == null))
			{
				adventureConfig.SetSelectedAdventureMode(adventureId, modeId);
			}
		}

		public bool AdventureRewardIsUnlocked(GameSaveKeyId gameSaveServerKey, GameSaveKeySubkeyId unlockGameSaveSubkey, int unlockValue, int unlockAchievement, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureRewardIsUnlocked(gameSaveServerKey, unlockGameSaveSubkey, unlockValue, unlockAchievement, out unlockProgress, out hasUnlockCriteria);
		}

		public AdventureDef GetAdventureDef()
		{
			AdventureScene adventureScene = AdventureScene.Get();
			if (adventureScene == null)
			{
				return null;
			}
			return adventureScene.GetAdventureDef(GetSelectedAdventure());
		}

		public GameSaveKeyId GetGameSaveClientKey()
		{
			AdventureDbId selectedAdventure = GetSelectedAdventure();
			AdventureModeDbId selectedMode = GetSelectedMode();
			return (GameSaveKeyId)(GameUtils.GetAdventureDataRecord((int)selectedAdventure, (int)selectedMode)?.GameSaveDataClientKey ?? (-1));
		}

		public AdventureWingDef GetWingDef(WingDbId wingId)
		{
			AdventureScene adventureScene = AdventureScene.Get();
			if (adventureScene == null)
			{
				return null;
			}
			return adventureScene.GetWingDef(wingId);
		}

		public AdventureDataDbfRecord GetSelectedAdventureDataRecord()
		{
			return AdventureConfig.Get().GetSelectedAdventureDataRecord();
		}

		public bool HasValidLoadoutForSelectedAdventure()
		{
			return AdventureConfig.Get().HasValidLoadoutForSelectedAdventure();
		}
	}
}
