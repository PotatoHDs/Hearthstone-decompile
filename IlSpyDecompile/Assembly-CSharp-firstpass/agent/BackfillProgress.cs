using System.Runtime.InteropServices;

namespace agent
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct BackfillProgress
	{
		[MarshalAs(UnmanagedType.Struct)]
		public DownloadDetails m_downloadDetails;

		[MarshalAs(UnmanagedType.Struct)]
		public ProgressDetails m_progressDetails;

		[MarshalAs(UnmanagedType.U1)]
		public bool m_isBgdl;

		[MarshalAs(UnmanagedType.U1)]
		public bool m_isPaused;
	}
}
