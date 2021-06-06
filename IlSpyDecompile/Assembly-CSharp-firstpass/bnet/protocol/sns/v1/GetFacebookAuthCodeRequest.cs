using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.sns.v1
{
	public class GetFacebookAuthCodeRequest : IProtoBuf
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
			GetFacebookAuthCodeRequest getFacebookAuthCodeRequest = obj as GetFacebookAuthCodeRequest;
			if (getFacebookAuthCodeRequest == null)
			{
				return false;
			}
			if (HasAccount != getFacebookAuthCodeRequest.HasAccount || (HasAccount && !Account.Equals(getFacebookAuthCodeRequest.Account)))
			{
				return false;
			}
			return true;
		}

		public static GetFacebookAuthCodeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFacebookAuthCodeRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetFacebookAuthCodeRequest Deserialize(Stream stream, GetFacebookAuthCodeRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetFacebookAuthCodeRequest DeserializeLengthDelimited(Stream stream)
		{
			GetFacebookAuthCodeRequest getFacebookAuthCodeRequest = new GetFacebookAuthCodeRequest();
			DeserializeLengthDelimited(stream, getFacebookAuthCodeRequest);
			return getFacebookAuthCodeRequest;
		}

		public static GetFacebookAuthCodeRequest DeserializeLengthDelimited(Stream stream, GetFacebookAuthCodeRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetFacebookAuthCodeRequest Deserialize(Stream stream, GetFacebookAuthCodeRequest instance, long limit)
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

		public static void Serialize(Stream stream, GetFacebookAuthCodeRequest instance)
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
