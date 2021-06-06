namespace bgs
{
	public interface IUrlDownloader
	{
		void Process();

		void Download(string url, UrlDownloadCompletedCallback cb);

		void Download(string url, UrlDownloadCompletedCallback cb, UrlDownloaderConfig config);
	}
}
