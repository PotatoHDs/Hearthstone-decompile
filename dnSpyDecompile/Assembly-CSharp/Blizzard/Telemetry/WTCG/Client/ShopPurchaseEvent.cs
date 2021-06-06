using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011F4 RID: 4596
	public class ShopPurchaseEvent : IProtoBuf
	{
		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x0600CD94 RID: 52628 RVA: 0x003D4FAE File Offset: 0x003D31AE
		// (set) Token: 0x0600CD95 RID: 52629 RVA: 0x003D4FB6 File Offset: 0x003D31B6
		public Player Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
				this.HasPlayer = (value != null);
			}
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x0600CD96 RID: 52630 RVA: 0x003D4FC9 File Offset: 0x003D31C9
		// (set) Token: 0x0600CD97 RID: 52631 RVA: 0x003D4FD1 File Offset: 0x003D31D1
		public Product Product
		{
			get
			{
				return this._Product;
			}
			set
			{
				this._Product = value;
				this.HasProduct = (value != null);
			}
		}

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x0600CD98 RID: 52632 RVA: 0x003D4FE4 File Offset: 0x003D31E4
		// (set) Token: 0x0600CD99 RID: 52633 RVA: 0x003D4FEC File Offset: 0x003D31EC
		public int Quantity
		{
			get
			{
				return this._Quantity;
			}
			set
			{
				this._Quantity = value;
				this.HasQuantity = true;
			}
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x0600CD9A RID: 52634 RVA: 0x003D4FFC File Offset: 0x003D31FC
		// (set) Token: 0x0600CD9B RID: 52635 RVA: 0x003D5004 File Offset: 0x003D3204
		public string Currency
		{
			get
			{
				return this._Currency;
			}
			set
			{
				this._Currency = value;
				this.HasCurrency = (value != null);
			}
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x0600CD9C RID: 52636 RVA: 0x003D5017 File Offset: 0x003D3217
		// (set) Token: 0x0600CD9D RID: 52637 RVA: 0x003D501F File Offset: 0x003D321F
		public double Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				this._Amount = value;
				this.HasAmount = true;
			}
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x0600CD9E RID: 52638 RVA: 0x003D502F File Offset: 0x003D322F
		// (set) Token: 0x0600CD9F RID: 52639 RVA: 0x003D5037 File Offset: 0x003D3237
		public bool IsGift
		{
			get
			{
				return this._IsGift;
			}
			set
			{
				this._IsGift = value;
				this.HasIsGift = true;
			}
		}

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x0600CDA0 RID: 52640 RVA: 0x003D5047 File Offset: 0x003D3247
		// (set) Token: 0x0600CDA1 RID: 52641 RVA: 0x003D504F File Offset: 0x003D324F
		public string Storefront
		{
			get
			{
				return this._Storefront;
			}
			set
			{
				this._Storefront = value;
				this.HasStorefront = (value != null);
			}
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x0600CDA2 RID: 52642 RVA: 0x003D5062 File Offset: 0x003D3262
		// (set) Token: 0x0600CDA3 RID: 52643 RVA: 0x003D506A File Offset: 0x003D326A
		public bool PurchaseComplete
		{
			get
			{
				return this._PurchaseComplete;
			}
			set
			{
				this._PurchaseComplete = value;
				this.HasPurchaseComplete = true;
			}
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x0600CDA4 RID: 52644 RVA: 0x003D507A File Offset: 0x003D327A
		// (set) Token: 0x0600CDA5 RID: 52645 RVA: 0x003D5082 File Offset: 0x003D3282
		public string StoreType
		{
			get
			{
				return this._StoreType;
			}
			set
			{
				this._StoreType = value;
				this.HasStoreType = (value != null);
			}
		}

		// Token: 0x0600CDA6 RID: 52646 RVA: 0x003D5098 File Offset: 0x003D3298
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasProduct)
			{
				num ^= this.Product.GetHashCode();
			}
			if (this.HasQuantity)
			{
				num ^= this.Quantity.GetHashCode();
			}
			if (this.HasCurrency)
			{
				num ^= this.Currency.GetHashCode();
			}
			if (this.HasAmount)
			{
				num ^= this.Amount.GetHashCode();
			}
			if (this.HasIsGift)
			{
				num ^= this.IsGift.GetHashCode();
			}
			if (this.HasStorefront)
			{
				num ^= this.Storefront.GetHashCode();
			}
			if (this.HasPurchaseComplete)
			{
				num ^= this.PurchaseComplete.GetHashCode();
			}
			if (this.HasStoreType)
			{
				num ^= this.StoreType.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CDA7 RID: 52647 RVA: 0x003D5184 File Offset: 0x003D3384
		public override bool Equals(object obj)
		{
			ShopPurchaseEvent shopPurchaseEvent = obj as ShopPurchaseEvent;
			return shopPurchaseEvent != null && this.HasPlayer == shopPurchaseEvent.HasPlayer && (!this.HasPlayer || this.Player.Equals(shopPurchaseEvent.Player)) && this.HasProduct == shopPurchaseEvent.HasProduct && (!this.HasProduct || this.Product.Equals(shopPurchaseEvent.Product)) && this.HasQuantity == shopPurchaseEvent.HasQuantity && (!this.HasQuantity || this.Quantity.Equals(shopPurchaseEvent.Quantity)) && this.HasCurrency == shopPurchaseEvent.HasCurrency && (!this.HasCurrency || this.Currency.Equals(shopPurchaseEvent.Currency)) && this.HasAmount == shopPurchaseEvent.HasAmount && (!this.HasAmount || this.Amount.Equals(shopPurchaseEvent.Amount)) && this.HasIsGift == shopPurchaseEvent.HasIsGift && (!this.HasIsGift || this.IsGift.Equals(shopPurchaseEvent.IsGift)) && this.HasStorefront == shopPurchaseEvent.HasStorefront && (!this.HasStorefront || this.Storefront.Equals(shopPurchaseEvent.Storefront)) && this.HasPurchaseComplete == shopPurchaseEvent.HasPurchaseComplete && (!this.HasPurchaseComplete || this.PurchaseComplete.Equals(shopPurchaseEvent.PurchaseComplete)) && this.HasStoreType == shopPurchaseEvent.HasStoreType && (!this.HasStoreType || this.StoreType.Equals(shopPurchaseEvent.StoreType));
		}

		// Token: 0x0600CDA8 RID: 52648 RVA: 0x003D532D File Offset: 0x003D352D
		public void Deserialize(Stream stream)
		{
			ShopPurchaseEvent.Deserialize(stream, this);
		}

		// Token: 0x0600CDA9 RID: 52649 RVA: 0x003D5337 File Offset: 0x003D3537
		public static ShopPurchaseEvent Deserialize(Stream stream, ShopPurchaseEvent instance)
		{
			return ShopPurchaseEvent.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CDAA RID: 52650 RVA: 0x003D5344 File Offset: 0x003D3544
		public static ShopPurchaseEvent DeserializeLengthDelimited(Stream stream)
		{
			ShopPurchaseEvent shopPurchaseEvent = new ShopPurchaseEvent();
			ShopPurchaseEvent.DeserializeLengthDelimited(stream, shopPurchaseEvent);
			return shopPurchaseEvent;
		}

		// Token: 0x0600CDAB RID: 52651 RVA: 0x003D5360 File Offset: 0x003D3560
		public static ShopPurchaseEvent DeserializeLengthDelimited(Stream stream, ShopPurchaseEvent instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ShopPurchaseEvent.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CDAC RID: 52652 RVA: 0x003D5388 File Offset: 0x003D3588
		public static ShopPurchaseEvent Deserialize(Stream stream, ShopPurchaseEvent instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else
				{
					if (num <= 34)
					{
						if (num <= 18)
						{
							if (num != 10)
							{
								if (num == 18)
								{
									if (instance.Product == null)
									{
										instance.Product = Product.DeserializeLengthDelimited(stream);
										continue;
									}
									Product.DeserializeLengthDelimited(stream, instance.Product);
									continue;
								}
							}
							else
							{
								if (instance.Player == null)
								{
									instance.Player = Player.DeserializeLengthDelimited(stream);
									continue;
								}
								Player.DeserializeLengthDelimited(stream, instance.Player);
								continue;
							}
						}
						else
						{
							if (num == 24)
							{
								instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 34)
							{
								instance.Currency = ProtocolParser.ReadString(stream);
								continue;
							}
						}
					}
					else if (num <= 48)
					{
						if (num == 41)
						{
							instance.Amount = binaryReader.ReadDouble();
							continue;
						}
						if (num == 48)
						{
							instance.IsGift = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 58)
						{
							instance.Storefront = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 64)
						{
							instance.PurchaseComplete = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 74)
						{
							instance.StoreType = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CDAD RID: 52653 RVA: 0x003D5538 File Offset: 0x003D3738
		public void Serialize(Stream stream)
		{
			ShopPurchaseEvent.Serialize(stream, this);
		}

		// Token: 0x0600CDAE RID: 52654 RVA: 0x003D5544 File Offset: 0x003D3744
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
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Quantity));
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

		// Token: 0x0600CDAF RID: 52655 RVA: 0x003D5698 File Offset: 0x003D3898
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasProduct)
			{
				num += 1U;
				uint serializedSize2 = this.Product.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasQuantity)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Quantity));
			}
			if (this.HasCurrency)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Currency);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasAmount)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasIsGift)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasStorefront)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Storefront);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasPurchaseComplete)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasStoreType)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.StoreType);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}

		// Token: 0x0400A110 RID: 41232
		public bool HasPlayer;

		// Token: 0x0400A111 RID: 41233
		private Player _Player;

		// Token: 0x0400A112 RID: 41234
		public bool HasProduct;

		// Token: 0x0400A113 RID: 41235
		private Product _Product;

		// Token: 0x0400A114 RID: 41236
		public bool HasQuantity;

		// Token: 0x0400A115 RID: 41237
		private int _Quantity;

		// Token: 0x0400A116 RID: 41238
		public bool HasCurrency;

		// Token: 0x0400A117 RID: 41239
		private string _Currency;

		// Token: 0x0400A118 RID: 41240
		public bool HasAmount;

		// Token: 0x0400A119 RID: 41241
		private double _Amount;

		// Token: 0x0400A11A RID: 41242
		public bool HasIsGift;

		// Token: 0x0400A11B RID: 41243
		private bool _IsGift;

		// Token: 0x0400A11C RID: 41244
		public bool HasStorefront;

		// Token: 0x0400A11D RID: 41245
		private string _Storefront;

		// Token: 0x0400A11E RID: 41246
		public bool HasPurchaseComplete;

		// Token: 0x0400A11F RID: 41247
		private bool _PurchaseComplete;

		// Token: 0x0400A120 RID: 41248
		public bool HasStoreType;

		// Token: 0x0400A121 RID: 41249
		private string _StoreType;
	}
}
