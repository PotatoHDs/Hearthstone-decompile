using System.IO;
using System.Text;

namespace bnet.protocol.v2
{
	public class Variant : IProtoBuf
	{
		public bool HasBoolValue;

		private bool _BoolValue;

		public bool HasIntValue;

		private long _IntValue;

		public bool HasFloatValue;

		private double _FloatValue;

		public bool HasStringValue;

		private string _StringValue;

		public bool HasBlobValue;

		private byte[] _BlobValue;

		public bool HasUintValue;

		private ulong _UintValue;

		public bool BoolValue
		{
			get
			{
				return _BoolValue;
			}
			set
			{
				_BoolValue = value;
				HasBoolValue = true;
			}
		}

		public long IntValue
		{
			get
			{
				return _IntValue;
			}
			set
			{
				_IntValue = value;
				HasIntValue = true;
			}
		}

		public double FloatValue
		{
			get
			{
				return _FloatValue;
			}
			set
			{
				_FloatValue = value;
				HasFloatValue = true;
			}
		}

		public string StringValue
		{
			get
			{
				return _StringValue;
			}
			set
			{
				_StringValue = value;
				HasStringValue = value != null;
			}
		}

		public byte[] BlobValue
		{
			get
			{
				return _BlobValue;
			}
			set
			{
				_BlobValue = value;
				HasBlobValue = value != null;
			}
		}

		public ulong UintValue
		{
			get
			{
				return _UintValue;
			}
			set
			{
				_UintValue = value;
				HasUintValue = true;
			}
		}

		public bool IsInitialized => true;

		public void SetBoolValue(bool val)
		{
			BoolValue = val;
		}

		public void SetIntValue(long val)
		{
			IntValue = val;
		}

		public void SetFloatValue(double val)
		{
			FloatValue = val;
		}

		public void SetStringValue(string val)
		{
			StringValue = val;
		}

		public void SetBlobValue(byte[] val)
		{
			BlobValue = val;
		}

		public void SetUintValue(ulong val)
		{
			UintValue = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasBoolValue)
			{
				num ^= BoolValue.GetHashCode();
			}
			if (HasIntValue)
			{
				num ^= IntValue.GetHashCode();
			}
			if (HasFloatValue)
			{
				num ^= FloatValue.GetHashCode();
			}
			if (HasStringValue)
			{
				num ^= StringValue.GetHashCode();
			}
			if (HasBlobValue)
			{
				num ^= BlobValue.GetHashCode();
			}
			if (HasUintValue)
			{
				num ^= UintValue.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Variant variant = obj as Variant;
			if (variant == null)
			{
				return false;
			}
			if (HasBoolValue != variant.HasBoolValue || (HasBoolValue && !BoolValue.Equals(variant.BoolValue)))
			{
				return false;
			}
			if (HasIntValue != variant.HasIntValue || (HasIntValue && !IntValue.Equals(variant.IntValue)))
			{
				return false;
			}
			if (HasFloatValue != variant.HasFloatValue || (HasFloatValue && !FloatValue.Equals(variant.FloatValue)))
			{
				return false;
			}
			if (HasStringValue != variant.HasStringValue || (HasStringValue && !StringValue.Equals(variant.StringValue)))
			{
				return false;
			}
			if (HasBlobValue != variant.HasBlobValue || (HasBlobValue && !BlobValue.Equals(variant.BlobValue)))
			{
				return false;
			}
			if (HasUintValue != variant.HasUintValue || (HasUintValue && !UintValue.Equals(variant.UintValue)))
			{
				return false;
			}
			return true;
		}

		public static Variant ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Variant>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Variant Deserialize(Stream stream, Variant instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Variant DeserializeLengthDelimited(Stream stream)
		{
			Variant variant = new Variant();
			DeserializeLengthDelimited(stream, variant);
			return variant;
		}

		public static Variant DeserializeLengthDelimited(Stream stream, Variant instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Variant Deserialize(Stream stream, Variant instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					instance.BoolValue = ProtocolParser.ReadBool(stream);
					continue;
				case 16:
					instance.IntValue = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 25:
					instance.FloatValue = binaryReader.ReadDouble();
					continue;
				case 34:
					instance.StringValue = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.BlobValue = ProtocolParser.ReadBytes(stream);
					continue;
				case 48:
					instance.UintValue = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, Variant instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasBoolValue)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.BoolValue);
			}
			if (instance.HasIntValue)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.IntValue);
			}
			if (instance.HasFloatValue)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.FloatValue);
			}
			if (instance.HasStringValue)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.StringValue));
			}
			if (instance.HasBlobValue)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, instance.BlobValue);
			}
			if (instance.HasUintValue)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.UintValue);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasBoolValue)
			{
				num++;
				num++;
			}
			if (HasIntValue)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)IntValue);
			}
			if (HasFloatValue)
			{
				num++;
				num += 8;
			}
			if (HasStringValue)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(StringValue);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasBlobValue)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(BlobValue.Length) + BlobValue.Length);
			}
			if (HasUintValue)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(UintValue);
			}
			return num;
		}
	}
}
