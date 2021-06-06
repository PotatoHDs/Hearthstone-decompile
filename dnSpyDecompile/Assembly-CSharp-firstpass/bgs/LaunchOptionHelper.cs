using System;
using bgs.Shared.Util;

namespace bgs
{
	// Token: 0x02000229 RID: 553
	public static class LaunchOptionHelper
	{
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x0007B44E File Offset: 0x0007964E
		public static string ProgramCodeWtcg
		{
			get
			{
				return LaunchOptionHelper.s_programCodeWtcg;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x0007B455 File Offset: 0x00079655
		public static string ProgramCodeBna
		{
			get
			{
				return LaunchOptionHelper.s_programCodeBna;
			}
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x0007B45C File Offset: 0x0007965C
		public static string GetLaunchOption(string key, bool encrypted, string programCode = null)
		{
			programCode = (programCode ?? LaunchOptionHelper.ProgramCodeWtcg);
			string result;
			if (Registry.RetrieveString("Software\\Blizzard Entertainment\\Battle.net\\Launch Options\\" + programCode, key, out result, encrypted) == BattleNetErrors.ERROR_OK)
			{
				return result;
			}
			return string.Empty;
		}

		// Token: 0x04000E6F RID: 3695
		private static readonly string s_programCodeWtcg = new FourCC("WTCG").GetString();

		// Token: 0x04000E70 RID: 3696
		private static readonly string s_programCodeBna = new FourCC("BNA").GetString();

		// Token: 0x04000E71 RID: 3697
		private const string LaunchOptionBasePath = "Software\\Blizzard Entertainment\\Battle.net\\Launch Options\\";
	}
}
