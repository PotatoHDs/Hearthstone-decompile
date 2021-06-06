using System.ComponentModel;

namespace Assets
{
	public static class FixedRewardAction
	{
		public enum Type
		{
			[Description("tutorial_progress")]
			TUTORIAL_PROGRESS,
			[Description("wing_progress")]
			WING_PROGRESS,
			[Description("wing_flags")]
			WING_FLAGS,
			[Description("hero_level")]
			HERO_LEVEL,
			[Description("meta_action")]
			META_ACTION,
			[Description("achieve")]
			ACHIEVE,
			[Description("account_license_flags")]
			ACCOUNT_LICENSE_FLAGS,
			[Description("unconditional")]
			UNCONDITIONAL,
			[Description("unlock_wild")]
			UNLOCK_WILD,
			[Description("owns_counterpart_card")]
			OWNS_COUNTERPART_CARD,
			[Description("total_hero_level")]
			TOTAL_HERO_LEVEL
		}

		public static Type ParseTypeValue(string value)
		{
			EnumUtils.TryGetEnum<Type>(value, out var outVal);
			return outVal;
		}
	}
}
