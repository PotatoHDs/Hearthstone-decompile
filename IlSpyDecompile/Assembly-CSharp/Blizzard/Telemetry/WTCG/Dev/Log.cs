using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Dev
{
	public class Log : IProtoBuf
	{
		public bool HasCategory;

		private string _Category;

		public bool HasDetails;

		private string _Details;

		public string Category
		{
			get
			{
				return _Category;
			}
			set
			{
				_Category = value;
				HasCategory = value != null;
			}
		}

		public string Details
		{
			get
			{
				return _Details;
			}
			set
			{
				_Details = value;
				HasDetails = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasCategory)
			{
				num ^= Category.GetHashCode();
			}
			if (HasDetails)
			{
				num ^= Details.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Log log = obj as Log;
			if (log == null)
			{
				return false;
			}
			if (HasCategory != log.HasCategory || (HasCategory && !Category.Equals(log.Category)))
			{
				return false;
			}
			if (HasDetails != log.HasDetails || (HasDetails && !Details.Equals(log.Details)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Log Deserialize(Stream stream, Log instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Log DeserializeLengthDelimited(Stream stream)
		{
			Log log = new Log();
			DeserializeLengthDelimited(stream, log);
			return log;
		}

		public static Log DeserializeLengthDelimited(Stream stream, Log instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Log Deserialize(Stream stream, Log instance, long limit)
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
					instance.Category = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.Details = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, Log instance)
		{
			if (instance.HasCategory)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Category));
			}
			if (instance.HasDetails)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Details));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasCategory)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Category);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasDetails)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Details);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
