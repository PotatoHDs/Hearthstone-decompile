using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200006B RID: 107
	public class PurchaseWithGold : IProtoBuf
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00019716 File Offset: 0x00017916
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x0001971E File Offset: 0x0001791E
		public int Quantity { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00019727 File Offset: 0x00017927
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0001972F File Offset: 0x0001792F
		public ProductType Product { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00019738 File Offset: 0x00017938
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x00019740 File Offset: 0x00017940
		public int Data
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

		// Token: 0x060006C3 RID: 1731 RVA: 0x00019750 File Offset: 0x00017950
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Quantity.GetHashCode();
			num ^= this.Product.GetHashCode();
			if (this.HasData)
			{
				num ^= this.Data.GetHashCode();
			}
			return num;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000197AC File Offset: 0x000179AC
		public override bool Equals(object obj)
		{
			PurchaseWithGold purchaseWithGold = obj as PurchaseWithGold;
			return purchaseWithGold != null && this.Quantity.Equals(purchaseWithGold.Quantity) && this.Product.Equals(purchaseWithGold.Product) && this.HasData == purchaseWithGold.HasData && (!this.HasData || this.Data.Equals(purchaseWithGold.Data));
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001982F File Offset: 0x00017A2F
		public void Deserialize(Stream stream)
		{
			PurchaseWithGold.Deserialize(stream, this);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00019839 File Offset: 0x00017A39
		public static PurchaseWithGold Deserialize(Stream stream, PurchaseWithGold instance)
		{
			return PurchaseWithGold.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00019844 File Offset: 0x00017A44
		public static PurchaseWithGold DeserializeLengthDelimited(Stream stream)
		{
			PurchaseWithGold purchaseWithGold = new PurchaseWithGold();
			PurchaseWithGold.DeserializeLengthDelimited(stream, purchaseWithGold);
			return purchaseWithGold;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00019860 File Offset: 0x00017A60
		public static PurchaseWithGold DeserializeLengthDelimited(Stream stream, PurchaseWithGold instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PurchaseWithGold.Deserialize(stream, instance, num);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00019888 File Offset: 0x00017A88
		public static PurchaseWithGold Deserialize(Stream stream, PurchaseWithGold instance, long limit)
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
						if (num != 24)
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
							instance.Data = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.Product = (ProductType)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00019938 File Offset: 0x00017B38
		public void Serialize(Stream stream)
		{
			PurchaseWithGold.Serialize(stream, this);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00019944 File Offset: 0x00017B44
		public static void Serialize(Stream stream, PurchaseWithGold instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Quantity));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Product));
			if (instance.HasData)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Data));
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00019998 File Offset: 0x00017B98
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Quantity));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Product));
			if (this.HasData)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Data));
			}
			return num + 2U;
		}

		// Token: 0x04000239 RID: 569
		public bool HasData;

		// Token: 0x0400023A RID: 570
		private int _Data;

		// Token: 0x0200057E RID: 1406
		public enum PacketID
		{
			// Token: 0x04001ED6 RID: 7894
			ID = 279,
			// Token: 0x04001ED7 RID: 7895
			System = 0
		}
	}
}
