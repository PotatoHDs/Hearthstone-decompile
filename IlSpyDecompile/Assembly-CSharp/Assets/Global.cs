using System;
using System.ComponentModel;

namespace Assets
{
	public static class Global
	{
		[Flags]
		public enum AssetFlags
		{
			[Description("none")]
			NONE = 0x0,
			[Description("dev_only")]
			DEV_ONLY = 0x1,
			[Description("not_packaged_in_client")]
			NOT_PACKAGED_IN_CLIENT = 0x2,
			[Description("force_do_not_localize")]
			FORCE_DO_NOT_LOCALIZE = 0x4
		}

		public enum PresenceStatus
		{
			UNKNOWN = -1,
			LOGIN,
			TUTORIAL_PREGAME,
			TUTORIAL_GAME,
			WELCOMEQUESTS,
			HUB,
			STORE,
			QUESTLOG,
			PACKOPENING,
			COLLECTION,
			DECKEDITOR,
			CRAFTING,
			PLAY_DECKPICKER,
			PLAY_QUEUE,
			PLAY_GAME,
			PRACTICE_DECKPICKER,
			PRACTICE_GAME,
			ARENA_PURCHASE,
			ARENA_FORGE,
			ARENA_IDLE,
			ARENA_QUEUE,
			ARENA_GAME,
			ARENA_REWARD,
			FRIENDLY_DECKPICKER,
			FRIENDLY_GAME,
			ADVENTURE_CHOOSING_MODE,
			ADVENTURE_SCENARIO_SELECT,
			ADVENTURE_SCENARIO_PLAYING_GAME,
			SPECTATING_GAME_TUTORIAL,
			SPECTATING_GAME_PRACTICE,
			SPECTATING_GAME_PLAY,
			SPECTATING_GAME_ARENA,
			SPECTATING_GAME_FRIENDLY,
			SPECTATING_GAME_ADVENTURE_NAXX_NORMAL,
			SPECTATING_GAME_ADVENTURE_NAXX_HEROIC,
			SPECTATING_GAME_ADVENTURE_NAXX_CLASS_CHALLENGE,
			SPECTATING_GAME_ADVENTURE_BRM_NORMAL,
			SPECTATING_GAME_ADVENTURE_BRM_HEROIC,
			SPECTATING_GAME_ADVENTURE_BRM_CLASS_CHALLENGE,
			TAVERN_BRAWL_SCREEN,
			TAVERN_BRAWL_DECKEDITOR,
			TAVERN_BRAWL_QUEUE,
			TAVERN_BRAWL_GAME,
			TAVERN_BRAWL_FRIENDLY_WAITING,
			TAVERN_BRAWL_FRIENDLY_GAME,
			SPECTATING_GAME_TAVERN_BRAWL,
			SPECTATING_GAME_ADVENTURE_LOE_NORMAL,
			SPECTATING_GAME_ADVENTURE_LOE_HEROIC,
			SPECTATING_GAME_ADVENTURE_LOE_CLASS_CHALLENGE,
			SPECTATING_GAME_ADVENTURE_KAR_NORMAL,
			SPECTATING_GAME_ADVENTURE_KAR_HEROIC,
			SPECTATING_GAME_ADVENTURE_KAR_CLASS_CHALLENGE,
			SPECTATING_GAME_RETURNING_PLAYER_CHALLENGE,
			FIRESIDE_BRAWL_SCREEN,
			SPECTATING_GAME_ADVENTURE_ICC_NORMAL,
			SPECTATING_GAME_ADVENTURE_LOOT,
			WAIT_FOR_OPPONENT_RECONNECT,
			SPECTATING_GAME_ADVENTURE_GIL,
			SPECTATING_GAME_ADVENTURE_GIL_BONUS_CHALLENGE,
			SPECTATING_GAME_ADVENTURE_BOT,
			SPECTATING_GAME_ADVENTURE_TRL,
			SPECTATING_GAME_ADVENTURE_DAL,
			SPECTATING_GAME_ADVENTURE_DAL_HEROIC,
			SPECTATING_GAME_ADVENTURE_ULD,
			SPECTATING_GAME_ADVENTURE_ULD_HEROIC,
			BATTLEGROUNDS_QUEUE,
			BATTLEGROUNDS_GAME,
			SPECTATING_GAME_BATTLEGROUNDS,
			BATTLEGROUNDS_SCREEN,
			SPECTATING_GAME_ADVENTURE_DRG,
			SPECTATING_GAME_ADVENTURE_DRG_HEROIC,
			SPECTATING_GAME_ADVENTURE_BTP,
			SPECTATING_GAME_ADVENTURE_BTA,
			SPECTATING_GAME_ADVENTURE_BTA_HEROIC,
			SPECTATING_GAME_ADVENTURE_BOH,
			DUELS_QUEUE,
			DUELS_GAME,
			DUELS_BUILDING_DECK,
			DUELS_IDLE,
			SPECTATING_GAME_DUELS,
			DUELS_REWARD,
			DUELS_PURCHASE,
			VIEWING_JOURNAL,
			SPECTATING_GAME_ADVENTURE_BOM,
			PLAY_RANKED_STANDARD,
			PLAY_RANKED_WILD,
			PLAY_RANKED_CLASSIC,
			PLAY_CASUAL_STANDARD,
			PLAY_CASUAL_WILD,
			PLAY_CASUAL_CLASSIC
		}

		public enum Region
		{
			[Description("region_unknown")]
			REGION_UNKNOWN = 0,
			[Description("region_us")]
			REGION_US = 1,
			[Description("region_eu")]
			REGION_EU = 2,
			[Description("region_kr")]
			REGION_KR = 3,
			[Description("region_tw")]
			REGION_TW = 4,
			[Description("region_cn")]
			REGION_CN = 5,
			[Description("region_sg")]
			REGION_SG = 6,
			[Description("region_ptr")]
			REGION_PTR = 98
		}

		public enum FormatType
		{
			[Description("ft_unknown")]
			FT_UNKNOWN,
			[Description("ft_wild")]
			FT_WILD,
			[Description("ft_standard")]
			FT_STANDARD,
			[Description("ft_classic")]
			FT_CLASSIC
		}

		public enum RewardType
		{
			NONE = 0,
			GOLD = 1,
			DUST = 2,
			ARCANE_ORBS = 3,
			BOOSTER = 4,
			CARD = 6,
			RANDOM_CARD = 7,
			TAVERN_TICKET = 8,
			CARD_BACK = 9,
			HERO_SKIN = 10,
			CUSTOM_COIN = 11,
			REWARD_TRACK_XP_BOOST = 12,
			CARD_SUBSET = 13
		}

		public enum CardPremiumLevel
		{
			NORMAL,
			GOLDEN,
			DIAMOND
		}

		public enum BnetGameType
		{
			[Description("bgt_unknown")]
			BGT_UNKNOWN = 0,
			[Description("bgt_friends")]
			BGT_FRIENDS = 1,
			[Description("bgt_ranked_standard")]
			BGT_RANKED_STANDARD = 2,
			[Description("bgt_arena")]
			BGT_ARENA = 3,
			[Description("bgt_vs_ai")]
			BGT_VS_AI = 4,
			[Description("bgt_tutorial")]
			BGT_TUTORIAL = 5,
			[Description("bgt_async")]
			BGT_ASYNC = 6,
			[Description("bgt_casual_standard")]
			BGT_CASUAL_STANDARD = 10,
			[Description("bgt_test1")]
			BGT_TEST1 = 11,
			[Description("bgt_test2")]
			BGT_TEST2 = 12,
			[Description("bgt_test3")]
			BGT_TEST3 = 13,
			[Description("bgt_tavernbrawl_pvp")]
			BGT_TAVERNBRAWL_PVP = 0x10,
			[Description("bgt_tavernbrawl_1p_versus_ai")]
			BGT_TAVERNBRAWL_1P_VERSUS_AI = 17,
			[Description("bgt_tavernbrawl_2p_coop")]
			BGT_TAVERNBRAWL_2P_COOP = 18,
			[Description("bgt_ranked_wild")]
			BGT_RANKED_WILD = 30,
			[Description("bgt_casual_wild")]
			BGT_CASUAL_WILD = 0x1F,
			[Description("bgt_fsg_brawl_vs_friend")]
			BGT_FSG_BRAWL_VS_FRIEND = 40,
			[Description("bgt_fsg_brawl_pvp")]
			BGT_FSG_BRAWL_PVP = 41,
			[Description("bgt_fsg_brawl_1p_versus_ai")]
			BGT_FSG_BRAWL_1P_VERSUS_AI = 42,
			[Description("bgt_fsg_brawl_2p_coop")]
			BGT_FSG_BRAWL_2P_COOP = 43,
			[Description("bgt_ranked_standard_new_player")]
			BGT_RANKED_STANDARD_NEW_PLAYER = 45,
			[Description("bgt_battlegrounds")]
			BGT_BATTLEGROUNDS = 50,
			[Description("bgt_battlegrounds_friendly")]
			BGT_BATTLEGROUNDS_FRIENDLY = 51,
			[Description("bgt_pvpdr_paid")]
			BGT_PVPDR_PAID = 54,
			[Description("bgt_pvpdr")]
			BGT_PVPDR = 55,
			[Description("bgt_reserved_18_22")]
			BGT_RESERVED_18_22 = 56,
			[Description("bgt_reserved_18_23")]
			BGT_RESERVED_18_23 = 57,
			[Description("bgt_ranked_classic")]
			BGT_RANKED_CLASSIC = 58,
			[Description("bgt_casual_classic")]
			BGT_CASUAL_CLASSIC = 59,
			[Description("bgt_last")]
			BGT_LAST = 60
		}

		public enum SoundCategory
		{
			NONE,
			FX,
			MUSIC,
			VO,
			SPECIAL_VO,
			SPECIAL_CARD,
			AMBIENCE,
			SPECIAL_MUSIC,
			TRIGGER_VO,
			HERO_MUSIC,
			BOSS_VO,
			RESET_GAME
		}

		public enum GameStringCategory
		{
			INVALID,
			GLOBAL,
			GLUE,
			GAMEPLAY,
			TUTORIAL,
			PRESENCE,
			MISSION
		}

		public static AssetFlags ParseAssetFlagsValue(string value)
		{
			EnumUtils.TryGetEnum<AssetFlags>(value, out var outVal);
			return outVal;
		}

		public static PresenceStatus ParsePresenceStatusValue(string value)
		{
			EnumUtils.TryGetEnum<PresenceStatus>(value, out var outVal);
			return outVal;
		}

		public static Region ParseRegionValue(string value)
		{
			EnumUtils.TryGetEnum<Region>(value, out var outVal);
			return outVal;
		}

		public static FormatType ParseFormatTypeValue(string value)
		{
			EnumUtils.TryGetEnum<FormatType>(value, out var outVal);
			return outVal;
		}

		public static RewardType ParseRewardTypeValue(string value)
		{
			EnumUtils.TryGetEnum<RewardType>(value, out var outVal);
			return outVal;
		}

		public static CardPremiumLevel ParseCardPremiumLevelValue(string value)
		{
			EnumUtils.TryGetEnum<CardPremiumLevel>(value, out var outVal);
			return outVal;
		}

		public static BnetGameType ParseBnetGameTypeValue(string value)
		{
			EnumUtils.TryGetEnum<BnetGameType>(value, out var outVal);
			return outVal;
		}

		public static SoundCategory ParseSoundCategoryValue(string value)
		{
			EnumUtils.TryGetEnum<SoundCategory>(value, out var outVal);
			return outVal;
		}

		public static GameStringCategory ParseGameStringCategoryValue(string value)
		{
			EnumUtils.TryGetEnum<GameStringCategory>(value, out var outVal);
			return outVal;
		}
	}
}
