using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class SendMessageRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasRequiredPrivileges;

		private ulong _RequiredPrivileges;

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

		public Message Message { get; set; }

		public ulong RequiredPrivileges
		{
			get
			{
				return _RequiredPrivileges;
			}
			set
			{
				_RequiredPrivileges = value;
				HasRequiredPrivileges = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetMessage(Message val)
		{
			Message = val;
		}

		public void SetRequiredPrivileges(ulong val)
		{
			RequiredPrivileges = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			num ^= Message.GetHashCode();
			if (HasRequiredPrivileges)
			{
				num ^= RequiredPrivileges.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SendMessageRequest sendMessageRequest = obj as SendMessageRequest;
			if (sendMessageRequest == null)
			{
				return false;
			}
			if (HasAgentId != sendMessageRequest.HasAgentId || (HasAgentId && !AgentId.Equals(sendMessageRequest.AgentId)))
			{
				return false;
			}
			if (!Message.Equals(sendMessageRequest.Message))
			{
				return false;
			}
			if (HasRequiredPrivileges != sendMessageRequest.HasRequiredPrivileges || (HasRequiredPrivileges && !RequiredPrivileges.Equals(sendMessageRequest.RequiredPrivileges)))
			{
				return false;
			}
			return true;
		}

		public static SendMessageRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendMessageRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SendMessageRequest Deserialize(Stream stream, SendMessageRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SendMessageRequest DeserializeLengthDelimited(Stream stream)
		{
			SendMessageRequest sendMessageRequest = new SendMessageRequest();
			DeserializeLengthDelimited(stream, sendMessageRequest);
			return sendMessageRequest;
		}

		public static SendMessageRequest DeserializeLengthDelimited(Stream stream, SendMessageRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SendMessageRequest Deserialize(Stream stream, SendMessageRequest instance, long limit)
		{
			instance.RequiredPrivileges = 0uL;
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
					if (instance.Message == null)
					{
						instance.Message = Message.DeserializeLengthDelimited(stream);
					}
					else
					{
						Message.DeserializeLengthDelimited(stream, instance.Message);
					}
					continue;
				case 24:
					instance.RequiredPrivileges = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SendMessageRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.Message == null)
			{
				throw new ArgumentNullException("Message", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Message.GetSerializedSize());
			Message.Serialize(stream, instance.Message);
			if (instance.HasRequiredPrivileges)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.RequiredPrivileges);
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
			uint serializedSize2 = Message.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (HasRequiredPrivileges)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(RequiredPrivileges);
			}
			return num + 1;
		}
	}
}
