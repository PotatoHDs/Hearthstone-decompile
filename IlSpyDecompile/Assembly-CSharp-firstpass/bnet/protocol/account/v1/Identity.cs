using System.IO;

namespace bnet.protocol.account.v1
{
	public class Identity : IProtoBuf
	{
		public bool HasAccount;

		private AccountId _Account;

		public bool HasGameAccount;

		private GameAccountHandle _GameAccount;

		public AccountId Account
		{
			get
			{
				return _Account;
			}
			set
			{
				_Account = value;
				HasAccount = value != null;
			}
		}

		public GameAccountHandle GameAccount
		{
			get
			{
				return _GameAccount;
			}
			set
			{
				_GameAccount = value;
				HasGameAccount = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAccount(AccountId val)
		{
			Account = val;
		}

		public void SetGameAccount(GameAccountHandle val)
		{
			GameAccount = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccount)
			{
				num ^= Account.GetHashCode();
			}
			if (HasGameAccount)
			{
				num ^= GameAccount.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Identity identity = obj as Identity;
			if (identity == null)
			{
				return false;
			}
			if (HasAccount != identity.HasAccount || (HasAccount && !Account.Equals(identity.Account)))
			{
				return false;
			}
			if (HasGameAccount != identity.HasGameAccount || (HasGameAccount && !GameAccount.Equals(identity.GameAccount)))
			{
				return false;
			}
			return true;
		}

		public static Identity ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Identity>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Identity Deserialize(Stream stream, Identity instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Identity DeserializeLengthDelimited(Stream stream)
		{
			Identity identity = new Identity();
			DeserializeLengthDelimited(stream, identity);
			return identity;
		}

		public static Identity DeserializeLengthDelimited(Stream stream, Identity instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Identity Deserialize(Stream stream, Identity instance, long limit)
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
					if (instance.Account == null)
					{
						instance.Account = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.Account);
					}
					continue;
				case 18:
					if (instance.GameAccount == null)
					{
						instance.GameAccount = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.GameAccount);
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

		public static void Serialize(Stream stream, Identity instance)
		{
			if (instance.HasAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Account.GetSerializedSize());
				AccountId.Serialize(stream, instance.Account);
			}
			if (instance.HasGameAccount)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAccount)
			{
				num++;
				uint serializedSize = Account.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasGameAccount)
			{
				num++;
				uint serializedSize2 = GameAccount.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
