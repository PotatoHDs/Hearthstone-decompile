using System.Collections.Generic;
using System.Diagnostics;
using bgs.RPCServices;
using bnet.protocol;
using bnet.protocol.connection.v1;

namespace bgs
{
	public class RPCConnection : IRpcConnection, IClientConnectionListener<BattleNetPacket>
	{
		protected const int RESPONSE_SERVICE_ID = 254;

		protected BattleNetLogSource m_logSource = new BattleNetLogSource("Network");

		protected BattleNetLogSource m_cmLogSource = new BattleNetLogSource("ConnectionMetering");

		protected IClientConnection<BattleNetPacket> Connection;

		protected ServiceCollectionHelper m_serviceHelper = new ServiceCollectionHelper();

		private Queue<BattleNetPacket> outBoundPackets = new Queue<BattleNetPacket>();

		private Queue<BattleNetPacket> incomingPackets = new Queue<BattleNetPacket>();

		private List<BattleNetPacket> m_pendingOutboundPackets = new List<BattleNetPacket>();

		protected object tokenLock = new object();

		protected static uint nextToken;

		protected Dictionary<uint, RPCContext> waitingForResponse = new Dictionary<uint, RPCContext>();

		protected OnConnectHandler m_onConnectHandler;

		protected OnDisconnectHandler m_onDisconnectHandler;

		protected Stopwatch m_stopWatch;

		protected readonly IFileUtil m_fileUtil;

		protected readonly IJsonSerializer m_jsonSerializer;

		protected RPCConnectionMetering m_connMetering = new RPCConnectionMetering();

		private readonly ISocketEventListener m_socketEventListener;

		public RPCConnection(IFileUtil fileUtil, IJsonSerializer jsonSerializer, ISocketEventListener socketEventListener)
		{
			m_fileUtil = fileUtil;
			m_jsonSerializer = jsonSerializer;
			m_socketEventListener = socketEventListener;
		}

		public long GetMillisecondsSinceLastPacketSent()
		{
			return m_stopWatch.ElapsedMilliseconds;
		}

		public ServiceCollectionHelper GetServiceHelper()
		{
			return m_serviceHelper;
		}

		public void SetOnConnectHandler(OnConnectHandler handler)
		{
			m_onConnectHandler = handler;
		}

		public void SetOnDisconnectHandler(OnDisconnectHandler handler)
		{
			m_onDisconnectHandler = handler;
		}

		public void Connect(string host, uint port, SslParameters sslParams, int tryCount)
		{
			m_stopWatch = new Stopwatch();
			if (sslParams.useSsl)
			{
				QueueSslClientConnection queueSslClientConnection = new QueueSslClientConnection(sslParams.bundleSettings, m_fileUtil, m_jsonSerializer, m_socketEventListener);
				queueSslClientConnection.OnlyOneSend = true;
				Connection = queueSslClientConnection;
			}
			else
			{
				Connection = new QueueClientConnection<BattleNetPacket>(m_socketEventListener);
			}
			Connection.AddListener(this, null);
			Connection.AddConnectHandler(OnConnectCallback);
			Connection.AddDisconnectHandler(OnDisconnectCallback);
			Connection.Connect(host, port, tryCount);
		}

		public void Disconnect()
		{
			if (Connection == null)
			{
				m_logSource.LogError("Disconnect ignored: Connection is null. Probably couldn't talk to the server or get an ip address for it from DNS");
				return;
			}
			if (Connection is SslClientConnection)
			{
				((SslClientConnection)Connection).BlockOnSend = true;
			}
			Update();
			Connection.Disconnect();
		}

		public void BeginAuth()
		{
		}

		public bool GetInStartupPeriod()
		{
			return m_connMetering.GetInStartupPeriod();
		}

		public RPCContext SendRequest(ServiceDescriptor service, uint methodId, IProtoBuf message, RPCContextDelegate callback = null, uint objectId = 0u)
		{
			return QueueRequest(service, methodId, message, callback, objectId);
		}

		public RPCContext QueueRequest(ServiceDescriptor service, uint methodId, IProtoBuf message, RPCContextDelegate callback = null, uint objectId = 0u)
		{
			if (message == null)
			{
				return null;
			}
			if (!service.Imported)
			{
				m_logSource.LogError("Try sending message to a service which is not imported name={0} hash={0} method={0}", service.Name, service.Hash, methodId);
				return null;
			}
			uint num;
			lock (tokenLock)
			{
				num = nextToken;
				nextToken++;
			}
			RPCContext rPCContext = new RPCContext();
			if (callback != null)
			{
				rPCContext.Callback = callback;
				waitingForResponse.Add(num, rPCContext);
			}
			Header header = CreateHeader(service.Id, service.Hash, methodId, objectId, num, message.GetSerializedSize());
			BattleNetPacket battleNetPacket = new BattleNetPacket(header, message);
			rPCContext.Header = header;
			rPCContext.Request = message;
			if (!m_connMetering.AllowRPCCall(service, methodId))
			{
				m_pendingOutboundPackets.Add(battleNetPacket);
				LogOutgoingPacket(battleNetPacket, wasMetered: true);
			}
			else
			{
				QueuePacket(battleNetPacket);
			}
			return rPCContext;
		}

		public void SendResponse(RPCContext context, IProtoBuf message)
		{
			QueueResponse(context, message);
		}

		public void QueueResponse(RPCContext context, IProtoBuf message)
		{
			if (message == null || context.Header == null)
			{
				m_logSource.LogError("QueueResponse: invalid response");
				return;
			}
			if (m_serviceHelper.GetImportedServiceByHash(context.Header.ServiceHash) == null)
			{
				m_logSource.LogError("QueueResponse: error, unrecognized service hash: " + context.Header.ServiceHash);
				return;
			}
			m_logSource.LogDebug("QueueResponse: type=" + m_serviceHelper.GetImportedServiceByHash(context.Header.ServiceHash).GetMethodName(context.Header.MethodId) + " data=" + message);
			Header header = context.Header;
			header.SetServiceId(254u);
			header.SetMethodId(0u);
			header.SetSize(message.GetSerializedSize());
			context.Header = header;
			BattleNetPacket packet = new BattleNetPacket(context.Header, message);
			QueuePacket(packet);
		}

		public void RegisterServiceMethodListener(uint serviceId, uint methodId, RPCContextDelegate callback)
		{
			GetExportedServiceDescriptor(serviceId)?.RegisterMethodListener(methodId, callback);
		}

		public void Update()
		{
			ProcessPendingOutboundPackets();
			if (outBoundPackets.Count > 0)
			{
				Queue<BattleNetPacket> queue;
				lock (outBoundPackets)
				{
					queue = new Queue<BattleNetPacket>(outBoundPackets.ToArray());
					outBoundPackets.Clear();
				}
				while (queue.Count > 0)
				{
					BattleNetPacket battleNetPacket = queue.Dequeue();
					if (Connection != null)
					{
						Header header = battleNetPacket.GetHeader();
						m_logSource.LogDebug($"Packet sent: Header = ServiceId: {header.ServiceId}, ServiceHash: {header.ServiceHash}, MethodId: {header.MethodId} Token: {header.Token} Size: {header.Size}");
						Connection.SendPacket(battleNetPacket);
						if (battleNetPacket.GetHeader() != null && battleNetPacket.GetHeader().MethodId == 1)
						{
							m_connMetering.SetConnectPacketSentToNow();
						}
					}
					else
					{
						m_logSource.LogError("##Client Connection object does not exists!##");
					}
				}
			}
			if (Connection != null)
			{
				Connection.Update();
			}
			if (incomingPackets.Count <= 0)
			{
				return;
			}
			Queue<BattleNetPacket> queue2;
			lock (incomingPackets)
			{
				queue2 = new Queue<BattleNetPacket>(incomingPackets.ToArray());
				incomingPackets.Clear();
			}
			while (queue2.Count > 0)
			{
				BattleNetPacket battleNetPacket2 = queue2.Dequeue();
				Header header2 = battleNetPacket2.GetHeader();
				PrintHeader(header2);
				byte[] payload = (byte[])battleNetPacket2.GetBody();
				if (header2.ServiceId == 254)
				{
					if (!waitingForResponse.TryGetValue(header2.Token, out var value))
					{
						continue;
					}
					ServiceDescriptor importedServiceByHash = m_serviceHelper.GetImportedServiceByHash(value.Header.ServiceHash);
					MethodDescriptor.ParseMethod parseMethod = null;
					if (importedServiceByHash != null)
					{
						parseMethod = importedServiceByHash.GetParser(value.Header.MethodId);
					}
					if (parseMethod == null)
					{
						if (importedServiceByHash != null)
						{
							m_logSource.LogWarning("Incoming Response: Unable to find method for serviceName={0} method id={1}", importedServiceByHash.Name, value.Header.MethodId);
							int methodCount = importedServiceByHash.GetMethodCount();
							m_logSource.LogDebug("  Found {0} methods", methodCount);
							for (int i = 0; i < methodCount; i++)
							{
								MethodDescriptor methodDescriptor = importedServiceByHash.GetMethodDescriptor((uint)i);
								if (methodDescriptor == null && i != 0)
								{
									m_logSource.LogDebug("  Found method id={0} name={1}", i, "<null>");
								}
								else
								{
									m_logSource.LogDebug("  Found method id={0} name={1}", i, methodDescriptor.Name);
								}
							}
						}
						else
						{
							m_logSource.LogWarning("Incoming Response: Unable to identify service id={0}", value.Header.ServiceId);
						}
					}
					value.Header = header2;
					value.Payload = payload;
					value.ResponseReceived = true;
					if (value.Callback != null)
					{
						value.Callback(value);
					}
					waitingForResponse.Remove(header2.Token);
					continue;
				}
				ServiceDescriptor serviceDescriptor = null;
				if (header2.HasServiceHash && header2.ServiceHash != 0)
				{
					serviceDescriptor = GetExportedServiceDescriptor(header2.ServiceHash);
				}
				if (serviceDescriptor == null)
				{
					serviceDescriptor = GetExportedServiceDescriptor(header2.ServiceId);
				}
				if (serviceDescriptor != null)
				{
					if (serviceDescriptor.GetParser(header2.MethodId) == null)
					{
						m_logSource.LogDebug("Incoming Packet: NULL TYPE service=" + serviceDescriptor.Name + ", methodId=" + header2.MethodId);
					}
					if (serviceDescriptor.HasMethodListener(header2.MethodId))
					{
						RPCContext rPCContext = new RPCContext();
						rPCContext.Header = header2;
						rPCContext.Payload = payload;
						rPCContext.ResponseReceived = true;
						serviceDescriptor.NotifyMethodListener(rPCContext);
					}
					else
					{
						string text = ((serviceDescriptor != null && !string.IsNullOrEmpty(serviceDescriptor.Name)) ? serviceDescriptor.Name : "<null>");
						m_logSource.LogError("[!]Unhandled Server Request Received (Service Name: " + text + " Service hash:" + header2.ServiceHash + " Method id:" + header2.MethodId + ")");
					}
				}
				else
				{
					m_logSource.LogError("[!]Server Requested an Unsupported (Service hash:" + header2.ServiceHash + " Method id:" + header2.MethodId + ")");
				}
			}
		}

		public void PacketReceived(BattleNetPacket p, object state)
		{
			lock (incomingPackets)
			{
				incomingPackets.Enqueue(p);
			}
		}

		public void SetConnectionMeteringContentHandles(ConnectionMeteringContentHandles handles, LocalStorageAPI localStorage)
		{
			if (handles == null || !handles.IsInitialized || handles.ContentHandleCount == 0)
			{
				m_cmLogSource.LogWarning("Invalid connection metering content handle received.");
				return;
			}
			if (handles.ContentHandleCount != 1)
			{
				m_cmLogSource.LogWarning("More than 1 connection metering content handle specified!");
			}
			bnet.protocol.ContentHandle contentHandle = handles.ContentHandle[0];
			if (contentHandle == null || !contentHandle.IsInitialized)
			{
				m_cmLogSource.LogWarning("The content handle received is not valid!");
				return;
			}
			m_cmLogSource.LogDebug("Received request to enable connection metering.");
			ContentHandle contentHandle2 = ContentHandle.FromProtocol(contentHandle);
			m_cmLogSource.LogDebug("Requesting file from local storage. ContentHandle={0}", contentHandle2);
			localStorage.GetFile(contentHandle2, DownloadCompletedCallback);
		}

		protected Header CreateHeader(uint serviceId, uint serviceHash, uint methodId, uint objectId, uint token, uint size)
		{
			Header header = new Header();
			header.SetServiceId(serviceId);
			header.SetServiceHash(serviceHash);
			header.SetMethodId(methodId);
			if (objectId != 0)
			{
				header.SetObjectId(objectId);
			}
			header.SetToken(token);
			header.SetSize(size);
			return header;
		}

		protected void QueuePacket(BattleNetPacket packet)
		{
			LogOutgoingPacket(packet, wasMetered: false);
			lock (outBoundPackets)
			{
				outBoundPackets.Enqueue(packet);
				m_stopWatch.Reset();
				m_stopWatch.Start();
			}
		}

		protected void LogOutgoingPacket(BattleNetPacket packet, bool wasMetered)
		{
			if (m_logSource == null)
			{
				LogAdapter.Log(LogLevel.Warning, "tried to log with null log source, skipping");
				return;
			}
			bool flag = true;
			IProtoBuf protoBuf = (IProtoBuf)packet.GetBody();
			Header header = packet.GetHeader();
			uint methodId = header.MethodId;
			string text = (wasMetered ? "QueueRequest (METERED)" : "QueueRequest");
			if (!string.IsNullOrEmpty(protoBuf.ToString()))
			{
				ServiceDescriptor importedServiceByHash = m_serviceHelper.GetImportedServiceByHash(header.ServiceHash);
				m_logSource.LogDebug("{0}: type = {1}, header = {2}, request = {3}", text, (importedServiceByHash == null) ? "null" : importedServiceByHash.GetMethodName(methodId), header.ToString(), protoBuf.ToString());
			}
			else
			{
				string text2 = m_serviceHelper.GetImportedServiceByHash(header.ServiceHash)?.GetMethodName(methodId);
				if (text2 != "bnet.protocol.connection.ConnectionService.KeepAlive" && text2 != null)
				{
					m_logSource.LogDebug("{0}: type = {1}, header = {2}", text, text2, header.ToString());
				}
				else
				{
					flag = true;
				}
			}
			if (!flag)
			{
				m_logSource.LogDebugStackTrace("LogOutgoingPacket: ", 32, 1);
			}
		}

		protected void ProcessPendingOutboundPackets()
		{
			if (m_pendingOutboundPackets.Count <= 0)
			{
				return;
			}
			List<BattleNetPacket> list = new List<BattleNetPacket>();
			foreach (BattleNetPacket pendingOutboundPacket in m_pendingOutboundPackets)
			{
				Header header = pendingOutboundPacket.GetHeader();
				uint serviceHash = header.ServiceHash;
				uint methodId = header.MethodId;
				ServiceDescriptor exportedServiceDescriptor = GetExportedServiceDescriptor(serviceHash);
				if (m_connMetering.AllowRPCCall(exportedServiceDescriptor, methodId))
				{
					QueuePacket(pendingOutboundPacket);
				}
				else
				{
					list.Add(pendingOutboundPacket);
				}
			}
			m_pendingOutboundPackets = list;
		}

		protected void PrintHeader(Header h)
		{
			string text = $"Packet received: Header = [ ServiceId: {h.ServiceId}, ServiceHash: {h.ServiceHash}, MethodId: {h.MethodId} Token: {h.Token} Size: {h.Size} Status: {(BattleNetErrors)h.Status}";
			if (h.ErrorCount > 0)
			{
				text += " Error:[";
				foreach (ErrorInfo error in h.ErrorList)
				{
					text = text + " ErrorInfo{ " + error.ObjectAddress.Host.Label + "/" + error.ObjectAddress.Host.Epoch + "}";
				}
				text += "]";
			}
			text += "]";
			m_logSource.LogDebug(text);
		}

		private void OnConnectCallback(BattleNetErrors error)
		{
			if (m_onConnectHandler != null)
			{
				m_onConnectHandler(error);
			}
		}

		private void OnDisconnectCallback(BattleNetErrors error)
		{
			if (m_onDisconnectHandler != null)
			{
				m_onDisconnectHandler(error);
			}
		}

		private ServiceDescriptor GetImportedServiceDescriptor(uint serviceHash)
		{
			return m_serviceHelper.GetImportedServiceByHash(serviceHash);
		}

		protected ServiceDescriptor GetExportedServiceDescriptor(uint serviceHash)
		{
			return m_serviceHelper.GetExportedServiceByHash(serviceHash);
		}

		private void DownloadCompletedCallback(byte[] data, object userContext)
		{
			if (data == null)
			{
				m_cmLogSource.LogWarning("Downloading of the connection metering data failed!");
				return;
			}
			m_cmLogSource.LogDebug("Connection metering file downloaded. Length={0}", data.Length);
			m_connMetering.SetConnectionMeteringData(data, m_serviceHelper);
		}

		private string GetMethodName(Header header)
		{
			return GetMethodName(header, outgoing: true);
		}

		private string GetMethodName(Header header, bool outgoing)
		{
			if (header.ServiceId == 254)
			{
				return "Response";
			}
			ServiceDescriptor serviceDescriptor = null;
			serviceDescriptor = ((!outgoing) ? m_serviceHelper.GetExportedServiceByHash(header.ServiceHash) : m_serviceHelper.GetExportedServiceByHash(header.ServiceHash));
			if (serviceDescriptor != null)
			{
				return serviceDescriptor.GetMethodName(header.MethodId);
			}
			return "No Descriptor";
		}

		private string GetServiceName(Header header, bool outgoing)
		{
			if (header.ServiceId == 254)
			{
				return "Response";
			}
			ServiceDescriptor serviceDescriptor = null;
			serviceDescriptor = ((!outgoing) ? m_serviceHelper.GetExportedServiceByHash(header.ServiceHash) : m_serviceHelper.GetExportedServiceByHash(header.ServiceHash));
			if (serviceDescriptor != null)
			{
				return serviceDescriptor.Name;
			}
			return "No Descriptor";
		}

		public string PacketToString(BattleNetPacket packet, bool outgoing)
		{
			return PacketHeaderToString(packet.GetHeader(), outgoing);
		}

		public string PacketHeaderToString(Header header, bool outgoing)
		{
			string text = "";
			text = text + "Service:(" + header.ServiceId + ")" + GetServiceName(header, outgoing);
			text += " ";
			text = text + "Method:(" + (header.HasMethodId ? (header.MethodId + ")" + GetMethodName(header, outgoing)) : "?)");
			text += " ";
			text = text + "Token:" + header.Token;
			text += " ";
			text = text + "Status:" + (BattleNetErrors)header.Status;
			if (header.ErrorCount > 0)
			{
				text += " Error:[";
				foreach (ErrorInfo error in header.ErrorList)
				{
					text = text + " ErrorInfo{ " + error.ObjectAddress.Host.Label + "/" + error.ObjectAddress.Host.Epoch + "}";
				}
				text += "]";
			}
			return text;
		}
	}
}
