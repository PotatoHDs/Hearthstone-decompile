using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	public class UpdateChannelStateNotification : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasChannelId;

		private ChannelId _ChannelId;

		public bool HasSubscriberId;

		private SubscriberId _SubscriberId;

		public bool HasPresenceSubscriberId;

		private AccountId _PresenceSubscriberId;

		public EntityId AgentId
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

		public ChannelState StateChange { get; set; }

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

		public SubscriberId SubscriberId
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

		public AccountId PresenceSubscriberId
		{
			get
			{
				return _PresenceSubscriberId;
			}
			set
			{
				_PresenceSubscriberId = value;
				HasPresenceSubscriberId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetStateChange(ChannelState val)
		{
			StateChange = val;
		}

		public void SetChannelId(ChannelId val)
		{
			ChannelId = val;
		}

		public void SetSubscriberId(SubscriberId val)
		{
			SubscriberId = val;
		}

		public void SetPresenceSubscriberId(AccountId val)
		{
			PresenceSubscriberId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			num ^= StateChange.GetHashCode();
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			if (HasSubscriberId)
			{
				num ^= SubscriberId.GetHashCode();
			}
			if (HasPresenceSubscriberId)
			{
				num ^= PresenceSubscriberId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateChannelStateNotification updateChannelStateNotification = obj as UpdateChannelStateNotification;
			if (updateChannelStateNotification == null)
			{
				return false;
			}
			if (HasAgentId != updateChannelStateNotification.HasAgentId || (HasAgentId && !AgentId.Equals(updateChannelStateNotification.AgentId)))
			{
				return false;
			}
			if (!StateChange.Equals(updateChannelStateNotification.StateChange))
			{
				return false;
			}
			if (HasChannelId != updateChannelStateNotification.HasChannelId || (HasChannelId && !ChannelId.Equals(updateChannelStateNotification.ChannelId)))
			{
				return false;
			}
			if (HasSubscriberId != updateChannelStateNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(updateChannelStateNotification.SubscriberId)))
			{
				return false;
			}
			if (HasPresenceSubscriberId != updateChannelStateNotification.HasPresenceSubscriberId || (HasPresenceSubscriberId && !PresenceSubscriberId.Equals(updateChannelStateNotification.PresenceSubscriberId)))
			{
				return false;
			}
			return true;
		}

		public static UpdateChannelStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateChannelStateNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateChannelStateNotification Deserialize(Stream stream, UpdateChannelStateNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateChannelStateNotification DeserializeLengthDelimited(Stream stream)
		{
			UpdateChannelStateNotification updateChannelStateNotification = new UpdateChannelStateNotification();
			DeserializeLengthDelimited(stream, updateChannelStateNotification);
			return updateChannelStateNotification;
		}

		public static UpdateChannelStateNotification DeserializeLengthDelimited(Stream stream, UpdateChannelStateNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateChannelStateNotification Deserialize(Stream stream, UpdateChannelStateNotification instance, long limit)
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
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 18:
					if (instance.StateChange == null)
					{
						instance.StateChange = ChannelState.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelState.DeserializeLengthDelimited(stream, instance.StateChange);
					}
					continue;
				case 26:
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
					if (instance.SubscriberId == null)
					{
						instance.SubscriberId = SubscriberId.DeserializeLengthDelimited(stream);
					}
					else
					{
						SubscriberId.DeserializeLengthDelimited(stream, instance.SubscriberId);
					}
					continue;
				case 42:
					if (instance.PresenceSubscriberId == null)
					{
						instance.PresenceSubscriberId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.PresenceSubscriberId);
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

		public static void Serialize(Stream stream, UpdateChannelStateNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.StateChange == null)
			{
				throw new ArgumentNullException("StateChange", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.StateChange.GetSerializedSize());
			ChannelState.Serialize(stream, instance.StateChange);
			if (instance.HasChannelId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				SubscriberId.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasPresenceSubscriberId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.PresenceSubscriberId.GetSerializedSize());
				AccountId.Serialize(stream, instance.PresenceSubscriberId);
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
			uint serializedSize2 = StateChange.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (HasChannelId)
			{
				num++;
				uint serializedSize3 = ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasSubscriberId)
			{
				num++;
				uint serializedSize4 = SubscriberId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasPresenceSubscriberId)
			{
				num++;
				uint serializedSize5 = PresenceSubscriberId.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			return num + 1;
		}
	}
}
