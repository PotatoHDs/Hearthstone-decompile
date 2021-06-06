using System.IO;

namespace bnet.protocol.friends.v1
{
	public class SubscribeRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasForward;

		private ObjectAddress _Forward;

		public EntityId AgentId
		{
			get
			{
				return _AgentId;
			}
			set
			{
				_AgentId = value;
				HasAgentId = value != null;
			}
		}

		public ulong ObjectId { get; set; }

		public ObjectAddress Forward
		{
			get
			{
				return _Forward;
			}
			set
			{
				_Forward = value;
				HasForward = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetObjectId(ulong val)
		{
			ObjectId = val;
		}

		public void SetForward(ObjectAddress val)
		{
			Forward = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			num ^= ObjectId.GetHashCode();
			if (HasForward)
			{
				num ^= Forward.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			if (subscribeRequest == null)
			{
				return false;
			}
			if (HasAgentId != subscribeRequest.HasAgentId || (HasAgentId && !AgentId.Equals(subscribeRequest.AgentId)))
			{
				return false;
			}
			if (!ObjectId.Equals(subscribeRequest.ObjectId))
			{
				return false;
			}
			if (HasForward != subscribeRequest.HasForward || (HasForward && !Forward.Equals(subscribeRequest.Forward)))
			{
				return false;
			}
			return true;
		}

		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance, long limit)
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
					if (instance.AgentId == null)
					{
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 16:
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					if (instance.Forward == null)
					{
						instance.Forward = ObjectAddress.DeserializeLengthDelimited(stream);
					}
					else
					{
						ObjectAddress.DeserializeLengthDelimited(stream, instance.Forward);
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

		public static void Serialize(Stream stream, SubscribeRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			if (instance.HasForward)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Forward.GetSerializedSize());
				ObjectAddress.Serialize(stream, instance.Forward);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAgentId)
			{
				num++;
				uint serializedSize = AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			num += ProtocolParser.SizeOfUInt64(ObjectId);
			if (HasForward)
			{
				num++;
				uint serializedSize2 = Forward.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1;
		}
	}
}
