using System;
using System.IO;

namespace PegasusUtil
{
	public class NoticeNotification : IProtoBuf
	{
		public ProfileNotice Notice { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Notice.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			NoticeNotification noticeNotification = obj as NoticeNotification;
			if (noticeNotification == null)
			{
				return false;
			}
			if (!Notice.Equals(noticeNotification.Notice))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static NoticeNotification Deserialize(Stream stream, NoticeNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static NoticeNotification DeserializeLengthDelimited(Stream stream)
		{
			NoticeNotification noticeNotification = new NoticeNotification();
			DeserializeLengthDelimited(stream, noticeNotification);
			return noticeNotification;
		}

		public static NoticeNotification DeserializeLengthDelimited(Stream stream, NoticeNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static NoticeNotification Deserialize(Stream stream, NoticeNotification instance, long limit)
		{
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
					if (instance.Notice == null)
					{
						instance.Notice = ProfileNotice.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNotice.DeserializeLengthDelimited(stream, instance.Notice);
					}
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

		public static void Serialize(Stream stream, NoticeNotification instance)
		{
			if (instance.Notice == null)
			{
				throw new ArgumentNullException("Notice", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Notice.GetSerializedSize());
			ProfileNotice.Serialize(stream, instance.Notice);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = Notice.GetSerializedSize();
			return 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1;
		}
	}
}
