using System;
using bnet.protocol.channel.v1;

namespace bgs.RPCServices
{
	// Token: 0x02000290 RID: 656
	public class ChannelOwnerService : ServiceDescriptor
	{
		// Token: 0x06002606 RID: 9734 RVA: 0x00087820 File Offset: 0x00085A20
		public ChannelOwnerService() : base("bnet.protocol.channel.ChannelOwner")
		{
			base.Imported = true;
			this.Methods = new MethodDescriptor[7];
			this.Methods[2] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.CreateChannel", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<CreateChannelResponse>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.JoinChannel", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<JoinChannelResponse>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.FindChannel", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ListChannelsResponse>));
			this.Methods[5] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.GetChannelInfo", 5U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetChannelInfoResponse>));
			this.Methods[6] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.SubscribeChannel", 6U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SubscribeChannelResponse>));
		}

		// Token: 0x04001097 RID: 4247
		public const uint CREATE_CHANNEL_ID = 2U;

		// Token: 0x04001098 RID: 4248
		public const uint JOIN_CHANNEL_ID = 3U;

		// Token: 0x04001099 RID: 4249
		public const uint FIND_CHANNEL_ID = 4U;

		// Token: 0x0400109A RID: 4250
		public const uint GET_CHANNEL_INFO_ID = 5U;

		// Token: 0x0400109B RID: 4251
		public const uint SUBSCRIBE_CHANNEL_ID = 6U;
	}
}
