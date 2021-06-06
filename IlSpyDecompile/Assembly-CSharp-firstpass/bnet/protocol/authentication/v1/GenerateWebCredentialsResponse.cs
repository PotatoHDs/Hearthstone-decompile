using System.IO;

namespace bnet.protocol.authentication.v1
{
	public class GenerateWebCredentialsResponse : IProtoBuf
	{
		public bool HasWebCredentials;

		private byte[] _WebCredentials;

		public byte[] WebCredentials
		{
			get
			{
				return _WebCredentials;
			}
			set
			{
				_WebCredentials = value;
				HasWebCredentials = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetWebCredentials(byte[] val)
		{
			WebCredentials = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasWebCredentials)
			{
				num ^= WebCredentials.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GenerateWebCredentialsResponse generateWebCredentialsResponse = obj as GenerateWebCredentialsResponse;
			if (generateWebCredentialsResponse == null)
			{
				return false;
			}
			if (HasWebCredentials != generateWebCredentialsResponse.HasWebCredentials || (HasWebCredentials && !WebCredentials.Equals(generateWebCredentialsResponse.WebCredentials)))
			{
				return false;
			}
			return true;
		}

		public static GenerateWebCredentialsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenerateWebCredentialsResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GenerateWebCredentialsResponse Deserialize(Stream stream, GenerateWebCredentialsResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GenerateWebCredentialsResponse DeserializeLengthDelimited(Stream stream)
		{
			GenerateWebCredentialsResponse generateWebCredentialsResponse = new GenerateWebCredentialsResponse();
			DeserializeLengthDelimited(stream, generateWebCredentialsResponse);
			return generateWebCredentialsResponse;
		}

		public static GenerateWebCredentialsResponse DeserializeLengthDelimited(Stream stream, GenerateWebCredentialsResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GenerateWebCredentialsResponse Deserialize(Stream stream, GenerateWebCredentialsResponse instance, long limit)
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
					instance.WebCredentials = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, GenerateWebCredentialsResponse instance)
		{
			if (instance.HasWebCredentials)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.WebCredentials);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasWebCredentials)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(WebCredentials.Length) + WebCredentials.Length);
			}
			return num;
		}
	}
}
