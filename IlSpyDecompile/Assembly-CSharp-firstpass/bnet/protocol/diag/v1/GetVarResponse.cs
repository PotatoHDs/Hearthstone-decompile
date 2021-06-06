using System;
using System.IO;
using System.Text;

namespace bnet.protocol.diag.v1
{
	public class GetVarResponse : IProtoBuf
	{
		public string Value { get; set; }

		public bool IsInitialized => true;

		public void SetValue(string val)
		{
			Value = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GetVarResponse getVarResponse = obj as GetVarResponse;
			if (getVarResponse == null)
			{
				return false;
			}
			if (!Value.Equals(getVarResponse.Value))
			{
				return false;
			}
			return true;
		}

		public static GetVarResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetVarResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetVarResponse Deserialize(Stream stream, GetVarResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetVarResponse DeserializeLengthDelimited(Stream stream)
		{
			GetVarResponse getVarResponse = new GetVarResponse();
			DeserializeLengthDelimited(stream, getVarResponse);
			return getVarResponse;
		}

		public static GetVarResponse DeserializeLengthDelimited(Stream stream, GetVarResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetVarResponse Deserialize(Stream stream, GetVarResponse instance, long limit)
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

		public static void Serialize(Stream stream, GetVarResponse instance)
		{
			if (instance.Value == null)
			{
				throw new ArgumentNullException("Value", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Value));
		}

		public uint GetSerializedSize()
		{
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Value);
			return 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 1;
		}
	}
}
