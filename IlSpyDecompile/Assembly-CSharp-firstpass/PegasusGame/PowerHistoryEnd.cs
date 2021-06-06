using System.IO;

namespace PegasusGame
{
	public class PowerHistoryEnd : IProtoBuf
	{
		public override int GetHashCode()
		{
			return GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is PowerHistoryEnd))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PowerHistoryEnd Deserialize(Stream stream, PowerHistoryEnd instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PowerHistoryEnd DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryEnd powerHistoryEnd = new PowerHistoryEnd();
			DeserializeLengthDelimited(stream, powerHistoryEnd);
			return powerHistoryEnd;
		}

		public static PowerHistoryEnd DeserializeLengthDelimited(Stream stream, PowerHistoryEnd instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PowerHistoryEnd Deserialize(Stream stream, PowerHistoryEnd instance, long limit)
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
				if (num == -1)
				{
					if (limit < 0)
					{
						break;
					}
					throw new EndOfStreamException();
				}
				Key key = ProtocolParser.ReadKey((byte)num, stream);
				if (key.Field == 0)
				{
					throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
				}
				ProtocolParser.SkipKey(stream, key);
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, PowerHistoryEnd instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
