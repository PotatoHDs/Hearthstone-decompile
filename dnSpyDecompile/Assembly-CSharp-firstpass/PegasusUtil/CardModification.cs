using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000D4 RID: 212
	public class CardModification : IProtoBuf
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x00034BE7 File Offset: 0x00032DE7
		// (set) Token: 0x06000E73 RID: 3699 RVA: 0x00034BEF File Offset: 0x00032DEF
		public int AssetCardId { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x00034BF8 File Offset: 0x00032DF8
		// (set) Token: 0x06000E75 RID: 3701 RVA: 0x00034C00 File Offset: 0x00032E00
		public int Premium { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x00034C09 File Offset: 0x00032E09
		// (set) Token: 0x06000E77 RID: 3703 RVA: 0x00034C11 File Offset: 0x00032E11
		public int Quantity { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x00034C1A File Offset: 0x00032E1A
		// (set) Token: 0x06000E79 RID: 3705 RVA: 0x00034C22 File Offset: 0x00032E22
		public int AmountSeen { get; set; }

		// Token: 0x06000E7A RID: 3706 RVA: 0x00034C2C File Offset: 0x00032E2C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.AssetCardId.GetHashCode() ^ this.Premium.GetHashCode() ^ this.Quantity.GetHashCode() ^ this.AmountSeen.GetHashCode();
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00034C80 File Offset: 0x00032E80
		public override bool Equals(object obj)
		{
			CardModification cardModification = obj as CardModification;
			return cardModification != null && this.AssetCardId.Equals(cardModification.AssetCardId) && this.Premium.Equals(cardModification.Premium) && this.Quantity.Equals(cardModification.Quantity) && this.AmountSeen.Equals(cardModification.AmountSeen);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00034CFA File Offset: 0x00032EFA
		public void Deserialize(Stream stream)
		{
			CardModification.Deserialize(stream, this);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00034D04 File Offset: 0x00032F04
		public static CardModification Deserialize(Stream stream, CardModification instance)
		{
			return CardModification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00034D10 File Offset: 0x00032F10
		public static CardModification DeserializeLengthDelimited(Stream stream)
		{
			CardModification cardModification = new CardModification();
			CardModification.DeserializeLengthDelimited(stream, cardModification);
			return cardModification;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00034D2C File Offset: 0x00032F2C
		public static CardModification DeserializeLengthDelimited(Stream stream, CardModification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CardModification.Deserialize(stream, instance, num);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00034D54 File Offset: 0x00032F54
		public static CardModification Deserialize(Stream stream, CardModification instance, long limit)
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.AssetCardId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Premium = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.AmountSeen = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000E81 RID: 3713 RVA: 0x00034E28 File Offset: 0x00033028
		public void Serialize(Stream stream)
		{
			CardModification.Serialize(stream, this);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00034E34 File Offset: 0x00033034
		public static void Serialize(Stream stream, CardModification instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AssetCardId));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Premium));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Quantity));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AmountSeen));
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00034E94 File Offset: 0x00033094
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.AssetCardId)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Premium)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Quantity)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.AmountSeen)) + 4U;
		}
	}
}
