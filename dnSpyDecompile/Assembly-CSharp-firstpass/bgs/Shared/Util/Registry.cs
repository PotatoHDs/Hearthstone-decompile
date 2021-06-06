using System;
using bgs.Shared.Util.platform_win32;

namespace bgs.Shared.Util
{
	// Token: 0x0200027F RID: 639
	public class Registry
	{
		// Token: 0x060025D7 RID: 9687 RVA: 0x00086197 File Offset: 0x00084397
		public static BattleNetErrors RetrieveVector(string path, string name, out byte[] vec, bool encrypt = true)
		{
			return Registry.mImpl.RetrieveVector(path, name, out vec, encrypt);
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000861A7 File Offset: 0x000843A7
		public static BattleNetErrors RetrieveString(string path, string name, out string s, bool encrypt = false)
		{
			return Registry.mImpl.RetrieveString(path, name, out s, encrypt);
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000861B7 File Offset: 0x000843B7
		public static BattleNetErrors RetrieveInt(string path, string name, out int i)
		{
			return Registry.mImpl.RetrieveInt(path, name, out i);
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000861C6 File Offset: 0x000843C6
		private static BattleNetErrors DeleteData(string path, string name)
		{
			return Registry.mImpl.DeleteData(path, name);
		}

		// Token: 0x04001030 RID: 4144
		private static readonly IRegistry mImpl = new RegistryWin();

		// Token: 0x04001031 RID: 4145
		public static readonly byte[] s_entropy = new byte[]
		{
			200,
			118,
			244,
			174,
			76,
			149,
			46,
			254,
			242,
			250,
			15,
			84,
			25,
			192,
			156,
			67
		};
	}
}
