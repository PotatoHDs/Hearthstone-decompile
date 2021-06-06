using System.Runtime.InteropServices;

namespace agent
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class OverrideUrlChangedMessage
	{
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_product;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_overrideUrl;
	}
}
