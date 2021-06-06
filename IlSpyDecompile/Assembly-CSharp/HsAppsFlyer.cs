using System;
using System.Collections.Generic;
using AFMiniJSON;
using AppsFlyerSDK;
using Blizzard.BlizzardErrorMobile;

public static class HsAppsFlyer
{
	private interface IAppsFlyerSDK
	{
		void initSDK(string devKey, string appID);

		void startSDK();

		void waitForATTUserAuthorizationWithTimeoutInterval(int timeoutInterval);

		void setCustomerUserId(string id);

		void setIsDebug(bool shouldEnable);

		void sendEvent(string eventName, Dictionary<string, string> eventValues);
	}

	private class AppsFlyerSDKImpl : IAppsFlyerSDK
	{
		public void initSDK(string devKey, string appID)
		{
			AppsFlyer.initSDK(devKey, appID);
		}

		public void startSDK()
		{
			AppsFlyer.startSDK();
		}

		public void waitForATTUserAuthorizationWithTimeoutInterval(int timeoutInterval)
		{
		}

		public void sendEvent(string eventName, Dictionary<string, string> eventValues)
		{
			AppsFlyer.sendEvent(eventName, eventValues);
		}

		public void setCustomerUserId(string id)
		{
			AppsFlyer.setCustomerUserId(id);
		}

		public void setIsDebug(bool shouldEnable)
		{
			AppsFlyer.setIsDebug(shouldEnable);
		}
	}

	public const int DEFAULT_ATT_AUTHORIZATION_TIMEOUT_SECONDS = 60;

	private const string DEV_KEY = "biU9Lo4fZQJRMhPK4VVZjP";

	private const string IOS_APP_ID = "625257520";

	private static bool s_enableDebugLogs;

	private static IAppsFlyerSDK s_sdk;

	public static void Initialize(int attUserAuthorizationTimeoutSeconds)
	{
		if (s_sdk != null)
		{
			Log.AdTracking.PrintWarning("AFSDK already initialized");
			return;
		}
		IAppsFlyerSDK appsFlyerSDK = null;
		appsFlyerSDK = new AppsFlyerSDKImpl();
		Log.AdTracking.PrintInfo("Initializing AFSDK");
		try
		{
			appsFlyerSDK.setIsDebug(s_enableDebugLogs);
			appsFlyerSDK.initSDK("biU9Lo4fZQJRMhPK4VVZjP", "625257520");
			if (attUserAuthorizationTimeoutSeconds > 0)
			{
				appsFlyerSDK.waitForATTUserAuthorizationWithTimeoutInterval(attUserAuthorizationTimeoutSeconds);
			}
			appsFlyerSDK.startSDK();
			s_sdk = appsFlyerSDK;
			Log.AdTracking.PrintInfo("AFSDK initialized");
		}
		catch (Exception ex)
		{
			Log.AdTracking.PrintError("Failed to initialize AFSDK: " + ex);
			ExceptionReporter.Get().ReportCaughtException(ex.Message, ex.StackTrace);
		}
	}

	public static void SetCustomerUserId(string id)
	{
		if (s_sdk == null)
		{
			Log.AdTracking.PrintWarning("AF not initialized. Can't set id.");
			return;
		}
		Log.AdTracking.PrintInfo("Applying AF customer user id");
		try
		{
			if (s_enableDebugLogs)
			{
				Log.AdTracking.PrintInfo("AF customer user id set to " + id);
			}
			s_sdk.setCustomerUserId(id);
		}
		catch (Exception ex)
		{
			Log.AdTracking.PrintError("Failed set AF customer user id: " + ex);
			ExceptionReporter.Get().ReportCaughtException(ex.Message, ex.StackTrace);
		}
	}

	public static void SendEvent(string eventName, Dictionary<string, string> eventValues)
	{
		if (s_sdk == null)
		{
			Log.AdTracking.PrintWarning("AF not initialized. Can't log event " + eventName + ".");
			return;
		}
		Log.AdTracking.PrintInfo("Logging AF event: " + eventName);
		try
		{
			if (s_enableDebugLogs)
			{
				Log.AdTracking.PrintInfo("    eventValues=" + Json.Serialize(eventValues));
			}
			s_sdk.sendEvent(eventName, eventValues);
		}
		catch (Exception ex)
		{
			Log.AdTracking.PrintError("Failed to log AF event: " + ex);
			ExceptionReporter.Get().ReportCaughtException(ex.Message, ex.StackTrace);
		}
	}
}
