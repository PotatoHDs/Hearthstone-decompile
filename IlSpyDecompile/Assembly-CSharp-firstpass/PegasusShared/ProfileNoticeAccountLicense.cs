using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeAccountLicense : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 0x10
		}

		public long License { get; set; }

		public long CasId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ License.GetHashCode() ^ CasId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeAccountLicense profileNoticeAccountLicense = obj as ProfileNoticeAccountLicense;
			if (profileNoticeAccountLicense == null)
			{
				return false;
			}
			if (!License.Equals(profileNoticeAccountLicense.License))
			{
				return false;
			}
			if (!CasId.Equals(profileNoticeAccountLicense.CasId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeAccountLicense Deserialize(Stream stream, ProfileNoticeAccountLicense instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeAccountLicense DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeAccountLicense profileNoticeAccountLicense = new ProfileNoticeAccountLicense();
			DeserializeLengthDelimited(stream, profileNoticeAccountLicense);
			return profileNoticeAccountLicense;
		}

		public static ProfileNoticeAccountLicense DeserializeLengthDelimited(Stream stream, ProfileNoticeAccountLicense instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeAccountLicense Deserialize(Stream stream, ProfileNoticeAccountLicense instance, long limit)
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
					instance.License = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.CasId = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeAccountLicense instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.License);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CasId);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)License) + ProtocolParser.SizeOfUInt64((ulong)CasId) + 2;
		}
	}
}
