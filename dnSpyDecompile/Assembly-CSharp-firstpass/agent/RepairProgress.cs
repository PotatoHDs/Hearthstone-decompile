using System;
using System.Runtime.InteropServices;

namespace agent
{
	// Token: 0x020001ED RID: 493
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct RepairProgress
	{
		// Token: 0x04000B15 RID: 2837
		[MarshalAs(UnmanagedType.Struct)]
		public ProgressDetails m_progressDetails;

		// Token: 0x04000B16 RID: 2838
		[MarshalAs(UnmanagedType.U1)]
		public bool m_updateRequired;
	}
}
