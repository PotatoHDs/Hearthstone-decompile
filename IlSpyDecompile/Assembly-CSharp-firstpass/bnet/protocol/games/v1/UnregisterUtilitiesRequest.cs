using System.IO;

namespace bnet.protocol.games.v1
{
	public class UnregisterUtilitiesRequest : IProtoBuf
	{
		public bool IsInitialized => true;

		public override int GetHashCode()
		{
			return GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is UnregisterUtilitiesRequest))
			{
				return false;
			}
			return true;
		}

		public static UnregisterUtilitiesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnregisterUtilitiesRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UnregisterUtilitiesRequest Deserialize(Stream stream, UnregisterUtilitiesRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UnregisterUtilitiesRequest DeserializeLengthDelimited(Stream stream)
		{
			UnregisterUtilitiesRequest unregisterUtilitiesRequest = new UnregisterUtilitiesRequest();
			DeserializeLengthDelimited(stream, unregisterUtilitiesRequest);
			return unregisterUtilitiesRequest;
		}

		public static UnregisterUtilitiesRequest DeserializeLengthDelimited(Stream stream, UnregisterUtilitiesRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UnregisterUtilitiesRequest Deserialize(Stream stream, UnregisterUtilitiesRequest instance, long limit)
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

		public static void Serialize(Stream stream, UnregisterUtilitiesRequest instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
