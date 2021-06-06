using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	public class CreateChannelRequest : IProtoBuf
	{
		public bool HasAgentIdentity;

		private bnet.protocol.account.v1.Identity _AgentIdentity;

		public bool HasChannelState;

		private ChannelState _ChannelState;

		public bool HasChannelId;

		private EntityId _ChannelId;

		public bool HasObjectId;

		private ulong _ObjectId;

		public bnet.protocol.account.v1.Identity AgentIdentity
		{
			get
			{
				return _AgentIdentity;
			}
			set
			{
				_AgentIdentity = value;
				HasAgentIdentity = value != null;
			}
		}

		public ChannelState ChannelState
		{
			get
			{
				return _ChannelState;
			}
			set
			{
				_ChannelState = value;
				HasChannelState = value != null;
			}
		}

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

		public ulong ObjectId
		{
			get
			{
				return _ObjectId;
			}
			set
			{
				_ObjectId = value;
				HasObjectId = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentIdentity(bnet.protocol.account.v1.Identity val)
		{
			AgentIdentity = val;
		}

		public void SetChannelState(ChannelState val)
		{
			ChannelState = val;
		}

		public void SetChannelId(EntityId val)
		{
			ChannelId = val;
		}

		public void SetObjectId(ulong val)
		{
			ObjectId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentIdentity)
			{
				num ^= AgentIdentity.GetHashCode();
			}
			if (HasChannelState)
			{
				num ^= ChannelState.GetHashCode();
			}
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			if (HasObjectId)
			{
				num ^= ObjectId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateChannelRequest createChannelRequest = obj as CreateChannelRequest;
			if (createChannelRequest == null)
			{
				return false;
			}
			if (HasAgentIdentity != createChannelRequest.HasAgentIdentity || (HasAgentIdentity && !AgentIdentity.Equals(createChannelRequest.AgentIdentity)))
			{
				return false;
			}
			if (HasChannelState != createChannelRequest.HasChannelState || (HasChannelState && !ChannelState.Equals(createChannelRequest.ChannelState)))
			{
				return false;
			}
			if (HasChannelId != createChannelRequest.HasChannelId || (HasChannelId && !ChannelId.Equals(createChannelRequest.ChannelId)))
			{
				return false;
			}
			if (HasObjectId != createChannelRequest.HasObjectId || (HasObjectId && !ObjectId.Equals(createChannelRequest.ObjectId)))
			{
				return false;
			}
			return true;
		}

		public static CreateChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateChannelRequest Deserialize(Stream stream, CreateChannelRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelRequest createChannelRequest = new CreateChannelRequest();
			DeserializeLengthDelimited(stream, createChannelRequest);
			return createChannelRequest;
		}

		public static CreateChannelRequest DeserializeLengthDelimited(Stream stream, CreateChannelRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateChannelRequest Deserialize(Stream stream, CreateChannelRequest instance, long limit)
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
					if (instance.AgentIdentity == null)
					{
						instance.AgentIdentity = bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream, instance.AgentIdentity);
					}
					continue;
				case 26:
					if (instance.ChannelState == null)
					{
						instance.ChannelState = ChannelState.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelState.DeserializeLengthDelimited(stream, instance.ChannelState);
					}
					continue;
				case 34:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 40:
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, CreateChannelRequest instance)
		{
			if (instance.HasAgentIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentIdentity.GetSerializedSize());
				bnet.protocol.account.v1.Identity.Serialize(stream, instance.AgentIdentity);
			}
			if (instance.HasChannelState)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelState.GetSerializedSize());
				ChannelState.Serialize(stream, instance.ChannelState);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasObjectId)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAgentIdentity)
			{
				num++;
				uint serializedSize = AgentIdentity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasChannelState)
			{
				num++;
				uint serializedSize2 = ChannelState.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasChannelId)
			{
				num++;
				uint serializedSize3 = ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasObjectId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ObjectId);
			}
			return num;
		}
	}
}
