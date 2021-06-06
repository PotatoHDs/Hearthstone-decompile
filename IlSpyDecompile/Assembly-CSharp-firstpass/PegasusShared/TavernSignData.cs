using System.IO;

namespace PegasusShared
{
	public class TavernSignData : IProtoBuf
	{
		public int Sign { get; set; }

		public int Background { get; set; }

		public int Major { get; set; }

		public int Minor { get; set; }

		public TavernSignType SignType { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Sign.GetHashCode() ^ Background.GetHashCode() ^ Major.GetHashCode() ^ Minor.GetHashCode() ^ SignType.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			TavernSignData tavernSignData = obj as TavernSignData;
			if (tavernSignData == null)
			{
				return false;
			}
			if (!Sign.Equals(tavernSignData.Sign))
			{
				return false;
			}
			if (!Background.Equals(tavernSignData.Background))
			{
				return false;
			}
			if (!Major.Equals(tavernSignData.Major))
			{
				return false;
			}
			if (!Minor.Equals(tavernSignData.Minor))
			{
				return false;
			}
			if (!SignType.Equals(tavernSignData.SignType))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TavernSignData Deserialize(Stream stream, TavernSignData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TavernSignData DeserializeLengthDelimited(Stream stream)
		{
			TavernSignData tavernSignData = new TavernSignData();
			DeserializeLengthDelimited(stream, tavernSignData);
			return tavernSignData;
		}

		public static TavernSignData DeserializeLengthDelimited(Stream stream, TavernSignData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TavernSignData Deserialize(Stream stream, TavernSignData instance, long limit)
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
					instance.Sign = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Background = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Major = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Minor = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.SignType = (TavernSignType)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, TavernSignData instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Sign);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Background);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Major);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Minor);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SignType);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Sign) + ProtocolParser.SizeOfUInt64((ulong)Background) + ProtocolParser.SizeOfUInt64((ulong)Major) + ProtocolParser.SizeOfUInt64((ulong)Minor) + ProtocolParser.SizeOfUInt64((ulong)SignType) + 5;
		}
	}
}
