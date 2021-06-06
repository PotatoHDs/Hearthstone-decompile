using System.IO;
using System.Text;

namespace bnet.protocol
{
	public class EmbedInfo : IProtoBuf
	{
		public bool HasTitle;

		private string _Title;

		public bool HasType;

		private string _Type;

		public bool HasOriginalUrl;

		private string _OriginalUrl;

		public bool HasThumbnail;

		private EmbedImage _Thumbnail;

		public bool HasProvider;

		private Provider _Provider;

		public bool HasDescription;

		private string _Description;

		public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				_Title = value;
				HasTitle = value != null;
			}
		}

		public string Type
		{
			get
			{
				return _Type;
			}
			set
			{
				_Type = value;
				HasType = value != null;
			}
		}

		public string OriginalUrl
		{
			get
			{
				return _OriginalUrl;
			}
			set
			{
				_OriginalUrl = value;
				HasOriginalUrl = value != null;
			}
		}

		public EmbedImage Thumbnail
		{
			get
			{
				return _Thumbnail;
			}
			set
			{
				_Thumbnail = value;
				HasThumbnail = value != null;
			}
		}

		public Provider Provider
		{
			get
			{
				return _Provider;
			}
			set
			{
				_Provider = value;
				HasProvider = value != null;
			}
		}

		public string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				_Description = value;
				HasDescription = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetTitle(string val)
		{
			Title = val;
		}

		public void SetType(string val)
		{
			Type = val;
		}

		public void SetOriginalUrl(string val)
		{
			OriginalUrl = val;
		}

		public void SetThumbnail(EmbedImage val)
		{
			Thumbnail = val;
		}

		public void SetProvider(Provider val)
		{
			Provider = val;
		}

		public void SetDescription(string val)
		{
			Description = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTitle)
			{
				num ^= Title.GetHashCode();
			}
			if (HasType)
			{
				num ^= Type.GetHashCode();
			}
			if (HasOriginalUrl)
			{
				num ^= OriginalUrl.GetHashCode();
			}
			if (HasThumbnail)
			{
				num ^= Thumbnail.GetHashCode();
			}
			if (HasProvider)
			{
				num ^= Provider.GetHashCode();
			}
			if (HasDescription)
			{
				num ^= Description.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			EmbedInfo embedInfo = obj as EmbedInfo;
			if (embedInfo == null)
			{
				return false;
			}
			if (HasTitle != embedInfo.HasTitle || (HasTitle && !Title.Equals(embedInfo.Title)))
			{
				return false;
			}
			if (HasType != embedInfo.HasType || (HasType && !Type.Equals(embedInfo.Type)))
			{
				return false;
			}
			if (HasOriginalUrl != embedInfo.HasOriginalUrl || (HasOriginalUrl && !OriginalUrl.Equals(embedInfo.OriginalUrl)))
			{
				return false;
			}
			if (HasThumbnail != embedInfo.HasThumbnail || (HasThumbnail && !Thumbnail.Equals(embedInfo.Thumbnail)))
			{
				return false;
			}
			if (HasProvider != embedInfo.HasProvider || (HasProvider && !Provider.Equals(embedInfo.Provider)))
			{
				return false;
			}
			if (HasDescription != embedInfo.HasDescription || (HasDescription && !Description.Equals(embedInfo.Description)))
			{
				return false;
			}
			return true;
		}

		public static EmbedInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EmbedInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static EmbedInfo Deserialize(Stream stream, EmbedInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static EmbedInfo DeserializeLengthDelimited(Stream stream)
		{
			EmbedInfo embedInfo = new EmbedInfo();
			DeserializeLengthDelimited(stream, embedInfo);
			return embedInfo;
		}

		public static EmbedInfo DeserializeLengthDelimited(Stream stream, EmbedInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static EmbedInfo Deserialize(Stream stream, EmbedInfo instance, long limit)
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
					instance.Title = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.Type = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.OriginalUrl = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					if (instance.Thumbnail == null)
					{
						instance.Thumbnail = EmbedImage.DeserializeLengthDelimited(stream);
					}
					else
					{
						EmbedImage.DeserializeLengthDelimited(stream, instance.Thumbnail);
					}
					continue;
				case 42:
					if (instance.Provider == null)
					{
						instance.Provider = Provider.DeserializeLengthDelimited(stream);
					}
					else
					{
						Provider.DeserializeLengthDelimited(stream, instance.Provider);
					}
					continue;
				case 50:
					instance.Description = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, EmbedInfo instance)
		{
			if (instance.HasTitle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Title));
			}
			if (instance.HasType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Type));
			}
			if (instance.HasOriginalUrl)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.OriginalUrl));
			}
			if (instance.HasThumbnail)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Thumbnail.GetSerializedSize());
				EmbedImage.Serialize(stream, instance.Thumbnail);
			}
			if (instance.HasProvider)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Provider.GetSerializedSize());
				Provider.Serialize(stream, instance.Provider);
			}
			if (instance.HasDescription)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Description));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasTitle)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Title);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasType)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Type);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasOriginalUrl)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(OriginalUrl);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasThumbnail)
			{
				num++;
				uint serializedSize = Thumbnail.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasProvider)
			{
				num++;
				uint serializedSize2 = Provider.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasDescription)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(Description);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num;
		}
	}
}
