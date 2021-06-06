using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000057 RID: 87
	public class AckNotice : IProtoBuf
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00016AC6 File Offset: 0x00014CC6
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x00016ACE File Offset: 0x00014CCE
		public long Entry { get; set; }

		// Token: 0x06000597 RID: 1431 RVA: 0x00016AD8 File Offset: 0x00014CD8
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Entry.GetHashCode();
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00016B00 File Offset: 0x00014D00
		public override bool Equals(object obj)
		{
			AckNotice ackNotice = obj as AckNotice;
			return ackNotice != null && this.Entry.Equals(ackNotice.Entry);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00016B32 File Offset: 0x00014D32
		public void Deserialize(Stream stream)
		{
			AckNotice.Deserialize(stream, this);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00016B3C File Offset: 0x00014D3C
		public static AckNotice Deserialize(Stream stream, AckNotice instance)
		{
			return AckNotice.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00016B48 File Offset: 0x00014D48
		public static AckNotice DeserializeLengthDelimited(Stream stream)
		{
			AckNotice ackNotice = new AckNotice();
			AckNotice.DeserializeLengthDelimited(stream, ackNotice);
			return ackNotice;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00016B64 File Offset: 0x00014D64
		public static AckNotice DeserializeLengthDelimited(Stream stream, AckNotice instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AckNotice.Deserialize(stream, instance, num);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00016B8C File Offset: 0x00014D8C
		public static AckNotice Deserialize(Stream stream, AckNotice instance, long limit)
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
				else if (num == 8)
				{
					instance.Entry = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600059E RID: 1438 RVA: 0x00016C0B File Offset: 0x00014E0B
		public void Serialize(Stream stream)
		{
			AckNotice.Serialize(stream, this);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00016C14 File Offset: 0x00014E14
		public static void Serialize(Stream stream, AckNotice instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Entry);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00016C29 File Offset: 0x00014E29
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)this.Entry) + 1U;
		}

		// Token: 0x02000569 RID: 1385
		public enum PacketID
		{
			// Token: 0x04001E93 RID: 7827
			ID = 213,
			// Token: 0x04001E94 RID: 7828
			System = 0
		}
	}
}
