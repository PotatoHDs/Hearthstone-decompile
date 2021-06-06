using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x02000126 RID: 294
	public class Currency : IProtoBuf
	{
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x0004350C File Offset: 0x0004170C
		// (set) Token: 0x06001357 RID: 4951 RVA: 0x00043514 File Offset: 0x00041714
		public string Code { get; set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x0004351D File Offset: 0x0004171D
		// (set) Token: 0x06001359 RID: 4953 RVA: 0x00043525 File Offset: 0x00041725
		public int CurrencyId { get; set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x0004352E File Offset: 0x0004172E
		// (set) Token: 0x0600135B RID: 4955 RVA: 0x00043536 File Offset: 0x00041736
		public int SubRegionId
		{
			get
			{
				return this._SubRegionId;
			}
			set
			{
				this._SubRegionId = value;
				this.HasSubRegionId = true;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x00043546 File Offset: 0x00041746
		// (set) Token: 0x0600135D RID: 4957 RVA: 0x0004354E File Offset: 0x0004174E
		public string Symbol
		{
			get
			{
				return this._Symbol;
			}
			set
			{
				this._Symbol = value;
				this.HasSymbol = (value != null);
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x00043561 File Offset: 0x00041761
		// (set) Token: 0x0600135F RID: 4959 RVA: 0x00043569 File Offset: 0x00041769
		public int RoundingExponent
		{
			get
			{
				return this._RoundingExponent;
			}
			set
			{
				this._RoundingExponent = value;
				this.HasRoundingExponent = true;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x00043579 File Offset: 0x00041779
		// (set) Token: 0x06001361 RID: 4961 RVA: 0x00043581 File Offset: 0x00041781
		public Currency.Tax TaxText
		{
			get
			{
				return this._TaxText;
			}
			set
			{
				this._TaxText = value;
				this.HasTaxText = true;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x00043591 File Offset: 0x00041791
		// (set) Token: 0x06001363 RID: 4963 RVA: 0x00043599 File Offset: 0x00041799
		public int ChangedVersion
		{
			get
			{
				return this._ChangedVersion;
			}
			set
			{
				this._ChangedVersion = value;
				this.HasChangedVersion = true;
			}
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x000435AC File Offset: 0x000417AC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Code.GetHashCode();
			num ^= this.CurrencyId.GetHashCode();
			if (this.HasSubRegionId)
			{
				num ^= this.SubRegionId.GetHashCode();
			}
			if (this.HasSymbol)
			{
				num ^= this.Symbol.GetHashCode();
			}
			if (this.HasRoundingExponent)
			{
				num ^= this.RoundingExponent.GetHashCode();
			}
			if (this.HasTaxText)
			{
				num ^= this.TaxText.GetHashCode();
			}
			if (this.HasChangedVersion)
			{
				num ^= this.ChangedVersion.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00043668 File Offset: 0x00041868
		public override bool Equals(object obj)
		{
			Currency currency = obj as Currency;
			return currency != null && this.Code.Equals(currency.Code) && this.CurrencyId.Equals(currency.CurrencyId) && this.HasSubRegionId == currency.HasSubRegionId && (!this.HasSubRegionId || this.SubRegionId.Equals(currency.SubRegionId)) && this.HasSymbol == currency.HasSymbol && (!this.HasSymbol || this.Symbol.Equals(currency.Symbol)) && this.HasRoundingExponent == currency.HasRoundingExponent && (!this.HasRoundingExponent || this.RoundingExponent.Equals(currency.RoundingExponent)) && this.HasTaxText == currency.HasTaxText && (!this.HasTaxText || this.TaxText.Equals(currency.TaxText)) && this.HasChangedVersion == currency.HasChangedVersion && (!this.HasChangedVersion || this.ChangedVersion.Equals(currency.ChangedVersion));
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0004379D File Offset: 0x0004199D
		public void Deserialize(Stream stream)
		{
			Currency.Deserialize(stream, this);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x000437A7 File Offset: 0x000419A7
		public static Currency Deserialize(Stream stream, Currency instance)
		{
			return Currency.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x000437B4 File Offset: 0x000419B4
		public static Currency DeserializeLengthDelimited(Stream stream)
		{
			Currency currency = new Currency();
			Currency.DeserializeLengthDelimited(stream, currency);
			return currency;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000437D0 File Offset: 0x000419D0
		public static Currency DeserializeLengthDelimited(Stream stream, Currency instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Currency.Deserialize(stream, instance, num);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x000437F8 File Offset: 0x000419F8
		public static Currency Deserialize(Stream stream, Currency instance, long limit)
		{
			instance.SubRegionId = 1;
			instance.Symbol = "$";
			instance.RoundingExponent = 2;
			instance.TaxText = Currency.Tax.TAX_INCLUDED;
			instance.ChangedVersion = 0;
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
					if (num <= 24)
					{
						if (num == 10)
						{
							instance.Code = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 16)
						{
							instance.CurrencyId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 24)
						{
							instance.SubRegionId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num <= 40)
					{
						if (num == 34)
						{
							instance.Symbol = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 40)
						{
							instance.RoundingExponent = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 48)
						{
							instance.TaxText = (Currency.Tax)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 72)
						{
							instance.ChangedVersion = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600136B RID: 4971 RVA: 0x0004394A File Offset: 0x00041B4A
		public void Serialize(Stream stream)
		{
			Currency.Serialize(stream, this);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00043954 File Offset: 0x00041B54
		public static void Serialize(Stream stream, Currency instance)
		{
			if (instance.Code == null)
			{
				throw new ArgumentNullException("Code", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Code));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CurrencyId));
			if (instance.HasSubRegionId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SubRegionId));
			}
			if (instance.HasSymbol)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Symbol));
			}
			if (instance.HasRoundingExponent)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RoundingExponent));
			}
			if (instance.HasTaxText)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TaxText));
			}
			if (instance.HasChangedVersion)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ChangedVersion));
			}
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00043A48 File Offset: 0x00041C48
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Code);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CurrencyId));
			if (this.HasSubRegionId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SubRegionId));
			}
			if (this.HasSymbol)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Symbol);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasRoundingExponent)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RoundingExponent));
			}
			if (this.HasTaxText)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.TaxText));
			}
			if (this.HasChangedVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ChangedVersion));
			}
			return num + 2U;
		}

		// Token: 0x040005FA RID: 1530
		public bool HasSubRegionId;

		// Token: 0x040005FB RID: 1531
		private int _SubRegionId;

		// Token: 0x040005FC RID: 1532
		public bool HasSymbol;

		// Token: 0x040005FD RID: 1533
		private string _Symbol;

		// Token: 0x040005FE RID: 1534
		public bool HasRoundingExponent;

		// Token: 0x040005FF RID: 1535
		private int _RoundingExponent;

		// Token: 0x04000600 RID: 1536
		public bool HasTaxText;

		// Token: 0x04000601 RID: 1537
		private Currency.Tax _TaxText;

		// Token: 0x04000602 RID: 1538
		public bool HasChangedVersion;

		// Token: 0x04000603 RID: 1539
		private int _ChangedVersion;

		// Token: 0x02000617 RID: 1559
		public enum Tax
		{
			// Token: 0x0400207C RID: 8316
			TAX_INCLUDED,
			// Token: 0x0400207D RID: 8317
			TAX_ADDED,
			// Token: 0x0400207E RID: 8318
			NO_TAX
		}
	}
}
