using bgs;

namespace Networking
{
	public interface IDebugConnectionManager
	{
		bool AllowDebugConnections();

		bool ShouldBroadcastDebugConnections();

		void SendDebugPacket(int packetId, IProtoBuf body);

		bool HaveDebugPackets();

		int NextDebugConsoleType();

		void Shutdown();

		bool IsActive();

		void Update();

		void OnLoginStarted();

		void DropPacket();

		int DropAllPackets();

		void AddListener(IClientConnectionListener<PegasusPacket> listener);

		PegasusPacket NextDebugPacket();

		bool TryConnectDebugConsole();

		void OnPacketReceived(PegasusPacket packet);

		void SendDebugConsoleResponse(int responseType, string message);
	}
}
