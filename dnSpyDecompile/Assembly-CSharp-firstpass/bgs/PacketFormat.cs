using System;

namespace bgs
{
	// Token: 0x0200021B RID: 539
	public abstract class PacketFormat
	{
		// Token: 0x060022DF RID: 8927
		public abstract int Decode(byte[] bytes, int offset, int available);

		// Token: 0x060022E0 RID: 8928
		public abstract byte[] Encode();

		// Token: 0x060022E1 RID: 8929
		public abstract bool IsLoaded();

		// Token: 0x060022E2 RID: 8930
		public abstract bool IsFatalOnError();
	}
}
