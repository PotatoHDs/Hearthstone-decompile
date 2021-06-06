using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.authentication.v1
{
	public class GenerateTokenRequest : IProtoBuf
	{
		public bool HasAccountId;

		private AccountId _AccountId;

		public bool HasProgram;

		private uint _Program;

		public bool HasPlatformId;

		private string _PlatformId;

		public bool HasClientIp;

		private string _ClientIp;

		public bool HasSingleUse;

		private bool _SingleUse;

		public bool HasGenerateTokenId;

		private bool _GenerateTokenId;

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

		public bool SingleUse
		{
			get
			{
				return _SingleUse;
			}
			set
			{
				_SingleUse = value;
				HasSingleUse = true;
			}
		}

		public bool GenerateTokenId
		{
			get
			{
				return _GenerateTokenId;
			}
			set
			{
				_GenerateTokenId = value;
				HasGenerateTokenId = true;
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

		public void SetSingleUse(bool val)
		{
			SingleUse = val;
		}

		public void SetGenerateTokenId(bool val)
		{
			GenerateTokenId = val;
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
			if (HasSingleUse)
			{
				num ^= SingleUse.GetHashCode();
			}
			if (HasGenerateTokenId)
			{
				num ^= GenerateTokenId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GenerateTokenRequest generateTokenRequest = obj as GenerateTokenRequest;
			if (generateTokenRequest == null)
			{
				return false;
			}
			if (HasAccountId != generateTokenRequest.HasAccountId || (HasAccountId && !AccountId.Equals(generateTokenRequest.AccountId)))
			{
				return false;
			}
			if (HasProgram != generateTokenRequest.HasProgram || (HasProgram && !Program.Equals(generateTokenRequest.Program)))
			{
				return false;
			}
			if (HasPlatformId != generateTokenRequest.HasPlatformId || (HasPlatformId && !PlatformId.Equals(generateTokenRequest.PlatformId)))
			{
				return false;
			}
			if (HasClientIp != generateTokenRequest.HasClientIp || (HasClientIp && !ClientIp.Equals(generateTokenRequest.ClientIp)))
			{
				return false;
			}
			if (HasSingleUse != generateTokenRequest.HasSingleUse || (HasSingleUse && !SingleUse.Equals(generateTokenRequest.SingleUse)))
			{
				return false;
			}
			if (HasGenerateTokenId != generateTokenRequest.HasGenerateTokenId || (HasGenerateTokenId && !GenerateTokenId.Equals(generateTokenRequest.GenerateTokenId)))
			{
				return false;
			}
			return true;
		}

		public static GenerateTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenerateTokenRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GenerateTokenRequest Deserialize(Stream stream, GenerateTokenRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GenerateTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			GenerateTokenRequest generateTokenRequest = new GenerateTokenRequest();
			DeserializeLengthDelimited(stream, generateTokenRequest);
			return generateTokenRequest;
		}

		public static GenerateTokenRequest DeserializeLengthDelimited(Stream stream, GenerateTokenRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GenerateTokenRequest Deserialize(Stream stream, GenerateTokenRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.SingleUse = true;
			instance.GenerateTokenId = false;
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
				case 48:
					instance.SingleUse = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
					instance.GenerateTokenId = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, GenerateTokenRequest instance)
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
			if (instance.HasSingleUse)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.SingleUse);
			}
			if (instance.HasGenerateTokenId)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.GenerateTokenId);
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
			if (HasSingleUse)
			{
				num++;
				num++;
			}
			if (HasGenerateTokenId)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
