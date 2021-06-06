using System.IO;

namespace bnet.protocol.channel.v2
{
	public class GetChannelResponse : IProtoBuf
	{
		public bool HasChannel;

		private Channel _Channel;

		public Channel Channel
		{
			get
			{
				return _Channel;
			}
			set
			{
				_Channel = value;
				HasChannel = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetChannel(Channel val)
		{
			Channel = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannel)
			{
				num ^= Channel.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetChannelResponse getChannelResponse = obj as GetChannelResponse;
			if (getChannelResponse == null)
			{
				return false;
			}
			if (HasChannel != getChannelResponse.HasChannel || (HasChannel && !Channel.Equals(getChannelResponse.Channel)))
			{
				return false;
			}
			return true;
		}

		public static GetChannelResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetChannelResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetChannelResponse Deserialize(Stream stream, GetChannelResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetChannelResponse DeserializeLengthDelimited(Stream stream)
		{
			GetChannelResponse getChannelResponse = new GetChannelResponse();
			DeserializeLengthDelimited(stream, getChannelResponse);
			return getChannelResponse;
		}

		public static GetChannelResponse DeserializeLengthDelimited(Stream stream, GetChannelResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetChannelResponse Deserialize(Stream stream, GetChannelResponse instance, long limit)
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
					if (instance.Channel == null)
					{
						instance.Channel = Channel.DeserializeLengthDelimited(stream);
					}
					else
					{
						Channel.DeserializeLengthDelimited(stream, instance.Channel);
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

		public static void Serialize(Stream stream, GetChannelResponse instance)
		{
			if (instance.HasChannel)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Channel.GetSerializedSize());
				Channel.Serialize(stream, instance.Channel);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasChannel)
			{
				num++;
				uint serializedSize = Channel.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
