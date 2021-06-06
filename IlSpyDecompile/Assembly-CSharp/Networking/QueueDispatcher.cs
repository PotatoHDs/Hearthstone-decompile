using System;
using System.Collections.Generic;
using System.Linq;
using bgs;
using PegasusUtil;
using UnityEngine;

namespace Networking
{
	public class QueueDispatcher : IDispatcher, IClientConnectionListener<PegasusPacket>
	{
		private readonly QueueClientConnection<PegasusPacket> m_gameConnection;

		private readonly Queue<PegasusPacket> m_gamePackets;

		private readonly IClientRequestManager m_utilConnection;

		private readonly IDebugConnectionManager m_debugConnectionManager;

		private readonly IPacketDecoderManager m_packetDecoderManager;

		public IDebugConnectionManager DebugConnectionManager => m_debugConnectionManager;

		public GameStartState GameStartState { get; set; }

		public int PingsSinceLastPong { get; set; }

		public double TimeLastPingReceived { get; set; }

		public double TimeLastPingSent { get; set; }

		public bool ShouldIgnorePong { get; set; }

		public bool SpoofDisconnected { get; set; }

		public event Action<BattleNetErrors> OnGameServerConnectEvent;

		public event Action<BattleNetErrors> OnGameServerDisconnectEvent;

		public QueueDispatcher(IDebugConnectionManager debugConnectionManager, IClientRequestManager clientRequestManager, IPacketDecoderManager packetDecoder, ISocketEventListener socketEventListener)
		{
			m_utilConnection = clientRequestManager;
			m_gameConnection = new QueueClientConnection<PegasusPacket>(socketEventListener);
			m_gameConnection.AddConnectHandler(OnGameConnection);
			m_gameConnection.AddDisconnectHandler(OnGameDisconnect);
			m_gameConnection.AddListener(this, ServerType.GAME_SERVER);
			m_gamePackets = new Queue<PegasusPacket>();
			GameStartState = GameStartState.Invalid;
			m_debugConnectionManager = debugConnectionManager;
			m_debugConnectionManager.AddListener(this);
			m_packetDecoderManager = packetDecoder;
		}

		public void Close()
		{
			m_utilConnection.Terminate();
			m_utilConnection.Update();
			if (m_gameConnection != null)
			{
				m_gameConnection.Disconnect();
			}
		}

		public void ResetForNewConnection()
		{
			m_gamePackets.Clear();
			m_utilConnection.SetDisconnectedFromBattleNet();
			GameStartState = GameStartState.Invalid;
		}

		public PegasusPacket DecodePacket(PegasusPacket packet)
		{
			if (!m_packetDecoderManager.CanDecodePacket(packet.Type))
			{
				Log.Net.PrintWarning("Could not find a packet decoder for a packet of type " + packet.Type);
				return null;
			}
			return m_packetDecoderManager.DecodePacket(packet);
		}

		public void PacketReceived(PegasusPacket packet, object state)
		{
			if (m_packetDecoderManager.CanDecodePacket(packet.Type))
			{
				PegasusPacket pegasusPacket = m_packetDecoderManager.DecodePacket(packet);
				switch ((ServerType)state)
				{
				case ServerType.GAME_SERVER:
					OnGamePacketReceived(packet, packet.Type);
					break;
				case ServerType.UTIL_SERVER:
					OnUtilPacketReceived(pegasusPacket, packet.Type);
					break;
				case ServerType.DEBUG_CONSOLE:
					DebugConnectionManager.OnPacketReceived(pegasusPacket);
					break;
				}
			}
			else
			{
				Debug.LogError("Could not find a packet decoder for a packet of type " + packet.Type);
			}
		}

		public void SetDisconnectedFromBattleNet()
		{
			m_utilConnection.SetDisconnectedFromBattleNet();
		}

		public bool ShouldIgnoreError(BnetErrorInfo errorInfo)
		{
			return m_utilConnection.ShouldIgnoreError(errorInfo);
		}

		public void AddGameServerConnectionListener(IClientConnectionListener<PegasusPacket> listener)
		{
			m_gameConnection.AddListener(listener, ServerType.GAME_SERVER);
		}

		public bool ConnectToGameServer(string address, uint port)
		{
			m_gameConnection.Connect(address, port, new System.Random().Next(0, 9));
			return IsConnectedToGameServer();
		}

		public void DisconnectFromGameServer()
		{
			m_gameConnection.Disconnect();
		}

		public int DropAllGamePackets()
		{
			int count = m_gamePackets.Count;
			m_gamePackets.Clear();
			return count;
		}

		public void DropGamePacket()
		{
			if (m_gamePackets.Any())
			{
				m_gamePackets.Dequeue();
			}
		}

		public bool GameServerHasEvents()
		{
			return m_gameConnection.HasEvents();
		}

		public bool HasGamePackets()
		{
			return m_gamePackets.Count > 0;
		}

		public bool HasGameServerConnection()
		{
			return m_gameConnection != null;
		}

		public bool IsConnectedToGameServer()
		{
			if (m_gameConnection != null)
			{
				return m_gameConnection.Active;
			}
			return false;
		}

		public PegasusPacket NextGamePacket()
		{
			if (m_gamePackets.Count <= 0)
			{
				return null;
			}
			return m_gamePackets.Peek();
		}

		public int NextGameType()
		{
			return m_gamePackets.Peek().Type;
		}

		private void OnGameConnection(BattleNetErrors error)
		{
			if (this.OnGameServerConnectEvent != null)
			{
				this.OnGameServerConnectEvent(error);
			}
		}

		private void OnGameDisconnect(BattleNetErrors error)
		{
			if (this.OnGameServerDisconnectEvent != null)
			{
				this.OnGameServerDisconnectEvent(error);
			}
		}

		public void OnGamePacketReceived(PegasusPacket decodedPacket, int packetTypeId)
		{
			if (SpoofDisconnected)
			{
				return;
			}
			switch (packetTypeId)
			{
			case 16:
				GameStartState = GameStartState.Invalid;
				break;
			case 116:
				TimeLastPingReceived = bgs.TimeUtils.GetElapsedTimeSinceEpoch().TotalSeconds;
				if (!ShouldIgnorePong)
				{
					PingsSinceLastPong = 0;
				}
				break;
			}
			if (decodedPacket != null)
			{
				m_gamePackets.Enqueue(decodedPacket);
			}
		}

		public void ProcessGamePackets()
		{
			m_gameConnection.Update();
		}

		public void SendGamePacket(int packetId, IProtoBuf body)
		{
			if (!SpoofDisconnected)
			{
				m_gameConnection.SendPacket(new PegasusPacket(packetId, 0, body));
			}
		}

		public int DropAllUtilPackets()
		{
			return -1;
		}

		public bool HasUtilErrors()
		{
			return m_utilConnection.HasErrors();
		}

		public void DropUtilPacket()
		{
			m_utilConnection.DropNextClientRequest();
		}

		public bool HasUtilPackets()
		{
			return m_utilConnection.HasPendingDeliveryPackets();
		}

		public ResponseWithRequest NextUtilPacket()
		{
			return m_utilConnection.GetNextClientRequest();
		}

		public int NextUtilType()
		{
			return m_utilConnection.PeekNetClientRequestType();
		}

		public void NotifyUtilResponseReceived(PegasusPacket packet)
		{
			m_utilConnection.NotifyResponseReceived(packet);
		}

		public void OnLoginComplete()
		{
			m_utilConnection.NotifyLoginSequenceCompleted();
		}

		public void OnStartupPacketSequenceComplete()
		{
			m_utilConnection.NotifyStartupSequenceComplete();
		}

		public void OnUtilPacketReceived(PegasusPacket decodedPacket, int packetTypeId)
		{
		}

		public void ProcessUtilPackets()
		{
			m_utilConnection.Update();
		}

		public void SendUtilPacket(int type, UtilSystemId system, IProtoBuf body, RequestPhase requestPhase = RequestPhase.RUNNING, int subId = 0)
		{
			if (SpoofDisconnected)
			{
				return;
			}
			if (!Network.ShouldBeConnectedToAurora())
			{
				FakeUtilHandler.FakeUtilOutbound(type, body, subId);
				return;
			}
			ClientRequestManager.ClientRequestConfig clientRequestConfig = null;
			if (system != 0)
			{
				clientRequestConfig = new ClientRequestManager.ClientRequestConfig();
				clientRequestConfig.ShouldRetryOnError = false;
				clientRequestConfig.ShouldRetryOnUnhandled = true;
				clientRequestConfig.RequestedSystem = system;
			}
			if (m_utilConnection.SendClientRequest(type, body, clientRequestConfig, requestPhase, subId))
			{
				if (201 == type)
				{
					GetAccountInfo getAccountInfo = (GetAccountInfo)body;
					Network.Get().AddPendingRequestTimeout(type, (int)getAccountInfo.Request_);
				}
				else
				{
					Network.Get().AddPendingRequestTimeout(type, 0);
				}
			}
		}

		public void EnsureSubscribedTo(UtilSystemId system)
		{
			m_utilConnection.EnsureSubscribedTo(system);
		}
	}
}
