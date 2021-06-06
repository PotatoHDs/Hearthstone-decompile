using System;
using bgs;

namespace Networking
{
	public interface IDispatcher
	{
		IDebugConnectionManager DebugConnectionManager { get; }

		GameStartState GameStartState { get; set; }

		int PingsSinceLastPong { get; set; }

		double TimeLastPingReceived { get; set; }

		double TimeLastPingSent { get; set; }

		bool ShouldIgnorePong { get; set; }

		bool SpoofDisconnected { get; set; }

		event Action<BattleNetErrors> OnGameServerConnectEvent;

		event Action<BattleNetErrors> OnGameServerDisconnectEvent;

		void Close();

		PegasusPacket DecodePacket(PegasusPacket packet);

		void SetDisconnectedFromBattleNet();

		bool ShouldIgnoreError(BnetErrorInfo errorInfo);

		void ResetForNewConnection();

		bool ConnectToGameServer(string address, uint port);

		void DisconnectFromGameServer();

		int DropAllGamePackets();

		void DropGamePacket();

		bool GameServerHasEvents();

		bool HasGamePackets();

		bool HasGameServerConnection();

		bool IsConnectedToGameServer();

		PegasusPacket NextGamePacket();

		int NextGameType();

		void ProcessGamePackets();

		void OnGamePacketReceived(PegasusPacket decodedPacket, int packetTypeId);

		void SendGamePacket(int packetId, IProtoBuf body);

		int DropAllUtilPackets();

		void DropUtilPacket();

		bool HasUtilErrors();

		bool HasUtilPackets();

		ResponseWithRequest NextUtilPacket();

		int NextUtilType();

		void NotifyUtilResponseReceived(PegasusPacket packet);

		void OnLoginComplete();

		void OnStartupPacketSequenceComplete();

		void OnUtilPacketReceived(PegasusPacket decodedPacket, int packetTypeId);

		void ProcessUtilPackets();

		void EnsureSubscribedTo(UtilSystemId systemChannel);

		void SendUtilPacket(int type, UtilSystemId system, IProtoBuf body, RequestPhase requestPhase, int subId);
	}
}
