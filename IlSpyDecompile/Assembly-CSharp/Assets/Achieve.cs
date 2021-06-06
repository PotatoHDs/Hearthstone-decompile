using System;
using System.ComponentModel;

namespace Assets
{
	public static class Achieve
	{
		public enum Type
		{
			[Description("invalid")]
			INVALID,
			[Description("starter")]
			STARTER,
			[Description("hero")]
			HERO,
			[Description("goldhero")]
			GOLDHERO,
			[Description("daily")]
			DAILY,
			[Description("daily_repeatable")]
			DAILY_REPEATABLE,
			[Description("hidden")]
			HIDDEN,
			[Description("internal_active")]
			INTERNAL_ACTIVE,
			[Description("internal_inactive")]
			INTERNAL_INACTIVE,
			[Description("login_activated")]
			LOGIN_ACTIVATED,
			[Description("normal_quest")]
			NORMAL_QUEST,
			[Description("login_and_chain_activated")]
			LOGIN_AND_CHAIN_ACTIVATED,
			[Description("premiumhero")]
			PREMIUMHERO
		}

		[Flags]
		public enum ClientFlags
		{
			[Description("none")]
			NONE = 0x0,
			[Description("is_legendary")]
			IS_LEGENDARY = 0x1,
			[Description("show_in_quest_log")]
			SHOW_IN_QUEST_LOG = 0x2,
			[Description("is_affected_by_friend_week")]
			IS_AFFECTED_BY_FRIEND_WEEK = 0x4,
			[Description("is_affected_by_double_gold")]
			IS_AFFECTED_BY_DOUBLE_GOLD = 0x8
		}

		public enum Trigger
		{
			[Description("unknown")]
			UNKNOWN = 0,
			[Description("none")]
			NONE = 1,
			[Description("win")]
			WIN = 2,
			[Description("finish")]
			FINISH = 3,
			[Description("level")]
			LEVEL = 4,
			[Description("disenchant")]
			DISENCHANT = 5,
			[Description("race")]
			RACE = 6,
			[Description("goldrace")]
			GOLDRACE = 7,
			[Description("cardset")]
			CARDSET = 8,
			[Description("destroy")]
			DESTROY = 9,
			[Description("spell")]
			SPELL = 12,
			[Description("dmghero")]
			DMGHERO = 13,
			[Description("daily")]
			DAILY = 14,
			[Description("click")]
			CLICK = 0xF,
			[Description("purchase")]
			PURCHASE = 0x10,
			[Description("licenseadded")]
			LICENSEADDED = 17,
			[Description("licensedetected")]
			LICENSEDETECTED = 18,
			[Description("skiptutorial")]
			SKIPTUTORIAL = 19,
			[Description("starlevel")]
			STARLEVEL = 20,
			[Description("fsg_finish")]
			FSG_FINISH = 21,
			[Description("event_timing_only")]
			EVENT_TIMING_ONLY = 22,
			[Description("login_igr")]
			LOGIN_IGR = 23,
			[Description("adventure_progress")]
			ADVENTURE_PROGRESS = 24,
			[Description("zero_cost_license")]
			ZERO_COST_LICENSE = 25,
			[Description("spectate_win")]
			SPECTATE_WIN = 26,
			[Description("login_device")]
			LOGIN_DEVICE = 27,
			[Description("pack_ready_to_open")]
			PACK_READY_TO_OPEN = 28,
			[Description("login")]
			LOGIN = 29,
			[Description("player_recruited")]
			PLAYER_RECRUITED = 30,
			[Description("play_with_tag")]
			PLAY_WITH_TAG = 0x1F,
			[Description("play_card")]
			PLAY_CARD = 0x20,
			[Description("destroyed")]
			DESTROYED = 34,
			[Description("account_created")]
			ACCOUNT_CREATED = 35,
			[Description("daily_or_shared")]
			DAILY_OR_SHARED = 36,
			[Description("draw_card")]
			DRAW_CARD = 37,
			[Description("end_turn")]
			END_TURN = 38,
			[Description("play_card_of_cost")]
			PLAY_CARD_OF_COST = 39,
			[Description("play_minion_of_cost")]
			PLAY_MINION_OF_COST = 40
		}

		public enum GameMode
		{
			[Description("unknown")]
			UNKNOWN = -1,
			[Description("any")]
			ANY,
			[Description("any ai")]
			ANY_AI,
			[Description("any practice")]
			ANY_PRACTICE,
			[Description("basic ai")]
			BASIC_AI,
			[Description("expert ai")]
			EXPERT_AI,
			[Description("adventure")]
			ADVENTURE,
			[Description("tutorial")]
			TUTORIAL,
			[Description("matchmaker")]
			MATCHMAKER,
			[Description("play_mode")]
			PLAY_MODE,
			[Description("play_mode_standard")]
			PLAY_MODE_STANDARD,
			[Description("play_mode_wild")]
			PLAY_MODE_WILD,
			[Description("play_mode_tb")]
			PLAY_MODE_TB,
			[Description("ranked")]
			RANKED,
			[Description("casual")]
			CASUAL,
			[Description("arena")]
			ARENA,
			[Description("friendly")]
			FRIENDLY,
			[Description("tavernbrawl")]
			TAVERNBRAWL,
			[Description("tb_fsg_brawl")]
			TB_FSG_BRAWL,
			[Description("any_fsg_mode")]
			ANY_FSG_MODE,
			[Description("ranked_or_arena")]
			RANKED_OR_ARENA,
			[Description("other")]
			OTHER,
			[Description("any_non_adventure")]
			ANY_NON_ADVENTURE,
			[Description("battlegrounds")]
			BATTLEGROUNDS,
			[Description("duels")]
			DUELS,
			[Description("play_mode_classic")]
			PLAY_MODE_CLASSIC
		}

		public enum PlayerType
		{
			[Description("any")]
			ANY,
			[Description("challenger")]
			CHALLENGER,
			[Description("challengee")]
			CHALLENGEE
		}

		public enum RewardTiming
		{
			[Description("immediate")]
			IMMEDIATE,
			[Description("never")]
			NEVER,
			[Description("adventure_chest")]
			ADVENTURE_CHEST,
			[Description("out_of_band")]
			OUT_OF_BAND
		}

		public enum Unlocks
		{
			[Description("none")]
			NONE = -1,
			[Description("forge")]
			FORGE,
			[Description("daily")]
			DAILY,
			[Description("tier2")]
			TIER2,
			[Description("tier3")]
			TIER3,
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx1_owned")]
			NAXX1_OWNED,
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx2_owned")]
			NAXX2_OWNED,
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx3_owned")]
			NAXX3_OWNED,
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx4_owned")]
			NAXX4_OWNED,
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx5_owned")]
			NAXX5_OWNED,
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx1_playable")]
			NAXX1_PLAYABLE,
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx2_playable")]
			NAXX2_PLAYABLE,
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx3_playable")]
			NAXX3_PLAYABLE,
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx4_playable")]
			NAXX4_PLAYABLE,
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx5_playable")]
			NAXX5_PLAYABLE,
			[Description("vanilla_heroes")]
			VANILLA_HEROES
		}

		public enum AltTextPredicate
		{
			[Description("none")]
			NONE,
			[Description("can_see_wild")]
			CAN_SEE_WILD
		}

		public enum ShowToReturningPlayer
		{
			[Description("always")]
			ALWAYS,
			[Description("suppressed")]
			SUPPRESSED,
			[Description("suppressed_with_time")]
			SUPPRESSED_WITH_TIME
		}

		public enum AttentionBlocker
		{
			NONE = 0,
			FATAL_ERROR_SCENE = 1,
			SET_ROTATION_INTRO = 2,
			SET_ROTATION_CM_TUTORIALS = 4
		}

		public static Type ParseTypeValue(string value)
		{
			EnumUtils.TryGetEnum<Type>(value, out var outVal);
			return outVal;
		}

		public static ClientFlags ParseClientFlagsValue(string value)
		{
			EnumUtils.TryGetEnum<ClientFlags>(value, out var outVal);
			return outVal;
		}

		public static Trigger ParseTriggerValue(string value)
		{
			EnumUtils.TryGetEnum<Trigger>(value, out var outVal);
			return outVal;
		}

		public static GameMode ParseGameModeValue(string value)
		{
			EnumUtils.TryGetEnum<GameMode>(value, out var outVal);
			return outVal;
		}

		public static PlayerType ParsePlayerTypeValue(string value)
		{
			EnumUtils.TryGetEnum<PlayerType>(value, out var outVal);
			return outVal;
		}

		public static RewardTiming ParseRewardTimingValue(string value)
		{
			EnumUtils.TryGetEnum<RewardTiming>(value, out var outVal);
			return outVal;
		}

		public static Unlocks ParseUnlocksValue(string value)
		{
			EnumUtils.TryGetEnum<Unlocks>(value, out var outVal);
			return outVal;
		}

		public static AltTextPredicate ParseAltTextPredicateValue(string value)
		{
			EnumUtils.TryGetEnum<AltTextPredicate>(value, out var outVal);
			return outVal;
		}

		public static ShowToReturningPlayer ParseShowToReturningPlayerValue(string value)
		{
			EnumUtils.TryGetEnum<ShowToReturningPlayer>(value, out var outVal);
			return outVal;
		}

		public static AttentionBlocker ParseAttentionBlockerValue(string value)
		{
			EnumUtils.TryGetEnum<AttentionBlocker>(value, out var outVal);
			return outVal;
		}
	}
}
