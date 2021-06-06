using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using bnet.protocol;
using bnet.protocol.notification.v1;

namespace bgs
{
	// Token: 0x02000208 RID: 520
	public class NotificationAPI : BattleNetAPI
	{
		// Token: 0x06002000 RID: 8192 RVA: 0x000717FD File Offset: 0x0006F9FD
		public NotificationAPI(BattleNetCSharp battlenet) : base(battlenet, "Notification")
		{
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x0006D1CC File Offset: 0x0006B3CC
		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x0006C9FD File Offset: 0x0006ABFD
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x0006BFB5 File Offset: 0x0006A1B5
		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x00071818 File Offset: 0x0006FA18
		public void OnNotification(string notificationType, Notification notification)
		{
			if (notification.AttributeCount <= 0)
			{
				return;
			}
			if (notification.HasSenderId && notification.SenderId.Low != 0UL)
			{
				return;
			}
			BnetNotification bnetNotification = new BnetNotification(notificationType);
			SortedDictionary<string, int> sortedDictionary = new SortedDictionary<string, int>();
			int num = 0;
			bnetNotification.MessageType = 0;
			bnetNotification.MessageSize = 0;
			for (int i = 0; i < notification.AttributeCount; i++)
			{
				bnet.protocol.Attribute attribute = notification.Attribute[i];
				if (attribute.Name == "message_type")
				{
					bnetNotification.MessageType = (int)attribute.Value.IntValue;
				}
				else if (attribute.Name == "message_size")
				{
					bnetNotification.MessageSize = (int)attribute.Value.IntValue;
				}
				else if (attribute.Name.StartsWith("fragment_"))
				{
					num += attribute.Value.BlobValue.Length;
					sortedDictionary.Add(attribute.Name, i);
				}
			}
			if (bnetNotification.MessageType == 0)
			{
				BattleNet.Log.LogError(string.Format("Missing notification type {0} of size {1}", bnetNotification.MessageType, bnetNotification.MessageSize));
				return;
			}
			if (0 < num)
			{
				bnetNotification.BlobMessage = new byte[num];
				SortedDictionary<string, int>.Enumerator enumerator = sortedDictionary.GetEnumerator();
				int num2 = 0;
				while (enumerator.MoveNext())
				{
					List<bnet.protocol.Attribute> attribute2 = notification.Attribute;
					KeyValuePair<string, int> keyValuePair = enumerator.Current;
					byte[] blobValue = attribute2[keyValuePair.Value].Value.BlobValue;
					Array.Copy(blobValue, 0, bnetNotification.BlobMessage, num2, blobValue.Length);
					num2 += blobValue.Length;
				}
			}
			if (bnetNotification.MessageSize != num)
			{
				BattleNet.Log.LogError(string.Format("Message size mismatch for notification type {0} - {1} != {2}", bnetNotification.MessageType, bnetNotification.MessageSize, num));
				return;
			}
			this.m_notifications.Add(bnetNotification);
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x000719F2 File Offset: 0x0006FBF2
		public int GetNotificationCount()
		{
			return this.m_notifications.Count;
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x000719FF File Offset: 0x0006FBFF
		public void GetNotifications([Out] BnetNotification[] Notifications)
		{
			this.m_notifications.CopyTo(Notifications, 0);
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x00071A0E File Offset: 0x0006FC0E
		public void ClearNotifications()
		{
			this.m_notifications.Clear();
		}

		// Token: 0x04000B97 RID: 2967
		private List<BnetNotification> m_notifications = new List<BnetNotification>();
	}
}
