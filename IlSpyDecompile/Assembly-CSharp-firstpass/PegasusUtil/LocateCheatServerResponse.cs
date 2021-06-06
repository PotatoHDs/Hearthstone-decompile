using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class LocateCheatServerResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 362
		}

		public string Address { get; set; }

		public int Port { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Address.GetHashCode() ^ Port.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			LocateCheatServerResponse locateCheatServerResponse = obj as LocateCheatServerResponse;
			if (locateCheatServerResponse == null)
			{
				return false;
			}
			if (!Address.Equals(locateCheatServerResponse.Address))
			{
				return false;
			}
			if (!Port.Equals(locateCheatServerResponse.Port))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static LocateCheatServerResponse Deserialize(Stream stream, LocateCheatServerResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static LocateCheatServerResponse DeserializeLengthDelimited(Stream stream)
		{
			LocateCheatServerResponse locateCheatServerResponse = new LocateCheatServerResponse();
			DeserializeLengthDelimited(stream, locateCheatServerResponse);
			return locateCheatServerResponse;
		}

		public static LocateCheatServerResponse DeserializeLengthDelimited(Stream stream, LocateCheatServerResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static LocateCheatServerResponse Deserialize(Stream stream, LocateCheatServerResponse instance, long limit)
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
					instance.Address = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.Port = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, LocateCheatServerResponse instance)
		{
			if (instance.Address == null)
			{
				throw new ArgumentNullException("Address", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Address));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Port);
		}

		public uint GetSerializedSize()
		{
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Address);
			return 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + ProtocolParser.SizeOfUInt64((ulong)Port) + 2;
		}
	}
}
