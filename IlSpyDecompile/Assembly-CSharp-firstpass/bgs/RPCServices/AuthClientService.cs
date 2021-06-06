using bnet.protocol;
using bnet.protocol.authentication.v1;

namespace bgs.RPCServices
{
	public class AuthClientService : ServiceDescriptor
	{
		public const uint MODULE_LOAD_METHOD_ID = 1u;

		public const uint MODULE_MESSAGE_METHOD_ID = 2u;

		public const uint ACCOUNT_SETTINGS_METHOD_ID = 3u;

		public const uint SERVER_STATE_CHANGE_METHOD_ID = 4u;

		public const uint LOGON_COMPLETE_METHOD_ID = 5u;

		public const uint MEM_MODULE_LOAD_METHOD_ID = 6u;

		public const uint LOGON_UPDATE_METHOD_ID = 10u;

		public const uint VERSION_INFO_UPDATED_ID = 11u;

		public const uint LOGON_QUEUE_UPDATE_ID = 12u;

		public const uint LOGON_QUEUE_END_ID = 13u;

		public const uint GAME_ACCOUNT_SELECTED = 14u;

		public AuthClientService()
			: base("bnet.protocol.authentication.AuthenticationClient")
		{
			base.Exported = true;
			Methods = new MethodDescriptor[15];
			Methods[1] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.ModuleLoad", 1u, ProtobufUtil.ParseFromGeneric<ModuleLoadRequest>);
			Methods[2] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.ModuleMessage", 2u, ProtobufUtil.ParseFromGeneric<ModuleMessageRequest>);
			Methods[3] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.AccountSettings", 3u, ProtobufUtil.ParseFromGeneric<AccountSettingsNotification>);
			Methods[4] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.ServerStateChange", 4u, ProtobufUtil.ParseFromGeneric<ServerStateChangeRequest>);
			Methods[5] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.LogonComplete", 5u, ProtobufUtil.ParseFromGeneric<LogonResult>);
			Methods[6] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.MemModuleLoad", 6u, ProtobufUtil.ParseFromGeneric<MemModuleLoadRequest>);
			Methods[10] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.LogonUpdate", 10u, ProtobufUtil.ParseFromGeneric<LogonUpdateRequest>);
			Methods[11] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.VersionInfoUpdated", 11u, ProtobufUtil.ParseFromGeneric<VersionInfoNotification>);
			Methods[12] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.LogonQueueUpdate", 12u, ProtobufUtil.ParseFromGeneric<LogonQueueUpdateRequest>);
			Methods[13] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.LogonQueueEnd", 13u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[14] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.GameAccountSelected", 14u, ProtobufUtil.ParseFromGeneric<GameAccountSelectedRequest>);
		}
	}
}
