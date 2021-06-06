using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000040 RID: 64
	public class RewardTrackUnclaimedRewards : IProtoBuf
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000EF92 File Offset: 0x0000D192
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x0000EF9A File Offset: 0x0000D19A
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

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000EFAA File Offset: 0x0000D1AA
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000EFB2 File Offset: 0x0000D1B2
		public List<PlayerRewardTrackLevelState> UnclaimedLevel
		{
			get
			{
				return this._UnclaimedLevel;
			}
			set
			{
				this._UnclaimedLevel = value;
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000EFBC File Offset: 0x0000D1BC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRewardTrackId)
			{
				num ^= this.RewardTrackId.GetHashCode();
			}
			foreach (PlayerRewardTrackLevelState playerRewardTrackLevelState in this.UnclaimedLevel)
			{
				num ^= playerRewardTrackLevelState.GetHashCode();
			}
			return num;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000F038 File Offset: 0x0000D238
		public override bool Equals(object obj)
		{
			RewardTrackUnclaimedRewards rewardTrackUnclaimedRewards = obj as RewardTrackUnclaimedRewards;
			if (rewardTrackUnclaimedRewards == null)
			{
				return false;
			}
			if (this.HasRewardTrackId != rewardTrackUnclaimedRewards.HasRewardTrackId || (this.HasRewardTrackId && !this.RewardTrackId.Equals(rewardTrackUnclaimedRewards.RewardTrackId)))
			{
				return false;
			}
			if (this.UnclaimedLevel.Count != rewardTrackUnclaimedRewards.UnclaimedLevel.Count)
			{
				return false;
			}
			for (int i = 0; i < this.UnclaimedLevel.Count; i++)
			{
				if (!this.UnclaimedLevel[i].Equals(rewardTrackUnclaimedRewards.UnclaimedLevel[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000F0D1 File Offset: 0x0000D2D1
		public void Deserialize(Stream stream)
		{
			RewardTrackUnclaimedRewards.Deserialize(stream, this);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000F0DB File Offset: 0x0000D2DB
		public static RewardTrackUnclaimedRewards Deserialize(Stream stream, RewardTrackUnclaimedRewards instance)
		{
			return RewardTrackUnclaimedRewards.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000F0E8 File Offset: 0x0000D2E8
		public static RewardTrackUnclaimedRewards DeserializeLengthDelimited(Stream stream)
		{
			RewardTrackUnclaimedRewards rewardTrackUnclaimedRewards = new RewardTrackUnclaimedRewards();
			RewardTrackUnclaimedRewards.DeserializeLengthDelimited(stream, rewardTrackUnclaimedRewards);
			return rewardTrackUnclaimedRewards;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000F104 File Offset: 0x0000D304
		public static RewardTrackUnclaimedRewards DeserializeLengthDelimited(Stream stream, RewardTrackUnclaimedRewards instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RewardTrackUnclaimedRewards.Deserialize(stream, instance, num);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000F12C File Offset: 0x0000D32C
		public static RewardTrackUnclaimedRewards Deserialize(Stream stream, RewardTrackUnclaimedRewards instance, long limit)
		{
			if (instance.UnclaimedLevel == null)
			{
				instance.UnclaimedLevel = new List<PlayerRewardTrackLevelState>();
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
				else if (num != 8)
				{
					if (num != 18)
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
						instance.UnclaimedLevel.Add(PlayerRewardTrackLevelState.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.RewardTrackId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000F1DC File Offset: 0x0000D3DC
		public void Serialize(Stream stream)
		{
			RewardTrackUnclaimedRewards.Serialize(stream, this);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000F1E8 File Offset: 0x0000D3E8
		public static void Serialize(Stream stream, RewardTrackUnclaimedRewards instance)
		{
			if (instance.HasRewardTrackId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RewardTrackId));
			}
			if (instance.UnclaimedLevel.Count > 0)
			{
				foreach (PlayerRewardTrackLevelState playerRewardTrackLevelState in instance.UnclaimedLevel)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, playerRewardTrackLevelState.GetSerializedSize());
					PlayerRewardTrackLevelState.Serialize(stream, playerRewardTrackLevelState);
				}
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000F27C File Offset: 0x0000D47C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRewardTrackId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RewardTrackId));
			}
			if (this.UnclaimedLevel.Count > 0)
			{
				foreach (PlayerRewardTrackLevelState playerRewardTrackLevelState in this.UnclaimedLevel)
				{
					num += 1U;
					uint serializedSize = playerRewardTrackLevelState.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x0400013E RID: 318
		public bool HasRewardTrackId;

		// Token: 0x0400013F RID: 319
		private int _RewardTrackId;

		// Token: 0x04000140 RID: 320
		private List<PlayerRewardTrackLevelState> _UnclaimedLevel = new List<PlayerRewardTrackLevelState>();
	}
}
