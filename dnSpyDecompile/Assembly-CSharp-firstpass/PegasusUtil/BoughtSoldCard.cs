using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x0200008A RID: 138
	public class BoughtSoldCard : IProtoBuf
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00021BA7 File Offset: 0x0001FDA7
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x00021BAF File Offset: 0x0001FDAF
		public CardDef Def { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00021BB8 File Offset: 0x0001FDB8
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x00021BC0 File Offset: 0x0001FDC0
		public int Amount { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x00021BC9 File Offset: 0x0001FDC9
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x00021BD1 File Offset: 0x0001FDD1
		public BoughtSoldCard.Result Result_ { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00021BDA File Offset: 0x0001FDDA
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x00021BE2 File Offset: 0x0001FDE2
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

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00021BF2 File Offset: 0x0001FDF2
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x00021BFA File Offset: 0x0001FDFA
		public bool Nerfed
		{
			get
			{
				return this._Nerfed;
			}
			set
			{
				this._Nerfed = value;
				this.HasNerfed = true;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00021C0A File Offset: 0x0001FE0A
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x00021C12 File Offset: 0x0001FE12
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

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x00021C22 File Offset: 0x0001FE22
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x00021C2A File Offset: 0x0001FE2A
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

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00021C3A File Offset: 0x0001FE3A
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x00021C42 File Offset: 0x0001FE42
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

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00021C52 File Offset: 0x0001FE52
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x00021C5A File Offset: 0x0001FE5A
		public long CollectionVersion
		{
			get
			{
				return this._CollectionVersion;
			}
			set
			{
				this._CollectionVersion = value;
				this.HasCollectionVersion = true;
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00021C6C File Offset: 0x0001FE6C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Def.GetHashCode();
			num ^= this.Amount.GetHashCode();
			num ^= this.Result_.GetHashCode();
			if (this.HasCount)
			{
				num ^= this.Count.GetHashCode();
			}
			if (this.HasNerfed)
			{
				num ^= this.Nerfed.GetHashCode();
			}
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
			if (this.HasCollectionVersion)
			{
				num ^= this.CollectionVersion.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00021D54 File Offset: 0x0001FF54
		public override bool Equals(object obj)
		{
			BoughtSoldCard boughtSoldCard = obj as BoughtSoldCard;
			return boughtSoldCard != null && this.Def.Equals(boughtSoldCard.Def) && this.Amount.Equals(boughtSoldCard.Amount) && this.Result_.Equals(boughtSoldCard.Result_) && this.HasCount == boughtSoldCard.HasCount && (!this.HasCount || this.Count.Equals(boughtSoldCard.Count)) && this.HasNerfed == boughtSoldCard.HasNerfed && (!this.HasNerfed || this.Nerfed.Equals(boughtSoldCard.Nerfed)) && this.HasUnitSellPrice == boughtSoldCard.HasUnitSellPrice && (!this.HasUnitSellPrice || this.UnitSellPrice.Equals(boughtSoldCard.UnitSellPrice)) && this.HasUnitBuyPrice == boughtSoldCard.HasUnitBuyPrice && (!this.HasUnitBuyPrice || this.UnitBuyPrice.Equals(boughtSoldCard.UnitBuyPrice)) && this.HasCurrentCollectionCount == boughtSoldCard.HasCurrentCollectionCount && (!this.HasCurrentCollectionCount || this.CurrentCollectionCount.Equals(boughtSoldCard.CurrentCollectionCount)) && this.HasCollectionVersion == boughtSoldCard.HasCollectionVersion && (!this.HasCollectionVersion || this.CollectionVersion.Equals(boughtSoldCard.CollectionVersion));
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00021ED3 File Offset: 0x000200D3
		public void Deserialize(Stream stream)
		{
			BoughtSoldCard.Deserialize(stream, this);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00021EDD File Offset: 0x000200DD
		public static BoughtSoldCard Deserialize(Stream stream, BoughtSoldCard instance)
		{
			return BoughtSoldCard.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00021EE8 File Offset: 0x000200E8
		public static BoughtSoldCard DeserializeLengthDelimited(Stream stream)
		{
			BoughtSoldCard boughtSoldCard = new BoughtSoldCard();
			BoughtSoldCard.DeserializeLengthDelimited(stream, boughtSoldCard);
			return boughtSoldCard;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00021F04 File Offset: 0x00020104
		public static BoughtSoldCard DeserializeLengthDelimited(Stream stream, BoughtSoldCard instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BoughtSoldCard.Deserialize(stream, instance, num);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00021F2C File Offset: 0x0002012C
		public static BoughtSoldCard Deserialize(Stream stream, BoughtSoldCard instance, long limit)
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
					if (num <= 32)
					{
						if (num <= 16)
						{
							if (num != 10)
							{
								if (num == 16)
								{
									instance.Amount = (int)ProtocolParser.ReadUInt64(stream);
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
							if (num == 24)
							{
								instance.Result_ = (BoughtSoldCard.Result)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 32)
							{
								instance.Count = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 48)
					{
						if (num == 40)
						{
							instance.Nerfed = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 48)
						{
							instance.UnitSellPrice = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.UnitBuyPrice = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 64)
						{
							instance.CurrentCollectionCount = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 72)
						{
							instance.CollectionVersion = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000941 RID: 2369 RVA: 0x000220C0 File Offset: 0x000202C0
		public void Serialize(Stream stream)
		{
			BoughtSoldCard.Serialize(stream, this);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x000220CC File Offset: 0x000202CC
		public static void Serialize(Stream stream, BoughtSoldCard instance)
		{
			if (instance.Def == null)
			{
				throw new ArgumentNullException("Def", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Def.GetSerializedSize());
			CardDef.Serialize(stream, instance.Def);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Amount));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Result_));
			if (instance.HasCount)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Count));
			}
			if (instance.HasNerfed)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.Nerfed);
			}
			if (instance.HasUnitSellPrice)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.UnitSellPrice));
			}
			if (instance.HasUnitBuyPrice)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.UnitBuyPrice));
			}
			if (instance.HasCurrentCollectionCount)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CurrentCollectionCount));
			}
			if (instance.HasCollectionVersion)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CollectionVersion);
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x000221EC File Offset: 0x000203EC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Def.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Amount));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Result_));
			if (this.HasCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Count));
			}
			if (this.HasNerfed)
			{
				num += 1U;
				num += 1U;
			}
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
			if (this.HasCollectionVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.CollectionVersion);
			}
			return num + 3U;
		}

		// Token: 0x0400032F RID: 815
		public bool HasCount;

		// Token: 0x04000330 RID: 816
		private int _Count;

		// Token: 0x04000331 RID: 817
		public bool HasNerfed;

		// Token: 0x04000332 RID: 818
		private bool _Nerfed;

		// Token: 0x04000333 RID: 819
		public bool HasUnitSellPrice;

		// Token: 0x04000334 RID: 820
		private int _UnitSellPrice;

		// Token: 0x04000335 RID: 821
		public bool HasUnitBuyPrice;

		// Token: 0x04000336 RID: 822
		private int _UnitBuyPrice;

		// Token: 0x04000337 RID: 823
		public bool HasCurrentCollectionCount;

		// Token: 0x04000338 RID: 824
		private int _CurrentCollectionCount;

		// Token: 0x04000339 RID: 825
		public bool HasCollectionVersion;

		// Token: 0x0400033A RID: 826
		private long _CollectionVersion;

		// Token: 0x0200059C RID: 1436
		public enum PacketID
		{
			// Token: 0x04001F2C RID: 7980
			ID = 258
		}

		// Token: 0x0200059D RID: 1437
		public enum Result
		{
			// Token: 0x04001F2E RID: 7982
			GENERIC_FAILURE = 1,
			// Token: 0x04001F2F RID: 7983
			SOLD,
			// Token: 0x04001F30 RID: 7984
			BOUGHT,
			// Token: 0x04001F31 RID: 7985
			SOULBOUND,
			// Token: 0x04001F32 RID: 7986
			WRONG_SELL_PRICE,
			// Token: 0x04001F33 RID: 7987
			WRONG_BUY_PRICE,
			// Token: 0x04001F34 RID: 7988
			NO_PERMISSION,
			// Token: 0x04001F35 RID: 7989
			EVENT_NOT_ACTIVE,
			// Token: 0x04001F36 RID: 7990
			COUNT_MISMATCH
		}
	}
}
