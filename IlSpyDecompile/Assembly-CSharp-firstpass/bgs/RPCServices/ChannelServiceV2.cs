using bnet.protocol;
using bnet.protocol.channel.v2;

namespace bgs.RPCServices
{
	public class ChannelServiceV2 : ServiceDescriptor
	{
		public const uint CREATE_CHANNEL_ID = 2u;

		public const uint DISSOLVE_CHANNEL_ID = 3u;

		public const uint GET_CHANNEL_ID = 4u;

		public const uint GET_PUBLIC_CHANNEL_TYPES_ID = 5u;

		public const uint FIND_CHANNEL_ID = 6u;

		public const uint SUBSCRIBE_ID = 10u;

		public const uint UNSUBSCRIBE_ID = 11u;

		public const uint SET_ATTRIBUTE_ID = 21u;

		public const uint SET_PRIVACY_LEVEL_ID = 22u;

		public const uint SEND_MESSAGE_ID = 23u;

		public const uint SET_TYPING_INDICATOR_ID = 24u;

		public const uint JOIN_ID = 30u;

		public const uint LEAVE_ID = 31u;

		public const uint KICK_ID = 32u;

		public const uint SET_MEMBER_ATTRTIBUTE_ID = 40u;

		public const uint ASSIGN_ROLE_ID = 41u;

		public const uint UNASSIGN_ROLE_ID = 42u;

		public const uint SEND_INVITATION_ID = 50u;

		public const uint ACCEPT_INVITATION_ID = 51u;

		public const uint DECLINE_INVITATION_ID = 52u;

		public const uint REVOKE_INVITATION_ID = 53u;

		public const uint SEND_SUGGESTION_ID = 60u;

		public const uint GET_VOICE_JOIN_TOKEN_ID = 70u;

		public ChannelServiceV2()
			: base("bnet.protocol.channel.v2.ChannelService")
		{
			base.Imported = true;
			Methods = new MethodDescriptor[71];
			Methods[2] = new MethodDescriptor("bnet.protocol.channel.v2.CreateChannel", 2u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[3] = new MethodDescriptor("bnet.protocol.channel.v2.DissolveChannel", 3u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[4] = new MethodDescriptor("bnet.protocol.channel.v2.GetChannel", 4u, ProtobufUtil.ParseFromGeneric<GetChannelResponse>);
			Methods[5] = new MethodDescriptor("bnet.protocol.channel.v2.GetPublicChannelTypes", 5u, ProtobufUtil.ParseFromGeneric<GetPublicChannelTypesResponse>);
			Methods[6] = new MethodDescriptor("bnet.protocol.channel.v2.FindChannel", 6u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[10] = new MethodDescriptor("bnet.protocol.channel.v2.Subscribe", 10u, ProtobufUtil.ParseFromGeneric<SubscribeResponse>);
			Methods[11] = new MethodDescriptor("bnet.protocol.channel.v2.Unsubscribe", 11u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[21] = new MethodDescriptor("bnet.protocol.channel.v2.SetAttribute", 21u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[22] = new MethodDescriptor("bnet.protocol.channel.v2.SetPrivacyLevel", 22u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[23] = new MethodDescriptor("bnet.protocol.channel.v2.SendMessage", 23u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[24] = new MethodDescriptor("bnet.protocol.channel.v2.SetTypingIndicator", 24u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[30] = new MethodDescriptor("bnet.protocol.channel.v2.Join", 30u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[31] = new MethodDescriptor("bnet.protocol.channel.v2.Leave", 31u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[32] = new MethodDescriptor("bnet.protocol.channel.v2.Kick", 32u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[40] = new MethodDescriptor("bnet.protocol.channel.v2.SetMemberAttribute", 40u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[41] = new MethodDescriptor("bnet.protocol.channel.v2.AssignRole", 41u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[42] = new MethodDescriptor("bnet.protocol.channel.v2.UnassignRole", 42u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[50] = new MethodDescriptor("bnet.protocol.channel.v2.SendInvitation", 50u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[51] = new MethodDescriptor("bnet.protocol.channel.v2.AcceptInvitation", 51u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[52] = new MethodDescriptor("bnet.protocol.channel.v2.DeclineInvitation", 52u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[53] = new MethodDescriptor("bnet.protocol.channel.v2.RevokeInvitation", 53u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[60] = new MethodDescriptor("bnet.protocol.channel.v2.SendSuggestion", 60u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[70] = new MethodDescriptor("bnet.protocol.channel.v2.GetVoiceJoinToken", 70u, ProtobufUtil.ParseFromGeneric<GetJoinVoiceTokenResponse>);
		}
	}
}
