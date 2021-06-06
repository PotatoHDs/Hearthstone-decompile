using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x0200119B RID: 4507
	public class AttributionPurchase : IProtoBuf
	{
		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x0600C6F7 RID: 50935 RVA: 0x003BD264 File Offset: 0x003BB464
		// (set) Token: 0x0600C6F8 RID: 50936 RVA: 0x003BD26C File Offset: 0x003BB46C
		public string ApplicationId
		{
			get
			{
				return this._ApplicationId;
			}
			set
			{
				this._ApplicationId = value;
				this.HasApplicationId = (value != null);
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x0600C6F9 RID: 50937 RVA: 0x003BD27F File Offset: 0x003BB47F
		// (set) Token: 0x0600C6FA RID: 50938 RVA: 0x003BD287 File Offset: 0x003BB487
		public string DeviceType
		{
			get
			{
				return this._DeviceType;
			}
			set
			{
				this._DeviceType = value;
				this.HasDeviceType = (value != null);
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x0600C6FB RID: 50939 RVA: 0x003BD29A File Offset: 0x003BB49A
		// (set) Token: 0x0600C6FC RID: 50940 RVA: 0x003BD2A2 File Offset: 0x003BB4A2
		public ulong FirstInstallDate
		{
			get
			{
				return this._FirstInstallDate;
			}
			set
			{
				this._FirstInstallDate = value;
				this.HasFirstInstallDate = true;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x0600C6FD RID: 50941 RVA: 0x003BD2B2 File Offset: 0x003BB4B2
		// (set) Token: 0x0600C6FE RID: 50942 RVA: 0x003BD2BA File Offset: 0x003BB4BA
		public string BundleId
		{
			get
			{
				return this._BundleId;
			}
			set
			{
				this._BundleId = value;
				this.HasBundleId = (value != null);
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x0600C6FF RID: 50943 RVA: 0x003BD2CD File Offset: 0x003BB4CD
		// (set) Token: 0x0600C700 RID: 50944 RVA: 0x003BD2D5 File Offset: 0x003BB4D5
		public string PurchaseType
		{
			get
			{
				return this._PurchaseType;
			}
			set
			{
				this._PurchaseType = value;
				this.HasPurchaseType = (value != null);
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x0600C701 RID: 50945 RVA: 0x003BD2E8 File Offset: 0x003BB4E8
		// (set) Token: 0x0600C702 RID: 50946 RVA: 0x003BD2F0 File Offset: 0x003BB4F0
		public string TransactionId
		{
			get
			{
				return this._TransactionId;
			}
			set
			{
				this._TransactionId = value;
				this.HasTransactionId = (value != null);
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x0600C703 RID: 50947 RVA: 0x003BD303 File Offset: 0x003BB503
		// (set) Token: 0x0600C704 RID: 50948 RVA: 0x003BD30B File Offset: 0x003BB50B
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

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x0600C705 RID: 50949 RVA: 0x003BD31B File Offset: 0x003BB51B
		// (set) Token: 0x0600C706 RID: 50950 RVA: 0x003BD323 File Offset: 0x003BB523
		public List<AttributionPurchase.PaymentInfo> Payments
		{
			get
			{
				return this._Payments;
			}
			set
			{
				this._Payments = value;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x0600C707 RID: 50951 RVA: 0x003BD32C File Offset: 0x003BB52C
		// (set) Token: 0x0600C708 RID: 50952 RVA: 0x003BD334 File Offset: 0x003BB534
		public float Amount
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

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x0600C709 RID: 50953 RVA: 0x003BD344 File Offset: 0x003BB544
		// (set) Token: 0x0600C70A RID: 50954 RVA: 0x003BD34C File Offset: 0x003BB54C
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

		// Token: 0x0600C70B RID: 50955 RVA: 0x003BD360 File Offset: 0x003BB560
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasApplicationId)
			{
				num ^= this.ApplicationId.GetHashCode();
			}
			if (this.HasDeviceType)
			{
				num ^= this.DeviceType.GetHashCode();
			}
			if (this.HasFirstInstallDate)
			{
				num ^= this.FirstInstallDate.GetHashCode();
			}
			if (this.HasBundleId)
			{
				num ^= this.BundleId.GetHashCode();
			}
			if (this.HasPurchaseType)
			{
				num ^= this.PurchaseType.GetHashCode();
			}
			if (this.HasTransactionId)
			{
				num ^= this.TransactionId.GetHashCode();
			}
			if (this.HasQuantity)
			{
				num ^= this.Quantity.GetHashCode();
			}
			foreach (AttributionPurchase.PaymentInfo paymentInfo in this.Payments)
			{
				num ^= paymentInfo.GetHashCode();
			}
			if (this.HasAmount)
			{
				num ^= this.Amount.GetHashCode();
			}
			if (this.HasCurrency)
			{
				num ^= this.Currency.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C70C RID: 50956 RVA: 0x003BD494 File Offset: 0x003BB694
		public override bool Equals(object obj)
		{
			AttributionPurchase attributionPurchase = obj as AttributionPurchase;
			if (attributionPurchase == null)
			{
				return false;
			}
			if (this.HasApplicationId != attributionPurchase.HasApplicationId || (this.HasApplicationId && !this.ApplicationId.Equals(attributionPurchase.ApplicationId)))
			{
				return false;
			}
			if (this.HasDeviceType != attributionPurchase.HasDeviceType || (this.HasDeviceType && !this.DeviceType.Equals(attributionPurchase.DeviceType)))
			{
				return false;
			}
			if (this.HasFirstInstallDate != attributionPurchase.HasFirstInstallDate || (this.HasFirstInstallDate && !this.FirstInstallDate.Equals(attributionPurchase.FirstInstallDate)))
			{
				return false;
			}
			if (this.HasBundleId != attributionPurchase.HasBundleId || (this.HasBundleId && !this.BundleId.Equals(attributionPurchase.BundleId)))
			{
				return false;
			}
			if (this.HasPurchaseType != attributionPurchase.HasPurchaseType || (this.HasPurchaseType && !this.PurchaseType.Equals(attributionPurchase.PurchaseType)))
			{
				return false;
			}
			if (this.HasTransactionId != attributionPurchase.HasTransactionId || (this.HasTransactionId && !this.TransactionId.Equals(attributionPurchase.TransactionId)))
			{
				return false;
			}
			if (this.HasQuantity != attributionPurchase.HasQuantity || (this.HasQuantity && !this.Quantity.Equals(attributionPurchase.Quantity)))
			{
				return false;
			}
			if (this.Payments.Count != attributionPurchase.Payments.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Payments.Count; i++)
			{
				if (!this.Payments[i].Equals(attributionPurchase.Payments[i]))
				{
					return false;
				}
			}
			return this.HasAmount == attributionPurchase.HasAmount && (!this.HasAmount || this.Amount.Equals(attributionPurchase.Amount)) && this.HasCurrency == attributionPurchase.HasCurrency && (!this.HasCurrency || this.Currency.Equals(attributionPurchase.Currency));
		}

		// Token: 0x0600C70D RID: 50957 RVA: 0x003BD68C File Offset: 0x003BB88C
		public void Deserialize(Stream stream)
		{
			AttributionPurchase.Deserialize(stream, this);
		}

		// Token: 0x0600C70E RID: 50958 RVA: 0x003BD696 File Offset: 0x003BB896
		public static AttributionPurchase Deserialize(Stream stream, AttributionPurchase instance)
		{
			return AttributionPurchase.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C70F RID: 50959 RVA: 0x003BD6A4 File Offset: 0x003BB8A4
		public static AttributionPurchase DeserializeLengthDelimited(Stream stream)
		{
			AttributionPurchase attributionPurchase = new AttributionPurchase();
			AttributionPurchase.DeserializeLengthDelimited(stream, attributionPurchase);
			return attributionPurchase;
		}

		// Token: 0x0600C710 RID: 50960 RVA: 0x003BD6C0 File Offset: 0x003BB8C0
		public static AttributionPurchase DeserializeLengthDelimited(Stream stream, AttributionPurchase instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributionPurchase.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C711 RID: 50961 RVA: 0x003BD6E8 File Offset: 0x003BB8E8
		public static AttributionPurchase Deserialize(Stream stream, AttributionPurchase instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Payments == null)
			{
				instance.Payments = new List<AttributionPurchase.PaymentInfo>();
			}
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
					if (num <= 26)
					{
						if (num == 10)
						{
							instance.PurchaseType = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 21)
						{
							instance.Amount = binaryReader.ReadSingle();
							continue;
						}
						if (num == 26)
						{
							instance.Currency = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 34)
						{
							instance.TransactionId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 40)
						{
							instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 50)
						{
							instance.Payments.Add(AttributionPurchase.PaymentInfo.DeserializeLengthDelimited(stream));
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					switch (field)
					{
					case 100U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ApplicationId = ProtocolParser.ReadString(stream);
						}
						break;
					case 101U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeviceType = ProtocolParser.ReadString(stream);
						}
						break;
					case 102U:
						if (key.WireType == Wire.Varint)
						{
							instance.FirstInstallDate = ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 103U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.BundleId = ProtocolParser.ReadString(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C712 RID: 50962 RVA: 0x003BD896 File Offset: 0x003BBA96
		public void Serialize(Stream stream)
		{
			AttributionPurchase.Serialize(stream, this);
		}

		// Token: 0x0600C713 RID: 50963 RVA: 0x003BD8A0 File Offset: 0x003BBAA0
		public static void Serialize(Stream stream, AttributionPurchase instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasApplicationId)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
			if (instance.HasDeviceType)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceType));
			}
			if (instance.HasFirstInstallDate)
			{
				stream.WriteByte(176);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt64(stream, instance.FirstInstallDate);
			}
			if (instance.HasBundleId)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BundleId));
			}
			if (instance.HasPurchaseType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PurchaseType));
			}
			if (instance.HasTransactionId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TransactionId));
			}
			if (instance.HasQuantity)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Quantity));
			}
			if (instance.Payments.Count > 0)
			{
				foreach (AttributionPurchase.PaymentInfo paymentInfo in instance.Payments)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, paymentInfo.GetSerializedSize());
					AttributionPurchase.PaymentInfo.Serialize(stream, paymentInfo);
				}
			}
			if (instance.HasAmount)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Amount);
			}
			if (instance.HasCurrency)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Currency));
			}
		}

		// Token: 0x0600C714 RID: 50964 RVA: 0x003BDA80 File Offset: 0x003BBC80
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasApplicationId)
			{
				num += 2U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasDeviceType)
			{
				num += 2U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.DeviceType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasFirstInstallDate)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64(this.FirstInstallDate);
			}
			if (this.HasBundleId)
			{
				num += 2U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.BundleId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasPurchaseType)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.PurchaseType);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasTransactionId)
			{
				num += 1U;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.TransactionId);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (this.HasQuantity)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Quantity));
			}
			if (this.Payments.Count > 0)
			{
				foreach (AttributionPurchase.PaymentInfo paymentInfo in this.Payments)
				{
					num += 1U;
					uint serializedSize = paymentInfo.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasAmount)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasCurrency)
			{
				num += 1U;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(this.Currency);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			return num;
		}

		// Token: 0x04009E49 RID: 40521
		public bool HasApplicationId;

		// Token: 0x04009E4A RID: 40522
		private string _ApplicationId;

		// Token: 0x04009E4B RID: 40523
		public bool HasDeviceType;

		// Token: 0x04009E4C RID: 40524
		private string _DeviceType;

		// Token: 0x04009E4D RID: 40525
		public bool HasFirstInstallDate;

		// Token: 0x04009E4E RID: 40526
		private ulong _FirstInstallDate;

		// Token: 0x04009E4F RID: 40527
		public bool HasBundleId;

		// Token: 0x04009E50 RID: 40528
		private string _BundleId;

		// Token: 0x04009E51 RID: 40529
		public bool HasPurchaseType;

		// Token: 0x04009E52 RID: 40530
		private string _PurchaseType;

		// Token: 0x04009E53 RID: 40531
		public bool HasTransactionId;

		// Token: 0x04009E54 RID: 40532
		private string _TransactionId;

		// Token: 0x04009E55 RID: 40533
		public bool HasQuantity;

		// Token: 0x04009E56 RID: 40534
		private int _Quantity;

		// Token: 0x04009E57 RID: 40535
		private List<AttributionPurchase.PaymentInfo> _Payments = new List<AttributionPurchase.PaymentInfo>();

		// Token: 0x04009E58 RID: 40536
		public bool HasAmount;

		// Token: 0x04009E59 RID: 40537
		private float _Amount;

		// Token: 0x04009E5A RID: 40538
		public bool HasCurrency;

		// Token: 0x04009E5B RID: 40539
		private string _Currency;

		// Token: 0x0200293C RID: 10556
		public class PaymentInfo : IProtoBuf
		{
			// Token: 0x17002D74 RID: 11636
			// (get) Token: 0x06013E58 RID: 81496 RVA: 0x0053F714 File Offset: 0x0053D914
			// (set) Token: 0x06013E59 RID: 81497 RVA: 0x0053F71C File Offset: 0x0053D91C
			public string CurrencyCode
			{
				get
				{
					return this._CurrencyCode;
				}
				set
				{
					this._CurrencyCode = value;
					this.HasCurrencyCode = (value != null);
				}
			}

			// Token: 0x17002D75 RID: 11637
			// (get) Token: 0x06013E5A RID: 81498 RVA: 0x0053F72F File Offset: 0x0053D92F
			// (set) Token: 0x06013E5B RID: 81499 RVA: 0x0053F737 File Offset: 0x0053D937
			public bool IsVirtualCurrency
			{
				get
				{
					return this._IsVirtualCurrency;
				}
				set
				{
					this._IsVirtualCurrency = value;
					this.HasIsVirtualCurrency = true;
				}
			}

			// Token: 0x17002D76 RID: 11638
			// (get) Token: 0x06013E5C RID: 81500 RVA: 0x0053F747 File Offset: 0x0053D947
			// (set) Token: 0x06013E5D RID: 81501 RVA: 0x0053F74F File Offset: 0x0053D94F
			public float Amount
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

			// Token: 0x06013E5E RID: 81502 RVA: 0x0053F760 File Offset: 0x0053D960
			public override int GetHashCode()
			{
				int num = base.GetType().GetHashCode();
				if (this.HasCurrencyCode)
				{
					num ^= this.CurrencyCode.GetHashCode();
				}
				if (this.HasIsVirtualCurrency)
				{
					num ^= this.IsVirtualCurrency.GetHashCode();
				}
				if (this.HasAmount)
				{
					num ^= this.Amount.GetHashCode();
				}
				return num;
			}

			// Token: 0x06013E5F RID: 81503 RVA: 0x0053F7C4 File Offset: 0x0053D9C4
			public override bool Equals(object obj)
			{
				AttributionPurchase.PaymentInfo paymentInfo = obj as AttributionPurchase.PaymentInfo;
				return paymentInfo != null && this.HasCurrencyCode == paymentInfo.HasCurrencyCode && (!this.HasCurrencyCode || this.CurrencyCode.Equals(paymentInfo.CurrencyCode)) && this.HasIsVirtualCurrency == paymentInfo.HasIsVirtualCurrency && (!this.HasIsVirtualCurrency || this.IsVirtualCurrency.Equals(paymentInfo.IsVirtualCurrency)) && this.HasAmount == paymentInfo.HasAmount && (!this.HasAmount || this.Amount.Equals(paymentInfo.Amount));
			}

			// Token: 0x06013E60 RID: 81504 RVA: 0x0053F865 File Offset: 0x0053DA65
			public void Deserialize(Stream stream)
			{
				AttributionPurchase.PaymentInfo.Deserialize(stream, this);
			}

			// Token: 0x06013E61 RID: 81505 RVA: 0x0053F86F File Offset: 0x0053DA6F
			public static AttributionPurchase.PaymentInfo Deserialize(Stream stream, AttributionPurchase.PaymentInfo instance)
			{
				return AttributionPurchase.PaymentInfo.Deserialize(stream, instance, -1L);
			}

			// Token: 0x06013E62 RID: 81506 RVA: 0x0053F87C File Offset: 0x0053DA7C
			public static AttributionPurchase.PaymentInfo DeserializeLengthDelimited(Stream stream)
			{
				AttributionPurchase.PaymentInfo paymentInfo = new AttributionPurchase.PaymentInfo();
				AttributionPurchase.PaymentInfo.DeserializeLengthDelimited(stream, paymentInfo);
				return paymentInfo;
			}

			// Token: 0x06013E63 RID: 81507 RVA: 0x0053F898 File Offset: 0x0053DA98
			public static AttributionPurchase.PaymentInfo DeserializeLengthDelimited(Stream stream, AttributionPurchase.PaymentInfo instance)
			{
				long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
				num += stream.Position;
				return AttributionPurchase.PaymentInfo.Deserialize(stream, instance, num);
			}

			// Token: 0x06013E64 RID: 81508 RVA: 0x0053F8C0 File Offset: 0x0053DAC0
			public static AttributionPurchase.PaymentInfo Deserialize(Stream stream, AttributionPurchase.PaymentInfo instance, long limit)
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
					else if (num == 82)
					{
						instance.CurrencyCode = ProtocolParser.ReadString(stream);
					}
					else
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						if (field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						if (field != 20U)
						{
							if (field != 30U)
							{
								ProtocolParser.SkipKey(stream, key);
							}
							else if (key.WireType == Wire.Fixed32)
							{
								instance.Amount = binaryReader.ReadSingle();
							}
						}
						else if (key.WireType == Wire.Varint)
						{
							instance.IsVirtualCurrency = ProtocolParser.ReadBool(stream);
						}
					}
				}
				if (stream.Position != limit)
				{
					throw new ProtocolBufferException("Read past max limit");
				}
				return instance;
			}

			// Token: 0x06013E65 RID: 81509 RVA: 0x0053F995 File Offset: 0x0053DB95
			public void Serialize(Stream stream)
			{
				AttributionPurchase.PaymentInfo.Serialize(stream, this);
			}

			// Token: 0x06013E66 RID: 81510 RVA: 0x0053F9A0 File Offset: 0x0053DBA0
			public static void Serialize(Stream stream, AttributionPurchase.PaymentInfo instance)
			{
				BinaryWriter binaryWriter = new BinaryWriter(stream);
				if (instance.HasCurrencyCode)
				{
					stream.WriteByte(82);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
				}
				if (instance.HasIsVirtualCurrency)
				{
					stream.WriteByte(160);
					stream.WriteByte(1);
					ProtocolParser.WriteBool(stream, instance.IsVirtualCurrency);
				}
				if (instance.HasAmount)
				{
					stream.WriteByte(245);
					stream.WriteByte(1);
					binaryWriter.Write(instance.Amount);
				}
			}

			// Token: 0x06013E67 RID: 81511 RVA: 0x0053FA28 File Offset: 0x0053DC28
			public uint GetSerializedSize()
			{
				uint num = 0U;
				if (this.HasCurrencyCode)
				{
					num += 1U;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.CurrencyCode);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
				if (this.HasIsVirtualCurrency)
				{
					num += 2U;
					num += 1U;
				}
				if (this.HasAmount)
				{
					num += 2U;
					num += 4U;
				}
				return num;
			}

			// Token: 0x0400FC3C RID: 64572
			public bool HasCurrencyCode;

			// Token: 0x0400FC3D RID: 64573
			private string _CurrencyCode;

			// Token: 0x0400FC3E RID: 64574
			public bool HasIsVirtualCurrency;

			// Token: 0x0400FC3F RID: 64575
			private bool _IsVirtualCurrency;

			// Token: 0x0400FC40 RID: 64576
			public bool HasAmount;

			// Token: 0x0400FC41 RID: 64577
			private float _Amount;
		}
	}
}
