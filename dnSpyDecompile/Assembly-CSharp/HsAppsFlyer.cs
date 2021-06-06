using System;
using System.Collections.Generic;
using AFMiniJSON;
using AppsFlyerSDK;
using Blizzard.BlizzardErrorMobile;

// Token: 0x0200074E RID: 1870
public static class HsAppsFlyer
{
	// Token: 0x060069A4 RID: 27044 RVA: 0x00226C14 File Offset: 0x00224E14
	public static void Initialize(int attUserAuthorizationTimeoutSeconds)
	{
		if (HsAppsFlyer.s_sdk != null)
		{
			Log.AdTracking.PrintWarning("AFSDK already initialized", Array.Empty<object>());
			return;
		}
		HsAppsFlyer.IAppsFlyerSDK appsFlyerSDK = new HsAppsFlyer.AppsFlyerSDKImpl();
		Log.AdTracking.PrintInfo("Initializing AFSDK", Array.Empty<object>());
		try
		{
			appsFlyerSDK.setIsDebug(HsAppsFlyer.s_enableDebugLogs);
			appsFlyerSDK.initSDK("biU9Lo4fZQJRMhPK4VVZjP", "625257520");
			if (attUserAuthorizationTimeoutSeconds > 0)
			{
				appsFlyerSDK.waitForATTUserAuthorizationWithTimeoutInterval(attUserAuthorizationTimeoutSeconds);
			}
			appsFlyerSDK.startSDK();
			HsAppsFlyer.s_sdk = appsFlyerSDK;
			Log.AdTracking.PrintInfo("AFSDK initialized", Array.Empty<object>());
		}
		catch (Exception ex)
		{
			Log.AdTracking.PrintError("Failed to initialize AFSDK: " + ex, Array.Empty<object>());
			ExceptionReporter.Get().ReportCaughtException(ex.Message, ex.StackTrace);
		}
	}

	// Token: 0x060069A5 RID: 27045 RVA: 0x00226CE4 File Offset: 0x00224EE4
	public static void SetCustomerUserId(string id)
	{
		if (HsAppsFlyer.s_sdk == null)
		{
			Log.AdTracking.PrintWarning("AF not initialized. Can't set id.", Array.Empty<object>());
			return;
		}
		Log.AdTracking.PrintInfo("Applying AF customer user id", Array.Empty<object>());
		try
		{
			if (HsAppsFlyer.s_enableDebugLogs)
			{
				Log.AdTracking.PrintInfo("AF customer user id set to " + id, Array.Empty<object>());
			}
			HsAppsFlyer.s_sdk.setCustomerUserId(id);
		}
		catch (Exception ex)
		{
			Log.AdTracking.PrintError("Failed set AF customer user id: " + ex, Array.Empty<object>());
			ExceptionReporter.Get().ReportCaughtException(ex.Message, ex.StackTrace);
		}
	}

	// Token: 0x060069A6 RID: 27046 RVA: 0x00226D94 File Offset: 0x00224F94
	public static void SendEvent(string eventName, Dictionary<string, string> eventValues)
	{
		if (HsAppsFlyer.s_sdk == null)
		{
			Log.AdTracking.PrintWarning("AF not initialized. Can't log event " + eventName + ".", Array.Empty<object>());
			return;
		}
		Log.AdTracking.PrintInfo("Logging AF event: " + eventName, Array.Empty<object>());
		try
		{
			if (HsAppsFlyer.s_enableDebugLogs)
			{
				Log.AdTracking.PrintInfo("    eventValues=" + Json.Serialize(eventValues), Array.Empty<object>());
			}
			HsAppsFlyer.s_sdk.sendEvent(eventName, eventValues);
		}
		catch (Exception ex)
		{
			Log.AdTracking.PrintError("Failed to log AF event: " + ex, Array.Empty<object>());
			ExceptionReporter.Get().ReportCaughtException(ex.Message, ex.StackTrace);
		}
	}

	// Token: 0x0400566A RID: 22122
	public const int DEFAULT_ATT_AUTHORIZATION_TIMEOUT_SECONDS = 60;

	// Token: 0x0400566B RID: 22123
	private const string DEV_KEY = "biU9Lo4fZQJRMhPK4VVZjP";

	// Token: 0x0400566C RID: 22124
	private const string IOS_APP_ID = "625257520";

	// Token: 0x0400566D RID: 22125
	private static bool s_enableDebugLogs;

	// Token: 0x0400566E RID: 22126
	private static HsAppsFlyer.IAppsFlyerSDK s_sdk;

	// Token: 0x0200232A RID: 9002
	private interface IAppsFlyerSDK
	{
		// Token: 0x06012A10 RID: 76304
		void initSDK(string devKey, string appID);

		// Token: 0x06012A11 RID: 76305
		void startSDK();

		// Token: 0x06012A12 RID: 76306
		void waitForATTUserAuthorizationWithTimeoutInterval(int timeoutInterval);

		// Token: 0x06012A13 RID: 76307
		void setCustomerUserId(string id);

		// Token: 0x06012A14 RID: 76308
		void setIsDebug(bool shouldEnable);

		// Token: 0x06012A15 RID: 76309
		void sendEvent(string eventName, Dictionary<string, string> eventValues);
	}

	// Token: 0x0200232B RID: 9003
	private class AppsFlyerSDKImpl : HsAppsFlyer.IAppsFlyerSDK
	{
		// Token: 0x06012A16 RID: 76310 RVA: 0x005117C8 File Offset: 0x0050F9C8
		public void initSDK(string devKey, string appID)
		{
			AppsFlyer.initSDK(devKey, appID);
		}

		// Token: 0x06012A17 RID: 76311 RVA: 0x005117D1 File Offset: 0x0050F9D1
		public void startSDK()
		{
			AppsFlyer.startSDK();
		}

		// Token: 0x06012A18 RID: 76312 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void waitForATTUserAuthorizationWithTimeoutInterval(int timeoutInterval)
		{
		}

		// Token: 0x06012A19 RID: 76313 RVA: 0x005117D8 File Offset: 0x0050F9D8
		public void sendEvent(string eventName, Dictionary<string, string> eventValues)
		{
			AppsFlyer.sendEvent(eventName, eventValues);
		}

		// Token: 0x06012A1A RID: 76314 RVA: 0x005117E1 File Offset: 0x0050F9E1
		public void setCustomerUserId(string id)
		{
			AppsFlyer.setCustomerUserId(id);
		}

		// Token: 0x06012A1B RID: 76315 RVA: 0x005117E9 File Offset: 0x0050F9E9
		public void setIsDebug(bool shouldEnable)
		{
			AppsFlyer.setIsDebug(shouldEnable);
		}
	}
}
