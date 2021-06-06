using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	public class GameRoundHistory : IProtoBuf
	{
		public enum PacketID
		{
			ID = 30
		}

		private List<GameRoundHistoryEntry> _Rounds = new List<GameRoundHistoryEntry>();

		public List<GameRoundHistoryEntry> Rounds
		{
			get
			{
				return _Rounds;
			}
			set
			{
				_Rounds = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (GameRoundHistoryEntry round in Rounds)
			{
				num ^= round.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameRoundHistory gameRoundHistory = obj as GameRoundHistory;
			if (gameRoundHistory == null)
			{
				return false;
			}
			if (Rounds.Count != gameRoundHistory.Rounds.Count)
			{
				return false;
			}
			for (int i = 0; i < Rounds.Count; i++)
			{
				if (!Rounds[i].Equals(gameRoundHistory.Rounds[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameRoundHistory Deserialize(Stream stream, GameRoundHistory instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameRoundHistory DeserializeLengthDelimited(Stream stream)
		{
			GameRoundHistory gameRoundHistory = new GameRoundHistory();
			DeserializeLengthDelimited(stream, gameRoundHistory);
			return gameRoundHistory;
		}

		public static GameRoundHistory DeserializeLengthDelimited(Stream stream, GameRoundHistory instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameRoundHistory Deserialize(Stream stream, GameRoundHistory instance, long limit)
		{
			if (instance.Rounds == null)
			{
				instance.Rounds = new List<GameRoundHistoryEntry>();
			}
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
					instance.Rounds.Add(GameRoundHistoryEntry.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GameRoundHistory instance)
		{
			if (instance.Rounds.Count <= 0)
			{
				return;
			}
			foreach (GameRoundHistoryEntry round in instance.Rounds)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, round.GetSerializedSize());
				GameRoundHistoryEntry.Serialize(stream, round);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Rounds.Count > 0)
			{
				foreach (GameRoundHistoryEntry round in Rounds)
				{
					num++;
					uint serializedSize = round.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
