using bgs.Shared.Util;

namespace bgs
{
	public static class LaunchOptionHelper
	{
		private static readonly string s_programCodeWtcg = new FourCC("WTCG").GetString();

		private static readonly string s_programCodeBna = new FourCC("BNA").GetString();

		private const string LaunchOptionBasePath = "Software\\Blizzard Entertainment\\Battle.net\\Launch Options\\";

		public static string ProgramCodeWtcg => s_programCodeWtcg;

		public static string ProgramCodeBna => s_programCodeBna;

		public static string GetLaunchOption(string key, bool encrypted, string programCode = null)
		{
			programCode = programCode ?? ProgramCodeWtcg;
			if (Registry.RetrieveString("Software\\Blizzard Entertainment\\Battle.net\\Launch Options\\" + programCode, key, out var s, encrypted) == BattleNetErrors.ERROR_OK)
			{
				return s;
			}
			return string.Empty;
		}
	}
}
