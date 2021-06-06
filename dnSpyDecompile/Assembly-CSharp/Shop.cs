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

// Token: 0x020006B5 RID: 1717
public class Shop : MonoBehaviour, IStore
{
	// Token: 0x06005FF1 RID: 24561 RVA: 0x001F47D4 File Offset: 0x001F29D4
	protected virtual void Start()
	{
		Shop.s_instance = this;
		this.m_cameraMasks = base.GetComponentsInChildren<CameraMask>(true);
		StoreManager storeManager = StoreManager.Get();
		storeManager.RegisterSuccessfulPurchaseListener(new Action<Network.Bundle, PaymentMethod>(this.HandleSuccessfulPurchase));
		storeManager.RegisterSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.HandleSuccessfulPurchaseAck));
		this.m_shopBrowserRef.RegisterReadyListener<ShopBrowser>(delegate(ShopBrowser page)
		{
			this.m_browser = page;
			this.m_browserWidgetTemplate = page.GetComponent<WidgetTemplate>();
		});
		this.m_vcPageRef.RegisterReadyListener<VirtualCurrencyPurchasePage>(delegate(VirtualCurrencyPurchasePage page)
		{
			this.RegisterProductPage<VirtualCurrencyPurchasePage>(page, out this.m_vcPage);
		});
		this.m_bcPageRef.RegisterReadyListener<CurrencyConversionPage>(delegate(CurrencyConversionPage page)
		{
			this.RegisterProductPage<CurrencyConversionPage>(page, out this.m_bcPage);
		});
		this.m_productPageContainerRef.RegisterReadyListener<ProductPageContainer>(delegate(ProductPageContainer page)
		{
			this.m_productPageContainer = page;
			this.m_productPageContainer.OnOpened += this.HandlePageOpened;
			this.m_productPageContainer.OnClosed += this.HandlePageClosed;
		});
		this.m_quantityPromptRef.RegisterReadyListener<StoreQuantityPrompt>(delegate(StoreQuantityPrompt page)
		{
			this.m_quantityPrompt = page;
		});
		this.m_shopData = new ShopDataModel();
		this.m_shopData.VirtualCurrencyBalance = this.GetCurrencyBalanceDataModel(CurrencyType.RUNESTONES);
		this.m_shopData.BoosterCurrencyBalance = this.GetCurrencyBalanceDataModel(CurrencyType.ARCANE_ORBS);
		this.m_shopData.GoldBalance = this.GetCurrencyBalanceDataModel(CurrencyType.GOLD);
		this.m_shopData.DustBalance = this.GetCurrencyBalanceDataModel(CurrencyType.DUST);
		this.m_shopData.VirtualCurrency = ProductFactory.CreateEmptyProductDataModel();
		this.m_shopData.BoosterCurrency = ProductFactory.CreateEmptyProductDataModel();
		foreach (CurrencyCache currencyCache in this.GetAllCurrencyCaches(true))
		{
			currencyCache.OnFirstCache += this.HandleOnCurrencyFirstCached;
			currencyCache.OnBalanceChanged += this.HandleOnCurrencyBalanceChanged;
		}
		GlobalDataContext.Get().BindDataModel(this.m_shopData);
		this.m_widget = base.GetComponent<WidgetTemplate>();
		if (this.m_widget != null)
		{
			this.m_widget.RegisterEventListener(new Widget.EventListenerDelegate(this.HandleWidgetEvent));
			this.m_widget.BindDataModel(this.m_shopData, false);
		}
		NetCache.Get().RegisterGoldBalanceListener(new NetCache.DelGoldBalanceListener(this.HandleGoldBalanceUpdate));
		NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheArcaneDustBalance), new Action(this.HandleDustBalanceUpdate));
		this.CurrencyBalanceChanged += delegate(CurrencyBalanceChangedEventArgs _)
		{
			this.TryNextAutoPurchase();
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

	// Token: 0x06005FF2 RID: 24562 RVA: 0x001F4A20 File Offset: 0x001F2C20
	protected virtual void OnDestroy()
	{
		if (Shop.s_instance == this)
		{
			Shop.s_instance = null;
		}
		foreach (CurrencyCache currencyCache in this.GetAllCurrencyCaches(true))
		{
			currencyCache.OnFirstCache -= this.HandleOnCurrencyFirstCached;
			currencyCache.OnBalanceChanged -= this.CurrencyBalanceChanged;
		}
		StoreManager storeManager = StoreManager.Get();
		if (storeManager != null)
		{
			storeManager.RemoveSuccessfulPurchaseListener(new Action<Network.Bundle, PaymentMethod>(this.HandleSuccessfulPurchase));
			storeManager.RemoveSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.HandleSuccessfulPurchaseAck));
		}
		NetCache netCache = NetCache.Get();
		if (netCache != null)
		{
			netCache.RemoveGoldBalanceListener(new NetCache.DelGoldBalanceListener(this.HandleGoldBalanceUpdate));
			netCache.RemoveUpdatedListener(typeof(NetCache.NetCacheArcaneDustBalance), new Action(this.HandleDustBalanceUpdate));
		}
	}

	// Token: 0x06005FF3 RID: 24563 RVA: 0x001F4AFC File Offset: 0x001F2CFC
	protected virtual void Update()
	{
		if (this.IsReadyToRequestVirtualCurrencyBalances())
		{
			this.RequestVirtualBalanceIfNeeded(CurrencyType.RUNESTONES);
			this.RequestVirtualBalanceIfNeeded(CurrencyType.ARCANE_ORBS);
		}
		if (StoreManager.Get().Catalog.TiersChangeCount != this.m_tiersChangeCountAtLastRefresh && StoreManager.Get().IsOpen(false))
		{
			this.RefreshContent();
		}
	}

	// Token: 0x06005FF4 RID: 24564 RVA: 0x001F4B49 File Offset: 0x001F2D49
	public bool IsReady()
	{
		return Shop.s_instance != null;
	}

	// Token: 0x06005FF5 RID: 24565 RVA: 0x001F4B56 File Offset: 0x001F2D56
	public static Shop Get()
	{
		return Shop.s_instance;
	}

	// Token: 0x14000043 RID: 67
	// (add) Token: 0x06005FF6 RID: 24566 RVA: 0x001F4B60 File Offset: 0x001F2D60
	// (remove) Token: 0x06005FF7 RID: 24567 RVA: 0x001F4B98 File Offset: 0x001F2D98
	public event Action OnOpened;

	// Token: 0x14000044 RID: 68
	// (add) Token: 0x06005FF8 RID: 24568 RVA: 0x001F4BD0 File Offset: 0x001F2DD0
	// (remove) Token: 0x06005FF9 RID: 24569 RVA: 0x001F4C08 File Offset: 0x001F2E08
	public event Action<StoreClosedArgs> OnClosed;

	// Token: 0x14000045 RID: 69
	// (add) Token: 0x06005FFA RID: 24570 RVA: 0x001F4C40 File Offset: 0x001F2E40
	// (remove) Token: 0x06005FFB RID: 24571 RVA: 0x001F4C78 File Offset: 0x001F2E78
	public event Action OnOpenCompleted;

	// Token: 0x14000046 RID: 70
	// (add) Token: 0x06005FFC RID: 24572 RVA: 0x001F4CB0 File Offset: 0x001F2EB0
	// (remove) Token: 0x06005FFD RID: 24573 RVA: 0x001F4CE8 File Offset: 0x001F2EE8
	public event Action OnCloseCompleted;

	// Token: 0x14000047 RID: 71
	// (add) Token: 0x06005FFE RID: 24574 RVA: 0x001F4D20 File Offset: 0x001F2F20
	// (remove) Token: 0x06005FFF RID: 24575 RVA: 0x001F4D58 File Offset: 0x001F2F58
	public event Action OnReady;

	// Token: 0x14000048 RID: 72
	// (add) Token: 0x06006000 RID: 24576 RVA: 0x001F4D90 File Offset: 0x001F2F90
	// (remove) Token: 0x06006001 RID: 24577 RVA: 0x001F4DC8 File Offset: 0x001F2FC8
	public event Action<ProductPage> OnProductPageChanged;

	// Token: 0x14000049 RID: 73
	// (add) Token: 0x06006002 RID: 24578 RVA: 0x001F4E00 File Offset: 0x001F3000
	// (remove) Token: 0x06006003 RID: 24579 RVA: 0x001F4E38 File Offset: 0x001F3038
	public event Action<BuyProductEventArgs> OnProductPurchaseAttempt;

	// Token: 0x1400004A RID: 74
	// (add) Token: 0x06006004 RID: 24580 RVA: 0x001F4E70 File Offset: 0x001F3070
	// (remove) Token: 0x06006005 RID: 24581 RVA: 0x001F4EA8 File Offset: 0x001F30A8
	public event Action<CurrencyBalanceChangedEventArgs> CurrencyBalanceChanged;

	// Token: 0x1400004B RID: 75
	// (add) Token: 0x06006006 RID: 24582 RVA: 0x001F4EE0 File Offset: 0x001F30E0
	// (remove) Token: 0x06006007 RID: 24583 RVA: 0x001F4F18 File Offset: 0x001F3118
	public event Action OnProductPageClosed;

	// Token: 0x06006008 RID: 24584 RVA: 0x001F4F4D File Offset: 0x001F314D
	public bool IsOpen()
	{
		return this.m_isOpen;
	}

	// Token: 0x170005C1 RID: 1473
	// (get) Token: 0x06006009 RID: 24585 RVA: 0x001F4F55 File Offset: 0x001F3155
	// (set) Token: 0x0600600A RID: 24586 RVA: 0x001F4F5D File Offset: 0x001F315D
	public ProductDataModel CurrentProduct { get; private set; }

	// Token: 0x170005C2 RID: 1474
	// (get) Token: 0x0600600B RID: 24587 RVA: 0x001F4F66 File Offset: 0x001F3166
	// (set) Token: 0x0600600C RID: 24588 RVA: 0x001F4F6E File Offset: 0x001F316E
	public ProductPage CurrentProductPage
	{
		get
		{
			return this.m_currentProductPage;
		}
		private set
		{
			if (this.m_currentProductPage == value)
			{
				return;
			}
			this.m_currentProductPage = value;
			if (this.OnProductPageChanged != null)
			{
				this.OnProductPageChanged(this.m_currentProductPage);
			}
		}
	}

	// Token: 0x170005C3 RID: 1475
	// (get) Token: 0x0600600D RID: 24589 RVA: 0x001F4F9F File Offset: 0x001F319F
	public ShopDataModel ShopData
	{
		get
		{
			return this.m_shopData;
		}
	}

	// Token: 0x170005C4 RID: 1476
	// (get) Token: 0x0600600E RID: 24590 RVA: 0x001F4FA7 File Offset: 0x001F31A7
	public ShopBrowser Browser
	{
		get
		{
			return this.m_browser;
		}
	}

	// Token: 0x0600600F RID: 24591 RVA: 0x001F4FAF File Offset: 0x001F31AF
	public PriceDataModel GetCurrencyBalanceDataModel(CurrencyType currency)
	{
		return this.GetCurrencyCache(currency).priceDataModel;
	}

	// Token: 0x06006010 RID: 24592 RVA: 0x001F4FC0 File Offset: 0x001F31C0
	public bool IsCloseDisabled()
	{
		HearthstoneCheckout hearthstoneCheckout;
		return (HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout) && hearthstoneCheckout.CurrentState == HearthstoneCheckout.State.InProgress) || !StoreManager.Get().CanTapOutConfirmationUI();
	}

	// Token: 0x06006011 RID: 24593 RVA: 0x001F4FF0 File Offset: 0x001F31F0
	public void Open()
	{
		if (this.m_isOpen)
		{
			return;
		}
		this.m_isOpen = true;
		ShownUIMgr.Get().SetShownUI(ShownUIMgr.UI_WINDOW.GENERAL_STORE);
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.STORE
		});
		StoreManager.Get().Catalog.TryRefreshStaleProductAvailability();
		this.RefreshContent();
		base.gameObject.SetActive(true);
		this.m_browser.gameObject.SetActive(!this.m_suppressBoxOpen);
		if (!this.m_suppressBoxOpen)
		{
			this.m_isAnimatingOpenOrClose = true;
			this.SetMasking(true);
			this.m_shopStateController.SetState("OPEN");
			this.UpdateScrollerEnabled();
			base.StartCoroutine(this.SendShopVisitTelemetry());
		}
		if (this.OnOpened != null)
		{
			this.OnOpened();
		}
	}

	// Token: 0x06006012 RID: 24594 RVA: 0x001F50CB File Offset: 0x001F32CB
	public void Close()
	{
		this.Close(false);
	}

	// Token: 0x06006013 RID: 24595 RVA: 0x001F50D4 File Offset: 0x001F32D4
	public void Close(bool forceClose)
	{
		if (!forceClose && this.IsCloseDisabled())
		{
			return;
		}
		if (!this.m_isOpen)
		{
			return;
		}
		if (this.m_productPageContainer != null)
		{
			this.m_productPageContainer.Close();
		}
		this.CurrentProduct = null;
		this.CancelAutoPurchases();
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		if (ShownUIMgr.Get() != null)
		{
			ShownUIMgr.Get().ClearShownUI();
		}
		PresenceMgr.Get().SetPrevStatus();
		this.m_isOpen = false;
		if (!this.m_suppressBoxOpen)
		{
			this.m_isAnimatingOpenOrClose = true;
			this.m_shopStateController.SetState("CLOSED");
		}
		this.m_suppressBoxOpen = false;
		this.UpdateScrollerEnabled();
		this.SetMasking(true);
		if (this.OnClosed != null)
		{
			this.OnClosed(new StoreClosedArgs(false));
		}
	}

	// Token: 0x06006014 RID: 24596 RVA: 0x001F51A0 File Offset: 0x001F33A0
	public void BlockInterface(bool blocked)
	{
		if (blocked)
		{
			this.m_widget.TriggerEvent("SHOP_BLOCK_INTERFACE", new Widget.TriggerEventParameters
			{
				IgnorePlaymaker = true,
				NoDownwardPropagation = false
			});
			return;
		}
		this.m_widget.TriggerEvent("SHOP_UNBLOCK_INTERFACE", new Widget.TriggerEventParameters
		{
			IgnorePlaymaker = true,
			NoDownwardPropagation = false
		});
	}

	// Token: 0x06006015 RID: 24597 RVA: 0x001F5208 File Offset: 0x001F3408
	public bool CanSafelyOpenCurrencyPage()
	{
		if (this.m_vcPage.IsAnimating)
		{
			Log.Store.PrintDebug("Cannot open currency page while runestones page is still animating.", Array.Empty<object>());
			return false;
		}
		if (this.m_bcPage.IsAnimating)
		{
			Log.Store.PrintDebug("Cannot open currency page while arcane orbs page is still animating.", Array.Empty<object>());
			return false;
		}
		if (PopupDisplayManager.Get() != null && PopupDisplayManager.Get().IsShowing)
		{
			Log.Store.PrintDebug("Cannot open currency page while PopupDisplayManager is showing popup.", Array.Empty<object>());
			return false;
		}
		if (StoreManager.Get() != null && StoreManager.Get().IsPromptShowing)
		{
			Log.Store.PrintDebug("Cannot open currency page while StoreManager is showing popup.", Array.Empty<object>());
			return false;
		}
		return true;
	}

	// Token: 0x06006016 RID: 24598 RVA: 0x001F52B0 File Offset: 0x001F34B0
	public void OpenVirtualCurrencyPurchase(float desiredPurchaseAmount = 0f, bool rememberLastPage = false)
	{
		if (this.m_shopData.VirtualCurrency == null || this.m_shopData.VirtualCurrency == ProductFactory.CreateEmptyProductDataModel())
		{
			Log.Store.PrintError("No valid runestone products received.", Array.Empty<object>());
			return;
		}
		if (this.m_shopData.VirtualCurrency.Availability != ProductAvailability.CAN_PURCHASE)
		{
			Log.Store.PrintError("Runestones not available for purchase", Array.Empty<object>());
			return;
		}
		if (this.m_vcPage.IsOpen)
		{
			Log.Store.PrintDebug("Cannot open runestone purchase page while already open.", Array.Empty<object>());
			return;
		}
		this.CleanUpPagesForCurrencyPage(rememberLastPage);
		if (this.m_vcPage != null)
		{
			this.m_vcPage.OpenToSKU(desiredPurchaseAmount, rememberLastPage);
		}
	}

	// Token: 0x06006017 RID: 24599 RVA: 0x001F5360 File Offset: 0x001F3560
	public void OpenBoosterCurrencyPurchase(float desiredPurchaseAmount = 0f, bool rememberLastPage = false)
	{
		if (this.m_shopData.BoosterCurrency == null || this.m_shopData.BoosterCurrency == ProductFactory.CreateEmptyProductDataModel())
		{
			Log.Store.PrintError("No valid Arcane Orb product received.", Array.Empty<object>());
			return;
		}
		if (this.m_shopData.BoosterCurrency.Availability != ProductAvailability.CAN_PURCHASE)
		{
			Log.Store.PrintError("Arcane Orbs not available for purchase", Array.Empty<object>());
			return;
		}
		if (this.m_bcPage.IsOpen)
		{
			Log.Store.PrintDebug("Cannot open arcane orb purchase page while already open.", Array.Empty<object>());
			return;
		}
		if (this.GetCurrencyCache(CurrencyType.ARCANE_ORBS).NeedsRefresh())
		{
			if (DialogManager.Get() != null)
			{
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				popupInfo.m_text = GameStrings.Format("GLUE_STORE_FAIL_CURRENCY_BALANCE", Array.Empty<object>());
				popupInfo.m_showAlertIcon = true;
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
				DialogManager.Get().ShowPopup(popupInfo);
			}
			return;
		}
		float cachedBalance = (float)ShopUtils.GetCachedBalance(CurrencyType.ARCANE_ORBS);
		float amountOfCurrencyInProduct = ShopUtils.GetAmountOfCurrencyInProduct(this.m_shopData.BoosterCurrency, CurrencyType.ARCANE_ORBS);
		if (cachedBalance + amountOfCurrencyInProduct > 9999f)
		{
			if (DialogManager.Get() != null)
			{
				AlertPopup.PopupInfo popupInfo2 = new AlertPopup.PopupInfo();
				popupInfo2.m_headerText = GameStrings.Format("GLUE_ARCANE_ORBS_CAP_HEADER", Array.Empty<object>());
				popupInfo2.m_text = GameStrings.Format("GLUE_ARCANE_ORBS_CAP_BODY", new object[]
				{
					9999
				});
				popupInfo2.m_showAlertIcon = true;
				popupInfo2.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
				DialogManager.Get().ShowPopup(popupInfo2);
			}
			return;
		}
		this.CleanUpPagesForCurrencyPage(rememberLastPage);
		if (this.m_bcPage != null)
		{
			this.m_bcPage.OpenToSKU(desiredPurchaseAmount);
		}
	}

	// Token: 0x06006018 RID: 24600 RVA: 0x001F54E3 File Offset: 0x001F36E3
	public static void OpenToProductPageWhenReady(long pmtProductId, bool suppressBox)
	{
		Shop.s_instance.m_suppressBoxOpen = suppressBox;
		Processor.QueueJob("OpenToProductPage", Shop.Job_OpenToProductPage(pmtProductId), Array.Empty<IJobDependency>());
	}

	// Token: 0x06006019 RID: 24601 RVA: 0x001F5508 File Offset: 0x001F3708
	public static void OpenToTavernPassPageWhenReady()
	{
		long pmtProductId;
		if (StoreManager.Get().TryGetBonusProductBundleId(ProductType.PRODUCT_TYPE_PROGRESSION_BONUS, out pmtProductId))
		{
			Shop.OpenToProductPageWhenReady(pmtProductId, true);
			return;
		}
		Shop.OpenTavernPassErrorPopup();
	}

	// Token: 0x0600601A RID: 24602 RVA: 0x001F5534 File Offset: 0x001F3734
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

	// Token: 0x0600601B RID: 24603 RVA: 0x001F5580 File Offset: 0x001F3780
	internal void OpenProductPage(ProductDataModel product, ProductDataModel variant = null)
	{
		if (product == null || product == ProductFactory.CreateEmptyProductDataModel())
		{
			Log.Store.PrintError("Shop cannot open null or empty product", Array.Empty<object>());
			return;
		}
		Log.Store.PrintDebug("[Shop.OpenProductPage] display product {0}", new object[]
		{
			product.Name
		});
		this.CurrentProduct = product;
		if (this.m_productPageContainer != null)
		{
			this.m_productPageContainer.InitializeTempInstances();
			this.m_productPageContainer.Open(product, variant);
		}
		this.MarkProductAsSeen(product);
	}

	// Token: 0x0600601C RID: 24604 RVA: 0x001F5600 File Offset: 0x001F3800
	public void AttemptToPurchaseProduct(ProductDataModel product, PriceDataModel price, int quantity = 1)
	{
		this.CancelAutoPurchases();
		Shop.PurchaseOrder purchaseOrder = new Shop.PurchaseOrder
		{
			m_product = product,
			m_price = price,
			m_quantity = quantity
		};
		while (purchaseOrder != null)
		{
			this.m_autoPurchaseStack.Push(purchaseOrder);
			purchaseOrder = this.GetPrerequisitePurchase(purchaseOrder);
			if (purchaseOrder == null)
			{
				break;
			}
			if (purchaseOrder.m_product == null)
			{
				Log.Store.PrintError("Purchase could not be started", Array.Empty<object>());
				return;
			}
		}
		purchaseOrder = this.m_autoPurchaseStack.Pop();
		this.ExecutePurchaseOrder(purchaseOrder);
	}

	// Token: 0x0600601D RID: 24605 RVA: 0x001F567A File Offset: 0x001F387A
	public void RefreshWallet()
	{
		this.UpdateCurrencyBalance(CurrencyType.GOLD, ShopUtils.GetCachedBalance(CurrencyType.GOLD));
		this.UpdateCurrencyBalance(CurrencyType.DUST, ShopUtils.GetCachedBalance(CurrencyType.DUST));
	}

	// Token: 0x0600601E RID: 24606 RVA: 0x001F5696 File Offset: 0x001F3896
	public void DisplayCurrencyBalance(CurrencyType currency, long balance)
	{
		this.GetCurrencyCache(currency).UpdateDisplayText(balance.ToString());
	}

	// Token: 0x0600601F RID: 24607 RVA: 0x001F56AB File Offset: 0x001F38AB
	public void Unload()
	{
		this.Close(true);
	}

	// Token: 0x170005C5 RID: 1477
	// (get) Token: 0x06006020 RID: 24608 RVA: 0x001F56B4 File Offset: 0x001F38B4
	public StoreQuantityPrompt QuantityPrompt
	{
		get
		{
			return this.m_quantityPrompt;
		}
	}

	// Token: 0x06006021 RID: 24609 RVA: 0x001F56BC File Offset: 0x001F38BC
	public bool WillAutoPurchase()
	{
		return this.m_autoPurchaseStack.Count > 0;
	}

	// Token: 0x06006022 RID: 24610 RVA: 0x001F56CC File Offset: 0x001F38CC
	public IEnumerable<CurrencyType> GetVisibleCurrencies()
	{
		if (ShopUtils.IsVirtualCurrencyEnabled())
		{
			return new CurrencyType[]
			{
				CurrencyType.GOLD,
				CurrencyType.RUNESTONES,
				CurrencyType.ARCANE_ORBS
			};
		}
		return new CurrencyType[]
		{
			CurrencyType.GOLD
		};
	}

	// Token: 0x06006023 RID: 24611 RVA: 0x001F56F4 File Offset: 0x001F38F4
	private void MarkProductAsSeen(ProductDataModel product)
	{
		if (!product.Tags.Remove("new"))
		{
			return;
		}
		string text = Options.Get().GetString(Option.LATEST_SEEN_SHOP_PRODUCT_LIST);
		List<string> list = new List<string>(text.Split(new char[]
		{
			':'
		}));
		string item = product.PmtId.ToString();
		if (!list.Contains(item))
		{
			list.Add(item);
		}
		if (this.m_shopData.Tiers.Any((ProductTierDataModel t) => t.BrowserButtons.Count > 0))
		{
			for (int i = 0; i < list.Count; i++)
			{
				long pmtId = 0L;
				Func<ShopBrowserButtonDataModel, bool> <>9__2;
				if (!long.TryParse(list[i], out pmtId) || !this.m_shopData.Tiers.Any(delegate(ProductTierDataModel t)
				{
					IEnumerable<ShopBrowserButtonDataModel> browserButtons = t.BrowserButtons;
					Func<ShopBrowserButtonDataModel, bool> predicate;
					if ((predicate = <>9__2) == null)
					{
						predicate = (<>9__2 = ((ShopBrowserButtonDataModel b) => b.DisplayProduct.PmtId == pmtId));
					}
					return browserButtons.Any(predicate);
				}))
				{
					list.RemoveAt(i--);
				}
			}
		}
		text = string.Join(":", list.ToArray());
		Options.Get().SetString(Option.LATEST_SEEN_SHOP_PRODUCT_LIST, text);
	}

	// Token: 0x06006024 RID: 24612 RVA: 0x001F5810 File Offset: 0x001F3A10
	private bool ContainsNewlyDisplayedItems()
	{
		if (StoreManager.Get() == null || StoreManager.Get().IsVintageStoreEnabled() || Box.Get() == null || Box.Get().IsTutorial())
		{
			return false;
		}
		List<string> listOfNewProducts = this.GetListOfNewProducts();
		string @string = Options.Get().GetString(Option.LATEST_DISPLAYED_SHOP_PRODUCT_LIST);
		List<string> list = new List<string>();
		list.AddRange(@string.Split(new char[]
		{
			':'
		}));
		foreach (string item in listOfNewProducts)
		{
			if (!list.Contains(item))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006025 RID: 24613 RVA: 0x001F58CC File Offset: 0x001F3ACC
	private void MarkShopAsSeen()
	{
		if (this.m_shopData.HasNewItems)
		{
			this.m_shopData.HasNewItems = false;
			List<string> listOfNewProducts = this.GetListOfNewProducts();
			string val = string.Join(":", listOfNewProducts.ToArray());
			Options.Get().SetString(Option.LATEST_DISPLAYED_SHOP_PRODUCT_LIST, val);
		}
	}

	// Token: 0x06006026 RID: 24614 RVA: 0x001F5918 File Offset: 0x001F3B18
	private List<string> GetListOfNewProducts()
	{
		List<string> productIds = new List<string>();
		Action<ShopBrowserButtonDataModel> <>9__1;
		this.m_shopData.Tiers.ForEach(delegate(ProductTierDataModel t)
		{
			IEnumerable<ShopBrowserButtonDataModel> browserButtons = t.BrowserButtons;
			Action<ShopBrowserButtonDataModel> func;
			if ((func = <>9__1) == null)
			{
				func = (<>9__1 = delegate(ShopBrowserButtonDataModel button)
				{
					if (button.DisplayProduct.Tags.Contains("new"))
					{
						productIds.Add(button.DisplayProduct.PmtId.ToString());
					}
				});
			}
			browserButtons.ForEach(func);
		});
		return productIds;
	}

	// Token: 0x06006027 RID: 24615 RVA: 0x001F5958 File Offset: 0x001F3B58
	private void ExecutePurchaseOrder(Shop.PurchaseOrder purchase)
	{
		if (purchase == null || purchase.m_product == null || purchase.m_price == null)
		{
			Log.Store.PrintError("ExecutePurchaseOrder failed. PurchaseOrder invalid.", Array.Empty<object>());
			return;
		}
		if (purchase.m_product.Tags.Contains("runestones") && this.m_autoPurchaseStack.Count > 0)
		{
			Shop.PurchaseOrder nextBuyWithVC = this.m_autoPurchaseStack.Last((Shop.PurchaseOrder p) => p.m_price != null && p.m_price.Currency == CurrencyType.RUNESTONES);
			if (nextBuyWithVC == null)
			{
				Log.Store.PrintError("Unnecessary VC purchase planned; skipping", Array.Empty<object>());
				this.ExecutePurchaseOrder(this.m_autoPurchaseStack.Pop());
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
					this.BlockInterface(false);
					if (response == AlertPopup.Response.CONFIRM)
					{
						long deficitForPurchase = this.GetDeficitForPurchase(nextBuyWithVC);
						this.OpenVirtualCurrencyPurchase((float)deficitForPurchase, true);
					}
				}
			};
			this.CancelAutoPurchases();
			this.BlockInterface(true);
			DialogManager.Get().ShowPopup(info);
			return;
		}
		else
		{
			BuyProductEventArgs buyProductArgs = purchase.m_product.GetBuyProductArgs(purchase.m_price, purchase.m_quantity);
			if (buyProductArgs == null)
			{
				Log.Store.PrintError("ExecutePurchaseOrder failed. No valid BuyProductEventArgs for product.", Array.Empty<object>());
				return;
			}
			if (this.OnProductPurchaseAttempt != null)
			{
				this.OnProductPurchaseAttempt(buyProductArgs);
				return;
			}
			Log.Store.PrintError("ExecutePurchaseOrder failed. No OnProductPurchaseAttempt event handler registered.", Array.Empty<object>());
			return;
		}
	}

	// Token: 0x06006028 RID: 24616 RVA: 0x001F5B09 File Offset: 0x001F3D09
	private void CleanUpPagesForCurrencyPage(bool rememberLastPage)
	{
		if (!rememberLastPage)
		{
			this.m_reopenPageCall = null;
		}
		this.CloseCurrentPage(rememberLastPage);
	}

	// Token: 0x06006029 RID: 24617 RVA: 0x001F5B1C File Offset: 0x001F3D1C
	private void CloseCurrentPage(bool reopenLater)
	{
		ProductPage currentProductPage = this.CurrentProductPage;
		if (!currentProductPage)
		{
			return;
		}
		if (currentProductPage == this.m_vcPage || currentProductPage == this.m_bcPage)
		{
			currentProductPage.Close();
			if (reopenLater)
			{
				this.m_reopenPageCall = new Shop.ReopenCallback(currentProductPage.Open);
				return;
			}
		}
		else if (currentProductPage == this.m_productPageContainer.GetCurrentProductPage())
		{
			this.m_productPageContainer.Close();
			if (reopenLater)
			{
				this.m_reopenPageCall = new Shop.ReopenCallback(this.m_productPageContainer.Open);
			}
		}
	}

	// Token: 0x0600602A RID: 24618 RVA: 0x001F5BAA File Offset: 0x001F3DAA
	private void ReopenClosedPage()
	{
		if (this.m_reopenPageCall != null)
		{
			this.m_reopenPageCall();
			this.m_reopenPageCall = null;
		}
	}

	// Token: 0x0600602B RID: 24619 RVA: 0x001F5BC6 File Offset: 0x001F3DC6
	private long GetDeficitForPurchase(Shop.PurchaseOrder purchase)
	{
		return ShopUtils.GetDeficit(new PriceDataModel
		{
			Currency = purchase.m_price.Currency,
			Amount = purchase.m_price.Amount * (float)purchase.m_quantity
		});
	}

	// Token: 0x0600602C RID: 24620 RVA: 0x001F5BFC File Offset: 0x001F3DFC
	private void HandleWidgetEvent(string eventName)
	{
		if (!(eventName == "SHOP_GO_BACK"))
		{
			if (eventName == "SHOP_SHOW_INFO")
			{
				StoreManager.Get().ShowStoreInfo();
				return;
			}
			if (eventName == "SHOP_TOGGLE_AUTOCONVERT")
			{
				this.m_shopData.AutoconvertCurrency = !this.m_shopData.AutoconvertCurrency;
				Options.Get().SetBool(Option.AUTOCONVERT_VIRTUAL_CURRENCY, this.m_shopData.AutoconvertCurrency);
				return;
			}
			if (!(eventName == "SHOP_BUY_VC"))
			{
				return;
			}
			this.OpenVirtualCurrencyPurchase(0f, true);
			return;
		}
		else
		{
			if (this.IsOpen() && !Navigation.BackStackContainsHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack)))
			{
				this.Close();
				return;
			}
			Navigation.GoBack();
			return;
		}
	}

	// Token: 0x0600602D RID: 24621 RVA: 0x001F5CAF File Offset: 0x001F3EAF
	private void RefreshContent()
	{
		this.m_browserScroller.SetScroll(0f, false, true);
		this.RefreshDataModel();
		if (this.m_browser != null)
		{
			this.m_browser.RefreshContents();
		}
	}

	// Token: 0x0600602E RID: 24622 RVA: 0x001F5CE2 File Offset: 0x001F3EE2
	private void CompleteOpen()
	{
		this.m_isAnimatingOpenOrClose = false;
		this.MarkShopAsSeen();
		if (this.OnOpenCompleted != null)
		{
			this.OnOpenCompleted();
		}
		this.SetMasking(false);
		this.UpdateScrollerEnabled();
	}

	// Token: 0x0600602F RID: 24623 RVA: 0x001F5D14 File Offset: 0x001F3F14
	private void CompleteClose()
	{
		this.m_isAnimatingOpenOrClose = false;
		if (this.OnCloseCompleted != null)
		{
			this.OnCloseCompleted();
		}
		this.UpdateScrollerEnabled();
		if (this.UnloadUnusedAssetsOnClose && HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().UnloadUnusedAssets();
		}
	}

	// Token: 0x06006030 RID: 24624 RVA: 0x001F5D68 File Offset: 0x001F3F68
	private void SetMasking(bool maskingEnabled)
	{
		if (this.m_browserWidgetTemplate != null)
		{
			this.m_browserWidgetTemplate.SetLayerOverride(maskingEnabled ? GameLayer.CameraMask : GameLayer.Default);
		}
		if (this.m_cameraMasks != null)
		{
			CameraMask[] cameraMasks = this.m_cameraMasks;
			for (int i = 0; i < cameraMasks.Length; i++)
			{
				cameraMasks[i].enabled = maskingEnabled;
			}
		}
	}

	// Token: 0x06006031 RID: 24625 RVA: 0x001F5DBC File Offset: 0x001F3FBC
	private void HandleSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		StoreManager.Get().Catalog.UpdateProductStatus();
	}

	// Token: 0x06006032 RID: 24626 RVA: 0x001F5DCD File Offset: 0x001F3FCD
	private void HandleSuccessfulPurchase(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		this.RefreshWallet();
	}

	// Token: 0x06006033 RID: 24627 RVA: 0x001F5DD5 File Offset: 0x001F3FD5
	private void HandleGoldBalanceUpdate(NetCache.NetCacheGoldBalance balance)
	{
		Log.Store.PrintDebug("Gold balance updated to {0}", new object[]
		{
			balance.GetTotal()
		});
		this.UpdateCurrencyBalance(CurrencyType.GOLD, balance.GetTotal());
	}

	// Token: 0x06006034 RID: 24628 RVA: 0x001F5E08 File Offset: 0x001F4008
	private void HandleDustBalanceUpdate()
	{
		if (NetCache.Get() != null)
		{
			long arcaneDustBalance = NetCache.Get().GetArcaneDustBalance();
			Log.Store.PrintDebug("Arcane Dust balance updated to {0}", new object[]
			{
				arcaneDustBalance
			});
			this.UpdateCurrencyBalance(CurrencyType.DUST, arcaneDustBalance);
		}
	}

	// Token: 0x06006035 RID: 24629 RVA: 0x001F5E50 File Offset: 0x001F4050
	public void RefreshDataModel()
	{
		ProductCatalog catalog = StoreManager.Get().Catalog;
		this.m_shopData.IsWild = (CollectionManager.Get() != null && CollectionManager.Get().ShouldAccountSeeStandardWild());
		this.m_shopData.Tiers.Clear();
		if (catalog.HasTestData)
		{
			this.m_shopData.Tiers.AddRange(catalog.Tiers);
		}
		else
		{
			this.m_shopData.Tiers.AddRange(from t in catalog.Tiers
			where this.ShouldDisplayTier(t)
			select t);
		}
		this.m_shopData.VirtualCurrency = (catalog.VirtualCurrencyProductItem ?? ProductFactory.CreateEmptyProductDataModel());
		this.m_shopData.BoosterCurrency = (catalog.BoosterCurrencyProductItem ?? ProductFactory.CreateEmptyProductDataModel());
		this.m_shopData.AutoconvertCurrency = Options.Get().GetBool(Option.AUTOCONVERT_VIRTUAL_CURRENCY);
		this.m_shopData.HasNewItems = this.ContainsNewlyDisplayedItems();
		this.m_tiersChangeCountAtLastRefresh = catalog.TiersChangeCount;
		this.m_shopData.TavernTicketBalance = NetCache.Get().GetArenaTicketBalance();
		this.RefreshWallet();
	}

	// Token: 0x06006036 RID: 24630 RVA: 0x001F5F61 File Offset: 0x001F4161
	private bool ShouldDisplayTier(ProductTierDataModel tier)
	{
		return tier.BrowserButtons.Any((ShopBrowserButtonDataModel b) => !b.IsFiller && ShopUtils.AreProductOrVariantsPurchasable(b.DisplayProduct));
	}

	// Token: 0x06006037 RID: 24631 RVA: 0x001F5F90 File Offset: 0x001F4190
	private Shop.PurchaseOrder GetPrerequisitePurchase(Shop.PurchaseOrder pendingPurchase)
	{
		PriceDataModel price = pendingPurchase.m_price;
		if (price.Currency == CurrencyType.REAL_MONEY)
		{
			return null;
		}
		long deficitForPurchase = this.GetDeficitForPurchase(pendingPurchase);
		if (deficitForPurchase <= 0L)
		{
			return null;
		}
		Shop.PurchaseOrder purchaseOrder = new Shop.PurchaseOrder();
		purchaseOrder.m_product = ShopUtils.FindCurrencyProduct(price.Currency, (float)deficitForPurchase);
		if (purchaseOrder.m_product == null)
		{
			Log.Store.PrintError("Unable to find product with {0} of currency {1}", new object[]
			{
				deficitForPurchase,
				price.Currency.ToString()
			});
			return purchaseOrder;
		}
		if (purchaseOrder.m_product.Items.FirstOrDefault<RewardItemDataModel>().ItemType == RewardItemType.ARCANE_ORBS && !this.m_shopData.AutoconvertCurrency)
		{
			Log.Store.PrintError("Unable to convert Booster Currency; autoconversion required", Array.Empty<object>());
			purchaseOrder.m_product = null;
			return purchaseOrder;
		}
		float amountOfCurrencyInProduct = ShopUtils.GetAmountOfCurrencyInProduct(purchaseOrder.m_product, price.Currency);
		if (amountOfCurrencyInProduct <= 0f)
		{
			Log.Store.PrintError("Invalid currency product; contains no currency", Array.Empty<object>());
			return purchaseOrder;
		}
		purchaseOrder.m_quantity = Mathf.CeilToInt((float)deficitForPurchase / amountOfCurrencyInProduct);
		purchaseOrder.m_price = purchaseOrder.m_product.Prices.FirstOrDefault<PriceDataModel>();
		return purchaseOrder;
	}

	// Token: 0x06006038 RID: 24632 RVA: 0x001F60B0 File Offset: 0x001F42B0
	private void CancelAutoPurchases()
	{
		this.m_autoPurchaseStack.Clear();
	}

	// Token: 0x06006039 RID: 24633 RVA: 0x001F60BD File Offset: 0x001F42BD
	private void HandlePageOpened()
	{
		this.UpdateScrollerEnabled();
		this.UpdateCurrentProductPage();
	}

	// Token: 0x0600603A RID: 24634 RVA: 0x001F60CB File Offset: 0x001F42CB
	private void HandlePageClosed()
	{
		this.CancelAutoPurchases();
		this.ReopenClosedPage();
		this.UpdateScrollerEnabled();
		this.UpdateCurrentProductPage();
		if (this.m_suppressBoxOpen)
		{
			this.Close();
		}
		if (this.OnProductPageClosed != null)
		{
			this.OnProductPageClosed();
		}
	}

	// Token: 0x0600603B RID: 24635 RVA: 0x001F6108 File Offset: 0x001F4308
	private void UpdateCurrentProductPage()
	{
		ProductPage currentProductPage = null;
		if (this.m_productPageContainer.IsOpen)
		{
			currentProductPage = this.m_productPageContainer.GetCurrentProductPage();
		}
		else if (this.m_vcPage.IsOpen)
		{
			currentProductPage = this.m_vcPage;
		}
		else if (this.m_bcPage.IsOpen)
		{
			currentProductPage = this.m_bcPage;
		}
		this.CurrentProductPage = currentProductPage;
	}

	// Token: 0x0600603C RID: 24636 RVA: 0x001F6164 File Offset: 0x001F4364
	private void UpdateScrollerEnabled()
	{
		bool flag = (this.m_bcPage != null && this.m_bcPage.IsOpen) || (this.m_vcPage != null && this.m_vcPage.IsOpen) || (this.m_productPageContainer != null && this.m_productPageContainer.IsOpen);
		bool flag2 = this.IsOpen() && !flag && !this.m_isAnimatingOpenOrClose;
		this.m_browserScroller.enabled = flag2;
		this.m_browserScroller.SetHideThumb(!flag2);
	}

	// Token: 0x0600603D RID: 24637 RVA: 0x001F61FC File Offset: 0x001F43FC
	private void TryNextAutoPurchase()
	{
		if (this.m_autoPurchaseStack.Count == 0)
		{
			return;
		}
		Shop.PurchaseOrder purchaseOrder = this.m_autoPurchaseStack.Peek();
		if (purchaseOrder == null)
		{
			return;
		}
		if (this.GetDeficitForPurchase(purchaseOrder) == 0L)
		{
			this.m_autoPurchaseStack.Pop();
			this.ExecutePurchaseOrder(purchaseOrder);
		}
	}

	// Token: 0x0600603E RID: 24638 RVA: 0x001F6244 File Offset: 0x001F4444
	private void RequestVirtualBalanceIfNeeded(CurrencyType currencyType)
	{
		if (!ShopUtils.IsVirtualCurrencyEnabled())
		{
			return;
		}
		if (!ShopUtils.IsCurrencyVirtual(currencyType))
		{
			Log.Store.PrintError("{0} is not a virtual currency", new object[]
			{
				currencyType
			});
			return;
		}
		CurrencyCache currencyCache = this.GetCurrencyCache(currencyType);
		if (!currencyCache.NeedsRefresh())
		{
			return;
		}
		currencyCache.TryRefresh();
	}

	// Token: 0x0600603F RID: 24639 RVA: 0x001F6298 File Offset: 0x001F4498
	private void UpdateCurrencyBalance(CurrencyType type, long balance)
	{
		this.GetCurrencyCache(type).UpdateBalance(balance);
	}

	// Token: 0x06006040 RID: 24640 RVA: 0x001F62A7 File Offset: 0x001F44A7
	private CurrencyCache GetCurrencyCache(CurrencyType type)
	{
		return StoreManager.Get().GetCurrencyCache(type);
	}

	// Token: 0x06006041 RID: 24641 RVA: 0x001F62B4 File Offset: 0x001F44B4
	private IEnumerable<CurrencyCache> GetAllCurrencyCaches(bool forceIncludeVc = false)
	{
		List<CurrencyCache> list = new List<CurrencyCache>();
		list.Add(this.GetCurrencyCache(CurrencyType.GOLD));
		list.Add(this.GetCurrencyCache(CurrencyType.DUST));
		if (forceIncludeVc || ShopUtils.IsVirtualCurrencyEnabled())
		{
			list.Add(this.GetCurrencyCache(CurrencyType.RUNESTONES));
			list.Add(this.GetCurrencyCache(CurrencyType.ARCANE_ORBS));
		}
		return list;
	}

	// Token: 0x06006042 RID: 24642 RVA: 0x001F6308 File Offset: 0x001F4508
	private void HandleOnCurrencyFirstCached()
	{
		List<Balance> list = new List<Balance>();
		foreach (CurrencyCache currencyCache in this.GetAllCurrencyCaches(false))
		{
			if (!currencyCache.IsCached())
			{
				return;
			}
			list.Add(new Balance
			{
				Name = Enum.GetName(typeof(CurrencyType), currencyCache.type).ToLowerInvariant(),
				Amount = (double)currencyCache.priceDataModel.Amount
			});
		}
		TelemetryManager.Client().SendShopBalanceAvailable(list);
	}

	// Token: 0x06006043 RID: 24643 RVA: 0x001F63B0 File Offset: 0x001F45B0
	private void HandleOnCurrencyBalanceChanged(CurrencyBalanceChangedEventArgs args)
	{
		if (this.CurrencyBalanceChanged != null)
		{
			this.CurrencyBalanceChanged(args);
		}
	}

	// Token: 0x06006044 RID: 24644 RVA: 0x001F63C6 File Offset: 0x001F45C6
	private bool OnNavigateBack()
	{
		this.Close();
		return true;
	}

	// Token: 0x06006045 RID: 24645 RVA: 0x001F63CF File Offset: 0x001F45CF
	private void RegisterProductPage<T>(T page, out T member) where T : ProductPage
	{
		member = page;
		member.OnOpened += this.HandlePageOpened;
		member.OnClosed += this.HandlePageClosed;
	}

	// Token: 0x06006046 RID: 24646 RVA: 0x001F6408 File Offset: 0x001F4608
	private static IEnumerator<IAsyncJobResult> Job_OpenToProductPage(long pmtProductId)
	{
		StoreManager storeManager = StoreManager.Get();
		if (storeManager == null)
		{
			yield return new JobFailedResult("[Shop.OpenToProductPage] Cannot open product because StoreManager is unavailable", Array.Empty<object>());
		}
		while (!storeManager.IsOpen(false))
		{
			yield return null;
		}
		storeManager.StartGeneralTransaction();
		if (pmtProductId == 0L)
		{
			yield return new JobFailedResult("[Shop.OpenToProductPage] Must provide a PMT product Id", Array.Empty<object>());
		}
		while (storeManager.Catalog.TiersChangeCount == 0L)
		{
			yield return null;
		}
		while (Shop.s_instance == null)
		{
			yield return null;
		}
		ProductDataModel product = storeManager.Catalog.Products.FirstOrDefault((ProductDataModel p) => p.PmtId == pmtProductId);
		if (product == null)
		{
			yield return new JobFailedResult("[Shop.OpenToProductPage] Unable to find product {0} in catalog", new object[]
			{
				pmtProductId
			});
		}
		ProductDataModel baseProductFromPmtProductId = Shop.s_instance.GetBaseProductFromPmtProductId(pmtProductId);
		if (baseProductFromPmtProductId != null)
		{
			if (storeManager.Catalog.VirtualCurrencyProductItem == baseProductFromPmtProductId)
			{
				float amountOfCurrencyInProduct = ShopUtils.GetAmountOfCurrencyInProduct(product, CurrencyType.RUNESTONES);
				Shop.s_instance.OpenVirtualCurrencyPurchase(amountOfCurrencyInProduct, false);
			}
			else if (storeManager.Catalog.BoosterCurrencyProductItem == baseProductFromPmtProductId)
			{
				float amountOfCurrencyInProduct2 = ShopUtils.GetAmountOfCurrencyInProduct(product, CurrencyType.ARCANE_ORBS);
				Shop.s_instance.OpenBoosterCurrencyPurchase(amountOfCurrencyInProduct2, false);
			}
			else
			{
				Shop.s_instance.OpenProductPage(baseProductFromPmtProductId, product);
			}
		}
		else
		{
			Shop.s_instance.OpenProductPage(product, null);
		}
		if (Shop.s_instance.m_suppressBoxOpen)
		{
			yield break;
		}
		while (!Shop.s_instance.IsOpen())
		{
			yield return null;
		}
		while (!Shop.s_instance.m_browser.IsReady() || Shop.s_instance.m_browser.IsLayoutDirty())
		{
			yield return null;
		}
		if (!Shop.s_instance.IsOpen())
		{
			yield break;
		}
		ShopSlot shopSlot = null;
		Func<ShopBrowserButtonDataModel, bool> <>9__1;
		foreach (ShopSection shopSection in Shop.s_instance.m_browser.GetActiveSections())
		{
			IEnumerable<ShopBrowserButtonDataModel> browserButtons = shopSection.GetTierDataModel().BrowserButtons;
			Func<ShopBrowserButtonDataModel, bool> predicate;
			if ((predicate = <>9__1) == null)
			{
				predicate = (<>9__1 = ((ShopBrowserButtonDataModel b) => b.DisplayProduct == product || b.DisplayProduct.Variants.Contains(product)));
			}
			ShopBrowserButtonDataModel shopBrowserButtonDataModel = browserButtons.FirstOrDefault(predicate);
			if (shopBrowserButtonDataModel != null)
			{
				int index = shopSection.GetTierDataModel().BrowserButtons.IndexOf(shopBrowserButtonDataModel);
				shopSlot = shopSection.GetSortedEnabledSlots().ElementAtOrDefault(index);
				break;
			}
		}
		if (shopSlot == null)
		{
			Log.Store.PrintWarning("Product {0} not found on landing page", new object[]
			{
				pmtProductId
			});
			yield break;
		}
		Shop.s_instance.m_browserScroller.CenterObjectInView(shopSlot.gameObject, 0f, null, iTween.EaseType.easeInExpo, 0.2f, true);
		yield break;
	}

	// Token: 0x06006047 RID: 24647 RVA: 0x001F6418 File Offset: 0x001F4618
	private ProductDataModel GetBaseProductFromPmtProductId(long pmtProductId)
	{
		ProductDataModel productDataModel = null;
		Func<ProductDataModel, bool> <>9__1;
		Func<ShopBrowserButtonDataModel, bool> <>9__0;
		foreach (ProductTierDataModel productTierDataModel in StoreManager.Get().Catalog.Tiers)
		{
			IEnumerable<ShopBrowserButtonDataModel> browserButtons = productTierDataModel.BrowserButtons;
			Func<ShopBrowserButtonDataModel, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = delegate(ShopBrowserButtonDataModel b)
				{
					IEnumerable<ProductDataModel> variants2 = b.DisplayProduct.Variants;
					Func<ProductDataModel, bool> predicate3;
					if ((predicate3 = <>9__1) == null)
					{
						predicate3 = (<>9__1 = ((ProductDataModel v) => v.PmtId == pmtProductId));
					}
					return variants2.Any(predicate3);
				});
			}
			ShopBrowserButtonDataModel shopBrowserButtonDataModel = browserButtons.FirstOrDefault(predicate);
			productDataModel = ((shopBrowserButtonDataModel != null) ? shopBrowserButtonDataModel.DisplayProduct : null);
			if (productDataModel != null)
			{
				break;
			}
		}
		if (productDataModel == null)
		{
			Func<ProductDataModel, bool> <>9__2;
			foreach (ProductDataModel productDataModel2 in new List<ProductDataModel>
			{
				StoreManager.Get().Catalog.VirtualCurrencyProductItem,
				StoreManager.Get().Catalog.BoosterCurrencyProductItem
			})
			{
				if (productDataModel2 != null)
				{
					IEnumerable<ProductDataModel> variants = productDataModel2.Variants;
					Func<ProductDataModel, bool> predicate2;
					if ((predicate2 = <>9__2) == null)
					{
						predicate2 = (<>9__2 = ((ProductDataModel v) => v.PmtId == pmtProductId));
					}
					if (variants.Any(predicate2))
					{
						productDataModel = productDataModel2;
						break;
					}
				}
			}
		}
		return productDataModel;
	}

	// Token: 0x06006048 RID: 24648 RVA: 0x001F6560 File Offset: 0x001F4760
	private IEnumerator SendShopVisitTelemetry()
	{
		float startTime = Time.time;
		while (!(this == null) && !(this.m_browser == null))
		{
			if ((this.m_browser.IsReady() && !this.m_browser.IsLayoutDirty()) || Time.time - startTime >= 20f)
			{
				List<ShopCard> list = new List<ShopCard>();
				foreach (ShopSection shopSection in this.m_browser.GetActiveSections())
				{
					foreach (ShopSlot shopSlot in shopSection.GetSortedEnabledSlots())
					{
						ShopCard shopCardTelemetry = shopSlot.GetShopCardTelemetry();
						if (shopCardTelemetry.HasProduct)
						{
							list.Add(shopCardTelemetry);
						}
					}
				}
				TelemetryManager.Client().SendShopVisit(list);
				yield break;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006049 RID: 24649 RVA: 0x001F6570 File Offset: 0x001F4770
	private bool IsReadyToRequestVirtualCurrencyBalances()
	{
		return ShopUtils.IsVirtualCurrencyEnabled() && HearthstoneCheckout.IsAvailable() && !HearthstoneCheckout.IsClientCreationInProgress() && (StoreManager.Get().GetCurrentStore() != null || (!(Box.Get() == null) && Box.Get().GetState() != Box.State.OPEN));
	}

	// Token: 0x04005082 RID: 20610
	[SerializeField]
	protected UIBScrollable m_browserScroller;

	// Token: 0x04005083 RID: 20611
	[SerializeField]
	protected VisualController m_shopStateController;

	// Token: 0x04005084 RID: 20612
	[SerializeField]
	protected AsyncReference m_shopBrowserRef;

	// Token: 0x04005085 RID: 20613
	[SerializeField]
	protected AsyncReference m_vcPageRef;

	// Token: 0x04005086 RID: 20614
	[SerializeField]
	protected AsyncReference m_bcPageRef;

	// Token: 0x04005087 RID: 20615
	[SerializeField]
	protected AsyncReference m_productPageContainerRef;

	// Token: 0x04005088 RID: 20616
	[SerializeField]
	protected AsyncReference m_quantityPromptRef;

	// Token: 0x04005089 RID: 20617
	private static Shop s_instance;

	// Token: 0x0400508A RID: 20618
	private ShopBrowser m_browser;

	// Token: 0x0400508B RID: 20619
	private WidgetTemplate m_browserWidgetTemplate;

	// Token: 0x0400508C RID: 20620
	private VirtualCurrencyPurchasePage m_vcPage;

	// Token: 0x0400508D RID: 20621
	private CurrencyConversionPage m_bcPage;

	// Token: 0x0400508E RID: 20622
	private ProductPageContainer m_productPageContainer;

	// Token: 0x0400508F RID: 20623
	private ProductPage m_currentProductPage;

	// Token: 0x04005090 RID: 20624
	private StoreQuantityPrompt m_quantityPrompt;

	// Token: 0x04005091 RID: 20625
	private WidgetTemplate m_widget;

	// Token: 0x04005092 RID: 20626
	private ShopDataModel m_shopData;

	// Token: 0x04005093 RID: 20627
	private Stack<Shop.PurchaseOrder> m_autoPurchaseStack = new Stack<Shop.PurchaseOrder>();

	// Token: 0x04005094 RID: 20628
	private Shop.ReopenCallback m_reopenPageCall;

	// Token: 0x04005095 RID: 20629
	private bool m_suppressBoxOpen;

	// Token: 0x04005096 RID: 20630
	protected bool m_isOpen;

	// Token: 0x04005097 RID: 20631
	private long m_tiersChangeCountAtLastRefresh;

	// Token: 0x04005098 RID: 20632
	private CameraMask[] m_cameraMasks;

	// Token: 0x04005099 RID: 20633
	private bool m_isAnimatingOpenOrClose;

	// Token: 0x0400509A RID: 20634
	private const string OPEN = "OPEN";

	// Token: 0x0400509B RID: 20635
	private const string CLOSED = "CLOSED";

	// Token: 0x0400509C RID: 20636
	private const string SHOP_GO_BACK = "SHOP_GO_BACK";

	// Token: 0x0400509D RID: 20637
	private const string SHOP_SHOW_INFO = "SHOP_SHOW_INFO";

	// Token: 0x0400509E RID: 20638
	private const string SHOP_BUY_VC = "SHOP_BUY_VC";

	// Token: 0x0400509F RID: 20639
	private const string SHOP_TOGGLE_AUTOCONVERT = "SHOP_TOGGLE_AUTOCONVERT";

	// Token: 0x040050A0 RID: 20640
	private const string SHOP_BLOCK_INTERFACE = "SHOP_BLOCK_INTERFACE";

	// Token: 0x040050A1 RID: 20641
	private const string SHOP_UNBLOCK_INTERFACE = "SHOP_UNBLOCK_INTERFACE";

	// Token: 0x040050A2 RID: 20642
	private readonly PlatformDependentValue<bool> UnloadUnusedAssetsOnClose = new PlatformDependentValue<bool>(PlatformCategory.Memory)
	{
		LowMemory = true,
		MediumMemory = true,
		HighMemory = false
	};

	// Token: 0x020021ED RID: 8685
	private class PurchaseOrder
	{
		// Token: 0x0400E1C5 RID: 57797
		public ProductDataModel m_product;

		// Token: 0x0400E1C6 RID: 57798
		public PriceDataModel m_price;

		// Token: 0x0400E1C7 RID: 57799
		public int m_quantity = 1;
	}

	// Token: 0x020021EE RID: 8686
	// (Invoke) Token: 0x06012550 RID: 75088
	private delegate void ReopenCallback();
}
