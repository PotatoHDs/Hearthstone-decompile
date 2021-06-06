using System;
using System.Collections.Generic;

// Token: 0x020007C2 RID: 1986
public class DeckRule_IsInAllSubsets : DeckRule
{
	// Token: 0x06006DB8 RID: 28088 RVA: 0x00235DFC File Offset: 0x00233FFC
	public DeckRule_IsInAllSubsets(DeckRulesetRuleDbfRecord record) : base(DeckRule.RuleType.IS_IN_ALL_SUBSETS, record)
	{
	}

	// Token: 0x06006DB9 RID: 28089 RVA: 0x00235E08 File Offset: 0x00234008
	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		string cardId = def.GetCardId();
		return !base.AppliesTo(cardId) || base.GetResult(DeckRule_IsInAllSubsets.CardBelongsInAllSubsets(cardId, this.m_subsets));
	}

	// Token: 0x06006DBA RID: 28090 RVA: 0x00235E3C File Offset: 0x0023403C
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
				bool val = DeckRule_IsInAllSubsets.CardBelongsInAllSubsets(cardID, this.m_subsets);
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

	// Token: 0x06006DBB RID: 28091 RVA: 0x00235EDC File Offset: 0x002340DC
	private static bool CardBelongsInAllSubsets(string cardId, IList<HashSet<string>> subsets)
	{
		for (int i = 0; i < subsets.Count; i++)
		{
			if (!subsets[i].Contains(cardId))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06006DBC RID: 28092 RVA: 0x00235F0C File Offset: 0x0023410C
	public override bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		string cardId = def.GetCardId();
		if (!base.AppliesTo(cardId))
		{
			return true;
		}
		reason = new RuleInvalidReason(GameStrings.Get("GLUE_COLLECTION_LOCK_CARD_BANNED"), 0, false);
		if (!DeckRule_IsInAllSubsets.CardBelongsInAllSubsets(cardId, this.m_subsets))
		{
			return base.GetResult(false);
		}
		return base.GetResult(true);
	}
}
