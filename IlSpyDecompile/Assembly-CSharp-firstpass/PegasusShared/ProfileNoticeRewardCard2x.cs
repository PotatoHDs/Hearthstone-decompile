using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeRewardCard2x : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 13
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ProfileNoticeRewardCard2x))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeRewardCard2x Deserialize(Stream stream, ProfileNoticeRewardCard2x instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeRewardCard2x DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeRewardCard2x profileNoticeRewardCard2x = new ProfileNoticeRewardCard2x();
			DeserializeLengthDelimited(stream, profileNoticeRewardCard2x);
			return profileNoticeRewardCard2x;
		}

		public static ProfileNoticeRewardCard2x DeserializeLengthDelimited(Stream stream, ProfileNoticeRewardCard2x instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeRewardCard2x Deserialize(Stream stream, ProfileNoticeRewardCard2x instance, long limit)
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
				if (num == -1)
				{
					if (limit < 0)
					{
						break;
					}
					throw new EndOfStreamException();
				}
				Key key = ProtocolParser.ReadKey((byte)num, stream);
				if (key.Field == 0)
				{
					throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
				}
				ProtocolParser.SkipKey(stream, key);
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ProfileNoticeRewardCard2x instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
