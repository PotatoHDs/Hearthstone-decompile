using System.IO;

namespace bnet.protocol.authentication.v1
{
	public class GenerateSSOTokenResponse : IProtoBuf
	{
		public bool HasSsoId;

		private byte[] _SsoId;

		public bool HasSsoSecret;

		private byte[] _SsoSecret;

		public byte[] SsoId
		{
			get
			{
				return _SsoId;
			}
			set
			{
				_SsoId = value;
				HasSsoId = value != null;
			}
		}

		public byte[] SsoSecret
		{
			get
			{
				return _SsoSecret;
			}
			set
			{
				_SsoSecret = value;
				HasSsoSecret = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetSsoId(byte[] val)
		{
			SsoId = val;
		}

		public void SetSsoSecret(byte[] val)
		{
			SsoSecret = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSsoId)
			{
				num ^= SsoId.GetHashCode();
			}
			if (HasSsoSecret)
			{
				num ^= SsoSecret.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GenerateSSOTokenResponse generateSSOTokenResponse = obj as GenerateSSOTokenResponse;
			if (generateSSOTokenResponse == null)
			{
				return false;
			}
			if (HasSsoId != generateSSOTokenResponse.HasSsoId || (HasSsoId && !SsoId.Equals(generateSSOTokenResponse.SsoId)))
			{
				return false;
			}
			if (HasSsoSecret != generateSSOTokenResponse.HasSsoSecret || (HasSsoSecret && !SsoSecret.Equals(generateSSOTokenResponse.SsoSecret)))
			{
				return false;
			}
			return true;
		}

		public static GenerateSSOTokenResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenerateSSOTokenResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GenerateSSOTokenResponse Deserialize(Stream stream, GenerateSSOTokenResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GenerateSSOTokenResponse DeserializeLengthDelimited(Stream stream)
		{
			GenerateSSOTokenResponse generateSSOTokenResponse = new GenerateSSOTokenResponse();
			DeserializeLengthDelimited(stream, generateSSOTokenResponse);
			return generateSSOTokenResponse;
		}

		public static GenerateSSOTokenResponse DeserializeLengthDelimited(Stream stream, GenerateSSOTokenResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GenerateSSOTokenResponse Deserialize(Stream stream, GenerateSSOTokenResponse instance, long limit)
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
					instance.SsoId = ProtocolParser.ReadBytes(stream);
					continue;
				case 18:
					instance.SsoSecret = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, GenerateSSOTokenResponse instance)
		{
			if (instance.HasSsoId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.SsoId);
			}
			if (instance.HasSsoSecret)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.SsoSecret);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSsoId)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(SsoId.Length) + SsoId.Length);
			}
			if (HasSsoSecret)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(SsoSecret.Length) + SsoSecret.Length);
			}
			return num;
		}
	}
}
