using System;
using System.Collections.Generic;

// Token: 0x020007BF RID: 1983
public class DeckRule_HasTagValue : DeckRule
{
	// Token: 0x06006DA8 RID: 28072 RVA: 0x00235A08 File Offset: 0x00233C08
	public DeckRule_HasTagValue(GAME_TAG tag, int minValue, int maxValue)
	{
		this.m_ruleType = DeckRule.RuleType.HAS_TAG_VALUE;
		this.m_tag = (int)tag;
		this.m_tagMinValue = minValue;
		this.m_tagMaxValue = maxValue;
	}

	// Token: 0x06006DA9 RID: 28073 RVA: 0x00235A2C File Offset: 0x00233C2C
	public DeckRule_HasTagValue(DeckRulesetRuleDbfRecord record) : base(DeckRule.RuleType.HAS_TAG_VALUE, record)
	{
	}

	// Token: 0x06006DAA RID: 28074 RVA: 0x00235A38 File Offset: 0x00233C38
	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		if (!base.AppliesTo(def.GetCardId()))
		{
			return true;
		}
		int tag = def.GetTag(this.m_tag);
		return base.GetResult(DeckRule_HasTagValue.CardHasTagValue(tag, this.m_tagMaxValue, this.m_tagMinValue));
	}

	// Token: 0x06006DAB RID: 28075 RVA: 0x00235A7C File Offset: 0x00233C7C
	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		bool flag = true;
		List<CollectionDeckSlot> slots = deck.GetSlots();
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in slots)
		{
			string cardID = collectionDeckSlot.CardID;
			if (base.AppliesTo(cardID))
			{
				bool val = DeckRule_HasTagValue.CardHasTagValue(DefLoader.Get().GetEntityDef(cardID).GetTag(this.m_tag), this.m_tagMaxValue, this.m_tagMinValue);
				if (!base.GetResult(val))
				{
					num += collectionDeckSlot.Count;
					flag = false;
				}
			}
		}
		if (!flag)
		{
			reason = new RuleInvalidReason(this.m_errorString, num, false);
		}
		return flag;
	}

	// Token: 0x06006DAC RID: 28076 RVA: 0x00235B38 File Offset: 0x00233D38
	private static bool CardHasTagValue(int tagValue, int max, int min)
	{
		return tagValue >= max && tagValue <= min;
	}
}
