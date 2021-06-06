using System.Runtime.InteropServices;

namespace agent
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct UserSettings
	{
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_languages;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_region;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_branch;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_additionalTags;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_accountCountry;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_geoIpCountry;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_selectedTextLocale;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_selectedAudioLocale;
	}
}
