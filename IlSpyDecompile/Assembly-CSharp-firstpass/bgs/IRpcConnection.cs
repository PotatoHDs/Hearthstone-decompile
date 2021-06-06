using bgs.RPCServices;
using bnet.protocol;
using bnet.protocol.connection.v1;

namespace bgs
{
	public interface IRpcConnection
	{
		long GetMillisecondsSinceLastPacketSent();

		ServiceCollectionHelper GetServiceHelper();

		void SetOnConnectHandler(OnConnectHandler handler);

		void SetOnDisconnectHandler(OnDisconnectHandler handler);

		void Connect(string host, uint port, SslParameters sslParams, int tryCount);

		void Disconnect();

		void BeginAuth();

		bool GetInStartupPeriod();

		RPCContext SendRequest(ServiceDescriptor service, uint methodId, IProtoBuf message, RPCContextDelegate callback = null, uint objectId = 0u);

		RPCContext QueueRequest(ServiceDescriptor service, uint methodId, IProtoBuf message, RPCContextDelegate callback = null, uint objectId = 0u);

		void SendResponse(RPCContext context, IProtoBuf message);

		void QueueResponse(RPCContext context, IProtoBuf message);

		void RegisterServiceMethodListener(uint serviceId, uint methodId, RPCContextDelegate callback);

		void Update();

		void PacketReceived(BattleNetPacket p, object state);

		void SetConnectionMeteringContentHandles(ConnectionMeteringContentHandles handles, LocalStorageAPI localStorage);

		string PacketToString(BattleNetPacket packet, bool outgoing);

		string PacketHeaderToString(Header header, bool outgoing);
	}
}
