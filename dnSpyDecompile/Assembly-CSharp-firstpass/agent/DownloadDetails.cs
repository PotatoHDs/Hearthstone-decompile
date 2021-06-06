using System;
using System.Runtime.InteropServices;

namespace agent
{
	// Token: 0x020001EB RID: 491
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct DownloadDetails
	{
		// Token: 0x04000B07 RID: 2823
		public double m_downloadRate;

		// Token: 0x04000B08 RID: 2824
		public long m_totalBytesDownloaded;

		// Token: 0x04000B09 RID: 2825
		public long m_totalBytesToDownload;

		// Token: 0x04000B0A RID: 2826
		public long m_downloadLimit;

		// Token: 0x04000B0B RID: 2827
		public long m_realDownloadedBytes;

		// Token: 0x04000B0C RID: 2828
		public long m_expectedDownloadBytes;

		// Token: 0x04000B0D RID: 2829
		public long m_expectedOriginalBytes;
	}
}
