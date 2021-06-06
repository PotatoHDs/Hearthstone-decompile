using System.IO;

namespace PegasusShared
{
	public class SmartDeckCardData : IProtoBuf
	{
		public int Asset { get; set; }

		public int QtyNormal { get; set; }

		public int QtyGolden { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Asset.GetHashCode() ^ QtyNormal.GetHashCode() ^ QtyGolden.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SmartDeckCardData smartDeckCardData = obj as SmartDeckCardData;
			if (smartDeckCardData == null)
			{
				return false;
			}
			if (!Asset.Equals(smartDeckCardData.Asset))
			{
				return false;
			}
			if (!QtyNormal.Equals(smartDeckCardData.QtyNormal))
			{
				return false;
			}
			if (!QtyGolden.Equals(smartDeckCardData.QtyGolden))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SmartDeckCardData Deserialize(Stream stream, SmartDeckCardData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SmartDeckCardData DeserializeLengthDelimited(Stream stream)
		{
			SmartDeckCardData smartDeckCardData = new SmartDeckCardData();
			DeserializeLengthDelimited(stream, smartDeckCardData);
			return smartDeckCardData;
		}

		public static SmartDeckCardData DeserializeLengthDelimited(Stream stream, SmartDeckCardData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SmartDeckCardData Deserialize(Stream stream, SmartDeckCardData instance, long limit)
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
					instance.QtyNormal = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.QtyGolden = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SmartDeckCardData instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Asset);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.QtyNormal);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.QtyGolden);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Asset) + ProtocolParser.SizeOfUInt64((ulong)QtyNormal) + ProtocolParser.SizeOfUInt64((ulong)QtyGolden) + 3;
		}
	}
}
