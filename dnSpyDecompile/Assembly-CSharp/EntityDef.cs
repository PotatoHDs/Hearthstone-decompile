using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using UnityEngine;

// Token: 0x020008A3 RID: 2211
public class EntityDef : EntityBase
{
	// Token: 0x06007AD1 RID: 31441 RVA: 0x0027D8C6 File Offset: 0x0027BAC6
	public override string ToString()
	{
		return this.GetDebugName();
	}

	// Token: 0x06007AD2 RID: 31442 RVA: 0x0027D8CE File Offset: 0x0027BACE
	public EntityDef Clone()
	{
		EntityDef entityDef = new EntityDef();
		entityDef.m_cardId = base.m_cardId;
		entityDef.ReplaceTags(this.m_tags);
		entityDef.m_referencedTags.Replace(this.m_referencedTags);
		return entityDef;
	}

	// Token: 0x06007AD3 RID: 31443 RVA: 0x0027D8FE File Offset: 0x0027BAFE
	public TagMap GetReferencedTags()
	{
		return this.m_referencedTags;
	}

	// Token: 0x06007AD4 RID: 31444 RVA: 0x0027D906 File Offset: 0x0027BB06
	public override int GetReferencedTag(int tag)
	{
		return this.m_referencedTags.GetTag(tag);
	}

	// Token: 0x06007AD5 RID: 31445 RVA: 0x0027D914 File Offset: 0x0027BB14
	public void SetReferencedTag(GAME_TAG enumTag, int val)
	{
		this.SetReferencedTag((int)enumTag, val);
	}

	// Token: 0x06007AD6 RID: 31446 RVA: 0x0027D91E File Offset: 0x0027BB1E
	public void SetReferencedTag(int tag, int val)
	{
		this.m_referencedTags.SetTag(tag, val);
	}

	// Token: 0x06007AD7 RID: 31447 RVA: 0x0027D92D File Offset: 0x0027BB2D
	public TAG_RACE GetRace()
	{
		return base.GetTag<TAG_RACE>(GAME_TAG.CARDRACE);
	}

	// Token: 0x06007AD8 RID: 31448 RVA: 0x0027D93A File Offset: 0x0027BB3A
	public TAG_ENCHANTMENT_VISUAL GetEnchantmentBirthVisual()
	{
		return base.GetTag<TAG_ENCHANTMENT_VISUAL>(GAME_TAG.ENCHANTMENT_BIRTH_VISUAL);
	}

	// Token: 0x06007AD9 RID: 31449 RVA: 0x0027D947 File Offset: 0x0027BB47
	public TAG_ENCHANTMENT_VISUAL GetEnchantmentIdleVisual()
	{
		return base.GetTag<TAG_ENCHANTMENT_VISUAL>(GAME_TAG.ENCHANTMENT_IDLE_VISUAL);
	}

	// Token: 0x06007ADA RID: 31450 RVA: 0x0027D954 File Offset: 0x0027BB54
	public TAG_RARITY GetRarity()
	{
		return base.GetTag<TAG_RARITY>(GAME_TAG.RARITY);
	}

	// Token: 0x06007ADB RID: 31451 RVA: 0x0027D964 File Offset: 0x0027BB64
	public bool HasValidDisplayName()
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(base.m_cardId);
		return cardRecord != null && cardRecord.Name != null && cardRecord.Name.GetString(true) != null;
	}

	// Token: 0x06007ADC RID: 31452 RVA: 0x0027D99E File Offset: 0x0027BB9E
	public string GetName()
	{
		if (!this.IsValidEntityName())
		{
			this.UpdateEntityName();
		}
		return this.m_cachedEntityName.Name;
	}

	// Token: 0x06007ADD RID: 31453 RVA: 0x0027D9BC File Offset: 0x0027BBBC
	public string GetShortName()
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(base.m_cardId);
		if (cardRecord != null && cardRecord.ShortName != null)
		{
			return cardRecord.ShortName.GetString(true);
		}
		return null;
	}

	// Token: 0x06007ADE RID: 31454 RVA: 0x0027D9F3 File Offset: 0x0027BBF3
	public string GetDebugName()
	{
		if (!this.IsValidEntityDebugName())
		{
			this.UpdateEntityDebugName();
		}
		return this.m_cachedEntityDebugName.Name;
	}

	// Token: 0x06007ADF RID: 31455 RVA: 0x0027DA10 File Offset: 0x0027BC10
	public string GetArtistName()
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(base.m_cardId);
		if (cardRecord == null)
		{
			return "ERROR: NO ARTIST NAME";
		}
		return cardRecord.ArtistName ?? string.Empty;
	}

	// Token: 0x06007AE0 RID: 31456 RVA: 0x0027DA48 File Offset: 0x0027BC48
	public string GetFlavorText()
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(base.m_cardId);
		if (cardRecord == null || cardRecord.FlavorText == null)
		{
			return string.Empty;
		}
		return cardRecord.FlavorText.GetString(true) ?? string.Empty;
	}

	// Token: 0x06007AE1 RID: 31457 RVA: 0x0027DA8C File Offset: 0x0027BC8C
	public string GetHowToEarnText(TAG_PREMIUM premium)
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(base.m_cardId);
		if (cardRecord == null)
		{
			return string.Empty;
		}
		if (premium != TAG_PREMIUM.GOLDEN)
		{
			if (premium != TAG_PREMIUM.DIAMOND)
			{
				if (cardRecord.HowToGetCard != null)
				{
					return cardRecord.HowToGetCard.GetString(true) ?? string.Empty;
				}
			}
			else if (cardRecord.HowToGetDiamondCard != null)
			{
				return cardRecord.HowToGetDiamondCard.GetString(true) ?? string.Empty;
			}
		}
		else if (cardRecord.HowToGetGoldCard != null)
		{
			return cardRecord.HowToGetGoldCard.GetString(true) ?? string.Empty;
		}
		return string.Empty;
	}

	// Token: 0x06007AE2 RID: 31458 RVA: 0x0027DB1C File Offset: 0x0027BD1C
	public string GetCardTextInHand()
	{
		if (this.GetCardTextBuilder() != null)
		{
			return this.GetCardTextBuilder().BuildCardTextInHand(this);
		}
		Debug.LogWarning(string.Format("EntityDef.GetCardTextInHand: No textbuilder found for {0}, returning default text", base.m_cardId));
		return CardTextBuilder.GetDefaultCardTextInHand(this);
	}

	// Token: 0x06007AE3 RID: 31459 RVA: 0x0027DB50 File Offset: 0x0027BD50
	public string GetRaceText()
	{
		if (base.HasTag(GAME_TAG.CARDRACE))
		{
			return GameStrings.GetRaceName(this.GetRace());
		}
		if (base.IsSpell() && base.HasTag(GAME_TAG.SPELL_SCHOOL))
		{
			return GameStrings.GetSpellSchoolName(base.GetSpellSchool());
		}
		return "";
	}

	// Token: 0x06007AE4 RID: 31460 RVA: 0x0027DB9C File Offset: 0x0027BD9C
	public CardTextBuilder GetCardTextBuilder()
	{
		if (this.m_cardTextBuilder == null)
		{
			if (base.HasTag(GAME_TAG.OVERRIDECARDTEXTBUILDER))
			{
				this.m_cardTextBuilder = CardTextBuilderFactory.Create((Assets.Card.CardTextBuilderType)base.GetTag(GAME_TAG.OVERRIDECARDTEXTBUILDER));
			}
			else
			{
				CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(base.m_cardId);
				if (cardRecord != null)
				{
					this.m_cardTextBuilder = CardTextBuilderFactory.Create(cardRecord.CardTextBuilderType);
				}
			}
		}
		return this.m_cardTextBuilder;
	}

	// Token: 0x06007AE5 RID: 31461 RVA: 0x0027DC01 File Offset: 0x0027BE01
	public void ClearCardTextBuilder()
	{
		this.m_cardTextBuilder = null;
	}

	// Token: 0x06007AE6 RID: 31462 RVA: 0x0027DC0C File Offset: 0x0027BE0C
	public string GetWatermarkTextureOverride()
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(base.m_cardId);
		if (cardRecord == null)
		{
			return null;
		}
		return cardRecord.WatermarkTextureOverride;
	}

	// Token: 0x06007AE7 RID: 31463 RVA: 0x0027DC38 File Offset: 0x0027BE38
	public bool LoadTagFromDBF(string designCode)
	{
		base.m_cardId = designCode;
		IEnumerable<CardTagDbfRecord> cardTagRecords = GameUtils.GetCardTagRecords(base.m_cardId);
		if (cardTagRecords == null)
		{
			Debug.LogError(string.Format("EntityDef.LoadDataFromCardXml() - No tags found for the card: {0}", base.m_cardId));
			return false;
		}
		int num = cardTagRecords.Count<CardTagDbfRecord>();
		if (num > EntityBase.DEFAULT_TAG_MAP_SIZE)
		{
			this.m_tags = new TagMap(num);
		}
		foreach (CardTagDbfRecord cardTagDbfRecord in cardTagRecords)
		{
			if (cardTagDbfRecord.IsReferenceTag)
			{
				this.SetReferencedTag(cardTagDbfRecord.TagId, cardTagDbfRecord.TagValue);
				if (cardTagDbfRecord.IsPowerKeywordTag)
				{
					base.SetTag(cardTagDbfRecord.TagId, cardTagDbfRecord.TagValue);
				}
			}
			else
			{
				base.SetTag(cardTagDbfRecord.TagId, cardTagDbfRecord.TagValue);
			}
		}
		return true;
	}

	// Token: 0x06007AE8 RID: 31464 RVA: 0x0027DD0C File Offset: 0x0027BF0C
	public static Map<string, EntityDef> LoadBatchCardEntityDefs(List<string> cardIds, out List<string> failedCardIds)
	{
		Map<string, EntityDef> map = new Map<string, EntityDef>(cardIds.Count + 1);
		failedCardIds = new List<string>();
		foreach (string text in cardIds)
		{
			EntityDef entityDef = new EntityDef();
			if (!entityDef.LoadTagFromDBF(text))
			{
				failedCardIds.Add(text);
			}
			else
			{
				map.Add(text, entityDef);
			}
		}
		return map;
	}

	// Token: 0x06007AE9 RID: 31465 RVA: 0x0027DD8C File Offset: 0x0027BF8C
	public bool IsValidEntityName()
	{
		int tag = base.GetTag(GAME_TAG.OVERRIDECARDNAME);
		return this.m_cachedEntityName.OverrideCardNameValue == tag && this.m_cachedEntityName.CardId == base.m_cardId && !string.IsNullOrEmpty(this.m_cachedEntityName.Name);
	}

	// Token: 0x06007AEA RID: 31466 RVA: 0x0027DDE0 File Offset: 0x0027BFE0
	public bool IsValidEntityDebugName()
	{
		return this.m_cachedEntityDebugName.CardId == base.m_cardId && this.m_cachedEntityDebugName.CardType == base.GetCardType() && !string.IsNullOrEmpty(this.m_cachedEntityName.Name);
	}

	// Token: 0x06007AEB RID: 31467 RVA: 0x0027DE30 File Offset: 0x0027C030
	private void UpdateEntityName()
	{
		int tag = base.GetTag(GAME_TAG.OVERRIDECARDNAME);
		this.m_cachedEntityName.OverrideCardNameValue = tag;
		this.m_cachedEntityName.CardId = base.m_cardId;
		if (tag > 0)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(tag, true);
			if (entityDef != null)
			{
				this.m_cachedEntityName.Name = entityDef.GetName();
				return;
			}
		}
		if (this.GetCardTextBuilder() != null)
		{
			this.m_cachedEntityName.Name = this.GetCardTextBuilder().BuildCardName(this);
			return;
		}
		this.m_cachedEntityName.Name = CardTextBuilder.GetDefaultCardName(this);
	}

	// Token: 0x06007AEC RID: 31468 RVA: 0x0027DEC0 File Offset: 0x0027C0C0
	private void UpdateEntityDebugName()
	{
		string text = null;
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(base.m_cardId);
		if (cardRecord != null && cardRecord.Name != null)
		{
			text = cardRecord.Name.GetString(true);
		}
		TAG_CARDTYPE cardType = base.GetCardType();
		this.m_cachedEntityDebugName.CardId = base.m_cardId;
		this.m_cachedEntityDebugName.CardType = cardType;
		if (text != null)
		{
			this.m_cachedEntityDebugName.Name = string.Format("[name={0} cardId={1} type={2}]", text, base.m_cardId, cardType);
			return;
		}
		if (base.m_cardId != null)
		{
			this.m_cachedEntityDebugName.Name = string.Format("[cardId={0} type={1}]", base.m_cardId, cardType);
			return;
		}
		this.m_cachedEntityDebugName.Name = string.Format("UNKNOWN ENTITY [cardType={0}]", cardType);
	}

	// Token: 0x04005ED4 RID: 24276
	protected TagMap m_referencedTags = new TagMap();

	// Token: 0x04005ED5 RID: 24277
	private CardTextBuilder m_cardTextBuilder;

	// Token: 0x04005ED6 RID: 24278
	private static readonly CardPortraitQuality s_noTextureQuality = new CardPortraitQuality(0, false);

	// Token: 0x04005ED7 RID: 24279
	private EntityDef.CachedEntityName m_cachedEntityName;

	// Token: 0x04005ED8 RID: 24280
	private EntityDef.CachedEntityDebugName m_cachedEntityDebugName;

	// Token: 0x02002528 RID: 9512
	private struct CachedEntityName
	{
		// Token: 0x0400ECCA RID: 60618
		public string Name;

		// Token: 0x0400ECCB RID: 60619
		public int OverrideCardNameValue;

		// Token: 0x0400ECCC RID: 60620
		public string CardId;
	}

	// Token: 0x02002529 RID: 9513
	private struct CachedEntityDebugName
	{
		// Token: 0x0400ECCD RID: 60621
		public string Name;

		// Token: 0x0400ECCE RID: 60622
		public string CardId;

		// Token: 0x0400ECCF RID: 60623
		public TAG_CARDTYPE CardType;
	}
}
