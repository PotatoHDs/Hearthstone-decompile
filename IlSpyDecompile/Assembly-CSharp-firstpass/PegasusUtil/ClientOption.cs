using System.IO;

namespace PegasusUtil
{
	public class ClientOption : IProtoBuf
	{
		public bool HasAsBool;

		private bool _AsBool;

		public bool HasAsInt32;

		private int _AsInt32;

		public bool HasAsInt64;

		private long _AsInt64;

		public bool HasAsFloat;

		private float _AsFloat;

		public bool HasAsUint64;

		private ulong _AsUint64;

		public int Index { get; set; }

		public bool AsBool
		{
			get
			{
				return _AsBool;
			}
			set
			{
				_AsBool = value;
				HasAsBool = true;
			}
		}

		public int AsInt32
		{
			get
			{
				return _AsInt32;
			}
			set
			{
				_AsInt32 = value;
				HasAsInt32 = true;
			}
		}

		public long AsInt64
		{
			get
			{
				return _AsInt64;
			}
			set
			{
				_AsInt64 = value;
				HasAsInt64 = true;
			}
		}

		public float AsFloat
		{
			get
			{
				return _AsFloat;
			}
			set
			{
				_AsFloat = value;
				HasAsFloat = true;
			}
		}

		public ulong AsUint64
		{
			get
			{
				return _AsUint64;
			}
			set
			{
				_AsUint64 = value;
				HasAsUint64 = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Index.GetHashCode();
			if (HasAsBool)
			{
				hashCode ^= AsBool.GetHashCode();
			}
			if (HasAsInt32)
			{
				hashCode ^= AsInt32.GetHashCode();
			}
			if (HasAsInt64)
			{
				hashCode ^= AsInt64.GetHashCode();
			}
			if (HasAsFloat)
			{
				hashCode ^= AsFloat.GetHashCode();
			}
			if (HasAsUint64)
			{
				hashCode ^= AsUint64.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ClientOption clientOption = obj as ClientOption;
			if (clientOption == null)
			{
				return false;
			}
			if (!Index.Equals(clientOption.Index))
			{
				return false;
			}
			if (HasAsBool != clientOption.HasAsBool || (HasAsBool && !AsBool.Equals(clientOption.AsBool)))
			{
				return false;
			}
			if (HasAsInt32 != clientOption.HasAsInt32 || (HasAsInt32 && !AsInt32.Equals(clientOption.AsInt32)))
			{
				return false;
			}
			if (HasAsInt64 != clientOption.HasAsInt64 || (HasAsInt64 && !AsInt64.Equals(clientOption.AsInt64)))
			{
				return false;
			}
			if (HasAsFloat != clientOption.HasAsFloat || (HasAsFloat && !AsFloat.Equals(clientOption.AsFloat)))
			{
				return false;
			}
			if (HasAsUint64 != clientOption.HasAsUint64 || (HasAsUint64 && !AsUint64.Equals(clientOption.AsUint64)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ClientOption Deserialize(Stream stream, ClientOption instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ClientOption DeserializeLengthDelimited(Stream stream)
		{
			ClientOption clientOption = new ClientOption();
			DeserializeLengthDelimited(stream, clientOption);
			return clientOption;
		}

		public static ClientOption DeserializeLengthDelimited(Stream stream, ClientOption instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ClientOption Deserialize(Stream stream, ClientOption instance, long limit)
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
					instance.Index = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.AsBool = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.AsInt32 = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.AsInt64 = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 45:
					instance.AsFloat = binaryReader.ReadSingle();
					continue;
				case 48:
					instance.AsUint64 = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ClientOption instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Index);
			if (instance.HasAsBool)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.AsBool);
			}
			if (instance.HasAsInt32)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AsInt32);
			}
			if (instance.HasAsInt64)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AsInt64);
			}
			if (instance.HasAsFloat)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.AsFloat);
			}
			if (instance.HasAsUint64)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.AsUint64);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Index);
			if (HasAsBool)
			{
				num++;
				num++;
			}
			if (HasAsInt32)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)AsInt32);
			}
			if (HasAsInt64)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)AsInt64);
			}
			if (HasAsFloat)
			{
				num++;
				num += 4;
			}
			if (HasAsUint64)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(AsUint64);
			}
			return num + 1;
		}
	}
}
