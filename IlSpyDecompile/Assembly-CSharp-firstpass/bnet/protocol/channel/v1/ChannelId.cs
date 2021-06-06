using System.IO;

namespace bnet.protocol.channel.v1
{
	public class ChannelId : IProtoBuf
	{
		public bool HasType;

		private uint _Type;

		public bool HasHost;

		private ProcessId _Host;

		public bool HasId;

		private uint _Id;

		public uint Type
		{
			get
			{
				return _Type;
			}
			set
			{
				_Type = value;
				HasType = true;
			}
		}

		public ProcessId Host
		{
			get
			{
				return _Host;
			}
			set
			{
				_Host = value;
				HasHost = value != null;
			}
		}

		public uint Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				HasId = true;
			}
		}

		public bool IsInitialized => true;

		public void SetType(uint val)
		{
			Type = val;
		}

		public void SetHost(ProcessId val)
		{
			Host = val;
		}

		public void SetId(uint val)
		{
			Id = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasType)
			{
				num ^= Type.GetHashCode();
			}
			if (HasHost)
			{
				num ^= Host.GetHashCode();
			}
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelId channelId = obj as ChannelId;
			if (channelId == null)
			{
				return false;
			}
			if (HasType != channelId.HasType || (HasType && !Type.Equals(channelId.Type)))
			{
				return false;
			}
			if (HasHost != channelId.HasHost || (HasHost && !Host.Equals(channelId.Host)))
			{
				return false;
			}
			if (HasId != channelId.HasId || (HasId && !Id.Equals(channelId.Id)))
			{
				return false;
			}
			return true;
		}

		public static ChannelId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelId>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelId Deserialize(Stream stream, ChannelId instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelId DeserializeLengthDelimited(Stream stream)
		{
			ChannelId channelId = new ChannelId();
			DeserializeLengthDelimited(stream, channelId);
			return channelId;
		}

		public static ChannelId DeserializeLengthDelimited(Stream stream, ChannelId instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelId Deserialize(Stream stream, ChannelId instance, long limit)
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
					instance.Type = ProtocolParser.ReadUInt32(stream);
					continue;
				case 18:
					if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
					}
					continue;
				case 29:
					instance.Id = binaryReader.ReadUInt32();
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

		public static void Serialize(Stream stream, ChannelId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasType)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.Type);
			}
			if (instance.HasHost)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
			if (instance.HasId)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.Id);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Type);
			}
			if (HasHost)
			{
				num++;
				uint serializedSize = Host.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasId)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
