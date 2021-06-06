using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class NoticeNotifications : IProtoBuf
	{
		private List<NoticeNotification> _NoticeNotifications_ = new List<NoticeNotification>();

		public List<NoticeNotification> NoticeNotifications_
		{
			get
			{
				return _NoticeNotifications_;
			}
			set
			{
				_NoticeNotifications_ = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (NoticeNotification item in NoticeNotifications_)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			NoticeNotifications noticeNotifications = obj as NoticeNotifications;
			if (noticeNotifications == null)
			{
				return false;
			}
			if (NoticeNotifications_.Count != noticeNotifications.NoticeNotifications_.Count)
			{
				return false;
			}
			for (int i = 0; i < NoticeNotifications_.Count; i++)
			{
				if (!NoticeNotifications_[i].Equals(noticeNotifications.NoticeNotifications_[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static NoticeNotifications Deserialize(Stream stream, NoticeNotifications instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static NoticeNotifications DeserializeLengthDelimited(Stream stream)
		{
			NoticeNotifications noticeNotifications = new NoticeNotifications();
			DeserializeLengthDelimited(stream, noticeNotifications);
			return noticeNotifications;
		}

		public static NoticeNotifications DeserializeLengthDelimited(Stream stream, NoticeNotifications instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static NoticeNotifications Deserialize(Stream stream, NoticeNotifications instance, long limit)
		{
			if (instance.NoticeNotifications_ == null)
			{
				instance.NoticeNotifications_ = new List<NoticeNotification>();
			}
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
					instance.NoticeNotifications_.Add(NoticeNotification.DeserializeLengthDelimited(stream));
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, NoticeNotifications instance)
		{
			if (instance.NoticeNotifications_.Count <= 0)
			{
				return;
			}
			foreach (NoticeNotification item in instance.NoticeNotifications_)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				NoticeNotification.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (NoticeNotifications_.Count > 0)
			{
				foreach (NoticeNotification item in NoticeNotifications_)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
