using System;
using bnet.protocol.notification.v1;

namespace bgs.RPCServices
{
	// Token: 0x02000295 RID: 661
	public class NotificationListenerService : ServiceDescriptor
	{
		// Token: 0x0600260B RID: 9739 RVA: 0x00087BDB File Offset: 0x00085DDB
		public NotificationListenerService() : base("bnet.protocol.notification.NotificationListener")
		{
			base.Exported = true;
			this.Methods = new MethodDescriptor[2];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.notification.v1.NotificationListener.OnNotificationReceived", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<Notification>));
		}

		// Token: 0x040010AF RID: 4271
		public const uint ON_NOTIFICATION_REC_ID = 1U;
	}
}
