using System;
using System.Collections.Generic;
using bgs;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using UnityEngine;
using UnityEngine.Profiling;

// Token: 0x020005EA RID: 1514
public class MobileCallbackManager : MonoBehaviour, IService
{
	// Token: 0x060052D5 RID: 21205 RVA: 0x001B1AAF File Offset: 0x001AFCAF
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		base.gameObject.AddComponent<CloudStorageManager>();
		base.gameObject.AddComponent<BreakingNews>();
		yield break;
	}

	// Token: 0x060052D6 RID: 21206 RVA: 0x00090064 File Offset: 0x0008E264
	public Type[] GetDependencies()
	{
		return null;
	}

	// Token: 0x060052D7 RID: 21207 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x060052D8 RID: 21208 RVA: 0x001B1ABE File Offset: 0x001AFCBE
	public static MobileCallbackManager Get()
	{
		return HearthstoneServices.Get<MobileCallbackManager>();
	}

	// Token: 0x060052D9 RID: 21209 RVA: 0x001B1AC8 File Offset: 0x001AFCC8
	public void ClearCaches(LowMemorySeverity severity)
	{
		SpellCache spellCache;
		if (severity == LowMemorySeverity.CRITICAL && HearthstoneServices.TryGet<SpellCache>(out spellCache))
		{
			Debug.LogWarning("Clearing SpellCache");
			spellCache.Clear();
		}
	}

	// Token: 0x060052DA RID: 21210 RVA: 0x001B1AF4 File Offset: 0x001AFCF4
	public void LowMemoryWarning(string msg)
	{
		ulong num = global::TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now);
		if (num < this.m_nextTrimAvailableTime)
		{
			Debug.Log("Ignored because it didn't pass max time(" + this.m_nextTrimAvailableTime + ")");
			return;
		}
		if (num - this.m_nextTrimAvailableTime > 10UL)
		{
			this.m_trimDelay = 1UL;
		}
		this.m_nextTrimAvailableTime = num + this.m_trimDelay;
		this.m_trimDelay *= 2UL;
		LowMemorySeverity lowMemorySeverity;
		if (!global::EnumUtils.TryGetEnum<LowMemorySeverity>(msg, out lowMemorySeverity))
		{
			lowMemorySeverity = LowMemorySeverity.MODERATE;
		}
		Debug.LogWarningFormat("Receiving LowMemoryWarning severity={0}", new object[]
		{
			lowMemorySeverity
		});
		this.ClearCaches(lowMemorySeverity);
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.UnloadUnusedAssets();
		}
		PreviousInstanceStatus.LowMemoryCount++;
	}

	// Token: 0x060052DB RID: 21211 RVA: 0x001B1BB4 File Offset: 0x001AFDB4
	public static bool IsAndroidDeviceTabletSized()
	{
		return Application.isEditor;
	}

	// Token: 0x060052DC RID: 21212 RVA: 0x001B1BC0 File Offset: 0x001AFDC0
	public static void RegisterPushNotifications()
	{
		global::Log.MobileCallback.Print("RegisterPushNotifications()", Array.Empty<object>());
		MobileCallbackManager.RegisterForPushNotifications();
	}

	// Token: 0x060052DD RID: 21213 RVA: 0x001B1BDB File Offset: 0x001AFDDB
	public static void LogoutPushNotifications()
	{
		global::Log.MobileCallback.Print("LogoutPushNotifications()", Array.Empty<object>());
		MobileCallbackManager.LogoutForPushNotifications();
	}

	// Token: 0x060052DE RID: 21214 RVA: 0x001B1BF8 File Offset: 0x001AFDF8
	public static void SetPushRegistrationInfo(ulong bnetAccountId, constants.BnetRegion bnetRegion, string locale)
	{
		if (MobileCallbackManager.s_pushRegistrationSet || bnetAccountId == 0UL)
		{
			return;
		}
		MobileCallbackManager.s_pushRegistrationSet = true;
		global::Log.MobileCallback.Print(string.Concat(new object[]
		{
			"SetPushRegistrationInfo([",
			bnetAccountId,
			"] ",
			bnetRegion,
			", ",
			locale,
			")"
		}), Array.Empty<object>());
		string regionString = ExternalUrlService.GetRegionString();
		MobileCallbackManager.PushRegistrationInfo((long)bnetAccountId, regionString, locale);
	}

	// Token: 0x060052DF RID: 21215 RVA: 0x001B1C74 File Offset: 0x001AFE74
	public static void SetTelemetryInfo(string programId, string programName, string programVersion, string sessionId)
	{
		global::Log.MobileCallback.Print("SetTelemetryInfo()", Array.Empty<object>());
		MobileCallbackManager.TelemetryInfo(programId, programName, programVersion, sessionId);
	}

	// Token: 0x060052E0 RID: 21216 RVA: 0x001B1C93 File Offset: 0x001AFE93
	public static void SendPushAcknowledgement()
	{
		global::Log.MobileCallback.Print("SendPushAcknowledgement()", Array.Empty<object>());
		MobileCallbackManager.SendForPushAcknowledgement();
	}

	// Token: 0x060052E1 RID: 21217 RVA: 0x001B1CB0 File Offset: 0x001AFEB0
	public static string[] ConsumeDeepLink(bool retain)
	{
		global::Log.MobileCallback.Print("GetDeepLink()", Array.Empty<object>());
		string text = MobileCallbackManager.ConsumeDeepLinkInfo(retain);
		if (string.IsNullOrEmpty(text))
		{
			text = MobileCallbackManager.GetStartupDeeplink();
		}
		global::Log.DeepLink.Print("Deep link info retrieved: " + text, Array.Empty<object>());
		string[] result = null;
		if (text != null && text.StartsWith("hearthstone://"))
		{
			result = text.Substring("hearthstone://".Length).Split(new char[]
			{
				'/'
			});
		}
		return result;
	}

	// Token: 0x060052E2 RID: 21218 RVA: 0x001B1D34 File Offset: 0x001AFF34
	public static bool RequestAppReview(bool forcePopupToShow = false)
	{
		global::Log.MobileCallback.Print("RequestAppReview()", Array.Empty<object>());
		int num = 0;
		if (!forcePopupToShow)
		{
			if (PlatformSettings.RuntimeOS != OSCategory.iOS && (PlatformSettings.RuntimeOS != OSCategory.Android || AndroidDeviceSettings.Get().GetAndroidStore() != AndroidStore.GOOGLE))
			{
				global::Log.MobileCallback.PrintInfo("No applicable storefront for rating app found.", Array.Empty<object>());
				return false;
			}
			if (NetCache.Get() != null)
			{
				NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
				if (!netObject.AppRatingEnabled)
				{
					return false;
				}
				ulong? num2 = BnetUtils.TryGetGameAccountId();
				float? num3 = (num2 != null) ? new float?(num2.GetValueOrDefault()) : null;
				if (num3 != null && num3.Value % 100f / 100f >= netObject.AppRatingSamplingPercentage)
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
			global::Log.MobileCallback.Print("Forcing app rating popup to show, bypassing popup limitations.", Array.Empty<object>());
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_APP_RATING_REQUEST_POPUP_TITLE");
		popupInfo.m_text = GameStrings.Get("GLUE_APP_RATING_REQUEST_POPUP_TEXT");
		popupInfo.m_confirmText = GameStrings.Get("GLUE_APP_RATING_REQUEST_CONFIRM");
		popupInfo.m_cancelText = GameStrings.Get("GLUE_APP_RATING_REQUEST_CANCEL");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_iconTexture = new AssetReference("HS.tif:f7eebe7fed3c76b4da1dd53875182b34");
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			if (response == AlertPopup.Response.CONFIRM)
			{
				TelemetryManager.Client().SendButtonPressed("AppEnjoymentAccept");
				MobileCallbackManager.ShowAppRatingPopup();
				return;
			}
			TelemetryManager.Client().SendButtonPressed("AppEnjoymentReject");
			MobileCallbackManager.ShowTroubleshootingPopup();
		};
		AlertPopup.PopupInfo info = popupInfo;
		DialogManager.Get().ShowPopup(info);
		Options.Get().SetInt(Option.APP_RATING_POPUP_COUNT, num + 1);
		return true;
	}

	// Token: 0x060052E3 RID: 21219 RVA: 0x001B1ECC File Offset: 0x001B00CC
	private static void ShowAppRatingPopup()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_APP_RATING_REQUEST_POPUP_TITLE");
		popupInfo.m_text = GameStrings.Get("GLUE_APP_RATING_POPUP_TEXT");
		popupInfo.m_confirmText = GameStrings.Get("GLUE_APP_RATING_REQUEST_CONFIRM");
		popupInfo.m_cancelText = GameStrings.Get("GLUE_APP_RATING_REQUEST_CANCEL");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_iconTexture = new AssetReference("HS.tif:f7eebe7fed3c76b4da1dd53875182b34");
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			if (response == AlertPopup.Response.CONFIRM)
			{
				TelemetryManager.Client().SendButtonPressed("AppReviewAccept");
				MobileCallbackManager.ShowNativeAppRatingPopup();
				return;
			}
			TelemetryManager.Client().SendButtonPressed("AppReviewReject");
		};
		AlertPopup.PopupInfo info = popupInfo;
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x060052E4 RID: 21220 RVA: 0x001B1F70 File Offset: 0x001B0170
	private static void ShowTroubleshootingPopup()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_APP_RATING_REQUEST_POPUP_TITLE");
		popupInfo.m_text = GameStrings.Get("GLUE_TROUBLESHOOTING_POPUP_TEXT");
		popupInfo.m_confirmText = GameStrings.Get("GLUE_APP_RATING_REQUEST_CONFIRM");
		popupInfo.m_cancelText = GameStrings.Get("GLUE_APP_RATING_REQUEST_CANCEL");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_iconTexture = new AssetReference("HS.tif:f7eebe7fed3c76b4da1dd53875182b34");
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			if (response == AlertPopup.Response.CONFIRM)
			{
				TelemetryManager.Client().SendButtonPressed("TroubleshootingAccept");
				Application.OpenURL(ExternalUrlService.Get().GetCustomerSupportLink());
				return;
			}
			TelemetryManager.Client().SendButtonPressed("TroubleshootingReject");
		};
		AlertPopup.PopupInfo info = popupInfo;
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x060052E5 RID: 21221 RVA: 0x001B2011 File Offset: 0x001B0211
	public static int GetMemoryUsage()
	{
		return (int)Profiler.GetTotalAllocatedMemoryLong();
	}

	// Token: 0x060052E6 RID: 21222 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public static void CreateCrashPlugInLayer(string desc)
	{
	}

	// Token: 0x060052E7 RID: 21223 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public static void CreateCrashInNativeLayer(string desc)
	{
	}

	// Token: 0x060052E8 RID: 21224 RVA: 0x000052EC File Offset: 0x000034EC
	public static bool AreMotionEffectsEnabled()
	{
		return true;
	}

	// Token: 0x060052E9 RID: 21225 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void TelemetryInfo(string programId, string programName, string programVersion, string sessionId)
	{
	}

	// Token: 0x060052EA RID: 21226 RVA: 0x0001FA65 File Offset: 0x0001DC65
	private static bool IsDevice(string deviceModel)
	{
		return false;
	}

	// Token: 0x060052EB RID: 21227 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void RegisterForPushNotifications()
	{
	}

	// Token: 0x060052EC RID: 21228 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void LogoutForPushNotifications()
	{
	}

	// Token: 0x060052ED RID: 21229 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void PushRegistrationInfo(long bnetAccountId, string bnetRegion, string locale)
	{
	}

	// Token: 0x060052EE RID: 21230 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void SendForPushAcknowledgement()
	{
	}

	// Token: 0x060052EF RID: 21231 RVA: 0x000D5239 File Offset: 0x000D3439
	private static string ConsumeDeepLinkInfo(bool retain)
	{
		return "";
	}

	// Token: 0x060052F0 RID: 21232 RVA: 0x0019DE03 File Offset: 0x0019C003
	private static string GetStartupDeeplink()
	{
		return string.Empty;
	}

	// Token: 0x060052F1 RID: 21233 RVA: 0x0019DE03 File Offset: 0x0019C003
	public static string GetSharedKeychainIdentifier()
	{
		return string.Empty;
	}

	// Token: 0x060052F2 RID: 21234 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public static void ShowNativeAppRatingPopup()
	{
	}

	// Token: 0x040049BE RID: 18878
	private const string CHINESE_CURRENCY_CODE = "CNY";

	// Token: 0x040049BF RID: 18879
	private const string CHINESE_COUNTRY_CODE = "CN";

	// Token: 0x040049C0 RID: 18880
	private const char RECEIPT_DATA_DELIMITER = '|';

	// Token: 0x040049C1 RID: 18881
	private const int LARGE_RECEIPT_CHAR_THRESHOLD = 9788;

	// Token: 0x040049C2 RID: 18882
	private const int MAX_TRIM_DELAY_SECONDS = 10;

	// Token: 0x040049C3 RID: 18883
	public bool m_wasBreakingNewsShown;

	// Token: 0x040049C4 RID: 18884
	private ulong m_nextTrimAvailableTime;

	// Token: 0x040049C5 RID: 18885
	private ulong m_trimDelay = 1UL;

	// Token: 0x040049C6 RID: 18886
	private static bool s_pushRegistrationSet;
}
