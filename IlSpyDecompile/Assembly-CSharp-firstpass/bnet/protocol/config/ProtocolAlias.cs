using System;
using System.IO;
using System.Text;

namespace bnet.protocol.config
{
	public class ProtocolAlias : IProtoBuf
	{
		public string ServerServiceName { get; set; }

		public string ClientServiceName { get; set; }

		public bool IsInitialized => true;

		public void SetServerServiceName(string val)
		{
			ServerServiceName = val;
		}

		public void SetClientServiceName(string val)
		{
			ClientServiceName = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ServerServiceName.GetHashCode() ^ ClientServiceName.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProtocolAlias protocolAlias = obj as ProtocolAlias;
			if (protocolAlias == null)
			{
				return false;
			}
			if (!ServerServiceName.Equals(protocolAlias.ServerServiceName))
			{
				return false;
			}
			if (!ClientServiceName.Equals(protocolAlias.ClientServiceName))
			{
				return false;
			}
			return true;
		}

		public static ProtocolAlias ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ProtocolAlias>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProtocolAlias Deserialize(Stream stream, ProtocolAlias instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProtocolAlias DeserializeLengthDelimited(Stream stream)
		{
			ProtocolAlias protocolAlias = new ProtocolAlias();
			DeserializeLengthDelimited(stream, protocolAlias);
			return protocolAlias;
		}

		public static ProtocolAlias DeserializeLengthDelimited(Stream stream, ProtocolAlias instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProtocolAlias Deserialize(Stream stream, ProtocolAlias instance, long limit)
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
					instance.ServerServiceName = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.ClientServiceName = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ProtocolAlias instance)
		{
			if (instance.ServerServiceName == null)
			{
				throw new ArgumentNullException("ServerServiceName", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ServerServiceName));
			if (instance.ClientServiceName == null)
			{
				throw new ArgumentNullException("ClientServiceName", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientServiceName));
		}

		public uint GetSerializedSize()
		{
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(ServerServiceName);
			uint num = 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount);
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ClientServiceName);
			return num + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2) + 2;
		}
	}
}
