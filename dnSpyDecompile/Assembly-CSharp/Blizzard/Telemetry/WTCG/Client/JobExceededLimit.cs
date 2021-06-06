using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011CE RID: 4558
	public class JobExceededLimit : IProtoBuf
	{
		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x0600CADC RID: 51932 RVA: 0x003CB86C File Offset: 0x003C9A6C
		// (set) Token: 0x0600CADD RID: 51933 RVA: 0x003CB874 File Offset: 0x003C9A74
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

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x0600CADE RID: 51934 RVA: 0x003CB887 File Offset: 0x003C9A87
		// (set) Token: 0x0600CADF RID: 51935 RVA: 0x003CB88F File Offset: 0x003C9A8F
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

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x0600CAE0 RID: 51936 RVA: 0x003CB89F File Offset: 0x003C9A9F
		// (set) Token: 0x0600CAE1 RID: 51937 RVA: 0x003CB8A7 File Offset: 0x003C9AA7
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

		// Token: 0x0600CAE2 RID: 51938 RVA: 0x003CB8BC File Offset: 0x003C9ABC
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

		// Token: 0x0600CAE3 RID: 51939 RVA: 0x003CB91C File Offset: 0x003C9B1C
		public override bool Equals(object obj)
		{
			JobExceededLimit jobExceededLimit = obj as JobExceededLimit;
			return jobExceededLimit != null && this.HasJobId == jobExceededLimit.HasJobId && (!this.HasJobId || this.JobId.Equals(jobExceededLimit.JobId)) && this.HasJobDuration == jobExceededLimit.HasJobDuration && (!this.HasJobDuration || this.JobDuration.Equals(jobExceededLimit.JobDuration)) && this.HasTestType == jobExceededLimit.HasTestType && (!this.HasTestType || this.TestType.Equals(jobExceededLimit.TestType));
		}

		// Token: 0x0600CAE4 RID: 51940 RVA: 0x003CB9BA File Offset: 0x003C9BBA
		public void Deserialize(Stream stream)
		{
			JobExceededLimit.Deserialize(stream, this);
		}

		// Token: 0x0600CAE5 RID: 51941 RVA: 0x003CB9C4 File Offset: 0x003C9BC4
		public static JobExceededLimit Deserialize(Stream stream, JobExceededLimit instance)
		{
			return JobExceededLimit.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CAE6 RID: 51942 RVA: 0x003CB9D0 File Offset: 0x003C9BD0
		public static JobExceededLimit DeserializeLengthDelimited(Stream stream)
		{
			JobExceededLimit jobExceededLimit = new JobExceededLimit();
			JobExceededLimit.DeserializeLengthDelimited(stream, jobExceededLimit);
			return jobExceededLimit;
		}

		// Token: 0x0600CAE7 RID: 51943 RVA: 0x003CB9EC File Offset: 0x003C9BEC
		public static JobExceededLimit DeserializeLengthDelimited(Stream stream, JobExceededLimit instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JobExceededLimit.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CAE8 RID: 51944 RVA: 0x003CBA14 File Offset: 0x003C9C14
		public static JobExceededLimit Deserialize(Stream stream, JobExceededLimit instance, long limit)
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

		// Token: 0x0600CAE9 RID: 51945 RVA: 0x003CBAC2 File Offset: 0x003C9CC2
		public void Serialize(Stream stream)
		{
			JobExceededLimit.Serialize(stream, this);
		}

		// Token: 0x0600CAEA RID: 51946 RVA: 0x003CBACC File Offset: 0x003C9CCC
		public static void Serialize(Stream stream, JobExceededLimit instance)
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

		// Token: 0x0600CAEB RID: 51947 RVA: 0x003CBB44 File Offset: 0x003C9D44
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

		// Token: 0x04009FFB RID: 40955
		public bool HasJobId;

		// Token: 0x04009FFC RID: 40956
		private string _JobId;

		// Token: 0x04009FFD RID: 40957
		public bool HasJobDuration;

		// Token: 0x04009FFE RID: 40958
		private long _JobDuration;

		// Token: 0x04009FFF RID: 40959
		public bool HasTestType;

		// Token: 0x0400A000 RID: 40960
		private string _TestType;
	}
}
