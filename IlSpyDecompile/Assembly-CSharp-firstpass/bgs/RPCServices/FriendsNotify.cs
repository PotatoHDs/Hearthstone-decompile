using bnet.protocol.friends.v1;

namespace bgs.RPCServices
{
	public class FriendsNotify : ServiceDescriptor
	{
		public const uint NOTIFY_FRIEND_ADDED = 1u;

		public const uint NOTIFY_FRIEND_REMOVED = 2u;

		public const uint NOTIFY_RECEIVED_INVITATION_ADDED = 3u;

		public const uint NOTIFY_RECEIVED_INVITATION_REMOVED = 4u;

		public const uint NOTIFY_SENT_INVITATION_ADDED = 5u;

		public const uint NOTIFY_SENT_INVITATION_REMOVED = 6u;

		public const uint NOTIFY_UPDATE_FRIEND_STATE = 7u;

		public FriendsNotify()
			: base("bnet.protocol.friends.FriendsNotify")
		{
			base.Exported = true;
			Methods = new MethodDescriptor[8];
			Methods[1] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsNotify.NotifyFriendAdded", 1u, ProtobufUtil.ParseFromGeneric<FriendNotification>);
			Methods[2] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsNotify.NotifyFriendRemoved", 2u, ProtobufUtil.ParseFromGeneric<FriendNotification>);
			Methods[3] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsNotify.NotifyReceivedInvitationAdded", 3u, ProtobufUtil.ParseFromGeneric<InvitationNotification>);
			Methods[4] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsNotify.NotifyReceivedInvitationRemoved", 4u, ProtobufUtil.ParseFromGeneric<InvitationNotification>);
			Methods[5] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsNotify.NotifySentInvitationAdded", 5u, ProtobufUtil.ParseFromGeneric<InvitationNotification>);
			Methods[6] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsNotify.NotifySentInvitationRemoved", 6u, ProtobufUtil.ParseFromGeneric<InvitationNotification>);
			Methods[7] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsNotify.NotifyUpdateFriendState", 7u, ProtobufUtil.ParseFromGeneric<UpdateFriendStateNotification>);
		}
	}
}
