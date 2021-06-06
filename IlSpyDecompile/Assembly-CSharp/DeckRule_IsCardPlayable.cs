using System.Linq;

public class DeckRule_IsCardPlayable : DeckRule
{
	public DeckRule_IsCardPlayable()
	{
		m_ruleType = RuleType.IS_CARD_PLAYABLE;
	}

	public DeckRule_IsCardPlayable(DeckRulesetRuleDbfRecord record)
		: base(RuleType.IS_CARD_PLAYABLE, record)
	{
	}

	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		return GameUtils.IsCardGameplayEventActive(def);
	}

	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		int num = deck.GetSlots().Sum((CollectionDeckSlot s) => (AppliesTo(s.CardID) && !GameUtils.IsCardGameplayEventActive(s.CardID)) ? s.Count : 0);
		bool result = GetResult(num <= 0);
		if (!result)
		{
			reason = new RuleInvalidReason(GameStrings.Format("GLUE_COLLECTION_DECK_RULE_UNPLAYABLE_CARDS", num), num);
		}
		return result;
	}

	public override bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		string cardId = def.GetCardId();
		if (!AppliesTo(cardId))
		{
			return true;
		}
		if (!GameUtils.IsCardGameplayEventActive(cardId))
		{
			reason = new RuleInvalidReason(GameStrings.Get("GLUE_COLLECTION_LOCK_CARD_NOT_PLAYABLE"));
			return GetResult(val: false);
		}
		return GetResult(val: true);
	}
}
