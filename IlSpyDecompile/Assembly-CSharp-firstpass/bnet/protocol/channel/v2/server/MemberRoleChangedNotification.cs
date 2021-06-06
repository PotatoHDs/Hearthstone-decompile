using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	public class MemberRoleChangedNotification : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasChannelId;

		private bnet.protocol.channel.v1.ChannelId _ChannelId;

		private List<RoleAssignment> _Assignment = new List<RoleAssignment>();

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

		public bnet.protocol.channel.v1.ChannelId ChannelId
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

		public List<RoleAssignment> Assignment
		{
			get
			{
				return _Assignment;
			}
			set
			{
				_Assignment = value;
			}
		}

		public List<RoleAssignment> AssignmentList => _Assignment;

		public int AssignmentCount => _Assignment.Count;

		public bool IsInitialized => true;

		public void SetAgentId(GameAccountHandle val)
		{
			AgentId = val;
		}

		public void SetChannelId(bnet.protocol.channel.v1.ChannelId val)
		{
			ChannelId = val;
		}

		public void AddAssignment(RoleAssignment val)
		{
			_Assignment.Add(val);
		}

		public void ClearAssignment()
		{
			_Assignment.Clear();
		}

		public void SetAssignment(List<RoleAssignment> val)
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
			foreach (RoleAssignment item in Assignment)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MemberRoleChangedNotification memberRoleChangedNotification = obj as MemberRoleChangedNotification;
			if (memberRoleChangedNotification == null)
			{
				return false;
			}
			if (HasAgentId != memberRoleChangedNotification.HasAgentId || (HasAgentId && !AgentId.Equals(memberRoleChangedNotification.AgentId)))
			{
				return false;
			}
			if (HasChannelId != memberRoleChangedNotification.HasChannelId || (HasChannelId && !ChannelId.Equals(memberRoleChangedNotification.ChannelId)))
			{
				return false;
			}
			if (Assignment.Count != memberRoleChangedNotification.Assignment.Count)
			{
				return false;
			}
			for (int i = 0; i < Assignment.Count; i++)
			{
				if (!Assignment[i].Equals(memberRoleChangedNotification.Assignment[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static MemberRoleChangedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberRoleChangedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MemberRoleChangedNotification Deserialize(Stream stream, MemberRoleChangedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MemberRoleChangedNotification DeserializeLengthDelimited(Stream stream)
		{
			MemberRoleChangedNotification memberRoleChangedNotification = new MemberRoleChangedNotification();
			DeserializeLengthDelimited(stream, memberRoleChangedNotification);
			return memberRoleChangedNotification;
		}

		public static MemberRoleChangedNotification DeserializeLengthDelimited(Stream stream, MemberRoleChangedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MemberRoleChangedNotification Deserialize(Stream stream, MemberRoleChangedNotification instance, long limit)
		{
			if (instance.Assignment == null)
			{
				instance.Assignment = new List<RoleAssignment>();
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
						instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 26:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = bnet.protocol.channel.v1.ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.channel.v1.ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 42:
					instance.Assignment.Add(RoleAssignment.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, MemberRoleChangedNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				bnet.protocol.channel.v1.ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.Assignment.Count <= 0)
			{
				return;
			}
			foreach (RoleAssignment item in instance.Assignment)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				RoleAssignment.Serialize(stream, item);
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
			if (Assignment.Count > 0)
			{
				foreach (RoleAssignment item in Assignment)
				{
					num++;
					uint serializedSize3 = item.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
				return num;
			}
			return num;
		}
	}
}
