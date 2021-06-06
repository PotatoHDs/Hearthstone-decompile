using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	public class SetMemberAttributeRequest : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasChannelId;

		private ChannelId _ChannelId;

		public bool HasAssignment;

		private AttributeAssignment _Assignment;

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

		public AttributeAssignment Assignment
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

		public void SetAssignment(AttributeAssignment val)
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
			SetMemberAttributeRequest setMemberAttributeRequest = obj as SetMemberAttributeRequest;
			if (setMemberAttributeRequest == null)
			{
				return false;
			}
			if (HasAgentId != setMemberAttributeRequest.HasAgentId || (HasAgentId && !AgentId.Equals(setMemberAttributeRequest.AgentId)))
			{
				return false;
			}
			if (HasChannelId != setMemberAttributeRequest.HasChannelId || (HasChannelId && !ChannelId.Equals(setMemberAttributeRequest.ChannelId)))
			{
				return false;
			}
			if (HasAssignment != setMemberAttributeRequest.HasAssignment || (HasAssignment && !Assignment.Equals(setMemberAttributeRequest.Assignment)))
			{
				return false;
			}
			return true;
		}

		public static SetMemberAttributeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SetMemberAttributeRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SetMemberAttributeRequest Deserialize(Stream stream, SetMemberAttributeRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SetMemberAttributeRequest DeserializeLengthDelimited(Stream stream)
		{
			SetMemberAttributeRequest setMemberAttributeRequest = new SetMemberAttributeRequest();
			DeserializeLengthDelimited(stream, setMemberAttributeRequest);
			return setMemberAttributeRequest;
		}

		public static SetMemberAttributeRequest DeserializeLengthDelimited(Stream stream, SetMemberAttributeRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SetMemberAttributeRequest Deserialize(Stream stream, SetMemberAttributeRequest instance, long limit)
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
						instance.Assignment = AttributeAssignment.DeserializeLengthDelimited(stream);
					}
					else
					{
						AttributeAssignment.DeserializeLengthDelimited(stream, instance.Assignment);
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

		public static void Serialize(Stream stream, SetMemberAttributeRequest instance)
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
				AttributeAssignment.Serialize(stream, instance.Assignment);
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
