using System;
using System.Collections.Generic;
using bnet.protocol;
using bnet.protocol.notification.v1;

namespace bgs
{
	// Token: 0x020001FE RID: 510
	public class BroadcastAPI : BattleNetAPI
	{
		// Token: 0x06001F36 RID: 7990 RVA: 0x0006D1B3 File Offset: 0x0006B3B3
		public BroadcastAPI(BattleNetCSharp battlenet) : base(battlenet, "Broadcast")
		{
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x0006D1CC File Offset: 0x0006B3CC
		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x0006C9FD File Offset: 0x0006ABFD
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x0006BFB5 File Offset: 0x0006A1B5
		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x0006D1D5 File Offset: 0x0006B3D5
		public void RegisterListener(BroadcastAPI.BroadcastCallback cb)
		{
			if (this.m_listenerList.Contains(cb))
			{
				return;
			}
			this.m_listenerList.Add(cb);
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0006D1F4 File Offset: 0x0006B3F4
		public void OnBroadcast(Notification notification)
		{
			foreach (BroadcastAPI.BroadcastCallback broadcastCallback in this.m_listenerList)
			{
				broadcastCallback(notification.AttributeList);
			}
		}

		// Token: 0x04000B5F RID: 2911
		private List<BroadcastAPI.BroadcastCallback> m_listenerList = new List<BroadcastAPI.BroadcastCallback>();

		// Token: 0x02000682 RID: 1666
		// (Invoke) Token: 0x060061EF RID: 25071
		public delegate void BroadcastCallback(IList<bnet.protocol.Attribute> AttributeList);
	}
}
