using System.IO;

namespace PegasusGame
{
	public class GameRealTimeRaceCount : IProtoBuf
	{
		public int Race { get; set; }

		public int Count { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Race.GetHashCode() ^ Count.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GameRealTimeRaceCount gameRealTimeRaceCount = obj as GameRealTimeRaceCount;
			if (gameRealTimeRaceCount == null)
			{
				return false;
			}
			if (!Race.Equals(gameRealTimeRaceCount.Race))
			{
				return false;
			}
			if (!Count.Equals(gameRealTimeRaceCount.Count))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameRealTimeRaceCount Deserialize(Stream stream, GameRealTimeRaceCount instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameRealTimeRaceCount DeserializeLengthDelimited(Stream stream)
		{
			GameRealTimeRaceCount gameRealTimeRaceCount = new GameRealTimeRaceCount();
			DeserializeLengthDelimited(stream, gameRealTimeRaceCount);
			return gameRealTimeRaceCount;
		}

		public static GameRealTimeRaceCount DeserializeLengthDelimited(Stream stream, GameRealTimeRaceCount instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameRealTimeRaceCount Deserialize(Stream stream, GameRealTimeRaceCount instance, long limit)
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
					instance.Race = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Count = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GameRealTimeRaceCount instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Race);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Count);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Race) + ProtocolParser.SizeOfUInt64((ulong)Count) + 2;
		}
	}
}
