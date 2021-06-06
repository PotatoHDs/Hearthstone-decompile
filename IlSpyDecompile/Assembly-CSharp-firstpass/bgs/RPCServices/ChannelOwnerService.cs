using bnet.protocol.channel.v1;

namespace bgs.RPCServices
{
	public class ChannelOwnerService : ServiceDescriptor
	{
		public const uint CREATE_CHANNEL_ID = 2u;

		public const uint JOIN_CHANNEL_ID = 3u;

		public const uint FIND_CHANNEL_ID = 4u;

		public const uint GET_CHANNEL_INFO_ID = 5u;

		public const uint SUBSCRIBE_CHANNEL_ID = 6u;

		public ChannelOwnerService()
			: base("bnet.protocol.channel.ChannelOwner")
		{
			base.Imported = true;
			Methods = new MethodDescriptor[7];
			Methods[2] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.CreateChannel", 2u, ProtobufUtil.ParseFromGeneric<CreateChannelResponse>);
			Methods[3] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.JoinChannel", 3u, ProtobufUtil.ParseFromGeneric<JoinChannelResponse>);
			Methods[4] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.FindChannel", 4u, ProtobufUtil.ParseFromGeneric<ListChannelsResponse>);
			Methods[5] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.GetChannelInfo", 5u, ProtobufUtil.ParseFromGeneric<GetChannelInfoResponse>);
			Methods[6] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.SubscribeChannel", 6u, ProtobufUtil.ParseFromGeneric<SubscribeChannelResponse>);
		}
	}
}
