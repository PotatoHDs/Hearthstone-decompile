using System.IO;

namespace PegasusShared
{
	public class AccountLicenseInfo : IProtoBuf
	{
		public enum Flags
		{
			OWNED = 1
		}

		public long License { get; set; }

		public ulong Flags_ { get; set; }

		public long CasId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ License.GetHashCode() ^ Flags_.GetHashCode() ^ CasId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			AccountLicenseInfo accountLicenseInfo = obj as AccountLicenseInfo;
			if (accountLicenseInfo == null)
			{
				return false;
			}
			if (!License.Equals(accountLicenseInfo.License))
			{
				return false;
			}
			if (!Flags_.Equals(accountLicenseInfo.Flags_))
			{
				return false;
			}
			if (!CasId.Equals(accountLicenseInfo.CasId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AccountLicenseInfo Deserialize(Stream stream, AccountLicenseInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AccountLicenseInfo DeserializeLengthDelimited(Stream stream)
		{
			AccountLicenseInfo accountLicenseInfo = new AccountLicenseInfo();
			DeserializeLengthDelimited(stream, accountLicenseInfo);
			return accountLicenseInfo;
		}

		public static AccountLicenseInfo DeserializeLengthDelimited(Stream stream, AccountLicenseInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AccountLicenseInfo Deserialize(Stream stream, AccountLicenseInfo instance, long limit)
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
					instance.Flags_ = ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
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

		public static void Serialize(Stream stream, AccountLicenseInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.License);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.Flags_);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CasId);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)License) + ProtocolParser.SizeOfUInt64(Flags_) + ProtocolParser.SizeOfUInt64((ulong)CasId) + 3;
		}
	}
}
