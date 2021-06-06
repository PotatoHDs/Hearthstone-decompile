using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FCE RID: 4046
	public static class ModularBundleLayoutNode
	{
		// Token: 0x0600B029 RID: 45097 RVA: 0x00366E94 File Offset: 0x00365094
		public static ModularBundleLayoutNode.DisplayType ParseDisplayTypeValue(string value)
		{
			ModularBundleLayoutNode.DisplayType result;
			EnumUtils.TryGetEnum<ModularBundleLayoutNode.DisplayType>(value, out result);
			return result;
		}

		// Token: 0x020027F4 RID: 10228
		public enum DisplayType
		{
			// Token: 0x0400F7D5 RID: 63445
			[Description("invalid")]
			INVALID,
			// Token: 0x0400F7D6 RID: 63446
			[Description("booster")]
			BOOSTER,
			// Token: 0x0400F7D7 RID: 63447
			[Description("text")]
			TEXT,
			// Token: 0x0400F7D8 RID: 63448
			[Description("dust")]
			DUST,
			// Token: 0x0400F7D9 RID: 63449
			[Description("prefab")]
			PREFAB,
			// Token: 0x0400F7DA RID: 63450
			[Description("hero_skin")]
			HERO_SKIN,
			// Token: 0x0400F7DB RID: 63451
			[Description("card_back")]
			CARD_BACK,
			// Token: 0x0400F7DC RID: 63452
			[Description("arena_ticket")]
			ARENA_TICKET
		}
	}
}
