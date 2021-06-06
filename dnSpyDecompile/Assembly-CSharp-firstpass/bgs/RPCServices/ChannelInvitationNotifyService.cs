using System;
using bnet.protocol.channel.v1;

namespace bgs.RPCServices
{
	// Token: 0x02000292 RID: 658
	public class ChannelInvitationNotifyService : ServiceDescriptor
	{
		// Token: 0x06002608 RID: 9736 RVA: 0x00087A10 File Offset: 0x00085C10
		public ChannelInvitationNotifyService() : base("bnet.protocol.channel_invitation.ChannelInvitationNotify")
		{
			base.Exported = true;
			this.Methods = new MethodDescriptor[4];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationNotify.NotifyReceivedInvitationAdded", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationAddedNotification>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationNotify.NotifyReceivedInvitationRemoved", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationRemovedNotification>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationNotify.NotifyReceivedSuggestionAdded", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SuggestionAddedNotification>));
		}

		// Token: 0x040010A4 RID: 4260
		public const uint NOTIFY_RECEIVED_INVITATION_ADDED_ID = 1U;

		// Token: 0x040010A5 RID: 4261
		public const uint NOTIFY_RECEIVED_INVITATION_REMOVED_ID = 2U;

		// Token: 0x040010A6 RID: 4262
		public const uint NOTIFY_RECEIVED_SUGGESTION_ADDED_ID = 3U;
	}
}
