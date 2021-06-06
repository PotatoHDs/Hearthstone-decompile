using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeAdventureProgress : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 14
		}

		public int WingId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ WingId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeAdventureProgress profileNoticeAdventureProgress = obj as ProfileNoticeAdventureProgress;
			if (profileNoticeAdventureProgress == null)
			{
				return false;
			}
			if (!WingId.Equals(profileNoticeAdventureProgress.WingId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeAdventureProgress Deserialize(Stream stream, ProfileNoticeAdventureProgress instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeAdventureProgress DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeAdventureProgress profileNoticeAdventureProgress = new ProfileNoticeAdventureProgress();
			DeserializeLengthDelimited(stream, profileNoticeAdventureProgress);
			return profileNoticeAdventureProgress;
		}

		public static ProfileNoticeAdventureProgress DeserializeLengthDelimited(Stream stream, ProfileNoticeAdventureProgress instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeAdventureProgress Deserialize(Stream stream, ProfileNoticeAdventureProgress instance, long limit)
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
					instance.WingId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeAdventureProgress instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.WingId);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)WingId) + 1;
		}
	}
}
