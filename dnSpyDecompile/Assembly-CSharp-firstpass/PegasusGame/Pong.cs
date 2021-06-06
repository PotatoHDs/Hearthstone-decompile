using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001B1 RID: 433
	public class Pong : IProtoBuf
	{
		// Token: 0x06001B5E RID: 7006 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x00060ABB File Offset: 0x0005ECBB
		public override bool Equals(object obj)
		{
			return obj is Pong;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00060AC8 File Offset: 0x0005ECC8
		public void Deserialize(Stream stream)
		{
			Pong.Deserialize(stream, this);
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x00060AD2 File Offset: 0x0005ECD2
		public static Pong Deserialize(Stream stream, Pong instance)
		{
			return Pong.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x00060AE0 File Offset: 0x0005ECE0
		public static Pong DeserializeLengthDelimited(Stream stream)
		{
			Pong pong = new Pong();
			Pong.DeserializeLengthDelimited(stream, pong);
			return pong;
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x00060AFC File Offset: 0x0005ECFC
		public static Pong DeserializeLengthDelimited(Stream stream, Pong instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Pong.Deserialize(stream, instance, num);
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x00060B24 File Offset: 0x0005ED24
		public static Pong Deserialize(Stream stream, Pong instance, long limit)
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

		// Token: 0x06001B65 RID: 7013 RVA: 0x00060B91 File Offset: 0x0005ED91
		public void Serialize(Stream stream)
		{
			Pong.Serialize(stream, this);
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, Pong instance)
		{
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000648 RID: 1608
		public enum PacketID
		{
			// Token: 0x04002104 RID: 8452
			ID = 116
		}
	}
}
