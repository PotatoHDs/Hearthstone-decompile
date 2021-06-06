using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	public class MemModuleLoadResponse : IProtoBuf
	{
		public byte[] Data { get; set; }

		public bool IsInitialized => true;

		public void SetData(byte[] val)
		{
			Data = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Data.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			MemModuleLoadResponse memModuleLoadResponse = obj as MemModuleLoadResponse;
			if (memModuleLoadResponse == null)
			{
				return false;
			}
			if (!Data.Equals(memModuleLoadResponse.Data))
			{
				return false;
			}
			return true;
		}

		public static MemModuleLoadResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemModuleLoadResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MemModuleLoadResponse Deserialize(Stream stream, MemModuleLoadResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MemModuleLoadResponse DeserializeLengthDelimited(Stream stream)
		{
			MemModuleLoadResponse memModuleLoadResponse = new MemModuleLoadResponse();
			DeserializeLengthDelimited(stream, memModuleLoadResponse);
			return memModuleLoadResponse;
		}

		public static MemModuleLoadResponse DeserializeLengthDelimited(Stream stream, MemModuleLoadResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MemModuleLoadResponse Deserialize(Stream stream, MemModuleLoadResponse instance, long limit)
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
					instance.Data = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, MemModuleLoadResponse instance)
		{
			if (instance.Data == null)
			{
				throw new ArgumentNullException("Data", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, instance.Data);
		}

		public uint GetSerializedSize()
		{
			return (uint)(0 + ((int)ProtocolParser.SizeOfUInt32(Data.Length) + Data.Length) + 1);
		}
	}
}
