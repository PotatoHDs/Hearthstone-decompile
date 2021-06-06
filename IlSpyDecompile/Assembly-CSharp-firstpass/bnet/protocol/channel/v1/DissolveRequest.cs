using System.IO;

namespace bnet.protocol.channel.v1
{
	public class DissolveRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasReason;

		private uint _Reason;

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

		public uint Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetReason(uint val)
		{
			Reason = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DissolveRequest dissolveRequest = obj as DissolveRequest;
			if (dissolveRequest == null)
			{
				return false;
			}
			if (HasAgentId != dissolveRequest.HasAgentId || (HasAgentId && !AgentId.Equals(dissolveRequest.AgentId)))
			{
				return false;
			}
			if (HasReason != dissolveRequest.HasReason || (HasReason && !Reason.Equals(dissolveRequest.Reason)))
			{
				return false;
			}
			return true;
		}

		public static DissolveRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DissolveRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DissolveRequest Deserialize(Stream stream, DissolveRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DissolveRequest DeserializeLengthDelimited(Stream stream)
		{
			DissolveRequest dissolveRequest = new DissolveRequest();
			DeserializeLengthDelimited(stream, dissolveRequest);
			return dissolveRequest;
		}

		public static DissolveRequest DeserializeLengthDelimited(Stream stream, DissolveRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DissolveRequest Deserialize(Stream stream, DissolveRequest instance, long limit)
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
					instance.Reason = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, DissolveRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
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
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Reason);
			}
			return num;
		}
	}
}
