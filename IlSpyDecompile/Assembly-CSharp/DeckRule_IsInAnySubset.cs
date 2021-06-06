using System.Collections.Generic;

public class DeckRule_IsInAnySubset : DeckRule
{
	public DeckRule_IsInAnySubset(DeckRulesetRuleDbfRecord record)
		: base(RuleType.IS_IN_ANY_SUBSET, record)
	{
	}

	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		string cardId = def.GetCardId();
		if (!AppliesTo(cardId))
		{
			return true;
		}
		return GetResult(CardBelongsInAnySubset(cardId, m_subsets));
	}

	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		bool flag = true;
		List<CollectionDeckSlot> slots = deck.GetSlots();
		int num = 0;
		foreach (CollectionDeckSlot item in slots)
		{
			string cardID = item.CardID;
			if (AppliesTo(cardID))
			{
				bool val = CardBelongsInAnySubset(cardID, m_subsets);
				if (!GetResult(val))
				{
					num += item.Count;
					flag = false;
				}
			}
		}
		if (!flag)
		{
			reason = new RuleInvalidReason(m_errorString, num);
		}
		return flag;
	}

	public override bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		string cardId = def.GetCardId();
		if (!AppliesTo(cardId))
		{
			return true;
		}
		reason = new RuleInvalidReason(GameStrings.Get("GLUE_COLLECTION_LOCK_CARD_BANNED"));
		if (!CardBelongsInAnySubset(cardId, m_subsets))
		{
			return GetResult(val: false);
		}
		return GetResult(val: true);
	}

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
