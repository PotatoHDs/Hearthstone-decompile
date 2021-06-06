using System.Collections.Generic;
using PegasusShared;

public class DeckRule_IsInFormat : DeckRule
{
	public DeckRule_IsInFormat(DeckRulesetRuleDbfRecord record)
		: base(RuleType.IS_IN_FORMAT, record)
	{
	}

	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		string cardId = def.GetCardId();
		if (!AppliesTo(cardId))
		{
			return true;
		}
		if (deck == null)
		{
			return true;
		}
		return GetResult(CardBelongsInFormat(cardId));
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
				bool val = CardBelongsInFormat(cardID);
				if (!GetResult(val))
				{
					num += item.Count;
					flag = false;
				}
			}
		}
		if (!flag)
		{
			reason = new RuleInvalidReason(GameStrings.Format("GLUE_COLLECTION_DECK_RULE_NOT_IN_FORMAT", num), num);
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
		bool val = CardBelongsInFormat(cardId);
		val = GetResult(val);
		if (!val)
		{
			reason = new RuleInvalidReason(GameStrings.Get("GLUE_COLLECTION_LOCK_CARD_NOT_IN_FORMAT"));
		}
		return val;
	}

	private bool CardBelongsInFormat(string cardId)
	{
		return GameUtils.IsCardValidForFormat((FormatType)m_minValue, cardId);
	}
}
