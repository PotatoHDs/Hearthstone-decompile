using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200003D RID: 61
	public class PlayerRewardTrackLevelState : IProtoBuf
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000DFCB File Offset: 0x0000C1CB
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000DFD3 File Offset: 0x0000C1D3
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

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000DFE3 File Offset: 0x0000C1E3
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000DFEB File Offset: 0x0000C1EB
		public int FreeRewardStatus
		{
			get
			{
				return this._FreeRewardStatus;
			}
			set
			{
				this._FreeRewardStatus = value;
				this.HasFreeRewardStatus = true;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000DFFB File Offset: 0x0000C1FB
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000E003 File Offset: 0x0000C203
		public int PaidRewardStatus
		{
			get
			{
				return this._PaidRewardStatus;
			}
			set
			{
				this._PaidRewardStatus = value;
				this.HasPaidRewardStatus = true;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000E013 File Offset: 0x0000C213
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000E01B File Offset: 0x0000C21B
		public List<RewardItemOutput> RewardItemOutput
		{
			get
			{
				return this._RewardItemOutput;
			}
			set
			{
				this._RewardItemOutput = value;
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000E024 File Offset: 0x0000C224
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasLevel)
			{
				num ^= this.Level.GetHashCode();
			}
			if (this.HasFreeRewardStatus)
			{
				num ^= this.FreeRewardStatus.GetHashCode();
			}
			if (this.HasPaidRewardStatus)
			{
				num ^= this.PaidRewardStatus.GetHashCode();
			}
			foreach (RewardItemOutput rewardItemOutput in this.RewardItemOutput)
			{
				num ^= rewardItemOutput.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
		public override bool Equals(object obj)
		{
			PlayerRewardTrackLevelState playerRewardTrackLevelState = obj as PlayerRewardTrackLevelState;
			if (playerRewardTrackLevelState == null)
			{
				return false;
			}
			if (this.HasLevel != playerRewardTrackLevelState.HasLevel || (this.HasLevel && !this.Level.Equals(playerRewardTrackLevelState.Level)))
			{
				return false;
			}
			if (this.HasFreeRewardStatus != playerRewardTrackLevelState.HasFreeRewardStatus || (this.HasFreeRewardStatus && !this.FreeRewardStatus.Equals(playerRewardTrackLevelState.FreeRewardStatus)))
			{
				return false;
			}
			if (this.HasPaidRewardStatus != playerRewardTrackLevelState.HasPaidRewardStatus || (this.HasPaidRewardStatus && !this.PaidRewardStatus.Equals(playerRewardTrackLevelState.PaidRewardStatus)))
			{
				return false;
			}
			if (this.RewardItemOutput.Count != playerRewardTrackLevelState.RewardItemOutput.Count)
			{
				return false;
			}
			for (int i = 0; i < this.RewardItemOutput.Count; i++)
			{
				if (!this.RewardItemOutput[i].Equals(playerRewardTrackLevelState.RewardItemOutput[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000E1C9 File Offset: 0x0000C3C9
		public void Deserialize(Stream stream)
		{
			PlayerRewardTrackLevelState.Deserialize(stream, this);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000E1D3 File Offset: 0x0000C3D3
		public static PlayerRewardTrackLevelState Deserialize(Stream stream, PlayerRewardTrackLevelState instance)
		{
			return PlayerRewardTrackLevelState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000E1E0 File Offset: 0x0000C3E0
		public static PlayerRewardTrackLevelState DeserializeLengthDelimited(Stream stream)
		{
			PlayerRewardTrackLevelState playerRewardTrackLevelState = new PlayerRewardTrackLevelState();
			PlayerRewardTrackLevelState.DeserializeLengthDelimited(stream, playerRewardTrackLevelState);
			return playerRewardTrackLevelState;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000E1FC File Offset: 0x0000C3FC
		public static PlayerRewardTrackLevelState DeserializeLengthDelimited(Stream stream, PlayerRewardTrackLevelState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerRewardTrackLevelState.Deserialize(stream, instance, num);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000E224 File Offset: 0x0000C424
		public static PlayerRewardTrackLevelState Deserialize(Stream stream, PlayerRewardTrackLevelState instance, long limit)
		{
			if (instance.RewardItemOutput == null)
			{
				instance.RewardItemOutput = new List<RewardItemOutput>();
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.Level = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.FreeRewardStatus = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.PaidRewardStatus = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
						{
							instance.RewardItemOutput.Add(PegasusUtil.RewardItemOutput.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000376 RID: 886 RVA: 0x0000E30F File Offset: 0x0000C50F
		public void Serialize(Stream stream)
		{
			PlayerRewardTrackLevelState.Serialize(stream, this);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000E318 File Offset: 0x0000C518
		public static void Serialize(Stream stream, PlayerRewardTrackLevelState instance)
		{
			if (instance.HasLevel)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Level));
			}
			if (instance.HasFreeRewardStatus)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FreeRewardStatus));
			}
			if (instance.HasPaidRewardStatus)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PaidRewardStatus));
			}
			if (instance.RewardItemOutput.Count > 0)
			{
				foreach (RewardItemOutput rewardItemOutput in instance.RewardItemOutput)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, rewardItemOutput.GetSerializedSize());
					PegasusUtil.RewardItemOutput.Serialize(stream, rewardItemOutput);
				}
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000E3E4 File Offset: 0x0000C5E4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Level));
			}
			if (this.HasFreeRewardStatus)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FreeRewardStatus));
			}
			if (this.HasPaidRewardStatus)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PaidRewardStatus));
			}
			if (this.RewardItemOutput.Count > 0)
			{
				foreach (RewardItemOutput rewardItemOutput in this.RewardItemOutput)
				{
					num += 1U;
					uint serializedSize = rewardItemOutput.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000122 RID: 290
		public bool HasLevel;

		// Token: 0x04000123 RID: 291
		private int _Level;

		// Token: 0x04000124 RID: 292
		public bool HasFreeRewardStatus;

		// Token: 0x04000125 RID: 293
		private int _FreeRewardStatus;

		// Token: 0x04000126 RID: 294
		public bool HasPaidRewardStatus;

		// Token: 0x04000127 RID: 295
		private int _PaidRewardStatus;

		// Token: 0x04000128 RID: 296
		private List<RewardItemOutput> _RewardItemOutput = new List<RewardItemOutput>();
	}
}
