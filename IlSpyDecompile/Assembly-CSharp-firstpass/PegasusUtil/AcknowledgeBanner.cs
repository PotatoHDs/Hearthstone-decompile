using System.IO;

namespace PegasusUtil
{
	public class AcknowledgeBanner : IProtoBuf
	{
		public enum PacketID
		{
			ID = 309,
			System = 0
		}

		public int Banner { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Banner.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			AcknowledgeBanner acknowledgeBanner = obj as AcknowledgeBanner;
			if (acknowledgeBanner == null)
			{
				return false;
			}
			if (!Banner.Equals(acknowledgeBanner.Banner))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AcknowledgeBanner Deserialize(Stream stream, AcknowledgeBanner instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AcknowledgeBanner DeserializeLengthDelimited(Stream stream)
		{
			AcknowledgeBanner acknowledgeBanner = new AcknowledgeBanner();
			DeserializeLengthDelimited(stream, acknowledgeBanner);
			return acknowledgeBanner;
		}

		public static AcknowledgeBanner DeserializeLengthDelimited(Stream stream, AcknowledgeBanner instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AcknowledgeBanner Deserialize(Stream stream, AcknowledgeBanner instance, long limit)
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
					instance.Banner = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AcknowledgeBanner instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Banner);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Banner) + 1;
		}
	}
}
