using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200014A RID: 330
[Serializable]
public class AchieveDbfRecord : DbfRecord
{
	// Token: 0x17000101 RID: 257
	// (get) Token: 0x06001550 RID: 5456 RVA: 0x00079812 File Offset: 0x00077A12
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x06001551 RID: 5457 RVA: 0x0007981A File Offset: 0x00077A1A
	[DbfField("ACH_TYPE")]
	public Achieve.Type AchType
	{
		get
		{
			return this.m_achType;
		}
	}

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x06001552 RID: 5458 RVA: 0x00079822 File Offset: 0x00077A22
	[DbfField("ENABLED")]
	public bool Enabled
	{
		get
		{
			return this.m_enabled;
		}
	}

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06001553 RID: 5459 RVA: 0x0007982A File Offset: 0x00077A2A
	[DbfField("PARENT_ACH")]
	public string ParentAch
	{
		get
		{
			return this.m_parentAch;
		}
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06001554 RID: 5460 RVA: 0x00079832 File Offset: 0x00077A32
	[DbfField("LINK_TO")]
	public string LinkTo
	{
		get
		{
			return this.m_linkTo;
		}
	}

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x06001555 RID: 5461 RVA: 0x0007983A File Offset: 0x00077A3A
	[DbfField("SHARED_ACHIEVE_ID")]
	public int SharedAchieveId
	{
		get
		{
			return this.m_sharedAchieveId;
		}
	}

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06001556 RID: 5462 RVA: 0x00079842 File Offset: 0x00077A42
	public AchieveDbfRecord SharedAchieveRecord
	{
		get
		{
			return GameDbf.Achieve.GetRecord(this.m_sharedAchieveId);
		}
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06001557 RID: 5463 RVA: 0x00079854 File Offset: 0x00077A54
	[DbfField("CLIENT_FLAGS")]
	public Achieve.ClientFlags ClientFlags
	{
		get
		{
			return this.m_clientFlags;
		}
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06001558 RID: 5464 RVA: 0x0007985C File Offset: 0x00077A5C
	[DbfField("TRIGGERED")]
	public Achieve.Trigger Triggered
	{
		get
		{
			return this.m_triggered;
		}
	}

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x06001559 RID: 5465 RVA: 0x00079864 File Offset: 0x00077A64
	[DbfField("ACH_QUOTA")]
	public int AchQuota
	{
		get
		{
			return this.m_achQuota;
		}
	}

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x0600155A RID: 5466 RVA: 0x0007986C File Offset: 0x00077A6C
	[DbfField("GAME_MODE")]
	public Achieve.GameMode GameMode
	{
		get
		{
			return this.m_gameMode;
		}
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x0600155B RID: 5467 RVA: 0x00079874 File Offset: 0x00077A74
	[DbfField("RACE")]
	public int Race
	{
		get
		{
			return this.m_raceId;
		}
	}

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x0600155C RID: 5468 RVA: 0x0007987C File Offset: 0x00077A7C
	[DbfField("CARD_SET")]
	public int CardSet
	{
		get
		{
			return this.m_cardSetId;
		}
	}

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x0600155D RID: 5469 RVA: 0x00079884 File Offset: 0x00077A84
	public CardSetDbfRecord CardSetRecord
	{
		get
		{
			return GameDbf.CardSet.GetRecord(this.m_cardSetId);
		}
	}

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x0600155E RID: 5470 RVA: 0x00079896 File Offset: 0x00077A96
	[DbfField("MY_HERO_CLASS_ID")]
	public int MyHeroClassId
	{
		get
		{
			return this.m_myHeroClassId;
		}
	}

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x0600155F RID: 5471 RVA: 0x0007989E File Offset: 0x00077A9E
	public ClassDbfRecord MyHeroClassRecord
	{
		get
		{
			return GameDbf.Class.GetRecord(this.m_myHeroClassId);
		}
	}

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x06001560 RID: 5472 RVA: 0x000798B0 File Offset: 0x00077AB0
	[DbfField("ENEMY_HERO_CLASS_ID")]
	public int EnemyHeroClassId
	{
		get
		{
			return this.m_enemyHeroClassId;
		}
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x06001561 RID: 5473 RVA: 0x000798B8 File Offset: 0x00077AB8
	public ClassDbfRecord EnemyHeroClassRecord
	{
		get
		{
			return GameDbf.Class.GetRecord(this.m_enemyHeroClassId);
		}
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x06001562 RID: 5474 RVA: 0x000798CA File Offset: 0x00077ACA
	[DbfField("MAX_DEFENSE")]
	public int MaxDefense
	{
		get
		{
			return this.m_maxDefense;
		}
	}

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x06001563 RID: 5475 RVA: 0x000798D2 File Offset: 0x00077AD2
	[DbfField("PLAYER_TYPE")]
	public Achieve.PlayerType PlayerType
	{
		get
		{
			return this.m_playerType;
		}
	}

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x06001564 RID: 5476 RVA: 0x000798DA File Offset: 0x00077ADA
	[DbfField("LEAGUE_VERSION_MIN")]
	public int LeagueVersionMin
	{
		get
		{
			return this.m_leagueVersionMin;
		}
	}

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06001565 RID: 5477 RVA: 0x000798E2 File Offset: 0x00077AE2
	[DbfField("LEAGUE_VERSION_MAX")]
	public int LeagueVersionMax
	{
		get
		{
			return this.m_leagueVersionMax;
		}
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06001566 RID: 5478 RVA: 0x000798EA File Offset: 0x00077AEA
	[Obsolete("Use ACHIEVE_CONDITION.SCENARIO_ID instead")]
	[DbfField("SCENARIO_ID")]
	public int ScenarioId
	{
		get
		{
			return this.m_scenarioId;
		}
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06001567 RID: 5479 RVA: 0x000798F2 File Offset: 0x00077AF2
	public ScenarioDbfRecord ScenarioRecord
	{
		get
		{
			return GameDbf.Scenario.GetRecord(this.m_scenarioId);
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x06001568 RID: 5480 RVA: 0x00079904 File Offset: 0x00077B04
	[DbfField("ADVENTURE_ID")]
	public int AdventureId
	{
		get
		{
			return this.m_adventureId;
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x06001569 RID: 5481 RVA: 0x0007990C File Offset: 0x00077B0C
	public AdventureDbfRecord AdventureRecord
	{
		get
		{
			return GameDbf.Adventure.GetRecord(this.m_adventureId);
		}
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x0600156A RID: 5482 RVA: 0x0007991E File Offset: 0x00077B1E
	[DbfField("ADVENTURE_MODE_ID")]
	public int AdventureModeId
	{
		get
		{
			return this.m_adventureModeId;
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x0600156B RID: 5483 RVA: 0x00079926 File Offset: 0x00077B26
	public AdventureModeDbfRecord AdventureModeRecord
	{
		get
		{
			return GameDbf.AdventureMode.GetRecord(this.m_adventureModeId);
		}
	}

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x0600156C RID: 5484 RVA: 0x00079938 File Offset: 0x00077B38
	[DbfField("ADVENTURE_WING_ID")]
	public int AdventureWingId
	{
		get
		{
			return this.m_adventureWingId;
		}
	}

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x0600156D RID: 5485 RVA: 0x00079940 File Offset: 0x00077B40
	public WingDbfRecord AdventureWingRecord
	{
		get
		{
			return GameDbf.Wing.GetRecord(this.m_adventureWingId);
		}
	}

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x0600156E RID: 5486 RVA: 0x00079952 File Offset: 0x00077B52
	[DbfField("BOOSTER")]
	public int Booster
	{
		get
		{
			return this.m_boosterId;
		}
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x0600156F RID: 5487 RVA: 0x0007995A File Offset: 0x00077B5A
	public BoosterDbfRecord BoosterRecord
	{
		get
		{
			return GameDbf.Booster.GetRecord(this.m_boosterId);
		}
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x06001570 RID: 5488 RVA: 0x0007996C File Offset: 0x00077B6C
	[DbfField("REWARD_TIMING")]
	public Achieve.RewardTiming RewardTiming
	{
		get
		{
			return this.m_rewardTiming;
		}
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x06001571 RID: 5489 RVA: 0x00079974 File Offset: 0x00077B74
	[DbfField("REWARD")]
	public string Reward
	{
		get
		{
			return this.m_reward;
		}
	}

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x06001572 RID: 5490 RVA: 0x0007997C File Offset: 0x00077B7C
	[DbfField("REWARD_DATA1")]
	public long RewardData1
	{
		get
		{
			return this.m_rewardData1;
		}
	}

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x06001573 RID: 5491 RVA: 0x00079984 File Offset: 0x00077B84
	[DbfField("REWARD_DATA2")]
	public long RewardData2
	{
		get
		{
			return this.m_rewardData2;
		}
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x06001574 RID: 5492 RVA: 0x0007998C File Offset: 0x00077B8C
	[DbfField("UNLOCKS")]
	public Achieve.Unlocks Unlocks
	{
		get
		{
			return this.m_unlocks;
		}
	}

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x06001575 RID: 5493 RVA: 0x00079994 File Offset: 0x00077B94
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06001576 RID: 5494 RVA: 0x0007999C File Offset: 0x00077B9C
	[DbfField("DESCRIPTION")]
	public DbfLocValue Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x17000128 RID: 296
	// (get) Token: 0x06001577 RID: 5495 RVA: 0x000799A4 File Offset: 0x00077BA4
	[DbfField("ALT_TEXT_PREDICATE")]
	public Achieve.AltTextPredicate AltTextPredicate
	{
		get
		{
			return this.m_altTextPredicate;
		}
	}

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x06001578 RID: 5496 RVA: 0x000799AC File Offset: 0x00077BAC
	[DbfField("ALT_NAME")]
	public DbfLocValue AltName
	{
		get
		{
			return this.m_altName;
		}
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06001579 RID: 5497 RVA: 0x000799B4 File Offset: 0x00077BB4
	[DbfField("ALT_DESCRIPTION")]
	public DbfLocValue AltDescription
	{
		get
		{
			return this.m_altDescription;
		}
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x0600157A RID: 5498 RVA: 0x000799BC File Offset: 0x00077BBC
	[DbfField("CUSTOM_VISUAL_WIDGET")]
	public string CustomVisualWidget
	{
		get
		{
			return this.m_customVisualWidget;
		}
	}

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x0600157B RID: 5499 RVA: 0x000799C4 File Offset: 0x00077BC4
	[DbfField("USE_GENERIC_REWARD_VISUAL")]
	public bool UseGenericRewardVisual
	{
		get
		{
			return this.m_useGenericRewardVisual;
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x0600157C RID: 5500 RVA: 0x000799CC File Offset: 0x00077BCC
	[DbfField("SHOW_TO_RETURNING_PLAYER")]
	public Achieve.ShowToReturningPlayer ShowToReturningPlayer
	{
		get
		{
			return this.m_showToReturningPlayer;
		}
	}

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x0600157D RID: 5501 RVA: 0x000799D4 File Offset: 0x00077BD4
	[DbfField("QUEST_DIALOG_ID")]
	public int QuestDialogId
	{
		get
		{
			return this.m_questDialogId;
		}
	}

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x0600157E RID: 5502 RVA: 0x000799DC File Offset: 0x00077BDC
	public CharacterDialogDbfRecord QuestDialogRecord
	{
		get
		{
			return GameDbf.CharacterDialog.GetRecord(this.m_questDialogId);
		}
	}

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x0600157F RID: 5503 RVA: 0x000799EE File Offset: 0x00077BEE
	[DbfField("AUTO_DESTROY")]
	public bool AutoDestroy
	{
		get
		{
			return this.m_autoDestroy;
		}
	}

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x06001580 RID: 5504 RVA: 0x000799F6 File Offset: 0x00077BF6
	[DbfField("QUEST_TILE_PREFAB")]
	public string QuestTilePrefab
	{
		get
		{
			return this.m_questTilePrefab;
		}
	}

	// Token: 0x17000132 RID: 306
	// (get) Token: 0x06001581 RID: 5505 RVA: 0x000799FE File Offset: 0x00077BFE
	[DbfField("ATTENTION_BLOCKER")]
	public Achieve.AttentionBlocker AttentionBlocker
	{
		get
		{
			return this.m_attentionBlocker;
		}
	}

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x06001582 RID: 5506 RVA: 0x00079A06 File Offset: 0x00077C06
	[DbfField("ENABLED_WITH_PROGRESSION")]
	public bool EnabledWithProgression
	{
		get
		{
			return this.m_enabledWithProgression;
		}
	}

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x06001583 RID: 5507 RVA: 0x00079A0E File Offset: 0x00077C0E
	public List<AchieveConditionDbfRecord> Conditions
	{
		get
		{
			return GameDbf.AchieveCondition.GetRecords((AchieveConditionDbfRecord r) => r.AchieveId == base.ID, -1);
		}
	}

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x06001584 RID: 5508 RVA: 0x00079A27 File Offset: 0x00077C27
	public List<AchieveRegionDataDbfRecord> RegionDataList
	{
		get
		{
			return GameDbf.AchieveRegionData.GetRecords((AchieveRegionDataDbfRecord r) => r.AchieveId == base.ID, -1);
		}
	}

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x06001585 RID: 5509 RVA: 0x00079A40 File Offset: 0x00077C40
	public List<VisualBlacklistDbfRecord> VisualBlacklist
	{
		get
		{
			return GameDbf.VisualBlacklist.GetRecords((VisualBlacklistDbfRecord r) => r.AchieveId == base.ID, -1);
		}
	}

	// Token: 0x06001586 RID: 5510 RVA: 0x00079A59 File Offset: 0x00077C59
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001587 RID: 5511 RVA: 0x00079A62 File Offset: 0x00077C62
	public void SetAchType(Achieve.Type v)
	{
		this.m_achType = v;
	}

	// Token: 0x06001588 RID: 5512 RVA: 0x00079A6B File Offset: 0x00077C6B
	public void SetEnabled(bool v)
	{
		this.m_enabled = v;
	}

	// Token: 0x06001589 RID: 5513 RVA: 0x00079A74 File Offset: 0x00077C74
	public void SetParentAch(string v)
	{
		this.m_parentAch = v;
	}

	// Token: 0x0600158A RID: 5514 RVA: 0x00079A7D File Offset: 0x00077C7D
	public void SetLinkTo(string v)
	{
		this.m_linkTo = v;
	}

	// Token: 0x0600158B RID: 5515 RVA: 0x00079A86 File Offset: 0x00077C86
	public void SetSharedAchieveId(int v)
	{
		this.m_sharedAchieveId = v;
	}

	// Token: 0x0600158C RID: 5516 RVA: 0x00079A8F File Offset: 0x00077C8F
	public void SetClientFlags(Achieve.ClientFlags v)
	{
		this.m_clientFlags = v;
	}

	// Token: 0x0600158D RID: 5517 RVA: 0x00079A98 File Offset: 0x00077C98
	public void SetTriggered(Achieve.Trigger v)
	{
		this.m_triggered = v;
	}

	// Token: 0x0600158E RID: 5518 RVA: 0x00079AA1 File Offset: 0x00077CA1
	public void SetAchQuota(int v)
	{
		this.m_achQuota = v;
	}

	// Token: 0x0600158F RID: 5519 RVA: 0x00079AAA File Offset: 0x00077CAA
	public void SetGameMode(Achieve.GameMode v)
	{
		this.m_gameMode = v;
	}

	// Token: 0x06001590 RID: 5520 RVA: 0x00079AB3 File Offset: 0x00077CB3
	public void SetRace(int v)
	{
		this.m_raceId = v;
	}

	// Token: 0x06001591 RID: 5521 RVA: 0x00079ABC File Offset: 0x00077CBC
	public void SetCardSet(int v)
	{
		this.m_cardSetId = v;
	}

	// Token: 0x06001592 RID: 5522 RVA: 0x00079AC5 File Offset: 0x00077CC5
	public void SetMyHeroClassId(int v)
	{
		this.m_myHeroClassId = v;
	}

	// Token: 0x06001593 RID: 5523 RVA: 0x00079ACE File Offset: 0x00077CCE
	public void SetEnemyHeroClassId(int v)
	{
		this.m_enemyHeroClassId = v;
	}

	// Token: 0x06001594 RID: 5524 RVA: 0x00079AD7 File Offset: 0x00077CD7
	public void SetMaxDefense(int v)
	{
		this.m_maxDefense = v;
	}

	// Token: 0x06001595 RID: 5525 RVA: 0x00079AE0 File Offset: 0x00077CE0
	public void SetPlayerType(Achieve.PlayerType v)
	{
		this.m_playerType = v;
	}

	// Token: 0x06001596 RID: 5526 RVA: 0x00079AE9 File Offset: 0x00077CE9
	public void SetLeagueVersionMin(int v)
	{
		this.m_leagueVersionMin = v;
	}

	// Token: 0x06001597 RID: 5527 RVA: 0x00079AF2 File Offset: 0x00077CF2
	public void SetLeagueVersionMax(int v)
	{
		this.m_leagueVersionMax = v;
	}

	// Token: 0x06001598 RID: 5528 RVA: 0x00079AFB File Offset: 0x00077CFB
	[Obsolete("Use ACHIEVE_CONDITION.SCENARIO_ID instead")]
	public void SetScenarioId(int v)
	{
		this.m_scenarioId = v;
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x00079B04 File Offset: 0x00077D04
	public void SetAdventureId(int v)
	{
		this.m_adventureId = v;
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x00079B0D File Offset: 0x00077D0D
	public void SetAdventureModeId(int v)
	{
		this.m_adventureModeId = v;
	}

	// Token: 0x0600159B RID: 5531 RVA: 0x00079B16 File Offset: 0x00077D16
	public void SetAdventureWingId(int v)
	{
		this.m_adventureWingId = v;
	}

	// Token: 0x0600159C RID: 5532 RVA: 0x00079B1F File Offset: 0x00077D1F
	public void SetBooster(int v)
	{
		this.m_boosterId = v;
	}

	// Token: 0x0600159D RID: 5533 RVA: 0x00079B28 File Offset: 0x00077D28
	public void SetRewardTiming(Achieve.RewardTiming v)
	{
		this.m_rewardTiming = v;
	}

	// Token: 0x0600159E RID: 5534 RVA: 0x00079B31 File Offset: 0x00077D31
	public void SetReward(string v)
	{
		this.m_reward = v;
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x00079B3A File Offset: 0x00077D3A
	public void SetRewardData1(long v)
	{
		this.m_rewardData1 = v;
	}

	// Token: 0x060015A0 RID: 5536 RVA: 0x00079B43 File Offset: 0x00077D43
	public void SetRewardData2(long v)
	{
		this.m_rewardData2 = v;
	}

	// Token: 0x060015A1 RID: 5537 RVA: 0x00079B4C File Offset: 0x00077D4C
	public void SetUnlocks(Achieve.Unlocks v)
	{
		this.m_unlocks = v;
	}

	// Token: 0x060015A2 RID: 5538 RVA: 0x00079B55 File Offset: 0x00077D55
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x060015A3 RID: 5539 RVA: 0x00079B6F File Offset: 0x00077D6F
	public void SetDescription(DbfLocValue v)
	{
		this.m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	// Token: 0x060015A4 RID: 5540 RVA: 0x00079B89 File Offset: 0x00077D89
	public void SetAltTextPredicate(Achieve.AltTextPredicate v)
	{
		this.m_altTextPredicate = v;
	}

	// Token: 0x060015A5 RID: 5541 RVA: 0x00079B92 File Offset: 0x00077D92
	public void SetAltName(DbfLocValue v)
	{
		this.m_altName = v;
		v.SetDebugInfo(base.ID, "ALT_NAME");
	}

	// Token: 0x060015A6 RID: 5542 RVA: 0x00079BAC File Offset: 0x00077DAC
	public void SetAltDescription(DbfLocValue v)
	{
		this.m_altDescription = v;
		v.SetDebugInfo(base.ID, "ALT_DESCRIPTION");
	}

	// Token: 0x060015A7 RID: 5543 RVA: 0x00079BC6 File Offset: 0x00077DC6
	public void SetCustomVisualWidget(string v)
	{
		this.m_customVisualWidget = v;
	}

	// Token: 0x060015A8 RID: 5544 RVA: 0x00079BCF File Offset: 0x00077DCF
	public void SetUseGenericRewardVisual(bool v)
	{
		this.m_useGenericRewardVisual = v;
	}

	// Token: 0x060015A9 RID: 5545 RVA: 0x00079BD8 File Offset: 0x00077DD8
	public void SetShowToReturningPlayer(Achieve.ShowToReturningPlayer v)
	{
		this.m_showToReturningPlayer = v;
	}

	// Token: 0x060015AA RID: 5546 RVA: 0x00079BE1 File Offset: 0x00077DE1
	public void SetQuestDialogId(int v)
	{
		this.m_questDialogId = v;
	}

	// Token: 0x060015AB RID: 5547 RVA: 0x00079BEA File Offset: 0x00077DEA
	public void SetAutoDestroy(bool v)
	{
		this.m_autoDestroy = v;
	}

	// Token: 0x060015AC RID: 5548 RVA: 0x00079BF3 File Offset: 0x00077DF3
	public void SetQuestTilePrefab(string v)
	{
		this.m_questTilePrefab = v;
	}

	// Token: 0x060015AD RID: 5549 RVA: 0x00079BFC File Offset: 0x00077DFC
	public void SetAttentionBlocker(Achieve.AttentionBlocker v)
	{
		this.m_attentionBlocker = v;
	}

	// Token: 0x060015AE RID: 5550 RVA: 0x00079C05 File Offset: 0x00077E05
	public void SetEnabledWithProgression(bool v)
	{
		this.m_enabledWithProgression = v;
	}

	// Token: 0x060015AF RID: 5551 RVA: 0x00079C10 File Offset: 0x00077E10
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2013650947U)
		{
			if (num <= 902917601U)
			{
				if (num <= 655598188U)
				{
					if (num <= 190718801U)
					{
						if (num != 190375133U)
						{
							if (num == 190718801U)
							{
								if (name == "ADVENTURE_ID")
								{
									return this.m_adventureId;
								}
							}
						}
						else if (name == "ADVENTURE_MODE_ID")
						{
							return this.m_adventureModeId;
						}
					}
					else if (num != 378746893U)
					{
						if (num != 493513528U)
						{
							if (num == 655598188U)
							{
								if (name == "LEAGUE_VERSION_MIN")
								{
									return this.m_leagueVersionMin;
								}
							}
						}
						else if (name == "ACH_TYPE")
						{
							return this.m_achType;
						}
					}
					else if (name == "USE_GENERIC_REWARD_VISUAL")
					{
						return this.m_useGenericRewardVisual;
					}
				}
				else if (num <= 693605261U)
				{
					if (num != 679831291U)
					{
						if (num == 693605261U)
						{
							if (name == "SCENARIO_ID")
							{
								return this.m_scenarioId;
							}
						}
					}
					else if (name == "BOOSTER")
					{
						return this.m_boosterId;
					}
				}
				else if (num != 787754026U)
				{
					if (num != 829736880U)
					{
						if (num == 902917601U)
						{
							if (name == "MY_HERO_CLASS_ID")
							{
								return this.m_myHeroClassId;
							}
						}
					}
					else if (name == "CARD_SET")
					{
						return this.m_cardSetId;
					}
				}
				else if (name == "CLIENT_FLAGS")
				{
					return this.m_clientFlags;
				}
			}
			else if (num <= 1458105184U)
			{
				if (num <= 1145534743U)
				{
					if (num != 1103584457U)
					{
						if (num == 1145534743U)
						{
							if (name == "LINK_TO")
							{
								return this.m_linkTo;
							}
						}
					}
					else if (name == "DESCRIPTION")
					{
						return this.m_description;
					}
				}
				else if (num != 1387956774U)
				{
					if (num != 1421814995U)
					{
						if (num == 1458105184U)
						{
							if (name == "ID")
							{
								return base.ID;
							}
						}
					}
					else if (name == "AUTO_DESTROY")
					{
						return this.m_autoDestroy;
					}
				}
				else if (name == "NAME")
				{
					return this.m_name;
				}
			}
			else if (num <= 1638382104U)
			{
				if (num != 1521075980U)
				{
					if (num != 1629023597U)
					{
						if (num == 1638382104U)
						{
							if (name == "ACH_QUOTA")
							{
								return this.m_achQuota;
							}
						}
					}
					else if (name == "ALT_DESCRIPTION")
					{
						return this.m_altDescription;
					}
				}
				else if (name == "ATTENTION_BLOCKER")
				{
					return this.m_attentionBlocker;
				}
			}
			else if (num != 1832324731U)
			{
				if (num != 1984182043U)
				{
					if (num == 2013650947U)
					{
						if (name == "ENABLED_WITH_PROGRESSION")
						{
							return this.m_enabledWithProgression;
						}
					}
				}
				else if (name == "GAME_MODE")
				{
					return this.m_gameMode;
				}
			}
			else if (name == "SHOW_TO_RETURNING_PLAYER")
			{
				return this.m_showToReturningPlayer;
			}
		}
		else if (num <= 2967890242U)
		{
			if (num <= 2349802968U)
			{
				if (num <= 2099820198U)
				{
					if (num != 2076288754U)
					{
						if (num == 2099820198U)
						{
							if (name == "MAX_DEFENSE")
							{
								return this.m_maxDefense;
							}
						}
					}
					else if (name == "UNLOCKS")
					{
						return this.m_unlocks;
					}
				}
				else if (num != 2222469004U)
				{
					if (num != 2294480894U)
					{
						if (num == 2349802968U)
						{
							if (name == "RACE")
							{
								return this.m_raceId;
							}
						}
					}
					else if (name == "ENABLED")
					{
						return this.m_enabled;
					}
				}
				else if (name == "CUSTOM_VISUAL_WIDGET")
				{
					return this.m_customVisualWidget;
				}
			}
			else if (num <= 2537485753U)
			{
				if (num != 2513575373U)
				{
					if (num == 2537485753U)
					{
						if (name == "REWARD_TIMING")
						{
							return this.m_rewardTiming;
						}
					}
				}
				else if (name == "ADVENTURE_WING_ID")
				{
					return this.m_adventureWingId;
				}
			}
			else if (num != 2851436049U)
			{
				if (num != 2951112623U)
				{
					if (num == 2967890242U)
					{
						if (name == "REWARD_DATA1")
						{
							return this.m_rewardData1;
						}
					}
				}
				else if (name == "REWARD_DATA2")
				{
					return this.m_rewardData2;
				}
			}
			else if (name == "QUEST_TILE_PREFAB")
			{
				return this.m_questTilePrefab;
			}
		}
		else if (num <= 3638028361U)
		{
			if (num <= 3082479862U)
			{
				if (num != 3022554311U)
				{
					if (num == 3082479862U)
					{
						if (name == "QUEST_DIALOG_ID")
						{
							return this.m_questDialogId;
						}
					}
				}
				else if (name == "NOTE_DESC")
				{
					return this.m_noteDesc;
				}
			}
			else if (num != 3403422626U)
			{
				if (num != 3531042190U)
				{
					if (num == 3638028361U)
					{
						if (name == "ENEMY_HERO_CLASS_ID")
						{
							return this.m_enemyHeroClassId;
						}
					}
				}
				else if (name == "PARENT_ACH")
				{
					return this.m_parentAch;
				}
			}
			else if (name == "TRIGGERED")
			{
				return this.m_triggered;
			}
		}
		else if (num <= 4057912964U)
		{
			if (num != 3661704248U)
			{
				if (num != 3714292274U)
				{
					if (num == 4057912964U)
					{
						if (name == "ALT_TEXT_PREDICATE")
						{
							return this.m_altTextPredicate;
						}
					}
				}
				else if (name == "ALT_NAME")
				{
					return this.m_altName;
				}
			}
			else if (name == "SHARED_ACHIEVE_ID")
			{
				return this.m_sharedAchieveId;
			}
		}
		else if (num != 4110404606U)
		{
			if (num != 4274580549U)
			{
				if (num == 4286938522U)
				{
					if (name == "REWARD")
					{
						return this.m_reward;
					}
				}
			}
			else if (name == "PLAYER_TYPE")
			{
				return this.m_playerType;
			}
		}
		else if (name == "LEAGUE_VERSION_MAX")
		{
			return this.m_leagueVersionMax;
		}
		return null;
	}

	// Token: 0x060015B0 RID: 5552 RVA: 0x0007A3FC File Offset: 0x000785FC
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2013650947U)
		{
			if (num <= 902917601U)
			{
				if (num <= 655598188U)
				{
					if (num <= 190718801U)
					{
						if (num != 190375133U)
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
							if (!(name == "ADVENTURE_MODE_ID"))
							{
								return;
							}
							this.m_adventureModeId = (int)val;
							return;
						}
					}
					else if (num != 378746893U)
					{
						if (num != 493513528U)
						{
							if (num != 655598188U)
							{
								return;
							}
							if (!(name == "LEAGUE_VERSION_MIN"))
							{
								return;
							}
							this.m_leagueVersionMin = (int)val;
							return;
						}
						else
						{
							if (!(name == "ACH_TYPE"))
							{
								return;
							}
							if (val == null)
							{
								this.m_achType = Achieve.Type.INVALID;
								return;
							}
							if (val is Achieve.Type || val is int)
							{
								this.m_achType = (Achieve.Type)val;
								return;
							}
							if (val is string)
							{
								this.m_achType = Achieve.ParseTypeValue((string)val);
								return;
							}
						}
					}
					else
					{
						if (!(name == "USE_GENERIC_REWARD_VISUAL"))
						{
							return;
						}
						this.m_useGenericRewardVisual = (bool)val;
						return;
					}
				}
				else if (num <= 693605261U)
				{
					if (num != 679831291U)
					{
						if (num != 693605261U)
						{
							return;
						}
						if (!(name == "SCENARIO_ID"))
						{
							return;
						}
						this.m_scenarioId = (int)val;
						return;
					}
					else
					{
						if (!(name == "BOOSTER"))
						{
							return;
						}
						this.m_boosterId = (int)val;
						return;
					}
				}
				else if (num != 787754026U)
				{
					if (num != 829736880U)
					{
						if (num != 902917601U)
						{
							return;
						}
						if (!(name == "MY_HERO_CLASS_ID"))
						{
							return;
						}
						this.m_myHeroClassId = (int)val;
						return;
					}
					else
					{
						if (!(name == "CARD_SET"))
						{
							return;
						}
						this.m_cardSetId = (int)val;
						return;
					}
				}
				else
				{
					if (!(name == "CLIENT_FLAGS"))
					{
						return;
					}
					if (val == null)
					{
						this.m_clientFlags = Achieve.ClientFlags.NONE;
						return;
					}
					if (val is Achieve.ClientFlags || val is int)
					{
						this.m_clientFlags = (Achieve.ClientFlags)val;
						return;
					}
					if (val is string)
					{
						this.m_clientFlags = Achieve.ParseClientFlagsValue((string)val);
						return;
					}
				}
			}
			else if (num <= 1458105184U)
			{
				if (num <= 1145534743U)
				{
					if (num != 1103584457U)
					{
						if (num != 1145534743U)
						{
							return;
						}
						if (!(name == "LINK_TO"))
						{
							return;
						}
						this.m_linkTo = (string)val;
						return;
					}
					else
					{
						if (!(name == "DESCRIPTION"))
						{
							return;
						}
						this.m_description = (DbfLocValue)val;
						return;
					}
				}
				else if (num != 1387956774U)
				{
					if (num != 1421814995U)
					{
						if (num != 1458105184U)
						{
							return;
						}
						if (!(name == "ID"))
						{
							return;
						}
						base.SetID((int)val);
						return;
					}
					else
					{
						if (!(name == "AUTO_DESTROY"))
						{
							return;
						}
						this.m_autoDestroy = (bool)val;
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
			else if (num <= 1638382104U)
			{
				if (num != 1521075980U)
				{
					if (num != 1629023597U)
					{
						if (num != 1638382104U)
						{
							return;
						}
						if (!(name == "ACH_QUOTA"))
						{
							return;
						}
						this.m_achQuota = (int)val;
						return;
					}
					else
					{
						if (!(name == "ALT_DESCRIPTION"))
						{
							return;
						}
						this.m_altDescription = (DbfLocValue)val;
						return;
					}
				}
				else
				{
					if (!(name == "ATTENTION_BLOCKER"))
					{
						return;
					}
					if (val == null)
					{
						this.m_attentionBlocker = Achieve.AttentionBlocker.NONE;
						return;
					}
					if (val is Achieve.AttentionBlocker || val is int)
					{
						this.m_attentionBlocker = (Achieve.AttentionBlocker)val;
						return;
					}
					if (val is string)
					{
						this.m_attentionBlocker = Achieve.ParseAttentionBlockerValue((string)val);
						return;
					}
				}
			}
			else if (num != 1832324731U)
			{
				if (num != 1984182043U)
				{
					if (num != 2013650947U)
					{
						return;
					}
					if (!(name == "ENABLED_WITH_PROGRESSION"))
					{
						return;
					}
					this.m_enabledWithProgression = (bool)val;
				}
				else
				{
					if (!(name == "GAME_MODE"))
					{
						return;
					}
					if (val == null)
					{
						this.m_gameMode = Achieve.GameMode.ANY;
						return;
					}
					if (val is Achieve.GameMode || val is int)
					{
						this.m_gameMode = (Achieve.GameMode)val;
						return;
					}
					if (val is string)
					{
						this.m_gameMode = Achieve.ParseGameModeValue((string)val);
						return;
					}
				}
			}
			else
			{
				if (!(name == "SHOW_TO_RETURNING_PLAYER"))
				{
					return;
				}
				if (val == null)
				{
					this.m_showToReturningPlayer = Achieve.ShowToReturningPlayer.ALWAYS;
					return;
				}
				if (val is Achieve.ShowToReturningPlayer || val is int)
				{
					this.m_showToReturningPlayer = (Achieve.ShowToReturningPlayer)val;
					return;
				}
				if (val is string)
				{
					this.m_showToReturningPlayer = Achieve.ParseShowToReturningPlayerValue((string)val);
					return;
				}
			}
		}
		else if (num <= 2967890242U)
		{
			if (num <= 2349802968U)
			{
				if (num <= 2099820198U)
				{
					if (num != 2076288754U)
					{
						if (num != 2099820198U)
						{
							return;
						}
						if (!(name == "MAX_DEFENSE"))
						{
							return;
						}
						this.m_maxDefense = (int)val;
						return;
					}
					else
					{
						if (!(name == "UNLOCKS"))
						{
							return;
						}
						if (val == null)
						{
							this.m_unlocks = Achieve.Unlocks.FORGE;
							return;
						}
						if (val is Achieve.Unlocks || val is int)
						{
							this.m_unlocks = (Achieve.Unlocks)val;
							return;
						}
						if (val is string)
						{
							this.m_unlocks = Achieve.ParseUnlocksValue((string)val);
							return;
						}
					}
				}
				else if (num != 2222469004U)
				{
					if (num != 2294480894U)
					{
						if (num != 2349802968U)
						{
							return;
						}
						if (!(name == "RACE"))
						{
							return;
						}
						this.m_raceId = (int)val;
						return;
					}
					else
					{
						if (!(name == "ENABLED"))
						{
							return;
						}
						this.m_enabled = (bool)val;
						return;
					}
				}
				else
				{
					if (!(name == "CUSTOM_VISUAL_WIDGET"))
					{
						return;
					}
					this.m_customVisualWidget = (string)val;
					return;
				}
			}
			else if (num <= 2537485753U)
			{
				if (num != 2513575373U)
				{
					if (num != 2537485753U)
					{
						return;
					}
					if (!(name == "REWARD_TIMING"))
					{
						return;
					}
					if (val == null)
					{
						this.m_rewardTiming = Achieve.RewardTiming.IMMEDIATE;
						return;
					}
					if (val is Achieve.RewardTiming || val is int)
					{
						this.m_rewardTiming = (Achieve.RewardTiming)val;
						return;
					}
					if (val is string)
					{
						this.m_rewardTiming = Achieve.ParseRewardTimingValue((string)val);
						return;
					}
				}
				else
				{
					if (!(name == "ADVENTURE_WING_ID"))
					{
						return;
					}
					this.m_adventureWingId = (int)val;
					return;
				}
			}
			else if (num != 2851436049U)
			{
				if (num != 2951112623U)
				{
					if (num != 2967890242U)
					{
						return;
					}
					if (!(name == "REWARD_DATA1"))
					{
						return;
					}
					this.m_rewardData1 = (long)val;
					return;
				}
				else
				{
					if (!(name == "REWARD_DATA2"))
					{
						return;
					}
					this.m_rewardData2 = (long)val;
					return;
				}
			}
			else
			{
				if (!(name == "QUEST_TILE_PREFAB"))
				{
					return;
				}
				this.m_questTilePrefab = (string)val;
				return;
			}
		}
		else if (num <= 3638028361U)
		{
			if (num <= 3082479862U)
			{
				if (num != 3022554311U)
				{
					if (num != 3082479862U)
					{
						return;
					}
					if (!(name == "QUEST_DIALOG_ID"))
					{
						return;
					}
					this.m_questDialogId = (int)val;
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
			else if (num != 3403422626U)
			{
				if (num != 3531042190U)
				{
					if (num != 3638028361U)
					{
						return;
					}
					if (!(name == "ENEMY_HERO_CLASS_ID"))
					{
						return;
					}
					this.m_enemyHeroClassId = (int)val;
					return;
				}
				else
				{
					if (!(name == "PARENT_ACH"))
					{
						return;
					}
					this.m_parentAch = (string)val;
					return;
				}
			}
			else
			{
				if (!(name == "TRIGGERED"))
				{
					return;
				}
				if (val == null)
				{
					this.m_triggered = Achieve.Trigger.UNKNOWN;
					return;
				}
				if (val is Achieve.Trigger || val is int)
				{
					this.m_triggered = (Achieve.Trigger)val;
					return;
				}
				if (val is string)
				{
					this.m_triggered = Achieve.ParseTriggerValue((string)val);
					return;
				}
			}
		}
		else if (num <= 4057912964U)
		{
			if (num != 3661704248U)
			{
				if (num != 3714292274U)
				{
					if (num != 4057912964U)
					{
						return;
					}
					if (!(name == "ALT_TEXT_PREDICATE"))
					{
						return;
					}
					if (val == null)
					{
						this.m_altTextPredicate = Achieve.AltTextPredicate.NONE;
						return;
					}
					if (val is Achieve.AltTextPredicate || val is int)
					{
						this.m_altTextPredicate = (Achieve.AltTextPredicate)val;
						return;
					}
					if (val is string)
					{
						this.m_altTextPredicate = Achieve.ParseAltTextPredicateValue((string)val);
						return;
					}
				}
				else
				{
					if (!(name == "ALT_NAME"))
					{
						return;
					}
					this.m_altName = (DbfLocValue)val;
					return;
				}
			}
			else
			{
				if (!(name == "SHARED_ACHIEVE_ID"))
				{
					return;
				}
				this.m_sharedAchieveId = (int)val;
				return;
			}
		}
		else if (num != 4110404606U)
		{
			if (num != 4274580549U)
			{
				if (num != 4286938522U)
				{
					return;
				}
				if (!(name == "REWARD"))
				{
					return;
				}
				this.m_reward = (string)val;
				return;
			}
			else
			{
				if (!(name == "PLAYER_TYPE"))
				{
					return;
				}
				if (val == null)
				{
					this.m_playerType = Achieve.PlayerType.ANY;
					return;
				}
				if (val is Achieve.PlayerType || val is int)
				{
					this.m_playerType = (Achieve.PlayerType)val;
					return;
				}
				if (val is string)
				{
					this.m_playerType = Achieve.ParsePlayerTypeValue((string)val);
					return;
				}
			}
		}
		else
		{
			if (!(name == "LEAGUE_VERSION_MAX"))
			{
				return;
			}
			this.m_leagueVersionMax = (int)val;
			return;
		}
	}

	// Token: 0x060015B1 RID: 5553 RVA: 0x0007AD88 File Offset: 0x00078F88
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2013650947U)
		{
			if (num <= 902917601U)
			{
				if (num <= 655598188U)
				{
					if (num <= 190718801U)
					{
						if (num != 190375133U)
						{
							if (num == 190718801U)
							{
								if (name == "ADVENTURE_ID")
								{
									return typeof(int);
								}
							}
						}
						else if (name == "ADVENTURE_MODE_ID")
						{
							return typeof(int);
						}
					}
					else if (num != 378746893U)
					{
						if (num != 493513528U)
						{
							if (num == 655598188U)
							{
								if (name == "LEAGUE_VERSION_MIN")
								{
									return typeof(int);
								}
							}
						}
						else if (name == "ACH_TYPE")
						{
							return typeof(Achieve.Type);
						}
					}
					else if (name == "USE_GENERIC_REWARD_VISUAL")
					{
						return typeof(bool);
					}
				}
				else if (num <= 693605261U)
				{
					if (num != 679831291U)
					{
						if (num == 693605261U)
						{
							if (name == "SCENARIO_ID")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "BOOSTER")
					{
						return typeof(int);
					}
				}
				else if (num != 787754026U)
				{
					if (num != 829736880U)
					{
						if (num == 902917601U)
						{
							if (name == "MY_HERO_CLASS_ID")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "CARD_SET")
					{
						return typeof(int);
					}
				}
				else if (name == "CLIENT_FLAGS")
				{
					return typeof(Achieve.ClientFlags);
				}
			}
			else if (num <= 1458105184U)
			{
				if (num <= 1145534743U)
				{
					if (num != 1103584457U)
					{
						if (num == 1145534743U)
						{
							if (name == "LINK_TO")
							{
								return typeof(string);
							}
						}
					}
					else if (name == "DESCRIPTION")
					{
						return typeof(DbfLocValue);
					}
				}
				else if (num != 1387956774U)
				{
					if (num != 1421814995U)
					{
						if (num == 1458105184U)
						{
							if (name == "ID")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "AUTO_DESTROY")
					{
						return typeof(bool);
					}
				}
				else if (name == "NAME")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num <= 1638382104U)
			{
				if (num != 1521075980U)
				{
					if (num != 1629023597U)
					{
						if (num == 1638382104U)
						{
							if (name == "ACH_QUOTA")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "ALT_DESCRIPTION")
					{
						return typeof(DbfLocValue);
					}
				}
				else if (name == "ATTENTION_BLOCKER")
				{
					return typeof(Achieve.AttentionBlocker);
				}
			}
			else if (num != 1832324731U)
			{
				if (num != 1984182043U)
				{
					if (num == 2013650947U)
					{
						if (name == "ENABLED_WITH_PROGRESSION")
						{
							return typeof(bool);
						}
					}
				}
				else if (name == "GAME_MODE")
				{
					return typeof(Achieve.GameMode);
				}
			}
			else if (name == "SHOW_TO_RETURNING_PLAYER")
			{
				return typeof(Achieve.ShowToReturningPlayer);
			}
		}
		else if (num <= 2967890242U)
		{
			if (num <= 2349802968U)
			{
				if (num <= 2099820198U)
				{
					if (num != 2076288754U)
					{
						if (num == 2099820198U)
						{
							if (name == "MAX_DEFENSE")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "UNLOCKS")
					{
						return typeof(Achieve.Unlocks);
					}
				}
				else if (num != 2222469004U)
				{
					if (num != 2294480894U)
					{
						if (num == 2349802968U)
						{
							if (name == "RACE")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "ENABLED")
					{
						return typeof(bool);
					}
				}
				else if (name == "CUSTOM_VISUAL_WIDGET")
				{
					return typeof(string);
				}
			}
			else if (num <= 2537485753U)
			{
				if (num != 2513575373U)
				{
					if (num == 2537485753U)
					{
						if (name == "REWARD_TIMING")
						{
							return typeof(Achieve.RewardTiming);
						}
					}
				}
				else if (name == "ADVENTURE_WING_ID")
				{
					return typeof(int);
				}
			}
			else if (num != 2851436049U)
			{
				if (num != 2951112623U)
				{
					if (num == 2967890242U)
					{
						if (name == "REWARD_DATA1")
						{
							return typeof(long);
						}
					}
				}
				else if (name == "REWARD_DATA2")
				{
					return typeof(long);
				}
			}
			else if (name == "QUEST_TILE_PREFAB")
			{
				return typeof(string);
			}
		}
		else if (num <= 3638028361U)
		{
			if (num <= 3082479862U)
			{
				if (num != 3022554311U)
				{
					if (num == 3082479862U)
					{
						if (name == "QUEST_DIALOG_ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "NOTE_DESC")
				{
					return typeof(string);
				}
			}
			else if (num != 3403422626U)
			{
				if (num != 3531042190U)
				{
					if (num == 3638028361U)
					{
						if (name == "ENEMY_HERO_CLASS_ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "PARENT_ACH")
				{
					return typeof(string);
				}
			}
			else if (name == "TRIGGERED")
			{
				return typeof(Achieve.Trigger);
			}
		}
		else if (num <= 4057912964U)
		{
			if (num != 3661704248U)
			{
				if (num != 3714292274U)
				{
					if (num == 4057912964U)
					{
						if (name == "ALT_TEXT_PREDICATE")
						{
							return typeof(Achieve.AltTextPredicate);
						}
					}
				}
				else if (name == "ALT_NAME")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (name == "SHARED_ACHIEVE_ID")
			{
				return typeof(int);
			}
		}
		else if (num != 4110404606U)
		{
			if (num != 4274580549U)
			{
				if (num == 4286938522U)
				{
					if (name == "REWARD")
					{
						return typeof(string);
					}
				}
			}
			else if (name == "PLAYER_TYPE")
			{
				return typeof(Achieve.PlayerType);
			}
		}
		else if (name == "LEAGUE_VERSION_MAX")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x060015B2 RID: 5554 RVA: 0x0007B57C File Offset: 0x0007977C
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchieveDbfRecords loadRecords = new LoadAchieveDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060015B3 RID: 5555 RVA: 0x0007B594 File Offset: 0x00079794
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchieveDbfAsset achieveDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchieveDbfAsset)) as AchieveDbfAsset;
		if (achieveDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AchieveDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < achieveDbfAsset.Records.Count; i++)
		{
			achieveDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (achieveDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x0007B613 File Offset: 0x00079813
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
		this.m_description.StripUnusedLocales();
		this.m_altName.StripUnusedLocales();
		this.m_altDescription.StripUnusedLocales();
	}

	// Token: 0x04000E35 RID: 3637
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04000E36 RID: 3638
	[SerializeField]
	private Achieve.Type m_achType = Achieve.ParseTypeValue("invalid");

	// Token: 0x04000E37 RID: 3639
	[SerializeField]
	private bool m_enabled = true;

	// Token: 0x04000E38 RID: 3640
	[SerializeField]
	private string m_parentAch;

	// Token: 0x04000E39 RID: 3641
	[SerializeField]
	private string m_linkTo;

	// Token: 0x04000E3A RID: 3642
	[SerializeField]
	private int m_sharedAchieveId;

	// Token: 0x04000E3B RID: 3643
	[SerializeField]
	private Achieve.ClientFlags m_clientFlags;

	// Token: 0x04000E3C RID: 3644
	[SerializeField]
	private Achieve.Trigger m_triggered = Achieve.ParseTriggerValue("none");

	// Token: 0x04000E3D RID: 3645
	[SerializeField]
	private int m_achQuota;

	// Token: 0x04000E3E RID: 3646
	[SerializeField]
	private Achieve.GameMode m_gameMode = Achieve.ParseGameModeValue("any");

	// Token: 0x04000E3F RID: 3647
	[SerializeField]
	private int m_raceId;

	// Token: 0x04000E40 RID: 3648
	[SerializeField]
	private int m_cardSetId;

	// Token: 0x04000E41 RID: 3649
	[SerializeField]
	private int m_myHeroClassId;

	// Token: 0x04000E42 RID: 3650
	[SerializeField]
	private int m_enemyHeroClassId;

	// Token: 0x04000E43 RID: 3651
	[SerializeField]
	private int m_maxDefense;

	// Token: 0x04000E44 RID: 3652
	[SerializeField]
	private Achieve.PlayerType m_playerType = Achieve.ParsePlayerTypeValue("any");

	// Token: 0x04000E45 RID: 3653
	[SerializeField]
	private int m_leagueVersionMin;

	// Token: 0x04000E46 RID: 3654
	[SerializeField]
	private int m_leagueVersionMax;

	// Token: 0x04000E47 RID: 3655
	[SerializeField]
	private int m_scenarioId;

	// Token: 0x04000E48 RID: 3656
	[SerializeField]
	private int m_adventureId;

	// Token: 0x04000E49 RID: 3657
	[SerializeField]
	private int m_adventureModeId;

	// Token: 0x04000E4A RID: 3658
	[SerializeField]
	private int m_adventureWingId;

	// Token: 0x04000E4B RID: 3659
	[SerializeField]
	private int m_boosterId;

	// Token: 0x04000E4C RID: 3660
	[SerializeField]
	private Achieve.RewardTiming m_rewardTiming = Achieve.ParseRewardTimingValue("immediate");

	// Token: 0x04000E4D RID: 3661
	[SerializeField]
	private string m_reward = "none";

	// Token: 0x04000E4E RID: 3662
	[SerializeField]
	private long m_rewardData1;

	// Token: 0x04000E4F RID: 3663
	[SerializeField]
	private long m_rewardData2;

	// Token: 0x04000E50 RID: 3664
	[SerializeField]
	private Achieve.Unlocks m_unlocks = Achieve.ParseUnlocksValue("none");

	// Token: 0x04000E51 RID: 3665
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x04000E52 RID: 3666
	[SerializeField]
	private DbfLocValue m_description;

	// Token: 0x04000E53 RID: 3667
	[SerializeField]
	private Achieve.AltTextPredicate m_altTextPredicate = Achieve.ParseAltTextPredicateValue("none");

	// Token: 0x04000E54 RID: 3668
	[SerializeField]
	private DbfLocValue m_altName;

	// Token: 0x04000E55 RID: 3669
	[SerializeField]
	private DbfLocValue m_altDescription;

	// Token: 0x04000E56 RID: 3670
	[SerializeField]
	private string m_customVisualWidget;

	// Token: 0x04000E57 RID: 3671
	[SerializeField]
	private bool m_useGenericRewardVisual;

	// Token: 0x04000E58 RID: 3672
	[SerializeField]
	private Achieve.ShowToReturningPlayer m_showToReturningPlayer = Achieve.ParseShowToReturningPlayerValue("always");

	// Token: 0x04000E59 RID: 3673
	[SerializeField]
	private int m_questDialogId;

	// Token: 0x04000E5A RID: 3674
	[SerializeField]
	private bool m_autoDestroy;

	// Token: 0x04000E5B RID: 3675
	[SerializeField]
	private string m_questTilePrefab;

	// Token: 0x04000E5C RID: 3676
	[SerializeField]
	private Achieve.AttentionBlocker m_attentionBlocker = Achieve.ParseAttentionBlockerValue("NONE");

	// Token: 0x04000E5D RID: 3677
	[SerializeField]
	private bool m_enabledWithProgression = true;
}
