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

public class HearthstoneCheckout : ICatalogListener, IBrowserListener, IPurchaseListener, IVirtualCurrencyEventListener, IService, IHasUpdate
{
	public delegate void VirtualCurrencyBalanceCallback(string currencyCode, float balance);

	public delegate void PersonalizedShopResponseCallback(GetPageResponse response);

	public enum State
	{
		Startup,
		Idle,
		Initializing,
		Ready,
		InProgress,
		InProgress_Backgroundable,
		Finished,
		Unavailable
	}

	private struct VirtualCurrencyRequest
	{
		public string CurrencyCode;

		public VirtualCurrencyBalanceCallback Callback;

		public VirtualCurrencyRequest(string currencyCode, VirtualCurrencyBalanceCallback callback)
		{
			CurrencyCode = currencyCode;
			Callback = callback;
		}
	}

	public struct ProductInfo
	{
		public string ProductID;

		public string Title;

		public string Price;

		public ProductInfo(string productID, string title, string price)
		{
			ProductID = productID;
			Title = title;
			Price = price;
		}
	}

	private class WaitForClientInitializationResponse : IJobDependency, IAsyncJobResult
	{
		private HearthstoneCheckout m_hearthstoneCheckout;

		private float m_timeoutTimestamp;

		public WaitForClientInitializationResponse(HearthstoneCheckout hearthstoneCheckout, float timeoutDuration)
		{
			m_hearthstoneCheckout = hearthstoneCheckout;
			m_timeoutTimestamp = Time.realtimeSinceStartup + timeoutDuration;
		}

		public bool IsReady()
		{
			if (m_hearthstoneCheckout.m_clientInitializationResponse == ClientInitializationResponse.Waiting)
			{
				return m_timeoutTimestamp <= Time.realtimeSinceStartup;
			}
			return true;
		}
	}

	private enum ClientInitializationResponse
	{
		Waiting,
		Success,
		Fail
	}

	private static string m_oneStoreKey;

	private const string kHearthstoneCheckoutPrefab = "HearthstoneCheckout.prefab:da1b8fa18876ab5468bd2aa04a3f2539";

	private const int kInitializationRetryCount = 3;

	public static readonly float m_resolutionUpdateInterval = 1f;

	private IBrowserListener m_browserListener;

	private IPurchaseListener m_purchaseListener;

	private IVirtualCurrencyEventListener m_virtualCurrencyEventListener;

	private ICatalogListener m_catalogListener;

	private blz_commerce_result_t m_purchaseHandle;

	private blz_commerce_checkout_browser_params_t m_checkoutParams;

	private blz_commerce_http_params_t m_httpParams;

	private blz_commerce_browser_params_t m_browserParams;

	private readonly Dictionary<int, ProductInfo> m_productMap = new Dictionary<int, ProductInfo>();

	private HearthstoneCheckoutUI m_checkoutUI;

	private bool m_closeRequested;

	private HearthstoneCheckoutTransactionData m_currentTransaction;

	private State m_currentState;

	private Vector2 m_screenResolution;

	private float m_elapsedTimeSinceResolutionCheck;

	private float m_elapsedTimeSinceShown;

	private const float kInProgressBackgroundableDelay = 10f;

	private DateTime m_transactionStart = DateTime.Now;

	private int m_retriesRemaining = 3;

	private string[] m_productCatalog;

	private string m_currencyCode;

	private List<VirtualCurrencyRequest> m_virtualCurrencyRequests = new List<VirtualCurrencyRequest>();

	private Queue<PersonalizedShopResponseCallback> m_personalizedShopResponseCallbacks = new Queue<PersonalizedShopResponseCallback>();

	private string m_clientID;

	private ClientInitializationResponse m_clientInitializationResponse;

	private bool? m_overrideEndpointToProduction;

	private BlizzardCommerceListener m_commerceListener;

	private BlizzardCommerceLogger m_commerceLogger;

	private blz_commerce_sdk_t m_commerceSdk;

	private IDisposable androidSceneFrameLayout;

	private bool m_receivedSdkProducts;

	private blz_commerce_pair_t m_commercePairsArray;

	public static string OneStoreKey
	{
		set
		{
		}
	}

	public CheckoutEvent OnInitializedEvent { get; private set; }

	public CheckoutEvent OnReadyEvent { get; private set; }

	public CheckoutEvent OnDisconnectEvent { get; private set; }

	public CheckoutEvent OnCancelEvent { get; private set; }

	public CheckoutEvent OnCloseEvent { get; private set; }

	public CheckoutEvent OnPurchaseCanceledBeforeSubmitEvent { get; private set; }

	public CheckoutEvent OnPurchaseFailureBeforeSubmitEvent { get; private set; }

	public CheckoutEvent OnPurchaseSubmittedEvent { get; private set; }

	public TransactionCheckoutEvent OnOrderPendingEvent { get; private set; }

	public TransactionCheckoutEvent OnOrderFailureEvent { get; private set; }

	public TransactionCheckoutEvent OnOrderCompleteEvent { get; private set; }

	public HearthstoneCheckoutUI CheckoutUi => m_checkoutUI;

	public bool CheckoutIsReady { get; private set; }

	public blz_commerce_sdk_t Sdk => m_commerceSdk;

	public State CurrentState => m_currentState;

	public bool HasProductCatalog => m_productCatalog != null;

	public bool HasClientID => m_clientID != null;

	public bool HasCurrencyCode => m_currencyCode != null;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		if (Vars.Key("Commerce.OverrideEndpointToProduction").HasValue)
		{
			m_overrideEndpointToProduction = Vars.Key("Commerce.OverrideEndpointToProduction").GetBool(def: true);
		}
		OnInitializedEvent = new CheckoutEvent();
		OnReadyEvent = new CheckoutEvent();
		OnDisconnectEvent = new CheckoutEvent();
		OnCancelEvent = new CheckoutEvent();
		OnCloseEvent = new CheckoutEvent();
		OnPurchaseCanceledBeforeSubmitEvent = new CheckoutEvent();
		OnPurchaseFailureBeforeSubmitEvent = new CheckoutEvent();
		OnPurchaseSubmittedEvent = new CheckoutEvent();
		OnOrderPendingEvent = new TransactionCheckoutEvent();
		OnOrderFailureEvent = new TransactionCheckoutEvent();
		OnOrderCompleteEvent = new TransactionCheckoutEvent();
		m_commerceListener = new BlizzardCommerceListener();
		string text = null;
		try
		{
			m_commerceListener.SceneEventListener.Ready += OnSceneViewReady;
			m_commerceListener.SceneEventListener.Disconnect += OnDisconnect;
			m_commerceListener.SceneEventListener.Cancel += OnCancel;
			m_commerceListener.SceneEventListener.WindowResize += OnWindowResized;
			m_commerceListener.SceneEventListener.BufferUpdate += OnBufferUpdate;
			m_commerceListener.SceneEventListener.WindowResizeRequested += OnWindowResizeRequested;
			m_commerceListener.SceneEventListener.WindowCloseRequest += OnWindowCloseRequested;
			m_commerceListener.SceneEventListener.CursorChanged += OnCursorChangeRequest;
			m_commerceListener.SceneEventListener.ExternalLink += OnExternalLink;
			m_commerceListener.PurchaseEventListener.Cancel += OnPurchaseCanceledBeforeSubmit;
			m_commerceListener.PurchaseEventListener.Failure += OnOrderFailure;
			m_commerceListener.PurchaseEventListener.Pending += OnOrderPending;
			m_commerceListener.PurchaseEventListener.Successful += OnOrderComplete;
			m_commerceListener.VirtualCurrencyEventListener.GetBalance += OnGetBalanceResponse;
			m_commerceListener.VirtualCurrencyEventListener.PurchaseEvent += OnVirtualCurrencyResponse;
			m_commerceListener.CatalogEventListener.PersonalizedShopReceived += OnGetPersonalizedShopEvent;
			m_commerceListener.CatalogEventListener.ProductLoaded += ProductLoaded;
		}
		catch (Exception ex)
		{
			m_currentState = State.Unavailable;
			text = $"Failed to initialize HearthstoneCheckout: {ex}";
			TelemetryManager.Client().SendBlizzardCheckoutInitializationResult(success: false, "Checkout Library Exception.", ex.ToString());
		}
		if (!string.IsNullOrEmpty(text))
		{
			yield return new JobFailedResult(text);
		}
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset += OnReset;
		}
		JobDefinition jobDefinition = new JobDefinition("HearthstoneCheckout.LoadCheckoutUI", Job_LoadCheckoutUI(), JobFlags.StartImmediately);
		Processor.QueueJob(jobDefinition);
		Processor.QueueJob(new JobDefinition("HearthstoneCheckout.InitializeCheckoutClient", Job_InitializeCheckoutClient(), jobDefinition.CreateDependency(), new WaitForCheckoutConfiguration()));
	}

	private void LoadProducts()
	{
		GetProductsByStoreIdRequest getProductsByStoreIdRequest = new GetProductsByStoreIdRequest();
		getProductsByStoreIdRequest.locale = Localization.GetLocaleName();
		getProductsByStoreIdRequest.gameServiceRegionId = (int)BattleNet.GetCurrentRegion();
		getProductsByStoreIdRequest.storeId = 6;
		getProductsByStoreIdRequest.paginationSize = 200;
		battlenet_commerce.blz_catalog_load_products(m_commerceSdk, JsonUtility.ToJson(getProductsByStoreIdRequest));
	}

	private void ProductLoaded(GetProductsByStoreIdResponse productLoadEvent)
	{
		if (productLoadEvent == null)
		{
			Log.Store.PrintError("Received a product from server that was not defined!");
			return;
		}
		foreach (Product product in productLoadEvent.products)
		{
			if (product.prices.Count <= 0)
			{
				Log.Store.PrintError("The product received had no prices given.");
				continue;
			}
			if (product.prices.Count > 1)
			{
				Log.Store.PrintError("The product received had multiple prices! (Product ID: {0} (Price count {1})", product.productId, product.prices.Count);
			}
			m_productMap[product.productId] = new ProductInfo(product.productId.ToString(), product.localization.name, product.prices[0].localizedCurrentPrice);
		}
		m_receivedSdkProducts = true;
		battlenet_commerce.blz_checkout_resume(m_commerceSdk);
	}

	public Type[] GetDependencies()
	{
		return new Type[4]
		{
			typeof(Network),
			typeof(LoginManager),
			typeof(IAssetLoader),
			typeof(ILoginService)
		};
	}

	public void Shutdown()
	{
		Log.Store.PrintDebug("[HearthstoneCheckout.Shutdown]");
		m_currentState = State.Startup;
		m_currentTransaction = null;
		m_productCatalog = null;
		m_virtualCurrencyRequests.Clear();
		DestroyCheckoutUI();
		DisposeCurrentCheckoutClient();
		DisposeListeners();
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset -= OnReset;
		}
	}

	public static bool IsAvailable()
	{
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out var service) && service.CurrentState != State.Unavailable)
		{
			return service.CurrentState != State.Startup;
		}
		return false;
	}

	public static bool IsClientCreationInProgress()
	{
		ServiceLocator.ServiceState serviceState = HearthstoneServices.GetServiceState<HearthstoneCheckout>();
		if (serviceState == ServiceLocator.ServiceState.Error || serviceState == ServiceLocator.ServiceState.Invalid)
		{
			return false;
		}
		if (!HearthstoneServices.TryGet<HearthstoneCheckout>(out var service))
		{
			return true;
		}
		return service.CurrentState == State.Startup;
	}

	public void ShowCheckout(long productID, string currencyCode, uint quantity)
	{
		Processor.QueueJob("ShowCheckout", Job_ShowCheckout(productID, currencyCode, quantity)).AddJobFinishedEventListener(OnPurchaseJobFinished);
	}

	public void PurchaseWithVirtualCurrency(long productID, string currencyCode, uint quantity)
	{
		Processor.QueueJob("PurchaseWithVirtualCurrency", Job_PurchaseWithVirtualCurrency(productID, currencyCode, quantity)).AddJobFinishedEventListener(OnPurchaseJobFinished);
	}

	public bool GetVirtualCurrencyBalance(string currencyCode, VirtualCurrencyBalanceCallback callback)
	{
		if (m_commerceSdk != null)
		{
			GetBalanceRequest getBalanceRequest = new GetBalanceRequest();
			getBalanceRequest.currencyCode = currencyCode;
			battlenet_commerce.blz_commerce_vc_get_balance(m_commerceSdk, JsonUtility.ToJson(getBalanceRequest));
			m_virtualCurrencyRequests.Add(new VirtualCurrencyRequest(currencyCode, callback));
			return true;
		}
		Log.Store.PrintWarning("[HearthstoneCheckout.GetVirtualCurrencyBalance] Cannot get virtual currency balance because the checkout client isn't initialized.");
		return false;
	}

	public bool TryGetProductInfo(string productID, out ProductInfo productInfo)
	{
		int result = 0;
		if (!int.TryParse(productID, out result))
		{
			Log.Store.PrintError("[HearthstoneCheckout.GetProductInfo] The store ID passed in was not an integer and therefore could not be mapped. (ID: {0})", productID);
			productInfo = new ProductInfo(null, null, null);
			return false;
		}
		if (!m_productMap.ContainsKey(result))
		{
			Log.Store.PrintError("[HearthstoneCheckout.GetProductInfo] The product map did not contain the product ID {0}.", result.ToString());
			productInfo = new ProductInfo(null, null, null);
			return false;
		}
		productInfo = m_productMap[result];
		return true;
	}

	public void GetPersonalizedShopData(string pageId, PersonalizedShopResponseCallback callback)
	{
		if (callback == null)
		{
			Log.Store.PrintWarning("[HearthstoneCheckout.GetPersonalizedShopData] Callback cannot be null.");
		}
		else if (!Network.IsLoggedIn())
		{
			Log.Store.PrintWarning("[HearthstoneCheckout.GetPersonalizedShopData] Cannot get personalized shop data because the user is off-line.");
		}
		else if (m_commerceSdk != null)
		{
			m_personalizedShopResponseCallbacks.Enqueue(callback);
			GetPageRequest obj = new GetPageRequest
			{
				pageId = pageId,
				locale = Localization.GetLocaleName(),
				gameServiceRegionId = (int)BattleNet.GetCurrentRegion()
			};
			battlenet_commerce.blz_catalog_personalized_shop(m_commerceSdk, JsonUtility.ToJson(obj));
		}
		else
		{
			Log.Store.PrintWarning("[HearthstoneCheckout.GetPersonalizedShopData] Cannot get personalized shop data because the checkout client isn't initialized.");
		}
	}

	public bool IsUIShown()
	{
		if (m_checkoutUI != null)
		{
			return m_checkoutUI.IsShown();
		}
		return false;
	}

	public float GetShownTime()
	{
		return m_elapsedTimeSinceShown;
	}

	public void RequestClose()
	{
		switch (m_currentState)
		{
		case State.Initializing:
		case State.Ready:
			OnCancel();
			break;
		case State.InProgress_Backgroundable:
		case State.Finished:
			SignalCloseNextFrame();
			break;
		case State.Idle:
			Log.Store.PrintWarning("[HearthstoneCheckout.RequestClose] HearthstoneCheckout received a request close when it should already be closed.  Attempting to close again...");
			SignalCloseNextFrame();
			break;
		case State.InProgress:
			break;
		}
	}

	public bool ShouldBlockInput()
	{
		return IsUIShown();
	}

	public void Update()
	{
		if (m_checkoutUI != null && m_checkoutUI.HasCheckoutMesh)
		{
			m_elapsedTimeSinceResolutionCheck += Time.deltaTime;
			if (m_elapsedTimeSinceResolutionCheck > m_resolutionUpdateInterval)
			{
				ScreenResolutionUpdate();
				m_elapsedTimeSinceResolutionCheck = 0f;
			}
		}
		if (IsUIShown())
		{
			m_elapsedTimeSinceShown += Time.deltaTime;
		}
		if (m_closeRequested)
		{
			if (m_currentTransaction != null && !m_currentTransaction.IsVCPurchase)
			{
				battlenet_commerce.blz_commerce_browser_send_event(m_commerceSdk, blz_commerce_browser_event_type_t.WINDOW_CLOSE, IntPtr.Zero);
			}
			if (m_currentState != State.InProgress && m_currentState != State.InProgress_Backgroundable)
			{
				ClearTransaction();
			}
			else if (IsUIShown())
			{
				m_checkoutUI.Hide();
			}
			m_closeRequested = false;
			OnCloseEvent.Fire();
		}
		else
		{
			if (m_commerceSdk != null)
			{
				battlenet_commerce.blz_commerce_update(m_commerceSdk);
			}
			State currentState = m_currentState;
			if (currentState == State.InProgress && (float)(DateTime.Now - m_transactionStart).Seconds >= 10f)
			{
				m_currentState = State.InProgress_Backgroundable;
			}
		}
	}

	public void SetProductCatalog(string[] productCatalog)
	{
		m_productCatalog = productCatalog;
	}

	public void SetClientID(string clientID)
	{
		m_clientID = clientID;
	}

	public void SetCurrencyCode(string currencyCode)
	{
		m_currencyCode = currencyCode;
	}

	private void OnReset()
	{
		m_currentState = State.Startup;
		m_productCatalog = null;
		m_clientID = null;
		m_personalizedShopResponseCallbacks.Clear();
		m_virtualCurrencyRequests.Clear();
		DestroyCheckoutUI();
		DisposeCurrentCheckoutClient();
		JobDefinition jobDefinition = new JobDefinition("HearthstoneCheckout.LoadCheckoutUI", Job_LoadCheckoutUI(), JobFlags.StartImmediately, new WaitForGameDownloadManagerState());
		Processor.QueueJob(jobDefinition);
		Processor.QueueJob(new JobDefinition("HearthstoneCheckout.InitializeCheckoutClient", Job_InitializeCheckoutClient(), jobDefinition.CreateDependency(), new WaitForCheckoutConfiguration()));
	}

	private blz_commerce_checkout_browser_params_t CreateCheckoutParams(string ssoToken)
	{
		GetBrowserPath();
		string titleVersionString = GetTitleVersionString();
		HearthstoneApplication.IsPublic();
		if (m_overrideEndpointToProduction.HasValue)
		{
			_ = m_overrideEndpointToProduction.Value;
		}
		if (m_purchaseListener == null)
		{
			m_purchaseListener = new PurchaseListener(this);
		}
		if (m_browserListener == null)
		{
			m_browserListener = new BrowserListener(this);
		}
		if (m_virtualCurrencyEventListener == null)
		{
			m_virtualCurrencyEventListener = new VirtualCurrencyEventListener(this);
		}
		if (m_catalogListener == null)
		{
			m_catalogListener = new CatalogListener(this);
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

	private string GetTitleVersionString()
	{
		string text = "20.4";
		string text2 = "0";
		int num = 84593;
		string platformString = GetPlatformString();
		return $"{text}.{text2}.{num}-{platformString}";
	}

	private string GetPlatformString()
	{
		return PlatformSettings.RuntimeOS switch
		{
			OSCategory.PC => "Windows", 
			OSCategory.Mac => "MacOS", 
			OSCategory.Android => GetAndroidPlatformString(), 
			OSCategory.iOS => GetIOSPlatformString(), 
			_ => "UnknownOS", 
		};
	}

	private string GetAndroidPlatformString()
	{
		return AndroidDeviceSettings.Get().GetAndroidStore() switch
		{
			AndroidStore.GOOGLE => "Google", 
			AndroidStore.AMAZON => "Amazon", 
			AndroidStore.BLIZZARD => "AndroidBattlenet", 
			AndroidStore.HUAWEI => "Huawei", 
			_ => "UnkownAndroid", 
		};
	}

	private string GetIOSPlatformString()
	{
		if (PlatformSettings.LocaleVariant != LocaleVariant.China)
		{
			return "iOS";
		}
		return "iOSCN";
	}

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
			blz_commerce_browser_purchase_t.externalTransactionId = GenerateExternalTransactionID();
		}
		return new blz_commerce_purchase_t
		{
			currency_id = currencyCode,
			product_id = productID.ToString(),
			browser_purchase = blz_commerce_browser_purchase_t
		};
	}

	private string GetLocaleForURL(string locale)
	{
		return locale;
	}

	private string GenerateExternalTransactionID()
	{
		if (m_commerceSdk == null)
		{
			Log.Store.PrintError("[HearthstoneCheckout.GenerateExternalTransactionID] Checkout Client must exists to generate an external transaction ID.");
			return null;
		}
		constants.BnetRegion bnetRegion = BattleNet.GetAccountRegion();
		if ((uint)(bnetRegion - 1) > 4u)
		{
			bnetRegion = constants.BnetRegion.REGION_PTR;
		}
		return battlenet_commerce.blz_commerce_generate_transaction_id(14, (int)bnetRegion);
	}

	private static string GetBrowserPath()
	{
		if (Application.isEditor)
		{
			switch (PlatformSettings.RuntimeOS)
			{
			case OSCategory.PC:
				return Path.Combine(Application.dataPath, "../Contrib/BlizzardCommerce/windows/BlizzardBrowser/release/BlizzardBrowser").Replace('/', '\\');
			case OSCategory.Mac:
				return Path.Combine(Application.dataPath, "../Contrib/BlizzardCommerce/macosx/BlizzardBrowser/release/BlizzardBrowser").Replace('/', '\\');
			}
		}
		return Application.dataPath;
	}

	private void ScreenResolutionUpdate()
	{
		if (m_checkoutUI != null && m_checkoutUI.IsShown() && m_commerceSdk != null && (m_screenResolution.x != (float)Screen.width || m_screenResolution.y != (float)Screen.height))
		{
			m_checkoutUI.DetermineBrowserSize();
			blz_commerce_vec2d_t obj = new blz_commerce_vec2d_t
			{
				x = m_checkoutUI.BrowserWidth,
				y = m_checkoutUI.BrowserHeight
			};
			battlenet_commerce.blz_commerce_browser_send_event(m_commerceSdk, blz_commerce_browser_event_type_t.RESIZE_WINDOW, blz_commerce_vec2d_t.getCPtr(obj).Handle);
			m_screenResolution.x = Screen.width;
			m_screenResolution.y = Screen.height;
		}
	}

	private void UpdateTransactionData(blz_commerce_purchase_event_t response)
	{
		if (!string.IsNullOrEmpty(response.product_id))
		{
			long result = 0L;
			long.TryParse(response.product_id, out result);
			m_currentTransaction.ProductID = result;
		}
		m_currentTransaction.TransactionID = response.transaction_id;
		m_currentTransaction.ErrorCodes = response.error_code;
	}

	private void LogPurchaseResponse(string tag, blz_commerce_purchase_event_t response)
	{
		Log.Store.PrintDebug("{0} Status - {1}", tag, response.status.ToString());
		string error_code = response.error_code;
		if (!string.IsNullOrEmpty(error_code))
		{
			Log.Store.PrintError("[HearthstoneCheckout] CHECKOUT ERROR: {0}", error_code);
		}
	}

	private void SetScreenResolution()
	{
		m_screenResolution = new Vector2(Screen.width, Screen.height);
	}

	private void SignalCloseNextFrame()
	{
		m_closeRequested = true;
	}

	private void ClearTransaction()
	{
		if (IsUIShown())
		{
			m_checkoutUI.Hide();
		}
		if (m_purchaseHandle != null)
		{
			m_purchaseHandle.Dispose();
			m_purchaseHandle = null;
		}
		m_closeRequested = false;
		m_currentTransaction = null;
		if (m_currentState != State.Unavailable)
		{
			m_currentState = State.Idle;
		}
	}

	private void DestroyCheckoutUI()
	{
		if (m_checkoutUI != null && m_checkoutUI.gameObject != null)
		{
			UnityEngine.Object.Destroy(m_checkoutUI.gameObject);
			m_checkoutUI = null;
		}
	}

	private void OnTransactionProcessCompleted()
	{
		if (!IsUIShown())
		{
			SignalCloseNextFrame();
		}
	}

	private void OnPurchaseJobFinished(JobDefinition job, bool success)
	{
		if (!success && m_currentState != State.InProgress && m_currentState != State.InProgress_Backgroundable && m_currentState != State.Finished)
		{
			OnPurchaseFailureBeforeSubmit(new blz_commerce_purchase_event_t());
			RequestClose();
		}
	}

	private void OnOutsideClick()
	{
		if (StoreManager.Get().CanTapOutConfirmationUI())
		{
			RequestClose();
		}
	}

	private void DisposeCurrentCheckoutClient()
	{
		if (m_commerceSdk != null)
		{
			m_commerceSdk.Dispose();
			m_commerceSdk = null;
		}
		if (m_purchaseHandle != null)
		{
			m_purchaseHandle.Dispose();
			m_purchaseHandle = null;
		}
		if (m_checkoutParams != null)
		{
			m_checkoutParams.Dispose();
			m_checkoutParams = null;
		}
	}

	private void DisposeListeners()
	{
		if (m_browserListener != null)
		{
			m_browserListener = null;
		}
		if (m_purchaseListener != null)
		{
			m_purchaseListener = null;
		}
		if (m_virtualCurrencyEventListener != null)
		{
			m_virtualCurrencyEventListener = null;
		}
		if (m_catalogListener != null)
		{
			m_catalogListener = null;
		}
	}

	private bool IsTransactionInProgress()
	{
		if (m_currentTransaction != null)
		{
			if (m_currentState != State.Initializing && m_currentState != State.Ready && m_currentState != State.InProgress)
			{
				return m_currentState == State.InProgress_Backgroundable;
			}
			return true;
		}
		return false;
	}

	private IEnumerator<IAsyncJobResult> Job_ShowCheckout(long productID, string currencyCode, uint quantity)
	{
		if (m_currentState != State.Idle)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.ShowCheckout] Cannot initiate checkout while not in the idle state.  Current State - {0}", m_currentState);
		}
		if (m_commerceSdk == null)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.ShowCheckout] Cannot show checkout because the checkout client isn't initialized.");
		}
		if (m_checkoutUI == null)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.ShowCheckout] Cannot show checkout because the UI isn't loaded.");
		}
		Log.Store.PrintDebug("[HearthstoneCheckout.ShowCheckout]");
		m_currentState = State.Initializing;
		m_elapsedTimeSinceResolutionCheck = 0f;
		m_elapsedTimeSinceShown = 0f;
		m_checkoutUI.GenerateMeshes();
		m_currentTransaction = new HearthstoneCheckoutTransactionData(productID, currencyCode, quantity, isVCPurchase: false);
		yield return new WaitForLogin();
		GenerateSSOToken generateToken = new GenerateSSOToken();
		yield return generateToken;
		blz_commerce_purchase_t purchase = CreatePurchaseRequest(m_currentTransaction.ProductID, m_currentTransaction.CurrencyCode, m_currentTransaction.Quantity, generateToken.Token, generateExternalTransactionID: false);
		m_purchaseHandle = battlenet_commerce.blz_checkout_purchase(m_commerceSdk, purchase);
		if (m_purchaseHandle == null)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.ShowCheckout] Failed to obtain purchase handle.");
		}
		Log.Store.PrintDebug("[HearthstoneCheckout.ShowCheckout]Purchase was successfully initiated.");
		m_checkoutUI.InitiateCheckout(this);
	}

	private IEnumerator<IAsyncJobResult> Job_PurchaseWithVirtualCurrency(long productID, string currencyCode, uint quantity)
	{
		if (m_currentState != State.Idle)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.PurchaseWithVirtualCurrency] Cannot initiate purchase while not in the idle state.  Current State - {0}", m_currentState);
		}
		if (m_commerceSdk == null)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.PurchaseWithVirtualCurrency] Cannot initiate purchase because the checkout client isn't initialized.");
		}
		GenerateSSOToken generateSSOToken = new GenerateSSOToken();
		yield return generateSSOToken;
		if (!generateSSOToken.HasToken)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.PurchaseWithVirtualCurrency] Cannot show checkout because it didn't receive an SSO token.");
		}
		yield return new WaitForLogin();
		m_currentState = State.Initializing;
		m_currentTransaction = new HearthstoneCheckoutTransactionData(productID, currencyCode, quantity, isVCPurchase: true);
		m_currentTransaction.TransactionID = GenerateExternalTransactionID();
		PlaceOrderWithVCRequest placeOrderWithVCRequest = new PlaceOrderWithVCRequest();
		placeOrderWithVCRequest.currencyCode = m_currentTransaction.CurrencyCode;
		placeOrderWithVCRequest.externalTransactionId = m_currentTransaction.TransactionID;
		placeOrderWithVCRequest.gameServiceRegionId = (int)BattleNet.GetCurrentRegion();
		placeOrderWithVCRequest.productId = (int)m_currentTransaction.ProductID;
		placeOrderWithVCRequest.quantity = (int)m_currentTransaction.Quantity;
		placeOrderWithVCRequest.pointOfSaleId = 1465140039;
		battlenet_commerce.blz_commerce_vc_purchase(m_commerceSdk, JsonUtility.ToJson(placeOrderWithVCRequest));
	}

	private IEnumerator<IAsyncJobResult> Job_LoadCheckoutUI()
	{
		if (m_checkoutUI != null)
		{
			Log.Store.PrintError("[HearthstoneCheckout.LoadCheckoutUI] Checkout UI already exists!  Please destroy the existing UI before creating a new one.");
			yield break;
		}
		while (!Network.IsLoggedIn())
		{
			yield return null;
		}
		InstantiatePrefab loadCheckoutUI = new InstantiatePrefab("HearthstoneCheckout.prefab:da1b8fa18876ab5468bd2aa04a3f2539");
		yield return loadCheckoutUI;
		m_checkoutUI = loadCheckoutUI.InstantiatedPrefab.GetComponent<HearthstoneCheckoutUI>();
		loadCheckoutUI.InstantiatedPrefab.AddComponent<HSDontDestroyOnLoad>();
		m_checkoutUI.Hide();
		m_checkoutUI.DetermineBrowserSize();
		while (string.IsNullOrEmpty(m_clientID))
		{
			yield return null;
		}
		GenerateSSOToken generateSSOToken = new GenerateSSOToken();
		yield return generateSSOToken;
		if (!generateSSOToken.HasToken)
		{
			yield return new JobFailedResult("[HearthstoneCheckout.LoadCheckoutUI] Cannot show checkout because it didn't receive an SSO token.");
		}
		m_currentState = State.Initializing;
		m_commerceSdk = battlenet_commerce.blz_commerce_create().sdk;
		m_commerceLogger = new BlizzardCommerceLogger();
		List<blz_commerce_pair_t> list = new List<blz_commerce_pair_t>(6);
		m_httpParams = new blz_commerce_http_params_t();
		m_httpParams.title_code = "WTCG";
		m_httpParams.title_version = GetTitleVersionString();
		m_httpParams.region = (int)BattleNet.GetCurrentRegion();
		m_httpParams.environment = (HearthstoneApplication.IsPublic() ? blz_commerce_http_environment_t.BLZ_COMMERCE_HTTP_ENVIRONMENT_PROD : blz_commerce_http_environment_t.BLZ_COMMERCE_HTTP_ENVIRONMENT_QA);
		battlenet_commerce.blz_commerce_add_listener(m_commerceSdk, IntPtr.Zero, m_commerceListener);
		battlenet_commerce.blz_commerce_register_log(IntPtr.Zero, m_commerceLogger);
		m_httpParams.token = generateSSOToken.Token;
		m_httpParams.client_id = m_clientID;
		m_checkoutParams = new blz_commerce_checkout_browser_params_t
		{
			title_code = "WTCG",
			title_version = GetTitleVersionString(),
			locale = Localization.GetBnetLocaleName(),
			game_service_region = ((int)BattleNet.GetCurrentRegion()).ToString(),
			device_id = SystemInfo.deviceUniqueIdentifier,
			checkout_url = $"https://nydus-qa.web.blizzard.net/Bnet/{GetLocaleForURL(Localization.GetLocaleName())}/client/checkout",
			game_account_id = ""
		};
		blz_commerce_pair_t blz_commerce_pair_t = new blz_commerce_pair_t();
		blz_commerce_pair_t.key = battlenet_commerce.BLZ_COMMERCE_CHECKOUT_BROWSER_PARAM;
		blz_commerce_pair_t.data = blz_commerce_checkout_browser_params_t.getCPtr(m_checkoutParams).Handle;
		list.Add(blz_commerce_pair_t);
		blz_commerce_pair_t blz_commerce_pair_t2 = new blz_commerce_pair_t();
		blz_commerce_pair_t2.key = battlenet_commerce.BLZ_COMMERCE_HTTP_PARAM;
		blz_commerce_pair_t2.data = blz_commerce_http_params_t.getCPtr(m_httpParams).Handle;
		list.Add(blz_commerce_pair_t2);
		m_browserParams = new blz_commerce_browser_params_t
		{
			is_prod = HearthstoneApplication.IsPublic(),
			browser_directory = GetBrowserPath(),
			window_height = m_checkoutUI.BrowserHeight,
			window_width = m_checkoutUI.BrowserWidth,
			log_directory = Logger.LogsPath
		};
		m_checkoutUI.SetUIParams(m_browserParams);
		OnReadyEvent.AddListener(m_checkoutUI.OnReady);
		m_checkoutUI.AddOutsideClickListener(OnOutsideClick);
		blz_commerce_pair_t blz_commerce_pair_t3 = new blz_commerce_pair_t();
		blz_commerce_pair_t3.key = "android_activity";
		list.Add(blz_commerce_pair_t3);
		blz_commerce_pair_t blz_commerce_pair_t4 = new blz_commerce_pair_t();
		blz_commerce_pair_t4.key = "parentContentView";
		list.Add(blz_commerce_pair_t4);
		blz_commerce_pair_t blz_commerce_pair_t5 = new blz_commerce_pair_t();
		blz_commerce_pair_t5.key = battlenet_commerce.BLZ_COMMERCE_BROWSER_PARAM;
		blz_commerce_pair_t5.data = blz_commerce_browser_params_t.getCPtr(m_browserParams).Handle;
		list.Add(blz_commerce_pair_t5);
		m_commercePairsArray = battlenet_commerce.new_blzCommercePairArray(list.Count);
		for (int i = 0; i < list.Count; i++)
		{
			battlenet_commerce.blzCommercePairArray_setitem(m_commercePairsArray, i, list[i]);
		}
		battlenet_commerce.blz_commerce_register(m_commerceSdk, battlenet_commerce.blz_commerce_register_http());
		battlenet_commerce.blz_commerce_register(m_commerceSdk, battlenet_commerce.blz_commerce_register_scene());
		battlenet_commerce.blz_commerce_register(m_commerceSdk, battlenet_commerce.blz_commerce_register_checkout());
		battlenet_commerce.blz_commerce_register(m_commerceSdk, battlenet_commerce.blz_commerce_register_catalog());
		battlenet_commerce.blz_commerce_register(m_commerceSdk, battlenet_commerce.blz_commerce_register_vc());
		if (battlenet_commerce.blz_commerce_init(m_commerceSdk, m_commercePairsArray, list.Count).state == blz_commerce_result_state_t.BLZ_COMMERCE_RESULT_OK)
		{
			OnReady();
			Log.Store.PrintInfo("Making Browser");
			battlenet_commerce.delete_blzCommercePairArray(m_commercePairsArray);
			LoadProducts();
			yield break;
		}
		throw new Exception("The commerce SDK failed to initialize internally!");
	}

	private IEnumerator<IAsyncJobResult> Job_CreateCheckoutClient()
	{
		Log.Store.PrintDebug("[HearthstoneCheckout.CreateCheckoutClient] Initialize");
		m_currentState = State.Startup;
		GenerateSSOToken generateSSOToken = new GenerateSSOToken();
		yield return generateSSOToken;
		if (!generateSSOToken.HasToken)
		{
			m_clientInitializationResponse = ClientInitializationResponse.Fail;
			yield return new JobFailedResult("[HearthstoneCheckout.CreateCheckoutClient] Didn't receive a SSO token from request.");
		}
		yield return new WaitForLogin();
		m_checkoutParams = CreateCheckoutParams(generateSSOToken.Token);
		if (m_clientInitializationResponse == ClientInitializationResponse.Fail)
		{
			m_clientInitializationResponse = ClientInitializationResponse.Waiting;
		}
		Log.Store.PrintDebug("[HearthstoneCheckout.CreateCheckoutClient] Scene Checkout was successfully created.");
	}

	private IEnumerator<IAsyncJobResult> Job_InitializeCheckoutClient()
	{
		m_retriesRemaining = 3;
		bool success = false;
		while (!success && m_retriesRemaining-- > 0)
		{
			Log.Store.PrintDebug("[HearthstoneCheckout.InitializeCheckoutClient] Create client");
			m_currentState = State.Startup;
			yield return new JobDefinition("HearthstoneCheckout.CreateCheckoutClient", Job_CreateCheckoutClient(), new WaitForLogin());
			Log.Store.PrintDebug("[HearthstoneCheckout.InitializeCheckoutClient] Client response: {0}", m_clientInitializationResponse);
			if (m_clientInitializationResponse == ClientInitializationResponse.Waiting)
			{
				yield return new WaitForClientInitializationResponse(this, 60f);
			}
			switch (m_clientInitializationResponse)
			{
			case ClientInitializationResponse.Waiting:
				Log.Store.PrintError("[HearthstoneCheckout.InitializeCheckoutClient] Client timed out");
				TelemetryManager.Client().SendBlizzardCheckoutInitializationResult(success: false, "Checkout Client Initialization Timeout", $"Attempt {3 - m_retriesRemaining} of {3}");
				break;
			case ClientInitializationResponse.Success:
				Log.Store.PrintDebug("[HearthstoneCheckout.InitializeCheckoutClient] Client initialized");
				TelemetryManager.Client().SendBlizzardCheckoutInitializationResult(success: true, "", "");
				success = true;
				break;
			case ClientInitializationResponse.Fail:
				Log.Store.PrintError("[HearthstoneCheckout.InitializeCheckoutClient] Client failed");
				TelemetryManager.Client().SendBlizzardCheckoutInitializationResult(success: false, "Checkout Client Initialization Unsuccessful", "");
				break;
			default:
				Log.Store.PrintError("[HearthstoneCheckout.InitializeCheckoutClient] Unrecognized initialization response: {0}", m_clientInitializationResponse);
				break;
			}
		}
		if (success)
		{
			while (!m_receivedSdkProducts)
			{
				yield return null;
			}
			m_currentState = State.Idle;
			OnInitializedEvent.Fire();
		}
		else
		{
			m_currentState = State.Unavailable;
			yield return new JobFailedResult("[HearthstoneCheckout.InitializeCheckoutClient] Failed to initialize checkout client.");
		}
	}

	public void OnReady()
	{
		if (m_currentState == State.Initializing)
		{
			Log.Store.PrintDebug("[HearthstoneCheckout.OnReady]");
			m_currentState = State.Ready;
			m_clientInitializationResponse = ClientInitializationResponse.Success;
			OnReadyEvent.Fire();
		}
		else
		{
			Log.Store.PrintWarning("[HearthstoneCheckout.OnReady] Received ready event while in unexpected state ({0}).", m_currentState.ToString());
		}
	}

	private void OnSceneViewReady()
	{
		CheckoutIsReady = true;
		m_checkoutUI.Show();
	}

	public void OnDisconnect()
	{
		Log.Store.PrintDebug("[HearthstoneCheckout.OnDisconnect]");
		SignalCloseNextFrame();
		OnDisconnectEvent.Fire();
	}

	public void OnCancel()
	{
		Log.Store.PrintDebug("[HearthstoneCheckout.OnCancel]");
		SignalCloseNextFrame();
		OnCancelEvent.Fire();
	}

	public void OnWindowResized(blz_commerce_scene_window_resize_event_t windowResizeEvent)
	{
		Log.Store.PrintDebug("[HearthstoneCheckout.OnWindowResized] (x:{0}, y:{1})", windowResizeEvent.win_size.x, windowResizeEvent.win_size.y);
		if (m_checkoutUI != null)
		{
			m_checkoutUI.ResizeTexture(windowResizeEvent.win_size.x, windowResizeEvent.win_size.y);
		}
	}

	public void OnBufferUpdate(blz_commerce_scene_buffer_update_event_t bufferUpdateEvent)
	{
		Log.Store.PrintDebug("[HearthstoneCheckout.OnBufferUpdate]");
		if (m_checkoutUI != null)
		{
			m_checkoutUI.UpdateTexture(bufferUpdateEvent.buffer.bytes);
		}
	}

	public void OnWindowResizeRequested(blz_commerce_scene_window_resize_requested_event_t windowResizeRequestedEvent)
	{
		Log.Store.PrintDebug("[HearthstoneCheckout.OnWindowResizeRequested] Requested Size (x: {0}, y:{1})", windowResizeRequestedEvent.requested_size.x, windowResizeRequestedEvent.requested_size.y);
	}

	public void OnWindowCloseRequested()
	{
		Log.Store.PrintDebug("[HearthstoneCheckout.OnWindowCloseRequested]");
		SignalCloseNextFrame();
	}

	public void OnCursorChangeRequest()
	{
	}

	public void OnExternalLink(blz_commerce_scene_external_link_event_t externalLinkEvent)
	{
		Log.Store.PrintDebug("[HearthstoneCheckout.OnExternalLink] URL: {0}", externalLinkEvent.url);
		Application.OpenURL(externalLinkEvent.url);
	}

	public void OnPurchaseCanceledBeforeSubmit(blz_commerce_purchase_event_t response)
	{
		Log.Store.PrintInfo("[HearthstoneCheckout.OnPurchaseCanceledBeforeSubmit]");
		OnCancel();
		OnPurchaseCanceledBeforeSubmitEvent.Fire();
	}

	public void OnPurchaseFailureBeforeSubmit(blz_commerce_purchase_event_t response)
	{
		LogPurchaseResponse("[HearthstoneCheckout.OnPurchaseFailureBeforeSubmit]", response);
		OnPurchaseFailureBeforeSubmitEvent.Fire();
	}

	public void OnOrderFailure(blz_commerce_purchase_event_t response)
	{
		if (!IsTransactionInProgress())
		{
			LogPurchaseResponse("[HearthstoneCheckout.OnOrderFailure: Canceled Before Response]", response);
			return;
		}
		LogPurchaseResponse("[HearthstoneCheckout.OnOrderFailure]", response);
		m_currentState = State.Finished;
		UpdateTransactionData(response);
		OnOrderFailureEvent.Fire(m_currentTransaction);
		if (m_checkoutUI != null && m_checkoutUI.IsShown())
		{
			m_checkoutUI.Hide();
		}
		OnTransactionProcessCompleted();
	}

	public void OnPurchaseSubmitted(blz_commerce_purchase_event_t response)
	{
		LogPurchaseResponse("[HearthstoneCheckout.OnPurchaseSubmitted]", response);
		OnPurchaseSubmittedEvent.Fire();
		if (!response.browser_info.is_cancelable)
		{
			m_currentState = State.InProgress;
		}
	}

	public void OnOrderComplete(blz_commerce_purchase_event_t response)
	{
		if (!IsTransactionInProgress())
		{
			LogPurchaseResponse("[HearthstoneCheckout.OnOrderComplete: Canceled Before Response]", response);
			return;
		}
		LogPurchaseResponse("[HearthstoneCheckout.OnOrderComplete]", response);
		m_currentState = State.Finished;
		UpdateTransactionData(response);
		OnOrderCompleteEvent.Fire(m_currentTransaction);
		if (m_checkoutUI != null && m_checkoutUI.IsShown())
		{
			m_checkoutUI.Hide();
		}
		OnTransactionProcessCompleted();
	}

	public void OnOrderPending(blz_commerce_purchase_event_t response)
	{
		if (!IsTransactionInProgress())
		{
			LogPurchaseResponse("[HearthstoneCheckout.OnOrderPending: Canceled Before Response]", response);
			return;
		}
		LogPurchaseResponse("[HearthstoneCheckout.OnOrderPending]", response);
		if (!response.browser_info.is_cancelable)
		{
			m_currentState = State.InProgress;
		}
		m_transactionStart = DateTime.Now;
		UpdateTransactionData(response);
		OnOrderPendingEvent.Fire(m_currentTransaction);
	}

	public void OnVirtualCurrencyResponse(blz_commerce_vc_order_event_t virtualCurrencyResponse)
	{
		if (!IsTransactionInProgress())
		{
			Log.Store.PrintDebug("[HearthstoneCheckout.OnVirtualCurrencyResponse: Canceled Before Response] Status - {0}", virtualCurrencyResponse.http_result);
			return;
		}
		Log.Store.PrintDebug("[HearthstoneCheckout.OnVirtualCurrencyResponse] Status - {0}", virtualCurrencyResponse.response);
		m_currentState = State.Finished;
		OnOrderCompleteEvent.Fire(m_currentTransaction);
		OnTransactionProcessCompleted();
	}

	public void OnGetBalanceResponse(blz_commerce_vc_get_balance_event_t getBalanceEvent)
	{
		if (!getBalanceEvent.http_result.ok)
		{
			Log.Store.PrintError("There was an error with the virtual currency 'GetBalance' call! (Http Result Status: {0}", getBalanceEvent.http_result);
			return;
		}
		GetBalanceResponse getBalanceResponse = JsonUtility.FromJson<GetBalanceResponse>(getBalanceEvent.response);
		if (!float.TryParse(getBalanceResponse.balance.balance, out var result))
		{
			Log.Store.PrintInfo("[HearthstoneCheckout.OnGetBalanceResponse] Failed to parse balance: {0}", getBalanceResponse.balance.balance);
			result = 0f;
		}
		string currencyCode = getBalanceResponse.balance.ledgerId.currencyCode;
		Log.Store.PrintInfo("[HearthstoneCheckout.OnGetBalanceResponse] Received balance response.  Currency - {0}   Balance - {1}", currencyCode, result);
		int num = 0;
		while (num < m_virtualCurrencyRequests.Count)
		{
			if (m_virtualCurrencyRequests[num].CurrencyCode == currencyCode)
			{
				m_virtualCurrencyRequests[num].Callback?.Invoke(currencyCode, result);
				m_virtualCurrencyRequests.RemoveAt(num);
			}
			else
			{
				num++;
			}
		}
	}

	public void OnGetPersonalizedShopEvent(blz_commerce_catalog_personalized_shop_event_t personalizedShopEvent)
	{
		Log.Store.PrintInfo("[HearthstoneCheckout.OnGetPersonalizedShopEvent] Received shop personalization data.");
		GetPageResponse response = JsonUtility.FromJson<GetPageResponse>(personalizedShopEvent.response);
		if (m_personalizedShopResponseCallbacks.Count == 0)
		{
			Log.Store.PrintWarning("[HearthstoneCheckout.OnGetPersonalizedShopEvent] Received PersonalizedShop data without any queued callbacks.  This can occur if a response is received after a client reset.");
		}
		else
		{
			m_personalizedShopResponseCallbacks.Dequeue()?.Invoke(response);
		}
	}
}
