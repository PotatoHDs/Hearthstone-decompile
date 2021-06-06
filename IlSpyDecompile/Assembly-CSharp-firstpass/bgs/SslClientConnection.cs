using System;
using System.Collections.Generic;
using System.Threading;

namespace bgs
{
	public class SslClientConnection : IClientConnection<BattleNetPacket>, IDisposable
	{
		protected const int RECEIVE_BUFFER_SIZE = 262144;

		protected const int BACKING_BUFFER_SIZE = 262144;

		protected const float BLOCKING_SEND_TIME_OUT = 1f;

		protected static readonly TimeSpan BlockSendTimeSpan = TimeSpan.FromSeconds(1.0);

		protected readonly AutoResetEvent m_blockWaitHandle = new AutoResetEvent(initialState: false);

		protected ConnectionState m_connectionState;

		protected SslSocket m_sslSocket;

		private byte[] m_receiveBuffer;

		private byte[] m_backingBuffer;

		private int m_backingBufferBytes;

		protected string m_hostAddress;

		protected uint m_hostPort;

		protected bool IsDisposed;

		protected BattleNetPacket m_currentPacket;

		private SslCertBundleSettings m_bundleSettings;

		private readonly IFileUtil m_fileUtil;

		private readonly IJsonSerializer m_jsonSerializer;

		private readonly ISocketEventListener m_socketEventListener;

		private readonly int m_backingBufferSize;

		private List<IClientConnectionListener<BattleNetPacket>> m_listeners = new List<IClientConnectionListener<BattleNetPacket>>();

		private List<object> m_listenerStates = new List<object>();

		private List<ConnectHandler> m_connectHandlers = new List<ConnectHandler>();

		private List<DisconnectHandler> m_disconnectHandlers = new List<DisconnectHandler>();

		public bool Active => m_sslSocket.Connected;

		public bool BlockOnSend { get; set; }

		public SslClientConnection(SslCertBundleSettings bundleSettings, IFileUtil fileUtil, IJsonSerializer jsonSerializer, ISocketEventListener socketEventListener, int receiveBufferSize, int backingBufferSize)
		{
			m_connectionState = ConnectionState.Disconnected;
			m_receiveBuffer = new byte[receiveBufferSize];
			m_backingBuffer = new byte[backingBufferSize];
			m_bundleSettings = bundleSettings;
			m_fileUtil = fileUtil;
			m_jsonSerializer = jsonSerializer;
			m_backingBufferSize = backingBufferSize;
			m_socketEventListener = socketEventListener;
			IsDisposed = false;
		}

		public void Dispose()
		{
			Dispose(isDisposing: true);
			GC.SuppressFinalize(this);
		}

		public void Connect(string host, uint port, int tryCount)
		{
			try
			{
				if (!UriUtils.GetHostAddress(host, out m_hostAddress))
				{
					m_hostAddress = host;
				}
			}
			catch (Exception ex)
			{
				LogAdapter.Log(LogLevel.Warning, "Unable to get host address for host: " + host + " -- " + ex.Message);
				m_hostAddress = host;
			}
			m_hostPort = port;
			Disconnect();
			m_sslSocket = new SslSocket(m_fileUtil, m_jsonSerializer);
			m_connectionState = ConnectionState.Connecting;
			try
			{
				m_sslSocket.BeginConnect(host, port, m_bundleSettings, ConnectCallback, tryCount);
			}
			catch (Exception ex2)
			{
				string text = m_hostAddress + ":" + m_hostPort;
				LogAdapter.Log(LogLevel.Warning, "Could not connect to " + text + " -- " + ex2.Message);
				m_connectionState = ConnectionState.ConnectionFailed;
				TriggerOnConnectHandler(BattleNetErrors.ERROR_RPC_PEER_UNKNOWN);
			}
			m_bundleSettings = null;
		}

		public void Disconnect()
		{
			if (m_sslSocket != null)
			{
				m_sslSocket.Close();
				m_sslSocket = null;
			}
			m_connectionState = ConnectionState.Disconnected;
			if (m_socketEventListener != null)
			{
				m_socketEventListener.DisconnectEvent(m_hostAddress, m_hostPort);
			}
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

		private void SendBytes(byte[] bytes)
		{
			if (bytes.Length == 0)
			{
				return;
			}
			m_blockWaitHandle.Reset();
			m_sslSocket.BeginSend(bytes, delegate(bool wasSent)
			{
				if (!wasSent)
				{
					TriggerOnDisconnectHandler(BattleNetErrors.ERROR_RPC_CONNECTION_TIMED_OUT);
				}
				m_blockWaitHandle.Set();
			});
			if (BlockOnSend)
			{
				m_blockWaitHandle.WaitOne(BlockSendTimeSpan, exitContext: false);
			}
		}

		public virtual void SendPacket(BattleNetPacket packet)
		{
			byte[] array = packet.Encode();
			SendBytes(array);
			if (m_socketEventListener != null)
			{
				m_socketEventListener.SendPacketEvent(m_hostAddress, m_hostPort, (uint)array.Length);
			}
		}

		public virtual void AddListener(IClientConnectionListener<BattleNetPacket> listener, object state)
		{
			m_listeners.Add(listener);
			m_listenerStates.Add(state);
		}

		public void RemoveListener(IClientConnectionListener<BattleNetPacket> listener)
		{
			m_listeners.Remove(listener);
		}

		public virtual void Update()
		{
		}

		~SslClientConnection()
		{
			Dispose(isDisposing: false);
		}

		protected virtual void OnConnectionEventFinish(ConnectionEvent<BattleNetPacket> connectionEvent)
		{
			HandleConnectionEvent(connectionEvent);
		}

		protected void HandleConnectionEvent(ConnectionEvent<BattleNetPacket> connectionEvent)
		{
			switch (connectionEvent.Type)
			{
			case ConnectionEventTypes.OnConnected:
			{
				if (connectionEvent.Error != 0)
				{
					Disconnect();
					m_connectionState = ConnectionState.ConnectionFailed;
				}
				else
				{
					m_connectionState = ConnectionState.Connected;
				}
				int count = m_disconnectHandlers.Count;
				ConnectHandler[] array = m_connectHandlers.ToArray();
				for (int j = 0; j < count; j++)
				{
					array[j](connectionEvent.Error);
				}
				break;
			}
			case ConnectionEventTypes.OnDisconnected:
			{
				if (connectionEvent.Error != 0)
				{
					Disconnect();
				}
				int count2 = m_disconnectHandlers.Count;
				for (int k = 0; k < count2; k++)
				{
					m_disconnectHandlers[k](connectionEvent.Error);
				}
				break;
			}
			case ConnectionEventTypes.OnPacketCompleted:
			{
				for (int i = 0; i < m_listeners.Count; i++)
				{
					m_listeners[i].PacketReceived(connectionEvent.Packet, m_listenerStates[i]);
				}
				break;
			}
			}
		}

		protected virtual void Dispose(bool isDisposing)
		{
			if (!IsDisposed)
			{
				if (isDisposing && m_sslSocket != null)
				{
					Disconnect();
				}
				IsDisposed = true;
			}
		}

		private void TriggerOnConnectHandler(BattleNetErrors error)
		{
			OnConnectionEventFinish(new ConnectionEvent<BattleNetPacket>
			{
				Type = ConnectionEventTypes.OnConnected,
				Error = error
			});
		}

		private void TriggerOnDisconnectHandler(BattleNetErrors error)
		{
			OnConnectionEventFinish(new ConnectionEvent<BattleNetPacket>
			{
				Type = ConnectionEventTypes.OnDisconnected,
				Error = error
			});
		}

		private void ConnectCallback(bool connectFailed, bool isEncrypted, bool isSigned)
		{
			if (!connectFailed)
			{
				try
				{
					m_sslSocket.BeginReceive(m_receiveBuffer, m_receiveBuffer.Length, ReceiveCallback);
				}
				catch (Exception ex)
				{
					LogAdapter.Log(LogLevel.Error, "SslClientConnection.ConnectCallback: exception calling BeginReceive: " + ex.ToString());
					connectFailed = true;
				}
			}
			if (connectFailed || !m_sslSocket.Connected)
			{
				TriggerOnConnectHandler(BattleNetErrors.ERROR_RPC_PEER_UNAVAILABLE);
				return;
			}
			TriggerOnConnectHandler(BattleNetErrors.ERROR_OK);
			if (m_socketEventListener != null)
			{
				m_socketEventListener.ConnectEvent(m_hostAddress, m_hostPort);
			}
		}

		private void BytesReceived(byte[] bytes, int nBytes, int offset)
		{
			while (nBytes > 0)
			{
				if (m_currentPacket == null)
				{
					m_currentPacket = new BattleNetPacket();
				}
				int num = m_currentPacket.Decode(bytes, offset, nBytes);
				nBytes -= num;
				offset += num;
				if (m_currentPacket.IsLoaded())
				{
					OnConnectionEventFinish(new ConnectionEvent<BattleNetPacket>
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
				m_socketEventListener.ReceivePacketEvent(m_hostAddress, m_hostPort, (uint)nBytes);
			}
		}

		private void ReceiveCallback(int bytesReceived)
		{
			if (bytesReceived == 0 || !m_sslSocket.Connected)
			{
				TriggerOnDisconnectHandler(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
			}
			else
			{
				if (m_sslSocket == null || !m_sslSocket.Connected)
				{
					return;
				}
				try
				{
					if (bytesReceived > 0)
					{
						BytesReceived(bytesReceived);
						m_sslSocket.BeginReceive(m_receiveBuffer, m_receiveBuffer.Length, ReceiveCallback);
					}
				}
				catch (Exception)
				{
					TriggerOnDisconnectHandler(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
				}
			}
		}
	}
}
