using System.IO;

namespace bnet.protocol.friends.v1
{
	public class IgnoreInvitationRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasProgram;

		private uint _Program;

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

		public ulong InvitationId { get; set; }

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

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetInvitationId(ulong val)
		{
			InvitationId = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			num ^= InvitationId.GetHashCode();
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			IgnoreInvitationRequest ignoreInvitationRequest = obj as IgnoreInvitationRequest;
			if (ignoreInvitationRequest == null)
			{
				return false;
			}
			if (HasAgentId != ignoreInvitationRequest.HasAgentId || (HasAgentId && !AgentId.Equals(ignoreInvitationRequest.AgentId)))
			{
				return false;
			}
			if (!InvitationId.Equals(ignoreInvitationRequest.InvitationId))
			{
				return false;
			}
			if (HasProgram != ignoreInvitationRequest.HasProgram || (HasProgram && !Program.Equals(ignoreInvitationRequest.Program)))
			{
				return false;
			}
			return true;
		}

		public static IgnoreInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<IgnoreInvitationRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static IgnoreInvitationRequest Deserialize(Stream stream, IgnoreInvitationRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static IgnoreInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			IgnoreInvitationRequest ignoreInvitationRequest = new IgnoreInvitationRequest();
			DeserializeLengthDelimited(stream, ignoreInvitationRequest);
			return ignoreInvitationRequest;
		}

		public static IgnoreInvitationRequest DeserializeLengthDelimited(Stream stream, IgnoreInvitationRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static IgnoreInvitationRequest Deserialize(Stream stream, IgnoreInvitationRequest instance, long limit)
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
					if (instance.AgentId == null)
					{
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 25:
					instance.InvitationId = binaryReader.ReadUInt64();
					continue;
				case 37:
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

		public static void Serialize(Stream stream, IgnoreInvitationRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			stream.WriteByte(25);
			binaryWriter.Write(instance.InvitationId);
			if (instance.HasProgram)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.Program);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAgentId)
			{
				num++;
				uint serializedSize = AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			num += 8;
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			return num + 1;
		}
	}
}
