using System;
using bnet.protocol.channel.v2.membership;

namespace bgs.RPCServices
{
	// Token: 0x0200028C RID: 652
	public class ChannelMembershipListener : ServiceDescriptor
	{
		// Token: 0x06002602 RID: 9730 RVA: 0x000871A8 File Offset: 0x000853A8
		public ChannelMembershipListener() : base("bnet.protocol.channel.v2.membership.ChannelMembershipListener")
		{
			base.Exported = true;
			this.Methods = new MethodDescriptor[5];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.channel.v2.membership.OnChannelAdded", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChannelAddedNotification>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.channel.v2.membership.OnChannelRemoved", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChannelRemovedNotification>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.channel.v2.membership.OnReceivedInvitationAdded", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ReceivedInvitationAddedNotification>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.channel.v2.membership.OnReceivedInvitationRemoved", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ReceivedInvitationRemovedNotification>));
		}

		// Token: 0x04001069 RID: 4201
		public const uint ON_CHANNEL_ADDED_ID = 1U;

		// Token: 0x0400106A RID: 4202
		public const uint ON_CHANNEL_REMOVED_ID = 2U;

		// Token: 0x0400106B RID: 4203
		public const uint ON_RECEIVED_INVITATION_ADDED_ID = 3U;

		// Token: 0x0400106C RID: 4204
		public const uint ON_RECEIVED_INVITATION_REMOVED_ID = 4U;
	}
}
