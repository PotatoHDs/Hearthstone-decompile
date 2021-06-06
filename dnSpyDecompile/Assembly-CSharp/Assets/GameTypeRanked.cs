using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FC9 RID: 4041
	public static class GameTypeRanked
	{
		// Token: 0x0600B01A RID: 45082 RVA: 0x00366D2C File Offset: 0x00364F2C
		public static GameTypeRanked.FormatType ParseFormatTypeValue(string value)
		{
			GameTypeRanked.FormatType result;
			EnumUtils.TryGetEnum<GameTypeRanked.FormatType>(value, out result);
			return result;
		}

		// Token: 0x0600B01B RID: 45083 RVA: 0x00366D44 File Offset: 0x00364F44
		public static GameTypeRanked.GameType ParseGameTypeValue(string value)
		{
			GameTypeRanked.GameType result;
			EnumUtils.TryGetEnum<GameTypeRanked.GameType>(value, out result);
			return result;
		}

		// Token: 0x020027E5 RID: 10213
		public enum FormatType
		{
			// Token: 0x0400F6D6 RID: 63190
			[Description("ft_unknown")]
			FT_UNKNOWN,
			// Token: 0x0400F6D7 RID: 63191
			[Description("ft_wild")]
			FT_WILD,
			// Token: 0x0400F6D8 RID: 63192
			[Description("ft_standard")]
			FT_STANDARD,
			// Token: 0x0400F6D9 RID: 63193
			[Description("ft_classic")]
			FT_CLASSIC
		}

		// Token: 0x020027E6 RID: 10214
		public enum GameType
		{
			// Token: 0x0400F6DB RID: 63195
			[Description("bgt_unknown")]
			BGT_UNKNOWN,
			// Token: 0x0400F6DC RID: 63196
			[Description("bgt_friends")]
			BGT_FRIENDS,
			// Token: 0x0400F6DD RID: 63197
			[Description("bgt_ranked_standard")]
			BGT_RANKED_STANDARD,
			// Token: 0x0400F6DE RID: 63198
			[Description("bgt_arena")]
			BGT_ARENA,
			// Token: 0x0400F6DF RID: 63199
			[Description("bgt_vs_ai")]
			BGT_VS_AI,
			// Token: 0x0400F6E0 RID: 63200
			[Description("bgt_tutorial")]
			BGT_TUTORIAL,
			// Token: 0x0400F6E1 RID: 63201
			[Description("bgt_async")]
			BGT_ASYNC,
			// Token: 0x0400F6E2 RID: 63202
			[Description("bgt_test1")]
			BGT_TEST1 = 11,
			// Token: 0x0400F6E3 RID: 63203
			[Description("bgt_test2")]
			BGT_TEST2,
			// Token: 0x0400F6E4 RID: 63204
			[Description("bgt_test3")]
			BGT_TEST3,
			// Token: 0x0400F6E5 RID: 63205
			[Description("bgt_tavernbrawl_pvp")]
			BGT_TAVERNBRAWL_PVP = 16,
			// Token: 0x0400F6E6 RID: 63206
			[Description("bgt_tavernbrawl_1p_versus_ai")]
			BGT_TAVERNBRAWL_1P_VERSUS_AI,
			// Token: 0x0400F6E7 RID: 63207
			[Description("bgt_tavernbrawl_2p_coop")]
			BGT_TAVERNBRAWL_2P_COOP,
			// Token: 0x0400F6E8 RID: 63208
			[Description("bgt_ranked_wild")]
			BGT_RANKED_WILD = 30,
			// Token: 0x0400F6E9 RID: 63209
			[Description("bgt_fsg_brawl_vs_friend")]
			BGT_FSG_BRAWL_VS_FRIEND = 40,
			// Token: 0x0400F6EA RID: 63210
			[Description("bgt_fsg_brawl_pvp")]
			BGT_FSG_BRAWL_PVP,
			// Token: 0x0400F6EB RID: 63211
			[Description("bgt_fsg_brawl_1p_versus_ai")]
			BGT_FSG_BRAWL_1P_VERSUS_AI,
			// Token: 0x0400F6EC RID: 63212
			[Description("bgt_fsg_brawl_2p_coop")]
			BGT_FSG_BRAWL_2P_COOP,
			// Token: 0x0400F6ED RID: 63213
			[Description("bgt_ranked_standard_new_player")]
			BGT_RANKED_STANDARD_NEW_PLAYER = 45,
			// Token: 0x0400F6EE RID: 63214
			[Description("bgt_battlegrounds")]
			BGT_BATTLEGROUNDS = 50,
			// Token: 0x0400F6EF RID: 63215
			[Description("bgt_battlegrounds_friendly")]
			BGT_BATTLEGROUNDS_FRIENDLY,
			// Token: 0x0400F6F0 RID: 63216
			[Description("bgt_reserved_18_2")]
			BGT_RESERVED_18_2 = 55,
			// Token: 0x0400F6F1 RID: 63217
			[Description("bgt_reserved_19_6")]
			BGT_RESERVED_19_6,
			// Token: 0x0400F6F2 RID: 63218
			[Description("bgt_ranked_classic")]
			BGT_RANKED_CLASSIC = 58,
			// Token: 0x0400F6F3 RID: 63219
			[Description("bgt_last")]
			BGT_LAST = 60
		}
	}
}
