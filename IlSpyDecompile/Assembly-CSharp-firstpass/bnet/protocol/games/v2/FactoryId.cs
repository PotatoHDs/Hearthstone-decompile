using System.IO;

namespace bnet.protocol.games.v2
{
	public class FactoryId : IProtoBuf
	{
		public bool HasId;

		private ulong _Id;

		public ulong Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				HasId = true;
			}
		}

		public bool IsInitialized => true;

		public void SetId(ulong val)
		{
			Id = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FactoryId factoryId = obj as FactoryId;
			if (factoryId == null)
			{
				return false;
			}
			if (HasId != factoryId.HasId || (HasId && !Id.Equals(factoryId.Id)))
			{
				return false;
			}
			return true;
		}

		public static FactoryId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FactoryId>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FactoryId Deserialize(Stream stream, FactoryId instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FactoryId DeserializeLengthDelimited(Stream stream)
		{
			FactoryId factoryId = new FactoryId();
			DeserializeLengthDelimited(stream, factoryId);
			return factoryId;
		}

		public static FactoryId DeserializeLengthDelimited(Stream stream, FactoryId instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FactoryId Deserialize(Stream stream, FactoryId instance, long limit)
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
					instance.Id = binaryReader.ReadUInt64();
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

		public static void Serialize(Stream stream, FactoryId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.Id);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasId)
			{
				num++;
				num += 8;
			}
			return num;
		}
	}
}
