using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200009D RID: 157
	public class LocaleMapEntry : IProtoBuf
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00026C9F File Offset: 0x00024E9F
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x00026CA7 File Offset: 0x00024EA7
		public long CatalogLocaleId
		{
			get
			{
				return this._CatalogLocaleId;
			}
			set
			{
				this._CatalogLocaleId = value;
				this.HasCatalogLocaleId = true;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00026CB7 File Offset: 0x00024EB7
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x00026CBF File Offset: 0x00024EBF
		public long GameLocaleId
		{
			get
			{
				return this._GameLocaleId;
			}
			set
			{
				this._GameLocaleId = value;
				this.HasGameLocaleId = true;
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00026CD0 File Offset: 0x00024ED0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCatalogLocaleId)
			{
				num ^= this.CatalogLocaleId.GetHashCode();
			}
			if (this.HasGameLocaleId)
			{
				num ^= this.GameLocaleId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00026D1C File Offset: 0x00024F1C
		public override bool Equals(object obj)
		{
			LocaleMapEntry localeMapEntry = obj as LocaleMapEntry;
			return localeMapEntry != null && this.HasCatalogLocaleId == localeMapEntry.HasCatalogLocaleId && (!this.HasCatalogLocaleId || this.CatalogLocaleId.Equals(localeMapEntry.CatalogLocaleId)) && this.HasGameLocaleId == localeMapEntry.HasGameLocaleId && (!this.HasGameLocaleId || this.GameLocaleId.Equals(localeMapEntry.GameLocaleId));
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00026D92 File Offset: 0x00024F92
		public void Deserialize(Stream stream)
		{
			LocaleMapEntry.Deserialize(stream, this);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00026D9C File Offset: 0x00024F9C
		public static LocaleMapEntry Deserialize(Stream stream, LocaleMapEntry instance)
		{
			return LocaleMapEntry.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00026DA8 File Offset: 0x00024FA8
		public static LocaleMapEntry DeserializeLengthDelimited(Stream stream)
		{
			LocaleMapEntry localeMapEntry = new LocaleMapEntry();
			LocaleMapEntry.DeserializeLengthDelimited(stream, localeMapEntry);
			return localeMapEntry;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00026DC4 File Offset: 0x00024FC4
		public static LocaleMapEntry DeserializeLengthDelimited(Stream stream, LocaleMapEntry instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LocaleMapEntry.Deserialize(stream, instance, num);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00026DEC File Offset: 0x00024FEC
		public static LocaleMapEntry Deserialize(Stream stream, LocaleMapEntry instance, long limit)
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
						instance.GameLocaleId = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.CatalogLocaleId = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00026E83 File Offset: 0x00025083
		public void Serialize(Stream stream)
		{
			LocaleMapEntry.Serialize(stream, this);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00026E8C File Offset: 0x0002508C
		public static void Serialize(Stream stream, LocaleMapEntry instance)
		{
			if (instance.HasCatalogLocaleId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CatalogLocaleId);
			}
			if (instance.HasGameLocaleId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameLocaleId);
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00026EC8 File Offset: 0x000250C8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCatalogLocaleId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.CatalogLocaleId);
			}
			if (this.HasGameLocaleId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.GameLocaleId);
			}
			return num;
		}

		// Token: 0x04000392 RID: 914
		public bool HasCatalogLocaleId;

		// Token: 0x04000393 RID: 915
		private long _CatalogLocaleId;

		// Token: 0x04000394 RID: 916
		public bool HasGameLocaleId;

		// Token: 0x04000395 RID: 917
		private long _GameLocaleId;
	}
}
