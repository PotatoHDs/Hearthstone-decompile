using System;

// Token: 0x020007CA RID: 1994
public class DeckRuleViolation
{
	// Token: 0x06006DF5 RID: 28149 RVA: 0x00237240 File Offset: 0x00235440
	public DeckRuleViolation(DeckRule rule, string displayError)
	{
		this.Rule = rule;
		this.DisplayError = displayError;
	}

	// Token: 0x06006DF6 RID: 28150 RVA: 0x00237256 File Offset: 0x00235456
	private static int SortComparison_RuleType(DeckRule.RuleType lhs, DeckRule.RuleType rhs)
	{
		if (lhs == rhs)
		{
			return 0;
		}
		if (lhs == DeckRule.RuleType.COUNT_COPIES_OF_EACH_CARD)
		{
			return -1;
		}
		if (rhs == DeckRule.RuleType.COUNT_COPIES_OF_EACH_CARD)
		{
			return 1;
		}
		return lhs.CompareTo(rhs);
	}

	// Token: 0x06006DF7 RID: 28151 RVA: 0x00237280 File Offset: 0x00235480
	public static int SortComparison_Rule(DeckRule lhs, DeckRule rhs)
	{
		DeckRule.RuleType lhs2 = (lhs == null) ? DeckRule.RuleType.UNKNOWN : lhs.Type;
		DeckRule.RuleType rhs2 = (rhs == null) ? DeckRule.RuleType.UNKNOWN : rhs.Type;
		return DeckRuleViolation.SortComparison_RuleType(lhs2, rhs2);
	}

	// Token: 0x06006DF8 RID: 28152 RVA: 0x002372B0 File Offset: 0x002354B0
	public static int SortComparison_Violation(DeckRuleViolation lhs, DeckRuleViolation rhs)
	{
		DeckRule lhs2 = (lhs == null) ? null : lhs.Rule;
		DeckRule rhs2 = (rhs == null) ? null : rhs.Rule;
		return DeckRuleViolation.SortComparison_Rule(lhs2, rhs2);
	}

	// Token: 0x0400580D RID: 22541
	public DeckRule Rule;

	// Token: 0x0400580E RID: 22542
	public string DisplayError;
}
