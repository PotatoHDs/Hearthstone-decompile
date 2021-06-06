using System.IO;

namespace PegasusUtil
{
	public class TavernBrawlRequestSessionBegin : IProtoBuf
	{
		public enum PacketID
		{
			ID = 343,
			System = 0
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is TavernBrawlRequestSessionBegin))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TavernBrawlRequestSessionBegin Deserialize(Stream stream, TavernBrawlRequestSessionBegin instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TavernBrawlRequestSessionBegin DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlRequestSessionBegin tavernBrawlRequestSessionBegin = new TavernBrawlRequestSessionBegin();
			DeserializeLengthDelimited(stream, tavernBrawlRequestSessionBegin);
			return tavernBrawlRequestSessionBegin;
		}

		public static TavernBrawlRequestSessionBegin DeserializeLengthDelimited(Stream stream, TavernBrawlRequestSessionBegin instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TavernBrawlRequestSessionBegin Deserialize(Stream stream, TavernBrawlRequestSessionBegin instance, long limit)
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
				if (num == -1)
				{
					if (limit < 0)
					{
						break;
					}
					throw new EndOfStreamException();
				}
				Key key = ProtocolParser.ReadKey((byte)num, stream);
				if (key.Field == 0)
				{
					throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
				}
				ProtocolParser.SkipKey(stream, key);
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, TavernBrawlRequestSessionBegin instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
