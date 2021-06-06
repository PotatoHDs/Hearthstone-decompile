using System.IO;

namespace bnet.protocol.account.v1
{
	public class GameAccountStateTagged : IProtoBuf
	{
		public bool HasGameAccountState;

		private GameAccountState _GameAccountState;

		public bool HasGameAccountTags;

		private GameAccountFieldTags _GameAccountTags;

		public GameAccountState GameAccountState
		{
			get
			{
				return _GameAccountState;
			}
			set
			{
				_GameAccountState = value;
				HasGameAccountState = value != null;
			}
		}

		public GameAccountFieldTags GameAccountTags
		{
			get
			{
				return _GameAccountTags;
			}
			set
			{
				_GameAccountTags = value;
				HasGameAccountTags = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetGameAccountState(GameAccountState val)
		{
			GameAccountState = val;
		}

		public void SetGameAccountTags(GameAccountFieldTags val)
		{
			GameAccountTags = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameAccountState)
			{
				num ^= GameAccountState.GetHashCode();
			}
			if (HasGameAccountTags)
			{
				num ^= GameAccountTags.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountStateTagged gameAccountStateTagged = obj as GameAccountStateTagged;
			if (gameAccountStateTagged == null)
			{
				return false;
			}
			if (HasGameAccountState != gameAccountStateTagged.HasGameAccountState || (HasGameAccountState && !GameAccountState.Equals(gameAccountStateTagged.GameAccountState)))
			{
				return false;
			}
			if (HasGameAccountTags != gameAccountStateTagged.HasGameAccountTags || (HasGameAccountTags && !GameAccountTags.Equals(gameAccountStateTagged.GameAccountTags)))
			{
				return false;
			}
			return true;
		}

		public static GameAccountStateTagged ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountStateTagged>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameAccountStateTagged Deserialize(Stream stream, GameAccountStateTagged instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameAccountStateTagged DeserializeLengthDelimited(Stream stream)
		{
			GameAccountStateTagged gameAccountStateTagged = new GameAccountStateTagged();
			DeserializeLengthDelimited(stream, gameAccountStateTagged);
			return gameAccountStateTagged;
		}

		public static GameAccountStateTagged DeserializeLengthDelimited(Stream stream, GameAccountStateTagged instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameAccountStateTagged Deserialize(Stream stream, GameAccountStateTagged instance, long limit)
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
					if (instance.GameAccountState == null)
					{
						instance.GameAccountState = GameAccountState.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountState.DeserializeLengthDelimited(stream, instance.GameAccountState);
					}
					continue;
				case 18:
					if (instance.GameAccountTags == null)
					{
						instance.GameAccountTags = GameAccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountFieldTags.DeserializeLengthDelimited(stream, instance.GameAccountTags);
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

		public static void Serialize(Stream stream, GameAccountStateTagged instance)
		{
			if (instance.HasGameAccountState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountState.GetSerializedSize());
				GameAccountState.Serialize(stream, instance.GameAccountState);
			}
			if (instance.HasGameAccountTags)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountTags.GetSerializedSize());
				GameAccountFieldTags.Serialize(stream, instance.GameAccountTags);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameAccountState)
			{
				num++;
				uint serializedSize = GameAccountState.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasGameAccountTags)
			{
				num++;
				uint serializedSize2 = GameAccountTags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
