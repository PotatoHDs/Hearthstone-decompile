public class DeckRuleViolation
{
	public DeckRule Rule;

	public string DisplayError;

	public DeckRuleViolation(DeckRule rule, string displayError)
	{
		Rule = rule;
		DisplayError = displayError;
	}

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

	public static int SortComparison_Rule(DeckRule lhs, DeckRule rhs)
	{
		DeckRule.RuleType lhs2 = lhs?.Type ?? DeckRule.RuleType.UNKNOWN;
		DeckRule.RuleType rhs2 = rhs?.Type ?? DeckRule.RuleType.UNKNOWN;
		return SortComparison_RuleType(lhs2, rhs2);
	}

	public static int SortComparison_Violation(DeckRuleViolation lhs, DeckRuleViolation rhs)
	{
		DeckRule lhs2 = lhs?.Rule;
		DeckRule rhs2 = rhs?.Rule;
		return SortComparison_Rule(lhs2, rhs2);
	}
}
