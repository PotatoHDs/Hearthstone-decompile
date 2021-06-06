using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class DeleteDeck : IProtoBuf
	{
		public enum PacketID
		{
			ID = 210,
			System = 0
		}

		public long Deck { get; set; }

		public DeckType DeckType { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Deck.GetHashCode() ^ DeckType.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			DeleteDeck deleteDeck = obj as DeleteDeck;
			if (deleteDeck == null)
			{
				return false;
			}
			if (!Deck.Equals(deleteDeck.Deck))
			{
				return false;
			}
			if (!DeckType.Equals(deleteDeck.DeckType))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeleteDeck Deserialize(Stream stream, DeleteDeck instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeleteDeck DeserializeLengthDelimited(Stream stream)
		{
			DeleteDeck deleteDeck = new DeleteDeck();
			DeserializeLengthDelimited(stream, deleteDeck);
			return deleteDeck;
		}

		public static DeleteDeck DeserializeLengthDelimited(Stream stream, DeleteDeck instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeleteDeck Deserialize(Stream stream, DeleteDeck instance, long limit)
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
				case 16:
					instance.DeckType = (DeckType)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, DeleteDeck instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Deck);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckType);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Deck) + ProtocolParser.SizeOfUInt64((ulong)DeckType) + 2;
		}
	}
}
