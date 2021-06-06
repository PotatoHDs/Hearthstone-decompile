using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class DevBnetIdentify : IProtoBuf
	{
		public enum PacketID
		{
			ID = 259,
			System = 0
		}

		public string Name { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Name.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			DevBnetIdentify devBnetIdentify = obj as DevBnetIdentify;
			if (devBnetIdentify == null)
			{
				return false;
			}
			if (!Name.Equals(devBnetIdentify.Name))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DevBnetIdentify Deserialize(Stream stream, DevBnetIdentify instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DevBnetIdentify DeserializeLengthDelimited(Stream stream)
		{
			DevBnetIdentify devBnetIdentify = new DevBnetIdentify();
			DeserializeLengthDelimited(stream, devBnetIdentify);
			return devBnetIdentify;
		}

		public static DevBnetIdentify DeserializeLengthDelimited(Stream stream, DevBnetIdentify instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DevBnetIdentify Deserialize(Stream stream, DevBnetIdentify instance, long limit)
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

		public static void Serialize(Stream stream, DevBnetIdentify instance)
		{
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
		}

		public uint GetSerializedSize()
		{
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
			return 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 1;
		}
	}
}
