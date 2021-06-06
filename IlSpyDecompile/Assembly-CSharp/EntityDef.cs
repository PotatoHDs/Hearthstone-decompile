using System.Collections.Generic;
using System.Linq;
using Assets;
using UnityEngine;

public class EntityDef : EntityBase
{
	private struct CachedEntityName
	{
		public string Name;

		public int OverrideCardNameValue;

		public string CardId;
	}

	private struct CachedEntityDebugName
	{
		public string Name;

		public string CardId;

		public TAG_CARDTYPE CardType;
	}

	protected TagMap m_referencedTags = new TagMap();

	private CardTextBuilder m_cardTextBuilder;

	private static readonly CardPortraitQuality s_noTextureQuality = new CardPortraitQuality(0, loadPremium: false);

	private CachedEntityName m_cachedEntityName;

	private CachedEntityDebugName m_cachedEntityDebugName;

	public override string ToString()
	{
		return GetDebugName();
	}

	public EntityDef Clone()
	{
		EntityDef entityDef = new EntityDef();
		entityDef.m_cardId = base.m_cardId;
		entityDef.ReplaceTags(m_tags);
		entityDef.m_referencedTags.Replace(m_referencedTags);
		return entityDef;
	}

	public TagMap GetReferencedTags()
	{
		return m_referencedTags;
	}

	public override int GetReferencedTag(int tag)
	{
		return m_referencedTags.GetTag(tag);
	}

	public void SetReferencedTag(GAME_TAG enumTag, int val)
	{
		SetReferencedTag((int)enumTag, val);
	}

	public void SetReferencedTag(int tag, int val)
	{
		m_referencedTags.SetTag(tag, val);
	}

	public TAG_RACE GetRace()
	{
		return GetTag<TAG_RACE>(GAME_TAG.CARDRACE);
	}

	public TAG_ENCHANTMENT_VISUAL GetEnchantmentBirthVisual()
	{
		return GetTag<TAG_ENCHANTMENT_VISUAL>(GAME_TAG.ENCHANTMENT_BIRTH_VISUAL);
	}

	public TAG_ENCHANTMENT_VISUAL GetEnchantmentIdleVisual()
	{
		return GetTag<TAG_ENCHANTMENT_VISUAL>(GAME_TAG.ENCHANTMENT_IDLE_VISUAL);
	}

	public TAG_RARITY GetRarity()
	{
		return GetTag<TAG_RARITY>(GAME_TAG.RARITY);
	}

	public bool HasValidDisplayName()
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(base.m_cardId);
		if (cardRecord != null && cardRecord.Name != null)
		{
			return cardRecord.Name.GetString() != null;
		}
		return false;
	}

	public string GetName()
	{
		if (!IsValidEntityName())
		{
			UpdateEntityName();
		}
		return m_cachedEntityName.Name;
	}

	public string GetShortName()
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(base.m_cardId);
		if (cardRecord != null && cardRecord.ShortName != null)
		{
			return cardRecord.ShortName.GetString();
		}
		return null;
	}

	public string GetDebugName()
	{
		if (!IsValidEntityDebugName())
		{
			UpdateEntityDebugName();
		}
		return m_cachedEntityDebugName.Name;
	}

	public string GetArtistName()
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(base.m_cardId);
		if (cardRecord == null)
		{
			return "ERROR: NO ARTIST NAME";
		}
		return cardRecord.ArtistName ?? string.Empty;
	}

	public string GetFlavorText()
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(base.m_cardId);
		if (cardRecord == null || cardRecord.FlavorText == null)
		{
			return string.Empty;
		}
		return cardRecord.FlavorText.GetString() ?? string.Empty;
	}

	public string GetHowToEarnText(TAG_PREMIUM premium)
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(base.m_cardId);
		if (cardRecord == null)
		{
			return string.Empty;
		}
		switch (premium)
		{
		case TAG_PREMIUM.GOLDEN:
			if (cardRecord.HowToGetGoldCard != null)
			{
				return cardRecord.HowToGetGoldCard.GetString() ?? string.Empty;
			}
			break;
		case TAG_PREMIUM.DIAMOND:
			if (cardRecord.HowToGetDiamondCard != null)
			{
				return cardRecord.HowToGetDiamondCard.GetString() ?? string.Empty;
			}
			break;
		default:
			if (cardRecord.HowToGetCard != null)
			{
				return cardRecord.HowToGetCard.GetString() ?? string.Empty;
			}
			break;
		}
		return string.Empty;
	}

	public string GetCardTextInHand()
	{
		if (GetCardTextBuilder() != null)
		{
			return GetCardTextBuilder().BuildCardTextInHand(this);
		}
		Debug.LogWarning($"EntityDef.GetCardTextInHand: No textbuilder found for {base.m_cardId}, returning default text");
		return CardTextBuilder.GetDefaultCardTextInHand(this);
	}

	public string GetRaceText()
	{
		if (HasTag(GAME_TAG.CARDRACE))
		{
			return GameStrings.GetRaceName(GetRace());
		}
		if (IsSpell() && HasTag(GAME_TAG.SPELL_SCHOOL))
		{
			return GameStrings.GetSpellSchoolName(GetSpellSchool());
		}
		return "";
	}

	public CardTextBuilder GetCardTextBuilder()
	{
		if (m_cardTextBuilder == null)
		{
			if (HasTag(GAME_TAG.OVERRIDECARDTEXTBUILDER))
			{
				m_cardTextBuilder = CardTextBuilderFactory.Create((Assets.Card.CardTextBuilderType)GetTag(GAME_TAG.OVERRIDECARDTEXTBUILDER));
			}
			else
			{
				CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(base.m_cardId);
				if (cardRecord != null)
				{
					m_cardTextBuilder = CardTextBuilderFactory.Create(cardRecord.CardTextBuilderType);
				}
			}
		}
		return m_cardTextBuilder;
	}

	public void ClearCardTextBuilder()
	{
		m_cardTextBuilder = null;
	}

	public string GetWatermarkTextureOverride()
	{
		return GameDbf.GetIndex().GetCardRecord(base.m_cardId)?.WatermarkTextureOverride;
	}

	public bool LoadTagFromDBF(string designCode)
	{
		base.m_cardId = designCode;
		IEnumerable<CardTagDbfRecord> cardTagRecords = GameUtils.GetCardTagRecords(base.m_cardId);
		if (cardTagRecords == null)
		{
			Debug.LogError($"EntityDef.LoadDataFromCardXml() - No tags found for the card: {base.m_cardId}");
			return false;
		}
		int num = cardTagRecords.Count();
		if (num > EntityBase.DEFAULT_TAG_MAP_SIZE)
		{
			m_tags = new TagMap(num);
		}
		foreach (CardTagDbfRecord item in cardTagRecords)
		{
			if (item.IsReferenceTag)
			{
				SetReferencedTag(item.TagId, item.TagValue);
				if (item.IsPowerKeywordTag)
				{
					SetTag(item.TagId, item.TagValue);
				}
			}
			else
			{
				SetTag(item.TagId, item.TagValue);
			}
		}
		return true;
	}

	public static Map<string, EntityDef> LoadBatchCardEntityDefs(List<string> cardIds, out List<string> failedCardIds)
	{
		Map<string, EntityDef> map = new Map<string, EntityDef>(cardIds.Count + 1);
		failedCardIds = new List<string>();
		foreach (string cardId in cardIds)
		{
			EntityDef entityDef = new EntityDef();
			if (!entityDef.LoadTagFromDBF(cardId))
			{
				failedCardIds.Add(cardId);
			}
			else
			{
				map.Add(cardId, entityDef);
			}
		}
		return map;
	}

	public bool IsValidEntityName()
	{
		int tag = GetTag(GAME_TAG.OVERRIDECARDNAME);
		if (m_cachedEntityName.OverrideCardNameValue == tag && m_cachedEntityName.CardId == base.m_cardId)
		{
			return !string.IsNullOrEmpty(m_cachedEntityName.Name);
		}
		return false;
	}

	public bool IsValidEntityDebugName()
	{
		if (m_cachedEntityDebugName.CardId == base.m_cardId && m_cachedEntityDebugName.CardType == GetCardType())
		{
			return !string.IsNullOrEmpty(m_cachedEntityName.Name);
		}
		return false;
	}

	private void UpdateEntityName()
	{
		int tag = GetTag(GAME_TAG.OVERRIDECARDNAME);
		m_cachedEntityName.OverrideCardNameValue = tag;
		m_cachedEntityName.CardId = base.m_cardId;
		if (tag > 0)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(tag);
			if (entityDef != null)
			{
				m_cachedEntityName.Name = entityDef.GetName();
				return;
			}
		}
		if (GetCardTextBuilder() != null)
		{
			m_cachedEntityName.Name = GetCardTextBuilder().BuildCardName(this);
		}
		else
		{
			m_cachedEntityName.Name = CardTextBuilder.GetDefaultCardName(this);
		}
	}

	private void UpdateEntityDebugName()
	{
		string text = null;
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(base.m_cardId);
		if (cardRecord != null && cardRecord.Name != null)
		{
			text = cardRecord.Name.GetString();
		}
		TAG_CARDTYPE cardType = GetCardType();
		m_cachedEntityDebugName.CardId = base.m_cardId;
		m_cachedEntityDebugName.CardType = cardType;
		if (text != null)
		{
			m_cachedEntityDebugName.Name = $"[name={text} cardId={base.m_cardId} type={cardType}]";
		}
		else if (base.m_cardId != null)
		{
			m_cachedEntityDebugName.Name = $"[cardId={base.m_cardId} type={cardType}]";
		}
		else
		{
			m_cachedEntityDebugName.Name = $"UNKNOWN ENTITY [cardType={cardType}]";
		}
	}
}
