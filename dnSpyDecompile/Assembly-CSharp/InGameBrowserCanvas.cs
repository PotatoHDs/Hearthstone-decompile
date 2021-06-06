using System;
using UnityEngine;

// Token: 0x02000368 RID: 872
public class InGameBrowserCanvas : MonoBehaviour
{
	// Token: 0x06003337 RID: 13111 RVA: 0x00107244 File Offset: 0x00105444
	private void Awake()
	{
		OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
		this.m_closeButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClosePressed));
		this.m_backButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackPressed));
		this.m_backSymbolButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackPressed));
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
	}

	// Token: 0x06003338 RID: 13112 RVA: 0x001072C0 File Offset: 0x001054C0
	private void Update()
	{
		if (this.m_loadingText.gameObject.activeSelf)
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = now - this.m_loadingLastTimeFade;
			bool shouldLoadingFadeIn = this.m_shouldLoadingFadeIn;
			double num;
			if (timeSpan.TotalSeconds >= 1.0)
			{
				this.m_loadingLastTimeFade = now;
				this.m_shouldLoadingFadeIn = !this.m_shouldLoadingFadeIn;
				num = 1.0;
			}
			else
			{
				num = timeSpan.TotalSeconds / 1.0;
			}
			num = (shouldLoadingFadeIn ? num : (1.0 - num));
			float textAlpha = (float)(num * 0.699999988079071 + 0.30000001192092896);
			this.m_loadingText.TextAlpha = textAlpha;
		}
	}

	// Token: 0x06003339 RID: 13113 RVA: 0x00107375 File Offset: 0x00105575
	private void OnDestroy()
	{
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		if (this.m_inputBlockerPegUIElement != null)
		{
			UnityEngine.Object.Destroy(this.m_inputBlockerPegUIElement.gameObject);
			this.m_inputBlockerPegUIElement = null;
		}
	}

	// Token: 0x0600333A RID: 13114 RVA: 0x001073B0 File Offset: 0x001055B0
	public void WebViewDidFinishLoad(string pageState)
	{
		Log.InGameBrowser.Print("Web View Page State: " + pageState, Array.Empty<object>());
		if (pageState == null)
		{
			return;
		}
		string[] array = pageState.Split(new string[]
		{
			"|"
		}, StringSplitOptions.None);
		if (array.Length < 2)
		{
			Log.InGameBrowser.PrintWarning(string.Format("WebViewDidFinishLoad() - Invalid parsed pageState ({0})", pageState), Array.Empty<object>());
			return;
		}
		this.m_canGoBack = array[array.Length - 1].Equals("canGoBack");
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < array.Length - 1; i++)
		{
			string text = array[i];
			text.Equals("STATE_ACCOUNT_CREATION", StringComparison.InvariantCultureIgnoreCase);
			if (text.Equals("STATE_ACCOUNT_CREATED", StringComparison.InvariantCultureIgnoreCase))
			{
				flag = true;
			}
			if (text.Equals("STATE_NO_BACK", StringComparison.InvariantCultureIgnoreCase))
			{
				flag2 = true;
			}
		}
		flag2 = (flag2 || flag);
		if (flag)
		{
			Options.Get().SetBool(Option.CREATED_ACCOUNT, true);
		}
		(UniversalInputManager.UsePhoneUI ? this.m_backSymbolButton.gameObject : this.m_backButton.gameObject).SetActive(!this.m_hideAllButtons && !flag2 && this.m_canGoBack);
	}

	// Token: 0x0600333B RID: 13115 RVA: 0x00004EB5 File Offset: 0x000030B5
	public void WebViewBackButtonPressed(string dummyState)
	{
		Navigation.GoBack();
	}

	// Token: 0x0600333C RID: 13116 RVA: 0x001074C0 File Offset: 0x001056C0
	public void ShowDarkenInputBlocker()
	{
		if (this.m_inputBlockerPegUIElement != null)
		{
			UnityEngine.Object.Destroy(this.m_inputBlockerPegUIElement.gameObject);
			this.m_inputBlockerPegUIElement = null;
		}
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "InGameBrowserCanvasInputBlocker");
		SceneUtils.SetLayer(gameObject, base.gameObject.layer, null);
		this.m_inputBlockerPegUIElement = gameObject.AddComponent<PegUIElement>();
		TransformUtil.SetPosY(this.m_inputBlockerPegUIElement, base.gameObject.transform.position.y - 5f);
		this.DarkenInputBlocker(gameObject, 0.5f);
	}

	// Token: 0x0600333D RID: 13117 RVA: 0x00107565 File Offset: 0x00105765
	public void ShowLoading()
	{
		this.m_loadingText.gameObject.SetActive(true);
	}

	// Token: 0x0600333E RID: 13118 RVA: 0x00107578 File Offset: 0x00105778
	public void HideLoading()
	{
		this.m_loadingText.gameObject.SetActive(false);
	}

	// Token: 0x0600333F RID: 13119 RVA: 0x0010758C File Offset: 0x0010578C
	public void HideAllButtons()
	{
		this.m_hideAllButtons = true;
		this.m_closeButton.gameObject.SetActive(false);
		(UniversalInputManager.UsePhoneUI ? this.m_backSymbolButton.gameObject : this.m_backButton.gameObject).SetActive(false);
	}

	// Token: 0x06003340 RID: 13120 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void OnBackPressed(UIEvent e)
	{
		Navigation.GoBack();
	}

	// Token: 0x06003341 RID: 13121 RVA: 0x001075DB File Offset: 0x001057DB
	private void OnClosePressed(UIEvent e)
	{
		InGameBrowserManager.Get().Hide();
	}

	// Token: 0x06003342 RID: 13122 RVA: 0x001075E7 File Offset: 0x001057E7
	private bool OnNavigateBack()
	{
		if (!this.m_hideAllButtons)
		{
			if (!this.m_canGoBack)
			{
				InGameBrowserManager.Get().Hide();
				return true;
			}
			WebAuth.GoBackWebPage();
		}
		return false;
	}

	// Token: 0x06003343 RID: 13123 RVA: 0x00107610 File Offset: 0x00105810
	private void DarkenInputBlocker(GameObject inputBlockerObject, float alpha)
	{
		inputBlockerObject.AddComponent<MeshRenderer>().SetMaterial(this.m_inputBlockerRenderer.GetComponent<MeshRenderer>().GetMaterial());
		inputBlockerObject.AddComponent<MeshFilter>().SetMesh(this.m_inputBlockerRenderer.GetComponent<MeshFilter>().GetMesh());
		BoxCollider component = inputBlockerObject.GetComponent<BoxCollider>();
		TransformUtil.SetLocalScaleXY(inputBlockerObject, component.size.x, component.size.y);
		component.size = new Vector3(1f, 1f, 0f);
		TransformUtil.SetLocalEulerAngleX(inputBlockerObject, 90f);
		RenderUtils.SetAlpha(inputBlockerObject, alpha);
	}

	// Token: 0x04001C16 RID: 7190
	private const int LOADING_TIME_FADE_SEC = 1;

	// Token: 0x04001C17 RID: 7191
	private const float LOADING_MIN_ALPHA = 0.3f;

	// Token: 0x04001C18 RID: 7192
	public PegUIElement m_closeButton;

	// Token: 0x04001C19 RID: 7193
	public PegUIElement m_backButton;

	// Token: 0x04001C1A RID: 7194
	public PegUIElement m_backSymbolButton;

	// Token: 0x04001C1B RID: 7195
	public UberText m_loadingText;

	// Token: 0x04001C1C RID: 7196
	public GameObject m_topLeftBone;

	// Token: 0x04001C1D RID: 7197
	public GameObject m_bottomRightBone;

	// Token: 0x04001C1E RID: 7198
	public GameObject m_inputBlockerRenderer;

	// Token: 0x04001C1F RID: 7199
	private bool m_hideAllButtons;

	// Token: 0x04001C20 RID: 7200
	private bool m_canGoBack;

	// Token: 0x04001C21 RID: 7201
	private PegUIElement m_inputBlockerPegUIElement;

	// Token: 0x04001C22 RID: 7202
	private DateTime m_loadingLastTimeFade = DateTime.Now;

	// Token: 0x04001C23 RID: 7203
	private bool m_shouldLoadingFadeIn;
}
