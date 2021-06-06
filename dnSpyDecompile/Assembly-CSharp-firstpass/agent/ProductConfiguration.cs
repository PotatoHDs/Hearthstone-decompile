using System;
using System.Runtime.InteropServices;

namespace agent
{
	// Token: 0x020001F2 RID: 498
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ProductConfiguration
	{
		// Token: 0x04000B2E RID: 2862
		public int m_updateMethod;

		// Token: 0x04000B2F RID: 2863
		public int m_versionCooldown;

		// Token: 0x04000B30 RID: 2864
		public long m_tolerantIncreasedBytes;

		// Token: 0x04000B31 RID: 2865
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_relativeDataPath;

		// Token: 0x04000B32 RID: 2866
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_lastVersionCheck;

		// Token: 0x04000B33 RID: 2867
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_apkFilePath;

		// Token: 0x04000B34 RID: 2868
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_liveDisplayVersion;

		// Token: 0x04000B35 RID: 2869
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_availableLanguages;
	}
}
