using System;
using System.Runtime.InteropServices;

namespace agent
{
	// Token: 0x020001EF RID: 495
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct BaseState
	{
		// Token: 0x04000B1B RID: 2843
		[MarshalAs(UnmanagedType.U1)]
		public bool m_playable;

		// Token: 0x04000B1C RID: 2844
		[MarshalAs(UnmanagedType.U1)]
		public bool m_installed;

		// Token: 0x04000B1D RID: 2845
		[MarshalAs(UnmanagedType.U1)]
		public bool m_updateComplete;

		// Token: 0x04000B1E RID: 2846
		[MarshalAs(UnmanagedType.U1)]
		public bool m_bgdlAvailable;

		// Token: 0x04000B1F RID: 2847
		[MarshalAs(UnmanagedType.U1)]
		public bool m_bgdlComplete;

		// Token: 0x04000B20 RID: 2848
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_currentVersionStr;
	}
}
