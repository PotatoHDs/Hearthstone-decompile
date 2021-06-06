using System.IO;

namespace bnet.protocol.report.v2
{
	public class ReportItem : IProtoBuf
	{
		public bool HasMessageId;

		private MessageId _MessageId;

		public MessageId MessageId
		{
			get
			{
				return _MessageId;
			}
			set
			{
				_MessageId = value;
				HasMessageId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetMessageId(MessageId val)
		{
			MessageId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMessageId)
			{
				num ^= MessageId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ReportItem reportItem = obj as ReportItem;
			if (reportItem == null)
			{
				return false;
			}
			if (HasMessageId != reportItem.HasMessageId || (HasMessageId && !MessageId.Equals(reportItem.MessageId)))
			{
				return false;
			}
			return true;
		}

		public static ReportItem ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ReportItem>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ReportItem Deserialize(Stream stream, ReportItem instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ReportItem DeserializeLengthDelimited(Stream stream)
		{
			ReportItem reportItem = new ReportItem();
			DeserializeLengthDelimited(stream, reportItem);
			return reportItem;
		}

		public static ReportItem DeserializeLengthDelimited(Stream stream, ReportItem instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ReportItem Deserialize(Stream stream, ReportItem instance, long limit)
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
					if (instance.MessageId == null)
					{
						instance.MessageId = MessageId.DeserializeLengthDelimited(stream);
					}
					else
					{
						MessageId.DeserializeLengthDelimited(stream, instance.MessageId);
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

		public static void Serialize(Stream stream, ReportItem instance)
		{
			if (instance.HasMessageId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.MessageId.GetSerializedSize());
				MessageId.Serialize(stream, instance.MessageId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMessageId)
			{
				num++;
				uint serializedSize = MessageId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
