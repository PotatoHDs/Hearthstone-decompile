using System;
using UnityEngine;

// Token: 0x0200093D RID: 2365
public class WebAuth
{
	// Token: 0x060082C7 RID: 33479 RVA: 0x002A7728 File Offset: 0x002A5928
	public WebAuth(string url, float x, float y, float width, float height, string gameObjectName, bool passSelectedTemporaryAccountId)
	{
		Log.Login.Print("WebAuthentication.WebAuth() {0} {1} {2} {3} {4} {5}", new object[]
		{
			url,
			x,
			y,
			width,
			height,
			gameObjectName
		});
		this.m_url = url;
		this.m_window = new Rect(x, y, width, height);
		this.m_callbackGameObject = gameObjectName;
		this.m_passSelectedTemporaryAccountId = passSelectedTemporaryAccountId;
	}

	// Token: 0x060082C8 RID: 33480 RVA: 0x002A77A8 File Offset: 0x002A59A8
	public virtual void Load()
	{
		Log.Login.Print("WebAuthentication.Load()", Array.Empty<object>());
		string temporaryAccountId = this.m_passSelectedTemporaryAccountId ? TemporaryAccountManager.Get().GetSelectedTemporaryAccountId() : "";
		WebAuth.LoadWebView(this.m_url, this.m_window.x, this.m_window.y, this.m_window.width, this.m_window.height, SystemInfo.deviceUniqueIdentifier, temporaryAccountId, this.m_callbackGameObject, "MobileCallbackManager");
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
	}

	// Token: 0x060082C9 RID: 33481 RVA: 0x002A7844 File Offset: 0x002A5A44
	public virtual void Close()
	{
		Log.Login.Print("WebAuthentication.Close()", Array.Empty<object>());
		SplashScreen splashScreen = SplashScreen.Get();
		if (splashScreen != null)
		{
			splashScreen.HideWebLoginCanvas();
		}
		WebAuth.CloseWebView();
		FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		if (Cheats.SimulateWebPaneLogin)
		{
			WebAuth.m_SimulateWebLoginWebViewStatus = WebAuth.Status.Closed;
		}
	}

	// Token: 0x060082CA RID: 33482 RVA: 0x002A78A4 File Offset: 0x002A5AA4
	public virtual WebAuth.Status GetStatus()
	{
		int webViewStatus = WebAuth.GetWebViewStatus();
		if (webViewStatus < 0 || webViewStatus >= 6)
		{
			return WebAuth.Status.Error;
		}
		return (WebAuth.Status)webViewStatus;
	}

	// Token: 0x060082CB RID: 33483 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public WebAuth.Error GetError()
	{
		return WebAuth.Error.InternalError;
	}

	// Token: 0x060082CC RID: 33484 RVA: 0x002A78C4 File Offset: 0x002A5AC4
	public virtual string GetLoginCode()
	{
		string text = WebAuth.GetWebViewLoginCode();
		if (text == null)
		{
			text = "";
		}
		int num = text.IndexOf("#");
		if (num > 0)
		{
			Log.Login.PrintDebug("Trimming invalid token characters from #: originalToken={0}", new object[]
			{
				text
			});
			text = text.Substring(0, num);
		}
		num = text.IndexOf("&");
		if (num > 0)
		{
			Log.Login.PrintDebug("Trimming invalid token characters from &: originalToken={0}", new object[]
			{
				text
			});
			text = text.Substring(0, num);
		}
		Log.Login.PrintDebug("WebAuthentication.GetLoginCode() webLoginCode={0}", new object[]
		{
			text
		});
		return text;
	}

	// Token: 0x060082CD RID: 33485 RVA: 0x002A795E File Offset: 0x002A5B5E
	public void Show()
	{
		if (this.m_show)
		{
			return;
		}
		Log.Login.Print("WebAuthentication.Show()", Array.Empty<object>());
		this.m_show = true;
		WebAuth.ShowWebView(true);
	}

	// Token: 0x060082CE RID: 33486 RVA: 0x002A798A File Offset: 0x002A5B8A
	public bool IsShown()
	{
		return this.m_show;
	}

	// Token: 0x060082CF RID: 33487 RVA: 0x002A7992 File Offset: 0x002A5B92
	public void Hide()
	{
		if (!this.m_show)
		{
			return;
		}
		this.m_show = false;
		WebAuth.ShowWebView(false);
	}

	// Token: 0x060082D0 RID: 33488 RVA: 0x002A79AA File Offset: 0x002A5BAA
	public static void ClearLoginData()
	{
		WebAuth.DeleteStoredToken();
		WebAuth.DeleteCookies();
		WebAuth.ClearBrowserCache();
	}

	// Token: 0x060082D1 RID: 33489 RVA: 0x002A79BB File Offset: 0x002A5BBB
	public static void DeleteCookies()
	{
		WebAuth.ClearWebViewCookies();
	}

	// Token: 0x060082D2 RID: 33490 RVA: 0x002A79C2 File Offset: 0x002A5BC2
	public static void ClearBrowserCache()
	{
		WebAuth.ClearURLCache();
	}

	// Token: 0x060082D3 RID: 33491 RVA: 0x002A79C9 File Offset: 0x002A5BC9
	public static string GetStoredToken()
	{
		return WebAuth.GetStoredLoginToken();
	}

	// Token: 0x060082D4 RID: 33492 RVA: 0x002A79D0 File Offset: 0x002A5BD0
	public static bool SetStoredToken(string str)
	{
		return WebAuth.SetStoredLoginToken(str);
	}

	// Token: 0x060082D5 RID: 33493 RVA: 0x002A79D8 File Offset: 0x002A5BD8
	public static void DeleteStoredToken()
	{
		WebAuth.DeleteStoredLoginToken();
	}

	// Token: 0x060082D6 RID: 33494 RVA: 0x002A79DF File Offset: 0x002A5BDF
	public static void UpdateBreakingNews(string title, string body, string gameObjectName)
	{
		WebAuth.SetBreakingNews(title, body, gameObjectName);
	}

	// Token: 0x060082D7 RID: 33495 RVA: 0x002A79E9 File Offset: 0x002A5BE9
	public static void UpdateRegionSelectVisualState(bool isVisible)
	{
		WebAuth.SetRegionSelectVisualState(isVisible);
	}

	// Token: 0x060082D8 RID: 33496 RVA: 0x002A79F1 File Offset: 0x002A5BF1
	public static void GoBackWebPage()
	{
		WebAuth.GoBack();
	}

	// Token: 0x060082D9 RID: 33497 RVA: 0x002A79F8 File Offset: 0x002A5BF8
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		this.Close();
	}

	// Token: 0x060082DA RID: 33498 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void LoadWebView(string str, float x, float y, float width, float height, string deviceUniqueIdentifier, string temporaryAccountId, string gameObjectName, string callbackManagerObjectName)
	{
	}

	// Token: 0x060082DB RID: 33499 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void CloseWebView()
	{
	}

	// Token: 0x060082DC RID: 33500 RVA: 0x002A7A00 File Offset: 0x002A5C00
	private static int GetWebViewStatus()
	{
		if (!Cheats.SimulateWebPaneLogin)
		{
			return 0;
		}
		return (int)WebAuth.m_SimulateWebLoginWebViewStatus;
	}

	// Token: 0x060082DD RID: 33501 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void ShowWebView(bool show)
	{
	}

	// Token: 0x060082DE RID: 33502 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void ClearWebViewCookies()
	{
	}

	// Token: 0x060082DF RID: 33503 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void ClearURLCache()
	{
	}

	// Token: 0x060082E0 RID: 33504 RVA: 0x000D5239 File Offset: 0x000D3439
	private static string GetWebViewLoginCode()
	{
		return "";
	}

	// Token: 0x060082E1 RID: 33505 RVA: 0x000D5239 File Offset: 0x000D3439
	private static string GetStoredLoginToken()
	{
		return "";
	}

	// Token: 0x060082E2 RID: 33506 RVA: 0x000052EC File Offset: 0x000034EC
	private static bool SetStoredLoginToken(string str)
	{
		return true;
	}

	// Token: 0x060082E3 RID: 33507 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void DeleteStoredLoginToken()
	{
	}

	// Token: 0x060082E4 RID: 33508 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void SetBreakingNews(string localized_title, string body, string gameObjectName)
	{
	}

	// Token: 0x060082E5 RID: 33509 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void SetRegionSelectVisualState(bool isVisible)
	{
	}

	// Token: 0x060082E6 RID: 33510 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void GoBack()
	{
	}

	// Token: 0x060082E7 RID: 33511 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void SetWebViewCountryCodeCookie(string countryCode, string domain)
	{
	}

	// Token: 0x04006D8B RID: 28043
	private string m_url;

	// Token: 0x04006D8C RID: 28044
	private Rect m_window;

	// Token: 0x04006D8D RID: 28045
	private bool m_show;

	// Token: 0x04006D8E RID: 28046
	private string m_callbackGameObject;

	// Token: 0x04006D8F RID: 28047
	private bool m_passSelectedTemporaryAccountId;

	// Token: 0x04006D90 RID: 28048
	public static WebAuth.Status m_SimulateWebLoginWebViewStatus;

	// Token: 0x02002608 RID: 9736
	public enum Status
	{
		// Token: 0x0400EF69 RID: 61289
		Closed,
		// Token: 0x0400EF6A RID: 61290
		Loading,
		// Token: 0x0400EF6B RID: 61291
		ReadyToDisplay,
		// Token: 0x0400EF6C RID: 61292
		Processing,
		// Token: 0x0400EF6D RID: 61293
		Success,
		// Token: 0x0400EF6E RID: 61294
		Error,
		// Token: 0x0400EF6F RID: 61295
		MAX
	}

	// Token: 0x02002609 RID: 9737
	public enum Error
	{
		// Token: 0x0400EF71 RID: 61297
		InternalError
	}
}
