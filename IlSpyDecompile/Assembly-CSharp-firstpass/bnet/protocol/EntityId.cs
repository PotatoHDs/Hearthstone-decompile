using System.IO;

namespace bnet.protocol
{
	public class EntityId : IProtoBuf
	{
		public ulong High { get; set; }

		public ulong Low { get; set; }

		public bool IsInitialized => true;

		public void SetHigh(ulong val)
		{
			High = val;
		}

		public void SetLow(ulong val)
		{
			Low = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ High.GetHashCode() ^ Low.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			EntityId entityId = obj as EntityId;
			if (entityId == null)
			{
				return false;
			}
			if (!High.Equals(entityId.High))
			{
				return false;
			}
			if (!Low.Equals(entityId.Low))
			{
				return false;
			}
			return true;
		}

		public static EntityId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EntityId>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static EntityId Deserialize(Stream stream, EntityId instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static EntityId DeserializeLengthDelimited(Stream stream)
		{
			EntityId entityId = new EntityId();
			DeserializeLengthDelimited(stream, entityId);
			return entityId;
		}

		public static EntityId DeserializeLengthDelimited(Stream stream, EntityId instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static EntityId Deserialize(Stream stream, EntityId instance, long limit)
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
					instance.High = binaryReader.ReadUInt64();
					continue;
				case 17:
					instance.Low = binaryReader.ReadUInt64();
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

		public static void Serialize(Stream stream, EntityId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.High);
			stream.WriteByte(17);
			binaryWriter.Write(instance.Low);
		}

		public uint GetSerializedSize()
		{
			return 18u;
		}
	}
}
