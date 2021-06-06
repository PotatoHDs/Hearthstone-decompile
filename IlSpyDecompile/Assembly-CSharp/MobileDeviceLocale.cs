using System;
using System.Collections.Generic;
using bgs;
using UnityEngine;

public class MobileDeviceLocale
{
	public struct ConnectionData
	{
		public string address;

		public int port;

		public string version;

		public string name;

		public uint tutorialPort;

		public string gameServerAddress;
	}

	private static Map<string, Locale> s_languageCodeToLocale = new Map<string, Locale>
	{
		{
			"fr",
			Locale.frFR
		},
		{
			"de",
			Locale.deDE
		},
		{
			"ko",
			Locale.koKR
		},
		{
			"ru",
			Locale.ruRU
		},
		{
			"it",
			Locale.itIT
		},
		{
			"pt",
			Locale.ptBR
		},
		{
			"pl",
			Locale.plPL
		},
		{
			"ja",
			Locale.jaJP
		},
		{
			"th",
			Locale.thTH
		},
		{
			"en-AU",
			Locale.enUS
		},
		{
			"en-GB",
			Locale.enGB
		},
		{
			"fr-CA",
			Locale.frFR
		},
		{
			"es-MX",
			Locale.esMX
		},
		{
			"zh-Hans",
			Locale.zhCN
		},
		{
			"zh-Hant",
			Locale.zhTW
		},
		{
			"pt-PT",
			Locale.ptBR
		}
	};

	private static Map<string, int> s_countryCodeToRegionId = new Map<string, int>
	{
		{ "AD", 2 },
		{ "AE", 2 },
		{ "AG", 1 },
		{ "AL", 2 },
		{ "AM", 2 },
		{ "AO", 2 },
		{ "AR", 1 },
		{ "AT", 2 },
		{ "AU", 1 },
		{ "AZ", 2 },
		{ "BA", 2 },
		{ "BB", 1 },
		{ "BD", 1 },
		{ "BE", 2 },
		{ "BF", 2 },
		{ "BG", 2 },
		{ "BH", 2 },
		{ "BI", 2 },
		{ "BJ", 2 },
		{ "BM", 2 },
		{ "BN", 1 },
		{ "BO", 1 },
		{ "BR", 1 },
		{ "BS", 1 },
		{ "BT", 1 },
		{ "BW", 2 },
		{ "BY", 2 },
		{ "BZ", 1 },
		{ "CA", 1 },
		{ "CD", 2 },
		{ "CF", 2 },
		{ "CG", 2 },
		{ "CH", 2 },
		{ "CI", 2 },
		{ "CL", 1 },
		{ "CM", 2 },
		{ "CN", 3 },
		{ "CO", 1 },
		{ "CR", 1 },
		{ "CU", 1 },
		{ "CV", 2 },
		{ "CY", 2 },
		{ "CZ", 2 },
		{ "DE", 2 },
		{ "DJ", 2 },
		{ "DK", 2 },
		{ "DM", 1 },
		{ "DO", 1 },
		{ "DZ", 2 },
		{ "EC", 1 },
		{ "EE", 2 },
		{ "EG", 2 },
		{ "ER", 2 },
		{ "ES", 2 },
		{ "ET", 2 },
		{ "FI", 2 },
		{ "FJ", 1 },
		{ "FK", 2 },
		{ "FO", 2 },
		{ "FR", 2 },
		{ "GA", 2 },
		{ "GB", 2 },
		{ "GD", 1 },
		{ "GE", 2 },
		{ "GL", 2 },
		{ "GM", 2 },
		{ "GN", 2 },
		{ "GQ", 2 },
		{ "GR", 2 },
		{ "GS", 2 },
		{ "GT", 1 },
		{ "GW", 2 },
		{ "GY", 1 },
		{ "HK", 3 },
		{ "HN", 1 },
		{ "HR", 2 },
		{ "HT", 1 },
		{ "HU", 2 },
		{ "ID", 1 },
		{ "IE", 2 },
		{ "IL", 2 },
		{ "IM", 2 },
		{ "IN", 1 },
		{ "IQ", 2 },
		{ "IR", 2 },
		{ "IS", 2 },
		{ "IT", 2 },
		{ "JM", 1 },
		{ "JO", 2 },
		{ "JP", 3 },
		{ "KE", 2 },
		{ "KG", 2 },
		{ "KH", 2 },
		{ "KI", 1 },
		{ "KM", 2 },
		{ "KP", 1 },
		{ "KR", 3 },
		{ "KW", 2 },
		{ "KY", 2 },
		{ "KZ", 2 },
		{ "LA", 1 },
		{ "LB", 2 },
		{ "LC", 1 },
		{ "LI", 2 },
		{ "LK", 1 },
		{ "LR", 2 },
		{ "LS", 2 },
		{ "LT", 2 },
		{ "LU", 2 },
		{ "LV", 2 },
		{ "LY", 2 },
		{ "MA", 2 },
		{ "MC", 2 },
		{ "MD", 2 },
		{ "ME", 2 },
		{ "MG", 2 },
		{ "MK", 2 },
		{ "ML", 2 },
		{ "MM", 1 },
		{ "MN", 2 },
		{ "MO", 3 },
		{ "MR", 2 },
		{ "MT", 2 },
		{ "MU", 2 },
		{ "MV", 2 },
		{ "MW", 2 },
		{ "MX", 1 },
		{ "MY", 1 },
		{ "MZ", 2 },
		{ "NA", 2 },
		{ "NC", 2 },
		{ "NE", 2 },
		{ "NG", 2 },
		{ "NI", 1 },
		{ "NL", 2 },
		{ "NO", 2 },
		{ "NP", 1 },
		{ "NR", 1 },
		{ "NZ", 1 },
		{ "OM", 2 },
		{ "PA", 1 },
		{ "PE", 1 },
		{ "PF", 1 },
		{ "PG", 1 },
		{ "PH", 1 },
		{ "PK", 2 },
		{ "PL", 2 },
		{ "PT", 2 },
		{ "PY", 1 },
		{ "QA", 2 },
		{ "RO", 2 },
		{ "RS", 2 },
		{ "RU", 2 },
		{ "RW", 2 },
		{ "SA", 2 },
		{ "SB", 1 },
		{ "SC", 2 },
		{ "SD", 2 },
		{ "SE", 2 },
		{ "SG", 1 },
		{ "SH", 2 },
		{ "SI", 2 },
		{ "SK", 2 },
		{ "SL", 2 },
		{ "SN", 2 },
		{ "SO", 2 },
		{ "SR", 2 },
		{ "ST", 2 },
		{ "SV", 1 },
		{ "SY", 2 },
		{ "SZ", 2 },
		{ "TD", 2 },
		{ "TG", 2 },
		{ "TH", 1 },
		{ "TJ", 2 },
		{ "TL", 1 },
		{ "TM", 2 },
		{ "TN", 2 },
		{ "TO", 1 },
		{ "TR", 2 },
		{ "TT", 1 },
		{ "TV", 1 },
		{ "TW", 3 },
		{ "TZ", 2 },
		{ "UA", 2 },
		{ "UG", 2 },
		{ "US", 1 },
		{ "UY", 1 },
		{ "UZ", 2 },
		{ "VA", 2 },
		{ "VC", 1 },
		{ "VE", 1 },
		{ "VN", 1 },
		{ "VU", 1 },
		{ "WS", 1 },
		{ "YE", 2 },
		{ "YU", 2 },
		{ "ZA", 2 },
		{ "ZM", 2 },
		{ "ZW", 2 }
	};

	private static Map<constants.BnetRegion, ConnectionData> s_regionIdToProdIP;

	private static ConnectionData s_defaultProdIP;

	public static Map<constants.BnetRegion, ConnectionData> s_regionIdToDevIP;

	public const string ptrServerVersion = "BETA";

	private static constants.BnetRegion s_defaultDevRegion;

	public static constants.BnetRegion FindDevRegionByServerVersion(string version)
	{
		foreach (constants.BnetRegion key in s_regionIdToDevIP.Keys)
		{
			if (version == s_regionIdToDevIP[key].version)
			{
				return key;
			}
		}
		return constants.BnetRegion.REGION_UNINITIALIZED;
	}

	public static constants.BnetRegion GetCurrentRegionId()
	{
		if (PlatformSettings.LocaleVariant == LocaleVariant.China)
		{
			return constants.BnetRegion.REGION_CN;
		}
		int value = Options.Get().GetInt(Option.PREFERRED_REGION, -1);
		if (value < 0)
		{
			if (UseClientConfigForEnv())
			{
				constants.BnetRegion bnetRegion = FindDevRegionByServerVersion(Vars.Key("Aurora.Version.String").GetStr(""));
				Log.BattleNet.Print("Battle.net region from client.config version: " + bnetRegion);
				if (bnetRegion != constants.BnetRegion.REGION_UNINITIALIZED)
				{
					return bnetRegion;
				}
			}
			try
			{
				if (!s_countryCodeToRegionId.TryGetValue(GetCountryCode(), out value))
				{
					value = 1;
					return (constants.BnetRegion)value;
				}
				return (constants.BnetRegion)value;
			}
			catch (Exception)
			{
				return (constants.BnetRegion)value;
			}
		}
		return (constants.BnetRegion)value;
	}

	public static List<string> GetRegionCodesForCurrentRegionId()
	{
		List<string> list = new List<string>();
		int currentRegionId = (int)GetCurrentRegionId();
		foreach (KeyValuePair<string, int> item in s_countryCodeToRegionId)
		{
			if (item.Value == currentRegionId)
			{
				list.Add(item.Key);
			}
		}
		return list;
	}

	public static ConnectionData GetConnectionDataFromRegionId(constants.BnetRegion region, bool isDev)
	{
		ConnectionData value;
		if (isDev)
		{
			if (!s_regionIdToDevIP.TryGetValue(region, out value) && !s_regionIdToDevIP.TryGetValue(s_defaultDevRegion, out value))
			{
				Debug.LogError("Invalid region set for s_defaultDevRegion!  This should never happen!!!");
			}
		}
		else if (!s_regionIdToProdIP.TryGetValue(region, out value))
		{
			return s_defaultProdIP;
		}
		return value;
	}

	public static Locale GetBestGuessForLocale()
	{
		Locale value = Locale.enUS;
		string text = GetLanguageCode();
		if (PlatformSettings.LocaleVariant == LocaleVariant.China)
		{
			value = ((!(text == "en")) ? Locale.zhCN : Locale.enUS);
		}
		else
		{
			bool flag = false;
			try
			{
				flag = s_languageCodeToLocale.TryGetValue(text, out value);
			}
			catch (Exception)
			{
			}
			if (!flag)
			{
				text = text.Substring(0, 2);
				try
				{
					flag = s_languageCodeToLocale.TryGetValue(text, out value);
				}
				catch (Exception)
				{
				}
			}
			if (!flag)
			{
				int value2 = 1;
				string countryCode = GetCountryCode();
				try
				{
					s_countryCodeToRegionId.TryGetValue(countryCode, out value2);
				}
				catch (Exception)
				{
				}
				value = text switch
				{
					"es" => (value2 != 1) ? Locale.esES : Locale.esMX, 
					"zh" => (!(countryCode == "CN")) ? Locale.zhTW : Locale.zhCN, 
					"en" => (value2 == 2) ? Locale.enGB : Locale.enUS, 
					_ => Locale.enUS, 
				};
			}
		}
		return value;
	}

	public static bool UseClientConfigForEnv()
	{
		bool flag = Vars.Key("Aurora.Env.Override").GetInt(0) != 0;
		if (Vars.Key("Aurora.Env.DisableOverrideOnDevices").GetInt(0) != 0)
		{
			flag = false;
		}
		string str = Vars.Key("Aurora.Env").GetStr("");
		bool flag2 = str != null && !(str == "");
		return flag && flag2;
	}

	public static string GetCountryCode()
	{
		return GetLocaleCountryCode();
	}

	public static string GetLanguageCode()
	{
		return GetLocaleLanguageCode();
	}

	private static string GetLocaleCountryCode()
	{
		return "";
	}

	private static string GetLocaleLanguageCode()
	{
		return "";
	}

	static MobileDeviceLocale()
	{
		Map<constants.BnetRegion, ConnectionData> map = new Map<constants.BnetRegion, ConnectionData>();
		ConnectionData value = new ConnectionData
		{
			address = "us.actual.battle.net",
			port = 1119,
			version = "product"
		};
		map.Add(constants.BnetRegion.REGION_UNKNOWN, value);
		value = new ConnectionData
		{
			address = "us.actual.battle.net",
			port = 1119,
			version = "product"
		};
		map.Add(constants.BnetRegion.REGION_US, value);
		value = new ConnectionData
		{
			address = "eu.actual.battle.net",
			port = 1119,
			version = "product"
		};
		map.Add(constants.BnetRegion.REGION_EU, value);
		value = new ConnectionData
		{
			address = "kr.actual.battle.net",
			port = 1119,
			version = "product"
		};
		map.Add(constants.BnetRegion.REGION_KR, value);
		value = new ConnectionData
		{
			address = "kr.actual.battle.net",
			port = 1119,
			version = "product"
		};
		map.Add(constants.BnetRegion.REGION_TW, value);
		value = new ConnectionData
		{
			address = "cn.actual.battle.net",
			port = 1119,
			version = "product"
		};
		map.Add(constants.BnetRegion.REGION_CN, value);
		value = new ConnectionData
		{
			address = "beta.actual.battle.net",
			port = 1119,
			version = "LOC"
		};
		map.Add(constants.BnetRegion.REGION_PTR_LOC, value);
		s_regionIdToProdIP = map;
		value = new ConnectionData
		{
			address = "us.actual.battle.net",
			port = 1119,
			version = "product"
		};
		s_defaultProdIP = value;
		Map<constants.BnetRegion, ConnectionData> map2 = new Map<constants.BnetRegion, ConnectionData>();
		value = new ConnectionData
		{
			name = "dev25-qaus",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-qaus",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.51"
		};
		map2.Add(constants.BnetRegion.REGION_US, value);
		value = new ConnectionData
		{
			name = "dev25-qacn",
			address = "st5.bgs.battle.net",
			port = 1119,
			version = "dev25-qacn",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.65"
		};
		map2.Add(constants.BnetRegion.REGION_CN, value);
		value = new ConnectionData
		{
			name = "dev25-qaeu",
			address = "st2.bgs.battle.net",
			port = 1119,
			version = "dev25-qaeu",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.59"
		};
		map2.Add(constants.BnetRegion.REGION_EU, value);
		value = new ConnectionData
		{
			name = "dev25-qakr",
			address = "st3.bgs.battle.net",
			port = 1119,
			version = "dev25-qakr",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.60"
		};
		map2.Add(constants.BnetRegion.REGION_KR, value);
		value = new ConnectionData
		{
			name = "dev25-loc-cn",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-loc-cn",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.39"
		};
		map2.Add((constants.BnetRegion)42, value);
		value = new ConnectionData
		{
			name = "dev25-loc-eu",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-loc-eu",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.37"
		};
		map2.Add((constants.BnetRegion)43, value);
		value = new ConnectionData
		{
			name = "dev25-loc-kr",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-loc-kr",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.38"
		};
		map2.Add((constants.BnetRegion)44, value);
		value = new ConnectionData
		{
			name = "dev25-loc-latam",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-loc-latam",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.41"
		};
		map2.Add((constants.BnetRegion)45, value);
		value = new ConnectionData
		{
			name = "dev25-loc-tw",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-loc-tw",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.33"
		};
		map2.Add((constants.BnetRegion)46, value);
		value = new ConnectionData
		{
			name = "dev25-dev1",
			address = "dev25.bgs.battle.net",
			port = 1119,
			version = "dev25-dev1",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.71"
		};
		map2.Add((constants.BnetRegion)75, value);
		value = new ConnectionData
		{
			name = "dev25-dev2",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-dev2",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.72"
		};
		map2.Add((constants.BnetRegion)76, value);
		value = new ConnectionData
		{
			name = "dev25-dev3",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev3",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.73"
		};
		map2.Add((constants.BnetRegion)77, value);
		value = new ConnectionData
		{
			name = "dev25-dev4",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev4",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.52"
		};
		map2.Add((constants.BnetRegion)78, value);
		value = new ConnectionData
		{
			name = "dev25-dev5",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev5",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.53"
		};
		map2.Add((constants.BnetRegion)79, value);
		value = new ConnectionData
		{
			name = "dev25-dev6",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev6",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.139"
		};
		map2.Add((constants.BnetRegion)80, value);
		value = new ConnectionData
		{
			name = "dev25-qa1",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-qa1",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.62"
		};
		map2.Add((constants.BnetRegion)81, value);
		value = new ConnectionData
		{
			name = "dev25-qa2",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-qa2",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.161.132"
		};
		map2.Add((constants.BnetRegion)82, value);
		value = new ConnectionData
		{
			name = "dev25-qa3",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-qa3",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.162.23"
		};
		map2.Add((constants.BnetRegion)83, value);
		value = new ConnectionData
		{
			name = "dev25-qa4",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-qa4",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.68"
		};
		map2.Add((constants.BnetRegion)84, value);
		value = new ConnectionData
		{
			name = "dev25-qa5",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-qa5",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.69"
		};
		map2.Add((constants.BnetRegion)85, value);
		value = new ConnectionData
		{
			name = "dev25-qa6",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-qa6",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.70"
		};
		map2.Add((constants.BnetRegion)86, value);
		value = new ConnectionData
		{
			name = "dev25-qa7",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-qa7",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.23"
		};
		map2.Add((constants.BnetRegion)87, value);
		value = new ConnectionData
		{
			name = "dev25-qa8",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-qa8",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.163.34"
		};
		map2.Add((constants.BnetRegion)88, value);
		value = new ConnectionData
		{
			name = "dev25-qa9",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-qa9",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.25"
		};
		map2.Add((constants.BnetRegion)89, value);
		value = new ConnectionData
		{
			name = "dev25-fireside",
			address = "st1.bgs.battle.net",
			port = 1119,
			version = "dev25-fireside",
			tutorialPort = 3725u,
			gameServerAddress = "10.63.160.56"
		};
		map2.Add((constants.BnetRegion)90, value);
		s_regionIdToDevIP = map2;
		s_defaultDevRegion = constants.BnetRegion.REGION_US;
	}
}
