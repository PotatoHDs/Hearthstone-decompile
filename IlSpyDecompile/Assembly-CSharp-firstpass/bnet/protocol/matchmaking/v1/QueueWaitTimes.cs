using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class QueueWaitTimes : IProtoBuf
	{
		public bool HasMinWait;

		private int _MinWait;

		public bool HasMaxWait;

		private int _MaxWait;

		public bool HasAvgWait;

		private int _AvgWait;

		public bool HasStdDevWait;

		private int _StdDevWait;

		public int MinWait
		{
			get
			{
				return _MinWait;
			}
			set
			{
				_MinWait = value;
				HasMinWait = true;
			}
		}

		public int MaxWait
		{
			get
			{
				return _MaxWait;
			}
			set
			{
				_MaxWait = value;
				HasMaxWait = true;
			}
		}

		public int AvgWait
		{
			get
			{
				return _AvgWait;
			}
			set
			{
				_AvgWait = value;
				HasAvgWait = true;
			}
		}

		public int StdDevWait
		{
			get
			{
				return _StdDevWait;
			}
			set
			{
				_StdDevWait = value;
				HasStdDevWait = true;
			}
		}

		public bool IsInitialized => true;

		public void SetMinWait(int val)
		{
			MinWait = val;
		}

		public void SetMaxWait(int val)
		{
			MaxWait = val;
		}

		public void SetAvgWait(int val)
		{
			AvgWait = val;
		}

		public void SetStdDevWait(int val)
		{
			StdDevWait = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMinWait)
			{
				num ^= MinWait.GetHashCode();
			}
			if (HasMaxWait)
			{
				num ^= MaxWait.GetHashCode();
			}
			if (HasAvgWait)
			{
				num ^= AvgWait.GetHashCode();
			}
			if (HasStdDevWait)
			{
				num ^= StdDevWait.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			QueueWaitTimes queueWaitTimes = obj as QueueWaitTimes;
			if (queueWaitTimes == null)
			{
				return false;
			}
			if (HasMinWait != queueWaitTimes.HasMinWait || (HasMinWait && !MinWait.Equals(queueWaitTimes.MinWait)))
			{
				return false;
			}
			if (HasMaxWait != queueWaitTimes.HasMaxWait || (HasMaxWait && !MaxWait.Equals(queueWaitTimes.MaxWait)))
			{
				return false;
			}
			if (HasAvgWait != queueWaitTimes.HasAvgWait || (HasAvgWait && !AvgWait.Equals(queueWaitTimes.AvgWait)))
			{
				return false;
			}
			if (HasStdDevWait != queueWaitTimes.HasStdDevWait || (HasStdDevWait && !StdDevWait.Equals(queueWaitTimes.StdDevWait)))
			{
				return false;
			}
			return true;
		}

		public static QueueWaitTimes ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueWaitTimes>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static QueueWaitTimes Deserialize(Stream stream, QueueWaitTimes instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static QueueWaitTimes DeserializeLengthDelimited(Stream stream)
		{
			QueueWaitTimes queueWaitTimes = new QueueWaitTimes();
			DeserializeLengthDelimited(stream, queueWaitTimes);
			return queueWaitTimes;
		}

		public static QueueWaitTimes DeserializeLengthDelimited(Stream stream, QueueWaitTimes instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static QueueWaitTimes Deserialize(Stream stream, QueueWaitTimes instance, long limit)
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
				case 8:
					instance.MinWait = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.MaxWait = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.AvgWait = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.StdDevWait = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, QueueWaitTimes instance)
		{
			if (instance.HasMinWait)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MinWait);
			}
			if (instance.HasMaxWait)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxWait);
			}
			if (instance.HasAvgWait)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AvgWait);
			}
			if (instance.HasStdDevWait)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.StdDevWait);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMinWait)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MinWait);
			}
			if (HasMaxWait)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MaxWait);
			}
			if (HasAvgWait)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)AvgWait);
			}
			if (HasStdDevWait)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)StdDevWait);
			}
			return num;
		}
	}
}
