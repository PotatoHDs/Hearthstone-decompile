using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeRewardBooster : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 2
		}

		public int BoosterType { get; set; }

		public int BoosterCount { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ BoosterType.GetHashCode() ^ BoosterCount.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeRewardBooster profileNoticeRewardBooster = obj as ProfileNoticeRewardBooster;
			if (profileNoticeRewardBooster == null)
			{
				return false;
			}
			if (!BoosterType.Equals(profileNoticeRewardBooster.BoosterType))
			{
				return false;
			}
			if (!BoosterCount.Equals(profileNoticeRewardBooster.BoosterCount))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeRewardBooster Deserialize(Stream stream, ProfileNoticeRewardBooster instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeRewardBooster DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeRewardBooster profileNoticeRewardBooster = new ProfileNoticeRewardBooster();
			DeserializeLengthDelimited(stream, profileNoticeRewardBooster);
			return profileNoticeRewardBooster;
		}

		public static ProfileNoticeRewardBooster DeserializeLengthDelimited(Stream stream, ProfileNoticeRewardBooster instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeRewardBooster Deserialize(Stream stream, ProfileNoticeRewardBooster instance, long limit)
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
					instance.BoosterType = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.BoosterCount = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeRewardBooster instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.BoosterType);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.BoosterCount);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)BoosterType) + ProtocolParser.SizeOfUInt64((ulong)BoosterCount) + 2;
		}
	}
}
