using System;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	public class UnblockPlayerRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

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

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetTargetId(EntityId val)
		{
			TargetId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			return num ^ TargetId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			UnblockPlayerRequest unblockPlayerRequest = obj as UnblockPlayerRequest;
			if (unblockPlayerRequest == null)
			{
				return false;
			}
			if (HasAgentId != unblockPlayerRequest.HasAgentId || (HasAgentId && !AgentId.Equals(unblockPlayerRequest.AgentId)))
			{
				return false;
			}
			if (!TargetId.Equals(unblockPlayerRequest.TargetId))
			{
				return false;
			}
			return true;
		}

		public static UnblockPlayerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnblockPlayerRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UnblockPlayerRequest Deserialize(Stream stream, UnblockPlayerRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UnblockPlayerRequest DeserializeLengthDelimited(Stream stream)
		{
			UnblockPlayerRequest unblockPlayerRequest = new UnblockPlayerRequest();
			DeserializeLengthDelimited(stream, unblockPlayerRequest);
			return unblockPlayerRequest;
		}

		public static UnblockPlayerRequest DeserializeLengthDelimited(Stream stream, UnblockPlayerRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UnblockPlayerRequest Deserialize(Stream stream, UnblockPlayerRequest instance, long limit)
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

		public static void Serialize(Stream stream, UnblockPlayerRequest instance)
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
			return num + 1;
		}
	}
}
