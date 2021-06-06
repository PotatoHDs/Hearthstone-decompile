using System.IO;

namespace PegasusUtil
{
	public class GuardianVars : IProtoBuf
	{
		public enum PacketID
		{
			ID = 264
		}

		public bool HasTourney;

		private bool _Tourney;

		public bool HasPractice;

		private bool _Practice;

		public bool HasCasual;

		private bool _Casual;

		public bool HasForge;

		private bool _Forge;

		public bool HasFriendly;

		private bool _Friendly;

		public bool HasManager;

		private bool _Manager;

		public bool HasCrafting;

		private bool _Crafting;

		public bool HasHunter;

		private bool _Hunter;

		public bool HasMage;

		private bool _Mage;

		public bool HasPaladin;

		private bool _Paladin;

		public bool HasPriest;

		private bool _Priest;

		public bool HasRogue;

		private bool _Rogue;

		public bool HasShaman;

		private bool _Shaman;

		public bool HasWarlock;

		private bool _Warlock;

		public bool HasWarrior;

		private bool _Warrior;

		public bool HasShowUserUI;

		private int _ShowUserUI;

		public bool HasStore;

		private bool _Store;

		public bool HasBattlePay;

		private bool _BattlePay;

		public bool HasBuyWithGold;

		private bool _BuyWithGold;

		public bool HasTavernBrawl;

		private bool _TavernBrawl;

		public bool HasClientOptionsUpdateIntervalSeconds;

		private int _ClientOptionsUpdateIntervalSeconds;

		public bool HasCaisEnabledNonMobile;

		private bool _CaisEnabledNonMobile;

		public bool HasCaisEnabledMobileChina;

		private bool _CaisEnabledMobileChina;

		public bool HasCaisEnabledMobileSouthKorea;

		private bool _CaisEnabledMobileSouthKorea;

		public bool HasSendTelemetryPresence;

		private bool _SendTelemetryPresence;

		public bool HasFriendWeekConcederMaxDefense;

		private int _FriendWeekConcederMaxDefense;

		public bool HasWinsPerGold;

		private int _WinsPerGold;

		public bool HasGoldPerReward;

		private int _GoldPerReward;

		public bool HasMaxGoldPerDay;

		private int _MaxGoldPerDay;

		public bool HasXpSoloLimit;

		private int _XpSoloLimit;

		public bool HasMaxHeroLevel;

		private int _MaxHeroLevel;

		public bool HasEventTimingMod;

		private float _EventTimingMod;

		public bool HasFsgEnabled;

		private bool _FsgEnabled;

		public bool HasFsgAutoCheckinEnabled;

		private bool _FsgAutoCheckinEnabled;

		public bool HasFriendWeekConcededGameMinTotalTurns;

		private int _FriendWeekConcededGameMinTotalTurns;

		public bool HasFriendWeekAllowsTavernBrawlRecordUpdate;

		private bool _FriendWeekAllowsTavernBrawlRecordUpdate;

		public bool HasFsgShowBetaLabel;

		private bool _FsgShowBetaLabel;

		public bool HasFsgFriendListPatronCountLimit;

		private int _FsgFriendListPatronCountLimit;

		public bool HasArenaClosedToNewSessionsSeconds;

		private uint _ArenaClosedToNewSessionsSeconds;

		public bool HasFsgLoginScanEnabled;

		private bool _FsgLoginScanEnabled;

		public bool HasFsgMaxPresencePubscribedPatronCount;

		private int _FsgMaxPresencePubscribedPatronCount;

		public bool HasQuickPackOpeningAllowed;

		private bool _QuickPackOpeningAllowed;

		public bool HasAllowIosHighres;

		private bool _AllowIosHighres;

		public bool HasSimpleCheckout;

		private bool _SimpleCheckout;

		public bool HasDeckCompletionGetPlayerCollectionFromClient;

		private bool _DeckCompletionGetPlayerCollectionFromClient;

		public bool HasSoftAccountPurchasing;

		private bool _SoftAccountPurchasing;

		public bool HasEnableSmartDeckCompletion;

		private bool _EnableSmartDeckCompletion;

		public bool HasNumClassicPacksUntilDeprioritize;

		private int _NumClassicPacksUntilDeprioritize;

		public bool HasAllowOfflineClientActivityIos;

		private bool _AllowOfflineClientActivityIos;

		public bool HasAllowOfflineClientActivityAndroid;

		private bool _AllowOfflineClientActivityAndroid;

		public bool HasAllowOfflineClientActivityDesktop;

		private bool _AllowOfflineClientActivityDesktop;

		public bool HasAllowOfflineClientDeckDeletion;

		private bool _AllowOfflineClientDeckDeletion;

		public bool HasBattlegrounds;

		private bool _Battlegrounds;

		public bool HasBattlegroundsFriendlyChallenge;

		private bool _BattlegroundsFriendlyChallenge;

		public bool HasSimpleCheckoutIos;

		private bool _SimpleCheckoutIos;

		public bool HasSimpleCheckoutAndroidAmazon;

		private bool _SimpleCheckoutAndroidAmazon;

		public bool HasSimpleCheckoutAndroidGoogle;

		private bool _SimpleCheckoutAndroidGoogle;

		public bool HasSimpleCheckoutAndroidGlobal;

		private bool _SimpleCheckoutAndroidGlobal;

		public bool HasSimpleCheckoutWin;

		private bool _SimpleCheckoutWin;

		public bool HasSimpleCheckoutMac;

		private bool _SimpleCheckoutMac;

		public bool HasBattlegroundsEarlyAccessLicense;

		private int _BattlegroundsEarlyAccessLicense;

		public bool HasVirtualCurrencyEnabled;

		private bool _VirtualCurrencyEnabled;

		public bool HasBattlegroundsTutorial;

		private bool _BattlegroundsTutorial;

		public bool HasVintageStoreEnabled;

		private bool _VintageStoreEnabled;

		public bool HasBoosterRotatingSoonWarnDaysWithoutSale;

		private int _BoosterRotatingSoonWarnDaysWithoutSale;

		public bool HasBoosterRotatingSoonWarnDaysWithSale;

		private int _BoosterRotatingSoonWarnDaysWithSale;

		public bool HasBattlegroundsMaxRankedPartySize;

		private int _BattlegroundsMaxRankedPartySize;

		public bool HasDeckReordering;

		private bool _DeckReordering;

		public bool HasProgressionEnabled;

		private bool _ProgressionEnabled;

		public bool HasPvpdrClosedToNewSessionsSeconds;

		private uint _PvpdrClosedToNewSessionsSeconds;

		public bool HasDuels;

		private bool _Duels;

		public bool HasDuelsEarlyAccessLicense;

		private uint _DuelsEarlyAccessLicense;

		public bool HasPaidDuels;

		private bool _PaidDuels;

		public bool HasAllowLiveFpsGathering;

		private bool _AllowLiveFpsGathering;

		public bool HasJournalButtonDisabled;

		private bool _JournalButtonDisabled;

		public bool HasAchievementToastDisabled;

		private bool _AchievementToastDisabled;

		public bool HasCheckForNewQuestsIntervalJitterSecs;

		private float _CheckForNewQuestsIntervalJitterSecs;

		public bool HasRankedStandard;

		private bool _RankedStandard;

		public bool HasRankedWild;

		private bool _RankedWild;

		public bool HasRankedClassic;

		private bool _RankedClassic;

		public bool HasRankedNewPlayer;

		private bool _RankedNewPlayer;

		public bool HasContentstackEnabled;

		private bool _ContentstackEnabled;

		public bool HasEndOfTurnToastPauseBufferSecs;

		private float _EndOfTurnToastPauseBufferSecs;

		public bool HasAppRatingEnabled;

		private bool _AppRatingEnabled;

		public bool HasAppRatingSamplingPercentage;

		private float _AppRatingSamplingPercentage;

		public bool HasBuyCardBacksFromCollectionManagerEnabled;

		private bool _BuyCardBacksFromCollectionManagerEnabled;

		public bool HasBuyHeroSkinsFromCollectionManagerEnabled;

		private bool _BuyHeroSkinsFromCollectionManagerEnabled;

		public bool Tourney
		{
			get
			{
				return _Tourney;
			}
			set
			{
				_Tourney = value;
				HasTourney = true;
			}
		}

		public bool Practice
		{
			get
			{
				return _Practice;
			}
			set
			{
				_Practice = value;
				HasPractice = true;
			}
		}

		public bool Casual
		{
			get
			{
				return _Casual;
			}
			set
			{
				_Casual = value;
				HasCasual = true;
			}
		}

		public bool Forge
		{
			get
			{
				return _Forge;
			}
			set
			{
				_Forge = value;
				HasForge = true;
			}
		}

		public bool Friendly
		{
			get
			{
				return _Friendly;
			}
			set
			{
				_Friendly = value;
				HasFriendly = true;
			}
		}

		public bool Manager
		{
			get
			{
				return _Manager;
			}
			set
			{
				_Manager = value;
				HasManager = true;
			}
		}

		public bool Crafting
		{
			get
			{
				return _Crafting;
			}
			set
			{
				_Crafting = value;
				HasCrafting = true;
			}
		}

		public bool Hunter
		{
			get
			{
				return _Hunter;
			}
			set
			{
				_Hunter = value;
				HasHunter = true;
			}
		}

		public bool Mage
		{
			get
			{
				return _Mage;
			}
			set
			{
				_Mage = value;
				HasMage = true;
			}
		}

		public bool Paladin
		{
			get
			{
				return _Paladin;
			}
			set
			{
				_Paladin = value;
				HasPaladin = true;
			}
		}

		public bool Priest
		{
			get
			{
				return _Priest;
			}
			set
			{
				_Priest = value;
				HasPriest = true;
			}
		}

		public bool Rogue
		{
			get
			{
				return _Rogue;
			}
			set
			{
				_Rogue = value;
				HasRogue = true;
			}
		}

		public bool Shaman
		{
			get
			{
				return _Shaman;
			}
			set
			{
				_Shaman = value;
				HasShaman = true;
			}
		}

		public bool Warlock
		{
			get
			{
				return _Warlock;
			}
			set
			{
				_Warlock = value;
				HasWarlock = true;
			}
		}

		public bool Warrior
		{
			get
			{
				return _Warrior;
			}
			set
			{
				_Warrior = value;
				HasWarrior = true;
			}
		}

		public int ShowUserUI
		{
			get
			{
				return _ShowUserUI;
			}
			set
			{
				_ShowUserUI = value;
				HasShowUserUI = true;
			}
		}

		public bool Store
		{
			get
			{
				return _Store;
			}
			set
			{
				_Store = value;
				HasStore = true;
			}
		}

		public bool BattlePay
		{
			get
			{
				return _BattlePay;
			}
			set
			{
				_BattlePay = value;
				HasBattlePay = true;
			}
		}

		public bool BuyWithGold
		{
			get
			{
				return _BuyWithGold;
			}
			set
			{
				_BuyWithGold = value;
				HasBuyWithGold = true;
			}
		}

		public bool TavernBrawl
		{
			get
			{
				return _TavernBrawl;
			}
			set
			{
				_TavernBrawl = value;
				HasTavernBrawl = true;
			}
		}

		public int ClientOptionsUpdateIntervalSeconds
		{
			get
			{
				return _ClientOptionsUpdateIntervalSeconds;
			}
			set
			{
				_ClientOptionsUpdateIntervalSeconds = value;
				HasClientOptionsUpdateIntervalSeconds = true;
			}
		}

		public bool CaisEnabledNonMobile
		{
			get
			{
				return _CaisEnabledNonMobile;
			}
			set
			{
				_CaisEnabledNonMobile = value;
				HasCaisEnabledNonMobile = true;
			}
		}

		public bool CaisEnabledMobileChina
		{
			get
			{
				return _CaisEnabledMobileChina;
			}
			set
			{
				_CaisEnabledMobileChina = value;
				HasCaisEnabledMobileChina = true;
			}
		}

		public bool CaisEnabledMobileSouthKorea
		{
			get
			{
				return _CaisEnabledMobileSouthKorea;
			}
			set
			{
				_CaisEnabledMobileSouthKorea = value;
				HasCaisEnabledMobileSouthKorea = true;
			}
		}

		public bool SendTelemetryPresence
		{
			get
			{
				return _SendTelemetryPresence;
			}
			set
			{
				_SendTelemetryPresence = value;
				HasSendTelemetryPresence = true;
			}
		}

		public int FriendWeekConcederMaxDefense
		{
			get
			{
				return _FriendWeekConcederMaxDefense;
			}
			set
			{
				_FriendWeekConcederMaxDefense = value;
				HasFriendWeekConcederMaxDefense = true;
			}
		}

		public int WinsPerGold
		{
			get
			{
				return _WinsPerGold;
			}
			set
			{
				_WinsPerGold = value;
				HasWinsPerGold = true;
			}
		}

		public int GoldPerReward
		{
			get
			{
				return _GoldPerReward;
			}
			set
			{
				_GoldPerReward = value;
				HasGoldPerReward = true;
			}
		}

		public int MaxGoldPerDay
		{
			get
			{
				return _MaxGoldPerDay;
			}
			set
			{
				_MaxGoldPerDay = value;
				HasMaxGoldPerDay = true;
			}
		}

		public int XpSoloLimit
		{
			get
			{
				return _XpSoloLimit;
			}
			set
			{
				_XpSoloLimit = value;
				HasXpSoloLimit = true;
			}
		}

		public int MaxHeroLevel
		{
			get
			{
				return _MaxHeroLevel;
			}
			set
			{
				_MaxHeroLevel = value;
				HasMaxHeroLevel = true;
			}
		}

		public float EventTimingMod
		{
			get
			{
				return _EventTimingMod;
			}
			set
			{
				_EventTimingMod = value;
				HasEventTimingMod = true;
			}
		}

		public bool FsgEnabled
		{
			get
			{
				return _FsgEnabled;
			}
			set
			{
				_FsgEnabled = value;
				HasFsgEnabled = true;
			}
		}

		public bool FsgAutoCheckinEnabled
		{
			get
			{
				return _FsgAutoCheckinEnabled;
			}
			set
			{
				_FsgAutoCheckinEnabled = value;
				HasFsgAutoCheckinEnabled = true;
			}
		}

		public int FriendWeekConcededGameMinTotalTurns
		{
			get
			{
				return _FriendWeekConcededGameMinTotalTurns;
			}
			set
			{
				_FriendWeekConcededGameMinTotalTurns = value;
				HasFriendWeekConcededGameMinTotalTurns = true;
			}
		}

		public bool FriendWeekAllowsTavernBrawlRecordUpdate
		{
			get
			{
				return _FriendWeekAllowsTavernBrawlRecordUpdate;
			}
			set
			{
				_FriendWeekAllowsTavernBrawlRecordUpdate = value;
				HasFriendWeekAllowsTavernBrawlRecordUpdate = true;
			}
		}

		public bool FsgShowBetaLabel
		{
			get
			{
				return _FsgShowBetaLabel;
			}
			set
			{
				_FsgShowBetaLabel = value;
				HasFsgShowBetaLabel = true;
			}
		}

		public int FsgFriendListPatronCountLimit
		{
			get
			{
				return _FsgFriendListPatronCountLimit;
			}
			set
			{
				_FsgFriendListPatronCountLimit = value;
				HasFsgFriendListPatronCountLimit = true;
			}
		}

		public uint ArenaClosedToNewSessionsSeconds
		{
			get
			{
				return _ArenaClosedToNewSessionsSeconds;
			}
			set
			{
				_ArenaClosedToNewSessionsSeconds = value;
				HasArenaClosedToNewSessionsSeconds = true;
			}
		}

		public bool FsgLoginScanEnabled
		{
			get
			{
				return _FsgLoginScanEnabled;
			}
			set
			{
				_FsgLoginScanEnabled = value;
				HasFsgLoginScanEnabled = true;
			}
		}

		public int FsgMaxPresencePubscribedPatronCount
		{
			get
			{
				return _FsgMaxPresencePubscribedPatronCount;
			}
			set
			{
				_FsgMaxPresencePubscribedPatronCount = value;
				HasFsgMaxPresencePubscribedPatronCount = true;
			}
		}

		public bool QuickPackOpeningAllowed
		{
			get
			{
				return _QuickPackOpeningAllowed;
			}
			set
			{
				_QuickPackOpeningAllowed = value;
				HasQuickPackOpeningAllowed = true;
			}
		}

		public bool AllowIosHighres
		{
			get
			{
				return _AllowIosHighres;
			}
			set
			{
				_AllowIosHighres = value;
				HasAllowIosHighres = true;
			}
		}

		public bool SimpleCheckout
		{
			get
			{
				return _SimpleCheckout;
			}
			set
			{
				_SimpleCheckout = value;
				HasSimpleCheckout = true;
			}
		}

		public bool DeckCompletionGetPlayerCollectionFromClient
		{
			get
			{
				return _DeckCompletionGetPlayerCollectionFromClient;
			}
			set
			{
				_DeckCompletionGetPlayerCollectionFromClient = value;
				HasDeckCompletionGetPlayerCollectionFromClient = true;
			}
		}

		public bool SoftAccountPurchasing
		{
			get
			{
				return _SoftAccountPurchasing;
			}
			set
			{
				_SoftAccountPurchasing = value;
				HasSoftAccountPurchasing = true;
			}
		}

		public bool EnableSmartDeckCompletion
		{
			get
			{
				return _EnableSmartDeckCompletion;
			}
			set
			{
				_EnableSmartDeckCompletion = value;
				HasEnableSmartDeckCompletion = true;
			}
		}

		public int NumClassicPacksUntilDeprioritize
		{
			get
			{
				return _NumClassicPacksUntilDeprioritize;
			}
			set
			{
				_NumClassicPacksUntilDeprioritize = value;
				HasNumClassicPacksUntilDeprioritize = true;
			}
		}

		public bool AllowOfflineClientActivityIos
		{
			get
			{
				return _AllowOfflineClientActivityIos;
			}
			set
			{
				_AllowOfflineClientActivityIos = value;
				HasAllowOfflineClientActivityIos = true;
			}
		}

		public bool AllowOfflineClientActivityAndroid
		{
			get
			{
				return _AllowOfflineClientActivityAndroid;
			}
			set
			{
				_AllowOfflineClientActivityAndroid = value;
				HasAllowOfflineClientActivityAndroid = true;
			}
		}

		public bool AllowOfflineClientActivityDesktop
		{
			get
			{
				return _AllowOfflineClientActivityDesktop;
			}
			set
			{
				_AllowOfflineClientActivityDesktop = value;
				HasAllowOfflineClientActivityDesktop = true;
			}
		}

		public bool AllowOfflineClientDeckDeletion
		{
			get
			{
				return _AllowOfflineClientDeckDeletion;
			}
			set
			{
				_AllowOfflineClientDeckDeletion = value;
				HasAllowOfflineClientDeckDeletion = true;
			}
		}

		public bool Battlegrounds
		{
			get
			{
				return _Battlegrounds;
			}
			set
			{
				_Battlegrounds = value;
				HasBattlegrounds = true;
			}
		}

		public bool BattlegroundsFriendlyChallenge
		{
			get
			{
				return _BattlegroundsFriendlyChallenge;
			}
			set
			{
				_BattlegroundsFriendlyChallenge = value;
				HasBattlegroundsFriendlyChallenge = true;
			}
		}

		public bool SimpleCheckoutIos
		{
			get
			{
				return _SimpleCheckoutIos;
			}
			set
			{
				_SimpleCheckoutIos = value;
				HasSimpleCheckoutIos = true;
			}
		}

		public bool SimpleCheckoutAndroidAmazon
		{
			get
			{
				return _SimpleCheckoutAndroidAmazon;
			}
			set
			{
				_SimpleCheckoutAndroidAmazon = value;
				HasSimpleCheckoutAndroidAmazon = true;
			}
		}

		public bool SimpleCheckoutAndroidGoogle
		{
			get
			{
				return _SimpleCheckoutAndroidGoogle;
			}
			set
			{
				_SimpleCheckoutAndroidGoogle = value;
				HasSimpleCheckoutAndroidGoogle = true;
			}
		}

		public bool SimpleCheckoutAndroidGlobal
		{
			get
			{
				return _SimpleCheckoutAndroidGlobal;
			}
			set
			{
				_SimpleCheckoutAndroidGlobal = value;
				HasSimpleCheckoutAndroidGlobal = true;
			}
		}

		public bool SimpleCheckoutWin
		{
			get
			{
				return _SimpleCheckoutWin;
			}
			set
			{
				_SimpleCheckoutWin = value;
				HasSimpleCheckoutWin = true;
			}
		}

		public bool SimpleCheckoutMac
		{
			get
			{
				return _SimpleCheckoutMac;
			}
			set
			{
				_SimpleCheckoutMac = value;
				HasSimpleCheckoutMac = true;
			}
		}

		public int BattlegroundsEarlyAccessLicense
		{
			get
			{
				return _BattlegroundsEarlyAccessLicense;
			}
			set
			{
				_BattlegroundsEarlyAccessLicense = value;
				HasBattlegroundsEarlyAccessLicense = true;
			}
		}

		public bool VirtualCurrencyEnabled
		{
			get
			{
				return _VirtualCurrencyEnabled;
			}
			set
			{
				_VirtualCurrencyEnabled = value;
				HasVirtualCurrencyEnabled = true;
			}
		}

		public bool BattlegroundsTutorial
		{
			get
			{
				return _BattlegroundsTutorial;
			}
			set
			{
				_BattlegroundsTutorial = value;
				HasBattlegroundsTutorial = true;
			}
		}

		public bool VintageStoreEnabled
		{
			get
			{
				return _VintageStoreEnabled;
			}
			set
			{
				_VintageStoreEnabled = value;
				HasVintageStoreEnabled = true;
			}
		}

		public int BoosterRotatingSoonWarnDaysWithoutSale
		{
			get
			{
				return _BoosterRotatingSoonWarnDaysWithoutSale;
			}
			set
			{
				_BoosterRotatingSoonWarnDaysWithoutSale = value;
				HasBoosterRotatingSoonWarnDaysWithoutSale = true;
			}
		}

		public int BoosterRotatingSoonWarnDaysWithSale
		{
			get
			{
				return _BoosterRotatingSoonWarnDaysWithSale;
			}
			set
			{
				_BoosterRotatingSoonWarnDaysWithSale = value;
				HasBoosterRotatingSoonWarnDaysWithSale = true;
			}
		}

		public int BattlegroundsMaxRankedPartySize
		{
			get
			{
				return _BattlegroundsMaxRankedPartySize;
			}
			set
			{
				_BattlegroundsMaxRankedPartySize = value;
				HasBattlegroundsMaxRankedPartySize = true;
			}
		}

		public bool DeckReordering
		{
			get
			{
				return _DeckReordering;
			}
			set
			{
				_DeckReordering = value;
				HasDeckReordering = true;
			}
		}

		public bool ProgressionEnabled
		{
			get
			{
				return _ProgressionEnabled;
			}
			set
			{
				_ProgressionEnabled = value;
				HasProgressionEnabled = true;
			}
		}

		public uint PvpdrClosedToNewSessionsSeconds
		{
			get
			{
				return _PvpdrClosedToNewSessionsSeconds;
			}
			set
			{
				_PvpdrClosedToNewSessionsSeconds = value;
				HasPvpdrClosedToNewSessionsSeconds = true;
			}
		}

		public bool Duels
		{
			get
			{
				return _Duels;
			}
			set
			{
				_Duels = value;
				HasDuels = true;
			}
		}

		public uint DuelsEarlyAccessLicense
		{
			get
			{
				return _DuelsEarlyAccessLicense;
			}
			set
			{
				_DuelsEarlyAccessLicense = value;
				HasDuelsEarlyAccessLicense = true;
			}
		}

		public bool PaidDuels
		{
			get
			{
				return _PaidDuels;
			}
			set
			{
				_PaidDuels = value;
				HasPaidDuels = true;
			}
		}

		public bool AllowLiveFpsGathering
		{
			get
			{
				return _AllowLiveFpsGathering;
			}
			set
			{
				_AllowLiveFpsGathering = value;
				HasAllowLiveFpsGathering = true;
			}
		}

		public bool JournalButtonDisabled
		{
			get
			{
				return _JournalButtonDisabled;
			}
			set
			{
				_JournalButtonDisabled = value;
				HasJournalButtonDisabled = true;
			}
		}

		public bool AchievementToastDisabled
		{
			get
			{
				return _AchievementToastDisabled;
			}
			set
			{
				_AchievementToastDisabled = value;
				HasAchievementToastDisabled = true;
			}
		}

		public float CheckForNewQuestsIntervalJitterSecs
		{
			get
			{
				return _CheckForNewQuestsIntervalJitterSecs;
			}
			set
			{
				_CheckForNewQuestsIntervalJitterSecs = value;
				HasCheckForNewQuestsIntervalJitterSecs = true;
			}
		}

		public bool RankedStandard
		{
			get
			{
				return _RankedStandard;
			}
			set
			{
				_RankedStandard = value;
				HasRankedStandard = true;
			}
		}

		public bool RankedWild
		{
			get
			{
				return _RankedWild;
			}
			set
			{
				_RankedWild = value;
				HasRankedWild = true;
			}
		}

		public bool RankedClassic
		{
			get
			{
				return _RankedClassic;
			}
			set
			{
				_RankedClassic = value;
				HasRankedClassic = true;
			}
		}

		public bool RankedNewPlayer
		{
			get
			{
				return _RankedNewPlayer;
			}
			set
			{
				_RankedNewPlayer = value;
				HasRankedNewPlayer = true;
			}
		}

		public bool ContentstackEnabled
		{
			get
			{
				return _ContentstackEnabled;
			}
			set
			{
				_ContentstackEnabled = value;
				HasContentstackEnabled = true;
			}
		}

		public float EndOfTurnToastPauseBufferSecs
		{
			get
			{
				return _EndOfTurnToastPauseBufferSecs;
			}
			set
			{
				_EndOfTurnToastPauseBufferSecs = value;
				HasEndOfTurnToastPauseBufferSecs = true;
			}
		}

		public bool AppRatingEnabled
		{
			get
			{
				return _AppRatingEnabled;
			}
			set
			{
				_AppRatingEnabled = value;
				HasAppRatingEnabled = true;
			}
		}

		public float AppRatingSamplingPercentage
		{
			get
			{
				return _AppRatingSamplingPercentage;
			}
			set
			{
				_AppRatingSamplingPercentage = value;
				HasAppRatingSamplingPercentage = true;
			}
		}

		public bool BuyCardBacksFromCollectionManagerEnabled
		{
			get
			{
				return _BuyCardBacksFromCollectionManagerEnabled;
			}
			set
			{
				_BuyCardBacksFromCollectionManagerEnabled = value;
				HasBuyCardBacksFromCollectionManagerEnabled = true;
			}
		}

		public bool BuyHeroSkinsFromCollectionManagerEnabled
		{
			get
			{
				return _BuyHeroSkinsFromCollectionManagerEnabled;
			}
			set
			{
				_BuyHeroSkinsFromCollectionManagerEnabled = value;
				HasBuyHeroSkinsFromCollectionManagerEnabled = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTourney)
			{
				num ^= Tourney.GetHashCode();
			}
			if (HasPractice)
			{
				num ^= Practice.GetHashCode();
			}
			if (HasCasual)
			{
				num ^= Casual.GetHashCode();
			}
			if (HasForge)
			{
				num ^= Forge.GetHashCode();
			}
			if (HasFriendly)
			{
				num ^= Friendly.GetHashCode();
			}
			if (HasManager)
			{
				num ^= Manager.GetHashCode();
			}
			if (HasCrafting)
			{
				num ^= Crafting.GetHashCode();
			}
			if (HasHunter)
			{
				num ^= Hunter.GetHashCode();
			}
			if (HasMage)
			{
				num ^= Mage.GetHashCode();
			}
			if (HasPaladin)
			{
				num ^= Paladin.GetHashCode();
			}
			if (HasPriest)
			{
				num ^= Priest.GetHashCode();
			}
			if (HasRogue)
			{
				num ^= Rogue.GetHashCode();
			}
			if (HasShaman)
			{
				num ^= Shaman.GetHashCode();
			}
			if (HasWarlock)
			{
				num ^= Warlock.GetHashCode();
			}
			if (HasWarrior)
			{
				num ^= Warrior.GetHashCode();
			}
			if (HasShowUserUI)
			{
				num ^= ShowUserUI.GetHashCode();
			}
			if (HasStore)
			{
				num ^= Store.GetHashCode();
			}
			if (HasBattlePay)
			{
				num ^= BattlePay.GetHashCode();
			}
			if (HasBuyWithGold)
			{
				num ^= BuyWithGold.GetHashCode();
			}
			if (HasTavernBrawl)
			{
				num ^= TavernBrawl.GetHashCode();
			}
			if (HasClientOptionsUpdateIntervalSeconds)
			{
				num ^= ClientOptionsUpdateIntervalSeconds.GetHashCode();
			}
			if (HasCaisEnabledNonMobile)
			{
				num ^= CaisEnabledNonMobile.GetHashCode();
			}
			if (HasCaisEnabledMobileChina)
			{
				num ^= CaisEnabledMobileChina.GetHashCode();
			}
			if (HasCaisEnabledMobileSouthKorea)
			{
				num ^= CaisEnabledMobileSouthKorea.GetHashCode();
			}
			if (HasSendTelemetryPresence)
			{
				num ^= SendTelemetryPresence.GetHashCode();
			}
			if (HasFriendWeekConcederMaxDefense)
			{
				num ^= FriendWeekConcederMaxDefense.GetHashCode();
			}
			if (HasWinsPerGold)
			{
				num ^= WinsPerGold.GetHashCode();
			}
			if (HasGoldPerReward)
			{
				num ^= GoldPerReward.GetHashCode();
			}
			if (HasMaxGoldPerDay)
			{
				num ^= MaxGoldPerDay.GetHashCode();
			}
			if (HasXpSoloLimit)
			{
				num ^= XpSoloLimit.GetHashCode();
			}
			if (HasMaxHeroLevel)
			{
				num ^= MaxHeroLevel.GetHashCode();
			}
			if (HasEventTimingMod)
			{
				num ^= EventTimingMod.GetHashCode();
			}
			if (HasFsgEnabled)
			{
				num ^= FsgEnabled.GetHashCode();
			}
			if (HasFsgAutoCheckinEnabled)
			{
				num ^= FsgAutoCheckinEnabled.GetHashCode();
			}
			if (HasFriendWeekConcededGameMinTotalTurns)
			{
				num ^= FriendWeekConcededGameMinTotalTurns.GetHashCode();
			}
			if (HasFriendWeekAllowsTavernBrawlRecordUpdate)
			{
				num ^= FriendWeekAllowsTavernBrawlRecordUpdate.GetHashCode();
			}
			if (HasFsgShowBetaLabel)
			{
				num ^= FsgShowBetaLabel.GetHashCode();
			}
			if (HasFsgFriendListPatronCountLimit)
			{
				num ^= FsgFriendListPatronCountLimit.GetHashCode();
			}
			if (HasArenaClosedToNewSessionsSeconds)
			{
				num ^= ArenaClosedToNewSessionsSeconds.GetHashCode();
			}
			if (HasFsgLoginScanEnabled)
			{
				num ^= FsgLoginScanEnabled.GetHashCode();
			}
			if (HasFsgMaxPresencePubscribedPatronCount)
			{
				num ^= FsgMaxPresencePubscribedPatronCount.GetHashCode();
			}
			if (HasQuickPackOpeningAllowed)
			{
				num ^= QuickPackOpeningAllowed.GetHashCode();
			}
			if (HasAllowIosHighres)
			{
				num ^= AllowIosHighres.GetHashCode();
			}
			if (HasSimpleCheckout)
			{
				num ^= SimpleCheckout.GetHashCode();
			}
			if (HasDeckCompletionGetPlayerCollectionFromClient)
			{
				num ^= DeckCompletionGetPlayerCollectionFromClient.GetHashCode();
			}
			if (HasSoftAccountPurchasing)
			{
				num ^= SoftAccountPurchasing.GetHashCode();
			}
			if (HasEnableSmartDeckCompletion)
			{
				num ^= EnableSmartDeckCompletion.GetHashCode();
			}
			if (HasNumClassicPacksUntilDeprioritize)
			{
				num ^= NumClassicPacksUntilDeprioritize.GetHashCode();
			}
			if (HasAllowOfflineClientActivityIos)
			{
				num ^= AllowOfflineClientActivityIos.GetHashCode();
			}
			if (HasAllowOfflineClientActivityAndroid)
			{
				num ^= AllowOfflineClientActivityAndroid.GetHashCode();
			}
			if (HasAllowOfflineClientActivityDesktop)
			{
				num ^= AllowOfflineClientActivityDesktop.GetHashCode();
			}
			if (HasAllowOfflineClientDeckDeletion)
			{
				num ^= AllowOfflineClientDeckDeletion.GetHashCode();
			}
			if (HasBattlegrounds)
			{
				num ^= Battlegrounds.GetHashCode();
			}
			if (HasBattlegroundsFriendlyChallenge)
			{
				num ^= BattlegroundsFriendlyChallenge.GetHashCode();
			}
			if (HasSimpleCheckoutIos)
			{
				num ^= SimpleCheckoutIos.GetHashCode();
			}
			if (HasSimpleCheckoutAndroidAmazon)
			{
				num ^= SimpleCheckoutAndroidAmazon.GetHashCode();
			}
			if (HasSimpleCheckoutAndroidGoogle)
			{
				num ^= SimpleCheckoutAndroidGoogle.GetHashCode();
			}
			if (HasSimpleCheckoutAndroidGlobal)
			{
				num ^= SimpleCheckoutAndroidGlobal.GetHashCode();
			}
			if (HasSimpleCheckoutWin)
			{
				num ^= SimpleCheckoutWin.GetHashCode();
			}
			if (HasSimpleCheckoutMac)
			{
				num ^= SimpleCheckoutMac.GetHashCode();
			}
			if (HasBattlegroundsEarlyAccessLicense)
			{
				num ^= BattlegroundsEarlyAccessLicense.GetHashCode();
			}
			if (HasVirtualCurrencyEnabled)
			{
				num ^= VirtualCurrencyEnabled.GetHashCode();
			}
			if (HasBattlegroundsTutorial)
			{
				num ^= BattlegroundsTutorial.GetHashCode();
			}
			if (HasVintageStoreEnabled)
			{
				num ^= VintageStoreEnabled.GetHashCode();
			}
			if (HasBoosterRotatingSoonWarnDaysWithoutSale)
			{
				num ^= BoosterRotatingSoonWarnDaysWithoutSale.GetHashCode();
			}
			if (HasBoosterRotatingSoonWarnDaysWithSale)
			{
				num ^= BoosterRotatingSoonWarnDaysWithSale.GetHashCode();
			}
			if (HasBattlegroundsMaxRankedPartySize)
			{
				num ^= BattlegroundsMaxRankedPartySize.GetHashCode();
			}
			if (HasDeckReordering)
			{
				num ^= DeckReordering.GetHashCode();
			}
			if (HasProgressionEnabled)
			{
				num ^= ProgressionEnabled.GetHashCode();
			}
			if (HasPvpdrClosedToNewSessionsSeconds)
			{
				num ^= PvpdrClosedToNewSessionsSeconds.GetHashCode();
			}
			if (HasDuels)
			{
				num ^= Duels.GetHashCode();
			}
			if (HasDuelsEarlyAccessLicense)
			{
				num ^= DuelsEarlyAccessLicense.GetHashCode();
			}
			if (HasPaidDuels)
			{
				num ^= PaidDuels.GetHashCode();
			}
			if (HasAllowLiveFpsGathering)
			{
				num ^= AllowLiveFpsGathering.GetHashCode();
			}
			if (HasJournalButtonDisabled)
			{
				num ^= JournalButtonDisabled.GetHashCode();
			}
			if (HasAchievementToastDisabled)
			{
				num ^= AchievementToastDisabled.GetHashCode();
			}
			if (HasCheckForNewQuestsIntervalJitterSecs)
			{
				num ^= CheckForNewQuestsIntervalJitterSecs.GetHashCode();
			}
			if (HasRankedStandard)
			{
				num ^= RankedStandard.GetHashCode();
			}
			if (HasRankedWild)
			{
				num ^= RankedWild.GetHashCode();
			}
			if (HasRankedClassic)
			{
				num ^= RankedClassic.GetHashCode();
			}
			if (HasRankedNewPlayer)
			{
				num ^= RankedNewPlayer.GetHashCode();
			}
			if (HasContentstackEnabled)
			{
				num ^= ContentstackEnabled.GetHashCode();
			}
			if (HasEndOfTurnToastPauseBufferSecs)
			{
				num ^= EndOfTurnToastPauseBufferSecs.GetHashCode();
			}
			if (HasAppRatingEnabled)
			{
				num ^= AppRatingEnabled.GetHashCode();
			}
			if (HasAppRatingSamplingPercentage)
			{
				num ^= AppRatingSamplingPercentage.GetHashCode();
			}
			if (HasBuyCardBacksFromCollectionManagerEnabled)
			{
				num ^= BuyCardBacksFromCollectionManagerEnabled.GetHashCode();
			}
			if (HasBuyHeroSkinsFromCollectionManagerEnabled)
			{
				num ^= BuyHeroSkinsFromCollectionManagerEnabled.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GuardianVars guardianVars = obj as GuardianVars;
			if (guardianVars == null)
			{
				return false;
			}
			if (HasTourney != guardianVars.HasTourney || (HasTourney && !Tourney.Equals(guardianVars.Tourney)))
			{
				return false;
			}
			if (HasPractice != guardianVars.HasPractice || (HasPractice && !Practice.Equals(guardianVars.Practice)))
			{
				return false;
			}
			if (HasCasual != guardianVars.HasCasual || (HasCasual && !Casual.Equals(guardianVars.Casual)))
			{
				return false;
			}
			if (HasForge != guardianVars.HasForge || (HasForge && !Forge.Equals(guardianVars.Forge)))
			{
				return false;
			}
			if (HasFriendly != guardianVars.HasFriendly || (HasFriendly && !Friendly.Equals(guardianVars.Friendly)))
			{
				return false;
			}
			if (HasManager != guardianVars.HasManager || (HasManager && !Manager.Equals(guardianVars.Manager)))
			{
				return false;
			}
			if (HasCrafting != guardianVars.HasCrafting || (HasCrafting && !Crafting.Equals(guardianVars.Crafting)))
			{
				return false;
			}
			if (HasHunter != guardianVars.HasHunter || (HasHunter && !Hunter.Equals(guardianVars.Hunter)))
			{
				return false;
			}
			if (HasMage != guardianVars.HasMage || (HasMage && !Mage.Equals(guardianVars.Mage)))
			{
				return false;
			}
			if (HasPaladin != guardianVars.HasPaladin || (HasPaladin && !Paladin.Equals(guardianVars.Paladin)))
			{
				return false;
			}
			if (HasPriest != guardianVars.HasPriest || (HasPriest && !Priest.Equals(guardianVars.Priest)))
			{
				return false;
			}
			if (HasRogue != guardianVars.HasRogue || (HasRogue && !Rogue.Equals(guardianVars.Rogue)))
			{
				return false;
			}
			if (HasShaman != guardianVars.HasShaman || (HasShaman && !Shaman.Equals(guardianVars.Shaman)))
			{
				return false;
			}
			if (HasWarlock != guardianVars.HasWarlock || (HasWarlock && !Warlock.Equals(guardianVars.Warlock)))
			{
				return false;
			}
			if (HasWarrior != guardianVars.HasWarrior || (HasWarrior && !Warrior.Equals(guardianVars.Warrior)))
			{
				return false;
			}
			if (HasShowUserUI != guardianVars.HasShowUserUI || (HasShowUserUI && !ShowUserUI.Equals(guardianVars.ShowUserUI)))
			{
				return false;
			}
			if (HasStore != guardianVars.HasStore || (HasStore && !Store.Equals(guardianVars.Store)))
			{
				return false;
			}
			if (HasBattlePay != guardianVars.HasBattlePay || (HasBattlePay && !BattlePay.Equals(guardianVars.BattlePay)))
			{
				return false;
			}
			if (HasBuyWithGold != guardianVars.HasBuyWithGold || (HasBuyWithGold && !BuyWithGold.Equals(guardianVars.BuyWithGold)))
			{
				return false;
			}
			if (HasTavernBrawl != guardianVars.HasTavernBrawl || (HasTavernBrawl && !TavernBrawl.Equals(guardianVars.TavernBrawl)))
			{
				return false;
			}
			if (HasClientOptionsUpdateIntervalSeconds != guardianVars.HasClientOptionsUpdateIntervalSeconds || (HasClientOptionsUpdateIntervalSeconds && !ClientOptionsUpdateIntervalSeconds.Equals(guardianVars.ClientOptionsUpdateIntervalSeconds)))
			{
				return false;
			}
			if (HasCaisEnabledNonMobile != guardianVars.HasCaisEnabledNonMobile || (HasCaisEnabledNonMobile && !CaisEnabledNonMobile.Equals(guardianVars.CaisEnabledNonMobile)))
			{
				return false;
			}
			if (HasCaisEnabledMobileChina != guardianVars.HasCaisEnabledMobileChina || (HasCaisEnabledMobileChina && !CaisEnabledMobileChina.Equals(guardianVars.CaisEnabledMobileChina)))
			{
				return false;
			}
			if (HasCaisEnabledMobileSouthKorea != guardianVars.HasCaisEnabledMobileSouthKorea || (HasCaisEnabledMobileSouthKorea && !CaisEnabledMobileSouthKorea.Equals(guardianVars.CaisEnabledMobileSouthKorea)))
			{
				return false;
			}
			if (HasSendTelemetryPresence != guardianVars.HasSendTelemetryPresence || (HasSendTelemetryPresence && !SendTelemetryPresence.Equals(guardianVars.SendTelemetryPresence)))
			{
				return false;
			}
			if (HasFriendWeekConcederMaxDefense != guardianVars.HasFriendWeekConcederMaxDefense || (HasFriendWeekConcederMaxDefense && !FriendWeekConcederMaxDefense.Equals(guardianVars.FriendWeekConcederMaxDefense)))
			{
				return false;
			}
			if (HasWinsPerGold != guardianVars.HasWinsPerGold || (HasWinsPerGold && !WinsPerGold.Equals(guardianVars.WinsPerGold)))
			{
				return false;
			}
			if (HasGoldPerReward != guardianVars.HasGoldPerReward || (HasGoldPerReward && !GoldPerReward.Equals(guardianVars.GoldPerReward)))
			{
				return false;
			}
			if (HasMaxGoldPerDay != guardianVars.HasMaxGoldPerDay || (HasMaxGoldPerDay && !MaxGoldPerDay.Equals(guardianVars.MaxGoldPerDay)))
			{
				return false;
			}
			if (HasXpSoloLimit != guardianVars.HasXpSoloLimit || (HasXpSoloLimit && !XpSoloLimit.Equals(guardianVars.XpSoloLimit)))
			{
				return false;
			}
			if (HasMaxHeroLevel != guardianVars.HasMaxHeroLevel || (HasMaxHeroLevel && !MaxHeroLevel.Equals(guardianVars.MaxHeroLevel)))
			{
				return false;
			}
			if (HasEventTimingMod != guardianVars.HasEventTimingMod || (HasEventTimingMod && !EventTimingMod.Equals(guardianVars.EventTimingMod)))
			{
				return false;
			}
			if (HasFsgEnabled != guardianVars.HasFsgEnabled || (HasFsgEnabled && !FsgEnabled.Equals(guardianVars.FsgEnabled)))
			{
				return false;
			}
			if (HasFsgAutoCheckinEnabled != guardianVars.HasFsgAutoCheckinEnabled || (HasFsgAutoCheckinEnabled && !FsgAutoCheckinEnabled.Equals(guardianVars.FsgAutoCheckinEnabled)))
			{
				return false;
			}
			if (HasFriendWeekConcededGameMinTotalTurns != guardianVars.HasFriendWeekConcededGameMinTotalTurns || (HasFriendWeekConcededGameMinTotalTurns && !FriendWeekConcededGameMinTotalTurns.Equals(guardianVars.FriendWeekConcededGameMinTotalTurns)))
			{
				return false;
			}
			if (HasFriendWeekAllowsTavernBrawlRecordUpdate != guardianVars.HasFriendWeekAllowsTavernBrawlRecordUpdate || (HasFriendWeekAllowsTavernBrawlRecordUpdate && !FriendWeekAllowsTavernBrawlRecordUpdate.Equals(guardianVars.FriendWeekAllowsTavernBrawlRecordUpdate)))
			{
				return false;
			}
			if (HasFsgShowBetaLabel != guardianVars.HasFsgShowBetaLabel || (HasFsgShowBetaLabel && !FsgShowBetaLabel.Equals(guardianVars.FsgShowBetaLabel)))
			{
				return false;
			}
			if (HasFsgFriendListPatronCountLimit != guardianVars.HasFsgFriendListPatronCountLimit || (HasFsgFriendListPatronCountLimit && !FsgFriendListPatronCountLimit.Equals(guardianVars.FsgFriendListPatronCountLimit)))
			{
				return false;
			}
			if (HasArenaClosedToNewSessionsSeconds != guardianVars.HasArenaClosedToNewSessionsSeconds || (HasArenaClosedToNewSessionsSeconds && !ArenaClosedToNewSessionsSeconds.Equals(guardianVars.ArenaClosedToNewSessionsSeconds)))
			{
				return false;
			}
			if (HasFsgLoginScanEnabled != guardianVars.HasFsgLoginScanEnabled || (HasFsgLoginScanEnabled && !FsgLoginScanEnabled.Equals(guardianVars.FsgLoginScanEnabled)))
			{
				return false;
			}
			if (HasFsgMaxPresencePubscribedPatronCount != guardianVars.HasFsgMaxPresencePubscribedPatronCount || (HasFsgMaxPresencePubscribedPatronCount && !FsgMaxPresencePubscribedPatronCount.Equals(guardianVars.FsgMaxPresencePubscribedPatronCount)))
			{
				return false;
			}
			if (HasQuickPackOpeningAllowed != guardianVars.HasQuickPackOpeningAllowed || (HasQuickPackOpeningAllowed && !QuickPackOpeningAllowed.Equals(guardianVars.QuickPackOpeningAllowed)))
			{
				return false;
			}
			if (HasAllowIosHighres != guardianVars.HasAllowIosHighres || (HasAllowIosHighres && !AllowIosHighres.Equals(guardianVars.AllowIosHighres)))
			{
				return false;
			}
			if (HasSimpleCheckout != guardianVars.HasSimpleCheckout || (HasSimpleCheckout && !SimpleCheckout.Equals(guardianVars.SimpleCheckout)))
			{
				return false;
			}
			if (HasDeckCompletionGetPlayerCollectionFromClient != guardianVars.HasDeckCompletionGetPlayerCollectionFromClient || (HasDeckCompletionGetPlayerCollectionFromClient && !DeckCompletionGetPlayerCollectionFromClient.Equals(guardianVars.DeckCompletionGetPlayerCollectionFromClient)))
			{
				return false;
			}
			if (HasSoftAccountPurchasing != guardianVars.HasSoftAccountPurchasing || (HasSoftAccountPurchasing && !SoftAccountPurchasing.Equals(guardianVars.SoftAccountPurchasing)))
			{
				return false;
			}
			if (HasEnableSmartDeckCompletion != guardianVars.HasEnableSmartDeckCompletion || (HasEnableSmartDeckCompletion && !EnableSmartDeckCompletion.Equals(guardianVars.EnableSmartDeckCompletion)))
			{
				return false;
			}
			if (HasNumClassicPacksUntilDeprioritize != guardianVars.HasNumClassicPacksUntilDeprioritize || (HasNumClassicPacksUntilDeprioritize && !NumClassicPacksUntilDeprioritize.Equals(guardianVars.NumClassicPacksUntilDeprioritize)))
			{
				return false;
			}
			if (HasAllowOfflineClientActivityIos != guardianVars.HasAllowOfflineClientActivityIos || (HasAllowOfflineClientActivityIos && !AllowOfflineClientActivityIos.Equals(guardianVars.AllowOfflineClientActivityIos)))
			{
				return false;
			}
			if (HasAllowOfflineClientActivityAndroid != guardianVars.HasAllowOfflineClientActivityAndroid || (HasAllowOfflineClientActivityAndroid && !AllowOfflineClientActivityAndroid.Equals(guardianVars.AllowOfflineClientActivityAndroid)))
			{
				return false;
			}
			if (HasAllowOfflineClientActivityDesktop != guardianVars.HasAllowOfflineClientActivityDesktop || (HasAllowOfflineClientActivityDesktop && !AllowOfflineClientActivityDesktop.Equals(guardianVars.AllowOfflineClientActivityDesktop)))
			{
				return false;
			}
			if (HasAllowOfflineClientDeckDeletion != guardianVars.HasAllowOfflineClientDeckDeletion || (HasAllowOfflineClientDeckDeletion && !AllowOfflineClientDeckDeletion.Equals(guardianVars.AllowOfflineClientDeckDeletion)))
			{
				return false;
			}
			if (HasBattlegrounds != guardianVars.HasBattlegrounds || (HasBattlegrounds && !Battlegrounds.Equals(guardianVars.Battlegrounds)))
			{
				return false;
			}
			if (HasBattlegroundsFriendlyChallenge != guardianVars.HasBattlegroundsFriendlyChallenge || (HasBattlegroundsFriendlyChallenge && !BattlegroundsFriendlyChallenge.Equals(guardianVars.BattlegroundsFriendlyChallenge)))
			{
				return false;
			}
			if (HasSimpleCheckoutIos != guardianVars.HasSimpleCheckoutIos || (HasSimpleCheckoutIos && !SimpleCheckoutIos.Equals(guardianVars.SimpleCheckoutIos)))
			{
				return false;
			}
			if (HasSimpleCheckoutAndroidAmazon != guardianVars.HasSimpleCheckoutAndroidAmazon || (HasSimpleCheckoutAndroidAmazon && !SimpleCheckoutAndroidAmazon.Equals(guardianVars.SimpleCheckoutAndroidAmazon)))
			{
				return false;
			}
			if (HasSimpleCheckoutAndroidGoogle != guardianVars.HasSimpleCheckoutAndroidGoogle || (HasSimpleCheckoutAndroidGoogle && !SimpleCheckoutAndroidGoogle.Equals(guardianVars.SimpleCheckoutAndroidGoogle)))
			{
				return false;
			}
			if (HasSimpleCheckoutAndroidGlobal != guardianVars.HasSimpleCheckoutAndroidGlobal || (HasSimpleCheckoutAndroidGlobal && !SimpleCheckoutAndroidGlobal.Equals(guardianVars.SimpleCheckoutAndroidGlobal)))
			{
				return false;
			}
			if (HasSimpleCheckoutWin != guardianVars.HasSimpleCheckoutWin || (HasSimpleCheckoutWin && !SimpleCheckoutWin.Equals(guardianVars.SimpleCheckoutWin)))
			{
				return false;
			}
			if (HasSimpleCheckoutMac != guardianVars.HasSimpleCheckoutMac || (HasSimpleCheckoutMac && !SimpleCheckoutMac.Equals(guardianVars.SimpleCheckoutMac)))
			{
				return false;
			}
			if (HasBattlegroundsEarlyAccessLicense != guardianVars.HasBattlegroundsEarlyAccessLicense || (HasBattlegroundsEarlyAccessLicense && !BattlegroundsEarlyAccessLicense.Equals(guardianVars.BattlegroundsEarlyAccessLicense)))
			{
				return false;
			}
			if (HasVirtualCurrencyEnabled != guardianVars.HasVirtualCurrencyEnabled || (HasVirtualCurrencyEnabled && !VirtualCurrencyEnabled.Equals(guardianVars.VirtualCurrencyEnabled)))
			{
				return false;
			}
			if (HasBattlegroundsTutorial != guardianVars.HasBattlegroundsTutorial || (HasBattlegroundsTutorial && !BattlegroundsTutorial.Equals(guardianVars.BattlegroundsTutorial)))
			{
				return false;
			}
			if (HasVintageStoreEnabled != guardianVars.HasVintageStoreEnabled || (HasVintageStoreEnabled && !VintageStoreEnabled.Equals(guardianVars.VintageStoreEnabled)))
			{
				return false;
			}
			if (HasBoosterRotatingSoonWarnDaysWithoutSale != guardianVars.HasBoosterRotatingSoonWarnDaysWithoutSale || (HasBoosterRotatingSoonWarnDaysWithoutSale && !BoosterRotatingSoonWarnDaysWithoutSale.Equals(guardianVars.BoosterRotatingSoonWarnDaysWithoutSale)))
			{
				return false;
			}
			if (HasBoosterRotatingSoonWarnDaysWithSale != guardianVars.HasBoosterRotatingSoonWarnDaysWithSale || (HasBoosterRotatingSoonWarnDaysWithSale && !BoosterRotatingSoonWarnDaysWithSale.Equals(guardianVars.BoosterRotatingSoonWarnDaysWithSale)))
			{
				return false;
			}
			if (HasBattlegroundsMaxRankedPartySize != guardianVars.HasBattlegroundsMaxRankedPartySize || (HasBattlegroundsMaxRankedPartySize && !BattlegroundsMaxRankedPartySize.Equals(guardianVars.BattlegroundsMaxRankedPartySize)))
			{
				return false;
			}
			if (HasDeckReordering != guardianVars.HasDeckReordering || (HasDeckReordering && !DeckReordering.Equals(guardianVars.DeckReordering)))
			{
				return false;
			}
			if (HasProgressionEnabled != guardianVars.HasProgressionEnabled || (HasProgressionEnabled && !ProgressionEnabled.Equals(guardianVars.ProgressionEnabled)))
			{
				return false;
			}
			if (HasPvpdrClosedToNewSessionsSeconds != guardianVars.HasPvpdrClosedToNewSessionsSeconds || (HasPvpdrClosedToNewSessionsSeconds && !PvpdrClosedToNewSessionsSeconds.Equals(guardianVars.PvpdrClosedToNewSessionsSeconds)))
			{
				return false;
			}
			if (HasDuels != guardianVars.HasDuels || (HasDuels && !Duels.Equals(guardianVars.Duels)))
			{
				return false;
			}
			if (HasDuelsEarlyAccessLicense != guardianVars.HasDuelsEarlyAccessLicense || (HasDuelsEarlyAccessLicense && !DuelsEarlyAccessLicense.Equals(guardianVars.DuelsEarlyAccessLicense)))
			{
				return false;
			}
			if (HasPaidDuels != guardianVars.HasPaidDuels || (HasPaidDuels && !PaidDuels.Equals(guardianVars.PaidDuels)))
			{
				return false;
			}
			if (HasAllowLiveFpsGathering != guardianVars.HasAllowLiveFpsGathering || (HasAllowLiveFpsGathering && !AllowLiveFpsGathering.Equals(guardianVars.AllowLiveFpsGathering)))
			{
				return false;
			}
			if (HasJournalButtonDisabled != guardianVars.HasJournalButtonDisabled || (HasJournalButtonDisabled && !JournalButtonDisabled.Equals(guardianVars.JournalButtonDisabled)))
			{
				return false;
			}
			if (HasAchievementToastDisabled != guardianVars.HasAchievementToastDisabled || (HasAchievementToastDisabled && !AchievementToastDisabled.Equals(guardianVars.AchievementToastDisabled)))
			{
				return false;
			}
			if (HasCheckForNewQuestsIntervalJitterSecs != guardianVars.HasCheckForNewQuestsIntervalJitterSecs || (HasCheckForNewQuestsIntervalJitterSecs && !CheckForNewQuestsIntervalJitterSecs.Equals(guardianVars.CheckForNewQuestsIntervalJitterSecs)))
			{
				return false;
			}
			if (HasRankedStandard != guardianVars.HasRankedStandard || (HasRankedStandard && !RankedStandard.Equals(guardianVars.RankedStandard)))
			{
				return false;
			}
			if (HasRankedWild != guardianVars.HasRankedWild || (HasRankedWild && !RankedWild.Equals(guardianVars.RankedWild)))
			{
				return false;
			}
			if (HasRankedClassic != guardianVars.HasRankedClassic || (HasRankedClassic && !RankedClassic.Equals(guardianVars.RankedClassic)))
			{
				return false;
			}
			if (HasRankedNewPlayer != guardianVars.HasRankedNewPlayer || (HasRankedNewPlayer && !RankedNewPlayer.Equals(guardianVars.RankedNewPlayer)))
			{
				return false;
			}
			if (HasContentstackEnabled != guardianVars.HasContentstackEnabled || (HasContentstackEnabled && !ContentstackEnabled.Equals(guardianVars.ContentstackEnabled)))
			{
				return false;
			}
			if (HasEndOfTurnToastPauseBufferSecs != guardianVars.HasEndOfTurnToastPauseBufferSecs || (HasEndOfTurnToastPauseBufferSecs && !EndOfTurnToastPauseBufferSecs.Equals(guardianVars.EndOfTurnToastPauseBufferSecs)))
			{
				return false;
			}
			if (HasAppRatingEnabled != guardianVars.HasAppRatingEnabled || (HasAppRatingEnabled && !AppRatingEnabled.Equals(guardianVars.AppRatingEnabled)))
			{
				return false;
			}
			if (HasAppRatingSamplingPercentage != guardianVars.HasAppRatingSamplingPercentage || (HasAppRatingSamplingPercentage && !AppRatingSamplingPercentage.Equals(guardianVars.AppRatingSamplingPercentage)))
			{
				return false;
			}
			if (HasBuyCardBacksFromCollectionManagerEnabled != guardianVars.HasBuyCardBacksFromCollectionManagerEnabled || (HasBuyCardBacksFromCollectionManagerEnabled && !BuyCardBacksFromCollectionManagerEnabled.Equals(guardianVars.BuyCardBacksFromCollectionManagerEnabled)))
			{
				return false;
			}
			if (HasBuyHeroSkinsFromCollectionManagerEnabled != guardianVars.HasBuyHeroSkinsFromCollectionManagerEnabled || (HasBuyHeroSkinsFromCollectionManagerEnabled && !BuyHeroSkinsFromCollectionManagerEnabled.Equals(guardianVars.BuyHeroSkinsFromCollectionManagerEnabled)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GuardianVars Deserialize(Stream stream, GuardianVars instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GuardianVars DeserializeLengthDelimited(Stream stream)
		{
			GuardianVars guardianVars = new GuardianVars();
			DeserializeLengthDelimited(stream, guardianVars);
			return guardianVars;
		}

		public static GuardianVars DeserializeLengthDelimited(Stream stream, GuardianVars instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GuardianVars Deserialize(Stream stream, GuardianVars instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 8:
					instance.Tourney = ProtocolParser.ReadBool(stream);
					continue;
				case 16:
					instance.Practice = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.Casual = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.Forge = ProtocolParser.ReadBool(stream);
					continue;
				case 40:
					instance.Friendly = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.Manager = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
					instance.Crafting = ProtocolParser.ReadBool(stream);
					continue;
				case 64:
					instance.Hunter = ProtocolParser.ReadBool(stream);
					continue;
				case 72:
					instance.Mage = ProtocolParser.ReadBool(stream);
					continue;
				case 80:
					instance.Paladin = ProtocolParser.ReadBool(stream);
					continue;
				case 88:
					instance.Priest = ProtocolParser.ReadBool(stream);
					continue;
				case 96:
					instance.Rogue = ProtocolParser.ReadBool(stream);
					continue;
				case 104:
					instance.Shaman = ProtocolParser.ReadBool(stream);
					continue;
				case 112:
					instance.Warlock = ProtocolParser.ReadBool(stream);
					continue;
				case 120:
					instance.Warrior = ProtocolParser.ReadBool(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 16u:
						if (key.WireType == Wire.Varint)
						{
							instance.ShowUserUI = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 17u:
						if (key.WireType == Wire.Varint)
						{
							instance.Store = ProtocolParser.ReadBool(stream);
						}
						break;
					case 18u:
						if (key.WireType == Wire.Varint)
						{
							instance.BattlePay = ProtocolParser.ReadBool(stream);
						}
						break;
					case 19u:
						if (key.WireType == Wire.Varint)
						{
							instance.BuyWithGold = ProtocolParser.ReadBool(stream);
						}
						break;
					case 20u:
						if (key.WireType == Wire.Varint)
						{
							instance.TavernBrawl = ProtocolParser.ReadBool(stream);
						}
						break;
					case 21u:
						if (key.WireType == Wire.Varint)
						{
							instance.ClientOptionsUpdateIntervalSeconds = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 22u:
						if (key.WireType == Wire.Varint)
						{
							instance.CaisEnabledNonMobile = ProtocolParser.ReadBool(stream);
						}
						break;
					case 23u:
						if (key.WireType == Wire.Varint)
						{
							instance.CaisEnabledMobileChina = ProtocolParser.ReadBool(stream);
						}
						break;
					case 24u:
						if (key.WireType == Wire.Varint)
						{
							instance.CaisEnabledMobileSouthKorea = ProtocolParser.ReadBool(stream);
						}
						break;
					case 25u:
						if (key.WireType == Wire.Varint)
						{
							instance.SendTelemetryPresence = ProtocolParser.ReadBool(stream);
						}
						break;
					case 26u:
						if (key.WireType == Wire.Varint)
						{
							instance.FriendWeekConcederMaxDefense = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 27u:
						if (key.WireType == Wire.Varint)
						{
							instance.WinsPerGold = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 28u:
						if (key.WireType == Wire.Varint)
						{
							instance.GoldPerReward = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 29u:
						if (key.WireType == Wire.Varint)
						{
							instance.MaxGoldPerDay = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 30u:
						if (key.WireType == Wire.Varint)
						{
							instance.XpSoloLimit = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 31u:
						if (key.WireType == Wire.Varint)
						{
							instance.MaxHeroLevel = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 32u:
						if (key.WireType == Wire.Fixed32)
						{
							instance.EventTimingMod = binaryReader.ReadSingle();
						}
						break;
					case 33u:
						if (key.WireType == Wire.Varint)
						{
							instance.FsgEnabled = ProtocolParser.ReadBool(stream);
						}
						break;
					case 34u:
						if (key.WireType == Wire.Varint)
						{
							instance.FsgAutoCheckinEnabled = ProtocolParser.ReadBool(stream);
						}
						break;
					case 35u:
						if (key.WireType == Wire.Varint)
						{
							instance.FriendWeekConcededGameMinTotalTurns = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 36u:
						if (key.WireType == Wire.Varint)
						{
							instance.FriendWeekAllowsTavernBrawlRecordUpdate = ProtocolParser.ReadBool(stream);
						}
						break;
					case 37u:
						if (key.WireType == Wire.Varint)
						{
							instance.FsgShowBetaLabel = ProtocolParser.ReadBool(stream);
						}
						break;
					case 38u:
						if (key.WireType == Wire.Varint)
						{
							instance.FsgFriendListPatronCountLimit = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 39u:
						if (key.WireType == Wire.Varint)
						{
							instance.ArenaClosedToNewSessionsSeconds = ProtocolParser.ReadUInt32(stream);
						}
						break;
					case 40u:
						if (key.WireType == Wire.Varint)
						{
							instance.FsgLoginScanEnabled = ProtocolParser.ReadBool(stream);
						}
						break;
					case 41u:
						if (key.WireType == Wire.Varint)
						{
							instance.FsgMaxPresencePubscribedPatronCount = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 42u:
						if (key.WireType == Wire.Varint)
						{
							instance.QuickPackOpeningAllowed = ProtocolParser.ReadBool(stream);
						}
						break;
					case 43u:
						if (key.WireType == Wire.Varint)
						{
							instance.AllowIosHighres = ProtocolParser.ReadBool(stream);
						}
						break;
					case 44u:
						if (key.WireType == Wire.Varint)
						{
							instance.SimpleCheckout = ProtocolParser.ReadBool(stream);
						}
						break;
					case 45u:
						if (key.WireType == Wire.Varint)
						{
							instance.DeckCompletionGetPlayerCollectionFromClient = ProtocolParser.ReadBool(stream);
						}
						break;
					case 46u:
						if (key.WireType == Wire.Varint)
						{
							instance.SoftAccountPurchasing = ProtocolParser.ReadBool(stream);
						}
						break;
					case 47u:
						if (key.WireType == Wire.Varint)
						{
							instance.EnableSmartDeckCompletion = ProtocolParser.ReadBool(stream);
						}
						break;
					case 48u:
						if (key.WireType == Wire.Varint)
						{
							instance.NumClassicPacksUntilDeprioritize = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 49u:
						if (key.WireType == Wire.Varint)
						{
							instance.AllowOfflineClientActivityIos = ProtocolParser.ReadBool(stream);
						}
						break;
					case 50u:
						if (key.WireType == Wire.Varint)
						{
							instance.AllowOfflineClientActivityAndroid = ProtocolParser.ReadBool(stream);
						}
						break;
					case 51u:
						if (key.WireType == Wire.Varint)
						{
							instance.AllowOfflineClientActivityDesktop = ProtocolParser.ReadBool(stream);
						}
						break;
					case 52u:
						if (key.WireType == Wire.Varint)
						{
							instance.AllowOfflineClientDeckDeletion = ProtocolParser.ReadBool(stream);
						}
						break;
					case 53u:
						if (key.WireType == Wire.Varint)
						{
							instance.Battlegrounds = ProtocolParser.ReadBool(stream);
						}
						break;
					case 54u:
						if (key.WireType == Wire.Varint)
						{
							instance.BattlegroundsFriendlyChallenge = ProtocolParser.ReadBool(stream);
						}
						break;
					case 55u:
						if (key.WireType == Wire.Varint)
						{
							instance.SimpleCheckoutIos = ProtocolParser.ReadBool(stream);
						}
						break;
					case 56u:
						if (key.WireType == Wire.Varint)
						{
							instance.SimpleCheckoutAndroidAmazon = ProtocolParser.ReadBool(stream);
						}
						break;
					case 57u:
						if (key.WireType == Wire.Varint)
						{
							instance.SimpleCheckoutAndroidGoogle = ProtocolParser.ReadBool(stream);
						}
						break;
					case 58u:
						if (key.WireType == Wire.Varint)
						{
							instance.SimpleCheckoutAndroidGlobal = ProtocolParser.ReadBool(stream);
						}
						break;
					case 59u:
						if (key.WireType == Wire.Varint)
						{
							instance.SimpleCheckoutWin = ProtocolParser.ReadBool(stream);
						}
						break;
					case 60u:
						if (key.WireType == Wire.Varint)
						{
							instance.SimpleCheckoutMac = ProtocolParser.ReadBool(stream);
						}
						break;
					case 61u:
						if (key.WireType == Wire.Varint)
						{
							instance.BattlegroundsEarlyAccessLicense = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 62u:
						if (key.WireType == Wire.Varint)
						{
							instance.VirtualCurrencyEnabled = ProtocolParser.ReadBool(stream);
						}
						break;
					case 63u:
						if (key.WireType == Wire.Varint)
						{
							instance.BattlegroundsTutorial = ProtocolParser.ReadBool(stream);
						}
						break;
					case 64u:
						if (key.WireType == Wire.Varint)
						{
							instance.VintageStoreEnabled = ProtocolParser.ReadBool(stream);
						}
						break;
					case 65u:
						if (key.WireType == Wire.Varint)
						{
							instance.BoosterRotatingSoonWarnDaysWithoutSale = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 66u:
						if (key.WireType == Wire.Varint)
						{
							instance.BoosterRotatingSoonWarnDaysWithSale = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 67u:
						if (key.WireType == Wire.Varint)
						{
							instance.BattlegroundsMaxRankedPartySize = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 68u:
						if (key.WireType == Wire.Varint)
						{
							instance.DeckReordering = ProtocolParser.ReadBool(stream);
						}
						break;
					case 69u:
						if (key.WireType == Wire.Varint)
						{
							instance.ProgressionEnabled = ProtocolParser.ReadBool(stream);
						}
						break;
					case 70u:
						if (key.WireType == Wire.Varint)
						{
							instance.PvpdrClosedToNewSessionsSeconds = ProtocolParser.ReadUInt32(stream);
						}
						break;
					case 71u:
						if (key.WireType == Wire.Varint)
						{
							instance.Duels = ProtocolParser.ReadBool(stream);
						}
						break;
					case 72u:
						if (key.WireType == Wire.Varint)
						{
							instance.DuelsEarlyAccessLicense = ProtocolParser.ReadUInt32(stream);
						}
						break;
					case 73u:
						if (key.WireType == Wire.Varint)
						{
							instance.PaidDuels = ProtocolParser.ReadBool(stream);
						}
						break;
					case 74u:
						if (key.WireType == Wire.Varint)
						{
							instance.AllowLiveFpsGathering = ProtocolParser.ReadBool(stream);
						}
						break;
					case 75u:
						if (key.WireType == Wire.Varint)
						{
							instance.JournalButtonDisabled = ProtocolParser.ReadBool(stream);
						}
						break;
					case 76u:
						if (key.WireType == Wire.Varint)
						{
							instance.AchievementToastDisabled = ProtocolParser.ReadBool(stream);
						}
						break;
					case 77u:
						if (key.WireType == Wire.Fixed32)
						{
							instance.CheckForNewQuestsIntervalJitterSecs = binaryReader.ReadSingle();
						}
						break;
					case 78u:
						if (key.WireType == Wire.Varint)
						{
							instance.RankedStandard = ProtocolParser.ReadBool(stream);
						}
						break;
					case 79u:
						if (key.WireType == Wire.Varint)
						{
							instance.RankedWild = ProtocolParser.ReadBool(stream);
						}
						break;
					case 80u:
						if (key.WireType == Wire.Varint)
						{
							instance.RankedClassic = ProtocolParser.ReadBool(stream);
						}
						break;
					case 81u:
						if (key.WireType == Wire.Varint)
						{
							instance.RankedNewPlayer = ProtocolParser.ReadBool(stream);
						}
						break;
					case 82u:
						if (key.WireType == Wire.Varint)
						{
							instance.ContentstackEnabled = ProtocolParser.ReadBool(stream);
						}
						break;
					case 83u:
						if (key.WireType == Wire.Fixed32)
						{
							instance.EndOfTurnToastPauseBufferSecs = binaryReader.ReadSingle();
						}
						break;
					case 84u:
						if (key.WireType == Wire.Varint)
						{
							instance.AppRatingEnabled = ProtocolParser.ReadBool(stream);
						}
						break;
					case 85u:
						if (key.WireType == Wire.Fixed32)
						{
							instance.AppRatingSamplingPercentage = binaryReader.ReadSingle();
						}
						break;
					case 86u:
						if (key.WireType == Wire.Varint)
						{
							instance.BuyCardBacksFromCollectionManagerEnabled = ProtocolParser.ReadBool(stream);
						}
						break;
					case 87u:
						if (key.WireType == Wire.Varint)
						{
							instance.BuyHeroSkinsFromCollectionManagerEnabled = ProtocolParser.ReadBool(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GuardianVars instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasTourney)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.Tourney);
			}
			if (instance.HasPractice)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Practice);
			}
			if (instance.HasCasual)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Casual);
			}
			if (instance.HasForge)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.Forge);
			}
			if (instance.HasFriendly)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.Friendly);
			}
			if (instance.HasManager)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.Manager);
			}
			if (instance.HasCrafting)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.Crafting);
			}
			if (instance.HasHunter)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.Hunter);
			}
			if (instance.HasMage)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.Mage);
			}
			if (instance.HasPaladin)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteBool(stream, instance.Paladin);
			}
			if (instance.HasPriest)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteBool(stream, instance.Priest);
			}
			if (instance.HasRogue)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteBool(stream, instance.Rogue);
			}
			if (instance.HasShaman)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteBool(stream, instance.Shaman);
			}
			if (instance.HasWarlock)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteBool(stream, instance.Warlock);
			}
			if (instance.HasWarrior)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteBool(stream, instance.Warrior);
			}
			if (instance.HasShowUserUI)
			{
				stream.WriteByte(128);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ShowUserUI);
			}
			if (instance.HasStore)
			{
				stream.WriteByte(136);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.Store);
			}
			if (instance.HasBattlePay)
			{
				stream.WriteByte(144);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.BattlePay);
			}
			if (instance.HasBuyWithGold)
			{
				stream.WriteByte(152);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.BuyWithGold);
			}
			if (instance.HasTavernBrawl)
			{
				stream.WriteByte(160);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.TavernBrawl);
			}
			if (instance.HasClientOptionsUpdateIntervalSeconds)
			{
				stream.WriteByte(168);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientOptionsUpdateIntervalSeconds);
			}
			if (instance.HasCaisEnabledNonMobile)
			{
				stream.WriteByte(176);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.CaisEnabledNonMobile);
			}
			if (instance.HasCaisEnabledMobileChina)
			{
				stream.WriteByte(184);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.CaisEnabledMobileChina);
			}
			if (instance.HasCaisEnabledMobileSouthKorea)
			{
				stream.WriteByte(192);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.CaisEnabledMobileSouthKorea);
			}
			if (instance.HasSendTelemetryPresence)
			{
				stream.WriteByte(200);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.SendTelemetryPresence);
			}
			if (instance.HasFriendWeekConcederMaxDefense)
			{
				stream.WriteByte(208);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FriendWeekConcederMaxDefense);
			}
			if (instance.HasWinsPerGold)
			{
				stream.WriteByte(216);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.WinsPerGold);
			}
			if (instance.HasGoldPerReward)
			{
				stream.WriteByte(224);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GoldPerReward);
			}
			if (instance.HasMaxGoldPerDay)
			{
				stream.WriteByte(232);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxGoldPerDay);
			}
			if (instance.HasXpSoloLimit)
			{
				stream.WriteByte(240);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.XpSoloLimit);
			}
			if (instance.HasMaxHeroLevel)
			{
				stream.WriteByte(248);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxHeroLevel);
			}
			if (instance.HasEventTimingMod)
			{
				stream.WriteByte(133);
				stream.WriteByte(2);
				binaryWriter.Write(instance.EventTimingMod);
			}
			if (instance.HasFsgEnabled)
			{
				stream.WriteByte(136);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.FsgEnabled);
			}
			if (instance.HasFsgAutoCheckinEnabled)
			{
				stream.WriteByte(144);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.FsgAutoCheckinEnabled);
			}
			if (instance.HasFriendWeekConcededGameMinTotalTurns)
			{
				stream.WriteByte(152);
				stream.WriteByte(2);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FriendWeekConcededGameMinTotalTurns);
			}
			if (instance.HasFriendWeekAllowsTavernBrawlRecordUpdate)
			{
				stream.WriteByte(160);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.FriendWeekAllowsTavernBrawlRecordUpdate);
			}
			if (instance.HasFsgShowBetaLabel)
			{
				stream.WriteByte(168);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.FsgShowBetaLabel);
			}
			if (instance.HasFsgFriendListPatronCountLimit)
			{
				stream.WriteByte(176);
				stream.WriteByte(2);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgFriendListPatronCountLimit);
			}
			if (instance.HasArenaClosedToNewSessionsSeconds)
			{
				stream.WriteByte(184);
				stream.WriteByte(2);
				ProtocolParser.WriteUInt32(stream, instance.ArenaClosedToNewSessionsSeconds);
			}
			if (instance.HasFsgLoginScanEnabled)
			{
				stream.WriteByte(192);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.FsgLoginScanEnabled);
			}
			if (instance.HasFsgMaxPresencePubscribedPatronCount)
			{
				stream.WriteByte(200);
				stream.WriteByte(2);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgMaxPresencePubscribedPatronCount);
			}
			if (instance.HasQuickPackOpeningAllowed)
			{
				stream.WriteByte(208);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.QuickPackOpeningAllowed);
			}
			if (instance.HasAllowIosHighres)
			{
				stream.WriteByte(216);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.AllowIosHighres);
			}
			if (instance.HasSimpleCheckout)
			{
				stream.WriteByte(224);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.SimpleCheckout);
			}
			if (instance.HasDeckCompletionGetPlayerCollectionFromClient)
			{
				stream.WriteByte(232);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.DeckCompletionGetPlayerCollectionFromClient);
			}
			if (instance.HasSoftAccountPurchasing)
			{
				stream.WriteByte(240);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.SoftAccountPurchasing);
			}
			if (instance.HasEnableSmartDeckCompletion)
			{
				stream.WriteByte(248);
				stream.WriteByte(2);
				ProtocolParser.WriteBool(stream, instance.EnableSmartDeckCompletion);
			}
			if (instance.HasNumClassicPacksUntilDeprioritize)
			{
				stream.WriteByte(128);
				stream.WriteByte(3);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.NumClassicPacksUntilDeprioritize);
			}
			if (instance.HasAllowOfflineClientActivityIos)
			{
				stream.WriteByte(136);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.AllowOfflineClientActivityIos);
			}
			if (instance.HasAllowOfflineClientActivityAndroid)
			{
				stream.WriteByte(144);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.AllowOfflineClientActivityAndroid);
			}
			if (instance.HasAllowOfflineClientActivityDesktop)
			{
				stream.WriteByte(152);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.AllowOfflineClientActivityDesktop);
			}
			if (instance.HasAllowOfflineClientDeckDeletion)
			{
				stream.WriteByte(160);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.AllowOfflineClientDeckDeletion);
			}
			if (instance.HasBattlegrounds)
			{
				stream.WriteByte(168);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.Battlegrounds);
			}
			if (instance.HasBattlegroundsFriendlyChallenge)
			{
				stream.WriteByte(176);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.BattlegroundsFriendlyChallenge);
			}
			if (instance.HasSimpleCheckoutIos)
			{
				stream.WriteByte(184);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.SimpleCheckoutIos);
			}
			if (instance.HasSimpleCheckoutAndroidAmazon)
			{
				stream.WriteByte(192);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.SimpleCheckoutAndroidAmazon);
			}
			if (instance.HasSimpleCheckoutAndroidGoogle)
			{
				stream.WriteByte(200);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.SimpleCheckoutAndroidGoogle);
			}
			if (instance.HasSimpleCheckoutAndroidGlobal)
			{
				stream.WriteByte(208);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.SimpleCheckoutAndroidGlobal);
			}
			if (instance.HasSimpleCheckoutWin)
			{
				stream.WriteByte(216);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.SimpleCheckoutWin);
			}
			if (instance.HasSimpleCheckoutMac)
			{
				stream.WriteByte(224);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.SimpleCheckoutMac);
			}
			if (instance.HasBattlegroundsEarlyAccessLicense)
			{
				stream.WriteByte(232);
				stream.WriteByte(3);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BattlegroundsEarlyAccessLicense);
			}
			if (instance.HasVirtualCurrencyEnabled)
			{
				stream.WriteByte(240);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.VirtualCurrencyEnabled);
			}
			if (instance.HasBattlegroundsTutorial)
			{
				stream.WriteByte(248);
				stream.WriteByte(3);
				ProtocolParser.WriteBool(stream, instance.BattlegroundsTutorial);
			}
			if (instance.HasVintageStoreEnabled)
			{
				stream.WriteByte(128);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.VintageStoreEnabled);
			}
			if (instance.HasBoosterRotatingSoonWarnDaysWithoutSale)
			{
				stream.WriteByte(136);
				stream.WriteByte(4);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BoosterRotatingSoonWarnDaysWithoutSale);
			}
			if (instance.HasBoosterRotatingSoonWarnDaysWithSale)
			{
				stream.WriteByte(144);
				stream.WriteByte(4);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BoosterRotatingSoonWarnDaysWithSale);
			}
			if (instance.HasBattlegroundsMaxRankedPartySize)
			{
				stream.WriteByte(152);
				stream.WriteByte(4);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BattlegroundsMaxRankedPartySize);
			}
			if (instance.HasDeckReordering)
			{
				stream.WriteByte(160);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.DeckReordering);
			}
			if (instance.HasProgressionEnabled)
			{
				stream.WriteByte(168);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.ProgressionEnabled);
			}
			if (instance.HasPvpdrClosedToNewSessionsSeconds)
			{
				stream.WriteByte(176);
				stream.WriteByte(4);
				ProtocolParser.WriteUInt32(stream, instance.PvpdrClosedToNewSessionsSeconds);
			}
			if (instance.HasDuels)
			{
				stream.WriteByte(184);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.Duels);
			}
			if (instance.HasDuelsEarlyAccessLicense)
			{
				stream.WriteByte(192);
				stream.WriteByte(4);
				ProtocolParser.WriteUInt32(stream, instance.DuelsEarlyAccessLicense);
			}
			if (instance.HasPaidDuels)
			{
				stream.WriteByte(200);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.PaidDuels);
			}
			if (instance.HasAllowLiveFpsGathering)
			{
				stream.WriteByte(208);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.AllowLiveFpsGathering);
			}
			if (instance.HasJournalButtonDisabled)
			{
				stream.WriteByte(216);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.JournalButtonDisabled);
			}
			if (instance.HasAchievementToastDisabled)
			{
				stream.WriteByte(224);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.AchievementToastDisabled);
			}
			if (instance.HasCheckForNewQuestsIntervalJitterSecs)
			{
				stream.WriteByte(237);
				stream.WriteByte(4);
				binaryWriter.Write(instance.CheckForNewQuestsIntervalJitterSecs);
			}
			if (instance.HasRankedStandard)
			{
				stream.WriteByte(240);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.RankedStandard);
			}
			if (instance.HasRankedWild)
			{
				stream.WriteByte(248);
				stream.WriteByte(4);
				ProtocolParser.WriteBool(stream, instance.RankedWild);
			}
			if (instance.HasRankedClassic)
			{
				stream.WriteByte(128);
				stream.WriteByte(5);
				ProtocolParser.WriteBool(stream, instance.RankedClassic);
			}
			if (instance.HasRankedNewPlayer)
			{
				stream.WriteByte(136);
				stream.WriteByte(5);
				ProtocolParser.WriteBool(stream, instance.RankedNewPlayer);
			}
			if (instance.HasContentstackEnabled)
			{
				stream.WriteByte(144);
				stream.WriteByte(5);
				ProtocolParser.WriteBool(stream, instance.ContentstackEnabled);
			}
			if (instance.HasEndOfTurnToastPauseBufferSecs)
			{
				stream.WriteByte(157);
				stream.WriteByte(5);
				binaryWriter.Write(instance.EndOfTurnToastPauseBufferSecs);
			}
			if (instance.HasAppRatingEnabled)
			{
				stream.WriteByte(160);
				stream.WriteByte(5);
				ProtocolParser.WriteBool(stream, instance.AppRatingEnabled);
			}
			if (instance.HasAppRatingSamplingPercentage)
			{
				stream.WriteByte(173);
				stream.WriteByte(5);
				binaryWriter.Write(instance.AppRatingSamplingPercentage);
			}
			if (instance.HasBuyCardBacksFromCollectionManagerEnabled)
			{
				stream.WriteByte(176);
				stream.WriteByte(5);
				ProtocolParser.WriteBool(stream, instance.BuyCardBacksFromCollectionManagerEnabled);
			}
			if (instance.HasBuyHeroSkinsFromCollectionManagerEnabled)
			{
				stream.WriteByte(184);
				stream.WriteByte(5);
				ProtocolParser.WriteBool(stream, instance.BuyHeroSkinsFromCollectionManagerEnabled);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasTourney)
			{
				num++;
				num++;
			}
			if (HasPractice)
			{
				num++;
				num++;
			}
			if (HasCasual)
			{
				num++;
				num++;
			}
			if (HasForge)
			{
				num++;
				num++;
			}
			if (HasFriendly)
			{
				num++;
				num++;
			}
			if (HasManager)
			{
				num++;
				num++;
			}
			if (HasCrafting)
			{
				num++;
				num++;
			}
			if (HasHunter)
			{
				num++;
				num++;
			}
			if (HasMage)
			{
				num++;
				num++;
			}
			if (HasPaladin)
			{
				num++;
				num++;
			}
			if (HasPriest)
			{
				num++;
				num++;
			}
			if (HasRogue)
			{
				num++;
				num++;
			}
			if (HasShaman)
			{
				num++;
				num++;
			}
			if (HasWarlock)
			{
				num++;
				num++;
			}
			if (HasWarrior)
			{
				num++;
				num++;
			}
			if (HasShowUserUI)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)ShowUserUI);
			}
			if (HasStore)
			{
				num += 2;
				num++;
			}
			if (HasBattlePay)
			{
				num += 2;
				num++;
			}
			if (HasBuyWithGold)
			{
				num += 2;
				num++;
			}
			if (HasTavernBrawl)
			{
				num += 2;
				num++;
			}
			if (HasClientOptionsUpdateIntervalSeconds)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)ClientOptionsUpdateIntervalSeconds);
			}
			if (HasCaisEnabledNonMobile)
			{
				num += 2;
				num++;
			}
			if (HasCaisEnabledMobileChina)
			{
				num += 2;
				num++;
			}
			if (HasCaisEnabledMobileSouthKorea)
			{
				num += 2;
				num++;
			}
			if (HasSendTelemetryPresence)
			{
				num += 2;
				num++;
			}
			if (HasFriendWeekConcederMaxDefense)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)FriendWeekConcederMaxDefense);
			}
			if (HasWinsPerGold)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)WinsPerGold);
			}
			if (HasGoldPerReward)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)GoldPerReward);
			}
			if (HasMaxGoldPerDay)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)MaxGoldPerDay);
			}
			if (HasXpSoloLimit)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)XpSoloLimit);
			}
			if (HasMaxHeroLevel)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)MaxHeroLevel);
			}
			if (HasEventTimingMod)
			{
				num += 2;
				num += 4;
			}
			if (HasFsgEnabled)
			{
				num += 2;
				num++;
			}
			if (HasFsgAutoCheckinEnabled)
			{
				num += 2;
				num++;
			}
			if (HasFriendWeekConcededGameMinTotalTurns)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)FriendWeekConcededGameMinTotalTurns);
			}
			if (HasFriendWeekAllowsTavernBrawlRecordUpdate)
			{
				num += 2;
				num++;
			}
			if (HasFsgShowBetaLabel)
			{
				num += 2;
				num++;
			}
			if (HasFsgFriendListPatronCountLimit)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)FsgFriendListPatronCountLimit);
			}
			if (HasArenaClosedToNewSessionsSeconds)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt32(ArenaClosedToNewSessionsSeconds);
			}
			if (HasFsgLoginScanEnabled)
			{
				num += 2;
				num++;
			}
			if (HasFsgMaxPresencePubscribedPatronCount)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)FsgMaxPresencePubscribedPatronCount);
			}
			if (HasQuickPackOpeningAllowed)
			{
				num += 2;
				num++;
			}
			if (HasAllowIosHighres)
			{
				num += 2;
				num++;
			}
			if (HasSimpleCheckout)
			{
				num += 2;
				num++;
			}
			if (HasDeckCompletionGetPlayerCollectionFromClient)
			{
				num += 2;
				num++;
			}
			if (HasSoftAccountPurchasing)
			{
				num += 2;
				num++;
			}
			if (HasEnableSmartDeckCompletion)
			{
				num += 2;
				num++;
			}
			if (HasNumClassicPacksUntilDeprioritize)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)NumClassicPacksUntilDeprioritize);
			}
			if (HasAllowOfflineClientActivityIos)
			{
				num += 2;
				num++;
			}
			if (HasAllowOfflineClientActivityAndroid)
			{
				num += 2;
				num++;
			}
			if (HasAllowOfflineClientActivityDesktop)
			{
				num += 2;
				num++;
			}
			if (HasAllowOfflineClientDeckDeletion)
			{
				num += 2;
				num++;
			}
			if (HasBattlegrounds)
			{
				num += 2;
				num++;
			}
			if (HasBattlegroundsFriendlyChallenge)
			{
				num += 2;
				num++;
			}
			if (HasSimpleCheckoutIos)
			{
				num += 2;
				num++;
			}
			if (HasSimpleCheckoutAndroidAmazon)
			{
				num += 2;
				num++;
			}
			if (HasSimpleCheckoutAndroidGoogle)
			{
				num += 2;
				num++;
			}
			if (HasSimpleCheckoutAndroidGlobal)
			{
				num += 2;
				num++;
			}
			if (HasSimpleCheckoutWin)
			{
				num += 2;
				num++;
			}
			if (HasSimpleCheckoutMac)
			{
				num += 2;
				num++;
			}
			if (HasBattlegroundsEarlyAccessLicense)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)BattlegroundsEarlyAccessLicense);
			}
			if (HasVirtualCurrencyEnabled)
			{
				num += 2;
				num++;
			}
			if (HasBattlegroundsTutorial)
			{
				num += 2;
				num++;
			}
			if (HasVintageStoreEnabled)
			{
				num += 2;
				num++;
			}
			if (HasBoosterRotatingSoonWarnDaysWithoutSale)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)BoosterRotatingSoonWarnDaysWithoutSale);
			}
			if (HasBoosterRotatingSoonWarnDaysWithSale)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)BoosterRotatingSoonWarnDaysWithSale);
			}
			if (HasBattlegroundsMaxRankedPartySize)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)BattlegroundsMaxRankedPartySize);
			}
			if (HasDeckReordering)
			{
				num += 2;
				num++;
			}
			if (HasProgressionEnabled)
			{
				num += 2;
				num++;
			}
			if (HasPvpdrClosedToNewSessionsSeconds)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt32(PvpdrClosedToNewSessionsSeconds);
			}
			if (HasDuels)
			{
				num += 2;
				num++;
			}
			if (HasDuelsEarlyAccessLicense)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt32(DuelsEarlyAccessLicense);
			}
			if (HasPaidDuels)
			{
				num += 2;
				num++;
			}
			if (HasAllowLiveFpsGathering)
			{
				num += 2;
				num++;
			}
			if (HasJournalButtonDisabled)
			{
				num += 2;
				num++;
			}
			if (HasAchievementToastDisabled)
			{
				num += 2;
				num++;
			}
			if (HasCheckForNewQuestsIntervalJitterSecs)
			{
				num += 2;
				num += 4;
			}
			if (HasRankedStandard)
			{
				num += 2;
				num++;
			}
			if (HasRankedWild)
			{
				num += 2;
				num++;
			}
			if (HasRankedClassic)
			{
				num += 2;
				num++;
			}
			if (HasRankedNewPlayer)
			{
				num += 2;
				num++;
			}
			if (HasContentstackEnabled)
			{
				num += 2;
				num++;
			}
			if (HasEndOfTurnToastPauseBufferSecs)
			{
				num += 2;
				num += 4;
			}
			if (HasAppRatingEnabled)
			{
				num += 2;
				num++;
			}
			if (HasAppRatingSamplingPercentage)
			{
				num += 2;
				num += 4;
			}
			if (HasBuyCardBacksFromCollectionManagerEnabled)
			{
				num += 2;
				num++;
			}
			if (HasBuyHeroSkinsFromCollectionManagerEnabled)
			{
				num += 2;
				num++;
			}
			return num;
		}
	}
}
