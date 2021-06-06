using System;
using System.IO;

namespace bnet.protocol.games.v2
{
	// Token: 0x0200036D RID: 877
	public class QueueUpdate : IProtoBuf
	{
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x000B67D4 File Offset: 0x000B49D4
		// (set) Token: 0x06003794 RID: 14228 RVA: 0x000B67DC File Offset: 0x000B49DC
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

		// Token: 0x06003795 RID: 14229 RVA: 0x000B67EC File Offset: 0x000B49EC
		public void SetMinWait(int val)
		{
			this.MinWait = val;
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06003796 RID: 14230 RVA: 0x000B67F5 File Offset: 0x000B49F5
		// (set) Token: 0x06003797 RID: 14231 RVA: 0x000B67FD File Offset: 0x000B49FD
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

		// Token: 0x06003798 RID: 14232 RVA: 0x000B680D File Offset: 0x000B4A0D
		public void SetMaxWait(int val)
		{
			this.MaxWait = val;
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000B6816 File Offset: 0x000B4A16
		// (set) Token: 0x0600379A RID: 14234 RVA: 0x000B681E File Offset: 0x000B4A1E
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

		// Token: 0x0600379B RID: 14235 RVA: 0x000B682E File Offset: 0x000B4A2E
		public void SetAvgWait(int val)
		{
			this.AvgWait = val;
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x0600379C RID: 14236 RVA: 0x000B6837 File Offset: 0x000B4A37
		// (set) Token: 0x0600379D RID: 14237 RVA: 0x000B683F File Offset: 0x000B4A3F
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

		// Token: 0x0600379E RID: 14238 RVA: 0x000B684F File Offset: 0x000B4A4F
		public void SetStdDevWait(int val)
		{
			this.StdDevWait = val;
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x000B6858 File Offset: 0x000B4A58
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

		// Token: 0x060037A0 RID: 14240 RVA: 0x000B68D8 File Offset: 0x000B4AD8
		public override bool Equals(object obj)
		{
			QueueUpdate queueUpdate = obj as QueueUpdate;
			return queueUpdate != null && this.HasMinWait == queueUpdate.HasMinWait && (!this.HasMinWait || this.MinWait.Equals(queueUpdate.MinWait)) && this.HasMaxWait == queueUpdate.HasMaxWait && (!this.HasMaxWait || this.MaxWait.Equals(queueUpdate.MaxWait)) && this.HasAvgWait == queueUpdate.HasAvgWait && (!this.HasAvgWait || this.AvgWait.Equals(queueUpdate.AvgWait)) && this.HasStdDevWait == queueUpdate.HasStdDevWait && (!this.HasStdDevWait || this.StdDevWait.Equals(queueUpdate.StdDevWait));
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060037A1 RID: 14241 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x000B69AA File Offset: 0x000B4BAA
		public static QueueUpdate ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueUpdate>(bs, 0, -1);
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000B69B4 File Offset: 0x000B4BB4
		public void Deserialize(Stream stream)
		{
			QueueUpdate.Deserialize(stream, this);
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000B69BE File Offset: 0x000B4BBE
		public static QueueUpdate Deserialize(Stream stream, QueueUpdate instance)
		{
			return QueueUpdate.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x000B69CC File Offset: 0x000B4BCC
		public static QueueUpdate DeserializeLengthDelimited(Stream stream)
		{
			QueueUpdate queueUpdate = new QueueUpdate();
			QueueUpdate.DeserializeLengthDelimited(stream, queueUpdate);
			return queueUpdate;
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000B69E8 File Offset: 0x000B4BE8
		public static QueueUpdate DeserializeLengthDelimited(Stream stream, QueueUpdate instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueueUpdate.Deserialize(stream, instance, num);
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x000B6A10 File Offset: 0x000B4C10
		public static QueueUpdate Deserialize(Stream stream, QueueUpdate instance, long limit)
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

		// Token: 0x060037A8 RID: 14248 RVA: 0x000B6AE4 File Offset: 0x000B4CE4
		public void Serialize(Stream stream)
		{
			QueueUpdate.Serialize(stream, this);
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000B6AF0 File Offset: 0x000B4CF0
		public static void Serialize(Stream stream, QueueUpdate instance)
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

		// Token: 0x060037AA RID: 14250 RVA: 0x000B6B70 File Offset: 0x000B4D70
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

		// Token: 0x040014C1 RID: 5313
		public bool HasMinWait;

		// Token: 0x040014C2 RID: 5314
		private int _MinWait;

		// Token: 0x040014C3 RID: 5315
		public bool HasMaxWait;

		// Token: 0x040014C4 RID: 5316
		private int _MaxWait;

		// Token: 0x040014C5 RID: 5317
		public bool HasAvgWait;

		// Token: 0x040014C6 RID: 5318
		private int _AvgWait;

		// Token: 0x040014C7 RID: 5319
		public bool HasStdDevWait;

		// Token: 0x040014C8 RID: 5320
		private int _StdDevWait;
	}
}
