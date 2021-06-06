using System;
using System.Runtime.InteropServices;

namespace agent
{
	// Token: 0x020001F1 RID: 497
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct UserSettings
	{
		// Token: 0x04000B26 RID: 2854
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_languages;

		// Token: 0x04000B27 RID: 2855
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_region;

		// Token: 0x04000B28 RID: 2856
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_branch;

		// Token: 0x04000B29 RID: 2857
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_additionalTags;

		// Token: 0x04000B2A RID: 2858
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_accountCountry;

		// Token: 0x04000B2B RID: 2859
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_geoIpCountry;

		// Token: 0x04000B2C RID: 2860
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_selectedTextLocale;

		// Token: 0x04000B2D RID: 2861
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_selectedAudioLocale;
	}
}
