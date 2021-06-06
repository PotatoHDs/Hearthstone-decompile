using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x02000142 RID: 322
	public class DeckInfo : IProtoBuf
	{
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x0004824D File Offset: 0x0004644D
		// (set) Token: 0x0600151F RID: 5407 RVA: 0x00048255 File Offset: 0x00046455
		public long Id { get; set; }

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0004825E File Offset: 0x0004645E
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x00048266 File Offset: 0x00046466
		public string Name { get; set; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x0004826F File Offset: 0x0004646F
		// (set) Token: 0x06001523 RID: 5411 RVA: 0x00048277 File Offset: 0x00046477
		public int CardBack { get; set; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x00048280 File Offset: 0x00046480
		// (set) Token: 0x06001525 RID: 5413 RVA: 0x00048288 File Offset: 0x00046488
		public int Hero { get; set; }

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x00048291 File Offset: 0x00046491
		// (set) Token: 0x06001527 RID: 5415 RVA: 0x00048299 File Offset: 0x00046499
		public DeckType DeckType { get; set; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x000482A2 File Offset: 0x000464A2
		// (set) Token: 0x06001529 RID: 5417 RVA: 0x000482AA File Offset: 0x000464AA
		public ulong Validity { get; set; }

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x000482B3 File Offset: 0x000464B3
		// (set) Token: 0x0600152B RID: 5419 RVA: 0x000482BB File Offset: 0x000464BB
		public int HeroPremium { get; set; }

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x000482C4 File Offset: 0x000464C4
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x000482CC File Offset: 0x000464CC
		public bool CardBackOverride { get; set; }

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x000482D5 File Offset: 0x000464D5
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x000482DD File Offset: 0x000464DD
		public bool HeroOverride { get; set; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x000482E6 File Offset: 0x000464E6
		// (set) Token: 0x06001531 RID: 5425 RVA: 0x000482EE File Offset: 0x000464EE
		public long LastModified
		{
			get
			{
				return this._LastModified;
			}
			set
			{
				this._LastModified = value;
				this.HasLastModified = true;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x000482FE File Offset: 0x000464FE
		// (set) Token: 0x06001533 RID: 5427 RVA: 0x00048306 File Offset: 0x00046506
		public int SeasonId
		{
			get
			{
				return this._SeasonId;
			}
			set
			{
				this._SeasonId = value;
				this.HasSeasonId = true;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00048316 File Offset: 0x00046516
		// (set) Token: 0x06001535 RID: 5429 RVA: 0x0004831E File Offset: 0x0004651E
		public long SortOrder
		{
			get
			{
				return this._SortOrder;
			}
			set
			{
				this._SortOrder = value;
				this.HasSortOrder = true;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x0004832E File Offset: 0x0004652E
		// (set) Token: 0x06001537 RID: 5431 RVA: 0x00048336 File Offset: 0x00046536
		public long CreateDate
		{
			get
			{
				return this._CreateDate;
			}
			set
			{
				this._CreateDate = value;
				this.HasCreateDate = true;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00048346 File Offset: 0x00046546
		// (set) Token: 0x06001539 RID: 5433 RVA: 0x0004834E File Offset: 0x0004654E
		public DeckSourceType SourceType
		{
			get
			{
				return this._SourceType;
			}
			set
			{
				this._SourceType = value;
				this.HasSourceType = true;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0004835E File Offset: 0x0004655E
		// (set) Token: 0x0600153B RID: 5435 RVA: 0x00048366 File Offset: 0x00046566
		public string PastedDeckHash
		{
			get
			{
				return this._PastedDeckHash;
			}
			set
			{
				this._PastedDeckHash = value;
				this.HasPastedDeckHash = (value != null);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x00048379 File Offset: 0x00046579
		// (set) Token: 0x0600153D RID: 5437 RVA: 0x00048381 File Offset: 0x00046581
		public int BrawlLibraryItemId
		{
			get
			{
				return this._BrawlLibraryItemId;
			}
			set
			{
				this._BrawlLibraryItemId = value;
				this.HasBrawlLibraryItemId = true;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x00048391 File Offset: 0x00046591
		// (set) Token: 0x0600153F RID: 5439 RVA: 0x00048399 File Offset: 0x00046599
		public int UiHeroOverride
		{
			get
			{
				return this._UiHeroOverride;
			}
			set
			{
				this._UiHeroOverride = value;
				this.HasUiHeroOverride = true;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x000483A9 File Offset: 0x000465A9
		// (set) Token: 0x06001541 RID: 5441 RVA: 0x000483B1 File Offset: 0x000465B1
		public int UiHeroOverridePremium
		{
			get
			{
				return this._UiHeroOverridePremium;
			}
			set
			{
				this._UiHeroOverridePremium = value;
				this.HasUiHeroOverridePremium = true;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x000483C1 File Offset: 0x000465C1
		// (set) Token: 0x06001543 RID: 5443 RVA: 0x000483C9 File Offset: 0x000465C9
		public FormatType FormatType
		{
			get
			{
				return this._FormatType;
			}
			set
			{
				this._FormatType = value;
				this.HasFormatType = true;
			}
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x000483DC File Offset: 0x000465DC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			num ^= this.Name.GetHashCode();
			num ^= this.CardBack.GetHashCode();
			num ^= this.Hero.GetHashCode();
			num ^= this.DeckType.GetHashCode();
			num ^= this.Validity.GetHashCode();
			num ^= this.HeroPremium.GetHashCode();
			num ^= this.CardBackOverride.GetHashCode();
			num ^= this.HeroOverride.GetHashCode();
			if (this.HasLastModified)
			{
				num ^= this.LastModified.GetHashCode();
			}
			if (this.HasSeasonId)
			{
				num ^= this.SeasonId.GetHashCode();
			}
			if (this.HasSortOrder)
			{
				num ^= this.SortOrder.GetHashCode();
			}
			if (this.HasCreateDate)
			{
				num ^= this.CreateDate.GetHashCode();
			}
			if (this.HasSourceType)
			{
				num ^= this.SourceType.GetHashCode();
			}
			if (this.HasPastedDeckHash)
			{
				num ^= this.PastedDeckHash.GetHashCode();
			}
			if (this.HasBrawlLibraryItemId)
			{
				num ^= this.BrawlLibraryItemId.GetHashCode();
			}
			if (this.HasUiHeroOverride)
			{
				num ^= this.UiHeroOverride.GetHashCode();
			}
			if (this.HasUiHeroOverridePremium)
			{
				num ^= this.UiHeroOverridePremium.GetHashCode();
			}
			if (this.HasFormatType)
			{
				num ^= this.FormatType.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0004859C File Offset: 0x0004679C
		public override bool Equals(object obj)
		{
			DeckInfo deckInfo = obj as DeckInfo;
			return deckInfo != null && this.Id.Equals(deckInfo.Id) && this.Name.Equals(deckInfo.Name) && this.CardBack.Equals(deckInfo.CardBack) && this.Hero.Equals(deckInfo.Hero) && this.DeckType.Equals(deckInfo.DeckType) && this.Validity.Equals(deckInfo.Validity) && this.HeroPremium.Equals(deckInfo.HeroPremium) && this.CardBackOverride.Equals(deckInfo.CardBackOverride) && this.HeroOverride.Equals(deckInfo.HeroOverride) && this.HasLastModified == deckInfo.HasLastModified && (!this.HasLastModified || this.LastModified.Equals(deckInfo.LastModified)) && this.HasSeasonId == deckInfo.HasSeasonId && (!this.HasSeasonId || this.SeasonId.Equals(deckInfo.SeasonId)) && this.HasSortOrder == deckInfo.HasSortOrder && (!this.HasSortOrder || this.SortOrder.Equals(deckInfo.SortOrder)) && this.HasCreateDate == deckInfo.HasCreateDate && (!this.HasCreateDate || this.CreateDate.Equals(deckInfo.CreateDate)) && this.HasSourceType == deckInfo.HasSourceType && (!this.HasSourceType || this.SourceType.Equals(deckInfo.SourceType)) && this.HasPastedDeckHash == deckInfo.HasPastedDeckHash && (!this.HasPastedDeckHash || this.PastedDeckHash.Equals(deckInfo.PastedDeckHash)) && this.HasBrawlLibraryItemId == deckInfo.HasBrawlLibraryItemId && (!this.HasBrawlLibraryItemId || this.BrawlLibraryItemId.Equals(deckInfo.BrawlLibraryItemId)) && this.HasUiHeroOverride == deckInfo.HasUiHeroOverride && (!this.HasUiHeroOverride || this.UiHeroOverride.Equals(deckInfo.UiHeroOverride)) && this.HasUiHeroOverridePremium == deckInfo.HasUiHeroOverridePremium && (!this.HasUiHeroOverridePremium || this.UiHeroOverridePremium.Equals(deckInfo.UiHeroOverridePremium)) && this.HasFormatType == deckInfo.HasFormatType && (!this.HasFormatType || this.FormatType.Equals(deckInfo.FormatType));
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0004887A File Offset: 0x00046A7A
		public void Deserialize(Stream stream)
		{
			DeckInfo.Deserialize(stream, this);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00048884 File Offset: 0x00046A84
		public static DeckInfo Deserialize(Stream stream, DeckInfo instance)
		{
			return DeckInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00048890 File Offset: 0x00046A90
		public static DeckInfo DeserializeLengthDelimited(Stream stream)
		{
			DeckInfo deckInfo = new DeckInfo();
			DeckInfo.DeserializeLengthDelimited(stream, deckInfo);
			return deckInfo;
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x000488AC File Offset: 0x00046AAC
		public static DeckInfo DeserializeLengthDelimited(Stream stream, DeckInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x000488D4 File Offset: 0x00046AD4
		public static DeckInfo Deserialize(Stream stream, DeckInfo instance, long limit)
		{
			instance.SourceType = DeckSourceType.DECK_SOURCE_TYPE_UNKNOWN;
			instance.FormatType = FormatType.FT_UNKNOWN;
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
					if (num <= 56)
					{
						if (num <= 24)
						{
							if (num == 8)
							{
								instance.Id = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 18)
							{
								instance.Name = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 24)
							{
								instance.CardBack = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else if (num <= 40)
						{
							if (num == 32)
							{
								instance.Hero = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 40)
							{
								instance.DeckType = (DeckType)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 48)
							{
								instance.Validity = ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 56)
							{
								instance.HeroPremium = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 88)
					{
						if (num <= 72)
						{
							if (num == 64)
							{
								instance.CardBackOverride = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 72)
							{
								instance.HeroOverride = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (num == 80)
							{
								instance.LastModified = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 88)
							{
								instance.SeasonId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 104)
					{
						if (num == 96)
						{
							instance.SortOrder = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 104)
						{
							instance.CreateDate = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 112)
						{
							instance.SourceType = (DeckSourceType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 122)
						{
							instance.PastedDeckHash = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					switch (field)
					{
					case 16U:
						if (key.WireType == Wire.Varint)
						{
							instance.BrawlLibraryItemId = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 17U:
						if (key.WireType == Wire.Varint)
						{
							instance.UiHeroOverride = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 18U:
						if (key.WireType == Wire.Varint)
						{
							instance.UiHeroOverridePremium = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 19U:
						if (key.WireType == Wire.Varint)
						{
							instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00048BA4 File Offset: 0x00046DA4
		public void Serialize(Stream stream)
		{
			DeckInfo.Serialize(stream, this);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00048BB0 File Offset: 0x00046DB0
		public static void Serialize(Stream stream, DeckInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CardBack));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Hero));
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeckType));
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, instance.Validity);
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.HeroPremium));
			stream.WriteByte(64);
			ProtocolParser.WriteBool(stream, instance.CardBackOverride);
			stream.WriteByte(72);
			ProtocolParser.WriteBool(stream, instance.HeroOverride);
			if (instance.HasLastModified)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.LastModified);
			}
			if (instance.HasSeasonId)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SeasonId));
			}
			if (instance.HasSortOrder)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SortOrder);
			}
			if (instance.HasCreateDate)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CreateDate);
			}
			if (instance.HasSourceType)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SourceType));
			}
			if (instance.HasPastedDeckHash)
			{
				stream.WriteByte(122);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PastedDeckHash));
			}
			if (instance.HasBrawlLibraryItemId)
			{
				stream.WriteByte(128);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BrawlLibraryItemId));
			}
			if (instance.HasUiHeroOverride)
			{
				stream.WriteByte(136);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.UiHeroOverride));
			}
			if (instance.HasUiHeroOverridePremium)
			{
				stream.WriteByte(144);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.UiHeroOverridePremium));
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(152);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00048DE8 File Offset: 0x00046FE8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.Id);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CardBack));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Hero));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeckType));
			num += ProtocolParser.SizeOfUInt64(this.Validity);
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.HeroPremium));
			num += 1U;
			num += 1U;
			if (this.HasLastModified)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.LastModified);
			}
			if (this.HasSeasonId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SeasonId));
			}
			if (this.HasSortOrder)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.SortOrder);
			}
			if (this.HasCreateDate)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.CreateDate);
			}
			if (this.HasSourceType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SourceType));
			}
			if (this.HasPastedDeckHash)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.PastedDeckHash);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasBrawlLibraryItemId)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BrawlLibraryItemId));
			}
			if (this.HasUiHeroOverride)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.UiHeroOverride));
			}
			if (this.HasUiHeroOverridePremium)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.UiHeroOverridePremium));
			}
			if (this.HasFormatType)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			}
			return num + 9U;
		}

		// Token: 0x04000676 RID: 1654
		public bool HasLastModified;

		// Token: 0x04000677 RID: 1655
		private long _LastModified;

		// Token: 0x04000678 RID: 1656
		public bool HasSeasonId;

		// Token: 0x04000679 RID: 1657
		private int _SeasonId;

		// Token: 0x0400067A RID: 1658
		public bool HasSortOrder;

		// Token: 0x0400067B RID: 1659
		private long _SortOrder;

		// Token: 0x0400067C RID: 1660
		public bool HasCreateDate;

		// Token: 0x0400067D RID: 1661
		private long _CreateDate;

		// Token: 0x0400067E RID: 1662
		public bool HasSourceType;

		// Token: 0x0400067F RID: 1663
		private DeckSourceType _SourceType;

		// Token: 0x04000680 RID: 1664
		public bool HasPastedDeckHash;

		// Token: 0x04000681 RID: 1665
		private string _PastedDeckHash;

		// Token: 0x04000682 RID: 1666
		public bool HasBrawlLibraryItemId;

		// Token: 0x04000683 RID: 1667
		private int _BrawlLibraryItemId;

		// Token: 0x04000684 RID: 1668
		public bool HasUiHeroOverride;

		// Token: 0x04000685 RID: 1669
		private int _UiHeroOverride;

		// Token: 0x04000686 RID: 1670
		public bool HasUiHeroOverridePremium;

		// Token: 0x04000687 RID: 1671
		private int _UiHeroOverridePremium;

		// Token: 0x04000688 RID: 1672
		public bool HasFormatType;

		// Token: 0x04000689 RID: 1673
		private FormatType _FormatType;

		// Token: 0x02000637 RID: 1591
		public enum ValidityFlags
		{
			// Token: 0x040020CF RID: 8399
			UNLOCKED_HERO_CLASS = 1,
			// Token: 0x040020D0 RID: 8400
			OWNS_CARDS,
			// Token: 0x040020D1 RID: 8401
			HAS_30_CARDS = 4,
			// Token: 0x040020D2 RID: 8402
			OBEYS_MAXES = 8,
			// Token: 0x040020D3 RID: 8403
			CLASS_MATCHES = 16,
			// Token: 0x040020D4 RID: 8404
			OWNS_CARD_BACK = 32,
			// Token: 0x040020D5 RID: 8405
			OWNS_HERO = 64,
			// Token: 0x040020D6 RID: 8406
			TAGGED_STANDARD = 128,
			// Token: 0x040020D7 RID: 8407
			NEEDS_VALIDATION = 256,
			// Token: 0x040020D8 RID: 8408
			NEEDS_NAME = 512,
			// Token: 0x040020D9 RID: 8409
			LOCKED_DECK = 1024
		}
	}
}
