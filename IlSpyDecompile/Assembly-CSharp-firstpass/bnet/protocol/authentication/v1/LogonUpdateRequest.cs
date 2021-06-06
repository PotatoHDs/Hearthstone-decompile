using System.IO;

namespace bnet.protocol.authentication.v1
{
	public class LogonUpdateRequest : IProtoBuf
	{
		public uint ErrorCode { get; set; }

		public bool IsInitialized => true;

		public void SetErrorCode(uint val)
		{
			ErrorCode = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ErrorCode.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			LogonUpdateRequest logonUpdateRequest = obj as LogonUpdateRequest;
			if (logonUpdateRequest == null)
			{
				return false;
			}
			if (!ErrorCode.Equals(logonUpdateRequest.ErrorCode))
			{
				return false;
			}
			return true;
		}

		public static LogonUpdateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<LogonUpdateRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static LogonUpdateRequest Deserialize(Stream stream, LogonUpdateRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static LogonUpdateRequest DeserializeLengthDelimited(Stream stream)
		{
			LogonUpdateRequest logonUpdateRequest = new LogonUpdateRequest();
			DeserializeLengthDelimited(stream, logonUpdateRequest);
			return logonUpdateRequest;
		}

		public static LogonUpdateRequest DeserializeLengthDelimited(Stream stream, LogonUpdateRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static LogonUpdateRequest Deserialize(Stream stream, LogonUpdateRequest instance, long limit)
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
					instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, LogonUpdateRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt32(ErrorCode) + 1;
		}
	}
}
