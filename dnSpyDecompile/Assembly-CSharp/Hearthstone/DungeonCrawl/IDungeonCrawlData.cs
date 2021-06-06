using System;
using System.Collections.Generic;
using PegasusShared;

namespace Hearthstone.DungeonCrawl
{
	// Token: 0x0200116A RID: 4458
	public interface IDungeonCrawlData
	{
		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x0600C31E RID: 49950
		// (set) Token: 0x0600C31F RID: 49951
		TAG_CLASS SelectedHeroClass { get; set; }

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x0600C320 RID: 49952
		// (set) Token: 0x0600C321 RID: 49953
		long SelectedDeckId { get; set; }

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x0600C322 RID: 49954
		// (set) Token: 0x0600C323 RID: 49955
		long SelectedHeroPowerDbId { get; set; }

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x0600C324 RID: 49956
		// (set) Token: 0x0600C325 RID: 49957
		long SelectedLoadoutTreasureDbId { get; set; }

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x0600C326 RID: 49958
		// (set) Token: 0x0600C327 RID: 49959
		bool AnomalyModeActivated { get; set; }

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x0600C328 RID: 49960
		// (set) Token: 0x0600C329 RID: 49961
		bool IsDevMode { get; set; }

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x0600C32A RID: 49962
		DungeonRunVisualStyle VisualStyle { get; }

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x0600C32B RID: 49963
		GameType GameType { get; }

		// Token: 0x0600C32C RID: 49964
		AdventureDbId GetSelectedAdventure();

		// Token: 0x0600C32D RID: 49965
		AdventureModeDbId GetSelectedMode();

		// Token: 0x0600C32E RID: 49966
		void SetMission(ScenarioDbId mission, bool showDetails = true);

		// Token: 0x0600C32F RID: 49967
		ScenarioDbId GetMission();

		// Token: 0x0600C330 RID: 49968
		bool SelectableHeroPowersExist();

		// Token: 0x0600C331 RID: 49969
		bool SelectableDecksExist();

		// Token: 0x0600C332 RID: 49970
		bool SelectableHeroPowersAndDecksExist();

		// Token: 0x0600C333 RID: 49971
		bool SelectableLoadoutTreasuresExist();

		// Token: 0x0600C334 RID: 49972
		void SetMissionOverride(ScenarioDbId missionOverride);

		// Token: 0x0600C335 RID: 49973
		ScenarioDbId GetMissionOverride();

		// Token: 0x0600C336 RID: 49974
		ScenarioDbId GetMissionToPlay();

		// Token: 0x0600C337 RID: 49975
		int GetAdventureBossesInRun(WingDbfRecord wingRecord);

		// Token: 0x0600C338 RID: 49976
		bool HeroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure();

		// Token: 0x0600C339 RID: 49977
		List<int> GetGuestHeroesForCurrentAdventure();

		// Token: 0x0600C33A RID: 49978
		bool GuestHeroesExistForCurrentAdventure();

		// Token: 0x0600C33B RID: 49979
		bool DoesSelectedMissionRequireDeck();

		// Token: 0x0600C33C RID: 49980
		List<AdventureHeroPowerDbfRecord> GetHeroPowersForClass(TAG_CLASS classId);

		// Token: 0x0600C33D RID: 49981
		List<AdventureDeckDbfRecord> GetDecksForClass(TAG_CLASS classId);

		// Token: 0x0600C33E RID: 49982
		List<AdventureLoadoutTreasuresDbfRecord> GetLoadoutTreasuresForClass(TAG_CLASS classId);

		// Token: 0x0600C33F RID: 49983
		bool AdventureHeroPowerIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureHeroPowerDbfRecord heroPowerRecord, out long unlockProgress, out bool hasUnlockCriteria);

		// Token: 0x0600C340 RID: 49984
		bool AdventureDeckIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureDeckDbfRecord deckRecord, out long unlockProgress, out bool hasUnlockCriteria);

		// Token: 0x0600C341 RID: 49985
		bool AdventureTreasureIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureLoadoutTreasuresDbfRecord treasureLoadoutRecord, out long unlockProgress, out bool hasUnlockCriteria);

		// Token: 0x0600C342 RID: 49986
		bool AdventureTreasureIsUpgraded(GameSaveKeyId gameSaveServerKey, AdventureLoadoutTreasuresDbfRecord treasureLoadoutRecord, out long upgradeProgress);

		// Token: 0x0600C343 RID: 49987
		void SetSelectedAdventureMode(AdventureDbId adventureId, AdventureModeDbId modeId);

		// Token: 0x0600C344 RID: 49988
		bool AdventureRewardIsUnlocked(GameSaveKeyId gameSaveServerKey, GameSaveKeySubkeyId unlockGameSaveSubkey, int unlockValue, int unlockAchievement, out long unlockProgress, out bool hasUnlockCriteria);

		// Token: 0x0600C345 RID: 49989
		AdventureDef GetAdventureDef();

		// Token: 0x0600C346 RID: 49990
		GameSaveKeyId GetGameSaveClientKey();

		// Token: 0x0600C347 RID: 49991
		AdventureWingDef GetWingDef(WingDbId wingId);

		// Token: 0x0600C348 RID: 49992
		AdventureDataDbfRecord GetSelectedAdventureDataRecord();

		// Token: 0x0600C349 RID: 49993
		bool HasValidLoadoutForSelectedAdventure();
	}
}
