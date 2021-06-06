using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class ChannelInvitationParams : IProtoBuf
	{
		public bool HasReserved;

		private bool _Reserved;

		public bool HasRejoin;

		private bool _Rejoin;

		public EntityId ChannelId { get; set; }

		public bool Reserved
		{
			get
			{
				return _Reserved;
			}
			set
			{
				_Reserved = value;
				HasReserved = true;
			}
		}

		public bool Rejoin
		{
			get
			{
				return _Rejoin;
			}
			set
			{
				_Rejoin = value;
				HasRejoin = true;
			}
		}

		public uint ServiceType { get; set; }

		public bool IsInitialized => true;

		public void SetChannelId(EntityId val)
		{
			ChannelId = val;
		}

		public void SetReserved(bool val)
		{
			Reserved = val;
		}

		public void SetRejoin(bool val)
		{
			Rejoin = val;
		}

		public void SetServiceType(uint val)
		{
			ServiceType = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ChannelId.GetHashCode();
			if (HasReserved)
			{
				hashCode ^= Reserved.GetHashCode();
			}
			if (HasRejoin)
			{
				hashCode ^= Rejoin.GetHashCode();
			}
			return hashCode ^ ServiceType.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ChannelInvitationParams channelInvitationParams = obj as ChannelInvitationParams;
			if (channelInvitationParams == null)
			{
				return false;
			}
			if (!ChannelId.Equals(channelInvitationParams.ChannelId))
			{
				return false;
			}
			if (HasReserved != channelInvitationParams.HasReserved || (HasReserved && !Reserved.Equals(channelInvitationParams.Reserved)))
			{
				return false;
			}
			if (HasRejoin != channelInvitationParams.HasRejoin || (HasRejoin && !Rejoin.Equals(channelInvitationParams.Rejoin)))
			{
				return false;
			}
			if (!ServiceType.Equals(channelInvitationParams.ServiceType))
			{
				return false;
			}
			return true;
		}

		public static ChannelInvitationParams ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelInvitationParams>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelInvitationParams Deserialize(Stream stream, ChannelInvitationParams instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelInvitationParams DeserializeLengthDelimited(Stream stream)
		{
			ChannelInvitationParams channelInvitationParams = new ChannelInvitationParams();
			DeserializeLengthDelimited(stream, channelInvitationParams);
			return channelInvitationParams;
		}

		public static ChannelInvitationParams DeserializeLengthDelimited(Stream stream, ChannelInvitationParams instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelInvitationParams Deserialize(Stream stream, ChannelInvitationParams instance, long limit)
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
					instance.Reserved = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.Rejoin = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.ServiceType = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, ChannelInvitationParams instance)
		{
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
			if (instance.HasReserved)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Reserved);
			}
			if (instance.HasRejoin)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Rejoin);
			}
			stream.WriteByte(32);
			ProtocolParser.WriteUInt32(stream, instance.ServiceType);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = ChannelId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasReserved)
			{
				num++;
				num++;
			}
			if (HasRejoin)
			{
				num++;
				num++;
			}
			num += ProtocolParser.SizeOfUInt32(ServiceType);
			return num + 2;
		}
	}
}
