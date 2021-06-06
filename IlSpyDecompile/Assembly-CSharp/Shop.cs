using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Blizzard.Telemetry.WTCG.Client;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

public class Shop : MonoBehaviour, IStore
{
	private class PurchaseOrder
	{
		public ProductDataModel m_product;

		public PriceDataModel m_price;

		public int m_quantity = 1;
	}

	private delegate void ReopenCallback();

	[SerializeField]
	protected UIBScrollable m_browserScroller;

	[SerializeField]
	protected VisualController m_shopStateController;

	[SerializeField]
	protected AsyncReference m_shopBrowserRef;

	[SerializeField]
	protected AsyncReference m_vcPageRef;

	[SerializeField]
	protected AsyncReference m_bcPageRef;

	[SerializeField]
	protected AsyncReference m_productPageContainerRef;

	[SerializeField]
	protected AsyncReference m_quantityPromptRef;

	private static Shop s_instance;

	private ShopBrowser m_browser;

	private WidgetTemplate m_browserWidgetTemplate;

	private VirtualCurrencyPurchasePage m_vcPage;

	private CurrencyConversionPage m_bcPage;

	private ProductPageContainer m_productPageContainer;

	private ProductPage m_currentProductPage;

	private StoreQuantityPrompt m_quantityPrompt;

	private WidgetTemplate m_widget;

	private ShopDataModel m_shopData;

	private Stack<PurchaseOrder> m_autoPurchaseStack = new Stack<PurchaseOrder>();

	private ReopenCallback m_reopenPageCall;

	private bool m_suppressBoxOpen;

	protected bool m_isOpen;

	private long m_tiersChangeCountAtLastRefresh;

	private CameraMask[] m_cameraMasks;

	private bool m_isAnimatingOpenOrClose;

	private const string OPEN = "OPEN";

	private const string CLOSED = "CLOSED";

	private const string SHOP_GO_BACK = "SHOP_GO_BACK";

	private const string SHOP_SHOW_INFO = "SHOP_SHOW_INFO";

	private const string SHOP_BUY_VC = "SHOP_BUY_VC";

	private const string SHOP_TOGGLE_AUTOCONVERT = "SHOP_TOGGLE_AUTOCONVERT";

	private const string SHOP_BLOCK_INTERFACE = "SHOP_BLOCK_INTERFACE";

	private const string SHOP_UNBLOCK_INTERFACE = "SHOP_UNBLOCK_INTERFACE";

	private readonly PlatformDependentValue<bool> UnloadUnusedAssetsOnClose = new PlatformDependentValue<bool>(PlatformCategory.Memory)
	{
		LowMemory = true,
		MediumMemory = true,
		HighMemory = false
	};

	public ProductDataModel CurrentProduct { get; private set; }

	public ProductPage CurrentProductPage
	{
		get
		{
			return m_currentProductPage;
		}
		private set
		{
			if (!(m_currentProductPage == value))
			{
				m_currentProductPage = value;
				if (this.OnProductPageChanged != null)
				{
					this.OnProductPageChanged(m_currentProductPage);
				}
			}
		}
	}

	public ShopDataModel ShopData => m_shopData;

	public ShopBrowser Browser => m_browser;

	public StoreQuantityPrompt QuantityPrompt => m_quantityPrompt;

	public event Action OnOpened;

	public event Action<StoreClosedArgs> OnClosed;

	public event Action OnOpenCompleted;

	public event Action OnCloseCompleted;

	public event Action OnReady;

	public event Action<ProductPage> OnProductPageChanged;

	public event Action<BuyProductEventArgs> OnProductPurchaseAttempt;

	public event Action<CurrencyBalanceChangedEventArgs> CurrencyBalanceChanged;

	public event Action OnProductPageClosed;

	protected virtual void Start()
	{
		s_instance = this;
		m_cameraMasks = GetComponentsInChildren<CameraMask>(includeInactive: true);
		StoreManager storeManager = StoreManager.Get();
		storeManager.RegisterSuccessfulPurchaseListener(HandleSuccessfulPurchase);
		storeManager.RegisterSuccessfulPurchaseAckListener(HandleSuccessfulPurchaseAck);
		m_shopBrowserRef.RegisterReadyListener(delegate(ShopBrowser page)
		{
			m_browser = page;
			m_browserWidgetTemplate = page.GetComponent<WidgetTemplate>();
		});
		m_vcPageRef.RegisterReadyListener(delegate(VirtualCurrencyPurchasePage page)
		{
			RegisterProductPage(page, out m_vcPage);
		});
		m_bcPageRef.RegisterReadyListener(delegate(CurrencyConversionPage page)
		{
			RegisterProductPage(page, out m_bcPage);
		});
		m_productPageContainerRef.RegisterReadyListener(delegate(ProductPageContainer page)
		{
			m_productPageContainer = page;
			m_productPageContainer.OnOpened += HandlePageOpened;
			m_productPageContainer.OnClosed += HandlePageClosed;
		});
		m_quantityPromptRef.RegisterReadyListener(delegate(StoreQuantityPrompt page)
		{
			m_quantityPrompt = page;
		});
		m_shopData = new ShopDataModel();
		m_shopData.VirtualCurrencyBalance = GetCurrencyBalanceDataModel(CurrencyType.RUNESTONES);
		m_shopData.BoosterCurrencyBalance = GetCurrencyBalanceDataModel(CurrencyType.ARCANE_ORBS);
		m_shopData.GoldBalance = GetCurrencyBalanceDataModel(CurrencyType.GOLD);
		m_shopData.DustBalance = GetCurrencyBalanceDataModel(CurrencyType.DUST);
		m_shopData.VirtualCurrency = ProductFactory.CreateEmptyProductDataModel();
		m_shopData.BoosterCurrency = ProductFactory.CreateEmptyProductDataModel();
		foreach (CurrencyCache allCurrencyCache in GetAllCurrencyCaches(forceIncludeVc: true))
		{
			allCurrencyCache.OnFirstCache += HandleOnCurrencyFirstCached;
			allCurrencyCache.OnBalanceChanged += HandleOnCurrencyBalanceChanged;
		}
		GlobalDataContext.Get().BindDataModel(m_shopData);
		m_widget = GetComponent<WidgetTemplate>();
		if (m_widget != null)
		{
			m_widget.RegisterEventListener(HandleWidgetEvent);
			m_widget.BindDataModel(m_shopData);
		}
		NetCache.Get().RegisterGoldBalanceListener(HandleGoldBalanceUpdate);
		NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheArcaneDustBalance), HandleDustBalanceUpdate);
		CurrencyBalanceChanged += delegate
		{
			TryNextAutoPurchase();
		};
		if (StoreManager.Get() != null)
		{
			StoreManager.Get().RegisterAmazingNewShop(this);
		}
		if (this.OnReady != null)
		{
			this.OnReady();
		}
	}

	protected virtual void OnDestroy()
	{
		if (s_instance == this)
		{
			s_instance = null;
		}
		foreach (CurrencyCache allCurrencyCache in GetAllCurrencyCaches(forceIncludeVc: true))
		{
			allCurrencyCache.OnFirstCache -= HandleOnCurrencyFirstCached;
			allCurrencyCache.OnBalanceChanged -= this.CurrencyBalanceChanged;
		}
		StoreManager storeManager = StoreManager.Get();
		if (storeManager != null)
		{
			storeManager.RemoveSuccessfulPurchaseListener(HandleSuccessfulPurchase);
			storeManager.RemoveSuccessfulPurchaseAckListener(HandleSuccessfulPurchaseAck);
		}
		NetCache netCache = NetCache.Get();
		if (netCache != null)
		{
			netCache.RemoveGoldBalanceListener(HandleGoldBalanceUpdate);
			netCache.RemoveUpdatedListener(typeof(NetCache.NetCacheArcaneDustBalance), HandleDustBalanceUpdate);
		}
	}

	protected virtual void Update()
	{
		if (IsReadyToRequestVirtualCurrencyBalances())
		{
			RequestVirtualBalanceIfNeeded(CurrencyType.RUNESTONES);
			RequestVirtualBalanceIfNeeded(CurrencyType.ARCANE_ORBS);
		}
		if (StoreManager.Get().Catalog.TiersChangeCount != m_tiersChangeCountAtLastRefresh && StoreManager.Get().IsOpen(printStatus: false))
		{
			RefreshContent();
		}
	}

	public bool IsReady()
	{
		return s_instance != null;
	}

	public static Shop Get()
	{
		return s_instance;
	}

	public bool IsOpen()
	{
		return m_isOpen;
	}

	public PriceDataModel GetCurrencyBalanceDataModel(CurrencyType currency)
	{
		return GetCurrencyCache(currency).priceDataModel;
	}

	public bool IsCloseDisabled()
	{
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out var service) && service.CurrentState == HearthstoneCheckout.State.InProgress)
		{
			return true;
		}
		if (!StoreManager.Get().CanTapOutConfirmationUI())
		{
			return true;
		}
		return false;
	}

	public void Open()
	{
		if (!m_isOpen)
		{
			m_isOpen = true;
			ShownUIMgr.Get().SetShownUI(ShownUIMgr.UI_WINDOW.GENERAL_STORE);
			Navigation.Push(OnNavigateBack);
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.STORE);
			StoreManager.Get().Catalog.TryRefreshStaleProductAvailability();
			RefreshContent();
			base.gameObject.SetActive(value: true);
			m_browser.gameObject.SetActive(!m_suppressBoxOpen);
			if (!m_suppressBoxOpen)
			{
				m_isAnimatingOpenOrClose = true;
				SetMasking(maskingEnabled: true);
				m_shopStateController.SetState("OPEN");
				UpdateScrollerEnabled();
				StartCoroutine(SendShopVisitTelemetry());
			}
			if (this.OnOpened != null)
			{
				this.OnOpened();
			}
		}
	}

	public void Close()
	{
		Close(forceClose: false);
	}

	public void Close(bool forceClose)
	{
		if ((forceClose || !IsCloseDisabled()) && m_isOpen)
		{
			if (m_productPageContainer != null)
			{
				m_productPageContainer.Close();
			}
			CurrentProduct = null;
			CancelAutoPurchases();
			Navigation.RemoveHandler(OnNavigateBack);
			if (ShownUIMgr.Get() != null)
			{
				ShownUIMgr.Get().ClearShownUI();
			}
			PresenceMgr.Get().SetPrevStatus();
			m_isOpen = false;
			if (!m_suppressBoxOpen)
			{
				m_isAnimatingOpenOrClose = true;
				m_shopStateController.SetState("CLOSED");
			}
			m_suppressBoxOpen = false;
			UpdateScrollerEnabled();
			SetMasking(maskingEnabled: true);
			if (this.OnClosed != null)
			{
				this.OnClosed(new StoreClosedArgs());
			}
		}
	}

	public void BlockInterface(bool blocked)
	{
		Widget.TriggerEventParameters parameters;
		if (blocked)
		{
			WidgetTemplate widget = m_widget;
			parameters = new Widget.TriggerEventParameters
			{
				IgnorePlaymaker = true,
				NoDownwardPropagation = false
			};
			widget.TriggerEvent("SHOP_BLOCK_INTERFACE", parameters);
		}
		else
		{
			WidgetTemplate widget2 = m_widget;
			parameters = new Widget.TriggerEventParameters
			{
				IgnorePlaymaker = true,
				NoDownwardPropagation = false
			};
			widget2.TriggerEvent("SHOP_UNBLOCK_INTERFACE", parameters);
		}
	}

	public bool CanSafelyOpenCurrencyPage()
	{
		if (m_vcPage.IsAnimating)
		{
			Log.Store.PrintDebug("Cannot open currency page while runestones page is still animating.");
			return false;
		}
		if (m_bcPage.IsAnimating)
		{
			Log.Store.PrintDebug("Cannot open currency page while arcane orbs page is still animating.");
			return false;
		}
		if (PopupDisplayManager.Get() != null && PopupDisplayManager.Get().IsShowing)
		{
			Log.Store.PrintDebug("Cannot open currency page while PopupDisplayManager is showing popup.");
			return false;
		}
		if (StoreManager.Get() != null && StoreManager.Get().IsPromptShowing)
		{
			Log.Store.PrintDebug("Cannot open currency page while StoreManager is showing popup.");
			return false;
		}
		return true;
	}

	public void OpenVirtualCurrencyPurchase(float desiredPurchaseAmount = 0f, bool rememberLastPage = false)
	{
		if (m_shopData.VirtualCurrency == null || m_shopData.VirtualCurrency == ProductFactory.CreateEmptyProductDataModel())
		{
			Log.Store.PrintError("No valid runestone products received.");
			return;
		}
		if (m_shopData.VirtualCurrency.Availability != ProductAvailability.CAN_PURCHASE)
		{
			Log.Store.PrintError("Runestones not available for purchase");
			return;
		}
		if (m_vcPage.IsOpen)
		{
			Log.Store.PrintDebug("Cannot open runestone purchase page while already open.");
			return;
		}
		CleanUpPagesForCurrencyPage(rememberLastPage);
		if (m_vcPage != null)
		{
			m_vcPage.OpenToSKU(desiredPurchaseAmount, rememberLastPage);
		}
	}

	public void OpenBoosterCurrencyPurchase(float desiredPurchaseAmount = 0f, bool rememberLastPage = false)
	{
		if (m_shopData.BoosterCurrency == null || m_shopData.BoosterCurrency == ProductFactory.CreateEmptyProductDataModel())
		{
			Log.Store.PrintError("No valid Arcane Orb product received.");
			return;
		}
		if (m_shopData.BoosterCurrency.Availability != ProductAvailability.CAN_PURCHASE)
		{
			Log.Store.PrintError("Arcane Orbs not available for purchase");
			return;
		}
		if (m_bcPage.IsOpen)
		{
			Log.Store.PrintDebug("Cannot open arcane orb purchase page while already open.");
			return;
		}
		if (GetCurrencyCache(CurrencyType.ARCANE_ORBS).NeedsRefresh())
		{
			if (DialogManager.Get() != null)
			{
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				popupInfo.m_text = GameStrings.Format("GLUE_STORE_FAIL_CURRENCY_BALANCE");
				popupInfo.m_showAlertIcon = true;
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
				DialogManager.Get().ShowPopup(popupInfo);
			}
			return;
		}
		long cachedBalance = ShopUtils.GetCachedBalance(CurrencyType.ARCANE_ORBS);
		float amountOfCurrencyInProduct = ShopUtils.GetAmountOfCurrencyInProduct(m_shopData.BoosterCurrency, CurrencyType.ARCANE_ORBS);
		if ((float)cachedBalance + amountOfCurrencyInProduct > 9999f)
		{
			if (DialogManager.Get() != null)
			{
				AlertPopup.PopupInfo popupInfo2 = new AlertPopup.PopupInfo();
				popupInfo2.m_headerText = GameStrings.Format("GLUE_ARCANE_ORBS_CAP_HEADER");
				popupInfo2.m_text = GameStrings.Format("GLUE_ARCANE_ORBS_CAP_BODY", 9999);
				popupInfo2.m_showAlertIcon = true;
				popupInfo2.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
				DialogManager.Get().ShowPopup(popupInfo2);
			}
		}
		else
		{
			CleanUpPagesForCurrencyPage(rememberLastPage);
			if (m_bcPage != null)
			{
				m_bcPage.OpenToSKU(desiredPurchaseAmount);
			}
		}
	}

	public static void OpenToProductPageWhenReady(long pmtProductId, bool suppressBox)
	{
		s_instance.m_suppressBoxOpen = suppressBox;
		Processor.QueueJob("OpenToProductPage", Job_OpenToProductPage(pmtProductId));
	}

	public static void OpenToTavernPassPageWhenReady()
	{
		if (StoreManager.Get().TryGetBonusProductBundleId(ProductType.PRODUCT_TYPE_PROGRESSION_BONUS, out var pmtId))
		{
			OpenToProductPageWhenReady(pmtId, suppressBox: true);
		}
		else
		{
			OpenTavernPassErrorPopup();
		}
	}

	public static void OpenTavernPassErrorPopup()
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_PROGRESSION_BONUS_ERROR_HEADER"),
			m_text = GameStrings.Get("GLUE_PROGRESSION_BONUS_ERROR_BODY"),
			m_showAlertIcon = false,
			m_responseDisplay = AlertPopup.ResponseDisplay.OK
		};
		DialogManager.Get().ShowPopup(info);
	}

	internal void OpenProductPage(ProductDataModel product, ProductDataModel variant = null)
	{
		if (product == null || product == ProductFactory.CreateEmptyProductDataModel())
		{
			Log.Store.PrintError("Shop cannot open null or empty product");
			return;
		}
		Log.Store.PrintDebug("[Shop.OpenProductPage] display product {0}", product.Name);
		CurrentProduct = product;
		if (m_productPageContainer != null)
		{
			m_productPageContainer.InitializeTempInstances();
			m_productPageContainer.Open(product, variant);
		}
		MarkProductAsSeen(product);
	}

	public void AttemptToPurchaseProduct(ProductDataModel product, PriceDataModel price, int quantity = 1)
	{
		CancelAutoPurchases();
		PurchaseOrder purchaseOrder = new PurchaseOrder
		{
			m_product = product,
			m_price = price,
			m_quantity = quantity
		};
		while (purchaseOrder != null)
		{
			m_autoPurchaseStack.Push(purchaseOrder);
			purchaseOrder = GetPrerequisitePurchase(purchaseOrder);
			if (purchaseOrder == null)
			{
				break;
			}
			if (purchaseOrder.m_product == null)
			{
				Log.Store.PrintError("Purchase could not be started");
				return;
			}
		}
		purchaseOrder = m_autoPurchaseStack.Pop();
		ExecutePurchaseOrder(purchaseOrder);
	}

	public void RefreshWallet()
	{
		UpdateCurrencyBalance(CurrencyType.GOLD, ShopUtils.GetCachedBalance(CurrencyType.GOLD));
		UpdateCurrencyBalance(CurrencyType.DUST, ShopUtils.GetCachedBalance(CurrencyType.DUST));
	}

	public void DisplayCurrencyBalance(CurrencyType currency, long balance)
	{
		GetCurrencyCache(currency).UpdateDisplayText(balance.ToString());
	}

	public void Unload()
	{
		Close(forceClose: true);
	}

	public bool WillAutoPurchase()
	{
		return m_autoPurchaseStack.Count > 0;
	}

	public IEnumerable<CurrencyType> GetVisibleCurrencies()
	{
		if (!ShopUtils.IsVirtualCurrencyEnabled())
		{
			return new CurrencyType[1] { CurrencyType.GOLD };
		}
		return new CurrencyType[3]
		{
			CurrencyType.GOLD,
			CurrencyType.RUNESTONES,
			CurrencyType.ARCANE_ORBS
		};
	}

	private void MarkProductAsSeen(ProductDataModel product)
	{
		if (!product.Tags.Remove("new"))
		{
			return;
		}
		string @string = Options.Get().GetString(Option.LATEST_SEEN_SHOP_PRODUCT_LIST);
		List<string> list = new List<string>(@string.Split(':'));
		string item = product.PmtId.ToString();
		if (!list.Contains(item))
		{
			list.Add(item);
		}
		if (m_shopData.Tiers.Any((ProductTierDataModel t) => t.BrowserButtons.Count > 0))
		{
			for (int i = 0; i < list.Count; i++)
			{
				long pmtId = 0L;
				if (!long.TryParse(list[i], out pmtId) || !m_shopData.Tiers.Any((ProductTierDataModel t) => t.BrowserButtons.Any((ShopBrowserButtonDataModel b) => b.DisplayProduct.PmtId == pmtId)))
				{
					list.RemoveAt(i--);
				}
			}
		}
		@string = string.Join(":", list.ToArray());
		Options.Get().SetString(Option.LATEST_SEEN_SHOP_PRODUCT_LIST, @string);
	}

	private bool ContainsNewlyDisplayedItems()
	{
		if (StoreManager.Get() == null || StoreManager.Get().IsVintageStoreEnabled() || Box.Get() == null || Box.Get().IsTutorial())
		{
			return false;
		}
		List<string> listOfNewProducts = GetListOfNewProducts();
		string @string = Options.Get().GetString(Option.LATEST_DISPLAYED_SHOP_PRODUCT_LIST);
		List<string> list = new List<string>();
		list.AddRange(@string.Split(':'));
		foreach (string item in listOfNewProducts)
		{
			if (!list.Contains(item))
			{
				return true;
			}
		}
		return false;
	}

	private void MarkShopAsSeen()
	{
		if (m_shopData.HasNewItems)
		{
			m_shopData.HasNewItems = false;
			List<string> listOfNewProducts = GetListOfNewProducts();
			string val = string.Join(":", listOfNewProducts.ToArray());
			Options.Get().SetString(Option.LATEST_DISPLAYED_SHOP_PRODUCT_LIST, val);
		}
	}

	private List<string> GetListOfNewProducts()
	{
		List<string> productIds = new List<string>();
		m_shopData.Tiers.ForEach(delegate(ProductTierDataModel t)
		{
			t.BrowserButtons.ForEach(delegate(ShopBrowserButtonDataModel button)
			{
				if (button.DisplayProduct.Tags.Contains("new"))
				{
					productIds.Add(button.DisplayProduct.PmtId.ToString());
				}
			});
		});
		return productIds;
	}

	private void ExecutePurchaseOrder(PurchaseOrder purchase)
	{
		if (purchase == null || purchase.m_product == null || purchase.m_price == null)
		{
			Log.Store.PrintError("ExecutePurchaseOrder failed. PurchaseOrder invalid.");
		}
		else if (purchase.m_product.Tags.Contains("runestones") && m_autoPurchaseStack.Count > 0)
		{
			PurchaseOrder nextBuyWithVC = m_autoPurchaseStack.Last((PurchaseOrder p) => p.m_price != null && p.m_price.Currency == CurrencyType.RUNESTONES);
			if (nextBuyWithVC == null)
			{
				Log.Store.PrintError("Unnecessary VC purchase planned; skipping");
				ExecutePurchaseOrder(m_autoPurchaseStack.Pop());
				return;
			}
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_showAlertIcon = false,
				m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
				m_confirmText = GameStrings.Get("GLOBAL_BUTTON_YES"),
				m_cancelText = GameStrings.Get("GLOBAL_BUTTON_NOT_NOW"),
				m_alertTextAlignment = UberText.AlignmentOptions.Center,
				m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle,
				m_headerText = GameStrings.Get("GLUE_SHOP_GET_MORE_RUNESTONES_HEADER"),
				m_text = GameStrings.Get("GLUE_SHOP_GET_MORE_RUNESTONES"),
				m_responseCallback = delegate(AlertPopup.Response response, object userData)
				{
					BlockInterface(blocked: false);
					if (response == AlertPopup.Response.CONFIRM)
					{
						long deficitForPurchase = GetDeficitForPurchase(nextBuyWithVC);
						OpenVirtualCurrencyPurchase(deficitForPurchase, rememberLastPage: true);
					}
				}
			};
			CancelAutoPurchases();
			BlockInterface(blocked: true);
			DialogManager.Get().ShowPopup(info);
		}
		else
		{
			BuyProductEventArgs buyProductArgs = purchase.m_product.GetBuyProductArgs(purchase.m_price, purchase.m_quantity);
			if (buyProductArgs == null)
			{
				Log.Store.PrintError("ExecutePurchaseOrder failed. No valid BuyProductEventArgs for product.");
			}
			else if (this.OnProductPurchaseAttempt != null)
			{
				this.OnProductPurchaseAttempt(buyProductArgs);
			}
			else
			{
				Log.Store.PrintError("ExecutePurchaseOrder failed. No OnProductPurchaseAttempt event handler registered.");
			}
		}
	}

	private void CleanUpPagesForCurrencyPage(bool rememberLastPage)
	{
		if (!rememberLastPage)
		{
			m_reopenPageCall = null;
		}
		CloseCurrentPage(rememberLastPage);
	}

	private void CloseCurrentPage(bool reopenLater)
	{
		ProductPage currentProductPage = CurrentProductPage;
		if (!currentProductPage)
		{
			return;
		}
		if (currentProductPage == m_vcPage || currentProductPage == m_bcPage)
		{
			currentProductPage.Close();
			if (reopenLater)
			{
				m_reopenPageCall = currentProductPage.Open;
			}
		}
		else if (currentProductPage == m_productPageContainer.GetCurrentProductPage())
		{
			m_productPageContainer.Close();
			if (reopenLater)
			{
				m_reopenPageCall = m_productPageContainer.Open;
			}
		}
	}

	private void ReopenClosedPage()
	{
		if (m_reopenPageCall != null)
		{
			m_reopenPageCall();
			m_reopenPageCall = null;
		}
	}

	private long GetDeficitForPurchase(PurchaseOrder purchase)
	{
		return ShopUtils.GetDeficit(new PriceDataModel
		{
			Currency = purchase.m_price.Currency,
			Amount = purchase.m_price.Amount * (float)purchase.m_quantity
		});
	}

	private void HandleWidgetEvent(string eventName)
	{
		switch (eventName)
		{
		case "SHOP_GO_BACK":
			if (IsOpen() && !Navigation.BackStackContainsHandler(OnNavigateBack))
			{
				Close();
			}
			else
			{
				Navigation.GoBack();
			}
			break;
		case "SHOP_SHOW_INFO":
			StoreManager.Get().ShowStoreInfo();
			break;
		case "SHOP_TOGGLE_AUTOCONVERT":
			m_shopData.AutoconvertCurrency = !m_shopData.AutoconvertCurrency;
			Options.Get().SetBool(Option.AUTOCONVERT_VIRTUAL_CURRENCY, m_shopData.AutoconvertCurrency);
			break;
		case "SHOP_BUY_VC":
			OpenVirtualCurrencyPurchase(0f, rememberLastPage: true);
			break;
		}
	}

	private void RefreshContent()
	{
		m_browserScroller.SetScroll(0f);
		RefreshDataModel();
		if (m_browser != null)
		{
			m_browser.RefreshContents();
		}
	}

	private void CompleteOpen()
	{
		m_isAnimatingOpenOrClose = false;
		MarkShopAsSeen();
		if (this.OnOpenCompleted != null)
		{
			this.OnOpenCompleted();
		}
		SetMasking(maskingEnabled: false);
		UpdateScrollerEnabled();
	}

	private void CompleteClose()
	{
		m_isAnimatingOpenOrClose = false;
		if (this.OnCloseCompleted != null)
		{
			this.OnCloseCompleted();
		}
		UpdateScrollerEnabled();
		if ((bool)UnloadUnusedAssetsOnClose && HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().UnloadUnusedAssets();
		}
	}

	private void SetMasking(bool maskingEnabled)
	{
		if (m_browserWidgetTemplate != null)
		{
			m_browserWidgetTemplate.SetLayerOverride(maskingEnabled ? GameLayer.CameraMask : GameLayer.Default);
		}
		if (m_cameraMasks != null)
		{
			CameraMask[] cameraMasks = m_cameraMasks;
			for (int i = 0; i < cameraMasks.Length; i++)
			{
				cameraMasks[i].enabled = maskingEnabled;
			}
		}
	}

	private void HandleSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		StoreManager.Get().Catalog.UpdateProductStatus();
	}

	private void HandleSuccessfulPurchase(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		RefreshWallet();
	}

	private void HandleGoldBalanceUpdate(NetCache.NetCacheGoldBalance balance)
	{
		Log.Store.PrintDebug("Gold balance updated to {0}", balance.GetTotal());
		UpdateCurrencyBalance(CurrencyType.GOLD, balance.GetTotal());
	}

	private void HandleDustBalanceUpdate()
	{
		if (NetCache.Get() != null)
		{
			long arcaneDustBalance = NetCache.Get().GetArcaneDustBalance();
			Log.Store.PrintDebug("Arcane Dust balance updated to {0}", arcaneDustBalance);
			UpdateCurrencyBalance(CurrencyType.DUST, arcaneDustBalance);
		}
	}

	public void RefreshDataModel()
	{
		ProductCatalog catalog = StoreManager.Get().Catalog;
		m_shopData.IsWild = CollectionManager.Get() != null && CollectionManager.Get().ShouldAccountSeeStandardWild();
		m_shopData.Tiers.Clear();
		if (catalog.HasTestData)
		{
			m_shopData.Tiers.AddRange(catalog.Tiers);
		}
		else
		{
			m_shopData.Tiers.AddRange(catalog.Tiers.Where((ProductTierDataModel t) => ShouldDisplayTier(t)));
		}
		m_shopData.VirtualCurrency = catalog.VirtualCurrencyProductItem ?? ProductFactory.CreateEmptyProductDataModel();
		m_shopData.BoosterCurrency = catalog.BoosterCurrencyProductItem ?? ProductFactory.CreateEmptyProductDataModel();
		m_shopData.AutoconvertCurrency = Options.Get().GetBool(Option.AUTOCONVERT_VIRTUAL_CURRENCY);
		m_shopData.HasNewItems = ContainsNewlyDisplayedItems();
		m_tiersChangeCountAtLastRefresh = catalog.TiersChangeCount;
		m_shopData.TavernTicketBalance = NetCache.Get().GetArenaTicketBalance();
		RefreshWallet();
	}

	private bool ShouldDisplayTier(ProductTierDataModel tier)
	{
		return tier.BrowserButtons.Any((ShopBrowserButtonDataModel b) => !b.IsFiller && ShopUtils.AreProductOrVariantsPurchasable(b.DisplayProduct));
	}

	private PurchaseOrder GetPrerequisitePurchase(PurchaseOrder pendingPurchase)
	{
		PriceDataModel price = pendingPurchase.m_price;
		if (price.Currency == CurrencyType.REAL_MONEY)
		{
			return null;
		}
		long deficitForPurchase = GetDeficitForPurchase(pendingPurchase);
		if (deficitForPurchase <= 0)
		{
			return null;
		}
		PurchaseOrder purchaseOrder = new PurchaseOrder();
		purchaseOrder.m_product = ShopUtils.FindCurrencyProduct(price.Currency, deficitForPurchase);
		if (purchaseOrder.m_product == null)
		{
			Log.Store.PrintError("Unable to find product with {0} of currency {1}", deficitForPurchase, price.Currency.ToString());
			return purchaseOrder;
		}
		if (purchaseOrder.m_product.Items.FirstOrDefault().ItemType == RewardItemType.ARCANE_ORBS && !m_shopData.AutoconvertCurrency)
		{
			Log.Store.PrintError("Unable to convert Booster Currency; autoconversion required");
			purchaseOrder.m_product = null;
			return purchaseOrder;
		}
		float amountOfCurrencyInProduct = ShopUtils.GetAmountOfCurrencyInProduct(purchaseOrder.m_product, price.Currency);
		if (amountOfCurrencyInProduct <= 0f)
		{
			Log.Store.PrintError("Invalid currency product; contains no currency");
			return purchaseOrder;
		}
		purchaseOrder.m_quantity = Mathf.CeilToInt((float)deficitForPurchase / amountOfCurrencyInProduct);
		purchaseOrder.m_price = purchaseOrder.m_product.Prices.FirstOrDefault();
		return purchaseOrder;
	}

	private void CancelAutoPurchases()
	{
		m_autoPurchaseStack.Clear();
	}

	private void HandlePageOpened()
	{
		UpdateScrollerEnabled();
		UpdateCurrentProductPage();
	}

	private void HandlePageClosed()
	{
		CancelAutoPurchases();
		ReopenClosedPage();
		UpdateScrollerEnabled();
		UpdateCurrentProductPage();
		if (m_suppressBoxOpen)
		{
			Close();
		}
		if (this.OnProductPageClosed != null)
		{
			this.OnProductPageClosed();
		}
	}

	private void UpdateCurrentProductPage()
	{
		ProductPage currentProductPage = null;
		if (m_productPageContainer.IsOpen)
		{
			currentProductPage = m_productPageContainer.GetCurrentProductPage();
		}
		else if (m_vcPage.IsOpen)
		{
			currentProductPage = m_vcPage;
		}
		else if (m_bcPage.IsOpen)
		{
			currentProductPage = m_bcPage;
		}
		CurrentProductPage = currentProductPage;
	}

	private void UpdateScrollerEnabled()
	{
		bool flag = (m_bcPage != null && m_bcPage.IsOpen) || (m_vcPage != null && m_vcPage.IsOpen) || (m_productPageContainer != null && m_productPageContainer.IsOpen);
		bool flag2 = IsOpen() && !flag && !m_isAnimatingOpenOrClose;
		m_browserScroller.enabled = flag2;
		m_browserScroller.SetHideThumb(!flag2);
	}

	private void TryNextAutoPurchase()
	{
		if (m_autoPurchaseStack.Count != 0)
		{
			PurchaseOrder purchaseOrder = m_autoPurchaseStack.Peek();
			if (purchaseOrder != null && GetDeficitForPurchase(purchaseOrder) == 0L)
			{
				m_autoPurchaseStack.Pop();
				ExecutePurchaseOrder(purchaseOrder);
			}
		}
	}

	private void RequestVirtualBalanceIfNeeded(CurrencyType currencyType)
	{
		if (!ShopUtils.IsVirtualCurrencyEnabled())
		{
			return;
		}
		if (!ShopUtils.IsCurrencyVirtual(currencyType))
		{
			Log.Store.PrintError("{0} is not a virtual currency", currencyType);
			return;
		}
		CurrencyCache currencyCache = GetCurrencyCache(currencyType);
		if (currencyCache.NeedsRefresh())
		{
			currencyCache.TryRefresh();
		}
	}

	private void UpdateCurrencyBalance(CurrencyType type, long balance)
	{
		GetCurrencyCache(type).UpdateBalance(balance);
	}

	private CurrencyCache GetCurrencyCache(CurrencyType type)
	{
		return StoreManager.Get().GetCurrencyCache(type);
	}

	private IEnumerable<CurrencyCache> GetAllCurrencyCaches(bool forceIncludeVc = false)
	{
		List<CurrencyCache> list = new List<CurrencyCache>();
		list.Add(GetCurrencyCache(CurrencyType.GOLD));
		list.Add(GetCurrencyCache(CurrencyType.DUST));
		if (forceIncludeVc || ShopUtils.IsVirtualCurrencyEnabled())
		{
			list.Add(GetCurrencyCache(CurrencyType.RUNESTONES));
			list.Add(GetCurrencyCache(CurrencyType.ARCANE_ORBS));
		}
		return list;
	}

	private void HandleOnCurrencyFirstCached()
	{
		List<Balance> list = new List<Balance>();
		foreach (CurrencyCache allCurrencyCache in GetAllCurrencyCaches())
		{
			if (allCurrencyCache.IsCached())
			{
				list.Add(new Balance
				{
					Name = Enum.GetName(typeof(CurrencyType), allCurrencyCache.type).ToLowerInvariant(),
					Amount = allCurrencyCache.priceDataModel.Amount
				});
				continue;
			}
			return;
		}
		TelemetryManager.Client().SendShopBalanceAvailable(list);
	}

	private void HandleOnCurrencyBalanceChanged(CurrencyBalanceChangedEventArgs args)
	{
		if (this.CurrencyBalanceChanged != null)
		{
			this.CurrencyBalanceChanged(args);
		}
	}

	private bool OnNavigateBack()
	{
		Close();
		return true;
	}

	private void RegisterProductPage<T>(T page, out T member) where T : ProductPage
	{
		member = page;
		member.OnOpened += HandlePageOpened;
		member.OnClosed += HandlePageClosed;
	}

	private static IEnumerator<IAsyncJobResult> Job_OpenToProductPage(long pmtProductId)
	{
		StoreManager storeManager = StoreManager.Get();
		if (storeManager == null)
		{
			yield return new JobFailedResult("[Shop.OpenToProductPage] Cannot open product because StoreManager is unavailable");
		}
		while (!storeManager.IsOpen(printStatus: false))
		{
			yield return null;
		}
		storeManager.StartGeneralTransaction();
		if (pmtProductId == 0L)
		{
			yield return new JobFailedResult("[Shop.OpenToProductPage] Must provide a PMT product Id");
		}
		while (storeManager.Catalog.TiersChangeCount == 0L)
		{
			yield return null;
		}
		while (s_instance == null)
		{
			yield return null;
		}
		ProductDataModel product = storeManager.Catalog.Products.FirstOrDefault((ProductDataModel p) => p.PmtId == pmtProductId);
		if (product == null)
		{
			yield return new JobFailedResult("[Shop.OpenToProductPage] Unable to find product {0} in catalog", pmtProductId);
		}
		ProductDataModel baseProductFromPmtProductId = s_instance.GetBaseProductFromPmtProductId(pmtProductId);
		if (baseProductFromPmtProductId != null)
		{
			if (storeManager.Catalog.VirtualCurrencyProductItem == baseProductFromPmtProductId)
			{
				float amountOfCurrencyInProduct = ShopUtils.GetAmountOfCurrencyInProduct(product, CurrencyType.RUNESTONES);
				s_instance.OpenVirtualCurrencyPurchase(amountOfCurrencyInProduct);
			}
			else if (storeManager.Catalog.BoosterCurrencyProductItem == baseProductFromPmtProductId)
			{
				float amountOfCurrencyInProduct2 = ShopUtils.GetAmountOfCurrencyInProduct(product, CurrencyType.ARCANE_ORBS);
				s_instance.OpenBoosterCurrencyPurchase(amountOfCurrencyInProduct2);
			}
			else
			{
				s_instance.OpenProductPage(baseProductFromPmtProductId, product);
			}
		}
		else
		{
			s_instance.OpenProductPage(product);
		}
		if (s_instance.m_suppressBoxOpen)
		{
			yield break;
		}
		while (!s_instance.IsOpen())
		{
			yield return null;
		}
		while (!s_instance.m_browser.IsReady() || s_instance.m_browser.IsLayoutDirty())
		{
			yield return null;
		}
		if (!s_instance.IsOpen())
		{
			yield break;
		}
		ShopSlot shopSlot = null;
		foreach (ShopSection activeSection in s_instance.m_browser.GetActiveSections())
		{
			ShopBrowserButtonDataModel shopBrowserButtonDataModel = activeSection.GetTierDataModel().BrowserButtons.FirstOrDefault((ShopBrowserButtonDataModel b) => b.DisplayProduct == product || b.DisplayProduct.Variants.Contains(product));
			if (shopBrowserButtonDataModel != null)
			{
				int index = activeSection.GetTierDataModel().BrowserButtons.IndexOf(shopBrowserButtonDataModel);
				shopSlot = activeSection.GetSortedEnabledSlots().ElementAtOrDefault(index);
				break;
			}
		}
		if (shopSlot == null)
		{
			Log.Store.PrintWarning("Product {0} not found on landing page", pmtProductId);
		}
		else
		{
			s_instance.m_browserScroller.CenterObjectInView(shopSlot.gameObject, 0f, null, iTween.EaseType.easeInExpo, 0.2f, blockInputWhileScrolling: true);
		}
	}

	private ProductDataModel GetBaseProductFromPmtProductId(long pmtProductId)
	{
		ProductDataModel productDataModel = null;
		foreach (ProductTierDataModel tier in StoreManager.Get().Catalog.Tiers)
		{
			productDataModel = tier.BrowserButtons.FirstOrDefault((ShopBrowserButtonDataModel b) => b.DisplayProduct.Variants.Any((ProductDataModel v) => v.PmtId == pmtProductId))?.DisplayProduct;
			if (productDataModel != null)
			{
				break;
			}
		}
		if (productDataModel == null)
		{
			foreach (ProductDataModel item in new List<ProductDataModel>
			{
				StoreManager.Get().Catalog.VirtualCurrencyProductItem,
				StoreManager.Get().Catalog.BoosterCurrencyProductItem
			})
			{
				if (item != null && item.Variants.Any((ProductDataModel v) => v.PmtId == pmtProductId))
				{
					return item;
				}
			}
			return productDataModel;
		}
		return productDataModel;
	}

	private IEnumerator SendShopVisitTelemetry()
	{
		float startTime = Time.time;
		while (true)
		{
			if (this == null || m_browser == null)
			{
				yield break;
			}
			if ((m_browser.IsReady() && !m_browser.IsLayoutDirty()) || Time.time - startTime >= 20f)
			{
				break;
			}
			yield return null;
		}
		List<ShopCard> list = new List<ShopCard>();
		foreach (ShopSection activeSection in m_browser.GetActiveSections())
		{
			foreach (ShopSlot sortedEnabledSlot in activeSection.GetSortedEnabledSlots())
			{
				ShopCard shopCardTelemetry = sortedEnabledSlot.GetShopCardTelemetry();
				if (shopCardTelemetry.HasProduct)
				{
					list.Add(shopCardTelemetry);
				}
			}
		}
		TelemetryManager.Client().SendShopVisit(list);
	}

	private bool IsReadyToRequestVirtualCurrencyBalances()
	{
		if (!ShopUtils.IsVirtualCurrencyEnabled())
		{
			return false;
		}
		if (!HearthstoneCheckout.IsAvailable() || HearthstoneCheckout.IsClientCreationInProgress())
		{
			return false;
		}
		if (StoreManager.Get().GetCurrentStore() != null)
		{
			return true;
		}
		if (Box.Get() == null || Box.Get().GetState() == Box.State.OPEN)
		{
			return false;
		}
		return true;
	}
}
