using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FC5 RID: 4037
	public static class DraftContent
	{
		// Token: 0x0600B015 RID: 45077 RVA: 0x00366CB4 File Offset: 0x00364EB4
		public static DraftContent.SlotType ParseSlotTypeValue(string value)
		{
			DraftContent.SlotType result;
			EnumUtils.TryGetEnum<DraftContent.SlotType>(value, out result);
			return result;
		}

		// Token: 0x020027E0 RID: 10208
		public enum SlotType
		{
			// Token: 0x0400F69E RID: 63134
			[Description("none")]
			NONE,
			// Token: 0x0400F69F RID: 63135
			[Description("card")]
			CARD,
			// Token: 0x0400F6A0 RID: 63136
			[Description("hero")]
			HERO,
			// Token: 0x0400F6A1 RID: 63137
			[Description("hero_power")]
			HERO_POWER
		}
	}
}
