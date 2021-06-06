using System.Runtime.InteropServices;

namespace agent
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class ProductStatus
	{
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_product;

		[MarshalAs(UnmanagedType.Struct)]
		public UserSettings m_settings;

		[MarshalAs(UnmanagedType.Struct)]
		public CachedState m_cachedState;

		[MarshalAs(UnmanagedType.Struct)]
		public ProductConfiguration m_configuration;
	}
}
