using System;
using bnet.protocol.account.v1;

namespace bgs.RPCServices
{
	// Token: 0x0200029A RID: 666
	public class AccountNotify : ServiceDescriptor
	{
		// Token: 0x06002610 RID: 9744 RVA: 0x000880CC File Offset: 0x000862CC
		public AccountNotify() : base("bnet.protocol.account.AccountNotify")
		{
			base.Exported = true;
			this.Methods = new MethodDescriptor[5];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.account.AccountNotify.NotifyAccountStateUpdated", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<AccountStateNotification>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.account.AccountNotify.NotifyGameAccountStateUpdated", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountStateNotification>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.account.AccountNotify.NotifyGameAccountsUpdated", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountNotification>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.account.AccountNotify.NotifyGameSessionUpdated", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountSessionNotification>));
		}

		// Token: 0x040010D0 RID: 4304
		public const uint NOTIFY_ACCOUNT_STATE_UPDATED_ID = 1U;

		// Token: 0x040010D1 RID: 4305
		public const uint NOTIFY_GAME_ACCOUNT_STATE_UPDATED_ID = 2U;

		// Token: 0x040010D2 RID: 4306
		public const uint NOTIFY_GAME_ACCOUNTS_UPDATED_ID = 3U;

		// Token: 0x040010D3 RID: 4307
		public const uint NOTIFY_GAME_SESSION_UPDATED_ID = 4U;
	}
}
