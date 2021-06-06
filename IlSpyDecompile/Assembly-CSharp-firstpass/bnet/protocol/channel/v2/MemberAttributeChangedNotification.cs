using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	public class MemberAttributeChangedNotification : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasSubscriberId;

		private GameAccountHandle _SubscriberId;

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

		public GameAccountHandle SubscriberId
		{
			get
			{
				return _SubscriberId;
			}
			set
			{
				_SubscriberId = value;
				HasSubscriberId = value != null;
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

		public void SetSubscriberId(GameAccountHandle val)
		{
			SubscriberId = val;
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
			if (HasSubscriberId)
			{
				num ^= SubscriberId.GetHashCode();
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
			MemberAttributeChangedNotification memberAttributeChangedNotification = obj as MemberAttributeChangedNotification;
			if (memberAttributeChangedNotification == null)
			{
				return false;
			}
			if (HasAgentId != memberAttributeChangedNotification.HasAgentId || (HasAgentId && !AgentId.Equals(memberAttributeChangedNotification.AgentId)))
			{
				return false;
			}
			if (HasSubscriberId != memberAttributeChangedNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(memberAttributeChangedNotification.SubscriberId)))
			{
				return false;
			}
			if (HasChannelId != memberAttributeChangedNotification.HasChannelId || (HasChannelId && !ChannelId.Equals(memberAttributeChangedNotification.ChannelId)))
			{
				return false;
			}
			if (HasAssignment != memberAttributeChangedNotification.HasAssignment || (HasAssignment && !Assignment.Equals(memberAttributeChangedNotification.Assignment)))
			{
				return false;
			}
			return true;
		}

		public static MemberAttributeChangedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberAttributeChangedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MemberAttributeChangedNotification Deserialize(Stream stream, MemberAttributeChangedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MemberAttributeChangedNotification DeserializeLengthDelimited(Stream stream)
		{
			MemberAttributeChangedNotification memberAttributeChangedNotification = new MemberAttributeChangedNotification();
			DeserializeLengthDelimited(stream, memberAttributeChangedNotification);
			return memberAttributeChangedNotification;
		}

		public static MemberAttributeChangedNotification DeserializeLengthDelimited(Stream stream, MemberAttributeChangedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MemberAttributeChangedNotification Deserialize(Stream stream, MemberAttributeChangedNotification instance, long limit)
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
					if (instance.SubscriberId == null)
					{
						instance.SubscriberId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.SubscriberId);
					}
					continue;
				case 26:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 34:
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

		public static void Serialize(Stream stream, MemberAttributeChangedNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasAssignment)
			{
				stream.WriteByte(34);
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
			if (HasSubscriberId)
			{
				num++;
				uint serializedSize2 = SubscriberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasChannelId)
			{
				num++;
				uint serializedSize3 = ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasAssignment)
			{
				num++;
				uint serializedSize4 = Assignment.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}
	}
}
