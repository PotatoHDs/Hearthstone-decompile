using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000E5 RID: 229
	public class TavernBrawlAckSessionRewards : IProtoBuf
	{
		// Token: 0x06000F75 RID: 3957 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00037B66 File Offset: 0x00035D66
		public override bool Equals(object obj)
		{
			return obj is TavernBrawlAckSessionRewards;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00037B73 File Offset: 0x00035D73
		public void Deserialize(Stream stream)
		{
			TavernBrawlAckSessionRewards.Deserialize(stream, this);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00037B7D File Offset: 0x00035D7D
		public static TavernBrawlAckSessionRewards Deserialize(Stream stream, TavernBrawlAckSessionRewards instance)
		{
			return TavernBrawlAckSessionRewards.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00037B88 File Offset: 0x00035D88
		public static TavernBrawlAckSessionRewards DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlAckSessionRewards tavernBrawlAckSessionRewards = new TavernBrawlAckSessionRewards();
			TavernBrawlAckSessionRewards.DeserializeLengthDelimited(stream, tavernBrawlAckSessionRewards);
			return tavernBrawlAckSessionRewards;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00037BA4 File Offset: 0x00035DA4
		public static TavernBrawlAckSessionRewards DeserializeLengthDelimited(Stream stream, TavernBrawlAckSessionRewards instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TavernBrawlAckSessionRewards.Deserialize(stream, instance, num);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00037BCC File Offset: 0x00035DCC
		public static TavernBrawlAckSessionRewards Deserialize(Stream stream, TavernBrawlAckSessionRewards instance, long limit)
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

		// Token: 0x06000F7C RID: 3964 RVA: 0x00037C39 File Offset: 0x00035E39
		public void Serialize(Stream stream)
		{
			TavernBrawlAckSessionRewards.Serialize(stream, this);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, TavernBrawlAckSessionRewards instance)
		{
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x020005E9 RID: 1513
		public enum PacketID
		{
			// Token: 0x04002001 RID: 8193
			ID = 345,
			// Token: 0x04002002 RID: 8194
			System = 0
		}
	}
}
