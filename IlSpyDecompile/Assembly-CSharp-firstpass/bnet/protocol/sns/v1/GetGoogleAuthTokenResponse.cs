using System.IO;
using System.Text;

namespace bnet.protocol.sns.v1
{
	public class GetGoogleAuthTokenResponse : IProtoBuf
	{
		public bool HasAccessToken;

		private string _AccessToken;

		public string AccessToken
		{
			get
			{
				return _AccessToken;
			}
			set
			{
				_AccessToken = value;
				HasAccessToken = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAccessToken(string val)
		{
			AccessToken = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccessToken)
			{
				num ^= AccessToken.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGoogleAuthTokenResponse getGoogleAuthTokenResponse = obj as GetGoogleAuthTokenResponse;
			if (getGoogleAuthTokenResponse == null)
			{
				return false;
			}
			if (HasAccessToken != getGoogleAuthTokenResponse.HasAccessToken || (HasAccessToken && !AccessToken.Equals(getGoogleAuthTokenResponse.AccessToken)))
			{
				return false;
			}
			return true;
		}

		public static GetGoogleAuthTokenResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGoogleAuthTokenResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetGoogleAuthTokenResponse Deserialize(Stream stream, GetGoogleAuthTokenResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetGoogleAuthTokenResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGoogleAuthTokenResponse getGoogleAuthTokenResponse = new GetGoogleAuthTokenResponse();
			DeserializeLengthDelimited(stream, getGoogleAuthTokenResponse);
			return getGoogleAuthTokenResponse;
		}

		public static GetGoogleAuthTokenResponse DeserializeLengthDelimited(Stream stream, GetGoogleAuthTokenResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetGoogleAuthTokenResponse Deserialize(Stream stream, GetGoogleAuthTokenResponse instance, long limit)
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
					instance.AccessToken = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, GetGoogleAuthTokenResponse instance)
		{
			if (instance.HasAccessToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AccessToken));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAccessToken)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(AccessToken);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
