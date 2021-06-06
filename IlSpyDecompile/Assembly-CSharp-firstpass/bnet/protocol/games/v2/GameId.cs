using System.IO;

namespace bnet.protocol.games.v2
{
	public class GameId : IProtoBuf
	{
		public bool HasId;

		private uint _Id;

		public bool HasServerId;

		private ProcessId _ServerId;

		public uint Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				HasId = true;
			}
		}

		public ProcessId ServerId
		{
			get
			{
				return _ServerId;
			}
			set
			{
				_ServerId = value;
				HasServerId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetId(uint val)
		{
			Id = val;
		}

		public void SetServerId(ProcessId val)
		{
			ServerId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			if (HasServerId)
			{
				num ^= ServerId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameId gameId = obj as GameId;
			if (gameId == null)
			{
				return false;
			}
			if (HasId != gameId.HasId || (HasId && !Id.Equals(gameId.Id)))
			{
				return false;
			}
			if (HasServerId != gameId.HasServerId || (HasServerId && !ServerId.Equals(gameId.ServerId)))
			{
				return false;
			}
			return true;
		}

		public static GameId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameId>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameId Deserialize(Stream stream, GameId instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameId DeserializeLengthDelimited(Stream stream)
		{
			GameId gameId = new GameId();
			DeserializeLengthDelimited(stream, gameId);
			return gameId;
		}

		public static GameId DeserializeLengthDelimited(Stream stream, GameId instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameId Deserialize(Stream stream, GameId instance, long limit)
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
				case 13:
					instance.Id = binaryReader.ReadUInt32();
					continue;
				case 18:
					if (instance.ServerId == null)
					{
						instance.ServerId = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.ServerId);
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

		public static void Serialize(Stream stream, GameId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.Id);
			}
			if (instance.HasServerId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ServerId.GetSerializedSize());
				ProcessId.Serialize(stream, instance.ServerId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasId)
			{
				num++;
				num += 4;
			}
			if (HasServerId)
			{
				num++;
				uint serializedSize = ServerId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
