using System.IO;

namespace bnet.protocol.channel.v1
{
	public class CreateChannelResponse : IProtoBuf
	{
		public bool HasChannelId;

		private EntityId _ChannelId;

		public ulong ObjectId { get; set; }

		public EntityId ChannelId
		{
			get
			{
				return _ChannelId;
			}
			set
			{
				_ChannelId = value;
				HasChannelId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetObjectId(ulong val)
		{
			ObjectId = val;
		}

		public void SetChannelId(EntityId val)
		{
			ChannelId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ObjectId.GetHashCode();
			if (HasChannelId)
			{
				hashCode ^= ChannelId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			CreateChannelResponse createChannelResponse = obj as CreateChannelResponse;
			if (createChannelResponse == null)
			{
				return false;
			}
			if (!ObjectId.Equals(createChannelResponse.ObjectId))
			{
				return false;
			}
			if (HasChannelId != createChannelResponse.HasChannelId || (HasChannelId && !ChannelId.Equals(createChannelResponse.ChannelId)))
			{
				return false;
			}
			return true;
		}

		public static CreateChannelResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateChannelResponse Deserialize(Stream stream, CreateChannelResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateChannelResponse DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelResponse createChannelResponse = new CreateChannelResponse();
			DeserializeLengthDelimited(stream, createChannelResponse);
			return createChannelResponse;
		}

		public static CreateChannelResponse DeserializeLengthDelimited(Stream stream, CreateChannelResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateChannelResponse Deserialize(Stream stream, CreateChannelResponse instance, long limit)
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
				case 8:
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
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

		public static void Serialize(Stream stream, CreateChannelResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64(ObjectId);
			if (HasChannelId)
			{
				num++;
				uint serializedSize = ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 1;
		}
	}
}
