using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusFSG
{
	// Token: 0x02000026 RID: 38
	public class DeckValidity : IProtoBuf
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000821B File Offset: 0x0000641B
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00008223 File Offset: 0x00006423
		public bool ValidStandardDeckDeprecated { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000822C File Offset: 0x0000642C
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00008234 File Offset: 0x00006434
		public bool ValidWildDeckDeprecated { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000823D File Offset: 0x0000643D
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00008245 File Offset: 0x00006445
		public List<BrawlDeckValidity> ValidTavernBrawlDeck
		{
			get
			{
				return this._ValidTavernBrawlDeck;
			}
			set
			{
				this._ValidTavernBrawlDeck = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000824E File Offset: 0x0000644E
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00008256 File Offset: 0x00006456
		public List<BrawlDeckValidity> ValidFiresideBrawlDeck
		{
			get
			{
				return this._ValidFiresideBrawlDeck;
			}
			set
			{
				this._ValidFiresideBrawlDeck = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000825F File Offset: 0x0000645F
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00008267 File Offset: 0x00006467
		public List<FormatDeckValidity> ValidFormatDecks
		{
			get
			{
				return this._ValidFormatDecks;
			}
			set
			{
				this._ValidFormatDecks = value;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008270 File Offset: 0x00006470
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ValidStandardDeckDeprecated.GetHashCode();
			num ^= this.ValidWildDeckDeprecated.GetHashCode();
			foreach (BrawlDeckValidity brawlDeckValidity in this.ValidTavernBrawlDeck)
			{
				num ^= brawlDeckValidity.GetHashCode();
			}
			foreach (BrawlDeckValidity brawlDeckValidity2 in this.ValidFiresideBrawlDeck)
			{
				num ^= brawlDeckValidity2.GetHashCode();
			}
			foreach (FormatDeckValidity formatDeckValidity in this.ValidFormatDecks)
			{
				num ^= formatDeckValidity.GetHashCode();
			}
			return num;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008384 File Offset: 0x00006584
		public override bool Equals(object obj)
		{
			DeckValidity deckValidity = obj as DeckValidity;
			if (deckValidity == null)
			{
				return false;
			}
			if (!this.ValidStandardDeckDeprecated.Equals(deckValidity.ValidStandardDeckDeprecated))
			{
				return false;
			}
			if (!this.ValidWildDeckDeprecated.Equals(deckValidity.ValidWildDeckDeprecated))
			{
				return false;
			}
			if (this.ValidTavernBrawlDeck.Count != deckValidity.ValidTavernBrawlDeck.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ValidTavernBrawlDeck.Count; i++)
			{
				if (!this.ValidTavernBrawlDeck[i].Equals(deckValidity.ValidTavernBrawlDeck[i]))
				{
					return false;
				}
			}
			if (this.ValidFiresideBrawlDeck.Count != deckValidity.ValidFiresideBrawlDeck.Count)
			{
				return false;
			}
			for (int j = 0; j < this.ValidFiresideBrawlDeck.Count; j++)
			{
				if (!this.ValidFiresideBrawlDeck[j].Equals(deckValidity.ValidFiresideBrawlDeck[j]))
				{
					return false;
				}
			}
			if (this.ValidFormatDecks.Count != deckValidity.ValidFormatDecks.Count)
			{
				return false;
			}
			for (int k = 0; k < this.ValidFormatDecks.Count; k++)
			{
				if (!this.ValidFormatDecks[k].Equals(deckValidity.ValidFormatDecks[k]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000084C7 File Offset: 0x000066C7
		public void Deserialize(Stream stream)
		{
			DeckValidity.Deserialize(stream, this);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000084D1 File Offset: 0x000066D1
		public static DeckValidity Deserialize(Stream stream, DeckValidity instance)
		{
			return DeckValidity.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000084DC File Offset: 0x000066DC
		public static DeckValidity DeserializeLengthDelimited(Stream stream)
		{
			DeckValidity deckValidity = new DeckValidity();
			DeckValidity.DeserializeLengthDelimited(stream, deckValidity);
			return deckValidity;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000084F8 File Offset: 0x000066F8
		public static DeckValidity DeserializeLengthDelimited(Stream stream, DeckValidity instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckValidity.Deserialize(stream, instance, num);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008520 File Offset: 0x00006720
		public static DeckValidity Deserialize(Stream stream, DeckValidity instance, long limit)
		{
			if (instance.ValidTavernBrawlDeck == null)
			{
				instance.ValidTavernBrawlDeck = new List<BrawlDeckValidity>();
			}
			if (instance.ValidFiresideBrawlDeck == null)
			{
				instance.ValidFiresideBrawlDeck = new List<BrawlDeckValidity>();
			}
			if (instance.ValidFormatDecks == null)
			{
				instance.ValidFormatDecks = new List<FormatDeckValidity>();
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.ValidStandardDeckDeprecated = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 16)
						{
							instance.ValidWildDeckDeprecated = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.ValidTavernBrawlDeck.Add(BrawlDeckValidity.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 34)
						{
							instance.ValidFiresideBrawlDeck.Add(BrawlDeckValidity.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 42)
						{
							instance.ValidFormatDecks.Add(FormatDeckValidity.DeserializeLengthDelimited(stream));
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

		// Token: 0x060001E0 RID: 480 RVA: 0x0000864E File Offset: 0x0000684E
		public void Serialize(Stream stream)
		{
			DeckValidity.Serialize(stream, this);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008658 File Offset: 0x00006858
		public static void Serialize(Stream stream, DeckValidity instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.ValidStandardDeckDeprecated);
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.ValidWildDeckDeprecated);
			if (instance.ValidTavernBrawlDeck.Count > 0)
			{
				foreach (BrawlDeckValidity brawlDeckValidity in instance.ValidTavernBrawlDeck)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, brawlDeckValidity.GetSerializedSize());
					BrawlDeckValidity.Serialize(stream, brawlDeckValidity);
				}
			}
			if (instance.ValidFiresideBrawlDeck.Count > 0)
			{
				foreach (BrawlDeckValidity brawlDeckValidity2 in instance.ValidFiresideBrawlDeck)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, brawlDeckValidity2.GetSerializedSize());
					BrawlDeckValidity.Serialize(stream, brawlDeckValidity2);
				}
			}
			if (instance.ValidFormatDecks.Count > 0)
			{
				foreach (FormatDeckValidity formatDeckValidity in instance.ValidFormatDecks)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, formatDeckValidity.GetSerializedSize());
					FormatDeckValidity.Serialize(stream, formatDeckValidity);
				}
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000087C0 File Offset: 0x000069C0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += 1U;
			num += 1U;
			if (this.ValidTavernBrawlDeck.Count > 0)
			{
				foreach (BrawlDeckValidity brawlDeckValidity in this.ValidTavernBrawlDeck)
				{
					num += 1U;
					uint serializedSize = brawlDeckValidity.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.ValidFiresideBrawlDeck.Count > 0)
			{
				foreach (BrawlDeckValidity brawlDeckValidity2 in this.ValidFiresideBrawlDeck)
				{
					num += 1U;
					uint serializedSize2 = brawlDeckValidity2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.ValidFormatDecks.Count > 0)
			{
				foreach (FormatDeckValidity formatDeckValidity in this.ValidFormatDecks)
				{
					num += 1U;
					uint serializedSize3 = formatDeckValidity.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			num += 2U;
			return num;
		}

		// Token: 0x0400007D RID: 125
		private List<BrawlDeckValidity> _ValidTavernBrawlDeck = new List<BrawlDeckValidity>();

		// Token: 0x0400007E RID: 126
		private List<BrawlDeckValidity> _ValidFiresideBrawlDeck = new List<BrawlDeckValidity>();

		// Token: 0x0400007F RID: 127
		private List<FormatDeckValidity> _ValidFormatDecks = new List<FormatDeckValidity>();

		// Token: 0x0200055A RID: 1370
		public enum PacketID
		{
			// Token: 0x04001E2A RID: 7722
			ID = 513
		}
	}
}
