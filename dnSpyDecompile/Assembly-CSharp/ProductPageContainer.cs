using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020006B1 RID: 1713
public class ProductPageContainer : MonoBehaviour
{
	// Token: 0x06005FA8 RID: 24488 RVA: 0x001F319F File Offset: 0x001F139F
	protected virtual void Awake()
	{
		if (this.m_pageRoot != null)
		{
			this.m_pageRoot.SetActive(false);
			return;
		}
		Log.Store.PrintError("ProductPageContainer missing reference to product page root object. This may prevent pages from opening!", Array.Empty<object>());
	}

	// Token: 0x06005FA9 RID: 24489 RVA: 0x001F31D0 File Offset: 0x001F13D0
	protected virtual void Start()
	{
		this.m_widget = base.GetComponent<Widget>();
		this.m_widget.RegisterEventListener(delegate(string evt)
		{
			if (evt == "CODE_DISMISS")
			{
				UIContext.GetRoot().DismissPopup(this.m_widget.gameObject);
				if (this.m_pageRoot != null)
				{
					this.m_pageRoot.SetActive(false);
				}
			}
		});
		this.m_tempInstances = base.GetComponentsInChildren<WidgetInstance>(true).ToList<WidgetInstance>().FindAll((WidgetInstance a) => a.name.Contains("[temp]"));
		Shop shop = Shop.Get();
		if (shop != null)
		{
			shop.OnOpened += this.HandleShopOpened;
			shop.OnCloseCompleted += this.HandleShopClosed;
		}
	}

	// Token: 0x06005FAA RID: 24490 RVA: 0x001F326C File Offset: 0x001F146C
	protected virtual void OnDestroy()
	{
		Shop shop = Shop.Get();
		if (shop != null)
		{
			shop.OnOpened -= this.HandleShopOpened;
			shop.OnCloseCompleted -= this.HandleShopClosed;
		}
	}

	// Token: 0x06005FAB RID: 24491 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void Update()
	{
	}

	// Token: 0x1400003F RID: 63
	// (add) Token: 0x06005FAC RID: 24492 RVA: 0x001F32AC File Offset: 0x001F14AC
	// (remove) Token: 0x06005FAD RID: 24493 RVA: 0x001F32E4 File Offset: 0x001F14E4
	public event Action OnOpened;

	// Token: 0x14000040 RID: 64
	// (add) Token: 0x06005FAE RID: 24494 RVA: 0x001F331C File Offset: 0x001F151C
	// (remove) Token: 0x06005FAF RID: 24495 RVA: 0x001F3354 File Offset: 0x001F1554
	public event Action OnClosed;

	// Token: 0x14000041 RID: 65
	// (add) Token: 0x06005FB0 RID: 24496 RVA: 0x001F338C File Offset: 0x001F158C
	// (remove) Token: 0x06005FB1 RID: 24497 RVA: 0x001F33C4 File Offset: 0x001F15C4
	public event Action OnProductSet;

	// Token: 0x170005BA RID: 1466
	// (get) Token: 0x06005FB2 RID: 24498 RVA: 0x001F33F9 File Offset: 0x001F15F9
	// (set) Token: 0x06005FB3 RID: 24499 RVA: 0x001F3401 File Offset: 0x001F1601
	public bool IsOpen { get; private set; }

	// Token: 0x06005FB4 RID: 24500 RVA: 0x001F340A File Offset: 0x001F160A
	public void Open()
	{
		this.Open(this.m_product, this.Variant);
	}

	// Token: 0x06005FB5 RID: 24501 RVA: 0x001F3420 File Offset: 0x001F1620
	public void Open(ProductDataModel product, ProductDataModel variant = null)
	{
		if (product == null || product == ProductFactory.CreateEmptyProductDataModel())
		{
			Log.Store.PrintError("ProductPageContainer cannot open null or empty product", Array.Empty<object>());
			return;
		}
		base.gameObject.SetActive(true);
		if (this.IsOpen)
		{
			return;
		}
		if (this.m_pageRoot == null)
		{
			Log.Store.PrintError("ProductPageContainer missing reference to the product page root object", Array.Empty<object>());
			return;
		}
		this.m_pageRoot.SetActive(true);
		this.IsOpen = true;
		this.SetProduct(product, variant);
		base.StartCoroutine(this.OpenProductPageCoroutine());
	}

	// Token: 0x06005FB6 RID: 24502 RVA: 0x001F34B0 File Offset: 0x001F16B0
	public void Close()
	{
		if (!this.IsOpen)
		{
			return;
		}
		if (this.m_currentProductPage != null)
		{
			if (this.m_currentProductPage.IsOpen)
			{
				this.m_currentProductPage.Close();
				return;
			}
			this.m_currentProductPage = null;
		}
		this.IsOpen = false;
		this.StopMusicOverride();
		this.m_widget.TriggerEvent("CLOSED", new Widget.TriggerEventParameters
		{
			NoDownwardPropagation = true,
			IgnorePlaymaker = true
		});
		if (this.OnClosed != null)
		{
			this.OnClosed();
		}
		if (this.UnloadUnusedAssetsOnClose && HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().UnloadUnusedAssets();
		}
	}

	// Token: 0x06005FB7 RID: 24503 RVA: 0x001F3562 File Offset: 0x001F1762
	public ProductPage GetCurrentProductPage()
	{
		return this.m_currentProductPage;
	}

	// Token: 0x06005FB8 RID: 24504 RVA: 0x001F356C File Offset: 0x001F176C
	public void RegisterProductPage(ProductPage page)
	{
		this.m_pages.Add(page);
		page.OnOpened += delegate()
		{
			this.HandleProductPageOpened(page);
		};
		page.OnClosed += delegate()
		{
			this.HandleProductPageClosed(page);
		};
	}

	// Token: 0x06005FB9 RID: 24505 RVA: 0x001F35CC File Offset: 0x001F17CC
	public void SetProduct(ProductDataModel product, ProductDataModel variant = null)
	{
		product = (product ?? ProductFactory.CreateEmptyProductDataModel());
		this.Variant = (variant ?? product);
		if (product != this.m_product)
		{
			this.m_product = product;
			this.BindCurrentProduct();
			if (this.OnProductSet != null)
			{
				this.OnProductSet();
			}
		}
	}

	// Token: 0x170005BB RID: 1467
	// (get) Token: 0x06005FBA RID: 24506 RVA: 0x001F361A File Offset: 0x001F181A
	public ProductDataModel Product
	{
		get
		{
			return this.m_product;
		}
	}

	// Token: 0x170005BC RID: 1468
	// (get) Token: 0x06005FBB RID: 24507 RVA: 0x001F3622 File Offset: 0x001F1822
	// (set) Token: 0x06005FBC RID: 24508 RVA: 0x001F362A File Offset: 0x001F182A
	public ProductDataModel Variant { get; set; }

	// Token: 0x06005FBD RID: 24509 RVA: 0x001F3633 File Offset: 0x001F1833
	public void OverrideMusic(MusicPlaylistType playList)
	{
		if (this.m_musicPlaylistBookmark == null)
		{
			this.m_musicPlaylistBookmark = MusicManager.Get().CreateBookmarkOfCurrentPlaylist();
		}
		MusicManager.Get().StartPlaylist(playList);
	}

	// Token: 0x06005FBE RID: 24510 RVA: 0x001F365C File Offset: 0x001F185C
	public void StopMusicOverride()
	{
		if (this.m_musicPlaylistBookmark != null)
		{
			MusicManager musicManager = MusicManager.Get();
			if (musicManager != null)
			{
				musicManager.StopPlaylist();
				musicManager.PlayFromBookmark(this.m_musicPlaylistBookmark);
			}
			this.m_musicPlaylistBookmark = null;
		}
	}

	// Token: 0x06005FBF RID: 24511 RVA: 0x001F3698 File Offset: 0x001F1898
	public void InitializeTempInstances()
	{
		if (this.m_tempInstancesHaveBeenInitialized)
		{
			return;
		}
		this.m_tempInstancesHaveBeenInitialized = true;
		foreach (WidgetInstance instance in this.m_tempInstances)
		{
			this.ForceInitializeTempInstance(instance);
		}
	}

	// Token: 0x06005FC0 RID: 24512 RVA: 0x001F36FC File Offset: 0x001F18FC
	protected void BindCurrentProduct()
	{
		this.m_widget.BindDataModel(this.m_product, false);
	}

	// Token: 0x06005FC1 RID: 24513 RVA: 0x001F3710 File Offset: 0x001F1910
	protected void HandleProductPageOpened(ProductPage page)
	{
		if (this.m_currentProductPage != null)
		{
			Log.Store.PrintError("Previous product page did not close properly: {0}", new object[]
			{
				this.m_currentProductPage.gameObject.name
			});
		}
		this.m_currentProductPage = page;
	}

	// Token: 0x06005FC2 RID: 24514 RVA: 0x001F3750 File Offset: 0x001F1950
	protected void HandleProductPageClosed(ProductPage page)
	{
		if (this.m_currentProductPage == page)
		{
			this.m_currentProductPage = null;
			this.Close();
			return;
		}
		Log.Store.PrintError("Product page closed but it is not the currently open page: {0}", new object[]
		{
			page.gameObject.name
		});
	}

	// Token: 0x06005FC3 RID: 24515 RVA: 0x001F379C File Offset: 0x001F199C
	protected void HandleShopOpened()
	{
		foreach (WidgetInstance instance in this.m_tempInstances)
		{
			base.StartCoroutine(this.PreloadPageInstanceCoroutine(instance));
		}
	}

	// Token: 0x06005FC4 RID: 24516 RVA: 0x001F37F8 File Offset: 0x001F19F8
	protected IEnumerator PreloadPageInstanceCoroutine(WidgetInstance instance)
	{
		yield return new WaitForSeconds(0.1f);
		Shop shop = Shop.Get();
		while (shop != null && !shop.Browser.IsReady() && shop.IsOpen())
		{
			yield return null;
		}
		if (shop == null || !shop.IsOpen())
		{
			yield break;
		}
		instance.Initialize();
		bool wasActive = instance.gameObject.activeSelf;
		instance.gameObject.SetActive(true);
		yield return null;
		instance.gameObject.SetActive(wasActive);
		yield break;
	}

	// Token: 0x06005FC5 RID: 24517 RVA: 0x001F3807 File Offset: 0x001F1A07
	protected void ForceInitializeTempInstance(WidgetInstance instance)
	{
		instance.Initialize();
		instance.gameObject.SetActive(true);
	}

	// Token: 0x06005FC6 RID: 24518 RVA: 0x001F381C File Offset: 0x001F1A1C
	protected void HandleShopClosed()
	{
		this.m_tempInstancesHaveBeenInitialized = false;
		this.SetProduct(null, null);
		this.m_tempInstances.ForEach(delegate(WidgetInstance i)
		{
			i.Unload();
		});
		this.m_pages.Clear();
	}

	// Token: 0x06005FC7 RID: 24519 RVA: 0x001F386D File Offset: 0x001F1A6D
	protected IEnumerator OpenProductPageCoroutine()
	{
		while (this.m_widget.IsChangingStates && this.IsOpen)
		{
			yield return null;
		}
		if (!this.IsOpen)
		{
			yield break;
		}
		WidgetInstance activeInstance = this.m_tempInstances.FirstOrDefault((WidgetInstance i) => i.gameObject.activeInHierarchy);
		if (activeInstance == null)
		{
			Log.Store.PrintError("Failed to activate any product page for data model.", Array.Empty<object>());
			this.Close();
			yield break;
		}
		activeInstance.Initialize();
		while (this.IsOpen && (!activeInstance.IsReady || activeInstance.IsChangingStates))
		{
			yield return null;
		}
		ProductPage activePage = this.m_pages.FirstOrDefault((ProductPage p) => p.gameObject.activeInHierarchy);
		if (activePage == null)
		{
			Log.Store.PrintError("Failed to instantiate any product page for data model.", Array.Empty<object>());
			this.Close();
			yield break;
		}
		activePage.Open();
		while (activePage.WidgetComponent.IsChangingStates && this.IsOpen)
		{
			yield return null;
		}
		if (!this.IsOpen)
		{
			yield break;
		}
		this.m_widget.TriggerEvent("OPEN", new Widget.TriggerEventParameters
		{
			NoDownwardPropagation = true,
			IgnorePlaymaker = true
		});
		UIContext.GetRoot().ShowPopup(this.m_widget.gameObject, UIContext.BlurType.Standard);
		if (this.OnOpened != null)
		{
			this.OnOpened();
		}
		yield break;
	}

	// Token: 0x0400504D RID: 20557
	[SerializeField]
	private GameObject m_pageRoot;

	// Token: 0x0400504E RID: 20558
	private Widget m_widget;

	// Token: 0x0400504F RID: 20559
	private ProductDataModel m_product = ProductFactory.CreateEmptyProductDataModel();

	// Token: 0x04005050 RID: 20560
	private MusicPlaylistBookmark m_musicPlaylistBookmark;

	// Token: 0x04005051 RID: 20561
	private List<ProductPage> m_pages = new List<ProductPage>();

	// Token: 0x04005052 RID: 20562
	private List<WidgetInstance> m_tempInstances = new List<WidgetInstance>();

	// Token: 0x04005053 RID: 20563
	private ProductPage m_currentProductPage;

	// Token: 0x04005054 RID: 20564
	private bool m_tempInstancesHaveBeenInitialized;

	// Token: 0x04005055 RID: 20565
	private const string OPEN = "OPEN";

	// Token: 0x04005056 RID: 20566
	private const string CLOSED = "CLOSED";

	// Token: 0x04005057 RID: 20567
	private const string EVENT_DISMISS = "CODE_DISMISS";

	// Token: 0x04005058 RID: 20568
	private readonly PlatformDependentValue<bool> UnloadUnusedAssetsOnClose = new PlatformDependentValue<bool>(PlatformCategory.Memory)
	{
		LowMemory = true,
		MediumMemory = true,
		HighMemory = false
	};
}
