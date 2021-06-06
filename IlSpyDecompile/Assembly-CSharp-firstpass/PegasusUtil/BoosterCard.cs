using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class BoosterCard : IProtoBuf
	{
		public CardDef CardDef { get; set; }

		public Date InsertDate { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ CardDef.GetHashCode() ^ InsertDate.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			BoosterCard boosterCard = obj as BoosterCard;
			if (boosterCard == null)
			{
				return false;
			}
			if (!CardDef.Equals(boosterCard.CardDef))
			{
				return false;
			}
			if (!InsertDate.Equals(boosterCard.InsertDate))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BoosterCard Deserialize(Stream stream, BoosterCard instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BoosterCard DeserializeLengthDelimited(Stream stream)
		{
			BoosterCard boosterCard = new BoosterCard();
			DeserializeLengthDelimited(stream, boosterCard);
			return boosterCard;
		}

		public static BoosterCard DeserializeLengthDelimited(Stream stream, BoosterCard instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BoosterCard Deserialize(Stream stream, BoosterCard instance, long limit)
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
					if (instance.InsertDate == null)
					{
						instance.InsertDate = Date.DeserializeLengthDelimited(stream);
					}
					else
					{
						Date.DeserializeLengthDelimited(stream, instance.InsertDate);
					}
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

		public static void Serialize(Stream stream, BoosterCard instance)
		{
			if (instance.CardDef == null)
			{
				throw new ArgumentNullException("CardDef", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.CardDef.GetSerializedSize());
			CardDef.Serialize(stream, instance.CardDef);
			if (instance.InsertDate == null)
			{
				throw new ArgumentNullException("InsertDate", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.InsertDate.GetSerializedSize());
			Date.Serialize(stream, instance.InsertDate);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = CardDef.GetSerializedSize();
			uint num = 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = InsertDate.GetSerializedSize();
			return num + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 2;
		}
	}
}
