using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2.membership
{
	public class ReceivedInvitationAddedNotification : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasSubscriberId;

		private GameAccountHandle _SubscriberId;

		public bool HasInvitation;

		private ChannelInvitation _Invitation;

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

		public ChannelInvitation Invitation
		{
			get
			{
				return _Invitation;
			}
			set
			{
				_Invitation = value;
				HasInvitation = value != null;
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

		public void SetInvitation(ChannelInvitation val)
		{
			Invitation = val;
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
			if (HasInvitation)
			{
				num ^= Invitation.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ReceivedInvitationAddedNotification receivedInvitationAddedNotification = obj as ReceivedInvitationAddedNotification;
			if (receivedInvitationAddedNotification == null)
			{
				return false;
			}
			if (HasAgentId != receivedInvitationAddedNotification.HasAgentId || (HasAgentId && !AgentId.Equals(receivedInvitationAddedNotification.AgentId)))
			{
				return false;
			}
			if (HasSubscriberId != receivedInvitationAddedNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(receivedInvitationAddedNotification.SubscriberId)))
			{
				return false;
			}
			if (HasInvitation != receivedInvitationAddedNotification.HasInvitation || (HasInvitation && !Invitation.Equals(receivedInvitationAddedNotification.Invitation)))
			{
				return false;
			}
			return true;
		}

		public static ReceivedInvitationAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ReceivedInvitationAddedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ReceivedInvitationAddedNotification Deserialize(Stream stream, ReceivedInvitationAddedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ReceivedInvitationAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			ReceivedInvitationAddedNotification receivedInvitationAddedNotification = new ReceivedInvitationAddedNotification();
			DeserializeLengthDelimited(stream, receivedInvitationAddedNotification);
			return receivedInvitationAddedNotification;
		}

		public static ReceivedInvitationAddedNotification DeserializeLengthDelimited(Stream stream, ReceivedInvitationAddedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ReceivedInvitationAddedNotification Deserialize(Stream stream, ReceivedInvitationAddedNotification instance, long limit)
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
					if (instance.Invitation == null)
					{
						instance.Invitation = ChannelInvitation.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelInvitation.DeserializeLengthDelimited(stream, instance.Invitation);
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

		public static void Serialize(Stream stream, ReceivedInvitationAddedNotification instance)
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
			if (instance.HasInvitation)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Invitation.GetSerializedSize());
				ChannelInvitation.Serialize(stream, instance.Invitation);
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
			if (HasInvitation)
			{
				num++;
				uint serializedSize3 = Invitation.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
