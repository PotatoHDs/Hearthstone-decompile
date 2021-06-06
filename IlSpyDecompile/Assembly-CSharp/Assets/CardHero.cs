using System.ComponentModel;

namespace Assets
{
	public static class CardHero
	{
		public enum HeroType
		{
			[Description("Unknown")]
			UNKNOWN,
			[Description("Vanilla")]
			VANILLA,
			[Description("Honored")]
			HONORED
		}

		public static HeroType ParseHeroTypeValue(string value)
		{
			EnumUtils.TryGetEnum<HeroType>(value, out var outVal);
			return outVal;
		}
	}
}
