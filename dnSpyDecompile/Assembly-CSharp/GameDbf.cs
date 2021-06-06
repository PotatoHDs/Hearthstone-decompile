using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using UnityEngine;

// Token: 0x020001E7 RID: 487
public class GameDbf : IService
{
	// Token: 0x06001B7C RID: 7036 RVA: 0x0008F1B8 File Offset: 0x0008D3B8
	private static Action[] GetLoadDbfActions(DbfFormat format)
	{
		return new Action[]
		{
			delegate()
			{
				GameDbf.AccountLicense = Dbf<AccountLicenseDbfRecord>.Load("ACCOUNT_LICENSE", format);
			},
			delegate()
			{
				GameDbf.Achieve = Dbf<AchieveDbfRecord>.Load("ACHIEVE", format);
			},
			delegate()
			{
				GameDbf.AchieveCondition = Dbf<AchieveConditionDbfRecord>.Load("ACHIEVE_CONDITION", format);
			},
			delegate()
			{
				GameDbf.AchieveRegionData = Dbf<AchieveRegionDataDbfRecord>.Load("ACHIEVE_REGION_DATA", format);
			},
			delegate()
			{
				GameDbf.Achievement = Dbf<AchievementDbfRecord>.Load("ACHIEVEMENT", format);
			},
			delegate()
			{
				GameDbf.AchievementCategory = Dbf<AchievementCategoryDbfRecord>.Load("ACHIEVEMENT_CATEGORY", format);
			},
			delegate()
			{
				GameDbf.AchievementSection = Dbf<AchievementSectionDbfRecord>.Load("ACHIEVEMENT_SECTION", format);
			},
			delegate()
			{
				GameDbf.AchievementSectionItem = Dbf<AchievementSectionItemDbfRecord>.Load("ACHIEVEMENT_SECTION_ITEM", format);
			},
			delegate()
			{
				GameDbf.AchievementSubcategory = Dbf<AchievementSubcategoryDbfRecord>.Load("ACHIEVEMENT_SUBCATEGORY", format);
			},
			delegate()
			{
				GameDbf.Adventure = Dbf<AdventureDbfRecord>.Load("ADVENTURE", format);
			},
			delegate()
			{
				GameDbf.AdventureData = Dbf<AdventureDataDbfRecord>.Load("ADVENTURE_DATA", format);
			},
			delegate()
			{
				GameDbf.AdventureDeck = Dbf<AdventureDeckDbfRecord>.Load("ADVENTURE_DECK", format);
			},
			delegate()
			{
				GameDbf.AdventureGuestHeroes = Dbf<AdventureGuestHeroesDbfRecord>.Load("ADVENTURE_GUEST_HEROES", format);
			},
			delegate()
			{
				GameDbf.AdventureHeroPower = Dbf<AdventureHeroPowerDbfRecord>.Load("ADVENTURE_HERO_POWER", format);
			},
			delegate()
			{
				GameDbf.AdventureLoadoutTreasures = Dbf<AdventureLoadoutTreasuresDbfRecord>.Load("ADVENTURE_LOADOUT_TREASURES", format);
			},
			delegate()
			{
				GameDbf.AdventureMission = Dbf<AdventureMissionDbfRecord>.Load("ADVENTURE_MISSION", format);
			},
			delegate()
			{
				GameDbf.AdventureMode = Dbf<AdventureModeDbfRecord>.Load("ADVENTURE_MODE", format);
			},
			delegate()
			{
				GameDbf.Banner = Dbf<BannerDbfRecord>.Load("BANNER", format);
			},
			delegate()
			{
				GameDbf.BattlegroundsSeason = Dbf<BattlegroundsSeasonDbfRecord>.Load("BATTLEGROUNDS_SEASON", format);
			},
			delegate()
			{
				GameDbf.Board = Dbf<BoardDbfRecord>.Load("BOARD", format);
			},
			delegate()
			{
				GameDbf.Booster = Dbf<BoosterDbfRecord>.Load("BOOSTER", format);
			},
			delegate()
			{
				GameDbf.BoosterCardSet = Dbf<BoosterCardSetDbfRecord>.Load("BOOSTER_CARD_SET", format);
			},
			delegate()
			{
				GameDbf.Card = Dbf<CardDbfRecord>.Load("CARD", format);
			},
			delegate()
			{
				GameDbf.CardAdditonalSearchTerms = Dbf<CardAdditonalSearchTermsDbfRecord>.Load("CARD_ADDITONAL_SEARCH_TERMS", format);
			},
			delegate()
			{
				GameDbf.CardBack = Dbf<CardBackDbfRecord>.Load("CARD_BACK", format);
			},
			delegate()
			{
				GameDbf.CardChange = Dbf<CardChangeDbfRecord>.Load("CARD_CHANGE", format);
			},
			delegate()
			{
				GameDbf.CardDiscoverString = Dbf<CardDiscoverStringDbfRecord>.Load("CARD_DISCOVER_STRING", format);
			},
			delegate()
			{
				GameDbf.CardHero = Dbf<CardHeroDbfRecord>.Load("CARD_HERO", format);
			},
			delegate()
			{
				GameDbf.CardPlayerDeckOverride = Dbf<CardPlayerDeckOverrideDbfRecord>.Load("CARD_PLAYER_DECK_OVERRIDE", format);
			},
			delegate()
			{
				GameDbf.CardSet = Dbf<CardSetDbfRecord>.Load("CARD_SET", format);
			},
			delegate()
			{
				GameDbf.CardSetSpellOverride = Dbf<CardSetSpellOverrideDbfRecord>.Load("CARD_SET_SPELL_OVERRIDE", format);
			},
			delegate()
			{
				GameDbf.CardSetTiming = Dbf<CardSetTimingDbfRecord>.Load("CARD_SET_TIMING", format);
			},
			delegate()
			{
				GameDbf.CardTag = Dbf<CardTagDbfRecord>.Load("CARD_TAG", format);
			},
			delegate()
			{
				GameDbf.CharacterDialog = Dbf<CharacterDialogDbfRecord>.Load("CHARACTER_DIALOG", format);
			},
			delegate()
			{
				GameDbf.CharacterDialogItems = Dbf<CharacterDialogItemsDbfRecord>.Load("CHARACTER_DIALOG_ITEMS", format);
			},
			delegate()
			{
				GameDbf.Class = Dbf<ClassDbfRecord>.Load("CLASS", format);
			},
			delegate()
			{
				GameDbf.ClassExclusions = Dbf<ClassExclusionsDbfRecord>.Load("CLASS_EXCLUSIONS", format);
			},
			delegate()
			{
				GameDbf.ClientString = Dbf<ClientStringDbfRecord>.Load("CLIENT_STRING", format);
			},
			delegate()
			{
				GameDbf.Coin = Dbf<CoinDbfRecord>.Load("COIN", format);
			},
			delegate()
			{
				GameDbf.CreditsYear = Dbf<CreditsYearDbfRecord>.Load("CREDITS_YEAR", format);
			},
			delegate()
			{
				GameDbf.Deck = Dbf<DeckDbfRecord>.Load("DECK", format);
			},
			delegate()
			{
				GameDbf.DeckCard = Dbf<DeckCardDbfRecord>.Load("DECK_CARD", format);
			},
			delegate()
			{
				GameDbf.DeckRuleset = Dbf<DeckRulesetDbfRecord>.Load("DECK_RULESET", format);
			},
			delegate()
			{
				GameDbf.DeckRulesetRule = Dbf<DeckRulesetRuleDbfRecord>.Load("DECK_RULESET_RULE", format);
			},
			delegate()
			{
				GameDbf.DeckRulesetRuleSubset = Dbf<DeckRulesetRuleSubsetDbfRecord>.Load("DECK_RULESET_RULE_SUBSET", format);
			},
			delegate()
			{
				GameDbf.DeckTemplate = Dbf<DeckTemplateDbfRecord>.Load("DECK_TEMPLATE", format);
			},
			delegate()
			{
				GameDbf.DraftContent = Dbf<DraftContentDbfRecord>.Load("DRAFT_CONTENT", format);
			},
			delegate()
			{
				GameDbf.ExternalUrl = Dbf<ExternalUrlDbfRecord>.Load("EXTERNAL_URL", format);
			},
			delegate()
			{
				GameDbf.FixedReward = Dbf<FixedRewardDbfRecord>.Load("FIXED_REWARD", format);
			},
			delegate()
			{
				GameDbf.FixedRewardAction = Dbf<FixedRewardActionDbfRecord>.Load("FIXED_REWARD_ACTION", format);
			},
			delegate()
			{
				GameDbf.FixedRewardMap = Dbf<FixedRewardMapDbfRecord>.Load("FIXED_REWARD_MAP", format);
			},
			delegate()
			{
				GameDbf.GameMode = Dbf<GameModeDbfRecord>.Load("GAME_MODE", format);
			},
			delegate()
			{
				GameDbf.GameSaveSubkey = Dbf<GameSaveSubkeyDbfRecord>.Load("GAME_SAVE_SUBKEY", format);
			},
			delegate()
			{
				GameDbf.Global = Dbf<GlobalDbfRecord>.Load("GLOBAL", format);
			},
			delegate()
			{
				GameDbf.GuestHero = Dbf<GuestHeroDbfRecord>.Load("GUEST_HERO", format);
			},
			delegate()
			{
				GameDbf.GuestHeroSelectionRatio = Dbf<GuestHeroSelectionRatioDbfRecord>.Load("GUEST_HERO_SELECTION_RATIO", format);
			},
			delegate()
			{
				GameDbf.HiddenLicense = Dbf<HiddenLicenseDbfRecord>.Load("HIDDEN_LICENSE", format);
			},
			delegate()
			{
				GameDbf.KeywordText = Dbf<KeywordTextDbfRecord>.Load("KEYWORD_TEXT", format);
			},
			delegate()
			{
				GameDbf.League = Dbf<LeagueDbfRecord>.Load("LEAGUE", format);
			},
			delegate()
			{
				GameDbf.LeagueGameType = Dbf<LeagueGameTypeDbfRecord>.Load("LEAGUE_GAME_TYPE", format);
			},
			delegate()
			{
				GameDbf.LeagueRank = Dbf<LeagueRankDbfRecord>.Load("LEAGUE_RANK", format);
			},
			delegate()
			{
				GameDbf.LoginPopupSequence = Dbf<LoginPopupSequenceDbfRecord>.Load("LOGIN_POPUP_SEQUENCE", format);
			},
			delegate()
			{
				GameDbf.LoginPopupSequencePopup = Dbf<LoginPopupSequencePopupDbfRecord>.Load("LOGIN_POPUP_SEQUENCE_POPUP", format);
			},
			delegate()
			{
				GameDbf.LoginReward = Dbf<LoginRewardDbfRecord>.Load("LOGIN_REWARD", format);
			},
			delegate()
			{
				GameDbf.MiniSet = Dbf<MiniSetDbfRecord>.Load("MINI_SET", format);
			},
			delegate()
			{
				GameDbf.ModularBundle = Dbf<ModularBundleDbfRecord>.Load("MODULAR_BUNDLE", format);
			},
			delegate()
			{
				GameDbf.ModularBundleLayout = Dbf<ModularBundleLayoutDbfRecord>.Load("MODULAR_BUNDLE_LAYOUT", format);
			},
			delegate()
			{
				GameDbf.ModularBundleLayoutNode = Dbf<ModularBundleLayoutNodeDbfRecord>.Load("MODULAR_BUNDLE_LAYOUT_NODE", format);
			},
			delegate()
			{
				GameDbf.MultiClassGroup = Dbf<MultiClassGroupDbfRecord>.Load("MULTI_CLASS_GROUP", format);
			},
			delegate()
			{
				GameDbf.Product = Dbf<ProductDbfRecord>.Load("PRODUCT", format);
			},
			delegate()
			{
				GameDbf.ProductClientData = Dbf<ProductClientDataDbfRecord>.Load("PRODUCT_CLIENT_DATA", format);
			},
			delegate()
			{
				GameDbf.PvpdrSeason = Dbf<PvpdrSeasonDbfRecord>.Load("PVPDR_SEASON", format);
			},
			delegate()
			{
				GameDbf.Quest = Dbf<QuestDbfRecord>.Load("QUEST", format);
			},
			delegate()
			{
				GameDbf.QuestDialog = Dbf<QuestDialogDbfRecord>.Load("QUEST_DIALOG", format);
			},
			delegate()
			{
				GameDbf.QuestDialogOnComplete = Dbf<QuestDialogOnCompleteDbfRecord>.Load("QUEST_DIALOG_ON_COMPLETE", format);
			},
			delegate()
			{
				GameDbf.QuestDialogOnProgress1 = Dbf<QuestDialogOnProgress1DbfRecord>.Load("QUEST_DIALOG_ON_PROGRESS1", format);
			},
			delegate()
			{
				GameDbf.QuestDialogOnProgress2 = Dbf<QuestDialogOnProgress2DbfRecord>.Load("QUEST_DIALOG_ON_PROGRESS2", format);
			},
			delegate()
			{
				GameDbf.QuestDialogOnReceived = Dbf<QuestDialogOnReceivedDbfRecord>.Load("QUEST_DIALOG_ON_RECEIVED", format);
			},
			delegate()
			{
				GameDbf.QuestModifier = Dbf<QuestModifierDbfRecord>.Load("QUEST_MODIFIER", format);
			},
			delegate()
			{
				GameDbf.QuestPool = Dbf<QuestPoolDbfRecord>.Load("QUEST_POOL", format);
			},
			delegate()
			{
				GameDbf.RegionOverrides = Dbf<RegionOverridesDbfRecord>.Load("REGION_OVERRIDES", format);
			},
			delegate()
			{
				GameDbf.RewardBag = Dbf<RewardBagDbfRecord>.Load("REWARD_BAG", format);
			},
			delegate()
			{
				GameDbf.RewardChest = Dbf<RewardChestDbfRecord>.Load("REWARD_CHEST", format);
			},
			delegate()
			{
				GameDbf.RewardChestContents = Dbf<RewardChestContentsDbfRecord>.Load("REWARD_CHEST_CONTENTS", format);
			},
			delegate()
			{
				GameDbf.RewardItem = Dbf<RewardItemDbfRecord>.Load("REWARD_ITEM", format);
			},
			delegate()
			{
				GameDbf.RewardList = Dbf<RewardListDbfRecord>.Load("REWARD_LIST", format);
			},
			delegate()
			{
				GameDbf.RewardTrack = Dbf<RewardTrackDbfRecord>.Load("REWARD_TRACK", format);
			},
			delegate()
			{
				GameDbf.RewardTrackLevel = Dbf<RewardTrackLevelDbfRecord>.Load("REWARD_TRACK_LEVEL", format);
			},
			delegate()
			{
				GameDbf.Scenario = Dbf<ScenarioDbfRecord>.Load("SCENARIO", format);
			},
			delegate()
			{
				GameDbf.ScenarioGuestHeroes = Dbf<ScenarioGuestHeroesDbfRecord>.Load("SCENARIO_GUEST_HEROES", format);
			},
			delegate()
			{
				GameDbf.ScheduledCharacterDialog = Dbf<ScheduledCharacterDialogDbfRecord>.Load("SCHEDULED_CHARACTER_DIALOG", format);
			},
			delegate()
			{
				GameDbf.ScoreLabel = Dbf<ScoreLabelDbfRecord>.Load("SCORE_LABEL", format);
			},
			delegate()
			{
				GameDbf.SellableDeck = Dbf<SellableDeckDbfRecord>.Load("SELLABLE_DECK", format);
			},
			delegate()
			{
				GameDbf.ShopTier = Dbf<ShopTierDbfRecord>.Load("SHOP_TIER", format);
			},
			delegate()
			{
				GameDbf.ShopTierProductSale = Dbf<ShopTierProductSaleDbfRecord>.Load("SHOP_TIER_PRODUCT_SALE", format);
			},
			delegate()
			{
				GameDbf.Subset = Dbf<SubsetDbfRecord>.Load("SUBSET", format);
			},
			delegate()
			{
				GameDbf.SubsetCard = Dbf<SubsetCardDbfRecord>.Load("SUBSET_CARD", format);
			},
			delegate()
			{
				GameDbf.SubsetRule = Dbf<SubsetRuleDbfRecord>.Load("SUBSET_RULE", format);
			},
			delegate()
			{
				GameDbf.TavernBrawlTicket = Dbf<TavernBrawlTicketDbfRecord>.Load("TAVERN_BRAWL_TICKET", format);
			},
			delegate()
			{
				GameDbf.Trigger = Dbf<TriggerDbfRecord>.Load("TRIGGER", format);
			},
			delegate()
			{
				GameDbf.VisualBlacklist = Dbf<VisualBlacklistDbfRecord>.Load("VISUAL_BLACKLIST", format);
			},
			delegate()
			{
				GameDbf.Wing = Dbf<WingDbfRecord>.Load("WING", format);
			},
			delegate()
			{
				GameDbf.XpPerGameType = Dbf<XpPerGameTypeDbfRecord>.Load("XP_PER_GAME_TYPE", format);
			}
		};
	}

	// Token: 0x06001B7D RID: 7037 RVA: 0x0008F840 File Offset: 0x0008DA40
	private static JobResultCollection GetLoadDbfJobs(DbfFormat format)
	{
		return new JobResultCollection(new IAsyncJobResult[]
		{
			Dbf<AccountLicenseDbfRecord>.CreateLoadAsyncJob("ACCOUNT_LICENSE", format, ref GameDbf.AccountLicense),
			Dbf<AchieveDbfRecord>.CreateLoadAsyncJob("ACHIEVE", format, ref GameDbf.Achieve),
			Dbf<AchieveConditionDbfRecord>.CreateLoadAsyncJob("ACHIEVE_CONDITION", format, ref GameDbf.AchieveCondition),
			Dbf<AchieveRegionDataDbfRecord>.CreateLoadAsyncJob("ACHIEVE_REGION_DATA", format, ref GameDbf.AchieveRegionData),
			Dbf<AchievementDbfRecord>.CreateLoadAsyncJob("ACHIEVEMENT", format, ref GameDbf.Achievement),
			Dbf<AchievementCategoryDbfRecord>.CreateLoadAsyncJob("ACHIEVEMENT_CATEGORY", format, ref GameDbf.AchievementCategory),
			Dbf<AchievementSectionDbfRecord>.CreateLoadAsyncJob("ACHIEVEMENT_SECTION", format, ref GameDbf.AchievementSection),
			Dbf<AchievementSectionItemDbfRecord>.CreateLoadAsyncJob("ACHIEVEMENT_SECTION_ITEM", format, ref GameDbf.AchievementSectionItem),
			Dbf<AchievementSubcategoryDbfRecord>.CreateLoadAsyncJob("ACHIEVEMENT_SUBCATEGORY", format, ref GameDbf.AchievementSubcategory),
			Dbf<AdventureDbfRecord>.CreateLoadAsyncJob("ADVENTURE", format, ref GameDbf.Adventure),
			Dbf<AdventureDataDbfRecord>.CreateLoadAsyncJob("ADVENTURE_DATA", format, ref GameDbf.AdventureData),
			Dbf<AdventureDeckDbfRecord>.CreateLoadAsyncJob("ADVENTURE_DECK", format, ref GameDbf.AdventureDeck),
			Dbf<AdventureGuestHeroesDbfRecord>.CreateLoadAsyncJob("ADVENTURE_GUEST_HEROES", format, ref GameDbf.AdventureGuestHeroes),
			Dbf<AdventureHeroPowerDbfRecord>.CreateLoadAsyncJob("ADVENTURE_HERO_POWER", format, ref GameDbf.AdventureHeroPower),
			Dbf<AdventureLoadoutTreasuresDbfRecord>.CreateLoadAsyncJob("ADVENTURE_LOADOUT_TREASURES", format, ref GameDbf.AdventureLoadoutTreasures),
			Dbf<AdventureMissionDbfRecord>.CreateLoadAsyncJob("ADVENTURE_MISSION", format, ref GameDbf.AdventureMission),
			Dbf<AdventureModeDbfRecord>.CreateLoadAsyncJob("ADVENTURE_MODE", format, ref GameDbf.AdventureMode),
			Dbf<BannerDbfRecord>.CreateLoadAsyncJob("BANNER", format, ref GameDbf.Banner),
			Dbf<BattlegroundsSeasonDbfRecord>.CreateLoadAsyncJob("BATTLEGROUNDS_SEASON", format, ref GameDbf.BattlegroundsSeason),
			Dbf<BoardDbfRecord>.CreateLoadAsyncJob("BOARD", format, ref GameDbf.Board),
			Dbf<BoosterDbfRecord>.CreateLoadAsyncJob("BOOSTER", format, ref GameDbf.Booster),
			Dbf<BoosterCardSetDbfRecord>.CreateLoadAsyncJob("BOOSTER_CARD_SET", format, ref GameDbf.BoosterCardSet),
			Dbf<CardDbfRecord>.CreateLoadAsyncJob("CARD", format, ref GameDbf.Card),
			Dbf<CardAdditonalSearchTermsDbfRecord>.CreateLoadAsyncJob("CARD_ADDITONAL_SEARCH_TERMS", format, ref GameDbf.CardAdditonalSearchTerms),
			Dbf<CardBackDbfRecord>.CreateLoadAsyncJob("CARD_BACK", format, ref GameDbf.CardBack),
			Dbf<CardChangeDbfRecord>.CreateLoadAsyncJob("CARD_CHANGE", format, ref GameDbf.CardChange),
			Dbf<CardDiscoverStringDbfRecord>.CreateLoadAsyncJob("CARD_DISCOVER_STRING", format, ref GameDbf.CardDiscoverString),
			Dbf<CardHeroDbfRecord>.CreateLoadAsyncJob("CARD_HERO", format, ref GameDbf.CardHero),
			Dbf<CardPlayerDeckOverrideDbfRecord>.CreateLoadAsyncJob("CARD_PLAYER_DECK_OVERRIDE", format, ref GameDbf.CardPlayerDeckOverride),
			Dbf<CardSetDbfRecord>.CreateLoadAsyncJob("CARD_SET", format, ref GameDbf.CardSet),
			Dbf<CardSetSpellOverrideDbfRecord>.CreateLoadAsyncJob("CARD_SET_SPELL_OVERRIDE", format, ref GameDbf.CardSetSpellOverride),
			Dbf<CardSetTimingDbfRecord>.CreateLoadAsyncJob("CARD_SET_TIMING", format, ref GameDbf.CardSetTiming),
			Dbf<CardTagDbfRecord>.CreateLoadAsyncJob("CARD_TAG", format, ref GameDbf.CardTag),
			Dbf<CharacterDialogDbfRecord>.CreateLoadAsyncJob("CHARACTER_DIALOG", format, ref GameDbf.CharacterDialog),
			Dbf<CharacterDialogItemsDbfRecord>.CreateLoadAsyncJob("CHARACTER_DIALOG_ITEMS", format, ref GameDbf.CharacterDialogItems),
			Dbf<ClassDbfRecord>.CreateLoadAsyncJob("CLASS", format, ref GameDbf.Class),
			Dbf<ClassExclusionsDbfRecord>.CreateLoadAsyncJob("CLASS_EXCLUSIONS", format, ref GameDbf.ClassExclusions),
			Dbf<ClientStringDbfRecord>.CreateLoadAsyncJob("CLIENT_STRING", format, ref GameDbf.ClientString),
			Dbf<CoinDbfRecord>.CreateLoadAsyncJob("COIN", format, ref GameDbf.Coin),
			Dbf<CreditsYearDbfRecord>.CreateLoadAsyncJob("CREDITS_YEAR", format, ref GameDbf.CreditsYear),
			Dbf<DeckDbfRecord>.CreateLoadAsyncJob("DECK", format, ref GameDbf.Deck),
			Dbf<DeckCardDbfRecord>.CreateLoadAsyncJob("DECK_CARD", format, ref GameDbf.DeckCard),
			Dbf<DeckRulesetDbfRecord>.CreateLoadAsyncJob("DECK_RULESET", format, ref GameDbf.DeckRuleset),
			Dbf<DeckRulesetRuleDbfRecord>.CreateLoadAsyncJob("DECK_RULESET_RULE", format, ref GameDbf.DeckRulesetRule),
			Dbf<DeckRulesetRuleSubsetDbfRecord>.CreateLoadAsyncJob("DECK_RULESET_RULE_SUBSET", format, ref GameDbf.DeckRulesetRuleSubset),
			Dbf<DeckTemplateDbfRecord>.CreateLoadAsyncJob("DECK_TEMPLATE", format, ref GameDbf.DeckTemplate),
			Dbf<DraftContentDbfRecord>.CreateLoadAsyncJob("DRAFT_CONTENT", format, ref GameDbf.DraftContent),
			Dbf<ExternalUrlDbfRecord>.CreateLoadAsyncJob("EXTERNAL_URL", format, ref GameDbf.ExternalUrl),
			Dbf<FixedRewardDbfRecord>.CreateLoadAsyncJob("FIXED_REWARD", format, ref GameDbf.FixedReward),
			Dbf<FixedRewardActionDbfRecord>.CreateLoadAsyncJob("FIXED_REWARD_ACTION", format, ref GameDbf.FixedRewardAction),
			Dbf<FixedRewardMapDbfRecord>.CreateLoadAsyncJob("FIXED_REWARD_MAP", format, ref GameDbf.FixedRewardMap),
			Dbf<GameModeDbfRecord>.CreateLoadAsyncJob("GAME_MODE", format, ref GameDbf.GameMode),
			Dbf<GameSaveSubkeyDbfRecord>.CreateLoadAsyncJob("GAME_SAVE_SUBKEY", format, ref GameDbf.GameSaveSubkey),
			Dbf<GlobalDbfRecord>.CreateLoadAsyncJob("GLOBAL", format, ref GameDbf.Global),
			Dbf<GuestHeroDbfRecord>.CreateLoadAsyncJob("GUEST_HERO", format, ref GameDbf.GuestHero),
			Dbf<GuestHeroSelectionRatioDbfRecord>.CreateLoadAsyncJob("GUEST_HERO_SELECTION_RATIO", format, ref GameDbf.GuestHeroSelectionRatio),
			Dbf<HiddenLicenseDbfRecord>.CreateLoadAsyncJob("HIDDEN_LICENSE", format, ref GameDbf.HiddenLicense),
			Dbf<KeywordTextDbfRecord>.CreateLoadAsyncJob("KEYWORD_TEXT", format, ref GameDbf.KeywordText),
			Dbf<LeagueDbfRecord>.CreateLoadAsyncJob("LEAGUE", format, ref GameDbf.League),
			Dbf<LeagueGameTypeDbfRecord>.CreateLoadAsyncJob("LEAGUE_GAME_TYPE", format, ref GameDbf.LeagueGameType),
			Dbf<LeagueRankDbfRecord>.CreateLoadAsyncJob("LEAGUE_RANK", format, ref GameDbf.LeagueRank),
			Dbf<LoginPopupSequenceDbfRecord>.CreateLoadAsyncJob("LOGIN_POPUP_SEQUENCE", format, ref GameDbf.LoginPopupSequence),
			Dbf<LoginPopupSequencePopupDbfRecord>.CreateLoadAsyncJob("LOGIN_POPUP_SEQUENCE_POPUP", format, ref GameDbf.LoginPopupSequencePopup),
			Dbf<LoginRewardDbfRecord>.CreateLoadAsyncJob("LOGIN_REWARD", format, ref GameDbf.LoginReward),
			Dbf<MiniSetDbfRecord>.CreateLoadAsyncJob("MINI_SET", format, ref GameDbf.MiniSet),
			Dbf<ModularBundleDbfRecord>.CreateLoadAsyncJob("MODULAR_BUNDLE", format, ref GameDbf.ModularBundle),
			Dbf<ModularBundleLayoutDbfRecord>.CreateLoadAsyncJob("MODULAR_BUNDLE_LAYOUT", format, ref GameDbf.ModularBundleLayout),
			Dbf<ModularBundleLayoutNodeDbfRecord>.CreateLoadAsyncJob("MODULAR_BUNDLE_LAYOUT_NODE", format, ref GameDbf.ModularBundleLayoutNode),
			Dbf<MultiClassGroupDbfRecord>.CreateLoadAsyncJob("MULTI_CLASS_GROUP", format, ref GameDbf.MultiClassGroup),
			Dbf<ProductDbfRecord>.CreateLoadAsyncJob("PRODUCT", format, ref GameDbf.Product),
			Dbf<ProductClientDataDbfRecord>.CreateLoadAsyncJob("PRODUCT_CLIENT_DATA", format, ref GameDbf.ProductClientData),
			Dbf<PvpdrSeasonDbfRecord>.CreateLoadAsyncJob("PVPDR_SEASON", format, ref GameDbf.PvpdrSeason),
			Dbf<QuestDbfRecord>.CreateLoadAsyncJob("QUEST", format, ref GameDbf.Quest),
			Dbf<QuestDialogDbfRecord>.CreateLoadAsyncJob("QUEST_DIALOG", format, ref GameDbf.QuestDialog),
			Dbf<QuestDialogOnCompleteDbfRecord>.CreateLoadAsyncJob("QUEST_DIALOG_ON_COMPLETE", format, ref GameDbf.QuestDialogOnComplete),
			Dbf<QuestDialogOnProgress1DbfRecord>.CreateLoadAsyncJob("QUEST_DIALOG_ON_PROGRESS1", format, ref GameDbf.QuestDialogOnProgress1),
			Dbf<QuestDialogOnProgress2DbfRecord>.CreateLoadAsyncJob("QUEST_DIALOG_ON_PROGRESS2", format, ref GameDbf.QuestDialogOnProgress2),
			Dbf<QuestDialogOnReceivedDbfRecord>.CreateLoadAsyncJob("QUEST_DIALOG_ON_RECEIVED", format, ref GameDbf.QuestDialogOnReceived),
			Dbf<QuestModifierDbfRecord>.CreateLoadAsyncJob("QUEST_MODIFIER", format, ref GameDbf.QuestModifier),
			Dbf<QuestPoolDbfRecord>.CreateLoadAsyncJob("QUEST_POOL", format, ref GameDbf.QuestPool),
			Dbf<RegionOverridesDbfRecord>.CreateLoadAsyncJob("REGION_OVERRIDES", format, ref GameDbf.RegionOverrides),
			Dbf<RewardBagDbfRecord>.CreateLoadAsyncJob("REWARD_BAG", format, ref GameDbf.RewardBag),
			Dbf<RewardChestDbfRecord>.CreateLoadAsyncJob("REWARD_CHEST", format, ref GameDbf.RewardChest),
			Dbf<RewardChestContentsDbfRecord>.CreateLoadAsyncJob("REWARD_CHEST_CONTENTS", format, ref GameDbf.RewardChestContents),
			Dbf<RewardItemDbfRecord>.CreateLoadAsyncJob("REWARD_ITEM", format, ref GameDbf.RewardItem),
			Dbf<RewardListDbfRecord>.CreateLoadAsyncJob("REWARD_LIST", format, ref GameDbf.RewardList),
			Dbf<RewardTrackDbfRecord>.CreateLoadAsyncJob("REWARD_TRACK", format, ref GameDbf.RewardTrack),
			Dbf<RewardTrackLevelDbfRecord>.CreateLoadAsyncJob("REWARD_TRACK_LEVEL", format, ref GameDbf.RewardTrackLevel),
			Dbf<ScenarioDbfRecord>.CreateLoadAsyncJob("SCENARIO", format, ref GameDbf.Scenario),
			Dbf<ScenarioGuestHeroesDbfRecord>.CreateLoadAsyncJob("SCENARIO_GUEST_HEROES", format, ref GameDbf.ScenarioGuestHeroes),
			Dbf<ScheduledCharacterDialogDbfRecord>.CreateLoadAsyncJob("SCHEDULED_CHARACTER_DIALOG", format, ref GameDbf.ScheduledCharacterDialog),
			Dbf<ScoreLabelDbfRecord>.CreateLoadAsyncJob("SCORE_LABEL", format, ref GameDbf.ScoreLabel),
			Dbf<SellableDeckDbfRecord>.CreateLoadAsyncJob("SELLABLE_DECK", format, ref GameDbf.SellableDeck),
			Dbf<ShopTierDbfRecord>.CreateLoadAsyncJob("SHOP_TIER", format, ref GameDbf.ShopTier),
			Dbf<ShopTierProductSaleDbfRecord>.CreateLoadAsyncJob("SHOP_TIER_PRODUCT_SALE", format, ref GameDbf.ShopTierProductSale),
			Dbf<SubsetDbfRecord>.CreateLoadAsyncJob("SUBSET", format, ref GameDbf.Subset),
			Dbf<SubsetCardDbfRecord>.CreateLoadAsyncJob("SUBSET_CARD", format, ref GameDbf.SubsetCard),
			Dbf<SubsetRuleDbfRecord>.CreateLoadAsyncJob("SUBSET_RULE", format, ref GameDbf.SubsetRule),
			Dbf<TavernBrawlTicketDbfRecord>.CreateLoadAsyncJob("TAVERN_BRAWL_TICKET", format, ref GameDbf.TavernBrawlTicket),
			Dbf<TriggerDbfRecord>.CreateLoadAsyncJob("TRIGGER", format, ref GameDbf.Trigger),
			Dbf<VisualBlacklistDbfRecord>.CreateLoadAsyncJob("VISUAL_BLACKLIST", format, ref GameDbf.VisualBlacklist),
			Dbf<WingDbfRecord>.CreateLoadAsyncJob("WING", format, ref GameDbf.Wing),
			Dbf<XpPerGameTypeDbfRecord>.CreateLoadAsyncJob("XP_PER_GAME_TYPE", format, ref GameDbf.XpPerGameType)
		});
	}

	// Token: 0x06001B7E RID: 7038 RVA: 0x0009005C File Offset: 0x0008E25C
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield return GameDbf.CreateLoadDbfJob();
		yield break;
	}

	// Token: 0x06001B7F RID: 7039 RVA: 0x00090064 File Offset: 0x0008E264
	public Type[] GetDependencies()
	{
		return null;
	}

	// Token: 0x06001B80 RID: 7040 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06001B81 RID: 7041 RVA: 0x00090067 File Offset: 0x0008E267
	public static GameDbfIndex GetIndex()
	{
		if (GameDbf.s_index == null)
		{
			GameDbf.s_index = new GameDbfIndex();
		}
		return GameDbf.s_index;
	}

	// Token: 0x06001B82 RID: 7042 RVA: 0x00090080 File Offset: 0x0008E280
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

	// Token: 0x06001B83 RID: 7043 RVA: 0x000900BE File Offset: 0x0008E2BE
	public static JobDefinition CreateLoadDbfJob()
	{
		return new JobDefinition("GameDbf.Load", GameDbf.Load(), Array.Empty<IJobDependency>());
	}

	// Token: 0x06001B84 RID: 7044 RVA: 0x000900D4 File Offset: 0x0008E2D4
	public static void LoadXml()
	{
		IEnumerator enumerator = GameDbf.Load(true, false);
		while (enumerator.MoveNext())
		{
		}
	}

	// Token: 0x06001B85 RID: 7045 RVA: 0x000900F1 File Offset: 0x0008E2F1
	public static IEnumerator<IAsyncJobResult> Load()
	{
		return GameDbf.Load(false, true);
	}

	// Token: 0x06001B86 RID: 7046 RVA: 0x000900FA File Offset: 0x0008E2FA
	public static IEnumerator<IAsyncJobResult> Load(bool useXmlLoading)
	{
		return GameDbf.Load(useXmlLoading, true);
	}

	// Token: 0x06001B87 RID: 7047 RVA: 0x00090103 File Offset: 0x0008E303
	public static IEnumerator<IAsyncJobResult> Load(bool useXmlLoading, bool useAssetJobs)
	{
		if (HearthstoneApplication.IsHearthstoneRunning)
		{
			yield return new WaitForGameDownloadManagerState();
		}
		if (GameDbf.s_index == null)
		{
			GameDbf.s_index = new GameDbfIndex();
		}
		else
		{
			GameDbf.s_index.Initialize();
		}
		if (GameDbf.ShouldForceXmlLoading())
		{
			useXmlLoading = true;
		}
		DbfFormat format = useXmlLoading ? DbfFormat.XML : DbfFormat.ASSET;
		DbfShared.Reset();
		if (!useXmlLoading)
		{
			if (useAssetJobs)
			{
				yield return new JobDefinition("GameDbf.LoadDBFSharedAssetBundle", DbfShared.Job_LoadSharedDBFAssetBundle(), Array.Empty<IJobDependency>());
			}
			else
			{
				DbfShared.LoadSharedAssetBundle();
			}
		}
		Log.Dbf.Print("Loading DBFS with format={0}", new object[]
		{
			format
		});
		Action[] loadActions = null;
		CPUTimeSoftYield softYielder = new CPUTimeSoftYield(1f / (float)Application.targetFrameRate * 0.8f);
		if (!useAssetJobs)
		{
			loadActions = GameDbf.GetLoadDbfActions(format);
			Action[] array = loadActions;
			for (int i = 0; i < array.Length; i++)
			{
				array[i]();
				if (softYielder.ShouldSoftYield())
				{
					yield return null;
					softYielder.NewFrame();
				}
			}
			array = null;
		}
		else
		{
			JobResultCollection loadDbfJobs = GameDbf.GetLoadDbfJobs(format);
			yield return loadDbfJobs;
		}
		loadActions = GameDbf.GetPostProcessDbfActions();
		int num;
		for (int i = 0; i < loadActions.Length; i = num)
		{
			loadActions[i]();
			if (softYielder.ShouldSoftYield())
			{
				yield return null;
				softYielder.NewFrame();
			}
			num = i + 1;
		}
		GameDbf.IsLoaded = true;
		GameDbf.SetDbfCallbacksForIndexing();
		yield break;
	}

	// Token: 0x06001B88 RID: 7048 RVA: 0x0009011C File Offset: 0x0008E31C
	private static Action[] GetPostProcessDbfActions()
	{
		Action[] array = new Action[12];
		array[0] = delegate()
		{
			GameDbf.s_index.PostProcessDbfLoad_CardTag(GameDbf.CardTag);
		};
		array[1] = delegate()
		{
			GameDbf.s_index.PostProcessDbfLoad_CardChange(GameDbf.CardChange);
		};
		array[2] = delegate()
		{
			GameDbf.s_index.PostProcessDbfLoad_Card(GameDbf.Card);
		};
		array[3] = delegate()
		{
			GameDbf.s_index.PostProcessDbfLoad_CardDiscoverString(GameDbf.CardDiscoverString);
		};
		array[4] = delegate()
		{
			GameDbf.s_index.PostProcessDbfLoad_CardSetSpellOverride(GameDbf.CardSetSpellOverride);
		};
		array[5] = delegate()
		{
			GameDbf.s_index.PostProcessDbfLoad_DeckRulesetRule(GameDbf.DeckRulesetRule);
		};
		array[6] = delegate()
		{
			GameDbf.s_index.PostProcessDbfLoad_DeckRulesetRuleSubset(GameDbf.DeckRulesetRuleSubset);
		};
		array[7] = delegate()
		{
			GameDbf.s_index.PostProcessDbfLoad_FixedRewardAction(GameDbf.FixedRewardAction);
		};
		array[8] = delegate()
		{
			GameDbf.s_index.PostProcessDbfLoad_FixedRewardMap(GameDbf.FixedRewardMap);
		};
		array[9] = delegate()
		{
			GameDbf.s_index.PostProcessDbfLoad_SubsetCard(GameDbf.SubsetCard);
		};
		array[10] = delegate()
		{
			GameDbf.s_index.PostProcessDbfLoad_CardPlayerDeckOverride(GameDbf.CardPlayerDeckOverride);
		};
		array[11] = delegate()
		{
			RankMgr.Get().PostProcessDbfLoad_League();
		};
		return array;
	}

	// Token: 0x06001B89 RID: 7049 RVA: 0x000902CC File Offset: 0x0008E4CC
	private static void SetDbfCallbacksForIndexing()
	{
		GameDbf.CardTag.AddListeners(new Dbf<CardTagDbfRecord>.RecordAddedListener(GameDbf.s_index.OnCardTagAdded), new Dbf<CardTagDbfRecord>.RecordsRemovedListener(GameDbf.s_index.OnCardTagRemoved));
		GameDbf.CardChange.AddListeners(new Dbf<CardChangeDbfRecord>.RecordAddedListener(GameDbf.s_index.OnCardChangeAdded), new Dbf<CardChangeDbfRecord>.RecordsRemovedListener(GameDbf.s_index.OnCardChangeRemoved));
		GameDbf.Card.AddListeners(new Dbf<CardDbfRecord>.RecordAddedListener(GameDbf.s_index.OnCardAdded), new Dbf<CardDbfRecord>.RecordsRemovedListener(GameDbf.s_index.OnCardRemoved));
		GameDbf.CardDiscoverString.AddListeners(new Dbf<CardDiscoverStringDbfRecord>.RecordAddedListener(GameDbf.s_index.OnCardDiscoverStringAdded), new Dbf<CardDiscoverStringDbfRecord>.RecordsRemovedListener(GameDbf.s_index.OnCardDiscoverStringRemoved));
		GameDbf.CardSetSpellOverride.AddListeners(new Dbf<CardSetSpellOverrideDbfRecord>.RecordAddedListener(GameDbf.s_index.OnCardSetSpellOverrideAdded), new Dbf<CardSetSpellOverrideDbfRecord>.RecordsRemovedListener(GameDbf.s_index.OnCardSetSpellOverrideRemoved));
		GameDbf.CardPlayerDeckOverride.AddListeners(new Dbf<CardPlayerDeckOverrideDbfRecord>.RecordAddedListener(GameDbf.s_index.OnCardPlayerDeckOverrideAdded), new Dbf<CardPlayerDeckOverrideDbfRecord>.RecordsRemovedListener(GameDbf.s_index.OnCardPlayerDeckOverrideRemoved));
		GameDbf.DeckRulesetRule.AddListeners(new Dbf<DeckRulesetRuleDbfRecord>.RecordAddedListener(GameDbf.s_index.OnDeckRulesetRuleAdded), new Dbf<DeckRulesetRuleDbfRecord>.RecordsRemovedListener(GameDbf.s_index.OnDeckRulesetRuleRemoved));
		GameDbf.DeckRulesetRuleSubset.AddListeners(new Dbf<DeckRulesetRuleSubsetDbfRecord>.RecordAddedListener(GameDbf.s_index.OnDeckRulesetRuleSubsetAdded), new Dbf<DeckRulesetRuleSubsetDbfRecord>.RecordsRemovedListener(GameDbf.s_index.OnDeckRulesetRuleSubsetRemoved));
		GameDbf.FixedRewardAction.AddListeners(new Dbf<FixedRewardActionDbfRecord>.RecordAddedListener(GameDbf.s_index.OnFixedRewardActionAdded), new Dbf<FixedRewardActionDbfRecord>.RecordsRemovedListener(GameDbf.s_index.OnFixedRewardActionRemoved));
		GameDbf.FixedRewardMap.AddListeners(new Dbf<FixedRewardMapDbfRecord>.RecordAddedListener(GameDbf.s_index.OnFixedRewardMapAdded), new Dbf<FixedRewardMapDbfRecord>.RecordsRemovedListener(GameDbf.s_index.OnFixedRewardMapRemoved));
		GameDbf.SubsetCard.AddListeners(new Dbf<SubsetCardDbfRecord>.RecordAddedListener(GameDbf.s_index.OnSubsetCardAdded), new Dbf<SubsetCardDbfRecord>.RecordsRemovedListener(GameDbf.s_index.OnSubsetCardRemoved));
	}

	// Token: 0x06001B8A RID: 7050 RVA: 0x000904A8 File Offset: 0x0008E6A8
	public static void Reload(string name, string xml)
	{
		if (!(name == "ACHIEVE"))
		{
			if (!(name == "CARD_BACK"))
			{
				Error.AddDevFatal("Reloading {0} is unsupported", new object[]
				{
					name
				});
			}
			else
			{
				GameDbf.CardBack = Dbf<CardBackDbfRecord>.Load(name, DbfFormat.XML);
				CardBackManager cardBackManager;
				if (HearthstoneServices.TryGet<CardBackManager>(out cardBackManager))
				{
					cardBackManager.InitCardBackData();
					return;
				}
			}
		}
		else
		{
			GameDbf.Achieve = Dbf<AchieveDbfRecord>.Load(name, DbfFormat.XML);
			AchieveManager achieveManager;
			if (HearthstoneServices.TryGet<AchieveManager>(out achieveManager))
			{
				achieveManager.InitAchieveManager();
				return;
			}
		}
	}

	// Token: 0x06001B8B RID: 7051 RVA: 0x0009051D File Offset: 0x0008E71D
	public static int GetDataVersion()
	{
		return GameDbf.GetDOPAsset().DataVersion;
	}

	// Token: 0x06001B8C RID: 7052 RVA: 0x0009052C File Offset: 0x0008E72C
	private static DOPAsset GetDOPAsset()
	{
		if (GameDbf.s_DOPAsset == null)
		{
			if (Application.isEditor && AssetLoaderPrefs.AssetLoadingMethod == AssetLoaderPrefs.ASSET_LOADING_METHOD.EDITOR_FILES)
			{
				GameDbf.s_DOPAsset = DOPAsset.GenerateDOPAsset();
			}
			else
			{
				AssetBundle assetBundle = DbfShared.GetAssetBundle();
				if (assetBundle != null)
				{
					GameDbf.s_DOPAsset = assetBundle.LoadAsset<DOPAsset>("Assets/Game/DOPAsset.asset");
				}
				if (GameDbf.s_DOPAsset == null)
				{
					Log.Dbf.PrintWarning("Failed to load DOP asset, generating default...", Array.Empty<object>());
					GameDbf.s_DOPAsset = DOPAsset.GenerateDOPAsset();
				}
			}
		}
		return GameDbf.s_DOPAsset;
	}

	// Token: 0x0400103F RID: 4159
	public static Dbf<AccountLicenseDbfRecord> AccountLicense;

	// Token: 0x04001040 RID: 4160
	public static Dbf<AchieveDbfRecord> Achieve;

	// Token: 0x04001041 RID: 4161
	public static Dbf<AchieveConditionDbfRecord> AchieveCondition;

	// Token: 0x04001042 RID: 4162
	public static Dbf<AchieveRegionDataDbfRecord> AchieveRegionData;

	// Token: 0x04001043 RID: 4163
	public static Dbf<AchievementDbfRecord> Achievement;

	// Token: 0x04001044 RID: 4164
	public static Dbf<AchievementCategoryDbfRecord> AchievementCategory;

	// Token: 0x04001045 RID: 4165
	public static Dbf<AchievementSectionDbfRecord> AchievementSection;

	// Token: 0x04001046 RID: 4166
	public static Dbf<AchievementSectionItemDbfRecord> AchievementSectionItem;

	// Token: 0x04001047 RID: 4167
	public static Dbf<AchievementSubcategoryDbfRecord> AchievementSubcategory;

	// Token: 0x04001048 RID: 4168
	public static Dbf<AdventureDbfRecord> Adventure;

	// Token: 0x04001049 RID: 4169
	public static Dbf<AdventureDataDbfRecord> AdventureData;

	// Token: 0x0400104A RID: 4170
	public static Dbf<AdventureDeckDbfRecord> AdventureDeck;

	// Token: 0x0400104B RID: 4171
	public static Dbf<AdventureGuestHeroesDbfRecord> AdventureGuestHeroes;

	// Token: 0x0400104C RID: 4172
	public static Dbf<AdventureHeroPowerDbfRecord> AdventureHeroPower;

	// Token: 0x0400104D RID: 4173
	public static Dbf<AdventureLoadoutTreasuresDbfRecord> AdventureLoadoutTreasures;

	// Token: 0x0400104E RID: 4174
	public static Dbf<AdventureMissionDbfRecord> AdventureMission;

	// Token: 0x0400104F RID: 4175
	public static Dbf<AdventureModeDbfRecord> AdventureMode;

	// Token: 0x04001050 RID: 4176
	public static Dbf<BannerDbfRecord> Banner;

	// Token: 0x04001051 RID: 4177
	public static Dbf<BattlegroundsSeasonDbfRecord> BattlegroundsSeason;

	// Token: 0x04001052 RID: 4178
	public static Dbf<BoardDbfRecord> Board;

	// Token: 0x04001053 RID: 4179
	public static Dbf<BoosterDbfRecord> Booster;

	// Token: 0x04001054 RID: 4180
	public static Dbf<BoosterCardSetDbfRecord> BoosterCardSet;

	// Token: 0x04001055 RID: 4181
	public static Dbf<CardDbfRecord> Card;

	// Token: 0x04001056 RID: 4182
	public static Dbf<CardAdditonalSearchTermsDbfRecord> CardAdditonalSearchTerms;

	// Token: 0x04001057 RID: 4183
	public static Dbf<CardBackDbfRecord> CardBack;

	// Token: 0x04001058 RID: 4184
	public static Dbf<CardChangeDbfRecord> CardChange;

	// Token: 0x04001059 RID: 4185
	public static Dbf<CardDiscoverStringDbfRecord> CardDiscoverString;

	// Token: 0x0400105A RID: 4186
	public static Dbf<CardHeroDbfRecord> CardHero;

	// Token: 0x0400105B RID: 4187
	public static Dbf<CardPlayerDeckOverrideDbfRecord> CardPlayerDeckOverride;

	// Token: 0x0400105C RID: 4188
	public static Dbf<CardSetDbfRecord> CardSet;

	// Token: 0x0400105D RID: 4189
	public static Dbf<CardSetSpellOverrideDbfRecord> CardSetSpellOverride;

	// Token: 0x0400105E RID: 4190
	public static Dbf<CardSetTimingDbfRecord> CardSetTiming;

	// Token: 0x0400105F RID: 4191
	public static Dbf<CardTagDbfRecord> CardTag;

	// Token: 0x04001060 RID: 4192
	public static Dbf<CharacterDialogDbfRecord> CharacterDialog;

	// Token: 0x04001061 RID: 4193
	public static Dbf<CharacterDialogItemsDbfRecord> CharacterDialogItems;

	// Token: 0x04001062 RID: 4194
	public static Dbf<ClassDbfRecord> Class;

	// Token: 0x04001063 RID: 4195
	public static Dbf<ClassExclusionsDbfRecord> ClassExclusions;

	// Token: 0x04001064 RID: 4196
	public static Dbf<ClientStringDbfRecord> ClientString;

	// Token: 0x04001065 RID: 4197
	public static Dbf<CoinDbfRecord> Coin;

	// Token: 0x04001066 RID: 4198
	public static Dbf<CreditsYearDbfRecord> CreditsYear;

	// Token: 0x04001067 RID: 4199
	public static Dbf<DeckDbfRecord> Deck;

	// Token: 0x04001068 RID: 4200
	public static Dbf<DeckCardDbfRecord> DeckCard;

	// Token: 0x04001069 RID: 4201
	public static Dbf<DeckRulesetDbfRecord> DeckRuleset;

	// Token: 0x0400106A RID: 4202
	public static Dbf<DeckRulesetRuleDbfRecord> DeckRulesetRule;

	// Token: 0x0400106B RID: 4203
	public static Dbf<DeckRulesetRuleSubsetDbfRecord> DeckRulesetRuleSubset;

	// Token: 0x0400106C RID: 4204
	public static Dbf<DeckTemplateDbfRecord> DeckTemplate;

	// Token: 0x0400106D RID: 4205
	public static Dbf<DraftContentDbfRecord> DraftContent;

	// Token: 0x0400106E RID: 4206
	public static Dbf<ExternalUrlDbfRecord> ExternalUrl;

	// Token: 0x0400106F RID: 4207
	public static Dbf<FixedRewardDbfRecord> FixedReward;

	// Token: 0x04001070 RID: 4208
	public static Dbf<FixedRewardActionDbfRecord> FixedRewardAction;

	// Token: 0x04001071 RID: 4209
	public static Dbf<FixedRewardMapDbfRecord> FixedRewardMap;

	// Token: 0x04001072 RID: 4210
	public static Dbf<GameModeDbfRecord> GameMode;

	// Token: 0x04001073 RID: 4211
	public static Dbf<GameSaveSubkeyDbfRecord> GameSaveSubkey;

	// Token: 0x04001074 RID: 4212
	public static Dbf<GlobalDbfRecord> Global;

	// Token: 0x04001075 RID: 4213
	public static Dbf<GuestHeroDbfRecord> GuestHero;

	// Token: 0x04001076 RID: 4214
	public static Dbf<GuestHeroSelectionRatioDbfRecord> GuestHeroSelectionRatio;

	// Token: 0x04001077 RID: 4215
	public static Dbf<HiddenLicenseDbfRecord> HiddenLicense;

	// Token: 0x04001078 RID: 4216
	public static Dbf<KeywordTextDbfRecord> KeywordText;

	// Token: 0x04001079 RID: 4217
	public static Dbf<LeagueDbfRecord> League;

	// Token: 0x0400107A RID: 4218
	public static Dbf<LeagueGameTypeDbfRecord> LeagueGameType;

	// Token: 0x0400107B RID: 4219
	public static Dbf<LeagueRankDbfRecord> LeagueRank;

	// Token: 0x0400107C RID: 4220
	public static Dbf<LoginPopupSequenceDbfRecord> LoginPopupSequence;

	// Token: 0x0400107D RID: 4221
	public static Dbf<LoginPopupSequencePopupDbfRecord> LoginPopupSequencePopup;

	// Token: 0x0400107E RID: 4222
	public static Dbf<LoginRewardDbfRecord> LoginReward;

	// Token: 0x0400107F RID: 4223
	public static Dbf<MiniSetDbfRecord> MiniSet;

	// Token: 0x04001080 RID: 4224
	public static Dbf<ModularBundleDbfRecord> ModularBundle;

	// Token: 0x04001081 RID: 4225
	public static Dbf<ModularBundleLayoutDbfRecord> ModularBundleLayout;

	// Token: 0x04001082 RID: 4226
	public static Dbf<ModularBundleLayoutNodeDbfRecord> ModularBundleLayoutNode;

	// Token: 0x04001083 RID: 4227
	public static Dbf<MultiClassGroupDbfRecord> MultiClassGroup;

	// Token: 0x04001084 RID: 4228
	public static Dbf<ProductDbfRecord> Product;

	// Token: 0x04001085 RID: 4229
	public static Dbf<ProductClientDataDbfRecord> ProductClientData;

	// Token: 0x04001086 RID: 4230
	public static Dbf<PvpdrSeasonDbfRecord> PvpdrSeason;

	// Token: 0x04001087 RID: 4231
	public static Dbf<QuestDbfRecord> Quest;

	// Token: 0x04001088 RID: 4232
	public static Dbf<QuestDialogDbfRecord> QuestDialog;

	// Token: 0x04001089 RID: 4233
	public static Dbf<QuestDialogOnCompleteDbfRecord> QuestDialogOnComplete;

	// Token: 0x0400108A RID: 4234
	public static Dbf<QuestDialogOnProgress1DbfRecord> QuestDialogOnProgress1;

	// Token: 0x0400108B RID: 4235
	public static Dbf<QuestDialogOnProgress2DbfRecord> QuestDialogOnProgress2;

	// Token: 0x0400108C RID: 4236
	public static Dbf<QuestDialogOnReceivedDbfRecord> QuestDialogOnReceived;

	// Token: 0x0400108D RID: 4237
	public static Dbf<QuestModifierDbfRecord> QuestModifier;

	// Token: 0x0400108E RID: 4238
	public static Dbf<QuestPoolDbfRecord> QuestPool;

	// Token: 0x0400108F RID: 4239
	public static Dbf<RegionOverridesDbfRecord> RegionOverrides;

	// Token: 0x04001090 RID: 4240
	public static Dbf<RewardBagDbfRecord> RewardBag;

	// Token: 0x04001091 RID: 4241
	public static Dbf<RewardChestDbfRecord> RewardChest;

	// Token: 0x04001092 RID: 4242
	public static Dbf<RewardChestContentsDbfRecord> RewardChestContents;

	// Token: 0x04001093 RID: 4243
	public static Dbf<RewardItemDbfRecord> RewardItem;

	// Token: 0x04001094 RID: 4244
	public static Dbf<RewardListDbfRecord> RewardList;

	// Token: 0x04001095 RID: 4245
	public static Dbf<RewardTrackDbfRecord> RewardTrack;

	// Token: 0x04001096 RID: 4246
	public static Dbf<RewardTrackLevelDbfRecord> RewardTrackLevel;

	// Token: 0x04001097 RID: 4247
	public static Dbf<ScenarioDbfRecord> Scenario;

	// Token: 0x04001098 RID: 4248
	public static Dbf<ScenarioGuestHeroesDbfRecord> ScenarioGuestHeroes;

	// Token: 0x04001099 RID: 4249
	public static Dbf<ScheduledCharacterDialogDbfRecord> ScheduledCharacterDialog;

	// Token: 0x0400109A RID: 4250
	public static Dbf<ScoreLabelDbfRecord> ScoreLabel;

	// Token: 0x0400109B RID: 4251
	public static Dbf<SellableDeckDbfRecord> SellableDeck;

	// Token: 0x0400109C RID: 4252
	public static Dbf<ShopTierDbfRecord> ShopTier;

	// Token: 0x0400109D RID: 4253
	public static Dbf<ShopTierProductSaleDbfRecord> ShopTierProductSale;

	// Token: 0x0400109E RID: 4254
	public static Dbf<SubsetDbfRecord> Subset;

	// Token: 0x0400109F RID: 4255
	public static Dbf<SubsetCardDbfRecord> SubsetCard;

	// Token: 0x040010A0 RID: 4256
	public static Dbf<SubsetRuleDbfRecord> SubsetRule;

	// Token: 0x040010A1 RID: 4257
	public static Dbf<TavernBrawlTicketDbfRecord> TavernBrawlTicket;

	// Token: 0x040010A2 RID: 4258
	public static Dbf<TriggerDbfRecord> Trigger;

	// Token: 0x040010A3 RID: 4259
	public static Dbf<VisualBlacklistDbfRecord> VisualBlacklist;

	// Token: 0x040010A4 RID: 4260
	public static Dbf<WingDbfRecord> Wing;

	// Token: 0x040010A5 RID: 4261
	public static Dbf<XpPerGameTypeDbfRecord> XpPerGameType;

	// Token: 0x040010A6 RID: 4262
	public static bool IsLoaded;

	// Token: 0x040010A7 RID: 4263
	private static GameDbfIndex s_index;

	// Token: 0x040010A8 RID: 4264
	private static DOPAsset s_DOPAsset;

	// Token: 0x040010A9 RID: 4265
	public const string kDOPAssetPath = "Assets/Game/DOPAsset.asset";
}
