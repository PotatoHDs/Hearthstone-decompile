using System.Runtime.InteropServices;

namespace agent
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class TelemetryMessage
	{
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_messageName;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_packageName;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_component;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10000)]
		public byte[] m_payload;
	}
}
