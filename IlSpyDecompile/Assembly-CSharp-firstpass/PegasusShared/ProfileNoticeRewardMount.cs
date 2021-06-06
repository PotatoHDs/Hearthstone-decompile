using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeRewardMount : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 7
		}

		public int MountId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ MountId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeRewardMount profileNoticeRewardMount = obj as ProfileNoticeRewardMount;
			if (profileNoticeRewardMount == null)
			{
				return false;
			}
			if (!MountId.Equals(profileNoticeRewardMount.MountId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeRewardMount Deserialize(Stream stream, ProfileNoticeRewardMount instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeRewardMount DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeRewardMount profileNoticeRewardMount = new ProfileNoticeRewardMount();
			DeserializeLengthDelimited(stream, profileNoticeRewardMount);
			return profileNoticeRewardMount;
		}

		public static ProfileNoticeRewardMount DeserializeLengthDelimited(Stream stream, ProfileNoticeRewardMount instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeRewardMount Deserialize(Stream stream, ProfileNoticeRewardMount instance, long limit)
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
					instance.MountId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeRewardMount instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.MountId);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)MountId) + 1;
		}
	}
}
