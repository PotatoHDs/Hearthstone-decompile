using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000B3 RID: 179
	public class PurchaseWithGoldResponse : IProtoBuf
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0002ED32 File Offset: 0x0002CF32
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x0002ED3A File Offset: 0x0002CF3A
		public PurchaseWithGoldResponse.PurchaseResult Result { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x0002ED43 File Offset: 0x0002CF43
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x0002ED4B File Offset: 0x0002CF4B
		public long GoldUsed
		{
			get
			{
				return this._GoldUsed;
			}
			set
			{
				this._GoldUsed = value;
				this.HasGoldUsed = true;
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002ED5C File Offset: 0x0002CF5C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Result.GetHashCode();
			if (this.HasGoldUsed)
			{
				num ^= this.GoldUsed.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002EDA8 File Offset: 0x0002CFA8
		public override bool Equals(object obj)
		{
			PurchaseWithGoldResponse purchaseWithGoldResponse = obj as PurchaseWithGoldResponse;
			return purchaseWithGoldResponse != null && this.Result.Equals(purchaseWithGoldResponse.Result) && this.HasGoldUsed == purchaseWithGoldResponse.HasGoldUsed && (!this.HasGoldUsed || this.GoldUsed.Equals(purchaseWithGoldResponse.GoldUsed));
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002EE13 File Offset: 0x0002D013
		public void Deserialize(Stream stream)
		{
			PurchaseWithGoldResponse.Deserialize(stream, this);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002EE1D File Offset: 0x0002D01D
		public static PurchaseWithGoldResponse Deserialize(Stream stream, PurchaseWithGoldResponse instance)
		{
			return PurchaseWithGoldResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0002EE28 File Offset: 0x0002D028
		public static PurchaseWithGoldResponse DeserializeLengthDelimited(Stream stream)
		{
			PurchaseWithGoldResponse purchaseWithGoldResponse = new PurchaseWithGoldResponse();
			PurchaseWithGoldResponse.DeserializeLengthDelimited(stream, purchaseWithGoldResponse);
			return purchaseWithGoldResponse;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0002EE44 File Offset: 0x0002D044
		public static PurchaseWithGoldResponse DeserializeLengthDelimited(Stream stream, PurchaseWithGoldResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PurchaseWithGoldResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0002EE6C File Offset: 0x0002D06C
		public static PurchaseWithGoldResponse Deserialize(Stream stream, PurchaseWithGoldResponse instance, long limit)
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
					if (num != 16)
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
						instance.GoldUsed = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Result = (PurchaseWithGoldResponse.PurchaseResult)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0002EF04 File Offset: 0x0002D104
		public void Serialize(Stream stream)
		{
			PurchaseWithGoldResponse.Serialize(stream, this);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0002EF0D File Offset: 0x0002D10D
		public static void Serialize(Stream stream, PurchaseWithGoldResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Result));
			if (instance.HasGoldUsed)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GoldUsed);
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0002EF40 File Offset: 0x0002D140
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Result));
			if (this.HasGoldUsed)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.GoldUsed);
			}
			return num + 1U;
		}

		// Token: 0x0400045F RID: 1119
		public bool HasGoldUsed;

		// Token: 0x04000460 RID: 1120
		private long _GoldUsed;

		// Token: 0x020005BD RID: 1469
		public enum PacketID
		{
			// Token: 0x04001F84 RID: 8068
			ID = 280
		}

		// Token: 0x020005BE RID: 1470
		public enum PurchaseResult
		{
			// Token: 0x04001F86 RID: 8070
			PR_SUCCESS = 1,
			// Token: 0x04001F87 RID: 8071
			PR_INSUFFICIENT_FUNDS,
			// Token: 0x04001F88 RID: 8072
			PR_PRODUCT_NA,
			// Token: 0x04001F89 RID: 8073
			PR_FEATURE_NA,
			// Token: 0x04001F8A RID: 8074
			PR_INVALID_QUANTITY,
			// Token: 0x04001F8B RID: 8075
			PR_PRODUCT_EVENT_HAS_ENDED
		}
	}
}
