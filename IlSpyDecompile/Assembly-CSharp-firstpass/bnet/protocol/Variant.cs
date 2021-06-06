using System.IO;
using System.Text;

namespace bnet.protocol
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

		public bool HasMessageValue;

		private byte[] _MessageValue;

		public bool HasFourccValue;

		private string _FourccValue;

		public bool HasUintValue;

		private ulong _UintValue;

		public bool HasEntityIdValue;

		private EntityId _EntityIdValue;

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

		public byte[] MessageValue
		{
			get
			{
				return _MessageValue;
			}
			set
			{
				_MessageValue = value;
				HasMessageValue = value != null;
			}
		}

		public string FourccValue
		{
			get
			{
				return _FourccValue;
			}
			set
			{
				_FourccValue = value;
				HasFourccValue = value != null;
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

		public EntityId EntityIdValue
		{
			get
			{
				return _EntityIdValue;
			}
			set
			{
				_EntityIdValue = value;
				HasEntityIdValue = value != null;
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

		public void SetMessageValue(byte[] val)
		{
			MessageValue = val;
		}

		public void SetFourccValue(string val)
		{
			FourccValue = val;
		}

		public void SetUintValue(ulong val)
		{
			UintValue = val;
		}

		public void SetEntityIdValue(EntityId val)
		{
			EntityIdValue = val;
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
			if (HasMessageValue)
			{
				num ^= MessageValue.GetHashCode();
			}
			if (HasFourccValue)
			{
				num ^= FourccValue.GetHashCode();
			}
			if (HasUintValue)
			{
				num ^= UintValue.GetHashCode();
			}
			if (HasEntityIdValue)
			{
				num ^= EntityIdValue.GetHashCode();
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
			if (HasMessageValue != variant.HasMessageValue || (HasMessageValue && !MessageValue.Equals(variant.MessageValue)))
			{
				return false;
			}
			if (HasFourccValue != variant.HasFourccValue || (HasFourccValue && !FourccValue.Equals(variant.FourccValue)))
			{
				return false;
			}
			if (HasUintValue != variant.HasUintValue || (HasUintValue && !UintValue.Equals(variant.UintValue)))
			{
				return false;
			}
			if (HasEntityIdValue != variant.HasEntityIdValue || (HasEntityIdValue && !EntityIdValue.Equals(variant.EntityIdValue)))
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
				case 16:
					instance.BoolValue = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.IntValue = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 33:
					instance.FloatValue = binaryReader.ReadDouble();
					continue;
				case 42:
					instance.StringValue = ProtocolParser.ReadString(stream);
					continue;
				case 50:
					instance.BlobValue = ProtocolParser.ReadBytes(stream);
					continue;
				case 58:
					instance.MessageValue = ProtocolParser.ReadBytes(stream);
					continue;
				case 66:
					instance.FourccValue = ProtocolParser.ReadString(stream);
					continue;
				case 72:
					instance.UintValue = ProtocolParser.ReadUInt64(stream);
					continue;
				case 82:
					if (instance.EntityIdValue == null)
					{
						instance.EntityIdValue = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.EntityIdValue);
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

		public static void Serialize(Stream stream, Variant instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasBoolValue)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.BoolValue);
			}
			if (instance.HasIntValue)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.IntValue);
			}
			if (instance.HasFloatValue)
			{
				stream.WriteByte(33);
				binaryWriter.Write(instance.FloatValue);
			}
			if (instance.HasStringValue)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.StringValue));
			}
			if (instance.HasBlobValue)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, instance.BlobValue);
			}
			if (instance.HasMessageValue)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, instance.MessageValue);
			}
			if (instance.HasFourccValue)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FourccValue));
			}
			if (instance.HasUintValue)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, instance.UintValue);
			}
			if (instance.HasEntityIdValue)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.EntityIdValue.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityIdValue);
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
			if (HasMessageValue)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(MessageValue.Length) + MessageValue.Length);
			}
			if (HasFourccValue)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(FourccValue);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasUintValue)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(UintValue);
			}
			if (HasEntityIdValue)
			{
				num++;
				uint serializedSize = EntityIdValue.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
