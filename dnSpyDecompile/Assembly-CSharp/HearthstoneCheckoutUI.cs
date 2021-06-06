using System;
using Blizzard.Commerce;
using Hearthstone;
using UnityEngine;

// Token: 0x020008DB RID: 2267
public class HearthstoneCheckoutUI : UIBPopup
{
	// Token: 0x1400007F RID: 127
	// (add) Token: 0x06007D8E RID: 32142 RVA: 0x0028BD58 File Offset: 0x00289F58
	// (remove) Token: 0x06007D8F RID: 32143 RVA: 0x0028BD90 File Offset: 0x00289F90
	private event HearthstoneCheckoutUI.OutsideClickListener m_outsideClickEvent;

	// Token: 0x17000739 RID: 1849
	// (get) Token: 0x06007D90 RID: 32144 RVA: 0x0028BDC5 File Offset: 0x00289FC5
	// (set) Token: 0x06007D91 RID: 32145 RVA: 0x0028BDCD File Offset: 0x00289FCD
	public int BrowserWidth { get; private set; }

	// Token: 0x1700073A RID: 1850
	// (get) Token: 0x06007D92 RID: 32146 RVA: 0x0028BDD6 File Offset: 0x00289FD6
	// (set) Token: 0x06007D93 RID: 32147 RVA: 0x0028BDDE File Offset: 0x00289FDE
	public int BrowserHeight { get; private set; }

	// Token: 0x1700073B RID: 1851
	// (get) Token: 0x06007D94 RID: 32148 RVA: 0x0028BDE7 File Offset: 0x00289FE7
	public IScreenSpace ScreenSpace
	{
		get
		{
			return this.m_checkoutMesh;
		}
	}

	// Token: 0x1700073C RID: 1852
	// (get) Token: 0x06007D95 RID: 32149 RVA: 0x0028BDEF File Offset: 0x00289FEF
	public bool HasCheckoutMesh
	{
		get
		{
			return this.m_checkoutMesh != null;
		}
	}

	// Token: 0x06007D96 RID: 32150 RVA: 0x0028BE00 File Offset: 0x0028A000
	protected override void Awake()
	{
		base.Awake();
		this.m_destroyOnSceneLoad = false;
		if (this.m_OffClickCatcher != null)
		{
			this.m_OffClickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnOutsideClick));
		}
		if (this.m_ConsoleButton != null)
		{
			if (!HearthstoneCheckoutUI.ShouldStreamBrowserTexture() && HearthstoneApplication.IsInternal())
			{
				this.m_ConsoleButton.gameObject.SetActive(true);
				this.m_ConsoleButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBugReporterClick));
				return;
			}
			this.m_ConsoleButton.gameObject.SetActive(false);
		}
	}

	// Token: 0x06007D97 RID: 32151 RVA: 0x0028BE99 File Offset: 0x0028A099
	private void Update()
	{
		if (Application.isEditor)
		{
			this.UpdateMeshTransform();
		}
	}

	// Token: 0x06007D98 RID: 32152 RVA: 0x0028BEA8 File Offset: 0x0028A0A8
	public override void Show()
	{
		base.Show();
		if (this.m_checkoutInput != null)
		{
			this.m_checkoutInput.IsActive = true;
		}
	}

	// Token: 0x06007D99 RID: 32153 RVA: 0x0028BECC File Offset: 0x0028A0CC
	public void GenerateMeshes()
	{
		this.DetermineBrowserSize();
		if (HearthstoneCheckoutUI.ShouldStreamBrowserTexture())
		{
			if (this.m_checkoutMesh == null)
			{
				this.m_checkoutMesh = CheckoutMesh.GenerateCheckoutMesh(this.BrowserWidth, this.BrowserHeight, (float)this.m_MeshWidth, (float)this.m_MeshHeight);
				this.m_checkoutMesh.gameObject.AddComponent<PegUIElement>();
				this.m_checkoutMesh.transform.SetParent(base.transform, false);
				this.m_checkoutMesh.gameObject.layer = base.gameObject.layer;
				this.m_checkoutInput = this.m_checkoutMesh.gameObject.AddComponent<CheckoutInputManager>();
				this.m_checkoutInput.AddKeyboardEventListener(KeyCode.Escape, delegate(bool onKeyDown)
				{
					if (!onKeyDown)
					{
						this.OnOutsideClick(null);
					}
				});
				this.UpdateMeshTransform();
			}
			else
			{
				this.m_checkoutMesh.ResizeTexture(this.BrowserWidth, this.BrowserHeight);
			}
			this.UpdateTextureHandle();
		}
	}

	// Token: 0x06007D9A RID: 32154 RVA: 0x0028BFB8 File Offset: 0x0028A1B8
	public void InitiateCheckout(HearthstoneCheckout checkoutClient)
	{
		if (checkoutClient != null && this.HasCheckoutMesh)
		{
			blz_commerce_vec2d_t obj = new blz_commerce_vec2d_t
			{
				x = this.BrowserWidth,
				y = this.BrowserHeight
			};
			battlenet_commerce.blz_commerce_browser_send_event(checkoutClient.Sdk, blz_commerce_browser_event_type_t.RESIZE_WINDOW, blz_commerce_vec2d_t.getCPtr(obj).Handle);
			if (this.m_checkoutInput != null)
			{
				this.m_checkoutInput.Setup(checkoutClient, this.m_checkoutMesh);
			}
		}
	}

	// Token: 0x06007D9B RID: 32155 RVA: 0x0028C029 File Offset: 0x0028A229
	public blz_commerce_browser_params_t SetUIParams(blz_commerce_browser_params_t parms)
	{
		parms.window_height = this.BrowserHeight;
		parms.window_width = this.BrowserWidth;
		return parms;
	}

	// Token: 0x06007D9C RID: 32156 RVA: 0x0028C044 File Offset: 0x0028A244
	public void ResizeTexture(int width, int height)
	{
		if (this.m_checkoutMesh != null)
		{
			this.m_checkoutMesh.ResizeTexture(width, height);
			this.UpdateTextureHandle();
		}
	}

	// Token: 0x06007D9D RID: 32157 RVA: 0x0028C067 File Offset: 0x0028A267
	public void UpdateTexture(byte[] buffer)
	{
		if (this.m_checkoutMesh != null)
		{
			this.m_checkoutMesh.UpdateTexture(buffer);
		}
	}

	// Token: 0x06007D9E RID: 32158 RVA: 0x0028C084 File Offset: 0x0028A284
	public void DetermineBrowserSize()
	{
		if (!HearthstoneCheckoutUI.ShouldStreamBrowserTexture())
		{
			this.BrowserWidth = (int)((float)Screen.height * this.m_BrowserResolutionScale);
			this.BrowserHeight = (int)((float)Screen.width * this.m_BrowserResolutionScale);
		}
		else
		{
			if ((float)Screen.height > 1080f)
			{
				this.m_BrowserMeshScale = Mathf.Max(864f / (float)Screen.height, this.m_MeshScaleMinBound);
				float num = 864f / this.m_BrowserMeshScale;
				this.BrowserWidth = (int)(num * 1.5f * this.m_BrowserResolutionScale * this.m_BrowserMeshScale);
				this.BrowserHeight = (int)(num * this.m_BrowserResolutionScale * this.m_BrowserMeshScale);
			}
			else
			{
				this.m_BrowserMeshScale = 0.8f;
				this.BrowserWidth = (int)((float)Screen.height * 1.5f * this.m_BrowserResolutionScale * this.m_BrowserMeshScale);
				this.BrowserHeight = (int)((float)Screen.height * this.m_BrowserResolutionScale * this.m_BrowserMeshScale);
			}
			this.UpdateMeshTransform();
		}
		Log.Store.PrintDebug(string.Concat(new object[]
		{
			"[DetermineBrowserSize] Height: ",
			this.BrowserHeight,
			" Width: ",
			this.BrowserWidth
		}), Array.Empty<object>());
	}

	// Token: 0x06007D9F RID: 32159 RVA: 0x0028C1C3 File Offset: 0x0028A3C3
	public void AddOutsideClickListener(HearthstoneCheckoutUI.OutsideClickListener listener)
	{
		this.m_outsideClickEvent -= listener;
		this.m_outsideClickEvent += listener;
	}

	// Token: 0x06007DA0 RID: 32160 RVA: 0x0028C1D3 File Offset: 0x0028A3D3
	public void RemoveOutsideClickListener(HearthstoneCheckoutUI.OutsideClickListener listener)
	{
		this.m_outsideClickEvent -= listener;
	}

	// Token: 0x06007DA1 RID: 32161 RVA: 0x0028C1DC File Offset: 0x0028A3DC
	public void OnReady()
	{
		if (this.m_checkoutInput != null)
		{
			this.m_checkoutInput.IsActive = this.IsShown();
		}
	}

	// Token: 0x06007DA2 RID: 32162 RVA: 0x0028C200 File Offset: 0x0028A400
	private void UpdateMeshTransform()
	{
		if (this.m_checkoutMesh != null)
		{
			Transform transform = this.m_checkoutMesh.transform;
			transform.localPosition = this.m_BrowserMeshPosition * this.m_BrowserMeshScale;
			transform.localRotation = Quaternion.Euler(this.m_BrowserMeshRotation);
			transform.localScale = new Vector3(this.m_BrowserMeshScale, this.m_BrowserMeshScale, this.m_BrowserMeshScale);
		}
	}

	// Token: 0x06007DA3 RID: 32163 RVA: 0x0028C26A File Offset: 0x0028A46A
	private void OnOutsideClick(UIEvent e)
	{
		if (this.m_outsideClickEvent != null)
		{
			this.m_outsideClickEvent();
		}
	}

	// Token: 0x06007DA4 RID: 32164 RVA: 0x0028C280 File Offset: 0x0028A480
	private void OnBugReporterClick(UIEvent e)
	{
		CheatMgr cheatMgr = CheatMgr.Get();
		if (cheatMgr != null)
		{
			cheatMgr.ShowConsole();
		}
	}

	// Token: 0x06007DA5 RID: 32165 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void UpdateTextureHandle()
	{
	}

	// Token: 0x06007DA6 RID: 32166 RVA: 0x0028C29C File Offset: 0x0028A49C
	private static bool ShouldStreamBrowserTexture()
	{
		return Application.isEditor || !PlatformSettings.IsMobileRuntimeOS;
	}

	// Token: 0x06007DA7 RID: 32167 RVA: 0x0028C2AF File Offset: 0x0028A4AF
	protected override void Hide(bool animate)
	{
		if (this.m_checkoutInput != null)
		{
			this.m_checkoutInput.IsActive = false;
		}
		base.Hide(animate);
	}

	// Token: 0x040065C4 RID: 26052
	[CustomEditField(Sections = "Checkout Configuration")]
	public int m_MeshWidth = 64;

	// Token: 0x040065C5 RID: 26053
	[CustomEditField(Sections = "Checkout Configuration")]
	public int m_MeshHeight = 48;

	// Token: 0x040065C6 RID: 26054
	[CustomEditField(Sections = "Checkout Configuration")]
	public float m_MeshScaleMinBound = 0.6f;

	// Token: 0x040065C7 RID: 26055
	[CustomEditField(Sections = "Checkout Configuration")]
	public Vector3 m_BrowserMeshPosition = Vector3.zero;

	// Token: 0x040065C8 RID: 26056
	[CustomEditField(Sections = "Checkout Configuration")]
	public Vector3 m_BrowserMeshRotation = Vector3.zero;

	// Token: 0x040065C9 RID: 26057
	[CustomEditField(Sections = "Checkout Configuration")]
	public float m_BrowserMeshScale = 1f;

	// Token: 0x040065CA RID: 26058
	[CustomEditField(Sections = "Checkout Configuration")]
	public float m_BrowserResolutionScale = 1f;

	// Token: 0x040065CB RID: 26059
	[CustomEditField(Sections = "UI")]
	public PegUIElement m_OffClickCatcher;

	// Token: 0x040065CC RID: 26060
	[CustomEditField(Sections = "UI")]
	public PegUIElement m_ConsoleButton;

	// Token: 0x040065CD RID: 26061
	private CheckoutMesh m_checkoutMesh;

	// Token: 0x040065CE RID: 26062
	private CheckoutInputManager m_checkoutInput;

	// Token: 0x0200256D RID: 9581
	public enum TransactionUIFlowConfig
	{
		// Token: 0x0400ED75 RID: 60789
		ShowAll,
		// Token: 0x0400ED76 RID: 60790
		HideAllTransactionProcessing,
		// Token: 0x0400ED77 RID: 60791
		HideTransactionCompletion
	}

	// Token: 0x0200256E RID: 9582
	// (Invoke) Token: 0x06013309 RID: 78601
	public delegate void OutsideClickListener();
}
