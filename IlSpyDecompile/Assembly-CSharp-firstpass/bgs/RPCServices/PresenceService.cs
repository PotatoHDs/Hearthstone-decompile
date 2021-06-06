using bnet.protocol;

namespace bgs.RPCServices
{
	public class PresenceService : ServiceDescriptor
	{
		public const uint SUBSCRIBE_ID = 1u;

		public const uint UNSUBSCRIBE_ID = 2u;

		public const uint UPDATE_ID = 3u;

		public const uint QUERY_ID = 4u;

		public PresenceService()
			: base("bnet.protocol.presence.PresenceService")
		{
			base.Imported = true;
			Methods = new MethodDescriptor[5];
			Methods[1] = new MethodDescriptor("bnet.protocol.presence.PresenceService.Subscribe", 1u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[2] = new MethodDescriptor("bnet.protocol.presence.PresenceService.Unsubscribe", 2u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[3] = new MethodDescriptor("bnet.protocol.presence.PresenceService.Update", 3u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[4] = new MethodDescriptor("bnet.protocol.presence.PresenceService.Query", 4u, ProtobufUtil.ParseFromGeneric<NoData>);
		}
	}
}
