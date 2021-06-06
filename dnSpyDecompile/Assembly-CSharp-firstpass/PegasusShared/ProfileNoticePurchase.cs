using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x02000130 RID: 304
	public class ProfileNoticePurchase : IProtoBuf
	{
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0004541A File Offset: 0x0004361A
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x00045422 File Offset: 0x00043622
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

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x00045432 File Offset: 0x00043632
		// (set) Token: 0x06001405 RID: 5125 RVA: 0x0004543A File Offset: 0x0004363A
		public long Data
		{
			get
			{
				return this._Data;
			}
			set
			{
				this._Data = value;
				this.HasData = true;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x0004544A File Offset: 0x0004364A
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x00045452 File Offset: 0x00043652
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

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x00045462 File Offset: 0x00043662
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x0004546A File Offset: 0x0004366A
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

		// Token: 0x0600140A RID: 5130 RVA: 0x00045480 File Offset: 0x00043680
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPmtProductId)
			{
				num ^= this.PmtProductId.GetHashCode();
			}
			if (this.HasData)
			{
				num ^= this.Data.GetHashCode();
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

		// Token: 0x0600140B RID: 5131 RVA: 0x000454FC File Offset: 0x000436FC
		public override bool Equals(object obj)
		{
			ProfileNoticePurchase profileNoticePurchase = obj as ProfileNoticePurchase;
			return profileNoticePurchase != null && this.HasPmtProductId == profileNoticePurchase.HasPmtProductId && (!this.HasPmtProductId || this.PmtProductId.Equals(profileNoticePurchase.PmtProductId)) && this.HasData == profileNoticePurchase.HasData && (!this.HasData || this.Data.Equals(profileNoticePurchase.Data)) && this.HasCurrencyDeprecated == profileNoticePurchase.HasCurrencyDeprecated && (!this.HasCurrencyDeprecated || this.CurrencyDeprecated.Equals(profileNoticePurchase.CurrencyDeprecated)) && this.HasCurrencyCode == profileNoticePurchase.HasCurrencyCode && (!this.HasCurrencyCode || this.CurrencyCode.Equals(profileNoticePurchase.CurrencyCode));
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x000455CB File Offset: 0x000437CB
		public void Deserialize(Stream stream)
		{
			ProfileNoticePurchase.Deserialize(stream, this);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x000455D5 File Offset: 0x000437D5
		public static ProfileNoticePurchase Deserialize(Stream stream, ProfileNoticePurchase instance)
		{
			return ProfileNoticePurchase.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x000455E0 File Offset: 0x000437E0
		public static ProfileNoticePurchase DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticePurchase profileNoticePurchase = new ProfileNoticePurchase();
			ProfileNoticePurchase.DeserializeLengthDelimited(stream, profileNoticePurchase);
			return profileNoticePurchase;
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x000455FC File Offset: 0x000437FC
		public static ProfileNoticePurchase DeserializeLengthDelimited(Stream stream, ProfileNoticePurchase instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticePurchase.Deserialize(stream, instance, num);
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x00045624 File Offset: 0x00043824
		public static ProfileNoticePurchase Deserialize(Stream stream, ProfileNoticePurchase instance, long limit)
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
							instance.PmtProductId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Data = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001411 RID: 5137 RVA: 0x00045700 File Offset: 0x00043900
		public void Serialize(Stream stream)
		{
			ProfileNoticePurchase.Serialize(stream, this);
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0004570C File Offset: 0x0004390C
		public static void Serialize(Stream stream, ProfileNoticePurchase instance)
		{
			if (instance.HasPmtProductId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PmtProductId);
			}
			if (instance.HasData)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Data);
			}
			if (instance.HasCurrencyDeprecated)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CurrencyDeprecated));
			}
			if (instance.HasCurrencyCode)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
			}
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x00045794 File Offset: 0x00043994
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPmtProductId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.PmtProductId);
			}
			if (this.HasData)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.Data);
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
			return num;
		}

		// Token: 0x0400062A RID: 1578
		public bool HasPmtProductId;

		// Token: 0x0400062B RID: 1579
		private long _PmtProductId;

		// Token: 0x0400062C RID: 1580
		public bool HasData;

		// Token: 0x0400062D RID: 1581
		private long _Data;

		// Token: 0x0400062E RID: 1582
		public bool HasCurrencyDeprecated;

		// Token: 0x0400062F RID: 1583
		private int _CurrencyDeprecated;

		// Token: 0x04000630 RID: 1584
		public bool HasCurrencyCode;

		// Token: 0x04000631 RID: 1585
		private string _CurrencyCode;

		// Token: 0x02000624 RID: 1572
		public enum NoticeID
		{
			// Token: 0x040020A2 RID: 8354
			ID = 10
		}
	}
}
