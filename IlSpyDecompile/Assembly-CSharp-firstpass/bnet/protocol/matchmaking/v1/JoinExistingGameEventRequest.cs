using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class JoinExistingGameEventRequest : IProtoBuf
	{
		public bool HasEventInfo;

		private MatchmakingEventInfo _EventInfo;

		public MatchmakingEventInfo EventInfo
		{
			get
			{
				return _EventInfo;
			}
			set
			{
				_EventInfo = value;
				HasEventInfo = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetEventInfo(MatchmakingEventInfo val)
		{
			EventInfo = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasEventInfo)
			{
				num ^= EventInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			JoinExistingGameEventRequest joinExistingGameEventRequest = obj as JoinExistingGameEventRequest;
			if (joinExistingGameEventRequest == null)
			{
				return false;
			}
			if (HasEventInfo != joinExistingGameEventRequest.HasEventInfo || (HasEventInfo && !EventInfo.Equals(joinExistingGameEventRequest.EventInfo)))
			{
				return false;
			}
			return true;
		}

		public static JoinExistingGameEventRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinExistingGameEventRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static JoinExistingGameEventRequest Deserialize(Stream stream, JoinExistingGameEventRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static JoinExistingGameEventRequest DeserializeLengthDelimited(Stream stream)
		{
			JoinExistingGameEventRequest joinExistingGameEventRequest = new JoinExistingGameEventRequest();
			DeserializeLengthDelimited(stream, joinExistingGameEventRequest);
			return joinExistingGameEventRequest;
		}

		public static JoinExistingGameEventRequest DeserializeLengthDelimited(Stream stream, JoinExistingGameEventRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static JoinExistingGameEventRequest Deserialize(Stream stream, JoinExistingGameEventRequest instance, long limit)
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
					if (instance.EventInfo == null)
					{
						instance.EventInfo = MatchmakingEventInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						MatchmakingEventInfo.DeserializeLengthDelimited(stream, instance.EventInfo);
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

		public static void Serialize(Stream stream, JoinExistingGameEventRequest instance)
		{
			if (instance.HasEventInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EventInfo.GetSerializedSize());
				MatchmakingEventInfo.Serialize(stream, instance.EventInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasEventInfo)
			{
				num++;
				uint serializedSize = EventInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
