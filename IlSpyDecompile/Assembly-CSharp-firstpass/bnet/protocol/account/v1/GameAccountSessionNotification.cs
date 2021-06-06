using System.IO;

namespace bnet.protocol.account.v1
{
	public class GameAccountSessionNotification : IProtoBuf
	{
		public bool HasGameAccount;

		private GameAccountHandle _GameAccount;

		public bool HasSessionInfo;

		private GameSessionUpdateInfo _SessionInfo;

		public GameAccountHandle GameAccount
		{
			get
			{
				return _GameAccount;
			}
			set
			{
				_GameAccount = value;
				HasGameAccount = value != null;
			}
		}

		public GameSessionUpdateInfo SessionInfo
		{
			get
			{
				return _SessionInfo;
			}
			set
			{
				_SessionInfo = value;
				HasSessionInfo = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetGameAccount(GameAccountHandle val)
		{
			GameAccount = val;
		}

		public void SetSessionInfo(GameSessionUpdateInfo val)
		{
			SessionInfo = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameAccount)
			{
				num ^= GameAccount.GetHashCode();
			}
			if (HasSessionInfo)
			{
				num ^= SessionInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountSessionNotification gameAccountSessionNotification = obj as GameAccountSessionNotification;
			if (gameAccountSessionNotification == null)
			{
				return false;
			}
			if (HasGameAccount != gameAccountSessionNotification.HasGameAccount || (HasGameAccount && !GameAccount.Equals(gameAccountSessionNotification.GameAccount)))
			{
				return false;
			}
			if (HasSessionInfo != gameAccountSessionNotification.HasSessionInfo || (HasSessionInfo && !SessionInfo.Equals(gameAccountSessionNotification.SessionInfo)))
			{
				return false;
			}
			return true;
		}

		public static GameAccountSessionNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountSessionNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameAccountSessionNotification Deserialize(Stream stream, GameAccountSessionNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameAccountSessionNotification DeserializeLengthDelimited(Stream stream)
		{
			GameAccountSessionNotification gameAccountSessionNotification = new GameAccountSessionNotification();
			DeserializeLengthDelimited(stream, gameAccountSessionNotification);
			return gameAccountSessionNotification;
		}

		public static GameAccountSessionNotification DeserializeLengthDelimited(Stream stream, GameAccountSessionNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameAccountSessionNotification Deserialize(Stream stream, GameAccountSessionNotification instance, long limit)
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
					if (instance.GameAccount == null)
					{
						instance.GameAccount = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.GameAccount);
					}
					continue;
				case 18:
					if (instance.SessionInfo == null)
					{
						instance.SessionInfo = GameSessionUpdateInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameSessionUpdateInfo.DeserializeLengthDelimited(stream, instance.SessionInfo);
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

		public static void Serialize(Stream stream, GameAccountSessionNotification instance)
		{
			if (instance.HasGameAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
			if (instance.HasSessionInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SessionInfo.GetSerializedSize());
				GameSessionUpdateInfo.Serialize(stream, instance.SessionInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameAccount)
			{
				num++;
				uint serializedSize = GameAccount.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasSessionInfo)
			{
				num++;
				uint serializedSize2 = SessionInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
