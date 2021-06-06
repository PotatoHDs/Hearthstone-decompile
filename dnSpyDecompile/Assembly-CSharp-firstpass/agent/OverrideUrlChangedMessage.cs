using System;
using System.Runtime.InteropServices;

namespace agent
{
	// Token: 0x020001F5 RID: 501
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class OverrideUrlChangedMessage
	{
		// Token: 0x04000B3E RID: 2878
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_product;

		// Token: 0x04000B3F RID: 2879
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_overrideUrl;
	}
}
