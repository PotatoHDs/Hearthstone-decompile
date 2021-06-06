using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000055 RID: 85
	public class DeleteDeck : IProtoBuf
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x000166AA File Offset: 0x000148AA
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x000166B2 File Offset: 0x000148B2
		public long Deck { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x000166BB File Offset: 0x000148BB
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x000166C3 File Offset: 0x000148C3
		public DeckType DeckType { get; set; }

		// Token: 0x0600057B RID: 1403 RVA: 0x000166CC File Offset: 0x000148CC
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Deck.GetHashCode() ^ this.DeckType.GetHashCode();
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00016708 File Offset: 0x00014908
		public override bool Equals(object obj)
		{
			DeleteDeck deleteDeck = obj as DeleteDeck;
			return deleteDeck != null && this.Deck.Equals(deleteDeck.Deck) && this.DeckType.Equals(deleteDeck.DeckType);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001675D File Offset: 0x0001495D
		public void Deserialize(Stream stream)
		{
			DeleteDeck.Deserialize(stream, this);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00016767 File Offset: 0x00014967
		public static DeleteDeck Deserialize(Stream stream, DeleteDeck instance)
		{
			return DeleteDeck.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00016774 File Offset: 0x00014974
		public static DeleteDeck DeserializeLengthDelimited(Stream stream)
		{
			DeleteDeck deleteDeck = new DeleteDeck();
			DeleteDeck.DeserializeLengthDelimited(stream, deleteDeck);
			return deleteDeck;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00016790 File Offset: 0x00014990
		public static DeleteDeck DeserializeLengthDelimited(Stream stream, DeleteDeck instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeleteDeck.Deserialize(stream, instance, num);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000167B8 File Offset: 0x000149B8
		public static DeleteDeck Deserialize(Stream stream, DeleteDeck instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
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
						instance.DeckType = (DeckType)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Deck = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00016850 File Offset: 0x00014A50
		public void Serialize(Stream stream)
		{
			DeleteDeck.Serialize(stream, this);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00016859 File Offset: 0x00014A59
		public static void Serialize(Stream stream, DeleteDeck instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Deck);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeckType));
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00016883 File Offset: 0x00014A83
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)this.Deck) + ProtocolParser.SizeOfUInt64((ulong)((long)this.DeckType)) + 2U;
		}

		// Token: 0x02000567 RID: 1383
		public enum PacketID
		{
			// Token: 0x04001E8D RID: 7821
			ID = 210,
			// Token: 0x04001E8E RID: 7822
			System = 0
		}
	}
}
