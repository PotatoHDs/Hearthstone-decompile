using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class UnregisterEventRequest : IProtoBuf
	{
		public bool HasMatchmakerId;

		private uint _MatchmakerId;

		public bool HasGameInstanceId;

		private uint _GameInstanceId;

		public bool HasMatchmakerGuid;

		private ulong _MatchmakerGuid;

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

		public bool IsInitialized => true;

		public void SetMatchmakerId(uint val)
		{
			MatchmakerId = val;
		}

		public void SetGameInstanceId(uint val)
		{
			GameInstanceId = val;
		}

		public void SetMatchmakerGuid(ulong val)
		{
			MatchmakerGuid = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMatchmakerId)
			{
				num ^= MatchmakerId.GetHashCode();
			}
			if (HasGameInstanceId)
			{
				num ^= GameInstanceId.GetHashCode();
			}
			if (HasMatchmakerGuid)
			{
				num ^= MatchmakerGuid.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UnregisterEventRequest unregisterEventRequest = obj as UnregisterEventRequest;
			if (unregisterEventRequest == null)
			{
				return false;
			}
			if (HasMatchmakerId != unregisterEventRequest.HasMatchmakerId || (HasMatchmakerId && !MatchmakerId.Equals(unregisterEventRequest.MatchmakerId)))
			{
				return false;
			}
			if (HasGameInstanceId != unregisterEventRequest.HasGameInstanceId || (HasGameInstanceId && !GameInstanceId.Equals(unregisterEventRequest.GameInstanceId)))
			{
				return false;
			}
			if (HasMatchmakerGuid != unregisterEventRequest.HasMatchmakerGuid || (HasMatchmakerGuid && !MatchmakerGuid.Equals(unregisterEventRequest.MatchmakerGuid)))
			{
				return false;
			}
			return true;
		}

		public static UnregisterEventRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnregisterEventRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UnregisterEventRequest Deserialize(Stream stream, UnregisterEventRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UnregisterEventRequest DeserializeLengthDelimited(Stream stream)
		{
			UnregisterEventRequest unregisterEventRequest = new UnregisterEventRequest();
			DeserializeLengthDelimited(stream, unregisterEventRequest);
			return unregisterEventRequest;
		}

		public static UnregisterEventRequest DeserializeLengthDelimited(Stream stream, UnregisterEventRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UnregisterEventRequest Deserialize(Stream stream, UnregisterEventRequest instance, long limit)
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
				case 21:
					instance.GameInstanceId = binaryReader.ReadUInt32();
					continue;
				case 25:
					instance.MatchmakerGuid = binaryReader.ReadUInt64();
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

		public static void Serialize(Stream stream, UnregisterEventRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.MatchmakerId);
			}
			if (instance.HasGameInstanceId)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.GameInstanceId);
			}
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.MatchmakerGuid);
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
			return num;
		}
	}
}
