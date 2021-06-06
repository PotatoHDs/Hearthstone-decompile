using System;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	public class Address : IProtoBuf
	{
		public bool HasPort;

		private uint _Port;

		public string Address_ { get; set; }

		public uint Port
		{
			get
			{
				return _Port;
			}
			set
			{
				_Port = value;
				HasPort = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAddress_(string val)
		{
			Address_ = val;
		}

		public void SetPort(uint val)
		{
			Port = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Address_.GetHashCode();
			if (HasPort)
			{
				hashCode ^= Port.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			Address address = obj as Address;
			if (address == null)
			{
				return false;
			}
			if (!Address_.Equals(address.Address_))
			{
				return false;
			}
			if (HasPort != address.HasPort || (HasPort && !Port.Equals(address.Port)))
			{
				return false;
			}
			return true;
		}

		public static Address ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Address>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Address Deserialize(Stream stream, Address instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Address DeserializeLengthDelimited(Stream stream)
		{
			Address address = new Address();
			DeserializeLengthDelimited(stream, address);
			return address;
		}

		public static Address DeserializeLengthDelimited(Stream stream, Address instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Address Deserialize(Stream stream, Address instance, long limit)
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
					instance.Address_ = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.Port = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, Address instance)
		{
			if (instance.Address_ == null)
			{
				throw new ArgumentNullException("Address_", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Address_));
			if (instance.HasPort)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Port);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Address_);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (HasPort)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Port);
			}
			return num + 1;
		}
	}
}
