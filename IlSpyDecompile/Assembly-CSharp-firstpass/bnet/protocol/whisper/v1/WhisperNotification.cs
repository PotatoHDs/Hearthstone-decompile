using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	public class WhisperNotification : IProtoBuf
	{
		public bool HasSubscriberId;

		private AccountId _SubscriberId;

		public bool HasWhisper;

		private Whisper _Whisper;

		public bool HasSenderBattleTag;

		private string _SenderBattleTag;

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

		public string SenderBattleTag
		{
			get
			{
				return _SenderBattleTag;
			}
			set
			{
				_SenderBattleTag = value;
				HasSenderBattleTag = value != null;
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

		public void SetSenderBattleTag(string val)
		{
			SenderBattleTag = val;
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
			if (HasSenderBattleTag)
			{
				num ^= SenderBattleTag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			WhisperNotification whisperNotification = obj as WhisperNotification;
			if (whisperNotification == null)
			{
				return false;
			}
			if (HasSubscriberId != whisperNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(whisperNotification.SubscriberId)))
			{
				return false;
			}
			if (HasWhisper != whisperNotification.HasWhisper || (HasWhisper && !Whisper.Equals(whisperNotification.Whisper)))
			{
				return false;
			}
			if (HasSenderBattleTag != whisperNotification.HasSenderBattleTag || (HasSenderBattleTag && !SenderBattleTag.Equals(whisperNotification.SenderBattleTag)))
			{
				return false;
			}
			return true;
		}

		public static WhisperNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<WhisperNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static WhisperNotification Deserialize(Stream stream, WhisperNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static WhisperNotification DeserializeLengthDelimited(Stream stream)
		{
			WhisperNotification whisperNotification = new WhisperNotification();
			DeserializeLengthDelimited(stream, whisperNotification);
			return whisperNotification;
		}

		public static WhisperNotification DeserializeLengthDelimited(Stream stream, WhisperNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static WhisperNotification Deserialize(Stream stream, WhisperNotification instance, long limit)
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
				case 26:
					instance.SenderBattleTag = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, WhisperNotification instance)
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
			if (instance.HasSenderBattleTag)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SenderBattleTag));
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
			if (HasSenderBattleTag)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(SenderBattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
