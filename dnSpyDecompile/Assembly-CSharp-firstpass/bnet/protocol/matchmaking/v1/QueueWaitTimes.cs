using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003E8 RID: 1000
	public class QueueWaitTimes : IProtoBuf
	{
		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06004201 RID: 16897 RVA: 0x000D1A3B File Offset: 0x000CFC3B
		// (set) Token: 0x06004202 RID: 16898 RVA: 0x000D1A43 File Offset: 0x000CFC43
		public int MinWait
		{
			get
			{
				return this._MinWait;
			}
			set
			{
				this._MinWait = value;
				this.HasMinWait = true;
			}
		}

		// Token: 0x06004203 RID: 16899 RVA: 0x000D1A53 File Offset: 0x000CFC53
		public void SetMinWait(int val)
		{
			this.MinWait = val;
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06004204 RID: 16900 RVA: 0x000D1A5C File Offset: 0x000CFC5C
		// (set) Token: 0x06004205 RID: 16901 RVA: 0x000D1A64 File Offset: 0x000CFC64
		public int MaxWait
		{
			get
			{
				return this._MaxWait;
			}
			set
			{
				this._MaxWait = value;
				this.HasMaxWait = true;
			}
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x000D1A74 File Offset: 0x000CFC74
		public void SetMaxWait(int val)
		{
			this.MaxWait = val;
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06004207 RID: 16903 RVA: 0x000D1A7D File Offset: 0x000CFC7D
		// (set) Token: 0x06004208 RID: 16904 RVA: 0x000D1A85 File Offset: 0x000CFC85
		public int AvgWait
		{
			get
			{
				return this._AvgWait;
			}
			set
			{
				this._AvgWait = value;
				this.HasAvgWait = true;
			}
		}

		// Token: 0x06004209 RID: 16905 RVA: 0x000D1A95 File Offset: 0x000CFC95
		public void SetAvgWait(int val)
		{
			this.AvgWait = val;
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x0600420A RID: 16906 RVA: 0x000D1A9E File Offset: 0x000CFC9E
		// (set) Token: 0x0600420B RID: 16907 RVA: 0x000D1AA6 File Offset: 0x000CFCA6
		public int StdDevWait
		{
			get
			{
				return this._StdDevWait;
			}
			set
			{
				this._StdDevWait = value;
				this.HasStdDevWait = true;
			}
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x000D1AB6 File Offset: 0x000CFCB6
		public void SetStdDevWait(int val)
		{
			this.StdDevWait = val;
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x000D1AC0 File Offset: 0x000CFCC0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMinWait)
			{
				num ^= this.MinWait.GetHashCode();
			}
			if (this.HasMaxWait)
			{
				num ^= this.MaxWait.GetHashCode();
			}
			if (this.HasAvgWait)
			{
				num ^= this.AvgWait.GetHashCode();
			}
			if (this.HasStdDevWait)
			{
				num ^= this.StdDevWait.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600420E RID: 16910 RVA: 0x000D1B40 File Offset: 0x000CFD40
		public override bool Equals(object obj)
		{
			QueueWaitTimes queueWaitTimes = obj as QueueWaitTimes;
			return queueWaitTimes != null && this.HasMinWait == queueWaitTimes.HasMinWait && (!this.HasMinWait || this.MinWait.Equals(queueWaitTimes.MinWait)) && this.HasMaxWait == queueWaitTimes.HasMaxWait && (!this.HasMaxWait || this.MaxWait.Equals(queueWaitTimes.MaxWait)) && this.HasAvgWait == queueWaitTimes.HasAvgWait && (!this.HasAvgWait || this.AvgWait.Equals(queueWaitTimes.AvgWait)) && this.HasStdDevWait == queueWaitTimes.HasStdDevWait && (!this.HasStdDevWait || this.StdDevWait.Equals(queueWaitTimes.StdDevWait));
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x0600420F RID: 16911 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x000D1C12 File Offset: 0x000CFE12
		public static QueueWaitTimes ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueWaitTimes>(bs, 0, -1);
		}

		// Token: 0x06004211 RID: 16913 RVA: 0x000D1C1C File Offset: 0x000CFE1C
		public void Deserialize(Stream stream)
		{
			QueueWaitTimes.Deserialize(stream, this);
		}

		// Token: 0x06004212 RID: 16914 RVA: 0x000D1C26 File Offset: 0x000CFE26
		public static QueueWaitTimes Deserialize(Stream stream, QueueWaitTimes instance)
		{
			return QueueWaitTimes.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004213 RID: 16915 RVA: 0x000D1C34 File Offset: 0x000CFE34
		public static QueueWaitTimes DeserializeLengthDelimited(Stream stream)
		{
			QueueWaitTimes queueWaitTimes = new QueueWaitTimes();
			QueueWaitTimes.DeserializeLengthDelimited(stream, queueWaitTimes);
			return queueWaitTimes;
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x000D1C50 File Offset: 0x000CFE50
		public static QueueWaitTimes DeserializeLengthDelimited(Stream stream, QueueWaitTimes instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueueWaitTimes.Deserialize(stream, instance, num);
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x000D1C78 File Offset: 0x000CFE78
		public static QueueWaitTimes Deserialize(Stream stream, QueueWaitTimes instance, long limit)
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
							instance.MinWait = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.MaxWait = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.AvgWait = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.StdDevWait = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06004216 RID: 16918 RVA: 0x000D1D4C File Offset: 0x000CFF4C
		public void Serialize(Stream stream)
		{
			QueueWaitTimes.Serialize(stream, this);
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x000D1D58 File Offset: 0x000CFF58
		public static void Serialize(Stream stream, QueueWaitTimes instance)
		{
			if (instance.HasMinWait)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MinWait));
			}
			if (instance.HasMaxWait)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxWait));
			}
			if (instance.HasAvgWait)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AvgWait));
			}
			if (instance.HasStdDevWait)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.StdDevWait));
			}
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x000D1DD8 File Offset: 0x000CFFD8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMinWait)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MinWait));
			}
			if (this.HasMaxWait)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxWait));
			}
			if (this.HasAvgWait)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AvgWait));
			}
			if (this.HasStdDevWait)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.StdDevWait));
			}
			return num;
		}

		// Token: 0x040016BA RID: 5818
		public bool HasMinWait;

		// Token: 0x040016BB RID: 5819
		private int _MinWait;

		// Token: 0x040016BC RID: 5820
		public bool HasMaxWait;

		// Token: 0x040016BD RID: 5821
		private int _MaxWait;

		// Token: 0x040016BE RID: 5822
		public bool HasAvgWait;

		// Token: 0x040016BF RID: 5823
		private int _AvgWait;

		// Token: 0x040016C0 RID: 5824
		public bool HasStdDevWait;

		// Token: 0x040016C1 RID: 5825
		private int _StdDevWait;
	}
}
