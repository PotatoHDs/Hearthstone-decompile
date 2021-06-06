using System.Collections.Generic;
using System.Net.Sockets;

namespace bgs
{
	public class QueueClientConnection<PacketType> : ClientConnection<PacketType> where PacketType : PacketFormat, new()
	{
		private readonly Queue<PacketType> m_outQueue;

		private object m_mutexConnectionEvents = new object();

		private readonly List<ConnectionEvent<PacketType>> m_connectionEvents;

		private const double MAX_SOCKET_DISCONNECTED_TIME_WITHOUT_BEING_NOTICED = 5.0;

		private double m_timeSocketDisconnected;

		public QueueClientConnection(ISocketEventListener socketEventListener)
			: this(socketEventListener, 65536, 262144)
		{
		}

		public QueueClientConnection(ISocketEventListener socketEventListener, int receiveBufferSize, int backingBufferSize)
			: base(socketEventListener, receiveBufferSize, backingBufferSize)
		{
			m_outQueue = new Queue<PacketType>();
			m_connectionEvents = new List<ConnectionEvent<PacketType>>();
		}

		public QueueClientConnection(Socket socket)
			: base(socket)
		{
			m_outQueue = new Queue<PacketType>();
			m_connectionEvents = new List<ConnectionEvent<PacketType>>();
		}

		~QueueClientConnection()
		{
			Dispose(isDisposing: false);
		}

		public override bool HasEvents()
		{
			return m_connectionEvents.Count > 0;
		}

		public override void Connect(string host, uint port, int tryCount)
		{
			m_outQueue.Clear();
			base.Connect(host, port, tryCount);
		}

		public override void SendPacket(PacketType packet)
		{
			m_outQueue.Enqueue(packet);
		}

		private void SendQueuedPackets()
		{
			while (m_outQueue.Count > 0)
			{
				PacketType packet = m_outQueue.Dequeue();
				base.SendPacket(packet);
			}
		}

		public override void Update()
		{
			CheckSocketStatus();
			lock (m_mutexConnectionEvents)
			{
				m_connectionEvents.ForEach(delegate(ConnectionEvent<PacketType> connectionEvent)
				{
					PrintConnectionException(connectionEvent);
					HandleConnectionEvent(connectionEvent);
				});
				m_connectionEvents.Clear();
			}
			if (m_connectionState == ConnectionState.Connected)
			{
				SendQueuedPackets();
			}
		}

		private void CheckSocketStatus()
		{
			if ((m_socket == null || !m_socket.Connected) && m_connectionState == ConnectionState.Connected)
			{
				double totalSeconds = TimeUtils.GetElapsedTimeSinceEpoch().TotalSeconds;
				if (m_timeSocketDisconnected == 0.0)
				{
					m_timeSocketDisconnected = totalSeconds;
				}
				else if (totalSeconds - m_timeSocketDisconnected > 5.0)
				{
					m_timeSocketDisconnected = 0.0;
					AddDisconnectEvent(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
				}
			}
			else
			{
				m_timeSocketDisconnected = 0.0;
			}
		}

		protected override void OnConnectionEventFinished(ConnectionEvent<PacketType> connectionEvent)
		{
			lock (m_mutexConnectionEvents)
			{
				m_connectionEvents.Add(connectionEvent);
			}
		}

		private void PrintConnectionException(ConnectionEvent<PacketType> connectionEvent)
		{
			if (connectionEvent.Exception != null)
			{
				LogAdapter.Log(LogLevel.Error, $"ClientConnection Exception - {connectionEvent.Exception.Message} - {m_connection.Host}:{m_connection.Port}\n{connectionEvent.Exception.StackTrace}");
			}
		}
	}
}
