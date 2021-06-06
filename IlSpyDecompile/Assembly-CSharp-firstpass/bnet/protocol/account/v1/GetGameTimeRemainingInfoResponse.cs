using System.IO;

namespace bnet.protocol.account.v1
{
	public class GetGameTimeRemainingInfoResponse : IProtoBuf
	{
		public bool HasGameTimeRemainingInfo;

		private GameTimeRemainingInfo _GameTimeRemainingInfo;

		public GameTimeRemainingInfo GameTimeRemainingInfo
		{
			get
			{
				return _GameTimeRemainingInfo;
			}
			set
			{
				_GameTimeRemainingInfo = value;
				HasGameTimeRemainingInfo = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetGameTimeRemainingInfo(GameTimeRemainingInfo val)
		{
			GameTimeRemainingInfo = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameTimeRemainingInfo)
			{
				num ^= GameTimeRemainingInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGameTimeRemainingInfoResponse getGameTimeRemainingInfoResponse = obj as GetGameTimeRemainingInfoResponse;
			if (getGameTimeRemainingInfoResponse == null)
			{
				return false;
			}
			if (HasGameTimeRemainingInfo != getGameTimeRemainingInfoResponse.HasGameTimeRemainingInfo || (HasGameTimeRemainingInfo && !GameTimeRemainingInfo.Equals(getGameTimeRemainingInfoResponse.GameTimeRemainingInfo)))
			{
				return false;
			}
			return true;
		}

		public static GetGameTimeRemainingInfoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameTimeRemainingInfoResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetGameTimeRemainingInfoResponse Deserialize(Stream stream, GetGameTimeRemainingInfoResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetGameTimeRemainingInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGameTimeRemainingInfoResponse getGameTimeRemainingInfoResponse = new GetGameTimeRemainingInfoResponse();
			DeserializeLengthDelimited(stream, getGameTimeRemainingInfoResponse);
			return getGameTimeRemainingInfoResponse;
		}

		public static GetGameTimeRemainingInfoResponse DeserializeLengthDelimited(Stream stream, GetGameTimeRemainingInfoResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetGameTimeRemainingInfoResponse Deserialize(Stream stream, GetGameTimeRemainingInfoResponse instance, long limit)
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
					if (instance.GameTimeRemainingInfo == null)
					{
						instance.GameTimeRemainingInfo = GameTimeRemainingInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameTimeRemainingInfo.DeserializeLengthDelimited(stream, instance.GameTimeRemainingInfo);
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

		public static void Serialize(Stream stream, GetGameTimeRemainingInfoResponse instance)
		{
			if (instance.HasGameTimeRemainingInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameTimeRemainingInfo.GetSerializedSize());
				GameTimeRemainingInfo.Serialize(stream, instance.GameTimeRemainingInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameTimeRemainingInfo)
			{
				num++;
				uint serializedSize = GameTimeRemainingInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
