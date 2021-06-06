using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class ProductPageContainer : MonoBehaviour
{
	[SerializeField]
	private GameObject m_pageRoot;

	private Widget m_widget;

	private ProductDataModel m_product = ProductFactory.CreateEmptyProductDataModel();

	private MusicPlaylistBookmark m_musicPlaylistBookmark;

	private List<ProductPage> m_pages = new List<ProductPage>();

	private List<WidgetInstance> m_tempInstances = new List<WidgetInstance>();

	private ProductPage m_currentProductPage;

	private bool m_tempInstancesHaveBeenInitialized;

	private const string OPEN = "OPEN";

	private const string CLOSED = "CLOSED";

	private const string EVENT_DISMISS = "CODE_DISMISS";

	private readonly PlatformDependentValue<bool> UnloadUnusedAssetsOnClose = new PlatformDependentValue<bool>(PlatformCategory.Memory)
	{
		LowMemory = true,
		MediumMemory = true,
		HighMemory = false
	};

	public bool IsOpen { get; private set; }

	public ProductDataModel Product => m_product;

	public ProductDataModel Variant { get; set; }

	public event Action OnOpened;

	public event Action OnClosed;

	public event Action OnProductSet;

	protected virtual void Awake()
	{
		if (m_pageRoot != null)
		{
			m_pageRoot.SetActive(value: false);
		}
		else
		{
			Log.Store.PrintError("ProductPageContainer missing reference to product page root object. This may prevent pages from opening!");
		}
	}

	protected virtual void Start()
	{
		m_widget = GetComponent<Widget>();
		m_widget.RegisterEventListener(delegate(string evt)
		{
			if (evt == "CODE_DISMISS")
			{
				UIContext.GetRoot().DismissPopup(m_widget.gameObject);
				if (m_pageRoot != null)
				{
					m_pageRoot.SetActive(value: false);
				}
			}
		});
		m_tempInstances = GetComponentsInChildren<WidgetInstance>(includeInactive: true).ToList().FindAll((WidgetInstance a) => a.name.Contains("[temp]"));
		Shop shop = Shop.Get();
		if (shop != null)
		{
			shop.OnOpened += HandleShopOpened;
			shop.OnCloseCompleted += HandleShopClosed;
		}
	}

	protected virtual void OnDestroy()
	{
		Shop shop = Shop.Get();
		if (shop != null)
		{
			shop.OnOpened -= HandleShopOpened;
			shop.OnCloseCompleted -= HandleShopClosed;
		}
	}

	protected virtual void Update()
	{
	}

	public void Open()
	{
		Open(m_product, Variant);
	}

	public void Open(ProductDataModel product, ProductDataModel variant = null)
	{
		if (product == null || product == ProductFactory.CreateEmptyProductDataModel())
		{
			Log.Store.PrintError("ProductPageContainer cannot open null or empty product");
			return;
		}
		base.gameObject.SetActive(value: true);
		if (!IsOpen)
		{
			if (m_pageRoot == null)
			{
				Log.Store.PrintError("ProductPageContainer missing reference to the product page root object");
				return;
			}
			m_pageRoot.SetActive(value: true);
			IsOpen = true;
			SetProduct(product, variant);
			StartCoroutine(OpenProductPageCoroutine());
		}
	}

	public void Close()
	{
		if (!IsOpen)
		{
			return;
		}
		if (m_currentProductPage != null)
		{
			if (m_currentProductPage.IsOpen)
			{
				m_currentProductPage.Close();
				return;
			}
			m_currentProductPage = null;
		}
		IsOpen = false;
		StopMusicOverride();
		m_widget.TriggerEvent("CLOSED", new Widget.TriggerEventParameters
		{
			NoDownwardPropagation = true,
			IgnorePlaymaker = true
		});
		if (this.OnClosed != null)
		{
			this.OnClosed();
		}
		if ((bool)UnloadUnusedAssetsOnClose && HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().UnloadUnusedAssets();
		}
	}

	public ProductPage GetCurrentProductPage()
	{
		return m_currentProductPage;
	}

	public void RegisterProductPage(ProductPage page)
	{
		m_pages.Add(page);
		page.OnOpened += delegate
		{
			HandleProductPageOpened(page);
		};
		page.OnClosed += delegate
		{
			HandleProductPageClosed(page);
		};
	}

	public void SetProduct(ProductDataModel product, ProductDataModel variant = null)
	{
		product = product ?? ProductFactory.CreateEmptyProductDataModel();
		Variant = variant ?? product;
		if (product != m_product)
		{
			m_product = product;
			BindCurrentProduct();
			if (this.OnProductSet != null)
			{
				this.OnProductSet();
			}
		}
	}

	public void OverrideMusic(MusicPlaylistType playList)
	{
		if (m_musicPlaylistBookmark == null)
		{
			m_musicPlaylistBookmark = MusicManager.Get().CreateBookmarkOfCurrentPlaylist();
		}
		MusicManager.Get().StartPlaylist(playList);
	}

	public void StopMusicOverride()
	{
		if (m_musicPlaylistBookmark != null)
		{
			MusicManager musicManager = MusicManager.Get();
			if (musicManager != null)
			{
				musicManager.StopPlaylist();
				musicManager.PlayFromBookmark(m_musicPlaylistBookmark);
			}
			m_musicPlaylistBookmark = null;
		}
	}

	public void InitializeTempInstances()
	{
		if (m_tempInstancesHaveBeenInitialized)
		{
			return;
		}
		m_tempInstancesHaveBeenInitialized = true;
		foreach (WidgetInstance tempInstance in m_tempInstances)
		{
			ForceInitializeTempInstance(tempInstance);
		}
	}

	protected void BindCurrentProduct()
	{
		m_widget.BindDataModel(m_product);
	}

	protected void HandleProductPageOpened(ProductPage page)
	{
		if (m_currentProductPage != null)
		{
			Log.Store.PrintError("Previous product page did not close properly: {0}", m_currentProductPage.gameObject.name);
		}
		m_currentProductPage = page;
	}

	protected void HandleProductPageClosed(ProductPage page)
	{
		if (m_currentProductPage == page)
		{
			m_currentProductPage = null;
			Close();
		}
		else
		{
			Log.Store.PrintError("Product page closed but it is not the currently open page: {0}", page.gameObject.name);
		}
	}

	protected void HandleShopOpened()
	{
		foreach (WidgetInstance tempInstance in m_tempInstances)
		{
			StartCoroutine(PreloadPageInstanceCoroutine(tempInstance));
		}
	}

	protected IEnumerator PreloadPageInstanceCoroutine(WidgetInstance instance)
	{
		yield return new WaitForSeconds(0.1f);
		Shop shop = Shop.Get();
		while (shop != null && !shop.Browser.IsReady() && shop.IsOpen())
		{
			yield return null;
		}
		if (!(shop == null) && shop.IsOpen())
		{
			instance.Initialize();
			bool wasActive = instance.gameObject.activeSelf;
			instance.gameObject.SetActive(value: true);
			yield return null;
			instance.gameObject.SetActive(wasActive);
		}
	}

	protected void ForceInitializeTempInstance(WidgetInstance instance)
	{
		instance.Initialize();
		instance.gameObject.SetActive(value: true);
	}

	protected void HandleShopClosed()
	{
		m_tempInstancesHaveBeenInitialized = false;
		SetProduct(null);
		m_tempInstances.ForEach(delegate(WidgetInstance i)
		{
			i.Unload();
		});
		m_pages.Clear();
	}

	protected IEnumerator OpenProductPageCoroutine()
	{
		while (m_widget.IsChangingStates && IsOpen)
		{
			yield return null;
		}
		if (!IsOpen)
		{
			yield break;
		}
		WidgetInstance activeInstance = m_tempInstances.FirstOrDefault((WidgetInstance i) => i.gameObject.activeInHierarchy);
		if (activeInstance == null)
		{
			Log.Store.PrintError("Failed to activate any product page for data model.");
			Close();
			yield break;
		}
		activeInstance.Initialize();
		while (IsOpen && (!activeInstance.IsReady || activeInstance.IsChangingStates))
		{
			yield return null;
		}
		ProductPage activePage = m_pages.FirstOrDefault((ProductPage p) => p.gameObject.activeInHierarchy);
		if (activePage == null)
		{
			Log.Store.PrintError("Failed to instantiate any product page for data model.");
			Close();
			yield break;
		}
		activePage.Open();
		while (activePage.WidgetComponent.IsChangingStates && IsOpen)
		{
			yield return null;
		}
		if (IsOpen)
		{
			m_widget.TriggerEvent("OPEN", new Widget.TriggerEventParameters
			{
				NoDownwardPropagation = true,
				IgnorePlaymaker = true
			});
			UIContext.GetRoot().ShowPopup(m_widget.gameObject);
			if (this.OnOpened != null)
			{
				this.OnOpened();
			}
		}
	}
}
