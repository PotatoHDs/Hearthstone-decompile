using System.IO;

namespace PegasusUtil
{
	public class ValidateAchieve : IProtoBuf
	{
		public enum PacketID
		{
			ID = 284,
			System = 0
		}

		public int Achieve { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Achieve.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ValidateAchieve validateAchieve = obj as ValidateAchieve;
			if (validateAchieve == null)
			{
				return false;
			}
			if (!Achieve.Equals(validateAchieve.Achieve))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ValidateAchieve Deserialize(Stream stream, ValidateAchieve instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ValidateAchieve DeserializeLengthDelimited(Stream stream)
		{
			ValidateAchieve validateAchieve = new ValidateAchieve();
			DeserializeLengthDelimited(stream, validateAchieve);
			return validateAchieve;
		}

		public static ValidateAchieve DeserializeLengthDelimited(Stream stream, ValidateAchieve instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ValidateAchieve Deserialize(Stream stream, ValidateAchieve instance, long limit)
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

		public static void Serialize(Stream stream, ValidateAchieve instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Achieve);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Achieve) + 1;
		}
	}
}
