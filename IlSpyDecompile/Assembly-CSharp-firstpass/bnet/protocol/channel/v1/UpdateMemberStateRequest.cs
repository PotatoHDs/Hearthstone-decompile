using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class UpdateMemberStateRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

		private List<Member> _StateChange = new List<Member>();

		private List<uint> _RemovedRole = new List<uint>();

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

		public List<Member> StateChange
		{
			get
			{
				return _StateChange;
			}
			set
			{
				_StateChange = value;
			}
		}

		public List<Member> StateChangeList => _StateChange;

		public int StateChangeCount => _StateChange.Count;

		public List<uint> RemovedRole
		{
			get
			{
				return _RemovedRole;
			}
			set
			{
				_RemovedRole = value;
			}
		}

		public List<uint> RemovedRoleList => _RemovedRole;

		public int RemovedRoleCount => _RemovedRole.Count;

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void AddStateChange(Member val)
		{
			_StateChange.Add(val);
		}

		public void ClearStateChange()
		{
			_StateChange.Clear();
		}

		public void SetStateChange(List<Member> val)
		{
			StateChange = val;
		}

		public void AddRemovedRole(uint val)
		{
			_RemovedRole.Add(val);
		}

		public void ClearRemovedRole()
		{
			_RemovedRole.Clear();
		}

		public void SetRemovedRole(List<uint> val)
		{
			RemovedRole = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			foreach (Member item in StateChange)
			{
				num ^= item.GetHashCode();
			}
			foreach (uint item2 in RemovedRole)
			{
				num ^= item2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateMemberStateRequest updateMemberStateRequest = obj as UpdateMemberStateRequest;
			if (updateMemberStateRequest == null)
			{
				return false;
			}
			if (HasAgentId != updateMemberStateRequest.HasAgentId || (HasAgentId && !AgentId.Equals(updateMemberStateRequest.AgentId)))
			{
				return false;
			}
			if (StateChange.Count != updateMemberStateRequest.StateChange.Count)
			{
				return false;
			}
			for (int i = 0; i < StateChange.Count; i++)
			{
				if (!StateChange[i].Equals(updateMemberStateRequest.StateChange[i]))
				{
					return false;
				}
			}
			if (RemovedRole.Count != updateMemberStateRequest.RemovedRole.Count)
			{
				return false;
			}
			for (int j = 0; j < RemovedRole.Count; j++)
			{
				if (!RemovedRole[j].Equals(updateMemberStateRequest.RemovedRole[j]))
				{
					return false;
				}
			}
			return true;
		}

		public static UpdateMemberStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateMemberStateRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateMemberStateRequest Deserialize(Stream stream, UpdateMemberStateRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateMemberStateRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateMemberStateRequest updateMemberStateRequest = new UpdateMemberStateRequest();
			DeserializeLengthDelimited(stream, updateMemberStateRequest);
			return updateMemberStateRequest;
		}

		public static UpdateMemberStateRequest DeserializeLengthDelimited(Stream stream, UpdateMemberStateRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateMemberStateRequest Deserialize(Stream stream, UpdateMemberStateRequest instance, long limit)
		{
			if (instance.StateChange == null)
			{
				instance.StateChange = new List<Member>();
			}
			if (instance.RemovedRole == null)
			{
				instance.RemovedRole = new List<uint>();
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
					instance.StateChange.Add(Member.DeserializeLengthDelimited(stream));
					continue;
				case 26:
				{
					long num2 = ProtocolParser.ReadUInt32(stream);
					num2 += stream.Position;
					while (stream.Position < num2)
					{
						instance.RemovedRole.Add(ProtocolParser.ReadUInt32(stream));
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

		public static void Serialize(Stream stream, UpdateMemberStateRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.StateChange.Count > 0)
			{
				foreach (Member item in instance.StateChange)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					Member.Serialize(stream, item);
				}
			}
			if (instance.RemovedRole.Count <= 0)
			{
				return;
			}
			stream.WriteByte(26);
			uint num = 0u;
			foreach (uint item2 in instance.RemovedRole)
			{
				num += ProtocolParser.SizeOfUInt32(item2);
			}
			ProtocolParser.WriteUInt32(stream, num);
			foreach (uint item3 in instance.RemovedRole)
			{
				ProtocolParser.WriteUInt32(stream, item3);
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
			if (StateChange.Count > 0)
			{
				foreach (Member item in StateChange)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (RemovedRole.Count > 0)
			{
				num++;
				uint num2 = num;
				foreach (uint item2 in RemovedRole)
				{
					num += ProtocolParser.SizeOfUInt32(item2);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			return num;
		}
	}
}
