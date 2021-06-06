using System.IO;

namespace bnet.protocol.account.v1
{
	public class GetGameTimeRemainingInfoRequest : IProtoBuf
	{
		public bool HasGameAccountId;

		private EntityId _GameAccountId;

		public bool HasAccountId;

		private EntityId _AccountId;

		public EntityId GameAccountId
		{
			get
			{
				return _GameAccountId;
			}
			set
			{
				_GameAccountId = value;
				HasGameAccountId = value != null;
			}
		}

		public EntityId AccountId
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

		public void SetGameAccountId(EntityId val)
		{
			GameAccountId = val;
		}

		public void SetAccountId(EntityId val)
		{
			AccountId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameAccountId)
			{
				num ^= GameAccountId.GetHashCode();
			}
			if (HasAccountId)
			{
				num ^= AccountId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGameTimeRemainingInfoRequest getGameTimeRemainingInfoRequest = obj as GetGameTimeRemainingInfoRequest;
			if (getGameTimeRemainingInfoRequest == null)
			{
				return false;
			}
			if (HasGameAccountId != getGameTimeRemainingInfoRequest.HasGameAccountId || (HasGameAccountId && !GameAccountId.Equals(getGameTimeRemainingInfoRequest.GameAccountId)))
			{
				return false;
			}
			if (HasAccountId != getGameTimeRemainingInfoRequest.HasAccountId || (HasAccountId && !AccountId.Equals(getGameTimeRemainingInfoRequest.AccountId)))
			{
				return false;
			}
			return true;
		}

		public static GetGameTimeRemainingInfoRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameTimeRemainingInfoRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetGameTimeRemainingInfoRequest Deserialize(Stream stream, GetGameTimeRemainingInfoRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetGameTimeRemainingInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGameTimeRemainingInfoRequest getGameTimeRemainingInfoRequest = new GetGameTimeRemainingInfoRequest();
			DeserializeLengthDelimited(stream, getGameTimeRemainingInfoRequest);
			return getGameTimeRemainingInfoRequest;
		}

		public static GetGameTimeRemainingInfoRequest DeserializeLengthDelimited(Stream stream, GetGameTimeRemainingInfoRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetGameTimeRemainingInfoRequest Deserialize(Stream stream, GetGameTimeRemainingInfoRequest instance, long limit)
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
					if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
					continue;
				case 18:
					if (instance.AccountId == null)
					{
						instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
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

		public static void Serialize(Stream stream, GetGameTimeRemainingInfoRequest instance)
		{
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
			if (instance.HasAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameAccountId)
			{
				num++;
				uint serializedSize = GameAccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasAccountId)
			{
				num++;
				uint serializedSize2 = AccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
