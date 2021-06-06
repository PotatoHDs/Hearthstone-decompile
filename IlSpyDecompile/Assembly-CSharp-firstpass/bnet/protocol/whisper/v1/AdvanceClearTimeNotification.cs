using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	public class AdvanceClearTimeNotification : IProtoBuf
	{
		public bool HasSubscriberId;

		private AccountId _SubscriberId;

		public bool HasSenderId;

		private AccountId _SenderId;

		public bool HasClearTime;

		private ulong _ClearTime;

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

		public ulong ClearTime
		{
			get
			{
				return _ClearTime;
			}
			set
			{
				_ClearTime = value;
				HasClearTime = true;
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

		public void SetClearTime(ulong val)
		{
			ClearTime = val;
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
			if (HasClearTime)
			{
				num ^= ClearTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AdvanceClearTimeNotification advanceClearTimeNotification = obj as AdvanceClearTimeNotification;
			if (advanceClearTimeNotification == null)
			{
				return false;
			}
			if (HasSubscriberId != advanceClearTimeNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(advanceClearTimeNotification.SubscriberId)))
			{
				return false;
			}
			if (HasSenderId != advanceClearTimeNotification.HasSenderId || (HasSenderId && !SenderId.Equals(advanceClearTimeNotification.SenderId)))
			{
				return false;
			}
			if (HasClearTime != advanceClearTimeNotification.HasClearTime || (HasClearTime && !ClearTime.Equals(advanceClearTimeNotification.ClearTime)))
			{
				return false;
			}
			return true;
		}

		public static AdvanceClearTimeNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AdvanceClearTimeNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AdvanceClearTimeNotification Deserialize(Stream stream, AdvanceClearTimeNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AdvanceClearTimeNotification DeserializeLengthDelimited(Stream stream)
		{
			AdvanceClearTimeNotification advanceClearTimeNotification = new AdvanceClearTimeNotification();
			DeserializeLengthDelimited(stream, advanceClearTimeNotification);
			return advanceClearTimeNotification;
		}

		public static AdvanceClearTimeNotification DeserializeLengthDelimited(Stream stream, AdvanceClearTimeNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AdvanceClearTimeNotification Deserialize(Stream stream, AdvanceClearTimeNotification instance, long limit)
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
					instance.ClearTime = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AdvanceClearTimeNotification instance)
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
			if (instance.HasClearTime)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.ClearTime);
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
			if (HasClearTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ClearTime);
			}
			return num;
		}
	}
}
