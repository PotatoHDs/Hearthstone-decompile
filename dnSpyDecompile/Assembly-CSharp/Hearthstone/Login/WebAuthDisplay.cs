using System;
using UnityEngine;

namespace Hearthstone.Login
{
	// Token: 0x0200114C RID: 4428
	public class WebAuthDisplay
	{
		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x0600C1F6 RID: 49654 RVA: 0x003ADAF9 File Offset: 0x003ABCF9
		// (set) Token: 0x0600C1F7 RID: 49655 RVA: 0x003ADB00 File Offset: 0x003ABD00
		private static GameObject WebLoginCanvas { get; set; }

		// Token: 0x0600C1F8 RID: 49656 RVA: 0x003ADB08 File Offset: 0x003ABD08
		public void Create(string url)
		{
			this.m_webAuthUrl = url;
			this.CreateCanvasAndWebAuth();
		}

		// Token: 0x0600C1F9 RID: 49657 RVA: 0x003ADB17 File Offset: 0x003ABD17
		public void SetPassTempAccounts(bool passTempAccounts)
		{
			this.m_passSelectedTemporaryAccountId = passTempAccounts;
		}

		// Token: 0x0600C1FA RID: 49658 RVA: 0x003ADB20 File Offset: 0x003ABD20
		public void Load()
		{
			WebAuthDisplay.s_webAuth.Load();
			this.RequestBreakingNews();
			this.SetBreakingNewsSeen(true);
		}

		// Token: 0x0600C1FB RID: 49659 RVA: 0x003ADB39 File Offset: 0x003ABD39
		public string GetWebToken()
		{
			return this.m_webToken;
		}

		// Token: 0x0600C1FC RID: 49660 RVA: 0x003ADB41 File Offset: 0x003ABD41
		public static void CloseWebAuth()
		{
			if (WebAuthDisplay.s_webAuth != null)
			{
				WebAuthDisplay.s_webAuth.Close();
				WebAuthDisplay.s_webAuth = null;
				WebAuthDisplay.s_webAuthHidden = false;
			}
		}

		// Token: 0x0600C1FD RID: 49661 RVA: 0x003ADB60 File Offset: 0x003ABD60
		public static void HideWebLoginCanvas()
		{
			if (WebAuthDisplay.WebLoginCanvas != null)
			{
				UnityEngine.Object.Destroy(WebAuthDisplay.WebLoginCanvas);
				WebAuthDisplay.WebLoginCanvas = null;
			}
		}

		// Token: 0x0600C1FE RID: 49662 RVA: 0x003ADB7F File Offset: 0x003ABD7F
		public static void HideWebAuth()
		{
			if (WebAuthDisplay.s_webAuth != null)
			{
				WebAuthDisplay.s_webAuth.Hide();
				WebAuthDisplay.s_webAuthHidden = true;
			}
		}

		// Token: 0x0600C1FF RID: 49663 RVA: 0x003ADB98 File Offset: 0x003ABD98
		public static void UnHideWebAuth()
		{
			if (WebAuthDisplay.s_webAuth != null && WebAuthDisplay.s_webAuth.GetStatus() >= WebAuth.Status.ReadyToDisplay)
			{
				WebAuthDisplay.s_webAuth.Show();
				WebAuthDisplay.s_webAuthHidden = false;
			}
		}

		// Token: 0x0600C200 RID: 49664 RVA: 0x003ADBBE File Offset: 0x003ABDBE
		protected virtual WebLoginCanvas CreateWebCanvas()
		{
			WebAuthDisplay.WebLoginCanvas = (GameObject)GameUtils.InstantiateGameObject(WebAuthDisplay.s_webLoginCanvasPrefab, null, false);
			return WebAuthDisplay.WebLoginCanvas.GetComponent<WebLoginCanvas>();
		}

		// Token: 0x0600C201 RID: 49665 RVA: 0x003ADBE0 File Offset: 0x003ABDE0
		protected virtual void RequestBreakingNews()
		{
			string breakingNewsLocalized = GameStrings.Get("GLUE_MOBILE_SPLASH_SCREEN_BREAKING_NEWS");
			BreakingNews.FetchBreakingNews(ExternalUrlService.Get().GetBreakingNewsLink(), delegate(string response, bool error)
			{
				if (!error)
				{
					WebAuth.UpdateBreakingNews(breakingNewsLocalized, response, "MobileCallbackManager");
				}
			});
		}

		// Token: 0x0600C202 RID: 49666 RVA: 0x003ADC1E File Offset: 0x003ABE1E
		public virtual bool Update()
		{
			return this.ProcessWebAuth();
		}

		// Token: 0x0600C203 RID: 49667 RVA: 0x003ADC26 File Offset: 0x003ABE26
		protected virtual void OnWebAuthFailure()
		{
			TelemetryManager.Client().SendWebLoginError();
			Network.Get().ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_LOGIN_FAILURE");
		}

		// Token: 0x0600C204 RID: 49668 RVA: 0x003ADC44 File Offset: 0x003ABE44
		protected virtual void CreateCanvasAndWebAuth()
		{
			WebLoginCanvas webLoginCanvas = this.CreateWebCanvas();
			WebAuthDisplay.WebLoginCanvas.SetActive(false);
			if (Cheats.SimulateWebPaneLogin)
			{
				WebAuthDisplay.WebLoginCanvas.SetActive(true);
				webLoginCanvas.WebViewDidFinishLoad("|canGoBack");
			}
			Camera uicamera = OverlayUI.Get().m_UICamera;
			Vector3 vector = uicamera.WorldToScreenPoint(webLoginCanvas.m_topLeftBone.transform.position);
			Vector3 vector2 = uicamera.WorldToScreenPoint(webLoginCanvas.m_bottomRightBone.transform.position);
			float x = vector.x;
			float y = (float)uicamera.pixelHeight - vector.y;
			float width = vector2.x - vector.x;
			float height = vector.y - vector2.y;
			this.CreateWebAuth(x, y, width, height, WebAuthDisplay.WebLoginCanvas.gameObject.name);
		}

		// Token: 0x0600C205 RID: 49669 RVA: 0x003ADD08 File Offset: 0x003ABF08
		protected virtual void SetBreakingNewsSeen(bool seen)
		{
			MobileCallbackManager.Get().m_wasBreakingNewsShown = seen;
		}

		// Token: 0x0600C206 RID: 49670 RVA: 0x003ADD15 File Offset: 0x003ABF15
		public static bool IsWebLoginCanvasActive()
		{
			return WebAuthDisplay.WebLoginCanvas != null && WebAuthDisplay.WebLoginCanvas.activeSelf;
		}

		// Token: 0x0600C207 RID: 49671 RVA: 0x003ADD33 File Offset: 0x003ABF33
		public static void SetCanvasPrefab(string webLoginCanvasPrefab)
		{
			if (!string.IsNullOrEmpty(webLoginCanvasPrefab))
			{
				WebAuthDisplay.s_webLoginCanvasPrefab = webLoginCanvasPrefab;
			}
		}

		// Token: 0x0600C208 RID: 49672 RVA: 0x003ADD43 File Offset: 0x003ABF43
		protected virtual void CreateWebAuth(float x, float y, float width, float height, string gameObjectName)
		{
			WebAuthDisplay.s_webAuth = new WebAuth(this.m_webAuthUrl, x, y, width, height, gameObjectName, this.m_passSelectedTemporaryAccountId);
		}

		// Token: 0x0600C209 RID: 49673 RVA: 0x003ADD64 File Offset: 0x003ABF64
		private bool ProcessWebAuth()
		{
			switch (WebAuthDisplay.s_webAuth.GetStatus())
			{
			case WebAuth.Status.ReadyToDisplay:
				this.ActivateCanvas();
				if (!WebAuthDisplay.s_webAuthHidden && !WebAuthDisplay.s_webAuth.IsShown())
				{
					WebAuthDisplay.s_webAuth.Show();
				}
				break;
			case WebAuth.Status.Success:
				this.m_webToken = WebAuthDisplay.s_webAuth.GetLoginCode();
				WebAuth.SetStoredToken(this.m_webToken);
				WebAuthDisplay.HideWebLoginCanvas();
				WebAuthDisplay.s_webAuth.Close();
				WebAuthDisplay.s_webAuth = null;
				return true;
			case WebAuth.Status.Error:
				this.OnWebAuthFailure();
				WebAuthDisplay.HideWebLoginCanvas();
				WebAuthDisplay.s_webAuth.Close();
				WebAuthDisplay.s_webAuth = null;
				return true;
			}
			return false;
		}

		// Token: 0x0600C20A RID: 49674 RVA: 0x003ADE0C File Offset: 0x003AC00C
		protected virtual void ActivateCanvas()
		{
			if (!WebAuthDisplay.WebLoginCanvas.activeSelf)
			{
				WebAuthDisplay.WebLoginCanvas.SetActive(true);
			}
		}

		// Token: 0x04009C51 RID: 40017
		protected static WebAuth s_webAuth;

		// Token: 0x04009C53 RID: 40019
		private static string s_webLoginCanvasPrefab;

		// Token: 0x04009C54 RID: 40020
		protected string m_webToken;

		// Token: 0x04009C55 RID: 40021
		protected string m_webAuthUrl;

		// Token: 0x04009C56 RID: 40022
		private static bool s_webAuthHidden;

		// Token: 0x04009C57 RID: 40023
		protected bool m_passSelectedTemporaryAccountId;
	}
}
