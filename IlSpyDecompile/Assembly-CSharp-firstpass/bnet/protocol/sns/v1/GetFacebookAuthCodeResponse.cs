using System.IO;
using System.Text;

namespace bnet.protocol.sns.v1
{
	public class GetFacebookAuthCodeResponse : IProtoBuf
	{
		public bool HasFbCode;

		private string _FbCode;

		public bool HasRedirectUri;

		private string _RedirectUri;

		public bool HasFbId;

		private string _FbId;

		public string FbCode
		{
			get
			{
				return _FbCode;
			}
			set
			{
				_FbCode = value;
				HasFbCode = value != null;
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

		public string FbId
		{
			get
			{
				return _FbId;
			}
			set
			{
				_FbId = value;
				HasFbId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetFbCode(string val)
		{
			FbCode = val;
		}

		public void SetRedirectUri(string val)
		{
			RedirectUri = val;
		}

		public void SetFbId(string val)
		{
			FbId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFbCode)
			{
				num ^= FbCode.GetHashCode();
			}
			if (HasRedirectUri)
			{
				num ^= RedirectUri.GetHashCode();
			}
			if (HasFbId)
			{
				num ^= FbId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetFacebookAuthCodeResponse getFacebookAuthCodeResponse = obj as GetFacebookAuthCodeResponse;
			if (getFacebookAuthCodeResponse == null)
			{
				return false;
			}
			if (HasFbCode != getFacebookAuthCodeResponse.HasFbCode || (HasFbCode && !FbCode.Equals(getFacebookAuthCodeResponse.FbCode)))
			{
				return false;
			}
			if (HasRedirectUri != getFacebookAuthCodeResponse.HasRedirectUri || (HasRedirectUri && !RedirectUri.Equals(getFacebookAuthCodeResponse.RedirectUri)))
			{
				return false;
			}
			if (HasFbId != getFacebookAuthCodeResponse.HasFbId || (HasFbId && !FbId.Equals(getFacebookAuthCodeResponse.FbId)))
			{
				return false;
			}
			return true;
		}

		public static GetFacebookAuthCodeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFacebookAuthCodeResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetFacebookAuthCodeResponse Deserialize(Stream stream, GetFacebookAuthCodeResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetFacebookAuthCodeResponse DeserializeLengthDelimited(Stream stream)
		{
			GetFacebookAuthCodeResponse getFacebookAuthCodeResponse = new GetFacebookAuthCodeResponse();
			DeserializeLengthDelimited(stream, getFacebookAuthCodeResponse);
			return getFacebookAuthCodeResponse;
		}

		public static GetFacebookAuthCodeResponse DeserializeLengthDelimited(Stream stream, GetFacebookAuthCodeResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetFacebookAuthCodeResponse Deserialize(Stream stream, GetFacebookAuthCodeResponse instance, long limit)
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
					instance.FbCode = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.RedirectUri = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.FbId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, GetFacebookAuthCodeResponse instance)
		{
			if (instance.HasFbCode)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FbCode));
			}
			if (instance.HasRedirectUri)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RedirectUri));
			}
			if (instance.HasFbId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FbId));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasFbCode)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(FbCode);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasRedirectUri)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(RedirectUri);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasFbId)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(FbId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}
	}
}
