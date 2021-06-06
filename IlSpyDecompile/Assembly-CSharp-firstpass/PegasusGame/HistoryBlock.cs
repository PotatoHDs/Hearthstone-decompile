using System.IO;

namespace PegasusGame
{
	public class HistoryBlock : IProtoBuf
	{
		public enum Type
		{
			INVALID = 0,
			ATTACK = 1,
			JOUST = 2,
			POWER = 3,
			TRIGGER = 5,
			DEATHS = 6,
			PLAY = 7,
			FATIGUE = 8,
			RITUAL = 9,
			REVEAL_CARD = 10,
			GAME_RESET = 11,
			MOVE_MINION = 12
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is HistoryBlock))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static HistoryBlock Deserialize(Stream stream, HistoryBlock instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static HistoryBlock DeserializeLengthDelimited(Stream stream)
		{
			HistoryBlock historyBlock = new HistoryBlock();
			DeserializeLengthDelimited(stream, historyBlock);
			return historyBlock;
		}

		public static HistoryBlock DeserializeLengthDelimited(Stream stream, HistoryBlock instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static HistoryBlock Deserialize(Stream stream, HistoryBlock instance, long limit)
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

		public static void Serialize(Stream stream, HistoryBlock instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
