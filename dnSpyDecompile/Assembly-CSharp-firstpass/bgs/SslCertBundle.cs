using System;

namespace bgs
{
	// Token: 0x02000263 RID: 611
	public class SslCertBundle
	{
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x0600252B RID: 9515 RVA: 0x00083A98 File Offset: 0x00081C98
		public bool IsUsingCertBundle
		{
			get
			{
				return this.isUsingCertBundle;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x00083AA0 File Offset: 0x00081CA0
		// (set) Token: 0x0600252D RID: 9517 RVA: 0x00083AA8 File Offset: 0x00081CA8
		public byte[] CertBundleBytes
		{
			get
			{
				return this.certBundleBytes;
			}
			set
			{
				this.certBundleBytes = value;
				this.isUsingCertBundle = (this.certBundleBytes != null);
			}
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x00083AC0 File Offset: 0x00081CC0
		public SslCertBundle(byte[] certBundleBytes)
		{
			this.CertBundleBytes = certBundleBytes;
		}

		// Token: 0x04000F76 RID: 3958
		private bool isUsingCertBundle;

		// Token: 0x04000F77 RID: 3959
		public bool isCertBundleSigned = true;

		// Token: 0x04000F78 RID: 3960
		private byte[] certBundleBytes;
	}
}
