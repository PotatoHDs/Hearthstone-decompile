using System.IO;

namespace bnet.protocol.connection.v1
{
	public class DisconnectRequest : IProtoBuf
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
			DisconnectRequest disconnectRequest = obj as DisconnectRequest;
			if (disconnectRequest == null)
			{
				return false;
			}
			if (!ErrorCode.Equals(disconnectRequest.ErrorCode))
			{
				return false;
			}
			return true;
		}

		public static DisconnectRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DisconnectRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DisconnectRequest Deserialize(Stream stream, DisconnectRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DisconnectRequest DeserializeLengthDelimited(Stream stream)
		{
			DisconnectRequest disconnectRequest = new DisconnectRequest();
			DeserializeLengthDelimited(stream, disconnectRequest);
			return disconnectRequest;
		}

		public static DisconnectRequest DeserializeLengthDelimited(Stream stream, DisconnectRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DisconnectRequest Deserialize(Stream stream, DisconnectRequest instance, long limit)
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

		public static void Serialize(Stream stream, DisconnectRequest instance)
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
