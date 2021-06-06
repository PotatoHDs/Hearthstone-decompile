using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class GetChannelInfoRequest : IProtoBuf
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

		public EntityId ChannelId { get; set; }

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetChannelId(EntityId val)
		{
			ChannelId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			return num ^ ChannelId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GetChannelInfoRequest getChannelInfoRequest = obj as GetChannelInfoRequest;
			if (getChannelInfoRequest == null)
			{
				return false;
			}
			if (HasAgentId != getChannelInfoRequest.HasAgentId || (HasAgentId && !AgentId.Equals(getChannelInfoRequest.AgentId)))
			{
				return false;
			}
			if (!ChannelId.Equals(getChannelInfoRequest.ChannelId))
			{
				return false;
			}
			return true;
		}

		public static GetChannelInfoRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetChannelInfoRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetChannelInfoRequest Deserialize(Stream stream, GetChannelInfoRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetChannelInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			GetChannelInfoRequest getChannelInfoRequest = new GetChannelInfoRequest();
			DeserializeLengthDelimited(stream, getChannelInfoRequest);
			return getChannelInfoRequest;
		}

		public static GetChannelInfoRequest DeserializeLengthDelimited(Stream stream, GetChannelInfoRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetChannelInfoRequest Deserialize(Stream stream, GetChannelInfoRequest instance, long limit)
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
					if (instance.ChannelId == null)
					{
						instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
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

		public static void Serialize(Stream stream, GetChannelInfoRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
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
			uint serializedSize2 = ChannelId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			return num + 1;
		}
	}
}
