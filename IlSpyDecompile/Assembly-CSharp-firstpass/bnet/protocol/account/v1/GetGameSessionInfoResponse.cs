using System.IO;

namespace bnet.protocol.account.v1
{
	public class GetGameSessionInfoResponse : IProtoBuf
	{
		public bool HasSessionInfo;

		private GameSessionInfo _SessionInfo;

		public GameSessionInfo SessionInfo
		{
			get
			{
				return _SessionInfo;
			}
			set
			{
				_SessionInfo = value;
				HasSessionInfo = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetSessionInfo(GameSessionInfo val)
		{
			SessionInfo = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSessionInfo)
			{
				num ^= SessionInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGameSessionInfoResponse getGameSessionInfoResponse = obj as GetGameSessionInfoResponse;
			if (getGameSessionInfoResponse == null)
			{
				return false;
			}
			if (HasSessionInfo != getGameSessionInfoResponse.HasSessionInfo || (HasSessionInfo && !SessionInfo.Equals(getGameSessionInfoResponse.SessionInfo)))
			{
				return false;
			}
			return true;
		}

		public static GetGameSessionInfoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameSessionInfoResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetGameSessionInfoResponse Deserialize(Stream stream, GetGameSessionInfoResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetGameSessionInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGameSessionInfoResponse getGameSessionInfoResponse = new GetGameSessionInfoResponse();
			DeserializeLengthDelimited(stream, getGameSessionInfoResponse);
			return getGameSessionInfoResponse;
		}

		public static GetGameSessionInfoResponse DeserializeLengthDelimited(Stream stream, GetGameSessionInfoResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetGameSessionInfoResponse Deserialize(Stream stream, GetGameSessionInfoResponse instance, long limit)
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
				case 18:
					if (instance.SessionInfo == null)
					{
						instance.SessionInfo = GameSessionInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameSessionInfo.DeserializeLengthDelimited(stream, instance.SessionInfo);
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

		public static void Serialize(Stream stream, GetGameSessionInfoResponse instance)
		{
			if (instance.HasSessionInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SessionInfo.GetSerializedSize());
				GameSessionInfo.Serialize(stream, instance.SessionInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSessionInfo)
			{
				num++;
				uint serializedSize = SessionInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
