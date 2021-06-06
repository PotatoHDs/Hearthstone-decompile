using System;
using System.Collections.Generic;
using System.IO;
using bgs;
using Blizzard.Commerce;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using com.blizzard.commerce.Model;
using Hearthstone;
using Hearthstone.Commerce;
using Hearthstone.Core;
using Hearthstone.Login;
using UnityEngine;

// Token: 0x020008D3 RID: 2259
public class HearthstoneCheckout : ICatalogListener, IBrowserListener, IPurchaseListener, IVirtualCurrencyEventListener, IService, IHasUpdate
{
	// Token: 0x1700071E RID: 1822
	// (set) Token: 0x06007D06 RID: 32006 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public static string OneStoreKey
	{
		set
		{
		}
	}

	// Token: 0x1700071F RID: 1823
	// (get) Token: 0x06007D07 RID: 32007 RVA: 0x0028A4B4 File Offset: 0x002886B4
	// (set) Token: 0x06007D08 RID: 32008 RVA: 0x0028A4BC File Offset: 0x002886BC
	public CheckoutEvent OnInitializedEvent { get; private set; }

	// Token: 0x17000720 RID: 1824
	// (get) Token: 0x06007D09 RID: 32009 RVA: 0x0028A4C5 File Offset: 0x002886C5
	// (set) Token: 0x06007D0A RID: 32010 RVA: 0x0028A4CD File Offset: 0x002886CD
	public CheckoutEvent OnReadyEvent { get; private set; }

	// Token: 0x17000721 RID: 1825
	// (get) Token: 0x06007D0B RID: 32011 RVA: 0x0028A4D6 File Offset: 0x002886D6
	// (set) Token: 0x06007D0C RID: 32012 RVA: 0x0028A4DE File Offset: 0x002886DE
	public CheckoutEvent OnDisconnectEvent { get; private set; }

	// Token: 0x17000722 RID: 1826
	// (get) Token: 0x06007D0D RID: 32013 RVA: 0x0028A4E7 File Offset: 0x002886E7
	// (set) Token: 0x06007D0E RID: 32014 RVA: 0x0028A4EF File Offset: 0x002886EF
	public CheckoutEvent OnCancelEvent { get; private set; }

	// Token: 0x17000723 RID: 1827
	// (get) Token: 0x06007D0F RID: 32015 RVA: 0x0028A4F8 File Offset: 0x002886F8
	// (set) Token: 0x06007D10 RID: 32016 RVA: 0x0028A500 File Offset: 0x00288700
	public CheckoutEvent OnCloseEvent { get; private set; }

	// Token: 0x17000724 RID: 1828
	// (get) Token: 0x06007D11 RID: 32017 RVA: 0x0028A509 File Offset: 0x00288709
	// (set) Token: 0x06007D12 RID: 32018 RVA: 0x0028A511 File Offset: 0x00288711
	public CheckoutEvent OnPurchaseCanceledBeforeSubmitEvent { get; private set; }

	// Token: 0x17000725 RID: 1829
	// (get) Token: 0x06007D13 RID: 32019 RVA: 0x0028A51A File Offset: 0x0028871A
	// (set) Token: 0x06007D14 RID: 32020 RVA: 0x0028A522 File Offset: 0x00288722
	public CheckoutEvent OnPurchaseFailureBeforeSubmitEvent { get; private set; }

	// Token: 0x17000726 RID: 1830
	// (get) Token: 0x06007D15 RID: 32021 RVA: 0x0028A52B File Offset: 0x0028872B
	// (set) Token: 0x06007D16 RID: 32022 RVA: 0x0028A533 File Offset: 0x00288733
	public CheckoutEvent OnPurchaseSubmittedEvent { get; private set; }

	// Token: 0x17000727 RID: 1831
	// (get) Token: 0x06007D17 RID: 32023 RVA: 0x0028A53C File Offset: 0x0028873C
	// (set) Token: 0x06007D18 RID: 32024 RVA: 0x0028A544 File Offset: 0x00288744
	public TransactionCheckoutEvent OnOrderPendingEvent { get; private set; }

	// Token: 0x17000728 RID: 1832
	// (get) Token: 0x06007D19 RID: 32025 RVA: 0x0028A54D File Offset: 0x0028874D
	// (set) Token: 0x06007D1A RID: 32026 RVA: 0x0028A555 File Offset: 0x00288755
	public TransactionCheckoutEvent OnOrderFailureEvent { get; private set; }

	// Token: 0x17000729 RID: 1833
	// (get) Token: 0x06007D1B RID: 32027 RVA: 0x0028A55E File Offset: 0x0028875E
	// (set) Token: 0x06007D1C RID: 32028 RVA: 0x0028A566 File Offset: 0x00288766
	public TransactionCheckoutEvent OnOrderCompleteEvent { get; private set; }

	// Token: 0x1700072A RID: 1834
	// (get) Token: 0x06007D1D RID: 32029 RVA: 0x0028A56F File Offset: 0x0028876F
	public HearthstoneCheckoutUI CheckoutUi
	{
		get
		{
			return this.m_checkoutUI;
		}
	}

	// Token: 0x1700072B RID: 1835
	// (get) Token: 0x06007D1E RID: 32030 RVA: 0x0028A577 File Offset: 0x00288777
	// (set) Token: 0x06007D1F RID: 32031 RVA: 0x0028A57F File Offset: 0x0028877F
	public bool CheckoutIsReady { get; private set; }

	// Token: 0x1700072C RID: 1836
	// (get) Token: 0x06007D20 RID: 32032 RVA: 0x0028A588 File Offset: 0x00288788
	public blz_commerce_sdk_t Sdk
	{
		get
		{
			return this.m_commerceSdk;
		}
	}

	// Token: 0x1700072D RID: 1837
	// (get) Token: 0x06007D21 RID: 32033 RVA: 0x0028A590 File Offset: 0x00288790
	public HearthstoneCheckout.State CurrentState
	{
		get
		{
			return this.m_currentState;
		}
	}

	// Token: 0x1700072E RID: 1838
	// (get) Token: 0x06007D22 RID: 32034 RVA: 0x0028A598 File Offset: 0x00288798
	public bool HasProductCatalog
	{
		get
		{
			return this.m_productCatalog != null;
		}
	}

	// Token: 0x1700072F RID: 1839
	// (get) Token: 0x06007D23 RID: 32035 RVA: 0x0028A5A3 File Offset: 0x002887A3
	public bool HasClientID
	{
		get
		{
			return this.m_clientID != null;
		}
	}

	// Token: 0x17000730 RID: 1840
	// (get) Token: 0x06007D24 RID: 32036 RVA: 0x0028A5AE File Offset: 0x002887AE
	public bool HasCurrencyCode
	{
		get
		{
			return this.m_currencyCode != null;
		}
	}

	// Token: 0x06007D25 RID: 32037 RVA: 0x0028A5B9 File Offset: 0x002887B9
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		if (Vars.Key("Commerce.OverrideEndpointToProduction").HasValue)
		{
			this.m_overrideEndpointToProduction = new bool?(Vars.Key("Commerce.OverrideEndpointToProduction").GetBool(true));
		}
		this.OnInitializedEvent = new CheckoutEvent();
		this.OnReadyEvent = new CheckoutEvent();
		this.OnDisconnectEvent = new CheckoutEvent();
		this.OnCancelEvent = new CheckoutEvent();
		this.OnCloseEvent = new CheckoutEvent();
		this.OnPurchaseCanceledBeforeSubmitEvent = new CheckoutEvent();
		this.OnPurchaseFailureBeforeSubmitEvent = new CheckoutEvent();
		this.OnPurchaseSubmittedEvent = new CheckoutEvent();
		this.OnOrderPendingEvent = new TransactionCheckoutEvent();
		this.OnOrderFailureEvent = new TransactionCheckoutEvent();
		this.OnOrderCompleteEvent = new TransactionCheckoutEvent();
		this.m_commerceListener = new BlizzardCommerceListener();
		string text = null;
		try
		{
			this.m_commerceListener.SceneEventListener.Ready += this.OnSceneViewReady;
			this.m_commerceListener.SceneEventListener.Disconnect += this.OnDisconnect;
			this.m_commerceListener.SceneEventListener.Cancel += this.OnCancel;
			this.m_commerceListener.SceneEventListener.WindowResize += this.OnWindowResized;
			this.m_commerceListener.SceneEventListener.BufferUpdate += this.OnBufferUpdate;
			this.m_commerceListener.SceneEventListener.WindowResizeRequested += this.OnWindowResizeRequested;
			this.m_commerceListener.SceneEventListener.WindowCloseRequest += this.OnWindowCloseRequested;
			this.m_commerceListener.SceneEventListener.CursorChanged += this.OnCursorChangeRequest;
			this.m_commerceListener.SceneEventListener.ExternalLink += this.OnExternalLink;
			this.m_commerceListener.PurchaseEventListener.Cancel += this.OnPurchaseCanceledBeforeSubmit;
			this.m_commerceListener.PurchaseEventListener.Failure += this.OnOrderFailure;
			this.m_commerceListener.PurchaseEventListener.Pending += this.OnOrderPending;
			this.m_commerceListener.PurchaseEventListener.Successful += this.OnOrderComplete;
			this.m_commerceListener.VirtualCurrencyEventListener.GetBalance += this.OnGetBalanceResponse;
			this.m_commerceListener.VirtualCurrencyEventListener.PurchaseEvent += this.OnVirtualCurrencyResponse;
			this.m_commerceListener.CatalogEventListener.PersonalizedShopReceived += this.OnGetPersonalizedShopEvent;
			this.m_commerceListener.CatalogEventListener.ProductLoaded += this.ProductLoaded;
		}
		catch (Exception ex)
		{
			this.m_currentState = HearthstoneCheckout.State.Unavailable;
			text = string.Format("Failed to initialize HearthstoneCheckout: {0}", ex);
			TelemetryManager.Client().SendBlizzardCheckoutInitializationResult(false, "Checkout Library Exception.", ex.ToString());
		}
		if (!string.IsNullOrEmpty(text))
		{
			yield return new JobFailedResult(text, Array.Empty<object>());
		}
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset += this.OnReset;
		}
		JobDefinition jobDefinition = new JobDefinition("HearthstoneCheckout.LoadCheckoutUI", this.Job_LoadCheckoutUI(), JobFlags.StartImmediately, Array.Empty<IJobDependency>());
		Processor.QueueJob(jobDefinition);
		Processor.QueueJob(new JobDefinition("HearthstoneCheckout.InitializeCheckoutClient", this.Job_InitializeCheckoutClient(), new IJobDependency[]
		{
			jobDefinition.CreateDependency(),
			new WaitForCheckoutConfiguration()
		}));
		yield break;
	}

	// Token: 0x06007D26 RID: 32038 RVA: 0x0028A5C8 File Offset: 0x002887C8
	private void LoadProducts()
	{
		GetProductsByStoreIdRequest getProductsByStoreIdRequest = new GetProductsByStoreIdRequest();
		getProductsByStoreIdRequest.locale = Localization.GetLocaleName();
		getProductsByStoreIdRequest.gameServiceRegionId = (int)BattleNet.GetCurrentRegion();
		getProductsByStoreIdRequest.storeId = 6;
		getProductsByStoreIdRequest.paginationSize = 200;
		battlenet_commerce.blz_catalog_load_products(this.m_commerceSdk, JsonUtility.ToJson(getProductsByStoreIdRequest));
	}

	// Token: 0x06007D27 RID: 32039 RVA: 0x0028A618 File Offset: 0x00288818
	private void ProductLoaded(GetProductsByStoreIdResponse productLoadEvent)
	{
		if (productLoadEvent == null)
		{
			global::Log.Store.PrintError("Received a product from server that was not defined!", Array.Empty<object>());
			return;
		}
		foreach (Product product in productLoadEvent.products)
		{
			if (product.prices.Count <= 0)
			{
				global::Log.Store.PrintError("The product received had no prices given.", Array.Empty<object>());
			}
			else
			{
				if (product.prices.Count > 1)
				{
					global::Log.Store.PrintError("The product received had multiple prices! (Product ID: {0} (Price count {1})", new object[]
					{
						product.productId,
						product.prices.Count
					});
				}
				this.m_productMap[product.productId] = new HearthstoneCheckout.ProductInfo(product.productId.ToString(), product.localization.name, product.prices[0].localizedCurrentPrice);
			}
		}
		this.m_receivedSdkProducts = true;
		battlenet_commerce.blz_checkout_resume(this.m_commerceSdk);
	}

	// Token: 0x06007D28 RID: 32040 RVA: 0x0028A740 File Offset: 0x00288940
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network),
			typeof(LoginManager),
			typeof(IAssetLoader),
			typeof(ILoginService)
		};
	}

	// Token: 0x06007D29 RID: 32041 RVA: 0x0028A77C File Offset: 0x0028897C
	public void Shutdown()
	{
		global::Log.Store.PrintDebug("[HearthstoneCheckout.Shutdown]", Array.Empty<object>());
		this.m_currentState = HearthstoneCheckout.State.Startup;
		this.m_currentTransaction = null;
		this.m_productCatalog = null;
		this.m_virtualCurrencyRequests.Clear();
		this.DestroyCheckoutUI();
		this.DisposeCurrentCheckoutClient();
		this.DisposeListeners();
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset -= this.OnReset;
		}
	}

	// Token: 0x06007D2A RID: 32042 RVA: 0x0028A7F0 File Offset: 0x002889F0
	public static bool IsAvailable()
	{
		HearthstoneCheckout hearthstoneCheckout;
		return HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout) && hearthstoneCheckout.CurrentState != HearthstoneCheckout.State.Unavailable && hearthstoneCheckout.CurrentState > HearthstoneCheckout.State.Startup;
	}

	// Token: 0x06007D2B RID: 32043 RVA: 0x0028A81C File Offset: 0x00288A1C
	public static bool IsClientCreationInProgress()
	{
		ServiceLocator.ServiceState serviceState = HearthstoneServices.GetServiceState<HearthstoneCheckout>();
		HearthstoneCheckout hearthstoneCheckout;
		return serviceState != ServiceLocator.ServiceState.Error && serviceState != ServiceLocator.ServiceState.Invalid && (!HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout) || hearthstoneCheckout.CurrentState == HearthstoneCheckout.State.Startup);
	}

	// Token: 0x06007D2C RID: 32044 RVA: 0x0028A84C File Offset: 0x00288A4C
	public void ShowCheckout(long productID, string currencyCode, uint quantity)
	{
		Processor.QueueJob("ShowCheckout", this.Job_ShowCheckout(productID, currencyCode, quantity), Array.Empty<IJobDependency>()).AddJobFinishedEventListener(new JobDefinition.JobFinishedEventListener(this.OnPurchaseJobFinished));
	}

	// Token: 0x06007D2D RID: 32045 RVA: 0x0028A877 File Offset: 0x00288A77
	public void PurchaseWithVirtualCurrency(long productID, string currencyCode, uint quantity)
	{
		Processor.QueueJob("PurchaseWithVirtualCurrency", this.Job_PurchaseWithVirtualCurrency(productID, currencyCode, quantity), Array.Empty<IJobDependency>()).AddJobFinishedEventListener(new JobDefinition.JobFinishedEventListener(this.OnPurchaseJobFinished));
	}

	// Token: 0x06007D2E RID: 32046 RVA: 0x0028A8A4 File Offset: 0x00288AA4
	public bool GetVirtualCurrencyBalance(string currencyCode, HearthstoneCheckout.VirtualCurrencyBalanceCallback callback)
	{
		if (this.m_commerceSdk != null)
		{
			GetBalanceRequest getBalanceRequest = new GetBalanceRequest();
			getBalanceRequest.currencyCode = currencyCode;
			battlenet_commerce.blz_commerce_vc_get_balance(this.m_commerceSdk, JsonUtility.ToJson(getBalanceRequest));
			this.m_virtualCurrencyRequests.Add(new HearthstoneCheckout.VirtualCurrencyRequest(currencyCode, callback));
			return true;
		}
		global::Log.Store.PrintWarning("[HearthstoneCheckout.GetVirtualCurrencyBalance] Cannot get virtual currency balance because the checkout client isn't initialized.", Array.Empty<object>());
		return false;
	}

	// Token: 0x06007D2F RID: 32047 RVA: 0x0028A904 File Offset: 0x00288B04
	public bool TryGetProductInfo(string productID, out HearthstoneCheckout.ProductInfo productInfo)
	{
		int key = 0;
		if (!int.TryParse(productID, out key))
		{
			global::Log.Store.PrintError("[HearthstoneCheckout.GetProductInfo] The store ID passed in was not an integer and therefore could not be mapped. (ID: {0})", new object[]
			{
				productID
			});
			productInfo = new HearthstoneCheckout.ProductInfo(null, null, null);
			return false;
		}
		if (!this.m_productMap.ContainsKey(key))
		{
			global::Log.Store.PrintError("[HearthstoneCheckout.GetProductInfo] The product map did not contain the product ID {0}.", new object[]
			{
				key.ToString()
			});
			productInfo = new HearthstoneCheckout.ProductInfo(null, null, null);
			return false;
		}
		productInfo = this.m_productMap[key];
		return true;
	}

	// Token: 0x06007D30 RID: 32048 RVA: 0x0028A998 File Offset: 0x00288B98
	public void GetPersonalizedShopData(string pageId, HearthstoneCheckout.PersonalizedShopResponseCallback callback)
	{
		if (callback == null)
		{
			global::Log.Store.PrintWarning("[HearthstoneCheckout.GetPersonalizedShopData] Callback cannot be null.", Array.Empty<object>());
			return;
		}
		if (!Network.IsLoggedIn())
		{
			global::Log.Store.PrintWarning("[HearthstoneCheckout.GetPersonalizedShopData] Cannot get personalized shop data because the user is off-line.", Array.Empty<object>());
			return;
		}
		if (this.m_commerceSdk != null)
		{
			this.m_personalizedShopResponseCallbacks.Enqueue(callback);
			GetPageRequest obj = new GetPageRequest
			{
				pageId = pageId,
				locale = Localization.GetLocaleName(),
				gameServiceRegionId = (int)BattleNet.GetCurrentRegion()
			};
			battlenet_commerce.blz_catalog_personalized_shop(this.m_commerceSdk, JsonUtility.ToJson(obj));
			return;
		}
		global::Log.Store.PrintWarning("[HearthstoneCheckout.GetPersonalizedShopData] Cannot get personalized shop data because the checkout client isn't initialized.", Array.Empty<object>());
	}

	// Token: 0x06007D31 RID: 32049 RVA: 0x0028AA37 File Offset: 0x00288C37
	public bool IsUIShown()
	{
		return this.m_checkoutUI != null && this.m_checkoutUI.IsShown();
	}

	// Token: 0x06007D32 RID: 32050 RVA: 0x0028AA54 File Offset: 0x00288C54
	public float GetShownTime()
	{
		return this.m_elapsedTimeSinceShown;
	}

	// Token: 0x06007D33 RID: 32051 RVA: 0x0028AA5C File Offset: 0x00288C5C
	public void RequestClose()
	{
		switch (this.m_currentState)
		{
		case HearthstoneCheckout.State.Idle:
			global::Log.Store.PrintWarning("[HearthstoneCheckout.RequestClose] HearthstoneCheckout received a request close when it should already be closed.  Attempting to close again...", Array.Empty<object>());
			this.SignalCloseNextFrame();
			break;
		case HearthstoneCheckout.State.Initializing:
		case HearthstoneCheckout.State.Ready:
			this.OnCancel();
			return;
		case HearthstoneCheckout.State.InProgress:
			break;
		case HearthstoneCheckout.State.InProgress_Backgroundable:
		case HearthstoneCheckout.State.Finished:
			this.SignalCloseNextFrame();
			return;
		default:
			return;
		}
	}

	// Token: 0x06007D34 RID: 32052 RVA: 0x0028AAB9 File Offset: 0x00288CB9
	public bool ShouldBlockInput()
	{
		return this.IsUIShown();
	}

	// Token: 0x06007D35 RID: 32053 RVA: 0x0028AAC4 File Offset: 0x00288CC4
	public void Update()
	{
		if (this.m_checkoutUI != null && this.m_checkoutUI.HasCheckoutMesh)
		{
			this.m_elapsedTimeSinceResolutionCheck += Time.deltaTime;
			if (this.m_elapsedTimeSinceResolutionCheck > HearthstoneCheckout.m_resolutionUpdateInterval)
			{
				this.ScreenResolutionUpdate();
				this.m_elapsedTimeSinceResolutionCheck = 0f;
			}
		}
		if (this.IsUIShown())
		{
			this.m_elapsedTimeSinceShown += Time.deltaTime;
		}
		if (this.m_closeRequested)
		{
			if (this.m_currentTransaction != null && !this.m_currentTransaction.IsVCPurchase)
			{
				battlenet_commerce.blz_commerce_browser_send_event(this.m_commerceSdk, blz_commerce_browser_event_type_t.WINDOW_CLOSE, IntPtr.Zero);
			}
			if (this.m_currentState != HearthstoneCheckout.State.InProgress && this.m_currentState != HearthstoneCheckout.State.InProgress_Backgroundable)
			{
				this.ClearTransaction();
			}
			else if (this.IsUIShown())
			{
				this.m_checkoutUI.Hide();
			}
			this.m_closeRequested = false;
			this.OnCloseEvent.Fire();
			return;
		}
		if (this.m_commerceSdk != null)
		{
			battlenet_commerce.blz_commerce_update(this.m_commerceSdk);
		}
		HearthstoneCheckout.State currentState = this.m_currentState;
		if (currentState == HearthstoneCheckout.State.InProgress && (float)(DateTime.Now - this.m_transactionStart).Seconds >= 10f)
		{
			this.m_currentState = HearthstoneCheckout.State.InProgress_Backgroundable;
		}
	}

	// Token: 0x06007D36 RID: 32054 RVA: 0x0028ABEB File Offset: 0x00288DEB
	public void SetProductCatalog(string[] productCatalog)
	{
		this.m_productCatalog = productCatalog;
	}

	// Token: 0x06007D37 RID: 32055 RVA: 0x0028ABF4 File Offset: 0x00288DF4
	public void SetClientID(string clientID)
	{
		this.m_clientID = clientID;
	}

	// Token: 0x06007D38 RID: 32056 RVA: 0x0028ABFD File Offset: 0x00288DFD
	public void SetCurrencyCode(string currencyCode)
	{
		this.m_currencyCode = currencyCode;
	}

	// Token: 0x06007D39 RID: 32057 RVA: 0x0028AC08 File Offset: 0x00288E08
	private void OnReset()
	{
		this.m_currentState = HearthstoneCheckout.State.Startup;
		this.m_productCatalog = null;
		this.m_clientID = null;
		this.m_personalizedShopResponseCallbacks.Clear();
		this.m_virtualCurrencyRequests.Clear();
		this.DestroyCheckoutUI();
		this.DisposeCurrentCheckoutClient();
		JobDefinition jobDefinition = new JobDefinition("HearthstoneCheckout.LoadCheckoutUI", this.Job_LoadCheckoutUI(), JobFlags.StartImmediately, new IJobDependency[]
		{
			new WaitForGameDownloadManagerState()
		});
		Processor.QueueJob(jobDefinition);
		Processor.QueueJob(new JobDefinition("HearthstoneCheckout.InitializeCheckoutClient", this.Job_InitializeCheckoutClient(), new IJobDependency[]
		{
			jobDefinition.CreateDependency(),
			new WaitForCheckoutConfiguration()
		}));
	}

	// Token: 0x06007D3A RID: 32058 RVA: 0x0028ACA0 File Offset: 0x00288EA0
	private blz_commerce_checkout_browser_params_t CreateCheckoutParams(string ssoToken)
	{
		HearthstoneCheckout.GetBrowserPath();
		string titleVersionString = this.GetTitleVersionString();
		HearthstoneApplication.IsPublic();
		if (this.m_overrideEndpointToProduction != null)
		{
			bool value = this.m_overrideEndpointToProduction.Value;
		}
		if (this.m_purchaseListener == null)
		{
			this.m_purchaseListener = new PurchaseListener(this);
		}
		if (this.m_browserListener == null)
		{
			this.m_browserListener = new BrowserListener(this);
		}
		if (this.m_virtualCurrencyEventListener == null)
		{
			this.m_virtualCurrencyEventListener = new global::VirtualCurrencyEventListener(this);
		}
		if (this.m_catalogListener == null)
		{
			this.m_catalogListener = new CatalogListener(this);
		}
		return new blz_commerce_checkout_browser_params_t
		{
			title_code = "WTCG",
			title_version = titleVersionString,
			checkout_url = ExternalUrlService.Get().GetCheckoutLink(),
			device_id = SystemInfo.deviceUniqueIdentifier,
			game_service_region = ((int)BattleNet.GetCurrentRegion()).ToString(),
			locale = Localization.GetLocaleName()
		};
	}

	// Token: 0x06007D3B RID: 32059 RVA: 0x0028AD7C File Offset: 0x00288F7C
	private string GetTitleVersionString()
	{
		string text = "20.4";
		string text2 = "0";
		int num = 84593;
		string platformString = this.GetPlatformString();
		return string.Format("{0}.{1}.{2}-{3}", new object[]
		{
			text,
			text2,
			num,
			platformString
		});
	}

	// Token: 0x06007D3C RID: 32060 RVA: 0x0028ADC8 File Offset: 0x00288FC8
	private string GetPlatformString()
	{
		switch (PlatformSettings.RuntimeOS)
		{
		case OSCategory.PC:
			return "Windows";
		case OSCategory.Mac:
			return "MacOS";
		case OSCategory.iOS:
			return this.GetIOSPlatformString();
		case OSCategory.Android:
			return this.GetAndroidPlatformString();
		default:
			return "UnknownOS";
		}
	}

	// Token: 0x06007D3D RID: 32061 RVA: 0x0028AE14 File Offset: 0x00289014
	private string GetAndroidPlatformString()
	{
		switch (AndroidDeviceSettings.Get().GetAndroidStore())
		{
		case AndroidStore.BLIZZARD:
			return "AndroidBattlenet";
		case AndroidStore.GOOGLE:
			return "Google";
		case AndroidStore.AMAZON:
			return "Amazon";
		case AndroidStore.HUAWEI:
			return "Huawei";
		default:
			return "UnkownAndroid";
		}
	}

	// Token: 0x06007D3E RID: 32062 RVA: 0x0028AE63 File Offset: 0x00289063
	private string GetIOSPlatformString()
	{
		if (PlatformSettings.LocaleVariant != LocaleVariant.China)
		{
			return "iOS";
		}
		return "iOSCN";
	}

	// Token: 0x06007D3F RID: 32063 RVA: 0x0028AE7C File Offset: 0x0028907C
	private blz_commerce_purchase_t CreatePurchaseRequest(long productID, string currencyCode, uint quantity, string ssoToken, bool generateExternalTransactionID)
	{
		((int)BattleNet.GetCurrentRegion()).ToString();
		Localization.GetLocaleName();
		blz_commerce_browser_purchase_t blz_commerce_browser_purchase_t = new blz_commerce_browser_purchase_t
		{
			sso_token = ssoToken
		};
		if (generateExternalTransactionID)
		{
			blz_commerce_browser_purchase_t.externalTransactionId = this.GenerateExternalTransactionID();
		}
		return new blz_commerce_purchase_t
		{
			currency_id = currencyCode,
			product_id = productID.ToString(),
			browser_purchase = blz_commerce_browser_purchase_t
		};
	}

	// Token: 0x06007D40 RID: 32064 RVA: 0x0028AEDB File Offset: 0x002890DB
	private string GetLocaleForURL(string locale)
	{
		return locale;
	}

	// Token: 0x06007D41 RID: 32065 RVA: 0x0028AEE0 File Offset: 0x002890E0
	private string GenerateExternalTransactionID()
	{
		if (this.m_commerceSdk == null)
		{
			global::Log.Store.PrintError("[HearthstoneCheckout.GenerateExternalTransactionID] Checkout Client must exists to generate an external transaction ID.", Array.Empty<object>());
			return null;
		}
		constants.BnetRegion bnetRegion = BattleNet.GetAccountRegion();
		if (bnetRegion - constants.BnetRegion.REGION_US > 4)
		{
			bnetRegion = constants.BnetRegion.REGION_PTR;
		}
		return battlenet_commerce.blz_commerce_generate_transaction_id(14, (int)bnetRegion);
	}

	// Token: 0x06007D42 RID: 32066 RVA: 0x0028AF24 File Offset: 0x00289124
	private static string GetBrowserPath()
	{
		if (Application.isEditor)
		{
			OSCategory runtimeOS = PlatformSettings.RuntimeOS;
			if (runtimeOS == OSCategory.PC)
			{
				return Path.Combine(Application.dataPath, "../Contrib/BlizzardCommerce/windows/BlizzardBrowser/release/BlizzardBrowser").Replace('/', '\\');
			}
			if (runtimeOS == OSCategory.Mac)
			{
				return Path.Combine(Application.dataPath, "../Contrib/BlizzardCommerce/macosx/BlizzardBrowser/release/BlizzardBrowser").Replace('/', '\\');
			}
		}
		return Application.dataPath;
	}

	// Token: 0x06007D43 RID: 32067 RVA: 0x0028AF80 File Offset: 0x00289180
	private void ScreenResolutionUpdate()
	{
		if (this.m_checkoutUI != null && this.m_checkoutUI.IsShown() && this.m_commerceSdk != null && (this.m_screenResolution.x != (float)Screen.width || this.m_screenResolution.y != (float)Screen.height))
		{
			this.m_checkoutUI.DetermineBrowserSize();
			blz_commerce_vec2d_t obj = new blz_commerce_vec2d_t
			{
				x = this.m_checkoutUI.BrowserWidth,
				y = this.m_checkoutUI.BrowserHeight
			};
			battlenet_commerce.blz_commerce_browser_send_event(this.m_commerceSdk, blz_commerce_browser_event_type_t.RESIZE_WINDOW, blz_commerce_vec2d_t.getCPtr(obj).Handle);
			this.m_screenResolution.x = (float)Screen.width;
			this.m_screenResolution.y = (float)Screen.height;
		}
	}

	// Token: 0x06007D44 RID: 32068 RVA: 0x0028B050 File Offset: 0x00289250
	private void UpdateTransactionData(blz_commerce_purchase_event_t response)
	{
		if (!string.IsNullOrEmpty(response.product_id))
		{
			long productID = 0L;
			long.TryParse(response.product_id, out productID);
			this.m_currentTransaction.ProductID = productID;
		}
		this.m_currentTransaction.TransactionID = response.transaction_id;
		this.m_currentTransaction.ErrorCodes = response.error_code;
	}

	// Token: 0x06007D45 RID: 32069 RVA: 0x0028B0AC File Offset: 0x002892AC
	private void LogPurchaseResponse(string tag, blz_commerce_purchase_event_t response)
	{
		global::Log.Store.PrintDebug("{0} Status - {1}", new object[]
		{
			tag,
			response.status.ToString()
		});
		string error_code = response.error_code;
		if (!string.IsNullOrEmpty(error_code))
		{
			global::Log.Store.PrintError("[HearthstoneCheckout] CHECKOUT ERROR: {0}", new object[]
			{
				error_code
			});
		}
	}

	// Token: 0x06007D46 RID: 32070 RVA: 0x0028B111 File Offset: 0x00289311
	private void SetScreenResolution()
	{
		this.m_screenResolution = new Vector2((float)Screen.width, (float)Screen.height);
	}

	// Token: 0x06007D47 RID: 32071 RVA: 0x0028B12A File Offset: 0x0028932A
	private void SignalCloseNextFrame()
	{
		this.m_closeRequested = true;
	}

	// Token: 0x06007D48 RID: 32072 RVA: 0x0028B134 File Offset: 0x00289334
	private void ClearTransaction()
	{
		if (this.IsUIShown())
		{
			this.m_checkoutUI.Hide();
		}
		if (this.m_purchaseHandle != null)
		{
			this.m_purchaseHandle.Dispose();
			this.m_purchaseHandle = null;
		}
		this.m_closeRequested = false;
		this.m_currentTransaction = null;
		if (this.m_currentState != HearthstoneCheckout.State.Unavailable)
		{
			this.m_currentState = HearthstoneCheckout.State.Idle;
		}
	}

	// Token: 0x06007D49 RID: 32073 RVA: 0x0028B18C File Offset: 0x0028938C
	private void DestroyCheckoutUI()
	{
		if (this.m_checkoutUI != null && this.m_checkoutUI.gameObject != null)
		{
			UnityEngine.Object.Destroy(this.m_checkoutUI.gameObject);
			this.m_checkoutUI = null;
		}
	}

	// Token: 0x06007D4A RID: 32074 RVA: 0x0028B1C6 File Offset: 0x002893C6
	private void OnTransactionProcessCompleted()
	{
		if (!this.IsUIShown())
		{
			this.SignalCloseNextFrame();
		}
	}

	// Token: 0x06007D4B RID: 32075 RVA: 0x0028B1D6 File Offset: 0x002893D6
	private void OnPurchaseJobFinished(JobDefinition job, bool success)
	{
		if (!success && this.m_currentState != HearthstoneCheckout.State.InProgress && this.m_currentState != HearthstoneCheckout.State.InProgress_Backgroundable && this.m_currentState != HearthstoneCheckout.State.Finished)
		{
			this.OnPurchaseFailureBeforeSubmit(new blz_commerce_purchase_event_t());
			this.RequestClose();
		}
	}

	// Token: 0x06007D4C RID: 32076 RVA: 0x0028B207 File Offset: 0x00289407
	private void OnOutsideClick()
	{
		if (StoreManager.Get().CanTapOutConfirmationUI())
		{
			this.RequestClose();
		}
	}

	// Token: 0x06007D4D RID: 32077 RVA: 0x0028B21C File Offset: 0x0028941C
	private void DisposeCurrentCheckoutClient()
	{
		if (this.m_commerceSdk != null)
		{
			this.m_commerceSdk.Dispose();
			this.m_commerceSdk = null;
		}
		if (this.m_purchaseHandle != null)
		{
			this.m_purchaseHandle.Dispose();
			this.m_purchaseHandle = null;
		}
		if (this.m_checkoutParams != null)
		{
			this.m_checkoutParams.Dispose();
			this.m_checkoutParams = null;
		}
	}

	// Token: 0x06007D4E RID: 32078 RVA: 0x0028B277 File Offset: 0x00289477
	private void DisposeListeners()
	{
		if (this.m_browserListener != null)
		{
			this.m_browserListener = null;
		}
		if (this.m_purchaseListener != null)
		{
			this.m_purchaseListener = null;
		}
		if (this.m_virtualCurrencyEventListener != null)
		{
			this.m_virtualCurrencyEventListener = null;
		}
		if (this.m_catalogListener != null)
		{
			this.m_catalogListener = null;
		}
	}

	// Token: 0x06007D4F RID: 32079 RVA: 0x0028B2B5 File Offset: 0x002894B5
	private bool IsTransactionInProgress()
	{
		return this.m_currentTransaction != null && (this.m_currentState == HearthstoneCheckout.State.Initializing || this.m_currentState == HearthstoneCheckout.State.Ready || this.m_currentState == HearthstoneCheckout.State.InProgress || this.m_currentState == HearthstoneCheckout.State.InProgress_Backgroundable);
	}

	// Token: 0x06007D50 RID: 32080 RVA: 0x0028B2E7 File Offset: 0x002894E7
	private IEnumerator<IAsyncJobResult> Job_ShowCheckout(long productID, string currencyCode, uint quantity)
	{
		if (this.m_currentState != HearthstoneCheckout.State.Idle)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.ShowCheckout] Cannot initiate checkout while not in the idle state.  Current State - {0}", new object[]
			{
				this.m_currentState
			});
		}
		if (this.m_commerceSdk == null)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.ShowCheckout] Cannot show checkout because the checkout client isn't initialized.", Array.Empty<object>());
		}
		if (this.m_checkoutUI == null)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.ShowCheckout] Cannot show checkout because the UI isn't loaded.", Array.Empty<object>());
		}
		global::Log.Store.PrintDebug("[HearthstoneCheckout.ShowCheckout]", Array.Empty<object>());
		this.m_currentState = HearthstoneCheckout.State.Initializing;
		this.m_elapsedTimeSinceResolutionCheck = 0f;
		this.m_elapsedTimeSinceShown = 0f;
		this.m_checkoutUI.GenerateMeshes();
		this.m_currentTransaction = new HearthstoneCheckoutTransactionData(productID, currencyCode, quantity, false);
		yield return new WaitForLogin();
		GenerateSSOToken generateToken = new GenerateSSOToken();
		yield return generateToken;
		blz_commerce_purchase_t purchase = this.CreatePurchaseRequest(this.m_currentTransaction.ProductID, this.m_currentTransaction.CurrencyCode, this.m_currentTransaction.Quantity, generateToken.Token, false);
		this.m_purchaseHandle = battlenet_commerce.blz_checkout_purchase(this.m_commerceSdk, purchase);
		if (this.m_purchaseHandle == null)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.ShowCheckout] Failed to obtain purchase handle.", Array.Empty<object>());
		}
		global::Log.Store.PrintDebug("[HearthstoneCheckout.ShowCheckout]Purchase was successfully initiated.", Array.Empty<object>());
		this.m_checkoutUI.InitiateCheckout(this);
		yield break;
	}

	// Token: 0x06007D51 RID: 32081 RVA: 0x0028B30B File Offset: 0x0028950B
	private IEnumerator<IAsyncJobResult> Job_PurchaseWithVirtualCurrency(long productID, string currencyCode, uint quantity)
	{
		if (this.m_currentState != HearthstoneCheckout.State.Idle)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.PurchaseWithVirtualCurrency] Cannot initiate purchase while not in the idle state.  Current State - {0}", new object[]
			{
				this.m_currentState
			});
		}
		if (this.m_commerceSdk == null)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.PurchaseWithVirtualCurrency] Cannot initiate purchase because the checkout client isn't initialized.", Array.Empty<object>());
		}
		GenerateSSOToken generateSSOToken = new GenerateSSOToken();
		yield return generateSSOToken;
		if (!generateSSOToken.HasToken)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.PurchaseWithVirtualCurrency] Cannot show checkout because it didn't receive an SSO token.", Array.Empty<object>());
		}
		yield return new WaitForLogin();
		this.m_currentState = HearthstoneCheckout.State.Initializing;
		this.m_currentTransaction = new HearthstoneCheckoutTransactionData(productID, currencyCode, quantity, true);
		this.m_currentTransaction.TransactionID = this.GenerateExternalTransactionID();
		PlaceOrderWithVCRequest placeOrderWithVCRequest = new PlaceOrderWithVCRequest();
		placeOrderWithVCRequest.currencyCode = this.m_currentTransaction.CurrencyCode;
		placeOrderWithVCRequest.externalTransactionId = this.m_currentTransaction.TransactionID;
		placeOrderWithVCRequest.gameServiceRegionId = (int)BattleNet.GetCurrentRegion();
		placeOrderWithVCRequest.productId = (int)this.m_currentTransaction.ProductID;
		placeOrderWithVCRequest.quantity = (int)this.m_currentTransaction.Quantity;
		placeOrderWithVCRequest.pointOfSaleId = 1465140039;
		battlenet_commerce.blz_commerce_vc_purchase(this.m_commerceSdk, JsonUtility.ToJson(placeOrderWithVCRequest));
		yield break;
	}

	// Token: 0x06007D52 RID: 32082 RVA: 0x0028B32F File Offset: 0x0028952F
	private IEnumerator<IAsyncJobResult> Job_LoadCheckoutUI()
	{
		if (this.m_checkoutUI != null)
		{
			global::Log.Store.PrintError("[HearthstoneCheckout.LoadCheckoutUI] Checkout UI already exists!  Please destroy the existing UI before creating a new one.", Array.Empty<object>());
			yield break;
		}
		while (!Network.IsLoggedIn())
		{
			yield return null;
		}
		InstantiatePrefab loadCheckoutUI = new InstantiatePrefab("HearthstoneCheckout.prefab:da1b8fa18876ab5468bd2aa04a3f2539");
		yield return loadCheckoutUI;
		this.m_checkoutUI = loadCheckoutUI.InstantiatedPrefab.GetComponent<HearthstoneCheckoutUI>();
		loadCheckoutUI.InstantiatedPrefab.AddComponent<HSDontDestroyOnLoad>();
		this.m_checkoutUI.Hide();
		this.m_checkoutUI.DetermineBrowserSize();
		while (string.IsNullOrEmpty(this.m_clientID))
		{
			yield return null;
		}
		GenerateSSOToken generateSSOToken = new GenerateSSOToken();
		yield return generateSSOToken;
		if (!generateSSOToken.HasToken)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.LoadCheckoutUI] Cannot show checkout because it didn't receive an SSO token.", Array.Empty<object>());
		}
		this.m_currentState = HearthstoneCheckout.State.Initializing;
		this.m_commerceSdk = battlenet_commerce.blz_commerce_create().sdk;
		this.m_commerceLogger = new BlizzardCommerceLogger();
		List<blz_commerce_pair_t> list = new List<blz_commerce_pair_t>(6);
		this.m_httpParams = new blz_commerce_http_params_t();
		this.m_httpParams.title_code = "WTCG";
		this.m_httpParams.title_version = this.GetTitleVersionString();
		this.m_httpParams.region = (int)BattleNet.GetCurrentRegion();
		this.m_httpParams.environment = (HearthstoneApplication.IsPublic() ? blz_commerce_http_environment_t.BLZ_COMMERCE_HTTP_ENVIRONMENT_PROD : blz_commerce_http_environment_t.BLZ_COMMERCE_HTTP_ENVIRONMENT_QA);
		battlenet_commerce.blz_commerce_add_listener(this.m_commerceSdk, IntPtr.Zero, this.m_commerceListener);
		battlenet_commerce.blz_commerce_register_log(IntPtr.Zero, this.m_commerceLogger);
		this.m_httpParams.token = generateSSOToken.Token;
		this.m_httpParams.client_id = this.m_clientID;
		this.m_checkoutParams = new blz_commerce_checkout_browser_params_t
		{
			title_code = "WTCG",
			title_version = this.GetTitleVersionString(),
			locale = Localization.GetBnetLocaleName(),
			game_service_region = ((int)BattleNet.GetCurrentRegion()).ToString(),
			device_id = SystemInfo.deviceUniqueIdentifier,
			checkout_url = string.Format("https://nydus-qa.web.blizzard.net/Bnet/{0}/client/checkout", this.GetLocaleForURL(Localization.GetLocaleName())),
			game_account_id = ""
		};
		list.Add(new blz_commerce_pair_t
		{
			key = battlenet_commerce.BLZ_COMMERCE_CHECKOUT_BROWSER_PARAM,
			data = blz_commerce_checkout_browser_params_t.getCPtr(this.m_checkoutParams).Handle
		});
		list.Add(new blz_commerce_pair_t
		{
			key = battlenet_commerce.BLZ_COMMERCE_HTTP_PARAM,
			data = blz_commerce_http_params_t.getCPtr(this.m_httpParams).Handle
		});
		this.m_browserParams = new blz_commerce_browser_params_t
		{
			is_prod = HearthstoneApplication.IsPublic(),
			browser_directory = HearthstoneCheckout.GetBrowserPath(),
			window_height = this.m_checkoutUI.BrowserHeight,
			window_width = this.m_checkoutUI.BrowserWidth,
			log_directory = global::Logger.LogsPath
		};
		this.m_checkoutUI.SetUIParams(this.m_browserParams);
		this.OnReadyEvent.AddListener(new Action(this.m_checkoutUI.OnReady));
		this.m_checkoutUI.AddOutsideClickListener(new HearthstoneCheckoutUI.OutsideClickListener(this.OnOutsideClick));
		list.Add(new blz_commerce_pair_t
		{
			key = "android_activity"
		});
		list.Add(new blz_commerce_pair_t
		{
			key = "parentContentView"
		});
		list.Add(new blz_commerce_pair_t
		{
			key = battlenet_commerce.BLZ_COMMERCE_BROWSER_PARAM,
			data = blz_commerce_browser_params_t.getCPtr(this.m_browserParams).Handle
		});
		this.m_commercePairsArray = battlenet_commerce.new_blzCommercePairArray(list.Count);
		for (int i = 0; i < list.Count; i++)
		{
			battlenet_commerce.blzCommercePairArray_setitem(this.m_commercePairsArray, i, list[i]);
		}
		battlenet_commerce.blz_commerce_register(this.m_commerceSdk, battlenet_commerce.blz_commerce_register_http());
		battlenet_commerce.blz_commerce_register(this.m_commerceSdk, battlenet_commerce.blz_commerce_register_scene());
		battlenet_commerce.blz_commerce_register(this.m_commerceSdk, battlenet_commerce.blz_commerce_register_checkout());
		battlenet_commerce.blz_commerce_register(this.m_commerceSdk, battlenet_commerce.blz_commerce_register_catalog());
		battlenet_commerce.blz_commerce_register(this.m_commerceSdk, battlenet_commerce.blz_commerce_register_vc());
		if (battlenet_commerce.blz_commerce_init(this.m_commerceSdk, this.m_commercePairsArray, list.Count).state == blz_commerce_result_state_t.BLZ_COMMERCE_RESULT_OK)
		{
			this.OnReady();
			global::Log.Store.PrintInfo("Making Browser", Array.Empty<object>());
			battlenet_commerce.delete_blzCommercePairArray(this.m_commercePairsArray);
			this.LoadProducts();
			yield break;
		}
		throw new Exception("The commerce SDK failed to initialize internally!");
	}

	// Token: 0x06007D53 RID: 32083 RVA: 0x0028B33E File Offset: 0x0028953E
	private IEnumerator<IAsyncJobResult> Job_CreateCheckoutClient()
	{
		global::Log.Store.PrintDebug("[HearthstoneCheckout.CreateCheckoutClient] Initialize", Array.Empty<object>());
		this.m_currentState = HearthstoneCheckout.State.Startup;
		GenerateSSOToken generateSSOToken = new GenerateSSOToken();
		yield return generateSSOToken;
		if (!generateSSOToken.HasToken)
		{
			this.m_clientInitializationResponse = HearthstoneCheckout.ClientInitializationResponse.Fail;
			yield return new JobFailedResult("[HearthstoneCheckout.CreateCheckoutClient] Didn't receive a SSO token from request.", Array.Empty<object>());
		}
		yield return new WaitForLogin();
		this.m_checkoutParams = this.CreateCheckoutParams(generateSSOToken.Token);
		if (this.m_clientInitializationResponse == HearthstoneCheckout.ClientInitializationResponse.Fail)
		{
			this.m_clientInitializationResponse = HearthstoneCheckout.ClientInitializationResponse.Waiting;
		}
		global::Log.Store.PrintDebug("[HearthstoneCheckout.CreateCheckoutClient] Scene Checkout was successfully created.", Array.Empty<object>());
		yield break;
	}

	// Token: 0x06007D54 RID: 32084 RVA: 0x0028B34D File Offset: 0x0028954D
	private IEnumerator<IAsyncJobResult> Job_InitializeCheckoutClient()
	{
		this.m_retriesRemaining = 3;
		bool success = false;
		while (!success)
		{
			int retriesRemaining = this.m_retriesRemaining;
			this.m_retriesRemaining = retriesRemaining - 1;
			if (retriesRemaining <= 0)
			{
				break;
			}
			global::Log.Store.PrintDebug("[HearthstoneCheckout.InitializeCheckoutClient] Create client", Array.Empty<object>());
			this.m_currentState = HearthstoneCheckout.State.Startup;
			yield return new JobDefinition("HearthstoneCheckout.CreateCheckoutClient", this.Job_CreateCheckoutClient(), new IJobDependency[]
			{
				new WaitForLogin()
			});
			global::Log.Store.PrintDebug("[HearthstoneCheckout.InitializeCheckoutClient] Client response: {0}", new object[]
			{
				this.m_clientInitializationResponse
			});
			if (this.m_clientInitializationResponse == HearthstoneCheckout.ClientInitializationResponse.Waiting)
			{
				yield return new HearthstoneCheckout.WaitForClientInitializationResponse(this, 60f);
			}
			switch (this.m_clientInitializationResponse)
			{
			case HearthstoneCheckout.ClientInitializationResponse.Waiting:
				global::Log.Store.PrintError("[HearthstoneCheckout.InitializeCheckoutClient] Client timed out", Array.Empty<object>());
				TelemetryManager.Client().SendBlizzardCheckoutInitializationResult(false, "Checkout Client Initialization Timeout", string.Format("Attempt {0} of {1}", 3 - this.m_retriesRemaining, 3));
				break;
			case HearthstoneCheckout.ClientInitializationResponse.Success:
				global::Log.Store.PrintDebug("[HearthstoneCheckout.InitializeCheckoutClient] Client initialized", Array.Empty<object>());
				TelemetryManager.Client().SendBlizzardCheckoutInitializationResult(true, "", "");
				success = true;
				break;
			case HearthstoneCheckout.ClientInitializationResponse.Fail:
				global::Log.Store.PrintError("[HearthstoneCheckout.InitializeCheckoutClient] Client failed", Array.Empty<object>());
				TelemetryManager.Client().SendBlizzardCheckoutInitializationResult(false, "Checkout Client Initialization Unsuccessful", "");
				break;
			default:
				global::Log.Store.PrintError("[HearthstoneCheckout.InitializeCheckoutClient] Unrecognized initialization response: {0}", new object[]
				{
					this.m_clientInitializationResponse
				});
				break;
			}
		}
		if (success)
		{
			while (!this.m_receivedSdkProducts)
			{
				yield return null;
			}
			this.m_currentState = HearthstoneCheckout.State.Idle;
			this.OnInitializedEvent.Fire();
		}
		else
		{
			this.m_currentState = HearthstoneCheckout.State.Unavailable;
			yield return new JobFailedResult("[HearthstoneCheckout.InitializeCheckoutClient] Failed to initialize checkout client.", Array.Empty<object>());
		}
		yield break;
	}

	// Token: 0x06007D55 RID: 32085 RVA: 0x0028B35C File Offset: 0x0028955C
	public void OnReady()
	{
		if (this.m_currentState == HearthstoneCheckout.State.Initializing)
		{
			global::Log.Store.PrintDebug("[HearthstoneCheckout.OnReady]", Array.Empty<object>());
			this.m_currentState = HearthstoneCheckout.State.Ready;
			this.m_clientInitializationResponse = HearthstoneCheckout.ClientInitializationResponse.Success;
			this.OnReadyEvent.Fire();
			return;
		}
		global::Log.Store.PrintWarning("[HearthstoneCheckout.OnReady] Received ready event while in unexpected state ({0}).", new object[]
		{
			this.m_currentState.ToString()
		});
	}

	// Token: 0x06007D56 RID: 32086 RVA: 0x0028B3C9 File Offset: 0x002895C9
	private void OnSceneViewReady()
	{
		this.CheckoutIsReady = true;
		this.m_checkoutUI.Show();
	}

	// Token: 0x06007D57 RID: 32087 RVA: 0x0028B3DD File Offset: 0x002895DD
	public void OnDisconnect()
	{
		global::Log.Store.PrintDebug("[HearthstoneCheckout.OnDisconnect]", Array.Empty<object>());
		this.SignalCloseNextFrame();
		this.OnDisconnectEvent.Fire();
	}

	// Token: 0x06007D58 RID: 32088 RVA: 0x0028B404 File Offset: 0x00289604
	public void OnCancel()
	{
		global::Log.Store.PrintDebug("[HearthstoneCheckout.OnCancel]", Array.Empty<object>());
		this.SignalCloseNextFrame();
		this.OnCancelEvent.Fire();
	}

	// Token: 0x06007D59 RID: 32089 RVA: 0x0028B42C File Offset: 0x0028962C
	public void OnWindowResized(blz_commerce_scene_window_resize_event_t windowResizeEvent)
	{
		global::Log.Store.PrintDebug("[HearthstoneCheckout.OnWindowResized] (x:{0}, y:{1})", new object[]
		{
			windowResizeEvent.win_size.x,
			windowResizeEvent.win_size.y
		});
		if (this.m_checkoutUI != null)
		{
			this.m_checkoutUI.ResizeTexture(windowResizeEvent.win_size.x, windowResizeEvent.win_size.y);
		}
	}

	// Token: 0x06007D5A RID: 32090 RVA: 0x0028B4A3 File Offset: 0x002896A3
	public void OnBufferUpdate(blz_commerce_scene_buffer_update_event_t bufferUpdateEvent)
	{
		global::Log.Store.PrintDebug("[HearthstoneCheckout.OnBufferUpdate]", Array.Empty<object>());
		if (this.m_checkoutUI != null)
		{
			this.m_checkoutUI.UpdateTexture(bufferUpdateEvent.buffer.bytes);
		}
	}

	// Token: 0x06007D5B RID: 32091 RVA: 0x0028B4DD File Offset: 0x002896DD
	public void OnWindowResizeRequested(blz_commerce_scene_window_resize_requested_event_t windowResizeRequestedEvent)
	{
		global::Log.Store.PrintDebug("[HearthstoneCheckout.OnWindowResizeRequested] Requested Size (x: {0}, y:{1})", new object[]
		{
			windowResizeRequestedEvent.requested_size.x,
			windowResizeRequestedEvent.requested_size.y
		});
	}

	// Token: 0x06007D5C RID: 32092 RVA: 0x0028B51A File Offset: 0x0028971A
	public void OnWindowCloseRequested()
	{
		global::Log.Store.PrintDebug("[HearthstoneCheckout.OnWindowCloseRequested]", Array.Empty<object>());
		this.SignalCloseNextFrame();
	}

	// Token: 0x06007D5D RID: 32093 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void OnCursorChangeRequest()
	{
	}

	// Token: 0x06007D5E RID: 32094 RVA: 0x0028B536 File Offset: 0x00289736
	public void OnExternalLink(blz_commerce_scene_external_link_event_t externalLinkEvent)
	{
		global::Log.Store.PrintDebug("[HearthstoneCheckout.OnExternalLink] URL: {0}", new object[]
		{
			externalLinkEvent.url
		});
		Application.OpenURL(externalLinkEvent.url);
	}

	// Token: 0x06007D5F RID: 32095 RVA: 0x0028B561 File Offset: 0x00289761
	public void OnPurchaseCanceledBeforeSubmit(blz_commerce_purchase_event_t response)
	{
		global::Log.Store.PrintInfo("[HearthstoneCheckout.OnPurchaseCanceledBeforeSubmit]", Array.Empty<object>());
		this.OnCancel();
		this.OnPurchaseCanceledBeforeSubmitEvent.Fire();
	}

	// Token: 0x06007D60 RID: 32096 RVA: 0x0028B588 File Offset: 0x00289788
	public void OnPurchaseFailureBeforeSubmit(blz_commerce_purchase_event_t response)
	{
		this.LogPurchaseResponse("[HearthstoneCheckout.OnPurchaseFailureBeforeSubmit]", response);
		this.OnPurchaseFailureBeforeSubmitEvent.Fire();
	}

	// Token: 0x06007D61 RID: 32097 RVA: 0x0028B5A4 File Offset: 0x002897A4
	public void OnOrderFailure(blz_commerce_purchase_event_t response)
	{
		if (!this.IsTransactionInProgress())
		{
			this.LogPurchaseResponse("[HearthstoneCheckout.OnOrderFailure: Canceled Before Response]", response);
			return;
		}
		this.LogPurchaseResponse("[HearthstoneCheckout.OnOrderFailure]", response);
		this.m_currentState = HearthstoneCheckout.State.Finished;
		this.UpdateTransactionData(response);
		this.OnOrderFailureEvent.Fire(this.m_currentTransaction);
		if (this.m_checkoutUI != null && this.m_checkoutUI.IsShown())
		{
			this.m_checkoutUI.Hide();
		}
		this.OnTransactionProcessCompleted();
	}

	// Token: 0x06007D62 RID: 32098 RVA: 0x0028B61D File Offset: 0x0028981D
	public void OnPurchaseSubmitted(blz_commerce_purchase_event_t response)
	{
		this.LogPurchaseResponse("[HearthstoneCheckout.OnPurchaseSubmitted]", response);
		this.OnPurchaseSubmittedEvent.Fire();
		if (!response.browser_info.is_cancelable)
		{
			this.m_currentState = HearthstoneCheckout.State.InProgress;
		}
	}

	// Token: 0x06007D63 RID: 32099 RVA: 0x0028B64C File Offset: 0x0028984C
	public void OnOrderComplete(blz_commerce_purchase_event_t response)
	{
		if (!this.IsTransactionInProgress())
		{
			this.LogPurchaseResponse("[HearthstoneCheckout.OnOrderComplete: Canceled Before Response]", response);
			return;
		}
		this.LogPurchaseResponse("[HearthstoneCheckout.OnOrderComplete]", response);
		this.m_currentState = HearthstoneCheckout.State.Finished;
		this.UpdateTransactionData(response);
		this.OnOrderCompleteEvent.Fire(this.m_currentTransaction);
		if (this.m_checkoutUI != null && this.m_checkoutUI.IsShown())
		{
			this.m_checkoutUI.Hide();
		}
		this.OnTransactionProcessCompleted();
	}

	// Token: 0x06007D64 RID: 32100 RVA: 0x0028B6C8 File Offset: 0x002898C8
	public void OnOrderPending(blz_commerce_purchase_event_t response)
	{
		if (!this.IsTransactionInProgress())
		{
			this.LogPurchaseResponse("[HearthstoneCheckout.OnOrderPending: Canceled Before Response]", response);
			return;
		}
		this.LogPurchaseResponse("[HearthstoneCheckout.OnOrderPending]", response);
		if (!response.browser_info.is_cancelable)
		{
			this.m_currentState = HearthstoneCheckout.State.InProgress;
		}
		this.m_transactionStart = DateTime.Now;
		this.UpdateTransactionData(response);
		this.OnOrderPendingEvent.Fire(this.m_currentTransaction);
	}

	// Token: 0x06007D65 RID: 32101 RVA: 0x0028B730 File Offset: 0x00289930
	public void OnVirtualCurrencyResponse(blz_commerce_vc_order_event_t virtualCurrencyResponse)
	{
		if (!this.IsTransactionInProgress())
		{
			global::Log.Store.PrintDebug("[HearthstoneCheckout.OnVirtualCurrencyResponse: Canceled Before Response] Status - {0}", new object[]
			{
				virtualCurrencyResponse.http_result
			});
			return;
		}
		global::Log.Store.PrintDebug("[HearthstoneCheckout.OnVirtualCurrencyResponse] Status - {0}", new object[]
		{
			virtualCurrencyResponse.response
		});
		this.m_currentState = HearthstoneCheckout.State.Finished;
		this.OnOrderCompleteEvent.Fire(this.m_currentTransaction);
		this.OnTransactionProcessCompleted();
	}

	// Token: 0x06007D66 RID: 32102 RVA: 0x0028B7A0 File Offset: 0x002899A0
	public void OnGetBalanceResponse(blz_commerce_vc_get_balance_event_t getBalanceEvent)
	{
		if (!getBalanceEvent.http_result.ok)
		{
			global::Log.Store.PrintError("There was an error with the virtual currency 'GetBalance' call! (Http Result Status: {0}", new object[]
			{
				getBalanceEvent.http_result
			});
			return;
		}
		GetBalanceResponse getBalanceResponse = JsonUtility.FromJson<GetBalanceResponse>(getBalanceEvent.response);
		float num;
		if (!float.TryParse(getBalanceResponse.balance.balance, out num))
		{
			global::Log.Store.PrintInfo("[HearthstoneCheckout.OnGetBalanceResponse] Failed to parse balance: {0}", new object[]
			{
				getBalanceResponse.balance.balance
			});
			num = 0f;
		}
		string currencyCode = getBalanceResponse.balance.ledgerId.currencyCode;
		global::Log.Store.PrintInfo("[HearthstoneCheckout.OnGetBalanceResponse] Received balance response.  Currency - {0}   Balance - {1}", new object[]
		{
			currencyCode,
			num
		});
		int i = 0;
		while (i < this.m_virtualCurrencyRequests.Count)
		{
			if (this.m_virtualCurrencyRequests[i].CurrencyCode == currencyCode)
			{
				HearthstoneCheckout.VirtualCurrencyBalanceCallback callback = this.m_virtualCurrencyRequests[i].Callback;
				if (callback != null)
				{
					callback(currencyCode, num);
				}
				this.m_virtualCurrencyRequests.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x06007D67 RID: 32103 RVA: 0x0028B8B0 File Offset: 0x00289AB0
	public void OnGetPersonalizedShopEvent(blz_commerce_catalog_personalized_shop_event_t personalizedShopEvent)
	{
		global::Log.Store.PrintInfo("[HearthstoneCheckout.OnGetPersonalizedShopEvent] Received shop personalization data.", Array.Empty<object>());
		GetPageResponse response = JsonUtility.FromJson<GetPageResponse>(personalizedShopEvent.response);
		if (this.m_personalizedShopResponseCallbacks.Count == 0)
		{
			global::Log.Store.PrintWarning("[HearthstoneCheckout.OnGetPersonalizedShopEvent] Received PersonalizedShop data without any queued callbacks.  This can occur if a response is received after a client reset.", Array.Empty<object>());
			return;
		}
		HearthstoneCheckout.PersonalizedShopResponseCallback personalizedShopResponseCallback = this.m_personalizedShopResponseCallbacks.Dequeue();
		if (personalizedShopResponseCallback != null)
		{
			personalizedShopResponseCallback(response);
		}
	}

	// Token: 0x04006584 RID: 25988
	private static string m_oneStoreKey;

	// Token: 0x04006590 RID: 26000
	private const string kHearthstoneCheckoutPrefab = "HearthstoneCheckout.prefab:da1b8fa18876ab5468bd2aa04a3f2539";

	// Token: 0x04006591 RID: 26001
	private const int kInitializationRetryCount = 3;

	// Token: 0x04006592 RID: 26002
	public static readonly float m_resolutionUpdateInterval = 1f;

	// Token: 0x04006593 RID: 26003
	private IBrowserListener m_browserListener;

	// Token: 0x04006594 RID: 26004
	private IPurchaseListener m_purchaseListener;

	// Token: 0x04006595 RID: 26005
	private IVirtualCurrencyEventListener m_virtualCurrencyEventListener;

	// Token: 0x04006596 RID: 26006
	private ICatalogListener m_catalogListener;

	// Token: 0x04006597 RID: 26007
	private blz_commerce_result_t m_purchaseHandle;

	// Token: 0x04006598 RID: 26008
	private blz_commerce_checkout_browser_params_t m_checkoutParams;

	// Token: 0x04006599 RID: 26009
	private blz_commerce_http_params_t m_httpParams;

	// Token: 0x0400659A RID: 26010
	private blz_commerce_browser_params_t m_browserParams;

	// Token: 0x0400659B RID: 26011
	private readonly Dictionary<int, HearthstoneCheckout.ProductInfo> m_productMap = new Dictionary<int, HearthstoneCheckout.ProductInfo>();

	// Token: 0x0400659C RID: 26012
	private HearthstoneCheckoutUI m_checkoutUI;

	// Token: 0x0400659D RID: 26013
	private bool m_closeRequested;

	// Token: 0x0400659E RID: 26014
	private HearthstoneCheckoutTransactionData m_currentTransaction;

	// Token: 0x0400659F RID: 26015
	private HearthstoneCheckout.State m_currentState;

	// Token: 0x040065A0 RID: 26016
	private Vector2 m_screenResolution;

	// Token: 0x040065A1 RID: 26017
	private float m_elapsedTimeSinceResolutionCheck;

	// Token: 0x040065A2 RID: 26018
	private float m_elapsedTimeSinceShown;

	// Token: 0x040065A3 RID: 26019
	private const float kInProgressBackgroundableDelay = 10f;

	// Token: 0x040065A4 RID: 26020
	private DateTime m_transactionStart = DateTime.Now;

	// Token: 0x040065A5 RID: 26021
	private int m_retriesRemaining = 3;

	// Token: 0x040065A6 RID: 26022
	private string[] m_productCatalog;

	// Token: 0x040065A7 RID: 26023
	private string m_currencyCode;

	// Token: 0x040065A8 RID: 26024
	private List<HearthstoneCheckout.VirtualCurrencyRequest> m_virtualCurrencyRequests = new List<HearthstoneCheckout.VirtualCurrencyRequest>();

	// Token: 0x040065A9 RID: 26025
	private Queue<HearthstoneCheckout.PersonalizedShopResponseCallback> m_personalizedShopResponseCallbacks = new Queue<HearthstoneCheckout.PersonalizedShopResponseCallback>();

	// Token: 0x040065AA RID: 26026
	private string m_clientID;

	// Token: 0x040065AB RID: 26027
	private HearthstoneCheckout.ClientInitializationResponse m_clientInitializationResponse;

	// Token: 0x040065AC RID: 26028
	private bool? m_overrideEndpointToProduction;

	// Token: 0x040065AD RID: 26029
	private BlizzardCommerceListener m_commerceListener;

	// Token: 0x040065AE RID: 26030
	private BlizzardCommerceLogger m_commerceLogger;

	// Token: 0x040065AF RID: 26031
	private blz_commerce_sdk_t m_commerceSdk;

	// Token: 0x040065B0 RID: 26032
	private IDisposable androidSceneFrameLayout;

	// Token: 0x040065B1 RID: 26033
	private bool m_receivedSdkProducts;

	// Token: 0x040065B3 RID: 26035
	private blz_commerce_pair_t m_commercePairsArray;

	// Token: 0x02002560 RID: 9568
	// (Invoke) Token: 0x060132D9 RID: 78553
	public delegate void VirtualCurrencyBalanceCallback(string currencyCode, float balance);

	// Token: 0x02002561 RID: 9569
	// (Invoke) Token: 0x060132DD RID: 78557
	public delegate void PersonalizedShopResponseCallback(GetPageResponse response);

	// Token: 0x02002562 RID: 9570
	public enum State
	{
		// Token: 0x0400ED43 RID: 60739
		Startup,
		// Token: 0x0400ED44 RID: 60740
		Idle,
		// Token: 0x0400ED45 RID: 60741
		Initializing,
		// Token: 0x0400ED46 RID: 60742
		Ready,
		// Token: 0x0400ED47 RID: 60743
		InProgress,
		// Token: 0x0400ED48 RID: 60744
		InProgress_Backgroundable,
		// Token: 0x0400ED49 RID: 60745
		Finished,
		// Token: 0x0400ED4A RID: 60746
		Unavailable
	}

	// Token: 0x02002563 RID: 9571
	private struct VirtualCurrencyRequest
	{
		// Token: 0x060132E0 RID: 78560 RVA: 0x0052BB10 File Offset: 0x00529D10
		public VirtualCurrencyRequest(string currencyCode, HearthstoneCheckout.VirtualCurrencyBalanceCallback callback)
		{
			this.CurrencyCode = currencyCode;
			this.Callback = callback;
		}

		// Token: 0x0400ED4B RID: 60747
		public string CurrencyCode;

		// Token: 0x0400ED4C RID: 60748
		public HearthstoneCheckout.VirtualCurrencyBalanceCallback Callback;
	}

	// Token: 0x02002564 RID: 9572
	public struct ProductInfo
	{
		// Token: 0x060132E1 RID: 78561 RVA: 0x0052BB20 File Offset: 0x00529D20
		public ProductInfo(string productID, string title, string price)
		{
			this.ProductID = productID;
			this.Title = title;
			this.Price = price;
		}

		// Token: 0x0400ED4D RID: 60749
		public string ProductID;

		// Token: 0x0400ED4E RID: 60750
		public string Title;

		// Token: 0x0400ED4F RID: 60751
		public string Price;
	}

	// Token: 0x02002565 RID: 9573
	private class WaitForClientInitializationResponse : IJobDependency, IAsyncJobResult
	{
		// Token: 0x060132E2 RID: 78562 RVA: 0x0052BB37 File Offset: 0x00529D37
		public WaitForClientInitializationResponse(HearthstoneCheckout hearthstoneCheckout, float timeoutDuration)
		{
			this.m_hearthstoneCheckout = hearthstoneCheckout;
			this.m_timeoutTimestamp = Time.realtimeSinceStartup + timeoutDuration;
		}

		// Token: 0x060132E3 RID: 78563 RVA: 0x0052BB53 File Offset: 0x00529D53
		public bool IsReady()
		{
			return this.m_hearthstoneCheckout.m_clientInitializationResponse != HearthstoneCheckout.ClientInitializationResponse.Waiting || this.m_timeoutTimestamp <= Time.realtimeSinceStartup;
		}

		// Token: 0x0400ED50 RID: 60752
		private HearthstoneCheckout m_hearthstoneCheckout;

		// Token: 0x0400ED51 RID: 60753
		private float m_timeoutTimestamp;
	}

	// Token: 0x02002566 RID: 9574
	private enum ClientInitializationResponse
	{
		// Token: 0x0400ED53 RID: 60755
		Waiting,
		// Token: 0x0400ED54 RID: 60756
		Success,
		// Token: 0x0400ED55 RID: 60757
		Fail
	}
}
