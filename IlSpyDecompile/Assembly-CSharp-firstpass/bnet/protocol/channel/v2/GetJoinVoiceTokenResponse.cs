using System.IO;
using System.Text;

namespace bnet.protocol.channel.v2
{
	public class GetJoinVoiceTokenResponse : IProtoBuf
	{
		public bool HasChannelUri;

		private string _ChannelUri;

		public bool HasCredentials;

		private VoiceCredentials _Credentials;

		public string ChannelUri
		{
			get
			{
				return _ChannelUri;
			}
			set
			{
				_ChannelUri = value;
				HasChannelUri = value != null;
			}
		}

		public VoiceCredentials Credentials
		{
			get
			{
				return _Credentials;
			}
			set
			{
				_Credentials = value;
				HasCredentials = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetChannelUri(string val)
		{
			ChannelUri = val;
		}

		public void SetCredentials(VoiceCredentials val)
		{
			Credentials = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelUri)
			{
				num ^= ChannelUri.GetHashCode();
			}
			if (HasCredentials)
			{
				num ^= Credentials.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetJoinVoiceTokenResponse getJoinVoiceTokenResponse = obj as GetJoinVoiceTokenResponse;
			if (getJoinVoiceTokenResponse == null)
			{
				return false;
			}
			if (HasChannelUri != getJoinVoiceTokenResponse.HasChannelUri || (HasChannelUri && !ChannelUri.Equals(getJoinVoiceTokenResponse.ChannelUri)))
			{
				return false;
			}
			if (HasCredentials != getJoinVoiceTokenResponse.HasCredentials || (HasCredentials && !Credentials.Equals(getJoinVoiceTokenResponse.Credentials)))
			{
				return false;
			}
			return true;
		}

		public static GetJoinVoiceTokenResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetJoinVoiceTokenResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetJoinVoiceTokenResponse Deserialize(Stream stream, GetJoinVoiceTokenResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetJoinVoiceTokenResponse DeserializeLengthDelimited(Stream stream)
		{
			GetJoinVoiceTokenResponse getJoinVoiceTokenResponse = new GetJoinVoiceTokenResponse();
			DeserializeLengthDelimited(stream, getJoinVoiceTokenResponse);
			return getJoinVoiceTokenResponse;
		}

		public static GetJoinVoiceTokenResponse DeserializeLengthDelimited(Stream stream, GetJoinVoiceTokenResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetJoinVoiceTokenResponse Deserialize(Stream stream, GetJoinVoiceTokenResponse instance, long limit)
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
					instance.ChannelUri = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					if (instance.Credentials == null)
					{
						instance.Credentials = VoiceCredentials.DeserializeLengthDelimited(stream);
					}
					else
					{
						VoiceCredentials.DeserializeLengthDelimited(stream, instance.Credentials);
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

		public static void Serialize(Stream stream, GetJoinVoiceTokenResponse instance)
		{
			if (instance.HasChannelUri)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelUri));
			}
			if (instance.HasCredentials)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Credentials.GetSerializedSize());
				VoiceCredentials.Serialize(stream, instance.Credentials);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasChannelUri)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ChannelUri);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasCredentials)
			{
				num++;
				uint serializedSize = Credentials.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
