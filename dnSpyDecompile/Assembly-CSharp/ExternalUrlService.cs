using System;
using System.Collections.Generic;
using Assets;
using bgs;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;

// Token: 0x020008A4 RID: 2212
public class ExternalUrlService : IService
{
	// Token: 0x06007AEF RID: 31471 RVA: 0x0027DFA8 File Offset: 0x0027C1A8
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	// Token: 0x06007AF0 RID: 31472 RVA: 0x0027DFB0 File Offset: 0x0027C1B0
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(GameDbf)
		};
	}

	// Token: 0x06007AF1 RID: 31473 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06007AF2 RID: 31474 RVA: 0x0027DFC5 File Offset: 0x0027C1C5
	public static ExternalUrlService Get()
	{
		return HearthstoneServices.Get<ExternalUrlService>();
	}

	// Token: 0x06007AF3 RID: 31475 RVA: 0x0027DFCC File Offset: 0x0027C1CC
	public string GetSetRotationInfoLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.SET_ROTATION, Array.Empty<string>());
	}

	// Token: 0x06007AF4 RID: 31476 RVA: 0x0027DFDA File Offset: 0x0027C1DA
	public string GetBreakingNewsLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.ALERT, new string[]
		{
			Localization.GetBnetLocaleName().ToLower()
		});
	}

	// Token: 0x06007AF5 RID: 31477 RVA: 0x0027DFF5 File Offset: 0x0027C1F5
	public string GetAccountCreationLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.CREATION, Array.Empty<string>());
	}

	// Token: 0x06007AF6 RID: 31478 RVA: 0x0027E002 File Offset: 0x0027C202
	public string GetAccountLandingLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.LANDING, new string[]
		{
			ExternalUrlService.GetPlatformWebAppId()
		});
	}

	// Token: 0x06007AF7 RID: 31479 RVA: 0x0027E018 File Offset: 0x0027C218
	public string GetFSGLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.FIRESIDE_GATHERINGS, Array.Empty<string>());
	}

	// Token: 0x06007AF8 RID: 31480 RVA: 0x0027E025 File Offset: 0x0027C225
	public string GetAccountHealUpLink(TemporaryAccountManager.HealUpReason reason, int wins = 0)
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.ACCOUNT_HEALUP, new string[]
		{
			ExternalUrlService.GetPlatformWebAppId(),
			reason.ToString(),
			wins.ToString()
		});
	}

	// Token: 0x06007AF9 RID: 31481 RVA: 0x0027E055 File Offset: 0x0027C255
	public string GetPrivacyPolicyLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.PRIVACY_POLICY, Array.Empty<string>());
	}

	// Token: 0x06007AFA RID: 31482 RVA: 0x0027E062 File Offset: 0x0027C262
	public string GetEULALink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.EULA, Array.Empty<string>());
	}

	// Token: 0x06007AFB RID: 31483 RVA: 0x0027E06F File Offset: 0x0027C26F
	public string GetDataManagementLink(string ssoToken)
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.DATA_MANAGEMENT, new string[]
		{
			ssoToken
		});
	}

	// Token: 0x06007AFC RID: 31484 RVA: 0x0027E081 File Offset: 0x0027C281
	public string GetSystemRequirementsLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.SYSTEM_REQUIREMENTS, Array.Empty<string>());
	}

	// Token: 0x06007AFD RID: 31485 RVA: 0x0027E08F File Offset: 0x0027C28F
	public string GetRecruitAFriendLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.RECRUIT_A_FRIEND, Array.Empty<string>());
	}

	// Token: 0x06007AFE RID: 31486 RVA: 0x0027E09D File Offset: 0x0027C29D
	public string GetTermsOfSaleLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.TERMS_OF_SALES, Array.Empty<string>());
	}

	// Token: 0x06007AFF RID: 31487 RVA: 0x0027E0AB File Offset: 0x0027C2AB
	public string GetCVVLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.CVV, Array.Empty<string>());
	}

	// Token: 0x06007B00 RID: 31488 RVA: 0x0027E0B9 File Offset: 0x0027C2B9
	public string GetResetPasswordLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.PASSWORD_RESET, Array.Empty<string>());
	}

	// Token: 0x06007B01 RID: 31489 RVA: 0x0027E0C7 File Offset: 0x0027C2C7
	public string GetDuplicatePurchaseErrorLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.DUPLICATE_PURCHASE_ERROR, Array.Empty<string>());
	}

	// Token: 0x06007B02 RID: 31490 RVA: 0x0027E0D5 File Offset: 0x0027C2D5
	public string GetDuplicateThirdPartyPurchaseLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.DUPLICATE_THIRDPARTY_PURCHASE, Array.Empty<string>());
	}

	// Token: 0x06007B03 RID: 31491 RVA: 0x0027E0E3 File Offset: 0x0027C2E3
	public string GetOutstandingPurchaseLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.OUTSTANDING_PURCHASE, Array.Empty<string>());
	}

	// Token: 0x06007B04 RID: 31492 RVA: 0x0027E0F1 File Offset: 0x0027C2F1
	public string GetPaymentInfoLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.PAYMENT_INFO, Array.Empty<string>());
	}

	// Token: 0x06007B05 RID: 31493 RVA: 0x0027E0FF File Offset: 0x0027C2FF
	public string GetGenericPurchaseErrorLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.GENERIC_PURCHASE_ERROR, Array.Empty<string>());
	}

	// Token: 0x06007B06 RID: 31494 RVA: 0x0027E10D File Offset: 0x0027C30D
	public string GetHearthstoneOnIpadLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.HEARTHSTONE_ON_IPAD, Array.Empty<string>());
	}

	// Token: 0x06007B07 RID: 31495 RVA: 0x0027E11B File Offset: 0x0027C31B
	public string GetAddPaymentLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.ADD_PAYMENT, Array.Empty<string>());
	}

	// Token: 0x06007B08 RID: 31496 RVA: 0x0027E129 File Offset: 0x0027C329
	public string GetCheckoutLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.CHECKOUT, Array.Empty<string>());
	}

	// Token: 0x06007B09 RID: 31497 RVA: 0x0027E137 File Offset: 0x0027C337
	public string GetCheckoutNavbarLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.CHECKOUT_NAVBAR, Array.Empty<string>());
	}

	// Token: 0x06007B0A RID: 31498 RVA: 0x0027E145 File Offset: 0x0027C345
	public string GetCustomerSupportLink()
	{
		return ExternalUrlService.BuildUrl(ExternalUrl.Endpoint.CUSTOMER_SUPPORT, Array.Empty<string>());
	}

	// Token: 0x06007B0B RID: 31499 RVA: 0x0027E154 File Offset: 0x0027C354
	private static string BuildUrl(ExternalUrl.Endpoint endpoint, params string[] args)
	{
		string regionStr = ExternalUrlService.GetRegionString();
		bool flag = HearthstoneApplication.GetMobileEnvironment() == MobileEnv.DEVELOPMENT || HearthstoneApplication.IsInternal();
		ExternalUrlDbfRecord externalUrlDbfRecord = null;
		if (flag)
		{
			externalUrlDbfRecord = GameDbf.ExternalUrl.GetRecord((ExternalUrlDbfRecord dbf) => dbf.AssetFlags == ExternalUrl.AssetFlags.DEV_ONLY && dbf.Endpoint == endpoint);
		}
		if (externalUrlDbfRecord == null)
		{
			externalUrlDbfRecord = GameDbf.ExternalUrl.GetRecord((ExternalUrlDbfRecord dbf) => dbf.Endpoint == endpoint);
		}
		if (externalUrlDbfRecord != null)
		{
			RegionOverridesDbfRecord regionOverridesDbfRecord = externalUrlDbfRecord.RegionOverrides.Find((RegionOverridesDbfRecord x) => x.Region == regionStr);
			string text = (regionOverridesDbfRecord == null) ? externalUrlDbfRecord.GlobalUrl : regionOverridesDbfRecord.OverrideUrl;
			string result;
			try
			{
				string text2 = string.Format(text, args);
				global::Log.BattleNet.PrintDebug("Url for endpoint {0}: {1}", new object[]
				{
					endpoint.ToString(),
					text2
				});
				result = text2;
			}
			catch (Exception ex)
			{
				global::Log.BattleNet.PrintError(ex.ToString(), Array.Empty<object>());
				global::Log.BattleNet.PrintError("Url for endpoint {0} could not be formatted, using unformatted URL instead: {1}", new object[]
				{
					endpoint.ToString(),
					text
				});
				result = text;
			}
			return result;
		}
		global::Log.BattleNet.PrintError("No external URL found for endpoint {0}", new object[]
		{
			endpoint.ToString()
		});
		if (regionStr == "CN")
		{
			return "https://www.blizzardgames.cn/";
		}
		return "https://www.blizzard.com/";
	}

	// Token: 0x06007B0C RID: 31500 RVA: 0x0027E2D0 File Offset: 0x0027C4D0
	public static string GetRegionString()
	{
		switch (PlatformSettings.IsMobile() ? MobileDeviceLocale.GetCurrentRegionId() : BattleNet.GetAccountRegion())
		{
		case constants.BnetRegion.REGION_US:
			return "US";
		case constants.BnetRegion.REGION_EU:
			return "EU";
		case constants.BnetRegion.REGION_KR:
			return "KR";
		case constants.BnetRegion.REGION_CN:
			return "CN";
		}
		return "US";
	}

	// Token: 0x06007B0D RID: 31501 RVA: 0x0027E32C File Offset: 0x0027C52C
	private static string GetPlatformWebAppId()
	{
		OSCategory os = PlatformSettings.OS;
		if (os == OSCategory.iOS)
		{
			return "wtcg-ios";
		}
		if (os == OSCategory.Android)
		{
			return "wtcg-and";
		}
		return "wtcg";
	}

	// Token: 0x04005ED9 RID: 24281
	private const string DefaultExternalUrl = "https://www.blizzard.com/";

	// Token: 0x04005EDA RID: 24282
	private const string DefaultExternalCNUrl = "https://www.blizzardgames.cn/";
}
