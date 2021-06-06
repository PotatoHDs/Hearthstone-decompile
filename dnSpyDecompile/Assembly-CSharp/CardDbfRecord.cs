using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000198 RID: 408
[Serializable]
public class CardDbfRecord : DbfRecord
{
	// Token: 0x17000231 RID: 561
	// (get) Token: 0x060018BE RID: 6334 RVA: 0x0008646A File Offset: 0x0008466A
	[DbfField("NOTE_MINI_GUID")]
	public string NoteMiniGuid
	{
		get
		{
			return this.m_noteMiniGuid;
		}
	}

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x060018BF RID: 6335 RVA: 0x00086472 File Offset: 0x00084672
	[DbfField("LONG_GUID")]
	public string LongGuid
	{
		get
		{
			return this.m_longGuid;
		}
	}

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0008647A File Offset: 0x0008467A
	[DbfField("TEXT_IN_HAND")]
	public DbfLocValue TextInHand
	{
		get
		{
			return this.m_textInHand;
		}
	}

	// Token: 0x17000234 RID: 564
	// (get) Token: 0x060018C1 RID: 6337 RVA: 0x00086482 File Offset: 0x00084682
	[DbfField("GAMEPLAY_EVENT")]
	public string GameplayEvent
	{
		get
		{
			return this.m_gameplayEvent;
		}
	}

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x060018C2 RID: 6338 RVA: 0x0008648A File Offset: 0x0008468A
	[DbfField("CRAFTING_EVENT")]
	public string CraftingEvent
	{
		get
		{
			return this.m_craftingEvent;
		}
	}

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x060018C3 RID: 6339 RVA: 0x00086492 File Offset: 0x00084692
	[DbfField("GOLDEN_CRAFTING_EVENT")]
	public string GoldenCraftingEvent
	{
		get
		{
			return this.m_goldenCraftingEvent;
		}
	}

	// Token: 0x17000237 RID: 567
	// (get) Token: 0x060018C4 RID: 6340 RVA: 0x0008649A File Offset: 0x0008469A
	[DbfField("SUGGESTION_WEIGHT")]
	public int SuggestionWeight
	{
		get
		{
			return this.m_suggestionWeight;
		}
	}

	// Token: 0x17000238 RID: 568
	// (get) Token: 0x060018C5 RID: 6341 RVA: 0x000864A2 File Offset: 0x000846A2
	[DbfField("CHANGE_VERSION")]
	public int ChangeVersion
	{
		get
		{
			return this.m_changeVersion;
		}
	}

	// Token: 0x17000239 RID: 569
	// (get) Token: 0x060018C6 RID: 6342 RVA: 0x000864AA File Offset: 0x000846AA
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x1700023A RID: 570
	// (get) Token: 0x060018C7 RID: 6343 RVA: 0x000864B2 File Offset: 0x000846B2
	[DbfField("FLAVOR_TEXT")]
	public DbfLocValue FlavorText
	{
		get
		{
			return this.m_flavorText;
		}
	}

	// Token: 0x1700023B RID: 571
	// (get) Token: 0x060018C8 RID: 6344 RVA: 0x000864BA File Offset: 0x000846BA
	[DbfField("HOW_TO_GET_CARD")]
	public DbfLocValue HowToGetCard
	{
		get
		{
			return this.m_howToGetCard;
		}
	}

	// Token: 0x1700023C RID: 572
	// (get) Token: 0x060018C9 RID: 6345 RVA: 0x000864C2 File Offset: 0x000846C2
	[DbfField("HOW_TO_GET_GOLD_CARD")]
	public DbfLocValue HowToGetGoldCard
	{
		get
		{
			return this.m_howToGetGoldCard;
		}
	}

	// Token: 0x1700023D RID: 573
	// (get) Token: 0x060018CA RID: 6346 RVA: 0x000864CA File Offset: 0x000846CA
	[DbfField("HOW_TO_GET_DIAMOND_CARD")]
	public DbfLocValue HowToGetDiamondCard
	{
		get
		{
			return this.m_howToGetDiamondCard;
		}
	}

	// Token: 0x1700023E RID: 574
	// (get) Token: 0x060018CB RID: 6347 RVA: 0x000864D2 File Offset: 0x000846D2
	[DbfField("TARGET_ARROW_TEXT")]
	public DbfLocValue TargetArrowText
	{
		get
		{
			return this.m_targetArrowText;
		}
	}

	// Token: 0x1700023F RID: 575
	// (get) Token: 0x060018CC RID: 6348 RVA: 0x000864DA File Offset: 0x000846DA
	[DbfField("ARTIST_NAME")]
	public string ArtistName
	{
		get
		{
			return this.m_artistName;
		}
	}

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x060018CD RID: 6349 RVA: 0x000864E2 File Offset: 0x000846E2
	[DbfField("SHORT_NAME")]
	public DbfLocValue ShortName
	{
		get
		{
			return this.m_shortName;
		}
	}

	// Token: 0x17000241 RID: 577
	// (get) Token: 0x060018CE RID: 6350 RVA: 0x000864EA File Offset: 0x000846EA
	[DbfField("CREDITS_CARD_NAME")]
	public string CreditsCardName
	{
		get
		{
			return this.m_creditsCardName;
		}
	}

	// Token: 0x17000242 RID: 578
	// (get) Token: 0x060018CF RID: 6351 RVA: 0x000864F2 File Offset: 0x000846F2
	[DbfField("FEATURED_CARDS_EVENT")]
	public string FeaturedCardsEvent
	{
		get
		{
			return this.m_featuredCardsEvent;
		}
	}

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x060018D0 RID: 6352 RVA: 0x000864FA File Offset: 0x000846FA
	[DbfField("CARD_TEXT_BUILDER_TYPE")]
	public Assets.Card.CardTextBuilderType CardTextBuilderType
	{
		get
		{
			return this.m_cardTextBuilderType;
		}
	}

	// Token: 0x17000244 RID: 580
	// (get) Token: 0x060018D1 RID: 6353 RVA: 0x00086502 File Offset: 0x00084702
	[DbfField("WATERMARK_TEXTURE_OVERRIDE")]
	public string WatermarkTextureOverride
	{
		get
		{
			return this.m_watermarkTextureOverride;
		}
	}

	// Token: 0x17000245 RID: 581
	// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0008650A File Offset: 0x0008470A
	public CardHeroDbfRecord CardHero
	{
		get
		{
			return GameDbf.CardHero.GetRecord((CardHeroDbfRecord r) => r.CardId == base.ID);
		}
	}

	// Token: 0x17000246 RID: 582
	// (get) Token: 0x060018D3 RID: 6355 RVA: 0x00086522 File Offset: 0x00084722
	public CardPlayerDeckOverrideDbfRecord PlayerDeckOverride
	{
		get
		{
			return GameDbf.CardPlayerDeckOverride.GetRecord((CardPlayerDeckOverrideDbfRecord r) => r.CardId == base.ID);
		}
	}

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0008653A File Offset: 0x0008473A
	public List<CardAdditonalSearchTermsDbfRecord> SearchTerms
	{
		get
		{
			return GameDbf.CardAdditonalSearchTerms.GetRecords((CardAdditonalSearchTermsDbfRecord r) => r.CardId == base.ID, -1);
		}
	}

	// Token: 0x17000248 RID: 584
	// (get) Token: 0x060018D5 RID: 6357 RVA: 0x00086553 File Offset: 0x00084753
	public List<CardChangeDbfRecord> CardChanges
	{
		get
		{
			return GameDbf.CardChange.GetRecords((CardChangeDbfRecord r) => r.CardId == base.ID, -1);
		}
	}

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x060018D6 RID: 6358 RVA: 0x0008656C File Offset: 0x0008476C
	public List<CardSetTimingDbfRecord> CardSetTimings
	{
		get
		{
			return GameDbf.CardSetTiming.GetRecords((CardSetTimingDbfRecord r) => r.CardId == base.ID, -1);
		}
	}

	// Token: 0x1700024A RID: 586
	// (get) Token: 0x060018D7 RID: 6359 RVA: 0x00086585 File Offset: 0x00084785
	public List<CardTagDbfRecord> Tags
	{
		get
		{
			return GameDbf.CardTag.GetRecords((CardTagDbfRecord r) => r.CardId == base.ID, -1);
		}
	}

	// Token: 0x060018D8 RID: 6360 RVA: 0x0008659E File Offset: 0x0008479E
	public void SetNoteMiniGuid(string v)
	{
		this.m_noteMiniGuid = v;
	}

	// Token: 0x060018D9 RID: 6361 RVA: 0x000865A7 File Offset: 0x000847A7
	public void SetLongGuid(string v)
	{
		this.m_longGuid = v;
	}

	// Token: 0x060018DA RID: 6362 RVA: 0x000865B0 File Offset: 0x000847B0
	public void SetTextInHand(DbfLocValue v)
	{
		this.m_textInHand = v;
		v.SetDebugInfo(base.ID, "TEXT_IN_HAND");
	}

	// Token: 0x060018DB RID: 6363 RVA: 0x000865CA File Offset: 0x000847CA
	public void SetGameplayEvent(string v)
	{
		this.m_gameplayEvent = v;
	}

	// Token: 0x060018DC RID: 6364 RVA: 0x000865D3 File Offset: 0x000847D3
	public void SetCraftingEvent(string v)
	{
		this.m_craftingEvent = v;
	}

	// Token: 0x060018DD RID: 6365 RVA: 0x000865DC File Offset: 0x000847DC
	public void SetGoldenCraftingEvent(string v)
	{
		this.m_goldenCraftingEvent = v;
	}

	// Token: 0x060018DE RID: 6366 RVA: 0x000865E5 File Offset: 0x000847E5
	public void SetSuggestionWeight(int v)
	{
		this.m_suggestionWeight = v;
	}

	// Token: 0x060018DF RID: 6367 RVA: 0x000865EE File Offset: 0x000847EE
	public void SetChangeVersion(int v)
	{
		this.m_changeVersion = v;
	}

	// Token: 0x060018E0 RID: 6368 RVA: 0x000865F7 File Offset: 0x000847F7
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x060018E1 RID: 6369 RVA: 0x00086611 File Offset: 0x00084811
	public void SetFlavorText(DbfLocValue v)
	{
		this.m_flavorText = v;
		v.SetDebugInfo(base.ID, "FLAVOR_TEXT");
	}

	// Token: 0x060018E2 RID: 6370 RVA: 0x0008662B File Offset: 0x0008482B
	public void SetHowToGetCard(DbfLocValue v)
	{
		this.m_howToGetCard = v;
		v.SetDebugInfo(base.ID, "HOW_TO_GET_CARD");
	}

	// Token: 0x060018E3 RID: 6371 RVA: 0x00086645 File Offset: 0x00084845
	public void SetHowToGetGoldCard(DbfLocValue v)
	{
		this.m_howToGetGoldCard = v;
		v.SetDebugInfo(base.ID, "HOW_TO_GET_GOLD_CARD");
	}

	// Token: 0x060018E4 RID: 6372 RVA: 0x0008665F File Offset: 0x0008485F
	public void SetHowToGetDiamondCard(DbfLocValue v)
	{
		this.m_howToGetDiamondCard = v;
		v.SetDebugInfo(base.ID, "HOW_TO_GET_DIAMOND_CARD");
	}

	// Token: 0x060018E5 RID: 6373 RVA: 0x00086679 File Offset: 0x00084879
	public void SetTargetArrowText(DbfLocValue v)
	{
		this.m_targetArrowText = v;
		v.SetDebugInfo(base.ID, "TARGET_ARROW_TEXT");
	}

	// Token: 0x060018E6 RID: 6374 RVA: 0x00086693 File Offset: 0x00084893
	public void SetArtistName(string v)
	{
		this.m_artistName = v;
	}

	// Token: 0x060018E7 RID: 6375 RVA: 0x0008669C File Offset: 0x0008489C
	public void SetShortName(DbfLocValue v)
	{
		this.m_shortName = v;
		v.SetDebugInfo(base.ID, "SHORT_NAME");
	}

	// Token: 0x060018E8 RID: 6376 RVA: 0x000866B6 File Offset: 0x000848B6
	public void SetCreditsCardName(string v)
	{
		this.m_creditsCardName = v;
	}

	// Token: 0x060018E9 RID: 6377 RVA: 0x000866BF File Offset: 0x000848BF
	public void SetFeaturedCardsEvent(string v)
	{
		this.m_featuredCardsEvent = v;
	}

	// Token: 0x060018EA RID: 6378 RVA: 0x000866C8 File Offset: 0x000848C8
	public void SetCardTextBuilderType(Assets.Card.CardTextBuilderType v)
	{
		this.m_cardTextBuilderType = v;
	}

	// Token: 0x060018EB RID: 6379 RVA: 0x000866D1 File Offset: 0x000848D1
	public void SetWatermarkTextureOverride(string v)
	{
		this.m_watermarkTextureOverride = v;
	}

	// Token: 0x060018EC RID: 6380 RVA: 0x000866DC File Offset: 0x000848DC
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2694405912U)
		{
			if (num <= 1458105184U)
			{
				if (num <= 752250442U)
				{
					if (num != 605406857U)
					{
						if (num == 752250442U)
						{
							if (name == "HOW_TO_GET_GOLD_CARD")
							{
								return this.m_howToGetGoldCard;
							}
						}
					}
					else if (name == "FLAVOR_TEXT")
					{
						return this.m_flavorText;
					}
				}
				else if (num != 1015438284U)
				{
					if (num != 1387956774U)
					{
						if (num == 1458105184U)
						{
							if (name == "ID")
							{
								return base.ID;
							}
						}
					}
					else if (name == "NAME")
					{
						return this.m_name;
					}
				}
				else if (name == "FEATURED_CARDS_EVENT")
				{
					return this.m_featuredCardsEvent;
				}
			}
			else if (num <= 1677273194U)
			{
				if (num != 1667820126U)
				{
					if (num == 1677273194U)
					{
						if (name == "GAMEPLAY_EVENT")
						{
							return this.m_gameplayEvent;
						}
					}
				}
				else if (name == "SUGGESTION_WEIGHT")
				{
					return this.m_suggestionWeight;
				}
			}
			else if (num != 1957941545U)
			{
				if (num != 2635681263U)
				{
					if (num == 2694405912U)
					{
						if (name == "ARTIST_NAME")
						{
							return this.m_artistName;
						}
					}
				}
				else if (name == "HOW_TO_GET_CARD")
				{
					return this.m_howToGetCard;
				}
			}
			else if (name == "LONG_GUID")
			{
				return this.m_longGuid;
			}
		}
		else if (num <= 3336689320U)
		{
			if (num <= 2914581156U)
			{
				if (num != 2741045532U)
				{
					if (num == 2914581156U)
					{
						if (name == "GOLDEN_CRAFTING_EVENT")
						{
							return this.m_goldenCraftingEvent;
						}
					}
				}
				else if (name == "TARGET_ARROW_TEXT")
				{
					return this.m_targetArrowText;
				}
			}
			else if (num != 3023772136U)
			{
				if (num != 3226467965U)
				{
					if (num == 3336689320U)
					{
						if (name == "CRAFTING_EVENT")
						{
							return this.m_craftingEvent;
						}
					}
				}
				else if (name == "SHORT_NAME")
				{
					return this.m_shortName;
				}
			}
			else if (name == "CARD_TEXT_BUILDER_TYPE")
			{
				return this.m_cardTextBuilderType;
			}
		}
		else if (num <= 3794169416U)
		{
			if (num != 3593298050U)
			{
				if (num != 3632971787U)
				{
					if (num == 3794169416U)
					{
						if (name == "TEXT_IN_HAND")
						{
							return this.m_textInHand;
						}
					}
				}
				else if (name == "NOTE_MINI_GUID")
				{
					return this.m_noteMiniGuid;
				}
			}
			else if (name == "CREDITS_CARD_NAME")
			{
				return this.m_creditsCardName;
			}
		}
		else if (num != 3802577366U)
		{
			if (num != 4085651046U)
			{
				if (num == 4209797098U)
				{
					if (name == "CHANGE_VERSION")
					{
						return this.m_changeVersion;
					}
				}
			}
			else if (name == "WATERMARK_TEXTURE_OVERRIDE")
			{
				return this.m_watermarkTextureOverride;
			}
		}
		else if (name == "HOW_TO_GET_DIAMOND_CARD")
		{
			return this.m_howToGetDiamondCard;
		}
		return null;
	}

	// Token: 0x060018ED RID: 6381 RVA: 0x00086A98 File Offset: 0x00084C98
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num > 2694405912U)
		{
			if (num <= 3336689320U)
			{
				if (num <= 2914581156U)
				{
					if (num != 2741045532U)
					{
						if (num != 2914581156U)
						{
							return;
						}
						if (!(name == "GOLDEN_CRAFTING_EVENT"))
						{
							return;
						}
						this.m_goldenCraftingEvent = (string)val;
						return;
					}
					else
					{
						if (!(name == "TARGET_ARROW_TEXT"))
						{
							return;
						}
						this.m_targetArrowText = (DbfLocValue)val;
						return;
					}
				}
				else if (num != 3023772136U)
				{
					if (num != 3226467965U)
					{
						if (num != 3336689320U)
						{
							return;
						}
						if (!(name == "CRAFTING_EVENT"))
						{
							return;
						}
						this.m_craftingEvent = (string)val;
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
					if (!(name == "CARD_TEXT_BUILDER_TYPE"))
					{
						return;
					}
					if (val == null)
					{
						this.m_cardTextBuilderType = Assets.Card.CardTextBuilderType.DEFAULT;
						return;
					}
					if (val is Assets.Card.CardTextBuilderType || val is int)
					{
						this.m_cardTextBuilderType = (Assets.Card.CardTextBuilderType)val;
						return;
					}
					if (val is string)
					{
						this.m_cardTextBuilderType = Assets.Card.ParseCardTextBuilderTypeValue((string)val);
						return;
					}
				}
			}
			else if (num <= 3794169416U)
			{
				if (num != 3593298050U)
				{
					if (num != 3632971787U)
					{
						if (num != 3794169416U)
						{
							return;
						}
						if (!(name == "TEXT_IN_HAND"))
						{
							return;
						}
						this.m_textInHand = (DbfLocValue)val;
						return;
					}
					else
					{
						if (!(name == "NOTE_MINI_GUID"))
						{
							return;
						}
						this.m_noteMiniGuid = (string)val;
						return;
					}
				}
				else
				{
					if (!(name == "CREDITS_CARD_NAME"))
					{
						return;
					}
					this.m_creditsCardName = (string)val;
					return;
				}
			}
			else if (num != 3802577366U)
			{
				if (num != 4085651046U)
				{
					if (num != 4209797098U)
					{
						return;
					}
					if (!(name == "CHANGE_VERSION"))
					{
						return;
					}
					this.m_changeVersion = (int)val;
					return;
				}
				else
				{
					if (!(name == "WATERMARK_TEXTURE_OVERRIDE"))
					{
						return;
					}
					this.m_watermarkTextureOverride = (string)val;
				}
			}
			else
			{
				if (!(name == "HOW_TO_GET_DIAMOND_CARD"))
				{
					return;
				}
				this.m_howToGetDiamondCard = (DbfLocValue)val;
				return;
			}
			return;
		}
		if (num <= 1458105184U)
		{
			if (num <= 752250442U)
			{
				if (num != 605406857U)
				{
					if (num != 752250442U)
					{
						return;
					}
					if (!(name == "HOW_TO_GET_GOLD_CARD"))
					{
						return;
					}
					this.m_howToGetGoldCard = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "FLAVOR_TEXT"))
					{
						return;
					}
					this.m_flavorText = (DbfLocValue)val;
					return;
				}
			}
			else if (num != 1015438284U)
			{
				if (num != 1387956774U)
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
					if (!(name == "NAME"))
					{
						return;
					}
					this.m_name = (DbfLocValue)val;
					return;
				}
			}
			else
			{
				if (!(name == "FEATURED_CARDS_EVENT"))
				{
					return;
				}
				this.m_featuredCardsEvent = (string)val;
				return;
			}
		}
		else if (num <= 1677273194U)
		{
			if (num != 1667820126U)
			{
				if (num != 1677273194U)
				{
					return;
				}
				if (!(name == "GAMEPLAY_EVENT"))
				{
					return;
				}
				this.m_gameplayEvent = (string)val;
				return;
			}
			else
			{
				if (!(name == "SUGGESTION_WEIGHT"))
				{
					return;
				}
				this.m_suggestionWeight = (int)val;
				return;
			}
		}
		else if (num != 1957941545U)
		{
			if (num != 2635681263U)
			{
				if (num != 2694405912U)
				{
					return;
				}
				if (!(name == "ARTIST_NAME"))
				{
					return;
				}
				this.m_artistName = (string)val;
				return;
			}
			else
			{
				if (!(name == "HOW_TO_GET_CARD"))
				{
					return;
				}
				this.m_howToGetCard = (DbfLocValue)val;
				return;
			}
		}
		else
		{
			if (!(name == "LONG_GUID"))
			{
				return;
			}
			this.m_longGuid = (string)val;
			return;
		}
	}

	// Token: 0x060018EE RID: 6382 RVA: 0x00086E7C File Offset: 0x0008507C
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2694405912U)
		{
			if (num <= 1458105184U)
			{
				if (num <= 752250442U)
				{
					if (num != 605406857U)
					{
						if (num == 752250442U)
						{
							if (name == "HOW_TO_GET_GOLD_CARD")
							{
								return typeof(DbfLocValue);
							}
						}
					}
					else if (name == "FLAVOR_TEXT")
					{
						return typeof(DbfLocValue);
					}
				}
				else if (num != 1015438284U)
				{
					if (num != 1387956774U)
					{
						if (num == 1458105184U)
						{
							if (name == "ID")
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
				else if (name == "FEATURED_CARDS_EVENT")
				{
					return typeof(string);
				}
			}
			else if (num <= 1677273194U)
			{
				if (num != 1667820126U)
				{
					if (num == 1677273194U)
					{
						if (name == "GAMEPLAY_EVENT")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "SUGGESTION_WEIGHT")
				{
					return typeof(int);
				}
			}
			else if (num != 1957941545U)
			{
				if (num != 2635681263U)
				{
					if (num == 2694405912U)
					{
						if (name == "ARTIST_NAME")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "HOW_TO_GET_CARD")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (name == "LONG_GUID")
			{
				return typeof(string);
			}
		}
		else if (num <= 3336689320U)
		{
			if (num <= 2914581156U)
			{
				if (num != 2741045532U)
				{
					if (num == 2914581156U)
					{
						if (name == "GOLDEN_CRAFTING_EVENT")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "TARGET_ARROW_TEXT")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num != 3023772136U)
			{
				if (num != 3226467965U)
				{
					if (num == 3336689320U)
					{
						if (name == "CRAFTING_EVENT")
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
			else if (name == "CARD_TEXT_BUILDER_TYPE")
			{
				return typeof(Assets.Card.CardTextBuilderType);
			}
		}
		else if (num <= 3794169416U)
		{
			if (num != 3593298050U)
			{
				if (num != 3632971787U)
				{
					if (num == 3794169416U)
					{
						if (name == "TEXT_IN_HAND")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "NOTE_MINI_GUID")
				{
					return typeof(string);
				}
			}
			else if (name == "CREDITS_CARD_NAME")
			{
				return typeof(string);
			}
		}
		else if (num != 3802577366U)
		{
			if (num != 4085651046U)
			{
				if (num == 4209797098U)
				{
					if (name == "CHANGE_VERSION")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "WATERMARK_TEXTURE_OVERRIDE")
			{
				return typeof(string);
			}
		}
		else if (name == "HOW_TO_GET_DIAMOND_CARD")
		{
			return typeof(DbfLocValue);
		}
		return null;
	}

	// Token: 0x060018EF RID: 6383 RVA: 0x00087275 File Offset: 0x00085475
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardDbfRecords loadRecords = new LoadCardDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060018F0 RID: 6384 RVA: 0x0008728C File Offset: 0x0008548C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardDbfAsset cardDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardDbfAsset)) as CardDbfAsset;
		if (cardDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CardDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < cardDbfAsset.Records.Count; i++)
		{
			cardDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (cardDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060018F1 RID: 6385 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060018F2 RID: 6386 RVA: 0x0008730C File Offset: 0x0008550C
	public override void StripUnusedLocales()
	{
		this.m_textInHand.StripUnusedLocales();
		this.m_name.StripUnusedLocales();
		this.m_flavorText.StripUnusedLocales();
		this.m_howToGetCard.StripUnusedLocales();
		this.m_howToGetGoldCard.StripUnusedLocales();
		this.m_howToGetDiamondCard.StripUnusedLocales();
		this.m_targetArrowText.StripUnusedLocales();
		this.m_shortName.StripUnusedLocales();
	}

	// Token: 0x04000F64 RID: 3940
	[SerializeField]
	private string m_noteMiniGuid;

	// Token: 0x04000F65 RID: 3941
	[SerializeField]
	private string m_longGuid;

	// Token: 0x04000F66 RID: 3942
	[SerializeField]
	private DbfLocValue m_textInHand;

	// Token: 0x04000F67 RID: 3943
	[SerializeField]
	private string m_gameplayEvent = "always";

	// Token: 0x04000F68 RID: 3944
	[SerializeField]
	private string m_craftingEvent = "always";

	// Token: 0x04000F69 RID: 3945
	[SerializeField]
	private string m_goldenCraftingEvent;

	// Token: 0x04000F6A RID: 3946
	[SerializeField]
	private int m_suggestionWeight;

	// Token: 0x04000F6B RID: 3947
	[SerializeField]
	private int m_changeVersion;

	// Token: 0x04000F6C RID: 3948
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x04000F6D RID: 3949
	[SerializeField]
	private DbfLocValue m_flavorText;

	// Token: 0x04000F6E RID: 3950
	[SerializeField]
	private DbfLocValue m_howToGetCard;

	// Token: 0x04000F6F RID: 3951
	[SerializeField]
	private DbfLocValue m_howToGetGoldCard;

	// Token: 0x04000F70 RID: 3952
	[SerializeField]
	private DbfLocValue m_howToGetDiamondCard;

	// Token: 0x04000F71 RID: 3953
	[SerializeField]
	private DbfLocValue m_targetArrowText;

	// Token: 0x04000F72 RID: 3954
	[SerializeField]
	private string m_artistName;

	// Token: 0x04000F73 RID: 3955
	[SerializeField]
	private DbfLocValue m_shortName;

	// Token: 0x04000F74 RID: 3956
	[SerializeField]
	private string m_creditsCardName;

	// Token: 0x04000F75 RID: 3957
	[SerializeField]
	private string m_featuredCardsEvent;

	// Token: 0x04000F76 RID: 3958
	[SerializeField]
	private Assets.Card.CardTextBuilderType m_cardTextBuilderType;

	// Token: 0x04000F77 RID: 3959
	[SerializeField]
	private string m_watermarkTextureOverride;
}
