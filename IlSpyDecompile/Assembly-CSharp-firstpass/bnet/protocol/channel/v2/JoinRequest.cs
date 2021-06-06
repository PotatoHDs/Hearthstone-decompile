using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	public class JoinRequest : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasChannelId;

		private ChannelId _ChannelId;

		public bool HasMember;

		private CreateMemberOptions _Member;

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

		public CreateMemberOptions Member
		{
			get
			{
				return _Member;
			}
			set
			{
				_Member = value;
				HasMember = value != null;
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

		public void SetMember(CreateMemberOptions val)
		{
			Member = val;
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
			if (HasMember)
			{
				num ^= Member.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			JoinRequest joinRequest = obj as JoinRequest;
			if (joinRequest == null)
			{
				return false;
			}
			if (HasAgentId != joinRequest.HasAgentId || (HasAgentId && !AgentId.Equals(joinRequest.AgentId)))
			{
				return false;
			}
			if (HasChannelId != joinRequest.HasChannelId || (HasChannelId && !ChannelId.Equals(joinRequest.ChannelId)))
			{
				return false;
			}
			if (HasMember != joinRequest.HasMember || (HasMember && !Member.Equals(joinRequest.Member)))
			{
				return false;
			}
			return true;
		}

		public static JoinRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static JoinRequest Deserialize(Stream stream, JoinRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static JoinRequest DeserializeLengthDelimited(Stream stream)
		{
			JoinRequest joinRequest = new JoinRequest();
			DeserializeLengthDelimited(stream, joinRequest);
			return joinRequest;
		}

		public static JoinRequest DeserializeLengthDelimited(Stream stream, JoinRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static JoinRequest Deserialize(Stream stream, JoinRequest instance, long limit)
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
					if (instance.Member == null)
					{
						instance.Member = CreateMemberOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						CreateMemberOptions.DeserializeLengthDelimited(stream, instance.Member);
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

		public static void Serialize(Stream stream, JoinRequest instance)
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
			if (instance.HasMember)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Member.GetSerializedSize());
				CreateMemberOptions.Serialize(stream, instance.Member);
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
			if (HasMember)
			{
				num++;
				uint serializedSize3 = Member.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
