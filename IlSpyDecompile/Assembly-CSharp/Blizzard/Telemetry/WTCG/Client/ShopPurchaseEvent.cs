using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ShopPurchaseEvent : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasProduct;

		private Product _Product;

		public bool HasQuantity;

		private int _Quantity;

		public bool HasCurrency;

		private string _Currency;

		public bool HasAmount;

		private double _Amount;

		public bool HasIsGift;

		private bool _IsGift;

		public bool HasStorefront;

		private string _Storefront;

		public bool HasPurchaseComplete;

		private bool _PurchaseComplete;

		public bool HasStoreType;

		private string _StoreType;

		public Player Player
		{
			get
			{
				return _Player;
			}
			set
			{
				_Player = value;
				HasPlayer = value != null;
			}
		}

		public Product Product
		{
			get
			{
				return _Product;
			}
			set
			{
				_Product = value;
				HasProduct = value != null;
			}
		}

		public int Quantity
		{
			get
			{
				return _Quantity;
			}
			set
			{
				_Quantity = value;
				HasQuantity = true;
			}
		}

		public string Currency
		{
			get
			{
				return _Currency;
			}
			set
			{
				_Currency = value;
				HasCurrency = value != null;
			}
		}

		public double Amount
		{
			get
			{
				return _Amount;
			}
			set
			{
				_Amount = value;
				HasAmount = true;
			}
		}

		public bool IsGift
		{
			get
			{
				return _IsGift;
			}
			set
			{
				_IsGift = value;
				HasIsGift = true;
			}
		}

		public string Storefront
		{
			get
			{
				return _Storefront;
			}
			set
			{
				_Storefront = value;
				HasStorefront = value != null;
			}
		}

		public bool PurchaseComplete
		{
			get
			{
				return _PurchaseComplete;
			}
			set
			{
				_PurchaseComplete = value;
				HasPurchaseComplete = true;
			}
		}

		public string StoreType
		{
			get
			{
				return _StoreType;
			}
			set
			{
				_StoreType = value;
				HasStoreType = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			if (HasProduct)
			{
				num ^= Product.GetHashCode();
			}
			if (HasQuantity)
			{
				num ^= Quantity.GetHashCode();
			}
			if (HasCurrency)
			{
				num ^= Currency.GetHashCode();
			}
			if (HasAmount)
			{
				num ^= Amount.GetHashCode();
			}
			if (HasIsGift)
			{
				num ^= IsGift.GetHashCode();
			}
			if (HasStorefront)
			{
				num ^= Storefront.GetHashCode();
			}
			if (HasPurchaseComplete)
			{
				num ^= PurchaseComplete.GetHashCode();
			}
			if (HasStoreType)
			{
				num ^= StoreType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ShopPurchaseEvent shopPurchaseEvent = obj as ShopPurchaseEvent;
			if (shopPurchaseEvent == null)
			{
				return false;
			}
			if (HasPlayer != shopPurchaseEvent.HasPlayer || (HasPlayer && !Player.Equals(shopPurchaseEvent.Player)))
			{
				return false;
			}
			if (HasProduct != shopPurchaseEvent.HasProduct || (HasProduct && !Product.Equals(shopPurchaseEvent.Product)))
			{
				return false;
			}
			if (HasQuantity != shopPurchaseEvent.HasQuantity || (HasQuantity && !Quantity.Equals(shopPurchaseEvent.Quantity)))
			{
				return false;
			}
			if (HasCurrency != shopPurchaseEvent.HasCurrency || (HasCurrency && !Currency.Equals(shopPurchaseEvent.Currency)))
			{
				return false;
			}
			if (HasAmount != shopPurchaseEvent.HasAmount || (HasAmount && !Amount.Equals(shopPurchaseEvent.Amount)))
			{
				return false;
			}
			if (HasIsGift != shopPurchaseEvent.HasIsGift || (HasIsGift && !IsGift.Equals(shopPurchaseEvent.IsGift)))
			{
				return false;
			}
			if (HasStorefront != shopPurchaseEvent.HasStorefront || (HasStorefront && !Storefront.Equals(shopPurchaseEvent.Storefront)))
			{
				return false;
			}
			if (HasPurchaseComplete != shopPurchaseEvent.HasPurchaseComplete || (HasPurchaseComplete && !PurchaseComplete.Equals(shopPurchaseEvent.PurchaseComplete)))
			{
				return false;
			}
			if (HasStoreType != shopPurchaseEvent.HasStoreType || (HasStoreType && !StoreType.Equals(shopPurchaseEvent.StoreType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ShopPurchaseEvent Deserialize(Stream stream, ShopPurchaseEvent instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ShopPurchaseEvent DeserializeLengthDelimited(Stream stream)
		{
			ShopPurchaseEvent shopPurchaseEvent = new ShopPurchaseEvent();
			DeserializeLengthDelimited(stream, shopPurchaseEvent);
			return shopPurchaseEvent;
		}

		public static ShopPurchaseEvent DeserializeLengthDelimited(Stream stream, ShopPurchaseEvent instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ShopPurchaseEvent Deserialize(Stream stream, ShopPurchaseEvent instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (instance.Player == null)
					{
						instance.Player = Player.DeserializeLengthDelimited(stream);
					}
					else
					{
						Player.DeserializeLengthDelimited(stream, instance.Player);
					}
					continue;
				case 18:
					if (instance.Product == null)
					{
						instance.Product = Product.DeserializeLengthDelimited(stream);
					}
					else
					{
						Product.DeserializeLengthDelimited(stream, instance.Product);
					}
					continue;
				case 24:
					instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.Currency = ProtocolParser.ReadString(stream);
					continue;
				case 41:
					instance.Amount = binaryReader.ReadDouble();
					continue;
				case 48:
					instance.IsGift = ProtocolParser.ReadBool(stream);
					continue;
				case 58:
					instance.Storefront = ProtocolParser.ReadString(stream);
					continue;
				case 64:
					instance.PurchaseComplete = ProtocolParser.ReadBool(stream);
					continue;
				case 74:
					instance.StoreType = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ShopPurchaseEvent instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasProduct)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Product.GetSerializedSize());
				Product.Serialize(stream, instance.Product);
			}
			if (instance.HasQuantity)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Quantity);
			}
			if (instance.HasCurrency)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Currency));
			}
			if (instance.HasAmount)
			{
				stream.WriteByte(41);
				binaryWriter.Write(instance.Amount);
			}
			if (instance.HasIsGift)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.IsGift);
			}
			if (instance.HasStorefront)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Storefront));
			}
			if (instance.HasPurchaseComplete)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.PurchaseComplete);
			}
			if (instance.HasStoreType)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.StoreType));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPlayer)
			{
				num++;
				uint serializedSize = Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasProduct)
			{
				num++;
				uint serializedSize2 = Product.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasQuantity)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Quantity);
			}
			if (HasCurrency)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Currency);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasAmount)
			{
				num++;
				num += 8;
			}
			if (HasIsGift)
			{
				num++;
				num++;
			}
			if (HasStorefront)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Storefront);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasPurchaseComplete)
			{
				num++;
				num++;
			}
			if (HasStoreType)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(StoreType);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}
	}
}
