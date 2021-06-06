using System;
using System.Runtime.InteropServices;

namespace agent
{
	// Token: 0x020001F0 RID: 496
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct CachedState
	{
		// Token: 0x04000B21 RID: 2849
		[MarshalAs(UnmanagedType.Struct)]
		public BaseState m_baseState;

		// Token: 0x04000B22 RID: 2850
		[MarshalAs(UnmanagedType.Struct)]
		public UpdateProgress m_updateProgress;

		// Token: 0x04000B23 RID: 2851
		[MarshalAs(UnmanagedType.Struct)]
		public RepairProgress m_repairProgress;

		// Token: 0x04000B24 RID: 2852
		[MarshalAs(UnmanagedType.Struct)]
		public BackfillProgress m_backfillProgress;

		// Token: 0x04000B25 RID: 2853
		[MarshalAs(UnmanagedType.Struct)]
		public ProgressDetails m_versionProgress;
	}
}
