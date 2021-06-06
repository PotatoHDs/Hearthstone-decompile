namespace bgs
{
	public class SslParameters
	{
		public bool useSsl = true;

		public SslCertBundleSettings bundleSettings;

		public SslParameters()
		{
			bundleSettings = new SslCertBundleSettings();
		}
	}
}
