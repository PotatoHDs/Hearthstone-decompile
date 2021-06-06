using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000090 RID: 144
	public class DraftChoicesAndContents : IProtoBuf
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0002316A File Offset: 0x0002136A
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x00023172 File Offset: 0x00021372
		public long DeckId { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0002317B File Offset: 0x0002137B
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x00023183 File Offset: 0x00021383
		public int Slot { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0002318C File Offset: 0x0002138C
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x00023194 File Offset: 0x00021394
		public List<DeckCardData> Cards
		{
			get
			{
				return this._Cards;
			}
			set
			{
				this._Cards = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0002319D File Offset: 0x0002139D
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x000231A5 File Offset: 0x000213A5
		public int DeprecatedWins
		{
			get
			{
				return this._DeprecatedWins;
			}
			set
			{
				this._DeprecatedWins = value;
				this.HasDeprecatedWins = true;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x000231B5 File Offset: 0x000213B5
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x000231BD File Offset: 0x000213BD
		public int DeprecatedLosses
		{
			get
			{
				return this._DeprecatedLosses;
			}
			set
			{
				this._DeprecatedLosses = value;
				this.HasDeprecatedLosses = true;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x000231CD File Offset: 0x000213CD
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x000231D5 File Offset: 0x000213D5
		public RewardChest Chest
		{
			get
			{
				return this._Chest;
			}
			set
			{
				this._Chest = value;
				this.HasChest = (value != null);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x000231E8 File Offset: 0x000213E8
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x000231F0 File Offset: 0x000213F0
		public List<CardDef> ChoiceList
		{
			get
			{
				return this._ChoiceList;
			}
			set
			{
				this._ChoiceList = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x000231F9 File Offset: 0x000213F9
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x00023201 File Offset: 0x00021401
		public CardDef HeroDef { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0002320A File Offset: 0x0002140A
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x00023212 File Offset: 0x00021412
		public int MaxWins
		{
			get
			{
				return this._MaxWins;
			}
			set
			{
				this._MaxWins = value;
				this.HasMaxWins = true;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00023222 File Offset: 0x00021422
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x0002322A File Offset: 0x0002142A
		public int MaxSlot { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00023233 File Offset: 0x00021433
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x0002323B File Offset: 0x0002143B
		public ArenaSession CurrentSession
		{
			get
			{
				return this._CurrentSession;
			}
			set
			{
				this._CurrentSession = value;
				this.HasCurrentSession = (value != null);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0002324E File Offset: 0x0002144E
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x00023256 File Offset: 0x00021456
		public DraftSlotType SlotType { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0002325F File Offset: 0x0002145F
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x00023267 File Offset: 0x00021467
		public CardDef HeroPowerDef
		{
			get
			{
				return this._HeroPowerDef;
			}
			set
			{
				this._HeroPowerDef = value;
				this.HasHeroPowerDef = (value != null);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0002327A File Offset: 0x0002147A
		// (set) Token: 0x060009AF RID: 2479 RVA: 0x00023282 File Offset: 0x00021482
		public List<DraftSlotType> UniqueSlotTypes
		{
			get
			{
				return this._UniqueSlotTypes;
			}
			set
			{
				this._UniqueSlotTypes = value;
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002328C File Offset: 0x0002148C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.DeckId.GetHashCode();
			num ^= this.Slot.GetHashCode();
			foreach (DeckCardData deckCardData in this.Cards)
			{
				num ^= deckCardData.GetHashCode();
			}
			if (this.HasDeprecatedWins)
			{
				num ^= this.DeprecatedWins.GetHashCode();
			}
			if (this.HasDeprecatedLosses)
			{
				num ^= this.DeprecatedLosses.GetHashCode();
			}
			if (this.HasChest)
			{
				num ^= this.Chest.GetHashCode();
			}
			foreach (CardDef cardDef in this.ChoiceList)
			{
				num ^= cardDef.GetHashCode();
			}
			num ^= this.HeroDef.GetHashCode();
			if (this.HasMaxWins)
			{
				num ^= this.MaxWins.GetHashCode();
			}
			num ^= this.MaxSlot.GetHashCode();
			if (this.HasCurrentSession)
			{
				num ^= this.CurrentSession.GetHashCode();
			}
			num ^= this.SlotType.GetHashCode();
			if (this.HasHeroPowerDef)
			{
				num ^= this.HeroPowerDef.GetHashCode();
			}
			foreach (DraftSlotType draftSlotType in this.UniqueSlotTypes)
			{
				num ^= draftSlotType.GetHashCode();
			}
			return num;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002346C File Offset: 0x0002166C
		public override bool Equals(object obj)
		{
			DraftChoicesAndContents draftChoicesAndContents = obj as DraftChoicesAndContents;
			if (draftChoicesAndContents == null)
			{
				return false;
			}
			if (!this.DeckId.Equals(draftChoicesAndContents.DeckId))
			{
				return false;
			}
			if (!this.Slot.Equals(draftChoicesAndContents.Slot))
			{
				return false;
			}
			if (this.Cards.Count != draftChoicesAndContents.Cards.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Cards.Count; i++)
			{
				if (!this.Cards[i].Equals(draftChoicesAndContents.Cards[i]))
				{
					return false;
				}
			}
			if (this.HasDeprecatedWins != draftChoicesAndContents.HasDeprecatedWins || (this.HasDeprecatedWins && !this.DeprecatedWins.Equals(draftChoicesAndContents.DeprecatedWins)))
			{
				return false;
			}
			if (this.HasDeprecatedLosses != draftChoicesAndContents.HasDeprecatedLosses || (this.HasDeprecatedLosses && !this.DeprecatedLosses.Equals(draftChoicesAndContents.DeprecatedLosses)))
			{
				return false;
			}
			if (this.HasChest != draftChoicesAndContents.HasChest || (this.HasChest && !this.Chest.Equals(draftChoicesAndContents.Chest)))
			{
				return false;
			}
			if (this.ChoiceList.Count != draftChoicesAndContents.ChoiceList.Count)
			{
				return false;
			}
			for (int j = 0; j < this.ChoiceList.Count; j++)
			{
				if (!this.ChoiceList[j].Equals(draftChoicesAndContents.ChoiceList[j]))
				{
					return false;
				}
			}
			if (!this.HeroDef.Equals(draftChoicesAndContents.HeroDef))
			{
				return false;
			}
			if (this.HasMaxWins != draftChoicesAndContents.HasMaxWins || (this.HasMaxWins && !this.MaxWins.Equals(draftChoicesAndContents.MaxWins)))
			{
				return false;
			}
			if (!this.MaxSlot.Equals(draftChoicesAndContents.MaxSlot))
			{
				return false;
			}
			if (this.HasCurrentSession != draftChoicesAndContents.HasCurrentSession || (this.HasCurrentSession && !this.CurrentSession.Equals(draftChoicesAndContents.CurrentSession)))
			{
				return false;
			}
			if (!this.SlotType.Equals(draftChoicesAndContents.SlotType))
			{
				return false;
			}
			if (this.HasHeroPowerDef != draftChoicesAndContents.HasHeroPowerDef || (this.HasHeroPowerDef && !this.HeroPowerDef.Equals(draftChoicesAndContents.HeroPowerDef)))
			{
				return false;
			}
			if (this.UniqueSlotTypes.Count != draftChoicesAndContents.UniqueSlotTypes.Count)
			{
				return false;
			}
			for (int k = 0; k < this.UniqueSlotTypes.Count; k++)
			{
				if (!this.UniqueSlotTypes[k].Equals(draftChoicesAndContents.UniqueSlotTypes[k]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00023720 File Offset: 0x00021920
		public void Deserialize(Stream stream)
		{
			DraftChoicesAndContents.Deserialize(stream, this);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002372A File Offset: 0x0002192A
		public static DraftChoicesAndContents Deserialize(Stream stream, DraftChoicesAndContents instance)
		{
			return DraftChoicesAndContents.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00023738 File Offset: 0x00021938
		public static DraftChoicesAndContents DeserializeLengthDelimited(Stream stream)
		{
			DraftChoicesAndContents draftChoicesAndContents = new DraftChoicesAndContents();
			DraftChoicesAndContents.DeserializeLengthDelimited(stream, draftChoicesAndContents);
			return draftChoicesAndContents;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00023754 File Offset: 0x00021954
		public static DraftChoicesAndContents DeserializeLengthDelimited(Stream stream, DraftChoicesAndContents instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DraftChoicesAndContents.Deserialize(stream, instance, num);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002377C File Offset: 0x0002197C
		public static DraftChoicesAndContents Deserialize(Stream stream, DraftChoicesAndContents instance, long limit)
		{
			if (instance.Cards == null)
			{
				instance.Cards = new List<DeckCardData>();
			}
			if (instance.ChoiceList == null)
			{
				instance.ChoiceList = new List<CardDef>();
			}
			if (instance.UniqueSlotTypes == null)
			{
				instance.UniqueSlotTypes = new List<DraftSlotType>();
			}
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
					if (num <= 66)
					{
						if (num <= 42)
						{
							if (num == 8)
							{
								instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 16)
							{
								instance.Slot = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 42)
							{
								instance.Cards.Add(DeckCardData.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else
						{
							if (num == 48)
							{
								instance.DeprecatedWins = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 56)
							{
								instance.DeprecatedLosses = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 66)
							{
								if (instance.Chest == null)
								{
									instance.Chest = RewardChest.DeserializeLengthDelimited(stream);
									continue;
								}
								RewardChest.DeserializeLengthDelimited(stream, instance.Chest);
								continue;
							}
						}
					}
					else if (num <= 88)
					{
						if (num == 74)
						{
							instance.ChoiceList.Add(CardDef.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num != 82)
						{
							if (num == 88)
							{
								instance.MaxWins = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (instance.HeroDef == null)
							{
								instance.HeroDef = CardDef.DeserializeLengthDelimited(stream);
								continue;
							}
							CardDef.DeserializeLengthDelimited(stream, instance.HeroDef);
							continue;
						}
					}
					else if (num <= 106)
					{
						if (num == 96)
						{
							instance.MaxSlot = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 106)
						{
							if (instance.CurrentSession == null)
							{
								instance.CurrentSession = ArenaSession.DeserializeLengthDelimited(stream);
								continue;
							}
							ArenaSession.DeserializeLengthDelimited(stream, instance.CurrentSession);
							continue;
						}
					}
					else
					{
						if (num == 112)
						{
							instance.SlotType = (DraftSlotType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 122)
						{
							if (instance.HeroPowerDef == null)
							{
								instance.HeroPowerDef = CardDef.DeserializeLengthDelimited(stream);
								continue;
							}
							CardDef.DeserializeLengthDelimited(stream, instance.HeroPowerDef);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 16U)
					{
						ProtocolParser.SkipKey(stream, key);
					}
					else if (key.WireType == Wire.Varint)
					{
						instance.UniqueSlotTypes.Add((DraftSlotType)ProtocolParser.ReadUInt64(stream));
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00023A3D File Offset: 0x00021C3D
		public void Serialize(Stream stream)
		{
			DraftChoicesAndContents.Serialize(stream, this);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00023A48 File Offset: 0x00021C48
		public static void Serialize(Stream stream, DraftChoicesAndContents instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Slot));
			if (instance.Cards.Count > 0)
			{
				foreach (DeckCardData deckCardData in instance.Cards)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, deckCardData.GetSerializedSize());
					DeckCardData.Serialize(stream, deckCardData);
				}
			}
			if (instance.HasDeprecatedWins)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedWins));
			}
			if (instance.HasDeprecatedLosses)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedLosses));
			}
			if (instance.HasChest)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.Chest.GetSerializedSize());
				RewardChest.Serialize(stream, instance.Chest);
			}
			if (instance.ChoiceList.Count > 0)
			{
				foreach (CardDef cardDef in instance.ChoiceList)
				{
					stream.WriteByte(74);
					ProtocolParser.WriteUInt32(stream, cardDef.GetSerializedSize());
					CardDef.Serialize(stream, cardDef);
				}
			}
			if (instance.HeroDef == null)
			{
				throw new ArgumentNullException("HeroDef", "Required by proto specification.");
			}
			stream.WriteByte(82);
			ProtocolParser.WriteUInt32(stream, instance.HeroDef.GetSerializedSize());
			CardDef.Serialize(stream, instance.HeroDef);
			if (instance.HasMaxWins)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxWins));
			}
			stream.WriteByte(96);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxSlot));
			if (instance.HasCurrentSession)
			{
				stream.WriteByte(106);
				ProtocolParser.WriteUInt32(stream, instance.CurrentSession.GetSerializedSize());
				ArenaSession.Serialize(stream, instance.CurrentSession);
			}
			stream.WriteByte(112);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SlotType));
			if (instance.HasHeroPowerDef)
			{
				stream.WriteByte(122);
				ProtocolParser.WriteUInt32(stream, instance.HeroPowerDef.GetSerializedSize());
				CardDef.Serialize(stream, instance.HeroPowerDef);
			}
			if (instance.UniqueSlotTypes.Count > 0)
			{
				foreach (DraftSlotType draftSlotType in instance.UniqueSlotTypes)
				{
					stream.WriteByte(128);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)draftSlotType));
				}
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00023CF4 File Offset: 0x00021EF4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.DeckId);
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Slot));
			if (this.Cards.Count > 0)
			{
				foreach (DeckCardData deckCardData in this.Cards)
				{
					num += 1U;
					uint serializedSize = deckCardData.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasDeprecatedWins)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedWins));
			}
			if (this.HasDeprecatedLosses)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedLosses));
			}
			if (this.HasChest)
			{
				num += 1U;
				uint serializedSize2 = this.Chest.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.ChoiceList.Count > 0)
			{
				foreach (CardDef cardDef in this.ChoiceList)
				{
					num += 1U;
					uint serializedSize3 = cardDef.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			uint serializedSize4 = this.HeroDef.GetSerializedSize();
			num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			if (this.HasMaxWins)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxWins));
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxSlot));
			if (this.HasCurrentSession)
			{
				num += 1U;
				uint serializedSize5 = this.CurrentSession.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SlotType));
			if (this.HasHeroPowerDef)
			{
				num += 1U;
				uint serializedSize6 = this.HeroPowerDef.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (this.UniqueSlotTypes.Count > 0)
			{
				foreach (DraftSlotType draftSlotType in this.UniqueSlotTypes)
				{
					num += 2U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)draftSlotType));
				}
			}
			num += 5U;
			return num;
		}

		// Token: 0x0400034C RID: 844
		private List<DeckCardData> _Cards = new List<DeckCardData>();

		// Token: 0x0400034D RID: 845
		public bool HasDeprecatedWins;

		// Token: 0x0400034E RID: 846
		private int _DeprecatedWins;

		// Token: 0x0400034F RID: 847
		public bool HasDeprecatedLosses;

		// Token: 0x04000350 RID: 848
		private int _DeprecatedLosses;

		// Token: 0x04000351 RID: 849
		public bool HasChest;

		// Token: 0x04000352 RID: 850
		private RewardChest _Chest;

		// Token: 0x04000353 RID: 851
		private List<CardDef> _ChoiceList = new List<CardDef>();

		// Token: 0x04000355 RID: 853
		public bool HasMaxWins;

		// Token: 0x04000356 RID: 854
		private int _MaxWins;

		// Token: 0x04000358 RID: 856
		public bool HasCurrentSession;

		// Token: 0x04000359 RID: 857
		private ArenaSession _CurrentSession;

		// Token: 0x0400035B RID: 859
		public bool HasHeroPowerDef;

		// Token: 0x0400035C RID: 860
		private CardDef _HeroPowerDef;

		// Token: 0x0400035D RID: 861
		private List<DraftSlotType> _UniqueSlotTypes = new List<DraftSlotType>();

		// Token: 0x020005A4 RID: 1444
		public enum PacketID
		{
			// Token: 0x04001F4D RID: 8013
			ID = 248
		}
	}
}
