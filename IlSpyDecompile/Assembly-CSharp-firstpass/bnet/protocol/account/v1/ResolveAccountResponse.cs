using System.IO;

namespace bnet.protocol.account.v1
{
	public class ResolveAccountResponse : IProtoBuf
	{
		public bool HasId;

		private AccountId _Id;

		public AccountId Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				HasId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetId(AccountId val)
		{
			Id = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ResolveAccountResponse resolveAccountResponse = obj as ResolveAccountResponse;
			if (resolveAccountResponse == null)
			{
				return false;
			}
			if (HasId != resolveAccountResponse.HasId || (HasId && !Id.Equals(resolveAccountResponse.Id)))
			{
				return false;
			}
			return true;
		}

		public static ResolveAccountResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ResolveAccountResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ResolveAccountResponse Deserialize(Stream stream, ResolveAccountResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ResolveAccountResponse DeserializeLengthDelimited(Stream stream)
		{
			ResolveAccountResponse resolveAccountResponse = new ResolveAccountResponse();
			DeserializeLengthDelimited(stream, resolveAccountResponse);
			return resolveAccountResponse;
		}

		public static ResolveAccountResponse DeserializeLengthDelimited(Stream stream, ResolveAccountResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ResolveAccountResponse Deserialize(Stream stream, ResolveAccountResponse instance, long limit)
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
				case 98:
					if (instance.Id == null)
					{
						instance.Id = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.Id);
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

		public static void Serialize(Stream stream, ResolveAccountResponse instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(98);
				ProtocolParser.WriteUInt32(stream, instance.Id.GetSerializedSize());
				AccountId.Serialize(stream, instance.Id);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasId)
			{
				num++;
				uint serializedSize = Id.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
