using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeRewardForge : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 8
		}

		public int Quantity { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Quantity.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeRewardForge profileNoticeRewardForge = obj as ProfileNoticeRewardForge;
			if (profileNoticeRewardForge == null)
			{
				return false;
			}
			if (!Quantity.Equals(profileNoticeRewardForge.Quantity))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeRewardForge Deserialize(Stream stream, ProfileNoticeRewardForge instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeRewardForge DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeRewardForge profileNoticeRewardForge = new ProfileNoticeRewardForge();
			DeserializeLengthDelimited(stream, profileNoticeRewardForge);
			return profileNoticeRewardForge;
		}

		public static ProfileNoticeRewardForge DeserializeLengthDelimited(Stream stream, ProfileNoticeRewardForge instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeRewardForge Deserialize(Stream stream, ProfileNoticeRewardForge instance, long limit)
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
					instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeRewardForge instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Quantity);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Quantity) + 1;
		}
	}
}
