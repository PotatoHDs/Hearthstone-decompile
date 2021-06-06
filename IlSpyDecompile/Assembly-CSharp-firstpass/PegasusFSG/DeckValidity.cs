using System.Collections.Generic;
using System.IO;

namespace PegasusFSG
{
	public class DeckValidity : IProtoBuf
	{
		public enum PacketID
		{
			ID = 513
		}

		private List<BrawlDeckValidity> _ValidTavernBrawlDeck = new List<BrawlDeckValidity>();

		private List<BrawlDeckValidity> _ValidFiresideBrawlDeck = new List<BrawlDeckValidity>();

		private List<FormatDeckValidity> _ValidFormatDecks = new List<FormatDeckValidity>();

		public bool ValidStandardDeckDeprecated { get; set; }

		public bool ValidWildDeckDeprecated { get; set; }

		public List<BrawlDeckValidity> ValidTavernBrawlDeck
		{
			get
			{
				return _ValidTavernBrawlDeck;
			}
			set
			{
				_ValidTavernBrawlDeck = value;
			}
		}

		public List<BrawlDeckValidity> ValidFiresideBrawlDeck
		{
			get
			{
				return _ValidFiresideBrawlDeck;
			}
			set
			{
				_ValidFiresideBrawlDeck = value;
			}
		}

		public List<FormatDeckValidity> ValidFormatDecks
		{
			get
			{
				return _ValidFormatDecks;
			}
			set
			{
				_ValidFormatDecks = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ValidStandardDeckDeprecated.GetHashCode();
			hashCode ^= ValidWildDeckDeprecated.GetHashCode();
			foreach (BrawlDeckValidity item in ValidTavernBrawlDeck)
			{
				hashCode ^= item.GetHashCode();
			}
			foreach (BrawlDeckValidity item2 in ValidFiresideBrawlDeck)
			{
				hashCode ^= item2.GetHashCode();
			}
			foreach (FormatDeckValidity validFormatDeck in ValidFormatDecks)
			{
				hashCode ^= validFormatDeck.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DeckValidity deckValidity = obj as DeckValidity;
			if (deckValidity == null)
			{
				return false;
			}
			if (!ValidStandardDeckDeprecated.Equals(deckValidity.ValidStandardDeckDeprecated))
			{
				return false;
			}
			if (!ValidWildDeckDeprecated.Equals(deckValidity.ValidWildDeckDeprecated))
			{
				return false;
			}
			if (ValidTavernBrawlDeck.Count != deckValidity.ValidTavernBrawlDeck.Count)
			{
				return false;
			}
			for (int i = 0; i < ValidTavernBrawlDeck.Count; i++)
			{
				if (!ValidTavernBrawlDeck[i].Equals(deckValidity.ValidTavernBrawlDeck[i]))
				{
					return false;
				}
			}
			if (ValidFiresideBrawlDeck.Count != deckValidity.ValidFiresideBrawlDeck.Count)
			{
				return false;
			}
			for (int j = 0; j < ValidFiresideBrawlDeck.Count; j++)
			{
				if (!ValidFiresideBrawlDeck[j].Equals(deckValidity.ValidFiresideBrawlDeck[j]))
				{
					return false;
				}
			}
			if (ValidFormatDecks.Count != deckValidity.ValidFormatDecks.Count)
			{
				return false;
			}
			for (int k = 0; k < ValidFormatDecks.Count; k++)
			{
				if (!ValidFormatDecks[k].Equals(deckValidity.ValidFormatDecks[k]))
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

		public static DeckValidity Deserialize(Stream stream, DeckValidity instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckValidity DeserializeLengthDelimited(Stream stream)
		{
			DeckValidity deckValidity = new DeckValidity();
			DeserializeLengthDelimited(stream, deckValidity);
			return deckValidity;
		}

		public static DeckValidity DeserializeLengthDelimited(Stream stream, DeckValidity instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckValidity Deserialize(Stream stream, DeckValidity instance, long limit)
		{
			if (instance.ValidTavernBrawlDeck == null)
			{
				instance.ValidTavernBrawlDeck = new List<BrawlDeckValidity>();
			}
			if (instance.ValidFiresideBrawlDeck == null)
			{
				instance.ValidFiresideBrawlDeck = new List<BrawlDeckValidity>();
			}
			if (instance.ValidFormatDecks == null)
			{
				instance.ValidFormatDecks = new List<FormatDeckValidity>();
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
					instance.ValidStandardDeckDeprecated = ProtocolParser.ReadBool(stream);
					continue;
				case 16:
					instance.ValidWildDeckDeprecated = ProtocolParser.ReadBool(stream);
					continue;
				case 26:
					instance.ValidTavernBrawlDeck.Add(BrawlDeckValidity.DeserializeLengthDelimited(stream));
					continue;
				case 34:
					instance.ValidFiresideBrawlDeck.Add(BrawlDeckValidity.DeserializeLengthDelimited(stream));
					continue;
				case 42:
					instance.ValidFormatDecks.Add(FormatDeckValidity.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, DeckValidity instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.ValidStandardDeckDeprecated);
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.ValidWildDeckDeprecated);
			if (instance.ValidTavernBrawlDeck.Count > 0)
			{
				foreach (BrawlDeckValidity item in instance.ValidTavernBrawlDeck)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					BrawlDeckValidity.Serialize(stream, item);
				}
			}
			if (instance.ValidFiresideBrawlDeck.Count > 0)
			{
				foreach (BrawlDeckValidity item2 in instance.ValidFiresideBrawlDeck)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
					BrawlDeckValidity.Serialize(stream, item2);
				}
			}
			if (instance.ValidFormatDecks.Count <= 0)
			{
				return;
			}
			foreach (FormatDeckValidity validFormatDeck in instance.ValidFormatDecks)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, validFormatDeck.GetSerializedSize());
				FormatDeckValidity.Serialize(stream, validFormatDeck);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num++;
			num++;
			if (ValidTavernBrawlDeck.Count > 0)
			{
				foreach (BrawlDeckValidity item in ValidTavernBrawlDeck)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (ValidFiresideBrawlDeck.Count > 0)
			{
				foreach (BrawlDeckValidity item2 in ValidFiresideBrawlDeck)
				{
					num++;
					uint serializedSize2 = item2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (ValidFormatDecks.Count > 0)
			{
				foreach (FormatDeckValidity validFormatDeck in ValidFormatDecks)
				{
					num++;
					uint serializedSize3 = validFormatDeck.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num + 2;
		}
	}
}
