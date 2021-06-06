using System;
using System.Collections.Generic;
using PegasusShared;

namespace Hearthstone.DungeonCrawl
{
	// Token: 0x02001170 RID: 4464
	public class DungeonCrawlDataTavernBrawl : IDungeonCrawlData
	{
		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x0600C3C1 RID: 50113 RVA: 0x003B1956 File Offset: 0x003AFB56
		// (set) Token: 0x0600C3C2 RID: 50114 RVA: 0x003B195E File Offset: 0x003AFB5E
		public TAG_CLASS SelectedHeroClass { get; set; }

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x0600C3C3 RID: 50115 RVA: 0x003B1967 File Offset: 0x003AFB67
		// (set) Token: 0x0600C3C4 RID: 50116 RVA: 0x003B196F File Offset: 0x003AFB6F
		public long SelectedDeckId { get; set; }

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x0600C3C5 RID: 50117 RVA: 0x003B1978 File Offset: 0x003AFB78
		// (set) Token: 0x0600C3C6 RID: 50118 RVA: 0x003B1980 File Offset: 0x003AFB80
		public long SelectedHeroPowerDbId { get; set; }

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x0600C3C7 RID: 50119 RVA: 0x003B1989 File Offset: 0x003AFB89
		// (set) Token: 0x0600C3C8 RID: 50120 RVA: 0x003B1991 File Offset: 0x003AFB91
		public long SelectedLoadoutTreasureDbId { get; set; }

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x0600C3C9 RID: 50121 RVA: 0x003B199A File Offset: 0x003AFB9A
		// (set) Token: 0x0600C3CA RID: 50122 RVA: 0x003B19A2 File Offset: 0x003AFBA2
		public bool AnomalyModeActivated { get; set; }

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x0600C3CB RID: 50123 RVA: 0x003B19AB File Offset: 0x003AFBAB
		// (set) Token: 0x0600C3CC RID: 50124 RVA: 0x003B19B3 File Offset: 0x003AFBB3
		public bool IsDevMode { get; set; }

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x0600C3CD RID: 50125 RVA: 0x003B19BC File Offset: 0x003AFBBC
		// (set) Token: 0x0600C3CE RID: 50126 RVA: 0x003B19C4 File Offset: 0x003AFBC4
		public DungeonRunVisualStyle VisualStyle { get; set; }

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x0600C3CF RID: 50127 RVA: 0x003B19CD File Offset: 0x003AFBCD
		// (set) Token: 0x0600C3D0 RID: 50128 RVA: 0x003B19D5 File Offset: 0x003AFBD5
		public AssetLoadingHelper AssetLoadingHelper { get; set; }

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x0600C3D1 RID: 50129 RVA: 0x003B19DE File Offset: 0x003AFBDE
		public GameType GameType
		{
			get
			{
				return GameType.GT_TAVERNBRAWL;
			}
		}

		// Token: 0x0600C3D2 RID: 50130 RVA: 0x003B19E4 File Offset: 0x003AFBE4
		public DungeonCrawlDataTavernBrawl(DungeonCrawlDataTavernBrawl.DataSet data)
		{
			this.m_data = data;
			this.SelectedHeroClass = TAG_CLASS.INVALID;
			this.SelectedDeckId = 0L;
			this.SelectedHeroPowerDbId = 0L;
			this.SelectedLoadoutTreasureDbId = 0L;
			this.AnomalyModeActivated = false;
			this.IsDevMode = false;
			this.VisualStyle = DungeonRunVisualStyle.TAVERN_BRAWL;
		}

		// Token: 0x0600C3D3 RID: 50131 RVA: 0x003B1A32 File Offset: 0x003AFC32
		public AdventureDbId GetSelectedAdventure()
		{
			return this.m_data.m_selectedAdventure;
		}

		// Token: 0x0600C3D4 RID: 50132 RVA: 0x003B1A3F File Offset: 0x003AFC3F
		public AdventureModeDbId GetSelectedMode()
		{
			return this.m_data.m_selectedMode;
		}

		// Token: 0x0600C3D5 RID: 50133 RVA: 0x003B1A4C File Offset: 0x003AFC4C
		public void SetMission(ScenarioDbId mission, bool showDetails = true)
		{
			this.m_data.m_mission = mission;
		}

		// Token: 0x0600C3D6 RID: 50134 RVA: 0x003B1A5A File Offset: 0x003AFC5A
		public ScenarioDbId GetMission()
		{
			return this.m_data.m_mission;
		}

		// Token: 0x0600C3D7 RID: 50135 RVA: 0x003B1A67 File Offset: 0x003AFC67
		public bool SelectableHeroPowersExist()
		{
			return this.m_data.m_selectableHeroPowersExist;
		}

		// Token: 0x0600C3D8 RID: 50136 RVA: 0x003B1A74 File Offset: 0x003AFC74
		public bool SelectableDecksExist()
		{
			return this.m_data.m_selectableDecksExist;
		}

		// Token: 0x0600C3D9 RID: 50137 RVA: 0x003B1A81 File Offset: 0x003AFC81
		public bool SelectableHeroPowersAndDecksExist()
		{
			return this.m_data.m_selectableHeroPowersAndDecksExist;
		}

		// Token: 0x0600C3DA RID: 50138 RVA: 0x003B1A8E File Offset: 0x003AFC8E
		public bool SelectableLoadoutTreasuresExist()
		{
			return this.m_data.m_selectableLoadoutTreasuresExist;
		}

		// Token: 0x0600C3DB RID: 50139 RVA: 0x003B1A9B File Offset: 0x003AFC9B
		public void SetMissionOverride(ScenarioDbId missionOverride)
		{
			this.m_data.m_missionOverride = missionOverride;
		}

		// Token: 0x0600C3DC RID: 50140 RVA: 0x003B1AA9 File Offset: 0x003AFCA9
		public ScenarioDbId GetMissionOverride()
		{
			return this.m_data.m_missionOverride;
		}

		// Token: 0x0600C3DD RID: 50141 RVA: 0x003B1AB6 File Offset: 0x003AFCB6
		public ScenarioDbId GetMissionToPlay()
		{
			if (this.m_data.m_missionOverride == ScenarioDbId.INVALID)
			{
				return this.m_data.m_mission;
			}
			return this.m_data.m_missionOverride;
		}

		// Token: 0x0600C3DE RID: 50142 RVA: 0x003B1ADC File Offset: 0x003AFCDC
		public int GetAdventureBossesInRun(WingDbfRecord wingRecord)
		{
			return this.m_data.m_bossesInRun;
		}

		// Token: 0x0600C3DF RID: 50143 RVA: 0x003B1AE9 File Offset: 0x003AFCE9
		public bool HeroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure()
		{
			return this.m_data.m_heroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure;
		}

		// Token: 0x0600C3E0 RID: 50144 RVA: 0x003B1AF6 File Offset: 0x003AFCF6
		public List<int> GetGuestHeroesForCurrentAdventure()
		{
			return this.m_data.m_guestHeroes;
		}

		// Token: 0x0600C3E1 RID: 50145 RVA: 0x003B1B03 File Offset: 0x003AFD03
		public bool GuestHeroesExistForCurrentAdventure()
		{
			return this.m_data.m_guestHeroes != null && this.m_data.m_guestHeroes.Count > 0;
		}

		// Token: 0x0600C3E2 RID: 50146 RVA: 0x003B1B27 File Offset: 0x003AFD27
		public bool DoesSelectedMissionRequireDeck()
		{
			return this.m_data.m_requiresDeck;
		}

		// Token: 0x0600C3E3 RID: 50147 RVA: 0x003B1B34 File Offset: 0x003AFD34
		public List<AdventureHeroPowerDbfRecord> GetHeroPowersForClass(TAG_CLASS classId)
		{
			return AdventureUtils.GetHeroPowersForAdventureAndClass(this.GetSelectedAdventure(), classId);
		}

		// Token: 0x0600C3E4 RID: 50148 RVA: 0x003B1B42 File Offset: 0x003AFD42
		public List<AdventureDeckDbfRecord> GetDecksForClass(TAG_CLASS classId)
		{
			return AdventureUtils.GetDecksForAdventureAndClass(this.GetSelectedAdventure(), classId);
		}

		// Token: 0x0600C3E5 RID: 50149 RVA: 0x003B1B50 File Offset: 0x003AFD50
		public List<AdventureLoadoutTreasuresDbfRecord> GetLoadoutTreasuresForClass(TAG_CLASS classId)
		{
			return AdventureUtils.GetLoadoutTreasuresForAdventureAndClass(this.GetSelectedAdventure(), classId);
		}

		// Token: 0x0600C3E6 RID: 50150 RVA: 0x003B12DE File Offset: 0x003AF4DE
		public bool AdventureHeroPowerIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureHeroPowerDbfRecord heroPowerRecord, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureHeroPowerIsUnlocked(gameSaveServerKey, heroPowerRecord, out unlockProgress, out hasUnlockCriteria);
		}

		// Token: 0x0600C3E7 RID: 50151 RVA: 0x003B12EA File Offset: 0x003AF4EA
		public bool AdventureDeckIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureDeckDbfRecord deckRecord, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureDeckIsUnlocked(gameSaveServerKey, deckRecord, out unlockProgress, out hasUnlockCriteria);
		}

		// Token: 0x0600C3E8 RID: 50152 RVA: 0x003B12F6 File Offset: 0x003AF4F6
		public bool AdventureTreasureIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureLoadoutTreasuresDbfRecord treasureLoadoutRecord, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureTreasureIsUnlocked(gameSaveServerKey, treasureLoadoutRecord, out unlockProgress, out hasUnlockCriteria);
		}

		// Token: 0x0600C3E9 RID: 50153 RVA: 0x003B1302 File Offset: 0x003AF502
		public bool AdventureTreasureIsUpgraded(GameSaveKeyId gameSaveServerKey, AdventureLoadoutTreasuresDbfRecord treasureLoadoutRecord, out long upgradeProgress)
		{
			return AdventureUtils.AdventureTreasureIsUpgraded(gameSaveServerKey, treasureLoadoutRecord, out upgradeProgress);
		}

		// Token: 0x0600C3EA RID: 50154 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void SetSelectedAdventureMode(AdventureDbId adventureId, AdventureModeDbId modeId)
		{
		}

		// Token: 0x0600C3EB RID: 50155 RVA: 0x003B1331 File Offset: 0x003AF531
		public bool AdventureRewardIsUnlocked(GameSaveKeyId gameSaveServerKey, GameSaveKeySubkeyId unlockGameSaveSubkey, int unlockValue, int unlockAchievement, out long unlockProgress, out bool hasUnlockCriteria)
		{
			return AdventureUtils.AdventureRewardIsUnlocked(gameSaveServerKey, unlockGameSaveSubkey, unlockValue, unlockAchievement, out unlockProgress, out hasUnlockCriteria);
		}

		// Token: 0x0600C3EC RID: 50156 RVA: 0x003B1B60 File Offset: 0x003AFD60
		public AdventureDef GetAdventureDef()
		{
			TavernBrawlDisplay tavernBrawlDisplay = TavernBrawlDisplay.Get();
			if (tavernBrawlDisplay == null)
			{
				return null;
			}
			return tavernBrawlDisplay.GetAdventureDef(this.GetSelectedAdventure());
		}

		// Token: 0x0600C3ED RID: 50157 RVA: 0x003B1B8A File Offset: 0x003AFD8A
		public GameSaveKeyId GetGameSaveClientKey()
		{
			return this.m_data.m_gameSaveClientKey;
		}

		// Token: 0x0600C3EE RID: 50158 RVA: 0x003B1B98 File Offset: 0x003AFD98
		public AdventureWingDef GetWingDef(WingDbId wingId)
		{
			TavernBrawlDisplay tavernBrawlDisplay = TavernBrawlDisplay.Get();
			if (tavernBrawlDisplay == null)
			{
				return null;
			}
			return tavernBrawlDisplay.GetAdventureWingDef(wingId);
		}

		// Token: 0x0600C3EF RID: 50159 RVA: 0x003B1BBD File Offset: 0x003AFDBD
		public AdventureDataDbfRecord GetSelectedAdventureDataRecord()
		{
			return GameUtils.GetAdventureDataRecord((int)this.GetSelectedAdventure(), (int)this.GetSelectedMode());
		}

		// Token: 0x0600C3F0 RID: 50160 RVA: 0x000052EC File Offset: 0x000034EC
		public bool HasValidLoadoutForSelectedAdventure()
		{
			return true;
		}

		// Token: 0x04009D03 RID: 40195
		private DungeonCrawlDataTavernBrawl.DataSet m_data;

		// Token: 0x02002930 RID: 10544
		public struct DataSet
		{
			// Token: 0x0400FC05 RID: 64517
			public AdventureDbId m_selectedAdventure;

			// Token: 0x0400FC06 RID: 64518
			public AdventureModeDbId m_selectedMode;

			// Token: 0x0400FC07 RID: 64519
			public ScenarioDbId m_mission;

			// Token: 0x0400FC08 RID: 64520
			public ScenarioDbId m_missionOverride;

			// Token: 0x0400FC09 RID: 64521
			public bool m_selectableHeroPowersExist;

			// Token: 0x0400FC0A RID: 64522
			public bool m_selectableDecksExist;

			// Token: 0x0400FC0B RID: 64523
			public bool m_selectableHeroPowersAndDecksExist;

			// Token: 0x0400FC0C RID: 64524
			public bool m_selectableLoadoutTreasuresExist;

			// Token: 0x0400FC0D RID: 64525
			public int m_bossesInRun;

			// Token: 0x0400FC0E RID: 64526
			public bool m_heroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure;

			// Token: 0x0400FC0F RID: 64527
			public List<int> m_guestHeroes;

			// Token: 0x0400FC10 RID: 64528
			public bool m_requiresDeck;

			// Token: 0x0400FC11 RID: 64529
			public GameSaveKeyId m_gameSaveClientKey;
		}
	}
}
