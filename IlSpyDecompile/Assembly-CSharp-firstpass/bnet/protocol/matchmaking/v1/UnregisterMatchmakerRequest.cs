using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class UnregisterMatchmakerRequest : IProtoBuf
	{
		public bool HasMatchmakerId;

		private uint _MatchmakerId;

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
			if (HasMatchmakerGuid)
			{
				num ^= MatchmakerGuid.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UnregisterMatchmakerRequest unregisterMatchmakerRequest = obj as UnregisterMatchmakerRequest;
			if (unregisterMatchmakerRequest == null)
			{
				return false;
			}
			if (HasMatchmakerId != unregisterMatchmakerRequest.HasMatchmakerId || (HasMatchmakerId && !MatchmakerId.Equals(unregisterMatchmakerRequest.MatchmakerId)))
			{
				return false;
			}
			if (HasMatchmakerGuid != unregisterMatchmakerRequest.HasMatchmakerGuid || (HasMatchmakerGuid && !MatchmakerGuid.Equals(unregisterMatchmakerRequest.MatchmakerGuid)))
			{
				return false;
			}
			return true;
		}

		public static UnregisterMatchmakerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnregisterMatchmakerRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UnregisterMatchmakerRequest Deserialize(Stream stream, UnregisterMatchmakerRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UnregisterMatchmakerRequest DeserializeLengthDelimited(Stream stream)
		{
			UnregisterMatchmakerRequest unregisterMatchmakerRequest = new UnregisterMatchmakerRequest();
			DeserializeLengthDelimited(stream, unregisterMatchmakerRequest);
			return unregisterMatchmakerRequest;
		}

		public static UnregisterMatchmakerRequest DeserializeLengthDelimited(Stream stream, UnregisterMatchmakerRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UnregisterMatchmakerRequest Deserialize(Stream stream, UnregisterMatchmakerRequest instance, long limit)
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
				case 17:
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

		public static void Serialize(Stream stream, UnregisterMatchmakerRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.MatchmakerId);
			}
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(17);
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
			if (HasMatchmakerGuid)
			{
				num++;
				num += 8;
			}
			return num;
		}
	}
}
