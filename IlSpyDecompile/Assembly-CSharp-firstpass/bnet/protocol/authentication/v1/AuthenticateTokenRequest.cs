using System.IO;
using System.Text;

namespace bnet.protocol.authentication.v1
{
	public class AuthenticateTokenRequest : IProtoBuf
	{
		public bool HasAuthenticationToken;

		private byte[] _AuthenticationToken;

		public bool HasProgram;

		private uint _Program;

		public bool HasPlatformId;

		private string _PlatformId;

		public bool HasLocale;

		private string _Locale;

		public bool HasClientIp;

		private string _ClientIp;

		public bool HasUserAgent;

		private string _UserAgent;

		public bool HasVersion;

		private ulong _Version;

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

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public string PlatformId
		{
			get
			{
				return _PlatformId;
			}
			set
			{
				_PlatformId = value;
				HasPlatformId = value != null;
			}
		}

		public string Locale
		{
			get
			{
				return _Locale;
			}
			set
			{
				_Locale = value;
				HasLocale = value != null;
			}
		}

		public string ClientIp
		{
			get
			{
				return _ClientIp;
			}
			set
			{
				_ClientIp = value;
				HasClientIp = value != null;
			}
		}

		public string UserAgent
		{
			get
			{
				return _UserAgent;
			}
			set
			{
				_UserAgent = value;
				HasUserAgent = value != null;
			}
		}

		public ulong Version
		{
			get
			{
				return _Version;
			}
			set
			{
				_Version = value;
				HasVersion = true;
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

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetPlatformId(string val)
		{
			PlatformId = val;
		}

		public void SetLocale(string val)
		{
			Locale = val;
		}

		public void SetClientIp(string val)
		{
			ClientIp = val;
		}

		public void SetUserAgent(string val)
		{
			UserAgent = val;
		}

		public void SetVersion(ulong val)
		{
			Version = val;
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
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasPlatformId)
			{
				num ^= PlatformId.GetHashCode();
			}
			if (HasLocale)
			{
				num ^= Locale.GetHashCode();
			}
			if (HasClientIp)
			{
				num ^= ClientIp.GetHashCode();
			}
			if (HasUserAgent)
			{
				num ^= UserAgent.GetHashCode();
			}
			if (HasVersion)
			{
				num ^= Version.GetHashCode();
			}
			if (HasAuthenticationTokenId)
			{
				num ^= AuthenticationTokenId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AuthenticateTokenRequest authenticateTokenRequest = obj as AuthenticateTokenRequest;
			if (authenticateTokenRequest == null)
			{
				return false;
			}
			if (HasAuthenticationToken != authenticateTokenRequest.HasAuthenticationToken || (HasAuthenticationToken && !AuthenticationToken.Equals(authenticateTokenRequest.AuthenticationToken)))
			{
				return false;
			}
			if (HasProgram != authenticateTokenRequest.HasProgram || (HasProgram && !Program.Equals(authenticateTokenRequest.Program)))
			{
				return false;
			}
			if (HasPlatformId != authenticateTokenRequest.HasPlatformId || (HasPlatformId && !PlatformId.Equals(authenticateTokenRequest.PlatformId)))
			{
				return false;
			}
			if (HasLocale != authenticateTokenRequest.HasLocale || (HasLocale && !Locale.Equals(authenticateTokenRequest.Locale)))
			{
				return false;
			}
			if (HasClientIp != authenticateTokenRequest.HasClientIp || (HasClientIp && !ClientIp.Equals(authenticateTokenRequest.ClientIp)))
			{
				return false;
			}
			if (HasUserAgent != authenticateTokenRequest.HasUserAgent || (HasUserAgent && !UserAgent.Equals(authenticateTokenRequest.UserAgent)))
			{
				return false;
			}
			if (HasVersion != authenticateTokenRequest.HasVersion || (HasVersion && !Version.Equals(authenticateTokenRequest.Version)))
			{
				return false;
			}
			if (HasAuthenticationTokenId != authenticateTokenRequest.HasAuthenticationTokenId || (HasAuthenticationTokenId && !AuthenticationTokenId.Equals(authenticateTokenRequest.AuthenticationTokenId)))
			{
				return false;
			}
			return true;
		}

		public static AuthenticateTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AuthenticateTokenRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AuthenticateTokenRequest Deserialize(Stream stream, AuthenticateTokenRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AuthenticateTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			AuthenticateTokenRequest authenticateTokenRequest = new AuthenticateTokenRequest();
			DeserializeLengthDelimited(stream, authenticateTokenRequest);
			return authenticateTokenRequest;
		}

		public static AuthenticateTokenRequest DeserializeLengthDelimited(Stream stream, AuthenticateTokenRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AuthenticateTokenRequest Deserialize(Stream stream, AuthenticateTokenRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 21:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 26:
					instance.PlatformId = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.Locale = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.ClientIp = ProtocolParser.ReadString(stream);
					continue;
				case 50:
					instance.UserAgent = ProtocolParser.ReadString(stream);
					continue;
				case 56:
					instance.Version = ProtocolParser.ReadUInt64(stream);
					continue;
				case 66:
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

		public static void Serialize(Stream stream, AuthenticateTokenRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAuthenticationToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.AuthenticationToken);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasPlatformId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PlatformId));
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Locale));
			}
			if (instance.HasClientIp)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientIp));
			}
			if (instance.HasUserAgent)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UserAgent));
			}
			if (instance.HasVersion)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.Version);
			}
			if (instance.HasAuthenticationTokenId)
			{
				stream.WriteByte(66);
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
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (HasPlatformId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(PlatformId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasLocale)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Locale);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasClientIp)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(ClientIp);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasUserAgent)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(UserAgent);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasVersion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Version);
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
