using System.IO;

namespace PegasusUtil
{
	public class AckNotice : IProtoBuf
	{
		public enum PacketID
		{
			ID = 213,
			System = 0
		}

		public long Entry { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Entry.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			AckNotice ackNotice = obj as AckNotice;
			if (ackNotice == null)
			{
				return false;
			}
			if (!Entry.Equals(ackNotice.Entry))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AckNotice Deserialize(Stream stream, AckNotice instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AckNotice DeserializeLengthDelimited(Stream stream)
		{
			AckNotice ackNotice = new AckNotice();
			DeserializeLengthDelimited(stream, ackNotice);
			return ackNotice;
		}

		public static AckNotice DeserializeLengthDelimited(Stream stream, AckNotice instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AckNotice Deserialize(Stream stream, AckNotice instance, long limit)
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
				case 8:
					instance.Entry = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AckNotice instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Entry);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Entry) + 1;
		}
	}
}
