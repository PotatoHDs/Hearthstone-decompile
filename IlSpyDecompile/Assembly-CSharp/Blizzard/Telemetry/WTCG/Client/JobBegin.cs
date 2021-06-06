using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class JobBegin : IProtoBuf
	{
		public bool HasJobId;

		private string _JobId;

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
			if (HasTestType)
			{
				num ^= TestType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			JobBegin jobBegin = obj as JobBegin;
			if (jobBegin == null)
			{
				return false;
			}
			if (HasJobId != jobBegin.HasJobId || (HasJobId && !JobId.Equals(jobBegin.JobId)))
			{
				return false;
			}
			if (HasTestType != jobBegin.HasTestType || (HasTestType && !TestType.Equals(jobBegin.TestType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static JobBegin Deserialize(Stream stream, JobBegin instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static JobBegin DeserializeLengthDelimited(Stream stream)
		{
			JobBegin jobBegin = new JobBegin();
			DeserializeLengthDelimited(stream, jobBegin);
			return jobBegin;
		}

		public static JobBegin DeserializeLengthDelimited(Stream stream, JobBegin instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static JobBegin Deserialize(Stream stream, JobBegin instance, long limit)
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
				case 18:
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
			return num;
		}
	}
}
