using System.IO;

namespace bnet.protocol.authentication.v1
{
	public class LogonQueueUpdateRequest : IProtoBuf
	{
		public uint Position { get; set; }

		public ulong EstimatedTime { get; set; }

		public ulong EtaDeviationInSec { get; set; }

		public bool IsInitialized => true;

		public void SetPosition(uint val)
		{
			Position = val;
		}

		public void SetEstimatedTime(ulong val)
		{
			EstimatedTime = val;
		}

		public void SetEtaDeviationInSec(ulong val)
		{
			EtaDeviationInSec = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Position.GetHashCode() ^ EstimatedTime.GetHashCode() ^ EtaDeviationInSec.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			LogonQueueUpdateRequest logonQueueUpdateRequest = obj as LogonQueueUpdateRequest;
			if (logonQueueUpdateRequest == null)
			{
				return false;
			}
			if (!Position.Equals(logonQueueUpdateRequest.Position))
			{
				return false;
			}
			if (!EstimatedTime.Equals(logonQueueUpdateRequest.EstimatedTime))
			{
				return false;
			}
			if (!EtaDeviationInSec.Equals(logonQueueUpdateRequest.EtaDeviationInSec))
			{
				return false;
			}
			return true;
		}

		public static LogonQueueUpdateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<LogonQueueUpdateRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static LogonQueueUpdateRequest Deserialize(Stream stream, LogonQueueUpdateRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static LogonQueueUpdateRequest DeserializeLengthDelimited(Stream stream)
		{
			LogonQueueUpdateRequest logonQueueUpdateRequest = new LogonQueueUpdateRequest();
			DeserializeLengthDelimited(stream, logonQueueUpdateRequest);
			return logonQueueUpdateRequest;
		}

		public static LogonQueueUpdateRequest DeserializeLengthDelimited(Stream stream, LogonQueueUpdateRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static LogonQueueUpdateRequest Deserialize(Stream stream, LogonQueueUpdateRequest instance, long limit)
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
					instance.Position = ProtocolParser.ReadUInt32(stream);
					continue;
				case 16:
					instance.EstimatedTime = ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.EtaDeviationInSec = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, LogonQueueUpdateRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Position);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.EstimatedTime);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.EtaDeviationInSec);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt32(Position) + ProtocolParser.SizeOfUInt64(EstimatedTime) + ProtocolParser.SizeOfUInt64(EtaDeviationInSec) + 3;
		}
	}
}
