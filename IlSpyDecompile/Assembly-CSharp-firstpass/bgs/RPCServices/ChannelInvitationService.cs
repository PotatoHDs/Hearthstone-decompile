using bnet.protocol;
using bnet.protocol.channel.v1;

namespace bgs.RPCServices
{
	public class ChannelInvitationService : ServiceDescriptor
	{
		public const uint SUBSCRIBE_ID = 1u;

		public const uint UNSUBSCRIBE_ID = 2u;

		public const uint SEND_INVITATION_ID = 3u;

		public const uint ACCEPT_INVITATION_ID = 4u;

		public const uint DECLINE_INVITATION_ID = 5u;

		public const uint REVOKE_INVITATION_ID = 6u;

		public const uint SUGGEST_INVITATION_ID = 7u;

		public const uint LIST_CHANNEL_COUNT_ID = 11u;

		public ChannelInvitationService()
			: base("bnet.protocol.channel_invitation.ChannelInvitationService")
		{
			base.Imported = true;
			Methods = new MethodDescriptor[12];
			Methods[1] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.Subscribe", 1u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[2] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.Unsubscribe", 2u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[3] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.SendInvitation", 3u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[4] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.AcceptInvitation", 4u, ProtobufUtil.ParseFromGeneric<AcceptInvitationResponse>);
			Methods[5] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.DeclineInvitation", 5u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[6] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.RevokeInvitation", 6u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[7] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.SuggestInvitation", 7u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[11] = new MethodDescriptor("bnet.protocol.channel.v1.ChannelInvitationService.ListChannelCount", 11u, ProtobufUtil.ParseFromGeneric<ListChannelCountResponse>);
		}
	}
}
