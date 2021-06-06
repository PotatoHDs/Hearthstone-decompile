using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FC8 RID: 4040
	public static class FixedReward
	{
		// Token: 0x0600B019 RID: 45081 RVA: 0x00366D14 File Offset: 0x00364F14
		public static FixedReward.Type ParseTypeValue(string value)
		{
			FixedReward.Type result;
			EnumUtils.TryGetEnum<FixedReward.Type>(value, out result);
			return result;
		}

		// Token: 0x020027E4 RID: 10212
		public enum Type
		{
			// Token: 0x0400F6CF RID: 63183
			[Description("unknown")]
			UNKNOWN,
			// Token: 0x0400F6D0 RID: 63184
			[Description("virtual_card")]
			VIRTUAL_CARD,
			// Token: 0x0400F6D1 RID: 63185
			[Description("real_card")]
			REAL_CARD,
			// Token: 0x0400F6D2 RID: 63186
			[Description("cardback")]
			CARDBACK,
			// Token: 0x0400F6D3 RID: 63187
			[Description("craftable_card")]
			CRAFTABLE_CARD,
			// Token: 0x0400F6D4 RID: 63188
			[Description("meta_action_flags")]
			META_ACTION_FLAGS
		}
	}
}
