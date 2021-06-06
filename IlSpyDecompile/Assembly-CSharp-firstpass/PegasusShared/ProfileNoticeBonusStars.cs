using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeBonusStars : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 12
		}

		public int StarLevel { get; set; }

		public int Stars { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ StarLevel.GetHashCode() ^ Stars.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeBonusStars profileNoticeBonusStars = obj as ProfileNoticeBonusStars;
			if (profileNoticeBonusStars == null)
			{
				return false;
			}
			if (!StarLevel.Equals(profileNoticeBonusStars.StarLevel))
			{
				return false;
			}
			if (!Stars.Equals(profileNoticeBonusStars.Stars))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeBonusStars Deserialize(Stream stream, ProfileNoticeBonusStars instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeBonusStars DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeBonusStars profileNoticeBonusStars = new ProfileNoticeBonusStars();
			DeserializeLengthDelimited(stream, profileNoticeBonusStars);
			return profileNoticeBonusStars;
		}

		public static ProfileNoticeBonusStars DeserializeLengthDelimited(Stream stream, ProfileNoticeBonusStars instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeBonusStars Deserialize(Stream stream, ProfileNoticeBonusStars instance, long limit)
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
					instance.StarLevel = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Stars = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeBonusStars instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.StarLevel);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Stars);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)StarLevel) + ProtocolParser.SizeOfUInt64((ulong)Stars) + 2;
		}
	}
}
