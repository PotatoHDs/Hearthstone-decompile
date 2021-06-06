using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000074 RID: 116
	public class StartThirdPartyPurchase : IProtoBuf
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001AF52 File Offset: 0x00019152
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x0001AF5A File Offset: 0x0001915A
		public BattlePayProvider Provider { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001AF63 File Offset: 0x00019163
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x0001AF6B File Offset: 0x0001916B
		public string ProductId { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001AF74 File Offset: 0x00019174
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0001AF7C File Offset: 0x0001917C
		public int Quantity { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0001AF85 File Offset: 0x00019185
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x0001AF8D File Offset: 0x0001918D
		public ThirdPartyReceiptData DanglingReceiptData
		{
			get
			{
				return this._DanglingReceiptData;
			}
			set
			{
				this._DanglingReceiptData = value;
				this.HasDanglingReceiptData = (value != null);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0001AFA0 File Offset: 0x000191A0
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x0001AFA8 File Offset: 0x000191A8
		public string DeviceId { get; set; }

		// Token: 0x06000756 RID: 1878 RVA: 0x0001AFB4 File Offset: 0x000191B4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Provider.GetHashCode();
			num ^= this.ProductId.GetHashCode();
			num ^= this.Quantity.GetHashCode();
			if (this.HasDanglingReceiptData)
			{
				num ^= this.DanglingReceiptData.GetHashCode();
			}
			return num ^ this.DeviceId.GetHashCode();
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001B028 File Offset: 0x00019228
		public override bool Equals(object obj)
		{
			StartThirdPartyPurchase startThirdPartyPurchase = obj as StartThirdPartyPurchase;
			return startThirdPartyPurchase != null && this.Provider.Equals(startThirdPartyPurchase.Provider) && this.ProductId.Equals(startThirdPartyPurchase.ProductId) && this.Quantity.Equals(startThirdPartyPurchase.Quantity) && this.HasDanglingReceiptData == startThirdPartyPurchase.HasDanglingReceiptData && (!this.HasDanglingReceiptData || this.DanglingReceiptData.Equals(startThirdPartyPurchase.DanglingReceiptData)) && this.DeviceId.Equals(startThirdPartyPurchase.DeviceId);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001B0D2 File Offset: 0x000192D2
		public void Deserialize(Stream stream)
		{
			StartThirdPartyPurchase.Deserialize(stream, this);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001B0DC File Offset: 0x000192DC
		public static StartThirdPartyPurchase Deserialize(Stream stream, StartThirdPartyPurchase instance)
		{
			return StartThirdPartyPurchase.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001B0E8 File Offset: 0x000192E8
		public static StartThirdPartyPurchase DeserializeLengthDelimited(Stream stream)
		{
			StartThirdPartyPurchase startThirdPartyPurchase = new StartThirdPartyPurchase();
			StartThirdPartyPurchase.DeserializeLengthDelimited(stream, startThirdPartyPurchase);
			return startThirdPartyPurchase;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001B104 File Offset: 0x00019304
		public static StartThirdPartyPurchase DeserializeLengthDelimited(Stream stream, StartThirdPartyPurchase instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return StartThirdPartyPurchase.Deserialize(stream, instance, num);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001B12C File Offset: 0x0001932C
		public static StartThirdPartyPurchase Deserialize(Stream stream, StartThirdPartyPurchase instance, long limit)
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
							instance.Provider = (BattlePayProvider)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							instance.ProductId = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num != 34)
						{
							if (num == 42)
							{
								instance.DeviceId = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.DanglingReceiptData == null)
							{
								instance.DanglingReceiptData = ThirdPartyReceiptData.DeserializeLengthDelimited(stream);
								continue;
							}
							ThirdPartyReceiptData.DeserializeLengthDelimited(stream, instance.DanglingReceiptData);
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

		// Token: 0x0600075D RID: 1885 RVA: 0x0001B22E File Offset: 0x0001942E
		public void Serialize(Stream stream)
		{
			StartThirdPartyPurchase.Serialize(stream, this);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001B238 File Offset: 0x00019438
		public static void Serialize(Stream stream, StartThirdPartyPurchase instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Provider));
			if (instance.ProductId == null)
			{
				throw new ArgumentNullException("ProductId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProductId));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Quantity));
			if (instance.HasDanglingReceiptData)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.DanglingReceiptData.GetSerializedSize());
				ThirdPartyReceiptData.Serialize(stream, instance.DanglingReceiptData);
			}
			if (instance.DeviceId == null)
			{
				throw new ArgumentNullException("DeviceId", "Required by proto specification.");
			}
			stream.WriteByte(42);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceId));
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001B308 File Offset: 0x00019508
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Provider));
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ProductId);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Quantity));
			if (this.HasDanglingReceiptData)
			{
				num += 1U;
				uint serializedSize = this.DanglingReceiptData.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.DeviceId);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			return num + 4U;
		}

		// Token: 0x04000256 RID: 598
		public bool HasDanglingReceiptData;

		// Token: 0x04000257 RID: 599
		private ThirdPartyReceiptData _DanglingReceiptData;

		// Token: 0x02000587 RID: 1415
		public enum PacketID
		{
			// Token: 0x04001EF2 RID: 7922
			ID = 312,
			// Token: 0x04001EF3 RID: 7923
			System = 1
		}
	}
}
