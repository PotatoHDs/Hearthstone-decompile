using System.Runtime.InteropServices;

namespace agent
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ProductConfiguration
	{
		public int m_updateMethod;

		public int m_versionCooldown;

		public long m_tolerantIncreasedBytes;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_relativeDataPath;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_lastVersionCheck;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_apkFilePath;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_liveDisplayVersion;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_availableLanguages;
	}
}
