using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class RegisterMatchmakerRequest : IProtoBuf
	{
		public bool HasMatchmakerId;

		private uint _MatchmakerId;

		public bool HasAttributeInfo;

		private MatchmakerAttributeInfo _AttributeInfo;

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

		public MatchmakerAttributeInfo AttributeInfo
		{
			get
			{
				return _AttributeInfo;
			}
			set
			{
				_AttributeInfo = value;
				HasAttributeInfo = value != null;
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

		public void SetAttributeInfo(MatchmakerAttributeInfo val)
		{
			AttributeInfo = val;
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
			if (HasAttributeInfo)
			{
				num ^= AttributeInfo.GetHashCode();
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
			RegisterMatchmakerRequest registerMatchmakerRequest = obj as RegisterMatchmakerRequest;
			if (registerMatchmakerRequest == null)
			{
				return false;
			}
			if (HasMatchmakerId != registerMatchmakerRequest.HasMatchmakerId || (HasMatchmakerId && !MatchmakerId.Equals(registerMatchmakerRequest.MatchmakerId)))
			{
				return false;
			}
			if (HasAttributeInfo != registerMatchmakerRequest.HasAttributeInfo || (HasAttributeInfo && !AttributeInfo.Equals(registerMatchmakerRequest.AttributeInfo)))
			{
				return false;
			}
			if (HasControlProperties != registerMatchmakerRequest.HasControlProperties || (HasControlProperties && !ControlProperties.Equals(registerMatchmakerRequest.ControlProperties)))
			{
				return false;
			}
			if (HasMatchmakerGuid != registerMatchmakerRequest.HasMatchmakerGuid || (HasMatchmakerGuid && !MatchmakerGuid.Equals(registerMatchmakerRequest.MatchmakerGuid)))
			{
				return false;
			}
			return true;
		}

		public static RegisterMatchmakerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterMatchmakerRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RegisterMatchmakerRequest Deserialize(Stream stream, RegisterMatchmakerRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RegisterMatchmakerRequest DeserializeLengthDelimited(Stream stream)
		{
			RegisterMatchmakerRequest registerMatchmakerRequest = new RegisterMatchmakerRequest();
			DeserializeLengthDelimited(stream, registerMatchmakerRequest);
			return registerMatchmakerRequest;
		}

		public static RegisterMatchmakerRequest DeserializeLengthDelimited(Stream stream, RegisterMatchmakerRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RegisterMatchmakerRequest Deserialize(Stream stream, RegisterMatchmakerRequest instance, long limit)
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
					if (instance.AttributeInfo == null)
					{
						instance.AttributeInfo = MatchmakerAttributeInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						MatchmakerAttributeInfo.DeserializeLengthDelimited(stream, instance.AttributeInfo);
					}
					continue;
				case 26:
					if (instance.ControlProperties == null)
					{
						instance.ControlProperties = MatchmakerControlProperties.DeserializeLengthDelimited(stream);
					}
					else
					{
						MatchmakerControlProperties.DeserializeLengthDelimited(stream, instance.ControlProperties);
					}
					continue;
				case 33:
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

		public static void Serialize(Stream stream, RegisterMatchmakerRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.MatchmakerId);
			}
			if (instance.HasAttributeInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AttributeInfo.GetSerializedSize());
				MatchmakerAttributeInfo.Serialize(stream, instance.AttributeInfo);
			}
			if (instance.HasControlProperties)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ControlProperties.GetSerializedSize());
				MatchmakerControlProperties.Serialize(stream, instance.ControlProperties);
			}
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(33);
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
			if (HasAttributeInfo)
			{
				num++;
				uint serializedSize = AttributeInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasControlProperties)
			{
				num++;
				uint serializedSize2 = ControlProperties.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
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
