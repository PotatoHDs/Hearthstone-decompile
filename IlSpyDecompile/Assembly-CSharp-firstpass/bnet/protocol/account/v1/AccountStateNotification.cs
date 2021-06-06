using System.IO;

namespace bnet.protocol.account.v1
{
	public class AccountStateNotification : IProtoBuf
	{
		public bool HasAccountState;

		private AccountState _AccountState;

		public bool HasSubscriberId;

		private ulong _SubscriberId;

		public bool HasAccountTags;

		private AccountFieldTags _AccountTags;

		public bool HasSubscriptionCompleted;

		private bool _SubscriptionCompleted;

		public AccountState AccountState
		{
			get
			{
				return _AccountState;
			}
			set
			{
				_AccountState = value;
				HasAccountState = value != null;
			}
		}

		public ulong SubscriberId
		{
			get
			{
				return _SubscriberId;
			}
			set
			{
				_SubscriberId = value;
				HasSubscriberId = true;
			}
		}

		public AccountFieldTags AccountTags
		{
			get
			{
				return _AccountTags;
			}
			set
			{
				_AccountTags = value;
				HasAccountTags = value != null;
			}
		}

		public bool SubscriptionCompleted
		{
			get
			{
				return _SubscriptionCompleted;
			}
			set
			{
				_SubscriptionCompleted = value;
				HasSubscriptionCompleted = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAccountState(AccountState val)
		{
			AccountState = val;
		}

		public void SetSubscriberId(ulong val)
		{
			SubscriberId = val;
		}

		public void SetAccountTags(AccountFieldTags val)
		{
			AccountTags = val;
		}

		public void SetSubscriptionCompleted(bool val)
		{
			SubscriptionCompleted = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccountState)
			{
				num ^= AccountState.GetHashCode();
			}
			if (HasSubscriberId)
			{
				num ^= SubscriberId.GetHashCode();
			}
			if (HasAccountTags)
			{
				num ^= AccountTags.GetHashCode();
			}
			if (HasSubscriptionCompleted)
			{
				num ^= SubscriptionCompleted.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountStateNotification accountStateNotification = obj as AccountStateNotification;
			if (accountStateNotification == null)
			{
				return false;
			}
			if (HasAccountState != accountStateNotification.HasAccountState || (HasAccountState && !AccountState.Equals(accountStateNotification.AccountState)))
			{
				return false;
			}
			if (HasSubscriberId != accountStateNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(accountStateNotification.SubscriberId)))
			{
				return false;
			}
			if (HasAccountTags != accountStateNotification.HasAccountTags || (HasAccountTags && !AccountTags.Equals(accountStateNotification.AccountTags)))
			{
				return false;
			}
			if (HasSubscriptionCompleted != accountStateNotification.HasSubscriptionCompleted || (HasSubscriptionCompleted && !SubscriptionCompleted.Equals(accountStateNotification.SubscriptionCompleted)))
			{
				return false;
			}
			return true;
		}

		public static AccountStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountStateNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AccountStateNotification Deserialize(Stream stream, AccountStateNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AccountStateNotification DeserializeLengthDelimited(Stream stream)
		{
			AccountStateNotification accountStateNotification = new AccountStateNotification();
			DeserializeLengthDelimited(stream, accountStateNotification);
			return accountStateNotification;
		}

		public static AccountStateNotification DeserializeLengthDelimited(Stream stream, AccountStateNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AccountStateNotification Deserialize(Stream stream, AccountStateNotification instance, long limit)
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
					if (instance.AccountState == null)
					{
						instance.AccountState = AccountState.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountState.DeserializeLengthDelimited(stream, instance.AccountState);
					}
					continue;
				case 16:
					instance.SubscriberId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					if (instance.AccountTags == null)
					{
						instance.AccountTags = AccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountFieldTags.DeserializeLengthDelimited(stream, instance.AccountTags);
					}
					continue;
				case 32:
					instance.SubscriptionCompleted = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, AccountStateNotification instance)
		{
			if (instance.HasAccountState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountState.GetSerializedSize());
				AccountState.Serialize(stream, instance.AccountState);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.SubscriberId);
			}
			if (instance.HasAccountTags)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AccountTags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.AccountTags);
			}
			if (instance.HasSubscriptionCompleted)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.SubscriptionCompleted);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAccountState)
			{
				num++;
				uint serializedSize = AccountState.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasSubscriberId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(SubscriberId);
			}
			if (HasAccountTags)
			{
				num++;
				uint serializedSize2 = AccountTags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasSubscriptionCompleted)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
