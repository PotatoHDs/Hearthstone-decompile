using System.IO;

namespace bnet.protocol.account.v1
{
	public class AccountStateTagged : IProtoBuf
	{
		public bool HasAccountState;

		private AccountState _AccountState;

		public bool HasAccountTags;

		private AccountFieldTags _AccountTags;

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

		public bool IsInitialized => true;

		public void SetAccountState(AccountState val)
		{
			AccountState = val;
		}

		public void SetAccountTags(AccountFieldTags val)
		{
			AccountTags = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccountState)
			{
				num ^= AccountState.GetHashCode();
			}
			if (HasAccountTags)
			{
				num ^= AccountTags.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountStateTagged accountStateTagged = obj as AccountStateTagged;
			if (accountStateTagged == null)
			{
				return false;
			}
			if (HasAccountState != accountStateTagged.HasAccountState || (HasAccountState && !AccountState.Equals(accountStateTagged.AccountState)))
			{
				return false;
			}
			if (HasAccountTags != accountStateTagged.HasAccountTags || (HasAccountTags && !AccountTags.Equals(accountStateTagged.AccountTags)))
			{
				return false;
			}
			return true;
		}

		public static AccountStateTagged ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountStateTagged>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AccountStateTagged Deserialize(Stream stream, AccountStateTagged instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AccountStateTagged DeserializeLengthDelimited(Stream stream)
		{
			AccountStateTagged accountStateTagged = new AccountStateTagged();
			DeserializeLengthDelimited(stream, accountStateTagged);
			return accountStateTagged;
		}

		public static AccountStateTagged DeserializeLengthDelimited(Stream stream, AccountStateTagged instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AccountStateTagged Deserialize(Stream stream, AccountStateTagged instance, long limit)
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
				case 18:
					if (instance.AccountTags == null)
					{
						instance.AccountTags = AccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountFieldTags.DeserializeLengthDelimited(stream, instance.AccountTags);
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

		public static void Serialize(Stream stream, AccountStateTagged instance)
		{
			if (instance.HasAccountState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountState.GetSerializedSize());
				AccountState.Serialize(stream, instance.AccountState);
			}
			if (instance.HasAccountTags)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AccountTags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.AccountTags);
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
			if (HasAccountTags)
			{
				num++;
				uint serializedSize2 = AccountTags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
