using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class InvitationRemovedNotification : IProtoBuf
	{
		public bool HasReason;

		private uint _Reason;

		public bool HasSubscriberId;

		private SubscriberId _SubscriberId;

		public Invitation Invitation { get; set; }

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

		public SubscriberId SubscriberId
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

		public bool IsInitialized => true;

		public void SetInvitation(Invitation val)
		{
			Invitation = val;
		}

		public void SetReason(uint val)
		{
			Reason = val;
		}

		public void SetSubscriberId(SubscriberId val)
		{
			SubscriberId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Invitation.GetHashCode();
			if (HasReason)
			{
				hashCode ^= Reason.GetHashCode();
			}
			if (HasSubscriberId)
			{
				hashCode ^= SubscriberId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			InvitationRemovedNotification invitationRemovedNotification = obj as InvitationRemovedNotification;
			if (invitationRemovedNotification == null)
			{
				return false;
			}
			if (!Invitation.Equals(invitationRemovedNotification.Invitation))
			{
				return false;
			}
			if (HasReason != invitationRemovedNotification.HasReason || (HasReason && !Reason.Equals(invitationRemovedNotification.Reason)))
			{
				return false;
			}
			if (HasSubscriberId != invitationRemovedNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(invitationRemovedNotification.SubscriberId)))
			{
				return false;
			}
			return true;
		}

		public static InvitationRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationRemovedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static InvitationRemovedNotification Deserialize(Stream stream, InvitationRemovedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static InvitationRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			InvitationRemovedNotification invitationRemovedNotification = new InvitationRemovedNotification();
			DeserializeLengthDelimited(stream, invitationRemovedNotification);
			return invitationRemovedNotification;
		}

		public static InvitationRemovedNotification DeserializeLengthDelimited(Stream stream, InvitationRemovedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static InvitationRemovedNotification Deserialize(Stream stream, InvitationRemovedNotification instance, long limit)
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
					if (instance.Invitation == null)
					{
						instance.Invitation = Invitation.DeserializeLengthDelimited(stream);
					}
					else
					{
						Invitation.DeserializeLengthDelimited(stream, instance.Invitation);
					}
					continue;
				case 16:
					instance.Reason = ProtocolParser.ReadUInt32(stream);
					continue;
				case 26:
					if (instance.SubscriberId == null)
					{
						instance.SubscriberId = SubscriberId.DeserializeLengthDelimited(stream);
					}
					else
					{
						SubscriberId.DeserializeLengthDelimited(stream, instance.SubscriberId);
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

		public static void Serialize(Stream stream, InvitationRemovedNotification instance)
		{
			if (instance.Invitation == null)
			{
				throw new ArgumentNullException("Invitation", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Invitation.GetSerializedSize());
			Invitation.Serialize(stream, instance.Invitation);
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				SubscriberId.Serialize(stream, instance.SubscriberId);
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
			if (HasSubscriberId)
			{
				num++;
				uint serializedSize2 = SubscriberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1;
		}
	}
}
