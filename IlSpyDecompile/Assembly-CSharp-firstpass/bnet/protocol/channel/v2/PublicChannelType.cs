using System.IO;
using System.Text;

namespace bnet.protocol.channel.v2
{
	public class PublicChannelType : IProtoBuf
	{
		public bool HasType;

		private UniqueChannelType _Type;

		public bool HasName;

		private string _Name;

		public bool HasIdentity;

		private string _Identity;

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

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
				HasName = value != null;
			}
		}

		public string Identity
		{
			get
			{
				return _Identity;
			}
			set
			{
				_Identity = value;
				HasIdentity = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetType(UniqueChannelType val)
		{
			Type = val;
		}

		public void SetName(string val)
		{
			Name = val;
		}

		public void SetIdentity(string val)
		{
			Identity = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasType)
			{
				num ^= Type.GetHashCode();
			}
			if (HasName)
			{
				num ^= Name.GetHashCode();
			}
			if (HasIdentity)
			{
				num ^= Identity.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PublicChannelType publicChannelType = obj as PublicChannelType;
			if (publicChannelType == null)
			{
				return false;
			}
			if (HasType != publicChannelType.HasType || (HasType && !Type.Equals(publicChannelType.Type)))
			{
				return false;
			}
			if (HasName != publicChannelType.HasName || (HasName && !Name.Equals(publicChannelType.Name)))
			{
				return false;
			}
			if (HasIdentity != publicChannelType.HasIdentity || (HasIdentity && !Identity.Equals(publicChannelType.Identity)))
			{
				return false;
			}
			return true;
		}

		public static PublicChannelType ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PublicChannelType>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PublicChannelType Deserialize(Stream stream, PublicChannelType instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PublicChannelType DeserializeLengthDelimited(Stream stream)
		{
			PublicChannelType publicChannelType = new PublicChannelType();
			DeserializeLengthDelimited(stream, publicChannelType);
			return publicChannelType;
		}

		public static PublicChannelType DeserializeLengthDelimited(Stream stream, PublicChannelType instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PublicChannelType Deserialize(Stream stream, PublicChannelType instance, long limit)
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
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.Identity = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, PublicChannelType instance)
		{
			if (instance.HasType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Type.GetSerializedSize());
				UniqueChannelType.Serialize(stream, instance.Type);
			}
			if (instance.HasName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasIdentity)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Identity));
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
			if (HasName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasIdentity)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Identity);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
