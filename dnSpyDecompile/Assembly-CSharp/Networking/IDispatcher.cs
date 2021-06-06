using System;
using bgs;

namespace Networking
{
	// Token: 0x02000FB0 RID: 4016
	public interface IDispatcher
	{
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x0600AF8C RID: 44940
		IDebugConnectionManager DebugConnectionManager { get; }

		// Token: 0x0600AF8D RID: 44941
		void Close();

		// Token: 0x0600AF8E RID: 44942
		PegasusPacket DecodePacket(PegasusPacket packet);

		// Token: 0x0600AF8F RID: 44943
		void SetDisconnectedFromBattleNet();

		// Token: 0x0600AF90 RID: 44944
		bool ShouldIgnoreError(BnetErrorInfo errorInfo);

		// Token: 0x0600AF91 RID: 44945
		void ResetForNewConnection();

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x0600AF92 RID: 44946
		// (set) Token: 0x0600AF93 RID: 44947
		GameStartState GameStartState { get; set; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x0600AF94 RID: 44948
		// (set) Token: 0x0600AF95 RID: 44949
		int PingsSinceLastPong { get; set; }

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x0600AF96 RID: 44950
		// (set) Token: 0x0600AF97 RID: 44951
		double TimeLastPingReceived { get; set; }

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x0600AF98 RID: 44952
		// (set) Token: 0x0600AF99 RID: 44953
		double TimeLastPingSent { get; set; }

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x0600AF9A RID: 44954
		// (set) Token: 0x0600AF9B RID: 44955
		bool ShouldIgnorePong { get; set; }

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x0600AF9C RID: 44956
		// (set) Token: 0x0600AF9D RID: 44957
		bool SpoofDisconnected { get; set; }

		// Token: 0x0600AF9E RID: 44958
		bool ConnectToGameServer(string address, uint port);

		// Token: 0x0600AF9F RID: 44959
		void DisconnectFromGameServer();

		// Token: 0x1400009D RID: 157
		// (add) Token: 0x0600AFA0 RID: 44960
		// (remove) Token: 0x0600AFA1 RID: 44961
		event Action<BattleNetErrors> OnGameServerConnectEvent;

		// Token: 0x1400009E RID: 158
		// (add) Token: 0x0600AFA2 RID: 44962
		// (remove) Token: 0x0600AFA3 RID: 44963
		event Action<BattleNetErrors> OnGameServerDisconnectEvent;

		// Token: 0x0600AFA4 RID: 44964
		int DropAllGamePackets();

		// Token: 0x0600AFA5 RID: 44965
		void DropGamePacket();

		// Token: 0x0600AFA6 RID: 44966
		bool GameServerHasEvents();

		// Token: 0x0600AFA7 RID: 44967
		bool HasGamePackets();

		// Token: 0x0600AFA8 RID: 44968
		bool HasGameServerConnection();

		// Token: 0x0600AFA9 RID: 44969
		bool IsConnectedToGameServer();

		// Token: 0x0600AFAA RID: 44970
		PegasusPacket NextGamePacket();

		// Token: 0x0600AFAB RID: 44971
		int NextGameType();

		// Token: 0x0600AFAC RID: 44972
		void ProcessGamePackets();

		// Token: 0x0600AFAD RID: 44973
		void OnGamePacketReceived(PegasusPacket decodedPacket, int packetTypeId);

		// Token: 0x0600AFAE RID: 44974
		void SendGamePacket(int packetId, IProtoBuf body);

		// Token: 0x0600AFAF RID: 44975
		int DropAllUtilPackets();

		// Token: 0x0600AFB0 RID: 44976
		void DropUtilPacket();

		// Token: 0x0600AFB1 RID: 44977
		bool HasUtilErrors();

		// Token: 0x0600AFB2 RID: 44978
		bool HasUtilPackets();

		// Token: 0x0600AFB3 RID: 44979
		ResponseWithRequest NextUtilPacket();

		// Token: 0x0600AFB4 RID: 44980
		int NextUtilType();

		// Token: 0x0600AFB5 RID: 44981
		void NotifyUtilResponseReceived(PegasusPacket packet);

		// Token: 0x0600AFB6 RID: 44982
		void OnLoginComplete();

		// Token: 0x0600AFB7 RID: 44983
		void OnStartupPacketSequenceComplete();

		// Token: 0x0600AFB8 RID: 44984
		void OnUtilPacketReceived(PegasusPacket decodedPacket, int packetTypeId);

		// Token: 0x0600AFB9 RID: 44985
		void ProcessUtilPackets();

		// Token: 0x0600AFBA RID: 44986
		void EnsureSubscribedTo(UtilSystemId systemChannel);

		// Token: 0x0600AFBB RID: 44987
		void SendUtilPacket(int type, UtilSystemId system, IProtoBuf body, RequestPhase requestPhase, int subId);
	}
}
