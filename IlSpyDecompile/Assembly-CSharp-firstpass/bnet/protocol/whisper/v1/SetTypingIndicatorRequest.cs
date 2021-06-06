using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.Types;

namespace bnet.protocol.whisper.v1
{
	public class SetTypingIndicatorRequest : IProtoBuf
	{
		public bool HasAgentId;

		private AccountId _AgentId;

		public bool HasTargetId;

		private AccountId _TargetId;

		public bool HasAction;

		private TypingIndicator _Action;

		public AccountId AgentId
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

		public AccountId TargetId
		{
			get
			{
				return _TargetId;
			}
			set
			{
				_TargetId = value;
				HasTargetId = value != null;
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

		public void SetAgentId(AccountId val)
		{
			AgentId = val;
		}

		public void SetTargetId(AccountId val)
		{
			TargetId = val;
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
			if (HasTargetId)
			{
				num ^= TargetId.GetHashCode();
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
			if (HasTargetId != setTypingIndicatorRequest.HasTargetId || (HasTargetId && !TargetId.Equals(setTypingIndicatorRequest.TargetId)))
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
						instance.AgentId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 18:
					if (instance.TargetId == null)
					{
						instance.TargetId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.TargetId);
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
				AccountId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasTargetId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				AccountId.Serialize(stream, instance.TargetId);
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
			if (HasTargetId)
			{
				num++;
				uint serializedSize2 = TargetId.GetSerializedSize();
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
