using System.IO;

namespace bnet.protocol.channel.v2
{
	public class ChannelSlot : IProtoBuf
	{
		public bool HasReserved;

		private bool _Reserved;

		public bool HasRejoin;

		private bool _Rejoin;

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

		public bool IsInitialized => true;

		public void SetReserved(bool val)
		{
			Reserved = val;
		}

		public void SetRejoin(bool val)
		{
			Rejoin = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasReserved)
			{
				num ^= Reserved.GetHashCode();
			}
			if (HasRejoin)
			{
				num ^= Rejoin.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelSlot channelSlot = obj as ChannelSlot;
			if (channelSlot == null)
			{
				return false;
			}
			if (HasReserved != channelSlot.HasReserved || (HasReserved && !Reserved.Equals(channelSlot.Reserved)))
			{
				return false;
			}
			if (HasRejoin != channelSlot.HasRejoin || (HasRejoin && !Rejoin.Equals(channelSlot.Rejoin)))
			{
				return false;
			}
			return true;
		}

		public static ChannelSlot ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelSlot>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelSlot Deserialize(Stream stream, ChannelSlot instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelSlot DeserializeLengthDelimited(Stream stream)
		{
			ChannelSlot channelSlot = new ChannelSlot();
			DeserializeLengthDelimited(stream, channelSlot);
			return channelSlot;
		}

		public static ChannelSlot DeserializeLengthDelimited(Stream stream, ChannelSlot instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelSlot Deserialize(Stream stream, ChannelSlot instance, long limit)
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
				case 8:
					instance.Reserved = ProtocolParser.ReadBool(stream);
					continue;
				case 16:
					instance.Rejoin = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, ChannelSlot instance)
		{
			if (instance.HasReserved)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.Reserved);
			}
			if (instance.HasRejoin)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Rejoin);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
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
			return num;
		}
	}
}
