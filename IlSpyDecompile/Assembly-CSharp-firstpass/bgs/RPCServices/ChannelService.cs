using bnet.protocol;

namespace bgs.RPCServices
{
	public class ChannelService : ServiceDescriptor
	{
		public const uint ADD_MEMBER_ID = 1u;

		public const uint REMOVE_MEMBER_ID = 2u;

		public const uint SEND_MESSAGE_ID = 3u;

		public const uint UPDATE_CHANNEL_STATE_ID = 4u;

		public const uint UPDATE_MEMBER_STATE_ID = 5u;

		public const uint DISSOLVE_ID = 6u;

		public const uint SETROLES_ID = 7u;

		public const uint UNSUBSCRIBE_MEMBER_ID = 8u;

		public ChannelService()
			: base("bnet.protocol.channel.Channel")
		{
			base.Imported = true;
			Methods = new MethodDescriptor[9];
			Methods[1] = new MethodDescriptor("bnet.protocol.channel.Channel.AddMember", 1u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[2] = new MethodDescriptor("bnet.protocol.channel.Channel.RemoveMember", 2u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[3] = new MethodDescriptor("bnet.protocol.channel.Channel.SendMessage", 3u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[4] = new MethodDescriptor("bnet.protocol.channel.Channel.UpdateChannelState", 4u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[5] = new MethodDescriptor("bnet.protocol.channel.Channel.UpdateMemberState", 5u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[6] = new MethodDescriptor("bnet.protocol.channel.Channel.Dissolve", 6u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[7] = new MethodDescriptor("bnet.protocol.channel.Channel.AddMember", 7u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[8] = new MethodDescriptor("bnet.protocol.channel.Channel.UnsubscribeMember", 8u, ProtobufUtil.ParseFromGeneric<NoData>);
		}
	}
}
