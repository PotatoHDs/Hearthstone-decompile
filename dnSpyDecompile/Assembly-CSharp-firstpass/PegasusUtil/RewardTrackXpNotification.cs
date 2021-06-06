using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200010C RID: 268
	public class RewardTrackXpNotification : IProtoBuf
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x0003E17A File Offset: 0x0003C37A
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x0003E182 File Offset: 0x0003C382
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

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0003E192 File Offset: 0x0003C392
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x0003E19A File Offset: 0x0003C39A
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

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0003E1AA File Offset: 0x0003C3AA
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x0003E1B2 File Offset: 0x0003C3B2
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

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0003E1C2 File Offset: 0x0003C3C2
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x0003E1CA File Offset: 0x0003C3CA
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

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0003E1DA File Offset: 0x0003C3DA
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x0003E1E2 File Offset: 0x0003C3E2
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

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0003E1F2 File Offset: 0x0003C3F2
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x0003E1FA File Offset: 0x0003C3FA
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

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0003E20A File Offset: 0x0003C40A
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x0003E212 File Offset: 0x0003C412
		public List<RewardTrackXpChange> XpChange
		{
			get
			{
				return this._XpChange;
			}
			set
			{
				this._XpChange = value;
			}
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0003E21C File Offset: 0x0003C41C
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
			foreach (RewardTrackXpChange rewardTrackXpChange in this.XpChange)
			{
				num ^= rewardTrackXpChange.GetHashCode();
			}
			return num;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0003E314 File Offset: 0x0003C514
		public override bool Equals(object obj)
		{
			RewardTrackXpNotification rewardTrackXpNotification = obj as RewardTrackXpNotification;
			if (rewardTrackXpNotification == null)
			{
				return false;
			}
			if (this.HasRewardSourceType != rewardTrackXpNotification.HasRewardSourceType || (this.HasRewardSourceType && !this.RewardSourceType.Equals(rewardTrackXpNotification.RewardSourceType)))
			{
				return false;
			}
			if (this.HasRewardSourceId != rewardTrackXpNotification.HasRewardSourceId || (this.HasRewardSourceId && !this.RewardSourceId.Equals(rewardTrackXpNotification.RewardSourceId)))
			{
				return false;
			}
			if (this.HasPrevXp != rewardTrackXpNotification.HasPrevXp || (this.HasPrevXp && !this.PrevXp.Equals(rewardTrackXpNotification.PrevXp)))
			{
				return false;
			}
			if (this.HasCurrXp != rewardTrackXpNotification.HasCurrXp || (this.HasCurrXp && !this.CurrXp.Equals(rewardTrackXpNotification.CurrXp)))
			{
				return false;
			}
			if (this.HasPrevLevel != rewardTrackXpNotification.HasPrevLevel || (this.HasPrevLevel && !this.PrevLevel.Equals(rewardTrackXpNotification.PrevLevel)))
			{
				return false;
			}
			if (this.HasCurrLevel != rewardTrackXpNotification.HasCurrLevel || (this.HasCurrLevel && !this.CurrLevel.Equals(rewardTrackXpNotification.CurrLevel)))
			{
				return false;
			}
			if (this.XpChange.Count != rewardTrackXpNotification.XpChange.Count)
			{
				return false;
			}
			for (int i = 0; i < this.XpChange.Count; i++)
			{
				if (!this.XpChange[i].Equals(rewardTrackXpNotification.XpChange[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0003E493 File Offset: 0x0003C693
		public void Deserialize(Stream stream)
		{
			RewardTrackXpNotification.Deserialize(stream, this);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0003E49D File Offset: 0x0003C69D
		public static RewardTrackXpNotification Deserialize(Stream stream, RewardTrackXpNotification instance)
		{
			return RewardTrackXpNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0003E4A8 File Offset: 0x0003C6A8
		public static RewardTrackXpNotification DeserializeLengthDelimited(Stream stream)
		{
			RewardTrackXpNotification rewardTrackXpNotification = new RewardTrackXpNotification();
			RewardTrackXpNotification.DeserializeLengthDelimited(stream, rewardTrackXpNotification);
			return rewardTrackXpNotification;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0003E4C4 File Offset: 0x0003C6C4
		public static RewardTrackXpNotification DeserializeLengthDelimited(Stream stream, RewardTrackXpNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RewardTrackXpNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0003E4EC File Offset: 0x0003C6EC
		public static RewardTrackXpNotification Deserialize(Stream stream, RewardTrackXpNotification instance, long limit)
		{
			if (instance.XpChange == null)
			{
				instance.XpChange = new List<RewardTrackXpChange>();
			}
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
					else if (num <= 48)
					{
						if (num == 32)
						{
							instance.CurrXp = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.PrevLevel = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.CurrLevel = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 66)
						{
							instance.XpChange.Add(RewardTrackXpChange.DeserializeLengthDelimited(stream));
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

		// Token: 0x060011C1 RID: 4545 RVA: 0x0003E632 File Offset: 0x0003C832
		public void Serialize(Stream stream)
		{
			RewardTrackXpNotification.Serialize(stream, this);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0003E63C File Offset: 0x0003C83C
		public static void Serialize(Stream stream, RewardTrackXpNotification instance)
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
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PrevLevel));
			}
			if (instance.HasCurrLevel)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CurrLevel));
			}
			if (instance.XpChange.Count > 0)
			{
				foreach (RewardTrackXpChange rewardTrackXpChange in instance.XpChange)
				{
					stream.WriteByte(66);
					ProtocolParser.WriteUInt32(stream, rewardTrackXpChange.GetSerializedSize());
					RewardTrackXpChange.Serialize(stream, rewardTrackXpChange);
				}
			}
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0003E760 File Offset: 0x0003C960
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
			if (this.XpChange.Count > 0)
			{
				foreach (RewardTrackXpChange rewardTrackXpChange in this.XpChange)
				{
					num += 1U;
					uint serializedSize = rewardTrackXpChange.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x0400056A RID: 1386
		public bool HasRewardSourceType;

		// Token: 0x0400056B RID: 1387
		private int _RewardSourceType;

		// Token: 0x0400056C RID: 1388
		public bool HasRewardSourceId;

		// Token: 0x0400056D RID: 1389
		private int _RewardSourceId;

		// Token: 0x0400056E RID: 1390
		public bool HasPrevXp;

		// Token: 0x0400056F RID: 1391
		private int _PrevXp;

		// Token: 0x04000570 RID: 1392
		public bool HasCurrXp;

		// Token: 0x04000571 RID: 1393
		private int _CurrXp;

		// Token: 0x04000572 RID: 1394
		public bool HasPrevLevel;

		// Token: 0x04000573 RID: 1395
		private int _PrevLevel;

		// Token: 0x04000574 RID: 1396
		public bool HasCurrLevel;

		// Token: 0x04000575 RID: 1397
		private int _CurrLevel;

		// Token: 0x04000576 RID: 1398
		private List<RewardTrackXpChange> _XpChange = new List<RewardTrackXpChange>();

		// Token: 0x0200060E RID: 1550
		public enum PacketID
		{
			// Token: 0x04002066 RID: 8294
			ID = 617,
			// Token: 0x04002067 RID: 8295
			System = 0
		}
	}
}
