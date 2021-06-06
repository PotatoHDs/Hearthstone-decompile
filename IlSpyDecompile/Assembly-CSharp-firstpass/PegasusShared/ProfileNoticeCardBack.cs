using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeCardBack : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 11
		}

		public int CardBack { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ CardBack.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeCardBack profileNoticeCardBack = obj as ProfileNoticeCardBack;
			if (profileNoticeCardBack == null)
			{
				return false;
			}
			if (!CardBack.Equals(profileNoticeCardBack.CardBack))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeCardBack Deserialize(Stream stream, ProfileNoticeCardBack instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeCardBack DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeCardBack profileNoticeCardBack = new ProfileNoticeCardBack();
			DeserializeLengthDelimited(stream, profileNoticeCardBack);
			return profileNoticeCardBack;
		}

		public static ProfileNoticeCardBack DeserializeLengthDelimited(Stream stream, ProfileNoticeCardBack instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeCardBack Deserialize(Stream stream, ProfileNoticeCardBack instance, long limit)
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
				case 8:
					instance.CardBack = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeCardBack instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CardBack);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)CardBack) + 1;
		}
	}
}
