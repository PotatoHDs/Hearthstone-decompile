using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	public class GetJoinTokenResponse : IProtoBuf
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
			GetJoinTokenResponse getJoinTokenResponse = obj as GetJoinTokenResponse;
			if (getJoinTokenResponse == null)
			{
				return false;
			}
			if (HasChannelUri != getJoinTokenResponse.HasChannelUri || (HasChannelUri && !ChannelUri.Equals(getJoinTokenResponse.ChannelUri)))
			{
				return false;
			}
			if (HasCredentials != getJoinTokenResponse.HasCredentials || (HasCredentials && !Credentials.Equals(getJoinTokenResponse.Credentials)))
			{
				return false;
			}
			return true;
		}

		public static GetJoinTokenResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetJoinTokenResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetJoinTokenResponse Deserialize(Stream stream, GetJoinTokenResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetJoinTokenResponse DeserializeLengthDelimited(Stream stream)
		{
			GetJoinTokenResponse getJoinTokenResponse = new GetJoinTokenResponse();
			DeserializeLengthDelimited(stream, getJoinTokenResponse);
			return getJoinTokenResponse;
		}

		public static GetJoinTokenResponse DeserializeLengthDelimited(Stream stream, GetJoinTokenResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetJoinTokenResponse Deserialize(Stream stream, GetJoinTokenResponse instance, long limit)
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

		public static void Serialize(Stream stream, GetJoinTokenResponse instance)
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
