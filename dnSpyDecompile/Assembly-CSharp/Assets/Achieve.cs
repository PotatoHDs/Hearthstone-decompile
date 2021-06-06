using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FBA RID: 4026
	public static class Achieve
	{
		// Token: 0x0600B000 RID: 45056 RVA: 0x00366ABC File Offset: 0x00364CBC
		public static Achieve.Type ParseTypeValue(string value)
		{
			Achieve.Type result;
			EnumUtils.TryGetEnum<Achieve.Type>(value, out result);
			return result;
		}

		// Token: 0x0600B001 RID: 45057 RVA: 0x00366AD4 File Offset: 0x00364CD4
		public static Achieve.ClientFlags ParseClientFlagsValue(string value)
		{
			Achieve.ClientFlags result;
			EnumUtils.TryGetEnum<Achieve.ClientFlags>(value, out result);
			return result;
		}

		// Token: 0x0600B002 RID: 45058 RVA: 0x00366AEC File Offset: 0x00364CEC
		public static Achieve.Trigger ParseTriggerValue(string value)
		{
			Achieve.Trigger result;
			EnumUtils.TryGetEnum<Achieve.Trigger>(value, out result);
			return result;
		}

		// Token: 0x0600B003 RID: 45059 RVA: 0x00366B04 File Offset: 0x00364D04
		public static Achieve.GameMode ParseGameModeValue(string value)
		{
			Achieve.GameMode result;
			EnumUtils.TryGetEnum<Achieve.GameMode>(value, out result);
			return result;
		}

		// Token: 0x0600B004 RID: 45060 RVA: 0x00366B1C File Offset: 0x00364D1C
		public static Achieve.PlayerType ParsePlayerTypeValue(string value)
		{
			Achieve.PlayerType result;
			EnumUtils.TryGetEnum<Achieve.PlayerType>(value, out result);
			return result;
		}

		// Token: 0x0600B005 RID: 45061 RVA: 0x00366B34 File Offset: 0x00364D34
		public static Achieve.RewardTiming ParseRewardTimingValue(string value)
		{
			Achieve.RewardTiming result;
			EnumUtils.TryGetEnum<Achieve.RewardTiming>(value, out result);
			return result;
		}

		// Token: 0x0600B006 RID: 45062 RVA: 0x00366B4C File Offset: 0x00364D4C
		public static Achieve.Unlocks ParseUnlocksValue(string value)
		{
			Achieve.Unlocks result;
			EnumUtils.TryGetEnum<Achieve.Unlocks>(value, out result);
			return result;
		}

		// Token: 0x0600B007 RID: 45063 RVA: 0x00366B64 File Offset: 0x00364D64
		public static Achieve.AltTextPredicate ParseAltTextPredicateValue(string value)
		{
			Achieve.AltTextPredicate result;
			EnumUtils.TryGetEnum<Achieve.AltTextPredicate>(value, out result);
			return result;
		}

		// Token: 0x0600B008 RID: 45064 RVA: 0x00366B7C File Offset: 0x00364D7C
		public static Achieve.ShowToReturningPlayer ParseShowToReturningPlayerValue(string value)
		{
			Achieve.ShowToReturningPlayer result;
			EnumUtils.TryGetEnum<Achieve.ShowToReturningPlayer>(value, out result);
			return result;
		}

		// Token: 0x0600B009 RID: 45065 RVA: 0x00366B94 File Offset: 0x00364D94
		public static Achieve.AttentionBlocker ParseAttentionBlockerValue(string value)
		{
			Achieve.AttentionBlocker result;
			EnumUtils.TryGetEnum<Achieve.AttentionBlocker>(value, out result);
			return result;
		}

		// Token: 0x020027CB RID: 10187
		public enum Type
		{
			// Token: 0x0400F5B2 RID: 62898
			[Description("invalid")]
			INVALID,
			// Token: 0x0400F5B3 RID: 62899
			[Description("starter")]
			STARTER,
			// Token: 0x0400F5B4 RID: 62900
			[Description("hero")]
			HERO,
			// Token: 0x0400F5B5 RID: 62901
			[Description("goldhero")]
			GOLDHERO,
			// Token: 0x0400F5B6 RID: 62902
			[Description("daily")]
			DAILY,
			// Token: 0x0400F5B7 RID: 62903
			[Description("daily_repeatable")]
			DAILY_REPEATABLE,
			// Token: 0x0400F5B8 RID: 62904
			[Description("hidden")]
			HIDDEN,
			// Token: 0x0400F5B9 RID: 62905
			[Description("internal_active")]
			INTERNAL_ACTIVE,
			// Token: 0x0400F5BA RID: 62906
			[Description("internal_inactive")]
			INTERNAL_INACTIVE,
			// Token: 0x0400F5BB RID: 62907
			[Description("login_activated")]
			LOGIN_ACTIVATED,
			// Token: 0x0400F5BC RID: 62908
			[Description("normal_quest")]
			NORMAL_QUEST,
			// Token: 0x0400F5BD RID: 62909
			[Description("login_and_chain_activated")]
			LOGIN_AND_CHAIN_ACTIVATED,
			// Token: 0x0400F5BE RID: 62910
			[Description("premiumhero")]
			PREMIUMHERO
		}

		// Token: 0x020027CC RID: 10188
		[Flags]
		public enum ClientFlags
		{
			// Token: 0x0400F5C0 RID: 62912
			[Description("none")]
			NONE = 0,
			// Token: 0x0400F5C1 RID: 62913
			[Description("is_legendary")]
			IS_LEGENDARY = 1,
			// Token: 0x0400F5C2 RID: 62914
			[Description("show_in_quest_log")]
			SHOW_IN_QUEST_LOG = 2,
			// Token: 0x0400F5C3 RID: 62915
			[Description("is_affected_by_friend_week")]
			IS_AFFECTED_BY_FRIEND_WEEK = 4,
			// Token: 0x0400F5C4 RID: 62916
			[Description("is_affected_by_double_gold")]
			IS_AFFECTED_BY_DOUBLE_GOLD = 8
		}

		// Token: 0x020027CD RID: 10189
		public enum Trigger
		{
			// Token: 0x0400F5C6 RID: 62918
			[Description("unknown")]
			UNKNOWN,
			// Token: 0x0400F5C7 RID: 62919
			[Description("none")]
			NONE,
			// Token: 0x0400F5C8 RID: 62920
			[Description("win")]
			WIN,
			// Token: 0x0400F5C9 RID: 62921
			[Description("finish")]
			FINISH,
			// Token: 0x0400F5CA RID: 62922
			[Description("level")]
			LEVEL,
			// Token: 0x0400F5CB RID: 62923
			[Description("disenchant")]
			DISENCHANT,
			// Token: 0x0400F5CC RID: 62924
			[Description("race")]
			RACE,
			// Token: 0x0400F5CD RID: 62925
			[Description("goldrace")]
			GOLDRACE,
			// Token: 0x0400F5CE RID: 62926
			[Description("cardset")]
			CARDSET,
			// Token: 0x0400F5CF RID: 62927
			[Description("destroy")]
			DESTROY,
			// Token: 0x0400F5D0 RID: 62928
			[Description("spell")]
			SPELL = 12,
			// Token: 0x0400F5D1 RID: 62929
			[Description("dmghero")]
			DMGHERO,
			// Token: 0x0400F5D2 RID: 62930
			[Description("daily")]
			DAILY,
			// Token: 0x0400F5D3 RID: 62931
			[Description("click")]
			CLICK,
			// Token: 0x0400F5D4 RID: 62932
			[Description("purchase")]
			PURCHASE,
			// Token: 0x0400F5D5 RID: 62933
			[Description("licenseadded")]
			LICENSEADDED,
			// Token: 0x0400F5D6 RID: 62934
			[Description("licensedetected")]
			LICENSEDETECTED,
			// Token: 0x0400F5D7 RID: 62935
			[Description("skiptutorial")]
			SKIPTUTORIAL,
			// Token: 0x0400F5D8 RID: 62936
			[Description("starlevel")]
			STARLEVEL,
			// Token: 0x0400F5D9 RID: 62937
			[Description("fsg_finish")]
			FSG_FINISH,
			// Token: 0x0400F5DA RID: 62938
			[Description("event_timing_only")]
			EVENT_TIMING_ONLY,
			// Token: 0x0400F5DB RID: 62939
			[Description("login_igr")]
			LOGIN_IGR,
			// Token: 0x0400F5DC RID: 62940
			[Description("adventure_progress")]
			ADVENTURE_PROGRESS,
			// Token: 0x0400F5DD RID: 62941
			[Description("zero_cost_license")]
			ZERO_COST_LICENSE,
			// Token: 0x0400F5DE RID: 62942
			[Description("spectate_win")]
			SPECTATE_WIN,
			// Token: 0x0400F5DF RID: 62943
			[Description("login_device")]
			LOGIN_DEVICE,
			// Token: 0x0400F5E0 RID: 62944
			[Description("pack_ready_to_open")]
			PACK_READY_TO_OPEN,
			// Token: 0x0400F5E1 RID: 62945
			[Description("login")]
			LOGIN,
			// Token: 0x0400F5E2 RID: 62946
			[Description("player_recruited")]
			PLAYER_RECRUITED,
			// Token: 0x0400F5E3 RID: 62947
			[Description("play_with_tag")]
			PLAY_WITH_TAG,
			// Token: 0x0400F5E4 RID: 62948
			[Description("play_card")]
			PLAY_CARD,
			// Token: 0x0400F5E5 RID: 62949
			[Description("destroyed")]
			DESTROYED = 34,
			// Token: 0x0400F5E6 RID: 62950
			[Description("account_created")]
			ACCOUNT_CREATED,
			// Token: 0x0400F5E7 RID: 62951
			[Description("daily_or_shared")]
			DAILY_OR_SHARED,
			// Token: 0x0400F5E8 RID: 62952
			[Description("draw_card")]
			DRAW_CARD,
			// Token: 0x0400F5E9 RID: 62953
			[Description("end_turn")]
			END_TURN,
			// Token: 0x0400F5EA RID: 62954
			[Description("play_card_of_cost")]
			PLAY_CARD_OF_COST,
			// Token: 0x0400F5EB RID: 62955
			[Description("play_minion_of_cost")]
			PLAY_MINION_OF_COST
		}

		// Token: 0x020027CE RID: 10190
		public enum GameMode
		{
			// Token: 0x0400F5ED RID: 62957
			[Description("unknown")]
			UNKNOWN = -1,
			// Token: 0x0400F5EE RID: 62958
			[Description("any")]
			ANY,
			// Token: 0x0400F5EF RID: 62959
			[Description("any ai")]
			ANY_AI,
			// Token: 0x0400F5F0 RID: 62960
			[Description("any practice")]
			ANY_PRACTICE,
			// Token: 0x0400F5F1 RID: 62961
			[Description("basic ai")]
			BASIC_AI,
			// Token: 0x0400F5F2 RID: 62962
			[Description("expert ai")]
			EXPERT_AI,
			// Token: 0x0400F5F3 RID: 62963
			[Description("adventure")]
			ADVENTURE,
			// Token: 0x0400F5F4 RID: 62964
			[Description("tutorial")]
			TUTORIAL,
			// Token: 0x0400F5F5 RID: 62965
			[Description("matchmaker")]
			MATCHMAKER,
			// Token: 0x0400F5F6 RID: 62966
			[Description("play_mode")]
			PLAY_MODE,
			// Token: 0x0400F5F7 RID: 62967
			[Description("play_mode_standard")]
			PLAY_MODE_STANDARD,
			// Token: 0x0400F5F8 RID: 62968
			[Description("play_mode_wild")]
			PLAY_MODE_WILD,
			// Token: 0x0400F5F9 RID: 62969
			[Description("play_mode_tb")]
			PLAY_MODE_TB,
			// Token: 0x0400F5FA RID: 62970
			[Description("ranked")]
			RANKED,
			// Token: 0x0400F5FB RID: 62971
			[Description("casual")]
			CASUAL,
			// Token: 0x0400F5FC RID: 62972
			[Description("arena")]
			ARENA,
			// Token: 0x0400F5FD RID: 62973
			[Description("friendly")]
			FRIENDLY,
			// Token: 0x0400F5FE RID: 62974
			[Description("tavernbrawl")]
			TAVERNBRAWL,
			// Token: 0x0400F5FF RID: 62975
			[Description("tb_fsg_brawl")]
			TB_FSG_BRAWL,
			// Token: 0x0400F600 RID: 62976
			[Description("any_fsg_mode")]
			ANY_FSG_MODE,
			// Token: 0x0400F601 RID: 62977
			[Description("ranked_or_arena")]
			RANKED_OR_ARENA,
			// Token: 0x0400F602 RID: 62978
			[Description("other")]
			OTHER,
			// Token: 0x0400F603 RID: 62979
			[Description("any_non_adventure")]
			ANY_NON_ADVENTURE,
			// Token: 0x0400F604 RID: 62980
			[Description("battlegrounds")]
			BATTLEGROUNDS,
			// Token: 0x0400F605 RID: 62981
			[Description("duels")]
			DUELS,
			// Token: 0x0400F606 RID: 62982
			[Description("play_mode_classic")]
			PLAY_MODE_CLASSIC
		}

		// Token: 0x020027CF RID: 10191
		public enum PlayerType
		{
			// Token: 0x0400F608 RID: 62984
			[Description("any")]
			ANY,
			// Token: 0x0400F609 RID: 62985
			[Description("challenger")]
			CHALLENGER,
			// Token: 0x0400F60A RID: 62986
			[Description("challengee")]
			CHALLENGEE
		}

		// Token: 0x020027D0 RID: 10192
		public enum RewardTiming
		{
			// Token: 0x0400F60C RID: 62988
			[Description("immediate")]
			IMMEDIATE,
			// Token: 0x0400F60D RID: 62989
			[Description("never")]
			NEVER,
			// Token: 0x0400F60E RID: 62990
			[Description("adventure_chest")]
			ADVENTURE_CHEST,
			// Token: 0x0400F60F RID: 62991
			[Description("out_of_band")]
			OUT_OF_BAND
		}

		// Token: 0x020027D1 RID: 10193
		public enum Unlocks
		{
			// Token: 0x0400F611 RID: 62993
			[Description("none")]
			NONE = -1,
			// Token: 0x0400F612 RID: 62994
			[Description("forge")]
			FORGE,
			// Token: 0x0400F613 RID: 62995
			[Description("daily")]
			DAILY,
			// Token: 0x0400F614 RID: 62996
			[Description("tier2")]
			TIER2,
			// Token: 0x0400F615 RID: 62997
			[Description("tier3")]
			TIER3,
			// Token: 0x0400F616 RID: 62998
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx1_owned")]
			NAXX1_OWNED,
			// Token: 0x0400F617 RID: 62999
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx2_owned")]
			NAXX2_OWNED,
			// Token: 0x0400F618 RID: 63000
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx3_owned")]
			NAXX3_OWNED,
			// Token: 0x0400F619 RID: 63001
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx4_owned")]
			NAXX4_OWNED,
			// Token: 0x0400F61A RID: 63002
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx5_owned")]
			NAXX5_OWNED,
			// Token: 0x0400F61B RID: 63003
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx1_playable")]
			NAXX1_PLAYABLE,
			// Token: 0x0400F61C RID: 63004
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx2_playable")]
			NAXX2_PLAYABLE,
			// Token: 0x0400F61D RID: 63005
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx3_playable")]
			NAXX3_PLAYABLE,
			// Token: 0x0400F61E RID: 63006
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx4_playable")]
			NAXX4_PLAYABLE,
			// Token: 0x0400F61F RID: 63007
			[Obsolete("will be removed when the achievement system no longer requires it")]
			[Description("naxx5_playable")]
			NAXX5_PLAYABLE,
			// Token: 0x0400F620 RID: 63008
			[Description("vanilla_heroes")]
			VANILLA_HEROES
		}

		// Token: 0x020027D2 RID: 10194
		public enum AltTextPredicate
		{
			// Token: 0x0400F622 RID: 63010
			[Description("none")]
			NONE,
			// Token: 0x0400F623 RID: 63011
			[Description("can_see_wild")]
			CAN_SEE_WILD
		}

		// Token: 0x020027D3 RID: 10195
		public enum ShowToReturningPlayer
		{
			// Token: 0x0400F625 RID: 63013
			[Description("always")]
			ALWAYS,
			// Token: 0x0400F626 RID: 63014
			[Description("suppressed")]
			SUPPRESSED,
			// Token: 0x0400F627 RID: 63015
			[Description("suppressed_with_time")]
			SUPPRESSED_WITH_TIME
		}

		// Token: 0x020027D4 RID: 10196
		public enum AttentionBlocker
		{
			// Token: 0x0400F629 RID: 63017
			NONE,
			// Token: 0x0400F62A RID: 63018
			FATAL_ERROR_SCENE,
			// Token: 0x0400F62B RID: 63019
			SET_ROTATION_INTRO,
			// Token: 0x0400F62C RID: 63020
			SET_ROTATION_CM_TUTORIALS = 4
		}
	}
}
