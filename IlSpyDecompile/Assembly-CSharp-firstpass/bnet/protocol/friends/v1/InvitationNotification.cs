using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	public class InvitationNotification : IProtoBuf
	{
		public bool HasReason;

		private uint _Reason;

		public bool HasAccountId;

		private EntityId _AccountId;

		public bool HasForward;

		private ObjectAddress _Forward;

		public ReceivedInvitation Invitation { get; set; }

		public uint Reason
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

		public EntityId AccountId
		{
			get
			{
				return _AccountId;
			}
			set
			{
				_AccountId = value;
				HasAccountId = value != null;
			}
		}

		public ObjectAddress Forward
		{
			get
			{
				return _Forward;
			}
			set
			{
				_Forward = value;
				HasForward = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetInvitation(ReceivedInvitation val)
		{
			Invitation = val;
		}

		public void SetReason(uint val)
		{
			Reason = val;
		}

		public void SetAccountId(EntityId val)
		{
			AccountId = val;
		}

		public void SetForward(ObjectAddress val)
		{
			Forward = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Invitation.GetHashCode();
			if (HasReason)
			{
				hashCode ^= Reason.GetHashCode();
			}
			if (HasAccountId)
			{
				hashCode ^= AccountId.GetHashCode();
			}
			if (HasForward)
			{
				hashCode ^= Forward.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			InvitationNotification invitationNotification = obj as InvitationNotification;
			if (invitationNotification == null)
			{
				return false;
			}
			if (!Invitation.Equals(invitationNotification.Invitation))
			{
				return false;
			}
			if (HasReason != invitationNotification.HasReason || (HasReason && !Reason.Equals(invitationNotification.Reason)))
			{
				return false;
			}
			if (HasAccountId != invitationNotification.HasAccountId || (HasAccountId && !AccountId.Equals(invitationNotification.AccountId)))
			{
				return false;
			}
			if (HasForward != invitationNotification.HasForward || (HasForward && !Forward.Equals(invitationNotification.Forward)))
			{
				return false;
			}
			return true;
		}

		public static InvitationNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static InvitationNotification Deserialize(Stream stream, InvitationNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static InvitationNotification DeserializeLengthDelimited(Stream stream)
		{
			InvitationNotification invitationNotification = new InvitationNotification();
			DeserializeLengthDelimited(stream, invitationNotification);
			return invitationNotification;
		}

		public static InvitationNotification DeserializeLengthDelimited(Stream stream, InvitationNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static InvitationNotification Deserialize(Stream stream, InvitationNotification instance, long limit)
		{
			instance.Reason = 0u;
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
					if (instance.Invitation == null)
					{
						instance.Invitation = ReceivedInvitation.DeserializeLengthDelimited(stream);
					}
					else
					{
						ReceivedInvitation.DeserializeLengthDelimited(stream, instance.Invitation);
					}
					continue;
				case 24:
					instance.Reason = ProtocolParser.ReadUInt32(stream);
					continue;
				case 42:
					if (instance.AccountId == null)
					{
						instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
					}
					continue;
				case 50:
					if (instance.Forward == null)
					{
						instance.Forward = ObjectAddress.DeserializeLengthDelimited(stream);
					}
					else
					{
						ObjectAddress.DeserializeLengthDelimited(stream, instance.Forward);
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

		public static void Serialize(Stream stream, InvitationNotification instance)
		{
			if (instance.Invitation == null)
			{
				throw new ArgumentNullException("Invitation", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Invitation.GetSerializedSize());
			ReceivedInvitation.Serialize(stream, instance.Invitation);
			if (instance.HasReason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
			if (instance.HasAccountId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasForward)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.Forward.GetSerializedSize());
				ObjectAddress.Serialize(stream, instance.Forward);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Invitation.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Reason);
			}
			if (HasAccountId)
			{
				num++;
				uint serializedSize2 = AccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasForward)
			{
				num++;
				uint serializedSize3 = Forward.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 1;
		}
	}
}
