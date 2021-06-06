using System.IO;
using bnet.protocol.channel.v2;

namespace bnet.protocol.matchmaking.v1
{
	public class QueueMatchmakingRequest : IProtoBuf
	{
		public bool HasOptions;

		private GameMatchmakingOptions _Options;

		public bool HasChannelId;

		private ChannelId _ChannelId;

		public GameMatchmakingOptions Options
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

		public ChannelId ChannelId
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

		public bool IsInitialized => true;

		public void SetOptions(GameMatchmakingOptions val)
		{
			Options = val;
		}

		public void SetChannelId(ChannelId val)
		{
			ChannelId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasOptions)
			{
				num ^= Options.GetHashCode();
			}
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			QueueMatchmakingRequest queueMatchmakingRequest = obj as QueueMatchmakingRequest;
			if (queueMatchmakingRequest == null)
			{
				return false;
			}
			if (HasOptions != queueMatchmakingRequest.HasOptions || (HasOptions && !Options.Equals(queueMatchmakingRequest.Options)))
			{
				return false;
			}
			if (HasChannelId != queueMatchmakingRequest.HasChannelId || (HasChannelId && !ChannelId.Equals(queueMatchmakingRequest.ChannelId)))
			{
				return false;
			}
			return true;
		}

		public static QueueMatchmakingRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueMatchmakingRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static QueueMatchmakingRequest Deserialize(Stream stream, QueueMatchmakingRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static QueueMatchmakingRequest DeserializeLengthDelimited(Stream stream)
		{
			QueueMatchmakingRequest queueMatchmakingRequest = new QueueMatchmakingRequest();
			DeserializeLengthDelimited(stream, queueMatchmakingRequest);
			return queueMatchmakingRequest;
		}

		public static QueueMatchmakingRequest DeserializeLengthDelimited(Stream stream, QueueMatchmakingRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static QueueMatchmakingRequest Deserialize(Stream stream, QueueMatchmakingRequest instance, long limit)
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
						instance.Options = GameMatchmakingOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameMatchmakingOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
					continue;
				case 18:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
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

		public static void Serialize(Stream stream, QueueMatchmakingRequest instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				GameMatchmakingOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
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
			if (HasChannelId)
			{
				num++;
				uint serializedSize2 = ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
