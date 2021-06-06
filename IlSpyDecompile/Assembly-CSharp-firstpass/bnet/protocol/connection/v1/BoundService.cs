using System.IO;

namespace bnet.protocol.connection.v1
{
	public class BoundService : IProtoBuf
	{
		public uint Hash { get; set; }

		public uint Id { get; set; }

		public bool IsInitialized => true;

		public void SetHash(uint val)
		{
			Hash = val;
		}

		public void SetId(uint val)
		{
			Id = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Hash.GetHashCode() ^ Id.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			BoundService boundService = obj as BoundService;
			if (boundService == null)
			{
				return false;
			}
			if (!Hash.Equals(boundService.Hash))
			{
				return false;
			}
			if (!Id.Equals(boundService.Id))
			{
				return false;
			}
			return true;
		}

		public static BoundService ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BoundService>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BoundService Deserialize(Stream stream, BoundService instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BoundService DeserializeLengthDelimited(Stream stream)
		{
			BoundService boundService = new BoundService();
			DeserializeLengthDelimited(stream, boundService);
			return boundService;
		}

		public static BoundService DeserializeLengthDelimited(Stream stream, BoundService instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BoundService Deserialize(Stream stream, BoundService instance, long limit)
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
				case 13:
					instance.Hash = binaryReader.ReadUInt32();
					continue;
				case 16:
					instance.Id = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, BoundService instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Hash);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Id);
		}

		public uint GetSerializedSize()
		{
			return 0 + 4 + ProtocolParser.SizeOfUInt32(Id) + 2;
		}
	}
}
