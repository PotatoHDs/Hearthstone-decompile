using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000D0 RID: 208
	public class AchievementNotification : IProtoBuf
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00033D98 File Offset: 0x00031F98
		// (set) Token: 0x06000E2D RID: 3629 RVA: 0x00033DA0 File Offset: 0x00031FA0
		public long PlayerId { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x00033DA9 File Offset: 0x00031FA9
		// (set) Token: 0x06000E2F RID: 3631 RVA: 0x00033DB1 File Offset: 0x00031FB1
		public long AchievementId { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00033DBA File Offset: 0x00031FBA
		// (set) Token: 0x06000E31 RID: 3633 RVA: 0x00033DC2 File Offset: 0x00031FC2
		public int Amount { get; set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00033DCB File Offset: 0x00031FCB
		// (set) Token: 0x06000E33 RID: 3635 RVA: 0x00033DD3 File Offset: 0x00031FD3
		public int Quota { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x00033DDC File Offset: 0x00031FDC
		// (set) Token: 0x06000E35 RID: 3637 RVA: 0x00033DE4 File Offset: 0x00031FE4
		public Date StartDateLocal
		{
			get
			{
				return this._StartDateLocal;
			}
			set
			{
				this._StartDateLocal = value;
				this.HasStartDateLocal = (value != null);
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x00033DF7 File Offset: 0x00031FF7
		// (set) Token: 0x06000E37 RID: 3639 RVA: 0x00033DFF File Offset: 0x00031FFF
		public Date EndDateLocal
		{
			get
			{
				return this._EndDateLocal;
			}
			set
			{
				this._EndDateLocal = value;
				this.HasEndDateLocal = (value != null);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x00033E12 File Offset: 0x00032012
		// (set) Token: 0x06000E39 RID: 3641 RVA: 0x00033E1A File Offset: 0x0003201A
		public bool Complete { get; set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x00033E23 File Offset: 0x00032023
		// (set) Token: 0x06000E3B RID: 3643 RVA: 0x00033E2B File Offset: 0x0003202B
		public bool NewAchievement { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x00033E34 File Offset: 0x00032034
		// (set) Token: 0x06000E3D RID: 3645 RVA: 0x00033E3C File Offset: 0x0003203C
		public bool RemoveAchievement { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x00033E45 File Offset: 0x00032045
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x00033E4D File Offset: 0x0003204D
		public int IntervalRewardCount
		{
			get
			{
				return this._IntervalRewardCount;
			}
			set
			{
				this._IntervalRewardCount = value;
				this.HasIntervalRewardCount = true;
			}
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00033E60 File Offset: 0x00032060
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.PlayerId.GetHashCode();
			num ^= this.AchievementId.GetHashCode();
			num ^= this.Amount.GetHashCode();
			num ^= this.Quota.GetHashCode();
			if (this.HasStartDateLocal)
			{
				num ^= this.StartDateLocal.GetHashCode();
			}
			if (this.HasEndDateLocal)
			{
				num ^= this.EndDateLocal.GetHashCode();
			}
			num ^= this.Complete.GetHashCode();
			num ^= this.NewAchievement.GetHashCode();
			num ^= this.RemoveAchievement.GetHashCode();
			if (this.HasIntervalRewardCount)
			{
				num ^= this.IntervalRewardCount.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00033F38 File Offset: 0x00032138
		public override bool Equals(object obj)
		{
			AchievementNotification achievementNotification = obj as AchievementNotification;
			return achievementNotification != null && this.PlayerId.Equals(achievementNotification.PlayerId) && this.AchievementId.Equals(achievementNotification.AchievementId) && this.Amount.Equals(achievementNotification.Amount) && this.Quota.Equals(achievementNotification.Quota) && this.HasStartDateLocal == achievementNotification.HasStartDateLocal && (!this.HasStartDateLocal || this.StartDateLocal.Equals(achievementNotification.StartDateLocal)) && this.HasEndDateLocal == achievementNotification.HasEndDateLocal && (!this.HasEndDateLocal || this.EndDateLocal.Equals(achievementNotification.EndDateLocal)) && this.Complete.Equals(achievementNotification.Complete) && this.NewAchievement.Equals(achievementNotification.NewAchievement) && this.RemoveAchievement.Equals(achievementNotification.RemoveAchievement) && this.HasIntervalRewardCount == achievementNotification.HasIntervalRewardCount && (!this.HasIntervalRewardCount || this.IntervalRewardCount.Equals(achievementNotification.IntervalRewardCount));
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0003407E File Offset: 0x0003227E
		public void Deserialize(Stream stream)
		{
			AchievementNotification.Deserialize(stream, this);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00034088 File Offset: 0x00032288
		public static AchievementNotification Deserialize(Stream stream, AchievementNotification instance)
		{
			return AchievementNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00034094 File Offset: 0x00032294
		public static AchievementNotification DeserializeLengthDelimited(Stream stream)
		{
			AchievementNotification achievementNotification = new AchievementNotification();
			AchievementNotification.DeserializeLengthDelimited(stream, achievementNotification);
			return achievementNotification;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x000340B0 File Offset: 0x000322B0
		public static AchievementNotification DeserializeLengthDelimited(Stream stream, AchievementNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AchievementNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x000340D8 File Offset: 0x000322D8
		public static AchievementNotification Deserialize(Stream stream, AchievementNotification instance, long limit)
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
					if (num <= 42)
					{
						if (num <= 16)
						{
							if (num == 8)
							{
								instance.PlayerId = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 16)
							{
								instance.AchievementId = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 24)
							{
								instance.Amount = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 32)
							{
								instance.Quota = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 42)
							{
								if (instance.StartDateLocal == null)
								{
									instance.StartDateLocal = Date.DeserializeLengthDelimited(stream);
									continue;
								}
								Date.DeserializeLengthDelimited(stream, instance.StartDateLocal);
								continue;
							}
						}
					}
					else if (num <= 56)
					{
						if (num != 50)
						{
							if (num == 56)
							{
								instance.Complete = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (instance.EndDateLocal == null)
							{
								instance.EndDateLocal = Date.DeserializeLengthDelimited(stream);
								continue;
							}
							Date.DeserializeLengthDelimited(stream, instance.EndDateLocal);
							continue;
						}
					}
					else
					{
						if (num == 64)
						{
							instance.NewAchievement = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 72)
						{
							instance.RemoveAchievement = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 80)
						{
							instance.IntervalRewardCount = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000E47 RID: 3655 RVA: 0x00034295 File Offset: 0x00032495
		public void Serialize(Stream stream)
		{
			AchievementNotification.Serialize(stream, this);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x000342A0 File Offset: 0x000324A0
		public static void Serialize(Stream stream, AchievementNotification instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.AchievementId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Amount));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Quota));
			if (instance.HasStartDateLocal)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.StartDateLocal.GetSerializedSize());
				Date.Serialize(stream, instance.StartDateLocal);
			}
			if (instance.HasEndDateLocal)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.EndDateLocal.GetSerializedSize());
				Date.Serialize(stream, instance.EndDateLocal);
			}
			stream.WriteByte(56);
			ProtocolParser.WriteBool(stream, instance.Complete);
			stream.WriteByte(64);
			ProtocolParser.WriteBool(stream, instance.NewAchievement);
			stream.WriteByte(72);
			ProtocolParser.WriteBool(stream, instance.RemoveAchievement);
			if (instance.HasIntervalRewardCount)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.IntervalRewardCount));
			}
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x000343B4 File Offset: 0x000325B4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.PlayerId);
			num += ProtocolParser.SizeOfUInt64((ulong)this.AchievementId);
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Amount));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Quota));
			if (this.HasStartDateLocal)
			{
				num += 1U;
				uint serializedSize = this.StartDateLocal.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasEndDateLocal)
			{
				num += 1U;
				uint serializedSize2 = this.EndDateLocal.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			num += 1U;
			num += 1U;
			num += 1U;
			if (this.HasIntervalRewardCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.IntervalRewardCount));
			}
			return num + 7U;
		}

		// Token: 0x040004BE RID: 1214
		public bool HasStartDateLocal;

		// Token: 0x040004BF RID: 1215
		private Date _StartDateLocal;

		// Token: 0x040004C0 RID: 1216
		public bool HasEndDateLocal;

		// Token: 0x040004C1 RID: 1217
		private Date _EndDateLocal;

		// Token: 0x040004C5 RID: 1221
		public bool HasIntervalRewardCount;

		// Token: 0x040004C6 RID: 1222
		private int _IntervalRewardCount;
	}
}
