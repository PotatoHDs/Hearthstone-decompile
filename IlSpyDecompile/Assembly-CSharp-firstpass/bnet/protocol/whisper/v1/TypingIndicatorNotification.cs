using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.Types;

namespace bnet.protocol.whisper.v1
{
	public class TypingIndicatorNotification : IProtoBuf
	{
		public bool HasSubscriberId;

		private AccountId _SubscriberId;

		public bool HasSenderId;

		private AccountId _SenderId;

		public bool HasAction;

		private TypingIndicator _Action;

		public AccountId SubscriberId
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

		public AccountId SenderId
		{
			get
			{
				return _SenderId;
			}
			set
			{
				_SenderId = value;
				HasSenderId = value != null;
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

		public void SetSubscriberId(AccountId val)
		{
			SubscriberId = val;
		}

		public void SetSenderId(AccountId val)
		{
			SenderId = val;
		}

		public void SetAction(TypingIndicator val)
		{
			Action = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSubscriberId)
			{
				num ^= SubscriberId.GetHashCode();
			}
			if (HasSenderId)
			{
				num ^= SenderId.GetHashCode();
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
			if (HasSubscriberId != typingIndicatorNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(typingIndicatorNotification.SubscriberId)))
			{
				return false;
			}
			if (HasSenderId != typingIndicatorNotification.HasSenderId || (HasSenderId && !SenderId.Equals(typingIndicatorNotification.SenderId)))
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
					if (instance.SubscriberId == null)
					{
						instance.SubscriberId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.SubscriberId);
					}
					continue;
				case 18:
					if (instance.SenderId == null)
					{
						instance.SenderId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.SenderId);
					}
					continue;
				case 24:
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
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasSenderId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SenderId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SenderId);
			}
			if (instance.HasAction)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Action);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSubscriberId)
			{
				num++;
				uint serializedSize = SubscriberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasSenderId)
			{
				num++;
				uint serializedSize2 = SenderId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
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
