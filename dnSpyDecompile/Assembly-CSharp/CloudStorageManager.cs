using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005E4 RID: 1508
public class CloudStorageManager : MonoBehaviour
{
	// Token: 0x0600528E RID: 21134 RVA: 0x001B1663 File Offset: 0x001AF863
	private void Awake()
	{
		CloudStorageManager.s_Instance = this;
	}

	// Token: 0x0600528F RID: 21135 RVA: 0x001B166B File Offset: 0x001AF86B
	private void OnDestroy()
	{
		CloudStorageManager.s_Instance = null;
	}

	// Token: 0x06005290 RID: 21136 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnApplicationPause(bool pauseStatus)
	{
	}

	// Token: 0x06005291 RID: 21137 RVA: 0x001B1673 File Offset: 0x001AF873
	public static CloudStorageManager Get()
	{
		return CloudStorageManager.s_Instance;
	}

	// Token: 0x06005292 RID: 21138 RVA: 0x001B167A File Offset: 0x001AF87A
	public static bool ShouldDisallowCloudStorage()
	{
		if (Options.Get().GetBool(Option.DISALLOWED_CLOUD_STORAGE))
		{
			Log.CloudStorage.Print("Cloud Storage is Disallowed", Array.Empty<object>());
			return true;
		}
		return false;
	}

	// Token: 0x06005293 RID: 21139 RVA: 0x001B16A1 File Offset: 0x001AF8A1
	public void DisallowCloudStorage()
	{
		Log.CloudStorage.Print("Setting Cloud Storage to Disallowed", Array.Empty<object>());
		Options.Get().SetBool(Option.DISALLOWED_CLOUD_STORAGE, true);
		this.m_isShowingThirdPartyPermission = false;
	}

	// Token: 0x06005294 RID: 21140 RVA: 0x001B16CC File Offset: 0x001AF8CC
	public void ShowCloudStorageContext(string contextBody)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_CLOUD_STORAGE_CONTEXT_HEADER");
		popupInfo.m_text = contextBody;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_confirmText = GameStrings.Get("GLOBAL_BUTTON_YES");
		popupInfo.m_cancelText = GameStrings.Get("GLOBAL_BUTTON_NO");
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnCloudStorageContextResponse);
		DialogManager.Get().ShowPopup(popupInfo);
		this.m_isShowingContext = true;
	}

	// Token: 0x06005295 RID: 21141 RVA: 0x001B1750 File Offset: 0x001AF950
	private void OnCloudStorageContextResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			Log.CloudStorage.Print("Cloud Storage prompt permission not granted", Array.Empty<object>());
			this.m_continueInitialize = false;
			this.DisallowCloudStorage();
		}
		else
		{
			Log.CloudStorage.Print("Cloud Storage prompt permission granted", Array.Empty<object>());
			this.m_continueInitialize = true;
		}
		this.m_isShowingContext = false;
	}

	// Token: 0x06005296 RID: 21142 RVA: 0x001B17A6 File Offset: 0x001AF9A6
	public void StartInitialize(CloudStorageManager.OnInitializedFinished onInitializedFinishedHandler, string contextBody)
	{
		base.StartCoroutine(this.Initialize(onInitializedFinishedHandler, contextBody));
	}

	// Token: 0x06005297 RID: 21143 RVA: 0x001B17B8 File Offset: 0x001AF9B8
	public bool SetString(string key, string value)
	{
		if (CloudStorageManager.ShouldDisallowCloudStorage())
		{
			return false;
		}
		if (!this.m_isInitialized)
		{
			Log.CloudStorage.PrintWarning("Cloud Storage is not Initialized!", Array.Empty<object>());
			return false;
		}
		Log.CloudStorage.Print(string.Concat(new string[]
		{
			"Set string \"",
			(value == null) ? "null" : value,
			"\" for key \"",
			(key == null) ? "null" : key,
			"\""
		}), Array.Empty<object>());
		CloudStorageManager.CloudSetString(key, value);
		return true;
	}

	// Token: 0x06005298 RID: 21144 RVA: 0x001B1844 File Offset: 0x001AFA44
	public string GetString(string key)
	{
		if (CloudStorageManager.ShouldDisallowCloudStorage())
		{
			return null;
		}
		if (!this.m_isInitialized)
		{
			Log.CloudStorage.PrintWarning("Cloud Storage is not Initialized!", Array.Empty<object>());
			return null;
		}
		string text = CloudStorageManager.CloudGetString(key);
		Log.CloudStorage.Print(string.Concat(new string[]
		{
			"Get string \"",
			(text == null) ? "null" : text,
			"\" from key \"",
			(key == null) ? "null" : key,
			"\""
		}), Array.Empty<object>());
		return text;
	}

	// Token: 0x06005299 RID: 21145 RVA: 0x001B18D0 File Offset: 0x001AFAD0
	public void RemoveObject(string key)
	{
		if (CloudStorageManager.ShouldDisallowCloudStorage())
		{
			return;
		}
		if (!this.m_isInitialized)
		{
			Log.CloudStorage.PrintWarning("Cloud Storage is not Initialized!", Array.Empty<object>());
			return;
		}
		Log.CloudStorage.Print("Remove object for key \"" + ((key == null) ? "null" : key) + "\"", Array.Empty<object>());
		CloudStorageManager.CloudRemoveObject(key);
	}

	// Token: 0x0600529A RID: 21146 RVA: 0x001B1931 File Offset: 0x001AFB31
	public bool IsShowingContext()
	{
		return this.m_isShowingContext;
	}

	// Token: 0x0600529B RID: 21147 RVA: 0x001B1939 File Offset: 0x001AFB39
	public bool ContinueInitialize()
	{
		return this.m_continueInitialize;
	}

	// Token: 0x0600529C RID: 21148 RVA: 0x001B1941 File Offset: 0x001AFB41
	public bool GetIsShowingThirdPartyPermission()
	{
		return this.m_isShowingThirdPartyPermission;
	}

	// Token: 0x0600529D RID: 21149 RVA: 0x001B1949 File Offset: 0x001AFB49
	public bool IsConnecting()
	{
		return this.m_isConnecting;
	}

	// Token: 0x0600529E RID: 21150 RVA: 0x001B1951 File Offset: 0x001AFB51
	public bool IsAPIUnavailable()
	{
		return this.m_isAPIUnavailable;
	}

	// Token: 0x0600529F RID: 21151 RVA: 0x001B1959 File Offset: 0x001AFB59
	public bool IsSignInRequired()
	{
		return this.m_isSignInRequired;
	}

	// Token: 0x060052A0 RID: 21152 RVA: 0x001B1961 File Offset: 0x001AFB61
	public void APIUnavailable()
	{
		Log.CloudStorage.Print("API Unavailable", Array.Empty<object>());
		this.m_isConnecting = false;
		this.m_isAPIUnavailable = true;
		this.m_isSignInRequired = false;
	}

	// Token: 0x060052A1 RID: 21153 RVA: 0x001B198C File Offset: 0x001AFB8C
	public void APISignInRequired()
	{
		Log.CloudStorage.Print("API Sign In Required", Array.Empty<object>());
		this.m_isConnecting = false;
		this.m_isAPIUnavailable = false;
		this.m_isSignInRequired = true;
	}

	// Token: 0x060052A2 RID: 21154 RVA: 0x001B19B7 File Offset: 0x001AFBB7
	public void APIConnected()
	{
		Log.CloudStorage.Print("API Connected", Array.Empty<object>());
		this.m_isConnecting = false;
		this.m_isAPIUnavailable = false;
		this.m_isSignInRequired = false;
		this.m_isShowingThirdPartyPermission = false;
	}

	// Token: 0x060052A3 RID: 21155 RVA: 0x001B19E9 File Offset: 0x001AFBE9
	private IEnumerator Initialize(CloudStorageManager.OnInitializedFinished onInitializedFinishedHandler, string contextBody)
	{
		Log.CloudStorage.Print("Initialize", Array.Empty<object>());
		if (this.m_isInitialized)
		{
			Log.CloudStorage.PrintWarning("Cloud Storage is already Initialized!", Array.Empty<object>());
			if (onInitializedFinishedHandler != null)
			{
				onInitializedFinishedHandler();
			}
			yield break;
		}
		if (onInitializedFinishedHandler != null)
		{
			this.m_onInitializedFinishedHandlers.Add(onInitializedFinishedHandler);
		}
		if (this.m_isInitializing)
		{
			Log.CloudStorage.PrintWarning("Cloud Storage is being Initialized!", Array.Empty<object>());
			yield break;
		}
		this.m_isInitializing = true;
		Log.CloudStorage.PrintWarning("Cloud Storage has finished initializing!", Array.Empty<object>());
		this.m_isInitializing = false;
		this.m_isInitialized = true;
		foreach (CloudStorageManager.OnInitializedFinished onInitializedFinished in this.m_onInitializedFinishedHandlers)
		{
			onInitializedFinished();
		}
		this.m_onInitializedFinishedHandlers.Clear();
		yield break;
	}

	// Token: 0x060052A4 RID: 21156 RVA: 0x001B1A00 File Offset: 0x001AFC00
	private static bool IsStorageEnabledOnAndroidPlatform()
	{
		AndroidDeviceSettings androidDeviceSettings = AndroidDeviceSettings.Get();
		if (androidDeviceSettings == null)
		{
			Log.CloudStorage.PrintError("Android device settings unexpectedly null", Array.Empty<object>());
			return false;
		}
		return androidDeviceSettings.GetAndroidStore() != AndroidStore.HUAWEI;
	}

	// Token: 0x060052A5 RID: 21157 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void CloudSetString(string key, string value)
	{
	}

	// Token: 0x060052A6 RID: 21158 RVA: 0x00090064 File Offset: 0x0008E264
	private static string CloudGetString(string key)
	{
		return null;
	}

	// Token: 0x060052A7 RID: 21159 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void CloudRemoveObject(string key)
	{
	}

	// Token: 0x040049A9 RID: 18857
	private static CloudStorageManager s_Instance;

	// Token: 0x040049AA RID: 18858
	private bool m_isInitialized;

	// Token: 0x040049AB RID: 18859
	private bool m_isInitializing;

	// Token: 0x040049AC RID: 18860
	private bool m_isShowingContext;

	// Token: 0x040049AD RID: 18861
	private bool m_continueInitialize;

	// Token: 0x040049AE RID: 18862
	private bool m_isConnecting;

	// Token: 0x040049AF RID: 18863
	private bool m_isAPIUnavailable;

	// Token: 0x040049B0 RID: 18864
	private bool m_isSignInRequired;

	// Token: 0x040049B1 RID: 18865
	private bool m_isShowingThirdPartyPermission;

	// Token: 0x040049B2 RID: 18866
	private List<CloudStorageManager.OnInitializedFinished> m_onInitializedFinishedHandlers = new List<CloudStorageManager.OnInitializedFinished>();

	// Token: 0x02002022 RID: 8226
	// (Invoke) Token: 0x06011C2B RID: 72747
	public delegate void OnInitializedFinished();
}
