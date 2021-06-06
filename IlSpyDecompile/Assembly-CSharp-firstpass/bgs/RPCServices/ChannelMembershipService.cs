using bnet.protocol;
using bnet.protocol.channel.v2.membership;

namespace bgs.RPCServices
{
	public class ChannelMembershipService : ServiceDescriptor
	{
		public const uint SUBSCRIBE_ID = 1u;

		public const uint UNSUBSCRIBE_ID = 2u;

		public const uint GET_STATE_ID = 3u;

		public ChannelMembershipService()
			: base("bnet.protocol.channel.v2.membership.ChannelMembershipService")
		{
			base.Imported = true;
			Methods = new MethodDescriptor[4];
			Methods[1] = new MethodDescriptor("bnet.protocol.channel.v2.membership.Subscribe", 1u, ProtobufUtil.ParseFromGeneric<SubscribeResponse>);
			Methods[2] = new MethodDescriptor("bnet.protocol.channel.v2.membership.Unsubscribe", 2u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[3] = new MethodDescriptor("bnet.protocol.channel.v2.membership.GetState", 3u, ProtobufUtil.ParseFromGeneric<GetStateResponse>);
		}
	}
}
