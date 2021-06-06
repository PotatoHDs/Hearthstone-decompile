using PegasusShared;

internal static class VisualsFormatTypeExtensions
{
	public static VisualsFormatType GetCurrentVisualsFormatType()
	{
		return ToVisualsFormatType(Options.GetFormatType(), Options.GetInRankedPlayMode());
	}

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

	public static bool IsRanked(this VisualsFormatType visualsFormatType)
	{
		if (visualsFormatType == VisualsFormatType.VFT_UNKNOWN || visualsFormatType == VisualsFormatType.VFT_CASUAL)
		{
			return false;
		}
		return true;
	}
}
