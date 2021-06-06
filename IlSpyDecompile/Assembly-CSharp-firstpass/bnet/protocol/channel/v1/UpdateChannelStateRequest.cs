using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class UpdateChannelStateRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

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

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetStateChange(ChannelState val)
		{
			StateChange = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			return num ^ StateChange.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			UpdateChannelStateRequest updateChannelStateRequest = obj as UpdateChannelStateRequest;
			if (updateChannelStateRequest == null)
			{
				return false;
			}
			if (HasAgentId != updateChannelStateRequest.HasAgentId || (HasAgentId && !AgentId.Equals(updateChannelStateRequest.AgentId)))
			{
				return false;
			}
			if (!StateChange.Equals(updateChannelStateRequest.StateChange))
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
			return num + 1;
		}
	}
}
