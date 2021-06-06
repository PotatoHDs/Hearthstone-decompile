using System;
using bnet.protocol;
using bnet.protocol.authentication.v1;

namespace bgs.RPCServices
{
	// Token: 0x02000286 RID: 646
	public class AuthClientService : ServiceDescriptor
	{
		// Token: 0x060025FC RID: 9724 RVA: 0x00086CB4 File Offset: 0x00084EB4
		public AuthClientService() : base("bnet.protocol.authentication.AuthenticationClient")
		{
			base.Exported = true;
			this.Methods = new MethodDescriptor[15];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.ModuleLoad", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ModuleLoadRequest>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.ModuleMessage", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ModuleMessageRequest>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.AccountSettings", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<AccountSettingsNotification>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.ServerStateChange", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ServerStateChangeRequest>));
			this.Methods[5] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.LogonComplete", 5U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<LogonResult>));
			this.Methods[6] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.MemModuleLoad", 6U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<MemModuleLoadRequest>));
			this.Methods[10] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.LogonUpdate", 10U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<LogonUpdateRequest>));
			this.Methods[11] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.VersionInfoUpdated", 11U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<VersionInfoNotification>));
			this.Methods[12] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.LogonQueueUpdate", 12U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<LogonQueueUpdateRequest>));
			this.Methods[13] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.LogonQueueEnd", 13U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[14] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.GameAccountSelected", 14U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountSelectedRequest>));
		}

		// Token: 0x04001049 RID: 4169
		public const uint MODULE_LOAD_METHOD_ID = 1U;

		// Token: 0x0400104A RID: 4170
		public const uint MODULE_MESSAGE_METHOD_ID = 2U;

		// Token: 0x0400104B RID: 4171
		public const uint ACCOUNT_SETTINGS_METHOD_ID = 3U;

		// Token: 0x0400104C RID: 4172
		public const uint SERVER_STATE_CHANGE_METHOD_ID = 4U;

		// Token: 0x0400104D RID: 4173
		public const uint LOGON_COMPLETE_METHOD_ID = 5U;

		// Token: 0x0400104E RID: 4174
		public const uint MEM_MODULE_LOAD_METHOD_ID = 6U;

		// Token: 0x0400104F RID: 4175
		public const uint LOGON_UPDATE_METHOD_ID = 10U;

		// Token: 0x04001050 RID: 4176
		public const uint VERSION_INFO_UPDATED_ID = 11U;

		// Token: 0x04001051 RID: 4177
		public const uint LOGON_QUEUE_UPDATE_ID = 12U;

		// Token: 0x04001052 RID: 4178
		public const uint LOGON_QUEUE_END_ID = 13U;

		// Token: 0x04001053 RID: 4179
		public const uint GAME_ACCOUNT_SELECTED = 14U;
	}
}
