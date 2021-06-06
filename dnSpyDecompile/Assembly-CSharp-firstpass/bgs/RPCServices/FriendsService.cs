using System;
using bnet.protocol;
using bnet.protocol.friends.v1;

namespace bgs.RPCServices
{
	// Token: 0x02000296 RID: 662
	public class FriendsService : ServiceDescriptor
	{
		// Token: 0x0600260C RID: 9740 RVA: 0x00087C1C File Offset: 0x00085E1C
		public FriendsService() : base("bnet.protocol.friends.FriendsService")
		{
			base.Imported = true;
			this.Methods = new MethodDescriptor[13];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.SubscribeToFriends", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SubscribeResponse>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.SendInvitation", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.AcceptInvitation", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.RevokeInvitation", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[5] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.DeclineInvitation", 5U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[6] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.IgnoreInvitation", 6U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[7] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.AssignRole", 7U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[8] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.RemoveFriend", 8U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[9] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.ViewFriends", 9U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ViewFriendsResponse>));
			this.Methods[10] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.UpdateFriendState", 10U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[11] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.UnsubscribeToFriends", 11U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[12] = new MethodDescriptor("bnet.protocol.friends.v1.FriendsService.RevokeAllInvitations", 12U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
		}

		// Token: 0x040010B0 RID: 4272
		public const uint SUBSCRIBE_TO_FRIENDS = 1U;

		// Token: 0x040010B1 RID: 4273
		public const uint SEND_INVITATION = 2U;

		// Token: 0x040010B2 RID: 4274
		public const uint ACCEPT_INVITATION = 3U;

		// Token: 0x040010B3 RID: 4275
		public const uint REVOKE_INVITATION = 4U;

		// Token: 0x040010B4 RID: 4276
		public const uint DECLINE_INVITATION = 5U;

		// Token: 0x040010B5 RID: 4277
		public const uint IGNORE_INVITATION = 6U;

		// Token: 0x040010B6 RID: 4278
		public const uint ASSIGN_ROLE = 7U;

		// Token: 0x040010B7 RID: 4279
		public const uint REMOVE_FRIEND = 8U;

		// Token: 0x040010B8 RID: 4280
		public const uint VIEW_FRIENDS = 9U;

		// Token: 0x040010B9 RID: 4281
		public const uint UPDATE_FRIEND_STATE = 10U;

		// Token: 0x040010BA RID: 4282
		public const uint UNSUBSCRIBE_TO_FRIENDS = 11U;

		// Token: 0x040010BB RID: 4283
		public const uint REVOKE_ALL_INVITATIONS = 12U;
	}
}
