using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200008B RID: 139
	public class NOP : IProtoBuf
	{
		// Token: 0x06000945 RID: 2373 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000222CB File Offset: 0x000204CB
		public override bool Equals(object obj)
		{
			return obj is NOP;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x000222D8 File Offset: 0x000204D8
		public void Deserialize(Stream stream)
		{
			NOP.Deserialize(stream, this);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x000222E2 File Offset: 0x000204E2
		public static NOP Deserialize(Stream stream, NOP instance)
		{
			return NOP.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x000222F0 File Offset: 0x000204F0
		public static NOP DeserializeLengthDelimited(Stream stream)
		{
			NOP nop = new NOP();
			NOP.DeserializeLengthDelimited(stream, nop);
			return nop;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0002230C File Offset: 0x0002050C
		public static NOP DeserializeLengthDelimited(Stream stream, NOP instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return NOP.Deserialize(stream, instance, num);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00022334 File Offset: 0x00020534
		public static NOP Deserialize(Stream stream, NOP instance, long limit)
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

		// Token: 0x0600094C RID: 2380 RVA: 0x000223A1 File Offset: 0x000205A1
		public void Serialize(Stream stream)
		{
			NOP.Serialize(stream, this);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, NOP instance)
		{
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x0200059E RID: 1438
		public enum PacketID
		{
			// Token: 0x04001F38 RID: 7992
			ID = 254
		}
	}
}
