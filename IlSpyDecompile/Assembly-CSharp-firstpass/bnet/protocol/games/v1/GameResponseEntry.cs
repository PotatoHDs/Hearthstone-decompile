using System.IO;

namespace bnet.protocol.games.v1
{
	public class GameResponseEntry : IProtoBuf
	{
		public bool HasFactoryId;

		private ulong _FactoryId;

		public bool HasPopularity;

		private float _Popularity;

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

		public float Popularity
		{
			get
			{
				return _Popularity;
			}
			set
			{
				_Popularity = value;
				HasPopularity = true;
			}
		}

		public bool IsInitialized => true;

		public void SetFactoryId(ulong val)
		{
			FactoryId = val;
		}

		public void SetPopularity(float val)
		{
			Popularity = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFactoryId)
			{
				num ^= FactoryId.GetHashCode();
			}
			if (HasPopularity)
			{
				num ^= Popularity.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameResponseEntry gameResponseEntry = obj as GameResponseEntry;
			if (gameResponseEntry == null)
			{
				return false;
			}
			if (HasFactoryId != gameResponseEntry.HasFactoryId || (HasFactoryId && !FactoryId.Equals(gameResponseEntry.FactoryId)))
			{
				return false;
			}
			if (HasPopularity != gameResponseEntry.HasPopularity || (HasPopularity && !Popularity.Equals(gameResponseEntry.Popularity)))
			{
				return false;
			}
			return true;
		}

		public static GameResponseEntry ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameResponseEntry>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameResponseEntry Deserialize(Stream stream, GameResponseEntry instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameResponseEntry DeserializeLengthDelimited(Stream stream)
		{
			GameResponseEntry gameResponseEntry = new GameResponseEntry();
			DeserializeLengthDelimited(stream, gameResponseEntry);
			return gameResponseEntry;
		}

		public static GameResponseEntry DeserializeLengthDelimited(Stream stream, GameResponseEntry instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameResponseEntry Deserialize(Stream stream, GameResponseEntry instance, long limit)
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
				case 21:
					instance.Popularity = binaryReader.ReadSingle();
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

		public static void Serialize(Stream stream, GameResponseEntry instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasFactoryId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.FactoryId);
			}
			if (instance.HasPopularity)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Popularity);
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
			if (HasPopularity)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
