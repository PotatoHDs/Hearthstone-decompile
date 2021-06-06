using System;
using System.Runtime.InteropServices;

namespace agent
{
	// Token: 0x020001EC RID: 492
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct UpdateProgress
	{
		// Token: 0x04000B0E RID: 2830
		public const int UPDATE_STAGES = 3;

		// Token: 0x04000B0F RID: 2831
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public double[] m_thresholds;

		// Token: 0x04000B10 RID: 2832
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public long[] m_downloadCurrent;

		// Token: 0x04000B11 RID: 2833
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public long[] m_downloadTotal;

		// Token: 0x04000B12 RID: 2834
		[MarshalAs(UnmanagedType.LPStr)]
		public string m_decryptionKey;

		// Token: 0x04000B13 RID: 2835
		[MarshalAs(UnmanagedType.Struct)]
		public DownloadDetails m_downloadDetails;

		// Token: 0x04000B14 RID: 2836
		[MarshalAs(UnmanagedType.Struct)]
		public ProgressDetails m_progressDetails;
	}
}
