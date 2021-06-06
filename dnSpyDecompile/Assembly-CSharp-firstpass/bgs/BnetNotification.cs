using System;

namespace bgs
{
	// Token: 0x0200023E RID: 574
	public struct BnetNotification
	{
		// Token: 0x06002408 RID: 9224 RVA: 0x0007EF0F File Offset: 0x0007D10F
		public BnetNotification(string notificationType)
		{
			this.NotificationType = notificationType;
			this.BlobMessage = new byte[0];
			this.MessageType = 0;
			this.MessageSize = 0;
		}

		// Token: 0x04000EAF RID: 3759
		public const string NotificationType_UtilNotificationMessage = "WTCG.UtilNotificationMessage";

		// Token: 0x04000EB0 RID: 3760
		public const string NotificationAttribute_MessageType = "message_type";

		// Token: 0x04000EB1 RID: 3761
		public const string NotificationAttribute_MessageSize = "message_size";

		// Token: 0x04000EB2 RID: 3762
		public const string NotificationAttribute_MessageFragmentPrefix = "fragment_";

		// Token: 0x04000EB3 RID: 3763
		public string NotificationType;

		// Token: 0x04000EB4 RID: 3764
		public byte[] BlobMessage;

		// Token: 0x04000EB5 RID: 3765
		public int MessageType;

		// Token: 0x04000EB6 RID: 3766
		public int MessageSize;
	}
}
