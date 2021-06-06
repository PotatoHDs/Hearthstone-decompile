using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x02000034 RID: 52
	public class PurchaseError : IProtoBuf
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000B60A File Offset: 0x0000980A
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000B612 File Offset: 0x00009812
		public PurchaseError.Error Error_ { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000B61B File Offset: 0x0000981B
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0000B623 File Offset: 0x00009823
		public string PurchaseInProgress
		{
			get
			{
				return this._PurchaseInProgress;
			}
			set
			{
				this._PurchaseInProgress = value;
				this.HasPurchaseInProgress = (value != null);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000B636 File Offset: 0x00009836
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000B63E File Offset: 0x0000983E
		public string ErrorCode
		{
			get
			{
				return this._ErrorCode;
			}
			set
			{
				this._ErrorCode = value;
				this.HasErrorCode = (value != null);
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000B654 File Offset: 0x00009854
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Error_.GetHashCode();
			if (this.HasPurchaseInProgress)
			{
				num ^= this.PurchaseInProgress.GetHashCode();
			}
			if (this.HasErrorCode)
			{
				num ^= this.ErrorCode.GetHashCode();
			}
			return num;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B6B4 File Offset: 0x000098B4
		public override bool Equals(object obj)
		{
			PurchaseError purchaseError = obj as PurchaseError;
			return purchaseError != null && this.Error_.Equals(purchaseError.Error_) && this.HasPurchaseInProgress == purchaseError.HasPurchaseInProgress && (!this.HasPurchaseInProgress || this.PurchaseInProgress.Equals(purchaseError.PurchaseInProgress)) && this.HasErrorCode == purchaseError.HasErrorCode && (!this.HasErrorCode || this.ErrorCode.Equals(purchaseError.ErrorCode));
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B747 File Offset: 0x00009947
		public void Deserialize(Stream stream)
		{
			PurchaseError.Deserialize(stream, this);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000B751 File Offset: 0x00009951
		public static PurchaseError Deserialize(Stream stream, PurchaseError instance)
		{
			return PurchaseError.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000B75C File Offset: 0x0000995C
		public static PurchaseError DeserializeLengthDelimited(Stream stream)
		{
			PurchaseError purchaseError = new PurchaseError();
			PurchaseError.DeserializeLengthDelimited(stream, purchaseError);
			return purchaseError;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000B778 File Offset: 0x00009978
		public static PurchaseError DeserializeLengthDelimited(Stream stream, PurchaseError instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PurchaseError.Deserialize(stream, instance, num);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000B7A0 File Offset: 0x000099A0
		public static PurchaseError Deserialize(Stream stream, PurchaseError instance, long limit)
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
				else if (num != 8)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.ErrorCode = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.PurchaseInProgress = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Error_ = (PurchaseError.Error)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B84E File Offset: 0x00009A4E
		public void Serialize(Stream stream)
		{
			PurchaseError.Serialize(stream, this);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B858 File Offset: 0x00009A58
		public static void Serialize(Stream stream, PurchaseError instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Error_));
			if (instance.HasPurchaseInProgress)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PurchaseInProgress));
			}
			if (instance.HasErrorCode)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ErrorCode));
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Error_));
			if (this.HasPurchaseInProgress)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.PurchaseInProgress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasErrorCode)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ErrorCode);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 1U;
		}

		// Token: 0x040000D7 RID: 215
		public bool HasPurchaseInProgress;

		// Token: 0x040000D8 RID: 216
		private string _PurchaseInProgress;

		// Token: 0x040000D9 RID: 217
		public bool HasErrorCode;

		// Token: 0x040000DA RID: 218
		private string _ErrorCode;

		// Token: 0x0200055B RID: 1371
		public enum Error
		{
			// Token: 0x04001E2C RID: 7724
			E_UNKNOWN = -1,
			// Token: 0x04001E2D RID: 7725
			E_SUCCESS,
			// Token: 0x04001E2E RID: 7726
			E_STILL_IN_PROGRESS,
			// Token: 0x04001E2F RID: 7727
			E_INVALID_BNET,
			// Token: 0x04001E30 RID: 7728
			E_SERVICE_NA,
			// Token: 0x04001E31 RID: 7729
			E_PURCHASE_IN_PROGRESS,
			// Token: 0x04001E32 RID: 7730
			E_DATABASE,
			// Token: 0x04001E33 RID: 7731
			E_INVALID_QUANTITY,
			// Token: 0x04001E34 RID: 7732
			E_DUPLICATE_LICENSE,
			// Token: 0x04001E35 RID: 7733
			E_REQUEST_NOT_SENT,
			// Token: 0x04001E36 RID: 7734
			E_NO_ACTIVE_BPAY,
			// Token: 0x04001E37 RID: 7735
			E_FAILED_RISK,
			// Token: 0x04001E38 RID: 7736
			E_CANCELED,
			// Token: 0x04001E39 RID: 7737
			E_WAIT_MOP,
			// Token: 0x04001E3A RID: 7738
			E_WAIT_CLIENT_CONFIRM,
			// Token: 0x04001E3B RID: 7739
			E_WAIT_CLIENT_RISK,
			// Token: 0x04001E3C RID: 7740
			E_PRODUCT_NA,
			// Token: 0x04001E3D RID: 7741
			E_RISK_TIMEOUT,
			// Token: 0x04001E3E RID: 7742
			E_PRODUCT_ALREADY_OWNED,
			// Token: 0x04001E3F RID: 7743
			E_WAIT_THIRD_PARTY_RECEIPT,
			// Token: 0x04001E40 RID: 7744
			E_PRODUCT_EVENT_HAS_ENDED,
			// Token: 0x04001E41 RID: 7745
			E_BP_GENERIC_FAIL = 100,
			// Token: 0x04001E42 RID: 7746
			E_BP_INVALID_CC_EXPIRY,
			// Token: 0x04001E43 RID: 7747
			E_BP_RISK_ERROR,
			// Token: 0x04001E44 RID: 7748
			E_BP_NO_VALID_PAYMENT,
			// Token: 0x04001E45 RID: 7749
			E_BP_PAYMENT_AUTH,
			// Token: 0x04001E46 RID: 7750
			E_BP_PROVIDER_DENIED,
			// Token: 0x04001E47 RID: 7751
			E_BP_PURCHASE_BAN,
			// Token: 0x04001E48 RID: 7752
			E_BP_SPENDING_LIMIT,
			// Token: 0x04001E49 RID: 7753
			E_BP_PARENTAL_CONTROL,
			// Token: 0x04001E4A RID: 7754
			E_BP_THROTTLED,
			// Token: 0x04001E4B RID: 7755
			E_BP_THIRD_PARTY_BAD_RECEIPT,
			// Token: 0x04001E4C RID: 7756
			E_BP_THIRD_PARTY_RECEIPT_USED,
			// Token: 0x04001E4D RID: 7757
			E_BP_PRODUCT_UNIQUENESS_VIOLATED,
			// Token: 0x04001E4E RID: 7758
			E_BP_REGION_IS_DOWN,
			// Token: 0x04001E4F RID: 7759
			E_BP_GENERIC_FAIL_RETRY_CONTACT_CS_IF_PERSISTS = 115,
			// Token: 0x04001E50 RID: 7760
			E_BP_CHALLENGE_ID_FAILED_VERIFICATION
		}
	}
}
