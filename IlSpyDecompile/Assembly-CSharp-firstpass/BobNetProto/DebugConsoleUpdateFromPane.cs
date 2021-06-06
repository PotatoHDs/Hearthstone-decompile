using System;
using System.IO;
using System.Text;

namespace BobNetProto
{
	public class DebugConsoleUpdateFromPane : IProtoBuf
	{
		public enum PacketID
		{
			ID = 145
		}

		public string Name { get; set; }

		public string Value { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Name.GetHashCode() ^ Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			DebugConsoleUpdateFromPane debugConsoleUpdateFromPane = obj as DebugConsoleUpdateFromPane;
			if (debugConsoleUpdateFromPane == null)
			{
				return false;
			}
			if (!Name.Equals(debugConsoleUpdateFromPane.Name))
			{
				return false;
			}
			if (!Value.Equals(debugConsoleUpdateFromPane.Value))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DebugConsoleUpdateFromPane Deserialize(Stream stream, DebugConsoleUpdateFromPane instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DebugConsoleUpdateFromPane DeserializeLengthDelimited(Stream stream)
		{
			DebugConsoleUpdateFromPane debugConsoleUpdateFromPane = new DebugConsoleUpdateFromPane();
			DeserializeLengthDelimited(stream, debugConsoleUpdateFromPane);
			return debugConsoleUpdateFromPane;
		}

		public static DebugConsoleUpdateFromPane DeserializeLengthDelimited(Stream stream, DebugConsoleUpdateFromPane instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DebugConsoleUpdateFromPane Deserialize(Stream stream, DebugConsoleUpdateFromPane instance, long limit)
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
					instance.Name = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, DebugConsoleUpdateFromPane instance)
		{
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			if (instance.Value == null)
			{
				throw new ArgumentNullException("Value", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Value));
		}

		public uint GetSerializedSize()
		{
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
			uint num = 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount);
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Value);
			return num + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2) + 2;
		}
	}
}
