using System;

// Token: 0x020007BB RID: 1979
public class DeckRule_DefaultType : DeckRule
{
	// Token: 0x06006D97 RID: 28055 RVA: 0x002354D0 File Offset: 0x002336D0
	public DeckRule_DefaultType(string ruleType, DeckRulesetRuleDbfRecord record) : base(DeckRule.RuleType.UNKNOWN, record)
	{
		Log.DeckRuleset.Print("DeckRule_DefaultType created for ruleType={0} ruleId={1} deckRulesetId={2}", new object[]
		{
			ruleType,
			record.ID,
			record.DeckRulesetId
		});
	}

	// Token: 0x06006D98 RID: 28056 RVA: 0x00235510 File Offset: 0x00233710
	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		return base.DefaultYes(out reason);
	}
}
