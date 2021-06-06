using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	public class SendMessageNotification : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasSubscriberId;

		private GameAccountHandle _SubscriberId;

		public bool HasChannelId;

		private ChannelId _ChannelId;

		public bool HasMessage;

		private ChannelMessage _Message;

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

		public ChannelMessage Message
		{
			get
			{
				return _Message;
			}
			set
			{
				_Message = value;
				HasMessage = value != null;
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

		public void SetMessage(ChannelMessage val)
		{
			Message = val;
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
			if (HasMessage)
			{
				num ^= Message.GetHashCode();
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
			if (HasSubscriberId != sendMessageNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(sendMessageNotification.SubscriberId)))
			{
				return false;
			}
			if (HasChannelId != sendMessageNotification.HasChannelId || (HasChannelId && !ChannelId.Equals(sendMessageNotification.ChannelId)))
			{
				return false;
			}
			if (HasMessage != sendMessageNotification.HasMessage || (HasMessage && !Message.Equals(sendMessageNotification.Message)))
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
					if (instance.Message == null)
					{
						instance.Message = ChannelMessage.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelMessage.DeserializeLengthDelimited(stream, instance.Message);
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
			if (instance.HasMessage)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Message.GetSerializedSize());
				ChannelMessage.Serialize(stream, instance.Message);
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
			if (HasMessage)
			{
				num++;
				uint serializedSize4 = Message.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}
	}
}
