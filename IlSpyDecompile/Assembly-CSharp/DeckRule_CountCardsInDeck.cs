using UnityEngine;

public class DeckRule_CountCardsInDeck : DeckRule
{
	public DeckRule_CountCardsInDeck(int min, int max)
	{
		m_ruleType = RuleType.COUNT_CARDS_IN_DECK;
		m_minValue = min;
		m_maxValue = max;
	}

	public DeckRule_CountCardsInDeck(DeckRulesetRuleDbfRecord record)
		: base(RuleType.COUNT_CARDS_IN_DECK, record)
	{
		if (m_appliesToSubset == null)
		{
			Debug.LogError("COUNT_CARDS_IN_DECK only supports rules with a defined \"applies to\" subset");
		}
	}

	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		int cardCountInSet = deck.GetCardCountInSet(m_appliesToSubset, m_appliesToIsNot);
		int countParam = 0;
		bool isMinimum = false;
		bool val = true;
		if (cardCountInSet < m_minValue)
		{
			val = false;
			countParam = m_minValue - cardCountInSet;
			isMinimum = true;
		}
		else if (cardCountInSet > m_maxValue)
		{
			val = false;
			countParam = cardCountInSet - m_maxValue;
		}
		bool result = GetResult(val);
		if (!result)
		{
			reason = new RuleInvalidReason(m_errorString, countParam, isMinimum);
		}
		return result;
	}
}
