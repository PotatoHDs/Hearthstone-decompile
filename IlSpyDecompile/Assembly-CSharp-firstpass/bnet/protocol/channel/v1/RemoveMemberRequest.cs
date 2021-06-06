using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class RemoveMemberRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasReason;

		private uint _Reason;

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

		public EntityId MemberId { get; set; }

		public uint Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetMemberId(EntityId val)
		{
			MemberId = val;
		}

		public void SetReason(uint val)
		{
			Reason = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			num ^= MemberId.GetHashCode();
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RemoveMemberRequest removeMemberRequest = obj as RemoveMemberRequest;
			if (removeMemberRequest == null)
			{
				return false;
			}
			if (HasAgentId != removeMemberRequest.HasAgentId || (HasAgentId && !AgentId.Equals(removeMemberRequest.AgentId)))
			{
				return false;
			}
			if (!MemberId.Equals(removeMemberRequest.MemberId))
			{
				return false;
			}
			if (HasReason != removeMemberRequest.HasReason || (HasReason && !Reason.Equals(removeMemberRequest.Reason)))
			{
				return false;
			}
			return true;
		}

		public static RemoveMemberRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveMemberRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RemoveMemberRequest Deserialize(Stream stream, RemoveMemberRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RemoveMemberRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveMemberRequest removeMemberRequest = new RemoveMemberRequest();
			DeserializeLengthDelimited(stream, removeMemberRequest);
			return removeMemberRequest;
		}

		public static RemoveMemberRequest DeserializeLengthDelimited(Stream stream, RemoveMemberRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RemoveMemberRequest Deserialize(Stream stream, RemoveMemberRequest instance, long limit)
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
					if (instance.MemberId == null)
					{
						instance.MemberId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.MemberId);
					}
					continue;
				case 24:
					instance.Reason = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, RemoveMemberRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.MemberId == null)
			{
				throw new ArgumentNullException("MemberId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
			EntityId.Serialize(stream, instance.MemberId);
			if (instance.HasReason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
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
			uint serializedSize2 = MemberId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Reason);
			}
			return num + 1;
		}
	}
}
