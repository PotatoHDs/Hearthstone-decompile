using bnet.protocol.channel.v2;

namespace bgs.RPCServices
{
	public class ChannelListener : ServiceDescriptor
	{
		public const uint ON_MEMBER_ADDED_ID = 3u;

		public const uint ON_MEMBER_REMOVED_ID = 4u;

		public const uint ON_MEMBER_ATTRIBUTE_CHANGED_ID = 5u;

		public const uint ON_MEMBER_ROLE_CHANGED_ID = 6u;

		public const uint ON_SEND_MESSAGE_ID = 10u;

		public const uint ON_TYPING_INDICATOR_ID = 11u;

		public const uint ON_ATTRIBUTE_CHANGED_ID = 16u;

		public const uint ON_PRIVACY_LEVEL_CHANGED_ID = 17u;

		public const uint ON_INVITATION_ADDED_ID = 18u;

		public const uint ON_INVITATION_REMOVED_ID = 19u;

		public const uint ON_SUGGESTION_ADDED_ID = 20u;

		public ChannelListener()
			: base("bnet.protocol.channel.v2.ChannelListener")
		{
			base.Exported = true;
			Methods = new MethodDescriptor[21];
			Methods[3] = new MethodDescriptor("bnet.protocol.channel.v2.OnMemberAdded", 3u, ProtobufUtil.ParseFromGeneric<MemberAddedNotification>);
			Methods[4] = new MethodDescriptor("bnet.protocol.channel.v2.OnMemberRemoved", 4u, ProtobufUtil.ParseFromGeneric<MemberRemovedNotification>);
			Methods[5] = new MethodDescriptor("bnet.protocol.channel.v2.OnMemberAttributeChanged", 5u, ProtobufUtil.ParseFromGeneric<MemberAttributeChangedNotification>);
			Methods[6] = new MethodDescriptor("bnet.protocol.channel.v2.OnMemberRoleChanged", 6u, ProtobufUtil.ParseFromGeneric<MemberRoleChangedNotification>);
			Methods[10] = new MethodDescriptor("bnet.protocol.channel.v2.OnSendMessage", 10u, ProtobufUtil.ParseFromGeneric<SendMessageNotification>);
			Methods[11] = new MethodDescriptor("bnet.protocol.channel.v2.OnTypingIndicator", 11u, ProtobufUtil.ParseFromGeneric<TypingIndicatorNotification>);
			Methods[16] = new MethodDescriptor("bnet.protocol.channel.v2.OnAttributeChanged", 16u, ProtobufUtil.ParseFromGeneric<AttributeChangedNotification>);
			Methods[17] = new MethodDescriptor("bnet.protocol.channel.v2.OnPrivacyLevelChanged", 17u, ProtobufUtil.ParseFromGeneric<PrivacyLevelChangedNotification>);
			Methods[18] = new MethodDescriptor("bnet.protocol.channel.v2.OnInvitationAdded", 18u, ProtobufUtil.ParseFromGeneric<InvitationAddedNotification>);
			Methods[19] = new MethodDescriptor("bnet.protocol.channel.v2.OnInvitationRemoved", 19u, ProtobufUtil.ParseFromGeneric<InvitationRemovedNotification>);
			Methods[20] = new MethodDescriptor("bnet.protocol.channel.v2.OnSuggestionAdded", 20u, ProtobufUtil.ParseFromGeneric<SuggestionAddedNotification>);
		}
	}
}
