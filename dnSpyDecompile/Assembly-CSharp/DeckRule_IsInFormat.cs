using System;
using System.Collections.Generic;
using PegasusShared;

// Token: 0x020007C5 RID: 1989
public class DeckRule_IsInFormat : DeckRule
{
	// Token: 0x06006DC7 RID: 28103 RVA: 0x0023620F File Offset: 0x0023440F
	public DeckRule_IsInFormat(DeckRulesetRuleDbfRecord record) : base(DeckRule.RuleType.IS_IN_FORMAT, record)
	{
	}

	// Token: 0x06006DC8 RID: 28104 RVA: 0x0023621C File Offset: 0x0023441C
	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		string cardId = def.GetCardId();
		return !base.AppliesTo(cardId) || deck == null || base.GetResult(this.CardBelongsInFormat(cardId));
	}

	// Token: 0x06006DC9 RID: 28105 RVA: 0x00236250 File Offset: 0x00234450
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
				bool val = this.CardBelongsInFormat(cardID);
				if (!base.GetResult(val))
				{
					num += collectionDeckSlot.Count;
					flag = false;
				}
			}
		}
		if (!flag)
		{
			reason = new RuleInvalidReason(GameStrings.Format("GLUE_COLLECTION_DECK_RULE_NOT_IN_FORMAT", new object[]
			{
				num
			}), num, false);
		}
		return flag;
	}

	// Token: 0x06006DCA RID: 28106 RVA: 0x00236300 File Offset: 0x00234500
	public override bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		string cardId = def.GetCardId();
		if (!base.AppliesTo(cardId))
		{
			return true;
		}
		bool flag = this.CardBelongsInFormat(cardId);
		flag = base.GetResult(flag);
		if (!flag)
		{
			reason = new RuleInvalidReason(GameStrings.Get("GLUE_COLLECTION_LOCK_CARD_NOT_IN_FORMAT"), 0, false);
		}
		return flag;
	}

	// Token: 0x06006DCB RID: 28107 RVA: 0x0023634B File Offset: 0x0023454B
	private bool CardBelongsInFormat(string cardId)
	{
		return GameUtils.IsCardValidForFormat((FormatType)this.m_minValue, cardId);
	}
}
