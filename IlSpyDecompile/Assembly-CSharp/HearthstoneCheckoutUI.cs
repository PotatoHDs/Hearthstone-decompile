using Blizzard.Commerce;
using Hearthstone;
using UnityEngine;

public class HearthstoneCheckoutUI : UIBPopup
{
	public enum TransactionUIFlowConfig
	{
		ShowAll,
		HideAllTransactionProcessing,
		HideTransactionCompletion
	}

	public delegate void OutsideClickListener();

	[CustomEditField(Sections = "Checkout Configuration")]
	public int m_MeshWidth = 64;

	[CustomEditField(Sections = "Checkout Configuration")]
	public int m_MeshHeight = 48;

	[CustomEditField(Sections = "Checkout Configuration")]
	public float m_MeshScaleMinBound = 0.6f;

	[CustomEditField(Sections = "Checkout Configuration")]
	public Vector3 m_BrowserMeshPosition = Vector3.zero;

	[CustomEditField(Sections = "Checkout Configuration")]
	public Vector3 m_BrowserMeshRotation = Vector3.zero;

	[CustomEditField(Sections = "Checkout Configuration")]
	public float m_BrowserMeshScale = 1f;

	[CustomEditField(Sections = "Checkout Configuration")]
	public float m_BrowserResolutionScale = 1f;

	[CustomEditField(Sections = "UI")]
	public PegUIElement m_OffClickCatcher;

	[CustomEditField(Sections = "UI")]
	public PegUIElement m_ConsoleButton;

	private CheckoutMesh m_checkoutMesh;

	private CheckoutInputManager m_checkoutInput;

	public int BrowserWidth { get; private set; }

	public int BrowserHeight { get; private set; }

	public IScreenSpace ScreenSpace => m_checkoutMesh;

	public bool HasCheckoutMesh => m_checkoutMesh != null;

	private event OutsideClickListener m_outsideClickEvent;

	protected override void Awake()
	{
		base.Awake();
		m_destroyOnSceneLoad = false;
		if (m_OffClickCatcher != null)
		{
			m_OffClickCatcher.AddEventListener(UIEventType.RELEASE, OnOutsideClick);
		}
		if (m_ConsoleButton != null)
		{
			if (!ShouldStreamBrowserTexture() && HearthstoneApplication.IsInternal())
			{
				m_ConsoleButton.gameObject.SetActive(value: true);
				m_ConsoleButton.AddEventListener(UIEventType.RELEASE, OnBugReporterClick);
			}
			else
			{
				m_ConsoleButton.gameObject.SetActive(value: false);
			}
		}
	}

	private void Update()
	{
		if (Application.isEditor)
		{
			UpdateMeshTransform();
		}
	}

	public override void Show()
	{
		base.Show();
		if (m_checkoutInput != null)
		{
			m_checkoutInput.IsActive = true;
		}
	}

	public void GenerateMeshes()
	{
		DetermineBrowserSize();
		if (!ShouldStreamBrowserTexture())
		{
			return;
		}
		if (m_checkoutMesh == null)
		{
			m_checkoutMesh = CheckoutMesh.GenerateCheckoutMesh(BrowserWidth, BrowserHeight, m_MeshWidth, m_MeshHeight);
			m_checkoutMesh.gameObject.AddComponent<PegUIElement>();
			m_checkoutMesh.transform.SetParent(base.transform, worldPositionStays: false);
			m_checkoutMesh.gameObject.layer = base.gameObject.layer;
			m_checkoutInput = m_checkoutMesh.gameObject.AddComponent<CheckoutInputManager>();
			m_checkoutInput.AddKeyboardEventListener(KeyCode.Escape, delegate(bool onKeyDown)
			{
				if (!onKeyDown)
				{
					OnOutsideClick(null);
				}
			});
			UpdateMeshTransform();
		}
		else
		{
			m_checkoutMesh.ResizeTexture(BrowserWidth, BrowserHeight);
		}
		UpdateTextureHandle();
	}

	public void InitiateCheckout(HearthstoneCheckout checkoutClient)
	{
		if (checkoutClient != null && HasCheckoutMesh)
		{
			blz_commerce_vec2d_t obj = new blz_commerce_vec2d_t
			{
				x = BrowserWidth,
				y = BrowserHeight
			};
			battlenet_commerce.blz_commerce_browser_send_event(checkoutClient.Sdk, blz_commerce_browser_event_type_t.RESIZE_WINDOW, blz_commerce_vec2d_t.getCPtr(obj).Handle);
			if (m_checkoutInput != null)
			{
				m_checkoutInput.Setup(checkoutClient, m_checkoutMesh);
			}
		}
	}

	public blz_commerce_browser_params_t SetUIParams(blz_commerce_browser_params_t parms)
	{
		parms.window_height = BrowserHeight;
		parms.window_width = BrowserWidth;
		return parms;
	}

	public void ResizeTexture(int width, int height)
	{
		if (m_checkoutMesh != null)
		{
			m_checkoutMesh.ResizeTexture(width, height);
			UpdateTextureHandle();
		}
	}

	public void UpdateTexture(byte[] buffer)
	{
		if (m_checkoutMesh != null)
		{
			m_checkoutMesh.UpdateTexture(buffer);
		}
	}

	public void DetermineBrowserSize()
	{
		if (!ShouldStreamBrowserTexture())
		{
			BrowserWidth = (int)((float)Screen.height * m_BrowserResolutionScale);
			BrowserHeight = (int)((float)Screen.width * m_BrowserResolutionScale);
		}
		else
		{
			if ((float)Screen.height > 1080f)
			{
				m_BrowserMeshScale = Mathf.Max(864f / (float)Screen.height, m_MeshScaleMinBound);
				float num = 864f / m_BrowserMeshScale;
				BrowserWidth = (int)(num * 1.5f * m_BrowserResolutionScale * m_BrowserMeshScale);
				BrowserHeight = (int)(num * m_BrowserResolutionScale * m_BrowserMeshScale);
			}
			else
			{
				m_BrowserMeshScale = 0.8f;
				BrowserWidth = (int)((float)Screen.height * 1.5f * m_BrowserResolutionScale * m_BrowserMeshScale);
				BrowserHeight = (int)((float)Screen.height * m_BrowserResolutionScale * m_BrowserMeshScale);
			}
			UpdateMeshTransform();
		}
		Log.Store.PrintDebug("[DetermineBrowserSize] Height: " + BrowserHeight + " Width: " + BrowserWidth);
	}

	public void AddOutsideClickListener(OutsideClickListener listener)
	{
		m_outsideClickEvent -= listener;
		m_outsideClickEvent += listener;
	}

	public void RemoveOutsideClickListener(OutsideClickListener listener)
	{
		m_outsideClickEvent -= listener;
	}

	public void OnReady()
	{
		if (m_checkoutInput != null)
		{
			m_checkoutInput.IsActive = IsShown();
		}
	}

	private void UpdateMeshTransform()
	{
		if (m_checkoutMesh != null)
		{
			Transform obj = m_checkoutMesh.transform;
			obj.localPosition = m_BrowserMeshPosition * m_BrowserMeshScale;
			obj.localRotation = Quaternion.Euler(m_BrowserMeshRotation);
			obj.localScale = new Vector3(m_BrowserMeshScale, m_BrowserMeshScale, m_BrowserMeshScale);
		}
	}

	private void OnOutsideClick(UIEvent e)
	{
		if (this.m_outsideClickEvent != null)
		{
			this.m_outsideClickEvent();
		}
	}

	private void OnBugReporterClick(UIEvent e)
	{
		CheatMgr.Get()?.ShowConsole();
	}

	private void UpdateTextureHandle()
	{
	}

	private static bool ShouldStreamBrowserTexture()
	{
		if (!Application.isEditor)
		{
			return !PlatformSettings.IsMobileRuntimeOS;
		}
		return true;
	}

	protected override void Hide(bool animate)
	{
		if (m_checkoutInput != null)
		{
			m_checkoutInput.IsActive = false;
		}
		base.Hide(animate);
	}
}
