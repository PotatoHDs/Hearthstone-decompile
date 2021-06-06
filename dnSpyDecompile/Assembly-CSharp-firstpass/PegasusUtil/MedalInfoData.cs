using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000A8 RID: 168
	public class MedalInfoData : IProtoBuf
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0002A0F3 File Offset: 0x000282F3
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x0002A0FB File Offset: 0x000282FB
		public int SeasonWins { get; set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0002A104 File Offset: 0x00028304
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x0002A10C File Offset: 0x0002830C
		public int Stars { get; set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0002A115 File Offset: 0x00028315
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x0002A11D File Offset: 0x0002831D
		public int Streak { get; set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0002A126 File Offset: 0x00028326
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x0002A12E File Offset: 0x0002832E
		public int StarLevel { get; set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0002A137 File Offset: 0x00028337
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x0002A13F File Offset: 0x0002833F
		public int LevelStartDeprecated
		{
			get
			{
				return this._LevelStartDeprecated;
			}
			set
			{
				this._LevelStartDeprecated = value;
				this.HasLevelStartDeprecated = true;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0002A14F File Offset: 0x0002834F
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x0002A157 File Offset: 0x00028357
		public int LevelEndDeprecated
		{
			get
			{
				return this._LevelEndDeprecated;
			}
			set
			{
				this._LevelEndDeprecated = value;
				this.HasLevelEndDeprecated = true;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0002A167 File Offset: 0x00028367
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x0002A16F File Offset: 0x0002836F
		public bool CanLoseLevelDeprecated
		{
			get
			{
				return this._CanLoseLevelDeprecated;
			}
			set
			{
				this._CanLoseLevelDeprecated = value;
				this.HasCanLoseLevelDeprecated = true;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0002A17F File Offset: 0x0002837F
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x0002A187 File Offset: 0x00028387
		public int LegendRank
		{
			get
			{
				return this._LegendRank;
			}
			set
			{
				this._LegendRank = value;
				this.HasLegendRank = true;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0002A197 File Offset: 0x00028397
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x0002A19F File Offset: 0x0002839F
		public int BestStarLevel
		{
			get
			{
				return this._BestStarLevel;
			}
			set
			{
				this._BestStarLevel = value;
				this.HasBestStarLevel = true;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0002A1AF File Offset: 0x000283AF
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x0002A1B7 File Offset: 0x000283B7
		public bool CanLoseStarsDeprecated
		{
			get
			{
				return this._CanLoseStarsDeprecated;
			}
			set
			{
				this._CanLoseStarsDeprecated = value;
				this.HasCanLoseStarsDeprecated = true;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0002A1C7 File Offset: 0x000283C7
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x0002A1CF File Offset: 0x000283CF
		public int SeasonGames
		{
			get
			{
				return this._SeasonGames;
			}
			set
			{
				this._SeasonGames = value;
				this.HasSeasonGames = true;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0002A1DF File Offset: 0x000283DF
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x0002A1E7 File Offset: 0x000283E7
		public int LeagueId { get; set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0002A1F0 File Offset: 0x000283F0
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x0002A1F8 File Offset: 0x000283F8
		public int StarsPerWin
		{
			get
			{
				return this._StarsPerWin;
			}
			set
			{
				this._StarsPerWin = value;
				this.HasStarsPerWin = true;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0002A208 File Offset: 0x00028408
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x0002A210 File Offset: 0x00028410
		public int RatingId
		{
			get
			{
				return this._RatingId;
			}
			set
			{
				this._RatingId = value;
				this.HasRatingId = true;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0002A220 File Offset: 0x00028420
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x0002A228 File Offset: 0x00028428
		public int SeasonId
		{
			get
			{
				return this._SeasonId;
			}
			set
			{
				this._SeasonId = value;
				this.HasSeasonId = true;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0002A238 File Offset: 0x00028438
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x0002A240 File Offset: 0x00028440
		public float Rating
		{
			get
			{
				return this._Rating;
			}
			set
			{
				this._Rating = value;
				this.HasRating = true;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0002A250 File Offset: 0x00028450
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x0002A258 File Offset: 0x00028458
		public float Variance
		{
			get
			{
				return this._Variance;
			}
			set
			{
				this._Variance = value;
				this.HasVariance = true;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0002A268 File Offset: 0x00028468
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x0002A270 File Offset: 0x00028470
		public int BestStars
		{
			get
			{
				return this._BestStars;
			}
			set
			{
				this._BestStars = value;
				this.HasBestStars = true;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0002A280 File Offset: 0x00028480
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x0002A288 File Offset: 0x00028488
		public int BestEverLeagueId
		{
			get
			{
				return this._BestEverLeagueId;
			}
			set
			{
				this._BestEverLeagueId = value;
				this.HasBestEverLeagueId = true;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0002A298 File Offset: 0x00028498
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x0002A2A0 File Offset: 0x000284A0
		public int BestEverStarLevel
		{
			get
			{
				return this._BestEverStarLevel;
			}
			set
			{
				this._BestEverStarLevel = value;
				this.HasBestEverStarLevel = true;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0002A2B0 File Offset: 0x000284B0
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x0002A2B8 File Offset: 0x000284B8
		public float BestRating
		{
			get
			{
				return this._BestRating;
			}
			set
			{
				this._BestRating = value;
				this.HasBestRating = true;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002A2C8 File Offset: 0x000284C8
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x0002A2D0 File Offset: 0x000284D0
		public float PublicRating
		{
			get
			{
				return this._PublicRating;
			}
			set
			{
				this._PublicRating = value;
				this.HasPublicRating = true;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0002A2E0 File Offset: 0x000284E0
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x0002A2E8 File Offset: 0x000284E8
		public float RatingAdjustment
		{
			get
			{
				return this._RatingAdjustment;
			}
			set
			{
				this._RatingAdjustment = value;
				this.HasRatingAdjustment = true;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0002A2F8 File Offset: 0x000284F8
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x0002A300 File Offset: 0x00028500
		public int RatingAdjustmentWins
		{
			get
			{
				return this._RatingAdjustmentWins;
			}
			set
			{
				this._RatingAdjustmentWins = value;
				this.HasRatingAdjustmentWins = true;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0002A310 File Offset: 0x00028510
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x0002A318 File Offset: 0x00028518
		public FormatType FormatType
		{
			get
			{
				return this._FormatType;
			}
			set
			{
				this._FormatType = value;
				this.HasFormatType = true;
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002A328 File Offset: 0x00028528
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.SeasonWins.GetHashCode();
			num ^= this.Stars.GetHashCode();
			num ^= this.Streak.GetHashCode();
			num ^= this.StarLevel.GetHashCode();
			if (this.HasLevelStartDeprecated)
			{
				num ^= this.LevelStartDeprecated.GetHashCode();
			}
			if (this.HasLevelEndDeprecated)
			{
				num ^= this.LevelEndDeprecated.GetHashCode();
			}
			if (this.HasCanLoseLevelDeprecated)
			{
				num ^= this.CanLoseLevelDeprecated.GetHashCode();
			}
			if (this.HasLegendRank)
			{
				num ^= this.LegendRank.GetHashCode();
			}
			if (this.HasBestStarLevel)
			{
				num ^= this.BestStarLevel.GetHashCode();
			}
			if (this.HasCanLoseStarsDeprecated)
			{
				num ^= this.CanLoseStarsDeprecated.GetHashCode();
			}
			if (this.HasSeasonGames)
			{
				num ^= this.SeasonGames.GetHashCode();
			}
			num ^= this.LeagueId.GetHashCode();
			if (this.HasStarsPerWin)
			{
				num ^= this.StarsPerWin.GetHashCode();
			}
			if (this.HasRatingId)
			{
				num ^= this.RatingId.GetHashCode();
			}
			if (this.HasSeasonId)
			{
				num ^= this.SeasonId.GetHashCode();
			}
			if (this.HasRating)
			{
				num ^= this.Rating.GetHashCode();
			}
			if (this.HasVariance)
			{
				num ^= this.Variance.GetHashCode();
			}
			if (this.HasBestStars)
			{
				num ^= this.BestStars.GetHashCode();
			}
			if (this.HasBestEverLeagueId)
			{
				num ^= this.BestEverLeagueId.GetHashCode();
			}
			if (this.HasBestEverStarLevel)
			{
				num ^= this.BestEverStarLevel.GetHashCode();
			}
			if (this.HasBestRating)
			{
				num ^= this.BestRating.GetHashCode();
			}
			if (this.HasPublicRating)
			{
				num ^= this.PublicRating.GetHashCode();
			}
			if (this.HasRatingAdjustment)
			{
				num ^= this.RatingAdjustment.GetHashCode();
			}
			if (this.HasRatingAdjustmentWins)
			{
				num ^= this.RatingAdjustmentWins.GetHashCode();
			}
			if (this.HasFormatType)
			{
				num ^= this.FormatType.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002A594 File Offset: 0x00028794
		public override bool Equals(object obj)
		{
			MedalInfoData medalInfoData = obj as MedalInfoData;
			return medalInfoData != null && this.SeasonWins.Equals(medalInfoData.SeasonWins) && this.Stars.Equals(medalInfoData.Stars) && this.Streak.Equals(medalInfoData.Streak) && this.StarLevel.Equals(medalInfoData.StarLevel) && this.HasLevelStartDeprecated == medalInfoData.HasLevelStartDeprecated && (!this.HasLevelStartDeprecated || this.LevelStartDeprecated.Equals(medalInfoData.LevelStartDeprecated)) && this.HasLevelEndDeprecated == medalInfoData.HasLevelEndDeprecated && (!this.HasLevelEndDeprecated || this.LevelEndDeprecated.Equals(medalInfoData.LevelEndDeprecated)) && this.HasCanLoseLevelDeprecated == medalInfoData.HasCanLoseLevelDeprecated && (!this.HasCanLoseLevelDeprecated || this.CanLoseLevelDeprecated.Equals(medalInfoData.CanLoseLevelDeprecated)) && this.HasLegendRank == medalInfoData.HasLegendRank && (!this.HasLegendRank || this.LegendRank.Equals(medalInfoData.LegendRank)) && this.HasBestStarLevel == medalInfoData.HasBestStarLevel && (!this.HasBestStarLevel || this.BestStarLevel.Equals(medalInfoData.BestStarLevel)) && this.HasCanLoseStarsDeprecated == medalInfoData.HasCanLoseStarsDeprecated && (!this.HasCanLoseStarsDeprecated || this.CanLoseStarsDeprecated.Equals(medalInfoData.CanLoseStarsDeprecated)) && this.HasSeasonGames == medalInfoData.HasSeasonGames && (!this.HasSeasonGames || this.SeasonGames.Equals(medalInfoData.SeasonGames)) && this.LeagueId.Equals(medalInfoData.LeagueId) && this.HasStarsPerWin == medalInfoData.HasStarsPerWin && (!this.HasStarsPerWin || this.StarsPerWin.Equals(medalInfoData.StarsPerWin)) && this.HasRatingId == medalInfoData.HasRatingId && (!this.HasRatingId || this.RatingId.Equals(medalInfoData.RatingId)) && this.HasSeasonId == medalInfoData.HasSeasonId && (!this.HasSeasonId || this.SeasonId.Equals(medalInfoData.SeasonId)) && this.HasRating == medalInfoData.HasRating && (!this.HasRating || this.Rating.Equals(medalInfoData.Rating)) && this.HasVariance == medalInfoData.HasVariance && (!this.HasVariance || this.Variance.Equals(medalInfoData.Variance)) && this.HasBestStars == medalInfoData.HasBestStars && (!this.HasBestStars || this.BestStars.Equals(medalInfoData.BestStars)) && this.HasBestEverLeagueId == medalInfoData.HasBestEverLeagueId && (!this.HasBestEverLeagueId || this.BestEverLeagueId.Equals(medalInfoData.BestEverLeagueId)) && this.HasBestEverStarLevel == medalInfoData.HasBestEverStarLevel && (!this.HasBestEverStarLevel || this.BestEverStarLevel.Equals(medalInfoData.BestEverStarLevel)) && this.HasBestRating == medalInfoData.HasBestRating && (!this.HasBestRating || this.BestRating.Equals(medalInfoData.BestRating)) && this.HasPublicRating == medalInfoData.HasPublicRating && (!this.HasPublicRating || this.PublicRating.Equals(medalInfoData.PublicRating)) && this.HasRatingAdjustment == medalInfoData.HasRatingAdjustment && (!this.HasRatingAdjustment || this.RatingAdjustment.Equals(medalInfoData.RatingAdjustment)) && this.HasRatingAdjustmentWins == medalInfoData.HasRatingAdjustmentWins && (!this.HasRatingAdjustmentWins || this.RatingAdjustmentWins.Equals(medalInfoData.RatingAdjustmentWins)) && this.HasFormatType == medalInfoData.HasFormatType && (!this.HasFormatType || this.FormatType.Equals(medalInfoData.FormatType));
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002A9CA File Offset: 0x00028BCA
		public void Deserialize(Stream stream)
		{
			MedalInfoData.Deserialize(stream, this);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002A9D4 File Offset: 0x00028BD4
		public static MedalInfoData Deserialize(Stream stream, MedalInfoData instance)
		{
			return MedalInfoData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002A9E0 File Offset: 0x00028BE0
		public static MedalInfoData DeserializeLengthDelimited(Stream stream)
		{
			MedalInfoData medalInfoData = new MedalInfoData();
			MedalInfoData.DeserializeLengthDelimited(stream, medalInfoData);
			return medalInfoData;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002A9FC File Offset: 0x00028BFC
		public static MedalInfoData DeserializeLengthDelimited(Stream stream, MedalInfoData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MedalInfoData.Deserialize(stream, instance, num);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002AA24 File Offset: 0x00028C24
		public static MedalInfoData Deserialize(Stream stream, MedalInfoData instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.FormatType = FormatType.FT_UNKNOWN;
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else
				{
					if (num <= 72)
					{
						if (num <= 48)
						{
							if (num == 24)
							{
								instance.SeasonWins = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 48)
							{
								instance.Stars = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 56)
							{
								instance.Streak = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 64)
							{
								instance.StarLevel = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 72)
							{
								instance.LevelStartDeprecated = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 88)
					{
						if (num == 80)
						{
							instance.LevelEndDeprecated = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 88)
						{
							instance.CanLoseLevelDeprecated = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 104)
						{
							instance.LegendRank = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 112)
						{
							instance.BestStarLevel = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 120)
						{
							instance.CanLoseStarsDeprecated = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0U:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 16U:
						if (key.WireType == Wire.Varint)
						{
							instance.SeasonGames = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 17U:
						if (key.WireType == Wire.Varint)
						{
							instance.LeagueId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 18U:
						if (key.WireType == Wire.Varint)
						{
							instance.StarsPerWin = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 19U:
						if (key.WireType == Wire.Varint)
						{
							instance.RatingId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 20U:
						if (key.WireType == Wire.Varint)
						{
							instance.SeasonId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 21U:
						if (key.WireType == Wire.Fixed32)
						{
							instance.Rating = binaryReader.ReadSingle();
							continue;
						}
						continue;
					case 22U:
						if (key.WireType == Wire.Fixed32)
						{
							instance.Variance = binaryReader.ReadSingle();
							continue;
						}
						continue;
					case 23U:
						if (key.WireType == Wire.Varint)
						{
							instance.BestStars = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 24U:
						if (key.WireType == Wire.Varint)
						{
							instance.BestEverLeagueId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 25U:
						if (key.WireType == Wire.Varint)
						{
							instance.BestEverStarLevel = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 26U:
						if (key.WireType == Wire.Fixed32)
						{
							instance.BestRating = binaryReader.ReadSingle();
							continue;
						}
						continue;
					case 27U:
						if (key.WireType == Wire.Fixed32)
						{
							instance.PublicRating = binaryReader.ReadSingle();
							continue;
						}
						continue;
					case 28U:
						if (key.WireType == Wire.Fixed32)
						{
							instance.RatingAdjustment = binaryReader.ReadSingle();
							continue;
						}
						continue;
					case 29U:
						if (key.WireType == Wire.Varint)
						{
							instance.RatingAdjustmentWins = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					case 30U:
						if (key.WireType == Wire.Varint)
						{
							instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						continue;
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002ADF8 File Offset: 0x00028FF8
		public void Serialize(Stream stream)
		{
			MedalInfoData.Serialize(stream, this);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002AE04 File Offset: 0x00029004
		public static void Serialize(Stream stream, MedalInfoData instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SeasonWins));
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Stars));
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Streak));
			stream.WriteByte(64);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.StarLevel));
			if (instance.HasLevelStartDeprecated)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.LevelStartDeprecated));
			}
			if (instance.HasLevelEndDeprecated)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.LevelEndDeprecated));
			}
			if (instance.HasCanLoseLevelDeprecated)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteBool(stream, instance.CanLoseLevelDeprecated);
			}
			if (instance.HasLegendRank)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.LegendRank));
			}
			if (instance.HasBestStarLevel)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BestStarLevel));
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
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SeasonGames));
			}
			stream.WriteByte(136);
			stream.WriteByte(1);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.LeagueId));
			if (instance.HasStarsPerWin)
			{
				stream.WriteByte(144);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.StarsPerWin));
			}
			if (instance.HasRatingId)
			{
				stream.WriteByte(152);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RatingId));
			}
			if (instance.HasSeasonId)
			{
				stream.WriteByte(160);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SeasonId));
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
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BestStars));
			}
			if (instance.HasBestEverLeagueId)
			{
				stream.WriteByte(192);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BestEverLeagueId));
			}
			if (instance.HasBestEverStarLevel)
			{
				stream.WriteByte(200);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BestEverStarLevel));
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
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RatingAdjustmentWins));
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(240);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002B154 File Offset: 0x00029354
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SeasonWins));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Stars));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Streak));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.StarLevel));
			if (this.HasLevelStartDeprecated)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.LevelStartDeprecated));
			}
			if (this.HasLevelEndDeprecated)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.LevelEndDeprecated));
			}
			if (this.HasCanLoseLevelDeprecated)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasLegendRank)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.LegendRank));
			}
			if (this.HasBestStarLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BestStarLevel));
			}
			if (this.HasCanLoseStarsDeprecated)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasSeasonGames)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SeasonGames));
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.LeagueId));
			if (this.HasStarsPerWin)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.StarsPerWin));
			}
			if (this.HasRatingId)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RatingId));
			}
			if (this.HasSeasonId)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SeasonId));
			}
			if (this.HasRating)
			{
				num += 2U;
				num += 4U;
			}
			if (this.HasVariance)
			{
				num += 2U;
				num += 4U;
			}
			if (this.HasBestStars)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BestStars));
			}
			if (this.HasBestEverLeagueId)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BestEverLeagueId));
			}
			if (this.HasBestEverStarLevel)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BestEverStarLevel));
			}
			if (this.HasBestRating)
			{
				num += 2U;
				num += 4U;
			}
			if (this.HasPublicRating)
			{
				num += 2U;
				num += 4U;
			}
			if (this.HasRatingAdjustment)
			{
				num += 2U;
				num += 4U;
			}
			if (this.HasRatingAdjustmentWins)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RatingAdjustmentWins));
			}
			if (this.HasFormatType)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			}
			return num + 6U;
		}

		// Token: 0x040003D3 RID: 979
		public bool HasLevelStartDeprecated;

		// Token: 0x040003D4 RID: 980
		private int _LevelStartDeprecated;

		// Token: 0x040003D5 RID: 981
		public bool HasLevelEndDeprecated;

		// Token: 0x040003D6 RID: 982
		private int _LevelEndDeprecated;

		// Token: 0x040003D7 RID: 983
		public bool HasCanLoseLevelDeprecated;

		// Token: 0x040003D8 RID: 984
		private bool _CanLoseLevelDeprecated;

		// Token: 0x040003D9 RID: 985
		public bool HasLegendRank;

		// Token: 0x040003DA RID: 986
		private int _LegendRank;

		// Token: 0x040003DB RID: 987
		public bool HasBestStarLevel;

		// Token: 0x040003DC RID: 988
		private int _BestStarLevel;

		// Token: 0x040003DD RID: 989
		public bool HasCanLoseStarsDeprecated;

		// Token: 0x040003DE RID: 990
		private bool _CanLoseStarsDeprecated;

		// Token: 0x040003DF RID: 991
		public bool HasSeasonGames;

		// Token: 0x040003E0 RID: 992
		private int _SeasonGames;

		// Token: 0x040003E2 RID: 994
		public bool HasStarsPerWin;

		// Token: 0x040003E3 RID: 995
		private int _StarsPerWin;

		// Token: 0x040003E4 RID: 996
		public bool HasRatingId;

		// Token: 0x040003E5 RID: 997
		private int _RatingId;

		// Token: 0x040003E6 RID: 998
		public bool HasSeasonId;

		// Token: 0x040003E7 RID: 999
		private int _SeasonId;

		// Token: 0x040003E8 RID: 1000
		public bool HasRating;

		// Token: 0x040003E9 RID: 1001
		private float _Rating;

		// Token: 0x040003EA RID: 1002
		public bool HasVariance;

		// Token: 0x040003EB RID: 1003
		private float _Variance;

		// Token: 0x040003EC RID: 1004
		public bool HasBestStars;

		// Token: 0x040003ED RID: 1005
		private int _BestStars;

		// Token: 0x040003EE RID: 1006
		public bool HasBestEverLeagueId;

		// Token: 0x040003EF RID: 1007
		private int _BestEverLeagueId;

		// Token: 0x040003F0 RID: 1008
		public bool HasBestEverStarLevel;

		// Token: 0x040003F1 RID: 1009
		private int _BestEverStarLevel;

		// Token: 0x040003F2 RID: 1010
		public bool HasBestRating;

		// Token: 0x040003F3 RID: 1011
		private float _BestRating;

		// Token: 0x040003F4 RID: 1012
		public bool HasPublicRating;

		// Token: 0x040003F5 RID: 1013
		private float _PublicRating;

		// Token: 0x040003F6 RID: 1014
		public bool HasRatingAdjustment;

		// Token: 0x040003F7 RID: 1015
		private float _RatingAdjustment;

		// Token: 0x040003F8 RID: 1016
		public bool HasRatingAdjustmentWins;

		// Token: 0x040003F9 RID: 1017
		private int _RatingAdjustmentWins;

		// Token: 0x040003FA RID: 1018
		public bool HasFormatType;

		// Token: 0x040003FB RID: 1019
		private FormatType _FormatType;
	}
}
