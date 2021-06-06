using System.IO;

namespace bnet.protocol.games.v1
{
	public class GameRequestEntry : IProtoBuf
	{
		public bool HasFactoryId;

		private ulong _FactoryId;

		public bool HasNumGames;

		private uint _NumGames;

		public bool HasServerCost;

		private uint _ServerCost;

		public ulong FactoryId
		{
			get
			{
				return _FactoryId;
			}
			set
			{
				_FactoryId = value;
				HasFactoryId = true;
			}
		}

		public uint NumGames
		{
			get
			{
				return _NumGames;
			}
			set
			{
				_NumGames = value;
				HasNumGames = true;
			}
		}

		public uint ServerCost
		{
			get
			{
				return _ServerCost;
			}
			set
			{
				_ServerCost = value;
				HasServerCost = true;
			}
		}

		public bool IsInitialized => true;

		public void SetFactoryId(ulong val)
		{
			FactoryId = val;
		}

		public void SetNumGames(uint val)
		{
			NumGames = val;
		}

		public void SetServerCost(uint val)
		{
			ServerCost = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFactoryId)
			{
				num ^= FactoryId.GetHashCode();
			}
			if (HasNumGames)
			{
				num ^= NumGames.GetHashCode();
			}
			if (HasServerCost)
			{
				num ^= ServerCost.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameRequestEntry gameRequestEntry = obj as GameRequestEntry;
			if (gameRequestEntry == null)
			{
				return false;
			}
			if (HasFactoryId != gameRequestEntry.HasFactoryId || (HasFactoryId && !FactoryId.Equals(gameRequestEntry.FactoryId)))
			{
				return false;
			}
			if (HasNumGames != gameRequestEntry.HasNumGames || (HasNumGames && !NumGames.Equals(gameRequestEntry.NumGames)))
			{
				return false;
			}
			if (HasServerCost != gameRequestEntry.HasServerCost || (HasServerCost && !ServerCost.Equals(gameRequestEntry.ServerCost)))
			{
				return false;
			}
			return true;
		}

		public static GameRequestEntry ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameRequestEntry>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameRequestEntry Deserialize(Stream stream, GameRequestEntry instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameRequestEntry DeserializeLengthDelimited(Stream stream)
		{
			GameRequestEntry gameRequestEntry = new GameRequestEntry();
			DeserializeLengthDelimited(stream, gameRequestEntry);
			return gameRequestEntry;
		}

		public static GameRequestEntry DeserializeLengthDelimited(Stream stream, GameRequestEntry instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameRequestEntry Deserialize(Stream stream, GameRequestEntry instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 9:
					instance.FactoryId = binaryReader.ReadUInt64();
					continue;
				case 16:
					instance.NumGames = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.ServerCost = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, GameRequestEntry instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasFactoryId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.FactoryId);
			}
			if (instance.HasNumGames)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.NumGames);
			}
			if (instance.HasServerCost)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.ServerCost);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasFactoryId)
			{
				num++;
				num += 8;
			}
			if (HasNumGames)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(NumGames);
			}
			if (HasServerCost)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ServerCost);
			}
			return num;
		}
	}
}
