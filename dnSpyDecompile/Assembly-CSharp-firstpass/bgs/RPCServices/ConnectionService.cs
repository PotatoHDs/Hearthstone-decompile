using System;
using bnet.protocol;
using bnet.protocol.connection.v1;

namespace bgs.RPCServices
{
	// Token: 0x02000284 RID: 644
	public class ConnectionService : ServiceDescriptor
	{
		// Token: 0x060025FA RID: 9722 RVA: 0x00086A84 File Offset: 0x00084C84
		public ConnectionService() : base("bnet.protocol.connection.ConnectionService")
		{
			base.Imported = true;
			base.Exported = true;
			this.Methods = new MethodDescriptor[8];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.Connect", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ConnectResponse>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.Bind", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<BindResponse>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.Echo", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<EchoResponse>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.ForceDisconnect", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
			this.Methods[5] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.KeepAlive", 5U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
			this.Methods[6] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.Encrypt", 6U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[7] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.RequestDisconnect", 7U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
		}

		// Token: 0x0400103A RID: 4154
		public const uint CONNECT_METHOD_ID = 1U;

		// Token: 0x0400103B RID: 4155
		public const uint BIND_METHOD_ID = 2U;

		// Token: 0x0400103C RID: 4156
		public const uint ECHO_METHOD_ID = 3U;

		// Token: 0x0400103D RID: 4157
		public const uint FORCE_DISCONNECT_METHOD_ID = 4U;

		// Token: 0x0400103E RID: 4158
		public const uint KEEP_ALIVE_METHOD_ID = 5U;

		// Token: 0x0400103F RID: 4159
		public const uint ENCRYPT_METHOD_ID = 6U;

		// Token: 0x04001040 RID: 4160
		public const uint REQUEST_DISCONNECT_METHOD_ID = 7U;
	}
}
