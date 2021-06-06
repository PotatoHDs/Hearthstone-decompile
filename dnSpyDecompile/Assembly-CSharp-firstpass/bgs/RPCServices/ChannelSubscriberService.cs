using System;
using bnet.protocol.channel.v1;

namespace bgs.RPCServices
{
	// Token: 0x02000293 RID: 659
	public class ChannelSubscriberService : ServiceDescriptor
	{
		// Token: 0x06002609 RID: 9737 RVA: 0x00087A98 File Offset: 0x00085C98
		public ChannelSubscriberService() : base("bnet.protocol.channel.ChannelSubscriber")
		{
			base.Exported = true;
			this.Methods = new MethodDescriptor[8];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyAdd", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<JoinNotification>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyJoin", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<MemberAddedNotification>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyRemove", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<LeaveNotification>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyLeave", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<MemberRemovedNotification>));
			this.Methods[5] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifySendMessage", 5U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SendMessageNotification>));
			this.Methods[6] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyUpdateChannelState", 6U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<UpdateChannelStateNotification>));
			this.Methods[7] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyUpdateMemberState", 7U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<UpdateMemberStateNotification>));
		}

		// Token: 0x040010A7 RID: 4263
		public const uint NOTIFY_ADD_ID = 1U;

		// Token: 0x040010A8 RID: 4264
		public const uint NOTIFY_JOIN_ID = 2U;

		// Token: 0x040010A9 RID: 4265
		public const uint NOTIFY_REMOVE_ID = 3U;

		// Token: 0x040010AA RID: 4266
		public const uint NOTIFY_LEAVE_ID = 4U;

		// Token: 0x040010AB RID: 4267
		public const uint NOTIFY_SEND_MESSAGE_ID = 5U;

		// Token: 0x040010AC RID: 4268
		public const uint NOTIFY_UPDATE_CHANNEL_STATE_ID = 6U;

		// Token: 0x040010AD RID: 4269
		public const uint NOTIFY_UPDATE_MEMBER_STATE_ID = 7U;
	}
}
