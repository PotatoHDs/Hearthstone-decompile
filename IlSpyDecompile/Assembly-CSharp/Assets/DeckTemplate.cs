using System.ComponentModel;

namespace Assets
{
	public static class DeckTemplate
	{
		public enum FormatType
		{
			[Description("ft_unknown")]
			FT_UNKNOWN,
			[Description("ft_wild")]
			FT_WILD,
			[Description("ft_standard")]
			FT_STANDARD,
			[Description("ft_classic")]
			FT_CLASSIC
		}

		public static FormatType ParseFormatTypeValue(string value)
		{
			EnumUtils.TryGetEnum<FormatType>(value, out var outVal);
			return outVal;
		}
	}
}
