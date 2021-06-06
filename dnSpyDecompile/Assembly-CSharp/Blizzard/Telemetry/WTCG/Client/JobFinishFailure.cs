using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011CF RID: 4559
	public class JobFinishFailure : IProtoBuf
	{
		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x0600CAED RID: 51949 RVA: 0x003CBBBE File Offset: 0x003C9DBE
		// (set) Token: 0x0600CAEE RID: 51950 RVA: 0x003CBBC6 File Offset: 0x003C9DC6
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

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x0600CAEF RID: 51951 RVA: 0x003CBBD9 File Offset: 0x003C9DD9
		// (set) Token: 0x0600CAF0 RID: 51952 RVA: 0x003CBBE1 File Offset: 0x003C9DE1
		public string JobFailureReason
		{
			get
			{
				return this._JobFailureReason;
			}
			set
			{
				this._JobFailureReason = value;
				this.HasJobFailureReason = (value != null);
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x0600CAF1 RID: 51953 RVA: 0x003CBBF4 File Offset: 0x003C9DF4
		// (set) Token: 0x0600CAF2 RID: 51954 RVA: 0x003CBBFC File Offset: 0x003C9DFC
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

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x0600CAF3 RID: 51955 RVA: 0x003CBC0F File Offset: 0x003C9E0F
		// (set) Token: 0x0600CAF4 RID: 51956 RVA: 0x003CBC17 File Offset: 0x003C9E17
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

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x0600CAF5 RID: 51957 RVA: 0x003CBC2A File Offset: 0x003C9E2A
		// (set) Token: 0x0600CAF6 RID: 51958 RVA: 0x003CBC32 File Offset: 0x003C9E32
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

		// Token: 0x0600CAF7 RID: 51959 RVA: 0x003CBC44 File Offset: 0x003C9E44
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasJobId)
			{
				num ^= this.JobId.GetHashCode();
			}
			if (this.HasJobFailureReason)
			{
				num ^= this.JobFailureReason.GetHashCode();
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

		// Token: 0x0600CAF8 RID: 51960 RVA: 0x003CBCD0 File Offset: 0x003C9ED0
		public override bool Equals(object obj)
		{
			JobFinishFailure jobFinishFailure = obj as JobFinishFailure;
			return jobFinishFailure != null && this.HasJobId == jobFinishFailure.HasJobId && (!this.HasJobId || this.JobId.Equals(jobFinishFailure.JobId)) && this.HasJobFailureReason == jobFinishFailure.HasJobFailureReason && (!this.HasJobFailureReason || this.JobFailureReason.Equals(jobFinishFailure.JobFailureReason)) && this.HasTestType == jobFinishFailure.HasTestType && (!this.HasTestType || this.TestType.Equals(jobFinishFailure.TestType)) && this.HasClientChangelist == jobFinishFailure.HasClientChangelist && (!this.HasClientChangelist || this.ClientChangelist.Equals(jobFinishFailure.ClientChangelist)) && this.HasDuration == jobFinishFailure.HasDuration && (!this.HasDuration || this.Duration.Equals(jobFinishFailure.Duration));
		}

		// Token: 0x0600CAF9 RID: 51961 RVA: 0x003CBDC4 File Offset: 0x003C9FC4
		public void Deserialize(Stream stream)
		{
			JobFinishFailure.Deserialize(stream, this);
		}

		// Token: 0x0600CAFA RID: 51962 RVA: 0x003CBDCE File Offset: 0x003C9FCE
		public static JobFinishFailure Deserialize(Stream stream, JobFinishFailure instance)
		{
			return JobFinishFailure.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CAFB RID: 51963 RVA: 0x003CBDDC File Offset: 0x003C9FDC
		public static JobFinishFailure DeserializeLengthDelimited(Stream stream)
		{
			JobFinishFailure jobFinishFailure = new JobFinishFailure();
			JobFinishFailure.DeserializeLengthDelimited(stream, jobFinishFailure);
			return jobFinishFailure;
		}

		// Token: 0x0600CAFC RID: 51964 RVA: 0x003CBDF8 File Offset: 0x003C9FF8
		public static JobFinishFailure DeserializeLengthDelimited(Stream stream, JobFinishFailure instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JobFinishFailure.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CAFD RID: 51965 RVA: 0x003CBE20 File Offset: 0x003CA020
		public static JobFinishFailure Deserialize(Stream stream, JobFinishFailure instance, long limit)
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
							instance.JobFailureReason = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.TestType = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 50)
						{
							instance.ClientChangelist = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 61)
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

		// Token: 0x0600CAFE RID: 51966 RVA: 0x003CBF0E File Offset: 0x003CA10E
		public void Serialize(Stream stream)
		{
			JobFinishFailure.Serialize(stream, this);
		}

		// Token: 0x0600CAFF RID: 51967 RVA: 0x003CBF18 File Offset: 0x003CA118
		public static void Serialize(Stream stream, JobFinishFailure instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasJobId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.JobId));
			}
			if (instance.HasJobFailureReason)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.JobFailureReason));
			}
			if (instance.HasTestType)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TestType));
			}
			if (instance.HasClientChangelist)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientChangelist));
			}
			if (instance.HasDuration)
			{
				stream.WriteByte(61);
				binaryWriter.Write(instance.Duration);
			}
		}

		// Token: 0x0600CB00 RID: 51968 RVA: 0x003CBFE0 File Offset: 0x003CA1E0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasJobId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.JobId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasJobFailureReason)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.JobFailureReason);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasTestType)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.TestType);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasClientChangelist)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.ClientChangelist);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasDuration)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x0400A001 RID: 40961
		public bool HasJobId;

		// Token: 0x0400A002 RID: 40962
		private string _JobId;

		// Token: 0x0400A003 RID: 40963
		public bool HasJobFailureReason;

		// Token: 0x0400A004 RID: 40964
		private string _JobFailureReason;

		// Token: 0x0400A005 RID: 40965
		public bool HasTestType;

		// Token: 0x0400A006 RID: 40966
		private string _TestType;

		// Token: 0x0400A007 RID: 40967
		public bool HasClientChangelist;

		// Token: 0x0400A008 RID: 40968
		private string _ClientChangelist;

		// Token: 0x0400A009 RID: 40969
		public bool HasDuration;

		// Token: 0x0400A00A RID: 40970
		private float _Duration;
	}
}
