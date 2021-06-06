using System;
using bnet.protocol;

namespace bgs.RPCServices
{
	// Token: 0x02000294 RID: 660
	public class NotificationService : ServiceDescriptor
	{
		// Token: 0x0600260A RID: 9738 RVA: 0x00087B9C File Offset: 0x00085D9C
		public NotificationService() : base("bnet.protocol.notification.NotificationService")
		{
			base.Imported = true;
			this.Methods = new MethodDescriptor[2];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.notification.v1.NotificationService.SendNotification", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
		}

		// Token: 0x040010AE RID: 4270
		public const uint SEND_NOTIFICATION_ID = 1U;
	}
}
