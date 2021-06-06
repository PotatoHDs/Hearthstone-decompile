using System;
using System.Collections.Generic;
using bgs;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using UnityEngine;
using UnityEngine.Profiling;

public class MobileCallbackManager : MonoBehaviour, IService
{
	private const string CHINESE_CURRENCY_CODE = "CNY";

	private const string CHINESE_COUNTRY_CODE = "CN";

	private const char RECEIPT_DATA_DELIMITER = '|';

	private const int LARGE_RECEIPT_CHAR_THRESHOLD = 9788;

	private const int MAX_TRIM_DELAY_SECONDS = 10;

	public bool m_wasBreakingNewsShown;

	private ulong m_nextTrimAvailableTime;

	private ulong m_trimDelay = 1uL;

	private static bool s_pushRegistrationSet;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		base.gameObject.AddComponent<CloudStorageManager>();
		base.gameObject.AddComponent<BreakingNews>();
		yield break;
	}

	public Type[] GetDependencies()
	{
		return null;
	}

	public void Shutdown()
	{
	}

	public static MobileCallbackManager Get()
	{
		return HearthstoneServices.Get<MobileCallbackManager>();
	}

	public void ClearCaches(LowMemorySeverity severity)
	{
		if (severity == LowMemorySeverity.CRITICAL && HearthstoneServices.TryGet<SpellCache>(out var service))
		{
			Debug.LogWarning("Clearing SpellCache");
			service.Clear();
		}
	}

	public void LowMemoryWarning(string msg)
	{
		ulong num = TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now);
		if (num < m_nextTrimAvailableTime)
		{
			Debug.Log("Ignored because it didn't pass max time(" + m_nextTrimAvailableTime + ")");
			return;
		}
		if (num - m_nextTrimAvailableTime > 10)
		{
			m_trimDelay = 1uL;
		}
		m_nextTrimAvailableTime = num + m_trimDelay;
		m_trimDelay *= 2uL;
		if (!EnumUtils.TryGetEnum<LowMemorySeverity>(msg, out var outVal))
		{
			outVal = LowMemorySeverity.MODERATE;
		}
		Debug.LogWarningFormat("Receiving LowMemoryWarning severity={0}", outVal);
		ClearCaches(outVal);
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.UnloadUnusedAssets();
		}
		PreviousInstanceStatus.LowMemoryCount++;
	}

	public static bool IsAndroidDeviceTabletSized()
	{
		if (Application.isEditor)
		{
			return true;
		}
		return false;
	}

	public static void RegisterPushNotifications()
	{
		Log.MobileCallback.Print("RegisterPushNotifications()");
		RegisterForPushNotifications();
	}

	public static void LogoutPushNotifications()
	{
		Log.MobileCallback.Print("LogoutPushNotifications()");
		LogoutForPushNotifications();
	}

	public static void SetPushRegistrationInfo(ulong bnetAccountId, constants.BnetRegion bnetRegion, string locale)
	{
		if (!s_pushRegistrationSet && bnetAccountId != 0L)
		{
			s_pushRegistrationSet = true;
			Log.MobileCallback.Print(string.Concat("SetPushRegistrationInfo([", bnetAccountId, "] ", bnetRegion, ", ", locale, ")"));
			string regionString = ExternalUrlService.GetRegionString();
			PushRegistrationInfo((long)bnetAccountId, regionString, locale);
		}
	}

	public static void SetTelemetryInfo(string programId, string programName, string programVersion, string sessionId)
	{
		Log.MobileCallback.Print("SetTelemetryInfo()");
		TelemetryInfo(programId, programName, programVersion, sessionId);
	}

	public static void SendPushAcknowledgement()
	{
		Log.MobileCallback.Print("SendPushAcknowledgement()");
		SendForPushAcknowledgement();
	}

	public static string[] ConsumeDeepLink(bool retain)
	{
		Log.MobileCallback.Print("GetDeepLink()");
		string text = ConsumeDeepLinkInfo(retain);
		if (string.IsNullOrEmpty(text))
		{
			text = GetStartupDeeplink();
		}
		Log.DeepLink.Print("Deep link info retrieved: " + text);
		string[] result = null;
		if (text != null && text.StartsWith("hearthstone://"))
		{
			result = text.Substring("hearthstone://".Length).Split('/');
		}
		return result;
	}

	public static bool RequestAppReview(bool forcePopupToShow = false)
	{
		Log.MobileCallback.Print("RequestAppReview()");
		int num = 0;
		if (!forcePopupToShow)
		{
			if (PlatformSettings.RuntimeOS != OSCategory.iOS && (PlatformSettings.RuntimeOS != OSCategory.Android || AndroidDeviceSettings.Get().GetAndroidStore() != AndroidStore.GOOGLE))
			{
				Log.MobileCallback.PrintInfo("No applicable storefront for rating app found.");
				return false;
			}
			if (NetCache.Get() != null)
			{
				NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
				if (!netObject.AppRatingEnabled)
				{
					return false;
				}
				float? num2 = BnetUtils.TryGetGameAccountId();
				if (num2.HasValue && num2.Value % 100f / 100f >= netObject.AppRatingSamplingPercentage)
				{
					return false;
				}
			}
			num = Options.Get().GetInt(Option.APP_RATING_POPUP_COUNT, 0);
			if (num >= 1)
			{
				return false;
			}
		}
		else
		{
			Log.MobileCallback.Print("Forcing app rating popup to show, bypassing popup limitations.");
		}
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_APP_RATING_REQUEST_POPUP_TITLE"),
			m_text = GameStrings.Get("GLUE_APP_RATING_REQUEST_POPUP_TEXT"),
			m_confirmText = GameStrings.Get("GLUE_APP_RATING_REQUEST_CONFIRM"),
			m_cancelText = GameStrings.Get("GLUE_APP_RATING_REQUEST_CANCEL"),
			m_showAlertIcon = true,
			m_iconTexture = new AssetReference("HS.tif:f7eebe7fed3c76b4da1dd53875182b34"),
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					TelemetryManager.Client().SendButtonPressed("AppEnjoymentAccept");
					ShowAppRatingPopup();
				}
				else
				{
					TelemetryManager.Client().SendButtonPressed("AppEnjoymentReject");
					ShowTroubleshootingPopup();
				}
			}
		};
		DialogManager.Get().ShowPopup(info);
		Options.Get().SetInt(Option.APP_RATING_POPUP_COUNT, num + 1);
		return true;
	}

	private static void ShowAppRatingPopup()
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_APP_RATING_REQUEST_POPUP_TITLE"),
			m_text = GameStrings.Get("GLUE_APP_RATING_POPUP_TEXT"),
			m_confirmText = GameStrings.Get("GLUE_APP_RATING_REQUEST_CONFIRM"),
			m_cancelText = GameStrings.Get("GLUE_APP_RATING_REQUEST_CANCEL"),
			m_showAlertIcon = true,
			m_iconTexture = new AssetReference("HS.tif:f7eebe7fed3c76b4da1dd53875182b34"),
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					TelemetryManager.Client().SendButtonPressed("AppReviewAccept");
					ShowNativeAppRatingPopup();
				}
				else
				{
					TelemetryManager.Client().SendButtonPressed("AppReviewReject");
				}
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	private static void ShowTroubleshootingPopup()
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_APP_RATING_REQUEST_POPUP_TITLE"),
			m_text = GameStrings.Get("GLUE_TROUBLESHOOTING_POPUP_TEXT"),
			m_confirmText = GameStrings.Get("GLUE_APP_RATING_REQUEST_CONFIRM"),
			m_cancelText = GameStrings.Get("GLUE_APP_RATING_REQUEST_CANCEL"),
			m_showAlertIcon = true,
			m_iconTexture = new AssetReference("HS.tif:f7eebe7fed3c76b4da1dd53875182b34"),
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					TelemetryManager.Client().SendButtonPressed("TroubleshootingAccept");
					Application.OpenURL(ExternalUrlService.Get().GetCustomerSupportLink());
				}
				else
				{
					TelemetryManager.Client().SendButtonPressed("TroubleshootingReject");
				}
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	public static int GetMemoryUsage()
	{
		return (int)Profiler.GetTotalAllocatedMemoryLong();
	}

	public static void CreateCrashPlugInLayer(string desc)
	{
	}

	public static void CreateCrashInNativeLayer(string desc)
	{
	}

	public static bool AreMotionEffectsEnabled()
	{
		return true;
	}

	private static void TelemetryInfo(string programId, string programName, string programVersion, string sessionId)
	{
	}

	private static bool IsDevice(string deviceModel)
	{
		return false;
	}

	private static void RegisterForPushNotifications()
	{
	}

	private static void LogoutForPushNotifications()
	{
	}

	private static void PushRegistrationInfo(long bnetAccountId, string bnetRegion, string locale)
	{
	}

	private static void SendForPushAcknowledgement()
	{
	}

	private static string ConsumeDeepLinkInfo(bool retain)
	{
		return "";
	}

	private static string GetStartupDeeplink()
	{
		return string.Empty;
	}

	public static string GetSharedKeychainIdentifier()
	{
		return string.Empty;
	}

	public static void ShowNativeAppRatingPopup()
	{
	}
}
