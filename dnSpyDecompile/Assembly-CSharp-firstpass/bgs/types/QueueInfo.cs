using System;
using System.Runtime.InteropServices;

namespace bgs.types
{
	// Token: 0x02000272 RID: 626
	public struct QueueInfo
	{
		// Token: 0x04000FCE RID: 4046
		[MarshalAs(UnmanagedType.I1)]
		public bool changed;

		// Token: 0x04000FCF RID: 4047
		public int position;

		// Token: 0x04000FD0 RID: 4048
		public long end;

		// Token: 0x04000FD1 RID: 4049
		public long stdev;
	}
}
