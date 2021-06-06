namespace bgs
{
	public class SslCertBundleSettings
	{
		public SslCertBundle bundle;

		public UrlDownloaderConfig bundleDownloadConfig;

		public SslCertBundleSettings()
		{
			bundle = new SslCertBundle(null);
			bundleDownloadConfig = new UrlDownloaderConfig();
		}
	}
}
