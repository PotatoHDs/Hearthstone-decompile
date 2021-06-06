using System.IO;

namespace bnet.protocol.channel.v2
{
	public class GetPublicChannelTypesOptions : IProtoBuf
	{
		public bool HasType;

		private UniqueChannelType _Type;

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

		public bool IsInitialized => true;

		public void SetType(UniqueChannelType val)
		{
			Type = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasType)
			{
				num ^= Type.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetPublicChannelTypesOptions getPublicChannelTypesOptions = obj as GetPublicChannelTypesOptions;
			if (getPublicChannelTypesOptions == null)
			{
				return false;
			}
			if (HasType != getPublicChannelTypesOptions.HasType || (HasType && !Type.Equals(getPublicChannelTypesOptions.Type)))
			{
				return false;
			}
			return true;
		}

		public static GetPublicChannelTypesOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetPublicChannelTypesOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetPublicChannelTypesOptions Deserialize(Stream stream, GetPublicChannelTypesOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetPublicChannelTypesOptions DeserializeLengthDelimited(Stream stream)
		{
			GetPublicChannelTypesOptions getPublicChannelTypesOptions = new GetPublicChannelTypesOptions();
			DeserializeLengthDelimited(stream, getPublicChannelTypesOptions);
			return getPublicChannelTypesOptions;
		}

		public static GetPublicChannelTypesOptions DeserializeLengthDelimited(Stream stream, GetPublicChannelTypesOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetPublicChannelTypesOptions Deserialize(Stream stream, GetPublicChannelTypesOptions instance, long limit)
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

		public static void Serialize(Stream stream, GetPublicChannelTypesOptions instance)
		{
			if (instance.HasType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Type.GetSerializedSize());
				UniqueChannelType.Serialize(stream, instance.Type);
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
			return num;
		}
	}
}
