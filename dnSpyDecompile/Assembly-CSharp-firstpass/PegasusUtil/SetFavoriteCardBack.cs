using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200006C RID: 108
	public class SetFavoriteCardBack : IProtoBuf
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x000199E5 File Offset: 0x00017BE5
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x000199ED File Offset: 0x00017BED
		public int CardBack { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x000199F6 File Offset: 0x00017BF6
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x000199FE File Offset: 0x00017BFE
		public long DeprecatedDeckId
		{
			get
			{
				return this._DeprecatedDeckId;
			}
			set
			{
				this._DeprecatedDeckId = value;
				this.HasDeprecatedDeckId = true;
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00019A10 File Offset: 0x00017C10
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.CardBack.GetHashCode();
			if (this.HasDeprecatedDeckId)
			{
				num ^= this.DeprecatedDeckId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00019A54 File Offset: 0x00017C54
		public override bool Equals(object obj)
		{
			SetFavoriteCardBack setFavoriteCardBack = obj as SetFavoriteCardBack;
			return setFavoriteCardBack != null && this.CardBack.Equals(setFavoriteCardBack.CardBack) && this.HasDeprecatedDeckId == setFavoriteCardBack.HasDeprecatedDeckId && (!this.HasDeprecatedDeckId || this.DeprecatedDeckId.Equals(setFavoriteCardBack.DeprecatedDeckId));
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00019AB4 File Offset: 0x00017CB4
		public void Deserialize(Stream stream)
		{
			SetFavoriteCardBack.Deserialize(stream, this);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00019ABE File Offset: 0x00017CBE
		public static SetFavoriteCardBack Deserialize(Stream stream, SetFavoriteCardBack instance)
		{
			return SetFavoriteCardBack.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00019ACC File Offset: 0x00017CCC
		public static SetFavoriteCardBack DeserializeLengthDelimited(Stream stream)
		{
			SetFavoriteCardBack setFavoriteCardBack = new SetFavoriteCardBack();
			SetFavoriteCardBack.DeserializeLengthDelimited(stream, setFavoriteCardBack);
			return setFavoriteCardBack;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00019AE8 File Offset: 0x00017CE8
		public static SetFavoriteCardBack DeserializeLengthDelimited(Stream stream, SetFavoriteCardBack instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetFavoriteCardBack.Deserialize(stream, instance, num);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00019B10 File Offset: 0x00017D10
		public static SetFavoriteCardBack Deserialize(Stream stream, SetFavoriteCardBack instance, long limit)
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
						instance.DeprecatedDeckId = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.CardBack = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00019BA8 File Offset: 0x00017DA8
		public void Serialize(Stream stream)
		{
			SetFavoriteCardBack.Serialize(stream, this);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00019BB1 File Offset: 0x00017DB1
		public static void Serialize(Stream stream, SetFavoriteCardBack instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CardBack));
			if (instance.HasDeprecatedDeckId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedDeckId);
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00019BE4 File Offset: 0x00017DE4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CardBack));
			if (this.HasDeprecatedDeckId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.DeprecatedDeckId);
			}
			return num + 1U;
		}

		// Token: 0x0400023C RID: 572
		public bool HasDeprecatedDeckId;

		// Token: 0x0400023D RID: 573
		private long _DeprecatedDeckId;

		// Token: 0x0200057F RID: 1407
		public enum PacketID
		{
			// Token: 0x04001ED9 RID: 7897
			ID = 291,
			// Token: 0x04001EDA RID: 7898
			System = 0
		}
	}
}
