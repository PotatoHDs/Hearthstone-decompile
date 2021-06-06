using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200005A RID: 90
	public class Unsubscribe : IProtoBuf
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000171A3 File Offset: 0x000153A3
		public override bool Equals(object obj)
		{
			return obj is Unsubscribe;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000171B0 File Offset: 0x000153B0
		public void Deserialize(Stream stream)
		{
			Unsubscribe.Deserialize(stream, this);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000171BA File Offset: 0x000153BA
		public static Unsubscribe Deserialize(Stream stream, Unsubscribe instance)
		{
			return Unsubscribe.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000171C8 File Offset: 0x000153C8
		public static Unsubscribe DeserializeLengthDelimited(Stream stream)
		{
			Unsubscribe unsubscribe = new Unsubscribe();
			Unsubscribe.DeserializeLengthDelimited(stream, unsubscribe);
			return unsubscribe;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000171E4 File Offset: 0x000153E4
		public static Unsubscribe DeserializeLengthDelimited(Stream stream, Unsubscribe instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Unsubscribe.Deserialize(stream, instance, num);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001720C File Offset: 0x0001540C
		public static Unsubscribe Deserialize(Stream stream, Unsubscribe instance, long limit)
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

		// Token: 0x060005C9 RID: 1481 RVA: 0x00017279 File Offset: 0x00015479
		public void Serialize(Stream stream)
		{
			Unsubscribe.Serialize(stream, this);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, Unsubscribe instance)
		{
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x0200056C RID: 1388
		public enum PacketID
		{
			// Token: 0x04001E9B RID: 7835
			ID = 329
		}
	}
}
