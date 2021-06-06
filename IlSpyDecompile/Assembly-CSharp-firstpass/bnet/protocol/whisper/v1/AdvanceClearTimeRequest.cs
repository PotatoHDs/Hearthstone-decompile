using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	public class AdvanceClearTimeRequest : IProtoBuf
	{
		public bool HasAgentId;

		private AccountId _AgentId;

		public bool HasTargetId;

		private AccountId _TargetId;

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

		public bool IsInitialized => true;

		public void SetAgentId(AccountId val)
		{
			AgentId = val;
		}

		public void SetTargetId(AccountId val)
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
			if (HasTargetId)
			{
				num ^= TargetId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AdvanceClearTimeRequest advanceClearTimeRequest = obj as AdvanceClearTimeRequest;
			if (advanceClearTimeRequest == null)
			{
				return false;
			}
			if (HasAgentId != advanceClearTimeRequest.HasAgentId || (HasAgentId && !AgentId.Equals(advanceClearTimeRequest.AgentId)))
			{
				return false;
			}
			if (HasTargetId != advanceClearTimeRequest.HasTargetId || (HasTargetId && !TargetId.Equals(advanceClearTimeRequest.TargetId)))
			{
				return false;
			}
			return true;
		}

		public static AdvanceClearTimeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AdvanceClearTimeRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AdvanceClearTimeRequest Deserialize(Stream stream, AdvanceClearTimeRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AdvanceClearTimeRequest DeserializeLengthDelimited(Stream stream)
		{
			AdvanceClearTimeRequest advanceClearTimeRequest = new AdvanceClearTimeRequest();
			DeserializeLengthDelimited(stream, advanceClearTimeRequest);
			return advanceClearTimeRequest;
		}

		public static AdvanceClearTimeRequest DeserializeLengthDelimited(Stream stream, AdvanceClearTimeRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AdvanceClearTimeRequest Deserialize(Stream stream, AdvanceClearTimeRequest instance, long limit)
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
					if (instance.TargetId == null)
					{
						instance.TargetId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.TargetId);
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

		public static void Serialize(Stream stream, AdvanceClearTimeRequest instance)
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
			return num;
		}
	}
}
