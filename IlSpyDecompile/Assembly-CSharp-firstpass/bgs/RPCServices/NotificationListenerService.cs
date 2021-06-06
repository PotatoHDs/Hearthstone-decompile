using bnet.protocol.notification.v1;

namespace bgs.RPCServices
{
	public class NotificationListenerService : ServiceDescriptor
	{
		public const uint ON_NOTIFICATION_REC_ID = 1u;

		public NotificationListenerService()
			: base("bnet.protocol.notification.NotificationListener")
		{
			base.Exported = true;
			Methods = new MethodDescriptor[2];
			Methods[1] = new MethodDescriptor("bnet.protocol.notification.v1.NotificationListener.OnNotificationReceived", 1u, ProtobufUtil.ParseFromGeneric<Notification>);
		}
	}
}
