using System.IO;

namespace PegasusUtil
{
	public class PurchaseWithGold : IProtoBuf
	{
		public enum PacketID
		{
			ID = 279,
			System = 0
		}

		public bool HasData;

		private int _Data;

		public int Quantity { get; set; }

		public ProductType Product { get; set; }

		public int Data
		{
			get
			{
				return _Data;
			}
			set
			{
				_Data = value;
				HasData = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Quantity.GetHashCode();
			hashCode ^= Product.GetHashCode();
			if (HasData)
			{
				hashCode ^= Data.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PurchaseWithGold purchaseWithGold = obj as PurchaseWithGold;
			if (purchaseWithGold == null)
			{
				return false;
			}
			if (!Quantity.Equals(purchaseWithGold.Quantity))
			{
				return false;
			}
			if (!Product.Equals(purchaseWithGold.Product))
			{
				return false;
			}
			if (HasData != purchaseWithGold.HasData || (HasData && !Data.Equals(purchaseWithGold.Data)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PurchaseWithGold Deserialize(Stream stream, PurchaseWithGold instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PurchaseWithGold DeserializeLengthDelimited(Stream stream)
		{
			PurchaseWithGold purchaseWithGold = new PurchaseWithGold();
			DeserializeLengthDelimited(stream, purchaseWithGold);
			return purchaseWithGold;
		}

		public static PurchaseWithGold DeserializeLengthDelimited(Stream stream, PurchaseWithGold instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PurchaseWithGold Deserialize(Stream stream, PurchaseWithGold instance, long limit)
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
					instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Product = (ProductType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Data = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, PurchaseWithGold instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Quantity);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Product);
			if (instance.HasData)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Data);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Quantity);
			num += ProtocolParser.SizeOfUInt64((ulong)Product);
			if (HasData)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Data);
			}
			return num + 2;
		}
	}
}
