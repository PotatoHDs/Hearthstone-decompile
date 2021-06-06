using System.IO;

namespace bnet.protocol.games.v1
{
	public class CreateGameBatchResponse : IProtoBuf
	{
		public bool IsInitialized => true;

		public override int GetHashCode()
		{
			return GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is CreateGameBatchResponse))
			{
				return false;
			}
			return true;
		}

		public static CreateGameBatchResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameBatchResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateGameBatchResponse Deserialize(Stream stream, CreateGameBatchResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateGameBatchResponse DeserializeLengthDelimited(Stream stream)
		{
			CreateGameBatchResponse createGameBatchResponse = new CreateGameBatchResponse();
			DeserializeLengthDelimited(stream, createGameBatchResponse);
			return createGameBatchResponse;
		}

		public static CreateGameBatchResponse DeserializeLengthDelimited(Stream stream, CreateGameBatchResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateGameBatchResponse Deserialize(Stream stream, CreateGameBatchResponse instance, long limit)
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

		public static void Serialize(Stream stream, CreateGameBatchResponse instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
