using System.IO;

namespace PegasusUtil
{
	public class FreeDeckChoice : IProtoBuf
	{
		public enum PacketID
		{
			ID = 333,
			System = 0
		}

		public int ClassId { get; set; }

		public long NoticeId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ClassId.GetHashCode() ^ NoticeId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			FreeDeckChoice freeDeckChoice = obj as FreeDeckChoice;
			if (freeDeckChoice == null)
			{
				return false;
			}
			if (!ClassId.Equals(freeDeckChoice.ClassId))
			{
				return false;
			}
			if (!NoticeId.Equals(freeDeckChoice.NoticeId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FreeDeckChoice Deserialize(Stream stream, FreeDeckChoice instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FreeDeckChoice DeserializeLengthDelimited(Stream stream)
		{
			FreeDeckChoice freeDeckChoice = new FreeDeckChoice();
			DeserializeLengthDelimited(stream, freeDeckChoice);
			return freeDeckChoice;
		}

		public static FreeDeckChoice DeserializeLengthDelimited(Stream stream, FreeDeckChoice instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FreeDeckChoice Deserialize(Stream stream, FreeDeckChoice instance, long limit)
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
					instance.ClassId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.NoticeId = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, FreeDeckChoice instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ClassId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.NoticeId);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)ClassId) + ProtocolParser.SizeOfUInt64((ulong)NoticeId) + 2;
		}
	}
}
