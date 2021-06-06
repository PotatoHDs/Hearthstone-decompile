using System;
using System.IO;
using System.Text;

namespace bnet.protocol.diag.v1
{
	public class GetVarRequest : IProtoBuf
	{
		public string Name { get; set; }

		public bool IsInitialized => true;

		public void SetName(string val)
		{
			Name = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Name.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GetVarRequest getVarRequest = obj as GetVarRequest;
			if (getVarRequest == null)
			{
				return false;
			}
			if (!Name.Equals(getVarRequest.Name))
			{
				return false;
			}
			return true;
		}

		public static GetVarRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetVarRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetVarRequest Deserialize(Stream stream, GetVarRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetVarRequest DeserializeLengthDelimited(Stream stream)
		{
			GetVarRequest getVarRequest = new GetVarRequest();
			DeserializeLengthDelimited(stream, getVarRequest);
			return getVarRequest;
		}

		public static GetVarRequest DeserializeLengthDelimited(Stream stream, GetVarRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetVarRequest Deserialize(Stream stream, GetVarRequest instance, long limit)
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

		public static void Serialize(Stream stream, GetVarRequest instance)
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
