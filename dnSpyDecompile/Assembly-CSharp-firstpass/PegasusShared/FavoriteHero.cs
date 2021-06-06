using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000144 RID: 324
	public class FavoriteHero : IProtoBuf
	{
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x000492D7 File Offset: 0x000474D7
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x000492DF File Offset: 0x000474DF
		public int ClassId { get; set; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x000492E8 File Offset: 0x000474E8
		// (set) Token: 0x06001565 RID: 5477 RVA: 0x000492F0 File Offset: 0x000474F0
		public CardDef Hero { get; set; }

		// Token: 0x06001566 RID: 5478 RVA: 0x000492FC File Offset: 0x000474FC
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ClassId.GetHashCode() ^ this.Hero.GetHashCode();
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00049330 File Offset: 0x00047530
		public override bool Equals(object obj)
		{
			FavoriteHero favoriteHero = obj as FavoriteHero;
			return favoriteHero != null && this.ClassId.Equals(favoriteHero.ClassId) && this.Hero.Equals(favoriteHero.Hero);
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00049377 File Offset: 0x00047577
		public void Deserialize(Stream stream)
		{
			FavoriteHero.Deserialize(stream, this);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00049381 File Offset: 0x00047581
		public static FavoriteHero Deserialize(Stream stream, FavoriteHero instance)
		{
			return FavoriteHero.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0004938C File Offset: 0x0004758C
		public static FavoriteHero DeserializeLengthDelimited(Stream stream)
		{
			FavoriteHero favoriteHero = new FavoriteHero();
			FavoriteHero.DeserializeLengthDelimited(stream, favoriteHero);
			return favoriteHero;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x000493A8 File Offset: 0x000475A8
		public static FavoriteHero DeserializeLengthDelimited(Stream stream, FavoriteHero instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FavoriteHero.Deserialize(stream, instance, num);
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x000493D0 File Offset: 0x000475D0
		public static FavoriteHero Deserialize(Stream stream, FavoriteHero instance, long limit)
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
					else if (instance.Hero == null)
					{
						instance.Hero = CardDef.DeserializeLengthDelimited(stream);
					}
					else
					{
						CardDef.DeserializeLengthDelimited(stream, instance.Hero);
					}
				}
				else
				{
					instance.ClassId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00049482 File Offset: 0x00047682
		public void Serialize(Stream stream)
		{
			FavoriteHero.Serialize(stream, this);
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0004948C File Offset: 0x0004768C
		public static void Serialize(Stream stream, FavoriteHero instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ClassId));
			if (instance.Hero == null)
			{
				throw new ArgumentNullException("Hero", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Hero.GetSerializedSize());
			CardDef.Serialize(stream, instance.Hero);
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x000494EC File Offset: 0x000476EC
		public uint GetSerializedSize()
		{
			uint num = 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.ClassId));
			uint serializedSize = this.Hero.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 2U;
		}
	}
}
