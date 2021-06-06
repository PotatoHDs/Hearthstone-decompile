using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000146 RID: 326
	public class RewardBag : IProtoBuf
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x00049774 File Offset: 0x00047974
		// (set) Token: 0x06001583 RID: 5507 RVA: 0x0004977C File Offset: 0x0004797C
		public ProfileNoticeRewardBooster RewardBooster
		{
			get
			{
				return this._RewardBooster;
			}
			set
			{
				this._RewardBooster = value;
				this.HasRewardBooster = (value != null);
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x0004978F File Offset: 0x0004798F
		// (set) Token: 0x06001585 RID: 5509 RVA: 0x00049797 File Offset: 0x00047997
		public ProfileNoticeRewardCard RewardCard
		{
			get
			{
				return this._RewardCard;
			}
			set
			{
				this._RewardCard = value;
				this.HasRewardCard = (value != null);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x000497AA File Offset: 0x000479AA
		// (set) Token: 0x06001587 RID: 5511 RVA: 0x000497B2 File Offset: 0x000479B2
		public ProfileNoticeRewardDust RewardDust
		{
			get
			{
				return this._RewardDust;
			}
			set
			{
				this._RewardDust = value;
				this.HasRewardDust = (value != null);
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x000497C5 File Offset: 0x000479C5
		// (set) Token: 0x06001589 RID: 5513 RVA: 0x000497CD File Offset: 0x000479CD
		public ProfileNoticeRewardCurrency RewardGold
		{
			get
			{
				return this._RewardGold;
			}
			set
			{
				this._RewardGold = value;
				this.HasRewardGold = (value != null);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x000497E0 File Offset: 0x000479E0
		// (set) Token: 0x0600158B RID: 5515 RVA: 0x000497E8 File Offset: 0x000479E8
		public ProfileNoticeCardBack RewardCardBack
		{
			get
			{
				return this._RewardCardBack;
			}
			set
			{
				this._RewardCardBack = value;
				this.HasRewardCardBack = (value != null);
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x000497FB File Offset: 0x000479FB
		// (set) Token: 0x0600158D RID: 5517 RVA: 0x00049803 File Offset: 0x00047A03
		public ProfileNoticeRewardForge RewardArenaTicket
		{
			get
			{
				return this._RewardArenaTicket;
			}
			set
			{
				this._RewardArenaTicket = value;
				this.HasRewardArenaTicket = (value != null);
			}
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00049818 File Offset: 0x00047A18
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRewardBooster)
			{
				num ^= this.RewardBooster.GetHashCode();
			}
			if (this.HasRewardCard)
			{
				num ^= this.RewardCard.GetHashCode();
			}
			if (this.HasRewardDust)
			{
				num ^= this.RewardDust.GetHashCode();
			}
			if (this.HasRewardGold)
			{
				num ^= this.RewardGold.GetHashCode();
			}
			if (this.HasRewardCardBack)
			{
				num ^= this.RewardCardBack.GetHashCode();
			}
			if (this.HasRewardArenaTicket)
			{
				num ^= this.RewardArenaTicket.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x000498B8 File Offset: 0x00047AB8
		public override bool Equals(object obj)
		{
			RewardBag rewardBag = obj as RewardBag;
			return rewardBag != null && this.HasRewardBooster == rewardBag.HasRewardBooster && (!this.HasRewardBooster || this.RewardBooster.Equals(rewardBag.RewardBooster)) && this.HasRewardCard == rewardBag.HasRewardCard && (!this.HasRewardCard || this.RewardCard.Equals(rewardBag.RewardCard)) && this.HasRewardDust == rewardBag.HasRewardDust && (!this.HasRewardDust || this.RewardDust.Equals(rewardBag.RewardDust)) && this.HasRewardGold == rewardBag.HasRewardGold && (!this.HasRewardGold || this.RewardGold.Equals(rewardBag.RewardGold)) && this.HasRewardCardBack == rewardBag.HasRewardCardBack && (!this.HasRewardCardBack || this.RewardCardBack.Equals(rewardBag.RewardCardBack)) && this.HasRewardArenaTicket == rewardBag.HasRewardArenaTicket && (!this.HasRewardArenaTicket || this.RewardArenaTicket.Equals(rewardBag.RewardArenaTicket));
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x000499D4 File Offset: 0x00047BD4
		public void Deserialize(Stream stream)
		{
			RewardBag.Deserialize(stream, this);
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x000499DE File Offset: 0x00047BDE
		public static RewardBag Deserialize(Stream stream, RewardBag instance)
		{
			return RewardBag.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x000499EC File Offset: 0x00047BEC
		public static RewardBag DeserializeLengthDelimited(Stream stream)
		{
			RewardBag rewardBag = new RewardBag();
			RewardBag.DeserializeLengthDelimited(stream, rewardBag);
			return rewardBag;
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00049A08 File Offset: 0x00047C08
		public static RewardBag DeserializeLengthDelimited(Stream stream, RewardBag instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RewardBag.Deserialize(stream, instance, num);
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00049A30 File Offset: 0x00047C30
		public static RewardBag Deserialize(Stream stream, RewardBag instance, long limit)
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
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num != 18)
							{
								if (num == 26)
								{
									if (instance.RewardDust == null)
									{
										instance.RewardDust = ProfileNoticeRewardDust.DeserializeLengthDelimited(stream);
										continue;
									}
									ProfileNoticeRewardDust.DeserializeLengthDelimited(stream, instance.RewardDust);
									continue;
								}
							}
							else
							{
								if (instance.RewardCard == null)
								{
									instance.RewardCard = ProfileNoticeRewardCard.DeserializeLengthDelimited(stream);
									continue;
								}
								ProfileNoticeRewardCard.DeserializeLengthDelimited(stream, instance.RewardCard);
								continue;
							}
						}
						else
						{
							if (instance.RewardBooster == null)
							{
								instance.RewardBooster = ProfileNoticeRewardBooster.DeserializeLengthDelimited(stream);
								continue;
							}
							ProfileNoticeRewardBooster.DeserializeLengthDelimited(stream, instance.RewardBooster);
							continue;
						}
					}
					else if (num != 34)
					{
						if (num != 42)
						{
							if (num == 50)
							{
								if (instance.RewardArenaTicket == null)
								{
									instance.RewardArenaTicket = ProfileNoticeRewardForge.DeserializeLengthDelimited(stream);
									continue;
								}
								ProfileNoticeRewardForge.DeserializeLengthDelimited(stream, instance.RewardArenaTicket);
								continue;
							}
						}
						else
						{
							if (instance.RewardCardBack == null)
							{
								instance.RewardCardBack = ProfileNoticeCardBack.DeserializeLengthDelimited(stream);
								continue;
							}
							ProfileNoticeCardBack.DeserializeLengthDelimited(stream, instance.RewardCardBack);
							continue;
						}
					}
					else
					{
						if (instance.RewardGold == null)
						{
							instance.RewardGold = ProfileNoticeRewardCurrency.DeserializeLengthDelimited(stream);
							continue;
						}
						ProfileNoticeRewardCurrency.DeserializeLengthDelimited(stream, instance.RewardGold);
						continue;
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

		// Token: 0x06001595 RID: 5525 RVA: 0x00049BDE File Offset: 0x00047DDE
		public void Serialize(Stream stream)
		{
			RewardBag.Serialize(stream, this);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00049BE8 File Offset: 0x00047DE8
		public static void Serialize(Stream stream, RewardBag instance)
		{
			if (instance.HasRewardBooster)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RewardBooster.GetSerializedSize());
				ProfileNoticeRewardBooster.Serialize(stream, instance.RewardBooster);
			}
			if (instance.HasRewardCard)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.RewardCard.GetSerializedSize());
				ProfileNoticeRewardCard.Serialize(stream, instance.RewardCard);
			}
			if (instance.HasRewardDust)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.RewardDust.GetSerializedSize());
				ProfileNoticeRewardDust.Serialize(stream, instance.RewardDust);
			}
			if (instance.HasRewardGold)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.RewardGold.GetSerializedSize());
				ProfileNoticeRewardCurrency.Serialize(stream, instance.RewardGold);
			}
			if (instance.HasRewardCardBack)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.RewardCardBack.GetSerializedSize());
				ProfileNoticeCardBack.Serialize(stream, instance.RewardCardBack);
			}
			if (instance.HasRewardArenaTicket)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.RewardArenaTicket.GetSerializedSize());
				ProfileNoticeRewardForge.Serialize(stream, instance.RewardArenaTicket);
			}
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00049D04 File Offset: 0x00047F04
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRewardBooster)
			{
				num += 1U;
				uint serializedSize = this.RewardBooster.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasRewardCard)
			{
				num += 1U;
				uint serializedSize2 = this.RewardCard.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasRewardDust)
			{
				num += 1U;
				uint serializedSize3 = this.RewardDust.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasRewardGold)
			{
				num += 1U;
				uint serializedSize4 = this.RewardGold.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasRewardCardBack)
			{
				num += 1U;
				uint serializedSize5 = this.RewardCardBack.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (this.HasRewardArenaTicket)
			{
				num += 1U;
				uint serializedSize6 = this.RewardArenaTicket.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			return num;
		}

		// Token: 0x04000694 RID: 1684
		public bool HasRewardBooster;

		// Token: 0x04000695 RID: 1685
		private ProfileNoticeRewardBooster _RewardBooster;

		// Token: 0x04000696 RID: 1686
		public bool HasRewardCard;

		// Token: 0x04000697 RID: 1687
		private ProfileNoticeRewardCard _RewardCard;

		// Token: 0x04000698 RID: 1688
		public bool HasRewardDust;

		// Token: 0x04000699 RID: 1689
		private ProfileNoticeRewardDust _RewardDust;

		// Token: 0x0400069A RID: 1690
		public bool HasRewardGold;

		// Token: 0x0400069B RID: 1691
		private ProfileNoticeRewardCurrency _RewardGold;

		// Token: 0x0400069C RID: 1692
		public bool HasRewardCardBack;

		// Token: 0x0400069D RID: 1693
		private ProfileNoticeCardBack _RewardCardBack;

		// Token: 0x0400069E RID: 1694
		public bool HasRewardArenaTicket;

		// Token: 0x0400069F RID: 1695
		private ProfileNoticeRewardForge _RewardArenaTicket;
	}
}
