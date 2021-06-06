using System.IO;

namespace bnet.protocol.friends.v1
{
	public class AcceptInvitationRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasOptions;

		private AcceptInvitationOptions _Options;

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

		public AcceptInvitationOptions Options
		{
			get
			{
				return _Options;
			}
			set
			{
				_Options = value;
				HasOptions = value != null;
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

		public void SetOptions(AcceptInvitationOptions val)
		{
			Options = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			num ^= InvitationId.GetHashCode();
			if (HasOptions)
			{
				num ^= Options.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AcceptInvitationRequest acceptInvitationRequest = obj as AcceptInvitationRequest;
			if (acceptInvitationRequest == null)
			{
				return false;
			}
			if (HasAgentId != acceptInvitationRequest.HasAgentId || (HasAgentId && !AgentId.Equals(acceptInvitationRequest.AgentId)))
			{
				return false;
			}
			if (!InvitationId.Equals(acceptInvitationRequest.InvitationId))
			{
				return false;
			}
			if (HasOptions != acceptInvitationRequest.HasOptions || (HasOptions && !Options.Equals(acceptInvitationRequest.Options)))
			{
				return false;
			}
			return true;
		}

		public static AcceptInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AcceptInvitationRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AcceptInvitationRequest Deserialize(Stream stream, AcceptInvitationRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AcceptInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			AcceptInvitationRequest acceptInvitationRequest = new AcceptInvitationRequest();
			DeserializeLengthDelimited(stream, acceptInvitationRequest);
			return acceptInvitationRequest;
		}

		public static AcceptInvitationRequest DeserializeLengthDelimited(Stream stream, AcceptInvitationRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AcceptInvitationRequest Deserialize(Stream stream, AcceptInvitationRequest instance, long limit)
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
				case 34:
					if (instance.Options == null)
					{
						instance.Options = AcceptInvitationOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						AcceptInvitationOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		public static void Serialize(Stream stream, AcceptInvitationRequest instance)
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
			if (instance.HasOptions)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				AcceptInvitationOptions.Serialize(stream, instance.Options);
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
			if (HasOptions)
			{
				num++;
				uint serializedSize2 = Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1;
		}
	}
}
