namespace bgs
{
	public struct BnetNotification
	{
		public const string NotificationType_UtilNotificationMessage = "WTCG.UtilNotificationMessage";

		public const string NotificationAttribute_MessageType = "message_type";

		public const string NotificationAttribute_MessageSize = "message_size";

		public const string NotificationAttribute_MessageFragmentPrefix = "fragment_";

		public string NotificationType;

		public byte[] BlobMessage;

		public int MessageType;

		public int MessageSize;

		public BnetNotification(string notificationType)
		{
			NotificationType = notificationType;
			BlobMessage = new byte[0];
			MessageType = 0;
			MessageSize = 0;
		}
	}
}
