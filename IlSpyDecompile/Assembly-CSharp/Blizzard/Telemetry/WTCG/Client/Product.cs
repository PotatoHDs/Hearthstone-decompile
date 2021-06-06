using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class Product : IProtoBuf
	{
		public bool HasProductId;

		private long _ProductId;

		public bool HasHsProductType;

		private string _HsProductType;

		public bool HasHsProductId;

		private int _HsProductId;

		public long ProductId
		{
			get
			{
				return _ProductId;
			}
			set
			{
				_ProductId = value;
				HasProductId = true;
			}
		}

		public string HsProductType
		{
			get
			{
				return _HsProductType;
			}
			set
			{
				_HsProductType = value;
				HasHsProductType = value != null;
			}
		}

		public int HsProductId
		{
			get
			{
				return _HsProductId;
			}
			set
			{
				_HsProductId = value;
				HasHsProductId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasProductId)
			{
				num ^= ProductId.GetHashCode();
			}
			if (HasHsProductType)
			{
				num ^= HsProductType.GetHashCode();
			}
			if (HasHsProductId)
			{
				num ^= HsProductId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Product product = obj as Product;
			if (product == null)
			{
				return false;
			}
			if (HasProductId != product.HasProductId || (HasProductId && !ProductId.Equals(product.ProductId)))
			{
				return false;
			}
			if (HasHsProductType != product.HasHsProductType || (HasHsProductType && !HsProductType.Equals(product.HsProductType)))
			{
				return false;
			}
			if (HasHsProductId != product.HasHsProductId || (HasHsProductId && !HsProductId.Equals(product.HsProductId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Product Deserialize(Stream stream, Product instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Product DeserializeLengthDelimited(Stream stream)
		{
			Product product = new Product();
			DeserializeLengthDelimited(stream, product);
			return product;
		}

		public static Product DeserializeLengthDelimited(Stream stream, Product instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Product Deserialize(Stream stream, Product instance, long limit)
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
					instance.ProductId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.HsProductType = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.HsProductId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, Product instance)
		{
			if (instance.HasProductId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ProductId);
			}
			if (instance.HasHsProductType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.HsProductType));
			}
			if (instance.HasHsProductId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.HsProductId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasProductId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ProductId);
			}
			if (HasHsProductType)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(HsProductType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasHsProductId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)HsProductId);
			}
			return num;
		}
	}
}
