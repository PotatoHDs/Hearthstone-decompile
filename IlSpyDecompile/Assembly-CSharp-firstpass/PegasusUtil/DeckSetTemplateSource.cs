using System.IO;

namespace PegasusUtil
{
	public class DeckSetTemplateSource : IProtoBuf
	{
		public enum PacketID
		{
			ID = 332,
			System = 0
		}

		public long Deck { get; set; }

		public int TemplateId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Deck.GetHashCode() ^ TemplateId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			DeckSetTemplateSource deckSetTemplateSource = obj as DeckSetTemplateSource;
			if (deckSetTemplateSource == null)
			{
				return false;
			}
			if (!Deck.Equals(deckSetTemplateSource.Deck))
			{
				return false;
			}
			if (!TemplateId.Equals(deckSetTemplateSource.TemplateId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeckSetTemplateSource Deserialize(Stream stream, DeckSetTemplateSource instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckSetTemplateSource DeserializeLengthDelimited(Stream stream)
		{
			DeckSetTemplateSource deckSetTemplateSource = new DeckSetTemplateSource();
			DeserializeLengthDelimited(stream, deckSetTemplateSource);
			return deckSetTemplateSource;
		}

		public static DeckSetTemplateSource DeserializeLengthDelimited(Stream stream, DeckSetTemplateSource instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckSetTemplateSource Deserialize(Stream stream, DeckSetTemplateSource instance, long limit)
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
					instance.TemplateId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, DeckSetTemplateSource instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Deck);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.TemplateId);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Deck) + ProtocolParser.SizeOfUInt64((ulong)TemplateId) + 2;
		}
	}
}
