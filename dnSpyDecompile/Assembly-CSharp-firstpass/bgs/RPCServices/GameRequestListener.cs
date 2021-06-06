using System;
using bnet.protocol.matchmaking.v1;

namespace bgs.RPCServices
{
	// Token: 0x02000289 RID: 649
	public class GameRequestListener : ServiceDescriptor
	{
		// Token: 0x060025FF RID: 9727 RVA: 0x00086FB0 File Offset: 0x000851B0
		public GameRequestListener() : base("bnet.protocol.matchmaking.GameRequestListener")
		{
			base.Exported = true;
			this.Methods = new MethodDescriptor[6];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestListener.OnQueueEntry", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<QueueEntryNotification>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestListener.OnQueueExit", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<QueueExitNotification>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestListener.OnQueueLeft", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<QueueLeftNotification>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestListener.OnQueueUpdate", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<QueueUpdateNotification>));
			this.Methods[5] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestListener.OnMatchmakingResult", 5U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<MatchmakingResultNotification>));
		}

		// Token: 0x0400105D RID: 4189
		public const uint QUEUE_ENTRY_NOTIFICATION = 1U;

		// Token: 0x0400105E RID: 4190
		public const uint QUEUE_EXIT_NOTIFICATION = 2U;

		// Token: 0x0400105F RID: 4191
		public const uint QUEUE_LEFT_NOTIFICATION = 3U;

		// Token: 0x04001060 RID: 4192
		public const uint QUEUE_UPDATE_NOTIFICATION = 4U;

		// Token: 0x04001061 RID: 4193
		public const uint MATCHMAKING_RESULT_NOTIFICATION = 5U;
	}
}
