using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class JobFinishFailure : IProtoBuf
	{
		public bool HasJobId;

		private string _JobId;

		public bool HasJobFailureReason;

		private string _JobFailureReason;

		public bool HasTestType;

		private string _TestType;

		public bool HasClientChangelist;

		private string _ClientChangelist;

		public bool HasDuration;

		private float _Duration;

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

		public string JobFailureReason
		{
			get
			{
				return _JobFailureReason;
			}
			set
			{
				_JobFailureReason = value;
				HasJobFailureReason = value != null;
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

		public string ClientChangelist
		{
			get
			{
				return _ClientChangelist;
			}
			set
			{
				_ClientChangelist = value;
				HasClientChangelist = value != null;
			}
		}

		public float Duration
		{
			get
			{
				return _Duration;
			}
			set
			{
				_Duration = value;
				HasDuration = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasJobId)
			{
				num ^= JobId.GetHashCode();
			}
			if (HasJobFailureReason)
			{
				num ^= JobFailureReason.GetHashCode();
			}
			if (HasTestType)
			{
				num ^= TestType.GetHashCode();
			}
			if (HasClientChangelist)
			{
				num ^= ClientChangelist.GetHashCode();
			}
			if (HasDuration)
			{
				num ^= Duration.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			JobFinishFailure jobFinishFailure = obj as JobFinishFailure;
			if (jobFinishFailure == null)
			{
				return false;
			}
			if (HasJobId != jobFinishFailure.HasJobId || (HasJobId && !JobId.Equals(jobFinishFailure.JobId)))
			{
				return false;
			}
			if (HasJobFailureReason != jobFinishFailure.HasJobFailureReason || (HasJobFailureReason && !JobFailureReason.Equals(jobFinishFailure.JobFailureReason)))
			{
				return false;
			}
			if (HasTestType != jobFinishFailure.HasTestType || (HasTestType && !TestType.Equals(jobFinishFailure.TestType)))
			{
				return false;
			}
			if (HasClientChangelist != jobFinishFailure.HasClientChangelist || (HasClientChangelist && !ClientChangelist.Equals(jobFinishFailure.ClientChangelist)))
			{
				return false;
			}
			if (HasDuration != jobFinishFailure.HasDuration || (HasDuration && !Duration.Equals(jobFinishFailure.Duration)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static JobFinishFailure Deserialize(Stream stream, JobFinishFailure instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static JobFinishFailure DeserializeLengthDelimited(Stream stream)
		{
			JobFinishFailure jobFinishFailure = new JobFinishFailure();
			DeserializeLengthDelimited(stream, jobFinishFailure);
			return jobFinishFailure;
		}

		public static JobFinishFailure DeserializeLengthDelimited(Stream stream, JobFinishFailure instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static JobFinishFailure Deserialize(Stream stream, JobFinishFailure instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 18:
					instance.JobFailureReason = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.TestType = ProtocolParser.ReadString(stream);
					continue;
				case 50:
					instance.ClientChangelist = ProtocolParser.ReadString(stream);
					continue;
				case 61:
					instance.Duration = binaryReader.ReadSingle();
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

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasJobId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(JobId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasJobFailureReason)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(JobFailureReason);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasTestType)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(TestType);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasClientChangelist)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(ClientChangelist);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasDuration)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
