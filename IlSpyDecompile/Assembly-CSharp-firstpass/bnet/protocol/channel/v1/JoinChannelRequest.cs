using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	public class JoinChannelRequest : IProtoBuf
	{
		public bool HasAgentIdentity;

		private bnet.protocol.account.v1.Identity _AgentIdentity;

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

		public EntityId ChannelId { get; set; }

		public ulong ObjectId { get; set; }

		public bool IsInitialized => true;

		public void SetAgentIdentity(bnet.protocol.account.v1.Identity val)
		{
			AgentIdentity = val;
		}

		public void SetChannelId(EntityId val)
		{
			ChannelId = val;
		}

		public void SetObjectId(ulong val)
		{
			ObjectId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentIdentity)
			{
				num ^= AgentIdentity.GetHashCode();
			}
			num ^= ChannelId.GetHashCode();
			return num ^ ObjectId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			JoinChannelRequest joinChannelRequest = obj as JoinChannelRequest;
			if (joinChannelRequest == null)
			{
				return false;
			}
			if (HasAgentIdentity != joinChannelRequest.HasAgentIdentity || (HasAgentIdentity && !AgentIdentity.Equals(joinChannelRequest.AgentIdentity)))
			{
				return false;
			}
			if (!ChannelId.Equals(joinChannelRequest.ChannelId))
			{
				return false;
			}
			if (!ObjectId.Equals(joinChannelRequest.ObjectId))
			{
				return false;
			}
			return true;
		}

		public static JoinChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinChannelRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static JoinChannelRequest Deserialize(Stream stream, JoinChannelRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static JoinChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			JoinChannelRequest joinChannelRequest = new JoinChannelRequest();
			DeserializeLengthDelimited(stream, joinChannelRequest);
			return joinChannelRequest;
		}

		public static JoinChannelRequest DeserializeLengthDelimited(Stream stream, JoinChannelRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static JoinChannelRequest Deserialize(Stream stream, JoinChannelRequest instance, long limit)
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
					if (instance.AgentIdentity == null)
					{
						instance.AgentIdentity = bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream, instance.AgentIdentity);
					}
					continue;
				case 26:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 32:
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, JoinChannelRequest instance)
		{
			if (instance.HasAgentIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentIdentity.GetSerializedSize());
				bnet.protocol.account.v1.Identity.Serialize(stream, instance.AgentIdentity);
			}
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAgentIdentity)
			{
				num++;
				uint serializedSize = AgentIdentity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = ChannelId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			num += ProtocolParser.SizeOfUInt64(ObjectId);
			return num + 2;
		}
	}
}
