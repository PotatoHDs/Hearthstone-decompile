using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class JobExceededLimit : IProtoBuf
	{
		public bool HasJobId;

		private string _JobId;

		public bool HasJobDuration;

		private long _JobDuration;

		public bool HasTestType;

		private string _TestType;

		public string JobId
		{
			get
			{
				return _JobId;
			}
			set
			{
				_JobId = value;
				HasJobId = value != null;
			}
		}

		public long JobDuration
		{
			get
			{
				return _JobDuration;
			}
			set
			{
				_JobDuration = value;
				HasJobDuration = true;
			}
		}

		public string TestType
		{
			get
			{
				return _TestType;
			}
			set
			{
				_TestType = value;
				HasTestType = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasJobId)
			{
				num ^= JobId.GetHashCode();
			}
			if (HasJobDuration)
			{
				num ^= JobDuration.GetHashCode();
			}
			if (HasTestType)
			{
				num ^= TestType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			JobExceededLimit jobExceededLimit = obj as JobExceededLimit;
			if (jobExceededLimit == null)
			{
				return false;
			}
			if (HasJobId != jobExceededLimit.HasJobId || (HasJobId && !JobId.Equals(jobExceededLimit.JobId)))
			{
				return false;
			}
			if (HasJobDuration != jobExceededLimit.HasJobDuration || (HasJobDuration && !JobDuration.Equals(jobExceededLimit.JobDuration)))
			{
				return false;
			}
			if (HasTestType != jobExceededLimit.HasTestType || (HasTestType && !TestType.Equals(jobExceededLimit.TestType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static JobExceededLimit Deserialize(Stream stream, JobExceededLimit instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static JobExceededLimit DeserializeLengthDelimited(Stream stream)
		{
			JobExceededLimit jobExceededLimit = new JobExceededLimit();
			DeserializeLengthDelimited(stream, jobExceededLimit);
			return jobExceededLimit;
		}

		public static JobExceededLimit DeserializeLengthDelimited(Stream stream, JobExceededLimit instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static JobExceededLimit Deserialize(Stream stream, JobExceededLimit instance, long limit)
		{
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
					instance.JobId = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.JobDuration = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.TestType = ProtocolParser.ReadString(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

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

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasJobId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(JobId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasJobDuration)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)JobDuration);
			}
			if (HasTestType)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(TestType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
