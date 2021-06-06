using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class MatchmakingRequestFailedNotification : IProtoBuf
	{
		public bool HasRequestId;

		private RequestId _RequestId;

		public bool HasResult;

		private uint _Result;

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

		public uint Result
		{
			get
			{
				return _Result;
			}
			set
			{
				_Result = value;
				HasResult = true;
			}
		}

		public bool IsInitialized => true;

		public void SetRequestId(RequestId val)
		{
			RequestId = val;
		}

		public void SetResult(uint val)
		{
			Result = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRequestId)
			{
				num ^= RequestId.GetHashCode();
			}
			if (HasResult)
			{
				num ^= Result.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MatchmakingRequestFailedNotification matchmakingRequestFailedNotification = obj as MatchmakingRequestFailedNotification;
			if (matchmakingRequestFailedNotification == null)
			{
				return false;
			}
			if (HasRequestId != matchmakingRequestFailedNotification.HasRequestId || (HasRequestId && !RequestId.Equals(matchmakingRequestFailedNotification.RequestId)))
			{
				return false;
			}
			if (HasResult != matchmakingRequestFailedNotification.HasResult || (HasResult && !Result.Equals(matchmakingRequestFailedNotification.Result)))
			{
				return false;
			}
			return true;
		}

		public static MatchmakingRequestFailedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MatchmakingRequestFailedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MatchmakingRequestFailedNotification Deserialize(Stream stream, MatchmakingRequestFailedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MatchmakingRequestFailedNotification DeserializeLengthDelimited(Stream stream)
		{
			MatchmakingRequestFailedNotification matchmakingRequestFailedNotification = new MatchmakingRequestFailedNotification();
			DeserializeLengthDelimited(stream, matchmakingRequestFailedNotification);
			return matchmakingRequestFailedNotification;
		}

		public static MatchmakingRequestFailedNotification DeserializeLengthDelimited(Stream stream, MatchmakingRequestFailedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MatchmakingRequestFailedNotification Deserialize(Stream stream, MatchmakingRequestFailedNotification instance, long limit)
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
				case 16:
					instance.Result = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, MatchmakingRequestFailedNotification instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.HasResult)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Result);
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
			if (HasResult)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Result);
			}
			return num;
		}
	}
}
