using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000064 RID: 100
	public class BuySellCard : IProtoBuf
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001854C File Offset: 0x0001674C
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x00018554 File Offset: 0x00016754
		public CardDef Def { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001855D File Offset: 0x0001675D
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x00018565 File Offset: 0x00016765
		public int Count
		{
			get
			{
				return this._Count;
			}
			set
			{
				this._Count = value;
				this.HasCount = true;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00018575 File Offset: 0x00016775
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x0001857D File Offset: 0x0001677D
		public bool Buying { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00018586 File Offset: 0x00016786
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0001858E File Offset: 0x0001678E
		public int UnitSellPrice
		{
			get
			{
				return this._UnitSellPrice;
			}
			set
			{
				this._UnitSellPrice = value;
				this.HasUnitSellPrice = true;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001859E File Offset: 0x0001679E
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x000185A6 File Offset: 0x000167A6
		public int UnitBuyPrice
		{
			get
			{
				return this._UnitBuyPrice;
			}
			set
			{
				this._UnitBuyPrice = value;
				this.HasUnitBuyPrice = true;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x000185B6 File Offset: 0x000167B6
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x000185BE File Offset: 0x000167BE
		public int CurrentCollectionCount
		{
			get
			{
				return this._CurrentCollectionCount;
			}
			set
			{
				this._CurrentCollectionCount = value;
				this.HasCurrentCollectionCount = true;
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x000185D0 File Offset: 0x000167D0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Def.GetHashCode();
			if (this.HasCount)
			{
				num ^= this.Count.GetHashCode();
			}
			num ^= this.Buying.GetHashCode();
			if (this.HasUnitSellPrice)
			{
				num ^= this.UnitSellPrice.GetHashCode();
			}
			if (this.HasUnitBuyPrice)
			{
				num ^= this.UnitBuyPrice.GetHashCode();
			}
			if (this.HasCurrentCollectionCount)
			{
				num ^= this.CurrentCollectionCount.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00018670 File Offset: 0x00016870
		public override bool Equals(object obj)
		{
			BuySellCard buySellCard = obj as BuySellCard;
			return buySellCard != null && this.Def.Equals(buySellCard.Def) && this.HasCount == buySellCard.HasCount && (!this.HasCount || this.Count.Equals(buySellCard.Count)) && this.Buying.Equals(buySellCard.Buying) && this.HasUnitSellPrice == buySellCard.HasUnitSellPrice && (!this.HasUnitSellPrice || this.UnitSellPrice.Equals(buySellCard.UnitSellPrice)) && this.HasUnitBuyPrice == buySellCard.HasUnitBuyPrice && (!this.HasUnitBuyPrice || this.UnitBuyPrice.Equals(buySellCard.UnitBuyPrice)) && this.HasCurrentCollectionCount == buySellCard.HasCurrentCollectionCount && (!this.HasCurrentCollectionCount || this.CurrentCollectionCount.Equals(buySellCard.CurrentCollectionCount));
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001876F File Offset: 0x0001696F
		public void Deserialize(Stream stream)
		{
			BuySellCard.Deserialize(stream, this);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00018779 File Offset: 0x00016979
		public static BuySellCard Deserialize(Stream stream, BuySellCard instance)
		{
			return BuySellCard.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00018784 File Offset: 0x00016984
		public static BuySellCard DeserializeLengthDelimited(Stream stream)
		{
			BuySellCard buySellCard = new BuySellCard();
			BuySellCard.DeserializeLengthDelimited(stream, buySellCard);
			return buySellCard;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000187A0 File Offset: 0x000169A0
		public static BuySellCard DeserializeLengthDelimited(Stream stream, BuySellCard instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BuySellCard.Deserialize(stream, instance, num);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x000187C8 File Offset: 0x000169C8
		public static BuySellCard Deserialize(Stream stream, BuySellCard instance, long limit)
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
					if (num <= 24)
					{
						if (num != 10)
						{
							if (num == 16)
							{
								instance.Count = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 24)
							{
								instance.Buying = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (instance.Def == null)
							{
								instance.Def = CardDef.DeserializeLengthDelimited(stream);
								continue;
							}
							CardDef.DeserializeLengthDelimited(stream, instance.Def);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.UnitSellPrice = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.UnitBuyPrice = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.CurrentCollectionCount = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000661 RID: 1633 RVA: 0x000188EF File Offset: 0x00016AEF
		public void Serialize(Stream stream)
		{
			BuySellCard.Serialize(stream, this);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000188F8 File Offset: 0x00016AF8
		public static void Serialize(Stream stream, BuySellCard instance)
		{
			if (instance.Def == null)
			{
				throw new ArgumentNullException("Def", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Def.GetSerializedSize());
			CardDef.Serialize(stream, instance.Def);
			if (instance.HasCount)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Count));
			}
			stream.WriteByte(24);
			ProtocolParser.WriteBool(stream, instance.Buying);
			if (instance.HasUnitSellPrice)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.UnitSellPrice));
			}
			if (instance.HasUnitBuyPrice)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.UnitBuyPrice));
			}
			if (instance.HasCurrentCollectionCount)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CurrentCollectionCount));
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000189CC File Offset: 0x00016BCC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Def.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Count));
			}
			num += 1U;
			if (this.HasUnitSellPrice)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.UnitSellPrice));
			}
			if (this.HasUnitBuyPrice)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.UnitBuyPrice));
			}
			if (this.HasCurrentCollectionCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CurrentCollectionCount));
			}
			return num + 2U;
		}

		// Token: 0x0400021F RID: 543
		public bool HasCount;

		// Token: 0x04000220 RID: 544
		private int _Count;

		// Token: 0x04000222 RID: 546
		public bool HasUnitSellPrice;

		// Token: 0x04000223 RID: 547
		private int _UnitSellPrice;

		// Token: 0x04000224 RID: 548
		public bool HasUnitBuyPrice;

		// Token: 0x04000225 RID: 549
		private int _UnitBuyPrice;

		// Token: 0x04000226 RID: 550
		public bool HasCurrentCollectionCount;

		// Token: 0x04000227 RID: 551
		private int _CurrentCollectionCount;

		// Token: 0x02000576 RID: 1398
		public enum PacketID
		{
			// Token: 0x04001EB8 RID: 7864
			ID = 257,
			// Token: 0x04001EB9 RID: 7865
			System = 0
		}
	}
}
