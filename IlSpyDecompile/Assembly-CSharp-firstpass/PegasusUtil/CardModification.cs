using System.IO;

namespace PegasusUtil
{
	public class CardModification : IProtoBuf
	{
		public int AssetCardId { get; set; }

		public int Premium { get; set; }

		public int Quantity { get; set; }

		public int AmountSeen { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ AssetCardId.GetHashCode() ^ Premium.GetHashCode() ^ Quantity.GetHashCode() ^ AmountSeen.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			CardModification cardModification = obj as CardModification;
			if (cardModification == null)
			{
				return false;
			}
			if (!AssetCardId.Equals(cardModification.AssetCardId))
			{
				return false;
			}
			if (!Premium.Equals(cardModification.Premium))
			{
				return false;
			}
			if (!Quantity.Equals(cardModification.Quantity))
			{
				return false;
			}
			if (!AmountSeen.Equals(cardModification.AmountSeen))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CardModification Deserialize(Stream stream, CardModification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CardModification DeserializeLengthDelimited(Stream stream)
		{
			CardModification cardModification = new CardModification();
			DeserializeLengthDelimited(stream, cardModification);
			return cardModification;
		}

		public static CardModification DeserializeLengthDelimited(Stream stream, CardModification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CardModification Deserialize(Stream stream, CardModification instance, long limit)
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
					instance.AssetCardId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Premium = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.AmountSeen = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, CardModification instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.AssetCardId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Premium);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Quantity);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.AmountSeen);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)AssetCardId) + ProtocolParser.SizeOfUInt64((ulong)Premium) + ProtocolParser.SizeOfUInt64((ulong)Quantity) + ProtocolParser.SizeOfUInt64((ulong)AmountSeen) + 4;
		}
	}
}
