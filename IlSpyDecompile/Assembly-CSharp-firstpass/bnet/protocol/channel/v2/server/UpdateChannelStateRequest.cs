using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	public class UpdateChannelStateRequest : IProtoBuf
	{
		public bool HasChannelId;

		private bnet.protocol.channel.v1.ChannelId _ChannelId;

		public bool HasOptions;

		private ChannelStateOptions _Options;

		public bnet.protocol.channel.v1.ChannelId ChannelId
		{
			get
			{
				return _ChannelId;
			}
			set
			{
				_ChannelId = value;
				HasChannelId = value != null;
			}
		}

		public ChannelStateOptions Options
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

		public void SetChannelId(bnet.protocol.channel.v1.ChannelId val)
		{
			ChannelId = val;
		}

		public void SetOptions(ChannelStateOptions val)
		{
			Options = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			if (HasOptions)
			{
				num ^= Options.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateChannelStateRequest updateChannelStateRequest = obj as UpdateChannelStateRequest;
			if (updateChannelStateRequest == null)
			{
				return false;
			}
			if (HasChannelId != updateChannelStateRequest.HasChannelId || (HasChannelId && !ChannelId.Equals(updateChannelStateRequest.ChannelId)))
			{
				return false;
			}
			if (HasOptions != updateChannelStateRequest.HasOptions || (HasOptions && !Options.Equals(updateChannelStateRequest.Options)))
			{
				return false;
			}
			return true;
		}

		public static UpdateChannelStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateChannelStateRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateChannelStateRequest Deserialize(Stream stream, UpdateChannelStateRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateChannelStateRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateChannelStateRequest updateChannelStateRequest = new UpdateChannelStateRequest();
			DeserializeLengthDelimited(stream, updateChannelStateRequest);
			return updateChannelStateRequest;
		}

		public static UpdateChannelStateRequest DeserializeLengthDelimited(Stream stream, UpdateChannelStateRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateChannelStateRequest Deserialize(Stream stream, UpdateChannelStateRequest instance, long limit)
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
					if (instance.ChannelId == null)
					{
						instance.ChannelId = bnet.protocol.channel.v1.ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.channel.v1.ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 18:
					if (instance.Options == null)
					{
						instance.Options = ChannelStateOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelStateOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		public static void Serialize(Stream stream, UpdateChannelStateRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				bnet.protocol.channel.v1.ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				ChannelStateOptions.Serialize(stream, instance.Options);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasChannelId)
			{
				num++;
				uint serializedSize = ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasOptions)
			{
				num++;
				uint serializedSize2 = Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
