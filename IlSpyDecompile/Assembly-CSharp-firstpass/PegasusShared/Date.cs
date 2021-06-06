using System.IO;

namespace PegasusShared
{
	public class Date : IProtoBuf
	{
		public int Year { get; set; }

		public int Month { get; set; }

		public int Day { get; set; }

		public int Hours { get; set; }

		public int Min { get; set; }

		public int Sec { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Year.GetHashCode() ^ Month.GetHashCode() ^ Day.GetHashCode() ^ Hours.GetHashCode() ^ Min.GetHashCode() ^ Sec.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Date date = obj as Date;
			if (date == null)
			{
				return false;
			}
			if (!Year.Equals(date.Year))
			{
				return false;
			}
			if (!Month.Equals(date.Month))
			{
				return false;
			}
			if (!Day.Equals(date.Day))
			{
				return false;
			}
			if (!Hours.Equals(date.Hours))
			{
				return false;
			}
			if (!Min.Equals(date.Min))
			{
				return false;
			}
			if (!Sec.Equals(date.Sec))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Date Deserialize(Stream stream, Date instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Date DeserializeLengthDelimited(Stream stream)
		{
			Date date = new Date();
			DeserializeLengthDelimited(stream, date);
			return date;
		}

		public static Date DeserializeLengthDelimited(Stream stream, Date instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Date Deserialize(Stream stream, Date instance, long limit)
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
					instance.Year = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Month = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Day = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Hours = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.Min = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.Sec = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, Date instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Year);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Month);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Day);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Hours);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Min);
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Sec);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Year) + ProtocolParser.SizeOfUInt64((ulong)Month) + ProtocolParser.SizeOfUInt64((ulong)Day) + ProtocolParser.SizeOfUInt64((ulong)Hours) + ProtocolParser.SizeOfUInt64((ulong)Min) + ProtocolParser.SizeOfUInt64((ulong)Sec) + 6;
		}
	}
}
