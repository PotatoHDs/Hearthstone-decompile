using System;

namespace Hearthstone
{
	// Token: 0x02000FD9 RID: 4057
	public static class AssetVariantTags
	{
		// Token: 0x0600B096 RID: 45206 RVA: 0x003689D4 File Offset: 0x00366BD4
		public static AssetVariantTags.Locale GetLocaleVariantTagForLocale(global::Locale locale)
		{
			switch (locale)
			{
			case global::Locale.enUS:
				return AssetVariantTags.Locale.enUS;
			case global::Locale.enGB:
				return AssetVariantTags.Locale.enUS;
			case global::Locale.frFR:
				return AssetVariantTags.Locale.frFR;
			case global::Locale.deDE:
				return AssetVariantTags.Locale.deDE;
			case global::Locale.koKR:
				return AssetVariantTags.Locale.koKR;
			case global::Locale.esES:
				return AssetVariantTags.Locale.esES;
			case global::Locale.esMX:
				return AssetVariantTags.Locale.esMX;
			case global::Locale.ruRU:
				return AssetVariantTags.Locale.ruRU;
			case global::Locale.zhTW:
				return AssetVariantTags.Locale.zhTW;
			case global::Locale.zhCN:
				return AssetVariantTags.Locale.zhCN;
			case global::Locale.itIT:
				return AssetVariantTags.Locale.itIT;
			case global::Locale.ptBR:
				return AssetVariantTags.Locale.ptBR;
			case global::Locale.plPL:
				return AssetVariantTags.Locale.plPL;
			default:
				return AssetVariantTags.Locale.Global;
			case global::Locale.jaJP:
				return AssetVariantTags.Locale.jaJP;
			case global::Locale.thTH:
				return AssetVariantTags.Locale.thTH;
			}
		}

		// Token: 0x0600B097 RID: 45207 RVA: 0x00368A4C File Offset: 0x00366C4C
		public static bool ParseVariantTag(string variant, out Type variantTagType, out object variantTag)
		{
			variantTagType = null;
			variantTag = null;
			string[] array = variant.Split(new char[]
			{
				'.'
			});
			if (array.Length != 2)
			{
				return false;
			}
			string a = array[0].ToLower();
			if (!(a == "locale"))
			{
				if (!(a == "platform"))
				{
					if (!(a == "quality"))
					{
						return false;
					}
					variantTagType = typeof(AssetVariantTags.Quality);
				}
				else
				{
					variantTagType = typeof(AssetVariantTags.Platform);
				}
			}
			else
			{
				variantTagType = typeof(AssetVariantTags.Locale);
			}
			try
			{
				variantTag = Enum.Parse(variantTagType, array[1], true);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0200280C RID: 10252
		public enum Quality
		{
			// Token: 0x0400F852 RID: 63570
			Normal,
			// Token: 0x0400F853 RID: 63571
			Low
		}

		// Token: 0x0200280D RID: 10253
		public enum Platform
		{
			// Token: 0x0400F855 RID: 63573
			Any,
			// Token: 0x0400F856 RID: 63574
			Phone
		}

		// Token: 0x0200280E RID: 10254
		public enum Locale
		{
			// Token: 0x0400F858 RID: 63576
			enUS,
			// Token: 0x0400F859 RID: 63577
			ruRU,
			// Token: 0x0400F85A RID: 63578
			esES,
			// Token: 0x0400F85B RID: 63579
			deDE,
			// Token: 0x0400F85C RID: 63580
			plPL,
			// Token: 0x0400F85D RID: 63581
			ptBR,
			// Token: 0x0400F85E RID: 63582
			esMX,
			// Token: 0x0400F85F RID: 63583
			frFR,
			// Token: 0x0400F860 RID: 63584
			itIT,
			// Token: 0x0400F861 RID: 63585
			koKR,
			// Token: 0x0400F862 RID: 63586
			jaJP,
			// Token: 0x0400F863 RID: 63587
			thTH,
			// Token: 0x0400F864 RID: 63588
			zhTW,
			// Token: 0x0400F865 RID: 63589
			zhCN,
			// Token: 0x0400F866 RID: 63590
			Global
		}
	}
}
