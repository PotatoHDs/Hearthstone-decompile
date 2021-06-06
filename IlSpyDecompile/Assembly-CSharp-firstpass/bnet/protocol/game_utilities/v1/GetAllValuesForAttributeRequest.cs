using System.IO;
using System.Text;

namespace bnet.protocol.game_utilities.v1
{
	public class GetAllValuesForAttributeRequest : IProtoBuf
	{
		public bool HasAttributeKey;

		private string _AttributeKey;

		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasProgram;

		private uint _Program;

		public string AttributeKey
		{
			get
			{
				return _AttributeKey;
			}
			set
			{
				_AttributeKey = value;
				HasAttributeKey = value != null;
			}
		}

		public EntityId AgentId
		{
			get
			{
				return _AgentId;
			}
			set
			{
				_AgentId = value;
				HasAgentId = value != null;
			}
		}

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAttributeKey(string val)
		{
			AttributeKey = val;
		}

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAttributeKey)
			{
				num ^= AttributeKey.GetHashCode();
			}
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetAllValuesForAttributeRequest getAllValuesForAttributeRequest = obj as GetAllValuesForAttributeRequest;
			if (getAllValuesForAttributeRequest == null)
			{
				return false;
			}
			if (HasAttributeKey != getAllValuesForAttributeRequest.HasAttributeKey || (HasAttributeKey && !AttributeKey.Equals(getAllValuesForAttributeRequest.AttributeKey)))
			{
				return false;
			}
			if (HasAgentId != getAllValuesForAttributeRequest.HasAgentId || (HasAgentId && !AgentId.Equals(getAllValuesForAttributeRequest.AgentId)))
			{
				return false;
			}
			if (HasProgram != getAllValuesForAttributeRequest.HasProgram || (HasProgram && !Program.Equals(getAllValuesForAttributeRequest.Program)))
			{
				return false;
			}
			return true;
		}

		public static GetAllValuesForAttributeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAllValuesForAttributeRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetAllValuesForAttributeRequest Deserialize(Stream stream, GetAllValuesForAttributeRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetAllValuesForAttributeRequest DeserializeLengthDelimited(Stream stream)
		{
			GetAllValuesForAttributeRequest getAllValuesForAttributeRequest = new GetAllValuesForAttributeRequest();
			DeserializeLengthDelimited(stream, getAllValuesForAttributeRequest);
			return getAllValuesForAttributeRequest;
		}

		public static GetAllValuesForAttributeRequest DeserializeLengthDelimited(Stream stream, GetAllValuesForAttributeRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetAllValuesForAttributeRequest Deserialize(Stream stream, GetAllValuesForAttributeRequest instance, long limit)
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
				case 10:
					instance.AttributeKey = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					if (instance.AgentId == null)
					{
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 45:
					instance.Program = binaryReader.ReadUInt32();
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

		public static void Serialize(Stream stream, GetAllValuesForAttributeRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAttributeKey)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AttributeKey));
			}
			if (instance.HasAgentId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.Program);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAttributeKey)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(AttributeKey);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasAgentId)
			{
				num++;
				uint serializedSize = AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
