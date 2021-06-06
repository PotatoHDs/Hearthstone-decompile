using System.IO;

namespace bnet.protocol.authentication.v1
{
	public class AuthenticateTokenResponse : IProtoBuf
	{
		public bool HasAuthenticationToken;

		private byte[] _AuthenticationToken;

		public bool HasSsoData;

		private SSOData _SsoData;

		public byte[] AuthenticationToken
		{
			get
			{
				return _AuthenticationToken;
			}
			set
			{
				_AuthenticationToken = value;
				HasAuthenticationToken = value != null;
			}
		}

		public SSOData SsoData
		{
			get
			{
				return _SsoData;
			}
			set
			{
				_SsoData = value;
				HasSsoData = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAuthenticationToken(byte[] val)
		{
			AuthenticationToken = val;
		}

		public void SetSsoData(SSOData val)
		{
			SsoData = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAuthenticationToken)
			{
				num ^= AuthenticationToken.GetHashCode();
			}
			if (HasSsoData)
			{
				num ^= SsoData.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AuthenticateTokenResponse authenticateTokenResponse = obj as AuthenticateTokenResponse;
			if (authenticateTokenResponse == null)
			{
				return false;
			}
			if (HasAuthenticationToken != authenticateTokenResponse.HasAuthenticationToken || (HasAuthenticationToken && !AuthenticationToken.Equals(authenticateTokenResponse.AuthenticationToken)))
			{
				return false;
			}
			if (HasSsoData != authenticateTokenResponse.HasSsoData || (HasSsoData && !SsoData.Equals(authenticateTokenResponse.SsoData)))
			{
				return false;
			}
			return true;
		}

		public static AuthenticateTokenResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AuthenticateTokenResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AuthenticateTokenResponse Deserialize(Stream stream, AuthenticateTokenResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AuthenticateTokenResponse DeserializeLengthDelimited(Stream stream)
		{
			AuthenticateTokenResponse authenticateTokenResponse = new AuthenticateTokenResponse();
			DeserializeLengthDelimited(stream, authenticateTokenResponse);
			return authenticateTokenResponse;
		}

		public static AuthenticateTokenResponse DeserializeLengthDelimited(Stream stream, AuthenticateTokenResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AuthenticateTokenResponse Deserialize(Stream stream, AuthenticateTokenResponse instance, long limit)
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
					instance.AuthenticationToken = ProtocolParser.ReadBytes(stream);
					continue;
				case 18:
					if (instance.SsoData == null)
					{
						instance.SsoData = SSOData.DeserializeLengthDelimited(stream);
					}
					else
					{
						SSOData.DeserializeLengthDelimited(stream, instance.SsoData);
					}
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

		public static void Serialize(Stream stream, AuthenticateTokenResponse instance)
		{
			if (instance.HasAuthenticationToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.AuthenticationToken);
			}
			if (instance.HasSsoData)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SsoData.GetSerializedSize());
				SSOData.Serialize(stream, instance.SsoData);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAuthenticationToken)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(AuthenticationToken.Length) + AuthenticationToken.Length);
			}
			if (HasSsoData)
			{
				num++;
				uint serializedSize = SsoData.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
