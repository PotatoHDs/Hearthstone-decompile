using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	public class GameRoundHistoryEntry : IProtoBuf
	{
		private List<GameRoundHistoryPlayerEntry> _Combats = new List<GameRoundHistoryPlayerEntry>();

		public List<GameRoundHistoryPlayerEntry> Combats
		{
			get
			{
				return _Combats;
			}
			set
			{
				_Combats = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (GameRoundHistoryPlayerEntry combat in Combats)
			{
				num ^= combat.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameRoundHistoryEntry gameRoundHistoryEntry = obj as GameRoundHistoryEntry;
			if (gameRoundHistoryEntry == null)
			{
				return false;
			}
			if (Combats.Count != gameRoundHistoryEntry.Combats.Count)
			{
				return false;
			}
			for (int i = 0; i < Combats.Count; i++)
			{
				if (!Combats[i].Equals(gameRoundHistoryEntry.Combats[i]))
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

		public static GameRoundHistoryEntry Deserialize(Stream stream, GameRoundHistoryEntry instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameRoundHistoryEntry DeserializeLengthDelimited(Stream stream)
		{
			GameRoundHistoryEntry gameRoundHistoryEntry = new GameRoundHistoryEntry();
			DeserializeLengthDelimited(stream, gameRoundHistoryEntry);
			return gameRoundHistoryEntry;
		}

		public static GameRoundHistoryEntry DeserializeLengthDelimited(Stream stream, GameRoundHistoryEntry instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameRoundHistoryEntry Deserialize(Stream stream, GameRoundHistoryEntry instance, long limit)
		{
			if (instance.Combats == null)
			{
				instance.Combats = new List<GameRoundHistoryPlayerEntry>();
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
					instance.Combats.Add(GameRoundHistoryPlayerEntry.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GameRoundHistoryEntry instance)
		{
			if (instance.Combats.Count <= 0)
			{
				return;
			}
			foreach (GameRoundHistoryPlayerEntry combat in instance.Combats)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, combat.GetSerializedSize());
				GameRoundHistoryPlayerEntry.Serialize(stream, combat);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Combats.Count > 0)
			{
				foreach (GameRoundHistoryPlayerEntry combat in Combats)
				{
					num++;
					uint serializedSize = combat.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
