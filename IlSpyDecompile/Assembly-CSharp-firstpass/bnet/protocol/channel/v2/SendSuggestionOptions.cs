using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	public class SendSuggestionOptions : IProtoBuf
	{
		public bool HasChannelId;

		private ChannelId _ChannelId;

		public bool HasTargetId;

		private GameAccountHandle _TargetId;

		public bool HasApprovalId;

		private GameAccountHandle _ApprovalId;

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

		public GameAccountHandle TargetId
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

		public GameAccountHandle ApprovalId
		{
			get
			{
				return _ApprovalId;
			}
			set
			{
				_ApprovalId = value;
				HasApprovalId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetChannelId(ChannelId val)
		{
			ChannelId = val;
		}

		public void SetTargetId(GameAccountHandle val)
		{
			TargetId = val;
		}

		public void SetApprovalId(GameAccountHandle val)
		{
			ApprovalId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			if (HasTargetId)
			{
				num ^= TargetId.GetHashCode();
			}
			if (HasApprovalId)
			{
				num ^= ApprovalId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SendSuggestionOptions sendSuggestionOptions = obj as SendSuggestionOptions;
			if (sendSuggestionOptions == null)
			{
				return false;
			}
			if (HasChannelId != sendSuggestionOptions.HasChannelId || (HasChannelId && !ChannelId.Equals(sendSuggestionOptions.ChannelId)))
			{
				return false;
			}
			if (HasTargetId != sendSuggestionOptions.HasTargetId || (HasTargetId && !TargetId.Equals(sendSuggestionOptions.TargetId)))
			{
				return false;
			}
			if (HasApprovalId != sendSuggestionOptions.HasApprovalId || (HasApprovalId && !ApprovalId.Equals(sendSuggestionOptions.ApprovalId)))
			{
				return false;
			}
			return true;
		}

		public static SendSuggestionOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendSuggestionOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SendSuggestionOptions Deserialize(Stream stream, SendSuggestionOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SendSuggestionOptions DeserializeLengthDelimited(Stream stream)
		{
			SendSuggestionOptions sendSuggestionOptions = new SendSuggestionOptions();
			DeserializeLengthDelimited(stream, sendSuggestionOptions);
			return sendSuggestionOptions;
		}

		public static SendSuggestionOptions DeserializeLengthDelimited(Stream stream, SendSuggestionOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SendSuggestionOptions Deserialize(Stream stream, SendSuggestionOptions instance, long limit)
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
					if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 18:
					if (instance.TargetId == null)
					{
						instance.TargetId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.TargetId);
					}
					continue;
				case 26:
					if (instance.ApprovalId == null)
					{
						instance.ApprovalId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.ApprovalId);
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

		public static void Serialize(Stream stream, SendSuggestionOptions instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasTargetId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.TargetId);
			}
			if (instance.HasApprovalId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ApprovalId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.ApprovalId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasChannelId)
			{
				num++;
				uint serializedSize = ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasTargetId)
			{
				num++;
				uint serializedSize2 = TargetId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasApprovalId)
			{
				num++;
				uint serializedSize3 = ApprovalId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
