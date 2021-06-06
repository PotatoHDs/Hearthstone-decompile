using System.IO;
using System.Text;

namespace bnet.protocol
{
	public class EmbedImage : IProtoBuf
	{
		public bool HasUrl;

		private string _Url;

		public bool HasWidth;

		private uint _Width;

		public bool HasHeight;

		private uint _Height;

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

		public uint Width
		{
			get
			{
				return _Width;
			}
			set
			{
				_Width = value;
				HasWidth = true;
			}
		}

		public uint Height
		{
			get
			{
				return _Height;
			}
			set
			{
				_Height = value;
				HasHeight = true;
			}
		}

		public bool IsInitialized => true;

		public void SetUrl(string val)
		{
			Url = val;
		}

		public void SetWidth(uint val)
		{
			Width = val;
		}

		public void SetHeight(uint val)
		{
			Height = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasUrl)
			{
				num ^= Url.GetHashCode();
			}
			if (HasWidth)
			{
				num ^= Width.GetHashCode();
			}
			if (HasHeight)
			{
				num ^= Height.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			EmbedImage embedImage = obj as EmbedImage;
			if (embedImage == null)
			{
				return false;
			}
			if (HasUrl != embedImage.HasUrl || (HasUrl && !Url.Equals(embedImage.Url)))
			{
				return false;
			}
			if (HasWidth != embedImage.HasWidth || (HasWidth && !Width.Equals(embedImage.Width)))
			{
				return false;
			}
			if (HasHeight != embedImage.HasHeight || (HasHeight && !Height.Equals(embedImage.Height)))
			{
				return false;
			}
			return true;
		}

		public static EmbedImage ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EmbedImage>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static EmbedImage Deserialize(Stream stream, EmbedImage instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static EmbedImage DeserializeLengthDelimited(Stream stream)
		{
			EmbedImage embedImage = new EmbedImage();
			DeserializeLengthDelimited(stream, embedImage);
			return embedImage;
		}

		public static EmbedImage DeserializeLengthDelimited(Stream stream, EmbedImage instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static EmbedImage Deserialize(Stream stream, EmbedImage instance, long limit)
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
				case 16:
					instance.Width = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.Height = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, EmbedImage instance)
		{
			if (instance.HasUrl)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Url));
			}
			if (instance.HasWidth)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Width);
			}
			if (instance.HasHeight)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Height);
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
			if (HasWidth)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Width);
			}
			if (HasHeight)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Height);
			}
			return num;
		}
	}
}
