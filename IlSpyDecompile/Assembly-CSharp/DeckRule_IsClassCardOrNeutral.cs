using System.Collections.Generic;
using UnityEngine;

public class DeckRule_IsClassCardOrNeutral : DeckRule
{
	public DeckRule_IsClassCardOrNeutral(DeckRulesetRuleDbfRecord record)
		: base(RuleType.IS_CLASS_CARD_OR_NEUTRAL, record)
	{
		if (m_ruleIsNot)
		{
			Debug.LogError("IS_CLASS_CARD_OR_NEUTRAL rules do not support \"is not\".");
		}
	}

	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		if (!AppliesTo(def.GetCardId()))
		{
			return true;
		}
		if (deck == null)
		{
			return true;
		}
		TAG_CLASS @class = deck.GetClass();
		return GetResult(CardIsClassCardOrNeutral(def, @class));
	}

	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		bool flag = true;
		List<CollectionDeckSlot> slots = deck.GetSlots();
		int num = 0;
		TAG_CLASS @class = deck.GetClass();
		foreach (CollectionDeckSlot item in slots)
		{
			string cardID = item.CardID;
			if (AppliesTo(cardID))
			{
				bool val = CardIsClassCardOrNeutral(DefLoader.Get().GetEntityDef(cardID), @class);
				if (!GetResult(val))
				{
					num += item.Count;
					flag = false;
				}
			}
		}
		if (!flag)
		{
			reason = new RuleInvalidReason(GameStrings.Format("GLUE_COLLECTION_DECK_RULE_INVALID_CLASS_CARD", num), num);
		}
		return flag;
	}

	public override bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		if (!AppliesTo(def.GetCardId()))
		{
			return true;
		}
		TAG_CLASS @class = deck.GetClass();
		bool val = CardIsClassCardOrNeutral(def, @class);
		val = GetResult(val);
		if (!val)
		{
			reason = new RuleInvalidReason(GameStrings.Get("GLUE_COLLECTION_LOCK_CARD_INVALID_CLASS"));
		}
		return val;
	}

	private static bool CardIsClassCardOrNeutral(EntityBase def, TAG_CLASS deckClass)
	{
		IEnumerable<TAG_CLASS> classes = def.GetClasses();
		bool result = false;
		foreach (TAG_CLASS item in classes)
		{
			if (item == TAG_CLASS.NEUTRAL || item == deckClass)
			{
				return true;
			}
		}
		return result;
	}
}
