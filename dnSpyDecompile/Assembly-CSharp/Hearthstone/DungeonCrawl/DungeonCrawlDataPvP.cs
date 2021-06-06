using System;
using System.Collections.Generic;
using PegasusShared;

namespace Hearthstone.DungeonCrawl
{
	// Token: 0x0200116E RID: 4462
	public class DungeonCrawlDataPvP : IDungeonCrawlData
	{
		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x0600C389 RID: 50057 RVA: 0x003B1503 File Offset: 0x003AF703
		// (set) Token: 0x0600C38A RID: 50058 RVA: 0x003B150B File Offset: 0x003AF70B
		public TAG_CLASS SelectedHeroClass { get; set; }

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x0600C38B RID: 50059 RVA: 0x003B1514 File Offset: 0x003AF714
		// (set) Token: 0x0600C38C RID: 50060 RVA: 0x003B151C File Offset: 0x003AF71C
		public long SelectedDeckId { get; set; }

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x0600C38D RID: 50061 RVA: 0x003B1525 File Offset: 0x003AF725
		// (set) Token: 0x0600C38E RID: 50062 RVA: 0x003B152D File Offset: 0x003AF72D
		public long SelectedHeroPowerDbId { get; set; }

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x0600C38F RID: 50063 RVA: 0x003B1536 File Offset: 0x003AF736
		// (set) Token: 0x0600C390 RID: 50064 RVA: 0x003B153E File Offset: 0x003AF73E
		public long SelectedLoadoutTreasureDbId { get; set; }

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x0600C391 RID: 50065 RVA: 0x003B1547 File Offset: 0x003AF747
		// (set) Token: 0x0600C392 RID: 50066 RVA: 0x003B154F File Offset: 0x003AF74F
		public bool AnomalyModeActivated { get; set; }

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x0600C393 RID: 50067 RVA: 0x003B1558 File Offset: 0x003AF758
		// (set) Token: 0x0600C394 RID: 50068 RVA: 0x003B1560 File Offset: 0x003AF760
		public bool IsDevMode { get; set; }

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x0600C395 RID: 50069 RVA: 0x003B1569 File Offset: 0x003AF769
		// (set) Token: 0x0600C396 RID: 50070 RVA: 0x003B1571 File Offset: 0x003AF771
		public DungeonRunVisualStyle VisualStyle { get; set; }

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x0600C397 RID: 50071 RVA: 0x003B157A File Offset: 0x003AF77A
		// (set) Token: 0x0600C398 RID: 50072 RVA: 0x003B1582 File Offset: 0x003AF782
		public AssetLoadingHelper AssetLoadingHelper { get; set; }

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x0600C399 RID: 50073 RVA: 0x003B158C File Offset: 0x003AF78C
		public GameType GameType
		{
			get
			{
				GameType result = GameType.GT_PVPDR;
				PvPDungeonRunDisplay pvPDungeonRunDisplay = PvPDungeonRunDisplay.Get();
				if (pvPDungeonRunDisplay != null && pvPDungeonRunDisplay.GetPVPDRLobbyDataModel().IsPaidEntry)
				{
					result = GameType.GT_PVPDR_PAID;
				}
				if (this.m_data.m_missionOverride == ScenarioDbId.INVALID)
				{
					return result;
				}
				return GameType.GT_VS_AI;
			}
		}

		// Token: 0x0600C39A RID: 50074 RVA: 0x003B15CC File Offset: 0x003AF7CC
		public DungeonCrawlDataPvP(DungeonCrawlDataPvP.DataSet data)
		{
			this.m_data = data;
			this.SelectedHeroClass = TAG_CLASS.INVALID;
			this.SelectedDeckId = 0L;
			this.SelectedHeroPowerDbId = 0L;
			this.SelectedLoadoutTreasureDbId = 0L;
			this.AnomalyModeActivated = false;
			this.IsDevMode = false;
			this.VisualStyle = DungeonRunVisualStyle.PVPDR;
		}

		// Token: 0x0600C39B RID: 50075 RVA: 0x003B161E File Offset: 0x003AF81E
		public AdventureDbId GetSelectedAdventure()
		{
			return this.m_data.m_selectedAdventure;
		}

		// Token: 0x0600C39C RID: 50076 RVA: 0x003B162B File Offset: 0x003AF82B
		public AdventureModeDbId GetSelectedMode()
		{
			return this.m_data.m_selectedMode;
		}

		// Token: 0x0600C39D RID: 50077 RVA: 0x003B1638 File Offset: 0x003AF838
		public void SetMission(ScenarioDbId mission, bool showDetails = true)
		{
			this.m_data.m_mission = mission;
		}

		// Token: 0x0600C39E RID: 50078 RVA: 0x003B1646 File Offset: 0x003AF846
		public ScenarioDbId GetMission()
		{
			return this.m_data.m_mission;
		}

		// Token: 0x0600C39F RID: 50079 RVA: 0x003B1653 File Offset: 0x003AF853
		public bool SelectableHeroPowersExist()
		{
			return this.m_data.m_selectableHeroPowersExist;
		}

		// Token: 0x0600C3A0 RID: 50080 RVA: 0x003B1660 File Offset: 0x003AF860
		public bool SelectableDecksExist()
		{
			return this.m_data.m_selectableDecksExist;
		}

		// Token: 0x0600C3A1 RID: 50081 RVA: 0x003B166D File Offset: 0x003AF86D
		public bool SelectableHeroPowersAndDecksExist()
		{
			return this.m_data.m_selectableHeroPowersAndDecksExist;
		}

		// Token: 0x0600C3A2 RID: 50082 RVA: 0x003B167A File Offset: 0x003AF87A
		public bool SelectableLoadoutTreasuresExist()
		{
			return this.m_data.m_selectableLoadoutTreasuresExist;
		}

		// Token: 0x0600C3A3 RID: 50083 RVA: 0x003B1687 File Offset: 0x003AF887
		public void SetMissionOverride(ScenarioDbId missionOverride)
		{
			this.m_data.m_missionOverride = missionOverride;
		}

		// Token: 0x0600C3A4 RID: 50084 RVA: 0x003B1695 File Offset: 0x003AF895
		public ScenarioDbId GetMissionOverride()
		{
			return this.m_data.m_missionOverride;
		}

		// Token: 0x0600C3A5 RID: 50085 RVA: 0x003B16A2 File Offset: 0x003AF8A2
		public ScenarioDbId GetMissionToPlay()
		{
			if (this.m_data.m_missionOverride == ScenarioDbId.INVALID)
			{
				return this.m_data.m_mission;
			}
			return this.m_data.m_missionOverride;
		}

		// Token: 0x0600C3A6 RID: 50086 RVA: 0x003B16C8 File Offset: 0x003AF8C8
		public int GetAdventureBossesInRun(WingDbfRecord wingRecord)
		{
			return this.m_data.m_bossesInRun;
		}

		// Token: 0x0600C3A7 RID: 50087 RVA: 0x000052EC File Offset: 0x000034EC
		public bool HeroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure()
		{
			return true;
		}

		// Token: 0x0600C3A8 RID: 50088 RVA: 0x003B16D5 File Offset: 0x003AF8D5
		public List<int> GetGuestHeroesForCurrentAdventure()
		{
			return this.m_data.m_guestHeroes;
		}

		// Token: 0x0600C3A9 RID: 50089 RVA: 0x003B16E2 File Offset: 0x003AF8E2
		public bool GuestHeroesExistForCurrentAdventure()
		{
			return this.m_data.m_guestHeroes != null && this.m_data.m_guestHeroes.Count > 0;
		}

		// Token: 0x0600C3AA RID: 50090 RVA: 0x003B1706 File Offset: 0x003AF906
		public bool DoesSelectedMissionRequireDeck()
		{
			return this.m_data.m_requiresDeck;
		}

		// Token: 0x0600C3AB RID: 50091 RVA: 0x003B1713 File Offset: 0x003AF913
		public List<AdventureHeroPowerDbfRecord> GetHeroPowersForClass(TAG_CLASS classId)
		{
			return AdventureUtils.GetHeroPowersForAdventureAndClass(this.GetSelectedAdventure(), classId);
		}

		// Token: 0x0600C3AC RID: 50092 RVA: 0x003B1721 File Offset: 0x003AF921
		public List<AdventureDeckDbfRecord> GetDecksForClass(TAG_CLASS classId)
		{
			return AdventureUtils.GetDecksForAdventureAndClass(this.GetSelectedAdventure(), classId);
		}

		// Token: 0x0600C3AD RID: 50093 RVA: 0x003B172F File Offset: 0x003AF92F
		public List<AdventureLoadoutTreasuresDbfRecord> GetLoadoutTreasuresForClass(TAG_CLASS classId)
		{
			return AdventureUtils.GetLoadoutTreasuresForAdventureAndClass(this.GetSelectedAdventure(), classId);
		}

		// Token: 0x0600C3AE RID: 50094 RVA: 0x003B12DE File Offset: 0x003AF4DE
		public bool AdventureHeroPowerIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureHeroPowerDbfRecord heroPowerRecord, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureHeroPowerIsUnlocked(gameSaveServerKey, heroPowerRecord, out unlockProgress, out hasUnlockCriteria);
		}

		// Token: 0x0600C3AF RID: 50095 RVA: 0x003B12EA File Offset: 0x003AF4EA
		public bool AdventureDeckIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureDeckDbfRecord deckRecord, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureDeckIsUnlocked(gameSaveServerKey, deckRecord, out unlockProgress, out hasUnlockCriteria);
		}

		// Token: 0x0600C3B0 RID: 50096 RVA: 0x003B12F6 File Offset: 0x003AF4F6
		public bool AdventureTreasureIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureLoadoutTreasuresDbfRecord treasureLoadoutRecord, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureTreasureIsUnlocked(gameSaveServerKey, treasureLoadoutRecord, out unlockProgress, out hasUnlockCriteria);
		}

		// Token: 0x0600C3B1 RID: 50097 RVA: 0x003B1302 File Offset: 0x003AF502
		public bool AdventureTreasureIsUpgraded(GameSaveKeyId gameSaveServerKey, AdventureLoadoutTreasuresDbfRecord treasureLoadoutRecord, out long upgradeProgress)
		{
			return AdventureUtils.AdventureTreasureIsUpgraded(gameSaveServerKey, treasureLoadoutRecord, out upgradeProgress);
		}

		// Token: 0x0600C3B2 RID: 50098 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void SetSelectedAdventureMode(AdventureDbId adventureId, AdventureModeDbId modeId)
		{
		}

		// Token: 0x0600C3B3 RID: 50099 RVA: 0x003B1331 File Offset: 0x003AF531
		public bool AdventureRewardIsUnlocked(GameSaveKeyId gameSaveServerKey, GameSaveKeySubkeyId unlockGameSaveSubkey, int unlockValue, int unlockAchievement, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureRewardIsUnlocked(gameSaveServerKey, unlockGameSaveSubkey, unlockValue, unlockAchievement, out unlockProgress, out hasUnlockCriteria);
		}

		// Token: 0x0600C3B4 RID: 50100 RVA: 0x003B1740 File Offset: 0x003AF940
		public AdventureDef GetAdventureDef()
		{
			TavernBrawlDisplay tavernBrawlDisplay = TavernBrawlDisplay.Get();
			if (tavernBrawlDisplay == null)
			{
				return null;
			}
			return tavernBrawlDisplay.GetAdventureDef(this.GetSelectedAdventure());
		}

		// Token: 0x0600C3B5 RID: 50101 RVA: 0x003B176A File Offset: 0x003AF96A
		public GameSaveKeyId GetGameSaveClientKey()
		{
			return this.m_data.m_gameSaveClientKey;
		}

		// Token: 0x0600C3B6 RID: 50102 RVA: 0x003B1778 File Offset: 0x003AF978
		public AdventureWingDef GetWingDef(WingDbId wingId)
		{
			TavernBrawlDisplay tavernBrawlDisplay = TavernBrawlDisplay.Get();
			if (tavernBrawlDisplay == null)
			{
				return null;
			}
			return tavernBrawlDisplay.GetAdventureWingDef(wingId);
		}

		// Token: 0x0600C3B7 RID: 50103 RVA: 0x003B179D File Offset: 0x003AF99D
		public AdventureDataDbfRecord GetSelectedAdventureDataRecord()
		{
			return GameUtils.GetAdventureDataRecord((int)this.GetSelectedAdventure(), (int)this.GetSelectedMode());
		}

		// Token: 0x0600C3B8 RID: 50104 RVA: 0x003B17B0 File Offset: 0x003AF9B0
		public bool HasValidLoadoutForSelectedAdventure()
		{
			return AdventureUtils.IsValidLoadoutForSelectedAdventure(this.m_data.m_selectedAdventure, this.m_data.m_selectedMode, this.GetMission(), this.SelectedHeroClass, (int)this.SelectedHeroPowerDbId, (int)this.SelectedDeckId, (int)this.SelectedLoadoutTreasureDbId);
		}

		// Token: 0x04009CF9 RID: 40185
		private DungeonCrawlDataPvP.DataSet m_data;

		// Token: 0x0200292E RID: 10542
		public struct DataSet
		{
			// Token: 0x0400FBF6 RID: 64502
			public AdventureDbId m_selectedAdventure;

			// Token: 0x0400FBF7 RID: 64503
			public AdventureModeDbId m_selectedMode;

			// Token: 0x0400FBF8 RID: 64504
			public ScenarioDbId m_mission;

			// Token: 0x0400FBF9 RID: 64505
			public ScenarioDbId m_missionOverride;

			// Token: 0x0400FBFA RID: 64506
			public bool m_selectableHeroPowersExist;

			// Token: 0x0400FBFB RID: 64507
			public bool m_selectableDecksExist;

			// Token: 0x0400FBFC RID: 64508
			public bool m_selectableHeroPowersAndDecksExist;

			// Token: 0x0400FBFD RID: 64509
			public bool m_selectableLoadoutTreasuresExist;

			// Token: 0x0400FBFE RID: 64510
			public int m_bossesInRun;

			// Token: 0x0400FBFF RID: 64511
			public bool m_heroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure;

			// Token: 0x0400FC00 RID: 64512
			public List<int> m_guestHeroes;

			// Token: 0x0400FC01 RID: 64513
			public bool m_requiresDeck;

			// Token: 0x0400FC02 RID: 64514
			public GameSaveKeyId m_gameSaveClientKey;
		}
	}
}
