using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000155 RID: 341
	public class DeckCardDbRecord : IProtoBuf
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0004E9D3 File Offset: 0x0004CBD3
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x0004E9DB File Offset: 0x0004CBDB
		public int Id { get; set; }

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0004E9E4 File Offset: 0x0004CBE4
		// (set) Token: 0x060016CA RID: 5834 RVA: 0x0004E9EC File Offset: 0x0004CBEC
		public int CardId { get; set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0004E9F5 File Offset: 0x0004CBF5
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x0004E9FD File Offset: 0x0004CBFD
		public int DeckId { get; set; }

		// Token: 0x060016CD RID: 5837 RVA: 0x0004EA08 File Offset: 0x0004CC08
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Id.GetHashCode() ^ this.CardId.GetHashCode() ^ this.DeckId.GetHashCode();
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0004EA50 File Offset: 0x0004CC50
		public override bool Equals(object obj)
		{
			DeckCardDbRecord deckCardDbRecord = obj as DeckCardDbRecord;
			return deckCardDbRecord != null && this.Id.Equals(deckCardDbRecord.Id) && this.CardId.Equals(deckCardDbRecord.CardId) && this.DeckId.Equals(deckCardDbRecord.DeckId);
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0004EAB2 File Offset: 0x0004CCB2
		public void Deserialize(Stream stream)
		{
			DeckCardDbRecord.Deserialize(stream, this);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0004EABC File Offset: 0x0004CCBC
		public static DeckCardDbRecord Deserialize(Stream stream, DeckCardDbRecord instance)
		{
			return DeckCardDbRecord.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x0004EAC8 File Offset: 0x0004CCC8
		public static DeckCardDbRecord DeserializeLengthDelimited(Stream stream)
		{
			DeckCardDbRecord deckCardDbRecord = new DeckCardDbRecord();
			DeckCardDbRecord.DeserializeLengthDelimited(stream, deckCardDbRecord);
			return deckCardDbRecord;
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0004EAE4 File Offset: 0x0004CCE4
		public static DeckCardDbRecord DeserializeLengthDelimited(Stream stream, DeckCardDbRecord instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckCardDbRecord.Deserialize(stream, instance, num);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0004EB0C File Offset: 0x0004CD0C
		public static DeckCardDbRecord Deserialize(Stream stream, DeckCardDbRecord instance, long limit)
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
					if (num != 24)
					{
						if (num != 32)
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
							instance.DeckId = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.CardId = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0004EBBC File Offset: 0x0004CDBC
		public void Serialize(Stream stream)
		{
			DeckCardDbRecord.Serialize(stream, this);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0004EBC5 File Offset: 0x0004CDC5
		public static void Serialize(Stream stream, DeckCardDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CardId));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeckId));
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0004EC05 File Offset: 0x0004CE05
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Id)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.CardId)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.DeckId)) + 3U;
		}
	}
}
