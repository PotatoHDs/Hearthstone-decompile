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

// Token: 0x02000725 RID: 1829
public class StoreManager
{
	// Token: 0x17000607 RID: 1543
	// (get) Token: 0x060065E7 RID: 26087 RVA: 0x00211FB2 File Offset: 0x002101B2
	public bool IgnoreProductTiming
	{
		get
		{
			return this.m_ignoreProductTiming;
		}
	}

	// Token: 0x17000608 RID: 1544
	// (get) Token: 0x060065E8 RID: 26088 RVA: 0x00211FBA File Offset: 0x002101BA
	public IEnumerable<Network.Bundle> AllBundles
	{
		get
		{
			return this.m_bundles.Values;
		}
	}

	// Token: 0x17000609 RID: 1545
	// (get) Token: 0x060065E9 RID: 26089 RVA: 0x00211FC7 File Offset: 0x002101C7
	public IEnumerable<Network.GoldCostBooster> AllGoldCostBoosters
	{
		get
		{
			return this.m_goldCostBooster.Values;
		}
	}

	// Token: 0x1700060A RID: 1546
	// (get) Token: 0x060065EA RID: 26090 RVA: 0x00211FD4 File Offset: 0x002101D4
	public IEnumerable<Network.ShopSection> AllSections
	{
		get
		{
			return this.m_sections;
		}
	}

	// Token: 0x060065EB RID: 26091 RVA: 0x00211FDC File Offset: 0x002101DC
	private StoreManager()
	{
		this.Catalog = new ProductCatalog(this);
	}

	// Token: 0x1700060B RID: 1547
	// (get) Token: 0x060065EC RID: 26092 RVA: 0x00212195 File Offset: 0x00210395
	public StorePackId CurrentlySelectedId
	{
		get
		{
			return this.m_currentlySelectedId;
		}
	}

	// Token: 0x14000065 RID: 101
	// (add) Token: 0x060065ED RID: 26093 RVA: 0x002121A0 File Offset: 0x002103A0
	// (remove) Token: 0x060065EE RID: 26094 RVA: 0x002121D8 File Offset: 0x002103D8
	private event Action<bool> OnStatusChanged = delegate(bool isOpen)
	{
	};

	// Token: 0x14000066 RID: 102
	// (add) Token: 0x060065EF RID: 26095 RVA: 0x00212210 File Offset: 0x00210410
	// (remove) Token: 0x060065F0 RID: 26096 RVA: 0x00212248 File Offset: 0x00210448
	private event Action<Network.Bundle, PaymentMethod> OnSuccessfulPurchaseAck = delegate(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
	};

	// Token: 0x14000067 RID: 103
	// (add) Token: 0x060065F1 RID: 26097 RVA: 0x00212280 File Offset: 0x00210480
	// (remove) Token: 0x060065F2 RID: 26098 RVA: 0x002122B8 File Offset: 0x002104B8
	private event Action<Network.Bundle, PaymentMethod> OnSuccessfulPurchase = delegate(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
	};

	// Token: 0x14000068 RID: 104
	// (add) Token: 0x060065F3 RID: 26099 RVA: 0x002122F0 File Offset: 0x002104F0
	// (remove) Token: 0x060065F4 RID: 26100 RVA: 0x00212328 File Offset: 0x00210528
	private event Action<Network.Bundle, PaymentMethod> OnFailedPurchaseAck = delegate(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
	};

	// Token: 0x14000069 RID: 105
	// (add) Token: 0x060065F5 RID: 26101 RVA: 0x00212360 File Offset: 0x00210560
	// (remove) Token: 0x060065F6 RID: 26102 RVA: 0x00212398 File Offset: 0x00210598
	private event Action OnAuthorizationExit = delegate()
	{
	};

	// Token: 0x1400006A RID: 106
	// (add) Token: 0x060065F7 RID: 26103 RVA: 0x002123D0 File Offset: 0x002105D0
	// (remove) Token: 0x060065F8 RID: 26104 RVA: 0x00212408 File Offset: 0x00210608
	private event Action OnStoreShown = delegate()
	{
	};

	// Token: 0x1400006B RID: 107
	// (add) Token: 0x060065F9 RID: 26105 RVA: 0x00212440 File Offset: 0x00210640
	// (remove) Token: 0x060065FA RID: 26106 RVA: 0x00212478 File Offset: 0x00210678
	private event Action OnStoreHidden = delegate()
	{
	};

	// Token: 0x060065FB RID: 26107 RVA: 0x002124AD File Offset: 0x002106AD
	public static StoreManager Get()
	{
		StoreManager result;
		if ((result = StoreManager.s_instance) == null)
		{
			result = (StoreManager.s_instance = new StoreManager());
		}
		return result;
	}

	// Token: 0x060065FC RID: 26108 RVA: 0x002124C3 File Offset: 0x002106C3
	public static bool IsInitialized()
	{
		return StoreManager.s_instance != null;
	}

	// Token: 0x060065FD RID: 26109 RVA: 0x002124D0 File Offset: 0x002106D0
	private static void DestroyInstance()
	{
		IStore store = StoreManager.s_instance.GetStore(ShopType.GENERAL_STORE);
		if (store != null)
		{
			store.Unload();
		}
		if (AchieveManager.Get() != null && StoreManager.s_instance != null)
		{
			AchieveManager.Get().RemoveAchievesUpdatedListener(new AchieveManager.AchievesUpdatedCallback(StoreManager.s_instance.OnAchievesUpdated));
			AchieveManager.Get().RemoveLicenseAddedAchievesUpdatedListener(new AchieveManager.LicenseAddedAchievesUpdatedCallback(StoreManager.s_instance.OnLicenseAddedAchievesUpdated));
		}
		StoreManager.s_instance = null;
	}

	// Token: 0x1700060C RID: 1548
	// (get) Token: 0x060065FE RID: 26110 RVA: 0x0021253D File Offset: 0x0021073D
	public ProductCatalog Catalog { get; }

	// Token: 0x1700060D RID: 1549
	// (get) Token: 0x060065FF RID: 26111 RVA: 0x000052EC File Offset: 0x000034EC
	private static BattlePayProvider StoreProvider
	{
		get
		{
			return BattlePayProvider.BP_PROVIDER_BLIZZARD;
		}
	}

	// Token: 0x06006600 RID: 26112 RVA: 0x00212548 File Offset: 0x00210748
	private void NetworkRegistration()
	{
		Network network = Network.Get();
		network.RegisterNetHandler(BattlePayStatusResponse.PacketID.ID, new Network.NetHandler(this.OnBattlePayStatusResponse), null);
		network.RegisterNetHandler(BattlePayConfigResponse.PacketID.ID, new Network.NetHandler(this.OnBattlePayConfigResponse), null);
		network.RegisterNetHandler(PurchaseMethod.PacketID.ID, new Network.NetHandler(this.OnPurchaseMethod), null);
		network.RegisterNetHandler(PurchaseResponse.PacketID.ID, new Network.NetHandler(this.OnPurchaseResponse), null);
		network.RegisterNetHandler(CancelPurchaseResponse.PacketID.ID, new Network.NetHandler(this.OnPurchaseCanceledResponse), null);
		network.RegisterNetHandler(PurchaseWithGoldResponse.PacketID.ID, new Network.NetHandler(this.OnPurchaseViaGoldResponse), null);
		network.RegisterNetHandler(ThirdPartyPurchaseStatusResponse.PacketID.ID, new Network.NetHandler(this.OnThirdPartyPurchaseStatusResponse), null);
	}

	// Token: 0x06006601 RID: 26113 RVA: 0x0021262C File Offset: 0x0021082C
	private void HearthstoneCheckoutRegistration()
	{
		HearthstoneCheckout hearthstoneCheckout;
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout))
		{
			hearthstoneCheckout.OnInitializedEvent.AddListener(new Action(this.OnHearthstoneCheckoutInitialized));
			hearthstoneCheckout.OnReadyEvent.AddListener(new Action(this.OnHearthstoneCheckoutReady));
			hearthstoneCheckout.OnCancelEvent.AddListener(new Action(this.OnHearthstoneCheckoutCancel));
			hearthstoneCheckout.OnCloseEvent.AddListener(new Action(this.OnHearthstoneCheckoutClose));
			hearthstoneCheckout.OnOrderPendingEvent.AddListener(new Action<HearthstoneCheckoutTransactionData>(this.OnHearthstoneCheckoutOrderPending));
			hearthstoneCheckout.OnOrderFailureEvent.AddListener(new Action<HearthstoneCheckoutTransactionData>(this.OnHearthstoneCheckoutOrderFailure));
			hearthstoneCheckout.OnOrderCompleteEvent.AddListener(new Action<HearthstoneCheckoutTransactionData>(this.OnHearthstoneCheckoutOrderComplete));
			hearthstoneCheckout.OnPurchaseFailureBeforeSubmitEvent.AddListener(new Action(this.OnHearthstoneCheckoutSubmitFailure));
		}
	}

	// Token: 0x06006602 RID: 26114 RVA: 0x00212700 File Offset: 0x00210900
	public void Init()
	{
		NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheFeatures), new Action(this.OnNetCacheFeaturesReady));
		if (this.m_initComplete)
		{
			return;
		}
		SceneMgr.Get().RegisterSceneUnloadedEvent(new SceneMgr.SceneUnloadedCallback(this.OnSceneUnloaded));
		this.NetworkRegistration();
		this.HearthstoneCheckoutRegistration();
		NetCache.NetCacheProfileNotices netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>();
		if (netObject != null)
		{
			this.OnNewNotices(netObject.Notices, false);
		}
		NetCache.Get().RegisterNewNoticesListener(new NetCache.DelNewNoticesListener(this.OnNewNotices));
		LoginManager.Get().OnFullLoginFlowComplete += this.OnLoginCompleted;
		this.m_regionId = BattleNet.GetCurrentRegion();
		this.RegisterViewListeners();
		this.m_initComplete = true;
		AssetLoader.Get().InstantiatePrefab(ShopPrefabs.ShopPrefab, new PrefabCallback<GameObject>(this.OnGeneralStoreLoaded), null, AssetLoadingOptions.None);
		HearthstoneApplication.Get().WillReset += this.WillReset;
	}

	// Token: 0x06006603 RID: 26115 RVA: 0x002127F8 File Offset: 0x002109F8
	private void WillReset()
	{
		HearthstoneApplication.Get().WillReset -= this.WillReset;
		this.UnregisterViewListeners();
		Network network = Network.Get();
		network.RemoveNetHandler(BattlePayStatusResponse.PacketID.ID, new Network.NetHandler(this.OnBattlePayStatusResponse));
		network.RemoveNetHandler(BattlePayConfigResponse.PacketID.ID, new Network.NetHandler(this.OnBattlePayConfigResponse));
		network.RemoveNetHandler(PurchaseMethod.PacketID.ID, new Network.NetHandler(this.OnPurchaseMethod));
		network.RemoveNetHandler(PurchaseResponse.PacketID.ID, new Network.NetHandler(this.OnPurchaseResponse));
		network.RemoveNetHandler(CancelPurchaseResponse.PacketID.ID, new Network.NetHandler(this.OnPurchaseCanceledResponse));
		network.RemoveNetHandler(PurchaseWithGoldResponse.PacketID.ID, new Network.NetHandler(this.OnPurchaseViaGoldResponse));
		network.RemoveNetHandler(ThirdPartyPurchaseStatusResponse.PacketID.ID, new Network.NetHandler(this.OnThirdPartyPurchaseStatusResponse));
		HearthstoneCheckout hearthstoneCheckout;
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout))
		{
			hearthstoneCheckout.OnInitializedEvent.RemoveListener(new Action(this.OnHearthstoneCheckoutInitialized));
			hearthstoneCheckout.OnReadyEvent.RemoveListener(new Action(this.OnHearthstoneCheckoutReady));
			hearthstoneCheckout.OnCancelEvent.RemoveListener(new Action(this.OnHearthstoneCheckoutCancel));
			hearthstoneCheckout.OnCloseEvent.RemoveListener(new Action(this.OnHearthstoneCheckoutClose));
			hearthstoneCheckout.OnOrderPendingEvent.RemoveListener(new Action<HearthstoneCheckoutTransactionData>(this.OnHearthstoneCheckoutOrderPending));
			hearthstoneCheckout.OnOrderFailureEvent.RemoveListener(new Action<HearthstoneCheckoutTransactionData>(this.OnHearthstoneCheckoutOrderFailure));
			hearthstoneCheckout.OnOrderCompleteEvent.RemoveListener(new Action<HearthstoneCheckoutTransactionData>(this.OnHearthstoneCheckoutOrderComplete));
			hearthstoneCheckout.OnPurchaseFailureBeforeSubmitEvent.RemoveListener(new Action(this.OnHearthstoneCheckoutSubmitFailure));
		}
		NetCache.Get().RemoveUpdatedListener(typeof(NetCache.NetCacheFeatures), new Action(this.OnNetCacheFeaturesReady));
		StoreManager.DestroyInstance();
	}

	// Token: 0x06006604 RID: 26116 RVA: 0x002129DC File Offset: 0x00210BDC
	public void Heartbeat()
	{
		if (!this.m_initComplete)
		{
			return;
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		this.AutoCancelPurchaseIfNeeded(realtimeSinceStartup);
	}

	// Token: 0x06006605 RID: 26117 RVA: 0x00212A00 File Offset: 0x00210C00
	public ShopAvailabilityError GetStoreAvailabilityError()
	{
		if (!this.FirstNoticesProcessed)
		{
			return ShopAvailabilityError.FIRST_NOTICES_NOT_PROCESSED;
		}
		if (!this.IsStoreFeatureEnabled())
		{
			return ShopAvailabilityError.STORE_FEATURE_DISABLED;
		}
		if (!this.BattlePayAvailable)
		{
			return ShopAvailabilityError.BATTLEPAY_UNAVAILABLE;
		}
		if (!this.ConfigLoaded)
		{
			return ShopAvailabilityError.BATTLEPAY_CONFIG_NOT_LOADED;
		}
		if (!this.HaveProductsToSell())
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
		if (!this.IsCheckoutFallbackSupported() && !Vars.Key("Mobile.SkipStoreValidation").GetBool(false))
		{
			if (!HearthstoneCheckout.IsAvailable())
			{
				return ShopAvailabilityError.CHECKOUT_UNAVAILABLE;
			}
			if (!this.IsSimpleCheckoutFeatureEnabled())
			{
				return ShopAvailabilityError.CHECKOUT_NOT_ENABLED;
			}
		}
		if (this.Status == StoreManager.TransactionStatus.UNKNOWN)
		{
			return ShopAvailabilityError.TRANSACTION_STATUS_UNKNOWN;
		}
		return ShopAvailabilityError.NO_ERROR;
	}

	// Token: 0x06006606 RID: 26118 RVA: 0x00212A8C File Offset: 0x00210C8C
	public bool IsOpen(bool printStatus = true)
	{
		ShopAvailabilityError storeAvailabilityError = this.GetStoreAvailabilityError();
		if (storeAvailabilityError == ShopAvailabilityError.NO_ERROR)
		{
			if (printStatus)
			{
				global::Log.Store.Print("Store is OPEN.", Array.Empty<object>());
			}
			return true;
		}
		if (printStatus)
		{
			global::Log.Store.Print("Store is CLOSED due to: {0}", new object[]
			{
				storeAvailabilityError.ToString()
			});
		}
		return false;
	}

	// Token: 0x06006607 RID: 26119 RVA: 0x00212AE8 File Offset: 0x00210CE8
	private bool IsStoreFeatureEnabled()
	{
		NetCache.NetCacheFeatures netCacheFeatures = this.GetNetCacheFeatures();
		return netCacheFeatures != null && netCacheFeatures.Store.Store;
	}

	// Token: 0x06006608 RID: 26120 RVA: 0x00212B0C File Offset: 0x00210D0C
	public bool IsBattlePayFeatureEnabled()
	{
		NetCache.NetCacheFeatures netCacheFeatures = this.GetNetCacheFeatures();
		return netCacheFeatures != null && netCacheFeatures.Store.Store && netCacheFeatures.Store.BattlePay;
	}

	// Token: 0x06006609 RID: 26121 RVA: 0x00212B40 File Offset: 0x00210D40
	public bool IsBuyWithGoldFeatureEnabled()
	{
		NetCache.NetCacheFeatures netCacheFeatures = this.GetNetCacheFeatures();
		return netCacheFeatures != null && netCacheFeatures.Store.Store && netCacheFeatures.Store.BuyWithGold;
	}

	// Token: 0x0600660A RID: 26122 RVA: 0x00212B73 File Offset: 0x00210D73
	private void SetCanTapOutConfirmationUI(bool closeConfirmationUI)
	{
		this.m_canCloseConfirmation = closeConfirmationUI;
	}

	// Token: 0x0600660B RID: 26123 RVA: 0x00212B7C File Offset: 0x00210D7C
	public bool CanTapOutConfirmationUI()
	{
		return this.m_canCloseConfirmation;
	}

	// Token: 0x0600660C RID: 26124 RVA: 0x00212B84 File Offset: 0x00210D84
	public bool IsSimpleCheckoutFeatureEnabled()
	{
		NetCache.NetCacheFeatures netCacheFeatures = this.GetNetCacheFeatures();
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
			case AndroidStore.BLIZZARD:
			case AndroidStore.HUAWEI:
			case AndroidStore.ONE_STORE:
				flag = netCacheFeatures.Store.SimpleCheckoutAndroidGlobal;
				break;
			case AndroidStore.GOOGLE:
				flag = netCacheFeatures.Store.SimpleCheckoutAndroidGoogle;
				break;
			case AndroidStore.AMAZON:
				flag = netCacheFeatures.Store.SimpleCheckoutAndroidAmazon;
				break;
			default:
				global::Log.Store.PrintError("The given store was not accounted for: {0}\nPlease check in '{1}.{2}' class and method for implementation.", new object[]
				{
					AndroidDeviceSettings.Get().GetAndroidStore().ToString(),
					"StoreManager",
					"IsSimpleCheckoutFeatureEnabled"
				});
				break;
			}
			break;
		}
		return flag && netCacheFeatures.Store.Store && netCacheFeatures.Store.SimpleCheckout && HearthstoneCheckout.IsAvailable();
	}

	// Token: 0x0600660D RID: 26125 RVA: 0x00212CAC File Offset: 0x00210EAC
	private bool IsSoftAccountPurchasingEnabled()
	{
		NetCache.NetCacheFeatures netCacheFeatures = this.GetNetCacheFeatures();
		return netCacheFeatures != null && netCacheFeatures.Store.Store && netCacheFeatures.Store.SoftAccountPurchasing;
	}

	// Token: 0x0600660E RID: 26126 RVA: 0x00212CE0 File Offset: 0x00210EE0
	public bool IsVintageStoreEnabled()
	{
		NetCache.NetCacheFeatures netCacheFeatures = this.GetNetCacheFeatures();
		return netCacheFeatures == null || netCacheFeatures.Store.VintageStore;
	}

	// Token: 0x0600660F RID: 26127 RVA: 0x00212D04 File Offset: 0x00210F04
	public bool IsBuyCardBacksFromCollectionManagerEnabled()
	{
		NetCache.NetCacheFeatures netCacheFeatures = this.GetNetCacheFeatures();
		return netCacheFeatures == null || netCacheFeatures.Store.BuyCardBacksFromCollectionManager;
	}

	// Token: 0x06006610 RID: 26128 RVA: 0x00212D28 File Offset: 0x00210F28
	public bool IsBuyHeroSkinsFromCollectionManagerEnabled()
	{
		NetCache.NetCacheFeatures netCacheFeatures = this.GetNetCacheFeatures();
		return netCacheFeatures == null || netCacheFeatures.Store.BuyHeroSkinsFromCollectionManager;
	}

	// Token: 0x06006611 RID: 26129 RVA: 0x00212D4C File Offset: 0x00210F4C
	public BattlePayProvider? ActiveTransactionProvider()
	{
		MoneyOrGTAPPTransaction activeMoneyOrGTAPPTransaction = this.m_activeMoneyOrGTAPPTransaction;
		if (activeMoneyOrGTAPPTransaction == null)
		{
			return null;
		}
		return activeMoneyOrGTAPPTransaction.Provider;
	}

	// Token: 0x06006612 RID: 26130 RVA: 0x00212D72 File Offset: 0x00210F72
	public void RegisterStatusChangedListener(Action<bool> callback)
	{
		this.OnStatusChanged -= callback;
		this.OnStatusChanged += callback;
	}

	// Token: 0x06006613 RID: 26131 RVA: 0x00212D82 File Offset: 0x00210F82
	public void RemoveStatusChangedListener(Action<bool> callback)
	{
		this.OnStatusChanged -= callback;
	}

	// Token: 0x06006614 RID: 26132 RVA: 0x00212D8B File Offset: 0x00210F8B
	public void RegisterSuccessfulPurchaseListener(Action<Network.Bundle, PaymentMethod> callback)
	{
		this.OnSuccessfulPurchase -= callback;
		this.OnSuccessfulPurchase += callback;
	}

	// Token: 0x06006615 RID: 26133 RVA: 0x00212D9B File Offset: 0x00210F9B
	public void RemoveSuccessfulPurchaseListener(Action<Network.Bundle, PaymentMethod> callback)
	{
		this.OnSuccessfulPurchase -= callback;
	}

	// Token: 0x06006616 RID: 26134 RVA: 0x00212DA4 File Offset: 0x00210FA4
	public void RegisterSuccessfulPurchaseAckListener(Action<Network.Bundle, PaymentMethod> callback)
	{
		this.OnSuccessfulPurchaseAck -= callback;
		this.OnSuccessfulPurchaseAck += callback;
	}

	// Token: 0x06006617 RID: 26135 RVA: 0x00212DB4 File Offset: 0x00210FB4
	public void RemoveSuccessfulPurchaseAckListener(Action<Network.Bundle, PaymentMethod> callback)
	{
		this.OnSuccessfulPurchaseAck -= callback;
	}

	// Token: 0x06006618 RID: 26136 RVA: 0x00212DBD File Offset: 0x00210FBD
	public void RegisterFailedPurchaseAckListener(Action<Network.Bundle, PaymentMethod> callback)
	{
		this.OnFailedPurchaseAck -= callback;
		this.OnFailedPurchaseAck += callback;
	}

	// Token: 0x06006619 RID: 26137 RVA: 0x00212DCD File Offset: 0x00210FCD
	public void RemoveFailedPurchaseAckListener(Action<Network.Bundle, PaymentMethod> callback)
	{
		this.OnFailedPurchaseAck -= callback;
	}

	// Token: 0x0600661A RID: 26138 RVA: 0x00212DD6 File Offset: 0x00210FD6
	public void RegisterAuthorizationExitListener(Action callback)
	{
		this.OnAuthorizationExit -= callback;
		this.OnAuthorizationExit += callback;
	}

	// Token: 0x0600661B RID: 26139 RVA: 0x00212DE6 File Offset: 0x00210FE6
	public void RemoveAuthorizationExitListener(Action callback)
	{
		this.OnAuthorizationExit -= callback;
	}

	// Token: 0x0600661C RID: 26140 RVA: 0x00212DEF File Offset: 0x00210FEF
	public void RegisterStoreShownListener(Action callback)
	{
		this.OnStoreShown -= callback;
		this.OnStoreShown += callback;
	}

	// Token: 0x0600661D RID: 26141 RVA: 0x00212DFF File Offset: 0x00210FFF
	public void RemoveStoreShownListener(Action callback)
	{
		this.OnStoreShown -= callback;
	}

	// Token: 0x0600661E RID: 26142 RVA: 0x00212E08 File Offset: 0x00211008
	public void RegisterStoreHiddenListener(Action callback)
	{
		this.OnStoreHidden -= callback;
		this.OnStoreHidden += callback;
	}

	// Token: 0x0600661F RID: 26143 RVA: 0x00212E18 File Offset: 0x00211018
	public void RemoveStoreHiddenListener(Action callback)
	{
		this.OnStoreHidden -= callback;
	}

	// Token: 0x06006620 RID: 26144 RVA: 0x00212E24 File Offset: 0x00211024
	private void RegisterViewListeners()
	{
		this.m_view.OnComponentReady += this.StoreViewReady;
		this.m_view.PurchaseAuth.OnPurchaseResultAcknowledged += this.OnPurchaseResultAcknowledged;
		this.m_view.PurchaseAuth.OnAuthExit += this.OnAuthExit;
		this.m_view.Summary.OnSummaryConfirm += this.OnSummaryConfirm;
		this.m_view.Summary.OnSummaryCancel += this.OnSummaryCancel;
		this.m_view.Summary.OnSummaryInfo += this.OnSummaryInfo;
		this.m_view.Summary.OnSummaryPaymentAndTos += this.OnSummaryPaymentAndTOS;
		this.m_view.SendToBam.OnOkay += this.OnSendToBAMOkay;
		this.m_view.SendToBam.OnCancel += this.OnSendToBAMCancel;
		this.m_view.LegalBam.OnOkay += this.OnSendToBAMLegal;
		this.m_view.LegalBam.OnCancel += this.UnblockStoreInterface;
		this.m_view.DoneWithBam.OnOkay += this.UnblockStoreInterface;
		this.m_view.ChallengePrompt.OnComplete += this.OnChallengeComplete;
		this.m_view.ChallengePrompt.OnCancel += this.OnChallengeCancel;
	}

	// Token: 0x06006621 RID: 26145 RVA: 0x00212FB4 File Offset: 0x002111B4
	private void UnregisterViewListeners()
	{
		this.m_view.OnComponentReady -= this.StoreViewReady;
		this.m_view.PurchaseAuth.OnPurchaseResultAcknowledged -= this.OnPurchaseResultAcknowledged;
		this.m_view.PurchaseAuth.OnAuthExit -= this.OnAuthExit;
		this.m_view.Summary.OnSummaryConfirm -= this.OnSummaryConfirm;
		this.m_view.Summary.OnSummaryCancel -= this.OnSummaryCancel;
		this.m_view.Summary.OnSummaryInfo -= this.OnSummaryInfo;
		this.m_view.Summary.OnSummaryPaymentAndTos -= this.OnSummaryPaymentAndTOS;
		this.m_view.SendToBam.OnOkay -= this.OnSendToBAMOkay;
		this.m_view.SendToBam.OnCancel -= this.OnSendToBAMCancel;
		this.m_view.LegalBam.OnOkay -= this.OnSendToBAMLegal;
		this.m_view.LegalBam.OnCancel -= this.UnblockStoreInterface;
		this.m_view.DoneWithBam.OnOkay -= this.UnblockStoreInterface;
		this.m_view.ChallengePrompt.OnComplete -= this.OnChallengeComplete;
		this.m_view.ChallengePrompt.OnCancel -= this.OnChallengeCancel;
	}

	// Token: 0x06006622 RID: 26146 RVA: 0x00213144 File Offset: 0x00211344
	private bool IsWaitingToShow()
	{
		return this.m_waitingToShowStore;
	}

	// Token: 0x06006623 RID: 26147 RVA: 0x0021314C File Offset: 0x0021134C
	public IStore GetCurrentStore()
	{
		return this.GetStore(this.m_currentShopType);
	}

	// Token: 0x06006624 RID: 26148 RVA: 0x0021315C File Offset: 0x0021135C
	private IStore GetStore(ShopType shopType)
	{
		IStore result;
		this.m_stores.TryGetValue(shopType, out result);
		return result;
	}

	// Token: 0x06006625 RID: 26149 RVA: 0x0021317C File Offset: 0x0021137C
	public bool IsShown()
	{
		IStore currentStore = this.GetCurrentStore();
		return currentStore != null && currentStore.IsOpen();
	}

	// Token: 0x06006626 RID: 26150 RVA: 0x0021319B File Offset: 0x0021139B
	public bool IsShownOrWaitingToShow()
	{
		return this.IsWaitingToShow() || this.IsShown();
	}

	// Token: 0x06006627 RID: 26151 RVA: 0x002131B4 File Offset: 0x002113B4
	public bool GetGoldCostNoGTAPP(NoGTAPPTransactionData noGTAPPTransactionData, out long cost)
	{
		cost = 0L;
		if (noGTAPPTransactionData == null)
		{
			return false;
		}
		long num = 0L;
		ProductType product = noGTAPPTransactionData.Product;
		if (product != ProductType.PRODUCT_TYPE_BOOSTER)
		{
			if (product != ProductType.PRODUCT_TYPE_DRAFT)
			{
				if (product != ProductType.PRODUCT_TYPE_HIDDEN_LICENSE)
				{
					global::Log.Store.PrintWarning(string.Format("StoreManager.GetGoldCostNoGTAPP(): don't have a no-GTAPP gold price for product {0} data {1}", noGTAPPTransactionData.Product, noGTAPPTransactionData.ProductData), Array.Empty<object>());
					return false;
				}
				return false;
			}
			else if (!this.GetArenaGoldCostNoGTAPP(out num))
			{
				return false;
			}
		}
		else if (!this.GetBoosterGoldCostNoGTAPP(noGTAPPTransactionData.ProductData, out num))
		{
			return false;
		}
		cost = num * (long)noGTAPPTransactionData.Quantity;
		return true;
	}

	// Token: 0x06006628 RID: 26152 RVA: 0x00213240 File Offset: 0x00211440
	public Network.Bundle GetBundleFromPmtProductId(long pmtProductId)
	{
		if (pmtProductId == 0L)
		{
			return null;
		}
		Network.Bundle result;
		if (this.m_bundles.TryGetValue(pmtProductId, out result))
		{
			return result;
		}
		return null;
	}

	// Token: 0x06006629 RID: 26153 RVA: 0x00213268 File Offset: 0x00211468
	private HashSet<ProductType> GetProductsInItemList(List<Network.BundleItem> items)
	{
		HashSet<ProductType> hashSet = new HashSet<ProductType>();
		foreach (Network.BundleItem bundleItem in items)
		{
			hashSet.Add(bundleItem.ItemType);
		}
		return hashSet;
	}

	// Token: 0x0600662A RID: 26154 RVA: 0x002132C4 File Offset: 0x002114C4
	public HashSet<ProductType> GetProductsInBundle(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return new HashSet<ProductType>();
		}
		return this.GetProductsInItemList(bundle.Items);
	}

	// Token: 0x0600662B RID: 26155 RVA: 0x002132DC File Offset: 0x002114DC
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
		foreach (Network.BundleItem bundleItem in bundle.Items)
		{
			if (!shouldSeeWild && !flag)
			{
				switch (bundleItem.ItemType)
				{
				case ProductType.PRODUCT_TYPE_BOOSTER:
					flag = GameUtils.IsBoosterWild((BoosterDbId)bundleItem.ProductData);
					break;
				case ProductType.PRODUCT_TYPE_NAXX:
				case ProductType.PRODUCT_TYPE_BRM:
				case ProductType.PRODUCT_TYPE_LOE:
					flag = true;
					break;
				case ProductType.PRODUCT_TYPE_WING:
					flag = GameUtils.IsAdventureWild(GameUtils.GetAdventureIdByWingId(bundleItem.ProductData));
					break;
				}
			}
			switch (StoreManager.GetProductItemOwnershipStatus(bundleItem.ItemType, bundleItem.ProductData))
			{
			case ItemOwnershipStatus.IGNORED:
			case ItemOwnershipStatus.UNOWNED:
				if (bundleItem.ItemType != ProductType.PRODUCT_TYPE_HIDDEN_LICENSE)
				{
					num2++;
					continue;
				}
				continue;
			case ItemOwnershipStatus.OWNED:
				num++;
				if (StoreManager.GetProductItemPurchaseRule(bundleItem.ItemType, bundleItem.ProductData) == ItemPurchaseRule.BLOCKING)
				{
					num3++;
					continue;
				}
				continue;
			}
			num4++;
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
			global::Log.Store.PrintWarning("Product {0} ({1}) contains no buyable or owned items; are the licensed configured?", new object[]
			{
				bundle.PMTProductID,
				bundle.DisplayName
			});
			return ProductAvailability.UNDEFINED;
		}
		if (checkRange)
		{
			ProductAvailabilityRange bundleAvailabilityRange = this.GetBundleAvailabilityRange(bundle);
			if (bundleAvailabilityRange == null)
			{
				global::Logger store = global::Log.Store;
				string format = "Product is assigned to an unknown sale or event timing: PMT ID = {0}, Product Name = [{1}], event timing = {2}, Sale ID = {3}";
				object[] array = new object[4];
				array[0] = bundle.PMTProductID;
				array[1] = ((bundle.DisplayName != null) ? bundle.DisplayName.GetString(true) : "");
				array[2] = ((bundle.ProductEvent != null) ? bundle.ProductEvent : "");
				array[3] = string.Join(",", (from id in bundle.SaleIds
				select id.ToString()).ToArray<string>());
				store.PrintWarning(format, array);
				return ProductAvailability.SALE_NOT_ACTIVE;
			}
			if (!bundleAvailabilityRange.IsVisibleAtTime(DateTime.UtcNow))
			{
				return ProductAvailability.SALE_NOT_ACTIVE;
			}
		}
		return ProductAvailability.CAN_PURCHASE;
	}

	// Token: 0x0600662C RID: 26156 RVA: 0x00213510 File Offset: 0x00211710
	public bool IsProductAlreadyOwned(Network.Bundle bundle)
	{
		return this.GetNetworkBundleProductAvailability(bundle, true, false) == ProductAvailability.ALREADY_OWNED;
	}

	// Token: 0x0600662D RID: 26157 RVA: 0x0021351E File Offset: 0x0021171E
	public bool IsProductPrePurchase(Network.Bundle bundle)
	{
		return bundle != null && bundle.IsPrePurchase;
	}

	// Token: 0x0600662E RID: 26158 RVA: 0x0021352C File Offset: 0x0021172C
	public bool IsProductFirstPurchaseBundle(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return false;
		}
		if (this.GetProductsInItemList(bundle.Items).Contains(ProductType.PRODUCT_TYPE_HIDDEN_LICENSE))
		{
			Network.BundleItem bundleItem = bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_HIDDEN_LICENSE && obj.ProductData == 40);
			if (bundleItem != null)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600662F RID: 26159 RVA: 0x00213584 File Offset: 0x00211784
	public static bool DoesBundleContainProduct(Network.Bundle bundle, ProductType product, int productData = 0, int numItemsRequired = 0)
	{
		if (numItemsRequired != 0 && bundle.Items.Count != numItemsRequired)
		{
			return false;
		}
		foreach (Network.BundleItem bundleItem in bundle.Items)
		{
			if (bundleItem.ItemType == product && (productData == 0 || bundleItem.ProductData == productData))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006630 RID: 26160 RVA: 0x00213600 File Offset: 0x00211800
	public bool TryGetBonusProductBundleId(ProductType productType, out long pmtId)
	{
		pmtId = 0L;
		List<Network.Bundle> allBundlesForProduct = this.GetAllBundlesForProduct(productType, false, 0, 0, true);
		List<Network.Bundle> list = (allBundlesForProduct != null) ? allBundlesForProduct.ToList<Network.Bundle>() : null;
		if (list == null || list.Count == 0)
		{
			return false;
		}
		Network.Bundle bundle = (from bonusBundle in list
		where bonusBundle.IsPrePurchase
		select bonusBundle).FirstOrDefault(new Func<Network.Bundle, bool>(this.IsProductAlreadyOwned));
		if (bundle != null && bundle.PMTProductID != null)
		{
			pmtId = bundle.PMTProductID.Value;
			return true;
		}
		IEnumerable<Network.Bundle> regularBundles = from bonusBundle in list
		where !bonusBundle.IsPrePurchase
		where this.IsProductAlreadyOwned(bonusBundle) || this.CanBuyBundle(bonusBundle)
		select bonusBundle;
		Network.Bundle bundle2 = (from product in this.m_sections.SelectMany((Network.ShopSection section) => section.Products)
		select regularBundles.FirstOrDefault(delegate(Network.Bundle bundle)
		{
			long? pmtproductID = bundle.PMTProductID;
			long pmtId2 = product.PmtId;
			return pmtproductID.GetValueOrDefault() == pmtId2 & pmtproductID != null;
		})).FirstOrDefault((Network.Bundle bundle) => bundle != null);
		if (bundle2 == null || bundle2.PMTProductID == null)
		{
			return false;
		}
		pmtId = bundle2.PMTProductID.Value;
		return true;
	}

	// Token: 0x06006631 RID: 26161 RVA: 0x0021376B File Offset: 0x0021196B
	public IEnumerable<Network.Bundle> EnumerateBundlesForProductType(ProductType product, bool requireRealMoneyOption, int productData = 0, int numItemsRequired = 0, bool checkAvailability = true)
	{
		foreach (Network.Bundle bundle in this.m_bundles.Values)
		{
			if ((!requireRealMoneyOption || ShopUtils.BundleHasPrice(bundle, global::CurrencyType.REAL_MONEY)) && StoreManager.DoesBundleContainProduct(bundle, product, productData, numItemsRequired) && (!checkAvailability || this.IsBundleAvailableNow(bundle)))
			{
				yield return bundle;
			}
		}
		global::Map<long, Network.Bundle>.ValueCollection.Enumerator enumerator = default(global::Map<long, Network.Bundle>.ValueCollection.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06006632 RID: 26162 RVA: 0x002137A0 File Offset: 0x002119A0
	public List<Network.Bundle> GetAllBundlesForProduct(ProductType product, bool requireRealMoneyOption, int productData = 0, int numItemsRequired = 0, bool checkAvailability = true)
	{
		return this.EnumerateBundlesForProductType(product, requireRealMoneyOption, productData, numItemsRequired, checkAvailability).ToList<Network.Bundle>();
	}

	// Token: 0x06006633 RID: 26163 RVA: 0x002137B4 File Offset: 0x002119B4
	public Network.Bundle GetLowestCostBundle(ProductType product, bool requireRealMoneyOption, int productData, int numItemsRequired = 0)
	{
		List<Network.Bundle> allBundlesForProduct = StoreManager.Get().GetAllBundlesForProduct(product, requireRealMoneyOption, productData, numItemsRequired, true);
		Network.Bundle bundle = null;
		foreach (Network.Bundle bundle2 in allBundlesForProduct)
		{
			if (numItemsRequired == 0 || bundle2.Items.Count == numItemsRequired)
			{
				if (bundle == null)
				{
					bundle = bundle2;
				}
				else
				{
					ulong? cost = bundle.Cost;
					ulong? cost2 = bundle2.Cost;
					if (!(cost.GetValueOrDefault() <= cost2.GetValueOrDefault() & (cost != null & cost2 != null)))
					{
						bundle = bundle2;
					}
				}
			}
		}
		return bundle;
	}

	// Token: 0x06006634 RID: 26164 RVA: 0x00213860 File Offset: 0x00211A60
	public List<Network.Bundle> GetAvailableBundlesForProduct(ProductType productType, bool requireNonGoldPriceOption, int productData = 0, int numItemsRequired = 0)
	{
		List<Network.Bundle> list = new List<Network.Bundle>();
		Func<Network.BundleItem, bool> <>9__0;
		foreach (Network.Bundle bundle in this.m_bundles.Values)
		{
			if ((numItemsRequired == 0 || bundle.Items.Count == numItemsRequired) && (!requireNonGoldPriceOption || ShopUtils.BundleHasNonGoldPrice(bundle)))
			{
				IEnumerable<Network.BundleItem> items = bundle.Items;
				Func<Network.BundleItem, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((Network.BundleItem item) => item.ItemType == productType && (productData == 0 || productData == item.ProductData)));
				}
				if (items.Any(predicate) && this.CanBuyBundle(bundle))
				{
					list.Add(bundle);
				}
			}
		}
		return list;
	}

	// Token: 0x06006635 RID: 26165 RVA: 0x00213928 File Offset: 0x00211B28
	private List<Network.Bundle> GetAllBundlesContainingItem(ProductType productType, int productData)
	{
		List<Network.Bundle> list = new List<Network.Bundle>();
		foreach (Network.Bundle bundle in this.m_bundles.Values)
		{
			bool flag = false;
			foreach (Network.BundleItem bundleItem in bundle.Items)
			{
				if (bundleItem.ItemType == productType && bundleItem.ProductData == productData)
				{
					flag = true;
				}
			}
			if (flag)
			{
				list.Add(bundle);
			}
		}
		return list;
	}

	// Token: 0x06006636 RID: 26166 RVA: 0x002139E0 File Offset: 0x00211BE0
	public bool GetAvailableAdventureBundle(AdventureDbId adventureId, bool requireNonGoldOption, out Network.Bundle bundle)
	{
		bundle = null;
		if (StoreManager.GetAdventureProductType(adventureId) == ProductType.PRODUCT_TYPE_UNKNOWN)
		{
			return false;
		}
		List<Network.Bundle> availableBundlesForProduct;
		if (adventureId != AdventureDbId.NAXXRAMAS)
		{
			if (adventureId != AdventureDbId.BRM)
			{
				if (adventureId != AdventureDbId.LOE)
				{
					int finalAdventureWing = AdventureUtils.GetFinalAdventureWing((int)adventureId, false, false);
					availableBundlesForProduct = this.GetAvailableBundlesForProduct(ProductType.PRODUCT_TYPE_WING, requireNonGoldOption, finalAdventureWing, 0);
				}
				else
				{
					availableBundlesForProduct = this.GetAvailableBundlesForProduct(ProductType.PRODUCT_TYPE_LOE, requireNonGoldOption, 14, 0);
				}
			}
			else
			{
				availableBundlesForProduct = this.GetAvailableBundlesForProduct(ProductType.PRODUCT_TYPE_BRM, requireNonGoldOption, 10, 0);
			}
		}
		else
		{
			availableBundlesForProduct = this.GetAvailableBundlesForProduct(ProductType.PRODUCT_TYPE_NAXX, requireNonGoldOption, 5, 0);
		}
		if (availableBundlesForProduct != null)
		{
			foreach (Network.Bundle bundle2 in availableBundlesForProduct)
			{
				int count = bundle2.Items.Count;
				if (count != 0 && (!requireNonGoldOption || ShopUtils.BundleHasNonGoldPrice(bundle2)) && this.IsBundleAvailableNow(bundle2))
				{
					if (bundle == null)
					{
						bundle = bundle2;
					}
					else if (bundle.Items.Count <= count)
					{
						bundle = bundle2;
					}
				}
			}
		}
		return bundle != null;
	}

	// Token: 0x06006637 RID: 26167 RVA: 0x00213ACC File Offset: 0x00211CCC
	public bool CanBuyStorePackWithGold(StorePackId storePackId)
	{
		return storePackId.Type == StorePackType.BOOSTER && this.CanBuyBoosterWithGold(storePackId.Id);
	}

	// Token: 0x06006638 RID: 26168 RVA: 0x00213AE8 File Offset: 0x00211CE8
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
		return eventType != SpecialEventType.UNKNOWN && (eventType == SpecialEventType.IGNORE || SpecialEventManager.Get().IsEventActive(eventType, false));
	}

	// Token: 0x06006639 RID: 26169 RVA: 0x00213B38 File Offset: 0x00211D38
	public bool IsBoosterPreorderActive(int storePackIdData, ProductType productType, out Network.Bundle preOrderBundle)
	{
		foreach (Network.Bundle bundle in this.GetAllBundlesForProduct(productType, true, storePackIdData, 0, true))
		{
			if (this.IsProductPrePurchase(bundle))
			{
				preOrderBundle = bundle;
				return true;
			}
		}
		preOrderBundle = null;
		return false;
	}

	// Token: 0x0600663A RID: 26170 RVA: 0x00213BA0 File Offset: 0x00211DA0
	public bool IsBoosterHiddenLicenseBundle(StorePackId storePackId, out Network.Bundle hiddenLicenseBundle)
	{
		if (!GameUtils.IsHiddenLicenseBundleBooster(storePackId))
		{
			hiddenLicenseBundle = null;
			return false;
		}
		IEnumerable<Network.Bundle> source = this.EnumerateBundlesForProductType(ProductType.PRODUCT_TYPE_HIDDEN_LICENSE, true, GameUtils.GetProductDataFromStorePackId(storePackId, 0), 0, true);
		hiddenLicenseBundle = source.FirstOrDefault<Network.Bundle>();
		return hiddenLicenseBundle != null;
	}

	// Token: 0x0600663B RID: 26171 RVA: 0x00213BDC File Offset: 0x00211DDC
	public bool GetHeroBundleByCardDbId(int heroCardDbId, out Network.Bundle heroBundle)
	{
		foreach (Network.Bundle bundle in this.GetAllBundlesContainingItem(ProductType.PRODUCT_TYPE_HERO, heroCardDbId))
		{
			bool flag = false;
			using (List<Network.BundleItem>.Enumerator enumerator2 = bundle.Items.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.ItemType == ProductType.PRODUCT_TYPE_HIDDEN_LICENSE)
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				heroBundle = bundle;
				return true;
			}
		}
		heroBundle = null;
		return false;
	}

	// Token: 0x0600663C RID: 26172 RVA: 0x00213C84 File Offset: 0x00211E84
	public bool IsKoreanCustomer()
	{
		return this.m_currency.SubRegion == 3;
	}

	// Token: 0x0600663D RID: 26173 RVA: 0x00213C94 File Offset: 0x00211E94
	public bool IsEuropeanCustomer()
	{
		return this.m_currency.SubRegion == 2 || this.m_currency.SubRegion == 10;
	}

	// Token: 0x0600663E RID: 26174 RVA: 0x00213CB8 File Offset: 0x00211EB8
	public bool IsNorthAmericanCustomer()
	{
		return this.m_currency.SubRegion == 1;
	}

	// Token: 0x0600663F RID: 26175 RVA: 0x00213CCC File Offset: 0x00211ECC
	public string GetTaxText()
	{
		switch (this.m_currency.TaxText)
		{
		case global::Currency.Tax.TAX_ADDED:
			return GameStrings.Get("GLUE_STORE_SUMMARY_TAX_DISCLAIMER_USD");
		case global::Currency.Tax.NO_TAX:
			return string.Empty;
		}
		return GameStrings.Get("GLUE_STORE_SUMMARY_TAX_DISCLAIMER");
	}

	// Token: 0x06006640 RID: 26176 RVA: 0x00213D14 File Offset: 0x00211F14
	public int GetCurrencyChangedVersion()
	{
		return this.m_currency.ChangedVersion;
	}

	// Token: 0x06006641 RID: 26177 RVA: 0x00213D21 File Offset: 0x00211F21
	public string GetCurrencyCode()
	{
		return this.m_currency.Code;
	}

	// Token: 0x06006642 RID: 26178 RVA: 0x00213D30 File Offset: 0x00211F30
	public CurrencyCache GetCurrencyCache(global::CurrencyType currencyType)
	{
		CurrencyCache currencyCache;
		if (!this.m_currencyCaches.TryGetValue(currencyType, out currencyCache))
		{
			currencyCache = new CurrencyCache(currencyType);
			this.m_currencyCaches.Add(currencyType, currencyCache);
		}
		return currencyCache;
	}

	// Token: 0x06006643 RID: 26179 RVA: 0x00213D64 File Offset: 0x00211F64
	public string FormatCostBundle(Network.Bundle bundle)
	{
		if (bundle.Cost == null)
		{
			return string.Empty;
		}
		if (StoreManager.HasExternalStore)
		{
			string productPrice = this.GetProductPrice(bundle.PMTProductID.ToString());
			if (!string.IsNullOrEmpty(productPrice))
			{
				return productPrice;
			}
		}
		return this.FormatCost(bundle.CostDisplay);
	}

	// Token: 0x06006644 RID: 26180 RVA: 0x00213DC4 File Offset: 0x00211FC4
	public string FormatCost(double? costDisplay)
	{
		string format = this.m_currency.GetFormat();
		CultureInfo cultureInfo = Localization.GetCultureInfo();
		cultureInfo.NumberFormat.CurrencySymbol = " " + this.m_currency.Symbol + " ";
		return string.Format(cultureInfo, format, costDisplay).Replace("  ", " ").Trim();
	}

	// Token: 0x06006645 RID: 26181 RVA: 0x00213E28 File Offset: 0x00212028
	public string GetProductName(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return string.Empty;
		}
		if (bundle.DisplayName != null && !string.IsNullOrEmpty(bundle.DisplayName.GetString(true)))
		{
			return bundle.DisplayName.GetString(true);
		}
		if (bundle.Items.Count == 1)
		{
			Network.BundleItem item = bundle.Items[0];
			return this.GetSingleItemProductName(item);
		}
		return this.GetMultiItemProductName(bundle);
	}

	// Token: 0x06006646 RID: 26182 RVA: 0x00213E90 File Offset: 0x00212090
	public int GetWingItemCount(List<Network.BundleItem> items)
	{
		int num = 0;
		using (List<Network.BundleItem>.Enumerator enumerator = items.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (AdventureUtils.IsProductTypeAnAdventureWing(enumerator.Current.ItemType))
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06006647 RID: 26183 RVA: 0x00213EEC File Offset: 0x002120EC
	public string GetProductQuantityText(ProductType product, int productData, int quantity, int baseQuantity)
	{
		string result = string.Empty;
		if (product != ProductType.PRODUCT_TYPE_BOOSTER)
		{
			if (product != ProductType.PRODUCT_TYPE_DRAFT)
			{
				if (product != ProductType.PRODUCT_TYPE_CURRENCY)
				{
					global::Log.Store.PrintWarning(string.Format("StoreManager.GetProductQuantityText(): don't know how to format quantity for product {0} (data {1})", product, productData), Array.Empty<object>());
				}
				else
				{
					result = GameStrings.Format("GLUE_STORE_QUANTITY_DUST", new object[]
					{
						quantity
					});
				}
			}
			else
			{
				result = GameStrings.Format("GLUE_STORE_SUMMARY_ITEM_ORDERED", new object[]
				{
					quantity,
					GameStrings.Get("GLUE_STORE_PRODUCT_NAME_FORGE_TICKET")
				});
			}
		}
		else if (baseQuantity > 0)
		{
			int num = Math.Max(quantity - baseQuantity, 0);
			result = GameStrings.Format("GLUE_STORE_QUANTITY_PACK_PLUS_BONUS", new object[]
			{
				baseQuantity,
				num
			});
		}
		else
		{
			result = GameStrings.Format("GLUE_STORE_QUANTITY_PACK", new object[]
			{
				quantity
			});
		}
		return result;
	}

	// Token: 0x06006648 RID: 26184 RVA: 0x00213FD4 File Offset: 0x002121D4
	public void StartGeneralTransaction()
	{
		this.StartGeneralTransaction(StoreManager.s_defaultStoreMode);
	}

	// Token: 0x06006649 RID: 26185 RVA: 0x00213FE8 File Offset: 0x002121E8
	public void StartGeneralTransaction(GeneralStoreMode mode)
	{
		if (this.m_waitingToShowStore)
		{
			global::Log.Store.Print("StoreManager.StartGeneralTransaction(): already waiting to show store", Array.Empty<object>());
			return;
		}
		this.m_currentShopType = ShopType.GENERAL_STORE;
		this.m_showStoreData.exitCallback = null;
		this.m_showStoreData.exitCallbackUserData = null;
		this.m_showStoreData.isTotallyFake = false;
		this.m_showStoreData.storeProduct = ProductType.PRODUCT_TYPE_UNKNOWN;
		this.m_showStoreData.storeProductData = 0;
		this.m_showStoreData.storeMode = mode;
		this.m_showStoreData.useOverlayUI = true;
		this.m_showStoreData.closeOnTransactionComplete = false;
		this.ShowStoreWhenLoaded();
	}

	// Token: 0x0600664A RID: 26186 RVA: 0x00214080 File Offset: 0x00212280
	public void StartArenaTransaction(Store.ExitCallback exitCallback, object exitCallbackUserData, bool isTotallyFake)
	{
		if (this.m_waitingToShowStore)
		{
			global::Log.Store.Print("StoreManager.StartArenaTransaction(): already waiting to show store", Array.Empty<object>());
			return;
		}
		this.m_currentShopType = ShopType.ARENA_STORE;
		this.m_showStoreData.exitCallback = exitCallback;
		this.m_showStoreData.exitCallbackUserData = null;
		this.m_showStoreData.isTotallyFake = isTotallyFake;
		this.m_showStoreData.storeProduct = ProductType.PRODUCT_TYPE_UNKNOWN;
		this.m_showStoreData.storeProductData = 0;
		this.m_showStoreData.useOverlayUI = false;
		this.m_showStoreData.closeOnTransactionComplete = false;
		this.ShowStoreWhenLoaded();
	}

	// Token: 0x0600664B RID: 26187 RVA: 0x0021410C File Offset: 0x0021230C
	public void StartTavernBrawlTransaction(Store.ExitCallback exitCallback, bool isTotallyFake)
	{
		if (this.m_waitingToShowStore)
		{
			global::Log.Store.Print("StoreManager.StartTavernBrawlTransaction(): already waiting to show store", Array.Empty<object>());
			return;
		}
		this.m_currentShopType = ShopType.TAVERN_BRAWL_STORE;
		this.m_showStoreData.exitCallback = exitCallback;
		this.m_showStoreData.exitCallbackUserData = null;
		this.m_showStoreData.isTotallyFake = isTotallyFake;
		this.m_showStoreData.storeProduct = ProductType.PRODUCT_TYPE_UNKNOWN;
		this.m_showStoreData.storeProductData = 0;
		this.m_showStoreData.useOverlayUI = false;
		this.m_showStoreData.closeOnTransactionComplete = false;
		this.ShowStoreWhenLoaded();
	}

	// Token: 0x0600664C RID: 26188 RVA: 0x00214198 File Offset: 0x00212398
	public void StartAdventureTransaction(ProductType product, int productData, Store.ExitCallback exitCallback, object exitCallbackUserData, ShopType shopType, int numItemsRequired = 0, bool useOverlayUI = false, IDataModel dataModel = null, int pmtProductId = 0)
	{
		if (this.m_waitingToShowStore)
		{
			global::Log.Store.Print("StoreManager.StartAdventureTransaction(): already waiting to show store", Array.Empty<object>());
			return;
		}
		if (!this.CanBuyProductItem(product, productData))
		{
			global::Log.Store.PrintWarning("StoreManager.StartAdventureTransaction(): cannot buy product item", Array.Empty<object>());
			return;
		}
		this.m_currentShopType = shopType;
		this.m_showStoreData.exitCallback = exitCallback;
		this.m_showStoreData.exitCallbackUserData = exitCallbackUserData;
		this.m_showStoreData.isTotallyFake = false;
		this.m_showStoreData.storeProduct = product;
		this.m_showStoreData.storeProductData = productData;
		this.m_showStoreData.numItemsRequired = numItemsRequired;
		this.m_showStoreData.dataModel = dataModel;
		this.m_showStoreData.useOverlayUI = useOverlayUI;
		this.m_showStoreData.pmtProductId = pmtProductId;
		this.m_showStoreData.closeOnTransactionComplete = false;
		this.ShowStoreWhenLoaded();
	}

	// Token: 0x0600664D RID: 26189 RVA: 0x0021426C File Offset: 0x0021246C
	public void SetupDuelsStore(DuelsPopupManager duelsPopupManager)
	{
		this.m_currentShopType = ShopType.DUELS_STORE;
		this.m_showStoreData.isTotallyFake = false;
		this.m_showStoreData.storeProduct = ProductType.PRODUCT_TYPE_UNKNOWN;
		this.m_showStoreData.storeProductData = 0;
		this.m_showStoreData.useOverlayUI = false;
		this.m_showStoreData.closeOnTransactionComplete = true;
		this.m_stores[ShopType.DUELS_STORE] = duelsPopupManager;
		this.SetupLoadedStore(duelsPopupManager);
		if (this.m_view.HasStartedLoading)
		{
			this.ShowStore();
			return;
		}
		this.m_showStoreStart = Time.realtimeSinceStartup;
		this.m_waitingToShowStore = true;
		this.m_view.LoadAssets();
	}

	// Token: 0x0600664E RID: 26190 RVA: 0x00214301 File Offset: 0x00212501
	public void ShutDownDuelsStore()
	{
		if (this.m_stores.ContainsKey(ShopType.DUELS_STORE))
		{
			this.m_stores.Remove(ShopType.DUELS_STORE);
		}
	}

	// Token: 0x0600664F RID: 26191 RVA: 0x00214320 File Offset: 0x00212520
	public void SetupCardBackStore(CardBackInfoManager cardBackInfoManager, int productData)
	{
		this.m_currentShopType = ShopType.CARD_BACK_STORE;
		this.m_showStoreData.isTotallyFake = false;
		this.m_showStoreData.storeProduct = ProductType.PRODUCT_TYPE_CARD_BACK;
		this.m_showStoreData.storeProductData = productData;
		this.m_showStoreData.useOverlayUI = false;
		this.m_showStoreData.closeOnTransactionComplete = true;
		this.m_stores[ShopType.CARD_BACK_STORE] = cardBackInfoManager;
		this.SetupLoadedStore(cardBackInfoManager);
		if (this.m_view.HasStartedLoading)
		{
			this.ShowStore();
			return;
		}
		this.m_showStoreStart = Time.realtimeSinceStartup;
		this.m_waitingToShowStore = true;
		this.m_view.LoadAssets();
	}

	// Token: 0x06006650 RID: 26192 RVA: 0x002143B5 File Offset: 0x002125B5
	public void ShutDownCardBackStore()
	{
		if (this.m_stores.ContainsKey(ShopType.CARD_BACK_STORE))
		{
			this.m_stores.Remove(ShopType.CARD_BACK_STORE);
		}
	}

	// Token: 0x06006651 RID: 26193 RVA: 0x002143D4 File Offset: 0x002125D4
	public void SetupHeroSkinStore(HeroSkinInfoManager heroSkinInfoManager, int productData)
	{
		this.m_currentShopType = ShopType.HERO_SKIN_STORE;
		this.m_showStoreData.isTotallyFake = false;
		this.m_showStoreData.storeProduct = ProductType.PRODUCT_TYPE_HERO;
		this.m_showStoreData.storeProductData = productData;
		this.m_showStoreData.useOverlayUI = false;
		this.m_showStoreData.closeOnTransactionComplete = true;
		this.m_stores[ShopType.HERO_SKIN_STORE] = heroSkinInfoManager;
		this.SetupLoadedStore(heroSkinInfoManager);
		if (this.m_view.HasStartedLoading)
		{
			this.ShowStore();
			return;
		}
		this.m_showStoreStart = Time.realtimeSinceStartup;
		this.m_waitingToShowStore = true;
		this.m_view.LoadAssets();
	}

	// Token: 0x06006652 RID: 26194 RVA: 0x00214469 File Offset: 0x00212669
	public void ShutDownHeroSkinStore()
	{
		if (this.m_stores.ContainsKey(ShopType.HERO_SKIN_STORE))
		{
			this.m_stores.Remove(ShopType.HERO_SKIN_STORE);
		}
	}

	// Token: 0x06006653 RID: 26195 RVA: 0x00214488 File Offset: 0x00212688
	public void HandleDisconnect()
	{
		if (this.IsShown() && !this.TransactionInProgress())
		{
			while (this.IsPromptShowing)
			{
				Navigation.GoBack();
			}
			IStore currentStore = this.GetCurrentStore();
			if (currentStore != null)
			{
				currentStore.Close();
			}
			DialogManager.Get().ShowReconnectHelperDialog(null, null);
		}
		this.FireStatusChangedEventIfNeeded();
	}

	// Token: 0x06006654 RID: 26196 RVA: 0x002144D8 File Offset: 0x002126D8
	public void HideStore(ShopType shopType)
	{
		IStore store = this.GetStore(shopType);
		if (store != null)
		{
			store.Close();
			this.m_view.Hide();
			BnetBar bnetBar = BnetBar.Get();
			if (bnetBar == null)
			{
				return;
			}
			bnetBar.RefreshCurrency();
		}
	}

	// Token: 0x06006655 RID: 26197 RVA: 0x00214510 File Offset: 0x00212710
	public bool TransactionInProgress()
	{
		return this.Status != StoreManager.TransactionStatus.READY;
	}

	// Token: 0x1700060E RID: 1550
	// (get) Token: 0x06006656 RID: 26198 RVA: 0x0021451E File Offset: 0x0021271E
	public bool IsPromptShowing
	{
		get
		{
			return this.m_view.IsPromptShowing || this.IsHearthstoneCheckoutUIShowing();
		}
	}

	// Token: 0x06006657 RID: 26199 RVA: 0x00214538 File Offset: 0x00212738
	public bool HasOutstandingPurchaseNotices(ProductType product)
	{
		foreach (NetCache.ProfileNoticePurchase profileNoticePurchase in this.m_outstandingPurchaseNotices.ToArray())
		{
			if (profileNoticePurchase.PMTProductID != null)
			{
				Network.Bundle bundleFromPmtProductId = this.GetBundleFromPmtProductId(profileNoticePurchase.PMTProductID.Value);
				if (bundleFromPmtProductId != null)
				{
					using (List<Network.BundleItem>.Enumerator enumerator = bundleFromPmtProductId.Items.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.ItemType == product)
							{
								return true;
							}
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06006658 RID: 26200 RVA: 0x002145E0 File Offset: 0x002127E0
	public static ProductType GetAdventureProductType(AdventureDbId adventureId)
	{
		ProductType result;
		if (StoreManager.s_adventureToProductMap.TryGetValue(adventureId, out result))
		{
			return result;
		}
		if (GameUtils.IsExpansionAdventure(adventureId))
		{
			return ProductType.PRODUCT_TYPE_WING;
		}
		return ProductType.PRODUCT_TYPE_UNKNOWN;
	}

	// Token: 0x06006659 RID: 26201 RVA: 0x00214609 File Offset: 0x00212809
	public bool IsIdActiveTransaction(long id)
	{
		return this.m_activeMoneyOrGTAPPTransaction != null && id == this.m_activeMoneyOrGTAPPTransaction.ID;
	}

	// Token: 0x0600665A RID: 26202 RVA: 0x00214624 File Offset: 0x00212824
	public bool IsPMTProductIDActiveTransaction(long id)
	{
		if (this.m_activeMoneyOrGTAPPTransaction == null)
		{
			return false;
		}
		long? pmtproductID = this.m_activeMoneyOrGTAPPTransaction.PMTProductID;
		return id == pmtproductID.GetValueOrDefault() & pmtproductID != null;
	}

	// Token: 0x0600665B RID: 26203 RVA: 0x0021465C File Offset: 0x0021285C
	public static bool IsFirstPurchaseBundleOwned()
	{
		HiddenLicenseDbfRecord record = GameDbf.HiddenLicense.GetRecord(40);
		if (record == null)
		{
			return false;
		}
		AccountLicenseDbfRecord record2 = GameDbf.AccountLicense.GetRecord(record.AccountLicenseId);
		return record2 != null && AccountLicenseMgr.Get().OwnsAccountLicense(record2.LicenseId);
	}

	// Token: 0x0600665C RID: 26204 RVA: 0x002146A4 File Offset: 0x002128A4
	private static StoreManager.LicenseStatus GetHiddenLicenseStatus(int hiddenLicenseId)
	{
		HiddenLicenseDbfRecord record = GameDbf.HiddenLicense.GetRecord(hiddenLicenseId);
		if (record == null)
		{
			return StoreManager.LicenseStatus.UNDEFINED;
		}
		AccountLicenseDbfRecord record2 = GameDbf.AccountLicense.GetRecord(record.AccountLicenseId);
		if (record2 == null)
		{
			return StoreManager.LicenseStatus.UNDEFINED;
		}
		if (!AccountLicenseMgr.Get().OwnsAccountLicense(record2.LicenseId))
		{
			return StoreManager.LicenseStatus.NOT_OWNED;
		}
		if (!record.IsBlocking)
		{
			return StoreManager.LicenseStatus.OWNED;
		}
		return StoreManager.LicenseStatus.OWNED_AND_BLOCKING;
	}

	// Token: 0x0600665D RID: 26205 RVA: 0x002146F8 File Offset: 0x002128F8
	public static bool IsHiddenLicenseBundleOwned(int hiddenLicenseId)
	{
		StoreManager.LicenseStatus hiddenLicenseStatus = StoreManager.GetHiddenLicenseStatus(hiddenLicenseId);
		return hiddenLicenseStatus == StoreManager.LicenseStatus.OWNED || hiddenLicenseStatus == StoreManager.LicenseStatus.OWNED_AND_BLOCKING;
	}

	// Token: 0x0600665E RID: 26206 RVA: 0x00214716 File Offset: 0x00212916
	public void SetCurrentlySelectedStorePack(StorePackId storePackId)
	{
		this.m_currentlySelectedId = storePackId;
	}

	// Token: 0x0600665F RID: 26207 RVA: 0x00214720 File Offset: 0x00212920
	private ModularBundleLayoutDbfRecord GetRegionNodeLayoutForHiddenLicense(int hiddenLicenseId)
	{
		foreach (ModularBundleLayoutDbfRecord modularBundleLayoutDbfRecord in GameDbf.ModularBundleLayout.GetRecords())
		{
			if (modularBundleLayoutDbfRecord.HiddenLicenseId == hiddenLicenseId)
			{
				string[] array = modularBundleLayoutDbfRecord.Regions.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					if (global::EnumUtils.SafeParse<constants.BnetRegion>(array[i], constants.BnetRegion.REGION_UNKNOWN, false) == this.m_regionId)
					{
						return modularBundleLayoutDbfRecord;
					}
				}
			}
		}
		global::Log.Store.PrintWarning(string.Format("Unable to load layout for hidden license id={0}, region={1}. Using Default Node Layout.", hiddenLicenseId, this.m_regionId), Array.Empty<object>());
		return GameDbf.ModularBundleLayout.GetRecord((ModularBundleLayoutDbfRecord r) => r.ModularBundleId == hiddenLicenseId);
	}

	// Token: 0x06006660 RID: 26208 RVA: 0x00214814 File Offset: 0x00212A14
	public List<ModularBundleLayoutDbfRecord> GetRegionNodeLayoutsForBundle(int modularBundleRecordId)
	{
		List<ModularBundleLayoutDbfRecord> list = new List<ModularBundleLayoutDbfRecord>();
		foreach (ModularBundleLayoutDbfRecord modularBundleLayoutDbfRecord in GameDbf.ModularBundleLayout.GetRecords())
		{
			if (modularBundleLayoutDbfRecord.ModularBundleId == modularBundleRecordId)
			{
				string[] array = modularBundleLayoutDbfRecord.Regions.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					if (global::EnumUtils.SafeParse<constants.BnetRegion>(array[i], constants.BnetRegion.REGION_UNKNOWN, false) == this.m_regionId)
					{
						list.Add(modularBundleLayoutDbfRecord);
					}
				}
			}
		}
		if (list.Count == 0)
		{
			global::Log.Store.PrintWarning(string.Format("Unable to load layout for modular bundle id={0}, region={1}. Using Default Node Layout.", modularBundleRecordId, this.m_regionId), Array.Empty<object>());
			list.Add(GameDbf.ModularBundleLayout.GetRecord((ModularBundleLayoutDbfRecord r) => r.ModularBundleId == modularBundleRecordId));
		}
		return list;
	}

	// Token: 0x06006661 RID: 26209 RVA: 0x00214920 File Offset: 0x00212B20
	private void ShowStoreWhenLoaded()
	{
		this.m_showStoreStart = Time.realtimeSinceStartup;
		HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
		if (hearthstonePerformance != null)
		{
			hearthstonePerformance.StartPerformanceFlow(new global::FlowPerformanceShop.ShopSetupConfig
			{
				shopType = this.m_currentShopType
			});
		}
		this.m_waitingToShowStore = true;
		if (!this.IsCurrentStoreLoaded())
		{
			this.Load(this.m_currentShopType);
			return;
		}
		this.ShowStore();
	}

	// Token: 0x06006662 RID: 26210 RVA: 0x0021497C File Offset: 0x00212B7C
	private void ShowStore()
	{
		if (!this.m_licenseAchievesListenerRegistered)
		{
			AchieveManager.Get().RegisterLicenseAddedAchievesUpdatedListener(new AchieveManager.LicenseAddedAchievesUpdatedCallback(this.OnLicenseAddedAchievesUpdated));
			this.m_licenseAchievesListenerRegistered = true;
		}
		if (StoreManager.TransactionStatus.READY == this.Status && AchieveManager.Get().HasActiveLicenseAddedAchieves())
		{
			this.Status = StoreManager.TransactionStatus.WAIT_ZERO_COST_LICENSE;
		}
		IStore currentStore = this.GetCurrentStore();
		bool flag = true;
		bool flag2 = false;
		switch (this.m_currentShopType)
		{
		case ShopType.GENERAL_STORE:
			if (this.IsOpen(true))
			{
				if (this.IsVintageStoreEnabled())
				{
					((GeneralStore)currentStore).SetMode(this.m_showStoreData.storeMode);
				}
			}
			else
			{
				global::Log.Store.PrintWarning("StoreManager.ShowStore(): Cannot show general store.. Store is not open", Array.Empty<object>());
				if (this.m_showStoreData.exitCallback != null)
				{
					this.m_showStoreData.exitCallback(false, this.m_showStoreData.exitCallbackUserData);
				}
				flag = false;
			}
			break;
		case ShopType.ADVENTURE_STORE:
		case ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET:
		case ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET:
			if (this.IsOpen(true))
			{
				AdventureStore adventureStore = (AdventureStore)currentStore;
				if (adventureStore != null)
				{
					adventureStore.SetAdventureProduct(this.m_showStoreData.storeProduct, this.m_showStoreData.storeProductData, this.m_showStoreData.numItemsRequired, this.m_showStoreData.pmtProductId);
				}
			}
			else
			{
				global::Log.Store.PrintWarning("StoreManager.ShowStore(): Cannot show adventure store.. Store is not open", Array.Empty<object>());
				if (this.m_showStoreData.exitCallback != null)
				{
					this.m_showStoreData.exitCallback(false, this.m_showStoreData.exitCallbackUserData);
				}
				flag = false;
				flag2 = true;
			}
			break;
		case ShopType.DUELS_STORE:
			if (!this.IsOpen(true))
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
			this.m_waitingToShowStore = false;
			return;
		}
		if (flag && currentStore != null)
		{
			Store store;
			if ((store = (currentStore as Store)) != null)
			{
				store.Show(this.m_showStoreData.isTotallyFake, this.m_showStoreData.useOverlayUI, this.m_showStoreData.dataModel);
			}
			else
			{
				currentStore.Open();
			}
		}
		bool blocked = false;
		if (currentStore != null)
		{
			currentStore.BlockInterface(blocked);
		}
		global::Log.Store.Print("{0} took {1}s to load", new object[]
		{
			this.m_currentShopType,
			Time.realtimeSinceStartup - this.m_showStoreStart
		});
		this.m_waitingToShowStore = false;
	}

	// Token: 0x06006663 RID: 26211 RVA: 0x00214BF1 File Offset: 0x00212DF1
	private void OnLoginCompleted()
	{
		this.FireStatusChangedEventIfNeeded();
	}

	// Token: 0x06006664 RID: 26212 RVA: 0x00214BFC File Offset: 0x00212DFC
	private bool ShouldEnablePurchaseAuthBackButton(ShopType shopType)
	{
		ShopType currentShopType = this.m_currentShopType;
		return currentShopType == ShopType.ARENA_STORE || currentShopType == ShopType.TAVERN_BRAWL_STORE;
	}

	// Token: 0x06006665 RID: 26213 RVA: 0x00214C20 File Offset: 0x00212E20
	private bool IsCurrentStoreLoaded()
	{
		IStore currentStore = this.GetCurrentStore();
		return currentStore != null && currentStore.IsReady() && this.m_view.IsLoaded;
	}

	// Token: 0x06006666 RID: 26214 RVA: 0x00214C54 File Offset: 0x00212E54
	private void Load(ShopType shopType)
	{
		if (this.GetCurrentStore() != null)
		{
			return;
		}
		switch (shopType)
		{
		case ShopType.GENERAL_STORE:
			if (this.IsVintageStoreEnabled())
			{
				AssetLoader.Get().InstantiatePrefab(ShopPrefabs.ShopPrefab, new PrefabCallback<GameObject>(this.OnGeneralStoreLoaded), null, AssetLoadingOptions.None);
			}
			else
			{
				Shop value = Shop.Get();
				this.m_stores[shopType] = value;
			}
			break;
		case ShopType.ARENA_STORE:
		{
			WidgetInstance arenaStoreWidget = WidgetInstance.Create(ShopPrefabs.ArenaShopPrefab, false);
			arenaStoreWidget.RegisterReadyListener(delegate(object _)
			{
				this.OnArenaStoreLoaded(null, arenaStoreWidget.gameObject, null);
			}, null, true);
			break;
		}
		case ShopType.ADVENTURE_STORE:
		{
			WidgetInstance adventureStoreWidget = WidgetInstance.Create(ShopPrefabs.AdventureShopPrefab, false);
			adventureStoreWidget.RegisterReadyListener(delegate(object _)
			{
				this.OnAdventureStoreLoaded(null, adventureStoreWidget.gameObject, null);
			}, null, true);
			break;
		}
		case ShopType.TAVERN_BRAWL_STORE:
		{
			WidgetInstance brawlStoreWidget = WidgetInstance.Create(ShopPrefabs.TavernBrawlShopPrefab, false);
			brawlStoreWidget.RegisterReadyListener(delegate(object _)
			{
				this.OnBrawlStoreLoaded(null, brawlStoreWidget.gameObject, null);
			}, null, true);
			break;
		}
		case ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET:
		{
			WidgetInstance wingWidget = WidgetInstance.Create("AdventureStorymodeChapterStore.prefab:b797807e5c127af47badd08be121ea16", false);
			wingWidget.RegisterReadyListener(delegate(object _)
			{
				this.OnAdventureWingStoreLoaded(null, wingWidget.gameObject, null);
			}, null, true);
			break;
		}
		case ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET:
		{
			WidgetInstance bookWidget = WidgetInstance.Create("AdventureStorymodeBookStore.prefab:922203a90d48c1d47b2f6813ff72f160", false);
			bookWidget.RegisterReadyListener(delegate(object _)
			{
				this.OnAdventureFullStoreLoaded(null, bookWidget.gameObject, null);
			}, null, true);
			break;
		}
		}
		this.m_view.LoadAssets();
	}

	// Token: 0x06006667 RID: 26215 RVA: 0x00214E20 File Offset: 0x00213020
	private void UnloadAndFreeMemory()
	{
		if (Shop.Get() != null)
		{
			Shop.Get().Unload();
		}
		foreach (KeyValuePair<ShopType, IStore> keyValuePair in this.m_stores)
		{
			IStore value = keyValuePair.Value;
			if (value != null)
			{
				value.Unload();
			}
		}
		this.m_stores.Clear();
		this.m_view.UnloadAssets();
	}

	// Token: 0x1700060F RID: 1551
	// (get) Token: 0x06006668 RID: 26216 RVA: 0x00214EAC File Offset: 0x002130AC
	// (set) Token: 0x06006669 RID: 26217 RVA: 0x00214EB4 File Offset: 0x002130B4
	private StoreManager.TransactionStatus Status
	{
		get
		{
			return this.m_status;
		}
		set
		{
			if (0f == this.m_lastCancelRequestTime && this.m_status == StoreManager.TransactionStatus.UNKNOWN)
			{
				this.m_lastCancelRequestTime = Time.realtimeSinceStartup;
			}
			this.m_status = value;
			this.FireStatusChangedEventIfNeeded();
		}
	}

	// Token: 0x17000610 RID: 1552
	// (get) Token: 0x0600666A RID: 26218 RVA: 0x00214EE3 File Offset: 0x002130E3
	// (set) Token: 0x0600666B RID: 26219 RVA: 0x00214EEB File Offset: 0x002130EB
	private bool FirstNoticesProcessed
	{
		get
		{
			return this.m_firstNoticesProcessed;
		}
		set
		{
			this.m_firstNoticesProcessed = value;
			this.FireStatusChangedEventIfNeeded();
		}
	}

	// Token: 0x17000611 RID: 1553
	// (get) Token: 0x0600666C RID: 26220 RVA: 0x00214EFA File Offset: 0x002130FA
	// (set) Token: 0x0600666D RID: 26221 RVA: 0x00214F02 File Offset: 0x00213102
	private bool BattlePayAvailable
	{
		get
		{
			return this.m_battlePayAvailable;
		}
		set
		{
			this.m_battlePayAvailable = value;
			this.FireStatusChangedEventIfNeeded();
		}
	}

	// Token: 0x17000612 RID: 1554
	// (get) Token: 0x0600666E RID: 26222 RVA: 0x00214F11 File Offset: 0x00213111
	// (set) Token: 0x0600666F RID: 26223 RVA: 0x00214F19 File Offset: 0x00213119
	private bool FeaturesReady
	{
		get
		{
			return this.m_featuresReady;
		}
		set
		{
			this.m_featuresReady = value;
			this.FireStatusChangedEventIfNeeded();
		}
	}

	// Token: 0x17000613 RID: 1555
	// (get) Token: 0x06006670 RID: 26224 RVA: 0x00214F28 File Offset: 0x00213128
	// (set) Token: 0x06006671 RID: 26225 RVA: 0x00214F30 File Offset: 0x00213130
	private bool ConfigLoaded
	{
		get
		{
			return this.m_configLoaded;
		}
		set
		{
			this.m_configLoaded = value;
			this.FireStatusChangedEventIfNeeded();
		}
	}

	// Token: 0x06006672 RID: 26226 RVA: 0x00214F40 File Offset: 0x00213140
	private void FireStatusChangedEventIfNeeded()
	{
		bool flag = this.IsOpen(true);
		if (this.m_openWhenLastEventFired == flag)
		{
			return;
		}
		this.OnStatusChanged(flag);
		this.m_openWhenLastEventFired = flag;
	}

	// Token: 0x06006673 RID: 26227 RVA: 0x00214F72 File Offset: 0x00213172
	private NetCache.NetCacheFeatures GetNetCacheFeatures()
	{
		if (!this.FeaturesReady)
		{
			return null;
		}
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (netObject == null)
		{
			this.FeaturesReady = false;
		}
		return netObject;
	}

	// Token: 0x06006674 RID: 26228 RVA: 0x00214F94 File Offset: 0x00213194
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
		case ProductType.PRODUCT_TYPE_NAXX:
		case ProductType.PRODUCT_TYPE_BRM:
		case ProductType.PRODUCT_TYPE_LOE:
		case ProductType.PRODUCT_TYPE_WING:
		case ProductType.PRODUCT_TYPE_BATTLEGROUNDS_BONUS:
		case ProductType.PRODUCT_TYPE_PROGRESSION_BONUS:
			return ItemPurchaseRule.BLOCKING;
		case ProductType.PRODUCT_TYPE_CARD_BACK:
		case ProductType.PRODUCT_TYPE_HERO:
		case ProductType.PRODUCT_TYPE_MINI_SET:
		case ProductType.PRODUCT_TYPE_SELLABLE_DECK:
			return ItemPurchaseRule.NO_LIMIT;
		case ProductType.PRODUCT_TYPE_HIDDEN_LICENSE:
		case ProductType.PRODUCT_TYPE_FIXED_LICENSE:
			return ItemPurchaseRule.BLOCKING;
		default:
			global::Log.Store.PrintError("No purchase rule defined for product type: {0}", new object[]
			{
				product
			});
			return ItemPurchaseRule.UNDEFINED;
		}
	}

	// Token: 0x06006675 RID: 26229 RVA: 0x00215018 File Offset: 0x00213218
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
		case ProductType.PRODUCT_TYPE_CARD_BACK:
			if (!CardBackManager.Get().IsCardBackOwned(productData))
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
			string text = GameUtils.TranslateDbIdToCardId(productData, false);
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
			switch (StoreManager.GetHiddenLicenseStatus(productData))
			{
			case StoreManager.LicenseStatus.NOT_OWNED:
				return ItemOwnershipStatus.UNOWNED;
			case StoreManager.LicenseStatus.OWNED:
			case StoreManager.LicenseStatus.OWNED_AND_BLOCKING:
				return ItemOwnershipStatus.OWNED;
			}
			return ItemOwnershipStatus.UNDEFINED;
		case ProductType.PRODUCT_TYPE_FIXED_LICENSE:
			if (AccountLicenseMgr.Get().FixedLicensesState != AccountLicenseMgr.LicenseUpdateState.SUCCESS)
			{
				return ItemOwnershipStatus.UNDEFINED;
			}
			if (!AccountLicenseMgr.Get().OwnsAccountLicense((long)productData))
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

	// Token: 0x06006676 RID: 26230 RVA: 0x00215160 File Offset: 0x00213360
	private string GetSingleItemProductName(Network.BundleItem item)
	{
		string empty = string.Empty;
		switch (item.ItemType)
		{
		case ProductType.PRODUCT_TYPE_BOOSTER:
		{
			string text = GameDbf.Booster.GetRecord(item.ProductData).Name;
			return GameStrings.Format("GLUE_STORE_PRODUCT_NAME_PACK", new object[]
			{
				item.Quantity,
				text
			});
		}
		case ProductType.PRODUCT_TYPE_DRAFT:
			return GameStrings.Get("GLUE_STORE_PRODUCT_NAME_FORGE_TICKET");
		case ProductType.PRODUCT_TYPE_NAXX:
		case ProductType.PRODUCT_TYPE_BRM:
		case ProductType.PRODUCT_TYPE_LOE:
		case ProductType.PRODUCT_TYPE_WING:
			return AdventureProgressMgr.GetWingName(item.ProductData);
		case ProductType.PRODUCT_TYPE_CARD_BACK:
		{
			CardBackDbfRecord record = GameDbf.CardBack.GetRecord(item.ProductData);
			if (record != null)
			{
				return record.Name;
			}
			return empty;
		}
		case ProductType.PRODUCT_TYPE_HERO:
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(item.ProductData, true);
			if (entityDef != null)
			{
				return entityDef.GetName();
			}
			return empty;
		}
		case ProductType.PRODUCT_TYPE_TAVERN_BRAWL_TICKET:
		{
			TavernBrawlTicketDbfRecord record2 = GameDbf.TavernBrawlTicket.GetRecord(item.ProductData);
			if (record2 != null)
			{
				return record2.StoreName;
			}
			return empty;
		}
		case ProductType.PRODUCT_TYPE_BATTLEGROUNDS_BONUS:
			return GameStrings.Get("GLUE_STORE_PRODUCT_NAME_BATTLEGROUNDS_BONUS");
		case ProductType.PRODUCT_TYPE_PROGRESSION_BONUS:
			return GameStrings.Get("GLUE_STORE_PRODUCT_NAME_PROGRESSION_BONUS");
		}
		global::Log.Store.PrintWarning(string.Format("StoreManager.GetSingleItemProductName(): don't know how to format name for bundle product {0}", item.ItemType), Array.Empty<object>());
		return empty;
	}

	// Token: 0x06006677 RID: 26231 RVA: 0x002152D4 File Offset: 0x002134D4
	private string GetMultiItemProductName(Network.Bundle bundle)
	{
		HashSet<ProductType> productsInItemList = this.GetProductsInItemList(bundle.Items);
		if (productsInItemList.Contains(ProductType.PRODUCT_TYPE_NAXX))
		{
			return GameStrings.Format("GLUE_STORE_PRODUCT_NAME_NAXX_WING_BUNDLE", new object[]
			{
				bundle.Items.Count
			});
		}
		if (productsInItemList.Contains(ProductType.PRODUCT_TYPE_BRM))
		{
			if (productsInItemList.Contains(ProductType.PRODUCT_TYPE_CARD_BACK))
			{
				return GameStrings.Get("GLUE_STORE_PRODUCT_NAME_BRM_PRESALE_BUNDLE");
			}
			return GameStrings.Format("GLUE_STORE_PRODUCT_NAME_BRM_WING_BUNDLE", new object[]
			{
				bundle.Items.Count
			});
		}
		else
		{
			if (productsInItemList.Contains(ProductType.PRODUCT_TYPE_LOE))
			{
				return GameStrings.Format("GLUE_STORE_PRODUCT_NAME_LOE_WING_BUNDLE", new object[]
				{
					bundle.Items.Count
				});
			}
			if (!productsInItemList.Contains(ProductType.PRODUCT_TYPE_WING))
			{
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
					ModularBundleLayoutDbfRecord regionNodeLayoutForHiddenLicense = this.GetRegionNodeLayoutForHiddenLicense(bundleItem.ProductData);
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
						return this.GetSingleItemProductName(bundleItem2);
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
						return GameStrings.Format("GLUE_STORE_PRODUCT_NAME_DUST", new object[]
						{
							bundleItem4.Quantity,
							bundleItem3.Quantity,
							text
						});
					}
				}
				string text2 = string.Empty;
				foreach (Network.BundleItem bundleItem5 in bundle.Items)
				{
					text2 += string.Format("[Product={0},ProductData={1},Quantity={2}],", bundleItem5.ItemType, bundleItem5.ProductData, bundleItem5.Quantity);
				}
				global::Log.Store.PrintWarning("StoreManager.GetMultiItemProductName(): don't know how to format product name for items '" + text2 + "'", Array.Empty<object>());
				return string.Empty;
			}
			int num = (from r in bundle.Items
			where r.ItemType == ProductType.PRODUCT_TYPE_WING
			select r.ProductData).FirstOrDefault<int>();
			if (num == 0)
			{
				global::Log.Store.PrintError("StoreManager.GetMultiItemProductName: bundle with PRODUCT_TYPE_WING did not contain a valid wing ID in any of its product data.", Array.Empty<object>());
			}
			string adventureProductStringKey = GameUtils.GetAdventureProductStringKey(num);
			if (productsInItemList.Contains(ProductType.PRODUCT_TYPE_CARD_BACK))
			{
				return GameStrings.Get("GLUE_STORE_PRODUCT_NAME_" + adventureProductStringKey + "_PRESALE_BUNDLE");
			}
			int num2 = bundle.Items.Count((Network.BundleItem x) => x.ItemType == ProductType.PRODUCT_TYPE_WING);
			return GameStrings.Format("GLUE_STORE_PRODUCT_NAME_" + adventureProductStringKey + "_WING_BUNDLE", new object[]
			{
				num2
			});
		}
	}

	// Token: 0x06006678 RID: 26232 RVA: 0x0021580C File Offset: 0x00213A0C
	private bool GetBoosterGoldCostNoGTAPP(int boosterID, out long cost)
	{
		cost = 0L;
		if (!this.m_goldCostBooster.ContainsKey(boosterID))
		{
			return false;
		}
		if (!this.CanBuyBoosterWithGold(boosterID))
		{
			return false;
		}
		Network.GoldCostBooster goldCostBooster = this.m_goldCostBooster[boosterID];
		if (goldCostBooster.Cost == null)
		{
			return false;
		}
		if (goldCostBooster.Cost.Value <= 0L)
		{
			return false;
		}
		cost = goldCostBooster.Cost.Value;
		return true;
	}

	// Token: 0x06006679 RID: 26233 RVA: 0x0021587C File Offset: 0x00213A7C
	private bool GetArenaGoldCostNoGTAPP(out long cost)
	{
		cost = 0L;
		if (this.m_goldCostArena == null)
		{
			return false;
		}
		cost = this.m_goldCostArena.Value;
		return true;
	}

	// Token: 0x0600667A RID: 26234 RVA: 0x0021589F File Offset: 0x00213A9F
	private bool AutoCancelPurchaseIfNeeded(float now)
	{
		return now - this.m_lastCancelRequestTime >= this.m_secsBeforeAutoCancel && this.AutoCancelPurchaseIfPossible();
	}

	// Token: 0x0600667B RID: 26235 RVA: 0x002158BC File Offset: 0x00213ABC
	private bool AutoCancelPurchaseIfPossible()
	{
		MoneyOrGTAPPTransaction activeMoneyOrGTAPPTransaction = this.m_activeMoneyOrGTAPPTransaction;
		if (activeMoneyOrGTAPPTransaction == null || activeMoneyOrGTAPPTransaction.Provider == null)
		{
			return false;
		}
		if (BattlePayProvider.BP_PROVIDER_BLIZZARD == this.m_activeMoneyOrGTAPPTransaction.Provider.Value)
		{
			if (!this.IsSimpleCheckoutFeatureEnabled() || this.m_activeMoneyOrGTAPPTransaction.IsGTAPP)
			{
				StoreManager.TransactionStatus status = this.Status;
				if (status - StoreManager.TransactionStatus.IN_PROGRESS_MONEY <= 1 || status - StoreManager.TransactionStatus.WAIT_METHOD_OF_PAYMENT <= 4)
				{
					global::Log.Store.Print("StoreManager.AutoCancelPurchaseIfPossible() canceling Blizzard purchase, status={0}", new object[]
					{
						this.Status
					});
					this.Status = StoreManager.TransactionStatus.AUTO_CANCELING;
					this.m_lastCancelRequestTime = Time.realtimeSinceStartup;
					Network.Get().CancelBlizzardPurchase(true, null, null);
					return true;
				}
			}
			else if (this.Status != StoreManager.TransactionStatus.IN_PROGRESS_BLIZZARD_CHECKOUT)
			{
				HearthstoneCheckout hearthstoneCheckout;
				if (HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout))
				{
					hearthstoneCheckout.RequestClose();
				}
				this.Status = StoreManager.TransactionStatus.READY;
				this.m_lastCancelRequestTime = Time.realtimeSinceStartup;
				this.m_activeMoneyOrGTAPPTransaction = null;
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600667C RID: 26236 RVA: 0x002159B0 File Offset: 0x00213BB0
	private void CancelBlizzardPurchase(CancelPurchase.CancelReason? reason = null, string errorMessage = null)
	{
		global::Log.Store.Print("StoreManager.CancelBlizzardPurchase() reason=", new object[]
		{
			(reason != null) ? reason.Value.ToString() : "null"
		});
		this.Status = StoreManager.TransactionStatus.USER_CANCELING;
		this.m_lastCancelRequestTime = Time.realtimeSinceStartup;
		Network.Get().CancelBlizzardPurchase(false, reason, errorMessage);
	}

	// Token: 0x0600667D RID: 26237 RVA: 0x00215A1A File Offset: 0x00213C1A
	private bool HaveProductsToSell()
	{
		return this.m_bundles.Count > 0 || this.m_goldCostBooster.Count > 0 || this.m_goldCostArena != null;
	}

	// Token: 0x0600667E RID: 26238 RVA: 0x00215A48 File Offset: 0x00213C48
	public bool IsBundleAvailableNow(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return false;
		}
		ProductAvailabilityRange bundleAvailabilityRange = this.GetBundleAvailabilityRange(bundle);
		return bundleAvailabilityRange != null && bundleAvailabilityRange.IsBuyableAtTime(DateTime.UtcNow);
	}

	// Token: 0x0600667F RID: 26239 RVA: 0x00215A78 File Offset: 0x00213C78
	public ProductAvailabilityRange GetBundleAvailabilityRange(Network.Bundle bundle)
	{
		if (this.m_ignoreProductTiming)
		{
			return new ProductAvailabilityRange();
		}
		ProductAvailabilityRange productAvailabilityRange = null;
		if (!string.IsNullOrEmpty(bundle.ProductEvent))
		{
			SpecialEventType eventType = SpecialEventManager.GetEventType(bundle.ProductEvent);
			if (eventType == SpecialEventType.UNKNOWN)
			{
				return null;
			}
			if (eventType == SpecialEventType.SPECIAL_EVENT_NEVER)
			{
				productAvailabilityRange = new ProductAvailabilityRange(bundle.ProductEvent, null, null);
				productAvailabilityRange.IsNever = true;
				return productAvailabilityRange;
			}
			if (eventType != SpecialEventType.IGNORE)
			{
				DateTime? startUtc;
				DateTime? endUtc;
				if (!SpecialEventManager.Get().GetEventRangeUtc(eventType, out startUtc, out endUtc))
				{
					return null;
				}
				productAvailabilityRange = new ProductAvailabilityRange(bundle.ProductEvent, startUtc, endUtc);
				if (productAvailabilityRange.IsNever)
				{
					return productAvailabilityRange;
				}
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
			foreach (int key in bundle.SaleIds)
			{
				Network.ShopSale shopSale;
				this.m_sales.TryGetValue(key, out shopSale);
				if (shopSale != null)
				{
					ProductAvailabilityRange productAvailabilityRange3 = new ProductAvailabilityRange(shopSale);
					TimeSpan timeSpan;
					TimeSpan timeSpan2;
					if (productAvailabilityRange2 == null)
					{
						productAvailabilityRange2 = productAvailabilityRange3;
					}
					else if (ProductAvailabilityRange.AreOverlapping(productAvailabilityRange2, productAvailabilityRange3))
					{
						productAvailabilityRange2.UnionWith(productAvailabilityRange3);
					}
					else if (!productAvailabilityRange2.TryGetTimeDisplacementRequiredToBeBuyable(utcNow, out timeSpan))
					{
						productAvailabilityRange2 = productAvailabilityRange3;
					}
					else if (productAvailabilityRange3.TryGetTimeDisplacementRequiredToBeBuyable(utcNow, out timeSpan2) && Math.Abs(timeSpan2.Ticks) <= Math.Abs(timeSpan.Ticks))
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

	// Token: 0x06006680 RID: 26240 RVA: 0x00215BFC File Offset: 0x00213DFC
	private bool DoesBundleContainDust(Network.Bundle bundle)
	{
		bool result;
		if (bundle == null)
		{
			result = (null != null);
		}
		else
		{
			result = (bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_CURRENCY) != null);
		}
		return result;
	}

	// Token: 0x06006681 RID: 26241 RVA: 0x00215C33 File Offset: 0x00213E33
	public bool ShouldShowFeaturedDustJar(Network.Bundle bundle)
	{
		return this.m_regionId == constants.BnetRegion.REGION_CN && this.m_currentlySelectedId.Type == StorePackType.BOOSTER && this.DoesBundleContainDust(bundle);
	}

	// Token: 0x06006682 RID: 26242 RVA: 0x00215C55 File Offset: 0x00213E55
	public int DustQuantityInBundle(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return 0;
		}
		Network.BundleItem bundleItem = bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_CURRENCY);
		if (bundleItem == null)
		{
			return 0;
		}
		return bundleItem.Quantity;
	}

	// Token: 0x06006683 RID: 26243 RVA: 0x00215C91 File Offset: 0x00213E91
	public int DustBaseQuantityInBundle(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return 0;
		}
		Network.BundleItem bundleItem = bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_CURRENCY);
		if (bundleItem == null)
		{
			return 0;
		}
		return bundleItem.BaseQuantity;
	}

	// Token: 0x06006684 RID: 26244 RVA: 0x00215CCD File Offset: 0x00213ECD
	public int PackQuantityInBundle(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return 0;
		}
		Network.BundleItem bundleItem = bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_BOOSTER);
		if (bundleItem == null)
		{
			return 0;
		}
		return bundleItem.Quantity;
	}

	// Token: 0x06006685 RID: 26245 RVA: 0x00215D09 File Offset: 0x00213F09
	private void OnStoreOpen()
	{
		if (BnetBar.Get() != null)
		{
			BnetBar.Get().RefreshCurrency();
		}
		Action onStoreShown = this.OnStoreShown;
		if (onStoreShown == null)
		{
			return;
		}
		onStoreShown();
	}

	// Token: 0x06006686 RID: 26246 RVA: 0x00215D34 File Offset: 0x00213F34
	private void OnStoreExit(bool authorizationBackButtonPressed, object userData)
	{
		Store.ExitCallback exitCallback = this.m_showStoreData.exitCallback;
		if (exitCallback != null)
		{
			exitCallback(authorizationBackButtonPressed, userData);
		}
		if (this.m_activeMoneyOrGTAPPTransaction != null)
		{
			this.m_activeMoneyOrGTAPPTransaction.ClosedStore = true;
		}
		if (this.m_view.ChallengePrompt.IsLoaded && !this.m_view.ChallengePrompt.Cancel(new Action<string>(this.OnChallengeCancel)))
		{
			this.AutoCancelPurchaseIfPossible();
		}
		this.UnblockStoreInterface();
		this.m_view.Hide();
		this.OnStoreHidden();
		if (BnetBar.Get() != null)
		{
			BnetBar.Get().RefreshCurrency();
		}
		HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
		if (hearthstonePerformance == null)
		{
			return;
		}
		hearthstonePerformance.StopCurrentFlow();
	}

	// Token: 0x06006687 RID: 26247 RVA: 0x00215DE6 File Offset: 0x00213FE6
	private void OnStoreInfo(object userData)
	{
		this.ShowStoreInfo();
	}

	// Token: 0x06006688 RID: 26248 RVA: 0x00215DEE File Offset: 0x00213FEE
	public void ShowStoreInfo()
	{
		this.BlockStoreInterface();
		this.m_view.SendToBam.Show(null, StoreSendToBAM.BAMReason.PAYMENT_INFO, "", false);
	}

	// Token: 0x06006689 RID: 26249 RVA: 0x00215E10 File Offset: 0x00214010
	public bool CanBuyBundle(Network.Bundle bundleToBuy)
	{
		if (bundleToBuy == null)
		{
			global::Log.Store.PrintWarning("Null bundle passed to CanBuyBundle!", Array.Empty<object>());
			return false;
		}
		if (AchieveManager.Get() == null || !AchieveManager.Get().IsReady())
		{
			return false;
		}
		if (bundleToBuy.Items.Count < 1)
		{
			global::Log.Store.PrintWarning(string.Format("Attempting to buy bundle {0}, which does not contain any items!", bundleToBuy.PMTProductID), Array.Empty<object>());
			return false;
		}
		if (!this.IsBundleAvailableNow(bundleToBuy))
		{
			return false;
		}
		foreach (Network.BundleItem bundleItem in bundleToBuy.Items)
		{
			if (!this.CanBuyProductItem(bundleItem.ItemType, bundleItem.ProductData))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600668A RID: 26250 RVA: 0x00215EE4 File Offset: 0x002140E4
	private bool CanBuyProductItem(ProductType product, int productData)
	{
		if (AchieveManager.Get() == null || !AchieveManager.Get().IsReady())
		{
			return false;
		}
		switch (StoreManager.GetProductItemPurchaseRule(product, productData))
		{
		case ItemPurchaseRule.UNDEFINED:
		case ItemPurchaseRule.NO_LIMIT:
			return true;
		case ItemPurchaseRule.BLOCKING:
			return StoreManager.GetProductItemOwnershipStatus(product, productData) == ItemOwnershipStatus.UNOWNED;
		default:
			return true;
		}
	}

	// Token: 0x0600668B RID: 26251 RVA: 0x00215F34 File Offset: 0x00214134
	private void OnStoreBuyWithMoney(BuyPmtProductEventArgs args)
	{
		if (TemporaryAccountManager.IsTemporaryAccount() && !this.IsSoftAccountPurchasingEnabled())
		{
			TemporaryAccountManager.Get().ShowHealUpDialog(GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_01"), GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_02"), TemporaryAccountManager.HealUpReason.REAL_MONEY, true, null);
			return;
		}
		Network.Bundle bundleFromPmtProductId = this.GetBundleFromPmtProductId(args.pmtProductId);
		if (bundleFromPmtProductId == null)
		{
			global::Log.Store.PrintError("OnStoreBuyWithMoney failed: bundle not found for pmtProductID = {0}.", new object[]
			{
				args.pmtProductId
			});
		}
		if (!this.CanBuyBundle(bundleFromPmtProductId))
		{
			global::Log.Store.PrintError("OnStoreBuyWithMoney failed: CanBuyBundle is false for pmtProductID = {0}.", new object[]
			{
				args.pmtProductId
			});
			return;
		}
		if (this.IsSimpleCheckoutFeatureEnabled())
		{
			this.OnStoreBuyWithCheckout(args);
			return;
		}
		if (this.IsCheckoutFallbackSupported())
		{
			this.SetCanTapOutConfirmationUI(true);
			this.BlockStoreInterface();
			this.SetActiveMoneyOrGTAPPTransaction((long)StoreManager.UNKNOWN_TRANSACTION_ID, new long?(args.pmtProductId), new BattlePayProvider?(BattlePayProvider.BP_PROVIDER_BLIZZARD), false, false);
			this.m_lastCancelRequestTime = Time.realtimeSinceStartup;
			this.m_view.PurchaseAuth.Show(this.m_activeMoneyOrGTAPPTransaction, false, false);
			this.Status = StoreManager.TransactionStatus.WAIT_METHOD_OF_PAYMENT;
			Network.Get().GetPurchaseMethod(new long?(args.pmtProductId), args.quantity, this.m_currency);
		}
	}

	// Token: 0x0600668C RID: 26252 RVA: 0x00216064 File Offset: 0x00214264
	private void OnStoreBuyWithGTAPP(BuyPmtProductEventArgs args)
	{
		Network.Bundle bundleFromPmtProductId = this.GetBundleFromPmtProductId(args.pmtProductId);
		if (!this.CanBuyBundle(bundleFromPmtProductId))
		{
			global::Log.Store.PrintError("Purchase with GTAPP failed (PMT product ID = {0}): CanBuyProductItem is false.", new object[]
			{
				args.pmtProductId
			});
			return;
		}
		this.SetCanTapOutConfirmationUI(true);
		this.BlockStoreInterface();
		this.SetActiveMoneyOrGTAPPTransaction((long)StoreManager.UNKNOWN_TRANSACTION_ID, new long?(args.pmtProductId), new BattlePayProvider?(BattlePayProvider.BP_PROVIDER_BLIZZARD), true, false);
		this.Status = StoreManager.TransactionStatus.WAIT_METHOD_OF_PAYMENT;
		this.m_lastCancelRequestTime = Time.realtimeSinceStartup;
		this.m_view.PurchaseAuth.Show(this.m_activeMoneyOrGTAPPTransaction, false, false);
		Network.Get().GetPurchaseMethod(new long?(args.pmtProductId), args.quantity, global::Currency.GTAPP);
	}

	// Token: 0x0600668D RID: 26253 RVA: 0x00216124 File Offset: 0x00214324
	private void OnStoreBuyWithGoldNoGTAPP(NoGTAPPTransactionData noGTAPPtransactionData)
	{
		if (noGTAPPtransactionData == null)
		{
			global::Log.Store.PrintError("Purchase failed: null transaction data.", Array.Empty<object>());
			return;
		}
		if (!this.CanBuyProductItem(noGTAPPtransactionData.Product, noGTAPPtransactionData.ProductData))
		{
			global::Log.Store.PrintError("Purchase direct with gold (no GTAPP) failed: CanBuyProductItem is false.", Array.Empty<object>());
			return;
		}
		this.BlockStoreInterface();
		this.m_view.PurchaseAuth.Show(null, this.ShouldEnablePurchaseAuthBackButton(this.m_currentShopType), false);
		this.Status = StoreManager.TransactionStatus.IN_PROGRESS_GOLD_NO_GTAPP;
		Network.Get().PurchaseViaGold(noGTAPPtransactionData.Quantity, noGTAPPtransactionData.Product, noGTAPPtransactionData.ProductData);
	}

	// Token: 0x0600668E RID: 26254 RVA: 0x002161BC File Offset: 0x002143BC
	private void OnStoreBuyWithCheckout(BuyPmtProductEventArgs args)
	{
		if (this.GetBundleFromPmtProductId(args.pmtProductId) == null)
		{
			global::Log.Store.PrintError("Cannot buy product PMT ID = {0}. Bundle not found.", new object[]
			{
				args.pmtProductId
			});
			return;
		}
		if (!this.IsSimpleCheckoutFeatureEnabled())
		{
			global::Log.Store.PrintError("Purchase failed: Checkout feature is disabled.", Array.Empty<object>());
			return;
		}
		HearthstoneCheckout hearthstoneCheckout;
		if (!HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout))
		{
			global::Log.Store.PrintError("Purchase failed: Checkout service is not available.", Array.Empty<object>());
			return;
		}
		if (args.paymentCurrency == global::CurrencyType.REAL_MONEY)
		{
			if (TemporaryAccountManager.IsTemporaryAccount() && !this.IsSoftAccountPurchasingEnabled())
			{
				TemporaryAccountManager.Get().ShowHealUpDialog(GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_01"), GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_02"), TemporaryAccountManager.HealUpReason.REAL_MONEY, true, null);
				return;
			}
			this.Status = StoreManager.TransactionStatus.WAIT_BLIZZARD_CHECKOUT;
			this.SetActiveMoneyOrGTAPPTransaction((long)StoreManager.UNKNOWN_TRANSACTION_ID, new long?(args.pmtProductId), new BattlePayProvider?(BattlePayProvider.BP_PROVIDER_BLIZZARD), false, false);
			this.m_lastCancelRequestTime = Time.realtimeSinceStartup;
			this.SetCanTapOutConfirmationUI(true);
			this.BlockStoreInterface();
			hearthstoneCheckout.ShowCheckout(args.pmtProductId, ShopUtils.GetCurrencyCode(args.paymentCurrency), (uint)args.quantity);
			if (StoreManager.HasExternalStore)
			{
				this.m_view.PurchaseAuth.Show(this.m_activeMoneyOrGTAPPTransaction, false, false);
				return;
			}
		}
		else
		{
			if (ShopUtils.IsCurrencyVirtual(args.paymentCurrency))
			{
				this.Status = StoreManager.TransactionStatus.WAIT_BLIZZARD_CHECKOUT;
				this.SetActiveMoneyOrGTAPPTransaction((long)StoreManager.UNKNOWN_TRANSACTION_ID, new long?(args.pmtProductId), new BattlePayProvider?(BattlePayProvider.BP_PROVIDER_BLIZZARD), false, false);
				this.m_lastCancelRequestTime = Time.realtimeSinceStartup;
				this.SetCanTapOutConfirmationUI(true);
				this.BlockStoreInterface();
				if (this.m_view.PurchaseAuth.IsShown)
				{
					this.m_view.PurchaseAuth.StartNewTransaction(this.m_activeMoneyOrGTAPPTransaction, false, false);
				}
				else
				{
					this.m_view.PurchaseAuth.Show(this.m_activeMoneyOrGTAPPTransaction, false, false);
				}
				hearthstoneCheckout.PurchaseWithVirtualCurrency(args.pmtProductId, ShopUtils.GetCurrencyCode(args.paymentCurrency), (uint)args.quantity);
				return;
			}
			global::Log.Store.PrintError("Buy with checkout failed: Invalid currency type {0}", new object[]
			{
				args.paymentCurrency
			});
		}
	}

	// Token: 0x0600668F RID: 26255 RVA: 0x002163C8 File Offset: 0x002145C8
	private void OnSummaryConfirm(int quantity, object userData)
	{
		this.m_view.PurchaseAuth.Show(this.m_activeMoneyOrGTAPPTransaction, this.ShouldEnablePurchaseAuthBackButton(this.m_currentShopType), false);
		if (this.m_challengePurchaseMethod != null)
		{
			this.m_view.ChallengePrompt.StartChallenge(this.m_challengePurchaseMethod.ChallengeURL);
			return;
		}
		this.ConfirmPurchase();
	}

	// Token: 0x06006690 RID: 26256 RVA: 0x00216422 File Offset: 0x00214622
	private void ConfirmPurchase()
	{
		this.Status = (this.m_activeMoneyOrGTAPPTransaction.IsGTAPP ? StoreManager.TransactionStatus.IN_PROGRESS_GOLD_GTAPP : StoreManager.TransactionStatus.IN_PROGRESS_MONEY);
		Network.Get().ConfirmPurchase();
	}

	// Token: 0x06006691 RID: 26257 RVA: 0x00216448 File Offset: 0x00214648
	private void OnSummaryCancel(object userData)
	{
		this.CancelBlizzardPurchase(null, null);
		this.UnblockStoreInterface();
	}

	// Token: 0x06006692 RID: 26258 RVA: 0x0021646B File Offset: 0x0021466B
	private void OnSummaryInfo(object userData)
	{
		this.BlockStoreInterface();
		this.AutoCancelPurchaseIfPossible();
		this.m_view.SendToBam.Show(null, StoreSendToBAM.BAMReason.EULA_AND_TOS, string.Empty, false);
	}

	// Token: 0x06006693 RID: 26259 RVA: 0x00216492 File Offset: 0x00214692
	private void OnSummaryPaymentAndTOS(object userData)
	{
		this.AutoCancelPurchaseIfPossible();
		this.m_view.LegalBam.Show();
	}

	// Token: 0x06006694 RID: 26260 RVA: 0x002164AC File Offset: 0x002146AC
	private void OnChallengeComplete(string challengeID, bool isSuccess, CancelPurchase.CancelReason? reason, string internalErrorInfo)
	{
		if (!isSuccess)
		{
			this.OnChallengeCancel_Internal(challengeID, reason, internalErrorInfo);
			return;
		}
		this.m_view.PurchaseAuth.Show(this.m_activeMoneyOrGTAPPTransaction, this.ShouldEnablePurchaseAuthBackButton(this.m_currentShopType), false);
		this.Status = StoreManager.TransactionStatus.CHALLENGE_SUBMITTED;
		this.ConfirmPurchase();
	}

	// Token: 0x06006695 RID: 26261 RVA: 0x002164F8 File Offset: 0x002146F8
	private void OnChallengeCancel(string challengeID)
	{
		this.OnChallengeCancel_Internal(challengeID, null, null);
	}

	// Token: 0x06006696 RID: 26262 RVA: 0x00216518 File Offset: 0x00214718
	private void OnChallengeCancel_Internal(string challengeID, CancelPurchase.CancelReason? reason, string errorMessage)
	{
		Debug.LogFormat("Canceling purchase from challengeId={0} reason={1} msg={2}", new object[]
		{
			challengeID,
			(reason != null) ? reason.Value.ToString() : "null",
			errorMessage
		});
		this.Status = StoreManager.TransactionStatus.CHALLENGE_CANCELED;
		this.CancelBlizzardPurchase(reason, errorMessage);
		this.UnblockStoreInterface();
		this.m_view.Hide();
	}

	// Token: 0x06006697 RID: 26263 RVA: 0x00216586 File Offset: 0x00214786
	private void OnSendToBAMOkay(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, StoreSendToBAM.BAMReason reason)
	{
		if (moneyOrGTAPPTransaction != null)
		{
			this.ConfirmActiveMoneyTransaction(moneyOrGTAPPTransaction.ID);
		}
		if (reason == StoreSendToBAM.BAMReason.PAYMENT_INFO)
		{
			this.UnblockStoreInterface();
			return;
		}
		this.m_view.DoneWithBam.Show();
	}

	// Token: 0x06006698 RID: 26264 RVA: 0x002165B1 File Offset: 0x002147B1
	private void OnSendToBAMCancel(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction)
	{
		if (moneyOrGTAPPTransaction != null)
		{
			this.ConfirmActiveMoneyTransaction(moneyOrGTAPPTransaction.ID);
		}
		this.UnblockStoreInterface();
	}

	// Token: 0x06006699 RID: 26265 RVA: 0x002165C8 File Offset: 0x002147C8
	private void OnSendToBAMLegal(StoreLegalBAMLinks.BAMReason reason)
	{
		this.UnblockStoreInterface();
	}

	// Token: 0x0600669A RID: 26266 RVA: 0x002165D0 File Offset: 0x002147D0
	private void OnAchievesUpdated(List<Achievement> updatedAchives, List<Achievement> completedAchives, object userData)
	{
		this.m_completedAchieves = AchieveManager.Get().GetNewCompletedAchievesToShow();
		this.ShowCompletedAchieve();
	}

	// Token: 0x0600669B RID: 26267 RVA: 0x002165E8 File Offset: 0x002147E8
	private void OnLicenseAddedAchievesUpdated(List<Achievement> activeLicenseAddedAchieves, object userData)
	{
		if (StoreManager.TransactionStatus.WAIT_ZERO_COST_LICENSE != this.Status)
		{
			return;
		}
		if (activeLicenseAddedAchieves.Count > 0)
		{
			return;
		}
		global::Log.Store.Print("StoreManager.OnLicenseAddedAchievesUpdated(): done waiting for licenses!", Array.Empty<object>());
		if (this.IsCurrentStoreLoaded())
		{
			Processor.QueueJob("StoreManager.ShowCompletePurchaseSuccessWhenReady", this.Job_ShowCompletePurchaseSuccessWhenReady(null), Array.Empty<IJobDependency>());
		}
		this.Status = StoreManager.TransactionStatus.READY;
	}

	// Token: 0x0600669C RID: 26268 RVA: 0x00216648 File Offset: 0x00214848
	private void ShowCompletedAchieve()
	{
		bool flag = this.m_completedAchieves.Count == 0;
		if (this.m_currentShopType == ShopType.GENERAL_STORE)
		{
			GeneralStore generalStore = (GeneralStore)this.GetCurrentStore();
			if (generalStore != null)
			{
				generalStore.EnableClickCatcher(flag);
			}
		}
		if (flag)
		{
			return;
		}
		Achievement quest = this.m_completedAchieves[0];
		this.m_completedAchieves.RemoveAt(0);
		QuestToast.ShowQuestToast(UserAttentionBlocker.NONE, delegate(object userData)
		{
			this.ShowCompletedAchieve();
		}, true, quest, false);
	}

	// Token: 0x0600669D RID: 26269 RVA: 0x002166BC File Offset: 0x002148BC
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
			if (moneyOrGTAPPTransaction.ID > 0L)
			{
				this.m_transactionIDsConclusivelyHandled.Add(moneyOrGTAPPTransaction.ID);
			}
			paymentMethod = (moneyOrGTAPPTransaction.IsGTAPP ? PaymentMethod.GOLD_GTAPP : PaymentMethod.MONEY);
			arg = this.GetBundleFromPmtProductId(moneyOrGTAPPTransaction.PMTProductID.GetValueOrDefault());
		}
		if (PaymentMethod.GOLD_NO_GTAPP != paymentMethod)
		{
			this.ConfirmActiveMoneyTransaction(moneyOrGTAPPTransaction.ID);
		}
		if (success)
		{
			this.OnSuccessfulPurchaseAck(arg, paymentMethod);
		}
		else
		{
			this.OnFailedPurchaseAck(arg, paymentMethod);
		}
		this.SetCanTapOutConfirmationUI(true);
		this.UnblockStoreInterface();
		IStore currentStore = this.GetCurrentStore();
		if (this.m_currentShopType == ShopType.ADVENTURE_STORE || this.m_currentShopType == ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET || this.m_currentShopType == ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET)
		{
			currentStore.Close();
		}
		if (this.BattlePayAvailable)
		{
			return;
		}
		if (this.m_currentShopType == ShopType.GENERAL_STORE)
		{
			currentStore.Close();
		}
	}

	// Token: 0x0600669E RID: 26270 RVA: 0x0021678B File Offset: 0x0021498B
	private void OnAuthExit()
	{
		this.OnAuthorizationExit();
	}

	// Token: 0x0600669F RID: 26271 RVA: 0x00216798 File Offset: 0x00214998
	private void BlockStoreInterface()
	{
		IStore currentStore = this.GetCurrentStore();
		if (currentStore == null)
		{
			return;
		}
		currentStore.BlockInterface(true);
	}

	// Token: 0x060066A0 RID: 26272 RVA: 0x002167AB File Offset: 0x002149AB
	private void UnblockStoreInterface()
	{
		IStore currentStore = this.GetCurrentStore();
		if (currentStore == null)
		{
			return;
		}
		currentStore.BlockInterface(false);
	}

	// Token: 0x060066A1 RID: 26273 RVA: 0x002167C0 File Offset: 0x002149C0
	private void HandlePurchaseSuccess(StoreManager.PurchaseErrorSource? source, MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, string thirdPartyID, HearthstoneCheckoutTransactionData checkoutTransactionData)
	{
		this.Status = StoreManager.TransactionStatus.READY;
		this.SendShopPurchaseEventTelemetry(true);
		this.m_pendingProductPurchaseArgs = null;
		PaymentMethod paymentMethod;
		Network.Bundle bundle;
		if (moneyOrGTAPPTransaction == null)
		{
			paymentMethod = PaymentMethod.GOLD_NO_GTAPP;
			bundle = null;
		}
		else
		{
			if (checkoutTransactionData != null && ShopUtils.IsCurrencyVirtual(ShopUtils.GetCurrencyTypeFromCode(checkoutTransactionData.CurrencyCode)))
			{
				paymentMethod = PaymentMethod.VIRTUAL_CURRENCY;
			}
			else
			{
				paymentMethod = (moneyOrGTAPPTransaction.IsGTAPP ? PaymentMethod.GOLD_GTAPP : PaymentMethod.MONEY);
			}
			bundle = this.GetBundleFromPmtProductId(moneyOrGTAPPTransaction.PMTProductID.GetValueOrDefault());
		}
		this.OnSuccessfulPurchase(bundle, paymentMethod);
		if (!this.IsCurrentStoreLoaded())
		{
			return;
		}
		StoreManager.PurchaseErrorSource? purchaseErrorSource = source;
		StoreManager.PurchaseErrorSource purchaseErrorSource2 = StoreManager.PurchaseErrorSource.FROM_PREVIOUS_PURCHASE;
		if (purchaseErrorSource.GetValueOrDefault() == purchaseErrorSource2 & purchaseErrorSource != null)
		{
			this.BlockStoreInterface();
			this.m_view.PurchaseAuth.ShowPreviousPurchaseSuccess(moneyOrGTAPPTransaction, this.ShouldEnablePurchaseAuthBackButton(this.m_currentShopType));
			return;
		}
		this.MarkTransactionCurrenciesAsDirty(paymentMethod, bundle);
		Processor.QueueJob("StoreManager.ShowCompletePurchaseSuccessWhenReady", this.Job_ShowCompletePurchaseSuccessWhenReady(moneyOrGTAPPTransaction), Array.Empty<IJobDependency>());
	}

	// Token: 0x060066A2 RID: 26274 RVA: 0x0021689C File Offset: 0x00214A9C
	private void MarkTransactionCurrenciesAsDirty(PaymentMethod paymentMethod, Network.Bundle bundle)
	{
		if (paymentMethod - PaymentMethod.GOLD_GTAPP > 1)
		{
			if (paymentMethod == PaymentMethod.VIRTUAL_CURRENCY)
			{
				if (bundle != null)
				{
					global::CurrencyType bundleVirtualCurrencyPriceType = ShopUtils.GetBundleVirtualCurrencyPriceType(bundle);
					if (bundleVirtualCurrencyPriceType != global::CurrencyType.NONE)
					{
						this.GetCurrencyCache(bundleVirtualCurrencyPriceType).MarkDirty();
					}
				}
			}
		}
		else
		{
			this.GetCurrencyCache(global::CurrencyType.GOLD).MarkDirty();
		}
		if (bundle != null)
		{
			foreach (Network.BundleItem bundleItem in from i in bundle.Items
			where i.ItemType == ProductType.PRODUCT_TYPE_CURRENCY
			select i)
			{
				global::CurrencyType currencyTypeFromProto = ShopUtils.GetCurrencyTypeFromProto((PegasusShared.CurrencyType)bundleItem.ProductData);
				if (currencyTypeFromProto != global::CurrencyType.NONE)
				{
					this.GetCurrencyCache(currencyTypeFromProto).MarkDirty();
				}
			}
		}
	}

	// Token: 0x060066A3 RID: 26275 RVA: 0x00216958 File Offset: 0x00214B58
	private IEnumerator<IAsyncJobResult> Job_ShowCompletePurchaseSuccessWhenReady(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction)
	{
		DateTime startTime = DateTime.Now;
		double elapsedSeconds = 0.0;
		bool checkCurrency = true;
		while (!this.IsPurchaseSuccessReady(checkCurrency))
		{
			elapsedSeconds = DateTime.Now.Subtract(startTime).TotalSeconds;
			if (checkCurrency && elapsedSeconds > StoreManager.CURRENCY_TRANSACTION_TIMEOUT_SECONDS)
			{
				checkCurrency = false;
			}
			yield return null;
		}
		if (this.m_currencyCaches.Any((KeyValuePair<global::CurrencyType, CurrencyCache> c) => c.Value.NeedsRefresh()))
		{
			global::Log.Store.PrintError("[StoreManager.ShowCompletePurchaseSuccessWhenReady] gave up on waiting for currency balance after {0} seconds", new object[]
			{
				elapsedSeconds
			});
			if (DialogManager.Get() != null)
			{
				AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
				{
					m_text = GameStrings.Format("GLUE_STORE_FAIL_CURRENCY_BALANCE", Array.Empty<object>()),
					m_showAlertIcon = true,
					m_responseDisplay = AlertPopup.ResponseDisplay.OK
				};
				DialogManager.Get().ShowPopup(info);
			}
		}
		this.SetCanTapOutConfirmationUI(false);
		if (this.m_view.IsLoaded)
		{
			this.m_view.PurchaseAuth.CompletePurchaseSuccess(moneyOrGTAPPTransaction);
		}
		yield break;
	}

	// Token: 0x060066A4 RID: 26276 RVA: 0x00216970 File Offset: 0x00214B70
	private bool IsPurchaseSuccessReady(bool checkCurrency = true)
	{
		if (this.Status != StoreManager.TransactionStatus.READY || (!(Shop.Get() == null) && Shop.Get().WillAutoPurchase()))
		{
			return false;
		}
		if (checkCurrency)
		{
			return !this.m_currencyCaches.Any((KeyValuePair<global::CurrencyType, CurrencyCache> c) => c.Value.NeedsRefresh());
		}
		return true;
	}

	// Token: 0x060066A5 RID: 26277 RVA: 0x002169D4 File Offset: 0x00214BD4
	private void HandleFailedRiskError(StoreManager.PurchaseErrorSource source)
	{
		bool flag = StoreManager.TransactionStatus.CHALLENGE_CANCELED == this.Status;
		this.Status = StoreManager.TransactionStatus.READY;
		if (flag)
		{
			global::Log.Store.Print("HandleFailedRiskError for canceled transaction", Array.Empty<object>());
			if (this.m_activeMoneyOrGTAPPTransaction != null)
			{
				this.ConfirmActiveMoneyTransaction(this.m_activeMoneyOrGTAPPTransaction.ID);
			}
			this.UnblockStoreInterface();
			return;
		}
		if (!this.IsCurrentStoreLoaded())
		{
			return;
		}
		if (!this.GetCurrentStore().IsOpen())
		{
			return;
		}
		this.m_view.PurchaseAuth.Hide();
		this.m_view.Summary.Hide();
		this.BlockStoreInterface();
		this.m_view.SendToBam.Show(this.m_activeMoneyOrGTAPPTransaction, StoreSendToBAM.BAMReason.NEED_PASSWORD_RESET, string.Empty, source == StoreManager.PurchaseErrorSource.FROM_PREVIOUS_PURCHASE);
	}

	// Token: 0x060066A6 RID: 26278 RVA: 0x00216A88 File Offset: 0x00214C88
	private void HandleSendToBAMError(StoreManager.PurchaseErrorSource source, StoreSendToBAM.BAMReason reason, string errorCode)
	{
		this.Status = StoreManager.TransactionStatus.READY;
		if (!this.IsCurrentStoreLoaded())
		{
			return;
		}
		if (!this.GetCurrentStore().IsOpen())
		{
			return;
		}
		this.m_view.PurchaseAuth.Hide();
		this.m_view.Summary.Hide();
		this.BlockStoreInterface();
		this.m_view.SendToBam.Show(this.m_activeMoneyOrGTAPPTransaction, reason, errorCode, source == StoreManager.PurchaseErrorSource.FROM_PREVIOUS_PURCHASE);
	}

	// Token: 0x060066A7 RID: 26279 RVA: 0x00216AF8 File Offset: 0x00214CF8
	private void CompletePurchaseFailure(StoreManager.PurchaseErrorSource source, MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, string failDetails, string thirdPartyID, Network.PurchaseErrorInfo.ErrorType error)
	{
		if (!this.IsCurrentStoreLoaded())
		{
			return;
		}
		if (source == StoreManager.PurchaseErrorSource.FROM_PURCHASE_METHOD_RESPONSE)
		{
			if (this.m_view.SendToBam.IsShown)
			{
				return;
			}
			this.BlockStoreInterface();
			this.m_view.PurchaseAuth.ShowPreviousPurchaseFailure(moneyOrGTAPPTransaction, failDetails, this.ShouldEnablePurchaseAuthBackButton(this.m_currentShopType), error);
			return;
		}
		else
		{
			if (source == StoreManager.PurchaseErrorSource.FROM_PREVIOUS_PURCHASE)
			{
				this.BlockStoreInterface();
				this.m_view.PurchaseAuth.ShowPreviousPurchaseFailure(moneyOrGTAPPTransaction, failDetails, this.ShouldEnablePurchaseAuthBackButton(this.m_currentShopType), error);
				return;
			}
			if (this.m_view.PurchaseAuth.CompletePurchaseFailure(moneyOrGTAPPTransaction, failDetails, error))
			{
				return;
			}
			global::Log.Store.PrintWarning("StoreManager.CompletePurchaseFailure(): purchased failed (" + failDetails + ") but the store authorization window has been closed.", Array.Empty<object>());
			this.UnblockStoreInterface();
			return;
		}
	}

	// Token: 0x060066A8 RID: 26280 RVA: 0x00216BB4 File Offset: 0x00214DB4
	private void HandlePurchaseError(StoreManager.PurchaseErrorSource source, Network.PurchaseErrorInfo.ErrorType purchaseErrorType, string purchaseErrorCode, string thirdPartyID, bool isGTAPP)
	{
		if (this.IsConclusiveState(purchaseErrorType) && this.m_activeMoneyOrGTAPPTransaction != null && this.m_transactionIDsConclusivelyHandled.Contains(this.m_activeMoneyOrGTAPPTransaction.ID))
		{
			global::Log.Store.Print("HandlePurchaseError already handled purchase error for conclusive state on transaction (Transaction: {0}, current purchaseErrorType = {1})", new object[]
			{
				this.m_activeMoneyOrGTAPPTransaction,
				purchaseErrorType
			});
			return;
		}
		global::Log.Store.Print(string.Format("HandlePurchaseError source={0} purchaseErrorType={1} purchaseErrorCode={2} thirdPartyID={3}", new object[]
		{
			source,
			purchaseErrorType,
			purchaseErrorCode,
			thirdPartyID
		}), Array.Empty<object>());
		string failDetails = "";
		switch (purchaseErrorType)
		{
		case Network.PurchaseErrorInfo.ErrorType.UNKNOWN:
			global::Log.Store.PrintWarning("StoreManager.HandlePurchaseError: purchase error is UNKNOWN, taking no action on this purchase", Array.Empty<object>());
			return;
		case Network.PurchaseErrorInfo.ErrorType.SUCCESS:
			if (source == StoreManager.PurchaseErrorSource.FROM_PURCHASE_METHOD_RESPONSE)
			{
				global::Log.Store.PrintWarning("StoreManager.HandlePurchaseError: received SUCCESS from payment method purchase error.", Array.Empty<object>());
				return;
			}
			this.HandlePurchaseSuccess(new StoreManager.PurchaseErrorSource?(source), this.m_activeMoneyOrGTAPPTransaction, thirdPartyID, null);
			return;
		case Network.PurchaseErrorInfo.ErrorType.STILL_IN_PROGRESS:
			if (source == StoreManager.PurchaseErrorSource.FROM_PURCHASE_METHOD_RESPONSE)
			{
				global::Log.Store.PrintWarning("StoreManager.HandlePurchaseError: received STILL_IN_PROGRESS from payment method purchase error.", Array.Empty<object>());
				return;
			}
			if (source != StoreManager.PurchaseErrorSource.FROM_PREVIOUS_PURCHASE)
			{
				this.Status = (isGTAPP ? StoreManager.TransactionStatus.IN_PROGRESS_GOLD_GTAPP : StoreManager.TransactionStatus.IN_PROGRESS_MONEY);
			}
			return;
		case Network.PurchaseErrorInfo.ErrorType.INVALID_BNET:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_BNET_ID");
			break;
		case Network.PurchaseErrorInfo.ErrorType.SERVICE_NA:
			if (source != StoreManager.PurchaseErrorSource.FROM_PREVIOUS_PURCHASE)
			{
				if (this.Status != StoreManager.TransactionStatus.UNKNOWN)
				{
					this.BattlePayAvailable = false;
				}
				this.Status = StoreManager.TransactionStatus.UNKNOWN;
			}
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_NO_BATTLEPAY");
			this.CompletePurchaseFailure(source, this.m_activeMoneyOrGTAPPTransaction, failDetails, thirdPartyID, purchaseErrorType);
			return;
		case Network.PurchaseErrorInfo.ErrorType.PURCHASE_IN_PROGRESS:
			if (source != StoreManager.PurchaseErrorSource.FROM_PREVIOUS_PURCHASE)
			{
				this.Status = (isGTAPP ? StoreManager.TransactionStatus.IN_PROGRESS_GOLD_GTAPP : StoreManager.TransactionStatus.IN_PROGRESS_MONEY);
			}
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_IN_PROGRESS");
			this.CompletePurchaseFailure(source, this.m_activeMoneyOrGTAPPTransaction, failDetails, thirdPartyID, purchaseErrorType);
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
			if (source != StoreManager.PurchaseErrorSource.FROM_PREVIOUS_PURCHASE && this.Status != StoreManager.TransactionStatus.UNKNOWN)
			{
				this.BattlePayAvailable = false;
			}
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_NO_BATTLEPAY");
			break;
		case Network.PurchaseErrorInfo.ErrorType.NO_ACTIVE_BPAY:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_NO_ACTIVE_BPAY");
			break;
		case Network.PurchaseErrorInfo.ErrorType.FAILED_RISK:
			this.HandleFailedRiskError(source);
			return;
		case Network.PurchaseErrorInfo.ErrorType.CANCELED:
			if (source != StoreManager.PurchaseErrorSource.FROM_PREVIOUS_PURCHASE)
			{
				this.Status = StoreManager.TransactionStatus.READY;
			}
			return;
		case Network.PurchaseErrorInfo.ErrorType.WAIT_MOP:
			global::Log.Store.Print("StoreManager.HandlePurchaseError: Status is WAIT_MOP.. this probably shouldn't be happening.", Array.Empty<object>());
			if (source != StoreManager.PurchaseErrorSource.FROM_PREVIOUS_PURCHASE)
			{
				if (this.Status == StoreManager.TransactionStatus.UNKNOWN)
				{
					global::Log.Store.Print(string.Format("StoreManager.HandlePurchaseError: Status is WAIT_MOP, previous Status was UNKNOWN, source = {0}", source), Array.Empty<object>());
					return;
				}
				this.Status = StoreManager.TransactionStatus.WAIT_METHOD_OF_PAYMENT;
			}
			return;
		case Network.PurchaseErrorInfo.ErrorType.WAIT_CONFIRM:
			if (source != StoreManager.PurchaseErrorSource.FROM_PREVIOUS_PURCHASE && this.Status == StoreManager.TransactionStatus.UNKNOWN)
			{
				global::Log.Store.Print(string.Format("StoreManager.HandlePurchaseError: Status is WAIT_CONFIRM, previous Status was UNKNOWN, source = {0}. Going to try to cancel the purchase.", source), Array.Empty<object>());
				this.CancelBlizzardPurchase(null, null);
			}
			return;
		case Network.PurchaseErrorInfo.ErrorType.WAIT_RISK:
			if (source != StoreManager.PurchaseErrorSource.FROM_PREVIOUS_PURCHASE)
			{
				global::Log.Store.Print("StoreManager.HandlePurchaseError: Waiting for client to respond to Risk challenge", Array.Empty<object>());
				if (this.Status == StoreManager.TransactionStatus.UNKNOWN)
				{
					global::Log.Store.Print(string.Format("StoreManager.HandlePurchaseError: Status is WAIT_RISK, previous Status was UNKNOWN, source = {0}", source), Array.Empty<object>());
					return;
				}
				if (StoreManager.TransactionStatus.CHALLENGE_SUBMITTED == this.Status || StoreManager.TransactionStatus.CHALLENGE_CANCELED == this.Status)
				{
					global::Log.Store.Print(string.Format("StoreManager.HandlePurchaseError: Status = {0}; ignoring WAIT_RISK purchase error info", this.Status), Array.Empty<object>());
					return;
				}
				this.Status = StoreManager.TransactionStatus.WAIT_RISK;
			}
			return;
		case Network.PurchaseErrorInfo.ErrorType.PRODUCT_NA:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_PRODUCT_NA");
			break;
		case Network.PurchaseErrorInfo.ErrorType.RISK_TIMEOUT:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_CHALLENGE_TIMEOUT");
			break;
		case Network.PurchaseErrorInfo.ErrorType.PRODUCT_ALREADY_OWNED:
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_PRODUCT_ALREADY_OWNED");
			break;
		case Network.PurchaseErrorInfo.ErrorType.WAIT_THIRD_PARTY_RECEIPT:
			global::Log.Store.PrintWarning("StoreManager.HandlePurchaseError: Received WAIT_THIRD_PARTY_RECEIPT response, even though legacy third party purchasing is removed.", Array.Empty<object>());
			return;
		case Network.PurchaseErrorInfo.ErrorType.PRODUCT_EVENT_HAS_ENDED:
			if (this.m_activeMoneyOrGTAPPTransaction != null && this.IsProductPrePurchase(this.GetBundleFromPmtProductId(this.m_activeMoneyOrGTAPPTransaction.PMTProductID.GetValueOrDefault())))
			{
				failDetails = GameStrings.Get("GLUE_STORE_PRE_PURCHASE_HAS_ENDED");
			}
			else
			{
				failDetails = GameStrings.Get("GLUE_STORE_PRODUCT_EVENT_HAS_ENDED");
			}
			break;
		default:
			switch (purchaseErrorType)
			{
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
					this.HandleSendToBAMError(source, reason, purchaseErrorCode);
					if (StoreManager.HasExternalStore)
					{
						this.CompletePurchaseFailure(source, this.m_activeMoneyOrGTAPPTransaction, failDetails, thirdPartyID, purchaseErrorType);
					}
					return;
				}
				failDetails = GameStrings.Get("GLUE_STORE_FAIL_GOLD_GENERIC");
				goto IL_551;
			case Network.PurchaseErrorInfo.ErrorType.BP_INVALID_CC_EXPIRY:
				if (!isGTAPP)
				{
					this.HandleSendToBAMError(source, StoreSendToBAM.BAMReason.CREDIT_CARD_EXPIRED, string.Empty);
					return;
				}
				failDetails = GameStrings.Get("GLUE_STORE_FAIL_GOLD_GENERIC");
				goto IL_551;
			case Network.PurchaseErrorInfo.ErrorType.BP_NO_VALID_PAYMENT:
				if (source == StoreManager.PurchaseErrorSource.FROM_PURCHASE_METHOD_RESPONSE)
				{
					global::Log.Store.PrintWarning("StoreManager.HandlePurchaseError: received BP_NO_VALID_PAYMENT from payment method purchase error.", Array.Empty<object>());
					goto IL_551;
				}
				if (!isGTAPP)
				{
					this.HandleSendToBAMError(source, StoreSendToBAM.BAMReason.NO_VALID_PAYMENT_METHOD, string.Empty);
					return;
				}
				failDetails = GameStrings.Get("GLUE_STORE_FAIL_GOLD_GENERIC");
				goto IL_551;
			case Network.PurchaseErrorInfo.ErrorType.BP_PURCHASE_BAN:
				failDetails = GameStrings.Get("GLUE_STORE_FAIL_PURCHASE_BAN");
				goto IL_551;
			case Network.PurchaseErrorInfo.ErrorType.BP_SPENDING_LIMIT:
				if (!isGTAPP)
				{
					failDetails = GameStrings.Get("GLUE_STORE_FAIL_SPENDING_LIMIT");
					goto IL_551;
				}
				failDetails = GameStrings.Get("GLUE_STORE_FAIL_GOLD_GENERIC");
				goto IL_551;
			case Network.PurchaseErrorInfo.ErrorType.BP_PARENTAL_CONTROL:
				failDetails = GameStrings.Get("GLUE_STORE_FAIL_PARENTAL_CONTROL");
				goto IL_551;
			case Network.PurchaseErrorInfo.ErrorType.BP_THROTTLED:
				failDetails = GameStrings.Get("GLUE_STORE_FAIL_THROTTLED");
				goto IL_551;
			case Network.PurchaseErrorInfo.ErrorType.BP_THIRD_PARTY_BAD_RECEIPT:
			case Network.PurchaseErrorInfo.ErrorType.BP_THIRD_PARTY_RECEIPT_USED:
				failDetails = GameStrings.Get("GLUE_STORE_FAIL_THIRD_PARTY_BAD_RECEIPT");
				goto IL_551;
			case Network.PurchaseErrorInfo.ErrorType.BP_PRODUCT_UNIQUENESS_VIOLATED:
				this.HandleSendToBAMError(source, StoreSendToBAM.BAMReason.PRODUCT_UNIQUENESS_VIOLATED, string.Empty);
				return;
			case Network.PurchaseErrorInfo.ErrorType.BP_REGION_IS_DOWN:
				failDetails = GameStrings.Get("GLUE_STORE_FAIL_REGION_IS_DOWN");
				goto IL_551;
			case Network.PurchaseErrorInfo.ErrorType.E_BP_CHALLENGE_ID_FAILED_VERIFICATION:
				failDetails = GameStrings.Get("GLUE_STORE_FAIL_CHALLENGE_ID_FAILED_VERIFICATION");
				goto IL_551;
			}
			failDetails = GameStrings.Get("GLUE_STORE_FAIL_GENERAL");
			break;
		}
		IL_551:
		if (source != StoreManager.PurchaseErrorSource.FROM_PREVIOUS_PURCHASE)
		{
			this.Status = StoreManager.TransactionStatus.READY;
		}
		this.CompletePurchaseFailure(source, this.m_activeMoneyOrGTAPPTransaction, failDetails, thirdPartyID, purchaseErrorType);
	}

	// Token: 0x060066A9 RID: 26281 RVA: 0x00217130 File Offset: 0x00215330
	private void SetActiveMoneyOrGTAPPTransaction(long id, long? pmtProductID, BattlePayProvider? provider, bool isGTAPP, bool tryToResolvePreviousTransactionNotices)
	{
		MoneyOrGTAPPTransaction moneyOrGTAPPTransaction = new MoneyOrGTAPPTransaction(id, pmtProductID, provider, isGTAPP);
		bool flag = true;
		if (this.m_activeMoneyOrGTAPPTransaction != null)
		{
			if (moneyOrGTAPPTransaction.Equals(this.m_activeMoneyOrGTAPPTransaction))
			{
				flag = (this.m_activeMoneyOrGTAPPTransaction.Provider == null && provider != null);
			}
			else if ((long)StoreManager.UNKNOWN_TRANSACTION_ID != this.m_activeMoneyOrGTAPPTransaction.ID)
			{
				global::Log.Store.PrintWarning(string.Format("StoreManager.SetActiveMoneyOrGTAPPTransaction(id={0}, pmtProductId={1}, isGTAPP={2}, provider={3}) does not match active money or GTAPP transaction '{4}'", new object[]
				{
					id,
					pmtProductID,
					isGTAPP,
					(provider != null) ? provider.Value.ToString() : "UNKNOWN",
					this.m_activeMoneyOrGTAPPTransaction
				}), Array.Empty<object>());
			}
		}
		if (flag)
		{
			global::Log.Store.Print(string.Format("SetActiveMoneyOrGTAPPTransaction() {0}", moneyOrGTAPPTransaction), Array.Empty<object>());
			this.m_activeMoneyOrGTAPPTransaction = moneyOrGTAPPTransaction;
		}
		if (this.m_firstMoneyOrGTAPPTransactionSet)
		{
			return;
		}
		this.m_firstMoneyOrGTAPPTransactionSet = true;
		if (tryToResolvePreviousTransactionNotices)
		{
			this.ResolveFirstMoneyOrGTAPPTransactionIfPossible();
		}
	}

	// Token: 0x060066AA RID: 26282 RVA: 0x00217244 File Offset: 0x00215444
	private void ResolveFirstMoneyOrGTAPPTransactionIfPossible()
	{
		if (!this.m_firstMoneyOrGTAPPTransactionSet)
		{
			return;
		}
		if (!this.FirstNoticesProcessed)
		{
			return;
		}
		if (this.m_activeMoneyOrGTAPPTransaction == null)
		{
			return;
		}
		if (this.m_outstandingPurchaseNotices.Find((NetCache.ProfileNoticePurchase obj) => obj.OriginData == this.m_activeMoneyOrGTAPPTransaction.ID) != null)
		{
			return;
		}
		global::Log.Store.Print(string.Format("StoreManager.ResolveFirstMoneyTransactionIfPossible(): no outstanding notices for transaction {0}; setting m_activeMoneyOrGTAPPTransaction = null", this.m_activeMoneyOrGTAPPTransaction), Array.Empty<object>());
		this.m_activeMoneyOrGTAPPTransaction = null;
	}

	// Token: 0x060066AB RID: 26283 RVA: 0x002172AC File Offset: 0x002154AC
	private void ConfirmActiveMoneyTransaction(long id)
	{
		StoreManager.<>c__DisplayClass268_0 CS$<>8__locals1 = new StoreManager.<>c__DisplayClass268_0();
		CS$<>8__locals1.id = id;
		if (this.m_activeMoneyOrGTAPPTransaction == null || (this.m_activeMoneyOrGTAPPTransaction.ID != (long)StoreManager.UNKNOWN_TRANSACTION_ID && this.m_activeMoneyOrGTAPPTransaction.ID != CS$<>8__locals1.id))
		{
			global::Log.Store.PrintWarning(string.Format("StoreManager.ConfirmActiveMoneyTransaction(id={0}) does not match active money transaction '{1}'", CS$<>8__locals1.id, this.m_activeMoneyOrGTAPPTransaction), Array.Empty<object>());
		}
		global::Log.Store.Print(string.Format("ConfirmActiveMoneyTransaction() {0}", CS$<>8__locals1.id), Array.Empty<object>());
		List<NetCache.ProfileNoticePurchase> list = this.m_outstandingPurchaseNotices.FindAll(new Predicate<NetCache.ProfileNoticePurchase>(CS$<>8__locals1.<ConfirmActiveMoneyTransaction>g__Predicate|0));
		this.m_outstandingPurchaseNotices.RemoveAll(new Predicate<NetCache.ProfileNoticePurchase>(CS$<>8__locals1.<ConfirmActiveMoneyTransaction>g__Predicate|0));
		foreach (NetCache.ProfileNoticePurchase profileNoticePurchase in list)
		{
			Network.Get().AckNotice(profileNoticePurchase.NoticeID);
		}
		this.m_confirmedTransactionIDs.Add(CS$<>8__locals1.id);
		this.m_activeMoneyOrGTAPPTransaction = null;
	}

	// Token: 0x060066AC RID: 26284 RVA: 0x002173D4 File Offset: 0x002155D4
	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		List<long> list = new List<long>();
		foreach (NetCache.ProfileNotice profileNotice in newNotices)
		{
			if (profileNotice.Type == NetCache.ProfileNotice.NoticeType.PURCHASE)
			{
				if (profileNotice.Origin == NetCache.ProfileNotice.NoticeOrigin.PURCHASE_CANCELED)
				{
					global::Log.Store.Print(string.Format("StoreManager.OnNewNotices() ack'ing purchase canceled notice for bpay ID {0}", profileNotice.OriginData), Array.Empty<object>());
					list.Add(profileNotice.NoticeID);
				}
				else if (this.m_confirmedTransactionIDs.Contains(profileNotice.OriginData))
				{
					global::Log.Store.Print(string.Format("StoreManager.OnNewNotices() ack'ing purchase notice for already confirmed bpay ID {0}", profileNotice.OriginData), Array.Empty<object>());
					list.Add(profileNotice.NoticeID);
				}
				else
				{
					NetCache.ProfileNoticePurchase item = profileNotice as NetCache.ProfileNoticePurchase;
					global::Log.Store.Print(string.Format("StoreManager.OnNewNotices() adding outstanding purchase notice for bpay ID {0}", profileNotice.OriginData), Array.Empty<object>());
					this.m_outstandingPurchaseNotices.Add(item);
				}
			}
		}
		Network network = Network.Get();
		foreach (long id in list)
		{
			network.AckNotice(id);
		}
		if (this.FirstNoticesProcessed)
		{
			return;
		}
		this.FirstNoticesProcessed = true;
		if (this.Status == StoreManager.TransactionStatus.READY)
		{
			this.ResolveFirstMoneyOrGTAPPTransactionIfPossible();
		}
	}

	// Token: 0x060066AD RID: 26285 RVA: 0x00217554 File Offset: 0x00215754
	private void OnNetCacheFeaturesReady()
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		this.FeaturesReady = (netObject != null);
	}

	// Token: 0x060066AE RID: 26286 RVA: 0x00217578 File Offset: 0x00215778
	private void OnPurchaseCanceledResponse()
	{
		Network.PurchaseCanceledResponse purchaseCanceledResponse = Network.Get().GetPurchaseCanceledResponse();
		switch (purchaseCanceledResponse.Result)
		{
		case Network.PurchaseCanceledResponse.CancelResult.SUCCESS:
			global::Log.Store.Print("StoreManager.OnPurchaseCanceledResponse(): purchase successfully canceled.", Array.Empty<object>());
			this.ConfirmActiveMoneyTransaction(purchaseCanceledResponse.TransactionID);
			this.Status = StoreManager.TransactionStatus.READY;
			this.m_previousStatusBeforeAutoCancel = StoreManager.TransactionStatus.UNKNOWN;
			break;
		case Network.PurchaseCanceledResponse.CancelResult.NOT_ALLOWED:
		{
			global::Log.Store.PrintWarning("StoreManager.OnPurchaseCanceledResponse(): cancel purchase is not allowed right now.", Array.Empty<object>());
			bool flag = global::Currency.IsGTAPP(purchaseCanceledResponse.CurrencyCode);
			this.SetActiveMoneyOrGTAPPTransaction(purchaseCanceledResponse.TransactionID, purchaseCanceledResponse.PMTProductID, MoneyOrGTAPPTransaction.UNKNOWN_PROVIDER, flag, true);
			this.Status = (flag ? StoreManager.TransactionStatus.IN_PROGRESS_GOLD_GTAPP : StoreManager.TransactionStatus.IN_PROGRESS_MONEY);
			if (this.m_previousStatusBeforeAutoCancel != StoreManager.TransactionStatus.UNKNOWN)
			{
				this.Status = this.m_previousStatusBeforeAutoCancel;
				this.m_previousStatusBeforeAutoCancel = StoreManager.TransactionStatus.UNKNOWN;
				return;
			}
			break;
		}
		case Network.PurchaseCanceledResponse.CancelResult.NOTHING_TO_CANCEL:
			this.m_previousStatusBeforeAutoCancel = StoreManager.TransactionStatus.UNKNOWN;
			if (this.m_activeMoneyOrGTAPPTransaction != null && (long)StoreManager.UNKNOWN_TRANSACTION_ID != this.m_activeMoneyOrGTAPPTransaction.ID)
			{
				this.ConfirmActiveMoneyTransaction(this.m_activeMoneyOrGTAPPTransaction.ID);
			}
			this.Status = StoreManager.TransactionStatus.READY;
			return;
		default:
			return;
		}
	}

	// Token: 0x060066AF RID: 26287 RVA: 0x00217675 File Offset: 0x00215875
	private bool IsConclusiveState(Network.PurchaseErrorInfo.ErrorType errorType)
	{
		if (errorType <= Network.PurchaseErrorInfo.ErrorType.STILL_IN_PROGRESS)
		{
			if (errorType != Network.PurchaseErrorInfo.ErrorType.UNKNOWN && errorType != Network.PurchaseErrorInfo.ErrorType.STILL_IN_PROGRESS)
			{
				return true;
			}
		}
		else if (errorType - Network.PurchaseErrorInfo.ErrorType.WAIT_MOP > 2 && errorType != Network.PurchaseErrorInfo.ErrorType.WAIT_THIRD_PARTY_RECEIPT)
		{
			return true;
		}
		return false;
	}

	// Token: 0x060066B0 RID: 26288 RVA: 0x00217694 File Offset: 0x00215894
	private void OnBattlePayStatusResponse()
	{
		Network.BattlePayStatus battlePayStatusResponse = Network.Get().GetBattlePayStatusResponse();
		if (battlePayStatusResponse.BattlePayAvailable != this.BattlePayAvailable)
		{
			this.BattlePayAvailable = battlePayStatusResponse.BattlePayAvailable;
			global::Log.Store.Print("Store server status is now {0}", new object[]
			{
				this.BattlePayAvailable ? "available" : "unavailable"
			});
		}
		switch (battlePayStatusResponse.State)
		{
		case Network.BattlePayStatus.PurchaseState.READY:
			this.Status = StoreManager.TransactionStatus.READY;
			global::Log.Store.Print("Store PurchaseState is READY.", Array.Empty<object>());
			return;
		case Network.BattlePayStatus.PurchaseState.CHECK_RESULTS:
		{
			global::Log.Store.Print("Store PurchaseState is CHECK_RESULTS.", Array.Empty<object>());
			bool isGTAPP = global::Currency.IsGTAPP(battlePayStatusResponse.CurrencyCode);
			bool tryToResolvePreviousTransactionNotices = this.IsConclusiveState(battlePayStatusResponse.PurchaseError.Error);
			this.SetActiveMoneyOrGTAPPTransaction(battlePayStatusResponse.TransactionID, battlePayStatusResponse.PMTProductID, battlePayStatusResponse.Provider, isGTAPP, tryToResolvePreviousTransactionNotices);
			this.HandlePurchaseError(StoreManager.PurchaseErrorSource.FROM_STATUS_OR_PURCHASE_RESPONSE, battlePayStatusResponse.PurchaseError.Error, battlePayStatusResponse.PurchaseError.ErrorCode, battlePayStatusResponse.ThirdPartyID, isGTAPP);
			return;
		}
		case Network.BattlePayStatus.PurchaseState.ERROR:
			global::Log.Store.PrintError("Store PurchaseState is ERROR.", Array.Empty<object>());
			return;
		default:
			global::Log.Store.PrintError("Store PurchaseState is unknown value {0}.", new object[]
			{
				battlePayStatusResponse.State
			});
			return;
		}
	}

	// Token: 0x060066B1 RID: 26289 RVA: 0x00090064 File Offset: 0x0008E264
	private static string GetExternalStoreProductId(Network.Bundle bundle)
	{
		return null;
	}

	// Token: 0x060066B2 RID: 26290 RVA: 0x002177D8 File Offset: 0x002159D8
	private static bool ValidateExternalStoreProductIDs(Network.Bundle bundle)
	{
		if (bundle.Cost == null)
		{
			return true;
		}
		bool flag = true;
		bool flag2 = true;
		bool flag3 = true;
		if (bundle.ExclusiveProviders != null && bundle.ExclusiveProviders.Count > 0)
		{
			if (!bundle.ExclusiveProviders.Contains(StoreManager.StoreProvider))
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
		string text = (bundle.DisplayName != null) ? bundle.DisplayName.GetString(true) : "";
		if (StoreManager.HasExternalStore)
		{
			if (flag && !flag4)
			{
				global::Log.Store.PrintWarning("Product missing Apple Store Product ID: PMT ID = {0}, Name = {1}", new object[]
				{
					bundle.PMTProductID,
					text
				});
			}
			if (flag2 && !flag5)
			{
				global::Log.Store.PrintWarning("Product missing Google Play Store Product ID: PMT ID = {0}, Name = {1}", new object[]
				{
					bundle.PMTProductID,
					text
				});
			}
			if (flag3 && !flag6)
			{
				global::Log.Store.PrintWarning("Product missing Amazon Store Product ID: PMT ID = {0}, Name = {1}", new object[]
				{
					bundle.PMTProductID,
					text
				});
			}
		}
		bool flag7 = false;
		if (StoreManager.HasExternalStore && string.IsNullOrEmpty(StoreManager.GetExternalStoreProductId(bundle)))
		{
			global::Log.Store.PrintError("Product cannot have real money cost due to missing external store product ID. PMT ID = {0}. Name = {1}", new object[]
			{
				bundle.PMTProductID,
				text
			});
			flag7 = true;
		}
		if (!flag7)
		{
			return true;
		}
		if (ShopUtils.BundleHasNonRealMoneyPrice(bundle))
		{
			bundle.Cost = null;
			bundle.CostDisplay = null;
			return true;
		}
		return false;
	}

	// Token: 0x17000614 RID: 1556
	// (get) Token: 0x060066B3 RID: 26291 RVA: 0x002179A4 File Offset: 0x00215BA4
	public static bool HasExternalStore
	{
		get
		{
			return StoreManager.HAS_THIRD_PARTY_APP_STORE;
		}
	}

	// Token: 0x060066B4 RID: 26292 RVA: 0x002179B8 File Offset: 0x00215BB8
	private void OnBattlePayConfigResponse()
	{
		Network.BattlePayConfig battlePayConfigResponse = Network.Get().GetBattlePayConfigResponse();
		if (!battlePayConfigResponse.Available)
		{
			global::Log.Store.PrintWarning("Server responds that store is unavailable.", Array.Empty<object>());
			this.BattlePayAvailable = false;
			return;
		}
		HearthstoneCheckout.OneStoreKey = battlePayConfigResponse.CheckoutKrOnestoreKey;
		global::Log.Store.Print("Server responds that store is available.", Array.Empty<object>());
		this.BattlePayAvailable = true;
		this.m_currency = battlePayConfigResponse.Currency;
		this.m_secsBeforeAutoCancel = (float)battlePayConfigResponse.SecondsBeforeAutoCancel;
		bool hasExternalStore = StoreManager.HasExternalStore;
		this.m_bundles.Clear();
		foreach (Network.Bundle bundle in battlePayConfigResponse.Bundles)
		{
			if (StoreManager.ValidateExternalStoreProductIDs(bundle) && bundle.PMTProductID != null)
			{
				this.m_bundles.Add(bundle.PMTProductID.Value, bundle);
			}
		}
		this.m_goldCostBooster.Clear();
		foreach (Network.GoldCostBooster goldCostBooster in battlePayConfigResponse.GoldCostBoosters)
		{
			this.m_goldCostBooster.Add(goldCostBooster.ID, goldCostBooster);
		}
		this.m_goldCostArena = battlePayConfigResponse.GoldCostArena;
		this.SetPersonalizedShopPageAndRefreshCatalog(battlePayConfigResponse.PersonalizedShopPageID);
		this.m_sales.Clear();
		foreach (Network.ShopSale shopSale in battlePayConfigResponse.SaleList)
		{
			this.m_sales[shopSale.SaleId] = shopSale;
		}
		this.m_ignoreProductTiming = battlePayConfigResponse.IgnoreProductTiming;
		if (hasExternalStore)
		{
			global::Log.Store.Print("Validating mobile products. Store will remain closed until completed.", Array.Empty<object>());
		}
		else
		{
			this.ConfigLoaded = true;
		}
		global::Log.Store.Print("StoreManager.OnBattlePayConfigResponse: Queueing ConfigureCheckout Job.", Array.Empty<object>());
		Processor.QueueJob("StoreManager.ConfigureCheckoutFromBattlePayConfig", this.Job_ConfigureCheckoutFromBattlePayConfig(battlePayConfigResponse.CommerceClientID, (this.m_currency != null) ? this.m_currency.Code : ""), new IJobDependency[]
		{
			HearthstoneServices.CreateServiceSoftDependency(typeof(HearthstoneCheckout))
		});
	}

	// Token: 0x060066B5 RID: 26293 RVA: 0x00217C10 File Offset: 0x00215E10
	private IEnumerator<IAsyncJobResult> Job_ConfigureCheckoutFromBattlePayConfig(string clientID, string currencyCode)
	{
		HearthstoneCheckout hearthstoneCheckout;
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout))
		{
			List<string> list = new List<string>(this.m_bundles.Count);
			foreach (Network.Bundle bundle in this.m_bundles.Values)
			{
				if (bundle.PMTProductID != null)
				{
					list.Add(bundle.PMTProductID.ToString());
				}
			}
			hearthstoneCheckout.SetClientID(clientID);
			hearthstoneCheckout.SetProductCatalog(list.ToArray());
			hearthstoneCheckout.SetCurrencyCode(currencyCode);
			global::Log.Store.Print("StoreManager.ConfigureCheckoutFromBattlePayConfig: Queueing FireStatusChangeEventForHearthstoneCheckout Job.", Array.Empty<object>());
			Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("StoreManager.FireStatusChangeEventForHearthstoneCheckout", new Action(this.OnCheckoutInitializationComplete), new IJobDependency[]
			{
				new WaitForCheckoutState(new HearthstoneCheckout.State[]
				{
					HearthstoneCheckout.State.Idle,
					HearthstoneCheckout.State.Unavailable
				})
			}));
		}
		else
		{
			global::Log.Store.Print("StoreManager.ConfigureCheckoutFromBattlePayConfig: HearthstoneCheckout is unavailable.", Array.Empty<object>());
			this.OnCheckoutInitializationComplete();
		}
		yield break;
	}

	// Token: 0x060066B6 RID: 26294 RVA: 0x00217C2D File Offset: 0x00215E2D
	private void OnCheckoutInitializationComplete()
	{
		global::Log.Store.Print("StoreManager.OnCheckoutInitializationComplete: OnCheckoutInitializationComplete called.", Array.Empty<object>());
		this.FireStatusChangedEventIfNeeded();
	}

	// Token: 0x060066B7 RID: 26295 RVA: 0x00217C4C File Offset: 0x00215E4C
	private void HandleZeroCostLicensePurchaseMethod(Network.PurchaseMethod method)
	{
		if (Network.PurchaseErrorInfo.ErrorType.STILL_IN_PROGRESS != method.PurchaseError.Error)
		{
			global::Log.Store.PrintWarning(string.Format("StoreManager.HandleZeroCostLicensePurchaseMethod() FAILED error={0}", method.PurchaseError.Error), Array.Empty<object>());
			this.Status = StoreManager.TransactionStatus.READY;
			return;
		}
		global::Log.Store.Print("StoreManager.HandleZeroCostLicensePurchaseMethod succeeded, refreshing achieves", Array.Empty<object>());
	}

	// Token: 0x060066B8 RID: 26296 RVA: 0x00217CAC File Offset: 0x00215EAC
	private void OnPurchaseMethod()
	{
		Network.PurchaseMethod purchaseMethodResponse = Network.Get().GetPurchaseMethodResponse();
		if (purchaseMethodResponse.IsZeroCostLicense)
		{
			this.HandleZeroCostLicensePurchaseMethod(purchaseMethodResponse);
			return;
		}
		if (!string.IsNullOrEmpty(purchaseMethodResponse.ChallengeID) && !string.IsNullOrEmpty(purchaseMethodResponse.ChallengeURL))
		{
			this.m_challengePurchaseMethod = purchaseMethodResponse;
		}
		else
		{
			this.m_challengePurchaseMethod = null;
		}
		bool flag = global::Currency.IsGTAPP(purchaseMethodResponse.CurrencyCode);
		this.SetActiveMoneyOrGTAPPTransaction(purchaseMethodResponse.TransactionID, purchaseMethodResponse.PMTProductID, new BattlePayProvider?(BattlePayProvider.BP_PROVIDER_BLIZZARD), flag, false);
		if (purchaseMethodResponse.PurchaseError != null)
		{
			this.HandlePurchaseError(StoreManager.PurchaseErrorSource.FROM_PURCHASE_METHOD_RESPONSE, purchaseMethodResponse.PurchaseError.Error, purchaseMethodResponse.PurchaseError.ErrorCode, string.Empty, flag);
			return;
		}
		this.BlockStoreInterface();
		if (flag)
		{
			this.OnSummaryConfirm(purchaseMethodResponse.Quantity, null);
			return;
		}
		string paymentMethodName;
		if (purchaseMethodResponse.UseEBalance)
		{
			paymentMethodName = GameStrings.Get("GLUE_STORE_BNET_BALANCE");
		}
		else
		{
			paymentMethodName = purchaseMethodResponse.WalletName;
		}
		IStore currentStore = this.GetCurrentStore();
		if (currentStore == null || !currentStore.IsOpen())
		{
			this.AutoCancelPurchaseIfPossible();
			return;
		}
		this.m_view.PurchaseAuth.Hide();
		this.Status = StoreManager.TransactionStatus.WAIT_CONFIRM;
		this.m_view.Summary.Show(purchaseMethodResponse.PMTProductID ?? 0L, purchaseMethodResponse.Quantity, paymentMethodName);
	}

	// Token: 0x060066B9 RID: 26297 RVA: 0x00217DE8 File Offset: 0x00215FE8
	private void OnPurchaseResponse()
	{
		Network.PurchaseResponse purchaseResponse = Network.Get().GetPurchaseResponse();
		bool isGTAPP = global::Currency.IsGTAPP(purchaseResponse.CurrencyCode);
		this.SetActiveMoneyOrGTAPPTransaction(purchaseResponse.TransactionID, purchaseResponse.PMTProductID, MoneyOrGTAPPTransaction.UNKNOWN_PROVIDER, isGTAPP, false);
		this.HandlePurchaseError(StoreManager.PurchaseErrorSource.FROM_STATUS_OR_PURCHASE_RESPONSE, purchaseResponse.PurchaseError.Error, purchaseResponse.PurchaseError.ErrorCode, purchaseResponse.ThirdPartyID, isGTAPP);
	}

	// Token: 0x060066BA RID: 26298 RVA: 0x00217E4C File Offset: 0x0021604C
	private void OnPurchaseViaGoldResponse()
	{
		string details;
		switch (Network.Get().GetPurchaseWithGoldResponse().Error)
		{
		case Network.PurchaseViaGoldResponse.ErrorType.SUCCESS:
			this.HandlePurchaseSuccess(null, null, string.Empty, null);
			return;
		case Network.PurchaseViaGoldResponse.ErrorType.INSUFFICIENT_GOLD:
			details = GameStrings.Get("GLUE_STORE_FAIL_NOT_ENOUGH_GOLD");
			break;
		case Network.PurchaseViaGoldResponse.ErrorType.PRODUCT_NA:
			details = GameStrings.Get("GLUE_STORE_FAIL_PRODUCT_NA");
			break;
		case Network.PurchaseViaGoldResponse.ErrorType.FEATURE_NA:
			details = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC");
			break;
		case Network.PurchaseViaGoldResponse.ErrorType.INVALID_QUANTITY:
			details = GameStrings.Get("GLUE_STORE_FAIL_QUANTITY");
			break;
		default:
			details = GameStrings.Get("GLUE_STORE_FAIL_GENERAL");
			break;
		}
		this.Status = StoreManager.TransactionStatus.READY;
		this.m_view.PurchaseAuth.CompletePurchaseFailure(null, details, Network.PurchaseErrorInfo.ErrorType.BP_GENERIC_FAIL);
	}

	// Token: 0x060066BB RID: 26299 RVA: 0x00217EFF File Offset: 0x002160FF
	private void OnThirdPartyPurchaseStatusResponse()
	{
		global::Log.Store.PrintWarning("[StoreManager.OnThirdPartyPurchaseStatusResponse] Received OnThirdPartyPurchaseStatusResponse packet.  Legacy third party purchasing has been removed.", Array.Empty<object>());
	}

	// Token: 0x060066BC RID: 26300 RVA: 0x00217F15 File Offset: 0x00216115
	private void StoreViewReady()
	{
		if (!this.m_waitingToShowStore)
		{
			return;
		}
		if (!this.IsCurrentStoreLoaded())
		{
			return;
		}
		this.ShowStore();
	}

	// Token: 0x060066BD RID: 26301 RVA: 0x00217F30 File Offset: 0x00216130
	private void OnGeneralStoreLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		GeneralStore generalStore = this.OnStoreLoaded<GeneralStore>(go, ShopType.GENERAL_STORE);
		if (generalStore != null)
		{
			this.SetupLoadedStore(generalStore);
		}
	}

	// Token: 0x060066BE RID: 26302 RVA: 0x00217F58 File Offset: 0x00216158
	private void OnArenaStoreLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		ArenaStore arenaStore = this.OnStoreLoaded<ArenaStore>(go, ShopType.ARENA_STORE);
		if (arenaStore != null)
		{
			this.SetupLoadedStore(arenaStore);
		}
	}

	// Token: 0x060066BF RID: 26303 RVA: 0x00217F80 File Offset: 0x00216180
	private void OnBrawlStoreLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		TavernBrawlStore tavernBrawlStore = this.OnStoreLoaded<TavernBrawlStore>(go, ShopType.TAVERN_BRAWL_STORE);
		if (tavernBrawlStore != null)
		{
			this.SetupLoadedStore(tavernBrawlStore);
		}
	}

	// Token: 0x060066C0 RID: 26304 RVA: 0x00217FA8 File Offset: 0x002161A8
	private void OnAdventureStoreLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		AdventureStore adventureStore = this.OnStoreLoaded<AdventureStore>(go, ShopType.ADVENTURE_STORE);
		if (adventureStore != null)
		{
			this.SetupLoadedStore(adventureStore);
		}
	}

	// Token: 0x060066C1 RID: 26305 RVA: 0x00217FD0 File Offset: 0x002161D0
	private void OnAdventureWingStoreLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		AdventureStore adventureStore = this.OnStoreLoaded<AdventureStore>(go, ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET);
		if (adventureStore != null)
		{
			this.SetupLoadedStore(adventureStore);
		}
	}

	// Token: 0x060066C2 RID: 26306 RVA: 0x00217FF8 File Offset: 0x002161F8
	private void OnAdventureFullStoreLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		AdventureStore adventureStore = this.OnStoreLoaded<AdventureStore>(go, ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET);
		if (adventureStore != null)
		{
			this.SetupLoadedStore(adventureStore);
		}
	}

	// Token: 0x060066C3 RID: 26307 RVA: 0x00218020 File Offset: 0x00216220
	private T OnStoreLoaded<T>(GameObject go, ShopType shopType) where T : Store
	{
		if (go == null)
		{
			Debug.LogError(string.Format("StoreManager.OnStoreLoaded<{0}>(): go is null!", typeof(T)));
			return default(T);
		}
		T t = go.GetComponent<T>();
		if (t == null)
		{
			t = go.GetComponentInChildren<T>();
		}
		if (t == null)
		{
			Debug.LogError(string.Format("StoreManager.OnStoreLoaded<{0}>(): go has no {1} component!", typeof(T), typeof(T)));
			return default(T);
		}
		this.m_stores[shopType] = t;
		return t;
	}

	// Token: 0x060066C4 RID: 26308 RVA: 0x002180C4 File Offset: 0x002162C4
	private void SendShopPurchaseEventTelemetry(bool isComplete)
	{
		if (this.m_pendingProductPurchaseArgs == null)
		{
			global::Log.Store.PrintWarning("No active transaction in progress", Array.Empty<object>());
			return;
		}
		Blizzard.Telemetry.WTCG.Client.Product product = new Blizzard.Telemetry.WTCG.Client.Product();
		long productId;
		string currency;
		long num;
		int quantity;
		string hsProductType;
		int hsProductId;
		if (!ShopUtils.TryDecomposeBuyProductEventArgs(this.m_pendingProductPurchaseArgs, out productId, out currency, out num, out quantity, out hsProductType, out hsProductId))
		{
			global::Log.Store.PrintError("Failed to decompose pending product purchase args for telemetry.", Array.Empty<object>());
			return;
		}
		BattlePayProvider? battlePayProvider = this.ActiveTransactionProvider();
		string storefront = (battlePayProvider != null) ? battlePayProvider.Value.ToString().ToLowerInvariant() : "";
		product.ProductId = productId;
		product.HsProductType = hsProductType;
		product.HsProductId = hsProductId;
		TelemetryManager.Client().SendShopPurchaseEvent(product, quantity, currency, (double)num, false, storefront, isComplete, this.m_currentShopType.ToString());
	}

	// Token: 0x060066C5 RID: 26309 RVA: 0x00218193 File Offset: 0x00216393
	public void RegisterAmazingNewShop(Shop amazingNewShop)
	{
		this.SetupLoadedStore(amazingNewShop);
	}

	// Token: 0x060066C6 RID: 26310 RVA: 0x0021819C File Offset: 0x0021639C
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
				global::Log.Store.PrintError("Cannot attempt purchase due to null BuyProductEventArgs", Array.Empty<object>());
				return;
			}
			BuyPmtProductEventArgs args2 = args as BuyPmtProductEventArgs;
			this.m_pendingProductPurchaseArgs = args;
			this.SendShopPurchaseEventTelemetry(false);
			global::CurrencyType paymentCurrency = args.PaymentCurrency;
			if (paymentCurrency != global::CurrencyType.GOLD)
			{
				if (paymentCurrency == global::CurrencyType.REAL_MONEY)
				{
					this.OnStoreBuyWithMoney(args2);
					return;
				}
				if (ShopUtils.IsCurrencyVirtual(args.PaymentCurrency))
				{
					this.OnStoreBuyWithCheckout(args2);
					return;
				}
				global::Log.Store.PrintError("Attempted purchase with invalid currency type {0}", new object[]
				{
					args.PaymentCurrency
				});
				return;
			}
			else
			{
				BuyNoGTAPPEventArgs buyNoGTAPPEventArgs;
				if ((buyNoGTAPPEventArgs = (args as BuyNoGTAPPEventArgs)) != null)
				{
					this.OnStoreBuyWithGoldNoGTAPP(buyNoGTAPPEventArgs.transactionData);
					return;
				}
				this.OnStoreBuyWithGTAPP(args2);
				return;
			}
		};
		store.OnOpened += this.OnStoreOpen;
		store.OnClosed += delegate(StoreClosedArgs e)
		{
			this.OnStoreExit(e.authorizationBackButtonPressed.GetValueOrDefault(false), null);
		};
		store.OnReady += this.StoreViewReady;
		Store store2 = store as Store;
		if (store2 != null)
		{
			store2.RegisterInfoListener(new Store.InfoCallback(this.OnStoreInfo));
		}
		this.StoreViewReady();
	}

	// Token: 0x060066C7 RID: 26311 RVA: 0x00218219 File Offset: 0x00216419
	private void OnSceneUnloaded(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		this.UnloadAndFreeMemory();
	}

	// Token: 0x060066C8 RID: 26312 RVA: 0x00218221 File Offset: 0x00216421
	private void OnHearthstoneCheckoutInitialized()
	{
		if (this.ShouldGetPersonalizedShopData())
		{
			this.RequestPersonalizedShopData();
		}
	}

	// Token: 0x060066C9 RID: 26313 RVA: 0x00218234 File Offset: 0x00216434
	private void RequestPersonalizedShopData()
	{
		HearthstoneCheckout hearthstoneCheckout;
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout) && hearthstoneCheckout.CurrentState == HearthstoneCheckout.State.Idle)
		{
			global::Log.Store.PrintDebug("Getting personalized shop data with page id: {0}", new object[]
			{
				this.m_shopPageId
			});
			hearthstoneCheckout.GetPersonalizedShopData(this.m_shopPageId, new HearthstoneCheckout.PersonalizedShopResponseCallback(this.OnHearthstoneGetPersonalizedShopData));
		}
	}

	// Token: 0x060066CA RID: 26314 RVA: 0x00218289 File Offset: 0x00216489
	private void OnHearthstoneCheckoutReady()
	{
		this.SendBlizzardCheckoutIsReadyTelemetry(true);
	}

	// Token: 0x060066CB RID: 26315 RVA: 0x00218294 File Offset: 0x00216494
	private void SendBlizzardCheckoutIsReadyTelemetry(bool sendIfReady)
	{
		HearthstoneCheckout hearthstoneCheckout;
		if (!HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout))
		{
			return;
		}
		float shownTime = hearthstoneCheckout.GetShownTime();
		bool flag = hearthstoneCheckout.CurrentState != HearthstoneCheckout.State.Initializing;
		if (sendIfReady == flag)
		{
			TelemetryManager.Client().SendBlizzardCheckoutIsReady((double)shownTime, flag);
		}
	}

	// Token: 0x060066CC RID: 26316 RVA: 0x002182D0 File Offset: 0x002164D0
	private bool ShouldGetPersonalizedShopData()
	{
		return !string.IsNullOrEmpty(this.m_shopPageId);
	}

	// Token: 0x060066CD RID: 26317 RVA: 0x002182E0 File Offset: 0x002164E0
	public void SetPersonalizedShopPageAndRefreshCatalog(string pageId)
	{
		this.m_shopPageId = pageId;
		if (this.ShouldGetPersonalizedShopData())
		{
			this.RequestPersonalizedShopData();
		}
	}

	// Token: 0x060066CE RID: 26318 RVA: 0x002182F8 File Offset: 0x002164F8
	private void OnHearthstoneGetPersonalizedShopData(GetPageResponse response)
	{
		if (!this.ShouldGetPersonalizedShopData() || response == null)
		{
			return;
		}
		if (response.page == null)
		{
			global::Log.Store.PrintError("No page data was found for page id \"{0}\"", new object[]
			{
				this.m_shopPageId
			});
			if (response.error != null && !string.IsNullOrEmpty(response.error.code))
			{
				global::Log.Store.PrintError("GetPageResponse Error: code:{0}, message:{1}", new object[]
				{
					string.IsNullOrEmpty(response.error.code) ? "?" : response.error.code,
					string.IsNullOrEmpty(response.error.message) ? string.Empty : response.error.message
				});
			}
			return;
		}
		this.m_sections.Clear();
		global::Log.Store.PrintDebug("Section Data (page {0}):", new object[]
		{
			this.m_shopPageId
		});
		foreach (Section section in response.page.sections)
		{
			string text = string.Format("section {0}: {1}", this.m_sections.Count + 1, section.name);
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
				Network.ShopSection shopSection = this.m_sections.FirstOrDefault((Network.ShopSection s) => s.SortOrder == netSection.SortOrder);
				if (shopSection != null)
				{
					global::Log.Store.PrintError("section {0} has the same SortOrder as {1}: {2}. Order may be inconsistent", new object[]
					{
						section.name,
						shopSection.InternalName,
						netSection.SortOrder
					});
				}
				text += string.Format("\n  sortOrder={0}", netSection.SortOrder);
			}
			else
			{
				global::Log.Store.PrintError("section {0} missing OrderInPage", new object[]
				{
					section.name
				});
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
				foreach (ProductCollectionItem productCollectionItem in productCollection.items)
				{
					Network.ShopSection.ProductRef productRef = new Network.ShopSection.ProductRef();
					productRef.OrderId = productCollectionItem.orderInProductCollection;
					productRef.PmtId = (long)productCollectionItem.productCollectionItemValue;
					netSection.Products.Add(productRef);
					text += string.Format("\n    [{0}]={1}", productRef.OrderId, productRef.PmtId);
				}
			}
			this.m_sections.Add(netSection);
			global::Log.Store.PrintDebug(text, Array.Empty<object>());
		}
	}

	// Token: 0x060066CF RID: 26319 RVA: 0x00218728 File Offset: 0x00216928
	private void OnHearthstoneCheckoutCancel()
	{
		this.Status = StoreManager.TransactionStatus.READY;
		if (this.m_activeMoneyOrGTAPPTransaction != null)
		{
			this.ConfirmActiveMoneyTransaction(this.m_activeMoneyOrGTAPPTransaction.ID);
		}
		this.m_view.PurchaseAuth.Hide();
		Network.Get().ReportBlizzardCheckoutStatus(BlizzardCheckoutStatus.BLIZZARD_CHECKOUT_STATUS_CANCELED, null);
		TelemetryManager.Client().SendBlizzardCheckoutPurchaseCancel();
		this.SendBlizzardCheckoutIsReadyTelemetry(false);
	}

	// Token: 0x060066D0 RID: 26320 RVA: 0x00218784 File Offset: 0x00216984
	private void OnHearthstoneCheckoutClose()
	{
		if (!this.m_view.PurchaseAuth.IsShown)
		{
			this.SetCanTapOutConfirmationUI(true);
			this.UnblockStoreInterface();
			if (this.Status == StoreManager.TransactionStatus.IN_PROGRESS_BLIZZARD_CHECKOUT || this.m_showStoreData.closeOnTransactionComplete)
			{
				this.GetCurrentStore().Close();
				return;
			}
		}
		else if (this.Status != StoreManager.TransactionStatus.READY)
		{
			this.m_view.PurchaseAuth.Hide();
			this.UnblockStoreInterface();
		}
	}

	// Token: 0x060066D1 RID: 26321 RVA: 0x002187F4 File Offset: 0x002169F4
	private void OnHearthstoneCheckoutOrderPending(HearthstoneCheckoutTransactionData data)
	{
		if (this.IsHearthstoneCheckoutUIShowing() || this.m_view.PurchaseAuth.IsShown)
		{
			this.m_view.PurchaseAuth.Show(this.m_activeMoneyOrGTAPPTransaction, false, false);
		}
		this.Status = StoreManager.TransactionStatus.IN_PROGRESS_BLIZZARD_CHECKOUT;
		if (data != null)
		{
			Network.Get().ReportBlizzardCheckoutStatus(BlizzardCheckoutStatus.BLIZZARD_CHECKOUT_STATUS_START, data);
			TelemetryManager.Client().SendBlizzardCheckoutPurchaseStart(data.TransactionID, data.ProductID.ToString(), data.CurrencyCode);
		}
	}

	// Token: 0x060066D2 RID: 26322 RVA: 0x00218870 File Offset: 0x00216A70
	private void OnHearthstoneCheckoutOrderFailure(HearthstoneCheckoutTransactionData data)
	{
		if (!this.m_view.PurchaseAuth.IsShown)
		{
			this.m_view.PurchaseAuth.Show(this.m_activeMoneyOrGTAPPTransaction, false, false);
		}
		string hearthstoneCheckoutErrorString = this.GetHearthstoneCheckoutErrorString(data.ErrorCodes);
		this.m_view.PurchaseAuth.CompletePurchaseFailure(this.m_activeMoneyOrGTAPPTransaction, hearthstoneCheckoutErrorString, Network.PurchaseErrorInfo.ErrorType.BP_GENERIC_FAIL);
		this.Status = StoreManager.TransactionStatus.READY;
		Network.Get().ReportBlizzardCheckoutStatus(BlizzardCheckoutStatus.BLIZZARD_CHECKOUT_STATUS_COMPLETED_FAILED, data);
		TelemetryManager.Client().SendBlizzardCheckoutPurchaseCompletedFailure(data.TransactionID, data.ProductID.ToString(), data.CurrencyCode, new List<string>
		{
			data.ErrorCodes
		});
		global::Log.Store.PrintError("Checkout Order Failure: TransactionID={0}, ProductID={1}, CurrencyCode={2}, ErrorCodes={3}", new object[]
		{
			data.TransactionID,
			data.ProductID,
			data.CurrencyCode,
			data.ErrorCodes
		});
	}

	// Token: 0x060066D3 RID: 26323 RVA: 0x00218954 File Offset: 0x00216B54
	private void OnHearthstoneCheckoutSubmitFailure()
	{
		if (!this.m_view.PurchaseAuth.IsShown)
		{
			this.m_view.PurchaseAuth.Show(this.m_activeMoneyOrGTAPPTransaction, false, false);
		}
		string details = GameStrings.Get("GLUE_CHECKOUT_ERROR_GENERIC_FAILURE");
		this.m_view.PurchaseAuth.CompletePurchaseFailure(this.m_activeMoneyOrGTAPPTransaction, details, Network.PurchaseErrorInfo.ErrorType.BP_GENERIC_FAIL);
		this.Status = StoreManager.TransactionStatus.READY;
	}

	// Token: 0x060066D4 RID: 26324 RVA: 0x002189B8 File Offset: 0x00216BB8
	private string GetHearthstoneCheckoutErrorString(string errorCode)
	{
		if (errorCode == "BLZBNTPURJNL42203")
		{
			return GameStrings.Get("GLUE_STORE_FAIL_PRODUCT_ALREADY_OWNED");
		}
		if (errorCode == "BLZBNTPURJNL42208")
		{
			return GameStrings.Get("GLUE_STORE_FAIL_SPENDING_LIMIT");
		}
		if (errorCode == "BLZBNTPUR3000003" || errorCode == "10201001")
		{
			return GameStrings.Get("GLUE_CHECKOUT_ERROR_INSUFFICIENT_FUNDS");
		}
		if (!(errorCode == "30000101"))
		{
			if (!(errorCode == "10010101"))
			{
			}
			global::Log.Store.PrintWarning("Unhandled checkout error: {0}", new object[]
			{
				errorCode
			});
			return GameStrings.Get("GLUE_CHECKOUT_ERROR_GENERIC_FAILURE");
		}
		return GameStrings.Get("GLUE_CHECKOUT_ERROR_PRODUCT_UNAVAILABLE");
	}

	// Token: 0x060066D5 RID: 26325 RVA: 0x00218A64 File Offset: 0x00216C64
	private void OnHearthstoneCheckoutOrderComplete(HearthstoneCheckoutTransactionData data)
	{
		if (this.IsHearthstoneCheckoutUIShowing() && !this.m_view.PurchaseAuth.IsShown)
		{
			this.m_view.PurchaseAuth.Show(this.m_activeMoneyOrGTAPPTransaction, false, false);
		}
		this.SendAttributionPurchaseMessage(data);
		this.HandlePurchaseSuccess(null, this.m_activeMoneyOrGTAPPTransaction, string.Empty, data);
		if (data != null)
		{
			Network.Get().ReportBlizzardCheckoutStatus(BlizzardCheckoutStatus.BLIZZARD_CHECKOUT_STATUS_COMPLETED_SUCCESS, data);
			TelemetryManager.Client().SendBlizzardCheckoutPurchaseCompletedSuccess(data.TransactionID, data.ProductID.ToString(), data.CurrencyCode);
		}
	}

	// Token: 0x060066D6 RID: 26326 RVA: 0x00218AF8 File Offset: 0x00216CF8
	private void SendAttributionPurchaseMessage(HearthstoneCheckoutTransactionData transactionData)
	{
		if (!StoreManager.HasExternalStore)
		{
			return;
		}
		if (transactionData == null)
		{
			global::Log.Store.PrintWarning("[SendAttributionPurchaseMessage] No transaction data provided, skipping attribution message.", Array.Empty<object>());
			return;
		}
		if (transactionData.IsVCPurchase)
		{
			return;
		}
		AdTrackingManager adTrackingManager;
		if (!HearthstoneServices.TryGet<AdTrackingManager>(out adTrackingManager))
		{
			global::Log.Store.PrintWarning("[SendAttributionPurchaseMessage] AdTrackingManager unavailable, skipping attribution message.", Array.Empty<object>());
			return;
		}
		Network.Bundle bundleFromPmtProductId = this.GetBundleFromPmtProductId(transactionData.ProductID);
		if (bundleFromPmtProductId == null)
		{
			global::Log.Store.PrintWarning("[SendAttributionPurchaseMessage] Unable to find bundle for PMT Product ID {0}, skipping attribution message.", new object[]
			{
				transactionData.ProductID
			});
			return;
		}
		double? costDisplay = bundleFromPmtProductId.CostDisplay;
		string currencyCode = transactionData.CurrencyCode;
		string transactionID = transactionData.TransactionID;
		string productId = StoreManager.GetExternalStoreProductId(bundleFromPmtProductId) ?? bundleFromPmtProductId.PMTProductID.ToString();
		adTrackingManager.TrackSale(costDisplay ?? 0.0, currencyCode, productId, transactionID);
	}

	// Token: 0x060066D7 RID: 26327 RVA: 0x00218BE0 File Offset: 0x00216DE0
	private bool IsHearthstoneCheckoutUIShowing()
	{
		HearthstoneCheckout hearthstoneCheckout;
		return HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout) && hearthstoneCheckout.IsUIShown();
	}

	// Token: 0x060066D8 RID: 26328 RVA: 0x00218C00 File Offset: 0x00216E00
	public bool WillStoreDisplayNotice(NetCache.ProfileNotice.NoticeOrigin noticeOrigin, NetCache.ProfileNotice.NoticeType noticeType, long noticeOriginData)
	{
		GeneralStore generalStore = this.GetCurrentStore() as GeneralStore;
		if (generalStore == null)
		{
			return false;
		}
		GeneralStorePacksPane generalStorePacksPane = generalStore.GetCurrentPane() as GeneralStorePacksPane;
		return !(generalStorePacksPane == null) && generalStorePacksPane.WillStoreDisplayNotice(noticeOrigin, noticeType, noticeOriginData);
	}

	// Token: 0x060066D9 RID: 26329 RVA: 0x00218C44 File Offset: 0x00216E44
	private string GetProductPrice(string pmtProductId)
	{
		HearthstoneCheckout.ProductInfo productInfo;
		if (this.TryGetProductInfo(pmtProductId, out productInfo))
		{
			return productInfo.Price;
		}
		return null;
	}

	// Token: 0x060066DA RID: 26330 RVA: 0x00218C64 File Offset: 0x00216E64
	private bool TryGetProductInfo(string pmtProductId, out HearthstoneCheckout.ProductInfo productInfo)
	{
		HearthstoneCheckout hearthstoneCheckout;
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout))
		{
			return hearthstoneCheckout.TryGetProductInfo(pmtProductId, out productInfo);
		}
		productInfo = default(HearthstoneCheckout.ProductInfo);
		return false;
	}

	// Token: 0x060066DB RID: 26331 RVA: 0x0001FA65 File Offset: 0x0001DC65
	private bool IsCheckoutFallbackSupported()
	{
		return false;
	}

	// Token: 0x0400547B RID: 21627
	public static readonly int DEFAULT_SECONDS_BEFORE_AUTO_CANCEL = 600;

	// Token: 0x0400547C RID: 21628
	public const int NO_ITEM_COUNT_REQUIREMENT = 0;

	// Token: 0x0400547D RID: 21629
	public const int NO_PRODUCT_DATA_REQUIREMENT = 0;

	// Token: 0x0400547E RID: 21630
	public const string DEFAULT_COMMERCE_CLIENT_ID = "df5787f96b2b46c49c66dd45bcb05490";

	// Token: 0x0400547F RID: 21631
	private static readonly PlatformDependentValue<GeneralStoreMode> s_defaultStoreMode = new PlatformDependentValue<GeneralStoreMode>(PlatformCategory.Screen)
	{
		PC = GeneralStoreMode.CARDS,
		Phone = GeneralStoreMode.NONE
	};

	// Token: 0x04005480 RID: 21632
	private static readonly int UNKNOWN_TRANSACTION_ID = -1;

	// Token: 0x04005481 RID: 21633
	private static readonly double CURRENCY_TRANSACTION_TIMEOUT_SECONDS = 30.0;

	// Token: 0x04005482 RID: 21634
	private static readonly global::Map<AdventureDbId, ProductType> s_adventureToProductMap = new global::Map<AdventureDbId, ProductType>
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

	// Token: 0x04005483 RID: 21635
	private static StoreManager s_instance = null;

	// Token: 0x04005484 RID: 21636
	private readonly ShopView m_view = new ShopView();

	// Token: 0x04005485 RID: 21637
	private bool m_featuresReady;

	// Token: 0x04005486 RID: 21638
	private bool m_initComplete;

	// Token: 0x04005487 RID: 21639
	private bool m_battlePayAvailable;

	// Token: 0x04005488 RID: 21640
	private bool m_firstNoticesProcessed;

	// Token: 0x04005489 RID: 21641
	private bool m_firstMoneyOrGTAPPTransactionSet;

	// Token: 0x0400548A RID: 21642
	private float m_secsBeforeAutoCancel = (float)StoreManager.DEFAULT_SECONDS_BEFORE_AUTO_CANCEL;

	// Token: 0x0400548B RID: 21643
	private float m_lastCancelRequestTime;

	// Token: 0x0400548C RID: 21644
	private bool m_configLoaded;

	// Token: 0x0400548D RID: 21645
	private readonly global::Map<long, Network.Bundle> m_bundles = new global::Map<long, Network.Bundle>();

	// Token: 0x0400548E RID: 21646
	private readonly global::Map<int, Network.GoldCostBooster> m_goldCostBooster = new global::Map<int, Network.GoldCostBooster>();

	// Token: 0x0400548F RID: 21647
	private readonly List<Network.ShopSection> m_sections = new List<Network.ShopSection>();

	// Token: 0x04005490 RID: 21648
	private readonly Dictionary<int, Network.ShopSale> m_sales = new Dictionary<int, Network.ShopSale>();

	// Token: 0x04005491 RID: 21649
	private long? m_goldCostArena;

	// Token: 0x04005492 RID: 21650
	private global::Currency m_currency = new global::Currency();

	// Token: 0x04005493 RID: 21651
	private readonly HashSet<long> m_transactionIDsConclusivelyHandled = new HashSet<long>();

	// Token: 0x04005494 RID: 21652
	private readonly global::Map<ShopType, IStore> m_stores = new global::Map<ShopType, IStore>();

	// Token: 0x04005495 RID: 21653
	private ShopType m_currentShopType;

	// Token: 0x04005496 RID: 21654
	private bool m_ignoreProductTiming;

	// Token: 0x04005497 RID: 21655
	private readonly global::Map<global::CurrencyType, CurrencyCache> m_currencyCaches = new global::Map<global::CurrencyType, CurrencyCache>();

	// Token: 0x04005498 RID: 21656
	private float m_showStoreStart;

	// Token: 0x04005499 RID: 21657
	private Network.PurchaseMethod m_challengePurchaseMethod;

	// Token: 0x0400549A RID: 21658
	private constants.BnetRegion m_regionId;

	// Token: 0x0400549B RID: 21659
	private StorePackId m_currentlySelectedId;

	// Token: 0x0400549C RID: 21660
	private bool m_canCloseConfirmation = true;

	// Token: 0x0400549D RID: 21661
	private string m_shopPageId;

	// Token: 0x0400549E RID: 21662
	private bool m_openWhenLastEventFired;

	// Token: 0x040054A6 RID: 21670
	private StoreManager.TransactionStatus m_status;

	// Token: 0x040054A7 RID: 21671
	private bool m_waitingToShowStore;

	// Token: 0x040054A8 RID: 21672
	private StoreManager.ShowStoreData m_showStoreData;

	// Token: 0x040054A9 RID: 21673
	private MoneyOrGTAPPTransaction m_activeMoneyOrGTAPPTransaction;

	// Token: 0x040054AA RID: 21674
	private BuyProductEventArgs m_pendingProductPurchaseArgs;

	// Token: 0x040054AB RID: 21675
	private readonly HashSet<long> m_confirmedTransactionIDs = new HashSet<long>();

	// Token: 0x040054AC RID: 21676
	private readonly List<NetCache.ProfileNoticePurchase> m_outstandingPurchaseNotices = new List<NetCache.ProfileNoticePurchase>();

	// Token: 0x040054AD RID: 21677
	private List<Achievement> m_completedAchieves = new List<Achievement>();

	// Token: 0x040054AE RID: 21678
	private bool m_licenseAchievesListenerRegistered;

	// Token: 0x040054AF RID: 21679
	private StoreManager.TransactionStatus m_previousStatusBeforeAutoCancel;

	// Token: 0x040054B0 RID: 21680
	private static readonly PlatformDependentValue<bool> HAS_THIRD_PARTY_APP_STORE = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		PC = false,
		Mac = false,
		iOS = true,
		Android = true
	};

	// Token: 0x020022BD RID: 8893
	private enum PurchaseErrorSource
	{
		// Token: 0x0400E482 RID: 58498
		FROM_PURCHASE_METHOD_RESPONSE,
		// Token: 0x0400E483 RID: 58499
		FROM_STATUS_OR_PURCHASE_RESPONSE,
		// Token: 0x0400E484 RID: 58500
		FROM_PREVIOUS_PURCHASE
	}

	// Token: 0x020022BE RID: 8894
	private enum TransactionStatus
	{
		// Token: 0x0400E486 RID: 58502
		UNKNOWN,
		// Token: 0x0400E487 RID: 58503
		IN_PROGRESS_MONEY,
		// Token: 0x0400E488 RID: 58504
		IN_PROGRESS_GOLD_GTAPP,
		// Token: 0x0400E489 RID: 58505
		IN_PROGRESS_GOLD_NO_GTAPP,
		// Token: 0x0400E48A RID: 58506
		READY,
		// Token: 0x0400E48B RID: 58507
		WAIT_ZERO_COST_LICENSE,
		// Token: 0x0400E48C RID: 58508
		WAIT_METHOD_OF_PAYMENT,
		// Token: 0x0400E48D RID: 58509
		WAIT_CONFIRM,
		// Token: 0x0400E48E RID: 58510
		WAIT_RISK,
		// Token: 0x0400E48F RID: 58511
		CHALLENGE_SUBMITTED,
		// Token: 0x0400E490 RID: 58512
		CHALLENGE_CANCELED,
		// Token: 0x0400E491 RID: 58513
		USER_CANCELING,
		// Token: 0x0400E492 RID: 58514
		AUTO_CANCELING,
		// Token: 0x0400E493 RID: 58515
		IN_PROGRESS_BLIZZARD_CHECKOUT,
		// Token: 0x0400E494 RID: 58516
		WAIT_BLIZZARD_CHECKOUT
	}

	// Token: 0x020022BF RID: 8895
	private enum LicenseStatus
	{
		// Token: 0x0400E496 RID: 58518
		NOT_OWNED,
		// Token: 0x0400E497 RID: 58519
		OWNED,
		// Token: 0x0400E498 RID: 58520
		OWNED_AND_BLOCKING,
		// Token: 0x0400E499 RID: 58521
		UNDEFINED
	}

	// Token: 0x020022C0 RID: 8896
	private struct ShowStoreData
	{
		// Token: 0x0400E49A RID: 58522
		public bool isTotallyFake;

		// Token: 0x0400E49B RID: 58523
		public Store.ExitCallback exitCallback;

		// Token: 0x0400E49C RID: 58524
		public object exitCallbackUserData;

		// Token: 0x0400E49D RID: 58525
		public ProductType storeProduct;

		// Token: 0x0400E49E RID: 58526
		public int storeProductData;

		// Token: 0x0400E49F RID: 58527
		public GeneralStoreMode storeMode;

		// Token: 0x0400E4A0 RID: 58528
		public int numItemsRequired;

		// Token: 0x0400E4A1 RID: 58529
		public bool useOverlayUI;

		// Token: 0x0400E4A2 RID: 58530
		public int pmtProductId;

		// Token: 0x0400E4A3 RID: 58531
		public bool closeOnTransactionComplete;

		// Token: 0x0400E4A4 RID: 58532
		public IDataModel dataModel;
	}
}
