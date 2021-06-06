using System.IO;

namespace PegasusUtil
{
	public class DeckDeleted : IProtoBuf
	{
		public enum PacketID
		{
			ID = 218
		}

		public long Deck { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Deck.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			DeckDeleted deckDeleted = obj as DeckDeleted;
			if (deckDeleted == null)
			{
				return false;
			}
			if (!Deck.Equals(deckDeleted.Deck))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeckDeleted Deserialize(Stream stream, DeckDeleted instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckDeleted DeserializeLengthDelimited(Stream stream)
		{
			DeckDeleted deckDeleted = new DeckDeleted();
			DeserializeLengthDelimited(stream, deckDeleted);
			return deckDeleted;
		}

		public static DeckDeleted DeserializeLengthDelimited(Stream stream, DeckDeleted instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckDeleted Deserialize(Stream stream, DeckDeleted instance, long limit)
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
					instance.Deck = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, DeckDeleted instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Deck);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Deck) + 1;
		}
	}
}
