using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000C0 RID: 192
	public class FavoriteHeroesResponse : IProtoBuf
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x000312E7 File Offset: 0x0002F4E7
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x000312EF File Offset: 0x0002F4EF
		public List<FavoriteHero> FavoriteHeroes
		{
			get
			{
				return this._FavoriteHeroes;
			}
			set
			{
				this._FavoriteHeroes = value;
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x000312F8 File Offset: 0x0002F4F8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (FavoriteHero favoriteHero in this.FavoriteHeroes)
			{
				num ^= favoriteHero.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0003135C File Offset: 0x0002F55C
		public override bool Equals(object obj)
		{
			FavoriteHeroesResponse favoriteHeroesResponse = obj as FavoriteHeroesResponse;
			if (favoriteHeroesResponse == null)
			{
				return false;
			}
			if (this.FavoriteHeroes.Count != favoriteHeroesResponse.FavoriteHeroes.Count)
			{
				return false;
			}
			for (int i = 0; i < this.FavoriteHeroes.Count; i++)
			{
				if (!this.FavoriteHeroes[i].Equals(favoriteHeroesResponse.FavoriteHeroes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x000313C7 File Offset: 0x0002F5C7
		public void Deserialize(Stream stream)
		{
			FavoriteHeroesResponse.Deserialize(stream, this);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x000313D1 File Offset: 0x0002F5D1
		public static FavoriteHeroesResponse Deserialize(Stream stream, FavoriteHeroesResponse instance)
		{
			return FavoriteHeroesResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x000313DC File Offset: 0x0002F5DC
		public static FavoriteHeroesResponse DeserializeLengthDelimited(Stream stream)
		{
			FavoriteHeroesResponse favoriteHeroesResponse = new FavoriteHeroesResponse();
			FavoriteHeroesResponse.DeserializeLengthDelimited(stream, favoriteHeroesResponse);
			return favoriteHeroesResponse;
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x000313F8 File Offset: 0x0002F5F8
		public static FavoriteHeroesResponse DeserializeLengthDelimited(Stream stream, FavoriteHeroesResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FavoriteHeroesResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00031420 File Offset: 0x0002F620
		public static FavoriteHeroesResponse Deserialize(Stream stream, FavoriteHeroesResponse instance, long limit)
		{
			if (instance.FavoriteHeroes == null)
			{
				instance.FavoriteHeroes = new List<FavoriteHero>();
			}
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
					instance.FavoriteHeroes.Add(FavoriteHero.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000D45 RID: 3397 RVA: 0x000314B8 File Offset: 0x0002F6B8
		public void Serialize(Stream stream)
		{
			FavoriteHeroesResponse.Serialize(stream, this);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000314C4 File Offset: 0x0002F6C4
		public static void Serialize(Stream stream, FavoriteHeroesResponse instance)
		{
			if (instance.FavoriteHeroes.Count > 0)
			{
				foreach (FavoriteHero favoriteHero in instance.FavoriteHeroes)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, favoriteHero.GetSerializedSize());
					FavoriteHero.Serialize(stream, favoriteHero);
				}
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0003153C File Offset: 0x0002F73C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.FavoriteHeroes.Count > 0)
			{
				foreach (FavoriteHero favoriteHero in this.FavoriteHeroes)
				{
					num += 1U;
					uint serializedSize = favoriteHero.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000491 RID: 1169
		private List<FavoriteHero> _FavoriteHeroes = new List<FavoriteHero>();

		// Token: 0x020005CF RID: 1487
		public enum PacketID
		{
			// Token: 0x04001FB7 RID: 8119
			ID = 318
		}
	}
}
