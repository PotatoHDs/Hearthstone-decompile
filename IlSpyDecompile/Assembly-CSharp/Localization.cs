using System;
using System.Collections.Generic;
using System.Globalization;
using bgs;
using Hearthstone;
using Hearthstone.DataModels;
using Hearthstone.UI;

public class Localization
{
	public const Locale DEFAULT_LOCALE = Locale.enUS;

	public static readonly string DEFAULT_LOCALE_NAME = Locale.enUS.ToString();

	public static readonly Map<Locale, Locale[]> LOAD_ORDERS = new Map<Locale, Locale[]>
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
			new Locale[1] { Locale.frFR }
		},
		{
			Locale.deDE,
			new Locale[1] { Locale.deDE }
		},
		{
			Locale.koKR,
			new Locale[1] { Locale.koKR }
		},
		{
			Locale.esES,
			new Locale[1] { Locale.esES }
		},
		{
			Locale.esMX,
			new Locale[1] { Locale.esMX }
		},
		{
			Locale.ruRU,
			new Locale[1] { Locale.ruRU }
		},
		{
			Locale.zhTW,
			new Locale[1] { Locale.zhTW }
		},
		{
			Locale.zhCN,
			new Locale[1] { Locale.zhCN }
		},
		{
			Locale.itIT,
			new Locale[1] { Locale.itIT }
		},
		{
			Locale.ptBR,
			new Locale[1] { Locale.ptBR }
		},
		{
			Locale.plPL,
			new Locale[1] { Locale.plPL }
		},
		{
			Locale.jaJP,
			new Locale[1] { Locale.jaJP }
		},
		{
			Locale.thTH,
			new Locale[1] { Locale.thTH }
		}
	};

	public static readonly Map<string, Locale> LOCALE_TO_STRING = new Map<string, Locale>
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

	public const long LARGEST_LOCALE_SIZE = 314572800L;

	private static Localization s_instance = new Localization();

	private Locale m_locale;

	private string m_localeString;

	private int m_localeHashCode;

	private CultureInfo m_cultureInfo;

	private List<Locale> m_foreignLocales;

	private List<string> m_foreignLocaleNames;

	public static readonly PlatformDependentValue<bool> LOCALE_FROM_OPTIONS = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		iOS = true,
		Android = true,
		PC = false,
		Mac = false
	};

	public static void Initialize()
	{
		Locale? locale = null;
		if ((bool)LOCALE_FROM_OPTIONS && EnumUtils.TryGetEnum<Locale>(Options.Get().GetString(Option.LOCALE), out var outVal))
		{
			locale = outVal;
		}
		if (!locale.HasValue)
		{
			string text = null;
			if (HearthstoneApplication.IsPublic())
			{
				text = BattleNet.GetLaunchOption("LOCALE", encrypted: false);
			}
			if (string.IsNullOrEmpty(text))
			{
				text = Vars.Key("Localization.Locale").GetStr(DEFAULT_LOCALE_NAME);
			}
			if (HearthstoneApplication.IsInternal())
			{
				string str = Vars.Key("Localization.OverrideBnetLocale").GetStr("");
				if (!string.IsNullOrEmpty(str))
				{
					text = str;
				}
			}
			locale = ((!EnumUtils.TryGetEnum<Locale>(text, out var outVal2)) ? new Locale?(Locale.enUS) : new Locale?(outVal2));
		}
		SetLocale(locale.Value);
	}

	public static Locale GetLocale()
	{
		return s_instance.m_locale;
	}

	public static bool DoesLocaleNeedExtraReadingTime(Locale locale)
	{
		switch (locale)
		{
		case Locale.enUS:
		case Locale.enGB:
		case Locale.koKR:
		case Locale.zhTW:
		case Locale.zhCN:
		case Locale.jaJP:
			return false;
		default:
			return true;
		}
	}

	public static void SetLocale(Locale locale)
	{
		s_instance.SetPegLocale(locale);
	}

	public static bool IsIMELocale()
	{
		if (GetLocale() != Locale.zhCN && GetLocale() != Locale.zhTW)
		{
			return GetLocale() == Locale.koKR;
		}
		return true;
	}

	public static string GetLocaleName()
	{
		return s_instance.m_localeString;
	}

	public static int GetLocaleHashCode()
	{
		return s_instance.m_localeHashCode;
	}

	public static string GetBnetLocaleName()
	{
		string localeString = s_instance.m_localeString;
		return $"{localeString.Substring(0, 2)}-{localeString.Substring(2, 2)}";
	}

	public static Locale[] GetLoadOrder(Locale locale, bool isCardTexture = false)
	{
		Locale[] array = LOAD_ORDERS[locale];
		if (Network.IsRunning() && BattleNet.GetAccountCountry() == "CHN" && isCardTexture)
		{
			Array.Resize(ref array, array.Length + 1);
			Array.Copy(array, 0, array, 1, array.Length - 1);
			array[0] = Locale.zhCN;
		}
		return array;
	}

	public static Locale[] GetLoadOrder(bool isCardTexture = false)
	{
		return GetLoadOrder(s_instance.m_locale, isCardTexture);
	}

	public static CultureInfo GetCultureInfo()
	{
		return s_instance.m_cultureInfo;
	}

	public static bool IsValidLocaleName(string localeName)
	{
		return Enum.IsDefined(typeof(Locale), localeName);
	}

	public static bool IsValidLocaleName(string localeName, params Locale[] locales)
	{
		if (locales == null || locales.Length == 0)
		{
			return false;
		}
		for (int i = 0; i < locales.Length; i++)
		{
			Locale locale = locales[i];
			string text = locale.ToString();
			if (localeName == text)
			{
				return true;
			}
		}
		return false;
	}

	public static bool IsForeignLocale(Locale locale)
	{
		return locale != Locale.enUS;
	}

	public static bool IsForeignLocaleName(string localeName)
	{
		Locale locale;
		try
		{
			locale = EnumUtils.Parse<Locale>(localeName);
		}
		catch (Exception)
		{
			return false;
		}
		return IsForeignLocale(locale);
	}

	public static List<Locale> GetForeignLocales()
	{
		List<Locale> foreignLocales = s_instance.m_foreignLocales;
		if (foreignLocales != null)
		{
			return foreignLocales;
		}
		foreignLocales = new List<Locale>();
		foreach (Locale value in Enum.GetValues(typeof(Locale)))
		{
			if (value != Locale.UNKNOWN && value != 0)
			{
				foreignLocales.Add(value);
			}
		}
		s_instance.m_foreignLocales = foreignLocales;
		return foreignLocales;
	}

	public static List<string> GetForeignLocaleNames()
	{
		List<string> foreignLocaleNames = s_instance.m_foreignLocaleNames;
		if (foreignLocaleNames != null)
		{
			return foreignLocaleNames;
		}
		foreignLocaleNames = new List<string>();
		string[] names = Enum.GetNames(typeof(Locale));
		foreach (string text in names)
		{
			if (!(text == Locale.UNKNOWN.ToString()) && !(text == DEFAULT_LOCALE_NAME))
			{
				foreignLocaleNames.Add(text);
			}
		}
		s_instance.m_foreignLocaleNames = foreignLocaleNames;
		return foreignLocaleNames;
	}

	public static string ConvertLocaleToDotNet(Locale locale)
	{
		return ConvertLocaleToDotNet(locale.ToString());
	}

	public static string ConvertLocaleToDotNet(string localeName)
	{
		string arg = localeName.Substring(0, 2);
		string arg2 = localeName.Substring(2, 2).ToUpper();
		return $"{arg}-{arg2}";
	}

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
		default:
			return true;
		}
	}

	private void SetPegLocale(Locale locale)
	{
		string pegLocaleName = locale.ToString();
		SetPegLocaleName(pegLocaleName);
	}

	private void SetPegLocaleName(string localeName)
	{
		m_locale = EnumUtils.Parse<Locale>(localeName);
		m_localeString = localeName;
		m_localeHashCode = localeName.GetHashCode();
		string name = ConvertLocaleToDotNet(m_locale);
		m_cultureInfo = CultureInfo.CreateSpecificCulture(name);
		DataContext dataContext = GlobalDataContext.Get();
		IDataModel model = null;
		if (dataContext.GetDataModel(153, out model))
		{
			(model as AccountDataModel).Language = GetLocale();
		}
	}
}
