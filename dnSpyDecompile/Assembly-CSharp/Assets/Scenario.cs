using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FD3 RID: 4051
	public static class Scenario
	{
		// Token: 0x0600B030 RID: 45104 RVA: 0x00366F3C File Offset: 0x0036513C
		public static Scenario.RuleType ParseRuleTypeValue(string value)
		{
			Scenario.RuleType result;
			EnumUtils.TryGetEnum<Scenario.RuleType>(value, out result);
			return result;
		}

		// Token: 0x020027FB RID: 10235
		public enum RuleType
		{
			// Token: 0x0400F818 RID: 63512
			[Description("none")]
			NONE,
			// Token: 0x0400F819 RID: 63513
			[Description("choose_hero")]
			CHOOSE_HERO,
			// Token: 0x0400F81A RID: 63514
			[Description("choose_deck")]
			CHOOSE_DECK
		}
	}
}
