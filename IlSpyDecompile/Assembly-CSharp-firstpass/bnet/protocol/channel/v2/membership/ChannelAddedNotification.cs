using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2.membership
{
	public class ChannelAddedNotification : IProtoBuf
	{
		public bool HasAgentId;

		private GameAccountHandle _AgentId;

		public bool HasSubscriberId;

		private GameAccountHandle _SubscriberId;

		public bool HasMembership;

		private ChannelDescription _Membership;

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

		public GameAccountHandle SubscriberId
		{
			get
			{
				return _SubscriberId;
			}
			set
			{
				_SubscriberId = value;
				HasSubscriberId = value != null;
			}
		}

		public ChannelDescription Membership
		{
			get
			{
				return _Membership;
			}
			set
			{
				_Membership = value;
				HasMembership = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentId(GameAccountHandle val)
		{
			AgentId = val;
		}

		public void SetSubscriberId(GameAccountHandle val)
		{
			SubscriberId = val;
		}

		public void SetMembership(ChannelDescription val)
		{
			Membership = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			if (HasSubscriberId)
			{
				num ^= SubscriberId.GetHashCode();
			}
			if (HasMembership)
			{
				num ^= Membership.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelAddedNotification channelAddedNotification = obj as ChannelAddedNotification;
			if (channelAddedNotification == null)
			{
				return false;
			}
			if (HasAgentId != channelAddedNotification.HasAgentId || (HasAgentId && !AgentId.Equals(channelAddedNotification.AgentId)))
			{
				return false;
			}
			if (HasSubscriberId != channelAddedNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(channelAddedNotification.SubscriberId)))
			{
				return false;
			}
			if (HasMembership != channelAddedNotification.HasMembership || (HasMembership && !Membership.Equals(channelAddedNotification.Membership)))
			{
				return false;
			}
			return true;
		}

		public static ChannelAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelAddedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelAddedNotification Deserialize(Stream stream, ChannelAddedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			ChannelAddedNotification channelAddedNotification = new ChannelAddedNotification();
			DeserializeLengthDelimited(stream, channelAddedNotification);
			return channelAddedNotification;
		}

		public static ChannelAddedNotification DeserializeLengthDelimited(Stream stream, ChannelAddedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelAddedNotification Deserialize(Stream stream, ChannelAddedNotification instance, long limit)
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
					if (instance.SubscriberId == null)
					{
						instance.SubscriberId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.SubscriberId);
					}
					continue;
				case 26:
					if (instance.Membership == null)
					{
						instance.Membership = ChannelDescription.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelDescription.DeserializeLengthDelimited(stream, instance.Membership);
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

		public static void Serialize(Stream stream, ChannelAddedNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasMembership)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Membership.GetSerializedSize());
				ChannelDescription.Serialize(stream, instance.Membership);
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
			if (HasSubscriberId)
			{
				num++;
				uint serializedSize2 = SubscriberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasMembership)
			{
				num++;
				uint serializedSize3 = Membership.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
