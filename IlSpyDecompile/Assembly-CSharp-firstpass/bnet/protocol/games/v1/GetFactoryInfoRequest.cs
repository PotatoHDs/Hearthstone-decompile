using System.IO;

namespace bnet.protocol.games.v1
{
	public class GetFactoryInfoRequest : IProtoBuf
	{
		public ulong FactoryId { get; set; }

		public bool IsInitialized => true;

		public void SetFactoryId(ulong val)
		{
			FactoryId = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ FactoryId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GetFactoryInfoRequest getFactoryInfoRequest = obj as GetFactoryInfoRequest;
			if (getFactoryInfoRequest == null)
			{
				return false;
			}
			if (!FactoryId.Equals(getFactoryInfoRequest.FactoryId))
			{
				return false;
			}
			return true;
		}

		public static GetFactoryInfoRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFactoryInfoRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetFactoryInfoRequest Deserialize(Stream stream, GetFactoryInfoRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetFactoryInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			GetFactoryInfoRequest getFactoryInfoRequest = new GetFactoryInfoRequest();
			DeserializeLengthDelimited(stream, getFactoryInfoRequest);
			return getFactoryInfoRequest;
		}

		public static GetFactoryInfoRequest DeserializeLengthDelimited(Stream stream, GetFactoryInfoRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetFactoryInfoRequest Deserialize(Stream stream, GetFactoryInfoRequest instance, long limit)
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
				case 9:
					instance.FactoryId = binaryReader.ReadUInt64();
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

		public static void Serialize(Stream stream, GetFactoryInfoRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.FactoryId);
		}

		public uint GetSerializedSize()
		{
			return 9u;
		}
	}
}
