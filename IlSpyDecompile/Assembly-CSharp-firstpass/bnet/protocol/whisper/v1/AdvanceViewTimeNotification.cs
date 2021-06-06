using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	public class AdvanceViewTimeNotification : IProtoBuf
	{
		public bool HasSubscriberId;

		private AccountId _SubscriberId;

		public bool HasSenderId;

		private AccountId _SenderId;

		public bool HasViewTime;

		private ulong _ViewTime;

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

		public ulong ViewTime
		{
			get
			{
				return _ViewTime;
			}
			set
			{
				_ViewTime = value;
				HasViewTime = true;
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

		public void SetViewTime(ulong val)
		{
			ViewTime = val;
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
			if (HasViewTime)
			{
				num ^= ViewTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AdvanceViewTimeNotification advanceViewTimeNotification = obj as AdvanceViewTimeNotification;
			if (advanceViewTimeNotification == null)
			{
				return false;
			}
			if (HasSubscriberId != advanceViewTimeNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(advanceViewTimeNotification.SubscriberId)))
			{
				return false;
			}
			if (HasSenderId != advanceViewTimeNotification.HasSenderId || (HasSenderId && !SenderId.Equals(advanceViewTimeNotification.SenderId)))
			{
				return false;
			}
			if (HasViewTime != advanceViewTimeNotification.HasViewTime || (HasViewTime && !ViewTime.Equals(advanceViewTimeNotification.ViewTime)))
			{
				return false;
			}
			return true;
		}

		public static AdvanceViewTimeNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AdvanceViewTimeNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AdvanceViewTimeNotification Deserialize(Stream stream, AdvanceViewTimeNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AdvanceViewTimeNotification DeserializeLengthDelimited(Stream stream)
		{
			AdvanceViewTimeNotification advanceViewTimeNotification = new AdvanceViewTimeNotification();
			DeserializeLengthDelimited(stream, advanceViewTimeNotification);
			return advanceViewTimeNotification;
		}

		public static AdvanceViewTimeNotification DeserializeLengthDelimited(Stream stream, AdvanceViewTimeNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AdvanceViewTimeNotification Deserialize(Stream stream, AdvanceViewTimeNotification instance, long limit)
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
					instance.ViewTime = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AdvanceViewTimeNotification instance)
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
			if (instance.HasViewTime)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.ViewTime);
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
			if (HasViewTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ViewTime);
			}
			return num;
		}
	}
}
