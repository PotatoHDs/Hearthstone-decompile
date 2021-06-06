using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	public class SendInvitationOptions : IProtoBuf
	{
		public bool HasChannelId;

		private ChannelId _ChannelId;

		public bool HasTargetId;

		private GameAccountHandle _TargetId;

		public bool HasSlot;

		private ChannelSlot _Slot;

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

		public GameAccountHandle TargetId
		{
			get
			{
				return _TargetId;
			}
			set
			{
				_TargetId = value;
				HasTargetId = value != null;
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

		public bool IsInitialized => true;

		public void SetChannelId(ChannelId val)
		{
			ChannelId = val;
		}

		public void SetTargetId(GameAccountHandle val)
		{
			TargetId = val;
		}

		public void SetSlot(ChannelSlot val)
		{
			Slot = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			if (HasTargetId)
			{
				num ^= TargetId.GetHashCode();
			}
			if (HasSlot)
			{
				num ^= Slot.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SendInvitationOptions sendInvitationOptions = obj as SendInvitationOptions;
			if (sendInvitationOptions == null)
			{
				return false;
			}
			if (HasChannelId != sendInvitationOptions.HasChannelId || (HasChannelId && !ChannelId.Equals(sendInvitationOptions.ChannelId)))
			{
				return false;
			}
			if (HasTargetId != sendInvitationOptions.HasTargetId || (HasTargetId && !TargetId.Equals(sendInvitationOptions.TargetId)))
			{
				return false;
			}
			if (HasSlot != sendInvitationOptions.HasSlot || (HasSlot && !Slot.Equals(sendInvitationOptions.Slot)))
			{
				return false;
			}
			return true;
		}

		public static SendInvitationOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendInvitationOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SendInvitationOptions Deserialize(Stream stream, SendInvitationOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SendInvitationOptions DeserializeLengthDelimited(Stream stream)
		{
			SendInvitationOptions sendInvitationOptions = new SendInvitationOptions();
			DeserializeLengthDelimited(stream, sendInvitationOptions);
			return sendInvitationOptions;
		}

		public static SendInvitationOptions DeserializeLengthDelimited(Stream stream, SendInvitationOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SendInvitationOptions Deserialize(Stream stream, SendInvitationOptions instance, long limit)
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
					if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 18:
					if (instance.TargetId == null)
					{
						instance.TargetId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.TargetId);
					}
					continue;
				case 26:
					if (instance.Slot == null)
					{
						instance.Slot = ChannelSlot.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelSlot.DeserializeLengthDelimited(stream, instance.Slot);
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

		public static void Serialize(Stream stream, SendInvitationOptions instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasTargetId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.TargetId);
			}
			if (instance.HasSlot)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Slot.GetSerializedSize());
				ChannelSlot.Serialize(stream, instance.Slot);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasChannelId)
			{
				num++;
				uint serializedSize = ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasTargetId)
			{
				num++;
				uint serializedSize2 = TargetId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasSlot)
			{
				num++;
				uint serializedSize3 = Slot.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
