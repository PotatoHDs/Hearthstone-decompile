using System.IO;

namespace PegasusUtil
{
	public class GetServerTimeResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 365
		}

		public long ServerUnixTime { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ServerUnixTime.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GetServerTimeResponse getServerTimeResponse = obj as GetServerTimeResponse;
			if (getServerTimeResponse == null)
			{
				return false;
			}
			if (!ServerUnixTime.Equals(getServerTimeResponse.ServerUnixTime))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetServerTimeResponse Deserialize(Stream stream, GetServerTimeResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetServerTimeResponse DeserializeLengthDelimited(Stream stream)
		{
			GetServerTimeResponse getServerTimeResponse = new GetServerTimeResponse();
			DeserializeLengthDelimited(stream, getServerTimeResponse);
			return getServerTimeResponse;
		}

		public static GetServerTimeResponse DeserializeLengthDelimited(Stream stream, GetServerTimeResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetServerTimeResponse Deserialize(Stream stream, GetServerTimeResponse instance, long limit)
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
					instance.ServerUnixTime = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GetServerTimeResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ServerUnixTime);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)ServerUnixTime) + 1;
		}
	}
}
