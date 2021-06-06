using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	public class WhisperEchoNotification : IProtoBuf
	{
		public bool HasSubscriberId;

		private AccountId _SubscriberId;

		public bool HasWhisper;

		private Whisper _Whisper;

		public AccountId SubscriberId
		{
			get
			{
				return _SubscriberId;
			}
			set
			{
				_SubscriberId = value;
				HasSubscriberId = value != null;
			}
		}

		public Whisper Whisper
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

		public void SetSubscriberId(AccountId val)
		{
			SubscriberId = val;
		}

		public void SetWhisper(Whisper val)
		{
			Whisper = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSubscriberId)
			{
				num ^= SubscriberId.GetHashCode();
			}
			if (HasWhisper)
			{
				num ^= Whisper.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			WhisperEchoNotification whisperEchoNotification = obj as WhisperEchoNotification;
			if (whisperEchoNotification == null)
			{
				return false;
			}
			if (HasSubscriberId != whisperEchoNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(whisperEchoNotification.SubscriberId)))
			{
				return false;
			}
			if (HasWhisper != whisperEchoNotification.HasWhisper || (HasWhisper && !Whisper.Equals(whisperEchoNotification.Whisper)))
			{
				return false;
			}
			return true;
		}

		public static WhisperEchoNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<WhisperEchoNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static WhisperEchoNotification Deserialize(Stream stream, WhisperEchoNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static WhisperEchoNotification DeserializeLengthDelimited(Stream stream)
		{
			WhisperEchoNotification whisperEchoNotification = new WhisperEchoNotification();
			DeserializeLengthDelimited(stream, whisperEchoNotification);
			return whisperEchoNotification;
		}

		public static WhisperEchoNotification DeserializeLengthDelimited(Stream stream, WhisperEchoNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static WhisperEchoNotification Deserialize(Stream stream, WhisperEchoNotification instance, long limit)
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
					if (instance.SubscriberId == null)
					{
						instance.SubscriberId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.SubscriberId);
					}
					continue;
				case 18:
					if (instance.Whisper == null)
					{
						instance.Whisper = Whisper.DeserializeLengthDelimited(stream);
					}
					else
					{
						Whisper.DeserializeLengthDelimited(stream, instance.Whisper);
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

		public static void Serialize(Stream stream, WhisperEchoNotification instance)
		{
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasWhisper)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Whisper.GetSerializedSize());
				Whisper.Serialize(stream, instance.Whisper);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSubscriberId)
			{
				num++;
				uint serializedSize = SubscriberId.GetSerializedSize();
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
