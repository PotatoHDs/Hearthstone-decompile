using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.Types;

namespace bnet.protocol.channel.v2.membership
{
	public class ReceivedInvitationRemovedNotification : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasSubscriberId;

		private GameAccountHandle _SubscriberId;

		public bool HasInvitationId;

		private ulong _InvitationId;

		public bool HasReason;

		private InvitationRemovedReason _Reason;

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

		public ulong InvitationId
		{
			get
			{
				return _InvitationId;
			}
			set
			{
				_InvitationId = value;
				HasInvitationId = true;
			}
		}

		public InvitationRemovedReason Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = true;
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

		public void SetInvitationId(ulong val)
		{
			InvitationId = val;
		}

		public void SetReason(InvitationRemovedReason val)
		{
			Reason = val;
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
			if (HasInvitationId)
			{
				num ^= InvitationId.GetHashCode();
			}
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ReceivedInvitationRemovedNotification receivedInvitationRemovedNotification = obj as ReceivedInvitationRemovedNotification;
			if (receivedInvitationRemovedNotification == null)
			{
				return false;
			}
			if (HasAgentId != receivedInvitationRemovedNotification.HasAgentId || (HasAgentId && !AgentId.Equals(receivedInvitationRemovedNotification.AgentId)))
			{
				return false;
			}
			if (HasSubscriberId != receivedInvitationRemovedNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(receivedInvitationRemovedNotification.SubscriberId)))
			{
				return false;
			}
			if (HasInvitationId != receivedInvitationRemovedNotification.HasInvitationId || (HasInvitationId && !InvitationId.Equals(receivedInvitationRemovedNotification.InvitationId)))
			{
				return false;
			}
			if (HasReason != receivedInvitationRemovedNotification.HasReason || (HasReason && !Reason.Equals(receivedInvitationRemovedNotification.Reason)))
			{
				return false;
			}
			return true;
		}

		public static ReceivedInvitationRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ReceivedInvitationRemovedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ReceivedInvitationRemovedNotification Deserialize(Stream stream, ReceivedInvitationRemovedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ReceivedInvitationRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			ReceivedInvitationRemovedNotification receivedInvitationRemovedNotification = new ReceivedInvitationRemovedNotification();
			DeserializeLengthDelimited(stream, receivedInvitationRemovedNotification);
			return receivedInvitationRemovedNotification;
		}

		public static ReceivedInvitationRemovedNotification DeserializeLengthDelimited(Stream stream, ReceivedInvitationRemovedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ReceivedInvitationRemovedNotification Deserialize(Stream stream, ReceivedInvitationRemovedNotification instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Reason = InvitationRemovedReason.INVITATION_REMOVED_REASON_ACCEPTED;
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
				case 25:
					instance.InvitationId = binaryReader.ReadUInt64();
					continue;
				case 32:
					instance.Reason = (InvitationRemovedReason)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ReceivedInvitationRemovedNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
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
			if (instance.HasInvitationId)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.InvitationId);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Reason);
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
			if (HasInvitationId)
			{
				num++;
				num += 8;
			}
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Reason);
			}
			return num;
		}
	}
}
