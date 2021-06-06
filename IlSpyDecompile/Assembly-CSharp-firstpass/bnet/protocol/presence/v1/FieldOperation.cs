using System;
using System.IO;

namespace bnet.protocol.presence.v1
{
	public class FieldOperation : IProtoBuf
	{
		public static class Types
		{
			public enum OperationType
			{
				SET,
				CLEAR
			}
		}

		public bool HasOperation;

		private Types.OperationType _Operation;

		public Field Field { get; set; }

		public Types.OperationType Operation
		{
			get
			{
				return _Operation;
			}
			set
			{
				_Operation = value;
				HasOperation = true;
			}
		}

		public bool IsInitialized => true;

		public void SetField(Field val)
		{
			Field = val;
		}

		public void SetOperation(Types.OperationType val)
		{
			Operation = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Field.GetHashCode();
			if (HasOperation)
			{
				hashCode ^= Operation.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			FieldOperation fieldOperation = obj as FieldOperation;
			if (fieldOperation == null)
			{
				return false;
			}
			if (!Field.Equals(fieldOperation.Field))
			{
				return false;
			}
			if (HasOperation != fieldOperation.HasOperation || (HasOperation && !Operation.Equals(fieldOperation.Operation)))
			{
				return false;
			}
			return true;
		}

		public static FieldOperation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FieldOperation>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FieldOperation Deserialize(Stream stream, FieldOperation instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FieldOperation DeserializeLengthDelimited(Stream stream)
		{
			FieldOperation fieldOperation = new FieldOperation();
			DeserializeLengthDelimited(stream, fieldOperation);
			return fieldOperation;
		}

		public static FieldOperation DeserializeLengthDelimited(Stream stream, FieldOperation instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FieldOperation Deserialize(Stream stream, FieldOperation instance, long limit)
		{
			instance.Operation = Types.OperationType.SET;
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
					if (instance.Field == null)
					{
						instance.Field = Field.DeserializeLengthDelimited(stream);
					}
					else
					{
						Field.DeserializeLengthDelimited(stream, instance.Field);
					}
					continue;
				case 16:
					instance.Operation = (Types.OperationType)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, FieldOperation instance)
		{
			if (instance.Field == null)
			{
				throw new ArgumentNullException("Field", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Field.GetSerializedSize());
			Field.Serialize(stream, instance.Field);
			if (instance.HasOperation)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Operation);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Field.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasOperation)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Operation);
			}
			return num + 1;
		}
	}
}
