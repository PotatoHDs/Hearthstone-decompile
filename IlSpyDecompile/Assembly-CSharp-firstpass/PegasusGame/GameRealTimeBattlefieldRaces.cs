using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	public class GameRealTimeBattlefieldRaces : IProtoBuf
	{
		public enum PacketID
		{
			ID = 0x1F
		}

		private List<GameRealTimeRaceCount> _Races = new List<GameRealTimeRaceCount>();

		public int PlayerId { get; set; }

		public List<GameRealTimeRaceCount> Races
		{
			get
			{
				return _Races;
			}
			set
			{
				_Races = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= PlayerId.GetHashCode();
			foreach (GameRealTimeRaceCount race in Races)
			{
				hashCode ^= race.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GameRealTimeBattlefieldRaces gameRealTimeBattlefieldRaces = obj as GameRealTimeBattlefieldRaces;
			if (gameRealTimeBattlefieldRaces == null)
			{
				return false;
			}
			if (!PlayerId.Equals(gameRealTimeBattlefieldRaces.PlayerId))
			{
				return false;
			}
			if (Races.Count != gameRealTimeBattlefieldRaces.Races.Count)
			{
				return false;
			}
			for (int i = 0; i < Races.Count; i++)
			{
				if (!Races[i].Equals(gameRealTimeBattlefieldRaces.Races[i]))
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

		public static GameRealTimeBattlefieldRaces Deserialize(Stream stream, GameRealTimeBattlefieldRaces instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameRealTimeBattlefieldRaces DeserializeLengthDelimited(Stream stream)
		{
			GameRealTimeBattlefieldRaces gameRealTimeBattlefieldRaces = new GameRealTimeBattlefieldRaces();
			DeserializeLengthDelimited(stream, gameRealTimeBattlefieldRaces);
			return gameRealTimeBattlefieldRaces;
		}

		public static GameRealTimeBattlefieldRaces DeserializeLengthDelimited(Stream stream, GameRealTimeBattlefieldRaces instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameRealTimeBattlefieldRaces Deserialize(Stream stream, GameRealTimeBattlefieldRaces instance, long limit)
		{
			if (instance.Races == null)
			{
				instance.Races = new List<GameRealTimeRaceCount>();
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
				case 8:
					instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.Races.Add(GameRealTimeRaceCount.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GameRealTimeBattlefieldRaces instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			if (instance.Races.Count <= 0)
			{
				return;
			}
			foreach (GameRealTimeRaceCount race in instance.Races)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, race.GetSerializedSize());
				GameRealTimeRaceCount.Serialize(stream, race);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)PlayerId);
			if (Races.Count > 0)
			{
				foreach (GameRealTimeRaceCount race in Races)
				{
					num++;
					uint serializedSize = race.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 1;
		}
	}
}
