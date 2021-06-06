using System.IO;

namespace bnet.protocol.authentication.v1
{
	public class GenerateTokenResponse : IProtoBuf
	{
		public bool HasAuthenticationToken;

		private byte[] _AuthenticationToken;

		public bool HasAuthenticationTokenId;

		private byte[] _AuthenticationTokenId;

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

		public byte[] AuthenticationTokenId
		{
			get
			{
				return _AuthenticationTokenId;
			}
			set
			{
				_AuthenticationTokenId = value;
				HasAuthenticationTokenId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAuthenticationToken(byte[] val)
		{
			AuthenticationToken = val;
		}

		public void SetAuthenticationTokenId(byte[] val)
		{
			AuthenticationTokenId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAuthenticationToken)
			{
				num ^= AuthenticationToken.GetHashCode();
			}
			if (HasAuthenticationTokenId)
			{
				num ^= AuthenticationTokenId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GenerateTokenResponse generateTokenResponse = obj as GenerateTokenResponse;
			if (generateTokenResponse == null)
			{
				return false;
			}
			if (HasAuthenticationToken != generateTokenResponse.HasAuthenticationToken || (HasAuthenticationToken && !AuthenticationToken.Equals(generateTokenResponse.AuthenticationToken)))
			{
				return false;
			}
			if (HasAuthenticationTokenId != generateTokenResponse.HasAuthenticationTokenId || (HasAuthenticationTokenId && !AuthenticationTokenId.Equals(generateTokenResponse.AuthenticationTokenId)))
			{
				return false;
			}
			return true;
		}

		public static GenerateTokenResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenerateTokenResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GenerateTokenResponse Deserialize(Stream stream, GenerateTokenResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GenerateTokenResponse DeserializeLengthDelimited(Stream stream)
		{
			GenerateTokenResponse generateTokenResponse = new GenerateTokenResponse();
			DeserializeLengthDelimited(stream, generateTokenResponse);
			return generateTokenResponse;
		}

		public static GenerateTokenResponse DeserializeLengthDelimited(Stream stream, GenerateTokenResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GenerateTokenResponse Deserialize(Stream stream, GenerateTokenResponse instance, long limit)
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
					instance.AuthenticationTokenId = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, GenerateTokenResponse instance)
		{
			if (instance.HasAuthenticationToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.AuthenticationToken);
			}
			if (instance.HasAuthenticationTokenId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.AuthenticationTokenId);
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
			if (HasAuthenticationTokenId)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(AuthenticationTokenId.Length) + AuthenticationTokenId.Length);
			}
			return num;
		}
	}
}
