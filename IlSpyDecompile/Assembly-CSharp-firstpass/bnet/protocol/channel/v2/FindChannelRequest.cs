using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	public class FindChannelRequest : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasOptions;

		private FindChannelOptions _Options;

		public GameAccountHandle AgentId
		{
			get
			{
				return _AgentId;
			}
			set
			{
				_AgentId = value;
				HasAgentId = value != null;
			}
		}

		public FindChannelOptions Options
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

		public void SetAgentId(GameAccountHandle val)
		{
			AgentId = val;
		}

		public void SetOptions(FindChannelOptions val)
		{
			Options = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			if (HasOptions)
			{
				num ^= Options.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FindChannelRequest findChannelRequest = obj as FindChannelRequest;
			if (findChannelRequest == null)
			{
				return false;
			}
			if (HasAgentId != findChannelRequest.HasAgentId || (HasAgentId && !AgentId.Equals(findChannelRequest.AgentId)))
			{
				return false;
			}
			if (HasOptions != findChannelRequest.HasOptions || (HasOptions && !Options.Equals(findChannelRequest.Options)))
			{
				return false;
			}
			return true;
		}

		public static FindChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FindChannelRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FindChannelRequest Deserialize(Stream stream, FindChannelRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FindChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			FindChannelRequest findChannelRequest = new FindChannelRequest();
			DeserializeLengthDelimited(stream, findChannelRequest);
			return findChannelRequest;
		}

		public static FindChannelRequest DeserializeLengthDelimited(Stream stream, FindChannelRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FindChannelRequest Deserialize(Stream stream, FindChannelRequest instance, long limit)
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
					if (instance.AgentId == null)
					{
						instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 18:
					if (instance.Options == null)
					{
						instance.Options = FindChannelOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						FindChannelOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		public static void Serialize(Stream stream, FindChannelRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				FindChannelOptions.Serialize(stream, instance.Options);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAgentId)
			{
				num++;
				uint serializedSize = AgentId.GetSerializedSize();
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
