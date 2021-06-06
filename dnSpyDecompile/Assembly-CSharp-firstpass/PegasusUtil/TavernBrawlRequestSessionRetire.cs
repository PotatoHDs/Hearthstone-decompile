using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000E4 RID: 228
	public class TavernBrawlRequestSessionRetire : IProtoBuf
	{
		// Token: 0x06000F6A RID: 3946 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00037A8A File Offset: 0x00035C8A
		public override bool Equals(object obj)
		{
			return obj is TavernBrawlRequestSessionRetire;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00037A97 File Offset: 0x00035C97
		public void Deserialize(Stream stream)
		{
			TavernBrawlRequestSessionRetire.Deserialize(stream, this);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00037AA1 File Offset: 0x00035CA1
		public static TavernBrawlRequestSessionRetire Deserialize(Stream stream, TavernBrawlRequestSessionRetire instance)
		{
			return TavernBrawlRequestSessionRetire.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00037AAC File Offset: 0x00035CAC
		public static TavernBrawlRequestSessionRetire DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlRequestSessionRetire tavernBrawlRequestSessionRetire = new TavernBrawlRequestSessionRetire();
			TavernBrawlRequestSessionRetire.DeserializeLengthDelimited(stream, tavernBrawlRequestSessionRetire);
			return tavernBrawlRequestSessionRetire;
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00037AC8 File Offset: 0x00035CC8
		public static TavernBrawlRequestSessionRetire DeserializeLengthDelimited(Stream stream, TavernBrawlRequestSessionRetire instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TavernBrawlRequestSessionRetire.Deserialize(stream, instance, num);
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x00037AF0 File Offset: 0x00035CF0
		public static TavernBrawlRequestSessionRetire Deserialize(Stream stream, TavernBrawlRequestSessionRetire instance, long limit)
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

		// Token: 0x06000F71 RID: 3953 RVA: 0x00037B5D File Offset: 0x00035D5D
		public void Serialize(Stream stream)
		{
			TavernBrawlRequestSessionRetire.Serialize(stream, this);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, TavernBrawlRequestSessionRetire instance)
		{
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x020005E8 RID: 1512
		public enum PacketID
		{
			// Token: 0x04001FFE RID: 8190
			ID = 344,
			// Token: 0x04001FFF RID: 8191
			System = 0
		}
	}
}
