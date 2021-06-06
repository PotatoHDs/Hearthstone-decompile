using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FC4 RID: 4036
	public static class DeckTemplate
	{
		// Token: 0x0600B014 RID: 45076 RVA: 0x00366C9C File Offset: 0x00364E9C
		public static DeckTemplate.FormatType ParseFormatTypeValue(string value)
		{
			DeckTemplate.FormatType result;
			EnumUtils.TryGetEnum<DeckTemplate.FormatType>(value, out result);
			return result;
		}

		// Token: 0x020027DF RID: 10207
		public enum FormatType
		{
			// Token: 0x0400F699 RID: 63129
			[Description("ft_unknown")]
			FT_UNKNOWN,
			// Token: 0x0400F69A RID: 63130
			[Description("ft_wild")]
			FT_WILD,
			// Token: 0x0400F69B RID: 63131
			[Description("ft_standard")]
			FT_STANDARD,
			// Token: 0x0400F69C RID: 63132
			[Description("ft_classic")]
			FT_CLASSIC
		}
	}
}
