using System;

namespace bgs
{
	// Token: 0x0200021D RID: 541
	public class ConnectionEvent<T> where T : PacketFormat
	{
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060022ED RID: 8941 RVA: 0x0007B40A File Offset: 0x0007960A
		// (set) Token: 0x060022EE RID: 8942 RVA: 0x0007B412 File Offset: 0x00079612
		public ConnectionEventTypes Type { get; set; }

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x0007B41B File Offset: 0x0007961B
		// (set) Token: 0x060022F0 RID: 8944 RVA: 0x0007B423 File Offset: 0x00079623
		public BattleNetErrors Error { get; set; }

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060022F1 RID: 8945 RVA: 0x0007B42C File Offset: 0x0007962C
		// (set) Token: 0x060022F2 RID: 8946 RVA: 0x0007B434 File Offset: 0x00079634
		public T Packet { get; set; }

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x0007B43D File Offset: 0x0007963D
		// (set) Token: 0x060022F4 RID: 8948 RVA: 0x0007B445 File Offset: 0x00079645
		public Exception Exception { get; set; }
	}
}
