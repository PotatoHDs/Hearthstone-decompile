using System.IO;

namespace bnet.protocol.presence.v1
{
	public class FieldKey : IProtoBuf
	{
		public bool HasUniqueId;

		private ulong _UniqueId;

		public uint Program { get; set; }

		public uint Group { get; set; }

		public uint Field { get; set; }

		public ulong UniqueId
		{
			get
			{
				return _UniqueId;
			}
			set
			{
				_UniqueId = value;
				HasUniqueId = true;
			}
		}

		public bool IsInitialized => true;

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetGroup(uint val)
		{
			Group = val;
		}

		public void SetField(uint val)
		{
			Field = val;
		}

		public void SetUniqueId(ulong val)
		{
			UniqueId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Program.GetHashCode();
			hashCode ^= Group.GetHashCode();
			hashCode ^= Field.GetHashCode();
			if (HasUniqueId)
			{
				hashCode ^= UniqueId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			FieldKey fieldKey = obj as FieldKey;
			if (fieldKey == null)
			{
				return false;
			}
			if (!Program.Equals(fieldKey.Program))
			{
				return false;
			}
			if (!Group.Equals(fieldKey.Group))
			{
				return false;
			}
			if (!Field.Equals(fieldKey.Field))
			{
				return false;
			}
			if (HasUniqueId != fieldKey.HasUniqueId || (HasUniqueId && !UniqueId.Equals(fieldKey.UniqueId)))
			{
				return false;
			}
			return true;
		}

		public static FieldKey ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FieldKey>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FieldKey Deserialize(Stream stream, FieldKey instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FieldKey DeserializeLengthDelimited(Stream stream)
		{
			FieldKey fieldKey = new FieldKey();
			DeserializeLengthDelimited(stream, fieldKey);
			return fieldKey;
		}

		public static FieldKey DeserializeLengthDelimited(Stream stream, FieldKey instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FieldKey Deserialize(Stream stream, FieldKey instance, long limit)
		{
			instance.UniqueId = 0uL;
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
					instance.Program = ProtocolParser.ReadUInt32(stream);
					continue;
				case 16:
					instance.Group = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.Field = ProtocolParser.ReadUInt32(stream);
					continue;
				case 32:
					instance.UniqueId = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, FieldKey instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Program);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Group);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.Field);
			if (instance.HasUniqueId)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.UniqueId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(Program);
			num += ProtocolParser.SizeOfUInt32(Group);
			num += ProtocolParser.SizeOfUInt32(Field);
			if (HasUniqueId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(UniqueId);
			}
			return num + 3;
		}
	}
}
