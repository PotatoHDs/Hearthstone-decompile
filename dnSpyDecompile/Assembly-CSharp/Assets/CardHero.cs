using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FC0 RID: 4032
	public static class CardHero
	{
		// Token: 0x0600B010 RID: 45072 RVA: 0x00366C3C File Offset: 0x00364E3C
		public static CardHero.HeroType ParseHeroTypeValue(string value)
		{
			CardHero.HeroType result;
			EnumUtils.TryGetEnum<CardHero.HeroType>(value, out result);
			return result;
		}

		// Token: 0x020027DB RID: 10203
		public enum HeroType
		{
			// Token: 0x0400F674 RID: 63092
			[Description("Unknown")]
			UNKNOWN,
			// Token: 0x0400F675 RID: 63093
			[Description("Vanilla")]
			VANILLA,
			// Token: 0x0400F676 RID: 63094
			[Description("Honored")]
			HONORED
		}
	}
}
