using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000CD RID: 205
	public class DeckSetTemplateSource : IProtoBuf
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0003385E File Offset: 0x00031A5E
		// (set) Token: 0x06000E02 RID: 3586 RVA: 0x00033866 File Offset: 0x00031A66
		public long Deck { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0003386F File Offset: 0x00031A6F
		// (set) Token: 0x06000E04 RID: 3588 RVA: 0x00033877 File Offset: 0x00031A77
		public int TemplateId { get; set; }

		// Token: 0x06000E05 RID: 3589 RVA: 0x00033880 File Offset: 0x00031A80
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Deck.GetHashCode() ^ this.TemplateId.GetHashCode();
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x000338B8 File Offset: 0x00031AB8
		public override bool Equals(object obj)
		{
			DeckSetTemplateSource deckSetTemplateSource = obj as DeckSetTemplateSource;
			return deckSetTemplateSource != null && this.Deck.Equals(deckSetTemplateSource.Deck) && this.TemplateId.Equals(deckSetTemplateSource.TemplateId);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00033902 File Offset: 0x00031B02
		public void Deserialize(Stream stream)
		{
			DeckSetTemplateSource.Deserialize(stream, this);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003390C File Offset: 0x00031B0C
		public static DeckSetTemplateSource Deserialize(Stream stream, DeckSetTemplateSource instance)
		{
			return DeckSetTemplateSource.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00033918 File Offset: 0x00031B18
		public static DeckSetTemplateSource DeserializeLengthDelimited(Stream stream)
		{
			DeckSetTemplateSource deckSetTemplateSource = new DeckSetTemplateSource();
			DeckSetTemplateSource.DeserializeLengthDelimited(stream, deckSetTemplateSource);
			return deckSetTemplateSource;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00033934 File Offset: 0x00031B34
		public static DeckSetTemplateSource DeserializeLengthDelimited(Stream stream, DeckSetTemplateSource instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckSetTemplateSource.Deserialize(stream, instance, num);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0003395C File Offset: 0x00031B5C
		public static DeckSetTemplateSource Deserialize(Stream stream, DeckSetTemplateSource instance, long limit)
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
						instance.TemplateId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000E0C RID: 3596 RVA: 0x000339F4 File Offset: 0x00031BF4
		public void Serialize(Stream stream)
		{
			DeckSetTemplateSource.Serialize(stream, this);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x000339FD File Offset: 0x00031BFD
		public static void Serialize(Stream stream, DeckSetTemplateSource instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Deck);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TemplateId));
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00033A27 File Offset: 0x00031C27
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)this.Deck) + ProtocolParser.SizeOfUInt64((ulong)((long)this.TemplateId)) + 2U;
		}

		// Token: 0x020005DC RID: 1500
		public enum PacketID
		{
			// Token: 0x04001FDE RID: 8158
			ID = 332,
			// Token: 0x04001FDF RID: 8159
			System = 0
		}
	}
}
