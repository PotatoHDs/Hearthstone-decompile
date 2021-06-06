using System.IO;

namespace PegasusShared
{
	public class DeckCardDbRecord : IProtoBuf
	{
		public int Id { get; set; }

		public int CardId { get; set; }

		public int DeckId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Id.GetHashCode() ^ CardId.GetHashCode() ^ DeckId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			DeckCardDbRecord deckCardDbRecord = obj as DeckCardDbRecord;
			if (deckCardDbRecord == null)
			{
				return false;
			}
			if (!Id.Equals(deckCardDbRecord.Id))
			{
				return false;
			}
			if (!CardId.Equals(deckCardDbRecord.CardId))
			{
				return false;
			}
			if (!DeckId.Equals(deckCardDbRecord.DeckId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeckCardDbRecord Deserialize(Stream stream, DeckCardDbRecord instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckCardDbRecord DeserializeLengthDelimited(Stream stream)
		{
			DeckCardDbRecord deckCardDbRecord = new DeckCardDbRecord();
			DeserializeLengthDelimited(stream, deckCardDbRecord);
			return deckCardDbRecord;
		}

		public static DeckCardDbRecord DeserializeLengthDelimited(Stream stream, DeckCardDbRecord instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckCardDbRecord Deserialize(Stream stream, DeckCardDbRecord instance, long limit)
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
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.CardId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.DeckId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, DeckCardDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CardId);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Id) + ProtocolParser.SizeOfUInt64((ulong)CardId) + ProtocolParser.SizeOfUInt64((ulong)DeckId) + 3;
		}
	}
}
