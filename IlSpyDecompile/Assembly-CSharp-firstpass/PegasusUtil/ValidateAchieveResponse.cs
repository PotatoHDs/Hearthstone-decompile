using System.IO;

namespace PegasusUtil
{
	public class ValidateAchieveResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 285
		}

		public bool HasSuccess;

		private bool _Success;

		public int Achieve { get; set; }

		public bool Success
		{
			get
			{
				return _Success;
			}
			set
			{
				_Success = value;
				HasSuccess = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Achieve.GetHashCode();
			if (HasSuccess)
			{
				hashCode ^= Success.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ValidateAchieveResponse validateAchieveResponse = obj as ValidateAchieveResponse;
			if (validateAchieveResponse == null)
			{
				return false;
			}
			if (!Achieve.Equals(validateAchieveResponse.Achieve))
			{
				return false;
			}
			if (HasSuccess != validateAchieveResponse.HasSuccess || (HasSuccess && !Success.Equals(validateAchieveResponse.Success)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ValidateAchieveResponse Deserialize(Stream stream, ValidateAchieveResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ValidateAchieveResponse DeserializeLengthDelimited(Stream stream)
		{
			ValidateAchieveResponse validateAchieveResponse = new ValidateAchieveResponse();
			DeserializeLengthDelimited(stream, validateAchieveResponse);
			return validateAchieveResponse;
		}

		public static ValidateAchieveResponse DeserializeLengthDelimited(Stream stream, ValidateAchieveResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ValidateAchieveResponse Deserialize(Stream stream, ValidateAchieveResponse instance, long limit)
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
					instance.Success = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, ValidateAchieveResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Achieve);
			if (instance.HasSuccess)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Success);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Achieve);
			if (HasSuccess)
			{
				num++;
				num++;
			}
			return num + 1;
		}
	}
}
