using bnet.protocol;
using bnet.protocol.authentication.v1;

namespace bgs.RPCServices
{
	public class AuthServerService : ServiceDescriptor
	{
		public const uint LOGON_METHOD_ID = 1u;

		public const uint MODULE_NOTIFY_METHOD_ID = 2u;

		public const uint MODULE_MESSAGE_METHOD_ID = 3u;

		public const uint SELECT_GAME_ACCT_DEPRECATED_METHOD_ID = 4u;

		public const uint GEN_SSO_TOKEN_METHOD_ID = 5u;

		public const uint SELECT_GAME_ACCT_METHOD_ID = 6u;

		public const uint VERIFY_WEB_CREDENTIALS_METHOD_ID = 7u;

		public const uint GENERATE_WEB_CREDENTIALS_METHOD_ID = 8u;

		public AuthServerService()
			: base("bnet.protocol.authentication.AuthenticationServer")
		{
			base.Imported = true;
			Methods = new MethodDescriptor[9];
			Methods[1] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.Logon", 1u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[2] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.ModuleNotify", 2u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[3] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.ModuleMessage", 3u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[4] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.SelectGameAccount_DEPRECATED", 4u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[5] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.GenerateSSOToken", 5u, ProtobufUtil.ParseFromGeneric<GenerateSSOTokenResponse>);
			Methods[6] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.SelectGameAccount", 6u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[7] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.VerifyWebCredentials", 7u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[8] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.GenerateWebCredentials", 8u, ProtobufUtil.ParseFromGeneric<GenerateWebCredentialsResponse>);
		}
	}
}
