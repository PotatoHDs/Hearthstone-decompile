using System.IO;
using System.Text;

namespace bnet.protocol.url_filter.v1.admin
{
	public class AddBlackListEntityRequest : IProtoBuf
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
			AddBlackListEntityRequest addBlackListEntityRequest = obj as AddBlackListEntityRequest;
			if (addBlackListEntityRequest == null)
			{
				return false;
			}
			if (HasUrl != addBlackListEntityRequest.HasUrl || (HasUrl && !Url.Equals(addBlackListEntityRequest.Url)))
			{
				return false;
			}
			return true;
		}

		public static AddBlackListEntityRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddBlackListEntityRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AddBlackListEntityRequest Deserialize(Stream stream, AddBlackListEntityRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AddBlackListEntityRequest DeserializeLengthDelimited(Stream stream)
		{
			AddBlackListEntityRequest addBlackListEntityRequest = new AddBlackListEntityRequest();
			DeserializeLengthDelimited(stream, addBlackListEntityRequest);
			return addBlackListEntityRequest;
		}

		public static AddBlackListEntityRequest DeserializeLengthDelimited(Stream stream, AddBlackListEntityRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AddBlackListEntityRequest Deserialize(Stream stream, AddBlackListEntityRequest instance, long limit)
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

		public static void Serialize(Stream stream, AddBlackListEntityRequest instance)
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
