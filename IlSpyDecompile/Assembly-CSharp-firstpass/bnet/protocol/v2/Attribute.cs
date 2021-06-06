using System.IO;
using System.Text;

namespace bnet.protocol.v2
{
	public class Attribute : IProtoBuf
	{
		public bool HasName;

		private string _Name;

		public bool HasValue;

		private Variant _Value;

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

		public Variant Value
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

		public bool IsInitialized => true;

		public void SetName(string val)
		{
			Name = val;
		}

		public void SetValue(Variant val)
		{
			Value = val;
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
			Attribute attribute = obj as Attribute;
			if (attribute == null)
			{
				return false;
			}
			if (HasName != attribute.HasName || (HasName && !Name.Equals(attribute.Name)))
			{
				return false;
			}
			if (HasValue != attribute.HasValue || (HasValue && !Value.Equals(attribute.Value)))
			{
				return false;
			}
			return true;
		}

		public static Attribute ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Attribute>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Attribute Deserialize(Stream stream, Attribute instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Attribute DeserializeLengthDelimited(Stream stream)
		{
			Attribute attribute = new Attribute();
			DeserializeLengthDelimited(stream, attribute);
			return attribute;
		}

		public static Attribute DeserializeLengthDelimited(Stream stream, Attribute instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Attribute Deserialize(Stream stream, Attribute instance, long limit)
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
					if (instance.Value == null)
					{
						instance.Value = Variant.DeserializeLengthDelimited(stream);
					}
					else
					{
						Variant.DeserializeLengthDelimited(stream, instance.Value);
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

		public static void Serialize(Stream stream, Attribute instance)
		{
			if (instance.HasName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasValue)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Value.GetSerializedSize());
				Variant.Serialize(stream, instance.Value);
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
				uint serializedSize = Value.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
