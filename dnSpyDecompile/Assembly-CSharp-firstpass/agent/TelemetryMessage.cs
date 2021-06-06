using System;
using System.Runtime.InteropServices;

namespace agent
{
	// Token: 0x020001F4 RID: 500
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class TelemetryMessage
	{
		// Token: 0x04000B3A RID: 2874
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_messageName;

		// Token: 0x04000B3B RID: 2875
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_packageName;

		// Token: 0x04000B3C RID: 2876
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_component;

		// Token: 0x04000B3D RID: 2877
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10000)]
		public byte[] m_payload;
	}
}
