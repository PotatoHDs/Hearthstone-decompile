using System.IO;

namespace PegasusGame
{
	public class Tag : IProtoBuf
	{
		public int Name { get; set; }

		public int Value { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Name.GetHashCode() ^ Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Tag tag = obj as Tag;
			if (tag == null)
			{
				return false;
			}
			if (!Name.Equals(tag.Name))
			{
				return false;
			}
			if (!Value.Equals(tag.Value))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Tag Deserialize(Stream stream, Tag instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Tag DeserializeLengthDelimited(Stream stream)
		{
			Tag tag = new Tag();
			DeserializeLengthDelimited(stream, tag);
			return tag;
		}

		public static Tag DeserializeLengthDelimited(Stream stream, Tag instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Tag Deserialize(Stream stream, Tag instance, long limit)
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
					instance.Name = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Value = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, Tag instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Name);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Value);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Name) + ProtocolParser.SizeOfUInt64((ulong)Value) + 2;
		}
	}
}
