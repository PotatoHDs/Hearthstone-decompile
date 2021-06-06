using System;
using System.Collections.Generic;
using System.Globalization;
using bgs;
using Hearthstone;
using Hearthstone.DataModels;
using Hearthstone.UI;

// Token: 0x020008E5 RID: 2277
public class Localization
{
	// Token: 0x06007E63 RID: 32355 RVA: 0x0028E7A4 File Offset: 0x0028C9A4
	public static void Initialize()
	{
		Locale? locale = null;
		Locale value;
		if (Localization.LOCALE_FROM_OPTIONS && global::EnumUtils.TryGetEnum<Locale>(Options.Get().GetString(Option.LOCALE), out value))
		{
			locale = new Locale?(value);
		}
		if (locale == null)
		{
			string text = null;
			if (HearthstoneApplication.IsPublic())
			{
				text = BattleNet.GetLaunchOption("LOCALE", false);
			}
			if (string.IsNullOrEmpty(text))
			{
				text = Vars.Key("Localization.Locale").GetStr(Localization.DEFAULT_LOCALE_NAME);
			}
			if (HearthstoneApplication.IsInternal())
			{
				string str = Vars.Key("Localization.OverrideBnetLocale").GetStr("");
				if (!string.IsNullOrEmpty(str))
				{
					text = str;
				}
			}
			Locale value2;
			if (global::EnumUtils.TryGetEnum<Locale>(text, out value2))
			{
				locale = new Locale?(value2);
			}
			else
			{
				locale = new Locale?(Locale.enUS);
			}
		}
		Localization.SetLocale(locale.Value);
	}

	// Token: 0x06007E64 RID: 32356 RVA: 0x0028E86E File Offset: 0x0028CA6E
	public static Locale GetLocale()
	{
		return Localization.s_instance.m_locale;
	}

	// Token: 0x06007E65 RID: 32357 RVA: 0x0028E87A File Offset: 0x0028CA7A
	public static bool DoesLocaleNeedExtraReadingTime(Locale locale)
	{
		if (locale <= Locale.koKR)
		{
			if (locale > Locale.enGB && locale != Locale.koKR)
			{
				return true;
			}
		}
		else if (locale - Locale.zhTW > 1 && locale != Locale.jaJP)
		{
			return true;
		}
		return false;
	}

	// Token: 0x06007E66 RID: 32358 RVA: 0x0028E898 File Offset: 0x0028CA98
	public static void SetLocale(Locale locale)
	{
		Localization.s_instance.SetPegLocale(locale);
	}

	// Token: 0x06007E67 RID: 32359 RVA: 0x0028E8A5 File Offset: 0x0028CAA5
	public static bool IsIMELocale()
	{
		return Localization.GetLocale() == Locale.zhCN || Localization.GetLocale() == Locale.zhTW || Localization.GetLocale() == Locale.koKR;
	}

	// Token: 0x06007E68 RID: 32360 RVA: 0x0028E8C2 File Offset: 0x0028CAC2
	public static string GetLocaleName()
	{
		return Localization.s_instance.m_localeString;
	}

	// Token: 0x06007E69 RID: 32361 RVA: 0x0028E8CE File Offset: 0x0028CACE
	public static int GetLocaleHashCode()
	{
		return Localization.s_instance.m_localeHashCode;
	}

	// Token: 0x06007E6A RID: 32362 RVA: 0x0028E8DC File Offset: 0x0028CADC
	public static string GetBnetLocaleName()
	{
		string localeString = Localization.s_instance.m_localeString;
		return string.Format("{0}-{1}", localeString.Substring(0, 2), localeString.Substring(2, 2));
	}

	// Token: 0x06007E6B RID: 32363 RVA: 0x0028E910 File Offset: 0x0028CB10
	public static Locale[] GetLoadOrder(Locale locale, bool isCardTexture = false)
	{
		Locale[] array = Localization.LOAD_ORDERS[locale];
		if (Network.IsRunning() && BattleNet.GetAccountCountry() == "CHN" && isCardTexture)
		{
			Array.Resize<Locale>(ref array, array.Length + 1);
			Array.Copy(array, 0, array, 1, array.Length - 1);
			array[0] = Locale.zhCN;
		}
		return array;
	}

	// Token: 0x06007E6C RID: 32364 RVA: 0x0028E966 File Offset: 0x0028CB66
	public static Locale[] GetLoadOrder(bool isCardTexture = false)
	{
		return Localization.GetLoadOrder(Localization.s_instance.m_locale, isCardTexture);
	}

	// Token: 0x06007E6D RID: 32365 RVA: 0x0028E978 File Offset: 0x0028CB78
	public static CultureInfo GetCultureInfo()
	{
		return Localization.s_instance.m_cultureInfo;
	}

	// Token: 0x06007E6E RID: 32366 RVA: 0x0028E984 File Offset: 0x0028CB84
	public static bool IsValidLocaleName(string localeName)
	{
		return Enum.IsDefined(typeof(Locale), localeName);
	}

	// Token: 0x06007E6F RID: 32367 RVA: 0x0028E998 File Offset: 0x0028CB98
	public static bool IsValidLocaleName(string localeName, params Locale[] locales)
	{
		if (locales == null || locales.Length == 0)
		{
			return false;
		}
		foreach (Locale locale in locales)
		{
			string b = locale.ToString();
			if (localeName == b)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06007E70 RID: 32368 RVA: 0x0028E9DA File Offset: 0x0028CBDA
	public static bool IsForeignLocale(Locale locale)
	{
		return locale > Locale.enUS;
	}

	// Token: 0x06007E71 RID: 32369 RVA: 0x0028E9E0 File Offset: 0x0028CBE0
	public static bool IsForeignLocaleName(string localeName)
	{
		Locale locale;
		try
		{
			locale = global::EnumUtils.Parse<Locale>(localeName);
		}
		catch (Exception)
		{
			return false;
		}
		return Localization.IsForeignLocale(locale);
	}

	// Token: 0x06007E72 RID: 32370 RVA: 0x0028EA14 File Offset: 0x0028CC14
	public static List<Locale> GetForeignLocales()
	{
		List<Locale> list = Localization.s_instance.m_foreignLocales;
		if (list != null)
		{
			return list;
		}
		list = new List<Locale>();
		foreach (object obj in Enum.GetValues(typeof(Locale)))
		{
			Locale locale = (Locale)obj;
			if (locale != Locale.UNKNOWN && locale != Locale.enUS)
			{
				list.Add(locale);
			}
		}
		Localization.s_instance.m_foreignLocales = list;
		return list;
	}

	// Token: 0x06007E73 RID: 32371 RVA: 0x0028EAA0 File Offset: 0x0028CCA0
	public static List<string> GetForeignLocaleNames()
	{
		List<string> list = Localization.s_instance.m_foreignLocaleNames;
		if (list != null)
		{
			return list;
		}
		list = new List<string>();
		foreach (string text in Enum.GetNames(typeof(Locale)))
		{
			if (!(text == Locale.UNKNOWN.ToString()) && !(text == Localization.DEFAULT_LOCALE_NAME))
			{
				list.Add(text);
			}
		}
		Localization.s_instance.m_foreignLocaleNames = list;
		return list;
	}

	// Token: 0x06007E74 RID: 32372 RVA: 0x0028EB1D File Offset: 0x0028CD1D
	public static string ConvertLocaleToDotNet(Locale locale)
	{
		return Localization.ConvertLocaleToDotNet(locale.ToString());
	}

	// Token: 0x06007E75 RID: 32373 RVA: 0x0028EB34 File Offset: 0x0028CD34
	public static string ConvertLocaleToDotNet(string localeName)
	{
		string arg = localeName.Substring(0, 2);
		string arg2 = localeName.Substring(2, 2).ToUpper();
		return string.Format("{0}-{1}", arg, arg2);
	}

	// Token: 0x06007E76 RID: 32374 RVA: 0x0028EB64 File Offset: 0x0028CD64
	public static bool DoesLocaleUseDecimalPoint(Locale locale)
	{
		switch (locale)
		{
		case Locale.enUS:
		case Locale.enGB:
		case Locale.koKR:
		case Locale.esMX:
		case Locale.zhTW:
		case Locale.zhCN:
		case Locale.jaJP:
		case Locale.thTH:
			return true;
		case Locale.frFR:
		case Locale.deDE:
		case Locale.esES:
		case Locale.ruRU:
		case Locale.itIT:
		case Locale.ptBR:
		case Locale.plPL:
			return false;
		}
		return true;
	}

	// Token: 0x06007E77 RID: 32375 RVA: 0x0028EBC0 File Offset: 0x0028CDC0
	private void SetPegLocale(Locale locale)
	{
		string pegLocaleName = locale.ToString();
		this.SetPegLocaleName(pegLocaleName);
	}

	// Token: 0x06007E78 RID: 32376 RVA: 0x0028EBE4 File Offset: 0x0028CDE4
	private void SetPegLocaleName(string localeName)
	{
		this.m_locale = global::EnumUtils.Parse<Locale>(localeName);
		this.m_localeString = localeName;
		this.m_localeHashCode = localeName.GetHashCode();
		string name = Localization.ConvertLocaleToDotNet(this.m_locale);
		this.m_cultureInfo = CultureInfo.CreateSpecificCulture(name);
		DataContext dataContext = GlobalDataContext.Get();
		IDataModel dataModel = null;
		if (dataContext.GetDataModel(153, out dataModel))
		{
			(dataModel as AccountDataModel).Language = Localization.GetLocale();
		}
	}

	// Token: 0x0400661E RID: 26142
	public const Locale DEFAULT_LOCALE = Locale.enUS;

	// Token: 0x0400661F RID: 26143
	public static readonly string DEFAULT_LOCALE_NAME = Locale.enUS.ToString();

	// Token: 0x04006620 RID: 26144
	public static readonly global::Map<Locale, Locale[]> LOAD_ORDERS = new global::Map<Locale, Locale[]>
	{
		{
			Locale.enUS,
			new Locale[1]
		},
		{
			Locale.enGB,
			new Locale[1]
		},
		{
			Locale.frFR,
			new Locale[]
			{
				Locale.frFR
			}
		},
		{
			Locale.deDE,
			new Locale[]
			{
				Locale.deDE
			}
		},
		{
			Locale.koKR,
			new Locale[]
			{
				Locale.koKR
			}
		},
		{
			Locale.esES,
			new Locale[]
			{
				Locale.esES
			}
		},
		{
			Locale.esMX,
			new Locale[]
			{
				Locale.esMX
			}
		},
		{
			Locale.ruRU,
			new Locale[]
			{
				Locale.ruRU
			}
		},
		{
			Locale.zhTW,
			new Locale[]
			{
				Locale.zhTW
			}
		},
		{
			Locale.zhCN,
			new Locale[]
			{
				Locale.zhCN
			}
		},
		{
			Locale.itIT,
			new Locale[]
			{
				Locale.itIT
			}
		},
		{
			Locale.ptBR,
			new Locale[]
			{
				Locale.ptBR
			}
		},
		{
			Locale.plPL,
			new Locale[]
			{
				Locale.plPL
			}
		},
		{
			Locale.jaJP,
			new Locale[]
			{
				Locale.jaJP
			}
		},
		{
			Locale.thTH,
			new Locale[]
			{
				Locale.thTH
			}
		}
	};

	// Token: 0x04006621 RID: 26145
	public static readonly global::Map<string, Locale> LOCALE_TO_STRING = new global::Map<string, Locale>
	{
		{
			"enUS",
			Locale.enUS
		},
		{
			"enGB",
			Locale.enGB
		},
		{
			"frFR",
			Locale.frFR
		},
		{
			"deDE",
			Locale.deDE
		},
		{
			"koKR",
			Locale.koKR
		},
		{
			"esES",
			Locale.esES
		},
		{
			"esMX",
			Locale.esMX
		},
		{
			"ruRU",
			Locale.ruRU
		},
		{
			"zhTW",
			Locale.zhTW
		},
		{
			"zhCN",
			Locale.zhCN
		},
		{
			"itIT",
			Locale.itIT
		},
		{
			"ptBR",
			Locale.ptBR
		},
		{
			"plPL",
			Locale.plPL
		},
		{
			"jaJP",
			Locale.jaJP
		},
		{
			"thTH",
			Locale.thTH
		}
	};

	// Token: 0x04006622 RID: 26146
	public const long LARGEST_LOCALE_SIZE = 314572800L;

	// Token: 0x04006623 RID: 26147
	private static Localization s_instance = new Localization();

	// Token: 0x04006624 RID: 26148
	private Locale m_locale;

	// Token: 0x04006625 RID: 26149
	private string m_localeString;

	// Token: 0x04006626 RID: 26150
	private int m_localeHashCode;

	// Token: 0x04006627 RID: 26151
	private CultureInfo m_cultureInfo;

	// Token: 0x04006628 RID: 26152
	private List<Locale> m_foreignLocales;

	// Token: 0x04006629 RID: 26153
	private List<string> m_foreignLocaleNames;

	// Token: 0x0400662A RID: 26154
	public static readonly PlatformDependentValue<bool> LOCALE_FROM_OPTIONS = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		iOS = true,
		Android = true,
		PC = false,
		Mac = false
	};
}
