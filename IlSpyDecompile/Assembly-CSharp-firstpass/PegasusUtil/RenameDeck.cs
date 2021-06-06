using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class RenameDeck : IProtoBuf
	{
		public enum PacketID
		{
			ID = 211,
			System = 0
		}

		public long Deck { get; set; }

		public string Name { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Deck.GetHashCode() ^ Name.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			RenameDeck renameDeck = obj as RenameDeck;
			if (renameDeck == null)
			{
				return false;
			}
			if (!Deck.Equals(renameDeck.Deck))
			{
				return false;
			}
			if (!Name.Equals(renameDeck.Name))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RenameDeck Deserialize(Stream stream, RenameDeck instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RenameDeck DeserializeLengthDelimited(Stream stream)
		{
			RenameDeck renameDeck = new RenameDeck();
			DeserializeLengthDelimited(stream, renameDeck);
			return renameDeck;
		}

		public static RenameDeck DeserializeLengthDelimited(Stream stream, RenameDeck instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RenameDeck Deserialize(Stream stream, RenameDeck instance, long limit)
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

		public static void Serialize(Stream stream, RenameDeck instance)
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
