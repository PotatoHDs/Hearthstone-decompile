using System.Collections.Generic;

namespace bgs
{
	public class QueueSslClientConnection : SslClientConnection
	{
		private readonly Queue<BattleNetPacket> m_outQueue;

		private List<ConnectionEvent<BattleNetPacket>> m_connectionEvents;

		public bool OnlyOneSend { get; set; }

		public QueueSslClientConnection(SslCertBundleSettings bundleSettings, IFileUtil fileUtil, IJsonSerializer jsonSerializer, ISocketEventListener socketEventListener)
			: this(bundleSettings, fileUtil, jsonSerializer, socketEventListener, 262144, 262144)
		{
		}

		public QueueSslClientConnection(SslCertBundleSettings bundleSettings, IFileUtil fileUtil, IJsonSerializer jsonSerializer, ISocketEventListener socketEventListener, int receiveBufferSize, int backingBufferSize)
			: base(bundleSettings, fileUtil, jsonSerializer, socketEventListener, receiveBufferSize, backingBufferSize)
		{
			m_outQueue = new Queue<BattleNetPacket>();
			m_connectionEvents = new List<ConnectionEvent<BattleNetPacket>>();
		}

		public override void Update()
		{
			SslSocket.Process();
			lock (m_connectionEvents)
			{
				m_connectionEvents.ForEach(delegate(ConnectionEvent<BattleNetPacket> connectionEvent)
				{
					HandleConnectionEvent(connectionEvent);
				});
				m_connectionEvents.Clear();
			}
			if (m_sslSocket != null && m_connectionState == ConnectionState.Connected)
			{
				while (m_outQueue.Count > 0 && (!OnlyOneSend || m_sslSocket.m_canSend))
				{
					BattleNetPacket packet = m_outQueue.Dequeue();
					base.SendPacket(packet);
				}
			}
		}

		public override void SendPacket(BattleNetPacket packet)
		{
			m_outQueue.Enqueue(packet);
		}

		~QueueSslClientConnection()
		{
			Dispose(isDisposing: false);
		}

		protected override void OnConnectionEventFinish(ConnectionEvent<BattleNetPacket> connectionEvent)
		{
			lock (m_connectionEvents)
			{
				m_connectionEvents.Add(connectionEvent);
			}
		}
	}
}
