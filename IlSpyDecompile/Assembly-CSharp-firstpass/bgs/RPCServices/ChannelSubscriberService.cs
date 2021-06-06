using bnet.protocol.channel.v1;

namespace bgs.RPCServices
{
	public class ChannelSubscriberService : ServiceDescriptor
	{
		public const uint NOTIFY_ADD_ID = 1u;

		public const uint NOTIFY_JOIN_ID = 2u;

		public const uint NOTIFY_REMOVE_ID = 3u;

		public const uint NOTIFY_LEAVE_ID = 4u;

		public const uint NOTIFY_SEND_MESSAGE_ID = 5u;

		public const uint NOTIFY_UPDATE_CHANNEL_STATE_ID = 6u;

		public const uint NOTIFY_UPDATE_MEMBER_STATE_ID = 7u;

		public ChannelSubscriberService()
			: base("bnet.protocol.channel.ChannelSubscriber")
		{
			base.Exported = true;
			Methods = new MethodDescriptor[8];
			Methods[1] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyAdd", 1u, ProtobufUtil.ParseFromGeneric<JoinNotification>);
			Methods[2] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyJoin", 2u, ProtobufUtil.ParseFromGeneric<MemberAddedNotification>);
			Methods[3] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyRemove", 3u, ProtobufUtil.ParseFromGeneric<LeaveNotification>);
			Methods[4] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyLeave", 4u, ProtobufUtil.ParseFromGeneric<MemberRemovedNotification>);
			Methods[5] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifySendMessage", 5u, ProtobufUtil.ParseFromGeneric<SendMessageNotification>);
			Methods[6] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyUpdateChannelState", 6u, ProtobufUtil.ParseFromGeneric<UpdateChannelStateNotification>);
			Methods[7] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyUpdateMemberState", 7u, ProtobufUtil.ParseFromGeneric<UpdateMemberStateNotification>);
		}
	}
}
