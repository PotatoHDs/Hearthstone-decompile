using System.IO;
using System.Text;

namespace PegasusShared
{
	public class DeckRulesetViolation : IProtoBuf
	{
		public bool HasCard;

		private CardDef _Card;

		public bool HasCount;

		private int _Count;

		public bool HasDeckRuleDesc;

		private string _DeckRuleDesc;

		public CardDef Card
		{
			get
			{
				return _Card;
			}
			set
			{
				_Card = value;
				HasCard = value != null;
			}
		}

		public int Count
		{
			get
			{
				return _Count;
			}
			set
			{
				_Count = value;
				HasCount = true;
			}
		}

		public int DeckRuleId { get; set; }

		public string DeckRuleDesc
		{
			get
			{
				return _DeckRuleDesc;
			}
			set
			{
				_DeckRuleDesc = value;
				HasDeckRuleDesc = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasCard)
			{
				num ^= Card.GetHashCode();
			}
			if (HasCount)
			{
				num ^= Count.GetHashCode();
			}
			num ^= DeckRuleId.GetHashCode();
			if (HasDeckRuleDesc)
			{
				num ^= DeckRuleDesc.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DeckRulesetViolation deckRulesetViolation = obj as DeckRulesetViolation;
			if (deckRulesetViolation == null)
			{
				return false;
			}
			if (HasCard != deckRulesetViolation.HasCard || (HasCard && !Card.Equals(deckRulesetViolation.Card)))
			{
				return false;
			}
			if (HasCount != deckRulesetViolation.HasCount || (HasCount && !Count.Equals(deckRulesetViolation.Count)))
			{
				return false;
			}
			if (!DeckRuleId.Equals(deckRulesetViolation.DeckRuleId))
			{
				return false;
			}
			if (HasDeckRuleDesc != deckRulesetViolation.HasDeckRuleDesc || (HasDeckRuleDesc && !DeckRuleDesc.Equals(deckRulesetViolation.DeckRuleDesc)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeckRulesetViolation Deserialize(Stream stream, DeckRulesetViolation instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckRulesetViolation DeserializeLengthDelimited(Stream stream)
		{
			DeckRulesetViolation deckRulesetViolation = new DeckRulesetViolation();
			DeserializeLengthDelimited(stream, deckRulesetViolation);
			return deckRulesetViolation;
		}

		public static DeckRulesetViolation DeserializeLengthDelimited(Stream stream, DeckRulesetViolation instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckRulesetViolation Deserialize(Stream stream, DeckRulesetViolation instance, long limit)
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
					if (instance.Card == null)
					{
						instance.Card = CardDef.DeserializeLengthDelimited(stream);
					}
					else
					{
						CardDef.DeserializeLengthDelimited(stream, instance.Card);
					}
					continue;
				case 16:
					instance.Count = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 100u:
						if (key.WireType == Wire.Varint)
						{
							instance.DeckRuleId = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 101u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeckRuleDesc = ProtocolParser.ReadString(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
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

		public static void Serialize(Stream stream, DeckRulesetViolation instance)
		{
			if (instance.HasCard)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Card.GetSerializedSize());
				CardDef.Serialize(stream, instance.Card);
			}
			if (instance.HasCount)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Count);
			}
			stream.WriteByte(160);
			stream.WriteByte(6);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckRuleId);
			if (instance.HasDeckRuleDesc)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeckRuleDesc));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasCard)
			{
				num++;
				uint serializedSize = Card.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Count);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)DeckRuleId);
			if (HasDeckRuleDesc)
			{
				num += 2;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(DeckRuleDesc);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 2;
		}
	}
}
