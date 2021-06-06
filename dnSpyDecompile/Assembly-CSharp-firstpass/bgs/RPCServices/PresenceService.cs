using System;
using bnet.protocol;

namespace bgs.RPCServices
{
	// Token: 0x0200028A RID: 650
	public class PresenceService : ServiceDescriptor
	{
		// Token: 0x06002600 RID: 9728 RVA: 0x00087078 File Offset: 0x00085278
		public PresenceService() : base("bnet.protocol.presence.PresenceService")
		{
			base.Imported = true;
			this.Methods = new MethodDescriptor[5];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.presence.PresenceService.Subscribe", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.presence.PresenceService.Unsubscribe", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.presence.PresenceService.Update", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.presence.PresenceService.Query", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
		}

		// Token: 0x04001062 RID: 4194
		public const uint SUBSCRIBE_ID = 1U;

		// Token: 0x04001063 RID: 4195
		public const uint UNSUBSCRIBE_ID = 2U;

		// Token: 0x04001064 RID: 4196
		public const uint UPDATE_ID = 3U;

		// Token: 0x04001065 RID: 4197
		public const uint QUERY_ID = 4U;
	}
}
