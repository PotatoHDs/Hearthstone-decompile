using System;
using System.IO;
using System.Text;

namespace BobNetProto
{
	public class DebugConsoleCommand : IProtoBuf
	{
		public enum PacketID
		{
			ID = 123
		}

		public string Command { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Command.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			DebugConsoleCommand debugConsoleCommand = obj as DebugConsoleCommand;
			if (debugConsoleCommand == null)
			{
				return false;
			}
			if (!Command.Equals(debugConsoleCommand.Command))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DebugConsoleCommand Deserialize(Stream stream, DebugConsoleCommand instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DebugConsoleCommand DeserializeLengthDelimited(Stream stream)
		{
			DebugConsoleCommand debugConsoleCommand = new DebugConsoleCommand();
			DeserializeLengthDelimited(stream, debugConsoleCommand);
			return debugConsoleCommand;
		}

		public static DebugConsoleCommand DeserializeLengthDelimited(Stream stream, DebugConsoleCommand instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DebugConsoleCommand Deserialize(Stream stream, DebugConsoleCommand instance, long limit)
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
					instance.Command = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, DebugConsoleCommand instance)
		{
			if (instance.Command == null)
			{
				throw new ArgumentNullException("Command", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Command));
		}

		public uint GetSerializedSize()
		{
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Command);
			return 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 1;
		}
	}
}
