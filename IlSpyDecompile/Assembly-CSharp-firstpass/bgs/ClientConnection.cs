using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace bgs
{
	public class ClientConnection<PacketType> : IDisposable, IClientConnection<PacketType> where PacketType : PacketFormat, new()
	{
		private List<ConnectHandler> m_connectHandlers = new List<ConnectHandler>();

		private List<DisconnectHandler> m_disconnectHandlers = new List<DisconnectHandler>();

		protected const int RECEIVE_BUFFER_SIZE = 65536;

		protected const int BACKING_BUFFER_SIZE = 262144;

		private bool m_stolenSocket;

		protected ConnectionState m_connectionState;

		protected Socket m_socket;

		private byte[] m_receiveBuffer;

		private byte[] m_backingBuffer;

		private int m_backingBufferBytes;

		private readonly int m_backingBufferSize;

		protected TcpConnection m_connection = new TcpConnection();

		private PacketType m_currentPacket;

		private List<IClientConnectionListener<PacketType>> m_listeners = new List<IClientConnectionListener<PacketType>>();

		private List<object> m_listenerStates = new List<object>();

		private bool isDisposed;

		private readonly ISocketEventListener m_socketEventListener;

		public bool Active
		{
			get
			{
				if (m_connectionState != ConnectionState.Connecting)
				{
					return m_connectionState == ConnectionState.Connected;
				}
				return true;
			}
		}

		public ConnectionState State => m_connectionState;

		public ClientConnection(ISocketEventListener socketEventListener, int receiveBufferSize, int backingBufferSize)
		{
			m_socketEventListener = socketEventListener;
			m_connectionState = ConnectionState.Disconnected;
			m_receiveBuffer = new byte[receiveBufferSize];
			m_backingBuffer = new byte[backingBufferSize];
			m_backingBufferSize = backingBufferSize;
			m_stolenSocket = false;
			isDisposed = false;
		}

		public ClientConnection(Socket socket)
		{
			m_socket = socket;
			m_connectionState = ConnectionState.Connected;
			m_receiveBuffer = new byte[65536];
			m_stolenSocket = true;
			isDisposed = false;
		}

		~ClientConnection()
		{
			Dispose(isDisposing: false);
		}

		public void Dispose()
		{
			Dispose(isDisposing: true);
			GC.SuppressFinalize(this);
		}

		public bool AddConnectHandler(ConnectHandler handler)
		{
			if (m_connectHandlers.Contains(handler))
			{
				return false;
			}
			m_connectHandlers.Add(handler);
			return true;
		}

		public bool RemoveConnectHandler(ConnectHandler handler)
		{
			return m_connectHandlers.Remove(handler);
		}

		public bool AddDisconnectHandler(DisconnectHandler handler)
		{
			if (m_disconnectHandlers.Contains(handler))
			{
				return false;
			}
			m_disconnectHandlers.Add(handler);
			return true;
		}

		public bool RemoveDisconnectHandler(DisconnectHandler handler)
		{
			return m_disconnectHandlers.Remove(handler);
		}

		public virtual bool HasEvents()
		{
			return false;
		}

		private void AddConnectEvent(BattleNetErrors error, Exception exception = null)
		{
			OnConnectionEventFinished(new ConnectionEvent<PacketType>
			{
				Type = ConnectionEventTypes.OnConnected,
				Error = error,
				Exception = exception
			});
		}

		protected void AddDisconnectEvent(BattleNetErrors error)
		{
			OnConnectionEventFinished(new ConnectionEvent<PacketType>
			{
				Type = ConnectionEventTypes.OnDisconnected,
				Error = error
			});
		}

		private void CleanBuffer()
		{
			Array.Clear(m_receiveBuffer, 0, m_receiveBuffer.Length);
			Array.Clear(m_backingBuffer, 0, m_backingBuffer.Length);
			m_backingBufferBytes = 0;
			m_currentPacket = null;
		}

		public virtual void Connect(string host, uint port, int tryCount)
		{
			CleanBuffer();
			m_connection.LogDebug = delegate(string log)
			{
				LogAdapter.Log(LogLevel.Debug, log);
			};
			m_connection.LogWarning = delegate(string log)
			{
				LogAdapter.Log(LogLevel.Warning, log);
			};
			m_connection.OnFailure = delegate
			{
				m_connectionState = ConnectionState.ConnectionFailed;
				AddConnectEvent(BattleNetErrors.ERROR_RPC_PEER_UNKNOWN);
			};
			m_connection.OnSuccess = ConnectCallback;
			m_connectionState = ConnectionState.Connecting;
			LogAdapter.LogInfo("Socket Connecting...", "ClientConnection.Connect");
			m_connection.Connect(host, port, tryCount);
		}

		private void ConnectCallback()
		{
			Exception ex = null;
			m_socket = m_connection.Socket;
			LogAdapter.LogInfo("Socket Connected", "ClientConnection.ConnectCallback");
			try
			{
				m_socket.BeginReceive(m_receiveBuffer, 0, m_receiveBuffer.Length, SocketFlags.None, ReceiveCallback, null);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (ex != null || !m_socket.Connected)
			{
				LogAdapter.Log(LogLevel.Warning, $"ClientConnection - BeginReceive() failed. ip:{m_connection.Host}, port:{m_connection.Port}");
				DisconnectSocket();
				m_connectionState = ConnectionState.ConnectionFailed;
				AddConnectEvent(BattleNetErrors.ERROR_RPC_PEER_UNAVAILABLE, ex);
			}
			else
			{
				AddConnectEvent(BattleNetErrors.ERROR_OK);
				if (m_socketEventListener != null)
				{
					m_socketEventListener.ConnectEvent(m_connection.Host, m_connection.Port);
				}
			}
		}

		public void Disconnect()
		{
			if (m_socketEventListener != null)
			{
				m_socketEventListener.DisconnectEvent(m_connection.Host, m_connection.Port);
			}
			DisconnectSocket();
			m_connectionState = ConnectionState.Disconnected;
		}

		private void DisconnectSocket()
		{
			if (m_socket == null)
			{
				return;
			}
			try
			{
				if (m_socket.Connected)
				{
					m_socket.Shutdown(SocketShutdown.Both);
					m_socket.Close();
				}
			}
			catch (SocketException ex)
			{
				LogAdapter.LogException($"{ex.Message}  socket error code: {ex.ErrorCode}", "ClientConnection.DisconnectSocket");
			}
			catch (Exception ex2)
			{
				LogAdapter.LogException(ex2.Message, "ClientConnection.DisconnectSocket");
			}
			LogAdapter.LogInfo("Socket Disconnected", "ClientConnection.DisconnectSocket");
			m_socket = null;
		}

		public void StartReceiving()
		{
			if (!m_stolenSocket)
			{
				LogAdapter.Log(LogLevel.Error, "StartReceiving should only be called on sockets created with ClientConnection(Socket)");
				return;
			}
			try
			{
				m_socket.BeginReceive(m_receiveBuffer, 0, m_receiveBuffer.Length, SocketFlags.None, ReceiveCallback, null);
			}
			catch (Exception ex)
			{
				LogAdapter.LogException(ex.Message, "ClientConnection.StartReceiving");
			}
		}

		private void BytesReceived(byte[] bytes, int nBytes, int offset)
		{
			while (nBytes > 0)
			{
				if (m_currentPacket == null)
				{
					m_currentPacket = new PacketType();
				}
				int num = m_currentPacket.Decode(bytes, offset, nBytes);
				nBytes -= num;
				offset += num;
				if (m_currentPacket.IsLoaded())
				{
					OnConnectionEventFinished(new ConnectionEvent<PacketType>
					{
						Type = ConnectionEventTypes.OnPacketCompleted,
						Packet = m_currentPacket
					});
					m_currentPacket = null;
					continue;
				}
				Array.Copy(bytes, offset, m_backingBuffer, 0, nBytes);
				m_backingBufferBytes = nBytes;
				return;
			}
			m_backingBufferBytes = 0;
		}

		private void BytesReceived(int nBytes)
		{
			if (m_backingBufferBytes > 0)
			{
				int num = m_backingBufferBytes + nBytes;
				if (num > m_backingBuffer.Length)
				{
					byte[] array = new byte[(num + m_backingBufferSize - 1) / m_backingBufferSize * m_backingBufferSize];
					Array.Copy(m_backingBuffer, 0, array, 0, m_backingBuffer.Length);
					m_backingBuffer = array;
				}
				Array.Copy(m_receiveBuffer, 0, m_backingBuffer, m_backingBufferBytes, nBytes);
				m_backingBufferBytes = 0;
				BytesReceived(m_backingBuffer, num, 0);
			}
			else
			{
				BytesReceived(m_receiveBuffer, nBytes, 0);
			}
			if (m_socketEventListener != null)
			{
				m_socketEventListener.ReceivePacketEvent(m_connection.Host, m_connection.Port, (uint)nBytes);
			}
		}

		private void ReceiveCallback(IAsyncResult ar)
		{
			if (m_socket == null)
			{
				LogAdapter.LogException("ClientConnection.ReceiveCallback called after the connection has been disconnected.\n" + Environment.StackTrace, "ReceiveCallback");
			}
			else
			{
				try
				{
					SocketError errorCode = SocketError.Success;
					int num = m_socket.EndReceive(ar, out errorCode);
					if (num > 0 && errorCode == SocketError.Success)
					{
						BytesReceived(num);
						m_socket.BeginReceive(m_receiveBuffer, 0, m_receiveBuffer.Length, SocketFlags.None, ReceiveCallback, null);
						return;
					}
					LogAdapter.Log(LogLevel.Warning, $"ClientConnection.ReceiveCallback failed. bytesReceived:{num}, error:{errorCode}");
				}
				catch (Exception ex2)
				{
					string arg = "NONE";
					if (m_currentPacket != null)
					{
						try
						{
							arg = m_currentPacket.ToString();
						}
						catch (Exception ex)
						{
							arg = "UNKNOWN - Exception while gathering information: " + ex.Message;
						}
					}
					LogAdapter.LogException($"{ex2.Message}  Packet: {arg}", "ReceiveCallback");
				}
			}
			AddDisconnectEvent(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
		}

		public virtual void SendPacket(PacketType packet)
		{
			byte[] array = packet.Encode();
			if (array.Length == 0 || m_connectionState != ConnectionState.Connected)
			{
				return;
			}
			try
			{
				m_socket.BeginSend(array, 0, array.Length, SocketFlags.None, SendCallback, null);
				if (m_socketEventListener != null)
				{
					m_socketEventListener.SendPacketEvent(m_connection.Host, m_connection.Port, (uint)array.Length);
				}
			}
			catch (Exception ex)
			{
				if (packet.IsFatalOnError())
				{
					LogAdapter.LogFatal($"{ex.Message}  Packet: {packet}", "ClientConnection.SendPacket");
				}
				else
				{
					LogAdapter.LogException($"{ex.Message}  Packet: {packet}", "ClientConnection.SendPacket");
				}
				AddDisconnectEvent(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
			}
		}

		private void SendCallback(IAsyncResult ar)
		{
			try
			{
				SocketError errorCode = SocketError.Success;
				int num = m_socket.EndSend(ar, out errorCode);
				if (num > 0 && errorCode == SocketError.Success)
				{
					return;
				}
				LogAdapter.Log(LogLevel.Warning, $"ClientConnection.SendCallback failed. bytesSent:{num}, error:{errorCode}");
			}
			catch (Exception ex)
			{
				LogAdapter.LogException(ex.Message, "ClientConnection.SendCallback");
			}
			AddDisconnectEvent(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
		}

		public void AddListener(IClientConnectionListener<PacketType> listener, object state)
		{
			m_listeners.Add(listener);
			m_listenerStates.Add(state);
		}

		public void RemoveListener(IClientConnectionListener<PacketType> listener)
		{
			m_listeners.Remove(listener);
		}

		public virtual void Update()
		{
		}

		protected virtual void Dispose(bool isDisposing)
		{
			if (!isDisposed)
			{
				if (isDisposing)
				{
					DisconnectSocket();
				}
				isDisposed = true;
			}
		}

		protected virtual void OnConnectionEventFinished(ConnectionEvent<PacketType> connectionEvent)
		{
			HandleConnectionEvent(connectionEvent);
		}

		protected virtual void HandleConnectionEvent(ConnectionEvent<PacketType> connectionEvent)
		{
			switch (connectionEvent.Type)
			{
			case ConnectionEventTypes.OnConnected:
			{
				if (connectionEvent.Error != 0)
				{
					DisconnectSocket();
					m_connectionState = ConnectionState.ConnectionFailed;
				}
				else
				{
					m_connectionState = ConnectionState.Connected;
				}
				int count2 = m_connectHandlers.Count;
				for (int j = 0; j < count2; j++)
				{
					m_connectHandlers[j](connectionEvent.Error);
				}
				break;
			}
			case ConnectionEventTypes.OnDisconnected:
			{
				if (connectionEvent.Error != 0)
				{
					Disconnect();
				}
				int count3 = m_disconnectHandlers.Count;
				for (int k = 0; k < count3; k++)
				{
					m_disconnectHandlers[k](connectionEvent.Error);
				}
				break;
			}
			case ConnectionEventTypes.OnPacketCompleted:
			{
				int count = m_listeners.Count;
				for (int i = 0; i < count; i++)
				{
					m_listeners[i].PacketReceived(connectionEvent.Packet, m_listenerStates[i]);
				}
				break;
			}
			}
		}
	}
}
