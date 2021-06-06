using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class GameServerProperties : IProtoBuf
	{
		public bool HasMaxGameCount;

		private uint _MaxGameCount;

		public bool HasCreateGameRate;

		private uint _CreateGameRate;

		public uint MaxGameCount
		{
			get
			{
				return _MaxGameCount;
			}
			set
			{
				_MaxGameCount = value;
				HasMaxGameCount = true;
			}
		}

		public uint CreateGameRate
		{
			get
			{
				return _CreateGameRate;
			}
			set
			{
				_CreateGameRate = value;
				HasCreateGameRate = true;
			}
		}

		public bool IsInitialized => true;

		public void SetMaxGameCount(uint val)
		{
			MaxGameCount = val;
		}

		public void SetCreateGameRate(uint val)
		{
			CreateGameRate = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMaxGameCount)
			{
				num ^= MaxGameCount.GetHashCode();
			}
			if (HasCreateGameRate)
			{
				num ^= CreateGameRate.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameServerProperties gameServerProperties = obj as GameServerProperties;
			if (gameServerProperties == null)
			{
				return false;
			}
			if (HasMaxGameCount != gameServerProperties.HasMaxGameCount || (HasMaxGameCount && !MaxGameCount.Equals(gameServerProperties.MaxGameCount)))
			{
				return false;
			}
			if (HasCreateGameRate != gameServerProperties.HasCreateGameRate || (HasCreateGameRate && !CreateGameRate.Equals(gameServerProperties.CreateGameRate)))
			{
				return false;
			}
			return true;
		}

		public static GameServerProperties ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameServerProperties>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameServerProperties Deserialize(Stream stream, GameServerProperties instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameServerProperties DeserializeLengthDelimited(Stream stream)
		{
			GameServerProperties gameServerProperties = new GameServerProperties();
			DeserializeLengthDelimited(stream, gameServerProperties);
			return gameServerProperties;
		}

		public static GameServerProperties DeserializeLengthDelimited(Stream stream, GameServerProperties instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameServerProperties Deserialize(Stream stream, GameServerProperties instance, long limit)
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
					instance.MaxGameCount = ProtocolParser.ReadUInt32(stream);
					continue;
				case 16:
					instance.CreateGameRate = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, GameServerProperties instance)
		{
			if (instance.HasMaxGameCount)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.MaxGameCount);
			}
			if (instance.HasCreateGameRate)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.CreateGameRate);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMaxGameCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MaxGameCount);
			}
			if (HasCreateGameRate)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(CreateGameRate);
			}
			return num;
		}
	}
}
