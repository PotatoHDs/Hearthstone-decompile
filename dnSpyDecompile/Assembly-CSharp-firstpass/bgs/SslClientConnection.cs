using System;
using System.Collections.Generic;
using System.Threading;

namespace bgs
{
	// Token: 0x02000264 RID: 612
	public class SslClientConnection : IClientConnection<BattleNetPacket>, IDisposable
	{
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600252F RID: 9519 RVA: 0x00083AD6 File Offset: 0x00081CD6
		public bool Active
		{
			get
			{
				return this.m_sslSocket.Connected;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002530 RID: 9520 RVA: 0x00083AE3 File Offset: 0x00081CE3
		// (set) Token: 0x06002531 RID: 9521 RVA: 0x00083AEB File Offset: 0x00081CEB
		public bool BlockOnSend { get; set; }

		// Token: 0x06002532 RID: 9522 RVA: 0x00083AF4 File Offset: 0x00081CF4
		public SslClientConnection(SslCertBundleSettings bundleSettings, IFileUtil fileUtil, IJsonSerializer jsonSerializer, ISocketEventListener socketEventListener, int receiveBufferSize, int backingBufferSize)
		{
			this.m_connectionState = ConnectionState.Disconnected;
			this.m_receiveBuffer = new byte[receiveBufferSize];
			this.m_backingBuffer = new byte[backingBufferSize];
			this.m_bundleSettings = bundleSettings;
			this.m_fileUtil = fileUtil;
			this.m_jsonSerializer = jsonSerializer;
			this.m_backingBufferSize = backingBufferSize;
			this.m_socketEventListener = socketEventListener;
			this.IsDisposed = false;
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x00083B8C File Offset: 0x00081D8C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00083B9C File Offset: 0x00081D9C
		public void Connect(string host, uint port, int tryCount)
		{
			try
			{
				if (!UriUtils.GetHostAddress(host, out this.m_hostAddress))
				{
					this.m_hostAddress = host;
				}
			}
			catch (Exception ex)
			{
				LogAdapter.Log(LogLevel.Warning, "Unable to get host address for host: " + host + " -- " + ex.Message, "");
				this.m_hostAddress = host;
			}
			this.m_hostPort = port;
			this.Disconnect();
			this.m_sslSocket = new SslSocket(this.m_fileUtil, this.m_jsonSerializer);
			this.m_connectionState = ConnectionState.Connecting;
			try
			{
				this.m_sslSocket.BeginConnect(host, port, this.m_bundleSettings, new SslSocket.BeginConnectDelegate(this.ConnectCallback), tryCount);
			}
			catch (Exception ex2)
			{
				string str = this.m_hostAddress + ":" + this.m_hostPort;
				LogAdapter.Log(LogLevel.Warning, "Could not connect to " + str + " -- " + ex2.Message, "");
				this.m_connectionState = ConnectionState.ConnectionFailed;
				this.TriggerOnConnectHandler(BattleNetErrors.ERROR_RPC_PEER_UNKNOWN);
			}
			this.m_bundleSettings = null;
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x00083CB0 File Offset: 0x00081EB0
		public void Disconnect()
		{
			if (this.m_sslSocket != null)
			{
				this.m_sslSocket.Close();
				this.m_sslSocket = null;
			}
			this.m_connectionState = ConnectionState.Disconnected;
			if (this.m_socketEventListener != null)
			{
				this.m_socketEventListener.DisconnectEvent(this.m_hostAddress, this.m_hostPort);
			}
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x00083CFD File Offset: 0x00081EFD
		public bool AddConnectHandler(ConnectHandler handler)
		{
			if (this.m_connectHandlers.Contains(handler))
			{
				return false;
			}
			this.m_connectHandlers.Add(handler);
			return true;
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x00083D1C File Offset: 0x00081F1C
		public bool RemoveConnectHandler(ConnectHandler handler)
		{
			return this.m_connectHandlers.Remove(handler);
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x00083D2A File Offset: 0x00081F2A
		public bool AddDisconnectHandler(DisconnectHandler handler)
		{
			if (this.m_disconnectHandlers.Contains(handler))
			{
				return false;
			}
			this.m_disconnectHandlers.Add(handler);
			return true;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x00083D49 File Offset: 0x00081F49
		public bool RemoveDisconnectHandler(DisconnectHandler handler)
		{
			return this.m_disconnectHandlers.Remove(handler);
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x00083D58 File Offset: 0x00081F58
		private void SendBytes(byte[] bytes)
		{
			if (bytes.Length != 0)
			{
				this.m_blockWaitHandle.Reset();
				this.m_sslSocket.BeginSend(bytes, delegate(bool wasSent)
				{
					if (!wasSent)
					{
						this.TriggerOnDisconnectHandler(BattleNetErrors.ERROR_RPC_CONNECTION_TIMED_OUT);
					}
					this.m_blockWaitHandle.Set();
				});
				if (this.BlockOnSend)
				{
					this.m_blockWaitHandle.WaitOne(SslClientConnection.BlockSendTimeSpan, false);
				}
			}
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x00083DA8 File Offset: 0x00081FA8
		public virtual void SendPacket(BattleNetPacket packet)
		{
			byte[] array = packet.Encode();
			this.SendBytes(array);
			if (this.m_socketEventListener != null)
			{
				this.m_socketEventListener.SendPacketEvent(this.m_hostAddress, this.m_hostPort, (uint)array.Length);
			}
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x00083DE5 File Offset: 0x00081FE5
		public virtual void AddListener(IClientConnectionListener<BattleNetPacket> listener, object state)
		{
			this.m_listeners.Add(listener);
			this.m_listenerStates.Add(state);
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x00083DFF File Offset: 0x00081FFF
		public void RemoveListener(IClientConnectionListener<BattleNetPacket> listener)
		{
			this.m_listeners.Remove(listener);
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x00003FD0 File Offset: 0x000021D0
		public virtual void Update()
		{
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x00083E10 File Offset: 0x00082010
		~SslClientConnection()
		{
			this.Dispose(false);
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x0007C188 File Offset: 0x0007A388
		protected virtual void OnConnectionEventFinish(ConnectionEvent<BattleNetPacket> connectionEvent)
		{
			this.HandleConnectionEvent(connectionEvent);
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x00083E40 File Offset: 0x00082040
		protected void HandleConnectionEvent(ConnectionEvent<BattleNetPacket> connectionEvent)
		{
			switch (connectionEvent.Type)
			{
			case ConnectionEventTypes.OnConnected:
			{
				if (connectionEvent.Error != BattleNetErrors.ERROR_OK)
				{
					this.Disconnect();
					this.m_connectionState = ConnectionState.ConnectionFailed;
				}
				else
				{
					this.m_connectionState = ConnectionState.Connected;
				}
				int count = this.m_disconnectHandlers.Count;
				ConnectHandler[] array = this.m_connectHandlers.ToArray();
				for (int i = 0; i < count; i++)
				{
					array[i](connectionEvent.Error);
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
				for (int k = 0; k < this.m_listeners.Count; k++)
				{
					this.m_listeners[k].PacketReceived(connectionEvent.Packet, this.m_listenerStates[k]);
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x00083F3F File Offset: 0x0008213F
		protected virtual void Dispose(bool isDisposing)
		{
			if (this.IsDisposed)
			{
				return;
			}
			if (isDisposing && this.m_sslSocket != null)
			{
				this.Disconnect();
			}
			this.IsDisposed = true;
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x00083F62 File Offset: 0x00082162
		private void TriggerOnConnectHandler(BattleNetErrors error)
		{
			this.OnConnectionEventFinish(new ConnectionEvent<BattleNetPacket>
			{
				Type = ConnectionEventTypes.OnConnected,
				Error = error
			});
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x00083F7D File Offset: 0x0008217D
		private void TriggerOnDisconnectHandler(BattleNetErrors error)
		{
			this.OnConnectionEventFinish(new ConnectionEvent<BattleNetPacket>
			{
				Type = ConnectionEventTypes.OnDisconnected,
				Error = error
			});
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x00083F98 File Offset: 0x00082198
		private void ConnectCallback(bool connectFailed, bool isEncrypted, bool isSigned)
		{
			if (!connectFailed)
			{
				try
				{
					this.m_sslSocket.BeginReceive(this.m_receiveBuffer, this.m_receiveBuffer.Length, new SslSocket.BeginReceiveDelegate(this.ReceiveCallback));
				}
				catch (Exception ex)
				{
					LogAdapter.Log(LogLevel.Error, "SslClientConnection.ConnectCallback: exception calling BeginReceive: " + ex.ToString(), "");
					connectFailed = true;
				}
			}
			if (connectFailed || !this.m_sslSocket.Connected)
			{
				this.TriggerOnConnectHandler(BattleNetErrors.ERROR_RPC_PEER_UNAVAILABLE);
				return;
			}
			this.TriggerOnConnectHandler(BattleNetErrors.ERROR_OK);
			if (this.m_socketEventListener != null)
			{
				this.m_socketEventListener.ConnectEvent(this.m_hostAddress, this.m_hostPort);
			}
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x00084044 File Offset: 0x00082244
		private void BytesReceived(byte[] bytes, int nBytes, int offset)
		{
			while (nBytes > 0)
			{
				if (this.m_currentPacket == null)
				{
					this.m_currentPacket = new BattleNetPacket();
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
				this.OnConnectionEventFinish(new ConnectionEvent<BattleNetPacket>
				{
					Type = ConnectionEventTypes.OnPacketCompleted,
					Packet = this.m_currentPacket
				});
				this.m_currentPacket = null;
			}
			this.m_backingBufferBytes = 0;
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x000840D8 File Offset: 0x000822D8
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
				this.m_socketEventListener.ReceivePacketEvent(this.m_hostAddress, this.m_hostPort, (uint)nBytes);
			}
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x0008419C File Offset: 0x0008239C
		private void ReceiveCallback(int bytesReceived)
		{
			if (bytesReceived == 0 || !this.m_sslSocket.Connected)
			{
				this.TriggerOnDisconnectHandler(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
				return;
			}
			if (this.m_sslSocket != null && this.m_sslSocket.Connected)
			{
				try
				{
					if (bytesReceived > 0)
					{
						this.BytesReceived(bytesReceived);
						this.m_sslSocket.BeginReceive(this.m_receiveBuffer, this.m_receiveBuffer.Length, new SslSocket.BeginReceiveDelegate(this.ReceiveCallback));
					}
				}
				catch (Exception)
				{
					this.TriggerOnDisconnectHandler(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
				}
			}
		}

		// Token: 0x04000F79 RID: 3961
		protected const int RECEIVE_BUFFER_SIZE = 262144;

		// Token: 0x04000F7A RID: 3962
		protected const int BACKING_BUFFER_SIZE = 262144;

		// Token: 0x04000F7B RID: 3963
		protected const float BLOCKING_SEND_TIME_OUT = 1f;

		// Token: 0x04000F7C RID: 3964
		protected static readonly TimeSpan BlockSendTimeSpan = TimeSpan.FromSeconds(1.0);

		// Token: 0x04000F7D RID: 3965
		protected readonly AutoResetEvent m_blockWaitHandle = new AutoResetEvent(false);

		// Token: 0x04000F7E RID: 3966
		protected ConnectionState m_connectionState;

		// Token: 0x04000F7F RID: 3967
		protected SslSocket m_sslSocket;

		// Token: 0x04000F80 RID: 3968
		private byte[] m_receiveBuffer;

		// Token: 0x04000F81 RID: 3969
		private byte[] m_backingBuffer;

		// Token: 0x04000F82 RID: 3970
		private int m_backingBufferBytes;

		// Token: 0x04000F83 RID: 3971
		protected string m_hostAddress;

		// Token: 0x04000F84 RID: 3972
		protected uint m_hostPort;

		// Token: 0x04000F85 RID: 3973
		protected bool IsDisposed;

		// Token: 0x04000F86 RID: 3974
		protected BattleNetPacket m_currentPacket;

		// Token: 0x04000F87 RID: 3975
		private SslCertBundleSettings m_bundleSettings;

		// Token: 0x04000F88 RID: 3976
		private readonly IFileUtil m_fileUtil;

		// Token: 0x04000F89 RID: 3977
		private readonly IJsonSerializer m_jsonSerializer;

		// Token: 0x04000F8A RID: 3978
		private readonly ISocketEventListener m_socketEventListener;

		// Token: 0x04000F8B RID: 3979
		private readonly int m_backingBufferSize;

		// Token: 0x04000F8C RID: 3980
		private List<IClientConnectionListener<BattleNetPacket>> m_listeners = new List<IClientConnectionListener<BattleNetPacket>>();

		// Token: 0x04000F8D RID: 3981
		private List<object> m_listenerStates = new List<object>();

		// Token: 0x04000F8E RID: 3982
		private List<ConnectHandler> m_connectHandlers = new List<ConnectHandler>();

		// Token: 0x04000F8F RID: 3983
		private List<DisconnectHandler> m_disconnectHandlers = new List<DisconnectHandler>();
	}
}
