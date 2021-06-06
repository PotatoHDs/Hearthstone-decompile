using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	public class SendWhisperRequest : IProtoBuf
	{
		public bool HasAgentId;

		private AccountId _AgentId;

		public bool HasWhisper;

		private SendOptions _Whisper;

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

		public SendOptions Whisper
		{
			get
			{
				return _Whisper;
			}
			set
			{
				_Whisper = value;
				HasWhisper = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentId(AccountId val)
		{
			AgentId = val;
		}

		public void SetWhisper(SendOptions val)
		{
			Whisper = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			if (HasWhisper)
			{
				num ^= Whisper.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SendWhisperRequest sendWhisperRequest = obj as SendWhisperRequest;
			if (sendWhisperRequest == null)
			{
				return false;
			}
			if (HasAgentId != sendWhisperRequest.HasAgentId || (HasAgentId && !AgentId.Equals(sendWhisperRequest.AgentId)))
			{
				return false;
			}
			if (HasWhisper != sendWhisperRequest.HasWhisper || (HasWhisper && !Whisper.Equals(sendWhisperRequest.Whisper)))
			{
				return false;
			}
			return true;
		}

		public static SendWhisperRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendWhisperRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SendWhisperRequest Deserialize(Stream stream, SendWhisperRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SendWhisperRequest DeserializeLengthDelimited(Stream stream)
		{
			SendWhisperRequest sendWhisperRequest = new SendWhisperRequest();
			DeserializeLengthDelimited(stream, sendWhisperRequest);
			return sendWhisperRequest;
		}

		public static SendWhisperRequest DeserializeLengthDelimited(Stream stream, SendWhisperRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SendWhisperRequest Deserialize(Stream stream, SendWhisperRequest instance, long limit)
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
						instance.AgentId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 18:
					if (instance.Whisper == null)
					{
						instance.Whisper = SendOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						SendOptions.DeserializeLengthDelimited(stream, instance.Whisper);
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

		public static void Serialize(Stream stream, SendWhisperRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				AccountId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasWhisper)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Whisper.GetSerializedSize());
				SendOptions.Serialize(stream, instance.Whisper);
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
			if (HasWhisper)
			{
				num++;
				uint serializedSize2 = Whisper.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
