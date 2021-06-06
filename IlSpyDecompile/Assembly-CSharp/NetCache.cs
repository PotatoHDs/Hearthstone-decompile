using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bgs;
using bgs.types;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Blizzard.Telemetry.WTCG.Client;
using BobNetProto;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.Streaming;
using PegasusFSG;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class NetCache : IService, IHasUpdate
{
	public delegate void DelNewNoticesListener(List<ProfileNotice> newNotices, bool isInitialNoticeList);

	public delegate void DelGoldBalanceListener(NetCacheGoldBalance balance);

	public delegate void DelFavoriteCardBackChangedListener(int newFavoriteCardBackID);

	public delegate void DelFavoriteCoinChangedListener(int newFavoriteCoinID);

	public class NetCacheGamesPlayed
	{
		public int GamesStarted { get; set; }

		public int GamesWon { get; set; }

		public int GamesLost { get; set; }

		public int FreeRewardProgress { get; set; }
	}

	public class NetCacheFeatures
	{
		public class CacheMisc
		{
			public int ClientOptionsUpdateIntervalSeconds { get; set; }

			public bool AllowLiveFPSGathering { get; set; }
		}

		public class CacheGames
		{
			public enum FeatureFlags
			{
				Invalid,
				Tournament,
				Practice,
				Casual,
				Forge,
				Friendly,
				TavernBrawl,
				Battlegrounds,
				BattlegroundsFriendlyChallenge,
				BattlegroundsTutorial,
				Duels,
				PaidDuels
			}

			public bool Tournament { get; set; }

			public bool Practice { get; set; }

			public bool Casual { get; set; }

			public bool Forge { get; set; }

			public bool Friendly { get; set; }

			public bool TavernBrawl { get; set; }

			public bool Battlegrounds { get; set; }

			public bool BattlegroundsFriendlyChallenge { get; set; }

			public bool BattlegroundsTutorial { get; set; }

			public int ShowUserUI { get; set; }

			public bool Duels { get; set; }

			public bool PaidDuels { get; set; }

			public bool GetFeatureFlag(FeatureFlags flag)
			{
				return flag switch
				{
					FeatureFlags.Tournament => Tournament, 
					FeatureFlags.Practice => Practice, 
					FeatureFlags.Casual => Casual, 
					FeatureFlags.Forge => Forge, 
					FeatureFlags.Friendly => Friendly, 
					FeatureFlags.TavernBrawl => TavernBrawl, 
					FeatureFlags.Battlegrounds => Battlegrounds, 
					FeatureFlags.BattlegroundsFriendlyChallenge => BattlegroundsFriendlyChallenge, 
					FeatureFlags.BattlegroundsTutorial => BattlegroundsTutorial, 
					FeatureFlags.Duels => Duels, 
					FeatureFlags.PaidDuels => PaidDuels, 
					_ => false, 
				};
			}
		}

		public class CacheCollection
		{
			public bool Manager { get; set; }

			public bool Crafting { get; set; }

			public bool DeckReordering { get; set; }
		}

		public class CacheStore
		{
			public bool Store { get; set; }

			public bool BattlePay { get; set; }

			public bool BuyWithGold { get; set; }

			public bool SimpleCheckout { get; set; }

			public bool SoftAccountPurchasing { get; set; }

			public bool VirtualCurrencyEnabled { get; set; }

			public int NumClassicPacksUntilDeprioritize { get; set; }

			public bool SimpleCheckoutIOS { get; set; }

			public bool SimpleCheckoutAndroidAmazon { get; set; }

			public bool SimpleCheckoutAndroidGoogle { get; set; }

			public bool SimpleCheckoutAndroidGlobal { get; set; }

			public bool SimpleCheckoutWin { get; set; }

			public bool SimpleCheckoutMac { get; set; }

			public int BoosterRotatingSoonWarnDaysWithoutSale { get; set; }

			public int BoosterRotatingSoonWarnDaysWithSale { get; set; }

			public bool VintageStore { get; set; }

			public bool BuyCardBacksFromCollectionManager { get; set; }

			public bool BuyHeroSkinsFromCollectionManager { get; set; }
		}

		public class CacheHeroes
		{
			public bool Hunter { get; set; }

			public bool Mage { get; set; }

			public bool Paladin { get; set; }

			public bool Priest { get; set; }

			public bool Rogue { get; set; }

			public bool Shaman { get; set; }

			public bool Warlock { get; set; }

			public bool Warrior { get; set; }
		}

		public bool CaisEnabledNonMobile;

		public bool CaisEnabledMobileChina;

		public bool CaisEnabledMobileSouthKorea;

		public bool SendTelemetryPresence;

		public CacheMisc Misc { get; set; }

		public CacheGames Games { get; set; }

		public CacheCollection Collection { get; set; }

		public CacheStore Store { get; set; }

		public CacheHeroes Heroes { get; set; }

		public int WinsPerGold { get; set; }

		public int GoldPerReward { get; set; }

		public int MaxGoldPerDay { get; set; }

		public int XPSoloLimit { get; set; }

		public int MaxHeroLevel { get; set; }

		public float SpecialEventTimingMod { get; set; }

		public int FriendWeekConcederMaxDefense { get; set; }

		public int FriendWeekConcededGameMinTotalTurns { get; set; }

		public bool FriendWeekAllowsTavernBrawlRecordUpdate { get; set; }

		public bool FSGEnabled { get; set; }

		public bool FSGAutoCheckinEnabled { get; set; }

		public bool FSGLoginScanEnabled { get; set; }

		public bool FSGShowBetaLabel { get; set; }

		public int FSGFriendListPatronCountLimit { get; set; }

		public uint ArenaClosedToNewSessionsSeconds { get; set; }

		public uint PVPDRClosedToNewSessionsSeconds { get; set; }

		public int FsgMaxPresencePubscribedPatronCount { get; set; }

		public bool QuickPackOpeningAllowed { get; set; }

		public bool ForceIosLowRes { get; set; }

		public bool EnableSmartDeckCompletion { get; set; }

		public bool AllowOfflineClientActivity { get; set; }

		public bool AllowOfflineClientDeckDeletion { get; set; }

		public int BattlegroundsEarlyAccessLicense { get; set; }

		public int BattlegroundsMaxRankedPartySize { get; set; }

		public bool ProgressionEnabled { get; set; }

		public bool JournalButtonDisabled { get; set; }

		public bool AchievementToastDisabled { get; set; }

		public uint DuelsEarlyAccessLicense { get; set; }

		public bool ContentstackEnabled { get; set; }

		public bool AppRatingEnabled { get; set; }

		public float AppRatingSamplingPercentage { get; set; }

		public NetCacheFeatures()
		{
			Misc = new CacheMisc();
			Games = new CacheGames();
			Collection = new CacheCollection();
			Store = new CacheStore();
			Heroes = new CacheHeroes();
		}
	}

	public class NetCacheArcaneDustBalance
	{
		public long Balance { get; set; }
	}

	public class NetCacheGoldBalance
	{
		public long CappedBalance { get; set; }

		public long BonusBalance { get; set; }

		public long Cap { get; set; }

		public long CapWarning { get; set; }

		public long GetTotal()
		{
			return CappedBalance + BonusBalance;
		}
	}

	public class NetPlayerArenaTickets
	{
		public int Balance { get; set; }
	}

	public class NetCacheSubscribeResponse
	{
		public ulong FeaturesSupported;

		public ulong Route;

		public ulong KeepAliveDelay;

		public ulong RequestMaxWaitSecs;
	}

	public class HeroLevel
	{
		public class LevelInfo
		{
			public int Level { get; set; }

			public int MaxLevel { get; set; }

			public long XP { get; set; }

			public long MaxXP { get; set; }

			public LevelInfo()
			{
				Level = 0;
				MaxLevel = 60;
				XP = 0L;
				MaxXP = 0L;
			}

			public bool IsMaxLevel()
			{
				return Level == MaxLevel;
			}

			public override string ToString()
			{
				return $"[LevelInfo: Level={Level}, XP={XP}, MaxXP={MaxXP}]";
			}
		}

		public TAG_CLASS Class { get; set; }

		public LevelInfo PrevLevel { get; set; }

		public LevelInfo CurrentLevel { get; set; }

		public HeroLevel()
		{
			Class = TAG_CLASS.INVALID;
			PrevLevel = null;
			CurrentLevel = new LevelInfo();
		}

		public override string ToString()
		{
			return $"[HeroLevel: Class={Class}, PrevLevel={PrevLevel}, CurrentLevel={CurrentLevel}]";
		}
	}

	public class NetCacheHeroLevels
	{
		public List<HeroLevel> Levels { get; set; }

		public NetCacheHeroLevels()
		{
			Levels = new List<HeroLevel>();
		}

		public override string ToString()
		{
			string text = "[START NetCacheHeroLevels]\n";
			foreach (HeroLevel level in Levels)
			{
				text += $"{level}\n";
			}
			return text + "[END NetCacheHeroLevels]";
		}
	}

	public class NetCacheProfileProgress
	{
		public TutorialProgress CampaignProgress { get; set; }

		public int BestForgeWins { get; set; }

		public long LastForgeDate { get; set; }
	}

	public class NetCacheDisplayBanner
	{
		public int Id { get; set; }
	}

	public class NetCacheCardBacks
	{
		public int FavoriteCardBack { get; set; }

		public HashSet<int> CardBacks { get; set; }

		public NetCacheCardBacks()
		{
			CardBacks = new HashSet<int>();
		}
	}

	public class NetCacheCoins
	{
		public int FavoriteCoin { get; set; }

		public HashSet<int> Coins { get; set; }

		public NetCacheCoins()
		{
			Coins = new HashSet<int>();
		}
	}

	public class BoosterStack
	{
		public int Id { get; set; }

		public int Count { get; set; }

		public int LocallyPreConsumedCount { get; set; }

		public int EverGrantedCount { get; set; }
	}

	public class NetCacheBoosters
	{
		public List<BoosterStack> BoosterStacks { get; set; }

		public NetCacheBoosters()
		{
			BoosterStacks = new List<BoosterStack>();
		}

		public BoosterStack GetBoosterStack(int id)
		{
			return BoosterStacks.Find((BoosterStack obj) => obj.Id == id);
		}

		public int GetTotalNumBoosters()
		{
			int num = 0;
			foreach (BoosterStack boosterStack in BoosterStacks)
			{
				num += boosterStack.Count;
			}
			return num;
		}
	}

	public class DeckHeader
	{
		public long ID { get; set; }

		public string Name { get; set; }

		public int CardBack { get; set; }

		public string Hero { get; set; }

		public TAG_PREMIUM HeroPremium { get; set; }

		public string UIHeroOverride { get; set; }

		public TAG_PREMIUM UIHeroOverridePremium { get; set; }

		public string HeroPower { get; set; }

		public DeckType Type { get; set; }

		public bool CardBackOverridden { get; set; }

		public bool HeroOverridden { get; set; }

		public int SeasonId { get; set; }

		public int BrawlLibraryItemId { get; set; }

		public bool NeedsName { get; set; }

		public long SortOrder { get; set; }

		public PegasusShared.FormatType FormatType { get; set; }

		public bool Locked { get; set; }

		public DeckSourceType SourceType { get; set; }

		public DateTime? CreateDate { get; set; }

		public DateTime? LastModified { get; set; }

		public override string ToString()
		{
			return $"[DeckHeader: ID={ID} Name={Name} Hero={Hero} HeroPremium={HeroPremium} HeroPower={HeroPower} DeckType={Type} CardBack={CardBack} CardBackOverridden={CardBackOverridden} HeroOverridden={HeroOverridden} NeedsName={NeedsName} SortOrder={SortOrder} SourceType={SourceType} LastModified={CreateDate} CreateDate={LastModified}]";
		}
	}

	public class NetCacheDecks
	{
		public List<DeckHeader> Decks { get; set; }

		public NetCacheDecks()
		{
			Decks = new List<DeckHeader>();
		}
	}

	public class CardDefinition
	{
		public string Name { get; set; }

		public TAG_PREMIUM Premium { get; set; }

		public override bool Equals(object obj)
		{
			CardDefinition cardDefinition = obj as CardDefinition;
			if (cardDefinition != null && Premium == cardDefinition.Premium)
			{
				return Name.Equals(cardDefinition.Name);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (int)(Name.GetHashCode() + Premium);
		}

		public override string ToString()
		{
			return $"[CardDefinition: Name={Name}, Premium={Premium}]";
		}
	}

	public class CardValue
	{
		public int BaseBuyValue { get; set; }

		public int BaseSellValue { get; set; }

		public int BuyValueOverride { get; set; }

		public int SellValueOverride { get; set; }

		public SpecialEventType OverrideEvent { get; set; }

		public int GetBuyValue()
		{
			if (!IsOverrideActive())
			{
				return BaseBuyValue;
			}
			return BuyValueOverride;
		}

		public int GetSellValue()
		{
			if (!IsOverrideActive())
			{
				return BaseSellValue;
			}
			return SellValueOverride;
		}

		public bool IsOverrideActive()
		{
			return SpecialEventManager.Get().IsEventActive(OverrideEvent, activeIfDoesNotExist: false);
		}
	}

	public class NetCacheCardValues
	{
		public Map<CardDefinition, CardValue> Values { get; set; }

		public NetCacheCardValues()
		{
			Values = new Map<CardDefinition, CardValue>();
		}
	}

	public class NetCacheDisconnectedGame
	{
		public GameServerInfo ServerInfo { get; set; }

		public PegasusShared.GameType GameType { get; set; }

		public PegasusShared.FormatType FormatType { get; set; }

		public bool LoadGameState { get; set; }
	}

	public class BoosterCard
	{
		public CardDefinition Def { get; set; }

		public long Date { get; set; }

		public BoosterCard()
		{
			Def = new CardDefinition();
		}
	}

	public class CardStack
	{
		public CardDefinition Def { get; set; }

		public long Date { get; set; }

		public int Count { get; set; }

		public int NumSeen { get; set; }

		public CardStack()
		{
			Def = new CardDefinition();
		}
	}

	public class NetCacheCollection
	{
		public int TotalCardsOwned;

		public Map<TAG_CLASS, HashSet<string>> CoreCardsUnlockedPerClass = new Map<TAG_CLASS, HashSet<string>>();

		public List<CardStack> Stacks { get; set; }

		public NetCacheCollection()
		{
			Stacks = new List<CardStack>();
			foreach (TAG_CLASS value in Enum.GetValues(typeof(TAG_CLASS)))
			{
				CoreCardsUnlockedPerClass[value] = new HashSet<string>();
			}
		}
	}

	public class PlayerRecord
	{
		public PegasusShared.GameType RecordType { get; set; }

		public int Data { get; set; }

		public int Wins { get; set; }

		public int Losses { get; set; }

		public int Ties { get; set; }
	}

	public class NetCachePlayerRecords
	{
		public List<PlayerRecord> Records { get; set; }

		public NetCachePlayerRecords()
		{
			Records = new List<PlayerRecord>();
		}
	}

	public class NetCacheRewardProgress
	{
		public int Season { get; set; }

		public long SeasonEndDate { get; set; }

		public long NextQuestCancelDate { get; set; }
	}

	public class NetCacheMedalInfo
	{
		public Map<PegasusShared.FormatType, MedalInfoData> MedalData = new Map<PegasusShared.FormatType, MedalInfoData>();

		public NetCacheMedalInfo PreviousMedalInfo { get; set; }

		public NetCacheMedalInfo()
		{
		}

		public NetCacheMedalInfo(MedalInfo packet)
		{
			foreach (MedalInfoData medalDatum in packet.MedalData)
			{
				MedalData.Add(medalDatum.FormatType, medalDatum);
			}
		}

		public NetCacheMedalInfo Clone()
		{
			NetCacheMedalInfo netCacheMedalInfo = new NetCacheMedalInfo();
			foreach (KeyValuePair<PegasusShared.FormatType, MedalInfoData> medalDatum in MedalData)
			{
				netCacheMedalInfo.MedalData.Add(medalDatum.Key, CloneMedalInfoData(medalDatum.Value));
			}
			return netCacheMedalInfo;
		}

		public MedalInfoData GetMedalInfoData(PegasusShared.FormatType formatType)
		{
			if (!MedalData.TryGetValue(formatType, out var value))
			{
				Debug.LogError("NetCacheMedalInfo.GetMedalInfoData failed to find data for the format type " + formatType.ToString() + ". Returning null");
			}
			return value;
		}

		public Map<PegasusShared.FormatType, MedalInfoData>.ValueCollection GetAllMedalInfoData()
		{
			return MedalData.Values;
		}

		public static MedalInfoData CloneMedalInfoData(MedalInfoData original)
		{
			MedalInfoData medalInfoData = new MedalInfoData();
			medalInfoData.LeagueId = original.LeagueId;
			medalInfoData.SeasonWins = original.SeasonWins;
			medalInfoData.Stars = original.Stars;
			medalInfoData.Streak = original.Streak;
			medalInfoData.StarLevel = original.StarLevel;
			medalInfoData.HasLegendRank = original.HasLegendRank;
			medalInfoData.LegendRank = original.LegendRank;
			medalInfoData.HasBestStarLevel = original.HasBestStarLevel;
			medalInfoData.BestStarLevel = original.BestStarLevel;
			medalInfoData.HasSeasonGames = original.HasSeasonGames;
			medalInfoData.SeasonGames = original.SeasonGames;
			medalInfoData.StarsPerWin = original.StarsPerWin;
			if (original.HasRatingId)
			{
				medalInfoData.RatingId = original.RatingId;
			}
			if (original.HasSeasonId)
			{
				medalInfoData.SeasonId = original.SeasonId;
			}
			if (original.HasRating)
			{
				medalInfoData.Rating = original.Rating;
			}
			if (original.HasVariance)
			{
				medalInfoData.Variance = original.Variance;
			}
			if (original.HasBestStars)
			{
				medalInfoData.BestStars = original.BestStars;
			}
			if (original.HasBestEverLeagueId)
			{
				medalInfoData.BestEverLeagueId = original.BestEverLeagueId;
			}
			if (original.HasBestEverStarLevel)
			{
				medalInfoData.BestEverStarLevel = original.BestEverStarLevel;
			}
			if (original.HasBestRating)
			{
				medalInfoData.BestRating = original.BestRating;
			}
			if (original.HasPublicRating)
			{
				medalInfoData.PublicRating = original.PublicRating;
			}
			if (original.HasRatingAdjustment)
			{
				medalInfoData.RatingAdjustment = original.RatingAdjustment;
			}
			if (original.HasRatingAdjustmentWins)
			{
				medalInfoData.RatingAdjustmentWins = original.RatingAdjustmentWins;
			}
			if (original.HasFormatType)
			{
				medalInfoData.FormatType = original.FormatType;
			}
			return medalInfoData;
		}

		public override string ToString()
		{
			return $"[NetCacheMedalInfo] \n MedalData={MedalData.ToString()}";
		}
	}

	public class NetCacheBaconRatingInfo
	{
		public int Rating { get; set; }

		public NetCacheBaconRatingInfo Clone()
		{
			return new NetCacheBaconRatingInfo
			{
				Rating = Rating
			};
		}

		public override string ToString()
		{
			return $"[NetCacheBaconRatingInfo] \n Rating={Rating}";
		}
	}

	public class NetCacheBaconPremiumStatus
	{
		public List<BattlegroundSeasonPremiumStatus> SeasonPremiumStatus { get; set; }

		public override string ToString()
		{
			return $"[NetCacheBaconPremiumStatus] \n SeasonPremiumStatus={SeasonPremiumStatus}";
		}
	}

	public class NetCachePVPDRStatsInfo
	{
		public int Rating { get; set; }

		public int PaidRating { get; set; }

		public int HighWatermark { get; set; }

		public NetCachePVPDRStatsInfo Clone()
		{
			return new NetCachePVPDRStatsInfo
			{
				Rating = Rating,
				PaidRating = PaidRating,
				HighWatermark = HighWatermark
			};
		}

		public override string ToString()
		{
			return $"[NetCachePVPDRStatsInfo] \n Rating={Rating} PaidRating={PaidRating} HighWatermark={HighWatermark}";
		}
	}

	public abstract class ProfileNotice
	{
		public enum NoticeType
		{
			GAINED_MEDAL = 1,
			REWARD_BOOSTER = 2,
			REWARD_CARD = 3,
			DISCONNECTED_GAME = 4,
			PRECON_DECK = 5,
			REWARD_DUST = 6,
			REWARD_MOUNT = 7,
			REWARD_FORGE = 8,
			REWARD_CURRENCY = 9,
			PURCHASE = 10,
			REWARD_CARD_BACK = 11,
			BONUS_STARS = 12,
			ADVENTURE_PROGRESS = 14,
			HERO_LEVEL_UP = 0xF,
			ACCOUNT_LICENSE = 0x10,
			TAVERN_BRAWL_REWARDS = 17,
			TAVERN_BRAWL_TICKET = 18,
			EVENT = 19,
			GENERIC_REWARD_CHEST = 20,
			LEAGUE_PROMOTION_REWARDS = 21,
			CARD_REPLACEMENT = 22,
			DISCONNECTED_GAME_NEW = 23,
			FREE_DECK_CHOICE = 24,
			DECK_REMOVED = 25,
			DECK_GRANTED = 26,
			MINI_SET_GRANTED = 27,
			SELLABLE_DECK_GRANTED = 28
		}

		public enum NoticeOrigin
		{
			UNKNOWN = -1,
			SEASON = 1,
			BETA_REIMBURSE = 2,
			FORGE = 3,
			TOURNEY = 4,
			PRECON_DECK = 5,
			ACK = 6,
			ACHIEVEMENT = 7,
			LEVEL_UP = 8,
			PURCHASE_COMPLETE = 10,
			PURCHASE_FAILED = 11,
			PURCHASE_CANCELED = 12,
			BLIZZCON = 13,
			EVENT = 14,
			DISCONNECTED_GAME = 0xF,
			OUT_OF_BAND_LICENSE = 0x10,
			IGR = 17,
			ADVENTURE_PROGRESS = 18,
			ADVENTURE_FLAGS = 19,
			TAVERN_BRAWL_REWARD = 20,
			ACCOUNT_LICENSE_FLAGS = 21,
			FROM_PURCHASE = 22,
			HOF_COMPENSATION = 23,
			GENERIC_REWARD_CHEST_ACHIEVE = 24,
			GENERIC_REWARD_CHEST = 25,
			LEAGUE_PROMOTION = 26,
			CARD_REPLACEMENT = 27,
			NOTICE_ORIGIN_LEVEL_UP_MULTIPLE = 28,
			NOTICE_ORIGIN_DUELS = 29
		}

		private NoticeType m_type;

		public long NoticeID { get; set; }

		public NoticeType Type => m_type;

		public NoticeOrigin Origin { get; set; }

		public long OriginData { get; set; }

		public long Date { get; set; }

		protected ProfileNotice(NoticeType init)
		{
			m_type = init;
			NoticeID = 0L;
			Origin = NoticeOrigin.UNKNOWN;
			OriginData = 0L;
			Date = 0L;
		}

		public override string ToString()
		{
			return $"[{GetType()}: NoticeID={NoticeID}, Type={Type}, Origin={Origin}, OriginData={OriginData}, Date={Date}]";
		}
	}

	public class ProfileNoticeFreeDeckChoice : ProfileNotice
	{
		public long DeckID { get; set; }

		public int ClassID { get; set; }

		public ProfileNoticeFreeDeckChoice()
			: base(NoticeType.FREE_DECK_CHOICE)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [DeckID={DeckID}, ClassID={ClassID}]";
		}
	}

	public class ProfileNoticeMedal : ProfileNotice
	{
		public int LeagueId { get; set; }

		public int StarLevel { get; set; }

		public int LegendRank { get; set; }

		public int BestStarLevel { get; set; }

		public PegasusShared.FormatType FormatType { get; set; }

		public Network.RewardChest Chest { get; set; }

		public bool WasLimitedByBestEverStarLevel { get; set; }

		public ProfileNoticeMedal()
			: base(NoticeType.GAINED_MEDAL)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [LeagueId={LeagueId} StarLevel={StarLevel}, LegendRank={LegendRank}, BestStarLevel={BestStarLevel}, FormatType={FormatType}, Chest={Chest}, WasLimitedByBestEverStarLevel={WasLimitedByBestEverStarLevel}]";
		}
	}

	public class ProfileNoticeRewardBooster : ProfileNotice
	{
		public int Id { get; set; }

		public int Count { get; set; }

		public ProfileNoticeRewardBooster()
			: base(NoticeType.REWARD_BOOSTER)
		{
			Id = 0;
			Count = 0;
		}

		public override string ToString()
		{
			return $"{base.ToString()} [Id={Id}, Count={Count}]";
		}
	}

	public class ProfileNoticeRewardCard : ProfileNotice
	{
		public string CardID { get; set; }

		public TAG_PREMIUM Premium { get; set; }

		public int Quantity { get; set; }

		public ProfileNoticeRewardCard()
			: base(NoticeType.REWARD_CARD)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [CardID={CardID}, Premium={Premium}, Quantity={Quantity}]";
		}
	}

	public class ProfileNoticePreconDeck : ProfileNotice
	{
		public long DeckID { get; set; }

		public int HeroAsset { get; set; }

		public ProfileNoticePreconDeck()
			: base(NoticeType.PRECON_DECK)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [DeckID={DeckID}, HeroAsset={HeroAsset}]";
		}
	}

	public class ProfileNoticeDeckRemoved : ProfileNotice
	{
		public long DeckID { get; set; }

		public ProfileNoticeDeckRemoved()
			: base(NoticeType.DECK_REMOVED)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [DeckID={DeckID}]";
		}
	}

	public class ProfileNoticeDeckGranted : ProfileNotice
	{
		public int DeckDbiID { get; set; }

		public int ClassId { get; set; }

		public long PlayerDeckID { get; set; }

		public ProfileNoticeDeckGranted()
			: base(NoticeType.DECK_GRANTED)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [DeckDbiID={DeckDbiID}, ClassId={ClassId}]";
		}
	}

	public class ProfileNoticeMiniSetGranted : ProfileNotice
	{
		public int MiniSetID { get; set; }

		public ProfileNoticeMiniSetGranted()
			: base(NoticeType.MINI_SET_GRANTED)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [CardsRewardID={MiniSetID}]";
		}
	}

	public class ProfileNoticeSellableDeckGranted : ProfileNotice
	{
		public int SellableDeckID { get; set; }

		public bool WasDeckGranted { get; set; }

		public long PlayerDeckID { get; set; }

		public ProfileNoticeSellableDeckGranted()
			: base(NoticeType.SELLABLE_DECK_GRANTED)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [SellableDeckRewardID={SellableDeckID}, WasDeckGranted={WasDeckGranted}]";
		}
	}

	public class ProfileNoticeRewardDust : ProfileNotice
	{
		public int Amount { get; set; }

		public ProfileNoticeRewardDust()
			: base(NoticeType.REWARD_DUST)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [Amount={Amount}]";
		}
	}

	public class ProfileNoticeRewardMount : ProfileNotice
	{
		public int MountID { get; set; }

		public ProfileNoticeRewardMount()
			: base(NoticeType.REWARD_MOUNT)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [MountID={MountID}]";
		}
	}

	public class ProfileNoticeRewardForge : ProfileNotice
	{
		public int Quantity { get; set; }

		public ProfileNoticeRewardForge()
			: base(NoticeType.REWARD_FORGE)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [Quantity={Quantity}]";
		}
	}

	public class ProfileNoticeRewardCurrency : ProfileNotice
	{
		public int Amount { get; set; }

		public PegasusShared.CurrencyType CurrencyType { get; set; }

		public ProfileNoticeRewardCurrency()
			: base(NoticeType.REWARD_CURRENCY)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [CurrencyType={CurrencyType.ToString()}, Amount={Amount}]";
		}
	}

	public class ProfileNoticePurchase : ProfileNotice
	{
		public long? PMTProductID { get; set; }

		public string CurrencyCode { get; set; }

		public long Data { get; set; }

		public ProfileNoticePurchase()
			: base(NoticeType.PURCHASE)
		{
		}

		public override string ToString()
		{
			return $"[ProfileNoticePurchase: NoticeID={base.NoticeID}, Type={base.Type}, Origin={base.Origin}, OriginData={base.OriginData}, Date={base.Date} PMTProductID='{PMTProductID}', Data={Data} Currency={CurrencyCode}]";
		}
	}

	public class ProfileNoticeRewardCardBack : ProfileNotice
	{
		public int CardBackID { get; set; }

		public ProfileNoticeRewardCardBack()
			: base(NoticeType.REWARD_CARD_BACK)
		{
		}

		public override string ToString()
		{
			return $"[ProfileNoticePurchase: NoticeID={base.NoticeID}, Type={base.Type}, Origin={base.Origin}, OriginData={base.OriginData}, Date={base.Date} CardBackID={CardBackID}]";
		}
	}

	public class ProfileNoticeBonusStars : ProfileNotice
	{
		public int StarLevel { get; set; }

		public int Stars { get; set; }

		public ProfileNoticeBonusStars()
			: base(NoticeType.BONUS_STARS)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [StarLevel={StarLevel}, Stars={Stars}]";
		}
	}

	public class ProfileNoticeEvent : ProfileNotice
	{
		public int EventType { get; set; }

		public ProfileNoticeEvent()
			: base(NoticeType.EVENT)
		{
		}

		public override string ToString()
		{
			return $"[ProfileNoticeEvent: NoticeID={base.NoticeID}, Type={base.Type}, Origin={base.Origin}, OriginData={base.OriginData}, Date={base.Date} EventType={EventType}]";
		}
	}

	public class ProfileNoticeDisconnectedGame : ProfileNotice
	{
		public PegasusShared.GameType GameType { get; set; }

		public PegasusShared.FormatType FormatType { get; set; }

		public int MissionId { get; set; }

		public ProfileNoticeDisconnectedGameResult.GameResult GameResult { get; set; }

		public ProfileNoticeDisconnectedGameResult.PlayerResult YourResult { get; set; }

		public ProfileNoticeDisconnectedGameResult.PlayerResult OpponentResult { get; set; }

		public int PlayerIndex { get; set; }

		public ProfileNoticeDisconnectedGame()
			: base(NoticeType.DISCONNECTED_GAME)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [GameType={GameType}, FormatType={FormatType}, MissionId={MissionId} GameResult={GameResult}, YourResult={YourResult}, OpponentResult={OpponentResult}, PlayerIndex={PlayerIndex}]";
		}
	}

	public class ProfileNoticeAdventureProgress : ProfileNotice
	{
		public int Wing { get; set; }

		public int? Progress { get; set; }

		public ulong? Flags { get; set; }

		public ProfileNoticeAdventureProgress()
			: base(NoticeType.ADVENTURE_PROGRESS)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [Wing={Wing}, Progress={Progress}, Flags={Flags}]";
		}
	}

	public class ProfileNoticeLevelUp : ProfileNotice
	{
		public int HeroClass { get; set; }

		public int NewLevel { get; set; }

		public int TotalLevel { get; set; }

		public ProfileNoticeLevelUp()
			: base(NoticeType.HERO_LEVEL_UP)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [HeroClass={HeroClass}, NewLevel={NewLevel}], TotalLevel={TotalLevel}";
		}
	}

	public class ProfileNoticeAcccountLicense : ProfileNotice
	{
		public long License { get; set; }

		public long CasID { get; set; }

		public ProfileNoticeAcccountLicense()
			: base(NoticeType.ACCOUNT_LICENSE)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [License={License}, CasID={CasID}]";
		}
	}

	public class ProfileNoticeTavernBrawlRewards : ProfileNotice
	{
		public RewardChest Chest { get; set; }

		public int Wins { get; set; }

		public TavernBrawlMode Mode { get; set; }

		public ProfileNoticeTavernBrawlRewards()
			: base(NoticeType.TAVERN_BRAWL_REWARDS)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [Chest={Chest}, Wins={Wins}, Mode={Mode}]";
		}
	}

	public class ProfileNoticeTavernBrawlTicket : ProfileNotice
	{
		public int TicketType { get; set; }

		public int Quantity { get; set; }

		public ProfileNoticeTavernBrawlTicket()
			: base(NoticeType.TAVERN_BRAWL_TICKET)
		{
		}
	}

	public class ProfileNoticeGenericRewardChest : ProfileNotice
	{
		public int RewardChestAssetId { get; set; }

		public RewardChest RewardChest { get; set; }

		public uint RewardChestByteSize { get; set; }

		public byte[] RewardChestHash { get; set; }

		public ProfileNoticeGenericRewardChest()
			: base(NoticeType.GENERIC_REWARD_CHEST)
		{
		}
	}

	public class NetCacheProfileNotices
	{
		public List<ProfileNotice> Notices { get; set; }

		public NetCacheProfileNotices()
		{
			Notices = new List<ProfileNotice>();
		}
	}

	public class ProfileNoticeLeaguePromotionRewards : ProfileNotice
	{
		public RewardChest Chest { get; set; }

		public int LeagueId { get; set; }

		public ProfileNoticeLeaguePromotionRewards()
			: base(NoticeType.LEAGUE_PROMOTION_REWARDS)
		{
		}

		public override string ToString()
		{
			return $"{base.ToString()} [Chest={Chest}, LeagueId={LeagueId}]";
		}
	}

	public abstract class ClientOptionBase : ICloneable
	{
		public abstract void PopulateIntoPacket(ServerOption type, SetOptions packet);

		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			if (other.GetType() != GetType())
			{
				return false;
			}
			return true;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public object Clone()
		{
			return MemberwiseClone();
		}
	}

	public class ClientOptionInt : ClientOptionBase
	{
		public int OptionValue { get; set; }

		public ClientOptionInt(int val)
		{
			OptionValue = val;
		}

		public override void PopulateIntoPacket(ServerOption type, SetOptions packet)
		{
			PegasusUtil.ClientOption clientOption = new PegasusUtil.ClientOption();
			clientOption.Index = (int)type;
			clientOption.AsInt32 = OptionValue;
			packet.Options.Add(clientOption);
		}

		public override bool Equals(object other)
		{
			if (base.Equals(other) && ((ClientOptionInt)other).OptionValue == OptionValue)
			{
				return true;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return OptionValue.GetHashCode();
		}
	}

	public class ClientOptionLong : ClientOptionBase
	{
		public long OptionValue { get; set; }

		public ClientOptionLong(long val)
		{
			OptionValue = val;
		}

		public override void PopulateIntoPacket(ServerOption type, SetOptions packet)
		{
			PegasusUtil.ClientOption clientOption = new PegasusUtil.ClientOption();
			clientOption.Index = (int)type;
			clientOption.AsInt64 = OptionValue;
			packet.Options.Add(clientOption);
		}

		public override bool Equals(object other)
		{
			if (base.Equals(other) && ((ClientOptionLong)other).OptionValue == OptionValue)
			{
				return true;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return OptionValue.GetHashCode();
		}
	}

	public class ClientOptionFloat : ClientOptionBase
	{
		public float OptionValue { get; set; }

		public ClientOptionFloat(float val)
		{
			OptionValue = val;
		}

		public override void PopulateIntoPacket(ServerOption type, SetOptions packet)
		{
			PegasusUtil.ClientOption clientOption = new PegasusUtil.ClientOption();
			clientOption.Index = (int)type;
			clientOption.AsFloat = OptionValue;
			packet.Options.Add(clientOption);
		}

		public override bool Equals(object other)
		{
			if (base.Equals(other) && ((ClientOptionFloat)other).OptionValue == OptionValue)
			{
				return true;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return OptionValue.GetHashCode();
		}
	}

	public class ClientOptionULong : ClientOptionBase
	{
		public ulong OptionValue { get; set; }

		public ClientOptionULong(ulong val)
		{
			OptionValue = val;
		}

		public override void PopulateIntoPacket(ServerOption type, SetOptions packet)
		{
			PegasusUtil.ClientOption clientOption = new PegasusUtil.ClientOption();
			clientOption.Index = (int)type;
			clientOption.AsUint64 = OptionValue;
			packet.Options.Add(clientOption);
		}

		public override bool Equals(object other)
		{
			if (base.Equals(other) && ((ClientOptionULong)other).OptionValue == OptionValue)
			{
				return true;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return OptionValue.GetHashCode();
		}
	}

	public class NetCacheClientOptions
	{
		private DateTime? m_mostRecentDispatchToServer;

		private DateTime? m_currentScheduledDispatchTime;

		private int ClientOptionsUpdateIntervalSeconds
		{
			get
			{
				NetCacheFeatures netObject = Get().GetNetObject<NetCacheFeatures>();
				if (netObject != null && netObject.Misc != null)
				{
					return netObject.Misc.ClientOptionsUpdateIntervalSeconds;
				}
				return 180;
			}
		}

		public Map<ServerOption, ClientOptionBase> ClientState { get; private set; }

		private Map<ServerOption, ClientOptionBase> ServerState { get; set; }

		public NetCacheClientOptions()
		{
			ClientState = new Map<ServerOption, ClientOptionBase>();
			ServerState = new Map<ServerOption, ClientOptionBase>();
		}

		public void UpdateServerState()
		{
			foreach (KeyValuePair<ServerOption, ClientOptionBase> item in ClientState)
			{
				if (item.Value != null)
				{
					ServerState[item.Key] = (ClientOptionBase)item.Value.Clone();
				}
				else
				{
					ServerState[item.Key] = null;
				}
			}
		}

		public void OnUpdateIntervalElasped(object userData)
		{
			m_currentScheduledDispatchTime = null;
			DispatchClientOptionsToServer();
		}

		public void CancelScheduledDispatchToServer()
		{
			Processor.CancelScheduledCallback(OnUpdateIntervalElasped);
			m_currentScheduledDispatchTime = null;
		}

		public void DispatchClientOptionsToServer()
		{
			CancelScheduledDispatchToServer();
			bool flag = false;
			SetOptions setOptions = new SetOptions();
			foreach (KeyValuePair<ServerOption, ClientOptionBase> item in ClientState)
			{
				if (ServerState.TryGetValue(item.Key, out var value))
				{
					if (item.Value != null || value != null)
					{
						if ((item.Value == null && value != null) || (item.Value != null && value == null))
						{
							flag = true;
							break;
						}
						if (!value.Equals(item.Value))
						{
							flag = true;
							break;
						}
					}
					continue;
				}
				flag = true;
				break;
			}
			if (!flag)
			{
				return;
			}
			foreach (KeyValuePair<ServerOption, ClientOptionBase> item2 in ClientState)
			{
				if (item2.Value != null)
				{
					item2.Value.PopulateIntoPacket(item2.Key, setOptions);
				}
			}
			Network.Get().SetClientOptions(setOptions);
			m_mostRecentDispatchToServer = DateTime.UtcNow;
			UpdateServerState();
		}

		public void RemoveInvalidOptions()
		{
			List<ServerOption> list = new List<ServerOption>();
			foreach (KeyValuePair<ServerOption, ClientOptionBase> item in ClientState)
			{
				ServerOption key = item.Key;
				ClientOptionBase value = item.Value;
				Type serverOptionType = Options.Get().GetServerOptionType(key);
				if (value != null)
				{
					Type type = value.GetType();
					if (serverOptionType == typeof(int))
					{
						if (type == typeof(ClientOptionInt))
						{
							continue;
						}
					}
					else if (serverOptionType == typeof(long))
					{
						if (type == typeof(ClientOptionLong))
						{
							continue;
						}
					}
					else if (serverOptionType == typeof(float))
					{
						if (type == typeof(ClientOptionFloat))
						{
							continue;
						}
					}
					else if (serverOptionType == typeof(ulong) && type == typeof(ClientOptionULong))
					{
						continue;
					}
					if (serverOptionType == null)
					{
						Log.Net.Print("NetCacheClientOptions.RemoveInvalidOptions() - Option {0} has type {1}, but value is type {2}. Removing it.", key, serverOptionType, type);
					}
					else
					{
						Log.Net.Print("NetCacheClientOptions.RemoveInvalidOptions() - Option {0} has type {1}, but value is type {2}. Removing it.", EnumUtils.GetString(key), serverOptionType, type);
					}
				}
				list.Add(key);
			}
			foreach (ServerOption item2 in list)
			{
				ClientState.Remove(item2);
				ServerState.Remove(item2);
			}
		}

		public void CheckForDispatchToServer()
		{
			float num = ClientOptionsUpdateIntervalSeconds;
			if (num <= 0f)
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			bool flag = false;
			bool flag2 = false;
			if (!m_mostRecentDispatchToServer.HasValue)
			{
				flag = true;
			}
			else if (!m_currentScheduledDispatchTime.HasValue)
			{
				TimeSpan timeSpan = utcNow - m_mostRecentDispatchToServer.Value;
				if (timeSpan.TotalSeconds >= (double)num)
				{
					flag = true;
				}
				else
				{
					flag2 = true;
					num -= (float)timeSpan.TotalSeconds;
				}
			}
			if (!flag && !flag2 && m_currentScheduledDispatchTime.HasValue && (m_currentScheduledDispatchTime.Value - utcNow).TotalSeconds > (double)num)
			{
				flag2 = true;
			}
			if (flag || flag2)
			{
				float secondsToWait = (flag ? 0f : num);
				m_currentScheduledDispatchTime = utcNow;
				Processor.CancelScheduledCallback(OnUpdateIntervalElasped);
				Processor.ScheduleCallback(secondsToWait, realTime: true, OnUpdateIntervalElasped);
			}
		}
	}

	public class NetCacheFavoriteHeroes
	{
		public Map<TAG_CLASS, CardDefinition> FavoriteHeroes { get; set; }

		public NetCacheFavoriteHeroes()
		{
			FavoriteHeroes = new Map<TAG_CLASS, CardDefinition>();
		}
	}

	public class NetCacheAccountLicenses
	{
		public Map<long, AccountLicenseInfo> AccountLicenses { get; set; }

		public NetCacheAccountLicenses()
		{
			AccountLicenses = new Map<long, AccountLicenseInfo>();
		}
	}

	public delegate void ErrorCallback(ErrorInfo info);

	public enum ErrorCode
	{
		NONE,
		TIMEOUT,
		SERVER
	}

	public class ErrorInfo
	{
		public ErrorCode Error { get; set; }

		public uint ServerError { get; set; }

		public RequestFunc RequestingFunction { get; set; }

		public Map<Type, Request> RequestedTypes { get; set; }

		public string RequestStackTrace { get; set; }
	}

	public delegate void NetCacheCallback();

	public delegate void RequestFunc(NetCacheCallback callback, ErrorCallback errorCallback);

	public enum RequestResult
	{
		UNKNOWN,
		PENDING,
		IN_PROCESS,
		GENERIC_COMPLETE,
		DATA_COMPLETE,
		ERROR,
		MIGRATION_REQUIRED
	}

	public class Request
	{
		public const bool RELOAD = true;

		public Type m_type;

		public bool m_reload;

		public RequestResult m_result;

		public Request(Type rt, bool rl = false)
		{
			m_type = rt;
			m_reload = rl;
			m_result = RequestResult.UNKNOWN;
		}
	}

	private class NetCacheBatchRequest
	{
		public Map<Type, Request> m_requests = new Map<Type, Request>();

		public NetCacheCallback m_callback;

		public ErrorCallback m_errorCallback;

		public bool m_canTimeout = true;

		public float m_timeAdded = Time.realtimeSinceStartup;

		public RequestFunc m_requestFunc;

		public string m_requestStackTrace;

		public NetCacheBatchRequest(NetCacheCallback reply, ErrorCallback errorCallback, RequestFunc requestFunc)
		{
			m_callback = reply;
			m_errorCallback = errorCallback;
			m_requestFunc = requestFunc;
			m_requestStackTrace = Environment.StackTrace;
		}

		public void AddRequests(List<Request> requests)
		{
			foreach (Request request in requests)
			{
				AddRequest(request);
			}
		}

		public void AddRequest(Request r)
		{
			if (!m_requests.ContainsKey(r.m_type))
			{
				m_requests.Add(r.m_type, r);
			}
		}
	}

	private static readonly Map<Type, GetAccountInfo.Request> m_getAccountInfoTypeMap = new Map<Type, GetAccountInfo.Request>
	{
		{
			typeof(NetCacheDecks),
			GetAccountInfo.Request.DECK_LIST
		},
		{
			typeof(NetCacheMedalInfo),
			GetAccountInfo.Request.MEDAL_INFO
		},
		{
			typeof(NetCacheCardBacks),
			GetAccountInfo.Request.CARD_BACKS
		},
		{
			typeof(NetCachePlayerRecords),
			GetAccountInfo.Request.PLAYER_RECORD
		},
		{
			typeof(NetCacheGamesPlayed),
			GetAccountInfo.Request.GAMES_PLAYED
		},
		{
			typeof(NetCacheProfileProgress),
			GetAccountInfo.Request.CAMPAIGN_INFO
		},
		{
			typeof(NetCacheCardValues),
			GetAccountInfo.Request.CARD_VALUES
		},
		{
			typeof(NetCacheFeatures),
			GetAccountInfo.Request.FEATURES
		},
		{
			typeof(NetCacheRewardProgress),
			GetAccountInfo.Request.REWARD_PROGRESS
		},
		{
			typeof(NetCacheHeroLevels),
			GetAccountInfo.Request.HERO_XP
		},
		{
			typeof(NetCacheFavoriteHeroes),
			GetAccountInfo.Request.FAVORITE_HEROES
		},
		{
			typeof(NetCacheAccountLicenses),
			GetAccountInfo.Request.ACCOUNT_LICENSES
		},
		{
			typeof(NetCacheCoins),
			GetAccountInfo.Request.COINS
		}
	};

	private static readonly Map<Type, int> m_genericRequestTypeMap = new Map<Type, int> { 
	{
		typeof(ClientStaticAssetsResponse),
		340
	} };

	private static readonly List<Type> m_ServerInitiatedAccountInfoTypes = new List<Type>
	{
		typeof(NetCacheCollection),
		typeof(NetCacheClientOptions),
		typeof(NetCacheArcaneDustBalance),
		typeof(NetCacheGoldBalance),
		typeof(NetCacheProfileNotices),
		typeof(NetCacheBoosters),
		typeof(NetCacheDecks)
	};

	private static readonly Map<GetAccountInfo.Request, Type> m_requestTypeMap = GetInvertTypeMap();

	private Map<Type, object> m_netCache = new Map<Type, object>();

	private NetCacheHeroLevels m_prevHeroLevels;

	private NetCacheMedalInfo m_previousMedalInfo;

	private List<DelNewNoticesListener> m_newNoticesListeners = new List<DelNewNoticesListener>();

	private List<DelGoldBalanceListener> m_goldBalanceListeners = new List<DelGoldBalanceListener>();

	private Map<Type, HashSet<Action>> m_updatedListeners = new Map<Type, HashSet<Action>>();

	private Map<Type, int> m_changeRequests = new Map<Type, int>();

	private bool m_receivedInitialClientState;

	private HashSet<long> m_ackedNotices = new HashSet<long>();

	private List<ProfileNotice> m_queuedProfileNotices = new List<ProfileNotice>();

	private bool m_receivedInitialProfileNotices;

	private long m_currencyVersion;

	private long m_initialCollectionVersion;

	private HashSet<long> m_expectedCardModifications = new HashSet<long>();

	private HashSet<long> m_handledCardModifications = new HashSet<long>();

	private long m_lastForceCheckedSeason;

	private List<NetCacheBatchRequest> m_cacheRequests = new List<NetCacheBatchRequest>();

	private List<Type> m_inTransitRequests = new List<Type>();

	private static bool m_fatalErrorCodeSet = false;

	public bool HasReceivedInitialClientState => m_receivedInitialClientState;

	public event DelFavoriteCardBackChangedListener FavoriteCardBackChanged;

	public event DelFavoriteCoinChangedListener FavoriteCoinChanged;

	private static Map<GetAccountInfo.Request, Type> GetInvertTypeMap()
	{
		Map<GetAccountInfo.Request, Type> map = new Map<GetAccountInfo.Request, Type>();
		foreach (KeyValuePair<Type, GetAccountInfo.Request> item in m_getAccountInfoTypeMap)
		{
			map[item.Value] = item.Key;
		}
		return map;
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		serviceLocator.Get<Network>().RegisterThrottledPacketListener(OnPacketThrottled);
		RegisterNetCacheHandlers();
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(Network) };
	}

	public void Shutdown()
	{
	}

	public static NetCache Get()
	{
		return HearthstoneServices.Get<NetCache>();
	}

	public T GetNetObject<T>()
	{
		Type typeFromHandle = typeof(T);
		object value = GetTestData(typeFromHandle);
		if (value != null)
		{
			return (T)value;
		}
		if (m_netCache.TryGetValue(typeof(T), out value) && value is T)
		{
			return (T)value;
		}
		return default(T);
	}

	public bool IsNetObjectAvailable<T>()
	{
		return GetNetObject<T>() != null;
	}

	private object GetTestData(Type type)
	{
		if (type == typeof(NetCacheBoosters) && GameUtils.IsFakePackOpeningEnabled())
		{
			NetCacheBoosters netCacheBoosters = new NetCacheBoosters();
			int fakePackCount = GameUtils.GetFakePackCount();
			BoosterStack item = new BoosterStack
			{
				Id = 1,
				Count = fakePackCount
			};
			netCacheBoosters.BoosterStacks.Add(item);
			return netCacheBoosters;
		}
		return null;
	}

	public void UnloadNetObject<T>()
	{
		Type typeFromHandle = typeof(T);
		m_netCache[typeFromHandle] = null;
	}

	public void ReloadNetObject<T>()
	{
		NetCacheReload_Internal(null, typeof(T));
	}

	public void RefreshNetObject<T>()
	{
		RequestNetCacheObject(typeof(T));
	}

	public long GetArcaneDustBalance()
	{
		NetCacheArcaneDustBalance netObject = GetNetObject<NetCacheArcaneDustBalance>();
		if (netObject == null)
		{
			return 0L;
		}
		if (CraftingManager.IsInitialized)
		{
			return netObject.Balance + CraftingManager.Get().GetUnCommitedArcaneDustChanges();
		}
		return netObject.Balance;
	}

	public long GetGoldBalance()
	{
		return GetNetObject<NetCacheGoldBalance>()?.GetTotal() ?? 0;
	}

	public int GetArenaTicketBalance()
	{
		return GetNetObject<NetPlayerArenaTickets>()?.Balance ?? 0;
	}

	private bool GetOption<T>(ServerOption type, out T ret) where T : ClientOptionBase
	{
		ret = null;
		NetCacheClientOptions netObject = Get().GetNetObject<NetCacheClientOptions>();
		if (!ClientOptionExists(type))
		{
			return false;
		}
		T val = netObject.ClientState[type] as T;
		if (val == null)
		{
			return false;
		}
		ret = val;
		return true;
	}

	public int GetIntOption(ServerOption type)
	{
		ClientOptionInt ret = null;
		if (!GetOption<ClientOptionInt>(type, out ret))
		{
			return 0;
		}
		return ret.OptionValue;
	}

	public bool GetIntOption(ServerOption type, out int ret)
	{
		ret = 0;
		ClientOptionInt ret2 = null;
		if (!GetOption<ClientOptionInt>(type, out ret2))
		{
			return false;
		}
		ret = ret2.OptionValue;
		return true;
	}

	public long GetLongOption(ServerOption type)
	{
		ClientOptionLong ret = null;
		if (!GetOption<ClientOptionLong>(type, out ret))
		{
			return 0L;
		}
		return ret.OptionValue;
	}

	public bool GetLongOption(ServerOption type, out long ret)
	{
		ret = 0L;
		ClientOptionLong ret2 = null;
		if (!GetOption<ClientOptionLong>(type, out ret2))
		{
			return false;
		}
		ret = ret2.OptionValue;
		return true;
	}

	public float GetFloatOption(ServerOption type)
	{
		ClientOptionFloat ret = null;
		if (!GetOption<ClientOptionFloat>(type, out ret))
		{
			return 0f;
		}
		return ret.OptionValue;
	}

	public bool GetFloatOption(ServerOption type, out float ret)
	{
		ret = 0f;
		ClientOptionFloat ret2 = null;
		if (!GetOption<ClientOptionFloat>(type, out ret2))
		{
			return false;
		}
		ret = ret2.OptionValue;
		return true;
	}

	public ulong GetULongOption(ServerOption type)
	{
		ClientOptionULong ret = null;
		if (!GetOption<ClientOptionULong>(type, out ret))
		{
			return 0uL;
		}
		return ret.OptionValue;
	}

	public bool GetULongOption(ServerOption type, out ulong ret)
	{
		ret = 0uL;
		ClientOptionULong ret2 = null;
		if (!GetOption<ClientOptionULong>(type, out ret2))
		{
			return false;
		}
		ret = ret2.OptionValue;
		return true;
	}

	public void RegisterUpdatedListener(Type type, Action listener)
	{
		if (listener != null)
		{
			if (!m_updatedListeners.TryGetValue(type, out var value))
			{
				value = new HashSet<Action>();
				m_updatedListeners[type] = value;
			}
			m_updatedListeners[type].Add(listener);
		}
	}

	public void RemoveUpdatedListener(Type type, Action listener)
	{
		if (listener != null && m_updatedListeners.TryGetValue(type, out var value))
		{
			value.Remove(listener);
		}
	}

	public void RegisterNewNoticesListener(DelNewNoticesListener listener)
	{
		if (!m_newNoticesListeners.Contains(listener))
		{
			m_newNoticesListeners.Add(listener);
		}
	}

	public void RemoveNewNoticesListener(DelNewNoticesListener listener)
	{
		m_newNoticesListeners.Remove(listener);
	}

	public bool RemoveNotice(long ID)
	{
		NetCacheProfileNotices netCacheProfileNotices = m_netCache[typeof(NetCacheProfileNotices)] as NetCacheProfileNotices;
		if (netCacheProfileNotices == null)
		{
			Debug.LogWarning($"NetCache.RemoveNotice({ID}) - profileNotices is null");
			return false;
		}
		if (netCacheProfileNotices.Notices == null)
		{
			Debug.LogWarning($"NetCache.RemoveNotice({ID}) - profileNotices.Notices is null");
			return false;
		}
		ProfileNotice profileNotice = netCacheProfileNotices.Notices.Find((ProfileNotice obj) => obj.NoticeID == ID);
		if (profileNotice == null)
		{
			return false;
		}
		netCacheProfileNotices.Notices.Remove(profileNotice);
		m_ackedNotices.Add(profileNotice.NoticeID);
		return true;
	}

	public void NetCacheChanged<T>()
	{
		Type typeFromHandle = typeof(T);
		int value = 0;
		m_changeRequests.TryGetValue(typeFromHandle, out value);
		value++;
		m_changeRequests[typeFromHandle] = value;
		if (value <= 1)
		{
			while (m_changeRequests[typeFromHandle] > 0)
			{
				NetCacheChangedImpl<T>();
				m_changeRequests[typeFromHandle] -= 1;
			}
		}
	}

	private void NetCacheChangedImpl<T>()
	{
		NetCacheBatchRequest[] array = m_cacheRequests.ToArray();
		foreach (NetCacheBatchRequest netCacheBatchRequest in array)
		{
			foreach (KeyValuePair<Type, Request> request in netCacheBatchRequest.m_requests)
			{
				if (!(request.Key != typeof(T)))
				{
					NetCacheCheckRequest(netCacheBatchRequest);
					break;
				}
			}
		}
	}

	public void CheckSeasonForRoll()
	{
		if (GetNetObject<NetCacheProfileNotices>() == null)
		{
			return;
		}
		NetCacheRewardProgress netObject = GetNetObject<NetCacheRewardProgress>();
		if (netObject != null)
		{
			DateTime utcNow = DateTime.UtcNow;
			DateTime dateTime = DateTime.FromFileTimeUtc(netObject.SeasonEndDate);
			if (!(dateTime >= utcNow) && m_lastForceCheckedSeason != netObject.Season)
			{
				m_lastForceCheckedSeason = netObject.Season;
				Log.Net.Print("NetCache.CheckSeasonForRoll oldSeason = {0} season end = {1} utc now = {2}", m_lastForceCheckedSeason, dateTime, utcNow);
			}
		}
	}

	public void RegisterGoldBalanceListener(DelGoldBalanceListener listener)
	{
		if (!m_goldBalanceListeners.Contains(listener))
		{
			m_goldBalanceListeners.Add(listener);
		}
	}

	public void RemoveGoldBalanceListener(DelGoldBalanceListener listener)
	{
		m_goldBalanceListeners.Remove(listener);
	}

	public static void DefaultErrorHandler(ErrorInfo info)
	{
		if (info.Error == ErrorCode.TIMEOUT)
		{
			if (BreakingNews.SHOWS_BREAKING_NEWS)
			{
				string error = "GLOBAL_ERROR_NETWORK_UTIL_TIMEOUT";
				Network.Get().ShowBreakingNewsOrError(error);
			}
			else
			{
				ShowError(info, "GLOBAL_ERROR_NETWORK_UTIL_TIMEOUT");
			}
		}
		else
		{
			ShowError(info, "GLOBAL_ERROR_NETWORK_GENERIC");
		}
	}

	public static void ShowError(ErrorInfo info, string localizationKey, params object[] localizationArgs)
	{
		Error.AddFatal(FatalErrorReason.NET_CACHE, localizationKey, localizationArgs);
		Debug.LogError(GetInternalErrorMessage(info));
	}

	public static string GetInternalErrorMessage(ErrorInfo info, bool includeStackTrace = true)
	{
		Map<Type, object> netCache = Get().m_netCache;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("NetCache Error: {0}", info.Error);
		stringBuilder.AppendFormat("\nFrom: {0}", info.RequestingFunction.Method.Name);
		stringBuilder.AppendFormat("\nRequested Data ({0}):", info.RequestedTypes.Count);
		foreach (KeyValuePair<Type, Request> requestedType in info.RequestedTypes)
		{
			object value = null;
			netCache.TryGetValue(requestedType.Key, out value);
			if (value == null)
			{
				stringBuilder.AppendFormat("\n[{0}] MISSING", requestedType.Key);
			}
			else
			{
				stringBuilder.AppendFormat("\n[{0}]", requestedType.Key);
			}
		}
		if (includeStackTrace)
		{
			stringBuilder.AppendFormat("\nStack Trace:\n{0}", info.RequestStackTrace);
		}
		return stringBuilder.ToString();
	}

	private void NetCacheMakeBatchRequest(NetCacheBatchRequest batchRequest)
	{
		List<GetAccountInfo.Request> list = new List<GetAccountInfo.Request>();
		List<GenericRequest> list2 = null;
		foreach (KeyValuePair<Type, Request> request in batchRequest.m_requests)
		{
			Request value = request.Value;
			if (value == null)
			{
				Debug.LogError($"NetUseBatchRequest Null request for {value.m_type.Name}...SKIP");
				continue;
			}
			if (m_ServerInitiatedAccountInfoTypes.Contains(value.m_type))
			{
				if (value.m_reload)
				{
					Log.All.PrintWarning("Attempting to reload server-initiated NetCache request {0}. This is not valid - the server sends this data when it changes!", value.m_type.FullName);
				}
				continue;
			}
			if (value.m_reload)
			{
				m_netCache[value.m_type] = null;
			}
			if ((m_netCache.ContainsKey(value.m_type) && m_netCache[value.m_type] != null) || m_inTransitRequests.Contains(value.m_type))
			{
				continue;
			}
			value.m_result = RequestResult.PENDING;
			m_inTransitRequests.Add(value.m_type);
			int value3;
			if (m_getAccountInfoTypeMap.TryGetValue(value.m_type, out var value2))
			{
				list.Add(value2);
			}
			else if (m_genericRequestTypeMap.TryGetValue(value.m_type, out value3))
			{
				if (list2 == null)
				{
					list2 = new List<GenericRequest>();
				}
				GenericRequest genericRequest = new GenericRequest();
				genericRequest.RequestId = value3;
				list2.Add(genericRequest);
			}
			else
			{
				Log.Net.Print("NetCache: Unable to make request for type={0}", value.m_type.FullName);
			}
		}
		if (list.Count > 0 || list2 != null)
		{
			Network.Get().RequestNetCacheObjectList(list, list2);
		}
		if (m_cacheRequests.FindIndex((NetCacheBatchRequest o) => o.m_callback != null && o.m_callback == batchRequest.m_callback) >= 0)
		{
			Log.Net.PrintError("NetCache: detected multiple registrations for same callback! {0}.{1}", batchRequest.m_callback.Target.GetType().Name, batchRequest.m_callback.Method.Name);
		}
		m_cacheRequests.Add(batchRequest);
		NetCacheCheckRequest(batchRequest);
	}

	private void NetCacheUse_Internal(NetCacheBatchRequest request, Type type)
	{
		if (request != null && request.m_requests.ContainsKey(type))
		{
			Log.Net.Print($"NetCache ...SKIP {type.Name}");
			return;
		}
		if (m_netCache.ContainsKey(type) && m_netCache[type] != null)
		{
			Log.Net.Print($"NetCache ...USE {type.Name}");
			return;
		}
		Log.Net.Print($"NetCache <<<GET {type.Name}");
		RequestNetCacheObject(type);
	}

	private void RequestNetCacheObject(Type type)
	{
		if (!m_inTransitRequests.Contains(type))
		{
			m_inTransitRequests.Add(type);
			Network.Get().RequestNetCacheObject(m_getAccountInfoTypeMap[type]);
		}
	}

	private void NetCacheReload_Internal(NetCacheBatchRequest request, Type type)
	{
		m_netCache[type] = null;
		if (type == typeof(NetCacheProfileNotices))
		{
			Debug.LogError("NetCacheReload_Internal - tried to issue request with type NetCacheProfileNotices - this is no longer allowed!");
		}
		else
		{
			NetCacheUse_Internal(request, type);
		}
	}

	private void NetCacheCheckRequest(NetCacheBatchRequest request)
	{
		foreach (KeyValuePair<Type, Request> request2 in request.m_requests)
		{
			if (!m_netCache.ContainsKey(request2.Key) || m_netCache[request2.Key] == null)
			{
				return;
			}
		}
		request.m_canTimeout = false;
		if (request.m_callback != null)
		{
			request.m_callback();
		}
	}

	private void UpdateRequestNeedState(Type type, RequestResult result)
	{
		foreach (NetCacheBatchRequest cacheRequest in m_cacheRequests)
		{
			if (cacheRequest.m_requests.ContainsKey(type))
			{
				cacheRequest.m_requests[type].m_result = result;
			}
		}
	}

	private void OnNetCacheObjReceived<T>(T netCacheObject)
	{
		Type typeFromHandle = typeof(T);
		Log.Net.Print($"OnNetCacheObjReceived SAVE --> {typeFromHandle.Name}");
		UpdateRequestNeedState(typeFromHandle, RequestResult.DATA_COMPLETE);
		m_netCache[typeFromHandle] = netCacheObject;
		m_inTransitRequests.Remove(typeFromHandle);
		NetCacheChanged<T>();
		if (m_updatedListeners.TryGetValue(typeFromHandle, out var value))
		{
			Action[] array = value.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i]();
			}
		}
	}

	public void Clear()
	{
		Log.Net.PrintDebug("Clearing NetCache");
		m_netCache.Clear();
		m_prevHeroLevels = null;
		m_previousMedalInfo = null;
		m_changeRequests.Clear();
		m_cacheRequests.Clear();
		m_inTransitRequests.Clear();
		m_receivedInitialClientState = false;
		m_ackedNotices.Clear();
		m_queuedProfileNotices.Clear();
		m_receivedInitialProfileNotices = false;
		m_currencyVersion = 0L;
		m_initialCollectionVersion = 0L;
		m_expectedCardModifications.Clear();
		m_handledCardModifications.Clear();
		if (HearthstoneApplication.IsInternal() && HearthstoneServices.TryGet<SceneDebugger>(out var service))
		{
			service.SetPlayerId(null);
		}
	}

	public void ClearForNewAuroraConnection()
	{
		m_cacheRequests.Clear();
		m_inTransitRequests.Clear();
		m_receivedInitialClientState = false;
	}

	public void UnregisterNetCacheHandler(NetCacheCallback handler)
	{
		m_cacheRequests.RemoveAll((NetCacheBatchRequest o) => o.m_callback == handler);
	}

	public void Update()
	{
		if (!Network.IsRunning())
		{
			return;
		}
		NetCacheBatchRequest[] array = m_cacheRequests.ToArray();
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		NetCacheBatchRequest[] array2 = array;
		foreach (NetCacheBatchRequest netCacheBatchRequest in array2)
		{
			if (!netCacheBatchRequest.m_canTimeout || realtimeSinceStartup - netCacheBatchRequest.m_timeAdded < Network.GetMaxDeferredWait() || Network.Get().HaveUnhandledPackets())
			{
				continue;
			}
			netCacheBatchRequest.m_canTimeout = false;
			if (m_fatalErrorCodeSet)
			{
				continue;
			}
			ErrorInfo errorInfo = new ErrorInfo();
			errorInfo.Error = ErrorCode.TIMEOUT;
			errorInfo.RequestingFunction = netCacheBatchRequest.m_requestFunc;
			errorInfo.RequestedTypes = new Map<Type, Request>(netCacheBatchRequest.m_requests);
			errorInfo.RequestStackTrace = netCacheBatchRequest.m_requestStackTrace;
			string text = "CT";
			int num = 0;
			foreach (KeyValuePair<Type, Request> request in netCacheBatchRequest.m_requests)
			{
				RequestResult result = request.Value.m_result;
				if ((uint)(result - 3) > 1u)
				{
					string[] array3 = request.Value.m_type.ToString().Split('+');
					if (array3.GetLength(0) != 0)
					{
						string text2 = array3[array3.GetLength(0) - 1];
						text = text + ";" + text2 + "=" + (int)request.Value.m_result;
						num++;
					}
				}
				if (num >= 3)
				{
					break;
				}
			}
			FatalErrorMgr.Get().SetErrorCode("HS", text);
			m_fatalErrorCodeSet = true;
			netCacheBatchRequest.m_errorCallback(errorInfo);
		}
		CheckSeasonForRoll();
	}

	private void OnGenericResponse()
	{
		Network.GenericResponse genericResponse = Network.Get().GetGenericResponse();
		if (genericResponse == null)
		{
			Debug.LogError($"NetCache - GenericResponse parse error");
		}
		else
		{
			if ((long)genericResponse.RequestId != 201)
			{
				return;
			}
			if (!m_requestTypeMap.TryGetValue((GetAccountInfo.Request)genericResponse.RequestSubId, out var value))
			{
				Debug.LogError($"NetCache - Ignoring unexpected requestId={genericResponse.RequestId}:{genericResponse.RequestSubId}");
				return;
			}
			NetCacheBatchRequest[] array = m_cacheRequests.ToArray();
			foreach (NetCacheBatchRequest netCacheBatchRequest in array)
			{
				if (!netCacheBatchRequest.m_requests.ContainsKey(value))
				{
					continue;
				}
				switch (genericResponse.ResultCode)
				{
				case Network.GenericResponse.Result.RESULT_REQUEST_IN_PROCESS:
					if (RequestResult.PENDING == netCacheBatchRequest.m_requests[value].m_result)
					{
						netCacheBatchRequest.m_requests[value].m_result = RequestResult.IN_PROCESS;
					}
					continue;
				case Network.GenericResponse.Result.RESULT_REQUEST_COMPLETE:
					netCacheBatchRequest.m_requests[value].m_result = RequestResult.GENERIC_COMPLETE;
					Debug.LogWarning($"GenericResponse Success for requestId={genericResponse.RequestId}:{genericResponse.RequestSubId}");
					continue;
				case Network.GenericResponse.Result.RESULT_DATA_MIGRATION_REQUIRED:
					netCacheBatchRequest.m_requests[value].m_result = RequestResult.MIGRATION_REQUIRED;
					Debug.LogWarning($"GenericResponse player migration required code={(int)genericResponse.ResultCode} {genericResponse.ResultCode.ToString()} for requestId={genericResponse.RequestId}:{genericResponse.RequestSubId}");
					continue;
				}
				Debug.LogError($"Unhandled failure code={(int)genericResponse.ResultCode} {genericResponse.ResultCode.ToString()} for requestId={genericResponse.RequestId}:{genericResponse.RequestSubId}");
				netCacheBatchRequest.m_requests[value].m_result = RequestResult.ERROR;
				ErrorInfo errorInfo = new ErrorInfo();
				errorInfo.Error = ErrorCode.SERVER;
				errorInfo.ServerError = (uint)genericResponse.ResultCode;
				errorInfo.RequestingFunction = netCacheBatchRequest.m_requestFunc;
				errorInfo.RequestedTypes = new Map<Type, Request>(netCacheBatchRequest.m_requests);
				errorInfo.RequestStackTrace = netCacheBatchRequest.m_requestStackTrace;
				FatalErrorMgr.Get().SetErrorCode("HS", "CG" + genericResponse.ResultCode, genericResponse.RequestId.ToString(), genericResponse.RequestSubId.ToString());
				netCacheBatchRequest.m_errorCallback(errorInfo);
			}
		}
	}

	private void OnDBAction()
	{
		Network.DBAction dbAction = Network.Get().GetDbAction();
		if (Network.DBAction.ResultType.SUCCESS != dbAction.Result)
		{
			Debug.LogError($"Unhandled dbAction {dbAction.Action} with error {dbAction.Result}");
		}
	}

	public void FakeInitialClientState()
	{
		OnInitialClientState();
	}

	private void OnInitialClientState()
	{
		InitialClientState initialClientState = Network.Get().GetInitialClientState();
		if (initialClientState == null)
		{
			return;
		}
		m_receivedInitialClientState = true;
		if (initialClientState.HasGuardianVars)
		{
			OnGuardianVars(initialClientState.GuardianVars);
		}
		if (initialClientState.SpecialEventTiming.Count > 0)
		{
			long devTimeOffsetSeconds = (initialClientState.HasDevTimeOffsetSeconds ? initialClientState.DevTimeOffsetSeconds : 0);
			SpecialEventManager.Get().InitEventTimingsFromServer(devTimeOffsetSeconds, initialClientState.SpecialEventTiming);
		}
		if (initialClientState.HasClientOptions)
		{
			OnClientOptions(initialClientState.ClientOptions);
		}
		if (initialClientState.HasCollection)
		{
			OnCollection(initialClientState.Collection);
		}
		else
		{
			OnCollection(OfflineDataCache.GetCachedCollection());
		}
		if (initialClientState.HasAchievements)
		{
			AchieveManager.Get().OnInitialAchievements(initialClientState.Achievements);
		}
		if (initialClientState.HasNotices)
		{
			OnInitialClientState_ProfileNotices(initialClientState.Notices);
		}
		if (initialClientState.HasGameCurrencyStates)
		{
			OnCurrencyState(initialClientState.GameCurrencyStates);
		}
		if (initialClientState.HasBoosters)
		{
			OnBoosters(initialClientState.Boosters);
		}
		if (initialClientState.HasPlayerDraftTickets)
		{
			OnPlayerDraftTickets(initialClientState.PlayerDraftTickets);
		}
		foreach (TavernBrawlInfo tavernBrawls in initialClientState.TavernBrawlsList)
		{
			PegasusPacket packet = new PegasusPacket(316, 0, tavernBrawls);
			Network.Get().SimulateReceivedPacketFromServer(packet);
		}
		if (initialClientState.HasDisconnectedGame)
		{
			OnDisconnectedGame(initialClientState.DisconnectedGame);
		}
		if (initialClientState.HasArenaSession)
		{
			PegasusPacket packet2 = new PegasusPacket(351, 0, initialClientState.ArenaSession);
			Network.Get().SimulateReceivedPacketFromServer(packet2);
		}
		if (initialClientState.HasDisplayBanner)
		{
			OnDisplayBanner(initialClientState.DisplayBanner);
		}
		if (initialClientState.Decks != null)
		{
			OnReceivedDeckHeaders_InitialClientState(initialClientState.Decks, initialClientState.DeckContents, initialClientState.ValidCachedDeckIds);
		}
		if (initialClientState.MedalInfo != null)
		{
			OnMedalInfo(initialClientState.MedalInfo);
		}
		if (initialClientState.GameSaveData != null)
		{
			GameSaveDataManager.Get().SetGameSaveDataUpdateFromInitialClientState(initialClientState.GameSaveData);
		}
		if (HearthstoneApplication.IsInternal() && initialClientState.HasPlayerId)
		{
			if (!HearthstoneServices.TryGet<SceneDebugger>(out var service))
			{
				return;
			}
			service.SetPlayerId(initialClientState.PlayerId);
		}
		if (Network.Get() != null)
		{
			Network.Get().OnInitialClientStateProcessed();
		}
	}

	public void OnCollection(Collection collection)
	{
		m_initialCollectionVersion = collection.CollectionVersion;
		if (CollectionManager.Get() != null)
		{
			OnNetCacheObjReceived(CollectionManager.Get().OnInitialCollectionReceived(collection));
		}
		OfflineDataCache.CacheCollection(collection);
	}

	private void OnBoosters(Boosters boosters)
	{
		NetCacheBoosters netCacheBoosters = new NetCacheBoosters();
		for (int i = 0; i < boosters.List.Count; i++)
		{
			BoosterInfo boosterInfo = boosters.List[i];
			BoosterStack item = new BoosterStack
			{
				Id = boosterInfo.Type,
				Count = boosterInfo.Count,
				EverGrantedCount = boosterInfo.EverGrantedCount
			};
			netCacheBoosters.BoosterStacks.Add(item);
		}
		OnNetCacheObjReceived(netCacheBoosters);
	}

	public void OnPlayerDraftTickets(PlayerDraftTickets playerDraftTickets)
	{
		NetPlayerArenaTickets netPlayerArenaTickets = new NetPlayerArenaTickets();
		netPlayerArenaTickets.Balance = playerDraftTickets.UnusedTicketBalance;
		OnNetCacheObjReceived(netPlayerArenaTickets);
	}

	private void OnDisconnectedGame(GameConnectionInfo packet)
	{
		if (packet.HasAddress)
		{
			NetCacheDisconnectedGame netCacheDisconnectedGame = new NetCacheDisconnectedGame();
			netCacheDisconnectedGame.ServerInfo = new GameServerInfo();
			netCacheDisconnectedGame.ServerInfo.Address = packet.Address;
			netCacheDisconnectedGame.ServerInfo.GameHandle = (uint)packet.GameHandle;
			netCacheDisconnectedGame.ServerInfo.ClientHandle = packet.ClientHandle;
			netCacheDisconnectedGame.ServerInfo.Port = (uint)packet.Port;
			netCacheDisconnectedGame.ServerInfo.AuroraPassword = packet.AuroraPassword;
			netCacheDisconnectedGame.ServerInfo.Mission = packet.Scenario;
			netCacheDisconnectedGame.ServerInfo.BrawlLibraryItemId = packet.BrawlLibraryItemId;
			netCacheDisconnectedGame.ServerInfo.Version = BattleNet.GetVersion();
			netCacheDisconnectedGame.ServerInfo.Resumable = true;
			netCacheDisconnectedGame.GameType = packet.GameType;
			netCacheDisconnectedGame.FormatType = packet.FormatType;
			if (packet.HasLoadGameState)
			{
				netCacheDisconnectedGame.LoadGameState = packet.LoadGameState;
			}
			else
			{
				netCacheDisconnectedGame.LoadGameState = false;
			}
			OnNetCacheObjReceived(netCacheDisconnectedGame);
		}
	}

	private void OnDisplayBanner(int displayBanner)
	{
		NetCacheDisplayBanner netCacheDisplayBanner = new NetCacheDisplayBanner();
		netCacheDisplayBanner.Id = displayBanner;
		OnNetCacheObjReceived(netCacheDisplayBanner);
	}

	private void OnReceivedDeckHeaders()
	{
		NetCacheDecks deckHeaders = Network.Get().GetDeckHeaders();
		OnNetCacheObjReceived(deckHeaders);
	}

	private void OnReceivedDeckHeaders_InitialClientState(List<DeckInfo> deckHeaders, List<DeckContents> deckContents, List<long> validCachedDeckIds)
	{
		foreach (DeckInfo fakeDeckInfo in OfflineDataCache.GetFakeDeckInfos())
		{
			deckHeaders.Add(fakeDeckInfo);
		}
		NetCacheDecks deckHeaders2 = Network.GetDeckHeaders(deckHeaders);
		OnNetCacheObjReceived(deckHeaders2);
		Network.Get().ReconcileDeckContentsForChangedOfflineDecks(deckHeaders, deckContents, validCachedDeckIds);
		CollectionManager.Get().OnInitialClientStateDeckContents(deckHeaders2, OfflineDataCache.GetLocalDeckContentsFromCache());
	}

	public List<DeckInfo> GetDeckListFromNetCache()
	{
		return GetNetObject<NetCacheDecks>().Decks.Select((DeckHeader h) => Network.GetDeckInfoFromDeckHeader(h)).ToList();
	}

	private void OnCardValues()
	{
		NetCacheCardValues netCacheCardValues = Get().GetNetObject<NetCacheCardValues>();
		if (netCacheCardValues == null)
		{
			netCacheCardValues = new NetCacheCardValues();
		}
		CardValues cardValues = Network.Get().GetCardValues();
		if (cardValues != null)
		{
			foreach (PegasusUtil.CardValue card in cardValues.Cards)
			{
				string text = GameUtils.TranslateDbIdToCardId(card.Card.Asset);
				if (text == null)
				{
					Log.All.PrintError("NetCache.OnCardValues(): Cannot find card '{0}' in card manifest.  Confirm your card manifest matches your game server's database.", card.Card.Asset);
					continue;
				}
				netCacheCardValues.Values.Add(new CardDefinition
				{
					Name = text,
					Premium = (TAG_PREMIUM)card.Card.Premium
				}, new CardValue
				{
					BaseBuyValue = card.Buy,
					BaseSellValue = card.Sell,
					BuyValueOverride = (card.HasBuyValueOverride ? card.BuyValueOverride : 0),
					SellValueOverride = (card.HasSellValueOverride ? card.SellValueOverride : 0),
					OverrideEvent = (card.HasOverrideEventName ? SpecialEventManager.GetEventType(card.OverrideEventName) : SpecialEventType.SPECIAL_EVENT_NEVER)
				});
			}
		}
		OnNetCacheObjReceived(netCacheCardValues);
	}

	private void OnMedalInfo()
	{
		NetCacheMedalInfo medalInfo = Network.Get().GetMedalInfo();
		if (m_previousMedalInfo != null)
		{
			medalInfo.PreviousMedalInfo = m_previousMedalInfo.Clone();
		}
		m_previousMedalInfo = medalInfo;
		OnNetCacheObjReceived(medalInfo);
	}

	private void OnMedalInfo(MedalInfo packet)
	{
		NetCacheMedalInfo netCacheMedalInfo = new NetCacheMedalInfo(packet);
		if (m_previousMedalInfo != null)
		{
			netCacheMedalInfo.PreviousMedalInfo = m_previousMedalInfo.Clone();
		}
		m_previousMedalInfo = netCacheMedalInfo;
		OnNetCacheObjReceived(netCacheMedalInfo);
	}

	private void OnBaconRatingInfo()
	{
		NetCacheBaconRatingInfo baconRatingInfo = Network.Get().GetBaconRatingInfo();
		OnNetCacheObjReceived(baconRatingInfo);
	}

	private void OnBaconPremiumStatus()
	{
		NetCacheBaconPremiumStatus baconPremiumStatus = Network.Get().GetBaconPremiumStatus();
		OnNetCacheObjReceived(baconPremiumStatus);
	}

	public long GetBattlegroundsEarlyAccessLicenseId()
	{
		NetCacheFeatures netObject = GetNetObject<NetCacheFeatures>();
		if (netObject != null)
		{
			return netObject.BattlegroundsEarlyAccessLicense;
		}
		return 50336L;
	}

	public long GetDuelsEarlyAccessLicenseId()
	{
		NetCacheFeatures netObject = GetNetObject<NetCacheFeatures>();
		if (netObject != null)
		{
			return netObject.DuelsEarlyAccessLicense;
		}
		return 77345L;
	}

	private void OnPVPDRStatsInfo()
	{
		NetCachePVPDRStatsInfo pVPDRStatsInfo = Network.Get().GetPVPDRStatsInfo();
		OnNetCacheObjReceived(pVPDRStatsInfo);
	}

	private void OnGuardianVars()
	{
		GuardianVars guardianVars = Network.Get().GetGuardianVars();
		if (guardianVars != null)
		{
			OnGuardianVars(guardianVars);
		}
	}

	private void OnGuardianVars(GuardianVars packet)
	{
		NetCacheFeatures netCacheFeatures = new NetCacheFeatures();
		netCacheFeatures.Games.Tournament = !packet.HasTourney || packet.Tourney;
		netCacheFeatures.Games.Practice = !packet.HasPractice || packet.Practice;
		netCacheFeatures.Games.Casual = !packet.HasCasual || packet.Casual;
		netCacheFeatures.Games.Forge = !packet.HasForge || packet.Forge;
		netCacheFeatures.Games.Friendly = !packet.HasFriendly || packet.Friendly;
		netCacheFeatures.Games.TavernBrawl = !packet.HasTavernBrawl || packet.TavernBrawl;
		netCacheFeatures.Games.Battlegrounds = !packet.HasBattlegrounds || packet.Battlegrounds;
		netCacheFeatures.Games.BattlegroundsFriendlyChallenge = !packet.HasBattlegroundsFriendlyChallenge || packet.BattlegroundsFriendlyChallenge;
		netCacheFeatures.Games.BattlegroundsTutorial = !packet.HasBattlegroundsTutorial || packet.BattlegroundsTutorial;
		netCacheFeatures.Games.ShowUserUI = (packet.HasShowUserUI ? packet.ShowUserUI : 0);
		netCacheFeatures.Games.Duels = !packet.HasDuels || packet.Duels;
		netCacheFeatures.Games.PaidDuels = !packet.HasPaidDuels || packet.PaidDuels;
		netCacheFeatures.Collection.Manager = !packet.HasManager || packet.Manager;
		netCacheFeatures.Collection.Crafting = !packet.HasCrafting || packet.Crafting;
		netCacheFeatures.Collection.DeckReordering = !packet.HasDeckReordering || packet.DeckReordering;
		netCacheFeatures.Store.Store = !packet.HasStore || packet.Store;
		netCacheFeatures.Store.BattlePay = !packet.HasBattlePay || packet.BattlePay;
		netCacheFeatures.Store.BuyWithGold = !packet.HasBuyWithGold || packet.BuyWithGold;
		netCacheFeatures.Store.SimpleCheckout = !packet.HasSimpleCheckout || packet.SimpleCheckout;
		netCacheFeatures.Store.SoftAccountPurchasing = !packet.HasSoftAccountPurchasing || packet.SoftAccountPurchasing;
		netCacheFeatures.Store.VirtualCurrencyEnabled = packet.HasVirtualCurrencyEnabled && packet.VirtualCurrencyEnabled;
		netCacheFeatures.Store.NumClassicPacksUntilDeprioritize = (packet.HasNumClassicPacksUntilDeprioritize ? packet.NumClassicPacksUntilDeprioritize : (-1));
		netCacheFeatures.Store.SimpleCheckoutIOS = !packet.HasSimpleCheckoutIos || packet.SimpleCheckoutIos;
		netCacheFeatures.Store.SimpleCheckoutAndroidAmazon = !packet.HasSimpleCheckoutAndroidAmazon || packet.SimpleCheckoutAndroidAmazon;
		netCacheFeatures.Store.SimpleCheckoutAndroidGoogle = !packet.HasSimpleCheckoutAndroidGoogle || packet.SimpleCheckoutAndroidGoogle;
		netCacheFeatures.Store.SimpleCheckoutAndroidGlobal = !packet.HasSimpleCheckoutAndroidGlobal || packet.SimpleCheckoutAndroidGlobal;
		netCacheFeatures.Store.SimpleCheckoutWin = !packet.HasSimpleCheckoutWin || packet.SimpleCheckoutWin;
		netCacheFeatures.Store.SimpleCheckoutMac = !packet.HasSimpleCheckoutMac || packet.SimpleCheckoutMac;
		netCacheFeatures.Store.BoosterRotatingSoonWarnDaysWithoutSale = (packet.HasBoosterRotatingSoonWarnDaysWithoutSale ? packet.BoosterRotatingSoonWarnDaysWithoutSale : 0);
		netCacheFeatures.Store.BoosterRotatingSoonWarnDaysWithSale = (packet.HasBoosterRotatingSoonWarnDaysWithSale ? packet.BoosterRotatingSoonWarnDaysWithSale : 0);
		netCacheFeatures.Store.VintageStore = !packet.HasVintageStoreEnabled || packet.VintageStoreEnabled;
		netCacheFeatures.Store.BuyCardBacksFromCollectionManager = !packet.HasBuyCardBacksFromCollectionManagerEnabled || packet.BuyCardBacksFromCollectionManagerEnabled;
		netCacheFeatures.Store.BuyHeroSkinsFromCollectionManager = !packet.HasBuyHeroSkinsFromCollectionManagerEnabled || packet.BuyHeroSkinsFromCollectionManagerEnabled;
		netCacheFeatures.Heroes.Hunter = !packet.HasHunter || packet.Hunter;
		netCacheFeatures.Heroes.Mage = !packet.HasMage || packet.Mage;
		netCacheFeatures.Heroes.Paladin = !packet.HasPaladin || packet.Paladin;
		netCacheFeatures.Heroes.Priest = !packet.HasPriest || packet.Priest;
		netCacheFeatures.Heroes.Rogue = !packet.HasRogue || packet.Rogue;
		netCacheFeatures.Heroes.Shaman = !packet.HasShaman || packet.Shaman;
		netCacheFeatures.Heroes.Warlock = !packet.HasWarlock || packet.Warlock;
		netCacheFeatures.Heroes.Warrior = !packet.HasWarrior || packet.Warrior;
		netCacheFeatures.Misc.ClientOptionsUpdateIntervalSeconds = (packet.HasClientOptionsUpdateIntervalSeconds ? packet.ClientOptionsUpdateIntervalSeconds : 0);
		netCacheFeatures.Misc.AllowLiveFPSGathering = packet.HasAllowLiveFpsGathering && packet.AllowLiveFpsGathering;
		netCacheFeatures.CaisEnabledNonMobile = !packet.HasCaisEnabledNonMobile || packet.CaisEnabledNonMobile;
		netCacheFeatures.CaisEnabledMobileChina = packet.HasCaisEnabledMobileChina && packet.CaisEnabledMobileChina;
		netCacheFeatures.CaisEnabledMobileSouthKorea = packet.HasCaisEnabledMobileSouthKorea && packet.CaisEnabledMobileSouthKorea;
		netCacheFeatures.SendTelemetryPresence = packet.HasSendTelemetryPresence && packet.SendTelemetryPresence;
		netCacheFeatures.WinsPerGold = packet.WinsPerGold;
		netCacheFeatures.GoldPerReward = packet.GoldPerReward;
		netCacheFeatures.MaxGoldPerDay = packet.MaxGoldPerDay;
		netCacheFeatures.XPSoloLimit = packet.XpSoloLimit;
		netCacheFeatures.MaxHeroLevel = packet.MaxHeroLevel;
		netCacheFeatures.SpecialEventTimingMod = packet.EventTimingMod;
		netCacheFeatures.FriendWeekConcederMaxDefense = packet.FriendWeekConcederMaxDefense;
		netCacheFeatures.FriendWeekConcededGameMinTotalTurns = packet.FriendWeekConcededGameMinTotalTurns;
		netCacheFeatures.FriendWeekAllowsTavernBrawlRecordUpdate = packet.FriendWeekAllowsTavernBrawlRecordUpdate;
		netCacheFeatures.FSGEnabled = packet.HasFsgEnabled && packet.FsgEnabled;
		netCacheFeatures.FSGLoginScanEnabled = packet.HasFsgLoginScanEnabled && packet.FsgLoginScanEnabled;
		netCacheFeatures.FSGAutoCheckinEnabled = packet.HasFsgAutoCheckinEnabled && packet.FsgAutoCheckinEnabled;
		netCacheFeatures.FSGShowBetaLabel = packet.HasFsgShowBetaLabel && packet.FsgShowBetaLabel;
		netCacheFeatures.FSGFriendListPatronCountLimit = (packet.HasFsgFriendListPatronCountLimit ? packet.FsgFriendListPatronCountLimit : (-1));
		netCacheFeatures.ArenaClosedToNewSessionsSeconds = (packet.HasArenaClosedToNewSessionsSeconds ? packet.ArenaClosedToNewSessionsSeconds : 0u);
		netCacheFeatures.PVPDRClosedToNewSessionsSeconds = (packet.HasPvpdrClosedToNewSessionsSeconds ? packet.PvpdrClosedToNewSessionsSeconds : 0u);
		netCacheFeatures.FsgMaxPresencePubscribedPatronCount = (packet.HasFsgMaxPresencePubscribedPatronCount ? packet.FsgMaxPresencePubscribedPatronCount : (-1));
		netCacheFeatures.QuickPackOpeningAllowed = packet.HasQuickPackOpeningAllowed && packet.QuickPackOpeningAllowed;
		netCacheFeatures.ForceIosLowRes = packet.HasAllowIosHighres && !packet.AllowIosHighres;
		netCacheFeatures.AllowOfflineClientActivity = packet.HasAllowOfflineClientActivityDesktop && packet.AllowOfflineClientActivityDesktop;
		netCacheFeatures.EnableSmartDeckCompletion = packet.HasEnableSmartDeckCompletion && packet.EnableSmartDeckCompletion;
		netCacheFeatures.AllowOfflineClientDeckDeletion = packet.HasAllowOfflineClientDeckDeletion && packet.AllowOfflineClientDeckDeletion;
		netCacheFeatures.BattlegroundsEarlyAccessLicense = (packet.HasBattlegroundsEarlyAccessLicense ? packet.BattlegroundsEarlyAccessLicense : 0);
		netCacheFeatures.BattlegroundsMaxRankedPartySize = (packet.HasBattlegroundsMaxRankedPartySize ? packet.BattlegroundsMaxRankedPartySize : PartyManager.BATTLEGROUNDS_MAX_RANKED_PARTY_SIZE_FALLBACK);
		netCacheFeatures.ProgressionEnabled = packet.ProgressionEnabled;
		netCacheFeatures.JournalButtonDisabled = packet.JournalButtonDisabled;
		netCacheFeatures.AchievementToastDisabled = packet.AchievementToastDisabled;
		netCacheFeatures.DuelsEarlyAccessLicense = (packet.HasDuelsEarlyAccessLicense ? packet.DuelsEarlyAccessLicense : 0u);
		netCacheFeatures.ContentstackEnabled = !packet.HasContentstackEnabled || packet.ContentstackEnabled;
		netCacheFeatures.AppRatingEnabled = !packet.HasAppRatingEnabled || packet.AppRatingEnabled;
		netCacheFeatures.AppRatingSamplingPercentage = packet.AppRatingSamplingPercentage;
		if (packet.HasFsgEnabled && packet.FsgEnabled)
		{
			Network.Get().EnsureSubscribedTo(UtilSystemId.FIRESIDE_GATHERINGS);
		}
		OnNetCacheObjReceived(netCacheFeatures);
	}

	public void OnCurrencyState(GameCurrencyStates currencyState)
	{
		if (!currencyState.HasCurrencyVersion || m_currencyVersion > currencyState.CurrencyVersion)
		{
			Log.Net.PrintDebug("Ignoring currency state: {0}, (cached currency version: {1})", currencyState.ToHumanReadableString(), m_currencyVersion);
			return;
		}
		Log.Net.PrintDebug("Caching currency state: {0}", currencyState.ToHumanReadableString());
		m_currencyVersion = currencyState.CurrencyVersion;
		if (currencyState.HasArcaneDustBalance)
		{
			NetCacheArcaneDustBalance netCacheArcaneDustBalance = GetNetObject<NetCacheArcaneDustBalance>();
			if (netCacheArcaneDustBalance == null)
			{
				netCacheArcaneDustBalance = new NetCacheArcaneDustBalance();
			}
			netCacheArcaneDustBalance.Balance = currencyState.ArcaneDustBalance;
			OnNetCacheObjReceived(netCacheArcaneDustBalance);
		}
		if (currencyState.HasCappedGoldBalance && currencyState.HasBonusGoldBalance)
		{
			NetCacheGoldBalance netCacheGoldBalance = GetNetObject<NetCacheGoldBalance>();
			if (netCacheGoldBalance == null)
			{
				netCacheGoldBalance = new NetCacheGoldBalance();
			}
			netCacheGoldBalance.CappedBalance = currencyState.CappedGoldBalance;
			netCacheGoldBalance.BonusBalance = currencyState.BonusGoldBalance;
			if (currencyState.HasGoldCap)
			{
				netCacheGoldBalance.Cap = currencyState.GoldCap;
			}
			if (currencyState.HasGoldCapWarning)
			{
				netCacheGoldBalance.CapWarning = currencyState.GoldCapWarning;
			}
			OnNetCacheObjReceived(netCacheGoldBalance);
			DelGoldBalanceListener[] array = m_goldBalanceListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i](netCacheGoldBalance);
			}
		}
		if (BnetBar.Get() != null)
		{
			BnetBar.Get().RefreshCurrency();
		}
	}

	public void OnBoosterModifications(BoosterModifications packet)
	{
		NetCacheBoosters netObject = Get().GetNetObject<NetCacheBoosters>();
		if (netObject == null)
		{
			return;
		}
		foreach (BoosterInfo modification in packet.Modifications)
		{
			BoosterStack boosterStack = netObject.GetBoosterStack(modification.Type);
			if (boosterStack == null)
			{
				boosterStack = new BoosterStack();
				boosterStack.Id = modification.Type;
				netObject.BoosterStacks.Add(boosterStack);
			}
			if (modification.Count < 0)
			{
				while (modification.Count < 0)
				{
					if (boosterStack.LocallyPreConsumedCount > 0)
					{
						boosterStack.LocallyPreConsumedCount--;
					}
					else
					{
						boosterStack.Count--;
					}
					modification.Count++;
				}
			}
			else
			{
				boosterStack.Count += modification.Count;
				boosterStack.EverGrantedCount += modification.EverGrantedCount;
			}
		}
		OnNetCacheObjReceived(netObject);
	}

	public bool AddExpectedCollectionModification(long version)
	{
		if (!m_handledCardModifications.Contains(version))
		{
			m_expectedCardModifications.Add(version);
			return true;
		}
		return false;
	}

	public void OnCollectionModification(ClientStateNotification packet)
	{
		CollectionModifications collectionModifications = packet.CollectionModifications;
		if (m_handledCardModifications.Contains(collectionModifications.CollectionVersion) || m_initialCollectionVersion >= collectionModifications.CollectionVersion)
		{
			Log.Net.PrintDebug("Ignoring redundant coolection modification (modification was v.{0}; we are v.{1})", collectionModifications.CollectionVersion, Math.Max(m_handledCardModifications.DefaultIfEmpty(0L).Max(), m_initialCollectionVersion));
			return;
		}
		OnCollectionModificationInternal(collectionModifications);
		if (packet.HasAchievementNotifications)
		{
			AchieveManager.Get().OnAchievementNotifications(packet.AchievementNotifications.AchievementNotifications_);
		}
		if (packet.HasNoticeNotifications)
		{
			Network.Get().OnNoticeNotifications(packet.NoticeNotifications);
		}
		if (packet.HasBoosterModifications)
		{
			Get().OnBoosterModifications(packet.BoosterModifications);
		}
		if (collectionModifications.CardModifications.Count > 0 && CollectionManager.Get().GetCollectibleDisplay() != null && CollectionManager.Get().GetCollectibleDisplay().m_pageManager != null)
		{
			CollectionManager.Get().GetCollectibleDisplay().m_pageManager.RefreshCurrentPageContents();
			CollectionManager.Get().GetCollectibleDisplay().UpdateCurrentPageCardLocks();
		}
	}

	private void OnCollectionModificationInternal(CollectionModifications packet)
	{
		m_handledCardModifications.Add(packet.CollectionVersion);
		m_expectedCardModifications.Remove(packet.CollectionVersion);
		foreach (CardModification cardModification in packet.CardModifications)
		{
			Log.Net.PrintDebug("Handling card collection modification (collection version {0}): {1}", packet.CollectionVersion, cardModification.ToHumanReadableString());
			string cardID = GameUtils.TranslateDbIdToCardId(cardModification.AssetCardId);
			if (cardModification.Quantity > 0)
			{
				int num = 0;
				int num2 = Math.Min(cardModification.AmountSeen, cardModification.Quantity);
				if (cardModification.AmountSeen > 0)
				{
					CollectionManager.Get().OnCardAdded(cardID, (TAG_PREMIUM)cardModification.Premium, num2, seenBefore: true);
					num = num2;
				}
				int num3 = cardModification.Quantity - num;
				if (num3 > 0)
				{
					CollectionManager.Get().OnCardAdded(cardID, (TAG_PREMIUM)cardModification.Premium, num3, seenBefore: false);
				}
			}
			else if (cardModification.Quantity < 0)
			{
				CollectionManager.Get().OnCardRemoved(cardID, (TAG_PREMIUM)cardModification.Premium, -1 * cardModification.Quantity);
			}
		}
		AchieveManager.Get().ValidateAchievesNow();
	}

	public void OnCardBackModifications(CardBackModifications packet)
	{
		NetCacheCardBacks netObject = GetNetObject<NetCacheCardBacks>();
		if (netObject == null)
		{
			Debug.LogWarning($"NetCache.OnCardBackModifications(): trying to access NetCacheCardBacks before it's been loaded");
			return;
		}
		foreach (CardBackModification item in packet.CardBackModifications_)
		{
			netObject.CardBacks.Add(item.AssetCardBackId);
			if (item.HasAutoSetAsFavorite && item.AutoSetAsFavorite)
			{
				ProcessNewFavoriteCardBack(item.AssetCardBackId);
			}
		}
	}

	private void OnSetFavoriteCardBackResponse()
	{
		Network.CardBackResponse cardBackResponse = Network.Get().GetCardBackResponse();
		if (!cardBackResponse.Success)
		{
			Log.CardbackMgr.PrintError("SetFavoriteCardBack FAILED (cardBack = {0})", cardBackResponse.CardBack);
		}
		else
		{
			ProcessNewFavoriteCardBack(cardBackResponse.CardBack);
		}
	}

	public void ProcessNewFavoriteCardBack(int newFavoriteCardBackID)
	{
		NetCacheCardBacks netObject = GetNetObject<NetCacheCardBacks>();
		if (netObject == null)
		{
			Debug.LogWarning($"NetCache.ProcessNewFavoriteCardBack(): trying to access NetCacheCardBacks before it's been loaded");
		}
		else if (netObject.FavoriteCardBack != newFavoriteCardBackID)
		{
			netObject.FavoriteCardBack = newFavoriteCardBackID;
			if (this.FavoriteCardBackChanged != null)
			{
				this.FavoriteCardBackChanged(newFavoriteCardBackID);
			}
		}
	}

	private void OnSetFavoriteCoinResponse()
	{
		Network.CoinResponse coinResponse = Network.Get().GetCoinResponse();
		if (!coinResponse.Success)
		{
			Log.Net.PrintError("SetFavoriteCardBack FAILED (coin = {0})", coinResponse.Coin);
		}
		else
		{
			ProcessNewFavoriteCoin(coinResponse.Coin);
		}
	}

	public void ProcessNewFavoriteCoin(int newFavoriteCoinID)
	{
		NetCacheCoins netObject = GetNetObject<NetCacheCoins>();
		if (netObject == null)
		{
			Debug.LogWarning($"NetCache.ProcessNewFavoriteCoin(): trying to accessNetCacheCoins before it's been loaded");
		}
		else if (netObject.FavoriteCoin != newFavoriteCoinID)
		{
			netObject.FavoriteCoin = newFavoriteCoinID;
			if (this.FavoriteCoinChanged != null)
			{
				this.FavoriteCoinChanged(newFavoriteCoinID);
			}
		}
	}

	private void OnGamesInfo()
	{
		NetCacheGamesPlayed gamesInfo = Network.Get().GetGamesInfo();
		if (gamesInfo == null)
		{
			Debug.LogWarning("error getting games info");
		}
		else
		{
			OnNetCacheObjReceived(gamesInfo);
		}
	}

	private void OnProfileProgress()
	{
		OnNetCacheObjReceived(Network.Get().GetProfileProgress());
	}

	private void OnHearthstoneUnavailableGame()
	{
		OnHearthstoneUnavailable(gamePacket: true);
	}

	private void OnHearthstoneUnavailableUtil()
	{
		OnHearthstoneUnavailable(gamePacket: false);
	}

	private void OnHearthstoneUnavailable(bool gamePacket)
	{
		Network.UnavailableReason hearthstoneUnavailable = Network.Get().GetHearthstoneUnavailable(gamePacket);
		Debug.Log("Hearthstone Unavailable!  Reason: " + hearthstoneUnavailable.mainReason);
		string mainReason = hearthstoneUnavailable.mainReason;
		if (!(mainReason == "VERSION"))
		{
			if (mainReason == "OFFLINE")
			{
				Network.Get().ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_UNAVAILABLE_OFFLINE");
				return;
			}
			TelemetryManager.Client().SendNetworkError(NetworkError.ErrorType.SERVICE_UNAVAILABLE, $"{hearthstoneUnavailable.mainReason} - {hearthstoneUnavailable.subReason} - {hearthstoneUnavailable.extraData}", 0);
			Network.Get().ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_UNAVAILABLE_UNKNOWN");
			return;
		}
		ErrorParams errorParams = new ErrorParams();
		if (PlatformSettings.IsMobile() && GameDownloadManagerProvider.Get() != null && !GameDownloadManagerProvider.Get().IsNewMobileVersionReleased)
		{
			errorParams.m_message = GameStrings.Format("GLOBAL_ERROR_NETWORK_UNAVAILABLE_NEW_VERSION");
			errorParams.m_reason = FatalErrorReason.UNAVAILABLE_NEW_VERSION;
		}
		else
		{
			errorParams.m_message = GameStrings.Format("GLOBAL_ERROR_NETWORK_UNAVAILABLE_UPGRADE");
			if ((bool)Error.HAS_APP_STORE)
			{
				errorParams.m_redirectToStore = true;
			}
			errorParams.m_reason = FatalErrorReason.UNAVAILABLE_UPGRADE;
		}
		Error.AddFatal(errorParams);
		ReconnectMgr.Get().FullResetRequired = true;
		ReconnectMgr.Get().UpdateRequired = true;
	}

	private void OnCardBacks()
	{
		OnNetCacheObjReceived(Network.Get().GetCardBacks());
		CardBacks cardBacksPacket = Network.Get().GetCardBacksPacket();
		if (cardBacksPacket != null)
		{
			SetFavoriteCardBack setFavoriteCardBack = OfflineDataCache.GenerateSetFavoriteCardBackFromDiff(cardBacksPacket.FavoriteCardBack);
			if (setFavoriteCardBack != null)
			{
				Network.Get().SetFavoriteCardBack(setFavoriteCardBack.CardBack);
			}
			OfflineDataCache.ClearCardBackDirtyFlag();
			OfflineDataCache.CacheCardBacks(cardBacksPacket);
		}
	}

	private void OnCoins()
	{
		OnNetCacheObjReceived(Network.Get().GetCoins());
		Coins coinsPacket = Network.Get().GetCoinsPacket();
		if (coinsPacket != null)
		{
			SetFavoriteCoin setFavoriteCoin = OfflineDataCache.GenerateSetFavoriteCoinFromDiff(coinsPacket.FavoriteCoin);
			if (setFavoriteCoin != null)
			{
				Network.Get().SetFavoriteCoin(setFavoriteCoin.Coin);
			}
			OfflineDataCache.ClearCoinDirtyFlag();
			OfflineDataCache.CacheCoins(coinsPacket);
		}
	}

	private void OnPlayerRecords()
	{
		PlayerRecords playerRecordsPacket = Network.Get().GetPlayerRecordsPacket();
		OnPlayerRecordsPacket(playerRecordsPacket);
	}

	public void OnPlayerRecordsPacket(PlayerRecords packet)
	{
		OnNetCacheObjReceived(Network.GetPlayerRecords(packet));
	}

	private void OnRewardProgress()
	{
		OnNetCacheObjReceived(Network.Get().GetRewardProgress());
	}

	private NetCacheHeroLevels GetAllHeroXP(HeroXP packet)
	{
		if (packet == null)
		{
			return new NetCacheHeroLevels();
		}
		NetCacheHeroLevels netCacheHeroLevels = new NetCacheHeroLevels();
		for (int i = 0; i < packet.XpInfos.Count; i++)
		{
			HeroXPInfo heroXPInfo = packet.XpInfos[i];
			HeroLevel heroLevel = new HeroLevel();
			heroLevel.Class = (TAG_CLASS)heroXPInfo.ClassId;
			heroLevel.CurrentLevel.Level = heroXPInfo.Level;
			heroLevel.CurrentLevel.XP = heroXPInfo.CurrXp;
			heroLevel.CurrentLevel.MaxXP = heroXPInfo.MaxXp;
			netCacheHeroLevels.Levels.Add(heroLevel);
		}
		return netCacheHeroLevels;
	}

	public void OnHeroXP(HeroXP packet)
	{
		NetCacheHeroLevels allHeroXP = GetAllHeroXP(packet);
		if (m_prevHeroLevels != null)
		{
			foreach (HeroLevel newHeroLevel in allHeroXP.Levels)
			{
				HeroLevel heroLevel = m_prevHeroLevels.Levels.Find((HeroLevel obj) => obj.Class == newHeroLevel.Class);
				if (heroLevel == null)
				{
					continue;
				}
				if (newHeroLevel != null && newHeroLevel.CurrentLevel != null && newHeroLevel.CurrentLevel.Level != heroLevel.CurrentLevel.Level && (newHeroLevel.CurrentLevel.Level == 20 || newHeroLevel.CurrentLevel.Level == 30 || newHeroLevel.CurrentLevel.Level == 40 || newHeroLevel.CurrentLevel.Level == 50 || newHeroLevel.CurrentLevel.Level == 60))
				{
					if (newHeroLevel.Class == TAG_CLASS.DRUID)
					{
						BnetPresenceMgr.Get().SetGameField(5u, newHeroLevel.CurrentLevel.Level);
					}
					else if (newHeroLevel.Class == TAG_CLASS.HUNTER)
					{
						BnetPresenceMgr.Get().SetGameField(6u, newHeroLevel.CurrentLevel.Level);
					}
					else if (newHeroLevel.Class == TAG_CLASS.MAGE)
					{
						BnetPresenceMgr.Get().SetGameField(7u, newHeroLevel.CurrentLevel.Level);
					}
					else if (newHeroLevel.Class == TAG_CLASS.PALADIN)
					{
						BnetPresenceMgr.Get().SetGameField(8u, newHeroLevel.CurrentLevel.Level);
					}
					else if (newHeroLevel.Class == TAG_CLASS.PRIEST)
					{
						BnetPresenceMgr.Get().SetGameField(9u, newHeroLevel.CurrentLevel.Level);
					}
					else if (newHeroLevel.Class == TAG_CLASS.ROGUE)
					{
						BnetPresenceMgr.Get().SetGameField(10u, newHeroLevel.CurrentLevel.Level);
					}
					else if (newHeroLevel.Class == TAG_CLASS.SHAMAN)
					{
						BnetPresenceMgr.Get().SetGameField(11u, newHeroLevel.CurrentLevel.Level);
					}
					else if (newHeroLevel.Class == TAG_CLASS.WARLOCK)
					{
						BnetPresenceMgr.Get().SetGameField(12u, newHeroLevel.CurrentLevel.Level);
					}
					else if (newHeroLevel.Class == TAG_CLASS.WARRIOR)
					{
						BnetPresenceMgr.Get().SetGameField(13u, newHeroLevel.CurrentLevel.Level);
					}
				}
				newHeroLevel.PrevLevel = heroLevel.CurrentLevel;
			}
		}
		m_prevHeroLevels = allHeroXP;
		OnNetCacheObjReceived(allHeroXP);
	}

	private void OnAllHeroXP()
	{
		HeroXP heroXP = Network.Get().GetHeroXP();
		OnHeroXP(heroXP);
	}

	private void OnInitialClientState_ProfileNotices(ProfileNotices profileNotices)
	{
		List<ProfileNotice> result = new List<ProfileNotice>();
		Network.Get().HandleProfileNotices(profileNotices.List, ref result);
		m_receivedInitialProfileNotices = true;
		HandleIncomingProfileNotices(result, isInitialNoticeList: true);
		HandleIncomingProfileNotices(m_queuedProfileNotices, isInitialNoticeList: true);
		m_queuedProfileNotices.Clear();
	}

	public void HandleIncomingProfileNotices(List<ProfileNotice> receivedNotices, bool isInitialNoticeList)
	{
		if (!m_receivedInitialProfileNotices)
		{
			m_queuedProfileNotices.AddRange(receivedNotices);
			return;
		}
		if (receivedNotices.Find((ProfileNotice obj) => obj.Type == ProfileNotice.NoticeType.GAINED_MEDAL) != null)
		{
			m_previousMedalInfo = null;
			NetCacheMedalInfo netObject = GetNetObject<NetCacheMedalInfo>();
			if (netObject != null)
			{
				netObject.PreviousMedalInfo = null;
			}
		}
		List<ProfileNotice> list = FindNewNotices(receivedNotices);
		NetCacheProfileNotices netCacheProfileNotices = GetNetObject<NetCacheProfileNotices>();
		if (netCacheProfileNotices == null)
		{
			netCacheProfileNotices = new NetCacheProfileNotices();
		}
		HashSet<long> hashSet = new HashSet<long>();
		for (int i = 0; i < netCacheProfileNotices.Notices.Count; i++)
		{
			hashSet.Add(netCacheProfileNotices.Notices[i].NoticeID);
		}
		for (int j = 0; j < receivedNotices.Count; j++)
		{
			if (!m_ackedNotices.Contains(receivedNotices[j].NoticeID) && !hashSet.Contains(receivedNotices[j].NoticeID))
			{
				netCacheProfileNotices.Notices.Add(receivedNotices[j]);
			}
		}
		OnNetCacheObjReceived(netCacheProfileNotices);
		DelNewNoticesListener[] array = m_newNoticesListeners.ToArray();
		foreach (ProfileNotice item in list)
		{
			Log.Achievements.Print("NetCache.OnProfileNotices() sending {0} to {1} listeners", item, array.Length);
		}
		DelNewNoticesListener[] array2 = array;
		foreach (DelNewNoticesListener delNewNoticesListener in array2)
		{
			Log.Achievements.Print("NetCache.OnProfileNotices(): sending notices to {0}::{1}", delNewNoticesListener.Method.ReflectedType.Name, delNewNoticesListener.Method.Name);
			delNewNoticesListener(list, isInitialNoticeList);
		}
	}

	private List<ProfileNotice> FindNewNotices(List<ProfileNotice> receivedNotices)
	{
		List<ProfileNotice> list = new List<ProfileNotice>();
		NetCacheProfileNotices netObject = GetNetObject<NetCacheProfileNotices>();
		if (netObject == null)
		{
			list.AddRange(receivedNotices);
			return list;
		}
		foreach (ProfileNotice receivedNotice in receivedNotices)
		{
			if (netObject.Notices.Find((ProfileNotice obj) => obj.NoticeID == receivedNotice.NoticeID) == null)
			{
				list.Add(receivedNotice);
			}
		}
		return list;
	}

	public void OnClientOptions(ClientOptions packet)
	{
		NetCacheClientOptions netCacheClientOptions = GetNetObject<NetCacheClientOptions>();
		bool flag = netCacheClientOptions == null;
		if (flag)
		{
			netCacheClientOptions = new NetCacheClientOptions();
		}
		if (packet.HasFailed && packet.Failed)
		{
			Debug.LogError("ReadClientOptions: packet.Failed=true. Unable to retrieve client options from UtilServer.");
			Network.Get().ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_GENERIC");
			return;
		}
		foreach (PegasusUtil.ClientOption option in packet.Options)
		{
			ServerOption index = (ServerOption)option.Index;
			if (option.HasAsInt32)
			{
				netCacheClientOptions.ClientState[index] = new ClientOptionInt(option.AsInt32);
			}
			else if (option.HasAsInt64)
			{
				netCacheClientOptions.ClientState[index] = new ClientOptionLong(option.AsInt64);
			}
			else if (option.HasAsFloat)
			{
				netCacheClientOptions.ClientState[index] = new ClientOptionFloat(option.AsFloat);
			}
			else if (option.HasAsUint64)
			{
				netCacheClientOptions.ClientState[index] = new ClientOptionULong(option.AsUint64);
			}
		}
		netCacheClientOptions.UpdateServerState();
		OnNetCacheObjReceived(netCacheClientOptions);
		if (flag)
		{
			OptionsMigration.UpgradeServerOptions();
		}
		netCacheClientOptions.RemoveInvalidOptions();
	}

	private void SetClientOption(ServerOption type, ClientOptionBase newVal)
	{
		Type typeFromHandle = typeof(NetCacheClientOptions);
		if (!m_netCache.TryGetValue(typeFromHandle, out var value) || !(value is NetCacheClientOptions))
		{
			Debug.LogWarning("NetCache.OnClientOptions: Attempting to set an option before initializing the options cache.");
			return;
		}
		NetCacheClientOptions obj = (NetCacheClientOptions)value;
		obj.ClientState[type] = newVal;
		obj.CheckForDispatchToServer();
		NetCacheChanged<NetCacheClientOptions>();
	}

	public void SetIntOption(ServerOption type, int val)
	{
		SetClientOption(type, new ClientOptionInt(val));
	}

	public void SetLongOption(ServerOption type, long val)
	{
		SetClientOption(type, new ClientOptionLong(val));
	}

	public void SetFloatOption(ServerOption type, float val)
	{
		SetClientOption(type, new ClientOptionFloat(val));
	}

	public void SetULongOption(ServerOption type, ulong val)
	{
		SetClientOption(type, new ClientOptionULong(val));
	}

	public void DeleteClientOption(ServerOption type)
	{
		SetClientOption(type, null);
	}

	public bool ClientOptionExists(ServerOption type)
	{
		NetCacheClientOptions netObject = GetNetObject<NetCacheClientOptions>();
		if (netObject == null)
		{
			return false;
		}
		if (!netObject.ClientState.ContainsKey(type))
		{
			return false;
		}
		return netObject.ClientState[type] != null;
	}

	public void DispatchClientOptionsToServer()
	{
		Get().GetNetObject<NetCacheClientOptions>()?.DispatchClientOptionsToServer();
	}

	private void OnFavoriteHeroesResponse()
	{
		FavoriteHeroesResponse favoriteHeroesResponse = Network.Get().GetFavoriteHeroesResponse();
		NetCacheFavoriteHeroes netCacheFavoriteHeroes = new NetCacheFavoriteHeroes();
		foreach (FavoriteHero favoriteHero in favoriteHeroesResponse.FavoriteHeroes)
		{
			if (!EnumUtils.TryCast<TAG_CLASS>(favoriteHero.ClassId, out var outVal))
			{
				Debug.LogWarning($"NetCache.OnFavoriteHeroesResponse() unrecognized hero class {favoriteHero.ClassId}");
				continue;
			}
			if (!EnumUtils.TryCast<TAG_PREMIUM>(favoriteHero.Hero.Premium, out var outVal2))
			{
				Debug.LogWarning($"NetCache.OnFavoriteHeroesResponse() unrecognized hero premium {favoriteHero.Hero.Premium} for hero class {outVal}");
				continue;
			}
			CardDefinition value = new CardDefinition
			{
				Name = GameUtils.TranslateDbIdToCardId(favoriteHero.Hero.Asset),
				Premium = outVal2
			};
			netCacheFavoriteHeroes.FavoriteHeroes[outVal] = value;
		}
		List<SetFavoriteHero> list = OfflineDataCache.GenerateSetFavoriteHeroFromDiff(netCacheFavoriteHeroes);
		if (list.Any())
		{
			foreach (SetFavoriteHero item in list)
			{
				CardDefinition hero = new CardDefinition
				{
					Name = GameUtils.TranslateDbIdToCardId(item.FavoriteHero.Hero.Asset),
					Premium = (TAG_PREMIUM)item.FavoriteHero.Hero.Premium
				};
				Network.Get().SetFavoriteHero((TAG_CLASS)item.FavoriteHero.ClassId, hero);
			}
			OfflineDataCache.ClearFavoriteHeroesDirtyFlag();
		}
		OnNetCacheObjReceived(netCacheFavoriteHeroes);
		OfflineDataCache.CacheFavoriteHeroes(favoriteHeroesResponse);
	}

	private void OnSetFavoriteHeroResponse()
	{
		Network.SetFavoriteHeroResponse setFavoriteHeroResponse = Network.Get().GetSetFavoriteHeroResponse();
		if (!setFavoriteHeroResponse.Success)
		{
			return;
		}
		if (TAG_CLASS.NEUTRAL == setFavoriteHeroResponse.HeroClass || setFavoriteHeroResponse.Hero == null)
		{
			Debug.LogWarning($"NetCache.OnSetFavoriteHeroResponse: setting hero was a success, but message contains invalid class ({setFavoriteHeroResponse.HeroClass}) and/or hero ({setFavoriteHeroResponse.Hero})");
			return;
		}
		NetCacheFavoriteHeroes netObject = Get().GetNetObject<NetCacheFavoriteHeroes>();
		if (netObject != null)
		{
			netObject.FavoriteHeroes[setFavoriteHeroResponse.HeroClass] = setFavoriteHeroResponse.Hero;
			Debug.LogWarning($"NetCache.OnSetFavoriteHeroResponse: favorite hero for class {setFavoriteHeroResponse.HeroClass} updated to {setFavoriteHeroResponse.Hero}");
		}
		PegasusShared.CardDef cardDef = new PegasusShared.CardDef
		{
			Asset = GameUtils.TranslateCardIdToDbId(setFavoriteHeroResponse.Hero.Name),
			Premium = (int)setFavoriteHeroResponse.Hero.Premium
		};
		OfflineDataCache.SetFavoriteHero((int)setFavoriteHeroResponse.HeroClass, cardDef, wasCalledOffline: false);
	}

	private void OnAccountLicensesInfoResponse()
	{
		AccountLicensesInfoResponse accountLicensesInfoResponse = Network.Get().GetAccountLicensesInfoResponse();
		NetCacheAccountLicenses netCacheAccountLicenses = new NetCacheAccountLicenses();
		foreach (AccountLicenseInfo item in accountLicensesInfoResponse.List)
		{
			netCacheAccountLicenses.AccountLicenses[item.License] = item;
		}
		OnNetCacheObjReceived(netCacheAccountLicenses);
	}

	private void OnClientStaticAssetsResponse()
	{
		ClientStaticAssetsResponse clientStaticAssetsResponse = Network.Get().GetClientStaticAssetsResponse();
		if (clientStaticAssetsResponse != null)
		{
			OnNetCacheObjReceived(clientStaticAssetsResponse);
		}
	}

	private void OnFSGFeatureConfig()
	{
		FSGFeatureConfig fSGFeatureConfig = Network.Get().GetFSGFeatureConfig();
		if (fSGFeatureConfig != null)
		{
			OnNetCacheObjReceived(fSGFeatureConfig);
		}
	}

	private void RegisterNetCacheHandlers()
	{
		Network network = Network.Get();
		network.RegisterNetHandler(DBAction.PacketID.ID, OnDBAction);
		network.RegisterNetHandler(GenericResponse.PacketID.ID, OnGenericResponse);
		network.RegisterNetHandler(InitialClientState.PacketID.ID, OnInitialClientState);
		network.RegisterNetHandler(MedalInfo.PacketID.ID, OnMedalInfo);
		network.RegisterNetHandler(BattlegroundsRatingInfoResponse.PacketID.ID, OnBaconRatingInfo);
		network.RegisterNetHandler(ProfileProgress.PacketID.ID, OnProfileProgress);
		network.RegisterNetHandler(GamesInfo.PacketID.ID, OnGamesInfo);
		network.RegisterNetHandler(CardValues.PacketID.ID, OnCardValues);
		network.RegisterNetHandler(GuardianVars.PacketID.ID, OnGuardianVars);
		network.RegisterNetHandler(PlayerRecords.PacketID.ID, OnPlayerRecords);
		network.RegisterNetHandler(RewardProgress.PacketID.ID, OnRewardProgress);
		network.RegisterNetHandler(HeroXP.PacketID.ID, OnAllHeroXP);
		network.RegisterNetHandler(CardBacks.PacketID.ID, OnCardBacks);
		network.RegisterNetHandler(SetFavoriteCardBackResponse.PacketID.ID, OnSetFavoriteCardBackResponse);
		network.RegisterNetHandler(FavoriteHeroesResponse.PacketID.ID, OnFavoriteHeroesResponse);
		network.RegisterNetHandler(SetFavoriteHeroResponse.PacketID.ID, OnSetFavoriteHeroResponse);
		network.RegisterNetHandler(AccountLicensesInfoResponse.PacketID.ID, OnAccountLicensesInfoResponse);
		network.RegisterNetHandler(DeckList.PacketID.ID, OnReceivedDeckHeaders);
		network.RegisterNetHandler(BattlegroundsPremiumStatusResponse.PacketID.ID, OnBaconPremiumStatus);
		network.RegisterNetHandler(PVPDRStatsInfoResponse.PacketID.ID, OnPVPDRStatsInfo);
		network.RegisterNetHandler(Coins.PacketID.ID, OnCoins);
		network.RegisterNetHandler(SetFavoriteCoinResponse.PacketID.ID, OnSetFavoriteCoinResponse);
		network.RegisterNetHandler(Deadend.PacketID.ID, OnHearthstoneUnavailableGame);
		network.RegisterNetHandler(DeadendUtil.PacketID.ID, OnHearthstoneUnavailableUtil);
		network.RegisterNetHandler(ClientStaticAssetsResponse.PacketID.ID, OnClientStaticAssetsResponse);
		network.RegisterNetHandler(FSGFeatureConfig.PacketID.ID, OnFSGFeatureConfig);
	}

	public void RegisterCollectionManager(NetCacheCallback callback)
	{
		RegisterCollectionManager(callback, DefaultErrorHandler);
	}

	public void RegisterCollectionManager(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		NetCacheBatchRequest request = new NetCacheBatchRequest(callback, errorCallback, RegisterCollectionManager);
		AddCollectionManagerToRequest(ref request);
		NetCacheMakeBatchRequest(request);
	}

	public void RegisterScreenCollectionManager(NetCacheCallback callback)
	{
		RegisterScreenCollectionManager(callback, DefaultErrorHandler);
	}

	public void RegisterScreenCollectionManager(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		NetCacheBatchRequest request = new NetCacheBatchRequest(callback, errorCallback, RegisterScreenCollectionManager);
		AddCollectionManagerToRequest(ref request);
		request.AddRequests(new List<Request>
		{
			new Request(typeof(NetCacheCollection)),
			new Request(typeof(NetCacheFeatures)),
			new Request(typeof(NetCacheHeroLevels))
		});
		NetCacheMakeBatchRequest(request);
	}

	public void RegisterScreenForge(NetCacheCallback callback)
	{
		RegisterScreenForge(callback, DefaultErrorHandler);
	}

	public void RegisterScreenForge(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		NetCacheBatchRequest request = new NetCacheBatchRequest(callback, errorCallback, RegisterScreenForge);
		AddCollectionManagerToRequest(ref request);
		request.AddRequests(new List<Request>
		{
			new Request(typeof(NetCacheFeatures)),
			new Request(typeof(NetCacheHeroLevels))
		});
		NetCacheMakeBatchRequest(request);
	}

	public void RegisterScreenTourneys(NetCacheCallback callback)
	{
		RegisterScreenTourneys(callback, DefaultErrorHandler);
	}

	public void RegisterScreenTourneys(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		NetCacheBatchRequest netCacheBatchRequest = new NetCacheBatchRequest(callback, errorCallback, RegisterScreenTourneys);
		netCacheBatchRequest.AddRequests(new List<Request>
		{
			new Request(typeof(NetCachePlayerRecords)),
			new Request(typeof(NetCacheDecks)),
			new Request(typeof(NetCacheFeatures)),
			new Request(typeof(NetCacheHeroLevels))
		});
		NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	public void RegisterScreenFriendly(NetCacheCallback callback)
	{
		RegisterScreenFriendly(callback, DefaultErrorHandler);
	}

	public void RegisterScreenFriendly(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		NetCacheBatchRequest netCacheBatchRequest = new NetCacheBatchRequest(callback, errorCallback, RegisterScreenFriendly);
		netCacheBatchRequest.AddRequests(new List<Request>
		{
			new Request(typeof(NetCacheDecks)),
			new Request(typeof(NetCacheHeroLevels))
		});
		NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	public void RegisterScreenPractice(NetCacheCallback callback)
	{
		RegisterScreenPractice(callback, DefaultErrorHandler);
	}

	public void RegisterScreenPractice(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		NetCacheBatchRequest netCacheBatchRequest = new NetCacheBatchRequest(callback, errorCallback, RegisterScreenPractice);
		netCacheBatchRequest.AddRequests(new List<Request>
		{
			new Request(typeof(NetCacheDecks)),
			new Request(typeof(NetCacheFeatures)),
			new Request(typeof(NetCacheHeroLevels)),
			new Request(typeof(NetCacheRewardProgress))
		});
		NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	public void RegisterScreenEndOfGame(NetCacheCallback callback)
	{
		RegisterScreenEndOfGame(callback, DefaultErrorHandler);
	}

	public void RegisterScreenEndOfGame(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		if (HearthstoneServices.TryGet<GameMgr>(out var service) && service.IsSpectator())
		{
			Processor.ScheduleCallback(0f, realTime: false, delegate
			{
				callback();
			});
			return;
		}
		NetCacheBatchRequest netCacheBatchRequest = new NetCacheBatchRequest(callback, errorCallback, RegisterScreenEndOfGame);
		netCacheBatchRequest.AddRequests(new List<Request>
		{
			new Request(typeof(NetCacheRewardProgress)),
			new Request(typeof(NetCacheMedalInfo), rl: true),
			new Request(typeof(NetCacheGamesPlayed), rl: true),
			new Request(typeof(NetCachePlayerRecords), rl: true),
			new Request(typeof(NetCacheHeroLevels), rl: true)
		});
		NetCacheMakeBatchRequest(netCacheBatchRequest);
		PegasusShared.GameType num = service?.GetGameType() ?? PegasusShared.GameType.GT_UNKNOWN;
		bool flag = GameUtils.IsTavernBrawlGameType(num);
		if (num == PegasusShared.GameType.GT_VS_FRIEND && FriendChallengeMgr.Get().IsChallengeTavernBrawl())
		{
			NetCacheFeatures netObject = Get().GetNetObject<NetCacheFeatures>();
			if (netObject != null && netObject.FriendWeekAllowsTavernBrawlRecordUpdate && SpecialEventManager.Get().IsEventActive(SpecialEventType.FRIEND_WEEK, activeIfDoesNotExist: false))
			{
				flag = true;
			}
		}
		if (flag)
		{
			TavernBrawlManager.Get().RefreshPlayerRecord();
		}
		if (GameUtils.IsFiresideGatheringGameType(num))
		{
			Network.Get().RequestFSGPatronListUpdate();
		}
	}

	public void RegisterScreenPackOpening(NetCacheCallback callback)
	{
		RegisterScreenPackOpening(callback, DefaultErrorHandler);
	}

	public void RegisterScreenPackOpening(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		NetCacheBatchRequest netCacheBatchRequest = new NetCacheBatchRequest(callback, errorCallback, RegisterScreenPackOpening);
		netCacheBatchRequest.AddRequest(new Request(typeof(NetCacheBoosters)));
		NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	public void RegisterScreenBox(NetCacheCallback callback)
	{
		RegisterScreenBox(callback, DefaultErrorHandler);
	}

	public void RegisterScreenBox(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		NetCacheBatchRequest netCacheBatchRequest = new NetCacheBatchRequest(callback, errorCallback, RegisterScreenBox);
		NetCacheFeatures netObject = GetNetObject<NetCacheFeatures>();
		Debug.Log("RegisterScreenBox tempGuardianVars=" + netObject);
		netCacheBatchRequest.AddRequests(new List<Request>
		{
			new Request(typeof(NetCacheBoosters)),
			new Request(typeof(NetCacheClientOptions)),
			new Request(typeof(NetCacheProfileProgress)),
			new Request(typeof(NetCacheFeatures)),
			new Request(typeof(NetCacheMedalInfo)),
			new Request(typeof(NetCacheHeroLevels))
		});
		NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	public void RegisterScreenQuestLog(NetCacheCallback callback)
	{
		RegisterScreenQuestLog(callback, DefaultErrorHandler);
	}

	public void RegisterScreenQuestLog(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		NetCacheBatchRequest netCacheBatchRequest = new NetCacheBatchRequest(callback, errorCallback, RegisterScreenQuestLog);
		netCacheBatchRequest.AddRequests(new List<Request>
		{
			new Request(typeof(NetCacheMedalInfo)),
			new Request(typeof(NetCacheHeroLevels)),
			new Request(typeof(NetCachePlayerRecords)),
			new Request(typeof(NetCacheProfileProgress), rl: true)
		});
		NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	public void RegisterScreenStartup(NetCacheCallback callback)
	{
		RegisterScreenStartup(callback, DefaultErrorHandler);
	}

	public void RegisterScreenStartup(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		NetCacheBatchRequest netCacheBatchRequest = new NetCacheBatchRequest(callback, errorCallback, RegisterScreenStartup);
		netCacheBatchRequest.AddRequests(new List<Request>
		{
			new Request(typeof(NetCacheProfileProgress))
		});
		NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	public void RegisterScreenLogin(NetCacheCallback callback)
	{
		RegisterScreenLogin(callback, DefaultErrorHandler);
	}

	public void RegisterScreenLogin(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		NetCacheBatchRequest netCacheBatchRequest = new NetCacheBatchRequest(callback, errorCallback, RegisterScreenLogin);
		netCacheBatchRequest.AddRequests(new List<Request>
		{
			new Request(typeof(NetCacheRewardProgress)),
			new Request(typeof(NetCachePlayerRecords)),
			new Request(typeof(NetCacheGoldBalance)),
			new Request(typeof(NetCacheHeroLevels)),
			new Request(typeof(NetCacheCardBacks), rl: true),
			new Request(typeof(NetCacheFavoriteHeroes), rl: true),
			new Request(typeof(NetCacheAccountLicenses)),
			new Request(typeof(ClientStaticAssetsResponse)),
			new Request(typeof(NetCacheClientOptions)),
			new Request(typeof(NetCacheCoins))
		});
		NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	public void RegisterReconnectMgr(NetCacheCallback callback)
	{
		RegisterReconnectMgr(callback, DefaultErrorHandler);
	}

	public void RegisterReconnectMgr(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		NetCacheBatchRequest netCacheBatchRequest = new NetCacheBatchRequest(callback, errorCallback, RegisterReconnectMgr);
		netCacheBatchRequest.AddRequest(new Request(typeof(NetCacheDisconnectedGame)));
		NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	public void RegisterTutorialEndGameScreen(NetCacheCallback callback)
	{
		RegisterTutorialEndGameScreen(callback, DefaultErrorHandler);
	}

	public void RegisterTutorialEndGameScreen(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		if (HearthstoneServices.TryGet<GameMgr>(out var service) && service.IsSpectator())
		{
			Processor.ScheduleCallback(0f, realTime: false, delegate
			{
				callback();
			});
			return;
		}
		NetCacheBatchRequest netCacheBatchRequest = new NetCacheBatchRequest(callback, errorCallback, RegisterTutorialEndGameScreen);
		netCacheBatchRequest.AddRequests(new List<Request>
		{
			new Request(typeof(NetCacheProfileProgress))
		});
		NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	public void RegisterFriendChallenge(NetCacheCallback callback)
	{
		RegisterFriendChallenge(callback, DefaultErrorHandler);
	}

	public void RegisterFriendChallenge(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		NetCacheBatchRequest netCacheBatchRequest = new NetCacheBatchRequest(callback, errorCallback, RegisterFriendChallenge);
		netCacheBatchRequest.AddRequest(new Request(typeof(NetCacheProfileProgress)));
		NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	public void RegisterScreenBattlegrounds(NetCacheCallback callback)
	{
		RegisterScreenBattlegrounds(callback, DefaultErrorHandler);
	}

	public void RegisterScreenBattlegrounds(NetCacheCallback callback, ErrorCallback errorCallback)
	{
		NetCacheBatchRequest netCacheBatchRequest = new NetCacheBatchRequest(callback, errorCallback, RegisterScreenBattlegrounds);
		netCacheBatchRequest.AddRequests(new List<Request>
		{
			new Request(typeof(NetCacheFeatures))
		});
		NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	private void AddCollectionManagerToRequest(ref NetCacheBatchRequest request)
	{
		request.AddRequests(new List<Request>
		{
			new Request(typeof(NetCacheProfileNotices)),
			new Request(typeof(NetCacheDecks)),
			new Request(typeof(NetCacheCollection)),
			new Request(typeof(NetCacheCardValues)),
			new Request(typeof(NetCacheArcaneDustBalance)),
			new Request(typeof(NetCacheClientOptions))
		});
	}

	private void OnPacketThrottled(int packetID, long retryMillis)
	{
		if (packetID != 201)
		{
			return;
		}
		float timeAdded = Time.realtimeSinceStartup + (float)retryMillis / 1000f;
		foreach (NetCacheBatchRequest cacheRequest in m_cacheRequests)
		{
			cacheRequest.m_timeAdded = timeAdded;
		}
	}

	public void Cheat_AddNotice(ProfileNotice notice)
	{
		if (HearthstoneApplication.IsInternal())
		{
			UnloadNetObject<NetCacheProfileNotices>();
			PopupDisplayManager.Get().ClearSeenNotices();
			notice.NoticeID = 9999L;
			m_ackedNotices.Remove(notice.NoticeID);
			List<ProfileNotice> list = new List<ProfileNotice>();
			list.Add(notice);
			HandleIncomingProfileNotices(list, isInitialNoticeList: false);
		}
	}
}
