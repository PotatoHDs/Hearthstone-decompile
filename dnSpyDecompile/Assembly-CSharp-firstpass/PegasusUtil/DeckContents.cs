using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000A0 RID: 160
	public class DeckContents : IProtoBuf
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0002889D File Offset: 0x00026A9D
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x000288A5 File Offset: 0x00026AA5
		public bool Success { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x000288AE File Offset: 0x00026AAE
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x000288B6 File Offset: 0x00026AB6
		public long DeckId { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x000288BF File Offset: 0x00026ABF
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x000288C7 File Offset: 0x00026AC7
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

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000288D0 File Offset: 0x00026AD0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Success.GetHashCode();
			num ^= this.DeckId.GetHashCode();
			foreach (DeckCardData deckCardData in this.Cards)
			{
				num ^= deckCardData.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00028958 File Offset: 0x00026B58
		public override bool Equals(object obj)
		{
			DeckContents deckContents = obj as DeckContents;
			if (deckContents == null)
			{
				return false;
			}
			if (!this.Success.Equals(deckContents.Success))
			{
				return false;
			}
			if (!this.DeckId.Equals(deckContents.DeckId))
			{
				return false;
			}
			if (this.Cards.Count != deckContents.Cards.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Cards.Count; i++)
			{
				if (!this.Cards[i].Equals(deckContents.Cards[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000289F3 File Offset: 0x00026BF3
		public void Deserialize(Stream stream)
		{
			DeckContents.Deserialize(stream, this);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000289FD File Offset: 0x00026BFD
		public static DeckContents Deserialize(Stream stream, DeckContents instance)
		{
			return DeckContents.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00028A08 File Offset: 0x00026C08
		public static DeckContents DeserializeLengthDelimited(Stream stream)
		{
			DeckContents deckContents = new DeckContents();
			DeckContents.DeserializeLengthDelimited(stream, deckContents);
			return deckContents;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00028A24 File Offset: 0x00026C24
		public static DeckContents DeserializeLengthDelimited(Stream stream, DeckContents instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckContents.Deserialize(stream, instance, num);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00028A4C File Offset: 0x00026C4C
		public static DeckContents Deserialize(Stream stream, DeckContents instance, long limit)
		{
			if (instance.Cards == null)
			{
				instance.Cards = new List<DeckCardData>();
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
				else if (num != 8)
				{
					if (num != 16)
					{
						if (num != 26)
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
							instance.Cards.Add(DeckCardData.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Success = ProtocolParser.ReadBool(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00028B11 File Offset: 0x00026D11
		public void Serialize(Stream stream)
		{
			DeckContents.Serialize(stream, this);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00028B1C File Offset: 0x00026D1C
		public static void Serialize(Stream stream, DeckContents instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.Success);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			if (instance.Cards.Count > 0)
			{
				foreach (DeckCardData deckCardData in instance.Cards)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, deckCardData.GetSerializedSize());
					DeckCardData.Serialize(stream, deckCardData);
				}
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00028BB8 File Offset: 0x00026DB8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += 1U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.DeckId);
			if (this.Cards.Count > 0)
			{
				foreach (DeckCardData deckCardData in this.Cards)
				{
					num += 1U;
					uint serializedSize = deckCardData.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 2U;
			return num;
		}

		// Token: 0x040003BC RID: 956
		private List<DeckCardData> _Cards = new List<DeckCardData>();
	}
}
