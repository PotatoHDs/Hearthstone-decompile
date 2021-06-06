using System.IO;

namespace bnet.protocol.games.v1
{
	public class SubscribeRequest : IProtoBuf
	{
		public ulong ObjectId { get; set; }

		public bool IsInitialized => true;

		public void SetObjectId(ulong val)
		{
			ObjectId = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ObjectId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			if (subscribeRequest == null)
			{
				return false;
			}
			if (!ObjectId.Equals(subscribeRequest.ObjectId))
			{
				return false;
			}
			return true;
		}

		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance, long limit)
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
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SubscribeRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64(ObjectId) + 1;
		}
	}
}
