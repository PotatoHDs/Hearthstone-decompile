using System.ComponentModel;

namespace Assets
{
	public static class CardBack
	{
		public enum SortCategory
		{
			[Description("none")]
			NONE,
			[Description("base")]
			BASE,
			[Description("fireside")]
			FIRESIDE,
			[Description("expansions")]
			EXPANSIONS,
			[Description("hero_skins")]
			HERO_SKINS,
			[Description("seasonal")]
			SEASONAL,
			[Description("legend")]
			LEGEND,
			[Description("esports")]
			ESPORTS,
			[Description("game_licenses")]
			GAME_LICENSES,
			[Description("promotions")]
			PROMOTIONS,
			[Description("pre_purcahse")]
			PRE_PURCAHSE,
			[Description("blizzcon")]
			BLIZZCON,
			[Description("golden_celebration")]
			GOLDEN_CELEBRATION,
			[Description("events")]
			EVENTS
		}

		public static SortCategory ParseSortCategoryValue(string value)
		{
			EnumUtils.TryGetEnum<SortCategory>(value, out var outVal);
			return outVal;
		}
	}
}
