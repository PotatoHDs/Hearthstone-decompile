using bnet.protocol;
using bnet.protocol.matchmaking.v1;

namespace bgs.RPCServices
{
	public class GameRequestService : ServiceDescriptor
	{
		public const uint QUEUE_MATCHMAKING = 1u;

		public const uint JOIN_GAME = 2u;

		public const uint CANCEL_MATCHMAKING = 3u;

		public GameRequestService()
			: base("bnet.protocol.matchmaking.GameRequest")
		{
			base.Imported = true;
			Methods = new MethodDescriptor[4];
			Methods[1] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestService.QueueMatchmaking", 1u, ProtobufUtil.ParseFromGeneric<QueueMatchmakingResponse>);
			Methods[2] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestService.JoinGame", 2u, ProtobufUtil.ParseFromGeneric<JoinGameResponse>);
			Methods[3] = new MethodDescriptor("bnet.protocol.matchmaking.v1.GameRequestService.CancelMatchmaking", 3u, ProtobufUtil.ParseFromGeneric<NoData>);
		}
	}
}
