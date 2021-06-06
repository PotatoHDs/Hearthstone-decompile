using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FC7 RID: 4039
	public static class FixedRewardAction
	{
		// Token: 0x0600B018 RID: 45080 RVA: 0x00366CFC File Offset: 0x00364EFC
		public static FixedRewardAction.Type ParseTypeValue(string value)
		{
			FixedRewardAction.Type result;
			EnumUtils.TryGetEnum<FixedRewardAction.Type>(value, out result);
			return result;
		}

		// Token: 0x020027E3 RID: 10211
		public enum Type
		{
			// Token: 0x0400F6C3 RID: 63171
			[Description("tutorial_progress")]
			TUTORIAL_PROGRESS,
			// Token: 0x0400F6C4 RID: 63172
			[Description("wing_progress")]
			WING_PROGRESS,
			// Token: 0x0400F6C5 RID: 63173
			[Description("wing_flags")]
			WING_FLAGS,
			// Token: 0x0400F6C6 RID: 63174
			[Description("hero_level")]
			HERO_LEVEL,
			// Token: 0x0400F6C7 RID: 63175
			[Description("meta_action")]
			META_ACTION,
			// Token: 0x0400F6C8 RID: 63176
			[Description("achieve")]
			ACHIEVE,
			// Token: 0x0400F6C9 RID: 63177
			[Description("account_license_flags")]
			ACCOUNT_LICENSE_FLAGS,
			// Token: 0x0400F6CA RID: 63178
			[Description("unconditional")]
			UNCONDITIONAL,
			// Token: 0x0400F6CB RID: 63179
			[Description("unlock_wild")]
			UNLOCK_WILD,
			// Token: 0x0400F6CC RID: 63180
			[Description("owns_counterpart_card")]
			OWNS_COUNTERPART_CARD,
			// Token: 0x0400F6CD RID: 63181
			[Description("total_hero_level")]
			TOTAL_HERO_LEVEL
		}
	}
}
