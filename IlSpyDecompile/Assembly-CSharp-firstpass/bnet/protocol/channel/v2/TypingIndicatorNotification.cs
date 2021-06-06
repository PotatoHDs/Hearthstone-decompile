using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.Types;

namespace bnet.protocol.channel.v2
{
	public class TypingIndicatorNotification : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasSubscriberId;

		private GameAccountHandle _SubscriberId;

		public bool HasChannelId;

		private ChannelId _ChannelId;

		public bool HasAuthorId;

		private GameAccountHandle _AuthorId;

		public bool HasEpoch;

		private ulong _Epoch;

		public bool HasAction;

		private TypingIndicator _Action;

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

		public GameAccountHandle AuthorId
		{
			get
			{
				return _AuthorId;
			}
			set
			{
				_AuthorId = value;
				HasAuthorId = value != null;
			}
		}

		public ulong Epoch
		{
			get
			{
				return _Epoch;
			}
			set
			{
				_Epoch = value;
				HasEpoch = true;
			}
		}

		public TypingIndicator Action
		{
			get
			{
				return _Action;
			}
			set
			{
				_Action = value;
				HasAction = true;
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

		public void SetAuthorId(GameAccountHandle val)
		{
			AuthorId = val;
		}

		public void SetEpoch(ulong val)
		{
			Epoch = val;
		}

		public void SetAction(TypingIndicator val)
		{
			Action = val;
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
			if (HasAuthorId)
			{
				num ^= AuthorId.GetHashCode();
			}
			if (HasEpoch)
			{
				num ^= Epoch.GetHashCode();
			}
			if (HasAction)
			{
				num ^= Action.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			TypingIndicatorNotification typingIndicatorNotification = obj as TypingIndicatorNotification;
			if (typingIndicatorNotification == null)
			{
				return false;
			}
			if (HasAgentId != typingIndicatorNotification.HasAgentId || (HasAgentId && !AgentId.Equals(typingIndicatorNotification.AgentId)))
			{
				return false;
			}
			if (HasSubscriberId != typingIndicatorNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(typingIndicatorNotification.SubscriberId)))
			{
				return false;
			}
			if (HasChannelId != typingIndicatorNotification.HasChannelId || (HasChannelId && !ChannelId.Equals(typingIndicatorNotification.ChannelId)))
			{
				return false;
			}
			if (HasAuthorId != typingIndicatorNotification.HasAuthorId || (HasAuthorId && !AuthorId.Equals(typingIndicatorNotification.AuthorId)))
			{
				return false;
			}
			if (HasEpoch != typingIndicatorNotification.HasEpoch || (HasEpoch && !Epoch.Equals(typingIndicatorNotification.Epoch)))
			{
				return false;
			}
			if (HasAction != typingIndicatorNotification.HasAction || (HasAction && !Action.Equals(typingIndicatorNotification.Action)))
			{
				return false;
			}
			return true;
		}

		public static TypingIndicatorNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<TypingIndicatorNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TypingIndicatorNotification Deserialize(Stream stream, TypingIndicatorNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TypingIndicatorNotification DeserializeLengthDelimited(Stream stream)
		{
			TypingIndicatorNotification typingIndicatorNotification = new TypingIndicatorNotification();
			DeserializeLengthDelimited(stream, typingIndicatorNotification);
			return typingIndicatorNotification;
		}

		public static TypingIndicatorNotification DeserializeLengthDelimited(Stream stream, TypingIndicatorNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TypingIndicatorNotification Deserialize(Stream stream, TypingIndicatorNotification instance, long limit)
		{
			instance.Action = TypingIndicator.TYPING_START;
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
					if (instance.AuthorId == null)
					{
						instance.AuthorId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.AuthorId);
					}
					continue;
				case 40:
					instance.Epoch = ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.Action = (TypingIndicator)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, TypingIndicatorNotification instance)
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
			if (instance.HasAuthorId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.AuthorId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AuthorId);
			}
			if (instance.HasEpoch)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.Epoch);
			}
			if (instance.HasAction)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Action);
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
			if (HasAuthorId)
			{
				num++;
				uint serializedSize4 = AuthorId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasEpoch)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Epoch);
			}
			if (HasAction)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Action);
			}
			return num;
		}
	}
}
