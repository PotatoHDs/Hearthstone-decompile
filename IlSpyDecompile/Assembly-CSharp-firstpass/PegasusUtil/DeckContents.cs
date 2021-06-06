using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class DeckContents : IProtoBuf
	{
		private List<DeckCardData> _Cards = new List<DeckCardData>();

		public bool Success { get; set; }

		public long DeckId { get; set; }

		public List<DeckCardData> Cards
		{
			get
			{
				return _Cards;
			}
			set
			{
				_Cards = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Success.GetHashCode();
			hashCode ^= DeckId.GetHashCode();
			foreach (DeckCardData card in Cards)
			{
				hashCode ^= card.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DeckContents deckContents = obj as DeckContents;
			if (deckContents == null)
			{
				return false;
			}
			if (!Success.Equals(deckContents.Success))
			{
				return false;
			}
			if (!DeckId.Equals(deckContents.DeckId))
			{
				return false;
			}
			if (Cards.Count != deckContents.Cards.Count)
			{
				return false;
			}
			for (int i = 0; i < Cards.Count; i++)
			{
				if (!Cards[i].Equals(deckContents.Cards[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeckContents Deserialize(Stream stream, DeckContents instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckContents DeserializeLengthDelimited(Stream stream)
		{
			DeckContents deckContents = new DeckContents();
			DeserializeLengthDelimited(stream, deckContents);
			return deckContents;
		}

		public static DeckContents DeserializeLengthDelimited(Stream stream, DeckContents instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckContents Deserialize(Stream stream, DeckContents instance, long limit)
		{
			if (instance.Cards == null)
			{
				instance.Cards = new List<DeckCardData>();
			}
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
					instance.Success = ProtocolParser.ReadBool(stream);
					continue;
				case 16:
					instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.Cards.Add(DeckCardData.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, DeckContents instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.Success);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			if (instance.Cards.Count <= 0)
			{
				return;
			}
			foreach (DeckCardData card in instance.Cards)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, card.GetSerializedSize());
				DeckCardData.Serialize(stream, card);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num++;
			num += ProtocolParser.SizeOfUInt64((ulong)DeckId);
			if (Cards.Count > 0)
			{
				foreach (DeckCardData card in Cards)
				{
					num++;
					uint serializedSize = card.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 2;
		}
	}
}
