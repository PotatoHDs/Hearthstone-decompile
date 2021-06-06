using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000066 RID: 102
	public class DoPurchase : IProtoBuf
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00018F43 File Offset: 0x00017143
		public override bool Equals(object obj)
		{
			return obj is DoPurchase;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00018F50 File Offset: 0x00017150
		public void Deserialize(Stream stream)
		{
			DoPurchase.Deserialize(stream, this);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00018F5A File Offset: 0x0001715A
		public static DoPurchase Deserialize(Stream stream, DoPurchase instance)
		{
			return DoPurchase.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00018F68 File Offset: 0x00017168
		public static DoPurchase DeserializeLengthDelimited(Stream stream)
		{
			DoPurchase doPurchase = new DoPurchase();
			DoPurchase.DeserializeLengthDelimited(stream, doPurchase);
			return doPurchase;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00018F84 File Offset: 0x00017184
		public static DoPurchase DeserializeLengthDelimited(Stream stream, DoPurchase instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DoPurchase.Deserialize(stream, instance, num);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00018FAC File Offset: 0x000171AC
		public static DoPurchase Deserialize(Stream stream, DoPurchase instance, long limit)
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

		// Token: 0x06000683 RID: 1667 RVA: 0x00019019 File Offset: 0x00017219
		public void Serialize(Stream stream)
		{
			DoPurchase.Serialize(stream, this);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, DoPurchase instance)
		{
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000578 RID: 1400
		public enum PacketID
		{
			// Token: 0x04001EBE RID: 7870
			ID = 273,
			// Token: 0x04001EBF RID: 7871
			System = 1
		}
	}
}
