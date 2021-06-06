using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000108 RID: 264
	public class ClaimAchievementReward : IProtoBuf
	{
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0003D173 File Offset: 0x0003B373
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x0003D17B File Offset: 0x0003B37B
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

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0003D18B File Offset: 0x0003B38B
		// (set) Token: 0x06001167 RID: 4455 RVA: 0x0003D193 File Offset: 0x0003B393
		public int ChooseOneRewardItemId
		{
			get
			{
				return this._ChooseOneRewardItemId;
			}
			set
			{
				this._ChooseOneRewardItemId = value;
				this.HasChooseOneRewardItemId = true;
			}
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0003D1A4 File Offset: 0x0003B3A4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAchievementId)
			{
				num ^= this.AchievementId.GetHashCode();
			}
			if (this.HasChooseOneRewardItemId)
			{
				num ^= this.ChooseOneRewardItemId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0003D1F0 File Offset: 0x0003B3F0
		public override bool Equals(object obj)
		{
			ClaimAchievementReward claimAchievementReward = obj as ClaimAchievementReward;
			return claimAchievementReward != null && this.HasAchievementId == claimAchievementReward.HasAchievementId && (!this.HasAchievementId || this.AchievementId.Equals(claimAchievementReward.AchievementId)) && this.HasChooseOneRewardItemId == claimAchievementReward.HasChooseOneRewardItemId && (!this.HasChooseOneRewardItemId || this.ChooseOneRewardItemId.Equals(claimAchievementReward.ChooseOneRewardItemId));
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x0003D266 File Offset: 0x0003B466
		public void Deserialize(Stream stream)
		{
			ClaimAchievementReward.Deserialize(stream, this);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0003D270 File Offset: 0x0003B470
		public static ClaimAchievementReward Deserialize(Stream stream, ClaimAchievementReward instance)
		{
			return ClaimAchievementReward.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0003D27C File Offset: 0x0003B47C
		public static ClaimAchievementReward DeserializeLengthDelimited(Stream stream)
		{
			ClaimAchievementReward claimAchievementReward = new ClaimAchievementReward();
			ClaimAchievementReward.DeserializeLengthDelimited(stream, claimAchievementReward);
			return claimAchievementReward;
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0003D298 File Offset: 0x0003B498
		public static ClaimAchievementReward DeserializeLengthDelimited(Stream stream, ClaimAchievementReward instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClaimAchievementReward.Deserialize(stream, instance, num);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0003D2C0 File Offset: 0x0003B4C0
		public static ClaimAchievementReward Deserialize(Stream stream, ClaimAchievementReward instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
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
						instance.ChooseOneRewardItemId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600116F RID: 4463 RVA: 0x0003D359 File Offset: 0x0003B559
		public void Serialize(Stream stream)
		{
			ClaimAchievementReward.Serialize(stream, this);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0003D362 File Offset: 0x0003B562
		public static void Serialize(Stream stream, ClaimAchievementReward instance)
		{
			if (instance.HasAchievementId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AchievementId));
			}
			if (instance.HasChooseOneRewardItemId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ChooseOneRewardItemId));
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0003D3A0 File Offset: 0x0003B5A0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAchievementId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AchievementId));
			}
			if (this.HasChooseOneRewardItemId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ChooseOneRewardItemId));
			}
			return num;
		}

		// Token: 0x04000550 RID: 1360
		public bool HasAchievementId;

		// Token: 0x04000551 RID: 1361
		private int _AchievementId;

		// Token: 0x04000552 RID: 1362
		public bool HasChooseOneRewardItemId;

		// Token: 0x04000553 RID: 1363
		private int _ChooseOneRewardItemId;

		// Token: 0x0200060A RID: 1546
		public enum PacketID
		{
			// Token: 0x0400205A RID: 8282
			ID = 613,
			// Token: 0x0400205B RID: 8283
			System = 0
		}
	}
}
