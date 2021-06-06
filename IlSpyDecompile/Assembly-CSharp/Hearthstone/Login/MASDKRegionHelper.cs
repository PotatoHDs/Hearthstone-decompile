using System.Collections.Generic;
using bgs;
using Blizzard.MobileAuth;

namespace Hearthstone.Login
{
	internal class MASDKRegionHelper
	{
		public const string US_FRONT_REGION_ID = "US";

		public const string EU_FRONT_REGION_ID = "EU";

		public const string KR_FRONT_REGION_ID = "KR";

		public const string CN_FRONT_REGION_ID = "CN";

		public const string DEV_US_FRONT_REGION_ID = "US(DEV)";

		public const string DEV_EU_FRONT_REGION_ID = "EU(DEV)";

		public const string DEV_KR_FRONT_REGION_ID = "KR(DEV)";

		public const string DEV_CN_FRONT_REGION_ID = "CN(DEV)";

		private static readonly Map<constants.BnetRegion, string> REGION_TO_MASDK_ID_MAP = new Map<constants.BnetRegion, string>
		{
			{
				constants.BnetRegion.REGION_US,
				"US"
			},
			{
				constants.BnetRegion.REGION_EU,
				"EU"
			},
			{
				constants.BnetRegion.REGION_KR,
				"KR"
			},
			{
				constants.BnetRegion.REGION_CN,
				"CN"
			}
		};

		private static readonly Map<string, string> INTERNAL_ENVIRONMENT_MAP = new Map<string, string>
		{
			{ "st1.bgs.battle.net", "US(DEV)" },
			{ "st1-debug.bgs.battle.net", "US(DEV)" },
			{ "st21.bgs.battle.net", "US(DEV)" },
			{ "st2.bgs.battle.net", "EU(DEV)" },
			{ "st22.bgs.battle.net", "EU(DEV)" },
			{ "st3.bgs.battle.net", "KR(DEV)" },
			{ "st23.bgs.battle.net", "KR(DEV)" },
			{ "st5.bgs.battle.net", "CN(DEV)" },
			{ "st25.bgs.battle.net", "CN(DEV)" }
		};

		private static readonly Map<string, string> REGTION_ID_TO_BGS_REGION_MAP = new Map<string, string>
		{
			{ "US", "US" },
			{ "EU", "EU" },
			{ "KR", "KR" },
			{ "CN", "CN" },
			{ "US(DEV)", "US" },
			{ "EU(DEV)", "EU" },
			{ "KR(DEV)", "KR" },
			{ "CN(DEV)", "CN" }
		};

		private static Region[] m_lastSetRegions;

		public static string GetRegionIdForBGSRegion(constants.BnetRegion bgsRegion)
		{
			if (REGION_TO_MASDK_ID_MAP.TryGetValue(bgsRegion, out var value))
			{
				return value;
			}
			return null;
		}

		public static string GetRegionIdForInternalEnvironment(string environment)
		{
			if (INTERNAL_ENVIRONMENT_MAP.TryGetValue(environment, out var value))
			{
				return value;
			}
			return null;
		}

		public static Region GetCurrentlyConnectedRegion()
		{
			string text;
			if (HearthstoneApplication.IsInternal())
			{
				text = GetRegionIdForInternalEnvironment(BattleNet.GetEnvironment());
			}
			else
			{
				constants.BnetRegion bnetRegion = (constants.BnetRegion)Options.Get().GetInt(Option.PREFERRED_REGION, -1);
				if (bnetRegion == constants.BnetRegion.REGION_UNINITIALIZED)
				{
					bnetRegion = MobileDeviceLocale.GetCurrentRegionId();
				}
				text = GetRegionIdForBGSRegion(bnetRegion);
			}
			Region[] configuredRegions = GetConfiguredRegions();
			for (int i = 0; i < configuredRegions.Length; i++)
			{
				Region result = configuredRegions[i];
				if (result.regionId == text)
				{
					return result;
				}
			}
			return default(Region);
		}

		public static Region[] GetConfiguredRegions()
		{
			return m_lastSetRegions;
		}

		public static void SetConfiguredRegions(Region[] regions)
		{
			m_lastSetRegions = regions;
		}

		public static void ChangePreferredRegionFromRegionId(string regionId)
		{
			foreach (KeyValuePair<constants.BnetRegion, string> item in REGION_TO_MASDK_ID_MAP)
			{
				if (item.Value == regionId)
				{
					Log.Login.PrintInfo("Changing Region to {0}", item.Value);
					Options.Get().SetInt(Option.PREFERRED_REGION, (int)item.Key);
					return;
				}
			}
			Log.Login.PrintWarning("Could not find a valid region to switch to for regionId {0}", regionId);
		}

		public static string GetBGSRegionStringFromRegionId(string regionId)
		{
			if (REGTION_ID_TO_BGS_REGION_MAP.TryGetValue(regionId, out var value))
			{
				return value;
			}
			Log.Login.PrintWarning("Could not find a valid region string for regionId {0}. Defaulting to US", regionId);
			return REGTION_ID_TO_BGS_REGION_MAP["US"];
		}
	}
}
