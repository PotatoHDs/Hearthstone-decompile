using System.Collections.Generic;
using System.IO;

namespace PegasusClient
{
	public class GamePresenceRank : IProtoBuf
	{
		public bool HasStandardDeprecated;

		private GamePresenceRankData _StandardDeprecated;

		public bool HasWildDeprecated;

		private GamePresenceRankData _WildDeprecated;

		private List<GamePresenceRankData> _Values = new List<GamePresenceRankData>();

		public GamePresenceRankData StandardDeprecated
		{
			get
			{
				return _StandardDeprecated;
			}
			set
			{
				_StandardDeprecated = value;
				HasStandardDeprecated = value != null;
			}
		}

		public GamePresenceRankData WildDeprecated
		{
			get
			{
				return _WildDeprecated;
			}
			set
			{
				_WildDeprecated = value;
				HasWildDeprecated = value != null;
			}
		}

		public List<GamePresenceRankData> Values
		{
			get
			{
				return _Values;
			}
			set
			{
				_Values = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasStandardDeprecated)
			{
				num ^= StandardDeprecated.GetHashCode();
			}
			if (HasWildDeprecated)
			{
				num ^= WildDeprecated.GetHashCode();
			}
			foreach (GamePresenceRankData value in Values)
			{
				num ^= value.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GamePresenceRank gamePresenceRank = obj as GamePresenceRank;
			if (gamePresenceRank == null)
			{
				return false;
			}
			if (HasStandardDeprecated != gamePresenceRank.HasStandardDeprecated || (HasStandardDeprecated && !StandardDeprecated.Equals(gamePresenceRank.StandardDeprecated)))
			{
				return false;
			}
			if (HasWildDeprecated != gamePresenceRank.HasWildDeprecated || (HasWildDeprecated && !WildDeprecated.Equals(gamePresenceRank.WildDeprecated)))
			{
				return false;
			}
			if (Values.Count != gamePresenceRank.Values.Count)
			{
				return false;
			}
			for (int i = 0; i < Values.Count; i++)
			{
				if (!Values[i].Equals(gamePresenceRank.Values[i]))
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

		public static GamePresenceRank Deserialize(Stream stream, GamePresenceRank instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GamePresenceRank DeserializeLengthDelimited(Stream stream)
		{
			GamePresenceRank gamePresenceRank = new GamePresenceRank();
			DeserializeLengthDelimited(stream, gamePresenceRank);
			return gamePresenceRank;
		}

		public static GamePresenceRank DeserializeLengthDelimited(Stream stream, GamePresenceRank instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GamePresenceRank Deserialize(Stream stream, GamePresenceRank instance, long limit)
		{
			if (instance.Values == null)
			{
				instance.Values = new List<GamePresenceRankData>();
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
					if (instance.StandardDeprecated == null)
					{
						instance.StandardDeprecated = GamePresenceRankData.DeserializeLengthDelimited(stream);
					}
					else
					{
						GamePresenceRankData.DeserializeLengthDelimited(stream, instance.StandardDeprecated);
					}
					continue;
				case 18:
					if (instance.WildDeprecated == null)
					{
						instance.WildDeprecated = GamePresenceRankData.DeserializeLengthDelimited(stream);
					}
					else
					{
						GamePresenceRankData.DeserializeLengthDelimited(stream, instance.WildDeprecated);
					}
					continue;
				case 26:
					instance.Values.Add(GamePresenceRankData.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GamePresenceRank instance)
		{
			if (instance.HasStandardDeprecated)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.StandardDeprecated.GetSerializedSize());
				GamePresenceRankData.Serialize(stream, instance.StandardDeprecated);
			}
			if (instance.HasWildDeprecated)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.WildDeprecated.GetSerializedSize());
				GamePresenceRankData.Serialize(stream, instance.WildDeprecated);
			}
			if (instance.Values.Count <= 0)
			{
				return;
			}
			foreach (GamePresenceRankData value in instance.Values)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, value.GetSerializedSize());
				GamePresenceRankData.Serialize(stream, value);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasStandardDeprecated)
			{
				num++;
				uint serializedSize = StandardDeprecated.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasWildDeprecated)
			{
				num++;
				uint serializedSize2 = WildDeprecated.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (Values.Count > 0)
			{
				foreach (GamePresenceRankData value in Values)
				{
					num++;
					uint serializedSize3 = value.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
				return num;
			}
			return num;
		}
	}
}
