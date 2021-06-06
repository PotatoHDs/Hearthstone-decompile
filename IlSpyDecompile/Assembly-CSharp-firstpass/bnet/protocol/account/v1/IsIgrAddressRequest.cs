using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	public class IsIgrAddressRequest : IProtoBuf
	{
		public bool HasClientAddress;

		private string _ClientAddress;

		public bool HasRegion;

		private uint _Region;

		public string ClientAddress
		{
			get
			{
				return _ClientAddress;
			}
			set
			{
				_ClientAddress = value;
				HasClientAddress = value != null;
			}
		}

		public uint Region
		{
			get
			{
				return _Region;
			}
			set
			{
				_Region = value;
				HasRegion = true;
			}
		}

		public bool IsInitialized => true;

		public void SetClientAddress(string val)
		{
			ClientAddress = val;
		}

		public void SetRegion(uint val)
		{
			Region = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasClientAddress)
			{
				num ^= ClientAddress.GetHashCode();
			}
			if (HasRegion)
			{
				num ^= Region.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			IsIgrAddressRequest isIgrAddressRequest = obj as IsIgrAddressRequest;
			if (isIgrAddressRequest == null)
			{
				return false;
			}
			if (HasClientAddress != isIgrAddressRequest.HasClientAddress || (HasClientAddress && !ClientAddress.Equals(isIgrAddressRequest.ClientAddress)))
			{
				return false;
			}
			if (HasRegion != isIgrAddressRequest.HasRegion || (HasRegion && !Region.Equals(isIgrAddressRequest.Region)))
			{
				return false;
			}
			return true;
		}

		public static IsIgrAddressRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<IsIgrAddressRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static IsIgrAddressRequest Deserialize(Stream stream, IsIgrAddressRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static IsIgrAddressRequest DeserializeLengthDelimited(Stream stream)
		{
			IsIgrAddressRequest isIgrAddressRequest = new IsIgrAddressRequest();
			DeserializeLengthDelimited(stream, isIgrAddressRequest);
			return isIgrAddressRequest;
		}

		public static IsIgrAddressRequest DeserializeLengthDelimited(Stream stream, IsIgrAddressRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static IsIgrAddressRequest Deserialize(Stream stream, IsIgrAddressRequest instance, long limit)
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
					instance.ClientAddress = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.Region = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, IsIgrAddressRequest instance)
		{
			if (instance.HasClientAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientAddress));
			}
			if (instance.HasRegion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Region);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasClientAddress)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ClientAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasRegion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Region);
			}
			return num;
		}
	}
}
