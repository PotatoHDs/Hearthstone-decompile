using System;

namespace bgs
{
	// Token: 0x02000260 RID: 608
	public interface IUrlDownloader
	{
		// Token: 0x06002526 RID: 9510
		void Process();

		// Token: 0x06002527 RID: 9511
		void Download(string url, UrlDownloadCompletedCallback cb);

		// Token: 0x06002528 RID: 9512
		void Download(string url, UrlDownloadCompletedCallback cb, UrlDownloaderConfig config);
	}
}
