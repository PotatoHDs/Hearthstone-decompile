using System.IO;

namespace bnet.protocol.account.v1
{
	public class GameAccountState : IProtoBuf
	{
		public bool HasGameLevelInfo;

		private GameLevelInfo _GameLevelInfo;

		public bool HasGameTimeInfo;

		private GameTimeInfo _GameTimeInfo;

		public bool HasGameStatus;

		private GameStatus _GameStatus;

		public bool HasRafInfo;

		private RAFInfo _RafInfo;

		public GameLevelInfo GameLevelInfo
		{
			get
			{
				return _GameLevelInfo;
			}
			set
			{
				_GameLevelInfo = value;
				HasGameLevelInfo = value != null;
			}
		}

		public GameTimeInfo GameTimeInfo
		{
			get
			{
				return _GameTimeInfo;
			}
			set
			{
				_GameTimeInfo = value;
				HasGameTimeInfo = value != null;
			}
		}

		public GameStatus GameStatus
		{
			get
			{
				return _GameStatus;
			}
			set
			{
				_GameStatus = value;
				HasGameStatus = value != null;
			}
		}

		public RAFInfo RafInfo
		{
			get
			{
				return _RafInfo;
			}
			set
			{
				_RafInfo = value;
				HasRafInfo = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetGameLevelInfo(GameLevelInfo val)
		{
			GameLevelInfo = val;
		}

		public void SetGameTimeInfo(GameTimeInfo val)
		{
			GameTimeInfo = val;
		}

		public void SetGameStatus(GameStatus val)
		{
			GameStatus = val;
		}

		public void SetRafInfo(RAFInfo val)
		{
			RafInfo = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameLevelInfo)
			{
				num ^= GameLevelInfo.GetHashCode();
			}
			if (HasGameTimeInfo)
			{
				num ^= GameTimeInfo.GetHashCode();
			}
			if (HasGameStatus)
			{
				num ^= GameStatus.GetHashCode();
			}
			if (HasRafInfo)
			{
				num ^= RafInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountState gameAccountState = obj as GameAccountState;
			if (gameAccountState == null)
			{
				return false;
			}
			if (HasGameLevelInfo != gameAccountState.HasGameLevelInfo || (HasGameLevelInfo && !GameLevelInfo.Equals(gameAccountState.GameLevelInfo)))
			{
				return false;
			}
			if (HasGameTimeInfo != gameAccountState.HasGameTimeInfo || (HasGameTimeInfo && !GameTimeInfo.Equals(gameAccountState.GameTimeInfo)))
			{
				return false;
			}
			if (HasGameStatus != gameAccountState.HasGameStatus || (HasGameStatus && !GameStatus.Equals(gameAccountState.GameStatus)))
			{
				return false;
			}
			if (HasRafInfo != gameAccountState.HasRafInfo || (HasRafInfo && !RafInfo.Equals(gameAccountState.RafInfo)))
			{
				return false;
			}
			return true;
		}

		public static GameAccountState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountState>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameAccountState Deserialize(Stream stream, GameAccountState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameAccountState DeserializeLengthDelimited(Stream stream)
		{
			GameAccountState gameAccountState = new GameAccountState();
			DeserializeLengthDelimited(stream, gameAccountState);
			return gameAccountState;
		}

		public static GameAccountState DeserializeLengthDelimited(Stream stream, GameAccountState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameAccountState Deserialize(Stream stream, GameAccountState instance, long limit)
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
					if (instance.GameLevelInfo == null)
					{
						instance.GameLevelInfo = GameLevelInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameLevelInfo.DeserializeLengthDelimited(stream, instance.GameLevelInfo);
					}
					continue;
				case 18:
					if (instance.GameTimeInfo == null)
					{
						instance.GameTimeInfo = GameTimeInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameTimeInfo.DeserializeLengthDelimited(stream, instance.GameTimeInfo);
					}
					continue;
				case 26:
					if (instance.GameStatus == null)
					{
						instance.GameStatus = GameStatus.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameStatus.DeserializeLengthDelimited(stream, instance.GameStatus);
					}
					continue;
				case 34:
					if (instance.RafInfo == null)
					{
						instance.RafInfo = RAFInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						RAFInfo.DeserializeLengthDelimited(stream, instance.RafInfo);
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

		public static void Serialize(Stream stream, GameAccountState instance)
		{
			if (instance.HasGameLevelInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameLevelInfo.GetSerializedSize());
				GameLevelInfo.Serialize(stream, instance.GameLevelInfo);
			}
			if (instance.HasGameTimeInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameTimeInfo.GetSerializedSize());
				GameTimeInfo.Serialize(stream, instance.GameTimeInfo);
			}
			if (instance.HasGameStatus)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.GameStatus.GetSerializedSize());
				GameStatus.Serialize(stream, instance.GameStatus);
			}
			if (instance.HasRafInfo)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.RafInfo.GetSerializedSize());
				RAFInfo.Serialize(stream, instance.RafInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameLevelInfo)
			{
				num++;
				uint serializedSize = GameLevelInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasGameTimeInfo)
			{
				num++;
				uint serializedSize2 = GameTimeInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasGameStatus)
			{
				num++;
				uint serializedSize3 = GameStatus.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasRafInfo)
			{
				num++;
				uint serializedSize4 = RafInfo.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}
	}
}
