using bnet.protocol;
using bnet.protocol.connection.v1;

namespace bgs.RPCServices
{
	public class ConnectionService : ServiceDescriptor
	{
		public const uint CONNECT_METHOD_ID = 1u;

		public const uint BIND_METHOD_ID = 2u;

		public const uint ECHO_METHOD_ID = 3u;

		public const uint FORCE_DISCONNECT_METHOD_ID = 4u;

		public const uint KEEP_ALIVE_METHOD_ID = 5u;

		public const uint ENCRYPT_METHOD_ID = 6u;

		public const uint REQUEST_DISCONNECT_METHOD_ID = 7u;

		public ConnectionService()
			: base("bnet.protocol.connection.ConnectionService")
		{
			base.Imported = true;
			base.Exported = true;
			Methods = new MethodDescriptor[8];
			Methods[1] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.Connect", 1u, ProtobufUtil.ParseFromGeneric<ConnectResponse>);
			Methods[2] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.Bind", 2u, ProtobufUtil.ParseFromGeneric<BindResponse>);
			Methods[3] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.Echo", 3u, ProtobufUtil.ParseFromGeneric<EchoResponse>);
			Methods[4] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.ForceDisconnect", 4u, ProtobufUtil.ParseFromGeneric<NORESPONSE>);
			Methods[5] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.KeepAlive", 5u, ProtobufUtil.ParseFromGeneric<NORESPONSE>);
			Methods[6] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.Encrypt", 6u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[7] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.RequestDisconnect", 7u, ProtobufUtil.ParseFromGeneric<NORESPONSE>);
		}
	}
}
