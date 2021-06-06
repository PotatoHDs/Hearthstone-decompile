using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000C1 RID: 193
	public class SetFavoriteHeroResponse : IProtoBuf
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x000315C3 File Offset: 0x0002F7C3
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x000315CB File Offset: 0x0002F7CB
		public bool Success { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x000315D4 File Offset: 0x0002F7D4
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x000315DC File Offset: 0x0002F7DC
		public FavoriteHero FavoriteHero
		{
			get
			{
				return this._FavoriteHero;
			}
			set
			{
				this._FavoriteHero = value;
				this.HasFavoriteHero = (value != null);
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x000315F0 File Offset: 0x0002F7F0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Success.GetHashCode();
			if (this.HasFavoriteHero)
			{
				num ^= this.FavoriteHero.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00031634 File Offset: 0x0002F834
		public override bool Equals(object obj)
		{
			SetFavoriteHeroResponse setFavoriteHeroResponse = obj as SetFavoriteHeroResponse;
			return setFavoriteHeroResponse != null && this.Success.Equals(setFavoriteHeroResponse.Success) && this.HasFavoriteHero == setFavoriteHeroResponse.HasFavoriteHero && (!this.HasFavoriteHero || this.FavoriteHero.Equals(setFavoriteHeroResponse.FavoriteHero));
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00031691 File Offset: 0x0002F891
		public void Deserialize(Stream stream)
		{
			SetFavoriteHeroResponse.Deserialize(stream, this);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0003169B File Offset: 0x0002F89B
		public static SetFavoriteHeroResponse Deserialize(Stream stream, SetFavoriteHeroResponse instance)
		{
			return SetFavoriteHeroResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x000316A8 File Offset: 0x0002F8A8
		public static SetFavoriteHeroResponse DeserializeLengthDelimited(Stream stream)
		{
			SetFavoriteHeroResponse setFavoriteHeroResponse = new SetFavoriteHeroResponse();
			SetFavoriteHeroResponse.DeserializeLengthDelimited(stream, setFavoriteHeroResponse);
			return setFavoriteHeroResponse;
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x000316C4 File Offset: 0x0002F8C4
		public static SetFavoriteHeroResponse DeserializeLengthDelimited(Stream stream, SetFavoriteHeroResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetFavoriteHeroResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x000316EC File Offset: 0x0002F8EC
		public static SetFavoriteHeroResponse Deserialize(Stream stream, SetFavoriteHeroResponse instance, long limit)
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
					else if (instance.FavoriteHero == null)
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
					instance.Success = ProtocolParser.ReadBool(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0003179D File Offset: 0x0002F99D
		public void Serialize(Stream stream)
		{
			SetFavoriteHeroResponse.Serialize(stream, this);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x000317A8 File Offset: 0x0002F9A8
		public static void Serialize(Stream stream, SetFavoriteHeroResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.Success);
			if (instance.HasFavoriteHero)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.FavoriteHero.GetSerializedSize());
				FavoriteHero.Serialize(stream, instance.FavoriteHero);
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x000317F8 File Offset: 0x0002F9F8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += 1U;
			if (this.HasFavoriteHero)
			{
				num += 1U;
				uint serializedSize = this.FavoriteHero.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 1U;
		}

		// Token: 0x04000493 RID: 1171
		public bool HasFavoriteHero;

		// Token: 0x04000494 RID: 1172
		private FavoriteHero _FavoriteHero;

		// Token: 0x020005D0 RID: 1488
		public enum PacketID
		{
			// Token: 0x04001FB9 RID: 8121
			ID = 320
		}
	}
}
