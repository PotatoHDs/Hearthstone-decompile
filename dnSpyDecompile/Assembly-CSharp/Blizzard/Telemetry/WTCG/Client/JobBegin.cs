using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011CD RID: 4557
	public class JobBegin : IProtoBuf
	{
		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x0600CACD RID: 51917 RVA: 0x003CB5C2 File Offset: 0x003C97C2
		// (set) Token: 0x0600CACE RID: 51918 RVA: 0x003CB5CA File Offset: 0x003C97CA
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

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x0600CACF RID: 51919 RVA: 0x003CB5DD File Offset: 0x003C97DD
		// (set) Token: 0x0600CAD0 RID: 51920 RVA: 0x003CB5E5 File Offset: 0x003C97E5
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

		// Token: 0x0600CAD1 RID: 51921 RVA: 0x003CB5F8 File Offset: 0x003C97F8
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
			return num;
		}

		// Token: 0x0600CAD2 RID: 51922 RVA: 0x003CB640 File Offset: 0x003C9840
		public override bool Equals(object obj)
		{
			JobBegin jobBegin = obj as JobBegin;
			return jobBegin != null && this.HasJobId == jobBegin.HasJobId && (!this.HasJobId || this.JobId.Equals(jobBegin.JobId)) && this.HasTestType == jobBegin.HasTestType && (!this.HasTestType || this.TestType.Equals(jobBegin.TestType));
		}

		// Token: 0x0600CAD3 RID: 51923 RVA: 0x003CB6B0 File Offset: 0x003C98B0
		public void Deserialize(Stream stream)
		{
			JobBegin.Deserialize(stream, this);
		}

		// Token: 0x0600CAD4 RID: 51924 RVA: 0x003CB6BA File Offset: 0x003C98BA
		public static JobBegin Deserialize(Stream stream, JobBegin instance)
		{
			return JobBegin.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CAD5 RID: 51925 RVA: 0x003CB6C8 File Offset: 0x003C98C8
		public static JobBegin DeserializeLengthDelimited(Stream stream)
		{
			JobBegin jobBegin = new JobBegin();
			JobBegin.DeserializeLengthDelimited(stream, jobBegin);
			return jobBegin;
		}

		// Token: 0x0600CAD6 RID: 51926 RVA: 0x003CB6E4 File Offset: 0x003C98E4
		public static JobBegin DeserializeLengthDelimited(Stream stream, JobBegin instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JobBegin.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CAD7 RID: 51927 RVA: 0x003CB70C File Offset: 0x003C990C
		public static JobBegin Deserialize(Stream stream, JobBegin instance, long limit)
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
					if (num != 18)
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
					instance.JobId = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CAD8 RID: 51928 RVA: 0x003CB7A4 File Offset: 0x003C99A4
		public void Serialize(Stream stream)
		{
			JobBegin.Serialize(stream, this);
		}

		// Token: 0x0600CAD9 RID: 51929 RVA: 0x003CB7B0 File Offset: 0x003C99B0
		public static void Serialize(Stream stream, JobBegin instance)
		{
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
		}

		// Token: 0x0600CADA RID: 51930 RVA: 0x003CB80C File Offset: 0x003C9A0C
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
			return num;
		}

		// Token: 0x04009FF7 RID: 40951
		public bool HasJobId;

		// Token: 0x04009FF8 RID: 40952
		private string _JobId;

		// Token: 0x04009FF9 RID: 40953
		public bool HasTestType;

		// Token: 0x04009FFA RID: 40954
		private string _TestType;
	}
}
