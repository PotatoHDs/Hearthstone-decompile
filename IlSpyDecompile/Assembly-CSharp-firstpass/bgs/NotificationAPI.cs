using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using bnet.protocol;
using bnet.protocol.notification.v1;

namespace bgs
{
	public class NotificationAPI : BattleNetAPI
	{
		private List<BnetNotification> m_notifications = new List<BnetNotification>();

		public NotificationAPI(BattleNetCSharp battlenet)
			: base(battlenet, "Notification")
		{
		}

		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		public void OnNotification(string notificationType, Notification notification)
		{
			if (notification.AttributeCount <= 0 || (notification.HasSenderId && notification.SenderId.Low != 0L))
			{
				return;
			}
			BnetNotification item = new BnetNotification(notificationType);
			SortedDictionary<string, int> sortedDictionary = new SortedDictionary<string, int>();
			int num = 0;
			item.MessageType = 0;
			item.MessageSize = 0;
			for (int i = 0; i < notification.AttributeCount; i++)
			{
				bnet.protocol.Attribute attribute2 = notification.Attribute[i];
				if (attribute2.Name == "message_type")
				{
					item.MessageType = (int)attribute2.Value.IntValue;
				}
				else if (attribute2.Name == "message_size")
				{
					item.MessageSize = (int)attribute2.Value.IntValue;
				}
				else if (attribute2.Name.StartsWith("fragment_"))
				{
					num += attribute2.Value.BlobValue.Length;
					sortedDictionary.Add(attribute2.Name, i);
				}
			}
			if (item.MessageType == 0)
			{
				BattleNet.Log.LogError($"Missing notification type {item.MessageType} of size {item.MessageSize}");
				return;
			}
			if (0 < num)
			{
				item.BlobMessage = new byte[num];
				SortedDictionary<string, int>.Enumerator enumerator = sortedDictionary.GetEnumerator();
				int num2 = 0;
				while (enumerator.MoveNext())
				{
					byte[] blobValue = notification.Attribute[enumerator.Current.Value].Value.BlobValue;
					Array.Copy(blobValue, 0, item.BlobMessage, num2, blobValue.Length);
					num2 += blobValue.Length;
				}
			}
			if (item.MessageSize != num)
			{
				BattleNet.Log.LogError($"Message size mismatch for notification type {item.MessageType} - {item.MessageSize} != {num}");
			}
			else
			{
				m_notifications.Add(item);
			}
		}

		public int GetNotificationCount()
		{
			return m_notifications.Count;
		}

		public void GetNotifications([Out] BnetNotification[] Notifications)
		{
			m_notifications.CopyTo(Notifications, 0);
		}

		public void ClearNotifications()
		{
			m_notifications.Clear();
		}
	}
}
