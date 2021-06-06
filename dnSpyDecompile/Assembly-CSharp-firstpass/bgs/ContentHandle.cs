using System;
using System.Text;
using bnet.protocol;

namespace bgs
{
	// Token: 0x02000204 RID: 516
	public class ContentHandle
	{
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x00070A1E File Offset: 0x0006EC1E
		// (set) Token: 0x06001FD2 RID: 8146 RVA: 0x00070A26 File Offset: 0x0006EC26
		public string Region { get; set; }

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x00070A2F File Offset: 0x0006EC2F
		// (set) Token: 0x06001FD4 RID: 8148 RVA: 0x00070A37 File Offset: 0x0006EC37
		public string Usage { get; set; }

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x00070A40 File Offset: 0x0006EC40
		// (set) Token: 0x06001FD6 RID: 8150 RVA: 0x00070A48 File Offset: 0x0006EC48
		public string Sha256Digest { get; set; }

		// Token: 0x06001FD7 RID: 8151 RVA: 0x00070A54 File Offset: 0x0006EC54
		public static ContentHandle FromProtocol(ContentHandle contentHandle)
		{
			if (contentHandle == null || !contentHandle.IsInitialized)
			{
				return null;
			}
			return new ContentHandle
			{
				Region = new FourCC(contentHandle.Region).ToString(),
				Usage = new FourCC(contentHandle.Usage).ToString(),
				Sha256Digest = ContentHandle.ByteArrayToString(contentHandle.Hash)
			};
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x00070AB0 File Offset: 0x0006ECB0
		public override string ToString()
		{
			return string.Format("Region={0} Usage={1} Sha256={2}", this.Region, this.Usage, this.Sha256Digest);
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x00070AD0 File Offset: 0x0006ECD0
		public static string ByteArrayToString(byte[] ba)
		{
			StringBuilder stringBuilder = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}
	}
}
