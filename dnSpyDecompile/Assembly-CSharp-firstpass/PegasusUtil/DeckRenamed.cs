using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x020000A5 RID: 165
	public class DeckRenamed : IProtoBuf
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00029866 File Offset: 0x00027A66
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x0002986E File Offset: 0x00027A6E
		public long Deck { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00029877 File Offset: 0x00027A77
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x0002987F File Offset: 0x00027A7F
		public string Name { get; set; }

		// Token: 0x06000B25 RID: 2853 RVA: 0x00029888 File Offset: 0x00027A88
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Deck.GetHashCode() ^ this.Name.GetHashCode();
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x000298BC File Offset: 0x00027ABC
		public override bool Equals(object obj)
		{
			DeckRenamed deckRenamed = obj as DeckRenamed;
			return deckRenamed != null && this.Deck.Equals(deckRenamed.Deck) && this.Name.Equals(deckRenamed.Name);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00029903 File Offset: 0x00027B03
		public void Deserialize(Stream stream)
		{
			DeckRenamed.Deserialize(stream, this);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002990D File Offset: 0x00027B0D
		public static DeckRenamed Deserialize(Stream stream, DeckRenamed instance)
		{
			return DeckRenamed.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00029918 File Offset: 0x00027B18
		public static DeckRenamed DeserializeLengthDelimited(Stream stream)
		{
			DeckRenamed deckRenamed = new DeckRenamed();
			DeckRenamed.DeserializeLengthDelimited(stream, deckRenamed);
			return deckRenamed;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00029934 File Offset: 0x00027B34
		public static DeckRenamed DeserializeLengthDelimited(Stream stream, DeckRenamed instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckRenamed.Deserialize(stream, instance, num);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002995C File Offset: 0x00027B5C
		public static DeckRenamed Deserialize(Stream stream, DeckRenamed instance, long limit)
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
					if (num != 18)
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
						instance.Name = ProtocolParser.ReadString(stream);
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

		// Token: 0x06000B2C RID: 2860 RVA: 0x000299F3 File Offset: 0x00027BF3
		public void Serialize(Stream stream)
		{
			DeckRenamed.Serialize(stream, this);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x000299FC File Offset: 0x00027BFC
		public static void Serialize(Stream stream, DeckRenamed instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Deck);
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00029A54 File Offset: 0x00027C54
		public uint GetSerializedSize()
		{
			uint num = 0U + ProtocolParser.SizeOfUInt64((ulong)this.Deck);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 2U;
		}

		// Token: 0x020005AF RID: 1455
		public enum PacketID
		{
			// Token: 0x04001F64 RID: 8036
			ID = 219
		}
	}
}
