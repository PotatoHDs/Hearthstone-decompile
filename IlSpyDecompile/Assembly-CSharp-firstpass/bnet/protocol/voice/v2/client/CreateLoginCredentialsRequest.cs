using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.voice.v2.client
{
	public class CreateLoginCredentialsRequest : IProtoBuf
	{
		public bool HasAccountId;

		private AccountId _AccountId;

		public AccountId AccountId
		{
			get
			{
				return _AccountId;
			}
			set
			{
				_AccountId = value;
				HasAccountId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAccountId(AccountId val)
		{
			AccountId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccountId)
			{
				num ^= AccountId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateLoginCredentialsRequest createLoginCredentialsRequest = obj as CreateLoginCredentialsRequest;
			if (createLoginCredentialsRequest == null)
			{
				return false;
			}
			if (HasAccountId != createLoginCredentialsRequest.HasAccountId || (HasAccountId && !AccountId.Equals(createLoginCredentialsRequest.AccountId)))
			{
				return false;
			}
			return true;
		}

		public static CreateLoginCredentialsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateLoginCredentialsRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateLoginCredentialsRequest Deserialize(Stream stream, CreateLoginCredentialsRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateLoginCredentialsRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateLoginCredentialsRequest createLoginCredentialsRequest = new CreateLoginCredentialsRequest();
			DeserializeLengthDelimited(stream, createLoginCredentialsRequest);
			return createLoginCredentialsRequest;
		}

		public static CreateLoginCredentialsRequest DeserializeLengthDelimited(Stream stream, CreateLoginCredentialsRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateLoginCredentialsRequest Deserialize(Stream stream, CreateLoginCredentialsRequest instance, long limit)
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
					if (instance.AccountId == null)
					{
						instance.AccountId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.AccountId);
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

		public static void Serialize(Stream stream, CreateLoginCredentialsRequest instance)
		{
			if (instance.HasAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				AccountId.Serialize(stream, instance.AccountId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAccountId)
			{
				num++;
				uint serializedSize = AccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
