using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000065 RID: 101
	public class GetPurchaseMethod : IProtoBuf
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00018A67 File Offset: 0x00016C67
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x00018A6F File Offset: 0x00016C6F
		public long PmtProductId { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00018A78 File Offset: 0x00016C78
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x00018A80 File Offset: 0x00016C80
		public int Quantity { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00018A89 File Offset: 0x00016C89
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x00018A91 File Offset: 0x00016C91
		public int CurrencyDeprecated { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00018A9A File Offset: 0x00016C9A
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x00018AA2 File Offset: 0x00016CA2
		public string DeviceId { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00018AAB File Offset: 0x00016CAB
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x00018AB3 File Offset: 0x00016CB3
		public Platform Platform
		{
			get
			{
				return this._Platform;
			}
			set
			{
				this._Platform = value;
				this.HasPlatform = (value != null);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00018AC6 File Offset: 0x00016CC6
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x00018ACE File Offset: 0x00016CCE
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

		// Token: 0x06000671 RID: 1649 RVA: 0x00018AE4 File Offset: 0x00016CE4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.PmtProductId.GetHashCode();
			num ^= this.Quantity.GetHashCode();
			num ^= this.CurrencyDeprecated.GetHashCode();
			num ^= this.DeviceId.GetHashCode();
			if (this.HasPlatform)
			{
				num ^= this.Platform.GetHashCode();
			}
			if (this.HasCurrencyCode)
			{
				num ^= this.CurrencyCode.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00018B6C File Offset: 0x00016D6C
		public override bool Equals(object obj)
		{
			GetPurchaseMethod getPurchaseMethod = obj as GetPurchaseMethod;
			return getPurchaseMethod != null && this.PmtProductId.Equals(getPurchaseMethod.PmtProductId) && this.Quantity.Equals(getPurchaseMethod.Quantity) && this.CurrencyDeprecated.Equals(getPurchaseMethod.CurrencyDeprecated) && this.DeviceId.Equals(getPurchaseMethod.DeviceId) && this.HasPlatform == getPurchaseMethod.HasPlatform && (!this.HasPlatform || this.Platform.Equals(getPurchaseMethod.Platform)) && this.HasCurrencyCode == getPurchaseMethod.HasCurrencyCode && (!this.HasCurrencyCode || this.CurrencyCode.Equals(getPurchaseMethod.CurrencyCode));
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00018C39 File Offset: 0x00016E39
		public void Deserialize(Stream stream)
		{
			GetPurchaseMethod.Deserialize(stream, this);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00018C43 File Offset: 0x00016E43
		public static GetPurchaseMethod Deserialize(Stream stream, GetPurchaseMethod instance)
		{
			return GetPurchaseMethod.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00018C50 File Offset: 0x00016E50
		public static GetPurchaseMethod DeserializeLengthDelimited(Stream stream)
		{
			GetPurchaseMethod getPurchaseMethod = new GetPurchaseMethod();
			GetPurchaseMethod.DeserializeLengthDelimited(stream, getPurchaseMethod);
			return getPurchaseMethod;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00018C6C File Offset: 0x00016E6C
		public static GetPurchaseMethod DeserializeLengthDelimited(Stream stream, GetPurchaseMethod instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetPurchaseMethod.Deserialize(stream, instance, num);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00018C94 File Offset: 0x00016E94
		public static GetPurchaseMethod Deserialize(Stream stream, GetPurchaseMethod instance, long limit)
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
						if (num == 24)
						{
							instance.CurrencyDeprecated = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 34)
						{
							instance.DeviceId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num != 42)
						{
							if (num == 50)
							{
								instance.CurrencyCode = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.Platform == null)
							{
								instance.Platform = Platform.DeserializeLengthDelimited(stream);
								continue;
							}
							Platform.DeserializeLengthDelimited(stream, instance.Platform);
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

		// Token: 0x06000678 RID: 1656 RVA: 0x00018DBD File Offset: 0x00016FBD
		public void Serialize(Stream stream)
		{
			GetPurchaseMethod.Serialize(stream, this);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00018DC8 File Offset: 0x00016FC8
		public static void Serialize(Stream stream, GetPurchaseMethod instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PmtProductId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Quantity));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CurrencyDeprecated));
			if (instance.DeviceId == null)
			{
				throw new ArgumentNullException("DeviceId", "Required by proto specification.");
			}
			stream.WriteByte(34);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceId));
			if (instance.HasPlatform)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
				Platform.Serialize(stream, instance.Platform);
			}
			if (instance.HasCurrencyCode)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00018E9C File Offset: 0x0001709C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.PmtProductId);
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Quantity));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CurrencyDeprecated));
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.DeviceId);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.HasPlatform)
			{
				num += 1U;
				uint serializedSize = this.Platform.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasCurrencyCode)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.CurrencyCode);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 4U;
		}

		// Token: 0x0400022C RID: 556
		public bool HasPlatform;

		// Token: 0x0400022D RID: 557
		private Platform _Platform;

		// Token: 0x0400022E RID: 558
		public bool HasCurrencyCode;

		// Token: 0x0400022F RID: 559
		private string _CurrencyCode;

		// Token: 0x02000577 RID: 1399
		public enum PacketID
		{
			// Token: 0x04001EBB RID: 7867
			ID = 250,
			// Token: 0x04001EBC RID: 7868
			System = 1
		}
	}
}
