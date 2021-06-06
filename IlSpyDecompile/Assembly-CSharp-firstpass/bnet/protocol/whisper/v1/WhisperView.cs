using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	public class WhisperView : IProtoBuf
	{
		public bool HasSenderId;

		private AccountId _SenderId;

		public bool HasViewMarker;

		private ViewMarker _ViewMarker;

		public bool HasSenderBattleTag;

		private string _SenderBattleTag;

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

		public ViewMarker ViewMarker
		{
			get
			{
				return _ViewMarker;
			}
			set
			{
				_ViewMarker = value;
				HasViewMarker = value != null;
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

		public void SetSenderId(AccountId val)
		{
			SenderId = val;
		}

		public void SetViewMarker(ViewMarker val)
		{
			ViewMarker = val;
		}

		public void SetSenderBattleTag(string val)
		{
			SenderBattleTag = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSenderId)
			{
				num ^= SenderId.GetHashCode();
			}
			if (HasViewMarker)
			{
				num ^= ViewMarker.GetHashCode();
			}
			if (HasSenderBattleTag)
			{
				num ^= SenderBattleTag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			WhisperView whisperView = obj as WhisperView;
			if (whisperView == null)
			{
				return false;
			}
			if (HasSenderId != whisperView.HasSenderId || (HasSenderId && !SenderId.Equals(whisperView.SenderId)))
			{
				return false;
			}
			if (HasViewMarker != whisperView.HasViewMarker || (HasViewMarker && !ViewMarker.Equals(whisperView.ViewMarker)))
			{
				return false;
			}
			if (HasSenderBattleTag != whisperView.HasSenderBattleTag || (HasSenderBattleTag && !SenderBattleTag.Equals(whisperView.SenderBattleTag)))
			{
				return false;
			}
			return true;
		}

		public static WhisperView ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<WhisperView>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static WhisperView Deserialize(Stream stream, WhisperView instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static WhisperView DeserializeLengthDelimited(Stream stream)
		{
			WhisperView whisperView = new WhisperView();
			DeserializeLengthDelimited(stream, whisperView);
			return whisperView;
		}

		public static WhisperView DeserializeLengthDelimited(Stream stream, WhisperView instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static WhisperView Deserialize(Stream stream, WhisperView instance, long limit)
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
					if (instance.SenderId == null)
					{
						instance.SenderId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.SenderId);
					}
					continue;
				case 18:
					if (instance.ViewMarker == null)
					{
						instance.ViewMarker = ViewMarker.DeserializeLengthDelimited(stream);
					}
					else
					{
						ViewMarker.DeserializeLengthDelimited(stream, instance.ViewMarker);
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

		public static void Serialize(Stream stream, WhisperView instance)
		{
			if (instance.HasSenderId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.SenderId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SenderId);
			}
			if (instance.HasViewMarker)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ViewMarker.GetSerializedSize());
				ViewMarker.Serialize(stream, instance.ViewMarker);
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
			if (HasSenderId)
			{
				num++;
				uint serializedSize = SenderId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasViewMarker)
			{
				num++;
				uint serializedSize2 = ViewMarker.GetSerializedSize();
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
