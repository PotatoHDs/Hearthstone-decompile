using System;
using System.Collections.Generic;

namespace bgs
{
	// Token: 0x0200022F RID: 559
	public class QueueSslClientConnection : SslClientConnection
	{
		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06002360 RID: 9056 RVA: 0x0007C002 File Offset: 0x0007A202
		// (set) Token: 0x06002361 RID: 9057 RVA: 0x0007C00A File Offset: 0x0007A20A
		public bool OnlyOneSend { get; set; }

		// Token: 0x06002362 RID: 9058 RVA: 0x0007C013 File Offset: 0x0007A213
		public QueueSslClientConnection(SslCertBundleSettings bundleSettings, IFileUtil fileUtil, IJsonSerializer jsonSerializer, ISocketEventListener socketEventListener) : this(bundleSettings, fileUtil, jsonSerializer, socketEventListener, 262144, 262144)
		{
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x0007C02A File Offset: 0x0007A22A
		public QueueSslClientConnection(SslCertBundleSettings bundleSettings, IFileUtil fileUtil, IJsonSerializer jsonSerializer, ISocketEventListener socketEventListener, int receiveBufferSize, int backingBufferSize) : base(bundleSettings, fileUtil, jsonSerializer, socketEventListener, receiveBufferSize, backingBufferSize)
		{
			this.m_outQueue = new Queue<BattleNetPacket>();
			this.m_connectionEvents = new List<ConnectionEvent<BattleNetPacket>>();
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0007C054 File Offset: 0x0007A254
		public override void Update()
		{
			SslSocket.Process();
			List<ConnectionEvent<BattleNetPacket>> connectionEvents = this.m_connectionEvents;
			lock (connectionEvents)
			{
				this.m_connectionEvents.ForEach(delegate(ConnectionEvent<BattleNetPacket> connectionEvent)
				{
					base.HandleConnectionEvent(connectionEvent);
				});
				this.m_connectionEvents.Clear();
			}
			if (this.m_sslSocket == null || this.m_connectionState != ConnectionState.Connected)
			{
				return;
			}
			while (this.m_outQueue.Count > 0)
			{
				if (this.OnlyOneSend && !this.m_sslSocket.m_canSend)
				{
					return;
				}
				BattleNetPacket packet = this.m_outQueue.Dequeue();
				base.SendPacket(packet);
			}
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x0007C100 File Offset: 0x0007A300
		public override void SendPacket(BattleNetPacket packet)
		{
			this.m_outQueue.Enqueue(packet);
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x0007C110 File Offset: 0x0007A310
		~QueueSslClientConnection()
		{
			this.Dispose(false);
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x0007C140 File Offset: 0x0007A340
		protected override void OnConnectionEventFinish(ConnectionEvent<BattleNetPacket> connectionEvent)
		{
			List<ConnectionEvent<BattleNetPacket>> connectionEvents = this.m_connectionEvents;
			lock (connectionEvents)
			{
				this.m_connectionEvents.Add(connectionEvent);
			}
		}

		// Token: 0x04000E7F RID: 3711
		private readonly Queue<BattleNetPacket> m_outQueue;

		// Token: 0x04000E80 RID: 3712
		private List<ConnectionEvent<BattleNetPacket>> m_connectionEvents;
	}
}
