using System;
using bnet.protocol;
using bnet.protocol.channel.v2.membership;

namespace bgs.RPCServices
{
	// Token: 0x0200028B RID: 651
	public class ChannelMembershipService : ServiceDescriptor
	{
		// Token: 0x06002601 RID: 9729 RVA: 0x00087120 File Offset: 0x00085320
		public ChannelMembershipService() : base("bnet.protocol.channel.v2.membership.ChannelMembershipService")
		{
			base.Imported = true;
			this.Methods = new MethodDescriptor[4];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.channel.v2.membership.Subscribe", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SubscribeResponse>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.channel.v2.membership.Unsubscribe", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.channel.v2.membership.GetState", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetStateResponse>));
		}

		// Token: 0x04001066 RID: 4198
		public const uint SUBSCRIBE_ID = 1U;

		// Token: 0x04001067 RID: 4199
		public const uint UNSUBSCRIBE_ID = 2U;

		// Token: 0x04001068 RID: 4200
		public const uint GET_STATE_ID = 3U;
	}
}
