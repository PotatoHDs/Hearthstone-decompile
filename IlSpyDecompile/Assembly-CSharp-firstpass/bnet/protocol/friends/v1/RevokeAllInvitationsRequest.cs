using System.IO;

namespace bnet.protocol.friends.v1
{
	public class RevokeAllInvitationsRequest : IProtoBuf
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

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RevokeAllInvitationsRequest revokeAllInvitationsRequest = obj as RevokeAllInvitationsRequest;
			if (revokeAllInvitationsRequest == null)
			{
				return false;
			}
			if (HasAgentId != revokeAllInvitationsRequest.HasAgentId || (HasAgentId && !AgentId.Equals(revokeAllInvitationsRequest.AgentId)))
			{
				return false;
			}
			return true;
		}

		public static RevokeAllInvitationsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RevokeAllInvitationsRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RevokeAllInvitationsRequest Deserialize(Stream stream, RevokeAllInvitationsRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RevokeAllInvitationsRequest DeserializeLengthDelimited(Stream stream)
		{
			RevokeAllInvitationsRequest revokeAllInvitationsRequest = new RevokeAllInvitationsRequest();
			DeserializeLengthDelimited(stream, revokeAllInvitationsRequest);
			return revokeAllInvitationsRequest;
		}

		public static RevokeAllInvitationsRequest DeserializeLengthDelimited(Stream stream, RevokeAllInvitationsRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RevokeAllInvitationsRequest Deserialize(Stream stream, RevokeAllInvitationsRequest instance, long limit)
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
				case 18:
					if (instance.AgentId == null)
					{
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
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

		public static void Serialize(Stream stream, RevokeAllInvitationsRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
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
			return num;
		}
	}
}
