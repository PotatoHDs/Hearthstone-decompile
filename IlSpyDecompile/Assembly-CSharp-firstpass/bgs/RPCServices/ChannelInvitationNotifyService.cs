using bnet.protocol.channel.v1;

namespace bgs.RPCServices
{
	public class ChannelInvitationNotifyService : ServiceDescriptor
	{
		public const uint NOTIFY_RECEIVED_INVITATION_ADDED_ID = 1u;

		public const uint NOTIFY_RECEIVED_INVITATION_REMOVED_ID = 2u;

		public const uint NOTIFY_RECEIVED_SUGGESTION_ADDED_ID = 3u;

		public ChannelInvitationNotifyService()
			: base("bnet.protocol.channel_invitation.ChannelInvitationNotify")
		{
			base.Exported = true;
			Methods = new MethodDescriptor[4];
			Methods[1] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationNotify.NotifyReceivedInvitationAdded", 1u, ProtobufUtil.ParseFromGeneric<InvitationAddedNotification>);
			Methods[2] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationNotify.NotifyReceivedInvitationRemoved", 2u, ProtobufUtil.ParseFromGeneric<InvitationRemovedNotification>);
			Methods[3] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationNotify.NotifyReceivedSuggestionAdded", 3u, ProtobufUtil.ParseFromGeneric<SuggestionAddedNotification>);
		}
	}
}
