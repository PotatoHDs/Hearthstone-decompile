using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	public class GameSessionLocation : IProtoBuf
	{
		public bool HasIpAddress;

		private string _IpAddress;

		public bool HasCountry;

		private uint _Country;

		public bool HasCity;

		private string _City;

		public string IpAddress
		{
			get
			{
				return _IpAddress;
			}
			set
			{
				_IpAddress = value;
				HasIpAddress = value != null;
			}
		}

		public uint Country
		{
			get
			{
				return _Country;
			}
			set
			{
				_Country = value;
				HasCountry = true;
			}
		}

		public string City
		{
			get
			{
				return _City;
			}
			set
			{
				_City = value;
				HasCity = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetIpAddress(string val)
		{
			IpAddress = val;
		}

		public void SetCountry(uint val)
		{
			Country = val;
		}

		public void SetCity(string val)
		{
			City = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasIpAddress)
			{
				num ^= IpAddress.GetHashCode();
			}
			if (HasCountry)
			{
				num ^= Country.GetHashCode();
			}
			if (HasCity)
			{
				num ^= City.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameSessionLocation gameSessionLocation = obj as GameSessionLocation;
			if (gameSessionLocation == null)
			{
				return false;
			}
			if (HasIpAddress != gameSessionLocation.HasIpAddress || (HasIpAddress && !IpAddress.Equals(gameSessionLocation.IpAddress)))
			{
				return false;
			}
			if (HasCountry != gameSessionLocation.HasCountry || (HasCountry && !Country.Equals(gameSessionLocation.Country)))
			{
				return false;
			}
			if (HasCity != gameSessionLocation.HasCity || (HasCity && !City.Equals(gameSessionLocation.City)))
			{
				return false;
			}
			return true;
		}

		public static GameSessionLocation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameSessionLocation>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameSessionLocation Deserialize(Stream stream, GameSessionLocation instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameSessionLocation DeserializeLengthDelimited(Stream stream)
		{
			GameSessionLocation gameSessionLocation = new GameSessionLocation();
			DeserializeLengthDelimited(stream, gameSessionLocation);
			return gameSessionLocation;
		}

		public static GameSessionLocation DeserializeLengthDelimited(Stream stream, GameSessionLocation instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameSessionLocation Deserialize(Stream stream, GameSessionLocation instance, long limit)
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
					instance.IpAddress = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.Country = ProtocolParser.ReadUInt32(stream);
					continue;
				case 26:
					instance.City = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, GameSessionLocation instance)
		{
			if (instance.HasIpAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.IpAddress));
			}
			if (instance.HasCountry)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Country);
			}
			if (instance.HasCity)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.City));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasIpAddress)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(IpAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasCountry)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Country);
			}
			if (HasCity)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(City);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
