using System.Runtime.InteropServices;

namespace agent
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct DownloadDetails
	{
		public double m_downloadRate;

		public long m_totalBytesDownloaded;

		public long m_totalBytesToDownload;

		public long m_downloadLimit;

		public long m_realDownloadedBytes;

		public long m_expectedDownloadBytes;

		public long m_expectedOriginalBytes;
	}
}
