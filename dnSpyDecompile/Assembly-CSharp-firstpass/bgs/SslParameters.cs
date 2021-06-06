using System;

namespace bgs
{
	// Token: 0x02000261 RID: 609
	public class SslParameters
	{
		// Token: 0x06002529 RID: 9513 RVA: 0x00083A5F File Offset: 0x00081C5F
		public SslParameters()
		{
			this.bundleSettings = new SslCertBundleSettings();
		}

		// Token: 0x04000F72 RID: 3954
		public bool useSsl = true;

		// Token: 0x04000F73 RID: 3955
		public SslCertBundleSettings bundleSettings;
	}
}
