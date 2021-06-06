using System.IO;

namespace PegasusUtil
{
	public class ProfileDeckLimit : IProtoBuf
	{
		public enum PacketID
		{
			ID = 231
		}

		public int DeckLimit { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ DeckLimit.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProfileDeckLimit profileDeckLimit = obj as ProfileDeckLimit;
			if (profileDeckLimit == null)
			{
				return false;
			}
			if (!DeckLimit.Equals(profileDeckLimit.DeckLimit))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileDeckLimit Deserialize(Stream stream, ProfileDeckLimit instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileDeckLimit DeserializeLengthDelimited(Stream stream)
		{
			ProfileDeckLimit profileDeckLimit = new ProfileDeckLimit();
			DeserializeLengthDelimited(stream, profileDeckLimit);
			return profileDeckLimit;
		}

		public static ProfileDeckLimit DeserializeLengthDelimited(Stream stream, ProfileDeckLimit instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileDeckLimit Deserialize(Stream stream, ProfileDeckLimit instance, long limit)
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
					instance.DeckLimit = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileDeckLimit instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckLimit);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)DeckLimit) + 1;
		}
	}
}
