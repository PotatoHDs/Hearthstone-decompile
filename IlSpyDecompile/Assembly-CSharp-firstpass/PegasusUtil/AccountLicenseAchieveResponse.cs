using System.IO;

namespace PegasusUtil
{
	public class AccountLicenseAchieveResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 311
		}

		public enum Result
		{
			INVALID_ACHIEVE = 1,
			NOT_ACTIVE,
			IN_PROGRESS,
			COMPLETE,
			STATUS_UNKNOWN
		}

		public int Achieve { get; set; }

		public Result Result_ { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Achieve.GetHashCode() ^ Result_.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			AccountLicenseAchieveResponse accountLicenseAchieveResponse = obj as AccountLicenseAchieveResponse;
			if (accountLicenseAchieveResponse == null)
			{
				return false;
			}
			if (!Achieve.Equals(accountLicenseAchieveResponse.Achieve))
			{
				return false;
			}
			if (!Result_.Equals(accountLicenseAchieveResponse.Result_))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AccountLicenseAchieveResponse Deserialize(Stream stream, AccountLicenseAchieveResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AccountLicenseAchieveResponse DeserializeLengthDelimited(Stream stream)
		{
			AccountLicenseAchieveResponse accountLicenseAchieveResponse = new AccountLicenseAchieveResponse();
			DeserializeLengthDelimited(stream, accountLicenseAchieveResponse);
			return accountLicenseAchieveResponse;
		}

		public static AccountLicenseAchieveResponse DeserializeLengthDelimited(Stream stream, AccountLicenseAchieveResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AccountLicenseAchieveResponse Deserialize(Stream stream, AccountLicenseAchieveResponse instance, long limit)
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
					instance.Achieve = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Result_ = (Result)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AccountLicenseAchieveResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Achieve);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Result_);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Achieve) + ProtocolParser.SizeOfUInt64((ulong)Result_) + 2;
		}
	}
}
