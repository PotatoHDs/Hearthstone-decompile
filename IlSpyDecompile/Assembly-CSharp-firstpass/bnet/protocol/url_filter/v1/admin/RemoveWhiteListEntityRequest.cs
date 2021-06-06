using System.IO;
using System.Text;

namespace bnet.protocol.url_filter.v1.admin
{
	public class RemoveWhiteListEntityRequest : IProtoBuf
	{
		public bool HasUrl;

		private string _Url;

		public string Url
		{
			get
			{
				return _Url;
			}
			set
			{
				_Url = value;
				HasUrl = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetUrl(string val)
		{
			Url = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasUrl)
			{
				num ^= Url.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RemoveWhiteListEntityRequest removeWhiteListEntityRequest = obj as RemoveWhiteListEntityRequest;
			if (removeWhiteListEntityRequest == null)
			{
				return false;
			}
			if (HasUrl != removeWhiteListEntityRequest.HasUrl || (HasUrl && !Url.Equals(removeWhiteListEntityRequest.Url)))
			{
				return false;
			}
			return true;
		}

		public static RemoveWhiteListEntityRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveWhiteListEntityRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RemoveWhiteListEntityRequest Deserialize(Stream stream, RemoveWhiteListEntityRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RemoveWhiteListEntityRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveWhiteListEntityRequest removeWhiteListEntityRequest = new RemoveWhiteListEntityRequest();
			DeserializeLengthDelimited(stream, removeWhiteListEntityRequest);
			return removeWhiteListEntityRequest;
		}

		public static RemoveWhiteListEntityRequest DeserializeLengthDelimited(Stream stream, RemoveWhiteListEntityRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RemoveWhiteListEntityRequest Deserialize(Stream stream, RemoveWhiteListEntityRequest instance, long limit)
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
					instance.Url = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, RemoveWhiteListEntityRequest instance)
		{
			if (instance.HasUrl)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Url));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasUrl)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Url);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
