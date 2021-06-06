using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.authentication.v1
{
	public class GenerateTrustedWebCredentialsRequest : IProtoBuf
	{
		public bool HasAccountId;

		private AccountId _AccountId;

		public bool HasProgram;

		private uint _Program;

		public bool HasPlatformId;

		private string _PlatformId;

		public bool HasClientIp;

		private string _ClientIp;

		public bool HasSessionKey;

		private byte[] _SessionKey;

		public AccountId AccountId
		{
			get
			{
				return _AccountId;
			}
			set
			{
				_AccountId = value;
				HasAccountId = value != null;
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

		public byte[] SessionKey
		{
			get
			{
				return _SessionKey;
			}
			set
			{
				_SessionKey = value;
				HasSessionKey = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAccountId(AccountId val)
		{
			AccountId = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetPlatformId(string val)
		{
			PlatformId = val;
		}

		public void SetClientIp(string val)
		{
			ClientIp = val;
		}

		public void SetSessionKey(byte[] val)
		{
			SessionKey = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccountId)
			{
				num ^= AccountId.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasPlatformId)
			{
				num ^= PlatformId.GetHashCode();
			}
			if (HasClientIp)
			{
				num ^= ClientIp.GetHashCode();
			}
			if (HasSessionKey)
			{
				num ^= SessionKey.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GenerateTrustedWebCredentialsRequest generateTrustedWebCredentialsRequest = obj as GenerateTrustedWebCredentialsRequest;
			if (generateTrustedWebCredentialsRequest == null)
			{
				return false;
			}
			if (HasAccountId != generateTrustedWebCredentialsRequest.HasAccountId || (HasAccountId && !AccountId.Equals(generateTrustedWebCredentialsRequest.AccountId)))
			{
				return false;
			}
			if (HasProgram != generateTrustedWebCredentialsRequest.HasProgram || (HasProgram && !Program.Equals(generateTrustedWebCredentialsRequest.Program)))
			{
				return false;
			}
			if (HasPlatformId != generateTrustedWebCredentialsRequest.HasPlatformId || (HasPlatformId && !PlatformId.Equals(generateTrustedWebCredentialsRequest.PlatformId)))
			{
				return false;
			}
			if (HasClientIp != generateTrustedWebCredentialsRequest.HasClientIp || (HasClientIp && !ClientIp.Equals(generateTrustedWebCredentialsRequest.ClientIp)))
			{
				return false;
			}
			if (HasSessionKey != generateTrustedWebCredentialsRequest.HasSessionKey || (HasSessionKey && !SessionKey.Equals(generateTrustedWebCredentialsRequest.SessionKey)))
			{
				return false;
			}
			return true;
		}

		public static GenerateTrustedWebCredentialsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenerateTrustedWebCredentialsRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GenerateTrustedWebCredentialsRequest Deserialize(Stream stream, GenerateTrustedWebCredentialsRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GenerateTrustedWebCredentialsRequest DeserializeLengthDelimited(Stream stream)
		{
			GenerateTrustedWebCredentialsRequest generateTrustedWebCredentialsRequest = new GenerateTrustedWebCredentialsRequest();
			DeserializeLengthDelimited(stream, generateTrustedWebCredentialsRequest);
			return generateTrustedWebCredentialsRequest;
		}

		public static GenerateTrustedWebCredentialsRequest DeserializeLengthDelimited(Stream stream, GenerateTrustedWebCredentialsRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GenerateTrustedWebCredentialsRequest Deserialize(Stream stream, GenerateTrustedWebCredentialsRequest instance, long limit)
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
					if (instance.AccountId == null)
					{
						instance.AccountId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.AccountId);
					}
					continue;
				case 21:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 26:
					instance.PlatformId = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.ClientIp = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.SessionKey = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, GenerateTrustedWebCredentialsRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				AccountId.Serialize(stream, instance.AccountId);
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
			if (instance.HasClientIp)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientIp));
			}
			if (instance.HasSessionKey)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, instance.SessionKey);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAccountId)
			{
				num++;
				uint serializedSize = AccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
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
			if (HasClientIp)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ClientIp);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasSessionKey)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(SessionKey.Length) + SessionKey.Length);
			}
			return num;
		}
	}
}
