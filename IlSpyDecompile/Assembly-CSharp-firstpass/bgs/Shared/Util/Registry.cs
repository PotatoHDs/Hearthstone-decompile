using bgs.Shared.Util.platform_win32;

namespace bgs.Shared.Util
{
	public class Registry
	{
		private static readonly IRegistry mImpl = new RegistryWin();

		public static readonly byte[] s_entropy = new byte[16]
		{
			200, 118, 244, 174, 76, 149, 46, 254, 242, 250,
			15, 84, 25, 192, 156, 67
		};

		public static BattleNetErrors RetrieveVector(string path, string name, out byte[] vec, bool encrypt = true)
		{
			return mImpl.RetrieveVector(path, name, out vec, encrypt);
		}

		public static BattleNetErrors RetrieveString(string path, string name, out string s, bool encrypt = false)
		{
			return mImpl.RetrieveString(path, name, out s, encrypt);
		}

		public static BattleNetErrors RetrieveInt(string path, string name, out int i)
		{
			return mImpl.RetrieveInt(path, name, out i);
		}

		private static BattleNetErrors DeleteData(string path, string name)
		{
			return mImpl.DeleteData(path, name);
		}
	}
}
