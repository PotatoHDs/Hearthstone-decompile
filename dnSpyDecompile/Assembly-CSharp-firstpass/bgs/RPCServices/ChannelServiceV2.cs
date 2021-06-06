using System;
using bnet.protocol;
using bnet.protocol.channel.v2;

namespace bgs.RPCServices
{
	// Token: 0x0200028D RID: 653
	public class ChannelServiceV2 : ServiceDescriptor
	{
		// Token: 0x06002603 RID: 9731 RVA: 0x00087250 File Offset: 0x00085450
		public ChannelServiceV2() : base("bnet.protocol.channel.v2.ChannelService")
		{
			base.Imported = true;
			this.Methods = new MethodDescriptor[71];
			this.Methods[2] = new MethodDescriptor("bnet.protocol.channel.v2.CreateChannel", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.channel.v2.DissolveChannel", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.channel.v2.GetChannel", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetChannelResponse>));
			this.Methods[5] = new MethodDescriptor("bnet.protocol.channel.v2.GetPublicChannelTypes", 5U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetPublicChannelTypesResponse>));
			this.Methods[6] = new MethodDescriptor("bnet.protocol.channel.v2.FindChannel", 6U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[10] = new MethodDescriptor("bnet.protocol.channel.v2.Subscribe", 10U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SubscribeResponse>));
			this.Methods[11] = new MethodDescriptor("bnet.protocol.channel.v2.Unsubscribe", 11U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[21] = new MethodDescriptor("bnet.protocol.channel.v2.SetAttribute", 21U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[22] = new MethodDescriptor("bnet.protocol.channel.v2.SetPrivacyLevel", 22U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[23] = new MethodDescriptor("bnet.protocol.channel.v2.SendMessage", 23U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[24] = new MethodDescriptor("bnet.protocol.channel.v2.SetTypingIndicator", 24U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[30] = new MethodDescriptor("bnet.protocol.channel.v2.Join", 30U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[31] = new MethodDescriptor("bnet.protocol.channel.v2.Leave", 31U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[32] = new MethodDescriptor("bnet.protocol.channel.v2.Kick", 32U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[40] = new MethodDescriptor("bnet.protocol.channel.v2.SetMemberAttribute", 40U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[41] = new MethodDescriptor("bnet.protocol.channel.v2.AssignRole", 41U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[42] = new MethodDescriptor("bnet.protocol.channel.v2.UnassignRole", 42U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[50] = new MethodDescriptor("bnet.protocol.channel.v2.SendInvitation", 50U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[51] = new MethodDescriptor("bnet.protocol.channel.v2.AcceptInvitation", 51U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[52] = new MethodDescriptor("bnet.protocol.channel.v2.DeclineInvitation", 52U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[53] = new MethodDescriptor("bnet.protocol.channel.v2.RevokeInvitation", 53U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[60] = new MethodDescriptor("bnet.protocol.channel.v2.SendSuggestion", 60U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[70] = new MethodDescriptor("bnet.protocol.channel.v2.GetVoiceJoinToken", 70U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetJoinVoiceTokenResponse>));
		}

		// Token: 0x0400106D RID: 4205
		public const uint CREATE_CHANNEL_ID = 2U;

		// Token: 0x0400106E RID: 4206
		public const uint DISSOLVE_CHANNEL_ID = 3U;

		// Token: 0x0400106F RID: 4207
		public const uint GET_CHANNEL_ID = 4U;

		// Token: 0x04001070 RID: 4208
		public const uint GET_PUBLIC_CHANNEL_TYPES_ID = 5U;

		// Token: 0x04001071 RID: 4209
		public const uint FIND_CHANNEL_ID = 6U;

		// Token: 0x04001072 RID: 4210
		public const uint SUBSCRIBE_ID = 10U;

		// Token: 0x04001073 RID: 4211
		public const uint UNSUBSCRIBE_ID = 11U;

		// Token: 0x04001074 RID: 4212
		public const uint SET_ATTRIBUTE_ID = 21U;

		// Token: 0x04001075 RID: 4213
		public const uint SET_PRIVACY_LEVEL_ID = 22U;

		// Token: 0x04001076 RID: 4214
		public const uint SEND_MESSAGE_ID = 23U;

		// Token: 0x04001077 RID: 4215
		public const uint SET_TYPING_INDICATOR_ID = 24U;

		// Token: 0x04001078 RID: 4216
		public const uint JOIN_ID = 30U;

		// Token: 0x04001079 RID: 4217
		public const uint LEAVE_ID = 31U;

		// Token: 0x0400107A RID: 4218
		public const uint KICK_ID = 32U;

		// Token: 0x0400107B RID: 4219
		public const uint SET_MEMBER_ATTRTIBUTE_ID = 40U;

		// Token: 0x0400107C RID: 4220
		public const uint ASSIGN_ROLE_ID = 41U;

		// Token: 0x0400107D RID: 4221
		public const uint UNASSIGN_ROLE_ID = 42U;

		// Token: 0x0400107E RID: 4222
		public const uint SEND_INVITATION_ID = 50U;

		// Token: 0x0400107F RID: 4223
		public const uint ACCEPT_INVITATION_ID = 51U;

		// Token: 0x04001080 RID: 4224
		public const uint DECLINE_INVITATION_ID = 52U;

		// Token: 0x04001081 RID: 4225
		public const uint REVOKE_INVITATION_ID = 53U;

		// Token: 0x04001082 RID: 4226
		public const uint SEND_SUGGESTION_ID = 60U;

		// Token: 0x04001083 RID: 4227
		public const uint GET_VOICE_JOIN_TOKEN_ID = 70U;
	}
}
