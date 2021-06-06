using System.IO;
using System.Text;

namespace bnet.protocol.voice.v2.server
{
	public class CreateChannelUriResponse : IProtoBuf
	{
		public bool HasChannelUri;

		private string _ChannelUri;

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

		public bool IsInitialized => true;

		public void SetChannelUri(string val)
		{
			ChannelUri = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelUri)
			{
				num ^= ChannelUri.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateChannelUriResponse createChannelUriResponse = obj as CreateChannelUriResponse;
			if (createChannelUriResponse == null)
			{
				return false;
			}
			if (HasChannelUri != createChannelUriResponse.HasChannelUri || (HasChannelUri && !ChannelUri.Equals(createChannelUriResponse.ChannelUri)))
			{
				return false;
			}
			return true;
		}

		public static CreateChannelUriResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelUriResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateChannelUriResponse Deserialize(Stream stream, CreateChannelUriResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateChannelUriResponse DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelUriResponse createChannelUriResponse = new CreateChannelUriResponse();
			DeserializeLengthDelimited(stream, createChannelUriResponse);
			return createChannelUriResponse;
		}

		public static CreateChannelUriResponse DeserializeLengthDelimited(Stream stream, CreateChannelUriResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateChannelUriResponse Deserialize(Stream stream, CreateChannelUriResponse instance, long limit)
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

		public static void Serialize(Stream stream, CreateChannelUriResponse instance)
		{
			if (instance.HasChannelUri)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelUri));
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
			return num;
		}
	}
}
