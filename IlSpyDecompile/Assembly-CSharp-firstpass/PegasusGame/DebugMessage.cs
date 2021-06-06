using System;
using System.IO;
using System.Text;

namespace PegasusGame
{
	public class DebugMessage : IProtoBuf
	{
		public enum PacketID
		{
			ID = 5
		}

		public string Message { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Message.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			DebugMessage debugMessage = obj as DebugMessage;
			if (debugMessage == null)
			{
				return false;
			}
			if (!Message.Equals(debugMessage.Message))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DebugMessage Deserialize(Stream stream, DebugMessage instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DebugMessage DeserializeLengthDelimited(Stream stream)
		{
			DebugMessage debugMessage = new DebugMessage();
			DeserializeLengthDelimited(stream, debugMessage);
			return debugMessage;
		}

		public static DebugMessage DeserializeLengthDelimited(Stream stream, DebugMessage instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DebugMessage Deserialize(Stream stream, DebugMessage instance, long limit)
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
					instance.Message = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, DebugMessage instance)
		{
			if (instance.Message == null)
			{
				throw new ArgumentNullException("Message", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Message));
		}

		public uint GetSerializedSize()
		{
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Message);
			return 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 1;
		}
	}
}
