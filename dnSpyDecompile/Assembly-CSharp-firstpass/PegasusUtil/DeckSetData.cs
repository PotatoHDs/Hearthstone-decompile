using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x0200004C RID: 76
	public class DeckSetData : IProtoBuf
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0001440B File Offset: 0x0001260B
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00014413 File Offset: 0x00012613
		public long Deck { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0001441C File Offset: 0x0001261C
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00014424 File Offset: 0x00012624
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

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0001442D File Offset: 0x0001262D
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x00014435 File Offset: 0x00012635
		public CardDef Hero
		{
			get
			{
				return this._Hero;
			}
			set
			{
				this._Hero = value;
				this.HasHero = (value != null);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00014448 File Offset: 0x00012648
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x00014450 File Offset: 0x00012650
		public int CardBack
		{
			get
			{
				return this._CardBack;
			}
			set
			{
				this._CardBack = value;
				this.HasCardBack = true;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00014460 File Offset: 0x00012660
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x00014468 File Offset: 0x00012668
		public bool TaggedStandard
		{
			get
			{
				return this._TaggedStandard;
			}
			set
			{
				this._TaggedStandard = value;
				this.HasTaggedStandard = true;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00014478 File Offset: 0x00012678
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x00014480 File Offset: 0x00012680
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

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00014490 File Offset: 0x00012690
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00014498 File Offset: 0x00012698
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

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x000144AB File Offset: 0x000126AB
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x000144B3 File Offset: 0x000126B3
		public CardDef UiHeroOverride
		{
			get
			{
				return this._UiHeroOverride;
			}
			set
			{
				this._UiHeroOverride = value;
				this.HasUiHeroOverride = (value != null);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x000144C6 File Offset: 0x000126C6
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x000144CE File Offset: 0x000126CE
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

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x000144DE File Offset: 0x000126DE
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x000144E6 File Offset: 0x000126E6
		public long FsgId
		{
			get
			{
				return this._FsgId;
			}
			set
			{
				this._FsgId = value;
				this.HasFsgId = true;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x000144F6 File Offset: 0x000126F6
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x000144FE File Offset: 0x000126FE
		public byte[] FsgSharedSecretKey
		{
			get
			{
				return this._FsgSharedSecretKey;
			}
			set
			{
				this._FsgSharedSecretKey = value;
				this.HasFsgSharedSecretKey = (value != null);
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00014514 File Offset: 0x00012714
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Deck.GetHashCode();
			foreach (DeckCardData deckCardData in this.Cards)
			{
				num ^= deckCardData.GetHashCode();
			}
			if (this.HasHero)
			{
				num ^= this.Hero.GetHashCode();
			}
			if (this.HasCardBack)
			{
				num ^= this.CardBack.GetHashCode();
			}
			if (this.HasTaggedStandard)
			{
				num ^= this.TaggedStandard.GetHashCode();
			}
			if (this.HasSortOrder)
			{
				num ^= this.SortOrder.GetHashCode();
			}
			if (this.HasPastedDeckHash)
			{
				num ^= this.PastedDeckHash.GetHashCode();
			}
			if (this.HasUiHeroOverride)
			{
				num ^= this.UiHeroOverride.GetHashCode();
			}
			if (this.HasFormatType)
			{
				num ^= this.FormatType.GetHashCode();
			}
			if (this.HasFsgId)
			{
				num ^= this.FsgId.GetHashCode();
			}
			if (this.HasFsgSharedSecretKey)
			{
				num ^= this.FsgSharedSecretKey.GetHashCode();
			}
			return num;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00014668 File Offset: 0x00012868
		public override bool Equals(object obj)
		{
			DeckSetData deckSetData = obj as DeckSetData;
			if (deckSetData == null)
			{
				return false;
			}
			if (!this.Deck.Equals(deckSetData.Deck))
			{
				return false;
			}
			if (this.Cards.Count != deckSetData.Cards.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Cards.Count; i++)
			{
				if (!this.Cards[i].Equals(deckSetData.Cards[i]))
				{
					return false;
				}
			}
			return this.HasHero == deckSetData.HasHero && (!this.HasHero || this.Hero.Equals(deckSetData.Hero)) && this.HasCardBack == deckSetData.HasCardBack && (!this.HasCardBack || this.CardBack.Equals(deckSetData.CardBack)) && this.HasTaggedStandard == deckSetData.HasTaggedStandard && (!this.HasTaggedStandard || this.TaggedStandard.Equals(deckSetData.TaggedStandard)) && this.HasSortOrder == deckSetData.HasSortOrder && (!this.HasSortOrder || this.SortOrder.Equals(deckSetData.SortOrder)) && this.HasPastedDeckHash == deckSetData.HasPastedDeckHash && (!this.HasPastedDeckHash || this.PastedDeckHash.Equals(deckSetData.PastedDeckHash)) && this.HasUiHeroOverride == deckSetData.HasUiHeroOverride && (!this.HasUiHeroOverride || this.UiHeroOverride.Equals(deckSetData.UiHeroOverride)) && this.HasFormatType == deckSetData.HasFormatType && (!this.HasFormatType || this.FormatType.Equals(deckSetData.FormatType)) && this.HasFsgId == deckSetData.HasFsgId && (!this.HasFsgId || this.FsgId.Equals(deckSetData.FsgId)) && this.HasFsgSharedSecretKey == deckSetData.HasFsgSharedSecretKey && (!this.HasFsgSharedSecretKey || this.FsgSharedSecretKey.Equals(deckSetData.FsgSharedSecretKey));
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001488A File Offset: 0x00012A8A
		public void Deserialize(Stream stream)
		{
			DeckSetData.Deserialize(stream, this);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00014894 File Offset: 0x00012A94
		public static DeckSetData Deserialize(Stream stream, DeckSetData instance)
		{
			return DeckSetData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000148A0 File Offset: 0x00012AA0
		public static DeckSetData DeserializeLengthDelimited(Stream stream)
		{
			DeckSetData deckSetData = new DeckSetData();
			DeckSetData.DeserializeLengthDelimited(stream, deckSetData);
			return deckSetData;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000148BC File Offset: 0x00012ABC
		public static DeckSetData DeserializeLengthDelimited(Stream stream, DeckSetData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckSetData.Deserialize(stream, instance, num);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000148E4 File Offset: 0x00012AE4
		public static DeckSetData Deserialize(Stream stream, DeckSetData instance, long limit)
		{
			if (instance.Cards == null)
			{
				instance.Cards = new List<DeckCardData>();
			}
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
					if (num <= 32)
					{
						if (num <= 18)
						{
							if (num == 8)
							{
								instance.Deck = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 18)
							{
								instance.Cards.Add(DeckCardData.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else if (num != 26)
						{
							if (num == 32)
							{
								instance.CardBack = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (instance.Hero == null)
							{
								instance.Hero = CardDef.DeserializeLengthDelimited(stream);
								continue;
							}
							CardDef.DeserializeLengthDelimited(stream, instance.Hero);
							continue;
						}
					}
					else if (num <= 48)
					{
						if (num == 40)
						{
							instance.TaggedStandard = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 48)
						{
							instance.SortOrder = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 58)
						{
							instance.PastedDeckHash = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num != 66)
						{
							if (num == 72)
							{
								instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (instance.UiHeroOverride == null)
							{
								instance.UiHeroOverride = CardDef.DeserializeLengthDelimited(stream);
								continue;
							}
							CardDef.DeserializeLengthDelimited(stream, instance.UiHeroOverride);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 100U)
					{
						if (field != 101U)
						{
							ProtocolParser.SkipKey(stream, key);
						}
						else if (key.WireType == Wire.LengthDelimited)
						{
							instance.FsgSharedSecretKey = ProtocolParser.ReadBytes(stream);
						}
					}
					else if (key.WireType == Wire.Varint)
					{
						instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00014AEE File Offset: 0x00012CEE
		public void Serialize(Stream stream)
		{
			DeckSetData.Serialize(stream, this);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00014AF8 File Offset: 0x00012CF8
		public static void Serialize(Stream stream, DeckSetData instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Deck);
			if (instance.Cards.Count > 0)
			{
				foreach (DeckCardData deckCardData in instance.Cards)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, deckCardData.GetSerializedSize());
					DeckCardData.Serialize(stream, deckCardData);
				}
			}
			if (instance.HasHero)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Hero.GetSerializedSize());
				CardDef.Serialize(stream, instance.Hero);
			}
			if (instance.HasCardBack)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CardBack));
			}
			if (instance.HasTaggedStandard)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.TaggedStandard);
			}
			if (instance.HasSortOrder)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SortOrder);
			}
			if (instance.HasPastedDeckHash)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PastedDeckHash));
			}
			if (instance.HasUiHeroOverride)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.UiHeroOverride.GetSerializedSize());
				CardDef.Serialize(stream, instance.UiHeroOverride);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			}
			if (instance.HasFsgId)
			{
				stream.WriteByte(160);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			}
			if (instance.HasFsgSharedSecretKey)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, instance.FsgSharedSecretKey);
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00014CC0 File Offset: 0x00012EC0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.Deck);
			if (this.Cards.Count > 0)
			{
				foreach (DeckCardData deckCardData in this.Cards)
				{
					num += 1U;
					uint serializedSize = deckCardData.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasHero)
			{
				num += 1U;
				uint serializedSize2 = this.Hero.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasCardBack)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CardBack));
			}
			if (this.HasTaggedStandard)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasSortOrder)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.SortOrder);
			}
			if (this.HasPastedDeckHash)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.PastedDeckHash);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasUiHeroOverride)
			{
				num += 1U;
				uint serializedSize3 = this.UiHeroOverride.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasFormatType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			}
			if (this.HasFsgId)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.FsgId);
			}
			if (this.HasFsgSharedSecretKey)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt32(this.FsgSharedSecretKey.Length) + (uint)this.FsgSharedSecretKey.Length;
			}
			num += 1U;
			return num;
		}

		// Token: 0x040001CD RID: 461
		private List<DeckCardData> _Cards = new List<DeckCardData>();

		// Token: 0x040001CE RID: 462
		public bool HasHero;

		// Token: 0x040001CF RID: 463
		private CardDef _Hero;

		// Token: 0x040001D0 RID: 464
		public bool HasCardBack;

		// Token: 0x040001D1 RID: 465
		private int _CardBack;

		// Token: 0x040001D2 RID: 466
		public bool HasTaggedStandard;

		// Token: 0x040001D3 RID: 467
		private bool _TaggedStandard;

		// Token: 0x040001D4 RID: 468
		public bool HasSortOrder;

		// Token: 0x040001D5 RID: 469
		private long _SortOrder;

		// Token: 0x040001D6 RID: 470
		public bool HasPastedDeckHash;

		// Token: 0x040001D7 RID: 471
		private string _PastedDeckHash;

		// Token: 0x040001D8 RID: 472
		public bool HasUiHeroOverride;

		// Token: 0x040001D9 RID: 473
		private CardDef _UiHeroOverride;

		// Token: 0x040001DA RID: 474
		public bool HasFormatType;

		// Token: 0x040001DB RID: 475
		private FormatType _FormatType;

		// Token: 0x040001DC RID: 476
		public bool HasFsgId;

		// Token: 0x040001DD RID: 477
		private long _FsgId;

		// Token: 0x040001DE RID: 478
		public bool HasFsgSharedSecretKey;

		// Token: 0x040001DF RID: 479
		private byte[] _FsgSharedSecretKey;

		// Token: 0x0200055C RID: 1372
		public enum PacketID
		{
			// Token: 0x04001E52 RID: 7762
			ID = 222,
			// Token: 0x04001E53 RID: 7763
			System = 0
		}
	}
}
