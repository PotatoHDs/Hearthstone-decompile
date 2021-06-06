using System;
using System.Net.Sockets;

namespace bgs
{
	// Token: 0x02000205 RID: 517
	internal class LocalStorageFileState
	{
		// Token: 0x06001FDB RID: 8155 RVA: 0x00070B14 File Offset: 0x0006ED14
		public LocalStorageFileState(int id)
		{
			this.m_ID = id;
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x00070B2E File Offset: 0x0006ED2E
		public Socket Socket
		{
			get
			{
				return this.Connection.Socket;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x00070B3B File Offset: 0x0006ED3B
		// (set) Token: 0x06001FDE RID: 8158 RVA: 0x00070B43 File Offset: 0x0006ED43
		public string FailureMessage { get; set; }

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x00070B4C File Offset: 0x0006ED4C
		// (set) Token: 0x06001FE0 RID: 8160 RVA: 0x00070B54 File Offset: 0x0006ED54
		public LocalStorageAPI.DownloadCompletedCallback Callback { get; set; }

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x00070B5D File Offset: 0x0006ED5D
		public int ID
		{
			get
			{
				return this.m_ID;
			}
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x00070B68 File Offset: 0x0006ED68
		public override string ToString()
		{
			return string.Format("[Region={0} Usage={1} SHA256={2} ID={3}]", new object[]
			{
				this.CH.Region,
				this.CH.Usage,
				this.CH.Sha256Digest,
				this.m_ID
			});
		}

		// Token: 0x04000B87 RID: 2951
		public byte[] ReceiveBuffer;

		// Token: 0x04000B88 RID: 2952
		public ContentHandle CH;

		// Token: 0x04000B89 RID: 2953
		public TcpConnection Connection = new TcpConnection();

		// Token: 0x04000B8A RID: 2954
		public byte[] FileData;

		// Token: 0x04000B8B RID: 2955
		public byte[] CompressedData;

		// Token: 0x04000B8E RID: 2958
		public object UserContext;

		// Token: 0x04000B8F RID: 2959
		private int m_ID;
	}
}
