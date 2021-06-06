using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200005E RID: 94
	public class DraftBegin : IProtoBuf
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00017A1B File Offset: 0x00015C1B
		public override bool Equals(object obj)
		{
			return obj is DraftBegin;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00017A28 File Offset: 0x00015C28
		public void Deserialize(Stream stream)
		{
			DraftBegin.Deserialize(stream, this);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00017A32 File Offset: 0x00015C32
		public static DraftBegin Deserialize(Stream stream, DraftBegin instance)
		{
			return DraftBegin.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00017A40 File Offset: 0x00015C40
		public static DraftBegin DeserializeLengthDelimited(Stream stream)
		{
			DraftBegin draftBegin = new DraftBegin();
			DraftBegin.DeserializeLengthDelimited(stream, draftBegin);
			return draftBegin;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00017A5C File Offset: 0x00015C5C
		public static DraftBegin DeserializeLengthDelimited(Stream stream, DraftBegin instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DraftBegin.Deserialize(stream, instance, num);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00017A84 File Offset: 0x00015C84
		public static DraftBegin Deserialize(Stream stream, DraftBegin instance, long limit)
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

		// Token: 0x060005FD RID: 1533 RVA: 0x00017AF1 File Offset: 0x00015CF1
		public void Serialize(Stream stream)
		{
			DraftBegin.Serialize(stream, this);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, DraftBegin instance)
		{
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000570 RID: 1392
		public enum PacketID
		{
			// Token: 0x04001EA6 RID: 7846
			ID = 235,
			// Token: 0x04001EA7 RID: 7847
			System = 0
		}
	}
}
