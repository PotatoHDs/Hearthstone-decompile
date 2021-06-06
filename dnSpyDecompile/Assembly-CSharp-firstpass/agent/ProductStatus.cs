using System;
using System.Runtime.InteropServices;

namespace agent
{
	// Token: 0x020001F3 RID: 499
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class ProductStatus
	{
		// Token: 0x04000B36 RID: 2870
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_product;

		// Token: 0x04000B37 RID: 2871
		[MarshalAs(UnmanagedType.Struct)]
		public UserSettings m_settings;

		// Token: 0x04000B38 RID: 2872
		[MarshalAs(UnmanagedType.Struct)]
		public CachedState m_cachedState;

		// Token: 0x04000B39 RID: 2873
		[MarshalAs(UnmanagedType.Struct)]
		public ProductConfiguration m_configuration;
	}
}
