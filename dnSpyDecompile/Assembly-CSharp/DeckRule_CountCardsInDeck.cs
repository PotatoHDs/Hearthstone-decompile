using System;
using UnityEngine;

// Token: 0x020007BC RID: 1980
public class DeckRule_CountCardsInDeck : DeckRule
{
	// Token: 0x06006D99 RID: 28057 RVA: 0x00235519 File Offset: 0x00233719
	public DeckRule_CountCardsInDeck(int min, int max)
	{
		this.m_ruleType = DeckRule.RuleType.COUNT_CARDS_IN_DECK;
		this.m_minValue = min;
		this.m_maxValue = max;
	}

	// Token: 0x06006D9A RID: 28058 RVA: 0x00235536 File Offset: 0x00233736
	public DeckRule_CountCardsInDeck(DeckRulesetRuleDbfRecord record) : base(DeckRule.RuleType.COUNT_CARDS_IN_DECK, record)
	{
		if (this.m_appliesToSubset == null)
		{
			Debug.LogError("COUNT_CARDS_IN_DECK only supports rules with a defined \"applies to\" subset");
		}
	}

	// Token: 0x06006D9B RID: 28059 RVA: 0x00235554 File Offset: 0x00233754
	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		int cardCountInSet = deck.GetCardCountInSet(this.m_appliesToSubset, this.m_appliesToIsNot);
		int countParam = 0;
		bool isMinimum = false;
		bool val = true;
		if (cardCountInSet < this.m_minValue)
		{
			val = false;
			countParam = this.m_minValue - cardCountInSet;
			isMinimum = true;
		}
		else if (cardCountInSet > this.m_maxValue)
		{
			val = false;
			countParam = cardCountInSet - this.m_maxValue;
		}
		bool result = base.GetResult(val);
		if (!result)
		{
			reason = new RuleInvalidReason(this.m_errorString, countParam, isMinimum);
		}
		return result;
	}
}
