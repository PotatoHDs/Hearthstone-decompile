using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class MedalInfoData : IProtoBuf
	{
		public bool HasLevelStartDeprecated;

		private int _LevelStartDeprecated;

		public bool HasLevelEndDeprecated;

		private int _LevelEndDeprecated;

		public bool HasCanLoseLevelDeprecated;

		private bool _CanLoseLevelDeprecated;

		public bool HasLegendRank;

		private int _LegendRank;

		public bool HasBestStarLevel;

		private int _BestStarLevel;

		public bool HasCanLoseStarsDeprecated;

		private bool _CanLoseStarsDeprecated;

		public bool HasSeasonGames;

		private int _SeasonGames;

		public bool HasStarsPerWin;

		private int _StarsPerWin;

		public bool HasRatingId;

		private int _RatingId;

		public bool HasSeasonId;

		private int _SeasonId;

		public bool HasRating;

		private float _Rating;

		public bool HasVariance;

		private float _Variance;

		public bool HasBestStars;

		private int _BestStars;

		public bool HasBestEverLeagueId;

		private int _BestEverLeagueId;

		public bool HasBestEverStarLevel;

		private int _BestEverStarLevel;

		public bool HasBestRating;

		private float _BestRating;

		public bool HasPublicRating;

		private float _PublicRating;

		public bool HasRatingAdjustment;

		private float _RatingAdjustment;

		public bool HasRatingAdjustmentWins;

		private int _RatingAdjustmentWins;

		public bool HasFormatType;

		private FormatType _FormatType;

		public int SeasonWins { get; set; }

		public int Stars { get; set; }

		public int Streak { get; set; }

		public int StarLevel { get; set; }

		public int LevelStartDeprecated
		{
			get
			{
				return _LevelStartDeprecated;
			}
			set
			{
				_LevelStartDeprecated = value;
				HasLevelStartDeprecated = true;
			}
		}

		public int LevelEndDeprecated
		{
			get
			{
				return _LevelEndDeprecated;
			}
			set
			{
				_LevelEndDeprecated = value;
				HasLevelEndDeprecated = true;
			}
		}

		public bool CanLoseLevelDeprecated
		{
			get
			{
				return _CanLoseLevelDeprecated;
			}
			set
			{
				_CanLoseLevelDeprecated = value;
				HasCanLoseLevelDeprecated = true;
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

		public int BestStarLevel
		{
			get
			{
				return _BestStarLevel;
			}
			set
			{
				_BestStarLevel = value;
				HasBestStarLevel = true;
			}
		}

		public bool CanLoseStarsDeprecated
		{
			get
			{
				return _CanLoseStarsDeprecated;
			}
			set
			{
				_CanLoseStarsDeprecated = value;
				HasCanLoseStarsDeprecated = true;
			}
		}

		public int SeasonGames
		{
			get
			{
				return _SeasonGames;
			}
			set
			{
				_SeasonGames = value;
				HasSeasonGames = true;
			}
		}

		public int LeagueId { get; set; }

		public int StarsPerWin
		{
			get
			{
				return _StarsPerWin;
			}
			set
			{
				_StarsPerWin = value;
				HasStarsPerWin = true;
			}
		}

		public int RatingId
		{
			get
			{
				return _RatingId;
			}
			set
			{
				_RatingId = value;
				HasRatingId = true;
			}
		}

		public int SeasonId
		{
			get
			{
				return _SeasonId;
			}
			set
			{
				_SeasonId = value;
				HasSeasonId = true;
			}
		}

		public float Rating
		{
			get
			{
				return _Rating;
			}
			set
			{
				_Rating = value;
				HasRating = true;
			}
		}

		public float Variance
		{
			get
			{
				return _Variance;
			}
			set
			{
				_Variance = value;
				HasVariance = true;
			}
		}

		public int BestStars
		{
			get
			{
				return _BestStars;
			}
			set
			{
				_BestStars = value;
				HasBestStars = true;
			}
		}

		public int BestEverLeagueId
		{
			get
			{
				return _BestEverLeagueId;
			}
			set
			{
				_BestEverLeagueId = value;
				HasBestEverLeagueId = true;
			}
		}

		public int BestEverStarLevel
		{
			get
			{
				return _BestEverStarLevel;
			}
			set
			{
				_BestEverStarLevel = value;
				HasBestEverStarLevel = true;
			}
		}

		public float BestRating
		{
			get
			{
				return _BestRating;
			}
			set
			{
				_BestRating = value;
				HasBestRating = true;
			}
		}

		public float PublicRating
		{
			get
			{
				return _PublicRating;
			}
			set
			{
				_PublicRating = value;
				HasPublicRating = true;
			}
		}

		public float RatingAdjustment
		{
			get
			{
				return _RatingAdjustment;
			}
			set
			{
				_RatingAdjustment = value;
				HasRatingAdjustment = true;
			}
		}

		public int RatingAdjustmentWins
		{
			get
			{
				return _RatingAdjustmentWins;
			}
			set
			{
				_RatingAdjustmentWins = value;
				HasRatingAdjustmentWins = true;
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
			int hashCode = GetType().GetHashCode();
			hashCode ^= SeasonWins.GetHashCode();
			hashCode ^= Stars.GetHashCode();
			hashCode ^= Streak.GetHashCode();
			hashCode ^= StarLevel.GetHashCode();
			if (HasLevelStartDeprecated)
			{
				hashCode ^= LevelStartDeprecated.GetHashCode();
			}
			if (HasLevelEndDeprecated)
			{
				hashCode ^= LevelEndDeprecated.GetHashCode();
			}
			if (HasCanLoseLevelDeprecated)
			{
				hashCode ^= CanLoseLevelDeprecated.GetHashCode();
			}
			if (HasLegendRank)
			{
				hashCode ^= LegendRank.GetHashCode();
			}
			if (HasBestStarLevel)
			{
				hashCode ^= BestStarLevel.GetHashCode();
			}
			if (HasCanLoseStarsDeprecated)
			{
				hashCode ^= CanLoseStarsDeprecated.GetHashCode();
			}
			if (HasSeasonGames)
			{
				hashCode ^= SeasonGames.GetHashCode();
			}
			hashCode ^= LeagueId.GetHashCode();
			if (HasStarsPerWin)
			{
				hashCode ^= StarsPerWin.GetHashCode();
			}
			if (HasRatingId)
			{
				hashCode ^= RatingId.GetHashCode();
			}
			if (HasSeasonId)
			{
				hashCode ^= SeasonId.GetHashCode();
			}
			if (HasRating)
			{
				hashCode ^= Rating.GetHashCode();
			}
			if (HasVariance)
			{
				hashCode ^= Variance.GetHashCode();
			}
			if (HasBestStars)
			{
				hashCode ^= BestStars.GetHashCode();
			}
			if (HasBestEverLeagueId)
			{
				hashCode ^= BestEverLeagueId.GetHashCode();
			}
			if (HasBestEverStarLevel)
			{
				hashCode ^= BestEverStarLevel.GetHashCode();
			}
			if (HasBestRating)
			{
				hashCode ^= BestRating.GetHashCode();
			}
			if (HasPublicRating)
			{
				hashCode ^= PublicRating.GetHashCode();
			}
			if (HasRatingAdjustment)
			{
				hashCode ^= RatingAdjustment.GetHashCode();
			}
			if (HasRatingAdjustmentWins)
			{
				hashCode ^= RatingAdjustmentWins.GetHashCode();
			}
			if (HasFormatType)
			{
				hashCode ^= FormatType.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			MedalInfoData medalInfoData = obj as MedalInfoData;
			if (medalInfoData == null)
			{
				return false;
			}
			if (!SeasonWins.Equals(medalInfoData.SeasonWins))
			{
				return false;
			}
			if (!Stars.Equals(medalInfoData.Stars))
			{
				return false;
			}
			if (!Streak.Equals(medalInfoData.Streak))
			{
				return false;
			}
			if (!StarLevel.Equals(medalInfoData.StarLevel))
			{
				return false;
			}
			if (HasLevelStartDeprecated != medalInfoData.HasLevelStartDeprecated || (HasLevelStartDeprecated && !LevelStartDeprecated.Equals(medalInfoData.LevelStartDeprecated)))
			{
				return false;
			}
			if (HasLevelEndDeprecated != medalInfoData.HasLevelEndDeprecated || (HasLevelEndDeprecated && !LevelEndDeprecated.Equals(medalInfoData.LevelEndDeprecated)))
			{
				return false;
			}
			if (HasCanLoseLevelDeprecated != medalInfoData.HasCanLoseLevelDeprecated || (HasCanLoseLevelDeprecated && !CanLoseLevelDeprecated.Equals(medalInfoData.CanLoseLevelDeprecated)))
			{
				return false;
			}
			if (HasLegendRank != medalInfoData.HasLegendRank || (HasLegendRank && !LegendRank.Equals(medalInfoData.LegendRank)))
			{
				return false;
			}
			if (HasBestStarLevel != medalInfoData.HasBestStarLevel || (HasBestStarLevel && !BestStarLevel.Equals(medalInfoData.BestStarLevel)))
			{
				return false;
			}
			if (HasCanLoseStarsDeprecated != medalInfoData.HasCanLoseStarsDeprecated || (HasCanLoseStarsDeprecated && !CanLoseStarsDeprecated.Equals(medalInfoData.CanLoseStarsDeprecated)))
			{
				return false;
			}
			if (HasSeasonGames != medalInfoData.HasSeasonGames || (HasSeasonGames && !SeasonGames.Equals(medalInfoData.SeasonGames)))
			{
				return false;
			}
			if (!LeagueId.Equals(medalInfoData.LeagueId))
			{
				return false;
			}
			if (HasStarsPerWin != medalInfoData.HasStarsPerWin || (HasStarsPerWin && !StarsPerWin.Equals(medalInfoData.StarsPerWin)))
			{
				return false;
			}
			if (HasRatingId != medalInfoData.HasRatingId || (HasRatingId && !RatingId.Equals(medalInfoData.RatingId)))
			{
				return false;
			}
			if (HasSeasonId != medalInfoData.HasSeasonId || (HasSeasonId && !SeasonId.Equals(medalInfoData.SeasonId)))
			{
				return false;
			}
			if (HasRating != medalInfoData.HasRating || (HasRating && !Rating.Equals(medalInfoData.Rating)))
			{
				return false;
			}
			if (HasVariance != medalInfoData.HasVariance || (HasVariance && !Variance.Equals(medalInfoData.Variance)))
			{
				return false;
			}
			if (HasBestStars != medalInfoData.HasBestStars || (HasBestStars && !BestStars.Equals(medalInfoData.BestStars)))
			{
				return false;
			}
			if (HasBestEverLeagueId != medalInfoData.HasBestEverLeagueId || (HasBestEverLeagueId && !BestEverLeagueId.Equals(medalInfoData.BestEverLeagueId)))
			{
				return false;
			}
			if (HasBestEverStarLevel != medalInfoData.HasBestEverStarLevel || (HasBestEverStarLevel && !BestEverStarLevel.Equals(medalInfoData.BestEverStarLevel)))
			{
				return false;
			}
			if (HasBestRating != medalInfoData.HasBestRating || (HasBestRating && !BestRating.Equals(medalInfoData.BestRating)))
			{
				return false;
			}
			if (HasPublicRating != medalInfoData.HasPublicRating || (HasPublicRating && !PublicRating.Equals(medalInfoData.PublicRating)))
			{
				return false;
			}
			if (HasRatingAdjustment != medalInfoData.HasRatingAdjustment || (HasRatingAdjustment && !RatingAdjustment.Equals(medalInfoData.RatingAdjustment)))
			{
				return false;
			}
			if (HasRatingAdjustmentWins != medalInfoData.HasRatingAdjustmentWins || (HasRatingAdjustmentWins && !RatingAdjustmentWins.Equals(medalInfoData.RatingAdjustmentWins)))
			{
				return false;
			}
			if (HasFormatType != medalInfoData.HasFormatType || (HasFormatType && !FormatType.Equals(medalInfoData.FormatType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MedalInfoData Deserialize(Stream stream, MedalInfoData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MedalInfoData DeserializeLengthDelimited(Stream stream)
		{
			MedalInfoData medalInfoData = new MedalInfoData();
			DeserializeLengthDelimited(stream, medalInfoData);
			return medalInfoData;
		}

		public static MedalInfoData DeserializeLengthDelimited(Stream stream, MedalInfoData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MedalInfoData Deserialize(Stream stream, MedalInfoData instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 24:
					instance.SeasonWins = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.Stars = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.Streak = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.StarLevel = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.LevelStartDeprecated = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 80:
					instance.LevelEndDeprecated = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 88:
					instance.CanLoseLevelDeprecated = ProtocolParser.ReadBool(stream);
					continue;
				case 104:
					instance.LegendRank = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 112:
					instance.BestStarLevel = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 120:
					instance.CanLoseStarsDeprecated = ProtocolParser.ReadBool(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 16u:
						if (key.WireType == Wire.Varint)
						{
							instance.SeasonGames = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 17u:
						if (key.WireType == Wire.Varint)
						{
							instance.LeagueId = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 18u:
						if (key.WireType == Wire.Varint)
						{
							instance.StarsPerWin = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 19u:
						if (key.WireType == Wire.Varint)
						{
							instance.RatingId = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 20u:
						if (key.WireType == Wire.Varint)
						{
							instance.SeasonId = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 21u:
						if (key.WireType == Wire.Fixed32)
						{
							instance.Rating = binaryReader.ReadSingle();
						}
						break;
					case 22u:
						if (key.WireType == Wire.Fixed32)
						{
							instance.Variance = binaryReader.ReadSingle();
						}
						break;
					case 23u:
						if (key.WireType == Wire.Varint)
						{
							instance.BestStars = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 24u:
						if (key.WireType == Wire.Varint)
						{
							instance.BestEverLeagueId = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 25u:
						if (key.WireType == Wire.Varint)
						{
							instance.BestEverStarLevel = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 26u:
						if (key.WireType == Wire.Fixed32)
						{
							instance.BestRating = binaryReader.ReadSingle();
						}
						break;
					case 27u:
						if (key.WireType == Wire.Fixed32)
						{
							instance.PublicRating = binaryReader.ReadSingle();
						}
						break;
					case 28u:
						if (key.WireType == Wire.Fixed32)
						{
							instance.RatingAdjustment = binaryReader.ReadSingle();
						}
						break;
					case 29u:
						if (key.WireType == Wire.Varint)
						{
							instance.RatingAdjustmentWins = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 30u:
						if (key.WireType == Wire.Varint)
						{
							instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
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

		public static void Serialize(Stream stream, MedalInfoData instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SeasonWins);
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Stars);
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Streak);
			stream.WriteByte(64);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.StarLevel);
			if (instance.HasLevelStartDeprecated)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.LevelStartDeprecated);
			}
			if (instance.HasLevelEndDeprecated)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.LevelEndDeprecated);
			}
			if (instance.HasCanLoseLevelDeprecated)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteBool(stream, instance.CanLoseLevelDeprecated);
			}
			if (instance.HasLegendRank)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.LegendRank);
			}
			if (instance.HasBestStarLevel)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BestStarLevel);
			}
			if (instance.HasCanLoseStarsDeprecated)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteBool(stream, instance.CanLoseStarsDeprecated);
			}
			if (instance.HasSeasonGames)
			{
				stream.WriteByte(128);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SeasonGames);
			}
			stream.WriteByte(136);
			stream.WriteByte(1);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.LeagueId);
			if (instance.HasStarsPerWin)
			{
				stream.WriteByte(144);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.StarsPerWin);
			}
			if (instance.HasRatingId)
			{
				stream.WriteByte(152);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RatingId);
			}
			if (instance.HasSeasonId)
			{
				stream.WriteByte(160);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SeasonId);
			}
			if (instance.HasRating)
			{
				stream.WriteByte(173);
				stream.WriteByte(1);
				binaryWriter.Write(instance.Rating);
			}
			if (instance.HasVariance)
			{
				stream.WriteByte(181);
				stream.WriteByte(1);
				binaryWriter.Write(instance.Variance);
			}
			if (instance.HasBestStars)
			{
				stream.WriteByte(184);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BestStars);
			}
			if (instance.HasBestEverLeagueId)
			{
				stream.WriteByte(192);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BestEverLeagueId);
			}
			if (instance.HasBestEverStarLevel)
			{
				stream.WriteByte(200);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BestEverStarLevel);
			}
			if (instance.HasBestRating)
			{
				stream.WriteByte(213);
				stream.WriteByte(1);
				binaryWriter.Write(instance.BestRating);
			}
			if (instance.HasPublicRating)
			{
				stream.WriteByte(221);
				stream.WriteByte(1);
				binaryWriter.Write(instance.PublicRating);
			}
			if (instance.HasRatingAdjustment)
			{
				stream.WriteByte(229);
				stream.WriteByte(1);
				binaryWriter.Write(instance.RatingAdjustment);
			}
			if (instance.HasRatingAdjustmentWins)
			{
				stream.WriteByte(232);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RatingAdjustmentWins);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(240);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FormatType);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)SeasonWins);
			num += ProtocolParser.SizeOfUInt64((ulong)Stars);
			num += ProtocolParser.SizeOfUInt64((ulong)Streak);
			num += ProtocolParser.SizeOfUInt64((ulong)StarLevel);
			if (HasLevelStartDeprecated)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)LevelStartDeprecated);
			}
			if (HasLevelEndDeprecated)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)LevelEndDeprecated);
			}
			if (HasCanLoseLevelDeprecated)
			{
				num++;
				num++;
			}
			if (HasLegendRank)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)LegendRank);
			}
			if (HasBestStarLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BestStarLevel);
			}
			if (HasCanLoseStarsDeprecated)
			{
				num++;
				num++;
			}
			if (HasSeasonGames)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)SeasonGames);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)LeagueId);
			if (HasStarsPerWin)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)StarsPerWin);
			}
			if (HasRatingId)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)RatingId);
			}
			if (HasSeasonId)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)SeasonId);
			}
			if (HasRating)
			{
				num += 2;
				num += 4;
			}
			if (HasVariance)
			{
				num += 2;
				num += 4;
			}
			if (HasBestStars)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)BestStars);
			}
			if (HasBestEverLeagueId)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)BestEverLeagueId);
			}
			if (HasBestEverStarLevel)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)BestEverStarLevel);
			}
			if (HasBestRating)
			{
				num += 2;
				num += 4;
			}
			if (HasPublicRating)
			{
				num += 2;
				num += 4;
			}
			if (HasRatingAdjustment)
			{
				num += 2;
				num += 4;
			}
			if (HasRatingAdjustmentWins)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)RatingAdjustmentWins);
			}
			if (HasFormatType)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)FormatType);
			}
			return num + 6;
		}
	}
}
