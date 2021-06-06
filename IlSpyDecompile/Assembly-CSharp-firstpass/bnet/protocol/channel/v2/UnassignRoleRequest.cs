using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	public class UnassignRoleRequest : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasChannelId;

		private ChannelId _ChannelId;

		public bool HasAssignment;

		private RoleAssignment _Assignment;

		public GameAccountHandle AgentId
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

		public ChannelId ChannelId
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

		public RoleAssignment Assignment
		{
			get
			{
				return _Assignment;
			}
			set
			{
				_Assignment = value;
				HasAssignment = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentId(GameAccountHandle val)
		{
			AgentId = val;
		}

		public void SetChannelId(ChannelId val)
		{
			ChannelId = val;
		}

		public void SetAssignment(RoleAssignment val)
		{
			Assignment = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			if (HasAssignment)
			{
				num ^= Assignment.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UnassignRoleRequest unassignRoleRequest = obj as UnassignRoleRequest;
			if (unassignRoleRequest == null)
			{
				return false;
			}
			if (HasAgentId != unassignRoleRequest.HasAgentId || (HasAgentId && !AgentId.Equals(unassignRoleRequest.AgentId)))
			{
				return false;
			}
			if (HasChannelId != unassignRoleRequest.HasChannelId || (HasChannelId && !ChannelId.Equals(unassignRoleRequest.ChannelId)))
			{
				return false;
			}
			if (HasAssignment != unassignRoleRequest.HasAssignment || (HasAssignment && !Assignment.Equals(unassignRoleRequest.Assignment)))
			{
				return false;
			}
			return true;
		}

		public static UnassignRoleRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnassignRoleRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UnassignRoleRequest Deserialize(Stream stream, UnassignRoleRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UnassignRoleRequest DeserializeLengthDelimited(Stream stream)
		{
			UnassignRoleRequest unassignRoleRequest = new UnassignRoleRequest();
			DeserializeLengthDelimited(stream, unassignRoleRequest);
			return unassignRoleRequest;
		}

		public static UnassignRoleRequest DeserializeLengthDelimited(Stream stream, UnassignRoleRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UnassignRoleRequest Deserialize(Stream stream, UnassignRoleRequest instance, long limit)
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
						instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 18:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 26:
					if (instance.Assignment == null)
					{
						instance.Assignment = RoleAssignment.DeserializeLengthDelimited(stream);
					}
					else
					{
						RoleAssignment.DeserializeLengthDelimited(stream, instance.Assignment);
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

		public static void Serialize(Stream stream, UnassignRoleRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasAssignment)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Assignment.GetSerializedSize());
				RoleAssignment.Serialize(stream, instance.Assignment);
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
			if (HasChannelId)
			{
				num++;
				uint serializedSize2 = ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasAssignment)
			{
				num++;
				uint serializedSize3 = Assignment.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
