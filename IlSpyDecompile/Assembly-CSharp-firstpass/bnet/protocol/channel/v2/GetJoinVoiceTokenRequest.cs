using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	public class GetJoinVoiceTokenRequest : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasChannelId;

		private ChannelId _ChannelId;

		public GameAccountHandle AgentId
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

		public ChannelId ChannelId
		{
			get
			{
				return _ChannelId;
			}
			set
			{
				_ChannelId = value;
				HasChannelId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentId(GameAccountHandle val)
		{
			AgentId = val;
		}

		public void SetChannelId(ChannelId val)
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
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetJoinVoiceTokenRequest getJoinVoiceTokenRequest = obj as GetJoinVoiceTokenRequest;
			if (getJoinVoiceTokenRequest == null)
			{
				return false;
			}
			if (HasAgentId != getJoinVoiceTokenRequest.HasAgentId || (HasAgentId && !AgentId.Equals(getJoinVoiceTokenRequest.AgentId)))
			{
				return false;
			}
			if (HasChannelId != getJoinVoiceTokenRequest.HasChannelId || (HasChannelId && !ChannelId.Equals(getJoinVoiceTokenRequest.ChannelId)))
			{
				return false;
			}
			return true;
		}

		public static GetJoinVoiceTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetJoinVoiceTokenRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetJoinVoiceTokenRequest Deserialize(Stream stream, GetJoinVoiceTokenRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetJoinVoiceTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			GetJoinVoiceTokenRequest getJoinVoiceTokenRequest = new GetJoinVoiceTokenRequest();
			DeserializeLengthDelimited(stream, getJoinVoiceTokenRequest);
			return getJoinVoiceTokenRequest;
		}

		public static GetJoinVoiceTokenRequest DeserializeLengthDelimited(Stream stream, GetJoinVoiceTokenRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetJoinVoiceTokenRequest Deserialize(Stream stream, GetJoinVoiceTokenRequest instance, long limit)
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
						instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 18:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
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

		public static void Serialize(Stream stream, GetJoinVoiceTokenRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
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
			if (HasChannelId)
			{
				num++;
				uint serializedSize2 = ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
