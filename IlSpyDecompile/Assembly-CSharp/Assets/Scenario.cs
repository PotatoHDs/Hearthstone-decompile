using System.ComponentModel;

namespace Assets
{
	public static class Scenario
	{
		public enum RuleType
		{
			[Description("none")]
			NONE,
			[Description("choose_hero")]
			CHOOSE_HERO,
			[Description("choose_deck")]
			CHOOSE_DECK
		}

		public static RuleType ParseRuleTypeValue(string value)
		{
			EnumUtils.TryGetEnum<RuleType>(value, out var outVal);
			return outVal;
		}
	}
}
