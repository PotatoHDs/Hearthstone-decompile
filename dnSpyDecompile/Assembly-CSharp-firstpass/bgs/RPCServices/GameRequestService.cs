using System;
using bnet.protocol;
using bnet.protocol.matchmaking.v1;

namespace bgs.RPCServices
{
	// Token: 0x02000288 RID: 648
	public class GameRequestService : ServiceDescriptor
	{
		// Token: 0x060025FE RID: 9726 RVA: 0x00086F28 File Offset: 0x00085128
		public GameRequestService() : base("bnet.protocol.matchmaking.GameRequest")
		{
			base.Imported = true;
			this.Methods = new MethodDescriptor[4];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestService.QueueMatchmaking", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<QueueMatchmakingResponse>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestService.JoinGame", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<JoinGameResponse>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestService.CancelMatchmaking", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
		}

		// Token: 0x0400105A RID: 4186
		public const uint QUEUE_MATCHMAKING = 1U;

		// Token: 0x0400105B RID: 4187
		public const uint JOIN_GAME = 2U;

		// Token: 0x0400105C RID: 4188
		public const uint CANCEL_MATCHMAKING = 3U;
	}
}
