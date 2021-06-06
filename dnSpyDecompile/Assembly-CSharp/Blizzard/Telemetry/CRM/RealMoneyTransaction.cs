using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.CRM
{
	// Token: 0x02001178 RID: 4472
	public class RealMoneyTransaction : IProtoBuf
	{
		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x0600C45E RID: 50270 RVA: 0x003B3D81 File Offset: 0x003B1F81
		// (set) Token: 0x0600C45F RID: 50271 RVA: 0x003B3D89 File Offset: 0x003B1F89
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

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x0600C460 RID: 50272 RVA: 0x003B3D9C File Offset: 0x003B1F9C
		// (set) Token: 0x0600C461 RID: 50273 RVA: 0x003B3DA4 File Offset: 0x003B1FA4
		public string AppStore
		{
			get
			{
				return this._AppStore;
			}
			set
			{
				this._AppStore = value;
				this.HasAppStore = (value != null);
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x0600C462 RID: 50274 RVA: 0x003B3DB7 File Offset: 0x003B1FB7
		// (set) Token: 0x0600C463 RID: 50275 RVA: 0x003B3DBF File Offset: 0x003B1FBF
		public string Receipt
		{
			get
			{
				return this._Receipt;
			}
			set
			{
				this._Receipt = value;
				this.HasReceipt = (value != null);
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x0600C464 RID: 50276 RVA: 0x003B3DD2 File Offset: 0x003B1FD2
		// (set) Token: 0x0600C465 RID: 50277 RVA: 0x003B3DDA File Offset: 0x003B1FDA
		public string ReceiptSignature
		{
			get
			{
				return this._ReceiptSignature;
			}
			set
			{
				this._ReceiptSignature = value;
				this.HasReceiptSignature = (value != null);
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x0600C466 RID: 50278 RVA: 0x003B3DED File Offset: 0x003B1FED
		// (set) Token: 0x0600C467 RID: 50279 RVA: 0x003B3DF5 File Offset: 0x003B1FF5
		public string ProductId
		{
			get
			{
				return this._ProductId;
			}
			set
			{
				this._ProductId = value;
				this.HasProductId = (value != null);
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x0600C468 RID: 50280 RVA: 0x003B3E08 File Offset: 0x003B2008
		// (set) Token: 0x0600C469 RID: 50281 RVA: 0x003B3E10 File Offset: 0x003B2010
		public string ItemCost
		{
			get
			{
				return this._ItemCost;
			}
			set
			{
				this._ItemCost = value;
				this.HasItemCost = (value != null);
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x0600C46A RID: 50282 RVA: 0x003B3E23 File Offset: 0x003B2023
		// (set) Token: 0x0600C46B RID: 50283 RVA: 0x003B3E2B File Offset: 0x003B202B
		public string ItemQuantity
		{
			get
			{
				return this._ItemQuantity;
			}
			set
			{
				this._ItemQuantity = value;
				this.HasItemQuantity = (value != null);
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x0600C46C RID: 50284 RVA: 0x003B3E3E File Offset: 0x003B203E
		// (set) Token: 0x0600C46D RID: 50285 RVA: 0x003B3E46 File Offset: 0x003B2046
		public string LocalCurrency
		{
			get
			{
				return this._LocalCurrency;
			}
			set
			{
				this._LocalCurrency = value;
				this.HasLocalCurrency = (value != null);
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x0600C46E RID: 50286 RVA: 0x003B3E59 File Offset: 0x003B2059
		// (set) Token: 0x0600C46F RID: 50287 RVA: 0x003B3E61 File Offset: 0x003B2061
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

		// Token: 0x0600C470 RID: 50288 RVA: 0x003B3E74 File Offset: 0x003B2074
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasApplicationId)
			{
				num ^= this.ApplicationId.GetHashCode();
			}
			if (this.HasAppStore)
			{
				num ^= this.AppStore.GetHashCode();
			}
			if (this.HasReceipt)
			{
				num ^= this.Receipt.GetHashCode();
			}
			if (this.HasReceiptSignature)
			{
				num ^= this.ReceiptSignature.GetHashCode();
			}
			if (this.HasProductId)
			{
				num ^= this.ProductId.GetHashCode();
			}
			if (this.HasItemCost)
			{
				num ^= this.ItemCost.GetHashCode();
			}
			if (this.HasItemQuantity)
			{
				num ^= this.ItemQuantity.GetHashCode();
			}
			if (this.HasLocalCurrency)
			{
				num ^= this.LocalCurrency.GetHashCode();
			}
			if (this.HasTransactionId)
			{
				num ^= this.TransactionId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C471 RID: 50289 RVA: 0x003B3F54 File Offset: 0x003B2154
		public override bool Equals(object obj)
		{
			RealMoneyTransaction realMoneyTransaction = obj as RealMoneyTransaction;
			return realMoneyTransaction != null && this.HasApplicationId == realMoneyTransaction.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(realMoneyTransaction.ApplicationId)) && this.HasAppStore == realMoneyTransaction.HasAppStore && (!this.HasAppStore || this.AppStore.Equals(realMoneyTransaction.AppStore)) && this.HasReceipt == realMoneyTransaction.HasReceipt && (!this.HasReceipt || this.Receipt.Equals(realMoneyTransaction.Receipt)) && this.HasReceiptSignature == realMoneyTransaction.HasReceiptSignature && (!this.HasReceiptSignature || this.ReceiptSignature.Equals(realMoneyTransaction.ReceiptSignature)) && this.HasProductId == realMoneyTransaction.HasProductId && (!this.HasProductId || this.ProductId.Equals(realMoneyTransaction.ProductId)) && this.HasItemCost == realMoneyTransaction.HasItemCost && (!this.HasItemCost || this.ItemCost.Equals(realMoneyTransaction.ItemCost)) && this.HasItemQuantity == realMoneyTransaction.HasItemQuantity && (!this.HasItemQuantity || this.ItemQuantity.Equals(realMoneyTransaction.ItemQuantity)) && this.HasLocalCurrency == realMoneyTransaction.HasLocalCurrency && (!this.HasLocalCurrency || this.LocalCurrency.Equals(realMoneyTransaction.LocalCurrency)) && this.HasTransactionId == realMoneyTransaction.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(realMoneyTransaction.TransactionId));
		}

		// Token: 0x0600C472 RID: 50290 RVA: 0x003B40F1 File Offset: 0x003B22F1
		public void Deserialize(Stream stream)
		{
			RealMoneyTransaction.Deserialize(stream, this);
		}

		// Token: 0x0600C473 RID: 50291 RVA: 0x003B40FB File Offset: 0x003B22FB
		public static RealMoneyTransaction Deserialize(Stream stream, RealMoneyTransaction instance)
		{
			return RealMoneyTransaction.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C474 RID: 50292 RVA: 0x003B4108 File Offset: 0x003B2308
		public static RealMoneyTransaction DeserializeLengthDelimited(Stream stream)
		{
			RealMoneyTransaction realMoneyTransaction = new RealMoneyTransaction();
			RealMoneyTransaction.DeserializeLengthDelimited(stream, realMoneyTransaction);
			return realMoneyTransaction;
		}

		// Token: 0x0600C475 RID: 50293 RVA: 0x003B4124 File Offset: 0x003B2324
		public static RealMoneyTransaction DeserializeLengthDelimited(Stream stream, RealMoneyTransaction instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RealMoneyTransaction.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C476 RID: 50294 RVA: 0x003B414C File Offset: 0x003B234C
		public static RealMoneyTransaction Deserialize(Stream stream, RealMoneyTransaction instance, long limit)
		{
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
					instance.ApplicationId = ProtocolParser.ReadString(stream);
				}
				else
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field <= 40U)
					{
						if (field <= 20U)
						{
							if (field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							if (field == 20U)
							{
								if (key.WireType == Wire.LengthDelimited)
								{
									instance.AppStore = ProtocolParser.ReadString(stream);
									continue;
								}
								continue;
							}
						}
						else if (field != 30U)
						{
							if (field == 40U)
							{
								if (key.WireType == Wire.LengthDelimited)
								{
									instance.ReceiptSignature = ProtocolParser.ReadString(stream);
									continue;
								}
								continue;
							}
						}
						else
						{
							if (key.WireType == Wire.LengthDelimited)
							{
								instance.Receipt = ProtocolParser.ReadString(stream);
								continue;
							}
							continue;
						}
					}
					else if (field <= 60U)
					{
						if (field != 50U)
						{
							if (field == 60U)
							{
								if (key.WireType == Wire.LengthDelimited)
								{
									instance.ItemCost = ProtocolParser.ReadString(stream);
									continue;
								}
								continue;
							}
						}
						else
						{
							if (key.WireType == Wire.LengthDelimited)
							{
								instance.ProductId = ProtocolParser.ReadString(stream);
								continue;
							}
							continue;
						}
					}
					else if (field != 70U)
					{
						if (field != 80U)
						{
							if (field == 900U)
							{
								if (key.WireType == Wire.LengthDelimited)
								{
									instance.TransactionId = ProtocolParser.ReadString(stream);
									continue;
								}
								continue;
							}
						}
						else
						{
							if (key.WireType == Wire.LengthDelimited)
							{
								instance.LocalCurrency = ProtocolParser.ReadString(stream);
								continue;
							}
							continue;
						}
					}
					else
					{
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ItemQuantity = ProtocolParser.ReadString(stream);
							continue;
						}
						continue;
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

		// Token: 0x0600C477 RID: 50295 RVA: 0x003B431D File Offset: 0x003B251D
		public void Serialize(Stream stream)
		{
			RealMoneyTransaction.Serialize(stream, this);
		}

		// Token: 0x0600C478 RID: 50296 RVA: 0x003B4328 File Offset: 0x003B2528
		public static void Serialize(Stream stream, RealMoneyTransaction instance)
		{
			if (instance.HasApplicationId)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
			if (instance.HasAppStore)
			{
				stream.WriteByte(162);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AppStore));
			}
			if (instance.HasReceipt)
			{
				stream.WriteByte(242);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Receipt));
			}
			if (instance.HasReceiptSignature)
			{
				stream.WriteByte(194);
				stream.WriteByte(2);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ReceiptSignature));
			}
			if (instance.HasProductId)
			{
				stream.WriteByte(146);
				stream.WriteByte(3);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProductId));
			}
			if (instance.HasItemCost)
			{
				stream.WriteByte(226);
				stream.WriteByte(3);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ItemCost));
			}
			if (instance.HasItemQuantity)
			{
				stream.WriteByte(178);
				stream.WriteByte(4);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ItemQuantity));
			}
			if (instance.HasLocalCurrency)
			{
				stream.WriteByte(130);
				stream.WriteByte(5);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.LocalCurrency));
			}
			if (instance.HasTransactionId)
			{
				stream.WriteByte(162);
				stream.WriteByte(56);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TransactionId));
			}
		}

		// Token: 0x0600C479 RID: 50297 RVA: 0x003B44DC File Offset: 0x003B26DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasApplicationId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasAppStore)
			{
				num += 2U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.AppStore);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasReceipt)
			{
				num += 2U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.Receipt);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasReceiptSignature)
			{
				num += 2U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.ReceiptSignature);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasProductId)
			{
				num += 2U;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.ProductId);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (this.HasItemCost)
			{
				num += 2U;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(this.ItemCost);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			if (this.HasItemQuantity)
			{
				num += 2U;
				uint byteCount7 = (uint)Encoding.UTF8.GetByteCount(this.ItemQuantity);
				num += ProtocolParser.SizeOfUInt32(byteCount7) + byteCount7;
			}
			if (this.HasLocalCurrency)
			{
				num += 2U;
				uint byteCount8 = (uint)Encoding.UTF8.GetByteCount(this.LocalCurrency);
				num += ProtocolParser.SizeOfUInt32(byteCount8) + byteCount8;
			}
			if (this.HasTransactionId)
			{
				num += 2U;
				uint byteCount9 = (uint)Encoding.UTF8.GetByteCount(this.TransactionId);
				num += ProtocolParser.SizeOfUInt32(byteCount9) + byteCount9;
			}
			return num;
		}

		// Token: 0x04009D31 RID: 40241
		public bool HasApplicationId;

		// Token: 0x04009D32 RID: 40242
		private string _ApplicationId;

		// Token: 0x04009D33 RID: 40243
		public bool HasAppStore;

		// Token: 0x04009D34 RID: 40244
		private string _AppStore;

		// Token: 0x04009D35 RID: 40245
		public bool HasReceipt;

		// Token: 0x04009D36 RID: 40246
		private string _Receipt;

		// Token: 0x04009D37 RID: 40247
		public bool HasReceiptSignature;

		// Token: 0x04009D38 RID: 40248
		private string _ReceiptSignature;

		// Token: 0x04009D39 RID: 40249
		public bool HasProductId;

		// Token: 0x04009D3A RID: 40250
		private string _ProductId;

		// Token: 0x04009D3B RID: 40251
		public bool HasItemCost;

		// Token: 0x04009D3C RID: 40252
		private string _ItemCost;

		// Token: 0x04009D3D RID: 40253
		public bool HasItemQuantity;

		// Token: 0x04009D3E RID: 40254
		private string _ItemQuantity;

		// Token: 0x04009D3F RID: 40255
		public bool HasLocalCurrency;

		// Token: 0x04009D40 RID: 40256
		private string _LocalCurrency;

		// Token: 0x04009D41 RID: 40257
		public bool HasTransactionId;

		// Token: 0x04009D42 RID: 40258
		private string _TransactionId;
	}
}
