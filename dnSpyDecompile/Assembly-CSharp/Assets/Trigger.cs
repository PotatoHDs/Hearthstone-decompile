using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FD6 RID: 4054
	public static class Trigger
	{
		// Token: 0x0600B033 RID: 45107 RVA: 0x00366F84 File Offset: 0x00365184
		public static Trigger.Triggertype ParseTriggertypeValue(string value)
		{
			Trigger.Triggertype result;
			EnumUtils.TryGetEnum<Trigger.Triggertype>(value, out result);
			return result;
		}

		// Token: 0x020027FE RID: 10238
		public enum Triggertype
		{
			// Token: 0x0400F833 RID: 63539
			[Description("lua")]
			LUA,
			// Token: 0x0400F834 RID: 63540
			[Description("collect")]
			COLLECT,
			// Token: 0x0400F835 RID: 63541
			[Description("collect_non_unique_cards")]
			COLLECT_NON_UNIQUE_CARDS
		}
	}
}
