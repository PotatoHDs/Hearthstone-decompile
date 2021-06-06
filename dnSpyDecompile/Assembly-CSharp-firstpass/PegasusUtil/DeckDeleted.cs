using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000A4 RID: 164
	public class DeckDeleted : IProtoBuf
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x000296F2 File Offset: 0x000278F2
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x000296FA File Offset: 0x000278FA
		public long Deck { get; set; }

		// Token: 0x06000B16 RID: 2838 RVA: 0x00029704 File Offset: 0x00027904
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Deck.GetHashCode();
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002972C File Offset: 0x0002792C
		public override bool Equals(object obj)
		{
			DeckDeleted deckDeleted = obj as DeckDeleted;
			return deckDeleted != null && this.Deck.Equals(deckDeleted.Deck);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002975E File Offset: 0x0002795E
		public void Deserialize(Stream stream)
		{
			DeckDeleted.Deserialize(stream, this);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00029768 File Offset: 0x00027968
		public static DeckDeleted Deserialize(Stream stream, DeckDeleted instance)
		{
			return DeckDeleted.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00029774 File Offset: 0x00027974
		public static DeckDeleted DeserializeLengthDelimited(Stream stream)
		{
			DeckDeleted deckDeleted = new DeckDeleted();
			DeckDeleted.DeserializeLengthDelimited(stream, deckDeleted);
			return deckDeleted;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00029790 File Offset: 0x00027990
		public static DeckDeleted DeserializeLengthDelimited(Stream stream, DeckDeleted instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckDeleted.Deserialize(stream, instance, num);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000297B8 File Offset: 0x000279B8
		public static DeckDeleted Deserialize(Stream stream, DeckDeleted instance, long limit)
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
				else if (num == 8)
				{
					instance.Deck = (long)ProtocolParser.ReadUInt64(stream);
				}
				else
				{
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

		// Token: 0x06000B1D RID: 2845 RVA: 0x00029837 File Offset: 0x00027A37
		public void Serialize(Stream stream)
		{
			DeckDeleted.Serialize(stream, this);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00029840 File Offset: 0x00027A40
		public static void Serialize(Stream stream, DeckDeleted instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Deck);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00029855 File Offset: 0x00027A55
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)this.Deck) + 1U;
		}

		// Token: 0x020005AE RID: 1454
		public enum PacketID
		{
			// Token: 0x04001F62 RID: 8034
			ID = 218
		}
	}
}
