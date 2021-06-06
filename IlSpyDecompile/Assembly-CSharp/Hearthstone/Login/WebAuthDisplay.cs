using UnityEngine;

namespace Hearthstone.Login
{
	public class WebAuthDisplay
	{
		protected static WebAuth s_webAuth;

		private static string s_webLoginCanvasPrefab;

		protected string m_webToken;

		protected string m_webAuthUrl;

		private static bool s_webAuthHidden;

		protected bool m_passSelectedTemporaryAccountId;

		private static GameObject WebLoginCanvas { get; set; }

		public void Create(string url)
		{
			m_webAuthUrl = url;
			CreateCanvasAndWebAuth();
		}

		public void SetPassTempAccounts(bool passTempAccounts)
		{
			m_passSelectedTemporaryAccountId = passTempAccounts;
		}

		public void Load()
		{
			s_webAuth.Load();
			RequestBreakingNews();
			SetBreakingNewsSeen(seen: true);
		}

		public string GetWebToken()
		{
			return m_webToken;
		}

		public static void CloseWebAuth()
		{
			if (s_webAuth != null)
			{
				s_webAuth.Close();
				s_webAuth = null;
				s_webAuthHidden = false;
			}
		}

		public static void HideWebLoginCanvas()
		{
			if (WebLoginCanvas != null)
			{
				Object.Destroy(WebLoginCanvas);
				WebLoginCanvas = null;
			}
		}

		public static void HideWebAuth()
		{
			if (s_webAuth != null)
			{
				s_webAuth.Hide();
				s_webAuthHidden = true;
			}
		}

		public static void UnHideWebAuth()
		{
			if (s_webAuth != null && s_webAuth.GetStatus() >= WebAuth.Status.ReadyToDisplay)
			{
				s_webAuth.Show();
				s_webAuthHidden = false;
			}
		}

		protected virtual WebLoginCanvas CreateWebCanvas()
		{
			WebLoginCanvas = (GameObject)GameUtils.InstantiateGameObject(s_webLoginCanvasPrefab);
			return WebLoginCanvas.GetComponent<WebLoginCanvas>();
		}

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

		public virtual bool Update()
		{
			return ProcessWebAuth();
		}

		protected virtual void OnWebAuthFailure()
		{
			TelemetryManager.Client().SendWebLoginError();
			Network.Get().ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_LOGIN_FAILURE");
		}

		protected virtual void CreateCanvasAndWebAuth()
		{
			WebLoginCanvas webLoginCanvas = CreateWebCanvas();
			WebLoginCanvas.SetActive(value: false);
			if (Cheats.SimulateWebPaneLogin)
			{
				WebLoginCanvas.SetActive(value: true);
				webLoginCanvas.WebViewDidFinishLoad("|canGoBack");
			}
			Camera uICamera = OverlayUI.Get().m_UICamera;
			Vector3 vector = uICamera.WorldToScreenPoint(webLoginCanvas.m_topLeftBone.transform.position);
			Vector3 vector2 = uICamera.WorldToScreenPoint(webLoginCanvas.m_bottomRightBone.transform.position);
			float x = vector.x;
			float y = (float)uICamera.pixelHeight - vector.y;
			float width = vector2.x - vector.x;
			float height = vector.y - vector2.y;
			CreateWebAuth(x, y, width, height, WebLoginCanvas.gameObject.name);
		}

		protected virtual void SetBreakingNewsSeen(bool seen)
		{
			MobileCallbackManager.Get().m_wasBreakingNewsShown = seen;
		}

		public static bool IsWebLoginCanvasActive()
		{
			if (WebLoginCanvas != null && WebLoginCanvas.activeSelf)
			{
				return true;
			}
			return false;
		}

		public static void SetCanvasPrefab(string webLoginCanvasPrefab)
		{
			if (!string.IsNullOrEmpty(webLoginCanvasPrefab))
			{
				s_webLoginCanvasPrefab = webLoginCanvasPrefab;
			}
		}

		protected virtual void CreateWebAuth(float x, float y, float width, float height, string gameObjectName)
		{
			s_webAuth = new WebAuth(m_webAuthUrl, x, y, width, height, gameObjectName, m_passSelectedTemporaryAccountId);
		}

		private bool ProcessWebAuth()
		{
			switch (s_webAuth.GetStatus())
			{
			case WebAuth.Status.ReadyToDisplay:
				ActivateCanvas();
				if (!s_webAuthHidden && !s_webAuth.IsShown())
				{
					s_webAuth.Show();
				}
				break;
			case WebAuth.Status.Success:
				m_webToken = s_webAuth.GetLoginCode();
				WebAuth.SetStoredToken(m_webToken);
				HideWebLoginCanvas();
				s_webAuth.Close();
				s_webAuth = null;
				return true;
			case WebAuth.Status.Error:
				OnWebAuthFailure();
				HideWebLoginCanvas();
				s_webAuth.Close();
				s_webAuth = null;
				return true;
			}
			return false;
		}

		protected virtual void ActivateCanvas()
		{
			if (!WebLoginCanvas.activeSelf)
			{
				WebLoginCanvas.SetActive(value: true);
			}
		}
	}
}
