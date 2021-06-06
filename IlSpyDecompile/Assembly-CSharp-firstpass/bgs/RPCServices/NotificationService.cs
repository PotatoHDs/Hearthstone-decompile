using bnet.protocol;

namespace bgs.RPCServices
{
	public class NotificationService : ServiceDescriptor
	{
		public const uint SEND_NOTIFICATION_ID = 1u;

		public NotificationService()
			: base("bnet.protocol.notification.NotificationService")
		{
			base.Imported = true;
			Methods = new MethodDescriptor[2];
			Methods[1] = new MethodDescriptor("bnet.protocol.notification.v1.NotificationService.SendNotification", 1u, ProtobufUtil.ParseFromGeneric<NoData>);
		}
	}
}
