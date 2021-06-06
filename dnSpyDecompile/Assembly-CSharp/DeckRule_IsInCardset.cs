using System;
using System.Collections.Generic;

// Token: 0x020007C4 RID: 1988
public class DeckRule_IsInCardset : DeckRule
{
	// Token: 0x06006DC2 RID: 28098 RVA: 0x002360C4 File Offset: 0x002342C4
	public DeckRule_IsInCardset(DeckRulesetRuleDbfRecord record) : base(DeckRule.RuleType.IS_IN_CARDSET, record)
	{
	}

	// Token: 0x06006DC3 RID: 28099 RVA: 0x002360D0 File Offset: 0x002342D0
	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		string cardId = def.GetCardId();
		return !base.AppliesTo(cardId) || deck == null || base.GetResult(this.CardBelongsInSet(cardId));
	}

	// Token: 0x06006DC4 RID: 28100 RVA: 0x00236104 File Offset: 0x00234304
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
				bool val = this.CardBelongsInSet(cardID);
				if (!base.GetResult(val))
				{
					num += collectionDeckSlot.Count;
					flag = false;
				}
			}
		}
		if (!flag)
		{
			reason = new RuleInvalidReason(GameStrings.Format("GLUE_COLLECTION_DECK_RULE_NOT_IN_CARDSET", new object[]
			{
				num
			}), num, false);
		}
		return flag;
	}

	// Token: 0x06006DC5 RID: 28101 RVA: 0x002361B4 File Offset: 0x002343B4
	public override bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		string cardId = def.GetCardId();
		if (!base.AppliesTo(cardId))
		{
			return true;
		}
		bool flag = this.CardBelongsInSet(cardId);
		flag = base.GetResult(flag);
		if (!flag)
		{
			reason = new RuleInvalidReason(GameStrings.Get("GLUE_COLLECTION_LOCK_CARD_NOT_IN_CARDSET"), 0, false);
		}
		return flag;
	}

	// Token: 0x06006DC6 RID: 28102 RVA: 0x002361FF File Offset: 0x002343FF
	private bool CardBelongsInSet(string cardId)
	{
		return GameUtils.GetCardSetFromCardID(cardId) == (TAG_CARD_SET)this.m_minValue;
	}
}
