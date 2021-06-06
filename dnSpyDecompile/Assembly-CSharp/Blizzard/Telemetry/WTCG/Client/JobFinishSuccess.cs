using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011D0 RID: 4560
	public class JobFinishSuccess : IProtoBuf
	{
		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x0600CB02 RID: 51970 RVA: 0x003CC0A3 File Offset: 0x003CA2A3
		// (set) Token: 0x0600CB03 RID: 51971 RVA: 0x003CC0AB File Offset: 0x003CA2AB
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

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x0600CB04 RID: 51972 RVA: 0x003CC0BE File Offset: 0x003CA2BE
		// (set) Token: 0x0600CB05 RID: 51973 RVA: 0x003CC0C6 File Offset: 0x003CA2C6
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

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x0600CB06 RID: 51974 RVA: 0x003CC0D9 File Offset: 0x003CA2D9
		// (set) Token: 0x0600CB07 RID: 51975 RVA: 0x003CC0E1 File Offset: 0x003CA2E1
		public string ClientChangelist
		{
			get
			{
				return this._ClientChangelist;
			}
			set
			{
				this._ClientChangelist = value;
				this.HasClientChangelist = (value != null);
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x0600CB08 RID: 51976 RVA: 0x003CC0F4 File Offset: 0x003CA2F4
		// (set) Token: 0x0600CB09 RID: 51977 RVA: 0x003CC0FC File Offset: 0x003CA2FC
		public float Duration
		{
			get
			{
				return this._Duration;
			}
			set
			{
				this._Duration = value;
				this.HasDuration = true;
			}
		}

		// Token: 0x0600CB0A RID: 51978 RVA: 0x003CC10C File Offset: 0x003CA30C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasJobId)
			{
				num ^= this.JobId.GetHashCode();
			}
			if (this.HasTestType)
			{
				num ^= this.TestType.GetHashCode();
			}
			if (this.HasClientChangelist)
			{
				num ^= this.ClientChangelist.GetHashCode();
			}
			if (this.HasDuration)
			{
				num ^= this.Duration.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CB0B RID: 51979 RVA: 0x003CC184 File Offset: 0x003CA384
		public override bool Equals(object obj)
		{
			JobFinishSuccess jobFinishSuccess = obj as JobFinishSuccess;
			return jobFinishSuccess != null && this.HasJobId == jobFinishSuccess.HasJobId && (!this.HasJobId || this.JobId.Equals(jobFinishSuccess.JobId)) && this.HasTestType == jobFinishSuccess.HasTestType && (!this.HasTestType || this.TestType.Equals(jobFinishSuccess.TestType)) && this.HasClientChangelist == jobFinishSuccess.HasClientChangelist && (!this.HasClientChangelist || this.ClientChangelist.Equals(jobFinishSuccess.ClientChangelist)) && this.HasDuration == jobFinishSuccess.HasDuration && (!this.HasDuration || this.Duration.Equals(jobFinishSuccess.Duration));
		}

		// Token: 0x0600CB0C RID: 51980 RVA: 0x003CC24D File Offset: 0x003CA44D
		public void Deserialize(Stream stream)
		{
			JobFinishSuccess.Deserialize(stream, this);
		}

		// Token: 0x0600CB0D RID: 51981 RVA: 0x003CC257 File Offset: 0x003CA457
		public static JobFinishSuccess Deserialize(Stream stream, JobFinishSuccess instance)
		{
			return JobFinishSuccess.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CB0E RID: 51982 RVA: 0x003CC264 File Offset: 0x003CA464
		public static JobFinishSuccess DeserializeLengthDelimited(Stream stream)
		{
			JobFinishSuccess jobFinishSuccess = new JobFinishSuccess();
			JobFinishSuccess.DeserializeLengthDelimited(stream, jobFinishSuccess);
			return jobFinishSuccess;
		}

		// Token: 0x0600CB0F RID: 51983 RVA: 0x003CC280 File Offset: 0x003CA480
		public static JobFinishSuccess DeserializeLengthDelimited(Stream stream, JobFinishSuccess instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JobFinishSuccess.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CB10 RID: 51984 RVA: 0x003CC2A8 File Offset: 0x003CA4A8
		public static JobFinishSuccess Deserialize(Stream stream, JobFinishSuccess instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num <= 18)
					{
						if (num == 10)
						{
							instance.JobId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 18)
						{
							instance.TestType = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 42)
						{
							instance.ClientChangelist = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 53)
						{
							instance.Duration = binaryReader.ReadSingle();
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

		// Token: 0x0600CB11 RID: 51985 RVA: 0x003CC380 File Offset: 0x003CA580
		public void Serialize(Stream stream)
		{
			JobFinishSuccess.Serialize(stream, this);
		}

		// Token: 0x0600CB12 RID: 51986 RVA: 0x003CC38C File Offset: 0x003CA58C
		public static void Serialize(Stream stream, JobFinishSuccess instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasJobId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.JobId));
			}
			if (instance.HasTestType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TestType));
			}
			if (instance.HasClientChangelist)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientChangelist));
			}
			if (instance.HasDuration)
			{
				stream.WriteByte(53);
				binaryWriter.Write(instance.Duration);
			}
		}

		// Token: 0x0600CB13 RID: 51987 RVA: 0x003CC430 File Offset: 0x003CA630
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasJobId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.JobId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasTestType)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.TestType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasClientChangelist)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.ClientChangelist);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasDuration)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x0400A00B RID: 40971
		public bool HasJobId;

		// Token: 0x0400A00C RID: 40972
		private string _JobId;

		// Token: 0x0400A00D RID: 40973
		public bool HasTestType;

		// Token: 0x0400A00E RID: 40974
		private string _TestType;

		// Token: 0x0400A00F RID: 40975
		public bool HasClientChangelist;

		// Token: 0x0400A010 RID: 40976
		private string _ClientChangelist;

		// Token: 0x0400A011 RID: 40977
		public bool HasDuration;

		// Token: 0x0400A012 RID: 40978
		private float _Duration;
	}
}
