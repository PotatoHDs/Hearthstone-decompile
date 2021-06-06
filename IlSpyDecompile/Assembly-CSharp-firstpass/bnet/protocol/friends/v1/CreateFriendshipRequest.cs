using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v1
{
	public class CreateFriendshipRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasTargetId;

		private EntityId _TargetId;

		private List<uint> _Role = new List<uint>();

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

		public EntityId TargetId
		{
			get
			{
				return _TargetId;
			}
			set
			{
				_TargetId = value;
				HasTargetId = value != null;
			}
		}

		public List<uint> Role
		{
			get
			{
				return _Role;
			}
			set
			{
				_Role = value;
			}
		}

		public List<uint> RoleList => _Role;

		public int RoleCount => _Role.Count;

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetTargetId(EntityId val)
		{
			TargetId = val;
		}

		public void AddRole(uint val)
		{
			_Role.Add(val);
		}

		public void ClearRole()
		{
			_Role.Clear();
		}

		public void SetRole(List<uint> val)
		{
			Role = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			if (HasTargetId)
			{
				num ^= TargetId.GetHashCode();
			}
			foreach (uint item in Role)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateFriendshipRequest createFriendshipRequest = obj as CreateFriendshipRequest;
			if (createFriendshipRequest == null)
			{
				return false;
			}
			if (HasAgentId != createFriendshipRequest.HasAgentId || (HasAgentId && !AgentId.Equals(createFriendshipRequest.AgentId)))
			{
				return false;
			}
			if (HasTargetId != createFriendshipRequest.HasTargetId || (HasTargetId && !TargetId.Equals(createFriendshipRequest.TargetId)))
			{
				return false;
			}
			if (Role.Count != createFriendshipRequest.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < Role.Count; i++)
			{
				if (!Role[i].Equals(createFriendshipRequest.Role[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static CreateFriendshipRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateFriendshipRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateFriendshipRequest Deserialize(Stream stream, CreateFriendshipRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateFriendshipRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateFriendshipRequest createFriendshipRequest = new CreateFriendshipRequest();
			DeserializeLengthDelimited(stream, createFriendshipRequest);
			return createFriendshipRequest;
		}

		public static CreateFriendshipRequest DeserializeLengthDelimited(Stream stream, CreateFriendshipRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateFriendshipRequest Deserialize(Stream stream, CreateFriendshipRequest instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
			}
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
				case 18:
					if (instance.TargetId == null)
					{
						instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
					}
					continue;
				case 26:
				{
					long num2 = ProtocolParser.ReadUInt32(stream);
					num2 += stream.Position;
					while (stream.Position < num2)
					{
						instance.Role.Add(ProtocolParser.ReadUInt32(stream));
					}
					if (stream.Position == num2)
					{
						continue;
					}
					throw new ProtocolBufferException("Read too many bytes in packed data");
				}
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

		public static void Serialize(Stream stream, CreateFriendshipRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasTargetId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				EntityId.Serialize(stream, instance.TargetId);
			}
			if (instance.Role.Count <= 0)
			{
				return;
			}
			stream.WriteByte(26);
			uint num = 0u;
			foreach (uint item in instance.Role)
			{
				num += ProtocolParser.SizeOfUInt32(item);
			}
			ProtocolParser.WriteUInt32(stream, num);
			foreach (uint item2 in instance.Role)
			{
				ProtocolParser.WriteUInt32(stream, item2);
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
			if (HasTargetId)
			{
				num++;
				uint serializedSize2 = TargetId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (Role.Count > 0)
			{
				num++;
				uint num2 = num;
				foreach (uint item in Role)
				{
					num += ProtocolParser.SizeOfUInt32(item);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			return num;
		}
	}
}
