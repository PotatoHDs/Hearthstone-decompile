namespace bgs
{
	public class SslCertBundle
	{
		private bool isUsingCertBundle;

		public bool isCertBundleSigned = true;

		private byte[] certBundleBytes;

		public bool IsUsingCertBundle => isUsingCertBundle;

		public byte[] CertBundleBytes
		{
			get
			{
				return certBundleBytes;
			}
			set
			{
				certBundleBytes = value;
				isUsingCertBundle = certBundleBytes != null;
			}
		}

		public SslCertBundle(byte[] certBundleBytes)
		{
			CertBundleBytes = certBundleBytes;
		}
	}
}
