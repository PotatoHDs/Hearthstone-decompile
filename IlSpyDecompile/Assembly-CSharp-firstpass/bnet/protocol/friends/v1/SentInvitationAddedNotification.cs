using System.IO;

namespace bnet.protocol.friends.v1
{
	public class SentInvitationAddedNotification : IProtoBuf
	{
		public bool HasAccountId;

		private EntityId _AccountId;

		public bool HasInvitation;

		private SentInvitation _Invitation;

		public bool HasForward;

		private ObjectAddress _Forward;

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

		public SentInvitation Invitation
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

		public void SetAccountId(EntityId val)
		{
			AccountId = val;
		}

		public void SetInvitation(SentInvitation val)
		{
			Invitation = val;
		}

		public void SetForward(ObjectAddress val)
		{
			Forward = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccountId)
			{
				num ^= AccountId.GetHashCode();
			}
			if (HasInvitation)
			{
				num ^= Invitation.GetHashCode();
			}
			if (HasForward)
			{
				num ^= Forward.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SentInvitationAddedNotification sentInvitationAddedNotification = obj as SentInvitationAddedNotification;
			if (sentInvitationAddedNotification == null)
			{
				return false;
			}
			if (HasAccountId != sentInvitationAddedNotification.HasAccountId || (HasAccountId && !AccountId.Equals(sentInvitationAddedNotification.AccountId)))
			{
				return false;
			}
			if (HasInvitation != sentInvitationAddedNotification.HasInvitation || (HasInvitation && !Invitation.Equals(sentInvitationAddedNotification.Invitation)))
			{
				return false;
			}
			if (HasForward != sentInvitationAddedNotification.HasForward || (HasForward && !Forward.Equals(sentInvitationAddedNotification.Forward)))
			{
				return false;
			}
			return true;
		}

		public static SentInvitationAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SentInvitationAddedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SentInvitationAddedNotification Deserialize(Stream stream, SentInvitationAddedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SentInvitationAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			SentInvitationAddedNotification sentInvitationAddedNotification = new SentInvitationAddedNotification();
			DeserializeLengthDelimited(stream, sentInvitationAddedNotification);
			return sentInvitationAddedNotification;
		}

		public static SentInvitationAddedNotification DeserializeLengthDelimited(Stream stream, SentInvitationAddedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SentInvitationAddedNotification Deserialize(Stream stream, SentInvitationAddedNotification instance, long limit)
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
					if (instance.AccountId == null)
					{
						instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
					}
					continue;
				case 18:
					if (instance.Invitation == null)
					{
						instance.Invitation = SentInvitation.DeserializeLengthDelimited(stream);
					}
					else
					{
						SentInvitation.DeserializeLengthDelimited(stream, instance.Invitation);
					}
					continue;
				case 26:
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

		public static void Serialize(Stream stream, SentInvitationAddedNotification instance)
		{
			if (instance.HasAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasInvitation)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Invitation.GetSerializedSize());
				SentInvitation.Serialize(stream, instance.Invitation);
			}
			if (instance.HasForward)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Forward.GetSerializedSize());
				ObjectAddress.Serialize(stream, instance.Forward);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAccountId)
			{
				num++;
				uint serializedSize = AccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasInvitation)
			{
				num++;
				uint serializedSize2 = Invitation.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasForward)
			{
				num++;
				uint serializedSize3 = Forward.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
