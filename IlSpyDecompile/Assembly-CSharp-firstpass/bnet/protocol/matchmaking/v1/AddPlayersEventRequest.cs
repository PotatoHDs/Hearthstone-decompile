using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class AddPlayersEventRequest : IProtoBuf
	{
		public bool HasEventInfo;

		private MatchmakingEventInfo _EventInfo;

		public bool HasGameHandle;

		private GameHandle _GameHandle;

		public bool HasSkipClientNotifications;

		private bool _SkipClientNotifications;

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

		public GameHandle GameHandle
		{
			get
			{
				return _GameHandle;
			}
			set
			{
				_GameHandle = value;
				HasGameHandle = value != null;
			}
		}

		public bool SkipClientNotifications
		{
			get
			{
				return _SkipClientNotifications;
			}
			set
			{
				_SkipClientNotifications = value;
				HasSkipClientNotifications = true;
			}
		}

		public bool IsInitialized => true;

		public void SetEventInfo(MatchmakingEventInfo val)
		{
			EventInfo = val;
		}

		public void SetGameHandle(GameHandle val)
		{
			GameHandle = val;
		}

		public void SetSkipClientNotifications(bool val)
		{
			SkipClientNotifications = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasEventInfo)
			{
				num ^= EventInfo.GetHashCode();
			}
			if (HasGameHandle)
			{
				num ^= GameHandle.GetHashCode();
			}
			if (HasSkipClientNotifications)
			{
				num ^= SkipClientNotifications.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AddPlayersEventRequest addPlayersEventRequest = obj as AddPlayersEventRequest;
			if (addPlayersEventRequest == null)
			{
				return false;
			}
			if (HasEventInfo != addPlayersEventRequest.HasEventInfo || (HasEventInfo && !EventInfo.Equals(addPlayersEventRequest.EventInfo)))
			{
				return false;
			}
			if (HasGameHandle != addPlayersEventRequest.HasGameHandle || (HasGameHandle && !GameHandle.Equals(addPlayersEventRequest.GameHandle)))
			{
				return false;
			}
			if (HasSkipClientNotifications != addPlayersEventRequest.HasSkipClientNotifications || (HasSkipClientNotifications && !SkipClientNotifications.Equals(addPlayersEventRequest.SkipClientNotifications)))
			{
				return false;
			}
			return true;
		}

		public static AddPlayersEventRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddPlayersEventRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AddPlayersEventRequest Deserialize(Stream stream, AddPlayersEventRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AddPlayersEventRequest DeserializeLengthDelimited(Stream stream)
		{
			AddPlayersEventRequest addPlayersEventRequest = new AddPlayersEventRequest();
			DeserializeLengthDelimited(stream, addPlayersEventRequest);
			return addPlayersEventRequest;
		}

		public static AddPlayersEventRequest DeserializeLengthDelimited(Stream stream, AddPlayersEventRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AddPlayersEventRequest Deserialize(Stream stream, AddPlayersEventRequest instance, long limit)
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
				case 18:
					if (instance.GameHandle == null)
					{
						instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
					}
					continue;
				case 24:
					instance.SkipClientNotifications = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, AddPlayersEventRequest instance)
		{
			if (instance.HasEventInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EventInfo.GetSerializedSize());
				MatchmakingEventInfo.Serialize(stream, instance.EventInfo);
			}
			if (instance.HasGameHandle)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasSkipClientNotifications)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.SkipClientNotifications);
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
			if (HasGameHandle)
			{
				num++;
				uint serializedSize2 = GameHandle.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasSkipClientNotifications)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
