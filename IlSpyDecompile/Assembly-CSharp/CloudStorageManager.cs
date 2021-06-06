using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudStorageManager : MonoBehaviour
{
	public delegate void OnInitializedFinished();

	private static CloudStorageManager s_Instance;

	private bool m_isInitialized;

	private bool m_isInitializing;

	private bool m_isShowingContext;

	private bool m_continueInitialize;

	private bool m_isConnecting;

	private bool m_isAPIUnavailable;

	private bool m_isSignInRequired;

	private bool m_isShowingThirdPartyPermission;

	private List<OnInitializedFinished> m_onInitializedFinishedHandlers = new List<OnInitializedFinished>();

	private void Awake()
	{
		s_Instance = this;
	}

	private void OnDestroy()
	{
		s_Instance = null;
	}

	private void OnApplicationPause(bool pauseStatus)
	{
	}

	public static CloudStorageManager Get()
	{
		return s_Instance;
	}

	public static bool ShouldDisallowCloudStorage()
	{
		if (Options.Get().GetBool(Option.DISALLOWED_CLOUD_STORAGE))
		{
			Log.CloudStorage.Print("Cloud Storage is Disallowed");
			return true;
		}
		return false;
	}

	public void DisallowCloudStorage()
	{
		Log.CloudStorage.Print("Setting Cloud Storage to Disallowed");
		Options.Get().SetBool(Option.DISALLOWED_CLOUD_STORAGE, val: true);
		m_isShowingThirdPartyPermission = false;
	}

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
		popupInfo.m_responseCallback = OnCloudStorageContextResponse;
		DialogManager.Get().ShowPopup(popupInfo);
		m_isShowingContext = true;
	}

	private void OnCloudStorageContextResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			Log.CloudStorage.Print("Cloud Storage prompt permission not granted");
			m_continueInitialize = false;
			DisallowCloudStorage();
		}
		else
		{
			Log.CloudStorage.Print("Cloud Storage prompt permission granted");
			m_continueInitialize = true;
		}
		m_isShowingContext = false;
	}

	public void StartInitialize(OnInitializedFinished onInitializedFinishedHandler, string contextBody)
	{
		StartCoroutine(Initialize(onInitializedFinishedHandler, contextBody));
	}

	public bool SetString(string key, string value)
	{
		if (ShouldDisallowCloudStorage())
		{
			return false;
		}
		if (!m_isInitialized)
		{
			Log.CloudStorage.PrintWarning("Cloud Storage is not Initialized!");
			return false;
		}
		Log.CloudStorage.Print("Set string \"" + ((value == null) ? "null" : value) + "\" for key \"" + ((key == null) ? "null" : key) + "\"");
		CloudSetString(key, value);
		return true;
	}

	public string GetString(string key)
	{
		if (ShouldDisallowCloudStorage())
		{
			return null;
		}
		if (!m_isInitialized)
		{
			Log.CloudStorage.PrintWarning("Cloud Storage is not Initialized!");
			return null;
		}
		string text = CloudGetString(key);
		Log.CloudStorage.Print("Get string \"" + ((text == null) ? "null" : text) + "\" from key \"" + ((key == null) ? "null" : key) + "\"");
		return text;
	}

	public void RemoveObject(string key)
	{
		if (!ShouldDisallowCloudStorage())
		{
			if (!m_isInitialized)
			{
				Log.CloudStorage.PrintWarning("Cloud Storage is not Initialized!");
				return;
			}
			Log.CloudStorage.Print("Remove object for key \"" + ((key == null) ? "null" : key) + "\"");
			CloudRemoveObject(key);
		}
	}

	public bool IsShowingContext()
	{
		return m_isShowingContext;
	}

	public bool ContinueInitialize()
	{
		return m_continueInitialize;
	}

	public bool GetIsShowingThirdPartyPermission()
	{
		return m_isShowingThirdPartyPermission;
	}

	public bool IsConnecting()
	{
		return m_isConnecting;
	}

	public bool IsAPIUnavailable()
	{
		return m_isAPIUnavailable;
	}

	public bool IsSignInRequired()
	{
		return m_isSignInRequired;
	}

	public void APIUnavailable()
	{
		Log.CloudStorage.Print("API Unavailable");
		m_isConnecting = false;
		m_isAPIUnavailable = true;
		m_isSignInRequired = false;
	}

	public void APISignInRequired()
	{
		Log.CloudStorage.Print("API Sign In Required");
		m_isConnecting = false;
		m_isAPIUnavailable = false;
		m_isSignInRequired = true;
	}

	public void APIConnected()
	{
		Log.CloudStorage.Print("API Connected");
		m_isConnecting = false;
		m_isAPIUnavailable = false;
		m_isSignInRequired = false;
		m_isShowingThirdPartyPermission = false;
	}

	private IEnumerator Initialize(OnInitializedFinished onInitializedFinishedHandler, string contextBody)
	{
		Log.CloudStorage.Print("Initialize");
		if (m_isInitialized)
		{
			Log.CloudStorage.PrintWarning("Cloud Storage is already Initialized!");
			onInitializedFinishedHandler?.Invoke();
			yield break;
		}
		if (onInitializedFinishedHandler != null)
		{
			m_onInitializedFinishedHandlers.Add(onInitializedFinishedHandler);
		}
		if (m_isInitializing)
		{
			Log.CloudStorage.PrintWarning("Cloud Storage is being Initialized!");
			yield break;
		}
		m_isInitializing = true;
		Log.CloudStorage.PrintWarning("Cloud Storage has finished initializing!");
		m_isInitializing = false;
		m_isInitialized = true;
		foreach (OnInitializedFinished onInitializedFinishedHandler2 in m_onInitializedFinishedHandlers)
		{
			onInitializedFinishedHandler2();
		}
		m_onInitializedFinishedHandlers.Clear();
	}

	private static bool IsStorageEnabledOnAndroidPlatform()
	{
		AndroidDeviceSettings androidDeviceSettings = AndroidDeviceSettings.Get();
		if (androidDeviceSettings == null)
		{
			Log.CloudStorage.PrintError("Android device settings unexpectedly null");
			return false;
		}
		return androidDeviceSettings.GetAndroidStore() != AndroidStore.HUAWEI;
	}

	private static void CloudSetString(string key, string value)
	{
	}

	private static string CloudGetString(string key)
	{
		return null;
	}

	private static void CloudRemoveObject(string key)
	{
	}
}
