using System;
using bnet.protocol;

namespace bgs.RPCServices
{
	// Token: 0x0200028F RID: 655
	public class ChannelService : ServiceDescriptor
	{
		// Token: 0x06002605 RID: 9733 RVA: 0x000876FC File Offset: 0x000858FC
		public ChannelService() : base("bnet.protocol.channel.Channel")
		{
			base.Imported = true;
			this.Methods = new MethodDescriptor[9];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.channel.Channel.AddMember", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.channel.Channel.RemoveMember", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.channel.Channel.SendMessage", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.channel.Channel.UpdateChannelState", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[5] = new MethodDescriptor("bnet.protocol.channel.Channel.UpdateMemberState", 5U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[6] = new MethodDescriptor("bnet.protocol.channel.Channel.Dissolve", 6U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[7] = new MethodDescriptor("bnet.protocol.channel.Channel.AddMember", 7U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[8] = new MethodDescriptor("bnet.protocol.channel.Channel.UnsubscribeMember", 8U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
		}

		// Token: 0x0400108F RID: 4239
		public const uint ADD_MEMBER_ID = 1U;

		// Token: 0x04001090 RID: 4240
		public const uint REMOVE_MEMBER_ID = 2U;

		// Token: 0x04001091 RID: 4241
		public const uint SEND_MESSAGE_ID = 3U;

		// Token: 0x04001092 RID: 4242
		public const uint UPDATE_CHANNEL_STATE_ID = 4U;

		// Token: 0x04001093 RID: 4243
		public const uint UPDATE_MEMBER_STATE_ID = 5U;

		// Token: 0x04001094 RID: 4244
		public const uint DISSOLVE_ID = 6U;

		// Token: 0x04001095 RID: 4245
		public const uint SETROLES_ID = 7U;

		// Token: 0x04001096 RID: 4246
		public const uint UNSUBSCRIBE_MEMBER_ID = 8U;
	}
}
