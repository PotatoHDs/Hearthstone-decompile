using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class CardValues : IProtoBuf
	{
		public enum PacketID
		{
			ID = 260
		}

		private List<CardValue> _Cards = new List<CardValue>();

		public List<CardValue> Cards
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

		public int CardNerfIndex { get; set; }

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (CardValue card in Cards)
			{
				num ^= card.GetHashCode();
			}
			return num ^ CardNerfIndex.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			CardValues cardValues = obj as CardValues;
			if (cardValues == null)
			{
				return false;
			}
			if (Cards.Count != cardValues.Cards.Count)
			{
				return false;
			}
			for (int i = 0; i < Cards.Count; i++)
			{
				if (!Cards[i].Equals(cardValues.Cards[i]))
				{
					return false;
				}
			}
			if (!CardNerfIndex.Equals(cardValues.CardNerfIndex))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CardValues Deserialize(Stream stream, CardValues instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CardValues DeserializeLengthDelimited(Stream stream)
		{
			CardValues cardValues = new CardValues();
			DeserializeLengthDelimited(stream, cardValues);
			return cardValues;
		}

		public static CardValues DeserializeLengthDelimited(Stream stream, CardValues instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CardValues Deserialize(Stream stream, CardValues instance, long limit)
		{
			if (instance.Cards == null)
			{
				instance.Cards = new List<CardValue>();
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
				case 10:
					instance.Cards.Add(CardValue.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.CardNerfIndex = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, CardValues instance)
		{
			if (instance.Cards.Count > 0)
			{
				foreach (CardValue card in instance.Cards)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, card.GetSerializedSize());
					CardValue.Serialize(stream, card);
				}
			}
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CardNerfIndex);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Cards.Count > 0)
			{
				foreach (CardValue card in Cards)
				{
					num++;
					uint serializedSize = card.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += ProtocolParser.SizeOfUInt64((ulong)CardNerfIndex);
			return num + 1;
		}
	}
}
