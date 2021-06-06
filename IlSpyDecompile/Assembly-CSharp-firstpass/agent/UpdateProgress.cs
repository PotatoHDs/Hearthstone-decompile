using System.Runtime.InteropServices;

namespace agent
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct UpdateProgress
	{
		public const int UPDATE_STAGES = 3;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public double[] m_thresholds;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public long[] m_downloadCurrent;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public long[] m_downloadTotal;

		[MarshalAs(UnmanagedType.LPStr)]
		public string m_decryptionKey;

		[MarshalAs(UnmanagedType.Struct)]
		public DownloadDetails m_downloadDetails;

		[MarshalAs(UnmanagedType.Struct)]
		public ProgressDetails m_progressDetails;
	}
}
