using System.IO;

namespace bnet.protocol.account.v1
{
	public class GetGameAccountStateRequest : IProtoBuf
	{
		public bool HasAccountId;

		private EntityId _AccountId;

		public bool HasGameAccountId;

		private EntityId _GameAccountId;

		public bool HasOptions;

		private GameAccountFieldOptions _Options;

		public bool HasTags;

		private GameAccountFieldTags _Tags;

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

		public GameAccountFieldOptions Options
		{
			get
			{
				return _Options;
			}
			set
			{
				_Options = value;
				HasOptions = value != null;
			}
		}

		public GameAccountFieldTags Tags
		{
			get
			{
				return _Tags;
			}
			set
			{
				_Tags = value;
				HasTags = value != null;
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

		public void SetOptions(GameAccountFieldOptions val)
		{
			Options = val;
		}

		public void SetTags(GameAccountFieldTags val)
		{
			Tags = val;
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
			if (HasOptions)
			{
				num ^= Options.GetHashCode();
			}
			if (HasTags)
			{
				num ^= Tags.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGameAccountStateRequest getGameAccountStateRequest = obj as GetGameAccountStateRequest;
			if (getGameAccountStateRequest == null)
			{
				return false;
			}
			if (HasAccountId != getGameAccountStateRequest.HasAccountId || (HasAccountId && !AccountId.Equals(getGameAccountStateRequest.AccountId)))
			{
				return false;
			}
			if (HasGameAccountId != getGameAccountStateRequest.HasGameAccountId || (HasGameAccountId && !GameAccountId.Equals(getGameAccountStateRequest.GameAccountId)))
			{
				return false;
			}
			if (HasOptions != getGameAccountStateRequest.HasOptions || (HasOptions && !Options.Equals(getGameAccountStateRequest.Options)))
			{
				return false;
			}
			if (HasTags != getGameAccountStateRequest.HasTags || (HasTags && !Tags.Equals(getGameAccountStateRequest.Tags)))
			{
				return false;
			}
			return true;
		}

		public static GetGameAccountStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameAccountStateRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetGameAccountStateRequest Deserialize(Stream stream, GetGameAccountStateRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetGameAccountStateRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGameAccountStateRequest getGameAccountStateRequest = new GetGameAccountStateRequest();
			DeserializeLengthDelimited(stream, getGameAccountStateRequest);
			return getGameAccountStateRequest;
		}

		public static GetGameAccountStateRequest DeserializeLengthDelimited(Stream stream, GetGameAccountStateRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetGameAccountStateRequest Deserialize(Stream stream, GetGameAccountStateRequest instance, long limit)
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
				case 82:
					if (instance.Options == null)
					{
						instance.Options = GameAccountFieldOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountFieldOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
					continue;
				case 90:
					if (instance.Tags == null)
					{
						instance.Tags = GameAccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountFieldTags.DeserializeLengthDelimited(stream, instance.Tags);
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

		public static void Serialize(Stream stream, GetGameAccountStateRequest instance)
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
			if (instance.HasOptions)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				GameAccountFieldOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasTags)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.Tags.GetSerializedSize());
				GameAccountFieldTags.Serialize(stream, instance.Tags);
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
			if (HasOptions)
			{
				num++;
				uint serializedSize3 = Options.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasTags)
			{
				num++;
				uint serializedSize4 = Tags.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}
	}
}
