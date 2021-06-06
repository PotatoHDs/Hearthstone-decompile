using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200010A RID: 266
	public class ClaimRewardTrackReward : IProtoBuf
	{
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x0003DA7A File Offset: 0x0003BC7A
		// (set) Token: 0x06001189 RID: 4489 RVA: 0x0003DA82 File Offset: 0x0003BC82
		public int Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				this._Level = value;
				this.HasLevel = true;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x0003DA92 File Offset: 0x0003BC92
		// (set) Token: 0x0600118B RID: 4491 RVA: 0x0003DA9A File Offset: 0x0003BC9A
		public bool ForPaidTrack
		{
			get
			{
				return this._ForPaidTrack;
			}
			set
			{
				this._ForPaidTrack = value;
				this.HasForPaidTrack = true;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x0003DAAA File Offset: 0x0003BCAA
		// (set) Token: 0x0600118D RID: 4493 RVA: 0x0003DAB2 File Offset: 0x0003BCB2
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

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x0003DAC2 File Offset: 0x0003BCC2
		// (set) Token: 0x0600118F RID: 4495 RVA: 0x0003DACA File Offset: 0x0003BCCA
		public int RewardTrackId
		{
			get
			{
				return this._RewardTrackId;
			}
			set
			{
				this._RewardTrackId = value;
				this.HasRewardTrackId = true;
			}
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0003DADC File Offset: 0x0003BCDC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasLevel)
			{
				num ^= this.Level.GetHashCode();
			}
			if (this.HasForPaidTrack)
			{
				num ^= this.ForPaidTrack.GetHashCode();
			}
			if (this.HasChooseOneRewardItemId)
			{
				num ^= this.ChooseOneRewardItemId.GetHashCode();
			}
			if (this.HasRewardTrackId)
			{
				num ^= this.RewardTrackId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0003DB5C File Offset: 0x0003BD5C
		public override bool Equals(object obj)
		{
			ClaimRewardTrackReward claimRewardTrackReward = obj as ClaimRewardTrackReward;
			return claimRewardTrackReward != null && this.HasLevel == claimRewardTrackReward.HasLevel && (!this.HasLevel || this.Level.Equals(claimRewardTrackReward.Level)) && this.HasForPaidTrack == claimRewardTrackReward.HasForPaidTrack && (!this.HasForPaidTrack || this.ForPaidTrack.Equals(claimRewardTrackReward.ForPaidTrack)) && this.HasChooseOneRewardItemId == claimRewardTrackReward.HasChooseOneRewardItemId && (!this.HasChooseOneRewardItemId || this.ChooseOneRewardItemId.Equals(claimRewardTrackReward.ChooseOneRewardItemId)) && this.HasRewardTrackId == claimRewardTrackReward.HasRewardTrackId && (!this.HasRewardTrackId || this.RewardTrackId.Equals(claimRewardTrackReward.RewardTrackId));
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0003DC2E File Offset: 0x0003BE2E
		public void Deserialize(Stream stream)
		{
			ClaimRewardTrackReward.Deserialize(stream, this);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0003DC38 File Offset: 0x0003BE38
		public static ClaimRewardTrackReward Deserialize(Stream stream, ClaimRewardTrackReward instance)
		{
			return ClaimRewardTrackReward.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0003DC44 File Offset: 0x0003BE44
		public static ClaimRewardTrackReward DeserializeLengthDelimited(Stream stream)
		{
			ClaimRewardTrackReward claimRewardTrackReward = new ClaimRewardTrackReward();
			ClaimRewardTrackReward.DeserializeLengthDelimited(stream, claimRewardTrackReward);
			return claimRewardTrackReward;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0003DC60 File Offset: 0x0003BE60
		public static ClaimRewardTrackReward DeserializeLengthDelimited(Stream stream, ClaimRewardTrackReward instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClaimRewardTrackReward.Deserialize(stream, instance, num);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0003DC88 File Offset: 0x0003BE88
		public static ClaimRewardTrackReward Deserialize(Stream stream, ClaimRewardTrackReward instance, long limit)
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.Level = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.ForPaidTrack = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.ChooseOneRewardItemId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.RewardTrackId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001197 RID: 4503 RVA: 0x0003DD5B File Offset: 0x0003BF5B
		public void Serialize(Stream stream)
		{
			ClaimRewardTrackReward.Serialize(stream, this);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0003DD64 File Offset: 0x0003BF64
		public static void Serialize(Stream stream, ClaimRewardTrackReward instance)
		{
			if (instance.HasLevel)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Level));
			}
			if (instance.HasForPaidTrack)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.ForPaidTrack);
			}
			if (instance.HasChooseOneRewardItemId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ChooseOneRewardItemId));
			}
			if (instance.HasRewardTrackId)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RewardTrackId));
			}
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0003DDE4 File Offset: 0x0003BFE4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Level));
			}
			if (this.HasForPaidTrack)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasChooseOneRewardItemId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ChooseOneRewardItemId));
			}
			if (this.HasRewardTrackId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RewardTrackId));
			}
			return num;
		}

		// Token: 0x0400055C RID: 1372
		public bool HasLevel;

		// Token: 0x0400055D RID: 1373
		private int _Level;

		// Token: 0x0400055E RID: 1374
		public bool HasForPaidTrack;

		// Token: 0x0400055F RID: 1375
		private bool _ForPaidTrack;

		// Token: 0x04000560 RID: 1376
		public bool HasChooseOneRewardItemId;

		// Token: 0x04000561 RID: 1377
		private int _ChooseOneRewardItemId;

		// Token: 0x04000562 RID: 1378
		public bool HasRewardTrackId;

		// Token: 0x04000563 RID: 1379
		private int _RewardTrackId;

		// Token: 0x0200060C RID: 1548
		public enum PacketID
		{
			// Token: 0x04002060 RID: 8288
			ID = 615,
			// Token: 0x04002061 RID: 8289
			System = 0
		}
	}
}
