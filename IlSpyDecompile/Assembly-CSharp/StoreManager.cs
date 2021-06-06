using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using bgs;
using Blizzard.T5.Jobs;
using Blizzard.Telemetry.WTCG.Client;
using com.blizzard.commerce.Model;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.UI;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class StoreManager
{
	private enum PurchaseErrorSource
	{
		FROM_PURCHASE_METHOD_RESPONSE,
		FROM_STATUS_OR_PURCHASE_RESPONSE,
		FROM_PREVIOUS_PURCHASE
	}

	private enum TransactionStatus
	{
		UNKNOWN,
		IN_PROGRESS_MONEY,
		IN_PROGRESS_GOLD_GTAPP,
		IN_PROGRESS_GOLD_NO_GTAPP,
		READY,
		WAIT_ZERO_COST_LICENSE,
		WAIT_METHOD_OF_PAYMENT,
		WAIT_CONFIRM,
		WAIT_RISK,
		CHALLENGE_SUBMITTED,
		CHALLENGE_CANCELED,
		USER_CANCELING,
		AUTO_CANCELING,
		IN_PROGRESS_BLIZZARD_CHECKOUT,
		WAIT_BLIZZARD_CHECKOUT
	}

	private enum LicenseStatus
	{
		NOT_OWNED,
		OWNED,
		OWNED_AND_BLOCKING,
		UNDEFINED
	}

	private struct ShowStoreData
	{
		public bool isTotallyFake;

		public Store.ExitCallback exitCallback;

		public object exitCallbackUserData;

		public ProductType storeProduct;

		public int storeProductData;

		public GeneralStoreMode storeMode;

		public int numItemsRequired;

		public bool useOverlayUI;

		public int pmtProductId;

		public bool closeOnTransactionComplete;

		public IDataModel dataModel;
	}

	public static readonly int DEFAULT_SECONDS_BEFORE_AUTO_CANCEL = 600;

	public const int NO_ITEM_COUNT_REQUIREMENT = 0;

	public const int NO_PRODUCT_DATA_REQUIREMENT = 0;

	public const string DEFAULT_COMMERCE_CLIENT_ID = "df5787f96b2b46c49c66dd45bcb05490";

	private static readonly PlatformDependentValue<GeneralStoreMode> s_defaultStoreMode = new PlatformDependentValue<GeneralStoreMode>(PlatformCategory.Screen)
	{
		PC = GeneralStoreMode.CARDS,
		Phone = GeneralStoreMode.NONE
	};

	private static readonly int UNKNOWN_TRANSACTION_ID = -1;

	private static readonly double CURRENCY_TRANSACTION_TIMEOUT_SECONDS = 30.0;

	private static readonly Map<AdventureDbId, ProductType> s_adventureToProductMap = new Map<AdventureDbId, ProductType>
	{
		{
			AdventureDbId.NAXXRAMAS,
			ProductType.PRODUCT_TYPE_NAXX
		},
		{
			AdventureDbId.BRM,
			ProductType.PRODUCT_TYPE_BRM
		},
		{
			AdventureDbId.LOE,
			ProductType.PRODUCT_TYPE_LOE
		}
	};

	private static StoreManager s_instance = null;

	private readonly ShopView m_view = new ShopView();

	private bool m_featuresReady;

	private bool m_initComplete;

	private bool m_battlePayAvailable;

	private bool m_firstNoticesProcessed;

	private bool m_firstMoneyOrGTAPPTransactionSet;

	private float m_secsBeforeAutoCancel = DEFAULT_SECONDS_BEFORE_AUTO_CANCEL;

	private float m_lastCancelRequestTime;

	private bool m_configLoaded;

	private readonly Map<long, Network.Bundle> m_bundles = new Map<long, Network.Bundle>();

	private readonly Map<int, Network.GoldCostBooster> m_goldCostBooster = new Map<int, Network.GoldCostBooster>();

	private readonly List<Network.ShopSection> m_sections = new List<Network.ShopSection>();

	private readonly Dictionary<int, Network.ShopSale> m_sales = new Dictionary<int, Network.ShopSale>();

	private long? m_goldCostArena;

	private Currency m_currency = new Currency();

	private readonly HashSet<long> m_transactionIDsConclusivelyHandled = new HashSet<long>();

	private readonly Map<ShopType, IStore> m_stores = new Map<ShopType, IStore>();

	private ShopType m_currentShopType;

	private bool m_ignoreProductTiming;

	private readonly Map<CurrencyType, CurrencyCache> m_currencyCaches = new Map<CurrencyType, CurrencyCache>();

	private float m_showStoreStart;

	private Network.PurchaseMethod m_challengePurchaseMethod;

	private constants.BnetRegion m_regionId;

	private StorePackId m_currentlySelectedId;

	private bool m_canCloseConfirmation = true;

	private string m_shopPageId;

	private bool m_openWhenLastEventFired;

	private TransactionStatus m_status;

	private bool m_waitingToShowStore;

	private ShowStoreData m_showStoreData;

	private MoneyOrGTAPPTransaction m_activeMoneyOrGTAPPTransaction;

	private BuyProductEventArgs m_pendingProductPurchaseArgs;

	private readonly HashSet<long> m_confirmedTransactionIDs = new HashSet<long>();

	private readonly List<NetCache.ProfileNoticePurchase> m_outstandingPurchaseNotices = new List<NetCache.ProfileNoticePurchase>();

	private List<Achievement> m_completedAchieves = new List<Achievement>();

	private bool m_licenseAchievesListenerRegistered;

	private TransactionStatus m_previousStatusBeforeAutoCancel;

	private static readonly PlatformDependentValue<bool> HAS_THIRD_PARTY_APP_STORE = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		PC = false,
		Mac = false,
		iOS = true,
		Android = true
	};

	public bool IgnoreProductTiming => m_ignoreProductTiming;

	public IEnumerable<Network.Bundle> AllBundles => m_bundles.Values;

	public IEnumerable<Network.GoldCostBooster> AllGoldCostBoosters => m_goldCostBooster.Values;

	public IEnumerable<Network.ShopSection> AllSections => m_sections;

	public StorePackId CurrentlySelectedId => m_currentlySelectedId;

	public ProductCatalog Catalog { get; }

	private static BattlePayProvider StoreProvider => BattlePayProvider.BP_PROVIDER_BLIZZARD;

	public bool IsPromptShowing
	{
		get
		{
			if (!m_view.IsPromptShowing)
			{
				return IsHearthstoneCheckoutUIShowing();
			}
			return true;
		}
	}

	private TransactionStatus Status
	{
		get
		{
			return m_status;
		}
		set
		{
			if (0f == m_lastCancelRequestTime && m_status == TransactionStatus.UNKNOWN)
			{
				m_lastCancelRequestTime = Time.realtimeSinceStartup;
			}
			m_status = value;
			FireStatusChangedEventIfNeeded();
		}
	}

	private bool FirstNoticesProcessed
	{
		get
		{
			return m_firstNoticesProcessed;
		}
		set
		{
			m_firstNoticesProcessed = value;
			FireStatusChangedEventIfNeeded();
		}
	}

	private bool BattlePayAvailable
	{
		get
		{
			return m_battlePayAvailable;
		}
		set
		{
			m_battlePayAvailable = value;
			FireStatusChangedEventIfNeeded();
		}
	}

	private bool FeaturesReady
	{
		get
		{
			return m_featuresReady;
		}
		set
		{
			m_featuresReady = value;
			FireStatusChangedEventIfNeeded();
		}
	}

	private bool ConfigLoaded
	{
		get
		{
			return m_configLoaded;
		}
		set
		{
			m_configLoaded = value;
			FireStatusChangedEventIfNeeded();
		}
	}

	public static bool HasExternalStore
	{
		get
		{
			if (!HAS_THIRD_PARTY_APP_STORE)
			{
				return false;
			}
			return true;
		}
	}

	private event Action<bool> OnStatusChanged = delegate
	{
	};

	private event Action<Network.Bundle, PaymentMethod> OnSuccessfulPurchaseAck = delegate
	{
	};

	private event Action<Network.Bundle, PaymentMethod> OnSuccessfulPurchase = delegate
	{
	};

	private event Action<Network.Bundle, PaymentMethod> OnFailedPurchaseAck = delegate
	{
	};

	private event Action OnAuthorizationExit = delegate
	{
	};

	private event Action OnStoreShown = delegate
	{
	};

	private event Action OnStoreHidden = delegate
	{
	};

	private StoreManager()
	{
		Catalog = new ProductCatalog(this);
	}

	public static StoreManager Get()
	{
		return s_instance ?? (s_instance = new StoreManager());
	}

	public static bool IsInitialized()
	{
		return s_instance != null;
	}

	private static void DestroyInstance()
	{
		s_instance.GetStore(ShopType.GENERAL_STORE)?.Unload();
		if (AchieveManager.Get() != null && s_instance != null)
		{
			AchieveManager.Get().RemoveAchievesUpdatedListener(s_instance.OnAchievesUpdated);
			AchieveManager.Get().RemoveLicenseAddedAchievesUpdatedListener(s_instance.OnLicenseAddedAchievesUpdated);
		}
		s_instance = null;
	}

	private void NetworkRegistration()
	{
		Network network = Network.Get();
		network.RegisterNetHandler(BattlePayStatusResponse.PacketID.ID, OnBattlePayStatusResponse);
		network.RegisterNetHandler(BattlePayConfigResponse.PacketID.ID, OnBattlePayConfigResponse);
		network.RegisterNetHandler(PurchaseMethod.PacketID.ID, OnPurchaseMethod);
		network.RegisterNetHandler(PurchaseResponse.PacketID.ID, OnPurchaseResponse);
		network.RegisterNetHandler(CancelPurchaseResponse.PacketID.ID, OnPurchaseCanceledResponse);
		network.RegisterNetHandler(PurchaseWithGoldResponse.PacketID.ID, OnPurchaseViaGoldResponse);
		network.RegisterNetHandler(ThirdPartyPurchaseStatusResponse.PacketID.ID, OnThirdPartyPurchaseStatusResponse);
	}

	private void HearthstoneCheckoutRegistration()
	{
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out var service))
		{
			service.OnInitializedEvent.AddListener(OnHearthstoneCheckoutInitialized);
			service.OnReadyEvent.AddListener(OnHearthstoneCheckoutReady);
			service.OnCancelEvent.AddListener(OnHearthstoneCheckoutCancel);
			service.OnCloseEvent.AddListener(OnHearthstoneCheckoutClose);
			service.OnOrderPendingEvent.AddListener(OnHearthstoneCheckoutOrderPending);
			service.OnOrderFailureEvent.AddListener(OnHearthstoneCheckoutOrderFailure);
			service.OnOrderCompleteEvent.AddListener(OnHearthstoneCheckoutOrderComplete);
			service.OnPurchaseFailureBeforeSubmitEvent.AddListener(OnHearthstoneCheckoutSubmitFailure);
		}
	}

	public void Init()
	{
		NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheFeatures), OnNetCacheFeaturesReady);
		if (!m_initComplete)
		{
			SceneMgr.Get().RegisterSceneUnloadedEvent(OnSceneUnloaded);
			NetworkRegistration();
			HearthstoneCheckoutRegistration();
			NetCache.NetCacheProfileNotices netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>();
			if (netObject != null)
			{
				OnNewNotices(netObject.Notices, isInitialNoticeList: false);
			}
			NetCache.Get().RegisterNewNoticesListener(OnNewNotices);
			LoginManager.Get().OnFullLoginFlowComplete += OnLoginCompleted;
			m_regionId = BattleNet.GetCurrentRegion();
			RegisterViewListeners();
			m_initComplete = true;
			AssetLoader.Get().InstantiatePrefab((string)ShopPrefabs.ShopPrefab, OnGeneralStoreLoaded);
			HearthstoneApplication.Get().WillReset += WillReset;
		}
	}

	private void WillReset()
	{
		HearthstoneApplication.Get().WillReset -= WillReset;
		UnregisterViewListeners();
		Network network = Network.Get();
		network.RemoveNetHandler(BattlePayStatusResponse.PacketID.ID, OnBattlePayStatusResponse);
		network.RemoveNetHandler(BattlePayConfigResponse.PacketID.ID, OnBattlePayConfigResponse);
		network.RemoveNetHandler(PurchaseMethod.PacketID.ID, OnPurchaseMethod);
		network.RemoveNetHandler(PurchaseResponse.PacketID.ID, OnPurchaseResponse);
		network.RemoveNetHandler(CancelPurchaseResponse.PacketID.ID, OnPurchaseCanceledResponse);
		network.RemoveNetHandler(PurchaseWithGoldResponse.PacketID.ID, OnPurchaseViaGoldResponse);
		network.RemoveNetHandler(ThirdPartyPurchaseStatusResponse.PacketID.ID, OnThirdPartyPurchaseStatusResponse);
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out var service))
		{
			service.OnInitializedEvent.RemoveListener(OnHearthstoneCheckoutInitialized);
			service.OnReadyEvent.RemoveListener(OnHearthstoneCheckoutReady);
			service.OnCancelEvent.RemoveListener(OnHearthstoneCheckoutCancel);
			service.OnCloseEvent.RemoveListener(OnHearthstoneCheckoutClose);
			service.OnOrderPendingEvent.RemoveListener(OnHearthstoneCheckoutOrderPending);
			service.OnOrderFailureEvent.RemoveListener(OnHearthstoneCheckoutOrderFailure);
			service.OnOrderCompleteEvent.RemoveListener(OnHearthstoneCheckoutOrderComplete);
			service.OnPurchaseFailureBeforeSubmitEvent.RemoveListener(OnHearthstoneCheckoutSubmitFailure);
		}
		NetCache.Get().RemoveUpdatedListener(typeof(NetCache.NetCacheFeatures), OnNetCacheFeaturesReady);
		DestroyInstance();
	}

	public void Heartbeat()
	{
		if (m_initComplete)
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			AutoCancelPurchaseIfNeeded(realtimeSinceStartup);
		}
	}

	public ShopAvailabilityError GetStoreAvailabilityError()
	{
		if (!FirstNoticesProcessed)
		{
			return ShopAvailabilityError.FIRST_NOTICES_NOT_PROCESSED;
		}
		if (!IsStoreFeatureEnabled())
		{
			return ShopAvailabilityError.STORE_FEATURE_DISABLED;
		}
		if (!BattlePayAvailable)
		{
			return ShopAvailabilityError.BATTLEPAY_UNAVAILABLE;
		}
		if (!ConfigLoaded)
		{
			return ShopAvailabilityError.BATTLEPAY_CONFIG_NOT_LOADED;
		}
		if (!HaveProductsToSell())
		{
			return ShopAvailabilityError.NO_PRODUCTS_FOR_SALE;
		}
		if (!Network.IsLoggedIn())
		{
			return ShopAvailabilityError.NOT_LOGGED_IN;
		}
		if (HearthstoneCheckout.IsClientCreationInProgress())
		{
			return ShopAvailabilityError.CHECKOUT_INITIALIZING;
		}
		if (!IsCheckoutFallbackSupported() && !Vars.Key("Mobile.SkipStoreValidation").GetBool(def: false))
		{
			if (!HearthstoneCheckout.IsAvailable())
			{
				return ShopAvailabilityError.CHECKOUT_UNAVAILABLE;
			}
			if (!IsSimpleCheckoutFeatureEnabled())
			{
				return ShopAvailabilityError.CHECKOUT_NOT_ENABLED;
			}
		}
		if (Status == TransactionStatus.UNKNOWN)
		{
			return ShopAvailabilityError.TRANSACTION_STATUS_UNKNOWN;
		}
		return ShopAvailabilityError.NO_ERROR;
	}

	public bool IsOpen(bool printStatus = true)
	{
		ShopAvailabilityError storeAvailabilityError = GetStoreAvailabilityError();
		if (storeAvailabilityError == ShopAvailabilityError.NO_ERROR)
		{
			if (printStatus)
			{
				Log.Store.Print("Store is OPEN.");
			}
			return true;
		}
		if (printStatus)
		{
			Log.Store.Print("Store is CLOSED due to: {0}", storeAvailabilityError.ToString());
		}
		return false;
	}

	private bool IsStoreFeatureEnabled()
	{
		return GetNetCacheFeatures()?.Store.Store ?? false;
	}

	public bool IsBattlePayFeatureEnabled()
	{
		NetCache.NetCacheFeatures netCacheFeatures = GetNetCacheFeatures();
		if (netCacheFeatures == null)
		{
			return false;
		}
		if (netCacheFeatures.Store.Store)
		{
			return netCacheFeatures.Store.BattlePay;
		}
		return false;
	}

	public bool IsBuyWithGoldFeatureEnabled()
	{
		NetCache.NetCacheFeatures netCacheFeatures = GetNetCacheFeatures();
		if (netCacheFeatures == null)
		{
			return false;
		}
		if (netCacheFeatures.Store.Store)
		{
			return netCacheFeatures.Store.BuyWithGold;
		}
		return false;
	}

	private void SetCanTapOutConfirmationUI(bool closeConfirmationUI)
	{
		m_canCloseConfirmation = closeConfirmationUI;
	}

	public bool CanTapOutConfirmationUI()
	{
		return m_canCloseConfirmation;
	}

	public bool IsSimpleCheckoutFeatureEnabled()
	{
		NetCache.NetCacheFeatures netCacheFeatures = GetNetCacheFeatures();
		if (netCacheFeatures == null)
		{
			return false;
		}
		bool flag = false;
		switch (PlatformSettings.RuntimeOS)
		{
		case OSCategory.PC:
			flag = netCacheFeatures.Store.SimpleCheckoutWin;
			break;
		case OSCategory.Mac:
			flag = netCacheFeatures.Store.SimpleCheckoutMac;
			break;
		case OSCategory.iOS:
			flag = netCacheFeatures.Store.SimpleCheckoutIOS;
			break;
		case OSCategory.Android:
			switch (AndroidDeviceSettings.Get().GetAndroidStore())
			{
			case AndroidStore.AMAZON:
				flag = netCacheFeatures.Store.SimpleCheckoutAndroidAmazon;
				break;
			case AndroidStore.GOOGLE:
				flag = netCacheFeatures.Store.SimpleCheckoutAndroidGoogle;
				break;
			case AndroidStore.BLIZZARD:
			case AndroidStore.HUAWEI:
			case AndroidStore.ONE_STORE:
				flag = netCacheFeatures.Store.SimpleCheckoutAndroidGlobal;
				break;
			default:
				Log.Store.PrintError("The given store was not accounted for: {0}\nPlease check in '{1}.{2}' class and method for implementation.", AndroidDeviceSettings.Get().GetAndroidStore().ToString(), "StoreManager", "IsSimpleCheckoutFeatureEnabled");
				break;
			}
			break;
		}
		if (flag && netCacheFeatures.Store.Store && netCacheFeatures.Store.SimpleCheckout)
		{
			return HearthstoneCheckout.IsAvailable();
		}
		return false;
	}

	private bool IsSoftAccountPurchasingEnabled()
	{
		NetCache.NetCacheFeatures netCacheFeatures = GetNetCacheFeatures();
		if (netCacheFeatures == null)
		{
			return false;
		}
		if (netCacheFeatures.Store.Store)
		{
			return netCacheFeatures.Store.SoftAccountPurchasing;
		}
		return false;
	}

	public bool IsVintageStoreEnabled()
	{
		return GetNetCacheFeatures()?.Store.VintageStore ?? true;
	}

	public bool IsBuyCardBacksFromCollectionManagerEnabled()
	{
		return GetNetCacheFeatures()?.Store.BuyCardBacksFromCollectionManager ?? true;
	}

	public bool IsBuyHeroSkinsFromCollectionManagerEnabled()
	{
		return GetNetCacheFeatures()?.Store.BuyHeroSkinsFromCollectionManager ?? true;
	}

	public BattlePayProvider? ActiveTransactionProvider()
	{
		return m_activeMoneyOrGTAPPTransaction?.Provider;
	}

	public void RegisterStatusChangedListener(Action<bool> callback)
	{
		OnStatusChanged -= callback;
		OnStatusChanged += callback;
	}

	public void RemoveStatusChangedListener(Action<bool> callback)
	{
		OnStatusChanged -= callback;
	}

	public void RegisterSuccessfulPurchaseListener(Action<Network.Bundle, PaymentMethod> callback)
	{
		OnSuccessfulPurchase -= callback;
		OnSuccessfulPurchase += callback;
	}

	public void RemoveSuccessfulPurchaseListener(Action<Network.Bundle, PaymentMethod> callback)
	{
		OnSuccessfulPurchase -= callback;
	}

	public void RegisterSuccessfulPurchaseAckListener(Action<Network.Bundle, PaymentMethod> callback)
	{
		OnSuccessfulPurchaseAck -= callback;
		OnSuccessfulPurchaseAck += callback;
	}

	public void RemoveSuccessfulPurchaseAckListener(Action<Network.Bundle, PaymentMethod> callback)
	{
		OnSuccessfulPurchaseAck -= callback;
	}

	public void RegisterFailedPurchaseAckListener(Action<Network.Bundle, PaymentMethod> callback)
	{
		OnFailedPurchaseAck -= callback;
		OnFailedPurchaseAck += callback;
	}

	public void RemoveFailedPurchaseAckListener(Action<Network.Bundle, PaymentMethod> callback)
	{
		OnFailedPurchaseAck -= callback;
	}

	public void RegisterAuthorizationExitListener(Action callback)
	{
		OnAuthorizationExit -= callback;
		OnAuthorizationExit += callback;
	}

	public void RemoveAuthorizationExitListener(Action callback)
	{
		OnAuthorizationExit -= callback;
	}

	public void RegisterStoreShownListener(Action callback)
	{
		OnStoreShown -= callback;
		OnStoreShown += callback;
	}

	public void RemoveStoreShownListener(Action callback)
	{
		OnStoreShown -= callback;
	}

	public void RegisterStoreHiddenListener(Action callback)
	{
		OnStoreHidden -= callback;
		OnStoreHidden += callback;
	}

	public void RemoveStoreHiddenListener(Action callback)
	{
		OnStoreHidden -= callback;
	}

	private void RegisterViewListeners()
	{
		m_view.OnComponentReady += StoreViewReady;
		m_view.PurchaseAuth.OnPurchaseResultAcknowledged += OnPurchaseResultAcknowledged;
		m_view.PurchaseAuth.OnAuthExit += OnAuthExit;
		m_view.Summary.OnSummaryConfirm += OnSummaryConfirm;
		m_view.Summary.OnSummaryCancel += OnSummaryCancel;
		m_view.Summary.OnSummaryInfo += OnSummaryInfo;
		m_view.Summary.OnSummaryPaymentAndTos += OnSummaryPaymentAndTOS;
		m_view.SendToBam.OnOkay += OnSendToBAMOkay;
		m_view.SendToBam.OnCancel += OnSendToBAMCancel;
		m_view.LegalBam.OnOkay += OnSendToBAMLegal;
		m_view.LegalBam.OnCancel += UnblockStoreInterface;
		m_view.DoneWithBam.OnOkay += UnblockStoreInterface;
		m_view.ChallengePrompt.OnComplete += OnChallengeComplete;
		m_view.ChallengePrompt.OnCancel += OnChallengeCancel;
	}

	private void UnregisterViewListeners()
	{
		m_view.OnComponentReady -= StoreViewReady;
		m_view.PurchaseAuth.OnPurchaseResultAcknowledged -= OnPurchaseResultAcknowledged;
		m_view.PurchaseAuth.OnAuthExit -= OnAuthExit;
		m_view.Summary.OnSummaryConfirm -= OnSummaryConfirm;
		m_view.Summary.OnSummaryCancel -= OnSummaryCancel;
		m_view.Summary.OnSummaryInfo -= OnSummaryInfo;
		m_view.Summary.OnSummaryPaymentAndTos -= OnSummaryPaymentAndTOS;
		m_view.SendToBam.OnOkay -= OnSendToBAMOkay;
		m_view.SendToBam.OnCancel -= OnSendToBAMCancel;
		m_view.LegalBam.OnOkay -= OnSendToBAMLegal;
		m_view.LegalBam.OnCancel -= UnblockStoreInterface;
		m_view.DoneWithBam.OnOkay -= UnblockStoreInterface;
		m_view.ChallengePrompt.OnComplete -= OnChallengeComplete;
		m_view.ChallengePrompt.OnCancel -= OnChallengeCancel;
	}

	private bool IsWaitingToShow()
	{
		return m_waitingToShowStore;
	}

	public IStore GetCurrentStore()
	{
		return GetStore(m_currentShopType);
	}

	private IStore GetStore(ShopType shopType)
	{
		m_stores.TryGetValue(shopType, out var value);
		return value;
	}

	public bool IsShown()
	{
		return GetCurrentStore()?.IsOpen() ?? false;
	}

	public bool IsShownOrWaitingToShow()
	{
		if (IsWaitingToShow())
		{
			return true;
		}
		if (IsShown())
		{
			return true;
		}
		return false;
	}

	public bool GetGoldCostNoGTAPP(NoGTAPPTransactionData noGTAPPTransactionData, out long cost)
	{
		cost = 0L;
		if (noGTAPPTransactionData == null)
		{
			return false;
		}
		long cost2 = 0L;
		switch (noGTAPPTransactionData.Product)
		{
		case ProductType.PRODUCT_TYPE_BOOSTER:
			if (!GetBoosterGoldCostNoGTAPP(noGTAPPTransactionData.ProductData, out cost2))
			{
				return false;
			}
			break;
		case ProductType.PRODUCT_TYPE_DRAFT:
			if (!GetArenaGoldCostNoGTAPP(out cost2))
			{
				return false;
			}
			break;
		case ProductType.PRODUCT_TYPE_HIDDEN_LICENSE:
			return false;
		default:
			Log.Store.PrintWarning($"StoreManager.GetGoldCostNoGTAPP(): don't have a no-GTAPP gold price for product {noGTAPPTransactionData.Product} data {noGTAPPTransactionData.ProductData}");
			return false;
		}
		cost = cost2 * noGTAPPTransactionData.Quantity;
		return true;
	}

	public Network.Bundle GetBundleFromPmtProductId(long pmtProductId)
	{
		if (pmtProductId == 0L)
		{
			return null;
		}
		if (m_bundles.TryGetValue(pmtProductId, out var value))
		{
			return value;
		}
		return null;
	}

	private HashSet<ProductType> GetProductsInItemList(List<Network.BundleItem> items)
	{
		HashSet<ProductType> hashSet = new HashSet<ProductType>();
		foreach (Network.BundleItem item in items)
		{
			hashSet.Add(item.ItemType);
		}
		return hashSet;
	}

	public HashSet<ProductType> GetProductsInBundle(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return new HashSet<ProductType>();
		}
		return GetProductsInItemList(bundle.Items);
	}

	public ProductAvailability GetNetworkBundleProductAvailability(Network.Bundle bundle, bool shouldSeeWild, bool checkRange = true)
	{
		if (bundle == null)
		{
			return ProductAvailability.UNDEFINED;
		}
		bool flag = false;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		foreach (Network.BundleItem item in bundle.Items)
		{
			if (!shouldSeeWild && !flag)
			{
				switch (item.ItemType)
				{
				case ProductType.PRODUCT_TYPE_BOOSTER:
					flag = GameUtils.IsBoosterWild((BoosterDbId)item.ProductData);
					break;
				case ProductType.PRODUCT_TYPE_WING:
					flag = GameUtils.IsAdventureWild(GameUtils.GetAdventureIdByWingId(item.ProductData));
					break;
				case ProductType.PRODUCT_TYPE_NAXX:
				case ProductType.PRODUCT_TYPE_BRM:
				case ProductType.PRODUCT_TYPE_LOE:
					flag = true;
					break;
				}
			}
			switch (GetProductItemOwnershipStatus(item.ItemType, item.ProductData))
			{
			case ItemOwnershipStatus.OWNED:
				num++;
				if (GetProductItemPurchaseRule(item.ItemType, item.ProductData) == ItemPurchaseRule.BLOCKING)
				{
					num3++;
				}
				break;
			case ItemOwnershipStatus.IGNORED:
			case ItemOwnershipStatus.UNOWNED:
				if (item.ItemType != ProductType.PRODUCT_TYPE_HIDDEN_LICENSE)
				{
					num2++;
				}
				break;
			default:
				num4++;
				break;
			}
		}
		if (num4 > 0)
		{
			return ProductAvailability.UNDEFINED;
		}
		if (flag)
		{
			return ProductAvailability.RESTRICTED;
		}
		if ((num > 0 && num2 == 0) || num3 > 0)
		{
			return ProductAvailability.ALREADY_OWNED;
		}
		if (num == 0 && num2 == 0)
		{
			Log.Store.PrintWarning("Product {0} ({1}) contains no buyable or owned items; are the licensed configured?", bundle.PMTProductID, bundle.DisplayName);
			return ProductAvailability.UNDEFINED;
		}
		if (checkRange)
		{
			ProductAvailabilityRange bundleAvailabilityRange = GetBundleAvailabilityRange(bundle);
			if (bundleAvailabilityRange == null)
			{
				Log.Store.PrintWarning("Product is assigned to an unknown sale or event timing: PMT ID = {0}, Product Name = [{1}], event timing = {2}, Sale ID = {3}", bundle.PMTProductID, (bundle.DisplayName != null) ? bundle.DisplayName.GetString() : "", (bundle.ProductEvent != null) ? bundle.ProductEvent : "", string.Join(",", bundle.SaleIds.Select((int id) => id.ToString()).ToArray()));
				return ProductAvailability.SALE_NOT_ACTIVE;
			}
			if (!bundleAvailabilityRange.IsVisibleAtTime(DateTime.UtcNow))
			{
				return ProductAvailability.SALE_NOT_ACTIVE;
			}
		}
		return ProductAvailability.CAN_PURCHASE;
	}

	public bool IsProductAlreadyOwned(Network.Bundle bundle)
	{
		return GetNetworkBundleProductAvailability(bundle, shouldSeeWild: true, checkRange: false) == ProductAvailability.ALREADY_OWNED;
	}

	public bool IsProductPrePurchase(Network.Bundle bundle)
	{
		return bundle?.IsPrePurchase ?? false;
	}

	public bool IsProductFirstPurchaseBundle(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return false;
		}
		if (GetProductsInItemList(bundle.Items).Contains(ProductType.PRODUCT_TYPE_HIDDEN_LICENSE))
		{
			Network.BundleItem bundleItem = bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_HIDDEN_LICENSE && obj.ProductData == 40);
			if (bundleItem != null)
			{
				return true;
			}
		}
		return false;
	}

	public static bool DoesBundleContainProduct(Network.Bundle bundle, ProductType product, int productData = 0, int numItemsRequired = 0)
	{
		if (numItemsRequired != 0 && bundle.Items.Count != numItemsRequired)
		{
			return false;
		}
		foreach (Network.BundleItem item in bundle.Items)
		{
			if (item.ItemType == product && (productData == 0 || item.ProductData == productData))
			{
				return true;
			}
		}
		return false;
	}

	public bool TryGetBonusProductBundleId(ProductType productType, out long pmtId)
	{
		pmtId = 0L;
		List<Network.Bundle> list = GetAllBundlesForProduct(productType, requireRealMoneyOption: false)?.ToList();
		if (list == null || list.Count == 0)
		{
			return false;
		}
		Network.Bundle bundle2 = list.Where((Network.Bundle bonusBundle) => bonusBundle.IsPrePurchase).FirstOrDefault(IsProductAlreadyOwned);
		if (bundle2 != null && bundle2.PMTProductID.HasValue)
		{
			pmtId = bundle2.PMTProductID.Value;
			return true;
		}
		IEnumerable<Network.Bundle> regularBundles = from bonusBundle in list
			where !bonusBundle.IsPrePurchase
			where IsProductAlreadyOwned(bonusBundle) || CanBuyBundle(bonusBundle)
			select bonusBundle;
		Network.Bundle bundle3 = (from product in m_sections.SelectMany((Network.ShopSection section) => section.Products)
			select regularBundles.FirstOrDefault((Network.Bundle bundle) => bundle.PMTProductID == product.PmtId)).FirstOrDefault((Network.Bundle bundle) => bundle != null);
		if (bundle3 == null || !bundle3.PMTProductID.HasValue)
		{
			return false;
		}
		pmtId = bundle3.PMTProductID.Value;
		return true;
	}

	public IEnumerable<Network.Bundle> EnumerateBundlesForProductType(ProductType product, bool requireRealMoneyOption, int productData = 0, int numItemsRequired = 0, bool checkAvailability = true)
	{
		foreach (Network.Bundle value in m_bundles.Values)
		{
			if ((!requireRealMoneyOption || ShopUtils.BundleHasPrice(value, CurrencyType.REAL_MONEY)) && DoesBundleContainProduct(value, product, productData, numItemsRequired) && (!checkAvailability || IsBundleAvailableNow(value)))
			{
				yield return value;
			}
		}
	}

	public List<Network.Bundle> GetAllBundlesForProduct(ProductType product, bool requireRealMoneyOption, int productData = 0, int numItemsRequired = 0, bool checkAvailability = true)
	{
		return EnumerateBundlesForProductType(product, requireRealMoneyOption, productData, numItemsRequired, checkAvailability).ToList();
	}

	public Network.Bundle GetLowestCostBundle(ProductType product, bool requireRealMoneyOption, int productData, int numItemsRequired = 0)
	{
		List<Network.Bundle> allBundlesForProduct = Get().GetAllBundlesForProduct(product, requireRealMoneyOption, productData, numItemsRequired);
		Network.Bundle bundle = null;
		foreach (Network.Bundle item in allBundlesForProduct)
		{
			if (numItemsRequired == 0 || item.Items.Count == numItemsRequired)
			{
				if (bundle == null)
				{
					bundle = item;
				}
				else if (!(bundle.Cost <= item.Cost))
				{
					bundle = item;
				}
			}
		}
		return bundle;
	}

	public List<Network.Bundle> GetAvailableBundlesForProduct(ProductType productType, bool requireNonGoldPriceOption, int productData = 0, int numItemsRequired = 0)
	{
		List<Network.Bundle> list = new List<Network.Bundle>();
		foreach (Network.Bundle value in m_bundles.Values)
		{
			if ((numItemsRequired == 0 || value.Items.Count == numItemsRequired) && (!requireNonGoldPriceOption || ShopUtils.BundleHasNonGoldPrice(value)) && value.Items.Any((Network.BundleItem item) => item.ItemType == productType && (productData == 0 || productData == item.ProductData)) && CanBuyBundle(value))
			{
				list.Add(value);
			}
		}
		return list;
	}

	private List<Network.Bundle> GetAllBundlesContainingItem(ProductType productType, int productData)
	{
		List<Network.Bundle> list = new List<Network.Bundle>();
		foreach (Network.Bundle value in m_bundles.Values)
		{
			bool flag = false;
			foreach (Network.BundleItem item in value.Items)
			{
				if (item.ItemType == productType && item.ProductData == productData)
				{
					flag = true;
				}
			}
			if (flag)
			{
				list.Add(value);
			}
		}
		return list;
	}

	public bool GetAvailableAdventureBundle(AdventureDbId adventureId, bool requireNonGoldOption, out Network.Bundle bundle)
	{
		bundle = null;
		if (GetAdventureProductType(adventureId) == ProductType.PRODUCT_TYPE_UNKNOWN)
		{
			return false;
		}
		List<Network.Bundle> list = null;
		switch (adventureId)
		{
		case AdventureDbId.NAXXRAMAS:
			list = GetAvailableBundlesForProduct(ProductType.PRODUCT_TYPE_NAXX, requireNonGoldOption, 5);
			break;
		case AdventureDbId.BRM:
			list = GetAvailableBundlesForProduct(ProductType.PRODUCT_TYPE_BRM, requireNonGoldOption, 10);
			break;
		case AdventureDbId.LOE:
			list = GetAvailableBundlesForProduct(ProductType.PRODUCT_TYPE_LOE, requireNonGoldOption, 14);
			break;
		default:
		{
			int finalAdventureWing = AdventureUtils.GetFinalAdventureWing((int)adventureId, excludeOwnedWings: false);
			list = GetAvailableBundlesForProduct(ProductType.PRODUCT_TYPE_WING, requireNonGoldOption, finalAdventureWing);
			break;
		}
		}
		if (list != null)
		{
			foreach (Network.Bundle item in list)
			{
				int count = item.Items.Count;
				if (count != 0 && (!requireNonGoldOption || ShopUtils.BundleHasNonGoldPrice(item)) && IsBundleAvailableNow(item))
				{
					if (bundle == null)
					{
						bundle = item;
					}
					else if (bundle.Items.Count <= count)
					{
						bundle = item;
					}
				}
			}
		}
		return bundle != null;
	}

	public bool CanBuyStorePackWithGold(StorePackId storePackId)
	{
		if (storePackId.Type == StorePackType.BOOSTER)
		{
			return CanBuyBoosterWithGold(storePackId.Id);
		}
		return false;
	}

	private bool CanBuyBoosterWithGold(int boosterDbId)
	{
		BoosterDbfRecord record = GameDbf.Booster.GetRecord(boosterDbId);
		if (record == null)
		{
			return false;
		}
		if (string.IsNullOrEmpty(record.BuyWithGoldEvent))
		{
			return false;
		}
		SpecialEventType eventType = SpecialEventManager.GetEventType(record.BuyWithGoldEvent);
		return eventType switch
		{
			SpecialEventType.UNKNOWN => false, 
			SpecialEventType.IGNORE => true, 
			_ => SpecialEventManager.Get().IsEventActive(eventType, activeIfDoesNotExist: false), 
		};
	}

	public bool IsBoosterPreorderActive(int storePackIdData, ProductType productType, out Network.Bundle preOrderBundle)
	{
		foreach (Network.Bundle item in GetAllBundlesForProduct(productType, requireRealMoneyOption: true, storePackIdData))
		{
			if (IsProductPrePurchase(item))
			{
				preOrderBundle = item;
				return true;
			}
		}
		preOrderBundle = null;
		return false;
	}

	public bool IsBoosterHiddenLicenseBundle(StorePackId storePackId, out Network.Bundle hiddenLicenseBundle)
	{
		if (!GameUtils.IsHiddenLicenseBundleBooster(storePackId))
		{
			hiddenLicenseBundle = null;
			return false;
		}
		IEnumerable<Network.Bundle> source = EnumerateBundlesForProductType(ProductType.PRODUCT_TYPE_HIDDEN_LICENSE, requireRealMoneyOption: true, GameUtils.GetProductDataFromStorePackId(storePackId));
		hiddenLicenseBundle = source.FirstOrDefault();
		return hiddenLicenseBundle != null;
	}

	public bool GetHeroBundleByCardDbId(int heroCardDbId, out Network.Bundle heroBundle)
	{
		foreach (Network.Bundle item in GetAllBundlesContainingItem(ProductType.PRODUCT_TYPE_HERO, heroCardDbId))
		{
			bool flag = false;
			foreach (Network.BundleItem item2 in item.Items)
			{
				if (item2.ItemType == ProductType.PRODUCT_TYPE_HIDDEN_LICENSE)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				heroBundle = item;
				return true;
			}
		}
		heroBundle = null;
		return false;
	}

	public bool IsKoreanCustomer()
	{
		return m_currency.SubRegion == 3;
	}

	public bool IsEuropeanCustomer()
	{
		if (m_currency.SubRegion == 2)
		{
			return true;
		}
		if (m_currency.SubRegion == 10)
		{
			return true;
		}
		return false;
	}

	public bool IsNorthAmericanCustomer()
	{
		if (m_currency.SubRegion == 1)
		{
			return true;
		}
		return false;
	}

	public string GetTaxText()
	{
		return m_currency.TaxText switch
		{
			Currency.Tax.TAX_ADDED => GameStrings.Get("GLUE_STORE_SUMMARY_TAX_DISCLAIMER_USD"), 
			Currency.Tax.NO_TAX => string.Empty, 
			_ => GameStrings.Get("GLUE_STORE_SUMMARY_TAX_DISCLAIMER"), 
		};
	}

	public int GetCurrencyChangedVersion()
	{
		return m_currency.ChangedVersion;
	}

	public string GetCurrencyCode()
	{
		return m_currency.Code;
	}

	public CurrencyCache GetCurrencyCache(CurrencyType currencyType)
	{
		if (!m_currencyCaches.TryGetValue(currencyType, out var value))
		{
			value = new CurrencyCache(currencyType);
			m_currencyCaches.Add(currencyType, value);
		}
		return value;
	}

	public string FormatCostBundle(Network.Bundle bundle)
	{
		if (!bundle.Cost.HasValue)
		{
			return string.Empty;
		}
		if (HasExternalStore)
		{
			string productPrice = GetProductPrice(bundle.PMTProductID.ToString());
			if (!string.IsNullOrEmpty(productPrice))
			{
				return productPrice;
			}
		}
		return FormatCost(bundle.CostDisplay);
	}

	public string FormatCost(double? costDisplay)
	{
		string format = m_currency.GetFormat();
		CultureInfo cultureInfo = Localization.GetCultureInfo();
		cultureInfo.NumberFormat.CurrencySymbol = " " + m_currency.Symbol + " ";
		return string.Format(cultureInfo, format, costDisplay).Replace("  ", " ").Trim();
	}

	public string GetProductName(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return string.Empty;
		}
		if (bundle.DisplayName != null && !string.IsNullOrEmpty(bundle.DisplayName.GetString()))
		{
			return bundle.DisplayName.GetString();
		}
		if (bundle.Items.Count == 1)
		{
			Network.BundleItem item = bundle.Items[0];
			return GetSingleItemProductName(item);
		}
		return GetMultiItemProductName(bundle);
	}

	public int GetWingItemCount(List<Network.BundleItem> items)
	{
		int num = 0;
		foreach (Network.BundleItem item in items)
		{
			if (AdventureUtils.IsProductTypeAnAdventureWing(item.ItemType))
			{
				num++;
			}
		}
		return num;
	}

	public string GetProductQuantityText(ProductType product, int productData, int quantity, int baseQuantity)
	{
		string result = string.Empty;
		switch (product)
		{
		case ProductType.PRODUCT_TYPE_BOOSTER:
			if (baseQuantity > 0)
			{
				int num = Math.Max(quantity - baseQuantity, 0);
				result = GameStrings.Format("GLUE_STORE_QUANTITY_PACK_PLUS_BONUS", baseQuantity, num);
			}
			else
			{
				result = GameStrings.Format("GLUE_STORE_QUANTITY_PACK", quantity);
			}
			break;
		case ProductType.PRODUCT_TYPE_DRAFT:
			result = GameStrings.Format("GLUE_STORE_SUMMARY_ITEM_ORDERED", quantity, GameStrings.Get("GLUE_STORE_PRODUCT_NAME_FORGE_TICKET"));
			break;
		case ProductType.PRODUCT_TYPE_CURRENCY:
			result = GameStrings.Format("GLUE_STORE_QUANTITY_DUST", quantity);
			break;
		default:
			Log.Store.PrintWarning($"StoreManager.GetProductQuantityText(): don't know how to format quantity for product {product} (data {productData})");
			break;
		}
		return result;
	}

	public void StartGeneralTransaction()
	{
		StartGeneralTransaction(s_defaultStoreMode);
	}

	public void StartGeneralTransaction(GeneralStoreMode mode)
	{
		if (m_waitingToShowStore)
		{
			Log.Store.Print("StoreManager.StartGeneralTransaction(): already waiting to show store");
			return;
		}
		m_currentShopType = ShopType.GENERAL_STORE;
		m_showStoreData.exitCallback = null;
		m_showStoreData.exitCallbackUserData = null;
		m_showStoreData.isTotallyFake = false;
		m_showStoreData.storeProduct = ProductType.PRODUCT_TYPE_UNKNOWN;
		m_showStoreData.storeProductData = 0;
		m_showStoreData.storeMode = mode;
		m_showStoreData.useOverlayUI = true;
		m_showStoreData.closeOnTransactionComplete = false;
		ShowStoreWhenLoaded();
	}

	public void StartArenaTransaction(Store.ExitCallback exitCallback, object exitCallbackUserData, bool isTotallyFake)
	{
		if (m_waitingToShowStore)
		{
			Log.Store.Print("StoreManager.StartArenaTransaction(): already waiting to show store");
			return;
		}
		m_currentShopType = ShopType.ARENA_STORE;
		m_showStoreData.exitCallback = exitCallback;
		m_showStoreData.exitCallbackUserData = null;
		m_showStoreData.isTotallyFake = isTotallyFake;
		m_showStoreData.storeProduct = ProductType.PRODUCT_TYPE_UNKNOWN;
		m_showStoreData.storeProductData = 0;
		m_showStoreData.useOverlayUI = false;
		m_showStoreData.closeOnTransactionComplete = false;
		ShowStoreWhenLoaded();
	}

	public void StartTavernBrawlTransaction(Store.ExitCallback exitCallback, bool isTotallyFake)
	{
		if (m_waitingToShowStore)
		{
			Log.Store.Print("StoreManager.StartTavernBrawlTransaction(): already waiting to show store");
			return;
		}
		m_currentShopType = ShopType.TAVERN_BRAWL_STORE;
		m_showStoreData.exitCallback = exitCallback;
		m_showStoreData.exitCallbackUserData = null;
		m_showStoreData.isTotallyFake = isTotallyFake;
		m_showStoreData.storeProduct = ProductType.PRODUCT_TYPE_UNKNOWN;
		m_showStoreData.storeProductData = 0;
		m_showStoreData.useOverlayUI = false;
		m_showStoreData.closeOnTransactionComplete = false;
		ShowStoreWhenLoaded();
	}

	public void StartAdventureTransaction(ProductType product, int productData, Store.ExitCallback exitCallback, object exitCallbackUserData, ShopType shopType, int numItemsRequired = 0, bool useOverlayUI = false, IDataModel dataModel = null, int pmtProductId = 0)
	{
		if (m_waitingToShowStore)
		{
			Log.Store.Print("StoreManager.StartAdventureTransaction(): already waiting to show store");
			return;
		}
		if (!CanBuyProductItem(product, productData))
		{
			Log.Store.PrintWarning("StoreManager.StartAdventureTransaction(): cannot buy product item");
			return;
		}
		m_currentShopType = shopType;
		m_showStoreData.exitCallback = exitCallback;
		m_showStoreData.exitCallbackUserData = exitCallbackUserData;
		m_showStoreData.isTotallyFake = false;
		m_showStoreData.storeProduct = product;
		m_showStoreData.storeProductData = productData;
		m_showStoreData.numItemsRequired = numItemsRequired;
		m_showStoreData.dataModel = dataModel;
		m_showStoreData.useOverlayUI = useOverlayUI;
		m_showStoreData.pmtProductId = pmtProductId;
		m_showStoreData.closeOnTransactionComplete = false;
		ShowStoreWhenLoaded();
	}

	public void SetupDuelsStore(DuelsPopupManager duelsPopupManager)
	{
		m_currentShopType = ShopType.DUELS_STORE;
		m_showStoreData.isTotallyFake = false;
		m_showStoreData.storeProduct = ProductType.PRODUCT_TYPE_UNKNOWN;
		m_showStoreData.storeProductData = 0;
		m_showStoreData.useOverlayUI = false;
		m_showStoreData.closeOnTransactionComplete = true;
		m_stores[ShopType.DUELS_STORE] = duelsPopupManager;
		SetupLoadedStore(duelsPopupManager);
		if (m_view.HasStartedLoading)
		{
			ShowStore();
			return;
		}
		m_showStoreStart = Time.realtimeSinceStartup;
		m_waitingToShowStore = true;
		m_view.LoadAssets();
	}

	public void ShutDownDuelsStore()
	{
		if (m_stores.ContainsKey(ShopType.DUELS_STORE))
		{
			m_stores.Remove(ShopType.DUELS_STORE);
		}
	}

	public void SetupCardBackStore(CardBackInfoManager cardBackInfoManager, int productData)
	{
		m_currentShopType = ShopType.CARD_BACK_STORE;
		m_showStoreData.isTotallyFake = false;
		m_showStoreData.storeProduct = ProductType.PRODUCT_TYPE_CARD_BACK;
		m_showStoreData.storeProductData = productData;
		m_showStoreData.useOverlayUI = false;
		m_showStoreData.closeOnTransactionComplete = true;
		m_stores[ShopType.CARD_BACK_STORE] = cardBackInfoManager;
		SetupLoadedStore(cardBackInfoManager);
		if (m_view.HasStartedLoading)
		{
			ShowStore();
			return;
		}
		m_showStoreStart = Time.realtimeSinceStartup;
		m_waitingToShowStore = true;
		m_view.LoadAssets();
	}

	public void ShutDownCardBackStore()
	{
		if (m_stores.ContainsKey(ShopType.CARD_BACK_STORE))
		{
			m_stores.Remove(ShopType.CARD_BACK_STORE);
		}
	}

	public void SetupHeroSkinStore(HeroSkinInfoManager heroSkinInfoManager, int productData)
	{
		m_currentShopType = ShopType.HERO_SKIN_STORE;
		m_showStoreData.isTotallyFake = false;
		m_showStoreData.storeProduct = ProductType.PRODUCT_TYPE_HERO;
		m_showStoreData.storeProductData = productData;
		m_showStoreData.useOverlayUI = false;
		m_showStoreData.closeOnTransactionComplete = true;
		m_stores[ShopType.HERO_SKIN_STORE] = heroSkinInfoManager;
		SetupLoadedStore(heroSkinInfoManager);
		if (m_view.HasStartedLoading)
		{
			ShowStore();
			return;
		}
		m_showStoreStart = Time.realtimeSinceStartup;
		m_waitingToShowStore = true;
		m_view.LoadAssets();
	}

	public void ShutDownHeroSkinStore()
	{
		if (m_stores.ContainsKey(ShopType.HERO_SKIN_STORE))
		{
			m_stores.Remove(ShopType.HERO_SKIN_STORE);
		}
	}

	public void HandleDisconnect()
	{
		if (IsShown() && !TransactionInProgress())
		{
			while (IsPromptShowing)
			{
				Navigation.GoBack();
			}
			GetCurrentStore()?.Close();
			DialogManager.Get().ShowReconnectHelperDialog();
		}
		FireStatusChangedEventIfNeeded();
	}

	public void HideStore(ShopType shopType)
	{
		IStore store = GetStore(shopType);
		if (store != null)
		{
			store.Close();
			m_view.Hide();
			BnetBar.Get()?.RefreshCurrency();
		}
	}

	public bool TransactionInProgress()
	{
		if (Status != TransactionStatus.READY)
		{
			return true;
		}
		return false;
	}

	public bool HasOutstandingPurchaseNotices(ProductType product)
	{
		NetCache.ProfileNoticePurchase[] array = m_outstandingPurchaseNotices.ToArray();
		foreach (NetCache.ProfileNoticePurchase profileNoticePurchase in array)
		{
			if (!profileNoticePurchase.PMTProductID.HasValue)
			{
				continue;
			}
			Network.Bundle bundleFromPmtProductId = GetBundleFromPmtProductId(profileNoticePurchase.PMTProductID.Value);
			if (bundleFromPmtProductId == null)
			{
				continue;
			}
			foreach (Network.BundleItem item in bundleFromPmtProductId.Items)
			{
				if (item.ItemType == product)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static ProductType GetAdventureProductType(AdventureDbId adventureId)
	{
		if (s_adventureToProductMap.TryGetValue(adventureId, out var value))
		{
			return value;
		}
		if (GameUtils.IsExpansionAdventure(adventureId))
		{
			return ProductType.PRODUCT_TYPE_WING;
		}
		return ProductType.PRODUCT_TYPE_UNKNOWN;
	}

	public bool IsIdActiveTransaction(long id)
	{
		if (m_activeMoneyOrGTAPPTransaction == null)
		{
			return false;
		}
		return id == m_activeMoneyOrGTAPPTransaction.ID;
	}

	public bool IsPMTProductIDActiveTransaction(long id)
	{
		if (m_activeMoneyOrGTAPPTransaction == null)
		{
			return false;
		}
		return id == m_activeMoneyOrGTAPPTransaction.PMTProductID;
	}

	public static bool IsFirstPurchaseBundleOwned()
	{
		HiddenLicenseDbfRecord record = GameDbf.HiddenLicense.GetRecord(40);
		if (record == null)
		{
			return false;
		}
		AccountLicenseDbfRecord record2 = GameDbf.AccountLicense.GetRecord(record.AccountLicenseId);
		if (record2 == null)
		{
			return false;
		}
		return AccountLicenseMgr.Get().OwnsAccountLicense(record2.LicenseId);
	}

	private static LicenseStatus GetHiddenLicenseStatus(int hiddenLicenseId)
	{
		HiddenLicenseDbfRecord record = GameDbf.HiddenLicense.GetRecord(hiddenLicenseId);
		if (record == null)
		{
			return LicenseStatus.UNDEFINED;
		}
		AccountLicenseDbfRecord record2 = GameDbf.AccountLicense.GetRecord(record.AccountLicenseId);
		if (record2 == null)
		{
			return LicenseStatus.UNDEFINED;
		}
		if (AccountLicenseMgr.Get().OwnsAccountLicense(record2.LicenseId))
		{
			if (!record.IsBlocking)
			{
				return LicenseStatus.OWNED;
			}
			return LicenseStatus.OWNED_AND_BLOCKING;
		}
		return LicenseStatus.NOT_OWNED;
	}

	public static bool IsHiddenLicenseBundleOwned(int hiddenLicenseId)
	{
		LicenseStatus hiddenLicenseStatus = GetHiddenLicenseStatus(hiddenLicenseId);
		if (hiddenLicenseStatus != LicenseStatus.OWNED)
		{
			return hiddenLicenseStatus == LicenseStatus.OWNED_AND_BLOCKING;
		}
		return true;
	}

	public void SetCurrentlySelectedStorePack(StorePackId storePackId)
	{
		m_currentlySelectedId = storePackId;
	}

	private ModularBundleLayoutDbfRecord GetRegionNodeLayoutForHiddenLicense(int hiddenLicenseId)
	{
		foreach (ModularBundleLayoutDbfRecord record in GameDbf.ModularBundleLayout.GetRecords())
		{
			if (record.HiddenLicenseId != hiddenLicenseId)
			{
				continue;
			}
			string[] array = record.Regions.Split(',');
			for (int i = 0; i < array.Length; i++)
			{
				if (EnumUtils.SafeParse(array[i], constants.BnetRegion.REGION_UNKNOWN) == m_regionId)
				{
					return record;
				}
			}
		}
		Log.Store.PrintWarning($"Unable to load layout for hidden license id={hiddenLicenseId}, region={m_regionId}. Using Default Node Layout.");
		return GameDbf.ModularBundleLayout.GetRecord((ModularBundleLayoutDbfRecord r) => r.ModularBundleId == hiddenLicenseId);
	}

	public List<ModularBundleLayoutDbfRecord> GetRegionNodeLayoutsForBundle(int modularBundleRecordId)
	{
		List<ModularBundleLayoutDbfRecord> list = new List<ModularBundleLayoutDbfRecord>();
		foreach (ModularBundleLayoutDbfRecord record in GameDbf.ModularBundleLayout.GetRecords())
		{
			if (record.ModularBundleId != modularBundleRecordId)
			{
				continue;
			}
			string[] array = record.Regions.Split(',');
			for (int i = 0; i < array.Length; i++)
			{
				if (EnumUtils.SafeParse(array[i], constants.BnetRegion.REGION_UNKNOWN) == m_regionId)
				{
					list.Add(record);
				}
			}
		}
		if (list.Count == 0)
		{
			Log.Store.PrintWarning($"Unable to load layout for modular bundle id={modularBundleRecordId}, region={m_regionId}. Using Default Node Layout.");
			list.Add(GameDbf.ModularBundleLayout.GetRecord((ModularBundleLayoutDbfRecord r) => r.ModularBundleId == modularBundleRecordId));
		}
		return list;
	}

	private void ShowStoreWhenLoaded()
	{
		m_showStoreStart = Time.realtimeSinceStartup;
		HearthstonePerformance.Get()?.StartPerformanceFlow(new FlowPerformanceShop.ShopSetupConfig
		{
			shopType = m_currentShopType
		});
		m_waitingToShowStore = true;
		if (!IsCurrentStoreLoaded())
		{
			Load(m_currentShopType);
		}
		else
		{
			ShowStore();
		}
	}

	private void ShowStore()
	{
		if (!m_licenseAchievesListenerRegistered)
		{
			AchieveManager.Get().RegisterLicenseAddedAchievesUpdatedListener(OnLicenseAddedAchievesUpdated);
			m_licenseAchievesListenerRegistered = true;
		}
		if (TransactionStatus.READY == Status && AchieveManager.Get().HasActiveLicenseAddedAchieves())
		{
			Status = TransactionStatus.WAIT_ZERO_COST_LICENSE;
		}
		IStore currentStore = GetCurrentStore();
		bool flag = true;
		bool flag2 = false;
		switch (m_currentShopType)
		{
		case ShopType.GENERAL_STORE:
			if (IsOpen())
			{
				if (IsVintageStoreEnabled())
				{
					((GeneralStore)currentStore).SetMode(m_showStoreData.storeMode);
				}
				break;
			}
			Log.Store.PrintWarning("StoreManager.ShowStore(): Cannot show general store.. Store is not open");
			if (m_showStoreData.exitCallback != null)
			{
				m_showStoreData.exitCallback(authorizationBackButtonPressed: false, m_showStoreData.exitCallbackUserData);
			}
			flag = false;
			break;
		case ShopType.ADVENTURE_STORE:
		case ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET:
		case ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET:
			if (IsOpen())
			{
				AdventureStore adventureStore = (AdventureStore)currentStore;
				if (adventureStore != null)
				{
					adventureStore.SetAdventureProduct(m_showStoreData.storeProduct, m_showStoreData.storeProductData, m_showStoreData.numItemsRequired, m_showStoreData.pmtProductId);
				}
				break;
			}
			Log.Store.PrintWarning("StoreManager.ShowStore(): Cannot show adventure store.. Store is not open");
			if (m_showStoreData.exitCallback != null)
			{
				m_showStoreData.exitCallback(authorizationBackButtonPressed: false, m_showStoreData.exitCallbackUserData);
			}
			flag = false;
			flag2 = true;
			break;
		case ShopType.DUELS_STORE:
			if (!IsOpen())
			{
				flag = false;
			}
			break;
		}
		if (flag2)
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_SHOP_CLOSED_ALERT_HEADER"),
				m_text = GameStrings.Get("GLUE_SHOP_CLOSED_ALERT_TEXT"),
				m_showAlertIcon = false,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
			m_waitingToShowStore = false;
			return;
		}
		if (flag && currentStore != null)
		{
			Store store;
			if ((object)(store = currentStore as Store) != null)
			{
				store.Show(m_showStoreData.isTotallyFake, m_showStoreData.useOverlayUI, m_showStoreData.dataModel);
			}
			else
			{
				currentStore.Open();
			}
		}
		bool blocked = false;
		currentStore?.BlockInterface(blocked);
		Log.Store.Print("{0} took {1}s to load", m_currentShopType, Time.realtimeSinceStartup - m_showStoreStart);
		m_waitingToShowStore = false;
	}

	private void OnLoginCompleted()
	{
		FireStatusChangedEventIfNeeded();
	}

	private bool ShouldEnablePurchaseAuthBackButton(ShopType shopType)
	{
		return m_currentShopType switch
		{
			ShopType.ARENA_STORE => true, 
			ShopType.TAVERN_BRAWL_STORE => true, 
			_ => false, 
		};
	}

	private bool IsCurrentStoreLoaded()
	{
		IStore currentStore = GetCurrentStore();
		if (currentStore == null || !currentStore.IsReady())
		{
			return false;
		}
		if (!m_view.IsLoaded)
		{
			return false;
		}
		return true;
	}

	private void Load(ShopType shopType)
	{
		if (GetCurrentStore() != null)
		{
			return;
		}
		switch (shopType)
		{
		case ShopType.GENERAL_STORE:
		{
			if (IsVintageStoreEnabled())
			{
				AssetLoader.Get().InstantiatePrefab((string)ShopPrefabs.ShopPrefab, OnGeneralStoreLoaded);
				break;
			}
			Shop value = Shop.Get();
			m_stores[shopType] = value;
			break;
		}
		case ShopType.ARENA_STORE:
		{
			WidgetInstance arenaStoreWidget = WidgetInstance.Create(ShopPrefabs.ArenaShopPrefab);
			arenaStoreWidget.RegisterReadyListener(delegate
			{
				OnArenaStoreLoaded(null, arenaStoreWidget.gameObject, null);
			});
			break;
		}
		case ShopType.TAVERN_BRAWL_STORE:
		{
			WidgetInstance brawlStoreWidget = WidgetInstance.Create(ShopPrefabs.TavernBrawlShopPrefab);
			brawlStoreWidget.RegisterReadyListener(delegate
			{
				OnBrawlStoreLoaded(null, brawlStoreWidget.gameObject, null);
			});
			break;
		}
		case ShopType.ADVENTURE_STORE:
		{
			WidgetInstance adventureStoreWidget = WidgetInstance.Create(ShopPrefabs.AdventureShopPrefab);
			adventureStoreWidget.RegisterReadyListener(delegate
			{
				OnAdventureStoreLoaded(null, adventureStoreWidget.gameObject, null);
			});
			break;
		}
		case ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET:
		{
			WidgetInstance wingWidget = WidgetInstance.Create("AdventureStorymodeChapterStore.prefab:b797807e5c127af47badd08be121ea16");
			wingWidget.RegisterReadyListener(delegate
			{
				OnAdventureWingStoreLoaded(null, wingWidget.gameObject, null);
			});
			break;
		}
		case ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET:
		{
			WidgetInstance bookWidget = WidgetInstance.Create("AdventureStorymodeBookStore.prefab:922203a90d48c1d47b2f6813ff72f160");
			bookWidget.RegisterReadyListener(delegate
			{
				OnAdventureFullStoreLoaded(null, bookWidget.gameObject, null);
			});
			break;
		}
		}
		m_view.LoadAssets();
	}

	private void UnloadAndFreeMemory()
	{
		if (Shop.Get() != null)
		{
			Shop.Get().Unload();
		}
		foreach (KeyValuePair<ShopType, IStore> store in m_stores)
		{
			store.Value?.Unload();
		}
		m_stores.Clear();
		m_view.UnloadAssets();
	}

	private void FireStatusChangedEventIfNeeded()
	{
		bool flag = IsOpen();
		if (m_openWhenLastEventFired != flag)
		{
			this.OnStatusChanged(flag);
			m_openWhenLastEventFired = flag;
		}
	}

	private NetCache.NetCacheFeatures GetNetCacheFeatures()
	{
		if (!FeaturesReady)
		{
			return null;
		}
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (netObject == null)
		{
			FeaturesReady = false;
		}
		return netObject;
	}

	private static ItemPurchaseRule GetProductItemPurchaseRule(ProductType product, int productData)
	{
		switch (product)
		{
		case ProductType.PRODUCT_TYPE_BOOSTER:
		case ProductType.PRODUCT_TYPE_DRAFT:
		case ProductType.PRODUCT_TYPE_RANDOM_CARD:
		case ProductType.PRODUCT_TYPE_TAVERN_BRAWL_TICKET:
		case ProductType.PRODUCT_TYPE_CURRENCY:
			return ItemPurchaseRule.NO_LIMIT;
		case ProductType.PRODUCT_TYPE_CARD_BACK:
		case ProductType.PRODUCT_TYPE_HERO:
		case ProductType.PRODUCT_TYPE_MINI_SET:
		case ProductType.PRODUCT_TYPE_SELLABLE_DECK:
			return ItemPurchaseRule.NO_LIMIT;
		case ProductType.PRODUCT_TYPE_NAXX:
		case ProductType.PRODUCT_TYPE_BRM:
		case ProductType.PRODUCT_TYPE_LOE:
		case ProductType.PRODUCT_TYPE_WING:
		case ProductType.PRODUCT_TYPE_BATTLEGROUNDS_BONUS:
		case ProductType.PRODUCT_TYPE_PROGRESSION_BONUS:
			return ItemPurchaseRule.BLOCKING;
		case ProductType.PRODUCT_TYPE_HIDDEN_LICENSE:
		case ProductType.PRODUCT_TYPE_FIXED_LICENSE:
			return ItemPurchaseRule.BLOCKING;
		default:
			Log.Store.PrintError("No purchase rule defined for product type: {0}", product);
			return ItemPurchaseRule.UNDEFINED;
		}
	}

	public static ItemOwnershipStatus GetProductItemOwnershipStatus(ProductType product, int productData)
	{
		switch (product)
		{
		case ProductType.PRODUCT_TYPE_BOOSTER:
		case ProductType.PRODUCT_TYPE_DRAFT:
		case ProductType.PRODUCT_TYPE_RANDOM_CARD:
		case ProductType.PRODUCT_TYPE_TAVERN_BRAWL_TICKET:
		case ProductType.PRODUCT_TYPE_CURRENCY:
		case ProductType.PRODUCT_TYPE_MINI_SET:
		case ProductType.PRODUCT_TYPE_SELLABLE_DECK:
			return ItemOwnershipStatus.IGNORED;
		case ProductType.PRODUCT_TYPE_CARD_BACK:
			if (!CardBackManager.Get().IsCardBackOwned(productData))
			{
				return ItemOwnershipStatus.UNOWNED;
			}
			return ItemOwnershipStatus.OWNED;
		case ProductType.PRODUCT_TYPE_NAXX:
		case ProductType.PRODUCT_TYPE_BRM:
		case ProductType.PRODUCT_TYPE_LOE:
		case ProductType.PRODUCT_TYPE_WING:
			if (!AdventureProgressMgr.Get().IsReady)
			{
				return ItemOwnershipStatus.UNDEFINED;
			}
			if (!AdventureProgressMgr.Get().OwnsWing(productData))
			{
				return ItemOwnershipStatus.UNOWNED;
			}
			return ItemOwnershipStatus.OWNED;
		case ProductType.PRODUCT_TYPE_HERO:
		{
			if (NetCache.Get().GetNetObject<NetCache.NetCacheCollection>() == null)
			{
				return ItemOwnershipStatus.UNDEFINED;
			}
			string text = GameUtils.TranslateDbIdToCardId(productData);
			if (text == null || !CollectionManager.Get().IsCardInCollection(text, TAG_PREMIUM.NORMAL))
			{
				return ItemOwnershipStatus.UNOWNED;
			}
			return ItemOwnershipStatus.OWNED;
		}
		case ProductType.PRODUCT_TYPE_HIDDEN_LICENSE:
			if (AccountLicenseMgr.Get().FixedLicensesState != AccountLicenseMgr.LicenseUpdateState.SUCCESS)
			{
				return ItemOwnershipStatus.UNDEFINED;
			}
			switch (GetHiddenLicenseStatus(productData))
			{
			case LicenseStatus.NOT_OWNED:
				return ItemOwnershipStatus.UNOWNED;
			case LicenseStatus.OWNED:
			case LicenseStatus.OWNED_AND_BLOCKING:
				return ItemOwnershipStatus.OWNED;
			default:
				return ItemOwnershipStatus.UNDEFINED;
			}
		case ProductType.PRODUCT_TYPE_FIXED_LICENSE:
			if (AccountLicenseMgr.Get().FixedLicensesState != AccountLicenseMgr.LicenseUpdateState.SUCCESS)
			{
				return ItemOwnershipStatus.UNDEFINED;
			}
			if (!AccountLicenseMgr.Get().OwnsAccountLicense(productData))
			{
				return ItemOwnershipStatus.UNOWNED;
			}
			return ItemOwnershipStatus.OWNED;
		case ProductType.PRODUCT_TYPE_BATTLEGROUNDS_BONUS:
		case ProductType.PRODUCT_TYPE_PROGRESSION_BONUS:
		{
			if (AccountLicenseMgr.Get().FixedLicensesState != AccountLicenseMgr.LicenseUpdateState.SUCCESS)
			{
				return ItemOwnershipStatus.UNDEFINED;
			}
			AccountLicenseDbfRecord record = GameDbf.AccountLicense.GetRecord(productData);
			if (record == null || !AccountLicenseMgr.Get().OwnsAccountLicense(record.LicenseId))
			{
				return ItemOwnershipStatus.UNOWNED;
			}
			return ItemOwnershipStatus.OWNED;
		}
		default:
			return ItemOwnershipStatus.UNDEFINED;
		}
	}

	private string GetSingleItemProductName(Network.BundleItem item)
	{
		string result = string.Empty;
		switch (item.ItemType)
		{
		case ProductType.PRODUCT_TYPE_BOOSTER:
		{
			string text = GameDbf.Booster.GetRecord(item.ProductData).Name;
			result = GameStrings.Format("GLUE_STORE_PRODUCT_NAME_PACK", item.Quantity, text);
			break;
		}
		case ProductType.PRODUCT_TYPE_DRAFT:
			result = GameStrings.Get("GLUE_STORE_PRODUCT_NAME_FORGE_TICKET");
			break;
		case ProductType.PRODUCT_TYPE_BATTLEGROUNDS_BONUS:
			result = GameStrings.Get("GLUE_STORE_PRODUCT_NAME_BATTLEGROUNDS_BONUS");
			break;
		case ProductType.PRODUCT_TYPE_PROGRESSION_BONUS:
			result = GameStrings.Get("GLUE_STORE_PRODUCT_NAME_PROGRESSION_BONUS");
			break;
		case ProductType.PRODUCT_TYPE_NAXX:
		case ProductType.PRODUCT_TYPE_BRM:
		case ProductType.PRODUCT_TYPE_LOE:
		case ProductType.PRODUCT_TYPE_WING:
			result = AdventureProgressMgr.GetWingName(item.ProductData);
			break;
		case ProductType.PRODUCT_TYPE_CARD_BACK:
		{
			CardBackDbfRecord record2 = GameDbf.CardBack.GetRecord(item.ProductData);
			if (record2 != null)
			{
				result = record2.Name;
			}
			break;
		}
		case ProductType.PRODUCT_TYPE_HERO:
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(item.ProductData);
			if (entityDef != null)
			{
				result = entityDef.GetName();
			}
			break;
		}
		case ProductType.PRODUCT_TYPE_TAVERN_BRAWL_TICKET:
		{
			TavernBrawlTicketDbfRecord record = GameDbf.TavernBrawlTicket.GetRecord(item.ProductData);
			if (record != null)
			{
				result = record.StoreName;
			}
			break;
		}
		default:
			Log.Store.PrintWarning($"StoreManager.GetSingleItemProductName(): don't know how to format name for bundle product {item.ItemType}");
			break;
		}
		return result;
	}

	private string GetMultiItemProductName(Network.Bundle bundle)
	{
		HashSet<ProductType> productsInItemList = GetProductsInItemList(bundle.Items);
		if (productsInItemList.Contains(ProductType.PRODUCT_TYPE_NAXX))
		{
			return GameStrings.Format("GLUE_STORE_PRODUCT_NAME_NAXX_WING_BUNDLE", bundle.Items.Count);
		}
		if (productsInItemList.Contains(ProductType.PRODUCT_TYPE_BRM))
		{
			if (productsInItemList.Contains(ProductType.PRODUCT_TYPE_CARD_BACK))
			{
				return GameStrings.Get("GLUE_STORE_PRODUCT_NAME_BRM_PRESALE_BUNDLE");
			}
			return GameStrings.Format("GLUE_STORE_PRODUCT_NAME_BRM_WING_BUNDLE", bundle.Items.Count);
		}
		if (productsInItemList.Contains(ProductType.PRODUCT_TYPE_LOE))
		{
			return GameStrings.Format("GLUE_STORE_PRODUCT_NAME_LOE_WING_BUNDLE", bundle.Items.Count);
		}
		if (productsInItemList.Contains(ProductType.PRODUCT_TYPE_WING))
		{
			int num = (from r in bundle.Items
				where r.ItemType == ProductType.PRODUCT_TYPE_WING
				select r.ProductData).FirstOrDefault();
			if (num == 0)
			{
				Log.Store.PrintError("StoreManager.GetMultiItemProductName: bundle with PRODUCT_TYPE_WING did not contain a valid wing ID in any of its product data.");
			}
			string adventureProductStringKey = GameUtils.GetAdventureProductStringKey(num);
			if (productsInItemList.Contains(ProductType.PRODUCT_TYPE_CARD_BACK))
			{
				return GameStrings.Get("GLUE_STORE_PRODUCT_NAME_" + adventureProductStringKey + "_PRESALE_BUNDLE");
			}
			int num2 = bundle.Items.Count((Network.BundleItem x) => x.ItemType == ProductType.PRODUCT_TYPE_WING);
			return GameStrings.Format("GLUE_STORE_PRODUCT_NAME_" + adventureProductStringKey + "_WING_BUNDLE", num2);
		}
		if (productsInItemList.Contains(ProductType.PRODUCT_TYPE_HIDDEN_LICENSE))
		{
			Network.BundleItem bundleItem = bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_HIDDEN_LICENSE);
			if (bundleItem.ProductData == 40)
			{
				return GameStrings.Get("GLUE_STORE_PRODUCT_NAME_FIRST_PURCHASE_BUNDLE");
			}
			if (bundleItem.ProductData == 27)
			{
				return GameStrings.Get("GLUE_STORE_PRODUCT_NAME_MAMMOTH_BUNDLE");
			}
			ModularBundleLayoutDbfRecord regionNodeLayoutForHiddenLicense = GetRegionNodeLayoutForHiddenLicense(bundleItem.ProductData);
			if (regionNodeLayoutForHiddenLicense != null)
			{
				return regionNodeLayoutForHiddenLicense.OrderSummaryName;
			}
		}
		if (productsInItemList.Contains(ProductType.PRODUCT_TYPE_HERO))
		{
			Network.BundleItem bundleItem2 = bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_HERO);
			if (bundleItem2 != null)
			{
				return GetSingleItemProductName(bundleItem2);
			}
		}
		else if (productsInItemList.Contains(ProductType.PRODUCT_TYPE_BOOSTER) && productsInItemList.Contains(ProductType.PRODUCT_TYPE_CARD_BACK))
		{
			if (bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_BOOSTER && obj.ProductData == 10) != null)
			{
				return GameStrings.Get("GLUE_STORE_PRODUCT_NAME_TGT_PRESALE_BUNDLE");
			}
			if (bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_BOOSTER && obj.ProductData == 11) != null)
			{
				return GameStrings.Get("GLUE_STORE_PRODUCT_NAME_OG_PRESALE_BUNDLE");
			}
			if (bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_BOOSTER && obj.ProductData == 20) != null)
			{
				return GameStrings.Get("GLUE_STORE_PRODUCT_NAME_GORO_PRESALE_BUNDLE");
			}
			if (bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_BOOSTER && obj.ProductData == 21) != null)
			{
				return GameStrings.Get("GLUE_STORE_PRODUCT_NAME_ICC_PRESALE_BUNDLE");
			}
			if (bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_BOOSTER && obj.ProductData == 30) != null)
			{
				return GameStrings.Get("GLUE_STORE_PRODUCT_NAME_LOOT_PRESALE_BUNDLE");
			}
			if (bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_BOOSTER && obj.ProductData == 31) != null)
			{
				return GameStrings.Get("GLUE_STORE_PRODUCT_NAME_GIL_PRESALE_BUNDLE");
			}
		}
		else if (productsInItemList.Contains(ProductType.PRODUCT_TYPE_BOOSTER) && productsInItemList.Contains(ProductType.PRODUCT_TYPE_CURRENCY))
		{
			Network.BundleItem bundleItem3 = bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_BOOSTER);
			Network.BundleItem bundleItem4 = bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_CURRENCY && obj.ProductData == 2);
			if (bundleItem3 != null && bundleItem4 != null)
			{
				string text = GameDbf.Booster.GetRecord(bundleItem3.ProductData).Name;
				return GameStrings.Format("GLUE_STORE_PRODUCT_NAME_DUST", bundleItem4.Quantity, bundleItem3.Quantity, text);
			}
		}
		string text2 = string.Empty;
		foreach (Network.BundleItem item in bundle.Items)
		{
			text2 += $"[Product={item.ItemType},ProductData={item.ProductData},Quantity={item.Quantity}],";
		}
		Log.Store.PrintWarning("StoreManager.GetMultiItemProductName(): don't know how to format product name for items '" + text2 + "'");
		return string.Empty;
	}

	private bool GetBoosterGoldCostNoGTAPP(int boosterID, out long cost)
	{
		cost = 0L;
		if (!m_goldCostBooster.ContainsKey(boosterID))
		{
			return false;
		}
		if (!CanBuyBoosterWithGold(boosterID))
		{
			return false;
		}
		Network.GoldCostBooster goldCostBooster = m_goldCostBooster[boosterID];
		if (!goldCostBooster.Cost.HasValue)
		{
			return false;
		}
		if (goldCostBooster.Cost.Value <= 0)
		{
			return false;
		}
		cost = goldCostBooster.Cost.Value;
		return true;
	}

	private bool GetArenaGoldCostNoGTAPP(out long cost)
	{
		cost = 0L;
		if (!m_goldCostArena.HasValue)
		{
			return false;
		}
		cost = m_goldCostArena.Value;
		return true;
	}

	private bool AutoCancelPurchaseIfNeeded(float now)
	{
		if (now - m_lastCancelRequestTime < m_secsBeforeAutoCancel)
		{
			return false;
		}
		return AutoCancelPurchaseIfPossible();
	}

	private bool AutoCancelPurchaseIfPossible()
	{
		MoneyOrGTAPPTransaction activeMoneyOrGTAPPTransaction = m_activeMoneyOrGTAPPTransaction;
		if (activeMoneyOrGTAPPTransaction == null || !activeMoneyOrGTAPPTransaction.Provider.HasValue)
		{
			return false;
		}
		if (BattlePayProvider.BP_PROVIDER_BLIZZARD == m_activeMoneyOrGTAPPTransaction.Provider.Value)
		{
			if (!IsSimpleCheckoutFeatureEnabled() || m_activeMoneyOrGTAPPTransaction.IsGTAPP)
			{
				TransactionStatus status = Status;
				if ((uint)(status - 1) <= 1u || (uint)(status - 6) <= 4u)
				{
					Log.Store.Print("StoreManager.AutoCancelPurchaseIfPossible() canceling Blizzard purchase, status={0}", Status);
					Status = TransactionStatus.AUTO_CANCELING;
					m_lastCancelRequestTime = Time.realtimeSinceStartup;
					Network.Get().CancelBlizzardPurchase(isAutoCanceled: true, null, null);
					return true;
				}
			}
			else if (Status != TransactionStatus.IN_PROGRESS_BLIZZARD_CHECKOUT)
			{
				if (HearthstoneServices.TryGet<HearthstoneCheckout>(out var service))
				{
					service.RequestClose();
				}
				Status = TransactionStatus.READY;
				m_lastCancelRequestTime = Time.realtimeSinceStartup;
				m_activeMoneyOrGTAPPTransaction = null;
				return true;
			}
		}
		return false;
	}

	private void CancelBlizzardPurchase(CancelPurchase.CancelReason? reason = null, string errorMessage = null)
	{
		Log.Store.Print("StoreManager.CancelBlizzardPurchase() reason=", reason.HasValue ? reason.Value.ToString() : "null");
		Status = TransactionStatus.USER_CANCELING;
		m_lastCancelRequestTime = Time.realtimeSinceStartup;
		Network.Get().CancelBlizzardPurchase(isAutoCanceled: false, reason, errorMessage);
	}

	private bool HaveProductsToSell()
	{
		if (m_bundles.Count <= 0 && m_goldCostBooster.Count <= 0)
		{
			return m_goldCostArena.HasValue;
		}
		return true;
	}

	public bool IsBundleAvailableNow(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return false;
		}
		ProductAvailabilityRange bundleAvailabilityRange = GetBundleAvailabilityRange(bundle);
		if (bundleAvailabilityRange != null && bundleAvailabilityRange.IsBuyableAtTime(DateTime.UtcNow))
		{
			return true;
		}
		return false;
	}

	public ProductAvailabilityRange GetBundleAvailabilityRange(Network.Bundle bundle)
	{
		if (m_ignoreProductTiming)
		{
			return new ProductAvailabilityRange();
		}
		ProductAvailabilityRange productAvailabilityRange = null;
		if (!string.IsNullOrEmpty(bundle.ProductEvent))
		{
			SpecialEventType eventType = SpecialEventManager.GetEventType(bundle.ProductEvent);
			switch (eventType)
			{
			case SpecialEventType.UNKNOWN:
				return null;
			case SpecialEventType.SPECIAL_EVENT_NEVER:
				productAvailabilityRange = new ProductAvailabilityRange(bundle.ProductEvent, null, null);
				productAvailabilityRange.IsNever = true;
				return productAvailabilityRange;
			default:
			{
				if (SpecialEventManager.Get().GetEventRangeUtc(eventType, out var start, out var end))
				{
					productAvailabilityRange = new ProductAvailabilityRange(bundle.ProductEvent, start, end);
					if (productAvailabilityRange.IsNever)
					{
						return productAvailabilityRange;
					}
					break;
				}
				return null;
			}
			case SpecialEventType.IGNORE:
				break;
			}
		}
		ProductAvailabilityRange productAvailabilityRange2 = null;
		if (!bundle.VisibleOnSalePeriodOnly)
		{
			productAvailabilityRange2 = new ProductAvailabilityRange();
		}
		else
		{
			DateTime utcNow = DateTime.UtcNow;
			foreach (int saleId in bundle.SaleIds)
			{
				m_sales.TryGetValue(saleId, out var value);
				if (value != null)
				{
					ProductAvailabilityRange productAvailabilityRange3 = new ProductAvailabilityRange(value);
					TimeSpan displacement;
					TimeSpan displacement2;
					if (productAvailabilityRange2 == null)
					{
						productAvailabilityRange2 = productAvailabilityRange3;
					}
					else if (ProductAvailabilityRange.AreOverlapping(productAvailabilityRange2, productAvailabilityRange3))
					{
						productAvailabilityRange2.UnionWith(productAvailabilityRange3);
					}
					else if (!productAvailabilityRange2.TryGetTimeDisplacementRequiredToBeBuyable(utcNow, out displacement))
					{
						productAvailabilityRange2 = productAvailabilityRange3;
					}
					else if (productAvailabilityRange3.TryGetTimeDisplacementRequiredToBeBuyable(utcNow, out displacement2) && Math.Abs(displacement2.Ticks) <= Math.Abs(displacement.Ticks))
					{
						productAvailabilityRange2 = productAvailabilityRange3;
					}
				}
			}
		}
		if (productAvailabilityRange != null)
		{
			if (productAvailabilityRange2 == null)
			{
				productAvailabilityRange2 = productAvailabilityRange;
			}
			else
			{
				productAvailabilityRange2.IntersectWith(productAvailabilityRange);
			}
		}
		return productAvailabilityRange2;
	}

	private bool DoesBundleContainDust(Network.Bundle bundle)
	{
		if (bundle?.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_CURRENCY) == null)
		{
			return false;
		}
		return true;
	}

	public bool ShouldShowFeaturedDustJar(Network.Bundle bundle)
	{
		if (m_regionId == constants.BnetRegion.REGION_CN && m_currentlySelectedId.Type == StorePackType.BOOSTER)
		{
			return DoesBundleContainDust(bundle);
		}
		return false;
	}

	public int DustQuantityInBundle(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return 0;
		}
		return bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_CURRENCY)?.Quantity ?? 0;
	}

	public int DustBaseQuantityInBundle(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return 0;
		}
		return bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_CURRENCY)?.BaseQuantity ?? 0;
	}

	public int PackQuantityInBundle(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return 0;
		}
		return bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_BOOSTER)?.Quantity ?? 0;
	}

	private void OnStoreOpen()
	{
		if (BnetBar.Get() != null)
		{
			BnetBar.Get().RefreshCurrency();
		}
		this.OnStoreShown?.Invoke();
	}

	private void OnStoreExit(bool authorizationBackButtonPressed, object userData)
	{
		m_showStoreData.exitCallback?.Invoke(authorizationBackButtonPressed, userData);
		if (m_activeMoneyOrGTAPPTransaction != null)
		{
			m_activeMoneyOrGTAPPTransaction.ClosedStore = true;
		}
		if (m_view.ChallengePrompt.IsLoaded && !m_view.ChallengePrompt.Cancel(OnChallengeCancel))
		{
			AutoCancelPurchaseIfPossible();
		}
		UnblockStoreInterface();
		m_view.Hide();
		this.OnStoreHidden();
		if (BnetBar.Get() != null)
		{
			BnetBar.Get().RefreshCurrency();
		}
		HearthstonePerformance.Get()?.StopCurrentFlow();
	}

	private void OnStoreInfo(object userData)
	{
		ShowStoreInfo();
	}

	public void ShowStoreInfo()
	{
		BlockStoreInterface();
		m_view.SendToBam.Show(null, StoreSendToBAM.BAMReason.PAYMENT_INFO, "", fromPreviousPurchase: false);
	}

	public bool CanBuyBundle(Network.Bundle bundleToBuy)
	{
		if (bundleToBuy == null)
		{
			Log.Store.PrintWarning("Null bundle passed to CanBuyBundle!");
			return false;
		}
		if (AchieveManager.Get() == null || !AchieveManager.Get().IsReady())
		{
			return false;
		}
		if (bundleToBuy.Items.Count < 1)
		{
			Log.Store.PrintWarning($"Attempting to buy bundle {bundleToBuy.PMTProductID}, which does not contain any items!");
			return false;
		}
		if (!IsBundleAvailableNow(bundleToBuy))
		{
			return false;
		}
		foreach (Network.BundleItem item in bundleToBuy.Items)
		{
			if (!CanBuyProductItem(item.ItemType, item.ProductData))
			{
				return false;
			}
		}
		return true;
	}

	private bool CanBuyProductItem(ProductType product, int productData)
	{
		if (AchieveManager.Get() == null || !AchieveManager.Get().IsReady())
		{
			return false;
		}
		switch (GetProductItemPurchaseRule(product, productData))
		{
		case ItemPurchaseRule.UNDEFINED:
		case ItemPurchaseRule.NO_LIMIT:
			return true;
		case ItemPurchaseRule.BLOCKING:
			if (GetProductItemOwnershipStatus(product, productData) == ItemOwnershipStatus.UNOWNED)
			{
				return true;
			}
			return false;
		default:
			return true;
		}
	}

	private void OnStoreBuyWithMoney(BuyPmtProductEventArgs args)
	{
		if (TemporaryAccountManager.IsTemporaryAccount() && !IsSoftAccountPurchasingEnabled())
		{
			TemporaryAccountManager.Get().ShowHealUpDialog(GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_01"), GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_02"), TemporaryAccountManager.HealUpReason.REAL_MONEY, userTriggered: true, null);
			return;
		}
		Network.Bundle bundleFromPmtProductId = GetBundleFromPmtProductId(args.pmtProductId);
		if (bundleFromPmtProductId == null)
		{
			Log.Store.PrintError("OnStoreBuyWithMoney failed: bundle not found for pmtProductID = {0}.", args.pmtProductId);
		}
		if (!CanBuyBundle(bundleFromPmtProductId))
		{
			Log.Store.PrintError("OnStoreBuyWithMoney failed: CanBuyBundle is false for pmtProductID = {0}.", args.pmtProductId);
		}
		else if (IsSimpleCheckoutFeatureEnabled())
		{
			OnStoreBuyWithCheckout(args);
		}
		else if (IsCheckoutFallbackSupported())
		{
			SetCanTapOutConfirmationUI(closeConfirmationUI: true);
			BlockStoreInterface();
			SetActiveMoneyOrGTAPPTransaction(UNKNOWN_TRANSACTION_ID, args.pmtProductId, BattlePayProvider.BP_PROVIDER_BLIZZARD, isGTAPP: false, tryToResolvePreviousTransactionNotices: false);
			m_lastCancelRequestTime = Time.realtimeSinceStartup;
			m_view.PurchaseAuth.Show(m_activeMoneyOrGTAPPTransaction, enableBack: false, isZeroCostLicense: false);
			Status = TransactionStatus.WAIT_METHOD_OF_PAYMENT;
			Network.Get().GetPurchaseMethod(args.pmtProductId, args.quantity, m_currency);
		}
	}

	private void OnStoreBuyWithGTAPP(BuyPmtProductEventArgs args)
	{
		Network.Bundle bundleFromPmtProductId = GetBundleFromPmtProductId(args.pmtProductId);
		if (!CanBuyBundle(bundleFromPmtProductId))
		{
			Log.Store.PrintError("Purchase with GTAPP failed (PMT product ID = {0}): CanBuyProductItem is false.", args.pmtProductId);
			return;
		}
		SetCanTapOutConfirmationUI(closeConfirmationUI: true);
		BlockStoreInterface();
		SetActiveMoneyOrGTAPPTransaction(UNKNOWN_TRANSACTION_ID, args.pmtProductId, BattlePayProvider.BP_PROVIDER_BLIZZARD, isGTAPP: true, tryToResolvePreviousTransactionNotices: false);
		Status = TransactionStatus.WAIT_METHOD_OF_PAYMENT;
		m_lastCancelRequestTime = Time.realtimeSinceStartup;
		m_view.PurchaseAuth.Show(m_activeMoneyOrGTAPPTransaction, enableBack: false, isZeroCostLicense: false);
		Network.Get().GetPurchaseMethod(args.pmtProductId, args.quantity, Currency.GTAPP);
	}

	private void OnStoreBuyWithGoldNoGTAPP(NoGTAPPTransactionData noGTAPPtransactionData)
	{
		if (noGTAPPtransactionData == null)
		{
			Log.Store.PrintError("Purchase failed: null transaction data.");
			return;
		}
		if (!CanBuyProductItem(noGTAPPtransactionData.Product, noGTAPPtransactionData.ProductData))
		{
			Log.Store.PrintError("Purchase direct with gold (no GTAPP) failed: CanBuyProductItem is false.");
			return;
		}
		BlockStoreInterface();
		m_view.PurchaseAuth.Show(null, ShouldEnablePurchaseAuthBackButton(m_currentShopType), isZeroCostLicense: false);
		Status = TransactionStatus.IN_PROGRESS_GOLD_NO_GTAPP;
		Network.Get().PurchaseViaGold(noGTAPPtransactionData.Quantity, noGTAPPtransactionData.Product, noGTAPPtransactionData.ProductData);
	}

	private void OnStoreBuyWithCheckout(BuyPmtProductEventArgs args)
	{
		HearthstoneCheckout service;
		if (GetBundleFromPmtProductId(args.pmtProductId) == null)
		{
			Log.Store.PrintError("Cannot buy product PMT ID = {0}. Bundle not found.", args.pmtProductId);
		}
		else if (!IsSimpleCheckoutFeatureEnabled())
		{
			Log.Store.PrintError("Purchase failed: Checkout feature is disabled.");
		}
		else if (!HearthstoneServices.TryGet<HearthstoneCheckout>(out service))
		{
			Log.Store.PrintError("Purchase failed: Checkout service is not available.");
		}
		else if (args.paymentCurrency == CurrencyType.REAL_MONEY)
		{
			if (TemporaryAccountManager.IsTemporaryAccount() && !IsSoftAccountPurchasingEnabled())
			{
				TemporaryAccountManager.Get().ShowHealUpDialog(GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_01"), GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_02"), TemporaryAccountManager.HealUpReason.REAL_MONEY, userTriggered: true, null);
				return;
			}
			Status = TransactionStatus.WAIT_BLIZZARD_CHECKOUT;
			SetActiveMoneyOrGTAPPTransaction(UNKNOWN_TRANSACTION_ID, args.pmtProductId, BattlePayProvider.BP_PROVIDER_BLIZZARD, isGTAPP: false, tryToResolvePreviousTransactionNotices: false);
			m_lastCancelRequestTime = Time.realtimeSinceStartup;
			SetCanTapOutConfirmationUI(closeConfirmationUI: true);
			BlockStoreInterface();
			service.ShowCheckout(args.pmtProductId, ShopUtils.GetCurrencyCode(args.paymentCurrency), (uint)args.quantity);
			if (HasExternalStore)
			{
				m_view.PurchaseAuth.Show(m_activeMoneyOrGTAPPTransaction, enableBack: false, isZeroCostLicense: false);
			}
		}
		else if (ShopUtils.IsCurrencyVirtual(args.paymentCurrency))
		{
			Status = TransactionStatus.WAIT_BLIZZARD_CHECKOUT;
			SetActiveMoneyOrGTAPPTransaction(UNKNOWN_TRANSACTION_ID, args.pmtProductId, BattlePayProvider.BP_PROVIDER_BLIZZARD, isGTAPP: false, tryToResolvePreviousTransactionNotices: false);
			m_lastCancelRequestTime = Time.realtimeSinceStartup;
			SetCanTapOutConfirmationUI(closeConfirmationUI: true);
			BlockStoreInterface();
			if (m_view.PurchaseAuth.IsShown)
			{
				m_view.PurchaseAuth.StartNewTransaction(m_activeMoneyOrGTAPPTransaction, enableBack: false, isZeroCostLicense: false);
			}
			else
			{
				m_view.PurchaseAuth.Show(m_activeMoneyOrGTAPPTransaction, enableBack: false, isZeroCostLicense: false);
			}
			service.PurchaseWithVirtualCurrency(args.pmtProductId, ShopUtils.GetCurrencyCode(args.paymentCurrency), (uint)args.quantity);
		}
		else
		{
			Log.Store.PrintError("Buy with checkout failed: Invalid currency type {0}", args.paymentCurrency);
		}
	}

	private void OnSummaryConfirm(int quantity, object userData)
	{
		m_view.PurchaseAuth.Show(m_activeMoneyOrGTAPPTransaction, ShouldEnablePurchaseAuthBackButton(m_currentShopType), isZeroCostLicense: false);
		if (m_challengePurchaseMethod != null)
		{
			m_view.ChallengePrompt.StartChallenge(m_challengePurchaseMethod.ChallengeURL);
		}
		else
		{
			ConfirmPurchase();
		}
	}

	private void ConfirmPurchase()
	{
		Status = ((!m_activeMoneyOrGTAPPTransaction.IsGTAPP) ? TransactionStatus.IN_PROGRESS_MONEY : TransactionStatus.IN_PROGRESS_GOLD_GTAPP);
		Network.Get().ConfirmPurchase();
	}

	private void OnSummaryCancel(object userData)
	{
		CancelBlizzardPurchase();
		UnblockStoreInterface();
	}

	private void OnSummaryInfo(object userData)
	{
		BlockStoreInterface();
		AutoCancelPurchaseIfPossible();
		m_view.SendToBam.Show(null, StoreSendToBAM.BAMReason.EULA_AND_TOS, string.Empty, fromPreviousPurchase: false);
	}

	private void OnSummaryPaymentAndTOS(object userData)
	{
		AutoCancelPurchaseIfPossible();
		m_view.LegalBam.Show();
	}

	private void OnChallengeComplete(string challengeID, bool isSuccess, CancelPurchase.CancelReason? reason, string internalErrorInfo)
	{
		if (!isSuccess)
		{
			OnChallengeCancel_Internal(challengeID, reason, internalErrorInfo);
			return;
		}
		m_view.PurchaseAuth.Show(m_activeMoneyOrGTAPPTransaction, ShouldEnablePurchaseAuthBackButton(m_currentShopType), isZeroCostLicense: false);
		Status = TransactionStatus.CHALLENGE_SUBMITTED;
		ConfirmPurchase();
	}

	private void OnChallengeCancel(string challengeID)
	{
		OnChallengeCancel_Internal(challengeID, null, null);
	}

	private void OnChallengeCancel_Internal(string challengeID, CancelPurchase.CancelReason? reason, string errorMessage)
	{
		Debug.LogFormat("Canceling purchase from challengeId={0} reason={1} msg={2}", challengeID, reason.HasValue ? reason.Value.ToString() : "null", errorMessage);
		Status = TransactionStatus.CHALLENGE_CANCELED;
		CancelBlizzardPurchase(reason, errorMessage);
		UnblockStoreInterface();
		m_view.Hide();
	}

	private void OnSendToBAMOkay(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, StoreSendToBAM.BAMReason reason)
	{
		if (moneyOrGTAPPTransaction != null)
		{
			ConfirmActiveMoneyTransaction(moneyOrGTAPPTransaction.ID);
		}
		if (reason == StoreSendToBAM.BAMReason.PAYMENT_INFO)
		{
			UnblockStoreInterface();
		}
		else
		{
			m_view.DoneWithBam.Show();
		}
	}

	private void OnSendToBAMCancel(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction)
	{
		if (moneyOrGTAPPTransaction != null)
		{
			ConfirmActiveMoneyTransaction(moneyOrGTAPPTransaction.ID);
		}
		UnblockStoreInterface();
	}

	private void OnSendToBAMLegal(StoreLegalBAMLinks.BAMReason reason)
	{
		UnblockStoreInterface();
	}

	private void OnAchievesUpdated(List<Achievement> updatedAchives, List<Achievement> completedAchives, object userData)
	{
		m_completedAchieves = AchieveManager.Get().GetNewCompletedAchievesToShow();
		ShowCompletedAchieve();
	}

	private void OnLicenseAddedAchievesUpdated(List<Achievement> activeLicenseAddedAchieves, object userData)
	{
		if (TransactionStatus.WAIT_ZERO_COST_LICENSE == Status && activeLicenseAddedAchieves.Count <= 0)
		{
			Log.Store.Print("StoreManager.OnLicenseAddedAchievesUpdated(): done waiting for licenses!");
			if (IsCurrentStoreLoaded())
			{
				Processor.QueueJob("StoreManager.ShowCompletePurchaseSuccessWhenReady", Job_ShowCompletePurchaseSuccessWhenReady(null));
			}
			Status = TransactionStatus.READY;
		}
	}

	private void ShowCompletedAchieve()
	{
		bool flag = m_completedAchieves.Count == 0;
		if (m_currentShopType == ShopType.GENERAL_STORE)
		{
			GeneralStore generalStore = (GeneralStore)GetCurrentStore();
			if (generalStore != null)
			{
				generalStore.EnableClickCatcher(flag);
			}
		}
		if (!flag)
		{
			Achievement quest = m_completedAchieves[0];
			m_completedAchieves.RemoveAt(0);
			QuestToast.ShowQuestToast(UserAttentionBlocker.NONE, delegate
			{
				ShowCompletedAchieve();
			}, updateCacheValues: true, quest, fullScreenEffects: false);
		}
	}

	private void OnPurchaseResultAcknowledged(bool success, MoneyOrGTAPPTransaction moneyOrGTAPPTransaction)
	{
		PaymentMethod paymentMethod;
		Network.Bundle arg;
		if (moneyOrGTAPPTransaction == null)
		{
			paymentMethod = PaymentMethod.GOLD_NO_GTAPP;
			arg = null;
		}
		else
		{
			if (moneyOrGTAPPTransaction.ID > 0)
			{
				m_transactionIDsConclusivelyHandled.Add(moneyOrGTAPPTransaction.ID);
			}
			paymentMethod = (moneyOrGTAPPTransaction.IsGTAPP ? PaymentMethod.GOLD_GTAPP : PaymentMethod.MONEY);
			arg = GetBundleFromPmtProductId(moneyOrGTAPPTransaction.PMTProductID.GetValueOrDefault());
		}
		if (PaymentMethod.GOLD_NO_GTAPP != paymentMethod)
		{
			ConfirmActiveMoneyTransaction(moneyOrGTAPPTransaction.ID);
		}
		if (success)
		{
			this.OnSuccessfulPurchaseAck(arg, paymentMethod);
		}
		else
		{
			this.OnFailedPurchaseAck(arg, paymentMethod);
		}
		SetCanTapOutConfirmationUI(closeConfirmationUI: true);
		UnblockStoreInterface();
		IStore currentStore = GetCurrentStore();
		if (m_currentShopType == ShopType.ADVENTURE_STORE || m_currentShopType == ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET || m_currentShopType == ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET)
		{
			currentStore.Close();
		}
		if (!BattlePayAvailable && m_currentShopType == ShopType.GENERAL_STORE)
		{
			currentStore.Close();
		}
	}

	private void OnAuthExit()
	{
		this.OnAuthorizationExit();
	}

	private void BlockStoreInterface()
	{
		GetCurrentStore()?.BlockInterface(blocked: true);
	}

	private void UnblockStoreInterface()
	{
		GetCurrentStore()?.BlockInterface(blocked: false);
	}

	private void HandlePurchaseSuccess(PurchaseErrorSource? source, MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, string thirdPartyID, HearthstoneCheckoutTransactionData checkoutTransactionData)
	{
		Status = TransactionStatus.READY;
		SendShopPurchaseEventTelemetry(isComplete: true);
		m_pendingProductPurchaseArgs = null;
		PaymentMethod paymentMethod;
		Network.Bundle bundle;
		if (moneyOrGTAPPTransaction == null)
		{
			paymentMethod = PaymentMethod.GOLD_NO_GTAPP;
			bundle = null;
		}
		else
		{
			paymentMethod = ((checkoutTransactionData == null || !ShopUtils.IsCurrencyVirtual(ShopUtils.GetCurrencyTypeFromCode(checkoutTransactionData.CurrencyCode))) ? (moneyOrGTAPPTransaction.IsGTAPP ? PaymentMethod.GOLD_GTAPP : PaymentMethod.MONEY) : PaymentMethod.VIRTUAL_CURRENCY);
			bundle = GetBundleFromPmtProductId(moneyOrGTAPPTransaction.PMTProductID.GetValueOrDefault());
		}
		this.OnSuccessfulPurchase(bundle, paymentMethod);
		if (IsCurrentStoreLoaded())
		{
			if (source == PurchaseErrorSource.FROM_PREVIOUS_PURCHASE)
			{
				BlockStoreInterface();
				m_view.PurchaseAuth.ShowPreviousPurchaseSuccess(moneyOrGTAPPTransaction, ShouldEnablePurchaseAuthBackButton(m_currentShopType));
			}
			else
			{
				MarkTransactionCurrenciesAsDirty(paymentMethod, bundle);
				Processor.QueueJob("StoreManager.ShowCompletePurchaseSuccessWhenReady", Job_ShowCompletePurchaseSuccessWhenReady(moneyOrGTAPPTransaction));
			}
		}
	}

	private void MarkTransactionCurrenciesAsDirty(PaymentMethod paymentMethod, Network.Bundle bundle)
	{
		switch (paymentMethod)
		{
		case PaymentMethod.GOLD_GTAPP:
		case PaymentMethod.GOLD_NO_GTAPP:
			GetCurrencyCache(CurrencyType.GOLD).MarkDirty();
			break;
		case PaymentMethod.VIRTUAL_CURRENCY:
			if (bundle != null)
			{
				CurrencyType bundleVirtualCurrencyPriceType = ShopUtils.GetBundleVirtualCurrencyPriceType(bundle);
				if (bundleVirtualCurrencyPriceType != 0)
				{
					GetCurrencyCache(bundleVirtualCurrencyPriceType).MarkDirty();
				}
			}
			break;
		}
		if (bundle == null)
		{
			return;
		}
		foreach (Network.BundleItem item in bundle.Items.Where((Network.BundleItem i) => i.ItemType == ProductType.PRODUCT_TYPE_CURRENCY))
		{
			CurrencyType currencyTypeFromProto = ShopUtils.GetCurrencyTypeFromProto((PegasusShared.CurrencyType)item.ProductData);
			if (currencyTypeFromProto != 0)
			{
				GetCurrencyCache(currencyTypeFromProto).MarkDirty();
			}
		}
	}

	private IEnumerator<IAsyncJobResult> Job_ShowCompletePurchaseSuccessWhenReady(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction)
	{
		DateTime startTime = DateTime.Now;
		double elapsedSeconds = 0.0;
		bool checkCurrency = true;
		while (!IsPurchaseSuccessReady(checkCurrency))
		{
			elapsedSeconds = DateTime.Now.Subtract(startTime).TotalSeconds;
			if (checkCurrency && elapsedSeconds > CURRENCY_TRANSACTION_TIMEOUT_SECONDS)
			{
				checkCurrency = false;
			}
			yield return null;
		}
		if (m_currencyCaches.Any((KeyValuePair<CurrencyType, CurrencyCache> c) => c.Value.NeedsRefresh()))
		{
			Log.Store.PrintError("[StoreManager.ShowCompletePurchaseSuccessWhenReady] gave up on waiting for currency balance after {0} seconds", elapsedSeconds);
			if (DialogManager.Get() != null)
			{
				AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
				{
					m_text = GameStrings.Format("GLUE_STORE_FAIL_CURRENCY_BALANCE"),
					m_showAlertIcon = true,
					m_responseDisplay = AlertPopup.ResponseDisplay.OK
				};
				DialogManager.Get().ShowPopup(info);
			}
		}
		SetCanTapOutConfirmationUI(closeConfirmationUI: false);
		if (m_view.IsLoaded)
		{
			m_view.PurchaseAuth.CompletePurchaseSuccess(moneyOrGTAPPTransaction);
		}
	}

	private bool IsPurchaseSuccessReady(bool checkCurrency = true)
	{
		if (Status == TransactionStatus.READY && (Shop.Get() == null || !Shop.Get().WillAutoPurchase()))
		{
			if (checkCurrency)
			{
				return !m_currencyCaches.Any((KeyValuePair<CurrencyType, CurrencyCache> c) => c.Value.NeedsRefresh());
			}
			return true;
		}
		return false;
	}

	private void HandleFailedRiskError(PurchaseErrorSource source)
	{
		bool num = TransactionStatus.CHALLENGE_CANCELED == Status;
		Status = TransactionStatus.READY;
		if (num)
		{
			Log.Store.Print("HandleFailedRiskError for canceled transaction");
			if (m_activeMoneyOrGTAPPTransaction != null)
			{
				ConfirmActiveMoneyTransaction(m_activeMoneyOrGTAPPTransaction.ID);
			}
			UnblockStoreInterface();
		}
		else if (IsCurrentStoreLoaded() && GetCurrentStore().IsOpen())
		{
			m_view.PurchaseAuth.Hide();
			m_view.Summary.Hide();
			BlockStoreInterface();
			m_view.SendToBam.Show(m_activeMoneyOrGTAPPTransaction, StoreSendToBAM.BAMReason.NEED_PASSWORD_RESET, string.Empty, source == PurchaseErrorSource.FROM_PREVIOUS_PURCHASE);
		}
	}

	private void HandleSendToBAMError(PurchaseErrorSource source, StoreSendToBAM.BAMReason reason, string errorCode)
	{
		Status = TransactionStatus.READY;
		if (IsCurrentStoreLoaded() && GetCurrentStore().IsOpen())
		{
			m_view.PurchaseAuth.Hide();
			m_view.Summary.Hide();
			BlockStoreInterface();
			m_view.SendToBam.Show(m_activeMoneyOrGTAPPTransaction, reason, errorCode, source == PurchaseErrorSource.FROM_PREVIOUS_PURCHASE);
		}
	}

	private void CompletePurchaseFailure(PurchaseErrorSource source, MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, string failDetails, string thirdPartyID, Network.PurchaseErrorInfo.ErrorType error)
	{
		if (!IsCurrentStoreLoaded())
		{
			return;
		}
		switch (source)
		{
		case PurchaseErrorSource.FROM_PURCHASE_METHOD_RESPONSE:
			if (!m_view.SendToBam.IsShown)
			{
				BlockStoreInterface();
				m_view.PurchaseAuth.ShowPreviousPurchaseFailure(moneyOrGTAPPTransaction, failDetails, ShouldEnablePurchaseAuthBackButton(m_currentShopType), error);
			}
			break;
		case PurchaseErrorSource.FROM_PREVIOUS_PURCHASE:
			BlockStoreInterface();
			m_view.PurchaseAuth.ShowPreviousPurchaseFailure(moneyOrGTAPPTransaction, failDetails, ShouldEnablePurchaseAuthBackButton(m_currentShopType), error);
			break;
		default:
			if (!m_view.PurchaseAuth.CompletePurchaseFailure(moneyOrGTAPPTransaction, failDetails, error))
			{
				Log.Store.PrintWarning("StoreManager.CompletePurchaseFailure(): purchased failed (" + failDetails + ") but the store authorization window has been closed.");
				UnblockStoreInterface();
			}
			break;
		}
	}

	private void HandlePurchaseError(PurchaseErrorSource source, Network.PurchaseErrorInfo.ErrorType purchaseErrorType, string purchaseErrorCode, string thirdPartyID, bool isGTAPP)
	{
		if (IsConclusiveState(purchaseErrorType) && m_activeMoneyOrGTAPPTransaction != null && m_transactionIDsConclusivelyHandled.Contains(m_activeMoneyOrGTAPPTransaction.ID))
		{
			Log.Store.Print("HandlePurchaseError already handled purchase error for conclusive state on transaction (Transaction: {0}, current purchaseErrorType = {1})", m_activeMoneyOrGTAPPTransaction, purchaseErrorType);
			return;
		}
		Log.Store.Print($"HandlePurchaseError source={source} purchaseErrorType={purchaseErrorType} purchaseErrorCode={purchaseErrorCode} thirdPartyID={thirdPartyID}");
		string failDetails = "";
		switch (purchaseErrorType)
		{
		case Network.PurchaseErrorInfo.ErrorType.UNKNOWN:
			Log.Store.PrintWarning("StoreManager.HandlePurchaseError: purchase error is UNKNOWN, taking no action on this purchase");
			return;
		case Network.PurchaseErrorInfo.ErrorType.SUCCESS:
			if (source == PurchaseErrorSource.FROM_PURCHASE_METHOD_RESPONSE)
			{
				Log.Store.PrintWarning("StoreManager.HandlePurchaseError: received SUCCESS from payment method purchase error.");
			}
			else
			{
				HandlePurchaseSuccess(source, m_activeMoneyOrGTAPPTransaction, thirdPartyID, null);
			}
			return;
		case Network.PurchaseErrorInfo.ErrorType.STILL_IN_PROGRESS:
			switch (source)
			{
			case PurchaseErrorSource.FROM_PURCHASE_METHOD_RESPONSE:
				Log.Store.PrintWarning("StoreManager.HandlePurchaseError: received STILL_IN_PROGRESS from payment method purchase error.");
				break;
			default:
				Status = ((!isGTAPP) ? TransactionStatus.IN_PROGRESS_MONEY : TransactionStatus.IN_PROGRESS_GOLD_GTAPP);
				break;
			case PurchaseErrorSource.FROM_PREVIOUS_PURCHASE:
				break;
			}
			return;
		case Network.PurchaseErrorInfo.ErrorType.INVALID_BNET:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_BNET_ID");
			break;
		case Network.PurchaseErrorInfo.ErrorType.SERVICE_NA:
			if (source != PurchaseErrorSource.FROM_PREVIOUS_PURCHASE)
			{
				if (Status != 0)
				{
					BattlePayAvailable = false;
				}
				Status = TransactionStatus.UNKNOWN;
			}
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_NO_BATTLEPAY");
			CompletePurchaseFailure(source, m_activeMoneyOrGTAPPTransaction, failDetails, thirdPartyID, purchaseErrorType);
			return;
		case Network.PurchaseErrorInfo.ErrorType.PURCHASE_IN_PROGRESS:
			if (source != PurchaseErrorSource.FROM_PREVIOUS_PURCHASE)
			{
				Status = ((!isGTAPP) ? TransactionStatus.IN_PROGRESS_MONEY : TransactionStatus.IN_PROGRESS_GOLD_GTAPP);
			}
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_IN_PROGRESS");
			CompletePurchaseFailure(source, m_activeMoneyOrGTAPPTransaction, failDetails, thirdPartyID, purchaseErrorType);
			return;
		case Network.PurchaseErrorInfo.ErrorType.DATABASE:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_DATABASE");
			break;
		case Network.PurchaseErrorInfo.ErrorType.INVALID_QUANTITY:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_QUANTITY");
			break;
		case Network.PurchaseErrorInfo.ErrorType.DUPLICATE_LICENSE:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_LICENSE");
			break;
		case Network.PurchaseErrorInfo.ErrorType.REQUEST_NOT_SENT:
			if (source != PurchaseErrorSource.FROM_PREVIOUS_PURCHASE && Status != 0)
			{
				BattlePayAvailable = false;
			}
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_NO_BATTLEPAY");
			break;
		case Network.PurchaseErrorInfo.ErrorType.NO_ACTIVE_BPAY:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_NO_ACTIVE_BPAY");
			break;
		case Network.PurchaseErrorInfo.ErrorType.FAILED_RISK:
			HandleFailedRiskError(source);
			return;
		case Network.PurchaseErrorInfo.ErrorType.CANCELED:
			if (source != PurchaseErrorSource.FROM_PREVIOUS_PURCHASE)
			{
				Status = TransactionStatus.READY;
			}
			return;
		case Network.PurchaseErrorInfo.ErrorType.WAIT_MOP:
			Log.Store.Print("StoreManager.HandlePurchaseError: Status is WAIT_MOP.. this probably shouldn't be happening.");
			if (source != PurchaseErrorSource.FROM_PREVIOUS_PURCHASE)
			{
				if (Status == TransactionStatus.UNKNOWN)
				{
					Log.Store.Print($"StoreManager.HandlePurchaseError: Status is WAIT_MOP, previous Status was UNKNOWN, source = {source}");
				}
				else
				{
					Status = TransactionStatus.WAIT_METHOD_OF_PAYMENT;
				}
			}
			return;
		case Network.PurchaseErrorInfo.ErrorType.WAIT_CONFIRM:
			if (source != PurchaseErrorSource.FROM_PREVIOUS_PURCHASE && Status == TransactionStatus.UNKNOWN)
			{
				Log.Store.Print($"StoreManager.HandlePurchaseError: Status is WAIT_CONFIRM, previous Status was UNKNOWN, source = {source}. Going to try to cancel the purchase.");
				CancelBlizzardPurchase();
			}
			return;
		case Network.PurchaseErrorInfo.ErrorType.WAIT_RISK:
			if (source != PurchaseErrorSource.FROM_PREVIOUS_PURCHASE)
			{
				Log.Store.Print("StoreManager.HandlePurchaseError: Waiting for client to respond to Risk challenge");
				if (Status == TransactionStatus.UNKNOWN)
				{
					Log.Store.Print($"StoreManager.HandlePurchaseError: Status is WAIT_RISK, previous Status was UNKNOWN, source = {source}");
				}
				else if (TransactionStatus.CHALLENGE_SUBMITTED == Status || TransactionStatus.CHALLENGE_CANCELED == Status)
				{
					Log.Store.Print($"StoreManager.HandlePurchaseError: Status = {Status}; ignoring WAIT_RISK purchase error info");
				}
				else
				{
					Status = TransactionStatus.WAIT_RISK;
				}
			}
			return;
		case Network.PurchaseErrorInfo.ErrorType.PRODUCT_NA:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_PRODUCT_NA");
			break;
		case Network.PurchaseErrorInfo.ErrorType.PRODUCT_EVENT_HAS_ENDED:
			failDetails = ((m_activeMoneyOrGTAPPTransaction == null || !IsProductPrePurchase(GetBundleFromPmtProductId(m_activeMoneyOrGTAPPTransaction.PMTProductID.GetValueOrDefault()))) ? GameStrings.Get("GLUE_STORE_PRODUCT_EVENT_HAS_ENDED") : GameStrings.Get("GLUE_STORE_PRE_PURCHASE_HAS_ENDED"));
			break;
		case Network.PurchaseErrorInfo.ErrorType.RISK_TIMEOUT:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_CHALLENGE_TIMEOUT");
			break;
		case Network.PurchaseErrorInfo.ErrorType.PRODUCT_ALREADY_OWNED:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_PRODUCT_ALREADY_OWNED");
			break;
		case Network.PurchaseErrorInfo.ErrorType.WAIT_THIRD_PARTY_RECEIPT:
			Log.Store.PrintWarning("StoreManager.HandlePurchaseError: Received WAIT_THIRD_PARTY_RECEIPT response, even though legacy third party purchasing is removed.");
			return;
		case Network.PurchaseErrorInfo.ErrorType.BP_GENERIC_FAIL:
		case Network.PurchaseErrorInfo.ErrorType.BP_RISK_ERROR:
		case Network.PurchaseErrorInfo.ErrorType.BP_PAYMENT_AUTH:
		case Network.PurchaseErrorInfo.ErrorType.BP_PROVIDER_DENIED:
		case Network.PurchaseErrorInfo.ErrorType.E_BP_GENERIC_FAIL_RETRY_CONTACT_CS_IF_PERSISTS:
			if (!isGTAPP)
			{
				StoreSendToBAM.BAMReason reason = StoreSendToBAM.BAMReason.GENERIC_PAYMENT_FAIL;
				if (purchaseErrorType == Network.PurchaseErrorInfo.ErrorType.E_BP_GENERIC_FAIL_RETRY_CONTACT_CS_IF_PERSISTS)
				{
					reason = StoreSendToBAM.BAMReason.GENERIC_PURCHASE_FAIL_RETRY_CONTACT_CS_IF_PERSISTS;
				}
				HandleSendToBAMError(source, reason, purchaseErrorCode);
				if (HasExternalStore)
				{
					CompletePurchaseFailure(source, m_activeMoneyOrGTAPPTransaction, failDetails, thirdPartyID, purchaseErrorType);
				}
				return;
			}
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_GOLD_GENERIC");
			break;
		case Network.PurchaseErrorInfo.ErrorType.BP_INVALID_CC_EXPIRY:
			if (!isGTAPP)
			{
				HandleSendToBAMError(source, StoreSendToBAM.BAMReason.CREDIT_CARD_EXPIRED, string.Empty);
				return;
			}
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_GOLD_GENERIC");
			break;
		case Network.PurchaseErrorInfo.ErrorType.BP_NO_VALID_PAYMENT:
			if (source == PurchaseErrorSource.FROM_PURCHASE_METHOD_RESPONSE)
			{
				Log.Store.PrintWarning("StoreManager.HandlePurchaseError: received BP_NO_VALID_PAYMENT from payment method purchase error.");
				break;
			}
			if (!isGTAPP)
			{
				HandleSendToBAMError(source, StoreSendToBAM.BAMReason.NO_VALID_PAYMENT_METHOD, string.Empty);
				return;
			}
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_GOLD_GENERIC");
			break;
		case Network.PurchaseErrorInfo.ErrorType.BP_PURCHASE_BAN:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_PURCHASE_BAN");
			break;
		case Network.PurchaseErrorInfo.ErrorType.BP_SPENDING_LIMIT:
			failDetails = (isGTAPP ? GameStrings.Get("GLUE_STORE_FAIL_GOLD_GENERIC") : GameStrings.Get("GLUE_STORE_FAIL_SPENDING_LIMIT"));
			break;
		case Network.PurchaseErrorInfo.ErrorType.BP_PARENTAL_CONTROL:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_PARENTAL_CONTROL");
			break;
		case Network.PurchaseErrorInfo.ErrorType.BP_THROTTLED:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_THROTTLED");
			break;
		case Network.PurchaseErrorInfo.ErrorType.BP_THIRD_PARTY_BAD_RECEIPT:
		case Network.PurchaseErrorInfo.ErrorType.BP_THIRD_PARTY_RECEIPT_USED:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_THIRD_PARTY_BAD_RECEIPT");
			break;
		case Network.PurchaseErrorInfo.ErrorType.BP_PRODUCT_UNIQUENESS_VIOLATED:
			HandleSendToBAMError(source, StoreSendToBAM.BAMReason.PRODUCT_UNIQUENESS_VIOLATED, string.Empty);
			return;
		case Network.PurchaseErrorInfo.ErrorType.BP_REGION_IS_DOWN:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_REGION_IS_DOWN");
			break;
		case Network.PurchaseErrorInfo.ErrorType.E_BP_CHALLENGE_ID_FAILED_VERIFICATION:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_CHALLENGE_ID_FAILED_VERIFICATION");
			break;
		default:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_GENERAL");
			break;
		}
		if (source != PurchaseErrorSource.FROM_PREVIOUS_PURCHASE)
		{
			Status = TransactionStatus.READY;
		}
		CompletePurchaseFailure(source, m_activeMoneyOrGTAPPTransaction, failDetails, thirdPartyID, purchaseErrorType);
	}

	private void SetActiveMoneyOrGTAPPTransaction(long id, long? pmtProductID, BattlePayProvider? provider, bool isGTAPP, bool tryToResolvePreviousTransactionNotices)
	{
		MoneyOrGTAPPTransaction moneyOrGTAPPTransaction = new MoneyOrGTAPPTransaction(id, pmtProductID, provider, isGTAPP);
		bool flag = true;
		if (m_activeMoneyOrGTAPPTransaction != null)
		{
			if (moneyOrGTAPPTransaction.Equals(m_activeMoneyOrGTAPPTransaction))
			{
				flag = !m_activeMoneyOrGTAPPTransaction.Provider.HasValue && provider.HasValue;
			}
			else if (UNKNOWN_TRANSACTION_ID != m_activeMoneyOrGTAPPTransaction.ID)
			{
				Log.Store.PrintWarning(string.Format("StoreManager.SetActiveMoneyOrGTAPPTransaction(id={0}, pmtProductId={1}, isGTAPP={2}, provider={3}) does not match active money or GTAPP transaction '{4}'", id, pmtProductID, isGTAPP, provider.HasValue ? provider.Value.ToString() : "UNKNOWN", m_activeMoneyOrGTAPPTransaction));
			}
		}
		if (flag)
		{
			Log.Store.Print($"SetActiveMoneyOrGTAPPTransaction() {moneyOrGTAPPTransaction}");
			m_activeMoneyOrGTAPPTransaction = moneyOrGTAPPTransaction;
		}
		if (!m_firstMoneyOrGTAPPTransactionSet)
		{
			m_firstMoneyOrGTAPPTransactionSet = true;
			if (tryToResolvePreviousTransactionNotices)
			{
				ResolveFirstMoneyOrGTAPPTransactionIfPossible();
			}
		}
	}

	private void ResolveFirstMoneyOrGTAPPTransactionIfPossible()
	{
		if (m_firstMoneyOrGTAPPTransactionSet && FirstNoticesProcessed && m_activeMoneyOrGTAPPTransaction != null && m_outstandingPurchaseNotices.Find((NetCache.ProfileNoticePurchase obj) => obj.OriginData == m_activeMoneyOrGTAPPTransaction.ID) == null)
		{
			Log.Store.Print($"StoreManager.ResolveFirstMoneyTransactionIfPossible(): no outstanding notices for transaction {m_activeMoneyOrGTAPPTransaction}; setting m_activeMoneyOrGTAPPTransaction = null");
			m_activeMoneyOrGTAPPTransaction = null;
		}
	}

	private void ConfirmActiveMoneyTransaction(long id)
	{
		if (m_activeMoneyOrGTAPPTransaction == null || (m_activeMoneyOrGTAPPTransaction.ID != UNKNOWN_TRANSACTION_ID && m_activeMoneyOrGTAPPTransaction.ID != id))
		{
			Log.Store.PrintWarning($"StoreManager.ConfirmActiveMoneyTransaction(id={id}) does not match active money transaction '{m_activeMoneyOrGTAPPTransaction}'");
		}
		Log.Store.Print($"ConfirmActiveMoneyTransaction() {id}");
		List<NetCache.ProfileNoticePurchase> list = m_outstandingPurchaseNotices.FindAll(Predicate);
		m_outstandingPurchaseNotices.RemoveAll(Predicate);
		foreach (NetCache.ProfileNoticePurchase item in list)
		{
			Network.Get().AckNotice(item.NoticeID);
		}
		m_confirmedTransactionIDs.Add(id);
		m_activeMoneyOrGTAPPTransaction = null;
		bool Predicate(NetCache.ProfileNoticePurchase obj)
		{
			return obj.OriginData == id;
		}
	}

	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		List<long> list = new List<long>();
		foreach (NetCache.ProfileNotice newNotice in newNotices)
		{
			if (newNotice.Type == NetCache.ProfileNotice.NoticeType.PURCHASE)
			{
				if (newNotice.Origin == NetCache.ProfileNotice.NoticeOrigin.PURCHASE_CANCELED)
				{
					Log.Store.Print($"StoreManager.OnNewNotices() ack'ing purchase canceled notice for bpay ID {newNotice.OriginData}");
					list.Add(newNotice.NoticeID);
				}
				else if (m_confirmedTransactionIDs.Contains(newNotice.OriginData))
				{
					Log.Store.Print($"StoreManager.OnNewNotices() ack'ing purchase notice for already confirmed bpay ID {newNotice.OriginData}");
					list.Add(newNotice.NoticeID);
				}
				else
				{
					NetCache.ProfileNoticePurchase item = newNotice as NetCache.ProfileNoticePurchase;
					Log.Store.Print($"StoreManager.OnNewNotices() adding outstanding purchase notice for bpay ID {newNotice.OriginData}");
					m_outstandingPurchaseNotices.Add(item);
				}
			}
		}
		Network network = Network.Get();
		foreach (long item2 in list)
		{
			network.AckNotice(item2);
		}
		if (!FirstNoticesProcessed)
		{
			FirstNoticesProcessed = true;
			if (Status == TransactionStatus.READY)
			{
				ResolveFirstMoneyOrGTAPPTransactionIfPossible();
			}
		}
	}

	private void OnNetCacheFeaturesReady()
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		FeaturesReady = netObject != null;
	}

	private void OnPurchaseCanceledResponse()
	{
		Network.PurchaseCanceledResponse purchaseCanceledResponse = Network.Get().GetPurchaseCanceledResponse();
		switch (purchaseCanceledResponse.Result)
		{
		case Network.PurchaseCanceledResponse.CancelResult.NOT_ALLOWED:
		{
			Log.Store.PrintWarning("StoreManager.OnPurchaseCanceledResponse(): cancel purchase is not allowed right now.");
			bool flag = Currency.IsGTAPP(purchaseCanceledResponse.CurrencyCode);
			SetActiveMoneyOrGTAPPTransaction(purchaseCanceledResponse.TransactionID, purchaseCanceledResponse.PMTProductID, MoneyOrGTAPPTransaction.UNKNOWN_PROVIDER, flag, tryToResolvePreviousTransactionNotices: true);
			Status = ((!flag) ? TransactionStatus.IN_PROGRESS_MONEY : TransactionStatus.IN_PROGRESS_GOLD_GTAPP);
			if (m_previousStatusBeforeAutoCancel != 0)
			{
				Status = m_previousStatusBeforeAutoCancel;
				m_previousStatusBeforeAutoCancel = TransactionStatus.UNKNOWN;
			}
			break;
		}
		case Network.PurchaseCanceledResponse.CancelResult.NOTHING_TO_CANCEL:
			m_previousStatusBeforeAutoCancel = TransactionStatus.UNKNOWN;
			if (m_activeMoneyOrGTAPPTransaction != null && UNKNOWN_TRANSACTION_ID != m_activeMoneyOrGTAPPTransaction.ID)
			{
				ConfirmActiveMoneyTransaction(m_activeMoneyOrGTAPPTransaction.ID);
			}
			Status = TransactionStatus.READY;
			break;
		case Network.PurchaseCanceledResponse.CancelResult.SUCCESS:
			Log.Store.Print("StoreManager.OnPurchaseCanceledResponse(): purchase successfully canceled.");
			ConfirmActiveMoneyTransaction(purchaseCanceledResponse.TransactionID);
			Status = TransactionStatus.READY;
			m_previousStatusBeforeAutoCancel = TransactionStatus.UNKNOWN;
			break;
		}
	}

	private bool IsConclusiveState(Network.PurchaseErrorInfo.ErrorType errorType)
	{
		switch (errorType)
		{
		case Network.PurchaseErrorInfo.ErrorType.UNKNOWN:
		case Network.PurchaseErrorInfo.ErrorType.STILL_IN_PROGRESS:
		case Network.PurchaseErrorInfo.ErrorType.WAIT_MOP:
		case Network.PurchaseErrorInfo.ErrorType.WAIT_CONFIRM:
		case Network.PurchaseErrorInfo.ErrorType.WAIT_RISK:
		case Network.PurchaseErrorInfo.ErrorType.WAIT_THIRD_PARTY_RECEIPT:
			return false;
		default:
			return true;
		}
	}

	private void OnBattlePayStatusResponse()
	{
		Network.BattlePayStatus battlePayStatusResponse = Network.Get().GetBattlePayStatusResponse();
		if (battlePayStatusResponse.BattlePayAvailable != BattlePayAvailable)
		{
			BattlePayAvailable = battlePayStatusResponse.BattlePayAvailable;
			Log.Store.Print("Store server status is now {0}", BattlePayAvailable ? "available" : "unavailable");
		}
		switch (battlePayStatusResponse.State)
		{
		case Network.BattlePayStatus.PurchaseState.READY:
			Status = TransactionStatus.READY;
			Log.Store.Print("Store PurchaseState is READY.");
			break;
		case Network.BattlePayStatus.PurchaseState.CHECK_RESULTS:
		{
			Log.Store.Print("Store PurchaseState is CHECK_RESULTS.");
			bool isGTAPP = Currency.IsGTAPP(battlePayStatusResponse.CurrencyCode);
			bool tryToResolvePreviousTransactionNotices = IsConclusiveState(battlePayStatusResponse.PurchaseError.Error);
			SetActiveMoneyOrGTAPPTransaction(battlePayStatusResponse.TransactionID, battlePayStatusResponse.PMTProductID, battlePayStatusResponse.Provider, isGTAPP, tryToResolvePreviousTransactionNotices);
			HandlePurchaseError(PurchaseErrorSource.FROM_STATUS_OR_PURCHASE_RESPONSE, battlePayStatusResponse.PurchaseError.Error, battlePayStatusResponse.PurchaseError.ErrorCode, battlePayStatusResponse.ThirdPartyID, isGTAPP);
			break;
		}
		case Network.BattlePayStatus.PurchaseState.ERROR:
			Log.Store.PrintError("Store PurchaseState is ERROR.");
			break;
		default:
			Log.Store.PrintError("Store PurchaseState is unknown value {0}.", battlePayStatusResponse.State);
			break;
		}
	}

	private static string GetExternalStoreProductId(Network.Bundle bundle)
	{
		return null;
	}

	private static bool ValidateExternalStoreProductIDs(Network.Bundle bundle)
	{
		if (!bundle.Cost.HasValue)
		{
			return true;
		}
		bool flag = true;
		bool flag2 = true;
		bool flag3 = true;
		if (bundle.ExclusiveProviders != null && bundle.ExclusiveProviders.Count > 0)
		{
			if (!bundle.ExclusiveProviders.Contains(StoreProvider))
			{
				return false;
			}
			flag = bundle.ExclusiveProviders.Contains(BattlePayProvider.BP_PROVIDER_APPLE);
			flag2 = bundle.ExclusiveProviders.Contains(BattlePayProvider.BP_PROVIDER_GOOGLE_PLAY);
			flag3 = bundle.ExclusiveProviders.Contains(BattlePayProvider.BP_PROVIDER_AMAZON);
		}
		bool flag4 = !string.IsNullOrEmpty(bundle.AppleID);
		bool flag5 = !string.IsNullOrEmpty(bundle.GooglePlayID);
		bool flag6 = !string.IsNullOrEmpty(bundle.AmazonID);
		string text = ((bundle.DisplayName != null) ? bundle.DisplayName.GetString() : "");
		if (HasExternalStore)
		{
			if (flag && !flag4)
			{
				Log.Store.PrintWarning("Product missing Apple Store Product ID: PMT ID = {0}, Name = {1}", bundle.PMTProductID, text);
			}
			if (flag2 && !flag5)
			{
				Log.Store.PrintWarning("Product missing Google Play Store Product ID: PMT ID = {0}, Name = {1}", bundle.PMTProductID, text);
			}
			if (flag3 && !flag6)
			{
				Log.Store.PrintWarning("Product missing Amazon Store Product ID: PMT ID = {0}, Name = {1}", bundle.PMTProductID, text);
			}
		}
		bool flag7 = false;
		if (HasExternalStore && string.IsNullOrEmpty(GetExternalStoreProductId(bundle)))
		{
			Log.Store.PrintError("Product cannot have real money cost due to missing external store product ID. PMT ID = {0}. Name = {1}", bundle.PMTProductID, text);
			flag7 = true;
		}
		if (flag7)
		{
			if (ShopUtils.BundleHasNonRealMoneyPrice(bundle))
			{
				bundle.Cost = null;
				bundle.CostDisplay = null;
				return true;
			}
			return false;
		}
		return true;
	}

	private void OnBattlePayConfigResponse()
	{
		Network.BattlePayConfig battlePayConfigResponse = Network.Get().GetBattlePayConfigResponse();
		if (!battlePayConfigResponse.Available)
		{
			Log.Store.PrintWarning("Server responds that store is unavailable.");
			BattlePayAvailable = false;
			return;
		}
		HearthstoneCheckout.OneStoreKey = battlePayConfigResponse.CheckoutKrOnestoreKey;
		Log.Store.Print("Server responds that store is available.");
		BattlePayAvailable = true;
		m_currency = battlePayConfigResponse.Currency;
		m_secsBeforeAutoCancel = battlePayConfigResponse.SecondsBeforeAutoCancel;
		bool hasExternalStore = HasExternalStore;
		m_bundles.Clear();
		foreach (Network.Bundle bundle in battlePayConfigResponse.Bundles)
		{
			if (ValidateExternalStoreProductIDs(bundle) && bundle.PMTProductID.HasValue)
			{
				m_bundles.Add(bundle.PMTProductID.Value, bundle);
			}
		}
		m_goldCostBooster.Clear();
		foreach (Network.GoldCostBooster goldCostBooster in battlePayConfigResponse.GoldCostBoosters)
		{
			m_goldCostBooster.Add(goldCostBooster.ID, goldCostBooster);
		}
		m_goldCostArena = battlePayConfigResponse.GoldCostArena;
		SetPersonalizedShopPageAndRefreshCatalog(battlePayConfigResponse.PersonalizedShopPageID);
		m_sales.Clear();
		foreach (Network.ShopSale sale in battlePayConfigResponse.SaleList)
		{
			m_sales[sale.SaleId] = sale;
		}
		m_ignoreProductTiming = battlePayConfigResponse.IgnoreProductTiming;
		if (hasExternalStore)
		{
			Log.Store.Print("Validating mobile products. Store will remain closed until completed.");
		}
		else
		{
			ConfigLoaded = true;
		}
		Log.Store.Print("StoreManager.OnBattlePayConfigResponse: Queueing ConfigureCheckout Job.");
		Processor.QueueJob("StoreManager.ConfigureCheckoutFromBattlePayConfig", Job_ConfigureCheckoutFromBattlePayConfig(battlePayConfigResponse.CommerceClientID, (m_currency != null) ? m_currency.Code : ""), HearthstoneServices.CreateServiceSoftDependency(typeof(HearthstoneCheckout)));
	}

	private IEnumerator<IAsyncJobResult> Job_ConfigureCheckoutFromBattlePayConfig(string clientID, string currencyCode)
	{
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out var service))
		{
			List<string> list = new List<string>(m_bundles.Count);
			foreach (Network.Bundle value in m_bundles.Values)
			{
				if (value.PMTProductID.HasValue)
				{
					list.Add(value.PMTProductID.ToString());
				}
			}
			service.SetClientID(clientID);
			service.SetProductCatalog(list.ToArray());
			service.SetCurrencyCode(currencyCode);
			Log.Store.Print("StoreManager.ConfigureCheckoutFromBattlePayConfig: Queueing FireStatusChangeEventForHearthstoneCheckout Job.");
			Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("StoreManager.FireStatusChangeEventForHearthstoneCheckout", OnCheckoutInitializationComplete, new WaitForCheckoutState(HearthstoneCheckout.State.Idle, HearthstoneCheckout.State.Unavailable)));
		}
		else
		{
			Log.Store.Print("StoreManager.ConfigureCheckoutFromBattlePayConfig: HearthstoneCheckout is unavailable.");
			OnCheckoutInitializationComplete();
		}
		yield break;
	}

	private void OnCheckoutInitializationComplete()
	{
		Log.Store.Print("StoreManager.OnCheckoutInitializationComplete: OnCheckoutInitializationComplete called.");
		FireStatusChangedEventIfNeeded();
	}

	private void HandleZeroCostLicensePurchaseMethod(Network.PurchaseMethod method)
	{
		if (Network.PurchaseErrorInfo.ErrorType.STILL_IN_PROGRESS != method.PurchaseError.Error)
		{
			Log.Store.PrintWarning($"StoreManager.HandleZeroCostLicensePurchaseMethod() FAILED error={method.PurchaseError.Error}");
			Status = TransactionStatus.READY;
		}
		else
		{
			Log.Store.Print("StoreManager.HandleZeroCostLicensePurchaseMethod succeeded, refreshing achieves");
		}
	}

	private void OnPurchaseMethod()
	{
		Network.PurchaseMethod purchaseMethodResponse = Network.Get().GetPurchaseMethodResponse();
		if (purchaseMethodResponse.IsZeroCostLicense)
		{
			HandleZeroCostLicensePurchaseMethod(purchaseMethodResponse);
			return;
		}
		if (!string.IsNullOrEmpty(purchaseMethodResponse.ChallengeID) && !string.IsNullOrEmpty(purchaseMethodResponse.ChallengeURL))
		{
			m_challengePurchaseMethod = purchaseMethodResponse;
		}
		else
		{
			m_challengePurchaseMethod = null;
		}
		bool flag = Currency.IsGTAPP(purchaseMethodResponse.CurrencyCode);
		SetActiveMoneyOrGTAPPTransaction(purchaseMethodResponse.TransactionID, purchaseMethodResponse.PMTProductID, BattlePayProvider.BP_PROVIDER_BLIZZARD, flag, tryToResolvePreviousTransactionNotices: false);
		if (purchaseMethodResponse.PurchaseError != null)
		{
			HandlePurchaseError(PurchaseErrorSource.FROM_PURCHASE_METHOD_RESPONSE, purchaseMethodResponse.PurchaseError.Error, purchaseMethodResponse.PurchaseError.ErrorCode, string.Empty, flag);
			return;
		}
		BlockStoreInterface();
		if (flag)
		{
			OnSummaryConfirm(purchaseMethodResponse.Quantity, null);
			return;
		}
		string paymentMethodName = ((!purchaseMethodResponse.UseEBalance) ? purchaseMethodResponse.WalletName : GameStrings.Get("GLUE_STORE_BNET_BALANCE"));
		IStore currentStore = GetCurrentStore();
		if (currentStore == null || !currentStore.IsOpen())
		{
			AutoCancelPurchaseIfPossible();
			return;
		}
		m_view.PurchaseAuth.Hide();
		Status = TransactionStatus.WAIT_CONFIRM;
		m_view.Summary.Show(purchaseMethodResponse.PMTProductID ?? 0, purchaseMethodResponse.Quantity, paymentMethodName);
	}

	private void OnPurchaseResponse()
	{
		Network.PurchaseResponse purchaseResponse = Network.Get().GetPurchaseResponse();
		bool isGTAPP = Currency.IsGTAPP(purchaseResponse.CurrencyCode);
		SetActiveMoneyOrGTAPPTransaction(purchaseResponse.TransactionID, purchaseResponse.PMTProductID, MoneyOrGTAPPTransaction.UNKNOWN_PROVIDER, isGTAPP, tryToResolvePreviousTransactionNotices: false);
		HandlePurchaseError(PurchaseErrorSource.FROM_STATUS_OR_PURCHASE_RESPONSE, purchaseResponse.PurchaseError.Error, purchaseResponse.PurchaseError.ErrorCode, purchaseResponse.ThirdPartyID, isGTAPP);
	}

	private void OnPurchaseViaGoldResponse()
	{
		Network.PurchaseViaGoldResponse purchaseWithGoldResponse = Network.Get().GetPurchaseWithGoldResponse();
		string text = "";
		switch (purchaseWithGoldResponse.Error)
		{
		case Network.PurchaseViaGoldResponse.ErrorType.SUCCESS:
			HandlePurchaseSuccess(null, null, string.Empty, null);
			return;
		case Network.PurchaseViaGoldResponse.ErrorType.INSUFFICIENT_GOLD:
			text = GameStrings.Get("GLUE_STORE_FAIL_NOT_ENOUGH_GOLD");
			break;
		case Network.PurchaseViaGoldResponse.ErrorType.PRODUCT_NA:
			text = GameStrings.Get("GLUE_STORE_FAIL_PRODUCT_NA");
			break;
		case Network.PurchaseViaGoldResponse.ErrorType.FEATURE_NA:
			text = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC");
			break;
		case Network.PurchaseViaGoldResponse.ErrorType.INVALID_QUANTITY:
			text = GameStrings.Get("GLUE_STORE_FAIL_QUANTITY");
			break;
		default:
			text = GameStrings.Get("GLUE_STORE_FAIL_GENERAL");
			break;
		}
		Status = TransactionStatus.READY;
		m_view.PurchaseAuth.CompletePurchaseFailure(null, text, Network.PurchaseErrorInfo.ErrorType.BP_GENERIC_FAIL);
	}

	private void OnThirdPartyPurchaseStatusResponse()
	{
		Log.Store.PrintWarning("[StoreManager.OnThirdPartyPurchaseStatusResponse] Received OnThirdPartyPurchaseStatusResponse packet.  Legacy third party purchasing has been removed.");
	}

	private void StoreViewReady()
	{
		if (m_waitingToShowStore && IsCurrentStoreLoaded())
		{
			ShowStore();
		}
	}

	private void OnGeneralStoreLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		GeneralStore generalStore = OnStoreLoaded<GeneralStore>(go, ShopType.GENERAL_STORE);
		if (generalStore != null)
		{
			SetupLoadedStore(generalStore);
		}
	}

	private void OnArenaStoreLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		ArenaStore arenaStore = OnStoreLoaded<ArenaStore>(go, ShopType.ARENA_STORE);
		if (arenaStore != null)
		{
			SetupLoadedStore(arenaStore);
		}
	}

	private void OnBrawlStoreLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		TavernBrawlStore tavernBrawlStore = OnStoreLoaded<TavernBrawlStore>(go, ShopType.TAVERN_BRAWL_STORE);
		if (tavernBrawlStore != null)
		{
			SetupLoadedStore(tavernBrawlStore);
		}
	}

	private void OnAdventureStoreLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		AdventureStore adventureStore = OnStoreLoaded<AdventureStore>(go, ShopType.ADVENTURE_STORE);
		if (adventureStore != null)
		{
			SetupLoadedStore(adventureStore);
		}
	}

	private void OnAdventureWingStoreLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		AdventureStore adventureStore = OnStoreLoaded<AdventureStore>(go, ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET);
		if (adventureStore != null)
		{
			SetupLoadedStore(adventureStore);
		}
	}

	private void OnAdventureFullStoreLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		AdventureStore adventureStore = OnStoreLoaded<AdventureStore>(go, ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET);
		if (adventureStore != null)
		{
			SetupLoadedStore(adventureStore);
		}
	}

	private T OnStoreLoaded<T>(GameObject go, ShopType shopType) where T : Store
	{
		if (go == null)
		{
			Debug.LogError($"StoreManager.OnStoreLoaded<{typeof(T)}>(): go is null!");
			return null;
		}
		T val = go.GetComponent<T>();
		if ((UnityEngine.Object)val == (UnityEngine.Object)null)
		{
			val = go.GetComponentInChildren<T>();
		}
		if ((UnityEngine.Object)val == (UnityEngine.Object)null)
		{
			Debug.LogError($"StoreManager.OnStoreLoaded<{typeof(T)}>(): go has no {typeof(T)} component!");
			return null;
		}
		m_stores[shopType] = val;
		return val;
	}

	private void SendShopPurchaseEventTelemetry(bool isComplete)
	{
		if (m_pendingProductPurchaseArgs == null)
		{
			Log.Store.PrintWarning("No active transaction in progress");
			return;
		}
		Blizzard.Telemetry.WTCG.Client.Product product = new Blizzard.Telemetry.WTCG.Client.Product();
		if (!ShopUtils.TryDecomposeBuyProductEventArgs(m_pendingProductPurchaseArgs, out var pmtProductId, out var currencyCode, out var totalPrice, out var quantity, out var productItemType, out var productItemId))
		{
			Log.Store.PrintError("Failed to decompose pending product purchase args for telemetry.");
			return;
		}
		BattlePayProvider? battlePayProvider = ActiveTransactionProvider();
		string storefront = (battlePayProvider.HasValue ? battlePayProvider.Value.ToString().ToLowerInvariant() : "");
		product.ProductId = pmtProductId;
		product.HsProductType = productItemType;
		product.HsProductId = productItemId;
		TelemetryManager.Client().SendShopPurchaseEvent(product, quantity, currencyCode, totalPrice, isGift: false, storefront, isComplete, m_currentShopType.ToString());
	}

	public void RegisterAmazingNewShop(Shop amazingNewShop)
	{
		SetupLoadedStore(amazingNewShop);
	}

	private void SetupLoadedStore(IStore store)
	{
		if (store == null)
		{
			return;
		}
		store.OnProductPurchaseAttempt += delegate(BuyProductEventArgs args)
		{
			if (args == null)
			{
				Log.Store.PrintError("Cannot attempt purchase due to null BuyProductEventArgs");
			}
			else
			{
				BuyPmtProductEventArgs args2 = args as BuyPmtProductEventArgs;
				m_pendingProductPurchaseArgs = args;
				SendShopPurchaseEventTelemetry(isComplete: false);
				switch (args.PaymentCurrency)
				{
				case CurrencyType.GOLD:
				{
					BuyNoGTAPPEventArgs buyNoGTAPPEventArgs;
					if ((buyNoGTAPPEventArgs = args as BuyNoGTAPPEventArgs) != null)
					{
						OnStoreBuyWithGoldNoGTAPP(buyNoGTAPPEventArgs.transactionData);
					}
					else
					{
						OnStoreBuyWithGTAPP(args2);
					}
					break;
				}
				case CurrencyType.REAL_MONEY:
					OnStoreBuyWithMoney(args2);
					break;
				default:
					if (ShopUtils.IsCurrencyVirtual(args.PaymentCurrency))
					{
						OnStoreBuyWithCheckout(args2);
					}
					else
					{
						Log.Store.PrintError("Attempted purchase with invalid currency type {0}", args.PaymentCurrency);
					}
					break;
				}
			}
		};
		store.OnOpened += OnStoreOpen;
		store.OnClosed += delegate(StoreClosedArgs e)
		{
			OnStoreExit(e.authorizationBackButtonPressed.GetValueOrDefault(false), null);
		};
		store.OnReady += StoreViewReady;
		(store as Store)?.RegisterInfoListener(OnStoreInfo);
		StoreViewReady();
	}

	private void OnSceneUnloaded(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		UnloadAndFreeMemory();
	}

	private void OnHearthstoneCheckoutInitialized()
	{
		if (ShouldGetPersonalizedShopData())
		{
			RequestPersonalizedShopData();
		}
	}

	private void RequestPersonalizedShopData()
	{
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out var service) && service.CurrentState == HearthstoneCheckout.State.Idle)
		{
			Log.Store.PrintDebug("Getting personalized shop data with page id: {0}", m_shopPageId);
			service.GetPersonalizedShopData(m_shopPageId, OnHearthstoneGetPersonalizedShopData);
		}
	}

	private void OnHearthstoneCheckoutReady()
	{
		SendBlizzardCheckoutIsReadyTelemetry(sendIfReady: true);
	}

	private void SendBlizzardCheckoutIsReadyTelemetry(bool sendIfReady)
	{
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out var service))
		{
			float shownTime = service.GetShownTime();
			bool flag = service.CurrentState != HearthstoneCheckout.State.Initializing;
			if (sendIfReady == flag)
			{
				TelemetryManager.Client().SendBlizzardCheckoutIsReady(shownTime, flag);
			}
		}
	}

	private bool ShouldGetPersonalizedShopData()
	{
		return !string.IsNullOrEmpty(m_shopPageId);
	}

	public void SetPersonalizedShopPageAndRefreshCatalog(string pageId)
	{
		m_shopPageId = pageId;
		if (ShouldGetPersonalizedShopData())
		{
			RequestPersonalizedShopData();
		}
	}

	private void OnHearthstoneGetPersonalizedShopData(GetPageResponse response)
	{
		if (!ShouldGetPersonalizedShopData() || response == null)
		{
			return;
		}
		if (response.page == null)
		{
			Log.Store.PrintError("No page data was found for page id \"{0}\"", m_shopPageId);
			if (response.error != null && !string.IsNullOrEmpty(response.error.code))
			{
				Log.Store.PrintError("GetPageResponse Error: code:{0}, message:{1}", string.IsNullOrEmpty(response.error.code) ? "?" : response.error.code, string.IsNullOrEmpty(response.error.message) ? string.Empty : response.error.message);
			}
			return;
		}
		m_sections.Clear();
		Log.Store.PrintDebug("Section Data (page {0}):", m_shopPageId);
		foreach (Section section in response.page.sections)
		{
			string text = $"section {m_sections.Count + 1}: {section.name}";
			Network.ShopSection netSection = new Network.ShopSection();
			netSection.InternalName = section.name;
			if (section.localization != null)
			{
				netSection.Label = new DbfLocValue();
				netSection.Label.SetString(section.localization.name);
			}
			if (section.orderInPage >= 0)
			{
				netSection.SortOrder = section.orderInPage;
				Network.ShopSection shopSection = m_sections.FirstOrDefault((Network.ShopSection s) => s.SortOrder == netSection.SortOrder);
				if (shopSection != null)
				{
					Log.Store.PrintError("section {0} has the same SortOrder as {1}: {2}. Order may be inconsistent", section.name, shopSection.InternalName, netSection.SortOrder);
				}
				text += $"\n  sortOrder={netSection.SortOrder}";
			}
			else
			{
				Log.Store.PrintError("section {0} missing OrderInPage", section.name);
			}
			SectionAttribute sectionAttribute = section.attributes.FirstOrDefault((SectionAttribute a) => a.sectionAttributeKey.Equals("Style"));
			if (sectionAttribute != null)
			{
				netSection.Style = sectionAttribute.sectionAttributeValue;
				text = text + "\n  Style=" + netSection.Style;
			}
			SectionAttribute sectionAttribute2 = section.attributes.FirstOrDefault((SectionAttribute a) => a.sectionAttributeKey.Equals("TreatTagsAsFiller"));
			if (sectionAttribute2 != null)
			{
				netSection.FillerTags = sectionAttribute2.sectionAttributeValue;
			}
			netSection.Products = new List<Network.ShopSection.ProductRef>();
			foreach (ProductCollection productCollection in section.productCollections)
			{
				foreach (ProductCollectionItem item in productCollection.items)
				{
					Network.ShopSection.ProductRef productRef = new Network.ShopSection.ProductRef();
					productRef.OrderId = item.orderInProductCollection;
					productRef.PmtId = item.productCollectionItemValue;
					netSection.Products.Add(productRef);
					text += $"\n    [{productRef.OrderId}]={productRef.PmtId}";
				}
			}
			m_sections.Add(netSection);
			Log.Store.PrintDebug(text);
		}
	}

	private void OnHearthstoneCheckoutCancel()
	{
		Status = TransactionStatus.READY;
		if (m_activeMoneyOrGTAPPTransaction != null)
		{
			ConfirmActiveMoneyTransaction(m_activeMoneyOrGTAPPTransaction.ID);
		}
		m_view.PurchaseAuth.Hide();
		Network.Get().ReportBlizzardCheckoutStatus(BlizzardCheckoutStatus.BLIZZARD_CHECKOUT_STATUS_CANCELED);
		TelemetryManager.Client().SendBlizzardCheckoutPurchaseCancel();
		SendBlizzardCheckoutIsReadyTelemetry(sendIfReady: false);
	}

	private void OnHearthstoneCheckoutClose()
	{
		if (!m_view.PurchaseAuth.IsShown)
		{
			SetCanTapOutConfirmationUI(closeConfirmationUI: true);
			UnblockStoreInterface();
			if (Status == TransactionStatus.IN_PROGRESS_BLIZZARD_CHECKOUT || m_showStoreData.closeOnTransactionComplete)
			{
				GetCurrentStore().Close();
			}
		}
		else if (Status != TransactionStatus.READY)
		{
			m_view.PurchaseAuth.Hide();
			UnblockStoreInterface();
		}
	}

	private void OnHearthstoneCheckoutOrderPending(HearthstoneCheckoutTransactionData data)
	{
		if (IsHearthstoneCheckoutUIShowing() || m_view.PurchaseAuth.IsShown)
		{
			m_view.PurchaseAuth.Show(m_activeMoneyOrGTAPPTransaction, enableBack: false, isZeroCostLicense: false);
		}
		Status = TransactionStatus.IN_PROGRESS_BLIZZARD_CHECKOUT;
		if (data != null)
		{
			Network.Get().ReportBlizzardCheckoutStatus(BlizzardCheckoutStatus.BLIZZARD_CHECKOUT_STATUS_START, data);
			TelemetryManager.Client().SendBlizzardCheckoutPurchaseStart(data.TransactionID, data.ProductID.ToString(), data.CurrencyCode);
		}
	}

	private void OnHearthstoneCheckoutOrderFailure(HearthstoneCheckoutTransactionData data)
	{
		if (!m_view.PurchaseAuth.IsShown)
		{
			m_view.PurchaseAuth.Show(m_activeMoneyOrGTAPPTransaction, enableBack: false, isZeroCostLicense: false);
		}
		string hearthstoneCheckoutErrorString = GetHearthstoneCheckoutErrorString(data.ErrorCodes);
		m_view.PurchaseAuth.CompletePurchaseFailure(m_activeMoneyOrGTAPPTransaction, hearthstoneCheckoutErrorString, Network.PurchaseErrorInfo.ErrorType.BP_GENERIC_FAIL);
		Status = TransactionStatus.READY;
		Network.Get().ReportBlizzardCheckoutStatus(BlizzardCheckoutStatus.BLIZZARD_CHECKOUT_STATUS_COMPLETED_FAILED, data);
		TelemetryManager.Client().SendBlizzardCheckoutPurchaseCompletedFailure(data.TransactionID, data.ProductID.ToString(), data.CurrencyCode, new List<string> { data.ErrorCodes });
		Log.Store.PrintError("Checkout Order Failure: TransactionID={0}, ProductID={1}, CurrencyCode={2}, ErrorCodes={3}", data.TransactionID, data.ProductID, data.CurrencyCode, data.ErrorCodes);
	}

	private void OnHearthstoneCheckoutSubmitFailure()
	{
		if (!m_view.PurchaseAuth.IsShown)
		{
			m_view.PurchaseAuth.Show(m_activeMoneyOrGTAPPTransaction, enableBack: false, isZeroCostLicense: false);
		}
		string details = GameStrings.Get("GLUE_CHECKOUT_ERROR_GENERIC_FAILURE");
		m_view.PurchaseAuth.CompletePurchaseFailure(m_activeMoneyOrGTAPPTransaction, details, Network.PurchaseErrorInfo.ErrorType.BP_GENERIC_FAIL);
		Status = TransactionStatus.READY;
	}

	private string GetHearthstoneCheckoutErrorString(string errorCode)
	{
		switch (errorCode)
		{
		case "BLZBNTPURJNL42203":
			return GameStrings.Get("GLUE_STORE_FAIL_PRODUCT_ALREADY_OWNED");
		case "BLZBNTPURJNL42208":
			return GameStrings.Get("GLUE_STORE_FAIL_SPENDING_LIMIT");
		case "BLZBNTPUR3000003":
		case "10201001":
			return GameStrings.Get("GLUE_CHECKOUT_ERROR_INSUFFICIENT_FUNDS");
		case "30000101":
			return GameStrings.Get("GLUE_CHECKOUT_ERROR_PRODUCT_UNAVAILABLE");
		default:
			Log.Store.PrintWarning("Unhandled checkout error: {0}", errorCode);
			return GameStrings.Get("GLUE_CHECKOUT_ERROR_GENERIC_FAILURE");
		}
	}

	private void OnHearthstoneCheckoutOrderComplete(HearthstoneCheckoutTransactionData data)
	{
		if (IsHearthstoneCheckoutUIShowing() && !m_view.PurchaseAuth.IsShown)
		{
			m_view.PurchaseAuth.Show(m_activeMoneyOrGTAPPTransaction, enableBack: false, isZeroCostLicense: false);
		}
		SendAttributionPurchaseMessage(data);
		HandlePurchaseSuccess(null, m_activeMoneyOrGTAPPTransaction, string.Empty, data);
		if (data != null)
		{
			Network.Get().ReportBlizzardCheckoutStatus(BlizzardCheckoutStatus.BLIZZARD_CHECKOUT_STATUS_COMPLETED_SUCCESS, data);
			TelemetryManager.Client().SendBlizzardCheckoutPurchaseCompletedSuccess(data.TransactionID, data.ProductID.ToString(), data.CurrencyCode);
		}
	}

	private void SendAttributionPurchaseMessage(HearthstoneCheckoutTransactionData transactionData)
	{
		if (!HasExternalStore)
		{
			return;
		}
		if (transactionData == null)
		{
			Log.Store.PrintWarning("[SendAttributionPurchaseMessage] No transaction data provided, skipping attribution message.");
		}
		else
		{
			if (transactionData.IsVCPurchase)
			{
				return;
			}
			if (!HearthstoneServices.TryGet<AdTrackingManager>(out var service))
			{
				Log.Store.PrintWarning("[SendAttributionPurchaseMessage] AdTrackingManager unavailable, skipping attribution message.");
				return;
			}
			Network.Bundle bundleFromPmtProductId = GetBundleFromPmtProductId(transactionData.ProductID);
			if (bundleFromPmtProductId == null)
			{
				Log.Store.PrintWarning("[SendAttributionPurchaseMessage] Unable to find bundle for PMT Product ID {0}, skipping attribution message.", transactionData.ProductID);
				return;
			}
			double? costDisplay = bundleFromPmtProductId.CostDisplay;
			string currencyCode = transactionData.CurrencyCode;
			string transactionID = transactionData.TransactionID;
			string productId = GetExternalStoreProductId(bundleFromPmtProductId) ?? bundleFromPmtProductId.PMTProductID.ToString();
			service.TrackSale(costDisplay ?? 0.0, currencyCode, productId, transactionID);
		}
	}

	private bool IsHearthstoneCheckoutUIShowing()
	{
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out var service))
		{
			return service.IsUIShown();
		}
		return false;
	}

	public bool WillStoreDisplayNotice(NetCache.ProfileNotice.NoticeOrigin noticeOrigin, NetCache.ProfileNotice.NoticeType noticeType, long noticeOriginData)
	{
		GeneralStore generalStore = GetCurrentStore() as GeneralStore;
		if (generalStore == null)
		{
			return false;
		}
		GeneralStorePacksPane generalStorePacksPane = generalStore.GetCurrentPane() as GeneralStorePacksPane;
		if (generalStorePacksPane == null)
		{
			return false;
		}
		return generalStorePacksPane.WillStoreDisplayNotice(noticeOrigin, noticeType, noticeOriginData);
	}

	private string GetProductPrice(string pmtProductId)
	{
		if (TryGetProductInfo(pmtProductId, out var productInfo))
		{
			return productInfo.Price;
		}
		return null;
	}

	private bool TryGetProductInfo(string pmtProductId, out HearthstoneCheckout.ProductInfo productInfo)
	{
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out var service))
		{
			return service.TryGetProductInfo(pmtProductId, out productInfo);
		}
		productInfo = default(HearthstoneCheckout.ProductInfo);
		return false;
	}

	private bool IsCheckoutFallbackSupported()
	{
		return false;
	}
}
