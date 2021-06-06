using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x020000B2 RID: 178
	public class CancelPurchaseResponse : IProtoBuf
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0002E8AB File Offset: 0x0002CAAB
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x0002E8B3 File Offset: 0x0002CAB3
		public CancelPurchaseResponse.CancelResult Result { get; set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0002E8BC File Offset: 0x0002CABC
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x0002E8C4 File Offset: 0x0002CAC4
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

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0002E8D4 File Offset: 0x0002CAD4
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x0002E8DC File Offset: 0x0002CADC
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

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x0002E8EC File Offset: 0x0002CAEC
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x0002E8F4 File Offset: 0x0002CAF4
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

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0002E904 File Offset: 0x0002CB04
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x0002E90C File Offset: 0x0002CB0C
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

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002E920 File Offset: 0x0002CB20
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Result.GetHashCode();
			if (this.HasTransactionId)
			{
				num ^= this.TransactionId.GetHashCode();
			}
			if (this.HasPmtProductId)
			{
				num ^= this.PmtProductId.GetHashCode();
			}
			if (this.HasCurrencyDeprecated)
			{
				num ^= this.CurrencyDeprecated.GetHashCode();
			}
			if (this.HasCurrencyCode)
			{
				num ^= this.CurrencyCode.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002E9B4 File Offset: 0x0002CBB4
		public override bool Equals(object obj)
		{
			CancelPurchaseResponse cancelPurchaseResponse = obj as CancelPurchaseResponse;
			return cancelPurchaseResponse != null && this.Result.Equals(cancelPurchaseResponse.Result) && this.HasTransactionId == cancelPurchaseResponse.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(cancelPurchaseResponse.TransactionId)) && this.HasPmtProductId == cancelPurchaseResponse.HasPmtProductId && (!this.HasPmtProductId || this.PmtProductId.Equals(cancelPurchaseResponse.PmtProductId)) && this.HasCurrencyDeprecated == cancelPurchaseResponse.HasCurrencyDeprecated && (!this.HasCurrencyDeprecated || this.CurrencyDeprecated.Equals(cancelPurchaseResponse.CurrencyDeprecated)) && this.HasCurrencyCode == cancelPurchaseResponse.HasCurrencyCode && (!this.HasCurrencyCode || this.CurrencyCode.Equals(cancelPurchaseResponse.CurrencyCode));
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002EAA6 File Offset: 0x0002CCA6
		public void Deserialize(Stream stream)
		{
			CancelPurchaseResponse.Deserialize(stream, this);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002EAB0 File Offset: 0x0002CCB0
		public static CancelPurchaseResponse Deserialize(Stream stream, CancelPurchaseResponse instance)
		{
			return CancelPurchaseResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002EABC File Offset: 0x0002CCBC
		public static CancelPurchaseResponse DeserializeLengthDelimited(Stream stream)
		{
			CancelPurchaseResponse cancelPurchaseResponse = new CancelPurchaseResponse();
			CancelPurchaseResponse.DeserializeLengthDelimited(stream, cancelPurchaseResponse);
			return cancelPurchaseResponse;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002EAD8 File Offset: 0x0002CCD8
		public static CancelPurchaseResponse DeserializeLengthDelimited(Stream stream, CancelPurchaseResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CancelPurchaseResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002EB00 File Offset: 0x0002CD00
		public static CancelPurchaseResponse Deserialize(Stream stream, CancelPurchaseResponse instance, long limit)
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.Result = (CancelPurchaseResponse.CancelResult)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.TransactionId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.PmtProductId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.CurrencyDeprecated = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 42)
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

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002EBF3 File Offset: 0x0002CDF3
		public void Serialize(Stream stream)
		{
			CancelPurchaseResponse.Serialize(stream, this);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002EBFC File Offset: 0x0002CDFC
		public static void Serialize(Stream stream, CancelPurchaseResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Result));
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
			if (instance.HasCurrencyDeprecated)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CurrencyDeprecated));
			}
			if (instance.HasCurrencyCode)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002EC98 File Offset: 0x0002CE98
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Result));
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
			if (this.HasCurrencyDeprecated)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CurrencyDeprecated));
			}
			if (this.HasCurrencyCode)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.CurrencyCode);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 1U;
		}

		// Token: 0x04000456 RID: 1110
		public bool HasTransactionId;

		// Token: 0x04000457 RID: 1111
		private long _TransactionId;

		// Token: 0x04000458 RID: 1112
		public bool HasPmtProductId;

		// Token: 0x04000459 RID: 1113
		private long _PmtProductId;

		// Token: 0x0400045A RID: 1114
		public bool HasCurrencyDeprecated;

		// Token: 0x0400045B RID: 1115
		private int _CurrencyDeprecated;

		// Token: 0x0400045C RID: 1116
		public bool HasCurrencyCode;

		// Token: 0x0400045D RID: 1117
		private string _CurrencyCode;

		// Token: 0x020005BB RID: 1467
		public enum PacketID
		{
			// Token: 0x04001F7E RID: 8062
			ID = 275
		}

		// Token: 0x020005BC RID: 1468
		public enum CancelResult
		{
			// Token: 0x04001F80 RID: 8064
			CR_SUCCESS = 1,
			// Token: 0x04001F81 RID: 8065
			CR_NOT_ALLOWED,
			// Token: 0x04001F82 RID: 8066
			CR_NOTHING_TO_CANCEL
		}
	}
}
