using System;
using PegasusShared;

// Token: 0x020002B0 RID: 688
internal static class VisualsFormatTypeExtensions
{
	// Token: 0x060023B6 RID: 9142 RVA: 0x000B21A2 File Offset: 0x000B03A2
	public static VisualsFormatType GetCurrentVisualsFormatType()
	{
		return VisualsFormatTypeExtensions.ToVisualsFormatType(Options.GetFormatType(), Options.GetInRankedPlayMode());
	}

	// Token: 0x060023B7 RID: 9143 RVA: 0x000B21B3 File Offset: 0x000B03B3
	public static VisualsFormatType ToVisualsFormatType(FormatType formatType, bool inRankedPlayMode)
	{
		switch (formatType)
		{
		case FormatType.FT_WILD:
			if (!inRankedPlayMode)
			{
				return VisualsFormatType.VFT_CASUAL;
			}
			return VisualsFormatType.VFT_WILD;
		case FormatType.FT_STANDARD:
			return VisualsFormatType.VFT_STANDARD;
		case FormatType.FT_CLASSIC:
			return VisualsFormatType.VFT_CLASSIC;
		default:
			return VisualsFormatType.VFT_UNKNOWN;
		}
	}

	// Token: 0x060023B8 RID: 9144 RVA: 0x000B21D7 File Offset: 0x000B03D7
	public static FormatType ToFormatType(this VisualsFormatType visualsFormatType)
	{
		switch (visualsFormatType)
		{
		case VisualsFormatType.VFT_WILD:
		case VisualsFormatType.VFT_CASUAL:
			return FormatType.FT_WILD;
		case VisualsFormatType.VFT_STANDARD:
			return FormatType.FT_STANDARD;
		case VisualsFormatType.VFT_CLASSIC:
			return FormatType.FT_CLASSIC;
		default:
			return FormatType.FT_UNKNOWN;
		}
	}

	// Token: 0x060023B9 RID: 9145 RVA: 0x000B21FA File Offset: 0x000B03FA
	public static bool IsRanked(this VisualsFormatType visualsFormatType)
	{
		return visualsFormatType != VisualsFormatType.VFT_UNKNOWN && visualsFormatType != VisualsFormatType.VFT_CASUAL;
	}
}
