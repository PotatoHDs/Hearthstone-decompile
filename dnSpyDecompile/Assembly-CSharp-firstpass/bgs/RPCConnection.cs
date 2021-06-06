using System;
using System.Collections.Generic;
using System.Diagnostics;
using bgs.RPCServices;
using bnet.protocol;
using bnet.protocol.connection.v1;

namespace bgs
{
	// Token: 0x02000232 RID: 562
	public class RPCConnection : IRpcConnection, IClientConnectionListener<BattleNetPacket>
	{
		// Token: 0x06002371 RID: 9073 RVA: 0x0007C194 File Offset: 0x0007A394
		public RPCConnection(IFileUtil fileUtil, IJsonSerializer jsonSerializer, ISocketEventListener socketEventListener)
		{
			this.m_fileUtil = fileUtil;
			this.m_jsonSerializer = jsonSerializer;
			this.m_socketEventListener = socketEventListener;
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x0007C229 File Offset: 0x0007A429
		public long GetMillisecondsSinceLastPacketSent()
		{
			return this.m_stopWatch.ElapsedMilliseconds;
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x0007C236 File Offset: 0x0007A436
		public ServiceCollectionHelper GetServiceHelper()
		{
			return this.m_serviceHelper;
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x0007C23E File Offset: 0x0007A43E
		public void SetOnConnectHandler(OnConnectHandler handler)
		{
			this.m_onConnectHandler = handler;
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x0007C247 File Offset: 0x0007A447
		public void SetOnDisconnectHandler(OnDisconnectHandler handler)
		{
			this.m_onDisconnectHandler = handler;
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x0007C250 File Offset: 0x0007A450
		public void Connect(string host, uint port, SslParameters sslParams, int tryCount)
		{
			this.m_stopWatch = new Stopwatch();
			if (sslParams.useSsl)
			{
				this.Connection = new QueueSslClientConnection(sslParams.bundleSettings, this.m_fileUtil, this.m_jsonSerializer, this.m_socketEventListener)
				{
					OnlyOneSend = true
				};
			}
			else
			{
				this.Connection = new QueueClientConnection<BattleNetPacket>(this.m_socketEventListener);
			}
			this.Connection.AddListener(this, null);
			this.Connection.AddConnectHandler(new ConnectHandler(this.OnConnectCallback));
			this.Connection.AddDisconnectHandler(new DisconnectHandler(this.OnDisconnectCallback));
			this.Connection.Connect(host, port, tryCount);
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x0007C2FC File Offset: 0x0007A4FC
		public void Disconnect()
		{
			if (this.Connection == null)
			{
				this.m_logSource.LogError("Disconnect ignored: Connection is null. Probably couldn't talk to the server or get an ip address for it from DNS");
				return;
			}
			if (this.Connection is SslClientConnection)
			{
				((SslClientConnection)this.Connection).BlockOnSend = true;
			}
			this.Update();
			this.Connection.Disconnect();
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x00003FD0 File Offset: 0x000021D0
		public void BeginAuth()
		{
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x0007C351 File Offset: 0x0007A551
		public bool GetInStartupPeriod()
		{
			return this.m_connMetering.GetInStartupPeriod();
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x0007C35E File Offset: 0x0007A55E
		public RPCContext SendRequest(ServiceDescriptor service, uint methodId, IProtoBuf message, RPCContextDelegate callback = null, uint objectId = 0U)
		{
			return this.QueueRequest(service, methodId, message, callback, objectId);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x0007C370 File Offset: 0x0007A570
		public RPCContext QueueRequest(ServiceDescriptor service, uint methodId, IProtoBuf message, RPCContextDelegate callback = null, uint objectId = 0U)
		{
			if (message == null)
			{
				return null;
			}
			if (!service.Imported)
			{
				this.m_logSource.LogError("Try sending message to a service which is not imported name={0} hash={0} method={0}", new object[]
				{
					service.Name,
					service.Hash,
					methodId
				});
				return null;
			}
			object obj = this.tokenLock;
			uint num;
			lock (obj)
			{
				num = RPCConnection.nextToken;
				RPCConnection.nextToken += 1U;
			}
			RPCContext rpccontext = new RPCContext();
			if (callback != null)
			{
				rpccontext.Callback = callback;
				this.waitingForResponse.Add(num, rpccontext);
			}
			Header header = this.CreateHeader(service.Id, service.Hash, methodId, objectId, num, message.GetSerializedSize());
			BattleNetPacket battleNetPacket = new BattleNetPacket(header, message);
			rpccontext.Header = header;
			rpccontext.Request = message;
			if (!this.m_connMetering.AllowRPCCall(service, methodId))
			{
				this.m_pendingOutboundPackets.Add(battleNetPacket);
				this.LogOutgoingPacket(battleNetPacket, true);
			}
			else
			{
				this.QueuePacket(battleNetPacket);
			}
			return rpccontext;
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x0007C488 File Offset: 0x0007A688
		public void SendResponse(RPCContext context, IProtoBuf message)
		{
			this.QueueResponse(context, message);
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x0007C494 File Offset: 0x0007A694
		public void QueueResponse(RPCContext context, IProtoBuf message)
		{
			if (message == null || context.Header == null)
			{
				this.m_logSource.LogError("QueueResponse: invalid response");
				return;
			}
			if (this.m_serviceHelper.GetImportedServiceByHash(context.Header.ServiceHash) == null)
			{
				this.m_logSource.LogError("QueueResponse: error, unrecognized service hash: " + context.Header.ServiceHash);
				return;
			}
			this.m_logSource.LogDebug(string.Concat(new object[]
			{
				"QueueResponse: type=",
				this.m_serviceHelper.GetImportedServiceByHash(context.Header.ServiceHash).GetMethodName(context.Header.MethodId),
				" data=",
				message
			}));
			Header header = context.Header;
			header.SetServiceId(254U);
			header.SetMethodId(0U);
			header.SetSize(message.GetSerializedSize());
			context.Header = header;
			BattleNetPacket packet = new BattleNetPacket(context.Header, message);
			this.QueuePacket(packet);
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x0007C590 File Offset: 0x0007A790
		public void RegisterServiceMethodListener(uint serviceId, uint methodId, RPCContextDelegate callback)
		{
			ServiceDescriptor exportedServiceDescriptor = this.GetExportedServiceDescriptor(serviceId);
			if (exportedServiceDescriptor != null)
			{
				exportedServiceDescriptor.RegisterMethodListener(methodId, callback);
			}
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x0007C5B0 File Offset: 0x0007A7B0
		public void Update()
		{
			this.ProcessPendingOutboundPackets();
			if (this.outBoundPackets.Count > 0)
			{
				Queue<BattleNetPacket> obj = this.outBoundPackets;
				Queue<BattleNetPacket> queue;
				lock (obj)
				{
					queue = new Queue<BattleNetPacket>(this.outBoundPackets.ToArray());
					this.outBoundPackets.Clear();
					goto IL_112;
				}
				IL_53:
				BattleNetPacket battleNetPacket = queue.Dequeue();
				if (this.Connection != null)
				{
					Header header = battleNetPacket.GetHeader();
					this.m_logSource.LogDebug(string.Format("Packet sent: Header = ServiceId: {0}, ServiceHash: {1}, MethodId: {2} Token: {3} Size: {4}", new object[]
					{
						header.ServiceId,
						header.ServiceHash,
						header.MethodId,
						header.Token,
						header.Size
					}));
					this.Connection.SendPacket(battleNetPacket);
					if (battleNetPacket.GetHeader() != null && battleNetPacket.GetHeader().MethodId == 1U)
					{
						this.m_connMetering.SetConnectPacketSentToNow();
					}
				}
				else
				{
					this.m_logSource.LogError("##Client Connection object does not exists!##");
				}
				IL_112:
				if (queue.Count > 0)
				{
					goto IL_53;
				}
			}
			if (this.Connection != null)
			{
				this.Connection.Update();
			}
			if (this.incomingPackets.Count > 0)
			{
				Queue<BattleNetPacket> obj = this.incomingPackets;
				Queue<BattleNetPacket> queue2;
				lock (obj)
				{
					queue2 = new Queue<BattleNetPacket>(this.incomingPackets.ToArray());
					this.incomingPackets.Clear();
					goto IL_4EE;
				}
				IL_17F:
				BattleNetPacket battleNetPacket2 = queue2.Dequeue();
				Header header2 = battleNetPacket2.GetHeader();
				this.PrintHeader(header2);
				byte[] payload = (byte[])battleNetPacket2.GetBody();
				if (header2.ServiceId == 254U)
				{
					RPCContext rpccontext;
					if (this.waitingForResponse.TryGetValue(header2.Token, out rpccontext))
					{
						ServiceDescriptor importedServiceByHash = this.m_serviceHelper.GetImportedServiceByHash(rpccontext.Header.ServiceHash);
						MethodDescriptor.ParseMethod parseMethod = null;
						if (importedServiceByHash != null)
						{
							parseMethod = importedServiceByHash.GetParser(rpccontext.Header.MethodId);
						}
						if (parseMethod == null)
						{
							if (importedServiceByHash != null)
							{
								this.m_logSource.LogWarning("Incoming Response: Unable to find method for serviceName={0} method id={1}", new object[]
								{
									importedServiceByHash.Name,
									rpccontext.Header.MethodId
								});
								int methodCount = importedServiceByHash.GetMethodCount();
								this.m_logSource.LogDebug("  Found {0} methods", new object[]
								{
									methodCount
								});
								for (int i = 0; i < methodCount; i++)
								{
									MethodDescriptor methodDescriptor = importedServiceByHash.GetMethodDescriptor((uint)i);
									if (methodDescriptor == null && i != 0)
									{
										this.m_logSource.LogDebug("  Found method id={0} name={1}", new object[]
										{
											i,
											"<null>"
										});
									}
									else
									{
										this.m_logSource.LogDebug("  Found method id={0} name={1}", new object[]
										{
											i,
											methodDescriptor.Name
										});
									}
								}
							}
							else
							{
								this.m_logSource.LogWarning("Incoming Response: Unable to identify service id={0}", new object[]
								{
									rpccontext.Header.ServiceId
								});
							}
						}
						rpccontext.Header = header2;
						rpccontext.Payload = payload;
						rpccontext.ResponseReceived = true;
						if (rpccontext.Callback != null)
						{
							rpccontext.Callback(rpccontext);
						}
						this.waitingForResponse.Remove(header2.Token);
					}
				}
				else
				{
					ServiceDescriptor serviceDescriptor = null;
					if (header2.HasServiceHash && header2.ServiceHash != 0U)
					{
						serviceDescriptor = this.GetExportedServiceDescriptor(header2.ServiceHash);
					}
					if (serviceDescriptor == null)
					{
						serviceDescriptor = this.GetExportedServiceDescriptor(header2.ServiceId);
					}
					if (serviceDescriptor != null)
					{
						if (serviceDescriptor.GetParser(header2.MethodId) == null)
						{
							this.m_logSource.LogDebug(string.Concat(new object[]
							{
								"Incoming Packet: NULL TYPE service=",
								serviceDescriptor.Name,
								", methodId=",
								header2.MethodId
							}));
						}
						if (serviceDescriptor.HasMethodListener(header2.MethodId))
						{
							serviceDescriptor.NotifyMethodListener(new RPCContext
							{
								Header = header2,
								Payload = payload,
								ResponseReceived = true
							});
						}
						else
						{
							string text = (serviceDescriptor != null && !string.IsNullOrEmpty(serviceDescriptor.Name)) ? serviceDescriptor.Name : "<null>";
							this.m_logSource.LogError(string.Concat(new object[]
							{
								"[!]Unhandled Server Request Received (Service Name: ",
								text,
								" Service hash:",
								header2.ServiceHash,
								" Method id:",
								header2.MethodId,
								")"
							}));
						}
					}
					else
					{
						this.m_logSource.LogError(string.Concat(new object[]
						{
							"[!]Server Requested an Unsupported (Service hash:",
							header2.ServiceHash,
							" Method id:",
							header2.MethodId,
							")"
						}));
					}
				}
				IL_4EE:
				if (queue2.Count > 0)
				{
					goto IL_17F;
				}
			}
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x0007CAD4 File Offset: 0x0007ACD4
		public void PacketReceived(BattleNetPacket p, object state)
		{
			Queue<BattleNetPacket> obj = this.incomingPackets;
			lock (obj)
			{
				this.incomingPackets.Enqueue(p);
			}
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x0007CB1C File Offset: 0x0007AD1C
		public void SetConnectionMeteringContentHandles(ConnectionMeteringContentHandles handles, LocalStorageAPI localStorage)
		{
			if (handles == null || !handles.IsInitialized || handles.ContentHandleCount == 0)
			{
				this.m_cmLogSource.LogWarning("Invalid connection metering content handle received.");
				return;
			}
			if (handles.ContentHandleCount != 1)
			{
				this.m_cmLogSource.LogWarning("More than 1 connection metering content handle specified!");
			}
			ContentHandle contentHandle = handles.ContentHandle[0];
			if (contentHandle == null || !contentHandle.IsInitialized)
			{
				this.m_cmLogSource.LogWarning("The content handle received is not valid!");
				return;
			}
			this.m_cmLogSource.LogDebug("Received request to enable connection metering.");
			ContentHandle contentHandle2 = ContentHandle.FromProtocol(contentHandle);
			this.m_cmLogSource.LogDebug("Requesting file from local storage. ContentHandle={0}", new object[]
			{
				contentHandle2
			});
			localStorage.GetFile(contentHandle2, new LocalStorageAPI.DownloadCompletedCallback(this.DownloadCompletedCallback), null);
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x0007CBD8 File Offset: 0x0007ADD8
		protected Header CreateHeader(uint serviceId, uint serviceHash, uint methodId, uint objectId, uint token, uint size)
		{
			Header header = new Header();
			header.SetServiceId(serviceId);
			header.SetServiceHash(serviceHash);
			header.SetMethodId(methodId);
			if (objectId != 0U)
			{
				header.SetObjectId((ulong)objectId);
			}
			header.SetToken(token);
			header.SetSize(size);
			return header;
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x0007CC20 File Offset: 0x0007AE20
		protected void QueuePacket(BattleNetPacket packet)
		{
			this.LogOutgoingPacket(packet, false);
			Queue<BattleNetPacket> obj = this.outBoundPackets;
			lock (obj)
			{
				this.outBoundPackets.Enqueue(packet);
				this.m_stopWatch.Reset();
				this.m_stopWatch.Start();
			}
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x0007CC84 File Offset: 0x0007AE84
		protected void LogOutgoingPacket(BattleNetPacket packet, bool wasMetered)
		{
			if (this.m_logSource == null)
			{
				LogAdapter.Log(LogLevel.Warning, "tried to log with null log source, skipping", "");
				return;
			}
			bool flag = true;
			IProtoBuf protoBuf = (IProtoBuf)packet.GetBody();
			Header header = packet.GetHeader();
			uint methodId = header.MethodId;
			string text = wasMetered ? "QueueRequest (METERED)" : "QueueRequest";
			if (!string.IsNullOrEmpty(protoBuf.ToString()))
			{
				ServiceDescriptor importedServiceByHash = this.m_serviceHelper.GetImportedServiceByHash(header.ServiceHash);
				this.m_logSource.LogDebug("{0}: type = {1}, header = {2}, request = {3}", new object[]
				{
					text,
					(importedServiceByHash == null) ? "null" : importedServiceByHash.GetMethodName(methodId),
					header.ToString(),
					protoBuf.ToString()
				});
			}
			else
			{
				ServiceDescriptor importedServiceByHash2 = this.m_serviceHelper.GetImportedServiceByHash(header.ServiceHash);
				string text2 = (importedServiceByHash2 == null) ? null : importedServiceByHash2.GetMethodName(methodId);
				if (text2 != "bnet.protocol.connection.ConnectionService.KeepAlive" && text2 != null)
				{
					this.m_logSource.LogDebug("{0}: type = {1}, header = {2}", new object[]
					{
						text,
						text2,
						header.ToString()
					});
				}
				else
				{
					flag = true;
				}
			}
			if (!flag)
			{
				this.m_logSource.LogDebugStackTrace("LogOutgoingPacket: ", 32, 1);
			}
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x0007CDB8 File Offset: 0x0007AFB8
		protected void ProcessPendingOutboundPackets()
		{
			if (this.m_pendingOutboundPackets.Count > 0)
			{
				List<BattleNetPacket> list = new List<BattleNetPacket>();
				foreach (BattleNetPacket battleNetPacket in this.m_pendingOutboundPackets)
				{
					Header header = battleNetPacket.GetHeader();
					uint serviceHash = header.ServiceHash;
					uint methodId = header.MethodId;
					ServiceDescriptor exportedServiceDescriptor = this.GetExportedServiceDescriptor(serviceHash);
					if (this.m_connMetering.AllowRPCCall(exportedServiceDescriptor, methodId))
					{
						this.QueuePacket(battleNetPacket);
					}
					else
					{
						list.Add(battleNetPacket);
					}
				}
				this.m_pendingOutboundPackets = list;
			}
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x0007CE60 File Offset: 0x0007B060
		protected void PrintHeader(Header h)
		{
			string text = string.Format("Packet received: Header = [ ServiceId: {0}, ServiceHash: {1}, MethodId: {2} Token: {3} Size: {4} Status: {5}", new object[]
			{
				h.ServiceId,
				h.ServiceHash,
				h.MethodId,
				h.Token,
				h.Size,
				(BattleNetErrors)h.Status
			});
			if (h.ErrorCount > 0)
			{
				text += " Error:[";
				foreach (ErrorInfo errorInfo in h.ErrorList)
				{
					text = string.Concat(new object[]
					{
						text,
						" ErrorInfo{ ",
						errorInfo.ObjectAddress.Host.Label,
						"/",
						errorInfo.ObjectAddress.Host.Epoch,
						"}"
					});
				}
				text += "]";
			}
			text += "]";
			this.m_logSource.LogDebug(text);
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x0007CFA8 File Offset: 0x0007B1A8
		private void OnConnectCallback(BattleNetErrors error)
		{
			if (this.m_onConnectHandler != null)
			{
				this.m_onConnectHandler(error);
			}
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x0007CFBE File Offset: 0x0007B1BE
		private void OnDisconnectCallback(BattleNetErrors error)
		{
			if (this.m_onDisconnectHandler != null)
			{
				this.m_onDisconnectHandler(error);
			}
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x0007CFD4 File Offset: 0x0007B1D4
		private ServiceDescriptor GetImportedServiceDescriptor(uint serviceHash)
		{
			return this.m_serviceHelper.GetImportedServiceByHash(serviceHash);
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x0007CFE2 File Offset: 0x0007B1E2
		protected ServiceDescriptor GetExportedServiceDescriptor(uint serviceHash)
		{
			return this.m_serviceHelper.GetExportedServiceByHash(serviceHash);
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x0007CFF0 File Offset: 0x0007B1F0
		private void DownloadCompletedCallback(byte[] data, object userContext)
		{
			if (data == null)
			{
				this.m_cmLogSource.LogWarning("Downloading of the connection metering data failed!");
				return;
			}
			this.m_cmLogSource.LogDebug("Connection metering file downloaded. Length={0}", new object[]
			{
				data.Length
			});
			this.m_connMetering.SetConnectionMeteringData(data, this.m_serviceHelper);
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x0007D044 File Offset: 0x0007B244
		private string GetMethodName(Header header)
		{
			return this.GetMethodName(header, true);
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x0007D050 File Offset: 0x0007B250
		private string GetMethodName(Header header, bool outgoing)
		{
			if (header.ServiceId == 254U)
			{
				return "Response";
			}
			ServiceDescriptor exportedServiceByHash;
			if (outgoing)
			{
				exportedServiceByHash = this.m_serviceHelper.GetExportedServiceByHash(header.ServiceHash);
			}
			else
			{
				exportedServiceByHash = this.m_serviceHelper.GetExportedServiceByHash(header.ServiceHash);
			}
			if (exportedServiceByHash != null)
			{
				return exportedServiceByHash.GetMethodName(header.MethodId);
			}
			return "No Descriptor";
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x0007D0B0 File Offset: 0x0007B2B0
		private string GetServiceName(Header header, bool outgoing)
		{
			if (header.ServiceId == 254U)
			{
				return "Response";
			}
			ServiceDescriptor exportedServiceByHash;
			if (outgoing)
			{
				exportedServiceByHash = this.m_serviceHelper.GetExportedServiceByHash(header.ServiceHash);
			}
			else
			{
				exportedServiceByHash = this.m_serviceHelper.GetExportedServiceByHash(header.ServiceHash);
			}
			if (exportedServiceByHash != null)
			{
				return exportedServiceByHash.Name;
			}
			return "No Descriptor";
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x0007D10A File Offset: 0x0007B30A
		public string PacketToString(BattleNetPacket packet, bool outgoing)
		{
			return this.PacketHeaderToString(packet.GetHeader(), outgoing);
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x0007D11C File Offset: 0x0007B31C
		public string PacketHeaderToString(Header header, bool outgoing)
		{
			string text = "";
			text = string.Concat(new object[]
			{
				text,
				"Service:(",
				header.ServiceId,
				")",
				this.GetServiceName(header, outgoing)
			});
			text += " ";
			text = text + "Method:(" + (header.HasMethodId ? (header.MethodId + ")" + this.GetMethodName(header, outgoing)) : "?)");
			text += " ";
			text = text + "Token:" + header.Token;
			text += " ";
			text = text + "Status:" + (BattleNetErrors)header.Status;
			if (header.ErrorCount > 0)
			{
				text += " Error:[";
				foreach (ErrorInfo errorInfo in header.ErrorList)
				{
					text = string.Concat(new object[]
					{
						text,
						" ErrorInfo{ ",
						errorInfo.ObjectAddress.Host.Label,
						"/",
						errorInfo.ObjectAddress.Host.Epoch,
						"}"
					});
				}
				text += "]";
			}
			return text;
		}

		// Token: 0x04000E82 RID: 3714
		protected const int RESPONSE_SERVICE_ID = 254;

		// Token: 0x04000E83 RID: 3715
		protected BattleNetLogSource m_logSource = new BattleNetLogSource("Network");

		// Token: 0x04000E84 RID: 3716
		protected BattleNetLogSource m_cmLogSource = new BattleNetLogSource("ConnectionMetering");

		// Token: 0x04000E85 RID: 3717
		protected IClientConnection<BattleNetPacket> Connection;

		// Token: 0x04000E86 RID: 3718
		protected ServiceCollectionHelper m_serviceHelper = new ServiceCollectionHelper();

		// Token: 0x04000E87 RID: 3719
		private Queue<BattleNetPacket> outBoundPackets = new Queue<BattleNetPacket>();

		// Token: 0x04000E88 RID: 3720
		private Queue<BattleNetPacket> incomingPackets = new Queue<BattleNetPacket>();

		// Token: 0x04000E89 RID: 3721
		private List<BattleNetPacket> m_pendingOutboundPackets = new List<BattleNetPacket>();

		// Token: 0x04000E8A RID: 3722
		protected object tokenLock = new object();

		// Token: 0x04000E8B RID: 3723
		protected static uint nextToken;

		// Token: 0x04000E8C RID: 3724
		protected Dictionary<uint, RPCContext> waitingForResponse = new Dictionary<uint, RPCContext>();

		// Token: 0x04000E8D RID: 3725
		protected OnConnectHandler m_onConnectHandler;

		// Token: 0x04000E8E RID: 3726
		protected OnDisconnectHandler m_onDisconnectHandler;

		// Token: 0x04000E8F RID: 3727
		protected Stopwatch m_stopWatch;

		// Token: 0x04000E90 RID: 3728
		protected readonly IFileUtil m_fileUtil;

		// Token: 0x04000E91 RID: 3729
		protected readonly IJsonSerializer m_jsonSerializer;

		// Token: 0x04000E92 RID: 3730
		protected RPCConnectionMetering m_connMetering = new RPCConnectionMetering();

		// Token: 0x04000E93 RID: 3731
		private readonly ISocketEventListener m_socketEventListener;
	}
}
