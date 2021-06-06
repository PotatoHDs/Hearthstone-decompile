using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000295 RID: 661
[Serializable]
public class WingDbfRecord : DbfRecord
{
	// Token: 0x1700049B RID: 1179
	// (get) Token: 0x06002147 RID: 8519 RVA: 0x000A30F6 File Offset: 0x000A12F6
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x1700049C RID: 1180
	// (get) Token: 0x06002148 RID: 8520 RVA: 0x000A30FE File Offset: 0x000A12FE
	[DbfField("ADVENTURE_ID")]
	public int AdventureId
	{
		get
		{
			return this.m_adventureId;
		}
	}

	// Token: 0x1700049D RID: 1181
	// (get) Token: 0x06002149 RID: 8521 RVA: 0x000A3106 File Offset: 0x000A1306
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x1700049E RID: 1182
	// (get) Token: 0x0600214A RID: 8522 RVA: 0x000A310E File Offset: 0x000A130E
	[DbfField("UNLOCK_ORDER")]
	public int UnlockOrder
	{
		get
		{
			return this.m_unlockOrder;
		}
	}

	// Token: 0x1700049F RID: 1183
	// (get) Token: 0x0600214B RID: 8523 RVA: 0x000A3116 File Offset: 0x000A1316
	[DbfField("REQUIRED_EVENT")]
	public string RequiredEvent
	{
		get
		{
			return this.m_requiredEvent;
		}
	}

	// Token: 0x170004A0 RID: 1184
	// (get) Token: 0x0600214C RID: 8524 RVA: 0x000A311E File Offset: 0x000A131E
	[DbfField("OWNERSHIP_PREREQ_WING_ID")]
	public int OwnershipPrereqWingId
	{
		get
		{
			return this.m_ownershipPrereqWingId;
		}
	}

	// Token: 0x170004A1 RID: 1185
	// (get) Token: 0x0600214D RID: 8525 RVA: 0x000A3126 File Offset: 0x000A1326
	public WingDbfRecord OwnershipPrereqWingRecord
	{
		get
		{
			return GameDbf.Wing.GetRecord(this.m_ownershipPrereqWingId);
		}
	}

	// Token: 0x170004A2 RID: 1186
	// (get) Token: 0x0600214E RID: 8526 RVA: 0x000A3138 File Offset: 0x000A1338
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x170004A3 RID: 1187
	// (get) Token: 0x0600214F RID: 8527 RVA: 0x000A3140 File Offset: 0x000A1340
	[DbfField("NAME_SHORT")]
	public DbfLocValue NameShort
	{
		get
		{
			return this.m_nameShort;
		}
	}

	// Token: 0x170004A4 RID: 1188
	// (get) Token: 0x06002150 RID: 8528 RVA: 0x000A3148 File Offset: 0x000A1348
	[DbfField("DESCRIPTION")]
	public DbfLocValue Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x170004A5 RID: 1189
	// (get) Token: 0x06002151 RID: 8529 RVA: 0x000A3150 File Offset: 0x000A1350
	[DbfField("CLASS_CHALLENGE_REWARD_SOURCE")]
	public DbfLocValue ClassChallengeRewardSource
	{
		get
		{
			return this.m_classChallengeRewardSource;
		}
	}

	// Token: 0x170004A6 RID: 1190
	// (get) Token: 0x06002152 RID: 8530 RVA: 0x000A3158 File Offset: 0x000A1358
	[DbfField("ADVENTURE_WING_DEF_PREFAB")]
	public string AdventureWingDefPrefab
	{
		get
		{
			return this.m_adventureWingDefPrefab;
		}
	}

	// Token: 0x170004A7 RID: 1191
	// (get) Token: 0x06002153 RID: 8531 RVA: 0x000A3160 File Offset: 0x000A1360
	[DbfField("COMING_SOON_LABEL")]
	public DbfLocValue ComingSoonLabel
	{
		get
		{
			return this.m_comingSoonLabel;
		}
	}

	// Token: 0x170004A8 RID: 1192
	// (get) Token: 0x06002154 RID: 8532 RVA: 0x000A3168 File Offset: 0x000A1368
	[DbfField("REQUIRES_LABEL")]
	public DbfLocValue RequiresLabel
	{
		get
		{
			return this.m_requiresLabel;
		}
	}

	// Token: 0x170004A9 RID: 1193
	// (get) Token: 0x06002155 RID: 8533 RVA: 0x000A3170 File Offset: 0x000A1370
	[DbfField("OPEN_PREREQ_WING_ID")]
	public int OpenPrereqWingId
	{
		get
		{
			return this.m_openPrereqWingId;
		}
	}

	// Token: 0x170004AA RID: 1194
	// (get) Token: 0x06002156 RID: 8534 RVA: 0x000A3178 File Offset: 0x000A1378
	public WingDbfRecord OpenPrereqWingRecord
	{
		get
		{
			return GameDbf.Wing.GetRecord(this.m_openPrereqWingId);
		}
	}

	// Token: 0x170004AB RID: 1195
	// (get) Token: 0x06002157 RID: 8535 RVA: 0x000A318A File Offset: 0x000A138A
	[DbfField("OPEN_DISCOURAGED_LABEL")]
	public DbfLocValue OpenDiscouragedLabel
	{
		get
		{
			return this.m_openDiscouragedLabel;
		}
	}

	// Token: 0x170004AC RID: 1196
	// (get) Token: 0x06002158 RID: 8536 RVA: 0x000A3192 File Offset: 0x000A1392
	[DbfField("OPEN_DISCOURAGED_WARNING")]
	public DbfLocValue OpenDiscouragedWarning
	{
		get
		{
			return this.m_openDiscouragedWarning;
		}
	}

	// Token: 0x170004AD RID: 1197
	// (get) Token: 0x06002159 RID: 8537 RVA: 0x000A319A File Offset: 0x000A139A
	[DbfField("MUST_COMPLETE_OPEN_PREREQ")]
	public bool MustCompleteOpenPrereq
	{
		get
		{
			return this.m_mustCompleteOpenPrereq;
		}
	}

	// Token: 0x170004AE RID: 1198
	// (get) Token: 0x0600215A RID: 8538 RVA: 0x000A31A2 File Offset: 0x000A13A2
	[DbfField("UNLOCKS_AUTOMATICALLY")]
	public bool UnlocksAutomatically
	{
		get
		{
			return this.m_unlocksAutomatically;
		}
	}

	// Token: 0x170004AF RID: 1199
	// (get) Token: 0x0600215B RID: 8539 RVA: 0x000A31AA File Offset: 0x000A13AA
	[DbfField("USE_UNLOCK_COUNTDOWN")]
	public bool UseUnlockCountdown
	{
		get
		{
			return this.m_useUnlockCountdown;
		}
	}

	// Token: 0x170004B0 RID: 1200
	// (get) Token: 0x0600215C RID: 8540 RVA: 0x000A31B2 File Offset: 0x000A13B2
	[DbfField("STORE_BUY_WING_BUTTON_LABEL")]
	public DbfLocValue StoreBuyWingButtonLabel
	{
		get
		{
			return this.m_storeBuyWingButtonLabel;
		}
	}

	// Token: 0x170004B1 RID: 1201
	// (get) Token: 0x0600215D RID: 8541 RVA: 0x000A31BA File Offset: 0x000A13BA
	[DbfField("STORE_BUY_WING_DESC")]
	public DbfLocValue StoreBuyWingDesc
	{
		get
		{
			return this.m_storeBuyWingDesc;
		}
	}

	// Token: 0x170004B2 RID: 1202
	// (get) Token: 0x0600215E RID: 8542 RVA: 0x000A31C2 File Offset: 0x000A13C2
	[DbfField("DUNGEON_CRAWL_BOSSES")]
	public int DungeonCrawlBosses
	{
		get
		{
			return this.m_dungeonCrawlBosses;
		}
	}

	// Token: 0x170004B3 RID: 1203
	// (get) Token: 0x0600215F RID: 8543 RVA: 0x000A31CA File Offset: 0x000A13CA
	[DbfField("VISUAL_STATE_NAME")]
	public string VisualStateName
	{
		get
		{
			return this.m_visualStateName;
		}
	}

	// Token: 0x170004B4 RID: 1204
	// (get) Token: 0x06002160 RID: 8544 RVA: 0x000A31D2 File Offset: 0x000A13D2
	[DbfField("PLOT_TWIST_CARD_ID")]
	public int PlotTwistCardId
	{
		get
		{
			return this.m_plotTwistCardId;
		}
	}

	// Token: 0x170004B5 RID: 1205
	// (get) Token: 0x06002161 RID: 8545 RVA: 0x000A31DA File Offset: 0x000A13DA
	public CardDbfRecord PlotTwistCardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_plotTwistCardId);
		}
	}

	// Token: 0x170004B6 RID: 1206
	// (get) Token: 0x06002162 RID: 8546 RVA: 0x000A31EC File Offset: 0x000A13EC
	[DbfField("DISPLAY_RAID_BOSS_HEALTH")]
	public bool DisplayRaidBossHealth
	{
		get
		{
			return this.m_displayRaidBossHealth;
		}
	}

	// Token: 0x170004B7 RID: 1207
	// (get) Token: 0x06002163 RID: 8547 RVA: 0x000A31F4 File Offset: 0x000A13F4
	[DbfField("RAID_BOSS_CARD_ID")]
	public int RaidBossCardId
	{
		get
		{
			return this.m_raidBossCardId;
		}
	}

	// Token: 0x170004B8 RID: 1208
	// (get) Token: 0x06002164 RID: 8548 RVA: 0x000A31FC File Offset: 0x000A13FC
	public CardDbfRecord RaidBossCardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_raidBossCardId);
		}
	}

	// Token: 0x170004B9 RID: 1209
	// (get) Token: 0x06002165 RID: 8549 RVA: 0x000A320E File Offset: 0x000A140E
	[DbfField("ALLOWS_ANOMALY")]
	public bool AllowsAnomaly
	{
		get
		{
			return this.m_allowsAnomaly;
		}
	}

	// Token: 0x170004BA RID: 1210
	// (get) Token: 0x06002166 RID: 8550 RVA: 0x000A3216 File Offset: 0x000A1416
	[DbfField("PMT_PRODUCT_ID_FOR_SINGLE_WING_PURCHASE")]
	public int PmtProductIdForSingleWingPurchase
	{
		get
		{
			return this.m_pmtProductIdForSingleWingPurchase;
		}
	}

	// Token: 0x170004BB RID: 1211
	// (get) Token: 0x06002167 RID: 8551 RVA: 0x000A321E File Offset: 0x000A141E
	[DbfField("PMT_PRODUCT_ID_FOR_THIS_AND_REST_OF_ADVENTURE")]
	public int PmtProductIdForThisAndRestOfAdventure
	{
		get
		{
			return this.m_pmtProductIdForThisAndRestOfAdventure;
		}
	}

	// Token: 0x170004BC RID: 1212
	// (get) Token: 0x06002168 RID: 8552 RVA: 0x000A3226 File Offset: 0x000A1426
	[DbfField("BOOK_SECTION")]
	public int BookSection
	{
		get
		{
			return this.m_bookSection;
		}
	}

	// Token: 0x06002169 RID: 8553 RVA: 0x000A322E File Offset: 0x000A142E
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x0600216A RID: 8554 RVA: 0x000A3237 File Offset: 0x000A1437
	public void SetAdventureId(int v)
	{
		this.m_adventureId = v;
	}

	// Token: 0x0600216B RID: 8555 RVA: 0x000A3240 File Offset: 0x000A1440
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x0600216C RID: 8556 RVA: 0x000A3249 File Offset: 0x000A1449
	public void SetUnlockOrder(int v)
	{
		this.m_unlockOrder = v;
	}

	// Token: 0x0600216D RID: 8557 RVA: 0x000A3252 File Offset: 0x000A1452
	public void SetRequiredEvent(string v)
	{
		this.m_requiredEvent = v;
	}

	// Token: 0x0600216E RID: 8558 RVA: 0x000A325B File Offset: 0x000A145B
	public void SetOwnershipPrereqWingId(int v)
	{
		this.m_ownershipPrereqWingId = v;
	}

	// Token: 0x0600216F RID: 8559 RVA: 0x000A3264 File Offset: 0x000A1464
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x06002170 RID: 8560 RVA: 0x000A327E File Offset: 0x000A147E
	public void SetNameShort(DbfLocValue v)
	{
		this.m_nameShort = v;
		v.SetDebugInfo(base.ID, "NAME_SHORT");
	}

	// Token: 0x06002171 RID: 8561 RVA: 0x000A3298 File Offset: 0x000A1498
	public void SetDescription(DbfLocValue v)
	{
		this.m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	// Token: 0x06002172 RID: 8562 RVA: 0x000A32B2 File Offset: 0x000A14B2
	public void SetClassChallengeRewardSource(DbfLocValue v)
	{
		this.m_classChallengeRewardSource = v;
		v.SetDebugInfo(base.ID, "CLASS_CHALLENGE_REWARD_SOURCE");
	}

	// Token: 0x06002173 RID: 8563 RVA: 0x000A32CC File Offset: 0x000A14CC
	public void SetAdventureWingDefPrefab(string v)
	{
		this.m_adventureWingDefPrefab = v;
	}

	// Token: 0x06002174 RID: 8564 RVA: 0x000A32D5 File Offset: 0x000A14D5
	public void SetComingSoonLabel(DbfLocValue v)
	{
		this.m_comingSoonLabel = v;
		v.SetDebugInfo(base.ID, "COMING_SOON_LABEL");
	}

	// Token: 0x06002175 RID: 8565 RVA: 0x000A32EF File Offset: 0x000A14EF
	public void SetRequiresLabel(DbfLocValue v)
	{
		this.m_requiresLabel = v;
		v.SetDebugInfo(base.ID, "REQUIRES_LABEL");
	}

	// Token: 0x06002176 RID: 8566 RVA: 0x000A3309 File Offset: 0x000A1509
	public void SetOpenPrereqWingId(int v)
	{
		this.m_openPrereqWingId = v;
	}

	// Token: 0x06002177 RID: 8567 RVA: 0x000A3312 File Offset: 0x000A1512
	public void SetOpenDiscouragedLabel(DbfLocValue v)
	{
		this.m_openDiscouragedLabel = v;
		v.SetDebugInfo(base.ID, "OPEN_DISCOURAGED_LABEL");
	}

	// Token: 0x06002178 RID: 8568 RVA: 0x000A332C File Offset: 0x000A152C
	public void SetOpenDiscouragedWarning(DbfLocValue v)
	{
		this.m_openDiscouragedWarning = v;
		v.SetDebugInfo(base.ID, "OPEN_DISCOURAGED_WARNING");
	}

	// Token: 0x06002179 RID: 8569 RVA: 0x000A3346 File Offset: 0x000A1546
	public void SetMustCompleteOpenPrereq(bool v)
	{
		this.m_mustCompleteOpenPrereq = v;
	}

	// Token: 0x0600217A RID: 8570 RVA: 0x000A334F File Offset: 0x000A154F
	public void SetUnlocksAutomatically(bool v)
	{
		this.m_unlocksAutomatically = v;
	}

	// Token: 0x0600217B RID: 8571 RVA: 0x000A3358 File Offset: 0x000A1558
	public void SetUseUnlockCountdown(bool v)
	{
		this.m_useUnlockCountdown = v;
	}

	// Token: 0x0600217C RID: 8572 RVA: 0x000A3361 File Offset: 0x000A1561
	public void SetStoreBuyWingButtonLabel(DbfLocValue v)
	{
		this.m_storeBuyWingButtonLabel = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WING_BUTTON_LABEL");
	}

	// Token: 0x0600217D RID: 8573 RVA: 0x000A337B File Offset: 0x000A157B
	public void SetStoreBuyWingDesc(DbfLocValue v)
	{
		this.m_storeBuyWingDesc = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WING_DESC");
	}

	// Token: 0x0600217E RID: 8574 RVA: 0x000A3395 File Offset: 0x000A1595
	public void SetDungeonCrawlBosses(int v)
	{
		this.m_dungeonCrawlBosses = v;
	}

	// Token: 0x0600217F RID: 8575 RVA: 0x000A339E File Offset: 0x000A159E
	public void SetVisualStateName(string v)
	{
		this.m_visualStateName = v;
	}

	// Token: 0x06002180 RID: 8576 RVA: 0x000A33A7 File Offset: 0x000A15A7
	public void SetPlotTwistCardId(int v)
	{
		this.m_plotTwistCardId = v;
	}

	// Token: 0x06002181 RID: 8577 RVA: 0x000A33B0 File Offset: 0x000A15B0
	public void SetDisplayRaidBossHealth(bool v)
	{
		this.m_displayRaidBossHealth = v;
	}

	// Token: 0x06002182 RID: 8578 RVA: 0x000A33B9 File Offset: 0x000A15B9
	public void SetRaidBossCardId(int v)
	{
		this.m_raidBossCardId = v;
	}

	// Token: 0x06002183 RID: 8579 RVA: 0x000A33C2 File Offset: 0x000A15C2
	public void SetAllowsAnomaly(bool v)
	{
		this.m_allowsAnomaly = v;
	}

	// Token: 0x06002184 RID: 8580 RVA: 0x000A33CB File Offset: 0x000A15CB
	public void SetPmtProductIdForSingleWingPurchase(int v)
	{
		this.m_pmtProductIdForSingleWingPurchase = v;
	}

	// Token: 0x06002185 RID: 8581 RVA: 0x000A33D4 File Offset: 0x000A15D4
	public void SetPmtProductIdForThisAndRestOfAdventure(int v)
	{
		this.m_pmtProductIdForThisAndRestOfAdventure = v;
	}

	// Token: 0x06002186 RID: 8582 RVA: 0x000A33DD File Offset: 0x000A15DD
	public void SetBookSection(int v)
	{
		this.m_bookSection = v;
	}

	// Token: 0x06002187 RID: 8583 RVA: 0x000A33E8 File Offset: 0x000A15E8
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2290477324U)
		{
			if (num <= 933277803U)
			{
				if (num <= 265019175U)
				{
					if (num != 91939326U)
					{
						if (num != 190718801U)
						{
							if (num == 265019175U)
							{
								if (name == "REQUIRED_EVENT")
								{
									return this.m_requiredEvent;
								}
							}
						}
						else if (name == "ADVENTURE_ID")
						{
							return this.m_adventureId;
						}
					}
					else if (name == "OWNERSHIP_PREREQ_WING_ID")
					{
						return this.m_ownershipPrereqWingId;
					}
				}
				else if (num <= 827198106U)
				{
					if (num != 595659997U)
					{
						if (num == 827198106U)
						{
							if (name == "ADVENTURE_WING_DEF_PREFAB")
							{
								return this.m_adventureWingDefPrefab;
							}
						}
					}
					else if (name == "ALLOWS_ANOMALY")
					{
						return this.m_allowsAnomaly;
					}
				}
				else if (num != 864975350U)
				{
					if (num == 933277803U)
					{
						if (name == "DUNGEON_CRAWL_BOSSES")
						{
							return this.m_dungeonCrawlBosses;
						}
					}
				}
				else if (name == "UNLOCK_ORDER")
				{
					return this.m_unlockOrder;
				}
			}
			else if (num <= 1439689743U)
			{
				if (num <= 1311799363U)
				{
					if (num != 1103584457U)
					{
						if (num == 1311799363U)
						{
							if (name == "MUST_COMPLETE_OPEN_PREREQ")
							{
								return this.m_mustCompleteOpenPrereq;
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
					if (num == 1439689743U)
					{
						if (name == "PMT_PRODUCT_ID_FOR_SINGLE_WING_PURCHASE")
						{
							return this.m_pmtProductIdForSingleWingPurchase;
						}
					}
				}
				else if (name == "NAME")
				{
					return this.m_name;
				}
			}
			else if (num <= 1555399727U)
			{
				if (num != 1458105184U)
				{
					if (num == 1555399727U)
					{
						if (name == "STORE_BUY_WING_BUTTON_LABEL")
						{
							return this.m_storeBuyWingButtonLabel;
						}
					}
				}
				else if (name == "ID")
				{
					return base.ID;
				}
			}
			else if (num != 2014266179U)
			{
				if (num == 2290477324U)
				{
					if (name == "BOOK_SECTION")
					{
						return this.m_bookSection;
					}
				}
			}
			else if (name == "PLOT_TWIST_CARD_ID")
			{
				return this.m_plotTwistCardId;
			}
		}
		else if (num <= 3022554311U)
		{
			if (num <= 2503143781U)
			{
				if (num <= 2449846983U)
				{
					if (num != 2360313627U)
					{
						if (num == 2449846983U)
						{
							if (name == "OPEN_PREREQ_WING_ID")
							{
								return this.m_openPrereqWingId;
							}
						}
					}
					else if (name == "VISUAL_STATE_NAME")
					{
						return this.m_visualStateName;
					}
				}
				else if (num != 2490864483U)
				{
					if (num == 2503143781U)
					{
						if (name == "OPEN_DISCOURAGED_WARNING")
						{
							return this.m_openDiscouragedWarning;
						}
					}
				}
				else if (name == "PMT_PRODUCT_ID_FOR_THIS_AND_REST_OF_ADVENTURE")
				{
					return this.m_pmtProductIdForThisAndRestOfAdventure;
				}
			}
			else if (num <= 2694664299U)
			{
				if (num != 2610363275U)
				{
					if (num == 2694664299U)
					{
						if (name == "STORE_BUY_WING_DESC")
						{
							return this.m_storeBuyWingDesc;
						}
					}
				}
				else if (name == "OPEN_DISCOURAGED_LABEL")
				{
					return this.m_openDiscouragedLabel;
				}
			}
			else if (num != 2756755729U)
			{
				if (num == 3022554311U)
				{
					if (name == "NOTE_DESC")
					{
						return this.m_noteDesc;
					}
				}
			}
			else if (name == "USE_UNLOCK_COUNTDOWN")
			{
				return this.m_useUnlockCountdown;
			}
		}
		else if (num <= 3782964816U)
		{
			if (num <= 3152384434U)
			{
				if (num != 3070501611U)
				{
					if (num == 3152384434U)
					{
						if (name == "RAID_BOSS_CARD_ID")
						{
							return this.m_raidBossCardId;
						}
					}
				}
				else if (name == "CLASS_CHALLENGE_REWARD_SOURCE")
				{
					return this.m_classChallengeRewardSource;
				}
			}
			else if (num != 3753829705U)
			{
				if (num == 3782964816U)
				{
					if (name == "UNLOCKS_AUTOMATICALLY")
					{
						return this.m_unlocksAutomatically;
					}
				}
			}
			else if (name == "COMING_SOON_LABEL")
			{
				return this.m_comingSoonLabel;
			}
		}
		else if (num <= 4127906772U)
		{
			if (num != 4080209141U)
			{
				if (num == 4127906772U)
				{
					if (name == "REQUIRES_LABEL")
					{
						return this.m_requiresLabel;
					}
				}
			}
			else if (name == "DISPLAY_RAID_BOSS_HEALTH")
			{
				return this.m_displayRaidBossHealth;
			}
		}
		else if (num != 4136070939U)
		{
			if (num == 4214602626U)
			{
				if (name == "SORT_ORDER")
				{
					return this.m_sortOrder;
				}
			}
		}
		else if (name == "NAME_SHORT")
		{
			return this.m_nameShort;
		}
		return null;
	}

	// Token: 0x06002188 RID: 8584 RVA: 0x000A39CC File Offset: 0x000A1BCC
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2290477324U)
		{
			if (num <= 933277803U)
			{
				if (num <= 265019175U)
				{
					if (num != 91939326U)
					{
						if (num != 190718801U)
						{
							if (num != 265019175U)
							{
								return;
							}
							if (!(name == "REQUIRED_EVENT"))
							{
								return;
							}
							this.m_requiredEvent = (string)val;
							return;
						}
						else
						{
							if (!(name == "ADVENTURE_ID"))
							{
								return;
							}
							this.m_adventureId = (int)val;
							return;
						}
					}
					else
					{
						if (!(name == "OWNERSHIP_PREREQ_WING_ID"))
						{
							return;
						}
						this.m_ownershipPrereqWingId = (int)val;
						return;
					}
				}
				else if (num <= 827198106U)
				{
					if (num != 595659997U)
					{
						if (num != 827198106U)
						{
							return;
						}
						if (!(name == "ADVENTURE_WING_DEF_PREFAB"))
						{
							return;
						}
						this.m_adventureWingDefPrefab = (string)val;
						return;
					}
					else
					{
						if (!(name == "ALLOWS_ANOMALY"))
						{
							return;
						}
						this.m_allowsAnomaly = (bool)val;
						return;
					}
				}
				else if (num != 864975350U)
				{
					if (num != 933277803U)
					{
						return;
					}
					if (!(name == "DUNGEON_CRAWL_BOSSES"))
					{
						return;
					}
					this.m_dungeonCrawlBosses = (int)val;
					return;
				}
				else
				{
					if (!(name == "UNLOCK_ORDER"))
					{
						return;
					}
					this.m_unlockOrder = (int)val;
					return;
				}
			}
			else if (num <= 1439689743U)
			{
				if (num <= 1311799363U)
				{
					if (num != 1103584457U)
					{
						if (num != 1311799363U)
						{
							return;
						}
						if (!(name == "MUST_COMPLETE_OPEN_PREREQ"))
						{
							return;
						}
						this.m_mustCompleteOpenPrereq = (bool)val;
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
					if (num != 1439689743U)
					{
						return;
					}
					if (!(name == "PMT_PRODUCT_ID_FOR_SINGLE_WING_PURCHASE"))
					{
						return;
					}
					this.m_pmtProductIdForSingleWingPurchase = (int)val;
					return;
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
			else if (num <= 1555399727U)
			{
				if (num != 1458105184U)
				{
					if (num != 1555399727U)
					{
						return;
					}
					if (!(name == "STORE_BUY_WING_BUTTON_LABEL"))
					{
						return;
					}
					this.m_storeBuyWingButtonLabel = (DbfLocValue)val;
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
			else if (num != 2014266179U)
			{
				if (num != 2290477324U)
				{
					return;
				}
				if (!(name == "BOOK_SECTION"))
				{
					return;
				}
				this.m_bookSection = (int)val;
				return;
			}
			else
			{
				if (!(name == "PLOT_TWIST_CARD_ID"))
				{
					return;
				}
				this.m_plotTwistCardId = (int)val;
				return;
			}
		}
		else if (num <= 3022554311U)
		{
			if (num <= 2503143781U)
			{
				if (num <= 2449846983U)
				{
					if (num != 2360313627U)
					{
						if (num != 2449846983U)
						{
							return;
						}
						if (!(name == "OPEN_PREREQ_WING_ID"))
						{
							return;
						}
						this.m_openPrereqWingId = (int)val;
						return;
					}
					else
					{
						if (!(name == "VISUAL_STATE_NAME"))
						{
							return;
						}
						this.m_visualStateName = (string)val;
						return;
					}
				}
				else if (num != 2490864483U)
				{
					if (num != 2503143781U)
					{
						return;
					}
					if (!(name == "OPEN_DISCOURAGED_WARNING"))
					{
						return;
					}
					this.m_openDiscouragedWarning = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "PMT_PRODUCT_ID_FOR_THIS_AND_REST_OF_ADVENTURE"))
					{
						return;
					}
					this.m_pmtProductIdForThisAndRestOfAdventure = (int)val;
					return;
				}
			}
			else if (num <= 2694664299U)
			{
				if (num != 2610363275U)
				{
					if (num != 2694664299U)
					{
						return;
					}
					if (!(name == "STORE_BUY_WING_DESC"))
					{
						return;
					}
					this.m_storeBuyWingDesc = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "OPEN_DISCOURAGED_LABEL"))
					{
						return;
					}
					this.m_openDiscouragedLabel = (DbfLocValue)val;
					return;
				}
			}
			else if (num != 2756755729U)
			{
				if (num != 3022554311U)
				{
					return;
				}
				if (!(name == "NOTE_DESC"))
				{
					return;
				}
				this.m_noteDesc = (string)val;
				return;
			}
			else
			{
				if (!(name == "USE_UNLOCK_COUNTDOWN"))
				{
					return;
				}
				this.m_useUnlockCountdown = (bool)val;
				return;
			}
		}
		else if (num <= 3782964816U)
		{
			if (num <= 3152384434U)
			{
				if (num != 3070501611U)
				{
					if (num != 3152384434U)
					{
						return;
					}
					if (!(name == "RAID_BOSS_CARD_ID"))
					{
						return;
					}
					this.m_raidBossCardId = (int)val;
					return;
				}
				else
				{
					if (!(name == "CLASS_CHALLENGE_REWARD_SOURCE"))
					{
						return;
					}
					this.m_classChallengeRewardSource = (DbfLocValue)val;
					return;
				}
			}
			else if (num != 3753829705U)
			{
				if (num != 3782964816U)
				{
					return;
				}
				if (!(name == "UNLOCKS_AUTOMATICALLY"))
				{
					return;
				}
				this.m_unlocksAutomatically = (bool)val;
				return;
			}
			else
			{
				if (!(name == "COMING_SOON_LABEL"))
				{
					return;
				}
				this.m_comingSoonLabel = (DbfLocValue)val;
				return;
			}
		}
		else if (num <= 4127906772U)
		{
			if (num != 4080209141U)
			{
				if (num != 4127906772U)
				{
					return;
				}
				if (!(name == "REQUIRES_LABEL"))
				{
					return;
				}
				this.m_requiresLabel = (DbfLocValue)val;
				return;
			}
			else
			{
				if (!(name == "DISPLAY_RAID_BOSS_HEALTH"))
				{
					return;
				}
				this.m_displayRaidBossHealth = (bool)val;
				return;
			}
		}
		else if (num != 4136070939U)
		{
			if (num != 4214602626U)
			{
				return;
			}
			if (!(name == "SORT_ORDER"))
			{
				return;
			}
			this.m_sortOrder = (int)val;
			return;
		}
		else
		{
			if (!(name == "NAME_SHORT"))
			{
				return;
			}
			this.m_nameShort = (DbfLocValue)val;
			return;
		}
	}

	// Token: 0x06002189 RID: 8585 RVA: 0x000A3F58 File Offset: 0x000A2158
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2290477324U)
		{
			if (num <= 933277803U)
			{
				if (num <= 265019175U)
				{
					if (num != 91939326U)
					{
						if (num != 190718801U)
						{
							if (num == 265019175U)
							{
								if (name == "REQUIRED_EVENT")
								{
									return typeof(string);
								}
							}
						}
						else if (name == "ADVENTURE_ID")
						{
							return typeof(int);
						}
					}
					else if (name == "OWNERSHIP_PREREQ_WING_ID")
					{
						return typeof(int);
					}
				}
				else if (num <= 827198106U)
				{
					if (num != 595659997U)
					{
						if (num == 827198106U)
						{
							if (name == "ADVENTURE_WING_DEF_PREFAB")
							{
								return typeof(string);
							}
						}
					}
					else if (name == "ALLOWS_ANOMALY")
					{
						return typeof(bool);
					}
				}
				else if (num != 864975350U)
				{
					if (num == 933277803U)
					{
						if (name == "DUNGEON_CRAWL_BOSSES")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "UNLOCK_ORDER")
				{
					return typeof(int);
				}
			}
			else if (num <= 1439689743U)
			{
				if (num <= 1311799363U)
				{
					if (num != 1103584457U)
					{
						if (num == 1311799363U)
						{
							if (name == "MUST_COMPLETE_OPEN_PREREQ")
							{
								return typeof(bool);
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
					if (num == 1439689743U)
					{
						if (name == "PMT_PRODUCT_ID_FOR_SINGLE_WING_PURCHASE")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "NAME")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num <= 1555399727U)
			{
				if (num != 1458105184U)
				{
					if (num == 1555399727U)
					{
						if (name == "STORE_BUY_WING_BUTTON_LABEL")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "ID")
				{
					return typeof(int);
				}
			}
			else if (num != 2014266179U)
			{
				if (num == 2290477324U)
				{
					if (name == "BOOK_SECTION")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "PLOT_TWIST_CARD_ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 3022554311U)
		{
			if (num <= 2503143781U)
			{
				if (num <= 2449846983U)
				{
					if (num != 2360313627U)
					{
						if (num == 2449846983U)
						{
							if (name == "OPEN_PREREQ_WING_ID")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "VISUAL_STATE_NAME")
					{
						return typeof(string);
					}
				}
				else if (num != 2490864483U)
				{
					if (num == 2503143781U)
					{
						if (name == "OPEN_DISCOURAGED_WARNING")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "PMT_PRODUCT_ID_FOR_THIS_AND_REST_OF_ADVENTURE")
				{
					return typeof(int);
				}
			}
			else if (num <= 2694664299U)
			{
				if (num != 2610363275U)
				{
					if (num == 2694664299U)
					{
						if (name == "STORE_BUY_WING_DESC")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "OPEN_DISCOURAGED_LABEL")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num != 2756755729U)
			{
				if (num == 3022554311U)
				{
					if (name == "NOTE_DESC")
					{
						return typeof(string);
					}
				}
			}
			else if (name == "USE_UNLOCK_COUNTDOWN")
			{
				return typeof(bool);
			}
		}
		else if (num <= 3782964816U)
		{
			if (num <= 3152384434U)
			{
				if (num != 3070501611U)
				{
					if (num == 3152384434U)
					{
						if (name == "RAID_BOSS_CARD_ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "CLASS_CHALLENGE_REWARD_SOURCE")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num != 3753829705U)
			{
				if (num == 3782964816U)
				{
					if (name == "UNLOCKS_AUTOMATICALLY")
					{
						return typeof(bool);
					}
				}
			}
			else if (name == "COMING_SOON_LABEL")
			{
				return typeof(DbfLocValue);
			}
		}
		else if (num <= 4127906772U)
		{
			if (num != 4080209141U)
			{
				if (num == 4127906772U)
				{
					if (name == "REQUIRES_LABEL")
					{
						return typeof(DbfLocValue);
					}
				}
			}
			else if (name == "DISPLAY_RAID_BOSS_HEALTH")
			{
				return typeof(bool);
			}
		}
		else if (num != 4136070939U)
		{
			if (num == 4214602626U)
			{
				if (name == "SORT_ORDER")
				{
					return typeof(int);
				}
			}
		}
		else if (name == "NAME_SHORT")
		{
			return typeof(DbfLocValue);
		}
		return null;
	}

	// Token: 0x0600218A RID: 8586 RVA: 0x000A4560 File Offset: 0x000A2760
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadWingDbfRecords loadRecords = new LoadWingDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600218B RID: 8587 RVA: 0x000A4578 File Offset: 0x000A2778
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		WingDbfAsset wingDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(WingDbfAsset)) as WingDbfAsset;
		if (wingDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("WingDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < wingDbfAsset.Records.Count; i++)
		{
			wingDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (wingDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600218C RID: 8588 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600218D RID: 8589 RVA: 0x000A45F8 File Offset: 0x000A27F8
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
		this.m_nameShort.StripUnusedLocales();
		this.m_description.StripUnusedLocales();
		this.m_classChallengeRewardSource.StripUnusedLocales();
		this.m_comingSoonLabel.StripUnusedLocales();
		this.m_requiresLabel.StripUnusedLocales();
		this.m_openDiscouragedLabel.StripUnusedLocales();
		this.m_openDiscouragedWarning.StripUnusedLocales();
		this.m_storeBuyWingButtonLabel.StripUnusedLocales();
		this.m_storeBuyWingDesc.StripUnusedLocales();
	}

	// Token: 0x04001276 RID: 4726
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04001277 RID: 4727
	[SerializeField]
	private int m_adventureId;

	// Token: 0x04001278 RID: 4728
	[SerializeField]
	private int m_sortOrder;

	// Token: 0x04001279 RID: 4729
	[SerializeField]
	private int m_unlockOrder;

	// Token: 0x0400127A RID: 4730
	[SerializeField]
	private string m_requiredEvent = "none";

	// Token: 0x0400127B RID: 4731
	[SerializeField]
	private int m_ownershipPrereqWingId;

	// Token: 0x0400127C RID: 4732
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x0400127D RID: 4733
	[SerializeField]
	private DbfLocValue m_nameShort;

	// Token: 0x0400127E RID: 4734
	[SerializeField]
	private DbfLocValue m_description;

	// Token: 0x0400127F RID: 4735
	[SerializeField]
	private DbfLocValue m_classChallengeRewardSource;

	// Token: 0x04001280 RID: 4736
	[SerializeField]
	private string m_adventureWingDefPrefab;

	// Token: 0x04001281 RID: 4737
	[SerializeField]
	private DbfLocValue m_comingSoonLabel;

	// Token: 0x04001282 RID: 4738
	[SerializeField]
	private DbfLocValue m_requiresLabel;

	// Token: 0x04001283 RID: 4739
	[SerializeField]
	private int m_openPrereqWingId;

	// Token: 0x04001284 RID: 4740
	[SerializeField]
	private DbfLocValue m_openDiscouragedLabel;

	// Token: 0x04001285 RID: 4741
	[SerializeField]
	private DbfLocValue m_openDiscouragedWarning;

	// Token: 0x04001286 RID: 4742
	[SerializeField]
	private bool m_mustCompleteOpenPrereq;

	// Token: 0x04001287 RID: 4743
	[SerializeField]
	private bool m_unlocksAutomatically;

	// Token: 0x04001288 RID: 4744
	[SerializeField]
	private bool m_useUnlockCountdown;

	// Token: 0x04001289 RID: 4745
	[SerializeField]
	private DbfLocValue m_storeBuyWingButtonLabel;

	// Token: 0x0400128A RID: 4746
	[SerializeField]
	private DbfLocValue m_storeBuyWingDesc;

	// Token: 0x0400128B RID: 4747
	[SerializeField]
	private int m_dungeonCrawlBosses = 8;

	// Token: 0x0400128C RID: 4748
	[SerializeField]
	private string m_visualStateName;

	// Token: 0x0400128D RID: 4749
	[SerializeField]
	private int m_plotTwistCardId;

	// Token: 0x0400128E RID: 4750
	[SerializeField]
	private bool m_displayRaidBossHealth;

	// Token: 0x0400128F RID: 4751
	[SerializeField]
	private int m_raidBossCardId;

	// Token: 0x04001290 RID: 4752
	[SerializeField]
	private bool m_allowsAnomaly = true;

	// Token: 0x04001291 RID: 4753
	[SerializeField]
	private int m_pmtProductIdForSingleWingPurchase;

	// Token: 0x04001292 RID: 4754
	[SerializeField]
	private int m_pmtProductIdForThisAndRestOfAdventure;

	// Token: 0x04001293 RID: 4755
	[SerializeField]
	private int m_bookSection;
}
