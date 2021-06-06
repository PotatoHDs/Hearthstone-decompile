using System;
using System.Linq;

// Token: 0x020007C0 RID: 1984
public class DeckRule_IsCardPlayable : DeckRule
{
	// Token: 0x06006DAD RID: 28077 RVA: 0x00235B47 File Offset: 0x00233D47
	public DeckRule_IsCardPlayable()
	{
		this.m_ruleType = DeckRule.RuleType.IS_CARD_PLAYABLE;
	}

	// Token: 0x06006DAE RID: 28078 RVA: 0x00235B57 File Offset: 0x00233D57
	public DeckRule_IsCardPlayable(DeckRulesetRuleDbfRecord record) : base(DeckRule.RuleType.IS_CARD_PLAYABLE, record)
	{
	}

	// Token: 0x06006DAF RID: 28079 RVA: 0x00235B62 File Offset: 0x00233D62
	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		return GameUtils.IsCardGameplayEventActive(def);
	}

	// Token: 0x06006DB0 RID: 28080 RVA: 0x00235B6C File Offset: 0x00233D6C
	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		int num = deck.GetSlots().Sum(delegate(CollectionDeckSlot s)
		{
			if (!base.AppliesTo(s.CardID) || GameUtils.IsCardGameplayEventActive(s.CardID))
			{
				return 0;
			}
			return s.Count;
		});
		bool result = base.GetResult(num <= 0);
		if (!result)
		{
			reason = new RuleInvalidReason(GameStrings.Format("GLUE_COLLECTION_DECK_RULE_UNPLAYABLE_CARDS", new object[]
			{
				num
			}), num, false);
		}
		return result;
	}

	// Token: 0x06006DB1 RID: 28081 RVA: 0x00235BC8 File Offset: 0x00233DC8
	public override bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		string cardId = def.GetCardId();
		if (!base.AppliesTo(cardId))
		{
			return true;
		}
		if (!GameUtils.IsCardGameplayEventActive(cardId))
		{
			reason = new RuleInvalidReason(GameStrings.Get("GLUE_COLLECTION_LOCK_CARD_NOT_PLAYABLE"), 0, false);
			return base.GetResult(false);
		}
		return base.GetResult(true);
	}
}
