using System.IO;

namespace bnet.protocol
{
	public class Identity : IProtoBuf
	{
		public bool HasAccountId;

		private EntityId _AccountId;

		public bool HasGameAccountId;

		private EntityId _GameAccountId;

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

		public bool IsInitialized => true;

		public void SetAccountId(EntityId val)
		{
			AccountId = val;
		}

		public void SetGameAccountId(EntityId val)
		{
			GameAccountId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccountId)
			{
				num ^= AccountId.GetHashCode();
			}
			if (HasGameAccountId)
			{
				num ^= GameAccountId.GetHashCode();
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
			if (HasAccountId != identity.HasAccountId || (HasAccountId && !AccountId.Equals(identity.AccountId)))
			{
				return false;
			}
			if (HasGameAccountId != identity.HasGameAccountId || (HasGameAccountId && !GameAccountId.Equals(identity.GameAccountId)))
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
					if (instance.AccountId == null)
					{
						instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
					}
					continue;
				case 18:
					if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
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
			if (instance.HasAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
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
			if (HasGameAccountId)
			{
				num++;
				uint serializedSize2 = GameAccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
