using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.sns.v1
{
	public class GetGoogleAuthTokenRequest : IProtoBuf
	{
		public bool HasAccount;

		private AccountId _Account;

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

		public bool IsInitialized => true;

		public void SetAccount(AccountId val)
		{
			Account = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccount)
			{
				num ^= Account.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGoogleAuthTokenRequest getGoogleAuthTokenRequest = obj as GetGoogleAuthTokenRequest;
			if (getGoogleAuthTokenRequest == null)
			{
				return false;
			}
			if (HasAccount != getGoogleAuthTokenRequest.HasAccount || (HasAccount && !Account.Equals(getGoogleAuthTokenRequest.Account)))
			{
				return false;
			}
			return true;
		}

		public static GetGoogleAuthTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGoogleAuthTokenRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetGoogleAuthTokenRequest Deserialize(Stream stream, GetGoogleAuthTokenRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetGoogleAuthTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGoogleAuthTokenRequest getGoogleAuthTokenRequest = new GetGoogleAuthTokenRequest();
			DeserializeLengthDelimited(stream, getGoogleAuthTokenRequest);
			return getGoogleAuthTokenRequest;
		}

		public static GetGoogleAuthTokenRequest DeserializeLengthDelimited(Stream stream, GetGoogleAuthTokenRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetGoogleAuthTokenRequest Deserialize(Stream stream, GetGoogleAuthTokenRequest instance, long limit)
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

		public static void Serialize(Stream stream, GetGoogleAuthTokenRequest instance)
		{
			if (instance.HasAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Account.GetSerializedSize());
				AccountId.Serialize(stream, instance.Account);
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
			return num;
		}
	}
}
