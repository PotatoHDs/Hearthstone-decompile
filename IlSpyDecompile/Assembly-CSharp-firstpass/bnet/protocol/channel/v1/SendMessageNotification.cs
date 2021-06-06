using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	public class SendMessageNotification : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasRequiredPrivileges;

		private ulong _RequiredPrivileges;

		public bool HasBattleTag;

		private string _BattleTag;

		public bool HasChannelId;

		private ChannelId _ChannelId;

		public bool HasSubscriberId;

		private SubscriberId _SubscriberId;

		public EntityId AgentId
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

		public Message Message { get; set; }

		public ulong RequiredPrivileges
		{
			get
			{
				return _RequiredPrivileges;
			}
			set
			{
				_RequiredPrivileges = value;
				HasRequiredPrivileges = true;
			}
		}

		public string BattleTag
		{
			get
			{
				return _BattleTag;
			}
			set
			{
				_BattleTag = value;
				HasBattleTag = value != null;
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

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetMessage(Message val)
		{
			Message = val;
		}

		public void SetRequiredPrivileges(ulong val)
		{
			RequiredPrivileges = val;
		}

		public void SetBattleTag(string val)
		{
			BattleTag = val;
		}

		public void SetChannelId(ChannelId val)
		{
			ChannelId = val;
		}

		public void SetSubscriberId(SubscriberId val)
		{
			SubscriberId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			num ^= Message.GetHashCode();
			if (HasRequiredPrivileges)
			{
				num ^= RequiredPrivileges.GetHashCode();
			}
			if (HasBattleTag)
			{
				num ^= BattleTag.GetHashCode();
			}
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			if (HasSubscriberId)
			{
				num ^= SubscriberId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SendMessageNotification sendMessageNotification = obj as SendMessageNotification;
			if (sendMessageNotification == null)
			{
				return false;
			}
			if (HasAgentId != sendMessageNotification.HasAgentId || (HasAgentId && !AgentId.Equals(sendMessageNotification.AgentId)))
			{
				return false;
			}
			if (!Message.Equals(sendMessageNotification.Message))
			{
				return false;
			}
			if (HasRequiredPrivileges != sendMessageNotification.HasRequiredPrivileges || (HasRequiredPrivileges && !RequiredPrivileges.Equals(sendMessageNotification.RequiredPrivileges)))
			{
				return false;
			}
			if (HasBattleTag != sendMessageNotification.HasBattleTag || (HasBattleTag && !BattleTag.Equals(sendMessageNotification.BattleTag)))
			{
				return false;
			}
			if (HasChannelId != sendMessageNotification.HasChannelId || (HasChannelId && !ChannelId.Equals(sendMessageNotification.ChannelId)))
			{
				return false;
			}
			if (HasSubscriberId != sendMessageNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(sendMessageNotification.SubscriberId)))
			{
				return false;
			}
			return true;
		}

		public static SendMessageNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendMessageNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SendMessageNotification Deserialize(Stream stream, SendMessageNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SendMessageNotification DeserializeLengthDelimited(Stream stream)
		{
			SendMessageNotification sendMessageNotification = new SendMessageNotification();
			DeserializeLengthDelimited(stream, sendMessageNotification);
			return sendMessageNotification;
		}

		public static SendMessageNotification DeserializeLengthDelimited(Stream stream, SendMessageNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SendMessageNotification Deserialize(Stream stream, SendMessageNotification instance, long limit)
		{
			instance.RequiredPrivileges = 0uL;
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
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 18:
					if (instance.Message == null)
					{
						instance.Message = Message.DeserializeLengthDelimited(stream);
					}
					else
					{
						Message.DeserializeLengthDelimited(stream, instance.Message);
					}
					continue;
				case 24:
					instance.RequiredPrivileges = ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.BattleTag = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 50:
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

		public static void Serialize(Stream stream, SendMessageNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.Message == null)
			{
				throw new ArgumentNullException("Message", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Message.GetSerializedSize());
			Message.Serialize(stream, instance.Message);
			if (instance.HasRequiredPrivileges)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.RequiredPrivileges);
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				SubscriberId.Serialize(stream, instance.SubscriberId);
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
			uint serializedSize2 = Message.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (HasRequiredPrivileges)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(RequiredPrivileges);
			}
			if (HasBattleTag)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasChannelId)
			{
				num++;
				uint serializedSize3 = ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasSubscriberId)
			{
				num++;
				uint serializedSize4 = SubscriberId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num + 1;
		}
	}
}
