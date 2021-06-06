using System.IO;
using System.Text;

namespace bnet.protocol.validation
{
	public class FieldValidationError : IProtoBuf
	{
		public bool HasName;

		private string _Name;

		public bool HasIsRepeatedField;

		private bool _IsRepeatedField;

		public bool HasIndex;

		private int _Index;

		public bool HasReason;

		private string _Reason;

		public bool HasChild;

		private MessageValidationError _Child;

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

		public bool IsRepeatedField
		{
			get
			{
				return _IsRepeatedField;
			}
			set
			{
				_IsRepeatedField = value;
				HasIsRepeatedField = true;
			}
		}

		public int Index
		{
			get
			{
				return _Index;
			}
			set
			{
				_Index = value;
				HasIndex = true;
			}
		}

		public string Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = value != null;
			}
		}

		public MessageValidationError Child
		{
			get
			{
				return _Child;
			}
			set
			{
				_Child = value;
				HasChild = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetName(string val)
		{
			Name = val;
		}

		public void SetIsRepeatedField(bool val)
		{
			IsRepeatedField = val;
		}

		public void SetIndex(int val)
		{
			Index = val;
		}

		public void SetReason(string val)
		{
			Reason = val;
		}

		public void SetChild(MessageValidationError val)
		{
			Child = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasName)
			{
				num ^= Name.GetHashCode();
			}
			if (HasIsRepeatedField)
			{
				num ^= IsRepeatedField.GetHashCode();
			}
			if (HasIndex)
			{
				num ^= Index.GetHashCode();
			}
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			if (HasChild)
			{
				num ^= Child.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FieldValidationError fieldValidationError = obj as FieldValidationError;
			if (fieldValidationError == null)
			{
				return false;
			}
			if (HasName != fieldValidationError.HasName || (HasName && !Name.Equals(fieldValidationError.Name)))
			{
				return false;
			}
			if (HasIsRepeatedField != fieldValidationError.HasIsRepeatedField || (HasIsRepeatedField && !IsRepeatedField.Equals(fieldValidationError.IsRepeatedField)))
			{
				return false;
			}
			if (HasIndex != fieldValidationError.HasIndex || (HasIndex && !Index.Equals(fieldValidationError.Index)))
			{
				return false;
			}
			if (HasReason != fieldValidationError.HasReason || (HasReason && !Reason.Equals(fieldValidationError.Reason)))
			{
				return false;
			}
			if (HasChild != fieldValidationError.HasChild || (HasChild && !Child.Equals(fieldValidationError.Child)))
			{
				return false;
			}
			return true;
		}

		public static FieldValidationError ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FieldValidationError>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FieldValidationError Deserialize(Stream stream, FieldValidationError instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FieldValidationError DeserializeLengthDelimited(Stream stream)
		{
			FieldValidationError fieldValidationError = new FieldValidationError();
			DeserializeLengthDelimited(stream, fieldValidationError);
			return fieldValidationError;
		}

		public static FieldValidationError DeserializeLengthDelimited(Stream stream, FieldValidationError instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FieldValidationError Deserialize(Stream stream, FieldValidationError instance, long limit)
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
				case 16:
					instance.IsRepeatedField = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.Index = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.Reason = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					if (instance.Child == null)
					{
						instance.Child = MessageValidationError.DeserializeLengthDelimited(stream);
					}
					else
					{
						MessageValidationError.DeserializeLengthDelimited(stream, instance.Child);
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

		public static void Serialize(Stream stream, FieldValidationError instance)
		{
			if (instance.HasName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasIsRepeatedField)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.IsRepeatedField);
			}
			if (instance.HasIndex)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Index);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Reason));
			}
			if (instance.HasChild)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Child.GetSerializedSize());
				MessageValidationError.Serialize(stream, instance.Child);
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
			if (HasIsRepeatedField)
			{
				num++;
				num++;
			}
			if (HasIndex)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Index);
			}
			if (HasReason)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Reason);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasChild)
			{
				num++;
				uint serializedSize = Child.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
