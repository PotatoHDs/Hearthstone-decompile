using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200003A RID: 58
	public class PlayerQuestState : IProtoBuf
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000D20E File Offset: 0x0000B40E
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000D216 File Offset: 0x0000B416
		public int QuestId
		{
			get
			{
				return this._QuestId;
			}
			set
			{
				this._QuestId = value;
				this.HasQuestId = true;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000D226 File Offset: 0x0000B426
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000D22E File Offset: 0x0000B42E
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

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000D23E File Offset: 0x0000B43E
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000D246 File Offset: 0x0000B446
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000D256 File Offset: 0x0000B456
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000D25E File Offset: 0x0000B45E
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

		// Token: 0x06000336 RID: 822 RVA: 0x0000D268 File Offset: 0x0000B468
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasQuestId)
			{
				num ^= this.QuestId.GetHashCode();
			}
			if (this.HasStatus)
			{
				num ^= this.Status.GetHashCode();
			}
			if (this.HasProgress)
			{
				num ^= this.Progress.GetHashCode();
			}
			foreach (RewardItemOutput rewardItemOutput in this.RewardItemOutput)
			{
				num ^= rewardItemOutput.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000D318 File Offset: 0x0000B518
		public override bool Equals(object obj)
		{
			PlayerQuestState playerQuestState = obj as PlayerQuestState;
			if (playerQuestState == null)
			{
				return false;
			}
			if (this.HasQuestId != playerQuestState.HasQuestId || (this.HasQuestId && !this.QuestId.Equals(playerQuestState.QuestId)))
			{
				return false;
			}
			if (this.HasStatus != playerQuestState.HasStatus || (this.HasStatus && !this.Status.Equals(playerQuestState.Status)))
			{
				return false;
			}
			if (this.HasProgress != playerQuestState.HasProgress || (this.HasProgress && !this.Progress.Equals(playerQuestState.Progress)))
			{
				return false;
			}
			if (this.RewardItemOutput.Count != playerQuestState.RewardItemOutput.Count)
			{
				return false;
			}
			for (int i = 0; i < this.RewardItemOutput.Count; i++)
			{
				if (!this.RewardItemOutput[i].Equals(playerQuestState.RewardItemOutput[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000D40D File Offset: 0x0000B60D
		public void Deserialize(Stream stream)
		{
			PlayerQuestState.Deserialize(stream, this);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000D417 File Offset: 0x0000B617
		public static PlayerQuestState Deserialize(Stream stream, PlayerQuestState instance)
		{
			return PlayerQuestState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000D424 File Offset: 0x0000B624
		public static PlayerQuestState DeserializeLengthDelimited(Stream stream)
		{
			PlayerQuestState playerQuestState = new PlayerQuestState();
			PlayerQuestState.DeserializeLengthDelimited(stream, playerQuestState);
			return playerQuestState;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000D440 File Offset: 0x0000B640
		public static PlayerQuestState DeserializeLengthDelimited(Stream stream, PlayerQuestState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerQuestState.Deserialize(stream, instance, num);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000D468 File Offset: 0x0000B668
		public static PlayerQuestState Deserialize(Stream stream, PlayerQuestState instance, long limit)
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
							instance.QuestId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600033D RID: 829 RVA: 0x0000D553 File Offset: 0x0000B753
		public void Serialize(Stream stream)
		{
			PlayerQuestState.Serialize(stream, this);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000D55C File Offset: 0x0000B75C
		public static void Serialize(Stream stream, PlayerQuestState instance)
		{
			if (instance.HasQuestId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.QuestId));
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

		// Token: 0x0600033F RID: 831 RVA: 0x0000D628 File Offset: 0x0000B828
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasQuestId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.QuestId));
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

		// Token: 0x0400010C RID: 268
		public bool HasQuestId;

		// Token: 0x0400010D RID: 269
		private int _QuestId;

		// Token: 0x0400010E RID: 270
		public bool HasStatus;

		// Token: 0x0400010F RID: 271
		private int _Status;

		// Token: 0x04000110 RID: 272
		public bool HasProgress;

		// Token: 0x04000111 RID: 273
		private int _Progress;

		// Token: 0x04000112 RID: 274
		private List<RewardItemOutput> _RewardItemOutput = new List<RewardItemOutput>();
	}
}
