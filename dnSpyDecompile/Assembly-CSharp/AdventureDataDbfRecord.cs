using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000168 RID: 360
[Serializable]
public class AdventureDataDbfRecord : DbfRecord
{
	// Token: 0x17000168 RID: 360
	// (get) Token: 0x06001684 RID: 5764 RVA: 0x0007DB3E File Offset: 0x0007BD3E
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x06001685 RID: 5765 RVA: 0x0007DB46 File Offset: 0x0007BD46
	[DbfField("ADVENTURE_ID")]
	public int AdventureId
	{
		get
		{
			return this.m_adventureId;
		}
	}

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x06001686 RID: 5766 RVA: 0x0007DB4E File Offset: 0x0007BD4E
	[DbfField("MODE_ID")]
	public int ModeId
	{
		get
		{
			return this.m_modeId;
		}
	}

	// Token: 0x1700016B RID: 363
	// (get) Token: 0x06001687 RID: 5767 RVA: 0x0007DB56 File Offset: 0x0007BD56
	public AdventureModeDbfRecord ModeRecord
	{
		get
		{
			return GameDbf.AdventureMode.GetRecord(this.m_modeId);
		}
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x06001688 RID: 5768 RVA: 0x0007DB68 File Offset: 0x0007BD68
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x06001689 RID: 5769 RVA: 0x0007DB70 File Offset: 0x0007BD70
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x0600168A RID: 5770 RVA: 0x0007DB78 File Offset: 0x0007BD78
	[DbfField("SHORT_NAME")]
	public DbfLocValue ShortName
	{
		get
		{
			return this.m_shortName;
		}
	}

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x0600168B RID: 5771 RVA: 0x0007DB80 File Offset: 0x0007BD80
	[DbfField("DESCRIPTION")]
	public DbfLocValue Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x0600168C RID: 5772 RVA: 0x0007DB88 File Offset: 0x0007BD88
	[DbfField("SHORT_DESCRIPTION")]
	public DbfLocValue ShortDescription
	{
		get
		{
			return this.m_shortDescription;
		}
	}

	// Token: 0x17000171 RID: 369
	// (get) Token: 0x0600168D RID: 5773 RVA: 0x0007DB90 File Offset: 0x0007BD90
	[DbfField("LOCKED_SHORT_NAME")]
	public DbfLocValue LockedShortName
	{
		get
		{
			return this.m_lockedShortName;
		}
	}

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x0600168E RID: 5774 RVA: 0x0007DB98 File Offset: 0x0007BD98
	[DbfField("LOCKED_DESCRIPTION")]
	public DbfLocValue LockedDescription
	{
		get
		{
			return this.m_lockedDescription;
		}
	}

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x0600168F RID: 5775 RVA: 0x0007DBA0 File Offset: 0x0007BDA0
	[DbfField("LOCKED_SHORT_DESCRIPTION")]
	public DbfLocValue LockedShortDescription
	{
		get
		{
			return this.m_lockedShortDescription;
		}
	}

	// Token: 0x17000174 RID: 372
	// (get) Token: 0x06001690 RID: 5776 RVA: 0x0007DBA8 File Offset: 0x0007BDA8
	[DbfField("REQUIREMENTS_DESCRIPTION")]
	public DbfLocValue RequirementsDescription
	{
		get
		{
			return this.m_requirementsDescription;
		}
	}

	// Token: 0x17000175 RID: 373
	// (get) Token: 0x06001691 RID: 5777 RVA: 0x0007DBB0 File Offset: 0x0007BDB0
	[DbfField("REWARDS_DESCRIPTION")]
	public DbfLocValue RewardsDescription
	{
		get
		{
			return this.m_rewardsDescription;
		}
	}

	// Token: 0x17000176 RID: 374
	// (get) Token: 0x06001692 RID: 5778 RVA: 0x0007DBB8 File Offset: 0x0007BDB8
	[DbfField("COMPLETE_BANNER_TEXT")]
	public DbfLocValue CompleteBannerText
	{
		get
		{
			return this.m_completeBannerText;
		}
	}

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x06001693 RID: 5779 RVA: 0x0007DBC0 File Offset: 0x0007BDC0
	[DbfField("SHOW_PLAYABLE_SCENARIOS_COUNT")]
	public bool ShowPlayableScenariosCount
	{
		get
		{
			return this.m_showPlayableScenariosCount;
		}
	}

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x06001694 RID: 5780 RVA: 0x0007DBC8 File Offset: 0x0007BDC8
	[DbfField("STARTING_SUBSCENE")]
	public AdventureData.Adventuresubscene StartingSubscene
	{
		get
		{
			return this.m_startingSubscene;
		}
	}

	// Token: 0x17000179 RID: 377
	// (get) Token: 0x06001695 RID: 5781 RVA: 0x0007DBD0 File Offset: 0x0007BDD0
	[DbfField("SUBSCENE_TRANSITION_DIRECTION")]
	public string SubsceneTransitionDirection
	{
		get
		{
			return this.m_subsceneTransitionDirection;
		}
	}

	// Token: 0x1700017A RID: 378
	// (get) Token: 0x06001696 RID: 5782 RVA: 0x0007DBD8 File Offset: 0x0007BDD8
	[DbfField("ADVENTURE_SUB_DEF_PREFAB")]
	public string AdventureSubDefPrefab
	{
		get
		{
			return this.m_adventureSubDefPrefab;
		}
	}

	// Token: 0x1700017B RID: 379
	// (get) Token: 0x06001697 RID: 5783 RVA: 0x0007DBE0 File Offset: 0x0007BDE0
	[DbfField("GAME_SAVE_DATA_SERVER_KEY")]
	public int GameSaveDataServerKey
	{
		get
		{
			return this.m_gameSaveDataServerKeyId;
		}
	}

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x06001698 RID: 5784 RVA: 0x0007DBE8 File Offset: 0x0007BDE8
	[DbfField("GAME_SAVE_DATA_CLIENT_KEY")]
	public int GameSaveDataClientKey
	{
		get
		{
			return this.m_gameSaveDataClientKeyId;
		}
	}

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x06001699 RID: 5785 RVA: 0x0007DBF0 File Offset: 0x0007BDF0
	[DbfField("DUNGEON_CRAWL_BOSS_CARD_PREFAB")]
	public string DungeonCrawlBossCardPrefab
	{
		get
		{
			return this.m_dungeonCrawlBossCardPrefab;
		}
	}

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x0600169A RID: 5786 RVA: 0x0007DBF8 File Offset: 0x0007BDF8
	[DbfField("DUNGEON_CRAWL_PICK_HERO_FIRST")]
	public bool DungeonCrawlPickHeroFirst
	{
		get
		{
			return this.m_dungeonCrawlPickHeroFirst;
		}
	}

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x0600169B RID: 5787 RVA: 0x0007DC00 File Offset: 0x0007BE00
	[DbfField("DUNGEON_CRAWL_SKIP_HERO_SELECT")]
	public bool DungeonCrawlSkipHeroSelect
	{
		get
		{
			return this.m_dungeonCrawlSkipHeroSelect;
		}
	}

	// Token: 0x17000180 RID: 384
	// (get) Token: 0x0600169C RID: 5788 RVA: 0x0007DC08 File Offset: 0x0007BE08
	[DbfField("DUNGEON_CRAWL_MUST_PICK_SHRINE")]
	public bool DungeonCrawlMustPickShrine
	{
		get
		{
			return this.m_dungeonCrawlMustPickShrine;
		}
	}

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x0600169D RID: 5789 RVA: 0x0007DC10 File Offset: 0x0007BE10
	[DbfField("DUNGEON_CRAWL_SELECT_CHAPTER")]
	public bool DungeonCrawlSelectChapter
	{
		get
		{
			return this.m_dungeonCrawlSelectChapter;
		}
	}

	// Token: 0x17000182 RID: 386
	// (get) Token: 0x0600169E RID: 5790 RVA: 0x0007DC18 File Offset: 0x0007BE18
	[DbfField("DUNGEON_CRAWL_DISPLAY_HERO_WINS_PER_CHAPTER")]
	public bool DungeonCrawlDisplayHeroWinsPerChapter
	{
		get
		{
			return this.m_dungeonCrawlDisplayHeroWinsPerChapter;
		}
	}

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x0600169F RID: 5791 RVA: 0x0007DC20 File Offset: 0x0007BE20
	[DbfField("DUNGEON_CRAWL_IS_RETIRE_SUPPORTED")]
	public bool DungeonCrawlIsRetireSupported
	{
		get
		{
			return this.m_dungeonCrawlIsRetireSupported;
		}
	}

	// Token: 0x17000184 RID: 388
	// (get) Token: 0x060016A0 RID: 5792 RVA: 0x0007DC28 File Offset: 0x0007BE28
	[DbfField("DUNGEON_CRAWL_SHOW_BOSS_KILL_COUNT")]
	public bool DungeonCrawlShowBossKillCount
	{
		get
		{
			return this.m_dungeonCrawlShowBossKillCount;
		}
	}

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0007DC30 File Offset: 0x0007BE30
	[DbfField("DUNGEON_CRAWL_DEFAULT_TO_DECK_FROM_UPCOMING_SCENARIO")]
	public bool DungeonCrawlDefaultToDeckFromUpcomingScenario
	{
		get
		{
			return this.m_dungeonCrawlDefaultToDeckFromUpcomingScenario;
		}
	}

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x060016A2 RID: 5794 RVA: 0x0007DC38 File Offset: 0x0007BE38
	[DbfField("IGNORE_HERO_UNLOCK_REQUIREMENT")]
	public bool IgnoreHeroUnlockRequirement
	{
		get
		{
			return this.m_ignoreHeroUnlockRequirement;
		}
	}

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x060016A3 RID: 5795 RVA: 0x0007DC40 File Offset: 0x0007BE40
	[DbfField("BOSS_CARD_BACK")]
	public int BossCardBack
	{
		get
		{
			return this.m_bossCardBackId;
		}
	}

	// Token: 0x17000188 RID: 392
	// (get) Token: 0x060016A4 RID: 5796 RVA: 0x0007DC48 File Offset: 0x0007BE48
	public CardBackDbfRecord BossCardBackRecord
	{
		get
		{
			return GameDbf.CardBack.GetRecord(this.m_bossCardBackId);
		}
	}

	// Token: 0x17000189 RID: 393
	// (get) Token: 0x060016A5 RID: 5797 RVA: 0x0007DC5A File Offset: 0x0007BE5A
	[DbfField("HAS_SEEN_FEATURED_MODE_OPTION")]
	public string HasSeenFeaturedModeOption
	{
		get
		{
			return this.m_hasSeenFeaturedModeOption;
		}
	}

	// Token: 0x1700018A RID: 394
	// (get) Token: 0x060016A6 RID: 5798 RVA: 0x0007DC62 File Offset: 0x0007BE62
	[DbfField("HAS_SEEN_NEW_MODE_POPUP_OPTION")]
	public string HasSeenNewModePopupOption
	{
		get
		{
			return this.m_hasSeenNewModePopupOption;
		}
	}

	// Token: 0x1700018B RID: 395
	// (get) Token: 0x060016A7 RID: 5799 RVA: 0x0007DC6A File Offset: 0x0007BE6A
	[DbfField("PREFAB_SHOWN_ON_COMPLETE")]
	public string PrefabShownOnComplete
	{
		get
		{
			return this.m_prefabShownOnComplete;
		}
	}

	// Token: 0x1700018C RID: 396
	// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0007DC72 File Offset: 0x0007BE72
	[DbfField("ANOMALY_MODE_DEFAULT_CARD_ID")]
	public int AnomalyModeDefaultCardId
	{
		get
		{
			return this.m_anomalyModeDefaultCardId;
		}
	}

	// Token: 0x1700018D RID: 397
	// (get) Token: 0x060016A9 RID: 5801 RVA: 0x0007DC7A File Offset: 0x0007BE7A
	public CardDbfRecord AnomalyModeDefaultCardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_anomalyModeDefaultCardId);
		}
	}

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x060016AA RID: 5802 RVA: 0x0007DC8C File Offset: 0x0007BE8C
	[DbfField("ADVENTURE_BOOK_MAP_PAGE_LOCATION")]
	public AdventureData.Adventurebooklocation AdventureBookMapPageLocation
	{
		get
		{
			return this.m_adventureBookMapPageLocation;
		}
	}

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x060016AB RID: 5803 RVA: 0x0007DC94 File Offset: 0x0007BE94
	[DbfField("ADVENTURE_BOOK_REWARD_PAGE_LOCATION")]
	public AdventureData.Adventurebooklocation AdventureBookRewardPageLocation
	{
		get
		{
			return this.m_adventureBookRewardPageLocation;
		}
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x0007DC9C File Offset: 0x0007BE9C
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x060016AD RID: 5805 RVA: 0x0007DCA5 File Offset: 0x0007BEA5
	public void SetAdventureId(int v)
	{
		this.m_adventureId = v;
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x0007DCAE File Offset: 0x0007BEAE
	public void SetModeId(int v)
	{
		this.m_modeId = v;
	}

	// Token: 0x060016AF RID: 5807 RVA: 0x0007DCB7 File Offset: 0x0007BEB7
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x060016B0 RID: 5808 RVA: 0x0007DCC0 File Offset: 0x0007BEC0
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x060016B1 RID: 5809 RVA: 0x0007DCDA File Offset: 0x0007BEDA
	public void SetShortName(DbfLocValue v)
	{
		this.m_shortName = v;
		v.SetDebugInfo(base.ID, "SHORT_NAME");
	}

	// Token: 0x060016B2 RID: 5810 RVA: 0x0007DCF4 File Offset: 0x0007BEF4
	public void SetDescription(DbfLocValue v)
	{
		this.m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	// Token: 0x060016B3 RID: 5811 RVA: 0x0007DD0E File Offset: 0x0007BF0E
	public void SetShortDescription(DbfLocValue v)
	{
		this.m_shortDescription = v;
		v.SetDebugInfo(base.ID, "SHORT_DESCRIPTION");
	}

	// Token: 0x060016B4 RID: 5812 RVA: 0x0007DD28 File Offset: 0x0007BF28
	public void SetLockedShortName(DbfLocValue v)
	{
		this.m_lockedShortName = v;
		v.SetDebugInfo(base.ID, "LOCKED_SHORT_NAME");
	}

	// Token: 0x060016B5 RID: 5813 RVA: 0x0007DD42 File Offset: 0x0007BF42
	public void SetLockedDescription(DbfLocValue v)
	{
		this.m_lockedDescription = v;
		v.SetDebugInfo(base.ID, "LOCKED_DESCRIPTION");
	}

	// Token: 0x060016B6 RID: 5814 RVA: 0x0007DD5C File Offset: 0x0007BF5C
	public void SetLockedShortDescription(DbfLocValue v)
	{
		this.m_lockedShortDescription = v;
		v.SetDebugInfo(base.ID, "LOCKED_SHORT_DESCRIPTION");
	}

	// Token: 0x060016B7 RID: 5815 RVA: 0x0007DD76 File Offset: 0x0007BF76
	public void SetRequirementsDescription(DbfLocValue v)
	{
		this.m_requirementsDescription = v;
		v.SetDebugInfo(base.ID, "REQUIREMENTS_DESCRIPTION");
	}

	// Token: 0x060016B8 RID: 5816 RVA: 0x0007DD90 File Offset: 0x0007BF90
	public void SetRewardsDescription(DbfLocValue v)
	{
		this.m_rewardsDescription = v;
		v.SetDebugInfo(base.ID, "REWARDS_DESCRIPTION");
	}

	// Token: 0x060016B9 RID: 5817 RVA: 0x0007DDAA File Offset: 0x0007BFAA
	public void SetCompleteBannerText(DbfLocValue v)
	{
		this.m_completeBannerText = v;
		v.SetDebugInfo(base.ID, "COMPLETE_BANNER_TEXT");
	}

	// Token: 0x060016BA RID: 5818 RVA: 0x0007DDC4 File Offset: 0x0007BFC4
	public void SetShowPlayableScenariosCount(bool v)
	{
		this.m_showPlayableScenariosCount = v;
	}

	// Token: 0x060016BB RID: 5819 RVA: 0x0007DDCD File Offset: 0x0007BFCD
	public void SetStartingSubscene(AdventureData.Adventuresubscene v)
	{
		this.m_startingSubscene = v;
	}

	// Token: 0x060016BC RID: 5820 RVA: 0x0007DDD6 File Offset: 0x0007BFD6
	public void SetSubsceneTransitionDirection(string v)
	{
		this.m_subsceneTransitionDirection = v;
	}

	// Token: 0x060016BD RID: 5821 RVA: 0x0007DDDF File Offset: 0x0007BFDF
	public void SetAdventureSubDefPrefab(string v)
	{
		this.m_adventureSubDefPrefab = v;
	}

	// Token: 0x060016BE RID: 5822 RVA: 0x0007DDE8 File Offset: 0x0007BFE8
	public void SetGameSaveDataServerKey(int v)
	{
		this.m_gameSaveDataServerKeyId = v;
	}

	// Token: 0x060016BF RID: 5823 RVA: 0x0007DDF1 File Offset: 0x0007BFF1
	public void SetGameSaveDataClientKey(int v)
	{
		this.m_gameSaveDataClientKeyId = v;
	}

	// Token: 0x060016C0 RID: 5824 RVA: 0x0007DDFA File Offset: 0x0007BFFA
	public void SetDungeonCrawlBossCardPrefab(string v)
	{
		this.m_dungeonCrawlBossCardPrefab = v;
	}

	// Token: 0x060016C1 RID: 5825 RVA: 0x0007DE03 File Offset: 0x0007C003
	public void SetDungeonCrawlPickHeroFirst(bool v)
	{
		this.m_dungeonCrawlPickHeroFirst = v;
	}

	// Token: 0x060016C2 RID: 5826 RVA: 0x0007DE0C File Offset: 0x0007C00C
	public void SetDungeonCrawlSkipHeroSelect(bool v)
	{
		this.m_dungeonCrawlSkipHeroSelect = v;
	}

	// Token: 0x060016C3 RID: 5827 RVA: 0x0007DE15 File Offset: 0x0007C015
	public void SetDungeonCrawlMustPickShrine(bool v)
	{
		this.m_dungeonCrawlMustPickShrine = v;
	}

	// Token: 0x060016C4 RID: 5828 RVA: 0x0007DE1E File Offset: 0x0007C01E
	public void SetDungeonCrawlSelectChapter(bool v)
	{
		this.m_dungeonCrawlSelectChapter = v;
	}

	// Token: 0x060016C5 RID: 5829 RVA: 0x0007DE27 File Offset: 0x0007C027
	public void SetDungeonCrawlDisplayHeroWinsPerChapter(bool v)
	{
		this.m_dungeonCrawlDisplayHeroWinsPerChapter = v;
	}

	// Token: 0x060016C6 RID: 5830 RVA: 0x0007DE30 File Offset: 0x0007C030
	public void SetDungeonCrawlIsRetireSupported(bool v)
	{
		this.m_dungeonCrawlIsRetireSupported = v;
	}

	// Token: 0x060016C7 RID: 5831 RVA: 0x0007DE39 File Offset: 0x0007C039
	public void SetDungeonCrawlShowBossKillCount(bool v)
	{
		this.m_dungeonCrawlShowBossKillCount = v;
	}

	// Token: 0x060016C8 RID: 5832 RVA: 0x0007DE42 File Offset: 0x0007C042
	public void SetDungeonCrawlDefaultToDeckFromUpcomingScenario(bool v)
	{
		this.m_dungeonCrawlDefaultToDeckFromUpcomingScenario = v;
	}

	// Token: 0x060016C9 RID: 5833 RVA: 0x0007DE4B File Offset: 0x0007C04B
	public void SetIgnoreHeroUnlockRequirement(bool v)
	{
		this.m_ignoreHeroUnlockRequirement = v;
	}

	// Token: 0x060016CA RID: 5834 RVA: 0x0007DE54 File Offset: 0x0007C054
	public void SetBossCardBack(int v)
	{
		this.m_bossCardBackId = v;
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x0007DE5D File Offset: 0x0007C05D
	public void SetHasSeenFeaturedModeOption(string v)
	{
		this.m_hasSeenFeaturedModeOption = v;
	}

	// Token: 0x060016CC RID: 5836 RVA: 0x0007DE66 File Offset: 0x0007C066
	public void SetHasSeenNewModePopupOption(string v)
	{
		this.m_hasSeenNewModePopupOption = v;
	}

	// Token: 0x060016CD RID: 5837 RVA: 0x0007DE6F File Offset: 0x0007C06F
	public void SetPrefabShownOnComplete(string v)
	{
		this.m_prefabShownOnComplete = v;
	}

	// Token: 0x060016CE RID: 5838 RVA: 0x0007DE78 File Offset: 0x0007C078
	public void SetAnomalyModeDefaultCardId(int v)
	{
		this.m_anomalyModeDefaultCardId = v;
	}

	// Token: 0x060016CF RID: 5839 RVA: 0x0007DE81 File Offset: 0x0007C081
	public void SetAdventureBookMapPageLocation(AdventureData.Adventurebooklocation v)
	{
		this.m_adventureBookMapPageLocation = v;
	}

	// Token: 0x060016D0 RID: 5840 RVA: 0x0007DE8A File Offset: 0x0007C08A
	public void SetAdventureBookRewardPageLocation(AdventureData.Adventurebooklocation v)
	{
		this.m_adventureBookRewardPageLocation = v;
	}

	// Token: 0x060016D1 RID: 5841 RVA: 0x0007DE94 File Offset: 0x0007C094
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2758586749U)
		{
			if (num <= 1525009154U)
			{
				if (num <= 738995217U)
				{
					if (num <= 190718801U)
					{
						if (num != 177881879U)
						{
							if (num == 190718801U)
							{
								if (name == "ADVENTURE_ID")
								{
									return this.m_adventureId;
								}
							}
						}
						else if (name == "DUNGEON_CRAWL_DISPLAY_HERO_WINS_PER_CHAPTER")
						{
							return this.m_dungeonCrawlDisplayHeroWinsPerChapter;
						}
					}
					else if (num != 370156007U)
					{
						if (num == 738995217U)
						{
							if (name == "SUBSCENE_TRANSITION_DIRECTION")
							{
								return this.m_subsceneTransitionDirection;
							}
						}
					}
					else if (name == "LOCKED_SHORT_DESCRIPTION")
					{
						return this.m_lockedShortDescription;
					}
				}
				else if (num <= 1103584457U)
				{
					if (num != 938193592U)
					{
						if (num == 1103584457U)
						{
							if (name == "DESCRIPTION")
							{
								return this.m_description;
							}
						}
					}
					else if (name == "STARTING_SUBSCENE")
					{
						return this.m_startingSubscene;
					}
				}
				else if (num != 1387956774U)
				{
					if (num != 1458105184U)
					{
						if (num == 1525009154U)
						{
							if (name == "DUNGEON_CRAWL_DEFAULT_TO_DECK_FROM_UPCOMING_SCENARIO")
							{
								return this.m_dungeonCrawlDefaultToDeckFromUpcomingScenario;
							}
						}
					}
					else if (name == "ID")
					{
						return base.ID;
					}
				}
				else if (name == "NAME")
				{
					return this.m_name;
				}
			}
			else if (num <= 2203352005U)
			{
				if (num <= 1789602423U)
				{
					if (num != 1567271427U)
					{
						if (num == 1789602423U)
						{
							if (name == "BOSS_CARD_BACK")
							{
								return this.m_bossCardBackId;
							}
						}
					}
					else if (name == "HAS_SEEN_NEW_MODE_POPUP_OPTION")
					{
						return this.m_hasSeenNewModePopupOption;
					}
				}
				else if (num != 1954707640U)
				{
					if (num != 2011973942U)
					{
						if (num == 2203352005U)
						{
							if (name == "ADVENTURE_BOOK_REWARD_PAGE_LOCATION")
							{
								return this.m_adventureBookRewardPageLocation;
							}
						}
					}
					else if (name == "REWARDS_DESCRIPTION")
					{
						return this.m_rewardsDescription;
					}
				}
				else if (name == "GAME_SAVE_DATA_CLIENT_KEY")
				{
					return this.m_gameSaveDataClientKeyId;
				}
			}
			else if (num <= 2346086551U)
			{
				if (num != 2336483396U)
				{
					if (num == 2346086551U)
					{
						if (name == "DUNGEON_CRAWL_SKIP_HERO_SELECT")
						{
							return this.m_dungeonCrawlSkipHeroSelect;
						}
					}
				}
				else if (name == "GAME_SAVE_DATA_SERVER_KEY")
				{
					return this.m_gameSaveDataServerKeyId;
				}
			}
			else if (num != 2418820992U)
			{
				if (num != 2742593543U)
				{
					if (num == 2758586749U)
					{
						if (name == "IGNORE_HERO_UNLOCK_REQUIREMENT")
						{
							return this.m_ignoreHeroUnlockRequirement;
						}
					}
				}
				else if (name == "SHOW_PLAYABLE_SCENARIOS_COUNT")
				{
					return this.m_showPlayableScenariosCount;
				}
			}
			else if (name == "SHORT_DESCRIPTION")
			{
				return this.m_shortDescription;
			}
		}
		else if (num <= 3357947825U)
		{
			if (num <= 2994298469U)
			{
				if (num <= 2832700627U)
				{
					if (num != 2794963964U)
					{
						if (num == 2832700627U)
						{
							if (name == "PREFAB_SHOWN_ON_COMPLETE")
							{
								return this.m_prefabShownOnComplete;
							}
						}
					}
					else if (name == "REQUIREMENTS_DESCRIPTION")
					{
						return this.m_requirementsDescription;
					}
				}
				else if (num != 2879260603U)
				{
					if (num == 2994298469U)
					{
						if (name == "DUNGEON_CRAWL_PICK_HERO_FIRST")
						{
							return this.m_dungeonCrawlPickHeroFirst;
						}
					}
				}
				else if (name == "ANOMALY_MODE_DEFAULT_CARD_ID")
				{
					return this.m_anomalyModeDefaultCardId;
				}
			}
			else if (num <= 3030925245U)
			{
				if (num != 3022554311U)
				{
					if (num == 3030925245U)
					{
						if (name == "DUNGEON_CRAWL_MUST_PICK_SHRINE")
						{
							return this.m_dungeonCrawlMustPickShrine;
						}
					}
				}
				else if (name == "NOTE_DESC")
				{
					return this.m_noteDesc;
				}
			}
			else if (num != 3109662305U)
			{
				if (num != 3226467965U)
				{
					if (num == 3357947825U)
					{
						if (name == "DUNGEON_CRAWL_BOSS_CARD_PREFAB")
						{
							return this.m_dungeonCrawlBossCardPrefab;
						}
					}
				}
				else if (name == "SHORT_NAME")
				{
					return this.m_shortName;
				}
			}
			else if (name == "ADVENTURE_SUB_DEF_PREFAB")
			{
				return this.m_adventureSubDefPrefab;
			}
		}
		else if (num <= 3986679374U)
		{
			if (num <= 3731604237U)
			{
				if (num != 3511152538U)
				{
					if (num == 3731604237U)
					{
						if (name == "DUNGEON_CRAWL_IS_RETIRE_SUPPORTED")
						{
							return this.m_dungeonCrawlIsRetireSupported;
						}
					}
				}
				else if (name == "DUNGEON_CRAWL_SHOW_BOSS_KILL_COUNT")
				{
					return this.m_dungeonCrawlShowBossKillCount;
				}
			}
			else if (num != 3793523264U)
			{
				if (num != 3959141178U)
				{
					if (num == 3986679374U)
					{
						if (name == "LOCKED_DESCRIPTION")
						{
							return this.m_lockedDescription;
						}
					}
				}
				else if (name == "MODE_ID")
				{
					return this.m_modeId;
				}
			}
			else if (name == "ADVENTURE_BOOK_MAP_PAGE_LOCATION")
			{
				return this.m_adventureBookMapPageLocation;
			}
		}
		else if (num <= 4157405553U)
		{
			if (num != 4059986364U)
			{
				if (num == 4157405553U)
				{
					if (name == "COMPLETE_BANNER_TEXT")
					{
						return this.m_completeBannerText;
					}
				}
			}
			else if (name == "HAS_SEEN_FEATURED_MODE_OPTION")
			{
				return this.m_hasSeenFeaturedModeOption;
			}
		}
		else if (num != 4214602626U)
		{
			if (num != 4219270968U)
			{
				if (num == 4221424440U)
				{
					if (name == "LOCKED_SHORT_NAME")
					{
						return this.m_lockedShortName;
					}
				}
			}
			else if (name == "DUNGEON_CRAWL_SELECT_CHAPTER")
			{
				return this.m_dungeonCrawlSelectChapter;
			}
		}
		else if (name == "SORT_ORDER")
		{
			return this.m_sortOrder;
		}
		return null;
	}

	// Token: 0x060016D2 RID: 5842 RVA: 0x0007E5AC File Offset: 0x0007C7AC
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2758586749U)
		{
			if (num <= 1525009154U)
			{
				if (num <= 738995217U)
				{
					if (num <= 190718801U)
					{
						if (num != 177881879U)
						{
							if (num != 190718801U)
							{
								return;
							}
							if (!(name == "ADVENTURE_ID"))
							{
								return;
							}
							this.m_adventureId = (int)val;
							return;
						}
						else
						{
							if (!(name == "DUNGEON_CRAWL_DISPLAY_HERO_WINS_PER_CHAPTER"))
							{
								return;
							}
							this.m_dungeonCrawlDisplayHeroWinsPerChapter = (bool)val;
							return;
						}
					}
					else if (num != 370156007U)
					{
						if (num != 738995217U)
						{
							return;
						}
						if (!(name == "SUBSCENE_TRANSITION_DIRECTION"))
						{
							return;
						}
						this.m_subsceneTransitionDirection = (string)val;
						return;
					}
					else
					{
						if (!(name == "LOCKED_SHORT_DESCRIPTION"))
						{
							return;
						}
						this.m_lockedShortDescription = (DbfLocValue)val;
						return;
					}
				}
				else if (num <= 1103584457U)
				{
					if (num != 938193592U)
					{
						if (num != 1103584457U)
						{
							return;
						}
						if (!(name == "DESCRIPTION"))
						{
							return;
						}
						this.m_description = (DbfLocValue)val;
						return;
					}
					else
					{
						if (!(name == "STARTING_SUBSCENE"))
						{
							return;
						}
						if (val == null)
						{
							this.m_startingSubscene = AdventureData.Adventuresubscene.CHOOSER;
							return;
						}
						if (val is AdventureData.Adventuresubscene || val is int)
						{
							this.m_startingSubscene = (AdventureData.Adventuresubscene)val;
							return;
						}
						if (val is string)
						{
							this.m_startingSubscene = AdventureData.ParseAdventuresubsceneValue((string)val);
							return;
						}
					}
				}
				else if (num != 1387956774U)
				{
					if (num != 1458105184U)
					{
						if (num != 1525009154U)
						{
							return;
						}
						if (!(name == "DUNGEON_CRAWL_DEFAULT_TO_DECK_FROM_UPCOMING_SCENARIO"))
						{
							return;
						}
						this.m_dungeonCrawlDefaultToDeckFromUpcomingScenario = (bool)val;
						return;
					}
					else
					{
						if (!(name == "ID"))
						{
							return;
						}
						base.SetID((int)val);
						return;
					}
				}
				else
				{
					if (!(name == "NAME"))
					{
						return;
					}
					this.m_name = (DbfLocValue)val;
					return;
				}
			}
			else if (num <= 2203352005U)
			{
				if (num <= 1789602423U)
				{
					if (num != 1567271427U)
					{
						if (num != 1789602423U)
						{
							return;
						}
						if (!(name == "BOSS_CARD_BACK"))
						{
							return;
						}
						this.m_bossCardBackId = (int)val;
						return;
					}
					else
					{
						if (!(name == "HAS_SEEN_NEW_MODE_POPUP_OPTION"))
						{
							return;
						}
						this.m_hasSeenNewModePopupOption = (string)val;
						return;
					}
				}
				else if (num != 1954707640U)
				{
					if (num != 2011973942U)
					{
						if (num != 2203352005U)
						{
							return;
						}
						if (!(name == "ADVENTURE_BOOK_REWARD_PAGE_LOCATION"))
						{
							return;
						}
						if (val == null)
						{
							this.m_adventureBookRewardPageLocation = AdventureData.Adventurebooklocation.BEGINNING;
							return;
						}
						if (val is AdventureData.Adventurebooklocation || val is int)
						{
							this.m_adventureBookRewardPageLocation = (AdventureData.Adventurebooklocation)val;
							return;
						}
						if (val is string)
						{
							this.m_adventureBookRewardPageLocation = AdventureData.ParseAdventurebooklocationValue((string)val);
						}
					}
					else
					{
						if (!(name == "REWARDS_DESCRIPTION"))
						{
							return;
						}
						this.m_rewardsDescription = (DbfLocValue)val;
						return;
					}
				}
				else
				{
					if (!(name == "GAME_SAVE_DATA_CLIENT_KEY"))
					{
						return;
					}
					this.m_gameSaveDataClientKeyId = (int)val;
					return;
				}
			}
			else if (num <= 2346086551U)
			{
				if (num != 2336483396U)
				{
					if (num != 2346086551U)
					{
						return;
					}
					if (!(name == "DUNGEON_CRAWL_SKIP_HERO_SELECT"))
					{
						return;
					}
					this.m_dungeonCrawlSkipHeroSelect = (bool)val;
					return;
				}
				else
				{
					if (!(name == "GAME_SAVE_DATA_SERVER_KEY"))
					{
						return;
					}
					this.m_gameSaveDataServerKeyId = (int)val;
					return;
				}
			}
			else if (num != 2418820992U)
			{
				if (num != 2742593543U)
				{
					if (num != 2758586749U)
					{
						return;
					}
					if (!(name == "IGNORE_HERO_UNLOCK_REQUIREMENT"))
					{
						return;
					}
					this.m_ignoreHeroUnlockRequirement = (bool)val;
					return;
				}
				else
				{
					if (!(name == "SHOW_PLAYABLE_SCENARIOS_COUNT"))
					{
						return;
					}
					this.m_showPlayableScenariosCount = (bool)val;
					return;
				}
			}
			else
			{
				if (!(name == "SHORT_DESCRIPTION"))
				{
					return;
				}
				this.m_shortDescription = (DbfLocValue)val;
				return;
			}
		}
		else if (num <= 3357947825U)
		{
			if (num <= 2994298469U)
			{
				if (num <= 2832700627U)
				{
					if (num != 2794963964U)
					{
						if (num != 2832700627U)
						{
							return;
						}
						if (!(name == "PREFAB_SHOWN_ON_COMPLETE"))
						{
							return;
						}
						this.m_prefabShownOnComplete = (string)val;
						return;
					}
					else
					{
						if (!(name == "REQUIREMENTS_DESCRIPTION"))
						{
							return;
						}
						this.m_requirementsDescription = (DbfLocValue)val;
						return;
					}
				}
				else if (num != 2879260603U)
				{
					if (num != 2994298469U)
					{
						return;
					}
					if (!(name == "DUNGEON_CRAWL_PICK_HERO_FIRST"))
					{
						return;
					}
					this.m_dungeonCrawlPickHeroFirst = (bool)val;
					return;
				}
				else
				{
					if (!(name == "ANOMALY_MODE_DEFAULT_CARD_ID"))
					{
						return;
					}
					this.m_anomalyModeDefaultCardId = (int)val;
					return;
				}
			}
			else if (num <= 3030925245U)
			{
				if (num != 3022554311U)
				{
					if (num != 3030925245U)
					{
						return;
					}
					if (!(name == "DUNGEON_CRAWL_MUST_PICK_SHRINE"))
					{
						return;
					}
					this.m_dungeonCrawlMustPickShrine = (bool)val;
					return;
				}
				else
				{
					if (!(name == "NOTE_DESC"))
					{
						return;
					}
					this.m_noteDesc = (string)val;
					return;
				}
			}
			else if (num != 3109662305U)
			{
				if (num != 3226467965U)
				{
					if (num != 3357947825U)
					{
						return;
					}
					if (!(name == "DUNGEON_CRAWL_BOSS_CARD_PREFAB"))
					{
						return;
					}
					this.m_dungeonCrawlBossCardPrefab = (string)val;
					return;
				}
				else
				{
					if (!(name == "SHORT_NAME"))
					{
						return;
					}
					this.m_shortName = (DbfLocValue)val;
					return;
				}
			}
			else
			{
				if (!(name == "ADVENTURE_SUB_DEF_PREFAB"))
				{
					return;
				}
				this.m_adventureSubDefPrefab = (string)val;
				return;
			}
		}
		else if (num <= 3986679374U)
		{
			if (num <= 3731604237U)
			{
				if (num != 3511152538U)
				{
					if (num != 3731604237U)
					{
						return;
					}
					if (!(name == "DUNGEON_CRAWL_IS_RETIRE_SUPPORTED"))
					{
						return;
					}
					this.m_dungeonCrawlIsRetireSupported = (bool)val;
					return;
				}
				else
				{
					if (!(name == "DUNGEON_CRAWL_SHOW_BOSS_KILL_COUNT"))
					{
						return;
					}
					this.m_dungeonCrawlShowBossKillCount = (bool)val;
					return;
				}
			}
			else if (num != 3793523264U)
			{
				if (num != 3959141178U)
				{
					if (num != 3986679374U)
					{
						return;
					}
					if (!(name == "LOCKED_DESCRIPTION"))
					{
						return;
					}
					this.m_lockedDescription = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "MODE_ID"))
					{
						return;
					}
					this.m_modeId = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "ADVENTURE_BOOK_MAP_PAGE_LOCATION"))
				{
					return;
				}
				if (val == null)
				{
					this.m_adventureBookMapPageLocation = AdventureData.Adventurebooklocation.BEGINNING;
					return;
				}
				if (val is AdventureData.Adventurebooklocation || val is int)
				{
					this.m_adventureBookMapPageLocation = (AdventureData.Adventurebooklocation)val;
					return;
				}
				if (val is string)
				{
					this.m_adventureBookMapPageLocation = AdventureData.ParseAdventurebooklocationValue((string)val);
					return;
				}
			}
		}
		else if (num <= 4157405553U)
		{
			if (num != 4059986364U)
			{
				if (num != 4157405553U)
				{
					return;
				}
				if (!(name == "COMPLETE_BANNER_TEXT"))
				{
					return;
				}
				this.m_completeBannerText = (DbfLocValue)val;
				return;
			}
			else
			{
				if (!(name == "HAS_SEEN_FEATURED_MODE_OPTION"))
				{
					return;
				}
				this.m_hasSeenFeaturedModeOption = (string)val;
				return;
			}
		}
		else if (num != 4214602626U)
		{
			if (num != 4219270968U)
			{
				if (num != 4221424440U)
				{
					return;
				}
				if (!(name == "LOCKED_SHORT_NAME"))
				{
					return;
				}
				this.m_lockedShortName = (DbfLocValue)val;
				return;
			}
			else
			{
				if (!(name == "DUNGEON_CRAWL_SELECT_CHAPTER"))
				{
					return;
				}
				this.m_dungeonCrawlSelectChapter = (bool)val;
				return;
			}
		}
		else
		{
			if (!(name == "SORT_ORDER"))
			{
				return;
			}
			this.m_sortOrder = (int)val;
			return;
		}
	}

	// Token: 0x060016D3 RID: 5843 RVA: 0x0007ECFC File Offset: 0x0007CEFC
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2758586749U)
		{
			if (num <= 1525009154U)
			{
				if (num <= 738995217U)
				{
					if (num <= 190718801U)
					{
						if (num != 177881879U)
						{
							if (num == 190718801U)
							{
								if (name == "ADVENTURE_ID")
								{
									return typeof(int);
								}
							}
						}
						else if (name == "DUNGEON_CRAWL_DISPLAY_HERO_WINS_PER_CHAPTER")
						{
							return typeof(bool);
						}
					}
					else if (num != 370156007U)
					{
						if (num == 738995217U)
						{
							if (name == "SUBSCENE_TRANSITION_DIRECTION")
							{
								return typeof(string);
							}
						}
					}
					else if (name == "LOCKED_SHORT_DESCRIPTION")
					{
						return typeof(DbfLocValue);
					}
				}
				else if (num <= 1103584457U)
				{
					if (num != 938193592U)
					{
						if (num == 1103584457U)
						{
							if (name == "DESCRIPTION")
							{
								return typeof(DbfLocValue);
							}
						}
					}
					else if (name == "STARTING_SUBSCENE")
					{
						return typeof(AdventureData.Adventuresubscene);
					}
				}
				else if (num != 1387956774U)
				{
					if (num != 1458105184U)
					{
						if (num == 1525009154U)
						{
							if (name == "DUNGEON_CRAWL_DEFAULT_TO_DECK_FROM_UPCOMING_SCENARIO")
							{
								return typeof(bool);
							}
						}
					}
					else if (name == "ID")
					{
						return typeof(int);
					}
				}
				else if (name == "NAME")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num <= 2203352005U)
			{
				if (num <= 1789602423U)
				{
					if (num != 1567271427U)
					{
						if (num == 1789602423U)
						{
							if (name == "BOSS_CARD_BACK")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "HAS_SEEN_NEW_MODE_POPUP_OPTION")
					{
						return typeof(string);
					}
				}
				else if (num != 1954707640U)
				{
					if (num != 2011973942U)
					{
						if (num == 2203352005U)
						{
							if (name == "ADVENTURE_BOOK_REWARD_PAGE_LOCATION")
							{
								return typeof(AdventureData.Adventurebooklocation);
							}
						}
					}
					else if (name == "REWARDS_DESCRIPTION")
					{
						return typeof(DbfLocValue);
					}
				}
				else if (name == "GAME_SAVE_DATA_CLIENT_KEY")
				{
					return typeof(int);
				}
			}
			else if (num <= 2346086551U)
			{
				if (num != 2336483396U)
				{
					if (num == 2346086551U)
					{
						if (name == "DUNGEON_CRAWL_SKIP_HERO_SELECT")
						{
							return typeof(bool);
						}
					}
				}
				else if (name == "GAME_SAVE_DATA_SERVER_KEY")
				{
					return typeof(int);
				}
			}
			else if (num != 2418820992U)
			{
				if (num != 2742593543U)
				{
					if (num == 2758586749U)
					{
						if (name == "IGNORE_HERO_UNLOCK_REQUIREMENT")
						{
							return typeof(bool);
						}
					}
				}
				else if (name == "SHOW_PLAYABLE_SCENARIOS_COUNT")
				{
					return typeof(bool);
				}
			}
			else if (name == "SHORT_DESCRIPTION")
			{
				return typeof(DbfLocValue);
			}
		}
		else if (num <= 3357947825U)
		{
			if (num <= 2994298469U)
			{
				if (num <= 2832700627U)
				{
					if (num != 2794963964U)
					{
						if (num == 2832700627U)
						{
							if (name == "PREFAB_SHOWN_ON_COMPLETE")
							{
								return typeof(string);
							}
						}
					}
					else if (name == "REQUIREMENTS_DESCRIPTION")
					{
						return typeof(DbfLocValue);
					}
				}
				else if (num != 2879260603U)
				{
					if (num == 2994298469U)
					{
						if (name == "DUNGEON_CRAWL_PICK_HERO_FIRST")
						{
							return typeof(bool);
						}
					}
				}
				else if (name == "ANOMALY_MODE_DEFAULT_CARD_ID")
				{
					return typeof(int);
				}
			}
			else if (num <= 3030925245U)
			{
				if (num != 3022554311U)
				{
					if (num == 3030925245U)
					{
						if (name == "DUNGEON_CRAWL_MUST_PICK_SHRINE")
						{
							return typeof(bool);
						}
					}
				}
				else if (name == "NOTE_DESC")
				{
					return typeof(string);
				}
			}
			else if (num != 3109662305U)
			{
				if (num != 3226467965U)
				{
					if (num == 3357947825U)
					{
						if (name == "DUNGEON_CRAWL_BOSS_CARD_PREFAB")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "SHORT_NAME")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (name == "ADVENTURE_SUB_DEF_PREFAB")
			{
				return typeof(string);
			}
		}
		else if (num <= 3986679374U)
		{
			if (num <= 3731604237U)
			{
				if (num != 3511152538U)
				{
					if (num == 3731604237U)
					{
						if (name == "DUNGEON_CRAWL_IS_RETIRE_SUPPORTED")
						{
							return typeof(bool);
						}
					}
				}
				else if (name == "DUNGEON_CRAWL_SHOW_BOSS_KILL_COUNT")
				{
					return typeof(bool);
				}
			}
			else if (num != 3793523264U)
			{
				if (num != 3959141178U)
				{
					if (num == 3986679374U)
					{
						if (name == "LOCKED_DESCRIPTION")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "MODE_ID")
				{
					return typeof(int);
				}
			}
			else if (name == "ADVENTURE_BOOK_MAP_PAGE_LOCATION")
			{
				return typeof(AdventureData.Adventurebooklocation);
			}
		}
		else if (num <= 4157405553U)
		{
			if (num != 4059986364U)
			{
				if (num == 4157405553U)
				{
					if (name == "COMPLETE_BANNER_TEXT")
					{
						return typeof(DbfLocValue);
					}
				}
			}
			else if (name == "HAS_SEEN_FEATURED_MODE_OPTION")
			{
				return typeof(string);
			}
		}
		else if (num != 4214602626U)
		{
			if (num != 4219270968U)
			{
				if (num == 4221424440U)
				{
					if (name == "LOCKED_SHORT_NAME")
					{
						return typeof(DbfLocValue);
					}
				}
			}
			else if (name == "DUNGEON_CRAWL_SELECT_CHAPTER")
			{
				return typeof(bool);
			}
		}
		else if (name == "SORT_ORDER")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x060016D4 RID: 5844 RVA: 0x0007F441 File Offset: 0x0007D641
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAdventureDataDbfRecords loadRecords = new LoadAdventureDataDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060016D5 RID: 5845 RVA: 0x0007F458 File Offset: 0x0007D658
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AdventureDataDbfAsset adventureDataDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AdventureDataDbfAsset)) as AdventureDataDbfAsset;
		if (adventureDataDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AdventureDataDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < adventureDataDbfAsset.Records.Count; i++)
		{
			adventureDataDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (adventureDataDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060016D6 RID: 5846 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060016D7 RID: 5847 RVA: 0x0007F4D8 File Offset: 0x0007D6D8
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
		this.m_shortName.StripUnusedLocales();
		this.m_description.StripUnusedLocales();
		this.m_shortDescription.StripUnusedLocales();
		this.m_lockedShortName.StripUnusedLocales();
		this.m_lockedDescription.StripUnusedLocales();
		this.m_lockedShortDescription.StripUnusedLocales();
		this.m_requirementsDescription.StripUnusedLocales();
		this.m_rewardsDescription.StripUnusedLocales();
		this.m_completeBannerText.StripUnusedLocales();
	}

	// Token: 0x04000E99 RID: 3737
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04000E9A RID: 3738
	[SerializeField]
	private int m_adventureId;

	// Token: 0x04000E9B RID: 3739
	[SerializeField]
	private int m_modeId;

	// Token: 0x04000E9C RID: 3740
	[SerializeField]
	private int m_sortOrder;

	// Token: 0x04000E9D RID: 3741
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x04000E9E RID: 3742
	[SerializeField]
	private DbfLocValue m_shortName;

	// Token: 0x04000E9F RID: 3743
	[SerializeField]
	private DbfLocValue m_description;

	// Token: 0x04000EA0 RID: 3744
	[SerializeField]
	private DbfLocValue m_shortDescription;

	// Token: 0x04000EA1 RID: 3745
	[SerializeField]
	private DbfLocValue m_lockedShortName;

	// Token: 0x04000EA2 RID: 3746
	[SerializeField]
	private DbfLocValue m_lockedDescription;

	// Token: 0x04000EA3 RID: 3747
	[SerializeField]
	private DbfLocValue m_lockedShortDescription;

	// Token: 0x04000EA4 RID: 3748
	[SerializeField]
	private DbfLocValue m_requirementsDescription;

	// Token: 0x04000EA5 RID: 3749
	[SerializeField]
	private DbfLocValue m_rewardsDescription;

	// Token: 0x04000EA6 RID: 3750
	[SerializeField]
	private DbfLocValue m_completeBannerText;

	// Token: 0x04000EA7 RID: 3751
	[SerializeField]
	private bool m_showPlayableScenariosCount = true;

	// Token: 0x04000EA8 RID: 3752
	[SerializeField]
	private AdventureData.Adventuresubscene m_startingSubscene;

	// Token: 0x04000EA9 RID: 3753
	[SerializeField]
	private string m_subsceneTransitionDirection = "INVALID";

	// Token: 0x04000EAA RID: 3754
	[SerializeField]
	private string m_adventureSubDefPrefab;

	// Token: 0x04000EAB RID: 3755
	[SerializeField]
	private int m_gameSaveDataServerKeyId;

	// Token: 0x04000EAC RID: 3756
	[SerializeField]
	private int m_gameSaveDataClientKeyId;

	// Token: 0x04000EAD RID: 3757
	[SerializeField]
	private string m_dungeonCrawlBossCardPrefab;

	// Token: 0x04000EAE RID: 3758
	[SerializeField]
	private bool m_dungeonCrawlPickHeroFirst;

	// Token: 0x04000EAF RID: 3759
	[SerializeField]
	private bool m_dungeonCrawlSkipHeroSelect;

	// Token: 0x04000EB0 RID: 3760
	[SerializeField]
	private bool m_dungeonCrawlMustPickShrine;

	// Token: 0x04000EB1 RID: 3761
	[SerializeField]
	private bool m_dungeonCrawlSelectChapter;

	// Token: 0x04000EB2 RID: 3762
	[SerializeField]
	private bool m_dungeonCrawlDisplayHeroWinsPerChapter = true;

	// Token: 0x04000EB3 RID: 3763
	[SerializeField]
	private bool m_dungeonCrawlIsRetireSupported;

	// Token: 0x04000EB4 RID: 3764
	[SerializeField]
	private bool m_dungeonCrawlShowBossKillCount = true;

	// Token: 0x04000EB5 RID: 3765
	[SerializeField]
	private bool m_dungeonCrawlDefaultToDeckFromUpcomingScenario;

	// Token: 0x04000EB6 RID: 3766
	[SerializeField]
	private bool m_ignoreHeroUnlockRequirement;

	// Token: 0x04000EB7 RID: 3767
	[SerializeField]
	private int m_bossCardBackId;

	// Token: 0x04000EB8 RID: 3768
	[SerializeField]
	private string m_hasSeenFeaturedModeOption;

	// Token: 0x04000EB9 RID: 3769
	[SerializeField]
	private string m_hasSeenNewModePopupOption;

	// Token: 0x04000EBA RID: 3770
	[SerializeField]
	private string m_prefabShownOnComplete;

	// Token: 0x04000EBB RID: 3771
	[SerializeField]
	private int m_anomalyModeDefaultCardId;

	// Token: 0x04000EBC RID: 3772
	[SerializeField]
	private AdventureData.Adventurebooklocation m_adventureBookMapPageLocation = AdventureData.ParseAdventurebooklocationValue("Beginning");

	// Token: 0x04000EBD RID: 3773
	[SerializeField]
	private AdventureData.Adventurebooklocation m_adventureBookRewardPageLocation = AdventureData.ParseAdventurebooklocationValue("End");
}
