using System;
using System.Collections.Generic;

// Token: 0x020007C6 RID: 1990
public class DeckRule_IsNotBannedInLeague : DeckRule
{
	// Token: 0x06006DCC RID: 28108 RVA: 0x00236359 File Offset: 0x00234559
	public DeckRule_IsNotBannedInLeague()
	{
		this.m_ruleType = DeckRule.RuleType.IS_NOT_BANNED_IN_LEAGUE;
	}

	// Token: 0x06006DCD RID: 28109 RVA: 0x00236369 File Offset: 0x00234569
	public DeckRule_IsNotBannedInLeague(DeckRulesetRuleDbfRecord record) : base(DeckRule.RuleType.IS_NOT_BANNED_IN_LEAGUE, record)
	{
	}

	// Token: 0x06006DCE RID: 28110 RVA: 0x00236374 File Offset: 0x00234574
	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		return !RankMgr.Get().IsCardBannedInCurrentLeague(def);
	}

	// Token: 0x06006DCF RID: 28111 RVA: 0x00236384 File Offset: 0x00234584
	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		List<CollectionDeckSlot> slots = deck.GetSlots();
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in slots)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(collectionDeckSlot.CardID);
			if (base.AppliesTo(collectionDeckSlot.CardID) && RankMgr.Get().IsCardBannedInCurrentLeague(entityDef))
			{
				num++;
			}
		}
		bool result = base.GetResult(num == 0);
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
