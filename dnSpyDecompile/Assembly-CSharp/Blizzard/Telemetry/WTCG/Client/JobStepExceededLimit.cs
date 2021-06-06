using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011D1 RID: 4561
	public class JobStepExceededLimit : IProtoBuf
	{
		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x0600CB15 RID: 51989 RVA: 0x003CC4C8 File Offset: 0x003CA6C8
		// (set) Token: 0x0600CB16 RID: 51990 RVA: 0x003CC4D0 File Offset: 0x003CA6D0
		public string JobId
		{
			get
			{
				return this._JobId;
			}
			set
			{
				this._JobId = value;
				this.HasJobId = (value != null);
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x0600CB17 RID: 51991 RVA: 0x003CC4E3 File Offset: 0x003CA6E3
		// (set) Token: 0x0600CB18 RID: 51992 RVA: 0x003CC4EB File Offset: 0x003CA6EB
		public long JobDuration
		{
			get
			{
				return this._JobDuration;
			}
			set
			{
				this._JobDuration = value;
				this.HasJobDuration = true;
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x0600CB19 RID: 51993 RVA: 0x003CC4FB File Offset: 0x003CA6FB
		// (set) Token: 0x0600CB1A RID: 51994 RVA: 0x003CC503 File Offset: 0x003CA703
		public string TestType
		{
			get
			{
				return this._TestType;
			}
			set
			{
				this._TestType = value;
				this.HasTestType = (value != null);
			}
		}

		// Token: 0x0600CB1B RID: 51995 RVA: 0x003CC518 File Offset: 0x003CA718
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasJobId)
			{
				num ^= this.JobId.GetHashCode();
			}
			if (this.HasJobDuration)
			{
				num ^= this.JobDuration.GetHashCode();
			}
			if (this.HasTestType)
			{
				num ^= this.TestType.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CB1C RID: 51996 RVA: 0x003CC578 File Offset: 0x003CA778
		public override bool Equals(object obj)
		{
			JobStepExceededLimit jobStepExceededLimit = obj as JobStepExceededLimit;
			return jobStepExceededLimit != null && this.HasJobId == jobStepExceededLimit.HasJobId && (!this.HasJobId || this.JobId.Equals(jobStepExceededLimit.JobId)) && this.HasJobDuration == jobStepExceededLimit.HasJobDuration && (!this.HasJobDuration || this.JobDuration.Equals(jobStepExceededLimit.JobDuration)) && this.HasTestType == jobStepExceededLimit.HasTestType && (!this.HasTestType || this.TestType.Equals(jobStepExceededLimit.TestType));
		}

		// Token: 0x0600CB1D RID: 51997 RVA: 0x003CC616 File Offset: 0x003CA816
		public void Deserialize(Stream stream)
		{
			JobStepExceededLimit.Deserialize(stream, this);
		}

		// Token: 0x0600CB1E RID: 51998 RVA: 0x003CC620 File Offset: 0x003CA820
		public static JobStepExceededLimit Deserialize(Stream stream, JobStepExceededLimit instance)
		{
			return JobStepExceededLimit.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CB1F RID: 51999 RVA: 0x003CC62C File Offset: 0x003CA82C
		public static JobStepExceededLimit DeserializeLengthDelimited(Stream stream)
		{
			JobStepExceededLimit jobStepExceededLimit = new JobStepExceededLimit();
			JobStepExceededLimit.DeserializeLengthDelimited(stream, jobStepExceededLimit);
			return jobStepExceededLimit;
		}

		// Token: 0x0600CB20 RID: 52000 RVA: 0x003CC648 File Offset: 0x003CA848
		public static JobStepExceededLimit DeserializeLengthDelimited(Stream stream, JobStepExceededLimit instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JobStepExceededLimit.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CB21 RID: 52001 RVA: 0x003CC670 File Offset: 0x003CA870
		public static JobStepExceededLimit Deserialize(Stream stream, JobStepExceededLimit instance, long limit)
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
				else if (num != 10)
				{
					if (num != 16)
					{
						if (num != 26)
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
							instance.TestType = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.JobDuration = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.JobId = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CB22 RID: 52002 RVA: 0x003CC71E File Offset: 0x003CA91E
		public void Serialize(Stream stream)
		{
			JobStepExceededLimit.Serialize(stream, this);
		}

		// Token: 0x0600CB23 RID: 52003 RVA: 0x003CC728 File Offset: 0x003CA928
		public static void Serialize(Stream stream, JobStepExceededLimit instance)
		{
			if (instance.HasJobId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.JobId));
			}
			if (instance.HasJobDuration)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.JobDuration);
			}
			if (instance.HasTestType)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TestType));
			}
		}

		// Token: 0x0600CB24 RID: 52004 RVA: 0x003CC7A0 File Offset: 0x003CA9A0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasJobId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.JobId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasJobDuration)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.JobDuration);
			}
			if (this.HasTestType)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.TestType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x0400A013 RID: 40979
		public bool HasJobId;

		// Token: 0x0400A014 RID: 40980
		private string _JobId;

		// Token: 0x0400A015 RID: 40981
		public bool HasJobDuration;

		// Token: 0x0400A016 RID: 40982
		private long _JobDuration;

		// Token: 0x0400A017 RID: 40983
		public bool HasTestType;

		// Token: 0x0400A018 RID: 40984
		private string _TestType;
	}
}
