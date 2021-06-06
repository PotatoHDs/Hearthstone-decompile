using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	public class SendOptions : IProtoBuf
	{
		public bool HasTargetId;

		private AccountId _TargetId;

		public bool HasContent;

		private string _Content;

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

		public string Content
		{
			get
			{
				return _Content;
			}
			set
			{
				_Content = value;
				HasContent = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetTargetId(AccountId val)
		{
			TargetId = val;
		}

		public void SetContent(string val)
		{
			Content = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTargetId)
			{
				num ^= TargetId.GetHashCode();
			}
			if (HasContent)
			{
				num ^= Content.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SendOptions sendOptions = obj as SendOptions;
			if (sendOptions == null)
			{
				return false;
			}
			if (HasTargetId != sendOptions.HasTargetId || (HasTargetId && !TargetId.Equals(sendOptions.TargetId)))
			{
				return false;
			}
			if (HasContent != sendOptions.HasContent || (HasContent && !Content.Equals(sendOptions.Content)))
			{
				return false;
			}
			return true;
		}

		public static SendOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SendOptions Deserialize(Stream stream, SendOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SendOptions DeserializeLengthDelimited(Stream stream)
		{
			SendOptions sendOptions = new SendOptions();
			DeserializeLengthDelimited(stream, sendOptions);
			return sendOptions;
		}

		public static SendOptions DeserializeLengthDelimited(Stream stream, SendOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SendOptions Deserialize(Stream stream, SendOptions instance, long limit)
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
					if (instance.TargetId == null)
					{
						instance.TargetId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.TargetId);
					}
					continue;
				case 18:
					instance.Content = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, SendOptions instance)
		{
			if (instance.HasTargetId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				AccountId.Serialize(stream, instance.TargetId);
			}
			if (instance.HasContent)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Content));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasTargetId)
			{
				num++;
				uint serializedSize = TargetId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasContent)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Content);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
