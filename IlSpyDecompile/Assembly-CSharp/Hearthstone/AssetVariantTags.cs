using System;

namespace Hearthstone
{
	public static class AssetVariantTags
	{
		public enum Quality
		{
			Normal,
			Low
		}

		public enum Platform
		{
			Any,
			Phone
		}

		public enum Locale
		{
			enUS,
			ruRU,
			esES,
			deDE,
			plPL,
			ptBR,
			esMX,
			frFR,
			itIT,
			koKR,
			jaJP,
			thTH,
			zhTW,
			zhCN,
			Global
		}

		public static Locale GetLocaleVariantTagForLocale(global::Locale locale)
		{
			return locale switch
			{
				global::Locale.enUS => Locale.enUS, 
				global::Locale.enGB => Locale.enUS, 
				global::Locale.deDE => Locale.deDE, 
				global::Locale.frFR => Locale.frFR, 
				global::Locale.koKR => Locale.koKR, 
				global::Locale.esES => Locale.esES, 
				global::Locale.esMX => Locale.esMX, 
				global::Locale.ruRU => Locale.ruRU, 
				global::Locale.zhTW => Locale.zhTW, 
				global::Locale.zhCN => Locale.zhCN, 
				global::Locale.itIT => Locale.itIT, 
				global::Locale.ptBR => Locale.ptBR, 
				global::Locale.plPL => Locale.plPL, 
				global::Locale.jaJP => Locale.jaJP, 
				global::Locale.thTH => Locale.thTH, 
				_ => Locale.Global, 
			};
		}

		public static bool ParseVariantTag(string variant, out Type variantTagType, out object variantTag)
		{
			variantTagType = null;
			variantTag = null;
			string[] array = variant.Split('.');
			if (array.Length != 2)
			{
				return false;
			}
			switch (array[0].ToLower())
			{
			case "locale":
				variantTagType = typeof(Locale);
				break;
			case "platform":
				variantTagType = typeof(Platform);
				break;
			case "quality":
				variantTagType = typeof(Quality);
				break;
			default:
				return false;
			}
			try
			{
				variantTag = Enum.Parse(variantTagType, array[1], ignoreCase: true);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}
	}
}
