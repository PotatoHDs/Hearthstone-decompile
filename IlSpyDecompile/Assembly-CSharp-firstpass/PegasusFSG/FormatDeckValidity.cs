using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	public class FormatDeckValidity : IProtoBuf
	{
		public FormatType FormatType { get; set; }

		public bool ValidDeck { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ FormatType.GetHashCode() ^ ValidDeck.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			FormatDeckValidity formatDeckValidity = obj as FormatDeckValidity;
			if (formatDeckValidity == null)
			{
				return false;
			}
			if (!FormatType.Equals(formatDeckValidity.FormatType))
			{
				return false;
			}
			if (!ValidDeck.Equals(formatDeckValidity.ValidDeck))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FormatDeckValidity Deserialize(Stream stream, FormatDeckValidity instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FormatDeckValidity DeserializeLengthDelimited(Stream stream)
		{
			FormatDeckValidity formatDeckValidity = new FormatDeckValidity();
			DeserializeLengthDelimited(stream, formatDeckValidity);
			return formatDeckValidity;
		}

		public static FormatDeckValidity DeserializeLengthDelimited(Stream stream, FormatDeckValidity instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FormatDeckValidity Deserialize(Stream stream, FormatDeckValidity instance, long limit)
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
					instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.ValidDeck = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, FormatDeckValidity instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FormatType);
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.ValidDeck);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)FormatType) + 1 + 2;
		}
	}
}
