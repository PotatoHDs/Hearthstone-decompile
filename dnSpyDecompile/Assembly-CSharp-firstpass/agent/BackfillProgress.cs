using System;
using System.Runtime.InteropServices;

namespace agent
{
	// Token: 0x020001EE RID: 494
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct BackfillProgress
	{
		// Token: 0x04000B17 RID: 2839
		[MarshalAs(UnmanagedType.Struct)]
		public DownloadDetails m_downloadDetails;

		// Token: 0x04000B18 RID: 2840
		[MarshalAs(UnmanagedType.Struct)]
		public ProgressDetails m_progressDetails;

		// Token: 0x04000B19 RID: 2841
		[MarshalAs(UnmanagedType.U1)]
		public bool m_isBgdl;

		// Token: 0x04000B1A RID: 2842
		[MarshalAs(UnmanagedType.U1)]
		public bool m_isPaused;
	}
}
