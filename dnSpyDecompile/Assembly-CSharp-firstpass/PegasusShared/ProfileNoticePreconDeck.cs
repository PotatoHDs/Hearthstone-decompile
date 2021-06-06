using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200012B RID: 299
	public class ProfileNoticePreconDeck : IProtoBuf
	{
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x00044B76 File Offset: 0x00042D76
		// (set) Token: 0x060013BE RID: 5054 RVA: 0x00044B7E File Offset: 0x00042D7E
		public long Deck { get; set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x00044B87 File Offset: 0x00042D87
		// (set) Token: 0x060013C0 RID: 5056 RVA: 0x00044B8F File Offset: 0x00042D8F
		public int Hero { get; set; }

		// Token: 0x060013C1 RID: 5057 RVA: 0x00044B98 File Offset: 0x00042D98
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Deck.GetHashCode() ^ this.Hero.GetHashCode();
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00044BD0 File Offset: 0x00042DD0
		public override bool Equals(object obj)
		{
			ProfileNoticePreconDeck profileNoticePreconDeck = obj as ProfileNoticePreconDeck;
			return profileNoticePreconDeck != null && this.Deck.Equals(profileNoticePreconDeck.Deck) && this.Hero.Equals(profileNoticePreconDeck.Hero);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00044C1A File Offset: 0x00042E1A
		public void Deserialize(Stream stream)
		{
			ProfileNoticePreconDeck.Deserialize(stream, this);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00044C24 File Offset: 0x00042E24
		public static ProfileNoticePreconDeck Deserialize(Stream stream, ProfileNoticePreconDeck instance)
		{
			return ProfileNoticePreconDeck.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00044C30 File Offset: 0x00042E30
		public static ProfileNoticePreconDeck DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticePreconDeck profileNoticePreconDeck = new ProfileNoticePreconDeck();
			ProfileNoticePreconDeck.DeserializeLengthDelimited(stream, profileNoticePreconDeck);
			return profileNoticePreconDeck;
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00044C4C File Offset: 0x00042E4C
		public static ProfileNoticePreconDeck DeserializeLengthDelimited(Stream stream, ProfileNoticePreconDeck instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticePreconDeck.Deserialize(stream, instance, num);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00044C74 File Offset: 0x00042E74
		public static ProfileNoticePreconDeck Deserialize(Stream stream, ProfileNoticePreconDeck instance, long limit)
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
						instance.Hero = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060013C8 RID: 5064 RVA: 0x00044D0C File Offset: 0x00042F0C
		public void Serialize(Stream stream)
		{
			ProfileNoticePreconDeck.Serialize(stream, this);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x00044D15 File Offset: 0x00042F15
		public static void Serialize(Stream stream, ProfileNoticePreconDeck instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Deck);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Hero));
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00044D3F File Offset: 0x00042F3F
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)this.Deck) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Hero)) + 2U;
		}

		// Token: 0x0200061F RID: 1567
		public enum NoticeID
		{
			// Token: 0x04002098 RID: 8344
			ID = 5
		}
	}
}
