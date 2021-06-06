using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200003C RID: 60
	public class PlayerAchievementState : IProtoBuf
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000DA31 File Offset: 0x0000BC31
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000DA39 File Offset: 0x0000BC39
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

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000DA49 File Offset: 0x0000BC49
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0000DA51 File Offset: 0x0000BC51
		public int Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
				this.HasStatus = true;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000DA61 File Offset: 0x0000BC61
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000DA69 File Offset: 0x0000BC69
		public int Progress
		{
			get
			{
				return this._Progress;
			}
			set
			{
				this._Progress = value;
				this.HasProgress = true;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000DA79 File Offset: 0x0000BC79
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000DA81 File Offset: 0x0000BC81
		public long CompletedDate
		{
			get
			{
				return this._CompletedDate;
			}
			set
			{
				this._CompletedDate = value;
				this.HasCompletedDate = true;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000DA91 File Offset: 0x0000BC91
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0000DA99 File Offset: 0x0000BC99
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

		// Token: 0x0600035C RID: 860 RVA: 0x0000DAA4 File Offset: 0x0000BCA4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAchievementId)
			{
				num ^= this.AchievementId.GetHashCode();
			}
			if (this.HasStatus)
			{
				num ^= this.Status.GetHashCode();
			}
			if (this.HasProgress)
			{
				num ^= this.Progress.GetHashCode();
			}
			if (this.HasCompletedDate)
			{
				num ^= this.CompletedDate.GetHashCode();
			}
			foreach (RewardItemOutput rewardItemOutput in this.RewardItemOutput)
			{
				num ^= rewardItemOutput.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000DB6C File Offset: 0x0000BD6C
		public override bool Equals(object obj)
		{
			PlayerAchievementState playerAchievementState = obj as PlayerAchievementState;
			if (playerAchievementState == null)
			{
				return false;
			}
			if (this.HasAchievementId != playerAchievementState.HasAchievementId || (this.HasAchievementId && !this.AchievementId.Equals(playerAchievementState.AchievementId)))
			{
				return false;
			}
			if (this.HasStatus != playerAchievementState.HasStatus || (this.HasStatus && !this.Status.Equals(playerAchievementState.Status)))
			{
				return false;
			}
			if (this.HasProgress != playerAchievementState.HasProgress || (this.HasProgress && !this.Progress.Equals(playerAchievementState.Progress)))
			{
				return false;
			}
			if (this.HasCompletedDate != playerAchievementState.HasCompletedDate || (this.HasCompletedDate && !this.CompletedDate.Equals(playerAchievementState.CompletedDate)))
			{
				return false;
			}
			if (this.RewardItemOutput.Count != playerAchievementState.RewardItemOutput.Count)
			{
				return false;
			}
			for (int i = 0; i < this.RewardItemOutput.Count; i++)
			{
				if (!this.RewardItemOutput[i].Equals(playerAchievementState.RewardItemOutput[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000DC8F File Offset: 0x0000BE8F
		public void Deserialize(Stream stream)
		{
			PlayerAchievementState.Deserialize(stream, this);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000DC99 File Offset: 0x0000BE99
		public static PlayerAchievementState Deserialize(Stream stream, PlayerAchievementState instance)
		{
			return PlayerAchievementState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000DCA4 File Offset: 0x0000BEA4
		public static PlayerAchievementState DeserializeLengthDelimited(Stream stream)
		{
			PlayerAchievementState playerAchievementState = new PlayerAchievementState();
			PlayerAchievementState.DeserializeLengthDelimited(stream, playerAchievementState);
			return playerAchievementState;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
		public static PlayerAchievementState DeserializeLengthDelimited(Stream stream, PlayerAchievementState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerAchievementState.Deserialize(stream, instance, num);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
		public static PlayerAchievementState Deserialize(Stream stream, PlayerAchievementState instance, long limit)
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
							instance.AchievementId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Status = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Progress = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.CompletedDate = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 42)
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

		// Token: 0x06000363 RID: 867 RVA: 0x0000DDE9 File Offset: 0x0000BFE9
		public void Serialize(Stream stream)
		{
			PlayerAchievementState.Serialize(stream, this);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		public static void Serialize(Stream stream, PlayerAchievementState instance)
		{
			if (instance.HasAchievementId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AchievementId));
			}
			if (instance.HasStatus)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Status));
			}
			if (instance.HasProgress)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Progress));
			}
			if (instance.HasCompletedDate)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CompletedDate);
			}
			if (instance.RewardItemOutput.Count > 0)
			{
				foreach (RewardItemOutput rewardItemOutput in instance.RewardItemOutput)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, rewardItemOutput.GetSerializedSize());
					PegasusUtil.RewardItemOutput.Serialize(stream, rewardItemOutput);
				}
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000DEDC File Offset: 0x0000C0DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAchievementId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AchievementId));
			}
			if (this.HasStatus)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Status));
			}
			if (this.HasProgress)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Progress));
			}
			if (this.HasCompletedDate)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.CompletedDate);
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

		// Token: 0x04000119 RID: 281
		public bool HasAchievementId;

		// Token: 0x0400011A RID: 282
		private int _AchievementId;

		// Token: 0x0400011B RID: 283
		public bool HasStatus;

		// Token: 0x0400011C RID: 284
		private int _Status;

		// Token: 0x0400011D RID: 285
		public bool HasProgress;

		// Token: 0x0400011E RID: 286
		private int _Progress;

		// Token: 0x0400011F RID: 287
		public bool HasCompletedDate;

		// Token: 0x04000120 RID: 288
		private long _CompletedDate;

		// Token: 0x04000121 RID: 289
		private List<RewardItemOutput> _RewardItemOutput = new List<RewardItemOutput>();
	}
}
