using System;
using System.IO;

namespace PegasusShared
{
	public class DeckCardData : IProtoBuf
	{
		public bool HasQty;

		private int _Qty;

		public CardDef Def { get; set; }

		public int Qty
		{
			get
			{
				return _Qty;
			}
			set
			{
				_Qty = value;
				HasQty = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Def.GetHashCode();
			if (HasQty)
			{
				hashCode ^= Qty.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DeckCardData deckCardData = obj as DeckCardData;
			if (deckCardData == null)
			{
				return false;
			}
			if (!Def.Equals(deckCardData.Def))
			{
				return false;
			}
			if (HasQty != deckCardData.HasQty || (HasQty && !Qty.Equals(deckCardData.Qty)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeckCardData Deserialize(Stream stream, DeckCardData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckCardData DeserializeLengthDelimited(Stream stream)
		{
			DeckCardData deckCardData = new DeckCardData();
			DeserializeLengthDelimited(stream, deckCardData);
			return deckCardData;
		}

		public static DeckCardData DeserializeLengthDelimited(Stream stream, DeckCardData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckCardData Deserialize(Stream stream, DeckCardData instance, long limit)
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
				case 10:
					if (instance.Def == null)
					{
						instance.Def = CardDef.DeserializeLengthDelimited(stream);
					}
					else
					{
						CardDef.DeserializeLengthDelimited(stream, instance.Def);
					}
					continue;
				case 24:
					instance.Qty = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, DeckCardData instance)
		{
			if (instance.Def == null)
			{
				throw new ArgumentNullException("Def", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Def.GetSerializedSize());
			CardDef.Serialize(stream, instance.Def);
			if (instance.HasQty)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Qty);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Def.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasQty)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Qty);
			}
			return num + 1;
		}
	}
}
