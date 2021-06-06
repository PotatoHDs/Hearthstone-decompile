using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	public class GetJoinTokenRequest : IProtoBuf
	{
		public bool HasChannelId;

		private ChannelId _ChannelId;

		public bool HasMemberId;

		private GameAccountHandle _MemberId;

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

		public GameAccountHandle MemberId
		{
			get
			{
				return _MemberId;
			}
			set
			{
				_MemberId = value;
				HasMemberId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetChannelId(ChannelId val)
		{
			ChannelId = val;
		}

		public void SetMemberId(GameAccountHandle val)
		{
			MemberId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			if (HasMemberId)
			{
				num ^= MemberId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetJoinTokenRequest getJoinTokenRequest = obj as GetJoinTokenRequest;
			if (getJoinTokenRequest == null)
			{
				return false;
			}
			if (HasChannelId != getJoinTokenRequest.HasChannelId || (HasChannelId && !ChannelId.Equals(getJoinTokenRequest.ChannelId)))
			{
				return false;
			}
			if (HasMemberId != getJoinTokenRequest.HasMemberId || (HasMemberId && !MemberId.Equals(getJoinTokenRequest.MemberId)))
			{
				return false;
			}
			return true;
		}

		public static GetJoinTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetJoinTokenRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetJoinTokenRequest Deserialize(Stream stream, GetJoinTokenRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetJoinTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			GetJoinTokenRequest getJoinTokenRequest = new GetJoinTokenRequest();
			DeserializeLengthDelimited(stream, getJoinTokenRequest);
			return getJoinTokenRequest;
		}

		public static GetJoinTokenRequest DeserializeLengthDelimited(Stream stream, GetJoinTokenRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetJoinTokenRequest Deserialize(Stream stream, GetJoinTokenRequest instance, long limit)
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
				case 34:
					if (instance.MemberId == null)
					{
						instance.MemberId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.MemberId);
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

		public static void Serialize(Stream stream, GetJoinTokenRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasMemberId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.MemberId);
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
			if (HasMemberId)
			{
				num++;
				uint serializedSize2 = MemberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
