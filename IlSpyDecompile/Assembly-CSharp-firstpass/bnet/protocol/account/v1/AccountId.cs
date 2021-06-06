using System.IO;

namespace bnet.protocol.account.v1
{
	public class AccountId : IProtoBuf
	{
		public uint Id { get; set; }

		public bool IsInitialized => true;

		public void SetId(uint val)
		{
			Id = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Id.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			AccountId accountId = obj as AccountId;
			if (accountId == null)
			{
				return false;
			}
			if (!Id.Equals(accountId.Id))
			{
				return false;
			}
			return true;
		}

		public static AccountId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountId>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AccountId Deserialize(Stream stream, AccountId instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AccountId DeserializeLengthDelimited(Stream stream)
		{
			AccountId accountId = new AccountId();
			DeserializeLengthDelimited(stream, accountId);
			return accountId;
		}

		public static AccountId DeserializeLengthDelimited(Stream stream, AccountId instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AccountId Deserialize(Stream stream, AccountId instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 13:
					instance.Id = binaryReader.ReadUInt32();
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

		public static void Serialize(Stream stream, AccountId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Id);
		}

		public uint GetSerializedSize()
		{
			return 5u;
		}
	}
}
