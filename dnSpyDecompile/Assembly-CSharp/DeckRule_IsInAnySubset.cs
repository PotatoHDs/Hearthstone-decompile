using System;
using System.Collections.Generic;

// Token: 0x020007C3 RID: 1987
public class DeckRule_IsInAnySubset : DeckRule
{
	// Token: 0x06006DBD RID: 28093 RVA: 0x00235F60 File Offset: 0x00234160
	public DeckRule_IsInAnySubset(DeckRulesetRuleDbfRecord record) : base(DeckRule.RuleType.IS_IN_ANY_SUBSET, record)
	{
	}

	// Token: 0x06006DBE RID: 28094 RVA: 0x00235F6C File Offset: 0x0023416C
	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		string cardId = def.GetCardId();
		return !base.AppliesTo(cardId) || base.GetResult(DeckRule_IsInAnySubset.CardBelongsInAnySubset(cardId, this.m_subsets));
	}

	// Token: 0x06006DBF RID: 28095 RVA: 0x00235FA0 File Offset: 0x002341A0
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
				bool val = DeckRule_IsInAnySubset.CardBelongsInAnySubset(cardID, this.m_subsets);
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

	// Token: 0x06006DC0 RID: 28096 RVA: 0x00236040 File Offset: 0x00234240
	public override bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		string cardId = def.GetCardId();
		if (!base.AppliesTo(cardId))
		{
			return true;
		}
		reason = new RuleInvalidReason(GameStrings.Get("GLUE_COLLECTION_LOCK_CARD_BANNED"), 0, false);
		if (!DeckRule_IsInAnySubset.CardBelongsInAnySubset(cardId, this.m_subsets))
		{
			return base.GetResult(false);
		}
		return base.GetResult(true);
	}

	// Token: 0x06006DC1 RID: 28097 RVA: 0x00236094 File Offset: 0x00234294
	private static bool CardBelongsInAnySubset(string cardId, IList<HashSet<string>> subsets)
	{
		for (int i = 0; i < subsets.Count; i++)
		{
			if (subsets[i].Contains(cardId))
			{
				return true;
			}
		}
		return false;
	}
}
