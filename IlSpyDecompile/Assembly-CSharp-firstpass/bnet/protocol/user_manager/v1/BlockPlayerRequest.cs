using System;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	public class BlockPlayerRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasRole;

		private uint _Role;

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

		public EntityId TargetId { get; set; }

		public uint Role
		{
			get
			{
				return _Role;
			}
			set
			{
				_Role = value;
				HasRole = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetTargetId(EntityId val)
		{
			TargetId = val;
		}

		public void SetRole(uint val)
		{
			Role = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			num ^= TargetId.GetHashCode();
			if (HasRole)
			{
				num ^= Role.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BlockPlayerRequest blockPlayerRequest = obj as BlockPlayerRequest;
			if (blockPlayerRequest == null)
			{
				return false;
			}
			if (HasAgentId != blockPlayerRequest.HasAgentId || (HasAgentId && !AgentId.Equals(blockPlayerRequest.AgentId)))
			{
				return false;
			}
			if (!TargetId.Equals(blockPlayerRequest.TargetId))
			{
				return false;
			}
			if (HasRole != blockPlayerRequest.HasRole || (HasRole && !Role.Equals(blockPlayerRequest.Role)))
			{
				return false;
			}
			return true;
		}

		public static BlockPlayerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BlockPlayerRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BlockPlayerRequest Deserialize(Stream stream, BlockPlayerRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BlockPlayerRequest DeserializeLengthDelimited(Stream stream)
		{
			BlockPlayerRequest blockPlayerRequest = new BlockPlayerRequest();
			DeserializeLengthDelimited(stream, blockPlayerRequest);
			return blockPlayerRequest;
		}

		public static BlockPlayerRequest DeserializeLengthDelimited(Stream stream, BlockPlayerRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BlockPlayerRequest Deserialize(Stream stream, BlockPlayerRequest instance, long limit)
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
					if (instance.AgentId == null)
					{
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 18:
					if (instance.TargetId == null)
					{
						instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
					}
					continue;
				case 24:
					instance.Role = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, BlockPlayerRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.TargetId == null)
			{
				throw new ArgumentNullException("TargetId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
			EntityId.Serialize(stream, instance.TargetId);
			if (instance.HasRole)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Role);
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
			uint serializedSize2 = TargetId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (HasRole)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Role);
			}
			return num + 1;
		}
	}
}
