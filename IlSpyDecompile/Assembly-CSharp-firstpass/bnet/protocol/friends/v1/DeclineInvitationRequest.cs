using System.IO;

namespace bnet.protocol.friends.v1
{
	public class DeclineInvitationRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

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

		public ulong InvitationId { get; set; }

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetInvitationId(ulong val)
		{
			InvitationId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			return num ^ InvitationId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			DeclineInvitationRequest declineInvitationRequest = obj as DeclineInvitationRequest;
			if (declineInvitationRequest == null)
			{
				return false;
			}
			if (HasAgentId != declineInvitationRequest.HasAgentId || (HasAgentId && !AgentId.Equals(declineInvitationRequest.AgentId)))
			{
				return false;
			}
			if (!InvitationId.Equals(declineInvitationRequest.InvitationId))
			{
				return false;
			}
			return true;
		}

		public static DeclineInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DeclineInvitationRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeclineInvitationRequest Deserialize(Stream stream, DeclineInvitationRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeclineInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			DeclineInvitationRequest declineInvitationRequest = new DeclineInvitationRequest();
			DeserializeLengthDelimited(stream, declineInvitationRequest);
			return declineInvitationRequest;
		}

		public static DeclineInvitationRequest DeserializeLengthDelimited(Stream stream, DeclineInvitationRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeclineInvitationRequest Deserialize(Stream stream, DeclineInvitationRequest instance, long limit)
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
				case 25:
					instance.InvitationId = binaryReader.ReadUInt64();
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

		public static void Serialize(Stream stream, DeclineInvitationRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			stream.WriteByte(25);
			binaryWriter.Write(instance.InvitationId);
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
			num += 8;
			return num + 1;
		}
	}
}
