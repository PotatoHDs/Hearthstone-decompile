using System;
using System.Collections.Generic;
using PegasusShared;

namespace Hearthstone.DungeonCrawl
{
	// Token: 0x0200116C RID: 4460
	public class DungeonCrawlDataAdventureAdapter : IDungeonCrawlData
	{
		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x0600C352 RID: 50002 RVA: 0x003B0E2C File Offset: 0x003AF02C
		// (set) Token: 0x0600C353 RID: 50003 RVA: 0x003B0E50 File Offset: 0x003AF050
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
				if (adventureConfig == null)
				{
					return;
				}
				adventureConfig.SelectedHeroClass = value;
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x0600C354 RID: 50004 RVA: 0x003B0E74 File Offset: 0x003AF074
		// (set) Token: 0x0600C355 RID: 50005 RVA: 0x003B0E9C File Offset: 0x003AF09C
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
				if (adventureConfig == null)
				{
					return;
				}
				adventureConfig.SelectedDeckId = value;
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x0600C356 RID: 50006 RVA: 0x003B0EC0 File Offset: 0x003AF0C0
		// (set) Token: 0x0600C357 RID: 50007 RVA: 0x003B0EE8 File Offset: 0x003AF0E8
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
				if (adventureConfig == null)
				{
					return;
				}
				adventureConfig.SelectedHeroPowerDbId = value;
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x0600C358 RID: 50008 RVA: 0x003B0F0C File Offset: 0x003AF10C
		// (set) Token: 0x0600C359 RID: 50009 RVA: 0x003B0F34 File Offset: 0x003AF134
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
				if (adventureConfig == null)
				{
					return;
				}
				adventureConfig.SelectedLoadoutTreasureDbId = value;
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x0600C35A RID: 50010 RVA: 0x003B0F58 File Offset: 0x003AF158
		// (set) Token: 0x0600C35B RID: 50011 RVA: 0x003B0F7C File Offset: 0x003AF17C
		public bool AnomalyModeActivated
		{
			get
			{
				AdventureConfig adventureConfig = AdventureConfig.Get();
				return !(adventureConfig == null) && adventureConfig.AnomalyModeActivated;
			}
			set
			{
				AdventureConfig adventureConfig = AdventureConfig.Get();
				if (adventureConfig == null)
				{
					return;
				}
				adventureConfig.AnomalyModeActivated = value;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x0600C35C RID: 50012 RVA: 0x003B0FA0 File Offset: 0x003AF1A0
		// (set) Token: 0x0600C35D RID: 50013 RVA: 0x003B0FC4 File Offset: 0x003AF1C4
		public bool IsDevMode
		{
			get
			{
				AdventureScene adventureScene = AdventureScene.Get();
				return !(adventureScene == null) && adventureScene.IsDevMode;
			}
			set
			{
				AdventureScene adventureScene = AdventureScene.Get();
				if (adventureScene == null)
				{
					return;
				}
				adventureScene.IsDevMode = value;
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x0600C35E RID: 50014 RVA: 0x003B0FE8 File Offset: 0x003AF1E8
		public DungeonRunVisualStyle VisualStyle
		{
			get
			{
				return (DungeonRunVisualStyle)this.GetSelectedAdventure();
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x0600C35F RID: 50015 RVA: 0x003B0FF0 File Offset: 0x003AF1F0
		// (set) Token: 0x0600C360 RID: 50016 RVA: 0x003B0FF8 File Offset: 0x003AF1F8
		public AssetLoadingHelper AssetLoadingHelper { get; set; }

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x0600C361 RID: 50017 RVA: 0x000052EC File Offset: 0x000034EC
		public GameType GameType
		{
			get
			{
				return GameType.GT_VS_AI;
			}
		}

		// Token: 0x0600C362 RID: 50018 RVA: 0x003B1004 File Offset: 0x003AF204
		public AdventureDbId GetSelectedAdventure()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return AdventureDbId.INVALID;
			}
			return adventureConfig.GetSelectedAdventure();
		}

		// Token: 0x0600C363 RID: 50019 RVA: 0x003B1028 File Offset: 0x003AF228
		public AdventureModeDbId GetSelectedMode()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return AdventureModeDbId.INVALID;
			}
			return adventureConfig.GetSelectedMode();
		}

		// Token: 0x0600C364 RID: 50020 RVA: 0x003B104C File Offset: 0x003AF24C
		public void SetMission(ScenarioDbId mission, bool showDetails = true)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return;
			}
			adventureConfig.SetMission(mission, showDetails);
		}

		// Token: 0x0600C365 RID: 50021 RVA: 0x003B1074 File Offset: 0x003AF274
		public ScenarioDbId GetMission()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return ScenarioDbId.INVALID;
			}
			return adventureConfig.GetMission();
		}

		// Token: 0x0600C366 RID: 50022 RVA: 0x003B1098 File Offset: 0x003AF298
		public bool SelectableHeroPowersExist()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			return !(adventureConfig == null) && AdventureUtils.SelectableHeroPowersExistForAdventure(adventureConfig.GetSelectedAdventure());
		}

		// Token: 0x0600C367 RID: 50023 RVA: 0x003B10C4 File Offset: 0x003AF2C4
		public bool SelectableDecksExist()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			return !(adventureConfig == null) && AdventureUtils.SelectableDecksExistForAdventure(adventureConfig.GetSelectedAdventure());
		}

		// Token: 0x0600C368 RID: 50024 RVA: 0x003B10F0 File Offset: 0x003AF2F0
		public bool SelectableHeroPowersAndDecksExist()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			return !(adventureConfig == null) && AdventureUtils.SelectableHeroPowersAndDecksExistForAdventure(adventureConfig.GetSelectedAdventure());
		}

		// Token: 0x0600C369 RID: 50025 RVA: 0x003B111C File Offset: 0x003AF31C
		public bool SelectableLoadoutTreasuresExist()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			return !(adventureConfig == null) && AdventureUtils.SelectableLoadoutTreasuresExistForAdventure(adventureConfig.GetSelectedAdventure());
		}

		// Token: 0x0600C36A RID: 50026 RVA: 0x003B1148 File Offset: 0x003AF348
		public void SetMissionOverride(ScenarioDbId missionOverride)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return;
			}
			adventureConfig.SetMissionOverride(missionOverride);
		}

		// Token: 0x0600C36B RID: 50027 RVA: 0x003B116C File Offset: 0x003AF36C
		public ScenarioDbId GetMissionOverride()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return ScenarioDbId.INVALID;
			}
			return adventureConfig.GetMissionOverride();
		}

		// Token: 0x0600C36C RID: 50028 RVA: 0x003B1190 File Offset: 0x003AF390
		public ScenarioDbId GetMissionToPlay()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return ScenarioDbId.INVALID;
			}
			return adventureConfig.GetMissionToPlay();
		}

		// Token: 0x0600C36D RID: 50029 RVA: 0x003B11B4 File Offset: 0x003AF3B4
		public int GetAdventureBossesInRun(WingDbfRecord wingRecord)
		{
			return AdventureConfig.GetAdventureBossesInRun(wingRecord);
		}

		// Token: 0x0600C36E RID: 50030 RVA: 0x003B11BC File Offset: 0x003AF3BC
		public bool HeroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			return !(adventureConfig == null) && adventureConfig.IsHeroSelectedBeforeDungeonCrawlScreenForSelectedAdventure();
		}

		// Token: 0x0600C36F RID: 50031 RVA: 0x003B11E0 File Offset: 0x003AF3E0
		public List<int> GetGuestHeroesForCurrentAdventure()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return new List<int>();
			}
			return adventureConfig.GetGuestHeroesForCurrentAdventure();
		}

		// Token: 0x0600C370 RID: 50032 RVA: 0x003B1208 File Offset: 0x003AF408
		public bool GuestHeroesExistForCurrentAdventure()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			return !(adventureConfig == null) && adventureConfig.GuestHeroesExistForCurrentAdventure();
		}

		// Token: 0x0600C371 RID: 50033 RVA: 0x003B122C File Offset: 0x003AF42C
		public bool DoesSelectedMissionRequireDeck()
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			return !(adventureConfig == null) && adventureConfig.DoesSelectedMissionRequireDeck();
		}

		// Token: 0x0600C372 RID: 50034 RVA: 0x003B1250 File Offset: 0x003AF450
		public List<AdventureHeroPowerDbfRecord> GetHeroPowersForClass(TAG_CLASS classId)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return new List<AdventureHeroPowerDbfRecord>();
			}
			return AdventureUtils.GetHeroPowersForAdventureAndClass(adventureConfig.GetSelectedAdventure(), classId);
		}

		// Token: 0x0600C373 RID: 50035 RVA: 0x003B1280 File Offset: 0x003AF480
		public List<AdventureDeckDbfRecord> GetDecksForClass(TAG_CLASS classId)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return new List<AdventureDeckDbfRecord>();
			}
			return AdventureUtils.GetDecksForAdventureAndClass(adventureConfig.GetSelectedAdventure(), classId);
		}

		// Token: 0x0600C374 RID: 50036 RVA: 0x003B12B0 File Offset: 0x003AF4B0
		public List<AdventureLoadoutTreasuresDbfRecord> GetLoadoutTreasuresForClass(TAG_CLASS classId)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return new List<AdventureLoadoutTreasuresDbfRecord>();
			}
			return AdventureUtils.GetLoadoutTreasuresForAdventureAndClass(adventureConfig.GetSelectedAdventure(), classId);
		}

		// Token: 0x0600C375 RID: 50037 RVA: 0x003B12DE File Offset: 0x003AF4DE
		public bool AdventureHeroPowerIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureHeroPowerDbfRecord heroPowerRecord, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureHeroPowerIsUnlocked(gameSaveServerKey, heroPowerRecord, out unlockProgress, out hasUnlockCriteria);
		}

		// Token: 0x0600C376 RID: 50038 RVA: 0x003B12EA File Offset: 0x003AF4EA
		public bool AdventureDeckIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureDeckDbfRecord deckRecord, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureDeckIsUnlocked(gameSaveServerKey, deckRecord, out unlockProgress, out hasUnlockCriteria);
		}

		// Token: 0x0600C377 RID: 50039 RVA: 0x003B12F6 File Offset: 0x003AF4F6
		public bool AdventureTreasureIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureLoadoutTreasuresDbfRecord treasureLoadoutRecord, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureTreasureIsUnlocked(gameSaveServerKey, treasureLoadoutRecord, out unlockProgress, out hasUnlockCriteria);
		}

		// Token: 0x0600C378 RID: 50040 RVA: 0x003B1302 File Offset: 0x003AF502
		public bool AdventureTreasureIsUpgraded(GameSaveKeyId gameSaveServerKey, AdventureLoadoutTreasuresDbfRecord treasureLoadoutRecord, out long upgradeProgress)
		{
			return AdventureUtils.AdventureTreasureIsUpgraded(gameSaveServerKey, treasureLoadoutRecord, out upgradeProgress);
		}

		// Token: 0x0600C379 RID: 50041 RVA: 0x003B130C File Offset: 0x003AF50C
		public void SetSelectedAdventureMode(AdventureDbId adventureId, AdventureModeDbId modeId)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return;
			}
			adventureConfig.SetSelectedAdventureMode(adventureId, modeId);
		}

		// Token: 0x0600C37A RID: 50042 RVA: 0x003B1331 File Offset: 0x003AF531
		public bool AdventureRewardIsUnlocked(GameSaveKeyId gameSaveServerKey, GameSaveKeySubkeyId unlockGameSaveSubkey, int unlockValue, int unlockAchievement, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureRewardIsUnlocked(gameSaveServerKey, unlockGameSaveSubkey, unlockValue, unlockAchievement, out unlockProgress, out hasUnlockCriteria);
		}

		// Token: 0x0600C37B RID: 50043 RVA: 0x003B1344 File Offset: 0x003AF544
		public AdventureDef GetAdventureDef()
		{
			AdventureScene adventureScene = AdventureScene.Get();
			if (adventureScene == null)
			{
				return null;
			}
			return adventureScene.GetAdventureDef(this.GetSelectedAdventure());
		}

		// Token: 0x0600C37C RID: 50044 RVA: 0x003B1370 File Offset: 0x003AF570
		public GameSaveKeyId GetGameSaveClientKey()
		{
			int selectedAdventure = (int)this.GetSelectedAdventure();
			AdventureModeDbId selectedMode = this.GetSelectedMode();
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord(selectedAdventure, (int)selectedMode);
			if (adventureDataRecord == null)
			{
				return GameSaveKeyId.INVALID;
			}
			return (GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey;
		}

		// Token: 0x0600C37D RID: 50045 RVA: 0x003B139C File Offset: 0x003AF59C
		public AdventureWingDef GetWingDef(WingDbId wingId)
		{
			AdventureScene adventureScene = AdventureScene.Get();
			if (adventureScene == null)
			{
				return null;
			}
			return adventureScene.GetWingDef(wingId);
		}

		// Token: 0x0600C37E RID: 50046 RVA: 0x003B13C1 File Offset: 0x003AF5C1
		public AdventureDataDbfRecord GetSelectedAdventureDataRecord()
		{
			return AdventureConfig.Get().GetSelectedAdventureDataRecord();
		}

		// Token: 0x0600C37F RID: 50047 RVA: 0x003B13CD File Offset: 0x003AF5CD
		public bool HasValidLoadoutForSelectedAdventure()
		{
			return AdventureConfig.Get().HasValidLoadoutForSelectedAdventure();
		}
	}
}
