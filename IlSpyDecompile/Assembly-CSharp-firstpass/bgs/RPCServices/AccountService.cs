using bnet.protocol;
using bnet.protocol.account.v1;

namespace bgs.RPCServices
{
	public class AccountService : ServiceDescriptor
	{
		public const uint GET_ACCOUNT_ID = 13u;

		public const uint IS_IGR_ADDRESS_ID = 15u;

		public const uint SUBSCRIBE_ID = 25u;

		public const uint UNSUBSCRIBE_ID = 26u;

		public const uint GET_ACCOUNT_STATE_ID = 30u;

		public const uint GET_GAME_ACCOUNT_STATE_ID = 31u;

		public const uint GET_LICENSES_ID = 32u;

		public const uint GET_GAME_TIME_REMAINING_INFO_ID = 33u;

		public const uint GET_GAME_SESSION_INFO_ID = 34u;

		public const uint GET_CAIS_INFO_ID = 35u;

		public const uint FORWARD_CACHE_EXPIRE_ID = 36u;

		public AccountService()
			: base("bnet.protocol.account.AccountService")
		{
			base.Imported = true;
			Methods = new MethodDescriptor[37];
			Methods[13] = new MethodDescriptor("bnet.protocol.account.AccountService.GetAccount", 13u, ProtobufUtil.ParseFromGeneric<ResolveAccountResponse>);
			Methods[15] = new MethodDescriptor("bnet.protocol.account.AccountService.IsIgrAddress", 15u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[25] = new MethodDescriptor("bnet.protocol.account.AccountService.Subscribe", 25u, ProtobufUtil.ParseFromGeneric<SubscriptionUpdateResponse>);
			Methods[26] = new MethodDescriptor("bnet.protocol.account.AccountService.Unsubscribe", 26u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[30] = new MethodDescriptor("bnet.protocol.account.AccountService.GetAccountState", 30u, ProtobufUtil.ParseFromGeneric<GetAccountStateResponse>);
			Methods[31] = new MethodDescriptor("bnet.protocol.account.AccountService.GetGameAccountState", 31u, ProtobufUtil.ParseFromGeneric<GetGameAccountStateResponse>);
			Methods[32] = new MethodDescriptor("bnet.protocol.account.AccountService.GetLicenses", 32u, ProtobufUtil.ParseFromGeneric<GetLicensesResponse>);
			Methods[33] = new MethodDescriptor("bnet.protocol.account.AccountService.GetGameTimeRemainingInfo", 33u, ProtobufUtil.ParseFromGeneric<GetGameTimeRemainingInfoResponse>);
			Methods[34] = new MethodDescriptor("bnet.protocol.account.AccountService.GetGameSessionInfo", 34u, ProtobufUtil.ParseFromGeneric<GetGameSessionInfoResponse>);
			Methods[35] = new MethodDescriptor("bnet.protocol.account.AccountService.GetCAISInfo", 35u, ProtobufUtil.ParseFromGeneric<GetCAISInfoResponse>);
			Methods[36] = new MethodDescriptor("bnet.protocol.account.AccountService.ForwardCacheExpire", 36u, ProtobufUtil.ParseFromGeneric<NoData>);
		}
	}
}
