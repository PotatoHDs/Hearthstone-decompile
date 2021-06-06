using System;
using System.Collections.Generic;
using bgs;
using UnityEngine;

// Token: 0x020008EB RID: 2283
public class MobileDeviceLocale
{
	// Token: 0x06007E91 RID: 32401 RVA: 0x0028F3C4 File Offset: 0x0028D5C4
	public static constants.BnetRegion FindDevRegionByServerVersion(string version)
	{
		foreach (constants.BnetRegion bnetRegion in MobileDeviceLocale.s_regionIdToDevIP.Keys)
		{
			if (version == MobileDeviceLocale.s_regionIdToDevIP[bnetRegion].version)
			{
				return bnetRegion;
			}
		}
		return constants.BnetRegion.REGION_UNINITIALIZED;
	}

	// Token: 0x06007E92 RID: 32402 RVA: 0x0028F434 File Offset: 0x0028D634
	public static constants.BnetRegion GetCurrentRegionId()
	{
		if (PlatformSettings.LocaleVariant == LocaleVariant.China)
		{
			return constants.BnetRegion.REGION_CN;
		}
		int num = Options.Get().GetInt(Option.PREFERRED_REGION, -1);
		if (num < 0)
		{
			if (MobileDeviceLocale.UseClientConfigForEnv())
			{
				constants.BnetRegion bnetRegion = MobileDeviceLocale.FindDevRegionByServerVersion(Vars.Key("Aurora.Version.String").GetStr(""));
				global::Log.BattleNet.Print("Battle.net region from client.config version: " + bnetRegion, Array.Empty<object>());
				if (bnetRegion != constants.BnetRegion.REGION_UNINITIALIZED)
				{
					return bnetRegion;
				}
			}
			try
			{
				if (!MobileDeviceLocale.s_countryCodeToRegionId.TryGetValue(MobileDeviceLocale.GetCountryCode(), out num))
				{
					num = 1;
				}
			}
			catch (Exception)
			{
			}
		}
		return (constants.BnetRegion)num;
	}

	// Token: 0x06007E93 RID: 32403 RVA: 0x0028F4D0 File Offset: 0x0028D6D0
	public static List<string> GetRegionCodesForCurrentRegionId()
	{
		List<string> list = new List<string>();
		int currentRegionId = (int)MobileDeviceLocale.GetCurrentRegionId();
		foreach (KeyValuePair<string, int> keyValuePair in MobileDeviceLocale.s_countryCodeToRegionId)
		{
			if (keyValuePair.Value == currentRegionId)
			{
				list.Add(keyValuePair.Key);
			}
		}
		return list;
	}

	// Token: 0x06007E94 RID: 32404 RVA: 0x0028F540 File Offset: 0x0028D740
	public static MobileDeviceLocale.ConnectionData GetConnectionDataFromRegionId(constants.BnetRegion region, bool isDev)
	{
		MobileDeviceLocale.ConnectionData result;
		if (isDev)
		{
			if (!MobileDeviceLocale.s_regionIdToDevIP.TryGetValue(region, out result) && !MobileDeviceLocale.s_regionIdToDevIP.TryGetValue(MobileDeviceLocale.s_defaultDevRegion, out result))
			{
				Debug.LogError("Invalid region set for s_defaultDevRegion!  This should never happen!!!");
			}
		}
		else if (!MobileDeviceLocale.s_regionIdToProdIP.TryGetValue(region, out result))
		{
			result = MobileDeviceLocale.s_defaultProdIP;
		}
		return result;
	}

	// Token: 0x06007E95 RID: 32405 RVA: 0x0028F594 File Offset: 0x0028D794
	public static Locale GetBestGuessForLocale()
	{
		Locale result = Locale.enUS;
		string text = MobileDeviceLocale.GetLanguageCode();
		if (PlatformSettings.LocaleVariant == LocaleVariant.China)
		{
			if (text == "en")
			{
				result = Locale.enUS;
			}
			else
			{
				result = Locale.zhCN;
			}
		}
		else
		{
			bool flag = false;
			try
			{
				flag = MobileDeviceLocale.s_languageCodeToLocale.TryGetValue(text, out result);
			}
			catch (Exception)
			{
			}
			if (!flag)
			{
				text = text.Substring(0, 2);
				try
				{
					flag = MobileDeviceLocale.s_languageCodeToLocale.TryGetValue(text, out result);
				}
				catch (Exception)
				{
				}
			}
			if (!flag)
			{
				int num = 1;
				string countryCode = MobileDeviceLocale.GetCountryCode();
				try
				{
					MobileDeviceLocale.s_countryCodeToRegionId.TryGetValue(countryCode, out num);
				}
				catch (Exception)
				{
				}
				if (!(text == "es"))
				{
					if (!(text == "zh"))
					{
						if (!(text == "en"))
						{
							result = Locale.enUS;
						}
						else if (num == 2)
						{
							result = Locale.enGB;
						}
						else
						{
							result = Locale.enUS;
						}
					}
					else if (countryCode == "CN")
					{
						result = Locale.zhCN;
					}
					else
					{
						result = Locale.zhTW;
					}
				}
				else if (num == 1)
				{
					result = Locale.esMX;
				}
				else
				{
					result = Locale.esES;
				}
			}
		}
		return result;
	}

	// Token: 0x06007E96 RID: 32406 RVA: 0x0028F6A4 File Offset: 0x0028D8A4
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

	// Token: 0x06007E97 RID: 32407 RVA: 0x0028F709 File Offset: 0x0028D909
	public static string GetCountryCode()
	{
		return MobileDeviceLocale.GetLocaleCountryCode();
	}

	// Token: 0x06007E98 RID: 32408 RVA: 0x0028F710 File Offset: 0x0028D910
	public static string GetLanguageCode()
	{
		return MobileDeviceLocale.GetLocaleLanguageCode();
	}

	// Token: 0x06007E99 RID: 32409 RVA: 0x000D5239 File Offset: 0x000D3439
	private static string GetLocaleCountryCode()
	{
		return "";
	}

	// Token: 0x06007E9A RID: 32410 RVA: 0x000D5239 File Offset: 0x000D3439
	private static string GetLocaleLanguageCode()
	{
		return "";
	}

	// Token: 0x04006637 RID: 26167
	private static global::Map<string, Locale> s_languageCodeToLocale = new global::Map<string, Locale>
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

	// Token: 0x04006638 RID: 26168
	private static global::Map<string, int> s_countryCodeToRegionId = new global::Map<string, int>
	{
		{
			"AD",
			2
		},
		{
			"AE",
			2
		},
		{
			"AG",
			1
		},
		{
			"AL",
			2
		},
		{
			"AM",
			2
		},
		{
			"AO",
			2
		},
		{
			"AR",
			1
		},
		{
			"AT",
			2
		},
		{
			"AU",
			1
		},
		{
			"AZ",
			2
		},
		{
			"BA",
			2
		},
		{
			"BB",
			1
		},
		{
			"BD",
			1
		},
		{
			"BE",
			2
		},
		{
			"BF",
			2
		},
		{
			"BG",
			2
		},
		{
			"BH",
			2
		},
		{
			"BI",
			2
		},
		{
			"BJ",
			2
		},
		{
			"BM",
			2
		},
		{
			"BN",
			1
		},
		{
			"BO",
			1
		},
		{
			"BR",
			1
		},
		{
			"BS",
			1
		},
		{
			"BT",
			1
		},
		{
			"BW",
			2
		},
		{
			"BY",
			2
		},
		{
			"BZ",
			1
		},
		{
			"CA",
			1
		},
		{
			"CD",
			2
		},
		{
			"CF",
			2
		},
		{
			"CG",
			2
		},
		{
			"CH",
			2
		},
		{
			"CI",
			2
		},
		{
			"CL",
			1
		},
		{
			"CM",
			2
		},
		{
			"CN",
			3
		},
		{
			"CO",
			1
		},
		{
			"CR",
			1
		},
		{
			"CU",
			1
		},
		{
			"CV",
			2
		},
		{
			"CY",
			2
		},
		{
			"CZ",
			2
		},
		{
			"DE",
			2
		},
		{
			"DJ",
			2
		},
		{
			"DK",
			2
		},
		{
			"DM",
			1
		},
		{
			"DO",
			1
		},
		{
			"DZ",
			2
		},
		{
			"EC",
			1
		},
		{
			"EE",
			2
		},
		{
			"EG",
			2
		},
		{
			"ER",
			2
		},
		{
			"ES",
			2
		},
		{
			"ET",
			2
		},
		{
			"FI",
			2
		},
		{
			"FJ",
			1
		},
		{
			"FK",
			2
		},
		{
			"FO",
			2
		},
		{
			"FR",
			2
		},
		{
			"GA",
			2
		},
		{
			"GB",
			2
		},
		{
			"GD",
			1
		},
		{
			"GE",
			2
		},
		{
			"GL",
			2
		},
		{
			"GM",
			2
		},
		{
			"GN",
			2
		},
		{
			"GQ",
			2
		},
		{
			"GR",
			2
		},
		{
			"GS",
			2
		},
		{
			"GT",
			1
		},
		{
			"GW",
			2
		},
		{
			"GY",
			1
		},
		{
			"HK",
			3
		},
		{
			"HN",
			1
		},
		{
			"HR",
			2
		},
		{
			"HT",
			1
		},
		{
			"HU",
			2
		},
		{
			"ID",
			1
		},
		{
			"IE",
			2
		},
		{
			"IL",
			2
		},
		{
			"IM",
			2
		},
		{
			"IN",
			1
		},
		{
			"IQ",
			2
		},
		{
			"IR",
			2
		},
		{
			"IS",
			2
		},
		{
			"IT",
			2
		},
		{
			"JM",
			1
		},
		{
			"JO",
			2
		},
		{
			"JP",
			3
		},
		{
			"KE",
			2
		},
		{
			"KG",
			2
		},
		{
			"KH",
			2
		},
		{
			"KI",
			1
		},
		{
			"KM",
			2
		},
		{
			"KP",
			1
		},
		{
			"KR",
			3
		},
		{
			"KW",
			2
		},
		{
			"KY",
			2
		},
		{
			"KZ",
			2
		},
		{
			"LA",
			1
		},
		{
			"LB",
			2
		},
		{
			"LC",
			1
		},
		{
			"LI",
			2
		},
		{
			"LK",
			1
		},
		{
			"LR",
			2
		},
		{
			"LS",
			2
		},
		{
			"LT",
			2
		},
		{
			"LU",
			2
		},
		{
			"LV",
			2
		},
		{
			"LY",
			2
		},
		{
			"MA",
			2
		},
		{
			"MC",
			2
		},
		{
			"MD",
			2
		},
		{
			"ME",
			2
		},
		{
			"MG",
			2
		},
		{
			"MK",
			2
		},
		{
			"ML",
			2
		},
		{
			"MM",
			1
		},
		{
			"MN",
			2
		},
		{
			"MO",
			3
		},
		{
			"MR",
			2
		},
		{
			"MT",
			2
		},
		{
			"MU",
			2
		},
		{
			"MV",
			2
		},
		{
			"MW",
			2
		},
		{
			"MX",
			1
		},
		{
			"MY",
			1
		},
		{
			"MZ",
			2
		},
		{
			"NA",
			2
		},
		{
			"NC",
			2
		},
		{
			"NE",
			2
		},
		{
			"NG",
			2
		},
		{
			"NI",
			1
		},
		{
			"NL",
			2
		},
		{
			"NO",
			2
		},
		{
			"NP",
			1
		},
		{
			"NR",
			1
		},
		{
			"NZ",
			1
		},
		{
			"OM",
			2
		},
		{
			"PA",
			1
		},
		{
			"PE",
			1
		},
		{
			"PF",
			1
		},
		{
			"PG",
			1
		},
		{
			"PH",
			1
		},
		{
			"PK",
			2
		},
		{
			"PL",
			2
		},
		{
			"PT",
			2
		},
		{
			"PY",
			1
		},
		{
			"QA",
			2
		},
		{
			"RO",
			2
		},
		{
			"RS",
			2
		},
		{
			"RU",
			2
		},
		{
			"RW",
			2
		},
		{
			"SA",
			2
		},
		{
			"SB",
			1
		},
		{
			"SC",
			2
		},
		{
			"SD",
			2
		},
		{
			"SE",
			2
		},
		{
			"SG",
			1
		},
		{
			"SH",
			2
		},
		{
			"SI",
			2
		},
		{
			"SK",
			2
		},
		{
			"SL",
			2
		},
		{
			"SN",
			2
		},
		{
			"SO",
			2
		},
		{
			"SR",
			2
		},
		{
			"ST",
			2
		},
		{
			"SV",
			1
		},
		{
			"SY",
			2
		},
		{
			"SZ",
			2
		},
		{
			"TD",
			2
		},
		{
			"TG",
			2
		},
		{
			"TH",
			1
		},
		{
			"TJ",
			2
		},
		{
			"TL",
			1
		},
		{
			"TM",
			2
		},
		{
			"TN",
			2
		},
		{
			"TO",
			1
		},
		{
			"TR",
			2
		},
		{
			"TT",
			1
		},
		{
			"TV",
			1
		},
		{
			"TW",
			3
		},
		{
			"TZ",
			2
		},
		{
			"UA",
			2
		},
		{
			"UG",
			2
		},
		{
			"US",
			1
		},
		{
			"UY",
			1
		},
		{
			"UZ",
			2
		},
		{
			"VA",
			2
		},
		{
			"VC",
			1
		},
		{
			"VE",
			1
		},
		{
			"VN",
			1
		},
		{
			"VU",
			1
		},
		{
			"WS",
			1
		},
		{
			"YE",
			2
		},
		{
			"YU",
			2
		},
		{
			"ZA",
			2
		},
		{
			"ZM",
			2
		},
		{
			"ZW",
			2
		}
	};

	// Token: 0x04006639 RID: 26169
	private static global::Map<constants.BnetRegion, MobileDeviceLocale.ConnectionData> s_regionIdToProdIP = new global::Map<constants.BnetRegion, MobileDeviceLocale.ConnectionData>
	{
		{
			constants.BnetRegion.REGION_UNKNOWN,
			new MobileDeviceLocale.ConnectionData
			{
				address = "us.actual.battle.net",
				port = 1119,
				version = "product"
			}
		},
		{
			constants.BnetRegion.REGION_US,
			new MobileDeviceLocale.ConnectionData
			{
				address = "us.actual.battle.net",
				port = 1119,
				version = "product"
			}
		},
		{
			constants.BnetRegion.REGION_EU,
			new MobileDeviceLocale.ConnectionData
			{
				address = "eu.actual.battle.net",
				port = 1119,
				version = "product"
			}
		},
		{
			constants.BnetRegion.REGION_KR,
			new MobileDeviceLocale.ConnectionData
			{
				address = "kr.actual.battle.net",
				port = 1119,
				version = "product"
			}
		},
		{
			constants.BnetRegion.REGION_TW,
			new MobileDeviceLocale.ConnectionData
			{
				address = "kr.actual.battle.net",
				port = 1119,
				version = "product"
			}
		},
		{
			constants.BnetRegion.REGION_CN,
			new MobileDeviceLocale.ConnectionData
			{
				address = "cn.actual.battle.net",
				port = 1119,
				version = "product"
			}
		},
		{
			constants.BnetRegion.REGION_PTR_LOC,
			new MobileDeviceLocale.ConnectionData
			{
				address = "beta.actual.battle.net",
				port = 1119,
				version = "LOC"
			}
		}
	};

	// Token: 0x0400663A RID: 26170
	private static MobileDeviceLocale.ConnectionData s_defaultProdIP = new MobileDeviceLocale.ConnectionData
	{
		address = "us.actual.battle.net",
		port = 1119,
		version = "product"
	};

	// Token: 0x0400663B RID: 26171
	public static global::Map<constants.BnetRegion, MobileDeviceLocale.ConnectionData> s_regionIdToDevIP = new global::Map<constants.BnetRegion, MobileDeviceLocale.ConnectionData>
	{
		{
			constants.BnetRegion.REGION_US,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-qaus",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-qaus",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.51"
			}
		},
		{
			constants.BnetRegion.REGION_CN,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-qacn",
				address = "st5.bgs.battle.net",
				port = 1119,
				version = "dev25-qacn",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.65"
			}
		},
		{
			constants.BnetRegion.REGION_EU,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-qaeu",
				address = "st2.bgs.battle.net",
				port = 1119,
				version = "dev25-qaeu",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.59"
			}
		},
		{
			constants.BnetRegion.REGION_KR,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-qakr",
				address = "st3.bgs.battle.net",
				port = 1119,
				version = "dev25-qakr",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.60"
			}
		},
		{
			(constants.BnetRegion)42,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-loc-cn",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-loc-cn",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.39"
			}
		},
		{
			(constants.BnetRegion)43,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-loc-eu",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-loc-eu",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.37"
			}
		},
		{
			(constants.BnetRegion)44,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-loc-kr",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-loc-kr",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.38"
			}
		},
		{
			(constants.BnetRegion)45,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-loc-latam",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-loc-latam",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.41"
			}
		},
		{
			(constants.BnetRegion)46,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-loc-tw",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-loc-tw",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.33"
			}
		},
		{
			(constants.BnetRegion)75,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-dev1",
				address = "dev25.bgs.battle.net",
				port = 1119,
				version = "dev25-dev1",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.71"
			}
		},
		{
			(constants.BnetRegion)76,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-dev2",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-dev2",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.72"
			}
		},
		{
			(constants.BnetRegion)77,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-dev3",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev3",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.73"
			}
		},
		{
			(constants.BnetRegion)78,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-dev4",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev4",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.52"
			}
		},
		{
			(constants.BnetRegion)79,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-dev5",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev5",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.53"
			}
		},
		{
			(constants.BnetRegion)80,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-dev6",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev6",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.139"
			}
		},
		{
			(constants.BnetRegion)81,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-qa1",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-qa1",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.62"
			}
		},
		{
			(constants.BnetRegion)82,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-qa2",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-qa2",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.161.132"
			}
		},
		{
			(constants.BnetRegion)83,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-qa3",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-qa3",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.162.23"
			}
		},
		{
			(constants.BnetRegion)84,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-qa4",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-qa4",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.68"
			}
		},
		{
			(constants.BnetRegion)85,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-qa5",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-qa5",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.69"
			}
		},
		{
			(constants.BnetRegion)86,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-qa6",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-qa6",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.70"
			}
		},
		{
			(constants.BnetRegion)87,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-qa7",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-qa7",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.23"
			}
		},
		{
			(constants.BnetRegion)88,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-qa8",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-qa8",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.163.34"
			}
		},
		{
			(constants.BnetRegion)89,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-qa9",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-qa9",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.25"
			}
		},
		{
			(constants.BnetRegion)90,
			new MobileDeviceLocale.ConnectionData
			{
				name = "dev25-fireside",
				address = "st1.bgs.battle.net",
				port = 1119,
				version = "dev25-fireside",
				tutorialPort = 3725U,
				gameServerAddress = "10.63.160.56"
			}
		}
	};

	// Token: 0x0400663C RID: 26172
	public const string ptrServerVersion = "BETA";

	// Token: 0x0400663D RID: 26173
	private static constants.BnetRegion s_defaultDevRegion = constants.BnetRegion.REGION_US;

	// Token: 0x02002596 RID: 9622
	public struct ConnectionData
	{
		// Token: 0x0400EE05 RID: 60933
		public string address;

		// Token: 0x0400EE06 RID: 60934
		public int port;

		// Token: 0x0400EE07 RID: 60935
		public string version;

		// Token: 0x0400EE08 RID: 60936
		public string name;

		// Token: 0x0400EE09 RID: 60937
		public uint tutorialPort;

		// Token: 0x0400EE0A RID: 60938
		public string gameServerAddress;
	}
}
