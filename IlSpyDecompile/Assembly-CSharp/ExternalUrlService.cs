using System;
using System.Collections.Generic;
using Assets;
using bgs;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;

public class ExternalUrlService : IService
{
	private const string DefaultExternalUrl = "https://www.blizzard.com/";

	private const string DefaultExternalCNUrl = "https://www.blizzardgames.cn/";

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(GameDbf) };
	}

	public void Shutdown()
	{
	}

	public static ExternalUrlService Get()
	{
		return HearthstoneServices.Get<ExternalUrlService>();
	}

	public string GetSetRotationInfoLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.SET_ROTATION);
	}

	public string GetBreakingNewsLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.ALERT, Localization.GetBnetLocaleName().ToLower());
	}

	public string GetAccountCreationLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.CREATION);
	}

	public string GetAccountLandingLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.LANDING, GetPlatformWebAppId());
	}

	public string GetFSGLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.FIRESIDE_GATHERINGS);
	}

	public string GetAccountHealUpLink(TemporaryAccountManager.HealUpReason reason, int wins = 0)
	{
		return BuildUrl(ExternalUrl.Endpoint.ACCOUNT_HEALUP, GetPlatformWebAppId(), reason.ToString(), wins.ToString());
	}

	public string GetPrivacyPolicyLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.PRIVACY_POLICY);
	}

	public string GetEULALink()
	{
		return BuildUrl(ExternalUrl.Endpoint.EULA);
	}

	public string GetDataManagementLink(string ssoToken)
	{
		return BuildUrl(ExternalUrl.Endpoint.DATA_MANAGEMENT, ssoToken);
	}

	public string GetSystemRequirementsLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.SYSTEM_REQUIREMENTS);
	}

	public string GetRecruitAFriendLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.RECRUIT_A_FRIEND);
	}

	public string GetTermsOfSaleLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.TERMS_OF_SALES);
	}

	public string GetCVVLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.CVV);
	}

	public string GetResetPasswordLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.PASSWORD_RESET);
	}

	public string GetDuplicatePurchaseErrorLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.DUPLICATE_PURCHASE_ERROR);
	}

	public string GetDuplicateThirdPartyPurchaseLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.DUPLICATE_THIRDPARTY_PURCHASE);
	}

	public string GetOutstandingPurchaseLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.OUTSTANDING_PURCHASE);
	}

	public string GetPaymentInfoLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.PAYMENT_INFO);
	}

	public string GetGenericPurchaseErrorLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.GENERIC_PURCHASE_ERROR);
	}

	public string GetHearthstoneOnIpadLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.HEARTHSTONE_ON_IPAD);
	}

	public string GetAddPaymentLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.ADD_PAYMENT);
	}

	public string GetCheckoutLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.CHECKOUT);
	}

	public string GetCheckoutNavbarLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.CHECKOUT_NAVBAR);
	}

	public string GetCustomerSupportLink()
	{
		return BuildUrl(ExternalUrl.Endpoint.CUSTOMER_SUPPORT);
	}

	private static string BuildUrl(ExternalUrl.Endpoint endpoint, params string[] args)
	{
		string regionStr = GetRegionString();
		bool num = HearthstoneApplication.GetMobileEnvironment() == MobileEnv.DEVELOPMENT || HearthstoneApplication.IsInternal();
		ExternalUrlDbfRecord externalUrlDbfRecord = null;
		if (num)
		{
			externalUrlDbfRecord = GameDbf.ExternalUrl.GetRecord((ExternalUrlDbfRecord dbf) => dbf.AssetFlags == ExternalUrl.AssetFlags.DEV_ONLY && dbf.Endpoint == endpoint);
		}
		if (externalUrlDbfRecord == null)
		{
			externalUrlDbfRecord = GameDbf.ExternalUrl.GetRecord((ExternalUrlDbfRecord dbf) => dbf.Endpoint == endpoint);
		}
		if (externalUrlDbfRecord == null)
		{
			Log.BattleNet.PrintError("No external URL found for endpoint {0}", endpoint.ToString());
			if (regionStr == "CN")
			{
				return "https://www.blizzardgames.cn/";
			}
			return "https://www.blizzard.com/";
		}
		RegionOverridesDbfRecord regionOverridesDbfRecord = externalUrlDbfRecord.RegionOverrides.Find((RegionOverridesDbfRecord x) => x.Region == regionStr);
		string text = ((regionOverridesDbfRecord == null) ? externalUrlDbfRecord.GlobalUrl : regionOverridesDbfRecord.OverrideUrl);
		try
		{
			string text2 = string.Format(text, args);
			Log.BattleNet.PrintDebug("Url for endpoint {0}: {1}", endpoint.ToString(), text2);
			return text2;
		}
		catch (Exception ex)
		{
			Log.BattleNet.PrintError(ex.ToString());
			Log.BattleNet.PrintError("Url for endpoint {0} could not be formatted, using unformatted URL instead: {1}", endpoint.ToString(), text);
			return text;
		}
	}

	public static string GetRegionString()
	{
		return (PlatformSettings.IsMobile() ? MobileDeviceLocale.GetCurrentRegionId() : BattleNet.GetAccountRegion()) switch
		{
			constants.BnetRegion.REGION_US => "US", 
			constants.BnetRegion.REGION_EU => "EU", 
			constants.BnetRegion.REGION_KR => "KR", 
			constants.BnetRegion.REGION_CN => "CN", 
			_ => "US", 
		};
	}

	private static string GetPlatformWebAppId()
	{
		return PlatformSettings.OS switch
		{
			OSCategory.Android => "wtcg-and", 
			OSCategory.iOS => "wtcg-ios", 
			_ => "wtcg", 
		};
	}
}
