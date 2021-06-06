using System.ComponentModel;

namespace Assets
{
	public static class GameTypeRanked
	{
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

		public enum GameType
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
			[Description("bgt_reserved_18_2")]
			BGT_RESERVED_18_2 = 55,
			[Description("bgt_reserved_19_6")]
			BGT_RESERVED_19_6 = 56,
			[Description("bgt_ranked_classic")]
			BGT_RANKED_CLASSIC = 58,
			[Description("bgt_last")]
			BGT_LAST = 60
		}

		public static FormatType ParseFormatTypeValue(string value)
		{
			EnumUtils.TryGetEnum<FormatType>(value, out var outVal);
			return outVal;
		}

		public static GameType ParseGameTypeValue(string value)
		{
			EnumUtils.TryGetEnum<GameType>(value, out var outVal);
			return outVal;
		}
	}
}
