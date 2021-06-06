using bnet.protocol;
using bnet.protocol.friends.v1;

namespace bgs.RPCServices
{
	public class FriendsService : ServiceDescriptor
	{
		public const uint SUBSCRIBE_TO_FRIENDS = 1u;

		public const uint SEND_INVITATION = 2u;

		public const uint ACCEPT_INVITATION = 3u;

		public const uint REVOKE_INVITATION = 4u;

		public const uint DECLINE_INVITATION = 5u;

		public const uint IGNORE_INVITATION = 6u;

		public const uint ASSIGN_ROLE = 7u;

		public const uint REMOVE_FRIEND = 8u;

		public const uint VIEW_FRIENDS = 9u;

		public const uint UPDATE_FRIEND_STATE = 10u;

		public const uint UNSUBSCRIBE_TO_FRIENDS = 11u;

		public const uint REVOKE_ALL_INVITATIONS = 12u;

		public FriendsService()
			: base("bnet.protocol.friends.FriendsService")
		{
			base.Imported = true;
			Methods = new MethodDescriptor[13];
			Methods[1] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.SubscribeToFriends", 1u, ProtobufUtil.ParseFromGeneric<SubscribeResponse>);
			Methods[2] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.SendInvitation", 2u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[3] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.AcceptInvitation", 3u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[4] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.RevokeInvitation", 4u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[5] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.DeclineInvitation", 5u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[6] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.IgnoreInvitation", 6u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[7] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.AssignRole", 7u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[8] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.RemoveFriend", 8u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[9] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.ViewFriends", 9u, ProtobufUtil.ParseFromGeneric<ViewFriendsResponse>);
			Methods[10] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.UpdateFriendState", 10u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[11] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.UnsubscribeToFriends", 11u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[12] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.RevokeAllInvitations", 12u, ProtobufUtil.ParseFromGeneric<NoData>);
		}
	}
}
