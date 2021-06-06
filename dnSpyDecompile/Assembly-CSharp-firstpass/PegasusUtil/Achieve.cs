using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000038 RID: 56
	public class Achieve : IProtoBuf
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000C6EF File Offset: 0x0000A8EF
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000C6F7 File Offset: 0x0000A8F7
		public int Id { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000C700 File Offset: 0x0000A900
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000C708 File Offset: 0x0000A908
		public int Progress { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000C711 File Offset: 0x0000A911
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000C719 File Offset: 0x0000A919
		public int AckProgress { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000C722 File Offset: 0x0000A922
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0000C72A File Offset: 0x0000A92A
		public int CompletionCount
		{
			get
			{
				return this._CompletionCount;
			}
			set
			{
				this._CompletionCount = value;
				this.HasCompletionCount = true;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000C73A File Offset: 0x0000A93A
		// (set) Token: 0x06000307 RID: 775 RVA: 0x0000C742 File Offset: 0x0000A942
		public bool Active
		{
			get
			{
				return this._Active;
			}
			set
			{
				this._Active = value;
				this.HasActive = true;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000C752 File Offset: 0x0000A952
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0000C75A File Offset: 0x0000A95A
		public int StartedCount
		{
			get
			{
				return this._StartedCount;
			}
			set
			{
				this._StartedCount = value;
				this.HasStartedCount = true;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000C76A File Offset: 0x0000A96A
		// (set) Token: 0x0600030B RID: 779 RVA: 0x0000C772 File Offset: 0x0000A972
		public Date DateGiven
		{
			get
			{
				return this._DateGiven;
			}
			set
			{
				this._DateGiven = value;
				this.HasDateGiven = (value != null);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000C785 File Offset: 0x0000A985
		// (set) Token: 0x0600030D RID: 781 RVA: 0x0000C78D File Offset: 0x0000A98D
		public Date DateCompleted
		{
			get
			{
				return this._DateCompleted;
			}
			set
			{
				this._DateCompleted = value;
				this.HasDateCompleted = (value != null);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		public bool DoNotAck
		{
			get
			{
				return this._DoNotAck;
			}
			set
			{
				this._DoNotAck = value;
				this.HasDoNotAck = true;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000C7B8 File Offset: 0x0000A9B8
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0000C7C0 File Offset: 0x0000A9C0
		public int IntervalRewardCount
		{
			get
			{
				return this._IntervalRewardCount;
			}
			set
			{
				this._IntervalRewardCount = value;
				this.HasIntervalRewardCount = true;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000C7D0 File Offset: 0x0000A9D0
		// (set) Token: 0x06000313 RID: 787 RVA: 0x0000C7D8 File Offset: 0x0000A9D8
		public Date IntervalRewardStart
		{
			get
			{
				return this._IntervalRewardStart;
			}
			set
			{
				this._IntervalRewardStart = value;
				this.HasIntervalRewardStart = (value != null);
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000C7EC File Offset: 0x0000A9EC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			num ^= this.Progress.GetHashCode();
			num ^= this.AckProgress.GetHashCode();
			if (this.HasCompletionCount)
			{
				num ^= this.CompletionCount.GetHashCode();
			}
			if (this.HasActive)
			{
				num ^= this.Active.GetHashCode();
			}
			if (this.HasStartedCount)
			{
				num ^= this.StartedCount.GetHashCode();
			}
			if (this.HasDateGiven)
			{
				num ^= this.DateGiven.GetHashCode();
			}
			if (this.HasDateCompleted)
			{
				num ^= this.DateCompleted.GetHashCode();
			}
			if (this.HasDoNotAck)
			{
				num ^= this.DoNotAck.GetHashCode();
			}
			if (this.HasIntervalRewardCount)
			{
				num ^= this.IntervalRewardCount.GetHashCode();
			}
			if (this.HasIntervalRewardStart)
			{
				num ^= this.IntervalRewardStart.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
		public override bool Equals(object obj)
		{
			Achieve achieve = obj as Achieve;
			return achieve != null && this.Id.Equals(achieve.Id) && this.Progress.Equals(achieve.Progress) && this.AckProgress.Equals(achieve.AckProgress) && this.HasCompletionCount == achieve.HasCompletionCount && (!this.HasCompletionCount || this.CompletionCount.Equals(achieve.CompletionCount)) && this.HasActive == achieve.HasActive && (!this.HasActive || this.Active.Equals(achieve.Active)) && this.HasStartedCount == achieve.HasStartedCount && (!this.HasStartedCount || this.StartedCount.Equals(achieve.StartedCount)) && this.HasDateGiven == achieve.HasDateGiven && (!this.HasDateGiven || this.DateGiven.Equals(achieve.DateGiven)) && this.HasDateCompleted == achieve.HasDateCompleted && (!this.HasDateCompleted || this.DateCompleted.Equals(achieve.DateCompleted)) && this.HasDoNotAck == achieve.HasDoNotAck && (!this.HasDoNotAck || this.DoNotAck.Equals(achieve.DoNotAck)) && this.HasIntervalRewardCount == achieve.HasIntervalRewardCount && (!this.HasIntervalRewardCount || this.IntervalRewardCount.Equals(achieve.IntervalRewardCount)) && this.HasIntervalRewardStart == achieve.HasIntervalRewardStart && (!this.HasIntervalRewardStart || this.IntervalRewardStart.Equals(achieve.IntervalRewardStart));
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000CAC1 File Offset: 0x0000ACC1
		public void Deserialize(Stream stream)
		{
			Achieve.Deserialize(stream, this);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000CACB File Offset: 0x0000ACCB
		public static Achieve Deserialize(Stream stream, Achieve instance)
		{
			return Achieve.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		public static Achieve DeserializeLengthDelimited(Stream stream)
		{
			Achieve achieve = new Achieve();
			Achieve.DeserializeLengthDelimited(stream, achieve);
			return achieve;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000CAF4 File Offset: 0x0000ACF4
		public static Achieve DeserializeLengthDelimited(Stream stream, Achieve instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Achieve.Deserialize(stream, instance, num);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		public static Achieve Deserialize(Stream stream, Achieve instance, long limit)
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
					if (num <= 40)
					{
						if (num <= 16)
						{
							if (num == 8)
							{
								instance.Id = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 16)
							{
								instance.Progress = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 24)
							{
								instance.AckProgress = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 32)
							{
								instance.CompletionCount = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 40)
							{
								instance.Active = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
					}
					else if (num <= 66)
					{
						if (num == 48)
						{
							instance.StartedCount = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num != 58)
						{
							if (num == 66)
							{
								if (instance.DateCompleted == null)
								{
									instance.DateCompleted = Date.DeserializeLengthDelimited(stream);
									continue;
								}
								Date.DeserializeLengthDelimited(stream, instance.DateCompleted);
								continue;
							}
						}
						else
						{
							if (instance.DateGiven == null)
							{
								instance.DateGiven = Date.DeserializeLengthDelimited(stream);
								continue;
							}
							Date.DeserializeLengthDelimited(stream, instance.DateGiven);
							continue;
						}
					}
					else
					{
						if (num == 72)
						{
							instance.DoNotAck = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 80)
						{
							instance.IntervalRewardCount = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 90)
						{
							if (instance.IntervalRewardStart == null)
							{
								instance.IntervalRewardStart = Date.DeserializeLengthDelimited(stream);
								continue;
							}
							Date.DeserializeLengthDelimited(stream, instance.IntervalRewardStart);
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

		// Token: 0x0600031B RID: 795 RVA: 0x0000CD12 File Offset: 0x0000AF12
		public void Serialize(Stream stream)
		{
			Achieve.Serialize(stream, this);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000CD1C File Offset: 0x0000AF1C
		public static void Serialize(Stream stream, Achieve instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Progress));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AckProgress));
			if (instance.HasCompletionCount)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CompletionCount));
			}
			if (instance.HasActive)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.Active);
			}
			if (instance.HasStartedCount)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.StartedCount));
			}
			if (instance.HasDateGiven)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.DateGiven.GetSerializedSize());
				Date.Serialize(stream, instance.DateGiven);
			}
			if (instance.HasDateCompleted)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.DateCompleted.GetSerializedSize());
				Date.Serialize(stream, instance.DateCompleted);
			}
			if (instance.HasDoNotAck)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.DoNotAck);
			}
			if (instance.HasIntervalRewardCount)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.IntervalRewardCount));
			}
			if (instance.HasIntervalRewardStart)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.IntervalRewardStart.GetSerializedSize());
				Date.Serialize(stream, instance.IntervalRewardStart);
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000CE80 File Offset: 0x0000B080
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Progress));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AckProgress));
			if (this.HasCompletionCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CompletionCount));
			}
			if (this.HasActive)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasStartedCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.StartedCount));
			}
			if (this.HasDateGiven)
			{
				num += 1U;
				uint serializedSize = this.DateGiven.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasDateCompleted)
			{
				num += 1U;
				uint serializedSize2 = this.DateCompleted.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasDoNotAck)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIntervalRewardCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.IntervalRewardCount));
			}
			if (this.HasIntervalRewardStart)
			{
				num += 1U;
				uint serializedSize3 = this.IntervalRewardStart.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 3U;
		}

		// Token: 0x040000F8 RID: 248
		public bool HasCompletionCount;

		// Token: 0x040000F9 RID: 249
		private int _CompletionCount;

		// Token: 0x040000FA RID: 250
		public bool HasActive;

		// Token: 0x040000FB RID: 251
		private bool _Active;

		// Token: 0x040000FC RID: 252
		public bool HasStartedCount;

		// Token: 0x040000FD RID: 253
		private int _StartedCount;

		// Token: 0x040000FE RID: 254
		public bool HasDateGiven;

		// Token: 0x040000FF RID: 255
		private Date _DateGiven;

		// Token: 0x04000100 RID: 256
		public bool HasDateCompleted;

		// Token: 0x04000101 RID: 257
		private Date _DateCompleted;

		// Token: 0x04000102 RID: 258
		public bool HasDoNotAck;

		// Token: 0x04000103 RID: 259
		private bool _DoNotAck;

		// Token: 0x04000104 RID: 260
		public bool HasIntervalRewardCount;

		// Token: 0x04000105 RID: 261
		private int _IntervalRewardCount;

		// Token: 0x04000106 RID: 262
		public bool HasIntervalRewardStart;

		// Token: 0x04000107 RID: 263
		private Date _IntervalRewardStart;
	}
}
