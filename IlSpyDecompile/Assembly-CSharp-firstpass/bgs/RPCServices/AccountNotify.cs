using bnet.protocol.account.v1;

namespace bgs.RPCServices
{
	public class AccountNotify : ServiceDescriptor
	{
		public const uint NOTIFY_ACCOUNT_STATE_UPDATED_ID = 1u;

		public const uint NOTIFY_GAME_ACCOUNT_STATE_UPDATED_ID = 2u;

		public const uint NOTIFY_GAME_ACCOUNTS_UPDATED_ID = 3u;

		public const uint NOTIFY_GAME_SESSION_UPDATED_ID = 4u;

		public AccountNotify()
			: base("bnet.protocol.account.AccountNotify")
		{
			base.Exported = true;
			Methods = new MethodDescriptor[5];
			Methods[1] = new MethodDescriptor("bnet.protocol.account.AccountNotify.NotifyAccountStateUpdated", 1u, ProtobufUtil.ParseFromGeneric<AccountStateNotification>);
			Methods[2] = new MethodDescriptor("bnet.protocol.account.AccountNotify.NotifyGameAccountStateUpdated", 2u, ProtobufUtil.ParseFromGeneric<GameAccountStateNotification>);
			Methods[3] = new MethodDescriptor("bnet.protocol.account.AccountNotify.NotifyGameAccountsUpdated", 3u, ProtobufUtil.ParseFromGeneric<GameAccountNotification>);
			Methods[4] = new MethodDescriptor("bnet.protocol.account.AccountNotify.NotifyGameSessionUpdated", 4u, ProtobufUtil.ParseFromGeneric<GameAccountSessionNotification>);
		}
	}
}
