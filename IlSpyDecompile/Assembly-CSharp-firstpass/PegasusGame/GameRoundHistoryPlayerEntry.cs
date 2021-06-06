using System.IO;

namespace PegasusGame
{
	public class GameRoundHistoryPlayerEntry : IProtoBuf
	{
		public int PlayerId { get; set; }

		public int PlayerOpponentId { get; set; }

		public int PlayerDamageTaken { get; set; }

		public bool PlayerIsDead { get; set; }

		public bool PlayerDiedThisRound { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ PlayerId.GetHashCode() ^ PlayerOpponentId.GetHashCode() ^ PlayerDamageTaken.GetHashCode() ^ PlayerIsDead.GetHashCode() ^ PlayerDiedThisRound.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GameRoundHistoryPlayerEntry gameRoundHistoryPlayerEntry = obj as GameRoundHistoryPlayerEntry;
			if (gameRoundHistoryPlayerEntry == null)
			{
				return false;
			}
			if (!PlayerId.Equals(gameRoundHistoryPlayerEntry.PlayerId))
			{
				return false;
			}
			if (!PlayerOpponentId.Equals(gameRoundHistoryPlayerEntry.PlayerOpponentId))
			{
				return false;
			}
			if (!PlayerDamageTaken.Equals(gameRoundHistoryPlayerEntry.PlayerDamageTaken))
			{
				return false;
			}
			if (!PlayerIsDead.Equals(gameRoundHistoryPlayerEntry.PlayerIsDead))
			{
				return false;
			}
			if (!PlayerDiedThisRound.Equals(gameRoundHistoryPlayerEntry.PlayerDiedThisRound))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameRoundHistoryPlayerEntry Deserialize(Stream stream, GameRoundHistoryPlayerEntry instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameRoundHistoryPlayerEntry DeserializeLengthDelimited(Stream stream)
		{
			GameRoundHistoryPlayerEntry gameRoundHistoryPlayerEntry = new GameRoundHistoryPlayerEntry();
			DeserializeLengthDelimited(stream, gameRoundHistoryPlayerEntry);
			return gameRoundHistoryPlayerEntry;
		}

		public static GameRoundHistoryPlayerEntry DeserializeLengthDelimited(Stream stream, GameRoundHistoryPlayerEntry instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameRoundHistoryPlayerEntry Deserialize(Stream stream, GameRoundHistoryPlayerEntry instance, long limit)
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
					instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.PlayerOpponentId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.PlayerDamageTaken = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.PlayerIsDead = ProtocolParser.ReadBool(stream);
					continue;
				case 40:
					instance.PlayerDiedThisRound = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, GameRoundHistoryPlayerEntry instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerOpponentId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerDamageTaken);
			stream.WriteByte(32);
			ProtocolParser.WriteBool(stream, instance.PlayerIsDead);
			stream.WriteByte(40);
			ProtocolParser.WriteBool(stream, instance.PlayerDiedThisRound);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)PlayerId) + ProtocolParser.SizeOfUInt64((ulong)PlayerOpponentId) + ProtocolParser.SizeOfUInt64((ulong)PlayerDamageTaken) + 1 + 1 + 5;
		}
	}
}
