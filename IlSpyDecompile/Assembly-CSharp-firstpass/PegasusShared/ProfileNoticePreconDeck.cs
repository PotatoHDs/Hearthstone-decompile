using System.IO;

namespace PegasusShared
{
	public class ProfileNoticePreconDeck : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 5
		}

		public long Deck { get; set; }

		public int Hero { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Deck.GetHashCode() ^ Hero.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProfileNoticePreconDeck profileNoticePreconDeck = obj as ProfileNoticePreconDeck;
			if (profileNoticePreconDeck == null)
			{
				return false;
			}
			if (!Deck.Equals(profileNoticePreconDeck.Deck))
			{
				return false;
			}
			if (!Hero.Equals(profileNoticePreconDeck.Hero))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticePreconDeck Deserialize(Stream stream, ProfileNoticePreconDeck instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticePreconDeck DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticePreconDeck profileNoticePreconDeck = new ProfileNoticePreconDeck();
			DeserializeLengthDelimited(stream, profileNoticePreconDeck);
			return profileNoticePreconDeck;
		}

		public static ProfileNoticePreconDeck DeserializeLengthDelimited(Stream stream, ProfileNoticePreconDeck instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticePreconDeck Deserialize(Stream stream, ProfileNoticePreconDeck instance, long limit)
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
					instance.Deck = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Hero = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticePreconDeck instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Deck);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Hero);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Deck) + ProtocolParser.SizeOfUInt64((ulong)Hero) + 2;
		}
	}
}
