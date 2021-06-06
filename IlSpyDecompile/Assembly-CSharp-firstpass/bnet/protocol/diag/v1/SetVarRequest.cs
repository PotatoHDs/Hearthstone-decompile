using System;
using System.IO;
using System.Text;

namespace bnet.protocol.diag.v1
{
	public class SetVarRequest : IProtoBuf
	{
		public string Name { get; set; }

		public string Value { get; set; }

		public bool IsInitialized => true;

		public void SetName(string val)
		{
			Name = val;
		}

		public void SetValue(string val)
		{
			Value = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Name.GetHashCode() ^ Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SetVarRequest setVarRequest = obj as SetVarRequest;
			if (setVarRequest == null)
			{
				return false;
			}
			if (!Name.Equals(setVarRequest.Name))
			{
				return false;
			}
			if (!Value.Equals(setVarRequest.Value))
			{
				return false;
			}
			return true;
		}

		public static SetVarRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SetVarRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SetVarRequest Deserialize(Stream stream, SetVarRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SetVarRequest DeserializeLengthDelimited(Stream stream)
		{
			SetVarRequest setVarRequest = new SetVarRequest();
			DeserializeLengthDelimited(stream, setVarRequest);
			return setVarRequest;
		}

		public static SetVarRequest DeserializeLengthDelimited(Stream stream, SetVarRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SetVarRequest Deserialize(Stream stream, SetVarRequest instance, long limit)
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

		public static void Serialize(Stream stream, SetVarRequest instance)
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
