using System.IO;

namespace bnet.protocol
{
	public class NoData : IProtoBuf
	{
		public bool IsInitialized => true;

		public override int GetHashCode()
		{
			return GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is NoData))
			{
				return false;
			}
			return true;
		}

		public static NoData ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<NoData>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static NoData Deserialize(Stream stream, NoData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static NoData DeserializeLengthDelimited(Stream stream)
		{
			NoData noData = new NoData();
			DeserializeLengthDelimited(stream, noData);
			return noData;
		}

		public static NoData DeserializeLengthDelimited(Stream stream, NoData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static NoData Deserialize(Stream stream, NoData instance, long limit)
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
				if (num == -1)
				{
					if (limit < 0)
					{
						break;
					}
					throw new EndOfStreamException();
				}
				Key key = ProtocolParser.ReadKey((byte)num, stream);
				if (key.Field == 0)
				{
					throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
				}
				ProtocolParser.SkipKey(stream, key);
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, NoData instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
