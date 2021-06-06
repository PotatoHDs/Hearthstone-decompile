using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200011C RID: 284
	public class DeckCardData : IProtoBuf
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x000415EA File Offset: 0x0003F7EA
		// (set) Token: 0x060012A3 RID: 4771 RVA: 0x000415F2 File Offset: 0x0003F7F2
		public CardDef Def { get; set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x000415FB File Offset: 0x0003F7FB
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x00041603 File Offset: 0x0003F803
		public int Qty
		{
			get
			{
				return this._Qty;
			}
			set
			{
				this._Qty = value;
				this.HasQty = true;
			}
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00041614 File Offset: 0x0003F814
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Def.GetHashCode();
			if (this.HasQty)
			{
				num ^= this.Qty.GetHashCode();
			}
			return num;
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00041658 File Offset: 0x0003F858
		public override bool Equals(object obj)
		{
			DeckCardData deckCardData = obj as DeckCardData;
			return deckCardData != null && this.Def.Equals(deckCardData.Def) && this.HasQty == deckCardData.HasQty && (!this.HasQty || this.Qty.Equals(deckCardData.Qty));
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x000416B5 File Offset: 0x0003F8B5
		public void Deserialize(Stream stream)
		{
			DeckCardData.Deserialize(stream, this);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x000416BF File Offset: 0x0003F8BF
		public static DeckCardData Deserialize(Stream stream, DeckCardData instance)
		{
			return DeckCardData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000416CC File Offset: 0x0003F8CC
		public static DeckCardData DeserializeLengthDelimited(Stream stream)
		{
			DeckCardData deckCardData = new DeckCardData();
			DeckCardData.DeserializeLengthDelimited(stream, deckCardData);
			return deckCardData;
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x000416E8 File Offset: 0x0003F8E8
		public static DeckCardData DeserializeLengthDelimited(Stream stream, DeckCardData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckCardData.Deserialize(stream, instance, num);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00041710 File Offset: 0x0003F910
		public static DeckCardData Deserialize(Stream stream, DeckCardData instance, long limit)
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
						instance.Qty = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.Def == null)
				{
					instance.Def = CardDef.DeserializeLengthDelimited(stream);
				}
				else
				{
					CardDef.DeserializeLengthDelimited(stream, instance.Def);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x000417C3 File Offset: 0x0003F9C3
		public void Serialize(Stream stream)
		{
			DeckCardData.Serialize(stream, this);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x000417CC File Offset: 0x0003F9CC
		public static void Serialize(Stream stream, DeckCardData instance)
		{
			if (instance.Def == null)
			{
				throw new ArgumentNullException("Def", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Def.GetSerializedSize());
			CardDef.Serialize(stream, instance.Def);
			if (instance.HasQty)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Qty));
			}
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00041834 File Offset: 0x0003FA34
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Def.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasQty)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Qty));
			}
			return num + 1U;
		}

		// Token: 0x040005D1 RID: 1489
		public bool HasQty;

		// Token: 0x040005D2 RID: 1490
		private int _Qty;
	}
}
