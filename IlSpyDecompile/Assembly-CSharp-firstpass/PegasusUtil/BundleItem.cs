using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class BundleItem : IProtoBuf
	{
		public bool HasBaseQuantity;

		private int _BaseQuantity;

		private List<ProductAttribute> _Attributes = new List<ProductAttribute>();

		public ProductType ProductType { get; set; }

		public int Data { get; set; }

		public int Quantity { get; set; }

		public int BaseQuantity
		{
			get
			{
				return _BaseQuantity;
			}
			set
			{
				_BaseQuantity = value;
				HasBaseQuantity = true;
			}
		}

		public List<ProductAttribute> Attributes
		{
			get
			{
				return _Attributes;
			}
			set
			{
				_Attributes = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ProductType.GetHashCode();
			hashCode ^= Data.GetHashCode();
			hashCode ^= Quantity.GetHashCode();
			if (HasBaseQuantity)
			{
				hashCode ^= BaseQuantity.GetHashCode();
			}
			foreach (ProductAttribute attribute in Attributes)
			{
				hashCode ^= attribute.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			BundleItem bundleItem = obj as BundleItem;
			if (bundleItem == null)
			{
				return false;
			}
			if (!ProductType.Equals(bundleItem.ProductType))
			{
				return false;
			}
			if (!Data.Equals(bundleItem.Data))
			{
				return false;
			}
			if (!Quantity.Equals(bundleItem.Quantity))
			{
				return false;
			}
			if (HasBaseQuantity != bundleItem.HasBaseQuantity || (HasBaseQuantity && !BaseQuantity.Equals(bundleItem.BaseQuantity)))
			{
				return false;
			}
			if (Attributes.Count != bundleItem.Attributes.Count)
			{
				return false;
			}
			for (int i = 0; i < Attributes.Count; i++)
			{
				if (!Attributes[i].Equals(bundleItem.Attributes[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BundleItem Deserialize(Stream stream, BundleItem instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BundleItem DeserializeLengthDelimited(Stream stream)
		{
			BundleItem bundleItem = new BundleItem();
			DeserializeLengthDelimited(stream, bundleItem);
			return bundleItem;
		}

		public static BundleItem DeserializeLengthDelimited(Stream stream, BundleItem instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BundleItem Deserialize(Stream stream, BundleItem instance, long limit)
		{
			if (instance.Attributes == null)
			{
				instance.Attributes = new List<ProductAttribute>();
			}
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
					instance.ProductType = (ProductType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Data = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.BaseQuantity = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					instance.Attributes.Add(ProductAttribute.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, BundleItem instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ProductType);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Data);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Quantity);
			if (instance.HasBaseQuantity)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BaseQuantity);
			}
			if (instance.Attributes.Count <= 0)
			{
				return;
			}
			foreach (ProductAttribute attribute in instance.Attributes)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
				ProductAttribute.Serialize(stream, attribute);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)ProductType);
			num += ProtocolParser.SizeOfUInt64((ulong)Data);
			num += ProtocolParser.SizeOfUInt64((ulong)Quantity);
			if (HasBaseQuantity)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BaseQuantity);
			}
			if (Attributes.Count > 0)
			{
				foreach (ProductAttribute attribute in Attributes)
				{
					num++;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 3;
		}
	}
}
