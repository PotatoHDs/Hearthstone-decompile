using System.Runtime.InteropServices;

namespace agent
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct BaseState
	{
		[MarshalAs(UnmanagedType.U1)]
		public bool m_playable;

		[MarshalAs(UnmanagedType.U1)]
		public bool m_installed;

		[MarshalAs(UnmanagedType.U1)]
		public bool m_updateComplete;

		[MarshalAs(UnmanagedType.U1)]
		public bool m_bgdlAvailable;

		[MarshalAs(UnmanagedType.U1)]
		public bool m_bgdlComplete;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_currentVersionStr;
	}
}
