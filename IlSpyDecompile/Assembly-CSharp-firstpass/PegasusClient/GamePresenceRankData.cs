using System.IO;
using PegasusShared;

namespace PegasusClient
{
	public class GamePresenceRankData : IProtoBuf
	{
		public bool HasLeagueId;

		private int _LeagueId;

		public bool HasStarLevel;

		private int _StarLevel;

		public bool HasLegendRank;

		private int _LegendRank;

		public bool HasFormatType;

		private FormatType _FormatType;

		public int LeagueId
		{
			get
			{
				return _LeagueId;
			}
			set
			{
				_LeagueId = value;
				HasLeagueId = true;
			}
		}

		public int StarLevel
		{
			get
			{
				return _StarLevel;
			}
			set
			{
				_StarLevel = value;
				HasStarLevel = true;
			}
		}

		public int LegendRank
		{
			get
			{
				return _LegendRank;
			}
			set
			{
				_LegendRank = value;
				HasLegendRank = true;
			}
		}

		public FormatType FormatType
		{
			get
			{
				return _FormatType;
			}
			set
			{
				_FormatType = value;
				HasFormatType = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasLeagueId)
			{
				num ^= LeagueId.GetHashCode();
			}
			if (HasStarLevel)
			{
				num ^= StarLevel.GetHashCode();
			}
			if (HasLegendRank)
			{
				num ^= LegendRank.GetHashCode();
			}
			if (HasFormatType)
			{
				num ^= FormatType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GamePresenceRankData gamePresenceRankData = obj as GamePresenceRankData;
			if (gamePresenceRankData == null)
			{
				return false;
			}
			if (HasLeagueId != gamePresenceRankData.HasLeagueId || (HasLeagueId && !LeagueId.Equals(gamePresenceRankData.LeagueId)))
			{
				return false;
			}
			if (HasStarLevel != gamePresenceRankData.HasStarLevel || (HasStarLevel && !StarLevel.Equals(gamePresenceRankData.StarLevel)))
			{
				return false;
			}
			if (HasLegendRank != gamePresenceRankData.HasLegendRank || (HasLegendRank && !LegendRank.Equals(gamePresenceRankData.LegendRank)))
			{
				return false;
			}
			if (HasFormatType != gamePresenceRankData.HasFormatType || (HasFormatType && !FormatType.Equals(gamePresenceRankData.FormatType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GamePresenceRankData Deserialize(Stream stream, GamePresenceRankData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GamePresenceRankData DeserializeLengthDelimited(Stream stream)
		{
			GamePresenceRankData gamePresenceRankData = new GamePresenceRankData();
			DeserializeLengthDelimited(stream, gamePresenceRankData);
			return gamePresenceRankData;
		}

		public static GamePresenceRankData DeserializeLengthDelimited(Stream stream, GamePresenceRankData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GamePresenceRankData Deserialize(Stream stream, GamePresenceRankData instance, long limit)
		{
			instance.FormatType = FormatType.FT_UNKNOWN;
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
					instance.LeagueId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.StarLevel = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.LegendRank = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GamePresenceRankData instance)
		{
			if (instance.HasLeagueId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.LeagueId);
			}
			if (instance.HasStarLevel)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.StarLevel);
			}
			if (instance.HasLegendRank)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.LegendRank);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FormatType);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasLeagueId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)LeagueId);
			}
			if (HasStarLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)StarLevel);
			}
			if (HasLegendRank)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)LegendRank);
			}
			if (HasFormatType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FormatType);
			}
			return num;
		}
	}
}
