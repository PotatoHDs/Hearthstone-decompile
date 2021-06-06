using System.IO;

namespace bnet.protocol.authentication.v1
{
	public class GameAccountSelectedRequest : IProtoBuf
	{
		public bool HasGameAccountId;

		private EntityId _GameAccountId;

		public uint Result { get; set; }

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

		public void SetResult(uint val)
		{
			Result = val;
		}

		public void SetGameAccountId(EntityId val)
		{
			GameAccountId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Result.GetHashCode();
			if (HasGameAccountId)
			{
				hashCode ^= GameAccountId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GameAccountSelectedRequest gameAccountSelectedRequest = obj as GameAccountSelectedRequest;
			if (gameAccountSelectedRequest == null)
			{
				return false;
			}
			if (!Result.Equals(gameAccountSelectedRequest.Result))
			{
				return false;
			}
			if (HasGameAccountId != gameAccountSelectedRequest.HasGameAccountId || (HasGameAccountId && !GameAccountId.Equals(gameAccountSelectedRequest.GameAccountId)))
			{
				return false;
			}
			return true;
		}

		public static GameAccountSelectedRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountSelectedRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameAccountSelectedRequest Deserialize(Stream stream, GameAccountSelectedRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameAccountSelectedRequest DeserializeLengthDelimited(Stream stream)
		{
			GameAccountSelectedRequest gameAccountSelectedRequest = new GameAccountSelectedRequest();
			DeserializeLengthDelimited(stream, gameAccountSelectedRequest);
			return gameAccountSelectedRequest;
		}

		public static GameAccountSelectedRequest DeserializeLengthDelimited(Stream stream, GameAccountSelectedRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameAccountSelectedRequest Deserialize(Stream stream, GameAccountSelectedRequest instance, long limit)
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
				case 8:
					instance.Result = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, GameAccountSelectedRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Result);
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
			num += ProtocolParser.SizeOfUInt32(Result);
			if (HasGameAccountId)
			{
				num++;
				uint serializedSize = GameAccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 1;
		}
	}
}
