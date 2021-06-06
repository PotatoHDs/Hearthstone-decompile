using System;

namespace bgs
{
	// Token: 0x02000262 RID: 610
	public class SslCertBundleSettings
	{
		// Token: 0x0600252A RID: 9514 RVA: 0x00083A79 File Offset: 0x00081C79
		public SslCertBundleSettings()
		{
			this.bundle = new SslCertBundle(null);
			this.bundleDownloadConfig = new UrlDownloaderConfig();
		}

		// Token: 0x04000F74 RID: 3956
		public SslCertBundle bundle;

		// Token: 0x04000F75 RID: 3957
		public UrlDownloaderConfig bundleDownloadConfig;
	}
}
