using System;
using bgs;

namespace Networking
{
	// Token: 0x02000FAD RID: 4013
	public interface IDebugConnectionManager
	{
		// Token: 0x0600AF79 RID: 44921
		bool AllowDebugConnections();

		// Token: 0x0600AF7A RID: 44922
		bool ShouldBroadcastDebugConnections();

		// Token: 0x0600AF7B RID: 44923
		void SendDebugPacket(int packetId, IProtoBuf body);

		// Token: 0x0600AF7C RID: 44924
		bool HaveDebugPackets();

		// Token: 0x0600AF7D RID: 44925
		int NextDebugConsoleType();

		// Token: 0x0600AF7E RID: 44926
		void Shutdown();

		// Token: 0x0600AF7F RID: 44927
		bool IsActive();

		// Token: 0x0600AF80 RID: 44928
		void Update();

		// Token: 0x0600AF81 RID: 44929
		void OnLoginStarted();

		// Token: 0x0600AF82 RID: 44930
		void DropPacket();

		// Token: 0x0600AF83 RID: 44931
		int DropAllPackets();

		// Token: 0x0600AF84 RID: 44932
		void AddListener(IClientConnectionListener<PegasusPacket> listener);

		// Token: 0x0600AF85 RID: 44933
		PegasusPacket NextDebugPacket();

		// Token: 0x0600AF86 RID: 44934
		bool TryConnectDebugConsole();

		// Token: 0x0600AF87 RID: 44935
		void OnPacketReceived(PegasusPacket packet);

		// Token: 0x0600AF88 RID: 44936
		void SendDebugConsoleResponse(int responseType, string message);
	}
}
