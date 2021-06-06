using System;
using System.Collections.Generic;
using System.Linq;
using bgs;
using PegasusUtil;
using UnityEngine;

namespace Networking
{
	// Token: 0x02000FB2 RID: 4018
	public class QueueDispatcher : IDispatcher, IClientConnectionListener<PegasusPacket>
	{
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x0600AFBC RID: 44988 RVA: 0x00365C64 File Offset: 0x00363E64
		public IDebugConnectionManager DebugConnectionManager
		{
			get
			{
				return this.m_debugConnectionManager;
			}
		}

		// Token: 0x0600AFBD RID: 44989 RVA: 0x00365C6C File Offset: 0x00363E6C
		public QueueDispatcher(IDebugConnectionManager debugConnectionManager, IClientRequestManager clientRequestManager, IPacketDecoderManager packetDecoder, ISocketEventListener socketEventListener)
		{
			this.m_utilConnection = clientRequestManager;
			this.m_gameConnection = new QueueClientConnection<PegasusPacket>(socketEventListener);
			this.m_gameConnection.AddConnectHandler(new ConnectHandler(this.OnGameConnection));
			this.m_gameConnection.AddDisconnectHandler(new DisconnectHandler(this.OnGameDisconnect));
			this.m_gameConnection.AddListener(this, ServerType.GAME_SERVER);
			this.m_gamePackets = new Queue<PegasusPacket>();
			this.GameStartState = GameStartState.Invalid;
			this.m_debugConnectionManager = debugConnectionManager;
			this.m_debugConnectionManager.AddListener(this);
			this.m_packetDecoderManager = packetDecoder;
		}

		// Token: 0x0600AFBE RID: 44990 RVA: 0x00365D01 File Offset: 0x00363F01
		public void Close()
		{
			this.m_utilConnection.Terminate();
			this.m_utilConnection.Update();
			if (this.m_gameConnection != null)
			{
				this.m_gameConnection.Disconnect();
			}
		}

		// Token: 0x0600AFBF RID: 44991 RVA: 0x00365D2C File Offset: 0x00363F2C
		public void ResetForNewConnection()
		{
			this.m_gamePackets.Clear();
			this.m_utilConnection.SetDisconnectedFromBattleNet();
			this.GameStartState = GameStartState.Invalid;
		}

		// Token: 0x0600AFC0 RID: 44992 RVA: 0x00365D4C File Offset: 0x00363F4C
		public PegasusPacket DecodePacket(PegasusPacket packet)
		{
			if (!this.m_packetDecoderManager.CanDecodePacket(packet.Type))
			{
				global::Log.Net.PrintWarning("Could not find a packet decoder for a packet of type " + packet.Type, Array.Empty<object>());
				return null;
			}
			return this.m_packetDecoderManager.DecodePacket(packet);
		}

		// Token: 0x0600AFC1 RID: 44993 RVA: 0x00365DA0 File Offset: 0x00363FA0
		public void PacketReceived(PegasusPacket packet, object state)
		{
			if (!this.m_packetDecoderManager.CanDecodePacket(packet.Type))
			{
				Debug.LogError("Could not find a packet decoder for a packet of type " + packet.Type);
				return;
			}
			PegasusPacket pegasusPacket = this.m_packetDecoderManager.DecodePacket(packet);
			switch ((ServerType)state)
			{
			case ServerType.GAME_SERVER:
				this.OnGamePacketReceived(packet, packet.Type);
				return;
			case ServerType.UTIL_SERVER:
				this.OnUtilPacketReceived(pegasusPacket, packet.Type);
				return;
			case ServerType.DEBUG_CONSOLE:
				this.DebugConnectionManager.OnPacketReceived(pegasusPacket);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600AFC2 RID: 44994 RVA: 0x00365E2A File Offset: 0x0036402A
		public void SetDisconnectedFromBattleNet()
		{
			this.m_utilConnection.SetDisconnectedFromBattleNet();
		}

		// Token: 0x0600AFC3 RID: 44995 RVA: 0x00365E37 File Offset: 0x00364037
		public bool ShouldIgnoreError(BnetErrorInfo errorInfo)
		{
			return this.m_utilConnection.ShouldIgnoreError(errorInfo);
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x0600AFC4 RID: 44996 RVA: 0x00365E45 File Offset: 0x00364045
		// (set) Token: 0x0600AFC5 RID: 44997 RVA: 0x00365E4D File Offset: 0x0036404D
		public GameStartState GameStartState { get; set; }

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x0600AFC6 RID: 44998 RVA: 0x00365E56 File Offset: 0x00364056
		// (set) Token: 0x0600AFC7 RID: 44999 RVA: 0x00365E5E File Offset: 0x0036405E
		public int PingsSinceLastPong { get; set; }

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x0600AFC8 RID: 45000 RVA: 0x00365E67 File Offset: 0x00364067
		// (set) Token: 0x0600AFC9 RID: 45001 RVA: 0x00365E6F File Offset: 0x0036406F
		public double TimeLastPingReceived { get; set; }

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x0600AFCA RID: 45002 RVA: 0x00365E78 File Offset: 0x00364078
		// (set) Token: 0x0600AFCB RID: 45003 RVA: 0x00365E80 File Offset: 0x00364080
		public double TimeLastPingSent { get; set; }

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x0600AFCC RID: 45004 RVA: 0x00365E89 File Offset: 0x00364089
		// (set) Token: 0x0600AFCD RID: 45005 RVA: 0x00365E91 File Offset: 0x00364091
		public bool ShouldIgnorePong { get; set; }

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x0600AFCE RID: 45006 RVA: 0x00365E9A File Offset: 0x0036409A
		// (set) Token: 0x0600AFCF RID: 45007 RVA: 0x00365EA2 File Offset: 0x003640A2
		public bool SpoofDisconnected { get; set; }

		// Token: 0x0600AFD0 RID: 45008 RVA: 0x00365EAB File Offset: 0x003640AB
		public void AddGameServerConnectionListener(IClientConnectionListener<PegasusPacket> listener)
		{
			this.m_gameConnection.AddListener(listener, ServerType.GAME_SERVER);
		}

		// Token: 0x0600AFD1 RID: 45009 RVA: 0x00365EBF File Offset: 0x003640BF
		public bool ConnectToGameServer(string address, uint port)
		{
			this.m_gameConnection.Connect(address, port, new System.Random().Next(0, 9));
			return this.IsConnectedToGameServer();
		}

		// Token: 0x0600AFD2 RID: 45010 RVA: 0x00365EE1 File Offset: 0x003640E1
		public void DisconnectFromGameServer()
		{
			this.m_gameConnection.Disconnect();
		}

		// Token: 0x0600AFD3 RID: 45011 RVA: 0x00365EEE File Offset: 0x003640EE
		public int DropAllGamePackets()
		{
			int count = this.m_gamePackets.Count;
			this.m_gamePackets.Clear();
			return count;
		}

		// Token: 0x0600AFD4 RID: 45012 RVA: 0x00365F06 File Offset: 0x00364106
		public void DropGamePacket()
		{
			if (!this.m_gamePackets.Any<PegasusPacket>())
			{
				return;
			}
			this.m_gamePackets.Dequeue();
		}

		// Token: 0x0600AFD5 RID: 45013 RVA: 0x00365F22 File Offset: 0x00364122
		public bool GameServerHasEvents()
		{
			return this.m_gameConnection.HasEvents();
		}

		// Token: 0x0600AFD6 RID: 45014 RVA: 0x00365F2F File Offset: 0x0036412F
		public bool HasGamePackets()
		{
			return this.m_gamePackets.Count > 0;
		}

		// Token: 0x0600AFD7 RID: 45015 RVA: 0x00365F3F File Offset: 0x0036413F
		public bool HasGameServerConnection()
		{
			return this.m_gameConnection != null;
		}

		// Token: 0x0600AFD8 RID: 45016 RVA: 0x00365F4A File Offset: 0x0036414A
		public bool IsConnectedToGameServer()
		{
			return this.m_gameConnection != null && this.m_gameConnection.Active;
		}

		// Token: 0x0600AFD9 RID: 45017 RVA: 0x00365F61 File Offset: 0x00364161
		public PegasusPacket NextGamePacket()
		{
			if (this.m_gamePackets.Count <= 0)
			{
				return null;
			}
			return this.m_gamePackets.Peek();
		}

		// Token: 0x0600AFDA RID: 45018 RVA: 0x00365F7E File Offset: 0x0036417E
		public int NextGameType()
		{
			return this.m_gamePackets.Peek().Type;
		}

		// Token: 0x1400009F RID: 159
		// (add) Token: 0x0600AFDB RID: 45019 RVA: 0x00365F90 File Offset: 0x00364190
		// (remove) Token: 0x0600AFDC RID: 45020 RVA: 0x00365FC8 File Offset: 0x003641C8
		public event Action<BattleNetErrors> OnGameServerConnectEvent;

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x0600AFDD RID: 45021 RVA: 0x00366000 File Offset: 0x00364200
		// (remove) Token: 0x0600AFDE RID: 45022 RVA: 0x00366038 File Offset: 0x00364238
		public event Action<BattleNetErrors> OnGameServerDisconnectEvent;

		// Token: 0x0600AFDF RID: 45023 RVA: 0x0036606D File Offset: 0x0036426D
		private void OnGameConnection(BattleNetErrors error)
		{
			if (this.OnGameServerConnectEvent != null)
			{
				this.OnGameServerConnectEvent(error);
			}
		}

		// Token: 0x0600AFE0 RID: 45024 RVA: 0x00366083 File Offset: 0x00364283
		private void OnGameDisconnect(BattleNetErrors error)
		{
			if (this.OnGameServerDisconnectEvent != null)
			{
				this.OnGameServerDisconnectEvent(error);
			}
		}

		// Token: 0x0600AFE1 RID: 45025 RVA: 0x0036609C File Offset: 0x0036429C
		public void OnGamePacketReceived(PegasusPacket decodedPacket, int packetTypeId)
		{
			if (this.SpoofDisconnected)
			{
				return;
			}
			if (packetTypeId != 16)
			{
				if (packetTypeId == 116)
				{
					this.TimeLastPingReceived = bgs.TimeUtils.GetElapsedTimeSinceEpoch(null).TotalSeconds;
					if (!this.ShouldIgnorePong)
					{
						this.PingsSinceLastPong = 0;
					}
				}
			}
			else
			{
				this.GameStartState = GameStartState.Invalid;
			}
			if (decodedPacket != null)
			{
				this.m_gamePackets.Enqueue(decodedPacket);
			}
		}

		// Token: 0x0600AFE2 RID: 45026 RVA: 0x00366101 File Offset: 0x00364301
		public void ProcessGamePackets()
		{
			this.m_gameConnection.Update();
		}

		// Token: 0x0600AFE3 RID: 45027 RVA: 0x0036610E File Offset: 0x0036430E
		public void SendGamePacket(int packetId, IProtoBuf body)
		{
			if (this.SpoofDisconnected)
			{
				return;
			}
			this.m_gameConnection.SendPacket(new PegasusPacket(packetId, 0, body));
		}

		// Token: 0x0600AFE4 RID: 45028 RVA: 0x001B1A85 File Offset: 0x001AFC85
		public int DropAllUtilPackets()
		{
			return -1;
		}

		// Token: 0x0600AFE5 RID: 45029 RVA: 0x0036612C File Offset: 0x0036432C
		public bool HasUtilErrors()
		{
			return this.m_utilConnection.HasErrors();
		}

		// Token: 0x0600AFE6 RID: 45030 RVA: 0x00366139 File Offset: 0x00364339
		public void DropUtilPacket()
		{
			this.m_utilConnection.DropNextClientRequest();
		}

		// Token: 0x0600AFE7 RID: 45031 RVA: 0x00366146 File Offset: 0x00364346
		public bool HasUtilPackets()
		{
			return this.m_utilConnection.HasPendingDeliveryPackets();
		}

		// Token: 0x0600AFE8 RID: 45032 RVA: 0x00366153 File Offset: 0x00364353
		public ResponseWithRequest NextUtilPacket()
		{
			return this.m_utilConnection.GetNextClientRequest();
		}

		// Token: 0x0600AFE9 RID: 45033 RVA: 0x00366160 File Offset: 0x00364360
		public int NextUtilType()
		{
			return this.m_utilConnection.PeekNetClientRequestType();
		}

		// Token: 0x0600AFEA RID: 45034 RVA: 0x0036616D File Offset: 0x0036436D
		public void NotifyUtilResponseReceived(PegasusPacket packet)
		{
			this.m_utilConnection.NotifyResponseReceived(packet);
		}

		// Token: 0x0600AFEB RID: 45035 RVA: 0x0036617B File Offset: 0x0036437B
		public void OnLoginComplete()
		{
			this.m_utilConnection.NotifyLoginSequenceCompleted();
		}

		// Token: 0x0600AFEC RID: 45036 RVA: 0x00366188 File Offset: 0x00364388
		public void OnStartupPacketSequenceComplete()
		{
			this.m_utilConnection.NotifyStartupSequenceComplete();
		}

		// Token: 0x0600AFED RID: 45037 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void OnUtilPacketReceived(PegasusPacket decodedPacket, int packetTypeId)
		{
		}

		// Token: 0x0600AFEE RID: 45038 RVA: 0x00366195 File Offset: 0x00364395
		public void ProcessUtilPackets()
		{
			this.m_utilConnection.Update();
		}

		// Token: 0x0600AFEF RID: 45039 RVA: 0x003661A4 File Offset: 0x003643A4
		public void SendUtilPacket(int type, UtilSystemId system, IProtoBuf body, RequestPhase requestPhase = RequestPhase.RUNNING, int subId = 0)
		{
			if (this.SpoofDisconnected)
			{
				return;
			}
			if (!Network.ShouldBeConnectedToAurora())
			{
				FakeUtilHandler.FakeUtilOutbound(type, body, subId);
				return;
			}
			ClientRequestManager.ClientRequestConfig clientRequestConfig = null;
			if (system != UtilSystemId.CLIENT)
			{
				clientRequestConfig = new ClientRequestManager.ClientRequestConfig();
				clientRequestConfig.ShouldRetryOnError = false;
				clientRequestConfig.ShouldRetryOnUnhandled = true;
				clientRequestConfig.RequestedSystem = system;
			}
			if (!this.m_utilConnection.SendClientRequest(type, body, clientRequestConfig, requestPhase, subId))
			{
				return;
			}
			if (201 == type)
			{
				GetAccountInfo getAccountInfo = (GetAccountInfo)body;
				Network.Get().AddPendingRequestTimeout(type, (int)getAccountInfo.Request_);
				return;
			}
			Network.Get().AddPendingRequestTimeout(type, 0);
		}

		// Token: 0x0600AFF0 RID: 45040 RVA: 0x0036622E File Offset: 0x0036442E
		public void EnsureSubscribedTo(UtilSystemId system)
		{
			this.m_utilConnection.EnsureSubscribedTo(system);
		}

		// Token: 0x04009507 RID: 38151
		private readonly QueueClientConnection<PegasusPacket> m_gameConnection;

		// Token: 0x04009508 RID: 38152
		private readonly Queue<PegasusPacket> m_gamePackets;

		// Token: 0x04009509 RID: 38153
		private readonly IClientRequestManager m_utilConnection;

		// Token: 0x0400950A RID: 38154
		private readonly IDebugConnectionManager m_debugConnectionManager;

		// Token: 0x0400950B RID: 38155
		private readonly IPacketDecoderManager m_packetDecoderManager;
	}
}
