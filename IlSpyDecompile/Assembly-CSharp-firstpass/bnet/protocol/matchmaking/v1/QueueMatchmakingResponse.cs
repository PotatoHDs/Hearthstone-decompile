using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class QueueMatchmakingResponse : IProtoBuf
	{
		public bool HasRequestId;

		private RequestId _RequestId;

		public RequestId RequestId
		{
			get
			{
				return _RequestId;
			}
			set
			{
				_RequestId = value;
				HasRequestId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetRequestId(RequestId val)
		{
			RequestId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRequestId)
			{
				num ^= RequestId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			QueueMatchmakingResponse queueMatchmakingResponse = obj as QueueMatchmakingResponse;
			if (queueMatchmakingResponse == null)
			{
				return false;
			}
			if (HasRequestId != queueMatchmakingResponse.HasRequestId || (HasRequestId && !RequestId.Equals(queueMatchmakingResponse.RequestId)))
			{
				return false;
			}
			return true;
		}

		public static QueueMatchmakingResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueMatchmakingResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static QueueMatchmakingResponse Deserialize(Stream stream, QueueMatchmakingResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static QueueMatchmakingResponse DeserializeLengthDelimited(Stream stream)
		{
			QueueMatchmakingResponse queueMatchmakingResponse = new QueueMatchmakingResponse();
			DeserializeLengthDelimited(stream, queueMatchmakingResponse);
			return queueMatchmakingResponse;
		}

		public static QueueMatchmakingResponse DeserializeLengthDelimited(Stream stream, QueueMatchmakingResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static QueueMatchmakingResponse Deserialize(Stream stream, QueueMatchmakingResponse instance, long limit)
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
				case 10:
					if (instance.RequestId == null)
					{
						instance.RequestId = RequestId.DeserializeLengthDelimited(stream);
					}
					else
					{
						RequestId.DeserializeLengthDelimited(stream, instance.RequestId);
					}
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

		public static void Serialize(Stream stream, QueueMatchmakingResponse instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRequestId)
			{
				num++;
				uint serializedSize = RequestId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
