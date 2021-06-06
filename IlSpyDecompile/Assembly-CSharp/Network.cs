using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using bgs;
using bgs.types;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Blizzard.Telemetry.WTCG.Client;
using bnet.protocol;
using bnet.protocol.matchmaking.v1;
using bnet.protocol.v2;
using BobNetProto;
using Hearthstone;
using Hearthstone.Login;
using Hearthstone.Streaming;
using HSCachedDeckCompletion;
using Networking;
using PegasusFSG;
using PegasusGame;
using PegasusShared;
using PegasusUtil;
using SpectatorProto;
using UnityEngine;

public class Network : IService, IHasUpdate
{
	private class HSClientInterface : ClientInterface
	{
		private string s_tempCachePath = Application.temporaryCachePath;

		public string GetVersion()
		{
			return Network.GetVersion();
		}

		public string GetUserAgent()
		{
			string text = "Hearthstone/";
			text = text + "20.4." + 84593 + " (";
			text = ((PlatformSettings.OS == OSCategory.iOS) ? (text + "iOS;") : ((PlatformSettings.OS == OSCategory.Android) ? (text + "Android;") : ((PlatformSettings.OS == OSCategory.PC) ? (text + "PC;") : ((PlatformSettings.OS != OSCategory.Mac) ? (text + "UNKNOWN;") : (text + "Mac;")))));
			text = string.Concat(text, CleanUserAgentString(SystemInfo.deviceModel), ";", SystemInfo.deviceType, ";", CleanUserAgentString(SystemInfo.deviceUniqueIdentifier), ";", SystemInfo.graphicsDeviceID, ";", CleanUserAgentString(SystemInfo.graphicsDeviceName), ";", CleanUserAgentString(SystemInfo.graphicsDeviceVendor), ";", SystemInfo.graphicsDeviceVendorID, ";", CleanUserAgentString(SystemInfo.graphicsDeviceVersion), ";", SystemInfo.graphicsMemorySize, ";", SystemInfo.graphicsShaderLevel, ";", SystemInfo.npotSupport, ";", CleanUserAgentString(SystemInfo.operatingSystem), ";", SystemInfo.processorCount, ";", CleanUserAgentString(SystemInfo.processorType), ";", SystemInfo.supportedRenderTargetCount, ";", SystemInfo.supports3DTextures.ToString(), ";", SystemInfo.supportsAccelerometer.ToString(), ";", SystemInfo.supportsComputeShaders.ToString(), ";", SystemInfo.supportsGyroscope.ToString(), ";", SystemInfo.supportsImageEffects.ToString(), ";", SystemInfo.supportsInstancing.ToString(), ";", SystemInfo.supportsLocationService.ToString(), ";", SystemInfo.supportsRenderTextures.ToString(), ";", SystemInfo.supportsRenderToCubemap.ToString(), ";", SystemInfo.supportsShadows.ToString(), ";", SystemInfo.supportsSparseTextures.ToString(), ";", SystemInfo.supportsStencil, ";", SystemInfo.supportsVibration.ToString(), ";", SystemInfo.systemMemorySize, ";", SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf).ToString(), ";", SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB4444).ToString(), ";", SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth).ToString(), ";", SystemInfo.graphicsDeviceVersion.StartsWith("Metal").ToString(), ";", Screen.currentResolution.width, ";", Screen.currentResolution.height, ";", Screen.dpi, ";");
			text = ((!PlatformSettings.IsMobile()) ? (text + "Desktop;") : ((!UniversalInputManager.UsePhoneUI) ? (text + "Tablet;") : (text + "Phone;")));
			text += Application.genuine;
			text += ") Battle.net/CSharp";
			Log.Net.Print(text);
			return text;
		}

		public int GetApplicationVersion()
		{
			return 84593;
		}

		private string CleanUserAgentString(string data)
		{
			return Regex.Replace(data, "[^a-zA-Z0-9_.]+", "_");
		}

		public string GetBasePersistentDataPath()
		{
			return FileUtils.PersistentDataPath;
		}

		public string GetTemporaryCachePath()
		{
			return s_tempCachePath;
		}

		public bool GetDisableConnectionMetering()
		{
			return Vars.Key("Aurora.DisableConnectionMetering").GetBool(def: false);
		}

		public constants.MobileEnv GetMobileEnvironment()
		{
			MobileEnv mobileEnvironment = HearthstoneApplication.GetMobileEnvironment();
			if (mobileEnvironment == MobileEnv.PRODUCTION)
			{
				return constants.MobileEnv.PRODUCTION;
			}
			return constants.MobileEnv.DEVELOPMENT;
		}

		public string GetAuroraVersionName()
		{
			return 84593.ToString();
		}

		public string GetLocaleName()
		{
			return Localization.GetLocaleName();
		}

		public string GetPlatformName()
		{
			return "Win";
		}

		public constants.RuntimeEnvironment GetRuntimeEnvironment()
		{
			return constants.RuntimeEnvironment.Mono;
		}

		public IUrlDownloader GetUrlDownloader()
		{
			return s_urlDownloader;
		}

		public int GetDataVersion()
		{
			return GameDbf.GetDataVersion();
		}
	}

	public enum BnetLoginState
	{
		BATTLE_NET_UNKNOWN,
		BATTLE_NET_LOGGING_IN,
		BATTLE_NET_TIMEOUT,
		BATTLE_NET_LOGIN_FAILED,
		BATTLE_NET_LOGGED_IN
	}

	public enum BoosterSource
	{
		UNKNOWN = 0,
		ARENA_REWARD = 3,
		BOUGHT = 4,
		LICENSED = 6,
		CS_GIFT = 8,
		QUEST_REWARD = 10,
		BOUGHT_GOLD = 11
	}

	public enum Version
	{
		Major = 20,
		Minor = 4,
		Patch = 0,
		Sku = 0
	}

	public enum AuthResult
	{
		UNKNOWN,
		ALLOWED,
		INVALID,
		SECOND,
		OFFLINE
	}

	public class ConnectErrorParams : ErrorParams
	{
		public float m_creationTime;

		public ConnectErrorParams()
		{
			m_creationTime = Time.realtimeSinceStartup;
		}
	}

	private class RequestContext
	{
		public float m_waitUntil;

		public int m_pendingResponseId;

		public int m_requestId;

		public int m_requestSubId;

		public TimeoutHandler m_timeoutHandler;

		public RequestContext(int pendingResponseId, int requestId, int requestSubId, TimeoutHandler timeoutHandler)
		{
			m_waitUntil = Time.realtimeSinceStartup + GetMaxDeferredWait();
			m_pendingResponseId = pendingResponseId;
			m_requestId = requestId;
			m_requestSubId = requestSubId;
			m_timeoutHandler = timeoutHandler;
		}
	}

	public class UnavailableReason
	{
		public string mainReason;

		public string subReason;

		public string extraData;
	}

	private class BnetErrorListener : EventListener<BnetErrorCallback>
	{
		public bool Fire(BnetErrorInfo info)
		{
			return m_callback(info, m_userData);
		}
	}

	public delegate void NetHandler();

	public delegate void ThrottledPacketListener(int packetID, long retryMillis);

	public delegate void QueueInfoHandler(QueueInfo queueInfo);

	public delegate void GameQueueHandler(QueueEvent queueEvent);

	public delegate void TimeoutHandler(int pendingResponseId, int requestId, int requestSubId);

	public delegate void BnetEventHandler(BattleNet.BnetEvent[] updates);

	public delegate void FriendsHandler(FriendsUpdate[] updates);

	public delegate void WhisperHandler(BnetWhisper[] whispers);

	public delegate void PartyHandler(PartyEvent[] updates);

	public delegate void PresenceHandler(PresenceUpdate[] updates);

	public delegate void ShutdownHandler(int minutes);

	public delegate void ChallengeHandler(ChallengeInfo[] challenges);

	public delegate void SpectatorInviteReceivedHandler(Invite invite);

	public delegate bool BnetErrorCallback(BnetErrorInfo info, object userData);

	public delegate void GameServerDisconnectEvent(BattleNetErrors errorCode);

	private struct NetworkState
	{
		public BattleNetLogSource LogSource { get; set; }

		public BnetGameType FindingBnetGameType { get; set; }

		public float LastCall { get; set; }

		public float LastCallReport { get; set; }

		public int LastCallFrame { get; set; }

		public BnetEventHandler CurrentBnetEventHandler { get; set; }

		public FriendsHandler CurrentFriendsHandler { get; set; }

		public WhisperHandler CurrentWhisperHandler { get; set; }

		public PresenceHandler CurrentPresenceHandler { get; set; }

		public ShutdownHandler CurrentShutdownHandler { get; set; }

		public ChallengeHandler CurrentChallengeHandler { get; set; }

		public Map<BnetFeature, List<BnetErrorListener>> FeatureBnetErrorListeners { get; set; }

		public List<BnetErrorListener> GlobalBnetErrorListeners { get; set; }

		public GameServerDisconnectEvent GameServerDisconnectEventListener { get; set; }

		public FindGameResult LastFindGameParameters { get; set; }

		public ConnectToGameServer LastConnectToGameServerInfo { get; set; }

		public GameServerInfo LastGameServerInfo { get; set; }

		public string DelayedError { get; set; }

		public float TimeBeforeAllowReset { get; set; }

		public List<ClientStateNotification> QueuedClientStateNotifications { get; set; }

		public bgs.types.EntityId CachedGameAccountId { get; set; }

		public constants.BnetRegion CachedRegion { get; set; }

		public int CurrentCreateDeckRequestId { get; set; }

		public HashSet<int> InTransitOfflineCreateDeckRequestIds { get; set; }

		public HashSet<long> DeckIdsWaitingToDiffAgainstOfflineCache { get; set; }

		public Map<int, TimeoutHandler> NetTimeoutHandlers { get; set; }

		public void SetDefaults()
		{
			LogSource = new BattleNetLogSource("Network");
			FindingBnetGameType = BnetGameType.BGT_UNKNOWN;
			LastCall = Time.realtimeSinceStartup;
			LastCallReport = Time.realtimeSinceStartup;
			LastCallFrame = 0;
			FeatureBnetErrorListeners = new Map<BnetFeature, List<BnetErrorListener>>();
			GlobalBnetErrorListeners = new List<BnetErrorListener>();
			QueuedClientStateNotifications = new List<ClientStateNotification>();
			InTransitOfflineCreateDeckRequestIds = new HashSet<int>();
			DeckIdsWaitingToDiffAgainstOfflineCache = new HashSet<long>();
			NetTimeoutHandlers = new Map<int, TimeoutHandler>();
		}
	}

	public class QueueInfo
	{
		public int position;

		public long secondsTilEnd;

		public long stdev;
	}

	public class CanceledQuest
	{
		public int AchieveID { get; set; }

		public bool Canceled { get; set; }

		public long NextQuestCancelDate { get; set; }

		public CanceledQuest()
		{
			AchieveID = 0;
			Canceled = false;
			NextQuestCancelDate = 0L;
		}

		public override string ToString()
		{
			return $"[CanceledQuest AchieveID={AchieveID} Canceled={Canceled} NextQuestCancelDate={NextQuestCancelDate}]";
		}
	}

	public class TriggeredEvent
	{
		public int EventID { get; set; }

		public bool Success { get; set; }

		public TriggeredEvent()
		{
			EventID = 0;
			Success = false;
		}
	}

	public class AdventureProgress
	{
		public int Wing { get; set; }

		public int Progress { get; set; }

		public int Ack { get; set; }

		public ulong Flags { get; set; }

		public AdventureProgress()
		{
			Wing = 0;
			Progress = 0;
			Ack = 0;
			Flags = 0uL;
		}
	}

	public class CardSaleResult
	{
		public enum SaleResult
		{
			GENERIC_FAILURE = 1,
			CARD_WAS_SOLD,
			CARD_WAS_BOUGHT,
			SOULBOUND,
			FAILED_WRONG_SELL_PRICE,
			FAILED_WRONG_BUY_PRICE,
			FAILED_NO_PERMISSION,
			FAILED_EVENT_NOT_ACTIVE,
			COUNT_MISMATCH
		}

		public SaleResult Action { get; set; }

		public int AssetID { get; set; }

		public string AssetName { get; set; }

		public TAG_PREMIUM Premium { get; set; }

		public int Amount { get; set; }

		public int Count { get; set; }

		public bool Nerfed { get; set; }

		public int UnitSellPrice { get; set; }

		public int UnitBuyPrice { get; set; }

		public int? CurrentCollectionCount { get; set; }

		public override string ToString()
		{
			return $"[CardSaleResult Action={Action} assetName={AssetName} premium={Premium} amount={Amount} count={Count}]";
		}
	}

	public class BeginDraft
	{
		public long DeckID { get; set; }

		public List<NetCache.CardDefinition> Heroes { get; set; }

		public int Wins { get; set; }

		public int MaxSlot { get; set; }

		public ArenaSession Session { get; set; }

		public DraftSlotType SlotType { get; set; }

		public List<DraftSlotType> UniqueSlotTypesForDraft { get; set; }

		public BeginDraft()
		{
			Heroes = new List<NetCache.CardDefinition>();
		}
	}

	public class DraftChoicesAndContents
	{
		public int Slot { get; set; }

		public List<NetCache.CardDefinition> Choices { get; set; }

		public NetCache.CardDefinition Hero { get; set; }

		public NetCache.CardDefinition HeroPower { get; set; }

		public DeckContents DeckInfo { get; set; }

		public int Wins { get; set; }

		public int Losses { get; set; }

		public RewardChest Chest { get; set; }

		public int MaxWins { get; set; }

		public int MaxSlot { get; set; }

		public ArenaSession Session { get; set; }

		public DraftSlotType SlotType { get; set; }

		public List<DraftSlotType> UniqueSlotTypesForDraft { get; set; }

		public DraftChoicesAndContents()
		{
			Choices = new List<NetCache.CardDefinition>();
			Hero = new NetCache.CardDefinition();
			HeroPower = new NetCache.CardDefinition();
			DeckInfo = new DeckContents();
			Chest = null;
			UniqueSlotTypesForDraft = new List<DraftSlotType>();
		}
	}

	public class DraftChosen
	{
		public NetCache.CardDefinition ChosenCard { get; set; }

		public List<NetCache.CardDefinition> NextChoices { get; set; }

		public DraftSlotType SlotType { get; set; }

		public DraftChosen()
		{
			ChosenCard = new NetCache.CardDefinition();
			NextChoices = new List<NetCache.CardDefinition>();
		}
	}

	public class RewardChest
	{
		public List<RewardData> Rewards { get; set; }

		public RewardChest()
		{
			Rewards = new List<RewardData>();
		}
	}

	public class DraftRetired
	{
		public long Deck { get; set; }

		public RewardChest Chest { get; set; }

		public DraftRetired()
		{
			Deck = 0L;
			Chest = new RewardChest();
		}
	}

	public class MassDisenchantResponse
	{
		public int Amount { get; set; }

		public MassDisenchantResponse()
		{
			Amount = 0;
		}
	}

	public class SetFavoriteHeroResponse
	{
		public bool Success;

		public TAG_CLASS HeroClass;

		public NetCache.CardDefinition Hero;

		public SetFavoriteHeroResponse()
		{
			Success = false;
			HeroClass = TAG_CLASS.INVALID;
			Hero = null;
		}
	}

	public class PurchaseErrorInfo
	{
		public enum ErrorType
		{
			UNKNOWN = -1,
			SUCCESS = 0,
			STILL_IN_PROGRESS = 1,
			INVALID_BNET = 2,
			SERVICE_NA = 3,
			PURCHASE_IN_PROGRESS = 4,
			DATABASE = 5,
			INVALID_QUANTITY = 6,
			DUPLICATE_LICENSE = 7,
			REQUEST_NOT_SENT = 8,
			NO_ACTIVE_BPAY = 9,
			FAILED_RISK = 10,
			CANCELED = 11,
			WAIT_MOP = 12,
			WAIT_CONFIRM = 13,
			WAIT_RISK = 14,
			PRODUCT_NA = 0xF,
			RISK_TIMEOUT = 0x10,
			PRODUCT_ALREADY_OWNED = 17,
			WAIT_THIRD_PARTY_RECEIPT = 18,
			PRODUCT_EVENT_HAS_ENDED = 19,
			BP_GENERIC_FAIL = 100,
			BP_INVALID_CC_EXPIRY = 101,
			BP_RISK_ERROR = 102,
			BP_NO_VALID_PAYMENT = 103,
			BP_PAYMENT_AUTH = 104,
			BP_PROVIDER_DENIED = 105,
			BP_PURCHASE_BAN = 106,
			BP_SPENDING_LIMIT = 107,
			BP_PARENTAL_CONTROL = 108,
			BP_THROTTLED = 109,
			BP_THIRD_PARTY_BAD_RECEIPT = 110,
			BP_THIRD_PARTY_RECEIPT_USED = 111,
			BP_PRODUCT_UNIQUENESS_VIOLATED = 112,
			BP_REGION_IS_DOWN = 113,
			E_BP_GENERIC_FAIL_RETRY_CONTACT_CS_IF_PERSISTS = 115,
			E_BP_CHALLENGE_ID_FAILED_VERIFICATION = 116
		}

		public ErrorType Error { get; set; }

		public string PurchaseInProgressProductID { get; set; }

		public string ErrorCode { get; set; }

		public PurchaseErrorInfo()
		{
			Error = ErrorType.UNKNOWN;
			PurchaseInProgressProductID = string.Empty;
			ErrorCode = string.Empty;
		}
	}

	public class PurchaseCanceledResponse
	{
		public enum CancelResult
		{
			SUCCESS,
			NOT_ALLOWED,
			NOTHING_TO_CANCEL
		}

		public CancelResult Result { get; set; }

		public long TransactionID { get; set; }

		public long? PMTProductID { get; set; }

		public string CurrencyCode { get; set; }
	}

	public class BattlePayStatus
	{
		public enum PurchaseState
		{
			UNKNOWN = -1,
			READY,
			CHECK_RESULTS,
			ERROR
		}

		public PurchaseState State { get; set; }

		public long TransactionID { get; set; }

		public string ThirdPartyID { get; set; }

		public long? PMTProductID { get; set; }

		public PurchaseErrorInfo PurchaseError { get; set; }

		public bool BattlePayAvailable { get; set; }

		public string CurrencyCode { get; set; }

		public BattlePayProvider? Provider { get; set; }

		public BattlePayStatus()
		{
			State = PurchaseState.UNKNOWN;
			TransactionID = 0L;
			ThirdPartyID = string.Empty;
			PMTProductID = null;
			PurchaseError = new PurchaseErrorInfo();
			BattlePayAvailable = false;
			Provider = MoneyOrGTAPPTransaction.UNKNOWN_PROVIDER;
		}
	}

	public class BundleItem
	{
		public ProductType ItemType { get; set; }

		public int ProductData { get; set; }

		public int Quantity { get; set; }

		public int BaseQuantity { get; set; }

		public Dictionary<string, string> Attributes { get; set; }

		public BundleItem()
		{
			ItemType = ProductType.PRODUCT_TYPE_UNKNOWN;
			ProductData = 0;
			Quantity = 0;
			BaseQuantity = 0;
			Attributes = new Dictionary<string, string>();
		}

		public override bool Equals(object obj)
		{
			BundleItem bundleItem = obj as BundleItem;
			if (bundleItem == null)
			{
				return false;
			}
			if (bundleItem.ItemType != ItemType)
			{
				return false;
			}
			if (bundleItem.ProductData != ProductData)
			{
				return false;
			}
			if (bundleItem.BaseQuantity != BaseQuantity)
			{
				return false;
			}
			if (bundleItem.Quantity != Quantity)
			{
				return false;
			}
			if (bundleItem.Attributes != Attributes)
			{
				return false;
			}
			return true;
		}

		public override int GetHashCode()
		{
			return ItemType.GetHashCode() * ProductData.GetHashCode() * Quantity.GetHashCode();
		}
	}

	public class Bundle
	{
		public ulong? Cost { get; set; }

		public double? CostDisplay { get; set; }

		public long? GtappGoldCost { get; set; }

		public long? VirtualCurrencyCost { get; set; }

		public string VirtualCurrencyCode { get; set; }

		public string AppleID { get; set; }

		public string GooglePlayID { get; set; }

		public string AmazonID { get; set; }

		public string OneStoreID { get; set; }

		public List<BundleItem> Items { get; set; }

		public string ProductEvent { get; set; }

		public List<BattlePayProvider> ExclusiveProviders { get; set; }

		public bool IsPrePurchase { get; set; }

		public long? PMTProductID { get; set; }

		public DbfLocValue DisplayName { get; set; }

		public DbfLocValue DisplayDescription { get; set; }

		public Dictionary<string, string> Attributes { get; set; }

		public List<int> SaleIds { get; set; }

		public bool VisibleOnSalePeriodOnly { get; set; }

		public Bundle()
		{
			AppleID = string.Empty;
			GooglePlayID = string.Empty;
			AmazonID = string.Empty;
			Items = new List<BundleItem>();
		}
	}

	public class ShopSection
	{
		public class ProductRef
		{
			public long PmtId { get; set; }

			public int OrderId { get; set; }
		}

		public string InternalName { get; set; }

		public DbfLocValue Label { get; set; }

		public string Style { get; set; }

		public string FillerTags { get; set; }

		public int SortOrder { get; set; }

		public List<ProductRef> Products { get; set; }
	}

	public class ShopSale
	{
		public int SaleId { get; set; }

		public DateTime? StartUtc { get; set; }

		public DateTime? SoftEndUtc { get; set; }

		public DateTime? HardEndUtc { get; set; }
	}

	public class GoldCostBooster
	{
		public long? Cost { get; set; }

		public int ID { get; set; }

		public SpecialEventType BuyWithGoldEvent { get; set; }

		public GoldCostBooster()
		{
			Cost = null;
			ID = 0;
			BuyWithGoldEvent = SpecialEventType.UNKNOWN;
		}
	}

	public class BattlePayConfig
	{
		public bool Available { get; set; }

		public Currency Currency { get; set; }

		public List<Currency> Currencies { get; set; }

		public List<Bundle> Bundles { get; set; }

		public List<GoldCostBooster> GoldCostBoosters { get; set; }

		public long? GoldCostArena { get; set; }

		public int SecondsBeforeAutoCancel { get; set; }

		public string CommerceClientID { get; set; }

		public string PersonalizedShopPageID { get; set; }

		public Map<long, Locale> CatalogLocaleToGameLocale { get; set; }

		public List<ShopSale> SaleList { get; set; }

		public bool IgnoreProductTiming { get; set; }

		public string CheckoutKrOnestoreKey { get; set; }

		public BattlePayConfig()
		{
			Available = false;
			Currencies = new List<Currency>();
			Bundles = new List<Bundle>();
			GoldCostBoosters = new List<GoldCostBooster>();
			GoldCostArena = null;
			SecondsBeforeAutoCancel = StoreManager.DEFAULT_SECONDS_BEFORE_AUTO_CANCEL;
			CommerceClientID = "df5787f96b2b46c49c66dd45bcb05490";
			PersonalizedShopPageID = null;
			CatalogLocaleToGameLocale = new Map<long, Locale>();
			SaleList = new List<ShopSale>();
			IgnoreProductTiming = false;
			CheckoutKrOnestoreKey = null;
		}
	}

	public class PurchaseViaGoldResponse
	{
		public enum ErrorType
		{
			UNKNOWN = -1,
			SUCCESS = 1,
			INSUFFICIENT_GOLD = 2,
			PRODUCT_NA = 3,
			FEATURE_NA = 4,
			INVALID_QUANTITY = 5
		}

		public ErrorType Error { get; set; }

		public long GoldUsed { get; set; }

		public PurchaseViaGoldResponse()
		{
			Error = ErrorType.UNKNOWN;
			GoldUsed = 0L;
		}
	}

	public class PurchaseMethod
	{
		public long TransactionID { get; set; }

		public long? PMTProductID { get; set; }

		public int Quantity { get; set; }

		public string CurrencyCode { get; set; }

		public string WalletName { get; set; }

		public bool UseEBalance { get; set; }

		public bool IsZeroCostLicense { get; set; }

		public string ChallengeID { get; set; }

		public string ChallengeURL { get; set; }

		public PurchaseErrorInfo PurchaseError { get; set; }

		public PurchaseMethod()
		{
			TransactionID = 0L;
			PMTProductID = null;
			Quantity = 0;
			CurrencyCode = string.Empty;
			WalletName = string.Empty;
			UseEBalance = false;
			IsZeroCostLicense = false;
			ChallengeID = string.Empty;
			ChallengeURL = string.Empty;
			PurchaseError = null;
		}
	}

	public class PurchaseResponse
	{
		public PurchaseErrorInfo PurchaseError { get; set; }

		public long TransactionID { get; set; }

		public long? PMTProductID { get; set; }

		public string ThirdPartyID { get; set; }

		public string CurrencyCode { get; set; }

		public PurchaseResponse()
		{
			PurchaseError = new PurchaseErrorInfo();
			TransactionID = 0L;
			PMTProductID = null;
			ThirdPartyID = string.Empty;
		}
	}

	public class ThirdPartyPurchaseStatusResponse
	{
		public enum PurchaseStatus
		{
			UNKNOWN = -1,
			NOT_FOUND = 1,
			SUCCEEDED = 2,
			FAILED = 3,
			IN_PROGRESS = 4
		}

		public string ThirdPartyID { get; set; }

		public PurchaseStatus Status { get; set; }

		public ThirdPartyPurchaseStatusResponse()
		{
			ThirdPartyID = string.Empty;
			Status = PurchaseStatus.UNKNOWN;
		}
	}

	public class CardBackResponse
	{
		public bool Success { get; set; }

		public int CardBack { get; set; }

		public CardBackResponse()
		{
			Success = false;
			CardBack = 0;
		}
	}

	public class CoinResponse
	{
		public bool Success { get; set; }

		public int Coin { get; set; }

		public CoinResponse()
		{
			Success = false;
			Coin = 1;
		}
	}

	public class GameCancelInfo
	{
		public enum Reason
		{
			OPPONENT_TIMEOUT = 1,
			PLAYER_LOADING_TIMEOUT,
			PLAYER_LOADING_DISCONNECTED
		}

		public Reason CancelReason { get; set; }
	}

	public class Entity
	{
		public class Tag
		{
			public int Name { get; set; }

			public int Value { get; set; }
		}

		public int ID { get; set; }

		public List<Tag> Tags { get; set; }

		public List<Tag> DefTags { get; set; }

		public string CardID { get; set; }

		public Entity()
		{
			Tags = new List<Tag>();
			DefTags = new List<Tag>();
		}

		public static Entity CreateFromProto(PegasusGame.Entity src)
		{
			return new Entity
			{
				ID = src.Id,
				CardID = string.Empty,
				Tags = CreateTagsFromProto(src.Tags)
			};
		}

		public static Entity CreateFromProto(PowerHistoryEntity src)
		{
			return new Entity
			{
				ID = src.Entity,
				CardID = src.Name,
				Tags = CreateTagsFromProto(src.Tags),
				DefTags = CreateTagsFromProto(src.DefTags)
			};
		}

		public static List<Tag> CreateTagsFromProto(IList<PegasusGame.Tag> tagList)
		{
			List<Tag> list = new List<Tag>();
			for (int i = 0; i < tagList.Count; i++)
			{
				PegasusGame.Tag tag = tagList[i];
				list.Add(new Tag
				{
					Name = tag.Name,
					Value = tag.Value
				});
			}
			return list;
		}

		public override string ToString()
		{
			return $"id={ID} cardId={CardID} tags={Tags.Count}";
		}
	}

	public class Options
	{
		public class Option
		{
			public enum OptionType
			{
				PASS = 1,
				END_TURN,
				POWER
			}

			public class PlayErrorInfo
			{
				public PlayErrors.ErrorType PlayError { get; set; }

				public int? PlayErrorParam { get; set; }

				public PlayErrorInfo()
				{
					PlayError = PlayErrors.ErrorType.INVALID;
					PlayErrorParam = null;
				}

				public bool IsValid()
				{
					return PlayError == PlayErrors.ErrorType.NONE;
				}
			}

			public class TargetOption
			{
				public int ID { get; set; }

				public PlayErrorInfo PlayErrorInfo { get; set; }

				public TargetOption()
				{
					ID = 0;
					PlayErrorInfo = new PlayErrorInfo();
				}

				public void CopyFrom(TargetOption targetOption)
				{
					ID = targetOption.ID;
					PlayErrorInfo = targetOption.PlayErrorInfo;
				}

				public void CopyFrom(PegasusGame.TargetOption targetOption)
				{
					ID = targetOption.Id;
					PlayErrorInfo.PlayError = (PlayErrors.ErrorType)targetOption.PlayError;
					PlayErrorInfo.PlayErrorParam = (targetOption.HasPlayErrorParam ? new int?(targetOption.PlayErrorParam) : null);
				}
			}

			public class SubOption
			{
				public int ID { get; set; }

				public List<TargetOption> Targets { get; set; }

				public PlayErrorInfo PlayErrorInfo { get; set; }

				public SubOption()
				{
					ID = 0;
					PlayErrorInfo = new PlayErrorInfo();
				}

				public bool IsValidTarget(int entityID)
				{
					if (Targets == null)
					{
						return false;
					}
					for (int i = 0; i < Targets.Count; i++)
					{
						if (Targets[i].ID == entityID && Targets[i].PlayErrorInfo.IsValid())
						{
							return true;
						}
					}
					return false;
				}

				public PlayErrors.ErrorType GetErrorForTarget(int entityID)
				{
					if (Targets == null)
					{
						return PlayErrors.ErrorType.INVALID;
					}
					for (int i = 0; i < Targets.Count; i++)
					{
						if (Targets[i].ID == entityID)
						{
							return Targets[i].PlayErrorInfo.PlayError;
						}
					}
					return PlayErrors.ErrorType.INVALID;
				}

				public int? GetErrorParamForTarget(int entityID)
				{
					if (Targets == null)
					{
						return null;
					}
					for (int i = 0; i < Targets.Count; i++)
					{
						if (Targets[i].ID == entityID)
						{
							return Targets[i].PlayErrorInfo.PlayErrorParam;
						}
					}
					return null;
				}

				public bool HasValidTarget()
				{
					if (Targets == null)
					{
						return false;
					}
					for (int i = 0; i < Targets.Count; i++)
					{
						if (Targets[i].PlayErrorInfo.IsValid())
						{
							return true;
						}
					}
					return false;
				}

				public void CopyFrom(SubOption subOption)
				{
					ID = subOption.ID;
					PlayErrorInfo = subOption.PlayErrorInfo;
					if (subOption.Targets == null)
					{
						Targets = null;
						return;
					}
					if (Targets == null)
					{
						Targets = new List<TargetOption>();
					}
					else
					{
						Targets.Clear();
					}
					for (int i = 0; i < subOption.Targets.Count; i++)
					{
						TargetOption targetOption = new TargetOption();
						targetOption.CopyFrom(subOption.Targets[i]);
						Targets.Add(targetOption);
					}
				}
			}

			public OptionType Type { get; set; }

			public SubOption Main { get; set; }

			public List<SubOption> Subs { get; set; }

			public Option()
			{
				Main = new SubOption();
				Subs = new List<SubOption>();
			}

			public SubOption GetSubOptionFromEntityID(int entityID)
			{
				for (int i = 0; i < Subs.Count; i++)
				{
					if (Subs[i].ID == entityID)
					{
						return Subs[i];
					}
				}
				return null;
			}

			public bool HasValidSubOption()
			{
				for (int i = 0; i < Subs.Count; i++)
				{
					if (Subs[i].PlayErrorInfo.IsValid())
					{
						return true;
					}
				}
				return false;
			}

			public void CopyFrom(Option option)
			{
				Type = option.Type;
				if (Main == null)
				{
					Main = new SubOption();
				}
				Main.CopyFrom(option.Main);
				if (option.Subs == null)
				{
					Subs = null;
					return;
				}
				if (Subs == null)
				{
					Subs = new List<SubOption>();
				}
				else
				{
					Subs.Clear();
				}
				for (int i = 0; i < option.Subs.Count; i++)
				{
					SubOption subOption = new SubOption();
					subOption.CopyFrom(option.Subs[i]);
					Subs.Add(subOption);
				}
			}
		}

		public int ID { get; set; }

		public List<Option> List { get; set; }

		public Options()
		{
			List = new List<Option>();
		}

		public bool HasValidOption()
		{
			for (int i = 0; i < List.Count; i++)
			{
				if (List[i].Main.PlayErrorInfo.IsValid())
				{
					return true;
				}
			}
			return false;
		}

		public Option GetOptionFromEntityID(int entityID)
		{
			for (int i = 0; i < List.Count; i++)
			{
				if (List[i].Main.ID == entityID)
				{
					return List[i];
				}
			}
			return null;
		}

		public void CopyFrom(Options options)
		{
			ID = options.ID;
			if (options.List == null)
			{
				List = null;
				return;
			}
			if (List != null)
			{
				List.Clear();
			}
			else
			{
				List = new List<Option>();
			}
			for (int i = 0; i < options.List.Count; i++)
			{
				Option option = new Option();
				option.CopyFrom(options.List[i]);
				List.Add(option);
			}
		}
	}

	public class EntityChoices
	{
		public int ID { get; set; }

		public CHOICE_TYPE ChoiceType { get; set; }

		public int CountMin { get; set; }

		public int CountMax { get; set; }

		public List<int> Entities { get; set; }

		public int Source { get; set; }

		public int PlayerId { get; set; }

		public bool HideChosen { get; set; }

		public bool IsSingleChoice()
		{
			if (CountMax == 0)
			{
				return true;
			}
			if (CountMin == 1)
			{
				return CountMax == 1;
			}
			return false;
		}
	}

	public class EntitiesChosen
	{
		public int ID { get; set; }

		public List<int> Entities { get; set; }

		public int PlayerId { get; set; }

		public CHOICE_TYPE ChoiceType { get; set; }
	}

	public class Notification
	{
		public enum Type
		{
			IN_HAND_CARD_CAP = 1,
			MANA_CAP
		}

		public Type NotificationType { get; set; }
	}

	public class GameSetup
	{
		public int Board { get; set; }

		public int MaxSecretZoneSizePerPlayer { get; set; }

		public int MaxSecretsPerPlayer { get; set; }

		public int MaxQuestsPerPlayer { get; set; }

		public int MaxFriendlyMinionsPerPlayer { get; set; }

		public uint DisconnectWhenStuckSeconds { get; set; }
	}

	public class UserUI
	{
		public class MouseInfo
		{
			public int OverCardID { get; set; }

			public int HeldCardID { get; set; }

			public int ArrowOriginID { get; set; }

			public int X { get; set; }

			public int Y { get; set; }
		}

		public class EmoteInfo
		{
			public int Emote { get; set; }
		}

		public MouseInfo mouseInfo;

		public EmoteInfo emoteInfo;

		public int? playerId;
	}

	public enum PowerType
	{
		FULL_ENTITY = 1,
		SHOW_ENTITY,
		HIDE_ENTITY,
		TAG_CHANGE,
		BLOCK_START,
		BLOCK_END,
		CREATE_GAME,
		META_DATA,
		CHANGE_ENTITY,
		RESET_GAME,
		SUB_SPELL_START,
		SUB_SPELL_END,
		VO_SPELL,
		CACHED_TAG_FOR_DORMANT_CHANGE,
		SHUFFLE_DECK
	}

	public class PowerHistory
	{
		public PowerType Type { get; set; }

		public PowerHistory(PowerType init)
		{
			Type = init;
		}

		public override string ToString()
		{
			return $"type={Type}";
		}
	}

	public class HistBlockStart : PowerHistory
	{
		public HistoryBlock.Type BlockType { get; set; }

		public List<int> Entities { get; set; }

		public int Target { get; set; }

		public int SubOption { get; set; }

		public List<string> EffectCardId { get; set; }

		public List<bool> IsEffectCardIdClientCached { get; set; }

		public int EffectIndex { get; set; }

		public int TriggerKeyword { get; set; }

		public bool ShowInHistory { get; set; }

		public bool IsDeferrable { get; set; }

		public bool IsBatchable { get; set; }

		public bool IsDeferBlocker { get; set; }

		public bool ForceShowBigCard { get; set; }

		public HistBlockStart(HistoryBlock.Type type)
			: base(PowerType.BLOCK_START)
		{
			BlockType = type;
		}

		public override string ToString()
		{
			return $"type={base.Type} blockType={BlockType} entity={Entities} target={Target} b={IsBatchable} d={IsDeferrable} xd={IsDeferBlocker} bigCard={ForceShowBigCard}";
		}
	}

	public class HistBlockEnd : PowerHistory
	{
		public HistBlockEnd()
			: base(PowerType.BLOCK_END)
		{
		}
	}

	public class HistCreateGame : PowerHistory
	{
		public class PlayerData
		{
			public int ID { get; set; }

			public BnetGameAccountId GameAccountId { get; set; }

			public Entity Player { get; set; }

			public int CardBackID { get; set; }

			public static PlayerData CreateFromProto(PegasusGame.Player src)
			{
				return new PlayerData
				{
					ID = src.Id,
					GameAccountId = BnetGameAccountId.CreateFromNet(src.GameAccountId),
					Player = Entity.CreateFromProto(src.Entity),
					CardBackID = src.CardBack
				};
			}

			public override string ToString()
			{
				return $"ID={ID} GameAccountId={GameAccountId} Player={Player} CardBackID={CardBackID}";
			}
		}

		public class SharedPlayerInfo
		{
			public int ID { get; set; }

			public BnetGameAccountId GameAccountId { get; set; }

			public static SharedPlayerInfo CreateFromProto(PegasusGame.SharedPlayerInfo src)
			{
				return new SharedPlayerInfo
				{
					ID = src.Id,
					GameAccountId = BnetGameAccountId.CreateFromNet(src.GameAccountId)
				};
			}

			public override string ToString()
			{
				return $"ID={ID} GameAccountId={GameAccountId}";
			}
		}

		public Entity Game { get; set; }

		public string Uuid { get; set; }

		public List<PlayerData> Players { get; set; }

		public List<SharedPlayerInfo> PlayerInfos { get; set; }

		public static HistCreateGame CreateFromProto(PowerHistoryCreateGame src)
		{
			HistCreateGame histCreateGame = new HistCreateGame();
			histCreateGame.Uuid = src.GameUuid;
			histCreateGame.Game = Entity.CreateFromProto(src.GameEntity);
			if (src.Players != null)
			{
				histCreateGame.Players = new List<PlayerData>();
				for (int i = 0; i < src.Players.Count; i++)
				{
					PlayerData item = PlayerData.CreateFromProto(src.Players[i]);
					histCreateGame.Players.Add(item);
				}
			}
			if (src.PlayerInfos != null)
			{
				histCreateGame.PlayerInfos = new List<SharedPlayerInfo>();
				for (int j = 0; j < src.PlayerInfos.Count; j++)
				{
					SharedPlayerInfo item2 = SharedPlayerInfo.CreateFromProto(src.PlayerInfos[j]);
					histCreateGame.PlayerInfos.Add(item2);
				}
			}
			return histCreateGame;
		}

		public HistCreateGame()
			: base(PowerType.CREATE_GAME)
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("game={0}", Game);
			if (Players == null)
			{
				stringBuilder.Append(" players=(null)");
			}
			else if (Players.Count == 0)
			{
				stringBuilder.Append(" players=0");
			}
			else
			{
				for (int i = 0; i < Players.Count; i++)
				{
					stringBuilder.AppendFormat(" players[{0}]=[{1}]", i, Players[i]);
				}
			}
			if (PlayerInfos == null)
			{
				stringBuilder.Append(" playerInfos=(null)");
			}
			else if (PlayerInfos.Count == 0)
			{
				stringBuilder.Append(" playerInfos=0");
			}
			else
			{
				for (int j = 0; j < PlayerInfos.Count; j++)
				{
					stringBuilder.AppendFormat(" playerInfos[{0}]=[{1}]", j, PlayerInfos[j]);
				}
			}
			return stringBuilder.ToString();
		}
	}

	public class HistResetGame : PowerHistory
	{
		public HistCreateGame CreateGame { get; set; }

		public HistResetGame()
			: base(PowerType.RESET_GAME)
		{
		}

		public override string ToString()
		{
			return $"type={base.Type}";
		}

		public static HistResetGame CreateFromProto(PowerHistoryResetGame proto)
		{
			return new HistResetGame
			{
				CreateGame = HistCreateGame.CreateFromProto(proto.CreateGame)
			};
		}
	}

	public class HistFullEntity : PowerHistory
	{
		public Entity Entity { get; set; }

		public HistFullEntity()
			: base(PowerType.FULL_ENTITY)
		{
		}

		public override string ToString()
		{
			return $"type={base.Type} entity=[{Entity}]";
		}
	}

	public class HistShowEntity : PowerHistory
	{
		public Entity Entity { get; set; }

		public HistShowEntity()
			: base(PowerType.SHOW_ENTITY)
		{
		}

		public override string ToString()
		{
			return $"type={base.Type} entity=[{Entity}]";
		}
	}

	public class HistHideEntity : PowerHistory
	{
		public int Entity { get; set; }

		public int Zone { get; set; }

		public HistHideEntity()
			: base(PowerType.HIDE_ENTITY)
		{
		}

		public override string ToString()
		{
			return $"type={base.Type} entity={Entity} zone={Zone}";
		}
	}

	public class HistChangeEntity : PowerHistory
	{
		public Entity Entity { get; set; }

		public HistChangeEntity()
			: base(PowerType.CHANGE_ENTITY)
		{
		}

		public override string ToString()
		{
			return $"type={base.Type} entity=[{Entity}]";
		}
	}

	public class HistTagChange : PowerHistory
	{
		public int Entity { get; set; }

		public int Tag { get; set; }

		public int Value { get; set; }

		public bool ChangeDef { get; set; }

		public HistTagChange()
			: base(PowerType.TAG_CHANGE)
		{
		}

		public override string ToString()
		{
			return $"type={base.Type} entity={Entity} tag={Tag} value={Value}";
		}
	}

	public class HistMetaData : PowerHistory
	{
		public HistoryMeta.Type MetaType { get; set; }

		public List<int> Info { get; set; }

		public int Data { get; set; }

		public List<int> AdditionalData { get; set; }

		public HistMetaData()
			: base(PowerType.META_DATA)
		{
			Info = new List<int>();
			AdditionalData = new List<int>();
		}

		public override string ToString()
		{
			return $"type={base.Type} metaType={MetaType} infoCount={Info.Count} data={Data}";
		}
	}

	public class HistSubSpellStart : PowerHistory
	{
		public string SpellPrefabGUID { get; set; }

		public int SourceEntityID { get; set; }

		public List<int> TargetEntityIDS { get; set; }

		public HistSubSpellStart()
			: base(PowerType.SUB_SPELL_START)
		{
		}

		public static HistSubSpellStart CreateFromProto(PowerHistorySubSpellStart proto)
		{
			return new HistSubSpellStart
			{
				SpellPrefabGUID = proto.SpellPrefabGuid,
				SourceEntityID = (proto.HasSourceEntityId ? proto.SourceEntityId : 0),
				TargetEntityIDS = proto.TargetEntityIds
			};
		}
	}

	public class HistSubSpellEnd : PowerHistory
	{
		public HistSubSpellEnd()
			: base(PowerType.SUB_SPELL_END)
		{
		}
	}

	public class HistVoSpell : PowerHistory
	{
		public string SpellPrefabGUID { get; set; }

		public int Speaker { get; set; }

		public bool Blocking { get; set; }

		public int AdditionalDelayMs { get; set; }

		public string BrassRingGUID { get; set; }

		public AudioSource m_audioSource { get; set; }

		public bool m_ableToLoad { get; set; }

		public HistVoSpell()
			: base(PowerType.VO_SPELL)
		{
		}

		public static HistVoSpell CreateFromProto(PowerHistoryVoTask proto)
		{
			return new HistVoSpell
			{
				SpellPrefabGUID = proto.SpellPrefabGuid,
				Speaker = proto.SpeakingEntity,
				Blocking = proto.Blocking,
				AdditionalDelayMs = proto.AdditionalDelayMs,
				BrassRingGUID = proto.BrassRingPrefabGuid
			};
		}
	}

	public class HistCachedTagForDormantChange : PowerHistory
	{
		public int Entity { get; set; }

		public int Tag { get; set; }

		public int Value { get; set; }

		public HistCachedTagForDormantChange()
			: base(PowerType.CACHED_TAG_FOR_DORMANT_CHANGE)
		{
		}

		public static HistCachedTagForDormantChange CreateFromProto(PowerHistoryCachedTagForDormantChange proto)
		{
			return new HistCachedTagForDormantChange
			{
				Entity = proto.Entity,
				Tag = proto.Tag,
				Value = proto.Value
			};
		}

		public override string ToString()
		{
			return $"type={base.Type} entity={Entity} tag={Tag} value={Value}";
		}
	}

	public class HistShuffleDeck : PowerHistory
	{
		public int PlayerID { get; set; }

		public HistShuffleDeck()
			: base(PowerType.SHUFFLE_DECK)
		{
		}

		public static HistShuffleDeck CreateFromProto(PowerHistoryShuffleDeck proto)
		{
			return new HistShuffleDeck
			{
				PlayerID = proto.PlayerId
			};
		}

		public override string ToString()
		{
			return $"type={base.Type} player_id={PlayerID}";
		}
	}

	public class CardUserData
	{
		public int DbId { get; set; }

		public int Count { get; set; }

		public TAG_PREMIUM Premium { get; set; }
	}

	public class DeckContents
	{
		public long Deck { get; set; }

		public List<CardUserData> Cards { get; set; }

		public DeckContents()
		{
			Cards = new List<CardUserData>();
		}

		public static DeckContents FromPacket(PegasusUtil.DeckContents packet)
		{
			DeckContents deckContents = new DeckContents();
			deckContents.Deck = packet.DeckId;
			for (int i = 0; i < packet.Cards.Count; i++)
			{
				DeckCardData deckCardData = packet.Cards[i];
				CardUserData cardUserData = new CardUserData();
				cardUserData.DbId = deckCardData.Def.Asset;
				cardUserData.Count = ((!deckCardData.HasQty) ? 1 : deckCardData.Qty);
				cardUserData.Premium = (deckCardData.Def.HasPremium ? ((TAG_PREMIUM)deckCardData.Def.Premium) : TAG_PREMIUM.NORMAL);
				deckContents.Cards.Add(cardUserData);
			}
			return deckContents;
		}
	}

	public class DeckName
	{
		public long Deck { get; set; }

		public string Name { get; set; }
	}

	public class DeckCard
	{
		public long Deck { get; set; }

		public long Card { get; set; }
	}

	public class GenericResponse
	{
		public enum Result
		{
			RESULT_OK = 0,
			RESULT_REQUEST_IN_PROCESS = 1,
			RESULT_REQUEST_COMPLETE = 2,
			RESULT_UNKNOWN_ERROR = 100,
			RESULT_INTERNAL_ERROR = 101,
			RESULT_DB_ERROR = 102,
			RESULT_INVALID_REQUEST = 103,
			RESULT_LOGIN_LOAD = 104,
			RESULT_DATA_MIGRATION_OR_PLAYER_ID_ERROR = 105,
			RESULT_INTERNAL_RPC_ERROR = 106,
			RESULT_DATA_MIGRATION_REQUIRED = 107
		}

		public int RequestId { get; set; }

		public int RequestSubId { get; set; }

		public Result ResultCode { get; set; }

		public object GenericData { get; set; }
	}

	public class DBAction
	{
		public enum ActionType
		{
			UNKNOWN,
			GET_DECK,
			CREATE_DECK,
			RENAME_DECK,
			DELETE_DECK,
			SET_DECK,
			OPEN_BOOSTER,
			GAMES_INFO
		}

		public enum ResultType
		{
			UNKNOWN,
			SUCCESS,
			NOT_OWNED,
			CONSTRAINT
		}

		public ActionType Action { get; set; }

		public ResultType Result { get; set; }

		public long MetaData { get; set; }
	}

	public class TurnTimerInfo
	{
		public float Seconds { get; set; }

		public int Turn { get; set; }

		public bool Show { get; set; }
	}

	public class CardQuote
	{
		public enum QuoteState
		{
			SUCCESS,
			UNKNOWN_ERROR
		}

		public int AssetID { get; set; }

		public int BuyPrice { get; set; }

		public int SaleValue { get; set; }

		public QuoteState Status { get; set; }
	}

	public class GameEnd
	{
		public List<NetCache.ProfileNotice> Notices { get; set; }

		public GameEnd()
		{
			Notices = new List<NetCache.ProfileNotice>();
		}
	}

	public class ProfileNotices
	{
		public List<NetCache.ProfileNotice> Notices { get; set; }

		public ProfileNotices()
		{
			Notices = new List<NetCache.ProfileNotice>();
		}
	}

	public class AccountLicenseAchieveResponse
	{
		public enum AchieveResult
		{
			INVALID_ACHIEVE = 1,
			NOT_ACTIVE,
			IN_PROGRESS,
			COMPLETE,
			STATUS_UNKNOWN
		}

		public int Achieve { get; set; }

		public AchieveResult Result { get; set; }
	}

	public class DebugConsoleResponse
	{
		public int Type { get; set; }

		public string Response { get; set; }

		public DebugConsoleResponse()
		{
			Response = "";
		}
	}

	public class RecruitInfo
	{
		public ulong ID { get; set; }

		public BnetAccountId RecruitID { get; set; }

		public string Nickname { get; set; }

		public int Status { get; set; }

		public int Level { get; set; }

		public ulong CreationTimeMicrosec { get; set; }

		public RecruitInfo()
		{
			ID = 0uL;
			RecruitID = new BnetAccountId();
			Nickname = "";
			Status = 0;
			Level = 0;
		}

		public override string ToString()
		{
			return $"[RecruitInfo: ID={ID}, RecruitID={RecruitID}, Nickname={Nickname}, Status={Status}, Level={Level}]";
		}
	}

	public const int NoSubOption = -1;

	public const int NoPosition = 0;

	public static string TutorialServer = "01";

	public const string CosmeticVersion = "20.4";

	public const string CosmeticRevision = "0";

	public const string VersionPostfix = "";

	public const string DEFAULT_INTERNAL_ENVIRONMENT = "bn12-01.battle.net";

	public const string DEFAULT_PUBLIC_ENVIRONMENT = "us.actual.battle.net";

	private static readonly float PROCESS_WARNING = 15f;

	private static readonly float PROCESS_WARNING_REPORT_GAP = 1f;

	private const int MIN_DEFERRED_WAIT = 30;

	public const int SEND_DECK_DATA_NO_HERO_ASSET_CHANGE = -1;

	public const int SEND_DECK_DATA_NO_CARD_BACK_CHANGE = -1;

	private const bool ReconnectAfterFailedPings = true;

	private const float ERROR_HANDLING_DELAY = 0.4f;

	public static readonly PlatformDependentValue<bool> LAUNCHES_WITH_BNET_APP = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		PC = true,
		Mac = true,
		iOS = false,
		Android = false
	};

	public static readonly PlatformDependentValue<bool> TUTORIALS_WITHOUT_ACCOUNT = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		PC = false,
		Mac = false,
		iOS = true,
		Android = true
	};

	private static readonly Map<constants.BnetRegion, string> RegionToTutorialName = new Map<constants.BnetRegion, string>
	{
		{
			constants.BnetRegion.REGION_US,
			"us-tutorial{0}.actual.battle.net"
		},
		{
			constants.BnetRegion.REGION_EU,
			"eu-tutorial{0}.actual.battle.net"
		},
		{
			constants.BnetRegion.REGION_KR,
			"kr-tutorial{0}.actual.battle.net"
		},
		{
			constants.BnetRegion.REGION_CN,
			"cn-tutorial{0}.actual.battlenet.com.cn"
		}
	};

	private static readonly SortedDictionary<int, int> m_deferredMessageResponseMap = new SortedDictionary<int, int>
	{
		{ 305, 306 },
		{ 303, 304 },
		{ 205, 307 },
		{ 314, 315 }
	};

	private static readonly SortedDictionary<int, int> m_deferredGetAccountInfoMessageResponseMap = new SortedDictionary<int, int>
	{
		{ 11, 233 },
		{ 18, 264 },
		{ 4, 232 },
		{ 2, 202 },
		{ 10, 231 },
		{ 15, 260 },
		{ 19, 271 },
		{ 8, 270 },
		{ 21, 283 },
		{ 7, 236 },
		{ 27, 318 },
		{ 28, 325 },
		{ 29, 608 }
	};

	private IDispatcher m_dispatcherImpl;

	private Map<int, List<NetHandler>> m_netHandlers = new Map<int, List<NetHandler>>();

	private QueueInfoHandler m_queueInfoHandler;

	private GameQueueHandler m_gameQueueHandler;

	private int m_numConnectionFailures;

	private ConnectAPI m_connectApi;

	private uint m_gameServerKeepAliveFrequencySeconds;

	private uint m_gameServerKeepAliveRetry;

	private uint m_gameServerKeepAliveWaitForInternetSeconds;

	private bool m_gameConceded;

	private bool m_disconnectRequested;

	private double m_timeInternetUnreachable;

	private AckCardSeen m_ackCardSeenPacket = new AckCardSeen();

	private readonly List<ConnectErrorParams> m_errorList = new List<ConnectErrorParams>();

	private List<ThrottledPacketListener> m_throttledPacketListeners = new List<ThrottledPacketListener>();

	private List<RequestContext> m_inTransitRequests = new List<RequestContext>();

	private static float m_maxDeferredWait = 120f;

	private static bool s_shouldBeConnectedToAurora = !TUTORIALS_WITHOUT_ACCOUNT;

	private static bool s_running;

	private static UnityUrlDownloader s_urlDownloader = new UnityUrlDownloader();

	private NetworkState m_state;

	private NetworkReachabilityManager m_networkReachabilityManager;

	public static string BranchName => string.Format("{0}.{1}{2}", "20.4", "0", "");

	private static List<BattleNetErrors> GameServerDisconnectEvents { get; set; }

	private long FakeIdWaitingForResponse { get; set; }

	public event Action<BattleNetErrors> OnConnectedToBattleNet;

	public event Action<BattleNetErrors> OnDisconnectedFromBattleNet;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		m_networkReachabilityManager = HearthstoneServices.Get<NetworkReachabilityManager>();
		m_state.SetDefaults();
		if (PlatformSettings.s_isDeviceSupported)
		{
			HearthstoneApplication.Get().WillReset += WillReset;
			HearthstoneApplication.Get().Resetting += OnReset;
			s_running = true;
			CreateNewDispatcher();
			InitBattleNet(m_dispatcherImpl);
			RegisterNetHandler(SubscribeResponse.PacketID.ID, OnSubscribeResponse);
			RegisterNetHandler(ClientStateNotification.PacketID.ID, OnClientStateNotification);
			RegisterNetHandler(PegasusUtil.GenericResponse.PacketID.ID, OnGenericResponse);
			RegisterNetHandler(PegasusUtil.GetDeckContentsResponse.PacketID.ID, OnDeckContentsResponse);
			if ((bool)TUTORIALS_WITHOUT_ACCOUNT)
			{
				SetShouldBeConnectedToAurora(global::Options.Get().GetBool(Option.CONNECT_TO_AURORA));
			}
		}
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[2]
		{
			typeof(GameDbf),
			typeof(NetworkReachabilityManager)
		};
	}

	public void Shutdown()
	{
		if (s_running)
		{
			NetCache.Get().DispatchClientOptionsToServer();
			PresenceMgr.Get().OnShutdown();
			if (IsLoggedIn())
			{
				CancelFindGame();
			}
			CloseAll();
			BattleNet.AppQuit();
			BnetNearbyPlayerMgr.Get().Shutdown();
			s_running = false;
		}
	}

	private void WillReset()
	{
		NetCache.Get().DispatchClientOptionsToServer();
		NetCache.Get().Clear();
		m_state.DelayedError = null;
		m_state.TimeBeforeAllowReset = 0f;
		if (m_connectApi != null)
		{
			RemoveConnectApiConnectionListeners();
		}
	}

	public void OnReset()
	{
		m_state = default(NetworkState);
		m_state.SetDefaults();
		if (m_connectApi != null)
		{
			RegisterConnectApiConnectionListeners();
		}
		s_running = true;
		ResetForNewAuroraConnection();
	}

	public bool ResetForNewAuroraConnection()
	{
		Log.Offline.PrintDebug("Resetting for new Aurora Connection");
		NetCache.Get().ClearForNewAuroraConnection();
		m_state.QueuedClientStateNotifications.Clear();
		CloseAll();
		m_dispatcherImpl.ResetForNewConnection();
		m_inTransitRequests.Clear();
		bool flag = false;
		if (ShouldBeConnectedToAurora())
		{
			string username = GetUsername();
			string targetServer = GetTargetServer();
			uint port = GetPort();
			SslParameters sSLParams = GetSSLParams();
			flag = BattleNet.Reset(CreateBattleNetImplementation(), HearthstoneApplication.IsInternal(), username, targetServer, port, sSLParams);
			Log.Offline.PrintDebug("ResetForNewAuroraConnection: ResetOk={0}", flag);
		}
		if (flag || !ShouldBeConnectedToAurora())
		{
			BnetParty.SetDisconnectedFromBattleNet();
			m_connectApi.SetDisconnectedFromBattleNet();
			InitializeConnectApi(m_dispatcherImpl);
		}
		return flag;
	}

	public static Network Get()
	{
		return HearthstoneServices.Get<Network>();
	}

	public static float GetMaxDeferredWait()
	{
		return m_maxDeferredWait;
	}

	public static string ProductVersion()
	{
		return 20 + "." + 4 + "." + 0 + "." + 0;
	}

	private void CreateNewDispatcher()
	{
		IDebugConnectionManager debugConnectionManager = new DebugConnectionManager();
		m_dispatcherImpl = new QueueDispatcher(debugConnectionManager, new ClientRequestManager(), new PacketDecoderManager(debugConnectionManager.AllowDebugConnections()), TelemetryManager.NetworkComponent);
	}

	private void ProcessRequestTimeouts()
	{
		float now = Time.realtimeSinceStartup;
		for (int i = 0; i < m_inTransitRequests.Count; i++)
		{
			RequestContext requestContext = m_inTransitRequests[i];
			if (requestContext.m_timeoutHandler != null && requestContext.m_waitUntil < now)
			{
				Debug.LogWarning($"Encountered timeout waiting for {requestContext.m_pendingResponseId} {requestContext.m_requestId} {requestContext.m_requestSubId}");
				requestContext.m_timeoutHandler(requestContext.m_pendingResponseId, requestContext.m_requestId, requestContext.m_requestSubId);
			}
		}
		m_inTransitRequests.RemoveAll((RequestContext rc) => rc.m_waitUntil < now);
	}

	public void AddPendingRequestTimeout(int requestId, int requestSubId)
	{
		if (!ShouldBeConnectedToAurora())
		{
			return;
		}
		int value = 0;
		if ((201 == requestId && m_deferredGetAccountInfoMessageResponseMap.TryGetValue(requestSubId, out value)) || m_deferredMessageResponseMap.TryGetValue(requestId, out value))
		{
			TimeoutHandler value2 = null;
			if (m_state.NetTimeoutHandlers.TryGetValue(value, out value2))
			{
				m_inTransitRequests.Add(new RequestContext(value, requestId, requestSubId, value2));
			}
			else
			{
				m_inTransitRequests.Add(new RequestContext(value, requestId, requestSubId, OnRequestTimeout));
			}
		}
	}

	private void RemovePendingRequestTimeout(int pendingResponseId)
	{
		m_inTransitRequests.RemoveAll((RequestContext pc) => pc.m_pendingResponseId == pendingResponseId);
	}

	private static void OnRequestTimeout(int pendingResponseId, int requestId, int requestSubId)
	{
		if (m_deferredMessageResponseMap.ContainsValue(pendingResponseId) || m_deferredGetAccountInfoMessageResponseMap.ContainsValue(pendingResponseId))
		{
			Debug.LogError($"OnRequestTimeout pending ID {pendingResponseId} {requestId} {requestSubId}");
			FatalErrorMgr.Get().SetErrorCode("HS", "NT" + pendingResponseId, requestId.ToString(), requestSubId.ToString());
			TelemetryManager.Client().SendNetworkError(NetworkError.ErrorType.TIMEOUT_DEFERRED_RESPONSE, FatalErrorMgr.Get().GetFormattedErrorCode(), 0);
			Get().ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_UNAVAILABLE_UNKNOWN");
		}
		else
		{
			Debug.LogError($"Unhandled OnRequestTimeout pending ID {pendingResponseId} {requestId} {requestSubId}");
			FatalErrorMgr.Get().SetErrorCode("HS", "NU" + pendingResponseId, requestId.ToString(), requestSubId.ToString());
			TelemetryManager.Client().SendNetworkError(NetworkError.ErrorType.TIMEOUT_NOT_DEFERRED_RESPONSE, FatalErrorMgr.Get().GetFormattedErrorCode(), 0);
			Get().ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_UNAVAILABLE_UNKNOWN");
		}
	}

	private void OnGenericResponse()
	{
		GenericResponse genericResponse = GetGenericResponse();
		if (genericResponse == null)
		{
			Debug.LogError($"Login - GenericResponse parse error");
			return;
		}
		bool num = 201 == genericResponse.RequestId && m_deferredGetAccountInfoMessageResponseMap.ContainsKey(genericResponse.RequestSubId);
		bool flag = m_deferredMessageResponseMap.ContainsKey(genericResponse.RequestId);
		if ((num || flag) && GenericResponse.Result.RESULT_REQUEST_IN_PROCESS != genericResponse.ResultCode && GenericResponse.Result.RESULT_DATA_MIGRATION_REQUIRED != genericResponse.ResultCode)
		{
			Debug.LogError($"Unhandled resultCode {genericResponse.ResultCode} for requestId {genericResponse.RequestId}:{genericResponse.RequestSubId}");
			FatalErrorMgr.Get().SetErrorCode("HS", "NG" + genericResponse.ResultCode, genericResponse.RequestId.ToString(), genericResponse.RequestSubId.ToString());
			TelemetryManager.Client().SendNetworkError(NetworkError.ErrorType.REQUEST_ERROR, FatalErrorMgr.Get().GetFormattedErrorCode(), 0);
			ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_UNAVAILABLE_UNKNOWN");
		}
	}

	public static bool IsRunning()
	{
		return s_running;
	}

	public double TimeSinceLastPong()
	{
		if (!IsConnectedToGameServer() || m_gameServerKeepAliveFrequencySeconds == 0 || m_connectApi.GetTimeLastPingSent() <= m_connectApi.GetTimeLastPingReceieved())
		{
			return 0.0;
		}
		return (double)Time.realtimeSinceStartup - m_connectApi.GetTimeLastPingReceieved();
	}

	private void OnSubscribeResponse()
	{
		SubscribeResponse subscribeResponse = m_connectApi.GetSubscribeResponse();
		if (subscribeResponse != null && subscribeResponse.HasRequestMaxWaitSecs && subscribeResponse.RequestMaxWaitSecs >= 30)
		{
			m_maxDeferredWait = subscribeResponse.RequestMaxWaitSecs;
		}
	}

	private void OnClientStateNotification()
	{
		ClientStateNotification clientStateNotification = m_connectApi.GetClientStateNotification();
		if (!NetCache.Get().HasReceivedInitialClientState)
		{
			m_state.QueuedClientStateNotifications.Add(clientStateNotification);
			TelemetryManager.Client().SendInitialClientStateOutOfOrder(clientStateNotification.HasAchievementNotifications ? clientStateNotification.AchievementNotifications.AchievementNotifications_.Count : 0, clientStateNotification.HasNoticeNotifications ? clientStateNotification.NoticeNotifications.NoticeNotifications_.Count : 0, clientStateNotification.HasCollectionModifications ? clientStateNotification.CollectionModifications.CardModifications.Sum((CardModification m) => m.Quantity) : 0, clientStateNotification.HasCurrencyState ? 1 : 0, clientStateNotification.HasBoosterModifications ? clientStateNotification.BoosterModifications.Modifications.Sum((BoosterInfo m) => m.Count) : 0, clientStateNotification.HasHeroXp ? clientStateNotification.HeroXp.XpInfos.Count : 0, clientStateNotification.HasPlayerRecords ? clientStateNotification.PlayerRecords.Records.Count : 0, clientStateNotification.HasArenaSessionResponse ? 1 : 0, clientStateNotification.HasCardBackModifications ? clientStateNotification.CardBackModifications.CardBackModifications_.Count : 0);
		}
		else
		{
			ProcessClientStateNotification(clientStateNotification);
		}
	}

	public static void ProcessClientStateNotification(ClientStateNotification packet)
	{
		if (packet.HasCurrencyState)
		{
			NetCache.Get().OnCurrencyState(packet.CurrencyState);
		}
		if (packet.HasCollectionModifications)
		{
			NetCache.Get().OnCollectionModification(packet);
		}
		else
		{
			if (packet.HasAchievementNotifications)
			{
				AchieveManager.Get().OnAchievementNotifications(packet.AchievementNotifications.AchievementNotifications_);
			}
			if (packet.HasNoticeNotifications)
			{
				Get().OnNoticeNotifications(packet.NoticeNotifications);
			}
			if (packet.HasBoosterModifications)
			{
				NetCache.Get().OnBoosterModifications(packet.BoosterModifications);
			}
		}
		if (packet.HasHeroXp)
		{
			NetCache.Get().OnHeroXP(packet.HeroXp);
		}
		if (packet.HasPlayerRecords)
		{
			NetCache.Get().OnPlayerRecordsPacket(packet.PlayerRecords);
		}
		if (packet.HasArenaSessionResponse)
		{
			DraftManager.Get().OnArenaSessionResponsePacket(packet.ArenaSessionResponse);
		}
		if (packet.HasCardBackModifications)
		{
			NetCache.Get().OnCardBackModifications(packet.CardBackModifications);
		}
		if (packet.HasPlayerDraftTickets)
		{
			NetCache.Get().OnPlayerDraftTickets(packet.PlayerDraftTickets);
		}
	}

	public void OnInitialClientStateProcessed()
	{
		List<ClientStateNotification> list = new List<ClientStateNotification>(m_state.QueuedClientStateNotifications);
		m_state.QueuedClientStateNotifications.Clear();
		foreach (ClientStateNotification item in list)
		{
			ProcessClientStateNotification(item);
		}
	}

	public void OnNoticeNotifications(NoticeNotifications packet)
	{
		List<ProfileNotice> list = new List<ProfileNotice>();
		List<NetCache.ProfileNotice> result = new List<NetCache.ProfileNotice>();
		for (int i = 0; i < packet.NoticeNotifications_.Count; i++)
		{
			NoticeNotification noticeNotification = packet.NoticeNotifications_[i];
			list.Add(noticeNotification.Notice);
		}
		HandleProfileNotices(list, ref result);
		NetCache.Get().HandleIncomingProfileNotices(result, isInitialNoticeList: false);
	}

	private void RegisterConnectApiConnectionListeners()
	{
		m_connectApi.RegisterGameServerConnectEventListener(OnGameServerConnectEvent);
		m_connectApi.RegisterGameServerDisconnectEventListener(OnGameServerDisconnectEvent);
	}

	private void RemoveConnectApiConnectionListeners()
	{
		m_connectApi.RemoveGameServerConnectEventListener(OnGameServerConnectEvent);
		m_connectApi.RemoveGameServerDisconnectEventListener(OnGameServerDisconnectEvent);
	}

	public void UpdateCachedBnetValues()
	{
		m_state.CachedGameAccountId = BattleNet.GetMyGameAccountId();
		m_state.CachedRegion = BattleNet.GetCurrentRegion();
	}

	public void OverrideKeepAliveSeconds(uint value)
	{
		if (HearthstoneApplication.IsInternal())
		{
			m_gameServerKeepAliveFrequencySeconds = value;
		}
	}

	public bgs.types.EntityId GetMyGameAccountId()
	{
		bgs.types.EntityId myGameAccountId = BattleNet.GetMyGameAccountId();
		if (myGameAccountId.hi == 0L && myGameAccountId.lo == 0L)
		{
			return m_state.CachedGameAccountId;
		}
		return myGameAccountId;
	}

	public constants.BnetRegion GetCurrentRegion()
	{
		constants.BnetRegion currentRegion = BattleNet.GetCurrentRegion();
		if (currentRegion == constants.BnetRegion.REGION_UNINITIALIZED)
		{
			return m_state.CachedRegion;
		}
		return currentRegion;
	}

	private void InitializeConnectApi(IDispatcher dispatcher)
	{
		m_errorList.Clear();
		if (m_connectApi == null)
		{
			GameServerDisconnectEvents = new List<BattleNetErrors>();
			m_connectApi = new ConnectAPI(dispatcher);
			RegisterConnectApiConnectionListeners();
		}
		m_connectApi.SetGameStartState(GameStartState.Invalid);
	}

	public static void ApplicationPaused()
	{
		if (NetCache.Get() != null)
		{
			NetCache.Get().DispatchClientOptionsToServer();
		}
		if (HearthstoneServices.TryGet<Network>(out var service) && service.m_connectApi != null)
		{
			service.m_connectApi.ProcessUtilPackets();
		}
		BattleNet.ApplicationWasPaused();
	}

	public void CloseAll()
	{
		if (m_ackCardSeenPacket.CardDefs.Count != 0)
		{
			SendAckCardsSeen();
		}
		if (m_connectApi != null)
		{
			m_connectApi.Close();
		}
	}

	public static void ApplicationUnpaused()
	{
		BattleNet.ApplicationWasUnpaused();
	}

	public void Update()
	{
		if (s_running)
		{
			ProcessRequestTimeouts();
			ProcessNetworkReachability();
			ProcessConnectApiHeartbeat();
			StoreManager.Get().Heartbeat();
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float num = realtimeSinceStartup - m_state.LastCall;
			if (!(num < PROCESS_WARNING) && !(realtimeSinceStartup - m_state.LastCallReport < PROCESS_WARNING_REPORT_GAP))
			{
				m_state.LastCallReport = realtimeSinceStartup;
				string devElapsedTimeString = TimeUtils.GetDevElapsedTimeString(num);
				Debug.LogWarning($"Network.ProcessNetwork not called for {devElapsedTimeString}");
			}
		}
	}

	private void ProcessConnectApiHeartbeat()
	{
		GetBattleNetPackets();
		int count = m_errorList.Count;
		for (int i = 0; i < count; i++)
		{
			ConnectErrorParams connectErrorParams = m_errorList[i];
			if (connectErrorParams == null)
			{
				Debug.LogError("null error! " + m_errorList.Count);
			}
			else if (Time.realtimeSinceStartup >= connectErrorParams.m_creationTime + 0.4f)
			{
				m_errorList.RemoveAt(i);
				i--;
				count = m_errorList.Count;
				Error.AddFatal(connectErrorParams);
			}
		}
		if (m_connectApi != null)
		{
			if (m_connectApi.HasGameServerConnection())
			{
				m_connectApi.UpdateGameServerConnection();
				UpdatePingPong();
			}
			m_connectApi.ProcessUtilPackets();
			if (m_connectApi.TryConnectDebugConsole())
			{
				m_connectApi.UpdateDebugConsole();
			}
		}
	}

	private void ProcessNetworkReachability()
	{
		if (!IsLoggedIn())
		{
			return;
		}
		if (!m_networkReachabilityManager.InternetAvailable_Cached)
		{
			if (IsInGame())
			{
				double totalSeconds = TimeUtils.GetElapsedTimeSinceEpoch().TotalSeconds;
				if (m_timeInternetUnreachable == 0.0)
				{
					m_timeInternetUnreachable = totalSeconds;
					return;
				}
				if (totalSeconds - m_timeInternetUnreachable < (double)m_gameServerKeepAliveWaitForInternetSeconds)
				{
					return;
				}
			}
			Log.Offline.PrintError("Network.ProcessInternetReachability(): Access to the Internet has been lost.");
			Error.AddFatal(FatalErrorReason.NO_INTERNET_ACCESS, "GLOBAL_ERROR_NETWORK_DISCONNECT");
			return;
		}
		if (m_timeInternetUnreachable != 0.0)
		{
			double totalSeconds2 = TimeUtils.GetElapsedTimeSinceEpoch().TotalSeconds;
			TelemetryManager.Client().SendNetworkUnreachableRecovered((int)(totalSeconds2 - m_timeInternetUnreachable));
			if (IsInGame())
			{
				DisconnectFromGameServer();
			}
		}
		m_timeInternetUnreachable = 0.0;
	}

	public void AddErrorToList(ConnectErrorParams errorParams)
	{
		m_errorList.Add(errorParams);
	}

	public void SetShouldIgnorePong(bool value)
	{
		m_connectApi.SetShouldIgnorePong(value);
	}

	public void SetSpoofDisconnected(bool value)
	{
		m_connectApi.SetSpoofDisconnected(value);
	}

	private bool IsInGame()
	{
		return GameState.Get() != null;
	}

	private void UpdatePingPong()
	{
		if (m_gameServerKeepAliveFrequencySeconds == 0)
		{
			return;
		}
		double totalSeconds = TimeUtils.GetElapsedTimeSinceEpoch().TotalSeconds;
		if (m_connectApi.IsConnectedToGameServer() && totalSeconds - m_connectApi.GetTimeLastPingSent() > (double)m_gameServerKeepAliveFrequencySeconds)
		{
			int pingsSinceLastPong = m_connectApi.GetPingsSinceLastPong();
			if (m_connectApi.GetTimeLastPingSent() <= m_connectApi.GetTimeLastPingReceieved())
			{
				m_connectApi.SetTimeLastPingReceived(totalSeconds - 0.001);
			}
			m_connectApi.SetTimeLastPingSent(totalSeconds);
			m_connectApi.SendPing();
			if (pingsSinceLastPong >= m_gameServerKeepAliveRetry)
			{
				DisconnectFromGameServer();
				SetShouldIgnorePong(value: false);
			}
			m_connectApi.SetPingsSinceLastPong(pingsSinceLastPong + 1);
		}
	}

	private void GetBattleNetPackets()
	{
		GamesAPI.UtilResponse utilResponse;
		while ((utilResponse = BattleNet.NextUtilPacket()) != null)
		{
			bnet.protocol.Attribute attribute = utilResponse.m_response.AttributeList[0];
			bnet.protocol.Attribute attribute2 = utilResponse.m_response.AttributeList[1];
			int type = (int)attribute.Value.IntValue;
			byte[] blobValue = attribute2.Value.BlobValue;
			PegasusPacket pegasusPacket = new PegasusPacket(type, blobValue.Length, blobValue);
			pegasusPacket.Context = utilResponse.m_context;
			m_connectApi.DecodeAndProcessPacket(pegasusPacket);
		}
	}

	public void AppAbort()
	{
		if (s_running)
		{
			NetCache.Get().DispatchClientOptionsToServer();
			PresenceMgr.Get().OnShutdown();
			CancelFindGame();
			CloseAll();
			BattleNet.AppQuit();
			BnetNearbyPlayerMgr.Get().Shutdown();
			s_running = false;
		}
	}

	public void ResetConnectionFailureCount()
	{
		m_numConnectionFailures = 0;
	}

	public bool RegisterNetHandler(object enumId, NetHandler handler, TimeoutHandler timeoutHandler = null)
	{
		int key = (int)enumId;
		if (timeoutHandler != null)
		{
			if (m_state.NetTimeoutHandlers.ContainsKey(key))
			{
				return false;
			}
			m_state.NetTimeoutHandlers.Add(key, timeoutHandler);
		}
		if (m_netHandlers.TryGetValue(key, out var value))
		{
			if (value.Contains(handler))
			{
				return false;
			}
		}
		else
		{
			value = new List<NetHandler>();
			m_netHandlers.Add(key, value);
		}
		value.Add(handler);
		return true;
	}

	public bool RemoveNetHandler(object enumId, NetHandler handler)
	{
		int key = (int)enumId;
		if (m_netHandlers.TryGetValue(key, out var value) && value.Remove(handler))
		{
			return true;
		}
		return false;
	}

	public void RegisterThrottledPacketListener(ThrottledPacketListener listener)
	{
		if (!m_throttledPacketListeners.Contains(listener))
		{
			m_throttledPacketListeners.Add(listener);
		}
	}

	public void RemoveThrottledPacketListener(ThrottledPacketListener listener)
	{
		m_throttledPacketListeners.Remove(listener);
	}

	public void RegisterGameQueueHandler(GameQueueHandler handler)
	{
		if (m_gameQueueHandler != null)
		{
			Log.Net.Print("handler {0} would bash game queue handler {1}", handler, m_gameQueueHandler);
		}
		else
		{
			m_gameQueueHandler = handler;
		}
	}

	public void RemoveGameQueueHandler(GameQueueHandler handler)
	{
		if (m_gameQueueHandler != handler)
		{
			Log.Net.Print("Removing game queue handler that is not active {0}", handler);
		}
		else
		{
			m_gameQueueHandler = null;
		}
	}

	public void RegisterQueueInfoHandler(QueueInfoHandler handler)
	{
		if (m_queueInfoHandler != null)
		{
			Log.Net.Print("handler {0} would bash queue info handler {1}", handler, m_queueInfoHandler);
		}
		else
		{
			m_queueInfoHandler = handler;
		}
	}

	public void RemoveQueueInfoHandler(QueueInfoHandler handler)
	{
		if (m_queueInfoHandler != handler)
		{
			Log.Net.Print("Removing queue info handler that is not active {0}", handler);
		}
		else
		{
			m_queueInfoHandler = null;
		}
	}

	public bool FakeHandleType(Enum enumId)
	{
		int id = Convert.ToInt32(enumId);
		return FakeHandleType(id);
	}

	public bool FakeHandleType(int id)
	{
		if (!ShouldBeConnectedToAurora())
		{
			HandleType(id);
			return true;
		}
		return false;
	}

	private bool HandleType(int id)
	{
		RemovePendingRequestTimeout(id);
		if (!m_netHandlers.TryGetValue(id, out var value) || value.Count == 0)
		{
			if (!CanIgnoreUnhandledPacket(id))
			{
				Debug.LogError($"Network.HandleType() - Received packet {id}, but there are no handlers for it.");
			}
			return false;
		}
		NetHandler[] array = value.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
		return true;
	}

	private bool CanIgnoreUnhandledPacket(int id)
	{
		if (id == 15 || id == 116 || id == 254)
		{
			return true;
		}
		return false;
	}

	private bool ProcessGameQueue()
	{
		QueueEvent queueEvent = BattleNet.GetQueueEvent();
		if (queueEvent == null)
		{
			return false;
		}
		switch (queueEvent.EventType)
		{
		case QueueEvent.Type.QUEUE_LEAVE:
		case QueueEvent.Type.QUEUE_DELAY_ERROR:
		case QueueEvent.Type.QUEUE_AMM_ERROR:
		case QueueEvent.Type.QUEUE_CANCEL:
		case QueueEvent.Type.QUEUE_GAME_STARTED:
		case QueueEvent.Type.ABORT_CLIENT_DROPPED:
			m_state.FindingBnetGameType = BnetGameType.BGT_UNKNOWN;
			break;
		}
		if (m_gameQueueHandler == null)
		{
			Debug.LogWarningFormat("m_gameQueueHandler is null in Network.ProcessGameQueue! event={0} server={1}:{2} gameHandle={3} clientHandle={4}", queueEvent.EventType, (queueEvent.GameServer == null) ? "null" : queueEvent.GameServer.Address, (queueEvent.GameServer != null) ? queueEvent.GameServer.Port : 0u, (queueEvent.GameServer != null) ? queueEvent.GameServer.GameHandle : 0u, (queueEvent.GameServer == null) ? 0 : queueEvent.GameServer.ClientHandle);
		}
		else
		{
			m_gameQueueHandler(queueEvent);
		}
		return true;
	}

	private bool ProcessGameServer()
	{
		int id = NextGamePacketType();
		bool result = HandleType(id);
		m_connectApi.DropGamePacket();
		return result;
	}

	private bool ProcessUtilServer()
	{
		int id = m_connectApi.NextUtilPacketType();
		bool result = HandleType(id);
		m_connectApi.DropUtilPacket();
		return result;
	}

	private bool ProcessConsole()
	{
		int id = m_connectApi.NextDebugPacketType();
		bool result = HandleType(id);
		m_connectApi.DropDebugPacket();
		return result;
	}

	public UnavailableReason GetHearthstoneUnavailable(bool gamePacket)
	{
		UnavailableReason unavailableReason = new UnavailableReason();
		if (gamePacket)
		{
			Deadend deadendGame = m_connectApi.GetDeadendGame();
			unavailableReason.mainReason = deadendGame.Reply1;
			unavailableReason.subReason = deadendGame.Reply2;
			unavailableReason.extraData = deadendGame.Reply3;
		}
		else
		{
			DeadendUtil deadendUtil = m_connectApi.GetDeadendUtil();
			unavailableReason.mainReason = deadendUtil.Reply1;
			unavailableReason.subReason = deadendUtil.Reply2;
			unavailableReason.extraData = deadendUtil.Reply3;
		}
		return unavailableReason;
	}

	public void BuyCard(int assetId, TAG_PREMIUM premium, int count, int unitBuyPrice, int currentCollectionCount)
	{
		PegasusShared.CardDef cardDef = new PegasusShared.CardDef
		{
			Asset = assetId
		};
		if (premium != 0)
		{
			cardDef.Premium = (int)premium;
		}
		m_connectApi.BuyCard(cardDef, count, unitBuyPrice, currentCollectionCount);
	}

	public void SellCard(int assetId, TAG_PREMIUM premium, int count, int unitSellPrice, int currentCollectionCount)
	{
		PegasusShared.CardDef cardDef = new PegasusShared.CardDef
		{
			Asset = assetId
		};
		if (premium != 0)
		{
			cardDef.Premium = (int)premium;
		}
		m_connectApi.SellCard(cardDef, count, unitSellPrice, currentCollectionCount);
	}

	public void GetAllClientOptions()
	{
		m_connectApi.GetAllClientOptions();
	}

	public void SetClientOptions(SetOptions packet)
	{
		m_connectApi.SetClientOptions(packet);
	}

	public static BnetLoginState BattleNetStatus()
	{
		return (BnetLoginState)BattleNet.BattleNetStatus();
	}

	public static bool IsLoggedIn()
	{
		if (!BattleNet.IsInitialized())
		{
			return false;
		}
		return BattleNet.BattleNetStatus() == 4;
	}

	public bool HaveUnhandledPackets()
	{
		if (m_connectApi.HasUtilPackets())
		{
			return true;
		}
		if (m_connectApi.HasGamePackets())
		{
			return true;
		}
		if (m_connectApi.HasDebugPackets())
		{
			return true;
		}
		if (BattleNet.GetNotificationCount() > 0)
		{
			return true;
		}
		return false;
	}

	public int NextGamePacketType()
	{
		return m_connectApi.NextGamePacketType();
	}

	public PegasusPacket NextGamePacket()
	{
		return m_connectApi.NextGamePacket();
	}

	public void PushReceivedGamePacket(PegasusPacket packet)
	{
		m_connectApi.PushReceivedGamePacket(packet);
	}

	public void ProcessNetwork()
	{
		if (!s_running || m_state.LastCallFrame == Time.frameCount)
		{
			return;
		}
		m_state.LastCallFrame = Time.frameCount;
		m_state.LastCall = Time.realtimeSinceStartup;
		if (!InitBattleNet(m_dispatcherImpl) && ShouldBeConnectedToAurora())
		{
			return;
		}
		s_urlDownloader.Process();
		if (ShouldBeConnectedToAurora())
		{
			ProcessAurora();
		}
		else
		{
			ProcessDelayedError();
		}
		if (ProcessGameQueue())
		{
			return;
		}
		if (m_connectApi.HasGamePackets())
		{
			ProcessGameServer();
			return;
		}
		if (GameServerDisconnectEvents != null && GameServerDisconnectEvents.Count > 0)
		{
			BattleNetErrors[] array = GameServerDisconnectEvents.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				m_state.GameServerDisconnectEventListener?.Invoke(array[i]);
			}
			GameServerDisconnectEvents.Clear();
		}
		if (m_connectApi.HasUtilPackets())
		{
			ProcessUtilServer();
		}
		else if (m_connectApi.HasDebugPackets())
		{
			ProcessConsole();
		}
		else
		{
			ProcessQueuePosition();
		}
	}

	private bool InitBattleNet(IDispatcher dispatcher)
	{
		bool flag = BattleNet.IsInitialized();
		if (!flag)
		{
			if (BattleNet.Get() == null)
			{
				BattleNet.SetImpl(CreateBattleNetImplementation());
			}
			if (ShouldBeConnectedToAurora())
			{
				string username = GetUsername();
				string targetServer = GetTargetServer();
				uint port = GetPort();
				SslParameters sSLParams = GetSSLParams();
				flag = BattleNet.Init(BattleNet.Get(), HearthstoneApplication.IsInternal(), username, targetServer, port, sSLParams);
			}
			if (flag || !ShouldBeConnectedToAurora())
			{
				AddBnetErrorListener(BnetFeature.Auth, OnBnetAuthError);
				InitializeConnectApi(dispatcher);
			}
		}
		return flag;
	}

	private static IBattleNet CreateBattleNetImplementation()
	{
		ClientInterface clientInterface = new HSClientInterface();
		ICompressionProvider compressionProvider = new SharpZipCompressionProvider();
		IFileUtil fileUtil = new FileUtil(compressionProvider);
		IJsonSerializer jsonSerializer = new UnityJsonSerializer();
		LoggerInterface loggerInterface = new BattleNetLogger();
		IRpcConnectionFactory rpcConnectionFactory = new RpcConnectionFactory();
		BattleNetCSharp battleNetCSharp = new BattleNetCSharp(clientInterface, rpcConnectionFactory, compressionProvider, fileUtil, jsonSerializer, loggerInterface, TelemetryManager.NetworkComponent);
		battleNetCSharp.OnConnected += OnConnectedToBattleNetCallback;
		battleNetCSharp.OnDisconnected += OnDisconnectedFromBattleNetCallback;
		Debug.LogFormat("*** BattleNet version: Product = {0}, Data = {1}", clientInterface.GetVersion(), clientInterface.GetDataVersion());
		return battleNetCSharp;
	}

	private static void OnConnectedToBattleNetCallback(BattleNetErrors error)
	{
		if (Get().OnConnectedToBattleNet != null)
		{
			Get().OnConnectedToBattleNet(error);
		}
		TelemetryManager.OnBattleNetConnect(BattleNet.GetEnvironment(), (int)BattleNet.GetPort(), error);
	}

	private static void OnDisconnectedFromBattleNetCallback(BattleNetErrors error)
	{
		if (Get().OnDisconnectedFromBattleNet != null)
		{
			Get().OnDisconnectedFromBattleNet(error);
		}
		TelemetryManager.OnBattleNetDisconnect(BattleNet.GetEnvironment(), (int)BattleNet.GetPort(), error);
	}

	public static bool ShouldBeConnectedToAurora()
	{
		return s_shouldBeConnectedToAurora;
	}

	public static void SetShouldBeConnectedToAurora(bool shouldBeConnected)
	{
		s_shouldBeConnectedToAurora = shouldBeConnected;
	}

	public bool ShouldBeConnectedToAurora_NONSTATIC()
	{
		return s_shouldBeConnectedToAurora;
	}

	public void SetShouldBeConnectedToAurora_NONSTATIC(bool shouldBeConnected)
	{
		s_shouldBeConnectedToAurora = shouldBeConnected;
	}

	public void ProcessQueuePosition()
	{
		bgs.types.QueueInfo queueInfo = default(bgs.types.QueueInfo);
		BattleNet.GetQueueInfo(ref queueInfo);
		if (queueInfo.changed && m_queueInfoHandler != null)
		{
			QueueInfo queueInfo2 = new QueueInfo();
			queueInfo2.position = queueInfo.position;
			queueInfo2.secondsTilEnd = queueInfo.end;
			queueInfo2.stdev = queueInfo.stdev;
			m_queueInfoHandler(queueInfo2);
		}
	}

	public BnetEventHandler GetBnetEventHandler()
	{
		return m_state.CurrentBnetEventHandler;
	}

	public void SetBnetStateHandler(BnetEventHandler handler)
	{
		m_state.CurrentBnetEventHandler = handler;
	}

	public FriendsHandler GetFriendsHandler()
	{
		return m_state.CurrentFriendsHandler;
	}

	public void SetFriendsHandler(FriendsHandler handler)
	{
		m_state.CurrentFriendsHandler = handler;
	}

	public WhisperHandler GetWhisperHandler()
	{
		return m_state.CurrentWhisperHandler;
	}

	public void SetWhisperHandler(WhisperHandler handler)
	{
		m_state.CurrentWhisperHandler = handler;
	}

	public PresenceHandler GetPresenceHandler()
	{
		return m_state.CurrentPresenceHandler;
	}

	public void SetPresenceHandler(PresenceHandler handler)
	{
		m_state.CurrentPresenceHandler = handler;
	}

	public ShutdownHandler GetShutdownHandler()
	{
		return m_state.CurrentShutdownHandler;
	}

	public void SetShutdownHandler(ShutdownHandler handler)
	{
		m_state.CurrentShutdownHandler = handler;
	}

	public ChallengeHandler GetChallengeHandler()
	{
		return m_state.CurrentChallengeHandler;
	}

	public void SetChallengeHandler(ChallengeHandler handler)
	{
		m_state.CurrentChallengeHandler = handler;
	}

	public void SetGameServerDisconnectEventListener(GameServerDisconnectEvent handler)
	{
		m_state.GameServerDisconnectEventListener = handler;
	}

	public void RemoveGameServerDisconnectEventListener(GameServerDisconnectEvent handler)
	{
		if (m_state.GameServerDisconnectEventListener == handler)
		{
			m_state.GameServerDisconnectEventListener = null;
		}
	}

	public void AddBnetErrorListener(BnetFeature feature, BnetErrorCallback callback)
	{
		AddBnetErrorListener(feature, callback, null);
	}

	public void AddBnetErrorListener(BnetFeature feature, BnetErrorCallback callback, object userData)
	{
		BnetErrorListener bnetErrorListener = new BnetErrorListener();
		bnetErrorListener.SetCallback(callback);
		bnetErrorListener.SetUserData(userData);
		if (!m_state.FeatureBnetErrorListeners.TryGetValue(feature, out var value))
		{
			value = new List<BnetErrorListener>();
			m_state.FeatureBnetErrorListeners.Add(feature, value);
		}
		else if (value.Contains(bnetErrorListener))
		{
			return;
		}
		value.Add(bnetErrorListener);
	}

	public void AddBnetErrorListener(BnetErrorCallback callback)
	{
		AddBnetErrorListener(callback, null);
	}

	public void AddBnetErrorListener(BnetErrorCallback callback, object userData)
	{
		BnetErrorListener bnetErrorListener = new BnetErrorListener();
		bnetErrorListener.SetCallback(callback);
		bnetErrorListener.SetUserData(userData);
		if (!m_state.GlobalBnetErrorListeners.Contains(bnetErrorListener))
		{
			m_state.GlobalBnetErrorListeners.Add(bnetErrorListener);
		}
	}

	public bool RemoveBnetErrorListener(BnetFeature feature, BnetErrorCallback callback)
	{
		return RemoveBnetErrorListener(feature, callback, null);
	}

	public bool RemoveBnetErrorListener(BnetFeature feature, BnetErrorCallback callback, object userData)
	{
		if (!m_state.FeatureBnetErrorListeners.TryGetValue(feature, out var value))
		{
			return false;
		}
		BnetErrorListener bnetErrorListener = new BnetErrorListener();
		bnetErrorListener.SetCallback(callback);
		bnetErrorListener.SetUserData(userData);
		return value.Remove(bnetErrorListener);
	}

	public bool RemoveBnetErrorListener(BnetErrorCallback callback)
	{
		return RemoveBnetErrorListener(callback, null);
	}

	public bool RemoveBnetErrorListener(BnetErrorCallback callback, object userData)
	{
		BnetErrorListener bnetErrorListener = new BnetErrorListener();
		bnetErrorListener.SetCallback(callback);
		bnetErrorListener.SetUserData(userData);
		return m_state.GlobalBnetErrorListeners.Remove(bnetErrorListener);
	}

	public void SendUnsubcribeRequest(Unsubscribe packet, UtilSystemId systemChannel)
	{
		m_connectApi.SendUnsubscribeRequest(packet, systemChannel);
	}

	public void ProcessAurora()
	{
		BattleNet.ProcessAurora();
		ProcessBnetEvents();
		if (IsLoggedIn())
		{
			ProcessPresence();
			ProcessFriends();
			ProcessWhispers();
			ProcessParties();
			ProcessBroadcasts();
			ProcessNotifications();
			BnetNearbyPlayerMgr.Get().Update();
		}
		ProcessErrors();
	}

	private void ProcessBnetEvents()
	{
		int bnetEventsSize = BattleNet.GetBnetEventsSize();
		if (bnetEventsSize > 0 && m_state.CurrentBnetEventHandler != null)
		{
			BattleNet.BnetEvent[] array = new BattleNet.BnetEvent[bnetEventsSize];
			BattleNet.GetBnetEvents(array);
			m_state.CurrentBnetEventHandler(array);
			BattleNet.ClearBnetEvents();
		}
	}

	private void ProcessWhispers()
	{
		WhisperInfo info = default(WhisperInfo);
		BattleNet.GetWhisperInfo(ref info);
		if (info.whisperSize > 0 && m_state.CurrentWhisperHandler != null)
		{
			BnetWhisper[] whispers = new BnetWhisper[info.whisperSize];
			BattleNet.GetWhispers(whispers);
			m_state.CurrentWhisperHandler(whispers);
			BattleNet.ClearWhispers();
		}
	}

	private void ProcessParties()
	{
		BnetParty.Process();
	}

	private void ProcessBroadcasts()
	{
		int shutdownMinutes = BattleNet.GetShutdownMinutes();
		if (shutdownMinutes > 0 && m_state.CurrentShutdownHandler != null)
		{
			m_state.CurrentShutdownHandler(shutdownMinutes);
		}
	}

	private void ProcessNotifications()
	{
		int notificationCount = BattleNet.GetNotificationCount();
		if (notificationCount <= 0)
		{
			return;
		}
		BnetNotification[] array = new BnetNotification[notificationCount];
		BattleNet.GetNotifications(array);
		BattleNet.ClearNotifications();
		for (int i = 0; i < array.Length; i++)
		{
			BnetNotification bnetNotification = array[i];
			string notificationType = bnetNotification.NotificationType;
			if (notificationType == "WTCG.UtilNotificationMessage")
			{
				PegasusPacket packet = new PegasusPacket(bnetNotification.MessageType, 0, bnetNotification.MessageSize, bnetNotification.BlobMessage);
				m_connectApi.DecodeAndProcessPacket(packet);
			}
		}
	}

	private void ProcessFriends()
	{
		FriendsInfo info = default(FriendsInfo);
		BattleNet.GetFriendsInfo(ref info);
		if (info.updateSize != 0 && m_state.CurrentFriendsHandler != null)
		{
			FriendsUpdate[] updates = new FriendsUpdate[info.updateSize];
			BattleNet.GetFriendsUpdates(updates);
			m_state.CurrentFriendsHandler(updates);
			BattleNet.ClearFriendsUpdates();
		}
	}

	private void ProcessPresence()
	{
		int num = BattleNet.PresenceSize();
		if (num != 0 && m_state.CurrentPresenceHandler != null)
		{
			PresenceUpdate[] updates = new PresenceUpdate[num];
			BattleNet.GetPresence(updates);
			m_state.CurrentPresenceHandler(updates);
			BattleNet.ClearPresence();
		}
	}

	private void ProcessErrors()
	{
		ProcessDelayedError();
		BnetErrorInfo[] array = null;
		if (m_connectApi.HasErrors())
		{
			BnetErrorInfo bnetErrorInfo = new BnetErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnClientRequest, BattleNetErrors.ERROR_GAME_UTILITY_SERVER_NO_SERVER);
			array = new BnetErrorInfo[1] { bnetErrorInfo };
		}
		else
		{
			int errorsCount = BattleNet.GetErrorsCount();
			if (errorsCount == 0)
			{
				return;
			}
			array = new BnetErrorInfo[errorsCount];
			BattleNet.GetErrors(array);
		}
		foreach (BnetErrorInfo bnetErrorInfo2 in array)
		{
			BattleNetErrors error = bnetErrorInfo2.GetError();
			if (error == (BattleNetErrors)1003013u)
			{
				BattleNet.ClearErrors();
				HearthstoneApplication.Get().Reset();
				return;
			}
			string text = (HearthstoneApplication.IsPublic() ? "" : error.ToString());
			if (!m_connectApi.HasErrors() && m_connectApi.ShouldIgnoreError(bnetErrorInfo2))
			{
				if (!HearthstoneApplication.IsPublic())
				{
					Log.BattleNet.PrintDebug("BattleNet/ConnectDLL generated error={0} {1} (can ignore)", (int)error, text);
				}
			}
			else if (!FireErrorListeners(bnetErrorInfo2) && (m_connectApi.HasErrors() || !OnIgnorableBnetError(bnetErrorInfo2)))
			{
				OnFatalBnetError(bnetErrorInfo2);
			}
		}
		BattleNet.ClearErrors();
	}

	private bool FireErrorListeners(BnetErrorInfo info)
	{
		bool flag = false;
		if (m_state.FeatureBnetErrorListeners.TryGetValue(info.GetFeature(), out var value) && value.Count > 0)
		{
			BnetErrorListener[] array = value.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				flag = array[i].Fire(info) || flag;
			}
		}
		BnetErrorListener[] array2 = m_state.GlobalBnetErrorListeners.ToArray();
		for (int j = 0; j < array2.Length; j++)
		{
			flag = array2[j].Fire(info) || flag;
		}
		return flag;
	}

	public void ShowConnectionFailureError(string error)
	{
		ShowBreakingNewsOrError(error, DelayForConnectionFailures(m_numConnectionFailures++));
	}

	public void ShowBreakingNewsOrError(string error, float timeBeforeAllowReset = 0f)
	{
		m_state.DelayedError = error;
		m_state.TimeBeforeAllowReset = timeBeforeAllowReset;
		Debug.LogError($"Setting delayed error for Error Message: {error} and prevent reset for {timeBeforeAllowReset} seconds");
		ProcessDelayedError();
	}

	private bool ProcessDelayedError()
	{
		if (m_state.DelayedError == null)
		{
			return false;
		}
		bool result = false;
		if (BreakingNews.Get().GetStatus() != 0)
		{
			ErrorParams errorParams = new ErrorParams();
			errorParams.m_delayBeforeNextReset = m_state.TimeBeforeAllowReset;
			string text = BreakingNews.Get().GetText();
			if (string.IsNullOrEmpty(text))
			{
				if (BreakingNews.Get().GetError() != null && m_state.DelayedError == "GLOBAL_ERROR_NETWORK_NO_GAME_SERVER")
				{
					errorParams.m_message = GameStrings.Format("GLOBAL_ERROR_NETWORK_NO_CONNECTION");
				}
				else if (HearthstoneApplication.IsInternal() && m_state.DelayedError == "GLOBAL_ERROR_UNKNOWN_ERROR")
				{
					errorParams.m_message = "Dev Message: Could not connect to Battle.net, and there was no breaking news to display. Maybe Battle.net is down?";
				}
				else
				{
					errorParams.m_message = GameStrings.Format(m_state.DelayedError);
				}
			}
			else
			{
				errorParams.m_message = GameStrings.Format("GLOBAL_MOBILE_ERROR_BREAKING_NEWS", text);
				errorParams.m_reason = FatalErrorReason.BREAKING_NEWS;
			}
			Error.AddFatal(errorParams);
			m_state.DelayedError = null;
			m_state.TimeBeforeAllowReset = 0f;
			result = true;
		}
		return result;
	}

	public bool OnIgnorableBnetError(BnetErrorInfo info)
	{
		BattleNetErrors error = info.GetError();
		bool flag = false;
		switch (error)
		{
		case BattleNetErrors.ERROR_OK:
			flag = true;
			break;
		case BattleNetErrors.ERROR_GAME_UTILITY_SERVER_NO_SERVER:
			m_state.LogSource.LogError("Network.IgnoreBnetError() - error={0}", info);
			flag = true;
			break;
		case BattleNetErrors.ERROR_WAITING_FOR_DEPENDENCY:
			flag = true;
			break;
		case BattleNetErrors.ERROR_TARGET_OFFLINE:
			flag = true;
			break;
		case BattleNetErrors.ERROR_INCOMPLETE_PROFANITY_FILTERS:
		{
			Locale locale = Localization.GetLocale();
			if (locale == Locale.zhCN)
			{
				m_state.LogSource.LogError("Network.IgnoreBnetError() - error={0} locale={1}", info, locale);
			}
			flag = true;
			break;
		}
		case BattleNetErrors.ERROR_PRESENCE_TEMPORARY_OUTAGE:
			flag = true;
			break;
		case BattleNetErrors.ERROR_INVALID_TARGET_ID:
			flag = info.GetFeature() == BnetFeature.Friends && info.GetFeatureEvent() == BnetFeatureEvent.Friends_OnSendInvitation;
			break;
		case BattleNetErrors.ERROR_API_NOT_READY:
			flag = info.GetFeature() == BnetFeature.Presence;
			break;
		case BattleNetErrors.ERROR_FRIENDS_FRIENDSHIP_ALREADY_EXISTS:
		case BattleNetErrors.ERROR_FRIENDS_INVITATION_ALREADY_EXISTS:
		case BattleNetErrors.ERROR_FRIENDS_INVITEE_AT_MAX_FRIENDS:
		case BattleNetErrors.ERROR_FRIENDS_INVITER_AT_MAX_FRIENDS:
		case BattleNetErrors.ERROR_FRIENDS_INVITER_IS_BLOCKED_BY_INVITEE:
			flag = true;
			break;
		}
		if (error != BattleNetErrors.ERROR_OK && flag)
		{
			TelemetryManager.Client().SendIgnorableBattleNetError((int)error, error.ToString());
		}
		return flag;
	}

	public void OnFatalBnetError(BnetErrorInfo info)
	{
		BattleNetErrors error = info.GetError();
		m_state.LogSource.LogError("Network.OnFatalBnetError() - error={0}", info);
		TelemetryManager.Client().SendFatalBattleNetError((int)error, error.ToString());
		switch (error)
		{
		case BattleNetErrors.ERROR_DENIED:
			ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_LOGIN_FAILURE");
			break;
		case BattleNetErrors.ERROR_RPC_QUOTA_EXCEEDED:
		{
			string messageKey = "GLOBAL_ERROR_NETWORK_SPAM";
			Error.AddFatal(FatalErrorReason.BNET_NETWORK_SPAM, messageKey);
			break;
		}
		case BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED:
		{
			string messageKey = "GLOBAL_ERROR_NETWORK_DISCONNECT";
			ShowConnectionFailureError(messageKey);
			break;
		}
		case BattleNetErrors.ERROR_RPC_CONNECTION_TIMED_OUT:
			ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_CONNECTION_TIMEOUT");
			break;
		case BattleNetErrors.ERROR_RPC_REQUEST_TIMED_OUT:
		{
			string messageKey = "GLOBAL_ERROR_NETWORK_UTIL_TIMEOUT";
			ShowConnectionFailureError(messageKey);
			break;
		}
		case BattleNetErrors.ERROR_SESSION_DUPLICATE:
			Error.AddFatal(FatalErrorReason.LOGIN_FROM_ANOTHER_DEVICE, "GLOBAL_ERROR_NETWORK_DUPLICATE_LOGIN");
			break;
		case BattleNetErrors.ERROR_SESSION_DISCONNECTED:
		{
			string messageKey = "GLOBAL_ERROR_NETWORK_DISCONNECT";
			Error.AddFatal(FatalErrorReason.BNET_NETWORK_DISCONNECT, messageKey);
			break;
		}
		case BattleNetErrors.ERROR_ADMIN_KICK:
		case BattleNetErrors.ERROR_SESSION_ADMIN_KICK:
			Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_ADMIN_KICKED");
			break;
		case BattleNetErrors.ERROR_GAME_ACCOUNT_SUSPENDED:
			HearthstoneServices.Get<ILoginService>()?.ClearAuthentication();
			Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_ACCOUNT_SUSPENDED");
			break;
		case BattleNetErrors.ERROR_GAME_ACCOUNT_BANNED:
		case BattleNetErrors.ERROR_BATTLENET_ACCOUNT_BANNED:
			HearthstoneServices.Get<ILoginService>()?.ClearAuthentication();
			Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_ACCOUNT_BANNED");
			break;
		case BattleNetErrors.ERROR_RISK_ACCOUNT_LOCKED:
			HearthstoneServices.Get<ILoginService>()?.ClearAuthentication();
			Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_RISK_ACCOUNT_LOCKED");
			break;
		case BattleNetErrors.ERROR_PARENTAL_CONTROL_RESTRICTION:
			HearthstoneServices.Get<ILoginService>()?.ClearAuthentication();
			Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_PARENTAL_CONTROLS");
			break;
		case BattleNetErrors.ERROR_PHONE_LOCK:
			Error.AddFatal(FatalErrorReason.BNET_PHONE_LOCK, "GLOBAL_ERROR_NETWORK_PHONE_LOCK");
			break;
		case BattleNetErrors.ERROR_LOGON_WEB_VERIFY_TIMEOUT:
			ShowConnectionFailureError("GLOBAL_MOBILE_ERROR_LOGON_WEB_TIMEOUT");
			break;
		case BattleNetErrors.ERROR_SERVER_IS_PRIVATE:
			TelemetryManager.Client().SendNetworkError(NetworkError.ErrorType.PRIVATE_SERVER, info.ToString(), 33);
			ShowConnectionFailureError("GLOBAL_ERROR_UNKNOWN_ERROR");
			Log.Net.PrintWarning("ERROR_SERVER_IS_PRIVATE - {0} connection failures.", m_numConnectionFailures);
			break;
		case BattleNetErrors.ERROR_RPC_PEER_UNAVAILABLE:
			TelemetryManager.Client().SendNetworkError(NetworkError.ErrorType.PEER_UNAVAILABLE, info.ToString(), 3004);
			ShowConnectionFailureError("GLOBAL_ERROR_UNKNOWN_ERROR");
			Log.Net.PrintWarning("ERROR_RPC_PEER_UNAVAILABLE - {0} connection failures.", m_numConnectionFailures);
			break;
		case BattleNetErrors.ERROR_BAD_VERSION:
			if (PlatformSettings.IsMobile() && GameDownloadManagerProvider.Get() != null && !GameDownloadManagerProvider.Get().IsNewMobileVersionReleased)
			{
				Error.AddFatal(FatalErrorReason.UNAVAILABLE_NEW_VERSION, "GLOBAL_ERROR_NETWORK_UNAVAILABLE_NEW_VERSION");
			}
			else
			{
				Error.AddFatal(new ErrorParams
				{
					m_message = GameStrings.Format("GLOBAL_ERROR_NETWORK_UNAVAILABLE_UPGRADE"),
					m_redirectToStore = Error.HAS_APP_STORE,
					m_reason = FatalErrorReason.UNAVAILABLE_UPGRADE
				});
			}
			ReconnectMgr.Get().FullResetRequired = true;
			ReconnectMgr.Get().UpdateRequired = true;
			break;
		case BattleNetErrors.ERROR_SESSION_CAIS_PLAYTIME_EXCEEDED:
			HearthstoneServices.Get<ILoginService>()?.ClearAuthentication();
			Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_ACCOUNT_PLAYTIME_EXCEEDED");
			break;
		case BattleNetErrors.ERROR_SESSION_CAIS_CURFEW:
			HearthstoneServices.Get<ILoginService>()?.ClearAuthentication();
			Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_ACCOUNT_CURFEW_REACHED");
			break;
		case BattleNetErrors.ERROR_SESSION_INVALID_NID:
			HearthstoneServices.Get<ILoginService>()?.ClearAuthentication();
			Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_ACCOUNT_INVALID_NID");
			break;
		default:
		{
			string error2;
			if (HearthstoneApplication.IsInternal())
			{
				error2 = $"Unhandled Bnet Error: {info}";
			}
			else
			{
				Debug.LogError($"Unhandled Bnet Error: {info}");
				error2 = GameStrings.Format("GLOBAL_ERROR_UNKNOWN_ERROR");
			}
			TelemetryManager.Client().SendNetworkError(NetworkError.ErrorType.OTHER_UNKNOWN, info.ToString(), (int)info.GetError());
			ShowConnectionFailureError(error2);
			break;
		}
		}
	}

	private float DelayForConnectionFailures(int numFailures)
	{
		float num = (float)(new System.Random().NextDouble() * 3.0) + 3.5f;
		return (float)Math.Min(numFailures, 3) * num;
	}

	public void EnsureSubscribedTo(UtilSystemId systemChannel)
	{
		m_connectApi.EnsureSubscribedTo(systemChannel);
	}

	private bool OnBnetAuthError(BnetErrorInfo info, object userData)
	{
		return false;
	}

	public static void AcceptFriendInvite(BnetInvitationId inviteid)
	{
		BattleNet.ManageFriendInvite(1, inviteid.GetVal());
	}

	public static void RevokeFriendInvite(BnetInvitationId inviteid)
	{
		BattleNet.ManageFriendInvite(2, inviteid.GetVal());
	}

	public static void DeclineFriendInvite(BnetInvitationId inviteid)
	{
		BattleNet.ManageFriendInvite(3, inviteid.GetVal());
	}

	public static void IgnoreFriendInvite(BnetInvitationId inviteid)
	{
		BattleNet.ManageFriendInvite(4, inviteid.GetVal());
	}

	private static void SendFriendInvite(string sender, string target, bool byEmail)
	{
		BattleNet.SendFriendInvite(sender, target, byEmail);
	}

	public static void SendFriendInviteByEmail(string sender, string target)
	{
		SendFriendInvite(sender, target, byEmail: true);
	}

	public static void SendFriendInviteByBattleTag(string sender, string target)
	{
		SendFriendInvite(sender, target, byEmail: false);
	}

	public static void RemoveFriend(BnetAccountId id)
	{
		BattleNet.RemoveFriend(id);
	}

	public static void GetAccountState(BnetAccountId bnetAccount)
	{
		BattleNet.GetAccountState(bnetAccount);
	}

	public static void SendWhisper(BnetGameAccountId gameAccount, string message)
	{
		BattleNet.SendWhisper(gameAccount, message);
	}

	public void GotoGameServer(GameServerInfo info, bool reconnecting)
	{
		m_state.LastGameServerInfo = info;
		if (m_connectApi.GetGameStartState() != 0 && !ReconnectMgr.Get().IsRestoringGameStateFromDatabase())
		{
			Error.AddDevFatal("GotoGameServer() was called when we're already waiting for a game to start.");
			return;
		}
		string address = info.Address;
		uint uInt = Vars.Key("Application.GameServerPortOverride").GetUInt(info.Port);
		Debug.LogFormat("Network.GotoGameServer -- address= " + address + ":" + uInt + ", game=" + info.GameHandle + ", client=" + info.ClientHandle + ", spectateKey=" + info.SpectatorPassword + " reconnecting=" + reconnecting.ToString());
		if (address != null)
		{
			if (string.IsNullOrEmpty(address) || uInt == 0 || (info.GameHandle == 0 && ShouldBeConnectedToAurora()))
			{
				Debug.LogWarning("Network.GotoGameServer: ERROR in ServerInfo address= " + address + ":" + uInt + ",    game=" + info.GameHandle + ", client=" + info.ClientHandle + " reconnecting=" + reconnecting.ToString());
			}
			m_gameServerKeepAliveFrequencySeconds = 0u;
			m_gameServerKeepAliveRetry = 3u;
			m_gameConceded = false;
			m_disconnectRequested = false;
			m_connectApi.SetTimeLastPingSent(0.0);
			m_connectApi.SetTimeLastPingReceived(0.0);
			m_connectApi.SetPingsSinceLastPong(0);
			if (GameServerDisconnectEvents != null)
			{
				GameServerDisconnectEvents.Clear();
			}
			m_state.LastConnectToGameServerInfo = new ConnectToGameServer();
			m_state.LastConnectToGameServerInfo.TimeSpentMilliseconds = (long)TimeUtils.GetElapsedTimeSinceEpoch().TotalMilliseconds;
			m_state.LastConnectToGameServerInfo.GameSessionInfo = new GameSessionInfo();
			m_state.LastConnectToGameServerInfo.GameSessionInfo.GameServerIpAddress = info.Address;
			m_state.LastConnectToGameServerInfo.GameSessionInfo.GameServerPort = info.Port;
			m_state.LastConnectToGameServerInfo.GameSessionInfo.Version = info.Version;
			m_state.LastConnectToGameServerInfo.GameSessionInfo.GameHandle = info.GameHandle;
			m_state.LastConnectToGameServerInfo.GameSessionInfo.ScenarioId = GameMgr.Get().GetNextMissionId();
			m_state.LastConnectToGameServerInfo.GameSessionInfo.GameType = (Blizzard.Telemetry.WTCG.Client.GameType)GameMgr.Get().GetNextGameType();
			m_state.LastConnectToGameServerInfo.GameSessionInfo.FormatType = (Blizzard.Telemetry.WTCG.Client.FormatType)GameMgr.Get().GetNextFormatType();
			m_state.LastConnectToGameServerInfo.GameSessionInfo.IsReconnect = GameMgr.Get().IsNextReconnect();
			m_state.LastConnectToGameServerInfo.GameSessionInfo.IsSpectating = GameMgr.Get().IsNextSpectator();
			m_state.LastConnectToGameServerInfo.GameSessionInfo.ClientHandle = info.ClientHandle;
			if (GameMgr.Get().LastDeckId.HasValue)
			{
				m_state.LastConnectToGameServerInfo.GameSessionInfo.ClientDeckId = GameMgr.Get().LastDeckId.Value;
			}
			if (GameMgr.Get().LastHeroCardDbId.HasValue)
			{
				m_state.LastConnectToGameServerInfo.GameSessionInfo.ClientHeroCardId = GameMgr.Get().LastHeroCardDbId.Value;
			}
			if (m_connectApi.GotoGameServer(address, uInt))
			{
				SendGameServerHandshake(info);
				m_connectApi.SetGameStartState((!reconnecting) ? GameStartState.InitialStart : GameStartState.Reconnecting);
			}
		}
	}

	private void OnGameServerConnectEvent(BattleNetErrors error)
	{
		Log.GameMgr.Print("Connecting to game server with error code " + error);
		if (m_state.LastConnectToGameServerInfo != null)
		{
			long timeSpentMilliseconds = (long)(TimeUtils.GetElapsedTimeSinceEpoch().TotalMilliseconds - (double)m_state.LastConnectToGameServerInfo.TimeSpentMilliseconds);
			m_state.LastConnectToGameServerInfo.ResultBnetCode = (uint)error;
			m_state.LastConnectToGameServerInfo.ResultBnetCodeString = error.ToString();
			m_state.LastConnectToGameServerInfo.TimeSpentMilliseconds = timeSpentMilliseconds;
			TelemetryManager.Client().SendConnectToGameServer(m_state.LastConnectToGameServerInfo);
			m_state.LastConnectToGameServerInfo = null;
		}
		GameServerInfo lastGameServerJoined = GetLastGameServerJoined();
		if (error == BattleNetErrors.ERROR_OK)
		{
			TelemetryManager.Client().SendConnectSuccess("GAME", lastGameServerJoined?.Address, lastGameServerJoined?.Port);
			TelemetryManager.RegisterShutdownListener(SendDefaultDisconnectTelemetry);
			return;
		}
		TelemetryManager.Client().SendConnectFail("GAME", error.ToString(), lastGameServerJoined?.Address, lastGameServerJoined?.Port);
		GameStartState gameStartState = m_connectApi.GetGameStartState();
		m_connectApi.SetGameStartState(GameStartState.Invalid);
		if (ShouldBeConnectedToAurora())
		{
			if (gameStartState != GameStartState.Reconnecting)
			{
				ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_NO_GAME_SERVER");
				Debug.LogError("Failed to connect to game server with error " + error);
			}
		}
		else
		{
			ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_NO_GAME_SERVER");
			Debug.LogError("Failed to connect to game server with error " + error);
		}
	}

	private void OnGameServerDisconnectEvent(BattleNetErrors error)
	{
		Log.GameMgr.Print("Disconnected from game server with error {0} {1}", (int)error, error.ToString());
		TelemetryManager.UnregisterShutdownListener(SendDefaultDisconnectTelemetry);
		GameServerInfo lastGameServerJoined = GetLastGameServerJoined();
		TelemetryManager.Client().SendDisconnect("GAME", TelemetryUtil.GetReasonFromBnetError(error), (error == BattleNetErrors.ERROR_OK) ? null : error.ToString(), lastGameServerJoined?.Address, lastGameServerJoined?.Port);
		m_state.LastConnectToGameServerInfo = null;
		bool flag = false;
		if (error != 0)
		{
			switch (m_connectApi.GetGameStartState())
			{
			case GameStartState.Reconnecting:
				flag = true;
				break;
			case GameStartState.InitialStart:
				if (!(lastGameServerJoined?.SpectatorMode ?? false))
				{
					Debug.LogError("Disconnected from game server with error " + error);
					ConnectErrorParams connectErrorParams = new ConnectErrorParams();
					connectErrorParams.m_message = GameStrings.Format((error == BattleNetErrors.ERROR_RPC_CONNECTION_TIMED_OUT) ? "GLOBAL_ERROR_NETWORK_CONNECTION_TIMEOUT" : "GLOBAL_ERROR_NETWORK_DISCONNECT_GAME_SERVER");
					AddErrorToList(connectErrorParams);
					flag = true;
				}
				break;
			}
			m_connectApi.SetGameStartState(GameStartState.Invalid);
		}
		if (!flag)
		{
			AddGameServerDisconnectEvent(error);
		}
	}

	private void SendDefaultDisconnectTelemetry()
	{
		GameServerInfo lastGameServerJoined = GetLastGameServerJoined();
		TelemetryManager.Client().SendDisconnect("GAME", TelemetryUtil.GetReasonFromBnetError(BattleNetErrors.ERROR_OK), null, lastGameServerJoined?.Address, lastGameServerJoined?.Port);
	}

	private void AddGameServerDisconnectEvent(BattleNetErrors error)
	{
		if (GameServerDisconnectEvents == null)
		{
			GameServerDisconnectEvents = new List<BattleNetErrors>();
		}
		GameServerDisconnectEvents.Add(error);
	}

	public void SpectateSecondPlayer(GameServerInfo info)
	{
		info.SpectatorMode = true;
		if (!IsConnectedToGameServer())
		{
			GotoGameServer(info, reconnecting: false);
		}
		else
		{
			SendGameServerHandshake(info);
		}
	}

	public bool RetryGotoGameServer()
	{
		if (m_connectApi.GetGameStartState() == GameStartState.Invalid)
		{
			return false;
		}
		SendGameServerHandshake(m_state.LastGameServerInfo);
		return true;
	}

	public GameServerInfo GetLastGameServerJoined()
	{
		return m_state.LastGameServerInfo;
	}

	public void ClearLastGameServerJoined()
	{
		m_state.LastGameServerInfo = null;
	}

	public static string GetUsername()
	{
		string text = null;
		try
		{
			text = GetStoredUserName();
		}
		catch (Exception ex)
		{
			Debug.LogError("Exception while loading settings: " + ex.Message);
		}
		if (text == null)
		{
			text = Vars.Key("Aurora.Username").GetStr("NOT_PROVIDED_PLEASE_PROVIDE_VIA_CONFIG");
		}
		if (text != null && text.IndexOf("@") == -1)
		{
			text += "@blizzard.com";
		}
		return text;
	}

	public static string GetTargetServer()
	{
		bool num = Vars.Key("Aurora.Env.Override").GetInt(0) != 0;
		string text = "default";
		string text2 = null;
		if (num)
		{
			text2 = Vars.Key("Aurora.Env").GetStr(text);
			if (string.IsNullOrEmpty(text2))
			{
				text2 = null;
			}
		}
		if (text2 == null)
		{
			text2 = BattleNet.GetConnectionString();
		}
		if (text2 == null)
		{
			string launchOption = BattleNet.GetLaunchOption("REGION", encrypted: false);
			if (!string.IsNullOrEmpty(launchOption))
			{
				text2 = launchOption switch
				{
					"US" => "us.actual.battle.net", 
					"XX" => "beta.actual.battle.net", 
					"EU" => "eu.actual.battle.net", 
					"CN" => "cn.actual.battle.net", 
					"KR" => "kr.actual.battle.net", 
					_ => text, 
				};
			}
		}
		if (text2.ToLower() == text)
		{
			text2 = "bn11-01.battle.net";
		}
		return text2;
	}

	public static uint GetPort()
	{
		uint num = 0u;
		if (Vars.Key("Aurora.Env.Override").GetUInt(0u) != 0)
		{
			num = Vars.Key("Aurora.Port").GetUInt(0u);
		}
		if (num == 0)
		{
			num = 1119u;
		}
		return num;
	}

	private static SslParameters GetSSLParams()
	{
		SslParameters sslParameters = new SslParameters();
		TextAsset textAsset = (TextAsset)Resources.Load("SSLCert/ssl_cert_bundle");
		if (textAsset != null)
		{
			sslParameters.bundleSettings.bundle = new SslCertBundle(textAsset.bytes);
		}
		sslParameters.bundleSettings.bundleDownloadConfig.numRetries = 3;
		sslParameters.bundleSettings.bundleDownloadConfig.timeoutMs = -1;
		return sslParameters;
	}

	public static string GetVersion()
	{
		return GetVersionFromConfig();
	}

	private static string GetVersionFromConfig()
	{
		string text = null;
		string text2 = Vars.Key("Aurora.Version.Source").GetStr("undefined");
		if (text2 == "undefined")
		{
			text2 = "product";
		}
		if (text2 == "product")
		{
			text = ProductVersion();
		}
		else if (text2 == "string")
		{
			string text3 = "undefined";
			text = Vars.Key("Aurora.Version.String").GetStr(text3);
			if (text == text3)
			{
				Debug.LogError("Aurora.Version.String undefined");
			}
		}
		else
		{
			Debug.LogError("unknown version source: " + text2);
			text = "0";
		}
		string[] commandLineArgs = HearthstoneApplication.CommandLineArgs;
		foreach (string text4 in commandLineArgs)
		{
			if (text4.Equals("hsc") || text4.Equals("-hsc"))
			{
				text = "6969ef511a6cabbc24c5";
				break;
			}
			if (text4.Equals("hse") || text4.Equals("-hse"))
			{
				text = "2b4dbe94fa69d130c1a6";
				break;
			}
			if (text4.Equals("hsdev") || text4.Equals("-hsdev"))
			{
				text = "35d3a7a90e3bf4ad3b87";
				break;
			}
		}
		return text;
	}

	public void OnLoginStarted()
	{
		m_connectApi.OnLoginStarted();
	}

	public void DoLoginUpdate()
	{
		string text = Vars.Key("Application.Referral").GetStr("none");
		if (text.Equals("none"))
		{
			if (PlatformSettings.OS == OSCategory.PC || PlatformSettings.OS == OSCategory.Mac)
			{
				text = "Battle.net";
			}
			else if (PlatformSettings.OS == OSCategory.iOS)
			{
				text = "AppleAppStore";
			}
			else if (PlatformSettings.OS == OSCategory.Android)
			{
				switch (AndroidDeviceSettings.Get().GetAndroidStore())
				{
				case AndroidStore.GOOGLE:
					text = "GooglePlay";
					break;
				case AndroidStore.AMAZON:
					text = "AmazonAppStore";
					break;
				case AndroidStore.HUAWEI:
					text = "HuaweiAppStore";
					break;
				case AndroidStore.ONE_STORE:
					text = "OneStore";
					break;
				case AndroidStore.BLIZZARD:
					if (PlatformSettings.LocaleVariant == LocaleVariant.China)
					{
						text = "JV-Android";
					}
					break;
				}
			}
		}
		m_connectApi.DoLoginUpdate(text);
	}

	public void OnStartupPacketSequenceComplete()
	{
		m_connectApi.OnStartupPacketSequenceComplete();
	}

	public bool IsFindingGame()
	{
		return m_state.FindingBnetGameType != BnetGameType.BGT_UNKNOWN;
	}

	public BnetGameType GetFindingBnetGameType()
	{
		return m_state.FindingBnetGameType;
	}

	public static BnetGameType TranslateGameTypeToBnet(PegasusShared.GameType gameType, PegasusShared.FormatType formatType, int missionId)
	{
		switch (gameType)
		{
		case PegasusShared.GameType.GT_VS_AI:
			return BnetGameType.BGT_VS_AI;
		case PegasusShared.GameType.GT_VS_FRIEND:
			return BnetGameType.BGT_FRIENDS;
		case PegasusShared.GameType.GT_TUTORIAL:
			return BnetGameType.BGT_TUTORIAL;
		case PegasusShared.GameType.GT_RANKED:
		case PegasusShared.GameType.GT_CASUAL:
			return RankMgr.Get().GetBnetGameTypeForLeague(gameType == PegasusShared.GameType.GT_RANKED, formatType);
		case PegasusShared.GameType.GT_ARENA:
			return BnetGameType.BGT_ARENA;
		case PegasusShared.GameType.GT_TAVERNBRAWL:
			if (GameUtils.IsAIMission(missionId))
			{
				return BnetGameType.BGT_TAVERNBRAWL_1P_VERSUS_AI;
			}
			if (GameUtils.IsCoopMission(missionId))
			{
				return BnetGameType.BGT_TAVERNBRAWL_2P_COOP;
			}
			return BnetGameType.BGT_TAVERNBRAWL_PVP;
		case PegasusShared.GameType.GT_FSG_BRAWL_VS_FRIEND:
			return BnetGameType.BGT_FSG_BRAWL_VS_FRIEND;
		case PegasusShared.GameType.GT_FSG_BRAWL:
			return BnetGameType.BGT_FSG_BRAWL_PVP;
		case PegasusShared.GameType.GT_FSG_BRAWL_1P_VS_AI:
			return BnetGameType.BGT_FSG_BRAWL_1P_VERSUS_AI;
		case PegasusShared.GameType.GT_FSG_BRAWL_2P_COOP:
			return BnetGameType.BGT_FSG_BRAWL_2P_COOP;
		case PegasusShared.GameType.GT_BATTLEGROUNDS:
			return BnetGameType.BGT_BATTLEGROUNDS;
		case PegasusShared.GameType.GT_BATTLEGROUNDS_FRIENDLY:
			return BnetGameType.BGT_BATTLEGROUNDS_FRIENDLY;
		case PegasusShared.GameType.GT_PVPDR:
			return BnetGameType.BGT_PVPDR;
		case PegasusShared.GameType.GT_PVPDR_PAID:
			return BnetGameType.BGT_PVPDR_PAID;
		default:
			Error.AddDevFatal("Network.TranslateGameTypeToBnet() - do not know how to translate {0}", gameType);
			return BnetGameType.BGT_UNKNOWN;
		}
	}

	public void FindGame(PegasusShared.GameType gameType, PegasusShared.FormatType formatType, int scenarioId, int brawlLibraryItemId, long deckId, string aiDeck, int heroCardDbId, int? seasonId, bool restoredSavedGameState, byte[] snapshot, PegasusShared.GameType progFilterOverride = PegasusShared.GameType.GT_UNKNOWN)
	{
		if (gameType == PegasusShared.GameType.GT_VS_FRIEND || gameType == PegasusShared.GameType.GT_FSG_BRAWL_VS_FRIEND)
		{
			Error.AddDevFatal("Network.FindGame - friendly challenges must call EnterFriendlyChallengeGame instead.");
			return;
		}
		BnetGameType bnetGameType = TranslateGameTypeToBnet(gameType, formatType, scenarioId);
		if (bnetGameType == BnetGameType.BGT_UNKNOWN)
		{
			Error.AddDevFatal($"FindGame: no bnetGameType for {gameType} {formatType}");
			return;
		}
		m_state.FindingBnetGameType = bnetGameType;
		if (IsNoAccountTutorialGame(bnetGameType))
		{
			GoToNoAccountTutorialServer(scenarioId);
			return;
		}
		bool flag = RequiresScenarioIdAttribute(gameType);
		byte[] array = Guid.NewGuid().ToByteArray();
		long currentFsgId = FiresideGatheringManager.Get().CurrentFsgId;
		Log.BattleNet.PrintInfo("FindGame type={0} scenario={1} deck={2} aideck={3} setScenId={4} request_guid={5}", (int)bnetGameType, scenarioId, deckId, aiDeck, flag ? 1 : 0, (array == null) ? "null" : array.ToHexString());
		bnet.protocol.matchmaking.v1.Player player = new bnet.protocol.matchmaking.v1.Player();
		player.SetGameAccount(BnetPresenceMgr.Get().GetMyGameAccountId().GetGameAccountHandle());
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("type", (long)bnetGameType));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("scenario", scenarioId));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("brawl_library_item_id", brawlLibraryItemId));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("deck", deckId));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("aideck", aiDeck ?? ""));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("request_guid", array));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("fsg_id", currentFsgId));
		if (!string.IsNullOrEmpty(Cheats.Get().GetPlayerTags()))
		{
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("cheat_player_tags", Cheats.Get().GetPlayerTags()));
		}
		Cheats.Get().ClearAllPlayerTags();
		if (heroCardDbId != 0)
		{
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("hero_card_id", heroCardDbId));
		}
		if (seasonId.HasValue)
		{
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("season_id", seasonId.Value));
		}
		List<bnet.protocol.v2.Attribute> list = new List<bnet.protocol.v2.Attribute>();
		list.Add(ProtocolHelper.CreateAttributeV2("GameType", (long)bnetGameType));
		if (flag)
		{
			list.Add(ProtocolHelper.CreateAttributeV2("ScenarioId", scenarioId));
		}
		List<bnet.protocol.v2.Attribute> list2 = new List<bnet.protocol.v2.Attribute>();
		list2.Add(ProtocolHelper.CreateAttributeV2("type", (long)bnetGameType));
		list2.Add(ProtocolHelper.CreateAttributeV2("scenario", scenarioId));
		list2.Add(ProtocolHelper.CreateAttributeV2("brawl_library_item_id", brawlLibraryItemId));
		list2.Add(ProtocolHelper.CreateAttributeV2("prog_filter_override", (long)progFilterOverride));
		if (Cheats.Get().GetBoardId() > 0)
		{
			list2.Add(ProtocolHelper.CreateAttributeV2("cheat_board_override", Cheats.Get().GetBoardId()));
		}
		Cheats.Get().ClearBoardId();
		if (ReconnectMgr.Get().GetBypassReconnect())
		{
			list2.Add(ProtocolHelper.CreateAttributeV2("bypass", val: true));
			ReconnectMgr.Get().SetBypassReconnect(shouldBypass: false);
		}
		if (seasonId.HasValue)
		{
			list2.Add(ProtocolHelper.CreateAttributeV2("season_id", seasonId.Value));
		}
		if (snapshot != null)
		{
			list2.Add(ProtocolHelper.CreateAttributeV2("snapshot", snapshot));
		}
		if (restoredSavedGameState)
		{
			list2.Add(ProtocolHelper.CreateAttributeV2("load_game", val: true));
		}
		BattleNet.QueueMatchmaking(list, list2, player);
		m_state.LastFindGameParameters = new FindGameResult();
		m_state.LastFindGameParameters.TimeSpentMilliseconds = (long)TimeUtils.GetElapsedTimeSinceEpoch().TotalMilliseconds;
		m_state.LastFindGameParameters.GameSessionInfo = new GameSessionInfo();
		m_state.LastFindGameParameters.GameSessionInfo.Version = GetVersion();
		m_state.LastFindGameParameters.GameSessionInfo.ScenarioId = scenarioId;
		m_state.LastFindGameParameters.GameSessionInfo.BrawlLibraryItemId = brawlLibraryItemId;
		if (seasonId.HasValue)
		{
			m_state.LastFindGameParameters.GameSessionInfo.SeasonId = seasonId.Value;
		}
		m_state.LastFindGameParameters.GameSessionInfo.GameType = (Blizzard.Telemetry.WTCG.Client.GameType)gameType;
		m_state.LastFindGameParameters.GameSessionInfo.FormatType = (Blizzard.Telemetry.WTCG.Client.FormatType)formatType;
		m_state.LastFindGameParameters.GameSessionInfo.ClientDeckId = deckId;
		m_state.LastFindGameParameters.GameSessionInfo.ClientHeroCardId = heroCardDbId;
	}

	public void EnterFriendlyChallengeGame(PegasusShared.FormatType formatType, BrawlType brawlType, int scenarioId, int seasonId, int brawlLibraryItemId, DeckShareState player1DeckShareState, long player1DeckId, DeckShareState player2DeckShareState, long player2DeckId, long? player1HeroCardDbId, long? player2HeroCardDbId, BnetGameAccountId player2GameAccountId)
	{
		long val = 1L;
		PegasusShared.GameType gameType = PegasusShared.GameType.GT_VS_FRIEND;
		if (brawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
		{
			val = 40L;
			gameType = PegasusShared.GameType.GT_FSG_BRAWL_VS_FRIEND;
		}
		long currentFsgId = FiresideGatheringManager.Get().CurrentFsgId;
		List<bnet.protocol.v2.Attribute> matchmakerAttributesFilter = new List<bnet.protocol.v2.Attribute>
		{
			ProtocolHelper.CreateAttributeV2("GameType", val),
			ProtocolHelper.CreateAttributeV2("ScenarioId", scenarioId)
		};
		List<bnet.protocol.v2.Attribute> list = new List<bnet.protocol.v2.Attribute>
		{
			ProtocolHelper.CreateAttributeV2("type", val),
			ProtocolHelper.CreateAttributeV2("scenario", scenarioId),
			ProtocolHelper.CreateAttributeV2("format", (long)formatType),
			ProtocolHelper.CreateAttributeV2("season_id", seasonId),
			ProtocolHelper.CreateAttributeV2("brawl_library_item_id", brawlLibraryItemId)
		};
		if (Cheats.Get().GetBoardId() > 0)
		{
			list.Add(ProtocolHelper.CreateAttributeV2("cheat_board_override", Cheats.Get().GetBoardId()));
		}
		Cheats.Get().ClearBoardId();
		bnet.protocol.matchmaking.v1.Player player = new bnet.protocol.matchmaking.v1.Player();
		player.SetGameAccount(BnetPresenceMgr.Get().GetMyGameAccountId().GetGameAccountHandle());
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("type", val));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("scenario", scenarioId));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("deck_share_state", (long)player1DeckShareState));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("deck", player1DeckId));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("player_type", 1L));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("fsg_id", currentFsgId));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("season_id", seasonId));
		if (player1HeroCardDbId.HasValue)
		{
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("hero_card_id", player1HeroCardDbId.Value));
		}
		if (!string.IsNullOrEmpty(Cheats.Get().GetPlayerTags()))
		{
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("cheat_player_tags", Cheats.Get().GetPlayerTags()));
		}
		Cheats.Get().ClearAllPlayerTags();
		bnet.protocol.matchmaking.v1.Player player2 = new bnet.protocol.matchmaking.v1.Player();
		player2.SetGameAccount(player2GameAccountId.GetGameAccountHandle());
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("type", val));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("scenario", scenarioId));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("deck_share_state", (long)player2DeckShareState));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("deck", player2DeckId));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("player_type", 2L));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("fsg_id", currentFsgId));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("season_id", seasonId));
		if (player2HeroCardDbId.HasValue)
		{
			player2.AddAttribute(ProtocolHelper.CreateAttributeV2("hero_card_id", player2HeroCardDbId.Value));
		}
		BattleNet.QueueMatchmaking(matchmakerAttributesFilter, list, player, player2);
		m_state.LastFindGameParameters = new FindGameResult();
		m_state.LastFindGameParameters.TimeSpentMilliseconds = (long)TimeUtils.GetElapsedTimeSinceEpoch().TotalMilliseconds;
		m_state.LastFindGameParameters.GameSessionInfo = new GameSessionInfo();
		m_state.LastFindGameParameters.GameSessionInfo.Version = GetVersion();
		m_state.LastFindGameParameters.GameSessionInfo.ScenarioId = scenarioId;
		m_state.LastFindGameParameters.GameSessionInfo.BrawlLibraryItemId = brawlLibraryItemId;
		m_state.LastFindGameParameters.GameSessionInfo.SeasonId = seasonId;
		m_state.LastFindGameParameters.GameSessionInfo.GameType = (Blizzard.Telemetry.WTCG.Client.GameType)gameType;
		m_state.LastFindGameParameters.GameSessionInfo.FormatType = (Blizzard.Telemetry.WTCG.Client.FormatType)formatType;
		m_state.LastFindGameParameters.GameSessionInfo.ClientDeckId = player1DeckId;
		if (player1HeroCardDbId.HasValue)
		{
			m_state.LastFindGameParameters.GameSessionInfo.ClientHeroCardId = player1HeroCardDbId.Value;
		}
	}

	public void EnterBattlegroundsWithFriend(BnetGameAccountId player2GameAccountId, int scenarioId)
	{
		List<bnet.protocol.v2.Attribute> list = new List<bnet.protocol.v2.Attribute>();
		long val = 50L;
		m_state.FindingBnetGameType = BnetGameType.BGT_BATTLEGROUNDS;
		list.Add(ProtocolHelper.CreateAttributeV2("GameType", val));
		list.Add(ProtocolHelper.CreateAttributeV2("ScenarioId", scenarioId));
		List<bnet.protocol.v2.Attribute> gameAttributes = new List<bnet.protocol.v2.Attribute>
		{
			ProtocolHelper.CreateAttributeV2("type", val),
			ProtocolHelper.CreateAttributeV2("scenario", scenarioId),
			ProtocolHelper.CreateAttributeV2("format", 2L)
		};
		bnet.protocol.matchmaking.v1.Player player = new bnet.protocol.matchmaking.v1.Player();
		player.SetGameAccount(BnetPresenceMgr.Get().GetMyGameAccountId().GetGameAccountHandle());
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("type", val));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("scenario", scenarioId));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("player_type", 1L));
		if (!string.IsNullOrEmpty(Cheats.Get().GetPlayerTags()))
		{
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("cheat_player_tags", Cheats.Get().GetPlayerTags()));
		}
		Cheats.Get().ClearAllPlayerTags();
		bnet.protocol.matchmaking.v1.Player player2 = new bnet.protocol.matchmaking.v1.Player();
		player2.SetGameAccount(player2GameAccountId.GetGameAccountHandle());
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("type", val));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("scenario", scenarioId));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("player_type", 2L));
		BattleNet.QueueMatchmaking(list, gameAttributes, player, player2);
	}

	public void EnterBattlegroundsWithParty(bgs.PartyMember[] members, int scenarioId)
	{
		List<bnet.protocol.v2.Attribute> list = new List<bnet.protocol.v2.Attribute>();
		long val;
		if (members.Length <= PartyManager.Get().GetBattlegroundsMaxRankedPartySize())
		{
			val = 50L;
			m_state.FindingBnetGameType = BnetGameType.BGT_BATTLEGROUNDS;
		}
		else
		{
			val = 51L;
			m_state.FindingBnetGameType = BnetGameType.BGT_BATTLEGROUNDS_FRIENDLY;
		}
		int currentPartySize = PartyManager.Get().GetCurrentPartySize();
		BnetEntityId bnetEntityId = new BnetEntityId();
		if (PartyManager.Get().GetLeader() != null)
		{
			bnetEntityId = PartyManager.Get().GetLeader();
		}
		list.Add(ProtocolHelper.CreateAttributeV2("GameType", val));
		list.Add(ProtocolHelper.CreateAttributeV2("ScenarioId", scenarioId));
		List<bnet.protocol.v2.Attribute> list2 = new List<bnet.protocol.v2.Attribute>();
		list2.Add(ProtocolHelper.CreateAttributeV2("type", val));
		list2.Add(ProtocolHelper.CreateAttributeV2("scenario", scenarioId));
		list2.Add(ProtocolHelper.CreateAttributeV2("format", 2L));
		List<bnet.protocol.matchmaking.v1.Player> list3 = new List<bnet.protocol.matchmaking.v1.Player>();
		foreach (bgs.PartyMember partyMember in members)
		{
			bnet.protocol.matchmaking.v1.Player player = new bnet.protocol.matchmaking.v1.Player();
			player.SetGameAccount(partyMember.GameAccountId.GetGameAccountHandle());
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("type", val));
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("scenario", scenarioId));
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("player_type", 2L));
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("party_size", currentPartySize));
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("party_leader_game_account_id_hi", bnetEntityId.GetHi()));
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("party_leader_game_account_id_lo", bnetEntityId.GetLo()));
			list3.Add(player);
		}
		BattleNet.QueueMatchmaking(list, list2, list3.ToArray());
	}

	public void OnFindGameStateChanged(FindGameState prevState, FindGameState newState, uint errorCode)
	{
		switch (newState)
		{
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_ERROR:
		case FindGameState.SERVER_GAME_CONNECTING:
			SendTelemetry_FindGameResult(errorCode);
			break;
		case FindGameState.CLIENT_CANCELED:
		case FindGameState.BNET_QUEUE_ENTERED:
		case FindGameState.BNET_QUEUE_DELAYED:
		case FindGameState.BNET_QUEUE_UPDATED:
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.SERVER_GAME_STARTED:
		case FindGameState.SERVER_GAME_CANCELED:
			break;
		}
	}

	private void SendTelemetry_FindGameResult(uint errorCode)
	{
		if (m_state.LastFindGameParameters != null)
		{
			string resultCodeString;
			if (errorCode >= 1000000)
			{
				ErrorCode errorCode2 = (ErrorCode)errorCode;
				resultCodeString = errorCode2.ToString();
			}
			else
			{
				BattleNetErrors battleNetErrors = (BattleNetErrors)errorCode;
				resultCodeString = battleNetErrors.ToString();
			}
			long timeSpentMilliseconds = (long)(TimeUtils.GetElapsedTimeSinceEpoch().TotalMilliseconds - (double)m_state.LastFindGameParameters.TimeSpentMilliseconds);
			m_state.LastFindGameParameters.ResultCode = errorCode;
			m_state.LastFindGameParameters.ResultCodeString = resultCodeString;
			m_state.LastFindGameParameters.TimeSpentMilliseconds = timeSpentMilliseconds;
			TelemetryManager.Client().SendFindGameResult(m_state.LastFindGameParameters);
			m_state.LastFindGameParameters = null;
		}
	}

	private static bool RequiresScenarioIdAttribute(PegasusShared.GameType gameType)
	{
		if (gameType == PegasusShared.GameType.GT_VS_FRIEND)
		{
			return true;
		}
		if (GameUtils.IsTavernBrawlGameType(gameType))
		{
			return true;
		}
		return false;
	}

	public void CancelFindGame()
	{
		if (m_state.FindingBnetGameType == BnetGameType.BGT_UNKNOWN)
		{
			return;
		}
		if (!IsLoggedIn())
		{
			m_state.FindingBnetGameType = BnetGameType.BGT_UNKNOWN;
			return;
		}
		BnetGameType findingBnetGameType = GetFindingBnetGameType();
		if (!IsNoAccountTutorialGame(findingBnetGameType))
		{
			BattleNet.CancelMatchmaking();
		}
		m_state.FindingBnetGameType = BnetGameType.BGT_UNKNOWN;
	}

	private bool IsNoAccountTutorialGame(BnetGameType gameType)
	{
		if (ShouldBeConnectedToAurora())
		{
			return false;
		}
		if (gameType != BnetGameType.BGT_TUTORIAL)
		{
			return false;
		}
		return true;
	}

	private void SendGameServerHandshake(GameServerInfo gameInfo)
	{
		NetCache.Get().DispatchClientOptionsToServer();
		if (gameInfo.SpectatorMode)
		{
			BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
			m_connectApi.SendSpectatorGameHandshake(BattleNet.GetVersion(), GetPlatformBuilder(), gameInfo, new BnetId
			{
				Hi = myGameAccountId.GetHi(),
				Lo = myGameAccountId.GetLo()
			});
		}
		else
		{
			m_connectApi.SendGameHandshake(gameInfo, GetPlatformBuilder());
		}
	}

	private void GoToNoAccountTutorialServer(int scenario)
	{
		GameServerInfo gameServerInfo = new GameServerInfo();
		gameServerInfo.Version = BattleNet.GetVersion();
		if (Vars.Key("GameServerOverride.Active").GetBool(def: false))
		{
			gameServerInfo.Address = Vars.Key("GameServerOverride.Address").GetStr("");
			gameServerInfo.Port = Vars.Key("GameServerOverride.Port").GetUInt(0u);
			gameServerInfo.AuroraPassword = "";
		}
		else
		{
			constants.BnetRegion currentRegionId = MobileDeviceLocale.GetCurrentRegionId();
			if (HearthstoneApplication.GetMobileEnvironment() == MobileEnv.PRODUCTION)
			{
				string format;
				try
				{
					format = RegionToTutorialName[currentRegionId];
				}
				catch (KeyNotFoundException)
				{
					Debug.LogWarning("No matching tutorial server name found for region " + currentRegionId);
					format = "us";
				}
				gameServerInfo.Address = string.Format(format, TutorialServer);
				gameServerInfo.Port = 1119u;
			}
			else
			{
				MobileDeviceLocale.ConnectionData connectionDataFromRegionId = MobileDeviceLocale.GetConnectionDataFromRegionId(currentRegionId, isDev: true);
				gameServerInfo.Port = connectionDataFromRegionId.tutorialPort;
				gameServerInfo.Address = (string.IsNullOrEmpty(connectionDataFromRegionId.gameServerAddress) ? "10.130.126.28" : connectionDataFromRegionId.gameServerAddress);
				gameServerInfo.Version = connectionDataFromRegionId.version;
			}
			Log.Net.Print($"Connecting to account-free tutorial server for region {currentRegionId}.  Address: {gameServerInfo.Address}  Port: {gameServerInfo.Port}  Version: {gameServerInfo.Version}");
			gameServerInfo.AuroraPassword = "";
		}
		gameServerInfo.GameHandle = 0u;
		gameServerInfo.ClientHandle = 0L;
		gameServerInfo.Mission = scenario;
		gameServerInfo.BrawlLibraryItemId = 0;
		ResolveAddressAndGotoGameServer(gameServerInfo);
	}

	private void ResolveAddressAndGotoGameServer(GameServerInfo gameServer)
	{
		if (IPAddress.TryParse(gameServer.Address, out var address))
		{
			gameServer.Address = address.ToString();
			Get().GotoGameServer(gameServer, reconnecting: false);
			return;
		}
		try
		{
			IPHostEntry hostEntry = Dns.GetHostEntry(gameServer.Address);
			if (hostEntry.AddressList.Length != 0)
			{
				IPAddress iPAddress = hostEntry.AddressList[0];
				gameServer.Address = iPAddress.ToString();
				Get().GotoGameServer(gameServer, reconnecting: false);
				return;
			}
		}
		catch (Exception ex)
		{
			m_state.LogSource.LogError("Exception within ResolveAddressAndGotoGameServer: " + ex.Message);
		}
		ThrowDnsResolveError(gameServer.Address);
	}

	private void ThrowDnsResolveError(string environment)
	{
		if (HearthstoneApplication.IsInternal())
		{
			Error.AddDevFatal("Environment " + environment + " could not be resolved! Please check your environment and Internet connection!");
		}
		else
		{
			Error.AddFatal(FatalErrorReason.DNS_RESOLVE, "GLOBAL_ERROR_NETWORK_NO_CONNECTION");
		}
	}

	public GameCancelInfo GetGameCancelInfo()
	{
		GameCanceled gameCancelInfo = m_connectApi.GetGameCancelInfo();
		if (gameCancelInfo == null)
		{
			return null;
		}
		return new GameCancelInfo
		{
			CancelReason = (GameCancelInfo.Reason)gameCancelInfo.Reason_
		};
	}

	public void GetGameState()
	{
		m_connectApi.GetGameState();
	}

	public void UpdateBattlegroundInfo()
	{
		m_connectApi.UpdateBattlegroundInfo();
	}

	public void RequestGameRoundHistory()
	{
		m_connectApi.RequestGameRoundHistory();
	}

	public void RequestRealtimeBattlefieldRaces()
	{
		m_connectApi.RequestRealtimeBattlefieldRaces();
	}

	public TurnTimerInfo GetTurnTimerInfo()
	{
		PegasusGame.TurnTimer turnTimerInfo = m_connectApi.GetTurnTimerInfo();
		if (turnTimerInfo == null)
		{
			return null;
		}
		return new TurnTimerInfo
		{
			Seconds = turnTimerInfo.Seconds,
			Turn = turnTimerInfo.Turn,
			Show = turnTimerInfo.Show
		};
	}

	public int GetNAckOption()
	{
		return m_connectApi.GetNAckOption()?.Id ?? 0;
	}

	public SpectatorNotify GetSpectatorNotify()
	{
		return m_connectApi.GetSpectatorNotify();
	}

	public AIDebugInformation GetAIDebugInformation()
	{
		return m_connectApi.GetAIDebugInformation();
	}

	public RopeTimerDebugInformation GetRopeTimerDebugInformation()
	{
		return m_connectApi.GetRopeTimerDebugInformation();
	}

	public ScriptDebugInformation GetScriptDebugInformation()
	{
		return m_connectApi.GetScriptDebugInformation();
	}

	public GameRoundHistory GetGameRoundHistory()
	{
		return m_connectApi.GetGameRoundHistory();
	}

	public GameRealTimeBattlefieldRaces GetGameRealTimeBattlefieldRaces()
	{
		return m_connectApi.GetGameRealTimeBattlefieldRaces();
	}

	public BattlegroundsRatingChange GetBattlegroundsRatingChange()
	{
		return m_connectApi.GetBattlegroundsRatingChange();
	}

	public GameGuardianVars GetGameGuardianVars()
	{
		return m_connectApi.GetGameGuardianVars();
	}

	public UpdateBattlegroundInfo GetBattlegroundInfo()
	{
		return m_connectApi.GetBattlegroundInfo();
	}

	public DebugMessage GetDebugMessage()
	{
		return m_connectApi.GetDebugMessage();
	}

	public ScriptLogMessage GetScriptLogMessage()
	{
		return m_connectApi.GetScriptLogMessage();
	}

	public AchievementProgress GetAchievementInGameProgress()
	{
		return m_connectApi.GetAchievementInGameProgress();
	}

	public AchievementComplete GetAchievementComplete()
	{
		return m_connectApi.GetAchievementComplete();
	}

	public void DisconnectFromGameServer()
	{
		if (IsConnectedToGameServer())
		{
			m_disconnectRequested = true;
			m_connectApi.DisconnectFromGameServer();
		}
	}

	public bool WasDisconnectRequested()
	{
		return m_disconnectRequested;
	}

	public bool IsConnectedToGameServer()
	{
		return m_connectApi.IsConnectedToGameServer();
	}

	public bool GameServerHasEvents()
	{
		return m_connectApi.GameServerHasEvents();
	}

	public bool WasGameConceded()
	{
		return m_gameConceded;
	}

	public void Concede()
	{
		m_gameConceded = true;
		m_connectApi.Concede();
	}

	public void AutoConcede()
	{
		if (IsConnectedToGameServer() && !WasGameConceded())
		{
			Concede();
		}
	}

	public EntityChoices GetEntityChoices()
	{
		PegasusGame.EntityChoices entityChoices = m_connectApi.GetEntityChoices();
		if (entityChoices == null)
		{
			return null;
		}
		return new EntityChoices
		{
			ID = entityChoices.Id,
			ChoiceType = (CHOICE_TYPE)entityChoices.ChoiceType,
			CountMax = entityChoices.CountMax,
			CountMin = entityChoices.CountMin,
			Entities = CopyIntList(entityChoices.Entities),
			Source = entityChoices.Source,
			PlayerId = entityChoices.PlayerId,
			HideChosen = entityChoices.HideChosen
		};
	}

	public EntitiesChosen GetEntitiesChosen()
	{
		PegasusGame.EntitiesChosen entitiesChosen = m_connectApi.GetEntitiesChosen();
		if (entitiesChosen == null)
		{
			return null;
		}
		return new EntitiesChosen
		{
			ID = entitiesChosen.ChooseEntities.Id,
			Entities = CopyIntList(entitiesChosen.ChooseEntities.Entities),
			PlayerId = entitiesChosen.PlayerId,
			ChoiceType = (CHOICE_TYPE)entitiesChosen.ChoiceType
		};
	}

	public void SendChoices(int id, List<int> picks)
	{
		m_connectApi.SendChoices(id, picks);
	}

	public void SendOption(int id, int index, int target, int sub, int pos)
	{
		m_connectApi.SendOption(id, index, target, sub, pos);
	}

	public void SendFreeDeckChoice(int classId, long noticeId)
	{
		m_connectApi.SendFreeDeckChoice(classId, noticeId);
	}

	public Options GetOptions()
	{
		AllOptions allOptions = m_connectApi.GetAllOptions();
		Options options = new Options
		{
			ID = allOptions.Id
		};
		for (int i = 0; i < allOptions.Options.Count; i++)
		{
			PegasusGame.Option option = allOptions.Options[i];
			Options.Option option2 = new Options.Option();
			option2.Type = (Options.Option.OptionType)option.Type_;
			if (option.HasMainOption)
			{
				option2.Main.ID = option.MainOption.Id;
				option2.Main.PlayErrorInfo.PlayError = (PlayErrors.ErrorType)option.MainOption.PlayError;
				option2.Main.PlayErrorInfo.PlayErrorParam = (option.MainOption.HasPlayErrorParam ? new int?(option.MainOption.PlayErrorParam) : null);
				option2.Main.Targets = CopyTargetOptionList(option.MainOption.Targets);
			}
			for (int j = 0; j < option.SubOptions.Count; j++)
			{
				SubOption subOption = option.SubOptions[j];
				Options.Option.SubOption subOption2 = new Options.Option.SubOption();
				subOption2.ID = subOption.Id;
				subOption2.PlayErrorInfo.PlayError = (PlayErrors.ErrorType)subOption.PlayError;
				subOption2.PlayErrorInfo.PlayErrorParam = (subOption.HasPlayErrorParam ? new int?(subOption.PlayErrorParam) : null);
				subOption2.Targets = CopyTargetOptionList(subOption.Targets);
				option2.Subs.Add(subOption2);
			}
			options.List.Add(option2);
		}
		return options;
	}

	private List<Options.Option.TargetOption> CopyTargetOptionList(IList<TargetOption> originalList)
	{
		List<Options.Option.TargetOption> list = new List<Options.Option.TargetOption>();
		for (int i = 0; i < originalList.Count; i++)
		{
			TargetOption targetOption = originalList[i];
			Options.Option.TargetOption targetOption2 = new Options.Option.TargetOption();
			targetOption2.CopyFrom(targetOption);
			list.Add(targetOption2);
		}
		return list;
	}

	private List<int> CopyIntList(IList<int> intList)
	{
		int[] array = new int[intList.Count];
		intList.CopyTo(array, 0);
		return new List<int>(array);
	}

	public void SendUserUI(int overCard, int heldCard, int arrowOrigin, int x, int y)
	{
		if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.ShowUserUI != 0)
		{
			m_connectApi.SendUserUi(overCard, heldCard, arrowOrigin, x, y);
		}
	}

	public void SendEmote(EmoteType emote)
	{
		m_connectApi.SendEmote((int)emote);
	}

	public void SendSpectatorInvite(BnetAccountId bnetAccountId, BnetGameAccountId bnetGameAccountId)
	{
		BnetId targetBnetId = new BnetId
		{
			Hi = bnetAccountId.GetHi(),
			Lo = bnetAccountId.GetLo()
		};
		BnetId targetGameAccountId = new BnetId
		{
			Hi = bnetGameAccountId.GetHi(),
			Lo = bnetGameAccountId.GetLo()
		};
		m_connectApi.SendSpectatorInvite(targetBnetId, targetGameAccountId);
	}

	public void SendRemoveSpectators(bool regenerateSpectatorPassword, params BnetGameAccountId[] bnetGameAccountIds)
	{
		List<BnetId> list = new List<BnetId>();
		for (int i = 0; i < bnetGameAccountIds.Length; i++)
		{
			list.Add(new BnetId
			{
				Hi = bnetGameAccountIds[i].GetHi(),
				Lo = bnetGameAccountIds[i].GetLo()
			});
		}
		m_connectApi.SendRemoveSpectators(regenerateSpectatorPassword, list);
	}

	public void SendRemoveAllSpectators(bool regenerateSpectatorPassword)
	{
		m_connectApi.SendRemoveAllSpectators(regenerateSpectatorPassword);
	}

	public UserUI GetUserUI()
	{
		PegasusGame.UserUI userUi = m_connectApi.GetUserUi();
		if (userUi == null)
		{
			return null;
		}
		UserUI userUI = new UserUI();
		if (userUi.HasPlayerId)
		{
			userUI.playerId = userUi.PlayerId;
		}
		if (userUi.HasMouseInfo)
		{
			MouseInfo mouseInfo = userUi.MouseInfo;
			userUI.mouseInfo = new UserUI.MouseInfo();
			userUI.mouseInfo.ArrowOriginID = mouseInfo.ArrowOrigin;
			userUI.mouseInfo.HeldCardID = mouseInfo.HeldCard;
			userUI.mouseInfo.OverCardID = mouseInfo.OverCard;
			userUI.mouseInfo.X = mouseInfo.X;
			userUI.mouseInfo.Y = mouseInfo.Y;
		}
		else if (userUi.HasEmote)
		{
			userUI.emoteInfo = new UserUI.EmoteInfo();
			userUI.emoteInfo.Emote = userUi.Emote;
		}
		return userUI;
	}

	public GameSetup GetGameSetupInfo()
	{
		PegasusGame.GameSetup gameSetup = m_connectApi.GetGameSetup();
		if (gameSetup == null)
		{
			return null;
		}
		GameSetup gameSetup2 = new GameSetup();
		gameSetup2.Board = gameSetup.Board;
		gameSetup2.MaxSecretZoneSizePerPlayer = gameSetup.MaxSecretZoneSizePerPlayer;
		gameSetup2.MaxSecretsPerPlayer = gameSetup.MaxSecretsPerPlayer;
		gameSetup2.MaxQuestsPerPlayer = gameSetup.MaxQuestsPerPlayer;
		gameSetup2.MaxFriendlyMinionsPerPlayer = gameSetup.MaxFriendlyMinionsPerPlayer;
		if (gameSetup.HasKeepAliveFrequencySeconds)
		{
			m_gameServerKeepAliveFrequencySeconds = gameSetup.KeepAliveFrequencySeconds;
		}
		else
		{
			m_gameServerKeepAliveFrequencySeconds = 0u;
		}
		if (gameSetup.HasKeepAliveRetry)
		{
			m_gameServerKeepAliveRetry = gameSetup.KeepAliveRetry;
		}
		else
		{
			m_gameServerKeepAliveRetry = 1u;
		}
		if (gameSetup.HasKeepAliveWaitForInternetSeconds)
		{
			m_gameServerKeepAliveWaitForInternetSeconds = gameSetup.KeepAliveWaitForInternetSeconds;
		}
		else
		{
			m_gameServerKeepAliveWaitForInternetSeconds = 20u;
		}
		if (gameSetup.HasDisconnectWhenStuckSeconds)
		{
			gameSetup2.DisconnectWhenStuckSeconds = gameSetup.DisconnectWhenStuckSeconds;
		}
		return gameSetup2;
	}

	public List<PowerHistory> GetPowerHistory()
	{
		PegasusGame.PowerHistory powerHistory = m_connectApi.GetPowerHistory();
		if (powerHistory == null)
		{
			return null;
		}
		List<PowerHistory> list = new List<PowerHistory>();
		for (int i = 0; i < powerHistory.List.Count; i++)
		{
			PowerHistoryData powerHistoryData = powerHistory.List[i];
			PowerHistory powerHistory2 = null;
			if (powerHistoryData.HasFullEntity)
			{
				powerHistory2 = GetFullEntity(powerHistoryData.FullEntity);
			}
			else if (powerHistoryData.HasShowEntity)
			{
				powerHistory2 = GetShowEntity(powerHistoryData.ShowEntity);
			}
			else if (powerHistoryData.HasHideEntity)
			{
				powerHistory2 = GetHideEntity(powerHistoryData.HideEntity);
			}
			else if (powerHistoryData.HasChangeEntity)
			{
				powerHistory2 = GetChangeEntity(powerHistoryData.ChangeEntity);
			}
			else if (powerHistoryData.HasTagChange)
			{
				powerHistory2 = GetTagChange(powerHistoryData.TagChange);
			}
			else if (powerHistoryData.HasPowerStart)
			{
				powerHistory2 = GetBlockStart(powerHistoryData.PowerStart);
			}
			else if (powerHistoryData.HasPowerEnd)
			{
				powerHistory2 = GetBlockEnd(powerHistoryData.PowerEnd);
			}
			else if (powerHistoryData.HasCreateGame)
			{
				powerHistory2 = GetCreateGame(powerHistoryData.CreateGame);
			}
			else if (powerHistoryData.HasResetGame)
			{
				powerHistory2 = GetResetGame(powerHistoryData.ResetGame);
			}
			else if (powerHistoryData.HasMetaData)
			{
				powerHistory2 = GetMetaData(powerHistoryData.MetaData);
			}
			else if (powerHistoryData.HasSubSpellStart)
			{
				powerHistory2 = GetSubSpellStart(powerHistoryData.SubSpellStart);
			}
			else if (powerHistoryData.HasSubSpellEnd)
			{
				powerHistory2 = GetSubSpellEnd(powerHistoryData.SubSpellEnd);
			}
			else if (powerHistoryData.HasVoSpell)
			{
				powerHistory2 = GetVoSpell(powerHistoryData.VoSpell);
			}
			else if (powerHistoryData.HasCachedTagForDormantChange)
			{
				powerHistory2 = GetCachedTagForDormantChange(powerHistoryData.CachedTagForDormantChange);
			}
			else if (powerHistoryData.HasShuffleDeck)
			{
				powerHistory2 = GetShuffleDeck(powerHistoryData.ShuffleDeck);
			}
			else
			{
				Debug.LogError("Network.GetPowerHistory() - received invalid PowerHistoryData packet");
			}
			if (powerHistory2 != null)
			{
				list.Add(powerHistory2);
			}
		}
		return list;
	}

	private static HistFullEntity GetFullEntity(PowerHistoryEntity entity)
	{
		return new HistFullEntity
		{
			Entity = Entity.CreateFromProto(entity)
		};
	}

	private static HistShowEntity GetShowEntity(PowerHistoryEntity entity)
	{
		return new HistShowEntity
		{
			Entity = Entity.CreateFromProto(entity)
		};
	}

	private static HistHideEntity GetHideEntity(PowerHistoryHide hide)
	{
		return new HistHideEntity
		{
			Entity = hide.Entity,
			Zone = hide.Zone
		};
	}

	private static HistChangeEntity GetChangeEntity(PowerHistoryEntity entity)
	{
		return new HistChangeEntity
		{
			Entity = Entity.CreateFromProto(entity)
		};
	}

	private static HistTagChange GetTagChange(PowerHistoryTagChange tagChange)
	{
		return new HistTagChange
		{
			Entity = tagChange.Entity,
			Tag = tagChange.Tag,
			Value = tagChange.Value,
			ChangeDef = tagChange.ChangeDef
		};
	}

	private static HistBlockStart GetBlockStart(PowerHistoryStart start)
	{
		return new HistBlockStart(start.Type)
		{
			Entities = new List<int> { start.Source },
			Target = start.Target,
			SubOption = start.SubOption,
			EffectCardId = new List<string> { start.EffectCardId },
			IsEffectCardIdClientCached = new List<bool> { false },
			EffectIndex = start.EffectIndex,
			TriggerKeyword = start.TriggerKeyword,
			ShowInHistory = start.ShowInHistory,
			IsDeferrable = start.IsDeferrable,
			IsBatchable = start.IsBatchable,
			IsDeferBlocker = start.IsDeferBlocker,
			ForceShowBigCard = start.ForceShowBigCard
		};
	}

	private static HistBlockEnd GetBlockEnd(PowerHistoryEnd end)
	{
		return new HistBlockEnd();
	}

	private static HistCreateGame GetCreateGame(PowerHistoryCreateGame createGame)
	{
		return HistCreateGame.CreateFromProto(createGame);
	}

	private static HistResetGame GetResetGame(PowerHistoryResetGame resetGame)
	{
		return HistResetGame.CreateFromProto(resetGame);
	}

	private static HistMetaData GetMetaData(PowerHistoryMetaData metaData)
	{
		HistMetaData histMetaData = new HistMetaData();
		histMetaData.MetaType = (metaData.HasType ? metaData.Type : HistoryMeta.Type.TARGET);
		histMetaData.Data = (metaData.HasData ? metaData.Data : 0);
		for (int i = 0; i < metaData.Info.Count; i++)
		{
			int item = metaData.Info[i];
			histMetaData.Info.Add(item);
		}
		for (int j = 0; j < metaData.AdditionalData.Count; j++)
		{
			int item2 = metaData.AdditionalData[j];
			histMetaData.AdditionalData.Add(item2);
		}
		return histMetaData;
	}

	private static HistSubSpellStart GetSubSpellStart(PowerHistorySubSpellStart subSpellStart)
	{
		return HistSubSpellStart.CreateFromProto(subSpellStart);
	}

	private static HistSubSpellEnd GetSubSpellEnd(PowerHistorySubSpellEnd subSpellEnd)
	{
		return new HistSubSpellEnd();
	}

	private static HistVoSpell GetVoSpell(PowerHistoryVoTask voSubspellTask)
	{
		return HistVoSpell.CreateFromProto(voSubspellTask);
	}

	private static HistCachedTagForDormantChange GetCachedTagForDormantChange(PowerHistoryCachedTagForDormantChange tagChange)
	{
		return HistCachedTagForDormantChange.CreateFromProto(tagChange);
	}

	private static HistShuffleDeck GetShuffleDeck(PowerHistoryShuffleDeck shuffleDeck)
	{
		return HistShuffleDeck.CreateFromProto(shuffleDeck);
	}

	private static List<int> MakeChoicesList(int choice1, int choice2, int choice3)
	{
		List<int> list = new List<int>();
		if (choice1 == 0)
		{
			return null;
		}
		list.Add(choice1);
		if (choice2 == 0)
		{
			return list;
		}
		list.Add(choice2);
		if (choice3 == 0)
		{
			return list;
		}
		list.Add(choice3);
		return list;
	}

	public void ValidateAchieve(int achieveID)
	{
		Log.Achievements.Print("Validating achieve: " + achieveID);
		m_connectApi.ValidateAchieve(achieveID);
	}

	public ValidateAchieveResponse GetValidatedAchieve()
	{
		return m_connectApi.GetValidateAchieveResponse();
	}

	public void RequestCancelQuest(int achieveID)
	{
		m_connectApi.RequestCancelQuest(achieveID);
	}

	public CanceledQuest GetCanceledQuest()
	{
		CancelQuestResponse canceledQuestResponse = m_connectApi.GetCanceledQuestResponse();
		if (canceledQuestResponse == null)
		{
			return null;
		}
		return new CanceledQuest
		{
			AchieveID = canceledQuestResponse.QuestId,
			Canceled = canceledQuestResponse.Success,
			NextQuestCancelDate = (canceledQuestResponse.HasNextQuestCancel ? TimeUtils.PegDateToFileTimeUtc(canceledQuestResponse.NextQuestCancel) : 0)
		};
	}

	public TriggeredEvent GetTriggerEventResponse()
	{
		TriggerEventResponse triggerEventResponse = m_connectApi.GetTriggerEventResponse();
		if (triggerEventResponse == null)
		{
			return null;
		}
		return new TriggeredEvent
		{
			EventID = triggerEventResponse.EventId,
			Success = triggerEventResponse.Success
		};
	}

	public void RequestAdventureProgress()
	{
		m_connectApi.RequestAdventureProgress();
	}

	public List<AdventureProgress> GetAdventureProgressResponse()
	{
		AdventureProgressResponse adventureProgressResponse = m_connectApi.GetAdventureProgressResponse();
		if (adventureProgressResponse == null)
		{
			return null;
		}
		List<AdventureProgress> list = new List<AdventureProgress>();
		for (int i = 0; i < adventureProgressResponse.List.Count; i++)
		{
			PegasusShared.AdventureProgress adventureProgress = adventureProgressResponse.List[i];
			AdventureProgress adventureProgress2 = new AdventureProgress();
			adventureProgress2.Wing = adventureProgress.WingId;
			adventureProgress2.Progress = adventureProgress.Progress;
			adventureProgress2.Ack = adventureProgress.Ack;
			adventureProgress2.Flags = adventureProgress.Flags_;
			list.Add(adventureProgress2);
		}
		return list;
	}

	public BeginDraft GetBeginDraft()
	{
		DraftBeginning draftBeginning = m_connectApi.GetDraftBeginning();
		if (draftBeginning == null)
		{
			return null;
		}
		BeginDraft beginDraft = new BeginDraft();
		beginDraft.DeckID = draftBeginning.DeckId;
		for (int i = 0; i < draftBeginning.ChoiceList.Count; i++)
		{
			PegasusShared.CardDef cardDef = draftBeginning.ChoiceList[i];
			NetCache.CardDefinition item = new NetCache.CardDefinition
			{
				Name = GameUtils.TranslateDbIdToCardId(cardDef.Asset),
				Premium = (TAG_PREMIUM)cardDef.Premium
			};
			beginDraft.Heroes.Add(item);
		}
		beginDraft.Wins = (draftBeginning.HasCurrentSession ? draftBeginning.CurrentSession.Wins : 0);
		beginDraft.MaxSlot = draftBeginning.MaxSlot;
		if (draftBeginning.HasCurrentSession)
		{
			beginDraft.Session = draftBeginning.CurrentSession;
		}
		beginDraft.SlotType = draftBeginning.SlotType;
		beginDraft.UniqueSlotTypesForDraft = draftBeginning.UniqueSlotTypes;
		return beginDraft;
	}

	public DraftError GetDraftError()
	{
		return m_connectApi.DraftGetError();
	}

	public DraftChoicesAndContents GetDraftChoicesAndContents()
	{
		PegasusUtil.DraftChoicesAndContents draftChoicesAndContents = m_connectApi.GetDraftChoicesAndContents();
		if (draftChoicesAndContents == null)
		{
			return null;
		}
		DraftChoicesAndContents draftChoicesAndContents2 = new DraftChoicesAndContents();
		draftChoicesAndContents2.DeckInfo.Deck = draftChoicesAndContents.DeckId;
		draftChoicesAndContents2.Slot = draftChoicesAndContents.Slot;
		draftChoicesAndContents2.Hero.Name = ((draftChoicesAndContents.HeroDef.Asset == 0) ? string.Empty : GameUtils.TranslateDbIdToCardId(draftChoicesAndContents.HeroDef.Asset));
		draftChoicesAndContents2.Hero.Premium = (TAG_PREMIUM)draftChoicesAndContents.HeroDef.Premium;
		draftChoicesAndContents2.Wins = draftChoicesAndContents.CurrentSession.Wins;
		draftChoicesAndContents2.Losses = draftChoicesAndContents.CurrentSession.Losses;
		draftChoicesAndContents2.MaxWins = (draftChoicesAndContents.HasMaxWins ? draftChoicesAndContents.MaxWins : int.MaxValue);
		draftChoicesAndContents2.MaxSlot = draftChoicesAndContents.MaxSlot;
		if (draftChoicesAndContents.HasCurrentSession)
		{
			draftChoicesAndContents2.Session = draftChoicesAndContents.CurrentSession;
		}
		if (draftChoicesAndContents.HasHeroPowerDef)
		{
			draftChoicesAndContents2.HeroPower.Name = ((draftChoicesAndContents.HeroPowerDef.Asset == 0) ? string.Empty : GameUtils.TranslateDbIdToCardId(draftChoicesAndContents.HeroPowerDef.Asset));
		}
		for (int i = 0; i < draftChoicesAndContents.ChoiceList.Count; i++)
		{
			PegasusShared.CardDef cardDef = draftChoicesAndContents.ChoiceList[i];
			if (cardDef.Asset != 0)
			{
				NetCache.CardDefinition item = new NetCache.CardDefinition
				{
					Name = GameUtils.TranslateDbIdToCardId(cardDef.Asset),
					Premium = (TAG_PREMIUM)cardDef.Premium
				};
				draftChoicesAndContents2.Choices.Add(item);
			}
		}
		for (int j = 0; j < draftChoicesAndContents.Cards.Count; j++)
		{
			DeckCardData deckCardData = draftChoicesAndContents.Cards[j];
			CardUserData cardUserData = new CardUserData();
			cardUserData.DbId = deckCardData.Def.Asset;
			cardUserData.Count = ((!deckCardData.HasQty) ? 1 : deckCardData.Qty);
			cardUserData.Premium = (deckCardData.Def.HasPremium ? ((TAG_PREMIUM)deckCardData.Def.Premium) : TAG_PREMIUM.NORMAL);
			draftChoicesAndContents2.DeckInfo.Cards.Add(cardUserData);
		}
		draftChoicesAndContents2.Chest = (draftChoicesAndContents.HasChest ? ConvertRewardChest(draftChoicesAndContents.Chest) : null);
		draftChoicesAndContents2.SlotType = draftChoicesAndContents.SlotType;
		draftChoicesAndContents2.UniqueSlotTypesForDraft.AddRange(draftChoicesAndContents.UniqueSlotTypes);
		return draftChoicesAndContents2;
	}

	public DraftChosen GetDraftChosen()
	{
		PegasusUtil.DraftChosen draftChosen = m_connectApi.GetDraftChosen();
		if (draftChosen == null)
		{
			return null;
		}
		NetCache.CardDefinition chosenCard = new NetCache.CardDefinition
		{
			Name = GameUtils.TranslateDbIdToCardId(draftChosen.Chosen.Asset),
			Premium = (TAG_PREMIUM)draftChosen.Chosen.Premium
		};
		List<NetCache.CardDefinition> list = new List<NetCache.CardDefinition>();
		for (int i = 0; i < draftChosen.NextChoiceList.Count; i++)
		{
			PegasusShared.CardDef cardDef = draftChosen.NextChoiceList[i];
			NetCache.CardDefinition item = new NetCache.CardDefinition
			{
				Name = GameUtils.TranslateDbIdToCardId(cardDef.Asset),
				Premium = (TAG_PREMIUM)cardDef.Premium
			};
			list.Add(item);
		}
		return new DraftChosen
		{
			ChosenCard = chosenCard,
			NextChoices = list,
			SlotType = draftChosen.SlotType
		};
	}

	public void MakeDraftChoice(long deckID, int slot, int index, int premium)
	{
		m_connectApi.DraftMakePick(deckID, slot, index, premium);
	}

	public void RequestDraftChoicesAndContents()
	{
		m_connectApi.RequestDraftChoicesAndContents();
	}

	public void SendArenaSessionRequest()
	{
		m_connectApi.SendArenaSessionRequest();
	}

	public ArenaSessionResponse GetArenaSessionResponse()
	{
		return m_connectApi.GetArenaSessionResponse();
	}

	public void DraftBegin()
	{
		m_connectApi.DraftBegin();
	}

	public void DraftRetire(long deckID, int slot, int seasonId)
	{
		m_connectApi.DraftRetire(deckID, slot, seasonId);
	}

	public DraftRetired GetRetiredDraft()
	{
		PegasusUtil.DraftRetired draftRetired = m_connectApi.GetDraftRetired();
		if (draftRetired == null)
		{
			return null;
		}
		return new DraftRetired
		{
			Deck = draftRetired.DeckId,
			Chest = ConvertRewardChest(draftRetired.Chest)
		};
	}

	public void AckDraftRewards(long deckID, int slot)
	{
		m_connectApi.DraftAckRewards(deckID, slot);
	}

	public long GetRewardsAckDraftID()
	{
		return m_connectApi.DraftRewardsAcked()?.DeckId ?? 0;
	}

	public void DraftRequestDisablePremiums()
	{
		m_connectApi.DraftRequestDisablePremiums();
	}

	public DraftChoicesAndContents GetDraftRemovePremiumsResponse()
	{
		DraftRemovePremiumsResponse draftDisablePremiumsResponse = m_connectApi.GetDraftDisablePremiumsResponse();
		DraftChoicesAndContents draftChoicesAndContents = new DraftChoicesAndContents();
		for (int i = 0; i < draftDisablePremiumsResponse.ChoiceList.Count; i++)
		{
			PegasusShared.CardDef cardDef = draftDisablePremiumsResponse.ChoiceList[i];
			if (cardDef.Asset != 0)
			{
				NetCache.CardDefinition item = new NetCache.CardDefinition
				{
					Name = GameUtils.TranslateDbIdToCardId(cardDef.Asset),
					Premium = (TAG_PREMIUM)cardDef.Premium
				};
				draftChoicesAndContents.Choices.Add(item);
			}
		}
		for (int j = 0; j < draftDisablePremiumsResponse.Cards.Count; j++)
		{
			DeckCardData deckCardData = draftDisablePremiumsResponse.Cards[j];
			CardUserData cardUserData = new CardUserData();
			cardUserData.DbId = deckCardData.Def.Asset;
			cardUserData.Count = ((!deckCardData.HasQty) ? 1 : deckCardData.Qty);
			cardUserData.Premium = (deckCardData.Def.HasPremium ? ((TAG_PREMIUM)deckCardData.Def.Premium) : TAG_PREMIUM.NORMAL);
			draftChoicesAndContents.DeckInfo.Cards.Add(cardUserData);
		}
		return draftChoicesAndContents;
	}

	public static RewardChest ConvertRewardChest(PegasusShared.RewardChest chest)
	{
		RewardChest rewardChest = new RewardChest();
		for (int i = 0; i < chest.Bag.Count; i++)
		{
			rewardChest.Rewards.Add(ConvertRewardBag(chest.Bag[i]));
		}
		return rewardChest;
	}

	public static RewardData ConvertRewardBag(RewardBag bag)
	{
		if (bag.HasRewardBooster)
		{
			return new BoosterPackRewardData(bag.RewardBooster.BoosterType, bag.RewardBooster.BoosterCount);
		}
		if (bag.HasRewardCard)
		{
			return new CardRewardData(GameUtils.TranslateDbIdToCardId(bag.RewardCard.Card.Asset), (TAG_PREMIUM)bag.RewardCard.Card.Premium, bag.RewardCard.Quantity);
		}
		if (bag.HasRewardDust)
		{
			return new ArcaneDustRewardData(bag.RewardDust.Amount);
		}
		if (bag.HasRewardGold)
		{
			return new GoldRewardData(bag.RewardGold.Amount);
		}
		if (bag.HasRewardCardBack)
		{
			return new CardBackRewardData(bag.RewardCardBack.CardBack);
		}
		if (bag.HasRewardArenaTicket)
		{
			return new ForgeTicketRewardData(bag.RewardArenaTicket.Quantity);
		}
		Debug.LogError("Unrecognized reward bag reward");
		return null;
	}

	public void MassDisenchant()
	{
		m_connectApi.MassDisenchant();
	}

	public MassDisenchantResponse GetMassDisenchantResponse()
	{
		PegasusUtil.MassDisenchantResponse massDisenchantResponse = m_connectApi.GetMassDisenchantResponse();
		if (massDisenchantResponse == null)
		{
			return null;
		}
		if (massDisenchantResponse.HasCollectionVersion)
		{
			NetCache.Get().AddExpectedCollectionModification(massDisenchantResponse.CollectionVersion);
		}
		return new MassDisenchantResponse
		{
			Amount = massDisenchantResponse.Amount
		};
	}

	public void SetFavoriteHero(TAG_CLASS heroClass, NetCache.CardDefinition hero)
	{
		PegasusShared.CardDef cardDef = new PegasusShared.CardDef
		{
			Asset = GameUtils.TranslateCardIdToDbId(hero.Name),
			Premium = (int)hero.Premium
		};
		if (IsLoggedIn())
		{
			m_connectApi.SetFavoriteHero((int)heroClass, cardDef);
			return;
		}
		OfflineDataCache.SetFavoriteHero((int)heroClass, cardDef, wasCalledOffline: true);
		NetCache.NetCacheFavoriteHeroes netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFavoriteHeroes>();
		if (netObject != null)
		{
			netObject.FavoriteHeroes[heroClass] = hero;
		}
	}

	public void SetTag(int tagID, int entityID, int tagValue)
	{
		SendDebugConsoleCommand($"settag {tagID} {entityID} {tagValue}");
	}

	public void SetTag(int tagID, string entityIdentifier, int tagValue)
	{
		SendDebugConsoleCommand($"settag {tagID} {entityIdentifier} {tagValue}");
	}

	public void PrintPersistentList(int entityID)
	{
		SendDebugConsoleCommand($"printpersistentlist {entityID}");
	}

	public void DebugScript(string powerGUID)
	{
		SendDebugConsoleCommand($"debugscript {powerGUID}");
	}

	public void DisableScriptDebug()
	{
		SendDebugConsoleCommand("disablescriptdebug");
	}

	public void DebugRopeTimer()
	{
		SendDebugConsoleCommand("debugropetimer");
	}

	public void DisableDebugRopeTimer()
	{
		SendDebugConsoleCommand("disabledebugropetimer");
	}

	public SetFavoriteHeroResponse GetSetFavoriteHeroResponse()
	{
		PegasusUtil.SetFavoriteHeroResponse setFavoriteHeroResponse = m_connectApi.GetSetFavoriteHeroResponse();
		if (setFavoriteHeroResponse == null)
		{
			return null;
		}
		SetFavoriteHeroResponse setFavoriteHeroResponse2 = new SetFavoriteHeroResponse();
		setFavoriteHeroResponse2.Success = setFavoriteHeroResponse.Success;
		if (setFavoriteHeroResponse.HasFavoriteHero)
		{
			if (!EnumUtils.TryCast<TAG_CLASS>(setFavoriteHeroResponse.FavoriteHero.ClassId, out setFavoriteHeroResponse2.HeroClass))
			{
				Debug.LogWarning($"Network.GetSetFavoriteHeroResponse() invalid class {setFavoriteHeroResponse.FavoriteHero.ClassId}");
			}
			if (!EnumUtils.TryCast<TAG_PREMIUM>(setFavoriteHeroResponse.FavoriteHero.Hero.Premium, out var outVal))
			{
				Debug.LogWarning($"Network.GetSetFavoriteHeroResponse() invalid heroPremium {setFavoriteHeroResponse.FavoriteHero.Hero.Premium}");
			}
			setFavoriteHeroResponse2.Hero = new NetCache.CardDefinition
			{
				Name = GameUtils.TranslateDbIdToCardId(setFavoriteHeroResponse.FavoriteHero.Hero.Asset),
				Premium = outVal
			};
		}
		return setFavoriteHeroResponse2;
	}

	public void RequestRecruitAFriendUrl()
	{
		m_connectApi.RequestRecruitAFriendUrl(GetPlatformBuilder());
	}

	public RecruitAFriendURLResponse GetRecruitAFriendUrlResponse()
	{
		return m_connectApi.GetRecruitAFriendUrlResponse();
	}

	public void RequestRecruitAFriendData()
	{
		m_connectApi.RequestRecruitAFriendData();
	}

	public RecruitAFriendDataResponse GetRecruitAFriendDataResponse()
	{
		return m_connectApi.GetRecruitAFriendDataResponse();
	}

	public void RequestProcessRecruitAFriend()
	{
		m_connectApi.RequestProcessRecruitAFriend();
	}

	public ProcessRecruitAFriendResponse GetProcessRecruitAFriendResponse()
	{
		return m_connectApi.GetProcessRecruitAFriendResponse();
	}

	public PurchaseCanceledResponse GetPurchaseCanceledResponse()
	{
		CancelPurchaseResponse cancelPurchaseResponse = m_connectApi.GetCancelPurchaseResponse();
		if (cancelPurchaseResponse == null)
		{
			return null;
		}
		PurchaseCanceledResponse purchaseCanceledResponse = new PurchaseCanceledResponse
		{
			TransactionID = (cancelPurchaseResponse.HasTransactionId ? cancelPurchaseResponse.TransactionId : 0),
			PMTProductID = (cancelPurchaseResponse.HasPmtProductId ? new long?(cancelPurchaseResponse.PmtProductId) : null),
			CurrencyCode = cancelPurchaseResponse.CurrencyCode
		};
		switch (cancelPurchaseResponse.Result)
		{
		case CancelPurchaseResponse.CancelResult.CR_SUCCESS:
			purchaseCanceledResponse.Result = PurchaseCanceledResponse.CancelResult.SUCCESS;
			break;
		case CancelPurchaseResponse.CancelResult.CR_NOT_ALLOWED:
			purchaseCanceledResponse.Result = PurchaseCanceledResponse.CancelResult.NOT_ALLOWED;
			break;
		case CancelPurchaseResponse.CancelResult.CR_NOTHING_TO_CANCEL:
			purchaseCanceledResponse.Result = PurchaseCanceledResponse.CancelResult.NOTHING_TO_CANCEL;
			break;
		}
		return purchaseCanceledResponse;
	}

	public BattlePayStatus GetBattlePayStatusResponse()
	{
		BattlePayStatusResponse battlePayStatusResponse = m_connectApi.GetBattlePayStatusResponse();
		if (battlePayStatusResponse == null)
		{
			return null;
		}
		BattlePayStatus battlePayStatus = new BattlePayStatus
		{
			State = (BattlePayStatus.PurchaseState)battlePayStatusResponse.Status,
			BattlePayAvailable = battlePayStatusResponse.BattlePayAvailable,
			CurrencyCode = battlePayStatusResponse.CurrencyCode
		};
		if (battlePayStatusResponse.HasTransactionId)
		{
			battlePayStatus.TransactionID = battlePayStatusResponse.TransactionId;
		}
		if (battlePayStatusResponse.HasPmtProductId)
		{
			battlePayStatus.PMTProductID = battlePayStatusResponse.PmtProductId;
		}
		if (battlePayStatusResponse.HasPurchaseError)
		{
			battlePayStatus.PurchaseError = ConvertPurchaseError(battlePayStatusResponse.PurchaseError);
		}
		if (battlePayStatusResponse.HasThirdPartyId)
		{
			battlePayStatus.ThirdPartyID = battlePayStatusResponse.ThirdPartyId;
		}
		if (battlePayStatusResponse.HasProvider)
		{
			battlePayStatus.Provider = battlePayStatusResponse.Provider;
		}
		return battlePayStatus;
	}

	private PurchaseErrorInfo ConvertPurchaseError(PurchaseError purchaseError)
	{
		PurchaseErrorInfo purchaseErrorInfo = new PurchaseErrorInfo
		{
			Error = (PurchaseErrorInfo.ErrorType)purchaseError.Error_
		};
		if (purchaseError.HasPurchaseInProgress)
		{
			purchaseErrorInfo.PurchaseInProgressProductID = purchaseError.PurchaseInProgress;
		}
		if (purchaseError.HasErrorCode)
		{
			purchaseErrorInfo.ErrorCode = purchaseError.ErrorCode;
		}
		return purchaseErrorInfo;
	}

	private static Dictionary<string, string> ConvertProductAttributesFromProtobuf(List<ProductAttribute> protoAttributes)
	{
		if (protoAttributes == null || protoAttributes.Count == 0)
		{
			return null;
		}
		Dictionary<string, string> dictionary = new Dictionary<string, string>(protoAttributes.Count);
		foreach (ProductAttribute protoAttribute in protoAttributes)
		{
			if (protoAttribute.HasName && protoAttribute.HasValue && !string.IsNullOrEmpty(protoAttribute.Name) && !string.IsNullOrEmpty(protoAttribute.Value))
			{
				dictionary[protoAttribute.Name.ToLowerInvariant()] = protoAttribute.Value.ToLowerInvariant();
			}
		}
		return dictionary;
	}

	public BattlePayConfig GetBattlePayConfigResponse()
	{
		BattlePayConfigResponse battlePayConfigResponse = m_connectApi.GetBattlePayConfigResponse();
		if (battlePayConfigResponse == null)
		{
			return null;
		}
		BattlePayConfig battlePayConfig = new BattlePayConfig
		{
			Available = (!battlePayConfigResponse.HasUnavailable || !battlePayConfigResponse.Unavailable),
			SecondsBeforeAutoCancel = (battlePayConfigResponse.HasSecsBeforeAutoCancel ? battlePayConfigResponse.SecsBeforeAutoCancel : StoreManager.DEFAULT_SECONDS_BEFORE_AUTO_CANCEL)
		};
		if (battlePayConfigResponse.HasCheckoutKrOnestoreKey)
		{
			battlePayConfig.CheckoutKrOnestoreKey = battlePayConfigResponse.CheckoutKrOnestoreKey;
		}
		for (int i = 0; i < battlePayConfigResponse.Currencies.Count; i++)
		{
			Currency currency = new Currency(battlePayConfigResponse.Currencies[i]);
			battlePayConfig.Currencies.Add(currency);
			if (currency.Code == battlePayConfigResponse.DefaultCurrencyCode)
			{
				battlePayConfig.Currency = currency;
			}
		}
		for (int j = 0; j < battlePayConfigResponse.Bundles.Count; j++)
		{
			PegasusUtil.Bundle bundle = battlePayConfigResponse.Bundles[j];
			Bundle bundle2 = new Bundle
			{
				AppleID = (bundle.HasAppleId ? bundle.AppleId : string.Empty),
				GooglePlayID = (bundle.HasGooglePlayId ? bundle.GooglePlayId : string.Empty),
				AmazonID = (bundle.HasAmazonId ? bundle.AmazonId : string.Empty),
				OneStoreID = (bundle.HasKronestoreId ? bundle.KronestoreId : string.Empty),
				ExclusiveProviders = bundle.ExclusiveProviders,
				IsPrePurchase = bundle.IsPrePurchase,
				PMTProductID = bundle.PmtProductId,
				DisplayName = (bundle.HasDisplayName ? DbfUtils.ConvertFromProtobuf(bundle.DisplayName) : null),
				DisplayDescription = (bundle.HasDisplayDesc ? DbfUtils.ConvertFromProtobuf(bundle.DisplayDesc) : null),
				Attributes = ConvertProductAttributesFromProtobuf(bundle.Attributes),
				SaleIds = bundle.SaleIds.ToList(),
				VisibleOnSalePeriodOnly = bundle.VisibleOnSalePeriodOnly
			};
			if (bundle2.Attributes != null && bundle2.Attributes.TryGetValue("tags", out var value))
			{
				IEnumerable<string> enumerable = CatalogUtils.ParseTagsString(value);
				if (enumerable != null && enumerable.Contains("prepurchase"))
				{
					bundle2.IsPrePurchase = true;
				}
			}
			if (bundle.HasCost && bundle.Cost != 0)
			{
				bundle2.Cost = bundle.Cost;
				bundle2.CostDisplay = (double)bundle.Cost / (double)battlePayConfig.Currency.RoundingOffset();
			}
			if (bundle.HasGoldCost && bundle.GoldCost > 0)
			{
				bundle2.GtappGoldCost = bundle.GoldCost;
			}
			if (bundle.VirtualCurrencyCost != null)
			{
				bundle2.VirtualCurrencyCost = bundle.VirtualCurrencyCost.Cost;
				bundle2.VirtualCurrencyCode = bundle.VirtualCurrencyCost.CurrencyCode;
			}
			if (bundle.HasProductEventName)
			{
				bundle2.ProductEvent = bundle.ProductEventName;
			}
			for (int k = 0; k < bundle.Items.Count; k++)
			{
				PegasusUtil.BundleItem bundleItem = bundle.Items[k];
				BundleItem bundleItem2 = new BundleItem
				{
					ItemType = bundleItem.ProductType,
					ProductData = bundleItem.Data,
					Quantity = bundleItem.Quantity,
					BaseQuantity = bundleItem.BaseQuantity
				};
				foreach (ProductAttribute attribute in bundleItem.Attributes)
				{
					bundleItem2.Attributes[attribute.Name] = attribute.Value;
				}
				bundle2.Items.Add(bundleItem2);
			}
			battlePayConfig.Bundles.Add(bundle2);
		}
		for (int l = 0; l < battlePayConfigResponse.GoldCostBoosters.Count; l++)
		{
			PegasusUtil.GoldCostBooster goldCostBooster = battlePayConfigResponse.GoldCostBoosters[l];
			GoldCostBooster goldCostBooster2 = new GoldCostBooster
			{
				ID = goldCostBooster.PackType
			};
			if (goldCostBooster.Cost > 0)
			{
				goldCostBooster2.Cost = goldCostBooster.Cost;
			}
			else
			{
				goldCostBooster2.Cost = null;
			}
			if (goldCostBooster.HasBuyWithGoldEventName)
			{
				goldCostBooster2.BuyWithGoldEvent = SpecialEventManager.GetEventType(goldCostBooster.BuyWithGoldEventName);
			}
			battlePayConfig.GoldCostBoosters.Add(goldCostBooster2);
		}
		if (battlePayConfigResponse.HasGoldCostArena && battlePayConfigResponse.GoldCostArena > 0)
		{
			battlePayConfig.GoldCostArena = battlePayConfigResponse.GoldCostArena;
		}
		else
		{
			battlePayConfig.GoldCostArena = null;
		}
		if (battlePayConfigResponse.HasCheckoutOauthClientId && !string.IsNullOrEmpty(battlePayConfigResponse.CheckoutOauthClientId))
		{
			battlePayConfig.CommerceClientID = battlePayConfigResponse.CheckoutOauthClientId;
		}
		if (battlePayConfigResponse.HasPersonalizedShopPageId && !string.IsNullOrEmpty(battlePayConfigResponse.PersonalizedShopPageId))
		{
			battlePayConfig.PersonalizedShopPageID = battlePayConfigResponse.PersonalizedShopPageId;
		}
		if (battlePayConfigResponse.LocaleMap != null)
		{
			foreach (LocaleMapEntry item in battlePayConfigResponse.LocaleMap)
			{
				battlePayConfig.CatalogLocaleToGameLocale.Add(item.CatalogLocaleId, (Locale)item.GameLocaleId);
			}
		}
		foreach (Locale value2 in Enum.GetValues(typeof(Locale)))
		{
			if (value2 != Locale.UNKNOWN && !battlePayConfig.CatalogLocaleToGameLocale.ContainsValue(value2))
			{
				Log.Store.PrintError("BattlePayConfig includes no catalog locale ID mapping for {0}", value2.ToString());
			}
		}
		battlePayConfig.SaleList = CatalogDeserializer.DeserializeShopSaleList(battlePayConfigResponse.SaleList);
		battlePayConfig.IgnoreProductTiming = battlePayConfigResponse.IgnoreProductTiming;
		return battlePayConfig;
	}

	public void PurchaseViaGold(int quantity, ProductType productItemType, int data)
	{
		if (!IsLoggedIn())
		{
			Log.All.PrintError("Client attempted to make a gold purchase while offline!");
		}
		else
		{
			m_connectApi.PurchaseViaGold(quantity, productItemType, data);
		}
	}

	public void GetPurchaseMethod(long? pmtProductId, int quantity, Currency currency)
	{
		m_connectApi.RequestPurchaseMethod(pmtProductId, quantity, currency.toProto(), SystemInfo.deviceUniqueIdentifier, GetPlatformBuilder());
	}

	public void ConfirmPurchase()
	{
		m_connectApi.ConfirmPurchase();
	}

	public void BeginThirdPartyPurchase(BattlePayProvider provider, string pmtLegacyProductId, int quantity)
	{
		m_connectApi.BeginThirdPartyPurchase(SystemInfo.deviceUniqueIdentifier, provider, pmtLegacyProductId, quantity);
	}

	public void BeginThirdPartyPurchaseWithReceipt(BattlePayProvider provider, string pmtLegacyProductId, int quantity, string thirdPartyId, string base64receipt, string thirdPartyUserId = "")
	{
		m_connectApi.BeginThirdPartyPurchaseWithReceipt(SystemInfo.deviceUniqueIdentifier, provider, pmtLegacyProductId, quantity, thirdPartyId, base64receipt, string.IsNullOrEmpty(thirdPartyUserId) ? null : thirdPartyUserId);
	}

	public void SubmitThirdPartyReceipt(long bpayId, BattlePayProvider provider, string pmtLegacyProductId, int quantity, string thirdPartyId, string base64receipt, string thirdPartyUserId = "")
	{
		m_connectApi.SubmitThirdPartyPurchaseReceipt(bpayId, provider, pmtLegacyProductId, SystemInfo.deviceUniqueIdentifier, quantity, thirdPartyId, base64receipt, thirdPartyUserId);
	}

	public void GetThirdPartyPurchaseStatus(string transactionId)
	{
		m_connectApi.GetThirdPartyPurchaseStatus(transactionId);
	}

	public void CancelBlizzardPurchase(bool isAutoCanceled, CancelPurchase.CancelReason? reason, string error)
	{
		m_connectApi.AbortBlizzardPurchase(SystemInfo.deviceUniqueIdentifier, isAutoCanceled, reason, error);
	}

	public void CancelThirdPartyPurchase(CancelPurchase.CancelReason reason, string error)
	{
		m_connectApi.AbortThirdPartyPurchase(SystemInfo.deviceUniqueIdentifier, reason, error);
	}

	public PurchaseMethod GetPurchaseMethodResponse()
	{
		PegasusUtil.PurchaseMethod purchaseMethodResponse = m_connectApi.GetPurchaseMethodResponse();
		if (purchaseMethodResponse == null)
		{
			return null;
		}
		PurchaseMethod purchaseMethod = new PurchaseMethod();
		if (purchaseMethodResponse.HasTransactionId)
		{
			purchaseMethod.TransactionID = purchaseMethodResponse.TransactionId;
		}
		if (purchaseMethodResponse.HasPmtProductId)
		{
			purchaseMethod.PMTProductID = purchaseMethodResponse.PmtProductId;
		}
		if (purchaseMethodResponse.HasQuantity)
		{
			purchaseMethod.Quantity = purchaseMethodResponse.Quantity;
		}
		purchaseMethod.CurrencyCode = purchaseMethodResponse.CurrencyCode;
		if (purchaseMethodResponse.HasWalletName)
		{
			purchaseMethod.WalletName = purchaseMethodResponse.WalletName;
		}
		if (purchaseMethodResponse.HasUseEbalance)
		{
			purchaseMethod.UseEBalance = purchaseMethodResponse.UseEbalance;
		}
		purchaseMethod.IsZeroCostLicense = purchaseMethodResponse.HasIsZeroCostLicense && purchaseMethodResponse.IsZeroCostLicense;
		if (purchaseMethodResponse.HasChallengeId)
		{
			purchaseMethod.ChallengeID = purchaseMethodResponse.ChallengeId;
		}
		if (purchaseMethodResponse.HasChallengeUrl)
		{
			purchaseMethod.ChallengeURL = purchaseMethodResponse.ChallengeUrl;
		}
		if (purchaseMethodResponse.HasError)
		{
			purchaseMethod.PurchaseError = ConvertPurchaseError(purchaseMethodResponse.Error);
		}
		return purchaseMethod;
	}

	public PurchaseResponse GetPurchaseResponse()
	{
		PegasusUtil.PurchaseResponse purchaseResponse = m_connectApi.GetPurchaseResponse();
		if (purchaseResponse == null)
		{
			return null;
		}
		return new PurchaseResponse
		{
			PurchaseError = ConvertPurchaseError(purchaseResponse.Error),
			TransactionID = (purchaseResponse.HasTransactionId ? purchaseResponse.TransactionId : 0),
			PMTProductID = (purchaseResponse.HasPmtProductId ? new long?(purchaseResponse.PmtProductId) : null),
			ThirdPartyID = (purchaseResponse.HasThirdPartyId ? purchaseResponse.ThirdPartyId : string.Empty),
			CurrencyCode = purchaseResponse.CurrencyCode
		};
	}

	public PurchaseViaGoldResponse GetPurchaseWithGoldResponse()
	{
		PurchaseWithGoldResponse purchaseWithGoldResponse = m_connectApi.GetPurchaseWithGoldResponse();
		if (purchaseWithGoldResponse == null)
		{
			return null;
		}
		PurchaseViaGoldResponse purchaseViaGoldResponse = new PurchaseViaGoldResponse
		{
			Error = (PurchaseViaGoldResponse.ErrorType)purchaseWithGoldResponse.Result
		};
		if (purchaseWithGoldResponse.HasGoldUsed)
		{
			purchaseViaGoldResponse.GoldUsed = purchaseWithGoldResponse.GoldUsed;
		}
		return purchaseViaGoldResponse;
	}

	public ThirdPartyPurchaseStatusResponse GetThirdPartyPurchaseStatusResponse()
	{
		PegasusUtil.ThirdPartyPurchaseStatusResponse thirdPartyPurchaseStatusResponse = m_connectApi.GetThirdPartyPurchaseStatusResponse();
		if (thirdPartyPurchaseStatusResponse == null)
		{
			return null;
		}
		return new ThirdPartyPurchaseStatusResponse
		{
			ThirdPartyID = thirdPartyPurchaseStatusResponse.ThirdPartyId,
			Status = (ThirdPartyPurchaseStatusResponse.PurchaseStatus)thirdPartyPurchaseStatusResponse.Status_
		};
	}

	public CardBackResponse GetCardBackResponse()
	{
		SetFavoriteCardBackResponse setFavoriteCardBackResponse = m_connectApi.GetSetFavoriteCardBackResponse();
		if (setFavoriteCardBackResponse == null)
		{
			return null;
		}
		return new CardBackResponse
		{
			Success = setFavoriteCardBackResponse.Success,
			CardBack = setFavoriteCardBackResponse.CardBack
		};
	}

	public void SetFavoriteCardBack(int cardBack)
	{
		NetCache.NetCacheCardBacks netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCardBacks>();
		if (netObject != null && cardBack != netObject.FavoriteCardBack)
		{
			m_connectApi.SetFavoriteCardBack(cardBack);
		}
		if (!IsLoggedIn())
		{
			OfflineDataCache.SetFavoriteCardBack(cardBack);
			if (netObject != null)
			{
				NetCache.Get().ProcessNewFavoriteCardBack(cardBack);
			}
		}
	}

	public NetCache.NetCacheCardBacks GetCardBacks()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new NetCache.NetCacheCardBacks();
		}
		CardBacks cardBacksPacket = GetCardBacksPacket();
		if (cardBacksPacket == null)
		{
			return null;
		}
		NetCache.NetCacheCardBacks netCacheCardBacks = new NetCache.NetCacheCardBacks();
		netCacheCardBacks.FavoriteCardBack = cardBacksPacket.FavoriteCardBack;
		for (int i = 0; i < cardBacksPacket.CardBacks_.Count; i++)
		{
			int item = cardBacksPacket.CardBacks_[i];
			netCacheCardBacks.CardBacks.Add(item);
		}
		return netCacheCardBacks;
	}

	public CardBacks GetCardBacksPacket()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return null;
		}
		return m_connectApi.GetCardBacks();
	}

	public CoinResponse GetCoinResponse()
	{
		SetFavoriteCoinResponse setFavoriteCoinResponse = m_connectApi.GetSetFavoriteCoinResponse();
		if (setFavoriteCoinResponse == null)
		{
			return null;
		}
		return new CoinResponse
		{
			Success = setFavoriteCoinResponse.Success,
			Coin = setFavoriteCoinResponse.CoinId
		};
	}

	public void SetFavoriteCoin(int coin)
	{
		NetCache.NetCacheCoins netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCoins>();
		if (netObject != null && coin != netObject.FavoriteCoin)
		{
			m_connectApi.SetFavoriteCoin(coin);
		}
		if (!IsLoggedIn())
		{
			OfflineDataCache.SetFavoriteCoin(coin);
			if (netObject != null)
			{
				NetCache.Get().ProcessNewFavoriteCoin(coin);
			}
		}
	}

	public NetCache.NetCacheCoins GetCoins()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new NetCache.NetCacheCoins();
		}
		Coins coinsPacket = GetCoinsPacket();
		if (coinsPacket == null)
		{
			return null;
		}
		NetCache.NetCacheCoins netCacheCoins = new NetCache.NetCacheCoins();
		netCacheCoins.FavoriteCoin = coinsPacket.FavoriteCoin;
		for (int i = 0; i < coinsPacket.Coins_.Count; i++)
		{
			int item = coinsPacket.Coins_[i];
			netCacheCoins.Coins.Add(item);
		}
		return netCacheCoins;
	}

	public Coins GetCoinsPacket()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return null;
		}
		return m_connectApi.GetCoins();
	}

	public CoinUpdate GetCoinUpdate()
	{
		return m_connectApi.GetCoinUpdate();
	}

	public CardValues GetCardValues()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return null;
		}
		return m_connectApi.GetCardValues();
	}

	public InitialClientState GetInitialClientState()
	{
		if (!ShouldBeConnectedToAurora())
		{
			InitialClientState initialClientState = new InitialClientState();
			initialClientState.HasClientOptions = true;
			initialClientState.ClientOptions = new ClientOptions();
			initialClientState.HasCollection = true;
			initialClientState.Collection = new Collection();
			initialClientState.HasAchievements = true;
			initialClientState.Achievements = new Achieves();
			initialClientState.HasNotices = true;
			initialClientState.Notices = new PegasusUtil.ProfileNotices();
			initialClientState.HasGameCurrencyStates = true;
			initialClientState.GameCurrencyStates = new GameCurrencyStates();
			initialClientState.GameCurrencyStates.HasCurrencyVersion = true;
			initialClientState.GameCurrencyStates.CurrencyVersion = 0L;
			initialClientState.GameCurrencyStates.HasArcaneDustBalance = true;
			initialClientState.GameCurrencyStates.HasCappedGoldBalance = true;
			initialClientState.GameCurrencyStates.HasBonusGoldBalance = true;
			initialClientState.HasBoosters = true;
			initialClientState.Boosters = new Boosters();
			if (initialClientState.Decks == null)
			{
				initialClientState.Decks = new List<DeckInfo>();
			}
			return initialClientState;
		}
		return m_connectApi.GetInitialClientState();
	}

	public void OpenBooster(int id)
	{
		Log.Net.Print("Network.OpenBooster");
		NetCache.NetCacheBoosters netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
		if (netObject != null)
		{
			netObject.GetBoosterStack(id).LocallyPreConsumedCount++;
		}
		long fsgId = (FiresideGatheringManager.Get().IsCheckedIn ? FiresideGatheringManager.Get().CurrentFsgId : 0);
		m_connectApi.OpenBooster(id, fsgId);
	}

	public void CreateDeck(DeckType deckType, string name, int heroDatabaseAssetID, TAG_PREMIUM heroPremium, PegasusShared.FormatType formatType, long sortOrder, DeckSourceType sourceType, out int? requestId, string pastedDeckHash = null, int brawlLibraryItemId = 0)
	{
		if (!IsLoggedIn())
		{
			requestId = null;
			return;
		}
		requestId = GetNextCreateDeckRequestId();
		long? fsgId = (FiresideGatheringManager.Get().IsCheckedIn ? new long?(FiresideGatheringManager.Get().CurrentFsgId) : null);
		Log.Net.Print($"Network.CreateDeck hero={heroDatabaseAssetID},premium={heroPremium}");
		m_connectApi.CreateDeck(deckType, name, heroDatabaseAssetID, heroPremium, formatType, sortOrder, sourceType, pastedDeckHash, fsgId, FiresideGatheringManager.Get().CurrentFsgSharedSecretKey, brawlLibraryItemId, requestId);
	}

	private int GetNextCreateDeckRequestId()
	{
		return ++m_state.CurrentCreateDeckRequestId;
	}

	public void RenameDeck(long deck, string name)
	{
		if (IsLoggedIn())
		{
			Log.Net.Print($"Network.RenameDeck {deck}");
			CollectionManager.Get().AddPendingDeckRename(deck, name);
			m_connectApi.RenameDeck(deck, name);
		}
		else
		{
			OfflineDataCache.RenameDeck(deck, name);
		}
	}

	public void SendDeckData(long deck, List<CardUserData> cards, int newHeroAssetID, TAG_PREMIUM newHeroCardPremium, int heroOverrideAssetID, TAG_PREMIUM heroOverridePremium, int newCardBackID, PegasusShared.FormatType formatType, long sortOrder, string pastedDeckHash = null)
	{
		DeckSetData deckSetData = new DeckSetData
		{
			Deck = deck,
			FormatType = formatType,
			TaggedStandard = (formatType == PegasusShared.FormatType.FT_STANDARD),
			SortOrder = sortOrder
		};
		for (int i = 0; i < cards.Count; i++)
		{
			CardUserData cardUserData = cards[i];
			DeckCardData deckCardData = new DeckCardData();
			PegasusShared.CardDef cardDef = new PegasusShared.CardDef();
			cardDef.Asset = cardUserData.DbId;
			if (cardUserData.Premium != 0)
			{
				cardDef.Premium = (int)cardUserData.Premium;
			}
			deckCardData.Def = cardDef;
			deckCardData.Qty = cardUserData.Count;
			deckSetData.Cards.Add(deckCardData);
		}
		if (-1 != newHeroAssetID)
		{
			PegasusShared.CardDef cardDef2 = new PegasusShared.CardDef();
			cardDef2.Asset = newHeroAssetID;
			cardDef2.Premium = (int)newHeroCardPremium;
			deckSetData.Hero = cardDef2;
		}
		if (-1 != heroOverrideAssetID)
		{
			PegasusShared.CardDef cardDef3 = new PegasusShared.CardDef();
			cardDef3.Asset = heroOverrideAssetID;
			cardDef3.Premium = (int)heroOverridePremium;
			deckSetData.UiHeroOverride = cardDef3;
		}
		if (-1 != newCardBackID)
		{
			deckSetData.CardBack = newCardBackID;
		}
		if (!string.IsNullOrEmpty(pastedDeckHash))
		{
			deckSetData.PastedDeckHash = pastedDeckHash;
		}
		long? num = (FiresideGatheringManager.Get().IsCheckedIn ? new long?(FiresideGatheringManager.Get().CurrentFsgId) : null);
		if (num.HasValue)
		{
			deckSetData.FsgId = num.Value;
		}
		deckSetData.FsgSharedSecretKey = FiresideGatheringManager.Get().CurrentFsgSharedSecretKey;
		if (IsLoggedIn())
		{
			m_connectApi.SendDeckData(deckSetData);
			OfflineDataCache.ApplyDeckSetDataToOriginalDeck(deckSetData);
			CollectionManager.Get().AddPendingDeckEdit(deck);
		}
		OfflineDataCache.ApplyDeckSetDataLocally(deckSetData);
	}

	public void DeleteDeck(long deck, DeckType deckType)
	{
		OfflineDataCache.DeleteDeck(deck);
		if (IsLoggedIn())
		{
			Log.Net.Print($"Network.DeleteDeck {deck}");
			if (deck <= 0)
			{
				Log.Offline.PrintError("Network.DeleteDeck Error: Attempting to delete fake deck ID={0} on server.", deck);
			}
			else
			{
				m_connectApi.DeleteDeck(deck, deckType);
			}
		}
	}

	public void RequestDeckContents(params long[] deckIds)
	{
		if (IsLoggedIn())
		{
			Log.Net.Print("Network.GetDeckContents {0}", string.Join(", ", deckIds.Select((long id) => id.ToString()).ToArray()));
			m_connectApi.RequestDeckContents(deckIds);
		}
	}

	public void SetDeckTemplateSource(long deck, int templateID)
	{
		if (IsLoggedIn() && deck >= 0)
		{
			Log.Net.Print($"Network.SendDeckTemplateSource {deck}, {templateID}");
			m_connectApi.SendDeckTemplateSource(deck, templateID);
		}
	}

	public GetDeckContentsResponse GetDeckContentsResponse()
	{
		GetDeckContentsResponse getDeckContentsResponse;
		if (IsLoggedIn())
		{
			getDeckContentsResponse = m_connectApi.GetDeckContentsResponse();
		}
		else
		{
			getDeckContentsResponse = new GetDeckContentsResponse
			{
				Decks = new List<PegasusUtil.DeckContents>()
			};
			getDeckContentsResponse.Decks = OfflineDataCache.GetLocalDeckContentsFromCache();
		}
		return getDeckContentsResponse;
	}

	public FreeDeckChoiceResponse GetFreeDeckChoiceResponse()
	{
		if (IsLoggedIn())
		{
			return m_connectApi.GetFreeDeckChoiceResponse();
		}
		return new FreeDeckChoiceResponse
		{
			Success = false
		};
	}

	public static SmartDeckRequest GenerateSmartDeckRequestMessage(CollectionDeck deck)
	{
		List<SmartDeckCardData> list = new List<SmartDeckCardData>();
		Dictionary<long, SmartDeckCardData> dictionary = new Dictionary<long, SmartDeckCardData>();
		foreach (CollectionDeckSlot slot in deck.GetSlots())
		{
			if (slot.Owned)
			{
				int num = GameUtils.TranslateCardIdToDbId(slot.CardID);
				if (!dictionary.ContainsKey(num))
				{
					dictionary.Add(num, new SmartDeckCardData
					{
						Asset = num
					});
				}
				dictionary[num].QtyGolden += slot.GetCount(TAG_PREMIUM.GOLDEN);
				dictionary[num].QtyNormal += slot.GetCount(TAG_PREMIUM.NORMAL);
			}
		}
		foreach (long key in dictionary.Keys)
		{
			list.Add(dictionary[key]);
		}
		HSCachedDeckCompletionRequest requestMessage = new HSCachedDeckCompletionRequest
		{
			HeroClass = (int)deck.GetClass(),
			InsertedCard = list,
			DeckId = deck.ID,
			FormatType = deck.FormatType
		};
		return new SmartDeckRequest
		{
			RequestMessage = requestMessage
		};
	}

	public void RequestSmartDeckCompletion(CollectionDeck deck)
	{
		SmartDeckRequest packet = GenerateSmartDeckRequestMessage(deck);
		m_connectApi.SendSmartDeckRequest(packet);
	}

	public void RequestOfflineDeckContents()
	{
		m_connectApi.SendOfflineDeckContentsRequest();
	}

	public void RequestBaconRatingInfo()
	{
		m_connectApi.RequestBaconRatingInfo();
	}

	public ResponseWithRequest<BattlegroundsRatingInfoResponse, BattlegroundsRatingInfoRequest> BattlegroundsRatingInfoResponse()
	{
		return m_connectApi.BattlegroundsRatingInfoResponse();
	}

	public void RequestBattlegroundsPremiumStatus()
	{
		m_connectApi.RequestBattlegroundsPremiumStatus();
	}

	public ResponseWithRequest<BattlegroundsPremiumStatusResponse, BattlegroundsPremiumStatusRequest> GetBattlegroundsPremiumStatus()
	{
		return m_connectApi.GetBattlegroundsPremiumStatus();
	}

	public void SendPVPDRSessionStartRequest(bool paidEntry)
	{
		m_connectApi.SendPVPDRSessionStartRequest(paidEntry);
	}

	public PVPDRSessionStartResponse GetPVPDRSessionStartResponse()
	{
		return m_connectApi.GetPVPDRSessionStartResponse();
	}

	public void SendPVPDRSessionEndRequest()
	{
		m_connectApi.SendPVPDRSessionEndRequest();
	}

	public PVPDRSessionEndResponse GetPVPDRSessionEndResponse()
	{
		return m_connectApi.GetPVPDRSessionEndResponse();
	}

	public void SendPVPDRSessionInfoRequest()
	{
		m_connectApi.SendPVPDRSessionInfoRequest();
	}

	public PVPDRSessionInfoResponse GetPVPDRSessionInfoResponse()
	{
		return m_connectApi.GetPVPDRSessionInfoResponse();
	}

	public void SendPVPDRRetireRequest()
	{
		m_connectApi.SendPVPDRRetireRequest();
	}

	public PVPDRRetireResponse GetPVPDRRetireResponse()
	{
		return m_connectApi.GetPVPDRRetireResponse();
	}

	public void RequestPVPDRStatsInfo()
	{
		m_connectApi.RequestPVPDRStatsInfo();
	}

	public ResponseWithRequest<PVPDRStatsInfoResponse, PVPDRStatsInfoRequest> PVPDRStatsInfoResponse()
	{
		return m_connectApi.PVPDRStatsInfoResponse();
	}

	public List<NetCache.BoosterCard> OpenedBooster()
	{
		BoosterContent openedBooster = m_connectApi.GetOpenedBooster();
		if (openedBooster == null)
		{
			return null;
		}
		List<NetCache.BoosterCard> list = new List<NetCache.BoosterCard>();
		for (int i = 0; i < openedBooster.List.Count; i++)
		{
			BoosterCard boosterCard = openedBooster.List[i];
			NetCache.BoosterCard boosterCard2 = new NetCache.BoosterCard();
			boosterCard2.Def.Name = GameUtils.TranslateDbIdToCardId(boosterCard.CardDef.Asset);
			boosterCard2.Def.Premium = (TAG_PREMIUM)boosterCard.CardDef.Premium;
			boosterCard2.Date = TimeUtils.PegDateToFileTimeUtc(boosterCard.InsertDate);
			list.Add(boosterCard2);
		}
		if (openedBooster.HasCollectionVersion)
		{
			NetCache.Get().AddExpectedCollectionModification(openedBooster.CollectionVersion);
		}
		return list;
	}

	public DBAction GetDeckResponse()
	{
		return GetDbAction();
	}

	public DBAction GetDbAction()
	{
		PegasusUtil.DBAction dbAction = m_connectApi.GetDbAction();
		if (dbAction == null)
		{
			return null;
		}
		return new DBAction
		{
			Action = (DBAction.ActionType)dbAction.Action,
			Result = (DBAction.ResultType)dbAction.Result,
			MetaData = dbAction.MetaData
		};
	}

	public void ReconcileDeckContentsForChangedOfflineDecks(List<DeckInfo> remoteDecks, List<PegasusUtil.DeckContents> remoteContents, List<long> validDeckIds)
	{
		List<long> list = new List<long>();
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		foreach (DeckInfo remoteDeckInfo in remoteDecks)
		{
			if (validDeckIds.Exists((long item) => item == remoteDeckInfo.Id))
			{
				continue;
			}
			DeckInfo deckInfoFromDeckList = OfflineDataCache.GetDeckInfoFromDeckList(remoteDeckInfo.Id, offlineData.OriginalDeckList);
			DeckInfo deckInfoFromDeckList2 = OfflineDataCache.GetDeckInfoFromDeckList(remoteDeckInfo.Id, offlineData.LocalDeckList);
			if (deckInfoFromDeckList2 == null && deckInfoFromDeckList != null)
			{
				NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
				if (netObject != null && netObject.AllowOfflineClientDeckDeletion)
				{
					Get().DeleteDeck(remoteDeckInfo.Id, remoteDeckInfo.DeckType);
				}
			}
			else if (deckInfoFromDeckList2 == null && deckInfoFromDeckList == null)
			{
				list.Add(remoteDeckInfo.Id);
			}
			else if (deckInfoFromDeckList2 != null && deckInfoFromDeckList != null && remoteDeckInfo.LastModified != deckInfoFromDeckList2.LastModified)
			{
				if (remoteDeckInfo.LastModified != deckInfoFromDeckList.LastModified)
				{
					list.Add(remoteDeckInfo.Id);
				}
				else
				{
					list.Add(remoteDeckInfo.Id);
				}
			}
		}
		foreach (DeckInfo localDeck in offlineData.LocalDeckList)
		{
			if (!remoteDecks.Any((DeckInfo d) => d.Id == localDeck.Id) && offlineData.OriginalDeckList.Any((DeckInfo d) => d.Id == localDeck.Id))
			{
				CollectionManager.Get().OnDeckDeletedWhileOffline(localDeck.Id);
			}
		}
		if (list.Count > 0)
		{
			List<long> list2 = new List<long>();
			foreach (long deck in list)
			{
				m_state.DeckIdsWaitingToDiffAgainstOfflineCache.Add(deck);
				DeckInfo deckInfo = remoteDecks.Find((DeckInfo item) => item.Id == deck);
				bool num = deckInfo != null && deckInfo.DeckType == DeckType.PRECON_DECK;
				bool flag = remoteContents?.Exists((PegasusUtil.DeckContents item) => item.DeckId == deck) ?? false;
				if (!num && !flag)
				{
					list2.Add(deck);
				}
			}
			if (list2.Count > 0)
			{
				RequestDeckContents(list2.ToArray());
			}
		}
		RegisterNetHandler(DeckCreated.PacketID.ID, OnDeckCreatedResponse_SendOfflineDeckSetData);
		CreateDeckFromOfflineDeckCache(offlineData);
		if (remoteContents != null)
		{
			UpdateDecksFromContent(remoteContents);
		}
	}

	public NetCache.NetCacheDecks GetDeckHeaders()
	{
		NetCache.NetCacheDecks result = new NetCache.NetCacheDecks();
		if (!ShouldBeConnectedToAurora())
		{
			return result;
		}
		DeckList deckHeaders = m_connectApi.GetDeckHeaders();
		if (deckHeaders == null)
		{
			return null;
		}
		return GetDeckHeaders(deckHeaders.Decks);
	}

	public static NetCache.NetCacheDecks GetDeckHeaders(List<DeckInfo> deckHeaders)
	{
		NetCache.NetCacheDecks netCacheDecks = new NetCache.NetCacheDecks();
		if (deckHeaders == null)
		{
			return netCacheDecks;
		}
		for (int i = 0; i < deckHeaders.Count; i++)
		{
			netCacheDecks.Decks.Add(GetDeckHeaderFromDeckInfo(deckHeaders[i]));
		}
		return netCacheDecks;
	}

	private void OnDeckContentsResponse()
	{
		GetDeckContentsResponse deckContentsResponse = GetDeckContentsResponse();
		UpdateDecksFromContent(deckContentsResponse.Decks);
	}

	private void UpdateDecksFromContent(List<PegasusUtil.DeckContents> decksContents)
	{
		List<DeckSetData> deckSetDataToSend = new List<DeckSetData>();
		List<RenameDeck> deckRenameToSend = new List<RenameDeck>();
		List<DeckInfo> deckListFromNetCache = NetCache.Get().GetDeckListFromNetCache();
		foreach (PegasusUtil.DeckContents decksContent in decksContents)
		{
			if (m_state.DeckIdsWaitingToDiffAgainstOfflineCache.Contains(decksContent.DeckId))
			{
				m_state.DeckIdsWaitingToDiffAgainstOfflineCache.Remove(decksContent.DeckId);
				DiffRemoteDeckContentsAgainstOfflineDataCache(decksContent, deckListFromNetCache, ref deckSetDataToSend, ref deckRenameToSend);
			}
			else
			{
				OfflineDataCache.CacheLocalAndOriginalDeckContents(decksContent, decksContent);
			}
		}
		List<long> list = new List<long>();
		foreach (DeckSetData item in deckSetDataToSend)
		{
			m_connectApi.SendDeckData(item);
			list.Add(item.Deck);
		}
		CollectionManager.Get().RegisterDecksToRequestContentsAfterDeckSetDataResponse(list);
		foreach (RenameDeck item2 in deckRenameToSend)
		{
			m_connectApi.RenameDeck(item2.Deck, item2.Name);
		}
		OfflineDataCache.CacheLocalAndOriginalDeckList(deckListFromNetCache, deckListFromNetCache);
	}

	private void DiffRemoteDeckContentsAgainstOfflineDataCache(PegasusUtil.DeckContents remoteDeckContents, List<DeckInfo> currentNetCacheDeckList, ref List<DeckSetData> deckSetDataToSend, ref List<RenameDeck> deckRenameToSend)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		DeckInfo deckInfoFromDeckList = OfflineDataCache.GetDeckInfoFromDeckList(remoteDeckContents.DeckId, offlineData.LocalDeckList);
		PegasusUtil.DeckContents deckContentsFromDeckContentsList = OfflineDataCache.GetDeckContentsFromDeckContentsList(remoteDeckContents.DeckId, offlineData.LocalDeckContents);
		DeckInfo deckInfo = null;
		foreach (DeckInfo currentNetCacheDeck in currentNetCacheDeckList)
		{
			if (currentNetCacheDeck.Id == remoteDeckContents.DeckId)
			{
				deckInfo = currentNetCacheDeck;
				break;
			}
		}
		if (deckInfo == null)
		{
			return;
		}
		if (deckInfoFromDeckList != null && deckInfo.LastModified < deckInfoFromDeckList.LastModified)
		{
			if (OfflineDataCache.GenerateDeckSetDataFromDiff(remoteDeckContents.DeckId, deckInfoFromDeckList, deckInfo, deckContentsFromDeckContentsList, remoteDeckContents, out var deckSetData))
			{
				deckSetDataToSend.Add(deckSetData);
			}
			RenameDeck renameDeck = OfflineDataCache.GenerateRenameDeckFromDiff(remoteDeckContents.DeckId, deckInfoFromDeckList, deckInfo);
			if (renameDeck != null && renameDeck.Name != null)
			{
				deckRenameToSend.Add(renameDeck);
			}
		}
		else
		{
			OfflineDataCache.CacheLocalAndOriginalDeckContents(remoteDeckContents, remoteDeckContents);
		}
	}

	private void CreateDeckFromOfflineDeckCache(OfflineDataCache.OfflineData data)
	{
		int num = 0;
		List<long> fakeDeckIds = OfflineDataCache.GetFakeDeckIds(data);
		if (fakeDeckIds.Contains(FakeIdWaitingForResponse))
		{
			num = fakeDeckIds.IndexOf(Get().FakeIdWaitingForResponse);
			num++;
		}
		DeckInfo deckInfo = null;
		for (int i = num; i < fakeDeckIds.Count; i++)
		{
			if (deckInfo != null)
			{
				break;
			}
			FakeIdWaitingForResponse = fakeDeckIds[i];
			deckInfo = OfflineDataCache.GetDeckInfoFromDeckList(FakeIdWaitingForResponse, data.LocalDeckList);
		}
		if (deckInfo == null)
		{
			RemoveNetHandler(DeckCreated.PacketID.ID, OnDeckCreatedResponse_SendOfflineDeckSetData);
			OnFinishedCreatingDecksFromOfflineDataCache();
			return;
		}
		CreateDeck(deckInfo.DeckType, deckInfo.Name, deckInfo.Hero, (TAG_PREMIUM)deckInfo.HeroPremium, deckInfo.FormatType, deckInfo.SortOrder, deckInfo.SourceType, out var requestId, deckInfo.PastedDeckHash);
		if (requestId.HasValue)
		{
			m_state.InTransitOfflineCreateDeckRequestIds.Add(requestId.Value);
		}
	}

	private void OnFinishedCreatingDecksFromOfflineDataCache()
	{
		OfflineDataCache.ClearFakeDeckIds();
		OfflineDataCache.RemoveAllOldDecksContents();
		FakeIdWaitingForResponse = 0L;
	}

	private void OnDeckCreatedResponse_SendOfflineDeckSetData()
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		int? requestId;
		NetCache.DeckHeader createdDeck = GetCreatedDeck(out requestId);
		if (createdDeck != null && requestId.HasValue && m_state.InTransitOfflineCreateDeckRequestIds.Contains(requestId.Value))
		{
			m_state.InTransitOfflineCreateDeckRequestIds.Remove(requestId.Value);
			long fakeIdWaitingForResponse = Get().FakeIdWaitingForResponse;
			if (OfflineDataCache.GenerateDeckSetDataFromDiff(fakeIdWaitingForResponse, offlineData.LocalDeckList, offlineData.OriginalDeckList, offlineData.LocalDeckContents, offlineData.OriginalDeckContents, out var deckSetData))
			{
				deckSetData.Deck = createdDeck.ID;
				CollectionManager.Get().RegisterDecksToRequestContentsAfterDeckSetDataResponse(new List<long> { createdDeck.ID });
				m_connectApi.SendDeckData(deckSetData);
			}
			if (!OfflineDataCache.UpdateDeckWithNewId(fakeIdWaitingForResponse, createdDeck.ID))
			{
				Log.Offline.PrintDebug("OnDeckCreatedResponse_SendOfflineDeckSetData() - Deleting deck id={0} because it's fake id={1}  was not found in the offline cache.", createdDeck.ID, fakeIdWaitingForResponse);
				DeleteDeck(createdDeck.ID, createdDeck.Type);
			}
			else
			{
				CollectionManager.Get().UpdateDeckWithNewId(fakeIdWaitingForResponse, createdDeck.ID);
				CreateDeckFromOfflineDeckCache(offlineData);
			}
		}
	}

	public static bool DeckNeedsName(ulong deckValidityFlags)
	{
		return (deckValidityFlags & 0x200) != 0;
	}

	public static bool AreDeckFlagsWild(ulong deckValidityFlags)
	{
		return (deckValidityFlags & 0x80) == 0;
	}

	public static bool AreDeckFlagsLocked(ulong deckValidityFlags)
	{
		return (deckValidityFlags & 0x400) != 0;
	}

	public NetCache.DeckHeader GetCreatedDeck(out int? requestId)
	{
		DeckCreated deckCreated = m_connectApi.DeckCreated();
		if (deckCreated == null)
		{
			requestId = null;
			return null;
		}
		NetCache.DeckHeader deckHeaderFromDeckInfo = GetDeckHeaderFromDeckInfo(deckCreated.Info);
		requestId = deckCreated.RequestId;
		return deckHeaderFromDeckInfo;
	}

	public static NetCache.DeckHeader GetDeckHeaderFromDeckInfo(DeckInfo deck)
	{
		NetCache.DeckHeader deckHeader = new NetCache.DeckHeader
		{
			ID = deck.Id,
			Name = deck.Name,
			Hero = GameUtils.TranslateDbIdToCardId(deck.Hero),
			HeroPremium = (TAG_PREMIUM)deck.HeroPremium,
			HeroPower = GameUtils.GetHeroPowerCardIdFromHero(deck.Hero),
			Type = deck.DeckType,
			CardBack = deck.CardBack,
			CardBackOverridden = deck.CardBackOverride,
			HeroOverridden = deck.HeroOverride,
			SeasonId = deck.SeasonId,
			BrawlLibraryItemId = deck.BrawlLibraryItemId,
			NeedsName = DeckNeedsName(deck.Validity),
			SortOrder = (deck.HasSortOrder ? deck.SortOrder : deck.Id),
			FormatType = deck.FormatType,
			Locked = AreDeckFlagsLocked(deck.Validity),
			SourceType = (deck.HasSourceType ? deck.SourceType : DeckSourceType.DECK_SOURCE_TYPE_UNKNOWN),
			UIHeroOverride = ((deck.HasUiHeroOverride && deck.UiHeroOverride != 0) ? GameUtils.TranslateDbIdToCardId(deck.UiHeroOverride) : string.Empty),
			UIHeroOverridePremium = (deck.HasUiHeroOverridePremium ? ((TAG_PREMIUM)deck.UiHeroOverridePremium) : TAG_PREMIUM.NORMAL)
		};
		if (deck.HasCreateDate)
		{
			deckHeader.CreateDate = TimeUtils.UnixTimeStampToDateTimeUtc(deck.CreateDate);
		}
		else
		{
			deckHeader.CreateDate = null;
		}
		if (deck.HasLastModified)
		{
			deckHeader.LastModified = TimeUtils.UnixTimeStampToDateTimeUtc(deck.LastModified);
		}
		else
		{
			deckHeader.LastModified = null;
		}
		return deckHeader;
	}

	public static DeckInfo GetDeckInfoFromDeckHeader(NetCache.DeckHeader deckHeader)
	{
		if (deckHeader == null)
		{
			return null;
		}
		DeckInfo deckInfo = new DeckInfo
		{
			Id = deckHeader.ID,
			Name = deckHeader.Name,
			Hero = GameUtils.TranslateCardIdToDbId(deckHeader.Hero),
			HeroPremium = (int)deckHeader.HeroPremium,
			DeckType = deckHeader.Type,
			CardBack = deckHeader.CardBack,
			CardBackOverride = deckHeader.CardBackOverridden,
			HeroOverride = deckHeader.HeroOverridden,
			BrawlLibraryItemId = deckHeader.BrawlLibraryItemId,
			SortOrder = deckHeader.SortOrder,
			SourceType = deckHeader.SourceType
		};
		if (deckHeader.SeasonId != 0)
		{
			deckInfo.SeasonId = deckHeader.SeasonId;
		}
		if (!string.IsNullOrEmpty(deckHeader.UIHeroOverride))
		{
			deckInfo.UiHeroOverride = GameUtils.TranslateCardIdToDbId(deckHeader.UIHeroOverride);
			deckInfo.UiHeroOverridePremium = (int)deckHeader.UIHeroOverridePremium;
		}
		if (deckHeader.CreateDate.HasValue)
		{
			deckInfo.CreateDate = (long)TimeUtils.DateTimeToUnixTimeStamp(deckHeader.CreateDate.Value);
		}
		if (deckHeader.LastModified.HasValue)
		{
			deckInfo.LastModified = (long)TimeUtils.DateTimeToUnixTimeStamp(deckHeader.LastModified.Value);
		}
		return deckInfo;
	}

	public int GetDeckLimit()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return 0;
		}
		return m_connectApi.GetDeckLimit()?.DeckLimit ?? 0;
	}

	public long GetDeletedDeckID()
	{
		return m_connectApi.DeckDeleted()?.Deck ?? 0;
	}

	public DeckName GetRenamedDeck()
	{
		DeckRenamed deckRenamed = m_connectApi.DeckRenamed();
		if (deckRenamed == null)
		{
			return null;
		}
		return new DeckName
		{
			Deck = deckRenamed.Deck,
			Name = deckRenamed.Name
		};
	}

	public GenericResponse GetGenericResponse()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new GenericResponse
			{
				RequestId = 0,
				RequestSubId = 1,
				ResultCode = GenericResponse.Result.RESULT_OK
			};
		}
		PegasusUtil.GenericResponse genericResponse = m_connectApi.GetGenericResponse();
		if (genericResponse == null)
		{
			return null;
		}
		return new GenericResponse
		{
			ResultCode = (GenericResponse.Result)genericResponse.ResultCode,
			RequestId = genericResponse.RequestId,
			RequestSubId = (genericResponse.HasRequestSubId ? genericResponse.RequestSubId : 0),
			GenericData = genericResponse.GenericData
		};
	}

	public void RequestNetCacheObject(GetAccountInfo.Request request)
	{
		m_connectApi.RequestAccountInfoNetCacheObject(request);
	}

	public void RequestNetCacheObjectList(List<GetAccountInfo.Request> requestList, List<GenericRequest> genericRequests)
	{
		m_connectApi.RequestNetCacheObjectList(requestList, genericRequests);
	}

	public NetCache.NetCacheProfileProgress GetProfileProgress()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new NetCache.NetCacheProfileProgress
			{
				CampaignProgress = global::Options.Get().GetEnum<TutorialProgress>(Option.LOCAL_TUTORIAL_PROGRESS)
			};
		}
		ProfileProgress profileProgress = m_connectApi.GetProfileProgress();
		if (profileProgress == null)
		{
			return null;
		}
		return new NetCache.NetCacheProfileProgress
		{
			CampaignProgress = (TutorialProgress)profileProgress.Progress,
			BestForgeWins = profileProgress.BestForge,
			LastForgeDate = (profileProgress.HasLastForge ? TimeUtils.PegDateToFileTimeUtc(profileProgress.LastForge) : 0)
		};
	}

	public void SetProgress(long value)
	{
		m_connectApi.SetProgress(value);
	}

	public SetProgressResponse GetSetProgressResponse()
	{
		return m_connectApi.GetSetProgressResponse();
	}

	public void HandleProfileNotices(List<ProfileNotice> notices, ref List<NetCache.ProfileNotice> result)
	{
		for (int i = 0; i < notices.Count; i++)
		{
			ProfileNotice profileNotice = notices[i];
			NetCache.ProfileNotice profileNotice2 = null;
			if (profileNotice.HasMedal)
			{
				Map<ProfileNoticeMedal.MedalType, PegasusShared.FormatType> map = new Map<ProfileNoticeMedal.MedalType, PegasusShared.FormatType>
				{
					{
						ProfileNoticeMedal.MedalType.UNKNOWN_MEDAL,
						PegasusShared.FormatType.FT_UNKNOWN
					},
					{
						ProfileNoticeMedal.MedalType.WILD_MEDAL,
						PegasusShared.FormatType.FT_WILD
					},
					{
						ProfileNoticeMedal.MedalType.STANDARD_MEDAL,
						PegasusShared.FormatType.FT_STANDARD
					},
					{
						ProfileNoticeMedal.MedalType.CLASSIC_MEDAL,
						PegasusShared.FormatType.FT_CLASSIC
					}
				};
				PegasusShared.FormatType value = PegasusShared.FormatType.FT_UNKNOWN;
				if (profileNotice.Medal.HasMedalType_)
				{
					map.TryGetValue(profileNotice.Medal.MedalType_, out value);
				}
				NetCache.ProfileNoticeMedal profileNoticeMedal = new NetCache.ProfileNoticeMedal
				{
					LeagueId = profileNotice.Medal.LeagueId,
					StarLevel = profileNotice.Medal.StarLevel,
					LegendRank = (profileNotice.Medal.HasLegendRank ? profileNotice.Medal.LegendRank : 0),
					BestStarLevel = (profileNotice.Medal.HasBestStarLevel ? profileNotice.Medal.BestStarLevel : 0),
					FormatType = value,
					WasLimitedByBestEverStarLevel = (profileNotice.Medal.HasWasLimitedByBestEverStarLevel && profileNotice.Medal.WasLimitedByBestEverStarLevel)
				};
				if (profileNotice.Medal.HasChest)
				{
					profileNoticeMedal.Chest = ConvertRewardChest(profileNotice.Medal.Chest);
				}
				profileNotice2 = profileNoticeMedal;
			}
			else if (profileNotice.HasRewardBooster)
			{
				profileNotice2 = new NetCache.ProfileNoticeRewardBooster
				{
					Id = profileNotice.RewardBooster.BoosterType,
					Count = profileNotice.RewardBooster.BoosterCount
				};
			}
			else if (profileNotice.HasRewardCard)
			{
				profileNotice2 = new NetCache.ProfileNoticeRewardCard
				{
					CardID = GameUtils.TranslateDbIdToCardId(profileNotice.RewardCard.Card.Asset),
					Premium = (profileNotice.RewardCard.Card.HasPremium ? ((TAG_PREMIUM)profileNotice.RewardCard.Card.Premium) : TAG_PREMIUM.NORMAL),
					Quantity = ((!profileNotice.RewardCard.HasQuantity) ? 1 : profileNotice.RewardCard.Quantity)
				};
			}
			else if (profileNotice.HasPreconDeck)
			{
				profileNotice2 = new NetCache.ProfileNoticePreconDeck
				{
					DeckID = profileNotice.PreconDeck.Deck,
					HeroAsset = profileNotice.PreconDeck.Hero
				};
			}
			else if (profileNotice.HasRewardDust)
			{
				profileNotice2 = new NetCache.ProfileNoticeRewardDust
				{
					Amount = profileNotice.RewardDust.Amount
				};
			}
			else if (profileNotice.HasRewardMount)
			{
				profileNotice2 = new NetCache.ProfileNoticeRewardMount
				{
					MountID = profileNotice.RewardMount.MountId
				};
			}
			else if (profileNotice.HasRewardForge)
			{
				profileNotice2 = new NetCache.ProfileNoticeRewardForge
				{
					Quantity = profileNotice.RewardForge.Quantity
				};
			}
			else if (profileNotice.HasRewardCurrency)
			{
				profileNotice2 = new NetCache.ProfileNoticeRewardCurrency
				{
					Amount = profileNotice.RewardCurrency.Amount,
					CurrencyType = ((!profileNotice.HasRewardCurrency) ? PegasusShared.CurrencyType.CURRENCY_TYPE_GOLD : profileNotice.RewardCurrency.CurrencyType)
				};
			}
			else if (profileNotice.HasPurchase)
			{
				profileNotice2 = new NetCache.ProfileNoticePurchase
				{
					PMTProductID = (profileNotice.Purchase.HasPmtProductId ? new long?(profileNotice.Purchase.PmtProductId) : null),
					Data = (profileNotice.Purchase.HasData ? profileNotice.Purchase.Data : 0),
					CurrencyCode = profileNotice.Purchase.CurrencyCode
				};
			}
			else if (profileNotice.HasRewardCardBack)
			{
				profileNotice2 = new NetCache.ProfileNoticeRewardCardBack
				{
					CardBackID = profileNotice.RewardCardBack.CardBack
				};
			}
			else if (profileNotice.HasBonusStars)
			{
				profileNotice2 = new NetCache.ProfileNoticeBonusStars
				{
					StarLevel = profileNotice.BonusStars.StarLevel,
					Stars = profileNotice.BonusStars.Stars
				};
			}
			else if (profileNotice.HasDcGameResult)
			{
				if (!profileNotice.DcGameResult.HasGameType)
				{
					Debug.LogError("Network.GetProfileNotices(): Missing GameType");
					continue;
				}
				if (!profileNotice.DcGameResult.HasMissionId)
				{
					Debug.LogError("Network.GetProfileNotices(): Missing GameType");
					continue;
				}
				if (!profileNotice.DcGameResult.HasGameResult_)
				{
					Debug.LogError("Network.GetProfileNotices(): Missing GameResult");
					continue;
				}
				NetCache.ProfileNoticeDisconnectedGame profileNoticeDisconnectedGame = new NetCache.ProfileNoticeDisconnectedGame
				{
					GameType = profileNotice.DcGameResult.GameType,
					FormatType = profileNotice.DcGameResult.FormatType,
					MissionId = profileNotice.DcGameResult.MissionId,
					GameResult = profileNotice.DcGameResult.GameResult_
				};
				if (profileNoticeDisconnectedGame.GameResult == ProfileNoticeDisconnectedGameResult.GameResult.GR_WINNER)
				{
					if (!profileNotice.DcGameResult.HasYourResult || !profileNotice.DcGameResult.HasOpponentResult)
					{
						Debug.LogError("Network.GetProfileNotices(): Missing PlayerResult");
						continue;
					}
					profileNoticeDisconnectedGame.YourResult = profileNotice.DcGameResult.YourResult;
					profileNoticeDisconnectedGame.OpponentResult = profileNotice.DcGameResult.OpponentResult;
				}
				profileNotice2 = profileNoticeDisconnectedGame;
			}
			else if (profileNotice.HasDcGameResultNew)
			{
				if (!profileNotice.DcGameResultNew.HasGameType)
				{
					Debug.LogError("Network.GetProfileNotices(): Missing GameType");
					continue;
				}
				if (!profileNotice.DcGameResultNew.HasMissionId)
				{
					Debug.LogError("Network.GetProfileNotices(): Missing GameType");
					continue;
				}
				if (!profileNotice.DcGameResultNew.HasGameResult_)
				{
					Debug.LogError("Network.GetProfileNotices(): Missing GameResult");
					continue;
				}
				NetCache.ProfileNoticeDisconnectedGame profileNoticeDisconnectedGame2 = new NetCache.ProfileNoticeDisconnectedGame
				{
					GameType = profileNotice.DcGameResultNew.GameType,
					FormatType = profileNotice.DcGameResultNew.FormatType,
					MissionId = profileNotice.DcGameResultNew.MissionId,
					GameResult = (ProfileNoticeDisconnectedGameResult.GameResult)profileNotice.DcGameResultNew.GameResult_
				};
				if (profileNoticeDisconnectedGame2.GameResult == ProfileNoticeDisconnectedGameResult.GameResult.GR_WINNER)
				{
					if (!profileNotice.DcGameResultNew.HasYourResult)
					{
						Debug.LogError("Network.GetProfileNotices(): Missing New PlayerResult");
					}
					profileNoticeDisconnectedGame2.YourResult = (ProfileNoticeDisconnectedGameResult.PlayerResult)profileNotice.DcGameResultNew.YourResult;
				}
				profileNotice2 = profileNoticeDisconnectedGame2;
			}
			else if (profileNotice.HasAdventureProgress)
			{
				NetCache.ProfileNoticeAdventureProgress profileNoticeAdventureProgress = new NetCache.ProfileNoticeAdventureProgress
				{
					Wing = profileNotice.AdventureProgress.WingId
				};
				switch (profileNotice.Origin)
				{
				case 18:
					profileNoticeAdventureProgress.Progress = (int)(profileNotice.HasOriginData ? profileNotice.OriginData : 0);
					break;
				case 19:
					profileNoticeAdventureProgress.Flags = (ulong)(profileNotice.HasOriginData ? profileNotice.OriginData : 0);
					break;
				}
				profileNotice2 = profileNoticeAdventureProgress;
			}
			else if (profileNotice.HasLevelUp)
			{
				profileNotice2 = new NetCache.ProfileNoticeLevelUp
				{
					HeroClass = profileNotice.LevelUp.HeroClass,
					NewLevel = profileNotice.LevelUp.NewLevel,
					TotalLevel = profileNotice.LevelUp.TotalLevel
				};
			}
			else if (profileNotice.HasAccountLicense)
			{
				profileNotice2 = new NetCache.ProfileNoticeAcccountLicense
				{
					License = profileNotice.AccountLicense.License,
					CasID = profileNotice.AccountLicense.CasId
				};
			}
			else if (profileNotice.HasTavernBrawlRewards)
			{
				profileNotice2 = new NetCache.ProfileNoticeTavernBrawlRewards
				{
					Chest = profileNotice.TavernBrawlRewards.RewardChest,
					Wins = profileNotice.TavernBrawlRewards.NumWins,
					Mode = (profileNotice.TavernBrawlRewards.HasBrawlMode ? profileNotice.TavernBrawlRewards.BrawlMode : TavernBrawlMode.TB_MODE_NORMAL)
				};
			}
			else if (profileNotice.HasTavernBrawlTicket)
			{
				profileNotice2 = new NetCache.ProfileNoticeTavernBrawlTicket
				{
					TicketType = profileNotice.TavernBrawlTicket.TicketType,
					Quantity = profileNotice.TavernBrawlTicket.Quantity
				};
			}
			else if (profileNotice.HasGenericRewardChest)
			{
				NetCache.ProfileNoticeGenericRewardChest profileNoticeGenericRewardChest = new NetCache.ProfileNoticeGenericRewardChest();
				profileNoticeGenericRewardChest.RewardChestAssetId = profileNotice.GenericRewardChest.RewardChestAssetId;
				profileNoticeGenericRewardChest.RewardChest = profileNotice.GenericRewardChest.RewardChest;
				profileNoticeGenericRewardChest.RewardChestByteSize = 0u;
				profileNoticeGenericRewardChest.RewardChestHash = null;
				if (profileNotice.GenericRewardChest.HasRewardChestByteSize)
				{
					profileNoticeGenericRewardChest.RewardChestByteSize = profileNotice.GenericRewardChest.RewardChestByteSize;
				}
				if (profileNotice.GenericRewardChest.HasRewardChestHash)
				{
					profileNoticeGenericRewardChest.RewardChestHash = profileNotice.GenericRewardChest.RewardChestHash;
				}
				profileNotice2 = profileNoticeGenericRewardChest;
			}
			else if (profileNotice.HasLeaguePromotionRewards)
			{
				profileNotice2 = new NetCache.ProfileNoticeLeaguePromotionRewards
				{
					Chest = profileNotice.LeaguePromotionRewards.RewardChest,
					LeagueId = profileNotice.LeaguePromotionRewards.LeagueId
				};
			}
			else if (profileNotice.HasDeckRemoved)
			{
				profileNotice2 = new NetCache.ProfileNoticeDeckRemoved
				{
					DeckID = profileNotice.DeckRemoved.DeckId
				};
			}
			else if (profileNotice.HasFreeDeckChoice)
			{
				profileNotice2 = new NetCache.ProfileNoticeFreeDeckChoice();
			}
			else if (profileNotice.HasDeckGranted)
			{
				profileNotice2 = new NetCache.ProfileNoticeDeckGranted
				{
					DeckDbiID = profileNotice.DeckGranted.DeckDbiId,
					ClassId = profileNotice.DeckGranted.ClassId,
					PlayerDeckID = profileNotice.DeckGranted.PlayerDeckId
				};
			}
			else if (profileNotice.HasMiniSetGranted)
			{
				profileNotice2 = new NetCache.ProfileNoticeMiniSetGranted
				{
					MiniSetID = profileNotice.MiniSetGranted.MiniSetId
				};
			}
			else if (profileNotice.HasSellableDeckGranted)
			{
				profileNotice2 = new NetCache.ProfileNoticeSellableDeckGranted
				{
					SellableDeckID = profileNotice.SellableDeckGranted.SellableDeckId,
					WasDeckGranted = profileNotice.SellableDeckGranted.WasDeckGranted,
					PlayerDeckID = profileNotice.SellableDeckGranted.PlayerDeckId
				};
			}
			else
			{
				Debug.LogError("Network.GetProfileNotices(): Unrecognized profile notice");
			}
			if (profileNotice2 == null)
			{
				Debug.LogError("Network.GetProfileNotices(): Unhandled notice type! This notice will be lost!");
				continue;
			}
			profileNotice2.NoticeID = profileNotice.Entry;
			profileNotice2.Origin = (NetCache.ProfileNotice.NoticeOrigin)profileNotice.Origin;
			profileNotice2.OriginData = (profileNotice.HasOriginData ? profileNotice.OriginData : 0);
			profileNotice2.Date = TimeUtils.PegDateToFileTimeUtc(profileNotice.When);
			result.Add(profileNotice2);
		}
	}

	public NetCache.NetCacheMedalInfo GetMedalInfo()
	{
		if (!ShouldBeConnectedToAurora())
		{
			NetCache.NetCacheMedalInfo netCacheMedalInfo = new NetCache.NetCacheMedalInfo();
			{
				foreach (PegasusShared.FormatType value2 in Enum.GetValues(typeof(PegasusShared.FormatType)))
				{
					if (value2 != 0)
					{
						MedalInfoData value = new MedalInfoData
						{
							FormatType = value2
						};
						netCacheMedalInfo.MedalData.Add(value2, value);
					}
				}
				return netCacheMedalInfo;
			}
		}
		MedalInfo medalInfo = m_connectApi.GetMedalInfo();
		if (medalInfo == null)
		{
			return null;
		}
		return new NetCache.NetCacheMedalInfo(medalInfo);
	}

	public NetCache.NetCacheBaconRatingInfo GetBaconRatingInfo()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new NetCache.NetCacheBaconRatingInfo
			{
				Rating = 0
			};
		}
		ResponseWithRequest<BattlegroundsRatingInfoResponse, BattlegroundsRatingInfoRequest> responseWithRequest = m_connectApi.BattlegroundsRatingInfoResponse();
		if (responseWithRequest == null)
		{
			return null;
		}
		BattlegroundsRatingInfoResponse response = responseWithRequest.Response;
		if (response == null)
		{
			return null;
		}
		return new NetCache.NetCacheBaconRatingInfo
		{
			Rating = response.PlayerInfo.Rating
		};
	}

	public NetCache.NetCacheBaconPremiumStatus GetBaconPremiumStatus()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new NetCache.NetCacheBaconPremiumStatus
			{
				SeasonPremiumStatus = new List<BattlegroundSeasonPremiumStatus>()
			};
		}
		ResponseWithRequest<BattlegroundsPremiumStatusResponse, BattlegroundsPremiumStatusRequest> battlegroundsPremiumStatus = m_connectApi.GetBattlegroundsPremiumStatus();
		if (battlegroundsPremiumStatus == null)
		{
			return null;
		}
		BattlegroundsPremiumStatusResponse response = battlegroundsPremiumStatus.Response;
		if (response == null)
		{
			return null;
		}
		return new NetCache.NetCacheBaconPremiumStatus
		{
			SeasonPremiumStatus = response.SeasonPremiumStatus
		};
	}

	public NetCache.NetCachePVPDRStatsInfo GetPVPDRStatsInfo()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new NetCache.NetCachePVPDRStatsInfo
			{
				Rating = 0,
				PaidRating = 0,
				HighWatermark = 0
			};
		}
		ResponseWithRequest<PVPDRStatsInfoResponse, PVPDRStatsInfoRequest> responseWithRequest = m_connectApi.PVPDRStatsInfoResponse();
		if (responseWithRequest == null)
		{
			return null;
		}
		PVPDRStatsInfoResponse response = responseWithRequest.Response;
		if (response == null)
		{
			return null;
		}
		return new NetCache.NetCachePVPDRStatsInfo
		{
			Rating = response.Rating,
			PaidRating = response.PaidRating,
			HighWatermark = response.HighWatermark
		};
	}

	public GuardianVars GetGuardianVars()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new GuardianVars();
		}
		return m_connectApi.GetGuardianVars();
	}

	public PlayerRecords GetPlayerRecordsPacket()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new PlayerRecords();
		}
		return m_connectApi.GetPlayerRecords();
	}

	public static NetCache.NetCachePlayerRecords GetPlayerRecords(PlayerRecords packet)
	{
		if (packet == null)
		{
			return null;
		}
		NetCache.NetCachePlayerRecords netCachePlayerRecords = new NetCache.NetCachePlayerRecords();
		for (int i = 0; i < packet.Records.Count; i++)
		{
			PlayerRecord playerRecord = packet.Records[i];
			netCachePlayerRecords.Records.Add(new NetCache.PlayerRecord
			{
				RecordType = playerRecord.Type,
				Data = (playerRecord.HasData ? playerRecord.Data : 0),
				Wins = playerRecord.Wins,
				Losses = playerRecord.Losses,
				Ties = playerRecord.Ties
			});
		}
		return netCachePlayerRecords;
	}

	public NetCache.NetCacheRewardProgress GetRewardProgress()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new NetCache.NetCacheRewardProgress();
		}
		RewardProgress rewardProgress = m_connectApi.GetRewardProgress();
		if (rewardProgress == null)
		{
			return null;
		}
		return new NetCache.NetCacheRewardProgress
		{
			Season = rewardProgress.SeasonNumber,
			SeasonEndDate = TimeUtils.PegDateToFileTimeUtc(rewardProgress.SeasonEnd),
			NextQuestCancelDate = TimeUtils.PegDateToFileTimeUtc(rewardProgress.NextQuestCancel)
		};
	}

	public NetCache.NetCacheGamesPlayed GetGamesInfo()
	{
		GamesInfo gamesInfo = m_connectApi.GetGamesInfo();
		if (gamesInfo == null)
		{
			return null;
		}
		return new NetCache.NetCacheGamesPlayed
		{
			GamesStarted = gamesInfo.GamesStarted,
			GamesWon = gamesInfo.GamesWon,
			GamesLost = gamesInfo.GamesLost,
			FreeRewardProgress = gamesInfo.FreeRewardProgress
		};
	}

	public ClientStaticAssetsResponse GetClientStaticAssetsResponse()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new ClientStaticAssetsResponse();
		}
		return m_connectApi.GetClientStaticAssetsResponse();
	}

	public void RequestTavernBrawlInfo(BrawlType brawlType)
	{
		long? fsgId = (FiresideGatheringManager.Get().IsCheckedIn ? new long?(FiresideGatheringManager.Get().CurrentFsgId) : null);
		m_connectApi.RequestTavernBrawlInfo(brawlType, fsgId, FiresideGatheringManager.Get().CurrentFsgSharedSecretKey);
	}

	public void RequestTavernBrawlPlayerRecord(BrawlType brawlType)
	{
		long? fsgId = (FiresideGatheringManager.Get().IsCheckedIn ? new long?(FiresideGatheringManager.Get().CurrentFsgId) : null);
		m_connectApi.RequestTavernBrawlPlayerRecord(brawlType, fsgId, FiresideGatheringManager.Get().CurrentFsgSharedSecretKey);
	}

	public TavernBrawlInfo GetTavernBrawlInfo()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new TavernBrawlInfo();
		}
		return m_connectApi.GetTavernBrawlInfo();
	}

	public TavernBrawlRequestSessionBeginResponse GetTavernBrawlSessionBegin()
	{
		return m_connectApi.GetTavernBrawlSessionBeginResponse();
	}

	public void TavernBrawlRetire()
	{
		m_connectApi.TavernBrawlRetire();
	}

	public TavernBrawlRequestSessionRetireResponse GetTavernBrawlSessionRetired()
	{
		return m_connectApi.GetTavernBrawlSessionRetired();
	}

	public void RequestTavernBrawlSessionBegin()
	{
		m_connectApi.RequestTavernBrawlSessionBegin();
	}

	public void AckTavernBrawlSessionRewards()
	{
		m_connectApi.AckTavernBrawlSessionRewards();
	}

	public TavernBrawlPlayerRecord GetTavernBrawlRecord()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new TavernBrawlPlayerRecord();
		}
		return m_connectApi.GeTavernBrawlPlayerRecordResponse()?.Record;
	}

	public FavoriteHeroesResponse GetFavoriteHeroesResponse()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new FavoriteHeroesResponse();
		}
		return m_connectApi.GetFavoriteHeroesResponse();
	}

	public AccountLicensesInfoResponse GetAccountLicensesInfoResponse()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new AccountLicensesInfoResponse();
		}
		return m_connectApi.GetAccountLicensesInfoResponse();
	}

	public void RequestAccountLicensesUpdate()
	{
		m_connectApi.RequestAccountLicensesUpdate();
	}

	public UpdateAccountLicensesResponse GetUpdateAccountLicensesResponse()
	{
		return m_connectApi.GetUpdateAccountLicensesResponse();
	}

	public UpdateLoginComplete GetUpdateLoginComplete()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new UpdateLoginComplete();
		}
		return m_connectApi.GetUpdateLoginComplete();
	}

	public HeroXP GetHeroXP()
	{
		if (!ShouldBeConnectedToAurora())
		{
			return new HeroXP();
		}
		return m_connectApi.GetHeroXP();
	}

	public void AckNotice(long id)
	{
		if (NetCache.Get().RemoveNotice(id))
		{
			Log.Achievements.Print("acking notice: {0}", id);
			m_connectApi.AckNotice(id);
		}
	}

	public void AckAchieveProgress(int id, int ackProgress)
	{
		Log.Achievements.Print("AckAchieveProgress: Achieve={0} Progress={1}", id, ackProgress);
		m_connectApi.AckAchieveProgress(id, ackProgress);
	}

	public void AckQuest(int questId)
	{
		m_connectApi.AckQuest(questId);
	}

	public void CheckForNewQuests()
	{
		m_connectApi.CheckForNewQuests();
	}

	public void RerollQuest(int questId)
	{
		m_connectApi.RerollQuest(questId);
	}

	public void AckAchievement(int achievementId)
	{
		m_connectApi.AckAchievement(achievementId);
	}

	public void ClaimAchievementReward(int achievementId, int chooseOneRewardId = 0)
	{
		m_connectApi.ClaimAchievementReward(achievementId, chooseOneRewardId);
	}

	public void AckRewardTrackReward(int rewardTrackId, int level, bool forPaidTrack)
	{
		m_connectApi.AckRewardTrackReward(rewardTrackId, level, forPaidTrack);
	}

	public void ClaimRewardTrackReward(int rewardTrackId, int level, bool forPaidTrack, int chooseOneRewardItemId)
	{
		m_connectApi.ClaimRewardTrackReward(rewardTrackId, level, forPaidTrack, chooseOneRewardItemId);
	}

	public void CheckAccountLicenseAchieve(int achieveID)
	{
		m_connectApi.CheckAccountLicenseAchieve(achieveID);
	}

	public AccountLicenseAchieveResponse GetAccountLicenseAchieveResponse()
	{
		PegasusUtil.AccountLicenseAchieveResponse accountLicenseAchieveResponse = m_connectApi.GetAccountLicenseAchieveResponse();
		if (accountLicenseAchieveResponse == null)
		{
			return null;
		}
		return new AccountLicenseAchieveResponse
		{
			Achieve = accountLicenseAchieveResponse.Achieve,
			Result = (AccountLicenseAchieveResponse.AchieveResult)accountLicenseAchieveResponse.Result_
		};
	}

	public void AckCardSeenBefore(int assetId, TAG_PREMIUM premium)
	{
		PegasusShared.CardDef cardDef = new PegasusShared.CardDef
		{
			Asset = assetId
		};
		if (premium != 0)
		{
			cardDef.Premium = (int)premium;
		}
		m_ackCardSeenPacket.CardDefs.Add(cardDef);
		if (m_ackCardSeenPacket.CardDefs.Count > 15)
		{
			SendAckCardsSeen();
		}
	}

	public void AckWingProgress(int wingId, int ackId)
	{
		m_connectApi.AckWingProgress(wingId, ackId);
	}

	public void AcknowledgeBanner(int banner)
	{
		m_connectApi.AcknowledgeBanner(banner);
	}

	public void SendAckCardsSeen()
	{
		m_connectApi.AckCardSeen(m_ackCardSeenPacket);
		m_ackCardSeenPacket.CardDefs.Clear();
	}

	public void RequestNearbyFSGs(double latitude, double longitude, double accuracy, List<string> bssids)
	{
		Platform platformBuilder = GetPlatformBuilder();
		m_connectApi.RequestNearbyFSGs(latitude, longitude, accuracy, bssids, platformBuilder);
	}

	public void RequestNearbyFSGs(List<string> bssids)
	{
		Platform platformBuilder = GetPlatformBuilder();
		m_connectApi.RequestNearbyFSGs(bssids, platformBuilder);
	}

	public void CheckInToFSG(long gatheringID, double latitude, double longitude, double accuracy, List<string> bssids)
	{
		Platform platformBuilder = GetPlatformBuilder();
		m_connectApi.CheckInToFSG(gatheringID, latitude, longitude, accuracy, bssids, platformBuilder);
	}

	public void CheckInToFSG(long gatheringID, List<string> bssids)
	{
		Platform platformBuilder = GetPlatformBuilder();
		m_connectApi.CheckInToFSG(gatheringID, bssids, platformBuilder);
	}

	public void CheckOutOfFSG(long gatheringID)
	{
		Log.FiresideGatherings.Print("CheckOutOfFSG: sending check out to server for {0}", gatheringID);
		Platform platformBuilder = GetPlatformBuilder();
		m_connectApi.CheckOutOfFSG(gatheringID, platformBuilder);
	}

	public void InnkeeperSetupFSG(double latitude, double longitude, double accuracy, List<string> bssids, long fsgId)
	{
		Platform platformBuilder = GetPlatformBuilder();
		m_connectApi.InnkeeperSetupFSG(bssids, fsgId, new GPSCoords
		{
			Latitude = latitude,
			Longitude = longitude,
			Accuracy = accuracy
		}, platformBuilder);
	}

	public void InnkeeperSetupFSG(List<string> bssids, long fsgId)
	{
		Platform platformBuilder = GetPlatformBuilder();
		m_connectApi.InnkeeperSetupFSG(bssids, fsgId, platformBuilder);
	}

	public void RequestFSGPatronListUpdate()
	{
		m_connectApi.RequestFSGPatronListUpdate();
	}

	public void RequestLeaguePromoteSelf()
	{
		m_connectApi.RequestLeaguePromoteSelf();
	}

	public RequestNearbyFSGsResponse GetRequestNearbyFSGsResponse()
	{
		return m_connectApi.GetRequestNearbyFSGsResponse();
	}

	public CheckInToFSGResponse GetCheckInToFSGResponse()
	{
		return m_connectApi.GetCheckInToFSGResponse();
	}

	public CheckOutOfFSGResponse GetCheckOutOfFSGResponse()
	{
		return m_connectApi.GetCheckOutOfFSGResponse();
	}

	public InnkeeperSetupGatheringResponse GetInnkeeperSetupGatheringResponse()
	{
		return m_connectApi.GetInnkeeperSetupGatheringResponse();
	}

	public PatronCheckedInToFSG GetPatronCheckedInToFSG()
	{
		return m_connectApi.GetPatronCheckedInToFSG();
	}

	public PatronCheckedOutOfFSG GetPatronCheckedOutOfFSG()
	{
		return m_connectApi.GetPatronCheckedOutOfFSG();
	}

	public FSGPatronListUpdate GetFSGPatronListUpdate()
	{
		return m_connectApi.GetFSGPatronListUpdate();
	}

	public FSGFeatureConfig GetFSGFeatureConfig()
	{
		return m_connectApi.GetFSGFeatureConfig();
	}

	public LeaguePromoteSelfResponse GetLeaguePromoteSelfResponse()
	{
		return m_connectApi.GetLeaguePromoteSelfResponse();
	}

	public SmartDeckResponse GetSmartDeckResponse()
	{
		return m_connectApi.GetSmartDeckResponse();
	}

	public PlayerQuestStateUpdate GetPlayerQuestStateUpdate()
	{
		return m_connectApi.GetPlayerQuestStateUpdate();
	}

	public PlayerQuestPoolStateUpdate GetPlayerQuestPoolStateUpdate()
	{
		return m_connectApi.GetPlayerQuestPoolStateUpdate();
	}

	public PlayerAchievementStateUpdate GetPlayerAchievementStateUpdate()
	{
		return m_connectApi.GetPlayerAchievementStateUpdate();
	}

	public PlayerRewardTrackStateUpdate GetPlayerRewardTrackStateUpdate()
	{
		return m_connectApi.GetPlayerRewardTrackStateUpdate();
	}

	public RerollQuestResponse GetRerollQuestResponse()
	{
		return m_connectApi.GetRerollQuestResponse();
	}

	public RewardTrackXpNotification GetRewardTrackXpNotification()
	{
		return m_connectApi.GetRewardTrackXpNotification();
	}

	public RewardTrackUnclaimedNotification GetRewardTrackUnclaimedNotification()
	{
		return m_connectApi.GetRewardTrackUnclaimedNotification();
	}

	public void RequestGameSaveData(List<long> keys, int clientToken)
	{
		m_connectApi.RequestGameSaveData(keys, clientToken);
	}

	public GameSaveDataResponse GetGameSaveDataResponse()
	{
		return m_connectApi.GetGameSaveDataResponse();
	}

	public void SetGameSaveData(List<GameSaveDataUpdate> dataUpdates, int clientToken)
	{
		m_connectApi.SetGameSaveData(dataUpdates, clientToken);
	}

	public SetGameSaveDataResponse GetSetGameSaveDataResponse()
	{
		return m_connectApi.GetSetGameSaveDataResponse();
	}

	public CardSaleResult GetCardSaleResult()
	{
		BoughtSoldCard cardSaleResult = m_connectApi.GetCardSaleResult();
		if (cardSaleResult == null)
		{
			return null;
		}
		CardSaleResult cardSaleResult2 = new CardSaleResult
		{
			AssetID = cardSaleResult.Def.Asset,
			AssetName = GameUtils.TranslateDbIdToCardId(cardSaleResult.Def.Asset),
			Premium = (cardSaleResult.Def.HasPremium ? ((TAG_PREMIUM)cardSaleResult.Def.Premium) : TAG_PREMIUM.NORMAL),
			Action = (CardSaleResult.SaleResult)cardSaleResult.Result_,
			Amount = cardSaleResult.Amount,
			Count = ((!cardSaleResult.HasCount) ? 1 : cardSaleResult.Count),
			Nerfed = (cardSaleResult.HasNerfed && cardSaleResult.Nerfed),
			UnitSellPrice = (cardSaleResult.HasUnitSellPrice ? cardSaleResult.UnitSellPrice : 0),
			UnitBuyPrice = (cardSaleResult.HasUnitBuyPrice ? cardSaleResult.UnitBuyPrice : 0)
		};
		if (cardSaleResult.HasCurrentCollectionCount)
		{
			cardSaleResult2.CurrentCollectionCount = cardSaleResult.CurrentCollectionCount;
		}
		else
		{
			cardSaleResult2.CurrentCollectionCount = null;
		}
		if (cardSaleResult.HasCollectionVersion)
		{
			NetCache.Get().AddExpectedCollectionModification(cardSaleResult.CollectionVersion);
		}
		return cardSaleResult2;
	}

	public void TriggerPlayedNearbyPlayerOnSubnet(BnetGameAccountId lastOpponentHSGameAccountID, ulong lastOpponentSessionStartTime, BnetGameAccountId otherPlayerHSGameAccountID, ulong otherPlayerSessionStartTime)
	{
		m_connectApi.TriggerPlayedNearbyPlayerOnSubnet(lastOpponentHSGameAccountID.GetHi(), lastOpponentHSGameAccountID.GetLo(), lastOpponentSessionStartTime, otherPlayerHSGameAccountID.GetHi(), otherPlayerHSGameAccountID.GetLo(), otherPlayerSessionStartTime);
	}

	public void RequestAssetsVersion()
	{
		m_connectApi.RequestAssetsVersion(GetPlatformBuilder(), OfflineDataCache.GetCachedCollectionVersion(), OfflineDataCache.GetCachedDeckContentsTimes(), OfflineDataCache.GetCachedCollectionVersionLastModified());
	}

	public void LoginOk()
	{
		m_connectApi.OnLoginComplete();
	}

	public AssetsVersionResponse GetAssetsVersion()
	{
		return m_connectApi.GetAssetsVersionResponse();
	}

	public GetAssetResponse GetAssetResponse()
	{
		return m_connectApi.GetAssetResponse();
	}

	public void SendAssetRequest(int clientToken, List<AssetKey> requestKeys)
	{
		if (requestKeys != null && requestKeys.Count != 0)
		{
			long? fsgId = (FiresideGatheringManager.Get().IsCheckedIn ? new long?(FiresideGatheringManager.Get().CurrentFsgId) : null);
			m_connectApi.SendAssetRequest(clientToken, requestKeys, fsgId, FiresideGatheringManager.Get().CurrentFsgSharedSecretKey);
		}
	}

	public ServerResult GetServerResult()
	{
		return m_connectApi.GetServerResult();
	}

	private Platform GetPlatformBuilder()
	{
		Platform platform = new Platform
		{
			Os = (int)PlatformSettings.OS,
			Screen = (int)PlatformSettings.Screen,
			Name = PlatformSettings.DeviceName,
			UniqueDeviceIdentifier = SystemInfo.deviceUniqueIdentifier
		};
		AndroidStore androidStore = AndroidDeviceSettings.Get().GetAndroidStore();
		if (androidStore != 0)
		{
			platform.Store = (int)androidStore;
		}
		return platform;
	}

	public bool SendDebugConsoleCommand(string command)
	{
		if (!IsConnectedToGameServer())
		{
			Log.Net.Print($"Cannot send command '{command}' to server; no game server is active.");
			return false;
		}
		if (m_connectApi.AllowDebugConnections() && command != null)
		{
			m_connectApi.SendDebugConsoleCommand(command);
		}
		return true;
	}

	public void SendDebugConsoleResponse(int responseType, string message)
	{
		m_connectApi.SendDebugConsoleResponse(responseType, message);
	}

	public string GetDebugConsoleCommand()
	{
		DebugConsoleCommand debugConsoleCommand = m_connectApi.GetDebugConsoleCommand();
		if (debugConsoleCommand == null)
		{
			return string.Empty;
		}
		return debugConsoleCommand.Command;
	}

	public DebugConsoleResponse GetDebugConsoleResponse()
	{
		BobNetProto.DebugConsoleResponse debugConsoleResponse = m_connectApi.GetDebugConsoleResponse();
		if (debugConsoleResponse == null)
		{
			return null;
		}
		return new DebugConsoleResponse
		{
			Type = (int)debugConsoleResponse.ResponseType_,
			Response = debugConsoleResponse.Response
		};
	}

	public void SendDebugCommandRequest(DebugCommandRequest packet)
	{
		m_connectApi.SendDebugCommandRequest(packet);
	}

	public DebugCommandResponse GetDebugCommandResponse()
	{
		return m_connectApi.GetDebugCommandResponse();
	}

	public void SendLocateCheatServerRequest()
	{
		m_connectApi.SendLocateCheatServerRequest();
	}

	public LocateCheatServerResponse GetLocateCheatServerResponse()
	{
		return m_connectApi.GetLocateCheatServerResponse();
	}

	public GameToConnectNotification GetGameToConnectNotification()
	{
		return m_connectApi.GetGameToConnectNotification();
	}

	public void GetServerTimeRequest()
	{
		m_connectApi.GetServerTimeRequest((long)TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now));
	}

	public void ReportBlizzardCheckoutStatus(BlizzardCheckoutStatus status, HearthstoneCheckoutTransactionData data = null)
	{
		m_connectApi.ReportBlizzardCheckoutStatus(status, data, (long)TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now));
	}

	public ResponseWithRequest<GetServerTimeResponse, GetServerTimeRequest> GetServerTimeResponse()
	{
		return m_connectApi.GetServerTimeResponse();
	}

	public void SimulateUncleanDisconnectFromGameServer()
	{
		if (m_connectApi.HasGameServerConnection())
		{
			m_connectApi.DisconnectFromGameServer();
		}
	}

	public void SimulateReceivedPacketFromServer(PegasusPacket packet)
	{
		m_dispatcherImpl.NotifyUtilResponseReceived(packet);
	}

	private static string GetStoredUserName()
	{
		return null;
	}

	private static string GetStoredBNetIP()
	{
		return null;
	}

	private static string GetStoredVersion()
	{
		return null;
	}

	public UtilLogRelay GetUtilLogRelay()
	{
		return m_connectApi.GetUtilLogRelay();
	}

	public GameLogRelay GetGameLogRelay()
	{
		return m_connectApi.GetGameLogRelay();
	}
}
