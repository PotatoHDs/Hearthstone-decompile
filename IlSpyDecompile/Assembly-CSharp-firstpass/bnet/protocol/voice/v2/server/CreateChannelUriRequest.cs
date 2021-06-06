using System.IO;
using System.Text;

namespace bnet.protocol.voice.v2.server
{
	public class CreateChannelUriRequest : IProtoBuf
	{
		public bool HasChannelName;

		private string _ChannelName;

		public string ChannelName
		{
			get
			{
				return _ChannelName;
			}
			set
			{
				_ChannelName = value;
				HasChannelName = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetChannelName(string val)
		{
			ChannelName = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelName)
			{
				num ^= ChannelName.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateChannelUriRequest createChannelUriRequest = obj as CreateChannelUriRequest;
			if (createChannelUriRequest == null)
			{
				return false;
			}
			if (HasChannelName != createChannelUriRequest.HasChannelName || (HasChannelName && !ChannelName.Equals(createChannelUriRequest.ChannelName)))
			{
				return false;
			}
			return true;
		}

		public static CreateChannelUriRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelUriRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateChannelUriRequest Deserialize(Stream stream, CreateChannelUriRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateChannelUriRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelUriRequest createChannelUriRequest = new CreateChannelUriRequest();
			DeserializeLengthDelimited(stream, createChannelUriRequest);
			return createChannelUriRequest;
		}

		public static CreateChannelUriRequest DeserializeLengthDelimited(Stream stream, CreateChannelUriRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateChannelUriRequest Deserialize(Stream stream, CreateChannelUriRequest instance, long limit)
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
					instance.ChannelName = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, CreateChannelUriRequest instance)
		{
			if (instance.HasChannelName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelName));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasChannelName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ChannelName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
