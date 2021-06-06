using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.Types;

namespace bnet.protocol.channel.v2
{
	public class SetTypingIndicatorRequest : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasChannelId;

		private ChannelId _ChannelId;

		public bool HasAction;

		private TypingIndicator _Action;

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

		public TypingIndicator Action
		{
			get
			{
				return _Action;
			}
			set
			{
				_Action = value;
				HasAction = true;
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

		public void SetAction(TypingIndicator val)
		{
			Action = val;
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
			if (HasAction)
			{
				num ^= Action.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SetTypingIndicatorRequest setTypingIndicatorRequest = obj as SetTypingIndicatorRequest;
			if (setTypingIndicatorRequest == null)
			{
				return false;
			}
			if (HasAgentId != setTypingIndicatorRequest.HasAgentId || (HasAgentId && !AgentId.Equals(setTypingIndicatorRequest.AgentId)))
			{
				return false;
			}
			if (HasChannelId != setTypingIndicatorRequest.HasChannelId || (HasChannelId && !ChannelId.Equals(setTypingIndicatorRequest.ChannelId)))
			{
				return false;
			}
			if (HasAction != setTypingIndicatorRequest.HasAction || (HasAction && !Action.Equals(setTypingIndicatorRequest.Action)))
			{
				return false;
			}
			return true;
		}

		public static SetTypingIndicatorRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SetTypingIndicatorRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SetTypingIndicatorRequest Deserialize(Stream stream, SetTypingIndicatorRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SetTypingIndicatorRequest DeserializeLengthDelimited(Stream stream)
		{
			SetTypingIndicatorRequest setTypingIndicatorRequest = new SetTypingIndicatorRequest();
			DeserializeLengthDelimited(stream, setTypingIndicatorRequest);
			return setTypingIndicatorRequest;
		}

		public static SetTypingIndicatorRequest DeserializeLengthDelimited(Stream stream, SetTypingIndicatorRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SetTypingIndicatorRequest Deserialize(Stream stream, SetTypingIndicatorRequest instance, long limit)
		{
			instance.Action = TypingIndicator.TYPING_START;
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
				case 24:
					instance.Action = (TypingIndicator)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SetTypingIndicatorRequest instance)
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
			if (instance.HasAction)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Action);
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
			if (HasAction)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Action);
			}
			return num;
		}
	}
}
