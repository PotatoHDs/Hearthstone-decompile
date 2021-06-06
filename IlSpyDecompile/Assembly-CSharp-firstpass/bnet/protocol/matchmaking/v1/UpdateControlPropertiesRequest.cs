using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class UpdateControlPropertiesRequest : IProtoBuf
	{
		public bool HasMatchmakerId;

		private uint _MatchmakerId;

		public bool HasControlProperties;

		private MatchmakerControlProperties _ControlProperties;

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

		public MatchmakerControlProperties ControlProperties
		{
			get
			{
				return _ControlProperties;
			}
			set
			{
				_ControlProperties = value;
				HasControlProperties = value != null;
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

		public void SetControlProperties(MatchmakerControlProperties val)
		{
			ControlProperties = val;
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
			if (HasControlProperties)
			{
				num ^= ControlProperties.GetHashCode();
			}
			if (HasMatchmakerGuid)
			{
				num ^= MatchmakerGuid.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateControlPropertiesRequest updateControlPropertiesRequest = obj as UpdateControlPropertiesRequest;
			if (updateControlPropertiesRequest == null)
			{
				return false;
			}
			if (HasMatchmakerId != updateControlPropertiesRequest.HasMatchmakerId || (HasMatchmakerId && !MatchmakerId.Equals(updateControlPropertiesRequest.MatchmakerId)))
			{
				return false;
			}
			if (HasControlProperties != updateControlPropertiesRequest.HasControlProperties || (HasControlProperties && !ControlProperties.Equals(updateControlPropertiesRequest.ControlProperties)))
			{
				return false;
			}
			if (HasMatchmakerGuid != updateControlPropertiesRequest.HasMatchmakerGuid || (HasMatchmakerGuid && !MatchmakerGuid.Equals(updateControlPropertiesRequest.MatchmakerGuid)))
			{
				return false;
			}
			return true;
		}

		public static UpdateControlPropertiesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateControlPropertiesRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateControlPropertiesRequest Deserialize(Stream stream, UpdateControlPropertiesRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateControlPropertiesRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateControlPropertiesRequest updateControlPropertiesRequest = new UpdateControlPropertiesRequest();
			DeserializeLengthDelimited(stream, updateControlPropertiesRequest);
			return updateControlPropertiesRequest;
		}

		public static UpdateControlPropertiesRequest DeserializeLengthDelimited(Stream stream, UpdateControlPropertiesRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateControlPropertiesRequest Deserialize(Stream stream, UpdateControlPropertiesRequest instance, long limit)
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
					if (instance.ControlProperties == null)
					{
						instance.ControlProperties = MatchmakerControlProperties.DeserializeLengthDelimited(stream);
					}
					else
					{
						MatchmakerControlProperties.DeserializeLengthDelimited(stream, instance.ControlProperties);
					}
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

		public static void Serialize(Stream stream, UpdateControlPropertiesRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.MatchmakerId);
			}
			if (instance.HasControlProperties)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ControlProperties.GetSerializedSize());
				MatchmakerControlProperties.Serialize(stream, instance.ControlProperties);
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
			if (HasControlProperties)
			{
				num++;
				uint serializedSize = ControlProperties.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
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
