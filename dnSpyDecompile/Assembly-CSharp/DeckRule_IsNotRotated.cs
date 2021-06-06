using System;
using System.Linq;

// Token: 0x020007C7 RID: 1991
public class DeckRule_IsNotRotated : DeckRule
{
	// Token: 0x06006DD0 RID: 28112 RVA: 0x00236468 File Offset: 0x00234668
	public DeckRule_IsNotRotated()
	{
		this.m_ruleType = DeckRule.RuleType.IS_NOT_ROTATED;
	}

	// Token: 0x06006DD1 RID: 28113 RVA: 0x00236477 File Offset: 0x00234677
	public DeckRule_IsNotRotated(DeckRulesetRuleDbfRecord record) : base(DeckRule.RuleType.IS_NOT_ROTATED, record)
	{
	}

	// Token: 0x06006DD2 RID: 28114 RVA: 0x00236481 File Offset: 0x00234681
	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		return !GameUtils.IsCardRotated(def);
	}

	// Token: 0x06006DD3 RID: 28115 RVA: 0x0023648C File Offset: 0x0023468C
	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		int num = deck.GetSlots().Sum(delegate(CollectionDeckSlot s)
		{
			if (!base.AppliesTo(s.CardID) || !GameUtils.IsCardRotated(s.CardID))
			{
				return 0;
			}
			return s.Count;
		});
		bool result = base.GetResult(num <= 0);
		if (!result)
		{
			if (RankMgr.Get().IsNewPlayer())
			{
				reason = new RuleInvalidReason(GameStrings.Format("GLUE_COLLECTION_DECK_RULE_INVALID_CARDS_NPR_NEW", new object[]
				{
					num
				}), num, false);
			}
			else
			{
				reason = new RuleInvalidReason(GameStrings.Format("GLUE_COLLECTION_DECK_RULE_INVALID_CARDS", new object[]
				{
					num
				}), num, false);
			}
		}
		return result;
	}
}
