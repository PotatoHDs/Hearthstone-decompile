using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class ProductAttribute : IProtoBuf
	{
		public bool HasName;

		private string _Name;

		public bool HasValue;

		private string _Value;

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
				HasName = value != null;
			}
		}

		public string Value
		{
			get
			{
				return _Value;
			}
			set
			{
				_Value = value;
				HasValue = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasName)
			{
				num ^= Name.GetHashCode();
			}
			if (HasValue)
			{
				num ^= Value.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ProductAttribute productAttribute = obj as ProductAttribute;
			if (productAttribute == null)
			{
				return false;
			}
			if (HasName != productAttribute.HasName || (HasName && !Name.Equals(productAttribute.Name)))
			{
				return false;
			}
			if (HasValue != productAttribute.HasValue || (HasValue && !Value.Equals(productAttribute.Value)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProductAttribute Deserialize(Stream stream, ProductAttribute instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProductAttribute DeserializeLengthDelimited(Stream stream)
		{
			ProductAttribute productAttribute = new ProductAttribute();
			DeserializeLengthDelimited(stream, productAttribute);
			return productAttribute;
		}

		public static ProductAttribute DeserializeLengthDelimited(Stream stream, ProductAttribute instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProductAttribute Deserialize(Stream stream, ProductAttribute instance, long limit)
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
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.Value = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ProductAttribute instance)
		{
			if (instance.HasName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasValue)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Value));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasValue)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Value);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
