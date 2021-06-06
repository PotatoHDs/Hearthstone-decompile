using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000039 RID: 57
	public class RewardItemOutput : IProtoBuf
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000CF9B File Offset: 0x0000B19B
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000CFA3 File Offset: 0x0000B1A3
		public int RewardItemId
		{
			get
			{
				return this._RewardItemId;
			}
			set
			{
				this._RewardItemId = value;
				this.HasRewardItemId = true;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000CFB3 File Offset: 0x0000B1B3
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000CFBB File Offset: 0x0000B1BB
		public int OutputData
		{
			get
			{
				return this._OutputData;
			}
			set
			{
				this._OutputData = value;
				this.HasOutputData = true;
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000CFCC File Offset: 0x0000B1CC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRewardItemId)
			{
				num ^= this.RewardItemId.GetHashCode();
			}
			if (this.HasOutputData)
			{
				num ^= this.OutputData.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000D018 File Offset: 0x0000B218
		public override bool Equals(object obj)
		{
			RewardItemOutput rewardItemOutput = obj as RewardItemOutput;
			return rewardItemOutput != null && this.HasRewardItemId == rewardItemOutput.HasRewardItemId && (!this.HasRewardItemId || this.RewardItemId.Equals(rewardItemOutput.RewardItemId)) && this.HasOutputData == rewardItemOutput.HasOutputData && (!this.HasOutputData || this.OutputData.Equals(rewardItemOutput.OutputData));
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000D08E File Offset: 0x0000B28E
		public void Deserialize(Stream stream)
		{
			RewardItemOutput.Deserialize(stream, this);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000D098 File Offset: 0x0000B298
		public static RewardItemOutput Deserialize(Stream stream, RewardItemOutput instance)
		{
			return RewardItemOutput.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000D0A4 File Offset: 0x0000B2A4
		public static RewardItemOutput DeserializeLengthDelimited(Stream stream)
		{
			RewardItemOutput rewardItemOutput = new RewardItemOutput();
			RewardItemOutput.DeserializeLengthDelimited(stream, rewardItemOutput);
			return rewardItemOutput;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
		public static RewardItemOutput DeserializeLengthDelimited(Stream stream, RewardItemOutput instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RewardItemOutput.Deserialize(stream, instance, num);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
		public static RewardItemOutput Deserialize(Stream stream, RewardItemOutput instance, long limit)
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
						instance.OutputData = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.RewardItemId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000D181 File Offset: 0x0000B381
		public void Serialize(Stream stream)
		{
			RewardItemOutput.Serialize(stream, this);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000D18A File Offset: 0x0000B38A
		public static void Serialize(Stream stream, RewardItemOutput instance)
		{
			if (instance.HasRewardItemId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RewardItemId));
			}
			if (instance.HasOutputData)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.OutputData));
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRewardItemId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RewardItemId));
			}
			if (this.HasOutputData)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.OutputData));
			}
			return num;
		}

		// Token: 0x04000108 RID: 264
		public bool HasRewardItemId;

		// Token: 0x04000109 RID: 265
		private int _RewardItemId;

		// Token: 0x0400010A RID: 266
		public bool HasOutputData;

		// Token: 0x0400010B RID: 267
		private int _OutputData;
	}
}
