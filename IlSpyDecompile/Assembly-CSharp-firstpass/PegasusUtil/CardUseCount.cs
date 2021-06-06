using System.IO;

namespace PegasusUtil
{
	public class CardUseCount : IProtoBuf
	{
		public int Asset { get; set; }

		public int Count { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Asset.GetHashCode() ^ Count.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			CardUseCount cardUseCount = obj as CardUseCount;
			if (cardUseCount == null)
			{
				return false;
			}
			if (!Asset.Equals(cardUseCount.Asset))
			{
				return false;
			}
			if (!Count.Equals(cardUseCount.Count))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CardUseCount Deserialize(Stream stream, CardUseCount instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CardUseCount DeserializeLengthDelimited(Stream stream)
		{
			CardUseCount cardUseCount = new CardUseCount();
			DeserializeLengthDelimited(stream, cardUseCount);
			return cardUseCount;
		}

		public static CardUseCount DeserializeLengthDelimited(Stream stream, CardUseCount instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CardUseCount Deserialize(Stream stream, CardUseCount instance, long limit)
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
					instance.Asset = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Count = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, CardUseCount instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Asset);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Count);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Asset) + ProtocolParser.SizeOfUInt64((ulong)Count) + 2;
		}
	}
}
