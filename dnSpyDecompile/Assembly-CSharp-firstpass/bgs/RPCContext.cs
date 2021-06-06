using System;
using bnet.protocol;

namespace bgs
{
	// Token: 0x02000236 RID: 566
	public class RPCContext
	{
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x0007DFC5 File Offset: 0x0007C1C5
		// (set) Token: 0x060023A3 RID: 9123 RVA: 0x0007DFCD File Offset: 0x0007C1CD
		public Header Header
		{
			get
			{
				return this.header;
			}
			set
			{
				this.header = value;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x0007DFD6 File Offset: 0x0007C1D6
		// (set) Token: 0x060023A5 RID: 9125 RVA: 0x0007DFDE File Offset: 0x0007C1DE
		public byte[] Payload
		{
			get
			{
				return this.payload;
			}
			set
			{
				this.payload = value;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x0007DFE7 File Offset: 0x0007C1E7
		// (set) Token: 0x060023A7 RID: 9127 RVA: 0x0007DFEF File Offset: 0x0007C1EF
		public RPCContextDelegate Callback
		{
			get
			{
				return this.callback;
			}
			set
			{
				this.callback = value;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x0007DFF8 File Offset: 0x0007C1F8
		// (set) Token: 0x060023A9 RID: 9129 RVA: 0x0007E000 File Offset: 0x0007C200
		public bool ResponseReceived
		{
			get
			{
				return this.responseReceived;
			}
			set
			{
				this.responseReceived = value;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x0007E009 File Offset: 0x0007C209
		// (set) Token: 0x060023AB RID: 9131 RVA: 0x0007E011 File Offset: 0x0007C211
		public IProtoBuf Request { get; set; }

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x0007E01A File Offset: 0x0007C21A
		// (set) Token: 0x060023AD RID: 9133 RVA: 0x0007E022 File Offset: 0x0007C222
		public int PacketId { get; set; }

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x0007E02B File Offset: 0x0007C22B
		// (set) Token: 0x060023AF RID: 9135 RVA: 0x0007E033 File Offset: 0x0007C233
		public UtilSystemId SystemId { get; set; }

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060023B0 RID: 9136 RVA: 0x0007E03C File Offset: 0x0007C23C
		// (set) Token: 0x060023B1 RID: 9137 RVA: 0x0007E044 File Offset: 0x0007C244
		public int Context { get; set; }

		// Token: 0x04000E97 RID: 3735
		private Header header;

		// Token: 0x04000E98 RID: 3736
		private byte[] payload;

		// Token: 0x04000E99 RID: 3737
		private RPCContextDelegate callback;

		// Token: 0x04000E9A RID: 3738
		private bool responseReceived;
	}
}
