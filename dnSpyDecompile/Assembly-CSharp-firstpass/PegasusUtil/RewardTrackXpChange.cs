using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200003F RID: 63
	public class RewardTrackXpChange : IProtoBuf
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000EA4B File Offset: 0x0000CC4B
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000EA53 File Offset: 0x0000CC53
		public int RewardSourceType
		{
			get
			{
				return this._RewardSourceType;
			}
			set
			{
				this._RewardSourceType = value;
				this.HasRewardSourceType = true;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000EA63 File Offset: 0x0000CC63
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000EA6B File Offset: 0x0000CC6B
		public int RewardSourceId
		{
			get
			{
				return this._RewardSourceId;
			}
			set
			{
				this._RewardSourceId = value;
				this.HasRewardSourceId = true;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000EA7B File Offset: 0x0000CC7B
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000EA83 File Offset: 0x0000CC83
		public int PrevXp
		{
			get
			{
				return this._PrevXp;
			}
			set
			{
				this._PrevXp = value;
				this.HasPrevXp = true;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000EA93 File Offset: 0x0000CC93
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000EA9B File Offset: 0x0000CC9B
		public int CurrXp
		{
			get
			{
				return this._CurrXp;
			}
			set
			{
				this._CurrXp = value;
				this.HasCurrXp = true;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000EAAB File Offset: 0x0000CCAB
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000EAB3 File Offset: 0x0000CCB3
		public int PrevLevel
		{
			get
			{
				return this._PrevLevel;
			}
			set
			{
				this._PrevLevel = value;
				this.HasPrevLevel = true;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000EAC3 File Offset: 0x0000CCC3
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0000EACB File Offset: 0x0000CCCB
		public int CurrLevel
		{
			get
			{
				return this._CurrLevel;
			}
			set
			{
				this._CurrLevel = value;
				this.HasCurrLevel = true;
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000EADC File Offset: 0x0000CCDC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRewardSourceType)
			{
				num ^= this.RewardSourceType.GetHashCode();
			}
			if (this.HasRewardSourceId)
			{
				num ^= this.RewardSourceId.GetHashCode();
			}
			if (this.HasPrevXp)
			{
				num ^= this.PrevXp.GetHashCode();
			}
			if (this.HasCurrXp)
			{
				num ^= this.CurrXp.GetHashCode();
			}
			if (this.HasPrevLevel)
			{
				num ^= this.PrevLevel.GetHashCode();
			}
			if (this.HasCurrLevel)
			{
				num ^= this.CurrLevel.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000EB8C File Offset: 0x0000CD8C
		public override bool Equals(object obj)
		{
			RewardTrackXpChange rewardTrackXpChange = obj as RewardTrackXpChange;
			return rewardTrackXpChange != null && this.HasRewardSourceType == rewardTrackXpChange.HasRewardSourceType && (!this.HasRewardSourceType || this.RewardSourceType.Equals(rewardTrackXpChange.RewardSourceType)) && this.HasRewardSourceId == rewardTrackXpChange.HasRewardSourceId && (!this.HasRewardSourceId || this.RewardSourceId.Equals(rewardTrackXpChange.RewardSourceId)) && this.HasPrevXp == rewardTrackXpChange.HasPrevXp && (!this.HasPrevXp || this.PrevXp.Equals(rewardTrackXpChange.PrevXp)) && this.HasCurrXp == rewardTrackXpChange.HasCurrXp && (!this.HasCurrXp || this.CurrXp.Equals(rewardTrackXpChange.CurrXp)) && this.HasPrevLevel == rewardTrackXpChange.HasPrevLevel && (!this.HasPrevLevel || this.PrevLevel.Equals(rewardTrackXpChange.PrevLevel)) && this.HasCurrLevel == rewardTrackXpChange.HasCurrLevel && (!this.HasCurrLevel || this.CurrLevel.Equals(rewardTrackXpChange.CurrLevel));
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000ECBA File Offset: 0x0000CEBA
		public void Deserialize(Stream stream)
		{
			RewardTrackXpChange.Deserialize(stream, this);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
		public static RewardTrackXpChange Deserialize(Stream stream, RewardTrackXpChange instance)
		{
			return RewardTrackXpChange.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		public static RewardTrackXpChange DeserializeLengthDelimited(Stream stream)
		{
			RewardTrackXpChange rewardTrackXpChange = new RewardTrackXpChange();
			RewardTrackXpChange.DeserializeLengthDelimited(stream, rewardTrackXpChange);
			return rewardTrackXpChange;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		public static RewardTrackXpChange DeserializeLengthDelimited(Stream stream, RewardTrackXpChange instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RewardTrackXpChange.Deserialize(stream, instance, num);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000ED14 File Offset: 0x0000CF14
		public static RewardTrackXpChange Deserialize(Stream stream, RewardTrackXpChange instance, long limit)
		{
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
							instance.RewardSourceType = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.RewardSourceId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 24)
						{
							instance.PrevXp = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.CurrXp = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.PrevLevel = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.CurrLevel = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060003A2 RID: 930 RVA: 0x0000EE19 File Offset: 0x0000D019
		public void Serialize(Stream stream)
		{
			RewardTrackXpChange.Serialize(stream, this);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000EE24 File Offset: 0x0000D024
		public static void Serialize(Stream stream, RewardTrackXpChange instance)
		{
			if (instance.HasRewardSourceType)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RewardSourceType));
			}
			if (instance.HasRewardSourceId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RewardSourceId));
			}
			if (instance.HasPrevXp)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PrevXp));
			}
			if (instance.HasCurrXp)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CurrXp));
			}
			if (instance.HasPrevLevel)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PrevLevel));
			}
			if (instance.HasCurrLevel)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CurrLevel));
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRewardSourceType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RewardSourceType));
			}
			if (this.HasRewardSourceId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RewardSourceId));
			}
			if (this.HasPrevXp)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PrevXp));
			}
			if (this.HasCurrXp)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CurrXp));
			}
			if (this.HasPrevLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PrevLevel));
			}
			if (this.HasCurrLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CurrLevel));
			}
			return num;
		}

		// Token: 0x04000132 RID: 306
		public bool HasRewardSourceType;

		// Token: 0x04000133 RID: 307
		private int _RewardSourceType;

		// Token: 0x04000134 RID: 308
		public bool HasRewardSourceId;

		// Token: 0x04000135 RID: 309
		private int _RewardSourceId;

		// Token: 0x04000136 RID: 310
		public bool HasPrevXp;

		// Token: 0x04000137 RID: 311
		private int _PrevXp;

		// Token: 0x04000138 RID: 312
		public bool HasCurrXp;

		// Token: 0x04000139 RID: 313
		private int _CurrXp;

		// Token: 0x0400013A RID: 314
		public bool HasPrevLevel;

		// Token: 0x0400013B RID: 315
		private int _PrevLevel;

		// Token: 0x0400013C RID: 316
		public bool HasCurrLevel;

		// Token: 0x0400013D RID: 317
		private int _CurrLevel;
	}
}
