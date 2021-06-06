using System;
using bnet.protocol;
using bnet.protocol.channel.v1;

namespace bgs.RPCServices
{
	// Token: 0x02000291 RID: 657
	public class ChannelInvitationService : ServiceDescriptor
	{
		// Token: 0x06002607 RID: 9735 RVA: 0x000878E8 File Offset: 0x00085AE8
		public ChannelInvitationService() : base("bnet.protocol.channel_invitation.ChannelInvitationService")
		{
			base.Imported = true;
			this.Methods = new MethodDescriptor[12];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.Subscribe", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.Unsubscribe", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.SendInvitation", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.AcceptInvitation", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<AcceptInvitationResponse>));
			this.Methods[5] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.DeclineInvitation", 5U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[6] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.RevokeInvitation", 6U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[7] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.SuggestInvitation", 7U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[11] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.ListChannelCount", 11U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ListChannelCountResponse>));
		}

		// Token: 0x0400109C RID: 4252
		public const uint SUBSCRIBE_ID = 1U;

		// Token: 0x0400109D RID: 4253
		public const uint UNSUBSCRIBE_ID = 2U;

		// Token: 0x0400109E RID: 4254
		public const uint SEND_INVITATION_ID = 3U;

		// Token: 0x0400109F RID: 4255
		public const uint ACCEPT_INVITATION_ID = 4U;

		// Token: 0x040010A0 RID: 4256
		public const uint DECLINE_INVITATION_ID = 5U;

		// Token: 0x040010A1 RID: 4257
		public const uint REVOKE_INVITATION_ID = 6U;

		// Token: 0x040010A2 RID: 4258
		public const uint SUGGEST_INVITATION_ID = 7U;

		// Token: 0x040010A3 RID: 4259
		public const uint LIST_CHANNEL_COUNT_ID = 11U;
	}
}
