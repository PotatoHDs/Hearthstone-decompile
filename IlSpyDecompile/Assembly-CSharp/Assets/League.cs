using System.ComponentModel;

namespace Assets
{
	public static class League
	{
		public enum LeagueType
		{
			[Description("unknown")]
			UNKNOWN,
			[Description("normal")]
			NORMAL,
			[Description("new_player")]
			NEW_PLAYER
		}

		public static LeagueType ParseLeagueTypeValue(string value)
		{
			EnumUtils.TryGetEnum<LeagueType>(value, out var outVal);
			return outVal;
		}
	}
}
