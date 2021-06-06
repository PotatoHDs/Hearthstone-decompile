using System;
using bnet.protocol;
using bnet.protocol.account.v1;

namespace bgs.RPCServices
{
	// Token: 0x02000299 RID: 665
	public class AccountService : ServiceDescriptor
	{
		// Token: 0x0600260F RID: 9743 RVA: 0x00087F34 File Offset: 0x00086134
		public AccountService() : base("bnet.protocol.account.AccountService")
		{
			base.Imported = true;
			this.Methods = new MethodDescriptor[37];
			this.Methods[13] = new MethodDescriptor("bnet.protocol.account.AccountService.GetAccount", 13U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ResolveAccountResponse>));
			this.Methods[15] = new MethodDescriptor("bnet.protocol.account.AccountService.IsIgrAddress", 15U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[25] = new MethodDescriptor("bnet.protocol.account.AccountService.Subscribe", 25U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SubscriptionUpdateResponse>));
			this.Methods[26] = new MethodDescriptor("bnet.protocol.account.AccountService.Unsubscribe", 26U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[30] = new MethodDescriptor("bnet.protocol.account.AccountService.GetAccountState", 30U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetAccountStateResponse>));
			this.Methods[31] = new MethodDescriptor("bnet.protocol.account.AccountService.GetGameAccountState", 31U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetGameAccountStateResponse>));
			this.Methods[32] = new MethodDescriptor("bnet.protocol.account.AccountService.GetLicenses", 32U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetLicensesResponse>));
			this.Methods[33] = new MethodDescriptor("bnet.protocol.account.AccountService.GetGameTimeRemainingInfo", 33U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetGameTimeRemainingInfoResponse>));
			this.Methods[34] = new MethodDescriptor("bnet.protocol.account.AccountService.GetGameSessionInfo", 34U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetGameSessionInfoResponse>));
			this.Methods[35] = new MethodDescriptor("bnet.protocol.account.AccountService.GetCAISInfo", 35U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetCAISInfoResponse>));
			this.Methods[36] = new MethodDescriptor("bnet.protocol.account.AccountService.ForwardCacheExpire", 36U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
		}

		// Token: 0x040010C5 RID: 4293
		public const uint GET_ACCOUNT_ID = 13U;

		// Token: 0x040010C6 RID: 4294
		public const uint IS_IGR_ADDRESS_ID = 15U;

		// Token: 0x040010C7 RID: 4295
		public const uint SUBSCRIBE_ID = 25U;

		// Token: 0x040010C8 RID: 4296
		public const uint UNSUBSCRIBE_ID = 26U;

		// Token: 0x040010C9 RID: 4297
		public const uint GET_ACCOUNT_STATE_ID = 30U;

		// Token: 0x040010CA RID: 4298
		public const uint GET_GAME_ACCOUNT_STATE_ID = 31U;

		// Token: 0x040010CB RID: 4299
		public const uint GET_LICENSES_ID = 32U;

		// Token: 0x040010CC RID: 4300
		public const uint GET_GAME_TIME_REMAINING_INFO_ID = 33U;

		// Token: 0x040010CD RID: 4301
		public const uint GET_GAME_SESSION_INFO_ID = 34U;

		// Token: 0x040010CE RID: 4302
		public const uint GET_CAIS_INFO_ID = 35U;

		// Token: 0x040010CF RID: 4303
		public const uint FORWARD_CACHE_EXPIRE_ID = 36U;
	}
}
