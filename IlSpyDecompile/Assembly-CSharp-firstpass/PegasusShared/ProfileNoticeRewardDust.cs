using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeRewardDust : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 6
		}

		public int Amount { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Amount.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeRewardDust profileNoticeRewardDust = obj as ProfileNoticeRewardDust;
			if (profileNoticeRewardDust == null)
			{
				return false;
			}
			if (!Amount.Equals(profileNoticeRewardDust.Amount))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeRewardDust Deserialize(Stream stream, ProfileNoticeRewardDust instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeRewardDust DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeRewardDust profileNoticeRewardDust = new ProfileNoticeRewardDust();
			DeserializeLengthDelimited(stream, profileNoticeRewardDust);
			return profileNoticeRewardDust;
		}

		public static ProfileNoticeRewardDust DeserializeLengthDelimited(Stream stream, ProfileNoticeRewardDust instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeRewardDust Deserialize(Stream stream, ProfileNoticeRewardDust instance, long limit)
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
					instance.Amount = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeRewardDust instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Amount);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Amount) + 1;
		}
	}
}
