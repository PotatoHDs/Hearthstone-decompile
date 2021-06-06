using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	public class SubscribeChannelRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasAgentIdentity;

		private bnet.protocol.account.v1.Identity _AgentIdentity;

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

		public EntityId ChannelId { get; set; }

		public ulong ObjectId { get; set; }

		public bnet.protocol.account.v1.Identity AgentIdentity
		{
			get
			{
				return _AgentIdentity;
			}
			set
			{
				_AgentIdentity = value;
				HasAgentIdentity = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetChannelId(EntityId val)
		{
			ChannelId = val;
		}

		public void SetObjectId(ulong val)
		{
			ObjectId = val;
		}

		public void SetAgentIdentity(bnet.protocol.account.v1.Identity val)
		{
			AgentIdentity = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			num ^= ChannelId.GetHashCode();
			num ^= ObjectId.GetHashCode();
			if (HasAgentIdentity)
			{
				num ^= AgentIdentity.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscribeChannelRequest subscribeChannelRequest = obj as SubscribeChannelRequest;
			if (subscribeChannelRequest == null)
			{
				return false;
			}
			if (HasAgentId != subscribeChannelRequest.HasAgentId || (HasAgentId && !AgentId.Equals(subscribeChannelRequest.AgentId)))
			{
				return false;
			}
			if (!ChannelId.Equals(subscribeChannelRequest.ChannelId))
			{
				return false;
			}
			if (!ObjectId.Equals(subscribeChannelRequest.ObjectId))
			{
				return false;
			}
			if (HasAgentIdentity != subscribeChannelRequest.HasAgentIdentity || (HasAgentIdentity && !AgentIdentity.Equals(subscribeChannelRequest.AgentIdentity)))
			{
				return false;
			}
			return true;
		}

		public static SubscribeChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeChannelRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscribeChannelRequest Deserialize(Stream stream, SubscribeChannelRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscribeChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeChannelRequest subscribeChannelRequest = new SubscribeChannelRequest();
			DeserializeLengthDelimited(stream, subscribeChannelRequest);
			return subscribeChannelRequest;
		}

		public static SubscribeChannelRequest DeserializeLengthDelimited(Stream stream, SubscribeChannelRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscribeChannelRequest Deserialize(Stream stream, SubscribeChannelRequest instance, long limit)
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
					if (instance.ChannelId == null)
					{
						instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 24:
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					if (instance.AgentIdentity == null)
					{
						instance.AgentIdentity = bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream, instance.AgentIdentity);
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

		public static void Serialize(Stream stream, SubscribeChannelRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			if (instance.HasAgentIdentity)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.AgentIdentity.GetSerializedSize());
				bnet.protocol.account.v1.Identity.Serialize(stream, instance.AgentIdentity);
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
			uint serializedSize2 = ChannelId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			num += ProtocolParser.SizeOfUInt64(ObjectId);
			if (HasAgentIdentity)
			{
				num++;
				uint serializedSize3 = AgentIdentity.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 2;
		}
	}
}
