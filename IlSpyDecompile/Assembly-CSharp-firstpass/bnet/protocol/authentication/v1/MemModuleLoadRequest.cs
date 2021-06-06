using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	public class MemModuleLoadRequest : IProtoBuf
	{
		public ContentHandle Handle { get; set; }

		public byte[] Key { get; set; }

		public byte[] Input { get; set; }

		public bool IsInitialized => true;

		public void SetHandle(ContentHandle val)
		{
			Handle = val;
		}

		public void SetKey(byte[] val)
		{
			Key = val;
		}

		public void SetInput(byte[] val)
		{
			Input = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Handle.GetHashCode() ^ Key.GetHashCode() ^ Input.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			MemModuleLoadRequest memModuleLoadRequest = obj as MemModuleLoadRequest;
			if (memModuleLoadRequest == null)
			{
				return false;
			}
			if (!Handle.Equals(memModuleLoadRequest.Handle))
			{
				return false;
			}
			if (!Key.Equals(memModuleLoadRequest.Key))
			{
				return false;
			}
			if (!Input.Equals(memModuleLoadRequest.Input))
			{
				return false;
			}
			return true;
		}

		public static MemModuleLoadRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemModuleLoadRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MemModuleLoadRequest Deserialize(Stream stream, MemModuleLoadRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MemModuleLoadRequest DeserializeLengthDelimited(Stream stream)
		{
			MemModuleLoadRequest memModuleLoadRequest = new MemModuleLoadRequest();
			DeserializeLengthDelimited(stream, memModuleLoadRequest);
			return memModuleLoadRequest;
		}

		public static MemModuleLoadRequest DeserializeLengthDelimited(Stream stream, MemModuleLoadRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MemModuleLoadRequest Deserialize(Stream stream, MemModuleLoadRequest instance, long limit)
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
					if (instance.Handle == null)
					{
						instance.Handle = ContentHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						ContentHandle.DeserializeLengthDelimited(stream, instance.Handle);
					}
					continue;
				case 18:
					instance.Key = ProtocolParser.ReadBytes(stream);
					continue;
				case 26:
					instance.Input = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, MemModuleLoadRequest instance)
		{
			if (instance.Handle == null)
			{
				throw new ArgumentNullException("Handle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Handle.GetSerializedSize());
			ContentHandle.Serialize(stream, instance.Handle);
			if (instance.Key == null)
			{
				throw new ArgumentNullException("Key", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, instance.Key);
			if (instance.Input == null)
			{
				throw new ArgumentNullException("Input", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, instance.Input);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = Handle.GetSerializedSize();
			return (uint)((int)(0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize))) + ((int)ProtocolParser.SizeOfUInt32(Key.Length) + Key.Length) + ((int)ProtocolParser.SizeOfUInt32(Input.Length) + Input.Length) + 3);
		}
	}
}
