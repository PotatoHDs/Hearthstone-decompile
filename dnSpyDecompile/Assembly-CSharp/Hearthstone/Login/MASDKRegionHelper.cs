using System;
using System.Collections.Generic;
using bgs;
using Blizzard.MobileAuth;

namespace Hearthstone.Login
{
	// Token: 0x0200113F RID: 4415
	internal class MASDKRegionHelper
	{
		// Token: 0x0600C16E RID: 49518 RVA: 0x003AC1A4 File Offset: 0x003AA3A4
		public static string GetRegionIdForBGSRegion(constants.BnetRegion bgsRegion)
		{
			string result;
			if (MASDKRegionHelper.REGION_TO_MASDK_ID_MAP.TryGetValue(bgsRegion, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600C16F RID: 49519 RVA: 0x003AC1C4 File Offset: 0x003AA3C4
		public static string GetRegionIdForInternalEnvironment(string environment)
		{
			string result;
			if (MASDKRegionHelper.INTERNAL_ENVIRONMENT_MAP.TryGetValue(environment, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600C170 RID: 49520 RVA: 0x003AC1E4 File Offset: 0x003AA3E4
		public static Region GetCurrentlyConnectedRegion()
		{
			string b;
			if (HearthstoneApplication.IsInternal())
			{
				b = MASDKRegionHelper.GetRegionIdForInternalEnvironment(BattleNet.GetEnvironment());
			}
			else
			{
				constants.BnetRegion bnetRegion = (constants.BnetRegion)Options.Get().GetInt(Option.PREFERRED_REGION, -1);
				if (bnetRegion == constants.BnetRegion.REGION_UNINITIALIZED)
				{
					bnetRegion = MobileDeviceLocale.GetCurrentRegionId();
				}
				b = MASDKRegionHelper.GetRegionIdForBGSRegion(bnetRegion);
			}
			foreach (Region region in MASDKRegionHelper.GetConfiguredRegions())
			{
				if (region.regionId == b)
				{
					return region;
				}
			}
			return default(Region);
		}

		// Token: 0x0600C171 RID: 49521 RVA: 0x003AC25D File Offset: 0x003AA45D
		public static Region[] GetConfiguredRegions()
		{
			return MASDKRegionHelper.m_lastSetRegions;
		}

		// Token: 0x0600C172 RID: 49522 RVA: 0x003AC264 File Offset: 0x003AA464
		public static void SetConfiguredRegions(Region[] regions)
		{
			MASDKRegionHelper.m_lastSetRegions = regions;
		}

		// Token: 0x0600C173 RID: 49523 RVA: 0x003AC26C File Offset: 0x003AA46C
		public static void ChangePreferredRegionFromRegionId(string regionId)
		{
			foreach (KeyValuePair<constants.BnetRegion, string> keyValuePair in MASDKRegionHelper.REGION_TO_MASDK_ID_MAP)
			{
				if (keyValuePair.Value == regionId)
				{
					global::Log.Login.PrintInfo("Changing Region to {0}", new object[]
					{
						keyValuePair.Value
					});
					Options.Get().SetInt(Option.PREFERRED_REGION, (int)keyValuePair.Key);
					return;
				}
			}
			global::Log.Login.PrintWarning("Could not find a valid region to switch to for regionId {0}", new object[]
			{
				regionId
			});
		}

		// Token: 0x0600C174 RID: 49524 RVA: 0x003AC314 File Offset: 0x003AA514
		public static string GetBGSRegionStringFromRegionId(string regionId)
		{
			string result;
			if (MASDKRegionHelper.REGTION_ID_TO_BGS_REGION_MAP.TryGetValue(regionId, out result))
			{
				return result;
			}
			global::Log.Login.PrintWarning("Could not find a valid region string for regionId {0}. Defaulting to US", new object[]
			{
				regionId
			});
			return MASDKRegionHelper.REGTION_ID_TO_BGS_REGION_MAP["US"];
		}

		// Token: 0x04009C20 RID: 39968
		public const string US_FRONT_REGION_ID = "US";

		// Token: 0x04009C21 RID: 39969
		public const string EU_FRONT_REGION_ID = "EU";

		// Token: 0x04009C22 RID: 39970
		public const string KR_FRONT_REGION_ID = "KR";

		// Token: 0x04009C23 RID: 39971
		public const string CN_FRONT_REGION_ID = "CN";

		// Token: 0x04009C24 RID: 39972
		public const string DEV_US_FRONT_REGION_ID = "US(DEV)";

		// Token: 0x04009C25 RID: 39973
		public const string DEV_EU_FRONT_REGION_ID = "EU(DEV)";

		// Token: 0x04009C26 RID: 39974
		public const string DEV_KR_FRONT_REGION_ID = "KR(DEV)";

		// Token: 0x04009C27 RID: 39975
		public const string DEV_CN_FRONT_REGION_ID = "CN(DEV)";

		// Token: 0x04009C28 RID: 39976
		private static readonly global::Map<constants.BnetRegion, string> REGION_TO_MASDK_ID_MAP = new global::Map<constants.BnetRegion, string>
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

		// Token: 0x04009C29 RID: 39977
		private static readonly global::Map<string, string> INTERNAL_ENVIRONMENT_MAP = new global::Map<string, string>
		{
			{
				"st1.bgs.battle.net",
				"US(DEV)"
			},
			{
				"st1-debug.bgs.battle.net",
				"US(DEV)"
			},
			{
				"st21.bgs.battle.net",
				"US(DEV)"
			},
			{
				"st2.bgs.battle.net",
				"EU(DEV)"
			},
			{
				"st22.bgs.battle.net",
				"EU(DEV)"
			},
			{
				"st3.bgs.battle.net",
				"KR(DEV)"
			},
			{
				"st23.bgs.battle.net",
				"KR(DEV)"
			},
			{
				"st5.bgs.battle.net",
				"CN(DEV)"
			},
			{
				"st25.bgs.battle.net",
				"CN(DEV)"
			}
		};

		// Token: 0x04009C2A RID: 39978
		private static readonly global::Map<string, string> REGTION_ID_TO_BGS_REGION_MAP = new global::Map<string, string>
		{
			{
				"US",
				"US"
			},
			{
				"EU",
				"EU"
			},
			{
				"KR",
				"KR"
			},
			{
				"CN",
				"CN"
			},
			{
				"US(DEV)",
				"US"
			},
			{
				"EU(DEV)",
				"EU"
			},
			{
				"KR(DEV)",
				"KR"
			},
			{
				"CN(DEV)",
				"CN"
			}
		};

		// Token: 0x04009C2B RID: 39979
		private static Region[] m_lastSetRegions;
	}
}
