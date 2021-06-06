using System;
using System.Collections.Generic;
using System.Linq;
using bgs;
using BobNetProto;
using UnityEngine;

namespace Networking
{
	// Token: 0x02000FAB RID: 4011
	internal class DebugConnectionManager : IDebugConnectionManager
	{
		// Token: 0x0600AF66 RID: 44902 RVA: 0x0036583B File Offset: 0x00363A3B
		public DebugConnectionManager()
		{
			this.m_debugPackets = new Queue<PegasusPacket>();
			this.m_debugServerListener = new ServerConnection<PegasusPacket>();
			this.m_debugServerListener.Open(1226);
		}

		// Token: 0x0600AF67 RID: 44903 RVA: 0x0036586C File Offset: 0x00363A6C
		public bool TryConnectDebugConsole()
		{
			if (this.IsActive())
			{
				return true;
			}
			this.m_debugConnection = this.m_debugServerListener.GetNextAcceptedConnection();
			if (this.m_debugConnection == null)
			{
				return false;
			}
			if (this.m_connectionListener != null)
			{
				this.m_debugConnection.AddListener(this.m_connectionListener, ServerType.DEBUG_CONSOLE);
			}
			this.m_debugConnection.StartReceiving();
			return true;
		}

		// Token: 0x0600AF68 RID: 44904 RVA: 0x000052EC File Offset: 0x000034EC
		public bool AllowDebugConnections()
		{
			return true;
		}

		// Token: 0x0600AF69 RID: 44905 RVA: 0x003658C9 File Offset: 0x00363AC9
		public void OnPacketReceived(PegasusPacket packet)
		{
			this.m_debugPackets.Enqueue(packet);
		}

		// Token: 0x0600AF6A RID: 44906 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public bool ShouldBroadcastDebugConnections()
		{
			return false;
		}

		// Token: 0x0600AF6B RID: 44907 RVA: 0x003658D7 File Offset: 0x00363AD7
		public void SendDebugPacket(int packetId, IProtoBuf body)
		{
			this.m_debugConnection.SendPacket(new PegasusPacket(packetId, 0, body));
		}

		// Token: 0x0600AF6C RID: 44908 RVA: 0x003658EC File Offset: 0x00363AEC
		public bool HaveDebugPackets()
		{
			return this.m_debugPackets.Any<PegasusPacket>();
		}

		// Token: 0x0600AF6D RID: 44909 RVA: 0x003658F9 File Offset: 0x00363AF9
		public int NextDebugConsoleType()
		{
			return this.m_debugPackets.Peek().Type;
		}

		// Token: 0x0600AF6E RID: 44910 RVA: 0x0036590B File Offset: 0x00363B0B
		public void Shutdown()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_debugConnection.Disconnect();
			this.m_debugServerListener.Disconnect();
		}

		// Token: 0x0600AF6F RID: 44911 RVA: 0x0036592C File Offset: 0x00363B2C
		public bool IsActive()
		{
			return this.m_debugServerListener != null && this.m_debugConnection != null && this.m_debugConnection.Active;
		}

		// Token: 0x0600AF70 RID: 44912 RVA: 0x0036594B File Offset: 0x00363B4B
		public void OnLoginStarted()
		{
			this.SetupBroadcast();
		}

		// Token: 0x0600AF71 RID: 44913 RVA: 0x0036594B File Offset: 0x00363B4B
		public void Setup()
		{
			this.SetupBroadcast();
		}

		// Token: 0x0600AF72 RID: 44914 RVA: 0x00365953 File Offset: 0x00363B53
		public void Update()
		{
			this.m_debugConnection.Update();
		}

		// Token: 0x0600AF73 RID: 44915 RVA: 0x00365960 File Offset: 0x00363B60
		public void DropPacket()
		{
			this.m_debugPackets.Dequeue();
		}

		// Token: 0x0600AF74 RID: 44916 RVA: 0x0036596E File Offset: 0x00363B6E
		public int DropAllPackets()
		{
			int count = this.m_debugPackets.Count;
			this.m_debugPackets.Clear();
			return count;
		}

		// Token: 0x0600AF75 RID: 44917 RVA: 0x00365986 File Offset: 0x00363B86
		public void AddListener(IClientConnectionListener<PegasusPacket> listener)
		{
			this.m_connectionListener = listener;
		}

		// Token: 0x0600AF76 RID: 44918 RVA: 0x0036598F File Offset: 0x00363B8F
		public PegasusPacket NextDebugPacket()
		{
			return this.m_debugPackets.Peek();
		}

		// Token: 0x0600AF77 RID: 44919 RVA: 0x0036599C File Offset: 0x00363B9C
		public void SendDebugConsoleResponse(int responseType, string message)
		{
			if (message == null)
			{
				return;
			}
			if (!this.IsActive())
			{
				Debug.LogWarning("Cannot send console response " + message + "; no debug console is active.");
				return;
			}
			this.SendDebugPacket(124, new DebugConsoleResponse
			{
				ResponseType_ = (DebugConsoleResponse.ResponseType)responseType,
				Response = message
			});
		}

		// Token: 0x0600AF78 RID: 44920 RVA: 0x00003BE8 File Offset: 0x00001DE8
		private void SetupBroadcast()
		{
		}

		// Token: 0x040094F7 RID: 38135
		private const int DEBUG_CLIENT_TCP_PORT = 1226;

		// Token: 0x040094F8 RID: 38136
		private ClientConnection<PegasusPacket> m_debugConnection;

		// Token: 0x040094F9 RID: 38137
		private readonly Queue<PegasusPacket> m_debugPackets;

		// Token: 0x040094FA RID: 38138
		private readonly ServerConnection<PegasusPacket> m_debugServerListener;

		// Token: 0x040094FB RID: 38139
		private IClientConnectionListener<PegasusPacket> m_connectionListener;
	}
}
