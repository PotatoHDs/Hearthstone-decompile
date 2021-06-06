using System;
using bnet.protocol.channel.v2;

namespace bgs.RPCServices
{
	// Token: 0x0200028E RID: 654
	public class ChannelListener : ServiceDescriptor
	{
		// Token: 0x06002604 RID: 9732 RVA: 0x0008756C File Offset: 0x0008576C
		public ChannelListener() : base("bnet.protocol.channel.v2.ChannelListener")
		{
			base.Exported = true;
			this.Methods = new MethodDescriptor[21];
			this.Methods[3] = new MethodDescriptor("bnet.protocol.channel.v2.OnMemberAdded", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<MemberAddedNotification>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.channel.v2.OnMemberRemoved", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<MemberRemovedNotification>));
			this.Methods[5] = new MethodDescriptor("bnet.protocol.channel.v2.OnMemberAttributeChanged", 5U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<MemberAttributeChangedNotification>));
			this.Methods[6] = new MethodDescriptor("bnet.protocol.channel.v2.OnMemberRoleChanged", 6U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<MemberRoleChangedNotification>));
			this.Methods[10] = new MethodDescriptor("bnet.protocol.channel.v2.OnSendMessage", 10U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SendMessageNotification>));
			this.Methods[11] = new MethodDescriptor("bnet.protocol.channel.v2.OnTypingIndicator", 11U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<TypingIndicatorNotification>));
			this.Methods[16] = new MethodDescriptor("bnet.protocol.channel.v2.OnAttributeChanged", 16U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<AttributeChangedNotification>));
			this.Methods[17] = new MethodDescriptor("bnet.protocol.channel.v2.OnPrivacyLevelChanged", 17U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<PrivacyLevelChangedNotification>));
			this.Methods[18] = new MethodDescriptor("bnet.protocol.channel.v2.OnInvitationAdded", 18U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationAddedNotification>));
			this.Methods[19] = new MethodDescriptor("bnet.protocol.channel.v2.OnInvitationRemoved", 19U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationRemovedNotification>));
			this.Methods[20] = new MethodDescriptor("bnet.protocol.channel.v2.OnSuggestionAdded", 20U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SuggestionAddedNotification>));
		}

		// Token: 0x04001084 RID: 4228
		public const uint ON_MEMBER_ADDED_ID = 3U;

		// Token: 0x04001085 RID: 4229
		public const uint ON_MEMBER_REMOVED_ID = 4U;

		// Token: 0x04001086 RID: 4230
		public const uint ON_MEMBER_ATTRIBUTE_CHANGED_ID = 5U;

		// Token: 0x04001087 RID: 4231
		public const uint ON_MEMBER_ROLE_CHANGED_ID = 6U;

		// Token: 0x04001088 RID: 4232
		public const uint ON_SEND_MESSAGE_ID = 10U;

		// Token: 0x04001089 RID: 4233
		public const uint ON_TYPING_INDICATOR_ID = 11U;

		// Token: 0x0400108A RID: 4234
		public const uint ON_ATTRIBUTE_CHANGED_ID = 16U;

		// Token: 0x0400108B RID: 4235
		public const uint ON_PRIVACY_LEVEL_CHANGED_ID = 17U;

		// Token: 0x0400108C RID: 4236
		public const uint ON_INVITATION_ADDED_ID = 18U;

		// Token: 0x0400108D RID: 4237
		public const uint ON_INVITATION_REMOVED_ID = 19U;

		// Token: 0x0400108E RID: 4238
		public const uint ON_SUGGESTION_ADDED_ID = 20U;
	}
}
