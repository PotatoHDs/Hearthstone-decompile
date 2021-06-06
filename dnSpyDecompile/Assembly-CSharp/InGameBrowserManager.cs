using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

// Token: 0x02000369 RID: 873
public class InGameBrowserManager : IService, IHasUpdate
{
	// Token: 0x06003345 RID: 13125 RVA: 0x001076B5 File Offset: 0x001058B5
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	// Token: 0x06003346 RID: 13126 RVA: 0x00090064 File Offset: 0x0008E264
	public Type[] GetDependencies()
	{
		return null;
	}

	// Token: 0x06003347 RID: 13127 RVA: 0x001076BD File Offset: 0x001058BD
	public void Shutdown()
	{
		this.Hide();
	}

	// Token: 0x06003348 RID: 13128 RVA: 0x001076C5 File Offset: 0x001058C5
	public static InGameBrowserManager Get()
	{
		return HearthstoneServices.Get<InGameBrowserManager>();
	}

	// Token: 0x06003349 RID: 13129 RVA: 0x001076CC File Offset: 0x001058CC
	public void Show()
	{
		this.Show(null);
	}

	// Token: 0x0600334A RID: 13130 RVA: 0x001076D8 File Offset: 0x001058D8
	public void Show(InGameBrowserManager.BrowserClosedHandler browserClosedHandler)
	{
		if (this.m_inGameBrowserCanvas == null && !string.IsNullOrEmpty(this.m_url))
		{
			BnetBar.Get().HideGameMenu();
			this.m_isShown = true;
			this.m_browserClosedHandler = browserClosedHandler;
			this.m_wasBnetBarActive = BnetBar.Get().IsActive();
			BnetBar.Get().ToggleActive(false);
			this.m_inGameBrowserCanvas = (GameObject)GameUtils.InstantiateGameObject("InGameBrowserCanvas.prefab:3619a4ca9d3064a3790856f0726f7029", null, false);
			InGameBrowserCanvas component = this.m_inGameBrowserCanvas.GetComponent<InGameBrowserCanvas>();
			component.ShowDarkenInputBlocker();
			Camera uicamera = OverlayUI.Get().m_UICamera;
			Vector3 vector = uicamera.WorldToScreenPoint(component.m_topLeftBone.transform.position);
			Vector3 vector2 = uicamera.WorldToScreenPoint(component.m_bottomRightBone.transform.position);
			float x = vector.x;
			float y = (float)uicamera.pixelHeight - vector.y;
			float width = vector2.x - vector.x;
			float height = vector.y - vector2.y;
			this.m_nativeBrowser = new WebAuth(this.m_url, x, y, width, height, this.m_inGameBrowserCanvas.gameObject.name, true);
			this.m_nativeBrowser.Load();
		}
	}

	// Token: 0x0600334B RID: 13131 RVA: 0x00107804 File Offset: 0x00105A04
	public void Hide()
	{
		if (!this.m_isShown)
		{
			return;
		}
		if (this.m_nativeBrowser != null)
		{
			this.m_nativeBrowser.Close();
			this.m_nativeBrowser = null;
		}
		if (this.m_inGameBrowserCanvas != null)
		{
			UnityEngine.Object.Destroy(this.m_inGameBrowserCanvas);
			this.m_inGameBrowserCanvas = null;
		}
		if (this.m_wasBnetBarActive && BnetBar.Get() != null)
		{
			BnetBar.Get().ToggleActive(true);
		}
		if (this.m_browserClosedHandler != null)
		{
			this.m_browserClosedHandler();
		}
		this.m_isShown = false;
	}

	// Token: 0x0600334C RID: 13132 RVA: 0x0010788E File Offset: 0x00105A8E
	public void SetUrl(string url)
	{
		this.m_url = url;
	}

	// Token: 0x0600334D RID: 13133 RVA: 0x00107897 File Offset: 0x00105A97
	public void HideAllButtons()
	{
		if (this.m_inGameBrowserCanvas != null)
		{
			this.m_inGameBrowserCanvas.GetComponent<InGameBrowserCanvas>().HideAllButtons();
		}
	}

	// Token: 0x0600334E RID: 13134 RVA: 0x001078B8 File Offset: 0x00105AB8
	public void Update()
	{
		if (this.m_nativeBrowser == null)
		{
			return;
		}
		switch (this.m_nativeBrowser.GetStatus())
		{
		case WebAuth.Status.ReadyToDisplay:
			if (this.m_inGameBrowserCanvas != null)
			{
				if (!this.m_inGameBrowserCanvas.activeSelf)
				{
					this.m_inGameBrowserCanvas.SetActive(true);
				}
				this.m_inGameBrowserCanvas.GetComponent<InGameBrowserCanvas>().HideLoading();
			}
			if (!this.m_nativeBrowser.IsShown())
			{
				this.m_nativeBrowser.Show();
				return;
			}
			break;
		case WebAuth.Status.Processing:
			break;
		case WebAuth.Status.Success:
			Log.InGameBrowser.Print("WebAuth.Status.Success", Array.Empty<object>());
			if (this.m_inGameBrowserCanvas != null)
			{
				this.m_inGameBrowserCanvas.GetComponent<InGameBrowserCanvas>().ShowLoading();
				return;
			}
			break;
		case WebAuth.Status.Error:
			Log.InGameBrowser.PrintError("WebAuth.Status.Error", Array.Empty<object>());
			break;
		default:
			return;
		}
	}

	// Token: 0x04001C24 RID: 7204
	private GameObject m_inGameBrowserCanvas;

	// Token: 0x04001C25 RID: 7205
	private WebAuth m_nativeBrowser;

	// Token: 0x04001C26 RID: 7206
	private string m_url;

	// Token: 0x04001C27 RID: 7207
	private bool m_isShown;

	// Token: 0x04001C28 RID: 7208
	private bool m_wasBnetBarActive;

	// Token: 0x04001C29 RID: 7209
	private InGameBrowserManager.BrowserClosedHandler m_browserClosedHandler;

	// Token: 0x02001710 RID: 5904
	// (Invoke) Token: 0x0600E6DC RID: 59100
	public delegate void BrowserClosedHandler();
}
