using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x020000AC RID: 172
	public class PurchaseResponse : IProtoBuf
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0002C28F File Offset: 0x0002A48F
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x0002C297 File Offset: 0x0002A497
		public PurchaseError Error { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0002C2A0 File Offset: 0x0002A4A0
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x0002C2A8 File Offset: 0x0002A4A8
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

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0002C2B8 File Offset: 0x0002A4B8
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x0002C2C0 File Offset: 0x0002A4C0
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

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0002C2D0 File Offset: 0x0002A4D0
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x0002C2D8 File Offset: 0x0002A4D8
		public string ThirdPartyId
		{
			get
			{
				return this._ThirdPartyId;
			}
			set
			{
				this._ThirdPartyId = value;
				this.HasThirdPartyId = (value != null);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0002C2EB File Offset: 0x0002A4EB
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x0002C2F3 File Offset: 0x0002A4F3
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

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0002C303 File Offset: 0x0002A503
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x0002C30B File Offset: 0x0002A50B
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

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0002C31B File Offset: 0x0002A51B
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x0002C323 File Offset: 0x0002A523
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

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002C338 File Offset: 0x0002A538
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Error.GetHashCode();
			if (this.HasTransactionId)
			{
				num ^= this.TransactionId.GetHashCode();
			}
			if (this.HasPmtProductId)
			{
				num ^= this.PmtProductId.GetHashCode();
			}
			if (this.HasThirdPartyId)
			{
				num ^= this.ThirdPartyId.GetHashCode();
			}
			if (this.HasCurrencyDeprecated)
			{
				num ^= this.CurrencyDeprecated.GetHashCode();
			}
			if (this.HasIsZeroCostLicense)
			{
				num ^= this.IsZeroCostLicense.GetHashCode();
			}
			if (this.HasCurrencyCode)
			{
				num ^= this.CurrencyCode.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002C3F0 File Offset: 0x0002A5F0
		public override bool Equals(object obj)
		{
			PurchaseResponse purchaseResponse = obj as PurchaseResponse;
			return purchaseResponse != null && this.Error.Equals(purchaseResponse.Error) && this.HasTransactionId == purchaseResponse.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(purchaseResponse.TransactionId)) && this.HasPmtProductId == purchaseResponse.HasPmtProductId && (!this.HasPmtProductId || this.PmtProductId.Equals(purchaseResponse.PmtProductId)) && this.HasThirdPartyId == purchaseResponse.HasThirdPartyId && (!this.HasThirdPartyId || this.ThirdPartyId.Equals(purchaseResponse.ThirdPartyId)) && this.HasCurrencyDeprecated == purchaseResponse.HasCurrencyDeprecated && (!this.HasCurrencyDeprecated || this.CurrencyDeprecated.Equals(purchaseResponse.CurrencyDeprecated)) && this.HasIsZeroCostLicense == purchaseResponse.HasIsZeroCostLicense && (!this.HasIsZeroCostLicense || this.IsZeroCostLicense.Equals(purchaseResponse.IsZeroCostLicense)) && this.HasCurrencyCode == purchaseResponse.HasCurrencyCode && (!this.HasCurrencyCode || this.CurrencyCode.Equals(purchaseResponse.CurrencyCode));
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0002C52D File Offset: 0x0002A72D
		public void Deserialize(Stream stream)
		{
			PurchaseResponse.Deserialize(stream, this);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002C537 File Offset: 0x0002A737
		public static PurchaseResponse Deserialize(Stream stream, PurchaseResponse instance)
		{
			return PurchaseResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002C544 File Offset: 0x0002A744
		public static PurchaseResponse DeserializeLengthDelimited(Stream stream)
		{
			PurchaseResponse purchaseResponse = new PurchaseResponse();
			PurchaseResponse.DeserializeLengthDelimited(stream, purchaseResponse);
			return purchaseResponse;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002C560 File Offset: 0x0002A760
		public static PurchaseResponse DeserializeLengthDelimited(Stream stream, PurchaseResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PurchaseResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002C588 File Offset: 0x0002A788
		public static PurchaseResponse Deserialize(Stream stream, PurchaseResponse instance, long limit)
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
					if (num <= 24)
					{
						if (num != 10)
						{
							if (num == 16)
							{
								instance.TransactionId = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 24)
							{
								instance.PmtProductId = (long)ProtocolParser.ReadUInt64(stream);
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
					else if (num <= 40)
					{
						if (num == 34)
						{
							instance.ThirdPartyId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 40)
						{
							instance.CurrencyDeprecated = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 48)
						{
							instance.IsZeroCostLicense = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 58)
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

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002C6DA File Offset: 0x0002A8DA
		public void Serialize(Stream stream)
		{
			PurchaseResponse.Serialize(stream, this);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002C6E4 File Offset: 0x0002A8E4
		public static void Serialize(Stream stream, PurchaseResponse instance)
		{
			if (instance.Error == null)
			{
				throw new ArgumentNullException("Error", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Error.GetSerializedSize());
			PurchaseError.Serialize(stream, instance.Error);
			if (instance.HasTransactionId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TransactionId);
			}
			if (instance.HasPmtProductId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PmtProductId);
			}
			if (instance.HasThirdPartyId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ThirdPartyId));
			}
			if (instance.HasCurrencyDeprecated)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CurrencyDeprecated));
			}
			if (instance.HasIsZeroCostLicense)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.IsZeroCostLicense);
			}
			if (instance.HasCurrencyCode)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002C7EC File Offset: 0x0002A9EC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Error.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasTransactionId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.TransactionId);
			}
			if (this.HasPmtProductId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.PmtProductId);
			}
			if (this.HasThirdPartyId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ThirdPartyId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasCurrencyDeprecated)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CurrencyDeprecated));
			}
			if (this.HasIsZeroCostLicense)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCurrencyCode)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.CurrencyCode);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 1U;
		}

		// Token: 0x04000418 RID: 1048
		public bool HasTransactionId;

		// Token: 0x04000419 RID: 1049
		private long _TransactionId;

		// Token: 0x0400041A RID: 1050
		public bool HasPmtProductId;

		// Token: 0x0400041B RID: 1051
		private long _PmtProductId;

		// Token: 0x0400041C RID: 1052
		public bool HasThirdPartyId;

		// Token: 0x0400041D RID: 1053
		private string _ThirdPartyId;

		// Token: 0x0400041E RID: 1054
		public bool HasCurrencyDeprecated;

		// Token: 0x0400041F RID: 1055
		private int _CurrencyDeprecated;

		// Token: 0x04000420 RID: 1056
		public bool HasIsZeroCostLicense;

		// Token: 0x04000421 RID: 1057
		private bool _IsZeroCostLicense;

		// Token: 0x04000422 RID: 1058
		public bool HasCurrencyCode;

		// Token: 0x04000423 RID: 1059
		private string _CurrencyCode;

		// Token: 0x020005B4 RID: 1460
		public enum PacketID
		{
			// Token: 0x04001F6E RID: 8046
			ID = 256
		}
	}
}
