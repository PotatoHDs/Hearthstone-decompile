using System;

namespace bgs.Shared.Util
{
	// Token: 0x0200027E RID: 638
	public interface IRegistry
	{
		// Token: 0x060025D3 RID: 9683
		BattleNetErrors RetrieveVector(string path, string name, out byte[] vec, bool encrypt = true);

		// Token: 0x060025D4 RID: 9684
		BattleNetErrors RetrieveString(string path, string name, out string s, bool encrypt = false);

		// Token: 0x060025D5 RID: 9685
		BattleNetErrors RetrieveInt(string path, string name, out int i);

		// Token: 0x060025D6 RID: 9686
		BattleNetErrors DeleteData(string path, string name);
	}
}
