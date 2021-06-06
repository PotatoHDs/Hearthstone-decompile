using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class JobFinishSuccess : IProtoBuf
	{
		public bool HasJobId;

		private string _JobId;

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
			JobFinishSuccess jobFinishSuccess = obj as JobFinishSuccess;
			if (jobFinishSuccess == null)
			{
				return false;
			}
			if (HasJobId != jobFinishSuccess.HasJobId || (HasJobId && !JobId.Equals(jobFinishSuccess.JobId)))
			{
				return false;
			}
			if (HasTestType != jobFinishSuccess.HasTestType || (HasTestType && !TestType.Equals(jobFinishSuccess.TestType)))
			{
				return false;
			}
			if (HasClientChangelist != jobFinishSuccess.HasClientChangelist || (HasClientChangelist && !ClientChangelist.Equals(jobFinishSuccess.ClientChangelist)))
			{
				return false;
			}
			if (HasDuration != jobFinishSuccess.HasDuration || (HasDuration && !Duration.Equals(jobFinishSuccess.Duration)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static JobFinishSuccess Deserialize(Stream stream, JobFinishSuccess instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static JobFinishSuccess DeserializeLengthDelimited(Stream stream)
		{
			JobFinishSuccess jobFinishSuccess = new JobFinishSuccess();
			DeserializeLengthDelimited(stream, jobFinishSuccess);
			return jobFinishSuccess;
		}

		public static JobFinishSuccess DeserializeLengthDelimited(Stream stream, JobFinishSuccess instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static JobFinishSuccess Deserialize(Stream stream, JobFinishSuccess instance, long limit)
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
					instance.TestType = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.ClientChangelist = ProtocolParser.ReadString(stream);
					continue;
				case 53:
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

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasJobId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(JobId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasTestType)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(TestType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasClientChangelist)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(ClientChangelist);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
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
