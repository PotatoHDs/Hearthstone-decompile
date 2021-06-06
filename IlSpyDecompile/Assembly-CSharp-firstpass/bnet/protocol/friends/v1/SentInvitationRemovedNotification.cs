using System.IO;

namespace bnet.protocol.friends.v1
{
	public class SentInvitationRemovedNotification : IProtoBuf
	{
		public bool HasAccountId;

		private EntityId _AccountId;

		public bool HasInvitationId;

		private ulong _InvitationId;

		public bool HasReason;

		private uint _Reason;

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

		public void SetInvitationId(ulong val)
		{
			InvitationId = val;
		}

		public void SetReason(uint val)
		{
			Reason = val;
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
			if (HasInvitationId)
			{
				num ^= InvitationId.GetHashCode();
			}
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			if (HasForward)
			{
				num ^= Forward.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SentInvitationRemovedNotification sentInvitationRemovedNotification = obj as SentInvitationRemovedNotification;
			if (sentInvitationRemovedNotification == null)
			{
				return false;
			}
			if (HasAccountId != sentInvitationRemovedNotification.HasAccountId || (HasAccountId && !AccountId.Equals(sentInvitationRemovedNotification.AccountId)))
			{
				return false;
			}
			if (HasInvitationId != sentInvitationRemovedNotification.HasInvitationId || (HasInvitationId && !InvitationId.Equals(sentInvitationRemovedNotification.InvitationId)))
			{
				return false;
			}
			if (HasReason != sentInvitationRemovedNotification.HasReason || (HasReason && !Reason.Equals(sentInvitationRemovedNotification.Reason)))
			{
				return false;
			}
			if (HasForward != sentInvitationRemovedNotification.HasForward || (HasForward && !Forward.Equals(sentInvitationRemovedNotification.Forward)))
			{
				return false;
			}
			return true;
		}

		public static SentInvitationRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SentInvitationRemovedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SentInvitationRemovedNotification Deserialize(Stream stream, SentInvitationRemovedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SentInvitationRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			SentInvitationRemovedNotification sentInvitationRemovedNotification = new SentInvitationRemovedNotification();
			DeserializeLengthDelimited(stream, sentInvitationRemovedNotification);
			return sentInvitationRemovedNotification;
		}

		public static SentInvitationRemovedNotification DeserializeLengthDelimited(Stream stream, SentInvitationRemovedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SentInvitationRemovedNotification Deserialize(Stream stream, SentInvitationRemovedNotification instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 17:
					instance.InvitationId = binaryReader.ReadUInt64();
					continue;
				case 24:
					instance.Reason = ProtocolParser.ReadUInt32(stream);
					continue;
				case 34:
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

		public static void Serialize(Stream stream, SentInvitationRemovedNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasInvitationId)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.InvitationId);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
			if (instance.HasForward)
			{
				stream.WriteByte(34);
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
			if (HasInvitationId)
			{
				num++;
				num += 8;
			}
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Reason);
			}
			if (HasForward)
			{
				num++;
				uint serializedSize2 = Forward.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
