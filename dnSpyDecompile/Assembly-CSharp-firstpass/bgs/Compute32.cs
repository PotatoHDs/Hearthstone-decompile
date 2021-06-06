using System;
using System.Text;

namespace bgs
{
	// Token: 0x0200021A RID: 538
	public class Compute32
	{
		// Token: 0x060022DD RID: 8925 RVA: 0x0007B1A0 File Offset: 0x000793A0
		public static uint Hash(string str)
		{
			uint num = 2166136261U;
			foreach (byte b in Encoding.ASCII.GetBytes(str))
			{
				num ^= (uint)b;
				num *= 16777619U;
			}
			return num;
		}

		// Token: 0x04000E5C RID: 3676
		public const uint FNV_32_PRIME = 16777619U;

		// Token: 0x04000E5D RID: 3677
		public const uint COMPUTE_32_OFFSET = 2166136261U;
	}
}
