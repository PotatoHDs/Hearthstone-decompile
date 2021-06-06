using System;
using bgs.RPCServices;
using bnet.protocol;
using bnet.protocol.connection.v1;

namespace bgs
{
	// Token: 0x02000226 RID: 550
	public interface IRpcConnection
	{
		// Token: 0x0600230D RID: 8973
		long GetMillisecondsSinceLastPacketSent();

		// Token: 0x0600230E RID: 8974
		ServiceCollectionHelper GetServiceHelper();

		// Token: 0x0600230F RID: 8975
		void SetOnConnectHandler(OnConnectHandler handler);

		// Token: 0x06002310 RID: 8976
		void SetOnDisconnectHandler(OnDisconnectHandler handler);

		// Token: 0x06002311 RID: 8977
		void Connect(string host, uint port, SslParameters sslParams, int tryCount);

		// Token: 0x06002312 RID: 8978
		void Disconnect();

		// Token: 0x06002313 RID: 8979
		void BeginAuth();

		// Token: 0x06002314 RID: 8980
		bool GetInStartupPeriod();

		// Token: 0x06002315 RID: 8981
		RPCContext SendRequest(ServiceDescriptor service, uint methodId, IProtoBuf message, RPCContextDelegate callback = null, uint objectId = 0U);

		// Token: 0x06002316 RID: 8982
		RPCContext QueueRequest(ServiceDescriptor service, uint methodId, IProtoBuf message, RPCContextDelegate callback = null, uint objectId = 0U);

		// Token: 0x06002317 RID: 8983
		void SendResponse(RPCContext context, IProtoBuf message);

		// Token: 0x06002318 RID: 8984
		void QueueResponse(RPCContext context, IProtoBuf message);

		// Token: 0x06002319 RID: 8985
		void RegisterServiceMethodListener(uint serviceId, uint methodId, RPCContextDelegate callback);

		// Token: 0x0600231A RID: 8986
		void Update();

		// Token: 0x0600231B RID: 8987
		void PacketReceived(BattleNetPacket p, object state);

		// Token: 0x0600231C RID: 8988
		void SetConnectionMeteringContentHandles(ConnectionMeteringContentHandles handles, LocalStorageAPI localStorage);

		// Token: 0x0600231D RID: 8989
		string PacketToString(BattleNetPacket packet, bool outgoing);

		// Token: 0x0600231E RID: 8990
		string PacketHeaderToString(Header header, bool outgoing);
	}
}
