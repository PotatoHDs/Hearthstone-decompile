using System.IO;

namespace bnet.protocol.channel.v2
{
	public class ChannelInvitation : IProtoBuf
	{
		public bool HasId;

		private ulong _Id;

		public bool HasInviter;

		private MemberDescription _Inviter;

		public bool HasInvitee;

		private MemberDescription _Invitee;

		public bool HasChannel;

		private ChannelDescription _Channel;

		public bool HasSlot;

		private ChannelSlot _Slot;

		public bool HasCreationTime;

		private ulong _CreationTime;

		public bool HasExpirationTime;

		private ulong _ExpirationTime;

		public ulong Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				HasId = true;
			}
		}

		public MemberDescription Inviter
		{
			get
			{
				return _Inviter;
			}
			set
			{
				_Inviter = value;
				HasInviter = value != null;
			}
		}

		public MemberDescription Invitee
		{
			get
			{
				return _Invitee;
			}
			set
			{
				_Invitee = value;
				HasInvitee = value != null;
			}
		}

		public ChannelDescription Channel
		{
			get
			{
				return _Channel;
			}
			set
			{
				_Channel = value;
				HasChannel = value != null;
			}
		}

		public ChannelSlot Slot
		{
			get
			{
				return _Slot;
			}
			set
			{
				_Slot = value;
				HasSlot = value != null;
			}
		}

		public ulong CreationTime
		{
			get
			{
				return _CreationTime;
			}
			set
			{
				_CreationTime = value;
				HasCreationTime = true;
			}
		}

		public ulong ExpirationTime
		{
			get
			{
				return _ExpirationTime;
			}
			set
			{
				_ExpirationTime = value;
				HasExpirationTime = true;
			}
		}

		public bool IsInitialized => true;

		public void SetId(ulong val)
		{
			Id = val;
		}

		public void SetInviter(MemberDescription val)
		{
			Inviter = val;
		}

		public void SetInvitee(MemberDescription val)
		{
			Invitee = val;
		}

		public void SetChannel(ChannelDescription val)
		{
			Channel = val;
		}

		public void SetSlot(ChannelSlot val)
		{
			Slot = val;
		}

		public void SetCreationTime(ulong val)
		{
			CreationTime = val;
		}

		public void SetExpirationTime(ulong val)
		{
			ExpirationTime = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			if (HasInviter)
			{
				num ^= Inviter.GetHashCode();
			}
			if (HasInvitee)
			{
				num ^= Invitee.GetHashCode();
			}
			if (HasChannel)
			{
				num ^= Channel.GetHashCode();
			}
			if (HasSlot)
			{
				num ^= Slot.GetHashCode();
			}
			if (HasCreationTime)
			{
				num ^= CreationTime.GetHashCode();
			}
			if (HasExpirationTime)
			{
				num ^= ExpirationTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelInvitation channelInvitation = obj as ChannelInvitation;
			if (channelInvitation == null)
			{
				return false;
			}
			if (HasId != channelInvitation.HasId || (HasId && !Id.Equals(channelInvitation.Id)))
			{
				return false;
			}
			if (HasInviter != channelInvitation.HasInviter || (HasInviter && !Inviter.Equals(channelInvitation.Inviter)))
			{
				return false;
			}
			if (HasInvitee != channelInvitation.HasInvitee || (HasInvitee && !Invitee.Equals(channelInvitation.Invitee)))
			{
				return false;
			}
			if (HasChannel != channelInvitation.HasChannel || (HasChannel && !Channel.Equals(channelInvitation.Channel)))
			{
				return false;
			}
			if (HasSlot != channelInvitation.HasSlot || (HasSlot && !Slot.Equals(channelInvitation.Slot)))
			{
				return false;
			}
			if (HasCreationTime != channelInvitation.HasCreationTime || (HasCreationTime && !CreationTime.Equals(channelInvitation.CreationTime)))
			{
				return false;
			}
			if (HasExpirationTime != channelInvitation.HasExpirationTime || (HasExpirationTime && !ExpirationTime.Equals(channelInvitation.ExpirationTime)))
			{
				return false;
			}
			return true;
		}

		public static ChannelInvitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelInvitation>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelInvitation Deserialize(Stream stream, ChannelInvitation instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelInvitation DeserializeLengthDelimited(Stream stream)
		{
			ChannelInvitation channelInvitation = new ChannelInvitation();
			DeserializeLengthDelimited(stream, channelInvitation);
			return channelInvitation;
		}

		public static ChannelInvitation DeserializeLengthDelimited(Stream stream, ChannelInvitation instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelInvitation Deserialize(Stream stream, ChannelInvitation instance, long limit)
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
				case 9:
					instance.Id = binaryReader.ReadUInt64();
					continue;
				case 18:
					if (instance.Inviter == null)
					{
						instance.Inviter = MemberDescription.DeserializeLengthDelimited(stream);
					}
					else
					{
						MemberDescription.DeserializeLengthDelimited(stream, instance.Inviter);
					}
					continue;
				case 26:
					if (instance.Invitee == null)
					{
						instance.Invitee = MemberDescription.DeserializeLengthDelimited(stream);
					}
					else
					{
						MemberDescription.DeserializeLengthDelimited(stream, instance.Invitee);
					}
					continue;
				case 34:
					if (instance.Channel == null)
					{
						instance.Channel = ChannelDescription.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelDescription.DeserializeLengthDelimited(stream, instance.Channel);
					}
					continue;
				case 42:
					if (instance.Slot == null)
					{
						instance.Slot = ChannelSlot.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelSlot.DeserializeLengthDelimited(stream, instance.Slot);
					}
					continue;
				case 56:
					instance.CreationTime = ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.ExpirationTime = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ChannelInvitation instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.Id);
			}
			if (instance.HasInviter)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Inviter.GetSerializedSize());
				MemberDescription.Serialize(stream, instance.Inviter);
			}
			if (instance.HasInvitee)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Invitee.GetSerializedSize());
				MemberDescription.Serialize(stream, instance.Invitee);
			}
			if (instance.HasChannel)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Channel.GetSerializedSize());
				ChannelDescription.Serialize(stream, instance.Channel);
			}
			if (instance.HasSlot)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Slot.GetSerializedSize());
				ChannelSlot.Serialize(stream, instance.Slot);
			}
			if (instance.HasCreationTime)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.CreationTime);
			}
			if (instance.HasExpirationTime)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, instance.ExpirationTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasId)
			{
				num++;
				num += 8;
			}
			if (HasInviter)
			{
				num++;
				uint serializedSize = Inviter.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasInvitee)
			{
				num++;
				uint serializedSize2 = Invitee.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasChannel)
			{
				num++;
				uint serializedSize3 = Channel.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasSlot)
			{
				num++;
				uint serializedSize4 = Slot.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasCreationTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(CreationTime);
			}
			if (HasExpirationTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ExpirationTime);
			}
			return num;
		}
	}
}
