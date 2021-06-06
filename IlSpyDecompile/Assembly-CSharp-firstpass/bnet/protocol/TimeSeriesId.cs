using System.IO;

namespace bnet.protocol
{
	public class TimeSeriesId : IProtoBuf
	{
		public bool HasEpoch;

		private ulong _Epoch;

		public bool HasPosition;

		private ulong _Position;

		public ulong Epoch
		{
			get
			{
				return _Epoch;
			}
			set
			{
				_Epoch = value;
				HasEpoch = true;
			}
		}

		public ulong Position
		{
			get
			{
				return _Position;
			}
			set
			{
				_Position = value;
				HasPosition = true;
			}
		}

		public bool IsInitialized => true;

		public void SetEpoch(ulong val)
		{
			Epoch = val;
		}

		public void SetPosition(ulong val)
		{
			Position = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasEpoch)
			{
				num ^= Epoch.GetHashCode();
			}
			if (HasPosition)
			{
				num ^= Position.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			TimeSeriesId timeSeriesId = obj as TimeSeriesId;
			if (timeSeriesId == null)
			{
				return false;
			}
			if (HasEpoch != timeSeriesId.HasEpoch || (HasEpoch && !Epoch.Equals(timeSeriesId.Epoch)))
			{
				return false;
			}
			if (HasPosition != timeSeriesId.HasPosition || (HasPosition && !Position.Equals(timeSeriesId.Position)))
			{
				return false;
			}
			return true;
		}

		public static TimeSeriesId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<TimeSeriesId>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TimeSeriesId Deserialize(Stream stream, TimeSeriesId instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TimeSeriesId DeserializeLengthDelimited(Stream stream)
		{
			TimeSeriesId timeSeriesId = new TimeSeriesId();
			DeserializeLengthDelimited(stream, timeSeriesId);
			return timeSeriesId;
		}

		public static TimeSeriesId DeserializeLengthDelimited(Stream stream, TimeSeriesId instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TimeSeriesId Deserialize(Stream stream, TimeSeriesId instance, long limit)
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
					instance.Epoch = ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Position = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, TimeSeriesId instance)
		{
			if (instance.HasEpoch)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Epoch);
			}
			if (instance.HasPosition)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Position);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasEpoch)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Epoch);
			}
			if (HasPosition)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Position);
			}
			return num;
		}
	}
}
