using System;
using System.IO;
using PegasusShared;

namespace PegasusGame
{
	// Token: 0x020001D7 RID: 471
	public class AchievementProgress : IProtoBuf
	{
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x00069ABB File Offset: 0x00067CBB
		// (set) Token: 0x06001E11 RID: 7697 RVA: 0x00069AC3 File Offset: 0x00067CC3
		public int AchievementId
		{
			get
			{
				return this._AchievementId;
			}
			set
			{
				this._AchievementId = value;
				this.HasAchievementId = true;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x00069AD3 File Offset: 0x00067CD3
		// (set) Token: 0x06001E13 RID: 7699 RVA: 0x00069ADB File Offset: 0x00067CDB
		public ProgOpType OpType
		{
			get
			{
				return this._OpType;
			}
			set
			{
				this._OpType = value;
				this.HasOpType = true;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x00069AEB File Offset: 0x00067CEB
		// (set) Token: 0x06001E15 RID: 7701 RVA: 0x00069AF3 File Offset: 0x00067CF3
		public int Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				this._Amount = value;
				this.HasAmount = true;
			}
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x00069B04 File Offset: 0x00067D04
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAchievementId)
			{
				num ^= this.AchievementId.GetHashCode();
			}
			if (this.HasOpType)
			{
				num ^= this.OpType.GetHashCode();
			}
			if (this.HasAmount)
			{
				num ^= this.Amount.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x00069B70 File Offset: 0x00067D70
		public override bool Equals(object obj)
		{
			AchievementProgress achievementProgress = obj as AchievementProgress;
			return achievementProgress != null && this.HasAchievementId == achievementProgress.HasAchievementId && (!this.HasAchievementId || this.AchievementId.Equals(achievementProgress.AchievementId)) && this.HasOpType == achievementProgress.HasOpType && (!this.HasOpType || this.OpType.Equals(achievementProgress.OpType)) && this.HasAmount == achievementProgress.HasAmount && (!this.HasAmount || this.Amount.Equals(achievementProgress.Amount));
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00069C1F File Offset: 0x00067E1F
		public void Deserialize(Stream stream)
		{
			AchievementProgress.Deserialize(stream, this);
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00069C29 File Offset: 0x00067E29
		public static AchievementProgress Deserialize(Stream stream, AchievementProgress instance)
		{
			return AchievementProgress.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x00069C34 File Offset: 0x00067E34
		public static AchievementProgress DeserializeLengthDelimited(Stream stream)
		{
			AchievementProgress achievementProgress = new AchievementProgress();
			AchievementProgress.DeserializeLengthDelimited(stream, achievementProgress);
			return achievementProgress;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x00069C50 File Offset: 0x00067E50
		public static AchievementProgress DeserializeLengthDelimited(Stream stream, AchievementProgress instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AchievementProgress.Deserialize(stream, instance, num);
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x00069C78 File Offset: 0x00067E78
		public static AchievementProgress Deserialize(Stream stream, AchievementProgress instance, long limit)
		{
			instance.OpType = ProgOpType.PROG_OP_NONE;
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
				else if (num != 8)
				{
					if (num != 16)
					{
						if (num != 24)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.Amount = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.OpType = (ProgOpType)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.AchievementId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00069D2F File Offset: 0x00067F2F
		public void Serialize(Stream stream)
		{
			AchievementProgress.Serialize(stream, this);
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00069D38 File Offset: 0x00067F38
		public static void Serialize(Stream stream, AchievementProgress instance)
		{
			if (instance.HasAchievementId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AchievementId));
			}
			if (instance.HasOpType)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.OpType));
			}
			if (instance.HasAmount)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Amount));
			}
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x00069D9C File Offset: 0x00067F9C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAchievementId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AchievementId));
			}
			if (this.HasOpType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.OpType));
			}
			if (this.HasAmount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Amount));
			}
			return num;
		}

		// Token: 0x04000ADB RID: 2779
		public bool HasAchievementId;

		// Token: 0x04000ADC RID: 2780
		private int _AchievementId;

		// Token: 0x04000ADD RID: 2781
		public bool HasOpType;

		// Token: 0x04000ADE RID: 2782
		private ProgOpType _OpType;

		// Token: 0x04000ADF RID: 2783
		public bool HasAmount;

		// Token: 0x04000AE0 RID: 2784
		private int _Amount;

		// Token: 0x02000662 RID: 1634
		public enum PacketID
		{
			// Token: 0x0400215E RID: 8542
			ID = 52
		}
	}
}
