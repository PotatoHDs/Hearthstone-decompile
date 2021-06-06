using System.IO;

namespace bnet.protocol.account.v1
{
	public class GameAccountHandle : IProtoBuf
	{
		public uint Id { get; set; }

		public uint Program { get; set; }

		public uint Region { get; set; }

		public bool IsInitialized => true;

		public void SetId(uint val)
		{
			Id = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetRegion(uint val)
		{
			Region = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Id.GetHashCode() ^ Program.GetHashCode() ^ Region.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GameAccountHandle gameAccountHandle = obj as GameAccountHandle;
			if (gameAccountHandle == null)
			{
				return false;
			}
			if (!Id.Equals(gameAccountHandle.Id))
			{
				return false;
			}
			if (!Program.Equals(gameAccountHandle.Program))
			{
				return false;
			}
			if (!Region.Equals(gameAccountHandle.Region))
			{
				return false;
			}
			return true;
		}

		public static GameAccountHandle ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountHandle>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameAccountHandle Deserialize(Stream stream, GameAccountHandle instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameAccountHandle DeserializeLengthDelimited(Stream stream)
		{
			GameAccountHandle gameAccountHandle = new GameAccountHandle();
			DeserializeLengthDelimited(stream, gameAccountHandle);
			return gameAccountHandle;
		}

		public static GameAccountHandle DeserializeLengthDelimited(Stream stream, GameAccountHandle instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameAccountHandle Deserialize(Stream stream, GameAccountHandle instance, long limit)
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
					instance.Id = binaryReader.ReadUInt32();
					continue;
				case 21:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 24:
					instance.Region = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, GameAccountHandle instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Id);
			stream.WriteByte(21);
			binaryWriter.Write(instance.Program);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.Region);
		}

		public uint GetSerializedSize()
		{
			return 0 + 4 + 4 + ProtocolParser.SizeOfUInt32(Region) + 3;
		}
	}
}
