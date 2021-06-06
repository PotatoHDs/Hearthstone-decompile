using System.IO;

namespace bnet.protocol.games.v2
{
	public class GameHandle : IProtoBuf
	{
		public bool HasFactoryId;

		private FactoryId _FactoryId;

		public bool HasGameId;

		private GameId _GameId;

		public FactoryId FactoryId
		{
			get
			{
				return _FactoryId;
			}
			set
			{
				_FactoryId = value;
				HasFactoryId = value != null;
			}
		}

		public GameId GameId
		{
			get
			{
				return _GameId;
			}
			set
			{
				_GameId = value;
				HasGameId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetFactoryId(FactoryId val)
		{
			FactoryId = val;
		}

		public void SetGameId(GameId val)
		{
			GameId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFactoryId)
			{
				num ^= FactoryId.GetHashCode();
			}
			if (HasGameId)
			{
				num ^= GameId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameHandle gameHandle = obj as GameHandle;
			if (gameHandle == null)
			{
				return false;
			}
			if (HasFactoryId != gameHandle.HasFactoryId || (HasFactoryId && !FactoryId.Equals(gameHandle.FactoryId)))
			{
				return false;
			}
			if (HasGameId != gameHandle.HasGameId || (HasGameId && !GameId.Equals(gameHandle.GameId)))
			{
				return false;
			}
			return true;
		}

		public static GameHandle ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameHandle>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameHandle Deserialize(Stream stream, GameHandle instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameHandle DeserializeLengthDelimited(Stream stream)
		{
			GameHandle gameHandle = new GameHandle();
			DeserializeLengthDelimited(stream, gameHandle);
			return gameHandle;
		}

		public static GameHandle DeserializeLengthDelimited(Stream stream, GameHandle instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameHandle Deserialize(Stream stream, GameHandle instance, long limit)
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
					if (instance.FactoryId == null)
					{
						instance.FactoryId = FactoryId.DeserializeLengthDelimited(stream);
					}
					else
					{
						FactoryId.DeserializeLengthDelimited(stream, instance.FactoryId);
					}
					continue;
				case 18:
					if (instance.GameId == null)
					{
						instance.GameId = GameId.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameId.DeserializeLengthDelimited(stream, instance.GameId);
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

		public static void Serialize(Stream stream, GameHandle instance)
		{
			if (instance.HasFactoryId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.FactoryId.GetSerializedSize());
				FactoryId.Serialize(stream, instance.FactoryId);
			}
			if (instance.HasGameId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameId.GetSerializedSize());
				GameId.Serialize(stream, instance.GameId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasFactoryId)
			{
				num++;
				uint serializedSize = FactoryId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasGameId)
			{
				num++;
				uint serializedSize2 = GameId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
