using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using UnityEngine;

public class GameDbf : IService
{
	public static Dbf<AccountLicenseDbfRecord> AccountLicense;

	public static Dbf<AchieveDbfRecord> Achieve;

	public static Dbf<AchieveConditionDbfRecord> AchieveCondition;

	public static Dbf<AchieveRegionDataDbfRecord> AchieveRegionData;

	public static Dbf<AchievementDbfRecord> Achievement;

	public static Dbf<AchievementCategoryDbfRecord> AchievementCategory;

	public static Dbf<AchievementSectionDbfRecord> AchievementSection;

	public static Dbf<AchievementSectionItemDbfRecord> AchievementSectionItem;

	public static Dbf<AchievementSubcategoryDbfRecord> AchievementSubcategory;

	public static Dbf<AdventureDbfRecord> Adventure;

	public static Dbf<AdventureDataDbfRecord> AdventureData;

	public static Dbf<AdventureDeckDbfRecord> AdventureDeck;

	public static Dbf<AdventureGuestHeroesDbfRecord> AdventureGuestHeroes;

	public static Dbf<AdventureHeroPowerDbfRecord> AdventureHeroPower;

	public static Dbf<AdventureLoadoutTreasuresDbfRecord> AdventureLoadoutTreasures;

	public static Dbf<AdventureMissionDbfRecord> AdventureMission;

	public static Dbf<AdventureModeDbfRecord> AdventureMode;

	public static Dbf<BannerDbfRecord> Banner;

	public static Dbf<BattlegroundsSeasonDbfRecord> BattlegroundsSeason;

	public static Dbf<BoardDbfRecord> Board;

	public static Dbf<BoosterDbfRecord> Booster;

	public static Dbf<BoosterCardSetDbfRecord> BoosterCardSet;

	public static Dbf<CardDbfRecord> Card;

	public static Dbf<CardAdditonalSearchTermsDbfRecord> CardAdditonalSearchTerms;

	public static Dbf<CardBackDbfRecord> CardBack;

	public static Dbf<CardChangeDbfRecord> CardChange;

	public static Dbf<CardDiscoverStringDbfRecord> CardDiscoverString;

	public static Dbf<CardHeroDbfRecord> CardHero;

	public static Dbf<CardPlayerDeckOverrideDbfRecord> CardPlayerDeckOverride;

	public static Dbf<CardSetDbfRecord> CardSet;

	public static Dbf<CardSetSpellOverrideDbfRecord> CardSetSpellOverride;

	public static Dbf<CardSetTimingDbfRecord> CardSetTiming;

	public static Dbf<CardTagDbfRecord> CardTag;

	public static Dbf<CharacterDialogDbfRecord> CharacterDialog;

	public static Dbf<CharacterDialogItemsDbfRecord> CharacterDialogItems;

	public static Dbf<ClassDbfRecord> Class;

	public static Dbf<ClassExclusionsDbfRecord> ClassExclusions;

	public static Dbf<ClientStringDbfRecord> ClientString;

	public static Dbf<CoinDbfRecord> Coin;

	public static Dbf<CreditsYearDbfRecord> CreditsYear;

	public static Dbf<DeckDbfRecord> Deck;

	public static Dbf<DeckCardDbfRecord> DeckCard;

	public static Dbf<DeckRulesetDbfRecord> DeckRuleset;

	public static Dbf<DeckRulesetRuleDbfRecord> DeckRulesetRule;

	public static Dbf<DeckRulesetRuleSubsetDbfRecord> DeckRulesetRuleSubset;

	public static Dbf<DeckTemplateDbfRecord> DeckTemplate;

	public static Dbf<DraftContentDbfRecord> DraftContent;

	public static Dbf<ExternalUrlDbfRecord> ExternalUrl;

	public static Dbf<FixedRewardDbfRecord> FixedReward;

	public static Dbf<FixedRewardActionDbfRecord> FixedRewardAction;

	public static Dbf<FixedRewardMapDbfRecord> FixedRewardMap;

	public static Dbf<GameModeDbfRecord> GameMode;

	public static Dbf<GameSaveSubkeyDbfRecord> GameSaveSubkey;

	public static Dbf<GlobalDbfRecord> Global;

	public static Dbf<GuestHeroDbfRecord> GuestHero;

	public static Dbf<GuestHeroSelectionRatioDbfRecord> GuestHeroSelectionRatio;

	public static Dbf<HiddenLicenseDbfRecord> HiddenLicense;

	public static Dbf<KeywordTextDbfRecord> KeywordText;

	public static Dbf<LeagueDbfRecord> League;

	public static Dbf<LeagueGameTypeDbfRecord> LeagueGameType;

	public static Dbf<LeagueRankDbfRecord> LeagueRank;

	public static Dbf<LoginPopupSequenceDbfRecord> LoginPopupSequence;

	public static Dbf<LoginPopupSequencePopupDbfRecord> LoginPopupSequencePopup;

	public static Dbf<LoginRewardDbfRecord> LoginReward;

	public static Dbf<MiniSetDbfRecord> MiniSet;

	public static Dbf<ModularBundleDbfRecord> ModularBundle;

	public static Dbf<ModularBundleLayoutDbfRecord> ModularBundleLayout;

	public static Dbf<ModularBundleLayoutNodeDbfRecord> ModularBundleLayoutNode;

	public static Dbf<MultiClassGroupDbfRecord> MultiClassGroup;

	public static Dbf<ProductDbfRecord> Product;

	public static Dbf<ProductClientDataDbfRecord> ProductClientData;

	public static Dbf<PvpdrSeasonDbfRecord> PvpdrSeason;

	public static Dbf<QuestDbfRecord> Quest;

	public static Dbf<QuestDialogDbfRecord> QuestDialog;

	public static Dbf<QuestDialogOnCompleteDbfRecord> QuestDialogOnComplete;

	public static Dbf<QuestDialogOnProgress1DbfRecord> QuestDialogOnProgress1;

	public static Dbf<QuestDialogOnProgress2DbfRecord> QuestDialogOnProgress2;

	public static Dbf<QuestDialogOnReceivedDbfRecord> QuestDialogOnReceived;

	public static Dbf<QuestModifierDbfRecord> QuestModifier;

	public static Dbf<QuestPoolDbfRecord> QuestPool;

	public static Dbf<RegionOverridesDbfRecord> RegionOverrides;

	public static Dbf<RewardBagDbfRecord> RewardBag;

	public static Dbf<RewardChestDbfRecord> RewardChest;

	public static Dbf<RewardChestContentsDbfRecord> RewardChestContents;

	public static Dbf<RewardItemDbfRecord> RewardItem;

	public static Dbf<RewardListDbfRecord> RewardList;

	public static Dbf<RewardTrackDbfRecord> RewardTrack;

	public static Dbf<RewardTrackLevelDbfRecord> RewardTrackLevel;

	public static Dbf<ScenarioDbfRecord> Scenario;

	public static Dbf<ScenarioGuestHeroesDbfRecord> ScenarioGuestHeroes;

	public static Dbf<ScheduledCharacterDialogDbfRecord> ScheduledCharacterDialog;

	public static Dbf<ScoreLabelDbfRecord> ScoreLabel;

	public static Dbf<SellableDeckDbfRecord> SellableDeck;

	public static Dbf<ShopTierDbfRecord> ShopTier;

	public static Dbf<ShopTierProductSaleDbfRecord> ShopTierProductSale;

	public static Dbf<SubsetDbfRecord> Subset;

	public static Dbf<SubsetCardDbfRecord> SubsetCard;

	public static Dbf<SubsetRuleDbfRecord> SubsetRule;

	public static Dbf<TavernBrawlTicketDbfRecord> TavernBrawlTicket;

	public static Dbf<TriggerDbfRecord> Trigger;

	public static Dbf<VisualBlacklistDbfRecord> VisualBlacklist;

	public static Dbf<WingDbfRecord> Wing;

	public static Dbf<XpPerGameTypeDbfRecord> XpPerGameType;

	public static bool IsLoaded;

	private static GameDbfIndex s_index;

	private static DOPAsset s_DOPAsset;

	public const string kDOPAssetPath = "Assets/Game/DOPAsset.asset";

	private static Action[] GetLoadDbfActions(DbfFormat format)
	{
		return new Action[103]
		{
			delegate
			{
				AccountLicense = Dbf<AccountLicenseDbfRecord>.Load("ACCOUNT_LICENSE", format);
			},
			delegate
			{
				Achieve = Dbf<AchieveDbfRecord>.Load("ACHIEVE", format);
			},
			delegate
			{
				AchieveCondition = Dbf<AchieveConditionDbfRecord>.Load("ACHIEVE_CONDITION", format);
			},
			delegate
			{
				AchieveRegionData = Dbf<AchieveRegionDataDbfRecord>.Load("ACHIEVE_REGION_DATA", format);
			},
			delegate
			{
				Achievement = Dbf<AchievementDbfRecord>.Load("ACHIEVEMENT", format);
			},
			delegate
			{
				AchievementCategory = Dbf<AchievementCategoryDbfRecord>.Load("ACHIEVEMENT_CATEGORY", format);
			},
			delegate
			{
				AchievementSection = Dbf<AchievementSectionDbfRecord>.Load("ACHIEVEMENT_SECTION", format);
			},
			delegate
			{
				AchievementSectionItem = Dbf<AchievementSectionItemDbfRecord>.Load("ACHIEVEMENT_SECTION_ITEM", format);
			},
			delegate
			{
				AchievementSubcategory = Dbf<AchievementSubcategoryDbfRecord>.Load("ACHIEVEMENT_SUBCATEGORY", format);
			},
			delegate
			{
				Adventure = Dbf<AdventureDbfRecord>.Load("ADVENTURE", format);
			},
			delegate
			{
				AdventureData = Dbf<AdventureDataDbfRecord>.Load("ADVENTURE_DATA", format);
			},
			delegate
			{
				AdventureDeck = Dbf<AdventureDeckDbfRecord>.Load("ADVENTURE_DECK", format);
			},
			delegate
			{
				AdventureGuestHeroes = Dbf<AdventureGuestHeroesDbfRecord>.Load("ADVENTURE_GUEST_HEROES", format);
			},
			delegate
			{
				AdventureHeroPower = Dbf<AdventureHeroPowerDbfRecord>.Load("ADVENTURE_HERO_POWER", format);
			},
			delegate
			{
				AdventureLoadoutTreasures = Dbf<AdventureLoadoutTreasuresDbfRecord>.Load("ADVENTURE_LOADOUT_TREASURES", format);
			},
			delegate
			{
				AdventureMission = Dbf<AdventureMissionDbfRecord>.Load("ADVENTURE_MISSION", format);
			},
			delegate
			{
				AdventureMode = Dbf<AdventureModeDbfRecord>.Load("ADVENTURE_MODE", format);
			},
			delegate
			{
				Banner = Dbf<BannerDbfRecord>.Load("BANNER", format);
			},
			delegate
			{
				BattlegroundsSeason = Dbf<BattlegroundsSeasonDbfRecord>.Load("BATTLEGROUNDS_SEASON", format);
			},
			delegate
			{
				Board = Dbf<BoardDbfRecord>.Load("BOARD", format);
			},
			delegate
			{
				Booster = Dbf<BoosterDbfRecord>.Load("BOOSTER", format);
			},
			delegate
			{
				BoosterCardSet = Dbf<BoosterCardSetDbfRecord>.Load("BOOSTER_CARD_SET", format);
			},
			delegate
			{
				Card = Dbf<CardDbfRecord>.Load("CARD", format);
			},
			delegate
			{
				CardAdditonalSearchTerms = Dbf<CardAdditonalSearchTermsDbfRecord>.Load("CARD_ADDITONAL_SEARCH_TERMS", format);
			},
			delegate
			{
				CardBack = Dbf<CardBackDbfRecord>.Load("CARD_BACK", format);
			},
			delegate
			{
				CardChange = Dbf<CardChangeDbfRecord>.Load("CARD_CHANGE", format);
			},
			delegate
			{
				CardDiscoverString = Dbf<CardDiscoverStringDbfRecord>.Load("CARD_DISCOVER_STRING", format);
			},
			delegate
			{
				CardHero = Dbf<CardHeroDbfRecord>.Load("CARD_HERO", format);
			},
			delegate
			{
				CardPlayerDeckOverride = Dbf<CardPlayerDeckOverrideDbfRecord>.Load("CARD_PLAYER_DECK_OVERRIDE", format);
			},
			delegate
			{
				CardSet = Dbf<CardSetDbfRecord>.Load("CARD_SET", format);
			},
			delegate
			{
				CardSetSpellOverride = Dbf<CardSetSpellOverrideDbfRecord>.Load("CARD_SET_SPELL_OVERRIDE", format);
			},
			delegate
			{
				CardSetTiming = Dbf<CardSetTimingDbfRecord>.Load("CARD_SET_TIMING", format);
			},
			delegate
			{
				CardTag = Dbf<CardTagDbfRecord>.Load("CARD_TAG", format);
			},
			delegate
			{
				CharacterDialog = Dbf<CharacterDialogDbfRecord>.Load("CHARACTER_DIALOG", format);
			},
			delegate
			{
				CharacterDialogItems = Dbf<CharacterDialogItemsDbfRecord>.Load("CHARACTER_DIALOG_ITEMS", format);
			},
			delegate
			{
				Class = Dbf<ClassDbfRecord>.Load("CLASS", format);
			},
			delegate
			{
				ClassExclusions = Dbf<ClassExclusionsDbfRecord>.Load("CLASS_EXCLUSIONS", format);
			},
			delegate
			{
				ClientString = Dbf<ClientStringDbfRecord>.Load("CLIENT_STRING", format);
			},
			delegate
			{
				Coin = Dbf<CoinDbfRecord>.Load("COIN", format);
			},
			delegate
			{
				CreditsYear = Dbf<CreditsYearDbfRecord>.Load("CREDITS_YEAR", format);
			},
			delegate
			{
				Deck = Dbf<DeckDbfRecord>.Load("DECK", format);
			},
			delegate
			{
				DeckCard = Dbf<DeckCardDbfRecord>.Load("DECK_CARD", format);
			},
			delegate
			{
				DeckRuleset = Dbf<DeckRulesetDbfRecord>.Load("DECK_RULESET", format);
			},
			delegate
			{
				DeckRulesetRule = Dbf<DeckRulesetRuleDbfRecord>.Load("DECK_RULESET_RULE", format);
			},
			delegate
			{
				DeckRulesetRuleSubset = Dbf<DeckRulesetRuleSubsetDbfRecord>.Load("DECK_RULESET_RULE_SUBSET", format);
			},
			delegate
			{
				DeckTemplate = Dbf<DeckTemplateDbfRecord>.Load("DECK_TEMPLATE", format);
			},
			delegate
			{
				DraftContent = Dbf<DraftContentDbfRecord>.Load("DRAFT_CONTENT", format);
			},
			delegate
			{
				ExternalUrl = Dbf<ExternalUrlDbfRecord>.Load("EXTERNAL_URL", format);
			},
			delegate
			{
				FixedReward = Dbf<FixedRewardDbfRecord>.Load("FIXED_REWARD", format);
			},
			delegate
			{
				FixedRewardAction = Dbf<FixedRewardActionDbfRecord>.Load("FIXED_REWARD_ACTION", format);
			},
			delegate
			{
				FixedRewardMap = Dbf<FixedRewardMapDbfRecord>.Load("FIXED_REWARD_MAP", format);
			},
			delegate
			{
				GameMode = Dbf<GameModeDbfRecord>.Load("GAME_MODE", format);
			},
			delegate
			{
				GameSaveSubkey = Dbf<GameSaveSubkeyDbfRecord>.Load("GAME_SAVE_SUBKEY", format);
			},
			delegate
			{
				Global = Dbf<GlobalDbfRecord>.Load("GLOBAL", format);
			},
			delegate
			{
				GuestHero = Dbf<GuestHeroDbfRecord>.Load("GUEST_HERO", format);
			},
			delegate
			{
				GuestHeroSelectionRatio = Dbf<GuestHeroSelectionRatioDbfRecord>.Load("GUEST_HERO_SELECTION_RATIO", format);
			},
			delegate
			{
				HiddenLicense = Dbf<HiddenLicenseDbfRecord>.Load("HIDDEN_LICENSE", format);
			},
			delegate
			{
				KeywordText = Dbf<KeywordTextDbfRecord>.Load("KEYWORD_TEXT", format);
			},
			delegate
			{
				League = Dbf<LeagueDbfRecord>.Load("LEAGUE", format);
			},
			delegate
			{
				LeagueGameType = Dbf<LeagueGameTypeDbfRecord>.Load("LEAGUE_GAME_TYPE", format);
			},
			delegate
			{
				LeagueRank = Dbf<LeagueRankDbfRecord>.Load("LEAGUE_RANK", format);
			},
			delegate
			{
				LoginPopupSequence = Dbf<LoginPopupSequenceDbfRecord>.Load("LOGIN_POPUP_SEQUENCE", format);
			},
			delegate
			{
				LoginPopupSequencePopup = Dbf<LoginPopupSequencePopupDbfRecord>.Load("LOGIN_POPUP_SEQUENCE_POPUP", format);
			},
			delegate
			{
				LoginReward = Dbf<LoginRewardDbfRecord>.Load("LOGIN_REWARD", format);
			},
			delegate
			{
				MiniSet = Dbf<MiniSetDbfRecord>.Load("MINI_SET", format);
			},
			delegate
			{
				ModularBundle = Dbf<ModularBundleDbfRecord>.Load("MODULAR_BUNDLE", format);
			},
			delegate
			{
				ModularBundleLayout = Dbf<ModularBundleLayoutDbfRecord>.Load("MODULAR_BUNDLE_LAYOUT", format);
			},
			delegate
			{
				ModularBundleLayoutNode = Dbf<ModularBundleLayoutNodeDbfRecord>.Load("MODULAR_BUNDLE_LAYOUT_NODE", format);
			},
			delegate
			{
				MultiClassGroup = Dbf<MultiClassGroupDbfRecord>.Load("MULTI_CLASS_GROUP", format);
			},
			delegate
			{
				Product = Dbf<ProductDbfRecord>.Load("PRODUCT", format);
			},
			delegate
			{
				ProductClientData = Dbf<ProductClientDataDbfRecord>.Load("PRODUCT_CLIENT_DATA", format);
			},
			delegate
			{
				PvpdrSeason = Dbf<PvpdrSeasonDbfRecord>.Load("PVPDR_SEASON", format);
			},
			delegate
			{
				Quest = Dbf<QuestDbfRecord>.Load("QUEST", format);
			},
			delegate
			{
				QuestDialog = Dbf<QuestDialogDbfRecord>.Load("QUEST_DIALOG", format);
			},
			delegate
			{
				QuestDialogOnComplete = Dbf<QuestDialogOnCompleteDbfRecord>.Load("QUEST_DIALOG_ON_COMPLETE", format);
			},
			delegate
			{
				QuestDialogOnProgress1 = Dbf<QuestDialogOnProgress1DbfRecord>.Load("QUEST_DIALOG_ON_PROGRESS1", format);
			},
			delegate
			{
				QuestDialogOnProgress2 = Dbf<QuestDialogOnProgress2DbfRecord>.Load("QUEST_DIALOG_ON_PROGRESS2", format);
			},
			delegate
			{
				QuestDialogOnReceived = Dbf<QuestDialogOnReceivedDbfRecord>.Load("QUEST_DIALOG_ON_RECEIVED", format);
			},
			delegate
			{
				QuestModifier = Dbf<QuestModifierDbfRecord>.Load("QUEST_MODIFIER", format);
			},
			delegate
			{
				QuestPool = Dbf<QuestPoolDbfRecord>.Load("QUEST_POOL", format);
			},
			delegate
			{
				RegionOverrides = Dbf<RegionOverridesDbfRecord>.Load("REGION_OVERRIDES", format);
			},
			delegate
			{
				RewardBag = Dbf<RewardBagDbfRecord>.Load("REWARD_BAG", format);
			},
			delegate
			{
				RewardChest = Dbf<RewardChestDbfRecord>.Load("REWARD_CHEST", format);
			},
			delegate
			{
				RewardChestContents = Dbf<RewardChestContentsDbfRecord>.Load("REWARD_CHEST_CONTENTS", format);
			},
			delegate
			{
				RewardItem = Dbf<RewardItemDbfRecord>.Load("REWARD_ITEM", format);
			},
			delegate
			{
				RewardList = Dbf<RewardListDbfRecord>.Load("REWARD_LIST", format);
			},
			delegate
			{
				RewardTrack = Dbf<RewardTrackDbfRecord>.Load("REWARD_TRACK", format);
			},
			delegate
			{
				RewardTrackLevel = Dbf<RewardTrackLevelDbfRecord>.Load("REWARD_TRACK_LEVEL", format);
			},
			delegate
			{
				Scenario = Dbf<ScenarioDbfRecord>.Load("SCENARIO", format);
			},
			delegate
			{
				ScenarioGuestHeroes = Dbf<ScenarioGuestHeroesDbfRecord>.Load("SCENARIO_GUEST_HEROES", format);
			},
			delegate
			{
				ScheduledCharacterDialog = Dbf<ScheduledCharacterDialogDbfRecord>.Load("SCHEDULED_CHARACTER_DIALOG", format);
			},
			delegate
			{
				ScoreLabel = Dbf<ScoreLabelDbfRecord>.Load("SCORE_LABEL", format);
			},
			delegate
			{
				SellableDeck = Dbf<SellableDeckDbfRecord>.Load("SELLABLE_DECK", format);
			},
			delegate
			{
				ShopTier = Dbf<ShopTierDbfRecord>.Load("SHOP_TIER", format);
			},
			delegate
			{
				ShopTierProductSale = Dbf<ShopTierProductSaleDbfRecord>.Load("SHOP_TIER_PRODUCT_SALE", format);
			},
			delegate
			{
				Subset = Dbf<SubsetDbfRecord>.Load("SUBSET", format);
			},
			delegate
			{
				SubsetCard = Dbf<SubsetCardDbfRecord>.Load("SUBSET_CARD", format);
			},
			delegate
			{
				SubsetRule = Dbf<SubsetRuleDbfRecord>.Load("SUBSET_RULE", format);
			},
			delegate
			{
				TavernBrawlTicket = Dbf<TavernBrawlTicketDbfRecord>.Load("TAVERN_BRAWL_TICKET", format);
			},
			delegate
			{
				Trigger = Dbf<TriggerDbfRecord>.Load("TRIGGER", format);
			},
			delegate
			{
				VisualBlacklist = Dbf<VisualBlacklistDbfRecord>.Load("VISUAL_BLACKLIST", format);
			},
			delegate
			{
				Wing = Dbf<WingDbfRecord>.Load("WING", format);
			},
			delegate
			{
				XpPerGameType = Dbf<XpPerGameTypeDbfRecord>.Load("XP_PER_GAME_TYPE", format);
			}
		};
	}

	private static JobResultCollection GetLoadDbfJobs(DbfFormat format)
	{
		return new JobResultCollection(Dbf<AccountLicenseDbfRecord>.CreateLoadAsyncJob("ACCOUNT_LICENSE", format, ref AccountLicense), Dbf<AchieveDbfRecord>.CreateLoadAsyncJob("ACHIEVE", format, ref Achieve), Dbf<AchieveConditionDbfRecord>.CreateLoadAsyncJob("ACHIEVE_CONDITION", format, ref AchieveCondition), Dbf<AchieveRegionDataDbfRecord>.CreateLoadAsyncJob("ACHIEVE_REGION_DATA", format, ref AchieveRegionData), Dbf<AchievementDbfRecord>.CreateLoadAsyncJob("ACHIEVEMENT", format, ref Achievement), Dbf<AchievementCategoryDbfRecord>.CreateLoadAsyncJob("ACHIEVEMENT_CATEGORY", format, ref AchievementCategory), Dbf<AchievementSectionDbfRecord>.CreateLoadAsyncJob("ACHIEVEMENT_SECTION", format, ref AchievementSection), Dbf<AchievementSectionItemDbfRecord>.CreateLoadAsyncJob("ACHIEVEMENT_SECTION_ITEM", format, ref AchievementSectionItem), Dbf<AchievementSubcategoryDbfRecord>.CreateLoadAsyncJob("ACHIEVEMENT_SUBCATEGORY", format, ref AchievementSubcategory), Dbf<AdventureDbfRecord>.CreateLoadAsyncJob("ADVENTURE", format, ref Adventure), Dbf<AdventureDataDbfRecord>.CreateLoadAsyncJob("ADVENTURE_DATA", format, ref AdventureData), Dbf<AdventureDeckDbfRecord>.CreateLoadAsyncJob("ADVENTURE_DECK", format, ref AdventureDeck), Dbf<AdventureGuestHeroesDbfRecord>.CreateLoadAsyncJob("ADVENTURE_GUEST_HEROES", format, ref AdventureGuestHeroes), Dbf<AdventureHeroPowerDbfRecord>.CreateLoadAsyncJob("ADVENTURE_HERO_POWER", format, ref AdventureHeroPower), Dbf<AdventureLoadoutTreasuresDbfRecord>.CreateLoadAsyncJob("ADVENTURE_LOADOUT_TREASURES", format, ref AdventureLoadoutTreasures), Dbf<AdventureMissionDbfRecord>.CreateLoadAsyncJob("ADVENTURE_MISSION", format, ref AdventureMission), Dbf<AdventureModeDbfRecord>.CreateLoadAsyncJob("ADVENTURE_MODE", format, ref AdventureMode), Dbf<BannerDbfRecord>.CreateLoadAsyncJob("BANNER", format, ref Banner), Dbf<BattlegroundsSeasonDbfRecord>.CreateLoadAsyncJob("BATTLEGROUNDS_SEASON", format, ref BattlegroundsSeason), Dbf<BoardDbfRecord>.CreateLoadAsyncJob("BOARD", format, ref Board), Dbf<BoosterDbfRecord>.CreateLoadAsyncJob("BOOSTER", format, ref Booster), Dbf<BoosterCardSetDbfRecord>.CreateLoadAsyncJob("BOOSTER_CARD_SET", format, ref BoosterCardSet), Dbf<CardDbfRecord>.CreateLoadAsyncJob("CARD", format, ref Card), Dbf<CardAdditonalSearchTermsDbfRecord>.CreateLoadAsyncJob("CARD_ADDITONAL_SEARCH_TERMS", format, ref CardAdditonalSearchTerms), Dbf<CardBackDbfRecord>.CreateLoadAsyncJob("CARD_BACK", format, ref CardBack), Dbf<CardChangeDbfRecord>.CreateLoadAsyncJob("CARD_CHANGE", format, ref CardChange), Dbf<CardDiscoverStringDbfRecord>.CreateLoadAsyncJob("CARD_DISCOVER_STRING", format, ref CardDiscoverString), Dbf<CardHeroDbfRecord>.CreateLoadAsyncJob("CARD_HERO", format, ref CardHero), Dbf<CardPlayerDeckOverrideDbfRecord>.CreateLoadAsyncJob("CARD_PLAYER_DECK_OVERRIDE", format, ref CardPlayerDeckOverride), Dbf<CardSetDbfRecord>.CreateLoadAsyncJob("CARD_SET", format, ref CardSet), Dbf<CardSetSpellOverrideDbfRecord>.CreateLoadAsyncJob("CARD_SET_SPELL_OVERRIDE", format, ref CardSetSpellOverride), Dbf<CardSetTimingDbfRecord>.CreateLoadAsyncJob("CARD_SET_TIMING", format, ref CardSetTiming), Dbf<CardTagDbfRecord>.CreateLoadAsyncJob("CARD_TAG", format, ref CardTag), Dbf<CharacterDialogDbfRecord>.CreateLoadAsyncJob("CHARACTER_DIALOG", format, ref CharacterDialog), Dbf<CharacterDialogItemsDbfRecord>.CreateLoadAsyncJob("CHARACTER_DIALOG_ITEMS", format, ref CharacterDialogItems), Dbf<ClassDbfRecord>.CreateLoadAsyncJob("CLASS", format, ref Class), Dbf<ClassExclusionsDbfRecord>.CreateLoadAsyncJob("CLASS_EXCLUSIONS", format, ref ClassExclusions), Dbf<ClientStringDbfRecord>.CreateLoadAsyncJob("CLIENT_STRING", format, ref ClientString), Dbf<CoinDbfRecord>.CreateLoadAsyncJob("COIN", format, ref Coin), Dbf<CreditsYearDbfRecord>.CreateLoadAsyncJob("CREDITS_YEAR", format, ref CreditsYear), Dbf<DeckDbfRecord>.CreateLoadAsyncJob("DECK", format, ref Deck), Dbf<DeckCardDbfRecord>.CreateLoadAsyncJob("DECK_CARD", format, ref DeckCard), Dbf<DeckRulesetDbfRecord>.CreateLoadAsyncJob("DECK_RULESET", format, ref DeckRuleset), Dbf<DeckRulesetRuleDbfRecord>.CreateLoadAsyncJob("DECK_RULESET_RULE", format, ref DeckRulesetRule), Dbf<DeckRulesetRuleSubsetDbfRecord>.CreateLoadAsyncJob("DECK_RULESET_RULE_SUBSET", format, ref DeckRulesetRuleSubset), Dbf<DeckTemplateDbfRecord>.CreateLoadAsyncJob("DECK_TEMPLATE", format, ref DeckTemplate), Dbf<DraftContentDbfRecord>.CreateLoadAsyncJob("DRAFT_CONTENT", format, ref DraftContent), Dbf<ExternalUrlDbfRecord>.CreateLoadAsyncJob("EXTERNAL_URL", format, ref ExternalUrl), Dbf<FixedRewardDbfRecord>.CreateLoadAsyncJob("FIXED_REWARD", format, ref FixedReward), Dbf<FixedRewardActionDbfRecord>.CreateLoadAsyncJob("FIXED_REWARD_ACTION", format, ref FixedRewardAction), Dbf<FixedRewardMapDbfRecord>.CreateLoadAsyncJob("FIXED_REWARD_MAP", format, ref FixedRewardMap), Dbf<GameModeDbfRecord>.CreateLoadAsyncJob("GAME_MODE", format, ref GameMode), Dbf<GameSaveSubkeyDbfRecord>.CreateLoadAsyncJob("GAME_SAVE_SUBKEY", format, ref GameSaveSubkey), Dbf<GlobalDbfRecord>.CreateLoadAsyncJob("GLOBAL", format, ref Global), Dbf<GuestHeroDbfRecord>.CreateLoadAsyncJob("GUEST_HERO", format, ref GuestHero), Dbf<GuestHeroSelectionRatioDbfRecord>.CreateLoadAsyncJob("GUEST_HERO_SELECTION_RATIO", format, ref GuestHeroSelectionRatio), Dbf<HiddenLicenseDbfRecord>.CreateLoadAsyncJob("HIDDEN_LICENSE", format, ref HiddenLicense), Dbf<KeywordTextDbfRecord>.CreateLoadAsyncJob("KEYWORD_TEXT", format, ref KeywordText), Dbf<LeagueDbfRecord>.CreateLoadAsyncJob("LEAGUE", format, ref League), Dbf<LeagueGameTypeDbfRecord>.CreateLoadAsyncJob("LEAGUE_GAME_TYPE", format, ref LeagueGameType), Dbf<LeagueRankDbfRecord>.CreateLoadAsyncJob("LEAGUE_RANK", format, ref LeagueRank), Dbf<LoginPopupSequenceDbfRecord>.CreateLoadAsyncJob("LOGIN_POPUP_SEQUENCE", format, ref LoginPopupSequence), Dbf<LoginPopupSequencePopupDbfRecord>.CreateLoadAsyncJob("LOGIN_POPUP_SEQUENCE_POPUP", format, ref LoginPopupSequencePopup), Dbf<LoginRewardDbfRecord>.CreateLoadAsyncJob("LOGIN_REWARD", format, ref LoginReward), Dbf<MiniSetDbfRecord>.CreateLoadAsyncJob("MINI_SET", format, ref MiniSet), Dbf<ModularBundleDbfRecord>.CreateLoadAsyncJob("MODULAR_BUNDLE", format, ref ModularBundle), Dbf<ModularBundleLayoutDbfRecord>.CreateLoadAsyncJob("MODULAR_BUNDLE_LAYOUT", format, ref ModularBundleLayout), Dbf<ModularBundleLayoutNodeDbfRecord>.CreateLoadAsyncJob("MODULAR_BUNDLE_LAYOUT_NODE", format, ref ModularBundleLayoutNode), Dbf<MultiClassGroupDbfRecord>.CreateLoadAsyncJob("MULTI_CLASS_GROUP", format, ref MultiClassGroup), Dbf<ProductDbfRecord>.CreateLoadAsyncJob("PRODUCT", format, ref Product), Dbf<ProductClientDataDbfRecord>.CreateLoadAsyncJob("PRODUCT_CLIENT_DATA", format, ref ProductClientData), Dbf<PvpdrSeasonDbfRecord>.CreateLoadAsyncJob("PVPDR_SEASON", format, ref PvpdrSeason), Dbf<QuestDbfRecord>.CreateLoadAsyncJob("QUEST", format, ref Quest), Dbf<QuestDialogDbfRecord>.CreateLoadAsyncJob("QUEST_DIALOG", format, ref QuestDialog), Dbf<QuestDialogOnCompleteDbfRecord>.CreateLoadAsyncJob("QUEST_DIALOG_ON_COMPLETE", format, ref QuestDialogOnComplete), Dbf<QuestDialogOnProgress1DbfRecord>.CreateLoadAsyncJob("QUEST_DIALOG_ON_PROGRESS1", format, ref QuestDialogOnProgress1), Dbf<QuestDialogOnProgress2DbfRecord>.CreateLoadAsyncJob("QUEST_DIALOG_ON_PROGRESS2", format, ref QuestDialogOnProgress2), Dbf<QuestDialogOnReceivedDbfRecord>.CreateLoadAsyncJob("QUEST_DIALOG_ON_RECEIVED", format, ref QuestDialogOnReceived), Dbf<QuestModifierDbfRecord>.CreateLoadAsyncJob("QUEST_MODIFIER", format, ref QuestModifier), Dbf<QuestPoolDbfRecord>.CreateLoadAsyncJob("QUEST_POOL", format, ref QuestPool), Dbf<RegionOverridesDbfRecord>.CreateLoadAsyncJob("REGION_OVERRIDES", format, ref RegionOverrides), Dbf<RewardBagDbfRecord>.CreateLoadAsyncJob("REWARD_BAG", format, ref RewardBag), Dbf<RewardChestDbfRecord>.CreateLoadAsyncJob("REWARD_CHEST", format, ref RewardChest), Dbf<RewardChestContentsDbfRecord>.CreateLoadAsyncJob("REWARD_CHEST_CONTENTS", format, ref RewardChestContents), Dbf<RewardItemDbfRecord>.CreateLoadAsyncJob("REWARD_ITEM", format, ref RewardItem), Dbf<RewardListDbfRecord>.CreateLoadAsyncJob("REWARD_LIST", format, ref RewardList), Dbf<RewardTrackDbfRecord>.CreateLoadAsyncJob("REWARD_TRACK", format, ref RewardTrack), Dbf<RewardTrackLevelDbfRecord>.CreateLoadAsyncJob("REWARD_TRACK_LEVEL", format, ref RewardTrackLevel), Dbf<ScenarioDbfRecord>.CreateLoadAsyncJob("SCENARIO", format, ref Scenario), Dbf<ScenarioGuestHeroesDbfRecord>.CreateLoadAsyncJob("SCENARIO_GUEST_HEROES", format, ref ScenarioGuestHeroes), Dbf<ScheduledCharacterDialogDbfRecord>.CreateLoadAsyncJob("SCHEDULED_CHARACTER_DIALOG", format, ref ScheduledCharacterDialog), Dbf<ScoreLabelDbfRecord>.CreateLoadAsyncJob("SCORE_LABEL", format, ref ScoreLabel), Dbf<SellableDeckDbfRecord>.CreateLoadAsyncJob("SELLABLE_DECK", format, ref SellableDeck), Dbf<ShopTierDbfRecord>.CreateLoadAsyncJob("SHOP_TIER", format, ref ShopTier), Dbf<ShopTierProductSaleDbfRecord>.CreateLoadAsyncJob("SHOP_TIER_PRODUCT_SALE", format, ref ShopTierProductSale), Dbf<SubsetDbfRecord>.CreateLoadAsyncJob("SUBSET", format, ref Subset), Dbf<SubsetCardDbfRecord>.CreateLoadAsyncJob("SUBSET_CARD", format, ref SubsetCard), Dbf<SubsetRuleDbfRecord>.CreateLoadAsyncJob("SUBSET_RULE", format, ref SubsetRule), Dbf<TavernBrawlTicketDbfRecord>.CreateLoadAsyncJob("TAVERN_BRAWL_TICKET", format, ref TavernBrawlTicket), Dbf<TriggerDbfRecord>.CreateLoadAsyncJob("TRIGGER", format, ref Trigger), Dbf<VisualBlacklistDbfRecord>.CreateLoadAsyncJob("VISUAL_BLACKLIST", format, ref VisualBlacklist), Dbf<WingDbfRecord>.CreateLoadAsyncJob("WING", format, ref Wing), Dbf<XpPerGameTypeDbfRecord>.CreateLoadAsyncJob("XP_PER_GAME_TYPE", format, ref XpPerGameType));
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield return CreateLoadDbfJob();
	}

	public Type[] GetDependencies()
	{
		return null;
	}

	public void Shutdown()
	{
	}

	public static GameDbfIndex GetIndex()
	{
		if (s_index == null)
		{
			s_index = new GameDbfIndex();
		}
		return s_index;
	}

	public static bool ShouldForceXmlLoading()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return false;
		}
		if (HearthstoneApplication.UsingStandaloneLocalData())
		{
			return true;
		}
		object option = Options.Get().GetOption(Option.DBF_XML_LOADING);
		if (option == null)
		{
			return AssetLoaderPrefs.AssetLoadingMethod == AssetLoaderPrefs.ASSET_LOADING_METHOD.EDITOR_FILES;
		}
		return (bool)option;
	}

	public static JobDefinition CreateLoadDbfJob()
	{
		return new JobDefinition("GameDbf.Load", Load());
	}

	public static void LoadXml()
	{
		IEnumerator enumerator = Load(useXmlLoading: true, useAssetJobs: false);
		while (enumerator.MoveNext())
		{
		}
	}

	public static IEnumerator<IAsyncJobResult> Load()
	{
		return Load(useXmlLoading: false, useAssetJobs: true);
	}

	public static IEnumerator<IAsyncJobResult> Load(bool useXmlLoading)
	{
		return Load(useXmlLoading, useAssetJobs: true);
	}

	public static IEnumerator<IAsyncJobResult> Load(bool useXmlLoading, bool useAssetJobs)
	{
		if (HearthstoneApplication.IsHearthstoneRunning)
		{
			yield return new WaitForGameDownloadManagerState();
		}
		if (s_index == null)
		{
			s_index = new GameDbfIndex();
		}
		else
		{
			s_index.Initialize();
		}
		if (ShouldForceXmlLoading())
		{
			useXmlLoading = true;
		}
		DbfFormat format = (useXmlLoading ? DbfFormat.XML : DbfFormat.ASSET);
		DbfShared.Reset();
		if (!useXmlLoading)
		{
			if (useAssetJobs)
			{
				yield return new JobDefinition("GameDbf.LoadDBFSharedAssetBundle", DbfShared.Job_LoadSharedDBFAssetBundle());
			}
			else
			{
				DbfShared.LoadSharedAssetBundle();
			}
		}
		Log.Dbf.Print("Loading DBFS with format={0}", format);
		CPUTimeSoftYield softYielder = new CPUTimeSoftYield(1f / (float)Application.targetFrameRate * 0.8f);
		Action[] loadActions;
		int i;
		if (!useAssetJobs)
		{
			loadActions = GetLoadDbfActions(format);
			Action[] array = loadActions;
			for (i = 0; i < array.Length; i++)
			{
				array[i]();
				if (softYielder.ShouldSoftYield())
				{
					yield return null;
					softYielder.NewFrame();
				}
			}
		}
		else
		{
			yield return GetLoadDbfJobs(format);
		}
		loadActions = GetPostProcessDbfActions();
		i = 0;
		while (i < loadActions.Length)
		{
			loadActions[i]();
			if (softYielder.ShouldSoftYield())
			{
				yield return null;
				softYielder.NewFrame();
			}
			int num = i + 1;
			i = num;
		}
		IsLoaded = true;
		SetDbfCallbacksForIndexing();
	}

	private static Action[] GetPostProcessDbfActions()
	{
		return new Action[12]
		{
			delegate
			{
				s_index.PostProcessDbfLoad_CardTag(CardTag);
			},
			delegate
			{
				s_index.PostProcessDbfLoad_CardChange(CardChange);
			},
			delegate
			{
				s_index.PostProcessDbfLoad_Card(Card);
			},
			delegate
			{
				s_index.PostProcessDbfLoad_CardDiscoverString(CardDiscoverString);
			},
			delegate
			{
				s_index.PostProcessDbfLoad_CardSetSpellOverride(CardSetSpellOverride);
			},
			delegate
			{
				s_index.PostProcessDbfLoad_DeckRulesetRule(DeckRulesetRule);
			},
			delegate
			{
				s_index.PostProcessDbfLoad_DeckRulesetRuleSubset(DeckRulesetRuleSubset);
			},
			delegate
			{
				s_index.PostProcessDbfLoad_FixedRewardAction(FixedRewardAction);
			},
			delegate
			{
				s_index.PostProcessDbfLoad_FixedRewardMap(FixedRewardMap);
			},
			delegate
			{
				s_index.PostProcessDbfLoad_SubsetCard(SubsetCard);
			},
			delegate
			{
				s_index.PostProcessDbfLoad_CardPlayerDeckOverride(CardPlayerDeckOverride);
			},
			delegate
			{
				RankMgr.Get().PostProcessDbfLoad_League();
			}
		};
	}

	private static void SetDbfCallbacksForIndexing()
	{
		CardTag.AddListeners(s_index.OnCardTagAdded, s_index.OnCardTagRemoved);
		CardChange.AddListeners(s_index.OnCardChangeAdded, s_index.OnCardChangeRemoved);
		Card.AddListeners(s_index.OnCardAdded, s_index.OnCardRemoved);
		CardDiscoverString.AddListeners(s_index.OnCardDiscoverStringAdded, s_index.OnCardDiscoverStringRemoved);
		CardSetSpellOverride.AddListeners(s_index.OnCardSetSpellOverrideAdded, s_index.OnCardSetSpellOverrideRemoved);
		CardPlayerDeckOverride.AddListeners(s_index.OnCardPlayerDeckOverrideAdded, s_index.OnCardPlayerDeckOverrideRemoved);
		DeckRulesetRule.AddListeners(s_index.OnDeckRulesetRuleAdded, s_index.OnDeckRulesetRuleRemoved);
		DeckRulesetRuleSubset.AddListeners(s_index.OnDeckRulesetRuleSubsetAdded, s_index.OnDeckRulesetRuleSubsetRemoved);
		FixedRewardAction.AddListeners(s_index.OnFixedRewardActionAdded, s_index.OnFixedRewardActionRemoved);
		FixedRewardMap.AddListeners(s_index.OnFixedRewardMapAdded, s_index.OnFixedRewardMapRemoved);
		SubsetCard.AddListeners(s_index.OnSubsetCardAdded, s_index.OnSubsetCardRemoved);
	}

	public static void Reload(string name, string xml)
	{
		if (!(name == "ACHIEVE"))
		{
			if (name == "CARD_BACK")
			{
				CardBack = Dbf<CardBackDbfRecord>.Load(name, DbfFormat.XML);
				if (HearthstoneServices.TryGet<CardBackManager>(out var service))
				{
					service.InitCardBackData();
				}
			}
			else
			{
				Error.AddDevFatal("Reloading {0} is unsupported", name);
			}
		}
		else
		{
			Achieve = Dbf<AchieveDbfRecord>.Load(name, DbfFormat.XML);
			if (HearthstoneServices.TryGet<AchieveManager>(out var service2))
			{
				service2.InitAchieveManager();
			}
		}
	}

	public static int GetDataVersion()
	{
		return GetDOPAsset().DataVersion;
	}

	private static DOPAsset GetDOPAsset()
	{
		if (s_DOPAsset == null)
		{
			if (Application.isEditor && AssetLoaderPrefs.AssetLoadingMethod == AssetLoaderPrefs.ASSET_LOADING_METHOD.EDITOR_FILES)
			{
				s_DOPAsset = DOPAsset.GenerateDOPAsset();
			}
			else
			{
				AssetBundle assetBundle = DbfShared.GetAssetBundle();
				if (assetBundle != null)
				{
					s_DOPAsset = assetBundle.LoadAsset<DOPAsset>("Assets/Game/DOPAsset.asset");
				}
				if (s_DOPAsset == null)
				{
					Log.Dbf.PrintWarning("Failed to load DOP asset, generating default...");
					s_DOPAsset = DOPAsset.GenerateDOPAsset();
				}
			}
		}
		return s_DOPAsset;
	}
}
