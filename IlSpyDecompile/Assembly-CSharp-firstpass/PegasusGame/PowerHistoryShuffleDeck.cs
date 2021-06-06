using System.IO;

namespace PegasusGame
{
	public class PowerHistoryShuffleDeck : IProtoBuf
	{
		public int PlayerId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ PlayerId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			PowerHistoryShuffleDeck powerHistoryShuffleDeck = obj as PowerHistoryShuffleDeck;
			if (powerHistoryShuffleDeck == null)
			{
				return false;
			}
			if (!PlayerId.Equals(powerHistoryShuffleDeck.PlayerId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PowerHistoryShuffleDeck Deserialize(Stream stream, PowerHistoryShuffleDeck instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PowerHistoryShuffleDeck DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryShuffleDeck powerHistoryShuffleDeck = new PowerHistoryShuffleDeck();
			DeserializeLengthDelimited(stream, powerHistoryShuffleDeck);
			return powerHistoryShuffleDeck;
		}

		public static PowerHistoryShuffleDeck DeserializeLengthDelimited(Stream stream, PowerHistoryShuffleDeck instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PowerHistoryShuffleDeck Deserialize(Stream stream, PowerHistoryShuffleDeck instance, long limit)
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
					instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, PowerHistoryShuffleDeck instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)PlayerId) + 1;
		}
	}
}
