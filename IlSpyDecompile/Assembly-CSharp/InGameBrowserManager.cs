using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

public class InGameBrowserManager : IService, IHasUpdate
{
	public delegate void BrowserClosedHandler();

	private GameObject m_inGameBrowserCanvas;

	private WebAuth m_nativeBrowser;

	private string m_url;

	private bool m_isShown;

	private bool m_wasBnetBarActive;

	private BrowserClosedHandler m_browserClosedHandler;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	public Type[] GetDependencies()
	{
		return null;
	}

	public void Shutdown()
	{
		Hide();
	}

	public static InGameBrowserManager Get()
	{
		return HearthstoneServices.Get<InGameBrowserManager>();
	}

	public void Show()
	{
		Show(null);
	}

	public void Show(BrowserClosedHandler browserClosedHandler)
	{
		if (m_inGameBrowserCanvas == null && !string.IsNullOrEmpty(m_url))
		{
			BnetBar.Get().HideGameMenu();
			m_isShown = true;
			m_browserClosedHandler = browserClosedHandler;
			m_wasBnetBarActive = BnetBar.Get().IsActive();
			BnetBar.Get().ToggleActive(active: false);
			m_inGameBrowserCanvas = (GameObject)GameUtils.InstantiateGameObject("InGameBrowserCanvas.prefab:3619a4ca9d3064a3790856f0726f7029");
			InGameBrowserCanvas component = m_inGameBrowserCanvas.GetComponent<InGameBrowserCanvas>();
			component.ShowDarkenInputBlocker();
			Camera uICamera = OverlayUI.Get().m_UICamera;
			Vector3 vector = uICamera.WorldToScreenPoint(component.m_topLeftBone.transform.position);
			Vector3 vector2 = uICamera.WorldToScreenPoint(component.m_bottomRightBone.transform.position);
			float x = vector.x;
			float y = (float)uICamera.pixelHeight - vector.y;
			float width = vector2.x - vector.x;
			float height = vector.y - vector2.y;
			m_nativeBrowser = new WebAuth(m_url, x, y, width, height, m_inGameBrowserCanvas.gameObject.name, passSelectedTemporaryAccountId: true);
			m_nativeBrowser.Load();
		}
	}

	public void Hide()
	{
		if (m_isShown)
		{
			if (m_nativeBrowser != null)
			{
				m_nativeBrowser.Close();
				m_nativeBrowser = null;
			}
			if (m_inGameBrowserCanvas != null)
			{
				UnityEngine.Object.Destroy(m_inGameBrowserCanvas);
				m_inGameBrowserCanvas = null;
			}
			if (m_wasBnetBarActive && BnetBar.Get() != null)
			{
				BnetBar.Get().ToggleActive(active: true);
			}
			if (m_browserClosedHandler != null)
			{
				m_browserClosedHandler();
			}
			m_isShown = false;
		}
	}

	public void SetUrl(string url)
	{
		m_url = url;
	}

	public void HideAllButtons()
	{
		if (m_inGameBrowserCanvas != null)
		{
			m_inGameBrowserCanvas.GetComponent<InGameBrowserCanvas>().HideAllButtons();
		}
	}

	public void Update()
	{
		if (m_nativeBrowser == null)
		{
			return;
		}
		switch (m_nativeBrowser.GetStatus())
		{
		case WebAuth.Status.ReadyToDisplay:
			if (m_inGameBrowserCanvas != null)
			{
				if (!m_inGameBrowserCanvas.activeSelf)
				{
					m_inGameBrowserCanvas.SetActive(value: true);
				}
				m_inGameBrowserCanvas.GetComponent<InGameBrowserCanvas>().HideLoading();
			}
			if (!m_nativeBrowser.IsShown())
			{
				m_nativeBrowser.Show();
			}
			break;
		case WebAuth.Status.Success:
			Log.InGameBrowser.Print("WebAuth.Status.Success");
			if (m_inGameBrowserCanvas != null)
			{
				m_inGameBrowserCanvas.GetComponent<InGameBrowserCanvas>().ShowLoading();
			}
			break;
		case WebAuth.Status.Error:
			Log.InGameBrowser.PrintError("WebAuth.Status.Error");
			break;
		case WebAuth.Status.Processing:
			break;
		}
	}
}
