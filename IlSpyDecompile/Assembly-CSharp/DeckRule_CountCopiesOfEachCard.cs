using System.Collections.Generic;

public class DeckRule_CountCopiesOfEachCard : DeckRule
{
	public DeckRule_CountCopiesOfEachCard(int subset, int min, int max)
	{
		m_ruleType = RuleType.COUNT_COPIES_OF_EACH_CARD;
		m_appliesToSubsetId = subset;
		m_minValue = min;
		m_maxValue = max;
		if (m_appliesToSubsetId != 0)
		{
			m_appliesToSubset = GameDbf.GetIndex().GetSubsetById(m_appliesToSubsetId);
		}
	}

	public DeckRule_CountCopiesOfEachCard(DeckRulesetRuleDbfRecord record)
		: base(RuleType.COUNT_COPIES_OF_EACH_CARD, record)
	{
	}

	public override bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		if (!AppliesTo(def.GetCardId()))
		{
			return true;
		}
		int cardIdCount = deck.GetCardIdCount(def.GetCardId());
		int num = GameUtils.TranslateCardIdToDbId(def.GetCardId());
		bool flag = cardIdCount + deck.GetCardCountMatchingTag(GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID, num) + deck.GetCardIdCount(GameUtils.TranslateDbIdToCardId(GameUtils.GetCardTagValue(num, GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID))) >= m_maxValue;
		if (flag)
		{
			reason = new RuleInvalidReason(GameStrings.Format("GLUE_COLLECTION_LOCK_MAX_DECK_COPIES", m_maxValue), m_maxValue);
		}
		return GetResult(!flag);
	}

	public bool GetMaxCopies(EntityDef def, out int maxCopies)
	{
		maxCopies = int.MaxValue;
		if (!AppliesTo(def.GetCardId()))
		{
			return false;
		}
		maxCopies = m_maxValue;
		return true;
	}

	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		bool val = true;
		List<CollectionDeckSlot> slots = deck.GetSlots();
		int countParam = 0;
		bool isMinimum = false;
		foreach (CollectionDeckSlot item in slots)
		{
			string cardID = item.CardID;
			if (AppliesTo(cardID))
			{
				int cardIdCount = deck.GetCardIdCount(cardID);
				if (cardIdCount < m_minValue)
				{
					val = false;
					countParam = m_minValue - cardIdCount;
					isMinimum = true;
					break;
				}
				if (cardIdCount > m_maxValue)
				{
					val = false;
					countParam = (cardIdCount = m_maxValue);
					break;
				}
			}
		}
		val = GetResult(val);
		if (!val)
		{
			reason = new RuleInvalidReason(m_errorString, countParam, isMinimum);
		}
		return val;
	}
}
