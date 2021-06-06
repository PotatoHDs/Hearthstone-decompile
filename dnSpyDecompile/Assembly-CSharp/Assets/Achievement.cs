using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FBB RID: 4027
	public static class Achievement
	{
		// Token: 0x0600B00A RID: 45066 RVA: 0x00366BAC File Offset: 0x00364DAC
		public static Achievement.AchievementVisibility ParseAchievementVisibilityValue(string value)
		{
			Achievement.AchievementVisibility result;
			EnumUtils.TryGetEnum<Achievement.AchievementVisibility>(value, out result);
			return result;
		}

		// Token: 0x020027D5 RID: 10197
		public enum AchievementVisibility
		{
			// Token: 0x0400F62E RID: 63022
			[Description("visible")]
			VISIBLE,
			// Token: 0x0400F62F RID: 63023
			[Description("hidden")]
			HIDDEN,
			// Token: 0x0400F630 RID: 63024
			[Description("hidden_until_completed")]
			HIDDEN_UNTIL_COMPLETED
		}
	}
}
