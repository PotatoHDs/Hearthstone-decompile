using bnet.protocol.matchmaking.v1;

namespace bgs.RPCServices
{
	public class GameRequestListener : ServiceDescriptor
	{
		public const uint QUEUE_ENTRY_NOTIFICATION = 1u;

		public const uint QUEUE_EXIT_NOTIFICATION = 2u;

		public const uint QUEUE_LEFT_NOTIFICATION = 3u;

		public const uint QUEUE_UPDATE_NOTIFICATION = 4u;

		public const uint MATCHMAKING_RESULT_NOTIFICATION = 5u;

		public GameRequestListener()
			: base("bnet.protocol.matchmaking.GameRequestListener")
		{
			base.Exported = true;
			Methods = new MethodDescriptor[6];
			Methods[1] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestListener.OnQueueEntry", 1u, ProtobufUtil.ParseFromGeneric<QueueEntryNotification>);
			Methods[2] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestListener.OnQueueExit", 2u, ProtobufUtil.ParseFromGeneric<QueueExitNotification>);
			Methods[3] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestListener.OnQueueLeft", 3u, ProtobufUtil.ParseFromGeneric<QueueLeftNotification>);
			Methods[4] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestListener.OnQueueUpdate", 4u, ProtobufUtil.ParseFromGeneric<QueueUpdateNotification>);
			Methods[5] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestListener.OnMatchmakingResult", 5u, ProtobufUtil.ParseFromGeneric<MatchmakingResultNotification>);
		}
	}
}
