using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FCC RID: 4044
	public static class LeagueGameType
	{
		// Token: 0x0600B026 RID: 45094 RVA: 0x00366E4C File Offset: 0x0036504C
		public static LeagueGameType.FormatType ParseFormatTypeValue(string value)
		{
			LeagueGameType.FormatType result;
			EnumUtils.TryGetEnum<LeagueGameType.FormatType>(value, out result);
			return result;
		}

		// Token: 0x0600B027 RID: 45095 RVA: 0x00366E64 File Offset: 0x00365064
		public static LeagueGameType.BnetGameType ParseBnetGameTypeValue(string value)
		{
			LeagueGameType.BnetGameType result;
			EnumUtils.TryGetEnum<LeagueGameType.BnetGameType>(value, out result);
			return result;
		}

		// Token: 0x020027F1 RID: 10225
		public enum FormatType
		{
			// Token: 0x0400F7AD RID: 63405
			[Description("ft_unknown")]
			FT_UNKNOWN,
			// Token: 0x0400F7AE RID: 63406
			[Description("ft_wild")]
			FT_WILD,
			// Token: 0x0400F7AF RID: 63407
			[Description("ft_standard")]
			FT_STANDARD,
			// Token: 0x0400F7B0 RID: 63408
			[Description("ft_classic")]
			FT_CLASSIC
		}

		// Token: 0x020027F2 RID: 10226
		public enum BnetGameType
		{
			// Token: 0x0400F7B2 RID: 63410
			[Description("bgt_unknown")]
			BGT_UNKNOWN,
			// Token: 0x0400F7B3 RID: 63411
			[Description("bgt_friends")]
			BGT_FRIENDS,
			// Token: 0x0400F7B4 RID: 63412
			[Description("bgt_ranked_standard")]
			BGT_RANKED_STANDARD,
			// Token: 0x0400F7B5 RID: 63413
			[Description("bgt_arena")]
			BGT_ARENA,
			// Token: 0x0400F7B6 RID: 63414
			[Description("bgt_vs_ai")]
			BGT_VS_AI,
			// Token: 0x0400F7B7 RID: 63415
			[Description("bgt_tutorial")]
			BGT_TUTORIAL,
			// Token: 0x0400F7B8 RID: 63416
			[Description("bgt_async")]
			BGT_ASYNC,
			// Token: 0x0400F7B9 RID: 63417
			[Description("bgt_casual_standard")]
			BGT_CASUAL_STANDARD = 10,
			// Token: 0x0400F7BA RID: 63418
			[Description("bgt_test1")]
			BGT_TEST1,
			// Token: 0x0400F7BB RID: 63419
			[Description("bgt_test2")]
			BGT_TEST2,
			// Token: 0x0400F7BC RID: 63420
			[Description("bgt_test3")]
			BGT_TEST3,
			// Token: 0x0400F7BD RID: 63421
			[Description("bgt_tavernbrawl_pvp")]
			BGT_TAVERNBRAWL_PVP = 16,
			// Token: 0x0400F7BE RID: 63422
			[Description("bgt_tavernbrawl_1p_versus_ai")]
			BGT_TAVERNBRAWL_1P_VERSUS_AI,
			// Token: 0x0400F7BF RID: 63423
			[Description("bgt_tavernbrawl_2p_coop")]
			BGT_TAVERNBRAWL_2P_COOP,
			// Token: 0x0400F7C0 RID: 63424
			[Description("bgt_ranked_wild")]
			BGT_RANKED_WILD = 30,
			// Token: 0x0400F7C1 RID: 63425
			[Description("bgt_casual_wild")]
			BGT_CASUAL_WILD,
			// Token: 0x0400F7C2 RID: 63426
			[Description("bgt_fsg_brawl_vs_friend")]
			BGT_FSG_BRAWL_VS_FRIEND = 40,
			// Token: 0x0400F7C3 RID: 63427
			[Description("bgt_fsg_brawl_pvp")]
			BGT_FSG_BRAWL_PVP,
			// Token: 0x0400F7C4 RID: 63428
			[Description("bgt_fsg_brawl_1p_versus_ai")]
			BGT_FSG_BRAWL_1P_VERSUS_AI,
			// Token: 0x0400F7C5 RID: 63429
			[Description("bgt_fsg_brawl_2p_coop")]
			BGT_FSG_BRAWL_2P_COOP,
			// Token: 0x0400F7C6 RID: 63430
			[Description("bgt_ranked_standard_new_player")]
			BGT_RANKED_STANDARD_NEW_PLAYER = 45,
			// Token: 0x0400F7C7 RID: 63431
			[Description("bgt_battlegrounds")]
			BGT_BATTLEGROUNDS = 50,
			// Token: 0x0400F7C8 RID: 63432
			[Description("bgt_battlegrounds_friendly")]
			BGT_BATTLEGROUNDS_FRIENDLY,
			// Token: 0x0400F7C9 RID: 63433
			[Description("bgt_pvpdr_paid")]
			BGT_PVPDR_PAID = 54,
			// Token: 0x0400F7CA RID: 63434
			[Description("bgt_pvpdr")]
			BGT_PVPDR,
			// Token: 0x0400F7CB RID: 63435
			[Description("bgt_reserved_18_22")]
			BGT_RESERVED_18_22,
			// Token: 0x0400F7CC RID: 63436
			[Description("bgt_reserved_18_23")]
			BGT_RESERVED_18_23,
			// Token: 0x0400F7CD RID: 63437
			[Description("bgt_ranked_classic")]
			BGT_RANKED_CLASSIC,
			// Token: 0x0400F7CE RID: 63438
			[Description("bgt_casual_classic")]
			BGT_CASUAL_CLASSIC,
			// Token: 0x0400F7CF RID: 63439
			[Description("bgt_last")]
			BGT_LAST
		}
	}
}
