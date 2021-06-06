using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x020000AB RID: 171
	public class PurchaseMethod : IProtoBuf
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0002B945 File Offset: 0x00029B45
		// (set) Token: 0x06000BAA RID: 2986 RVA: 0x0002B94D File Offset: 0x00029B4D
		public long PmtProductId
		{
			get
			{
				return this._PmtProductId;
			}
			set
			{
				this._PmtProductId = value;
				this.HasPmtProductId = true;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0002B95D File Offset: 0x00029B5D
		// (set) Token: 0x06000BAC RID: 2988 RVA: 0x0002B965 File Offset: 0x00029B65
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

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0002B975 File Offset: 0x00029B75
		// (set) Token: 0x06000BAE RID: 2990 RVA: 0x0002B97D File Offset: 0x00029B7D
		public int CurrencyDeprecated
		{
			get
			{
				return this._CurrencyDeprecated;
			}
			set
			{
				this._CurrencyDeprecated = value;
				this.HasCurrencyDeprecated = true;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0002B98D File Offset: 0x00029B8D
		// (set) Token: 0x06000BB0 RID: 2992 RVA: 0x0002B995 File Offset: 0x00029B95
		public string WalletName
		{
			get
			{
				return this._WalletName;
			}
			set
			{
				this._WalletName = value;
				this.HasWalletName = (value != null);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0002B9A8 File Offset: 0x00029BA8
		// (set) Token: 0x06000BB2 RID: 2994 RVA: 0x0002B9B0 File Offset: 0x00029BB0
		public bool UseEbalance
		{
			get
			{
				return this._UseEbalance;
			}
			set
			{
				this._UseEbalance = value;
				this.HasUseEbalance = true;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0002B9C0 File Offset: 0x00029BC0
		// (set) Token: 0x06000BB4 RID: 2996 RVA: 0x0002B9C8 File Offset: 0x00029BC8
		public PurchaseError Error
		{
			get
			{
				return this._Error;
			}
			set
			{
				this._Error = value;
				this.HasError = (value != null);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0002B9DB File Offset: 0x00029BDB
		// (set) Token: 0x06000BB6 RID: 2998 RVA: 0x0002B9E3 File Offset: 0x00029BE3
		public long TransactionId
		{
			get
			{
				return this._TransactionId;
			}
			set
			{
				this._TransactionId = value;
				this.HasTransactionId = true;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0002B9F3 File Offset: 0x00029BF3
		// (set) Token: 0x06000BB8 RID: 3000 RVA: 0x0002B9FB File Offset: 0x00029BFB
		public bool IsZeroCostLicense
		{
			get
			{
				return this._IsZeroCostLicense;
			}
			set
			{
				this._IsZeroCostLicense = value;
				this.HasIsZeroCostLicense = true;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0002BA0B File Offset: 0x00029C0B
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x0002BA13 File Offset: 0x00029C13
		public string ChallengeId
		{
			get
			{
				return this._ChallengeId;
			}
			set
			{
				this._ChallengeId = value;
				this.HasChallengeId = (value != null);
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0002BA26 File Offset: 0x00029C26
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x0002BA2E File Offset: 0x00029C2E
		public string ChallengeUrl
		{
			get
			{
				return this._ChallengeUrl;
			}
			set
			{
				this._ChallengeUrl = value;
				this.HasChallengeUrl = (value != null);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0002BA41 File Offset: 0x00029C41
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x0002BA49 File Offset: 0x00029C49
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

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002BA5C File Offset: 0x00029C5C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPmtProductId)
			{
				num ^= this.PmtProductId.GetHashCode();
			}
			if (this.HasQuantity)
			{
				num ^= this.Quantity.GetHashCode();
			}
			if (this.HasCurrencyDeprecated)
			{
				num ^= this.CurrencyDeprecated.GetHashCode();
			}
			if (this.HasWalletName)
			{
				num ^= this.WalletName.GetHashCode();
			}
			if (this.HasUseEbalance)
			{
				num ^= this.UseEbalance.GetHashCode();
			}
			if (this.HasError)
			{
				num ^= this.Error.GetHashCode();
			}
			if (this.HasTransactionId)
			{
				num ^= this.TransactionId.GetHashCode();
			}
			if (this.HasIsZeroCostLicense)
			{
				num ^= this.IsZeroCostLicense.GetHashCode();
			}
			if (this.HasChallengeId)
			{
				num ^= this.ChallengeId.GetHashCode();
			}
			if (this.HasChallengeUrl)
			{
				num ^= this.ChallengeUrl.GetHashCode();
			}
			if (this.HasCurrencyCode)
			{
				num ^= this.CurrencyCode.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002BB7C File Offset: 0x00029D7C
		public override bool Equals(object obj)
		{
			PurchaseMethod purchaseMethod = obj as PurchaseMethod;
			return purchaseMethod != null && this.HasPmtProductId == purchaseMethod.HasPmtProductId && (!this.HasPmtProductId || this.PmtProductId.Equals(purchaseMethod.PmtProductId)) && this.HasQuantity == purchaseMethod.HasQuantity && (!this.HasQuantity || this.Quantity.Equals(purchaseMethod.Quantity)) && this.HasCurrencyDeprecated == purchaseMethod.HasCurrencyDeprecated && (!this.HasCurrencyDeprecated || this.CurrencyDeprecated.Equals(purchaseMethod.CurrencyDeprecated)) && this.HasWalletName == purchaseMethod.HasWalletName && (!this.HasWalletName || this.WalletName.Equals(purchaseMethod.WalletName)) && this.HasUseEbalance == purchaseMethod.HasUseEbalance && (!this.HasUseEbalance || this.UseEbalance.Equals(purchaseMethod.UseEbalance)) && this.HasError == purchaseMethod.HasError && (!this.HasError || this.Error.Equals(purchaseMethod.Error)) && this.HasTransactionId == purchaseMethod.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(purchaseMethod.TransactionId)) && this.HasIsZeroCostLicense == purchaseMethod.HasIsZeroCostLicense && (!this.HasIsZeroCostLicense || this.IsZeroCostLicense.Equals(purchaseMethod.IsZeroCostLicense)) && this.HasChallengeId == purchaseMethod.HasChallengeId && (!this.HasChallengeId || this.ChallengeId.Equals(purchaseMethod.ChallengeId)) && this.HasChallengeUrl == purchaseMethod.HasChallengeUrl && (!this.HasChallengeUrl || this.ChallengeUrl.Equals(purchaseMethod.ChallengeUrl)) && this.HasCurrencyCode == purchaseMethod.HasCurrencyCode && (!this.HasCurrencyCode || this.CurrencyCode.Equals(purchaseMethod.CurrencyCode));
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002BD81 File Offset: 0x00029F81
		public void Deserialize(Stream stream)
		{
			PurchaseMethod.Deserialize(stream, this);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002BD8B File Offset: 0x00029F8B
		public static PurchaseMethod Deserialize(Stream stream, PurchaseMethod instance)
		{
			return PurchaseMethod.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002BD98 File Offset: 0x00029F98
		public static PurchaseMethod DeserializeLengthDelimited(Stream stream)
		{
			PurchaseMethod purchaseMethod = new PurchaseMethod();
			PurchaseMethod.DeserializeLengthDelimited(stream, purchaseMethod);
			return purchaseMethod;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002BDB4 File Offset: 0x00029FB4
		public static PurchaseMethod DeserializeLengthDelimited(Stream stream, PurchaseMethod instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PurchaseMethod.Deserialize(stream, instance, num);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002BDDC File Offset: 0x00029FDC
		public static PurchaseMethod Deserialize(Stream stream, PurchaseMethod instance, long limit)
		{
			instance.CurrencyCode = "";
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
					if (num <= 40)
					{
						if (num <= 16)
						{
							if (num == 8)
							{
								instance.PmtProductId = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 16)
							{
								instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 24)
							{
								instance.CurrencyDeprecated = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 34)
							{
								instance.WalletName = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 40)
							{
								instance.UseEbalance = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
					}
					else if (num <= 64)
					{
						if (num != 50)
						{
							if (num == 56)
							{
								instance.TransactionId = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 64)
							{
								instance.IsZeroCostLicense = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (instance.Error == null)
							{
								instance.Error = PurchaseError.DeserializeLengthDelimited(stream);
								continue;
							}
							PurchaseError.DeserializeLengthDelimited(stream, instance.Error);
							continue;
						}
					}
					else
					{
						if (num == 74)
						{
							instance.ChallengeId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 82)
						{
							instance.ChallengeUrl = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 90)
						{
							instance.CurrencyCode = ProtocolParser.ReadString(stream);
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

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002BFA5 File Offset: 0x0002A1A5
		public void Serialize(Stream stream)
		{
			PurchaseMethod.Serialize(stream, this);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002BFB0 File Offset: 0x0002A1B0
		public static void Serialize(Stream stream, PurchaseMethod instance)
		{
			if (instance.HasPmtProductId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PmtProductId);
			}
			if (instance.HasQuantity)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Quantity));
			}
			if (instance.HasCurrencyDeprecated)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CurrencyDeprecated));
			}
			if (instance.HasWalletName)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.WalletName));
			}
			if (instance.HasUseEbalance)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.UseEbalance);
			}
			if (instance.HasError)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.Error.GetSerializedSize());
				PurchaseError.Serialize(stream, instance.Error);
			}
			if (instance.HasTransactionId)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TransactionId);
			}
			if (instance.HasIsZeroCostLicense)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.IsZeroCostLicense);
			}
			if (instance.HasChallengeId)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChallengeId));
			}
			if (instance.HasChallengeUrl)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChallengeUrl));
			}
			if (instance.HasCurrencyCode)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002C12C File Offset: 0x0002A32C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPmtProductId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.PmtProductId);
			}
			if (this.HasQuantity)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Quantity));
			}
			if (this.HasCurrencyDeprecated)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CurrencyDeprecated));
			}
			if (this.HasWalletName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.WalletName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasUseEbalance)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasError)
			{
				num += 1U;
				uint serializedSize = this.Error.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasTransactionId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.TransactionId);
			}
			if (this.HasIsZeroCostLicense)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasChallengeId)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ChallengeId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasChallengeUrl)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.ChallengeUrl);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasCurrencyCode)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.CurrencyCode);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num;
		}

		// Token: 0x04000401 RID: 1025
		public bool HasPmtProductId;

		// Token: 0x04000402 RID: 1026
		private long _PmtProductId;

		// Token: 0x04000403 RID: 1027
		public bool HasQuantity;

		// Token: 0x04000404 RID: 1028
		private int _Quantity;

		// Token: 0x04000405 RID: 1029
		public bool HasCurrencyDeprecated;

		// Token: 0x04000406 RID: 1030
		private int _CurrencyDeprecated;

		// Token: 0x04000407 RID: 1031
		public bool HasWalletName;

		// Token: 0x04000408 RID: 1032
		private string _WalletName;

		// Token: 0x04000409 RID: 1033
		public bool HasUseEbalance;

		// Token: 0x0400040A RID: 1034
		private bool _UseEbalance;

		// Token: 0x0400040B RID: 1035
		public bool HasError;

		// Token: 0x0400040C RID: 1036
		private PurchaseError _Error;

		// Token: 0x0400040D RID: 1037
		public bool HasTransactionId;

		// Token: 0x0400040E RID: 1038
		private long _TransactionId;

		// Token: 0x0400040F RID: 1039
		public bool HasIsZeroCostLicense;

		// Token: 0x04000410 RID: 1040
		private bool _IsZeroCostLicense;

		// Token: 0x04000411 RID: 1041
		public bool HasChallengeId;

		// Token: 0x04000412 RID: 1042
		private string _ChallengeId;

		// Token: 0x04000413 RID: 1043
		public bool HasChallengeUrl;

		// Token: 0x04000414 RID: 1044
		private string _ChallengeUrl;

		// Token: 0x04000415 RID: 1045
		public bool HasCurrencyCode;

		// Token: 0x04000416 RID: 1046
		private string _CurrencyCode;

		// Token: 0x020005B3 RID: 1459
		public enum PacketID
		{
			// Token: 0x04001F6C RID: 8044
			ID = 272
		}
	}
}
