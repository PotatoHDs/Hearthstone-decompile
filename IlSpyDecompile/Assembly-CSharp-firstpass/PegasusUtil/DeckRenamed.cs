using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class DeckRenamed : IProtoBuf
	{
		public enum PacketID
		{
			ID = 219
		}

		public long Deck { get; set; }

		public string Name { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Deck.GetHashCode() ^ Name.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			DeckRenamed deckRenamed = obj as DeckRenamed;
			if (deckRenamed == null)
			{
				return false;
			}
			if (!Deck.Equals(deckRenamed.Deck))
			{
				return false;
			}
			if (!Name.Equals(deckRenamed.Name))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeckRenamed Deserialize(Stream stream, DeckRenamed instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckRenamed DeserializeLengthDelimited(Stream stream)
		{
			DeckRenamed deckRenamed = new DeckRenamed();
			DeserializeLengthDelimited(stream, deckRenamed);
			return deckRenamed;
		}

		public static DeckRenamed DeserializeLengthDelimited(Stream stream, DeckRenamed instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckRenamed Deserialize(Stream stream, DeckRenamed instance, long limit)
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
				case 18:
					instance.Name = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, DeckRenamed instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Deck);
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
		}

		public uint GetSerializedSize()
		{
			uint num = 0 + ProtocolParser.SizeOfUInt64((ulong)Deck);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
			return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 2;
		}
	}
}
