using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000043 RID: 67
	public class CardUseCount : IProtoBuf
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00011096 File Offset: 0x0000F296
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0001109E File Offset: 0x0000F29E
		public int Asset { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x000110A7 File Offset: 0x0000F2A7
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x000110AF File Offset: 0x0000F2AF
		public int Count { get; set; }

		// Token: 0x0600040D RID: 1037 RVA: 0x000110B8 File Offset: 0x0000F2B8
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Asset.GetHashCode() ^ this.Count.GetHashCode();
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000110F0 File Offset: 0x0000F2F0
		public override bool Equals(object obj)
		{
			CardUseCount cardUseCount = obj as CardUseCount;
			return cardUseCount != null && this.Asset.Equals(cardUseCount.Asset) && this.Count.Equals(cardUseCount.Count);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001113A File Offset: 0x0000F33A
		public void Deserialize(Stream stream)
		{
			CardUseCount.Deserialize(stream, this);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00011144 File Offset: 0x0000F344
		public static CardUseCount Deserialize(Stream stream, CardUseCount instance)
		{
			return CardUseCount.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00011150 File Offset: 0x0000F350
		public static CardUseCount DeserializeLengthDelimited(Stream stream)
		{
			CardUseCount cardUseCount = new CardUseCount();
			CardUseCount.DeserializeLengthDelimited(stream, cardUseCount);
			return cardUseCount;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001116C File Offset: 0x0000F36C
		public static CardUseCount DeserializeLengthDelimited(Stream stream, CardUseCount instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CardUseCount.Deserialize(stream, instance, num);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00011194 File Offset: 0x0000F394
		public static CardUseCount Deserialize(Stream stream, CardUseCount instance, long limit)
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
						instance.Count = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Asset = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001122D File Offset: 0x0000F42D
		public void Serialize(Stream stream)
		{
			CardUseCount.Serialize(stream, this);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00011236 File Offset: 0x0000F436
		public static void Serialize(Stream stream, CardUseCount instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Asset));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Count));
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00011261 File Offset: 0x0000F461
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Asset)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Count)) + 2U;
		}
	}
}
