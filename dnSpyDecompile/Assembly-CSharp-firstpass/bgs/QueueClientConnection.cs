using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace bgs
{
	// Token: 0x0200022E RID: 558
	public class QueueClientConnection<PacketType> : ClientConnection<PacketType> where PacketType : PacketFormat, new()
	{
		// Token: 0x06002353 RID: 9043 RVA: 0x0007BD29 File Offset: 0x00079F29
		public QueueClientConnection(ISocketEventListener socketEventListener) : this(socketEventListener, 65536, 262144)
		{
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x0007BD3C File Offset: 0x00079F3C
		public QueueClientConnection(ISocketEventListener socketEventListener, int receiveBufferSize, int backingBufferSize)
		{
			this.m_mutexConnectionEvents = new object();
			base..ctor(socketEventListener, receiveBufferSize, backingBufferSize);
			this.m_outQueue = new Queue<PacketType>();
			this.m_connectionEvents = new List<ConnectionEvent<PacketType>>();
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x0007BD68 File Offset: 0x00079F68
		public QueueClientConnection(Socket socket)
		{
			this.m_mutexConnectionEvents = new object();
			base..ctor(socket);
			this.m_outQueue = new Queue<PacketType>();
			this.m_connectionEvents = new List<ConnectionEvent<PacketType>>();
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x0007BD94 File Offset: 0x00079F94
		~QueueClientConnection()
		{
			this.Dispose(false);
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x0007BDC4 File Offset: 0x00079FC4
		public override bool HasEvents()
		{
			return this.m_connectionEvents.Count > 0;
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x0007BDD4 File Offset: 0x00079FD4
		public override void Connect(string host, uint port, int tryCount)
		{
			this.m_outQueue.Clear();
			base.Connect(host, port, tryCount);
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x0007BDEA File Offset: 0x00079FEA
		public override void SendPacket(PacketType packet)
		{
			this.m_outQueue.Enqueue(packet);
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x0007BDF8 File Offset: 0x00079FF8
		private void SendQueuedPackets()
		{
			while (this.m_outQueue.Count > 0)
			{
				PacketType packet = this.m_outQueue.Dequeue();
				base.SendPacket(packet);
			}
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x0007BE28 File Offset: 0x0007A028
		public override void Update()
		{
			this.CheckSocketStatus();
			object mutexConnectionEvents = this.m_mutexConnectionEvents;
			lock (mutexConnectionEvents)
			{
				this.m_connectionEvents.ForEach(delegate(ConnectionEvent<PacketType> connectionEvent)
				{
					this.PrintConnectionException(connectionEvent);
					this.HandleConnectionEvent(connectionEvent);
				});
				this.m_connectionEvents.Clear();
			}
			if (this.m_connectionState != ConnectionState.Connected)
			{
				return;
			}
			this.SendQueuedPackets();
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x0007BE9C File Offset: 0x0007A09C
		private void CheckSocketStatus()
		{
			if ((this.m_socket == null || !this.m_socket.Connected) && this.m_connectionState == ConnectionState.Connected)
			{
				double totalSeconds = TimeUtils.GetElapsedTimeSinceEpoch(null).TotalSeconds;
				if (this.m_timeSocketDisconnected == 0.0)
				{
					this.m_timeSocketDisconnected = totalSeconds;
					return;
				}
				if (totalSeconds - this.m_timeSocketDisconnected > 5.0)
				{
					this.m_timeSocketDisconnected = 0.0;
					base.AddDisconnectEvent(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
					return;
				}
			}
			else
			{
				this.m_timeSocketDisconnected = 0.0;
			}
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x0007BF3C File Offset: 0x0007A13C
		protected override void OnConnectionEventFinished(ConnectionEvent<PacketType> connectionEvent)
		{
			object mutexConnectionEvents = this.m_mutexConnectionEvents;
			lock (mutexConnectionEvents)
			{
				this.m_connectionEvents.Add(connectionEvent);
			}
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x0007BF84 File Offset: 0x0007A184
		private void PrintConnectionException(ConnectionEvent<PacketType> connectionEvent)
		{
			if (connectionEvent.Exception == null)
			{
				return;
			}
			LogAdapter.Log(LogLevel.Error, string.Format("ClientConnection Exception - {0} - {1}:{2}\n{3}", new object[]
			{
				connectionEvent.Exception.Message,
				this.m_connection.Host,
				this.m_connection.Port,
				connectionEvent.Exception.StackTrace
			}), "");
		}

		// Token: 0x04000E7A RID: 3706
		private readonly Queue<PacketType> m_outQueue;

		// Token: 0x04000E7B RID: 3707
		private object m_mutexConnectionEvents;

		// Token: 0x04000E7C RID: 3708
		private readonly List<ConnectionEvent<PacketType>> m_connectionEvents;

		// Token: 0x04000E7D RID: 3709
		private const double MAX_SOCKET_DISCONNECTED_TIME_WITHOUT_BEING_NOTICED = 5.0;

		// Token: 0x04000E7E RID: 3710
		private double m_timeSocketDisconnected;
	}
}
