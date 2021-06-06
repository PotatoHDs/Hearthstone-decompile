using System.IO;
using System.Text;

namespace bnet.protocol.sns.v1
{
	public class GetFacebookSettingsResponse : IProtoBuf
	{
		public bool HasAppId;

		private string _AppId;

		public bool HasRedirectUri;

		private string _RedirectUri;

		public bool HasApiVersion;

		private string _ApiVersion;

		public string AppId
		{
			get
			{
				return _AppId;
			}
			set
			{
				_AppId = value;
				HasAppId = value != null;
			}
		}

		public string RedirectUri
		{
			get
			{
				return _RedirectUri;
			}
			set
			{
				_RedirectUri = value;
				HasRedirectUri = value != null;
			}
		}

		public string ApiVersion
		{
			get
			{
				return _ApiVersion;
			}
			set
			{
				_ApiVersion = value;
				HasApiVersion = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAppId(string val)
		{
			AppId = val;
		}

		public void SetRedirectUri(string val)
		{
			RedirectUri = val;
		}

		public void SetApiVersion(string val)
		{
			ApiVersion = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAppId)
			{
				num ^= AppId.GetHashCode();
			}
			if (HasRedirectUri)
			{
				num ^= RedirectUri.GetHashCode();
			}
			if (HasApiVersion)
			{
				num ^= ApiVersion.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetFacebookSettingsResponse getFacebookSettingsResponse = obj as GetFacebookSettingsResponse;
			if (getFacebookSettingsResponse == null)
			{
				return false;
			}
			if (HasAppId != getFacebookSettingsResponse.HasAppId || (HasAppId && !AppId.Equals(getFacebookSettingsResponse.AppId)))
			{
				return false;
			}
			if (HasRedirectUri != getFacebookSettingsResponse.HasRedirectUri || (HasRedirectUri && !RedirectUri.Equals(getFacebookSettingsResponse.RedirectUri)))
			{
				return false;
			}
			if (HasApiVersion != getFacebookSettingsResponse.HasApiVersion || (HasApiVersion && !ApiVersion.Equals(getFacebookSettingsResponse.ApiVersion)))
			{
				return false;
			}
			return true;
		}

		public static GetFacebookSettingsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFacebookSettingsResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetFacebookSettingsResponse Deserialize(Stream stream, GetFacebookSettingsResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetFacebookSettingsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetFacebookSettingsResponse getFacebookSettingsResponse = new GetFacebookSettingsResponse();
			DeserializeLengthDelimited(stream, getFacebookSettingsResponse);
			return getFacebookSettingsResponse;
		}

		public static GetFacebookSettingsResponse DeserializeLengthDelimited(Stream stream, GetFacebookSettingsResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetFacebookSettingsResponse Deserialize(Stream stream, GetFacebookSettingsResponse instance, long limit)
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
					instance.AppId = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.RedirectUri = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.ApiVersion = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, GetFacebookSettingsResponse instance)
		{
			if (instance.HasAppId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AppId));
			}
			if (instance.HasRedirectUri)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RedirectUri));
			}
			if (instance.HasApiVersion)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApiVersion));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAppId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(AppId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasRedirectUri)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(RedirectUri);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasApiVersion)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(ApiVersion);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}
	}
}
