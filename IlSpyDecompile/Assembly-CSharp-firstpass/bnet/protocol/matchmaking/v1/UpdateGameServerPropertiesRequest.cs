using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class UpdateGameServerPropertiesRequest : IProtoBuf
	{
		public bool HasServerProperties;

		private GameServerProperties _ServerProperties;

		public GameServerProperties ServerProperties
		{
			get
			{
				return _ServerProperties;
			}
			set
			{
				_ServerProperties = value;
				HasServerProperties = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetServerProperties(GameServerProperties val)
		{
			ServerProperties = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasServerProperties)
			{
				num ^= ServerProperties.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateGameServerPropertiesRequest updateGameServerPropertiesRequest = obj as UpdateGameServerPropertiesRequest;
			if (updateGameServerPropertiesRequest == null)
			{
				return false;
			}
			if (HasServerProperties != updateGameServerPropertiesRequest.HasServerProperties || (HasServerProperties && !ServerProperties.Equals(updateGameServerPropertiesRequest.ServerProperties)))
			{
				return false;
			}
			return true;
		}

		public static UpdateGameServerPropertiesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateGameServerPropertiesRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateGameServerPropertiesRequest Deserialize(Stream stream, UpdateGameServerPropertiesRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateGameServerPropertiesRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateGameServerPropertiesRequest updateGameServerPropertiesRequest = new UpdateGameServerPropertiesRequest();
			DeserializeLengthDelimited(stream, updateGameServerPropertiesRequest);
			return updateGameServerPropertiesRequest;
		}

		public static UpdateGameServerPropertiesRequest DeserializeLengthDelimited(Stream stream, UpdateGameServerPropertiesRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateGameServerPropertiesRequest Deserialize(Stream stream, UpdateGameServerPropertiesRequest instance, long limit)
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
					if (instance.ServerProperties == null)
					{
						instance.ServerProperties = GameServerProperties.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameServerProperties.DeserializeLengthDelimited(stream, instance.ServerProperties);
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

		public static void Serialize(Stream stream, UpdateGameServerPropertiesRequest instance)
		{
			if (instance.HasServerProperties)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ServerProperties.GetSerializedSize());
				GameServerProperties.Serialize(stream, instance.ServerProperties);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasServerProperties)
			{
				num++;
				uint serializedSize = ServerProperties.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
