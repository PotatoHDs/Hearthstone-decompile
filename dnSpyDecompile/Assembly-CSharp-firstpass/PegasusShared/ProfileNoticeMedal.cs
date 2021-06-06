using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000127 RID: 295
	public class ProfileNoticeMedal : IProtoBuf
	{
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x00043B1B File Offset: 0x00041D1B
		// (set) Token: 0x06001370 RID: 4976 RVA: 0x00043B23 File Offset: 0x00041D23
		public int StarLevel { get; set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x00043B2C File Offset: 0x00041D2C
		// (set) Token: 0x06001372 RID: 4978 RVA: 0x00043B34 File Offset: 0x00041D34
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

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x00043B44 File Offset: 0x00041D44
		// (set) Token: 0x06001374 RID: 4980 RVA: 0x00043B4C File Offset: 0x00041D4C
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

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x00043B5C File Offset: 0x00041D5C
		// (set) Token: 0x06001376 RID: 4982 RVA: 0x00043B64 File Offset: 0x00041D64
		public RewardChest Chest
		{
			get
			{
				return this._Chest;
			}
			set
			{
				this._Chest = value;
				this.HasChest = (value != null);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x00043B77 File Offset: 0x00041D77
		// (set) Token: 0x06001378 RID: 4984 RVA: 0x00043B7F File Offset: 0x00041D7F
		public ProfileNoticeMedal.MedalType MedalType_
		{
			get
			{
				return this._MedalType_;
			}
			set
			{
				this._MedalType_ = value;
				this.HasMedalType_ = true;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x00043B8F File Offset: 0x00041D8F
		// (set) Token: 0x0600137A RID: 4986 RVA: 0x00043B97 File Offset: 0x00041D97
		public int LeagueId
		{
			get
			{
				return this._LeagueId;
			}
			set
			{
				this._LeagueId = value;
				this.HasLeagueId = true;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x00043BA7 File Offset: 0x00041DA7
		// (set) Token: 0x0600137C RID: 4988 RVA: 0x00043BAF File Offset: 0x00041DAF
		public bool WasLimitedByBestEverStarLevel
		{
			get
			{
				return this._WasLimitedByBestEverStarLevel;
			}
			set
			{
				this._WasLimitedByBestEverStarLevel = value;
				this.HasWasLimitedByBestEverStarLevel = true;
			}
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00043BC0 File Offset: 0x00041DC0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.StarLevel.GetHashCode();
			if (this.HasLegendRank)
			{
				num ^= this.LegendRank.GetHashCode();
			}
			if (this.HasBestStarLevel)
			{
				num ^= this.BestStarLevel.GetHashCode();
			}
			if (this.HasChest)
			{
				num ^= this.Chest.GetHashCode();
			}
			if (this.HasMedalType_)
			{
				num ^= this.MedalType_.GetHashCode();
			}
			if (this.HasLeagueId)
			{
				num ^= this.LeagueId.GetHashCode();
			}
			if (this.HasWasLimitedByBestEverStarLevel)
			{
				num ^= this.WasLimitedByBestEverStarLevel.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00043C84 File Offset: 0x00041E84
		public override bool Equals(object obj)
		{
			ProfileNoticeMedal profileNoticeMedal = obj as ProfileNoticeMedal;
			return profileNoticeMedal != null && this.StarLevel.Equals(profileNoticeMedal.StarLevel) && this.HasLegendRank == profileNoticeMedal.HasLegendRank && (!this.HasLegendRank || this.LegendRank.Equals(profileNoticeMedal.LegendRank)) && this.HasBestStarLevel == profileNoticeMedal.HasBestStarLevel && (!this.HasBestStarLevel || this.BestStarLevel.Equals(profileNoticeMedal.BestStarLevel)) && this.HasChest == profileNoticeMedal.HasChest && (!this.HasChest || this.Chest.Equals(profileNoticeMedal.Chest)) && this.HasMedalType_ == profileNoticeMedal.HasMedalType_ && (!this.HasMedalType_ || this.MedalType_.Equals(profileNoticeMedal.MedalType_)) && this.HasLeagueId == profileNoticeMedal.HasLeagueId && (!this.HasLeagueId || this.LeagueId.Equals(profileNoticeMedal.LeagueId)) && this.HasWasLimitedByBestEverStarLevel == profileNoticeMedal.HasWasLimitedByBestEverStarLevel && (!this.HasWasLimitedByBestEverStarLevel || this.WasLimitedByBestEverStarLevel.Equals(profileNoticeMedal.WasLimitedByBestEverStarLevel));
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00043DD2 File Offset: 0x00041FD2
		public void Deserialize(Stream stream)
		{
			ProfileNoticeMedal.Deserialize(stream, this);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00043DDC File Offset: 0x00041FDC
		public static ProfileNoticeMedal Deserialize(Stream stream, ProfileNoticeMedal instance)
		{
			return ProfileNoticeMedal.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00043DE8 File Offset: 0x00041FE8
		public static ProfileNoticeMedal DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeMedal profileNoticeMedal = new ProfileNoticeMedal();
			ProfileNoticeMedal.DeserializeLengthDelimited(stream, profileNoticeMedal);
			return profileNoticeMedal;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00043E04 File Offset: 0x00042004
		public static ProfileNoticeMedal DeserializeLengthDelimited(Stream stream, ProfileNoticeMedal instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeMedal.Deserialize(stream, instance, num);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00043E2C File Offset: 0x0004202C
		public static ProfileNoticeMedal Deserialize(Stream stream, ProfileNoticeMedal instance, long limit)
		{
			instance.MedalType_ = ProfileNoticeMedal.MedalType.UNKNOWN_MEDAL;
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
					if (num <= 24)
					{
						if (num == 8)
						{
							instance.StarLevel = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.LegendRank = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 24)
						{
							instance.BestStarLevel = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num <= 40)
					{
						if (num != 34)
						{
							if (num == 40)
							{
								instance.MedalType_ = (ProfileNoticeMedal.MedalType)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (instance.Chest == null)
							{
								instance.Chest = RewardChest.DeserializeLengthDelimited(stream);
								continue;
							}
							RewardChest.DeserializeLengthDelimited(stream, instance.Chest);
							continue;
						}
					}
					else
					{
						if (num == 48)
						{
							instance.LeagueId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 56)
						{
							instance.WasLimitedByBestEverStarLevel = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
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

		// Token: 0x06001384 RID: 4996 RVA: 0x00043F80 File Offset: 0x00042180
		public void Serialize(Stream stream)
		{
			ProfileNoticeMedal.Serialize(stream, this);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00043F8C File Offset: 0x0004218C
		public static void Serialize(Stream stream, ProfileNoticeMedal instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.StarLevel));
			if (instance.HasLegendRank)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.LegendRank));
			}
			if (instance.HasBestStarLevel)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BestStarLevel));
			}
			if (instance.HasChest)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Chest.GetSerializedSize());
				RewardChest.Serialize(stream, instance.Chest);
			}
			if (instance.HasMedalType_)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MedalType_));
			}
			if (instance.HasLeagueId)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.LeagueId));
			}
			if (instance.HasWasLimitedByBestEverStarLevel)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.WasLimitedByBestEverStarLevel);
			}
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0004406C File Offset: 0x0004226C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.StarLevel));
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
			if (this.HasChest)
			{
				num += 1U;
				uint serializedSize = this.Chest.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasMedalType_)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MedalType_));
			}
			if (this.HasLeagueId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.LeagueId));
			}
			if (this.HasWasLimitedByBestEverStarLevel)
			{
				num += 1U;
				num += 1U;
			}
			return num + 1U;
		}

		// Token: 0x04000605 RID: 1541
		public bool HasLegendRank;

		// Token: 0x04000606 RID: 1542
		private int _LegendRank;

		// Token: 0x04000607 RID: 1543
		public bool HasBestStarLevel;

		// Token: 0x04000608 RID: 1544
		private int _BestStarLevel;

		// Token: 0x04000609 RID: 1545
		public bool HasChest;

		// Token: 0x0400060A RID: 1546
		private RewardChest _Chest;

		// Token: 0x0400060B RID: 1547
		public bool HasMedalType_;

		// Token: 0x0400060C RID: 1548
		private ProfileNoticeMedal.MedalType _MedalType_;

		// Token: 0x0400060D RID: 1549
		public bool HasLeagueId;

		// Token: 0x0400060E RID: 1550
		private int _LeagueId;

		// Token: 0x0400060F RID: 1551
		public bool HasWasLimitedByBestEverStarLevel;

		// Token: 0x04000610 RID: 1552
		private bool _WasLimitedByBestEverStarLevel;

		// Token: 0x02000618 RID: 1560
		public enum NoticeID
		{
			// Token: 0x04002080 RID: 8320
			ID = 1
		}

		// Token: 0x02000619 RID: 1561
		public enum MedalType
		{
			// Token: 0x04002082 RID: 8322
			UNKNOWN_MEDAL,
			// Token: 0x04002083 RID: 8323
			STANDARD_MEDAL,
			// Token: 0x04002084 RID: 8324
			WILD_MEDAL,
			// Token: 0x04002085 RID: 8325
			CLASSIC_MEDAL
		}
	}
}
