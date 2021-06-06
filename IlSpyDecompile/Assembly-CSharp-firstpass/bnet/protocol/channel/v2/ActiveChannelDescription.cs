using System.IO;

namespace bnet.protocol.channel.v2
{
	public class ActiveChannelDescription : IProtoBuf
	{
		public bool HasType;

		private UniqueChannelType _Type;

		public bool HasChannelId;

		private ChannelId _ChannelId;

		public UniqueChannelType Type
		{
			get
			{
				return _Type;
			}
			set
			{
				_Type = value;
				HasType = value != null;
			}
		}

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

		public bool IsInitialized => true;

		public void SetType(UniqueChannelType val)
		{
			Type = val;
		}

		public void SetChannelId(ChannelId val)
		{
			ChannelId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasType)
			{
				num ^= Type.GetHashCode();
			}
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ActiveChannelDescription activeChannelDescription = obj as ActiveChannelDescription;
			if (activeChannelDescription == null)
			{
				return false;
			}
			if (HasType != activeChannelDescription.HasType || (HasType && !Type.Equals(activeChannelDescription.Type)))
			{
				return false;
			}
			if (HasChannelId != activeChannelDescription.HasChannelId || (HasChannelId && !ChannelId.Equals(activeChannelDescription.ChannelId)))
			{
				return false;
			}
			return true;
		}

		public static ActiveChannelDescription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ActiveChannelDescription>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ActiveChannelDescription Deserialize(Stream stream, ActiveChannelDescription instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ActiveChannelDescription DeserializeLengthDelimited(Stream stream)
		{
			ActiveChannelDescription activeChannelDescription = new ActiveChannelDescription();
			DeserializeLengthDelimited(stream, activeChannelDescription);
			return activeChannelDescription;
		}

		public static ActiveChannelDescription DeserializeLengthDelimited(Stream stream, ActiveChannelDescription instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ActiveChannelDescription Deserialize(Stream stream, ActiveChannelDescription instance, long limit)
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
					if (instance.Type == null)
					{
						instance.Type = UniqueChannelType.DeserializeLengthDelimited(stream);
					}
					else
					{
						UniqueChannelType.DeserializeLengthDelimited(stream, instance.Type);
					}
					continue;
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

		public static void Serialize(Stream stream, ActiveChannelDescription instance)
		{
			if (instance.HasType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Type.GetSerializedSize());
				UniqueChannelType.Serialize(stream, instance.Type);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasType)
			{
				num++;
				uint serializedSize = Type.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasChannelId)
			{
				num++;
				uint serializedSize2 = ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
