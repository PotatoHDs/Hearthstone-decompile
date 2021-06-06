using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FBE RID: 4030
	public static class CardChange
	{
		// Token: 0x0600B00E RID: 45070 RVA: 0x00366C0C File Offset: 0x00364E0C
		public static CardChange.ChangeType ParseChangeTypeValue(string value)
		{
			CardChange.ChangeType result;
			EnumUtils.TryGetEnum<CardChange.ChangeType>(value, out result);
			return result;
		}

		// Token: 0x020027D9 RID: 10201
		public enum ChangeType
		{
			// Token: 0x0400F650 RID: 63056
			[Description("Invalid")]
			INVALID,
			// Token: 0x0400F651 RID: 63057
			[Description("Buff")]
			BUFF,
			// Token: 0x0400F652 RID: 63058
			[Description("Nerf")]
			NERF,
			// Token: 0x0400F653 RID: 63059
			[Description("Addition")]
			ADDITION,
			// Token: 0x0400F654 RID: 63060
			[Description("Hall_Of_Fame")]
			HALL_OF_FAME
		}
	}
}
