using System.IO;

namespace PegasusGame
{
	public class GetGameRoundHistory : IProtoBuf
	{
		public enum PacketID
		{
			ID = 0x20
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is GetGameRoundHistory))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetGameRoundHistory Deserialize(Stream stream, GetGameRoundHistory instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetGameRoundHistory DeserializeLengthDelimited(Stream stream)
		{
			GetGameRoundHistory getGameRoundHistory = new GetGameRoundHistory();
			DeserializeLengthDelimited(stream, getGameRoundHistory);
			return getGameRoundHistory;
		}

		public static GetGameRoundHistory DeserializeLengthDelimited(Stream stream, GetGameRoundHistory instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetGameRoundHistory Deserialize(Stream stream, GetGameRoundHistory instance, long limit)
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

		public static void Serialize(Stream stream, GetGameRoundHistory instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
