using System;
using bnet.protocol.friends.v1;

namespace bgs.RPCServices
{
	// Token: 0x02000297 RID: 663
	public class FriendsNotify : ServiceDescriptor
	{
		// Token: 0x0600260D RID: 9741 RVA: 0x00087DC4 File Offset: 0x00085FC4
		public FriendsNotify() : base("bnet.protocol.friends.FriendsNotify")
		{
			base.Exported = true;
			this.Methods = new MethodDescriptor[8];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsNotify.NotifyFriendAdded", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<FriendNotification>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsNotify.NotifyFriendRemoved", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<FriendNotification>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsNotify.NotifyReceivedInvitationAdded", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationNotification>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsNotify.NotifyReceivedInvitationRemoved", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationNotification>));
			this.Methods[5] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsNotify.NotifySentInvitationAdded", 5U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationNotification>));
			this.Methods[6] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsNotify.NotifySentInvitationRemoved", 6U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationNotification>));
			this.Methods[7] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsNotify.NotifyUpdateFriendState", 7U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<UpdateFriendStateNotification>));
		}

		// Token: 0x040010BC RID: 4284
		public const uint NOTIFY_FRIEND_ADDED = 1U;

		// Token: 0x040010BD RID: 4285
		public const uint NOTIFY_FRIEND_REMOVED = 2U;

		// Token: 0x040010BE RID: 4286
		public const uint NOTIFY_RECEIVED_INVITATION_ADDED = 3U;

		// Token: 0x040010BF RID: 4287
		public const uint NOTIFY_RECEIVED_INVITATION_REMOVED = 4U;

		// Token: 0x040010C0 RID: 4288
		public const uint NOTIFY_SENT_INVITATION_ADDED = 5U;

		// Token: 0x040010C1 RID: 4289
		public const uint NOTIFY_SENT_INVITATION_REMOVED = 6U;

		// Token: 0x040010C2 RID: 4290
		public const uint NOTIFY_UPDATE_FRIEND_STATE = 7U;
	}
}
