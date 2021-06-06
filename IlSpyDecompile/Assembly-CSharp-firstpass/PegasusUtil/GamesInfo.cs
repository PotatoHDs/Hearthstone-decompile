using System.IO;

namespace PegasusUtil
{
	public class GamesInfo : IProtoBuf
	{
		public enum PacketID
		{
			ID = 208
		}

		public int GamesStarted { get; set; }

		public int GamesWon { get; set; }

		public int GamesLost { get; set; }

		public int FreeRewardProgress { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ GamesStarted.GetHashCode() ^ GamesWon.GetHashCode() ^ GamesLost.GetHashCode() ^ FreeRewardProgress.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GamesInfo gamesInfo = obj as GamesInfo;
			if (gamesInfo == null)
			{
				return false;
			}
			if (!GamesStarted.Equals(gamesInfo.GamesStarted))
			{
				return false;
			}
			if (!GamesWon.Equals(gamesInfo.GamesWon))
			{
				return false;
			}
			if (!GamesLost.Equals(gamesInfo.GamesLost))
			{
				return false;
			}
			if (!FreeRewardProgress.Equals(gamesInfo.FreeRewardProgress))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GamesInfo Deserialize(Stream stream, GamesInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GamesInfo DeserializeLengthDelimited(Stream stream)
		{
			GamesInfo gamesInfo = new GamesInfo();
			DeserializeLengthDelimited(stream, gamesInfo);
			return gamesInfo;
		}

		public static GamesInfo DeserializeLengthDelimited(Stream stream, GamesInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GamesInfo Deserialize(Stream stream, GamesInfo instance, long limit)
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
					instance.GamesStarted = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.GamesWon = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.GamesLost = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.FreeRewardProgress = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GamesInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.GamesStarted);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.GamesWon);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.GamesLost);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FreeRewardProgress);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)GamesStarted) + ProtocolParser.SizeOfUInt64((ulong)GamesWon) + ProtocolParser.SizeOfUInt64((ulong)GamesLost) + ProtocolParser.SizeOfUInt64((ulong)FreeRewardProgress) + 4;
		}
	}
}
