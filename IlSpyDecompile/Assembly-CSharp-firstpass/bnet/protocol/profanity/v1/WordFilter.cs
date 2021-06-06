using System;
using System.IO;
using System.Text;

namespace bnet.protocol.profanity.v1
{
	public class WordFilter : IProtoBuf
	{
		public string Type { get; set; }

		public string Regex { get; set; }

		public bool IsInitialized => true;

		public void SetType(string val)
		{
			Type = val;
		}

		public void SetRegex(string val)
		{
			Regex = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Type.GetHashCode() ^ Regex.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			WordFilter wordFilter = obj as WordFilter;
			if (wordFilter == null)
			{
				return false;
			}
			if (!Type.Equals(wordFilter.Type))
			{
				return false;
			}
			if (!Regex.Equals(wordFilter.Regex))
			{
				return false;
			}
			return true;
		}

		public static WordFilter ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<WordFilter>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static WordFilter Deserialize(Stream stream, WordFilter instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static WordFilter DeserializeLengthDelimited(Stream stream)
		{
			WordFilter wordFilter = new WordFilter();
			DeserializeLengthDelimited(stream, wordFilter);
			return wordFilter;
		}

		public static WordFilter DeserializeLengthDelimited(Stream stream, WordFilter instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static WordFilter Deserialize(Stream stream, WordFilter instance, long limit)
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
					instance.Type = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.Regex = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, WordFilter instance)
		{
			if (instance.Type == null)
			{
				throw new ArgumentNullException("Type", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Type));
			if (instance.Regex == null)
			{
				throw new ArgumentNullException("Regex", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Regex));
		}

		public uint GetSerializedSize()
		{
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Type);
			uint num = 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount);
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Regex);
			return num + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2) + 2;
		}
	}
}
