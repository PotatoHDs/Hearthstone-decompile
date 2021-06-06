public class DeckRule_DefaultType : DeckRule
{
	public DeckRule_DefaultType(string ruleType, DeckRulesetRuleDbfRecord record)
		: base(RuleType.UNKNOWN, record)
	{
		Log.DeckRuleset.Print("DeckRule_DefaultType created for ruleType={0} ruleId={1} deckRulesetId={2}", ruleType, record.ID, record.DeckRulesetId);
	}

	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		return DefaultYes(out reason);
	}
}
