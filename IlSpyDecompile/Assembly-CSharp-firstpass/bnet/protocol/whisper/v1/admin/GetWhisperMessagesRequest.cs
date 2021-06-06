using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1.admin
{
	public class GetWhisperMessagesRequest : IProtoBuf
	{
		public bool HasReceiverId;

		private AccountId _ReceiverId;

		public bool HasSenderId;

		private AccountId _SenderId;

		public bool HasOptions;

		private GetEventOptions _Options;

		public AccountId ReceiverId
		{
			get
			{
				return _ReceiverId;
			}
			set
			{
				_ReceiverId = value;
				HasReceiverId = value != null;
			}
		}

		public AccountId SenderId
		{
			get
			{
				return _SenderId;
			}
			set
			{
				_SenderId = value;
				HasSenderId = value != null;
			}
		}

		public GetEventOptions Options
		{
			get
			{
				return _Options;
			}
			set
			{
				_Options = value;
				HasOptions = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetReceiverId(AccountId val)
		{
			ReceiverId = val;
		}

		public void SetSenderId(AccountId val)
		{
			SenderId = val;
		}

		public void SetOptions(GetEventOptions val)
		{
			Options = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasReceiverId)
			{
				num ^= ReceiverId.GetHashCode();
			}
			if (HasSenderId)
			{
				num ^= SenderId.GetHashCode();
			}
			if (HasOptions)
			{
				num ^= Options.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetWhisperMessagesRequest getWhisperMessagesRequest = obj as GetWhisperMessagesRequest;
			if (getWhisperMessagesRequest == null)
			{
				return false;
			}
			if (HasReceiverId != getWhisperMessagesRequest.HasReceiverId || (HasReceiverId && !ReceiverId.Equals(getWhisperMessagesRequest.ReceiverId)))
			{
				return false;
			}
			if (HasSenderId != getWhisperMessagesRequest.HasSenderId || (HasSenderId && !SenderId.Equals(getWhisperMessagesRequest.SenderId)))
			{
				return false;
			}
			if (HasOptions != getWhisperMessagesRequest.HasOptions || (HasOptions && !Options.Equals(getWhisperMessagesRequest.Options)))
			{
				return false;
			}
			return true;
		}

		public static GetWhisperMessagesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetWhisperMessagesRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetWhisperMessagesRequest Deserialize(Stream stream, GetWhisperMessagesRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetWhisperMessagesRequest DeserializeLengthDelimited(Stream stream)
		{
			GetWhisperMessagesRequest getWhisperMessagesRequest = new GetWhisperMessagesRequest();
			DeserializeLengthDelimited(stream, getWhisperMessagesRequest);
			return getWhisperMessagesRequest;
		}

		public static GetWhisperMessagesRequest DeserializeLengthDelimited(Stream stream, GetWhisperMessagesRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetWhisperMessagesRequest Deserialize(Stream stream, GetWhisperMessagesRequest instance, long limit)
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
					if (instance.ReceiverId == null)
					{
						instance.ReceiverId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.ReceiverId);
					}
					continue;
				case 18:
					if (instance.SenderId == null)
					{
						instance.SenderId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.SenderId);
					}
					continue;
				case 26:
					if (instance.Options == null)
					{
						instance.Options = GetEventOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						GetEventOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		public static void Serialize(Stream stream, GetWhisperMessagesRequest instance)
		{
			if (instance.HasReceiverId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ReceiverId.GetSerializedSize());
				AccountId.Serialize(stream, instance.ReceiverId);
			}
			if (instance.HasSenderId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SenderId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SenderId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				GetEventOptions.Serialize(stream, instance.Options);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasReceiverId)
			{
				num++;
				uint serializedSize = ReceiverId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasSenderId)
			{
				num++;
				uint serializedSize2 = SenderId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasOptions)
			{
				num++;
				uint serializedSize3 = Options.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
