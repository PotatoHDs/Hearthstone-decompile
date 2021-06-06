using System.IO;

namespace bnet.protocol.channel.v2.server
{
	public class CreateChannelRequest : IProtoBuf
	{
		public bool HasOptions;

		private CreateChannelServerOptions _Options;

		public CreateChannelServerOptions Options
		{
			get
			{
				return _Options;
			}
			set
			{
				_Options = value;
				HasOptions = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetOptions(CreateChannelServerOptions val)
		{
			Options = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasOptions)
			{
				num ^= Options.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateChannelRequest createChannelRequest = obj as CreateChannelRequest;
			if (createChannelRequest == null)
			{
				return false;
			}
			if (HasOptions != createChannelRequest.HasOptions || (HasOptions && !Options.Equals(createChannelRequest.Options)))
			{
				return false;
			}
			return true;
		}

		public static CreateChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateChannelRequest Deserialize(Stream stream, CreateChannelRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelRequest createChannelRequest = new CreateChannelRequest();
			DeserializeLengthDelimited(stream, createChannelRequest);
			return createChannelRequest;
		}

		public static CreateChannelRequest DeserializeLengthDelimited(Stream stream, CreateChannelRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateChannelRequest Deserialize(Stream stream, CreateChannelRequest instance, long limit)
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
					if (instance.Options == null)
					{
						instance.Options = CreateChannelServerOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						CreateChannelServerOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		public static void Serialize(Stream stream, CreateChannelRequest instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				CreateChannelServerOptions.Serialize(stream, instance.Options);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasOptions)
			{
				num++;
				uint serializedSize = Options.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
