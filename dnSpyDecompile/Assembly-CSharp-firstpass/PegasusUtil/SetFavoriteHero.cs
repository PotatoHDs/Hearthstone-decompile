using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000075 RID: 117
	public class SetFavoriteHero : IProtoBuf
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0001B395 File Offset: 0x00019595
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x0001B39D File Offset: 0x0001959D
		public FavoriteHero FavoriteHero { get; set; }

		// Token: 0x06000763 RID: 1891 RVA: 0x0001B3A6 File Offset: 0x000195A6
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.FavoriteHero.GetHashCode();
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001B3C0 File Offset: 0x000195C0
		public override bool Equals(object obj)
		{
			SetFavoriteHero setFavoriteHero = obj as SetFavoriteHero;
			return setFavoriteHero != null && this.FavoriteHero.Equals(setFavoriteHero.FavoriteHero);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001B3EF File Offset: 0x000195EF
		public void Deserialize(Stream stream)
		{
			SetFavoriteHero.Deserialize(stream, this);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001B3F9 File Offset: 0x000195F9
		public static SetFavoriteHero Deserialize(Stream stream, SetFavoriteHero instance)
		{
			return SetFavoriteHero.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001B404 File Offset: 0x00019604
		public static SetFavoriteHero DeserializeLengthDelimited(Stream stream)
		{
			SetFavoriteHero setFavoriteHero = new SetFavoriteHero();
			SetFavoriteHero.DeserializeLengthDelimited(stream, setFavoriteHero);
			return setFavoriteHero;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001B420 File Offset: 0x00019620
		public static SetFavoriteHero DeserializeLengthDelimited(Stream stream, SetFavoriteHero instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetFavoriteHero.Deserialize(stream, instance, num);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001B448 File Offset: 0x00019648
		public static SetFavoriteHero Deserialize(Stream stream, SetFavoriteHero instance, long limit)
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
				else if (num == 10)
				{
					if (instance.FavoriteHero == null)
					{
						instance.FavoriteHero = FavoriteHero.DeserializeLengthDelimited(stream);
					}
					else
					{
						FavoriteHero.DeserializeLengthDelimited(stream, instance.FavoriteHero);
					}
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

		// Token: 0x0600076A RID: 1898 RVA: 0x0001B4E2 File Offset: 0x000196E2
		public void Serialize(Stream stream)
		{
			SetFavoriteHero.Serialize(stream, this);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001B4EB File Offset: 0x000196EB
		public static void Serialize(Stream stream, SetFavoriteHero instance)
		{
			if (instance.FavoriteHero == null)
			{
				throw new ArgumentNullException("FavoriteHero", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.FavoriteHero.GetSerializedSize());
			FavoriteHero.Serialize(stream, instance.FavoriteHero);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001B52C File Offset: 0x0001972C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.FavoriteHero.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1U;
		}

		// Token: 0x02000588 RID: 1416
		public enum PacketID
		{
			// Token: 0x04001EF5 RID: 7925
			ID = 319,
			// Token: 0x04001EF6 RID: 7926
			System = 0
		}
	}
}
