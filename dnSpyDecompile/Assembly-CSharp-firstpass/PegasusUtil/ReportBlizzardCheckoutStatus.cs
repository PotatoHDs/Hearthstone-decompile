using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000F2 RID: 242
	public class ReportBlizzardCheckoutStatus : IProtoBuf
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x000397E6 File Offset: 0x000379E6
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x000397EE File Offset: 0x000379EE
		public BlizzardCheckoutStatus Status { get; set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x000397F7 File Offset: 0x000379F7
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x000397FF File Offset: 0x000379FF
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

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x00039812 File Offset: 0x00037A12
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x0003981A File Offset: 0x00037A1A
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

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0003982D File Offset: 0x00037A2D
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x00039835 File Offset: 0x00037A35
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

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x00039848 File Offset: 0x00037A48
		// (set) Token: 0x06001033 RID: 4147 RVA: 0x00039850 File Offset: 0x00037A50
		public long ClientUnixTime
		{
			get
			{
				return this._ClientUnixTime;
			}
			set
			{
				this._ClientUnixTime = value;
				this.HasClientUnixTime = true;
			}
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00039860 File Offset: 0x00037A60
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Status.GetHashCode();
			if (this.HasTransactionId)
			{
				num ^= this.TransactionId.GetHashCode();
			}
			if (this.HasProductId)
			{
				num ^= this.ProductId.GetHashCode();
			}
			if (this.HasCurrency)
			{
				num ^= this.Currency.GetHashCode();
			}
			if (this.HasClientUnixTime)
			{
				num ^= this.ClientUnixTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x000398EC File Offset: 0x00037AEC
		public override bool Equals(object obj)
		{
			ReportBlizzardCheckoutStatus reportBlizzardCheckoutStatus = obj as ReportBlizzardCheckoutStatus;
			return reportBlizzardCheckoutStatus != null && this.Status.Equals(reportBlizzardCheckoutStatus.Status) && this.HasTransactionId == reportBlizzardCheckoutStatus.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(reportBlizzardCheckoutStatus.TransactionId)) && this.HasProductId == reportBlizzardCheckoutStatus.HasProductId && (!this.HasProductId || this.ProductId.Equals(reportBlizzardCheckoutStatus.ProductId)) && this.HasCurrency == reportBlizzardCheckoutStatus.HasCurrency && (!this.HasCurrency || this.Currency.Equals(reportBlizzardCheckoutStatus.Currency)) && this.HasClientUnixTime == reportBlizzardCheckoutStatus.HasClientUnixTime && (!this.HasClientUnixTime || this.ClientUnixTime.Equals(reportBlizzardCheckoutStatus.ClientUnixTime));
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x000399D8 File Offset: 0x00037BD8
		public void Deserialize(Stream stream)
		{
			ReportBlizzardCheckoutStatus.Deserialize(stream, this);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x000399E2 File Offset: 0x00037BE2
		public static ReportBlizzardCheckoutStatus Deserialize(Stream stream, ReportBlizzardCheckoutStatus instance)
		{
			return ReportBlizzardCheckoutStatus.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x000399F0 File Offset: 0x00037BF0
		public static ReportBlizzardCheckoutStatus DeserializeLengthDelimited(Stream stream)
		{
			ReportBlizzardCheckoutStatus reportBlizzardCheckoutStatus = new ReportBlizzardCheckoutStatus();
			ReportBlizzardCheckoutStatus.DeserializeLengthDelimited(stream, reportBlizzardCheckoutStatus);
			return reportBlizzardCheckoutStatus;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00039A0C File Offset: 0x00037C0C
		public static ReportBlizzardCheckoutStatus DeserializeLengthDelimited(Stream stream, ReportBlizzardCheckoutStatus instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ReportBlizzardCheckoutStatus.Deserialize(stream, instance, num);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00039A34 File Offset: 0x00037C34
		public static ReportBlizzardCheckoutStatus Deserialize(Stream stream, ReportBlizzardCheckoutStatus instance, long limit)
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
				else
				{
					if (num <= 18)
					{
						if (num == 8)
						{
							instance.Status = (BlizzardCheckoutStatus)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							instance.TransactionId = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.ProductId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 34)
						{
							instance.Currency = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 40)
						{
							instance.ClientUnixTime = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600103B RID: 4155 RVA: 0x00039B1B File Offset: 0x00037D1B
		public void Serialize(Stream stream)
		{
			ReportBlizzardCheckoutStatus.Serialize(stream, this);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00039B24 File Offset: 0x00037D24
		public static void Serialize(Stream stream, ReportBlizzardCheckoutStatus instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Status));
			if (instance.HasTransactionId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TransactionId));
			}
			if (instance.HasProductId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProductId));
			}
			if (instance.HasCurrency)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Currency));
			}
			if (instance.HasClientUnixTime)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientUnixTime);
			}
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00039BD4 File Offset: 0x00037DD4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Status));
			if (this.HasTransactionId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.TransactionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasProductId)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ProductId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasCurrency)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.Currency);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasClientUnixTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.ClientUnixTime);
			}
			return num + 1U;
		}

		// Token: 0x0400051A RID: 1306
		public bool HasTransactionId;

		// Token: 0x0400051B RID: 1307
		private string _TransactionId;

		// Token: 0x0400051C RID: 1308
		public bool HasProductId;

		// Token: 0x0400051D RID: 1309
		private string _ProductId;

		// Token: 0x0400051E RID: 1310
		public bool HasCurrency;

		// Token: 0x0400051F RID: 1311
		private string _Currency;

		// Token: 0x04000520 RID: 1312
		public bool HasClientUnixTime;

		// Token: 0x04000521 RID: 1313
		private long _ClientUnixTime;

		// Token: 0x020005F6 RID: 1526
		public enum PacketID
		{
			// Token: 0x04002021 RID: 8225
			ID = 366,
			// Token: 0x04002022 RID: 8226
			System = 1
		}
	}
}
