using System.IO;

namespace bnet.protocol.channel.v1
{
	public class GetChannelInfoResponse : IProtoBuf
	{
		public bool HasChannelInfo;

		private ChannelInfo _ChannelInfo;

		public ChannelInfo ChannelInfo
		{
			get
			{
				return _ChannelInfo;
			}
			set
			{
				_ChannelInfo = value;
				HasChannelInfo = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetChannelInfo(ChannelInfo val)
		{
			ChannelInfo = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelInfo)
			{
				num ^= ChannelInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetChannelInfoResponse getChannelInfoResponse = obj as GetChannelInfoResponse;
			if (getChannelInfoResponse == null)
			{
				return false;
			}
			if (HasChannelInfo != getChannelInfoResponse.HasChannelInfo || (HasChannelInfo && !ChannelInfo.Equals(getChannelInfoResponse.ChannelInfo)))
			{
				return false;
			}
			return true;
		}

		public static GetChannelInfoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetChannelInfoResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetChannelInfoResponse Deserialize(Stream stream, GetChannelInfoResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetChannelInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			GetChannelInfoResponse getChannelInfoResponse = new GetChannelInfoResponse();
			DeserializeLengthDelimited(stream, getChannelInfoResponse);
			return getChannelInfoResponse;
		}

		public static GetChannelInfoResponse DeserializeLengthDelimited(Stream stream, GetChannelInfoResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetChannelInfoResponse Deserialize(Stream stream, GetChannelInfoResponse instance, long limit)
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
					if (instance.ChannelInfo == null)
					{
						instance.ChannelInfo = ChannelInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelInfo.DeserializeLengthDelimited(stream, instance.ChannelInfo);
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

		public static void Serialize(Stream stream, GetChannelInfoResponse instance)
		{
			if (instance.HasChannelInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelInfo.GetSerializedSize());
				ChannelInfo.Serialize(stream, instance.ChannelInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasChannelInfo)
			{
				num++;
				uint serializedSize = ChannelInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
