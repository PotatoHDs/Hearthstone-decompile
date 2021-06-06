using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FCB RID: 4043
	public static class League
	{
		// Token: 0x0600B025 RID: 45093 RVA: 0x00366E34 File Offset: 0x00365034
		public static League.LeagueType ParseLeagueTypeValue(string value)
		{
			League.LeagueType result;
			EnumUtils.TryGetEnum<League.LeagueType>(value, out result);
			return result;
		}

		// Token: 0x020027F0 RID: 10224
		public enum LeagueType
		{
			// Token: 0x0400F7A9 RID: 63401
			[Description("unknown")]
			UNKNOWN,
			// Token: 0x0400F7AA RID: 63402
			[Description("normal")]
			NORMAL,
			// Token: 0x0400F7AB RID: 63403
			[Description("new_player")]
			NEW_PLAYER
		}
	}
}
