using System.ComponentModel;

namespace Assets
{
	public static class Achievement
	{
		public enum AchievementVisibility
		{
			[Description("visible")]
			VISIBLE,
			[Description("hidden")]
			HIDDEN,
			[Description("hidden_until_completed")]
			HIDDEN_UNTIL_COMPLETED
		}

		public static AchievementVisibility ParseAchievementVisibilityValue(string value)
		{
			EnumUtils.TryGetEnum<AchievementVisibility>(value, out var outVal);
			return outVal;
		}
	}
}
