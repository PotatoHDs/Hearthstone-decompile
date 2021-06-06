using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace bgs
{
	// Token: 0x02000219 RID: 537
	public class ClientConnection<PacketType> : IDisposable, IClientConnection<PacketType> where PacketType : PacketFormat, new()
	{
		// Token: 0x060022BE RID: 8894 RVA: 0x0007A700 File Offset: 0x00078900
		public ClientConnection(ISocketEventListener socketEventListener, int receiveBufferSize, int backingBufferSize)
		{
			this.m_socketEventListener = socketEventListener;
			this.m_connectionState = ConnectionState.Disconnected;
			this.m_receiveBuffer = new byte[receiveBufferSize];
			this.m_backingBuffer = new byte[backingBufferSize];
			this.m_backingBufferSize = backingBufferSize;
			this.m_stolenSocket = false;
			this.isDisposed = false;
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x0007A788 File Offset: 0x00078988
		public ClientConnection(Socket socket)
		{
			this.m_socket = socket;
			this.m_connectionState = ConnectionState.Connected;
			this.m_receiveBuffer = new byte[65536];
			this.m_stolenSocket = true;
			this.isDisposed = false;
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x0007A800 File Offset: 0x00078A00
		~ClientConnection()
		{
			this.Dispose(false);
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x0007A830 File Offset: 0x00078A30
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0007A83F File Offset: 0x00078A3F
		public bool AddConnectHandler(ConnectHandler handler)
		{
			if (this.m_connectHandlers.Contains(handler))
			{
				return false;
			}
			this.m_connectHandlers.Add(handler);
			return true;
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0007A85E File Offset: 0x00078A5E
		public bool RemoveConnectHandler(ConnectHandler handler)
		{
			return this.m_connectHandlers.Remove(handler);
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x0007A86C File Offset: 0x00078A6C
		public bool AddDisconnectHandler(DisconnectHandler handler)
		{
			if (this.m_disconnectHandlers.Contains(handler))
			{
				return false;
			}
			this.m_disconnectHandlers.Add(handler);
			return true;
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0007A88B File Offset: 0x00078A8B
		public bool RemoveDisconnectHandler(DisconnectHandler handler)
		{
			return this.m_disconnectHandlers.Remove(handler);
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x00003D71 File Offset: 0x00001F71
		public virtual bool HasEvents()
		{
			return false;
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x0007A899 File Offset: 0x00078A99
		private void AddConnectEvent(BattleNetErrors error, Exception exception = null)
		{
			this.OnConnectionEventFinished(new ConnectionEvent<PacketType>
			{
				Type = ConnectionEventTypes.OnConnected,
				Error = error,
				Exception = exception
			});
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x0007A8BB File Offset: 0x00078ABB
		protected void AddDisconnectEvent(BattleNetErrors error)
		{
			this.OnConnectionEventFinished(new ConnectionEvent<PacketType>
			{
				Type = ConnectionEventTypes.OnDisconnected,
				Error = error
			});
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x0007A8D6 File Offset: 0x00078AD6
		public bool Active
		{
			get
			{
				return this.m_connectionState == ConnectionState.Connecting || this.m_connectionState == ConnectionState.Connected;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x0007A8EC File Offset: 0x00078AEC
		public ConnectionState State
		{
			get
			{
				return this.m_connectionState;
			}
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x0007A8F4 File Offset: 0x00078AF4
		private void CleanBuffer()
		{
			Array.Clear(this.m_receiveBuffer, 0, this.m_receiveBuffer.Length);
			Array.Clear(this.m_backingBuffer, 0, this.m_backingBuffer.Length);
			this.m_backingBufferBytes = 0;
			this.m_currentPacket = default(PacketType);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x0007A934 File Offset: 0x00078B34
		public virtual void Connect(string host, uint port, int tryCount)
		{
			this.CleanBuffer();
			this.m_connection.LogDebug = delegate(string log)
			{
				LogAdapter.Log(LogLevel.Debug, log, "");
			};
			this.m_connection.LogWarning = delegate(string log)
			{
				LogAdapter.Log(LogLevel.Warning, log, "");
			};
			this.m_connection.OnFailure = delegate()
			{
				this.m_connectionState = ConnectionState.ConnectionFailed;
				this.AddConnectEvent(BattleNetErrors.ERROR_RPC_PEER_UNKNOWN, null);
			};
			this.m_connection.OnSuccess = new Action(this.ConnectCallback);
			this.m_connectionState = ConnectionState.Connecting;
			LogAdapter.LogInfo("Socket Connecting...", "ClientConnection.Connect");
			this.m_connection.Connect(host, port, tryCount);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x0007A9F0 File Offset: 0x00078BF0
		private void ConnectCallback()
		{
			Exception ex = null;
			this.m_socket = this.m_connection.Socket;
			LogAdapter.LogInfo("Socket Connected", "ClientConnection.ConnectCallback");
			try
			{
				this.m_socket.BeginReceive(this.m_receiveBuffer, 0, this.m_receiveBuffer.Length, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), null);
			}
			catch (Exception ex)
			{
			}
			if (ex != null || !this.m_socket.Connected)
			{
				LogAdapter.Log(LogLevel.Warning, string.Format("ClientConnection - BeginReceive() failed. ip:{0}, port:{1}", this.m_connection.Host, this.m_connection.Port), "");
				this.DisconnectSocket();
				this.m_connectionState = ConnectionState.ConnectionFailed;
				this.AddConnectEvent(BattleNetErrors.ERROR_RPC_PEER_UNAVAILABLE, ex);
				return;
			}
			this.AddConnectEvent(BattleNetErrors.ERROR_OK, null);
			if (this.m_socketEventListener != null)
			{
				this.m_socketEventListener.ConnectEvent(this.m_connection.Host, this.m_connection.Port);
			}
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x0007AAE8 File Offset: 0x00078CE8
		public void Disconnect()
		{
			if (this.m_socketEventListener != null)
			{
				this.m_socketEventListener.DisconnectEvent(this.m_connection.Host, this.m_connection.Port);
			}
			this.DisconnectSocket();
			this.m_connectionState = ConnectionState.Disconnected;
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x0007AB20 File Offset: 0x00078D20
		private void DisconnectSocket()
		{
			if (this.m_socket == null)
			{
				return;
			}
			try
			{
				if (this.m_socket.Connected)
				{
					this.m_socket.Shutdown(SocketShutdown.Both);
					this.m_socket.Close();
				}
			}
			catch (SocketException ex)
			{
				LogAdapter.LogException(string.Format("{0}  socket error code: {1}", ex.Message, ex.ErrorCode), "ClientConnection.DisconnectSocket");
			}
			catch (Exception ex2)
			{
				LogAdapter.LogException(ex2.Message, "ClientConnection.DisconnectSocket");
			}
			LogAdapter.LogInfo("Socket Disconnected", "ClientConnection.DisconnectSocket");
			this.m_socket = null;
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x0007ABC8 File Offset: 0x00078DC8
		public void StartReceiving()
		{
			if (!this.m_stolenSocket)
			{
				LogAdapter.Log(LogLevel.Error, "StartReceiving should only be called on sockets created with ClientConnection(Socket)", "");
				return;
			}
			try
			{
				this.m_socket.BeginReceive(this.m_receiveBuffer, 0, this.m_receiveBuffer.Length, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), null);
			}
			catch (Exception ex)
			{
				LogAdapter.LogException(ex.Message, "ClientConnection.StartReceiving");
			}
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x0007AC3C File Offset: 0x00078E3C
		private void BytesReceived(byte[] bytes, int nBytes, int offset)
		{
			while (nBytes > 0)
			{
				if (this.m_currentPacket == null)
				{
					this.m_currentPacket = Activator.CreateInstance<PacketType>();
				}
				int num = this.m_currentPacket.Decode(bytes, offset, nBytes);
				nBytes -= num;
				offset += num;
				if (!this.m_currentPacket.IsLoaded())
				{
					Array.Copy(bytes, offset, this.m_backingBuffer, 0, nBytes);
					this.m_backingBufferBytes = nBytes;
					return;
				}
				this.OnConnectionEventFinished(new ConnectionEvent<PacketType>
				{
					Type = ConnectionEventTypes.OnPacketCompleted,
					Packet = this.m_currentPacket
				});
				this.m_currentPacket = default(PacketType);
			}
			this.m_backingBufferBytes = 0;
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x0007ACE8 File Offset: 0x00078EE8
		private void BytesReceived(int nBytes)
		{
			if (this.m_backingBufferBytes > 0)
			{
				int num = this.m_backingBufferBytes + nBytes;
				if (num > this.m_backingBuffer.Length)
				{
					byte[] array = new byte[(num + this.m_backingBufferSize - 1) / this.m_backingBufferSize * this.m_backingBufferSize];
					Array.Copy(this.m_backingBuffer, 0, array, 0, this.m_backingBuffer.Length);
					this.m_backingBuffer = array;
				}
				Array.Copy(this.m_receiveBuffer, 0, this.m_backingBuffer, this.m_backingBufferBytes, nBytes);
				this.m_backingBufferBytes = 0;
				this.BytesReceived(this.m_backingBuffer, num, 0);
			}
			else
			{
				this.BytesReceived(this.m_receiveBuffer, nBytes, 0);
			}
			if (this.m_socketEventListener != null)
			{
				this.m_socketEventListener.ReceivePacketEvent(this.m_connection.Host, this.m_connection.Port, (uint)nBytes);
			}
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x0007ADB8 File Offset: 0x00078FB8
		private void ReceiveCallback(IAsyncResult ar)
		{
			if (this.m_socket == null)
			{
				LogAdapter.LogException("ClientConnection.ReceiveCallback called after the connection has been disconnected.\n" + Environment.StackTrace, "ReceiveCallback");
			}
			else
			{
				try
				{
					SocketError socketError = SocketError.Success;
					int num = this.m_socket.EndReceive(ar, out socketError);
					if (num > 0 && socketError == SocketError.Success)
					{
						this.BytesReceived(num);
						this.m_socket.BeginReceive(this.m_receiveBuffer, 0, this.m_receiveBuffer.Length, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), null);
						return;
					}
					LogAdapter.Log(LogLevel.Warning, string.Format("ClientConnection.ReceiveCallback failed. bytesReceived:{0}, error:{1}", num, socketError), "");
				}
				catch (Exception ex)
				{
					string arg = "NONE";
					if (this.m_currentPacket != null)
					{
						try
						{
							arg = this.m_currentPacket.ToString();
						}
						catch (Exception ex2)
						{
							arg = "UNKNOWN - Exception while gathering information: " + ex2.Message;
						}
					}
					LogAdapter.LogException(string.Format("{0}  Packet: {1}", ex.Message, arg), "ReceiveCallback");
				}
			}
			this.AddDisconnectEvent(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x0007AEE0 File Offset: 0x000790E0
		public virtual void SendPacket(PacketType packet)
		{
			byte[] array = packet.Encode();
			if (array.Length == 0)
			{
				return;
			}
			if (this.m_connectionState != ConnectionState.Connected)
			{
				return;
			}
			try
			{
				this.m_socket.BeginSend(array, 0, array.Length, SocketFlags.None, new AsyncCallback(this.SendCallback), null);
				if (this.m_socketEventListener != null)
				{
					this.m_socketEventListener.SendPacketEvent(this.m_connection.Host, this.m_connection.Port, (uint)array.Length);
				}
			}
			catch (Exception ex)
			{
				if (packet.IsFatalOnError())
				{
					LogAdapter.LogFatal(string.Format("{0}  Packet: {1}", ex.Message, packet), "ClientConnection.SendPacket");
				}
				else
				{
					LogAdapter.LogException(string.Format("{0}  Packet: {1}", ex.Message, packet), "ClientConnection.SendPacket");
				}
				this.AddDisconnectEvent(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
			}
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x0007AFC4 File Offset: 0x000791C4
		private void SendCallback(IAsyncResult ar)
		{
			try
			{
				SocketError socketError = SocketError.Success;
				int num = this.m_socket.EndSend(ar, out socketError);
				if (num > 0 && socketError == SocketError.Success)
				{
					return;
				}
				LogAdapter.Log(LogLevel.Warning, string.Format("ClientConnection.SendCallback failed. bytesSent:{0}, error:{1}", num, socketError), "");
			}
			catch (Exception ex)
			{
				LogAdapter.LogException(ex.Message, "ClientConnection.SendCallback");
			}
			this.AddDisconnectEvent(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x0007B03C File Offset: 0x0007923C
		public void AddListener(IClientConnectionListener<PacketType> listener, object state)
		{
			this.m_listeners.Add(listener);
			this.m_listenerStates.Add(state);
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x0007B056 File Offset: 0x00079256
		public void RemoveListener(IClientConnectionListener<PacketType> listener)
		{
			this.m_listeners.Remove(listener);
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x00003FD0 File Offset: 0x000021D0
		public virtual void Update()
		{
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x0007B065 File Offset: 0x00079265
		protected virtual void Dispose(bool isDisposing)
		{
			if (this.isDisposed)
			{
				return;
			}
			if (isDisposing)
			{
				this.DisconnectSocket();
			}
			this.isDisposed = true;
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x0007B080 File Offset: 0x00079280
		protected virtual void OnConnectionEventFinished(ConnectionEvent<PacketType> connectionEvent)
		{
			this.HandleConnectionEvent(connectionEvent);
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x0007B08C File Offset: 0x0007928C
		protected virtual void HandleConnectionEvent(ConnectionEvent<PacketType> connectionEvent)
		{
			switch (connectionEvent.Type)
			{
			case ConnectionEventTypes.OnConnected:
			{
				if (connectionEvent.Error != BattleNetErrors.ERROR_OK)
				{
					this.DisconnectSocket();
					this.m_connectionState = ConnectionState.ConnectionFailed;
				}
				else
				{
					this.m_connectionState = ConnectionState.Connected;
				}
				int count = this.m_connectHandlers.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_connectHandlers[i](connectionEvent.Error);
				}
				return;
			}
			case ConnectionEventTypes.OnDisconnected:
			{
				if (connectionEvent.Error != BattleNetErrors.ERROR_OK)
				{
					this.Disconnect();
				}
				int count2 = this.m_disconnectHandlers.Count;
				for (int j = 0; j < count2; j++)
				{
					this.m_disconnectHandlers[j](connectionEvent.Error);
				}
				return;
			}
			case ConnectionEventTypes.OnPacketCompleted:
			{
				int count3 = this.m_listeners.Count;
				for (int k = 0; k < count3; k++)
				{
					this.m_listeners[k].PacketReceived(connectionEvent.Packet, this.m_listenerStates[k]);
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x04000E4B RID: 3659
		private List<ConnectHandler> m_connectHandlers = new List<ConnectHandler>();

		// Token: 0x04000E4C RID: 3660
		private List<DisconnectHandler> m_disconnectHandlers = new List<DisconnectHandler>();

		// Token: 0x04000E4D RID: 3661
		protected const int RECEIVE_BUFFER_SIZE = 65536;

		// Token: 0x04000E4E RID: 3662
		protected const int BACKING_BUFFER_SIZE = 262144;

		// Token: 0x04000E4F RID: 3663
		private bool m_stolenSocket;

		// Token: 0x04000E50 RID: 3664
		protected ConnectionState m_connectionState;

		// Token: 0x04000E51 RID: 3665
		protected Socket m_socket;

		// Token: 0x04000E52 RID: 3666
		private byte[] m_receiveBuffer;

		// Token: 0x04000E53 RID: 3667
		private byte[] m_backingBuffer;

		// Token: 0x04000E54 RID: 3668
		private int m_backingBufferBytes;

		// Token: 0x04000E55 RID: 3669
		private readonly int m_backingBufferSize;

		// Token: 0x04000E56 RID: 3670
		protected TcpConnection m_connection = new TcpConnection();

		// Token: 0x04000E57 RID: 3671
		private PacketType m_currentPacket;

		// Token: 0x04000E58 RID: 3672
		private List<IClientConnectionListener<PacketType>> m_listeners = new List<IClientConnectionListener<PacketType>>();

		// Token: 0x04000E59 RID: 3673
		private List<object> m_listenerStates = new List<object>();

		// Token: 0x04000E5A RID: 3674
		private bool isDisposed;

		// Token: 0x04000E5B RID: 3675
		private readonly ISocketEventListener m_socketEventListener;
	}
}
