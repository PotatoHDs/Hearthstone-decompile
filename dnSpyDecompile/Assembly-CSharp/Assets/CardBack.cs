using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FBD RID: 4029
	public static class CardBack
	{
		// Token: 0x0600B00D RID: 45069 RVA: 0x00366BF4 File Offset: 0x00364DF4
		public static CardBack.SortCategory ParseSortCategoryValue(string value)
		{
			CardBack.SortCategory result;
			EnumUtils.TryGetEnum<CardBack.SortCategory>(value, out result);
			return result;
		}

		// Token: 0x020027D8 RID: 10200
		public enum SortCategory
		{
			// Token: 0x0400F641 RID: 63041
			[Description("none")]
			NONE,
			// Token: 0x0400F642 RID: 63042
			[Description("base")]
			BASE,
			// Token: 0x0400F643 RID: 63043
			[Description("fireside")]
			FIRESIDE,
			// Token: 0x0400F644 RID: 63044
			[Description("expansions")]
			EXPANSIONS,
			// Token: 0x0400F645 RID: 63045
			[Description("hero_skins")]
			HERO_SKINS,
			// Token: 0x0400F646 RID: 63046
			[Description("seasonal")]
			SEASONAL,
			// Token: 0x0400F647 RID: 63047
			[Description("legend")]
			LEGEND,
			// Token: 0x0400F648 RID: 63048
			[Description("esports")]
			ESPORTS,
			// Token: 0x0400F649 RID: 63049
			[Description("game_licenses")]
			GAME_LICENSES,
			// Token: 0x0400F64A RID: 63050
			[Description("promotions")]
			PROMOTIONS,
			// Token: 0x0400F64B RID: 63051
			[Description("pre_purcahse")]
			PRE_PURCAHSE,
			// Token: 0x0400F64C RID: 63052
			[Description("blizzcon")]
			BLIZZCON,
			// Token: 0x0400F64D RID: 63053
			[Description("golden_celebration")]
			GOLDEN_CELEBRATION,
			// Token: 0x0400F64E RID: 63054
			[Description("events")]
			EVENTS
		}
	}
}
