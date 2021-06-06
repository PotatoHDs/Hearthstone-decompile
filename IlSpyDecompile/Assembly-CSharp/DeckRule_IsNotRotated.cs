using System.Linq;

public class DeckRule_IsNotRotated : DeckRule
{
	public DeckRule_IsNotRotated()
	{
		m_ruleType = RuleType.IS_NOT_ROTATED;
	}

	public DeckRule_IsNotRotated(DeckRulesetRuleDbfRecord record)
		: base(RuleType.IS_NOT_ROTATED, record)
	{
	}

	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		return !GameUtils.IsCardRotated(def);
	}

	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		int num = deck.GetSlots().Sum((CollectionDeckSlot s) => (AppliesTo(s.CardID) && GameUtils.IsCardRotated(s.CardID)) ? s.Count : 0);
		bool result = GetResult(num <= 0);
		if (!result)
		{
			if (RankMgr.Get().IsNewPlayer())
			{
				reason = new RuleInvalidReason(GameStrings.Format("GLUE_COLLECTION_DECK_RULE_INVALID_CARDS_NPR_NEW", num), num);
			}
			else
			{
				reason = new RuleInvalidReason(GameStrings.Format("GLUE_COLLECTION_DECK_RULE_INVALID_CARDS", num), num);
			}
		}
		return result;
	}
}
