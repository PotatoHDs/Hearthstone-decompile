using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class ChannelDescription : IProtoBuf
	{
		public bool HasCurrentMembers;

		private uint _CurrentMembers;

		public bool HasState;

		private ChannelState _State;

		public EntityId ChannelId { get; set; }

		public uint CurrentMembers
		{
			get
			{
				return _CurrentMembers;
			}
			set
			{
				_CurrentMembers = value;
				HasCurrentMembers = true;
			}
		}

		public ChannelState State
		{
			get
			{
				return _State;
			}
			set
			{
				_State = value;
				HasState = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetChannelId(EntityId val)
		{
			ChannelId = val;
		}

		public void SetCurrentMembers(uint val)
		{
			CurrentMembers = val;
		}

		public void SetState(ChannelState val)
		{
			State = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ChannelId.GetHashCode();
			if (HasCurrentMembers)
			{
				hashCode ^= CurrentMembers.GetHashCode();
			}
			if (HasState)
			{
				hashCode ^= State.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ChannelDescription channelDescription = obj as ChannelDescription;
			if (channelDescription == null)
			{
				return false;
			}
			if (!ChannelId.Equals(channelDescription.ChannelId))
			{
				return false;
			}
			if (HasCurrentMembers != channelDescription.HasCurrentMembers || (HasCurrentMembers && !CurrentMembers.Equals(channelDescription.CurrentMembers)))
			{
				return false;
			}
			if (HasState != channelDescription.HasState || (HasState && !State.Equals(channelDescription.State)))
			{
				return false;
			}
			return true;
		}

		public static ChannelDescription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelDescription>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelDescription Deserialize(Stream stream, ChannelDescription instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelDescription DeserializeLengthDelimited(Stream stream)
		{
			ChannelDescription channelDescription = new ChannelDescription();
			DeserializeLengthDelimited(stream, channelDescription);
			return channelDescription;
		}

		public static ChannelDescription DeserializeLengthDelimited(Stream stream, ChannelDescription instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelDescription Deserialize(Stream stream, ChannelDescription instance, long limit)
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
						instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 16:
					instance.CurrentMembers = ProtocolParser.ReadUInt32(stream);
					continue;
				case 26:
					if (instance.State == null)
					{
						instance.State = ChannelState.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelState.DeserializeLengthDelimited(stream, instance.State);
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

		public static void Serialize(Stream stream, ChannelDescription instance)
		{
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
			if (instance.HasCurrentMembers)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.CurrentMembers);
			}
			if (instance.HasState)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				ChannelState.Serialize(stream, instance.State);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = ChannelId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasCurrentMembers)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(CurrentMembers);
			}
			if (HasState)
			{
				num++;
				uint serializedSize2 = State.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1;
		}
	}
}
