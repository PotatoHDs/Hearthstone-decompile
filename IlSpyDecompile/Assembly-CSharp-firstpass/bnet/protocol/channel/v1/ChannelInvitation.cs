using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class ChannelInvitation : IProtoBuf
	{
		public bool HasReserved;

		private bool _Reserved;

		public bool HasRejoin;

		private bool _Rejoin;

		public ChannelDescription ChannelDescription { get; set; }

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

		public void SetChannelDescription(ChannelDescription val)
		{
			ChannelDescription = val;
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
			hashCode ^= ChannelDescription.GetHashCode();
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
			ChannelInvitation channelInvitation = obj as ChannelInvitation;
			if (channelInvitation == null)
			{
				return false;
			}
			if (!ChannelDescription.Equals(channelInvitation.ChannelDescription))
			{
				return false;
			}
			if (HasReserved != channelInvitation.HasReserved || (HasReserved && !Reserved.Equals(channelInvitation.Reserved)))
			{
				return false;
			}
			if (HasRejoin != channelInvitation.HasRejoin || (HasRejoin && !Rejoin.Equals(channelInvitation.Rejoin)))
			{
				return false;
			}
			if (!ServiceType.Equals(channelInvitation.ServiceType))
			{
				return false;
			}
			return true;
		}

		public static ChannelInvitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelInvitation>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelInvitation Deserialize(Stream stream, ChannelInvitation instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelInvitation DeserializeLengthDelimited(Stream stream)
		{
			ChannelInvitation channelInvitation = new ChannelInvitation();
			DeserializeLengthDelimited(stream, channelInvitation);
			return channelInvitation;
		}

		public static ChannelInvitation DeserializeLengthDelimited(Stream stream, ChannelInvitation instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelInvitation Deserialize(Stream stream, ChannelInvitation instance, long limit)
		{
			instance.Reserved = false;
			instance.Rejoin = false;
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
					if (instance.ChannelDescription == null)
					{
						instance.ChannelDescription = ChannelDescription.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelDescription.DeserializeLengthDelimited(stream, instance.ChannelDescription);
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

		public static void Serialize(Stream stream, ChannelInvitation instance)
		{
			if (instance.ChannelDescription == null)
			{
				throw new ArgumentNullException("ChannelDescription", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ChannelDescription.GetSerializedSize());
			ChannelDescription.Serialize(stream, instance.ChannelDescription);
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
			uint serializedSize = ChannelDescription.GetSerializedSize();
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
