using bnet.protocol.channel.v2.membership;

namespace bgs.RPCServices
{
	public class ChannelMembershipListener : ServiceDescriptor
	{
		public const uint ON_CHANNEL_ADDED_ID = 1u;

		public const uint ON_CHANNEL_REMOVED_ID = 2u;

		public const uint ON_RECEIVED_INVITATION_ADDED_ID = 3u;

		public const uint ON_RECEIVED_INVITATION_REMOVED_ID = 4u;

		public ChannelMembershipListener()
			: base("bnet.protocol.channel.v2.membership.ChannelMembershipListener")
		{
			base.Exported = true;
			Methods = new MethodDescriptor[5];
			Methods[1] = new MethodDescriptor("bnet.protocol.channel.v2.membership.OnChannelAdded", 1u, ProtobufUtil.ParseFromGeneric<ChannelAddedNotification>);
			Methods[2] = new MethodDescriptor("bnet.protocol.channel.v2.membership.OnChannelRemoved", 2u, ProtobufUtil.ParseFromGeneric<ChannelRemovedNotification>);
			Methods[3] = new MethodDescriptor("bnet.protocol.channel.v2.membership.OnReceivedInvitationAdded", 3u, ProtobufUtil.ParseFromGeneric<ReceivedInvitationAddedNotification>);
			Methods[4] = new MethodDescriptor("bnet.protocol.channel.v2.membership.OnReceivedInvitationRemoved", 4u, ProtobufUtil.ParseFromGeneric<ReceivedInvitationRemovedNotification>);
		}
	}
}
