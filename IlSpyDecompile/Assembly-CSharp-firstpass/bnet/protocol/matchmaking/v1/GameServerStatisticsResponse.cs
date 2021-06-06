using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class GameServerStatisticsResponse : IProtoBuf
	{
		public bool HasNumGameServers;

		private int _NumGameServers;

		public bool HasNumAvailableSlots;

		private int _NumAvailableSlots;

		public bool HasNumTotalSlots;

		private int _NumTotalSlots;

		public int NumGameServers
		{
			get
			{
				return _NumGameServers;
			}
			set
			{
				_NumGameServers = value;
				HasNumGameServers = true;
			}
		}

		public int NumAvailableSlots
		{
			get
			{
				return _NumAvailableSlots;
			}
			set
			{
				_NumAvailableSlots = value;
				HasNumAvailableSlots = true;
			}
		}

		public int NumTotalSlots
		{
			get
			{
				return _NumTotalSlots;
			}
			set
			{
				_NumTotalSlots = value;
				HasNumTotalSlots = true;
			}
		}

		public bool IsInitialized => true;

		public void SetNumGameServers(int val)
		{
			NumGameServers = val;
		}

		public void SetNumAvailableSlots(int val)
		{
			NumAvailableSlots = val;
		}

		public void SetNumTotalSlots(int val)
		{
			NumTotalSlots = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasNumGameServers)
			{
				num ^= NumGameServers.GetHashCode();
			}
			if (HasNumAvailableSlots)
			{
				num ^= NumAvailableSlots.GetHashCode();
			}
			if (HasNumTotalSlots)
			{
				num ^= NumTotalSlots.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameServerStatisticsResponse gameServerStatisticsResponse = obj as GameServerStatisticsResponse;
			if (gameServerStatisticsResponse == null)
			{
				return false;
			}
			if (HasNumGameServers != gameServerStatisticsResponse.HasNumGameServers || (HasNumGameServers && !NumGameServers.Equals(gameServerStatisticsResponse.NumGameServers)))
			{
				return false;
			}
			if (HasNumAvailableSlots != gameServerStatisticsResponse.HasNumAvailableSlots || (HasNumAvailableSlots && !NumAvailableSlots.Equals(gameServerStatisticsResponse.NumAvailableSlots)))
			{
				return false;
			}
			if (HasNumTotalSlots != gameServerStatisticsResponse.HasNumTotalSlots || (HasNumTotalSlots && !NumTotalSlots.Equals(gameServerStatisticsResponse.NumTotalSlots)))
			{
				return false;
			}
			return true;
		}

		public static GameServerStatisticsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameServerStatisticsResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameServerStatisticsResponse Deserialize(Stream stream, GameServerStatisticsResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameServerStatisticsResponse DeserializeLengthDelimited(Stream stream)
		{
			GameServerStatisticsResponse gameServerStatisticsResponse = new GameServerStatisticsResponse();
			DeserializeLengthDelimited(stream, gameServerStatisticsResponse);
			return gameServerStatisticsResponse;
		}

		public static GameServerStatisticsResponse DeserializeLengthDelimited(Stream stream, GameServerStatisticsResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameServerStatisticsResponse Deserialize(Stream stream, GameServerStatisticsResponse instance, long limit)
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
					instance.NumGameServers = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.NumAvailableSlots = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.NumTotalSlots = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GameServerStatisticsResponse instance)
		{
			if (instance.HasNumGameServers)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.NumGameServers);
			}
			if (instance.HasNumAvailableSlots)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.NumAvailableSlots);
			}
			if (instance.HasNumTotalSlots)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.NumTotalSlots);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasNumGameServers)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)NumGameServers);
			}
			if (HasNumAvailableSlots)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)NumAvailableSlots);
			}
			if (HasNumTotalSlots)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)NumTotalSlots);
			}
			return num;
		}
	}
}
