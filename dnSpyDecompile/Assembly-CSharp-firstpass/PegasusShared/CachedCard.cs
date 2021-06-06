using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000121 RID: 289
	public class CachedCard : IProtoBuf
	{
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x000424FC File Offset: 0x000406FC
		// (set) Token: 0x060012FE RID: 4862 RVA: 0x00042504 File Offset: 0x00040704
		public long CardId { get; set; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x0004250D File Offset: 0x0004070D
		// (set) Token: 0x06001300 RID: 4864 RVA: 0x00042515 File Offset: 0x00040715
		public int AssetCardId { get; set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x0004251E File Offset: 0x0004071E
		// (set) Token: 0x06001302 RID: 4866 RVA: 0x00042526 File Offset: 0x00040726
		public int UnixTimestamp { get; set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0004252F File Offset: 0x0004072F
		// (set) Token: 0x06001304 RID: 4868 RVA: 0x00042537 File Offset: 0x00040737
		public bool IsSeen { get; set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x00042540 File Offset: 0x00040740
		// (set) Token: 0x06001306 RID: 4870 RVA: 0x00042548 File Offset: 0x00040748
		public int Premium { get; set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x00042551 File Offset: 0x00040751
		// (set) Token: 0x06001308 RID: 4872 RVA: 0x00042559 File Offset: 0x00040759
		public int InsertSource { get; set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x00042562 File Offset: 0x00040762
		// (set) Token: 0x0600130A RID: 4874 RVA: 0x0004256A File Offset: 0x0004076A
		public long InsertData { get; set; }

		// Token: 0x0600130B RID: 4875 RVA: 0x00042574 File Offset: 0x00040774
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.CardId.GetHashCode() ^ this.AssetCardId.GetHashCode() ^ this.UnixTimestamp.GetHashCode() ^ this.IsSeen.GetHashCode() ^ this.Premium.GetHashCode() ^ this.InsertSource.GetHashCode() ^ this.InsertData.GetHashCode();
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x000425F8 File Offset: 0x000407F8
		public override bool Equals(object obj)
		{
			CachedCard cachedCard = obj as CachedCard;
			return cachedCard != null && this.CardId.Equals(cachedCard.CardId) && this.AssetCardId.Equals(cachedCard.AssetCardId) && this.UnixTimestamp.Equals(cachedCard.UnixTimestamp) && this.IsSeen.Equals(cachedCard.IsSeen) && this.Premium.Equals(cachedCard.Premium) && this.InsertSource.Equals(cachedCard.InsertSource) && this.InsertData.Equals(cachedCard.InsertData);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x000426BA File Offset: 0x000408BA
		public void Deserialize(Stream stream)
		{
			CachedCard.Deserialize(stream, this);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x000426C4 File Offset: 0x000408C4
		public static CachedCard Deserialize(Stream stream, CachedCard instance)
		{
			return CachedCard.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x000426D0 File Offset: 0x000408D0
		public static CachedCard DeserializeLengthDelimited(Stream stream)
		{
			CachedCard cachedCard = new CachedCard();
			CachedCard.DeserializeLengthDelimited(stream, cachedCard);
			return cachedCard;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x000426EC File Offset: 0x000408EC
		public static CachedCard DeserializeLengthDelimited(Stream stream, CachedCard instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CachedCard.Deserialize(stream, instance, num);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00042714 File Offset: 0x00040914
		public static CachedCard Deserialize(Stream stream, CachedCard instance, long limit)
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
				else
				{
					if (num <= 24)
					{
						if (num == 8)
						{
							instance.CardId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.AssetCardId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 24)
						{
							instance.UnixTimestamp = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num <= 40)
					{
						if (num == 32)
						{
							instance.IsSeen = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 40)
						{
							instance.Premium = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 48)
						{
							instance.InsertSource = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 56)
						{
							instance.InsertData = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
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

		// Token: 0x06001312 RID: 4882 RVA: 0x0004283D File Offset: 0x00040A3D
		public void Serialize(Stream stream)
		{
			CachedCard.Serialize(stream, this);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00042848 File Offset: 0x00040A48
		public static void Serialize(Stream stream, CachedCard instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CardId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AssetCardId));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.UnixTimestamp));
			stream.WriteByte(32);
			ProtocolParser.WriteBool(stream, instance.IsSeen);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Premium));
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.InsertSource));
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.InsertData);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x000428E4 File Offset: 0x00040AE4
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)this.CardId) + ProtocolParser.SizeOfUInt64((ulong)((long)this.AssetCardId)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.UnixTimestamp)) + 1U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Premium)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.InsertSource)) + ProtocolParser.SizeOfUInt64((ulong)this.InsertData) + 7U;
		}
	}
}
