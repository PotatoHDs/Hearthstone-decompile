using System;
using System.IO;

namespace PegasusShared
{
	public class CardStack : IProtoBuf
	{
		public CardDef CardDef { get; set; }

		public Date LatestInsertDate { get; set; }

		public int Count { get; set; }

		public int NumSeen { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ CardDef.GetHashCode() ^ LatestInsertDate.GetHashCode() ^ Count.GetHashCode() ^ NumSeen.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			CardStack cardStack = obj as CardStack;
			if (cardStack == null)
			{
				return false;
			}
			if (!CardDef.Equals(cardStack.CardDef))
			{
				return false;
			}
			if (!LatestInsertDate.Equals(cardStack.LatestInsertDate))
			{
				return false;
			}
			if (!Count.Equals(cardStack.Count))
			{
				return false;
			}
			if (!NumSeen.Equals(cardStack.NumSeen))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CardStack Deserialize(Stream stream, CardStack instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CardStack DeserializeLengthDelimited(Stream stream)
		{
			CardStack cardStack = new CardStack();
			DeserializeLengthDelimited(stream, cardStack);
			return cardStack;
		}

		public static CardStack DeserializeLengthDelimited(Stream stream, CardStack instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CardStack Deserialize(Stream stream, CardStack instance, long limit)
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
					if (instance.CardDef == null)
					{
						instance.CardDef = CardDef.DeserializeLengthDelimited(stream);
					}
					else
					{
						CardDef.DeserializeLengthDelimited(stream, instance.CardDef);
					}
					continue;
				case 18:
					if (instance.LatestInsertDate == null)
					{
						instance.LatestInsertDate = Date.DeserializeLengthDelimited(stream);
					}
					else
					{
						Date.DeserializeLengthDelimited(stream, instance.LatestInsertDate);
					}
					continue;
				case 24:
					instance.Count = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.NumSeen = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, CardStack instance)
		{
			if (instance.CardDef == null)
			{
				throw new ArgumentNullException("CardDef", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.CardDef.GetSerializedSize());
			CardDef.Serialize(stream, instance.CardDef);
			if (instance.LatestInsertDate == null)
			{
				throw new ArgumentNullException("LatestInsertDate", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.LatestInsertDate.GetSerializedSize());
			Date.Serialize(stream, instance.LatestInsertDate);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Count);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.NumSeen);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = CardDef.GetSerializedSize();
			uint num = 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = LatestInsertDate.GetSerializedSize();
			return num + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + ProtocolParser.SizeOfUInt64((ulong)Count) + ProtocolParser.SizeOfUInt64((ulong)NumSeen) + 4;
		}
	}
}
