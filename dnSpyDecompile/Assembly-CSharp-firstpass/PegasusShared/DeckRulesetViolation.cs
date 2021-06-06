using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x02000149 RID: 329
	public class DeckRulesetViolation : IProtoBuf
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x0004A967 File Offset: 0x00048B67
		// (set) Token: 0x060015C2 RID: 5570 RVA: 0x0004A96F File Offset: 0x00048B6F
		public CardDef Card
		{
			get
			{
				return this._Card;
			}
			set
			{
				this._Card = value;
				this.HasCard = (value != null);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x0004A982 File Offset: 0x00048B82
		// (set) Token: 0x060015C4 RID: 5572 RVA: 0x0004A98A File Offset: 0x00048B8A
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

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x0004A99A File Offset: 0x00048B9A
		// (set) Token: 0x060015C6 RID: 5574 RVA: 0x0004A9A2 File Offset: 0x00048BA2
		public int DeckRuleId { get; set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0004A9AB File Offset: 0x00048BAB
		// (set) Token: 0x060015C8 RID: 5576 RVA: 0x0004A9B3 File Offset: 0x00048BB3
		public string DeckRuleDesc
		{
			get
			{
				return this._DeckRuleDesc;
			}
			set
			{
				this._DeckRuleDesc = value;
				this.HasDeckRuleDesc = (value != null);
			}
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0004A9C8 File Offset: 0x00048BC8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCard)
			{
				num ^= this.Card.GetHashCode();
			}
			if (this.HasCount)
			{
				num ^= this.Count.GetHashCode();
			}
			num ^= this.DeckRuleId.GetHashCode();
			if (this.HasDeckRuleDesc)
			{
				num ^= this.DeckRuleDesc.GetHashCode();
			}
			return num;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x0004AA38 File Offset: 0x00048C38
		public override bool Equals(object obj)
		{
			DeckRulesetViolation deckRulesetViolation = obj as DeckRulesetViolation;
			return deckRulesetViolation != null && this.HasCard == deckRulesetViolation.HasCard && (!this.HasCard || this.Card.Equals(deckRulesetViolation.Card)) && this.HasCount == deckRulesetViolation.HasCount && (!this.HasCount || this.Count.Equals(deckRulesetViolation.Count)) && this.DeckRuleId.Equals(deckRulesetViolation.DeckRuleId) && this.HasDeckRuleDesc == deckRulesetViolation.HasDeckRuleDesc && (!this.HasDeckRuleDesc || this.DeckRuleDesc.Equals(deckRulesetViolation.DeckRuleDesc));
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0004AAEE File Offset: 0x00048CEE
		public void Deserialize(Stream stream)
		{
			DeckRulesetViolation.Deserialize(stream, this);
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0004AAF8 File Offset: 0x00048CF8
		public static DeckRulesetViolation Deserialize(Stream stream, DeckRulesetViolation instance)
		{
			return DeckRulesetViolation.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0004AB04 File Offset: 0x00048D04
		public static DeckRulesetViolation DeserializeLengthDelimited(Stream stream)
		{
			DeckRulesetViolation deckRulesetViolation = new DeckRulesetViolation();
			DeckRulesetViolation.DeserializeLengthDelimited(stream, deckRulesetViolation);
			return deckRulesetViolation;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0004AB20 File Offset: 0x00048D20
		public static DeckRulesetViolation DeserializeLengthDelimited(Stream stream, DeckRulesetViolation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckRulesetViolation.Deserialize(stream, instance, num);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0004AB48 File Offset: 0x00048D48
		public static DeckRulesetViolation Deserialize(Stream stream, DeckRulesetViolation instance, long limit)
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
				else if (num != 10)
				{
					if (num != 16)
					{
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
								instance.DeckRuleDesc = ProtocolParser.ReadString(stream);
							}
						}
						else if (key.WireType == Wire.Varint)
						{
							instance.DeckRuleId = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.Count = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.Card == null)
				{
					instance.Card = CardDef.DeserializeLengthDelimited(stream);
				}
				else
				{
					CardDef.DeserializeLengthDelimited(stream, instance.Card);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0004AC44 File Offset: 0x00048E44
		public void Serialize(Stream stream)
		{
			DeckRulesetViolation.Serialize(stream, this);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0004AC50 File Offset: 0x00048E50
		public static void Serialize(Stream stream, DeckRulesetViolation instance)
		{
			if (instance.HasCard)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Card.GetSerializedSize());
				CardDef.Serialize(stream, instance.Card);
			}
			if (instance.HasCount)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Count));
			}
			stream.WriteByte(160);
			stream.WriteByte(6);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeckRuleId));
			if (instance.HasDeckRuleDesc)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeckRuleDesc));
			}
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x0004ACF8 File Offset: 0x00048EF8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCard)
			{
				num += 1U;
				uint serializedSize = this.Card.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Count));
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeckRuleId));
			if (this.HasDeckRuleDesc)
			{
				num += 2U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.DeckRuleDesc);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 2U;
		}

		// Token: 0x040006AF RID: 1711
		public bool HasCard;

		// Token: 0x040006B0 RID: 1712
		private CardDef _Card;

		// Token: 0x040006B1 RID: 1713
		public bool HasCount;

		// Token: 0x040006B2 RID: 1714
		private int _Count;

		// Token: 0x040006B4 RID: 1716
		public bool HasDeckRuleDesc;

		// Token: 0x040006B5 RID: 1717
		private string _DeckRuleDesc;
	}
}
