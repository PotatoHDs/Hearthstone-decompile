using System.IO;

namespace PegasusUtil
{
	public class UpdateAccountLicensesResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 331
		}

		public bool FixedLicenseSuccess { get; set; }

		public bool ConsumableLicenseSuccess { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ FixedLicenseSuccess.GetHashCode() ^ ConsumableLicenseSuccess.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			UpdateAccountLicensesResponse updateAccountLicensesResponse = obj as UpdateAccountLicensesResponse;
			if (updateAccountLicensesResponse == null)
			{
				return false;
			}
			if (!FixedLicenseSuccess.Equals(updateAccountLicensesResponse.FixedLicenseSuccess))
			{
				return false;
			}
			if (!ConsumableLicenseSuccess.Equals(updateAccountLicensesResponse.ConsumableLicenseSuccess))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateAccountLicensesResponse Deserialize(Stream stream, UpdateAccountLicensesResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateAccountLicensesResponse DeserializeLengthDelimited(Stream stream)
		{
			UpdateAccountLicensesResponse updateAccountLicensesResponse = new UpdateAccountLicensesResponse();
			DeserializeLengthDelimited(stream, updateAccountLicensesResponse);
			return updateAccountLicensesResponse;
		}

		public static UpdateAccountLicensesResponse DeserializeLengthDelimited(Stream stream, UpdateAccountLicensesResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateAccountLicensesResponse Deserialize(Stream stream, UpdateAccountLicensesResponse instance, long limit)
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
					instance.FixedLicenseSuccess = ProtocolParser.ReadBool(stream);
					continue;
				case 16:
					instance.ConsumableLicenseSuccess = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, UpdateAccountLicensesResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.FixedLicenseSuccess);
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.ConsumableLicenseSuccess);
		}

		public uint GetSerializedSize()
		{
			return 4u;
		}
	}
}
