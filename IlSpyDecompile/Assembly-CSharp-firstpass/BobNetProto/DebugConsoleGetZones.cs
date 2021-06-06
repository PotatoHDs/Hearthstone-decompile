using System.IO;

namespace BobNetProto
{
	public class DebugConsoleGetZones : IProtoBuf
	{
		public enum PacketID
		{
			ID = 147
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is DebugConsoleGetZones))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DebugConsoleGetZones Deserialize(Stream stream, DebugConsoleGetZones instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DebugConsoleGetZones DeserializeLengthDelimited(Stream stream)
		{
			DebugConsoleGetZones debugConsoleGetZones = new DebugConsoleGetZones();
			DeserializeLengthDelimited(stream, debugConsoleGetZones);
			return debugConsoleGetZones;
		}

		public static DebugConsoleGetZones DeserializeLengthDelimited(Stream stream, DebugConsoleGetZones instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DebugConsoleGetZones Deserialize(Stream stream, DebugConsoleGetZones instance, long limit)
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
				if (num == -1)
				{
					if (limit < 0)
					{
						break;
					}
					throw new EndOfStreamException();
				}
				Key key = ProtocolParser.ReadKey((byte)num, stream);
				if (key.Field == 0)
				{
					throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
				}
				ProtocolParser.SkipKey(stream, key);
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, DebugConsoleGetZones instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
