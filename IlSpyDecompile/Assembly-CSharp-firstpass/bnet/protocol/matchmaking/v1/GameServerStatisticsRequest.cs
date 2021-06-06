using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class GameServerStatisticsRequest : IProtoBuf
	{
		public bool HasMatchmakerGuid;

		private ulong _MatchmakerGuid;

		public ulong MatchmakerGuid
		{
			get
			{
				return _MatchmakerGuid;
			}
			set
			{
				_MatchmakerGuid = value;
				HasMatchmakerGuid = true;
			}
		}

		public bool IsInitialized => true;

		public void SetMatchmakerGuid(ulong val)
		{
			MatchmakerGuid = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMatchmakerGuid)
			{
				num ^= MatchmakerGuid.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameServerStatisticsRequest gameServerStatisticsRequest = obj as GameServerStatisticsRequest;
			if (gameServerStatisticsRequest == null)
			{
				return false;
			}
			if (HasMatchmakerGuid != gameServerStatisticsRequest.HasMatchmakerGuid || (HasMatchmakerGuid && !MatchmakerGuid.Equals(gameServerStatisticsRequest.MatchmakerGuid)))
			{
				return false;
			}
			return true;
		}

		public static GameServerStatisticsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameServerStatisticsRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameServerStatisticsRequest Deserialize(Stream stream, GameServerStatisticsRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameServerStatisticsRequest DeserializeLengthDelimited(Stream stream)
		{
			GameServerStatisticsRequest gameServerStatisticsRequest = new GameServerStatisticsRequest();
			DeserializeLengthDelimited(stream, gameServerStatisticsRequest);
			return gameServerStatisticsRequest;
		}

		public static GameServerStatisticsRequest DeserializeLengthDelimited(Stream stream, GameServerStatisticsRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameServerStatisticsRequest Deserialize(Stream stream, GameServerStatisticsRequest instance, long limit)
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
					instance.MatchmakerGuid = binaryReader.ReadUInt64();
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

		public static void Serialize(Stream stream, GameServerStatisticsRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMatchmakerGuid)
			{
				num++;
				num += 8;
			}
			return num;
		}
	}
}
