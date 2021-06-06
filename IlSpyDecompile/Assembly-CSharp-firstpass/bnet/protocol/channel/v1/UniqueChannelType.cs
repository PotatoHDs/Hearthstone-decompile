using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	public class UniqueChannelType : IProtoBuf
	{
		public bool HasServiceType;

		private uint _ServiceType;

		public bool HasProgram;

		private uint _Program;

		public bool HasChannelType;

		private string _ChannelType;

		public uint ServiceType
		{
			get
			{
				return _ServiceType;
			}
			set
			{
				_ServiceType = value;
				HasServiceType = true;
			}
		}

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public string ChannelType
		{
			get
			{
				return _ChannelType;
			}
			set
			{
				_ChannelType = value;
				HasChannelType = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetServiceType(uint val)
		{
			ServiceType = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetChannelType(string val)
		{
			ChannelType = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasServiceType)
			{
				num ^= ServiceType.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasChannelType)
			{
				num ^= ChannelType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UniqueChannelType uniqueChannelType = obj as UniqueChannelType;
			if (uniqueChannelType == null)
			{
				return false;
			}
			if (HasServiceType != uniqueChannelType.HasServiceType || (HasServiceType && !ServiceType.Equals(uniqueChannelType.ServiceType)))
			{
				return false;
			}
			if (HasProgram != uniqueChannelType.HasProgram || (HasProgram && !Program.Equals(uniqueChannelType.Program)))
			{
				return false;
			}
			if (HasChannelType != uniqueChannelType.HasChannelType || (HasChannelType && !ChannelType.Equals(uniqueChannelType.ChannelType)))
			{
				return false;
			}
			return true;
		}

		public static UniqueChannelType ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UniqueChannelType>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UniqueChannelType Deserialize(Stream stream, UniqueChannelType instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UniqueChannelType DeserializeLengthDelimited(Stream stream)
		{
			UniqueChannelType uniqueChannelType = new UniqueChannelType();
			DeserializeLengthDelimited(stream, uniqueChannelType);
			return uniqueChannelType;
		}

		public static UniqueChannelType DeserializeLengthDelimited(Stream stream, UniqueChannelType instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UniqueChannelType Deserialize(Stream stream, UniqueChannelType instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.ChannelType = "default";
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
					instance.ServiceType = ProtocolParser.ReadUInt32(stream);
					continue;
				case 21:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 26:
					instance.ChannelType = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, UniqueChannelType instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasServiceType)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.ServiceType);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasChannelType)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelType));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasServiceType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ServiceType);
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (HasChannelType)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ChannelType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
