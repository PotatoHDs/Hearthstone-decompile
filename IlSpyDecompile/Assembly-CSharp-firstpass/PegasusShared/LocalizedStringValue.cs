using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	public class LocalizedStringValue : IProtoBuf
	{
		public int Locale { get; set; }

		public string Value { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Locale.GetHashCode() ^ Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			LocalizedStringValue localizedStringValue = obj as LocalizedStringValue;
			if (localizedStringValue == null)
			{
				return false;
			}
			if (!Locale.Equals(localizedStringValue.Locale))
			{
				return false;
			}
			if (!Value.Equals(localizedStringValue.Value))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static LocalizedStringValue Deserialize(Stream stream, LocalizedStringValue instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static LocalizedStringValue DeserializeLengthDelimited(Stream stream)
		{
			LocalizedStringValue localizedStringValue = new LocalizedStringValue();
			DeserializeLengthDelimited(stream, localizedStringValue);
			return localizedStringValue;
		}

		public static LocalizedStringValue DeserializeLengthDelimited(Stream stream, LocalizedStringValue instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static LocalizedStringValue Deserialize(Stream stream, LocalizedStringValue instance, long limit)
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
					instance.Locale = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.Value = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, LocalizedStringValue instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Locale);
			if (instance.Value == null)
			{
				throw new ArgumentNullException("Value", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Value));
		}

		public uint GetSerializedSize()
		{
			uint num = 0 + ProtocolParser.SizeOfUInt64((ulong)Locale);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Value);
			return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 2;
		}
	}
}
