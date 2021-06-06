using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000122 RID: 290
	public class CachedCollection : IProtoBuf
	{
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00042942 File Offset: 0x00040B42
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x0004294A File Offset: 0x00040B4A
		public List<CachedCard> CardCollection
		{
			get
			{
				return this._CardCollection;
			}
			set
			{
				this._CardCollection = value;
			}
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00042954 File Offset: 0x00040B54
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (CachedCard cachedCard in this.CardCollection)
			{
				num ^= cachedCard.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x000429B8 File Offset: 0x00040BB8
		public override bool Equals(object obj)
		{
			CachedCollection cachedCollection = obj as CachedCollection;
			if (cachedCollection == null)
			{
				return false;
			}
			if (this.CardCollection.Count != cachedCollection.CardCollection.Count)
			{
				return false;
			}
			for (int i = 0; i < this.CardCollection.Count; i++)
			{
				if (!this.CardCollection[i].Equals(cachedCollection.CardCollection[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00042A23 File Offset: 0x00040C23
		public void Deserialize(Stream stream)
		{
			CachedCollection.Deserialize(stream, this);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00042A2D File Offset: 0x00040C2D
		public static CachedCollection Deserialize(Stream stream, CachedCollection instance)
		{
			return CachedCollection.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00042A38 File Offset: 0x00040C38
		public static CachedCollection DeserializeLengthDelimited(Stream stream)
		{
			CachedCollection cachedCollection = new CachedCollection();
			CachedCollection.DeserializeLengthDelimited(stream, cachedCollection);
			return cachedCollection;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00042A54 File Offset: 0x00040C54
		public static CachedCollection DeserializeLengthDelimited(Stream stream, CachedCollection instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CachedCollection.Deserialize(stream, instance, num);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00042A7C File Offset: 0x00040C7C
		public static CachedCollection Deserialize(Stream stream, CachedCollection instance, long limit)
		{
			if (instance.CardCollection == null)
			{
				instance.CardCollection = new List<CachedCard>();
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
					instance.CardCollection.Add(CachedCard.DeserializeLengthDelimited(stream));
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

		// Token: 0x0600131F RID: 4895 RVA: 0x00042B14 File Offset: 0x00040D14
		public void Serialize(Stream stream)
		{
			CachedCollection.Serialize(stream, this);
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00042B20 File Offset: 0x00040D20
		public static void Serialize(Stream stream, CachedCollection instance)
		{
			if (instance.CardCollection.Count > 0)
			{
				foreach (CachedCard cachedCard in instance.CardCollection)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, cachedCard.GetSerializedSize());
					CachedCard.Serialize(stream, cachedCard);
				}
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00042B98 File Offset: 0x00040D98
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.CardCollection.Count > 0)
			{
				foreach (CachedCard cachedCard in this.CardCollection)
				{
					num += 1U;
					uint serializedSize = cachedCard.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040005EB RID: 1515
		private List<CachedCard> _CardCollection = new List<CachedCard>();
	}
}
