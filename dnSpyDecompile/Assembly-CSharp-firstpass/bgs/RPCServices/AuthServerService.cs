using System;
using bnet.protocol;
using bnet.protocol.authentication.v1;

namespace bgs.RPCServices
{
	// Token: 0x02000285 RID: 645
	public class AuthServerService : ServiceDescriptor
	{
		// Token: 0x060025FB RID: 9723 RVA: 0x00086B90 File Offset: 0x00084D90
		public AuthServerService() : base("bnet.protocol.authentication.AuthenticationServer")
		{
			base.Imported = true;
			this.Methods = new MethodDescriptor[9];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.Logon", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.ModuleNotify", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.ModuleMessage", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.SelectGameAccount_DEPRECATED", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[5] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.GenerateSSOToken", 5U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GenerateSSOTokenResponse>));
			this.Methods[6] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.SelectGameAccount", 6U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[7] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.VerifyWebCredentials", 7U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[8] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.GenerateWebCredentials", 8U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GenerateWebCredentialsResponse>));
		}

		// Token: 0x04001041 RID: 4161
		public const uint LOGON_METHOD_ID = 1U;

		// Token: 0x04001042 RID: 4162
		public const uint MODULE_NOTIFY_METHOD_ID = 2U;

		// Token: 0x04001043 RID: 4163
		public const uint MODULE_MESSAGE_METHOD_ID = 3U;

		// Token: 0x04001044 RID: 4164
		public const uint SELECT_GAME_ACCT_DEPRECATED_METHOD_ID = 4U;

		// Token: 0x04001045 RID: 4165
		public const uint GEN_SSO_TOKEN_METHOD_ID = 5U;

		// Token: 0x04001046 RID: 4166
		public const uint SELECT_GAME_ACCT_METHOD_ID = 6U;

		// Token: 0x04001047 RID: 4167
		public const uint VERIFY_WEB_CREDENTIALS_METHOD_ID = 7U;

		// Token: 0x04001048 RID: 4168
		public const uint GENERATE_WEB_CREDENTIALS_METHOD_ID = 8U;
	}
}
