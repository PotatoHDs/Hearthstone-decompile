using System.ComponentModel;

namespace Assets
{
	public static class ModularBundleLayoutNode
	{
		public enum DisplayType
		{
			[Description("invalid")]
			INVALID,
			[Description("booster")]
			BOOSTER,
			[Description("text")]
			TEXT,
			[Description("dust")]
			DUST,
			[Description("prefab")]
			PREFAB,
			[Description("hero_skin")]
			HERO_SKIN,
			[Description("card_back")]
			CARD_BACK,
			[Description("arena_ticket")]
			ARENA_TICKET
		}

		public static DisplayType ParseDisplayTypeValue(string value)
		{
			EnumUtils.TryGetEnum<DisplayType>(value, out var outVal);
			return outVal;
		}
	}
}
