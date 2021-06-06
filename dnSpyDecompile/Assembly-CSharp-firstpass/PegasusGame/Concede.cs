using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001AE RID: 430
	public class Concede : IProtoBuf
	{
		// Token: 0x06001B33 RID: 6963 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x000602ED File Offset: 0x0005E4ED
		public override bool Equals(object obj)
		{
			return obj is Concede;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x000602FA File Offset: 0x0005E4FA
		public void Deserialize(Stream stream)
		{
			Concede.Deserialize(stream, this);
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x00060304 File Offset: 0x0005E504
		public static Concede Deserialize(Stream stream, Concede instance)
		{
			return Concede.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x00060310 File Offset: 0x0005E510
		public static Concede DeserializeLengthDelimited(Stream stream)
		{
			Concede concede = new Concede();
			Concede.DeserializeLengthDelimited(stream, concede);
			return concede;
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0006032C File Offset: 0x0005E52C
		public static Concede DeserializeLengthDelimited(Stream stream, Concede instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Concede.Deserialize(stream, instance, num);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x00060354 File Offset: 0x0005E554
		public static Concede Deserialize(Stream stream, Concede instance, long limit)
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

		// Token: 0x06001B3A RID: 6970 RVA: 0x000603C1 File Offset: 0x0005E5C1
		public void Serialize(Stream stream)
		{
			Concede.Serialize(stream, this);
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, Concede instance)
		{
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000645 RID: 1605
		public enum PacketID
		{
			// Token: 0x040020FE RID: 8446
			ID = 11
		}
	}
}
