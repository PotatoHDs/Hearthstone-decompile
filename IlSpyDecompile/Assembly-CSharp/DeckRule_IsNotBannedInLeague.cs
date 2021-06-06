using System.Collections.Generic;

public class DeckRule_IsNotBannedInLeague : DeckRule
{
	public DeckRule_IsNotBannedInLeague()
	{
		m_ruleType = RuleType.IS_NOT_BANNED_IN_LEAGUE;
	}

	public DeckRule_IsNotBannedInLeague(DeckRulesetRuleDbfRecord record)
		: base(RuleType.IS_NOT_BANNED_IN_LEAGUE, record)
	{
	}

	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		return !RankMgr.Get().IsCardBannedInCurrentLeague(def);
	}

	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		List<CollectionDeckSlot> slots = deck.GetSlots();
		int num = 0;
		foreach (CollectionDeckSlot item in slots)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(item.CardID);
			if (AppliesTo(item.CardID) && RankMgr.Get().IsCardBannedInCurrentLeague(entityDef))
			{
				num++;
			}
		}
		bool result = GetResult(num == 0);
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
