using System.IO;

namespace PegasusGame
{
	public class HistoryMeta : IProtoBuf
	{
		public enum Type
		{
			TARGET = 0,
			DAMAGE = 1,
			HEALING = 2,
			JOUST = 3,
			SHOW_BIG_CARD = 5,
			EFFECT_TIMING = 6,
			HISTORY_TARGET = 7,
			OVERRIDE_HISTORY = 8,
			HISTORY_TARGET_DONT_DUPLICATE_UNTIL_END = 9,
			BEGIN_ARTIFICIAL_HISTORY_TILE = 10,
			BEGIN_ARTIFICIAL_HISTORY_TRIGGER_TILE = 11,
			END_ARTIFICIAL_HISTORY_TILE = 12,
			START_DRAW = 13,
			BURNED_CARD = 14,
			EFFECT_SELECTION = 0xF,
			BEGIN_LISTENING_FOR_TURN_EVENTS = 0x10,
			HOLD_DRAWN_CARD = 17,
			CONTROLLER_AND_ZONE_CHANGE = 18,
			ARTIFICIAL_PAUSE = 19,
			SLUSH_TIME = 20,
			ARTIFICIAL_HISTORY_INTERRUPT = 21,
			POISONOUS = 22,
			STUB_20_6_LETTUCE = 23
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is HistoryMeta))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static HistoryMeta Deserialize(Stream stream, HistoryMeta instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static HistoryMeta DeserializeLengthDelimited(Stream stream)
		{
			HistoryMeta historyMeta = new HistoryMeta();
			DeserializeLengthDelimited(stream, historyMeta);
			return historyMeta;
		}

		public static HistoryMeta DeserializeLengthDelimited(Stream stream, HistoryMeta instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static HistoryMeta Deserialize(Stream stream, HistoryMeta instance, long limit)
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

		public static void Serialize(Stream stream, HistoryMeta instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
