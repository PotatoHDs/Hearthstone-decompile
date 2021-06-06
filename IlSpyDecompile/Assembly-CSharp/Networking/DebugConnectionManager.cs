using System.Collections.Generic;
using System.Linq;
using bgs;
using BobNetProto;
using UnityEngine;

namespace Networking
{
	internal class DebugConnectionManager : IDebugConnectionManager
	{
		private const int DEBUG_CLIENT_TCP_PORT = 1226;

		private ClientConnection<PegasusPacket> m_debugConnection;

		private readonly Queue<PegasusPacket> m_debugPackets;

		private readonly ServerConnection<PegasusPacket> m_debugServerListener;

		private IClientConnectionListener<PegasusPacket> m_connectionListener;

		public DebugConnectionManager()
		{
			m_debugPackets = new Queue<PegasusPacket>();
			m_debugServerListener = new ServerConnection<PegasusPacket>();
			m_debugServerListener.Open(1226);
		}

		public bool TryConnectDebugConsole()
		{
			if (IsActive())
			{
				return true;
			}
			m_debugConnection = m_debugServerListener.GetNextAcceptedConnection();
			if (m_debugConnection == null)
			{
				return false;
			}
			if (m_connectionListener != null)
			{
				m_debugConnection.AddListener(m_connectionListener, ServerType.DEBUG_CONSOLE);
			}
			m_debugConnection.StartReceiving();
			return true;
		}

		public bool AllowDebugConnections()
		{
			return true;
		}

		public void OnPacketReceived(PegasusPacket packet)
		{
			m_debugPackets.Enqueue(packet);
		}

		public bool ShouldBroadcastDebugConnections()
		{
			return false;
		}

		public void SendDebugPacket(int packetId, IProtoBuf body)
		{
			m_debugConnection.SendPacket(new PegasusPacket(packetId, 0, body));
		}

		public bool HaveDebugPackets()
		{
			return m_debugPackets.Any();
		}

		public int NextDebugConsoleType()
		{
			return m_debugPackets.Peek().Type;
		}

		public void Shutdown()
		{
			if (IsActive())
			{
				m_debugConnection.Disconnect();
				m_debugServerListener.Disconnect();
			}
		}

		public bool IsActive()
		{
			if (m_debugServerListener != null && m_debugConnection != null)
			{
				return m_debugConnection.Active;
			}
			return false;
		}

		public void OnLoginStarted()
		{
			SetupBroadcast();
		}

		public void Setup()
		{
			SetupBroadcast();
		}

		public void Update()
		{
			m_debugConnection.Update();
		}

		public void DropPacket()
		{
			m_debugPackets.Dequeue();
		}

		public int DropAllPackets()
		{
			int count = m_debugPackets.Count;
			m_debugPackets.Clear();
			return count;
		}

		public void AddListener(IClientConnectionListener<PegasusPacket> listener)
		{
			m_connectionListener = listener;
		}

		public PegasusPacket NextDebugPacket()
		{
			return m_debugPackets.Peek();
		}

		public void SendDebugConsoleResponse(int responseType, string message)
		{
			if (message != null)
			{
				if (!IsActive())
				{
					Debug.LogWarning("Cannot send console response " + message + "; no debug console is active.");
					return;
				}
				SendDebugPacket(124, new DebugConsoleResponse
				{
					ResponseType_ = (DebugConsoleResponse.ResponseType)responseType,
					Response = message
				});
			}
		}

		private void SetupBroadcast()
		{
		}
	}
}
