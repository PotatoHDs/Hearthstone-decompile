using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class CreateGameEventRequest : IProtoBuf
	{
		public bool HasMatchmakerId;

		private uint _MatchmakerId;

		public bool HasEventInfo;

		private MatchmakingEventInfo _EventInfo;

		public bool HasCreationProperties;

		private GameCreationProperties _CreationProperties;

		public bool HasGameInstanceId;

		private uint _GameInstanceId;

		public bool HasMatchmakerGuid;

		private ulong _MatchmakerGuid;

		public bool HasSkipQueue;

		private bool _SkipQueue;

		public bool HasSkipClientNotifications;

		private bool _SkipClientNotifications;

		public uint MatchmakerId
		{
			get
			{
				return _MatchmakerId;
			}
			set
			{
				_MatchmakerId = value;
				HasMatchmakerId = true;
			}
		}

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

		public GameCreationProperties CreationProperties
		{
			get
			{
				return _CreationProperties;
			}
			set
			{
				_CreationProperties = value;
				HasCreationProperties = value != null;
			}
		}

		public uint GameInstanceId
		{
			get
			{
				return _GameInstanceId;
			}
			set
			{
				_GameInstanceId = value;
				HasGameInstanceId = true;
			}
		}

		public ulong MatchmakerGuid
		{
			get
			{
				return _MatchmakerGuid;
			}
			set
			{
				_MatchmakerGuid = value;
				HasMatchmakerGuid = true;
			}
		}

		public bool SkipQueue
		{
			get
			{
				return _SkipQueue;
			}
			set
			{
				_SkipQueue = value;
				HasSkipQueue = true;
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

		public void SetMatchmakerId(uint val)
		{
			MatchmakerId = val;
		}

		public void SetEventInfo(MatchmakingEventInfo val)
		{
			EventInfo = val;
		}

		public void SetCreationProperties(GameCreationProperties val)
		{
			CreationProperties = val;
		}

		public void SetGameInstanceId(uint val)
		{
			GameInstanceId = val;
		}

		public void SetMatchmakerGuid(ulong val)
		{
			MatchmakerGuid = val;
		}

		public void SetSkipQueue(bool val)
		{
			SkipQueue = val;
		}

		public void SetSkipClientNotifications(bool val)
		{
			SkipClientNotifications = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMatchmakerId)
			{
				num ^= MatchmakerId.GetHashCode();
			}
			if (HasEventInfo)
			{
				num ^= EventInfo.GetHashCode();
			}
			if (HasCreationProperties)
			{
				num ^= CreationProperties.GetHashCode();
			}
			if (HasGameInstanceId)
			{
				num ^= GameInstanceId.GetHashCode();
			}
			if (HasMatchmakerGuid)
			{
				num ^= MatchmakerGuid.GetHashCode();
			}
			if (HasSkipQueue)
			{
				num ^= SkipQueue.GetHashCode();
			}
			if (HasSkipClientNotifications)
			{
				num ^= SkipClientNotifications.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateGameEventRequest createGameEventRequest = obj as CreateGameEventRequest;
			if (createGameEventRequest == null)
			{
				return false;
			}
			if (HasMatchmakerId != createGameEventRequest.HasMatchmakerId || (HasMatchmakerId && !MatchmakerId.Equals(createGameEventRequest.MatchmakerId)))
			{
				return false;
			}
			if (HasEventInfo != createGameEventRequest.HasEventInfo || (HasEventInfo && !EventInfo.Equals(createGameEventRequest.EventInfo)))
			{
				return false;
			}
			if (HasCreationProperties != createGameEventRequest.HasCreationProperties || (HasCreationProperties && !CreationProperties.Equals(createGameEventRequest.CreationProperties)))
			{
				return false;
			}
			if (HasGameInstanceId != createGameEventRequest.HasGameInstanceId || (HasGameInstanceId && !GameInstanceId.Equals(createGameEventRequest.GameInstanceId)))
			{
				return false;
			}
			if (HasMatchmakerGuid != createGameEventRequest.HasMatchmakerGuid || (HasMatchmakerGuid && !MatchmakerGuid.Equals(createGameEventRequest.MatchmakerGuid)))
			{
				return false;
			}
			if (HasSkipQueue != createGameEventRequest.HasSkipQueue || (HasSkipQueue && !SkipQueue.Equals(createGameEventRequest.SkipQueue)))
			{
				return false;
			}
			if (HasSkipClientNotifications != createGameEventRequest.HasSkipClientNotifications || (HasSkipClientNotifications && !SkipClientNotifications.Equals(createGameEventRequest.SkipClientNotifications)))
			{
				return false;
			}
			return true;
		}

		public static CreateGameEventRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameEventRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateGameEventRequest Deserialize(Stream stream, CreateGameEventRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateGameEventRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateGameEventRequest createGameEventRequest = new CreateGameEventRequest();
			DeserializeLengthDelimited(stream, createGameEventRequest);
			return createGameEventRequest;
		}

		public static CreateGameEventRequest DeserializeLengthDelimited(Stream stream, CreateGameEventRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateGameEventRequest Deserialize(Stream stream, CreateGameEventRequest instance, long limit)
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
					instance.MatchmakerId = binaryReader.ReadUInt32();
					continue;
				case 18:
					if (instance.EventInfo == null)
					{
						instance.EventInfo = MatchmakingEventInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						MatchmakingEventInfo.DeserializeLengthDelimited(stream, instance.EventInfo);
					}
					continue;
				case 26:
					if (instance.CreationProperties == null)
					{
						instance.CreationProperties = GameCreationProperties.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameCreationProperties.DeserializeLengthDelimited(stream, instance.CreationProperties);
					}
					continue;
				case 37:
					instance.GameInstanceId = binaryReader.ReadUInt32();
					continue;
				case 41:
					instance.MatchmakerGuid = binaryReader.ReadUInt64();
					continue;
				case 48:
					instance.SkipQueue = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
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

		public static void Serialize(Stream stream, CreateGameEventRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.MatchmakerId);
			}
			if (instance.HasEventInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.EventInfo.GetSerializedSize());
				MatchmakingEventInfo.Serialize(stream, instance.EventInfo);
			}
			if (instance.HasCreationProperties)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.CreationProperties.GetSerializedSize());
				GameCreationProperties.Serialize(stream, instance.CreationProperties);
			}
			if (instance.HasGameInstanceId)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.GameInstanceId);
			}
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(41);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
			if (instance.HasSkipQueue)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.SkipQueue);
			}
			if (instance.HasSkipClientNotifications)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.SkipClientNotifications);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMatchmakerId)
			{
				num++;
				num += 4;
			}
			if (HasEventInfo)
			{
				num++;
				uint serializedSize = EventInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasCreationProperties)
			{
				num++;
				uint serializedSize2 = CreationProperties.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasGameInstanceId)
			{
				num++;
				num += 4;
			}
			if (HasMatchmakerGuid)
			{
				num++;
				num += 8;
			}
			if (HasSkipQueue)
			{
				num++;
				num++;
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
