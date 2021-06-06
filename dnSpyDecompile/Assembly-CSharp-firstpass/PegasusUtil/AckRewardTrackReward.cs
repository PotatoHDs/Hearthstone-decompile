using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200010B RID: 267
	public class AckRewardTrackReward : IProtoBuf
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x0003DE55 File Offset: 0x0003C055
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x0003DE5D File Offset: 0x0003C05D
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

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0003DE6D File Offset: 0x0003C06D
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x0003DE75 File Offset: 0x0003C075
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

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x0003DE85 File Offset: 0x0003C085
		// (set) Token: 0x060011A0 RID: 4512 RVA: 0x0003DE8D File Offset: 0x0003C08D
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

		// Token: 0x060011A1 RID: 4513 RVA: 0x0003DEA0 File Offset: 0x0003C0A0
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
			if (this.HasRewardTrackId)
			{
				num ^= this.RewardTrackId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0003DF08 File Offset: 0x0003C108
		public override bool Equals(object obj)
		{
			AckRewardTrackReward ackRewardTrackReward = obj as AckRewardTrackReward;
			return ackRewardTrackReward != null && this.HasLevel == ackRewardTrackReward.HasLevel && (!this.HasLevel || this.Level.Equals(ackRewardTrackReward.Level)) && this.HasForPaidTrack == ackRewardTrackReward.HasForPaidTrack && (!this.HasForPaidTrack || this.ForPaidTrack.Equals(ackRewardTrackReward.ForPaidTrack)) && this.HasRewardTrackId == ackRewardTrackReward.HasRewardTrackId && (!this.HasRewardTrackId || this.RewardTrackId.Equals(ackRewardTrackReward.RewardTrackId));
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0003DFAC File Offset: 0x0003C1AC
		public void Deserialize(Stream stream)
		{
			AckRewardTrackReward.Deserialize(stream, this);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0003DFB6 File Offset: 0x0003C1B6
		public static AckRewardTrackReward Deserialize(Stream stream, AckRewardTrackReward instance)
		{
			return AckRewardTrackReward.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0003DFC4 File Offset: 0x0003C1C4
		public static AckRewardTrackReward DeserializeLengthDelimited(Stream stream)
		{
			AckRewardTrackReward ackRewardTrackReward = new AckRewardTrackReward();
			AckRewardTrackReward.DeserializeLengthDelimited(stream, ackRewardTrackReward);
			return ackRewardTrackReward;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0003DFE0 File Offset: 0x0003C1E0
		public static AckRewardTrackReward DeserializeLengthDelimited(Stream stream, AckRewardTrackReward instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AckRewardTrackReward.Deserialize(stream, instance, num);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0003E008 File Offset: 0x0003C208
		public static AckRewardTrackReward Deserialize(Stream stream, AckRewardTrackReward instance, long limit)
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
							instance.RewardTrackId = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.ForPaidTrack = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.Level = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0003E0B7 File Offset: 0x0003C2B7
		public void Serialize(Stream stream)
		{
			AckRewardTrackReward.Serialize(stream, this);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0003E0C0 File Offset: 0x0003C2C0
		public static void Serialize(Stream stream, AckRewardTrackReward instance)
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
			if (instance.HasRewardTrackId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RewardTrackId));
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0003E124 File Offset: 0x0003C324
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
			if (this.HasRewardTrackId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RewardTrackId));
			}
			return num;
		}

		// Token: 0x04000564 RID: 1380
		public bool HasLevel;

		// Token: 0x04000565 RID: 1381
		private int _Level;

		// Token: 0x04000566 RID: 1382
		public bool HasForPaidTrack;

		// Token: 0x04000567 RID: 1383
		private bool _ForPaidTrack;

		// Token: 0x04000568 RID: 1384
		public bool HasRewardTrackId;

		// Token: 0x04000569 RID: 1385
		private int _RewardTrackId;

		// Token: 0x0200060D RID: 1549
		public enum PacketID
		{
			// Token: 0x04002063 RID: 8291
			ID = 616,
			// Token: 0x04002064 RID: 8292
			System = 0
		}
	}
}
