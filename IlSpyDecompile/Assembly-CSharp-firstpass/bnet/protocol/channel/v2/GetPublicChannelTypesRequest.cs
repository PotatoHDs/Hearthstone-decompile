using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	public class GetPublicChannelTypesRequest : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasOptions;

		private GetPublicChannelTypesOptions _Options;

		public bool HasContinuation;

		private ulong _Continuation;

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

		public GetPublicChannelTypesOptions Options
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

		public ulong Continuation
		{
			get
			{
				return _Continuation;
			}
			set
			{
				_Continuation = value;
				HasContinuation = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentId(GameAccountHandle val)
		{
			AgentId = val;
		}

		public void SetOptions(GetPublicChannelTypesOptions val)
		{
			Options = val;
		}

		public void SetContinuation(ulong val)
		{
			Continuation = val;
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
			if (HasContinuation)
			{
				num ^= Continuation.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetPublicChannelTypesRequest getPublicChannelTypesRequest = obj as GetPublicChannelTypesRequest;
			if (getPublicChannelTypesRequest == null)
			{
				return false;
			}
			if (HasAgentId != getPublicChannelTypesRequest.HasAgentId || (HasAgentId && !AgentId.Equals(getPublicChannelTypesRequest.AgentId)))
			{
				return false;
			}
			if (HasOptions != getPublicChannelTypesRequest.HasOptions || (HasOptions && !Options.Equals(getPublicChannelTypesRequest.Options)))
			{
				return false;
			}
			if (HasContinuation != getPublicChannelTypesRequest.HasContinuation || (HasContinuation && !Continuation.Equals(getPublicChannelTypesRequest.Continuation)))
			{
				return false;
			}
			return true;
		}

		public static GetPublicChannelTypesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetPublicChannelTypesRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetPublicChannelTypesRequest Deserialize(Stream stream, GetPublicChannelTypesRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetPublicChannelTypesRequest DeserializeLengthDelimited(Stream stream)
		{
			GetPublicChannelTypesRequest getPublicChannelTypesRequest = new GetPublicChannelTypesRequest();
			DeserializeLengthDelimited(stream, getPublicChannelTypesRequest);
			return getPublicChannelTypesRequest;
		}

		public static GetPublicChannelTypesRequest DeserializeLengthDelimited(Stream stream, GetPublicChannelTypesRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetPublicChannelTypesRequest Deserialize(Stream stream, GetPublicChannelTypesRequest instance, long limit)
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
						instance.Options = GetPublicChannelTypesOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						GetPublicChannelTypesOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
					continue;
				case 24:
					instance.Continuation = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GetPublicChannelTypesRequest instance)
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
				GetPublicChannelTypesOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
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
			if (HasContinuation)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Continuation);
			}
			return num;
		}
	}
}
