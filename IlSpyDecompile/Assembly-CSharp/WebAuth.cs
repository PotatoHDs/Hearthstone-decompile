using UnityEngine;

public class WebAuth
{
	public enum Status
	{
		Closed,
		Loading,
		ReadyToDisplay,
		Processing,
		Success,
		Error,
		MAX
	}

	public enum Error
	{
		InternalError
	}

	private string m_url;

	private Rect m_window;

	private bool m_show;

	private string m_callbackGameObject;

	private bool m_passSelectedTemporaryAccountId;

	public static Status m_SimulateWebLoginWebViewStatus;

	public WebAuth(string url, float x, float y, float width, float height, string gameObjectName, bool passSelectedTemporaryAccountId)
	{
		Log.Login.Print("WebAuthentication.WebAuth() {0} {1} {2} {3} {4} {5}", url, x, y, width, height, gameObjectName);
		m_url = url;
		m_window = new Rect(x, y, width, height);
		m_callbackGameObject = gameObjectName;
		m_passSelectedTemporaryAccountId = passSelectedTemporaryAccountId;
	}

	public virtual void Load()
	{
		Log.Login.Print("WebAuthentication.Load()");
		string temporaryAccountId = (m_passSelectedTemporaryAccountId ? TemporaryAccountManager.Get().GetSelectedTemporaryAccountId() : "");
		LoadWebView(m_url, m_window.x, m_window.y, m_window.width, m_window.height, SystemInfo.deviceUniqueIdentifier, temporaryAccountId, m_callbackGameObject, "MobileCallbackManager");
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
	}

	public virtual void Close()
	{
		Log.Login.Print("WebAuthentication.Close()");
		SplashScreen splashScreen = SplashScreen.Get();
		if (splashScreen != null)
		{
			splashScreen.HideWebLoginCanvas();
		}
		CloseWebView();
		FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
		if (Cheats.SimulateWebPaneLogin)
		{
			m_SimulateWebLoginWebViewStatus = Status.Closed;
		}
	}

	public virtual Status GetStatus()
	{
		int webViewStatus = GetWebViewStatus();
		if (webViewStatus < 0 || webViewStatus >= 6)
		{
			return Status.Error;
		}
		return (Status)webViewStatus;
	}

	public Error GetError()
	{
		return Error.InternalError;
	}

	public virtual string GetLoginCode()
	{
		string text = GetWebViewLoginCode();
		if (text == null)
		{
			text = "";
		}
		int num = text.IndexOf("#");
		if (num > 0)
		{
			Log.Login.PrintDebug("Trimming invalid token characters from #: originalToken={0}", text);
			text = text.Substring(0, num);
		}
		num = text.IndexOf("&");
		if (num > 0)
		{
			Log.Login.PrintDebug("Trimming invalid token characters from &: originalToken={0}", text);
			text = text.Substring(0, num);
		}
		Log.Login.PrintDebug("WebAuthentication.GetLoginCode() webLoginCode={0}", text);
		return text;
	}

	public void Show()
	{
		if (!m_show)
		{
			Log.Login.Print("WebAuthentication.Show()");
			m_show = true;
			ShowWebView(show: true);
		}
	}

	public bool IsShown()
	{
		return m_show;
	}

	public void Hide()
	{
		if (m_show)
		{
			m_show = false;
			ShowWebView(show: false);
		}
	}

	public static void ClearLoginData()
	{
		DeleteStoredToken();
		DeleteCookies();
		ClearBrowserCache();
	}

	public static void DeleteCookies()
	{
		ClearWebViewCookies();
	}

	public static void ClearBrowserCache()
	{
		ClearURLCache();
	}

	public static string GetStoredToken()
	{
		return GetStoredLoginToken();
	}

	public static bool SetStoredToken(string str)
	{
		return SetStoredLoginToken(str);
	}

	public static void DeleteStoredToken()
	{
		DeleteStoredLoginToken();
	}

	public static void UpdateBreakingNews(string title, string body, string gameObjectName)
	{
		SetBreakingNews(title, body, gameObjectName);
	}

	public static void UpdateRegionSelectVisualState(bool isVisible)
	{
		SetRegionSelectVisualState(isVisible);
	}

	public static void GoBackWebPage()
	{
		GoBack();
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		Close();
	}

	private static void LoadWebView(string str, float x, float y, float width, float height, string deviceUniqueIdentifier, string temporaryAccountId, string gameObjectName, string callbackManagerObjectName)
	{
	}

	private static void CloseWebView()
	{
	}

	private static int GetWebViewStatus()
	{
		if (!Cheats.SimulateWebPaneLogin)
		{
			return 0;
		}
		return (int)m_SimulateWebLoginWebViewStatus;
	}

	private static void ShowWebView(bool show)
	{
	}

	private static void ClearWebViewCookies()
	{
	}

	private static void ClearURLCache()
	{
	}

	private static string GetWebViewLoginCode()
	{
		return "";
	}

	private static string GetStoredLoginToken()
	{
		return "";
	}

	private static bool SetStoredLoginToken(string str)
	{
		return true;
	}

	private static void DeleteStoredLoginToken()
	{
	}

	private static void SetBreakingNews(string localized_title, string body, string gameObjectName)
	{
	}

	private static void SetRegionSelectVisualState(bool isVisible)
	{
	}

	private static void GoBack()
	{
	}

	private static void SetWebViewCountryCodeCookie(string countryCode, string domain)
	{
	}
}
